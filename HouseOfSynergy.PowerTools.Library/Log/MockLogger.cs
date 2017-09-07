using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseOfSynergy.PowerTools.Library.Log
{
    public class MockLogger :
        ILogger
    {
        public void Dispose()
        {
        }

        public void Write(Exception exception)
        {
        }

        public void Write(LogEntry entry)
        {
        }

        public void Write(string message)
        {
        }

        public void Write(string message, Exception exception)
        {
        }
    }
}