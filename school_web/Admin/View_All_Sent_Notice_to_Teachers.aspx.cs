using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.IO;
using System.Data;
using System.Web.UI.HtmlControls;

namespace school_web.Admin
{
    public partial class View_All_Sent_Notice_to_Teachers : System.Web.UI.Page
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
                        if (Session["SmsG"] != null)
                        {
                            Alertme(Session["SmsG"].ToString(), "success");
                            Session["SmsG"] = null;
                        }
                        find_firm_details();

                        ViewState["Userid"] = Session["Admin"].ToString();
                        string pagename_current = "Notice.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];

                        ViewState["session_id"] = My.get_session_id();
                        mycode.bind_all_ddl_with_id_cap_All(ddl_teacher, "select name as Name,user_id as  UserID from user_details where (User_Type='Teacher' or User_Type='Principal')  order by Name  asc");
                        txt_date.Text = mycode.sevendaysbackseven();
                        txt_enddate.Text = mycode.date();
                        if (Session["queryNotice_Teacher"] == null)
                        {
                            find_data();
                        }
                        else
                        {
                            BindRepeater(Session["queryNotice_Teacher"].ToString());
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "View_All_Sent_Notice_to_Teachers");
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


        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            if (ViewState["Is_Edit"].ToString() == "1")
            {
                LinkButton lnk = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lnk.Parent.Parent;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                Response.Redirect("Send_Notice_To_Teacher.aspx?id=" + lbl_Id.Text, false);
            }
            else
            {
                Alertme(My.get_restricted_message(), "warning");
            }

        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    GridViewRow row = (GridViewRow)lnk.Parent.Parent;
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");
                    mycode.executequery("delete from Notice_Board_Details_Teacher where Id=" + lbl_Id.Text + "");
                    BindRepeater(ViewState["query"].ToString());
                    Alertme("Deletion process has been successfully", "warning");
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

        private void BindRepeater(string query)
        {
            lbl_class22.Text = "Teacher " + ddl_teacher.Text + " Start date : " + txt_date.Text + " End date : " + txt_enddate.Text;
            ViewState["query"] = query;
            Session["queryNotice_Teacher"] = query;
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {

                btn_excels.Visible = false;
                print1.Visible = false;
                Alertme("Sorry there are no data list exist", "warning");
                GrdView.DataSource = null;
                GrdView.DataBind();

            }
            else
            {
                print1.Visible = true;
                btn_excels.Visible = true;
                GrdView.DataSource = dt;
                GrdView.DataBind();
                if (ViewState["Is_Print"].ToString() == "1")
                {
                    print1.Visible = true;
                }
                else
                {
                    print1.Visible = true;
                }

            }
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            find_data();
        }
        UsesCode code = new UsesCode();
        private void find_data()
        {
            try
            {
                if (txt_date.Text == "")
                {
                    Alertme("Please select start date", "warning");
                }
                else if (txt_enddate.Text == "")
                {
                    Alertme("Please select end date", "warning");
                }
                else
                {
                    if (ddl_teacher.SelectedItem.Text == "ALL")
                    {

                        if (Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) <= Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)))
                        {

                            BindRepeater("select  * from Notice_Board_Details_Teacher where  Posted_Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and Posted_Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + "  order by  Posted_Idate Desc");

                        }
                        else
                        {
                            Alertme("Please select date valid", "warning");
                        }
                    }
                    else
                    {
                        BindRepeater("select  * from Notice_Board_Details_Teacher where  Posted_Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and Posted_Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + " and Teacher_Id='" + ddl_teacher.SelectedValue + "' order by  Posted_Idate Desc");
                    }
                }
            }
            catch
            {

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
                        GrdView.RenderControl(hw);
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

        protected void GrdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_Attachments = (Label)e.Row.FindControl("lbl_Attachments");
                Label lbl_Teacher_Id = (Label)e.Row.FindControl("lbl_Teacher_Id");
                Label lbl_teacheridname = (Label)e.Row.FindControl("lbl_teacheridname");
                HtmlAnchor a1 = e.Row.FindControl("a1") as HtmlAnchor;
                if (lbl_Attachments.Text == "")
                {
                    a1.Visible = false;
                }
                else
                {
                    a1.Visible = true;
                }


                if (lbl_Teacher_Id.Text == "ALL")
                {
                    lbl_teacheridname.Text = "ALL";
                }
                else
                {
                    lbl_teacheridname.Text = code.get_teachername(lbl_Teacher_Id.Text);
                }




                Label lbl_links = (Label)e.Row.FindControl("lbl_links");
                HtmlAnchor a2 = e.Row.FindControl("a2") as HtmlAnchor;
                if (lbl_links.Text == "")
                {
                    a2.Visible = false;
                }
                else
                {
                    a2.Visible = true;
                }
            }
        }
    }
}