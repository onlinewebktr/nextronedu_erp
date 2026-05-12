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
    public partial class Upload_Header_Certificate : System.Web.UI.Page
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
                        if (Session["MsG"] != null)
                        {
                            Alertme(Session["MsG"].ToString(), "success");
                            Session["MsG"] = null;
                        }
                        ViewState["Userid"] = Session["Admin"].ToString();

                        ViewState["Branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        string pagename_current = Path.GetFileName("Upload_Header_Certificate.aspx");
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];


                        get_template();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "IdCardTemplate");
            }
        }

        private void get_template()
        {
            DataTable dt = mycode.FillData("select * from Header_templete where Module_type='Certificate'");
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no records list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    if (FileUpload1.PostedFile.ContentLength > 0)
                    {
                        string filepath = upload_template();
                        if (filepath == "")
                        {
                            Alertme("Max size 500 kb.", "warning");
                        }
                        else
                        {
                            final_submit(filepath);
                            Alertme("Template has been updated successfully.", "success");
                        }
                    }
                    else
                    {
                        Alertme("Please choose valid Image.", "warning");
                    }
                }
                else if (ViewState["Is_add"].ToString() == "1")
                {
                    if (FileUpload1.PostedFile.ContentLength > 0)
                    {
                        string filepath = upload_template();
                        if (filepath == "")
                        {
                            Alertme("Max size 500 kb.", "warning");
                        }
                        else
                        {
                            final_submit(filepath);
                            Alertme("Template has been updated successfully.", "success");
                        }
                    }
                    else
                    {
                        Alertme("Please choose valid Image.", "warning");
                    }
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }



            }
            catch (Exception ex)
            {
            }


        }
        My mycode = new My();
        private void final_submit(string filepath)
        {
            if (mycode.IsUserExist("select Id from Header_templete where Type='Certificate'"))
            {
                SqlCommand cmd;
                string query = "INSERT INTO Header_templete (Type,Status,Path,Name,Updated_by,Updated_date,Module_type) values (@Type,@Status,@Path,@Name,@Updated_by,@Updated_date,@Module_type)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Type", "Certificate");
                cmd.Parameters.AddWithValue("@Status", 1);
                cmd.Parameters.AddWithValue("@Path", filepath);
                cmd.Parameters.AddWithValue("@Name", ddl_type.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Updated_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Updated_date", mycode.date());
                cmd.Parameters.AddWithValue("@Module_type", "Certificate");
                if (My.InsertUpdateData(cmd))
                {
                    get_template();
                }
            }
            else
            {
                SqlCommand cmd;
                string query = "Update Header_templete set Path=@Path,Updated_by=@Updated_by,Updated_date=@Updated_date where Type=@Type";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Type", "Certificate");
                cmd.Parameters.AddWithValue("@Path", filepath);
                cmd.Parameters.AddWithValue("@Updated_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Updated_date", mycode.date());
                if (My.InsertUpdateData(cmd))
                {
                    get_template();
                }
            }
        }
        private string upload_template()
        {
            string filepath = "";
            if (FileUpload1.PostedFile.ContentLength <= 400000)
            {
                string extension = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
                DateTime dt = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                string date = dt.ToString("dd_MM_yyyy");
                string time = dt.ToString("hh_mm_ss");
                String FileName1 = date + time + extension;
                string originalPath = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, "");
                HttpPostedFile postedf = FileUpload1.PostedFile;
                postedf.SaveAs(MapPath("~/Master_Img/" + FileName1));
                filepath = originalPath + "/Master_Img/" + Path.GetFileName(FileName1);
            }
            return filepath;
        }
        protected void lnk_bnr_status_Click1(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            try
            {
               
                if (ViewState["Is_add"].ToString() == "1")
                {
                    change_status(lnk);
                }
                else if (ViewState["Is_Edit"].ToString() == "1")
                {
                    change_status(lnk);

                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void change_status(LinkButton lnk)
        {
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            Label id = (Label)row.FindControl("lbl_id");
            Label lbl_show_status = ((Label)(row.FindControl("lbl_show_status")));
            update_product_status(id.Text, lbl_show_status.Text);
        }

        private void update_product_status(string Id, string c_status)
        {
            SqlDataAdapter ad = new SqlDataAdapter("select * from Header_templete where Id='" + Id + "'", My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Header_templete");
            DataTable dt = ds.Tables[0];
            int rowcount = ds.Tables[0].Rows.Count;
            if (rowcount == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (c_status == "1")
                    {
                        dr["Status"] = "0";
                    }
                    else
                    {
                        dr["Status"] = "1";
                    }
                    SqlCommandBuilder cmb = new SqlCommandBuilder(ad);
                    ad.Update(dt);
                    get_template();
                    Alertme("Status updated successfully.", "success");
                }
            }
        }
        protected void rd_view_ItemDataBound1(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton lnk_bnr_status = (LinkButton)(e.Item.FindControl("lnk_bnr_status"));
                Label lbl_Status = ((Label)(e.Item.FindControl("lbl_show_status")));
                if (lbl_Status.Text == "1")
                {
                    lnk_bnr_status.Text = "ON";
                    lnk_bnr_status.Attributes.Add("class", "lnk-btn-actv");
                }
                else
                {
                    lnk_bnr_status.Text = "OFF";
                    lnk_bnr_status.Attributes.Add("class", "lnk-red-bg");
                }
            }
        }
    }
}