//using System;
//using HouseOfSynergy.PowerTools.Library.Interfaces;
//using HouseOfSynergy.PowerTools.Library.Log;
//using Microsoft.Win32;

//namespace HouseOfSynergy.PowerTools.Library.Utility
//{
//	public class OptionsBase:
//		IOptions
//	{
//		public IApplicationInfo ApplicationInfo { get; private set; }

//		public OptionsBase ()
//		{
//		}

//		protected virtual void OnInitialize () { }
//		protected virtual bool OnSave (RegistryKey key) { return (false); }
//		protected virtual bool OnLoad (RegistryKey key) { return (false); }

//		public virtual IOptions Clone ()
//		{
//			var instance = (OptionsBase) Activator.CreateInstance(this.GetType());

//			instance.Initialize(this.ApplicationInfo);

//			return (instance.CopyFrom(this));
//		}

//		public virtual IOptions CopyTo (IOptions destination) { return (destination.CopyFrom(this)); }
//		public virtual IOptions CopyFrom (IOptions source) { return (ReflectionUtilities.Copy((OptionsBase) source, this)); }

//		public void Initialize ()
//		{
//			this.OnInitialize();
//		}

//		public void Initialize (IApplicationInfo applicationInfo)
//		{
//			if (applicationInfo == null) { throw (new ArgumentNullException(nameof(applicationInfo))); }
//			//if (this.ApplicationInfo != null)

//			this.ApplicationInfo = applicationInfo;

//			this.OnInitialize();
//		}

//		public bool Save ()
//		{
//			Exception exception = null;
//			return (this.Save(out exception));
//		}

//		public bool Save (out Exception exception)
//		{
//			bool result = false;
//			RegistryKey key = null;

//			exception = null;

//			try
//			{
//				key = this.GetRegistryKeyApplicationOptions();

//				result = this.OnSave(key);

//				key.Close();
//			}
//			catch (Exception e)
//			{
//				exception = e;
//			}

//			return (result);
//		}

//		public bool Load ()
//		{
//			Exception exception = null;
//			return (this.Load(out exception));
//		}

//		public bool Load (out Exception exception)
//		{
//			bool result = false;
//			RegistryKey key = null;

//			exception = null;

//			try
//			{
//				this.Initialize();

//				key = this.GetRegistryKeyApplicationOptions();

//				result = this.OnLoad(key);

//				key.Close();
//			}
//			catch (Exception e)
//			{
//				exception = e;
//			}

//			return (result);
//		}

//		public virtual RegistryKey GetRegistryKeyApplication ()
//		{
//			var key = Registry.CurrentUser;

//			key = key.CreateSubKey("Software");
//			key = key.CreateSubKey(this.ApplicationInfo.ManufacturerName);
//			key = key.CreateSubKey(this.ApplicationInfo.CompanyName);
//			key = key.CreateSubKey(this.ApplicationInfo.ProductName);
//			key = key.CreateSubKey(this.ApplicationInfo.ProductVersion.ToString());
//			key = key.CreateSubKey(this.ApplicationInfo.AssemblyName);
//			key = key.CreateSubKey(this.ApplicationInfo.AssemblyVersion.ToString());

//			return (key);
//		}

//		public virtual RegistryKey GetRegistryKeyApplicationOptions ()
//		{
//			return (this.GetRegistryKeyApplication().CreateSubKey("Options"));
//		}
//	}
//}