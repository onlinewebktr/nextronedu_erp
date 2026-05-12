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

namespace school_web.Examination_Admin
{
    public partial class bulk_mark_entry : System.Web.UI.Page
    {
        My mycode = new My();
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
                    ViewState["Branchid"] = mycode.getbranchid(Session["Admin"].ToString());
                    mycode.bind_all_ddl_with_id(ddlsession, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                    ddlsession.SelectedValue = My.get_session_id();

                    mycode.bind_all_ddl_with_id(ddl_CourseCat, "Select   Course_Name, course_id from Add_course_table order by Position asc");


                }
            }
        }

        protected void ddl_CourseCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddl_CourseCat.SelectedItem.Text == "Select")
                {
                    Alertme("Please select class.", "warning");
                }
                else
                {
                    mycode.bind_all_ddl_with_id(ddl_term, "select Term_Name,Exam_Term_Id from Exam_Term_Details where Class_id='" + ddl_CourseCat.SelectedValue + "' and Session_Id='" + ddlsession.SelectedValue + "' and Branch_Id='" + ViewState["Branchid"].ToString() + "' order by Sequence_No asc");
                }
            }
            catch (Exception ex)
            {
            }
        }
        protected void ddl_term_SelectedIndexChanged(object sender, EventArgs e)
        {
            mycode.bind_all_ddl_with_id(ddl_assesment, "select Assessment_Name,Assessment_Id from Exam_Assessment_Details where Class_id='" + ddl_CourseCat.SelectedValue + "' and Exam_Term_Id='" + ddl_term.SelectedValue + "' and Session_Id='" + ddlsession.SelectedValue + "' and Branch_Id='" + ViewState["Branchid"].ToString() + "' order by Assessment_Name asc");
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlsession.SelectedItem.Text == "")
                {
                    ddlsession.Focus();
                    Alertme("Please select session.", "warning");
                    return;
                }
                if (ddl_CourseCat.SelectedItem.Text == "")
                {
                    ddl_CourseCat.Focus();
                    Alertme("Please select class.", "warning");
                    return;
                }
                if (ddl_term.SelectedItem.Text == "")
                {
                    ddl_term.Focus();
                    Alertme("Please select term.", "warning");
                    return;
                }
                if (ddl_assesment.SelectedItem.Text == "")
                {
                    ddl_assesment.Focus();
                    Alertme("Please select assesment.", "warning");
                    return;
                }

                // btn_final_submit.Visible = true; 
                //upload_excel_file();



                if (FileUpload1.HasFile)
                {
                    try
                    {
                        // Read the CSV file
                        DataTable dt = ReadCsvFile(FileUpload1.PostedFile.InputStream);

                        // Bind the DataTable to the GridView
                        grvExcelData.DataSource = dt;
                        grvExcelData.DataBind();
                        pnl_grid.Visible = true;
                    }
                    catch (Exception ex)
                    {
                        // Handle the exception (e.g., log it, show a message, etc.)
                        Response.Write("Error: " + ex.Message);
                        Alertme("Error: " + ex.Message, "warning");
                    }
                }
                else
                {
                    Response.Write("Please upload a CSV file.");
                }
            }
            catch (Exception ex)
            {
            }
        }

        private DataTable ReadCsvFile(Stream inputStream)
        {
            DataTable dt = new DataTable();
            using (StreamReader reader = new StreamReader(inputStream))
            {
                // Read the header line
                string headerLine = reader.ReadLine();
                if (headerLine != null)
                {
                    // Split the header line into columns
                    string[] headers = headerLine.Split(',');
                    foreach (string header in headers)
                    {
                        dt.Columns.Add(header.Trim());
                    }

                    // Read the rest of the file
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        if (line != null)
                        {
                            string[] rows = line.Split(',');
                            DataRow dr = dt.NewRow();
                            for (int i = 0; i < headers.Length; i++)
                            {
                                dr[i] = rows.Length > i ? rows[i].Trim() : string.Empty;
                            }
                            dt.Rows.Add(dr);
                        }
                    }
                }
            }
            ViewState["subjectData"] = dt;
            return dt;
        }



        //=======================================
        protected void btn_final_submit_Click1(object sender, EventArgs e)
        {
            try
            {
                string entry_id = My.create_random_no_otp();
                DataTable dt = ViewState["subjectData"] as DataTable;
                foreach (DataRow dr in dt.Rows)
                {
                    int i = 0;
                    string admission_no = "";
                    string Section = "";
                    string isStdFind = "0";
                    foreach (DataColumn dc in dt.Columns)
                    {
                        var colname = dc.ColumnName;
                        var colvalue = dr[colname];
                        if (i == 0)
                        {
                            admission_no = colvalue.ToString();
                            DataTable dtstd = My.dataTable("select Session_id,Class_id,admissionserialnumber as Adm_no,Section,studentname as Student_name from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddl_CourseCat.SelectedValue + "' and admissionserialnumber='" + admission_no + "'");
                            if (dtstd.Rows.Count > 0)
                            {
                                Section = dtstd.Rows[0]["Section"].ToString();
                                SqlCommand cmd;
                                string query = "INSERT INTO Exam_bulk_mark_entry (Session_id,Class_id,Admission_no,Section,Name,Status,Entry_id,Created_by,Created_date,Created_time) values (@Session_id,@Class_id,@Admission_no,@Section,@Name,@Status,@Entry_id,@Created_by,@Created_date,@Created_time)";
                                cmd = new SqlCommand(query);
                                cmd.Parameters.AddWithValue("@Session_id", ddlsession.SelectedValue);
                                cmd.Parameters.AddWithValue("@Class_id", ddl_CourseCat.SelectedValue);
                                cmd.Parameters.AddWithValue("@Admission_no", admission_no);
                                cmd.Parameters.AddWithValue("@Section", Section);
                                cmd.Parameters.AddWithValue("@Name", dtstd.Rows[0]["Student_name"].ToString());
                                cmd.Parameters.AddWithValue("@Status", "Success");
                                cmd.Parameters.AddWithValue("@Entry_id", entry_id);
                                cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                                cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                                cmd.Parameters.AddWithValue("@Created_time", mycode.idate());
                                if (My.InsertUpdateData(cmd))
                                {
                                }
                                isStdFind = "1";
                            }
                            else
                            {
                                SqlCommand cmd;
                                string query = "INSERT INTO Exam_bulk_mark_entry (Session_id,Class_id,Admission_no,Section,Name,Status,Entry_id,Created_by,Created_date,Created_time) values (@Session_id,@Class_id,@Admission_no,@Section,@Name,@Status,@Entry_id,@Created_by,@Created_date,@Created_time)";
                                cmd = new SqlCommand(query);
                                cmd.Parameters.AddWithValue("@Session_id", ddlsession.SelectedValue);
                                cmd.Parameters.AddWithValue("@Class_id", ddl_CourseCat.SelectedValue);
                                cmd.Parameters.AddWithValue("@Admission_no", admission_no);
                                cmd.Parameters.AddWithValue("@Section", "NA");
                                cmd.Parameters.AddWithValue("@Name", "Not Found");
                                cmd.Parameters.AddWithValue("@Status", "Fail");
                                cmd.Parameters.AddWithValue("@Entry_id", entry_id);
                                cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                                cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                                cmd.Parameters.AddWithValue("@Created_time", mycode.idate());
                                if (My.InsertUpdateData(cmd))
                                {
                                }
                            }
                        }

                        if (isStdFind == "1")
                        {
                            if (i > 1)
                            {
                                string subj_name = colname.ToString();
                                string marks = colvalue.ToString();
                                save_subject_marks(admission_no, Section, subj_name, marks);
                            }
                        }
                        i++;
                    } 
                }



                DataTable dtSuc = My.dataTable("select Admission_no,Section,Name,Status from Exam_bulk_mark_entry where Entry_id='" + entry_id + "'");
                if (dtSuc.Rows.Count > 0)
                {
                    grvExcelData.DataSource = dtSuc;
                    grvExcelData.DataBind();
                    btn_final_submit.Visible = false;
                    Alertme("Masrsk has been updated successfully.", "success");
                }
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }

        private void save_subject_marks(string admission_no, string section, string subj_name, string marks)
        {
            DataTable dtSubj = My.dataTable("select * from Subject_Master where course_id='" + ddl_CourseCat.SelectedValue + "' and Subject_name='" + subj_name + "'");
            if (dtSubj.Rows.Count > 0)
            {
                string subjActivityId = My.get_single_column_data("select Subject_Sub_Level_Id as Column_Name from Exam_Subject_Sub_Level where Session_Id='" + ddlsession.SelectedValue + "' and Branch_Id='" + ViewState["Branchid"].ToString() + "' and Exam_Term_Id='" + ddl_term.SelectedValue + "' and Assessment_Id='" + ddl_assesment.SelectedValue + "' and Class_id='" + ddl_CourseCat.SelectedValue + "' and Subject_id='" + dtSubj.Rows[0]["Subject_id"].ToString() + "'");
                string Is_character = "0";
                string valueinput = "";
                if (marks != "")
                {
                    valueinput = marks;
                    if (My.cheknum_fine(valueinput))
                    {
                        Is_character = "0";
                    }
                    else
                    {
                        Is_character = "1";
                    }
                }
                else
                {
                    valueinput = "";
                    Is_character = "0";
                }

                DataTable dt = mycode.FillData("select Id from Exam_marks where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddl_CourseCat.SelectedValue + "' and Section='" + section + "' and Term='" + ddl_term.SelectedValue + "' and Assessment='" + ddl_assesment.SelectedValue + "' and Subject='" + dtSubj.Rows[0]["Subject_id"].ToString() + "' and Subject_activity='" + subjActivityId + "' and Admission_no='" + admission_no + "' and Branch_id='" + ViewState["Branchid"].ToString() + "'");
                if (dt.Rows.Count == 0)
                {
                    if (valueinput == "")
                    {
                    }
                    else
                    {
                        SqlCommand cmd;
                        string query = "INSERT INTO Exam_marks (Session_id,Class_id,Section,Term,Assessment,Subject,Subject_activity,Admission_no,Marks,Branch_id,Created_by,Created_date,Created_idate,Mark_id,Is_character) values (@Session_id,@Class_id,@Section,@Term,@Assessment,@Subject,@Subject_activity,@Admission_no,@Marks,@Branch_id,@Created_by,@Created_date,@Created_idate,@Mark_id,@Is_character)";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@Session_id", ddlsession.SelectedValue);
                        cmd.Parameters.AddWithValue("@Class_id", ddl_CourseCat.SelectedValue);
                        cmd.Parameters.AddWithValue("@Section", section);
                        cmd.Parameters.AddWithValue("@Term", ddl_term.SelectedValue);
                        cmd.Parameters.AddWithValue("@Assessment", ddl_assesment.SelectedValue);
                        cmd.Parameters.AddWithValue("@Subject", dtSubj.Rows[0]["Subject_id"].ToString());
                        cmd.Parameters.AddWithValue("@Subject_activity", subjActivityId);
                        cmd.Parameters.AddWithValue("@Admission_no", admission_no);
                        cmd.Parameters.AddWithValue("@Marks", valueinput);
                        cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branchid"].ToString());
                        cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                        cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                        cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
                        cmd.Parameters.AddWithValue("@Mark_id", "0");
                        cmd.Parameters.AddWithValue("@Is_character", Is_character);
                        if (My.InsertUpdateData(cmd))
                        {
                        }
                    }
                }
                else
                {
                    string id = dt.Rows[0]["Id"].ToString();
                    if (marks != "")
                    {
                        valueinput = marks;
                        if (My.cheknum_fine(valueinput))
                        {
                            Is_character = "0";
                        }
                        else
                        {
                            Is_character = "1";
                        }
                    }
                    else
                    {
                        valueinput = "";
                        Is_character = "0";
                    }
                    SqlCommand cmd;
                    string query = "Update Exam_marks set Marks=@Marks,Mark_id=@Mark_id,Is_character=@Is_character,Updated_by=@Updated_by,Updated_date=@Updated_date,Updated_idate=@Updated_idate where Id=" + id + " ";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Marks", valueinput);
                    cmd.Parameters.AddWithValue("@Mark_id", "0");
                    cmd.Parameters.AddWithValue("@Is_character", Is_character);
                    cmd.Parameters.AddWithValue("@Updated_by", ViewState["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@Updated_date", mycode.date());
                    cmd.Parameters.AddWithValue("@Updated_idate", mycode.idate());
                    if (My.InsertUpdateData(cmd))
                    {
                    }
                }
            }
        }
    }
}