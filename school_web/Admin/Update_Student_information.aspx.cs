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
    public partial class Update_Student_information : System.Web.UI.Page
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
                        ViewState["college_name"] = My.get_college_name();
                        string pagename_current = Path.GetFileName(Request.Path);
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];



                        bind_session();
                        ddlsession.SelectedValue = My.get_session_id();


                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Update_Student_information");
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
        private void bind_session()
        {

            mycode.bind_all_ddl_with_id(ddlsession, " select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int)");

        }

        protected void btn_Submit_Click1(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_add"].ToString() == "1")
                {

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
                My.submitException(ex, "update student via csv");
            }
        }

        private void upload_excel_file()
        {
            string file = upload_csv_data();
            string csvid = My.auto_serialS("Upload_csvid");
            ViewState["csvid"] = csvid;
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
            dr["User_Id"] = ViewState["Userid"].ToString();
            dt.Rows.Add(dr);
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
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
            Session["file"] = ("Update_student_Details" + filerename + FileExtension);
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
                        //File.Create(path + "/" + Session["file"]).Close();
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

                    //Alertme(ex.ToString(), "warning");
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

        private bool check_wrap_or_not(string path)
        {
            try
            {
                DataTable tblReadCSV = new DataTable();
                tblReadCSV.Columns.Add("Session");
                tblReadCSV.Columns.Add("Student Type");
                tblReadCSV.Columns.Add("Admission No");
                tblReadCSV.Columns.Add("Admission Date");
                tblReadCSV.Columns.Add("Index No");
                tblReadCSV.Columns.Add("Class");
                tblReadCSV.Columns.Add("Section");
                tblReadCSV.Columns.Add("Roll No");
                tblReadCSV.Columns.Add("House");
                tblReadCSV.Columns.Add("Student First Name");
                tblReadCSV.Columns.Add("Student Middle Name");
                tblReadCSV.Columns.Add("Student Last Name");
                tblReadCSV.Columns.Add("Father First Name");
                tblReadCSV.Columns.Add("Father Middle Name");
                tblReadCSV.Columns.Add("Father Last Name");


                tblReadCSV.Columns.Add("Father Occupation");
                tblReadCSV.Columns.Add("Father Qualification");
                tblReadCSV.Columns.Add("Father Nationality");
                tblReadCSV.Columns.Add("Father Marital Status");
                tblReadCSV.Columns.Add("Aadhaar No");
                tblReadCSV.Columns.Add("Father Email Id");
                tblReadCSV.Columns.Add("Father Mobile No.");

                tblReadCSV.Columns.Add("Mather First Name");
                tblReadCSV.Columns.Add("Mather Middle Name");
                tblReadCSV.Columns.Add("Mather Last Name");
                 
                
                tblReadCSV.Columns.Add("Mather Occupation");
                tblReadCSV.Columns.Add("Mather Qualification");
                tblReadCSV.Columns.Add("Mather Nationality");
                tblReadCSV.Columns.Add("Mather Marital Status");
                tblReadCSV.Columns.Add("Mather Aadhaar No");
                tblReadCSV.Columns.Add("Mather Mobile No");
                tblReadCSV.Columns.Add("Guardian First Name");
                tblReadCSV.Columns.Add("Guardian Middle Name");
                tblReadCSV.Columns.Add("Guardian Last Name");
                tblReadCSV.Columns.Add("Student Nationality");
                

                tblReadCSV.Columns.Add("Religion");

                tblReadCSV.Columns.Add("Caste");
                tblReadCSV.Columns.Add("Date of birth");
                tblReadCSV.Columns.Add("Gender");
                tblReadCSV.Columns.Add("Blood Group");
                tblReadCSV.Columns.Add("Height");
                tblReadCSV.Columns.Add("Weight");
                 
                tblReadCSV.Columns.Add("Category_Jati");
                

                tblReadCSV.Columns.Add("Student Aadhar No.");
                tblReadCSV.Columns.Add("Mother tongue");
                tblReadCSV.Columns.Add("Present Address");
                tblReadCSV.Columns.Add("Present PO");
                tblReadCSV.Columns.Add("Present PS");
                tblReadCSV.Columns.Add("Present District");
                tblReadCSV.Columns.Add("Present City");
                tblReadCSV.Columns.Add("Present State");
                tblReadCSV.Columns.Add("Present PinCode");
                tblReadCSV.Columns.Add("Present Country");
                tblReadCSV.Columns.Add("Present Mobile No");
                tblReadCSV.Columns.Add("Permament_Address");
                tblReadCSV.Columns.Add("Permament_PO");
                tblReadCSV.Columns.Add("Permament_PS");
                tblReadCSV.Columns.Add("Permament_District");
                tblReadCSV.Columns.Add("Permament_City");
                tblReadCSV.Columns.Add("Permament_State");
                tblReadCSV.Columns.Add("Permament PinCode");
                tblReadCSV.Columns.Add("Permament Country");
                tblReadCSV.Columns.Add("Permament_Mobile No");
                tblReadCSV.Columns.Add("Previous School");
                tblReadCSV.Columns.Add("Annual Income");



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
                    Admission_no = grvExcelData.Rows[i].Cells[2].Text;
                    #region check duplicate
                    string adno = Admission_no;
                    DataTable dt = My.dataTable(" select admissionserialnumber from admission_registor where admissionserialnumber='" + adno + "' and Session_id='" + ddlsession.SelectedValue + "'  ");//and Session_id='" + ddlsession.SelectedValue + "'
                    if (dt.Rows.Count == 0)
                    {
                        grvExcelData.Rows[i].BackColor = System.Drawing.Color.Red;
                    }
                    else
                    {



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

        protected void btn_final_submit_Click(object sender, EventArgs e)
        {


            if (ddlsession.SelectedItem.Text == "Select")
            {
                Alertme("Please select  session", "warning");
            }

            else
            {
                try
                {


                    string Session = ddlsession.SelectedValue;
                    string class1 = "";
                    string Admission_Date = "";
                    string Section = "";
                    string AdmissionNo = "";
                    string Roll_No = "";
                    string Student_FName = "";
                    string Student_MName = "";
                    string Student_LName = "";
                    string Dateof_borth = "";
                    string BloodGroup = "";
                    string Religion = "";
                    //=======================
                    string MotherToung = "";
                    string Father_F_Name = "";
                    string Father_M_Name = "";
                    string Father_L_Name = "";
                    string Father_Occupation = "";
                    string Father_Qualification = "";
                    string Father_Nationalty = "";
                    string Father_Marrital_Status = "";
                    string Father_mobile_no = "";
                    string Father_email_id = "";
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
                    //==========
                    string Index_No = "";
                    string housname = "";
                    string Father_ocupation = "";
                    string Father_aadharnumber = "";
                    string Mother_aadharnum = "";
                    string Guardian_firstname = "";
                    string Guardian_middlename = "";
                    string Guardian_lastname = "";
                    string hight = "";
                    string Weight = "";
                    string student_aadhar_number = "";
                    string Mother_tongue = "";
                    string Present_Address = "";
                    string Present_po = "";
                    string Present_ps = "";
                    string Present_distict = "";
                    string Present_city = "";
                    string Present_state = "";
                    string Present_pincode = "";
                    string Present_Country = "";
                    string Present_Mbile = "";
                    string Permament_Address = "";
                    string Permament_po = "";
                    string Permament_ps = "";
                    string Permament_distict = "";
                    string Permament_city = "";
                    string Permament_state = "";
                    string Permament_pincode = "";
                    string Permament_Country = "";
                    string Permament_Mbile = "";

                    string Previous_School = "";
                    string Category_Caste = "";
                    string Annula_income = "";
                    string Gender = "";
                    string Category_Jati = "";
                    string Student_nationality = "";
                    DateTime date = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                    for (int i = 0; i < grvExcelData.Rows.Count; i++)
                    {
                        AdmissionNo = grvExcelData.Rows[i].Cells[2].Text;
                        Admission_Date = grvExcelData.Rows[i].Cells[3].Text;
                        Index_No = grvExcelData.Rows[i].Cells[4].Text;
                        class1 = grvExcelData.Rows[i].Cells[5].Text;
                        Section = grvExcelData.Rows[i].Cells[6].Text;
                        Roll_No = grvExcelData.Rows[i].Cells[7].Text;
                        housname = grvExcelData.Rows[i].Cells[8].Text;
                        Student_FName = grvExcelData.Rows[i].Cells[9].Text;
                        Student_MName = grvExcelData.Rows[i].Cells[10].Text;
                        Student_LName = grvExcelData.Rows[i].Cells[11].Text;

                        Father_F_Name = grvExcelData.Rows[i].Cells[12].Text;
                        Father_M_Name = grvExcelData.Rows[i].Cells[13].Text;
                        Father_L_Name = grvExcelData.Rows[i].Cells[14].Text;
                        Father_Occupation = grvExcelData.Rows[i].Cells[15].Text;
                        Father_Qualification = grvExcelData.Rows[i].Cells[16].Text;
                        Father_Nationalty = grvExcelData.Rows[i].Cells[17].Text;
                        Father_Marrital_Status = grvExcelData.Rows[i].Cells[18].Text;
                        Father_aadharnumber = grvExcelData.Rows[i].Cells[19].Text;
                        Father_email_id = grvExcelData.Rows[i].Cells[20].Text;
                        Father_mobile_no = grvExcelData.Rows[i].Cells[21].Text;


                        Mother_F_Name = grvExcelData.Rows[i].Cells[22].Text;
                        Mother_M_Name = grvExcelData.Rows[i].Cells[23].Text;
                        Mother_L_Name = grvExcelData.Rows[i].Cells[24].Text;
                        Mother_Occupation = grvExcelData.Rows[i].Cells[25].Text;
                        Mother_Qualification = grvExcelData.Rows[i].Cells[26].Text;
                        Mother_Nationality = grvExcelData.Rows[i].Cells[27].Text;
                        Mother_Maritial_Status = grvExcelData.Rows[i].Cells[28].Text;
                        Mother_aadharnum = grvExcelData.Rows[i].Cells[29].Text;
                        Mother_Mobile = grvExcelData.Rows[i].Cells[30].Text;

                        Guardian_firstname = grvExcelData.Rows[i].Cells[31].Text;
                        Guardian_middlename = grvExcelData.Rows[i].Cells[32].Text;
                        Guardian_lastname = grvExcelData.Rows[i].Cells[33].Text;
                        Student_nationality = grvExcelData.Rows[i].Cells[34].Text;//ddl

                        Religion = grvExcelData.Rows[i].Cells[35].Text;//ddl
                        Category_Caste = grvExcelData.Rows[i].Cells[36].Text;//ddl
                        Dateof_borth = grvExcelData.Rows[i].Cells[37].Text;

                        Gender = grvExcelData.Rows[i].Cells[38].Text;

                        BloodGroup = grvExcelData.Rows[i].Cells[39].Text;//ddl
                        hight = grvExcelData.Rows[i].Cells[40].Text;
                        Weight = grvExcelData.Rows[i].Cells[41].Text;
                        Category_Jati = grvExcelData.Rows[i].Cells[42].Text;//Textbox

                        student_aadhar_number = grvExcelData.Rows[i].Cells[43].Text;
                        Mother_tongue = grvExcelData.Rows[i].Cells[44].Text;
                        Present_Address = grvExcelData.Rows[i].Cells[45].Text;

                        Present_po = grvExcelData.Rows[i].Cells[46].Text;
                        Present_ps = grvExcelData.Rows[i].Cells[47].Text;
                        Present_distict = grvExcelData.Rows[i].Cells[48].Text;
                        Present_city = grvExcelData.Rows[i].Cells[49].Text;
                        Present_state = grvExcelData.Rows[i].Cells[50].Text;//ddl
                        Present_pincode = grvExcelData.Rows[i].Cells[51].Text;
                        Present_Country = grvExcelData.Rows[i].Cells[52].Text;//ddl
                        Present_Mbile = grvExcelData.Rows[i].Cells[53].Text;//ddl


                        Permament_Address = grvExcelData.Rows[i].Cells[54].Text;
                        Permament_po = grvExcelData.Rows[i].Cells[55].Text;
                        Permament_ps = grvExcelData.Rows[i].Cells[56].Text;
                        Permament_distict = grvExcelData.Rows[i].Cells[57].Text;
                        Permament_city = grvExcelData.Rows[i].Cells[58].Text;
                        Permament_state = grvExcelData.Rows[i].Cells[59].Text;//ddl
                        Permament_pincode = grvExcelData.Rows[i].Cells[60].Text;
                        Permament_Country = grvExcelData.Rows[i].Cells[61].Text;//ddl
                        Permament_Mbile = grvExcelData.Rows[i].Cells[62].Text;

                        Previous_School = grvExcelData.Rows[i].Cells[63].Text;

                        ParantIncome = grvExcelData.Rows[i].Cells[64].Text;

                        string studentmiddname = "";
                        string fathermiddname = "";
                        string mothermiddname = "";
                        SqlDataAdapter ad = new SqlDataAdapter("select * from admission_registor where admissionserialnumber='" + AdmissionNo + "'  and   Session_id='" + ddlsession.SelectedValue + "' ", My.conn);
                        DataSet ds = new DataSet();
                        ad.Fill(ds, "admission_registor");
                        DataTable dt = ds.Tables[0];
                        if (dt.Rows.Count == 0)
                        {



                        }
                        else
                        {
                            foreach (DataRow dr in dt.Rows)
                            {


                                
                                if (Admission_Date == "&nbsp;")
                                {

                                }
                                else
                                {
                                    bool validdate = mycode.checkvaliddate(Admission_Date);
                                    if (validdate == true)
                                    {

                                        dr["dateofadmission"] = Admission_Date;
                                    }

                                }
                                if (Index_No == "&nbsp;")
                                {
                                    Index_No = "";
                                }
                                if (Index_No == "")
                                {
                                    Index_No = "";
                                }

                                dr["Index_no"] = Index_No;


                                //if (Section == "&nbsp;")
                                //{
                                //    Section = "";
                                //}
                                //if (Section == "")
                                //{
                                //    Section = "";
                                //}

                                //dr["Section"] = Section;


                                //if (Roll_No == "&nbsp;")
                                //{
                                //    Roll_No = "0";
                                //}
                                //if (Roll_No == "")
                                //{
                                //    Roll_No = "0";
                                //}

                                //dr["rollnumber"] = Roll_No;




                                //if (housname == "&nbsp;")
                                //{
                                //    housname = "0";
                                //}
                                //if (housname == "")
                                //{
                                //    housname = "0";
                                //}

                                //string house_id = My.get_house_name(housname);
                                //dr["Hostel_id"] = house_id;


                                if (Student_FName == "&nbsp;")
                                {
                                    Student_FName = "";

                                }
                                if (Student_FName == "")
                                {
                                    Student_FName = "";

                                }

                                dr["Student_Name_First"] = Student_FName;




                                if (Student_MName == "&nbsp;")
                                {
                                    Student_MName = "";

                                }

                                else if (Student_MName == "")
                                {
                                    Student_MName = "";

                                }
                                dr["Student_Middle_Name"] = Student_MName;
                               

                                if (Student_LName == "")
                                {

                                    Student_LName = "";
                                }
                                else if (Student_LName == "&nbsp;")
                                {

                                    Student_LName = "";

                                }
                                dr["Student_Name_Last"] = Student_LName;
                                dr["studentname"] = Student_FName + " " + Student_MName + " " + Student_LName;


                                dr["Father_Name_First"] = Father_F_Name;

                                if (Father_M_Name == "&nbsp;")
                                {
                                    fathermiddname = "";

                                }

                                else if (Father_M_Name == "")
                                {
                                    fathermiddname = "";

                                }
                                dr["Father_Name_Middle"] = fathermiddname;
                                fathermiddname = Father_M_Name;


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

                                if (Father_Occupation == "&nbsp;")
                                {
                                    Father_Occupation = "NA";
                                    dr["occuption"] = Father_Occupation.ToUpper();
                                }
                                else if (Father_Occupation == "NA")
                                {
                                    Father_Occupation = "NA";
                                    dr["occuption"] = Father_Occupation.ToUpper();
                                }
                                else
                                {
                                    Boolean FileOK = false;
                                    string[] allowedExtension = { "OTHERS", "STATE GOVT. JOB", "CENTRAL GOVT. JOB", "PRIVATE JOB", "BUSINESS", "FARMER", "PUBLIC SECTOR EMPLOYEE","ACCOUNTANT","ADVOCATE", "AIR CRAFT ENG","ARMY", "ASSISTAND PROPESSOR", "ASSISTANT TEACHER", "BANKING SERVICE", "CENTRAL GOVT", "DOCTOR", "ENGINEER", "EXPIRED", "GOVT JOB", "GOVT RETIRED", "IT PROFESSIONAL", "JOURNALIST", "LAWYER", "MANAGER", "PRIVATE JOB", "RETIRED","SENIOR AME", "SERVICE", "OTHER", "NA" };

                                    for (int j = 0; j < allowedExtension.Length; j++)
                                    {
                                        if (Father_Occupation.ToUpper() == allowedExtension[j])
                                        {

                                            FileOK = true;
                                            break;
                                        }
                                    }
                                    if (FileOK == true)
                                    {
                                        dr["occuption"] = Father_Occupation.ToUpper();
                                    }
                                    else
                                    {
                                        dr["occuption"] = "NA";
                                    }


                                }
                                if (Father_Qualification == "&nbsp;")
                                {
                                    Father_Qualification = "";
                                }
                                else if (Father_Qualification == "")
                                {
                                    Father_Qualification = "";
                                }


                                dr["fatherqualification"] = Father_Qualification;



                                if (Father_Nationalty == "&nbsp;")
                                {
                                    Father_Nationalty = "INDIAN";
                                    dr["f_nationality"] = Father_Nationalty.ToUpper();
                                }
                                else if (Father_Nationalty == "")
                                {
                                    Father_Nationalty = "INDIAN";
                                    dr["f_nationality"] = Father_Nationalty.ToUpper();
                                }
                                else if (Father_Nationalty.ToUpper() == "INDIA")
                                {
                                    dr["f_nationality"] = Father_Nationalty.ToUpper();

                                }
                                else
                                {
                                    dr["f_nationality"] = Father_Nationalty.ToUpper();

                                }

                                if (Father_Marrital_Status == "&nbsp;")
                                {
                                    Father_Marrital_Status = "";
                                    dr["f_marrital_statue"] = "N/A";
                                }
                                else if (Father_Marrital_Status == "")
                                {
                                    Father_Marrital_Status = "";
                                    dr["f_marrital_statue"] = "N/A";
                                }
                                else
                                {
                                    Boolean FileOK = false;
                                    string[] allowedExtension = { "MARRIED", "UNMARRIED", "DIVORCE", "SINGLE PARENT", "N/A" };

                                    for (int j = 0; j < allowedExtension.Length; j++)
                                    {
                                        if (Father_Marrital_Status.ToUpper() == allowedExtension[j])
                                        {

                                            FileOK = true;
                                            break;
                                        }
                                    }
                                    if (FileOK == true)
                                    {
                                        dr["f_marrital_statue"] = Father_Marrital_Status.ToUpper();
                                    }
                                    else
                                    {
                                        dr["f_marrital_statue"] = "N/A";
                                    }
                                }
                                if (Father_aadharnumber == "&nbsp;")
                                {
                                    Father_aadharnumber = "N/A";
                                }
                                else if (Father_aadharnumber == "")
                                {
                                    Father_aadharnumber = "N/A";
                                }

                                dr["Father_aadhar_no"] = Father_aadharnumber;


                                if (Father_email_id == "&nbsp;")
                                {
                                    Father_email_id = "";
                                }
                                else if (Father_email_id == "")
                                {
                                    Father_email_id = "";
                                }
                                dr["email_id"] = Father_email_id;

                                if (Father_mobile_no == "&nbsp;")
                                {
                                    Father_mobile_no = "";
                                }
                                else if (Father_mobile_no == "")
                                {
                                    Father_mobile_no = "";
                                }

                                dr["father_mob"] = Father_mobile_no;



                                dr["Mother_Name_First"] = Mother_F_Name;

                                if (Mother_M_Name == "&nbsp;")
                                {
                                    Mother_M_Name = "";
                                    dr["Mother_Name_Middle"] = "";
                                }
                                else if (Mother_M_Name == "")
                                {
                                    Mother_M_Name = "";
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

                                if (Mother_Occupation == "&nbsp;")
                                {
                                    Mother_Occupation = "";
                                    dr["m_occupation"] = "NA";
                                }
                                else if (Mother_Occupation == "")
                                {
                                    Mother_Occupation = "";
                                    dr["m_occupation"] = "NA";
                                }
                                else
                                {
                                    Boolean FileOK = false;
                                    string[] allowedExtension = { "OTHERS", "STATE GOVT. JOB", "CENTRAL GOVT. JOB", "PRIVATE JOB", "BUSINESS", "FARMER", "PUBLIC SECTOR EMPLOYEE", "HOUSE WIFE", "ADVOCATE", "ASSISTANT PROFESSOR", "ASST TEACHER", "GOVT JOB", "JOURNALIST", "PENSION", "PRIVATE JOB", "SERVICE", "OTHERS", "NA" };

                                    for (int j = 0; j < allowedExtension.Length; j++)
                                    {
                                        if (Mother_Occupation.ToUpper() == allowedExtension[j])
                                        {

                                            FileOK = true;
                                            break;
                                        }
                                    }
                                    if (FileOK == true)
                                    {
                                        dr["m_occupation"] = Mother_Occupation.ToUpper();
                                    }
                                    else
                                    {
                                        dr["m_occupation"] = "NA";
                                    }


                                }
                                if (Mother_Qualification == "&nbsp;")
                                {
                                    Mother_Qualification = "";
                                }
                                else if (Mother_Qualification == "")
                                {
                                    Mother_Qualification = "";
                                }

                                dr["motherqualifiaction"] = Mother_Qualification;



                                if (Mother_Maritial_Status == "&nbsp;")
                                {
                                    Mother_Maritial_Status = "N/A";
                                    dr["m_marrital_statue"] = Mother_Maritial_Status.ToUpper();
                                }
                                else if (Mother_Maritial_Status == "")
                                {
                                    Mother_Maritial_Status = "N/A";
                                    dr["m_marrital_statue"] = Mother_Maritial_Status.ToUpper();
                                }
                                else
                                {
                                    Boolean FileOK = false;
                                    string[] allowedExtension = { "MARRIED", "UNMARRIED", "DIVORCE", "SINGLE PARENT", "N/A" };

                                    for (int j = 0; j < allowedExtension.Length; j++)
                                    {
                                        if (Mother_Maritial_Status.ToUpper() == allowedExtension[j])
                                        {

                                            FileOK = true;
                                            break;
                                        }
                                    }
                                    if (FileOK == true)
                                    {
                                        dr["m_marrital_statue"] = Mother_Maritial_Status.ToUpper();
                                    }
                                    else
                                    {
                                        dr["m_marrital_statue"] = "N/A";
                                    }


                                }


                                if (Mother_Nationality == "&nbsp;")
                                {
                                    Mother_Nationality = "Indian";
                                }
                                else if (Mother_Nationality == "")
                                {
                                    Mother_Nationality = "Indian";
                                }
                                else if (Mother_Nationality.ToUpper() == "INDIA")
                                {
                                    Mother_Nationality = "Indian";
                                }

                                dr["m_nationality"] = Mother_Nationality.ToUpper();


                                if (Mother_aadharnum == "&nbsp;")
                                {
                                    Mother_aadharnum = "";
                                }
                                else if (Mother_aadharnum == "")
                                {
                                    Mother_aadharnum = "";
                                }

                                dr["Mother_aadhar_no"] = Mother_aadharnum;
                                if (Mother_Mobile == "&nbsp;")
                                {
                                    Mother_Mobile = "";
                                }
                                else if (Mother_Mobile == "")
                                {
                                    Mother_Mobile = "";
                                }
                                dr["mother_mob"] = Mother_Mobile;

                                Guardian_firstname = grvExcelData.Rows[i].Cells[31].Text;
                                Guardian_middlename = grvExcelData.Rows[i].Cells[32].Text;
                                Guardian_lastname = grvExcelData.Rows[i].Cells[33].Text;



                                if (Guardian_firstname == "&nbsp;")
                                {
                                    Guardian_firstname = "";
                                }
                                else if (Guardian_firstname == "")
                                {
                                    Guardian_firstname = "";
                                }


                                if (Guardian_middlename == "&nbsp;")
                                {
                                    Guardian_middlename = "";
                                }
                                else if (Guardian_middlename == "")
                                {
                                    Guardian_middlename = "";
                                }
                                if (Guardian_lastname == "&nbsp;")
                                {
                                    Guardian_lastname = "";
                                }
                                else if (Guardian_lastname == "&nbsp;")
                                {
                                    Guardian_lastname = "";

                                }
                                dr["guardianname"] = Guardian_firstname + " " + Guardian_middlename + " " + Guardian_lastname;


                                if (Religion == "&nbsp;")
                                {
                                    Religion = "N/A";
                                    dr["religion"] = "N/A";
                                }
                                else if (Religion == "")
                                {
                                    Religion = "N/A";
                                    dr["religion"] = "N/A";
                                }
                                else
                                {
                                    Boolean FileOK = false;
                                    string[] allowedExtension = { "HINDU", "ISLAM", "SIKH", "CHRISTIAN", "BUDDHISM", "JAIN", "N/A" };

                                    for (int j = 0; j < allowedExtension.Length; j++)
                                    {
                                        if (Religion.ToUpper() == allowedExtension[j])
                                        {

                                            FileOK = true;
                                            break;
                                        }
                                    }
                                    if (FileOK == true)
                                    {
                                        dr["religion"] = Religion.ToUpper();
                                    }
                                    else
                                    {
                                        dr["religion"] = "N/A";
                                    }

                                }

                                if (Category_Caste == "&nbsp;")
                                {
                                    Category_Caste = "";
                                    dr["cast"] = "OTHERS";
                                }
                                else if (Category_Caste == "")
                                {
                                    Category_Caste = "";
                                    dr["cast"] = "OTHERS";
                                }
                                else
                                {
                                    Boolean FileOK = false;
                                    string[] allowedExtension = { "Select", "GENERAL", "OBC", "OBC-A", "OBC-B", "ST", "SC", "EBC", "OTHERS" };

                                    for (int j = 0; j < allowedExtension.Length; j++)
                                    {
                                        if (Category_Caste.ToUpper() == allowedExtension[j])
                                        {

                                            FileOK = true;
                                            break;
                                        }
                                    }
                                    if (FileOK == true)
                                    {
                                        dr["cast"] = Category_Caste.ToUpper();
                                        //dr["Category"] = Category_Caste.ToUpper();
                                    }
                                    else
                                    {
                                        dr["cast"] = "OTHERS";
                                        //dr["Category"] = "OTHERS";
                                        
                                         
                                    }


                                }



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
                                else
                                {
                                    bool validdate = mycode.checkvaliddate(Dateof_borth);
                                    if (validdate == true)
                                    { 
                                        dr["dob"] = Dateof_borth;
                                    } 
                                } 

                                if (Gender == "&nbsp;")
                                {
                                    dr["Gender"] = "MALE";
                                }
                                if (Gender == "")
                                {

                                    dr["Gender"] = "MALE";
                                }
                                else
                                {
                                    Boolean FileOK = false;
                                    string[] allowedExtension = { "MALE", "FEMALE", "TRANSGENDER", "SELECT" };

                                    for (int j = 0; j < allowedExtension.Length; j++)
                                    {
                                        if (Gender.ToUpper() == allowedExtension[j])
                                        {

                                            FileOK = true;
                                            break;
                                        }
                                    }
                                    if (FileOK == true)
                                    {
                                        dr["Gender"] = Gender.ToUpper();
                                    }
                                    else
                                    {
                                        dr["Gender"] = "MALE";
                                    } 
                                } 


                                if (BloodGroup == "&nbsp;")
                                {
                                    BloodGroup = "NA";
                                }
                                else if (BloodGroup == "")
                                {
                                    BloodGroup = "NA";
                                }
                                dr["blood_group"] = BloodGroup.ToUpper();

                                if (hight == "&nbsp;")
                                {
                                    hight = "";
                                }
                                else if (hight == "")
                                {
                                    hight = "";
                                }
                                dr["Height"] = hight;

                                if (Weight == "&nbsp;")
                                {
                                    Weight = "";
                                }
                                else if (Weight == "")
                                {
                                    Weight = "";
                                }
                                dr["Weight"] = Weight;
                                if (Category_Jati == "&nbsp;")
                                {
                                    Category_Jati = "";
                                }
                                else if (Category_Jati == "")
                                {
                                    Category_Jati = "";
                                }

                                dr["jati"] = Category_Jati;
                                


                                if (student_aadhar_number == "&nbsp;")
                                {
                                    student_aadhar_number = "";
                                }
                                else if (student_aadhar_number == "")
                                {
                                    student_aadhar_number = "";
                                }
                                dr["aadharno"] = student_aadhar_number;

                                if (MotherToung == "&nbsp;")
                                {
                                    MotherToung = "BANGALA";
                                }
                                else if (MotherToung == "")
                                {
                                    MotherToung = "BANGALA";
                                }

                                dr["student_mother_tounge"] = MotherToung.ToUpper();



                                

                                if (Present_Address == "&nbsp;")
                                {
                                    dr["careof"] = "";
                                }
                                else
                                {
                                    dr["careof"] = Present_Address.ToUpper();

                                }

                                if (Present_po == "&nbsp;")
                                {
                                    dr["postoffice"] ="";
                                }
                                else
                                {
                                    dr["postoffice"] = Present_po.ToUpper();

                                }
                                if (Present_ps == "&nbsp;")
                                {
                                    dr["policestation"] = "";
                                }
                                else
                                {
                                    dr["policestation"] = Present_ps.ToUpper();

                                }
                                if (Present_distict == "&nbsp;")
                                {
                                    dr["district"] = "";
                                }
                                else
                                {
                                    dr["district"] = Present_distict.ToUpper();

                                }
                                if (Present_city == "&nbsp;")
                                {
                                    dr["city"] = "";
                                }
                                else
                                {
                                    dr["city"] = Present_city.ToUpper();

                                }

                                if (Present_state == "&nbsp;")
                                {
                                    dr["state"] = "0";
                                }
                                else
                                {
                                    dr["state"] = My.get_state_code(Present_state);

                                }
                                if (Present_pincode == "&nbsp;")
                                {
                                    dr["pin"] = "";
                                }
                                else
                                {
                                    dr["pin"] = Present_pincode;

                                }

                                if (Present_Country == "&nbsp;")
                                {
                                    dr["Present_country"] = "INDIA";
                                }
                                else
                                {
                                    dr["Present_country"] = Present_Country.ToUpper();

                                }

                                if (Present_Mbile == "&nbsp;")
                                {
                                    dr["mobilenumber"] = "";
                                }
                                else
                                {
                                    dr["mobilenumber"] = Present_Mbile;

                                }

                                //-----------------------------------///

                               
                                if (Permament_Address == "&nbsp;")
                                {
                                    dr["careof_permanent"] = "";
                                }
                                else
                                {
                                    dr["careof_permanent"] = Permament_Address.ToUpper();

                                }
                                if (Permament_po == "&nbsp;")
                                {
                                    dr["postoffice_permanent"] = "";
                                }
                                else
                                {
                                    dr["postoffice_permanent"] = Permament_po.ToUpper();

                                }
                                if (Permament_ps == "&nbsp;")
                                {
                                    dr["policestation_permanent"] = "";
                                }
                                else
                                {
                                    dr["policestation_permanent"] = Permament_ps.ToUpper();

                                }
                                if (Permament_distict == "&nbsp;")
                                {
                                    dr["district_permanent"] = "";
                                }
                                else
                                {
                                    dr["district_permanent"] = Permament_distict.ToUpper();

                                }
                                if (Permament_city == "&nbsp;")
                                {
                                    dr["city_permanent"] = "";
                                }
                                else
                                {
                                    dr["city_permanent"] = Permament_city.ToUpper();

                                }

                                if (Permament_state == "&nbsp;")
                                {
                                    dr["state_permanent"] = "0";
                                }
                                else
                                {
                                    dr["state_permanent"] = My.get_state_code(Permament_state);

                                }
                                if (Permament_pincode == "&nbsp;")
                                {
                                    dr["pincode"] = "0";
                                }
                                else
                                {
                                    dr["pincode"] = Permament_pincode;

                                }
                                if (Permament_Country == "&nbsp;")
                                {
                                    dr["Present_country"] = "INDIA";
                                }
                                else
                                {
                                    dr["Present_country"] = Permament_Country; 
                                }

                                if (Permament_Mbile == "&nbsp;")
                                {
                                    dr["mobilenumber"] = "";
                                }
                                else
                                {
                                    dr["mobilenumber"] = Permament_Mbile;

                                }
                                dr["mob2"] = Permament_Mbile;
                                if (Previous_School == "")
                                {
                                    Previous_School = "";

                                }
                                else if (Previous_School == "&nbsp;")
                                {
                                    Previous_School = "";
                                }
                                
                                    dr["currentschool"] = Previous_School;
                                 

                                if (ParantIncome == "&nbsp;")
                                {
                                    ParantIncome = "";
                                }
                                else if (ParantIncome == "")
                                {
                                    ParantIncome = "";
                                }
                                dr["parentincome"] = ParantIncome;
                                dr["csv_id_update"] = ViewState["csvid"].ToString();
                                dr["User_id"] = ViewState["Userid"].ToString();
                                dr["csv_update_date"] = mycode.date();
                            }
                            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                            ad.Update(dt);
                        }
                        Alertme("Student has been uploaded successfully.", "success");
                        btn_final_submit.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    My.Save_Exception(ex.ToString());
                }
            }
        }





    }
}