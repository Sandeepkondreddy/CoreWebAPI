using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Designation
    {
        public int DesignationId { get; set; }
        public String DesignationCode { get; set; }
        public String DesignationName { get; set; }
        public String Remarks { get; set; }
        public Char Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public String CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public String ModifiedBy { get; set; }
    }
}
