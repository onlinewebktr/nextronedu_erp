using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class upload_student_image_individually : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
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
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();
                        ViewState["flag"] = "0";
                        bind_session();
                        ddlsession.SelectedValue = My.get_session_id();
                        hd_session.Value = My.get_session_id();
                        bind_class();
                        ddlclass.SelectedValue = My.get_top_one_class();
                        mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor order by Section");
                        ddl_section.Text = My.get_top_one_section();
                        hd_class.Value = ddlclass.SelectedValue;
                        hd_section.Value = ddl_section.Text;
                        bind_all_data();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "StudentList");
            }
        }

        private void bind_all_data()
        {
            hd_session.Value = ddlsession.SelectedValue;
            hd_class.Value = ddlclass.SelectedValue;
            hd_section.Value = ddl_section.Text;
            hd_student_Type.Value = ddl_studenttype.SelectedValue;
            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name   from admission_registor where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'   and Session_id='" + ddlsession.SelectedValue + "' and Class_Id=" + ddlclass.SelectedValue + " and Section='" + ddl_section.Text + "' order by rollnumber asc");
        }
        protected void ddlclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                find_by_class();
                ViewState["flag"] = "3";
            }
            catch (Exception ex)
            {
            }
        }

        private void find_by_class()
        {
            if (ddlclass.SelectedItem.Text == "All")
            {
                find_by_session();
                ViewState["flag"] = "2";
            }
            else
            {
                hd_session.Value = ddlsession.SelectedValue;
                hd_class.Value = ddlclass.SelectedValue;
                hd_section.Value = "0";
                hd_student_Type.Value = "ALL";

                bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Status='1' and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  order by rollnumber asc");
                string query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where  Session_id='" + ddlsession.SelectedValue + "' and  Class_id='" + ddlclass.SelectedValue + "'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'   group by Transfer_Status ";
            }
        }


        private void bind_session()
        {
            mycode.bind_all_ddl_with_id(ddlsession, "Select  Session,session_id from session_details");
        }

        private void bind_class()
        {
            mycode.bind_all_ddl_with_id_cap_All(ddlclass, "Select Course_Name,course_id,Position from Add_course_table order by Position");
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

        My mycode = new My();
        private void bind_grd_view(string qry)
        {
            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
                lnk_save_images.Visible = false;
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
                lnk_save_images.Visible = true;
            }
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session.", "warning");
                    ddlsession.Focus();
                }
                else if (ddlclass.SelectedItem.Text == "Select")
                {
                    Alertme("Please select class.", "warning");
                    ddlclass.Focus();
                }
                else if (ddl_section.SelectedItem.Text == "Select")
                {
                    Alertme("Please select section.", "warning");
                    ddl_section.Focus();
                }
                else
                {
                    find_by_c_s_a();
                    ViewState["flag"] = "1";
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_by_c_s_a()
        {
            string query2 = "";
            if (ddlclass.SelectedItem.Text == "ALL")
            {
                if (ddl_studenttype.Text == "ALL")
                {
                    hd_session.Value = ddlsession.SelectedValue;
                    hd_class.Value = "0";
                    hd_section.Value = "0";
                    hd_student_Type.Value = ddl_studenttype.SelectedValue;
                    bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where Session_id='" + ddlsession.SelectedValue + "'  and Status='1'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  order by rollnumber asc");
                }
                else
                {
                    hd_session.Value = ddlsession.SelectedValue;
                    hd_class.Value = "0";
                    hd_section.Value = "0";
                    hd_student_Type.Value = ddl_studenttype.SelectedValue;

                    bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Status='1' and Transfer_Status='" + ddl_studenttype.SelectedValue + "' order by rollnumber asc");
                }
            }
            else
            {
                if (ddl_section.Text == "ALL" && ddl_studenttype.Text == "ALL")
                {
                    hd_session.Value = ddlsession.SelectedValue;
                    hd_class.Value = ddlclass.SelectedValue;
                    hd_section.Value = "0";
                    hd_student_Type.Value = ddl_studenttype.SelectedValue;
                    bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Status='1'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  order by rollnumber asc");
                }
                else if (ddl_section.Text != "ALL" && ddl_studenttype.Text == "ALL")
                {
                    hd_session.Value = ddlsession.SelectedValue;
                    hd_class.Value = ddlclass.SelectedValue;
                    hd_section.Value = ddl_section.Text;
                    hd_student_Type.Value = ddl_studenttype.SelectedValue;

                    bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'  and Status='1' and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  order by rollnumber asc");
                }
                else if (ddl_section.Text != "ALL" && ddl_studenttype.Text != "ALL")
                {
                    hd_session.Value = ddlsession.SelectedValue;
                    hd_class.Value = ddlclass.SelectedValue;
                    hd_section.Value = ddl_section.Text;
                    hd_student_Type.Value = ddl_studenttype.SelectedValue;
                    bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and Status='1'  and Transfer_Status ='" + ddl_studenttype.SelectedValue + "'   order by rollnumber asc");
                }
                else if (ddl_section.Text == "ALL" && ddl_studenttype.Text != "ALL")
                {
                    hd_session.Value = ddlsession.SelectedValue;
                    hd_class.Value = ddlclass.SelectedValue;
                    hd_section.Value = "0";
                    hd_student_Type.Value = ddl_studenttype.SelectedValue;

                    bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'    and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1'  order by rollnumber asc");
                }
            }
        }

        protected void ddlsession_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session.", "warning");
                    ddlsession.Focus();
                }
                else
                {
                    mycode.bind_all_ddl_with_id(ddlclass, "  Select  Course_Name, course_id  from Add_course_table order by  Position");
                    find_by_session();
                    ViewState["flag"] = "2";
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_by_session()
        {
            hd_session.Value = ddlsession.SelectedValue;
            hd_class.Value = "0";
            hd_section.Value = "0";
            hd_student_Type.Value = "ALL";
            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'  and  Session_id='" + ddlsession.SelectedValue + "'  order by rollnumber asc");
        }

        protected void lnk_save_image_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                FileUpload FileUpload1 = (FileUpload)row.FindControl("FileUpload1");
                if (FileUpload1.HasFile)
                {
                    if (FileUpload1.FileBytes.Length < 400000)
                    {
                        string imgs_path = upload_image(FileUpload1, lbl_Id.Text);
                        if (imgs_path != "")
                        {
                            string qry = "update admission_registor set studentimagepath='" + imgs_path + "' where Id='" + lbl_Id.Text + "'";
                            My.exeSql(qry);

                            if (ViewState["flag"].ToString() == "0")
                            {
                                bind_all_data();
                            }
                            if (ViewState["flag"].ToString() == "1")
                            {
                                find_by_c_s_a();
                            }
                            if (ViewState["flag"].ToString() == "2")
                            {
                                find_by_session();
                            }
                            if (ViewState["flag"].ToString() == "3")
                            {
                                find_by_class();
                            }
                            Alertme("Image has been updated successfully.", "success");
                        }
                        else
                        {
                            Alertme("Please choose valid student image.", "warning");
                            FileUpload1.Focus();
                        }
                    }
                    else
                    {
                        Alertme("Please reduce image (max size 300kb)", "warning");
                        FileUpload1.Focus();
                    }
                }
                else
                {
                    Alertme("Please choose student image.", "warning");
                    FileUpload1.Focus();
                }
            }
            catch
            {

            }
        }


        private string upload_image(FileUpload FU, string FNmae)
        {
            string idate = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMdd");
            string time = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("hhmmss");
            string dbfilepath = "";
            Boolean FileOK = false;
            Boolean FileSaved = false;
            int k = 0;
            Session["WorkingImage"] = FU.FileName;
            string FileExtension = Path.GetExtension(Session["WorkingImage"].ToString()).ToLower();
            string[] allowedExtensions = { ".png", ".jpeg", ".jpg", ".gif", ".webp" };
            Session["WorkingImage1"] = FNmae + idate + time + FileExtension;
            for (int i = 0; i < allowedExtensions.Length; i++)
            {
                k++;
                if (FileExtension == allowedExtensions[i])
                {
                    FileOK = true;
                    break;
                }
            }


            if (FileOK)
            {
                try
                {
                    string path = (Server.MapPath("../Master_Img/Student")).ToString();
                    FU.SaveAs(path + "/" + Session["WorkingImage1"]);
                    FileSaved = true;
                }
                catch (Exception ex)
                {
                    FileSaved = false;
                    scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
                }
            }
            else
            {
                Alertme("Please select jpg and png image", "warning");
            }
            if (FileSaved)
            {
                string originalPath2 = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, ""); string[] New_originalPath1 = originalPath2.Split('?'); string originalPath1 = New_originalPath1[0].ToString();
                string fileName = Path.GetFileName(Session["WorkingImage1"].ToString());
                dbfilepath = originalPath1 + "/Master_Img/Student/" + fileName;
            }
            return dbfilepath;
        }

        protected void lnk_save_images_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["isupdateD"] = "0";
                update_image();
                if (ViewState["isupdateD"].ToString() == "0")
                {
                    Alertme("Please choose student image.", "warning");
                }

            }
            catch (Exception ex)
            {
                My.submitException(ex, "HomeSlideAdded");
            }
        }

        private void update_image()
        {
            int growcount = rd_view.Items.Count;
            int k = 0;
            for (int i = 0; i < growcount; i++)
            {
                Label lbl_Id = (Label)rd_view.Items[i].FindControl("lbl_id");
                FileUpload FileUpload1 = (FileUpload)rd_view.Items[i].FindControl("FileUpload1");

                if (FileUpload1.HasFile)
                {
                    if (FileUpload1.FileBytes.Length < 400000)
                    {
                        string imgs_path = upload_image(FileUpload1, lbl_Id.Text);
                        if (imgs_path != "")
                        {
                            string qry = "update admission_registor set studentimagepath='" + imgs_path + "' where Id='" + lbl_Id.Text + "'";
                            My.exeSql(qry);

                            if (ViewState["flag"].ToString() == "0")
                            {
                                bind_all_data();
                            }
                            if (ViewState["flag"].ToString() == "1")
                            {
                                find_by_c_s_a();
                            }
                            if (ViewState["flag"].ToString() == "2")
                            {
                                find_by_session();
                            }
                            if (ViewState["flag"].ToString() == "3")
                            {
                                find_by_class();
                            }
                            Alertme("Image has been updated successfully.", "success");
                            ViewState["isupdateD"] = "1";
                        }
                    }
                }
                k++;
            }
        }
    }
}