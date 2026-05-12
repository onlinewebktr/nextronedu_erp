using Microsoft.VisualBasic.FileIO;
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
    public partial class bulk_student_uploads : System.Web.UI.Page
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
                tblReadCSV.Columns.Add("Student_type");
                tblReadCSV.Columns.Add("Hostel_taken");
                tblReadCSV.Columns.Add("Transport_taken");
                tblReadCSV.Columns.Add("Admission_date");
                tblReadCSV.Columns.Add("Admission_no");
                tblReadCSV.Columns.Add("Student_name");
                tblReadCSV.Columns.Add("Class");
                tblReadCSV.Columns.Add("Section");
                tblReadCSV.Columns.Add("Roll_no");
                tblReadCSV.Columns.Add("Date_of_birth");
                tblReadCSV.Columns.Add("Aadhar_no");
                tblReadCSV.Columns.Add("Gender");
                tblReadCSV.Columns.Add("Religion");
                tblReadCSV.Columns.Add("Father_name");
                tblReadCSV.Columns.Add("Mother_name");
                tblReadCSV.Columns.Add("Mobile_no");
                tblReadCSV.Columns.Add("WhatsApp_no");
                tblReadCSV.Columns.Add("Monther_mobile_no");
                tblReadCSV.Columns.Add("Address");
                tblReadCSV.Columns.Add("Reg_no");
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
                for (int i = 0; i < grvExcelData.Rows.Count; i++)
                {
                    Admission_no = grvExcelData.Rows[i].Cells[4].Text;
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
                    string Student_type = "";
                    string Hostel_taken = "";
                    string Transport_taken = "";
                    string Admission_date = "";
                    string Admission_no = "";
                    string Student_name = "";
                    string Class_name = "";
                    string Section = "";
                    string Roll_no = "";
                    string Date_of_birth = "";
                    string Aadhar_no = "";
                    string Gender = "";
                    string Religion = "";
                    string Father_name = "";
                    string Mother_name = "";
                    string Mobile_no = "";
                    string WhatsApp_no = "";
                    string Mother_mobile_no = "";
                    string Address = "";
                    string Reg_no = "";
                    string excelEntryId = My.with_excel_name("excel");
                    DateTime date = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                    for (int i = 0; i < grvExcelData.Rows.Count; i++)
                    {
                        Student_type = grvExcelData.Rows[i].Cells[0].Text;
                        Hostel_taken = grvExcelData.Rows[i].Cells[1].Text;
                        Transport_taken = grvExcelData.Rows[i].Cells[2].Text;
                        Admission_date = grvExcelData.Rows[i].Cells[3].Text;
                        Admission_no = grvExcelData.Rows[i].Cells[4].Text;
                        Student_name = grvExcelData.Rows[i].Cells[5].Text;
                        Class_name = grvExcelData.Rows[i].Cells[6].Text;
                        Section = grvExcelData.Rows[i].Cells[7].Text;
                        Roll_no = grvExcelData.Rows[i].Cells[8].Text;
                        Date_of_birth = grvExcelData.Rows[i].Cells[9].Text;
                        Aadhar_no = grvExcelData.Rows[i].Cells[10].Text;
                        Gender = grvExcelData.Rows[i].Cells[11].Text;
                        Religion = grvExcelData.Rows[i].Cells[12].Text;
                        Father_name = grvExcelData.Rows[i].Cells[13].Text;
                        Mother_name = grvExcelData.Rows[i].Cells[14].Text;
                        Mobile_no = grvExcelData.Rows[i].Cells[15].Text;
                        WhatsApp_no = grvExcelData.Rows[i].Cells[16].Text;
                        Mother_mobile_no = grvExcelData.Rows[i].Cells[17].Text;
                        Address = grvExcelData.Rows[i].Cells[18].Text;
                        Reg_no = grvExcelData.Rows[i].Cells[19].Text;
                        string classid = mycode.get_classid(Class_name);
                        My.exeSql("insert into Bulk_student_entry_status(Session_id,Admission_no,Student_name,Father_name,Class_name,Status,Excel_entry_id) values ('" + ddlsession.SelectedValue + "','" + Admission_no + "','" + Student_name + "','" + Father_name + "','" + Class_name + "','Fail','" + excelEntryId + "')");
                        if (classid == "0")
                        { }
                        else
                        {
                            SqlDataAdapter ad = new SqlDataAdapter("select * from admission_registor where admissionserialnumber='" + Admission_no + "' ", My.conn);//and   Session_id='" + ddlsession.SelectedValue + "'
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
                                dr["class"] = Class_name;
                                dr["admissionserialnumber"] = Admission_no;
                                if (Section == "&nbsp;")
                                {
                                    Section = "";
                                }
                                if (Section == "")
                                {
                                    Section = "";
                                }

                                dr["Section"] = Section;
                                if (Roll_no == "&nbsp;")
                                {
                                    Roll_no = "0";
                                }
                                if (Roll_no == "")
                                {
                                    Roll_no = "0";
                                }

                                dr["rollnumber"] = Roll_no;
                                dr["session"] = ddlsession.SelectedItem.Text;

                                if (Admission_date == "&nbsp;")
                                {
                                    dr["dateofadmission"] = "";
                                }
                                if (Admission_date == "")
                                {
                                    dr["dateofadmission"] = "";
                                }
                                else
                                {
                                    dr["dateofadmission"] = Admission_date;
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
                                if (Date_of_birth == "")
                                {
                                    Date_of_birth = "";
                                }
                                else if (Date_of_birth == "&nbsp;")
                                {
                                    Date_of_birth = "";
                                }
                                else if (Date_of_birth == "Not Available")
                                {
                                    Date_of_birth = "";
                                }
                                dr["dob"] = Date_of_birth;

                                if (Religion == "&nbsp;")
                                {
                                    Religion = "";
                                }
                                else if (Religion == "")
                                {
                                    Religion = "";
                                }
                                dr["religion"] = Religion;

                                if (Address == "&nbsp;")
                                {
                                    Address = "";
                                }
                                else if (Address == "")
                                {
                                    Address = "";
                                }

                                dr["careof"] = Address;

                                if (Mobile_no == "&nbsp;")
                                {
                                    Mobile_no = "";
                                }
                                else if (Mobile_no == "")
                                {
                                    Mobile_no = "";
                                }
                                dr["mob2"] = Mobile_no;

                                if (WhatsApp_no == "&nbsp;")
                                {
                                    WhatsApp_no = "";
                                }
                                dr["Father_whatsApp_no"] = WhatsApp_no;

                                if (Mother_mobile_no == "&nbsp;")
                                {
                                    Mother_mobile_no = "";
                                }
                                dr["mother_mob"] = Mother_mobile_no;


                                dr["payment_status"] = "Unpaid";
                                if (Hostel_taken.ToUpper() == "HOSTEL")
                                {
                                    dr["hosteltaken"] = "Yes";
                                    dr["transportationtaken"] = "No";
                                    dr["Transportation_Id"] = "0";
                                    dr["Transportationpath"] = "";
                                }
                                if (Hostel_taken.ToUpper() == "YES")
                                {
                                    dr["hosteltaken"] = "Yes";
                                    dr["transportationtaken"] = "No";
                                    dr["Transportation_Id"] = "0";
                                    dr["Transportationpath"] = "";
                                }
                                else
                                {
                                    dr["hosteltaken"] = "No";
                                    if (Transport_taken.ToUpper() == "YES")
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

                                if (Aadhar_no == "&nbsp;")
                                {
                                    Aadhar_no = "";
                                }
                                else if (Aadhar_no == "")
                                {
                                    Aadhar_no = "";
                                }
                                dr["aadharno"] = Aadhar_no;
                                dr["admission_idate"] = mycode.ConvertStringToiDateup(Admission_date);

                                dr["Hostel_id"] = "0";
                                dr["Session_id"] = ddlsession.SelectedValue;
                                dr["Class_id"] = classid;
                                dr["Is_TC_Taken"] = false;
                                dr["Student_id"] = Admission_no;
                                dr["f_nationality"] = "INDIAN";
                                dr["m_nationality"] = "INDIAN";
                                dr["father_mob"] = Mobile_no;
                                dr["Course_Type"] = "";
                                dr["Academic_Sem_or_Year"] = "";
                                dr["Academic_Sem_or_Year_id"] = "0";
                                dr["Student_Name_First"] = Student_name;
                                //dr["Student_Name_Last"] = Student_name;
                                dr["studentname"] = Student_name;
                                dr["Father_Name_First"] = Father_name;
                                dr["fathername"] = Father_name;
                                dr["Mother_Name_First"] = Mother_name;
                                dr["mothername"] = Mother_name;
                                dr["is_applied_dayboarding"] = 0;
                                dr["day_bording_with_lunch"] = 0;
                                dr["Branch_id"] = ViewState["branchid"].ToString();
                                dr["StudentStatus"] = "AV";
                                dr["Pwd"] = My.create_random_no_otp();
                                dr["College_School_Name"] = ViewState["college_name"].ToString();
                                dr["Verification_Istatus"] = "0";
                                dr["Status"] = "1";
                                dr["Edit_Istatus"] = "0";
                                dr["Old_Admission_Date"] = Admission_date;
                                dr["OLd_Admission_Idate"] = mycode.ConvertStringToiDate(Admission_date);
                                dr["Created_by"] = ViewState["Userid"].ToString();
                                dr["Created_date"] = mycode.date();
                                dr["Created_time"] = mycode.time();
                                dr["Created_idate"] = mycode.idate();
                                dr["Admission_no_date"] = Reg_no;
                                dt.Rows.Add(dr);
                            }
                            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                            ad.Update(dt);
                            //-----------create account------------------
                            string name = Student_name;
                            try
                            {
                                send_data_Create_ledger(Admission_no, name, Gender, Date_of_birth, Address, Address, "0", Mobile_no, "", Father_name, Admission_date);
                            }
                            catch
                            {
                            }
                            My.exeSql("update Bulk_student_entry_status set Status='Success' where Session_id='" + ddlsession.SelectedValue + "' and Admission_no='" + Admission_no + "' and Excel_entry_id='" + excelEntryId + "'");
                        }
                    }
                    mycode.executequery("UPDATE admission_registor SET studentname = REPLACE(studentname, '&nbsp;', ''); UPDATE admission_registor SET mothername = REPLACE(mothername, '&nbsp;', ''); UPDATE admission_registor SET fathername = REPLACE(fathername, '&nbsp;', '');UPDATE admission_registor SET guardianname = REPLACE(guardianname, '&nbsp;', '');UPDATE admission_registor SET motherqualifiaction = REPLACE(motherqualifiaction, '&nbsp;', '');UPDATE admission_registor SET fatherqualification = REPLACE(fatherqualification, '&nbsp;', ''); UPDATE admission_registor SET aadharno = REPLACE(aadharno, ',','');UPDATE admission_registor SET mother_email = REPLACE(mother_email, '&nbsp;', '');UPDATE admission_registor SET Student_Name_First = REPLACE(Student_Name_First, '&nbsp;', '');UPDATE admission_registor SET Student_Middle_Name = REPLACE(Student_Middle_Name, '&nbsp;', '');UPDATE admission_registor SET Student_Name_Last = REPLACE(Student_Name_Last, '&nbsp;', '');UPDATE admission_registor SET Father_Name_First = REPLACE(Father_Name_First, '&nbsp;', '');UPDATE admission_registor SET Father_Name_Middle = REPLACE(Father_Name_Middle, '&nbsp;', '');UPDATE admission_registor SET Father_Name_Last = REPLACE(Father_Name_Last, '&nbsp;', '');UPDATE admission_registor SET Mother_Name_First = REPLACE(Mother_Name_First, '&nbsp;', '');UPDATE admission_registor SET Mother_Name_Middle = REPLACE(Mother_Name_Middle, '&nbsp;', '');UPDATE admission_registor SET Mother_Name_Last = REPLACE(Mother_Name_Last, '&nbsp;', '');UPDATE admission_registor SET city = REPLACE(city, '&nbsp;', '');UPDATE admission_registor SET postoffice = REPLACE(postoffice, '&nbsp;', '');UPDATE admission_registor SET policestation = REPLACE(policestation, '&nbsp;', '');UPDATE admission_registor SET district = REPLACE(district, '&nbsp;', '');UPDATE admission_registor SET mothername = REPLACE(mothername, '&nbsp;', '');UPDATE admission_registor SET fatherqualification = REPLACE(fatherqualification, '&nbsp;', '');UPDATE admission_registor SET district = REPLACE(district, '&nbsp;', '');UPDATE admission_registor SET mothername = REPLACE(mothername, '&nbsp;', '')");
                    Alertme("Student has been uploaded successfully.", "success");
                    try
                    {
                        DataTable dt = My.dataTable("select Admission_no as [Admission no],Student_name as [Name],Father_name as [Father Name],Class_name as [Class],Status from Bulk_student_entry_status where Excel_entry_id='" + excelEntryId + "'");
                        grvExcelData.DataSource = dt;
                        grvExcelData.DataBind();
                    }
                    catch (Exception ex)
                    {
                    }
                    btn_final_submit.Visible = false;
                }
                catch (Exception ex)
                {
                    My.Save_Exception(ex.ToString());
                }
            }
        }


        private void send_data_Create_ledger(string party_id, string studentname, string gender, string dob, string city, string district, string pin, string father_mob, string state, string fathername, string dateofadmission)
        {
            string statename = mycode.getstatename(state);
            string getstatecode = mycode.getstatename(state);
            SqlConnection conn = new SqlConnection(My.conn);
            SqlDataAdapter ad = new SqlDataAdapter("select * from party_details where  party_id='" + party_id + "'", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                DataRow dr = dt.NewRow();
                dr[1] = studentname;
                dr[2] = city;
                dr[3] = district;
                dr[4] = father_mob;
                dr["gstin"] = "UnRegistered";
                dr[6] = party_id;
                dr[7] = mycode.date();
                dr["firm"] = My.firm_id();
                dr["Registration_Type"] = "Customer";
                dr["State_Code"] = getstatecode;
                dr["State"] = statename;
                dr["type"] = "Customer";
                dr["Care_of"] = fathername;
                dr["pan_no"] = "";
                dr["Account_No"] = "";
                dr["Bank_Name"] = "";
                dr["IFSC_Code"] = "";
                dr[12] = "0";
                dr[13] = "NA";
                dt.Rows.Add(dr);
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(dt);
                My.save_Account_Ledger_Details(studentname, party_id, "26");
                My.update_Ledger_Opening_Balance(studentname, party_id, "26", "Dr", "0.00", dateofadmission, My.get_session());
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    dr[1] = studentname;
                    dr[2] = city;
                    dr[3] = district;
                    dr[4] = father_mob;
                }
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(dt);
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