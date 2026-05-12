using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using school_web.AppCode;
using System.IO;
using System.Transactions;

namespace school_web.Admin
{
    public partial class Admission_Transformation : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
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
                        ViewState["firm_id"] = Session["firm"].ToString();
                        ViewState["flagPosition"] = "1";
                        txt_date.Text = mycode.date();
                        ViewState["flag"] = "0";
                        ViewState["branch_id"] = "1";
                        //mycode.bind_all_ddl_with_id(ddl_session, " Select Session,session_id from session_details where  (Use_mode is null or Use_mode='0')  order by cast((Substring (Session,1,4)) as int) ");

                        //mycode.bind_all_ddl_with_id(ddl_session_adm, "Select Session,session_id from session_details where (Use_mode is null or Use_mode='0') order by cast((Substring (Session,1,4)) as int) ");

                        mycode.bind_all_ddl_with_id(ddl_session, " Select Session,session_id from session_details order by cast((Substring (Session,1,4)) as int) ");

                        mycode.bind_all_ddl_with_id(ddl_session_adm, "Select Session,session_id from session_details order by cast((Substring (Session,1,4)) as int) ");

                        mycode.bind_all_ddl_with_id(ddl_session_tranfser2, "Select Session,session_id from session_details where session_id!='" + ddl_session.SelectedValue + "'order by cast((Substring (Session,1,4)) as int)");


                        mycode.bind_all_ddl_with_id(ddl_course, "Select Course_Name,course_id from Add_course_table order by Position asc");
                        mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor order by Section ");
                        mycode.bind_all_ddl_with_id(ddl_course_transfer2, "Select Course_Name,course_id from Add_course_table order by Position asc");

