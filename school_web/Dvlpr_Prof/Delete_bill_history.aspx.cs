using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
using System.IO;

namespace school_web.Dvlpr_Prof
{
    public partial class Delete_bill_history : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Admindov"] == null)
                {
                    Session.Abandon();
                    Session.Clear();
                    Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
                    Response.Write("<script language=javascript>wnd.close();</script>");
                    Response.Write("<script language=javascript>window.open('../Default.aspx','_parent',replace=true);</script>");
                }
                else
                {
                    ViewState["Userid"] = Session["Admindov"].ToString();
                    ViewState["firm_id"] = My.get_firm_id();
                    ViewState["branchid"] = "1";

                    mycode.bind_all_ddl_with_id(ddl_session, "Select  Session,session_id from session_details order by Session asc");
                    ddl_session.SelectedValue = My.get_session_id();
                    txt_date.Text = mycode.sevendaysbackseven();
                    txt_enddate.Text = mycode.date();
                    find_data_bill_wise();
                }
            }
        }
        string scrpt;
        public void Alert(string Message)
        {
            lblmessage.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }

        protected void btn_find_admission_no_Click(object sender, EventArgs e)
        {
            if (txt_admission_no.Text == "")
            {

                lblmessage.Text = "Please enter admission";
                scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            }
            else if (ddl_session.SelectedItem.Text == "Select")
            {
                lblmessage.Text = "Please select session";
                scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);

            }
            else
            {
                pnl_payment_history.Visible = false;

                string query = "  select t1.*,format(t1.insert_time_date, 'dd/MM/yyyy') as insert_time_date1  from Student_Payment_History_Save_bakup t1 where  t1.Addmission_no='" + txt_admission_no.Text + "'   and t1.Session='" + ddl_session.SelectedItem.Text + "' ";
                DataTable dt = mycode.FillData(query);
                if (dt.Rows.Count == 0)
                {
                    // Alertme("There are no payment history found", "warning");
                    lbl_msg.Text = "There are no payment history found";
                    Alert("There are no payment history found");

                    grd_fee.DataSource = null;
                    grd_fee.DataBind();
                }
                else
                {
                    pnl_payment_history.Visible = true;
                    lbl_msg.Text = "";
                    grd_fee.DataSource = dt;
                    grd_fee.DataBind();
                }
            }
        }
        UsesCode code = new UsesCode();
        protected void btn_find_date_Click(object sender, EventArgs e)
        {
            find_data_bill_wise();


        }

        private void find_data_bill_wise()
        {
            if (txt_date.Text == "")
            {
                Alert("Please select start date");
            }
            else if (txt_enddate.Text == "")
            {
                Alert("Please select end date");
            }
            else
            {
                if (Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) <= Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)))
                {

                    string query = "  select t1.*,format(t1.insert_time_date, 'dd/MM/yyyy') as insert_time_date1  from Student_Payment_History_Save_bakup t1 where  format(t1.insert_time_date, 'yyyyMMdd')>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and format(t1.insert_time_date, 'yyyyMMdd')<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + " order by cast(format(t1.insert_time_date, 'yyyyMMdd') as int) ";


                    pnl_payment_history.Visible = false;


                    DataTable dt = mycode.FillData(query);
                    if (dt.Rows.Count == 0)
                    {

                        lbl_msg.Text = "There are no payment history found";
                        Alert("There are no payment history found");

                        grd_fee.DataSource = null;
                        grd_fee.DataBind();
                    }
                    else
                    {
                        pnl_payment_history.Visible = true;
                        lbl_msg.Text = "";
                        grd_fee.DataSource = dt;
                        grd_fee.DataBind();
                    }
                }
                else
                {
                    Alert("Please select date valid");
                }

            }
        }

        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {

                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=BillHistory.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                    grd_fee.RenderControl(hw);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
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