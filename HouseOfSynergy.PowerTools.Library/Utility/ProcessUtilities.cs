using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.CodeDom;
using System.CodeDom.Compiler;

namespace HouseOfSynergy.PowerTools.Library.Utility
{
	public enum ProcessRestarterMode
	{
		None,
		Test,
		Restart,
	}

	[Flags]
	public enum ProcessRestarterExitCode:
		int
	{
		Success = 2 >> 2,
		ErrorArgumentCount = 2 >> 1,
		ErrorArgumentMode = 2 << 0,
		ErrorArgumentProcessId = 2 << 1,
		ErrorArgumentProcessFilename = 2 << 2,
		ErrorProcessStart = 2 << 3,
		Unknown = 2 << 4,
	}

	public static class ProcessUtilities
	{
		public const int ProcessExitCodeSuccess = 0;
		public const string ProcessArgumentsRestarted = "-Restarted";

		private const string NameModule = ProcessUtilities.NameAssembly;
		private const string NameModuleFile = ProcessUtilities.NameAssembly + @".exe";
		private const string NameAssemblyFile = ProcessUtilities.NameAssembly + @".exe";
		private const string NameAssembly = @"HouseOfSynergy.PowerTools.ProcessRestarter";

		/// <summary>
		/// Launches the process restarter that will wait for the specified time for the currrent process to terminate and then relaunch it.
		/// If the current process terminates within the time specified, it will be relaunched with the command line arguments as follows:
		///  (1): [ProcessUtilities.ProcessArgumentsRestarted].
		///  (2): [The filename (including path) of the process restarter assembly so it can be deleted if required.].
		/// </summary>
		/// <param name="timeout">The amount of time to wait the the specified assembly to terminate.</param>
		/// <param name="exception">When this method returns, the out argument [exception] contains null if the function succeeded or an exception object explaining why the function failed.</param>
		/// <returns>Returns true if the assembly restarter process was launched successfully. False otherwise.</returns>
		//[ReflectionPermission(SecurityAction.PermitOnly, Flags = ReflectionPermissionFlag.RestrictedMemberAccess, MemberAccess = false, RestrictedMemberAccess = false)]
		public static bool RestartCurrentProcess (TimeSpan timeout, out Exception exception)
		{
			var result = false;
			var info = new ProcessStartInfo();

			exception = null;

			try
			{
				//var directory = new DirectoryInfo(Path.GetTempPath());
				//var file = PathUtilities.GetTempFile(directory, ".exe", true);
				//ShellUtilities.LaunchExplorerWithFileSelection(file);

				var directory = PathUtilities.GetTempDirectory(Global.Instance.Debug ? (new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Desktop))) : (new DirectoryInfo(Path.GetTempPath())), true);
				var file = new FileInfo(Path.Combine(directory.FullName, Global.Instance.Debug ? (ProcessUtilities.NameAssemblyFile) : (PathUtilities.GetTempFile(directory, ".exe", true).FullName)));

				//ProcessUtilities.GenerateAssembly(file);
				result = ProcessUtilities.GenerateAssembly(file, out exception);

