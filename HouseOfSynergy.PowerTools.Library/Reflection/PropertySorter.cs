using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Reflection
{
	public class PropertySorter:
		System.ComponentModel.ExpandableObjectConverter
	{
		public override bool GetPropertiesSupported (System.ComponentModel.ITypeDescriptorContext context)
		{
			return (true);
		}

		/// <summary>
		/// Gets a collection of properties for the type of object specified by the value parameter.
		/// </summary>
		/// <param name="context"> An System.ComponentModel.ITypeDescriptorContext that provides a format context.</param>
		/// <param name="value">An System.Object that specifies the type of object to get the properties for.</param>
		/// <param name="attributes">An array of type System.Attribute that will be used as a filter.</param>
		/// <returns>A System.ComponentModel.PropertyDescriptorCollection with the properties that are exposed for the component, or null if there are no properties.</returns>
		public override System.ComponentModel.PropertyDescriptorCollection GetProperties (System.ComponentModel.ITypeDescriptorContext context, object value, System.Attribute [] attributes)
		{
			System.Attribute attribute = null;
			System.Collections.ArrayList propertyNames = null;
			System.Collections.ArrayList orderedProperties = null;
			System.ComponentModel.PropertyDescriptorCollection properties = null;

			orderedProperties = new System.Collections.ArrayList();
			properties = System.ComponentModel.TypeDescriptor.GetProperties(value, attributes);

			foreach (System.ComponentModel.PropertyDescriptor property in properties)
			{
				attribute = property.Attributes [typeof(PropertyOrderAttribute)];

				if (attribute != null)
				{
					// If the attribute is found, then create an pair object to hold it
					PropertyOrderAttribute poa = (PropertyOrderAttribute) attribute;
					orderedProperties.Add(new PropertyOrderPair(property.Name, poa.Order));
				}
				else
				{
					// If no order attribute is specifed then given it an order of 0
					orderedProperties.Add(new PropertyOrderPair(property.Name, 0));
				}
			}

			// Perform the actual order using the value PropertyOrderPair classes implementation of IComparable to sort.
			orderedProperties.Sort();

			// Build a string list of the ordered names
			propertyNames = new System.Collections.ArrayList();
			foreach (PropertyOrderPair pair in orderedProperties)
			{
				propertyNames.Add(pair.Name);
			}

			// Pass in the ordered list for the PropertyDescriptorCollection to sort by
			return (properties.Sort((string []) propertyNames.ToArray(typeof(string))));
		}
	}
}