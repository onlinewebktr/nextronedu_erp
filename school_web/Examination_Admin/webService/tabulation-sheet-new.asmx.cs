using school_web.AppCode;
using school_web.AppCode.Exam;
using school_web.Examination_Admin.slip.general.api;
using school_web.Examination_Admin.slip.sdhr.api;
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
    /// Summary description for tabulation_sheet_new
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class tabulation_sheet_new : System.Web.Services.WebService
    {
        My mycode = new My();
        //===============================
        public class Fetch_Details_of_report_head
        {
            public string Assessment_Name { get; set; }
            public string Short_Name { get; set; }
            public string Maximum_Marks { get; set; }
            public string Term_Maximum_Marks { get; set; }
            public string Ttl_subject { get; set; }
            public string Bg_colors { get; set; }
        }

        List<Fetch_Details_of_report_head> Show_of_report_head = new List<Fetch_Details_of_report_head>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_tabulation_assesment(string Session_id, string Class_id, string Term_id, string Section)
        {
            string qry = "select Assessment_Name,Short_Name,Assessment_Id,Maximum_Marks,(select top 1 Maximum_Marks from Exam_Term_Details where Session_Id=Exam_Assessment_Details.Session_Id and Branch_Id=Exam_Assessment_Details.Branch_Id and Exam_Term_Id=Exam_Assessment_Details.Exam_Term_Id) as Term_Maximum_Marks from Exam_Assessment_Details where Session_Id='" + Session_id + "' and  Class_id='" + Class_id + "' and Exam_Term_Id=" + Term_id + "  and Scholastic_Co_scholastic='Scholastic' order by Sequence_No asc";
            SqlDataAdapter ad = new SqlDataAdapter(qry, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Complain_chat");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                int assesmentStage = 0;
                int rowcounTS = rowcount1 + 2;
                string Bg_colors = "";
                string ttl_subject = get_ttl_subject(Session_id, Class_id, Term_id, Section);
                ttl_subject = (My.toIntS(ttl_subject) * 1).ToString();
                for (int i = 0; i < rowcounTS; i++)
                {
                    if (i == 0)
                    {
                        Bg_colors = "thbg1";
                    }
                    if (i == 1)
                    {
                        Bg_colors = "thbg2";
                    }
                    if (i == 2)
                    {
                        Bg_colors = "thbg3";
                    }
                    if (i == 3)
                    {
                        Bg_colors = "thbg4";
                    }
                    if (i == 4)
                    {
                        Bg_colors = "thbg5";
                    }
                    if (i == 5)
                    {
                        Bg_colors = "thbg6";
                    }
                    if (i == 6)
                    {
                        Bg_colors = "thbg7";
                    }
                    if (i == 7)
                    {
                        Bg_colors = "thbg8";
                    }
                    if (i == 8)
                    {
                        Bg_colors = "thbg9";
                    }
                    if (i == 9)
                    {
                        Bg_colors = "thbg10";
                    }
                    if (i == 10)
                    {
                        Bg_colors = "thbg11";
                    }


                    if (rowcount1 > i)
                    {
                        assesmentStage = 1;
                        Show_of_report_head.Add(new Fetch_Details_of_report_head
                        {
                            Assessment_Name = dt.Rows[i]["Assessment_Name"].ToString(),   // + " (" + dt.Rows[i]["Maximum_Marks"].ToString() + ")",
                            Short_Name = dt.Rows[i]["Short_Name"].ToString(),
                            Maximum_Marks = dt.Rows[i]["Maximum_Marks"].ToString(),
                            Term_Maximum_Marks = dt.Rows[i]["Term_Maximum_Marks"].ToString(),
                            Ttl_subject = ttl_subject,
                            Bg_colors = Bg_colors,
                        });
                    }

                    if (rowcount1 <= i)
                    {
                        if (assesmentStage == 2)
                        {
                            assesmentStage = 3;
                            Show_of_report_head.Add(new Fetch_Details_of_report_head
                            {
                                Assessment_Name = "GRADE",
                                Short_Name = "",
                                Maximum_Marks = "",
                                Term_Maximum_Marks = "",
                                Ttl_subject = ttl_subject,
                                Bg_colors = "thbg112",
                            });
                        }

                        if (assesmentStage == 1)
                        {
                            assesmentStage = 2;
                            Show_of_report_head.Add(new Fetch_Details_of_report_head
                            {
                                Assessment_Name = "TOTAL",
                                Short_Name = "",
                                Maximum_Marks = "",
                                Term_Maximum_Marks = "",
                                Ttl_subject = ttl_subject,
                                Bg_colors = "thbg111",
                            });
                        }

                    }
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_report_head));
            }
        }

        private string get_ttl_subject(string session_id, string class_id, string term_id, string section)
        {
            string ttlSubj = "0";
            string qrySubJ = "select * from Subject_Master where  course_id='" + class_id + "'  and Subject_Type_Scholastic_Co_Scholastic='Scholastic'  and Is_mandatory=1";
            DataTable dtSubJ = My.dataTable(qrySubJ);
            ttlSubj = dtSubJ.Rows.Count.ToString();
            return ttlSubj;
        }


        //===============================
        public class Fetch_Details_of_report_head_subj
        {
            public string Subject_Short_Name { get; set; }
            public string Subject_name { get; set; }
            public string Subject_id { get; set; }
            public string Bg_colors { get; set; }
            public string ColSpans { get; set; }
            public string assSubj { get; set; }
        }

        List<Fetch_Details_of_report_head_subj> Show_of_report_head_subj = new List<Fetch_Details_of_report_head_subj>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_tabulation_subjects(string Session_id, string Class_id, string Term_id, string Section)
        {
            string qry = "select Assessment_Name,Short_Name,Assessment_Id,Maximum_Marks,(select top 1 Maximum_Marks from Exam_Term_Details where Session_Id=Exam_Assessment_Details.Session_Id and Branch_Id=Exam_Assessment_Details.Branch_Id and Exam_Term_Id=Exam_Assessment_Details.Exam_Term_Id) as Term_Maximum_Marks from Exam_Assessment_Details where Session_Id='" + Session_id + "' and  Class_id='" + Class_id + "' and Exam_Term_Id=" + Term_id + "  and Scholastic_Co_scholastic='Scholastic' order by Sequence_No asc";
            SqlDataAdapter ad = new SqlDataAdapter(qry, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Complain_chat");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                int assesmentStage = 0;
                int rowcounTS = rowcount1 + 2;
                string Bg_colors = "";
                for (int i = 0; i < rowcounTS; i++)
                {
                    if (i == 0)
                    {
                        Bg_colors = "thbg1";
                    }
                    if (i == 1)
                    {
                        Bg_colors = "thbg2";
                    }
                    if (i == 2)
                    {
                        Bg_colors = "thbg3";
                    }
                    if (i == 3)
                    {
                        Bg_colors = "thbg4";
                    }
                    if (i == 4)
                    {
                        Bg_colors = "thbg5";
                    }
                    if (i == 5)
                    {
                        Bg_colors = "thbg6";
                    }
                    if (i == 6)
                    {
                        Bg_colors = "thbg7";
                    }
                    if (i == 7)
                    {
                        Bg_colors = "thbg8";
                    }
                    if (i == 8)
                    {
                        Bg_colors = "thbg9";
                    }
                    if (i == 9)
                    {
                        Bg_colors = "thbg10";
                    }
                    if (i == 10)
                    {
                        Bg_colors = "thbg11";
                    }

                    if (rowcount1 > i)
                    {
                        string qrySubJ = "select * from Subject_Master where course_id='" + Class_id + "' and Subject_Type_Scholastic_Co_Scholastic='Scholastic'  and Is_mandatory=1 order by Subject_position asc";
                        DataTable dtSubJ = My.dataTable(qrySubJ);
                        if (dtSubJ.Rows.Count > 0)
                        {
                            assesmentStage = 1;
                            foreach (DataRow drS in dtSubJ.Rows)
                            {
                                Show_of_report_head_subj.Add(new Fetch_Details_of_report_head_subj
                                {
                                    Subject_Short_Name = drS["Subject_Short_Name"].ToString(),
                                    Subject_name = drS["Subject_name"].ToString(),
                                    Subject_id = drS["Subject_id"].ToString(),
                                    Bg_colors = Bg_colors,
                                    ColSpans = "2",
                                    assSubj = "assSubjCss",
                                });
                            }
                        }
                    }

                    if (rowcount1 <= i)
                    {
                        if (assesmentStage == 2)
                        {
                            assesmentStage = 3;
                            string qrySubJ = "select * from Subject_Master  where course_id='" + Class_id + "' and Subject_Type_Scholastic_Co_Scholastic='Scholastic'  and Is_mandatory=1 order by Subject_position asc";
                            DataTable dtSubJ = My.dataTable(qrySubJ);
                            if (dtSubJ.Rows.Count > 0)
                            {
                                foreach (DataRow drS in dtSubJ.Rows)
                                {
                                    Show_of_report_head_subj.Add(new Fetch_Details_of_report_head_subj
                                    {
                                        Subject_Short_Name = drS["Subject_Short_Name"].ToString(),
                                        Subject_name = drS["Subject_name"].ToString(),
                                        Subject_id = drS["Subject_id"].ToString(),
                                        Bg_colors = "thbg112",
                                        ColSpans = "2",
                                        assSubj = "",
                                    });
                                }
                            }
                        }

                        if (assesmentStage == 1)
                        {
                            assesmentStage = 2;
                            string qrySubJ = "select * from Subject_Master where course_id='" + Class_id + "'  and Subject_Type_Scholastic_Co_Scholastic='Scholastic' and Is_mandatory=1 order by Subject_position asc";
                            DataTable dtSubJ = My.dataTable(qrySubJ);
                            if (dtSubJ.Rows.Count > 0)
                            {
                                foreach (DataRow drS in dtSubJ.Rows)
                                {
                                    Show_of_report_head_subj.Add(new Fetch_Details_of_report_head_subj
                                    {
                                        Subject_Short_Name = drS["Subject_Short_Name"].ToString(),
                                        Subject_name = drS["Subject_name"].ToString(),
                                        Subject_id = drS["Subject_id"].ToString(),
                                        Bg_colors = "thbg111",
                                        ColSpans = "2",
                                        assSubj = "",
                                    });
                                }
                            }
                        }
                    }
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_report_head_subj));
            }
        }



        /// <summary>
        /// ============
        /// </summary>
        /// 
        public class Fetch_Details_of_report_head_subj_th_pr
        {
            public string Subject_type { get; set; }
            public string Bg_colors { get; set; }
        }

        List<Fetch_Details_of_report_head_subj_th_pr> Show_of_report_head_subj_th_pr = new List<Fetch_Details_of_report_head_subj_th_pr>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_tabulation_subjects_th_pr(string Session_id, string Class_id, string Term_id, string Section, string Branch_id)
        {
            string qry = "select Assessment_Name,Short_Name,Assessment_Id,Maximum_Marks,(select top 1 Maximum_Marks from Exam_Term_Details where Session_Id=Exam_Assessment_Details.Session_Id and Branch_Id=Exam_Assessment_Details.Branch_Id and Exam_Term_Id=Exam_Assessment_Details.Exam_Term_Id) as Term_Maximum_Marks from Exam_Assessment_Details where Session_Id='" + Session_id + "' and  Class_id='" + Class_id + "' and Exam_Term_Id=" + Term_id + "  and Scholastic_Co_scholastic='Scholastic' order by Sequence_No asc";
            SqlDataAdapter ad = new SqlDataAdapter(qry, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Complain_chat");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                string Bg_colors = "";
                for (int i = 0; i < rowcount1; i++)
                {
                    if (i == 0)
                    {
                        Bg_colors = "thbg1";
                    }
                    if (i == 1)
                    {
                        Bg_colors = "thbg2";
                    }
                    if (i == 2)
                    {
                        Bg_colors = "thbg3";
                    }
                    if (i == 3)
                    {
                        Bg_colors = "thbg4";
                    }
                    if (i == 4)
                    {
                        Bg_colors = "thbg5";
                    }
                    if (i == 5)
                    {
                        Bg_colors = "thbg6";
                    }
                    if (i == 6)
                    {
                        Bg_colors = "thbg7";
                    }
                    if (i == 7)
                    {
                        Bg_colors = "thbg8";
                    }
                    if (i == 8)
                    {
                        Bg_colors = "thbg9";
                    }
                    if (i == 9)
                    {
                        Bg_colors = "thbg10";
                    }
                    if (i == 10)
                    {
                        Bg_colors = "thbg11";
                    }

                    string qrySubJ = "select * from Subject_Master  where  course_id='" + Class_id + "' and Subject_Type_Scholastic_Co_Scholastic='Scholastic' and  Is_mandatory=1 order by Subject_position asc";
                    DataTable dtSubJ = My.dataTable(qrySubJ);
                    if (dtSubJ.Rows.Count > 0)
                    {
                        foreach (DataRow drS in dtSubJ.Rows)
                        {
                            Show_of_report_head_subj_th_pr.Add(new Fetch_Details_of_report_head_subj_th_pr
                            {
                                Subject_type = "TH",
                                Bg_colors = Bg_colors,
                            });

                            Show_of_report_head_subj_th_pr.Add(new Fetch_Details_of_report_head_subj_th_pr
                            {
                                Subject_type = "PR",
                                Bg_colors = Bg_colors,
                            });

                        }
                    }
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_report_head_subj_th_pr));
            }
        }


        public class Fetch_Details_of_report_head_subj_full_marks
        {
            public string Subject_maximum_marks { get; set; }
            public string Bg_colors { get; set; }
        }

        List<Fetch_Details_of_report_head_subj_full_marks> Show_of_report_head_subj_full_marks = new List<Fetch_Details_of_report_head_subj_full_marks>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_tabulation_subjects_full_marks(string Session_id, string Class_id, string Term_id, string Section, string Branch_id)
        {
            string qry = "select Assessment_Name,Short_Name,Assessment_Id,Maximum_Marks,(select top 1 Maximum_Marks from Exam_Term_Details where Session_Id=Exam_Assessment_Details.Session_Id and Branch_Id=Exam_Assessment_Details.Branch_Id and Exam_Term_Id=Exam_Assessment_Details.Exam_Term_Id) as Term_Maximum_Marks from Exam_Assessment_Details where Session_Id='" + Session_id + "' and  Class_id='" + Class_id + "' and Exam_Term_Id=" + Term_id + "  and Scholastic_Co_scholastic='Scholastic' order by Sequence_No asc";
            SqlDataAdapter ad = new SqlDataAdapter(qry, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Complain_chat");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 > 0)
            {
                string Bg_colors = "";
                for (int i = 0; i < rowcount1; i++)
                {
                    if (i == 0)
                    {
                        Bg_colors = "thbg1";
                    }
                    if (i == 1)
                    {
                        Bg_colors = "thbg2";
                    }
                    if (i == 2)
                    {
                        Bg_colors = "thbg3";
                    }
                    if (i == 3)
                    {
                        Bg_colors = "thbg4";
                    }
                    if (i == 4)
                    {
                        Bg_colors = "thbg5";
                    }
                    if (i == 5)
                    {
                        Bg_colors = "thbg6";
                    }
                    if (i == 6)
                    {
                        Bg_colors = "thbg7";
                    }
                    if (i == 7)
                    {
                        Bg_colors = "thbg8";
                    }
                    if (i == 8)
                    {
                        Bg_colors = "thbg9";
                    }
                    if (i == 9)
                    {
                        Bg_colors = "thbg10";
                    }
                    if (i == 10)
                    {
                        Bg_colors = "thbg11";
                    }



                    string qrySubJ = "select * from Subject_Master where course_id='" + Class_id + "' and Branch_id='" + Branch_id + "' and Subject_Type_Scholastic_Co_Scholastic='Scholastic' and Is_mandatory=1 order by Subject_position asc";
                    DataTable dtSubJ = My.dataTable(qrySubJ);
                    if (dtSubJ.Rows.Count > 0)
                    {
                        foreach (DataRow drS in dtSubJ.Rows)
                        {
                            string qrys = "select * from Exam_Subject_Sub_Level where Session_Id='" + Session_id + "' and Branch_Id='" + Branch_id + "' and Exam_Term_Id='" + Term_id + "' and Assessment_Id='" + dt.Rows[i]["Assessment_Id"].ToString() + "' and Class_id='" + Class_id + "' and Subject_id='" + drS["Subject_id"].ToString() + "' order by Sequence_No asc";
                            DataTable dtS = My.dataTable(qrys);
                            if (dtS.Rows.Count.ToString() == "0")
                            {
                                Show_of_report_head_subj_full_marks.Add(new Fetch_Details_of_report_head_subj_full_marks
                                {
                                    Subject_maximum_marks = "-",
                                    Bg_colors = Bg_colors,
                                });

                                //Show_of_report_head_subj_full_marks.Add(new Fetch_Details_of_report_head_subj_full_marks
                                //{
                                //    Subject_maximum_marks = "-",
                                //    Bg_colors = Bg_colors,
                                //});
                            }
                            else if (dtS.Rows.Count.ToString() == "1")
                            {
                                Show_of_report_head_subj_full_marks.Add(new Fetch_Details_of_report_head_subj_full_marks
                                {
                                    Subject_maximum_marks = dtS.Rows[0]["Maximum_Marks"].ToString(),
                                    Bg_colors = Bg_colors,
                                });

                                //Show_of_report_head_subj_full_marks.Add(new Fetch_Details_of_report_head_subj_full_marks
                                //{
                                //    Subject_maximum_marks = "-",
                                //    Bg_colors = Bg_colors,
                                //});
                            }
                            else
                            {
                                Show_of_report_head_subj_full_marks.Add(new Fetch_Details_of_report_head_subj_full_marks
                                {
                                    Subject_maximum_marks = dtS.Rows[0]["Maximum_Marks"].ToString(),
                                    Bg_colors = Bg_colors,
                                });

                                //Show_of_report_head_subj_full_marks.Add(new Fetch_Details_of_report_head_subj_full_marks
                                //{
                                //    Subject_maximum_marks = dtS.Rows[1]["Maximum_Marks"].ToString(),
                                //    Bg_colors = Bg_colors,
                                //});
                            }
                        }
                    }
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_report_head_subj_full_marks));
            }
        }



        public class Fetch_Details_of_report_head_subj_ttl_full_marks
        {
            public string FM { get; set; }
            public string Bg_colors { get; set; }
        }

        List<Fetch_Details_of_report_head_subj_ttl_full_marks> Show_of_report_head_subj_ttl_full_marks = new List<Fetch_Details_of_report_head_subj_ttl_full_marks>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_tabulation_subjects_ttl_full_marks(string Session_id, string Class_id, string Term_id, string Section, string Branch_id)
        {
            string qrySubJ = "select * from Subject_Master where course_id='" + Class_id + "' and Subject_Type_Scholastic_Co_Scholastic='Scholastic' and  Is_mandatory=1 order by Subject_position asc";
            DataTable dtSubJ = My.dataTable(qrySubJ);
            if (dtSubJ.Rows.Count > 0)
            {
                foreach (DataRow drS in dtSubJ.Rows)
                {
                    string FM = get_fm_of_a_subj(Session_id, Class_id, Term_id, Section, Branch_id, drS["Subject_id"].ToString());
                    Show_of_report_head_subj_ttl_full_marks.Add(new Fetch_Details_of_report_head_subj_ttl_full_marks
                    {
                        FM = FM,
                        Bg_colors = "thbg111",
                    });
                }
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(Show_of_report_head_subj_ttl_full_marks));

        }

        public class Fetch_Details_of_report_head_subj_ttl_G
        {
            public string FMG { get; set; }
            public string Bg_colors { get; set; }
        }

        List<Fetch_Details_of_report_head_subj_ttl_G> Show_of_report_head_subj_ttl_G = new List<Fetch_Details_of_report_head_subj_ttl_G>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_tabulation_subjects_G(string Session_id, string Class_id, string Term_id, string Section, string Branch_id)
        {
            string qrySubJ = "select * from  Subject_Master  where course_id='" + Class_id + "' and Subject_Type_Scholastic_Co_Scholastic='Scholastic' and Is_mandatory=1 order by Subject_position asc";
            DataTable dtSubJ = My.dataTable(qrySubJ);
            if (dtSubJ.Rows.Count > 0)
            {
                foreach (DataRow drS in dtSubJ.Rows)
                {
                    Show_of_report_head_subj_ttl_G.Add(new Fetch_Details_of_report_head_subj_ttl_G
                    {
                        FMG = "G",
                        Bg_colors = "thbg112",
                    });
                }
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Write(js.Serialize(Show_of_report_head_subj_ttl_G));

        }

        private string get_fm_of_a_subj(string session_id, string class_id, string term_id, string section, string branch_id, string Subject_id)
        {
            string FM = "0";
            DataTable dt = My.dataTable("select isnull(sum(convert(float, Full_marks)),0) as FM from Exam_temp_tabulation_total_no where Session_id='" + session_id + "' and Branch_id='" + branch_id + "' and Class_id='" + class_id + "' and Term_id='" + term_id + "' and Subject_id='" + Subject_id + "' GROUP by Admission_no");
            if (dt.Rows.Count > 0)
            {
                FM = dt.Rows[0][0].ToString();
            }
            return FM;
        }

        public class Fetch_student_details
        {
            public string Admission_no { get; set; }
            public string Section { get; set; }
            public string rollnumber { get; set; }
            public string studentname { get; set; }
            public string Rank { get; set; }
            public string Ttl_no_class { get; set; }
            public string Ttl_attended { get; set; }
            public string Ttl_percentage { get; set; }
            public List<MySubjectMarks> MySubjectMarksItem { get; set; }
            public List<MySubjectTotalMarks> MySubjectTotalMarksItem { get; set; }
            public List<MySubjectTotalMarksOvrALL> MySubjectTotalMarksItemOvrALL { get; set; }
            public List<MySubjectTotalGrade> MySubjectTotalGradeItem { get; set; }
            public List<MySubjectMarksCS> MySubjectMarksItemCS { get; set; }
            public List<MySubjectMarksDesc> MySubjectMarksItemDesc { get; set; }
        }

        public class MySubjectMarks
        {
            public string Marks { get; set; }
        }

        public class MySubjectTotalMarks
        {
            public string Total_subject_marks { get; set; }
        }
        public class MySubjectTotalMarksOvrALL
        {
            public string Overall_full_mark { get; set; }
            public string Overall_full_obtained_mark { get; set; }
            public string Overall_full_persentage_mark { get; set; }
        }
        public class MySubjectTotalGrade
        {
            public string subject_grade { get; set; }
        }

        public class MySubjectMarksCS
        {
            public string Marks { get; set; }
        }

        public class MySubjectMarksDesc
        {
            public string Marks { get; set; }
        }

        List<Fetch_student_details> EMySubMark = new List<Fetch_student_details>();
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_tabulation_student_marks(string Session_id, string Class_id, string Term_id, string Section)
        {
            string query = "select DISTINCT  em.Admission_no,em.Session_id,em.Class_id,em.Branch_id,em.Term,ar.class,ar.session,ar.Section,ar.rollnumber,ar.studentname,ar.dob,ar.mothername,ar.fathername,ar.studentimagepath,isnull((select top 1 Rank from Exam_rank_master where Session_id=ar.Session_id and Class_id=ar.Class_id and Branch_id=ar.Branch_id and Admission_no=ar.admissionserialnumber and Term_id=" + Term_id + "),'0') as Rank  from Exam_marks em join admission_registor ar on em.Session_id=ar.Session_id and em.Branch_id=ar.Branch_id and em.Class_id=ar.Class_id and em.Admission_no=ar.admissionserialnumber where em.Session_id=" + Session_id + " and em.Class_id=" + Class_id + "  and  em.Term=" + Term_id + " and ar.Section='" + Section + "' and ar.Status='1' order by ar.rollnumber asc";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "admission_registor");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                int rowconts = 1; int daysPlus = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    My.exeSql("delete from EXAM_TEMP_TABULATION_TOTAL_NO where Session_id='" + Session_id + "' and Class_id='" + Class_id + "' and Term_id='" + Term_id + "' and Admission_no='" + dr["Admission_no"].ToString() + "'");
                    List<MySubjectMarks> MBdetails = findmySubjMarks(Session_id, Class_id, Term_id, Section, dr["Admission_no"].ToString(), dr["Branch_id"].ToString());
                    List<MySubjectTotalMarks> MBdetailsTotal = findmySubjTotalMarks(Session_id, Class_id, Term_id, Section, dr["Admission_no"].ToString(), dr["Branch_id"].ToString());

                    List<MySubjectTotalMarksOvrALL> MBdetailsTotalOvrALL = findmySubjTotalMarksOvrALL(Session_id, Class_id, Term_id, Section, dr["Admission_no"].ToString(), dr["Branch_id"].ToString());

                    List<MySubjectTotalGrade> MBdetailsTotalGrade = findmySubjTotalGrade(Session_id, Class_id, Term_id, Section, dr["Admission_no"].ToString(), dr["Branch_id"].ToString());
                    List<MySubjectMarksCS> MBdetailsCS = findmySubjMarksCS(Session_id, Class_id, Term_id, Section, dr["Admission_no"].ToString(), dr["Branch_id"].ToString());
                    List<MySubjectMarksDesc> MBdetailsDesc = findmySubjMarksDesc(Session_id, Class_id, Term_id, Section, dr["Admission_no"].ToString(), dr["Branch_id"].ToString());


                    string attendance = get_attandance_detail(Session_id, "1", Class_id, Term_id, dr["Admission_no"].ToString());
                    string[] stringSeparatorss = new string[] { "月" };
                    string[] arrs = attendance.Split(stringSeparatorss, StringSplitOptions.None);
                    string ttl_no_class = arrs[0];
                    string ttl_attended = arrs[1];
                    string ttl_percentage = arrs[2];


                    EMySubMark.Add(new Fetch_student_details
                    {
                        Admission_no = dr["Admission_no"].ToString(),
                        Section = dr["Section"].ToString(),
                        rollnumber = dr["rollnumber"].ToString(),
                        studentname = dr["studentname"].ToString(),
                        Rank = dr["Rank"].ToString(),
                        Ttl_no_class = ttl_no_class,
                        Ttl_attended = ttl_attended,
                        Ttl_percentage = ttl_percentage,

                        MySubjectMarksItem = MBdetails,
                        MySubjectTotalMarksItem = MBdetailsTotal,
                        MySubjectTotalGradeItem = MBdetailsTotalGrade,
                        MySubjectMarksItemCS = MBdetailsCS,
                        MySubjectMarksItemDesc = MBdetailsDesc,
                        MySubjectTotalMarksItemOvrALL = MBdetailsTotalOvrALL,

                    });
                    rowconts++; daysPlus++;
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(EMySubMark));
            }
        }


        private string get_attandance_detail(string session_id, string branch_id, string class_id, string term_id, string admission_no)
        {
            string returN = "0月0月0";
            string querys = "select * from Exam_Term_Wise_Attendance where Session_id='" + session_id + "' and Class_id='" + class_id + "' and Branch_id='" + branch_id + "' and Admission_no='" + admission_no + "' and Exam_Term_Id='" + term_id + "'";
            DataTable dt = My.dataTable(querys);
            if (dt.Rows.Count == 0)
            {
                return returN;
            }
            else
            {
                try
                {
                    if (dt.Rows[0]["Total_no_of_class"].ToString() == "")
                    {
                        string query = "select *,(select top 1 No_of_class_Attendance from Exam_Term_Wise_Attendance where Session_Id=td.Session_Id and Branch_Id=td.Branch_Id and Class_id=td.Class_id and Exam_Term_Id=td.Exam_Term_Id and Admission_no='" + admission_no + "') as Persent_attandance from Exam_Term_Details td where Session_Id=" + session_id + " and Branch_Id='" + branch_id + "' and Class_id=" + class_id + " and Exam_Term_Id=" + term_id + "";
                        DataTable dtx = My.dataTable(query);
                        if (dtx.Rows.Count == 0)
                        {
                            return returN;
                        }
                        else
                        {
                            if (dtx.Rows[0]["Persent_attandance"].ToString() == "")
                            {
                                returN = dtx.Rows[0]["No_of_Class"].ToString() + "月0月0";
                            }
                            else
                            {
                                double att_persent = (My.toDouble(dtx.Rows[0]["Persent_attandance"].ToString()) / My.toDouble(dtx.Rows[0]["No_of_Class"].ToString())) * 100;
                                returN = dtx.Rows[0]["No_of_Class"].ToString() + "月" + dtx.Rows[0]["Persent_attandance"].ToString() + "月" + att_persent.ToString("0.00");
                            }
                            return returN;
                        }
                    }
                    else
                    {
                        if (dt.Rows[0]["No_of_class_Attendance"].ToString() == "")
                        {
                            returN = dt.Rows[0]["Total_no_of_class"].ToString() + "月0月0";
                        }
                        else
                        {
                            double att_persent = (My.toDouble(dt.Rows[0]["No_of_class_Attendance"].ToString()) / My.toDouble(dt.Rows[0]["Total_no_of_class"].ToString())) * 100;
                            returN = dt.Rows[0]["Total_no_of_class"].ToString() + "月" + dt.Rows[0]["No_of_class_Attendance"].ToString() + "月" + att_persent.ToString("0.00");
                        }
                    }
                }
                catch (Exception ex)
                {
                    string query = "select *,(select top 1 No_of_class_Attendance from Exam_Term_Wise_Attendance where Session_Id=td.Session_Id and Branch_Id=td.Branch_Id and Class_id=td.Class_id and Exam_Term_Id=td.Exam_Term_Id and Admission_no='" + admission_no + "') as Persent_attandance from Exam_Term_Details td where Session_Id=" + session_id + " and Branch_Id='" + branch_id + "' and Class_id=" + class_id + " and Exam_Term_Id=" + term_id + "";
                    DataTable dtxx = My.dataTable(query);
                    if (dtxx.Rows.Count == 0)
                    {
                        return returN;
                    }
                    else
                    {
                        if (dtxx.Rows[0]["Persent_attandance"].ToString() == "")
                        {
                            returN = dtxx.Rows[0]["No_of_Class"].ToString() + "月0月0";
                        }
                        else
                        {
                            double att_persent = (My.toDouble(dtxx.Rows[0]["Persent_attandance"].ToString()) / My.toDouble(dtxx.Rows[0]["No_of_Class"].ToString())) * 100;
                            returN = dtxx.Rows[0]["No_of_Class"].ToString() + "月" + dtxx.Rows[0]["Persent_attandance"].ToString() + "月" + att_persent.ToString("0.00");
                        }
                        return returN;
                    }
                }

                return returN;
            }
        }

        private List<MySubjectMarks> findmySubjMarks(string Session_id, string Class_id, string Term_id, string Section, string Admission_no, string Branch_id)
        {
            List<MySubjectMarks> MySubjectMarksItem = new List<MySubjectMarks>();
            string qry = "select Assessment_Name,Short_Name,Assessment_Id,Maximum_Marks,(select top 1 Maximum_Marks from Exam_Term_Details where Session_Id=Exam_Assessment_Details.Session_Id and Branch_Id=Exam_Assessment_Details.Branch_Id and Exam_Term_Id=Exam_Assessment_Details.Exam_Term_Id) as Term_Maximum_Marks from Exam_Assessment_Details where Session_Id='" + Session_id + "' and  Class_id='" + Class_id + "' and Exam_Term_Id=" + Term_id + "  and Scholastic_Co_scholastic='Scholastic' order by Sequence_No asc";
            SqlDataAdapter ad = new SqlDataAdapter(qry, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Complain_chat");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string qrySubJ = "select * from Subject_Master where course_id='" + Class_id + "' and Branch_id='" + Branch_id + "' and Subject_Type_Scholastic_Co_Scholastic='Scholastic' and Is_mandatory='1' order by Subject_position asc";
                    DataTable dtSubJ = My.dataTable(qrySubJ);
                    if (dtSubJ.Rows.Count > 0)
                    {
                        foreach (DataRow drS in dtSubJ.Rows)
                        {
                            string qrys = "select * from Exam_Subject_Sub_Level where Session_Id='" + Session_id + "' and Branch_Id='" + Branch_id + "' and Exam_Term_Id='" + Term_id + "' and Assessment_Id='" + dt.Rows[i]["Assessment_Id"].ToString() + "' and Class_id='" + Class_id + "' and Subject_id='" + drS["Subject_id"].ToString() + "' and Istatus=1  order by Sequence_No asc";
                            DataTable dtS = My.dataTable(qrys);
                            if (dtS.Rows.Count.ToString() == "0")
                            {
                                MySubjectMarksItem.Add(new MySubjectMarks
                                {
                                    Marks = "-",
                                });

                                //MySubjectMarksItem.Add(new MySubjectMarks
                                //{
                                //    Marks = "-",
                                //});
                            }
                            else if (dtS.Rows.Count.ToString() == "1")
                            {
                                string marks = tabulationSheet.get_marks_obt(Session_id, Class_id, Admission_no, Term_id, dt.Rows[i]["Assessment_Id"].ToString(), drS["Subject_id"].ToString(), Branch_id, "Scholastic", dtS.Rows[0]["Subject_Sub_Level_Id"].ToString());
                                string[] stringSeparatorss = new string[] { "/" };
                                string[] arrs = marks.Split(stringSeparatorss, StringSplitOptions.None);
                                string obt_marks = arrs[0];
                                string full_marks = arrs[1];
                                string marks_type = arrs[2];
                                string total_no = arrs[3];


                                if (obt_marks == "" || obt_marks == "0")
                                {
                                    obt_marks = "-";
                                }
                                else
                                {
                                    save_marks(Session_id, Class_id, Admission_no, Term_id, dt.Rows[i]["Assessment_Id"].ToString(), drS["Subject_id"].ToString(), Branch_id, obt_marks, full_marks, marks_type, total_no, dtS.Rows[0]["Subject_Sub_Level_Id"].ToString());
                                }
                                MySubjectMarksItem.Add(new MySubjectMarks
                                {
                                    Marks = obt_marks,
                                });

                                //MySubjectMarksItem.Add(new MySubjectMarks
                                //{
                                //    Marks = "-",
                                //});
                            }
                            else
                            {
                                string marks = tabulationSheet.get_marks_obt(Session_id, Class_id, Admission_no, Term_id, dt.Rows[i]["Assessment_Id"].ToString(), drS["Subject_id"].ToString(), Branch_id, "Scholastic", dtS.Rows[0]["Subject_Sub_Level_Id"].ToString());
                                string[] stringSeparatorss = new string[] { "/" };
                                string[] arrs = marks.Split(stringSeparatorss, StringSplitOptions.None);
                                string obt_marks = arrs[0];
                                string full_marks = arrs[1];
                                string marks_type = arrs[2];
                                string total_no = arrs[3];


                                if (obt_marks == "" || obt_marks == "0")
                                {
                                    obt_marks = "-";
                                }
                                else
                                {
                                    save_marks(Session_id, Class_id, Admission_no, Term_id, dt.Rows[i]["Assessment_Id"].ToString(), drS["Subject_id"].ToString(), Branch_id, obt_marks, full_marks, marks_type, total_no, dtS.Rows[0]["Subject_Sub_Level_Id"].ToString());
                                }
                                MySubjectMarksItem.Add(new MySubjectMarks
                                {
                                    Marks = obt_marks,
                                });



                                //=======================PR
                                //string marksPR = tabulationSheet.get_marks_obt(Session_id, Class_id, Admission_no, Term_id, dt.Rows[i]["Assessment_Id"].ToString(), drS["Subject_id"].ToString(), Branch_id, "Scholastic", dtS.Rows[1]["Subject_Sub_Level_Id"].ToString());
                                //string[] stringSeparatorssPR = new string[] { "/" };
                                //string[] arrsPR = marksPR.Split(stringSeparatorss, StringSplitOptions.None);
                                //string obt_marksPR = arrsPR[0];
                                //string full_marksPR = arrsPR[1];
                                //string marks_typePR = arrsPR[2];
                                //string total_noPR = arrsPR[3];


                                //if (obt_marksPR == "" || obt_marksPR == "0")
                                //{
                                //    obt_marksPR = "-";
                                //}
                                //else
                                //{
                                //    save_marks(Session_id, Class_id, Admission_no, Term_id, dt.Rows[i]["Assessment_Id"].ToString(), drS["Subject_id"].ToString(), Branch_id, obt_marksPR, full_marksPR, marks_typePR, total_noPR, dtS.Rows[1]["Subject_Sub_Level_Id"].ToString());
                                //}
                                //MySubjectMarksItem.Add(new MySubjectMarks
                                //{
                                //    Marks = obt_marksPR,
                                //});
                            }

                        }
                    }
                }
            }
            return MySubjectMarksItem;
        }

        private List<MySubjectMarksCS> findmySubjMarksCS(string Session_id, string Class_id, string Term_id, string Section, string Admission_no, string Branch_id)
        {
            List<MySubjectMarksCS> MySubjectMarksItemCS = new List<MySubjectMarksCS>();

            string qry = "select DISTINCT t1.Session_id,t1.Class_id,t1.Term,t1.Subject,t1.Branch_id,t2.Subject_name,t2.Subject_position,t1.Assessment from Exam_marks t1 join Subject_Master t2 on t1.Subject=t2.Subject_id where t1.Session_id=" + Session_id + " and t1.Class_id=" + Class_id + " and t1.Branch_id='" + Branch_id + "' and t1.Term=" + Term_id + " and t2.Subject_Type_Scholastic_Co_Scholastic='Co-Scholastic' and t1.Assessment in(select Assessment_Id from Exam_Assessment_Details where Session_Id=t1.Session_Id and Exam_Term_Id=t1.Term and Class_id=t1.Class_id) order by t2.Subject_position asc";
            SqlDataAdapter ad = new SqlDataAdapter(qry, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Subject_Master");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string marks = tabulationSheet.get_marks(Session_id, Class_id, Admission_no, Term_id, dr["Assessment"].ToString(), dr["Subject"].ToString(), dr["Branch_id"].ToString(), "Co-Scholastic");
                    string[] stringSeparatorss = new string[] { "/" };
                    string[] arrs = marks.Split(stringSeparatorss, StringSplitOptions.None);
                    string obt_marks = arrs[0];
                    string full_marks = arrs[1];


                    if (obt_marks == "" || obt_marks == "0")
                    {
                        obt_marks = "-";
                    }
                    MySubjectMarksItemCS.Add(new MySubjectMarksCS
                    {
                        Marks = obt_marks,
                    });
                }
            }


            return MySubjectMarksItemCS;
        }

        private List<MySubjectMarksDesc> findmySubjMarksDesc(string Session_id, string Class_id, string Term_id, string Section, string Admission_no, string Branch_id)
        {
            List<MySubjectMarksDesc> MySubjectMarksItemDesc = new List<MySubjectMarksDesc>();

            string qry = "select ptt.*,pt.Activity_name from Exam_Personality_Traits_Term_Wise ptt join Exam_Personality_Traits pt on ptt.Activity_Id=pt.Activity_Id where ptt.Session_id=" + Session_id + " and ptt.Branch_id='" + Branch_id + "' and ptt.Class_id=" + Class_id + " and ptt.Exam_Term_Id=" + Term_id + " and ptt.Admission_no='" + Admission_no + "'  order by pt.Position asc";
            SqlDataAdapter ad = new SqlDataAdapter(qry, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Subject_Master");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string grade = "";
                    bool isNum = valid_amount(dr["Term_grade"].ToString());
                    if (isNum == true)
                    {
                        grade = tabulationSheet.get_personality_grade(Session_id, Class_id, Branch_id, dr["Term_grade"].ToString());
                    }
                    else
                    {
                        grade = dr["Term_grade"].ToString();
                    }
                    MySubjectMarksItemDesc.Add(new MySubjectMarksDesc
                    {
                        Marks = grade,
                    });
                }
            }
            return MySubjectMarksItemDesc;
        }

        private List<MySubjectTotalMarksOvrALL> findmySubjTotalMarksOvrALL(string Session_id, string Class_id, string Term_id, string Section, string Admission_no, string Branch_id)
        {
            List<MySubjectTotalMarksOvrALL> MySubjectTotalMarksItemOvrALL = new List<MySubjectTotalMarksOvrALL>();
            string qry = "select isnull(sum(convert(float, Full_marks)),0) as Full_marks,isnull(sum(convert(float, Marks)),0) as Marks_obt from EXAM_TEMP_TABULATION_TOTAL_NO where Session_id='" + Session_id + "' and Class_id='" + Class_id + "' and Term_id='" + Term_id + "' and Admission_no='" + Admission_no + "'";
            DataTable dt = My.dataTable(qry);
            if (dt.Rows.Count > 0)
            {
                string full_marks = dt.Rows[0]["Full_marks"].ToString();
                string Marks_obt = dt.Rows[0]["Marks_obt"].ToString();
                double overall_percentage = (My.toDouble(dt.Rows[0]["Marks_obt"].ToString()) / My.toDouble(dt.Rows[0]["Full_marks"].ToString())) * 100;

                double roundPrc = Math.Round(My.toDouble(overall_percentage));
                string finl_perc = overall_percentage.ToString("0.00");
                if (roundPrc == My.toDouble(overall_percentage))
                {
                    finl_perc = My.toDouble(overall_percentage).ToString();
                }

                MySubjectTotalMarksItemOvrALL.Add(new MySubjectTotalMarksOvrALL
                {
                    Overall_full_mark = full_marks,
                    Overall_full_obtained_mark = Marks_obt,
                    Overall_full_persentage_mark = finl_perc,
                });

            }
            return MySubjectTotalMarksItemOvrALL;
        }


        private List<MySubjectTotalMarks> findmySubjTotalMarks(string Session_id, string Class_id, string Term_id, string Section, string Admission_no, string Branch_id)
        {
            List<MySubjectTotalMarks> MySubjectTotalMarksItem = new List<MySubjectTotalMarks>();
            string qrySubJ = "select * from Subject_Master where course_id='" + Class_id + "'  and Subject_Type_Scholastic_Co_Scholastic='Scholastic' and Is_mandatory=1 order by Subject_position asc";
            DataTable dtSubJ = My.dataTable(qrySubJ);
            if (dtSubJ.Rows.Count > 0)
            {
                foreach (DataRow drS in dtSubJ.Rows)
                {
                    string Total_marks = tabulationSheet.get_subject_total_marks(Session_id, Class_id, Admission_no, Term_id, drS["Subject_id"].ToString(), Branch_id, "Scholastic");
                    string[] stringSeparatorss = new string[] { "/" };
                    string[] arrs = Total_marks.Split(stringSeparatorss, StringSplitOptions.None);
                    string total_subject_marks = arrs[0];
                    string subject_grade = arrs[1];
                    string full_number = arrs[2];

                    if (total_subject_marks == "" || total_subject_marks == "0")
                    {
                        total_subject_marks = "-";
                    }
                    MySubjectTotalMarksItem.Add(new MySubjectTotalMarks
                    {
                        Total_subject_marks = total_subject_marks,
                    });
                }
            }


            return MySubjectTotalMarksItem;
        }
        private List<MySubjectTotalGrade> findmySubjTotalGrade(string Session_id, string Class_id, string Term_id, string Section, string Admission_no, string Branch_id)
        {
            List<MySubjectTotalGrade> MySubjectTotalGradeItem = new List<MySubjectTotalGrade>();
            string qrySubJ = "select * from  Subject_Master where course_id='" + Class_id + "' and Subject_Type_Scholastic_Co_Scholastic='Scholastic' and Is_mandatory=1 order by Subject_position asc";
            DataTable dtSubJ = My.dataTable(qrySubJ);
            if (dtSubJ.Rows.Count > 0)
            {
                foreach (DataRow drS in dtSubJ.Rows)
                {
                    string Total_marks = tabulationSheet.get_subject_total_marks(Session_id, Class_id, Admission_no, Term_id, drS["Subject_id"].ToString(), Branch_id, "Scholastic");
                    string[] stringSeparatorss = new string[] { "/" };
                    string[] arrs = Total_marks.Split(stringSeparatorss, StringSplitOptions.None);
                    string total_subject_marks = arrs[0];
                    string subject_grade = arrs[1];
                    string full_number = arrs[2];

                    if (total_subject_marks == "" || total_subject_marks == "0")
                    {
                        subject_grade = "-";
                    }
                    MySubjectTotalGradeItem.Add(new MySubjectTotalGrade
                    {
                        subject_grade = subject_grade,
                    });
                }
            }


            return MySubjectTotalGradeItem;
        }

        private void save_marks(string session_id, string class_id, string admission_no, string term_id, string assesment_id, string subject_id, string Branch_id, string marks, string full_marks, string marks_type, string total_no, string Subject_Sub_Level_Id)
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
                bool tot1 = mycode.cheknumer_Double(total_no);
                if (tot1 == false)
                {
                    total_no = "0";
                }
            }
            catch
            {
            }



            if (mycode.IsUserExist("select Id from Exam_temp_tabulation_total_no where Session_id='" + session_id + "' and Branch_id='" + Branch_id + "' and Class_id='" + class_id + "' and Term_id='" + term_id + "' and Assessment_id='" + assesment_id + "' and Subject_id='" + subject_id + "' and Admission_no='" + admission_no + "' and Subject_sub_level_id='" + Subject_Sub_Level_Id + "'"))
            {
                SqlCommand cmd;
                string query = "INSERT INTO Exam_temp_tabulation_total_no (Session_id,Branch_id,Class_id,Term_id,Assessment_id,Subject_id,Admission_no,Marks,Full_marks,Subject_sub_level_id) values (@Session_id,@Branch_id,@Class_id,@Term_id,@Assessment_id,@Subject_id,@Admission_no,@Marks,@Full_marks,@Subject_sub_level_id)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Session_id", session_id);
                cmd.Parameters.AddWithValue("@Branch_id", Branch_id);
                cmd.Parameters.AddWithValue("@Class_id", class_id);
                cmd.Parameters.AddWithValue("@Term_id", term_id);
                cmd.Parameters.AddWithValue("@Assessment_id", assesment_id);
                cmd.Parameters.AddWithValue("@Subject_id", subject_id);
                cmd.Parameters.AddWithValue("@Admission_no", admission_no);
                if (marks_type == "Marks")
                {
                    cmd.Parameters.AddWithValue("@Marks", marks);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Marks", total_no);
                }
                cmd.Parameters.AddWithValue("@Full_marks", full_marks);
                cmd.Parameters.AddWithValue("@Subject_sub_level_id", Subject_Sub_Level_Id);
                if (My.InsertUpdateData(cmd))
                {
                }
            }
            else
            {
                SqlCommand cmd;
                string query = "Update Exam_temp_tabulation_total_no set Marks=@Marks,Full_marks=@Full_marks where Session_id='" + session_id + "' and Branch_id='" + Branch_id + "' and Class_id='" + class_id + "' and Term_id='" + term_id + "' and Assessment_id='" + assesment_id + "' and Subject_id='" + subject_id + "' and Admission_no='" + admission_no + "'";
                cmd = new SqlCommand(query);
                if (marks_type == "Marks")
                {
                    cmd.Parameters.AddWithValue("@Marks", marks);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Marks", total_no);
                }
                cmd.Parameters.AddWithValue("@Full_marks", full_marks);
                if (My.InsertUpdateData(cmd))
                {
                }
            }



            //====================================================
            //string first_term = Exam_setting.get_first_term(session_id, Branch_id, class_id);
            //if (first_term == term_id)
            //{
            //    if (mycode.IsUserExist("select Id from Exam_temp_assesment_total_no where Session_id='" + session_id + "' and Branch_id='" + Branch_id + "' and Class_id='" + class_id + "' and Term_id='" + term_id + "' and Assessment_id='" + assesment_id + "' and Subject_id='" + subject_id + "' and Admission_no='" + admission_no + "'"))
            //    {
            //        SqlCommand cmd;
            //        string query = "INSERT INTO Exam_temp_assesment_total_no (Session_id,Branch_id,Class_id,Term_id,Assessment_id,Subject_id,Admission_no,Marks,Full_marks) values (@Session_id,@Branch_id,@Class_id,@Term_id,@Assessment_id,@Subject_id,@Admission_no,@Marks,@Full_marks)";
            //        cmd = new SqlCommand(query);
            //        cmd.Parameters.AddWithValue("@Session_id", session_id);
            //        cmd.Parameters.AddWithValue("@Branch_id", Branch_id);
            //        cmd.Parameters.AddWithValue("@Class_id", class_id);
            //        cmd.Parameters.AddWithValue("@Term_id", term_id);
            //        cmd.Parameters.AddWithValue("@Assessment_id", assesment_id);
            //        cmd.Parameters.AddWithValue("@Subject_id", subject_id);
            //        cmd.Parameters.AddWithValue("@Admission_no", admission_no);
            //        if (marks_type == "Marks")
            //        {
            //            cmd.Parameters.AddWithValue("@Marks", marks);
            //        }
            //        else
            //        {
            //            cmd.Parameters.AddWithValue("@Marks", total_no);
            //        }
            //        cmd.Parameters.AddWithValue("@Full_marks", full_marks);
            //        if (My.InsertUpdateData(cmd))
            //        {
            //        }
            //    }
            //    else
            //    {
            //        SqlCommand cmd;
            //        string query = "Update Exam_temp_assesment_total_no set Marks=@Marks,Full_marks=@Full_marks where Session_id='" + session_id + "' and Branch_id='" + Branch_id + "' and Class_id='" + class_id + "' and Term_id='" + term_id + "' and Assessment_id='" + assesment_id + "' and Subject_id='" + subject_id + "' and Admission_no='" + admission_no + "'";
            //        cmd = new SqlCommand(query);
            //        if (marks_type == "Marks")
            //        {
            //            cmd.Parameters.AddWithValue("@Marks", marks);
            //        }
            //        else
            //        {
            //            cmd.Parameters.AddWithValue("@Marks", total_no);
            //        }
            //        cmd.Parameters.AddWithValue("@Full_marks", full_marks);
            //        if (My.InsertUpdateData(cmd))
            //        {
            //        }
            //    }
            //}
            //else
            //{
            //    if (mycode.IsUserExist("select Id from Exam_temp_assesment_total_no_term_II where Session_id='" + session_id + "' and Branch_id='" + Branch_id + "' and Class_id='" + class_id + "' and Term_id='" + term_id + "' and Assessment_id='" + assesment_id + "' and Subject_id='" + subject_id + "' and Admission_no='" + admission_no + "'"))
            //    {
            //        SqlCommand cmd;
            //        string query = "INSERT INTO Exam_temp_assesment_total_no_term_II (Session_id,Branch_id,Class_id,Term_id,Assessment_id,Subject_id,Admission_no,Marks,Full_marks) values (@Session_id,@Branch_id,@Class_id,@Term_id,@Assessment_id,@Subject_id,@Admission_no,@Marks,@Full_marks)";
            //        cmd = new SqlCommand(query);
            //        cmd.Parameters.AddWithValue("@Session_id", session_id);
            //        cmd.Parameters.AddWithValue("@Branch_id", Branch_id);
            //        cmd.Parameters.AddWithValue("@Class_id", class_id);
            //        cmd.Parameters.AddWithValue("@Term_id", term_id);
            //        cmd.Parameters.AddWithValue("@Assessment_id", assesment_id);
            //        cmd.Parameters.AddWithValue("@Subject_id", subject_id);
            //        cmd.Parameters.AddWithValue("@Admission_no", admission_no);
            //        if (marks_type == "Marks")
            //        {
            //            cmd.Parameters.AddWithValue("@Marks", marks);
            //        }
            //        else
            //        {
            //            cmd.Parameters.AddWithValue("@Marks", total_no);
            //        }
            //        cmd.Parameters.AddWithValue("@Full_marks", full_marks);
            //        if (My.InsertUpdateData(cmd))
            //        {
            //        }
            //    }
            //    else
            //    {
            //        SqlCommand cmd;
            //        string query = "Update Exam_temp_assesment_total_no_term_II set Marks=@Marks,Full_marks=@Full_marks where Session_id='" + session_id + "' and Branch_id='" + Branch_id + "' and Class_id='" + class_id + "' and Term_id='" + term_id + "' and Assessment_id='" + assesment_id + "' and Subject_id='" + subject_id + "' and Admission_no='" + admission_no + "'";
            //        cmd = new SqlCommand(query);
            //        if (marks_type == "Marks")
            //        {
            //            cmd.Parameters.AddWithValue("@Marks", marks);
            //        }
            //        else
            //        {
            //            cmd.Parameters.AddWithValue("@Marks", total_no);
            //        }
            //        cmd.Parameters.AddWithValue("@Full_marks", full_marks);
            //        if (My.InsertUpdateData(cmd))
            //        {
            //        }
            //    }
            //}
        }

        ////=================================================
        /// <summary>
        /// 
        /// 
        /// </summary>
        /// 


        public class Fetch_Details_of_report_coScholastic_G
        {
            public string FMG { get; set; }
            public string Bg_colors { get; set; }
        }

        List<Fetch_Details_of_report_coScholastic_G> Show_of_report_coScholastic_G = new List<Fetch_Details_of_report_coScholastic_G>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_tabulation_coscholastic_G(string Session_id, string Class_id, string Term_id, string Section, string Branch_id)
        {
            string qry = "select DISTINCT t1.Session_id,t1.Class_id,t1.Term,t1.Subject,t1.Branch_id,t2.Subject_name,t2.Subject_position,t1.Assessment from Exam_marks t1 join Subject_Master t2 on t1.Subject=t2.Subject_id where t1.Session_id=" + Session_id + " and t1.Class_id=" + Class_id + " and t1.Branch_id='" + Branch_id + "' and t1.Term=" + Term_id + " and t2.Subject_Type_Scholastic_Co_Scholastic='Co-Scholastic' order by t2.Subject_position asc";
            SqlDataAdapter ad = new SqlDataAdapter(qry, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Subject_Master");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Show_of_report_coScholastic_G.Add(new Fetch_Details_of_report_coScholastic_G
                    {
                        FMG = "G",
                        Bg_colors = "thbg15",
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_report_coScholastic_G));
            }
        }

        public class Fetch_Details_of_report_coScholastic
        {
            public string Subject_name { get; set; }
            public string Subject_code { get; set; }
            public string RowCounTS { get; set; }
            public string Bg_colors { get; set; }
        }

        List<Fetch_Details_of_report_coScholastic> Show_of_report_coScholastic = new List<Fetch_Details_of_report_coScholastic>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_tabulation_coscholastic(string Session_id, string Class_id, string Term_id, string Section, string Branch_id)
        {
            string qry = "select DISTINCT t1.Session_id,t1.Class_id,t1.Term,t1.Subject,t1.Branch_id,t2.Subject_name,t2.Subject_position,t1.Assessment from Exam_marks t1 join Subject_Master t2 on t1.Subject=t2.Subject_id where t1.Session_id=" + Session_id + " and t1.Class_id=" + Class_id + " and t1.Branch_id='" + Branch_id + "' and t1.Term=" + Term_id + " and t2.Subject_Type_Scholastic_Co_Scholastic='Co-Scholastic' order by t2.Subject_position asc";
            SqlDataAdapter ad = new SqlDataAdapter(qry, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Subject_Master");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Show_of_report_coScholastic.Add(new Fetch_Details_of_report_coScholastic
                    {
                        Subject_name = dt.Rows[i]["Subject_name"].ToString(),
                        Subject_code = dt.Rows[i]["Subject"].ToString(),
                        RowCounTS = dt.Rows.Count.ToString(),
                        Bg_colors = "thbg15",
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_report_coScholastic));
            }
        }


        ////=================================================
        /// <summary>
        /// 
        /// 
        /// </summary>
        /// 

        public class Fetch_Details_of_report_descpline_G
        {
            public string FMG { get; set; }
            public string Bg_colors { get; set; }
        }

        List<Fetch_Details_of_report_descpline_G> Show_of_report_descpline_G = new List<Fetch_Details_of_report_descpline_G>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_tabulation_coscholastic_DESC(string Session_id, string Class_id, string Term_id, string Section, string Branch_id)
        {
            string qry = "select DISTINCT ptt.Activity_Id,pt.Activity_name,pt.Position from Exam_Personality_Traits_Term_Wise ptt join Exam_Personality_Traits pt on ptt.Activity_Id=pt.Activity_Id where ptt.Session_id=" + Session_id + " and ptt.Branch_id='" + Branch_id + "' and ptt.Class_id=" + Class_id + " and ptt.Exam_Term_Id=" + Term_id + " order by pt.Position asc";
            SqlDataAdapter ad = new SqlDataAdapter(qry, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Subject_Master");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Show_of_report_descpline_G.Add(new Fetch_Details_of_report_descpline_G
                    {
                        FMG = "G",
                        Bg_colors = "thbg16",
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_report_descpline_G));
            }
        }


        public class Fetch_Details_of_report_descpline
        {
            public string Subject_name { get; set; }
            public string Subject_code { get; set; }
            public string RowCounTS { get; set; }
            public string Bg_colors { get; set; }
        }

        List<Fetch_Details_of_report_descpline> Show_of_report_descpline = new List<Fetch_Details_of_report_descpline>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_tabulation_Descpline(string Session_id, string Class_id, string Term_id, string Section, string Branch_id)
        {
            string qry = "select DISTINCT ptt.Activity_Id,pt.Activity_name,pt.Position from Exam_Personality_Traits_Term_Wise ptt join Exam_Personality_Traits pt on ptt.Activity_Id=pt.Activity_Id where ptt.Session_id=" + Session_id + " and ptt.Branch_id='" + Branch_id + "' and ptt.Class_id=" + Class_id + " and ptt.Exam_Term_Id=" + Term_id + " order by pt.Position asc";
            SqlDataAdapter ad = new SqlDataAdapter(qry, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Subject_Master");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Show_of_report_descpline.Add(new Fetch_Details_of_report_descpline
                    {
                        Subject_name = dt.Rows[i]["Activity_name"].ToString(),
                        Subject_code = dt.Rows[i]["Activity_Id"].ToString(),
                        RowCounTS = dt.Rows.Count.ToString(),
                        Bg_colors = "thbg16",
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_of_report_descpline));
            }
        }


        private static bool valid_amount(string p)
        {
            try
            {
                Convert.ToDouble(p);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
