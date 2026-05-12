using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class hostel_student_list : System.Web.UI.Page
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
                        if (Session["MsgeS"] != null)
                        {
                            Alertme(Session["MsgeS"].ToString(), "success");
                            Session["MsgeS"] = null;
                        }

                        string pagename_current = "student-report-home.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        find_firm_details();
                        if (ViewState["Is_Print"].ToString() == "1")
                        {
                            print1.Visible = true;
                        }
                        else
                        {
                            print1.Visible = false;
                        }




                        ViewState["flag"] = "0";
                        find_firm_details();
                        bind_session();
                        ddlsession.SelectedValue = My.get_session_id();
                        bind_class();
                        ddlclass.SelectedValue = My.get_top_one_class();
                        fetch_section();

                        //bind_all_data();
                        //bind_data(); 


                        bind_student_data();

                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "StudentList");
            }
        }

        private void bind_student_data()
        {
            if (ddlclass.SelectedItem.Text == "ALL")
            {
                if (ddl_studenttype.Text == "ALL")
                {
                    bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "'  and hosteltaken='Yes' and Status='1'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  order by ct.Position,rollnumber asc");
                }
                else
                {
                    bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "'  and hosteltaken='Yes' and Status='1' and Transfer_Status='" + ddl_studenttype.SelectedValue + "' order by ct.Position,rollnumber asc");
                }
            }
            else
            {
                if (ddl_section.Text == "ALL" && ddl_studenttype.Text == "ALL")
                {
                    bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "'  and hosteltaken='Yes' and Class_id='" + ddlclass.SelectedValue + "' and Status='1'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  order by ct.Position,rollnumber asc");
                }
                else if (ddl_section.Text != "ALL" && ddl_studenttype.Text == "ALL")
                {
                    bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "'  and hosteltaken='Yes' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'  and Status='1' and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  order by ct.Position,rollnumber asc");
                }
                else if (ddl_section.Text != "ALL" && ddl_studenttype.Text != "ALL")
                {
                    bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "' and hosteltaken='Yes' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and Status='1'  and Transfer_Status ='" + ddl_studenttype.SelectedValue + "'   order by ct.Position,rollnumber asc");
                }
                else if (ddl_section.Text == "ALL" && ddl_studenttype.Text != "ALL")
                {
                    bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "' and hosteltaken='Yes' and Class_id='" + ddlclass.SelectedValue + "'    and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1'  order by ct.Position,rollnumber asc");
                }
            }
        }


        protected void btn_find_Click(object sender, EventArgs e)
        {
            lbl_class22.Text = "";
            try
            {
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session.", "warning");
                    ddlsession.Focus();
                }
                else
                {
                    lbl_class22.Text = "Session : " + ddlsession.SelectedItem.Text + " Class : " + ddlclass.SelectedItem.Text + " Section : " + ddl_section.Text + " Student Type : " + ddl_studenttype.SelectedItem.Text;
                    bind_student_data();
                }
            }
            catch (Exception ex)
            {
            }
        }


        protected void ddlclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                fetch_section();
            }
            catch (Exception ex)
            {
            }
        }
        private void bind_session()
        {
            mycode.bind_all_ddl_with_id(ddlsession, "Select Session,session_id from session_details");
        }
        private void bind_class()
        {
            mycode.bind_all_ddl_with_id_cap_All(ddlclass, "Select Course_Name,course_id,Position from Add_course_table order by Position");
        }
        private void fetch_section()
        {
            mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' order by Section");
        }

        My mycode = new My();
        private void find_firm_details()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                imglogo.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();
            }
        }


        private void bind_grd_view(string qry)
        {
            lbl_class22.Text = "";
            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {
                rd_view.DataSource = null;
                rd_view.DataBind();
                btn_excels.Visible = false;
            }
            else
            {
                lbl_class22.Text = "Session :" + ddlsession.SelectedItem.Text + " Class :" + ddlclass.SelectedItem.Text + " Section :" + ddl_section.Text + " Student Type :" + ddl_studenttype.SelectedItem.Text;
                rd_view.DataSource = dt;
                rd_view.DataBind();
                btn_excels.Visible = true;
                if (ViewState["Is_Print"].ToString() == "1")
                {
                    print1.Visible = true;
                }
                else
                {
                    print1.Visible = false;
                }
            }
        }


        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                Label lbl_admissionserialnumber = (Label)row.FindControl("lbl_admissionserialnumber");
                Response.Redirect("admission.aspx?stdid=" + lbl_Id.Text + "&admno=" + lbl_admissionserialnumber.Text + "&from=HSedt", false);
            }
            catch
            {
            }
        }


        protected void lnk_upload_image_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                Label lbl_admissionserialnumber = (Label)row.FindControl("lbl_admissionserialnumber");
                Label lbl_session_id = (Label)row.FindControl("lbl_session_id");
                ViewState["DocAdmNo"] = lbl_admissionserialnumber.Text;
                ViewState["DocSessionId"] = lbl_session_id.Text;
                bind_doc_type();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDocs();", true);
            }
            catch
            {

            }
        }

        private void bind_doc_type()
        {
            DataTable dt = mycode.FillData("select * from Upload_document_type order by Position asc");
            if (dt.Rows.Count == 0)
            {
                rd_view_docs.DataSource = null;
                rd_view_docs.DataBind();
            }
            else
            {
                rd_view_docs.DataSource = dt;
                rd_view_docs.DataBind();
            }
        }

        private void fetch_saved_images(HtmlImage Image1, Label lbl_column_name)
        {
            DataTable dt = mycode.FillData("select Image_path from Student_image_new where Admission_no='" + ViewState["DocAdmNo"].ToString() + "' and Image_Type='" + lbl_column_name.Text + "' and Session_id='" + ViewState["DocSessionId"].ToString() + "'");
            if (dt.Rows.Count > 0)
            {
                Image1.Src = dt.Rows[0]["Image_path"].ToString();
            }
        }

        protected void rd_view_docs_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlImage Image1 = e.Item.FindControl("stdimages") as HtmlImage;
                Label lbl_column_name = ((Label)e.Item.FindControl("lbl_column_name")) as Label;
                fetch_saved_images(Image1, lbl_column_name);
            }
        }



        #region ImageSave
        protected void btn_upload_image_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Name = (Label)row.FindControl("lbl_Name");
                Label lbl_column_name = (Label)row.FindControl("lbl_column_name");
                FileUpload FileUpload1 = (FileUpload)row.FindControl("FileUpload1");
                save_image(FileUpload1, lbl_Name, lbl_column_name);
                bind_doc_type();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDocs();", true);
            }
            catch (Exception ex)
            {
            }
        }


        My mycodeMy = new My();
        private void save_image(FileUpload FileUpload1, Label lbl_Name, Label lbl_column_name)
        {
            if (FileUpload1.HasFile)
            {
                if (FileUpload1.FileBytes.Length < 500000)
                {
                    string files_path = upload_imag(FileUpload1, lbl_column_name.Text);
                    if (files_path == "")
                    {
                    }
                    else
                    {
                        if (mycodeMy.IsUserExist("select Id from Student_image_new where Admission_no='" + ViewState["DocAdmNo"].ToString() + "' and Image_type='" + lbl_column_name.Text + "' and Session_id='" + ViewState["DocSessionId"].ToString() + "'"))
                        {
                            SqlCommand cmd;
                            string query = "INSERT INTO Student_image_new (Admission_no,Image_name,Image_type,Image_path,Session_id) values (@Admission_no,@Image_name,@Image_type,@Image_path,@Session_id)";
                            cmd = new SqlCommand(query);
                            cmd.Parameters.AddWithValue("@Admission_no", ViewState["DocAdmNo"].ToString());
                            cmd.Parameters.AddWithValue("@Image_name", lbl_Name.Text);
                            cmd.Parameters.AddWithValue("@Image_type", lbl_column_name.Text);
                            cmd.Parameters.AddWithValue("@Image_path", files_path);
                            cmd.Parameters.AddWithValue("@Session_id", ViewState["DocSessionId"].ToString());
                            if (My.InsertUpdateData(cmd))
                            {
                            }
                        }
                        else
                        {
                            SqlCommand cmd;
                            string query = "Update Student_image_new set Image_path=@Image_path where Admission_no='" + ViewState["DocAdmNo"].ToString() + "' and Image_type='" + lbl_column_name.Text + "' and Session_id='" + ViewState["DocSessionId"].ToString() + "'";
                            cmd = new SqlCommand(query);
                            cmd.Parameters.AddWithValue("@Image_path", files_path);
                            if (My.InsertUpdateData(cmd))
                            {
                            }
                        }


                        if (lbl_column_name.Text == "Student_image")
                        {
                            mycode.executequery("update admission_registor set studentimagepath='" + files_path + "' where admissionserialnumber='" + ViewState["DocAdmNo"].ToString() + "' and Session_id='" + ViewState["DocSessionId"].ToString() + "'");
                        }
                        if (lbl_column_name.Text == "Parent_Sign")
                        {
                            mycode.executequery("update admission_registor set signatureimagepath='" + files_path + "' where admissionserialnumber='" + ViewState["DocAdmNo"].ToString() + "' and Session_id='" + ViewState["DocSessionId"].ToString() + "'");
                        }
                        if (lbl_column_name.Text == "DOB_image")
                        {
                            mycode.executequery("update admission_registor set dobproof='" + files_path + "' where admissionserialnumber='" + ViewState["DocAdmNo"].ToString() + "' and Session_id='" + ViewState["DocSessionId"].ToString() + "'");
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDocs();", true);
                    Alertme("Please Reduce or compress size of  " + lbl_Name.Text + " max(200kb).", "warning");
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDocs();", true);
                Alertme("Please upload " + lbl_Name.Text, "warning");
            }
        }

        private string upload_imag(FileUpload file, string column_name)
        {
            string dbfilePath = "";
            DateTime dt = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string date = dt.ToString("dd_MM_yyyy");
            string time = dt.ToString("hh_mm_ss");
            String filerename = date + time;
            Boolean FileOK = false;
            Boolean FileSaved = false;
            int k = 0;
            Session["WorkingImage"] = file.FileName;
            string FileExtension = Path.GetExtension(Session["WorkingImage"].ToString()).ToLower();
            Session["renamedfile"] = filerename + "PIMG1" + FileExtension;
            string[] allowedExtension = { ".png", ".PNG", ".jpg", ".JPG", ".JPEG", ".jpeg" };
            for (int i = 0; i < allowedExtension.Length; i++)
            {
                k++;
                if (FileExtension == allowedExtension[i])
                {
                    FileOK = true;
                    break;
                }
            }

            if (FileOK)
            {
                try
                {
                    string path = (Server.MapPath("../Master_Img/Student")).ToString();
                    file.SaveAs(path + "/" + Session["renamedfile"]);
                    FileSaved = true;
                }
                catch (Exception exe)
                {
                    FileSaved = false;
                    Alertme("File has not save.", "warning");
                }
            }
            else
            {

            }
            if (FileSaved)
            {
                String originalPath2 = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, "");
                string[] New_originalPath1 = originalPath2.Split('?');
                string originalPath1 = New_originalPath1[0].ToString();

                string fileName = Path.GetFileName(Session["renamedfile"].ToString());
                dbfilePath = originalPath1 + "/Master_Img/Student/" + fileName;
            }
            return dbfilePath;
        }

        #endregion
        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (ViewState["Is_Print"].ToString() == "1")
                {
                    ((Panel)e.Item.FindControl("pnl_print_student")).Visible = true;
                    ((Panel)e.Item.FindControl("pnl_print_student_ladger")).Visible = true;
                }
                else
                {
                    ((Panel)e.Item.FindControl("pnl_print_student")).Visible = false;
                    ((Panel)e.Item.FindControl("pnl_print_student_ladger")).Visible = false;
                }

                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    ((LinkButton)e.Item.FindControl("lnkEdit")).Visible = true;
                }
                else
                {
                    ((LinkButton)e.Item.FindControl("lnkEdit")).Visible = true;
                }


                ////==========================================////
                if (((Label)e.Item.FindControl("lbl_pay_status")).Text.ToUpper() == "PAID")
                {
                    ((Label)e.Item.FindControl("lbl_pament")).Text = "Paid";
                }
                else
                {
                    ((Label)e.Item.FindControl("lbl_pament")).Text = "Dues";
                }
            }
        }

        #region ExcelDownloaD
        protected void btn_excels_Click(object sender, EventArgs e)
        {
            if (ViewState["Is_Download"].ToString() == "1")
            {
                try
                {
                    string house = "Select top 1 house_name from house_master where house_id=admission_registor.house";
                    if (ddlclass.SelectedItem.Text.ToUpper() == "ALL" && ddl_section.Text == "ALL" && ddl_studenttype.Text == "ALL")
                    {
                        string query = "select dateofadmission as Admission_Date_Current_Session,Old_Admission_Date as Admission_Date,admissionserialnumber as Admission_no,session,class,Section,rollnumber,Hostel_roll_no,Academic_Sem_or_Year_id,studentname,gender,dob as Date_of_birth,fathername as Father_name,fatherqualification as Father_qualification,mothername as Mother_name,motherqualifiaction as Mother_qualifiaction,identifacationmark as Identifacation_Mark,currentschool as Current_School,guardianname as Guardian_name,relation,occuption,religion,cast,parentincome,careof,city,postoffice,policestation,district,pin,mobilenumber as Mobile_no,careof_permanent,city_permanent,postoffice_permanent,policestation_permanent,district_permanent,pincode,payment_status,hosteltaken,transportationtaken,Transfer_Status,Busno,aadharno,RTE,(" + house + ") as housename,Pre_vious_rollnumber,Pre_vious_Section,roll_used,staff_ward,mob2,email_id,birth_certificate_number,place_of_birth,blood_group,cast_certificate_no,student_mother_tounge,is_illness,f_nationality as Father_nationality,f_marrital_statue as father_marrital_status,m_marrital_statue as mother_marrital_statue,m_nationality as mother_nationality,m_occupation as mother_occupation,ration_type,illness_remark,father_mob,mother_email,Account_Holder_name,Bnk_Name,IFSC_Code,Branch_Name,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status,Pwd as Password from admission_registor join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "'  and hosteltaken='Yes' and (Transfer_Status='New' or Transfer_Status='NT' or  Transfer_Status='Transferred') and Status='1'  order by ct.Position,Section,rollnumber asc";
                        DataTable dt = mycode.FillData(query);
                        export_to_excel(dt, "Hostel_Student_List");
                    }
                    if (ddl_section.Text == "ALL" && ddl_studenttype.Text == "ALL")
                    {
                        string query = "select dateofadmission as Admission_Date_Current_Session,Old_Admission_Date as Admission_Date,admissionserialnumber as Admission_no,session,class,Section,rollnumber,Hostel_roll_no,Academic_Sem_or_Year_id,studentname,gender,dob as Date_of_birth,fathername as Father_name,fatherqualification as Father_qualification,mothername as Mother_name,motherqualifiaction as Mother_qualifiaction,identifacationmark as Identifacation_Mark,currentschool as Current_School,guardianname as Guardian_name,relation,occuption,religion,cast,parentincome,careof,city,postoffice,policestation,district,pin,mobilenumber as Mobile_no,careof_permanent,city_permanent,postoffice_permanent,policestation_permanent,district_permanent,pincode,payment_status,hosteltaken,transportationtaken,Transfer_Status,Busno,aadharno,RTE,(" + house + ") as housename,Pre_vious_rollnumber,Pre_vious_Section,roll_used,staff_ward,mob2,email_id,birth_certificate_number,place_of_birth,blood_group,cast_certificate_no,student_mother_tounge,is_illness,f_nationality as Father_nationality,f_marrital_statue as father_marrital_status,m_marrital_statue as mother_marrital_statue,m_nationality as mother_nationality,m_occupation as mother_occupation,ration_type,illness_remark,father_mob,mother_email,Account_Holder_name,Bnk_Name,IFSC_Code,Branch_Name,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status,Pwd as Password from admission_registor join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "'  and hosteltaken='Yes' and Class_id='" + ddlclass.SelectedValue + "'  and (Transfer_Status='New' or Transfer_Status='NT' or  Transfer_Status='Transferred') and Status='1'  order by ct.Position,Section,rollnumber asc";
                        DataTable dt = mycode.FillData(query);
                        export_to_excel(dt, "Hostel_Student_List");
                    }
                    else if (ddl_section.Text != "ALL" && ddl_studenttype.Text == "ALL")
                    {
                        string query = "select dateofadmission as Admission_Date_Current_Session,Old_Admission_Date as Admission_Date,admissionserialnumber as Admission_no,session,class,Section,rollnumber,Hostel_roll_no,Academic_Sem_or_Year_id,studentname,gender,dob as Date_of_birth,fathername as Father_name,fatherqualification as Father_qualification,mothername as Mother_name,motherqualifiaction as Mother_qualifiaction,identifacationmark as Identifacation_Mark,currentschool as Current_School,guardianname as Guardian_name,relation,occuption,religion,cast,parentincome,careof,city,postoffice,policestation,district,pin,mobilenumber as Mobile_no,careof_permanent,city_permanent,postoffice_permanent,policestation_permanent,district_permanent,pincode,payment_status,hosteltaken,transportationtaken,Transfer_Status,Busno,aadharno,RTE,(" + house + ") as housename,Pre_vious_rollnumber,Pre_vious_Section,roll_used,staff_ward,mob2,email_id,birth_certificate_number,place_of_birth,blood_group,cast_certificate_no,student_mother_tounge,is_illness,f_nationality as Father_nationality,f_marrital_statue as father_marrital_status,m_marrital_statue as mother_marrital_statue,m_nationality as mother_nationality,m_occupation as mother_occupation,ration_type,illness_remark,father_mob,mother_email,Account_Holder_name,Bnk_Name,IFSC_Code,Branch_Name,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status,Pwd as Password from admission_registor join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "'  and hosteltaken='Yes' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and Status='1' and (Transfer_Status='New' or Transfer_Status='NT' or  Transfer_Status='Transferred')   order by ct.Position,Section,rollnumber asc";
                        DataTable dt = mycode.FillData(query);
                        export_to_excel(dt, "Hostel_Student_List"); 
                    }
                    else if (ddl_section.Text != "ALL" && ddl_studenttype.Text != "ALL")
                    {
                        string query = "select dateofadmission as Admission_Date_Current_Session,Old_Admission_Date as Admission_Date,admissionserialnumber as Admission_no,session,class,Section,rollnumber,Hostel_roll_no,Academic_Sem_or_Year_id,studentname,gender,dob as Date_of_birth,fathername as Father_name,fatherqualification as Father_qualification,mothername as Mother_name,motherqualifiaction as Mother_qualifiaction,identifacationmark as Identifacation_Mark,currentschool as Current_School,guardianname as Guardian_name,relation,occuption,religion,cast,parentincome,careof,city,postoffice,policestation,district,pin,mobilenumber as Mobile_no,careof_permanent,city_permanent,postoffice_permanent,policestation_permanent,district_permanent,pincode,payment_status,hosteltaken,transportationtaken,Transfer_Status,Busno,aadharno,RTE,(" + house + ") as housename,Pre_vious_rollnumber,Pre_vious_Section,roll_used,staff_ward,mob2,email_id,birth_certificate_number,place_of_birth,blood_group,cast_certificate_no,student_mother_tounge,is_illness,f_nationality as Father_nationality,f_marrital_statue as father_marrital_status,m_marrital_statue as mother_marrital_statue,m_nationality as mother_nationality,m_occupation as mother_occupation,ration_type,illness_remark,father_mob,mother_email,Account_Holder_name,Bnk_Name,IFSC_Code,Branch_Name,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status,Pwd as Password from admission_registor join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "'  and hosteltaken='Yes' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and Status='1' and    Transfer_Status ='" + ddl_studenttype.SelectedValue + "'  order by ct.Position,Section,rollnumber asc";
                        DataTable dt = mycode.FillData(query);
                        export_to_excel(dt, "Hostel_Student_List");
                    }
                    else if (ddlclass.SelectedItem.Text.ToUpper() == "ALL" && ddl_section.Text == "ALL" && ddl_studenttype.Text != "ALL")
                    {
                        string query = "select dateofadmission as Admission_Date_Current_Session,Old_Admission_Date as Admission_Date,admissionserialnumber as Admission_no,session,class,Section,rollnumber,Hostel_roll_no,Academic_Sem_or_Year_id,studentname,gender,dob as Date_of_birth,fathername as Father_name,fatherqualification as Father_qualification,mothername as Mother_name,motherqualifiaction as Mother_qualifiaction,identifacationmark as Identifacation_Mark,currentschool as Current_School,guardianname as Guardian_name,relation,occuption,religion,cast,parentincome,careof,city,postoffice,policestation,district,pin,mobilenumber as Mobile_no,careof_permanent,city_permanent,postoffice_permanent,policestation_permanent,district_permanent,pincode,payment_status,hosteltaken,transportationtaken,Transfer_Status,Busno,aadharno,RTE,(" + house + ") as housename,Pre_vious_rollnumber,Pre_vious_Section,roll_used,staff_ward,mob2,email_id,birth_certificate_number,place_of_birth,blood_group,cast_certificate_no,student_mother_tounge,is_illness,f_nationality as Father_nationality,f_marrital_statue as father_marrital_status,m_marrital_statue as mother_marrital_statue,m_nationality as mother_nationality,m_occupation as mother_occupation,ration_type,illness_remark,father_mob,mother_email,Account_Holder_name,Bnk_Name,IFSC_Code,Branch_Name,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status,Pwd as Password from admission_registor join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "'  and hosteltaken='Yes' and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1'  order by ct.Position,rollnumber asc";
                        DataTable dt = mycode.FillData(query);
                        export_to_excel(dt, "Hostel_Student_List");
                    }
                    else if (ddlclass.SelectedItem.Text.ToUpper() != "ALL" && ddl_section.Text == "ALL" && ddl_studenttype.Text != "ALL")
                    {
                        string query = "select dateofadmission as Admission_Date_Current_Session,Old_Admission_Date as Admission_Date,admissionserialnumber as Admission_no,session,class,Section,rollnumber,Hostel_roll_no,Academic_Sem_or_Year_id,studentname,gender,dob as Date_of_birth,fathername as Father_name,fatherqualification as Father_qualification,mothername as Mother_name,motherqualifiaction as Mother_qualifiaction,identifacationmark as Identifacation_Mark,currentschool as Current_School,guardianname as Guardian_name,relation,occuption,religion,cast,parentincome,careof,city,postoffice,policestation,district,pin,mobilenumber as Mobile_no,careof_permanent,city_permanent,postoffice_permanent,policestation_permanent,district_permanent,pincode,payment_status,hosteltaken,transportationtaken,Transfer_Status,Busno,aadharno,RTE,(" + house + ") as housename,Pre_vious_rollnumber,Pre_vious_Section,roll_used,staff_ward,mob2,email_id,birth_certificate_number,place_of_birth,blood_group,cast_certificate_no,student_mother_tounge,is_illness,f_nationality as Father_nationality,f_marrital_statue as father_marrital_status,m_marrital_statue as mother_marrital_statue,m_nationality as mother_nationality,m_occupation as mother_occupation,ration_type,illness_remark,father_mob,mother_email,Account_Holder_name,Bnk_Name,IFSC_Code,Branch_Name,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status,Pwd as Password from admission_registor join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "'  and hosteltaken='Yes' and Class_id='" + ddlclass.SelectedValue + "'   and    Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1'  order by ct.Position,Section,rollnumber asc";
                        DataTable dt = mycode.FillData(query);
                        export_to_excel(dt, "Hostel_Student_List");
                    }
                }
                catch (Exception ex)
                {
                }
            }
            else
            {
                Alertme(My.get_restricted_message(), "warning");
            }
        }

        private void export_to_excel(DataTable dt, string file)
        {
            string FileName = file + DateTime.Now + ".xls";
            string attachment = "attachment; filename=" + FileName;
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            Response.ContentEncoding = System.Text.Encoding.Unicode;
            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
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
        }
        #endregion
    }
}