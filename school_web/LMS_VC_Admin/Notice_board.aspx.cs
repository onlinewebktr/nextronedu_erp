using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace school_web.LMS_VC_Admin
{
    public partial class Notice_board1 : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
        string scrpt;
        My imp = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["Admin"] == null)
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
                        ViewState["sesssionid"] = My.get_session_id();
                        ViewState["sesssion"] = My.get_session();
                        ViewState["Branch_id"] = imp.get_branch_id(Session["Admin"].ToString());

                        code.bind_all_ddl_with_all(ddl_CourseCat, "Select Course_Name, course_id from Add_course_table order by Position");
                        if (ddl_CourseCat.SelectedItem.Text == "ALL")
                        {
                            code.bind_ddl_all1(ddl_section, "Select distinct Section  from admission_registor  order by Section");
                        }
                        else
                        {
                            code.bind_ddl_all1(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddl_CourseCat.SelectedValue + "'    order by Section");
                        }
                        BindGridView();

                        if (Request.QueryString["Id"] != null)
                        {
                            ViewState["id"] = Request.QueryString["Id"].ToString();
                            BindDetails();
                        }
                    }
                }
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        private void BindDetails()
        {
            DataTable dt = code.FillTable("select * from Notice_Board_Details where Id='" + ViewState["id"].ToString() + "'");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                btn_submit.Text = "Update";

                txt_notice_details.Text = dt.Rows[0]["Notice"].ToString();
                Hd_Photo.Value = dt.Rows[0]["Attachment"].ToString();

                code.bind_all_ddl_with_all(ddl_CourseCat, "Select Course_Name, course_id from Add_course_table order by Position");

                try
                {
                    if (dt.Rows[0]["Class"].ToString() == "ALL")
                    {
                        ddl_CourseCat.Text = "0";
                        code.bind_ddl_all1(ddl_section, "No records found.");
                    }
                    else
                    {
                        ddl_CourseCat.Text = dt.Rows[0]["Class"].ToString();
                        code.bind_ddl_all1(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddl_CourseCat.SelectedValue + "'    order by Section");
                    }
                    ddl_section.Text = dt.Rows[0]["Section"].ToString();
                }
                catch
                {
                }
                txt_date.Text = dt.Rows[0]["Posted_Date"].ToString();

                if (dt.Rows[0]["Send_Type"].ToString() == "Class Wise")
                {
                    class1.Visible = true;
                    class2.Visible = true;
                    admissionno.Visible = false;
                    rd_Class_wise.Checked = true;
                    rd_individual.Checked = false;
                }
                else
                {
                    rd_Class_wise.Checked = false;
                    rd_individual.Checked = true;
                    class1.Visible = false;
                    class2.Visible = false;
                    admissionno.Visible = true;
                    txt_admission_no.Text = dt.Rows[0]["Admission_no"].ToString();
                }


            }
        }

        protected void ddl_CourseCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_CourseCat.SelectedItem.Text == "ALL")
            {
                code.bind_ddl_all1(ddl_section, "Select distinct Section  from admission_registor  order by Section");
            }
            else
            {
                code.bind_ddl_all1(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddl_CourseCat.SelectedValue + "'    order by Section");
            }
        }


        private void BindGridView()
        {
            code.BindRepeater("select top 10 * from Notice_Board_Details order by  Posted_Idate Desc", RPDetails);
        }
        public void Alert(string Message)
        {
            lbl_msg.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }
        protected void btn_submit_Click(object sender, EventArgs e)
        {
            try
            {

                if (rd_Class_wise.Checked == true)
                {

                    if (ddl_CourseCat.SelectedItem.Text == "Select")
                    {
                        Alert("Select class");
                    }
                    else if (ddl_section.Text == "Select")
                    {
                        Alert("Select section");
                    }
                    else if (txt_date.Text == "")
                    {
                        Alert("Please enter notice date ");
                    }

                    else if (txt_notice_details.Text == "")
                    {
                        Alert("Please enter notice ");
                    }
                    else
                    {
                        final_save_data();
                    }
                }
                else
                {
                    if (txt_admission_no.Text == "")
                    {
                        Alert("Please enter admission no.");
                    }
                    else if (txt_date.Text == "")
                    {
                        Alert("Please enter notice date ");
                    }

                    else if (txt_notice_details.Text == "")
                    {
                        Alert("Please enter notice ");
                    }
                    else
                    {
                        final_save_data();
                    }
                }



            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        private void final_save_data()
        {
            if (btn_submit.Text == "Send")
            {
                string ImagePath = GetImagePath();
                SqlCommand cmd = new SqlCommand("INSERT INTO Notice_Board_Details (Notice,Posted_Date,Posted_Idate,Posted_Time,Class,Section,Send_Status,Attachment,Date_Main,Send_Type,Admission_no,Session_Id,Session_name,Branch_id) values (@Notice,@Posted_Date,@Posted_Idate,@Posted_Time,@Class,@Section,@Send_Status,@Attachment,@Date_Main,@Send_Type,@Admission_no,@Session_Id,@Session_name,@Branch_id)");
                cmd.Parameters.AddWithValue("@Notice", txt_notice_details.Text);
                cmd.Parameters.AddWithValue("@Posted_Date", txt_date.Text);
                cmd.Parameters.AddWithValue("@Posted_Idate", code.ConvertStringToiDate(txt_date.Text));
                cmd.Parameters.AddWithValue("@Posted_Time", code.time());
                cmd.Parameters.AddWithValue("@Send_Status", "Notsend");
                cmd.Parameters.AddWithValue("@Attachment", ImagePath);
                cmd.Parameters.AddWithValue("@Date_Main", code.getdate2(txt_date.Text));

                 
               
                if (rd_Class_wise.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@Send_Type", "Class Wise");
                    cmd.Parameters.AddWithValue("@Admission_no", "N/A");

                    if (ddl_CourseCat.SelectedItem.Text == "ALL")
                    {
                        cmd.Parameters.AddWithValue("@Class", "ALL");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Class", ddl_CourseCat.SelectedValue);

                    }
                    cmd.Parameters.AddWithValue("@Section", ddl_section.Text);



                }
                else
                {
                    //Dictionary<string, object> dc1 = code.getstudent(txt_admission_no.Text);
                    //string Class_id = (String)dc1["Class_id"];
                    //string Section = (String)dc1["Section"];
                    cmd.Parameters.AddWithValue("@Class", "N/A");
                    cmd.Parameters.AddWithValue("@Section", "N/A");
                    cmd.Parameters.AddWithValue("@Send_Type", "Individual");
                    cmd.Parameters.AddWithValue("@Admission_no", txt_admission_no.Text);
                }

                cmd.Parameters.AddWithValue("@Session_Id", ViewState["sesssionid"].ToString());
                cmd.Parameters.AddWithValue("@Session_name", ViewState["sesssion"].ToString());
                cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branch_id"].ToString());
                if (InsertUpdate.InsertUpdateData(cmd))
                {

                    BindGridView();
                    Alert("Notice details have been send sucessfully.");
                    txt_admission_no.Text = "";
                    txt_notice_details.Text = "";


                }

            }
            if (btn_submit.Text == "Update")
            {
                if (fl_Photo.HasFile)
                {
                    Hd_Photo.Value = GetImagePath();
                }
                else
                {

                }
                SqlCommand cmd = new SqlCommand("Update Notice_Board_Details set Notice=@Notice,Posted_Date=@Posted_Date,Posted_Idate=@Posted_Idate,Posted_Time=@Posted_Time,Class=@Class,Section=@Section,Send_Status=@Send_Status,Attachment=@Attachment,Date_Main=@Date_Main,Send_Type=@Send_Type where Id = @Id");

                cmd.Parameters.AddWithValue("@Notice", txt_notice_details.Text);

                cmd.Parameters.AddWithValue("@Posted_Date", txt_date.Text);
                cmd.Parameters.AddWithValue("@Posted_Idate", code.ConvertStringToiDate(txt_date.Text));
                cmd.Parameters.AddWithValue("@Posted_Time", code.time());
                cmd.Parameters.AddWithValue("@Send_Status", "Notsend");

                cmd.Parameters.AddWithValue("@Id", ViewState["id"].ToString());

                if (rd_Class_wise.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@Send_Type", "Class Wise");
                    cmd.Parameters.AddWithValue("@Admission_no", "N/A");

                    if (ddl_CourseCat.SelectedItem.Text == "ALL")
                    {
                        cmd.Parameters.AddWithValue("@Class", "ALL");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Class", ddl_CourseCat.SelectedValue);

                    }
                    cmd.Parameters.AddWithValue("@Section", ddl_section.Text);



                }
                else
                {
                    //Dictionary<string, object> dc1 = code.getstudent(txt_admission_no.Text);
                    //string Class_id = (String)dc1["Class_id"];
                    //string Section = (String)dc1["Section"];
                    cmd.Parameters.AddWithValue("@Class", "N/A");
                    cmd.Parameters.AddWithValue("@Section", "N/A");
                    cmd.Parameters.AddWithValue("@Send_Type", "Individual");
                    cmd.Parameters.AddWithValue("@Admission_no", txt_admission_no.Text);
                }


                cmd.Parameters.AddWithValue("@Attachment", Hd_Photo.Value);
                cmd.Parameters.AddWithValue("@Date_Main", code.getdate2(txt_date.Text));

                if (InsertUpdate.InsertUpdateData(cmd))
                {

                    btn_submit.Text = "Send";
                    Alert(" Notice details have been successfully Updated.");
                    txt_notice_details.Text = "";
                    txt_admission_no.Text = "";

                    BindGridView();
                }
            }
        }

        private string GetImagePath()
        {
            string Path = code.Upload_doc_images(fl_Photo, "/UploadedImage/Notice_board/");
            return Path;
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {

                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_id = (Label)row.FindControl("lbl_Id");
                Label lbl_Heading = (Label)row.FindControl("lbl_Heading");
                Label lbl_Details = (Label)row.FindControl("lbl_Details");
                Label lbl_Attachments = (Label)row.FindControl("lbl_Attachments");
                Label lbl_date = (Label)row.FindControl("lbl_date");
                Label lbl_Classid = (Label)row.FindControl("lbl_Classid");
                Label lbl_section = (Label)row.FindControl("lbl_section");
                Label lbl_Attachment = (Label)row.FindControl("lbl_Attachment");


                txt_date.Text = lbl_date.Text;
                ViewState["id"] = lbl_id.Text;
                btn_submit.Text = "Update";
                Hd_Photo.Value = lbl_Attachment.Text;
                txt_notice_details.Text = lbl_Details.Text;

                code.bind_all_ddl_with_all(ddl_CourseCat, "Select Course_Name, course_id from Add_course_table order by Position");

                if (lbl_Classid.Text == "ALL")
                {
                    ddl_CourseCat.Text = "0";
                    code.bind_ddl_all1(ddl_section, "Select distinct Section  from admission_registor  order by Section");
                }
                else
                {
                    ddl_CourseCat.Text = lbl_Classid.Text;
                    code.bind_ddl_all1(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddl_CourseCat.SelectedValue + "'  order by Section");
                }
                ddl_section.Text = lbl_section.Text;
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

        protected void rd_individual_CheckedChanged(object sender, EventArgs e)
        {
            class1.Visible = false;
            class2.Visible = false;
            admissionno.Visible = true;
        }

        protected void rd_Class_wise_CheckedChanged(object sender, EventArgs e)
        {
            class1.Visible = true;
            class2.Visible = true;
            admissionno.Visible = false;


        }
    }
}