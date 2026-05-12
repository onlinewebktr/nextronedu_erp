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
    public partial class deleted_bill_history : System.Web.UI.Page
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
                        Session["SlipBkSn"] = "MN33";
                        txt_s_date.Text = mycode.date();
                        txt_e_date.Text = mycode.date();
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();
                        ViewState["sessionid"] = My.get_session_id();
                        mycode.bind_all_ddl_with_id_cap_All(ddl_session_serach, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                        ddl_session_serach.SelectedValue = My.get_session_id();


                        string pagename_current = Path.GetFileName(Request.Path);
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        if (ViewState["Is_Print"].ToString() == "1")
                        {
                            print1.Visible = true;
                        }
                        else
                        {
                            print1.Visible = false;
                        }

                        find_firm_details();

                        find_bill_by_date();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Admission_Fee_Collection");
            }
        }

        private void find_bill_by_date()
        {
            int fromidate = My.DateConvertToIdate(txt_s_date.Text);
            int Toidate = My.DateConvertToIdate(txt_e_date.Text);
            lbl_class22.Text = "Deleted bill list for session :" + ddl_session_serach.SelectedItem.Text + ", from date : " + txt_s_date.Text + " To date : " + txt_e_date.Text;
            string query = "select *,(select top 1 name from user_details where user_id=t1.Insert_time_user_id) as Cancelled_by,(select top 1 studentname from admission_registor where admissionserialnumber=t1.Addmission_no and Session=t1.Session) as Student_name,(select top 1 rollnumber from admission_registor where admissionserialnumber=t1.Addmission_no and Session=t1.Session) as Rollnumber,(select top 1 class from admission_registor where admissionserialnumber=t1.Addmission_no and Session=t1.Session) as className,(select top 1 Section from admission_registor where admissionserialnumber=t1.Addmission_no and Session=t1.Session) as Section from Student_Payment_History_Save_bakup t1 where Session='" + ddl_session_serach.SelectedItem.Text + "' and Format(convert(DateTime,insert_time_date,103), 'yyyyMMdd')>=" + fromidate + " and Format(convert(DateTime,insert_time_date,103), 'yyyyMMdd')<=" + Toidate + " order by id desc";
            if (ddl_session_serach.SelectedItem.Text == "ALL")
            {
                query = "select *,(select top 1 name from user_details where user_id=t1.Insert_time_user_id) as Cancelled_by,(select top 1 studentname from admission_registor where admissionserialnumber=t1.Addmission_no and Session=t1.Session) as Student_name,(select top 1 rollnumber from admission_registor where admissionserialnumber=t1.Addmission_no and Session=t1.Session) as Rollnumber,(select top 1 class from admission_registor where admissionserialnumber=t1.Addmission_no and Session=t1.Session) as className,(select top 1 Section from admission_registor where admissionserialnumber=t1.Addmission_no and Session=t1.Session) as Section from Student_Payment_History_Save_bakup t1 where Format(convert(DateTime,insert_time_date,103), 'yyyyMMdd')>=" + fromidate + " and Format(convert(DateTime,insert_time_date,103), 'yyyyMMdd')<=" + Toidate + " order by id desc";
            }
            Bind_data(query);
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

                if (dt.Rows[0]["Monthly_bill_type"].ToString() == "")
                {
                    ViewState["BillType"] = "1";
                }
                else
                {
                    ViewState["BillType"] = dt.Rows[0]["Monthly_bill_type"].ToString();
                }
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


        private void Bind_data(string query)
        {
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no students list exists", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
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
                    Response.AddHeader("content-disposition", "attachment;filename=deleted_receipt_list" + mycode.date() + "_" + mycode.time() + ".xls");
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

        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_session_serach.SelectedItem.Text == "")
                {
                    ddl_session_serach.Focus();
                    Alertme("Please select session.", "warning");
                }
                else
                {
                    find_bill_by_date();
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}