using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.UI.WebControls;
using Microsoft.Ajax.Utilities;

namespace DMProject.Models
{
    public class Calculation
    {
        public string [] A;
        public string [] B;
        public string[] C;
        int sno = 0;
        public Calculation(string a, string b)
        {
            string[] A = a.Split(',');
            string[] B = b.Split(',');

            this.A = A;
            this.B = B;
        }
        public Calculation(string a, string b,string c)
        {
            string[] A = a.Split(',');
            string[] B = b.Split(',');
            string[] C = c.Split(',');

            this.A = A;
            this.B = B;
            this.C = C;
        }

        public Calculation()
        {
        }


        public List<string> Union(string[] a, string[] b)
        {
            List<string> ans = a.Union(b).Distinct().ToList();
            return ans;
        }

        public List<string> Intersection(string[] a, string[] b)
        {
            List<string> ans = a.Intersect(b).Distinct().ToList();
            return ans;
        }

        public List<string> Difference(string[] a , string[] b)
        {
            var diff = from x in a.Except(b)
                      select x;

            List<string> ans = diff.ToList();
            return ans;

        }

        public List<string> Compliment(string[] U, string[] b)
        {
            var compliment = from x in U.Except(b)
                             select x;
            List<string> ans = compliment.ToList();
            return ans;
           
        }


        

    }
}