using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using school_web.AppCode;
namespace school_web._adminETutorProf.webview
{
    public partial class Message : System.Web.UI.Page
    {
        UsesCode mycode = new UsesCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["regid"] != null)
                {
                    ViewState["regid"] = Request.QueryString["regid"].ToString();
                    Session["regid"] = ViewState["regid"].ToString();
                    try
                    {
                        txt_date.Text = mycode.date();
                        txt_enddate.Text = mycode.date();
                        Bind_data();
                    }
                    catch
                    {
                    }
                }
            }
        }

        private void Bind_data()
        {
            if (Convert.ToInt32(mycode.ConvertStringToiDate(txt_date.Text)) <= Convert.ToInt32(mycode.ConvertStringToiDate(txt_enddate.Text)))
            {
                BindRepeater("select *,Format(convert(DateTime,Date,103), 'dd-MMM-yyyy') as Date1,Format(convert(DateTime,Date,103), 'dd')   as day,Format(convert(DateTime,Date,103), 'MMM') as month,Format(convert(DateTime,Date,103), 'yyyy') as year from Private_Messages_For_Teacher where Teacher_Id in ('ALL','" + ViewState["regid"].ToString() + "')  and Idate>=" + Convert.ToInt32(mycode.ConvertStringToiDate(txt_date.Text)) + " and Idate<=" + Convert.ToInt32(mycode.ConvertStringToiDate(txt_enddate.Text)) + " order by Idate Desc");
            }
            else
            {
                Alert("Please select valid date");
            }
        }
        protected void btn_submit_Click(object sender, EventArgs e)
        {
            Bind_data();
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