using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
using System.IO;
using Ionic.Zip;

namespace school_web.Admin
{
    public partial class Download_bulk_student_photo : System.Web.UI.Page
    {
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
        protected void Page_Load(object sender, EventArgs e)
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
                    ViewState["Userid"] = Session["Admin"].ToString();
                  
                    ViewState["Branchid"] = mycode.getbranchid(Session["Admin"].ToString());

                    mycode.bind_all_ddl_with_id(ddl_CourseCat, "Select   Course_Name, course_id from Add_course_table order by Position asc");

                    // mycode.bind_ddl(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddl_CourseCat.SelectedValue + "'  order by Section");

                    mycode.bind_all_ddl_with_id(ddl_session, " select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                    ddl_session.SelectedValue = My.get_session_id();

                    string pagename_current = Path.GetFileName(Request.Path);
                    Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                    ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                    ViewState["Is_delete"] = (String)dc1["Is_delete"];
                    ViewState["Is_Download"] = (String)dc1["Is_Download"];
                    ViewState["Is_Print"] = (String)dc1["Is_Print"];
                    ViewState["Is_add"] = (String)dc1["Is_add"];


                }
            }
        }



        protected void ddl_CourseCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_CourseCat.SelectedItem.Text == "Select")
            {
                Alertme("Please select class.", "warning");
            }
            else
            {
                mycode.bind_ddl(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddl_CourseCat.SelectedValue + "'  order by Section");
            }

        }


        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {


                // copy for newfolder



                if (ViewState["Is_add"].ToString() == "1")
                {
                    copy_data_newfolder();
                }
                else if (ViewState["Is_Edit"].ToString() == "1")
                {
                    copy_data_newfolder();
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");

                }







                //string zipname = "Filedata";//ddl_CourseCat.SelectedItem.Text + "_" + ddl_section.SelectedItem.Text;
                //string get_url = My.URL();
                //string query = "Select admissionserialnumber,studentimagepath from admission_registor where Class_id='" + ddl_CourseCat.SelectedValue + "' and Section='" + ddl_section.Text + "' and Session_id='" + ViewState["sesssionid"].ToString() + "' and (studentimagepath!='' or studentimagepath!=null)";
                //DataTable dt = mycode.FillData(query);

                //if (dt.Rows.Count == 0)
                //{
                //}
                //else
                //{


                //    using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile())
                //    {
                //        foreach (DataRow dr in dt.Rows)
                //        {
                //            string admissionserialnumber = "Abcdes_" + dr["admissionserialnumber"].ToString();
                //            string patha1 = dr["studentimagepath"].ToString();
                //            if (patha1 != "")
                //            {

                //                patha1 = "../" + patha1.Replace(get_url, "");



                //                string ss = Server.MapPath(patha1);
                //                System.IO.FileInfo file = new System.IO.FileInfo(ss);




                //                string extension = file.Extension;



                //                if (extension != "")
                //                {
                //                    string finafilename = admissionserialnumber + extension;
                //                    zip.AddFile(finafilename.ToString(), "File");
                //                }
                //            }
                //        }
                //        Response.Clear();
                //        Response.AddHeader("Content-Disposition", "Document; filename=" + zipname + ".zip");
                //        Response.ContentType = "application/zip";
                //        zip.Save(Response.OutputStream);
                //        Response.End();
                //    }
                //}
            }
            catch(Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }

        private void copy_data_newfolder()
        {
            string get_url = My.URL();


            string query = "Select admissionserialnumber,studentimagepath from admission_registor where Class_id='" + ddl_CourseCat.SelectedValue + "' and Section='" + ddl_section.Text + "' and Session_id='" + ddl_session.SelectedValue + "' and (studentimagepath!='' or studentimagepath!=null) and Status=1";
            DataTable dt = mycode.FillData(query);

            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                delete_file();
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    string admissionserialnumber = dt.Rows[i]["admissionserialnumber"].ToString();
                    string studentimagepath = dt.Rows[i]["studentimagepath"].ToString();

                    Bulk_finl_Download_file(admissionserialnumber, studentimagepath);

                }
                zip_file(ddl_CourseCat.SelectedItem.Text + "_" + ddl_section.Text); 
            }
        }
        private void zip_file(string downloadfilename)
        {
            string ss = Server.MapPath("temp");
            string SourceFolderPath = ss;// System.IO.Path.Combine(ss, "Initial");

            Response.Clear();
            Response.ContentType = "application/zip";
            Response.AddHeader("Content-Disposition", String.Format("attachment; filename={0}", downloadfilename + ".zip"));

            bool recurseDirectories = true;
            using (ZipFile zip = new ZipFile())
            {
                zip.AddSelectedFiles("*", SourceFolderPath, string.Empty, recurseDirectories);
                zip.Save(Response.OutputStream);
            }
            Response.End();

        }
        private void Bulk_finl_Download_file(string admissionserialnumber, string studentimagepath)
        {
            string get_url = My.URL();
            string patha1 = studentimagepath;
            string modifiedString = admissionserialnumber.Replace("/", "-");
            if (patha1 != "")
            {
                patha1 = "../" + patha1.Replace(get_url, "");
                string ss = Server.MapPath(patha1);
                System.IO.FileInfo file = new System.IO.FileInfo(ss);
                string extension = file.Extension;
                if (extension != "")
                {
                    string filename = modifiedString;
                    string finalpath = Server.MapPath("temp");
                    System.IO.File.Copy(ss, finalpath + "/" + filename + extension);
                }
            }
        }

        private void delete_file()
        {
            try
            {
                string ss = Server.MapPath("temp");
                System.IO.DirectoryInfo di = new DirectoryInfo(ss);

                foreach (FileInfo file in di.GetFiles())
                {

                    file.Delete();
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}