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
    public partial class Notice_board : System.Web.UI.Page
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
                        ViewState["sesssionid"] = My.get_session_id();
                        ViewState["Branchid"] = code.getbranchid(Session["Admin"].ToString());
                        code.bind_all_ddl_with_id(ddl_CourseCat, " Select   Course_Name, course_id from Add_course_table  order by Position");
                        code.bind_ddl_all1(ddl_CourseCat, " Select distinct Section,  from admission_registor  order by Section");

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
            DataTable dt = code.FillTable("select * from News_Events_Details where Id='" + ViewState["id"].ToString() + "'");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                btn_submit.Text = "Update";
                txt_notice_subject.Text = dt.Rows[0]["Heading"].ToString();
                txt_notice_details.Text = dt.Rows[0]["Details"].ToString();
                Hd_Photo.Value = dt.Rows[0]["Attachments"].ToString();
                txt_date.Text = dt.Rows[0]["Posted_Date"].ToString();
                code.bind_all_ddl_with_all(ddl_CourseCat, " Select distinct class, Class_id from admission_registor  order by Position");

                if (dt.Rows[0]["Class"].ToString() == "ALL")
                {
                    ddl_CourseCat.Text = "0";
                    code.bind_ddl_all1(ddl_section, "Select distinct Section  from admission_registor  order by Section");
                }
                else
                {
                    ddl_CourseCat.Text = dt.Rows[0]["Class"].ToString();
                    code.bind_ddl_all1(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddl_CourseCat.SelectedValue + "'    order by Section");
                }
                ddl_section.Text = dt.Rows[0]["Section"].ToString();

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
                code.bind_ddl_all1(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddl_CourseCat.SelectedValue + "' order by Section");
            }
        }


        private void BindGridView()
        {
            code.BindRepeater("select top 10 * from News_Events_Details  order by  Posted_Idate Desc", RPDetails);
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
                    Alert("Please select a date from the calendar");

                }
                else if (txt_notice_subject.Text == "")
                {
                    Alert("Please Insert Subject Name.");
                }
                else if (txt_notice_details.Text == "")
                {
                    Alert("Please Insert Subject Name.");
                }
                else
                {
                    if (btn_submit.Text == "Submit")
                    {
                        string ImagePath = GetImagePath();

                        SqlCommand cmd = new SqlCommand("INSERT INTO News_Events_Details (Heading,Details,Posted_By,Posted_Date,Posted_Idate,Posted_Time,Attachments,Class,Section,Session_id,Branchid) values (@Heading,@Details,@Posted_By,@Posted_Date,@Posted_Idate,@Posted_Time,@Attachments,@Class,@Section,@Session_id,@Branchid)");
                        cmd.Parameters.AddWithValue("@Heading", txt_notice_subject.Text);
                        cmd.Parameters.AddWithValue("@Details", txt_notice_details.Text);
                        cmd.Parameters.AddWithValue("@Posted_By", Session["Admin"].ToString());
                        cmd.Parameters.AddWithValue("@Posted_Date", txt_date.Text);
                        cmd.Parameters.AddWithValue("@Posted_Idate", code.ConvertStringToiDate(txt_date.Text));
                        cmd.Parameters.AddWithValue("@Posted_Time", code.time());
                        cmd.Parameters.AddWithValue("@Attachments", ImagePath);
                        if (ddl_CourseCat.SelectedItem.Text == "ALL")
                        {
                            cmd.Parameters.AddWithValue("@Class", "ALL");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Class", ddl_CourseCat.SelectedValue);

                        }
                        cmd.Parameters.AddWithValue("@Section", ddl_section.Text);

                        cmd.Parameters.AddWithValue("@Session_id", ViewState["sesssionid"].ToString());
                        cmd.Parameters.AddWithValue("@Branchid", ViewState["Branchid"].ToString());


                        if (InsertUpdate.InsertUpdateData(cmd))
                        {

                            BindGridView();
                            Alert("Events details have been successfully added.");
                            txt_notice_subject.Text = "";
                            txt_notice_details.Text = "";
                            txt_date.Text = "";
                            code.bind_all_ddl_with_all(ddl_CourseCat, "Select Course_Name, course_id from Add_course_table order by Position");
                            if (ddl_CourseCat.SelectedItem.Text == "ALL")
                            {
                                code.bind_ddl_all1(ddl_section, "Select distinct section  from Course_or_Subject_Master where  Istatus='1'  order by section");
                            }
                            else
                            {
                                code.bind_ddl_all1(ddl_section, "Select distinct section  from Course_or_Subject_Master where CategoryID ='" + ddl_CourseCat.SelectedValue + "' and Istatus='1'  order by section");
                            }
                        }

                    }
                    if (btn_submit.Text == "Update")
                    {
                        if (fl_Photo.HasFile) { Hd_Photo.Value = GetImagePath(); }
                        else { }
                        SqlCommand cmd = new SqlCommand("Update News_Events_Details set Heading=@Heading,Details=@Details,Posted_By=@Posted_By,Posted_Date=@Posted_Date,Posted_Idate=@Posted_Idate,Posted_Time=@Posted_Time,Attachments=@Attachments,Class=@Class,Section=@Section where Id = @Id");
                        cmd.Parameters.AddWithValue("@Heading", txt_notice_subject.Text);
                        cmd.Parameters.AddWithValue("@Details", txt_notice_details.Text);
                        cmd.Parameters.AddWithValue("@Posted_By", Session["Admin"].ToString());
                        cmd.Parameters.AddWithValue("@Posted_Date", txt_date.Text);
                        cmd.Parameters.AddWithValue("@Posted_Idate", code.ConvertStringToiDate(txt_date.Text));
                        cmd.Parameters.AddWithValue("@Posted_Time", code.time());
                        cmd.Parameters.AddWithValue("@Attachments", Hd_Photo.Value);
                        cmd.Parameters.AddWithValue("@Id",  ViewState["id"].ToString());
                        if (ddl_CourseCat.SelectedItem.Text == "ALL")
                        {
                            cmd.Parameters.AddWithValue("@Class", "ALL");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Class", ddl_CourseCat.SelectedValue);

                        }
                        cmd.Parameters.AddWithValue("@Section", ddl_section.Text);
                        if (InsertUpdate.InsertUpdateData(cmd))
                        {
                            BindGridView();
                            btn_submit.Text = "Submit";
                            Alert(" Event details have been successfully Updated.");
                            txt_notice_subject.Text = "";
                            txt_notice_details.Text = "";
                            txt_date.Text = "";
                            code.bind_all_ddl_with_all(ddl_CourseCat, " Select distinct class, Class_id from admission_registor  order by class");
                            if (ddl_CourseCat.SelectedItem.Text == "ALL")
                            {
                                code.bind_ddl_all1(ddl_section, "Select distinct Section  from admission_registor  order by Section");
                            }
                            else
                            {
                                code.bind_ddl_all1(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddl_CourseCat.SelectedValue + "'    order by Section");
                            }
                        }
                        



                    }

                }
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
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
                Label lbl_section = (Label)row.FindControl("lbl_section");
                ViewState["id"] = lbl_id.Text;
                btn_submit.Text = "Update";
                txt_notice_subject.Text = lbl_Heading.Text;
                txt_notice_details.Text = lbl_Details.Text;
                Hd_Photo.Value = lbl_Attachments.Text;
                txt_date.Text = lbl_date.Text;
                code.bind_all_ddl_with_all(ddl_CourseCat, " Select distinct class, Class_id from admission_registor  order by class");

                if (lbl_Classid.Text == "ALL")
                {
                    ddl_CourseCat.Text = "0";
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
                SqlCommand cmd = new SqlCommand("Delete from News_Events_Details where Id='" + lbl_id.Text + "'");
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




    }
}