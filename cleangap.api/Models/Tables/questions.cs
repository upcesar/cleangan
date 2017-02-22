namespace cleangap.api.Models.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class questions
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public questions()
        {
            question_options = new HashSet<question_options>();
        }

        public int id { get; set; }

        [StringLength(100)]
        public string name { get; set; }

        public string description { get; set; }

        public int? id_section { get; set; }

        public int? dependent_question_id { get; set; }

        public int? dependent_answer_id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<question_options> question_options { get; set; }

        public virtual question_sections question_sections { get; set; }
    }
}
