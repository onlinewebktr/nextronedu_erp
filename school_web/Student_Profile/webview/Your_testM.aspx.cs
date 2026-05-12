using school_web.AppCode;
using school_web.AppCode.Onlineexam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile.webview
{
    public partial class Your_testM : System.Web.UI.Page
    {
        Find_and_execute_code fec = new Find_and_execute_code();
        My code = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string studentid = Request.QueryString["studentid"];
                string test_code = Request.QueryString["testid"];
                try
                {
                    hd_modepage.Value = Request.QueryString["mode"];
                    Session["mode"] = hd_modepage.Value;
                }
                catch
                {
                    hd_modepage.Value = "mobile";
                    Session["mode"] = "mobile";
                }

                bool checkdate = My.get_date_valid(test_code);
                if (checkdate == true)
                {

                    DataTable dt = code.FillData("select * from Firm_Details   ");
                    Image2.ImageUrl = dt.Rows[0]["logo"].ToString();

                    HttpBrowserCapabilities objBrwInfo = Request.Browser;
                    string browser_name = objBrwInfo.Browser;
                    string browser_version = objBrwInfo.Version;
                    if (browser_name == "Chrome" || browser_name == "Firefox" || browser_name == "Safari")
                    { 
                        string examcode = Request.QueryString["Examcode"];
                        string examtype_code = Request.QueryString["examtypecode"];
                        string testmode = Request.QueryString["testmode"];
                        string language = Request.QueryString["language"];
                        string examname = Request.QueryString["examname"];

                        



                        if (!string.IsNullOrEmpty(test_code))
                        {
                            //Session["student"] = code.Unzip(studentid);
                            //hd_userid.Value = code.Unzip(studentid);
                            //hd_studentid.Value = code.Unzip(studentid);
                            //hd_testid.Value = code.Unzip(test_code);
                            //hd_examcode.Value = code.Unzip(examcode);
                            //hd_examtype_code.Value = code.Unzip(examtype_code);
                            //hd_testmode.Value = code.Unzip(testmode);

                            Session["student"] = studentid;
                            hd_userid.Value = studentid;
                            hd_studentid.Value = studentid;
                            hd_testid.Value = test_code;
                            hd_examcode.Value = examcode;
                            hd_examtype_code.Value = examtype_code;
                            hd_testmode.Value = testmode;

                            Session["testid"] = hd_testid.Value;


                            Session["student"] = hd_studentid.Value;
                            hd_entry_id.Value = Request.QueryString["Entryno"];
                            hd_package_id.Value = Request.QueryString["Packageid"];
                            hd_exam_type.Value = Request.QueryString["examtype"]; //PT,MAINS
                            string lan = language;//code.Unzip(language);
                            hd_df_lang_con.Value = lan;
                            if (lan == "English")
                            {
                                ddl_default_language.Text = "0"; 
                                hd_language.Value = "0";
                            }
                            else
                            {
                                ddl_default_language.Text = "1";
                                hd_language.Value = "1";
                            }

                            if (Session["teststart"] == null)
                            {

                                lbl_testname.Text = examname;



                                find_test_is_resume_or_not(hd_package_id.Value, hd_entry_id.Value, hd_studentid.Value, hd_testid.Value, hd_examcode.Value);



                                hd_exam_category.Value = "1";

                                //section wise = 0, Overall=1
                                hd_numbering.Value = "1";

                                //time wise=0	, open all=1 
                                hd_tab_open.Value = "1";

                                //individual section wise=0,	Overall=1
                                hd_timing.Value = "1";

                                find_test_detail_for_other();

                                send_data_into_user_test_attempted_details();
                                find_user_details();
                                Session["teststart"] = "1";
                            }
                            else
                            {

                                lbl_testname.Text = examname;



                                find_test_is_resume_or_not(hd_package_id.Value, hd_entry_id.Value, hd_studentid.Value, hd_testid.Value, hd_examcode.Value);



                                hd_exam_category.Value = "1";


                                hd_numbering.Value = "1";


                                hd_tab_open.Value = "1";


                                hd_timing.Value = "1";

                                find_test_detail_for_other();

                                send_data_into_user_test_attempted_details();
                                find_user_details();
                                Session["teststart"] = "1";


                            }
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('This browser is not supported. Please use only Chrome Browser'); window.location='" + Request.ApplicationPath + "Student_Profile/webview/Test_Info.aspx';", true);
                        //Response.Redirect("Home.aspx");
                    }
                }
                else
                {
                    Response.Redirect("Home.aspx", false);
                }
            }
        }

        private bool check_test_attempt(string examcode, string examtype_code1)
        {
            bool toreturn = true;
            SqlDataAdapter ad = new SqlDataAdapter("Select iStatus from user_test_attempted_details where Studentid='" + hd_userid.Value + "' and Test_code='" + hd_testid.Value + "' and Exam_code='" + examcode + "' and Examtype_code='" + examtype_code1 + "' and Packageid='" + hd_package_id.Value + "' and Entry_no='" + hd_entry_id.Value + "'", My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                toreturn = true;
            }
            else
            {
                toreturn = false;
            }
            return toreturn;
        }

        private void find_user_details()
        {

            DataTable dt = new DataTable();
            dt = fec.featch_data("select top 1 studentname as FullName,studentimagepath as Image_Path from admission_registor where admissionserialnumber='" + hd_studentid.Value + "' and Transfer_Status in('NT','New')  order by id desc");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    lbl_user_name.Text = dr["FullName"].ToString();

                    if (dt.Rows[0]["Image_Path"].ToString() != "")
                    {
                        Image1.ImageUrl = dt.Rows[0]["Image_Path"].ToString();
                    }
                    else
                    {
                        Image1.ImageUrl = "../../Online_Test_admin/Doc/imagenotavailable.jpg";
                    }
                    break;
                }
            }
        }

        private void find_test_is_resume_or_not(string package_id, string entry_id, string studentid, string testid, string examcode)
        {
            SqlDataAdapter ad_reg = new SqlDataAdapter("select top 1 Data,Cur_sec_id,Cur_que_id,Cur_que_no,Rest_time,Not_visit_ar,Attempt_ar,Not_attempt,Review_mark,Review_ar,Selected_review,Marked,Review,Attempt_id from Pause_and_play_data where Package_id = '" + package_id + "' and Entry_id = '" + entry_id + "' and Student_id = '" + studentid + "' and Test_id = '" + testid + "' and Exam_code = '" + examcode + "' and Status!='Submitted' order by id desc", My.conn);
            DataSet ds = new DataSet();
            ad_reg.Fill(ds, "Pause_and_play_data");
            DataTable dt = ds.Tables[0];
            int rowcount = dt.Rows.Count;

            if (rowcount == 0)
            {
                hd_resume_status.Value = "0";

            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    hd_resume_data.Value = dr["Data"].ToString();
                    hd_cur_secid.Value = dr["Cur_sec_id"].ToString();
                    hd_cur_queid.Value = dr["Cur_que_id"].ToString();
                    hd_cur_queno.Value = dr["Cur_que_no"].ToString();
                    hd_time.Value = dr["Rest_time"].ToString();
                    hd_resume_status.Value = "1";
                    hd_attempt_id.Value = dr["Attempt_id"].ToString();
                    Session["Attemptid"] = dr["Attempt_id"].ToString();
                    HttpCookie StudentCookies = new HttpCookie("Pause_and_play");
                    Response.Cookies["Pause_and_play"].Value = dr["Not_visit_ar"].ToString() + "&" + dr["Attempt_ar"].ToString() + "&" + dr["Not_attempt"].ToString() + "&" + dr["Review_mark"].ToString() + "&" + dr["Review_ar"].ToString() + "&" + dr["Selected_review"].ToString() + "&" + dr["Marked"].ToString() + "&" + dr["Review"].ToString();
                    Response.Cookies["Pause_and_play"].Expires = DateTime.Now.AddMonths(1);

                }

            }
        }




        private void find_test_detail_for_other()
        {
            string examcategory = code.exam_categoty_id_via_examcode(hd_testid.Value);
            DataTable dt = new DataTable();



            dt = fec.featch_data(" Select Section,Exam_name,Exam_duration ,(Select top 1 created_date from question_info where test_id='" + hd_testid.Value + "' ) created_date1,(Select top 1 Create_time from question_info where test_id='" + hd_testid.Value + "' ) Create_time1,(Select top 1 created_by from question_info where test_id='" + hd_testid.Value + "' ) created_by1,(Select count(Id) from question_info where test_id='" + hd_testid.Value + "'  ) as noofquestion,(select top 1 Subject_name from Subject_Master where Subject_id=OlineTest_Exam_name.subjectname) as subjectname,(select top 1 Course_Name from Add_course_table where course_id=OlineTest_Exam_name.Class_id) as Course_Name,(Select top 1 Negative_Marking from Add_Exam_Category) as Negative_Marking,(Select top 1 Type from Add_Exam_Category) as Type,(Select top 1 Category_code from Add_Exam_Category) as Exame_catogry_code   from OlineTest_Exam_name where Exam_id='" + hd_testid.Value + "' ");


            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    lbl_testname.Text = dr["Exam_name"].ToString();
                    lbl_time.Text = dr["Exam_duration"].ToString();
                    if (hd_resume_status.Value == "0")
                    {
                        hd_time.Value = dr["Exam_duration"].ToString();
                    }

                    hd_time_type.Value = "Minutes"; //dr["Type"].ToString();

                    hd_neg_mark.Value = dr["Negative_Marking"].ToString();
                    hd_neg_mark_type.Value = dr["Type"].ToString();

                    hd_exam_category.Value = dr["Exame_catogry_code"].ToString();




                    break;
                }
            }
        }

        #region user_test_attempted_details
        private void send_data_into_user_test_attempted_details()
        {
            if (hd_resume_status.Value == "0")
            {
                string Attempt_id = find_Attempt_id();
                if (!string.IsNullOrEmpty(Attempt_id))
                {
                    hd_attempt_id.Value = Attempt_id;


                }
            }
            Session["Attemptid"] = hd_attempt_id.Value;
            string date = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MM/yyyy");
            string idate = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMdd");
            SqlCommand cmd1;
            string strQuery = @"INSERT INTO user_test_attempted_details (Studentid,Ip_address,Test_code,Exam_code,Examtype_code,Exam_category_id,Language_Itype,Attempt_id,created_date,icreated_date,iStatus,Packageid,Entry_no,submit_time) values (@Studentid,@Ip_address,@Test_code,@Exam_code,@Examtype_code,@Exam_category_id,@Language_Itype,@Attempt_id,@created_date,@icreated_date,@iStatus,@Packageid,@Entry_no,@submit_time)";
            cmd1 = new SqlCommand(strQuery);
            cmd1.Parameters.AddWithValue("@Studentid", hd_studentid.Value);
            cmd1.Parameters.AddWithValue("@Ip_address", code.find_ip());
            cmd1.Parameters.AddWithValue("@Test_code", hd_testid.Value);
            cmd1.Parameters.AddWithValue("@Exam_code", hd_examcode.Value);
            cmd1.Parameters.AddWithValue("@Examtype_code", hd_examtype_code.Value);
            cmd1.Parameters.AddWithValue("@Exam_category_id", hd_exam_category.Value);
            cmd1.Parameters.AddWithValue("@Language_Itype", hd_language.Value);
            cmd1.Parameters.AddWithValue("@Attempt_id", hd_attempt_id.Value);
            cmd1.Parameters.AddWithValue("@created_date", date);
            cmd1.Parameters.AddWithValue("@icreated_date", idate);
            cmd1.Parameters.AddWithValue("@iStatus", "0");
            cmd1.Parameters.AddWithValue("@Packageid", hd_package_id.Value);
            cmd1.Parameters.AddWithValue("@Entry_no", hd_entry_id.Value);
            cmd1.Parameters.AddWithValue("@submit_time", code.time());
            if (My.InsertUpdateData(cmd1))
            {

            }
        }

        private string find_Attempt_id()
        {
            SqlDataAdapter ad_reg = new SqlDataAdapter("select max(Attempt_id) from user_test_attempted_details where Studentid='" + hd_studentid.Value + "' and Test_code='" + hd_testid.Value + "' and Exam_code='" + hd_examcode.Value + "' and Examtype_code='" + hd_examtype_code.Value + "' and Exam_category_id='" + hd_exam_category.Value + "'", My.conn);
            DataSet ds = new DataSet();
            ad_reg.Fill(ds, "user_test_attempted_details");
            DataTable dt = ds.Tables[0];
            int rowcount = dt.Rows.Count;

            if (rowcount == 0)
            {
                return "1";
            }
            else
            {
                if (dt.Rows[0][0].ToString() == "")
                {
                    return "1";
                }
                else
                {
                    return (Convert.ToInt32(dt.Rows[0][0].ToString()) + 1).ToString();

                }
            }



        }

        #endregion user_test_attempted_details


    }
}