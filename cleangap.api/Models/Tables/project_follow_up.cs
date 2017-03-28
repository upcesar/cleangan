namespace cleangap.api.Models.Tables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class project_follow_up
    {
        public int id { get; set; }

        public int? id_project { get; set; }

        public int? id_staff { get; set; }

        public string staff_comments { get; set; }

        public string customer_comments { get; set; }

        [StringLength(255)]
        public string url_attachment { get; set; }

        public DateTime? staff_inquiry_date { get; set; }

        public DateTime? cust_response_date { get; set; }

        public virtual projects project { get; set; }

        public virtual staff staff { get; set; }
    }
}
