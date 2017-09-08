using Microsoft.WindowsAzure.ServiceRuntime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseOfSynergy.AffinityDms.Library
{
    public class RoleDiagnostics
    {
        // Trace sources - Think of these as multiple output channels. So write to the WorkerTrace when you're logging information about
        // the worker role activities themselves (e.g. OnStart, Run, etc.).  You can add more sources if you want - they
        // give you more ability to control what is and isn't logged.  For instance, rather than a single WorkerTrace,
        // you might have a source per major component of your app - database, search, etc.
        public TraceSource WorkerTrace;
        public TraceSource WebTrace;
        // Add additional sources here

        // Corresponding trace switches to control level of output for each source.  Note that having switches
        // 1:1 with TraceSource's is probably overkill since there is an internal switch in each source, so if
        // you're going to have one switch per source you may as well just use the TraceSource.Switch itself. 
        // (Code in the ctor for this class hooks up the switches below to the corresponding TraceSources so
        // that the internal switch isn't used by the source.)
        // Switches become useful when you have multiple source's sharing a single switch so you can control
        // the output from multiple sources with one change in a switch value.
        public SourceSwitch WebTraceSwitch { get; set; }
        public SourceSwitch WorkerTraceSwitch { get; set; }
        public SourceLevels SourceLevelFromString(string str)
        {
            SourceLevels lvl;
            try
            {
                lvl = (SourceLevels)Enum.Parse(typeof(SourceLevels), str, true);
            }
            catch (System.ArgumentException)
            {
                lvl = SourceLevels.Off;
            }

            return lvl;
        }

        public enum TraceEventID
        {
            traceGeneral = 0,           
            traceFunctionEntry = 1,
            traceFunctionExit = 2,      
            traceException = 100,
            traceUnexpected = 200,
            traceFlow = 300             
        };
        public enum Role
        {
           Web,
           Worker
        };
        [SwitchAttribute("WebSourceSwitch", typeof(SourceSwitch))]
        private void Webrole()
        {
            WebTraceSwitch = new SourceSwitch("WebSourceSwitch", "All");
            WebTrace = new TraceSource("WebTrace");
            int idxConsole = WebTrace.Listeners.Add(new TextWriterTraceListener());
            WebTrace.Listeners[idxConsole].Name = "eventlog";
            //WebTrace.Listeners["eventlog"].TraceOutputOptions |= TraceOptions.LogicalOperationStack;
            //WebTrace.Listeners["eventlog"].TraceOutputOptions |= TraceOptions.DateTime;
            WebTrace.Listeners["eventlog"].TraceOutputOptions = TraceOptions.Timestamp;
            WebTrace.Switch = WebTraceSwitch;
        }


        [SwitchAttribute("WorkerSourceSwitch", typeof(SourceSwitch))]
        private void Workerrole()
        {
            WorkerTraceSwitch = new SourceSwitch("WorkerSourceSwitch", "All");
            WorkerTrace = new TraceSource("WorkerTrace");
            
            int idxConsole = WorkerTrace.Listeners.Add(new TextWriterTraceListener());
            WorkerTrace.Listeners[idxConsole].Name = "eventlog";
            //WorkerTrace.Listeners[idxConsole].TraceOutputOptions |= TraceOptions.LogicalOperationStack;
            //WorkerTrace.Listeners[idxConsole].TraceOutputOptions |= TraceOptions.DateTime;
            WorkerTrace.Listeners["eventlog"].TraceOutputOptions = TraceOptions.Timestamp;
            WorkerTrace.Switch = WorkerTraceSwitch;
        }

        public RoleDiagnostics(Role roleDiagnosticsRole)
        {
           
            switch (roleDiagnosticsRole)
            {
                case Role.Web:
                    Webrole();
                    break;
                case Role.Worker:
                    Workerrole();
                    break;
                default:
                    break;
            }
            GetTraceSwitchValuesFromRoleConfiguration(roleDiagnosticsRole);
        }

        public void GetTraceSwitchValuesFromRoleConfiguration(Role role)
        {
            if ((WorkerTrace != null && WorkerTraceSwitch!=null) || (WebTrace != null && WebTraceSwitch != null))    
            {
                switch (role)
                {
                    case Role.Web:
                        WebTraceSwitch.Level = SourceLevelFromString(RoleEnvironment.GetConfigurationSettingValue("WebTrace"));
                        Trace.WriteLine("GetTraceSwitchValuesFromRoleConfiguration - Trace switch values set: " + WebTraceSwitch.Level.ToString() +
                        " Source Switches: " + WebTrace.Switch.Level.ToString());
                        break;
                    case Role.Worker:
                        WorkerTraceSwitch.Level = SourceLevelFromString(RoleEnvironment.GetConfigurationSettingValue("WorkerTrace"));
                        Trace.WriteLine("GetTraceSwitchValuesFromRoleConfiguration - Trace switch values set: " + WorkerTraceSwitch.Level.ToString() +
                        " Source Switches: " + WorkerTrace.Switch.Level.ToString());
                        break;
                    default:
                        break;
                }
            }
        }


        public void WriteDiagnosticInfo(TraceSource src, TraceEventType evtType, TraceEventID evtID, string msg)
        {
            if (src != null)
            {
                src.TraceEvent(evtType, (int)evtID, msg);
                src.Flush();
            }
        }

        public void WriteDiagnosticInfo(TraceSource src, TraceEventType evtType, TraceEventID evtID, string format, params object[] args)
        {
            if (src != null)
            {
                src.TraceEvent(evtType, (int)evtID, format, args);
                src.Flush();
            }
        }
    }
}