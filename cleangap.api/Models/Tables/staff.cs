namespace cleangap.api.Models.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("staff")]
    public partial class staff
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public staff()
        {
            project_follow_up = new HashSet<project_follow_up>();
        }

        public int id { get; set; }

        [StringLength(255)]
        public string fullname { get; set; }

        [StringLength(20)]
        public string phone { get; set; }

        [StringLength(255)]
        public string email { get; set; }

        [StringLength(32)]
        public string password { get; set; }

        public int? id_leader { get; set; }

        public bool? is_leader { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<project_follow_up> project_follow_up { get; set; }
    }
}
