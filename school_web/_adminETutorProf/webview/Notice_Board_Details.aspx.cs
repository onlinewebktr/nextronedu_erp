using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace school_web._adminETutorProf.webview
{
    public partial class Notice_Board_Details : System.Web.UI.Page
    {
        UsesCode mycode = new UsesCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Id"] != null)
                {
                    ViewState["Id"] = Request.QueryString["Id"].ToString();
                    try
                    {


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
            BindRepeater("select  *,format(Date_Main, 'dd-MMM-yyyy') as Date1,format(Date_Main, 'dd') as day,format(Date_Main, 'MMM') as month,format(Date_Main, 'yyyy') as year  from Notice_Board_Details_Teacher where Id=" + ViewState["Id"].ToString() + "   order by  Posted_Idate Desc");
        }
        private void BindRepeater(string query)
        {
            DataTable dt = mycode.FillTable(query);
            if (dt.Rows.Count != 0)
            {
                ViewState["rigid"] =
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
            HtmlGenericControl imgdis = e.Item.FindControl("imgdis") as HtmlGenericControl;
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
            bool flag1 = false;
            string Attachments = ((Label)e.Item.FindControl("lbl_Attachments")).Text;
            int num = 0;
            string lower = Path.GetExtension(Attachments).ToLower();
            string[] strArray = new string[4]
          {
            ".png",
            ".jpeg",
            ".jpg",
            ".gif"
          };
            foreach (string str2 in strArray)
            {
                ++num;
                if (lower == str2)
                {
                    flag1 = true;
                    break;
                }
            }
            if (flag1 == true)
            {
                imgdis.Visible = true;

            }
            else
            {
                imgdis.Visible = false;
            }



        }



        protected void btn_submit_Click(object sender, EventArgs e)
        {
            Response.Redirect("Notice_Board.aspx?regid=" + Session["regid"].ToString(), false);
        }
    }
}