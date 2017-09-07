using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using HouseOfSynergy.AffinityDms.WebRole.Controllers;
using HouseOfSynergy.PowerTools.Library.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HouseOfSynergy.AffinityDms.UnitTests.Compliance
{
	[TestClass]
	public class Conventions
	{
		[TestMethod]
		public void ControllerPublicMethods_HttpVerbAttributes_IsSingleApplied ()
		{
			var message = "";
			var result = true;

			var controllers = typeof(HomeController)
				.Assembly
				.GetTypes()
				.Where(t => t.BaseType == typeof(Controller))
				.ToList();

			foreach (var controller in controllers)
			{
				var addedEntry = false;
				var methodInfos = controller
					.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
					.Where(mi => !mi.IsSpecialName)
					.ToList();

				foreach (var methodInfo in methodInfos)
				{
					var countVerbs = 0;
					var attributes = methodInfo
						.GetCustomAttributes()
						.ToList();

					foreach (var attribute in attributes)
					{
						if (attribute is HttpGetAttribute)
						{
							countVerbs++;
							if (countVerbs == 2)
							{
								result = false;
								if (!addedEntry) { addedEntry = true; message += $"{Environment.NewLine}[{controller.Name}]:"; }
								message += $"{Environment.NewLine}	Public method [{methodInfo.Name}]: Multiple [HttpXXX] verbs found.";
							}
						}

						if (attribute is HttpPostAttribute)
						{
							countVerbs++;
							if (countVerbs == 2)
							{
								result = false;
								if (!addedEntry) { addedEntry = true; message += $"{Environment.NewLine}[{controller.Name}]:"; }
								message += $"{Environment.NewLine}	Public method [{methodInfo.Name}]: Multiple [HttpXXX] verbs found.";
							}
						}

						if (attribute is HttpPutAttribute)
						{
							countVerbs++;
							if (countVerbs == 2)
							{
								result = false;
								if (!addedEntry) { addedEntry = true; message += $"{Environment.NewLine}[{controller.Name}]:"; }
								message += $"{Environment.NewLine}	Public method [{methodInfo.Name}]: Multiple [HttpXXX] verbs found.";
							}
						}

						if (attribute is HttpDeleteAttribute)
						{
							countVerbs++;
							if (countVerbs == 2)
							{
								result = false;
								if (!addedEntry) { addedEntry = true; message += $"{Environment.NewLine}[{controller.Name}]:"; }
								message += $"{Environment.NewLine}	Public method [{methodInfo.Name}]: Multiple [HttpXXX] verbs found.";
							}
						}

						if (countVerbs == 0)
						{
							result = false;
							if (!addedEntry) { addedEntry = true; message += $"{Environment.NewLine}[{controller.Name}]:"; }
							message += $"{Environment.NewLine}	Public method [{methodInfo.Name}]: No [HttpXXX] verb found.";
						}
					}
				}
			}

			if (!result)
			{
				Assert.Fail(message);
			}
		}

		[TestMethod]
		public void ControllerPublicMethods_LocalVariables_AreInitializedToDefaults ()
		{
			var message = "";
			var result = true;

			var types = typeof(HomeController)
				.Assembly
				.GetTypes()
				.ToList();

			foreach (var type in types)
			{
				var methodInfos = type
					.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
					.Where(mi => !mi.IsSpecialName)
					.ToList();

				foreach (var methodInfo in methodInfos)
				{
					var methodBody = methodInfo.GetMethodBody();

					if (methodBody == null)
					{
						result = false;
						message += $"{Environment.NewLine}Assembly: [{type.Assembly.GetName().Name}], Class: [{type.Name}], Method [{methodInfo.Name}]: Method body is null.";
					}
					else
					{
						if (!methodBody.InitLocals)
						{
							result = false;
							message += $"{Environment.NewLine}Assembly: [{type.Assembly.GetName().Name}], Class: [{type.Name}], Method [{methodInfo.Name}]: Locals not initialized.";
						}
					}
				}
			}

			if (!result)
			{
				Assert.Fail(message);
			}
		}
	}
}