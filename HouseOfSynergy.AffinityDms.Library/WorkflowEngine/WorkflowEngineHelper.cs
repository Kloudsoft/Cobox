using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OptimaJet.Workflow;
using OptimaJet.Workflow.Core.Runtime;
using OptimaJet.Workflow.Core.Builder;
using System.Xml.Linq;
using OptimaJet.Workflow.Core.Bus;
using System.Threading;
using System.Data.SqlClient;
using System.Data;

namespace HouseOfSynergy.AffinityDms.Library.Workflow
{
    public class WorkflowEngineHelper
    {
        public string schemeCode { get; set; }
        public Guid? processId { get; set; }
        private string connectionString { get; set; }
        private static volatile WorkflowRuntime _runtime;
        private static readonly object _sync = new object();
        public WorkflowEngineHelper(string connectionString,string schemeCode,Guid? processId) {
            this.connectionString = connectionString;
            this.schemeCode = schemeCode;
            this.processId = processId;

        }
        public WorkflowRuntime getRuntime
        {
            get
            {
                if (_runtime == null)
                {
                    lock (_sync)
                    {
                        if (_runtime == null)
                        {
                            var builder = new WorkflowBuilder<XElement>(
                                 new OptimaJet.Workflow.DbPersistence.MSSQLProvider(connectionString),//new OptimaJet.Workflow.DbPersistence.DbXmlWorkflowGenerator(connectionString),
                                new OptimaJet.Workflow.Core.Parser.XmlWorkflowParser(),
                                new OptimaJet.Workflow.DbPersistence.MSSQLProvider(connectionString)//new OptimaJet.Workflow.DbPersistence.DbSchemePersistenceProvider(connectionString)
                                ).WithDefaultCache();
                            _runtime = new WorkflowRuntime(new Guid("{8D38DB8F-F3D5-4F26-A989-4FDD40F32D9D}"))
                                .WithBuilder(builder)
                                .WithPersistenceProvider(new OptimaJet.Workflow.DbPersistence.MSSQLProvider(connectionString))//new OptimaJet.Workflow.DbPersistence.DbPersistenceProvider(connectionString))
                                .WithTimerManager(new TimerManager())
                                .WithBus(new NullBus())
                                .SwitchAutoUpdateSchemeBeforeGetAvailableCommandsOn()
                                .Start();
                        }
                    }
                }
                return _runtime;
            }
        }
        public bool CreateInstance(out Exception exception)
        {
            bool result = false;
            exception = null;
            if (processId == null)
            {
                processId = GenerateUniqueProccessId(connectionString, out exception);
            }
            else if (this.getRuntime.IsProcessExists((Guid)processId))
            {
                processId = GenerateUniqueProccessId(connectionString, out exception);
            }
            try
            {
                this.getRuntime.CreateInstance(schemeCode, processId.Value);
                result = true;
            }
            catch (Exception ex)
            {
                processId = null;
                exception = ex;
            }
            return result;
        }
        private bool DeleteProcess(out Exception exception)
        {
            bool result = false;
            exception = null;
            try
            {
                if (processId == null)
                {
                    throw (new Exception("The process isn't created. Please, create process instance."));
                }
                getRuntime.DeleteInstance(processId.Value);
                processId = null;
                result = true;
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        public bool GetAvailableStateNames(out List<string>stateNames, out Exception exception)
        {
            exception = null;
            stateNames = new List<string>();
            bool result = false;
            try
            {
                if (processId == null)
                {
                    throw (new Exception("The process isn't created. Please, create process instance."));
                }

                var states = getRuntime.GetAvailableStateToSet(processId.Value, Thread.CurrentThread.CurrentCulture);

                foreach (var state in states)
                {
                    stateNames.Add(state.Name);
                }
                result = true;
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        public bool GetAvailableStates(out List<WorkflowState> states, out Exception exception)
        {
            exception = null;
            states = new List<WorkflowState>();
            bool result = false;
            try
            {
                if (processId == null)
                {
                    throw (new Exception("The process isn't created. Please, create process instance."));
                }

                var stateslist = getRuntime.GetAvailableStateToSet(processId.Value, Thread.CurrentThread.CurrentCulture);

                foreach (var state in stateslist)
                {
                    states.Add(state);
                }
                result = true;
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        public bool GetAvailableCommands(out List<WorkflowCommand>commands,out Exception exception)
        {
            exception = null;
            bool result = false;
            commands = new List<WorkflowCommand>();
            try
            {
                if (processId == null)
                {
                    throw (new Exception("The process isn't created. Please, create process instance."));
                }

                var commandslist = getRuntime.GetAvailableCommands(processId.Value, string.Empty);
                foreach (var command in commandslist)
                {
                    commands.Add(command);
                }
                result = true;
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        public static bool GetAvailableSchemeNames(string connectionString, out List<string> schemes, out Exception exception)
        {
            exception = null;
            schemes = new List<string>();
            bool result = false;
            try
            {
                using (var sqlcon = new SqlConnection(connectionString))
                {
                    using (var sqlcomm = new SqlCommand("select Code from WorkflowScheme",sqlcon))
                    {
                        sqlcon.Open();
                        using (SqlDataReader sdr = sqlcomm.ExecuteReader())
                        {
                            
                            while (sdr.Read())
                            {
                                if (!sdr.HasRows)
                                {
                                    break;
                                }
                                schemes.Add(sdr["Code"].ToString());
                            }
                        }
                        if (sqlcon.State == ConnectionState.Open)
                        {
                            sqlcon.Close();
                        }
                    }
                }
                result = true;
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        public static bool GetAvailableSchemes(string connectionString, out DataTable schemes, out Exception exception)
        {
            exception = null;
            schemes = new DataTable();
            bool result = false;
            try
            {
                using (var sqlcon = new SqlConnection(connectionString))
                {
                    using (var sqlcomm = new SqlCommand("select * from WorkflowScheme", sqlcon))
                    {
                        sqlcon.Open();
                        using (SqlDataAdapter sda = new SqlDataAdapter(sqlcomm))
                        {
                            sda.Fill(schemes);
                        }
                        if (sqlcon.State == ConnectionState.Open)
                        {
                            sqlcon.Close();
                        }
                    }
                }
                result = true;
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        public static Guid? GenerateUniqueProccessId(string connectionString,out Exception exception)
        {
            exception = null;
            Guid processId = Guid.Empty;
            bool result = true;
            try
            {
                do
                {
                    processId = Guid.NewGuid();
                    using (var sqlcon = new SqlConnection(connectionString))
                    {
                        using (var sqlcomm = new SqlCommand("select Id from WorkflowProcessInstanceStatus where id = '"+ processId + "'", sqlcon))
                        {
                            sqlcon.Open();
                            using (SqlDataReader sdr = sqlcomm.ExecuteReader())
                            {
                                if (!sdr.HasRows)
                                {
                                    result=false;
                                }
                            }
                            if (sqlcon.State == ConnectionState.Open)
                            {
                                sqlcon.Close();
                            }
                        }
                    }
                } while (result);
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return processId;
        }
        public bool ExecuteCommand(WorkflowCommand command, out Exception exception)
        {
            exception = null;
            bool result = false;
            try
            {
                if (processId == null)
                {
                    throw (new Exception("The process isn't created. Please, create process instance."));
                }
                if (command == null) {
                    throw (new Exception("No Commands Found"));
                }
                List<WorkflowCommand> commandslist = null;

                result = GetAvailableCommands(out commandslist, out exception);
                if (exception != null) { throw exception; }
                if(result)
                {
                    if (commandslist.Count > 0)
                    {
                        var foundcommand = commandslist.Where(c => c.CommandName.Trim().ToLower() == command.CommandName).FirstOrDefault();
                        getRuntime.ExecuteCommand(foundcommand, string.Empty, string.Empty);
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
        public bool SetState(WorkflowState state, out Exception exception)
        {
            exception = null;
            bool result = false;
            try
            {
                if (processId == null)
                {
                    throw (new Exception("The process isn't created. Please, create process instance."));
                }
                if (state == null)
                {
                    throw (new Exception("Unable to find and state"));
                }
                string stateName = string.Empty;
                List<WorkflowState> stateslist = null;
                result = GetAvailableStates(out stateslist, out exception);
                if (exception != null) { throw exception; }
                var foundstate = stateslist.Where(c => c.Name.Trim().ToLower() == stateName).FirstOrDefault();
                if (foundstate != null)
                {
                    getRuntime.SetState(processId.Value, string.Empty, string.Empty, state.Name, new Dictionary<string, object>());
                    result = true;
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return result;
        }
    }
}
  
