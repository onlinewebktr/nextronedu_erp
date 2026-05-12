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

namespace school_web.Library_Admin
{
    public partial class need_help : System.Web.UI.Page
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
                        find_school(); find_user();
                        hd_temp_id.Value = My.create_random_no_otp();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "StdentRegistration");
            }
        }

        private void find_user()
        {
            DataTable dt = mycode.FillData("select name from user_details where user_id='" + ViewState["Userid"].ToString() + "'");
            if (dt.Rows.Count != 0)
            {
                hd_user_name.Value = dt.Rows[0]["name"].ToString();
            }
        }

        private void find_school()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details");
            if (dt.Rows.Count != 0)
            {
                hd_school_name.Value = dt.Rows[0]["firm_name"].ToString();
                hd_school_id.Value = dt.Rows[0]["firm_id"].ToString();
                hd_branch.Value = mycode.get_branch_id(ViewState["Userid"].ToString());
            }
        }


        protected void btn_save_attechment_Click(object sender, EventArgs e)
        {
            try
            {
                string doc_path = "";
                if (FileUpload1.HasFile)
                {
                    doc_path = upload_image(FileUpload1, "complains");
                }

                if (doc_path == "")
                {
                    Alertme("Please choose attachment.", "warning");
                    FileUpload1.Focus();
                    return;
                }

                SqlCommand cmd;
                string strQuery = "INSERT INTO Complain_docs (Request_id,Documents,Upload_by,Date,idate) values (@Request_id,@Documents,@Upload_by,@Date,@idate)";
                cmd = new SqlCommand(strQuery);
                cmd.Parameters.AddWithValue("@Request_id", hd_temp_id.Value);
                cmd.Parameters.AddWithValue("@Documents", doc_path);
                cmd.Parameters.AddWithValue("@Upload_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Date", mycode.datetime());
                cmd.Parameters.AddWithValue("@idate", mycode.idate());
                if (compLN.InsertUpdateDataComp(cmd))
                {
                    bind_doc_grd();
                }
            }
            catch (Exception ex)
            {
            }
        }

        compLN mycode_c = new compLN();
        private void bind_doc_grd()
        {
            DataTable dt = mycode_c.FillDataComp("select *  from Complain_docs where Request_id='" + hd_temp_id.Value + "'");
            if (dt.Rows.Count == 0)
            {
                rd_view.DataSource = null;
                rd_view.DataBind();
                documentS.Visible = false;
            }
            else
            {
                documentS.Visible = true;
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }
        protected void lnkDel_Click(object sender, EventArgs e)
        {
            SqlCommand cmd;
            LinkButton lnk = (LinkButton)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            Label lbl_Id = (Label)row.FindControl("lbl_Id");
            string query = "delete from  Complain_docs where Id=@Id";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Id", lbl_Id.Text);
            if (compLN.InsertUpdateDataComp(cmd))
            {
                Alertme("Attachment has been delete Successfully.", "success");
                bind_doc_grd();
            }
        }

        #region FileUploaD
        private string upload_image(FileUpload FU, string FNmae)
        {
            string idate = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMdd");
            string time = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("hhmmss");
            string dbfilepath = "";
            Boolean FileOK = false;
            Boolean FileSaved = false;

            int k = 0;
            if (FU.HasFile)
            {
                if (FU.FileBytes.Length < 2000000)
                {
                    Session["WorkingImage"] = FU.FileName;
                    string FileExtension = Path.GetExtension(Session["WorkingImage"].ToString()).ToLower();
                    string[] allowedExtensions = { ".png", ".jpeg", ".jpg", ".gif", ".webp", ".pdf", ".PDF", ".doc", ".docx", ".xlsx" };
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
                }
                else
                {
                    Alertme("Please reduce image size (Max 200kb)", "warning");
                    return "";
                }
            }
            else
            {
            }
            if (FileOK)
            {
                try
                {
                    string path = (Server.MapPath("../Master_Img")).ToString();
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
                dbfilepath = originalPath1 + "/Master_Img/" + fileName;
            }
            return dbfilepath;
        }
        #endregion

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_remark.Text == "")
                {
                    Alertme("Please enter remarks.", "warning");
                    txt_remark.Focus();
                    return;
                }
                else
                {
                    save_request();
                }
            }
            catch (Exception ex)
            {
            }
        }

        string compid;
        private void save_request()
        {
            create_sl_no();
            SqlCommand cmd;
            string query = "INSERT INTO Complain_request (School_name,School_name_id,User_id,User_name,Request_id,Remarks,Request_date,Request_idate,Request_by,Status,Last_reply_date,Last_reply_idate,Branch_id,Firm_id) values (@School_name,@School_name_id,@User_id,@User_name,@Request_id,@Remarks,@Request_date,@Request_idate,@Request_by,@Status,@Last_reply_date,@Last_reply_idate,@Branch_id,@Firm_id)";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@School_name", hd_school_name.Value);
            cmd.Parameters.AddWithValue("@School_name_id", hd_school_id.Value);
            cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
            cmd.Parameters.AddWithValue("@User_name", hd_user_name.Value);
            cmd.Parameters.AddWithValue("@Request_id", compid);
            cmd.Parameters.AddWithValue("@Remarks", txt_remark.Text);
            cmd.Parameters.AddWithValue("@Request_date", My.getdate1());
            cmd.Parameters.AddWithValue("@Request_idate", mycode.idate());
            cmd.Parameters.AddWithValue("@Request_by", ViewState["Userid"].ToString());
            cmd.Parameters.AddWithValue("@Status", "Pending");
            cmd.Parameters.AddWithValue("@Last_reply_date", My.getdate1());
            cmd.Parameters.AddWithValue("@Last_reply_idate", mycode.idate());
            cmd.Parameters.AddWithValue("@Branch_id", hd_branch.Value);
            cmd.Parameters.AddWithValue("@Firm_id", ViewState["firm_id"].ToString());
            if (compLN.InsertUpdateDataComp(cmd))
            {




                string idate = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMdd");
                string time = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("hhmmss");
                string idatetime = idate + time;

                //string qry = "insert into Complain_chat(Request_id,Message,Message_by,Date,idate,UserId,ShowStatus,Docs_From,Docs_time) values ('" + compid + "','" + txt_remark.Text + "','1','" + My.getdate1() + "','" + mycode.idate() + "','" + ViewState["Userid"].ToString() + "','1','1','" + idatetime + "'); ";
                //mycode_c.executequery_comp(qry);
                SqlCommand cmd1;
                string query2 = "INSERT INTO Complain_chat (Request_id,Message,Message_by,Date,idate,UserId,ShowStatus,Docs_From,Docs_time) values (@Request_id,@Message,@Message_by,@Date,@idate,@UserId,@ShowStatus,@Docs_From,@Docs_time)";
                cmd1 = new SqlCommand(query2);
                cmd1.Parameters.AddWithValue("@Request_id", compid);
                cmd1.Parameters.AddWithValue("@Message", txt_remark.Text);
                cmd1.Parameters.AddWithValue("@Message_by", "1");// School
                cmd1.Parameters.AddWithValue("@Date", My.getdate1());
                cmd1.Parameters.AddWithValue("@idate", this.mycode.idate());
                cmd1.Parameters.AddWithValue("@UserId", ViewState["Userid"].ToString());
                cmd1.Parameters.AddWithValue("@ShowStatus", "1");
                cmd1.Parameters.AddWithValue("@Docs_From", "1");//pending
                cmd1.Parameters.AddWithValue("@Docs_time", idatetime);

                if (compLN.InsertUpdateDataComp(cmd1))
                {
                    mycode_c.executequery_comp("update Complain_docs set Request_id='" + compid + "',Docs_From='1',Docs_time='" + idatetime + "' where Request_id='" + hd_temp_id.Value + "'");

                    hd_temp_id.Value = My.create_random_no_otp();
                    Alertme("Complain Request has been sent successfully.", "success");
                    empty_form();
                    bind_request_grd();
                    bind_doc_grd();
                }
            }
        }

        private void create_sl_no()
        {
            bool duplicate = true;
            compid = "CO" + My.toint(My.create_random_no_otp());
            while (duplicate)
            {
                DataTable cdt = compLN.dataTable_comp(" select Request_id from Complain_request where Request_id='" + compid + "'");
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    duplicate = false;
                }
                else
                {
                    compid = "CO" + My.toint(My.create_random_no_otp());
                }
            }
        }


        private void bind_request_grd()
        {
            SqlCommand cmd = new SqlCommand("sp_Complain_request");
            cmd.Parameters.AddWithValue("@User_id ", ViewState["Userid"].ToString());
            cmd.Parameters.AddWithValue("@sp_status ", "1");
            DataSet ds = compLN.executeReaderDataSet_comp(cmd);
            DataTable dt = ds.Tables[0];
            int rowcount = ds.Tables[0].Rows.Count;
            if (rowcount > 0)
            {
                rd_views.DataSource = dt;
                rd_views.DataBind();
            }
            else
            {
                rd_views.DataSource = null;
                rd_views.DataBind();
            }
        }

        private void empty_form()
        {
            txt_remark.Text = "";
        }
    }
}