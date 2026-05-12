using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class print_income_certificate : System.Web.UI.Page
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
                            }
                        }
                        catch (Exception ex) { }

                        ViewState["Userid"] = Session["Admin"].ToString();
                        DataTable dtF = My.dataTable("select firm_name,firm_id from Firm_Details");
                        ViewState["firm_id"] = dtF.Rows[0]["firm_id"].ToString();

                        string pagename_current = "certificate-master.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);

                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];

                        mycode.bind_all_ddl_with_id(ddlsession, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int)");
                        mycode.bind_all_ddl_with_id_cap_All(ddlclass, "Select Course_Name,course_id from Add_course_table order by Position");
                        ddlsession.SelectedValue = My.get_session_id();
                        ddlclass.SelectedValue = My.get_top_one_class();

                        bind_created_certificate();
                        find_firm_details();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Character_certificate");
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
                lbl_emaiid.Text = dt.Rows[0]["email"].ToString();
                lbl_website.Text = dt.Rows[0]["website"].ToString();
                lbl_contact_details.Text = dt.Rows[0]["contact_no"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();
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

        private void bind_created_certificate()
        {
            string query = "";
            if (ddlclass.SelectedItem.Text == "ALL")
            {
                query = "select *,(select top 1 Certificate_no from Certificate_Master where Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id and Admission_no=admission_registor.admissionserialnumber and Certificate_type='Income') as Certificate_no from admission_registor where admissionserialnumber IN  (SELECT Admission_no FROM Certificate_Master WHERE Admission_no=admission_registor.admissionserialnumber and Class_id=admission_registor.Class_id and Session_id=admission_registor.Session_id and Certificate_type='Income') and Session_id=" + ddlsession.SelectedValue + "  order by rollnumber asc";
            }
            else

            {
                query = "select *,(select top 1 Certificate_no from Certificate_Master where Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id and Admission_no=admission_registor.admissionserialnumber and Certificate_type='Income') as Certificate_no from admission_registor where admissionserialnumber IN  (SELECT Admission_no FROM Certificate_Master WHERE Admission_no=admission_registor.admissionserialnumber and Class_id=admission_registor.Class_id and Session_id=admission_registor.Session_id  and Certificate_type='Income') and Session_id=" + ddlsession.SelectedValue + " and Class_id='" + ddlclass.SelectedValue + "' order by rollnumber asc";
            }

            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count == 0)
            {
                print1.Visible = false;
                lbl_class22.Text = "";
                btn_excels.Visible = false;
                rd_view.DataSource = null;
                rd_view.DataBind();
                Alertme("Data Not Found...", "warning");
            }
            else
            {
                lbl_class22.Text = " Session :" + ddlsession.SelectedItem.Text + " Class :" + ddlclass.SelectedItem.Text;
                btn_excels.Visible = true;
                rd_view.DataSource = dt;
                rd_view.DataBind();

                if (ViewState["Is_Print"].ToString() == "1")
                {
                    print1.Visible = true;
                }
                else
                {
                    print1.Visible = false;
                }
            }
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            if (ddlsession.SelectedItem.Text == "")
            {
                Alertme("Data Not Found...", "warning");
            }
            else
            {
                bind_created_certificate();

            }
        }
        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Download"].ToString() == "1")
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=Export.xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    using (StringWriter sw = new StringWriter())
                    {
                        HtmlTextWriter hw = new HtmlTextWriter(sw);
                        Panel1.RenderControl(hw);
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

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            if (ViewState["Is_delete"].ToString() == "1")
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_adm_no = (Label)row.FindControl("lbl_adm_no");
                Label lbl_session_id = (Label)row.FindControl("lbl_session_id");
                Label lbl_class_id = (Label)row.FindControl("lbl_class_id");
                Label lbl_certificate_no = (Label)row.FindControl("lbl_certificate_no");
                mycode.executequery("delete from Certificate_Master where Session_id='" + lbl_session_id.Text + "' and Class_id='" + lbl_class_id.Text + "' and Admission_no='" + lbl_adm_no.Text + "' and Certificate_type='Income'");
                string desc = "Income certificate has been delete for certificate no. " + lbl_certificate_no.Text + ".";
                log_hostory.delete_log(lbl_session_id.Text, lbl_class_id.Text, lbl_adm_no.Text, "Income Certificate", desc, "print-income-certificate.aspx", ViewState["Userid"].ToString());
                Alertme("Record has been deleted successfully.", "success");
                bind_created_certificate();
            }
            else
            {
                Alertme("SORRY! You have not permission for this work.", "warning");
            }
        }

        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    //Reference the Repeater Item.
                    RepeaterItem item = e.Item;
                    Label lbl_type_one = (Label)e.Item.FindControl("lbl_type_one");
                    Label lbl_type_two = (Label)e.Item.FindControl("lbl_type_two");
                    Label lbl_type_three = (Label)e.Item.FindControl("lbl_type_three");
                    lbl_type_two.Visible = false;
                    lbl_type_one.Visible = true;
                    lbl_type_three.Visible = false;
                    if (ViewState["firm_id"].ToString() == "GPSKTR")
                    { 
                        lbl_type_three.Visible = true;
                        lbl_type_two.Visible = false;
                        lbl_type_one.Visible = false;
                    } 
                }
            }
            catch (Exception ex) { }
        }
    }
}