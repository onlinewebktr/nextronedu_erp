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

namespace school_web.complain
{
    public partial class complain : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    bind_request_grd("All", 0, 0);
                    ViewState["flag"] = "0";
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Coupon-list");
            }
        }


        private void bind_request_grd(string type, int idate1, int idate21)
        {
            SqlCommand cmd = new SqlCommand("sp_Complain_request");


            if (type == "All")
            {
                if (ddl_complain_status.SelectedValue == "0")
                {
                    cmd.Parameters.AddWithValue("@sp_status ", "6");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Status ", ddl_complain_status.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@sp_status ", "7");
                }
            }
            else if (type == "School")
            {
                cmd.Parameters.AddWithValue("@School_name ", txt_school_name.Text);
                cmd.Parameters.AddWithValue("@sp_status ", "10");
            } 
            else
            {
                if (ddl_complain_status.SelectedValue == "0")
                {
                    cmd.Parameters.AddWithValue("@fromdate ", idate1);
                    cmd.Parameters.AddWithValue("@todate ", idate21);
                    cmd.Parameters.AddWithValue("@sp_status ", "9");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Status ", ddl_complain_status.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@fromdate ", idate1);
                    cmd.Parameters.AddWithValue("@todate ", idate21);
                    cmd.Parameters.AddWithValue("@sp_status ", "8");
                }
            }

            DataSet ds = compLN.executeReaderDataSet_comp(cmd);
            DataTable dt = ds.Tables[0];
            int rowcount = ds.Tables[0].Rows.Count;
            if (rowcount > 0)
            {
                RPDetails.DataSource = dt;
                RPDetails.DataBind();
                lbl_all_count.Text = dt.Rows.Count.ToString();
            }
            else
            {
                RPDetails.DataSource = null;
                RPDetails.DataBind();

                lbl_all_count.Text = "0";
                //=============================
                lbl_pending_count.Text = "0";
                lbl_runing_count.Text = "0";
                lbl_hold_count.Text = "0";
                lbl_closed_count.Text = "0";
            }
        }






        #region ajex search
        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static List<string> Getcusmob(string search_area)
        {
            string prefixText = search_area;
            List<string> areaname = new List<string>();
            SqlConnection conn = new SqlConnection(compLN.comp);
            SqlConnection sqlCnn;
            SqlCommand sqlCmd;
            string sql = null;
            string sql2 = "select DISTINCT School_name from Complain_request where School_name Like '" + prefixText + "%' order by School_name asc";
            sql = sql2;
            sqlCnn = new SqlConnection(compLN.comp);
            try
            {
                sqlCnn.Open();
                sqlCmd = new SqlCommand(sql, sqlCnn);
                SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                while (sqlReader.Read())
                {
                    string value = sqlReader.GetValue(0).ToString();
                    areaname.Add(value);
                }
                sqlReader.NextResult();

                while (sqlReader.Read())
                {
                    string value = sqlReader.GetValue(0).ToString();
                    areaname.Add(value);
                    areaname.Add(value);
                }
                sqlReader.Close();
                sqlCmd.Dispose();
                sqlCnn.Close();
            }
            catch (Exception ex)
            {
            }
            return areaname;
        }
        #endregion

        protected void lnk_update_status_Click(object sender, EventArgs e)
        {
            try
            {

                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_req_id = (Label)row.FindControl("lbl_req_id");
                Label lbl_date = (Label)row.FindControl("lbl_date");
                Label lbl_school_name = (Label)row.FindControl("lbl_school_name");
                ViewState["ComplainID"] = lbl_req_id.Text;

                lbl_school_names.Text = lbl_school_name.Text;
                lbl_complain_id.Text = lbl_req_id.Text;
                lbl_complain_date.Text = lbl_date.Text;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

            }
            catch (Exception ex)
            {
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
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    Alertme("Please choose attachment.");
                    FileUpload1.Focus();
                    return;
                }

                SqlCommand cmd;
                string strQuery = "INSERT INTO Complain_docs (Request_id,Documents,Upload_by,Date,idate,Docs_From) values (@Request_id,@Documents,@Upload_by,@Date,@idate,@Docs_From)";
                cmd = new SqlCommand(strQuery);
                cmd.Parameters.AddWithValue("@Request_id", ViewState["ComplainID"].ToString());
                cmd.Parameters.AddWithValue("@Documents", doc_path);
                cmd.Parameters.AddWithValue("@Upload_by", "Admin");
                cmd.Parameters.AddWithValue("@Date", mycode.datetime());
                cmd.Parameters.AddWithValue("@idate", mycode.idate());
                cmd.Parameters.AddWithValue("@Docs_From", "11");
                if (compLN.InsertUpdateDataComp(cmd))
                {
                    Alertme("Attachment has been saved successfully.");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
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
            DataTable dt = mycode_c.FillDataComp("select *  from Complain_docs where Request_id='" + ViewState["ComplainID"].ToString() + "' and Docs_From='11'");
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                Alertme("Attachment has been delete Successfully.");
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
                    Alertme("Please reduce image size (Max 200kb)");
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
                Alertme("Please select jpg and png image");
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
                if (ddl_status.SelectedItem.Text == "Select")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    Alertme("Please select status.");
                    ddl_status.Focus();
                    return;
                }
                if (txt_remark.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    Alertme("Please enter remarks.");
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


        private void save_request()
        {
            try
            {
                string idate = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMdd");
                string time = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("hhmmss");
                string idatetime = idate + time;

                string qry = "insert into Complain_chat(Request_id,Message,Message_by,Date,idate,UserId,ShowStatus,Docs_From,Docs_time) values ('" + ViewState["ComplainID"].ToString() + "','" + txt_remark.Text + "','2','" + My.getdate1() + "','" + mycode.idate() + "','Admin','1','" + ddl_status.SelectedValue + "','" + idatetime + "'); update Complain_docs set Docs_From='" + ddl_status.SelectedValue + "',Docs_time='" + idatetime + "' where Request_id='" + ViewState["ComplainID"].ToString() + "' and Docs_From='11'; update Complain_request set Status='" + ddl_status.SelectedItem.Text + "',Last_reply_date='" + My.getdate1() + "',Last_reply_idate='" + mycode.idate() + "' where Request_id='" + ViewState["ComplainID"].ToString() + "'";
                mycode_c.executequery_comp(qry);

                Alertme("Complain status has been updated successfully.");
                empty_form();

                if (ViewState["flag"].ToString() == "0")
                {
                    bind_request_grd("All", 0, 0);
                }
                if (ViewState["flag"].ToString() == "1")
                {
                    find_by_date();
                }
            }
            catch
            {
            }
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        private void empty_form()
        {
            txt_remark.Text = "";
        }

        string scrpt;
        private void Alertme(string msg)
        {
            lblmessage.Text = msg;
            scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                find_by_date();
                ViewState["flag"] = "1";
            }
            catch (Exception ex)
            {
            }
        }

        private void find_by_date()
        {
            if (txt_s_date.Text == "")
            {
                Alertme("Please choose from date");
                txt_s_date.Focus();
            }
            else if (txt_e_date.Text == "")
            {
                Alertme("Please choose to date");
                txt_e_date.Focus();
            }
            else
            {
                int idate = mycode.ConvertStringToiDate(txt_s_date.Text);
                int idate2 = mycode.ConvertStringToiDate(txt_e_date.Text);
                if (idate > idate2)
                {
                    Alertme("End date cannot be less than start date.");
                }
                else
                {
                    string sdate = txt_s_date.Text;
                    string sday = sdate.Substring(0, 2);
                    string smonth = sdate.Substring(3, 2);
                    string syear = sdate.Substring(6, 4);

                    string edate = txt_e_date.Text;
                    string eday = edate.Substring(0, 2);
                    string emonth = edate.Substring(3, 2);
                    string eyear = edate.Substring(6, 4);

                    int idate1 = Convert.ToInt32(syear + smonth + sday);
                    int idate21 = Convert.ToInt32(eyear + emonth + eday);
                    if (idate > idate2)
                    {
                        Alertme("End date cannot be less than start date.");
                    }
                    else
                    {
                        bind_request_grd("Date", idate1, idate21);
                    }
                }
            }
        }

        protected void ddl_complain_status_SelectedIndexChanged(object sender, EventArgs e)
        {
            bind_request_grd("All", 0, 0);
            ViewState["flag"] = "0";
        }


        int Pending = 0; int Process = 0; int Hold = 0; int Closed = 0;
        protected void RPDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (((Label)e.Item.FindControl("lbl_status")).Text == "Pending")
                {
                    Pending++;
                }
                if (((Label)e.Item.FindControl("lbl_status")).Text == "Process")
                {
                    Process++;
                }
                if (((Label)e.Item.FindControl("lbl_status")).Text == "Hold")
                {
                    Hold++;
                }
                if (((Label)e.Item.FindControl("lbl_status")).Text == "Closed")
                {
                    Closed++;
                }
            }

            //=============================
            lbl_pending_count.Text = Pending.ToString();
            lbl_runing_count.Text = Process.ToString();
            lbl_hold_count.Text = Hold.ToString();
            lbl_closed_count.Text = Closed.ToString();
        }

        protected void btn_find_by_school_Click(object sender, EventArgs e)
        {
            try
            {
                find_by_school();
                ViewState["flag"] = "2";
            }
            catch (Exception ex)
            {
            }
        }

        private void find_by_school()
        {
            bind_request_grd("School", 0, 0);
        }
    }
}