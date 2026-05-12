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
    public partial class create_bonafide_certificate : System.Web.UI.Page
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
                        DataTable dtF = My.dataTable("select firm_name,firm_id from Firm_Details");
                        ViewState["firm_id"] = dtF.Rows[0]["firm_id"].ToString();
                        string pagename_current = "certificate-creation.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);

                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];

                        ViewState["gcmid"] = "";
                        ViewState["stdnT"] = "0";
                        mycode.bind_all_ddl_with_id(ddl_session, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                        ddl_session.SelectedValue = My.get_session_id();
                        mycode.bind_all_ddl_with_id(ddlclass, "Select Course_Name,course_id from Add_course_table order by Position asc");
                        Bind_all_pending_list_for_approved();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "DOB_certificate");
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

        #region Approval For pending character certificate admisn
        private void Bind_all_pending_list_for_approved()
        {
            string query = " select ar.*,aft.Reply_remarks,format(aft.Reply_datetime, 'dd/MM/yyyy hh:mm:ss tt') as Reply_datetime1,aft.Id from dbo.[admission_registor] ar join Apply_For_TC aft on aft.Admission_no = ar.admissionserialnumber and aft.Session_id = ar.Session_id  where ar.Session_id = '" + ddl_session.SelectedValue + "' and ar.Transfer_Status in ('New', 'NT') and ar.Status = '1' and ar.admissionserialnumber not IN(SELECT Admission_no FROM Certificate_Master WHERE Admission_no=ar.admissionserialnumber and Class_id=ar.Class_id and Session_id=ar.Session_id and Certificate_type='Income') and ar.admissionserialnumber in(Select Admission_no from Apply_For_TC where Apply_For = 'Income Certificate') and aft.Apply_For = 'Income Certificate'  order by ar.id desc";
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count == 0)
            {
                pendingforgeneratedata.Visible = false;
                Repeater2.DataSource = null;
                Repeater2.DataBind();
            }
            else
            {
                pendingforgeneratedata.Visible = true;
                Repeater2.DataSource = dt;
                Repeater2.DataBind();

            }

        }
        protected void lnk_process_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                Label lbl_admissionserialnumber = (Label)row.FindControl("lbl_admissionserialnumber");
                Label lbl_studentname = (Label)row.FindControl("lbl_studentname");
                ViewState["id_process"] = lbl_Id.Text;

                string qry = "select top 1 * from dbo.[admission_registor] where admissionserialnumber='" + lbl_admissionserialnumber.Text + "' and   Status='1' and admissionserialnumber not IN  (SELECT Admission_no FROM Certificate_Master WHERE Admission_no=admission_registor.admissionserialnumber and Class_id=admission_registor.Class_id and Session_id=admission_registor.Session_id and Certificate_type='Income') order by id desc";
                find_details(qry);

            }
            catch
            {
            }
        }
        #endregion

        protected void btn_find_session_Click(object sender, EventArgs e)
        {
            if (ddl_session.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
            }
            else
            {
                string qry = "select * from dbo.[admission_registor] where Session_id='" + ddl_session.SelectedValue + "' and Transfer_Status in ('New','NT') and Status='1' and admissionserialnumber not IN  (SELECT Admission_no FROM Certificate_Master WHERE Admission_no=admission_registor.admissionserialnumber and Class_id=admission_registor.Class_id and Session_id=admission_registor.Session_id and Certificate_type='Income') order by id desc";
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
                string qry = "select top 1 * from dbo.[admission_registor] where admissionserialnumber='" + txt_admission_no.Text + "' and Session_id='" + ddl_session.SelectedValue + "' and  Status='1' and admissionserialnumber not IN  (SELECT Admission_no FROM Certificate_Master WHERE Admission_no=admission_registor.admissionserialnumber and Class_id=admission_registor.Class_id and Session_id=admission_registor.Session_id and Certificate_type='Income') order by id desc";
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
                string query = "select top 1 * from dbo.[admission_registor] where admissionserialnumber='" + lbladmissionserialnumber.Text + "'  and Session_id='" + lbl_session_id.Text + "' and admissionserialnumber not IN  (SELECT Admission_no FROM Certificate_Master WHERE Admission_no=admission_registor.admissionserialnumber and Class_id=admission_registor.Class_id and Session_id=admission_registor.Session_id and Certificate_type='Income') order by id desc";
                find_details(query);
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }



        private void find_details(string qryS)
        {
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
                //txt_admission_no.Text = dt.Rows[0]["admissionserialnumber"].ToString();
                //ddlclass.SelectedValue = dt.Rows[0]["Class_id"].ToString();
                //txtrollnumber.Text = dt.Rows[0]["rollnumber"].ToString();
                //txt_student_name.Text = dt.Rows[0]["studentname"].ToString();
            }
        }

        #endregion


        protected void lnkSelectStd_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_add"].ToString() == "1" || ViewState["Is_Edit"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_studentname = (Label)row.FindControl("lbl_studentname");
                    Label lbl_class = (Label)row.FindControl("lbl_class");
                    Label lbl_adm_no = (Label)row.FindControl("lbl_adm_no");
                    Label lbl_session_id = (Label)row.FindControl("lbl_session_id");
                    Label lbl_class_id = (Label)row.FindControl("lbl_class_id");
                    Label lbl_session_name = (Label)row.FindControl("lbl_session");
                    Label lbl_vill_city = (Label)row.FindControl("lbl_vill_city");
                    Label lbl_post_office = (Label)row.FindControl("lbl_post_office");
                    Label lbl_police_station = (Label)row.FindControl("lbl_police_station");
                    Label lbl_district = (Label)row.FindControl("lbl_district");
                    Label lbl_pincode = (Label)row.FindControl("lbl_pincode");
                    Label lbl_dob = (Label)row.FindControl("lbl_dob");
                    Label lbl_gcmid = (Label)row.FindControl("lbl_gcmid");
                    Label lbl_address = (Label)row.FindControl("lbl_address");
                    Label lbl_father_name = (Label)row.FindControl("lbl_father_name");
                    Label lbl_dateofadmission = (Label)row.FindControl("lbl_dateofadmission");

                    ViewState["gcmid"] = lbl_gcmid.Text;
                    ViewState["adm_no"] = lbl_adm_no.Text;
                    ViewState["sessioN"] = lbl_session_id.Text;
                    ViewState["class_id"] = lbl_class_id.Text;
                    ViewState["session_name"] = lbl_session_name.Text;

                    txt_student_name_c.Text = lbl_studentname.Text;
                    txt_guardian_name.Text = lbl_father_name.Text;
                    txt_class_grade.Text = lbl_class.Text;
                    txt_class_c.Text = lbl_class.Text;
                    txt_year.Text = lbl_session_name.Text;
                    txt_dob_c.Text = lbl_dob.Text;
                    txt_admission_no_c.Text = lbl_adm_no.Text;
                    txt_student_name_c1.Text = lbl_studentname.Text;
                    txt_date_of_admission_c.Text = lbl_dateofadmission.Text;
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

        protected void btn_save_bona_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_student_name_c.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    Alertme("Please enter student name.", "warning");
                    txt_student_name_c.Focus();
                }
                else if (txt_guardian_name.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    Alertme("Please enter parent's/guardian's name.", "warning");
                    txt_guardian_name.Focus();
                }
                else if (txt_class_grade.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    Alertme("Please enter class/grade/year.", "warning");
                    txt_class_grade.Focus();
                }
                else if (txt_class_c.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    Alertme("Please enter class name.", "warning");
                    txt_class_c.Focus();
                }
                else if (txt_year.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    Alertme("Please enter year.", "warning");
                    txt_year.Focus();
                }
                else if (txt_dob_c.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    Alertme("Please enter date of birth.", "warning");
                    txt_dob_c.Focus();
                }
                else if (txt_student_name_c1.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    Alertme("Please enter student name.", "warning");
                    txt_student_name_c1.Focus();
                }
                else if (txt_date_of_admission_c.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    Alertme("Please enter date of admission.", "warning");
                    txt_date_of_admission_c.Focus();
                }
                else if (txt_remarks.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    Alertme("Please enter remarks.", "warning");
                    txt_remarks.Focus();
                }
                else
                {
                    string certificate_no = My.bonafide_format("Bonafide_sl");
                    SqlDataAdapter ad = new SqlDataAdapter("select * from Bonafied_certificate where Session_id=" + ViewState["sessioN"].ToString() + " and Class_id='" + ViewState["class_id"].ToString() + "' and Admission_no='" + ViewState["adm_no"].ToString() + "'", My.conn);
                    DataSet ds = new DataSet();
                    ad.Fill(ds, "Bonafied_certificate");
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count == 0)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Certificate_no"] = certificate_no;
                        dr["Session_id"] = ViewState["sessioN"].ToString();
                        dr["Class_id"] = ViewState["class_id"].ToString();
                        dr["Admission_no"] = ViewState["adm_no"].ToString();
                        dr["Student_name1"] = txt_student_name_c.Text;
                        dr["Father_name"] = txt_guardian_name.Text;
                        dr["Class_1"] = txt_class_grade.Text;
                        dr["Class_2"] = txt_class_c.Text;  
                        dr["Year"] = txt_year.Text;
                        dr["Date_of_birth"] = txt_dob_c.Text;
                        dr["Student_name2"] = txt_student_name_c1.Text;
                        dr["Date_of_admission"] = txt_date_of_admission_c.Text;
                        dr["Remark"] = txt_remarks.Text;
                        dr["Created_by"] = Session["Admin"].ToString();
                        dr["Created_date"] = mycode.date();
                        dr["Created_idate"] = mycode.idate();
                        dt.Rows.Add(dr);
                        SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                        ad.Update(dt);
                        Response.Redirect("slip/bonafide-certificate.aspx?adm_no=" + ViewState["adm_no"].ToString() + "&clssid=" + ViewState["class_id"].ToString() + "&sessnid=" + ViewState["sessioN"].ToString(), false);
                    }
                    else
                    {
                        SqlCommand cmd;
                        string query = "Update Bonafied_certificate set Student_name1=@Student_name1,Father_name=@Father_name,Class_1=@Class_1,Class_2=@Class_2,Year=@Year,Date_of_birth=@Date_of_birth,Student_name2=@Student_name2,Date_of_admission=@Date_of_admission,Remark=@Remark,Updated_by=@Updated_by,Updated_date=@Updated_date,Updated_idate=@Updated_idate where Id = '" + hd_id.Value + "'";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@Session_id", ViewState["sessioN"].ToString());
                        cmd.Parameters.AddWithValue("@Class_id", ViewState["class_id"].ToString());
                        cmd.Parameters.AddWithValue("@Admission_no", ViewState["adm_no"].ToString());
                        cmd.Parameters.AddWithValue("@Student_name1", txt_student_name_c.Text);
                        cmd.Parameters.AddWithValue("@Father_name", txt_guardian_name.Text);
                        cmd.Parameters.AddWithValue("@Class_1", txt_class_grade.Text);
                        cmd.Parameters.AddWithValue("@Class_2", txt_class_c.Text);
                        cmd.Parameters.AddWithValue("@Year", txt_year.Text);
                        cmd.Parameters.AddWithValue("@Date_of_birth", txt_dob_c.Text);
                        cmd.Parameters.AddWithValue("@Student_name2", txt_student_name_c1.Text);
                        cmd.Parameters.AddWithValue("@Date_of_admission", txt_date_of_admission_c.Text);
                        cmd.Parameters.AddWithValue("@Remark", txt_remarks.Text);
                        cmd.Parameters.AddWithValue("@Updated_by", ViewState["Userid"].ToString());
                        cmd.Parameters.AddWithValue("@Updated_date", mycode.date());
                        cmd.Parameters.AddWithValue("@Updated_idate", mycode.idate());
                        if (My.InsertUpdateData(cmd))
                        {
                            Response.Redirect("slip/bonafide-certificate.aspx?adm_no=" + ViewState["adm_no"].ToString() + "&clssid=" + ViewState["class_id"].ToString() + "&sessnid=" + ViewState["sessioN"].ToString(), false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}