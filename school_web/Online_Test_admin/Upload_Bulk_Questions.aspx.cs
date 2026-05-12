using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using school_web.AppCode;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;
using System.Xml;
using System.IO;
using System.Text;
using System.Xml.Xsl;
using System.Text.RegularExpressions;
using System.Transactions;
 

namespace school_web.Online_Test_admin
{
    public partial class Upload_Bulk_Questions : System.Web.UI.Page
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
                    // lbl_Section.Text = Request.QueryString["Section"];
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

        private void Bind_pageload()
        {
            try
            {
                var condition = "where 1=1 ";
                condition += $" and  Session_id='{hd_sessionid.Value}' ";
                condition += $" and Class_id='{hd_class_id.Value}' ";
                condition += $" and Entry_id='{ViewState["entryid"].ToString()}' ";
                condition += $" and Section='{ddl_section.Text}' ";
                //  DataTable dt = My.MydataTable($@"select oen.*,(select top 1 Session from session_details where session_id=oen.Session_id) as session,(Select count(*) from question_info where test_id=oen.Exam_id) as toquestion,format(oen.live_date,'dd/MM/yyyy') as live_date_one,format(oen.live_date,'hh:mm tt') as live_time_one,ad.Course_Name,(select top 1 Subject_name from Subject_Master where Subject_id=oen.subjectname) as subject_name from  OlineTest_Exam_name oen join  Add_course_table ad  on oen.Class_id=ad.course_id  {condition} order by ad.Position,oen.Exam_name ");
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
            string testid = hd_test_id.Value;
            string sectionid = hd_test_id.Value;
            string examtype = hd_exam_type.Value;
            string testid1 = My.get_test_id_from_entry_id(ViewState["entryid"].ToString());
            string path = "Question_Details.aspx?testid=" + testid1 + "&sectionid=" + sectionid + "&type=View";
            viequestion.HRef = path;
            string query1 = "select test_id,Objective_Entry_id from question_info  where test_id='" + testid1 + "'  and Uploding_status='Uploaded'  ";//and Language_Itype=0
            SqlCommand cmd = new SqlCommand(query1);//and Section='" + hd_section_id.Value + "'
            DataTable dt = mycode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                viequestion.Visible = false;
                btn_delete_question.Visible = false;
                btn_final_submit.Visible = false;
                btn_s_add.Visible = true;
            }
            else
            {

                
                viequestion.Visible = true;
                btn_final_submit.Visible = true;
                btn_delete_question.Visible = true;
                btn_s_add.Visible = false;
               
            }
        }

        #region quetion uploading via word
        bool finalsend = false;
        protected void btn_s_add_Click(object sender, EventArgs e)
        {

            lblmsg.Text = "";
            viequestion.Visible = false;
            btn_final_submit.Visible = false;
            btn_delete_question.Visible = false;
            lblmsg.Text = "";
            try
            {
                //using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
                //{
                hd_exam_type.Value = "4";//PT

                // find_data();
                add_data();

                //scope.Complete();
                //}
                if (finalsend == true)
                {
                    btn_s_add.Visible = false;
                    lblmsg.Text = "Question has been uploaded successfully";
                    Alertme("Question has been uploaded successfully", "success");
                    string testid = hd_test_id.Value;
                    string sectionid = hd_test_id.Value;
                    string examtype = hd_exam_type.Value;
                    viequestion.Visible = true;
                    btn_final_submit.Visible = true;
                    btn_delete_question.Visible = true;
                    string testid1 = My.get_test_id_from_entry_id(ViewState["entryid"].ToString());
                    string path = "Question_Details.aspx?testid=" + testid1 + "&sectionid=" + sectionid + "&type=View";
                    viequestion.HRef = path;
                }




            }
            catch (Exception ex)
            {
                finalsend = false;
                My.submitException(ex, "Upload Bulk Question");
                string query = "Delete from  Save_question_url  where  Test_id='" + hd_test_id.Value + "' and User_id='" + hd_userid.Value + "' ";
                SqlCommand cmd = new SqlCommand(query);
                My.InsertUpdateData(cmd);
            }

        }

