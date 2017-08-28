using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PioneerTest.Models
{
    public class EmployeeDetailsModel
    {
        public int EmployeeID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }
        public string Mobile_Number { get; set; }
        public string AlternateMobileNumber { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Current_Country { get; set; }
        public string Home_Country { get; set; }
        public long ZipCode { get; set; }
    }
}
