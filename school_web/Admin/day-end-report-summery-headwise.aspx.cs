using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class day_end_report_summery_headwise : System.Web.UI.Page
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
                        mycode.bind_all_ddl_with_id_cap_All(ddl_session, "select Session,session_id from session_details order by Session asc");
                        Bind_course_fee_details();
                        txt_from_date.Text = mycode.date();
                        txt_to_date.Text = mycode.date();
                        find_firm_details(); get_class_id();
                        string pagename_current = "fee-report.aspx";
                        ViewState["Userid"] = Session["Admin"].ToString();
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        if (ViewState["Is_Print"].ToString() == "1")
                        {
                            print1.Visible = true;
                        }
                        else
                        {
                            print1.Visible = false;
                        }
                        if (ViewState["Is_Download"].ToString() == "1")
                        {
                            excel.Visible = true;
                        }
                        else
                        {
                            excel.Visible = false;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_group_master");
            }
        }

        private void Bind_course_fee_details()
        {
            DataTable dt = mycode.FillData("Select Course_Name,course_id from Add_course_table order by Position");
            if (dt.Rows.Count == 0)
            {
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
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
        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                get_class_id();
            }
            catch (Exception ex)
            {
            }
        }

        private void get_class_id()
        {
            string class_id = ""; string isClassChecked = "0"; string class_name = ""; string clsnamm = "";
            int feedtCount = rd_view.Items.Count;
            for (int i = 0; i < feedtCount; i++)
            {
                CheckBox chkContainer = (CheckBox)rd_view.Items[i].FindControl("chkContainer");
                Label lbl_class_id = (Label)rd_view.Items[i].FindControl("lbl_class_id");
                Label lbl_course_name = (Label)rd_view.Items[i].FindControl("lbl_course_name");
                if (chkContainer.Checked == true)
                {
                    isClassChecked = "1";
                    class_id = class_id + lbl_class_id.Text + ",";
                    class_name = class_name + "'" + lbl_course_name.Text + "'" + ",";
                    clsnamm = clsnamm + lbl_course_name.Text + ",";
                }
            }
            if (isClassChecked == "1")
            {
                hd_class_id.Value = class_id.Remove(class_id.Length - 1);
                hd_class_name.Value = class_name.Remove(class_name.Length - 1);
            }
            else
            {
                hd_class_name.Value = "0";
                hd_class_id.Value = "0";
                Alertme("Please choose class.", "warning");
            }


            lbl_class22.Text = "Date : " + txt_from_date.Text + " To " + txt_to_date.Text + ", Session : " + ddl_session.SelectedItem.Text + ", Class : " + clsnamm;
        }
    }
}