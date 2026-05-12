using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class Student_Transfer_Day_to_Hostel : System.Web.UI.Page
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
                        mycode.bind_all_ddl_with_id(ddl_session, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                        mycode.bind_all_ddl_with_id(ddl_session_name, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");

                        ddl_session.SelectedValue = My.get_session_id();
                        ddl_session_name.SelectedValue = ddl_session.SelectedValue;

                        Session["classchange"] = "2";
                        ViewState["Branch_id"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        mycode.bind_all_ddl_with_id_no_select(ddl_room_cat, "select Category_name,Category_id from Hostel_room_category_master order by Category_name asc");
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Student_Transfer_Day_to_Hostel");
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

        #region find student data
        protected void btn_find_admission_no_Click(object sender, EventArgs e)
        {
            try
            {

                find_data();



            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }

        private void find_data()
        {
            if (txt_admission_no.Text == "")
            {
                Alertme("Please select session", "warning");
            }
            else if (txt_admission_no.Text == "")
            {
                Alertme("Please enter  current admission no.", "warning");
            }
            else
            {
                ViewState["admissionserialnumber"] = txt_admission_no.Text;
                bool checknotsend = chek_data();

                if (checknotsend == true)
                {
                    string query = "select * from admission_registor where admissionserialnumber='" + txt_admission_no.Text + "' and Session_id='" + ddl_session.SelectedValue + "' and Status='1'";
                    find_details(query);
                }
                else
                {
                    Alertme("This student already in the hostel portal", "warning");
                }
            }
        }

        private void find_details(string query)
        {
            SqlDataAdapter ad_contactus = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad_contactus.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                std_basic_infoS.Visible = false;
                Alertme("Student details not found...", "warning");
                return;
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    std_basic_infoS.Visible = true;
                    lbl_name.Text = dr["studentname"].ToString();
                    lbl_father_name.Text = dr["fathername"].ToString();
                    lblclass.Text = dr["class"].ToString();
                    ViewState["class_id"] = dr["Class_id"].ToString();
                    txtsection.Text = dr["Section"].ToString();
                    lbl_admission_no.Text = dr["admissionserialnumber"].ToString();
                    txt_admission_no.Text = dr["admissionserialnumber"].ToString();
                    lblhostel.Text = dr["hosteltaken"].ToString();
                    ViewState["parameter"] = dr["hosteltaken"].ToString() == "yes" ? "HostelMonthlyFee" : "MonthlyFee";
                    ViewState["parameter_id"] = dr["hosteltaken"].ToString() == "yes" ? "3" : "4";
                    lbltransporttion.Text = dr["transportationtaken"].ToString();
                    lbl_phone.Text = dr["mobilenumber"].ToString();
                    ViewState["hostel_id"] = My.toint(dr["Hostel_id"].ToString());
                    ViewState["day_bording"] = My.toBool(dr["is_applied_dayboarding"]);
                    ViewState["day_bording_with_lunch"] = My.toBool(dr["day_boarding_with_lunch"]);
                    ViewState["group_id"] = "3";
                    ViewState["category_id"] = dr["category_id"].ToString();
                    ViewState["sub_category_id"] = dr["SubCategory_id"].ToString();
                    ViewState["classid"] = dr["Class_id"].ToString();
                    ViewState["Section"] = dr["Section"].ToString();
                    ViewState["sessionIDs"] = dr["Session_id"].ToString();
                    ViewState["admissionserialnumber"] = dr["admissionserialnumber"].ToString();
                    ViewState["session"] = dr["session"].ToString();
                    ViewState["id"] = dr["id"].ToString();
                    lbl_old_roll_no.Text = dr["rollnumber"].ToString();

                    //

                    mycode.bind_all_ddl_with_id_otherportal(ddl_hostel, "select DISTINCT Hostel_name,Hostel_id from (select t1.Hostel_name,t1.Hostel_id,t2.Category_id,t2.Room_id,t2.Bed_id from Hostels_master t1 join HOSTEL_ROOM_BED_MASTER t2 on t1.Hostel_id=t2.Hostel_id where t2.Bed_id not in (select Bed_id from Hostel_assign_master where  Room_id=t2.Room_id and Status='1' and Hostel_id=t2.Hostel_id and Category_id=t2.Category_id)) t order by Hostel_name asc");

                    // mycode.bind_all_ddl_with_id(ddl_room, "select Room_name,Room_id from Hostel_room_master where Hostel_id='" + ddl_hostel.SelectedValue + "' and Category_id='" + ddl_room_cat.SelectedValue + "' order by Room_name asc");
                    //string room_id = My.top_one_hostel_room(ddl_hostel.SelectedValue, ddl_room_cat.SelectedValue);
                    //  try
                    // {
                    //  ddl_room.SelectedValue = room_id; fetch_bed_details();
                    // }
                    // catch (Exception ex)
                    //  {
                    //  }


                }

            }
        }
        protected void ddl_hostel_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (ddl_hostel.SelectedItem.Text == "Select")
                {
                    ddl_hostel.Focus();
                    Alertme("Please choose hostel.", "warning");
                }
                else
                {
                    mycode.bind_all_ddl_with_id_otherportal(ddl_room, "select Room_name,Room_id from Hostel_room_master where Hostel_id='" + ddl_hostel.SelectedValue + "' and Category_id='" + ddl_room_cat.SelectedValue + "' order by Room_name asc");
                    string room_id = My.top_one_hostel_room_HDB(ddl_hostel.SelectedValue, ddl_room_cat.SelectedValue);
                    try
                    {
                        ddl_room.SelectedValue = room_id;
                        fetch_bed_details();
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_assign_student");
            }
        }
        private void fetch_bed_details()
        {
            mycode.bind_all_ddl_with_id_otherportal(ddl_bed, "select 'Bed No. :'+Bed_name+', Bed Position '+Bed_Position,Bed_id from Hostel_room_bed_master where Bed_id not in (select Bed_id from Hostel_assign_master where  Room_id='" + ddl_room.SelectedValue + "' and Status='1' and Hostel_id=" + ddl_hostel.SelectedValue + " and Category_id=" + ddl_room_cat.SelectedValue + " and Session_id='" + ViewState["sessionIDs"].ToString() + "') and Room_id='" + ddl_room.SelectedValue + "' and Hostel_id='" + ddl_hostel.SelectedValue + "'and Category_id='" + ddl_room_cat.SelectedValue + "' order by Id asc");
            string bed_id = My.get_top_on_bed_HDB(ddl_hostel.SelectedValue, ddl_room_cat.SelectedValue, ddl_room.SelectedValue, ViewState["sessionIDs"].ToString());
            try
            {
                ddl_bed.SelectedValue = bed_id;
            }
            catch (Exception ex)
            {
            }
        }

        protected void ddl_room_cat_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (ddl_hostel.SelectedItem.Text == "Select")
                {
                    ddl_hostel.Focus();
                    Alertme("Please choose hostel.", "warning");
                }
                else if (ddl_room_cat.SelectedItem.Text == "Select")
                {
                    ddl_room_cat.Focus();
                    Alertme("Please choose category.", "warning");
                }
                else
                {
                    fetch_rooms();
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_assign_student");
            }
        }
        private void fetch_rooms()
        {
            mycode.bind_all_ddl_with_id_otherportal(ddl_room, "select Room_name,Room_id from Hostel_room_master where Hostel_id='" + ddl_hostel.SelectedValue + "' and Category_id='" + ddl_room_cat.SelectedValue + "' order by Room_name asc");
            string room_id = My.top_one_hostel_room_HDB(ddl_hostel.SelectedValue, ddl_room_cat.SelectedValue);
            try
            {
                ddl_room.SelectedValue = room_id; fetch_bed_details();
            }
            catch (Exception ex)
            {
            }
        }
        protected void ddl_room_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (ddl_hostel.SelectedItem.Text == "Select")
                {
                    Alertme("Please choose hostel.", "warning");
                    ddl_hostel.Focus();
                }
                else if (ddl_room_cat.SelectedItem.Text == "Select")
                {
                    Alertme("Please choose category.", "warning");
                    ddl_room_cat.Focus();
                }
                else if (ddl_room.SelectedItem.Text == "Select")
                {
                    Alertme("Please choose room.", "warning");
                    ddl_room.Focus();
                }
                else
                {
                    fetch_bed_details();
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_assign_student");
            }
        }



        #endregion

        protected void btn_trasfer_student_Click(object sender, EventArgs e)
        {
            if (ddl_hostel.SelectedItem.Text == "Select")
            {
                Alertme("Please choose hostel.", "warning");
                ddl_hostel.Focus();
            }
            else if (ddl_room_cat.SelectedItem.Text == "Select")
            {
                Alertme("Please choose category.", "warning");
                ddl_room_cat.Focus();
            }
            else if (ddl_room.SelectedItem.Text == "Select")
            {
                Alertme("Please choose room.", "warning");
                ddl_room.Focus();
            }
            else if (ddl_bed.SelectedItem.Text == "Select")
            {
                Alertme("Please choose bed.", "warning");
                ddl_room.Focus();
            }
            else
            {
                finl_send_data();
            }
        }

        private void finl_send_data()
        {
            bool checknotsend = chek_data();

            if (checknotsend == true)
            {
                string query = "select  * from admission_registor where session='" + ddl_session.SelectedItem.Text + "' and admissionserialnumber='" + ViewState["admissionserialnumber"].ToString() + "'";
                DataTable dt = My.dataTable(query);
                if (dt.Rows.Count == 0)
                {

                }
                else
                {
                    bool flag = false;
                    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
                    {
                        SqlConnection con = new SqlConnection(My.hostelDB);
                        con.Open();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            SqlCommand cmd;
                            string query1 = "INSERT INTO admission_registor (Transfer_Status,Category_id,SubCategory_id,formserialnumber,UID_No,Index_no,dateofadmission,admission_idate,session,is_applied_dayboarding,day_boarding_with_lunch,class,rollnumber,Section,admissionserialnumber,house,hosteltaken,studentname,dob,place_of_birth,Is_birth_certificate,birth_certificate_number,gender,blood_group,Student_nationality,religion,ration_type,cast,Is_cast_certificate,cast_certificate_no,aadharno,student_mother_tounge,is_illness,illness_remark,Staff_employee_code,RTE,staff_ward,Staff_name,Personal_Identymarks,identifacationmark,Prev_school_name,currentschool,Old_Admission_Date,OLd_Admission_Idate,Prev_board_type,Prev_board,Old_class_id,Prev_percentage,Prev_reason_for_shift,Prev_year,fathername,occuption,fatherqualification,f_nationality,f_marrital_statue,Country_Code_Father,father_mob,Father_whatsapp_country_code,Father_whatsApp_no,email_id,guardianname,parentincome,mothername,m_occupation,motherqualifiaction,m_nationality,m_marrital_statue,Country_Code_Mother,mother_mob,Mother_whatsapp_country_code,Mother_whatsApp_no,mother_email,careof,postoffice,policestation,district,city,state,pin,Present_country,Country_Code_Current_add,mobilenumber,careof_permanent,postoffice_permanent,policestation_permanent,district_permanent,city_permanent,state_permanent,pincode,Permanent_country,Country_Code_Current_Perm_add,mob2,Bank_acount_no,Account_Holder_name,Bnk_Name,IFSC_Code,Branch_Name,payment_status,Hostel_id,Session_id,Class_id,Is_TC_Taken,Student_id,Branch_id,Student_Name_First,Student_Middle_Name,Student_Name_Last,Father_Name_First,Father_Name_Middle,Father_Name_Last,Mother_Name_First,Mother_Name_Middle,Mother_Name_Last,StudentStatus,Pwd,Verification_Istatus,Status,College_School_Name,relation,transportationtaken,Father_aadhar_no,Mother_aadhar_no,Height,Weight,Sibling_name1,Sibling_age1,Sibling_school1,Sibling_class1,Sibling_name2,Sibling_age2,Sibling_school2,Sibling_class2,Created_by,Created_date,Created_time,Created_idate,Student_pen_no,jati,Hobbie_of_student,Prev_class_attended,Prev_pass_fail_status,Father_age,Mother_age,Mother_annual_income,Guardian_relation_with_student,Guardian_occupation,Guardian_qualification,Guardian_contry_code,Guardian_mobile_no,Guardian_aadhar_no,Guardian_annual_income,Guardian_address,studentimagepath) values (@Transfer_Status,@Category_id,@SubCategory_id,@formserialnumber,@UID_No,@Index_no,@dateofadmission,@admission_idate,@session,@is_applied_dayboarding,@day_boarding_with_lunch,@class,@rollnumber,@Section,@admissionserialnumber,@house,@hosteltaken,@studentname,@dob,@place_of_birth,@Is_birth_certificate,@birth_certificate_number,@gender,@blood_group,@Student_nationality,@religion,@ration_type,@cast,@Is_cast_certificate,@cast_certificate_no,@aadharno,@student_mother_tounge,@is_illness,@illness_remark,@Staff_employee_code,@RTE,@staff_ward,@Staff_name,@Personal_Identymarks,@identifacationmark,@Prev_school_name,@currentschool,@Old_Admission_Date,@OLd_Admission_Idate,@Prev_board_type,@Prev_board,@Old_class_id,@Prev_percentage,@Prev_reason_for_shift,@Prev_year,@fathername,@occuption,@fatherqualification,@f_nationality,@f_marrital_statue,@Country_Code_Father,@father_mob,@Father_whatsapp_country_code,@Father_whatsApp_no,@email_id,@guardianname,@parentincome,@mothername,@m_occupation,@motherqualifiaction,@m_nationality,@m_marrital_statue,@Country_Code_Mother,@mother_mob,@Mother_whatsapp_country_code,@Mother_whatsApp_no,@mother_email,@careof,@postoffice,@policestation,@district,@city,@state,@pin,@Present_country,@Country_Code_Current_add,@mobilenumber,@careof_permanent,@postoffice_permanent,@policestation_permanent,@district_permanent,@city_permanent,@state_permanent,@pincode,@Permanent_country,@Country_Code_Current_Perm_add,@mob2,@Bank_acount_no,@Account_Holder_name,@Bnk_Name,@IFSC_Code,@Branch_Name,@payment_status,@Hostel_id,@Session_id,@Class_id,@Is_TC_Taken,@Student_id,@Branch_id,@Student_Name_First,@Student_Middle_Name,@Student_Name_Last,@Father_Name_First,@Father_Name_Middle,@Father_Name_Last,@Mother_Name_First,@Mother_Name_Middle,@Mother_Name_Last,@StudentStatus,@Pwd,@Verification_Istatus,@Status,@College_School_Name,@relation,@transportationtaken,@Father_aadhar_no,@Mother_aadhar_no,@Height,@Weight,@Sibling_name1,@Sibling_age1,@Sibling_school1,@Sibling_class1,@Sibling_name2,@Sibling_age2,@Sibling_school2,@Sibling_class2,@Created_by,@Created_date,@Created_time,@Created_idate,@Student_pen_no,@jati,@Hobbie_of_student,@Prev_class_attended,@Prev_pass_fail_status,@Father_age,@Mother_age,@Mother_annual_income,@Guardian_relation_with_student,@Guardian_occupation,@Guardian_qualification,@Guardian_contry_code,@Guardian_mobile_no,@Guardian_aadhar_no,@Guardian_annual_income,@Guardian_address,@studentimagepath)";
                            cmd = new SqlCommand(query1);

                            cmd.Parameters.AddWithValue("@studentimagepath", dt.Rows[i]["studentimagepath"].ToString());
                            cmd.Parameters.AddWithValue("@Hobbie_of_student", dt.Rows[i]["Hobbie_of_student"].ToString());
                            cmd.Parameters.AddWithValue("@Prev_class_attended", dt.Rows[i]["Prev_class_attended"].ToString());
                            cmd.Parameters.AddWithValue("@Prev_pass_fail_status", dt.Rows[i]["Prev_pass_fail_status"].ToString());
                            cmd.Parameters.AddWithValue("@Father_age", dt.Rows[i]["Father_age"].ToString());
                            cmd.Parameters.AddWithValue("@Mother_age", dt.Rows[i]["Mother_age"].ToString());
                            cmd.Parameters.AddWithValue("@Mother_annual_income", dt.Rows[i]["Mother_annual_income"].ToString());
                            cmd.Parameters.AddWithValue("@Guardian_relation_with_student", dt.Rows[i]["Guardian_relation_with_student"].ToString());
                            cmd.Parameters.AddWithValue("@Guardian_occupation", dt.Rows[i]["Guardian_occupation"].ToString());
                            cmd.Parameters.AddWithValue("@Guardian_qualification", dt.Rows[i]["Guardian_qualification"].ToString());
                            cmd.Parameters.AddWithValue("@Guardian_contry_code", dt.Rows[i]["Guardian_contry_code"].ToString());
                            cmd.Parameters.AddWithValue("@Guardian_mobile_no", dt.Rows[i]["Guardian_mobile_no"].ToString());
                            cmd.Parameters.AddWithValue("@Guardian_aadhar_no", dt.Rows[i]["Guardian_aadhar_no"].ToString());
                            cmd.Parameters.AddWithValue("@Guardian_annual_income", dt.Rows[i]["Guardian_annual_income"].ToString());
                            cmd.Parameters.AddWithValue("@Guardian_address", dt.Rows[i]["Guardian_address"].ToString());



                            cmd.Parameters.AddWithValue("@Transfer_Status", dt.Rows[i]["Transfer_Status"].ToString());

                            cmd.Parameters.AddWithValue("@Category_id", dt.Rows[i]["Category_id"].ToString());//--
                            cmd.Parameters.AddWithValue("@SubCategory_id", dt.Rows[i]["SubCategory_id"].ToString());//--

                            cmd.Parameters.AddWithValue("@formserialnumber", dt.Rows[i]["formserialnumber"].ToString());
                            cmd.Parameters.AddWithValue("@UID_No", dt.Rows[i]["UID_No"].ToString());
                            cmd.Parameters.AddWithValue("@Index_no", dt.Rows[i]["Index_no"].ToString());

                            cmd.Parameters.AddWithValue("@Student_pen_no", dt.Rows[i]["Student_pen_no"].ToString());
                            cmd.Parameters.AddWithValue("@dateofadmission", dt.Rows[i]["dateofadmission"].ToString());
                            cmd.Parameters.AddWithValue("@admission_idate", dt.Rows[i]["admission_idate"].ToString());
                            cmd.Parameters.AddWithValue("@session", dt.Rows[i]["session"].ToString());//--

                            cmd.Parameters.AddWithValue("@hosteltaken", "Yes");



                            cmd.Parameters.AddWithValue("@is_applied_dayboarding", false);
                            cmd.Parameters.AddWithValue("@day_boarding_with_lunch", false);

                            cmd.Parameters.AddWithValue("@class", dt.Rows[i]["class"].ToString());


                            cmd.Parameters.AddWithValue("@rollnumber", dt.Rows[i]["rollnumber"].ToString());
                            cmd.Parameters.AddWithValue("@Section", dt.Rows[i]["Section"].ToString());

                            cmd.Parameters.AddWithValue("@admissionserialnumber", dt.Rows[i]["admissionserialnumber"].ToString());
                            cmd.Parameters.AddWithValue("@house", dt.Rows[i]["house"].ToString());


                            //Student Info 
                            cmd.Parameters.AddWithValue("@studentname", dt.Rows[i]["studentname"].ToString());
                            cmd.Parameters.AddWithValue("@dob", dt.Rows[i]["dob"].ToString());
                            cmd.Parameters.AddWithValue("@place_of_birth", dt.Rows[i]["place_of_birth"].ToString());
                            cmd.Parameters.AddWithValue("@Is_birth_certificate", dt.Rows[i]["Is_birth_certificate"].ToString());
                            cmd.Parameters.AddWithValue("@birth_certificate_number", dt.Rows[i]["birth_certificate_number"].ToString());
                            cmd.Parameters.AddWithValue("@gender", dt.Rows[i]["gender"].ToString());
                            cmd.Parameters.AddWithValue("@blood_group", dt.Rows[i]["blood_group"].ToString());
                            cmd.Parameters.AddWithValue("@Student_nationality", dt.Rows[i]["Student_nationality"].ToString());
                            cmd.Parameters.AddWithValue("@religion", dt.Rows[i]["religion"].ToString());
                            cmd.Parameters.AddWithValue("@ration_type", dt.Rows[i]["ration_type"].ToString());
                            cmd.Parameters.AddWithValue("@cast", dt.Rows[i]["cast"].ToString());
                            cmd.Parameters.AddWithValue("@jati", dt.Rows[i]["jati"].ToString());
                            cmd.Parameters.AddWithValue("@Is_cast_certificate", dt.Rows[i]["Is_cast_certificate"].ToString());
                            cmd.Parameters.AddWithValue("@cast_certificate_no", dt.Rows[i]["cast_certificate_no"].ToString());
                            cmd.Parameters.AddWithValue("@aadharno", dt.Rows[i]["aadharno"].ToString());
                            cmd.Parameters.AddWithValue("@student_mother_tounge", dt.Rows[i]["student_mother_tounge"].ToString());
                            cmd.Parameters.AddWithValue("@is_illness", dt.Rows[i]["is_illness"].ToString());
                            cmd.Parameters.AddWithValue("@illness_remark", dt.Rows[i]["illness_remark"].ToString());
                            cmd.Parameters.AddWithValue("@Staff_employee_code", dt.Rows[i]["Staff_employee_code"].ToString());
                            cmd.Parameters.AddWithValue("@RTE", dt.Rows[i]["RTE"].ToString());
                            cmd.Parameters.AddWithValue("@staff_ward", dt.Rows[i]["staff_ward"].ToString());
                            cmd.Parameters.AddWithValue("@Staff_name", dt.Rows[i]["Staff_name"].ToString());
                            cmd.Parameters.AddWithValue("@Personal_Identymarks", dt.Rows[i]["Personal_Identymarks"].ToString());
                            cmd.Parameters.AddWithValue("@identifacationmark", dt.Rows[i]["identifacationmark"].ToString());





                            cmd.Parameters.AddWithValue("@Height", dt.Rows[i]["Height"].ToString());
                            cmd.Parameters.AddWithValue("@Weight", dt.Rows[i]["Weight"].ToString());
                            cmd.Parameters.AddWithValue("@Sibling_name1", dt.Rows[i]["Sibling_name1"].ToString());
                            cmd.Parameters.AddWithValue("@Sibling_age1", dt.Rows[i]["Sibling_age1"].ToString());
                            cmd.Parameters.AddWithValue("@Sibling_school1", dt.Rows[i]["Sibling_school1"].ToString());
                            cmd.Parameters.AddWithValue("@Sibling_class1", dt.Rows[i]["Sibling_class1"].ToString());
                            cmd.Parameters.AddWithValue("@Sibling_name2", dt.Rows[i]["Sibling_name2"].ToString());
                            cmd.Parameters.AddWithValue("@Sibling_age2", dt.Rows[i]["Sibling_age2"].ToString());
                            cmd.Parameters.AddWithValue("@Sibling_school2", dt.Rows[i]["Sibling_school2"].ToString());
                            cmd.Parameters.AddWithValue("@Sibling_class2", dt.Rows[i]["Sibling_class2"].ToString());


                            //Previous School Details
                            cmd.Parameters.AddWithValue("@currentschool", dt.Rows[i]["currentschool"].ToString());
                            cmd.Parameters.AddWithValue("@Prev_school_name", dt.Rows[i]["Prev_school_name"].ToString());



                            cmd.Parameters.AddWithValue("@Old_Admission_Date", dt.Rows[i]["Old_Admission_Date"].ToString());
                            cmd.Parameters.AddWithValue("@OLd_Admission_Idate", dt.Rows[i]["OLd_Admission_Idate"].ToString());
                            cmd.Parameters.AddWithValue("@Prev_board_type", dt.Rows[i]["Prev_board_type"].ToString());
                            cmd.Parameters.AddWithValue("@Prev_board", dt.Rows[i]["Prev_board"].ToString());
                            cmd.Parameters.AddWithValue("@Old_class_id", dt.Rows[i]["Old_class_id"].ToString());
                            cmd.Parameters.AddWithValue("@Prev_percentage", dt.Rows[i]["Prev_percentage"].ToString());
                            cmd.Parameters.AddWithValue("@Prev_reason_for_shift", dt.Rows[i]["Prev_reason_for_shift"].ToString());
                            cmd.Parameters.AddWithValue("@Prev_year", dt.Rows[i]["Prev_year"].ToString());

                            //Father Details
                            cmd.Parameters.AddWithValue("@fathername", dt.Rows[i]["fathername"].ToString());
                            cmd.Parameters.AddWithValue("@occuption", dt.Rows[i]["occuption"].ToString());
                            cmd.Parameters.AddWithValue("@fatherqualification", dt.Rows[i]["fatherqualification"].ToString());
                            cmd.Parameters.AddWithValue("@f_nationality", dt.Rows[i]["f_nationality"].ToString());
                            cmd.Parameters.AddWithValue("@f_marrital_statue", dt.Rows[i]["f_marrital_statue"].ToString());
                            cmd.Parameters.AddWithValue("@Country_Code_Father", dt.Rows[i]["Country_Code_Father"].ToString());
                            cmd.Parameters.AddWithValue("@father_mob", dt.Rows[i]["father_mob"].ToString());
                            cmd.Parameters.AddWithValue("@Father_whatsapp_country_code", dt.Rows[i]["Father_whatsapp_country_code"].ToString());
                            cmd.Parameters.AddWithValue("@Father_whatsApp_no", dt.Rows[i]["Father_whatsApp_no"].ToString());
                            cmd.Parameters.AddWithValue("@email_id", dt.Rows[i]["email_id"].ToString());
                            cmd.Parameters.AddWithValue("@guardianname", dt.Rows[i]["guardianname"].ToString());
                            cmd.Parameters.AddWithValue("@parentincome", dt.Rows[i]["parentincome"].ToString());
                            cmd.Parameters.AddWithValue("@Father_aadhar_no", dt.Rows[i]["Father_aadhar_no"].ToString());

                            //Mother Details
                            cmd.Parameters.AddWithValue("@mothername", dt.Rows[i]["mothername"].ToString());
                            cmd.Parameters.AddWithValue("@m_occupation", dt.Rows[i]["m_occupation"].ToString());
                            cmd.Parameters.AddWithValue("@motherqualifiaction", dt.Rows[i]["motherqualifiaction"].ToString());
                            cmd.Parameters.AddWithValue("@m_nationality", dt.Rows[i]["m_nationality"].ToString());
                            cmd.Parameters.AddWithValue("@m_marrital_statue", dt.Rows[i]["m_marrital_statue"].ToString());
                            cmd.Parameters.AddWithValue("@Country_Code_Mother", dt.Rows[i]["Country_Code_Mother"].ToString());
                            cmd.Parameters.AddWithValue("@mother_mob", dt.Rows[i]["mother_mob"].ToString());
                            cmd.Parameters.AddWithValue("@Mother_whatsapp_country_code", dt.Rows[i]["Mother_whatsapp_country_code"].ToString());
                            cmd.Parameters.AddWithValue("@Mother_whatsApp_no", dt.Rows[i]["Mother_whatsApp_no"].ToString());
                            cmd.Parameters.AddWithValue("@mother_email", dt.Rows[i]["mother_email"].ToString());
                            cmd.Parameters.AddWithValue("@Mother_aadhar_no", dt.Rows[i]["Mother_aadhar_no"].ToString());


                            //Present Address Details
                            cmd.Parameters.AddWithValue("@careof", dt.Rows[i]["careof"].ToString());
                            cmd.Parameters.AddWithValue("@postoffice", dt.Rows[i]["postoffice"].ToString());
                            cmd.Parameters.AddWithValue("@policestation", dt.Rows[i]["policestation"].ToString());
                            cmd.Parameters.AddWithValue("@district", dt.Rows[i]["district"].ToString());
                            cmd.Parameters.AddWithValue("@city", dt.Rows[i]["city"].ToString());
                            cmd.Parameters.AddWithValue("@state", dt.Rows[i]["state"].ToString());
                            cmd.Parameters.AddWithValue("@pin", dt.Rows[i]["pin"].ToString());
                            cmd.Parameters.AddWithValue("@Present_country", dt.Rows[i]["Present_country"].ToString());
                            cmd.Parameters.AddWithValue("@Country_Code_Current_add", dt.Rows[i]["Country_Code_Current_add"].ToString());
                            cmd.Parameters.AddWithValue("@mobilenumber", dt.Rows[i]["mobilenumber"].ToString());

                            //Permanent Address Details
                            cmd.Parameters.AddWithValue("@careof_permanent", dt.Rows[i]["careof_permanent"].ToString());
                            cmd.Parameters.AddWithValue("@postoffice_permanent", dt.Rows[i]["postoffice_permanent"].ToString());
                            cmd.Parameters.AddWithValue("@policestation_permanent", dt.Rows[i]["policestation_permanent"].ToString());
                            cmd.Parameters.AddWithValue("@district_permanent", dt.Rows[i]["district_permanent"].ToString());
                            cmd.Parameters.AddWithValue("@city_permanent", dt.Rows[i]["city_permanent"].ToString());
                            cmd.Parameters.AddWithValue("@state_permanent", dt.Rows[i]["state_permanent"].ToString());
                            cmd.Parameters.AddWithValue("@pincode", dt.Rows[i]["pincode"].ToString());
                            cmd.Parameters.AddWithValue("@Permanent_country", dt.Rows[i]["Permanent_country"].ToString());
                            cmd.Parameters.AddWithValue("@Country_Code_Current_Perm_add", dt.Rows[i]["Country_Code_Current_Perm_add"].ToString());
                            cmd.Parameters.AddWithValue("@mob2", dt.Rows[i]["mob2"].ToString());



                            //Bank Details
                            cmd.Parameters.AddWithValue("@Bank_acount_no", dt.Rows[i]["Bank_acount_no"].ToString());
                            cmd.Parameters.AddWithValue("@Account_Holder_name", dt.Rows[i]["Account_Holder_name"].ToString());
                            cmd.Parameters.AddWithValue("@Bnk_Name", dt.Rows[i]["Bnk_Name"].ToString());
                            cmd.Parameters.AddWithValue("@IFSC_Code", dt.Rows[i]["IFSC_Code"].ToString());
                            cmd.Parameters.AddWithValue("@Branch_Name", dt.Rows[i]["Branch_Name"].ToString());



                            //=======================
                            cmd.Parameters.AddWithValue("@payment_status", "Unpaid");
                            cmd.Parameters.AddWithValue("@Hostel_id", "0");
                            cmd.Parameters.AddWithValue("@Session_id", dt.Rows[i]["Session_id"].ToString());
                            cmd.Parameters.AddWithValue("@Class_id", dt.Rows[i]["Class_id"].ToString());
                            cmd.Parameters.AddWithValue("@Is_TC_Taken", false);
                            cmd.Parameters.AddWithValue("@Student_id", dt.Rows[i]["Student_id"].ToString());
                            cmd.Parameters.AddWithValue("@Branch_id", "1");

                            cmd.Parameters.AddWithValue("@Student_Name_First", dt.Rows[i]["Student_Name_First"].ToString());
                            cmd.Parameters.AddWithValue("@Student_Middle_Name", dt.Rows[i]["Student_Middle_Name"].ToString());
                            cmd.Parameters.AddWithValue("@Student_Name_Last", dt.Rows[i]["Student_Name_Last"].ToString());

                            cmd.Parameters.AddWithValue("@Father_Name_First", dt.Rows[i]["Father_Name_First"].ToString());
                            cmd.Parameters.AddWithValue("@Father_Name_Middle", dt.Rows[i]["Father_Name_Middle"].ToString());
                            cmd.Parameters.AddWithValue("@Father_Name_Last", dt.Rows[i]["Father_Name_Last"].ToString());

                            cmd.Parameters.AddWithValue("@Mother_Name_First", dt.Rows[i]["Mother_Name_First"].ToString());
                            cmd.Parameters.AddWithValue("@Mother_Name_Middle", dt.Rows[i]["Mother_Name_Middle"].ToString());
                            cmd.Parameters.AddWithValue("@Mother_Name_Last", dt.Rows[i]["Mother_Name_Last"].ToString());

                            cmd.Parameters.AddWithValue("@StudentStatus", "AV");
                            cmd.Parameters.AddWithValue("@Pwd", dt.Rows[i]["Pwd"].ToString());
                            cmd.Parameters.AddWithValue("@Verification_Istatus", "0");
                            cmd.Parameters.AddWithValue("@Status", "1");
                            cmd.Parameters.AddWithValue("@College_School_Name", dt.Rows[i]["College_School_Name"].ToString());
                            cmd.Parameters.AddWithValue("@relation", "Not Available");
                            cmd.Parameters.AddWithValue("@transportationtaken", "No");
                            cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                            cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                            cmd.Parameters.AddWithValue("@Created_time", mycode.time());
                            cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
                            if (payments.InsertUpdateData(cmd, con))
                            {
                                save_hostel_data(dt.Rows[i]["admissionserialnumber"].ToString(), dt.Rows[i]["session"].ToString(), dt.Rows[i]["Class_id"].ToString(), dt.Rows[i]["Session_id"].ToString(), con);

                                flag = true;
                            }


                        }
                        con.Close();
                        scope.Complete();

                    }
                    if (flag == true)
                    {
                        std_basic_infoS.Visible = false;
                        Alertme("Student has been sucessfully trasnferd hostel portal", "success");
                        // sucess
                    }


                }
            }
            else
            {
                Alertme("This student already in the hostel portal", "warning");
            }
        }


        private void save_hostel_data(string admissionserialnumber, string session, string Class_id, string Session_id, SqlConnection con)
        {
            string hostel_assign_id = create_sl_no(con);
            string session_frst_year = session.Substring(0, 4);
            SqlCommand cmd;
            string query = "INSERT INTO Hostel_assign_master (Session_id,Class_id,Admission_no,From_month_name,From_month_id,Hostel_id,Category_id,Room_id,Bed_id,Hostel_assign_id,Created_by,Created_date,Created_idate,Status,Assined_Year_Month) values (@Session_id,@Class_id,@Admission_no,@From_month_name,@From_month_id,@Hostel_id,@Category_id,@Room_id,@Bed_id,@Hostel_assign_id,@Created_by,@Created_date,@Created_idate,@Status,@Assined_Year_Month)";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Session_id", Session_id);
            cmd.Parameters.AddWithValue("@Class_id", Class_id);

            cmd.Parameters.AddWithValue("@Admission_no", txt_admission_no.Text);
            cmd.Parameters.AddWithValue("@From_month_name", "April");
            cmd.Parameters.AddWithValue("@From_month_id", "04");

            cmd.Parameters.AddWithValue("@Hostel_id", ddl_hostel.SelectedValue);
            cmd.Parameters.AddWithValue("@Category_id", ddl_room_cat.SelectedValue);
            cmd.Parameters.AddWithValue("@Room_id", ddl_room.SelectedValue);
            cmd.Parameters.AddWithValue("@Bed_id", ddl_bed.SelectedValue);
            cmd.Parameters.AddWithValue("@Hostel_assign_id", hostel_assign_id);
            cmd.Parameters.AddWithValue("@Status", 1);
            cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
            cmd.Parameters.AddWithValue("@Created_date", mycode.date());
            cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
            cmd.Parameters.AddWithValue("@Assined_Year_Month", session_frst_year + "04");
            if (payments.InsertUpdateData(cmd, con))
            {
                try
                {
                    payments.exeSql("update admission_registor set hosteltaken='Yes',transportationtaken='No',Hostel_id='" + ddl_hostel.SelectedValue + "',Hostel_assignD_id='" + hostel_assign_id + "' where admissionserialnumber='" + txt_admission_no.Text + "' and Session_id='" + Session_id + "' ", con);
                }
                catch
                {
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

        private bool chek_data()
        {
            DataTable dt = My.dataTableHDB("Select admissionserialnumber from admission_registor where admissionserialnumber='" + ViewState["admissionserialnumber"].ToString() + "'", My.hostelDB);


            if (dt.Rows.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void btn_find_by_name_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_session_name.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session", "warning");
                    ddl_session_name.Focus();
                    return;
                }
                if (txt_student_name.Text == "")
                {
                    Alertme("Please enter student name.", "warning");
                    txt_student_name.Focus();
                    return;
                }

                string stdname = txt_student_name.Text.Trim();
                string query = "select * from admission_registor where studentname like '%" + stdname + "%' and session='" + ddl_session_name.SelectedItem.Text + "' and  Status='1' order by id asc";

                DataTable dt = My.dataTable(query);
                if (dt.Rows.Count == 0)
                {
                    rp_std.DataSource = null;
                    rp_std.DataBind();
                    Alertme("Data Not Found...", "warning");
                }
                else if (dt.Rows.Count == 1)
                {
                    query = "select * from admission_registor where admissionserialnumber='" + dt.Rows[0]["admissionserialnumber"].ToString() + "' and Session='" + ddl_session_name.SelectedItem.Text + "' order by studentname asc";
                    find_details(query);
                }
                else
                {
                    rp_std.DataSource = dt;
                    rp_std.DataBind();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "studentInfo();", true);
                }
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }


        protected void lnk_select_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "";
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbladmissionserialnumber = (Label)row.FindControl("lbladmissionserialnumber");
                Label lbl_session = (Label)row.FindControl("lbl_session");
                query = "select * from admission_registor where admissionserialnumber='" + lbladmissionserialnumber.Text + "' and Session='" + lbl_session.Text + "'";
                find_details(query);
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }
    }
}