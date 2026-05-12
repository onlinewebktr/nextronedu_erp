using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class Add_Vehicle : System.Web.UI.Page
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
                        ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        if (Request.QueryString["transportid"] != null)
                        {
                            HdID.Value = Request.QueryString["transportid"];
                            btn_Submit.Text = "Update";
                            btn_cancel.Visible = true;

                            Bind_edit_data();
                            Bind_documnet();
                        }
                        else
                        {
                            btn_Submit.Text = "Save";
                            btn_cancel.Visible = true;

                            HdID.Value = My.auto_serialS("group_id");
                            Bind_documnet();
                        }


                    }
                }
                catch (Exception ex)
                {
                    My.submitException(ex, "Add_Vehicle");
                }
            }
        }

        private void Bind_edit_data()
        {
            DataTable dt1 = My.dataTable("select * from Transport_Master where transport_id='" + HdID.Value + "'  ");
            if (dt1.Rows.Count == 0)
            {

            }
            else
            {

                txt_Vehicle_name.Text = dt1.Rows[0]["transport_name"].ToString();
                txt_bus_no_vachileno.Text = dt1.Rows[0]["Bus_no"].ToString();
                txt_Vehicle_Owner_Name.Text = dt1.Rows[0]["Bus_owner_name"].ToString();
                txt_Vehicle_Owner_mobile_no.Text = dt1.Rows[0]["Bus_owner_mobile_no"].ToString();

                txt_Vehicle_drivername.Text = dt1.Rows[0]["Bus_driver_name"].ToString();

                txt_driver_mobile_no.Text = dt1.Rows[0]["Bus_driver_mobileno"].ToString();
                txt_no_seat.Text = dt1.Rows[0]["Bus_no_sheet"].ToString();
                ddl_Vehicle_type.Text = dt1.Rows[0]["Bus_type"].ToString();
                txt_vehicle_rute.Text = dt1.Rows[0]["transport_name"].ToString();
                ddl_Transport.Text = dt1.Rows[0]["Own_Transport"].ToString();
                //txt_Vehicle_Registration.Text = dt1.Rows[0]["Vehicle_Registration_No"].ToString();
                txt_Vehicle_Registration_date.Text = dt1.Rows[0]["Vehicle_Registration_No_Expiry_Date"].ToString();
                txt_vechile_insurance_expirydate.Text = dt1.Rows[0]["Vehicle_Insurance_Expiry_Date"].ToString();
                txt_Pollutionexpirydate.Text = dt1.Rows[0]["Pollution_Expiry_Date"].ToString();
                txt_body_Fitness_Expiry.Text = dt1.Rows[0]["Body_Fitness_Expiry_Date"].ToString();
                txt_driver_licence.Text = dt1.Rows[0]["Driver_licence_no"].ToString();
                txt_driver_licence_expiry.Text = dt1.Rows[0]["Driver_licence_expiry_Date"].ToString();
                ddl_Vehicle_Warden.Text = dt1.Rows[0]["Vehicle_Warden"].ToString();
                txt_Vehicle_Warden_name.Text = dt1.Rows[0]["Warden_Name"].ToString();
                txt_Warden_mobile_no.Text = dt1.Rows[0]["Warden_Mobile_No"].ToString();
                txt_Warden_aadhar_no.Text = dt1.Rows[0]["Warden_Addhar_No"].ToString();
                txt_Warden_address.Text = dt1.Rows[0]["Warden_Address"].ToString();

                txt_Registration_Expiry_date.Text= dt1.Rows[0]["Registration_Expiry_date"].ToString();
                if (dt1.Rows[0]["Vehicle_Warden"].ToString() == "Yes")
                {
                    Warden1.Visible = true;

                }
                else
                {
                    Warden1.Visible = false;

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
        private void Bind_documnet()
        {
            DataTable dt = mycode.FillData("select * from Transport_Vehicle_Document_Master order by Doc_id asc");
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no document list exist", "warning");
                grd_doc.DataSource = null;
                grd_doc.DataBind();

            }
            else
            {

                grd_doc.DataSource = dt;
                grd_doc.DataBind();
            }

        }


        #region uplode document grid

        protected void grd_doc_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HtmlAnchor lnkVoucher = e.Row.FindControl("a1") as HtmlAnchor;
                Label lbl_Description_ID_No = (Label)e.Row.FindControl("lbl_Description_ID_No");
                Button btn_delete = (Button)e.Row.FindControl("btn_delete");
                string file_avl = find_file_Available_data(lbl_Description_ID_No.Text);
                if (file_avl != "")
                {
                    lnkVoucher.Visible = true;
                    lnkVoucher.HRef = file_avl;
                    btn_delete.Visible = true;
                }
                else
                {
                    lnkVoucher.Visible = false;
                    btn_delete.Visible = false;
                }


            }
        }
        protected void btn_delete_Click(object sender, EventArgs e)
        {
            try
            {
                Button imgbtn = (Button)sender;
                GridViewRow gvr = (GridViewRow)imgbtn.NamingContainer;
                Label lbl_Description_ID_No = (Label)gvr.FindControl("lbl_Description_ID_No");
                mycode.executequery("delete from Transport_Vehicle_Document where Document_id='" + lbl_Description_ID_No.Text + "' and transport_id='" + HdID.Value + "' ");
                Bind_documnet();
                Alertme("Document has been deleted successfully", "success");
            }
            catch
            {

            }
        }
        private string find_file_Available_data(string Description_ID)
        {
            SqlCommand cmd;
            string strQuery = "Select  * from Transport_Vehicle_Document where transport_id=@transport_id   and Document_id=@Document_id";
            cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@transport_id", HdID.Value);
            cmd.Parameters.AddWithValue("@Document_id", Description_ID);
            DataTable dt = mycode.GetData(cmd);
            int count = dt.Rows.Count;
            if (count == 0)
            {
                return "";
            }
            else
            {
                return dt.Rows[0]["Doument_path"].ToString();
            }
        }





        protected void btn_upload_Click1(object sender, ImageClickEventArgs e)
        {

            try
            {
                DateTime ddt = DateTime.UtcNow.AddHours(5).AddMinutes(30);

                string idate1 = ddt.ToString("yyyyMMdd");
                string time1 = ddt.ToString("hhmmss");

                ImageButton imgbtn = (ImageButton)sender;
                GridViewRow gvr = (GridViewRow)imgbtn.NamingContainer;
                Label lbl_Description_ID_No = (Label)gvr.FindControl("lbl_Description_ID_No");
                Label lbl_document_name = (Label)gvr.FindControl("lbl_document_name");

                FileUpload fileUpload = (FileUpload)gvr.FindControl("fu_file1");
                string docname = HdID.Value + "Doc" + idate1 + time1 + "_" + lbl_Description_ID_No.Text;
                string docpath = "";
                if (fileUpload.HasFile)
                {
                    if (fileUpload.FileBytes.Length < 10000000)
                    {
                        docpath = upload_doc(fileUpload, docname);
                        if (docpath == "")
                        {
                            Alertme("Please select file", "warning");


                        }
                        else
                        {
                            insert_traspost_data_Uploded_Document(lbl_Description_ID_No.Text, docpath, lbl_document_name.Text);
                            Bind_documnet();
                        }


                    }
                    else
                    {
                        Alertme("Please reduce or compress size of file.(Max 300 KB)", "warning");



                    }
                }
                else
                {
                    Alertme("Please select file", "warning");

                }
            }
            catch (Exception ex)
            {

            }

        }

        private void insert_traspost_data_Uploded_Document(string docid, string docpath, string document_name)
        {
            DateTime dtm = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string date = dtm.ToString("dd/MM/yyyy");
            string idate = dtm.ToString("yyyyMMdd");
            string time = dtm.ToString("hh:mm:ss tt");
            SqlCommand cmd;
            string strQuery;
            strQuery = "select * from Transport_Vehicle_Document where transport_id='" + HdID.Value + "' and Document_id='" + docid + "'";
            cmd = new SqlCommand(strQuery);
            DataTable dt = mycode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                strQuery = "insert into Transport_Vehicle_Document(transport_id,Document_id,Doument_Name,Doument_path,Date,Created_by) values (@transport_id,@Document_id,@Doument_Name,@Doument_path,@Date,@Created_by);insert into Transport_Vehicle_Document_History(transport_id,Document_id,Doument_Name,Doument_path,Date,Created_by) values (@transport_id,@Document_id,@Doument_Name,@Doument_path,@Date,@Created_by)";
                cmd = new SqlCommand(strQuery);
                cmd.Parameters.AddWithValue("@transport_id", HdID.Value);
                cmd.Parameters.AddWithValue("@Document_id", docid);
                cmd.Parameters.AddWithValue("@Doument_Name", docid);
                cmd.Parameters.AddWithValue("@Doument_path", docpath);
                cmd.Parameters.AddWithValue("@Date", My.getdate1());
                cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());

                if (My.InsertUpdateData(cmd))
                {



                    Alertme("File uploaded successfully", "success");

                }
            }
            else
            {
                strQuery = "Update Transport_Vehicle_Document set Doument_path=@Doument_path  where transport_id=@transport_id and Document_id=@Document_id, ";
                cmd = new SqlCommand(strQuery);
                cmd.Parameters.AddWithValue("@transport_id", HdID.Value);
                cmd.Parameters.AddWithValue("@Document_id", docid);
                cmd.Parameters.AddWithValue("@Doument_path", docpath);
                if (My.InsertUpdateData(cmd))
                {
                    Alertme("File uploaded successfully", "success");


                    strQuery = "insert into Transport_Vehicle_Document_History(transport_id,Document_id,Doument_Name,Doument_path,Date,Created_by) values (@transport_id,@Document_id,@Doument_Name,@Doument_path,@Date,@Created_by)";
                    cmd = new SqlCommand(strQuery);
                    cmd.Parameters.AddWithValue("@transport_id", HdID.Value);
                    cmd.Parameters.AddWithValue("@Document_id", docid);
                    cmd.Parameters.AddWithValue("@Doument_Name", docid);
                    cmd.Parameters.AddWithValue("@Doument_path", docpath);
                    cmd.Parameters.AddWithValue("@Date", My.getdate1());
                    cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());

                    if (My.InsertUpdateData(cmd))
                    {



                        Alertme("File uploaded successfully", "success");

                    }

                }

            }
        }

        private string upload_doc(FileUpload File_uploadcont, string docname)
        {
            //return mycode.upload_doc_web(fileUpload, docname);
            Boolean FileOK = false;
            Boolean FileSaved = false;
            int k = 0;
            string FileName1 = "";
            string file_path = "";
            if (File_uploadcont.PostedFile.ContentLength > 0)
            {
                if (File_uploadcont.FileBytes.Length <= 10000000)
                {
                    string extension = Path.GetExtension(File_uploadcont.FileName.ToLower()); //System.IO.Path.GetExtension(File_uploadcont.PostedFile.FileName);
                    FileName1 = docname + extension;
                    string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".PDF", ".pdf", ".doc", ".docx", ".DOC", ".DOCX" };
                    for (int i = 0; i < allowedExtensions.Length; i++)
                    {
                        k++;
                        if (extension == allowedExtensions[i])
                        {
                            FileOK = true;
                            break;
                        }
                        else
                        {
                        }
                    }
                }
                if (FileOK)
                {
                    HttpPostedFile postedf = File_uploadcont.PostedFile;
                    postedf.SaveAs(MapPath("../Master_Img/Transport_Doc/" + FileName1));
                    FileSaved = true;
                }
                else
                {
                }
                if (FileSaved)
                {
                    String originalPath = My.url();
                    string fileName = Path.GetFileName(FileName1);
                    file_path = originalPath + "Master_Img/Transport_Doc/" + fileName;
                }
            }

            return file_path;
        }



        #endregion

        #region submit data
        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            if (ddl_Transport.Text == "Select")
            {
                Alertme("Please select Own transport/ third party transport", "warning");
            }
            else if (ddl_Vehicle_type.Text == "Select")
            {
                Alertme("Please select vehicle type ", "warning");
            }
            else if (ddl_Vehicle_Warden.Text == "Select")
            {
                Alertme("Please select vehicle warden", "warning");
            }
            else
            {
                save_data_table();
            }
        }

        private void save_data_table()
        {
            if (btn_Submit.Text == "Save")
            {

                DataTable dt1 = My.dataTable("select * from Transport_Master where transport_name='" + txt_Vehicle_name.Text + "'   ");
                if (dt1.Rows.Count == 0)
                {
                    SqlCommand cmd;
                    string query = "INSERT INTO Transport_Master (transport_name,transport_id,Bus_no,Bus_owner_name,Bus_owner_mobile_no,Bus_driver_name,Bus_driver_mobileno,Bus_no_sheet,Bus_type,Created_by,Created_date,Vehicle_Route,Own_Transport,Vehicle_Registration_No,Vehicle_Registration_No_Expiry_Date,Vehicle_Insurance_Expiry_Date,Pollution_Expiry_Date,Body_Fitness_Expiry_Date,Driver_licence_no,Driver_licence_expiry_Date,Vehicle_Warden,Warden_Name,Warden_Mobile_No,Warden_Addhar_No,Warden_Address,Registration_Expiry_date) values (@transport_name,@transport_id,@Bus_no,@Bus_owner_name,@Bus_owner_mobile_no,@Bus_driver_name,@Bus_driver_mobileno,@Bus_no_sheet,@Bus_type,@Created_by,@Created_date,@Vehicle_Route,@Own_Transport,@Vehicle_Registration_No,@Vehicle_Registration_No_Expiry_Date,@Vehicle_Insurance_Expiry_Date,@Pollution_Expiry_Date,@Body_Fitness_Expiry_Date,@Driver_licence_no,@Driver_licence_expiry_Date,@Vehicle_Warden,@Warden_Name,@Warden_Mobile_No,@Warden_Addhar_No,@Warden_Address,@Registration_Expiry_date)";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@transport_name", txt_Vehicle_name.Text);
                    cmd.Parameters.AddWithValue("@transport_id", HdID.Value);
                    cmd.Parameters.AddWithValue("@Bus_no", txt_bus_no_vachileno.Text);
                    cmd.Parameters.AddWithValue("@Bus_owner_name", txt_Vehicle_Owner_Name.Text);
                    cmd.Parameters.AddWithValue("@Bus_owner_mobile_no", txt_Vehicle_Owner_mobile_no.Text);
                    cmd.Parameters.AddWithValue("@Bus_driver_name", txt_Vehicle_drivername.Text);
                    cmd.Parameters.AddWithValue("@Bus_driver_mobileno", txt_driver_mobile_no.Text);
                    cmd.Parameters.AddWithValue("@Bus_no_sheet", txt_no_seat.Text);
                    cmd.Parameters.AddWithValue("@Bus_type", ddl_Vehicle_type.Text);
                    cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@Created_date", My.getdate1());

                    cmd.Parameters.AddWithValue("@Vehicle_Route", "A1");
                    cmd.Parameters.AddWithValue("@Own_Transport", ddl_Transport.Text);
                    cmd.Parameters.AddWithValue("@Vehicle_Registration_No", "");
                    cmd.Parameters.AddWithValue("@Vehicle_Registration_No_Expiry_Date", txt_Vehicle_Registration_date.Text);
                    cmd.Parameters.AddWithValue("@Vehicle_Insurance_Expiry_Date", txt_vechile_insurance_expirydate.Text);
                    cmd.Parameters.AddWithValue("@Pollution_Expiry_Date", txt_Pollutionexpirydate.Text);
                    cmd.Parameters.AddWithValue("@Body_Fitness_Expiry_Date", txt_body_Fitness_Expiry.Text);
                    cmd.Parameters.AddWithValue("@Driver_licence_no", txt_driver_licence.Text);
                    cmd.Parameters.AddWithValue("@Driver_licence_expiry_Date", txt_driver_licence_expiry.Text);
                    cmd.Parameters.AddWithValue("@Vehicle_Warden", ddl_Vehicle_Warden.Text);
                    cmd.Parameters.AddWithValue("@Warden_Name", txt_Vehicle_Warden_name.Text);
                    cmd.Parameters.AddWithValue("@Warden_Mobile_No", txt_Warden_mobile_no.Text);
                    cmd.Parameters.AddWithValue("@Warden_Addhar_No", txt_Warden_aadhar_no.Text);
                    cmd.Parameters.AddWithValue("@Warden_Address", txt_Warden_address.Text);
                    cmd.Parameters.AddWithValue("@Registration_Expiry_date", txt_Registration_Expiry_date.Text);
                  
                    if (My.InsertUpdateData(cmd))
                    {

                        save_history_driver();
                        Alertme("Vehicle details has been sucessfully added", "success");
                        HdID.Value = My.auto_serialS("group_id");
                        Bind_documnet();
                        empty_data();
                    }


                }
                else
                {

                    Alertme("Your entered vehicle name name is already added", "warning");

                }
            }
            else if (btn_Submit.Text == "Update")
            {
                DataTable dt1 = My.dataTable("select * from Transport_Master where transport_name='" + txt_Vehicle_name.Text + "'   and transport_id!='" + HdID.Value + "'  ");
                if (dt1.Rows.Count == 0)
                {
                    SqlCommand cmd;
                    string query = "Update Transport_Master set transport_name=@transport_name,Bus_no=@Bus_no,Bus_owner_name=@Bus_owner_name,Bus_owner_mobile_no=@Bus_owner_mobile_no,Bus_driver_name=@Bus_driver_name,Bus_driver_mobileno=@Bus_driver_mobileno,Bus_no_sheet=@Bus_no_sheet,Bus_type=@Bus_type,Updated_by=@Updated_by,Updated_time=@Updated_time,Vehicle_Route=@Vehicle_Route,Own_Transport=@Own_Transport,Vehicle_Registration_No_Expiry_Date=@Vehicle_Registration_No_Expiry_Date,Vehicle_Insurance_Expiry_Date=@Vehicle_Insurance_Expiry_Date,Pollution_Expiry_Date=@Pollution_Expiry_Date,Body_Fitness_Expiry_Date=@Body_Fitness_Expiry_Date,Driver_licence_no=@Driver_licence_no,Driver_licence_expiry_Date=@Driver_licence_expiry_Date,Vehicle_Warden=@Vehicle_Warden,Warden_Name=@Warden_Name,Warden_Mobile_No=@Warden_Mobile_No,Warden_Addhar_No=@Warden_Addhar_No,Warden_Address=@Warden_Address,Registration_Expiry_date=@Registration_Expiry_date where transport_id=@transport_id";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@transport_name", txt_Vehicle_name.Text);
                    cmd.Parameters.AddWithValue("@transport_id", HdID.Value);
                    cmd.Parameters.AddWithValue("@Bus_no", txt_bus_no_vachileno.Text);
                    cmd.Parameters.AddWithValue("@Bus_owner_name", txt_Vehicle_Owner_Name.Text);
                    cmd.Parameters.AddWithValue("@Bus_owner_mobile_no", txt_Vehicle_Owner_mobile_no.Text);
                    cmd.Parameters.AddWithValue("@Bus_driver_name", txt_Vehicle_drivername.Text);
                    cmd.Parameters.AddWithValue("@Bus_driver_mobileno", txt_driver_mobile_no.Text);
                    cmd.Parameters.AddWithValue("@Bus_no_sheet", txt_no_seat.Text);
                    cmd.Parameters.AddWithValue("@Bus_type", ddl_Vehicle_type.Text);
                    cmd.Parameters.AddWithValue("@Updated_by", ViewState["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@Updated_time", My.getdate1());
                    cmd.Parameters.AddWithValue("@Vehicle_Route", "A1");
                    cmd.Parameters.AddWithValue("@Own_Transport", ddl_Transport.Text);
                     
                    cmd.Parameters.AddWithValue("@Vehicle_Registration_No_Expiry_Date", txt_Vehicle_Registration_date.Text);
                    cmd.Parameters.AddWithValue("@Vehicle_Insurance_Expiry_Date", txt_vechile_insurance_expirydate.Text);
                    cmd.Parameters.AddWithValue("@Pollution_Expiry_Date", txt_Pollutionexpirydate.Text);
                    cmd.Parameters.AddWithValue("@Body_Fitness_Expiry_Date", txt_body_Fitness_Expiry.Text);
                    cmd.Parameters.AddWithValue("@Driver_licence_no", txt_driver_licence.Text);
                    cmd.Parameters.AddWithValue("@Driver_licence_expiry_Date", txt_driver_licence_expiry.Text);
                    cmd.Parameters.AddWithValue("@Vehicle_Warden", ddl_Vehicle_Warden.Text);
                    cmd.Parameters.AddWithValue("@Warden_Name", txt_Vehicle_Warden_name.Text);
                    cmd.Parameters.AddWithValue("@Warden_Mobile_No", txt_Warden_mobile_no.Text);
                    cmd.Parameters.AddWithValue("@Warden_Addhar_No", txt_Warden_aadhar_no.Text);
                    cmd.Parameters.AddWithValue("@Warden_Address", txt_Warden_address.Text);
                    cmd.Parameters.AddWithValue("@Registration_Expiry_date", txt_Registration_Expiry_date.Text);
                    if (My.InsertUpdateData(cmd))
                    {
                        Alertme("Vehicle details has been sucessfully updated", "success");

                    }
                }
                else
                {
                    Alertme("Your entered vehicle name is already added", "warning");
                }
            }
            else
            {

            }
        }

        private void save_history_driver()
        {
            DataTable dt1 = My.dataTable("select * from Transport_Driver_History where transport_id='" + HdID.Value + "' and Driver_licence_no='" + txt_driver_licence.Text + "'  ");
            if (dt1.Rows.Count == 0)
            {

                SqlCommand cmd;
                string query = "INSERT INTO Transport_Driver_History (transport_id,Bus_driver_name,Bus_driver_mobileno,Driver_licence_no,Driver_licence_expiry_Date,Created_by,Created_date) values (@transport_id,@Bus_driver_name,@Bus_driver_mobileno,@Driver_licence_no,@Driver_licence_expiry_Date,@Created_by,@Created_date)"; ;
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@transport_id", HdID.Value);
                cmd.Parameters.AddWithValue("@Bus_driver_name", txt_Vehicle_drivername.Text);
                cmd.Parameters.AddWithValue("@Bus_driver_mobileno", txt_driver_mobile_no.Text);
                cmd.Parameters.AddWithValue("@Driver_licence_no", txt_driver_licence.Text);
                cmd.Parameters.AddWithValue("@Driver_licence_expiry_Date", txt_driver_licence_expiry.Text);
                cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Created_date", My.getdate1());
                if (My.InsertUpdateData(cmd))
                {
                }

            }
            else
            {
                SqlCommand cmd;
                string query = "Update Transport_Driver_History set transport_id=@transport_id,Bus_driver_name=@Bus_driver_name,Bus_driver_mobileno=@Bus_driver_mobileno,Driver_licence_no=@Driver_licence_no,Driver_licence_expiry_Date=@Driver_licence_expiry_Date,Created_by=@Created_by,Created_date=@Created_date where transport_id = @transport_id";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@transport_id", HdID.Value);
                cmd.Parameters.AddWithValue("@Bus_driver_name", txt_Vehicle_drivername.Text);
                cmd.Parameters.AddWithValue("@Bus_driver_mobileno", txt_driver_mobile_no.Text);
                cmd.Parameters.AddWithValue("@Driver_licence_no", txt_driver_licence.Text);
                cmd.Parameters.AddWithValue("@Driver_licence_expiry_Date", txt_driver_licence_expiry.Text);
                cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Created_date", My.getdate1());
                if (My.InsertUpdateData(cmd))
                {

                }
            }
        }
        private void empty_data()
        {
            txt_Vehicle_name.Text = "";
            txt_bus_no_vachileno.Text = "";
            txt_Vehicle_Owner_Name.Text = "";
            txt_Vehicle_Owner_mobile_no.Text = "";
            txt_Vehicle_drivername.Text = "";
            txt_driver_mobile_no.Text = "";
            txt_no_seat.Text = "";
            ddl_Vehicle_type.Text = "Select";

            ddl_Transport.Text = "Select";
            
            txt_Vehicle_Registration_date.Text = "";
            txt_vechile_insurance_expirydate.Text = "";
            txt_Pollutionexpirydate.Text = "";
            txt_body_Fitness_Expiry.Text = "";
            txt_driver_licence.Text = "";
            txt_driver_licence_expiry.Text = "";
            ddl_Vehicle_Warden.Text = "Select";
            txt_Vehicle_Warden_name.Text = "";
            txt_Warden_mobile_no.Text = "";
            txt_Warden_aadhar_no.Text = "";
            txt_Warden_address.Text = "";
            txt_Registration_Expiry_date.Text = "";
        }
        #endregion

        protected void ddl_Vehicle_Warden_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Vehicle_Warden.Text == "Select")
            {
                Alertme("Please select vehicle warden", "warning");
            }
            else
            {
                if (ddl_Vehicle_Warden.Text == "Yes")
                {
                    Warden1.Visible = true;

                }
                else
                {
                    Warden1.Visible = false;

                }

            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Add_Vehicle.aspx", false);
        }
    }
}