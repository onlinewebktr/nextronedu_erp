using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
using System.Web.UI.HtmlControls;

namespace school_web.Student_Profile.webview
{
    public partial class Notice_Board : System.Web.UI.Page
    {
        UsesCode mycode = new UsesCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["sesssionid"] = My.get_session_id();
                if (Request.QueryString["regid"] != null)
                {
                    ViewState["regid"] = Request.QueryString["regid"].ToString();
                    try
                    {
                        txt_date.Text = mycode.date();
                        txt_enddate.Text = mycode.date();
                        Session["regid"] = ViewState["regid"].ToString();
                        bind_student_class_and_section(); 
                        //Bind_data(); 
                        BindRepeater("select top 10 *,format(Date_Main, 'dd-MMM-yyyy') as Date1,format(Date_Main, 'dd') as day,format(Date_Main, 'MMM') as month,format(Date_Main, 'yyyy') as year  from Notice_Board_Details where (Class in ('ALL','" + ViewState["classid"].ToString() + "')  and Section in ('ALL','" + ViewState["section"].ToString() + "') and Session_Id='" + ViewState["sesssionid"] + "' or Admission_no='" + ViewState["regid"].ToString() + "') and   Session_Id='" + ViewState["sesssionid"] + "'  order by  Posted_Idate Desc");
                    }
                    catch(Exception ex)
                    {

                    }
                } 
            }
        }


        protected void btn_submit_Click(object sender, EventArgs e)
        {
            Bind_data();
        }

        private void Bind_data()
        {
            if (Convert.ToInt32(mycode.ConvertStringToiDate(txt_date.Text)) <= Convert.ToInt32(mycode.ConvertStringToiDate(txt_enddate.Text)))
            {
                BindRepeater("select  *,format(Date_Main, 'dd-MMM-yyyy') as Date1,format(Date_Main, 'dd') as day,format(Date_Main, 'MMM') as month,format(Date_Main, 'yyyy') as year  from Notice_Board_Details where (Class in ('ALL','" + ViewState["classid"].ToString() + "')  and Section in ('ALL','" + ViewState["section"].ToString() + "') and Session_Id='" + ViewState["sesssionid"] + "' or Admission_no='" + ViewState["regid"].ToString() + "') and   Session_Id='" + ViewState["sesssionid"] + "' and Posted_Idate>=" + Convert.ToInt32(mycode.ConvertStringToiDate(txt_date.Text)) + " and Posted_Idate<=" + Convert.ToInt32(mycode.ConvertStringToiDate(txt_enddate.Text)) + " order by  Posted_Idate Desc");
            }
            else
            {
                Alert("Please select valid date");
            }

           // BindRepeater("select *,format(Date_Main, 'dd-MMM-yyyy') as Date1,format(Date_Main, 'dd') as day,format(Date_Main, 'MMM') as month,format(Date_Main, 'yyyy') as year  from Notice_Board_Details where (Class in ('ALL','" + ViewState["classid"].ToString() + "')  and Section in ('ALL','" + ViewState["section"].ToString() + "') and Session_Id='" + ViewState["sesssionid"] + "' or Admission_no='" + ViewState["regid"].ToString() + "') and   Session_Id='" + ViewState["sesssionid"] + "'  order by  Posted_Idate Desc");

        }

        private void BindRepeater(string query)
        {
            DataTable dt = mycode.FillTable(query);
            if (dt.Rows.Count != 0)
            {
                RPDetails.DataSource = dt;
                RPDetails.DataBind();
            }
            else
            {
                Alert("Currently,there is not update at notice board. Please keep visiting for update");
                RPDetails.DataSource = null;
                RPDetails.DataBind();
            }


        }

        private void Alert(string msg)
        {
            lbl_msg.Text = msg;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);

        }

        private void bind_student_class_and_section()
        {
            DataTable dt = mycode.FillTable("Select Class_id,Section from admission_registor where admissionserialnumber='" + ViewState["regid"].ToString() + "' and   Transfer_Status in ('New','NT') and  StudentStatus='AV' and Session_id=" + ViewState["sesssionid"].ToString() + " ");
            if (dt.Rows.Count == 0)
            {

                ViewState["classid"] = "0";
                ViewState["section"] = "0";
            }
            else
            {
                ViewState["classid"] = dt.Rows[0]["Class_id"].ToString();
                ViewState["section"] = dt.Rows[0]["Section"].ToString();
            }
        }
        protected void RPDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                string notice = ((Label)e.Item.FindControl("lbl_notice1")).Text;
                string trimmed50Char = notice.Substring(0, 50);
                trimmed50Char += "-";

                ((Label)e.Item.FindControl("lbl_notice2")).Text = trimmed50Char;
            }
            catch
            {
                string notice = ((Label)e.Item.FindControl("lbl_notice1")).Text;
                ((Label)e.Item.FindControl("lbl_notice2")).Text = notice;
            }





            if (((Label)e.Item.FindControl("lbl_Attachments")).Text == "")
            {
                ((Panel)e.Item.FindControl("Panel1")).Visible = false;
            }
            else
            {
                ((Panel)e.Item.FindControl("Panel1")).Visible = true;
            }

             
            if (((Label)e.Item.FindControl("lbl_link")).Text == "")
            {
                ((Panel)e.Item.FindControl("pnl_link")).Visible = false;
            }
            else
            {
                ((Panel)e.Item.FindControl("pnl_link")).Visible = true;
            }
        }
    }
}