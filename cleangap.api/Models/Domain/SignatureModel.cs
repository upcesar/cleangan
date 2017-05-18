using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cleangap.api.Models.Domain
{
    public class SignatureModel
    {
        public string FullName { get; set; }
        public DateTime SignDate { get; set; }
        public string DigitalSingature { get; set; }
    }
}