                        mycode.bind_ddl(ddl_section_transfer_2, "select Section from section_master order by Section asc"); 
                        string pagename_current = Path.GetFileName(Request.Path);
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Fee_Type_Master");
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
        protected void ddl_course_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_course.SelectedItem.Text == "Select")
            {
                Alertme("Please select class.", "warning");
            }
            else
            {

            }
        }


        protected void ddl_section_SelectedIndexChanged(object sender, EventArgs e)
        {
            find_data();
            ViewState["flag"] = "1";
        }

        private void find_data()
        {
            if (ddl_session.SelectedItem.Text == "Select")
            {
                Alertme("Please select session ", "warning");
            }
            else if (ddl_course.SelectedItem.Text == "Select")
            {
                Alertme("Please select course name", "warning");
            }
            else
            {
                string query = "Select * from admission_registor where session='" + ddl_session.SelectedItem.Text + "' and Section='" + ddl_section.Text + "' and Class_id=" + ddl_course.SelectedValue + " and (Transfer_Status='New' or Transfer_Status='NT') and Status='1' order by rollnumber asc";
                if (ddl_section.Text.ToUpper() == "ALL")
                {
                    query = "Select * from admission_registor where session='" + ddl_session.SelectedItem.Text + "' and Class_id=" + ddl_course.SelectedValue + " and (Transfer_Status='New' or Transfer_Status='NT') and Status='1' order by rollnumber asc";
                }
                bind_grids(query);
            }
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_add"].ToString() == "1")
                {
                    make_student_transfer();
                }
                else if (ViewState["Is_Edit"].ToString() == "1")
                {
                    make_student_transfer();
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Admission_Transformation");
            }
        }

        private void make_student_transfer()
        {
            bool send = false;
            if (ddl_session_tranfser2.SelectedItem.Text == "Select")
            {
                ddl_session_tranfser2.Focus();
                Alertme("Please select session ", "warning");
            }
            else if (ddl_course_transfer2.SelectedItem.Text == "Select")
            {
                ddl_course_transfer2.Focus();
                Alertme("Please select class", "warning");
            }
            else if (ddl_session_tranfser2.SelectedValue == ddl_course_transfer2.SelectedItem.Text)
            {
                ddl_session_tranfser2.Focus();
                Alertme("Sorry! Your old session and current session is same.", "warning");
            }
            else if (txt_date.Text == "")
            {
                txt_date.Focus();
                Alertme("Please choose transfer date", "warning");
            }
            else
            {
                if (chk_isduestransfer.Checked == true)
                {
                    if (ddl_with_admission.SelectedItem.Text == "Select")
                    {
                        Alertme("Please select dues Transfer with admission fee Yes or No", "warning");
                        ddl_with_admission.Focus();
                    }
                    else if (ddl_transfer_to.SelectedItem.Text == "Select")
                    {
                        Alertme("Please select dues transfer in Annual Fee or a Monthly Fee.", "warning");
                        ddl_transfer_to.Focus();
                    }
                    else
                    {
                        send = true;
                    }
                }
                else
                {
                    send = true;
                }
                if (send == true)
                {
                    int grdcount = grd_studentdetails.Rows.Count;
                    int j = 0;
                    for (int i = 0; i < grdcount; i++)
                    {
                        CheckBox chk_transferwithtrasport = (CheckBox)grd_studentdetails.Rows[i].FindControl("chk_transferwithtrasport");
                        CheckBox chk_transfer_with_hostel = (CheckBox)grd_studentdetails.Rows[i].FindControl("chk_transfer_with_hostel");
                        Label lbl_admission_no = (Label)grd_studentdetails.Rows[i].FindControl("lbl_admission_no");
                        Label lbl_status = (Label)grd_studentdetails.Rows[i].FindControl("lbl_status");
                        Label lbl_id = (Label)grd_studentdetails.Rows[i].FindControl("lbl_id");
                        Label lbl_is_trasport = (Label)grd_studentdetails.Rows[i].FindControl("lbl_is_trasport");
                        Label lbl_is_hostel = (Label)grd_studentdetails.Rows[i].FindControl("lbl_is_hostel");
                        Label lbl_Transfer_Status = (Label)grd_studentdetails.Rows[i].FindControl("lbl_Transfer_Status");
                        Label lbl_Class_id = (Label)grd_studentdetails.Rows[i].FindControl("lbl_Class_id");

                        Label lbl_Session_id = (Label)grd_studentdetails.Rows[i].FindControl("lbl_Session_id");
                        Label lbl_Category_id = (Label)grd_studentdetails.Rows[i].FindControl("lbl_Category_id");
                        Label lbl_SubCategory_id = (Label)grd_studentdetails.Rows[i].FindControl("lbl_SubCategory_id");

                        Label lbl_is_applied_dayboarding = (Label)grd_studentdetails.Rows[i].FindControl("lbl_is_applied_dayboarding");
                        Label lbl_day_boarding_with_lunch = (Label)grd_studentdetails.Rows[i].FindControl("lbl_day_boarding_with_lunch");

                        Label lbl_Section = (Label)grd_studentdetails.Rows[i].FindControl("lbl_Section");
                        CheckBox check = (CheckBox)grd_studentdetails.Rows[i].FindControl("rowChkBox");
                        Label lbl_session = (Label)grd_studentdetails.Rows[i].FindControl("lbl_session");
                        Label lbl_course = (Label)grd_studentdetails.Rows[i].FindControl("lbl_course");

                        if (check.Checked == true)
                        {
                            submit_student_transfer(lbl_admission_no.Text, lbl_status.Text, lbl_id.Text, lbl_is_trasport.Text, lbl_is_hostel.Text, chk_transferwithtrasport, chk_transfer_with_hostel, lbl_Transfer_Status.Text, lbl_Class_id.Text, lbl_Session_id.Text, lbl_Category_id.Text, lbl_SubCategory_id.Text, lbl_is_applied_dayboarding.Text, lbl_day_boarding_with_lunch.Text, lbl_Section.Text, lbl_session.Text, lbl_course.Text);
                        }
                        else
                        {
                            j++;
                        }
                    }
                    if (j == grdcount)
                    {
                        Alertme("Please check any one checkbox of admission list list", "warning");
                        scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
                    }
                    else
                    {
                        if (ViewState["flag"].ToString() == "2")
                        {
                            find_by_admission();
                        }
                        if (ViewState["flag"].ToString() == "1")
                        {
                            find_data();
                        }
                        Alertme("Student has been successfully transferred to new session.", "success");
                        scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
                    }
                }
            }
        }

        private void submit_student_transfer(string admission_no, string status, string id, string is_trasport, string is_hostel, CheckBox chk_transferwithtrasport, CheckBox chk_transfer_with_hostel, string Transfer_Status, string Classid, string Session_id, string category_id, string SubCategory_id, string is_applied_dayboarding, string day_boarding_with_lunch, string Section, string session, string classname)
        {
            double final_total_dues_amount = 0; double totaldues_monthley = 0; double totaldues_admision_fee = 0; double latefine = 0;
            if (chk_isduestransfer.Checked == true)
            {
                bool is_transfer_dues = true;
                double dues = 0;
                //and  payment_status!='Unpaid'========================******************
                string hosteltaken = "No";
                if (is_hostel == "")
                {
                    hosteltaken = "No";
                }
                else if (is_hostel.ToString().ToLower() == "no")
                {
                    hosteltaken = "No";
                }
                else if (is_hostel.ToString().ToLower() == "yes")
                {
                    hosteltaken = "Yes";
                }
                else
                {
                    hosteltaken = "No";
                }

                string transportationtaken = is_trasport;
                bool day_bording = My.toBool(is_applied_dayboarding);
                bool day_bording_with_lunch = My.toBool(day_boarding_with_lunch);
                Dictionary<string, object> dc2 = mycode.Bind_Transport_data_for_assined_student(Session_id, Classid, admission_no);
                ViewState["Transport_id"] = (String)dc2["Transport_id"];
                string Transportation_Id = (String)dc2["TransportPath_id"];
                ViewState["TransportPath_id"] = (String)dc2["TransportPath_id"];
                ViewState["Boarding_Point_id"] = (String)dc2["Boarding_Point_id"];
                ViewState["Transport_Assigned_Id"] = (String)dc2["Transport_Assigned_Id"];
                ViewState["Month_name"] = (String)dc2["Month_name"];
                ViewState["Month_id"] = (String)dc2["Month_id"];
                ViewState["Year_month"] = (String)dc2["Year_month"];
                ViewState["Sheet_Id"] = (String)dc2["Sheet_Id"];


                Dictionary<string, object> dc1 = mycode.Bind_hostel_data_for_assined_student(Session_id, Classid, admission_no);
                string Hostel_id = (String)dc1["Hostel_id"];
                string Room_Category_id = (String)dc1["Room_Category_id"];
                //ViewState["From_month_name"] = (String)dc1["From_month_name"];
                //ViewState["From_month_id"] = (String)dc1["From_month_id"];
                // ViewState["Assined_Year_Month"] = (String)dc1["Assined_Year_Month"];
                string Hostel_assign_id = (String)dc1["Hostel_assign_id"];
                string Branch_id = "1";

                ViewState["IsBoarding"] = "0";
                ViewState["parameteridS"] = "4";
                string queryS = "select * from Student_mapping_with_boarding_with_lunch where Session_id='" + Session_id + "' and Admission_no='" + admission_no + "' and Class_id='" + Classid + "'";
                DataTable dts = My.dataTable(queryS);
                if (dts.Rows.Count != 0)
                {
                    ViewState["LunchMnthName"] = dts.Rows[0]["Month_name"].ToString();
                    ViewState["LunchMnthId"] = dts.Rows[0]["Month_id"].ToString();
                    ViewState["IsBoarding"] = "1";
                }



                if (ddl_with_admission.SelectedValue == "1")
                {
                    totaldues_admision_fee = find_admission_dues(Session_id, hosteltaken, admission_no, session, Classid, Transfer_Status, Hostel_id, category_id, SubCategory_id);
                }

                totaldues_monthley = find_monthley_dues(Session_id, hosteltaken, admission_no, session, Classid, Transfer_Status, classname, Hostel_id, transportationtaken, day_bording, day_bording_with_lunch, category_id, SubCategory_id, Transportation_Id, Section, Branch_id, Room_Category_id, Hostel_assign_id);
                final_total_dues_amount = totaldues_admision_fee + totaldues_monthley;
            }



            // End Dues

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
            {
                SqlConnection con = new SqlConnection(My.conn);
                con.Open();

                string query = "Select * from admission_registor where session='" + ddl_session.SelectedItem.Text + "'  and Class_id=" + Classid + "  and (Transfer_Status='New' or Transfer_Status='NT') and admissionserialnumber='" + admission_no + "' ";
                if (ddl_section.Text.ToUpper() == "ALL")
                {
                    query = "Select * from admission_registor where session='" + ddl_session.SelectedItem.Text + "'  and Class_id=" + Classid + "  and (Transfer_Status='New' or Transfer_Status='NT') and admissionserialnumber='" + admission_no + "'";
                }
                SqlDataAdapter ad = new SqlDataAdapter(query, con);
                DataSet ds = new DataSet();
                ad.Fill(ds, "admission_registor");
                DataTable dt = ds.Tables[0];
                int rowcount = dt.Rows.Count;
                if (rowcount == 0)
                {
                }
                else
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = ddl_course_transfer2.SelectedItem.Text;
                    dr[1] = dt.Rows[0][1].ToString();
                    dr[2] = dt.Rows[0][2].ToString();
                    if (chk_issection_and_roll_no.Checked == true)
                    {
                        dr[3] = dt.Rows[0][3].ToString();
                        dr[4] = dt.Rows[0][4].ToString();

                        //if (ddl_section.Text.ToUpper() == "ALL")
                        //{
                        //    dr[4] = dt.Rows[0][4].ToString();
                        //}
                        //else
                        //{
                        //    dr[4] = ddl_section_transfer_2.Text;
                        //}
                    }
                    else
                    {
                        dr[3] = "0";

                        if (ddl_section_transfer_2.Text == "NA")
                        {
                            dr[4] = "NA";
                        }
                        if (ddl_section_transfer_2.Text == "N/A")
                        {
                            dr[4] = "NA";
                        }
                        else
                        {
                            dr[4] = ddl_section_transfer_2.Text;

                        }
                    }
                    dr[5] = ddl_session_tranfser2.SelectedItem.Text;
                    dr[6] = dt.Rows[0][6].ToString();// txt_date.Text;// session date current date
                    dr[7] = dt.Rows[0][7].ToString();
                    dr[10] = dt.Rows[0][10].ToString();
                    dr[11] = dt.Rows[0][11].ToString();
                    dr[12] = dt.Rows[0][12].ToString();
                    dr[13] = dt.Rows[0][13].ToString();
                    dr[14] = dt.Rows[0][14].ToString();
                    dr[15] = dt.Rows[0][15].ToString();
                    dr[16] = dt.Rows[0][16].ToString();
                    dr[17] = dt.Rows[0][17].ToString();
                    dr[18] = dt.Rows[0][18].ToString();
                    dr[19] = dt.Rows[0][19].ToString();
                    dr[20] = dt.Rows[0][20].ToString();
                    dr[21] = dt.Rows[0][21].ToString();
                    dr[22] = dt.Rows[0][22].ToString();
                    dr[23] = dt.Rows[0][23].ToString();
                    dr[24] = dt.Rows[0][24].ToString();
                    dr[25] = dt.Rows[0][25].ToString();
                    dr[26] = dt.Rows[0][26].ToString();
                    dr[27] = dt.Rows[0][27].ToString();
                    dr[28] = dt.Rows[0][28].ToString();
                    dr[29] = dt.Rows[0][29].ToString();
                    dr[30] = dt.Rows[0][30].ToString();
                    dr[31] = dt.Rows[0][31].ToString();
                    dr[32] = dt.Rows[0][32].ToString();
                    dr[33] = dt.Rows[0][33].ToString();
                    dr[34] = dt.Rows[0][34].ToString();
                    dr[35] = dt.Rows[0][35].ToString();
                    dr[36] = dt.Rows[0][36].ToString();
                    dr[37] = dt.Rows[0][37].ToString();
                    dr[38] = dt.Rows[0][38].ToString();
                    dr[43] = "Unpaid";
                    dr[47] = "NT";
                    dr[48] = dt.Rows[0][48].ToString();
                    dr[49] = dt.Rows[0][49].ToString();
                    dr[50] = dt.Rows[0][50].ToString();
                    dr[52] = "AV";
                    dr[53] = dt.Rows[0][53].ToString(); ;

                    try
                    {
                        dr[55] = dt.Rows[0]["admission_idate"].ToString(); // Convert.ToDateTime(txt_date.Text).ToString("yyyyMMdd");
                    }
                    catch
                    {
                    }
                    dr["staff_ward"] = dt.Rows[0]["staff_ward"].ToString();
                    dr["jati"] = dt.Rows[0]["jati"].ToString();
                    dr["mob2"] = dt.Rows[0]["mob2"].ToString();

                    dr["Session_id"] = ddl_session_tranfser2.SelectedValue;
                    dr["Class_id"] = ddl_course_transfer2.SelectedValue;
                    dr["Is_TC_Taken"] = false;
                    dr["Student_id"] = dt.Rows[0]["Student_id"].ToString();
                    dr["Academic_Sem_or_Year"] = "";
                    dr["Academic_Sem_or_Year_id"] = "0";
                    dr["Category_id"] = dt.Rows[0]["Category_id"].ToString();
                    dr["SubCategory_id"] = dt.Rows[0]["SubCategory_id"].ToString();
                    dr["Branch_id"] = dt.Rows[0]["Branch_id"].ToString();

                    dr["studentimagepath"] = dt.Rows[0]["studentimagepath"].ToString();
                    dr["is_applied_dayboarding"] = 0;
                    dr["day_boarding_with_lunch"] = 0;


                    dr["email_id"] = dt.Rows[0]["email_id"].ToString();
                    dr["birth_certificate_number"] = dt.Rows[0]["birth_certificate_number"].ToString();
                    dr["place_of_birth"] = dt.Rows[0]["place_of_birth"].ToString();
                    dr["blood_group"] = dt.Rows[0]["blood_group"].ToString();
                    dr["cast_certificate_no"] = dt.Rows[0]["cast_certificate_no"].ToString();
                    dr["student_mother_tounge"] = dt.Rows[0]["student_mother_tounge"].ToString();
                    dr["is_illness"] = dt.Rows[0]["is_illness"].ToString();
                    dr["f_nationality"] = dt.Rows[0]["f_nationality"].ToString();
                    dr["f_marrital_statue"] = dt.Rows[0]["f_marrital_statue"].ToString();
                    dr["m_marrital_statue"] = dt.Rows[0]["m_marrital_statue"].ToString();
                    dr["m_nationality"] = dt.Rows[0]["m_nationality"].ToString();
                    dr["m_occupation"] = dt.Rows[0]["m_occupation"].ToString();
                    dr["ration_type"] = dt.Rows[0]["ration_type"].ToString();
                    dr["illness_remark"] = dt.Rows[0]["illness_remark"].ToString();
                    dr["father_mob"] = dt.Rows[0]["father_mob"].ToString();
                    dr["mother_mob"] = dt.Rows[0]["mother_mob"].ToString();
                    dr["mother_email"] = dt.Rows[0]["mother_email"].ToString();
                    dr["Account_Holder_name"] = dt.Rows[0]["Account_Holder_name"].ToString();
                    dr["Bnk_Name"] = dt.Rows[0]["Bnk_Name"].ToString();
                    dr["IFSC_Code"] = dt.Rows[0]["IFSC_Code"].ToString();
                    dr["Branch_Name"] = dt.Rows[0]["Branch_Name"].ToString();
                    dr["lib_card_no"] = dt.Rows[0]["lib_card_no"].ToString();
                    dr["Course_Type"] = dt.Rows[0]["Course_Type"].ToString();

                    dr["CET_ROLL_NO"] = dt.Rows[0]["CET_ROLL_NO"].ToString();
                    dr["CET_RANK"] = dt.Rows[0]["CET_RANK"].ToString();
                    dr["CET_CATEGORY"] = dt.Rows[0]["CET_CATEGORY"].ToString();
                    dr["MAKAUT_Student_ID"] = dt.Rows[0]["MAKAUT_Student_ID"].ToString();
                    dr["MAKAUT_Student_password"] = dt.Rows[0]["MAKAUT_Student_password"].ToString();
                    dr["Student_Whatsapp_No"] = dt.Rows[0]["Student_Whatsapp_No"].ToString();
                    dr["Student_Other_Interest_Area"] = dt.Rows[0]["Student_Other_Interest_Area"].ToString();

                    dr["Student_Name_First"] = dt.Rows[0]["Student_Name_First"].ToString();
                    dr["Student_Middle_Name"] = dt.Rows[0]["Student_Middle_Name"].ToString();
                    dr["Student_Name_Last"] = dt.Rows[0]["Student_Name_Last"].ToString();
                    dr["Father_Name_First"] = dt.Rows[0]["Father_Name_First"].ToString();
                    dr["Father_Name_Middle"] = dt.Rows[0]["Father_Name_Middle"].ToString();
                    dr["Father_Name_Last"] = dt.Rows[0]["Father_Name_Last"].ToString();
                    dr["Mother_Name_First"] = dt.Rows[0]["Mother_Name_First"].ToString();
                    dr["Mother_Name_Middle"] = dt.Rows[0]["Mother_Name_Middle"].ToString();
                    dr["Mother_Name_Last"] = dt.Rows[0]["Mother_Name_Last"].ToString();
                    dr["Personal_Identymarks"] = dt.Rows[0]["Personal_Identymarks"].ToString();
                    dr["Country_Code_Father"] = dt.Rows[0]["Country_Code_Father"].ToString();
                    dr["Country_Code_Mother"] = dt.Rows[0]["Country_Code_Mother"].ToString();
                    dr["Country_Code_Current_add"] = dt.Rows[0]["Country_Code_Current_add"].ToString();
                    dr["Country_Code_Current_Perm_add"] = dt.Rows[0]["Country_Code_Current_Perm_add"].ToString();
                    dr["gcm_id"] = dt.Rows[0]["gcm_id"].ToString();
                    dr["Pwd"] = dt.Rows[0]["Pwd"].ToString();
                    dr["Bank_acount_no"] = dt.Rows[0]["Bank_acount_no"].ToString();
                    dr["Device_Id"] = dt.Rows[0]["Device_Id"].ToString();
                    dr["Status"] = "1"; //status
                    dr["Edit_Istatus"] = "0";
                    dr["Old_Admission_Date"] = dt.Rows[0]["Old_Admission_Date"].ToString();
                    dr["OLd_Admission_Idate"] = mycode.ConvertStringToiDate(dt.Rows[0]["Old_Admission_Date"].ToString());
                    dr["Old_class_id"] = My.toInt(dt.Rows[0]["Old_class_id"].ToString());
                    dr["UID_No"] = dt.Rows[0]["UID_No"].ToString();
                    dr["hosteltaken"] = "No";
                    dr["Hostel_id"] = "0";
                    dr["Created_by"] = ViewState["Userid"].ToString();
                    dr["Created_idate"] = mycode.idate();
                    dr["Created_time"] = mycode.time();
                    dr["Created_date"] = mycode.date();
                    dr["mother_signature"] = dt.Rows[0]["mother_signature"].ToString();

                    dr["Country_Name_CA"] = dt.Rows[0]["Country_Name_CA"].ToString();
                    dr["Country_Name_PA"] = dt.Rows[0]["Country_Name_PA"].ToString();
                    dr["Is_birth_certificate"] = dt.Rows[0]["Is_birth_certificate"].ToString();
                    dr["Student_nationality"] = dt.Rows[0]["Student_nationality"].ToString();
                    dr["Is_cast_certificate"] = dt.Rows[0]["Is_cast_certificate"].ToString();
                    dr["Staff_name"] = dt.Rows[0]["Staff_name"].ToString();
                    dr["Prev_school_name"] = dt.Rows[0]["Prev_school_name"].ToString();
                    dr["Prev_board_type"] = dt.Rows[0]["Prev_board_type"].ToString();
                    dr["Prev_percentage"] = dt.Rows[0]["Prev_percentage"].ToString();
                    dr["Prev_reason_for_shift"] = dt.Rows[0]["Prev_reason_for_shift"].ToString();
                    dr["Prev_year"] = dt.Rows[0]["Prev_year"].ToString();
                    dr["Father_whatsapp_country_code"] = dt.Rows[0]["Father_whatsapp_country_code"].ToString();
                    dr["Father_whatsApp_no"] = dt.Rows[0]["Father_whatsApp_no"].ToString();
                    dr["Mother_whatsapp_country_code"] = dt.Rows[0]["Mother_whatsapp_country_code"].ToString();
                    dr["Mother_whatsApp_no"] = dt.Rows[0]["Mother_whatsApp_no"].ToString();
                    dr["Present_country"] = dt.Rows[0]["Present_country"].ToString();
                    dr["Staff_employee_code"] = dt.Rows[0]["Staff_employee_code"].ToString();
                    dr["Father_aadhar_no"] = dt.Rows[0]["Father_aadhar_no"].ToString();
                    dr["Mother_aadhar_no"] = dt.Rows[0]["Mother_aadhar_no"].ToString();
                    dr["Height"] = dt.Rows[0]["Height"].ToString();
                    dr["Weight"] = dt.Rows[0]["Weight"].ToString();
                    dr["Sibling_name1"] = dt.Rows[0]["Sibling_name1"].ToString();
                    dr["Sibling_age1"] = dt.Rows[0]["Sibling_age1"].ToString();
                    dr["Sibling_school1"] = dt.Rows[0]["Sibling_school1"].ToString();
                    dr["Sibling_class1"] = dt.Rows[0]["Sibling_class1"].ToString();
                    dr["Admission_Transfer_date"] = Convert.ToDateTime(txt_date.Text).ToString("yyyyMMdd");
                    dr["Admission_Transfer_Idate"] = Convert.ToDateTime(txt_date.Text).ToString("yyyyMMdd");
                    dt.Rows.Add(dr);
                    SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                    ad.Update(dt);

                    update_transfer_status(admission_no, Transfer_Status, Classid, con);
                    if (chk_transferwithtrasport.Checked == true)
                    {
                        update_Student_mapping_with_TransportPath(admission_no, Classid, con);
                    }
                    else if (chk_transfer_with_hostel.Checked == true)
                    {
                        update_Student_mapping_with_hostel(admission_no, Classid, con);
                    }
                    try
                    {
                        payments.studentsubject_mapping(ddl_session_tranfser2.SelectedValue, ddl_session_tranfser2.SelectedItem.Text, ddl_course_transfer2.SelectedValue, admission_no, ddl_section_transfer_2.Text, ViewState["branch_id"].ToString(), con);
                    }
                    catch
                    {
                    }


                    try
                    {
                        if (My.toDouble(final_total_dues_amount) > 0)
                        {
                            insert_insert_data_Dues_Month_Calculation(totaldues_monthley, totaldues_admision_fee, admission_no, Session_id, latefine, Classid, con);
                            if (ddl_transfer_to.SelectedItem.Text == "Annual Fee")
                            {
                                final_save_data_Previous_Year_Dues(admission_no, final_total_dues_amount.ToString("0.00"), Classid, Session_id, ddl_session_tranfser2.SelectedValue, con);
                            }
                            else
                            {
                                final_save_data_Previous_Year_monthDues(admission_no, final_total_dues_amount.ToString("0.00"), Classid, Session_id, ddl_session_tranfser2.SelectedValue, con);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
                con.Close();
                scope.Complete();
            }
        }



        private void final_save_data_Previous_Year_monthDues(string admission_no, string totalfee, string old_classid, string old_Session_id, string newssionid, SqlConnection con)
        {
            string month = "April";
            DataTable dt1 = payments.dataTable("select top 1 * from Month_Index order by Position", con);
            if (dt1.Rows.Count == 0)
            {
                month = "April";
            }
            else
            {
                month = dt1.Rows[0]["Month"].ToString();
            }
            DataTable dt = payments.dataTable("select * from Misc_Fee_Master_Studentwise where Admission_No='" + admission_no + "' and Month='" + month + "' and Session_id='" + newssionid + "'  and Perticular='Previous Year Dues'", con);
            if (dt.Rows.Count == 0)
            {
                SqlCommand cmd;
                string query = "INSERT INTO Misc_Fee_Master_Studentwise (Admission_No,Month,Session,Session_id,Perticular,Amount,Old_year_Dues_Type,Date,Idate,Created_by) values (@Admission_No,@Month,@Session,@Session_id,@Perticular,@Amount,@Old_year_Dues_Type,@Date,@Idate,@Created_by)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Admission_No", admission_no);
                cmd.Parameters.AddWithValue("@Month", "April");
                cmd.Parameters.AddWithValue("@Session", ddl_session_tranfser2.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Session_id", newssionid);
                cmd.Parameters.AddWithValue("@Perticular", "Previous Year Dues");
                cmd.Parameters.AddWithValue("@Amount", totalfee);
                cmd.Parameters.AddWithValue("@Old_year_Dues_Type", "Yes");
                cmd.Parameters.AddWithValue("@Date", mycode.date());
                cmd.Parameters.AddWithValue("@Idate", mycode.idate());
                cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                if (payments.InsertUpdateData(cmd, con))
                {
                }
            }
            else
            {

                string id = dt.Rows[0]["Id"].ToString();
                SqlCommand cmd;
                string query2 = "Update Misc_Fee_Master_Studentwise set Admission_No=@Admission_No,Month=@Month,Session=@Session,Session_id=@Session_id,Amount=@Amount,Date=@Date,Idate=@Idate,Created_by=@Created_by where Id = @Id";
                cmd = new SqlCommand(query2);
                cmd.Parameters.AddWithValue("@Admission_No", admission_no);
                cmd.Parameters.AddWithValue("@Month", month);
                cmd.Parameters.AddWithValue("@Session", ddl_session_tranfser2.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Session_id", newssionid);
                cmd.Parameters.AddWithValue("@Amount", totalfee);
                cmd.Parameters.AddWithValue("@Date", mycode.date());
                cmd.Parameters.AddWithValue("@Idate", mycode.idate());
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                if (payments.InsertUpdateData(cmd, con))
                {
                }
            }
        }

        private void final_save_data_Previous_Year_Dues(string admission_no, string totalfee, string old_classid, string old_Session_id, string newssionid, SqlConnection con)
        {
            DataTable dt = payments.dataTable("select * from Misc_Fee_Master_Studentwise where Admission_No='" + admission_no + "' and Session_id='" + newssionid + "' and Perticular='Previous Year Dues'", con);
            if (dt.Rows.Count == 0)
            {
                SqlCommand cmd;
                string query = "INSERT INTO Misc_Fee_Master_Studentwise (Admission_No,Session,Session_id,Perticular,Amount,Old_year_Dues_Type,Date,Idate,Created_by,Type_Mode) values (@Admission_No,@Session,@Session_id,@Perticular,@Amount,@Old_year_Dues_Type,@Date,@Idate,@Created_by,@Type_Mode)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Admission_No", admission_no);
                cmd.Parameters.AddWithValue("@Session", ddl_session_tranfser2.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Session_id", newssionid);
                cmd.Parameters.AddWithValue("@Perticular", "Previous Year Dues");
                cmd.Parameters.AddWithValue("@Amount", totalfee);
                cmd.Parameters.AddWithValue("@Old_year_Dues_Type", "Yes");
                cmd.Parameters.AddWithValue("@Date", mycode.date());
                cmd.Parameters.AddWithValue("@Idate", mycode.idate());
                cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Type_Mode", "AnnualFee");
                if (payments.InsertUpdateData(cmd, con))
                {
                }
            }
            else
            {
                string id = dt.Rows[0]["Id"].ToString();
                SqlCommand cmd;
                string query2 = "Update Misc_Fee_Master_Studentwise set Admission_No=@Admission_No,Session=@Session,Session_id=@Session_id,Amount=@Amount,Date=@Date,Idate=@Idate,Created_by=@Created_by where Id = @Id";
                cmd = new SqlCommand(query2);
                cmd.Parameters.AddWithValue("@Admission_No", admission_no);
                cmd.Parameters.AddWithValue("@Session", ddl_session_tranfser2.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Session_id", newssionid);
                cmd.Parameters.AddWithValue("@Amount", totalfee);
                cmd.Parameters.AddWithValue("@Date", mycode.date());
                cmd.Parameters.AddWithValue("@Idate", mycode.idate());
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                if (payments.InsertUpdateData(cmd, con))
                {
                }
            }
        }


        private void insert_insert_data_Dues_Month_Calculation(double totaldues_monthley, double totaldues_admision_fee, string admission_no, string session_id, double latefine, string classid, SqlConnection con)
        {
            double total = totaldues_monthley + totaldues_admision_fee + latefine;
            DataTable dt = payments.dataTable("Select * from Dues_Month_Calculation where Session_id='" + session_id + "' and Admission_no='" + admission_no + "' ", con);//My.FillDatastatic("Select Month from Month_Index where Position='1'  ");
            if (dt.Rows.Count == 0)
            {
                SqlCommand cmd;
                string query = "INSERT INTO Dues_Month_Calculation (Session_id,Admission_no,Admission_fee,Month_Fee,Late_fine,Date,Idate,User_id,Class_id,Total_fee) values (@Session_id,@Admission_no,@Admission_fee,@Month_Fee,@Late_fine,@Date,@Idate,@User_id,@Class_id,@Total_fee)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Session_id", session_id);
                cmd.Parameters.AddWithValue("@Admission_no", admission_no);
                cmd.Parameters.AddWithValue("@Admission_fee", totaldues_admision_fee.ToString("0.00"));
                cmd.Parameters.AddWithValue("@Month_Fee", totaldues_monthley.ToString("0.00"));
                cmd.Parameters.AddWithValue("@Late_fine", latefine.ToString("0.00"));
                cmd.Parameters.AddWithValue("@Date", mycode.date());
                cmd.Parameters.AddWithValue("@Idate", mycode.date());
                cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Class_id", classid);
                cmd.Parameters.AddWithValue("@Total_fee", total.ToString("0.00"));
                if (payments.InsertUpdateData(cmd, con))
                {
                }
            }
            else
            {
                string id = dt.Rows[0]["Id"].ToString();
                SqlCommand cmd2;
                string query2 = "Update Dues_Month_Calculation set  Admission_fee=@Admission_fee,Month_Fee=@Month_Fee,Late_fine=@Late_fine,Date=@Date,Idate=@Idate,User_id=@User_id,Class_id=@Class_id,Total_fee=@Total_fee where Session_id=@Session_id and Admission_no=@Admission_no";
                cmd2 = new SqlCommand(query2);
                cmd2.Parameters.AddWithValue("@Session_id", session_id);
                cmd2.Parameters.AddWithValue("@Admission_no", admission_no);
                cmd2.Parameters.AddWithValue("@Admission_fee", totaldues_admision_fee.ToString());
                cmd2.Parameters.AddWithValue("@Month_Fee", totaldues_monthley.ToString());
                cmd2.Parameters.AddWithValue("@Late_fine", latefine);
                cmd2.Parameters.AddWithValue("@Date", mycode.date());
                cmd2.Parameters.AddWithValue("@Idate", mycode.date());
                cmd2.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                cmd2.Parameters.AddWithValue("@Class_id", classid);
                cmd2.Parameters.AddWithValue("@Total_fee", total.ToString("0.00"));
                if (payments.InsertUpdateData(cmd2, con))
                {
                }
            }
        }

        private void update_Student_mapping_with_hostel(string admission_no, string Class_id, SqlConnection con)
        {
            string cunrt_session = ddl_session_tranfser2.SelectedItem.Text;
            string session_frst_year = cunrt_session.Substring(0, 4);
            int session_s_year = My.toint(session_frst_year);
            int s_year = My.toint(session_frst_year);
            int pay_month;

            string year_month_id = "";
            string queryS = "select  * from HOSTEL_ASSIGN_MASTER where Session_id='" + ddl_session_tranfser2.SelectedValue + "' and Admission_no='" + admission_no + "' ";
            DataTable dts = payments.dataTable(queryS, con);
            if (dts.Rows.Count == 0)
            {
                string query2 = "Select TOP 1 * from HOSTEL_ASSIGN_MASTER where Admission_no='" + admission_no + "' and Session_id='" + ddl_session.SelectedValue + "' and Class_id='" + Class_id + "' and Status='1' order by id desc ";
                DataTable dts1 = payments.dataTable(query2, con);
                if (dts1.Rows.Count != 0)
                {
                    string Hostel_id = dts1.Rows[0]["Hostel_id"].ToString();
                    string Category_id = dts1.Rows[0]["Category_id"].ToString();
                    string Room_id = dts1.Rows[0]["Room_id"].ToString();
                    string Bed_id = dts1.Rows[0]["Bed_id"].ToString();

                    Dictionary<string, object> dc1 = payss.get_start_month_and_month_id(con);
                    string Month = (String)dc1["Month"];
                    string Month_Id = (String)dc1["Month_Id"];
                    pay_month = My.toint(Month_Id);
                    s_year = payments.check_start_months(pay_month, s_year, con);
                    year_month_id = s_year.ToString() + Month_Id;
                    string hostel_assign_id = create_sl_no(con);

                    SqlCommand cmd;
                    string query = "INSERT INTO Hostel_assign_master (Session_id,Class_id,Admission_no,From_month_name,From_month_id,Hostel_id,Category_id,Room_id,Bed_id,Hostel_assign_id,Created_by,Created_date,Created_idate,Status,Assined_Year_Month) values (@Session_id,@Class_id,@Admission_no,@From_month_name,@From_month_id,@Hostel_id,@Category_id,@Room_id,@Bed_id,@Hostel_assign_id,@Created_by,@Created_date,@Created_idate,@Status,@Assined_Year_Month)";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Session_id", ddl_session_tranfser2.SelectedValue);
                    cmd.Parameters.AddWithValue("@Class_id", ddl_course_transfer2.SelectedValue);
                    cmd.Parameters.AddWithValue("@Admission_no", admission_no);
                    cmd.Parameters.AddWithValue("@From_month_name", Month);
                    cmd.Parameters.AddWithValue("@From_month_id", Month_Id);
                    cmd.Parameters.AddWithValue("@Hostel_id", Hostel_id);
                    cmd.Parameters.AddWithValue("@Category_id", Category_id);
                    cmd.Parameters.AddWithValue("@Room_id", Room_id);
                    cmd.Parameters.AddWithValue("@Bed_id", Bed_id);
                    cmd.Parameters.AddWithValue("@Hostel_assign_id", hostel_assign_id);
                    cmd.Parameters.AddWithValue("@Status", 1);
                    cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                    cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
                    cmd.Parameters.AddWithValue("@Assined_Year_Month", year_month_id);
                    if (payments.InsertUpdateData(cmd, con))
                    {
                        try
                        {
                            payments.exeSql("update admission_registor set hosteltaken='Yes',transportationtaken='No',Hostel_id='" + Hostel_id + "',Hostel_assignD_id='" + hostel_assign_id + "' where admissionserialnumber='" + admission_no + "'  and Session_id='" + ddl_session_tranfser2.SelectedValue + "' ", con);
                        }
                        catch
                        {
                        }
                    }
                }
            }
        }

        private string create_sl_no(SqlConnection con)
        {
            bool duplicate = true;
            string hostel_assign_id = payments.auto_serialS("Hostel_assign_id", con);
            while (duplicate)
            {
                DataTable cdt = payments.dataTable("select Hostel_assign_id from Hostel_assign_master where Hostel_assign_id='" + hostel_assign_id + "'", con);
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    duplicate = false;
                }
                else
                {
                    hostel_assign_id = payments.auto_serialS("Hostel_assign_id", con);
                }
            }
            return hostel_assign_id;
        }
        private void update_transfer_status(string admission_no, string Transfer_Status, string Class_id, SqlConnection con)
        {
            payments.exeSql("update admission_registor set Transfer_Status='Transferred',Transfer_date='" + mycode.date() + "',Transfer_idate='" + mycode.idate() + "',Transfer_Status_Old='" + Transfer_Status + "',Admission_Transfer_by='" + ViewState["Userid"].ToString() + "' where Class_id='" + Class_id + "' and Session_id='" + ddl_session.SelectedValue + "'  and admissionserialnumber='" + admission_no + "'", con);
        }

        payments payss = new payments();
        private void update_Student_mapping_with_TransportPath(string admission_no, string Class_id, SqlConnection con)
        {
            string tranportassinedid = payments.get_transport_assigned_id(con);
            string cunrt_session = ddl_session_tranfser2.SelectedItem.Text;
            string session_frst_year = cunrt_session.Substring(0, 4);
            int session_s_year = My.toint(session_frst_year);
            int s_year = My.toint(session_frst_year);

            int pay_month = 0;
            string final = "";
            try
            {
                string queryS = "select   * from Student_mapping_with_TransportPath where Session_id='" + ddl_session_tranfser2.SelectedValue + "' and Admission_no='" + admission_no + "'    ";
                DataTable dts = payments.dataTable(queryS, con);
                if (dts.Rows.Count == 0)
                {
                    string query2 = "Select top 1 * from Student_mapping_with_TransportPath where Admission_no='" + admission_no + "' and Session_id='" + ddl_session.SelectedValue + "' and Class_id='" + Class_id + "' order by id desc ";
                    DataTable dts1 = payments.dataTable(query2, con);
                    if (dts1.Rows.Count != 0)
                    {
                        string TransportPath_id = dts1.Rows[0]["TransportPath_id"].ToString();
                        string transport_id = dts1.Rows[0]["transport_id"].ToString();
                        string Sheet_Id = dts1.Rows[0]["Sheet_Id"].ToString();
                        string Boarding_Point_id = dts1.Rows[0]["Boarding_Point_id"].ToString();

                        Dictionary<string, object> dc1 = payss.get_start_month_and_month_id(con);
                        string Month = (String)dc1["Month"];
                        string Month_Id = (String)dc1["Month_Id"];
                        pay_month = My.toint(Month_Id);
                        s_year = payments.check_start_months(pay_month, s_year, con);
                        final = s_year + Month_Id;
                        SqlCommand cmd;
                        string query = "INSERT INTO Student_mapping_with_TransportPath (Session_id,Admission_no,Class_id,Month_name,Month_id,TransportPath_id,Created_by,Created_date,Created_idate,branch_id,Year_month,Transport_Assigned_Id,Boarding_Point_id,transport_id,Sheet_Id,Mapping_Year) values (@Session_id,@Admission_no,@Class_id,@Month_name,@Month_id,@TransportPath_id,@Created_by,@Created_date,@Created_idate,@branch_id,@Year_month,@Transport_Assigned_Id,@Boarding_Point_id,@transport_id,@Sheet_Id,@Mapping_Year); INSERT INTO Student_mapping_with_TransportPath_history (Session_id,Admission_no,Class_id,Month_name,Month_id,TransportPath_id,Remark,Created_by,Created_date,Created_idate,branch_id,Year_month,Transport_Assigned_Id,Update_Status,Boarding_Point_id,transport_id,Sheet_Id,Mapping_Year) values (@Session_id,@Admission_no,@Class_id,@Month_name,@Month_id,@TransportPath_id,@Remark,@Created_by,@Created_date,@Created_idate,@branch_id,@Year_month,@Transport_Assigned_Id,@Update_Status,@Boarding_Point_id,@transport_id,@Sheet_Id,@Mapping_Year)";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@Session_id", ddl_session_tranfser2.SelectedValue);
                        cmd.Parameters.AddWithValue("@Class_id", ddl_course_transfer2.SelectedValue);
                        cmd.Parameters.AddWithValue("@Admission_no", admission_no);
                        cmd.Parameters.AddWithValue("@Month_name", Month);
                        cmd.Parameters.AddWithValue("@Month_id", Month_Id);
                        cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                        cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                        cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
                        cmd.Parameters.AddWithValue("@Remark", "Transport assigned");
                        cmd.Parameters.AddWithValue("@branch_id", ViewState["branch_id"].ToString());
                        cmd.Parameters.AddWithValue("@TransportPath_id", TransportPath_id);
                        cmd.Parameters.AddWithValue("@Year_month", final);
                        cmd.Parameters.AddWithValue("@Transport_Assigned_Id", tranportassinedid);
                        cmd.Parameters.AddWithValue("@Update_Status", "Assigned");
                        cmd.Parameters.AddWithValue("@Boarding_Point_id", Boarding_Point_id);
                        cmd.Parameters.AddWithValue("@transport_id", transport_id);
                        cmd.Parameters.AddWithValue("@Sheet_Id", Sheet_Id);
                        cmd.Parameters.AddWithValue("@Mapping_Year", s_year);
                        if (payments.InsertUpdateData(cmd, con))
                        {
                            SqlCommand cmd1;
                            string query1 = "Update admission_registor set   transportationtaken=@transportationtaken,hosteltaken=@hosteltaken,Transportation_Id=@Transportation_Id,Transportationpath=@Transportationpath where admissionserialnumber=@admissionserialnumber and Session_id=@Session_id ";
                            cmd1 = new SqlCommand(query1);
                            cmd1.Parameters.AddWithValue("@transportationtaken", "Yes");
                            cmd1.Parameters.AddWithValue("@hosteltaken", "No");
                            cmd1.Parameters.AddWithValue("@Session_id", ddl_session_tranfser2.SelectedValue);
                            cmd1.Parameters.AddWithValue("@Transportation_Id", transport_id);
                            cmd1.Parameters.AddWithValue("@Transportationpath", payments.get_TransportationPath(TransportPath_id, con));
                            cmd1.Parameters.AddWithValue("@admissionserialnumber", admission_no);
                            if (payments.InsertUpdateData(cmd1, con))
                            {
                            }
                        }
                    }
                    else
                    {
                    }
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
            }





        }
        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_session.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session.", "warning");
                }
                else if (ddl_course.SelectedItem.Text == "Select")
                {
                    Alertme("Please select class.", "warning");
                }
                else
                {
                    mycode.bind_all_ddl_with_id(ddl_session_tranfser2, "Select Session,session_id from session_details where session_id!='" + ddl_session.SelectedValue + "'order by cast((Substring (Session,1,4)) as int)");
                    ddl_session_adm.SelectedValue = ddl_session.SelectedValue;
                    find_data();
                    ViewState["flag"] = "1";
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void btn_find_by_adm_no_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_session_adm.SelectedItem.Text == "Select")
                {
                    ddl_session_adm.Focus();
                    Alertme("Please select session.", "warning");
                }
                else if (txt_Adm_no.Text == "")
                {
                    txt_Adm_no.Focus();
                    Alertme("Please enter admission no.", "warning");
                }
                else
                {
                    try
                    {
                        ddl_session.SelectedValue = ddl_session_adm.SelectedValue;
                    }
                    catch
                    {
                    }
                    mycode.bind_all_ddl_with_id(ddl_session_tranfser2, "Select Session,session_id from session_details where session_id!='" + ddl_session.SelectedValue + "'order by cast((Substring (Session,1,4)) as int)");

                    find_by_admission();
                    ViewState["flag"] = "2";
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_by_admission()
        {
            string query = "Select * from admission_registor where session='" + ddl_session_adm.SelectedItem.Text + "' and admissionserialnumber='" + txt_Adm_no.Text + "'   and (Transfer_Status='New' or Transfer_Status='NT') and    Status='1' order by rollnumber asc ";
            bind_grids(query);
        }

        private void bind_grids(string query)
        {
            DataTable dt1 = mycode.FillData(query);
            if (dt1.Rows.Count == 0)
            {
                Alertme("Sorry there are no records exist ", "warning");
                grd_studentdetails.DataSource = null;
                grd_studentdetails.DataBind();
            }
            else
            {
                grd_studentdetails.DataSource = dt1;
                grd_studentdetails.DataBind();
                try
                {
                    ddl_course.SelectedValue = dt1.Rows[0]["Class_id"].ToString();
                    mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor where Session_id='" + ddl_session.SelectedValue + "' and Class_id='" + ddl_course.SelectedValue + "' order by Section ");
                    ddl_section.Text = dt1.Rows[0]["Section"].ToString();
                }
                catch (Exception ex)
                { 
                }
                
            }
        }

        protected void lnk_position_Click(object sender, EventArgs e)
        {
            try
            {
                featch_by_acs_desc();
            }
            catch (Exception exc)
            {
            }
        }

        private void featch_by_acs_desc()
        {
            if (ViewState["flag"].ToString() == "1")
            {
                if (ViewState["flagPosition"].ToString() == "1")
                {
                    string query = "Select * from admission_registor where session='" + ddl_session.SelectedItem.Text + "' and Section='" + ddl_section.Text + "' and Class_id=" + ddl_course.SelectedValue + "   and (Transfer_Status='New' or Transfer_Status='NT') and  Status='1' order by studentname asc";
                    bind_grids(query);
                    ViewState["flagPosition"] = "0";
                }
                else
                {
                    string query = "Select * from admission_registor where session='" + ddl_session.SelectedItem.Text + "' and Section='" + ddl_section.Text + "' and Class_id=" + ddl_course.SelectedValue + "   and (Transfer_Status='New' or Transfer_Status='NT') and Status='1'  order by studentname desc";
                    bind_grids(query);
                    ViewState["flagPosition"] = "1";
                }
            }
        }

        #region row databound
        protected void grd_studentdetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk_transferwithtrasport = (CheckBox)e.Row.FindControl("chk_transferwithtrasport");
                CheckBox chk_transfer_with_hostel = (CheckBox)e.Row.FindControl("chk_transfer_with_hostel");
                Label lbl_is_trasport = (Label)e.Row.FindControl("lbl_is_trasport");
                Label lbl_is_hostel = (Label)e.Row.FindControl("lbl_is_hostel");

                if (lbl_is_trasport.Text.ToUpper() == "YES")
                {
                    chk_transferwithtrasport.Checked = true;
                    chk_transfer_with_hostel.Enabled = true;

                    chk_transfer_with_hostel.Checked = false;
                    chk_transfer_with_hostel.Enabled = false;
                }
                else
                {
                    chk_transferwithtrasport.Checked = false;
                    chk_transferwithtrasport.Enabled = false;
                    chk_transfer_with_hostel.Checked = false;
                    chk_transfer_with_hostel.Enabled = false;
                    if (lbl_is_hostel.Text.ToUpper() == "YES")
                    {
                        chk_transfer_with_hostel.Checked = true;
                        chk_transfer_with_hostel.Enabled = true;
                    }

                }



            }
        }
        #endregion

        protected void ddl_course_SelectedIndexChanged1(object sender, EventArgs e)
        {
            try
            {
                mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor where Session_id='" + ddl_session.SelectedValue + "' and Class_id='" + ddl_course.SelectedValue + "' order by Section ");
                
            }
            catch (Exception ex)
            {
            }
        }

        // dues find

        private double find_admission_dues(string session_id, string hosteltaken, string admissionserialnumber, string session, string class_id, string Transfer_Status, string Hostel_id, string category_id, string SubCategory_id)
        {
            double totalpay = 0;
            string parameter = ""; string parameter2 = "";
            string parameter_id = "";

            string Discount_on = "";

            if (Transfer_Status == "New")
            {
                parameter2 = "AdmissionFee";
                parameter = hosteltaken.ToUpper() == "NO" ? "AdmissionFee" : "HostelAdmissionFee";
                if (parameter == "AdmissionFee")
                {
                    parameter2 = "HostelAdmissionFee";
                }

                parameter_id = hosteltaken.ToString().ToUpper() == "NO" ? "1" : "5";
                ViewState["parameter"] = parameter;
                Discount_on = "Admission";
            }
            else
            {
                parameter2 = "AnnualFee";
                parameter = hosteltaken.ToUpper() == "NO" ? "AnnualFee" : "HostelAnnualFee";
                if (parameter == "AnnualFee")
                {
                    parameter2 = "HostelAnnualFee";
                }
                parameter_id = hosteltaken.ToUpper() == "NO" ? "2" : "6";
                ViewState["parameter"] = parameter;
                Discount_on = "Annual";
            }
            string qry = "select *,(payable-cast(disc_amount as float)-cast(paid as float)) net_payable from(select '0' payable_after_disc,session,feetype,cast(payable as float) payable,paid,dues,status,content_id, isnull((Disc),0) disc_amount from dbo.[Typewise_fee_collection] WHERE admission_no='" + admissionserialnumber + "' and (parameter='" + parameter + "' or parameter='" + parameter2 + "') and session='" + session + "')t";
            DataTable fee_dt = My.dataTable(qry);
            if (fee_dt.Rows.Count == 0)
            {
                if (Hostel_id == "0")
                {
                    qry = "select *,(payable-cast(disc_amount as float)) net_payable from(select '0' payable_after_disc,fmc.session,cm.content feetype,cast(fmc.amount as float) payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where    admission_no='" + admissionserialnumber + "' and   parameter_id='" + parameter_id + "' and session='" + session + "' and fee_head_id=cm.content_id and Discount_on='" + Discount_on + "' ),(select top 1 disc_amount from dbo.[Discount_Master] where  Class_id='" + class_id + "' and admission_no='All'  and parameter_id='" + parameter_id + "' and session='" + session + "' and fee_head_id=cm.content_id and Discount_on='" + Discount_on + "' and category_id='" + category_id + "' and sub_category_id='" + SubCategory_id + "'))),0) disc_amount   from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='" + parameter + "'  and session='" + session + "' and class_id='" + class_id + "' )t";
                }
                else
                {
                    qry = "select *,(payable-cast(disc_amount as float)) net_payable from(select '0' payable_after_disc,fmc.session,cm.content feetype,cast(fmc.amount as float) payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where   Hostel_Id=" + Hostel_id + " and admission_no='" + admissionserialnumber + "'  and parameter_id='" + parameter_id + "' and session='" + session + "' and fee_head_id=cm.content_id and Discount_on='Admission' ),(select top 1 disc_amount from dbo.[Discount_Master] where Hostel_Id=" + Hostel_id + " and Class_id='" + class_id + "' and admission_no='All'  and parameter_id='" + parameter_id + "' and session='" + session + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + category_id + "' and sub_category_id='" + SubCategory_id + "'))),0) disc_amount   from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='" + parameter + "'  and session='" + session + "' and class_id='" + class_id + "' and fmc.Hostel_Id='" + Hostel_id + "')t";
                }
                fee_dt = My.dataTable(qry);
            }
            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                double payable = 0, paid = 0, dues = 0, disc = 0, payble_after_disc = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    dr["payable_after_disc"] = My.toDouble(dr["payable"]) - My.toDouble(dr["disc_amount"]) - My.toDouble(dr["paid"]);
                    payable += My.toDouble(dr["payable"]);
                    paid += My.toDouble(dr["paid"]);
                    dues += My.toDouble(dr["dues"]);
                    disc += My.toDouble(dr["disc_amount"]);
                    payble_after_disc += My.toDouble(dr["payable_after_disc"]);
                }
                string previous_dues = get_previous_amount(admissionserialnumber, session_id, class_id);
                totalpay = payble_after_disc + My.toDouble(previous_dues);

            }

            return totalpay;


        }

        private string get_previous_amount(string regid, string Session_id, string Class_id)
        {

            DataTable dt = mycode.FillData("select Dues_Amount from Previous_Year_Dues  where Session_id='" + Session_id + "' and AdmissionNumber='" + regid + "'  and Class_id='" + Class_id + "' and   Status='Unpaid'");
            if (dt.Rows.Count == 0)
            {
                return "0.00";
            }
            else
            {

                return dt.Rows[0][0].ToString();
            }
        }

        private double find_monthley_dues(string session_id, string hosteltaken, string regid, string session, string class_id, string transfer_Status, string classname, string Hostel_id, string transportationtaken, bool day_bording, bool day_bording_with_lunch, string category_id, string SubCategory_id, string Transportation_Id, string Section, string Branch_id, string Room_Category_id, string Hostel_assign_id)
        {
            string parameter = ""; string parameter2 = "MonthlyFee";
            string parameter_id = "";

            string Discount_on = "";
            if (transfer_Status == "New")
            {
                parameter = hosteltaken.ToUpper() == "NO" ? "MonthlyFee" : "HostelMonthlyFee";
                if (parameter == "MonthlyFee")
                {
                    parameter2 = "HostelMonthlyFee";
                }
                ViewState["parameter"] = parameter;
                Discount_on = "Admission";
                parameter_id = hosteltaken.ToUpper() == "NO" ? "1" : "5";
            }
            else
            {
                parameter = hosteltaken.ToUpper() == "NO" ? "MonthlyFee" : "HostelMonthlyFee";
                if (parameter == "MonthlyFee")
                {
                    parameter2 = "HostelMonthlyFee";
                }
                parameter_id = hosteltaken.ToUpper() == "NO" ? "2" : "6";
                ViewState["parameter"] = parameter;
                Discount_on = "Annual";



            }

            double total = 0;

            mycode.executequery("delete from Typewise_fee_collection_temp_dues_calculation where admission_no='" + regid + "' and session='" + session + "'");
            List<string> month_lst = new List<string>();
            string slipno = "", entry_id = "";
            DataTable dt = mycode.FillData("Select Month,Month_Id from Month_Index   order by Position asc ");//My.FillDatastatic("Select Month from Month_Index where Position='1'  ");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                for (int iS = 0; iS < dt.Rows.Count; iS++)
                {
                    DataTable feedt = new DataTable();
                    string monthname = dt.Rows[iS]["Month"].ToString();
                    if (ViewState["IsBoarding"].ToString() == "1")
                    {
                        int mnthids = My.tomonth_number(monthname);
                        if (My.toint(ViewState["LunchMnthId"].ToString()) <= mnthids)
                        {
                            ViewState["parameteridS"] = "44";
                        }
                        else
                        {
                            ViewState["parameteridS"] = "4";
                        }
                    }


                    string type = "";
                    month_lst.Add(monthname);
                    if (My.dataTable("select  * from dbo.[Typewise_fee_collection]   where admission_no='" + regid + "' and session='" + session + "' and month='" + monthname + "' and (parameter='" + parameter + "' or parameter='" + parameter2 + "')").Rows.Count > 0)
                    {
                        feedt = My.dataTable("select id, admission_no,class, session,parameter, feetype content,payable amount,'0'Column1,payable amount,'0'Column2,month months,'0'content_id,'0'Ledger,'0'Column3,'0'Column4,'0'Column5,Disc as disc_amount,isnull(paid,'0') previously_paid from dbo.[Typewise_fee_collection]   where admission_no='" + regid + "' and session='" + session + "' and month='" + monthname + "' and (parameter='" + parameter + "' or parameter='" + parameter2 + "') and content_id!='6121' and status='Dues'");
                        type = "Calculated";
                        if (feedt.Rows.Count > 0)
                        {
                            send_in_typewise_fee(feedt, Section, regid, monthname, Branch_id, session_id);
                        }
                    }
                    else
                    {
                        Dictionary<string, object> dc = new Dictionary<string, object>();
                        dc["admission_no"] = regid;
                        dc["session_id"] = session_id;
                        dc["class"] = classname;
                        dc["session"] = session;
                        dc["class_id"] = class_id;
                        dc["hosteltaken"] = hosteltaken;
                        dc["months"] = monthname;
                        dc["tr_ledger"] = My.is_combine ? "School" : "Transport";
                        dc["hostel_id"] = Hostel_id;
                        dc["Room_Category_id"] = Room_Category_id;
                        dc["Hostel_assig_id"] = Hostel_assign_id;

                        dc["day_boarding"] = day_bording;
                        dc["day_boarding_lunch"] = day_bording_with_lunch;

                        dc["category_id"] = category_id;
                        dc["sub_category_id"] = SubCategory_id;

                        dc["TransportationPath_id"] = ViewState["TransportPath_id"].ToString();
                        dc["transportportation_id"] = ViewState["Transport_id"].ToString();
                        dc["Boarding_Point_id"] = ViewState["Boarding_Point_id"].ToString();
                        dc["parameter_id"] = ViewState["parameteridS"].ToString();


                        string cunrt_session = session;
                        string[] stringSeparators = new string[] { "-" };
                        string[] arr = cunrt_session.Split(stringSeparators, StringSplitOptions.None);
                        string session_frst_year = arr[0];
                        string session_last_year = arr[1];
                        int session_s_year = My.toint(session_frst_year);
                        int s_year = My.toint(session_frst_year);
                        string monthid = My.tomonth_numberstring(monthname);
                        int pay_month = My.toint(monthid);
                        s_year = My.check_start_months(pay_month, s_year);
                        dc["monthid"] = s_year + monthid;
                        feedt = My.dataTableSP("sp_fetch_monthly_fee", dc);
                        feedt.Columns.Add("previously_paid");
                        send_in_typewise_fee(feedt, Section, regid, monthname, Branch_id, session_id);
                    }
                }

                string month = "";
                double fee = 0, disc = 0, paid_prev = 0;
                double total_payable = 0;
                string late_fine_month = "", month_position = "";
                string qry = " select *  from Typewise_fee_collection_temp_dues_calculation  where admission_no='" + regid + "' and session='" + session + "' and status='Dues' and parameter like '%" + parameter + "%' and branchid='" + Branch_id + "' order by cast(Position as float)";

                SqlDataAdapter ad = new SqlDataAdapter(qry, My.con);
                DataSet ds = new DataSet();
                ad.Fill(ds, "Typewise_fee_collection");
                DataTable tdt = ds.Tables[0];
                if (tdt.Rows.Count == 0)
                {
                }
                else
                {
                    foreach (DataRow dr in tdt.Rows)
                    {
                        month = dr["month"].ToString();
                        total_payable = My.toDouble(dr["payable"]) - My.toDouble(dr["Disc"]) - My.toDouble(dr["previously_paid"]);

                        total += My.toDouble(total_payable);
                    }
                }
            }
            return total;
        }

        private void send_in_typewise_fee(DataTable feedt, string Section, string regid, string monthname, string Branch_id, string Session_id)
        {
            double fine_amt = 0;
            double fine = My.toDouble(fine_amt);
            if (fine > 0)
            {
                int mnth_idss = My.tomonth_number(monthname);
                string month_id = My.getMonthS_twoDigit(mnth_idss.ToString());
                DataTable dt = mycode.FillData("select Fine_amount from Temp_fine_monthwise where Session_id='" + Session_id + "' and Admission_no='" + feedt.Rows[0]["admission_no"].ToString() + "' and Month_id='" + month_id + "' and Branch_id='" + Branch_id + "'");
                if (dt.Rows.Count > 0)
                {
                    fine = My.toDouble(dt.Rows[0]["Fine_amount"].ToString());
                    My.exeSql("insert into Typewise_fee_collection(admission_no,class,session,parameter,Date,idate,feetype,payable,paid,dues,status,month,content_id,transection,Ledger,is_readyfor_sync,is_sync,is_sync_dues_diary,Disc,section,user_id,branchid) values ('" + feedt.Rows[0]["admission_no"].ToString() + "','" + feedt.Rows[0]["class"].ToString() + "','" + feedt.Rows[0]["session"].ToString() + "','" + feedt.Rows[0]["parameter"].ToString() + "','" + mycode.date() + "','" + mycode.idate() + "','Late Fine','" + My.toDouble(fine).ToString("0.00") + "','0','" + My.toDouble(fine).ToString("0.00") + "','Dues','" + monthname + "','6121','','School','false','false','false','0.00','" + Section + "','" + regid + "','" + Branch_id + "')");
                }
            }
            bool entrys = false;
            foreach (DataRow dr in feedt.Rows)
            {
                if (My.dataTable("select  * from dbo.[Typewise_fee_collection]   where admission_no='" + regid + "' and session='" + Session + "' and month='" + dr["months"].ToString() + "' and parameter='" + dr["parameter"].ToString() + "'").Rows.Count == 0)
                {
                    double paidAmt = My.toDouble(dr["previously_paid"].ToString()) + My.toDouble(dr["disc_amount"].ToString());
                    if (My.toDouble(dr["amount"].ToString()) > paidAmt)
                    {
                        My.exeSql("insert into Typewise_fee_collection_temp_dues_calculation(admission_no,class,session,parameter,Date,idate,feetype,payable,paid,dues,status,month,content_id,transection,Ledger,is_readyfor_sync,is_sync,is_sync_dues_diary,Disc,section,user_id,branchid,previously_paid) values ('" + dr["admission_no"].ToString() + "','" + dr["class"].ToString() + "','" + dr["session"].ToString() + "','" + dr["parameter"].ToString() + "','" + mycode.date() + "','" + mycode.idate() + "','" + dr["content"].ToString() + "','" + My.toDouble(dr["amount"].ToString()).ToString("0.00") + "','0','" + My.toDouble(dr["amount"].ToString()).ToString("0.00") + "','Dues','" + dr["months"].ToString() + "','" + dr["content_id"].ToString() + "','','School','false','false','false','" + My.toDouble(dr["disc_amount"].ToString()).ToString("0.00") + "','" + Section + "','" + regid + "','" + Branch_id + "','" + My.toDouble(dr["previously_paid"].ToString()) + "')");
                    }
                }
                else
                {
                }
            }
        }
    }
}