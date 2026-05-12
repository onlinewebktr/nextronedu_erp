
using Microsoft.VisualBasic.FileIO;
using school_web.AppCode;
using System;
using System.Collections;
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
    public partial class upload_student_details : System.Web.UI.Page
    {
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

                        try
                        {
                        }
                        catch
                        {
                        }
                        ViewState["Userid"] = Session["Admin"].ToString();

                        ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());


                        ViewState["firm_id"] = Session["firm"].ToString();
                        ViewState["college_name"] = My.get_college_name();
                        string pagename_current = Path.GetFileName(Request.Path);
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];



                        mycode.bind_all_ddl_with_id(ddlsession, " select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int)");
                        ddlsession.SelectedValue = My.get_session_id();
                        mycode.bind_all_ddl_with_id(ddl_category, "select Category_Name,Category_Id from dbo.[Category_Details] order by Category_Name asc");
                        mycode.bind_all_ddl_with_id(ddl_subcategory, "select Sub_CategoryName,Sub_CategoryId from dbo.[Sub_Category_Details] order by Sub_CategoryName asc");
                        try
                        {
                            ddl_category.SelectedValue = "3";
                        }
                        catch (Exception ex)
                        {
                        }
                        try
                        {
                            ddl_subcategory.SelectedValue = "4";
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_Strength_Master");
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

         

        protected void btn_Submit_Click1(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_add"].ToString() == "1")
                {
                    if (ddl_category.SelectedItem.Text == "Select")
                    {
                        Alertme("Please select category", "warning");
                        ddl_category.Focus();
                        return;
                    }
                    if (ddl_subcategory.SelectedItem.Text == "Select")
                    {
                        Alertme("Please select sub-category", "warning");
                        ddl_subcategory.Focus();
                        return;
                    }
                    if (ddlsession.SelectedItem.Text == "Select")
                    {
                        Alertme("Please select sesstion", "warning");
                        ddlsession.Focus();
                        return;
                    }
                    if (FileUpload1.HasFile)
                    {
                        btn_final_submit.Visible = true;
                        ViewState["dupAdmiD"] = "0";
                        upload_excel_file();
                    }
                    else
                    {
                        Alertme("Please choose excel.csv file.", "warning");
                        ddlsession.Focus();
                        return;
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

        private void upload_excel_file()
        {
            string file = upload_csv_data();
            string csvid = My.auto_serialS("Upload_csvid");
            SqlDataAdapter ad = new SqlDataAdapter("Select * from excel_file", My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Student_Csv_file");
            DataTable dt = ds.Tables[0];
            DataRow dr = dt.NewRow();
            dr[1] = file;
            dr[2] = mycode.date();
            dr[3] = mycode.idate();
            dr[4] = csvid;
            dr[5] = "SUBMITTED";
            dt.Rows.Add(dr);
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
        }



        private bool check_wrap_or_not(string path)
        {
            try
            {
                DataTable tblReadCSV = new DataTable();
                tblReadCSV.Columns.Add("Form S.No.");
                tblReadCSV.Columns.Add("Admission date");
                tblReadCSV.Columns.Add("Admission in");
                tblReadCSV.Columns.Add("Transportation");
                tblReadCSV.Columns.Add("Boarding Type");
                tblReadCSV.Columns.Add("Section");
                tblReadCSV.Columns.Add("Admission no.");
                tblReadCSV.Columns.Add("Roll No.");
                tblReadCSV.Columns.Add("Class"); 
                tblReadCSV.Columns.Add("Student_First Name");
                tblReadCSV.Columns.Add("Student_Middle Name");
                tblReadCSV.Columns.Add("StudentLast Name");
                tblReadCSV.Columns.Add("Date of Birth");
                tblReadCSV.Columns.Add("Certificate No.");
                tblReadCSV.Columns.Add("Place of Birth");
                tblReadCSV.Columns.Add("Blood Group");
                tblReadCSV.Columns.Add("Aadhar No.");
                tblReadCSV.Columns.Add("Gender");
                tblReadCSV.Columns.Add("Religion");
                tblReadCSV.Columns.Add("Ration Type");


                //==============]
                tblReadCSV.Columns.Add("Category");
                tblReadCSV.Columns.Add("Certificate No");
                tblReadCSV.Columns.Add("Mother Tongue");
                tblReadCSV.Columns.Add("Is any Illness");
                tblReadCSV.Columns.Add("Illness Remark");
                tblReadCSV.Columns.Add("Prev.School");
                tblReadCSV.Columns.Add("Cast");
                tblReadCSV.Columns.Add("RTE Student");
                tblReadCSV.Columns.Add("Staff Ward");
                tblReadCSV.Columns.Add("Personal Identification Marks");
                tblReadCSV.Columns.Add("Father First  Name");
                tblReadCSV.Columns.Add("Father Middle Name");
                tblReadCSV.Columns.Add("Father Last Name");
                tblReadCSV.Columns.Add("Father Occupation");
                tblReadCSV.Columns.Add("Father.Qualif.");
                tblReadCSV.Columns.Add("Father Nationality");
                tblReadCSV.Columns.Add("Father Maritial Status");
                tblReadCSV.Columns.Add("Father Mobile No");
                tblReadCSV.Columns.Add("Father Email Id");
                tblReadCSV.Columns.Add("Guardian's Name");

                //==============
                tblReadCSV.Columns.Add("Parent Income");
                tblReadCSV.Columns.Add("Mother First Name");
                tblReadCSV.Columns.Add("Mother Middle Name");
                tblReadCSV.Columns.Add("Mother Last Name");
                tblReadCSV.Columns.Add("Mother Occupation");
                tblReadCSV.Columns.Add("Mother.Qualif.");
                tblReadCSV.Columns.Add("Mother Nationality");
                tblReadCSV.Columns.Add("Mother Maritial Status");
                tblReadCSV.Columns.Add("Mother Mobile No");
                tblReadCSV.Columns.Add("Mother Email Id");
                tblReadCSV.Columns.Add("Current C/o");
                tblReadCSV.Columns.Add("Current Mobile No");
                tblReadCSV.Columns.Add("Current City/Village");
                tblReadCSV.Columns.Add("Current District");
                tblReadCSV.Columns.Add("Current P.O.");
                tblReadCSV.Columns.Add("Current P.S");
                tblReadCSV.Columns.Add("Current Pin  Code");
                tblReadCSV.Columns.Add("Permanent C/o");
                tblReadCSV.Columns.Add("Permanent Mobile No");
                tblReadCSV.Columns.Add("Permanent City/Village");


                //==============
                tblReadCSV.Columns.Add("Permanent District");
                tblReadCSV.Columns.Add("Permanent P.O.");
                tblReadCSV.Columns.Add("Permanent P.S");
                tblReadCSV.Columns.Add("Permanent Pin  Code");
                tblReadCSV.Columns.Add("Account Holder Name");
                tblReadCSV.Columns.Add("Bank Name");
                tblReadCSV.Columns.Add("IFSC Code");
                tblReadCSV.Columns.Add("Branch Name");
                tblReadCSV.Columns.Add("Bank_acount_no");

                tblReadCSV.Columns.Add("Student Type");
                tblReadCSV.Columns.Add("Father WhatsApp No");
                tblReadCSV.Columns.Add("Mother WhatsApp No");
                TextFieldParser csvParser = new TextFieldParser(path);
                csvParser.Delimiters = new string[] { "," };
                csvParser.TrimWhiteSpace = true;
                csvParser.ReadLine();
                while (!(csvParser.EndOfData == true))
                {
                    tblReadCSV.Rows.Add(csvParser.ReadFields());
                }
                pnl_grid.Visible = true;

                lbl_total.Text = "Total Data :- " + tblReadCSV.Rows.Count.ToString();
                grvExcelData.DataSource = tblReadCSV;
                grvExcelData.DataBind();

                //==============
                string Admission_no = "";
                //string Registration_No = "";
                //string Admission_Date = "";
                //string Roll_No = "";
                //string Student_Name = "";
                //string Gender = "";
                //string Father_Name = "";
                //string Mother_Name = "";
                //string Student_Mobile = "";
                //string Student_Email_Id = "";
                //string Aadhar_No = "";
                //string DOB = "";
                //string Guardian_Name = "";
                //string Religion = "";
                //string City_or_Village = "";
                //string Post_Office = "";
                //string Polic_Station = "";
                //string District = "";
                //string State = "";
                //string Pin = "";
                //string Blood_Group = "";
                //string Father_Mobile = "";
                //string Father_Email_Id = "";
                for (int i = 0; i < grvExcelData.Rows.Count; i++)
                {
                    Admission_no = grvExcelData.Rows[i].Cells[5].Text;
                    #region check duplicate
                    string adno = Admission_no;
                    DataTable dt = My.dataTable(" select admissionserialnumber from admission_registor where admissionserialnumber='" + adno + "'  ");//and Session_id='" + ddlsession.SelectedValue + "'
                    if (dt.Rows.Count == 0)
                    {
                    }
                    else
                    {
                        grvExcelData.Rows[i].BackColor = System.Drawing.Color.Red;
                        Alertme("Sorry! Duplicate Admission No", "warning");
                    }
                    #endregion



                    //Registration_No = grvExcelData.Rows[i].Cells[0].Text;
                    //Admission_Date = grvExcelData.Rows[i].Cells[1].Text;

                    //Roll_No = grvExcelData.Rows[i].Cells[2].Text;
                    //Student_Name = grvExcelData.Rows[i].Cells[3].Text;
                    //Gender = grvExcelData.Rows[i].Cells[4].Text;
                    //Father_Name = grvExcelData.Rows[i].Cells[5].Text;
                    //Mother_Name = grvExcelData.Rows[i].Cells[6].Text;
                    //Student_Mobile = grvExcelData.Rows[i].Cells[7].Text;
                    //Student_Email_Id = grvExcelData.Rows[i].Cells[8].Text;
                    //Aadhar_No = grvExcelData.Rows[i].Cells[9].Text;
                    //DOB = grvExcelData.Rows[i].Cells[10].Text;
                    //Guardian_Name = grvExcelData.Rows[i].Cells[11].Text;
                    //Religion = grvExcelData.Rows[i].Cells[12].Text;
                    //City_or_Village = grvExcelData.Rows[i].Cells[13].Text;
                    //Post_Office = grvExcelData.Rows[i].Cells[14].Text;
                    //Polic_Station = grvExcelData.Rows[i].Cells[15].Text;
                    //District = grvExcelData.Rows[i].Cells[16].Text;
                    //State = grvExcelData.Rows[i].Cells[17].Text;
                    //Pin = grvExcelData.Rows[i].Cells[18].Text;
                    //Blood_Group = grvExcelData.Rows[i].Cells[19].Text;
                    //Father_Mobile = grvExcelData.Rows[i].Cells[20].Text;
                    //Father_Email_Id = grvExcelData.Rows[i].Cells[21].Text;

                    //if (Registration_No == "&nbsp;")
                    //{
                    //    grvExcelData.Rows[i].BackColor = System.Drawing.Color.Red;
                    //}
                    //if (Admission_Date == "&nbsp;")
                    //{
                    //    grvExcelData.Rows[i].BackColor = System.Drawing.Color.Red;
                    //}
                    //if (Roll_No == "&nbsp;")
                    //{
                    //    grvExcelData.Rows[i].BackColor = System.Drawing.Color.Red;
                    //}

                    //if (Student_Name == "&nbsp;")
                    //{
                    //    grvExcelData.Rows[i].BackColor = System.Drawing.Color.Red;
                    //}

                    //if (Gender == "&nbsp;")
                    //{
                    //    grvExcelData.Rows[i].BackColor = System.Drawing.Color.Red;
                    //}
                    //if (Father_Name == "&nbsp;")
                    //{
                    //    grvExcelData.Rows[i].BackColor = System.Drawing.Color.Red;
                    //}

                    ////==
                    //if (Student_Mobile == "&nbsp;")
                    //{
                    //    grvExcelData.Rows[i].BackColor = System.Drawing.Color.Red;
                    //}

                    //if (Aadhar_No == "&nbsp;")
                    //{
                    //    grvExcelData.Rows[i].BackColor = System.Drawing.Color.Red;
                    //}
                    //if (DOB == "&nbsp;")
                    //{
                    //    grvExcelData.Rows[i].BackColor = System.Drawing.Color.Red;
                    //}
                    ////==
                    //if (Guardian_Name == "&nbsp;")
                    //{
                    //    grvExcelData.Rows[i].BackColor = System.Drawing.Color.Red;
                    //}

                    //if (Religion == "&nbsp;")
                    //{
                    //    grvExcelData.Rows[i].BackColor = System.Drawing.Color.Red;
                    //}
                    //if (City_or_Village == "&nbsp;")
                    //{
                    //    grvExcelData.Rows[i].BackColor = System.Drawing.Color.Red;
                    //}
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }


        #region FileSave
        private string upload_csv_data()
        {
            DateTime dtm = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string date = dtm.ToString("ddMMyyyy");
            string time = dtm.ToString("hhmmss");
            String filerename = date + time;

            string dbfilePath = "";
            Boolean FileOK = false;
            Boolean FileSaved = false;
            int k = 0;

            Session["file1"] = FileUpload1.FileName;
            String FileExtension = Path.GetExtension(Session["file1"].ToString()).ToLower();
            Session["file"] = ("Upload_Student" + filerename + FileExtension);
            String[] allowedExtensions = { ".csv" };
            for (int i = 0; i < allowedExtensions.Length; i++)
            {
                k++;
                if (FileExtension == allowedExtensions[i])
                {

                    FileOK = true;
                    break;
                }
                else
                {
                }
            }

            if (FileOK)
            {
                try
                {
                    string path = (Server.MapPath("../Master_Img/Student")).ToString();
                    FileUpload1.SaveAs(path + "/" + Session["file"]);

                    if (check_wrap_or_not((path + "/" + Session["file"])))
                    {
                        FileSaved = true;
                    }
                    else
                    {
                        File.Delete((path + "/" + Session["file"]));
                        FileSaved = false;
                    }
                }
                catch (Exception ex)
                {
                    FileSaved = false;

                    Alertme(ex.ToString(), "warning");
                }
            }
            else
            {
                dbfilePath = "Choose only csv File";
                return dbfilePath;
            }
            if (FileSaved)
            {

                string fileName = Path.GetFileName(Session["file"].ToString());
                dbfilePath = @"/Master_Img/Student/" + fileName;
                Session["fileName"] = fileName;
            }
            return dbfilePath;
        }
        #endregion

        protected void btn_final_submit_Click(object sender, EventArgs e)
        {
            if (ddl_category.SelectedItem.Text == "Select")
            {
                Alertme("Please select category", "warning");
            }
            else if (ddl_subcategory.SelectedItem.Text == "Select")
            {
                Alertme("Please select sub-catogery", "warning");
            }
            else if (ddlsession.SelectedItem.Text == "Select")
            {
                Alertme("Please select  session", "warning");
            }

            else
            {
                try
                {
                    string qry = "";

                    string Session = ddlsession.SelectedValue;
                    string class1 = "";
                    string FormNo = "";
                    string Admission_Date = "";
                    string AdmissionIn = "";
                    string Transportation = "";
                    string BoardingType = "";
                    string Section = "";
                    string AdmissionNo = "";
                    string Roll_No = "";
                    string AcademicYear = "";
                    string Student_FName = "";
                    string Student_MName = "";
                    string Student_LName = "";
                    string Dateof_borth = "";
                    string CertificateNo = "";
                    string Placeof_birth = "";
                    string BloodGroup = "";
                    string AadharNo = "";
                    string Gender = "";
                    string Religion = "";
                    string RationType = "";

                    //=======================
                    string Category = "";
                    string CertificateNo_category = "";
                    string MotherToung = "";
                    string IsAnyIllnes = "";
                    string IllnessRemark = "";
                    string PreSchool = "";
                    string Cast = "";
                    string RTEStudent = "";
                    string StaffWard = "";
                    string Personal_identificationMark = "";
                    string Father_F_Name = "";
                    string Father_M_Name = "";
                    string Father_L_Name = "";
                    string Father_Occupation = "";
                    string Father_Qualification = "";
                    string Father_Nationalty = "";
                    string Father_Marrital_Status = "";
                    string Father_mobile_no = "";
                    string Father_email_id = "";
                    string GuardianName = "";


                    //=====================
                    string ParantIncome = "";
                    string Mother_F_Name = "";
                    string Mother_M_Name = "";
                    string Mother_L_Name = "";
                    string Mother_Occupation = "";
                    string Mother_Qualification = "";
                    string Mother_Nationality = "";
                    string Mother_Maritial_Status = "";
                    string Mother_Mobile = "";
                    string Mother_Email = "";
                    string Current_co = "";
                    string Current_mobile = "";
                    string Current_city = "";
                    string Current_district = "";
                    string Current_po = "";
                    string Current_ps = "";
                    string Current_pin = "";
                    string Permanant_co = "";
                    string Permanant_mobile = "";
                    string Permanant_city = "";

                    //==========
                    string Permanant_district = "";
                    string Permanant_po = "";
                    string Permanant_ps = "";
                    string Permanant_pin = "";
                    string Acc_holder_name = "";
                    string Bank_name = "";
                    string Ifsc_code = "";
                    string Branch_name = "";
                    string Bank_acount_no = "";
                    string Student_type = "";
                    string Father_whatsApp_no = "";
                    string Mother_whatsApp_no = "";

                    DateTime date = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                    for (int i = 0; i < grvExcelData.Rows.Count; i++)
                    {
                        FormNo = grvExcelData.Rows[i].Cells[0].Text;
                        Admission_Date = grvExcelData.Rows[i].Cells[1].Text;
                        AdmissionIn = grvExcelData.Rows[i].Cells[2].Text;
                        Transportation = grvExcelData.Rows[i].Cells[3].Text;
                        BoardingType = grvExcelData.Rows[i].Cells[4].Text;
                        Section = grvExcelData.Rows[i].Cells[5].Text;
                        AdmissionNo = grvExcelData.Rows[i].Cells[6].Text;
                        Roll_No = grvExcelData.Rows[i].Cells[7].Text;
                        class1 = grvExcelData.Rows[i].Cells[8].Text;
                        Student_FName = grvExcelData.Rows[i].Cells[9].Text;
                        Student_MName = grvExcelData.Rows[i].Cells[10].Text;
                        Student_LName = grvExcelData.Rows[i].Cells[11].Text;
                        Dateof_borth = grvExcelData.Rows[i].Cells[12].Text;
                        CertificateNo = grvExcelData.Rows[i].Cells[13].Text;
                        Placeof_birth = grvExcelData.Rows[i].Cells[14].Text;
                        BloodGroup = grvExcelData.Rows[i].Cells[15].Text;
                        AadharNo = grvExcelData.Rows[i].Cells[16].Text;
                        Gender = grvExcelData.Rows[i].Cells[17].Text;
                        Religion = grvExcelData.Rows[i].Cells[18].Text;
                        RationType = grvExcelData.Rows[i].Cells[19].Text;
                        Category = grvExcelData.Rows[i].Cells[20].Text;
                        CertificateNo_category = grvExcelData.Rows[i].Cells[21].Text;
                        MotherToung = grvExcelData.Rows[i].Cells[22].Text;
                        IsAnyIllnes = grvExcelData.Rows[i].Cells[23].Text;
                        IllnessRemark = grvExcelData.Rows[i].Cells[24].Text;
                        PreSchool = grvExcelData.Rows[i].Cells[25].Text;
                        Cast = grvExcelData.Rows[i].Cells[26].Text;
                        RTEStudent = grvExcelData.Rows[i].Cells[27].Text;
                        StaffWard = grvExcelData.Rows[i].Cells[28].Text;
                        Personal_identificationMark = grvExcelData.Rows[i].Cells[29].Text;
                        Father_F_Name = grvExcelData.Rows[i].Cells[30].Text;
                        Father_M_Name = grvExcelData.Rows[i].Cells[31].Text;
                        Father_L_Name = grvExcelData.Rows[i].Cells[32].Text;
                        Father_Occupation = grvExcelData.Rows[i].Cells[33].Text;
                        Father_Qualification = grvExcelData.Rows[i].Cells[34].Text;
                        Father_Nationalty = grvExcelData.Rows[i].Cells[35].Text;
                        Father_Marrital_Status = grvExcelData.Rows[i].Cells[36].Text;
                        Father_mobile_no = grvExcelData.Rows[i].Cells[37].Text;
                        Father_email_id = grvExcelData.Rows[i].Cells[38].Text;
                        GuardianName = grvExcelData.Rows[i].Cells[39].Text;
                        ParantIncome = grvExcelData.Rows[i].Cells[40].Text;
                        Mother_F_Name = grvExcelData.Rows[i].Cells[41].Text; 
                        Mother_M_Name = grvExcelData.Rows[i].Cells[42].Text;
                        Mother_L_Name = grvExcelData.Rows[i].Cells[43].Text;
                        Mother_Occupation = grvExcelData.Rows[i].Cells[44].Text;
                        Mother_Qualification = grvExcelData.Rows[i].Cells[45].Text;
                        Mother_Nationality = grvExcelData.Rows[i].Cells[46].Text;
                        Mother_Maritial_Status = grvExcelData.Rows[i].Cells[47].Text;
                        Mother_Mobile = grvExcelData.Rows[i].Cells[48].Text;
                        Mother_Email = grvExcelData.Rows[i].Cells[49].Text;
                        Current_co = grvExcelData.Rows[i].Cells[50].Text;
                        Current_mobile = grvExcelData.Rows[i].Cells[51].Text;
                        Current_city = grvExcelData.Rows[i].Cells[52].Text;
                        Current_district = grvExcelData.Rows[i].Cells[53].Text;
                        Current_po = grvExcelData.Rows[i].Cells[54].Text;
                        Current_ps = grvExcelData.Rows[i].Cells[55].Text;
                        Current_pin = grvExcelData.Rows[i].Cells[56].Text;
                        Permanant_co = grvExcelData.Rows[i].Cells[57].Text;
                        Permanant_mobile = grvExcelData.Rows[i].Cells[58].Text;
                        Permanant_city = grvExcelData.Rows[i].Cells[59].Text;
                        Permanant_district = grvExcelData.Rows[i].Cells[60].Text;
                        Permanant_po = grvExcelData.Rows[i].Cells[61].Text;
                        Permanant_ps = grvExcelData.Rows[i].Cells[62].Text;
                        Permanant_pin = grvExcelData.Rows[i].Cells[63].Text;
                        Acc_holder_name = grvExcelData.Rows[i].Cells[64].Text;
                        Bank_name = grvExcelData.Rows[i].Cells[65].Text;
                        Ifsc_code = grvExcelData.Rows[i].Cells[66].Text;
                        Branch_name = grvExcelData.Rows[i].Cells[67].Text;
                        Bank_acount_no = grvExcelData.Rows[i].Cells[68].Text;
                        Student_type = grvExcelData.Rows[i].Cells[69].Text;
                        Father_whatsApp_no = grvExcelData.Rows[i].Cells[70].Text;
                        Mother_whatsApp_no = grvExcelData.Rows[i].Cells[71].Text;
                        string classid = mycode.get_classid(class1);
                        if (classid == "0")
                        { }
                        else
                        {
                            string studentmiddname = "";
                            string fathermiddname = "";
                            string mothermiddname = "";
                            SqlDataAdapter ad = new SqlDataAdapter("select * from admission_registor where admissionserialnumber='" + AdmissionNo + "' ", My.conn);//and   Session_id='" + ddlsession.SelectedValue + "'
                            DataSet ds = new DataSet();
                            ad.Fill(ds, "admission_registor");
                            DataTable dt = ds.Tables[0];
                            if (dt.Rows.Count == 0)
                            {
                                DataRow dr = dt.NewRow();
                                if (Student_type.ToUpper() == "NEW")
                                {
                                    dr["Transfer_Status"] = "New";
                                }
                                else
                                {
                                    dr["Transfer_Status"] = "NT";
                                }
                                dr["User_id"] = ViewState["Userid"].ToString();
                                dr["class"] = class1;

                                if (FormNo == "")
                                {

                                    dr["formserialnumber"] = "";
                                }
                                else if (FormNo == "&nbsp;")
                                {

                                    dr["formserialnumber"] = "";

                                }

                                dr["formserialnumber"] = FormNo;


                                dr["admissionserialnumber"] = AdmissionNo;



                                if (Section == "&nbsp;")
                                {
                                    Section = "";
                                }
                                if (Section == "")
                                {
                                    Section = "";
                                }
                                else
                                {

                                }
                                dr["Section"] = Section;

                                if (Roll_No == "&nbsp;")
                                {
                                    Roll_No = "0";
                                }
                                if (Roll_No == "")
                                {
                                    Roll_No = "0";
                                }
                                else
                                {

                                }

                                dr["rollnumber"] = Roll_No;



                                dr["session"] = ddlsession.SelectedItem.Text;

                                if (Admission_Date == "&nbsp;")
                                {
                                    dr["dateofadmission"] = "";
                                }
                                if (Admission_Date == "")
                                {
                                    dr["dateofadmission"] = "";
                                }
                                else
                                {
                                    dr["dateofadmission"] = Admission_Date;
                                }


                                dr["Category_id"] = ddl_category.SelectedValue;
                                dr["SubCategory_id"] = ddl_subcategory.SelectedValue;
                                if (Gender == "")
                                {
                                    Gender = "";
                                }
                                else if (Gender == "&nbsp;")
                                {
                                    Gender = "";
                                }

                                dr["gender"] = Gender.ToUpper(); 
                                if (Personal_identificationMark == "")
                                {
                                    Personal_identificationMark = "";
                                }
                                else if (Personal_identificationMark == "&nbsp;")
                                {
                                    Personal_identificationMark = "";
                                }
                                dr["identifacationmark"] = Personal_identificationMark; 

                                if (PreSchool == "")
                                {
                                    PreSchool = "";
                                }
                                else if (PreSchool == "&nbsp;")
                                {
                                    PreSchool = "";
                                }
                                dr["currentschool"] = PreSchool;
                                if (Dateof_borth == "")
                                {
                                    Dateof_borth = "";
                                }
                                else if (Dateof_borth == "&nbsp;")
                                {
                                    Dateof_borth = "";
                                }
                                else if (Dateof_borth == "Not Available")
                                {
                                    Dateof_borth = "";
                                }
                                dr["dob"] = Dateof_borth;

                                if (Father_Qualification == "&nbsp;")
                                {
                                    Father_Qualification = "";
                                }
                                else if (Father_Qualification == "")
                                {
                                    Father_Qualification = "";
                                }
                                dr["fatherqualification"] = Father_Qualification;

                                if (Mother_Qualification == "&nbsp;")
                                {
                                    Mother_Qualification = "";
                                }
                                else if (Mother_Qualification == "")
                                {
                                    Mother_Qualification = "";
                                }
                                dr["motherqualifiaction"] = Mother_Qualification;

                                if (GuardianName == "&nbsp;")
                                {
                                    GuardianName = "";
                                }
                                else if (GuardianName == "")
                                {
                                    GuardianName = "";
                                }
                                dr["guardianname"] = GuardianName;


                                dr["relation"] = "Not Available";

                                if (Father_Occupation == "&nbsp;")
                                {
                                    Father_Occupation = "";
                                }
                                else if (Father_Occupation == "")
                                {
                                    Father_Occupation = "";
                                }

                                dr["occuption"] = Father_Occupation;

                                if (Religion == "&nbsp;")
                                {
                                    Religion = "";
                                }
                                else if (Religion == "")
                                {
                                    Religion = "";
                                }


                                dr["religion"] = Religion;

                                if (Category == "&nbsp;")
                                {
                                    Category = "";
                                }
                                else if (Category == "")
                                {
                                    Category = "";
                                }

                                dr["cast"] = Category;




                                if (ParantIncome == "&nbsp;")
                                {
                                    ParantIncome = "";
                                }
                                else if (ParantIncome == "")
                                {
                                    ParantIncome = "";
                                }
                                dr["parentincome"] = ParantIncome;

                                if (Current_co == "&nbsp;")
                                {
                                    Current_co = "";
                                }
                                else if (Current_co == "")
                                {
                                    Current_co = "";
                                }

                                dr["careof"] = Current_co;

                                if (Current_city == "&nbsp;")
                                {
                                    Current_city = "";
                                }
                                else if (Current_city == "")
                                {
                                    Current_city = "";
                                }
                                dr["city"] = Current_city;



                                if (Current_po == "&nbsp;")
                                {
                                    Current_po = "";
                                }
                                else if (Current_po == "")
                                {
                                    Current_po = "";
                                }

                                dr["postoffice"] = Current_po;


                                if (Current_ps == "&nbsp;")
                                {
                                    Current_ps = "";
                                }
                                else if (Current_ps == "")
                                {
                                    Current_ps = "";
                                }
                                dr["policestation"] = Current_ps;

                                if (Current_district == "&nbsp;")
                                {
                                    Current_district = "";
                                }
                                else if (Current_district == "")
                                {
                                    Current_district = "";
                                }

                                dr["district"] = Current_district;
                                //dr["state"] = ddl_temp_state.Text;


                                if (Current_pin == "&nbsp;")
                                {
                                    Current_pin = "";
                                }
                                else if (Current_pin == "")
                                {
                                    Current_pin = "";
                                }
                                dr["pin"] = Current_pin;


                                if (Current_mobile == "&nbsp;")
                                {
                                    Current_mobile = "";
                                }
                                else if (Current_mobile == "")
                                {
                                    Current_mobile = "";
                                }
                                dr["mobilenumber"] = Current_mobile;

                                if (Permanant_co == "&nbsp;")
                                {
                                    Permanant_co = "";
                                }
                                else if (Permanant_co == "")
                                {
                                    Permanant_co = "";
                                }
                                dr["careof_permanent"] = Permanant_co;

                                if (Permanant_city == "&nbsp;")
                                {
                                    Permanant_city = "";
                                }
                                else if (Permanant_city == "")
                                {
                                    Permanant_city = "";
                                }

                                dr["city_permanent"] = Permanant_city;

                                if (Permanant_po == "&nbsp;")
                                {
                                    Permanant_po = "";
                                }
                                else if (Permanant_po == "")
                                {
                                    Permanant_po = "";
                                }
                                dr["postoffice_permanent"] = Permanant_po;

                                if (Permanant_ps == "&nbsp;")
                                {
                                    Permanant_ps = "";
                                }
                                else if (Permanant_ps == "")
                                {
                                    Permanant_ps = "";
                                }
                                dr["policestation_permanent"] = Permanant_ps;


                                if (Permanant_district == "&nbsp;")
                                {
                                    Permanant_district = "";
                                }
                                else if (Permanant_district == "")
                                {
                                    Permanant_district = "";
                                }
                                dr["district_permanent"] = Permanant_district;
                                //dr["state_permanent"] = ddl_par_state.Text;
                                if (Permanant_pin == "&nbsp;")
                                {
                                    Permanant_pin = "";
                                }
                                else if (Permanant_pin == "")
                                {
                                    Permanant_pin = "";
                                }


                                if (Father_whatsApp_no == "&nbsp;")
                                {
                                    Father_whatsApp_no = "";
                                }
                                dr["Father_whatsApp_no"] = Father_whatsApp_no;


                                if (Mother_whatsApp_no == "&nbsp;")
                                {
                                    Mother_whatsApp_no = "";
                                }
                                dr["Mother_whatsApp_no"] = Mother_whatsApp_no;

                                dr["pincode"] = Permanant_pin;
                                dr["payment_status"] = "Unpaid";
                                if (AdmissionIn.ToUpper() == "HOSTEL")
                                {
                                    dr["hosteltaken"] = "Yes"; 
                                    dr["transportationtaken"] = "No";
                                    dr["Transportation_Id"] = "0";
                                    dr["Transportationpath"] = ""; 
                                }
                                if (AdmissionIn.ToUpper() == "YES")
                                {
                                    dr["hosteltaken"] = "Yes"; 
                                    dr["transportationtaken"] = "No";
                                    dr["Transportation_Id"] = "0";
                                    dr["Transportationpath"] = ""; 
                                }
                                else
                                { 
                                    dr["hosteltaken"] = "No";
                                    if (Transportation.ToUpper() == "YES")
                                    {
                                        dr["transportationtaken"] = "Yes"; 
                                    }
                                    else
                                    {
                                        dr["transportationtaken"] = "No"; 
                                    }
                                    dr["Transportation_Id"] = "0";
                                    dr["Transportationpath"] = "";
                                }

                                if (AadharNo == "&nbsp;")
                                {
                                    AadharNo = "";
                                }
                                else if (AadharNo == "")
                                {
                                    AadharNo = "";
                                }
                                dr["aadharno"] = AadharNo;

                                if (RTEStudent == "&nbsp;")
                                {
                                    RTEStudent = "No";
                                }
                                else if (RTEStudent == "")
                                {
                                    RTEStudent = "No";
                                }
                                dr["RTE"] = RTEStudent;

                                dr["admission_idate"] = mycode.ConvertStringToiDateup(Admission_Date);

                                if (StaffWard == "&nbsp;")
                                {
                                    StaffWard = "No";
                                }
                                else if (StaffWard == "")
                                {
                                    StaffWard = "No";
                                }
                                dr["staff_ward"] = StaffWard;

                                if (Cast == "&nbsp;")
                                {
                                    Cast = "N/A";
                                }
                                else if (Cast == "")
                                {
                                    Cast = "N/A";
                                }
                                dr["jati"] = Cast;

                                if (Permanant_mobile == "&nbsp;")
                                {
                                    Permanant_mobile = "";
                                }
                                else if (Permanant_mobile == "")
                                {
                                    Permanant_mobile = "";
                                }
                                dr["mob2"] = Permanant_mobile;
                                dr["Hostel_id"] = "0";
                                dr["Session_id"] = ddlsession.SelectedValue;
                                dr["Class_id"] = classid;
                                dr["Is_TC_Taken"] = false;
                                dr["Student_id"] = AdmissionNo;
                                if (Father_email_id == "&nbsp;")
                                {
                                    Father_email_id = "";
                                }
                                else if (Father_email_id == "")
                                {
                                    Father_email_id = "";
                                }
                                dr["email_id"] = Father_email_id;

                                if (CertificateNo == "&nbsp;")
                                {
                                    CertificateNo = "";
                                }
                                else if (CertificateNo == "")
                                {
                                    CertificateNo = "";
                                }
                                dr["birth_certificate_number"] = CertificateNo;


                                if (Placeof_birth == "&nbsp;")
                                {
                                    Placeof_birth = "";
                                }
                                else if (Placeof_birth == "")
                                {
                                    Placeof_birth = "";
                                }

                                dr["place_of_birth"] = Placeof_birth;

                                if (BloodGroup == "&nbsp;")
                                {
                                    BloodGroup = "";
                                }
                                else if (BloodGroup == "")
                                {
                                    BloodGroup = "";
                                }

                                dr["blood_group"] = BloodGroup;
                                if (CertificateNo_category == "&nbsp;")
                                {
                                    CertificateNo_category = "";
                                }
                                else if (BloodGroup == "")
                                {
                                    CertificateNo_category = "";
                                }
                                dr["cast_certificate_no"] = CertificateNo_category;


                                if (MotherToung == "&nbsp;")
                                {
                                    MotherToung = "Hindi";
                                }
                                else if (MotherToung == "")
                                {
                                    MotherToung = "Hindi";
                                }

                                dr["student_mother_tounge"] = MotherToung;

                                if (RationType == "&nbsp;")
                                {
                                    RationType = "";
                                }
                                else if (RationType == "")
                                {
                                    RationType = "";
                                }
                                dr["ration_type"] = RationType;

                                if (IsAnyIllnes == "&nbsp;")
                                {
                                    IsAnyIllnes = "No";
                                }
                                else if (IsAnyIllnes == "")
                                {
                                    IsAnyIllnes = "No";
                                }

                                dr["is_illness"] = IsAnyIllnes;
                                if (Father_Nationalty == "&nbsp;")
                                {
                                    Father_Nationalty = "Indian";
                                }
                                else if (Father_Nationalty == "")
                                {
                                    Father_Nationalty = "Indian";
                                }
                                dr["f_nationality"] = Father_Nationalty;

                                if (Father_Marrital_Status == "&nbsp;")
                                {
                                    Father_Marrital_Status = "";
                                }
                                else if (Father_Marrital_Status == "")
                                {
                                    Father_Marrital_Status = "";
                                }
                                dr["f_marrital_statue"] = Father_Marrital_Status;
                                if (Mother_Maritial_Status == "&nbsp;")
                                {
                                    Mother_Maritial_Status = "";
                                }
                                else if (Mother_Maritial_Status == "")
                                {
                                    Mother_Maritial_Status = "";
                                }
                                dr["m_marrital_statue"] = Mother_Maritial_Status;

                                if (Mother_Nationality == "&nbsp;")
                                {
                                    Mother_Nationality = "Indian";
                                }
                                else if (Mother_Nationality == "")
                                {
                                    Mother_Nationality = "Indian";
                                }

                                dr["m_nationality"] = Mother_Nationality;

                                if (Mother_Occupation == "&nbsp;")
                                {
                                    Mother_Occupation = "";
                                }
                                else if (Mother_Occupation == "")
                                {
                                    Mother_Occupation = "";
                                }
                                dr["m_occupation"] = Mother_Occupation;

                                if (IllnessRemark == "&nbsp;")
                                {
                                    IllnessRemark = "";
                                }
                                else if (IllnessRemark == "")
                                {
                                    IllnessRemark = "";
                                }
                                dr["illness_remark"] = IllnessRemark;

                                if (Father_mobile_no == "&nbsp;")
                                {
                                    Father_mobile_no = "";
                                }
                                else if (Father_mobile_no == "")
                                {
                                    Father_mobile_no = "";
                                }

                                dr["father_mob"] = Father_mobile_no;

                                if (Mother_Mobile == "&nbsp;")
                                {
                                    Mother_Mobile = "";
                                }
                                else if (Mother_Mobile == "")
                                {
                                    Mother_Mobile = "";
                                }

                                dr["mother_mob"] = Mother_Mobile;
                                if (Mother_Email == "&nbsp;")
                                {
                                    Mother_Email = "";
                                }
                                else if (Mother_Email == "")
                                {
                                    Mother_Email = "";
                                }

                                dr["mother_email"] = Mother_Email;

                                if (Acc_holder_name == "&nbsp;")
                                {
                                    Acc_holder_name = "";
                                }
                                else if (Acc_holder_name == "")
                                {
                                    Acc_holder_name = "";
                                }

                                dr["Account_Holder_name"] = Acc_holder_name;
                                if (Bank_name == "&nbsp;")
                                {
                                    Bank_name = "";
                                }
                                else if (Bank_name == "")
                                {
                                    Bank_name = "";
                                }


                                dr["Bnk_Name"] = Bank_name;
                                if (Ifsc_code == "&nbsp;")
                                {
                                    Ifsc_code = "";
                                }
                                else if (Ifsc_code == "")
                                {
                                    Ifsc_code = "";
                                }
                                dr["IFSC_Code"] = Ifsc_code;

                                if (Branch_name == "&nbsp;")
                                {
                                    Branch_name = "";
                                }
                                else if (Branch_name == "")
                                {
                                    Branch_name = "";
                                }

                                dr["Branch_Name"] = Branch_name;
                                if (Bank_acount_no == "&nbsp;")
                                {
                                    Bank_acount_no = "";
                                }
                                else if (Bank_acount_no == "")
                                {
                                    Bank_acount_no = "";
                                }

                                dr["Bank_acount_no"] = Bank_acount_no;

                                dr["Course_Type"] = "";
                                dr["Academic_Sem_or_Year"] = "";
                                dr["Academic_Sem_or_Year_id"] = "0";

                                dr["Student_Name_First"] = Student_FName;

                                if (Student_MName == "&nbsp;")
                                {
                                    studentmiddname = "";
                                    dr["Student_Middle_Name"] = "";
                                }

                                else if (Student_MName == "")
                                {
                                    studentmiddname = "";
                                    dr["Student_Middle_Name"] = "";
                                }
                                else
                                {
                                    dr["Student_Middle_Name"] = Student_MName;
                                    studentmiddname = Student_MName;
                                }

                                if (Student_LName == "")
                                {

                                    Student_LName = "";
                                }
                                else if (Student_LName == "&nbsp;")
                                {

                                    Student_LName = "";

                                }

                                dr["Student_Name_Last"] = Student_LName;

                                dr["studentname"] = Student_FName + " " + studentmiddname + " " + Student_LName;

                                dr["Father_Name_First"] = Father_F_Name;

                                if (Father_M_Name == "&nbsp;")
                                {
                                    fathermiddname = "";
                                    dr["Father_Name_Middle"] = "";
                                }

                                else if (Father_M_Name == "")
                                {
                                    fathermiddname = "";
                                    dr["Father_Name_Middle"] = "";
                                }
                                else
                                {
                                    dr["Father_Name_Middle"] = Father_M_Name;
                                    fathermiddname = Father_M_Name;
                                }


                                if (Father_L_Name == "&nbsp;")
                                {
                                    Father_L_Name = "";
                                }
                                else if (Father_L_Name == "")
                                {
                                    Father_L_Name = "";
                                }
                                dr["Father_Name_Last"] = Father_L_Name;
                                dr["fathername"] = Father_F_Name + " " + fathermiddname + " " + Father_L_Name;
                                dr["Mother_Name_First"] = Mother_F_Name;
                                if (Mother_M_Name == "&nbsp;")
                                {
                                    dr["Mother_Name_Middle"] = "";
                                }
                                else if (Father_M_Name == "")
                                {
                                    dr["Mother_Name_Middle"] = "";
                                }
                                else
                                {
                                    dr["Mother_Name_Middle"] = Mother_M_Name;
                                    mothermiddname = Mother_M_Name;
                                }
                                if (Mother_L_Name == "&nbsp;")
                                {
                                    Mother_L_Name = "";
                                }
                                else if (Mother_L_Name == "&nbsp;")
                                {
                                    Mother_L_Name = "";

                                }

                                dr["Mother_Name_Last"] = Mother_L_Name;

                                dr["mothername"] = Mother_F_Name + " " + mothermiddname + " " + Mother_L_Name;

                                if (BoardingType == "")
                                {
                                    dr["is_applied_dayboarding"] = 0;
                                    dr["day_bording_with_lunch"] = 0;
                                }
                                else if (BoardingType == "&nbsp;")
                                {
                                    dr["is_applied_dayboarding"] = 0;
                                    dr["day_bording_with_lunch"] = 0;
                                }
                                else if (BoardingType.ToUpper() == "YES")
                                {
                                    dr["is_applied_dayboarding"] = 1;
                                    dr["day_bording_with_lunch"] = 1;
                                }
                                else
                                {
                                    dr["is_applied_dayboarding"] = 0;
                                    dr["day_bording_with_lunch"] = 0;

                                }

                                if (Personal_identificationMark == "&nbsp;")
                                {
                                    Personal_identificationMark = "";
                                }
                                else if (Personal_identificationMark == "")
                                {
                                    Personal_identificationMark = "";
                                }

                                dr["Personal_Identymarks"] = Personal_identificationMark;
                                dr["Branch_id"] = ViewState["branchid"].ToString();
                                dr["StudentStatus"] = "AV";
                                dr["Pwd"] = My.create_random_no_otp();

                                dr["College_School_Name"] = ViewState["college_name"].ToString();
                                dr["Verification_Istatus"] = "0";

                                dr["Status"] = "1";
                                dr["Edit_Istatus"] = "0";
                                if (Admission_Date == "&nbsp;")
                                {
                                    Admission_Date = "";
                                }
                                else if (Admission_Date == "")
                                {
                                    Admission_Date = "";
                                }
                                dr["Old_Admission_Date"] = Admission_Date;
                                dr["OLd_Admission_Idate"] = mycode.ConvertStringToiDate(Admission_Date);

                                dr["Created_by"] = ViewState["Userid"].ToString();
                                dr["Created_date"] = mycode.date();
                                dr["Created_time"] = mycode.time();
                                dr["Created_idate"] = mycode.idate();
                                dt.Rows.Add(dr);
                            }
                            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                            ad.Update(dt);
                        }
                    }
                    mycode.executequery("UPDATE admission_registor SET studentname = REPLACE(studentname, '&nbsp;', ''); UPDATE admission_registor SET mothername = REPLACE(mothername, '&nbsp;', ''); UPDATE admission_registor SET fathername = REPLACE(fathername, '&nbsp;', '');UPDATE admission_registor SET guardianname = REPLACE(guardianname, '&nbsp;', '');UPDATE admission_registor SET motherqualifiaction = REPLACE(motherqualifiaction, '&nbsp;', '');UPDATE admission_registor SET fatherqualification = REPLACE(fatherqualification, '&nbsp;', ''); UPDATE admission_registor SET aadharno = REPLACE(aadharno, ',','');UPDATE admission_registor SET mother_email = REPLACE(mother_email, '&nbsp;', '');UPDATE admission_registor SET Student_Name_First = REPLACE(Student_Name_First, '&nbsp;', '');UPDATE admission_registor SET Student_Middle_Name = REPLACE(Student_Middle_Name, '&nbsp;', '');UPDATE admission_registor SET Student_Name_Last = REPLACE(Student_Name_Last, '&nbsp;', '');UPDATE admission_registor SET Father_Name_First = REPLACE(Father_Name_First, '&nbsp;', '');UPDATE admission_registor SET Father_Name_Middle = REPLACE(Father_Name_Middle, '&nbsp;', '');UPDATE admission_registor SET Father_Name_Last = REPLACE(Father_Name_Last, '&nbsp;', '');UPDATE admission_registor SET Mother_Name_First = REPLACE(Mother_Name_First, '&nbsp;', '');UPDATE admission_registor SET Mother_Name_Middle = REPLACE(Mother_Name_Middle, '&nbsp;', '');UPDATE admission_registor SET Mother_Name_Last = REPLACE(Mother_Name_Last, '&nbsp;', '');UPDATE admission_registor SET city = REPLACE(city, '&nbsp;', '');UPDATE admission_registor SET postoffice = REPLACE(postoffice, '&nbsp;', '');UPDATE admission_registor SET policestation = REPLACE(policestation, '&nbsp;', '');UPDATE admission_registor SET district = REPLACE(district, '&nbsp;', '');UPDATE admission_registor SET mothername = REPLACE(mothername, '&nbsp;', '');UPDATE admission_registor SET fatherqualification = REPLACE(fatherqualification, '&nbsp;', '');UPDATE admission_registor SET district = REPLACE(district, '&nbsp;', '');UPDATE admission_registor SET mothername = REPLACE(mothername, '&nbsp;', '')");
                    Alertme("Student has been uploaded successfully.", "success");
                    btn_final_submit.Visible = false;
                }
                catch (Exception ex)
                {
                    My.Save_Exception(ex.ToString());
                }
            } 
        }


        private bool check_duplicate(string adno)
        {
            DataTable dt = My.dataTable(" select admissionserialnumber from admission_registor where admissionserialnumber='" + adno + "'");
            if (dt.Rows.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void ddl_category_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                mycode.bind_all_ddl_with_id(ddl_subcategory, "select Sub_CategoryName,Sub_CategoryId from dbo.[Sub_Category_Details] where Category_Id='" + ddl_category.SelectedValue + "' order by Sub_CategoryName asc");
            }
            catch (Exception ex)
            {
            }
        }
    }
}