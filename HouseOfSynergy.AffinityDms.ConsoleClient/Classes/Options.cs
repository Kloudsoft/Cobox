using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HouseOfSynergy.PowerTools.Library.Interfaces;
using HouseOfSynergy.PowerTools.Library.Utility;
using Microsoft.Win32;

namespace HouseOfSynergy.AffinityDms.ConsoleClient.Classes
{
	public class Options:
		ICloneable<Options>,
		ICopyable<Options>
	{
		public Global Global { get; private set; }

		public string PathDocumentPageImportLast { get; set; }
		public string PathDocumentTypeLast { get; set; }
		public string PathDocumentSetExportLast { get; set; }

		public Options ()
		{
		}

		public void Initialize ()
		{
		}

		public void Initialize (Global global)
		{
			this.Global = global;
		}

		public bool Save (bool throwOnError = true)
		{
			var result = true;
			var exceptions = new List<Exception>();

			try
			{
				var properties = this.GetProperties();

				using (var key = this.GetRegistryKey())
				{
					foreach (var property in properties)
					{
						try
						{
							if
							(
								(property.PropertyType == typeof(byte))
								|| (property.PropertyType == typeof(sbyte))
								|| (property.PropertyType == typeof(short))
								|| (property.PropertyType == typeof(ushort))
								|| (property.PropertyType == typeof(int))
								|| (property.PropertyType == typeof(uint))
								|| (property.PropertyType == typeof(byte?))
								|| (property.PropertyType == typeof(sbyte?))
								|| (property.PropertyType == typeof(short?))
								|| (property.PropertyType == typeof(ushort?))
								|| (property.PropertyType == typeof(int?))
								|| (property.PropertyType == typeof(uint?))
							)
							{
								key.SetValue(property.Name, property.GetValue(this) ?? 0, RegistryValueKind.DWord);
							}
							else if
							(
								(property.PropertyType == typeof(long))
								|| (property.PropertyType == typeof(ulong))
								|| (property.PropertyType == typeof(long?))
								|| (property.PropertyType == typeof(ulong?))
							)
							{
								key.SetValue(property.Name, property.GetValue(this) ?? 0, RegistryValueKind.QWord);
							}
							else if (property.PropertyType == typeof(string))
							{
								key.SetValue(property.Name, property.GetValue(this) ?? "", RegistryValueKind.String);
							}
							else if (property.PropertyType.IsEnum)
							{
								key.SetValue(property.Name, property.GetValue(this) ?? 0, RegistryValueKind.DWord);
							}
							else if (property.PropertyType == typeof(bool))
							{
								key.SetValue(property.Name, (property.GetValue(this) ?? false).ToString(), RegistryValueKind.String);
							}
						}
						catch (Exception exception)
						{
							result = false;
							exceptions.Add(exception);
						}
					}
				}
			}
			catch
			{
				result = false;
				if (throwOnError) { throw; }
			}

			if (exceptions.Any())
			{
				if (throwOnError)
				{
					var message
						= "One or more errors occurred during save:"
						+ Environment.NewLine
						+ Environment.NewLine
						+ string.Join(Environment.NewLine, exceptions.ConvertAll(e => e.Message));

					throw (new AggregateException(message, exceptions));
				}
			}

			return (result);
		}

		public bool Load (bool throwOnError = true)
		{
			var result = true;
			var exceptions = new List<Exception>();

			try
			{
				var properties = this.GetProperties();

				using (var key = this.GetRegistryKey())
				{
					foreach (var property in properties)
					{
						try
						{
							if (property.PropertyType == typeof(bool))
							{
								property.SetValue(this, bool.Parse(key.GetValue(property.Name, this.GetDefaultValueForType(property.PropertyType)).ToString()));
							}
							else if (property.PropertyType.IsEnum)
							{
								property.SetValue(this, key.GetValue(property.Name, this.GetDefaultValueForType(property.PropertyType)));
							}
							else
							{
								property.SetValue(this, key.GetValue(property.Name, this.GetDefaultValueForType(property.PropertyType)));
							}
						}
						catch (Exception exception)
						{
							result = false;
							exceptions.Add(exception);
						}
					}
				}
			}
			catch (Exception exception)
			{
				result = false;
				exceptions.Add(exception);
			}

			if (exceptions.Any())
			{
				if (throwOnError)
				{
					var message
						= "One or more errors occurred during save:"
						+ Environment.NewLine
						+ Environment.NewLine
						+ string.Join(Environment.NewLine, exceptions.ConvertAll(e => e.Message));

					throw (new AggregateException(message, exceptions));
				}
			}

			return (result);
		}

		private IEnumerable<PropertyInfo> GetProperties ()
		{
			var properties = this
				.GetType()
				.GetProperties(BindingFlags.Instance | BindingFlags.Public)
				.Where(p => ((p.CanWrite) && (p.GetSetMethod(nonPublic: true).IsPublic)))
				.ToList()
				.AsReadOnly();

			return (properties);
		}

		public virtual RegistryKey GetRegistryKey ()
		{
			var key = Registry.CurrentUser;

			key = key.CreateSubKey("Software");
			key = key.CreateSubKey(Global.ApplicationInfo.ManufacturerName);
			key = key.CreateSubKey(Global.ApplicationInfo.CompanyName);
			key = key.CreateSubKey(Global.ApplicationInfo.ProductName);
			key = key.CreateSubKey(Global.ApplicationInfo.ProductVersion.ToString());
			key = key.CreateSubKey(Global.ApplicationInfo.AssemblyName);
			key = key.CreateSubKey(Global.ApplicationInfo.AssemblyVersion.ToString());
			key = key.CreateSubKey("Options");

			return (key);
		}

		public object GetDefaultValueForType (Type targetType)
		{
			return (targetType.IsValueType ? Activator.CreateInstance(targetType) : null);
		}

		public Options Clone ()
		{
			return (new Options().CopyFrom(this));
		}

		public Options CopyFrom (Options source)
		{
			return (ReflectionUtilities.Copy(source, this));
		}

		public Options CopyTo (Options destination)
		{
			return (destination.CopyFrom(this));
		}
	}
}