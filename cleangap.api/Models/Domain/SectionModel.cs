using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cleangap.api.Models.Domain
{
    /// <summary>
    /// Section's Model
    /// </summary>
    public class SectionModel
    {
        /// <summary>
        /// Section ID
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// Section Name
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Question's list related with the section / subsection
        /// </summary>
        public List<QuestionsModel> questions { get; set; }

    }
}