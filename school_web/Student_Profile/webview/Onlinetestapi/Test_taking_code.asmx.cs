using school_web.AppCode;
using school_web.AppCode.Onlineexam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace school_web.Student_Profile.webview.Onlinetestapi
{
    /// <summary>
    /// Summary description for Test_taking_code
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Test_taking_code : System.Web.Services.WebService
    {
        Find_and_execute_code fec = new Find_and_execute_code();

        public class All_sections
        {
            public string id { get; set; }
            public string sec_name { get; set; }

            public List<questiions> ques { get; set; }

        }
        public class questiions
        {
            public string qid { get; set; }
            public string question { get; set; }
            public string answer { get; set; }
            public string marks { get; set; }
            public string id { get; set; }
            public string Direction { get; set; }
            public string Di { get; set; }
            public string cnt_type { get; set; }

            public string rcount { get; set; }

            public string Direction_HN { get; set; }
            public string Question_name_HN { get; set; }
            public string answer_HN { get; set; }

            public string DI_HN { get; set; }
            public string Question_no { get; set; }

            public string Language_Itype { get; set; }
            public string negative_mrk { get; set; }

            public List<answers> answers { get; set; }

        }
        public class answers
        {
            public string opt_id { get; set; }
            public string opt1 { get; set; }

            public string opt_id_hn { get; set; }
            public string opt1_hn { get; set; }
        }
        List<All_sections> ALLS = new List<All_sections>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string bind_top_heading_tabbing(string testid)
        {
            string qry = @"select t1.id,t1.Test_id,t1.Section_name as 'Section_name',t1.Test_id as Section_id from Section_Arranging t1   where t1.Test_id='" + testid + "'  order by t1.Position asc";


            DataTable dt = new DataTable();
            // dt = fec.featch_data("select id,Test_id,Section_name from Section_Arranging where Test_id='" + testid + "' order by Position asc");
            dt = fec.featch_data(qry);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    List<questiions> qs = question_list(dr["Test_id"].ToString(), dr["Section_id"].ToString());
                    ALLS.Add(new All_sections
                    {
                        id = dr["id"].ToString(),
                        sec_name = dr["Section_name"].ToString(),
                        ques = qs
                    });
                }
            }
            return new JavaScriptSerializer().Serialize(ALLS);
        }

        private List<questiions> question_list(string testid, string section)
        {
            List<questiions> qs = new List<questiions>();
            DataTable dt = new DataTable();

            string query;
            //DataTable dtType = fec.featch_data("select TimeType from Test_info Where test_id = " + test_id);
            string test_type = "Sequence"; /*dtType.Rows[0][0].ToString();*/

            //and Section = '" + section + "'

            if (test_type == "" || test_type == "Sequence")
            {
                query = "select *,( select top 1 Negative_Marking from dbo.[Add_Exam_Category]) as Negative_Marking from question_info where test_id='" + testid + "'  order by cast(Question_no as int) asc";
            }
            else
            {
                query = "select *,(select top 1 Negative_Marking from dbo.[Add_Exam_Category]) as Negative_Marking from question_info where test_id='" + testid + "'   order by NEWID()";
            }
            dt = fec.featch_data(query);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    List<answers> ans = find_answers(testid, section, dr["questionid"].ToString());
                    qs.Add(new questiions
                    {
                        qid = dr["questionid"].ToString(),
                        question = dr["Question_name"].ToString(),
                        Direction = dr["Direction"].ToString(),
                        answer = dr["ans"].ToString(),
                        marks = dr["marks"].ToString(),
                        id = dr["id"].ToString(),
                        cnt_type = dr["Type"].ToString(),
                        Di = dr["DI"].ToString(),

                        answers = ans,
                        rcount = dt.Rows.Count.ToString(),

                        Direction_HN = dr["Direction_HN"].ToString(),
                        Question_name_HN = dr["Question_name_HN"].ToString(),
                        answer_HN = dr["ans_HN"].ToString(),
                        DI_HN = dr["DI_HN"].ToString(),
                        Question_no = dr["Question_no"].ToString(),
                        Language_Itype = dr["Language_Itype"].ToString(),
                        negative_mrk = dr["Negative_Marking"].ToString(),
                    });
                }
            }
            return qs;
        }

        private List<answers> find_answers(string testid, string section, string quid)
        {
            List<answers> ans = new List<answers>();
            DataTable dt = new DataTable();
            dt = fec.featch_data("select opt_code,option_text,opetion_text_HN from question_answer_Master where test_code='" + testid + "' and Section='" + section + "'  and quest_code='" + quid + "'");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ans.Add(new answers
                    {
                        opt_id = dr["opt_code"].ToString(),
                        opt1 = dr["option_text"].ToString(),
                        opt_id_hn = dr["opt_code"].ToString(),
                        opt1_hn = dr["opetion_text_HN"].ToString()


                    });
                }
            }
            return ans;
        }


        My m1 = new My();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string save_data(string entryid, string packageid, string Section, string testid, string studentid, string sqid, string sansid, string tme, string testno, string examcode, string negmark, string negmarktype, string language, string attemptid, string asnitime)
        {
            string toreturn = "";
            string marks = "0";


            string ip = m1.find_ip();
            DataTable dt = new DataTable();
            //dt = m1.featch_data("select ans,marks,Opetion_id,Language_Itype,ans_HN  where  test_id='" + testid + "' and  questionid='" + s_qid + "' ");
            dt = m1.featch_data("select qam.option_text,qtm.ans,qtm.marks,qam.opetion_text_HN,qtm.ans_HN,qtm.Language_Itype,qtm.Opetion_id  from question_answer_Master qam join question_info qtm on qam.quest_code=qtm.questionid where qam.Section= (select Test_id from Section_Arranging where Id='" + Section + "' )  and qam.test_code='" + testid + "' and qam.quest_code='" + sqid + "' and qam.opt_code='" + sansid + "'");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string option_text = "";
                    string answer = "";
                    string Language_Itype = "";
                    //if (language == "0")
                    //{
                    Language_Itype = "0";
                    option_text = dr["option_text"].ToString();
                    answer = dr["ans"].ToString();
                    //}
                    //else
                    //{
                    //    if (dr["Language_Itype"].ToString() == "1")
                    //    {
                    //        if (dr["opetion_text_HN"].ToString() == "")
                    //        {
                    //            Language_Itype = "0";
                    //            option_text = dr["option_text"].ToString();
                    //            answer = dr["ans"].ToString();

                    //        }
                    //        else
                    //        {
                    //            Language_Itype = "1";
                    //            option_text = dr["opetion_text_HN"].ToString();
                    //            answer = dr["ans_HN"].ToString();
                    //        }
                    //    }
                    //    else
                    //    {
                    //        Language_Itype = "0";
                    //        option_text = dr["option_text"].ToString();
                    //        answer = dr["ans"].ToString();
                    //    }
                    //}

                    if (sansid.Trim() == dr["Opetion_id"].ToString())
                    {
                        marks = dr["marks"].ToString();
                    }
                    else
                    {
                        if (negmarktype == "Percentage")
                        {
                            marks = "-" + ((Convert.ToDouble(dr["marks"].ToString()) * Convert.ToDouble(negmark) / 100)).ToString();
                        }
                        if (negmarktype == "%")
                        {
                            marks = "-" + ((Convert.ToDouble(dr["marks"].ToString()) * Convert.ToDouble(negmark) / 100)).ToString();
                        }
                        else
                        {
                            marks = "-" + ((Convert.ToDouble(negmark))).ToString();
                        }

                    }

                    #region insert_and_update_user_test_details
                    // Ip_address ='" + ip + "' and
                    // SqlDataAdapter ad1 = new SqlDataAdapter("select * from user_test_details where  test_code='" + testid + "' and Section='" + Section + "' and Studentid='" + studentid + "' and quest_code='" + s_qid + "' and Attempt_id='" + attempt_id + "' and Packageid='" + package_id + "' and Entry_no='" + entry_id + "'   ", My.conn);
                    SqlDataAdapter ad1 = new SqlDataAdapter("select * from user_test_details where  test_code='" + testid + "' and Section='" + Section + "' and Studentid='" + studentid + "' and quest_code='" + sqid + "'  and Packageid='" + packageid + "' and Entry_no='" + entryid + "'   ", My.conn);
                    DataSet ds1 = new DataSet();
                    ad1.Fill(ds1);
                    DataTable dt1 = ds1.Tables[0];

                    int rowcount = dt1.Rows.Count;
                    if (rowcount == 0)
                    {

                        double minute = Math.Floor(double.Parse(tme) / 60);
                        double second = double.Parse(tme) - minute * 60;

                        string time_d = ((minute < 10 ? "0" : "") + minute + ":" + (second < 10 ? "0" : "") + second).ToString();

                        DataRow dr1 = dt1.NewRow();
                        dr1["Ip_address"] = ip;
                        dr1["test_code"] = testid;
                        dr1["quest_code"] = sqid;
                        dr1["opt_code"] = sansid;
                        dr1["opt_text"] = option_text.Trim();
                        dr1["answer"] = answer.Trim();
                        dr1["marks"] = marks;

                        dr1["taken_time"] = tme;
                        dr1["taken_time_d"] = time_d;
                        dr1["created_date"] = m1.date();
                        dr1["icreated_date"] = m1.idate();
                        dr1["Status"] = "Running";
                        dr1["Section"] = Section;
                        dr1["Studentid"] = studentid;

                        dr1["Exam_code"] = examcode;
                        dr1["neg_mark"] = negmark;
                        dr1["neg_mark_type"] = negmarktype;
                        dr1["Language_Itype"] = Language_Itype;
                        dr1["Attempt_id"] = attemptid;
                        dr1["itime"] = asnitime;

                        dr1["Packageid"] = packageid;
                        dr1["Entry_no"] = entryid;

                        dt1.Rows.Add(dr1);
                        SqlCommandBuilder cb = new SqlCommandBuilder(ad1);
                        ad1.Update(dt1);
                        toreturn = "0";
                    }
                    else
                    {
                        foreach (DataRow dr1 in dt1.Rows)
                        {

                            double tottime = Convert.ToDouble(dr1["taken_time"].ToString()) + Convert.ToDouble(tme);
                            double minute = Math.Floor(tottime / 60);
                            double second = tottime - minute * 60;

                            string time_d = ((minute < 10 ? "0" : "") + minute + ":" + (second < 10 ? "0" : "") + second).ToString();


                            dr1["opt_code"] = sansid;
                            dr1["opt_text"] = option_text;

                            dr1["taken_time"] = tottime;
                            dr1["taken_time_d"] = time_d;

                            dr1["marks"] = marks;
                            dr1["itime"] = asnitime;

                            SqlCommandBuilder cb = new SqlCommandBuilder(ad1);
                            ad1.Update(dt1);
                            toreturn = "1";
                        }
                    }

                    #endregion insert_and_update_user_test_details

                }
            }
            return toreturn;

        }



        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string update_data(string entryid, string packageid, string examcode, string testid, string studentid, string attemptid, string myanswes, string language, string negmarktype, string negmark)
        {
            string resendsatsu = "NOTDONE";
            string ip = m1.find_ip();
            string[] data = myanswes.Split('$');
            if (myanswes != "")
            {
                if (get_validadate(testid))
                {
                    foreach (string a in data)
                    {
                        string[] ary_res = a.Split(',');

                        string qid = ary_res[0];
                        string asid = ary_res[1];
                        string time_in = ary_res[2];
                        string itime = ary_res[3];
                        string section_id = ary_res[4];

                        #region calculation_and_updates


                        string marks = "0";
                        string myans = find_my_ans(qid, testid, entryid, packageid);
                        DataTable dt = new DataTable();
                        dt = m1.featch_data("select qam.option_text,qtm.ans,qtm.marks,qam.opetion_text_HN,qtm.ans_HN,qtm.Language_Itype,qtm.Opetion_id  from question_answer_Master qam join question_info qtm on qam.quest_code=qtm.questionid where qam.Section= (select Test_id from Section_Arranging where Id='" + section_id + "' ) and qam.test_code='" + testid + "' and qam.quest_code='" + qid + "' and qam.opt_code='" + asid + "'");
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                string option_text = "";
                                string answer = "";
                                string Language_Itype = "";
                                if (language == "English")
                                {
                                    Language_Itype = "0";
                                    option_text = dr["option_text"].ToString();
                                    answer = dr["Opetion_id"].ToString(); //dr["ans"].ToString();
                                }
                                else
                                {
                                    if (dr["Language_Itype"].ToString() == "1")
                                    {
                                        Language_Itype = "1";
                                        option_text = dr["opetion_text_HN"].ToString();
                                        //answer = dr["ans_HN"].ToString();
                                        answer = dr["Opetion_id"].ToString(); //dr["ans"].ToString();
                                    }
                                    else
                                    {
                                        Language_Itype = "0";
                                        option_text = dr["option_text"].ToString();
                                        // answer = dr["ans"].ToString();
                                        answer = dr["Opetion_id"].ToString(); //dr["ans"].ToString();
                                    }
                                }

                                if (myans == answer)// chnage code 31/08/2018//asid
                                {
                                    marks = dr["marks"].ToString();
                                }
                                else
                                {

                                    if (negmarktype == "Percentage")
                                    {
                                        marks = "-" + ((Convert.ToDouble(dr["marks"].ToString()) * Convert.ToDouble(negmark) / 100)).ToString();
                                    }
                                    else if (negmarktype == "%")
                                    {
                                        marks = "-" + ((Convert.ToDouble(dr["marks"].ToString()) * Convert.ToDouble(negmark) / 100)).ToString();
                                    }
                                    else
                                    {
                                        marks = "-" + ((Convert.ToDouble(negmark))).ToString();
                                    }

                                }

                                #region insert_and_update_user_test_details
                                //Ip_address ='" + ip + "' and
                                SqlDataAdapter ad1 = new SqlDataAdapter("select * from user_test_details where  test_code='" + testid + "' and Section='" + section_id + "' and Studentid='" + studentid + "' and quest_code='" + qid + "'   and Packageid='" + packageid + "' and Entry_no='" + entryid + "'", My.conn);
                                
                                DataSet ds1 = new DataSet();
                                ad1.Fill(ds1);
                                DataTable dt1 = ds1.Tables[0];

                                int rowcount = dt1.Rows.Count;
                                if (rowcount == 0)
                                {
                                    if (asid != "0")
                                    {
                                        double minute = Math.Floor(double.Parse(time_in) / 60);
                                        double second = double.Parse(time_in) - minute * 60;

                                        string time_d = ((minute < 10 ? "0" : "") + minute + ":" + (second < 10 ? "0" : "") + second).ToString();

                                        DataRow dr1 = dt1.NewRow();
                                        dr1["Ip_address"] = ip;
                                        dr1["test_code"] = testid;
                                        dr1["quest_code"] = qid;
                                        dr1["opt_code"] = asid;
                                        dr1["opt_text"] = option_text;
                                        dr1["answer"] = answer;
                                        dr1["marks"] = marks;

                                        dr1["taken_time"] = time_in;
                                        dr1["taken_time_d"] = time_d;
                                        dr1["created_date"] = m1.date();
                                        dr1["icreated_date"] = m1.idate();
                                        dr1["Status"] = "Running";
                                        dr1["Section"] = section_id;
                                        dr1["Studentid"] = studentid;

                                        dr1["Exam_code"] = examcode;
                                        dr1["neg_mark"] = negmark;
                                        dr1["neg_mark_type"] = negmarktype;
                                        dr1["Language_Itype"] = Language_Itype;
                                        dr1["Attempt_id"] = attemptid;
                                        dr1["itime"] = itime;

                                        dr1["Packageid"] = packageid;
                                        dr1["Entry_no"] = entryid;



                                        dt1.Rows.Add(dr1);
                                        SqlCommandBuilder cb = new SqlCommandBuilder(ad1);
                                        ad1.Update(dt1);
                                    }
                                }
                                else
                                {
                                    foreach (DataRow dr1 in dt1.Rows)
                                    {


                                        int asn_itime = Convert.ToInt32(dr1["itime"].ToString());
                                        if (asn_itime < Convert.ToInt32(itime))
                                        {

                                            double tottime = Convert.ToDouble(dr1["taken_time"].ToString()) + Convert.ToDouble(time_in);
                                            double minute = Math.Floor(tottime / 60);
                                            double second = tottime - minute * 60;

                                            string time_d = ((minute < 10 ? "0" : "") + minute + ":" + (second < 10 ? "0" : "") + second).ToString();

                                            dr1["opt_code"] = asid;
                                            dr1["opt_text"] = option_text;

                                            dr1["taken_time"] = tottime;
                                            dr1["taken_time_d"] = time_d;

                                            dr1["marks"] = marks;
                                            dr1["itime"] = itime;

                                            SqlCommandBuilder cb = new SqlCommandBuilder(ad1);
                                            ad1.Update(dt1);

                                        }
                                        else
                                        {
                                        }



                                    }
                                }

                                #endregion insert_and_update_user_test_details

                            }
                        }
                        else
                        {
                            //Ip_address ='" + ip + "' and
                            SqlDataAdapter ad1 = new SqlDataAdapter("select * from user_test_details where  test_code='" + testid + "' and Section='" + section_id + "' and Studentid='" + studentid + "' and quest_code='" + qid + "' and Packageid='" + packageid + "' and Entry_no='" + entryid + "'", My.conn);
                             
                            DataSet ds1 = new DataSet();
                            ad1.Fill(ds1);
                            DataTable dt1 = ds1.Tables[0];
                            int rowcount = dt1.Rows.Count;
                            if (rowcount != 0)
                            {
                                if (asid == "0")
                                {
                                    foreach (DataRow dr1 in dt1.Rows)
                                    {
                                        dr1.Delete();
                                        SqlCommandBuilder cb = new SqlCommandBuilder(ad1);
                                        ad1.Update(dt1);
                                        break;
                                    }
                                }
                            }
                        }

                        #endregion

                    }



                    string query1 = @"update user_test_details set Status='Done' where  test_code='" + testid + "' and Exam_code='" + examcode + "' and Studentid='" + studentid + "'    and Packageid='" + packageid + "' and Entry_no='" + entryid + "';";//Ip_address='" + ip + "' and
                    query1 = query1 + @"update user_test_attempted_details set iStatus='1' where Studentid='" + studentid + "'   and Test_code='" + testid + "' and Exam_code='" + examcode + "' and Attempt_id='" + attemptid + "'  and Packageid='" + packageid + "' and Entry_no='" + entryid + "';";//and  Ip_address='" + ip + "'
                    query1 = query1 + @"update Pause_and_play_data set Status='Submitted' where  Package_id = '" + packageid + "' and Entry_id = '" + entryid + "' and Student_id = '" + studentid + "' and Test_id = '" + testid + "' and Exam_code = '" + examcode + "';";//and  Ip_address='" + ip + "'

                   

                    m1.executeQuery(query1);
                    resendsatsu = "DONE";
                }
                else
                {
                    resendsatsu = "NOTDONE";
                }
            }
            return resendsatsu;
        }

        private bool get_validadate(string test_id)
        {
            SqlCommand cmd = new SqlCommand(" Select format(ti.live_date, 'dd/MM/yyyy') as testdate,format(ti.live_date, 'hh:mm:ss tt') as time2,ti.live_date as Test_time,format(ti.live_date, 'dd/MM/yyyy hh:mm:ss tt') as Test_time1, ti.Exam_duration as Duration from OlineTest_Exam_name ti where ti.Exam_id="+ test_id + " ");
            DataTable dt = m1.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                return false;
            }
            else
            {
                My mycode = new My();
                if (dt.Rows[0]["testdate"].ToString() == mycode.date())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private string find_my_ans(string qid, string testid, string entryid, string packageid)
        {
            SqlDataAdapter ad = new SqlDataAdapter("Select opt_code from user_test_details where quest_code='" + qid + "' and test_code='" + testid + "'  and  Entry_no='" + entryid + "' and  Packageid='" + packageid + "'", My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                return "0";
            }
            else
            {
                return dt.Rows[0]["opt_code"].ToString();
            }
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string clear_data(string entryid, string packageid, string Section, string testid, string studentid, string sqid, string sansid, string examcode, string attemptid)
        {
            string ip = m1.find_ip();
            string query1 = @"delete from user_test_details where  test_code='" + testid + "' and Section='" + Section + "' and Studentid='" + studentid + "' and quest_code='" + sqid + "' and opt_code='" + sansid + "' and Exam_code='" + examcode + "' and icreated_date='" + m1.idate() + "'  and Attempt_id='" + attemptid + "'  and Packageid='" + packageid + "' and Entry_no='" + entryid + "'";//Ip_address ='" + ip + "' and

            m1.executeQuery(query1);
            return "DONE";
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string save_reason_data(string entryid, string packageid, string sectionid, string testid, string studentid, string txtreson, string examcode, string attemptid)
        {
            string toreturn = "";
            SqlDataAdapter ad1 = new SqlDataAdapter("select * from Save_reason_of_pause_exam ", My.conn);
            DataSet ds1 = new DataSet();
            ad1.Fill(ds1);
            DataTable dt1 = ds1.Tables[0];

            DataRow dr = dt1.NewRow();
            dr[1] = entryid;
            dr[2] = packageid;
            dr[3] = sectionid;
            dr[4] = testid;
            dr[5] = studentid;
            dr[6] = txtreson;
            dr[7] = examcode;
            dr[8] = attemptid;
            dr[9] = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MM/yyyy");
            dr[10] = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMdd");
            dr["time"] = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("hh:mm:ss tt");
            dt1.Rows.Add(dr);
            SqlCommandBuilder cb = new SqlCommandBuilder(ad1);
            ad1.Update(dt1);
            toreturn = "0";

            return toreturn;


        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string update_default_language(string studentid, string testid, string examcode, string examtypecode, string examcategory, string dflanguage, string attemptid, string entryid, string packageid)
        {
            SqlDataAdapter ad = new SqlDataAdapter("Select * from user_test_attempted_details where Studentid='" + studentid + "' and Test_code='" + testid + "' and Exam_code='" + examcode + "' and  Examtype_code='" + examtypecode + "' and  Exam_category_id='" + examcategory + "' and   Attempt_id='" + attemptid + "' and  Entry_no='" + entryid + "' and  Packageid='" + packageid + "'", My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            int rowcount = dt.Rows.Count;
            if (rowcount > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    dr[7] = dflanguage;

                    SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                    ad.Update(dt);
                    break;
                }
            }
            return "0";
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string browser_cute_savepause_data(string entryid,string packageid, string sectionid, string testid, string studentid, string jsondata, string examcode, string seconds, string browser, string curqueid, string curqueno, string _notvisitar, string _attemptar, string _notattempt, string _reviewmark, string _reviewar, string _selectedreview, string _marked, string _review, string hdattemptid)
        {

            //string entryid = "0";
            double minute = Math.Floor(Convert.ToDouble(seconds) / 60);
            double second = Convert.ToDouble(seconds) - minute * 60;
            string time_d = ((minute < 10 ? "0" : "") + minute + ":" + (second < 10 ? "0" : "") + second).ToString();

            if (_notvisitar == "[]") { _notvisitar = "0"; } if (_attemptar == "[]") { _attemptar = "0"; }
            if (_notattempt == "[]") { _notattempt = "0"; } if (_reviewmark == "[]") { _reviewmark = "0"; }
            if (_reviewar == "[]") { _reviewar = "0"; } if (_selectedreview == "[]") { _selectedreview = "0"; }
            if (_marked == "[]") { _marked = "0"; } if (_review == "[]") { _review = "0"; }

            string toreturn = "";
          

            string strQuery = @"IF NOT EXISTS(SELECT * FROM Pause_and_play_data WHERE Package_id=@package_id and Entry_id=@entry_id and Student_id=@studentid and Test_id=@testid and Exam_code=@examcode and Attempt_id=@Attempt_id) 
                            BEGIN 
                                 insert into Pause_and_play_data(Package_id,Entry_id,Student_id,Test_id,Exam_code,Rest_time_count,Data,Date_time,Cur_sec_id,Rest_time,Browser_name,Status,Cur_que_id,Cur_que_no,Not_visit_ar,Attempt_ar,Not_attempt,Review_mark,Review_ar,Selected_review,Marked,Review,Attempt_id)
                                 values (@package_id,@entry_id,@studentid,@testid,@examcode,@seconds,@jsondata,@Date_time,@section_id,@time_d,@browser,@Status,@cur_que_id,@cur_que_no,@_not_visit_ar,@_attempt_ar,@_not_attempt,@_review_mark,@_review_ar,@_selected_review,@_marked,@_review,@Attempt_id)               
                            END 
                           ELSE 
                            BEGIN 
                               Update Pause_and_play_data  set Rest_time_count=@seconds,Data=@jsondata,Date_time=@Date_time,Cur_sec_id=@section_id,Rest_time=@time_d,Browser_name=@browser,Status=@Status,Cur_que_id=@cur_que_id,Cur_que_no=@cur_que_no,Not_visit_ar=@_not_visit_ar,Attempt_ar=@_attempt_ar,Not_attempt=@_not_attempt,Review_mark=@_review_mark,Review_ar=@_review_ar,Selected_review=@_selected_review,Marked=@_marked,Review=@_review WHERE Package_id=@package_id and Entry_id=@entry_id and Student_id=@studentid and Test_id=@testid and Exam_code=@examcode and Attempt_id=@Attempt_id
                            END ";

            SqlCommand cmd;
            cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@package_id", packageid);
            cmd.Parameters.AddWithValue("@entry_id", entryid);
            cmd.Parameters.AddWithValue("@studentid", studentid);
            cmd.Parameters.AddWithValue("@testid", testid);
            cmd.Parameters.AddWithValue("@examcode", examcode);
            cmd.Parameters.AddWithValue("@seconds", seconds);
            cmd.Parameters.AddWithValue("@jsondata", jsondata);
            cmd.Parameters.AddWithValue("@Date_time", DateTime.UtcNow.AddHours(5).AddMinutes(30));
            cmd.Parameters.AddWithValue("@section_id", sectionid);
            cmd.Parameters.AddWithValue("@time_d", time_d);
            cmd.Parameters.AddWithValue("@browser", browser);
            cmd.Parameters.AddWithValue("@Status", "Resume");
            cmd.Parameters.AddWithValue("@cur_que_id", curqueid);
            cmd.Parameters.AddWithValue("@cur_que_no", curqueno);
            cmd.Parameters.AddWithValue("@_not_visit_ar", _notvisitar);
            cmd.Parameters.AddWithValue("@_attempt_ar", _attemptar);
            cmd.Parameters.AddWithValue("@_not_attempt", _notattempt);
            cmd.Parameters.AddWithValue("@_review_mark", _reviewmark);
            cmd.Parameters.AddWithValue("@_review_ar", _reviewar);
            cmd.Parameters.AddWithValue("@_selected_review", _selectedreview);
            cmd.Parameters.AddWithValue("@_marked", _marked);
            cmd.Parameters.AddWithValue("@_review", _review);
            cmd.Parameters.AddWithValue("@Attempt_id", hdattemptid);
            if (My.InsertUpdateData(cmd))
            {
            }
            toreturn = "0";
            if (Session["teststart"] != null)
            {
                Session.Remove("teststart");
            }
            return toreturn;
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

        public string save_pause_data(string entryid, string packageid, string sectionid, string testid, string studentid, string jsondata, string examcode, string seconds, string browser, string curqueid, string curqueno, string _notvisitar, string _attemptar, string _notattempt, string _reviewmark, string _reviewar, string _selectedreview, string _marked, string _review, string hdattemptid)
        {
            double minute = Math.Floor(Convert.ToDouble(seconds) / 60);
            double second = Convert.ToDouble(seconds) - minute * 60;
            string time_d = ((minute < 10 ? "0" : "") + minute + ":" + (second < 10 ? "0" : "") + second).ToString();


            if (_notvisitar == "[]") { _notvisitar = "0"; } if (_attemptar == "[]") { _attemptar = "0"; }
            if (_notattempt == "[]") { _notattempt = "0"; } if (_reviewmark == "[]") { _reviewmark = "0"; }
            if (_reviewar == "[]") { _reviewar = "0"; } if (_selectedreview == "[]") { _selectedreview = "0"; }
            if (_marked == "[]") { _marked = "0"; } if (_review == "[]") { _review = "0"; }

            string toreturn = "";

            string strQuery = @" insert into Pause_and_play_data(Package_id,Entry_id,Student_id,Test_id,Exam_code,Rest_time_count,Data,Date_time,Cur_sec_id,Rest_time,Browser_name,Status,Cur_que_id,Cur_que_no,Not_visit_ar,Attempt_ar,Not_attempt,Review_mark,Review_ar,Selected_review,Marked,Review,Attempt_id)
                                 values (@package_id,@entry_id,@studentid,@testid,@examcode,@seconds,@jsondata,@Date_time,@section_id,@time_d,@browser,@Status,@cur_que_id,@cur_que_no,@_not_visit_ar,@_attempt_ar,@_not_attempt,@_review_mark,@_review_ar,@_selected_review,@_marked,@_review,@Attempt_id);";
            SqlCommand cmd;
            cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@package_id", packageid);
            cmd.Parameters.AddWithValue("@entry_id", entryid);
            cmd.Parameters.AddWithValue("@studentid", studentid);
            cmd.Parameters.AddWithValue("@testid", testid);
            cmd.Parameters.AddWithValue("@examcode", examcode);
            cmd.Parameters.AddWithValue("@seconds", seconds);
            cmd.Parameters.AddWithValue("@jsondata", jsondata);
            cmd.Parameters.AddWithValue("@Date_time", DateTime.UtcNow.AddHours(5).AddMinutes(30));
            cmd.Parameters.AddWithValue("@section_id", sectionid);
            cmd.Parameters.AddWithValue("@time_d", time_d);
            cmd.Parameters.AddWithValue("@browser", browser);
            cmd.Parameters.AddWithValue("@Status", "Resume");
            cmd.Parameters.AddWithValue("@cur_que_id", curqueid);
            cmd.Parameters.AddWithValue("@cur_que_no", curqueno);
            cmd.Parameters.AddWithValue("@_not_visit_ar", _notvisitar);
            cmd.Parameters.AddWithValue("@_attempt_ar", _attemptar);
            cmd.Parameters.AddWithValue("@_not_attempt", _notattempt);
            cmd.Parameters.AddWithValue("@_review_mark", _reviewmark);
            cmd.Parameters.AddWithValue("@_review_ar", _reviewar);
            cmd.Parameters.AddWithValue("@_selected_review", _selectedreview);
            cmd.Parameters.AddWithValue("@_marked", _marked);
            cmd.Parameters.AddWithValue("@_review", _review);
            cmd.Parameters.AddWithValue("@Attempt_id", hdattemptid);
            if (My.InsertUpdateData(cmd))
            {
            }


            toreturn = "0";

            return toreturn;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string update_pause_data(string entryid, string packageid, string sectionid, string testid, string studentid, string jsondata, string examcode, string seconds, string browser, string cur_que_id, string cur_que_no)
        {

            string toreturn = "";
            SqlDataAdapter ad1 = new SqlDataAdapter("select * from Pause_and_play_data where Package_id = '" + packageid + "' and Entry_id = '" + entryid + "' and Student_id = '" + studentid + "' and Test_id = '" + testid + "' and Exam_code = '" + examcode + "' and Rest_time_count='" + seconds + "'and Cur_que_id = '" + cur_que_id + "' and Cur_que_no='" + cur_que_no + "' ", My.conn);
            DataSet ds1 = new DataSet();
            ad1.Fill(ds1);
            DataTable dt1 = ds1.Tables[0];

            int rowcount = dt1.Rows.Count;
            if (rowcount > 0)
            {
                foreach (DataRow dr in dt1.Rows)
                {
                    dr[12] = "Play";
                }
                SqlCommandBuilder cb = new SqlCommandBuilder(ad1);
                ad1.Update(dt1);
            }
            toreturn = "0";

            return toreturn;
        }

        
    }
}
