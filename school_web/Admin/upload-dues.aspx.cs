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
    public partial class upload_dues : System.Web.UI.Page
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
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();
                        ViewState["college_name"] = My.get_college_name();
                        string pagename_current = Path.GetFileName(Request.Path);
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];

                        mycode.bind_all_ddl_with_id(ddlsession, " select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                        ddlsession.SelectedValue = My.get_session_id();

                        mycode.bind_all_ddl_with_id(ddl_class, "Select Course_Name,course_id from Add_course_table order by Position asc");
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Upload_dues");
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
                    if (ddlsession.SelectedItem.Text == "Select")
                    {
                        Alertme("Please select session", "warning");
                        ddlsession.Focus();
                        return;
                    }
                    if (ddl_class.SelectedItem.Text == "Select")
                    {
                        Alertme("Please select class", "warning");
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
                else if (ViewState["Is_Edit"].ToString() == "1")
                {
                    if (ddlsession.SelectedItem.Text == "Select")
                    {
                        Alertme("Please select session", "warning");
                        ddlsession.Focus();
                        return;
                    }
                    if (ddl_class.SelectedItem.Text == "Select")
                    {
                        Alertme("Please select class", "warning");
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
                tblReadCSV.Columns.Add("Admission No");
                tblReadCSV.Columns.Add("Dues");

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

                    Admission_no = grvExcelData.Rows[i].Cells[0].Text;
                    if (ViewState["college_name"].ToString() == "St. Mary school")
                    {
                        if (Admission_no.Length == 4)
                        {
                            Admission_no = "0" + Admission_no;
                        }
                        if (Admission_no.Length == 3)
                        {
                            Admission_no = "00" + Admission_no;
                        }

                    }
                    else if (ViewState["college_name"].ToString() == "ST Mary Itahar")
                    {
                        if (Admission_no.Length == 4)
                        {
                            Admission_no = "0" + Admission_no;
                        }
                        if (Admission_no.Length == 3)
                        {
                            Admission_no = "00" + Admission_no;
                        }
                    }



                    #region check duplicate
                    string adno = Admission_no;
                    DataTable dt = My.dataTable(" select admissionserialnumber,studentname from admission_registor where admissionserialnumber='" + adno + "' and Session_id='" + ddlsession.SelectedValue + "'");
                    if (dt.Rows.Count == 0)
                    {
                        grvExcelData.Rows[i].BackColor = System.Drawing.Color.Red;
                        Alertme("Sorry! Admission no. not found to selected session or class.", "warning");
                        btn_final_submit.Visible = false;
                    }
                    else
                    {
                        ViewState["SName"] = dt.Rows[0]["studentname"].ToString();
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
            Session["file"] = ("Upload_student_dues" + filerename + FileExtension);
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
                Alertme("Choose only csv File", "warning");
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

            if (ViewState["Is_add"].ToString() == "1")
            {
                btn_final_submit_data();
            }
            else if (ViewState["Is_Edit"].ToString() == "1")
            {
                btn_final_submit_data();
            }
            else
            {
                Alertme(My.get_restricted_message(), "warning");
            }

            
        }

        private void btn_final_submit_data()
        {
            if (ddlsession.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
                ddlsession.Focus();
                return;
            }
            if (ddl_class.SelectedItem.Text == "Select")
            {
                Alertme("Please select class", "warning");
                ddlsession.Focus();
                return;
            }
            else
            {
                try
                {
                    string qry = "";
                    string SessionName = ddlsession.SelectedItem.Text;
                    string Session = ddlsession.SelectedValue;
                    string class_id = ddl_class.SelectedValue;
                    string class_name = ddl_class.SelectedItem.Text;
                    string AdmissionNo = "";
                    string DuesAmt = "";
                    DateTime date = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                    for (int i = 0; i < grvExcelData.Rows.Count; i++)
                    {
                        AdmissionNo = grvExcelData.Rows[i].Cells[0].Text;
                        DuesAmt = grvExcelData.Rows[i].Cells[1].Text;


                        if (ViewState["college_name"].ToString() == "St. Mary school")
                        {
                            if (AdmissionNo.Length == 4)
                            {
                                AdmissionNo = "0" + AdmissionNo;
                            }
                            if (AdmissionNo.Length == 3)
                            {
                                AdmissionNo = "00" + AdmissionNo;
                            }

                        }
                        else if (ViewState["college_name"].ToString() == "ST Mary Itahar")
                        {
                            if (AdmissionNo.Length == 4)
                            {
                                AdmissionNo = "0" + AdmissionNo;
                            }
                            if (AdmissionNo.Length == 3)
                            {
                                AdmissionNo = "00" + AdmissionNo;
                            }
                        }





                        string student_name = ViewState["SName"].ToString();
                        DataTable dtX = My.dataTable(" select studentname from admission_registor where admissionserialnumber='" + AdmissionNo + "' and Session_id='" + ddlsession.SelectedValue + "'");
                        if (dtX.Rows.Count != 0)
                        {
                            student_name = dtX.Rows[0]["studentname"].ToString();
                        }

                        SqlDataAdapter ad = new SqlDataAdapter("select * from Previous_Year_Dues where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddl_class.SelectedValue + "' and AdmissionNumber='" + AdmissionNo + "'", My.conn);
                        DataSet ds = new DataSet();
                        ad.Fill(ds, "Previous_Year_Dues");
                        DataTable dt = ds.Tables[0];
                        if (dt.Rows.Count == 0)
                        {
                            DataRow dr = dt.NewRow();

                            dr["Session"] = ddlsession.SelectedItem.Text;
                            dr["AdmissionNumber"] = AdmissionNo;
                            dr["Name"] = student_name;
                            dr["Dues_Amount"] = My.toDouble(DuesAmt).ToString("0.00");
                            dr["Status"] = "Unpaid";
                            dr["Session_id"] = ddlsession.SelectedValue;
                            dr["Class_id"] = ddl_class.SelectedValue;
                            dr["Old_session_id"] = ddlsession.SelectedValue;
                            dr["Old_class_id"] = ddl_class.SelectedValue;
                            dt.Rows.Add(dr);
                        }
                        else
                        {
                            string id = dt.Rows[0]["Id"].ToString();
                            foreach (DataRow dr in dt.Rows)
                            {
                                dr["Dues_Amount"] = My.toDouble(DuesAmt).ToString("0.00");
                                dr["Status"] = "Unpaid";
                            }
                        }
                        SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                        ad.Update(dt);
                    }
                    Alertme("Student dues has been uploaded successfully.", "success");
                    btn_final_submit.Visible = false;
                }
                catch (Exception ex)
                {
                    My.Save_Exception(ex.ToString());
                }
            }
        }

        protected void btn_reset_Click(object sender, EventArgs e)
        {
            Response.Redirect("upload-dues.aspx", false);
        }
    }
}