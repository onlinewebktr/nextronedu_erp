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
    public partial class View_All_Message_Sent_to_Student : System.Web.UI.Page
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
                    code.bind_all_ddl_with_all(ddl_class, "Select Course_Name, course_id from Add_course_table order by Position");
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
                            if (ddl_class.SelectedItem.Text == "ALL" && ddl_section.SelectedItem.Text == "ALL")
                            {
                                code.BindRepeater("select  * from Private_Messages where  Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + " and Send_Type='Class Wise'  order by  Idate Desc", RPDetails);
                            }
                            else if (ddl_class.SelectedItem.Text != "ALL" && ddl_section.SelectedItem.Text == "ALL")
                            {
                                code.BindRepeater("select  * from Private_Messages where Class_Id='" + ddl_class.SelectedValue + "' and Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + " and Send_Type='Class Wise'  order by  Idate Desc", RPDetails);
                            }
                            else if (ddl_class.SelectedItem.Text == "ALL" && ddl_section.SelectedItem.Text != "ALL")
                            {
                                code.BindRepeater("select  * from Private_Messages where Section_Id='" + ddl_section.Text + "' and Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + " and Send_Type='Class Wise'  order by  Idate Desc", RPDetails);
                            }
                            else if (ddl_class.SelectedItem.Text != "ALL" && ddl_section.SelectedItem.Text != "ALL")
                            {
                                code.BindRepeater("select  * from Private_Messages where Section_Id='" + ddl_section.Text + "'  and Class_Id='" + ddl_class.SelectedValue + "' and Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + " and Send_Type='Class Wise' order by  Idate Desc", RPDetails);
                            }
                        }
                        else
                        {
                            Alert("Please select date valid");
                        }
                    }
                    else
                    {
                        code.BindRepeater("select  * from Private_Messages where  Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + " and Send_Type='Individual'  order by  Idate Desc", RPDetails);
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
                Response.Redirect("Send_Message_Class_Wise.aspx?id=" + lbl_id.Text, false);
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
                SqlCommand cmd = new SqlCommand("Delete from Private_Messages where Id='" + lbl_id.Text + "'");
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
                       // ((Label)e.Item.FindControl("lbl_class")).Text = "N/A";

                        Dictionary<string, object> dc1 = code.getstudent(((Label)e.Item.FindControl("lbl_admission_no")).Text);
                        string class1 = (String)dc1["class"];
                        string Section = (String)dc1["Section"];

                        ((Label)e.Item.FindControl("lbl_class")).Text = class1;
                        ((Label)e.Item.FindControl("lbl_Section_Id")).Text = Section;

                    }
                    else
                    {
                        ((Label)e.Item.FindControl("lbl_class")).Text = code.get_class_name(((Label)e.Item.FindControl("lbl_Classid")).Text);

                    }

                    //if (((Label)e.Item.FindControl("lbl_Section_Id")).Text == "0")
                    //{
                    //    ((Label)e.Item.FindControl("lbl_section")).Text = "ALL";



                    //}
                    //else
                    //{
                    //    ((Label)e.Item.FindControl("lbl_section")).Text = code.get_sectionname(((Label)e.Item.FindControl("lbl_Section_Id")).Text);

                    //}


                    if (((Label)e.Item.FindControl("lbl_Attachments")).Text == "")
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