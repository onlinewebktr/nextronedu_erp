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
    public partial class transport_mapping_by_excel : System.Web.UI.Page
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

                        mycode.bind_all_ddl_with_id(ddl_bus_name, "select transport_name+'-{BUS NO-'+Bus_no+'}',transport_id from  Transport_Master order by transport_name");



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

        protected void ddl_bus_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            btn_final_submit.Visible = false;
            grvExcelData.DataSource = null;
            grvExcelData.DataBind();
            if (ddl_bus_name.SelectedItem.Text == "Select")
            {
                Alertme("Please select bus name", "warning");
            }
            else
            {
                mycode.bind_all_ddl_with_id(ddl_path_root, " select  Rootname,TransportationPath_id from  TransportationPath where Transportation_Id=" + ddl_bus_name.SelectedValue + " order by Rootname asc");
            }
        }

        protected void ddl_path_root_SelectedIndexChanged(object sender, EventArgs e)
        {
            btn_final_submit.Visible = false;
            grvExcelData.DataSource = null;
            grvExcelData.DataBind();
            if (ddl_bus_name.SelectedItem.Text == "Select")
            {
                Alertme("Please select bus name", "warning");
            }
            else if (ddl_path_root.SelectedItem.Text == "Select")
            {
                Alertme("Please select transportation route", "warning");
            }
            else
            { 
                mycode.bind_all_ddl_with_id(ddl_boarding_point, " select  Boarding_Point,Boarding_Point_id from  Transportation_Boarding_Point where Transportation_Id=" + ddl_bus_name.SelectedValue + " and TransportationPath_id=" + ddl_path_root.SelectedValue + " and Session_Id='" + ddl_session.SelectedValue + "' order by Boarding_Point");
            }
        }


        protected void ddl_boarding_point_SelectedIndexChanged(object sender, EventArgs e)
        {
            btn_final_submit.Visible = false;
            grvExcelData.DataSource = null;
            grvExcelData.DataBind();
            lbl_boardingpoint.Text = "";
            lbl_kmcoverdby.Text = "";
            lbl_trasportfee.Text = "";
            if (ddl_bus_name.SelectedItem.Text == "Select")
            {
                Alertme("Please select bus Name ", "warning");
            }
            else if (ddl_path_root.SelectedItem.Text == "Select")
            {
                Alertme("Please select transportation route ", "warning");
            }
            else
            {
                string queryS = "select * from Transportation_Boarding_Point where Transportation_Id='" + ddl_bus_name.SelectedValue + "' and TransportationPath_id='" + ddl_path_root.SelectedValue + "' and Boarding_Point_id='" + ddl_boarding_point.SelectedValue + "'";
                DataTable dts = My.dataTable(queryS);
                if (dts.Rows.Count != 0)
                {
                    string ttl_vacant_seat = get_total_vacant_seat(ddl_bus_name.SelectedValue, ddl_path_root.SelectedValue);
                    lbl_boardingpoint.Text = dts.Rows[0]["Boarding_Point"].ToString();
                    lbl_kmcoverdby.Text = dts.Rows[0]["KM"].ToString();
                    lbl_trasportfee.Text = dts.Rows[0]["Amount"].ToString();
                    lbl_vacant_seat.Text = ttl_vacant_seat;
                    ViewState["VacantSeat"] = ttl_vacant_seat;
                }
                else
                {
                    lbl_boardingpoint.Text = "";
                    lbl_kmcoverdby.Text = "";
                    lbl_trasportfee.Text = "";
                    lbl_vacant_seat.Text = "";
                }
            }
        }

        private string get_total_vacant_seat(string Transportation_Id, string Path_id)
        {
            DataTable dt = My.dataTable("select count(Id) as Total_vacant from dbo.[TRANSPORT_PATH_MAPPING_WITH_SHEET] where TransportationPath_id='" + Path_id + "' and Transportation_Id='" + Transportation_Id + "' and Sheet_Status='0'");
            return dt.Rows[0][0].ToString();
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
                        qry = "select admissionserialnumber as Admission_No,studentname as Student_name,class as Class,rollnumber as Roll_no,Section,fathername as Father_name,'' as Payment_Date,'' as Payment_Mode,'' as Payment_Amount from admission_registor where Session_id='" + ddl_excel_session.SelectedValue + "' and transportationtaken='Yes'";
                    }
                    else
                    {
                        qry = "select admissionserialnumber as Admission_No,studentname as Student_name,class as Class,rollnumber as Roll_no,Section,fathername as Father_name,'' as Payment_Date,'' as Payment_Mode,'' as Payment_Amount from admission_registor where Session_id='" + ddl_excel_session.SelectedValue + "' and Class_id='" + ddl_excel_class.SelectedValue + "' and transportationtaken='Yes'";
                    }
                    DataTable dt = My.dataTable(qry);
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            string attachment = "attachment; filename=Student-list-for-monthly-fee.xls";
                            Response.ClearContent();
                            Response.AddHeader("content-disposition", attachment);
                            Response.ContentType = "application/vnd.ms-excel";
                            string tab = "";
                            foreach (DataColumn dc in dt.Columns)
                            {
                                Response.Write(tab + dc.ColumnName);
                                tab = "\t";
                            }
                            Response.Write("\n");
                            int i;
                            foreach (DataRow dr in dt.Rows)
                            {
                                tab = "";
                                for (i = 0; i < dt.Columns.Count; i++)
                                {
                                    Response.Write(tab + dr[i].ToString());
                                    tab = "\t";
                                }
                                Response.Write("\n");
                            }
                            Response.End();
                            Alertme("Student list with fee has been downloaded successfully.", "success");
                        }
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
                if (ddl_bus_name.SelectedItem.Text == "Select")
                {
                    ddl_bus_name.Focus();
                    Alertme("Please select vehicle.", "warning");
                    return;
                }
                if (ddl_path_root.SelectedItem.Text == "Select")
                {
                    ddl_path_root.Focus();
                    Alertme("Please select transport route.", "warning");
                    return;
                }
                if (ddl_boarding_point.SelectedItem.Text == "Select")
                {
                    ddl_boarding_point.Focus();
                    Alertme("Please select boarding point.", "warning");
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
                tblReadCSV.Columns.Add("Roll_no");
                tblReadCSV.Columns.Add("Section");
                tblReadCSV.Columns.Add("Father_name");
                tblReadCSV.Columns.Add("From_month");

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
                //==============

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
                        string From_month = "";

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
                            From_month = grvExcelData.Rows[i].Cells[6].Text; 
                            Dictionary<string, object> dc1 = My.get_selected_studentinfo(Admission_No, ViewState["Session_ids"].ToString(), ViewState["branch_id"].ToString());
                            string admissionserialnumber = (String)dc1["admissionserialnumber"];
                            string rollnumber = (String)dc1["rollnumber"];
                            string session = (String)dc1["session"];
                            string Section = (String)dc1["Section"];
                            string Class_id = (String)dc1["Class_id"];
                            string classname = (String)dc1["classname"];
                            string transportationtaken = (String)dc1["transportationtaken"];
                            if (transportationtaken == "Yes")
                            {
                                string Transaction_Id = My.create_random_no_otp();
                                SqlCommand cmd;
                                string query = "INSERT INTO Transport_mapping_bulk (group_transaction,From_month,Transaction_Id,Admission_no,Class_id,Session_id,Branch_id,User_id,Transport_id,Transport_path_id,Boarding_point_id,Seat_id,Seat_no,Status,createddate,Remarks) values (@group_transaction,@From_month,@Transaction_Id,@Admission_no,@Class_id,@Session_id,@Branch_id,@User_id,@Transport_id,@Transport_path_id,@Boarding_point_id,@Seat_id,@Seat_no,@Status,@createddate,@Remarks)";
                                cmd = new SqlCommand(query);
                                cmd.Parameters.AddWithValue("@group_transaction", group_transaction);
                                cmd.Parameters.AddWithValue("@From_month", From_month);
                                cmd.Parameters.AddWithValue("@Transaction_Id", Transaction_Id);
                                cmd.Parameters.AddWithValue("@Admission_no", Admission_No);
                                cmd.Parameters.AddWithValue("@Class_id", Class_id);
                                cmd.Parameters.AddWithValue("@Session_id", ViewState["Session_ids"].ToString());
                                cmd.Parameters.AddWithValue("@Branch_id", ViewState["branch_id"].ToString());
                                cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                                cmd.Parameters.AddWithValue("@Transport_id", ddl_bus_name.SelectedValue);
                                cmd.Parameters.AddWithValue("@Transport_path_id", ddl_path_root.SelectedValue);
                                cmd.Parameters.AddWithValue("@Boarding_point_id", ddl_boarding_point.SelectedValue);
                                cmd.Parameters.AddWithValue("@Seat_id", "0");
                                cmd.Parameters.AddWithValue("@Seat_no", "0");
                                cmd.Parameters.AddWithValue("@Status", "Failed");
                                cmd.Parameters.AddWithValue("@createddate", My.getdate1());
                                cmd.Parameters.AddWithValue("@Remarks", "");
                                if (InsertUpdate.InsertUpdateData(cmd))
                                {
                                }
                            }
                            else
                            {
                                if (admissionserialnumber != "0")
                                {
                                    string query2 = "Select * from Transport_mapping_bulk where Session_id=" + ViewState["Session_ids"].ToString() + "  and Class_id=" + Class_id + " and Admission_no='" + admissionserialnumber + "' and Branch_id='" + ViewState["branch_id"].ToString() + "' and Status='Pending' and group_transaction='" + group_transaction + "'";
                                    DataTable dt = mycode.FillData(query2);
                                    if (dt.Rows.Count == 0)
                                    {
                                        string Transaction_Id = My.create_random_no_otp();
                                        SqlCommand cmd;
                                        string query = "INSERT INTO Transport_mapping_bulk (group_transaction,From_month,Transaction_Id,Admission_no,Class_id,Session_id,Branch_id,User_id,Transport_id,Transport_path_id,Boarding_point_id,Seat_id,Seat_no,Status,createddate,Remarks) values (@group_transaction,@From_month,@Transaction_Id,@Admission_no,@Class_id,@Session_id,@Branch_id,@User_id,@Transport_id,@Transport_path_id,@Boarding_point_id,@Seat_id,@Seat_no,@Status,@createddate,@Remarks)";
                                        cmd = new SqlCommand(query);
                                        cmd.Parameters.AddWithValue("@group_transaction", group_transaction);
                                        cmd.Parameters.AddWithValue("@From_month", From_month);
                                        cmd.Parameters.AddWithValue("@Transaction_Id", Transaction_Id);
                                        cmd.Parameters.AddWithValue("@Admission_no", Admission_No);
                                        cmd.Parameters.AddWithValue("@Class_id", Class_id);
                                        cmd.Parameters.AddWithValue("@Session_id", ViewState["Session_ids"].ToString());
                                        cmd.Parameters.AddWithValue("@Branch_id", ViewState["branch_id"].ToString());
                                        cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                                        cmd.Parameters.AddWithValue("@Transport_id", ddl_bus_name.SelectedValue);
                                        cmd.Parameters.AddWithValue("@Transport_path_id", ddl_path_root.SelectedValue);
                                        cmd.Parameters.AddWithValue("@Boarding_point_id", ddl_boarding_point.SelectedValue);
                                        cmd.Parameters.AddWithValue("@Seat_id", "0");
                                        cmd.Parameters.AddWithValue("@Seat_no", "0");
                                        cmd.Parameters.AddWithValue("@Status", "Pending");
                                        cmd.Parameters.AddWithValue("@createddate", My.getdate1());
                                        cmd.Parameters.AddWithValue("@Remarks", "");
                                        if (InsertUpdate.InsertUpdateData(cmd))
                                        {
                                            Update_student_transport(Transaction_Id, Admission_No, Class_id, ddl_bus_name.SelectedValue, ddl_path_root.SelectedValue, ddl_boarding_point.SelectedValue, ViewState["Session_ids"].ToString(), From_month, ddl_path_root.SelectedItem.Text);
                                        }
                                    }
                                    else
                                    {
                                        Update_student_transport(dt.Rows[0]["Transaction_Id"].ToString(), dt.Rows[0]["Admission_no"].ToString(), dt.Rows[0]["Class_id"].ToString(), dt.Rows[0]["Transport_id"].ToString(), dt.Rows[0]["Transport_path_id"].ToString(), dt.Rows[0]["Boarding_point_id"].ToString(), ViewState["Session_ids"].ToString(), dt.Rows[0]["From_month"].ToString(), ddl_path_root.SelectedItem.Text);
                                    }
                                }
                                else
                                {
                                    string Transaction_Id = My.create_random_no_otp();
                                    SqlCommand cmd;
                                    string query = "INSERT INTO Transport_mapping_bulk (group_transaction,From_month,Transaction_Id,Admission_no,Class_id,Session_id,Branch_id,User_id,Transport_id,Transport_path_id,Boarding_point_id,Seat_id,Seat_no,Status,createddate,Remarks) values (@group_transaction,@From_month,@Transaction_Id,@Admission_no,@Class_id,@Session_id,@Branch_id,@User_id,@Transport_id,@Transport_path_id,@Boarding_point_id,@Seat_id,@Seat_no,@Status,@createddate,@Remarks)";
                                    cmd = new SqlCommand(query);
                                    cmd.Parameters.AddWithValue("@group_transaction", group_transaction);
                                    cmd.Parameters.AddWithValue("@From_month", From_month);
                                    cmd.Parameters.AddWithValue("@Transaction_Id", Transaction_Id);
                                    cmd.Parameters.AddWithValue("@Admission_no", Admission_No);
                                    cmd.Parameters.AddWithValue("@Class_id", Class_id);
                                    cmd.Parameters.AddWithValue("@Session_id", ViewState["Session_ids"].ToString());
                                    cmd.Parameters.AddWithValue("@Branch_id", ViewState["branch_id"].ToString());
                                    cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                                    cmd.Parameters.AddWithValue("@Transport_id", ddl_bus_name.SelectedValue);
                                    cmd.Parameters.AddWithValue("@Transport_path_id", ddl_path_root.SelectedValue);
                                    cmd.Parameters.AddWithValue("@Boarding_point_id", ddl_boarding_point.SelectedValue);
                                    cmd.Parameters.AddWithValue("@Seat_id", "0");
                                    cmd.Parameters.AddWithValue("@Seat_no", "0");
                                    cmd.Parameters.AddWithValue("@Status", "Failed");
                                    cmd.Parameters.AddWithValue("@createddate", My.getdate1());
                                    cmd.Parameters.AddWithValue("@Remarks", "");
                                    if (InsertUpdate.InsertUpdateData(cmd))
                                    {
                                    }
                                }
                            }
                        }
                        Alertme("Student has been mapped with transport successfully.", "success");
                        btn_final_submit.Visible = false;
                        string query23 = "Select Admission_no,Status,From_month,Seat_id,Seat_no  from Transport_mapping_bulk where Session_id=" + ViewState["Session_ids"].ToString() + " and User_id='" + ViewState["Userid"].ToString() + "' and Branch_id='" + ViewState["branch_id"].ToString() + "' and  group_transaction='" + group_transaction + "' ";
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
                    Alertme("Vacant seat is less than your uploaded student.", "warning");
                }
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }

        private void Update_student_transport(string transaction_Id, string admission_No, string class_id, string Transport_id, string Transport_path_id, string boarding_point, string session_id, string From_month, string path_name)
        {
            try
            {
                string session = mycode.get_session(session_id);
                string tranportassinedid = My.get_transport_assigned_id();
                string cunrt_session = session;
                string session_frst_year = cunrt_session.Substring(0, 4);
                int s_year = My.toint(session_frst_year);
                string monthid = My.tomonth_numberstring(From_month);
                int pay_month = My.toint(monthid);
                string final = s_year.ToString() + monthid;
                DataTable dt = My.dataTable("select * from TRANSPORT_PATH_MAPPING_WITH_SHEET where TransportationPath_id='" + Transport_path_id + "' and Transportation_Id='" + Transport_id + "' and Sheet_Status='0'");
                if (dt.Rows.Count > 0)
                {
                    SqlCommand cmd;
                    string query = "INSERT INTO Student_mapping_with_TransportPath (Session_id,Admission_no,Class_id,Month_name,Month_id,TransportPath_id,transport_id,Created_by,Created_date,Created_idate,branch_id,Year_month,Transport_Assigned_Id,Academic_Sem_or_Year_id,Mapping_Year,Sheet_Id,Boarding_Point_id) values (@Session_id,@Admission_no,@Class_id,@Month_name,@Month_id,@TransportPath_id,@transport_id,@Created_by,@Created_date,@Created_idate,@branch_id,@Year_month,@Transport_Assigned_Id,@Academic_Sem_or_Year_id,@Mapping_Year,@Sheet_Id,@Boarding_Point_id); INSERT INTO Student_mapping_with_TransportPath_history (Session_id,Admission_no,Class_id,Month_name,Month_id,TransportPath_id,transport_id,Remark,Created_by,Created_date,Created_idate,branch_id,Year_month,Transport_Assigned_Id,Update_Status,Academic_Sem_or_Year_id,Mapping_Year,Sheet_Id,Boarding_Point_id) values (@Session_id,@Admission_no,@Class_id,@Month_name,@Month_id,@TransportPath_id,@transport_id,@Remark,@Created_by,@Created_date,@Created_idate,@branch_id,@Year_month,@Transport_Assigned_Id,@Update_Status,@Academic_Sem_or_Year_id,@Mapping_Year,@Sheet_Id,@Boarding_Point_id)";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Session_id", session_id);
                    cmd.Parameters.AddWithValue("@Admission_no", admission_No);
                    cmd.Parameters.AddWithValue("@Class_id", class_id);
                    cmd.Parameters.AddWithValue("@Month_name", From_month);
                    cmd.Parameters.AddWithValue("@Month_id", monthid);
                    cmd.Parameters.AddWithValue("@TransportPath_id", Transport_path_id);
                    cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                    cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
                    cmd.Parameters.AddWithValue("@branch_id", ViewState["branch_id"].ToString());
                    cmd.Parameters.AddWithValue("@Year_month", final);
                    cmd.Parameters.AddWithValue("@Transport_Assigned_Id", tranportassinedid);
                    cmd.Parameters.AddWithValue("@Academic_Sem_or_Year_id", "0");
                    cmd.Parameters.AddWithValue("@Mapping_Year", s_year);
                    cmd.Parameters.AddWithValue("@Sheet_Id", dt.Rows[0]["Sheet_Id"].ToString());
                    cmd.Parameters.AddWithValue("@Remark", "Transport assigned");
                    cmd.Parameters.AddWithValue("@Update_Status", "Assigned");
                    cmd.Parameters.AddWithValue("@transport_id", Transport_id);
                    cmd.Parameters.AddWithValue("@Boarding_Point_id", boarding_point);
                    if (My.InsertUpdateData(cmd))
                    {
                        My.exeSql("update Transport_mapping_bulk set Status='success',Seat_id='" + dt.Rows[0]["Sheet_Id"].ToString() + "',Seat_no='" + dt.Rows[0]["Sheet_No"].ToString() + "' where Transaction_Id='" + transaction_Id + "' and Admission_no='" + admission_No + "' and Session_id='" + session_id + "'; update Transport_Path_Mapping_With_Sheet set Sheet_Status=1 where TransportationPath_id=" + Transport_path_id + " and Transportation_Id=" + Transport_id + " and Sheet_Id=" + dt.Rows[0]["Sheet_Id"].ToString() + "");
                        try
                        {
                            SqlCommand cmd1;
                            string query1 = "Update admission_registor set Hostel_id=@Hostel_id,Transportation_Id=@Transportation_Id,Transportationpath=@Transportationpath,transportationtaken=@transportationtaken,hosteltaken=@hosteltaken where admissionserialnumber=@admissionserialnumber and Session_id=@Session_id and Class_id=" + class_id + " ";
                            cmd1 = new SqlCommand(query1);
                            cmd1.Parameters.AddWithValue("@Transportation_Id", Transport_id);
                            cmd1.Parameters.AddWithValue("@Transportationpath", path_name);
                            cmd1.Parameters.AddWithValue("@transportationtaken", "Yes");
                            cmd1.Parameters.AddWithValue("@hosteltaken", "No");
                            cmd1.Parameters.AddWithValue("@Hostel_id", "0");
                            cmd1.Parameters.AddWithValue("@Session_id", session_id);
                            cmd1.Parameters.AddWithValue("@admissionserialnumber", admission_No);
                            if (My.InsertUpdateData(cmd1))
                            {
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
            catch
            {

            }
        }

    }
}