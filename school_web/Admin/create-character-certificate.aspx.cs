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
    public partial class create_character_certificate : System.Web.UI.Page
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
                        ViewState["gcmid"] = "";
                        ViewState["Userid"] = Session["Admin"].ToString();
                        string pagename_current = "certificate-creation.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);

                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        find_firm_dt();

                        ViewState["is_chrcter3"] = "0";
                        try
                        { 
                            DataTable dtc = My.dataTable("select Is_character_3 from Firm_Details");
                            if (dtc.Rows.Count > 0)
                            {
                                if (dtc.Rows[0][0].ToString() == "3" || dtc.Rows[0][0].ToString() == "True")
                                {
                                    ViewState["chrcterPage"] = "character-certificate3.aspx";
                                    ViewState["is_chrcter3"] = "1";
                                }
                                if (dtc.Rows[0][0].ToString() == "4")
                                {
                                    ViewState["chrcterPage"] = "character-certificate4.aspx";
                                    ViewState["is_chrcter3"] = "1";
                                }
                                if (dtc.Rows[0][0].ToString() == "5")
                                {
                                    ViewState["chrcterPage"] = "character-certificateBD.aspx";
                                    ViewState["is_chrcter3"] = "1";
                                } 
                            }
                        }
                        catch (Exception ex) { }

                        ViewState["stdnT"] = "0";
                        mycode.bind_all_ddl_with_id(ddl_session, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc ");
                        ddl_session.SelectedValue = My.get_session_id();
                        mycode.bind_all_ddl_with_id(ddlclass, "Select Course_Name,course_id from Add_course_table order by Position asc");

                        My.bind_ddl_select(ddl_old_class, "Select Course_Name from Add_course_table order by Position asc");
                        Bind_all_pending_list_for_approved();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Character_certificate");
            }
        }

        private void find_firm_dt()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details");
            if (dt.Rows.Count > 0)
            {
                ViewState["firm_id"] = dt.Rows[0]["firm_id"].ToString();
            }
        }


        #region Approval For pending character certificate admisn
        private void Bind_all_pending_list_for_approved()
        {
            string query = " select ar.*,aft.Reply_remarks,format(aft.Reply_datetime, 'dd/MM/yyyy hh:mm:ss tt') as Reply_datetime1,aft.Id from dbo.[admission_registor] ar join Apply_For_TC aft on aft.Admission_no = ar.admissionserialnumber and aft.Session_id = ar.Session_id  where ar.Session_id = '" + ddl_session.SelectedValue + "' and ar.Transfer_Status in ('New', 'NT') and ar.Status = '1' and ar.admissionserialnumber not IN(SELECT Admission_no FROM Certificate_Master WHERE Admission_no = ar.admissionserialnumber and Class_id = ar.Class_id and Session_id = ar.Session_id and Certificate_type = 'Character') and ar.admissionserialnumber in(Select Admission_no from Apply_For_TC where Apply_For = 'Character Certificate') and aft.Apply_For = 'Character Certificate'  order by ar.id desc";
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

                string qry = "select top 1 * from dbo.[admission_registor] where admissionserialnumber='" + lbl_admissionserialnumber.Text + "' and   Status='1' and admissionserialnumber not IN  (SELECT Admission_no FROM Certificate_Master WHERE Admission_no=admission_registor.admissionserialnumber and Class_id=admission_registor.Class_id and Session_id=admission_registor.Session_id and Certificate_type='Character') order by id desc";
                find_details(qry);

            }
            catch
            {
            }
        }
        #endregion










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
                string qry = "select * from dbo.[admission_registor] where Session_id='" + ddl_session.SelectedValue + "' and Transfer_Status in ('New','NT') and Status='1' and admissionserialnumber not IN  (SELECT Admission_no FROM Certificate_Master WHERE Admission_no=admission_registor.admissionserialnumber and Class_id=admission_registor.Class_id and Session_id=admission_registor.Session_id and Certificate_type='Character') order by id desc";
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
                    Alertme("Please select session.", "warning");
                    ddl_session.Focus();
                    return;
                }
                if (txt_admission_no.Text == "")
                {
                    Alertme("Please Enter Admission No", "warning");
                    txt_admission_no.Focus();
                    return;
                }
                string qry = "select top 1 * from dbo.[admission_registor] where admissionserialnumber='" + txt_admission_no.Text + "' and Session_id='" + ddl_session.SelectedValue + "' and  Status='1' and admissionserialnumber not IN  (SELECT Admission_no FROM Certificate_Master WHERE Admission_no=admission_registor.admissionserialnumber and Class_id=admission_registor.Class_id and Session_id=admission_registor.Session_id and Certificate_type='Character') order by id desc";
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
                    Alertme("Please select session.", "warning");
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
                    Alertme("Please select session.", "warning");
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
                string query = "select top 1 * from admission_registor where admissionserialnumber='" + lbladmissionserialnumber.Text + "'  and Session_id='" + lbl_session_id.Text + "' and admissionserialnumber not IN  (SELECT Admission_no FROM Certificate_Master WHERE Admission_no=admission_registor.admissionserialnumber and Class_id=admission_registor.Class_id and Session_id=admission_registor.Session_id and Certificate_type='Character') order by id desc";
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
            }
        }

        #endregion

        protected void lnkCreate_Click(object sender, EventArgs e)
        {
            try
            {
                int is_date_select = 0;
                try
                {
                    DataTable dtc = My.dataTable("select Is_character_3 from Firm_Details");
                    if (dtc.Rows.Count > 0)
                    {
                        if (dtc.Rows[0][0].ToString() == "3" || dtc.Rows[0][0].ToString() == "4" || dtc.Rows[0][0].ToString() == "True")
                        {
                            is_date_select = 1;
                        }
                    }
                }
                catch (Exception ex) { }

                if (is_date_select == 1)
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_adm_no = (Label)row.FindControl("lbl_adm_no");
                    Label lbl_session_id = (Label)row.FindControl("lbl_session_id");
                    Label lbl_class_id = (Label)row.FindControl("lbl_class_id");
                    Label lbl_gcmid = (Label)row.FindControl("lbl_gcmid");
                    ViewState["gcmid"] = lbl_gcmid.Text;

                    ViewState["adm_no"] = lbl_adm_no.Text;
                    ViewState["sessioN"] = lbl_session_id.Text;
                    ViewState["class_id"] = lbl_class_id.Text;


                    ViewState["adm_no"] = lbl_adm_no.Text;
                    ViewState["sessioN"] = lbl_session_id.Text;
                    ViewState["class_id"] = lbl_class_id.Text;
                    string query = "select * from dbo.[admission_registor] where admissionserialnumber='" + lbl_adm_no.Text + "' and Session_id='" + lbl_session_id.Text + "' and Class_id='" + lbl_class_id.Text + "' order by id desc";
                    DataTable dt = My.dataTable(query);
                    if (dt.Rows.Count == 0)
                    {
                        Repeater1.DataSource = null;
                        Repeater1.DataBind();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalCertificate();", true);
                        Repeater1.DataSource = dt;
                        Repeater1.DataBind();
                        txt_date_of_admission.Text = dt.Rows[0]["dateofadmission"].ToString();
                        string class_name = My.get_class_name_to_from_class_id(lbl_class_id.Text);
                        try
                        {
                            ddl_old_class.Text = lbl_class_id.Text;
                        }
                        catch (Exception ex)
                        {
                        }
                        txt_passout_date.Text = mycode.date();
                        txt_uid_no.Text = dt.Rows[0]["UID_no"].ToString();
                    }
                }
                else
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_adm_no = (Label)row.FindControl("lbl_adm_no");
                    Label lbl_session_id = (Label)row.FindControl("lbl_session_id");
                    Label lbl_class_id = (Label)row.FindControl("lbl_class_id");
                    Label lbl_gcmid = (Label)row.FindControl("lbl_gcmid");
                    Label lbl_dateofadmission = (Label)row.FindControl("lbl_dateofadmission");

                    ViewState["gcmid"] = lbl_gcmid.Text;

                    ViewState["adm_no"] = lbl_adm_no.Text;
                    ViewState["sessioN"] = lbl_session_id.Text;
                    ViewState["class_id"] = lbl_class_id.Text;
                    string certificate_no = My.character_format("Certificate_character_sl");
                    if (ViewState["firm_id"].ToString() == "NAVY-001")
                    {
                        if (certificate_no.Length == 1)
                        {
                            certificate_no = "000" + certificate_no;
                        }
                        if (certificate_no.Length == 2)
                        {
                            certificate_no = "00" + certificate_no;
                        }
                        if (certificate_no.Length == 3)
                        {
                            certificate_no = "0" + certificate_no;
                        }
                        string[] stringSeparatorss = new string[] { "-" };
                        string[] arrs = ViewState["sessioN"].ToString().Split(stringSeparatorss, StringSplitOptions.None);
                        string yearF = arrs[0];
                        certificate_no = yearF + "/" + certificate_no;
                    }

                    SqlDataAdapter ad = new SqlDataAdapter("select * from Certificate_Master where Session_id=" + ViewState["sessioN"].ToString() + " and Class_id='" + ViewState["class_id"].ToString() + "' and Admission_no='" + ViewState["adm_no"].ToString() + "' and Certificate_type='Character'", My.conn);
                    DataSet ds = new DataSet();
                    ad.Fill(ds, "Certificate_Master");
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count == 0)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Session_id"] = ViewState["sessioN"].ToString();
                        dr["Class_id"] = ViewState["class_id"].ToString();
                        dr["Admission_no"] = ViewState["adm_no"].ToString();
                        dr["Certificate_type"] = "Character";
                        dr["Certificate_no"] = certificate_no;
                        dr["Create_date"] = mycode.date();
                        dr["Create_idate"] = mycode.idate();
                        dr["User_id"] = Session["Admin"].ToString();
                        dr["Firm_id"] = Session["firm"].ToString();
                        dr["Issue_date"] = mycode.date();
                        dr["Date_of_admission"] = lbl_dateofadmission.Text;
                        dt.Rows.Add(dr);
                        SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                        ad.Update(dt);
                        string qry = "select top 1 * from dbo.[admission_registor] where admissionserialnumber='" + ViewState["adm_no"].ToString() + "' and Session_id='" + ViewState["sessioN"].ToString() + "' and admissionserialnumber not IN  (SELECT Admission_no FROM Certificate_Master WHERE Admission_no=admission_registor.admissionserialnumber and Class_id=admission_registor.Class_id and Session_id=admission_registor.Session_id and Certificate_type='Character') order by id desc";
                        find_details(qry);

                        if (ViewState["firm_id"].ToString() == "NAVY-001")
                        {
                            Response.Redirect("slip/bonafide/character.aspx?adm_no=" + ViewState["adm_no"].ToString() + "&clssid=" + ViewState["class_id"].ToString() + "&sessnid=" + ViewState["sessioN"].ToString() + "&crtificateno=" + certificate_no, false);
                        }
                        else
                        {
                            if (ViewState["is_chrcter3"].ToString() == "")
                            {
                                Response.Redirect("slip/character-certificate.aspx?adm_no=" + ViewState["adm_no"].ToString() + "&clssid=" + ViewState["class_id"].ToString() + "&sessnid=" + ViewState["sessioN"].ToString() + "&crtificateno=" + certificate_no, false);
                            }
                            else
                            {
                                Response.Redirect("slip/" + ViewState["chrcterPage"].ToString() + "?adm_no=" + ViewState["adm_no"].ToString() + "&clssid=" + ViewState["class_id"].ToString() + "&sessnid=" + ViewState["sessioN"].ToString() + "&crtificateno=" + certificate_no, false);
                            }

                        }
                       
                        
                    }
                    else
                    {
                        Alertme("Certificate already created of this student.", "warning");
                    }

                }
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
}

