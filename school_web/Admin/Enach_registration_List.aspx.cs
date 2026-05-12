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
    public partial class Enach_registration_List : System.Web.UI.Page
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

                        find_firm_details();
                        mycode.bind_all_ddl_with_id_cap_All(ddlsession, "Select  Session,session_id from session_details order by Session desc");
                        ddlsession.SelectedValue = My.get_session_id();

                        mycode.bind_all_ddl_with_id_cap_All(ddlclass, "Select Course_Name,course_id from Add_course_table order by Position");
                        ddlclass.SelectedValue = "0";
                        ;

                        bind_all_data();
                        ViewState["sessionid"] = My.get_session_id();
                        ViewState["flag"] = "0";
                      //  string pagename_current = Path.GetFileName("Enach_registration_List.aspx");
                      //  Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] ="1";
                        ViewState["Is_delete"] = "1";
                        ViewState["Is_Download"] = "1";
                        ViewState["Is_Print"] = "1";
                        ViewState["Is_add"] = "1";
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Active_Inactive_Student");
            }
        }

        private void bind_all_data()
        {
            if (ddlclass.SelectedItem.Text == "ALL")
            {
                bind_grd_view("select ener.*,ar.session,ar.admissionserialnumber,ar.class,Section,ar.rollnumber,ar.studentname,ar.Status,ar.fathername,ar.mobilenumber,ar.Session_id,ar.Class_id,ar.mothername,ar.dob,ar.dateofadmission,ar.gender,ar.blood_group,ar.careof,ar.email_id,ar.father_mob from admission_registor ar join Add_course_table ac on ac.course_id=ar.Class_id join  enach_registration  ener on ener.Session_id=ar.Session_id and ener.admissionserialnumber=ar.admissionserialnumber where(ar.Transfer_Status= 'New' or ar.Transfer_Status= 'NT') and ar.StudentStatus='AV'    and ar.Status= '1' and ar.Session_id = " + ddlsession.SelectedValue + "  order by ac.Position, ar.Section, ar.rollnumber asc");
            }
            else
            {

                bind_grd_view("select ener.*,ar.session,ar.admissionserialnumber,ar.class,Section,ar.rollnumber,ar.studentname,ar.Status,ar.fathername,ar.mobilenumber,ar.Session_id,ar.Class_id,ar.mothername,ar.dob,ar.dateofadmission,ar.gender,ar.blood_group,ar.careof,ar.email_id,ar.father_mob from admission_registor ar join Add_course_table ac on ac.course_id=ar.Class_id join  enach_registration  ener on ener.Session_id=ar.Session_id and ener.admissionserialnumber=ar.admissionserialnumber where(ar.Transfer_Status= 'New' or ar.Transfer_Status= 'NT') and ar.StudentStatus='AV'    and ar.Status= '1' and ar.Session_id = " + ddlsession.SelectedValue + "  and ar.Class_id=" + ddlclass.SelectedValue + "  order by ac.Position, ar.Section, ar.rollnumber asc");


               
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
        private void find_firm_details()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                imglogo.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();

                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();
            }
        }
        private void bind_grd_view(string query)
        {
            ViewState["query"] = query;
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            { 
                Alertme("Sorry there are no data list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
               
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }
        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                find_by_class();
                ViewState["flag"] = "1";
            }
            catch (Exception ex)
            {
            }
        }
        private void find_by_class()
        {
            if (ddlsession.SelectedItem.Text == "ALL")
            {
                if (ddlclass.SelectedItem.Text == "ALL")
                {

                    bind_grd_view("select ener.*,ar.session,ar.admissionserialnumber,ar.class,Section,ar.rollnumber,ar.studentname,ar.Status,ar.fathername,ar.mobilenumber,ar.Session_id,ar.Class_id,ar.mothername,ar.dob,ar.dateofadmission,ar.gender,ar.blood_group,ar.careof,ar.email_id,ar.father_mob from admission_registor ar join Add_course_table ac on ac.course_id=ar.Class_id join  enach_registration  ener on ener.Session_id=ar.Session_id and ener.admissionserialnumber=ar.admissionserialnumber where(ar.Transfer_Status= 'New' or ar.Transfer_Status= 'NT') and ar.StudentStatus='AV'    and ar.Status= '1' and ar.Session_id = " + ddlsession.SelectedValue + "  order by ac.Position, ar.Section, ar.rollnumber asc");


                }
                else if (ddlclass.SelectedItem.Text != "ALL")
                {
                    bind_grd_view("select ener.*,ar.session,ar.admissionserialnumber,ar.class,Section,ar.rollnumber,ar.studentname,ar.Status,ar.fathername,ar.mobilenumber,ar.Session_id,ar.Class_id,ar.mothername,ar.dob,ar.dateofadmission,ar.gender,ar.blood_group,ar.careof,ar.email_id,ar.father_mob from admission_registor ar join Add_course_table ac on ac.course_id=ar.Class_id join  enach_registration  ener on ener.Session_id=ar.Session_id and ener.admissionserialnumber=ar.admissionserialnumber where(ar.Transfer_Status= 'New' or ar.Transfer_Status= 'NT') and ar.StudentStatus='AV'    and ar.Status= '1'  ar.Class_id=" + ddlclass.SelectedValue + "  order by ac.Position, ar.Section, ar.rollnumber asc");


                
                }
                else
                {
                    bind_grd_view("select ener.*,ar.session,ar.admissionserialnumber,ar.class,Section,ar.rollnumber,ar.studentname,ar.Status,ar.fathername,ar.mobilenumber,ar.Session_id,ar.Class_id,ar.mothername,ar.dob,ar.dateofadmission,ar.gender,ar.blood_group,ar.careof,ar.email_id,ar.father_mob from admission_registor ar join Add_course_table ac on ac.course_id=ar.Class_id join  enach_registration  ener on ener.Session_id=ar.Session_id and ener.admissionserialnumber=ar.admissionserialnumber where(ar.Transfer_Status= 'New' or ar.Transfer_Status= 'NT') and ar.StudentStatus='AV'    and ar.Status= '1'  ar.Class_id=" + ddlclass.SelectedValue + "  order by ac.Position, ar.Section, ar.rollnumber asc");
 
                }
            }
            else if (ddlsession.SelectedItem.Text != "ALL" && ddlclass.SelectedItem.Text == "ALL")
            {
                bind_grd_view("select ener.*,ar.session,ar.admissionserialnumber,ar.class,Section,ar.rollnumber,ar.studentname,ar.Status,ar.fathername,ar.mobilenumber,ar.Session_id,ar.Class_id,ar.mothername,ar.dob,ar.dateofadmission,ar.gender,ar.blood_group,ar.careof,ar.email_id,ar.father_mob from admission_registor ar join Add_course_table ac on ac.course_id=ar.Class_id join  enach_registration  ener on ener.Session_id=ar.Session_id and ener.admissionserialnumber=ar.admissionserialnumber where(ar.Transfer_Status= 'New' or ar.Transfer_Status= 'NT') and ar.StudentStatus='AV'    and ar.Status= '1' and ar.Session_id = " + ddlsession.SelectedValue + "  order by ac.Position, ar.Section, ar.rollnumber asc");


            }
            else if (ddlsession.SelectedItem.Text != "ALL" && ddlclass.SelectedItem.Text != "ALL" )
            {
                bind_grd_view("select ener.*,ar.session,ar.admissionserialnumber,ar.class,Section,ar.rollnumber,ar.studentname,ar.Status,ar.fathername,ar.mobilenumber,ar.Session_id,ar.Class_id,ar.mothername,ar.dob,ar.dateofadmission,ar.gender,ar.blood_group,ar.careof,ar.email_id,ar.father_mob from admission_registor ar join Add_course_table ac on ac.course_id=ar.Class_id join  enach_registration  ener on ener.Session_id=ar.Session_id and ener.admissionserialnumber=ar.admissionserialnumber where(ar.Transfer_Status= 'New' or ar.Transfer_Status= 'NT') and ar.StudentStatus='AV'    and ar.Status= '1' and ar.Session_id = " + ddlsession.SelectedValue + " and ar.Class_id=" + ddlclass.SelectedValue + "   order by ac.Position, ar.Section, ar.rollnumber asc");


                
            }
            else
            {
                bind_grd_view("select ener.*,ar.session,ar.admissionserialnumber,ar.class,Section,ar.rollnumber,ar.studentname,ar.Status,ar.fathername,ar.mobilenumber,ar.Session_id,ar.Class_id,ar.mothername,ar.dob,ar.dateofadmission,ar.gender,ar.blood_group,ar.careof,ar.email_id,ar.father_mob from admission_registor ar join Add_course_table ac on ac.course_id=ar.Class_id join  enach_registration  ener on ener.Session_id=ar.Session_id and ener.admissionserialnumber=ar.admissionserialnumber where(ar.Transfer_Status= 'New' or ar.Transfer_Status= 'NT') and ar.StudentStatus='AV'    and ar.Status= '1' and ar.Session_id = " + ddlsession.SelectedValue + " and ar.Class_id=" + ddlclass.SelectedValue + "   order by ac.Position, ar.Section, ar.rollnumber asc");


               
            }
        }
        protected void btn_find_by_admission_no_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_admission_no.Text == "")
                {
                    Alertme("Please enter admission no.", "warning");
                    txt_admission_no.Focus();
                }
                else
                {
                    find_by_adm_no();
                    ViewState["flag"] = "2";
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_by_adm_no()
        {
            bind_grd_view("select ener.*,ar.session,ar.admissionserialnumber,ar.class,Section,ar.rollnumber,ar.studentname,ar.Status,ar.fathername,ar.mobilenumber,ar.Session_id,ar.Class_id,ar.mothername,ar.dob,ar.dateofadmission,ar.gender,ar.blood_group,ar.careof,ar.email_id,ar.father_mob from admission_registor ar join Add_course_table ac on ac.course_id=ar.Class_id join  enach_registration  ener on ener.Session_id=ar.Session_id and ener.admissionserialnumber=ar.admissionserialnumber where(ar.Transfer_Status= 'New' or ar.Transfer_Status= 'NT') and ar.StudentStatus='AV'    and ar.Status= '1'  and  admissionserialnumber='" + txt_admission_no.Text + "'  order by ac.Position, ar.Section, ar.rollnumber asc");

           
        }
         protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Download"].ToString() == "1")
                {
                    string file_name = My.with_excel_name("inactive-student-report");
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=" + file_name + ".xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    using (StringWriter sw = new StringWriter())
                    {
                        HtmlTextWriter hw = new HtmlTextWriter(sw);
                        Panel1.RenderControl(hw);
                        string style = @"<style> TABLE { border: 1px solid black; } TD { border: 1px solid black; } </style> ";
                        Response.Write(style);
                        Response.Output.Write(sw.ToString());
                        Response.Flush();
                        Response.End();
                    }
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            catch
            {
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {

                    RepeaterItem item = e.Item;
                    Label lbl_Status = (Label)e.Item.FindControl("lbl_Status");
                    LinkButton lnkEdit = (LinkButton)e.Item.FindControl("lnkEdit");
                    if (lbl_Status.Text == "Pending")
                    {
                        lnkEdit.BackColor = System.Drawing.Color.Green;
                        lnkEdit.ForeColor = System.Drawing.Color.White;
                        lnkEdit.Text = "Approved";
                    }
                    else
                    {
                        lbl_Status.Text = "Approved";
                        lnkEdit.Text = "Pending";
                        lnkEdit.BackColor = System.Drawing.Color.Red;
                        lnkEdit.ForeColor = System.Drawing.Color.White;
                    }
                }
            }
            catch (Exception ex) { }
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            Label lbl_id = (Label)row.FindControl("lbl_id");
            Label lbl_istatus = (Label)row.FindControl("lbl_Status");
            if (ViewState["Is_add"].ToString() == "1")
            {
             

                if (lbl_istatus.Text == "Pending")
                {
                    mycode.executequery("update enach_registration set Status='Approved' where id=" + lbl_id.Text + "");
                    Alertme("Record has been updated successfully ", "success");
                }
                
                else
                {
                    mycode.executequery("update enach_registration set Status='Pending' where id=" + lbl_id.Text + "");
                    Alertme("Record has been updated successfully", "success");
                }

                bind_grd_view(ViewState["query"].ToString());
            }
            else if (ViewState["Is_Edit"].ToString() == "1")


            {

                if (lbl_istatus.Text == "Pending")
                {
                    mycode.executequery("update enach_registration set Status='Approved' where id=" + lbl_id.Text + "");
                    Alertme("Record has been updated successfully ", "success");
                }

                else
                {
                    mycode.executequery("update enach_registration set Status='Pending' where id=" + lbl_id.Text + "");
                    Alertme("Record has been updated successfully", "success");
                }

                bind_grd_view(ViewState["query"].ToString());


            }
            else
            {
                Alertme(My.get_restricted_message(), "warning");
            }
        }

        [WebMethod]
        public static List<string> GetRooPathAdmNo(string PathRooT)
        {
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(My.conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct admissionserialnumber from admission_registor where admissionserialnumber LIKE '%'+@SearchMobNo+'%'    ";
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
    }
}