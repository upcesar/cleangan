namespace cleangap.api.Models.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class surveys
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public surveys()
        {
            project_follow_up = new HashSet<project_follow_up>();
        }

        public int id { get; set; }
        public int? id_section { get; set; }
        public int? id_customer { get; set; }
        public bool? is_open { get; set; }
        public int? project_status { get; set; }
        public DateTime? creation_date { get; set; }
        public DateTime? edition_date { get; set; }

        public virtual customers customers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<project_follow_up> project_follow_up { get; set; }
        public virtual question_sections question_sections { get; set; }
    }
}
