using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HouseOfSynergy.AffinityDms.WebRole.Classes.Common
{
    public class EnumUtilities
    {
        public static IEnumerable<T> GetEnumValues<T>(out Exception exception)
        {
            exception = null;
            IEnumerable<T> EnumValues = null;
            try
            {
                EnumValues = Enum.GetValues(typeof(T)).Cast<T>();
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return EnumValues;
        }
        public static List<SelectListItem> GetSelectItemListFromEnum<T>()
        {
            List<SelectListItem> SelectedList = null;
            SelectedList = Enum.GetValues(typeof(T)).Cast<T>().Select(itemType => new SelectListItem
            {
                Text = itemType.ToString(),
                Value = (Convert.ToInt32(itemType)).ToString()
            }).ToList();
            return SelectedList;
        }
        public static List<SelectListItem> GetSelectItemListFromEnum<T>(out Exception exception)
        {
            exception = null;
            List<SelectListItem> SelectedList = null;
            try
            {
                SelectedList = Enum.GetValues(typeof(T)).Cast<T>().Select(itemType => new SelectListItem
                {
                    Text = itemType.ToString(),
                    Value = (Convert.ToInt32(itemType)).ToString()
                }).ToList();
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            return SelectedList;
        }
    }
}