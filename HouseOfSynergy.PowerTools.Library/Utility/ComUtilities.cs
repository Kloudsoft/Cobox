using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace HouseOfSynergy.PowerTools.Library.Utility
{
	public static class ComUtilities
	{
		public static void ReleaseComObject (object o)
		{
			try
			{
				if (o != null)
				{
					if (Marshal.IsComObject(o))
					{
						Marshal.ReleaseComObject(o);
					}
				}
			}
			finally
			{
				o = null;
			}
		}

		public static void ReleaseComObject (ref object o)
		{
			try
			{
				if (o != null)
				{
					if (Marshal.IsComObject(o))
					{
						Marshal.ReleaseComObject(o);
					}
				}
			}
			finally
			{
				o = null;
			}
		}

		public static void ReleaseComObject (IEnumerable<object> objects)
		{
			try
			{
				if (objects != null)
				{
					var list = objects.ToList();

					for (int i = 0; i < list.Count; i++)
					{
						try
						{
							if (list [i] != null)
							{
								if (Marshal.IsComObject(list [i]))
								{
									Marshal.ReleaseComObject(list [i]);
								}
							}
						}
						finally
						{
						}
					}
				}
			}
			finally
			{
			}
		}

		public static void ReleaseComObject (params object [] objects)
		{
			try
			{
				if (objects != null)
				{
					for (int i = 0; i < objects.Length; i++)
					{
						try
						{
							if (objects [i] != null)
							{
								if (Marshal.IsComObject(objects [i]))
								{
									Marshal.ReleaseComObject(objects [i]);
								}
							}
						}
						finally
						{
						}
					}
				}
			}
			finally
			{
			}
		}
	}
}