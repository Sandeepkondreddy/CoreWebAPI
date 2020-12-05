using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public String EmployeeNo { get; set; }
        public String EmployeeName { get; set; }
        public String Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public String Email { get; set; }
        public String Mobile { get; set; }
        public String  PhotoFileName { get; set; }
        public DateTime DateofJoining { get; set; }
        public String Designation { get; set; }
        public String Department { get; set; }
        public String Company { get; set; }
        public String Branch { get; set; }
        public String Remarks { get; set; }
        public Char Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public String CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public String ModifiedBy { get; set; }
    }
}
