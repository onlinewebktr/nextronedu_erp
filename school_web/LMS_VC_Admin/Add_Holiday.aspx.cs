using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
namespace school_web.LMS_VC_Admin
{
    public partial class Add_Holiday : System.Web.UI.Page
    {
        UsesCode imp = new UsesCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Session["Admin"] != null)
                    {
                        DateTime dtm = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                        string date = dtm.ToString("dd/MM/yyyy");
                        string day = date.Substring(0, 2);
                        string month = date.Substring(3, 2);
                        string year = date.Substring(6, 4);
                        ddl_year.Text = year;

                        ddl_month.Text = dtm.ToString("MMM");
                        BindGrid();
                    }
                    else
                    {
                        Session.Abandon();
                        Session.Clear();
                        Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
                        Response.Write("<script language=javascript>wnd.close();</script>");
                        Response.Write("<script language=javascript>window.open('../Default.aspx','_parent',replace=true);</script>");
                    }
                }
            }
            catch
            {
            }
        }

        private void BindGrid()
        {
            string date = imp.getmontnumber(ddl_month.Text) + "/" + ddl_year.Text;
            string query = " select  * from HolidayList_Details where   Date like'%" + date + "%' ";
             
            DataTable dt = imp.FillTable(query);
            try
            {
                if (dt.Rows.Count > 0)
                {
                    RPDetails.DataSource = dt;
                    RPDetails.DataBind();

                }
                else
                {
                    RPDetails.DataSource = null;
                    RPDetails.DataBind();
                }

            }
            catch (Exception ex)
            {
            }
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (btn_Submit.Text == "Add")
                {
                    insertAll();

                }
                else
                {
                    updateAll();
                }

            }
            catch (Exception ex)
            {
                UsesCode.submitexception(ex.ToString());
            }
        }

        private void updateAll()
        {
            string query = " select  * from HolidayList_Details where Date='" + txt_date.Text + "' and Id!=" + HdID.Value + " ";
            DataTable dt = imp.FillTable(query);
            if (dt.Rows.Count == 0)
            {
                SqlCommand cmd;
                query = "Update HolidayList_Details set Date=@Date,Idate=@Idate,Remarks=@Remarks,Year=@Year,Month=@Month,Holiday_Date=@Holiday_Date where Id = @Id";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Date", txt_date.Text);
                cmd.Parameters.AddWithValue("@Idate", imp.ConvertStringToiDate(txt_date.Text));
                cmd.Parameters.AddWithValue("@Remarks", txt_description.Text);
                cmd.Parameters.AddWithValue("@Year", imp.ConvertStringToyear(txt_date.Text));
                cmd.Parameters.AddWithValue("@Month", imp.ConvertStringTomonth(txt_date.Text));
                cmd.Parameters.AddWithValue("@Holiday_Date", imp.ConvertStringToday(txt_date.Text));
                cmd.Parameters.AddWithValue("@Id", HdID.Value);
                if (InsertUpdate.InsertUpdateData(cmd))
                {
                    lblmessage.Text = "Holiday has been added";
                    scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
                    txt_description.Text = "";
                    BindGrid();
                }

            }
            else
            {
                lblmessage.Text = "This Holiday already exist";
                scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            }
        }
        string scrpt;
        private void insertAll()
        {
            string date = imp.getmontnumber(ddl_month.Text) + "/" + ddl_year.Text;
            string query = " select  * from HolidayList_Details where   Date like'%" + date + "%' ";
            DataTable dt = imp.FillTable(query);
            if (dt.Rows.Count == 0)
            {
                SqlCommand cmd;
                query = "INSERT INTO HolidayList_Details (Date,Idate,Remarks,Year,Month,Holiday_Date) values (@Date,@Idate,@Remarks,@Year,@Month,@Holiday_Date)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Date", txt_date.Text);
                cmd.Parameters.AddWithValue("@Idate", imp.ConvertStringToiDate(txt_date.Text));
                cmd.Parameters.AddWithValue("@Remarks", txt_description.Text);
                cmd.Parameters.AddWithValue("@Year", imp.ConvertStringToyear(txt_date.Text));
                cmd.Parameters.AddWithValue("@Month", imp.ConvertStringTomonth(txt_date.Text));
                cmd.Parameters.AddWithValue("@Holiday_Date", imp.ConvertStringToday(txt_date.Text));
                if (InsertUpdate.InsertUpdateData(cmd))
                {
                    lblmessage.Text = "Holiday has been added";
                    scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
                    txt_description.Text = "";
                    BindGrid();
                }
            }
            else
            {
                lblmessage.Text = "This Holiday already exist";
                scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);

            }

        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            btn_Submit.Text = "Add";
            txt_description.Text = "";
            btn_cancel.Visible = false;
        }
        protected void lnk_edit_Click(object sender, EventArgs e)
        {

            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                Label lbl_Description = (Label)row.FindControl("lbl_Remarks");
                Label lbl_Date = (Label)row.FindControl("lbl_Date");

                HdID.Value = lbl_Id.Text;
                txt_description.Text = lbl_Description.Text;
                txt_date.Text = lbl_Date.Text;
                btn_Submit.Text = "Update";
                btn_cancel.Visible = true;
            }
            catch { }
        }



        protected void lnk_Delete_Click(object sender, EventArgs e)
        {


            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                LinkButton lnkDel = (LinkButton)row.FindControl("lnkDel");
                Label Id = (Label)row.FindControl("lbl_Id");
                HdID.Value = Id.Text;
                imp.executequery("delete from HolidayList_Details where Id=" + HdID.Value + "");
                lblmessage.Text = "Holiday has been deleted successfully.";
                scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
                BindGrid();
            }
            catch { }
        }

        protected void Btn_Find_Click(object sender, EventArgs e)
        {
            BindGrid();
        }


    }
}