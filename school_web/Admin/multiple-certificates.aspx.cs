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
    public partial class multiple_certificates : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
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
                    if (!IsPostBack)
                    {
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();
                        string pagename_current = "certificate-creation.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["gcmid"] = "";
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        txt_issue_date.Text = mycode.date();
                        ViewState["stdnT"] = "0";
                        mycode.bind_all_ddl_with_id(ddl_session, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                        ddl_session.SelectedValue = My.get_session_id();
                        mycode.bind_all_ddl_with_id(ddlclass, "Select Course_Name,course_id from Add_course_table order by Position asc");

                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Sport_certificate");
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



        protected void btn_find_session_Click(object sender, EventArgs e)
        {
            if (ddl_session.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
            }
            else
            {
                string qry = "select * from dbo.[admission_registor] where Session_id='" + ddl_session.SelectedValue + "' and Transfer_Status in ('New','NT') and Status='1' and admissionserialnumber not IN  (SELECT Admission_no FROM Certificate_Master WHERE Admission_no=admission_registor.admissionserialnumber and Class_id=admission_registor.Class_id and Session_id=admission_registor.Session_id and Certificate_type='Sport') order by id desc";
                find_details(qry);
            }
        }

        #region FindStudenT
        protected void btn_find_admission_no_Click1(object sender, EventArgs e)
        {
            try
            {
                if (ddl_session.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session", "warning");
                    ddl_session.Focus();
                    return;
                }
                if (txt_admission_no.Text == "")
                {
                    Alertme("Please Enter Admission No", "warning");
                    txt_admission_no.Focus();
                    return;
                }
                string qry = "select top 1 * from dbo.[admission_registor] where admissionserialnumber='" + txt_admission_no.Text + "' and Session_id='" + ddl_session.SelectedValue + "' and Status='1'  order by id desc";//and admissionserialnumber not IN  (SELECT Admission_no FROM Certificate_Master WHERE Admission_no=admission_registor.admissionserialnumber and Class_id=admission_registor.Class_id and Session_id=admission_registor.Session_id and Certificate_type='Sport')
                find_details(qry);
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnfind_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_session.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session", "warning");
                    ddl_session.Focus();
                    return;
                }
                if (ddlclass.SelectedItem.Text == "Select")
                {
                    Alertme("Please select class.", "warning");
                    ddlclass.Focus();
                    return;
                }
                if (txtrollnumber.Text == "")
                {
                    Alertme("Please enter roll no.", "warning");
                    txtrollnumber.Focus();
                    return;
                }
                string query = "select * from dbo.[admission_registor] where rollnumber='" + txtrollnumber.Text + "'  and Session_id='" + ddl_session.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'";
                DataTable dt = My.dataTable(query);
                if (dt.Rows.Count == 0)
                {
                    rp_std.DataSource = null;
                    rp_std.DataBind();
                    Alertme("Data Not Found...", "warning");
                }
                else
                {
                    rp_std.DataSource = dt;
                    rp_std.DataBind();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalSTD();", true);
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void btn_find_name_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_session.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session", "warning");
                    ddl_session.Focus();
                    return;
                }
                if (txt_student_name.Text == "")
                {
                    Alertme("Please enter student name.", "warning");
                    txt_student_name.Focus();
                    return;
                }
                string query = "select * from admission_registor where studentname like '%" + txt_student_name.Text + "%' and   Status='1' and Session_id='" + ddl_session.SelectedValue + "' order by id asc";
                DataTable dt = My.dataTable(query);
                if (dt.Rows.Count == 0)
                {
                    rp_std.DataSource = null;
                    rp_std.DataBind();
                    Alertme("Data Not Found...", "warning");
                }
                else
                {
                    rp_std.DataSource = dt;
                    rp_std.DataBind();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalSTD();", true);
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
                Label lbl_session_id = (Label)row.FindControl("lbl_session_id");
                string query = "select top 1 * from dbo.[admission_registor] where admissionserialnumber='" + lbladmissionserialnumber.Text + "' and Session_id='" + lbl_session_id.Text + "' order by id desc";//and admissionserialnumber not IN  (SELECT Admission_no FROM Certificate_Master WHERE Admission_no=admission_registor.admissionserialnumber and Class_id=admission_registor.Class_id and Session_id=admission_registor.Session_id and Certificate_type='Sport')
                find_details(query);
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }



        private void find_details(string qryS)
        {
            ViewState["qryS"] = qryS;
            DataTable dt = My.dataTable(qryS);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data list exist or certificate already created.", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }

        #endregion


        protected void lnkSelectStd_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_add"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_studentname = (Label)row.FindControl("lbl_studentname");
                    Label lbl_class = (Label)row.FindControl("lbl_class");
                    Label lbl_adm_no = (Label)row.FindControl("lbl_adm_no");
                    Label lbl_session_id = (Label)row.FindControl("lbl_session_id");
                    Label lbl_class_id = (Label)row.FindControl("lbl_class_id");
                    Label lbl_dob = (Label)row.FindControl("lbl_dob");
                    Label lbl_gcmid = (Label)row.FindControl("lbl_gcmid");
                    ViewState["gcmid"] = lbl_gcmid.Text;
                    ViewState["adm_no"] = lbl_adm_no.Text;
                    ViewState["sessioN"] = lbl_session_id.Text;
                    ViewState["class_id"] = lbl_class_id.Text;
                    lbl_admisn_no.Text = lbl_adm_no.Text;
                    lbl_std_name.Text = lbl_studentname.Text;
                    lbl_classss.Text = lbl_class.Text;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                else if (ViewState["Is_Edit"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_studentname = (Label)row.FindControl("lbl_studentname");
                    Label lbl_class = (Label)row.FindControl("lbl_class");
                    Label lbl_adm_no = (Label)row.FindControl("lbl_adm_no");
                    Label lbl_session_id = (Label)row.FindControl("lbl_session_id");
                    Label lbl_class_id = (Label)row.FindControl("lbl_class_id");
                    Label lbl_dob = (Label)row.FindControl("lbl_dob");
                    Label lbl_gcmid = (Label)row.FindControl("lbl_gcmid");
                    ViewState["gcmid"] = lbl_gcmid.Text;
                    ViewState["adm_no"] = lbl_adm_no.Text;
                    ViewState["sessioN"] = lbl_session_id.Text;
                    ViewState["class_id"] = lbl_class_id.Text;
                    lbl_admisn_no.Text = lbl_adm_no.Text;
                    lbl_std_name.Text = lbl_studentname.Text;
                    lbl_classss.Text = lbl_class.Text;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
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

        protected void btn_create_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_certificate_type.SelectedValue == "0")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    Alertme("Please select certificate type.", "warning");
                    ddl_certificate_type.Focus();
                    return;
                }
                if (ddl_certificate_type.SelectedValue == "2")
                {
                    if (txt_securing.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                        Alertme("Please enter securing position.", "warning");
                        txt_securing.Focus();
                        return;
                    }
                    if (txt_competition_name.Text == "0")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                        Alertme("Please enter competition name.", "warning");
                        txt_competition_name.Focus();
                        return;
                    }
                }
                if (ddl_certificate_type.SelectedValue == "3")
                {
                    if (txt_securing.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                        Alertme("Please enter securing rank.", "warning");
                        txt_securing.Focus();
                        return;
                    }
                }


                if (txt_issue_date.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    Alertme("Please enter issue date.", "warning");
                    txt_issue_date.Focus();
                    return;
                }


                ///======================================
                string qryc = "select * from Certificate_master_multiple where Session_id=" + ViewState["sessioN"].ToString() + " and Class_id='" + ViewState["class_id"].ToString() + "' and Admisison_no='" + ViewState["adm_no"].ToString() + "' and Certificate_type_id='" + ddl_certificate_type.SelectedValue + "'";
                if (ddl_certificate_type.SelectedValue == "2")
                {
                    qryc = "select * from Certificate_master_multiple where Session_id=" + ViewState["sessioN"].ToString() + " and Class_id='" + ViewState["class_id"].ToString() + "' and Admisison_no='" + ViewState["adm_no"].ToString() + "' and Certificate_type_id='" + ddl_certificate_type.SelectedValue + "' and Competition_name='" + txt_competition_name.Text + "'";
                }
                SqlCommand cmd;
                string certificate_no = My.Sport_format("sport_sl");
                SqlDataAdapter ad = new SqlDataAdapter(qryc, My.conn);
                DataSet ds = new DataSet();
                ad.Fill(ds, "Certificate_master_multiple");
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    string query = "INSERT INTO Certificate_master_multiple (Session_id,Class_id,Admisison_no,Certificate_type_id,Certificate_type_name,Certificate_no,Securing_position_rank,Competition_name,Issue_date,Created_by,Created_date,Created_time,Created_idate) values (@Session_id,@Class_id,@Admisison_no,@Certificate_type_id,@Certificate_type_name,@Certificate_no,@Securing_position_rank,@Competition_name,@Issue_date,@Created_by,@Created_date,@Created_time,@Created_idate)";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Session_id", ViewState["sessioN"].ToString().ToString());
                    cmd.Parameters.AddWithValue("@Class_id", ViewState["class_id"].ToString());
                    cmd.Parameters.AddWithValue("@Admisison_no", ViewState["adm_no"].ToString());
                    cmd.Parameters.AddWithValue("@Certificate_type_name", ddl_certificate_type.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Certificate_type_id", ddl_certificate_type.SelectedValue);
                    cmd.Parameters.AddWithValue("@Certificate_no", certificate_no);
                    if (ddl_certificate_type.SelectedValue == "2")
                    {
                        cmd.Parameters.AddWithValue("@Securing_position_rank", txt_securing.Text);
                        cmd.Parameters.AddWithValue("@Competition_name", txt_competition_name.Text);
                    }
                    else if (ddl_certificate_type.SelectedValue == "3")
                    {
                        cmd.Parameters.AddWithValue("@Securing_position_rank", txt_securing.Text);
                        cmd.Parameters.AddWithValue("@Competition_name", "");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Securing_position_rank", "");
                        cmd.Parameters.AddWithValue("@Competition_name", "");
                    }

                    cmd.Parameters.AddWithValue("@Issue_date", txt_issue_date.Text);
                    cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                    cmd.Parameters.AddWithValue("@Created_time", mycode.idate());
                    cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
                    if (My.InsertUpdateData(cmd))
                    {
                        txt_competition_name.Text = "";
                        txt_securing.Text = "";
                        Alertme("certificate has been created successfully.", "success");
                        #region Comentted
                        //try
                        //{
                        //    string sub = "Sport Certificate";
                        //    string messge = "Dear student your Sport certificate has been generated please come to the school and collect the certificate.";
                        //    if (ViewState["gcmid"].ToString() != "")
                        //    {

                        //        Dictionary<String, String> ss = new Dictionary<string, string>();
                        //        ss["notification_id"] = Guid.NewGuid().ToString();
                        //        ss["message"] = messge;
                        //        ss["title"] = sub;
                        //        ss["messagetype"] = "Message";
                        //        ss["url"] = "";
                        //        ss["link_url"] = "";
                        //        ss["UserId"] = ViewState["adm_no"].ToString();
                        //        UsesCode.SendNotification(ViewState["gcmid"].ToString(), ss);
                        //    }
                        //}
                        //catch
                        //{
                        //}
                        #endregion

                        find_details(ViewState["qryS"].ToString());
                    }
                }
                else
                {
                    Alertme("Certificate already created for this student.", "warning");
                }
            }
            catch (Exception ex)
            {
            }
        }


        #region WebMethoD
        [WebMethod]
        public static List<string> GetRooPath(string PathRooT, string Session_id)
        {
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(My.conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct studentname from admission_registor where studentname LIKE '%'+@SearchMobNo+'%'   and Status='1' and Session_id='" + Session_id + "'";
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
                    cmd.CommandText = "select distinct admissionserialnumber from admission_registor where admissionserialnumber LIKE '%'+@SearchMobNo+'%'   and Status='1' and Session_id='" + Session_id + "'";
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