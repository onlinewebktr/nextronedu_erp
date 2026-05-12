using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using school_web.AppCode;
namespace school_web.LMS_VC_Admin
{
    public partial class School_Calender_List : System.Web.UI.Page
    {
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
            string query = " select  * from School_Holiday_Calendar where   Date like'%" + date + "%' order by Idate asc ";
            a1.HRef = "print/Print_school_calendar.aspx?date=" + date;
            DataTable dt = imp.FillTable(query);
            try
            {
                if (dt.Rows.Count > 0)
                {
                    a1.Visible = true;
                    RPDetails.DataSource = dt;
                    RPDetails.DataBind();

                }
                else
                {
                    a1.Visible = false;
                    RPDetails.DataSource = null;
                    RPDetails.DataBind();
                }

            }
            catch (Exception ex)
            {
            }
        }

        protected void lnk_edit_Click(object sender, EventArgs e)
        {

            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                Response.Redirect("Add_School_Calender.aspx?id=" + lbl_Id.Text, false);
              
            }
            catch { }
        }


        UsesCode imp = new UsesCode();
        string scrpt;
        protected void lnk_Delete_Click(object sender, EventArgs e)
        {


            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                LinkButton lnkDel = (LinkButton)row.FindControl("lnkDel");
                Label Id = (Label)row.FindControl("lbl_Id");
                HdID.Value = Id.Text;
                imp.executequery("delete from School_Holiday_Calendar where Id=" + HdID.Value + "");
                lblmessage.Text = "Deletion process has been completed";
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