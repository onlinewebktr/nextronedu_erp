using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class Student_Stup_And_Start : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    if (Session["Admin"] == null)
                    {
                        Session.Abandon();
                        Session.Clear();
                        Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
                        Response.Write("<script language=javascript>wnd.close();</script>");
                        Response.Write("<script language=javascript>window.open('../Default.aspx','_parent',replace=true);</script>");
                    }
                    else
                    {
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();
                        mycode.bind_all_ddl_with_id(ddl_session, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                        mycode.bind_all_ddl_with_id(ddl_session_name, "select Session,session_id from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                        ddl_session.SelectedValue = My.get_session_id();
                        ddl_session_name.SelectedValue = ddl_session.SelectedValue;
                        Session["classchange"] = "3";
                        ViewState["Branch_id"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        string pagename_current = "Student_Stup_And_Start.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "MonthlyFeePayment");
            }
        }

        string scrpt;
        private void Alertme(string msg, string panel)
        {
            scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            if (panel == "success")
            {
                lbl_success.Text = msg;
                success.Visible = true;
                warning.Visible = false;
            }
            if (panel == "warning")
            {
                lbl_warning.Text = msg;
                success.Visible = false;
                warning.Visible = true;
            }
        }

        #region find student data
        protected void btn_find_admission_no_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_add"].ToString() == "1")
                {
                    find_data();

                }
                else if (ViewState["Is_Edit"].ToString() == "1")
                {
                    find_data();
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }

        private void find_data()
        {
            if (txt_admission_no.Text == "")
            {
                Alertme("Please enter  current admission no.", "warning");
            }
            else
            {
                string query = "select * from admission_registor where admissionserialnumber='" + txt_admission_no.Text + "' and Session_id='" + ddl_session.SelectedValue + "' and StudentStatus='AV'     ";
                find_details(query);
            }
        }

        private void find_details(string query)
        {
            SqlDataAdapter ad_contactus = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad_contactus.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                pnl_stop_start.Visible = false;
                
                std_basic_infoS.Visible = false;
                Alertme("Student details not found...", "warning");
                return;
            }
            else
            {
                pnl_stop_start.Visible = true;
                foreach (DataRow dr in dt.Rows)
                {
                    std_basic_infoS.Visible = true;
                    

                    lbl_name.Text = dr["studentname"].ToString();
                    lbl_father_name.Text = dr["fathername"].ToString();
                    lblclass.Text = dr["class"].ToString();
                    ViewState["class_id"] = dr["Class_id"].ToString();
                    txtsection.Text = dr["Section"].ToString();
                    lbl_admission_no.Text = dr["admissionserialnumber"].ToString();
                    txt_admission_no.Text = dr["admissionserialnumber"].ToString();
                    lblhostel.Text = dr["hosteltaken"].ToString();
                    ViewState["parameter"] = dr["hosteltaken"].ToString() == "yes" ? "HostelMonthlyFee" : "MonthlyFee";
                    ViewState["parameter_id"] = dr["hosteltaken"].ToString() == "yes" ? "3" : "4";
                    lbltransporttion.Text = dr["transportationtaken"].ToString();
                    lbl_phone.Text = dr["mobilenumber"].ToString();
                    ViewState["hostel_id"] = My.toint(dr["Hostel_id"].ToString());
                    ViewState["day_bording"] = My.toBool(dr["is_applied_dayboarding"]);
                    ViewState["day_bording_with_lunch"] = My.toBool(dr["day_boarding_with_lunch"]);
                    ViewState["group_id"] = "3";
                    ViewState["category_id"] = dr["category_id"].ToString();
                    ViewState["sub_category_id"] = dr["SubCategory_id"].ToString();
                    ViewState["classid"] = dr["Class_id"].ToString();
                    ViewState["Section"] = dr["Section"].ToString();
                    ViewState["sessionIDs"] = dr["Session_id"].ToString();
                    ViewState["admissionserialnumber"] = dr["admissionserialnumber"].ToString();
                    ViewState["session"] = dr["session"].ToString();
                    ViewState["id"] = dr["id"].ToString();
                    lbl_old_roll_no.Text = dr["rollnumber"].ToString();

                  //  mycode.bind_all_ddl_with_id(ddl_monthname, "select Month,Position from Month_Index order by Position asc");


                    mycode.bind_all_ddl_with_id(ddl_monthname, "select Month,Month_Id from Month_Index where Month in (select month from Typewise_fee_collection where admission_no = '" + ViewState["admissionserialnumber"].ToString() + "' and (parameter = 'MonthlyFee'  or parameter = 'HostelMonthlyFee') and session = '" + ViewState["session"].ToString() + "' and class_id = '" + ViewState["classid"].ToString() + "' and status = 'Dues') or Month not in (select month from Typewise_fee_collection where admission_no = '" + ViewState["admissionserialnumber"].ToString() + "' and(parameter = 'MonthlyFee'  or parameter = 'HostelMonthlyFee') and session = '" + ViewState["session"].ToString() + "' and class_id = '" + ViewState["classid"].ToString() + "' and status = 'Paid') order by Position asc");

                }

            }
        }


        #endregion

        #region find by name
        protected void btn_find_by_name_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_add"].ToString() == "1")
                {
                    find_name();

                }
                else if (ViewState["Is_Edit"].ToString() == "1")
                {
                    find_name();
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }

          
        }

        private void find_name()
        {
            try
            {
                if (ddl_session_name.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session", "warning");
                    ddl_session_name.Focus();
                    return;
                }
                if (txt_student_name.Text == "")
                {
                    Alertme("Please enter student name.", "warning");
                    txt_student_name.Focus();
                    return;
                }

                string stdname = txt_student_name.Text.Trim();
                string query = "select * from admission_registor where studentname like '%" + stdname + "%' and session='" + ddl_session_name.SelectedItem.Text + "' order by id asc";
                DataTable dt = My.dataTable(query);
                if (dt.Rows.Count == 0)
                {
                    rp_std.DataSource = null;
                    rp_std.DataBind();
                    Alertme("Data Not Found...", "warning");
                }
                else if (dt.Rows.Count == 1)
                {
                    query = "select * from admission_registor where admissionserialnumber='" + dt.Rows[0]["admissionserialnumber"].ToString() + "' and Session='" + ddl_session_name.SelectedItem.Text + "' order by studentname asc";
                    find_details(query);
                }
                else
                {
                    rp_std.DataSource = dt;
                    rp_std.DataBind();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "studentInfo();", true);
                }
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }

        protected void lnk_select_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "";
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbladmissionserialnumber = (Label)row.FindControl("lbladmissionserialnumber");
                Label lbl_session = (Label)row.FindControl("lbl_session");
                query = "select * from admission_registor where admissionserialnumber='" + lbladmissionserialnumber.Text + "' and Session='" + lbl_session.Text + "' and  Status='1'    ";

                find_details(query);
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }



        #endregion


        #region process
        protected void btn_submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_monthname.SelectedItem.Text == "Select")
                {
                    Alertme("Please select month name", "warning");
                }
                if (txt_remarks.Text == "Select")
                {
                    Alertme("Please enter remarks", "warning");
                }
                else
                {
                    if (rd_start.Checked == false && rd_stop.Checked == false)
                    {
                        Alertme("Please choos the strat or stop", "warning");

                    }
                    else
                    {
                       




                        string Change_type = "";
                        string status = "";
                        string msg = "";
                        if (rd_start.Checked == true)
                        {
                            Change_type = "Student Start";
                            msg = "Student status has been started successfully";
                            status = "1";
                          
                        }
                        else
                        {
                            Change_type = "Student Stop";
                            msg = "Student status has been stopped successfully";
                            status = "0";
                        }
                        bool checkstatus = get_status(status);
                        if(checkstatus==true)
                        {
                            My.Insert("admission_registor_Change_admission_no_history", new
                            {
                                Old_admission_no = lbl_admission_no.Text,
                                Current_admission_no = lbl_admission_no.Text,
                                Session_id = ViewState["sessionIDs"].ToString(),
                                Created_By = ViewState["Userid"].ToString(),
                                Date_time = My.getdate1(),
                                Change_type = Change_type,
                                Class_Id_New = ViewState["classid"].ToString(),
                                Old_Class_id = ViewState["classid"].ToString(),
                                Reason = txt_remarks.Text,
                                Remark = txt_remarks.Text,
                                Month_id = ddl_monthname.SelectedValue,
                            });
                            My.exeSql("update admission_registor set Status=" + status + " where admissionserialnumber='" + lbl_admission_no.Text + "' and Session_id='" + ViewState["sessionIDs"].ToString() + "'");
                            Alertme(msg, "success");

                            txt_remarks.Text = "";

                        }
                        else
                        {
                            if(rd_start.Checked==true)
                            {
                                Alertme("Sorry you can't start because already started", "warning");
                            }
                           else

                           {
                                Alertme("Sorry you can't stop because already stop ", "warning");
                            }
                           

                        }
                    }
                }
            }
            catch
            {

            }
         
        }

        private bool get_status(string Status)
        {
            string query = "Select Status from admission_registor where admissionserialnumber='"+ lbl_admission_no.Text + "' and Class_id='"+ ViewState["classid"].ToString() + "' and Session_id='"+ ViewState["sessionIDs"].ToString() + "' and Status='" + Status + "'  ";
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count == 0)
            {
                return true;
            }
            else
            {
                return false;

            }

      }
        #endregion

        #region WebMethoD
        [WebMethod]
        public static List<string> GetRooPath(string PathRooT, string Session_id)
        {
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(My.conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct studentname from admission_registor where studentname LIKE '%'+@SearchMobNo+'%'     and Session_id='" + Session_id + "'";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchMobNo", PathRooT);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        MobResult.Add(dr["studentname"].ToString());
                    }
                    con.Close();
                    return MobResult;
                }
            }
        }

        [WebMethod]
        public static List<string> GetRooPathAdmNo(string PathRooT, string Session_id)
        {
            string sessionid = Session_id;
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(My.conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct admissionserialnumber from admission_registor where admissionserialnumber LIKE '%'+@SearchMobNo+'%'   and Session_id='" + Session_id + "'";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchMobNo", PathRooT);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        MobResult.Add(dr["admissionserialnumber"].ToString());
                    }
                    con.Close();
                    return MobResult;
                }
            }
        }
        #endregion
    }
}