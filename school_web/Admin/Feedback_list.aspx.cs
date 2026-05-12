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
    public partial class Feedback_list : System.Web.UI.Page
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
                        ViewState["Applicant_Image"] = "";
                        ViewState["Userid"] = Session["Admin"].ToString();
                        string pagename_current = "Enquiry_List.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];

                        txt_startdate.Text = mycode.date();
                        txt_enddate.Text = mycode.date();
                        bind_grd_view();
                        Bind_data_firm_detials();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Enquiry_List");
            }

        }
        protected void btn_find_Click(object sender, EventArgs e)
        {
            bind_grd_view();
        }
        private void bind_grd_view()
        {
            var condition = "where 1=1 ";
            condition += $" and  format(date_time, 'yyyyMMdd')>=" + Convert.ToInt32(mycode.ConvertStringToiDate(txt_startdate.Text)) + " and  format(date_time, 'yyyyMMdd')<=" + Convert.ToInt32(mycode.ConvertStringToiDate(txt_enddate.Text)) + "  ";
            if (ddl_studentype.Text != "ALL")
            {
                condition += $" and  Student_type='{ddl_studentype.Text}' ";
            }

            DataTable dt = My.MydataTable($@"select  *,format(date_time, 'dd/MM/yyyy hh:mm tt') as date_time1,CASE WHEN  Isparents = 1 THEN 'Parent' WHEN Isparents = 0 THEN 'Student'  WHEN Isparents = '' THEN 'Student' END AS isprents_name from Feedback_ans_master  {condition} order by format(date_time, 'yyyyMMdd') asc");
            if (dt.Rows.Count == 0)
            {
                btn_excels.Visible = false;
                Alertme("Sorry, there are no records in the list.", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
                lbl_class22.Text = "";
            }
            else
            {

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

        private void Bind_data_firm_detials()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                imglogo.ImageUrl = dt.Rows[0]["logo"].ToString();
                //lbl_address.Text = dt.Rows[0]["address1"].ToString();
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

        #region excel
        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {

                string heading = "Export_Feedback_List" + mycode.date() + mycode.itime() + ".xls";

                if (ViewState["Is_Download"].ToString() == "1")
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=" + heading);
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    using (StringWriter sw = new StringWriter())
                    {
                        HtmlTextWriter hw = new HtmlTextWriter(sw);
                        Panel1.RenderControl(hw);
                        //string style = @"<style> TABLE { border: 1px solid black; } TD { border: 1px solid black; } </style> ";
                        //Response.Write(style);
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

        #endregion

        protected void lnk_Flow_up_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            Label lbl_Feedback_id = (Label)row.FindControl("lbl_Feedback_id");
            Bind_data(lbl_Feedback_id.Text);
        }

        private void Bind_data(string Feedback_id)
        {
            string query = "Select * from  Feedback_ans    where  Feedback_id='" + Feedback_id + "'";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                GrdView_Follow_Up.DataSource = null;
                GrdView_Follow_Up.DataBind();

            }
            else
            {
                GrdView_Follow_Up.DataSource = dt;
                GrdView_Follow_Up.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal2();", true);
            }
        }

       
    }
}