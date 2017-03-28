namespace cleangap.api.Models.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class customers
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public customers()
        {
            answers = new HashSet<answers>();
            projects = new HashSet<projects>();
        }

        public int id { get; set; }

        [StringLength(100)]
        public string name { get; set; }

        [StringLength(255)]
        public string email { get; set; }

        [StringLength(32)]
        public string password { get; set; }

        [StringLength(64)]
        public string token_signin { get; set; }

        [StringLength(64)]
        public string token_forgot_pass { get; set; }

        public DateTime? token_expire { get; set; }

        public DateTime? creation_date { get; set; }

        public DateTime? edition_date { get; set; }

        public DateTime? confirmation_date { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<answers> answers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<projects> projects { get; set; }
    }
}
