using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace school_web.InstructorProfile
{
    public partial class Notice_Board : System.Web.UI.Page
    {
        UsesCode my = new UsesCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["teacher"] == null)
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
                    txt_date.Text = my.date();
                    txt_enddate.Text = my.date();
                    ViewState["teacher"] = Session["teacher"].ToString();
                    BindGridView();
                }
            }
        }

        private void BindGridView()
        {
            try
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


                    if (Convert.ToInt32(my.ConvertStringToiDate(txt_date.Text)) <= Convert.ToInt32(my.ConvertStringToiDate(txt_enddate.Text)))
                    {

                        my.BindRepeater("select  * from Notice_Board_Details_Teacher where  Posted_Idate>=" + Convert.ToInt32(my.ConvertStringToiDate(txt_date.Text)) + " and Posted_Idate<=" + Convert.ToInt32(my.ConvertStringToiDate(txt_enddate.Text)) + " and   Teacher_Id in ('ALL','" + ViewState["teacher"].ToString() + "') order by  Posted_Idate Desc", RPDetails);

                    }
                    else
                    {
                        Alert("Please select date valid");
                    }

                }
            }
            catch
            {

            }
        }
        public void Alert(string Message)
        {
            lbl_msg.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }
        protected void RPDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {

                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {

                    if (((Label)e.Item.FindControl("lbl_Teacher_Id")).Text == "ALL")
                    {
                        ((Label)e.Item.FindControl("lbl_teacheridname")).Text = "ALL";
                    }
                    else
                    {
                        ((Label)e.Item.FindControl("lbl_teacheridname")).Text = my.get_teachername(((Label)e.Item.FindControl("lbl_Teacher_Id")).Text);

                    }
                    if (((Label)e.Item.FindControl("lbl_Attachment")).Text == "")
                    {
                        HtmlAnchor a1 = e.Item.FindControl("a1") as HtmlAnchor;
                        a1.Visible = false;
                    }
                    else
                    {
                        HtmlAnchor a1 = e.Item.FindControl("a1") as HtmlAnchor;
                        a1.Visible = true;
                    }


                }
            }
            catch { }
        }

        protected void Btn_Find_Click(object sender, EventArgs e)
        {
            BindGridView();
        }
    }
}