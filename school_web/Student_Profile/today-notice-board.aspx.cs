using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile
{
    public partial class today_notice_board : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["User"] != null)
                {
                    ViewState["sesssionid"] = My.get_session_id();
                    ViewState["regid"] = Session["User"].ToString();
                    bind_student_class_and_section();
                    txt_from_date.Text = mycode.date();
                    txt_to_date.Text= mycode.date();
                    Bind_data();
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
        protected void btn_find_Click(object sender, EventArgs e)
        {
            Bind_data();
        }
        private void Bind_data()
        {

            if (Convert.ToInt32(mycode.ConvertStringToiDate(txt_from_date.Text)) <= Convert.ToInt32(mycode.ConvertStringToiDate(txt_to_date.Text)))
            {
                BindRepeater("select  *,format(Date_Main, 'dd-MMM-yyyy') as Date1,format(Date_Main, 'dd') as day,format(Date_Main, 'MMM') as month,format(Date_Main, 'yyyy') as year  from Notice_Board_Details where (Class in ('ALL','" + ViewState["classid"].ToString() + "')  and Section in ('ALL','" + ViewState["section"].ToString() + "') and Session_Id='" + ViewState["sesssionid"] + "' or Admission_no='" + ViewState["regid"].ToString() + "') and   Session_Id='" + ViewState["sesssionid"] + "' and Posted_Idate>=" + Convert.ToInt32(mycode.ConvertStringToiDate(txt_from_date.Text)) + " and Posted_Idate<=" + Convert.ToInt32(mycode.ConvertStringToiDate(txt_to_date.Text)) + " order by  Posted_Idate Desc");
            }
            else
            {
                Alertme("Please select valid date.", "warning");

            }




        }
        private void BindRepeater(string query)
        {
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count != 0)
            {
                RPDetails.DataSource = dt;
                RPDetails.DataBind();
            }
            else
            {
                Alertme("Currently, there is not update at notice board. Please keep visiting for update", "warning");
                RPDetails.DataSource = null;
                RPDetails.DataBind();
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

        private void bind_student_class_and_section()
        {
            DataTable dt = mycode.FillData("Select Class_id,Section from admission_registor where admissionserialnumber='" + ViewState["regid"].ToString() + "' and   Transfer_Status in ('New','NT') and  StudentStatus='AV' and Session_id=" + ViewState["sesssionid"].ToString() + " ");
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
                //string trimmed50Char = notice.Substring(0, 50);
                //trimmed50Char += "-";

                ((Label)e.Item.FindControl("lbl_notice2")).Text = notice;
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