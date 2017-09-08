using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using HouseOfSynergy.PowerTools.Library.Security.Cryptography;

namespace HouseOfSynergy.AffinityDms.Library
{
	public static class AuditTrailConstants
	{
        #region Generic
        public const string RenameDescription                                   = "User [{user}], Item: [{item}], Action: [Rename],				 {ItemName} to {ItemNameNew}.";
        public const string AddDescription                                      = "User [{user}], Item: [{item}], Action: [Added],				 {ItemName}.";
        public const string EditDescription                                     = "User [{user}], Item: [{item}], Action: [Edited],				 {ItemName}.";
        public const string ViewDescription                                     = "User [{user}], Item: [{item}], Action: [Viewed],				 {ItemName}.";
        public const string DeleteDescription                                   = "User [{user}], Item: [{item}], Action: [Deleted],			 {ItemName}.";
        public const string MoveDescription                                     = "User [{user}], Item: [{item}], Action: [Moved],				 {ItemName} from {ItemMoveFrom} to {ItemMoveTo}.";
        public const string SearchDescription                                   = "User [{user}], Item: [{item}], Action: [Searched],	{ItemName} for {SearchItemQurey}.";
        public const string ConfirmDescription                                  = "User [{user}], Item: [{item}], Action: [Confirmed],				 {ItemName} to {ItemNameNew}.";


        public const string CheckInDescription			                        = "User [{user}], Item: [{item}], Action: [Checked In],			 {ItemName}.";
		public const string CheckOutDescription			                        = "User [{user}], Item: [{item}], Action: [Checked Out],		 {ItemName}.";
		public const string CancelCheckOutDescription	                        = "User [{user}], Item: [{item}], Action: [Check Out Cancelled], {ItemName}.";
		public const string AddIndexDescription			                        = "User [{user}], Item: [{item}], Action: [Index Added],		 {ItemName}.";
		public const string UpdateVersionDescription	                        = "User [{user}], Item: [{item}], Action: [Version Updated],	 {ItemName} from {ItemOldVersion} to {ItemNewVersion}.";
        public const string RetrieveDescription                                 = "User [{user}], Item: [{item}], Action: [Retrieved],			 {ItemName}.";
        public const string RetrieveVersionDescription                          = "User [{user}], Item: [{item}], Action: [Retrieve Versions],	 {ItemName}.";
        public const string UpdateAccessRightsDescription                       = "User [{user}], Item: [{item}], Action: [Updated Access Rights],	{ItemName}. Rights assigned to: [{AssignedToUser}] ";
        public const string RetrieveAccessRightsDescription                     = "User [{user}], Item: [{item}], Action: [Retrieved Access Rights],	{ItemName}.";
        public const string MarkPrivateDescription                              = "User [{user}], Item: [{item}], Action: [Mark Private],	{ItemName}.";
        public const string MarkPublicDescription                               = "User [{user}], Item: [{item}], Action: [Mark Public],	{ItemName}.";
        public const string EnableManualClassificationDescription               = "User [{user}], Item: [{item}], Action: [Enabled Manual Classification],	{ItemName} for template [{TemplateName}]. ";

        #endregion Generic

        #region Specific
        public const string AddDocumentFragmentsDescription                     = "User [{user}], Item: [{item}], Action: [Added Document Fragments],	 {ItemName}.";
        public const string UpdateIndexDescription                              = "User [{user}], Item: [{item}], Action: [Updated Document Index],	 {ItemName}.";
        public const string RetrieveDocumentByHashDescription                   = "User [{user}], Item: [{item}], Action: [Retrieved By Hash],	{ItemName}.";
        public const string FinalizeDocumentDescription                         = "User [{user}], Item: [{item}], Action: [Finalized Document],	{ItemName}.";
        public const string RetrieveAllRelatedDescription                       = "User [{user}], Item: [{item}], Action: [Retrieved All Related Documents],	{ItemName}.";
        public const string PerformCorrectiveIndexDescription                   = "User [{user}], Item: [{item}], Action: [Performed Corrective Index],	 {ItemName}.";
        public const string UpdateCorrectiveIndexDescription                    = "User [{user}], Item: [{item}], Action: [Updated Corrective Index],	 {ItemName}.";
        //public const string RetrieveCorrectiveIndexDescription                  = "User [{user}], Item: [{item}], Action: [Retrieved Corrective Index],	 {ItemName}.";

        #endregion Specific


    }
}