protected void btn_create_certificate_Click(object sender, EventArgs e)
{
    try
    {
        if (ddl_old_class.Text == "Select")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalCertificate();", true);
            ddl_old_class.Focus();
            Alertme("Please select class.", "warning");
        }
        else if (txt_date_of_admission.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalCertificate();", true);
            txt_date_of_admission.Focus();
            Alertme("Please enter date of admission.", "warning");
        }
        else if (txt_passout_date.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalCertificate();", true);
            txt_passout_date.Focus();
            Alertme("Please enter passout date.", "warning");
        }
        else
        {
            create_certificates();
        }
    }
    catch (Exception ex)
    {
    }
}

private void create_certificates()
{
    string certificate_no = My.character_format("Certificate_character_sl");
    SqlDataAdapter ad = new SqlDataAdapter("select * from Certificate_Master where Session_id=" + ViewState["sessioN"].ToString() + " and Class_id='" + ViewState["class_id"].ToString() + "' and Admission_no='" + ViewState["adm_no"].ToString() + "' and Certificate_type='Character'", My.conn);
    DataSet ds = new DataSet();
    ad.Fill(ds, "Certificate_Master");
    DataTable dt = ds.Tables[0];
    if (dt.Rows.Count == 0)
    {
        DataRow dr = dt.NewRow();
        dr["Session_id"] = ViewState["sessioN"].ToString();
        dr["Class_id"] = ViewState["class_id"].ToString();
        dr["Admission_no"] = ViewState["adm_no"].ToString();
        dr["Certificate_type"] = "Character";
        dr["Certificate_no"] = certificate_no;
        dr["Create_date"] = mycode.date();
        dr["Create_idate"] = mycode.idate();
        dr["User_id"] = Session["Admin"].ToString();
        dr["Firm_id"] = Session["firm"].ToString();
        dr["Date_of_admission"] = txt_date_of_admission.Text;
        dr["Passout_date"] = txt_passout_date.Text;
        dr["Old_class_name"] = ddl_old_class.Text;
        dr["Issue_date"] = mycode.date();
        dt.Rows.Add(dr);
        SqlCommandBuilder cb = new SqlCommandBuilder(ad);
        ad.Update(dt);

        if (txt_uid_no.Text == "") { }
        else
        {
            My.exeSql("update admission_registor set UID_no='" + txt_uid_no.Text + "' where admissionserialnumber='" + ViewState["adm_no"].ToString() + "' and Session_id='" + ViewState["sessioN"].ToString() + "' ");
        }
        //string qry = "select top 1 * from dbo.[admission_registor] where admissionserialnumber='" + ViewState["adm_no"].ToString() + "' and Session_id='" + ViewState["sessioN"].ToString() + "' and admissionserialnumber not IN  (SELECT Admission_no FROM Certificate_Master WHERE Admission_no=admission_registor.admissionserialnumber and Class_id=admission_registor.Class_id and Session_id=admission_registor.Session_id and Certificate_type='Character') order by id desc";
        //find_details(qry); 
        // send puh message

        try
        {
            string sub = "Character Certificate";
            string messge = "Dear student your character certificate has been generated please come to the school and collect the certificate.";
            if (ViewState["gcmid"].ToString() != "")
            {

                Dictionary<String, String> ss = new Dictionary<string, string>();
                ss["notification_id"] = Guid.NewGuid().ToString();
                ss["message"] = messge;
                ss["title"] = sub;
                ss["messagetype"] = "Message";
                ss["url"] = "";
                ss["link_url"] = "";
                ss["UserId"] = ViewState["adm_no"].ToString();
                UsesCode.SendNotification(ViewState["gcmid"].ToString(), ss);
            }
        }
        catch (Exception ex)
        {
        }


        if (ViewState["is_chrcter3"].ToString() == "1")
        {
            Response.Redirect("slip/" + ViewState["chrcterPage"].ToString() + "?adm_no=" + ViewState["adm_no"].ToString() + "&clssid=" + ViewState["class_id"].ToString() + "&sessnid=" + ViewState["sessioN"].ToString() + "&crtificateno=" + certificate_no, false);
        }
        else
        {
            Response.Redirect("slip/character-certificate.aspx?adm_no=" + ViewState["adm_no"].ToString() + "&clssid=" + ViewState["class_id"].ToString() + "&sessnid=" + ViewState["sessioN"].ToString() + "&crtificateno=" + certificate_no, false);
        }
    }
    else
    {
        Alertme("Certificate already created of this student.", "warning");
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