using school_web.AppCode;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.LMS_VC_Admin
{
    public partial class Upload_excel_Class_Subject_Mapping : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                }
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }


        protected void btn_submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileUpload1.HasFile)
                    upload_excel_file();

            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        private void upload_excel_file()
        {
            string file = upload_csv_data();
            string csvid = code.auto_serial("Upload_csvid");

            SqlDataAdapter ad = new SqlDataAdapter("Select * from Student_Csv_file", connection.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Student_Csv_file");
            DataTable dt = ds.Tables[0];
            DataRow dr = dt.NewRow();
            dr[1] = file;
            dr[2] = code.date();
            dr[3] = code.idate();
            dr[4] = csvid;
            dr[5] = "SUBMITTED";
            dt.Rows.Add(dr);
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
            // uploaded_data(csvid);

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
            Session["file"] = ("Upload_excel_Class_Subject_Mapping" + filerename + FileExtension);
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
                    string path = (Server.MapPath("../UploadedImage")).ToString();
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
                    Alert(ex.ToString());

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
                dbfilePath = @"/UploadedImage/" + fileName;
                Session["fileName"] = fileName;
            }
            return dbfilePath;
        }

        private bool check_wrap_or_not(string path)
        {
            try
            {

                DataTable tblReadCSV = new DataTable();
                tblReadCSV.Columns.Add("Teacher_name");
                tblReadCSV.Columns.Add("Teacher_Userid");
                tblReadCSV.Columns.Add("Class");
                tblReadCSV.Columns.Add("Subject");

                TextFieldParser csvParser = new TextFieldParser(path);
                csvParser.Delimiters = new string[] { "," };
                csvParser.TrimWhiteSpace = true;
                csvParser.ReadLine();
                while (!(csvParser.EndOfData == true))
                {
                    tblReadCSV.Rows.Add(csvParser.ReadFields());
                }

                pnl_grd.Visible = true;
                lbl_total_student.Text = "Total Teacher :- " + tblReadCSV.Rows.Count.ToString();
                grvExcelData.DataSource = tblReadCSV;
                grvExcelData.DataBind();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public void Alert(string Message)
        {
            lbl_msg.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }



        protected void btn_final_butmit_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "";
                string Teacher_name = "";
                string Teacher_Userid = "";
                string Class = "";
                string Subject = "";
                string Class_id = "";
                string Subject_id = "";


                DateTime date = DateTime.UtcNow.AddHours(5).AddMinutes(30);

                for (int i = 0; i < grvExcelData.Rows.Count; i++)
                {

                    Teacher_name = grvExcelData.Rows[i].Cells[0].Text;
                    Teacher_Userid = grvExcelData.Rows[i].Cells[1].Text;
                    Class = grvExcelData.Rows[i].Cells[2].Text;
                    Subject = grvExcelData.Rows[i].Cells[3].Text;


                    if (Teacher_name != "&nbsp;")
                    {
                        if (Class != "&nbsp;")
                        {
                            if (Subject != "&nbsp;")
                            {

                                if (check_teacher(Teacher_Userid))
                                {
                                    Class_id = find_class(Class);
                                    Subject_id = find_subject(Class_id, Subject);
                                    qry = qry + @" IF NOT EXISTS ( select * from TeacherCourseSubjectMaping where UserID='" + Teacher_Userid + @"' and CategoryID='" + Class_id + @"'and AssignCourseID='" + Subject_id + @"' ) Begin insert into TeacherCourseSubjectMaping(UserID,AssignCourseID,Istatus,Date,Idate,CategoryID) 
                                                     Values('" + Teacher_Userid + "','" + Subject_id + "','1', '" + date.ToString("dd/MM/yyyy") + "','" + date.ToString("yyyyMMdd") + "','" + Class_id + "'); END; ";
                                }
                                else
                                {
                                    grvExcelData.Rows[i].BackColor = System.Drawing.Color.Red;
                                    lbl_smg.Text = "Teacher -" + Teacher_name + " not found in our record.";
                                }
                            }
                            else
                                grvExcelData.Rows[i].BackColor = System.Drawing.Color.Red;
                        }
                        else
                            grvExcelData.Rows[i].BackColor = System.Drawing.Color.Red;
                    }
                    else
                        grvExcelData.Rows[i].BackColor = System.Drawing.Color.Red;
                }
                UsesCode.exeSql(qry);

                Alert("File uploaded successfully.");

                //pnl_grd.Visible = false;
                //grvExcelData.DataSource = null;
                //grvExcelData.DataBind();
            }
            catch (Exception ex)
            {
            }
        }
        private string find_class(string Class)
        {
            string tureturn = "";
            DataTable dt = code.FillTable("Select * from ClassMaster where CategoryName='" + Class + "'");
            if (dt.Rows.Count > 0)
                tureturn = dt.Rows[0]["CategoryID"].ToString();
            else
            {
                tureturn = code.Auto_generate_user_id("Select CategoryID from ClassMaster where CategoryID=", 1000, 9999);
                string sql = "insert into ClassMaster (CategoryID, CategoryName,Istatus,Date,Idate) " +
                              "Values ('" + tureturn + "'," + "'" + Class + "','1','" + code.date() + "','" + code.idate() + "');";
                SqlCommand cmd = new SqlCommand(sql);
                InsertUpdate.InsertUpdateData(cmd);
            }
            return tureturn;
        }
        private string find_subject(string Class_id, string Subject)
        {
            string tureturn = "";
            DataTable dt = code.FillTable("Select * from Course_or_Subject_Master where CategoryID ='" + Class_id + "' and CourseName='" + Subject + "'");
            if (dt.Rows.Count > 0)
                tureturn = dt.Rows[0]["CourseID"].ToString();
            else
            {
                tureturn = code.Auto_generate_user_id("Select CourseID from Course_or_Subject_Master where CourseID=", 1000, 9999);
                string sql = "insert into Course_or_Subject_Master (CategoryID,CourseID, CourseName,Istatus,Date,Idate) " +
                              "Values ('" + Class_id + "','" + tureturn + "'," + "'" + Subject + "','1','" + code.date() + "','" + code.idate() + "');";
                SqlCommand cmd = new SqlCommand(sql);
                InsertUpdate.InsertUpdateData(cmd);
            }
            return tureturn;
        }

        private bool check_teacher(string Teacher_Userid)
        {
            DataTable dt = code.FillTable("Select * from InstructorProfile where UserID='" + Teacher_Userid + "'");
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }



    }
}