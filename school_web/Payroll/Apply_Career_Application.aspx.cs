using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace school_web.Payroll
{
    public partial class Apply_Career_Application : System.Web.UI.Page
    {
        string scrpt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["sfhsdfghjdncjszhfyshf"] != null)
            {
                if (!IsPostBack)
                {
                    ViewState["Applyid"] = Request.QueryString["sfhsdfghjdncjszhfyshf"];// applyid id id
                    fetch_company_name();
                    ddl_salution_name.bind("select distinct Initial_name,Initial_id from HMS_Initial_Master order by Initial_id");

                    ViewState["file_passport_photo"] = "";
                    ViewState["file_upoad_Signature"] = "";

                    fatch_data();

                }

            }
            else
            {


            }
        }

        private void fatch_data()
        {
            DataTable dt = PayrollMy.dataTable("select * from dbo.[HR_Employee_Online_Apply] where  Apply_id='" + ViewState["Applyid"].ToString() + "'");
            if (dt.Rows.Count == 0)
            {

            }
            else

            {
                ViewState["sessionid"] = dt.Rows[0]["Session_id"].ToString();
                txt_position_job_for.Text = dt.Rows[0]["Apply_for"].ToString();
                ViewState["Hiring_id"] = dt.Rows[0]["Hiring_id"].ToString();
                ViewState["HiringTypeId"] = dt.Rows[0]["HiringTypeId"].ToString();
                ViewState["Session_id"] = dt.Rows[0]["Session_id"].ToString();
                ViewState["Payable_amount"] = dt.Rows[0]["Payable_amount"].ToString();
                if (dt.Rows[0]["Order_id"].ToString() == "")
                {
                    Random r = new Random(DateTime.Now.Millisecond);
                    string order_id = DateTime.UtcNow.ToString("yyMMddHHMMss") + r.Next(12346, 48749);
                    ViewState["Order_id"] = order_id;
                }
                else
                {
                    ViewState["Order_id"] = dt.Rows[0]["Order_id"].ToString();
                }

                //if (dt.Rows[0]["Apply_for"].ToString() == "Teacher")
                //{
                //    subjecttype1.Visible = true;
                //    subject_type2.Visible = true;
                //}
                //else
                //{
                //    subjecttype1.Visible = false;
                //    subject_type2.Visible = false;
                //}

                Bind_HR_Employee_Online_Apply_Work_Experiance();

                bind_number_of_seat();
            }
        }

        private void bind_number_of_seat()
        {
            DataTable dt = PayrollMy.dataTable("select * from dbo.[HR_HiringParameterSetup] where     HiringParameterId='" + ViewState["HiringTypeId"].ToString() + "'");
            if (dt.Rows.Count == 0)
            {
                ViewState["no_application"] = "0";
            }
            else

            {
                ViewState["no_application"] = dt.Rows[0]["NoOfSeat"].ToString();
            }
        }

        private void fetch_company_name()
        {
            DataTable dt = PayrollMy.dataTable("select * from Firm_Details ");
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                school_logo.Src = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_emaiid.Text = dt.Rows[0]["email"].ToString();
                lbl_website.Text = dt.Rows[0]["website"].ToString();
                lbl_contact_details.Text = dt.Rows[0]["contact_no"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();
            }
        }
        //protected void ddl_subject_type_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddl_subject_type.Text == "SELECT TYPE OF THE POST")
        //    {
        //        messagealert("Please select subject type ");

        //    }
        //    else
        //    {
        //        ddl_subject_name.bind(" select distinct Subjectname from HR_Hiring_Subject where Teacher_type='" + ddl_subject_type.Text + "' order by Subjectname");
        //    }

        //}
        public void messagealert(string msg)
        {
            lblmessage.Text = msg;
            scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }

        protected void btn_passport_photo_Click(object sender, EventArgs e)
        {
            string filepassportphoto = "";
            if (file_passport_photo.HasFile)
            {
                if (file_passport_photo.FileBytes.Length < 500000)
                {
                    filepassportphoto = upload_image(file_passport_photo, "passport_size", ViewState["Applyid"].ToString());
                    if (filepassportphoto == "")
                    {
                        btn_passport_photo.Focus();
                        messagealert("Please upload valid passport size photo");
                        file_passport_photo.Focus();
                        return;
                    }
                    else
                    {
                        file_passport_photo.BackColor = HexColor("#ffffff");
                        file_passport_photo.BorderColor = HexColor("#000000");
                        btn_passport_photo.Focus();
                        img_passport_photo.Visible = true;
                        ViewState["file_passport_photo"] = filepassportphoto;
                        img_passport_photo.ImageUrl = filepassportphoto;
                    }
                }
                else
                {
                    messagealert("Please Reduce or compress size of passport size photo max(500kb)");
                    file_passport_photo.Focus();
                }
            }
            else
            {
                messagealert("Please upload valid passport size photo.");

            }
        }

        private string upload_image(FileUpload Files, string name, string Applyid)
        {
            string dbfilePath = "";
            DateTime dt = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string date = dt.ToString("dd_MM_yyyy");
            string time = dt.ToString("hh_mm_ss");
            String filerename = name + date + time + "_" + Applyid;
            Boolean FileOK = false;
            Boolean FileSaved = false;
            int k = 0;
            Session["WorkingImage"] = Files.FileName;
            string FileExtension = Path.GetExtension(Session["WorkingImage"].ToString()).ToLower();
            Session["renamedfile"] = filerename + "PIMG1" + FileExtension;
            string[] allowedExtension = { ".png", ".jpeg", ".jpg", ".JPEG" };
            for (int i = 0; i < allowedExtension.Length; i++)
            {
                k++;
                if (FileExtension == allowedExtension[i])
                {
                    FileOK = true;
                    lblmessage.Text = "";
                    break;
                }
            }


            if (FileOK)
            {
                try
                {
                    string path = (Server.MapPath("/UploadedImage/Uploads")).ToString();
                    Files.SaveAs(path + "/" + Session["renamedfile"]);
                    FileSaved = true;
                }
                catch (Exception ex)
                {
                    FileSaved = false;
                    lblmessage.Text = "File has not save.";
                    scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
                }
            }
            else
            {

            }
            if (FileSaved)
            {
                string fileName = Path.GetFileName(Session["renamedfile"].ToString());
                dbfilePath = "/UploadedImage/Uploads/" + fileName;
            }
            return dbfilePath;
        }

        protected void btn_upload_Signature_Click(object sender, EventArgs e)
        {
            string Signature = "";
            if (file_upoad_Signature.HasFile)
            {
                if (file_upoad_Signature.FileBytes.Length < 200000)
                {
                    Signature = upload_image(file_upoad_Signature, "Signature", ViewState["Applyid"].ToString());
                    if (Signature == "")
                    {
                        messagealert("Please upload valid signature size photo");
                        img_Signature.Focus();
                        return;
                    }
                    else
                    {
                        file_upoad_Signature.BackColor = HexColor("#ffffff");
                        file_upoad_Signature.BorderColor = HexColor("#000000");

                        img_Signature.Visible = true;
                        ViewState["file_upoad_Signature"] = Signature;
                        img_Signature.ImageUrl = Signature;
                    }
                }
                else
                {
                    messagealert("Please Reduce or compress size of signature max(200kb)");
                    file_passport_photo.Focus();
                }
            }
            else
            {
                messagealert("Please upload valid Signature size photo.");

            }

            btn_upload_Signature.Focus();
        }







        #region add work experiance
        protected void btn_add_work_experiance_Click(object sender, EventArgs e)
        {
            if (txt_organization.Text == "")
            {
                messagealert("Please enter organization name");
            }
            else if (txt_from.Text == "'")
            {
                messagealert("Please select date from ");

            }
            else if (txt_to.Text == "")
            {
                messagealert("Please select date to ");
            }
            else if (txt_subject.Text == "")
            {
                messagealert("Please enter Specifications ");
            }
            //else if (txt_class_teacher.Text == "")
            //{
            //    messagealert("Please enter class teacher ");
            //}

            //else if (txt_other_responsible.Text == "")
            //{
            //    messagealert("Please enter other responsible");
            //}
            else
            {
                try
                {


                    int idate = Convert.ToDateTime(txt_from.Text).ToString("yyyyMMdd").ToInt();
                    int idate2 = Convert.ToDateTime(txt_to.Text).ToString("yyyyMMdd").ToInt();
                    var fdate = Convert.ToDateTime(txt_from.Text).ToString("dd-MMM-yyyy");
                    var tdate2 = Convert.ToDateTime(txt_to.Text).ToString("dd-MMM-yyyy");

                    if (idate > idate2)
                    {
                        messagealert("To date cannot be less than from date.");
                    }
                    else
                    {
                        DateTime start_date1 = Convert.ToDateTime(txt_from.Text);
                        DateTime end_date1 = Convert.ToDateTime(txt_to.Text);


                        int days1 = Convert.ToInt32((end_date1 - start_date1).TotalDays);

                        SqlCommand cmd;
                        string query = "INSERT INTO HR_Employee_Online_Apply_Work_Experiance (Apply_id,Organization,From_Date,To_Date,Total_Days,Cass_teacher,Other_responsible,Specifications) values (@Apply_id,@Organization,@From_Date,@To_Date,@Total_Days,@Cass_teacher,@Other_responsible,@Specifications)";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@Apply_id", ViewState["Applyid"].ToString());
                        cmd.Parameters.AddWithValue("@Organization", txt_organization.Text);
                        cmd.Parameters.AddWithValue("@From_Date", fdate);
                        cmd.Parameters.AddWithValue("@To_Date", tdate2);
                        cmd.Parameters.AddWithValue("@Total_Days", days1.ToString());
                        cmd.Parameters.AddWithValue("@Cass_teacher", "");
                        cmd.Parameters.AddWithValue("@Other_responsible", txt_other_responsible.Text);
                        cmd.Parameters.AddWithValue("@Specifications", txt_subject.Text);

                        if (cmd.ExecuteCommand(PayrollMy.con))
                        {

                            txt_organization.Text = "";
                            txt_from.Text = "";
                            txt_to.Text = "";
                            txt_other_responsible.Text = "";
                            txt_subject.Text = "";
                            Bind_HR_Employee_Online_Apply_Work_Experiance();
                        }
                    }
                }

                catch (Exception ex)
                {
                    PayrollMy.saveException(ex, "add_work_experiance");
                }
            }

            txt_organization.Focus();

        }

        private void Bind_HR_Employee_Online_Apply_Work_Experiance()
        {
            DataTable dt = PayrollMy.dataTable("  Select * from   HR_Employee_Online_Apply_Work_Experiance    where Apply_id='" + ViewState["Applyid"].ToString() + "'    ");
            if (dt.Rows.Count == 0)
            {

                grid_work_experiance.DataSource = null;
                grid_work_experiance.DataBind();
            }
            else
            {
                grid_work_experiance.DataSource = dt;
                grid_work_experiance.DataBind();
            }
        }

        protected void lnkDel_Click1(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lnk.Parent.Parent;
                Label lbl_id = (Label)row.FindControl("lbl_id");
                PayrollMy.execute("delete from HR_Employee_Online_Apply_Work_Experiance where Id=" + lbl_id.Text + " and Apply_id='" + ViewState["Applyid"].ToString() + "'");
                messagealert("deletion process has been successfully");
                Bind_HR_Employee_Online_Apply_Work_Experiance();
            }
            catch (Exception ex)
            {
                PayrollMy.saveException(ex, "delete Experiance");
            }

        }
        int total_experianceday = 0;
        protected void grid_work_experiance_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_Total_Days = (Label)e.Row.FindControl("lbl_Total_Days");


                if (lbl_Total_Days.Text != "")
                {
                    total_experianceday = total_experianceday + lbl_Total_Days.Text.ToInt();
                }


            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_Total_Days_row = (Label)e.Row.FindControl("lbl_Total_Days_row");

                lbl_Total_Days_row.Text = total_experianceday.ToString();

            }
        }
        #endregion



        protected void chk_service_bond_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_service_bond.Checked == true)
            {
                no_boundpnl.Visible = true;
                txt_no_of_years_service_bond.Text = "0";
            }
            else
            {
                txt_no_of_years_service_bond.Text = "0";
                no_boundpnl.Visible = false;
            }

        }

        protected void chk_same_check_box_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_same_check_box.Checked == true)
            {
                txt_address_pa.Text = txt_address_ca.Text;
                txt_city_pa.Text = txt_city_ca.Text;
                txt_state_pa.Text = txt_state_ca.Text;
                txt_pin_pa.Text = txt_pincode_CA.Text;
            }
            else
            {
                txt_address_pa.Text = "";
                txt_city_pa.Text = "";
                txt_state_pa.Text = "";
                txt_pin_pa.Text = "";
            }

            txt_address_pa.Focus();
        }


        #region final submit

        private Color HexColor(string hex)
        {
            //remove the # at the front
            hex = hex.Replace("#", "");

            byte a = 255;
            byte r = 255;
            byte g = 255;
            byte b = 255;

            int start = 0;

            //handle ARGB strings (8 characters long)
            if (hex.Length == 8)
            {
                a = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
                start = 2;
            }

            //convert RGB characters to bytes
            r = byte.Parse(hex.Substring(start, 2), System.Globalization.NumberStyles.HexNumber);
            g = byte.Parse(hex.Substring(start + 2, 2), System.Globalization.NumberStyles.HexNumber);
            b = byte.Parse(hex.Substring(start + 4, 2), System.Globalization.NumberStyles.HexNumber);

            return Color.FromArgb(a, r, g, b);
        }


        //public void ddll_subjectcolor_chnage()
        //{
        //    if (ddl_subject_type.Text == "SELECT TYPE OF THE POST")
        //    {
        //        ddl_subject_type.BackColor = HexColor("#f91111");
        //    }
        //    else
        //    {
        //        ddl_subject_type.BackColor = HexColor("#ffffff");
        //        ddl_subject_type.BorderColor = HexColor("#000000");
        //    }

        //    if (ddl_subject_name.Text == "SELECT SUBJECT")
        //    {
        //        ddl_subject_name.BackColor = HexColor("#f91111");
        //    }
        //    else
        //    {
        //        ddl_subject_name.BackColor = HexColor("#ffffff");
        //        ddl_subject_name.BorderColor = HexColor("#000000");
        //    }
        //}





        private void chnage_color_fisrt_button()
        {




            if (txt_first_name.Text == "")
            {
                txt_first_name.BackColor = HexColor("#f91111");
            }
            else
            {
                txt_first_name.BackColor = HexColor("#ffffff");
                txt_first_name.BorderColor = HexColor("#000000");

            }
            if (txt_emailid.Text == "")
            {
                txt_emailid.BackColor = HexColor("#f91111");
            }
            else
            {
                txt_emailid.BackColor = HexColor("#ffffff");
                txt_emailid.BorderColor = HexColor("#000000");
            }

            if (txt_date_birthday.Text == "")
            {
                txt_date_birthday.BackColor = HexColor("#f91111");
            }
            else
            {
                txt_date_birthday.BackColor = HexColor("#ffffff");
                txt_date_birthday.BorderColor = HexColor("#000000");
            }

            if (ddl_religion.Text == "SELECT RELIGION")
            {
                ddl_religion.BackColor = HexColor("#f91111");
            }
            else
            {
                ddl_religion.BackColor = HexColor("#ffffff");
                ddl_religion.BorderColor = HexColor("#000000");
            }

            //if (txt_city_ca.Text == "")
            //{
            //    txt_city_ca.BackColor = HexColor("#f91111");
            //}
            //else
            //{
            //    txt_city_ca.BackColor = HexColor("#ffffff");
            //    txt_city_ca.BorderColor = HexColor("#000000");
            //}


            if (txt_mobile_no_CA.Text == "")
            {
                txt_mobile_no_CA.BackColor = HexColor("#f91111");
            }
            else
            {
                txt_mobile_no_CA.BackColor = HexColor("#ffffff");
                txt_mobile_no_CA.BorderColor = HexColor("#000000");
            }

            //if (txt_name_of_instituation.Text == "")
            //{
            //    txt_name_of_instituation.BackColor = HexColor("#f91111");
            //}
            //else
            //{
            //    txt_name_of_instituation.BackColor = HexColor("#ffffff");
            //    txt_name_of_instituation.BorderColor = HexColor("#000000");
            //}

            //if (txt_Contact_Numbe_instituation.Text == "")
            //{
            //    txt_Contact_Numbe_instituation.BackColor = HexColor("#f91111");
            //}
            //else
            //{
            //    txt_Contact_Numbe_instituation.BackColor = HexColor("#ffffff");
            //    txt_Contact_Numbe_instituation.BorderColor = HexColor("#000000");
            //}
            //if (txt_Present_Salary.Text == "")
            //{
            //    txt_Present_Salary.BackColor = HexColor("#f91111");
            //}
            //else
            //{
            //    txt_Present_Salary.BackColor = HexColor("#ffffff");
            //    txt_Present_Salary.BorderColor = HexColor("#000000");
            //}

            //if (txt_Expected_Salary.Text == "")
            //{
            //    txt_Expected_Salary.BackColor = HexColor("#f91111");
            //}
            //else
            //{
            //    txt_Expected_Salary.BackColor = HexColor("#ffffff");
            //    txt_Expected_Salary.BorderColor = HexColor("#000000");
            //}

            //if (ViewState["file_passport_photo"].ToString() == "")
            //{
            //    file_passport_photo.BackColor = HexColor("#f91111");
            //}
            //else
            //{
            //    file_passport_photo.BackColor = HexColor("#ffffff");
            //    file_passport_photo.BorderColor = HexColor("#000000");
            //}

            //if (ViewState["file_upoad_Signature"].ToString() == "")
            //{
            //    file_upoad_Signature.BackColor = HexColor("#f91111");
            //}
            //else
            //{
            //    file_upoad_Signature.BackColor = HexColor("#ffffff");
            //    file_upoad_Signature.BorderColor = HexColor("#000000");
            //}





        }

        protected void btn_final_submit_Click(object sender, EventArgs e)
        {
            chnage_color_fisrt_button();

            if (txt_position_job_for.Text == "Teacher")
            {
                //if (ddl_subject_type.Text == "SELECT TYPE OF THE POST")
                //{
                //    ddll_subjectcolor_chnage();
                //    messagealert("Please select type of the post");
                //}
                //else if (ddl_subject_name.Text == "SELECT SUBJECT")
                //{
                //    ddll_subjectcolor_chnage();
                //    messagealert("Please select subject ");
                //}
                //else
                //{
                save_data();
                //}


            }
            else
            {
                save_data();

            }

        }

        private void save_data()
        {
            if (txt_first_name.Text == "")
            {
                chnage_color_fisrt_button();
                messagealert("Please enter applicant name");

            }
            else if (txt_emailid.Text == "")
            {
                chnage_color_fisrt_button();
                messagealert("Please enter applicant email id");
            }
            else if (txt_date_birthday.Text == "")
            {
                chnage_color_fisrt_button();
                messagealert("Please enter date of birth");
            }
            else if (ddl_religion.Text == "SELECT RELIGION")
            {
                chnage_color_fisrt_button();
                messagealert("Please enter religion");
            }

            //else if (txt_city_ca.Text == "")
            //{
            //    chnage_color_fisrt_button();
            //    messagealert("Please enter city name");
            //}
            else if (txt_mobile_no_CA.Text == "")
            {
                chnage_color_fisrt_button();
                messagealert("Please enter mobile no.");
            }

            //else if (txt_name_of_instituation.Text == "")
            //{
            //    chnage_color_fisrt_button();
            //    messagealert("Please enter instituation name");
            //}

            else
            {
                //if (txt_tenth_main_subject.Text == "")
                //{
                //    chnage_color_fisrt_button();
                //    messagealert("Please enter high School/10th subject name");
                //}
                //else if (txt_tenth_school_collage_name.Text == "")
                //{
                //    chnage_color_fisrt_button();
                //    messagealert("Please enter high School/10th school or collage name");
                //}
                //else if (txt_tenth_board_university.Text == "")
                //{
                //    chnage_color_fisrt_button();
                //    messagealert("Please enter high School/10th board/university name");
                //}
                //else if (txt_tenth_passing_year.Text == "")
                //{
                //    chnage_color_fisrt_button();
                //    messagealert("Please enter high School/10th passing year");
                //}

                //else if (txt_tenth_percentage_mark.Text == "")
                //{
                //    chnage_color_fisrt_button();
                //    messagealert("Please enter high School/10th percentage");
                //}

                //else if (ddl_tenth_division.Text == "SELECT")
                //{
                //    chnage_color_fisrt_button();
                //    messagealert("Please select high School/10th division");
                //}



                ////-----------------------------Inter / +2--------------------------

                //if (txt_11th_main_subject.Text == "")
                //{
                //    chnage_color_fisrt_button();
                //    messagealert("Please enter  Inter/+2 subject name");
                //}
                //else if (txt_11th_school_collage_name.Text == "")
                //{
                //    chnage_color_fisrt_button();
                //    messagealert("Please enter Inter/+2 school or collage name");
                //}
                //else if (txt_11th_board_university.Text == "")
                //{
                //    chnage_color_fisrt_button();
                //    messagealert("Please enter Inter/+2 board/university");
                //}
                //else if (txt_11th_passing_year.Text == "")
                //{
                //    chnage_color_fisrt_button();
                //    messagealert("Please enter inter/+2 passing year");
                //}


                //else if (txt_11th_percentage_mark.Text == "")
                //{
                //    chnage_color_fisrt_button();
                //    messagealert("Please enter inter/+2 percentage");
                //}

                //else if (ddl_11th_division.Text == "SELECT")
                //{
                //    chnage_color_fisrt_button();
                //    messagealert("Please select inter/+2 division");
                //}
                ////----------------------------Graduation-------------

                //if (txt_Graduation_main_subject.Text == "")
                //{
                //    chnage_color_fisrt_button();
                //    messagealert("Please enter graduation subject name");
                //}
                //else if (txt_Graduation_school_collage_name.Text == "")
                //{
                //    chnage_color_fisrt_button();
                //    messagealert("Please enter graduation collage name");
                //}
                //else if (txt_Graduation_board_university.Text == "")
                //{
                //    chnage_color_fisrt_button();
                //    messagealert("Please enter graduation university name");
                //}
                //else if (txt_Graduation_passing_year.Text == "")
                //{
                //    chnage_color_fisrt_button();
                //    messagealert("Please enter graduation passing year");
                //}


                //else if (txt_Graduation_percentage_mark.Text == "")
                //{
                //    chnage_color_fisrt_button();
                //    messagealert("Please enter graduation percentage");
                //}

                //else if (ddl_Graduation_division.Text == "SELECT")
                //{
                //    chnage_color_fisrt_button();
                //    messagealert("Please select graduation division");
                //}
                //else
                //{
                final_submit();

                //bool check_WorkExperience = get_WorkExperience_add_or_not();
                //if (check_WorkExperience == false)
                //{
                //    messagealert("Please add work experience");
                //}
                //else
                //{
                //    if (txt_Contact_Numbe_instituation.Text == "")
                //    {
                //        chnage_color_fisrt_button();
                //        messagealert("Please enter instituation contact no.");
                //    }
                //    else if (txt_Present_Salary.Text == "")
                //    {
                //        chnage_color_fisrt_button();
                //        messagealert("Please enter present salary");
                //    }
                //    else if (txt_Expected_Salary.Text == "")
                //    {
                //        chnage_color_fisrt_button();
                //        messagealert("Please enter expected salary");
                //    }
                //    else if (ViewState["file_passport_photo"].ToString() == "")
                //    {
                //        chnage_color_fisrt_button();
                //        messagealert("Please upload passport size photo");
                //    }

                //    else if (ViewState["file_upoad_Signature"].ToString() == "")
                //    {
                //        chnage_color_fisrt_button();
                //        messagealert("Please upload signature ");
                //    }
                //    else if (chk_declaration.Checked == false)
                //    {
                //        messagealert("Please check declaration");
                //    }
                //    else
                //    {



                //    }
                //}
                //}
            }
        }

        private void final_submit()
        {

            SqlCommand cmd;
            string query = "Update HR_Employee_Online_Apply set Date=@Date,idate=@idate,subject_name=@subject_name,Salutation=@Salutation,First_Name=@First_Name,Middle_Name=@Middle_Name,Last_Name=@Last_Name,Emailid=@Emailid,Date_birthday=@Date_birthday,Gender=@Gender,Place_Of_Birth=@Place_Of_Birth,Birth_State=@Birth_State,Religion=@Religion,Nationality=@Nationality,Marital_Status=@Marital_Status,Address_CA=@Address_CA,City_CA=@City_CA,State_CA=@State_CA,Pincode_CA=@Pincode_CA,mobile_no_CA=@mobile_no_CA,Residence_telephone_no_CA=@Residence_telephone_no_CA,address_pa=@address_pa,city_pa=@city_pa,state_pa=@state_pa,pin_pa=@pin_pa,chiled_name1=@chiled_name1,chiled_gender1=@chiled_gender1,chiled_age1=@chiled_age1,chiled_name2=@chiled_name2,chiled_gender2=@chiled_gender2,chiled_age2=@chiled_age2,chiled_name3=@chiled_name3,chiled_gender3=@chiled_gender3,chiled_age3=@chiled_age3,fathername=@fathername,father_occupation=@father_occupation,mother_name=@mother_name,mother_occupation=@mother_occupation,Spouse_name=@Spouse_name,Spouses_job_is_transferable=@Spouses_job_is_transferable,spouse_qualification=@spouse_qualification,spouse_profession=@spouse_profession,spouse_organization=@spouse_organization,spouse_designation=@spouse_designation,completed_years=@completed_years,teaching_years=@teaching_years,Administration_year=@Administration_year,any_other=@any_other,Current_name_of_instituation=@Current_name_of_instituation,instituation_address=@instituation_address,Contact_Numbe_instituation=@Contact_Numbe_instituation,Designation_work=@Designation_work,joining_date=@joining_date,place_of_posting=@place_of_posting,Present_Salary=@Present_Salary,Basic_Salary_Present=@Basic_Salary_Present,Allowance_Present=@Allowance_Present,Other_Benefits_Present=@Other_Benefits_Present,Under_Service_Bond=@Under_Service_Bond,years_service_bond=@years_service_bond,Expected_Salary=@Expected_Salary,English_read=@English_read,English_write=@English_write,English_Speak=@English_Speak,Hindi_read=@Hindi_read,Hindi_write=@Hindi_write,Hindi_speak=@Hindi_speak,Bangla_read=@Bangla_read,Bangla_write=@Bangla_write,Bangla_speak=@Bangla_speak,Other_Language=@Other_Language,Proficiency_In_Computer=@Proficiency_In_Computer,passport_photo=@passport_photo,Signature=@Signature,order_id=@order_id,no_of_seat=@no_of_seat,iam_fresher=@iam_fresher,Subject_Type=@Subject_Type where Apply_id=@Apply_id";



            cmd = new SqlCommand(query);
            if (chk_iam_fresher.Checked == true)
            {
                cmd.Parameters.AddWithValue("@iam_fresher", "Yes");
            }
            else
            {
                cmd.Parameters.AddWithValue("@iam_fresher", "No");
            }

            //if (ddl_subject_type.Text == "SELECT TYPE OF THE POST")
            //{
            cmd.Parameters.AddWithValue("@Subject_Type", "N/A");
            cmd.Parameters.AddWithValue("@subject_name", "N/A");
            //}
            //else
            //{
            //    cmd.Parameters.AddWithValue("@Subject_Type", ddl_subject_type.Text);
            //    cmd.Parameters.AddWithValue("@subject_name", ddl_subject_name.Text);

            //}
            cmd.Parameters.AddWithValue("@no_of_seat", ViewState["no_application"].ToString());
            cmd.Parameters.AddWithValue("@Apply_id", ViewState["Applyid"].ToString());
            cmd.Parameters.AddWithValue("@Date", PayrollMy.date);
            cmd.Parameters.AddWithValue("@idate", PayrollMy.Now.idate());




            cmd.Parameters.AddWithValue("@Salutation", ddl_salution_name.Text);
            cmd.Parameters.AddWithValue("@First_Name", txt_first_name.Text);
            cmd.Parameters.AddWithValue("@Middle_Name", txt_Middle_Name.Text);
            cmd.Parameters.AddWithValue("@Last_Name", txt_last_Name.Text);
            cmd.Parameters.AddWithValue("@Emailid", txt_emailid.Text);
            cmd.Parameters.AddWithValue("@Date_birthday", Convert.ToDateTime(txt_date_birthday.Text).ToString("dd-MMM-yyyy"));
            cmd.Parameters.AddWithValue("@Gender", ddl_gender.Text);
            cmd.Parameters.AddWithValue("@Place_Of_Birth", txt_plac_of_birth.Text);
            cmd.Parameters.AddWithValue("@Birth_State", txt_birthstate.Text);
            cmd.Parameters.AddWithValue("@Religion", ddl_religion.Text);
            cmd.Parameters.AddWithValue("@Nationality", txt_Nationality.Text);
            cmd.Parameters.AddWithValue("@Marital_Status", ddl_marital_status.Text);
            cmd.Parameters.AddWithValue("@Address_CA", txt_address_ca.Text);
            cmd.Parameters.AddWithValue("@City_CA", txt_city_ca.Text);
            cmd.Parameters.AddWithValue("@State_CA", txt_state_ca.Text);
            cmd.Parameters.AddWithValue("@Pincode_CA", txt_pincode_CA.Text);
            cmd.Parameters.AddWithValue("@mobile_no_CA", txt_mobile_no_CA.Text);
            cmd.Parameters.AddWithValue("@Residence_telephone_no_CA", txt_residence_telephone_no.Text);
            cmd.Parameters.AddWithValue("@address_pa", txt_address_pa.Text);
            cmd.Parameters.AddWithValue("@city_pa", txt_city_pa.Text);
            cmd.Parameters.AddWithValue("@state_pa", txt_state_pa.Text);
            cmd.Parameters.AddWithValue("@pin_pa", txt_pin_pa.Text);

            if (txt_chiled_name1.Text == "")
            {
                cmd.Parameters.AddWithValue("@chiled_name1", "");
                cmd.Parameters.AddWithValue("@chiled_gender1", "");
                cmd.Parameters.AddWithValue("@chiled_age1", "");

            }
            else
            {
                cmd.Parameters.AddWithValue("@chiled_name1", txt_chiled_name1.Text);
                cmd.Parameters.AddWithValue("@chiled_gender1", ddl_chiled_gender1.Text);
                cmd.Parameters.AddWithValue("@chiled_age1", txt_chiled_age1.Text);
            }

            if (txt_chiled_name2.Text == "")
            {
                cmd.Parameters.AddWithValue("@chiled_name2", "");
                cmd.Parameters.AddWithValue("@chiled_gender2", "");
                cmd.Parameters.AddWithValue("@chiled_age2", "");
            }
            else
            {
                cmd.Parameters.AddWithValue("@chiled_name2", txt_chiled_name2.Text);
                cmd.Parameters.AddWithValue("@chiled_gender2", ddl_chiled_gender2.Text);
                cmd.Parameters.AddWithValue("@chiled_age2", txt_chiled_age2.Text);
            }


            if (txt_chiled_name3.Text == "")
            {

                cmd.Parameters.AddWithValue("@chiled_name3", "");
                cmd.Parameters.AddWithValue("@chiled_gender3", "");
                cmd.Parameters.AddWithValue("@chiled_age3", "");
            }
            else
            {
                cmd.Parameters.AddWithValue("@chiled_name3", txt_chiled_name3.Text);
                cmd.Parameters.AddWithValue("@chiled_gender3", ddl_chiled_gender2.Text);
                cmd.Parameters.AddWithValue("@chiled_age3", txt_chiled_age3.Text);

            }


            cmd.Parameters.AddWithValue("@fathername", txt_fathername.Text);
            cmd.Parameters.AddWithValue("@father_occupation", txt_father_occupation.Text);
            cmd.Parameters.AddWithValue("@mother_name", txt_mother_name.Text);
            cmd.Parameters.AddWithValue("@mother_occupation", txt_mother_occupation.Text);
            cmd.Parameters.AddWithValue("@Spouse_name", txt_Spouse_name.Text);
            cmd.Parameters.AddWithValue("@Spouses_job_is_transferable", ddl_spouse_transferableye_no.Text);
            cmd.Parameters.AddWithValue("@spouse_qualification", txt_spouse_qualification.Text);
            cmd.Parameters.AddWithValue("@spouse_profession", txt_spouse_Profession.Text);
            cmd.Parameters.AddWithValue("@spouse_organization", txt_spouse_organization.Text);
            cmd.Parameters.AddWithValue("@spouse_designation", txt_spouse_designation.Text);
            cmd.Parameters.AddWithValue("@completed_years", txt_in_completed_years.Text);
            cmd.Parameters.AddWithValue("@teaching_years", txt_teaching_years.Text);
            cmd.Parameters.AddWithValue("@Administration_year", txt_Administration_year.Text);
            cmd.Parameters.AddWithValue("@any_other", txt_any_other.Text);
            cmd.Parameters.AddWithValue("@Current_name_of_instituation", txt_name_of_instituation.Text);
            cmd.Parameters.AddWithValue("@instituation_address", txt_instituation_address.Text);
            cmd.Parameters.AddWithValue("@Contact_Numbe_instituation", txt_Contact_Numbe_instituation.Text);
            cmd.Parameters.AddWithValue("@Designation_work", txt_Designation_work.Text);
            try
            {
                cmd.Parameters.AddWithValue("@joining_date", Convert.ToDateTime(txt_joining_date.Text).ToString("dd-MMM-yyyy"));
            }
            catch
            {

                cmd.Parameters.AddWithValue("@joining_date", "");
            }

            cmd.Parameters.AddWithValue("@place_of_posting", txt_place_of_posting.Text);
            cmd.Parameters.AddWithValue("@Present_Salary", txt_Present_Salary.Text);
            cmd.Parameters.AddWithValue("@Basic_Salary_Present", txt_basic_Salary_Present.Text);
            cmd.Parameters.AddWithValue("@Allowance_Present", txt_Allowance_Present.Text);
            cmd.Parameters.AddWithValue("@Other_Benefits_Present", txt_Other_Benefits_Present.Text);

            if (chk_service_bond.Checked == true)
            {
                cmd.Parameters.AddWithValue("@Under_Service_Bond", "Yes");
                cmd.Parameters.AddWithValue("@years_service_bond", txt_no_of_years_service_bond.Text);

            }
            else
            {
                cmd.Parameters.AddWithValue("@Under_Service_Bond", "NO");
                cmd.Parameters.AddWithValue("@years_service_bond", "");
            }


            cmd.Parameters.AddWithValue("@Expected_Salary", txt_Expected_Salary.Text);
            if (chk_english_read.Checked == true)
            {
                cmd.Parameters.AddWithValue("@English_read", "Yes");
            }
            else
            {
                cmd.Parameters.AddWithValue("@English_read", "No");
            }
            if (chk_english_write.Checked == true)
            {
                cmd.Parameters.AddWithValue("@English_write", "Yes");
            }
            else
            {
                cmd.Parameters.AddWithValue("@English_write", "No");
            }
            if (chk_english_Speak.Checked == true)
            {
                cmd.Parameters.AddWithValue("@English_Speak", "Yes");
            }
            else
            {
                cmd.Parameters.AddWithValue("@English_Speak", "No");
            }


            if (chk_hindi_read.Checked == true)
            {
                cmd.Parameters.AddWithValue("@Hindi_read", "Yes");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Hindi_read", "No");
            }


            if (chk_hindi_write.Checked == true)
            {
                cmd.Parameters.AddWithValue("@Hindi_write", "Yes");

            }
            else
            {
                cmd.Parameters.AddWithValue("@Hindi_write", "No");
            }

            if (chk_hindi_speak.Checked == true)
            {
                cmd.Parameters.AddWithValue("@Hindi_speak", "Yes");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Hindi_speak", "No");

            }

            if (chk_Bangla_read.Checked == true)
            {
                cmd.Parameters.AddWithValue("@Bangla_read", "Yes");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Bangla_read", "No");
            }

            if (chk_Bangla_write.Checked == true)
            {
                cmd.Parameters.AddWithValue("@Bangla_write", "Yes");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Bangla_write", "No");
            }

            if (chk_Bangla_speak.Checked == true)
            {
                cmd.Parameters.AddWithValue("@Bangla_speak", "Yes");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Bangla_speak", "No");
            }

            cmd.Parameters.AddWithValue("@Other_Language", txt_any_other_language.Text);
            cmd.Parameters.AddWithValue("@Proficiency_In_Computer", ddl_computer_yesno.Text);
            cmd.Parameters.AddWithValue("@passport_photo", ViewState["file_passport_photo"].ToString());
            cmd.Parameters.AddWithValue("@Signature", ViewState["file_upoad_Signature"].ToString());
            cmd.Parameters.AddWithValue("@order_id", ViewState["Order_id"].ToString());
            if (cmd.ExecuteCommand(PayrollMy.con))
            {
                send_applicant_Academy_data();

                // go to payment_gateway 
                int fill_no_seat = PayrollMy.data("Select count(Id) from HR_Employee_Online_Apply where    HiringTypeId=" + ViewState["HiringTypeId"].ToString() + " and Payment_Status='Paid'  ").ToInt();                                                                                                                                                                           // ViewState["Getwey_Type"] = mycode.get_payment_Getwey_Type();
                                                                                                                                                                                                                                                                                                                                                                                      // bool ispaymnet_on = PayrollMy.get_status_ispaymnet_on();
                if (Convert.ToInt32(ViewState["no_application"].ToString()) > fill_no_seat)
                {
                    PayrollMy.execute("update HR_Employee_Online_Apply set razorpay_payment_id='0',razorpay_signature='0',Payment_Status='Paid',Seat_Remarks='OK' where Apply_id='" + ViewState["Applyid"].ToString() + "' and Order_id='" + ViewState["Order_id"].ToString() + "'");
                    try
                    {
                        if(ConfigurationManager.AppSettings["hrtype"].ToString()=="New")
                        {
                            Response.Redirect("../hr/print-application-receipt/" + ViewState["Applyid"].ToString(), false);
                        }
                        else
                        {
                            Response.Redirect("../print-application-receipt/" + ViewState["Applyid"].ToString(), false);

                        }
                    }
                    catch
                    {
                        Response.Redirect("../print-application-receipt/" + ViewState["Applyid"].ToString(), false);
                    }


                }

                else
                {
                    messagealert("Sorry seats are full please try next time");

                }

            }


            #endregion
        }
        private void send_applicant_Academy_data()
        {
            //High School/10th
            #region High School/10th


            if (txt_tenth_main_subject.Text == "")
            {


            }
            else
            {

                DataTable dt = PayrollMy.dataTable("select * from dbo.[HR_Employee_Online_Academy_Details] where  Apply_id='" + ViewState["Applyid"].ToString() + "' and Qualification='High School/10th'");
                if (dt.Rows.Count == 0)
                {
                    SqlCommand cmd;

                    cmd = new SqlCommand("INSERT INTO HR_Employee_Online_Academy_Details (Apply_id,Qualification,Main_Subjects,School_College,Board_University,Year_of_Passing,Percentage_of_Marks,Division) values (@Apply_id,@Qualification,@Main_Subjects,@School_College,@Board_University,@Year_of_Passing,@Percentage_of_Marks,@Division)");
                    cmd.Parameters.AddWithValue("@Apply_id", ViewState["Applyid"].ToString());
                    cmd.Parameters.AddWithValue("@Qualification", "High School/10th");
                    cmd.Parameters.AddWithValue("@Main_Subjects", txt_tenth_main_subject.Text);
                    cmd.Parameters.AddWithValue("@School_College", txt_tenth_school_collage_name.Text);
                    cmd.Parameters.AddWithValue("@Board_University", txt_tenth_board_university.Text);
                    cmd.Parameters.AddWithValue("@Year_of_Passing", txt_tenth_passing_year.Text);
                    cmd.Parameters.AddWithValue("@Percentage_of_Marks", txt_tenth_percentage_mark.Text);
                    cmd.Parameters.AddWithValue("@Division", ddl_tenth_division.Text);
                    cmd.ExecuteCommand(PayrollMy.con);

                }
                else
                {
                    SqlCommand cmd;

                    cmd = new SqlCommand("Update HR_Employee_Online_Academy_Details set  Main_Subjects=@Main_Subjects,School_College=@School_College,Board_University=@Board_University,Year_of_Passing=@Year_of_Passing,Percentage_of_Marks=@Percentage_of_Marks,Division=@Division where Apply_id=@Apply_id and Qualification=@Qualification");
                    cmd.Parameters.AddWithValue("@Apply_id", ViewState["Applyid"].ToString());
                    cmd.Parameters.AddWithValue("@Qualification", "High School/10th");
                    cmd.Parameters.AddWithValue("@Main_Subjects", txt_tenth_main_subject.Text);
                    cmd.Parameters.AddWithValue("@School_College", txt_tenth_school_collage_name.Text);
                    cmd.Parameters.AddWithValue("@Board_University", txt_tenth_board_university.Text);
                    cmd.Parameters.AddWithValue("@Year_of_Passing", txt_tenth_passing_year.Text);
                    cmd.Parameters.AddWithValue("@Percentage_of_Marks", txt_tenth_percentage_mark.Text);
                    cmd.Parameters.AddWithValue("@Division", ddl_tenth_division.Text);
                    cmd.ExecuteCommand(PayrollMy.con);
                }

            }
            #endregion


            #region Inter/+2
            if (txt_11th_main_subject.Text == "")
            {


            }
            else
            {
                DataTable dt1 = PayrollMy.dataTable("select * from dbo.[HR_Employee_Online_Academy_Details] where  Apply_id='" + ViewState["Applyid"].ToString() + "' and Qualification='Inter/+2'");
                if (dt1.Rows.Count == 0)
                {
                    SqlCommand cmd;

                    cmd = new SqlCommand("INSERT INTO HR_Employee_Online_Academy_Details (Apply_id,Qualification,Main_Subjects,School_College,Board_University,Year_of_Passing,Percentage_of_Marks,Division) values (@Apply_id,@Qualification,@Main_Subjects,@School_College,@Board_University,@Year_of_Passing,@Percentage_of_Marks,@Division)");
                    cmd.Parameters.AddWithValue("@Apply_id", ViewState["Applyid"].ToString());
                    cmd.Parameters.AddWithValue("@Qualification", "Inter/+2");
                    cmd.Parameters.AddWithValue("@Main_Subjects", txt_11th_main_subject.Text);
                    cmd.Parameters.AddWithValue("@School_College", txt_11th_school_collage_name.Text);
                    cmd.Parameters.AddWithValue("@Board_University", txt_11th_board_university.Text);
                    cmd.Parameters.AddWithValue("@Year_of_Passing", txt_11th_passing_year.Text);
                    cmd.Parameters.AddWithValue("@Percentage_of_Marks", txt_11th_percentage_mark.Text);
                    cmd.Parameters.AddWithValue("@Division", ddl_11th_division.Text);
                    cmd.ExecuteCommand(PayrollMy.con);

                }
                else
                {
                    SqlCommand cmd;

                    cmd = new SqlCommand("Update HR_Employee_Online_Academy_Details set  Main_Subjects=@Main_Subjects,School_College=@School_College,Board_University=@Board_University,Year_of_Passing=@Year_of_Passing,Percentage_of_Marks=@Percentage_of_Marks,Division=@Division where Apply_id=@Apply_id and Qualification=@Qualification");
                    cmd.Parameters.AddWithValue("@Apply_id", ViewState["Applyid"].ToString());
                    cmd.Parameters.AddWithValue("@Qualification", "Inter/+2");
                    cmd.Parameters.AddWithValue("@Main_Subjects", txt_11th_main_subject.Text);
                    cmd.Parameters.AddWithValue("@School_College", txt_11th_school_collage_name.Text);
                    cmd.Parameters.AddWithValue("@Board_University", txt_11th_board_university.Text);
                    cmd.Parameters.AddWithValue("@Year_of_Passing", txt_11th_passing_year.Text);
                    cmd.Parameters.AddWithValue("@Percentage_of_Marks", txt_11th_percentage_mark.Text);
                    cmd.Parameters.AddWithValue("@Division", ddl_11th_division.Text);
                    cmd.ExecuteCommand(PayrollMy.con);


                }
            }
            #endregion

            #region Graduation
            if (txt_Graduation_main_subject.Text == "")
            {


            }
            else
            {
                DataTable dt2 = PayrollMy.dataTable("select * from dbo.[HR_Employee_Online_Academy_Details] where  Apply_id='" + ViewState["Applyid"].ToString() + "' and Qualification='Graduation'");
                if (dt2.Rows.Count == 0)
                {
                    SqlCommand cmd;

                    cmd = new SqlCommand("INSERT INTO HR_Employee_Online_Academy_Details (Apply_id,Qualification,Main_Subjects,School_College,Board_University,Year_of_Passing,Percentage_of_Marks,Division) values (@Apply_id,@Qualification,@Main_Subjects,@School_College,@Board_University,@Year_of_Passing,@Percentage_of_Marks,@Division)");
                    cmd.Parameters.AddWithValue("@Apply_id", ViewState["Applyid"].ToString());
                    cmd.Parameters.AddWithValue("@Qualification", "Graduation");
                    cmd.Parameters.AddWithValue("@Main_Subjects", txt_Graduation_main_subject.Text);
                    cmd.Parameters.AddWithValue("@School_College", txt_Graduation_school_collage_name.Text);
                    cmd.Parameters.AddWithValue("@Board_University", txt_Graduation_board_university.Text);
                    cmd.Parameters.AddWithValue("@Year_of_Passing", txt_Graduation_passing_year.Text);
                    cmd.Parameters.AddWithValue("@Percentage_of_Marks", txt_Graduation_percentage_mark.Text);
                    cmd.Parameters.AddWithValue("@Division", ddl_Graduation_division.Text);
                    cmd.ExecuteCommand(PayrollMy.con);

                }
                else
                {
                    SqlCommand cmd;

                    cmd = new SqlCommand("Update HR_Employee_Online_Academy_Details set  Main_Subjects=@Main_Subjects,School_College=@School_College,Board_University=@Board_University,Year_of_Passing=@Year_of_Passing,Percentage_of_Marks=@Percentage_of_Marks,Division=@Division where Apply_id=@Apply_id and Qualification=@Qualification");
                    cmd.Parameters.AddWithValue("@Apply_id", ViewState["Applyid"].ToString());
                    cmd.Parameters.AddWithValue("@Qualification", "Graduation");
                    cmd.Parameters.AddWithValue("@Main_Subjects", txt_Graduation_main_subject.Text);
                    cmd.Parameters.AddWithValue("@School_College", txt_Graduation_school_collage_name.Text);
                    cmd.Parameters.AddWithValue("@Board_University", txt_Graduation_board_university.Text);
                    cmd.Parameters.AddWithValue("@Year_of_Passing", txt_Graduation_passing_year.Text);
                    cmd.Parameters.AddWithValue("@Percentage_of_Marks", txt_Graduation_percentage_mark.Text);
                    cmd.Parameters.AddWithValue("@Division", ddl_Graduation_division.Text);
                    cmd.ExecuteCommand(PayrollMy.con);


                }
            }
            #endregion

            #region Post Graduation
            if (txt_PostGraduation_main_subject.Text == "")
            {

            }
            else
            {
                DataTable dt3 = PayrollMy.dataTable("select * from dbo.[HR_Employee_Online_Academy_Details] where  Apply_id='" + ViewState["Applyid"].ToString() + "' and Qualification='Post Graduation'");
                if (dt3.Rows.Count == 0)
                {
                    SqlCommand cmd;

                    cmd = new SqlCommand("INSERT INTO HR_Employee_Online_Academy_Details (Apply_id,Qualification,Main_Subjects,School_College,Board_University,Year_of_Passing,Percentage_of_Marks,Division) values (@Apply_id,@Qualification,@Main_Subjects,@School_College,@Board_University,@Year_of_Passing,@Percentage_of_Marks,@Division)");
                    cmd.Parameters.AddWithValue("@Apply_id", ViewState["Applyid"].ToString());
                    cmd.Parameters.AddWithValue("@Qualification", "Post Graduation");
                    cmd.Parameters.AddWithValue("@Main_Subjects", txt_PostGraduation_main_subject.Text);
                    cmd.Parameters.AddWithValue("@School_College", txt_PostGraduation_school_collage_name.Text);
                    cmd.Parameters.AddWithValue("@Board_University", txt_PostGraduation_board_university.Text);
                    cmd.Parameters.AddWithValue("@Year_of_Passing", txt_PostGraduation_passing_year.Text);
                    cmd.Parameters.AddWithValue("@Percentage_of_Marks", txt_PostGraduation_percentage_mark.Text);
                    cmd.Parameters.AddWithValue("@Division", ddl_PostGraduation_division.Text);
                    cmd.ExecuteCommand(PayrollMy.con);

                }
                else
                {
                    SqlCommand cmd;

                    cmd = new SqlCommand("Update HR_Employee_Online_Academy_Details set  Main_Subjects=@Main_Subjects,School_College=@School_College,Board_University=@Board_University,Year_of_Passing=@Year_of_Passing,Percentage_of_Marks=@Percentage_of_Marks,Division=@Division where Apply_id=@Apply_id and Qualification=@Qualification");
                    cmd.Parameters.AddWithValue("@Apply_id", ViewState["Applyid"].ToString());
                    cmd.Parameters.AddWithValue("@Qualification", "Post Graduation");
                    cmd.Parameters.AddWithValue("@Main_Subjects", txt_PostGraduation_main_subject.Text);
                    cmd.Parameters.AddWithValue("@School_College", txt_PostGraduation_school_collage_name.Text);
                    cmd.Parameters.AddWithValue("@Board_University", txt_PostGraduation_board_university.Text);
                    cmd.Parameters.AddWithValue("@Year_of_Passing", txt_PostGraduation_passing_year.Text);
                    cmd.Parameters.AddWithValue("@Percentage_of_Marks", txt_PostGraduation_percentage_mark.Text);
                    cmd.Parameters.AddWithValue("@Division", ddl_PostGraduation_division.Text);
                    cmd.ExecuteCommand(PayrollMy.con);


                }
            }



            #endregion

            #region M Phil.

            if (txt_PostGraduation_main_subject.Text == "")
            {

            }
            else
            {
                DataTable dt4 = PayrollMy.dataTable("select * from dbo.[HR_Employee_Online_Academy_Details] where  Apply_id='" + ViewState["Applyid"].ToString() + "' and Qualification='M Phil.'");
                if (dt4.Rows.Count == 0)
                {
                    SqlCommand cmd;
                    cmd = new SqlCommand("INSERT INTO HR_Employee_Online_Academy_Details (Apply_id,Qualification,Main_Subjects,School_College,Board_University,Year_of_Passing,Percentage_of_Marks,Division) values (@Apply_id,@Qualification,@Main_Subjects,@School_College,@Board_University,@Year_of_Passing,@Percentage_of_Marks,@Division)");
                    cmd.Parameters.AddWithValue("@Apply_id", ViewState["Applyid"].ToString());
                    cmd.Parameters.AddWithValue("@Qualification", "M Phil.");
                    cmd.Parameters.AddWithValue("@Main_Subjects", txt_MPhil_main_subject.Text);
                    cmd.Parameters.AddWithValue("@School_College", txt_MPhil_school_collage_name.Text);
                    cmd.Parameters.AddWithValue("@Board_University", txt_MPhil_board_university.Text);
                    cmd.Parameters.AddWithValue("@Year_of_Passing", txt_MPhil_passing_year.Text);
                    cmd.Parameters.AddWithValue("@Percentage_of_Marks", txt_MPhil_percentage_mark.Text);
                    cmd.Parameters.AddWithValue("@Division", ddl_MPhil_division.Text);
                    cmd.ExecuteCommand(PayrollMy.con);

                }
                else
                {
                    SqlCommand cmd;

                    cmd = new SqlCommand("Update HR_Employee_Online_Academy_Details set  Main_Subjects=@Main_Subjects,School_College=@School_College,Board_University=@Board_University,Year_of_Passing=@Year_of_Passing,Percentage_of_Marks=@Percentage_of_Marks,Division=@Division where Apply_id=@Apply_id and Qualification=@Qualification");
                    cmd.Parameters.AddWithValue("@Apply_id", ViewState["Applyid"].ToString());
                    cmd.Parameters.AddWithValue("@Qualification", "M Phil.");
                    cmd.Parameters.AddWithValue("@Main_Subjects", txt_MPhil_main_subject.Text);
                    cmd.Parameters.AddWithValue("@School_College", txt_MPhil_school_collage_name.Text);
                    cmd.Parameters.AddWithValue("@Board_University", txt_MPhil_board_university.Text);
                    cmd.Parameters.AddWithValue("@Year_of_Passing", txt_MPhil_passing_year.Text);
                    cmd.Parameters.AddWithValue("@Percentage_of_Marks", txt_MPhil_percentage_mark.Text);
                    cmd.Parameters.AddWithValue("@Division", ddl_MPhil_division.Text);
                    cmd.ExecuteCommand(PayrollMy.con);


                }

            }

            #endregion


            #region B.Ed.

            if (txt_bed_main_subject.Text == "")
            {

            }
            else
            {
                DataTable dt5 = PayrollMy.dataTable("select * from dbo.[HR_Employee_Online_Academy_Details] where  Apply_id='" + ViewState["Applyid"].ToString() + "' and Qualification='B.Ed.'");
                if (dt5.Rows.Count == 0)
                {
                    SqlCommand cmd;
                    cmd = new SqlCommand("INSERT INTO HR_Employee_Online_Academy_Details (Apply_id,Qualification,Main_Subjects,School_College,Board_University,Year_of_Passing,Percentage_of_Marks,Division) values (@Apply_id,@Qualification,@Main_Subjects,@School_College,@Board_University,@Year_of_Passing,@Percentage_of_Marks,@Division)");
                    cmd.Parameters.AddWithValue("@Apply_id", ViewState["Applyid"].ToString());
                    cmd.Parameters.AddWithValue("@Qualification", "B.Ed.");
                    cmd.Parameters.AddWithValue("@Main_Subjects", txt_bed_main_subject.Text);
                    cmd.Parameters.AddWithValue("@School_College", txt_bed_school_collage_name.Text);
                    cmd.Parameters.AddWithValue("@Board_University", txt_bed_board_university.Text);
                    cmd.Parameters.AddWithValue("@Year_of_Passing", txt_bed_passing_year.Text);
                    cmd.Parameters.AddWithValue("@Percentage_of_Marks", txt_bed_percentage_mark.Text);
                    cmd.Parameters.AddWithValue("@Division", ddl_PHD_division.Text);
                    cmd.ExecuteCommand(PayrollMy.con);

                }
                else
                {
                    SqlCommand cmd;
                    cmd = new SqlCommand("Update HR_Employee_Online_Academy_Details set  Main_Subjects=@Main_Subjects,School_College=@School_College,Board_University=@Board_University,Year_of_Passing=@Year_of_Passing,Percentage_of_Marks=@Percentage_of_Marks,Division=@Division where Apply_id=@Apply_id and Qualification=@Qualification");
                    cmd.Parameters.AddWithValue("@Apply_id", ViewState["Applyid"].ToString());
                    cmd.Parameters.AddWithValue("@Qualification", "B.Ed.");
                    cmd.Parameters.AddWithValue("@Main_Subjects", txt_PostGraduation_main_subject.Text);
                    cmd.Parameters.AddWithValue("@School_College", txt_PostGraduation_school_collage_name.Text);
                    cmd.Parameters.AddWithValue("@Board_University", txt_PostGraduation_board_university.Text);
                    cmd.Parameters.AddWithValue("@Year_of_Passing", txt_PostGraduation_passing_year.Text);
                    cmd.Parameters.AddWithValue("@Percentage_of_Marks", txt_PostGraduation_percentage_mark.Text);
                    cmd.Parameters.AddWithValue("@Division", ddl_PostGraduation_division.Text);
                    cmd.ExecuteCommand(PayrollMy.con);



                }
            }


            #endregion


            #region CTET

            if (txt_bed_CTET_subject.Text == "")
            {

            }
            else
            {
                DataTable dt6 = PayrollMy.dataTable("select * from dbo.[HR_Employee_Online_Academy_Details] where  Apply_id='" + ViewState["Applyid"].ToString() + "' and Qualification='CTET'");
                if (dt6.Rows.Count == 0)
                {
                    SqlCommand cmd;
                    cmd = new SqlCommand("INSERT INTO HR_Employee_Online_Academy_Details (Apply_id,Qualification,Main_Subjects,School_College,Board_University,Year_of_Passing,Percentage_of_Marks,Division) values (@Apply_id,@Qualification,@Main_Subjects,@School_College,@Board_University,@Year_of_Passing,@Percentage_of_Marks,@Division)");
                    cmd.Parameters.AddWithValue("@Apply_id", ViewState["Applyid"].ToString());
                    cmd.Parameters.AddWithValue("@Qualification", "CTET");
                    cmd.Parameters.AddWithValue("@Main_Subjects", txt_bed_CTET_subject.Text);
                    cmd.Parameters.AddWithValue("@School_College", txt_CTET_school_collage_name.Text);
                    cmd.Parameters.AddWithValue("@Board_University", txt_CTET_board_university.Text);
                    cmd.Parameters.AddWithValue("@Year_of_Passing", txt_CTET_passing_year.Text);
                    cmd.Parameters.AddWithValue("@Percentage_of_Marks", txt_CTET_percentage_mark.Text);
                    cmd.Parameters.AddWithValue("@Division", ddl_CTET_division.Text);
                    cmd.ExecuteCommand(PayrollMy.con);

                }
                else
                {
                    SqlCommand cmd;
                    cmd = new SqlCommand("Update HR_Employee_Online_Academy_Details set  Main_Subjects=@Main_Subjects,School_College=@School_College,Board_University=@Board_University,Year_of_Passing=@Year_of_Passing,Percentage_of_Marks=@Percentage_of_Marks,Division=@Division where Apply_id=@Apply_id and Qualification=@Qualification");
                    cmd.Parameters.AddWithValue("@Apply_id", ViewState["Applyid"].ToString());
                    cmd.Parameters.AddWithValue("@Qualification", "CTET");
                    cmd.Parameters.AddWithValue("@Main_Subjects", txt_bed_CTET_subject.Text);
                    cmd.Parameters.AddWithValue("@School_College", txt_CTET_school_collage_name.Text);
                    cmd.Parameters.AddWithValue("@Board_University", txt_CTET_board_university.Text);
                    cmd.Parameters.AddWithValue("@Year_of_Passing", txt_CTET_passing_year.Text);
                    cmd.Parameters.AddWithValue("@Percentage_of_Marks", txt_CTET_percentage_mark.Text);
                    cmd.Parameters.AddWithValue("@Division", ddl_CTET_division.Text);
                    cmd.ExecuteCommand(PayrollMy.con);



                }
            }

            #endregion

            #region anyother

            if (txt_anyother_subject.Text == "")
            {

            }
            else
            {
                DataTable dt7 = PayrollMy.dataTable("select * from dbo.[HR_Employee_Online_Academy_Details] where  Apply_id='" + ViewState["Applyid"].ToString() + "' and Qualification='Any Other'");
                if (dt7.Rows.Count == 0)
                {
                    SqlCommand cmd;
                    cmd = new SqlCommand("INSERT INTO HR_Employee_Online_Academy_Details (Apply_id,Qualification,Main_Subjects,School_College,Board_University,Year_of_Passing,Percentage_of_Marks,Division) values (@Apply_id,@Qualification,@Main_Subjects,@School_College,@Board_University,@Year_of_Passing,@Percentage_of_Marks,@Division)");
                    cmd.Parameters.AddWithValue("@Apply_id", ViewState["Applyid"].ToString());
                    cmd.Parameters.AddWithValue("@Qualification", "Any Other");
                    cmd.Parameters.AddWithValue("@Main_Subjects", txt_anyother_subject.Text);
                    cmd.Parameters.AddWithValue("@School_College", txt_anyother_school_collage_name.Text);
                    cmd.Parameters.AddWithValue("@Board_University", txt_anyother_board_university.Text);
                    cmd.Parameters.AddWithValue("@Year_of_Passing", txt_anyother_passing_year.Text);
                    cmd.Parameters.AddWithValue("@Percentage_of_Marks", txt_anyother_percentage_mark.Text);
                    cmd.Parameters.AddWithValue("@Division", ddl_anyother_division.Text);
                    cmd.ExecuteCommand(PayrollMy.con);

                }
                else
                {
                    SqlCommand cmd;
                    cmd = new SqlCommand("Update HR_Employee_Online_Academy_Details set  Main_Subjects=@Main_Subjects,School_College=@School_College,Board_University=@Board_University,Year_of_Passing=@Year_of_Passing,Percentage_of_Marks=@Percentage_of_Marks,Division=@Division where Apply_id=@Apply_id and Qualification=@Qualification");
                    cmd.Parameters.AddWithValue("@Apply_id", ViewState["Applyid"].ToString());
                    cmd.Parameters.AddWithValue("@Qualification", "Any Other");
                    cmd.Parameters.AddWithValue("@Main_Subjects", txt_anyother_subject.Text);
                    cmd.Parameters.AddWithValue("@School_College", txt_anyother_school_collage_name.Text);
                    cmd.Parameters.AddWithValue("@Board_University", txt_anyother_board_university.Text);
                    cmd.Parameters.AddWithValue("@Year_of_Passing", txt_anyother_passing_year.Text);
                    cmd.Parameters.AddWithValue("@Percentage_of_Marks", txt_anyother_percentage_mark.Text);
                    cmd.Parameters.AddWithValue("@Division", ddl_anyother_division.Text);
                    cmd.ExecuteCommand(PayrollMy.con);



                }
            }


            #endregion
        }

        private bool get_WorkExperience_add_or_not()
        {
            DataTable dt = PayrollMy.dataTable("select * from dbo.[HR_Employee_Online_Apply_Work_Experiance] where  Apply_id='" + ViewState["Applyid"].ToString() + "'");
            if (dt.Rows.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


    }
}