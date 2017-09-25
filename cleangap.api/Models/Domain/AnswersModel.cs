using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cleangap.api.Models.Domain
{
    /// <summary>
    /// Answer input parameters 
    /// </summary>
    public class AnswersModel
    {
        /// <summary>
        /// Question Options ID
        /// </summary>
        public int QuestionOptionId { get; set; }
        /// <summary>
        /// Answer's Unique value.
        /// </summary>
        public string UniqueValue { get; set; }
        /// <summary>
        /// Answer's multiple value list
        /// </summary>
        public List<string> MultipleValues { get; set; }
        /// <summary>
        /// Choose whether use Unique or Multiple values. These are mutually exclusive (XOR).
        /// </summary>
        public bool HasMultipleValue { get; set; }
        /// <summary>
        /// For repeater option, index should be stored. In case of NULL value, the answers doesn't map to any repeater option
        /// </summary>
        public int? IndexRepeater { get; set; }
        /// <summary>
        /// Answer's model Inicialization
        /// </summary>
        public AnswersModel()
        {
            HasMultipleValue = false;
            MultipleValues = new List<string>();
            IndexRepeater = null;
        }
    }
}