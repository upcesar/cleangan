namespace cleangap.api.Models.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// Question Entity Model
    /// </summary>
    public partial class questions
    {
        /// <summary>
        /// Question Entity Model Constructor
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public questions()
        {
            question_options = new HashSet<question_options>();
        }
        /// <summary>
        /// Question ID
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// Question Short Name
        /// </summary>
        [StringLength(100)]
        public string name { get; set; }
        /// <summary>
        /// Question Description
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// Foreign Key Id for Sections Entity
        /// </summary>
        public int? id_section { get; set; }
        /// <summary>
        /// Foreign Key Id for SubSections Entity
        /// </summary>
        public int? id_subsection { get; set; }
        /// <summary>
        /// Foreign Key for mapping Parent Question row
        /// </summary>
        public int? parent_question_id { get; set; }
        /// <summary>
        /// Parent answer value for showing the related answers
        /// </summary>
        public string parent_answer_value { get; set; }
        /// <summary>
        /// Set repeated for mapped Question Option
        /// </summary>
        public bool has_repeater { get; set; }
        /// <summary>
        /// Hide question in UI
        /// </summary>
        public bool? hide_question { get; set; }
        /// <summary>
        /// Question Page
        /// </summary>

        public int? page { get; set; }
        /// <summary>
        /// Question Options collection
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<question_options> question_options { get; set; }
        /// <summary>
        /// Parent Question Object
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual questions parent_question { get; set; }

        /// <summary>
        /// Children Question List
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<questions> children_question { get; set; }
        /// <summary>
        /// Question section Object
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual question_sections question_sections { get; set; }
        /// <summary>
        /// Question subsection Object
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual question_sections questions_subsection { get; set; }
    }
}
