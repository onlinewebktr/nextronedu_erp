using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Web.UI.HtmlControls;
namespace school_web.LMS_VC_Admin
{
    public partial class All_Notice : System.Web.UI.Page
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
                    code.bind_all_ddl_with_all(ddl_class, " Select distinct class, Class_id from admission_registor  order by class");
                    if (ddl_class.SelectedItem.Text == "ALL")
                    {
                        code.bind_ddl_all1(ddl_section, "Select distinct Section  from admission_registor  order by Section");
                    }
                    else
                    {
                        code.bind_ddl_all1(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddl_class.SelectedValue + "'    order by Section");
                    }


                    BindGridView();
                }
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }
        string query = "";
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
                    if (ddl_send_type.Text == "Class Wise")
                    {
                        if (Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) <= Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)))
                        {
                            if (ddl_class.SelectedItem.Text == "ALL" && ddl_section.Text == "ALL")
                            {
                                code.BindRepeater("select  * from Notice_Board_Details where  Posted_Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and Posted_Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + "  and Send_Type='Class Wise'  order by  Posted_Idate Desc", RPDetails);
                            }
                            else if (ddl_class.SelectedItem.Text != "ALL" && ddl_section.Text == "ALL")
                            {
                                code.BindRepeater("select  * from Notice_Board_Details where Class='" + ddl_class.SelectedValue + "' and Posted_Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and Posted_Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + " and Send_Type='Class Wise'  order by  Posted_Idate Desc", RPDetails);
                            }
                            else if (ddl_class.SelectedItem.Text == "ALL" && ddl_section.Text != "ALL")
                            {
                                code.BindRepeater("select  * from Notice_Board_Details where Section='" + ddl_section.Text + "' and Posted_Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and Posted_Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + " and Send_Type='Class Wise'  order by  Posted_Idate Desc", RPDetails);
                            }
                            else if (ddl_class.SelectedItem.Text != "ALL" && ddl_section.Text != "ALL")
                            {
                                code.BindRepeater("select  * from Notice_Board_Details where Section='" + ddl_section.Text + "'  and Class='" + ddl_class.SelectedValue + "' and Posted_Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and Posted_Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + " and Send_Type='Class Wise'  order by  Posted_Idate Desc", RPDetails);
                            }
                        }
                        else
                        {
                            Alert("Please select date valid");
                        }
                    }
                    else
                    {
                        code.BindRepeater("select  * from Notice_Board_Details where  Posted_Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and Posted_Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + "  and Send_Type='Individual'  order by  Posted_Idate Desc", RPDetails);
                    }


                }
            }
            catch (Exception ex)
            {
            }
        }

        public void Alert(string Message)
        {
            lbl_msg.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }
        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_class.SelectedItem.Text == "ALL")
            {
                code.bind_ddl_all1(ddl_section, "Select distinct Section  from admission_registor  order by Section");
            }
            else
            {
                code.bind_ddl_all1(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddl_class.SelectedValue + "'    order by Section");
            }
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
                Response.Redirect("Notice_board.aspx?id=" + lbl_id.Text, false);
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
                SqlCommand cmd = new SqlCommand("Delete from Notice_Board_Details where Id='" + lbl_id.Text + "'");
                InsertUpdate.InsertUpdateData(cmd);
                BindGridView();
                Alert("successfully deleted.");
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        protected void RPDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            try
            {

                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {

                    if (((Label)e.Item.FindControl("lbl_Classid")).Text == "ALL")
                    {
                        ((Label)e.Item.FindControl("lbl_class")).Text = "ALL";



                    }
                    else if (((Label)e.Item.FindControl("lbl_Classid")).Text == "N/A")
                    {
                        ((Label)e.Item.FindControl("lbl_class")).Text = "N/A";


                        Dictionary<string, object> dc1 = code.getstudent(((Label)e.Item.FindControl("lbl_admission_no")).Text);
                        string class1 = (String)dc1["class"];
                        string Section = (String)dc1["Section"];

                        ((Label)e.Item.FindControl("lbl_class")).Text = class1;
                        ((Label)e.Item.FindControl("lbl_section")).Text = Section;

                    }
                    else
                    {
                        ((Label)e.Item.FindControl("lbl_class")).Text = code.get_class_name(((Label)e.Item.FindControl("lbl_Classid")).Text);

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

       

        protected void ddl_send_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_send_type.Text == "Class Wise")
            {
                class1.Visible = true;
                class2.Visible = true;
            }
            else
            {
                class1.Visible = false;
                class2.Visible = false;
            }
            BindGridView();
        }
    }
}