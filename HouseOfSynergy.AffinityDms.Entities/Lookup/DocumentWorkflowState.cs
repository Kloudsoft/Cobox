using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseOfSynergy.AffinityDms.Entities.Lookup
{
    /// <summary>
    /// States of the Workflow.
    /// </summary>
    public enum DocumentWorkflowState
    {
        /// <summary>
        /// Workflow is not yet initiated.
        /// </summary>
        None, //0
        /// <summary>
        /// Workflow is initiated but waiting for indexer to index the document.
        /// </summary>
        WaitingToBeIndexed, //1
        /// <summary>
        /// Workflow is verified.
        /// </summary>
        Verified, //2   


        /// <summary>
        /// A document that is queued and have a count less than 4 was found and ready for pcr processing.
        /// In this case, Document will be sent for Ocr Processing.
        /// </summary>
        Draft, //3

        /// <summary>
        /// A document that is queued and have a count less than 4 was found and ready for pcr processing.
        /// In this case, Document will be sent for Ocr Processing.
        /// </summary>
        Submitted, //4

        /// <summary>
        /// A document that is queued and have a count less than 4 was found and ready for pcr processing.
        /// In this case, Document will be sent for Ocr Processing.
        /// </summary>
        Approved, //5

        /// <summary>
        /// A document that is queued and have a count less than 4 was found and ready for pcr processing.
        /// In this case, Document will be sent for Ocr Processing.
        /// </summary>
        Rework, //6


        /// <summary>
        /// A document that is queued and have a count less than 4 was found and ready for pcr processing.
        /// In this case, Document will be sent for Ocr Processing.
        /// </summary>
        Closed, //7   Completed

        Recommend, //8
        
        Advised, ///9

        ProcessPayment, // 10

        ReworkSM,  // 11

        ReworkPM,  // 12

        ReworkSSOAD,  // 13

    }
}
