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
    public partial class bulk_hostel_mapping : System.Web.UI.Page
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
                        ViewState["branch_id"] = mycode.get_branch_id(Session["Admin"].ToString());

                        mycode.bind_all_ddl_with_id(ddl_session, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                        ddl_session.SelectedValue = My.get_session_id();

                        mycode.bind_all_ddl_with_id_no_select(ddl_hostel, "select Hostel_name,Hostel_id from Hostels_master order by Hostel_name asc");


                        mycode.bind_all_ddl_with_id(ddl_excel_session, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) ");
                        ddl_excel_session.SelectedValue = ddl_session.SelectedValue;
                        mycode.bind_all_ddl_with_id_cap_All(ddl_excel_class, "select Course_Name,course_id from Add_course_table order by Position asc");
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Mapping_Transportation_with_Student");
            }
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_session.SelectedItem.Text == "Select")
                {
                    ddl_session.Focus();
                    Alertme("Please select session.", "warning");
                    return;
                }
                if (FileUpload1.HasFile)
                {
                    btn_final_submit.Visible = true;
                    ViewState["Session_ids"] = ddl_session.SelectedValue;
                    upload_excel_file();
                }
                else
                {
                    Alertme("Please choose excel.csv file.", "warning");
                    return;
                }
            }
            catch (Exception ex)
            { }
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
            dr[6] = "Admission_Or_Annual";
            dt.Rows.Add(dr);
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
        }

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
            Session["file"] = ("Upload_transport_student" + filerename + FileExtension);
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

        private bool check_wrap_or_not(string path)
        {
            try
            {
                DataTable tblReadCSV = new DataTable();
                tblReadCSV.Columns.Add("Admission_No");
                tblReadCSV.Columns.Add("Student_name");
                tblReadCSV.Columns.Add("Class");
                tblReadCSV.Columns.Add("Section");
                tblReadCSV.Columns.Add("Roll_no");
                //tblReadCSV.Columns.Add("From_month");

                TextFieldParser csvParser = new TextFieldParser(path);
                csvParser.Delimiters = new string[] { "," };
                csvParser.TrimWhiteSpace = true;
                csvParser.ReadLine();
                while (!(csvParser.EndOfData == true))
                {
                    tblReadCSV.Rows.Add(csvParser.ReadFields());
                }
                pnl_grid.Visible = true;
                lbl_total1.Text = "Total no. of uploaded student : " + tblReadCSV.Rows.Count.ToString();
                ViewState["TotalNeedToMap"] = tblReadCSV.Rows.Count.ToString();
                grvExcelData.DataSource = tblReadCSV;
                grvExcelData.DataBind();

                DataTable dt = My.dataTable("select count(Id) as Count from Hostel_room_bed_master t1 where Bed_id not in (select Bed_id from Hostel_assign_master where Room_id=t1.Room_id and Status='1' and Hostel_id=t1.Hostel_id and Category_id=t1.Category_id and Session_id='" + ddl_session.SelectedValue + "') and Hostel_id='" + ddl_hostel.SelectedValue + "'");
                if (dt.Rows.Count > 0)
                {
                    ViewState["VacantSeat"] = dt.Rows[0]["Count"].ToString();
                    if (tblReadCSV.Rows.Count > My.toIntS(dt.Rows[0]["Count"].ToString()))
                    {
                        Alertme("According to your Excel file, there are fewer vacant beds. Please create additional beds first. Total no. of uploaded student : " + tblReadCSV.Rows.Count.ToString() + " & empty beds are : " + dt.Rows[0]["Count"].ToString(), "warning");
                        ///btn_final_submit.Visible = false;
                        lbl_total1.Text = "According to your Excel file, there are fewer vacant beds. Please create additional beds first. Total no. of uploaded student : " + tblReadCSV.Rows.Count.ToString() + " & empty beds are : " + dt.Rows[0]["Count"].ToString();
                    }
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
            try
            {
                if (My.toDouble(ViewState["VacantSeat"].ToString()) >= My.toDouble(ViewState["TotalNeedToMap"].ToString()))
                {
                    string confirmValue = string.Empty;
                    confirmValue = Request.Form["confirm_value"];
                    if (confirmValue == "Yes")
                    {
                        string group_transaction = My.create_random_no_otp();
                        string Admission_No = "";
                        string From_month = "April";

                        DateTime date = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                        for (int i = 0; i < grvExcelData.Rows.Count; i++)
                        {
                            Admission_No = grvExcelData.Rows[i].Cells[0].Text;
                            if (chk_is_first_chr_remove.Checked == true)
                            {
                                Admission_No = Admission_No.Substring(1);
                            }
                            //Student_name = grvExcelData.Rows[i].Cells[1].Text;
                            //Class = grvExcelData.Rows[i].Cells[2].Text;
                            //Roll_no = grvExcelData.Rows[i].Cells[3].Text; 
                            //Sections = grvExcelData.Rows[i].Cells[4].Text;
                            //Father_name = grvExcelData.Rows[i].Cells[5].Text;
                            // From_month = grvExcelData.Rows[i].Cells[5].Text;

                            Dictionary<string, object> dc1 = My.get_selected_studentinfo(Admission_No, ViewState["Session_ids"].ToString(), ViewState["branch_id"].ToString());
                            string admissionserialnumber = (String)dc1["admissionserialnumber"];
                            string rollnumber = (String)dc1["rollnumber"];
                            string session = (String)dc1["session"];
                            string Section = (String)dc1["Section"];
                            string Class_id = (String)dc1["Class_id"];
                            string classname = (String)dc1["classname"];
                            string hosteltaken = (String)dc1["hosteltaken"];
                            string Transfer_Status = (String)dc1["Transfer_Status"];
                            DataTable dtc = My.dataTable("select Id from HOSTEL_ASSIGN_MASTER where Session_id='" + ViewState["Session_ids"].ToString() + "' and Class_id='" + Class_id + "' and Admission_no='" + Admission_No + "' and Status='1'");
                            if (dtc.Rows.Count > 0)
                            {
                                string Transaction_Id = My.create_random_no_otp();
                                SqlCommand cmd;
                                string query = "INSERT INTO Hostel_mapping_bulk (group_transaction,From_month,Transaction_Id,Admission_no,Class_id,Session_id,Branch_id,User_id,Hostel_id,Room_type_id,Room_id,Bed_id,Bed_name,Status,createddate,Remarks) values (@group_transaction,@From_month,@Transaction_Id,@Admission_no,@Class_id,@Session_id,@Branch_id,@User_id,@Hostel_id,@Room_type_id,@Room_id,@Bed_id,@Bed_name,@Status,@createddate,@Remarks)";
                                cmd = new SqlCommand(query);
                                cmd.Parameters.AddWithValue("@group_transaction", group_transaction);
                                cmd.Parameters.AddWithValue("@From_month", From_month);
                                cmd.Parameters.AddWithValue("@Transaction_Id", Transaction_Id);
                                cmd.Parameters.AddWithValue("@Admission_no", Admission_No);
                                cmd.Parameters.AddWithValue("@Class_id", Class_id);
                                cmd.Parameters.AddWithValue("@Session_id", ViewState["Session_ids"].ToString());
                                cmd.Parameters.AddWithValue("@Branch_id", ViewState["branch_id"].ToString());
                                cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                                cmd.Parameters.AddWithValue("@Hostel_id", "0");
                                cmd.Parameters.AddWithValue("@Room_type_id", "0");
                                cmd.Parameters.AddWithValue("@Room_id", "0");
                                cmd.Parameters.AddWithValue("@Bed_id", "0");
                                cmd.Parameters.AddWithValue("@Bed_name", "0");
                                cmd.Parameters.AddWithValue("@Status", "Failed (Already Assigned)");
                                cmd.Parameters.AddWithValue("@createddate", My.getdate1());
                                cmd.Parameters.AddWithValue("@Remarks", "Already Assigned");
                                if (InsertUpdate.InsertUpdateData(cmd))
                                {
                                }
                            }
                            else
                            {
                                if (admissionserialnumber != "0")
                                {
                                    string query2 = "Select * from Hostel_mapping_bulk where Session_id=" + ViewState["Session_ids"].ToString() + "  and Class_id=" + Class_id + " and Admission_no='" + admissionserialnumber + "' and Branch_id='" + ViewState["branch_id"].ToString() + "' and Status='Pending' and group_transaction='" + group_transaction + "'";
                                    DataTable dt = mycode.FillData(query2);
                                    if (dt.Rows.Count == 0)
                                    {
                                        DataTable dtbed = My.dataTable("select * from Hostel_room_bed_master t1 where Bed_id not in (select Bed_id from Hostel_assign_master where Room_id=t1.Room_id and Status='1' and Hostel_id=t1.Hostel_id and Category_id=t1.Category_id and Session_id='" + ddl_session.SelectedValue + "') and Hostel_id='" + ddl_hostel.SelectedValue + "'");
                                        if (dtbed.Rows.Count > 0)
                                        {
                                            string hostel_id = dtbed.Rows[0]["Hostel_id"].ToString();
                                            string room_type = dtbed.Rows[0]["Category_id"].ToString();
                                            string room_id = dtbed.Rows[0]["Room_id"].ToString();
                                            string bed_no = dtbed.Rows[0]["Bed_id"].ToString();
                                            string Bed_name = dtbed.Rows[0]["Bed_name"].ToString();
                                            string Transaction_Id = My.create_random_no_otp();
                                            SqlCommand cmd;
                                            string query = "INSERT INTO Hostel_mapping_bulk (group_transaction,From_month,Transaction_Id,Admission_no,Class_id,Session_id,Branch_id,User_id,Hostel_id,Room_type_id,Room_id,Bed_id,Bed_name,Status,createddate,Remarks) values (@group_transaction,@From_month,@Transaction_Id,@Admission_no,@Class_id,@Session_id,@Branch_id,@User_id,@Hostel_id,@Room_type_id,@Room_id,@Bed_id,@Bed_name,@Status,@createddate,@Remarks)";
                                            cmd = new SqlCommand(query);
                                            cmd.Parameters.AddWithValue("@group_transaction", group_transaction);
                                            cmd.Parameters.AddWithValue("@From_month", From_month);
                                            cmd.Parameters.AddWithValue("@Transaction_Id", Transaction_Id);
                                            cmd.Parameters.AddWithValue("@Admission_no", Admission_No);
                                            cmd.Parameters.AddWithValue("@Class_id", Class_id);
                                            cmd.Parameters.AddWithValue("@Session_id", ViewState["Session_ids"].ToString());
                                            cmd.Parameters.AddWithValue("@Branch_id", ViewState["branch_id"].ToString());
                                            cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                                            cmd.Parameters.AddWithValue("@Hostel_id", hostel_id);
                                            cmd.Parameters.AddWithValue("@Room_type_id", room_type);
                                            cmd.Parameters.AddWithValue("@Room_id", room_id);
                                            cmd.Parameters.AddWithValue("@Bed_id", "0");
                                            cmd.Parameters.AddWithValue("@Bed_name", "0");
                                            cmd.Parameters.AddWithValue("@Status", "Pending");
                                            cmd.Parameters.AddWithValue("@createddate", My.getdate1());
                                            cmd.Parameters.AddWithValue("@Remarks", "");
                                            if (InsertUpdate.InsertUpdateData(cmd))
                                            {
                                                Update_student_transport(Transaction_Id, Admission_No, Class_id, hostel_id, room_type, room_id, bed_no, Bed_name, ViewState["Session_ids"].ToString(), From_month, Transfer_Status);
                                            }
                                        }
                                        else
                                        {
                                            //Failed
                                            string Transaction_Id = My.create_random_no_otp();
                                            SqlCommand cmd;
                                            string query = "INSERT INTO Hostel_mapping_bulk (group_transaction,From_month,Transaction_Id,Admission_no,Class_id,Session_id,Branch_id,User_id,Hostel_id,Room_type_id,Room_id,Bed_id,Bed_name,Status,createddate,Remarks) values (@group_transaction,@From_month,@Transaction_Id,@Admission_no,@Class_id,@Session_id,@Branch_id,@User_id,@Hostel_id,@Room_type_id,@Room_id,@Bed_id,@Bed_name,@Status,@createddate,@Remarks)";
                                            cmd = new SqlCommand(query);
                                            cmd.Parameters.AddWithValue("@group_transaction", group_transaction);
                                            cmd.Parameters.AddWithValue("@From_month", From_month);
                                            cmd.Parameters.AddWithValue("@Transaction_Id", Transaction_Id);
                                            cmd.Parameters.AddWithValue("@Admission_no", Admission_No);
                                            cmd.Parameters.AddWithValue("@Class_id", Class_id);
                                            cmd.Parameters.AddWithValue("@Session_id", ViewState["Session_ids"].ToString());
                                            cmd.Parameters.AddWithValue("@Branch_id", ViewState["branch_id"].ToString());
                                            cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                                            cmd.Parameters.AddWithValue("@Hostel_id", "0");
                                            cmd.Parameters.AddWithValue("@Room_type_id", "0");
                                            cmd.Parameters.AddWithValue("@Room_id", "0");
                                            cmd.Parameters.AddWithValue("@Bed_id", "0");
                                            cmd.Parameters.AddWithValue("@Bed_name", "0");
                                            cmd.Parameters.AddWithValue("@Status", "Failed");
                                            cmd.Parameters.AddWithValue("@createddate", My.getdate1());
                                            cmd.Parameters.AddWithValue("@Remarks", "");
                                            if (InsertUpdate.InsertUpdateData(cmd))
                                            {
                                            }
                                        }
                                    }
                                    else
                                    {
                                        //Failed
                                        string Transaction_Id = My.create_random_no_otp();
                                        SqlCommand cmd;
                                        string query = "INSERT INTO Hostel_mapping_bulk (group_transaction,From_month,Transaction_Id,Admission_no,Class_id,Session_id,Branch_id,User_id,Hostel_id,Room_type_id,Room_id,Bed_id,Bed_name,Status,createddate,Remarks) values (@group_transaction,@From_month,@Transaction_Id,@Admission_no,@Class_id,@Session_id,@Branch_id,@User_id,@Hostel_id,@Room_type_id,@Room_id,@Bed_id,@Bed_name,@Status,@createddate,@Remarks)";
                                        cmd = new SqlCommand(query);
                                        cmd.Parameters.AddWithValue("@group_transaction", group_transaction);
                                        cmd.Parameters.AddWithValue("@From_month", From_month);
                                        cmd.Parameters.AddWithValue("@Transaction_Id", Transaction_Id);
                                        cmd.Parameters.AddWithValue("@Admission_no", Admission_No);
                                        cmd.Parameters.AddWithValue("@Class_id", Class_id);
                                        cmd.Parameters.AddWithValue("@Session_id", ViewState["Session_ids"].ToString());
                                        cmd.Parameters.AddWithValue("@Branch_id", ViewState["branch_id"].ToString());
                                        cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                                        cmd.Parameters.AddWithValue("@Hostel_id", "0");
                                        cmd.Parameters.AddWithValue("@Room_type_id", "0");
                                        cmd.Parameters.AddWithValue("@Room_id", "0");
                                        cmd.Parameters.AddWithValue("@Bed_id", "0");
                                        cmd.Parameters.AddWithValue("@Bed_name", "0");
                                        cmd.Parameters.AddWithValue("@Status", "Failed");
                                        cmd.Parameters.AddWithValue("@createddate", My.getdate1());
                                        cmd.Parameters.AddWithValue("@Remarks", "");
                                        if (InsertUpdate.InsertUpdateData(cmd))
                                        {
                                        }
                                    }
                                }
                                else
                                {
                                    string Transaction_Id = My.create_random_no_otp();
                                    SqlCommand cmd;
                                    string query = "INSERT INTO Hostel_mapping_bulk (group_transaction,From_month,Transaction_Id,Admission_no,Class_id,Session_id,Branch_id,User_id,Hostel_id,Room_type_id,Room_id,Bed_id,Bed_name,Status,createddate,Remarks) values (@group_transaction,@From_month,@Transaction_Id,@Admission_no,@Class_id,@Session_id,@Branch_id,@User_id,@Hostel_id,@Room_type_id,@Room_id,@Bed_id,@Bed_name,@Status,@createddate,@Remarks)";
                                    cmd = new SqlCommand(query);
                                    cmd.Parameters.AddWithValue("@group_transaction", group_transaction);
                                    cmd.Parameters.AddWithValue("@From_month", From_month);
                                    cmd.Parameters.AddWithValue("@Transaction_Id", Transaction_Id);
                                    cmd.Parameters.AddWithValue("@Admission_no", Admission_No);
                                    cmd.Parameters.AddWithValue("@Class_id", Class_id);
                                    cmd.Parameters.AddWithValue("@Session_id", ViewState["Session_ids"].ToString());
                                    cmd.Parameters.AddWithValue("@Branch_id", ViewState["branch_id"].ToString());
                                    cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                                    cmd.Parameters.AddWithValue("@Hostel_id", "0");
                                    cmd.Parameters.AddWithValue("@Room_type_id", "0");
                                    cmd.Parameters.AddWithValue("@Room_id", "0");
                                    cmd.Parameters.AddWithValue("@Bed_id", "0");
                                    cmd.Parameters.AddWithValue("@Bed_name", "0");
                                    cmd.Parameters.AddWithValue("@Status", "Failed");
                                    cmd.Parameters.AddWithValue("@createddate", My.getdate1());
                                    cmd.Parameters.AddWithValue("@Remarks", "");
                                    if (InsertUpdate.InsertUpdateData(cmd))
                                    {
                                    }
                                }
                            }
                        }


                        Alertme("Student has been mapped with hostel successfully.", "success"); 
                        btn_final_submit.Visible = false;
                        string query23 = "Select Admission_no,Status,Bed_id,Bed_name from Hostel_mapping_bulk where Session_id=" + ViewState["Session_ids"].ToString() + " and User_id='" + ViewState["Userid"].ToString() + "' and Branch_id='" + ViewState["branch_id"].ToString() + "' and  group_transaction='" + group_transaction + "' ";
                        DataTable dt1 = mycode.FillData(query23);
                        if (dt1.Rows.Count == 0)
                        {
                            grvExcelData.DataSource = null;
                            grvExcelData.DataBind();
                        }
                        else
                        {
                            grvExcelData.DataSource = dt1;
                            grvExcelData.DataBind();
                        }
                    }
                    else
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked NO!')", true);
                    }
                }
                else
                {
                    Alertme("Vacant bed is less than your uploaded student.", "warning");
                }
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }



        private void Update_student_transport(string transaction_Id, string admission_No, string class_id, string Hostel_id, string Room_type_id, string Room_id, string bed_no, string Bed_name, string session_id, string From_month, string Transfer_Status)
        {
            try
            {
                string hostel_assign_id = "";
                string monthid = My.tomonth_numberstring(From_month);
                int pay_month = My.toint(monthid);
                string session = mycode.get_session(session_id);


                string[] stringSeparators = new string[] { "-" };
                string[] arr = session.Split(stringSeparators, StringSplitOptions.None);
                string session_frst_year = arr[0];

                int s_year = My.toint(session_frst_year);
                s_year = My.check_start_months(pay_month, s_year);
                string year_month_id = s_year.ToString() + mycode.get_monthid(pay_month);



                hostel_assign_id = create_sl_no();
                SqlCommand cmd;
                string query = "INSERT INTO Hostel_assign_master (Session_id,Class_id,Admission_no,From_month_name,From_month_id,Hostel_id,Category_id,Room_id,Bed_id,Hostel_assign_id,Created_by,Created_date,Created_idate,Status,Assined_Year_Month) values (@Session_id,@Class_id,@Admission_no,@From_month_name,@From_month_id,@Hostel_id,@Category_id,@Room_id,@Bed_id,@Hostel_assign_id,@Created_by,@Created_date,@Created_idate,@Status,@Assined_Year_Month)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Session_id", session_id);
                cmd.Parameters.AddWithValue("@Class_id", class_id);
                cmd.Parameters.AddWithValue("@Admission_no", admission_No);
                cmd.Parameters.AddWithValue("@From_month_name", From_month);
                cmd.Parameters.AddWithValue("@From_month_id", monthid);
                cmd.Parameters.AddWithValue("@Hostel_id", Hostel_id);
                cmd.Parameters.AddWithValue("@Category_id", Room_type_id);
                cmd.Parameters.AddWithValue("@Room_id", Room_id);
                cmd.Parameters.AddWithValue("@Bed_id", bed_no);
                cmd.Parameters.AddWithValue("@Hostel_assign_id", hostel_assign_id);
                cmd.Parameters.AddWithValue("@Status", 1);
                cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
                cmd.Parameters.AddWithValue("@Assined_Year_Month", year_month_id);
                if (My.InsertUpdateData(cmd))
                {
                    try
                    {
                        mycode.executequery("update Hostel_mapping_bulk set Status='success',Bed_id='" + bed_no + "',Bed_name='" + Bed_name + "' where Transaction_Id='" + transaction_Id + "' and Admission_no='" + admission_No + "' and Session_id='" + session_id + "'; update admission_registor set hosteltaken='Yes',transportationtaken='No',Hostel_id='" + Hostel_id + "',Hostel_assignD_id='" + hostel_assign_id + "' where admissionserialnumber='" + admission_No + "'  and Session_id='" + session_id + "' ");

                        string parameter_old = ""; string parameter_new = "";
                        string parameter_m_old = ""; string parameter_m_new = "";
                        if (Transfer_Status == "New")
                        {
                            parameter_old = "AdmissionFee";
                            parameter_new = "HostelAdmissionFee";
                            //====
                            parameter_m_old = "MonthlyFee";
                            parameter_m_new = "HostelMonthlyFee";
                        }
                        else
                        {
                            parameter_old = "AnnualFee";
                            parameter_new = "HostelAnnualFee";
                            //====
                            parameter_m_old = "MonthlyFee";
                            parameter_m_new = "HostelMonthlyFee";
                        }

                        mycode.executequery("update Typewise_fee_collection set parameter='" + parameter_m_new + "'  where admission_no='" + admission_No + "' and session='" + session + "' and parameter='" + parameter_m_old + "'; update Typewise_fee_collection set parameter='" + parameter_new + "'  where admission_no='" + admission_No + "' and session='" + session + "' and parameter='" + parameter_old + "'");
                    }
                    catch
                    {
                    }
                }
            }
            catch
            {
            }
        }


        private string create_sl_no()
        {
            bool duplicate = true;
            string hostel_assign_id = My.auto_serialS("Hostel_assign_id");
            while (duplicate)
            {
                DataTable cdt = My.dataTable("select Hostel_assign_id from Hostel_assign_master where Hostel_assign_id='" + hostel_assign_id + "'");
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    duplicate = false;
                }
                else
                {
                    hostel_assign_id = My.auto_serialS("Hostel_assign_id");
                }
            }
            return hostel_assign_id;
        }


        protected void lnk_download_excel_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_excel_session.SelectedItem.Text == "Select")
                {
                    ddl_excel_session.Focus();
                    Alertme("Please select session.", "warning");
                }
                else
                {
                    string qry = "";
                    if (ddl_excel_class.SelectedItem.Text == "ALL")
                    {
                        qry = "select admissionserialnumber as Admission_No,studentname as Student_name,class as Class,Section,rollnumber as Roll_no from admission_registor where Session_id='" + ddl_excel_session.SelectedValue + "' and hosteltaken='Yes' and Status=1";
                    }
                    else
                    {
                        qry = "select admissionserialnumber as Admission_No,studentname as Student_name,class as Class,Section,rollnumber as Roll_no from admission_registor where Session_id='" + ddl_excel_session.SelectedValue + "' and Class_id='" + ddl_excel_class.SelectedValue + "' and hosteltaken='Yes' and Status=1";
                    }
                    DataTable dt = My.dataTable(qry);
                    if (dt.Rows.Count > 0)
                    {
                        string file_name = My.with_excel_name("student-list-for-hostel");
                        string attachment = "attachment; filename=" + file_name + ".csv";
                        Response.ClearContent();
                        Response.AddHeader("content-disposition", attachment);
                        Response.ContentType = "text/csv";
                        var csvContent = My.DataTableToCsv(dt);
                        Response.Write(csvContent);
                        Response.End();
                        Alertme("Student list has been downloaded successfully.", "success");
                    }
                    else
                    {
                        Alertme("Student not found.", "warning");
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}