using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Library_Admin
{
    public partial class Generate_Library_Card : System.Web.UI.Page
    {
        Library lb = new Library();
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
                        Session["pagelibstu"] = "1";
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = My.get_firm_id();
                        ViewState["Branch_id"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        ViewState["flag"] = "0";

                        mycode.bind_all_ddl_with_id(ddlsession, "Select  Session,session_id from session_details ");
                        ddlsession.SelectedValue = My.get_session_id();

                        mycode.bind_ddl(ddl_section, "Select distinct Section from admission_registor");
                        ddl_section.Text = My.get_top_one_section();


                        mycode.bind_all_ddl_with_id(ddlclass, "Select Course_Name,course_id,Position from Add_course_table order by Position");
                        ddlclass.SelectedValue = My.get_top_one_class();



                        find_by_c_s_a();

                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "StudentList");
            }

        }
        private void bind_all_data()
        {
            bind_grd_view("select Session_id,session,admissionserialnumber,class,Section,rollnumber,studentname,lib_card_no,lib_card_Issuedate,lib_card_IssueIdate,lib_card_Valid_up_to_Date,lib_card_Valid_up_to_IDate,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name,Lib_Bar_code_img,Lib_Bar_code from admission_registor where (Transfer_Status='New' or Transfer_Status='NT') and StudentStatus='AV' and Session_id='" + ddlsession.SelectedValue + "' and  Branch_id = '" + ViewState["Branch_id"].ToString() + "' and Status='1' order by id desc");


        }
        protected void ddlclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                find_by_class();
                ViewState["flag"] = "3";
            }
            catch (Exception ex)
            {
            }
        }

        private void find_by_class()
        {
            bind_grd_view("select Session_id,session,admissionserialnumber,class,Section,rollnumber,studentname,lib_card_no,lib_card_Issuedate,lib_card_IssueIdate,lib_card_Valid_up_to_Date,lib_card_Valid_up_to_IDate,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name,Lib_Bar_code_img,Lib_Bar_code from admission_registor where Session_id='" + ddlsession.SelectedValue + "'  and Class_id='" + ddlclass.SelectedValue + "' and (Transfer_Status='New' or Transfer_Status='NT') and StudentStatus='AV' and Branch_id = '" + ViewState["Branch_id"].ToString() + "' and  Status='1'  order by id desc");
        }





        #region CountDataA

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
        #endregion

        My mycode = new My();
        private void bind_grd_view(string qry)
        {
            ViewState["query"] = qry;
            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {
                finalsubmitpnl.Visible = false;
                Alertme("Sorry there are no data list exist", "warning");
                GrdView.DataSource = null;
                GrdView.DataBind();
            }
            else
            {
                finalsubmitpnl.Visible = true;
                GrdView.DataSource = dt;
                GrdView.DataBind();
            }
        }



        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session.", "warning");
                    ddlsession.Focus();
                }
                else if (ddlclass.SelectedItem.Text == "Select")
                {
                    Alertme("Please select class.", "warning");
                    ddlclass.Focus();
                }
                else if (ddl_section.SelectedItem.Text == "Select")
                {
                    Alertme("Please select section.", "warning");
                    ddl_section.Focus();
                }
                else
                {
                    find_by_c_s_a();
                    ViewState["flag"] = "1";
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_by_c_s_a()
        {
            bind_grd_view("select Session_id,session,admissionserialnumber,class,Section,rollnumber,studentname,lib_card_no,lib_card_Issuedate,lib_card_IssueIdate,lib_card_Valid_up_to_Date,lib_card_Valid_up_to_IDate,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name,Lib_Bar_code_img,Lib_Bar_code from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  and  Section='" + ddl_section.SelectedItem.Text + "' and (Transfer_Status='New' or Transfer_Status='NT') and StudentStatus='AV'and Branch_id = '" + ViewState["Branch_id"].ToString() + "' and Status='1'  order by rollnumber asc");
        }

        protected void ddlsession_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session.", "warning");
                    ddlsession.Focus();
                }
                else
                {
                    mycode.bind_all_ddl_with_id(ddlclass, "  Select  Course_Name, course_id  from Add_course_table order by  Position");
                    find_by_session();
                    ViewState["flag"] = "2";
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_by_session()
        {
            bind_grd_view("select Session_id, session,admissionserialnumber,class,Section,rollnumber,studentname,lib_card_no,lib_card_Issuedate,lib_card_IssueIdate,lib_card_Valid_up_to_Date,lib_card_Valid_up_to_IDate,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name,Lib_Bar_code_img,Lib_Bar_code from admission_registor where (Transfer_Status='New' or Transfer_Status='NT') and StudentStatus='AV' and  Session_id='" + ddlsession.SelectedValue + "' and Branch_id = '" + ViewState["Branch_id"].ToString() + "' and  Status='1' order by id desc");
        }




        #region ExcelDownloaD
        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["flag"].ToString() == "0")
                {
                    DataTable dt = mycode.FillData("select dateofadmission as Admission_Date,admissionserialnumber as Admission_no,session,class,Section,rollnumber,Academic_Sem_or_Year_id,studentname,gender,dob as Date_of_birth,fathername as Father_name,fatherqualification as Father_qualification,mothername as Mother_name,motherqualifiaction as Mother_qualifiaction,identifacationmark as Identifacation_Mark,currentschool as Current_School,guardianname as Guardian_name,relation,occuption,religion,cast,parentincome,careof,city,postoffice,policestation,district,pin,mobilenumber as Mobile_no,careof_permanent,city_permanent,postoffice_permanent,policestation_permanent,district_permanent,pincode,payment_status,hosteltaken,transportationtaken,Transfer_Status,Busno,aadharno,RTE,house,Pre_vious_rollnumber,Pre_vious_Section,roll_used,staff_ward,mob2,email_id,birth_certificate_number,place_of_birth,blood_group,cast_certificate_no,student_mother_tounge,is_illness,f_nationality as Father_nationality,f_marrital_statue as father_marrital_status,m_marrital_statue as mother_marrital_statue,m_nationality as mother_nationality,m_occupation as mother_occupation,ration_type,illness_remark,father_mob,mother_email,Account_Holder_name,Bnk_Name,IFSC_Code,Branch_Name,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status from admission_registor where (Transfer_Status='New' or Transfer_Status='NT'   or Transfer_Status='Transferred') and StudentStatus='AV'order by id desc");
                    export_to_excel(dt, "Student_list");
                }

                if (ViewState["flag"].ToString() == "1")
                {
                    DataTable dt = mycode.FillData("select dateofadmission as Admission_Date,admissionserialnumber as Admission_no,session,class,Section,rollnumber,Academic_Sem_or_Year_id,studentname,gender,dob as Date_of_birth,fathername as Father_name,fatherqualification as Father_qualification,mothername as Mother_name,motherqualifiaction as Mother_qualifiaction,identifacationmark as Identifacation_Mark,currentschool as Current_School,guardianname as Guardian_name,relation,occuption,religion,cast,parentincome,careof,city,postoffice,policestation,district,pin,mobilenumber as Mobile_no,careof_permanent,city_permanent,postoffice_permanent,policestation_permanent,district_permanent,pincode,payment_status,hosteltaken,transportationtaken,Transfer_Status,Busno,aadharno,RTE,house,Pre_vious_rollnumber,Pre_vious_Section,roll_used,staff_ward,mob2,email_id,birth_certificate_number,place_of_birth,blood_group,cast_certificate_no,student_mother_tounge,is_illness,f_nationality as Father_nationality,f_marrital_statue as father_marrital_status,m_marrital_statue as mother_marrital_statue,m_nationality as mother_nationality,m_occupation as mother_occupation,ration_type,illness_remark,father_mob,mother_email,Account_Holder_name,Bnk_Name,IFSC_Code,Branch_Name,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and  Section='" + ddl_section.SelectedItem.Text + "'  and (Transfer_Status='New' or Transfer_Status='NT' or  Transfer_Status='Transferred') and StudentStatus='AV' order by id desc");
                    export_to_excel(dt, "Student_list");
                }
                if (ViewState["flag"].ToString() == "2")
                {
                    DataTable dt = mycode.FillData("select dateofadmission as Admission_Date,admissionserialnumber as Admission_no,session,class,Section,rollnumber,Academic_Sem_or_Year_id,studentname,gender,dob as Date_of_birth,fathername as Father_name,fatherqualification as Father_qualification,mothername as Mother_name,motherqualifiaction as Mother_qualifiaction,identifacationmark as Identifacation_Mark,currentschool as Current_School,guardianname as Guardian_name,relation,occuption,religion,cast,parentincome,careof,city,postoffice,policestation,district,pin,mobilenumber as Mobile_no,careof_permanent,city_permanent,postoffice_permanent,policestation_permanent,district_permanent,pincode,payment_status,hosteltaken,transportationtaken,Transfer_Status,Busno,aadharno,RTE,house,Pre_vious_rollnumber,Pre_vious_Section,roll_used,staff_ward,mob2,email_id,birth_certificate_number,place_of_birth,blood_group,cast_certificate_no,student_mother_tounge,is_illness,f_nationality as Father_nationality,f_marrital_statue as father_marrital_status,m_marrital_statue as mother_marrital_statue,m_nationality as mother_nationality,m_occupation as mother_occupation,ration_type,illness_remark,father_mob,mother_email,Account_Holder_name,Bnk_Name,IFSC_Code,Branch_Name,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and StudentStatus='AV' order by id desc");
                    export_to_excel(dt, "Student_list");
                }
                if (ViewState["flag"].ToString() == "3")
                {
                    DataTable dt = mycode.FillData("select dateofadmission as Admission_Date,admissionserialnumber as Admission_no,session,class,Section,rollnumber,Academic_Sem_or_Year_id,studentname,gender,dob as Date_of_birth,fathername as Father_name,fatherqualification as Father_qualification,mothername as Mother_name,motherqualifiaction as Mother_qualifiaction,identifacationmark as Identifacation_Mark,currentschool as Current_School,guardianname as Guardian_name,relation,occuption,religion,cast,parentincome,careof,city,postoffice,policestation,district,pin,mobilenumber as Mobile_no,careof_permanent,city_permanent,postoffice_permanent,policestation_permanent,district_permanent,pincode,payment_status,hosteltaken,transportationtaken,Transfer_Status,Busno,aadharno,RTE,house,Pre_vious_rollnumber,Pre_vious_Section,roll_used,staff_ward,mob2,email_id,birth_certificate_number,place_of_birth,blood_group,cast_certificate_no,student_mother_tounge,is_illness,f_nationality as Father_nationality,f_marrital_statue as father_marrital_status,m_marrital_statue as mother_marrital_statue,m_nationality as mother_nationality,m_occupation as mother_occupation,ration_type,illness_remark,father_mob,mother_email,Account_Holder_name,Bnk_Name,IFSC_Code,Branch_Name,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and StudentStatus='AV' order by id desc");
                    export_to_excel(dt, "Student_list");
                }

            }
            catch (Exception ex)
            {
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

        protected void Btn_Generate_Click(object sender, EventArgs e)
        {


            string confirmValue = string.Empty;
            confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                if (txt_issuedate.Text == "")
                {
                    txt_issuedate.Focus();
                    Alertme("Please enter issue date ", "warning");
                    return;
                }
                else if (txt_valid_up_to.Text == "")
                {
                    txt_valid_up_to.Focus();
                    Alertme("Please enter valid upto date ", "warning");

                }
                else
                {



                    bool update = false;
                    string sessionname = lb.get_session();
                    string Prefix = lb.get_prefix(ViewState["Branch_id"].ToString(), "student");
                    string Postfix = lb.get_postfix(ViewState["Branch_id"].ToString(), "student");
                    string serialNo = lb.get_serialNo(ViewState["Branch_id"].ToString(), "student");
                    DataTable dt = mycode.FillData(ViewState["query"].ToString());
                    string sdate = txt_issuedate.Text;
                    string sday = sdate.Substring(0, 2);
                    string smonth = sdate.Substring(3, 2);
                    string syear = sdate.Substring(6, 4);
                    string edate = txt_valid_up_to.Text;
                    string eday = edate.Substring(0, 2);
                    string emonth = edate.Substring(3, 2);
                    string eyear = edate.Substring(6, 4);

                    int idate = Convert.ToInt32(syear + smonth + sday);
                    int idate2 = Convert.ToInt32(eyear + emonth + eday);

                    if (idate > idate2)
                    {
                        Alertme("valid upto cannot be less than issue date.", "warning");
                    }
                    else
                    {
                        if (dt.Rows.Count == 0)
                        {

                        }
                        else
                        {
                            int k = 0;
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                CheckBox chk = (CheckBox)GrdView.Rows[i].FindControl("rowChkBox");
                                if (chk.Checked == true)
                                {
                                    if ((dt.Rows[i]["lib_card_no"].ToString()) == "")
                                    {
                                        update = true;
                                        string lib_card_no = Library.lib_card_format(sessionname, Prefix, Postfix, serialNo, "lib_card_no", ViewState["Branch_id"].ToString()).ToString();
                                        string barcode = lb.barcode_num("IssueLib_stu", ViewState["Branch_id"].ToString()); //ViewState["Branch_id"].ToString()
                                        string barcode_image = lb.get_barcode_img_issuebook(barcode, lib_card_no, dt.Rows[i]["admissionserialnumber"].ToString());


                                        SqlCommand cmd;
                                        string query = "Update admission_registor set Lib_Bar_code=@Lib_Bar_code,Lib_Bar_code_img=@Lib_Bar_code_img,lib_card_Issuedate=@lib_card_Issuedate,lib_card_IssueIdate=@lib_card_IssueIdate,lib_card_Valid_up_to_Date=@lib_card_Valid_up_to_Date,lib_card_Valid_up_to_IDate=@lib_card_Valid_up_to_IDate,lib_card_no=@lib_card_no where   admissionserialnumber=@admissionserialnumber and Session_id=@Session_id and Branch_id=@Branch_id ";
                                        cmd = new SqlCommand(query);
                                        cmd.Parameters.AddWithValue("@Lib_Bar_code", barcode);
                                        cmd.Parameters.AddWithValue("@Lib_Bar_code_img", barcode_image);
                                        cmd.Parameters.AddWithValue("@lib_card_Issuedate", sdate);
                                        cmd.Parameters.AddWithValue("@lib_card_IssueIdate", idate);
                                        cmd.Parameters.AddWithValue("@lib_card_Valid_up_to_Date", edate);
                                        cmd.Parameters.AddWithValue("@lib_card_Valid_up_to_IDate", idate2);
                                        cmd.Parameters.AddWithValue("@lib_card_no", lib_card_no);

                                        cmd.Parameters.AddWithValue("@admissionserialnumber", dt.Rows[i]["admissionserialnumber"].ToString());
                                        cmd.Parameters.AddWithValue("@Session_id", dt.Rows[i]["Session_id"].ToString());
                                        cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branch_id"].ToString());
                                        if (My.InsertUpdateData(cmd))
                                        {

                                        }



                                    }
                                    else
                                    {
                                      

                                        update = true;

                                        SqlCommand cmd;
                                        string query = "Update admission_registor set lib_card_Issuedate=@lib_card_Issuedate,lib_card_IssueIdate=@lib_card_IssueIdate,lib_card_Valid_up_to_Date=@lib_card_Valid_up_to_Date,lib_card_Valid_up_to_IDate=@lib_card_Valid_up_to_IDate where   admissionserialnumber=@admissionserialnumber and Session_id=@Session_id and Branch_id=@Branch_id ";
                                        cmd = new SqlCommand(query);

                                        cmd.Parameters.AddWithValue("@lib_card_Issuedate", sdate);
                                        cmd.Parameters.AddWithValue("@lib_card_IssueIdate", idate);
                                        cmd.Parameters.AddWithValue("@lib_card_Valid_up_to_Date", edate);
                                        cmd.Parameters.AddWithValue("@lib_card_Valid_up_to_IDate", idate2);


                                        cmd.Parameters.AddWithValue("@admissionserialnumber", dt.Rows[i]["admissionserialnumber"].ToString());
                                        cmd.Parameters.AddWithValue("@Session_id", dt.Rows[i]["Session_id"].ToString());
                                        cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branch_id"].ToString());
                                        if (My.InsertUpdateData(cmd))
                                        {

                                        }
                                    }
                                }
                                else
                                {
                                    k++;
                                }
                            }
                            if(dt.Rows.Count == k)
                            {
                                Alertme("Please select student first ", "warning");
                            }
                        }
                        if (update == true)
                        {
                            Alertme("Successful student library number generated", "success");
                            bind_grd_view(ViewState["query"].ToString());


                        }
                    }

                }

            }
        }



    }

}
