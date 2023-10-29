using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using DMProject.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;

namespace DMProject.Controllers
{
    public class HomeController : Controller
    {
        

        public JsonResult Calculate(string a,string b)
        {
            List<SetOperations> sets = new List<SetOperations>();
            Calculation cal = new Calculation(a,b);
            int sno = 0;

            var union = cal.Union(cal.A, cal.B);
            

            sets.Add(new SetOperations
            {
                Sno = ++sno,
                A = "{" + string.Join(",", cal.A) + "}",
                B = "{" + string.Join(",", cal.B) + "}",
                Operation = "A" + Opertaions.Union + "B",
                Symbol = Opertaions.Union,
                Result = "{" + string.Join(",", union) + "}",
                Type = 1
            });

            sets.Add(new SetOperations
            {
                Sno = ++sno,
                A = "{" + string.Join(",", cal.A) + "}",
                B = "{" + string.Join(",", cal.B) + "}",
                Operation = "A" + Opertaions.Intersection + "B",
                Symbol = Opertaions.Intersection,
                Result = "{" + string.Join(",", cal.Intersection(cal.A, cal.B)) + "}",
                Type = 1
            });

            sets.Add(new SetOperations
            {
                Sno = ++sno,
                A = "{" + string.Join(",", cal.A) + "}",
                B = "{" + string.Join(",", cal.B) + "}",
                Operation = "A" + Opertaions.Difference + "B",
                Symbol = Opertaions.Difference,
                Result = "{" + string.Join(",", cal.Difference(cal.A, cal.B)) + "}",
                Type = 1
            });

            sets.Add(new SetOperations
            {
                Sno = ++sno,
                A = "{" + string.Join(",", cal.A) + "}",
                B = "{" + string.Join(",", cal.B) + "}",
                Operation = "B" + Opertaions.Difference + "A",
                Symbol = Opertaions.Difference,
                Result = "{" + string.Join(",", cal.Difference(cal.B, cal.A)) + "}",
                Type = 1
            });

            string[] univeral = string.Join(",", union).Split(',');

            sets.Add(new SetOperations
            {
                Sno = ++sno,
                Universal = "{" + string.Join(",",union) + "}",
                A = "{" + string.Join(",", cal.A) + "}",
                Operation = Opertaions.Compliment,
                Symbol = Opertaions.Compliment,
                Result = "{" + string.Join(",", cal.Difference(univeral, cal.A)) + "}",
                Type = 1
            });

            sets.Add(new SetOperations
            {
                Sno = ++sno,
                Universal = "{" + string.Join(",", union) + "}",
                B = "{" + string.Join(",", cal.B) + "}",
                Operation = Opertaions.Compliment,
                Symbol = Opertaions.Compliment,
                Result = "{" + string.Join(",", cal.Difference(univeral, cal.B)) + "}",
                Type = 1
            });

            var data = sets;
            Session["data"] = sets;

            return Json(data,JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetData()
        {
            Calculation cal = new Calculation();
            BuildVennDigram venn = new BuildVennDigram();

            int sno = (int)Session["sno"];
            SetOperations obj = new SetOperations();
            List<SetOperations> list = (List<SetOperations>)Session["data"];
            obj = list.Where(m => m.Sno == sno).FirstOrDefault();


            string[] a = { };
            string[] b = { };
            string[] U = { };
            string[] result = { };
            if (!string.IsNullOrEmpty(obj.A))
            {
                a = obj.A.Replace("{", string.Empty).Replace("}", string.Empty).Split(',');
            }
            if (!string.IsNullOrEmpty(obj.B))
            {
                b = obj.B.Replace("{", string.Empty).Replace("}", string.Empty).Split(',');
            }
            if (!string.IsNullOrEmpty(obj.Universal))
            {
                U = obj.Universal.Replace("{", string.Empty).Replace("}", string.Empty).Split(',');
                if (!string.IsNullOrEmpty(obj.A))
                {
                    venn.A = cal.Intersection(U, a).ToList();
                    venn.U = cal.Difference(U, a).ToList();

                }
                else
                {
                    venn.B = cal.Intersection(U, b).ToList();
                    venn.U = cal.Difference(U, b).ToList();
                }
                venn.Type = obj.Symbol;
                var db = venn;
                return Json(db, JsonRequestBehavior.AllowGet);
            }

            
            venn.A = a.ToList();
            venn.B = b.ToList();
            result = obj.Result.Replace("{", string.Empty).Replace("}", string.Empty).Split(',');
            venn.Result = result.ToList();
            List<string> intersection = cal.Intersection(a, b);
            venn.Intersection1 = intersection;
            venn.Type = obj.Symbol;




            var data = venn;

            return Json(data,JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult VennDiagram(int sno)
        {
            Session["sno"] = sno;
            
            return View();
        }



        public ActionResult Index()
        {
            return View();
        }


        public JsonResult PowerSubset(string powerSet)
        {
            List<string> subsets = new List<string>();
            if (!string.IsNullOrEmpty(powerSet))
            {
                List<string> s = powerSet.Split(',').ToList();
                subsets.Add("  { }  ");
                for (int i = 0; i < s.Count(); i++)
                {
                    subsets.Add("  {" + s[i] + "}  ");
                    for (int k = i + 1; k < s.Count(); k++)
                    {
                        subsets.Add("  {" + s[i] + "," + s[k] + "}  ");

                        if (s.Count() == 4)
                        {
                            for (int j = k + 1; j < s.Count(); j++)
                            {
                                subsets.Add("  {" + s[i] + "," + s[k] + "," + s[j] + "}  ");
                            }
                        }
                        
                    }
                }
                if (s.Count() != 2 && s.Count() != 1)
                {
                    subsets.Add("  {" + string.Join(",", s).ToString() + "}  ");
                }
                
            }

            var data = string.Join(",", subsets).ToString();
            return Json(data,JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSubsets()
        {
            return View();
        }

        public ActionResult TwoSets()
        {
            return View();
        }



        public ActionResult DynamicVennDiagram()
        {

            return View();
        }

        public JsonResult DynamicTable()
        {

            var data = (BuildVennDigram)Session["dymanicSets"];

            return Json(data,JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs (HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult ThreeSets(SetOperations set)
        {
            string exp = "Au(BnC)";
            string exp1 = "(AuB)nC";
            string exp2 = "(AuB)n(AnC)u(AuC)";

            if (set.A !=null && set.B != null && set.C != null)
            {
                Calculation cal = new Calculation();
                BuildVennDigram venn = new BuildVennDigram();


                if (!string.IsNullOrEmpty(set.A) && !string.IsNullOrEmpty(set.B) && !string.IsNullOrEmpty(set.C))
                {
                    cal = new Calculation(set.A, set.B, set.C);
                    venn = EvaluteExpression(set.Expression, cal);
                }
                else if (!string.IsNullOrEmpty(set.A) && !string.IsNullOrEmpty(set.B))
                {
                    cal = new Calculation(set.A, set.B);
                    venn = EvaluteExpression(set.Expression, cal);
                }

                venn.A = set.A.Split(',').ToList();
                venn.B = set.B.Split(',').ToList();
                venn.C = set.C.Split(',').ToList();


                string[] anb = string.Join(",", cal.Intersection(cal.A, cal.B)).Split(',');
                string[] anc = string.Join(",", cal.Intersection(cal.A, cal.C)).Split(',');
                string[] bnc = string.Join(",", cal.Intersection(cal.B, cal.C)).Split(',');
                string[] anbnc = string.Join(",", cal.Intersection(anb, cal.C)).Split(',');

                //(AnB)nC
                venn.Intersection3 = anbnc.ToList();

                //AnB
                venn.Intersection1 = cal.Difference(anb,anbnc);
                //AnC
                venn.Intersection2 = cal.Difference(anc, anbnc);
                //BnC
                venn.Intersection4 = cal.Difference(bnc, anb);

                //Diff1 = (A-B)-C
                string[] a_b = string.Join(",",cal.Difference(cal.A,cal.B)).Split(',');
                venn.Diff1  = cal.Difference(a_b, cal.C).ToList();

                //Diff2 = (B-A)-C
                string[] b_a = string.Join(",", cal.Difference(cal.B, cal.A)).Split(',');
                venn.Diff2 = cal.Difference(b_a, cal.C).ToList();

                //Diff3 = (C-A)-B
                string[] c_a = string.Join(",", cal.Difference(cal.C, cal.A)).Split(',');
                venn.Diff3 = cal.Difference(c_a, cal.B).ToList();

                Session["dymanicSets"] = venn;
            }

         

            return View();
        }

        public BuildVennDigram EvaluteExpression(string exp,Calculation cal)
        {
            var openBracket = exp.Select((value, index) => new { value, index })
           .Where(a => "(".Any(s => string.Equals(a.value, s)))
           .Select(a => a.index).ToList();

            var closeBracket = exp.Select((value, index) => new { value, index })
            .Where(a => ")".Any(s => string.Equals(a.value, s)))
            .Select(a => a.index).ToList();

            List<string> result = new List<string>();
            List<string> breakPoints = new List<string>();
            int count = 0;
            string newExp = exp;
            int isfound = 0;
            SetOperations set = new SetOperations();
            if (openBracket.Count() == 0)
            {
                List<string[]> arr = new List<string[]>();
                string[] strArr1 = { };
                string[] strArr2 = { };
                string opr = "";
                for (int j = 0; j < newExp.Length; j++) //0u1u2
                {
                    if (newExp[j].ToString().Equals("n") || newExp[j].ToString().Equals("u") || newExp[j].ToString().Equals("-"))
                    {
                        opr = newExp[j].ToString();
                    }
                    else
                    {
                        if (newExp[j].ToString().Equals("A"))
                        {
                            strArr1 = cal.A;
                            arr.Add(strArr1);
                        }
                        else if (newExp[j].ToString().Equals("B"))
                        {
                            strArr1 = cal.B;
                            arr.Add(strArr1);
                        }
                        else if (newExp[j].ToString().Equals("C"))
                        {
                            strArr1 = cal.C;
                            arr.Add(strArr1);
                        }
                        else
                        {
                            int e = Convert.ToInt32(newExp[j].ToString());
                            strArr1 = result[e].Split(',');
                            arr.Add(strArr1);
                        }
                    }
                    if (j > 1)
                    {
                        strArr2 = arr[arr.Count() - 1];

                        if (string.IsNullOrEmpty(set.Result))
                        {
                            strArr1 = arr[arr.Count() - 2];
                            if (opr.Equals("n"))
                            {
                                set.Result = string.Join(",", cal.Intersection(strArr1, strArr2));
                            }
                            else if (opr.Equals("u"))
                            {
                                set.Result = string.Join(",", cal.Union(strArr1, strArr2));
                            }
                            else if (opr.Equals("-"))
                            {
                                set.Result = string.Join(",", cal.Difference(strArr1, strArr2));
                            }
                        }
                        else
                        {
                            strArr1 = set.Result.Split(',');

                            if (opr.Equals("n"))
                            {
                                set.Result = string.Join(",", cal.Intersection(strArr1, strArr2));
                            }
                            else if (opr.Equals("u"))
                            {
                                set.Result = string.Join(",", cal.Union(strArr1, strArr2));
                            }
                            else if (opr.Equals("-"))
                            {
                                set.Result = string.Join(",", cal.Difference(strArr1, strArr2));
                            }
                        }
                    }


                }

                breakPoints.Add(exp);
                result.Add(set.Result);
            }

            ///////////

            for (int i = openBracket.Count() - 1; i >= 0; i--)
            {
                int lastIndexOB = openBracket[i];
                int lastIndexCB = closeBracket[i];

                string Calstr = "";
                string str = "";
                if (isfound > 2)
                {

                }
                for (int k = lastIndexOB; k <= lastIndexCB; k++)
                {
                    if (exp[k].ToString().Equals(")"))
                    {
                        isfound ++;
                    }
                    if (isfound < 2)
                    {
                        //str += newExp[k];
                        str += exp[k];
                    }
                }
                isfound = 0;

                Calstr = str.Replace("(", string.Empty).Replace(")", string.Empty);
                breakPoints.Add(str);
                string ans = SolveExpression(Calstr,cal); //AnB
                result.Add(ans);    



                newExp = newExp.Replace(str, count.ToString());
                count++;
                if (i == 0) //0nC || 0n1
                {
                    List<string[]> arr = new List<string[]>();
                    string[] strArr1 = { };
                    string[] strArr2 = { };
                    string opr = "";
                    for (int j = 0; j < newExp.Length; j++) //0u1u2
                    {
                        if (newExp[j].ToString().Equals("n") || newExp[j].ToString().Equals("u") || newExp[j].ToString().Equals("-"))
                        {
                            opr = newExp[j].ToString();
                        }
                        else
                        {
                            if (newExp[j].ToString().Equals("A"))
                            {
                                strArr1 = cal.A;
                                arr.Add(strArr1);
                            }
                            else if (newExp[j].ToString().Equals("B"))
                            {
                                strArr1 = cal.B;
                                arr.Add(strArr1);
                            }
                            else if (newExp[j].ToString().Equals("C"))
                            {
                                strArr1 = cal.C;
                                arr.Add(strArr1);
                            }
                            else
                            {
                                int e = Convert.ToInt32(newExp[j].ToString());
                                strArr1 = result[e].Split(',');
                                arr.Add(strArr1);
                            }
                        }
                        if (j > 1)
                        {
                            strArr2 = arr[arr.Count() -1];

                            if (string.IsNullOrEmpty(set.Result))
                            {
                                strArr1 = arr[arr.Count() - 2];
                                if (opr.Equals("n"))
                                {
                                    set.Result = string.Join(",", cal.Intersection(strArr1, strArr2));
                                }
                                else if (opr.Equals("u"))
                                {
                                    set.Result = string.Join(",", cal.Union(strArr1, strArr2));
                                }
                                else if (opr.Equals("-"))
                                {
                                    set.Result = string.Join(",", cal.Difference(strArr1, strArr2));
                                }
                            }
                            else
                            {
                                strArr1 = set.Result.Split(',');

                                if (opr.Equals("n"))
                                {
                                    set.Result = string.Join(",", cal.Intersection(strArr1, strArr2));
                                }
                                else if (opr.Equals("u"))
                                {
                                    set.Result = string.Join(",", cal.Union(strArr1, strArr2));
                                }
                                else if (opr.Equals("-"))
                                {
                                    set.Result = string.Join(",", cal.Difference(strArr1, strArr2));
                                }
                            }
                        }

                        
                    }

                    breakPoints.Add(exp);
                    result.Add(set.Result);
                }
            }

            BuildVennDigram venn = new BuildVennDigram();
            venn.Result = result;
            venn.BreakPoints = breakPoints;

            return venn;
            
        }

        public string SolveExpression(string newExp,Calculation cal)
        {
            string opr = "";
            string[] strArr1 = { };
            string[] strArr2 = { };
            string result = "";
            for (int j = 0; j < newExp.Length; j++)
            {
                if (newExp[j].ToString().Equals("n") || newExp[j].ToString().Equals("u") || newExp[j].ToString().Equals("-"))
                {
                    opr = newExp[j].ToString();
                }
                else if (strArr1.Count() == 0)
                {
                    if (newExp[j].ToString().Equals("A"))
                    {
                        strArr1 = cal.A;
                    }
                    else if (newExp[j].ToString().Equals("B"))
                    {
                        strArr1 = cal.B;
                    }
                    else if (newExp[j].ToString().Equals("C"))
                    {
                        strArr1 = cal.C;
                    }
                }
                else if(strArr2.Count() == 0)
                {
                    if (newExp[j].ToString().Equals("A"))
                    {
                        strArr2 = cal.A;
                    }
                    else if (newExp[j].ToString().Equals("B"))
                    {
                        strArr2 = cal.B;
                    }
                    else if (newExp[j].ToString().Equals("C"))
                    {
                        strArr2 = cal.C;
                    }
                }
            }


            if (opr.Equals("n"))
            {
                result = string.Join(",", cal.Intersection(strArr1, strArr2));
            }
            else if(opr.Equals("u"))
            {
                result = string.Join(",", cal.Union(strArr1, strArr2));
            }
            else if (opr.Equals("-"))
            {
                result = string.Join(",", cal.Difference(strArr1, strArr2));
            }

            return result;
        }
    }
}
