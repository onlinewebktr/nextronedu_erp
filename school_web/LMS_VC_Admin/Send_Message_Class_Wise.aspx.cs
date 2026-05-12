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
namespace school_web.LMS_VC_Admin
{
    public partial class Send_Message_Class_Wise : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
        string scrpt;
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
                        ViewState["Admin"] = Session["Admin"].ToString();
                        ViewState["Branchid"] = code.getbranchid(Session["Admin"].ToString());
                        ViewState["sesssionid"] = My.get_session_id();
                        code.bind_all_ddl_with_all(ddl_CourseCat, "Select Course_Name, course_id from Add_course_table order by Position");
                        if (ddl_CourseCat.SelectedItem.Text == "ALL")
                        {
                            code.bind_ddl_all1(ddl_section, "Select distinct Section  from admission_registor  order by Section");

                        }
                        else
                        {
                            code.bind_ddl_all1(ddl_section, "SSelect distinct Section  from admission_registor where Class_id ='" + ddl_CourseCat.SelectedValue + "'    order by Section");

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
            DataTable dt = code.FillTable("select * from Private_Messages where Id='" + ViewState["id"].ToString() + "'");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                btn_submit.Text = "Update";
                txt_notice_subject.Text = dt.Rows[0]["Subject"].ToString();
                txt_notice_details.Text = dt.Rows[0]["Details"].ToString();
                Hd_Photo.Value = dt.Rows[0]["Attachments"].ToString();
                code.bind_all_ddl_with_all(ddl_CourseCat, "Select Course_Name, course_id from Add_course_table order by Position");
                try
                {
                   
                    if (dt.Rows[0]["Class_Id"].ToString() == "ALL")
                    {
                        ddl_CourseCat.SelectedValue = "0";
                        code.bind_ddl_all1(ddl_section, "Select distinct Section  from admission_registor  order by Section");
                    }
                    else
                    {
                        ddl_CourseCat.Text = dt.Rows[0]["Class_Id"].ToString();

                        code.bind_ddl_all1(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddl_CourseCat.SelectedValue + "'    order by Section");
                    }

                    ddl_section.Text = dt.Rows[0]["Section_Id"].ToString();
                }
                catch
                {

                }

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
            code.BindRepeater("select top 10 * from Private_Messages   order by  Idate Desc", RPDetails);
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
                    else if (txt_notice_subject.Text == "")
                    {
                        Alert("Please enter subject ");
                    }
                    else if (txt_notice_details.Text == "")
                    {
                        Alert("Please enter message details ");
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

                    else if (txt_notice_subject.Text == "")
                    {
                        Alert("Please enter subject ");
                    }
                    else if (txt_notice_details.Text == "")
                    {
                        Alert("Please enter message details ");
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
            if (btn_submit.Text == "Submit")
            {
                string ImagePath = GetImagePath();

                SqlCommand cmd = new SqlCommand("INSERT INTO Private_Messages (Uid,Message_By,Subject,Details,Sender_Id,Attachments,Date,Idate,Time,Class_Id,Section_Id,Send_Status,Send_Type,Admission_no,Branch_id,Session_id,Created_by) values (@Uid,@Message_By,@Subject,@Details,@Sender_Id,@Attachments,@Date,@Idate,@Time,@Class_Id,@Section_Id,@Send_Status,@Send_Type,@Admission_no,@Branch_id,@Session_id,@Created_by)"); 

                cmd.Parameters.AddWithValue("@Uid", "");
                cmd.Parameters.AddWithValue("@Message_By", "Admin");
                cmd.Parameters.AddWithValue("@Subject", txt_notice_subject.Text);
                cmd.Parameters.AddWithValue("@Details", txt_notice_details.Text);
                cmd.Parameters.AddWithValue("@Sender_Id", ViewState["Admin"].ToString());
                cmd.Parameters.AddWithValue("@Attachments", ImagePath);
                cmd.Parameters.AddWithValue("@Date", code.date());
                cmd.Parameters.AddWithValue("@Idate", code.idate());
                cmd.Parameters.AddWithValue("@Time", code.time());
                cmd.Parameters.AddWithValue("@Send_Status", "Notsend");
                if (rd_Class_wise.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@Send_Type", "Class Wise");
                    cmd.Parameters.AddWithValue("@Admission_no", "N/A");

                    if (ddl_CourseCat.SelectedItem.Text == "ALL")
                    {
                        cmd.Parameters.AddWithValue("@Class_Id", "ALL");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Class_Id", ddl_CourseCat.SelectedValue);

                    }
                    cmd.Parameters.AddWithValue("@Section_Id", ddl_section.Text);



                }
                else
                {
                    //Dictionary<string, object> dc1 = code.getstudent(txt_admission_no.Text);
                    //string Class_id = (String)dc1["Class_id"];
                    //string Section = (String)dc1["Section"];
                    cmd.Parameters.AddWithValue("@Class_Id", "N/A");
                    cmd.Parameters.AddWithValue("@Section_Id", "N/A");
                    cmd.Parameters.AddWithValue("@Send_Type", "Individual");
                    cmd.Parameters.AddWithValue("@Admission_no", txt_admission_no.Text);
                }

                cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branchid"].ToString());
                cmd.Parameters.AddWithValue("@Session_id", ViewState["sesssionid"].ToString());
                cmd.Parameters.AddWithValue("@Created_by", ViewState["Admin"].ToString());
                if (InsertUpdate.InsertUpdateData(cmd))
                {

                    BindGridView();
                    Alert("Message details have been successfully added.");
                    txt_notice_subject.Text = "";
                    txt_notice_details.Text = "";
                    txt_admission_no.Text = "";

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
                SqlCommand cmd = new SqlCommand("Update Private_Messages set Uid=@Uid,Message_By=@Message_By,Subject=@Subject,Details=@Details,Sender_Id=@Sender_Id,Attachments=@Attachments,Date=@Date,Idate=@Idate,Time=@Time,Class_Id=@Class_Id,Section_Id=@Section_Id,Send_Status=@Send_Status,Send_Type=@Send_Type,Admission_no=@Admission_no where Id = @Id");
                cmd.Parameters.AddWithValue("@Uid", "");
                cmd.Parameters.AddWithValue("@Message_By", "Admin");
                cmd.Parameters.AddWithValue("@Subject", txt_notice_subject.Text);
                cmd.Parameters.AddWithValue("@Details", txt_notice_details.Text);
                cmd.Parameters.AddWithValue("@Sender_Id", ViewState["Admin"].ToString());
                cmd.Parameters.AddWithValue("@Attachments", Hd_Photo.Value);
                cmd.Parameters.AddWithValue("@Date", code.date());
                cmd.Parameters.AddWithValue("@Idate", code.idate());
                cmd.Parameters.AddWithValue("@Time", code.time());




                cmd.Parameters.AddWithValue("@Id", ViewState["id"].ToString());
                cmd.Parameters.AddWithValue("@Send_Status", "Notsend");
                if (rd_Class_wise.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@Send_Type", "Class Wise");
                    cmd.Parameters.AddWithValue("@Admission_no", "N/A");

                    if (ddl_CourseCat.SelectedItem.Text == "ALL")
                    {
                        cmd.Parameters.AddWithValue("@Class_Id", "ALL");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Class_Id", ddl_CourseCat.SelectedValue);

                    }
                    cmd.Parameters.AddWithValue("@Section_Id", ddl_section.Text);



                }
                else
                {
                    //Dictionary<string, object> dc1 = code.getstudent(txt_admission_no.Text);
                    //string Class_id = (String)dc1["Class_id"];
                    //string Section = (String)dc1["Section"];
                    cmd.Parameters.AddWithValue("@Class_Id", "N/A");
                    cmd.Parameters.AddWithValue("@Section_Id", "N/A");
                    cmd.Parameters.AddWithValue("@Send_Type", "Individual");
                    cmd.Parameters.AddWithValue("@Admission_no", txt_admission_no.Text);
                }
                if (InsertUpdate.InsertUpdateData(cmd))
                {
                    BindGridView();
                    btn_submit.Text = "Submit";
                    Alert(" Message have been successfully Updated.");
                    txt_notice_subject.Text = "";
                    txt_notice_details.Text = "";
                    txt_admission_no.Text = "";


                }
            }
        }
        public string GetImagePath()
        {
            string Path = code.Upload_doc_images(fl_Photo, "/UploadedImage/Noticeboard/");
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
                Label lbl_section = (Label)row.FindControl("lbl_Section_Id");
                ViewState["id"] = lbl_id.Text;
                btn_submit.Text = "Update";
                txt_notice_subject.Text = lbl_Heading.Text;
                txt_notice_details.Text = lbl_Details.Text;
                Hd_Photo.Value = lbl_Attachments.Text;

                code.bind_all_ddl_with_all(ddl_CourseCat, "Select Course_Name, course_id from Add_course_table order by Position");
                if (lbl_Classid.Text == "ALL")
                {
                    ddl_CourseCat.SelectedValue = "0";
                    code.bind_ddl_all1(ddl_section, "Select distinct Section  from admission_registor  order by Section");
                }
                else
                {
                    ddl_CourseCat.Text = lbl_Classid.Text;
                    code.bind_ddl_all1(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddl_CourseCat.SelectedValue + "'    order by Section");
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