        private void add_data()
        {
            bool docsfile = false;
            string path = "";
            string savepath = "";
            if (FileUpload1.HasFile)
            {
                path = Upload_Image();//Server.MapPath(FileUpload1.FileName); //Server.MapPath("Document_file/m2013.docx");//
                savepath = path;
                path = Server.MapPath(path);

            }
            string path2 = Server.MapPath("../Document_file/OMML2MML.xsl");
            if (path != "")
            {

                WriteToWordDoc(path, "gggggggggggggggggggggggggggggggggg", path2, savepath);
            }
            else
            {
                lblmsg.Text = "Please Select file";
                Alertme("Please Select file", "warning");

            }



        }
        private string Upload_Image()
        {
            DateTime dt = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string date = dt.ToString("dd_MM_yyyy");
            string time = dt.ToString("hh_mm_ss");
            String filerename = date + time;             // rename file
            string dbfilePath = "";
            Boolean FileOK = false;
            Boolean FileSaved = false;
            int k = 0;
            if (FileUpload1.HasFile)
            {

                Session["WorkingImage"] = FileUpload1.FileName;
                String FileExtension = System.IO.Path.GetExtension(Session["WorkingImage"].ToString()).ToLower();
                Session["renamedfile"] = filerename + "PIMG1" + FileExtension;
                String[] allowedExtensions = { ".docx" };
                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    k++;
                    if (FileExtension == allowedExtensions[i])
                    {
                        FileOK = true;

                        break;
                    }
                }

            }
            else
            {
                Alertme("Please select image first", "warning");

            }
            if (FileOK)
            {
                try
                {
                    string path = (Server.MapPath("../question_doc")).ToString();
                    FileUpload1.SaveAs(path + "/" + Session["renamedfile"]);
                    FileSaved = true;
                }
                catch (Exception ex)
                {
                    FileSaved = false;
                    Alertme("File not save, please five the permission", "warning");

                }
            }
            else
            {
            }
            if (FileSaved)
            {

                string fileName = System.IO.Path.GetFileName(Session["renamedfile"].ToString());
                dbfilePath = "/question_doc/" + fileName;
            }
            return dbfilePath;
        }
        #endregion

        #region main Queation upload
        private void WriteToWordDoc(string filepath, string txt, string path2, string savepath)
        {
            string query = "";
            if (ddl_section.Text == "ALL")
            {
                query = "Select * from OLINETEST_EXAM_NAME where Entry_id='" + ViewState["entryid"].ToString() + "'";
            }
            else
            {
                query = "Select * from OLINETEST_EXAM_NAME where Entry_id='" + ViewState["entryid"].ToString() + "' and Section='" + ddl_section.Text + "'";
            }


            DataTable cdt = mycode.FillData(query);
            if (cdt.Rows.Count == 0)
            {

            }
            else
            {
                for (int j = 0; j < cdt.Rows.Count; j++)
                {
                    string Testid = cdt.Rows[j]["Exam_id"].ToString();
                    string Exam_id = cdt.Rows[j]["Exam_id"].ToString();
                    string section = cdt.Rows[j]["Section"].ToString();
                    string language_type = "";
                    string language_id = "";
                    string group_entryid = "";
                    string section1 = section;
                    string question_id = "";
                    string entryid = "";
                    group_entryid = My.auto_serialS("Online_Objective_Entry_id");
                    string Test_id = Testid;
                    string section_testmode = Testid;
                    string sub_id = "01";
                    question_id = My.auto_serialS("Online_questionid");
                    entryid = My.auto_serialS("Online_entry_id");
                    string lng = "0";
                    if (lng == "0")
                    {
                        language_type = "Single";
                        language_id = "0";
                    }
                    else
                    {
                        language_type = "Double";
                        language_id = "1";
                    }
                    save_final_question(question_id, entryid, group_entryid, Test_id, language_type, language_id, sub_id, section, filepath, txt, path2, savepath, section_testmode);
                    insert_data_Question_Upload_History(Test_id, sub_id, section);


                   
                }
                find_data();
            }
        }

        private void insert_data_Question_Upload_History(string test_id, string sub_id, string section)
        {
            SqlCommand cmd = new SqlCommand("Select * from Question_Upload_History where Sub_id=" + sub_id + " and Test_id=" + test_id + "");
            DataTable dt = mycode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {

                SqlCommand cmd1;
                string strQuery = @"INSERT INTO Question_Upload_History (Exam_Id,Class_Id,Sub_id,Session_Id,section,Create_Date,Create_idate,Create_time,Status,Created_By,Test_id) values (@Exam_Id,@Class_Id,@Sub_id,@Session_Id,@section,@Create_Date,@Create_idate,@Create_time,@Status,@Created_By,@Test_id)";
                cmd1 = new SqlCommand(strQuery);
                cmd1.Parameters.AddWithValue("@Exam_Id", test_id);
                cmd1.Parameters.AddWithValue("@Class_Id", hd_class_id.Value);
                cmd1.Parameters.AddWithValue("@Sub_id", sub_id);
                cmd1.Parameters.AddWithValue("@Session_Id", hd_sessionid.Value);
                cmd1.Parameters.AddWithValue("@section", section);
                cmd1.Parameters.AddWithValue("@Create_Date", mycode.date());
                cmd1.Parameters.AddWithValue("@Create_idate", mycode.idate());
                cmd1.Parameters.AddWithValue("@Create_time", mycode.time());
                cmd1.Parameters.AddWithValue("@Status", "Draft");
                cmd1.Parameters.AddWithValue("@Created_By", hd_userid.Value);
                cmd1.Parameters.AddWithValue("@Test_id", test_id);
                if (My.InsertUpdateData(cmd1))
                {

                }
            }
            else
            {

                SqlCommand cmd1;
                string strQuery = @"Update Question_Upload_History set Create_Date=@Create_Date,Create_idate=@Create_idate,Create_time=@Create_time where Exam_Id=@Exam_Id and Class_Id=@Class_Id and Sub_id=@Sub_id and Test_id=@Test_id ";
                cmd1 = new SqlCommand(strQuery);
                cmd1.Parameters.AddWithValue("@Exam_Id", test_id);
                cmd1.Parameters.AddWithValue("@Class_Id", hd_class_id.Value);
                cmd1.Parameters.AddWithValue("@section", section);
                cmd1.Parameters.AddWithValue("@Sub_id", sub_id);
                cmd1.Parameters.AddWithValue("@Create_Date", mycode.date());
                cmd1.Parameters.AddWithValue("@Create_idate", mycode.idate());
                cmd1.Parameters.AddWithValue("@Create_time", mycode.time());
                cmd1.Parameters.AddWithValue("@Status", "Draft");
                cmd1.Parameters.AddWithValue("@Created_By", hd_userid.Value);
                cmd1.Parameters.AddWithValue("@Test_id", test_id);
                if (My.InsertUpdateData(cmd1))
                {

                }

            }
        }

        private void find_data()
        {
            string testid1 = My.get_test_id_from_entry_id(ViewState["entryid"].ToString());
            string query1 = "select test_id,Objective_Entry_id from question_info  where test_id='" + testid1 + "'   and Uploding_status='Uploaded'  ";//and Language_Itype=0
            DataTable dt = mycode.FillData(query1);
            if (dt.Rows.Count == 0)
            {
                viequestion.Visible = false;
                btn_delete_question.Visible = false;
            }
            else
            {
                viequestion.Visible = true;
                btn_delete_question.Visible = true;
                hd_Objective_Entry_id.Value = dt.Rows[0]["Objective_Entry_id"].ToString();
           
                string path = "Question_Details.aspx?testid=" + testid1 + "&type=View";
                viequestion.HRef = path;

            }


        }
        private void save_final_question(string questionid, string entryid, string group_entryid, string testid, string language_type, string language_id, string sub_id, string class_section, string filepath, string txt, string path2, string savepath, string section_testmode)
        {

            hd_Objective_Entry_id.Value = group_entryid;
            string examtypecode = "4";
            hd_marks.Value = mycode.get_marks(testid);
            // Open a WordprocessingDocument for editing using the file path.
            using (WordprocessingDocument wordprocessingDocument = WordprocessingDocument.Open(filepath, true))
            {
                // Assign a reference to the existing document body.
                Body body = wordprocessingDocument.MainDocumentPart.Document.Body;

                int i = 0;
                string text = "";
                string declaration_id = "0";
                string question_id = questionid;
                string phrases_id = "0";
                string pre_prefix = "";
                bool section = true;
                string msg = "";
                string en_opt_id = "";
                string hn_opt_id = "";
                int k = 0;
                foreach (DocumentFormat.OpenXml.Wordprocessing.Paragraph pp in body.Descendants<DocumentFormat.OpenXml.Wordprocessing.Paragraph>())
                {
                    string value = "";
                    string value2 = "";

                    if (pp != null)
                    {
                        foreach (OpenXmlElement el in pp.Elements())
                        {
                            if (el.LocalName.Equals("r", StringComparison.OrdinalIgnoreCase))
                            {
                                foreach (OpenXmlElement eli in el.Elements())
                                {
                                    if (eli.LocalName.Equals("t", StringComparison.OrdinalIgnoreCase))
                                    {
                                        text = text + el.InnerText;
                                        value = value + el.InnerText;
                                    }
                                    if (eli.LocalName.Equals("drawing", StringComparison.OrdinalIgnoreCase))
                                    {
                                        string s = eli.InnerXml.Replace(':', '_');
                                        string id = "", name = "";
                                        using (XmlReader reader = XmlReader.Create(new StringReader(s)))
                                        {
                                            reader.ReadToFollowing("pic_cNvPr");
                                            name = reader.GetAttribute("name");

                                            reader.ReadToFollowing("a_blip");
                                            id = reader.GetAttribute("r_embed");
                                            reader.MoveToContent();
                                        }

                                        if (!String.IsNullOrEmpty(name) && !String.IsNullOrEmpty(id))
                                        {
                                            Random r = new Random();
                                            int rand = r.Next(100000, 999999);
                                            name = name.Replace(' ', '_');
                                            name = name.Replace('.', '_');
                                            name = name + rand + ".jpg";

                                            var outputFilename = Server.MapPath("~/image/" + name);
                                            // Get image from document
                                            var imageData = wordprocessingDocument.MainDocumentPart.GetPartById(id);

                                            // Read image data into bytestream
                                            var stream = imageData.GetStream();
                                            var byteStream = new byte[stream.Length];
                                            int length = (int)stream.Length;
                                            stream.Read(byteStream, 0, length);
                                            // Write bytestream to disk
                                            using (var fileStream = new FileStream(outputFilename, FileMode.OpenOrCreate))
                                            {
                                                fileStream.Write(byteStream, 0, length);
                                            }
                                            String originalPath = My.url(); //HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, "");
                                            string path = originalPath + "/image/" + name;
                                            text = text + "<img src='" + path + "'></img>";

                                            value = value + "<img src='" + path + "'></img>";
                                        }
                                    }
                                }
                            }
                            else if (el.LocalName.Equals("oMath", StringComparison.OrdinalIgnoreCase))
                            {
                                string mathml = el.InnerXml;
                                mathml = "<intspl>" + mathml + "</intspl>";
                                string ommltext = OMML(mathml, path2);
                                text = text + ommltext;

                                value = value + ommltext;
                            }
                            i++;
                        }
                        if (!string.IsNullOrEmpty(value))
                        {
                            if (value.ToUpper().Contains("#ndd#".ToUpper()))
                            {
                                declaration_id = "0";
                            }
                            if (value.ToUpper().Contains("#npp#".ToUpper()))
                            {
                                phrases_id = "0";
                            }

                            if (value.ToUpper().Contains("#ms#".ToUpper()))
                            {
                                //for heading
                                string val = after(value, "#ms#");
                                if (val.Trim() == lbl_exmaname.Text)
                                {
                                    finalsend = true;
                                    Send_data_input(savepath);
                                }
                                else
                                {
                                    finalsend = false;
                                    section = false;
                                    lblmsg.Text = " Please note that the exam name does not match the name in the uploaded document heading";
                                    Alertme("Please note that the exam name does not match the name in the uploaded document heading", "warning");
                                    break;
                                }

                            }
                            if (section == true)
                            {
                                #region declaration
                                if (value.ToUpper().Contains("#dd#".ToUpper()) || value.Contains("#hdd#".ToUpper())) //for declaration
                                {
                                    question_id = "0";
                                    if (value.ToUpper().Contains("#dd#".ToUpper()))
                                    {

                                        string val = after(value, "#dd#");
                                        declaration_id = mycode.auto_serial("declaration");

                                        string query = @"INSERT INTO Declaration (entry_id,declaration,declaration_id) 
                                                         values (@entry_id,@declaration,@declaration_id); ";
                                        SqlCommand cmd = new SqlCommand(query);
                                        cmd.Parameters.AddWithValue("@entry_id", entryid);
                                        cmd.Parameters.AddWithValue("@declaration", val);
                                        cmd.Parameters.AddWithValue("@declaration_id", declaration_id);
                                        My.InsertUpdateData(cmd);
                                    }
                                    else
                                    {
                                        string query = "";
                                        string val = after(value, "#hdd#");
                                        if (declaration_id == "0")
                                        {
                                            declaration_id = mycode.auto_serial("declaration");

                                            query = @"INSERT INTO Declaration (entry_id,declaration_hn,declaration_id) 
                                                         values (@entry_id,@declaration_hn,@declaration_id); ";
                                        }
                                        else
                                        {
                                            query = @"update Declaration set declaration_hn=@declaration_hn where entry_id=@entry_id and declaration_id=@declaration_id; ";
                                        }
                                        SqlCommand cmd = new SqlCommand(query);
                                        cmd.Parameters.AddWithValue("@entry_id", entryid);
                                        cmd.Parameters.AddWithValue("@declaration_hn", val);
                                        cmd.Parameters.AddWithValue("@declaration_id", declaration_id);
                                        My.InsertUpdateData(cmd);
                                    }
                                }
                                #endregion declaration

                                #region pharases
                                else if (value.ToUpper().Contains("#pp#".ToUpper()) || value.ToUpper().Contains("#hpp#".ToUpper())) //for phrase
                                {
                                    if (value.ToUpper().Contains("#pp#".ToUpper()))
                                    {
                                        pre_prefix = "#pp#";
                                        string val = after(value, "#pp#");
                                        phrases_id = mycode.auto_serial("phrase_id");

                                        string query = @"INSERT INTO Phrase_details (entry_id,phrases_en,phrases_id,type)
                                                      values (@entry_id,@phrases_en,@phrases_id,@type); ";

                                        SqlCommand cmd = new SqlCommand(query);
                                        cmd.Parameters.AddWithValue("@entry_id", entryid);
                                        cmd.Parameters.AddWithValue("@phrases_en", val);
                                        cmd.Parameters.AddWithValue("@phrases_id", phrases_id);
                                        if (val.Contains("<img"))
                                        {
                                            cmd.Parameters.AddWithValue("@type", "1");
                                        }
                                        else
                                        {
                                            cmd.Parameters.AddWithValue("@type", "0");
                                        }
                                        My.InsertUpdateData(cmd);
                                    }
                                    else
                                    {
                                        pre_prefix = "#hpp#";
                                        string query = "";
                                        string val = after(value, "#hpp#");
                                        if (phrases_id == "0")
                                        {
                                            phrases_id = mycode.auto_serial("phrase_id");
                                            query = @"INSERT INTO Phrase_details (entry_id,phrases_en,phrases_id,type)
                                                      values (@entry_id,@phrases_en,@phrases_id,@type); ";
                                        }
                                        else
                                        {
                                            query = @"update Phrase_details set phrases_hn=@phrases_hn where entry_id=@entry_id and phrases_id=@phrases_id; ";
                                        }
                                        SqlCommand cmd = new SqlCommand(query);
                                        cmd.Parameters.AddWithValue("@entry_id", entryid);
                                        cmd.Parameters.AddWithValue("@phrases_hn", val);
                                        cmd.Parameters.AddWithValue("@phrases_id", phrases_id);

                                        if (val.Contains("<img"))
                                        {
                                            cmd.Parameters.AddWithValue("@type", "1");
                                        }
                                        else
                                        {
                                            cmd.Parameters.AddWithValue("@type", "0");
                                        }
                                        My.InsertUpdateData(cmd);
                                    }
                                }
                                #endregion pharases

                                #region questions
                                else if (value.ToUpper().Contains("#qq#".ToUpper()) || value.ToUpper().Contains("#hqq#".ToUpper()))
                                {

                                    string Question_name = "";
                                    if (value.ToUpper().Contains("#qq#".ToUpper()))
                                    {
                                        pre_prefix = "#qq#";
                                        //for questions
                                        string val = after(value, "#qq#");

                                        question_id = mycode.auto_serial("Online_questionid");

                                        string query = @"INSERT INTO question_info (test_id,Direction,Question_name,Answer,questionid,created_by,created_date,created_idate,updated_by,updated_date,updated_idate,isActive,Status,Section,marks,DI,Type, Uploding_status,Direction_HN,Question_name_HN,ans_HN,DI_HN,Language_type,Language_Itype,entry_id,Direction_id,Phrase_id,Upload_Type,Create_time,Objective_Entry_id) 
                                                values (@test_id,@Direction,@Question_name,@Answer,@questionid,@created_by,@created_date,@created_idate,@updated_by,@updated_date,@updated_idate,@isActive,@Status,@Section,@marks,@DI,@Type, @Uploding_status,@Direction_HN,@Question_name_HN,@ans_HN,@DI_HN,@Language_type,@Language_Itype,@entry_id,@Direction_id,@Phrase_id,@Upload_Type,@Create_time,@Objective_Entry_id);";
                                        SqlCommand cmd = new SqlCommand(query);
                                        cmd.Parameters.AddWithValue("@entry_id", entryid);
                                        cmd.Parameters.AddWithValue("@Direction_id", declaration_id);
                                        cmd.Parameters.AddWithValue("@Phrase_id", phrases_id);
                                        cmd.Parameters.AddWithValue("@test_id", testid);
                                        cmd.Parameters.AddWithValue("@questionid", question_id);
                                        cmd.Parameters.AddWithValue("@Direction", My.find_direction_en(declaration_id));
                                        cmd.Parameters.AddWithValue("@Question_name", val);
                                        Question_name = val;
                                        cmd.Parameters.AddWithValue("@Answer", "0");

                                        cmd.Parameters.AddWithValue("@created_by", hd_userid.Value);
                                        cmd.Parameters.AddWithValue("@created_date", mycode.date());
                                        cmd.Parameters.AddWithValue("@created_idate", mycode.idate());
                                        cmd.Parameters.AddWithValue("@updated_by", hd_userid.Value);
                                        cmd.Parameters.AddWithValue("@updated_date", mycode.date());
                                        cmd.Parameters.AddWithValue("@updated_idate", mycode.idate());
                                        cmd.Parameters.AddWithValue("@isActive", 0);
                                        cmd.Parameters.AddWithValue("@Status", "Pending");

                                        cmd.Parameters.AddWithValue("@Section", section_testmode);
                                        cmd.Parameters.AddWithValue("@marks", hd_marks.Value);//check code=get new table from Subject_Mapping_with_Class_Exam

                                        cmd.Parameters.AddWithValue("@Uploding_status", "Uploaded");
                                        cmd.Parameters.AddWithValue("@Direction_HN", My.find_direction_hn(declaration_id));

                                        DataTable dt = My.find_phrases(phrases_id);
                                        if (dt.Rows.Count > 0)
                                        {
                                            cmd.Parameters.AddWithValue("@DI", dt.Rows[0][2].ToString());
                                            cmd.Parameters.AddWithValue("@DI_HN", dt.Rows[0][4].ToString());
                                            if (dt.Rows[0][5].ToString() == "1")
                                            {
                                                cmd.Parameters.AddWithValue("@Type", "Img");
                                            }
                                            else
                                            {
                                                cmd.Parameters.AddWithValue("@Type", "Text");

                                            }
                                        }
                                        else
                                        {
                                            cmd.Parameters.AddWithValue("@DI", "");
                                            cmd.Parameters.AddWithValue("@DI_HN", "");
                                            cmd.Parameters.AddWithValue("@Type", "None");
                                        }
                                        cmd.Parameters.AddWithValue("@Question_name_HN", "");
                                        cmd.Parameters.AddWithValue("@ans_HN", "");
                                        cmd.Parameters.AddWithValue("@Language_type", language_type);
                                        cmd.Parameters.AddWithValue("@Language_Itype", language_id);
                                        cmd.Parameters.AddWithValue("@Upload_Type", "Bulk");
                                        cmd.Parameters.AddWithValue("@Create_time", mycode.time());
                                        cmd.Parameters.AddWithValue("@Objective_Entry_id", group_entryid);

                                        My.InsertUpdateDatatest(cmd, Question_name);
                                    }
                                    else
                                    {

                                        pre_prefix = "#hqq#";
                                        string query = "";
                                        string val = after(value, "#hqq#");
                                        if (question_id == "0")
                                        {
                                           question_id = mycode.auto_serial("Online_questionid");
                                            query = @"INSERT INTO question_info (test_id,Direction,Question_name,Answer,questionid,created_by,created_date,created_idate,updated_by,updated_date,updated_idate,isActive,Status,Section,marks,DI,Type, Uploding_status,Direction_HN,Question_name_HN,ans_HN,DI_HN,Language_type,Language_Itype,entry_id,Direction_id,Phrase_id,Upload_Type,Create_time,Objective_Entry_id) 
                                                values (@test_id,@Direction,@Question_name,@Answer,@questionid,@created_by,@created_date,@created_idate,@updated_by,@updated_date,@updated_idate,@isActive,@Status,@Section,@marks,@DI,@Type, @Uploding_status,@Direction_HN,@Question_name_HN,@ans_HN,@DI_HN,@Language_type,@Language_Itype,@entry_id,@Direction_id,@Phrase_id,@Upload_Type,@Create_time,@Objective_Entry_id);";
                                        }
                                        else
                                        {
                                            query = @"update question_info set Question_name_HN=@Question_name_HN where test_id=@test_id and questionid=@questionid;";

                                        }
                                        SqlCommand cmd = new SqlCommand(query);
                                        cmd.Parameters.AddWithValue("@entry_id", entryid);
                                        cmd.Parameters.AddWithValue("@Direction_id", declaration_id);
                                        cmd.Parameters.AddWithValue("@Phrase_id", phrases_id);
                                        cmd.Parameters.AddWithValue("@test_id", testid);
                                        cmd.Parameters.AddWithValue("@Direction", My.find_direction_en(declaration_id));
                                        cmd.Parameters.AddWithValue("@Question_name", "");
                                        Question_name = val;
                                        cmd.Parameters.AddWithValue("@Answer", "0");
                                        cmd.Parameters.AddWithValue("@questionid", question_id);
                                        cmd.Parameters.AddWithValue("@created_by", hd_userid.Value);
                                        cmd.Parameters.AddWithValue("@created_date", mycode.date());
                                        cmd.Parameters.AddWithValue("@created_idate", mycode.idate());
                                        cmd.Parameters.AddWithValue("@updated_by", hd_userid.Value);
                                        cmd.Parameters.AddWithValue("@updated_date", mycode.date());
                                        cmd.Parameters.AddWithValue("@updated_idate", mycode.idate());
                                        cmd.Parameters.AddWithValue("@isActive", 0);
                                        cmd.Parameters.AddWithValue("@Status", "Pending");

                                        cmd.Parameters.AddWithValue("@Section", section_testmode);
                                        cmd.Parameters.AddWithValue("@marks", "0");
                                        cmd.Parameters.AddWithValue("@Objective_Entry_id", group_entryid);

                                        DataTable dt = My.find_phrases(phrases_id);
                                        if (dt.Rows.Count > 0)
                                        {
                                            cmd.Parameters.AddWithValue("@DI", dt.Rows[0][2].ToString());
                                            cmd.Parameters.AddWithValue("@DI_HN", dt.Rows[0][4].ToString());
                                            if (dt.Rows[0][5].ToString() == "1")
                                            {
                                                cmd.Parameters.AddWithValue("@Type", "Img");
                                            }
                                            else
                                            {
                                                cmd.Parameters.AddWithValue("@Type", "Text");
                                            }
                                        }
                                        else
                                        {
                                            cmd.Parameters.AddWithValue("@DI", "");
                                            cmd.Parameters.AddWithValue("@DI_HN", "");
                                            cmd.Parameters.AddWithValue("@Type", "None");
                                        }
                                        cmd.Parameters.AddWithValue("@Uploding_status", "Uploaded");
                                        cmd.Parameters.AddWithValue("@Direction_HN", My.find_direction_hn(declaration_id));
                                        cmd.Parameters.AddWithValue("@Question_name_HN", val);
                                        cmd.Parameters.AddWithValue("@ans_HN", "");
                                        cmd.Parameters.AddWithValue("@Language_type", language_type);
                                        cmd.Parameters.AddWithValue("@Language_Itype", language_id);
                                        cmd.Parameters.AddWithValue("@Upload_Type", "Bulk");
                                        cmd.Parameters.AddWithValue("@Create_time", mycode.time());
                                        My.InsertUpdateDatatest(cmd, Question_name);

                                    }

                                }
                                #endregion questions

                                #region options
                                else if (value.ToUpper().Contains("#oo#".ToUpper()) || value.ToUpper().Contains("#hoo#".ToUpper()))
                                {


                                    if (value.ToUpper().Contains("#oo#".ToUpper()))
                                    {

                                        pre_prefix = "#oo#";
                                        //for options
                                        string[] lines = Regex.Split(value, "#oo#");
                                        string optionid = "0";
                                        foreach (string line in lines)
                                        {
                                            string option = line;
                                            string query = "";
                                            if (!string.IsNullOrEmpty(option) && option.Length > 3)
                                            {


                                                optionid = mycode.auto_serial("option_id");
                                                option = option.Substring(3);

                                                if (hn_opt_id == "")
                                                {

                                                    if (en_opt_id == "")
                                                    {
                                                        en_opt_id = optionid;
                                                    }
                                                    else
                                                    {
                                                        en_opt_id = en_opt_id + "^" + optionid;
                                                    }
                                                    query = @"INSERT INTO question_answer_Master (Section,test_code,quest_code,opt_code,option_text,created_date,created_idate,created_by,updated_date,Updateidate,opetion_text_HN,Option_type,Option_Itype,entry_id,Direction_id,Phrase_id,Objective_Entry_id) 
                                                        values (@Section,@test_code,@quest_code,@opt_code,@option_text,@created_date,@created_idate,@created_by,@updated_date,@Updateidate,@opetion_text_HN,@Option_type,@Option_Itype,@entry_id,@Direction_id,@Phrase_id,@Objective_Entry_id); ";
                                                }
                                                else
                                                {
                                                    string[] opt = en_opt_id.Split('^');
                                                    query = @"update question_answer_Master set option_text=@option_text,Option_type=@Option_type,Option_Itype=@Option_Itype  where opt_code='" + opt[k].ToString() + "'; ";
                                                    k++;
                                                }
                                                SqlCommand cmd = new SqlCommand(query);
                                                cmd.Parameters.AddWithValue("@entry_id", entryid);
                                                cmd.Parameters.AddWithValue("@Direction_id", declaration_id);
                                                cmd.Parameters.AddWithValue("@Phrase_id", phrases_id);
                                                cmd.Parameters.AddWithValue("@Section", section_testmode);
                                                cmd.Parameters.AddWithValue("@test_code", testid);
                                                cmd.Parameters.AddWithValue("@quest_code", question_id);
                                                cmd.Parameters.AddWithValue("@opt_code", optionid);
                                                cmd.Parameters.AddWithValue("@option_text", option.Trim());
                                                cmd.Parameters.AddWithValue("@created_date", mycode.date());
                                                cmd.Parameters.AddWithValue("@created_idate", mycode.idate());
                                                cmd.Parameters.AddWithValue("@created_by", hd_userid.Value);
                                                cmd.Parameters.AddWithValue("@updated_by", hd_userid.Value);
                                                cmd.Parameters.AddWithValue("@updated_date", mycode.date());
                                                cmd.Parameters.AddWithValue("@Updateidate", mycode.idate());
                                                cmd.Parameters.AddWithValue("@Objective_Entry_id", group_entryid);

                                                cmd.Parameters.AddWithValue("@opetion_text_HN", "");
                                                if (option.Contains("<img"))
                                                {
                                                    cmd.Parameters.AddWithValue("@Option_type", "Pictorial");
                                                    cmd.Parameters.AddWithValue("@Option_Itype", "1");
                                                }
                                                else
                                                {
                                                    cmd.Parameters.AddWithValue("@Option_type", "Textual");
                                                    cmd.Parameters.AddWithValue("@Option_Itype", "0");
                                                }
                                                My.InsertUpdateData(cmd);

                                            }
                                        }
                                    }
                                    else
                                    {
                                        pre_prefix = "#hoo#";
                                        //for options
                                        string[] lines = Regex.Split(value, "#hoo#");
                                        string optionid = "0";

                                        foreach (string line in lines)
                                        {
                                            string option = line;

                                            if (!string.IsNullOrEmpty(option) && option.Length > 3)
                                            {
                                                string query = "";

                                                option = option.Substring(3);

                                                if (en_opt_id == "")
                                                {
                                                    optionid = mycode.auto_serial("option_id");
                                                    if (hn_opt_id == "")
                                                    {
                                                        hn_opt_id = optionid;
                                                    }
                                                    else
                                                    {
                                                        hn_opt_id = hn_opt_id + "^" + optionid;
                                                    }
                                                    query = @"INSERT INTO question_answer_Master (Section,test_code,quest_code,opt_code,option_text,created_date,created_idate,created_by,updated_date,Updateidate,opetion_text_HN,Option_type,Option_Itype,entry_id,Direction_id,Phrase_id,Objective_Entry_id) 
                                                        values (@Section,@test_code,@quest_code,@opt_code,@option_text,@created_date,@created_idate,@created_by,@updated_date,@Updateidate,@opetion_text_HN,@Option_type,@Option_Itype,@entry_id,@Direction_id,@Phrase_id,@Objective_Entry_id); ";

                                                }
                                                else
                                                {
                                                    string[] opt = en_opt_id.Split('^');
                                                    query = @"update question_answer_Master set opetion_text_HN=@opetion_text_HN,Option_type=@Option_type,Option_Itype=@Option_Itype  where opt_code='" + opt[k].ToString() + "'; ";
                                                    k++;
                                                }
                                                SqlCommand cmd = new SqlCommand(query);
                                                cmd.Parameters.AddWithValue("@Objective_Entry_id", group_entryid);
                                                cmd.Parameters.AddWithValue("@entry_id", entryid);
                                                cmd.Parameters.AddWithValue("@Direction_id", declaration_id);
                                                cmd.Parameters.AddWithValue("@Phrase_id", phrases_id);
                                                cmd.Parameters.AddWithValue("@Section", section_testmode);
                                                cmd.Parameters.AddWithValue("@test_code", testid);
                                                cmd.Parameters.AddWithValue("@quest_code", question_id);
                                                cmd.Parameters.AddWithValue("@opt_code", optionid);
                                                cmd.Parameters.AddWithValue("@option_text", option.Trim());
                                                cmd.Parameters.AddWithValue("@created_date", mycode.date());
                                                cmd.Parameters.AddWithValue("@created_idate", mycode.idate());
                                                cmd.Parameters.AddWithValue("@created_by", hd_userid.Value);
                                                cmd.Parameters.AddWithValue("@updated_by", hd_userid.Value);
                                                cmd.Parameters.AddWithValue("@updated_date", mycode.date());
                                                cmd.Parameters.AddWithValue("@Updateidate", mycode.idate());
                                                cmd.Parameters.AddWithValue("@opetion_text_HN", option);

                                                if (option.Contains("<img"))
                                                {
                                                    cmd.Parameters.AddWithValue("@Option_type", "Pictorial");
                                                    cmd.Parameters.AddWithValue("@Option_Itype", "1");
                                                }
                                                else
                                                {
                                                    cmd.Parameters.AddWithValue("@Option_type", "Textual");
                                                    cmd.Parameters.AddWithValue("@Option_Itype", "0");
                                                }
                                                My.InsertUpdateData(cmd);
                                            }
                                        }
                                    }
                                }
                                #endregion options

                                #region answer
                                else if (value.ToUpper().Contains("#ans#".ToUpper()) || value.ToUpper().Contains("#hans#".ToUpper()))
                                {
                                    if (value.ToUpper().Contains("#ans#".ToUpper()))
                                    {
                                        pre_prefix = "#ans#";
                                        //for answer
                                        string val = after(value, "#ans#");
                                        int index = val.IndexOf("<img");
                                        string option = "";
                                        string option_val = "";
                                        if (index > 0)
                                        {
                                            option = val.Substring(0, index - 2).Trim();
                                            option_val = val.Substring(index);
                                        }
                                        else
                                        {
                                            option = val.Substring(0, 9).Trim();
                                            option_val = val.Substring(10);
                                        }

                                        string query = @"update question_info set Answer=@Answer,ans=@ans,Option_type=@Option_type,Option_Itype=@Option_Itype where questionid=@questionid and entry_id=@entry_id and Direction_id=@Direction_id and Phrase_id=@Phrase_id and Section='" + section_testmode + "' and test_id=" + testid + "; ";
                                        SqlCommand cmd = new SqlCommand(query);
                                        cmd.Parameters.AddWithValue("@entry_id", entryid);
                                        cmd.Parameters.AddWithValue("@Direction_id", declaration_id);
                                        cmd.Parameters.AddWithValue("@Phrase_id", phrases_id);
                                        cmd.Parameters.AddWithValue("@Answer", option.Trim());
                                        cmd.Parameters.AddWithValue("@ans", option_val);

                                        cmd.Parameters.AddWithValue("@questionid", question_id);
                                        cmd.Parameters.AddWithValue("@Section", section_testmode);

                                        if (option_val.Contains("<img"))
                                        {
                                            cmd.Parameters.AddWithValue("@Option_type", "Pictoral");
                                            cmd.Parameters.AddWithValue("@Option_Itype", "1");
                                        }
                                        else
                                        {
                                            cmd.Parameters.AddWithValue("@Option_type", "Textual");
                                            cmd.Parameters.AddWithValue("@Option_Itype", "0");
                                        }


                                        My.InsertUpdateData(cmd);
                                        add_option_id_in_question_info(question_id, option, testid);
                                    }
                                    else
                                    {
                                        pre_prefix = "#hans#";
                                        //for answer
                                        string val = after(value, "#hans#");
                                        int index = val.IndexOf("<img");
                                        string option = "";
                                        string option_val = "";
                                        if (index > 0)
                                        {
                                            option = val.Substring(0, index - 2).Trim();
                                            option_val = val.Substring(index);
                                        }
                                        else
                                        {
                                            option = val.Substring(0, 9).Trim();
                                            option_val = val.Substring(10);
                                        }

                                        string query = @"update question_info set ans_HN=@ans_HN,Option_type=@Option_type,Option_Itype=@Option_Itype where questionid=@questionid and entry_id=@entry_id and Direction_id=@Direction_id and Phrase_id=@Phrase_id and Section='" + section_testmode + "' and test_id='" + testid + "'; ";
                                        SqlCommand cmd = new SqlCommand(query);
                                        cmd.Parameters.AddWithValue("@entry_id", entryid);
                                        cmd.Parameters.AddWithValue("@Direction_id", declaration_id);
                                        cmd.Parameters.AddWithValue("@Phrase_id", phrases_id);
                                        cmd.Parameters.AddWithValue("@ans_HN", option_val.Trim());

                                        cmd.Parameters.AddWithValue("@questionid", question_id);
                                        cmd.Parameters.AddWithValue("@Section", section_testmode);

                                        if (option_val.Contains("<img"))
                                        {
                                            cmd.Parameters.AddWithValue("@Option_type", "Pictoral");
                                            cmd.Parameters.AddWithValue("@Option_Itype", "1");
                                        }
                                        else
                                        {
                                            cmd.Parameters.AddWithValue("@Option_type", "Textual");
                                            cmd.Parameters.AddWithValue("@Option_Itype", "0");
                                        }
                                        My.InsertUpdateData(cmd);
                                    }
                                }
                                #endregion answer

                                #region marks
                                else if (value.Contains("#mrk#"))// masks=get Subject_Mapping_with_Class_Exam table
                                {
                                    en_opt_id = "";
                                    hn_opt_id = "";
                                    k = 0;
                                    pre_prefix = "#mrk#";

                                    //for answer
                                    string val = after(value, "#mrk#").Trim();
                                    double mark = My.converttodouble(val);
                                    if (My.converttodouble(hd_marks.Value) > 0)
                                    {
                                        mark = My.converttodouble(hd_marks.Value);
                                    }
                                    else
                                    {
                                        mark = 1;
                                    }

                                    string query = @"update question_info set marks=@marks where questionid=@questionid and entry_id=@entry_id and Direction_id=@Direction_id and Phrase_id=@Phrase_id and Section='" + section_testmode + "' and test_id='" + testid + "'; ";
                                    SqlCommand cmd = new SqlCommand(query);
                                    cmd.Parameters.AddWithValue("@entry_id", entryid);
                                    cmd.Parameters.AddWithValue("@Direction_id", declaration_id);
                                    cmd.Parameters.AddWithValue("@Phrase_id", phrases_id);
                                    cmd.Parameters.AddWithValue("@marks", mark);
                                    cmd.Parameters.AddWithValue("@questionid", question_id);
                                    My.InsertUpdateData(cmd);

                                }
                                #endregion marks

                                #region Explanetion
                                else if (value.Contains("#exp#") || value.Contains("#hexp#"))
                                {


                                    if (value.Contains("#exp#"))
                                    {

                                        pre_prefix = "#exp#";
                                        string val = after(value, "#exp#");

                                        string query = @"INSERT INTO Question_Explanation (test_id,Question_SL,Section,questionid,Explanation_en,Explanation_hn,Status,isActive,Uploding_status,created_by,Created_by_id,Language_type,Language_Itype,entry_id,Direction_id,Phrase_id,Objective_Entry_id)
                                                        values (@test_id,@Question_SL,@Section,@questionid,@Explanation_en,@Explanation_hn,@Status,@isActive,@Uploding_status,@created_by,@Created_by_id,@Language_type,@Language_Itype,@entry_id,@Direction_id,@Phrase_id,@Objective_Entry_id); ";

                                        SqlCommand cmd = new SqlCommand(query);

                                        cmd.Parameters.AddWithValue("@Objective_Entry_id", group_entryid);
                                        cmd.Parameters.AddWithValue("@entry_id", entryid);
                                        cmd.Parameters.AddWithValue("@Direction_id", declaration_id);
                                        cmd.Parameters.AddWithValue("@Phrase_id", phrases_id);
                                        cmd.Parameters.AddWithValue("@test_id", testid);
                                        cmd.Parameters.AddWithValue("@questionid", question_id);

                                        cmd.Parameters.AddWithValue("@Question_SL", "");
                                        cmd.Parameters.AddWithValue("@Section", section_testmode);
                                        cmd.Parameters.AddWithValue("@Explanation_en", val);
                                        cmd.Parameters.AddWithValue("@Explanation_hn", val);
                                        cmd.Parameters.AddWithValue("@Status", "Pending");
                                        cmd.Parameters.AddWithValue("@isActive", "0");
                                        cmd.Parameters.AddWithValue("@Uploding_status", "Uploaded");
                                        cmd.Parameters.AddWithValue("@Created_by", "DOE");
                                        cmd.Parameters.AddWithValue("@Created_by_id", 1);
                                        cmd.Parameters.AddWithValue("@Faculty_id", 0);
                                        cmd.Parameters.AddWithValue("@Faculty_hod_id", 0);
                                        cmd.Parameters.AddWithValue("@Language_type", language_type);
                                        cmd.Parameters.AddWithValue("@Language_Itype", language_id);

                                        My.InsertUpdateData(cmd);

                                    }
                                    else
                                    {
                                        pre_prefix = "#hexp#";
                                        string val = after(value, "#hexp#");

                                        string query = @"INSERT INTO Question_Explanation (test_id,Question_SL,Section,questionid,Explanation_en,Explanation_hn,Status,isActive,Uploding_status,created_by,Created_by_id,Language_type,Language_Itype,entry_id,Direction_id,Phrase_id,group_entryid)
                                                        values (@test_id,@Question_SL,@Section,@questionid,@Explanation_en,@Explanation_hn,@Status,@isActive,@Uploding_status,@created_by,@Created_by_id,@Language_type,@Language_Itype,@entry_id,@Direction_id,@Phrase_id,@group_entryid); ";

                                        SqlCommand cmd = new SqlCommand(query);
                                        cmd.Parameters.AddWithValue("@Objective_Entry_id", group_entryid);
                                        cmd.Parameters.AddWithValue("@entry_id", entryid);
                                        cmd.Parameters.AddWithValue("@Direction_id", declaration_id);
                                        cmd.Parameters.AddWithValue("@Phrase_id", phrases_id);
                                        cmd.Parameters.AddWithValue("@test_id", testid);
                                        cmd.Parameters.AddWithValue("@questionid", question_id);
                                        cmd.Parameters.AddWithValue("@Question_SL", "");
                                        cmd.Parameters.AddWithValue("@Section", section_testmode);
                                        cmd.Parameters.AddWithValue("@questionid", question_id);
                                        cmd.Parameters.AddWithValue("@Explanation_en", val);
                                        cmd.Parameters.AddWithValue("@Explanation_hn", val);
                                        cmd.Parameters.AddWithValue("@Status", "Pending");
                                        cmd.Parameters.AddWithValue("@isActive", "0");
                                        cmd.Parameters.AddWithValue("@Uploding_status", "Uploaded");
                                        cmd.Parameters.AddWithValue("@Created_by", "DOE");
                                        cmd.Parameters.AddWithValue("@Created_by_id", hd_userid.Value);
                                        cmd.Parameters.AddWithValue("@Faculty_id", 0);
                                        cmd.Parameters.AddWithValue("@Faculty_hod_id", 0);
                                        cmd.Parameters.AddWithValue("@Language_type", language_type);
                                        cmd.Parameters.AddWithValue("@Language_Itype", language_id);

                                        My.InsertUpdateData(cmd);
                                    }

                                }
                                #endregion Explanetion

                                #region pre-prefix
                                else
                                {
                                    string query = "";
                                    if (pre_prefix == "#pp#")
                                    {

                                        query = @"update Phrase_details set phrases_en=phrases_en+@phrases_en,type=@type where entry_id=@entry_id and phrases_id=@phrases_id; ";

                                        SqlCommand cmd = new SqlCommand(query);
                                        cmd.Parameters.AddWithValue("@entry_id", entryid);
                                        cmd.Parameters.AddWithValue("@phrases_en", "</br>" + value);
                                        cmd.Parameters.AddWithValue("@phrases_id", phrases_id);

                                        if (value.Contains("<img"))
                                        {
                                            cmd.Parameters.AddWithValue("@type", "1");
                                        }
                                        else
                                        {
                                            cmd.Parameters.AddWithValue("@type", "0");
                                        }
                                        My.InsertUpdateData(cmd);
                                    }
                                    else if (pre_prefix == "#exp#" || pre_prefix == "#hexp#")
                                    {
                                        query = @"update Question_Explanation set Explanation_en=Explanation_en+@Explanation_en,Explanation_hn=Explanation_hn+@Explanation_hn where entry_id=@entry_id and Phrase_id=@Phrase_id and Direction_id=@Direction_id and test_id=@test_id and questionid=@questionid; ";
                                        SqlCommand cmd = new SqlCommand(query);
                                        cmd.Parameters.AddWithValue("@entry_id", entryid);
                                        cmd.Parameters.AddWithValue("@Direction_id", declaration_id);
                                        cmd.Parameters.AddWithValue("@Phrase_id", phrases_id);
                                        cmd.Parameters.AddWithValue("@test_id", testid);
                                        cmd.Parameters.AddWithValue("@questionid", question_id);
                                        cmd.Parameters.AddWithValue("@Explanation_en", "</br>" + value);
                                        cmd.Parameters.AddWithValue("@Explanation_hn", "</br>" + value);


                                        My.InsertUpdateData(cmd);
                                    }
                                    else if (pre_prefix == "#hpp#")
                                    {
                                        query = @"update Phrase_details set phrases_hn=phrases_hn+@phrases_hn,type=@type where entry_id=@entry_id and phrases_id=@phrases_id; ";

                                        SqlCommand cmd = new SqlCommand(query);
                                        cmd.Parameters.AddWithValue("@entry_id", entryid);
                                        cmd.Parameters.AddWithValue("@phrases_hn", "</br>" + value);
                                        cmd.Parameters.AddWithValue("@phrases_id", phrases_id);

                                        if (value.Contains("<img"))
                                        {
                                            cmd.Parameters.AddWithValue("@type", "1");
                                        }
                                        else
                                        {
                                            cmd.Parameters.AddWithValue("@type", "0");
                                        }
                                        My.InsertUpdateData(cmd);
                                    }
                                    else if (pre_prefix == "#qq#")
                                    {
                                        query = @"update question_info set Question_name=Question_name+@Question_name where test_id=@test_id and questionid=@questionid;";

                                        SqlCommand cmd = new SqlCommand(query);
                                        cmd.Parameters.AddWithValue("@test_id", testid);
                                        cmd.Parameters.AddWithValue("@questionid", question_id);
                                        cmd.Parameters.AddWithValue("@Question_name", "</br>" + value);
                                        My.InsertUpdateData(cmd);
                                    }
                                    else if (pre_prefix == "#hqq#")
                                    {
                                        query = @"update question_info set Question_name_HN=Question_name_HN+@Question_name_HN where test_id=@test_id and questionid=@questionid;";

                                        SqlCommand cmd = new SqlCommand(query);
                                        cmd.Parameters.AddWithValue("@test_id", testid);
                                        cmd.Parameters.AddWithValue("@questionid", question_id);
                                        cmd.Parameters.AddWithValue("@Question_name_HN", "</br>" + value);
                                        My.InsertUpdateData(cmd);
                                    }
                                }
                                #endregion pre-prefix
                            }
                        }

                        text = text + "<br/> <br/>";
                    }
                }
            }
        }

        private void Send_data_input(string filepath)
        {
            String originalPath = My.url();// HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, "");
            SqlCommand cmd;
            string strQuery = @"INSERT INTO Save_question_url (Urlpath,Test_id,Section_id,Date,Idate,Time,User_id) values (@Urlpath,@Test_id,@Section_id,@Date,@Idate,@Time,@User_id)";
            cmd = new SqlCommand(strQuery);
            cmd.Parameters.AddWithValue("@Urlpath", originalPath + filepath);
            cmd.Parameters.AddWithValue("@Test_id", hd_test_id.Value);
            cmd.Parameters.AddWithValue("@Section_id", hd_section_id.Value);
            cmd.Parameters.AddWithValue("@Date", mycode.date());
            cmd.Parameters.AddWithValue("@Idate", mycode.idate());
            cmd.Parameters.AddWithValue("@time", mycode.time());
            cmd.Parameters.AddWithValue("@User_id", hd_userid.Value);


            if (My.InsertUpdateData(cmd))
            {
            }




        }
        private void add_option_id_in_question_info(string question_id, string option,string testid)
        {
            string query1 = "Select * from question_answer_Master where quest_code='" + question_id + "' and test_code='"+ testid + "' order by id ASC";
            SqlDataAdapter ad1 = new SqlDataAdapter(query1, My.conn);
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
                        update_question_info(opt_code, question_id, testid);
                        break;
                    }
                    index2++;
                }
            }
        }
        private void update_question_info(string optionid, string question_id,string testid)
        {
            string query = @"update question_info set Opetion_id=@Opetion_id  where  questionid=" + question_id + " and test_id='"+ testid + "' ";
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@questionid", question_id);
            cmd.Parameters.AddWithValue("@Opetion_id", optionid);
            My.InsertUpdateData(cmd);
        }
        public string after(string value, string a)
        {
            int posA = value.IndexOf(a);
            if (posA == -1)
            {
                return "";
            }

            int adjustedPosA = posA + a.Length;

            return value.Substring(adjustedPosA);
        }
        private string OMML(string omml, string path2)
        {


            XslCompiledTransform xslTransform = new XslCompiledTransform();
            xslTransform.Load(path2);

            using (XmlReader reader = XmlReader.Create(new StringReader(omml)))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    XmlWriterSettings settings = xslTransform.OutputSettings.Clone();

                    // Configure xml writer to omit xml declaration.
                    settings.ConformanceLevel = ConformanceLevel.Fragment;
                    settings.OmitXmlDeclaration = true;

                    XmlWriter xw = XmlWriter.Create(ms, settings);

                    // Transform our OfficeMathML to MathML


                    xslTransform.Transform(reader, xw);
                    ms.Seek(0, SeekOrigin.Begin);

                    StreamReader sr = new StreamReader(ms, Encoding.UTF8);
                    string MathML = sr.ReadToEnd();
                    MathML = MathML.Replace("mml:", "");

                    return MathML;
                }
            }
        }


        #endregion

        protected void btn_delete_question_Click(object sender, EventArgs e)
        {
            string query1;
            try
            {
                string query2;
                if (ddl_section.Text=="ALL")
                {

                    query2 = "select Exam_id from OLINETEST_EXAM_NAME where Entry_id='" + ViewState["entryid"].ToString() + "'";
                }
                else
                {
                    query2 = "select Exam_id from OLINETEST_EXAM_NAME where Entry_id='" + ViewState["entryid"].ToString() + "' and Section='"+ ddl_section.Text + "'";

                }
                DataTable cdt = mycode.FillData(query2);
                if (cdt.Rows.Count == 0)
                {

                }
                else
                {
                    for (int i = 0; i < cdt.Rows.Count; i++)
                    {
                        string testid = cdt.Rows[i]["Exam_id"].ToString();
                        query1 = "Delete from Declaration where declaration_id in (Select Direction_id from question_info where test_id='" + testid + "'  );";
                        query1 = query1 + "Delete from Phrase_details where phrases_id in (Select Phrase_id from question_info where test_id='" + testid + "'  );";
                        query1 = query1 + "Delete from Question_Explanation where test_id ='" + testid + "' ;";
                        query1 = query1 + "Delete from question_answer_Master where test_code ='" + testid + "'  ;";
                        query1 = query1 + "Delete from question_info where test_id ='" + testid + "' ;";
                        query1 = query1 + "Delete from Save_question_url where Test_id ='" + testid + "' ;";
                        query1 = query1 + "Delete from Question_Upload_History where Test_id ='" + testid + "' ;";
                        query1 = query1 + "update Test_info set isActive='0',Status='Pending',MapStatus='Not Map' where Test_id ='" + testid + "' ;";
                        query1 = query1 + "Delete from Section_Arranging where Test_id ='" + testid + "' ;";
                        query1 = query1 + "Delete from Upload_Question_Question_Temp where Test_id ='" + testid + "' ;";
                        mycode.executequery(query1);
                    }
                    Alertme("Question has been deleted successfully", "success");
                    viequestion.Visible = false;
                    btn_delete_question.Visible = false;
                    btn_final_submit.Visible = false;
                    btn_s_add.Visible = true;
                    lblmsg.Text = "";
                }
            }
            catch (Exception ex)
            {


            }
        }




        #region final submit

        protected void btn_final_submit_Click(object sender, EventArgs e)
        {
            try
            {
                bool send = false;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
                {
                    SqlConnection con = new SqlConnection(My.conn);
                    con.Open();
                    string query = "";
                    if (ddl_section.Text == "ALL")
                    {
                        query = "Select * from OLINETEST_EXAM_NAME where Entry_id='" + ViewState["entryid"].ToString() + "'";
                    }
                    else
                    {
                        query = "Select * from OLINETEST_EXAM_NAME where Entry_id='" + ViewState["entryid"].ToString() + "' and Section='" + ddl_section.Text + "'";
                    }
                    DataTable cdt = payments.dataTable(query, con);
                    if (cdt.Rows.Count == 0)
                    {
                    }
                    else
                    {
                        for (int j = 0; j < cdt.Rows.Count; j++)
                        {
                            string Testid = cdt.Rows[j]["Exam_id"].ToString();
                            string Exam_id = cdt.Rows[j]["Exam_id"].ToString();
                            string section = cdt.Rows[j]["Section"].ToString();
                            update_question_no(Testid, Exam_id, section,con);
                            string query1 = "update Question_Upload_History set Status='Final Copy',Final_Submited_Date='" + mycode.date() + "',Final_Submited_Idate='" + mycode.idate() + "',Final_Submited_time='" + mycode.time() + "' where Test_id=" + Testid + "";
                            payments.exeSql(query1, con);
                        }
                    }
                    send = true;
                    con.Close();
                    scope.Complete();
                }

                if (send == true)
                {
                    lblmsg.Text = "Question has been finally updated successfully";
                    // delete_all_temp_data();

                    Alertme("Question has been finally updated successfully", "success");
                    viequestion.Visible = false;
                    btn_delete_question.Visible = false;
                    btn_final_submit.Visible = false;

                }
            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());
            }
        }

        #region section Arranging
        private void update_question_no(string Testid, string Exam_id, string section,SqlConnection con)
        {
            Add_Section_Arranging(Testid,con);
            String query = "select * from Section_Arranging where Test_id='" + Testid + "' order by Position";
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
        private void Add_Section_Arranging(string Testid, SqlConnection con)
        {
            try
            {
                string examtypecode = "4";// PT
                string testmode_code = "101";
                string testid = Testid;
                string position = fsp.no_position(testid, con);


                SqlCommand cmd = new SqlCommand("Select * from Section_Arranging where test_id=" + Testid + " and Section_name='" + lbl_subject.Text + "' ");
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
        #endregion




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


        #region this is old code not current use
        private void update_data_question_info(SqlConnection con)
        {
            string query1 = "Select top 1 * from  Tamp_question_info  where test_id='" + hd_test_id.Value + "'    and Section='" + hd_section_id.Value + "'";
            SqlDataAdapter ad = new SqlDataAdapter(query1, con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Tamp_question_info");
            DataTable dt = ds.Tables[0];
            int rowcount = dt.Rows.Count;
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string Direction = dt.Rows[i]["Direction"].ToString();
                    string Question_name = dt.Rows[i]["Question_name"].ToString();
                    string Answer = dt.Rows[i]["Answer"].ToString();
                    string ans = dt.Rows[i]["ans"].ToString();
                    string marks = dt.Rows[i]["marks"].ToString();
                    string DI = dt.Rows[i]["DI"].ToString();
                    string Type = dt.Rows[i]["Type"].ToString();
                    string Direction_HN = dt.Rows[i]["Direction_HN"].ToString();
                    string Question_name_HN = dt.Rows[i]["Question_name_HN"].ToString();
                    string ans_HN = dt.Rows[i]["ans_HN"].ToString();
                    string DI_HN = dt.Rows[i]["DI_HN"].ToString();
                    string Option_type = dt.Rows[i]["Option_type"].ToString();
                    string Option_Itype = dt.Rows[i]["Option_Itype"].ToString();
                    string Direction_id = dt.Rows[i]["Direction_id"].ToString();
                    string Phrase_id = dt.Rows[i]["Phrase_id"].ToString();
                    string Opetion_id = dt.Rows[i]["Opetion_id"].ToString();
                    send_explanation_data(Direction, Question_name, Answer, ans, marks, DI, Type, Direction_HN, Question_name_HN, ans_HN, DI_HN, Option_type, Option_Itype, Direction_id, Phrase_id, Opetion_id, con);
                }
            }
        }
        private void send_explanation_data(string Direction, string Question_name, string Answer, string ans, string marks, string DI, string Type, string Direction_HN, string Question_name_HN, string ans_HN, string DI_HN, string Option_type, string Option_Itype, string Direction_id, string Phrase_id, string Opetion_id, SqlConnection con)
        {
            string query = " Update question_info set Direction=@Direction,Question_name=@Question_name,Answer=@Answer,updated_date=@updated_date,updated_idate=@updated_idate,ans=@ans,marks=@marks,DI=@DI,Type=@Type,Direction_HN=@Direction_HN,Question_name_HN=@Question_name_HN,ans_HN=@ans_HN,DI_HN=@DI_HN,Option_type=@Option_type,Option_Itype=@Option_Itype,Direction_id=@Direction_id,Phrase_id=@Phrase_id,Opetion_id=@Opetion_id where test_id='" + hd_test_id.Value + "'   and Section='" + hd_section_id.Value + "'";
            SqlCommand cmd;
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Direction", Direction);
            cmd.Parameters.AddWithValue("@Question_name", Question_name);
            cmd.Parameters.AddWithValue("@Answer", Answer);
            cmd.Parameters.AddWithValue("@updated_date", mycode.date());
            cmd.Parameters.AddWithValue("@updated_idate", mycode.idate());
            cmd.Parameters.AddWithValue("@ans", ans);
            cmd.Parameters.AddWithValue("@marks", marks);
            cmd.Parameters.AddWithValue("@DI", DI);
            cmd.Parameters.AddWithValue("@Type", Type);
            cmd.Parameters.AddWithValue("@Direction_HN", Direction_HN);
            cmd.Parameters.AddWithValue("@Question_name_HN", Question_name_HN);
            cmd.Parameters.AddWithValue("@ans_HN", ans_HN);
            cmd.Parameters.AddWithValue("@DI_HN", DI_HN);
            cmd.Parameters.AddWithValue("@Option_type", Option_type);
            cmd.Parameters.AddWithValue("@Option_Itype", Option_Itype);
            cmd.Parameters.AddWithValue("@Direction_id", Direction_id);
            cmd.Parameters.AddWithValue("@Phrase_id", Phrase_id);
            cmd.Parameters.AddWithValue("@Opetion_id", Opetion_id);
            if (payments.InsertUpdateData(cmd, con))
            {

            }
        }
        private void Update_question_answer_Master(SqlConnection con)
        {

            string query1 = "Select  * from  Tamp_question_answer_Master  where test_code='" + hd_test_id.Value + "' and quest_code='" + hd_test_id.Value + "'  and Section='" + hd_section_id.Value + "' order by id asc";
            SqlDataAdapter ad = new SqlDataAdapter(query1, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Tamp_question_info");
            DataTable dt = ds.Tables[0];
            int rowcount = dt.Rows.Count;
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                query1 = "Delete from question_answer_Master where test_code='" + hd_test_id.Value + "' and quest_code='" + hd_test_id.Value + "'  and Section='" + hd_section_id.Value + "'";
                payments.exeSql(query1, con);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string quest_code = dt.Rows[i]["quest_code"].ToString();
                    string opt_code = dt.Rows[i]["opt_code"].ToString();
                    string option_text = dt.Rows[i]["option_text"].ToString();
                    string opetion_text_HN = dt.Rows[i]["opetion_text_HN"].ToString();
                    string Option_type = dt.Rows[i]["Option_type"].ToString();
                    string Option_Itype = dt.Rows[i]["Option_Itype"].ToString();
                    string entry_id = dt.Rows[i]["entry_id"].ToString();
                    string Direction_id = dt.Rows[i]["Direction_id"].ToString();
                    string Phrase_id = dt.Rows[i]["Phrase_id"].ToString();
                    insert_into_question_answer_Master(quest_code, opt_code, option_text, opetion_text_HN, Option_type, Option_Itype, entry_id, Direction_id, Phrase_id, con);


                }
            }
        }
        private void insert_into_question_answer_Master(string quest_code, string opt_code, string option_text, string opetion_text_HN, string Option_type, string Option_Itype, string entry_id, string Direction_id, string Phrase_id, SqlConnection con)
        {

            string query = " INSERT INTO  question_answer_Master (Section,test_code,quest_code,opt_code,option_text,created_date,created_idate,created_by,updated_date,Updateidate,opetion_text_HN,Option_type,Option_Itype,entry_id,Direction_id,Phrase_id) values (@Section,@test_code,@quest_code,@opt_code,@option_text,@created_date,@created_idate,@created_by,@updated_date,@Updateidate,@opetion_text_HN,@Option_type,@Option_Itype,@entry_id,@Direction_id,@Phrase_id)";
            SqlCommand cmd;
            cmd = new SqlCommand(query);

            cmd.Parameters.AddWithValue("@Section", hd_section_id.Value);
            cmd.Parameters.AddWithValue("@test_code", hd_test_id.Value);
            cmd.Parameters.AddWithValue("@quest_code", quest_code);
            cmd.Parameters.AddWithValue("@opt_code", opt_code);
            cmd.Parameters.AddWithValue("@option_text", option_text);
            cmd.Parameters.AddWithValue("@created_date", mycode.date());
            cmd.Parameters.AddWithValue("@created_idate", mycode.idate());
            cmd.Parameters.AddWithValue("@created_by", hd_userid.Value);
            cmd.Parameters.AddWithValue("@updated_by", hd_userid.Value);
            cmd.Parameters.AddWithValue("@updated_date", mycode.date());
            cmd.Parameters.AddWithValue("@Updateidate", mycode.idate());
            cmd.Parameters.AddWithValue("@opetion_text_HN", opetion_text_HN);
            cmd.Parameters.AddWithValue("@Option_type", Option_type);
            cmd.Parameters.AddWithValue("@Option_Itype", Option_Itype);
            cmd.Parameters.AddWithValue("@entry_id", entry_id);
            cmd.Parameters.AddWithValue("@Direction_id", Direction_id);
            cmd.Parameters.AddWithValue("@Phrase_id", Phrase_id);
            if (payments.InsertUpdateData(cmd, con))
            {
            }
        }
        private void update_Question_Explanation(SqlConnection con)
        {
            string query1 = "Select top 1 Explanation_en,Explanation_hn from  Tamp_Question_Explanation  where test_id='" + hd_test_id.Value + "' and questionid='" + hd_question_id.Value + "'  and Section='" + hd_section_id.Value + "'";
            SqlDataAdapter ad = new SqlDataAdapter(query1, con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Tamp_question_info");
            DataTable dt = ds.Tables[0];
            int rowcount = dt.Rows.Count;
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                string query = " Update Question_Explanation set Explanation_en=@Explanation_en,Explanation_hn=@Explanation_hn     where test_id='" + hd_test_id.Value + "' and questionid='" + hd_question_id.Value + "' and Section='" + hd_section_id.Value + "'";
                SqlCommand cmd;
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Explanation_en", dt.Rows[0]["Explanation_en"].ToString());
                cmd.Parameters.AddWithValue("@Explanation_hn", dt.Rows[0]["Explanation_hn"].ToString());

                if (payments.InsertUpdateData(cmd, con))
                {
                }
            }
        }
        #endregion
        #endregion


    }
}