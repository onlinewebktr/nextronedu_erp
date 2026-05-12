using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

using System.Data;
using System.IO;
using school_web.AppCode;
namespace school_web.Admin
{
    public partial class College_Details : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
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
                        ViewState["Userid"] = Session["Admin"].ToString();
                        string pagename_current = Path.GetFileName(Request.Path);
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        My.bind_ddl_select(ddl_state, "select State from dbo.[StateList] order by State asc");
                        BindDetails();
                    }
                }
                catch (Exception ex)
                {
                    My.submitException(ex, "College_Details");
                }
            }
        }

        private void BindDetails()
        {
            DataTable dt = mycode.FillData("Select * from Firm_Details  ");
            if (dt.Rows.Count == 0)
            {
                Image1.Visible = false;

            }
            else
            {
                txt_Colleg_name.Text = dt.Rows[0]["firm_name"].ToString();
                txt_address1.Text = dt.Rows[0]["address1"].ToString();

                try
                {
                    if (dt.Rows[0]["Is_2nd_address"].ToString() == "True")
                    {
                        ddl_is_2d_branch.Text = "Yes";
                    }
                }
                catch (Exception ex)
                {
                }

                txt_address2.Text = dt.Rows[0]["address2"].ToString();
                txt_contactno.Text = dt.Rows[0]["contact_no"].ToString();
                txt_emailid.Text = dt.Rows[0]["email"].ToString();
                ddl_affilliated.Text = dt.Rows[0]["affiliated_type"].ToString();
                txt_emailid.Text = dt.Rows[0]["email"].ToString();
                txt_affliaction_no.Text = dt.Rows[0]["Affiliation"].ToString();
                txt_college_no.Text = dt.Rows[0]["school_no"].ToString();
                txt_wbesite.Text = dt.Rows[0]["website"].ToString();
                txt_gstno.Text = dt.Rows[0]["gstin"].ToString();
                txt_pan_no.Text = dt.Rows[0]["pan_no"].ToString();
                txt_tan_no.Text = dt.Rows[0]["tan_no"].ToString();
                txt_estad_no.Text = dt.Rows[0]["estd"].ToString();
                Image1.Visible = false;
                if (dt.Rows[0]["logo"].ToString() == "")
                {
                    lbl_img_path.Text = "";
                }
                else
                {
                    Image1.Visible = true;
                    Image1.ImageUrl = dt.Rows[0]["logo"].ToString();
                    lbl_img_path.Text = dt.Rows[0]["logo"].ToString();
                }
                txt_prifix.Text = dt.Rows[0]["Slip_Prefix"].ToString();
                txt_readmission_fee_slip.Text = dt.Rows[0]["Readmison_Prefix"].ToString();
                txt_monthly_reciptslip.Text = dt.Rows[0]["Monthly_Slip_Prefix"].ToString();

                txt_trustname.Text = dt.Rows[0]["Trust_name"].ToString();
                txt_trust_pan_no.Text = dt.Rows[0]["Trust_Pan_no"].ToString();
                txt_mobile_no_trust.Text = dt.Rows[0]["Trust_Mobile_No"].ToString();

                try
                {
                    txt_Character_certificate_prefix.Text = dt.Rows[0]["Character_certificate_prefix"].ToString();
                    txt_Transfer_certificate_prefix.Text = dt.Rows[0]["Transfer_certificate_prefix"].ToString();

                    txt_Leaving_certificate_prefix.Text = dt.Rows[0]["Leaving_certificate_prefix"].ToString();
                }
                catch
                {
                }
                txt_online_reg_prifix.Text = dt.Rows[0]["Online_reg_prefix"].ToString();

                try
                {
                    ddl_state.Text = dt.Rows[0]["School_state"].ToString();
                }
                catch
                {
                }

                try
                {
                    ddl_udise_no.Text = dt.Rows[0]["Is_udise"].ToString();
                }
                catch
                {
                }

                try
                {
                    txt_udise_no.Text = dt.Rows[0]["Udise_no"].ToString();
                }
                catch
                {
                }
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
        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            if (ViewState["Is_add"].ToString() == "1")
            {
                SqlCommand cmd;
                try
                {
                    if (txt_Colleg_name.Text == "")
                    {
                        txt_Colleg_name.Focus();
                        Alertme("Please enter college name.", "warning");
                        return;
                    }
                    else
                    {
                        bool image = false;
                        string imgpath = "";
                        string thumbnail = "";
                        DataTable dt = mycode.FillData("Select * from Firm_Details  ");
                        if (dt.Rows.Count == 0)
                        {
                            if (FileUpload1.HasFile)
                            {
                                decimal size = Math.Round(((decimal)FileUpload1.PostedFile.ContentLength / (decimal)1024), 2);
                                if (size > 500)
                                {
                                    Alertme("Image size must be less than or equal to 500kb. Your selected image size is " + size, "warning");
                                    return;
                                }

                                imgpath = upload_image();
                                if (imgpath == "")
                                {
                                    image = false;
                                }
                                else
                                {
                                    string originalPath2 = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, ""); string[] New_originalPath1 = originalPath2.Split('?'); string originalPath1 = New_originalPath1[0].ToString();
                                    thumbnail = originalPath1 + "/Master_Img/thumbnail_" + Session["WorkingImage1"].ToString();
                                    image = true;
                                }
                            }
                            else
                            {
                                image = true;
                                string originalPath2 = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, ""); string[] New_originalPath1 = originalPath2.Split('?'); string originalPath1 = New_originalPath1[0].ToString();
                                imgpath = originalPath1 + "/Master_Img/noimage1.png";
                                thumbnail = originalPath1 + "/Master_Img/noimage1.png";
                            }

                            if (image == true)
                            {
                                string query = "INSERT INTO Firm_Details (firm_name,address1,address2,gstin,contact_no,firm_id,Affiliation,logo,school_no,affiliated_type,email,website,font,Certificate_tagline1,Certificate_tagline2,State,State_code,pan_no,tan_no,estd,Slip_Prefix,Trust_name,Trust_Pan_no,Trust_Mobile_No,Readmison_Prefix,Monthly_Slip_Prefix,Character_certificate_prefix,Transfer_certificate_prefix,Leaving_certificate_prefix,Online_reg_prefix,School_state,Is_2nd_address,Is_udise,Udise_no) values (@firm_name,@address1,@address2,@gstin,@contact_no,@firm_id,@Affiliation,@logo,@school_no,@affiliated_type,@email,@website,@font,@Certificate_tagline1,@Certificate_tagline2,@State,@State_code,@pan_no,@tan_no,@estd,@Slip_Prefix,@Trust_name,@Trust_Pan_no,@Trust_Mobile_No,@Readmison_Prefix,@Monthly_Slip_Prefix,@Character_certificate_prefix,@Transfer_certificate_prefix,@Leaving_certificate_prefix,@Online_reg_prefix,@School_state,@Is_2nd_address,@Is_udise,@Udise_no)";
                                cmd = new SqlCommand(query);
                                cmd.Parameters.AddWithValue("@firm_name", txt_Colleg_name.Text.Trim());
                                cmd.Parameters.AddWithValue("@address1", txt_address1.Text);
                                if (ddl_is_2d_branch.Text == "Yes")
                                {
                                    cmd.Parameters.AddWithValue("@Is_2nd_address", "1");
                                    cmd.Parameters.AddWithValue("@address2", txt_address2.Text);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@Is_2nd_address", "0");
                                    cmd.Parameters.AddWithValue("@address2", "");
                                }
                                cmd.Parameters.AddWithValue("@gstin", txt_gstno.Text);
                                cmd.Parameters.AddWithValue("@contact_no", txt_contactno.Text);
                                cmd.Parameters.AddWithValue("@firm_id", "1");

                                cmd.Parameters.AddWithValue("@logo", imgpath);
                                cmd.Parameters.AddWithValue("@school_no", txt_college_no.Text);
                                cmd.Parameters.AddWithValue("@affiliated_type", ddl_affilliated.Text);
                                if (ddl_affilliated.Text == "Yes")
                                {
                                    cmd.Parameters.AddWithValue("@Affiliation", txt_affliaction_no.Text);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@Affiliation", "NA");
                                }
                                cmd.Parameters.AddWithValue("@email", txt_emailid.Text);
                                cmd.Parameters.AddWithValue("@website", txt_wbesite.Text);
                                cmd.Parameters.AddWithValue("@font", "");
                                cmd.Parameters.AddWithValue("@Certificate_tagline1", "");
                                cmd.Parameters.AddWithValue("@Certificate_tagline2", "");
                                cmd.Parameters.AddWithValue("@State", "");
                                cmd.Parameters.AddWithValue("@State_code", "");
                                cmd.Parameters.AddWithValue("@pan_no", txt_pan_no.Text);
                                cmd.Parameters.AddWithValue("@tan_no", txt_tan_no.Text);
                                cmd.Parameters.AddWithValue("@estd", txt_estad_no.Text);
                                cmd.Parameters.AddWithValue("@Slip_Prefix", txt_prifix.Text);

                                cmd.Parameters.AddWithValue("@Trust_name", txt_trustname.Text);
                                cmd.Parameters.AddWithValue("@Trust_Pan_no", txt_trust_pan_no.Text);
                                cmd.Parameters.AddWithValue("@Trust_Mobile_No", txt_mobile_no_trust.Text);
                                cmd.Parameters.AddWithValue("@Readmison_Prefix", txt_readmission_fee_slip.Text);
                                cmd.Parameters.AddWithValue("@Monthly_Slip_Prefix", txt_monthly_reciptslip.Text);

                                cmd.Parameters.AddWithValue("@Character_certificate_prefix", txt_Character_certificate_prefix.Text);
                                cmd.Parameters.AddWithValue("@Transfer_certificate_prefix", txt_Transfer_certificate_prefix.Text);
                                cmd.Parameters.AddWithValue("@Leaving_certificate_prefix", txt_Leaving_certificate_prefix.Text);
                                cmd.Parameters.AddWithValue("@Online_reg_prefix", txt_online_reg_prifix.Text);
                                cmd.Parameters.AddWithValue("@School_state", ddl_state.Text);

                                cmd.Parameters.AddWithValue("@Is_udise", ddl_udise_no.Text);
                                cmd.Parameters.AddWithValue("@Udise_no", txt_udise_no.Text);

                                if (My.InsertUpdateData(cmd))
                                {
                                    Alertme("School details has been save Successfully.", "success");
                                    BindDetails();
                                    btn_Submit.Text = "Save";
                                }
                            }
                            else
                            {
                                Alertme("Please choose logo.", "warning");
                            }
                        }
                        else
                        {
                            string id = dt.Rows[0]["id"].ToString();
                            if (FileUpload1.HasFile)
                            {
                                decimal size = Math.Round(((decimal)FileUpload1.PostedFile.ContentLength / (decimal)1024), 2);
                                if (size > 500)
                                {
                                    Alertme("Image size must be less than or equal to 500kb. Your selected image size is " + size, "warning");
                                    return;
                                }

                                imgpath = upload_image();
                                if (imgpath == "")
                                {
                                    image = false;
                                }
                                else
                                {
                                    string originalPath2 = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, ""); string[] New_originalPath1 = originalPath2.Split('?'); string originalPath1 = New_originalPath1[0].ToString();
                                    thumbnail = originalPath1 + "/Master_Img/thumbnail_" + Session["WorkingImage1"].ToString();
                                    image = true;
                                }
                            }
                            else
                            {
                                image = true;
                                imgpath = lbl_img_path.Text;

                            }

                            if (image == true)
                            {
                                string query = "Update Firm_Details set firm_name=@firm_name,address1=@address1,address2=@address2,gstin=@gstin,contact_no=@contact_no,Affiliation=@Affiliation,logo=@logo,school_no=@school_no,affiliated_type=@affiliated_type,email=@email,website=@website,font=@font,Certificate_tagline1=@Certificate_tagline1,Certificate_tagline2=@Certificate_tagline2,pan_no=@pan_no,tan_no=@tan_no,estd=@estd,Slip_Prefix=@Slip_Prefix,Trust_name=@Trust_name,Trust_Pan_no=@Trust_Pan_no,Trust_Mobile_No=@Trust_Mobile_No,Monthly_Slip_Prefix=@Monthly_Slip_Prefix,Readmison_Prefix=@Readmison_Prefix,Character_certificate_prefix=@Character_certificate_prefix,Transfer_certificate_prefix=@Transfer_certificate_prefix,Leaving_certificate_prefix=@Leaving_certificate_prefix,Online_reg_prefix=@Online_reg_prefix,School_state=@School_state,Is_2nd_address=@Is_2nd_address,Is_udise=@Is_udise,Udise_no=@Udise_no where id = @id";
                                cmd = new SqlCommand(query);
                                cmd.Parameters.AddWithValue("@firm_name", txt_Colleg_name.Text.Trim());
                                cmd.Parameters.AddWithValue("@address1", txt_address1.Text);

                                if (ddl_is_2d_branch.Text == "Yes")
                                {
                                    cmd.Parameters.AddWithValue("@Is_2nd_address", "1");
                                    cmd.Parameters.AddWithValue("@address2", txt_address2.Text);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@Is_2nd_address", "0");
                                    cmd.Parameters.AddWithValue("@address2", "");
                                }

                                cmd.Parameters.AddWithValue("@gstin", txt_gstno.Text);
                                cmd.Parameters.AddWithValue("@contact_no", txt_contactno.Text);
                                cmd.Parameters.AddWithValue("@id", id);

                                cmd.Parameters.AddWithValue("@logo", imgpath);
                                cmd.Parameters.AddWithValue("@school_no", txt_college_no.Text);
                                cmd.Parameters.AddWithValue("@affiliated_type", ddl_affilliated.Text);

                                if (ddl_affilliated.Text == "Yes")
                                {
                                    cmd.Parameters.AddWithValue("@Affiliation", txt_affliaction_no.Text);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@Affiliation", "NA");
                                }

                                cmd.Parameters.AddWithValue("@email", txt_emailid.Text);
                                cmd.Parameters.AddWithValue("@website", txt_wbesite.Text);
                                cmd.Parameters.AddWithValue("@font", "");
                                cmd.Parameters.AddWithValue("@Certificate_tagline1", "");
                                cmd.Parameters.AddWithValue("@Certificate_tagline2", "");
                                
                                cmd.Parameters.AddWithValue("@pan_no", txt_pan_no.Text);
                                cmd.Parameters.AddWithValue("@tan_no", txt_tan_no.Text);
                                cmd.Parameters.AddWithValue("@estd", txt_estad_no.Text);
                                cmd.Parameters.AddWithValue("@Slip_Prefix", txt_prifix.Text);//admission slip


                                cmd.Parameters.AddWithValue("@Trust_name", txt_trustname.Text);
                                cmd.Parameters.AddWithValue("@Trust_Pan_no", txt_trust_pan_no.Text);

                                cmd.Parameters.AddWithValue("@Trust_Mobile_No", txt_mobile_no_trust.Text);

                                cmd.Parameters.AddWithValue("@Readmison_Prefix", txt_readmission_fee_slip.Text);//readmission feee
                                cmd.Parameters.AddWithValue("@Monthly_Slip_Prefix", txt_monthly_reciptslip.Text);//monthly fee lana
                                cmd.Parameters.AddWithValue("@Character_certificate_prefix", txt_Character_certificate_prefix.Text);
                                cmd.Parameters.AddWithValue("@Transfer_certificate_prefix", txt_Transfer_certificate_prefix.Text);
                                cmd.Parameters.AddWithValue("@Leaving_certificate_prefix", txt_Leaving_certificate_prefix.Text);
                                cmd.Parameters.AddWithValue("@Online_reg_prefix", txt_online_reg_prifix.Text);
                                cmd.Parameters.AddWithValue("@School_state", ddl_state.Text);
                                cmd.Parameters.AddWithValue("@Is_udise", ddl_udise_no.Text);
                                cmd.Parameters.AddWithValue("@Udise_no", txt_udise_no.Text);
                                if (My.InsertUpdateData(cmd))
                                {
                                    Alertme("School details has been updated Successfully.", "success");
                                    BindDetails();
                                    btn_Submit.Text = "Save";

                                    try
                                    {
                                        string msg =  "School Details update by " + ViewState["Userid"].ToString() + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MM/yyyy hh:mm:ss tt");
                                        mycode.insert_data_logfile(ViewState["Userid"].ToString(), "1", msg, ViewState["branchid"].ToString());

                                    }
                                    catch
                                    {

                                    }


                                }
                            }
                            else
                            {
                                Alertme("Please choose logo", "warning");
                            }
                        }
                    }
                }
                catch
                {
                }
            }
            else
            {
                Alertme("SORRY! You have not permission for this work.", "warning");
            }
        }

        private string upload_image()
        {
            string idate = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMdd");
            string time = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("hhmmss");
            string dbfilepath = "";
            Boolean FileOK = false;
            Boolean FileSaved = false;

            int k = 0;
            if (FileUpload1.HasFile)
            {
                if (FileUpload1.FileBytes.Length < 1000000)
                {
                    Session["WorkingImage"] = FileUpload1.FileName;
                    string FileExtension = Path.GetExtension(Session["WorkingImage"].ToString()).ToLower();
                    string[] allowedExtensions = { ".png", ".jpeg", ".jpg", ".gif" };
                    Session["WorkingImage1"] = idate + time + FileExtension;
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
                    Alertme("Please reduce image size (Max 500KB).", "warning");
                    return "";
                }
            }
            else
            {
                Alertme("Please choose image.", "warning");
            }
            if (FileOK)
            {
                try
                {
                    string path = (Server.MapPath("../Master_Img")).ToString();
                    FileUpload1.SaveAs(path + "/" + Session["WorkingImage1"]);
                    FileSaved = true;
                }
                catch (Exception ex)
                {
                    FileSaved = false;
                    Alertme("Folder permission issue.", "warning");
                }
            }
            else
            {
                Alertme("Please select jpg or png image.", "warning");
            }
            if (FileSaved)
            {
                string originalPath2 = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, ""); string[] New_originalPath1 = originalPath2.Split('?'); string originalPath1 = New_originalPath1[0].ToString();
                string fileName = Path.GetFileName(Session["WorkingImage1"].ToString());
                dbfilepath = originalPath1 + "/Master_Img/" + fileName;
            }
            return dbfilepath;
        }
    }
}