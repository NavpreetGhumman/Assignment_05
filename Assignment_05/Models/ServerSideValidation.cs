using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment_05.Models
{
    public class ServerSideValidation
    {
        //Server-Side Validation logic can occur in many places.
        //The Model is a good place to store constraints on data, as it is meant to act as a representation.


        public int teacherid;
        public string teacherfname;
        public string teacherlname;
        public string employeenumber;
        public DateTime hiredate;
        public decimal salary;


        public bool IsValid()
        {
            bool valid = true;

            if (teacherfname == null || teacherlname == null || employeenumber == null)
            {
                //Base validation to check if the fields are entered.
                valid = false;
            }
            else
            {
                //Validation for fields to make sure they meet server constraints
                if (teacherfname.Length < 2 || teacherfname.Length > 255) valid = false;
                if (teacherlname.Length < 2 || teacherlname.Length > 255) valid = false;
                if (salary > 200) valid = false;
            }
            return valid;
        }

        //Parameter-less constructor function
        //Necessary for AJAX requests to automatically bind from the [FromBody] attribute
        //public Teacher() { }
    }
}