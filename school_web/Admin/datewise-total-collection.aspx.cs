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
    public partial class datewise_total_collection : System.Web.UI.Page
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
                        ViewState["firm_id"] = Session["firm"].ToString();

                        mycode.bind_all_ddl_with_id_cap_All(ddlsession, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                        ddlsession.SelectedValue = My.get_session_id();



                        txt_from_date.Text = mycode.date();
                        txt_to_date.Text = mycode.date();



                    }
                }
            }
            catch (Exception ex)
            {
                //  My.saveExceptionDetails(ex, "StudentList");
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
                if (txt_from_date.Text == "")
                {
                    Alertme("Please choose from date.", "warning");
                    txt_from_date.Focus();
                }
                else if (txt_to_date.Text == "")
                {
                    Alertme("Please choose to date.", "warning");
                    txt_from_date.Focus();
                }
                else
                {
                    find_collection();
                }
            }
            catch (Exception ex)
            {
            }
        }


        private void find_collection()
        {
            string qry = "";
            if (ddlsession.SelectedItem.Text == "ALL")
            {
                lbl_class22.Text = "Total Collection List From " + txt_from_date.Text + " To " + txt_to_date.Text;
                qry = "select Date,isnull(sum(convert(float, Amount)),0) as Total_collection from Student_Payment_History where Idate>='" + My.DateConvertToIdate(txt_from_date.Text) + "' and Idate<='" + My.DateConvertToIdate(txt_to_date.Text) + "' group by Date";
            }
            else
            {
                lbl_class22.Text = "Total Collection List From " + txt_from_date.Text + " To " + txt_to_date.Text;
                qry = "select Date,isnull(sum(convert(float, Amount)),0) as Total_collection from Student_Payment_History where Session='" + ddlsession.SelectedItem.Text + "' and  Idate>='" + My.DateConvertToIdate(txt_from_date.Text) + "' and Idate<='" + My.DateConvertToIdate(txt_to_date.Text) + "' group by Date";
            }

            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {
                lbl_ttl_dues.Text = "0.00";
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                String Total_mrp = Convert.ToDouble(dt.Compute("SUM(Total_collection)", string.Empty)).ToString();
                lbl_ttl_dues.Text = My.toDouble(Total_mrp).ToString("0.00");
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
                    Response.AddHeader("content-disposition", "attachment;filename=Collection_list" + mycode.date() + "_" + mycode.time() + ".xls");
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
    }
}