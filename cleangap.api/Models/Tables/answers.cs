namespace cleangap.api.Models.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class answers
    {
        public int id { get; set; }
        [StringLength(250)]
        public string answers_value { get; set; }
        public int? id_question_option { get; set; }
        public int? id_customer { get; set; }
        public int? id_project { get; set; }
        public virtual question_options question_options { get; set; }
        public virtual customers customers { get; set; }        
        public virtual projects projects { get; set; }
    }
}