				if (result)
				{
					result = false;

					// Launch the restarter assembly in test mode.
					using (var process = Process.GetCurrentProcess())
					{
						info = new ProcessStartInfo(file.FullName, string.Format("{0} {1} \"{2}\"", ProcessRestarterMode.Test.ToString(), process.Id.ToString(), process.MainModule.FileName));
					}

					//ShellUtilities.LaunchExplorerWithFileSelection(file);
					//Process.Start(@"C:\Program Files\Telerik\JustDecompile\Libraries\JustDecompile.exe", "\"" + file.FullName + "\"");
					//return (false);

					using (var restarter = Process.Start(info))
					{
						if (restarter.WaitForExit((int) TimeSpan.FromSeconds(Global.Instance.Debug ? 60 : 5).TotalMilliseconds))
						{
							switch ((ProcessRestarterExitCode) restarter.ExitCode)
							{
								case ProcessRestarterExitCode.Success:
								{
									result = true;

									break;
								}
								default:
								{
									throw (new ProcessRestarterExitCodeException((ProcessRestarterExitCode) restarter.ExitCode, "The assembly restarter process exited with code [" + ((ProcessRestarterExitCode) restarter.ExitCode).ToString() + "]."));
								}
							}
						}
						else
						{
							try { restarter.Kill(); }
							catch { }

							throw (new ProcessRestarterTimeoutException("The assembly restarter process did not respond within a reasonable time and was terminated."));
						}
					}

					if (result)
					{
						result = false;

						using (var process = Process.GetCurrentProcess())
						{
							info = new ProcessStartInfo(file.FullName, string.Format("{0} {1} \"{2}\"", ProcessRestarterMode.Restart.ToString(), process.Id.ToString(), process.MainModule.FileName));
						}

						Process.Start(info);

						result = true;
					}
				}
			}
			catch (Exception e)
			{
				exception = e;
			}
			finally
			{
			}

