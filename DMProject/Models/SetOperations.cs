using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMProject.Models
{
    public class SetOperations
    {
        public int Sno { get; set; }
        public string A { get; set; }
        public string B { get; set; }
        public string C { get; set; }
        public int Type { get; set; }
        public string Universal { get; set; }
        public string Result { get; set; }
        public string Operation { get; set; }
        public string Symbol { get; set; }
        public string Expression { get; set; }

    }

    public static class Opertaions
    {
        public static String Union = "∪";
        public static String Intersection = "∩";
        public static String Compliment = "c";
        public static String Difference = "-";
    }

    public class BuildVennDigram
    {
        public List<string> A { get; set; }
        public List<string> B { get; set; }
        public List<string> C { get; set; }
        public List<string> U { get; set; }
        public List<string> Intersection1 { get; set; }
        public List<string> Intersection2 { get; set; }
        public List<string> Intersection3 { get; set; }
        public List<string> Intersection4 { get; set; }
        public List<string> Diff1 { get; set; }
        public List<string> Diff2 { get; set; }
        public List<string> Diff3 { get; set; }
        public List<string> BreakPoints { get; set; }
        public List<string> Result { get; set; }
        public string Type { get; set; }
    }
}