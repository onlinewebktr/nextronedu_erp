using CsvHelper;
using ExcelDataReader;
using Microsoft.VisualBasic.FileIO;
using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Online_Test_admin
{
    public partial class Upload_ExcelBulk_Questions : System.Web.UI.Page
    {
        string scrpt;
        My mycode = new My();
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
                if (!IsPostBack)
                {
                    ViewState["Userid"] = Session["Admin"].ToString();
                    hd_userid.Value = ViewState["Userid"].ToString();
                    string examid = Request.QueryString["examid"];
                    ViewState["entryid"] = examid;
                    if (!String.IsNullOrEmpty(examid))
                    {
                        hd_exam_type.Value = "4";//PT
                        hd_test_id.Value = examid;
                        hd_question_id.Value = examid;
                        hd_section_id.Value = Request.QueryString["Section"];
                        hd_sessionid.Value = Request.QueryString["Session_id"];
                        hd_class_id.Value = Request.QueryString["Class_id"];
                        mycode.bind_ddlall(ddl_section, "Select distinct Section  from OLINETEST_EXAM_NAME where Entry_id = '" + ViewState["entryid"].ToString() + "' order by Section");
                       try
                        {
                            ddl_section.Text = Request.QueryString["Section"];
                        }
                        catch
                        {

                        }
                       


                        Bind_pageload();
                    }
                    else
                    {
                        Response.Redirect("Upload_Question.aspx", false);
                    }
                }
            }
        }

        private void Bind_pageload()
        {
            try
            {
                var condition = "where 1=1 ";
                condition += $" and  Session_id='{hd_sessionid.Value}' ";
                condition += $" and Class_id='{hd_class_id.Value}' ";
                condition += $" and Entry_id='{ViewState["entryid"].ToString()}' ";
                condition += $" and Section='{ddl_section.Text}' ";
                DataTable dt = My.MydataTable($@"select oen.*,(select top 1 Session from session_details where session_id=oen.Session_id) as session,format(oen.live_date,'dd/MM/yyyy') as live_date_one,format(oen.live_date,'hh:mm tt') as live_time_one,ad.Course_Name,(select top 1 Subject_name from Subject_Master where Subject_id=oen.subjectname) as subject_name from  OLINETEST_EXAM_NAME_Murge_Section oen join  Add_course_table ad  on oen.Class_id=ad.course_id  {condition} order by ad.Position,oen.Exam_name ");
                if (dt.Rows.Count == 0)
                {
                    Alertme("Sorry there are no record exist", "warning");
                }
                else
                {
                    lbl_exmaname.Text = dt.Rows[0]["Exam_name"].ToString();
                    lbl_classname.Text = dt.Rows[0]["Course_Name"].ToString();
                    if (dt.Rows[0]["subject_name"].ToString() == "0")
                    {
                        lbl_subject.Text = "All Subject";
                    }
                    else if (dt.Rows[0]["subject_name"].ToString() == "")
                    {
                        lbl_subject.Text = "All Subject";
                    }
                    else
                    {
                        lbl_subject.Text = dt.Rows[0]["subject_name"].ToString();
                    }

                    //lbl_Section.Text = dt.Rows[0]["Section"].ToString();
                    // if not final submit
                    find_if_not_final_submit();

                }
            }
            catch
            {
                
            }
        }

        private void find_if_not_final_submit()
        {

            string testid1 = My.get_test_id_from_entry_id(ViewState["entryid"].ToString());


            pnl_grd.Visible = false;
            string query1 = "select test_id,Objective_Entry_id from question_info  where test_id='" + testid1 + "'  and Uploding_status in ('Uploaded','Administrator')  ";//and Language_Itype=0
            SqlCommand cmd = new SqlCommand(query1);//and Section='" + hd_section_id.Value + "'
            DataTable dt = mycode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                string testid = hd_test_id.Value;
                string sectionid = hd_test_id.Value;
                string examtype = hd_exam_type.Value;
                btn_s_add.Visible = true;
            }
            else
            {

                string testid = hd_test_id.Value;
                string sectionid = hd_test_id.Value;
                string examtype = hd_exam_type.Value;
                btn_s_add.Visible = false;

            }
        }

        protected void btn_s_add_Click(object sender, EventArgs e)
        {
            lblmsg.Text = "";
            try
            {
                if (FileUpload1.HasFile)
                {
                    upload_excel_file();
                }
                else
                {
                    Alertme("Please chose excel file", "warning");
                }
            }
            catch
            {

            }


        }

        private void upload_excel_file()
        {
            string file = upload_csv_data();
            if (file != "")
            {
                string csvid = My.auto_serialS("csvid");

                SqlCommand cmd;
                string strQuery = @"INSERT INTO Upload_csv_Question (filedata,Date,idate,time,Status,Userid,testid) values (@filedata,@Date,@idate,@time,@Status,@Userid,@testid)";
                cmd = new SqlCommand(strQuery);
                cmd.Parameters.AddWithValue("@filedata", file);
                cmd.Parameters.AddWithValue("@Date", mycode.date());
                cmd.Parameters.AddWithValue("@idate", mycode.idate());
                cmd.Parameters.AddWithValue("@time", mycode.time());
                cmd.Parameters.AddWithValue("@Status", "SUBMITTED");
                cmd.Parameters.AddWithValue("@Userid", hd_userid.Value);
                cmd.Parameters.AddWithValue("@testid", hd_test_id.Value);
                if (My.InsertUpdateData(cmd))
                {
                }
            }
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
            Session["file"] = ("Upload_question_from_excel" + filerename + FileExtension);
            String[] allowedExtensions = { ".csv"  , ".xlsx" };
           
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
                    string path = (Server.MapPath("../UploadedImage/uploads")).ToString();
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
                    My.submitException(ex, "check_wrap_or_not");
                    FileSaved = false;


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
                dbfilePath = @"/UploadedImage/uploads/" + fileName;
                Session["fileName"] = fileName;
            }
            return dbfilePath;
        }

        private bool check_wrap_or_not(string path)
        {
            try
            {

                string qry = "";

                DataTable tblReadCSV = new DataTable();
                tblReadCSV.Columns.Add("Question_name");
                    tblReadCSV.Columns.Add("Option1");
                    tblReadCSV.Columns.Add("Option2");
                    tblReadCSV.Columns.Add("Option3");
                    tblReadCSV.Columns.Add("Option4");
                    tblReadCSV.Columns.Add("AnswerOption");
                    tblReadCSV.Columns.Add("Explanation");
                if (path.EndsWith(".csv"))
                {
                    
                    using (var reader = new StreamReader(path, System.Text.Encoding.UTF8)) // UTF-8 for Unicode
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        csv.Read(); // Skip header row
                        csv.ReadHeader();

                        while (csv.Read())
                        {
                            tblReadCSV.Rows.Add(
                                csv.GetField("Question_name"),
                                csv.GetField("Option1"),
                                csv.GetField("Option2"),
                                csv.GetField("Option3"),
                                csv.GetField("Option4"),
                                csv.GetField("AnswerOption"),
                                csv.GetField("Explanation")
                            );
                        }
                    }

                }
                else if (path.EndsWith(".xlsx"))
                {
                    var tbl = ReadExcelToDataTable(path);
                    foreach(DataRow dr in tbl.Rows)
                    {
                        tblReadCSV.Rows.Add(dr.ItemArray);
                    }
                }
               
                //TextFieldParser csvParser = new TextFieldParser(path);
                //csvParser.Delimiters = new string[] { "," };
                //csvParser.TrimWhiteSpace = true;
                //csvParser.ReadLine();
                //while (!(csvParser.EndOfData == true))
                //{
                //    tblReadCSV.Rows.Add(csvParser.ReadFields());

                //}

               


                pnl_grd.Visible = true;
                lbl_total_question.Text = "Total Question:- " + tblReadCSV.Rows.Count.ToString();
                grvExcelData.DataSource = tblReadCSV;
                grvExcelData.DataBind();
                Save_data_Upload_Question_Question_Temp();
            }
            catch (Exception e)
            {
                My.submitException(e, "check_wrap_or_not enter");
                Alertme(e.ToString(), "warning");
                return false;
            }
            return true;
        }

        public static DataTable ReadExcelToDataTable(string filePath)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var conf = new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration
                        {
                            UseHeaderRow = true // treat the first row as column names
                        }
                    };

                    var result = reader.AsDataSet(conf);
                    return result.Tables[0]; // returns the first worksheet as DataTable
                }
            }
        }
        private void Save_data_Upload_Question_Question_Temp()
        {
            save_data_temptable();
            bind_grid_data();
            btn_final_butmit.Visible = true;
        }

        private void save_data_temptable()
        {
            string group_entryid = My.auto_serialS("Online_Objective_Entry_id");
            string testid1 = My.get_test_id_from_entry_id(ViewState["entryid"].ToString());
            hd_marks.Value = mycode.get_marks(testid1);
            int a = 1;
            int b = 1;
            string section1 = "";
            mycode.executequery("Delete from Upload_Question_Question_Temp where user_id='" + hd_userid.Value + "'and Test_id='" + hd_test_id.Value + "' ");
            for (int i = 0; i < grvExcelData.Rows.Count; i++)
            {

                if (grvExcelData.Rows[i].Cells[0].Text == "&nbsp;")
                {
                    a = 1 * 0;

                }
                else if (grvExcelData.Rows[i].Cells[1].Text == "&nbsp;")
                {
                    a = 1 * 0;
                }
                else if (grvExcelData.Rows[i].Cells[2].Text == "&nbsp;")
                {
                    a = 1 * 0;
                }
                else if (grvExcelData.Rows[i].Cells[3].Text == "&nbsp;")
                {
                    a = 1 * 0;
                }
                else if (grvExcelData.Rows[i].Cells[4].Text == "&nbsp;")
                {
                    a = 1 * 0;
                }
                else if (grvExcelData.Rows[i].Cells[5].Text == "&nbsp;")
                {
                    a = 1 * 0;
                }
                else if (grvExcelData.Rows[i].Cells[6].Text == "&nbsp;")
                {
                    a = 1 * 0;
                }
                else
                {

                    if (grvExcelData.Rows[i].Cells[5].Text.Equals("Option1", StringComparison.OrdinalIgnoreCase))
                    {
                        b = 1 * 1;
                    }
                    else if (grvExcelData.Rows[i].Cells[5].Text.Equals("Option2", StringComparison.OrdinalIgnoreCase))
                    {
                        b = 1 * 1;
                    }
                    else if (grvExcelData.Rows[i].Cells[5].Text.Equals("Option3", StringComparison.OrdinalIgnoreCase))
                    {
                        b = 1 * 1;
                    }
                    else if (grvExcelData.Rows[i].Cells[5].Text.Equals("Option4", StringComparison.OrdinalIgnoreCase))
                    {
                        b = 1 * 1;
                    }
                    else
                    {
                        b = 1 * 0;
                    }


                }
                int final = a * b;
                section1 = ddl_section.Text;
                
                SqlCommand cmd;
                string strQuery = @"INSERT INTO Upload_Question_Question_Temp (Question_name,Option1,Option2,Option3,Option4,AnswerOption,Explanation,Status,Test_id,date,idate,user_id,row_error,sub_id,section,Exam_id,class_id,Objective_Entry_id,Marks) values (@Question_name,@Option1,@Option2,@Option3,@Option4,@AnswerOption,@Explanation,@Status,@Test_id,@date,@idate,@user_id,@row_error,@sub_id,@section,@Exam_id,@class_id,@Objective_Entry_id,@Marks)";
                cmd = new SqlCommand(strQuery);
                cmd.Parameters.AddWithValue("@Question_name", grvExcelData.Rows[i].Cells[0].Text);
                cmd.Parameters.AddWithValue("@Option1", grvExcelData.Rows[i].Cells[1].Text);
                cmd.Parameters.AddWithValue("@Option2", grvExcelData.Rows[i].Cells[2].Text);
                cmd.Parameters.AddWithValue("@Option3", grvExcelData.Rows[i].Cells[3].Text);
                cmd.Parameters.AddWithValue("@Option4", grvExcelData.Rows[i].Cells[4].Text);
                cmd.Parameters.AddWithValue("@AnswerOption", grvExcelData.Rows[i].Cells[5].Text);
                cmd.Parameters.AddWithValue("@Explanation", grvExcelData.Rows[i].Cells[6].Text);
                cmd.Parameters.AddWithValue("@Status", "Pending");
                cmd.Parameters.AddWithValue("@Test_id", hd_test_id.Value);
                cmd.Parameters.AddWithValue("@date", mycode.date());
                cmd.Parameters.AddWithValue("@idate", mycode.idate());
                cmd.Parameters.AddWithValue("@user_id", hd_userid.Value);
                cmd.Parameters.AddWithValue("@row_error", a);
                cmd.Parameters.AddWithValue("@sub_id", "01");
                cmd.Parameters.AddWithValue("@section", section1);
                cmd.Parameters.AddWithValue("@Exam_id", hd_test_id.Value);
                cmd.Parameters.AddWithValue("@class_id", hd_class_id.Value);
                cmd.Parameters.AddWithValue("@Objective_Entry_id", group_entryid);
                cmd.Parameters.AddWithValue("@Marks", hd_marks.Value);
                
                if (My.InsertUpdateData(cmd))
                {

                }



            }

        }





        private void bind_grid_data()
        {
            SqlCommand cmd = new SqlCommand("Select * from Upload_Question_Question_Temp where Test_id='" + hd_test_id.Value + "'  ");
            DataTable dtTemp = mycode.GetData(cmd);
            if (dtTemp.Rows.Count == 0)
            {
                GrdView.DataSource = null;
                GrdView.DataBind();
                btn_final_butmit.Visible = false;
            }
            else
            {
                GrdView.DataSource = dtTemp;
                GrdView.DataBind();
                btn_final_butmit.Visible = true;
            }
        }




        protected void lnkDel_Click1(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lnk.Parent.Parent;
                Label Id = (Label)row.FindControl("lbl_id");
                Label lbl_Objective_Entry_id = (Label)row.FindControl("lbl_Objective_Entry_id");
                mycode.executequery("delete from Upload_Question_Question_Temp where Objective_Entry_id=" + lbl_Objective_Entry_id.Text + " and Id="+ Id.Text + "");
                Alertme("Question has been delete", "warning");

                bind_grid_data();
            }
            catch { }
        }

        protected void GrdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_Status = (Label)e.Row.FindControl("lbl_Status");
                Label lbl_error = (Label)e.Row.FindControl("lbl_row_error");
                foreach (TableCell cell in e.Row.Cells)
                {
                    if (lbl_error.Text == "0")
                    {
                        cell.BackColor = Color.Red;
                        cell.ForeColor = Color.White;
                    }
                    else
                    {
                        if (lbl_Status.Text == "Pending")
                        {

                            cell.BackColor = Color.White;
                            cell.ForeColor = Color.Black;

                        }
                        else
                        {
                            cell.BackColor = Color.LightGreen;
                            cell.ForeColor = Color.Black;
                        }
                    }



                }
            }
        }


        #region Final Copy

        protected void btn_final_butmit_Click(object sender, EventArgs e)
        {
            bool final = false;
            try
            {
                hd_marks.Value = mycode.get_marks(hd_test_id.Value);
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
                {
                    SqlConnection con = new SqlConnection(My.conn);
                    con.Open();

                    string query = "";

                    if(ddl_section.Text=="ALL")
                    {
                        query = "Select * from OLINETEST_EXAM_NAME where Entry_id='" + ViewState["entryid"].ToString() + "'";
                    }
                    else
                    {
                        query = "Select * from OLINETEST_EXAM_NAME where Entry_id='" + ViewState["entryid"].ToString() + "' and Section='"+ddl_section.Text+"'";
                    }


                    DataTable cdt = payments.dataTable(query, con);
                    if (cdt.Rows.Count == 0)
                    {

                    }
                    else
                    {
                        for (int j = 0; j < cdt.Rows.Count; j++)
                        {
                            string Test_id = cdt.Rows[j]["Exam_id"].ToString();
                            string Exam_id = cdt.Rows[j]["Exam_id"].ToString();
                            string section = cdt.Rows[j]["Section"].ToString();
                            string query2 = "Select * from Upload_Question_Question_Temp where user_id='" + hd_userid.Value + "' and idate='" + mycode.idate() + "' and Status='Pending' and Test_id='" + hd_test_id.Value + "'";
                            DataTable dtTemp = payments.dataTable(query2, con);
                            if (dtTemp.Rows.Count == 0)
                            {

                            }
                            else
                            {
                                for (int i = 0; i < dtTemp.Rows.Count; i++)
                                {
                                    string Question_name = dtTemp.Rows[i]["Question_name"].ToString();
                                    string Option1 = dtTemp.Rows[i]["Option1"].ToString();
                                    string Option2 = dtTemp.Rows[i]["Option2"].ToString();
                                    string Option3 = dtTemp.Rows[i]["Option3"].ToString();
                                    string Option4 = dtTemp.Rows[i]["Option4"].ToString();
                                    string AnswerOption = dtTemp.Rows[i]["AnswerOption"].ToString();
                                    string Marks = hd_marks.Value; //dtTemp.Rows[i]["Marks"].ToString();
                                    string Explanation = dtTemp.Rows[i]["Explanation"].ToString();
                                    string Id = dtTemp.Rows[i]["Id"].ToString();
                                    string sub_id = dtTemp.Rows[i]["sub_id"].ToString();
                                   
                                    string class_id = dtTemp.Rows[i]["class_id"].ToString();
                                    string row_error = dtTemp.Rows[i]["row_error"].ToString();
                                    string Objective_Entry_id = dtTemp.Rows[i]["Objective_Entry_id"].ToString();
                                    if (row_error == "0")
                                    {

                                    }
                                    else
                                    {
                                        updated_fina_data(Question_name, Option1, Option2, Option3, Option4, AnswerOption, Marks, Explanation, Id, Exam_id, class_id, Test_id, section, sub_id, Objective_Entry_id, con);

                                    }
                                }
                                update_question_no(con, Test_id);
                                string query1 = "update Question_Upload_History set Status='Final Copy',Final_Submited_Date='" + mycode.date() + "',Final_Submited_Idate='" + mycode.idate() + "',Final_Submited_time='" + mycode.time() + "' where Test_id=" + Test_id + "";
                                payments.exeSql(query1, con);
                               
                            }
                        }
                        payments.exeSql("update Upload_Question_Question_Temp set Status='Verified' where Test_id='" + hd_test_id.Value + "'", con);
                        final = true;
                        con.Close();
                        scope.Complete();
                    }
                }
                if (final == true)
                {
                    Alertme("Question has been successfully added", "success");
                    bind_grid_data();
                    btn_final_butmit.Visible = false;

                }
            }
            catch(Exception ex)
            {
                My.submitException(ex, "upload question");

            }

        }

        private void update_question_no(SqlConnection con,string Test_id)
        {
            Add_Section_Arranging(con, Test_id);
            String query = "select * from Section_Arranging where Test_id='" + Test_id + "' order by Position";
            SqlDataAdapter ad = new SqlDataAdapter(query, con);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            int question_sl_no = 1;
            foreach (DataRow dr in dt.Rows)
            {
                question_sl_no = update_question_no(question_sl_no, dr["Test_id"].ToString(), dr["Section_name"].ToString(), con);
            }
        }
        Find_Section_Position fsp = new Find_Section_Position();
        private void Add_Section_Arranging(SqlConnection con,string testid)
        {
            try
            {
                string examtypecode = "4";// PT
                string testmode_code = "101";
                
                string position = fsp.no_position(testid, con);


                SqlCommand cmd = new SqlCommand("Select * from Section_Arranging where Test_id=" + testid + " and Section_name='" + lbl_subject.Text + "' ");
                DataTable dt = payments.GetData(cmd, con);
                if (dt.Rows.Count == 0)
                {
                    SqlCommand cmd1;
                    string strQuery = @"INSERT INTO Section_Arranging (Test_id,Section_name,Position,created_by,created_date,created_Idate,updated_date,updated_Idate) values (@Test_id,@Section_name,@Position,@created_by,@created_date,@created_Idate,@updated_date,@updated_Idate)";
                    cmd1 = new SqlCommand(strQuery);
                    cmd1.Parameters.AddWithValue("@Test_id", testid);
                    cmd1.Parameters.AddWithValue("@Section_name", lbl_subject.Text);
                    cmd1.Parameters.AddWithValue("@Position", position);
                    cmd1.Parameters.AddWithValue("@created_by", ViewState["Userid"].ToString());
                    cmd1.Parameters.AddWithValue("@created_date", mycode.date());
                    cmd1.Parameters.AddWithValue("@created_Idate", mycode.idate());
                    cmd1.Parameters.AddWithValue("@updated_by", ViewState["Userid"].ToString());
                    cmd1.Parameters.AddWithValue("@updated_date", mycode.date());
                    cmd1.Parameters.AddWithValue("@updated_Idate", mycode.idate());
                    if (payments.InsertUpdateData(cmd1, con))
                    {
                    }
                }

            }
            catch
            {

            }
        }

        private int update_question_no(int question_sl_no, string tesid, string section, SqlConnection con)
        {
            String query = "select * from question_info where test_id='" + tesid + "' ";//and Section='" + section + "'
            SqlDataAdapter ad = new SqlDataAdapter(query, con);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                dr["isActive"] = 1;
                dr["Status"] = "1";
                dr["Question_no"] = question_sl_no++;
                dr["Uploding_status"] = "Administrator";
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
            return question_sl_no;

        }

        private void updated_fina_data(string Question_name, string Option1, string Option2, string Option3, string Option4, string AnswerOption, string Marks, string Explanation, string Id, string Exam_id, string class_id, string Test_id, string section, string sub_id, string Objective_Entry_id, SqlConnection con)
        {
            string entryid = "";
            string language_type = "Single";
            string language_id = "0";

            string query1 = "Select entry_id from question_info where test_id='" + Test_id + "'  ";
            SqlDataAdapter ad1 = new SqlDataAdapter(query1, con);
            DataSet ds1 = new DataSet();
            ad1.Fill(ds1);
            DataTable dt1 = ds1.Tables[0];
            if (dt1.Rows.Count == 0)
            {
                entryid = payments.auto_serialS("Online_entry_id", con);
            }
            else
            {

                entryid = dt1.Rows[0]["entry_id"].ToString();
            }
            string testmodesection = Test_id; // hd_test_id.Value;
            string question_id = payments.auto_serialS("Online_questionid", con);
            string query = @"INSERT INTO question_info (test_id,Direction,Question_name,Answer,questionid,created_by,created_date,created_idate,updated_by,updated_date,updated_idate,isActive,Status,Section,marks,DI,Type, Uploding_status,Direction_HN,Question_name_HN,DI_HN,Language_type,Language_Itype,entry_id,Upload_Type,Create_time,question_img_EN,question_img_HN,Question_name_EN_sing,Question_name_SN_sing,ans,Option_type,Option_Itype,Objective_Entry_id) 
                                                values (@test_id,@Direction,@Question_name,@Answer,@questionid,@created_by,@created_date,@created_idate,@updated_by,@updated_date,@updated_idate,@isActive,@Status,@Section,@marks,@DI,@Type, @Uploding_status,@Direction_HN,@Question_name_HN,@DI_HN,@Language_type,@Language_Itype,@entry_id,@Upload_Type,@Create_time,@question_img_EN,@question_img_HN,@Question_name_EN_sing,@Question_name_SN_sing,@ans,@Option_type,@Option_Itype,@Objective_Entry_id);";
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@entry_id", entryid);
            cmd.Parameters.AddWithValue("@Direction_id", "0");
            cmd.Parameters.AddWithValue("@Phrase_id", "0");
            cmd.Parameters.AddWithValue("@test_id", Test_id);
            cmd.Parameters.AddWithValue("@questionid", question_id);
            cmd.Parameters.AddWithValue("@Direction", "");
            cmd.Parameters.AddWithValue("@Direction_HN", "");
            cmd.Parameters.AddWithValue("@DI", "");
            cmd.Parameters.AddWithValue("@DI_HN", "");
            cmd.Parameters.AddWithValue("@Type", "None");
            cmd.Parameters.AddWithValue("@Question_name", Question_name);
            cmd.Parameters.AddWithValue("@Question_name_HN", "");
            cmd.Parameters.AddWithValue("@question_img_EN", "");
            cmd.Parameters.AddWithValue("@question_img_HN", "");
            cmd.Parameters.AddWithValue("@Question_name_EN_sing", Question_name);
            cmd.Parameters.AddWithValue("@Question_name_SN_sing", "");
            cmd.Parameters.AddWithValue("@Answer", AnswerOption);
            if (AnswerOption.Equals("Option1", StringComparison.OrdinalIgnoreCase))
            {
                cmd.Parameters.AddWithValue("@ans", Option1);

            }
            else if (AnswerOption.Equals("Option2", StringComparison.OrdinalIgnoreCase))
            {
                cmd.Parameters.AddWithValue("@ans", Option2);

            }
            else if (AnswerOption.Equals("Option3", StringComparison.OrdinalIgnoreCase))
            {
                cmd.Parameters.AddWithValue("@ans", Option3);

            }
            else if (AnswerOption.Equals("Option4", StringComparison.OrdinalIgnoreCase))
            {
                cmd.Parameters.AddWithValue("@ans", Option4);

            }
            cmd.Parameters.AddWithValue("@created_by", hd_userid.Value);
            cmd.Parameters.AddWithValue("@created_date", mycode.date());
            cmd.Parameters.AddWithValue("@created_idate", mycode.idate());
            cmd.Parameters.AddWithValue("@updated_by", hd_userid.Value);
            cmd.Parameters.AddWithValue("@updated_date", mycode.date());
            cmd.Parameters.AddWithValue("@updated_idate", mycode.idate());
            cmd.Parameters.AddWithValue("@isActive", 0);
            cmd.Parameters.AddWithValue("@Status", "Pending");
            cmd.Parameters.AddWithValue("@Section", testmodesection);
            cmd.Parameters.AddWithValue("@marks", Marks);//check code=get new table from Subject_Mapping_with_Class_Exam
            cmd.Parameters.AddWithValue("@Uploding_status", "Uploaded");
            cmd.Parameters.AddWithValue("@Language_type", language_type);
            cmd.Parameters.AddWithValue("@Language_Itype", language_id);
            cmd.Parameters.AddWithValue("@Upload_Type", "Single");
            cmd.Parameters.AddWithValue("@Create_time", mycode.time());
            cmd.Parameters.AddWithValue("@Option_type", "Textual");
            cmd.Parameters.AddWithValue("@Option_Itype", "0");
            cmd.Parameters.AddWithValue("@Objective_Entry_id", Objective_Entry_id);
            if (payments.InsertUpdateData(cmd, con))
            {
                
                save_option_master(question_id, entryid, Option1, Option2, Option3, Option4, AnswerOption, testmodesection, Test_id, Objective_Entry_id, con);
                if (Explanation == "&nbsp;")
                {
                }
                else
                {
                    Save_Explanation(question_id, entryid, language_type, language_id, Explanation, testmodesection, Test_id, Objective_Entry_id, con);
                }
                Save_question_history(question_id, entryid, language_type, language_id, Exam_id, class_id, Test_id, section, sub_id, con);

            }
        }
        private void Save_question_history(string question_id, string entryid, string language_type, string language_id, string Exam_id, string class_id, string Test_id, string section, string sub_id, SqlConnection con)
        {


            SqlCommand cmd = new SqlCommand("Select * from Question_Upload_History where Sub_id=" + sub_id + " and Test_id=" + Test_id + "");


            DataTable dt = payments.GetData(cmd, con);
            if (dt.Rows.Count == 0)
            {

                SqlCommand cmd1;
                string strQuery = @"INSERT INTO Question_Upload_History (Exam_Id,Class_Id,Sub_id,Session_Id,section,Create_Date,Create_idate,Create_time,Status,Created_By,Test_id) values (@Exam_Id,@Class_Id,@Sub_id,@Session_Id,@section,@Create_Date,@Create_idate,@Create_time,@Status,@Created_By,@Test_id)";
                cmd1 = new SqlCommand(strQuery);
                cmd1.Parameters.AddWithValue("@Exam_Id", Exam_id);
                cmd1.Parameters.AddWithValue("@Class_Id", class_id);
                cmd1.Parameters.AddWithValue("@Sub_id", sub_id);
                cmd1.Parameters.AddWithValue("@Session_Id", hd_sessionid.Value);
                cmd1.Parameters.AddWithValue("@section", section);
                cmd1.Parameters.AddWithValue("@Create_Date", mycode.date());
                cmd1.Parameters.AddWithValue("@Create_idate", mycode.idate());
                cmd1.Parameters.AddWithValue("@Create_time", mycode.time());
                cmd1.Parameters.AddWithValue("@Status", "Draft");
                cmd1.Parameters.AddWithValue("@Created_By", hd_userid.Value);
                cmd1.Parameters.AddWithValue("@Test_id", Test_id);
                if (payments.InsertUpdateData(cmd1, con))
                {

                }
            }
            else
            {

                SqlCommand cmd1;
                string strQuery = @"Update Question_Upload_History set Create_Date=@Create_Date,Create_idate=@Create_idate,Create_time=@Create_time where Exam_Id=@Exam_Id and Class_Id=@Class_Id and Sub_id=@Sub_id and Test_id=@Test_id ";
                cmd1 = new SqlCommand(strQuery);
                cmd1.Parameters.AddWithValue("@Exam_Id", Exam_id);
                cmd1.Parameters.AddWithValue("@Class_Id", class_id);
                cmd1.Parameters.AddWithValue("@Sub_id", sub_id);
                cmd1.Parameters.AddWithValue("@Create_Date", mycode.date());
                cmd1.Parameters.AddWithValue("@Create_idate", mycode.idate());
                cmd1.Parameters.AddWithValue("@Create_time", mycode.time());
                cmd1.Parameters.AddWithValue("@Status", "Draft");
                cmd1.Parameters.AddWithValue("@Created_By", hd_userid.Value);
                cmd1.Parameters.AddWithValue("@Test_id", Test_id);
                if (payments.InsertUpdateData(cmd1, con))
                {

                }

            }
        }
        private void Save_Explanation(string question_id, string entryid, string language_type, string language_id, string Explanation, string testmodesection, string Test_id, string Objective_Entry_id, SqlConnection con)
        {
            string query1 = "Delete from Question_Explanation where test_id='" + Test_id + "' and questionid='" + question_id + "'   ";
            payments.exeSql(query1, con);
            string query = @"INSERT INTO Question_Explanation (test_id,Question_SL,Section,questionid,Explanation_en,Explanation_hn,Status,isActive,Uploding_status,Created_by,Created_by_id,Faculty_id,Faculty_hod_id,Language_type,Language_Itype,entry_id,Type,Objective_Entry_id) values (@test_id,@Question_SL,@Section,@questionid,@Explanation_en,@Explanation_hn,@Status,@isActive,@Uploding_status,@Created_by,@Created_by_id,@Faculty_id,@Faculty_hod_id,@Language_type,@Language_Itype,@entry_id,@Type,@Objective_Entry_id)";

            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@test_id", Test_id);
            cmd.Parameters.AddWithValue("@Question_SL", "0");
            cmd.Parameters.AddWithValue("@Section", testmodesection);
            cmd.Parameters.AddWithValue("@questionid", question_id);

            cmd.Parameters.AddWithValue("@Explanation_en", Explanation);
            cmd.Parameters.AddWithValue("@Explanation_hn", "");
            cmd.Parameters.AddWithValue("@Type", "Textual");

            cmd.Parameters.AddWithValue("@Status", "Pending");
            cmd.Parameters.AddWithValue("@isActive", "0");
            cmd.Parameters.AddWithValue("@Uploding_status", "Uploaded");
            cmd.Parameters.AddWithValue("@Created_by", hd_userid.Value);
            cmd.Parameters.AddWithValue("@Created_by_id", hd_userid.Value);
            cmd.Parameters.AddWithValue("@Faculty_id", "0");
            cmd.Parameters.AddWithValue("@Faculty_hod_id", "0");
            cmd.Parameters.AddWithValue("@Language_type", language_type);
            cmd.Parameters.AddWithValue("@Language_Itype", language_id);
            cmd.Parameters.AddWithValue("@entry_id", entryid);
            cmd.Parameters.AddWithValue("@Objective_Entry_id", Objective_Entry_id);
            if (payments.InsertUpdateData(cmd, con))
            {
            }
        }
        private void save_option_master(string question_id, string entryid, string Option1, string Option2, string Option3, string Option4, string AnswerOption, string testmodesection, string Test_id, string Objective_Entry_id, SqlConnection con)
        {
            string query1 = "Delete from question_answer_Master where test_code='" + Test_id + "' and  quest_code='" + question_id + "'";
            payments.exeSql(query1, con);

            //-----------------------------Option1----------------------------------
            string query = @"INSERT INTO question_answer_Master (Section,test_code,quest_code,opt_code,option_text,created_date,created_idate,created_by,updated_date,Updateidate,opetion_text_HN,Option_type,Option_Itype,entry_id,Objective_Entry_id) values (@Section,@test_code,@quest_code,@opt_code,@option_text,@created_date,@created_idate,@created_by,@updated_date,@Updateidate,@opetion_text_HN,@Option_type,@Option_Itype,@entry_id,@Objective_Entry_id)";
            string optionid = payments.auto_serialS("option_id", con);
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Section", testmodesection);
            cmd.Parameters.AddWithValue("@test_code", Test_id);
            cmd.Parameters.AddWithValue("@quest_code", question_id);
            cmd.Parameters.AddWithValue("@opt_code", optionid);
            cmd.Parameters.AddWithValue("@Option_type", "Textual");
            cmd.Parameters.AddWithValue("@Option_Itype", "0");
            cmd.Parameters.AddWithValue("@option_text", Option1);
            cmd.Parameters.AddWithValue("@opetion_text_HN", "");
            cmd.Parameters.AddWithValue("@created_date", mycode.date());
            cmd.Parameters.AddWithValue("@created_idate", mycode.idate());
            cmd.Parameters.AddWithValue("@created_by", hd_userid.Value);
            cmd.Parameters.AddWithValue("@updated_by", hd_userid.Value);
            cmd.Parameters.AddWithValue("@updated_date", mycode.date());
            cmd.Parameters.AddWithValue("@Updateidate", mycode.idate());
            cmd.Parameters.AddWithValue("@entry_id", entryid);
            cmd.Parameters.AddWithValue("@Objective_Entry_id", Objective_Entry_id);
            if (payments.InsertUpdateData(cmd, con))
            {
            }
            //-----------------------------Option2----------------------------------
            query = @"INSERT INTO question_answer_Master (Section,test_code,quest_code,opt_code,option_text,created_date,created_idate,created_by,updated_date,Updateidate,opetion_text_HN,Option_type,Option_Itype,entry_id,Objective_Entry_id) values (@Section,@test_code,@quest_code,@opt_code,@option_text,@created_date,@created_idate,@created_by,@updated_date,@Updateidate,@opetion_text_HN,@Option_type,@Option_Itype,@entry_id,@Objective_Entry_id)";
            optionid = payments.auto_serialS("option_id", con);
            SqlCommand cmd1 = new SqlCommand(query);
            cmd1.Parameters.AddWithValue("@Section", testmodesection);
            cmd1.Parameters.AddWithValue("@test_code", Test_id);
            cmd1.Parameters.AddWithValue("@quest_code", question_id);
            cmd1.Parameters.AddWithValue("@opt_code", optionid);
            cmd1.Parameters.AddWithValue("@Option_type", "Textual");
            cmd1.Parameters.AddWithValue("@Option_Itype", "0");
            cmd1.Parameters.AddWithValue("@option_text", Option2);
            cmd1.Parameters.AddWithValue("@opetion_text_HN", "");
            cmd1.Parameters.AddWithValue("@created_date", mycode.date());
            cmd1.Parameters.AddWithValue("@created_idate", mycode.idate());
            cmd1.Parameters.AddWithValue("@created_by", hd_userid.Value);
            cmd1.Parameters.AddWithValue("@updated_by", hd_userid.Value);
            cmd1.Parameters.AddWithValue("@updated_date", mycode.date());
            cmd1.Parameters.AddWithValue("@Updateidate", mycode.idate());
            cmd1.Parameters.AddWithValue("@entry_id", entryid);
            cmd1.Parameters.AddWithValue("@Objective_Entry_id", Objective_Entry_id);
            payments.InsertUpdateData(cmd1, con);

            //-----------------------------Option3----------------------------------
            query = @"INSERT INTO question_answer_Master (Section,test_code,quest_code,opt_code,option_text,created_date,created_idate,created_by,updated_date,Updateidate,opetion_text_HN,Option_type,Option_Itype,entry_id,Objective_Entry_id) values (@Section,@test_code,@quest_code,@opt_code,@option_text,@created_date,@created_idate,@created_by,@updated_date,@Updateidate,@opetion_text_HN,@Option_type,@Option_Itype,@entry_id,@Objective_Entry_id)";
            optionid = payments.auto_serialS("option_id", con);
            SqlCommand cmd2 = new SqlCommand(query);
            cmd2.Parameters.AddWithValue("@Section", testmodesection);
            cmd2.Parameters.AddWithValue("@test_code", Test_id);
            cmd2.Parameters.AddWithValue("@quest_code", question_id);
            cmd2.Parameters.AddWithValue("@opt_code", optionid);
            cmd2.Parameters.AddWithValue("@Option_type", "Textual");
            cmd2.Parameters.AddWithValue("@Option_Itype", "0");
            cmd2.Parameters.AddWithValue("@option_text", Option3);
            cmd2.Parameters.AddWithValue("@opetion_text_HN", "");
            cmd2.Parameters.AddWithValue("@created_date", mycode.date());
            cmd2.Parameters.AddWithValue("@created_idate", mycode.idate());
            cmd2.Parameters.AddWithValue("@created_by", hd_userid.Value);
            cmd2.Parameters.AddWithValue("@updated_by", hd_userid.Value);
            cmd2.Parameters.AddWithValue("@updated_date", mycode.date());
            cmd2.Parameters.AddWithValue("@Updateidate", mycode.idate());
            cmd2.Parameters.AddWithValue("@entry_id", entryid);
            cmd2.Parameters.AddWithValue("@Objective_Entry_id", Objective_Entry_id);
            payments.InsertUpdateData(cmd2, con);

            //-----------------------------Option4----------------------------------
            query = @"INSERT INTO question_answer_Master (Section,test_code,quest_code,opt_code,option_text,created_date,created_idate,created_by,updated_date,Updateidate,opetion_text_HN,Option_type,Option_Itype,entry_id,Objective_Entry_id) values (@Section,@test_code,@quest_code,@opt_code,@option_text,@created_date,@created_idate,@created_by,@updated_date,@Updateidate,@opetion_text_HN,@Option_type,@Option_Itype,@entry_id,@Objective_Entry_id)";
            optionid = payments.auto_serialS("option_id", con);
            SqlCommand cmd3 = new SqlCommand(query);
            cmd3.Parameters.AddWithValue("@Section", testmodesection);
            cmd3.Parameters.AddWithValue("@test_code", Test_id);
            cmd3.Parameters.AddWithValue("@quest_code", question_id);
            cmd3.Parameters.AddWithValue("@opt_code", optionid);
            cmd3.Parameters.AddWithValue("@Option_type", "Textual");
            cmd3.Parameters.AddWithValue("@Option_Itype", "0");
            cmd3.Parameters.AddWithValue("@option_text", Option4);
            cmd3.Parameters.AddWithValue("@opetion_text_HN", "");
            cmd3.Parameters.AddWithValue("@created_date", mycode.date());
            cmd3.Parameters.AddWithValue("@created_idate", mycode.idate());
            cmd3.Parameters.AddWithValue("@created_by", hd_userid.Value);
            cmd3.Parameters.AddWithValue("@updated_by", hd_userid.Value);
            cmd3.Parameters.AddWithValue("@updated_date", mycode.date());
            cmd3.Parameters.AddWithValue("@Updateidate", mycode.idate());
            cmd3.Parameters.AddWithValue("@entry_id", entryid);
            cmd3.Parameters.AddWithValue("@Objective_Entry_id", Objective_Entry_id);
            payments.InsertUpdateData(cmd3, con);

            add_option_id_in_question_info(question_id, AnswerOption, Test_id, con);
        }
        private void add_option_id_in_question_info(string question_id, string option, string Test_id, SqlConnection con)
        {
            string query1 = "Select * from question_answer_Master where quest_code='" + question_id + "' and test_code='" + Test_id + "' order by id ASC";
            SqlDataAdapter ad1 = new SqlDataAdapter(query1, con);
            DataSet ds1 = new DataSet();
            ad1.Fill(ds1);
            DataTable dt1 = ds1.Tables[0];
            if (dt1.Rows.Count == 0)
            {
            }
            else
            {
                int index = 0;
                int index2 = 1;
                //var result = String.Compare("Option1", "option1", StringComparison.OrdinalIgnoreCase);
                //if (option.Trim() == "Option1")
                if (option.Equals("Option1", StringComparison.OrdinalIgnoreCase))
                {
                    index = 1;
                }
                if (option.Equals("Option2", StringComparison.OrdinalIgnoreCase))
                {
                    index = 2;
                }
                if (option.Equals("Option3", StringComparison.OrdinalIgnoreCase))
                {
                    index = 3;
                }
                if (option.Equals("Option4", StringComparison.OrdinalIgnoreCase))
                {
                    index = 4;
                }
                if (option.Equals("Option5", StringComparison.OrdinalIgnoreCase))
                {
                    index = 5;
                }

                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    string opt_code = dt1.Rows[i]["opt_code"].ToString();

                    if (index == index2)
                    {
                        update_question_info(opt_code, question_id, Test_id, con);
                        break;
                    }
                    index2++;
                }
            }
        }
        private void update_question_info(string opt_code, string question_id,string Test_id, SqlConnection con)
        {
            string query = @"update question_info set Opetion_id=@Opetion_id  where  questionid=" + question_id + " and test_id='"+ Test_id + "' ";
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@questionid", question_id);
            cmd.Parameters.AddWithValue("@Opetion_id", opt_code);
            payments.InsertUpdateData(cmd, con);
        }
        #endregion
    }
}