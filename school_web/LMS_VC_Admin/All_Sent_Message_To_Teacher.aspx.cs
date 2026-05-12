using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace school_web.LMS_VC_Admin
{
    public partial class Send_All_Sent_Message_To_Teacher : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
        string scrpt;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    txt_date.Text = code.date();
                    txt_enddate.Text = code.date();
                    code.bind_all_ddl_with_all(ddl_teacher, "select name as Name,user_id as  UserID from user_details where (User_Type='Teacher' or User_Type='Principal')  order by Name  asc");

                    BindGridView();
                }
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
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
                    if (ddl_teacher.SelectedItem.Text == "ALL")
                    {

                        if (Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) <= Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)))
                        {

                            code.BindRepeater("select  * from Private_Messages_For_Teacher where  Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + "  order by  Idate Desc", RPDetails);

                        }
                        else
                        {
                            Alert("Please select date valid");
                        }
                    }
                    else
                    {
                        code.BindRepeater("select  * from Private_Messages_For_Teacher where  Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + " and Teacher_Id='" + ddl_teacher.SelectedValue + "' order by  Idate Desc", RPDetails);
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
                        ((Label)e.Item.FindControl("lbl_teacheridname")).Text = code.get_teachername(((Label)e.Item.FindControl("lbl_Teacher_Id")).Text);

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

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {

                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_id = (Label)row.FindControl("lbl_Id");
                Response.Redirect("Send_Teacher_Message.aspx?id=" + lbl_id.Text, false);
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        protected void lnk_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_id = (Label)row.FindControl("lbl_Id");
                SqlCommand cmd = new SqlCommand("Delete from Private_Messages_For_Teacher where Id='" + lbl_id.Text + "'");
                InsertUpdate.InsertUpdateData(cmd);
                BindGridView();
                Alert("successfully deleted.");
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }
    }
}