namespace cleangap.api.Models.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class question_options
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public question_options()
        {
            answers = new HashSet<answers>();
        }
        public int id { get; set; }
        [StringLength(150)]
        public string option_text { get; set; }
        public int? order { get; set; }
        [StringLength(10)]
        public string input_type { get; set; }
        [StringLength(255)]
        public string values_list { get; set; }
        public int? id_question { get; set; }
        public bool? hide_option { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<answers> answers { get; set; }
        public virtual questions questions { get; set; }
    }
}