			return (result);
		}

		/// <summary>
		/// Generates the process restarter assembly using Reflection.Emit.
		/// </summary>
		/// <param name="file">The assembly filename.</param>
		private static void GenerateAssembly (FileInfo file)
		{
			using (var provider = System.CodeDom.Compiler.CodeDomProvider.CreateProvider("CSharp"))
			{
				var references = new string [] { "System" };
				var snippent = new CodeSnippetExpression("public static class Program { }");
				var parameters = new CompilerParameters(references, file.FullName, true);

				var result = provider.CompileAssemblyFromSource(parameters, snippent.ToString());

				var t = result.CompiledAssembly;
			}
		}

		/// <summary>
		/// Generates the process restarter assembly using Reflection.Emit.
		/// </summary>
		/// <param name="file">The assembly filename.</param>
		/// <param name="exception">When the method reurns, the argument [exception] will be null if the operation succeeded or will contain an exception object explaining why the operation failed.</param>
		/// <returns>Returns true if the operation succeeded, false otherwise.</returns>
		private static bool GenerateAssembly (FileInfo file, out Exception exception)
		{
			bool result = false;

			exception = null;

			try
			{
				var domain = AppDomain.CurrentDomain;
				var name = new AssemblyName(ProcessUtilities.NameAssembly);
				var builderAssembly = domain.DefineDynamicAssembly(name, AssemblyBuilderAccess.RunAndSave, file.Directory.FullName);
				var builderModule = builderAssembly.DefineDynamicModule(ProcessUtilities.NameModule, ProcessUtilities.NameModuleFile, Global.Instance.Debug);
				var builderType = builderModule.DefineType(ProcessUtilities.NameAssembly + ".Program", TypeAttributes.Class | TypeAttributes.NotPublic | TypeAttributes.Sealed | TypeAttributes.Abstract);
				var builderMethod = builderType.DefineMethod("Main", MethodAttributes.HideBySig | MethodAttributes.Static | MethodAttributes.Private, typeof(int), new Type [] { typeof(string []) });
				var generator = builderMethod.GetILGenerator();
				var builderEnumMode = builderModule.DefineEnum(ProcessUtilities.NameAssembly + "." + typeof(ProcessRestarterMode).Name, TypeAttributes.Public, typeof(int));
				var builderEnumCode = builderModule.DefineEnum(ProcessUtilities.NameAssembly + "." + typeof(ProcessRestarterExitCode).Name, TypeAttributes.Public, typeof(int));
				var builderConstructorStatic = builderType.DefineConstructor(MethodAttributes.Static, CallingConventions.Standard, new Type [] { });
				var builderConstructorDefault = builderType.DefineDefaultConstructor(MethodAttributes.Private);

				//builderAssembly.refe

				EnumUtilities.GetValues<ProcessRestarterMode>().ForEach(value => builderEnumMode.DefineLiteral(value.ToString(), (int) value));
				builderEnumMode.CreateType();

				EnumUtilities.GetValues<ProcessRestarterExitCode>().ForEach(value => builderEnumCode.DefineLiteral(value.ToString(), (int) value));
				builderEnumCode.CreateType();
				var builderCustomAttribute = new CustomAttributeBuilder(typeof(FlagsAttribute).GetConstructor(new Type [] { }), new object [] { });
				builderEnumCode.SetCustomAttribute(builderCustomAttribute);

				ProcessUtilities.GenerateMainMethodBody(builderConstructorStatic);
				ProcessUtilities.GenerateMainMethodBody(builderMethod, builderEnumMode, builderEnumCode);

				builderType.CreateType();
				builderModule.CreateGlobalFunctions();

				builderAssembly.SetEntryPoint(builderMethod, PEFileKinds.ConsoleApplication);

				//if (file.Exists) { file.Delete(); file.Refresh(); }
				builderAssembly.Save(file.Name, PortableExecutableKinds.ILOnly, ImageFileMachine.I386);

				result = true;
			}
			catch (Exception e)
			{
				exception = e;
			}

			return (result);
		}

		/// <summary>
		/// Generates a minimum method body.
		/// </summary>
		private static void GenerateMainMethodBody (ConstructorBuilder builderConstructor)
		{
			var generator = builderConstructor.GetILGenerator();

			generator.Emit(OpCodes.Ret);
		}

		/// <summary>
		/// Generates the process restarter main method body using Expression Trees.
		/// </summary>
		private static void GenerateMainMethodBody (MethodBuilder builderMethod, EnumBuilder builderEnumMode, EnumBuilder builderEnumCode)
		{
			var queue = new Queue<Expression>();
			var variables = new Queue<ParameterExpression>();

			// Declare common expressions.
			var one = Expression.Constant(1, typeof(int));
			var two = Expression.Constant(2, typeof(int));
			var zero = Expression.Constant(0, typeof(int));
			var three = Expression.Constant(3, typeof(int));
			var arguments = Expression.Parameter(typeof(string []), "args");
			var length = Expression.ArrayLength(arguments);
			var argumentMode = Expression.ArrayIndex(arguments, zero);
			var argumentProcessId = Expression.ArrayIndex(arguments, one);
			var argumentFilename = Expression.ArrayIndex(arguments, two);
			var @break = Expression.Label();
			var writeline = Expression.Call(typeof(Console).GetMethod("WriteLine", new Type [] { }));
			var writeline2 = Expression.Block(writeline, writeline);
			var methodWriteInt32 = typeof(Console).GetMethod("Write", new Type [] { typeof(int) });
			var methodWriteString = typeof(Console).GetMethod("Write", new Type [] { typeof(string) });
			var methodWriteObject = typeof(Console).GetMethod("Write", new Type [] { typeof(object) });
			var methodWriteBoolean = typeof(Console).GetMethod("Write", new Type [] { typeof(bool) });
			var intTryParse = typeof(int).GetMethod("TryParse", new Type [] { typeof(string), typeof(int).MakeByRefType(), });

			// Declare and assign local variables.
			var i = Expression.Parameter(typeof(int), "i");
			var code = Expression.Variable(builderEnumCode, "code");
			var mode = Expression.Variable(builderEnumMode, "mode");
			var test = Expression.Variable(typeof(bool), "isTest");
			var id = Expression.Variable(typeof(int), "processId");
			var filename = Expression.Variable(typeof(string), "filename");
			var file = Expression.Variable(typeof(FileInfo), "file");
			var exists = Expression.Variable(typeof(bool), "exists");
			variables.Enqueue(code);
			variables.Enqueue(mode);
			variables.Enqueue(i);
			variables.Enqueue(test);
			variables.Enqueue(id);
			variables.Enqueue(filename);
			variables.Enqueue(file);
			queue.Enqueue(Expression.Assign(i, zero));
			queue.Enqueue(Expression.Assign(test, Expression.Constant(false)));
			queue.Enqueue(Expression.Assign(mode, Expression.Constant((int) ProcessRestarterMode.None, builderEnumMode)));
			queue.Enqueue(Expression.Assign(code, Expression.Constant((int) ProcessRestarterExitCode.Unknown, builderEnumCode)));
			queue.Enqueue(Expression.Assign(id, zero));
			queue.Enqueue(Expression.Assign(filename, Expression.Constant("")));

			queue.Enqueue(Expression.Assign(Expression.Property(null, typeof(Console).GetProperty("Title")), Expression.Constant("Process Restarter")));
			queue.Enqueue(Expression.Call(methodWriteString, Expression.Constant("Process Restarter:")));
			queue.Enqueue(writeline);
			queue.Enqueue(writeline);

			queue.Enqueue(Expression.Call(methodWriteString, Expression.Constant("Argument(s): [", typeof(string))));
			queue.Enqueue(Expression.Call(methodWriteInt32, length));
			queue.Enqueue(Expression.Call(methodWriteString, Expression.Constant("].", typeof(string))));

			queue.Enqueue(Expression.IfThen(Expression.GreaterThan(length, zero), writeline));
			queue.Enqueue
			(
				Expression.Loop
				(
					Expression.IfThenElse
					(
						Expression.LessThan(i, length),
						Expression.Block
						(
							writeline,
							Expression.Call(methodWriteString, Expression.Constant(" - [")),
							Expression.Call(methodWriteString, Expression.ArrayIndex(arguments, Expression.PostIncrementAssign(i))),
							Expression.Call(methodWriteString, Expression.Constant("]."))
						),
						Expression.Break(@break)
					),
					@break
				)
			);

			queue.Enqueue
			(
				Expression.IfThenElse
				(
					Expression.Equal(length, three),
					Expression.IfThenElse
					(
						Expression.Equal(argumentMode, Expression.Constant(ProcessRestarterMode.Test.ToString())),
						Expression.Assign(mode, Expression.Constant((int) ProcessRestarterMode.Test, builderEnumMode)),
						Expression.IfThen
						(
							Expression.Equal(argumentMode, Expression.Constant(ProcessRestarterMode.Restart.ToString())),
							Expression.Assign(mode, Expression.Constant((int) ProcessRestarterMode.Restart, builderEnumMode))
						)
					),
					Expression.Assign(code, Expression.Constant((int) ProcessRestarterExitCode.ErrorArgumentCount, builderEnumCode))
				)
			);

			//queue.Enqueue
			//(
			//	Expression.IfThenElse
			//	(
			//		Expression.Equal(Expression.Convert(mode, typeof(int)), Expression.Constant((int) ProcessRestarterMode.None)),
			//		Expression.OrAssign(code, Expression.Constant((int) ProcessRestarterResult.ErrorArgumentType, builderEnumCode)),
			//		Expression.Block
			//		(
			//			Expression.Assign(file, Expression.New(typeof(FileInfo).GetConstructor(new Type [] { typeof(string) }), argumentFilename)),
			//			Expression.Assign(exists, Expression.Property(file, "Exists")),
			//			Expression.IfThenElse
			//			(
			//				Expression.Equal(exists, Expression.Constant(false)),
			//				Expression.OrAssign(code, Expression.Constant((int) ProcessRestarterResult.ErrorArgumentProcessName, builderEnumCode)),
			//				// Launch process.
			//				Expression.Block
			//				(
			//					writeline2,
			//					Expression.Call(methodWriteString, Expression.Constant("Launching proces..."))
			//				)
			//			)
			//		)
			//	)
			//);

			//Expression.IfThenElse
			//(
			//    Expression.Equal(length, two),
			//    Expression.IfThenElse
			//    (
			//        Expression.Call(intTryParse, Expression.ArrayIndex(arguments, zero), id),
			//        // TODO: Check filename argument.
			//        Expression.Assign(code, Expression.Constant((int) ProcessExitCodes.Success, builderEnumCode)),
			//        Expression.Assign(code, Expression.Constant((int) ProcessExitCodes.ErrorProcessId, builderEnumCode))
			//    ),
			//)
			//Expression.Assign(code, Expression.Constant((int) ProcessExitCodes.ErrorArgumentCount, builderEnumCode))

			queue.Enqueue(writeline2);
			queue.Enqueue(Expression.Call(methodWriteString, Expression.Constant("Mode: [")));
			queue.Enqueue(Expression.Call(methodWriteString, Expression.Call(mode, mode.Type.GetMethod("ToString", new Type [] { }))));
			queue.Enqueue(Expression.Call(methodWriteString, Expression.Constant(" (")));
			queue.Enqueue(Expression.Call(methodWriteInt32, Expression.Convert(mode, typeof(int))));
			queue.Enqueue(Expression.Call(methodWriteString, Expression.Constant(")")));
			queue.Enqueue(Expression.Call(methodWriteString, Expression.Constant("].")));
			queue.Enqueue(writeline);
			queue.Enqueue(Expression.Call(methodWriteString, Expression.Constant("Exit Code: [")));
			queue.Enqueue(Expression.Call(methodWriteString, Expression.Call(code, code.Type.GetMethod("ToString", new Type [] { }))));
			queue.Enqueue(Expression.Call(methodWriteString, Expression.Constant(" (")));
			queue.Enqueue(Expression.Call(methodWriteInt32, Expression.Convert(code, typeof(int))));
			queue.Enqueue(Expression.Call(methodWriteString, Expression.Constant(")")));
			queue.Enqueue(Expression.Call(methodWriteString, Expression.Constant("].")));

			queue.Enqueue(writeline);
			queue.Enqueue(writeline);
			queue.Enqueue(Expression.Call(methodWriteString, Expression.Constant("Press any key to continue...", typeof(string))));
			queue.Enqueue(Expression.Call(typeof(Console).GetMethod("ReadKey", new Type [] { typeof(bool) }), Expression.Constant(true, typeof(bool))));
			queue.Enqueue(Expression.Convert(code, typeof(int)));

			var block = Expression.Block(variables.ToArray(), queue);
			var lambda = Expression.Lambda<Func<string [], int>>(block, new ParameterExpression [] { arguments });

			//var function = lambda.Compile();
			//var result = function(new string [] { "?" });
			lambda.CompileToMethod(builderMethod);
		}

		/// <summary>
		/// Generates the process restarter main method body using Reflection.Emit.
		/// </summary>
		private static void GenerateMainMethodBody (ILGenerator generator, EnumBuilder builderEnum)
		{
			//var labelTry = generator.BeginExceptionBlock();
			//generator.BeginFaultBlock();
			//generator.Emit(OpCodes.Leave, labelTry);
			//generator.BeginCatchBlock(typeof(Exception));
			//generator.Emit(OpCodes.Leave_S, labelTry);
			//generator.EndExceptionBlock();

			// Build a local variable to store the exit code.
			var builderLocal = generator.DeclareLocal(builderEnum);

			// Assign [ProcessExitCodes.Unknown] as the default value for the exit code.
			generator.Emit(OpCodes.Ldc_I4, (int) ProcessRestarterExitCode.Unknown);
			generator.Emit(OpCodes.Stloc_0);

			generator.Emit(OpCodes.Ldstr, "Assembly Restarter."); // Push string literal reference.
			generator.Emit(OpCodes.Call, typeof(Console).GetMethod("Write", new Type [] { typeof(string) }));

			generator.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new Type [] { }));
			generator.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new Type [] { }));

			generator.Emit(OpCodes.Ldstr, "Press any key to continue..."); // Push string literal reference.
			generator.Emit(OpCodes.Call, typeof(Console).GetMethod("Write", new Type [] { typeof(string) }));

			generator.Emit(OpCodes.Ldc_I4_1); // Push bool (true).
			generator.Emit(OpCodes.Call, typeof(Console).GetMethod("ReadKey", new Type [] { typeof(bool) }));
			generator.Emit(OpCodes.Pop); // Pop ConsoleKeyInfo.

			generator.Emit(OpCodes.Ldc_I4, 0); // Return value.
			generator.Emit(OpCodes.Ret);
		}
	}
}