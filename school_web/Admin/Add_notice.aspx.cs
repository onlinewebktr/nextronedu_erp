using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;

namespace school_web.Admin
{
    public partial class Add_notice : System.Web.UI.Page
    {
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
                    try
                    {
                        const string quote = "\"";
                        string tinyMC = My.get_single_column_data("select TinyMC_link as Column_Name from Firm_Details");
                        if (tinyMC != "")
                        {
                            lt_meata.Text = "<script src=" + quote + tinyMC + quote + " referrerpolicy=" + quote + "origin" + quote + " ></script>";
                        }
                        else
                        {
                            lt_meata.Text = "<script src=" + quote + "https://cdn.tiny.cloud/1/h64ik3cu5x1uocom2fuu89jbo1ah2yqk1rtvpwu420y3ye4w/tinymce/7/tinymce.min.js" + quote + " referrerpolicy=" + quote + "origin" + quote + "></script>";
                        }
                    }
                    catch (Exception ex)
                    {
                    }

                    ViewState["Userid"] = Session["Admin"].ToString();
                    string pagename_current = "Online_Notic_List.aspx";
                    Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                    ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                    ViewState["Is_delete"] = (String)dc1["Is_delete"];
                    ViewState["Is_Download"] = (String)dc1["Is_Download"];
                    ViewState["Is_Print"] = (String)dc1["Is_Print"];
                    ViewState["Is_add"] = (String)dc1["Is_add"];



                    ViewState["sessionid"] = My.get_session_id();
                    ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                    txt_date.Text = mycode.date();


                    if (Request.QueryString["id"] != null)
                    {
                        ViewState["id"] = Request.QueryString["id"];
                        Bind_data_fil();

                    }



                }
            }

            //if(!IsPostBack)
            //{

            //    
            //}

        }

        private void Bind_data_fil()
        {
            DataTable dt = mycode.FillData("Select * from Online_Webiste_Notice where Id=" + ViewState["id"].ToString() + "");
            if (dt.Rows.Count == 0)
            {

                Alertme("Sorry there are no data list exist", "warning");

            }
            else
            {
                btn_find.Text = "Update";
                txt_date.Text = dt.Rows[0]["Date"].ToString();
                txt_heading.Text = dt.Rows[0]["Heading"].ToString();
                txt_info.Value = dt.Rows[0]["Heading_Details"].ToString();
                ViewState["ImagePath"] = dt.Rows[0]["File_path"].ToString();
            }
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
        protected void btn_find_Click(object sender, EventArgs e)
        {
            string ImagePath = "";
            if (txt_date.Text == "")
            {
                Alertme("Please enter date", "warning");
            }
            else if (txt_heading.Text == "")
            {
                Alertme("Please enter notice heading", "warning");
            } 
            else
            {
                ImagePath = GetImagePath();
                SqlCommand cmd;
                if (btn_find.Text == "Save")
                {

                    if (ViewState["Is_add"].ToString() == "1")
                    {
                        string query = "INSERT INTO Online_Webiste_Notice (Heading,Heading_Details,File_path,Date,Idate,Session_id,User_Id) values (@Heading,@Heading_Details,@File_path,@Date,@Idate,@Session_id,@User_Id)";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@Heading", txt_heading.Text);
                        cmd.Parameters.AddWithValue("@Heading_Details", txt_info.Value);
                        cmd.Parameters.AddWithValue("@File_path", ImagePath);
                        cmd.Parameters.AddWithValue("@Date", txt_date.Text);
                        cmd.Parameters.AddWithValue("@Idate", mycode.ConvertStringToiDate(txt_date.Text));
                        cmd.Parameters.AddWithValue("@User_Id", ViewState["Userid"].ToString());
                        cmd.Parameters.AddWithValue("@Session_id", ViewState["sessionid"].ToString());
                        if (My.InsertUpdateData(cmd))
                        {
                            Alertme("Notice has been successfully uploaded", "success");
                            txt_date.Text = mycode.date();
                            txt_heading.Text = "";
                            txt_info.Value = "";
                            btn_find.Text = "Save";
                        }
                    }
                    else
                    {
                        Alertme(My.get_restricted_message(), "warning");
                    }

                }

                else
                {
                    if (ViewState["Is_Edit"].ToString() == "1")
                    {
                        if (FileUpload1.HasFile)
                        {
                            ImagePath = GetImagePath();
                        }
                        else
                        {
                            ImagePath = ViewState["ImagePath"].ToString();
                        }

                        string query = "Update Online_Webiste_Notice set Heading=@Heading,Heading_Details=@Heading_Details,File_path=@File_path,Date=@Date,Idate=@Idate,User_Id=@User_Id,Session_id=@Session_id where Id = @Id";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@Heading", txt_heading.Text);
                        cmd.Parameters.AddWithValue("@Heading_Details", txt_info.Value);
                        cmd.Parameters.AddWithValue("@File_path", ImagePath);
                        cmd.Parameters.AddWithValue("@Date", txt_date.Text);
                        cmd.Parameters.AddWithValue("@Idate", mycode.ConvertStringToiDate(txt_date.Text));
                        cmd.Parameters.AddWithValue("@User_Id", ViewState["Userid"].ToString());
                        cmd.Parameters.AddWithValue("@Session_id", ViewState["sessionid"].ToString());
                        cmd.Parameters.AddWithValue("@Id", ViewState["id"].ToString());
                        if (My.InsertUpdateData(cmd))
                        {
                            Alertme("Notice has been successfully update", "success");
                            txt_date.Text = mycode.date();
                            txt_heading.Text = "";
                            txt_info.Value = "";
                            btn_find.Text = "Save";
                        }
                    }
                    else
                    {
                        Alertme(My.get_restricted_message(), "warning");
                    } 
                } 
            }
        }

        private string GetImagePath()
        {
            UsesCode code = new UsesCode();
            string Path = code.Upload_doc_images(FileUpload1, "/UploadedImage/Notice_board/");
            return Path;
        }
    }
}