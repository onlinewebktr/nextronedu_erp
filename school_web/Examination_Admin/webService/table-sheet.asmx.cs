using school_web.AppCode;
using school_web.AppCode.Exam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace school_web.Examination_Admin.webService
{
    /// <summary>
    /// Summary description for table_sheet
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class table_sheet : System.Web.Services.WebService
    {
        My mycode = new My();
        //=======================
        public class Fetch_Details_of_report_head_structure
        {
            public string Assesment_head { get; set; }
            public string ColSpan { get; set; }
            public string bgColor { get; set; }
        }


        List<Fetch_Details_of_report_head_structure> Show_of_report_head_structure = new List<Fetch_Details_of_report_head_structure>();
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_report_mark_heading(string Session_id, string Class_id, string Term_id, string Subject_id)
        {
            string query = "select * from Exam_Assessment_Details where Session_Id=" + Session_id + " and Branch_Id='" + Session["Branchid"].ToString() + "' and  Class_id=" + Class_id + " and Exam_Term_Id=" + Term_id + " and Scholastic_Co_scholastic='Scholastic' and Istatus=1  order by Sequence_No asc";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Complain_chat");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {

                string marks_type = get_system_marks_type(Session_id, Class_id, Term_id);
                string[] stringSeparators = new string[] { "/" };
                string[] arr = marks_type.Split(stringSeparators, StringSplitOptions.None);
                marks_type = arr[0];
                string grade_system_id = arr[1];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string bgcolors = "";
                    if (i == 0)
                    {
                        bgcolors = "thbg2";
                    }
                    if (i == 1)
                    {
                        bgcolors = "thbg3";
                    }
                    if (i == 2)
                    {
                        bgcolors = "thbg4";
                    }
                    if (i == 3)
                    {
                        bgcolors = "thbg5";
                    }
                    if (i == 6)
                    {
                        bgcolors = "thbg7";
                    }

                    if (Subject_id == "1001")
                    {
                        DataTable dtx = mycode.FillData("select Subject_id from Subject_Master where course_id=" + Class_id + " and Subject_group='1001' and Branch_id='" + Session["Branchid"].ToString() + "'");
                        if (dt.Rows.Count > 0)
                        {
                            Subject_id = dtx.Rows[0]["Subject_id"].ToString();
                        }
                    }
                    if (Subject_id == "2001")
                    {
                        DataTable dtx = mycode.FillData("select Subject_id from Subject_Master where course_id=" + Class_id + " and Subject_group='2001' and Branch_id='" + Session["Branchid"].ToString() + "'");
                        if (dt.Rows.Count > 0)
                        {
                            Subject_id = dtx.Rows[0]["Subject_id"].ToString();
                        }
                    }


                    string qrys = "select * from dbo.[Exam_Subject_Sub_Level] where Session_Id=" + Session_id + " and Class_id=" + Class_id + " and Exam_Term_Id=" + Term_id + " and Assessment_Id=" + dt.Rows[i]["Assessment_Id"].ToString() + " and Subject_id=" + Subject_id + " and Branch_Id='" + Session["Branchid"].ToString() + "' and Istatus=1 order by Sequence_No asc";
                    DataTable dtS = mycode.FillData(qrys);
                    if (dtS.Rows.Count > 0)
                    {
                        int rowcount = dtS.Rows.Count;
                        if (rowcount == 1)
                        {
                            if (marks_type == "Marks")
                            {
                                for (int ii = 0; ii < dtS.Rows.Count; ii++)
                                {
                                    if (dt.Rows[i]["Maximum_Marks"].ToString() == dtS.Rows[ii]["Maximum_Marks"].ToString())
                                    {
                                        Show_of_report_head_structure.Add(new Fetch_Details_of_report_head_structure
                                        {
                                            Assesment_head = dt.Rows[i]["Short_Name"].ToString(),
                                            ColSpan = "1",
                                            bgColor = bgcolors,
                                        });
                                    }
                                    else
                                    {
                                        Show_of_report_head_structure.Add(new Fetch_Details_of_report_head_structure
                                        {
                                            Assesment_head = dt.Rows[i]["Short_Name"].ToString(),
                                            ColSpan = "2",
                                            bgColor = bgcolors,
                                        });
                                    }
                                }
                            }
                            else
                            {
                                for (int ii = 0; ii < dtS.Rows.Count; ii++)
                                {
                                    if (dt.Rows[i]["Maximum_Marks"].ToString() == dtS.Rows[ii]["Maximum_Marks"].ToString())
                                    {
                                        Show_of_report_head_structure.Add(new Fetch_Details_of_report_head_structure
                                        {
                                            Assesment_head = dt.Rows[i]["Short_Name"].ToString(),
                                            ColSpan = "3",
                                            bgColor = bgcolors,
                                        });
                                    }
                                    else
                                    {
                                        Show_of_report_head_structure.Add(new Fetch_Details_of_report_head_structure
                                        {
                                            Assesment_head = dt.Rows[i]["Short_Name"].ToString(),
                                            ColSpan = "4",
                                            bgColor = bgcolors,
                                        });
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (marks_type == "Marks")
                            {
                                int y = 1;
                                if (y == 1)
                                {
                                    bgcolors = "thbg12";
                                }
                                if (i == 2)
                                {
                                    bgcolors = "thbg13";
                                }
                                if (i == 3)
                                {
                                    bgcolors = "thbg14";
                                }
                                if (i == 4)
                                {
                                    bgcolors = "thbg15";
                                }
                                for (int ii = 0; ii < dtS.Rows.Count; ii++)
                                {
                                    Show_of_report_head_structure.Add(new Fetch_Details_of_report_head_structure
                                    {
                                        Assesment_head = dtS.Rows[ii]["Short_Name"].ToString(),
                                        ColSpan = rowcount.ToString(),
                                        bgColor = bgcolors,
                                    });
                                    y++;

                                    if (y > rowcount)
                                    {
                                        Show_of_report_head_structure.Add(new Fetch_Details_of_report_head_structure
                                        {
                                            Assesment_head = dt.Rows[i]["Short_Name"].ToString(),
                                            ColSpan = "1",
                                            bgColor = bgcolors,
                                        });
                                    }
                                }
                            }
                            else
                            {
                                int y = 1;
                                if (y == 1)
                                {
                                    bgcolors = "thbg12";
                                }
                                if (i == 2)
                                {
                                    bgcolors = "thbg13";
                                }
                                if (i == 3)
                                {
                                    bgcolors = "thbg14";
                                }
                                if (i == 4)
                                {
                                    bgcolors = "thbg15";
                                }
                                for (int ii = 0; ii < dtS.Rows.Count; ii++)
                                {
                                    Show_of_report_head_structure.Add(new Fetch_Details_of_report_head_structure
                                    {
                                        Assesment_head = dtS.Rows[ii]["Short_Name"].ToString(),
                                        ColSpan = (rowcount + 2).ToString(),
                                        bgColor = bgcolors,
                                    });
                                    y++;

                                    if (y > rowcount)
                                    {
                                        Show_of_report_head_structure.Add(new Fetch_Details_of_report_head_structure
                                        {
                                            Assesment_head = dt.Rows[i]["Short_Name"].ToString(),
                                            ColSpan = "3",
                                            bgColor = bgcolors,
                                        });
                                    }
                                }
                            }
                        }
                    }
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_report_head_structure));
            }
        }

        private string get_system_marks_type(string Session_id, string Class_id, string Term_id)
        {
            string returN = "0";
            string query = "select Grade_System_Id from Exam_Term_Details where Session_Id='" + Session_id + "' and Branch_Id='" + Session["Branchid"].ToString() + "' and Exam_Term_Id='" + Term_id + "' and Class_id='" + Class_id + "'";// and Scholastic_Co_scholastic='Scholastic'";
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count > 0)
            {
                string querys = "select Output from Exam_Grade_System where Session_Id='" + Session_id + "' and Branch_id='" + Session["Branchid"].ToString() + "'  and Grade_System_Id=" + dt.Rows[0]["Grade_System_Id"].ToString() + "";
                DataTable dts = My.dataTable(querys);
                if (dts.Rows.Count == 0)
                {
                    return returN;
                }
                else
                {
                    returN = dts.Rows[0]["Output"].ToString() + "/" + dt.Rows[0]["Grade_System_Id"].ToString();
                }
            }
            return returN;
        }


        public class Fetch_Details_of_report_head_structure_mm
        {
            public string MaxMarks { get; set; }
            public string bgColor { get; set; }
        }


        List<Fetch_Details_of_report_head_structure_mm> Show_of_report_head_structure_mm = new List<Fetch_Details_of_report_head_structure_mm>();
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_report_maxmark(string Session_id, string Class_id, string Term_id, string Subject_id)
        {
            string query = "select * from Exam_Assessment_Details where Session_Id=" + Session_id + " and Branch_Id='" + Session["Branchid"].ToString() + "' and  Class_id=" + Class_id + " and Exam_Term_Id=" + Term_id + " and Scholastic_Co_scholastic='Scholastic' and Istatus=1  order by Sequence_No asc";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Complain_chat");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                string marks_type = get_system_marks_type(Session_id, Class_id, Term_id);
                string[] stringSeparators = new string[] { "/" };
                string[] arr = marks_type.Split(stringSeparators, StringSplitOptions.None);
                marks_type = arr[0];
                string grade_system_id = arr[1];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Subject_id == "1001" || Subject_id == "2001")
                    {
                        string qrys = "";
                        if (i == 0)
                        {
                            if (Subject_id == "1001")
                            {
                                qrys = "select t1.Short_Name,isnull(sum(convert(float, Maximum_Marks)),0) as Maximum_Marks from Exam_Subject_Sub_Level t1 join Subject_Master t2 on t1.Branch_Id=t2.Branch_Id and t1.Class_id=t2.course_id and t1.Subject_id=t2.Subject_id where t1.Session_Id=" + Session_id + " and t1.Class_id=" + Class_id + " and t1.Exam_Term_Id=" + Term_id + " and t1.Assessment_Id=" + dt.Rows[i]["Assessment_Id"].ToString() + " and  t2.Subject_group='" + Subject_id + "' and t1.Branch_Id='" + Session["Branchid"].ToString() + "' and t1.Istatus=1 group by t1.Short_Name order by t1.Short_Name desc";
                            }
                            else
                            {
                                qrys = "select t1.Short_Name,isnull(sum(convert(float, Maximum_Marks)),0) as Maximum_Marks from Exam_Subject_Sub_Level t1 join Subject_Master t2 on t1.Branch_Id=t2.Branch_Id and t1.Class_id=t2.course_id and t1.Subject_id=t2.Subject_id where t1.Session_Id=" + Session_id + " and t1.Class_id=" + Class_id + " and t1.Exam_Term_Id=" + Term_id + " and t1.Assessment_Id=" + dt.Rows[i]["Assessment_Id"].ToString() + " and  t2.Subject_group='" + Subject_id + "' and t1.Branch_Id='" + Session["Branchid"].ToString() + "' and t1.Istatus=1 group by t1.Short_Name order by t1.Short_Name desc";
                            }
                        }
                        else
                        {
                            qrys = "select top 1 Maximum_Marks from Exam_Subject_Sub_Level t1 join Subject_Master t2 on t1.Branch_Id=t2.Branch_Id and t1.Class_id=t2.course_id and t1.Subject_id=t2.Subject_id where t1.Session_Id=" + Session_id + " and t1.Class_id=" + Class_id + " and t1.Exam_Term_Id=" + Term_id + " and t1.Assessment_Id=" + dt.Rows[i]["Assessment_Id"].ToString() + " and  t2.Subject_group='" + Subject_id + "' and t1.Branch_Id='" + Session["Branchid"].ToString() + "' and t1.Istatus=1";
                        }
                        DataTable dtS = mycode.FillData(qrys);
                        if (dtS.Rows.Count > 0)
                        {
                            string bgcolors = "";
                            if (i == 0)
                            {
                                bgcolors = "thbg2";
                            }
                            if (i == 1)
                            {
                                bgcolors = "thbg3";
                            }
                            if (i == 2)
                            {
                                bgcolors = "thbg4";
                            }
                            if (i == 3)
                            {
                                bgcolors = "thbg5";
                            }
                            if (i == 6)
                            {
                                bgcolors = "thbg6";
                            }


                            int rowcount = dtS.Rows.Count;
                            if (rowcount == 1)
                            {
                                if (marks_type == "Marks")
                                {
                                    for (int ii = 0; ii < dtS.Rows.Count; ii++)
                                    {
                                        if (dt.Rows[i]["Maximum_Marks"].ToString() == dtS.Rows[ii]["Maximum_Marks"].ToString())
                                        {
                                            Show_of_report_head_structure_mm.Add(new Fetch_Details_of_report_head_structure_mm
                                            {
                                                MaxMarks = dt.Rows[i]["Maximum_Marks"].ToString(),
                                                bgColor = bgcolors,
                                            });
                                        }
                                        else
                                        {
                                            Show_of_report_head_structure_mm.Add(new Fetch_Details_of_report_head_structure_mm
                                            {
                                                MaxMarks = dtS.Rows[ii]["Maximum_Marks"].ToString(),
                                                bgColor = bgcolors,
                                            });

                                            Show_of_report_head_structure_mm.Add(new Fetch_Details_of_report_head_structure_mm
                                            {
                                                MaxMarks = dt.Rows[i]["Maximum_Marks"].ToString(),
                                                bgColor = bgcolors,
                                            });
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (marks_type == "Marks")
                                {
                                    int y = 1;
                                    for (int ii = 0; ii < dtS.Rows.Count; ii++)
                                    {
                                        if (y == 1)
                                        {
                                            bgcolors = "thbg12";
                                        }
                                        if (i == 2)
                                        {
                                            bgcolors = "thbg13";
                                        }
                                        if (i == 3)
                                        {
                                            bgcolors = "thbg14";
                                        }
                                        if (i == 4)
                                        {
                                            bgcolors = "thbg15";
                                        }
                                        string mxmarks = dtS.Rows[ii]["Maximum_Marks"].ToString();
                                        if (mxmarks.Length > 2)
                                        {
                                            mxmarks = My.toDouble(mxmarks).ToString("0.0");
                                        }
                                        Show_of_report_head_structure_mm.Add(new Fetch_Details_of_report_head_structure_mm
                                        {
                                            MaxMarks = mxmarks,
                                            bgColor = bgcolors,
                                        });
                                        Show_of_report_head_structure_mm.Add(new Fetch_Details_of_report_head_structure_mm
                                        {
                                            MaxMarks = "100",
                                            bgColor = bgcolors,
                                        });
                                        y++;

                                        if (y > rowcount)
                                        {
                                            Show_of_report_head_structure_mm.Add(new Fetch_Details_of_report_head_structure_mm
                                            {
                                                MaxMarks = dt.Rows[i]["Maximum_Marks"].ToString(),
                                                bgColor = bgcolors,
                                            });
                                        }
                                    }
                                }
                            }
                        }

                    }
                    else
                    {
                        #region UngroupedSUbject
                        string qrys = "select * from Exam_Subject_Sub_Level where Session_Id=" + Session_id + " and Class_id=" + Class_id + " and Exam_Term_Id=" + Term_id + " and Assessment_Id=" + dt.Rows[i]["Assessment_Id"].ToString() + " and Subject_id=" + Subject_id + " and Branch_Id='" + Session["Branchid"].ToString() + "' and Istatus=1 order by Sequence_No asc";
                        DataTable dtS = mycode.FillData(qrys);
                        if (dtS.Rows.Count > 0)
                        {
                            string bgcolors = "";
                            if (i == 0)
                            {
                                bgcolors = "thbg2";
                            }
                            if (i == 1)
                            {
                                bgcolors = "thbg3";
                            }
                            if (i == 2)
                            {
                                bgcolors = "thbg4";
                            }
                            if (i == 3)
                            {
                                bgcolors = "thbg5";
                            }
                            if (i == 6)
                            {
                                bgcolors = "thbg6";
                            }


                            int rowcount = dtS.Rows.Count;
                            if (rowcount == 1)
                            {
                                if (marks_type == "Marks")
                                {
                                    for (int ii = 0; ii < dtS.Rows.Count; ii++)
                                    {
                                        if (dt.Rows[i]["Maximum_Marks"].ToString() == dtS.Rows[ii]["Maximum_Marks"].ToString())
                                        {
                                            Show_of_report_head_structure_mm.Add(new Fetch_Details_of_report_head_structure_mm
                                            {
                                                MaxMarks = dt.Rows[i]["Maximum_Marks"].ToString(),
                                                bgColor = bgcolors,
                                            });
                                        }
                                        else
                                        {
                                            Show_of_report_head_structure_mm.Add(new Fetch_Details_of_report_head_structure_mm
                                            {
                                                MaxMarks = dtS.Rows[ii]["Maximum_Marks"].ToString(),
                                                bgColor = bgcolors,
                                            });

                                            Show_of_report_head_structure_mm.Add(new Fetch_Details_of_report_head_structure_mm
                                            {
                                                MaxMarks = dt.Rows[i]["Maximum_Marks"].ToString(),
                                                bgColor = bgcolors,
                                            });
                                        }
                                    }
                                }
                                else
                                {
                                    for (int ii = 0; ii < dtS.Rows.Count; ii++)
                                    {
                                        if (dt.Rows[i]["Maximum_Marks"].ToString() == dtS.Rows[ii]["Maximum_Marks"].ToString())
                                        {
                                            Show_of_report_head_structure_mm.Add(new Fetch_Details_of_report_head_structure_mm
                                            {
                                                MaxMarks = dt.Rows[i]["Maximum_Marks"].ToString(),
                                                bgColor = bgcolors,
                                            });
                                            Show_of_report_head_structure_mm.Add(new Fetch_Details_of_report_head_structure_mm
                                            {
                                                MaxMarks = "100",
                                                bgColor = bgcolors,
                                            });
                                            Show_of_report_head_structure_mm.Add(new Fetch_Details_of_report_head_structure_mm
                                            {
                                                MaxMarks = "Grade",
                                                bgColor = bgcolors,
                                            });
                                        }
                                        else
                                        {
                                            Show_of_report_head_structure_mm.Add(new Fetch_Details_of_report_head_structure_mm
                                            {
                                                MaxMarks = dtS.Rows[ii]["Maximum_Marks"].ToString(),
                                                bgColor = bgcolors,
                                            });

                                            Show_of_report_head_structure_mm.Add(new Fetch_Details_of_report_head_structure_mm
                                            {
                                                MaxMarks = dt.Rows[i]["Maximum_Marks"].ToString(),
                                                bgColor = bgcolors,
                                            });
                                            Show_of_report_head_structure_mm.Add(new Fetch_Details_of_report_head_structure_mm
                                            {
                                                MaxMarks = "100",
                                                bgColor = bgcolors,
                                            });
                                            Show_of_report_head_structure_mm.Add(new Fetch_Details_of_report_head_structure_mm
                                            {
                                                MaxMarks = "Grade",
                                                bgColor = bgcolors,
                                            });
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (marks_type == "Marks")
                                {
                                    int y = 1;
                                    for (int ii = 0; ii < dtS.Rows.Count; ii++)
                                    {
                                        if (y == 1)
                                        {
                                            bgcolors = "thbg12";
                                        }
                                        if (i == 2)
                                        {
                                            bgcolors = "thbg13";
                                        }
                                        if (i == 3)
                                        {
                                            bgcolors = "thbg14";
                                        }
                                        if (i == 4)
                                        {
                                            bgcolors = "thbg15";
                                        }
                                        Show_of_report_head_structure_mm.Add(new Fetch_Details_of_report_head_structure_mm
                                        {
                                            MaxMarks = dtS.Rows[ii]["Maximum_Marks"].ToString(),
                                            bgColor = bgcolors,
                                        });
                                        Show_of_report_head_structure_mm.Add(new Fetch_Details_of_report_head_structure_mm
                                        {
                                            MaxMarks = "100",
                                            bgColor = bgcolors,
                                        });
                                        y++;

                                        if (y > rowcount)
                                        {
                                            Show_of_report_head_structure_mm.Add(new Fetch_Details_of_report_head_structure_mm
                                            {
                                                MaxMarks = dt.Rows[i]["Maximum_Marks"].ToString(),
                                                bgColor = bgcolors,
                                            });
                                        }
                                    }
                                }
                                else
                                {
                                    int y = 1;
                                    for (int ii = 0; ii < dtS.Rows.Count; ii++)
                                    {
                                        if (y == 1)
                                        {
                                            bgcolors = "thbg12";
                                        }
                                        if (i == 2)
                                        {
                                            bgcolors = "thbg13";
                                        }
                                        if (i == 3)
                                        {
                                            bgcolors = "thbg14";
                                        }
                                        if (i == 4)
                                        {
                                            bgcolors = "thbg15";
                                        }

                                        Show_of_report_head_structure_mm.Add(new Fetch_Details_of_report_head_structure_mm
                                        {
                                            MaxMarks = dtS.Rows[ii]["Maximum_Marks"].ToString(),
                                            bgColor = bgcolors,
                                        });
                                        Show_of_report_head_structure_mm.Add(new Fetch_Details_of_report_head_structure_mm
                                        {
                                            MaxMarks = "100",
                                            bgColor = bgcolors,
                                        });
                                        Show_of_report_head_structure_mm.Add(new Fetch_Details_of_report_head_structure_mm
                                        {
                                            MaxMarks = "100",
                                            bgColor = bgcolors,
                                        });
                                        Show_of_report_head_structure_mm.Add(new Fetch_Details_of_report_head_structure_mm
                                        {
                                            MaxMarks = "Grade",
                                            bgColor = bgcolors,
                                        });
                                        y++;

                                        if (y > rowcount)
                                        {
                                            Show_of_report_head_structure_mm.Add(new Fetch_Details_of_report_head_structure_mm
                                            {
                                                MaxMarks = dt.Rows[i]["Maximum_Marks"].ToString(),
                                                bgColor = bgcolors,
                                            });
                                            Show_of_report_head_structure_mm.Add(new Fetch_Details_of_report_head_structure_mm
                                            {
                                                MaxMarks = "100",
                                                bgColor = bgcolors,
                                            });
                                            Show_of_report_head_structure_mm.Add(new Fetch_Details_of_report_head_structure_mm
                                            {
                                                MaxMarks = "Grade",
                                                bgColor = bgcolors,
                                            });
                                        }
                                    }
                                }
                            }
                        }

                        #endregion
                    }
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_report_head_structure_mm));
            }
        }


        //===============================
        public class MyReporStudenttMark
        {
            public string Student_name { get; set; }
            public string Admission_no { get; set; }
            public string Roll_no { get; set; }

            public string Total_marks { get; set; }
            public string Full_mark { get; set; }
            public string Grade { get; set; }

            public List<MyStudentMDetails> MyStudentMaxMarkList { get; set; }

        }

        public class MyStudentMDetails
        {
            public string ResultMark { get; set; }
        } 
        List<MyReporStudenttMark> EMyBooking = new List<MyReporStudenttMark>();
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_report_marks_with_student(string Session_id, string Class_id, string Term_id, string Subject_id, string Section)
        {
            string qry = "select * from admission_registor where Session_id=" + Session_id + " and Class_id=" + Class_id + " and Section='" + Section + "' and   Branch_Id='" + Session["Branchid"].ToString() + "' and admissionserialnumber in (select Admission_no from Exam_marks where Session_id=" + Session_id + " and Class_id=" + Class_id + " and Section='" + Section + "' and Branch_Id='" + Session["Branchid"].ToString() + "'  and Subject='" + Subject_id + "' and Term='" + Term_id + "') order by rollnumber asc";

            if (Subject_id == "1001" || Subject_id == "2001")
            {
                DataTable dtx = mycode.FillData("select Subject_id from Subject_Master where course_id=" + Class_id + " and Subject_group='" + Subject_id + "' and Branch_id='" + Session["Branchid"].ToString() + "'");
                if (dtx.Rows.Count > 0)
                {
                    qry = "select * from admission_registor where Session_id=" + Session_id + " and Class_id=" + Class_id + " and Section='" + Section + "' and   Branch_Id='" + Session["Branchid"].ToString() + "' and admissionserialnumber in (select Admission_no from Exam_marks where Session_id=" + Session_id + " and Class_id=" + Class_id + " and Section='" + Section + "' and Branch_Id='" + Session["Branchid"].ToString() + "'  and Subject='" + dtx.Rows[0]["Subject_id"].ToString() + "' and Term='" + Term_id + "') order by rollnumber asc";
                }
            }


            SqlCommand cmd = new SqlCommand(qry);
            DataTable dt = mycode.GetData(cmd);
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
            }
            else
            {
                string marks_type = get_system_marks_type(Session_id, Class_id, Term_id);
                string[] stringSeparators = new string[] { "/" };
                string[] arr = marks_type.Split(stringSeparators, StringSplitOptions.None);
                marks_type = arr[0];
                string grade_system_id = arr[1];

                foreach (DataRow dr in dt.Rows)
                {
                    List<MyStudentMDetails> MBdetails = findmyStdMMMarks(Session_id, Class_id, Term_id, Subject_id, dr["admissionserialnumber"].ToString(), marks_type, grade_system_id);

                    string ttl_mrk = calculate_total(Session_id, Class_id, Term_id, Subject_id, dr["admissionserialnumber"].ToString(), Session["Branchid"].ToString(), grade_system_id);
                    string[] stringSeparatorss = new string[] { "/" };
                    string[] arrs = ttl_mrk.Split(stringSeparatorss, StringSplitOptions.None);
                    string total_mark = arrs[0];
                    string full_mark = arrs[1];
                    string grade = arrs[2];



                    //if (marks_type == "Marks")
                    //{
                    //}
                    //else
                    //{
                    //    string ttlgrade = get_grade_subj(Session_id, Session["Branchid"].ToString(), grade_system_id, My.toDouble(total_mark), full_mark, full_mark);
                    //    total_mark = ttlgrade;
                    //}

                    EMyBooking.Add(new MyReporStudenttMark
                    {
                        Student_name = dr["studentname"].ToString(),
                        Admission_no = dr["admissionserialnumber"].ToString(),
                        Roll_no = dr["rollnumber"].ToString(),

                        Total_marks = Math.Round(My.toDouble(total_mark), 0, MidpointRounding.AwayFromZero).ToString(),
                        Full_mark = full_mark,
                        Grade = grade,

                        MyStudentMaxMarkList = MBdetails
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(EMyBooking));
            }
        }


        private string calculate_total(string Session_id, string Class_id, string Term_id, string Subject_id, string Admission_no, string Branch_id, string grade_system_id)
        {
            string returN = "";
            string query = "select isnull(sum(convert(float, Marks)),0) as Total_marks,isnull(sum(convert(float, Full_marks)),0) as Total_full_marks from dbo.[Exam_temp_assesment_no_for_report] where Session_id='" + Session_id + "' and Branch_id='" + Branch_id + "' and Class_id='" + Class_id + "' and Term_id='" + Term_id + "' and Subject_id='" + Subject_id + "' and Admission_no='" + Admission_no + "'";
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count == 0)
            {
                return returN;
            }
            else
            {
                string ttl_mark = dt.Rows[0]["Total_marks"].ToString();
                string ttl_full_mark = dt.Rows[0]["Total_full_marks"].ToString();
                double ttl_perc = (My.toDouble(ttl_mark) / My.toDouble(ttl_full_mark)) * 100;
                string grade = Exam_setting.get_grade_of_a_subject(Session_id, Branch_id, grade_system_id, My.toDouble(ttl_mark), My.toDouble(ttl_full_mark), Class_id);

                returN = ttl_mark + "/" + ttl_full_mark + "/" + grade;
                return returN;
            }
        }

        private List<MyStudentMDetails> findmyStdMMMarks(string Session_id, string Class_id, string Term_id, string Subject_id, string Admission_no, string marks_type, string grade_system_id)
        {
            List<MyStudentMDetails> MyStudentMaxMarkList = new List<MyStudentMDetails>();
            SqlCommand cmd = new SqlCommand("select * from Exam_Assessment_Details where Session_Id=" + Session_id + " and Branch_Id='" + Session["Branchid"].ToString() + "' and  Class_id=" + Class_id + " and Exam_Term_Id=" + Term_id + " and Scholastic_Co_scholastic='Scholastic' and Istatus=1  order by Sequence_No asc");
            DataTable dt = mycode.GetData(cmd);
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
            }
            else
            {
                int x = 1;
                foreach (DataRow dr in dt.Rows)
                { 
                    if (Subject_id == "1001" || Subject_id == "2001")
                    {
                        #region GroupSubjecT
                        string qrys = "";
                        if (x == 1)
                        {
                            if (Subject_id == "1001")
                            {
                                qrys = "select t1.Short_Name,isnull(sum(convert(float, Maximum_Marks)),0) as Maximum_Marks from Exam_Subject_Sub_Level t1 join Subject_Master t2 on t1.Branch_Id=t2.Branch_Id and t1.Class_id=t2.course_id and t1.Subject_id=t2.Subject_id where Session_Id=" + Session_id + " and Class_id=" + Class_id + " and Exam_Term_Id=" + Term_id + " and Assessment_Id=" + dr["Assessment_Id"].ToString() + "  and t1.Branch_Id='" + Session["Branchid"].ToString() + "' and Istatus=1 and t2.Subject_group='" + Subject_id + "' group by t1.Short_Name  order by t1.Short_Name desc";
                            }
                            else
                            {
                                qrys = "select t1.Short_Name,isnull(sum(convert(float, Maximum_Marks)),0) as Maximum_Marks from Exam_Subject_Sub_Level t1 join Subject_Master t2 on t1.Branch_Id=t2.Branch_Id and t1.Class_id=t2.course_id and t1.Subject_id=t2.Subject_id where Session_Id=" + Session_id + " and Class_id=" + Class_id + " and Exam_Term_Id=" + Term_id + " and Assessment_Id=" + dr["Assessment_Id"].ToString() + "  and t1.Branch_Id='" + Session["Branchid"].ToString() + "' and Istatus=1 and t2.Subject_group='" + Subject_id + "' group by t1.Short_Name  order by t1.Short_Name desc";
                            }
                        }
                        else
                        {
                            if (Subject_id == "1001")
                            {
                                qrys = "select  isnull(sum(convert(float, Maximum_Marks)),0)/3 as Maximum_Marks from Exam_Subject_Sub_Level t1 join Subject_Master t2 on t1.Branch_Id=t2.Branch_Id and t1.Class_id=t2.course_id and t1.Subject_id=t2.Subject_id where Session_Id=" + Session_id + " and Class_id=" + Class_id + " and Exam_Term_Id=" + Term_id + " and Assessment_Id=" + dr["Assessment_Id"].ToString() + "  and t1.Branch_Id='" + Session["Branchid"].ToString() + "' and Istatus=1 and t2.Subject_group='" + Subject_id + "' ";
                            }
                            else
                            {
                                qrys = "select  isnull(sum(convert(float, Maximum_Marks)),0)/3 as Maximum_Marks from Exam_Subject_Sub_Level t1 join Subject_Master t2 on t1.Branch_Id=t2.Branch_Id and t1.Class_id=t2.course_id and t1.Subject_id=t2.Subject_id where Session_Id=" + Session_id + " and Class_id=" + Class_id + " and Exam_Term_Id=" + Term_id + " and Assessment_Id=" + dr["Assessment_Id"].ToString() + "  and t1.Branch_Id='" + Session["Branchid"].ToString() + "' and Istatus=1 and t2.Subject_group='" + Subject_id + "' ";
                            }
                        }

                        DataTable dtS = mycode.FillData(qrys);
                        if (dtS.Rows.Count > 0)
                        {
                            int rowcounts = dtS.Rows.Count;
                            if (rowcounts == 1)
                            {
                                for (int ii = 0; ii < dtS.Rows.Count; ii++)
                                {
                                    if (dr["Maximum_Marks"].ToString() == dtS.Rows[ii]["Maximum_Marks"].ToString())
                                    {
                                        string marks = get_marks(Session_id, Class_id, Admission_no, Term_id, dr["Assessment_Id"].ToString(), Subject_id, dr["Branch_id"].ToString(), "Scholastic", "0", x);

                                        if (marks_type == "Marks")   //Marks
                                        {
                                            MyStudentMaxMarkList.Add(new MyStudentMDetails
                                            {
                                                ResultMark = Math.Round(My.toDouble(marks), 1).ToString("0.0"),
                                            });
                                        }

                                        save_marks_temp(Session_id, Class_id, Admission_no, Term_id, dr["Assessment_Id"].ToString(), Subject_id, dr["Branch_id"].ToString(), Math.Round(My.toDouble(marks), 1).ToString("0.0"), dr["Maximum_Marks"].ToString(), dr["Grade_System_Id"].ToString());
                                    }
                                    else
                                    {
                                        string marks = get_marks(Session_id, Class_id, Admission_no, Term_id, dr["Assessment_Id"].ToString(), Subject_id, dr["Branch_id"].ToString(), "Scholastic", "0", x);

                                        double finalWeightage = 0;
                                        if (marks_type == "Marks")   //Marks
                                        {
                                            MyStudentMaxMarkList.Add(new MyStudentMDetails
                                            {
                                                ResultMark = Math.Round(My.toDouble(marks), 1).ToString("0.0"),
                                            });
                                            finalWeightage = (My.toDouble(marks) / My.toDouble(dtS.Rows[ii]["Maximum_Marks"].ToString())) * My.toDouble(dr["Maximum_Marks"].ToString());
                                            MyStudentMaxMarkList.Add(new MyStudentMDetails
                                            {
                                                ResultMark = Math.Round(finalWeightage, 1).ToString("0.0"),
                                            });
                                        }

                                        save_marks_temp(Session_id, Class_id, Admission_no, Term_id, dr["Assessment_Id"].ToString(), Subject_id, dr["Branch_id"].ToString(), Math.Round(finalWeightage, 1).ToString("0.0"), dr["Maximum_Marks"].ToString(), dr["Grade_System_Id"].ToString());
                                    }
                                }
                            }
                            else
                            {
                                int y = 1; double marksttl = 0; double maxmarksttl = 0; int devide_by = 0;
                                for (int ii = 0; ii < dtS.Rows.Count; ii++)
                                {
                                    string marks = get_marks(Session_id, Class_id, Admission_no, Term_id, dr["Assessment_Id"].ToString(), Subject_id, dr["Branch_id"].ToString(), "Scholastic", dtS.Rows[ii]["Short_Name"].ToString(), x);

                                    marksttl = marksttl + My.toDouble(marks);
                                    //if (My.toDouble(marks) > 0)
                                    //{
                                    maxmarksttl = maxmarksttl + My.toDouble(dtS.Rows[ii]["Maximum_Marks"].ToString());
                                    devide_by++;
                                    //}
                                    if (marks_type == "Marks")   //Marks
                                    {
                                        MyStudentMaxMarkList.Add(new MyStudentMDetails
                                        {
                                            ResultMark = Math.Round(My.toDouble(marks), 1).ToString("0.0"),
                                        });

                                        double percnt = (My.toDouble(marks) / My.toDouble(dtS.Rows[ii]["Maximum_Marks"].ToString())) * 100;
                                        MyStudentMaxMarkList.Add(new MyStudentMDetails
                                        {
                                            ResultMark = Math.Round(percnt, 1).ToString("0.0"),
                                        });


                                        y++;

                                        if (y > rowcounts)
                                        {
                                            marksttl = marksttl / dtS.Rows.Count;
                                            maxmarksttl = maxmarksttl / dtS.Rows.Count;

                                            //marksttl = marksttl / devide_by;
                                            //maxmarksttl = maxmarksttl / devide_by;

                                            double finalWeightage = (My.toDouble(marksttl) / maxmarksttl) * My.toDouble(dr["Maximum_Marks"].ToString());
                                            MyStudentMaxMarkList.Add(new MyStudentMDetails
                                            {
                                                ResultMark = Math.Round(finalWeightage, 1).ToString("0.0"),
                                            });

                                            save_marks_temp(Session_id, Class_id, Admission_no, Term_id, dr["Assessment_Id"].ToString(), Subject_id, dr["Branch_id"].ToString(), Math.Round(finalWeightage, 1).ToString("0.0"), dr["Maximum_Marks"].ToString(), dr["Grade_System_Id"].ToString());
                                        }
                                    }
                                }
                            }
                        }
                        x++;
                        #endregion
                    }
                    else
                    {
                        #region UngroupSubject
                        string qrys = "select * from Exam_Subject_Sub_Level where Session_Id=" + Session_id + " and Class_id=" + Class_id + " and Exam_Term_Id=" + Term_id + " and Assessment_Id=" + dr["Assessment_Id"].ToString() + " and Subject_id=" + Subject_id + " and Branch_Id='" + Session["Branchid"].ToString() + "' and Istatus=1 order by Sequence_No asc";
                        DataTable dtS = mycode.FillData(qrys);
                        if (dtS.Rows.Count > 0)
                        {
                            int rowcounts = dtS.Rows.Count;
                            if (rowcounts == 1)
                            {
                                for (int ii = 0; ii < dtS.Rows.Count; ii++)
                                {
                                    if (dr["Maximum_Marks"].ToString() == dtS.Rows[ii]["Maximum_Marks"].ToString())
                                    {
                                        string marks = get_marks(Session_id, Class_id, Admission_no, Term_id, dtS.Rows[ii]["Assessment_Id"].ToString(), Subject_id, dr["Branch_id"].ToString(), "Scholastic", dtS.Rows[ii]["Subject_Sub_Level_Id"].ToString(), x);

                                        if (marks_type == "Marks")   //Marks
                                        {
                                            MyStudentMaxMarkList.Add(new MyStudentMDetails
                                            {
                                                ResultMark = marks,
                                            });
                                        }
                                        else //GRADE
                                        {
                                            string grade = get_grade_subj(Session_id, Session["Branchid"].ToString(), grade_system_id, My.toDouble(marks), dtS.Rows[ii]["Maximum_Marks"].ToString(), dr["Maximum_Marks"].ToString());
                                            MyStudentMaxMarkList.Add(new MyStudentMDetails
                                            {
                                                ResultMark = marks,
                                            });


                                            double marksin100 = (My.toDouble(marks) / My.toDouble(dr["Maximum_Marks"].ToString())) * 100;
                                            MyStudentMaxMarkList.Add(new MyStudentMDetails
                                            {
                                                ResultMark = Math.Round(marksin100, 1).ToString("0.0"),
                                            });
                                            MyStudentMaxMarkList.Add(new MyStudentMDetails
                                            {
                                                ResultMark = grade,
                                            });
                                        }



                                        save_marks_temp(Session_id, Class_id, Admission_no, Term_id, dtS.Rows[ii]["Assessment_Id"].ToString(), Subject_id, dr["Branch_id"].ToString(), Math.Round(My.toDouble(marks), 1).ToString("0.0"), dr["Maximum_Marks"].ToString(), dr["Grade_System_Id"].ToString());
                                    }
                                    else
                                    {
                                        string marks = get_marks(Session_id, Class_id, Admission_no, Term_id, dtS.Rows[ii]["Assessment_Id"].ToString(), Subject_id, dr["Branch_id"].ToString(), "Scholastic", dtS.Rows[ii]["Subject_Sub_Level_Id"].ToString(), x);
                                        double finalWeightage = 0;
                                        if (marks_type == "Marks")   //Marks
                                        {
                                            MyStudentMaxMarkList.Add(new MyStudentMDetails
                                            {
                                                ResultMark = marks,
                                            });
                                            finalWeightage = (My.toDouble(marks) / My.toDouble(dtS.Rows[ii]["Maximum_Marks"].ToString())) * My.toDouble(dr["Maximum_Marks"].ToString());
                                            MyStudentMaxMarkList.Add(new MyStudentMDetails
                                            {
                                                ResultMark = Math.Round(finalWeightage, 1).ToString("0.0"),
                                            });
                                        }
                                        else //GRADE
                                        {
                                            MyStudentMaxMarkList.Add(new MyStudentMDetails
                                            {
                                                ResultMark = marks,
                                            });

                                            finalWeightage = (My.toDouble(marks) / My.toDouble(dtS.Rows[ii]["Maximum_Marks"].ToString())) * My.toDouble(dr["Maximum_Marks"].ToString());
                                            MyStudentMaxMarkList.Add(new MyStudentMDetails
                                            {
                                                ResultMark = Math.Round(finalWeightage, 1).ToString("0.0"),
                                            });

                                            double marksin100 = (My.toDouble(marks) / My.toDouble(dtS.Rows[ii]["Maximum_Marks"].ToString())) * 100;
                                            MyStudentMaxMarkList.Add(new MyStudentMDetails
                                            {
                                                ResultMark = Math.Round(marksin100, 1).ToString("0.0"),
                                            });

                                            string grade = get_grade_subj(Session_id, Session["Branchid"].ToString(), grade_system_id, My.toDouble(marks), dtS.Rows[ii]["Maximum_Marks"].ToString(), dr["Maximum_Marks"].ToString());
                                            MyStudentMaxMarkList.Add(new MyStudentMDetails
                                            {
                                                ResultMark = grade,
                                            });
                                        }

                                        save_marks_temp(Session_id, Class_id, Admission_no, Term_id, dtS.Rows[ii]["Assessment_Id"].ToString(), Subject_id, dr["Branch_id"].ToString(), Math.Round(finalWeightage, 1).ToString("0.0"), dr["Maximum_Marks"].ToString(), dr["Grade_System_Id"].ToString());
                                    }
                                }
                            }
                            else
                            {
                                int y = 1; double marksttl = 0; double maxmarksttl = 0; double devide_by = 0;
                                for (int ii = 0; ii < dtS.Rows.Count; ii++)
                                {
                                    string marks = get_marks(Session_id, Class_id, Admission_no, Term_id, dtS.Rows[ii]["Assessment_Id"].ToString(), Subject_id, dr["Branch_id"].ToString(), "Scholastic", dtS.Rows[ii]["Subject_Sub_Level_Id"].ToString(), x);

                                    marksttl = marksttl + My.toDouble(marks);
                                    //if (My.toDouble(marks) > 0)
                                    //{
                                    maxmarksttl = maxmarksttl + My.toDouble(dtS.Rows[ii]["Maximum_Marks"].ToString());
                                    devide_by++;
                                    //}
                                    // maxmarksttl = maxmarksttl + My.toDouble(dtS.Rows[ii]["Maximum_Marks"].ToString());

                                    if (marks_type == "Marks")   //Marks
                                    {
                                        MyStudentMaxMarkList.Add(new MyStudentMDetails
                                        {
                                            ResultMark = marks,
                                        });

                                        double percnt = (My.toDouble(marks) / My.toDouble(dtS.Rows[ii]["Maximum_Marks"].ToString())) * 100;
                                        MyStudentMaxMarkList.Add(new MyStudentMDetails
                                        {
                                            ResultMark = Math.Round(percnt, 1).ToString("0.0"),
                                        });


                                        y++;

                                        if (y > rowcounts)
                                        {
                                            marksttl = marksttl / dtS.Rows.Count;
                                            maxmarksttl = maxmarksttl / dtS.Rows.Count;

                                            //marksttl = marksttl / devide_by;
                                            //maxmarksttl = maxmarksttl / devide_by;


                                            double finalWeightage = (My.toDouble(marksttl) / maxmarksttl) * My.toDouble(dr["Maximum_Marks"].ToString());
                                            MyStudentMaxMarkList.Add(new MyStudentMDetails
                                            {
                                                ResultMark = Math.Round(finalWeightage, 1).ToString("0.0"),
                                            });

                                            save_marks_temp(Session_id, Class_id, Admission_no, Term_id, dtS.Rows[ii]["Assessment_Id"].ToString(), Subject_id, dr["Branch_id"].ToString(), Math.Round(finalWeightage, 1).ToString("0.0"), dr["Maximum_Marks"].ToString(), dr["Grade_System_Id"].ToString());
                                        }
                                    }
                                    else
                                    {
                                        string grade = get_grade_subj(Session_id, Session["Branchid"].ToString(), grade_system_id, My.toDouble(marks), dtS.Rows[ii]["Maximum_Marks"].ToString(), dr["Maximum_Marks"].ToString());
                                        MyStudentMaxMarkList.Add(new MyStudentMDetails
                                        {
                                            ResultMark = marks,
                                        });

                                        double percnt = (My.toDouble(marks) / My.toDouble(dtS.Rows[ii]["Maximum_Marks"].ToString())) * 100;
                                        MyStudentMaxMarkList.Add(new MyStudentMDetails
                                        {
                                            ResultMark = Math.Round(percnt, 1).ToString("0.0"),
                                        });


                                        double marksin100 = (My.toDouble(marks) / My.toDouble(dtS.Rows[ii]["Maximum_Marks"].ToString())) * 100;
                                        MyStudentMaxMarkList.Add(new MyStudentMDetails
                                        {
                                            ResultMark = Math.Round(marksin100, 1).ToString("0.0"),
                                        });

                                        MyStudentMaxMarkList.Add(new MyStudentMDetails
                                        {
                                            ResultMark = grade,
                                        });




                                        y++;

                                        if (y > rowcounts)
                                        {
                                            //marksttl = marksttl / devide_by;
                                            //maxmarksttl = maxmarksttl / devide_by;

                                            marksttl = marksttl / dtS.Rows.Count;
                                            maxmarksttl = maxmarksttl / dtS.Rows.Count;


                                            double finalWeightage = (My.toDouble(marksttl) / maxmarksttl) * My.toDouble(dr["Maximum_Marks"].ToString());

                                            string grades = get_grade_subj(Session_id, Session["Branchid"].ToString(), grade_system_id, marksttl, maxmarksttl.ToString(), dr["Maximum_Marks"].ToString());


                                            MyStudentMaxMarkList.Add(new MyStudentMDetails
                                            {
                                                ResultMark = Math.Round(finalWeightage, 1).ToString("0.0"),
                                            });


                                            double marksin100s = (My.toDouble(marksttl) / maxmarksttl) * 100;
                                            MyStudentMaxMarkList.Add(new MyStudentMDetails
                                            {
                                                ResultMark = Math.Round(marksin100s, 1).ToString("0.0"),
                                            });

                                            MyStudentMaxMarkList.Add(new MyStudentMDetails
                                            {
                                                ResultMark = grades, //finalWeightage.ToString("0.0"),
                                            });

                                            save_marks_temp(Session_id, Class_id, Admission_no, Term_id, dtS.Rows[ii]["Assessment_Id"].ToString(), Subject_id, dr["Branch_id"].ToString(), Math.Round(finalWeightage, 1).ToString("0.0"), dr["Maximum_Marks"].ToString(), dr["Grade_System_Id"].ToString());
                                        }
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                }
            }
            return MyStudentMaxMarkList;

        }

        private void save_marks_temp(string Session_id, string Class_id, string Admission_no, string Term_id, string Assessment_Id, string Subject_id, string Branch_id, string marks, string Maximum_Marks, string Grade_System_Id)
        {
            try
            {
                bool tot = mycode.cheknumer_Double(marks);
                if (tot == false)
                {
                    marks = "0";
                }
            }
            catch
            {
            }


            try
            {
                bool tot1 = mycode.cheknumer_Double(marks);
                if (tot1 == false)
                {
                    marks = "0";
                }
            }
            catch
            {
            }

            if (mycode.IsUserExist("select Id from Exam_temp_assesment_no_for_report where Session_id='" + Session_id + "' and Branch_id='" + Branch_id + "' and Class_id='" + Class_id + "' and Term_id='" + Term_id + "' and Assessment_id='" + Assessment_Id + "' and Subject_id='" + Subject_id + "' and Admission_no='" + Admission_no + "'"))
            {
                SqlCommand cmd;
                string query = "INSERT INTO Exam_temp_assesment_no_for_report (Session_id,Branch_id,Class_id,Term_id,Assessment_id,Subject_id,Admission_no,Marks,Full_marks,Grade_System_Id) values (@Session_id,@Branch_id,@Class_id,@Term_id,@Assessment_id,@Subject_id,@Admission_no,@Marks,@Full_marks,@Grade_System_Id)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Session_id", Session_id);
                cmd.Parameters.AddWithValue("@Branch_id", Branch_id);
                cmd.Parameters.AddWithValue("@Class_id", Class_id);
                cmd.Parameters.AddWithValue("@Term_id", Term_id);
                cmd.Parameters.AddWithValue("@Assessment_id", Assessment_Id);
                cmd.Parameters.AddWithValue("@Subject_id", Subject_id);
                cmd.Parameters.AddWithValue("@Admission_no", Admission_no);
                cmd.Parameters.AddWithValue("@Marks", marks);
                cmd.Parameters.AddWithValue("@Full_marks", Maximum_Marks);
                cmd.Parameters.AddWithValue("@Grade_System_Id", Grade_System_Id);
                if (My.InsertUpdateData(cmd))
                {
                }
            }
            else
            {
                SqlCommand cmd;
                string query = "Update Exam_temp_assesment_no_for_report set Marks=@Marks,Full_marks=@Full_marks,Grade_System_Id=@Grade_System_Id where Session_id='" + Session_id + "' and Branch_id='" + Branch_id + "' and Class_id='" + Class_id + "' and Term_id='" + Term_id + "' and Assessment_id='" + Assessment_Id + "' and Subject_id='" + Subject_id + "' and Admission_no='" + Admission_no + "'";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Marks", marks);
                cmd.Parameters.AddWithValue("@Full_marks", Maximum_Marks);
                cmd.Parameters.AddWithValue("@Grade_System_Id", Grade_System_Id);
                if (My.InsertUpdateData(cmd))
                {
                }
            }
        }




        //=======================================
        private static string get_grade_subj(string session_id, string branch_id, string Grade_System_Id, double final_marks, string Maximum_Marks, string assesment_weightage)
        {
            double final_markss = (My.toDouble(final_marks) / My.toDouble(Maximum_Marks)) * 100;
            final_marks = Math.Round(final_markss);
            string returN = "FAIL";
            string query = "select * from Exam_Grade_System_Range_Grade where Grade_System_Id=" + Grade_System_Id + " and  Session_Id=" + session_id + " and Branch_Id='" + branch_id + "'";
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count == 0)
            {
                return returN;
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    double Lower_Range = My.toDouble(dt.Rows[i]["Lower_Range"].ToString());
                    double Upper_Range = My.toDouble(dt.Rows[i]["Upper_Range"].ToString());

                    if (final_marks >= Lower_Range && final_marks <= Upper_Range)
                    {
                        returN = dt.Rows[i]["Grade"].ToString();
                    }
                }
                return returN;
            }
        }

        private string get_marks(string Session_id, string Class_id, string admission_no, string Term_id, string Assesment_id, string Subject_id, string branch_id, string Scholastic, string sub_sub_level_id, int ass_sequence)
        {
            string marks = "0";
            try
            {
                string querys = "";
                if (Subject_id == "1001" || Subject_id == "2001")
                {
                    string queryss = "";
                    if (sub_sub_level_id == "0")
                    {
                        queryss = "select t1.*,t3.Short_Name from Exam_marks t1 join Subject_Master t2 on t1.Branch_id=t2.Branch_id and t1.Class_id=t2.course_id and t1.Subject=t2.Subject_id join Exam_Subject_Sub_Level t3 on t1.Session_id=t3.Session_Id and t1.Branch_id=t3.Branch_id and t1.Class_id=t3.Class_id and t1.Term=t3.Exam_Term_Id and t1.Assessment=t3.Assessment_Id and t1.Subject=t3.Subject_id and t1.Subject_activity=t3.Subject_Sub_Level_Id where t1.Session_id='" + Session_id + "' and t1.Class_id='" + Class_id + "' and t1.Term='" + Term_id + "' and t1.Assessment='" + Assesment_id + "' and t1.Admission_no='" + admission_no + "' and t1.Branch_id='" + branch_id + "' and t2.Subject_group='" + Subject_id + "' ";
                    }
                    else
                    {
                        queryss = "select t1.*,t3.Short_Name from Exam_marks t1 join Subject_Master t2 on t1.Branch_id=t2.Branch_id and t1.Class_id=t2.course_id and t1.Subject=t2.Subject_id join Exam_Subject_Sub_Level t3 on t1.Session_id=t3.Session_Id and t1.Branch_id=t3.Branch_id and t1.Class_id=t3.Class_id and t1.Term=t3.Exam_Term_Id and t1.Assessment=t3.Assessment_Id and t1.Subject=t3.Subject_id and t1.Subject_activity=t3.Subject_Sub_Level_Id where t1.Session_id='" + Session_id + "' and t1.Class_id='" + Class_id + "' and t1.Term='" + Term_id + "' and t1.Assessment='" + Assesment_id + "' and t1.Admission_no='" + admission_no + "' and t1.Branch_id='" + branch_id + "' and t2.Subject_group='" + Subject_id + "' and t3.Short_Name='" + sub_sub_level_id + "'";
                    }
                    DataTable dtss = My.dataTable(queryss);
                    if (dtss.Rows.Count > 0)
                    {
                        double ttl_mrks = 0;
                        for (int i = 0; i < dtss.Rows.Count; i++)
                        {
                            ttl_mrks = ttl_mrks + My.toDouble(dtss.Rows[i]["Marks"].ToString());
                        }
                        if (ass_sequence == 1) { }
                        else { ttl_mrks = ttl_mrks / dtss.Rows.Count; }
                        marks = ttl_mrks.ToString();
                    }
                }
                else
                {
                    querys = "select Marks from Exam_marks where Session_id='" + Session_id + "' and Class_id='" + Class_id + "' and Term='" + Term_id + "'    and Assessment='" + Assesment_id + "' and Subject='" + Subject_id + "' and Subject_activity='" + sub_sub_level_id + "' and Admission_no='" + admission_no + "' and Branch_id='" + branch_id + "'";
                    DataTable dts = My.dataTable(querys);
                    if (dts.Rows.Count > 0)
                    {
                        marks = dts.Rows[0]["Marks"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return marks;
        }


    }
}
