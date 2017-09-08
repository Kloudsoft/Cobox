using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseOfSynergy.AffinityDms.Library
{
    public class ExceptionUtilities
    {
        public static string ExceptionToString(Exception exception)
        {
            string exceptionStr = "";
            //if (ex != null)
            //{
            //    if (ex.InnerException != null)
            //    {
            //        Exception exception = ex.InnerException;
            //        while ((exception.InnerException) != null)
            //        {
            //            exception = exception.InnerException;
            //        }
            //        if (string.IsNullOrEmpty(exception.Message) || string.IsNullOrWhiteSpace(ex.Message))
            //        {
            //            exceptionStr = exception.Message;
            //        }
            //    }
            //    else
            //    {
            //        if (string.IsNullOrEmpty(ex.Message) || string.IsNullOrWhiteSpace(ex.Message))
            //        {
            //            exceptionStr = ex.Message;
            //        }
            //    }
            //}



            if (exception != null)
            {
                Exception currentException = exception;
                Exception previousException = null;
                while (currentException.InnerException != null)
                {
                    currentException = currentException.InnerException;
                    previousException = currentException;
                }
                if ((!string.IsNullOrEmpty(exception.Message)) || (!string.IsNullOrWhiteSpace(exception.Message)))
                {
                    exceptionStr = exception.Message;
                }
            }
            return exceptionStr;
        }
    }
}
