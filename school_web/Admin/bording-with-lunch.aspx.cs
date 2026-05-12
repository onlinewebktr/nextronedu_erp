using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class bording_with_lunch : System.Web.UI.Page
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
                        ViewState["branch_id"] = mycode.get_branch_id(Session["Admin"].ToString());

                        mycode.bind_all_ddl_with_id(ddlclass, "Select Course_Name,course_id from Add_course_table order by Position asc");
                        mycode.bind_all_ddl_with_id(ddlsession, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int)");
                        mycode.bind_all_ddl_with_id(ddl_session_student, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int)");
                        mycode.bind_all_ddl_with_id(ddlsessionad, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int)");

                        ddlsessionad.SelectedValue = My.get_session_id();
                        ddlsession.SelectedValue = My.get_session_id();
                        ddl_session_student.SelectedValue = My.get_session_id();
                        mycode.bind_all_ddl_with_id(ddl_from_month, "select Month,Month_Id from Month_Index order by Position asc");
                        string pagename_current = Path.GetFileName(Request.Path);
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



        #region WebMethoD
        [WebMethod]
        public static List<string> GetRooPath(string PathRooT,string Session_id)
        {
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(My.conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct studentname from admission_registor where studentname LIKE '%'+@SearchMobNo+'%' and Session_id='" + Session_id + "' and  Transfer_Status in ('NT','New' ) and  Status='1'";
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

        public static List<string> GetRooPathAdmNo(string PathRooT,string Session_id)
        {
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(My.conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct admissionserialnumber from admission_registor where admissionserialnumber LIKE '%'+@SearchMobNo+'%'  and  Transfer_Status in ('NT','New' ) and  Status='1' and Session_id='" + Session_id + "'";
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


        #region FinD StudenT
        protected void btn_find_admission_no_Click(object sender, EventArgs e)
        {
            try
            {
                //empty_form();
                string query = "select * from admission_registor where admissionserialnumber='" + txt_admission_no.Text + "' and Session='" + ddlsessionad.SelectedItem.Text + "' and StudentStatus='AV'  and Is_TC_Taken!='true' and   Transfer_Status in ('NT','New' ) and  Status='1' order by id asc";
                find_details(query);
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
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
                std_basic_infoS.Visible = false;
                Alertme("Student details not found...", "warning");
                return;
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string queryS = "select * from Student_mapping_with_boarding_with_lunch where Session_id='" + dr["Session_id"].ToString() + "' and Admission_no='" + dr["admissionserialnumber"].ToString() + "' and Class_id='" + dr["Class_id"].ToString() + "'";
                    DataTable dts = My.dataTable(queryS);
                    if (dts.Rows.Count != 0)
                    {
                        Alertme("Selected student already mapped boarding with lunch.", "warning");
                        return;
                    }


                    std_basic_infoS.Visible = true;
                    txt_student_name.Text = dr["studentname"].ToString();
                    ddlclass.SelectedValue = dr["Class_id"].ToString();
                    ddlsession.SelectedValue = dr["Session_id"].ToString();
                    txt_section.Text = dr["Section"].ToString();
                    txtrollnumber.Text = dr["rollnumber"].ToString();
                    lbl_name.Text = dr["studentname"].ToString();
                    lbl_father_name.Text = dr["fathername"].ToString();
                    lblclass.Text = dr["class"].ToString();
                    ViewState["class_id"] = dr["Class_id"].ToString();
                    txtsection.Text = dr["Section"].ToString();
                    lbl_admission_no.Text = dr["admissionserialnumber"].ToString();
                    txt_admission_no.Text = dr["admissionserialnumber"].ToString();
                    lblhostel.Text = dr["hosteltaken"].ToString();
                    ViewState["parameter"] = dr["hosteltaken"].ToString().ToUpper() == "YES" ? "HostelMonthlyFee" : "MonthlyFee";
                    ViewState["parameter_id"] = dr["hosteltaken"].ToString().ToUpper() == "YES" ? "3" : "4";
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
                    ViewState["admSionNo"] = dr["admissionserialnumber"].ToString();
                    if (dr["Transportation_Id"].ToString() == "")
                    {
                        ViewState["transportID"] = "0";
                    }
                    else
                    {
                        ViewState["transportID"] = dr["Transportation_Id"].ToString();
                    }


                    if (ViewState["day_bording_with_lunch"].ToString() == "True")
                    {
                        ViewState["parameteridS"] = "44";
                    }
                    else
                    {
                        ViewState["parameteridS"] = "4";
                    }
                }
            }
        }

        protected void btnfind_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlclass.SelectedItem.Text == "Select")
                {
                    Alertme("Please select class", "warning");
                    ddlclass.Focus();
                    return;
                }
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session", "warning");
                    ddlsession.Focus();
                    return;
                }
                if (txt_section.Text == "")
                {
                    Alertme("Please enter section", "warning");
                    txt_section.Focus();
                    return;
                }
                if (txtrollnumber.Text == "")
                {
                    Alertme("Please enter roll no", "warning");
                    txtrollnumber.Focus();
                    return;
                }

                string query = "select * from admission_registor where class='" + ddlclass.SelectedItem.Text + "' and Session='" + ddlsession.SelectedItem.Text + "' and section='" + txt_section.Text + "' and rollnumber='" + txtrollnumber.Text + "' and StudentStatus='AV'   and Is_TC_Taken!='true' and    Transfer_Status in ('NT','New' ) and  Status='1' order by id asc";
                find_details(query);
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }

        protected void btn_find_name_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_session_student.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session", "warning");
                    ddl_session_student.Focus();
                    return;
                }
                if (txt_student_name.Text == "")
                {
                    Alertme("Please enter student name.", "warning");
                    txt_student_name.Focus();
                    return;
                }

                string query = "select * from admission_registor where studentname like '%" + txt_student_name.Text + "%' and session='" + ddl_session_student.SelectedItem.Text + "' and StudentStatus='AV'    and Is_TC_Taken!='true' and Transfer_Status in ('NT','New' ) and Status='1' order by id asc";
                DataTable dt = My.dataTable(query);
                if (dt.Rows.Count == 0)
                {
                    rp_std.DataSource = null;
                    rp_std.DataBind();
                    Alertme("Data Not Found...", "warning");
                    myModal2.Visible = false;
                }
                else
                {
                    rp_std.DataSource = dt;
                    rp_std.DataBind();
                    myModal2.Visible = true;
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
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbladmissionserialnumber = (Label)row.FindControl("lbladmissionserialnumber");
                Label lbl_session = (Label)row.FindControl("lbl_session");
                string query = "select * from admission_registor where admissionserialnumber='" + lbladmissionserialnumber.Text + "' and Session='" + lbl_session.Text + "' and StudentStatus='AV'    and Is_TC_Taken!='true' order by id asc";
                find_details(query);
                myModal2.Visible = false;
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            myModal2.Visible = false;
        }

        #endregion

        protected void btn_save_bording_Click(object sender, EventArgs e)
        {
            try
            {


                if (ViewState["Is_add"].ToString() == "1")
                {
                    if (ddl_from_month.SelectedItem.Text == "Select")
                    {
                        Alertme("Please select month.", "warning");
                        ddl_from_month.Focus();
                    }
                    else
                    {
                        string query = "select * from (select studentname,admissionserialnumber,class,Section,rollnumber,session,isnull((select sum(cast(dues as float)) from dbo.[Typewise_fee_collection] where admission_no=admission_registor.admissionserialnumber and session=admission_registor.session and parameter='MonthlyFee'),'0') as dues from dbo.[admission_registor]  where Class_id='" + ViewState["classid"].ToString() + "' and Session_id='" + ViewState["sessionIDs"].ToString() + "' and admissionserialnumber='" + ViewState["admSionNo"].ToString() + "') t";
                        DataTable dt = My.dataTable(query);
                        if (dt.Rows.Count != 0)
                        {
                            if (My.toDouble(dt.Rows[0]["dues"].ToString()) > 0)
                            {
                                Alertme("Can't allocate boarding with lunch to selected student due to previous dues.", "warning");
                            }
                            else
                            {
                                save_boarding_data();
                            }
                        }
                    }
                }
                else if (ViewState["Is_Edit"].ToString() == "1")
                {
                    if (ddl_from_month.SelectedItem.Text == "Select")
                    {
                        Alertme("Please select month.", "warning");
                        ddl_from_month.Focus();
                    }
                    else
                    {
                        string query = "select * from (select studentname,admissionserialnumber,class,Section,rollnumber,session,isnull((select sum(cast(dues as float)) from dbo.[Typewise_fee_collection] where admission_no=admission_registor.admissionserialnumber and session=admission_registor.session and parameter='MonthlyFee'),'0') as dues from dbo.[admission_registor]  where Class_id='" + ViewState["classid"].ToString() + "' and Session_id='" + ViewState["sessionIDs"].ToString() + "' and admissionserialnumber='" + ViewState["admSionNo"].ToString() + "') t";
                        DataTable dt = My.dataTable(query);
                        if (dt.Rows.Count != 0)
                        {
                            if (My.toDouble(dt.Rows[0]["dues"].ToString()) > 0)
                            {
                                Alertme("Can't allocate boarding with lunch to selected student due to previous dues.", "warning");
                            }
                            else
                            {
                                save_boarding_data();
                            }
                        }
                    }
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");

                }

            }
            catch (Exception ex)
            {
            }
        }

        private void save_boarding_data()
        {
            SqlCommand cmd;
            string query = "INSERT INTO Student_mapping_with_boarding_with_lunch (Session_id,Admission_no,Class_id,Month_name,Month_id,Created_by,Created_date,Created_idate,branch_id) values (@Session_id,@Admission_no,@Class_id,@Month_name,@Month_id,@Created_by,@Created_date,@Created_idate,@branch_id); INSERT INTO Student_mapping_with_boarding_with_lunch_history (Session_id,Admission_no,Class_id,Month_name,Month_id,Remark,Created_by,Created_date,Created_idate,branch_id) values (@Session_id,@Admission_no,@Class_id,@Month_name,@Month_id,@Remark,@Created_by,@Created_date,@Created_idate,@branch_id)";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Session_id", ViewState["sessionIDs"].ToString());
            cmd.Parameters.AddWithValue("@Class_id", ViewState["classid"].ToString());
            cmd.Parameters.AddWithValue("@Admission_no", ViewState["admSionNo"].ToString());
            cmd.Parameters.AddWithValue("@Month_name", ddl_from_month.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@Month_id", ddl_from_month.SelectedValue);
            cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
            cmd.Parameters.AddWithValue("@Created_date", mycode.date());
            cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());

            cmd.Parameters.AddWithValue("@Remark", "Boarding with lunch assigned.");
            cmd.Parameters.AddWithValue("@branch_id", ViewState["branch_id"].ToString());
            if (My.InsertUpdateData(cmd))
            {
                Alertme("Student has beed mapped with boarding with successfully.", "success");


                mycode.executequery("update admission_registor set is_applied_dayboarding=1,day_boarding_with_lunch=1 where admissionserialnumber='" + ViewState["admSionNo"].ToString() + "' and Session_id=" + ViewState["sessionIDs"].ToString() + "");





                empty_feilds();
            }
        }

        private void empty_feilds()
        {
            txt_admission_no.Text = "";
            txt_section.Text = "";
            txt_student_name.Text = "";
            txtrollnumber.Text = "";
            txtsection.Text = "";
            std_basic_infoS.Visible = false;
        }
    }
}