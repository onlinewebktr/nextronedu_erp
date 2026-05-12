using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class Hostel_Damage_Item_View_Mapped_With_Student : System.Web.UI.Page
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
                        ViewState["session"] = My.get_session();
                        ViewState["session_id"] = My.get_session_id();
                        
                        string pagename_current = "Hostel_Master.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        mycode.bind_all_ddl_with_id_All_New(ddl_month, " select Month,Month_Id from dbo.[Month_Index] order by Position ");
                        ddl_month.SelectedValue = mycode.get_current_month_id();
                        Bind_add_data();

                        get_firmdaetail();


                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Hostel_Damage_master");
            }
        }
        private void get_firmdaetail()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count > 0)
            {
                imglogo.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_emaiid.Text = dt.Rows[0]["email"].ToString();
                lbl_website.Text = dt.Rows[0]["website"].ToString();
                lbl_contact_details.Text = dt.Rows[0]["contact_no"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();
            }
        }
        private void Bind_add_data()
        {
            string query = "";
            if (ddl_month.SelectedItem.Text=="ALL")
            {
                query = " Select hs.*,ar.studentname,ar.session,ar.rollnumber,ar.class, im.Item_name from Hostel_student_damage_Item_maping hs join  admission_registor ar on  ar.admissionserialnumber=hs.Admission_no and ar.Hostel_assignD_id=hs.Assign_id join  Hostel_Damage_Item_master  im on hs.Damage_goods_item_id=im.Item_id  where  ar.Session_id=" + My.get_session_id() + " ";

            }
            else
            {
                query = " Select hs.*,ar.studentname,ar.session,ar.rollnumber,ar.class, im.Item_name from Hostel_student_damage_Item_maping hs join  admission_registor ar on  ar.admissionserialnumber=hs.Admission_no and ar.Hostel_assignD_id=hs.Assign_id join  Hostel_Damage_Item_master  im on hs.Damage_goods_item_id=im.Item_id  where hs.For_motn_name='" + ddl_month.SelectedItem.Text + "' and ar.Session_id=" + My.get_session_id() + " ";
                
            }
           
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data list exist", "warning");
                rd_viewaddedfee.DataSource = null;
                rd_viewaddedfee.DataBind();
            }
            else
            {
                rd_viewaddedfee.DataSource = dt;
                rd_viewaddedfee.DataBind();
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
        protected void ddl_month_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_month.SelectedItem.Text == "Select")
            {
                Alertme("Please select month anme", "warning");
            }
            else
            {
                Bind_add_data();
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
                    Response.AddHeader("content-disposition", "attachment;filename=Export_Student_Damaged_Item.xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    using (StringWriter sw = new StringWriter())
                    {
                        HtmlTextWriter hw = new HtmlTextWriter(sw);
                        Panel2.RenderControl(hw);
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

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            Label lbl_Id = (Label)row.FindControl("lbl_Id");
           

            My.exeSql("delete from Hostel_student_damage_Item_maping where Id='" + lbl_Id.Text + "'");
            Alertme("Deleteion process han been sucessfully done", "success");
            Bind_add_data();


        }
    }
}