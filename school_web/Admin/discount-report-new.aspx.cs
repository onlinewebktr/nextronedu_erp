using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class discount_report_new : System.Web.UI.Page
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
                        txt_from_date.Text = mycode.date();
                        txt_to_date.Text = mycode.date();
                        hd_find_status.Value = "0";
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();
                        bind_class();
                        find_firm_details();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "StudentList");
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


        private void bind_class()
        {
            using (SqlConnection conn = new SqlConnection(My.conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Select Course_Name,course_id from Add_course_table order by Position", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                ddl_classs.DataTextField = "Course_Name";
                ddl_classs.DataValueField = "course_id";
                ddl_classs.DataSource = reader;
                ddl_classs.DataBind();
            }



            foreach (ListItem item in ddl_classs.Items)
            {
                item.Selected = true;
            }
        }

        private void find_firm_details()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details");
            if (dt.Rows.Count > 0)
            {
                imglogo.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();
            }
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                //For Class
                bool isClassSelectd = false; string selectClassid = "";
                foreach (ListItem item in ddl_classs.Items)
                {
                    if (item.Selected)
                    {
                        selectClassid = selectClassid + item.Value + ",";
                        isClassSelectd = true;
                    }
                }
                if (isClassSelectd == false)
                {
                    ddl_classs.Focus();
                    Alertme("Please select class.", "warning");
                    return;
                }
                if (isClassSelectd == true)
                {
                    selectClassid = selectClassid.Remove(selectClassid.Length - 1);
                }
                if (txt_from_date.Text == "")
                {
                    txt_from_date.Focus();
                    Alertme("Please choose from date.", "warning");
                    return;
                }

                if (txt_to_date.Text == "")
                {
                    txt_to_date.Focus();
                    Alertme("Please choose to date.", "warning");
                    return;
                } 

                lbl_month.Text = txt_from_date.Text + " to " + txt_to_date.Text;
                if (txt_from_date.Text == txt_to_date.Text)
                {
                    DateTime datatimes = DateTime.ParseExact(txt_from_date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    lbl_month.Text = txt_from_date.Text + " - " + datatimes.ToString("dddd");
                } 
                hd_find_status.Value = "1";
                hd_class_id.Value = selectClassid;
                hd_mode_type.Value = "1";
                hd_from_idate.Value = My.DateConvertToIdate(txt_from_date.Text).ToString();
                hd_to_idate.Value = My.DateConvertToIdate(txt_to_date.Text).ToString(); 
                DataTable dt = My.dataTable("select t2.session,t2.studentname,t2.class,t2.Section,t2.rollnumber,t2.fathername,t.*,(convert(float, ttl_bill)-convert(float, PrevPaid)) as bill_amount,((convert(float, ttl_bill))-(convert(float, PrevPaid)+convert(float, ttl_disc))) as Net_patble from (select Session_id,Class_id,Admission_no,Bill_no,Created_date,Created_idate,Created_by,sum(convert(float, Amount)) as ttl_bill,sum(convert(float, Discount_amt)) as ttl_disc,(select top 1 previously_paid from Monthly_Fee_Collection_Slip where slipno=Discount_master_report.Bill_no and Date=Discount_master_report.Created_date) as PrevPaid from Discount_master_report where Class_id in (" + selectClassid + ") and  Created_idate>='" + hd_from_idate.Value + "' and Created_idate<='" + hd_to_idate.Value + "' group by Session_id,Class_id,Admission_no,Bill_no,Created_date,Created_idate,Created_by) t join admission_registor t2 on t.Session_id=t2.Session_id and t.Admission_no=t2.admissionserialnumber where ttl_disc>0 order by Bill_no asc");
                if (dt.Rows.Count > 0)
                {
                    rd_view.DataSource = dt;
                    rd_view.DataBind();
                }
                else
                {
                    rd_view.DataSource = null;
                    rd_view.DataBind();
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnk_view_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_session_id = (Label)row.FindControl("lbl_session_id");
                Label lbl_class_id = (Label)row.FindControl("lbl_class_id");
                Label lbl_Admission_no = (Label)row.FindControl("lbl_Admission_no");
                Label lbl_bill_no = (Label)row.FindControl("lbl_bill_no");
                DataTable dt = My.dataTable("select t.Content,(convert(float, Amount)-convert(float, PrevPaid)) as bill,Discount_amt,(convert(float, Amount)-(convert(float, PrevPaid)+convert(float, Discount_amt))) as Payble from (select *,(select top 1 previously_paid from Monthly_Fee_Collection_Slip where slipno=Discount_master_report.Bill_no and Date=Discount_master_report.Created_date and content_id=Discount_master_report.Content_id) as PrevPaid from Discount_master_report where Bill_no='" + lbl_bill_no.Text + "' and Session_id='" + lbl_session_id.Text + "' and Admission_no='" + lbl_Admission_no.Text + "') t");
                if (dt.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDT();", true);
                    Repeater1.DataSource = dt;
                    Repeater1.DataBind();
                }
            }
            catch
            {
            }
        }

        double amtss = 0; double discAmts = 0; double payble = 0;
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbl_bill_amts = (Label)e.Item.FindControl("lbl_bill_amts");
                Label lbl_disc_amt = (Label)e.Item.FindControl("lbl_disc_amt");
                Label lbl_paybles = (Label)e.Item.FindControl("lbl_paybles");


                amtss = amtss + My.toDouble(lbl_bill_amts.Text);
                discAmts = discAmts + My.toDouble(lbl_disc_amt.Text);
                payble = payble + My.toDouble(lbl_paybles.Text);
            }

            lbl_ttlatmt.Text = amtss.ToString();
            lbl_ttldiscss.Text = discAmts.ToString();
            lbl_ttpaybless.Text = payble.ToString();
        }

        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=discount-report.xls");
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
            catch
            {
            }
        }
    }
}