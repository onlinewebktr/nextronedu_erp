using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Dvlpr_Prof
{
    public partial class Update_data_Local_to_online_Purnank : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Admindov"] == null)
                {
                    Session.Abandon();
                    Session.Clear();
                    Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
                    Response.Write("<script language=javascript>wnd.close();</script>");
                    Response.Write("<script language=javascript>window.open('../Default.aspx','_parent',replace=true);</script>");
                }
                else
                {
                    ViewState["Userid"] = Session["Admindov"].ToString();

                    ViewState["branchid"] = "1";

                    mycode.bind_all_ddl_with_id(ddl_session, "Select  Session,session_id from session_details order by Session asc");


                }
            }
        }
        public void Alert(string Message)
        {
            lblmessage.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }
        protected void btn_update_student_data_Click(object sender, EventArgs e)
        {
            if (ddl_session.SelectedItem.Text == "Select")
            {
                Alert("Please select session");
            }
            else
            {
                try
                {
                    update_data();
                    Alert("Updated Successfully");
                }
                catch(Exception ex)
                {
                    Alert(ex.ToString());
                } 
            } 
        }

        private string olddatabse = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\BD Academy DB\Inext_School_Management_Data_BD.mdf;Integrated Security=True";// @"Data Source=sg1-wsq2.my-hosting-panel.com;Integrated Security=False;User ID=SchoolWeb_RRKPS; Password=Eoz69~79l;Max Pool Size=10000;Pooling=true";
        public DataTable FillData_new(string query)
        {
            DataTable dtc = new DataTable();
            try
            {
                SqlConnection conn = new SqlConnection(olddatabse);
                if (conn.State == ConnectionState.Closed) { conn.Open(); }
                SqlDataAdapter adpc = new SqlDataAdapter(query, olddatabse);
                adpc.Fill(dtc);
                if (conn.State == ConnectionState.Open) { conn.Close(); }

            }
            catch (Exception ex)
            {


            }
            return dtc;
        }

        private void update_data()
        {
            string query = "select admissionserialnumber,rollnumber,Section,session,dateofadmission,studentname,gender,dob,fathername,fatherqualification,mothername,motherqualifiaction,occuption,religion,cast,parentincome,careof,city,postoffice,policestation,district,state,pin,mobilenumber,careof_permanent,city_permanent,postoffice_permanent,policestation_permanent,district_permanent,state_permanent,pincode,admission_idate,staff_ward,jati,mob2,guardianname,identifacationmark,currentschool,relation,aadharno,RTE from admission_registor where session='" + ddl_session.SelectedItem.Text + "'";
            DataTable dt = FillData_new(query);
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string rollnumber = dt.Rows[i]["rollnumber"].ToString();
                    string Section = dt.Rows[i]["Section"].ToString();


                    string admissionserialnumber = dt.Rows[i]["admissionserialnumber"].ToString();

                    string dateofadmission = dt.Rows[i]["dateofadmission"].ToString();// orginal
                    string admission_idate = dt.Rows[i]["admission_idate"].ToString();// orginal

                    string studentname = dt.Rows[i]["studentname"].ToString();
                    string gender = dt.Rows[i]["gender"].ToString();
                    string dob = dt.Rows[i]["dob"].ToString();
                    string fathername = dt.Rows[i]["fathername"].ToString();
                    string fatherqualification = dt.Rows[i]["fatherqualification"].ToString();
                    string mothername = dt.Rows[i]["mothername"].ToString();
                    string motherqualifiaction = dt.Rows[i]["motherqualifiaction"].ToString();
                    string occuption = dt.Rows[i]["occuption"].ToString();// fatehr occupation
                    string religion = dt.Rows[i]["religion"].ToString();
                    string cast = dt.Rows[i]["cast"].ToString();
                    string parentincome = dt.Rows[i]["parentincome"].ToString();
                    string careof = dt.Rows[i]["careof"].ToString();
                    string city = dt.Rows[i]["city"].ToString();
                    string postoffice = dt.Rows[i]["postoffice"].ToString();
                    string policestation = dt.Rows[i]["policestation"].ToString();
                    string district = dt.Rows[i]["district"].ToString();
                    string state = dt.Rows[i]["state"].ToString();
                    string pin = dt.Rows[i]["pin"].ToString();
                    string mobilenumber = dt.Rows[i]["mobilenumber"].ToString();
                    string careof_permanent = dt.Rows[i]["careof_permanent"].ToString();
                    string city_permanent = dt.Rows[i]["city_permanent"].ToString();
                    string postoffice_permanent = dt.Rows[i]["postoffice_permanent"].ToString();
                    string policestation_permanent = dt.Rows[i]["policestation_permanent"].ToString();
                    string district_permanent = dt.Rows[i]["district_permanent"].ToString();
                    string state_permanent = dt.Rows[i]["state_permanent"].ToString();

                    string mob2 = dt.Rows[i]["mob2"].ToString();
                    string guardianname = dt.Rows[i]["guardianname"].ToString();
                    string identifacationmark = dt.Rows[i]["identifacationmark"].ToString();
                    string currentschool = dt.Rows[i]["currentschool"].ToString();
                    string relation = dt.Rows[i]["relation"].ToString();
                    string pincode = dt.Rows[i]["pincode"].ToString();
                    string aadharno = dt.Rows[i]["aadharno"].ToString();
                    string RTE = dt.Rows[i]["RTE"].ToString();
                    string staff_ward = dt.Rows[i]["staff_ward"].ToString();
                    string jati = dt.Rows[i]["jati"].ToString();

                    SqlCommand cmd;
                    string query2 = "Update admission_registor set rollnumber@rollnumber,Section@Section,dateofadmission@dateofadmission,studentname@studentname,gender@gender,identifacationmark@identifacationmark,currentschool@currentschool,dob@dob,fathername@fathername,fatherqualification@fatherqualification,mothername@mothername,motherqualifiaction=@motherqualifiaction,guardianname=@guardianname,relation=@relation,occuption=@occuption,religion=@religion,cast=@cast,parentincome=@parentincome,careof=@careof,city=@city,postoffice=@postoffice,policestation=@policestation,district=@district,state=@state,pin=@pin,mobilenumber=@mob2,careof_permanent=@careof_permanent,city_permanent=@city_permanent,postoffice_permanent=@postoffice_permanent,policestation_permanent=@policestation_permanent,district_permanent=@district_permanent,state_permanent=@state_permanent,pincode=@pincode,aadharno=@aadharno,RTE=@RTE,staff_ward=@staff_ward,jati=@jati,mob2=@mobilenumber,student_mother_tounge=@student_mother_tounge,is_illness=@is_illness,f_nationality=@f_nationality,f_marrital_statue=@f_marrital_statue,Is_birth_certificate =@Is_birth_certificate, birth_certificate_number=@birth_certificate_number,blood_group =@blood_group, Student_nationality=@Student_nationality,m_occupation =@m_occupation,m_nationality =@m_nationality, m_marrital_statue=@m_marrital_statue,Country_Code_Mother =@Country_Code_Mother, Mother_whatsapp_country_code=@Mother_whatsapp_country_code,Present_country =@Present_country, Country_Code_Current_add=@Country_Code_Current_add,state_permanent =@state_permanent, Father_whatsapp_country_code=@Father_whatsapp_country_code,Mother_whatsApp_no =@mobilenumber, Father_whatsApp_no=@mobilenumber,admission_idate=@admission_idate where admissionserialnumber ='" + admissionserialnumber + "' and session='"+ddl_session.SelectedItem.Text+"'";
                    cmd = new SqlCommand(query2);
                
                    cmd.Parameters.AddWithValue("@rollnumber", rollnumber);
                    cmd.Parameters.AddWithValue("@Section", Section);
                    cmd.Parameters.AddWithValue("@dateofadmission", dateofadmission);
                    cmd.Parameters.AddWithValue("@admission_idate", mycode.ConvertStringToiDate(dateofadmission));
                    
                    cmd.Parameters.AddWithValue("@studentname", studentname);
                    cmd.Parameters.AddWithValue("@gender", gender);
                    cmd.Parameters.AddWithValue("@identifacationmark", identifacationmark);
                    cmd.Parameters.AddWithValue("@currentschool", currentschool);
                    cmd.Parameters.AddWithValue("@dob", dob);
                    cmd.Parameters.AddWithValue("@fathername", fathername);
                    cmd.Parameters.AddWithValue("@fatherqualification", fatherqualification);
                    cmd.Parameters.AddWithValue("@mothername", mothername);
                    cmd.Parameters.AddWithValue("@motherqualifiaction", motherqualifiaction);
                    cmd.Parameters.AddWithValue("@guardianname", guardianname);
                    cmd.Parameters.AddWithValue("@relation", relation);
                    cmd.Parameters.AddWithValue("@occuption", occuption);
                    cmd.Parameters.AddWithValue("@religion", religion);
                    cmd.Parameters.AddWithValue("@cast", cast);
                    cmd.Parameters.AddWithValue("@parentincome", parentincome);
                    cmd.Parameters.AddWithValue("@careof", careof);
                    cmd.Parameters.AddWithValue("@city", city);
                    cmd.Parameters.AddWithValue("@postoffice", postoffice);
                    cmd.Parameters.AddWithValue("@policestation", policestation);
                    cmd.Parameters.AddWithValue("@district", district);
                    cmd.Parameters.AddWithValue("@state", state);
                    cmd.Parameters.AddWithValue("@pin", pin);
                    cmd.Parameters.AddWithValue("@mobilenumber", mob2);
                    cmd.Parameters.AddWithValue("@careof_permanent", careof_permanent);
                    cmd.Parameters.AddWithValue("@city_permanent", city_permanent);
                    cmd.Parameters.AddWithValue("@postoffice_permanent", postoffice_permanent);
                    cmd.Parameters.AddWithValue("@policestation_permanent", policestation_permanent);
                    cmd.Parameters.AddWithValue("@district_permanent", district_permanent);
                    cmd.Parameters.AddWithValue("@state_permanent", state_permanent);
                    cmd.Parameters.AddWithValue("@pincode", pincode);
                    cmd.Parameters.AddWithValue("@aadharno", aadharno);
                    cmd.Parameters.AddWithValue("@RTE", RTE);
                    cmd.Parameters.AddWithValue("@staff_ward", staff_ward);
                    cmd.Parameters.AddWithValue("@jati", jati);
                    cmd.Parameters.AddWithValue("@mob2", mobilenumber);
                    cmd.Parameters.AddWithValue("@student_mother_tounge", "HINDI");
                    cmd.Parameters.AddWithValue("@is_illness", "NO");
                    cmd.Parameters.AddWithValue("@f_nationality", "INDIAN");
                    cmd.Parameters.AddWithValue("@f_marrital_statue", "N/A");
                    cmd.Parameters.AddWithValue("@Is_birth_certificate", "0");
                    cmd.Parameters.AddWithValue("@birth_certificate_number", "N/A");
                    cmd.Parameters.AddWithValue("@blood_group", "NA");
                    cmd.Parameters.AddWithValue("@Student_nationality", "INDIAN");
                    cmd.Parameters.AddWithValue("@m_occupation", "NA");
                    cmd.Parameters.AddWithValue("@motherqualifiaction", "NA");
                    cmd.Parameters.AddWithValue("@m_nationality", "INDIAN");
                    cmd.Parameters.AddWithValue("@m_marrital_statue", "N/A");
                    cmd.Parameters.AddWithValue("@Country_Code_Mother", "+91");
                    cmd.Parameters.AddWithValue("@Mother_whatsapp_country_code", "+91");
                    cmd.Parameters.AddWithValue("@Present_country", "INDIAN");
                    cmd.Parameters.AddWithValue("@Country_Code_Current_add", "+91");
                    cmd.Parameters.AddWithValue("@state_permanent", "West Bengal");
                    cmd.Parameters.AddWithValue("@Father_whatsapp_country_code", "+91");
                    cmd.Parameters.AddWithValue("@Mother_whatsApp_no", mobilenumber);
                    cmd.Parameters.AddWithValue("@Father_whatsApp_no", mobilenumber);
                    if (My.InsertUpdateData(cmd))
                    {
                    }
                }
            }
        }

        protected void btn_insert_date_Click(object sender, EventArgs e)
        {
            string query = "select   admissionserialnumber,rollnumber,Section,session,dateofadmission,studentname,gender,dob,fathername,fatherqualification,mothername,motherqualifiaction,occuption,religion,cast,parentincome,careof,city,postoffice,policestation,district,state,pin,mobilenumber,careof_permanent,city_permanent,postoffice_permanent,policestation_permanent,district_permanent,state_permanent,pincode,admission_idate,staff_ward,jati,mob2,guardianname,identifacationmark,currentschool,relation,aadharno,RTE,Transfer_Status,Class_id,class   from admission_registor where session='" + ddl_session.SelectedItem.Text + "' and  Transfer_Status='New'  and admissionserialnumber in('01/XI/2024','124/XI/2023','252/24','253/24','254/24','255/24','256/24','257/24','258/24','263/23','264/23','51/16')";//and admissionserialnumber in(Select Admission_no from Temp_Student_Registration where Session='" + ddl_session.SelectedItem.Text + "' )
            DataTable dt = FillData_new(query);
            if (dt.Rows.Count == 0)
            { 
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                { 
                    string classname = dt.Rows[i]["class"].ToString();
                    string Class_id = dt.Rows[i]["Class_id"].ToString();
                    string rollnumber = dt.Rows[i]["rollnumber"].ToString();
                    string Section = dt.Rows[i]["Section"].ToString();


                    string admissionserialnumber = dt.Rows[i]["admissionserialnumber"].ToString();

                    string dateofadmission = dt.Rows[i]["dateofadmission"].ToString();// orginal
                    string admission_idate = dt.Rows[i]["admission_idate"].ToString();// orginal

                    string studentname = dt.Rows[i]["studentname"].ToString();
                    string gender = dt.Rows[i]["gender"].ToString();
                    string dob = dt.Rows[i]["dob"].ToString();
                    string fathername = dt.Rows[i]["fathername"].ToString();
                    string fatherqualification = dt.Rows[i]["fatherqualification"].ToString();
                    string mothername = dt.Rows[i]["mothername"].ToString();
                    string motherqualifiaction = dt.Rows[i]["motherqualifiaction"].ToString();
                    string occuption = dt.Rows[i]["occuption"].ToString();// fatehr occupation
                    string religion = dt.Rows[i]["religion"].ToString();
                    string cast = dt.Rows[i]["cast"].ToString();
                    string parentincome = dt.Rows[i]["parentincome"].ToString();
                    string careof = dt.Rows[i]["careof"].ToString();
                    string city = dt.Rows[i]["city"].ToString();
                    string postoffice = dt.Rows[i]["postoffice"].ToString();
                    string policestation = dt.Rows[i]["policestation"].ToString();
                    string district = dt.Rows[i]["district"].ToString();
                    string state = dt.Rows[i]["state"].ToString();
                    string pin = dt.Rows[i]["pin"].ToString();
                    string mobilenumber = dt.Rows[i]["mobilenumber"].ToString();
                    string careof_permanent = dt.Rows[i]["careof_permanent"].ToString();
                    string city_permanent = dt.Rows[i]["city_permanent"].ToString();
                    string postoffice_permanent = dt.Rows[i]["postoffice_permanent"].ToString();
                    string policestation_permanent = dt.Rows[i]["policestation_permanent"].ToString();
                    string district_permanent = dt.Rows[i]["district_permanent"].ToString();
                    string state_permanent = dt.Rows[i]["state_permanent"].ToString();

                    string mob2 = dt.Rows[i]["mob2"].ToString();
                    string guardianname = dt.Rows[i]["guardianname"].ToString();
                    string identifacationmark = dt.Rows[i]["identifacationmark"].ToString();
                    string currentschool = dt.Rows[i]["currentschool"].ToString();
                    string relation = dt.Rows[i]["relation"].ToString();
                    string pincode = dt.Rows[i]["pincode"].ToString();
                    string aadharno = dt.Rows[i]["aadharno"].ToString();
                    string RTE = dt.Rows[i]["RTE"].ToString();
                    string staff_ward = dt.Rows[i]["staff_ward"].ToString();
                    string jati = dt.Rows[i]["jati"].ToString();
                    DataTable dt1 = My.dataTable("Select admissionserialnumber from admission_registor   where admissionserialnumber ='" + admissionserialnumber + "' and session='" + ddl_session.SelectedItem.Text + "'");
                    if (dt1.Rows.Count == 0)
                    { 
                        SqlCommand cmd;
                        string query2 = "INSERT INTO admission_registor (Transfer_Status,Category_id,SubCategory_id,dateofadmission,admission_idate,session,hosteltaken,is_applied_dayboarding,day_boarding_with_lunch,class,rollnumber,Section,admissionserialnumber,house,studentname,dob,Is_birth_certificate,birth_certificate_number,gender,blood_group,Student_nationality,religion,ration_type,cast,Is_cast_certificate,cast_certificate_no,aadharno,student_mother_tounge,is_illness,illness_remark,Staff_employee_code,RTE,staff_ward,Staff_name,Personal_Identymarks,identifacationmark,Old_Admission_Date,OLd_Admission_Idate,Prev_board_type,fathername,occuption,fatherqualification,f_nationality,f_marrital_statue,Country_Code_Father,father_mob,Father_whatsapp_country_code,Father_whatsApp_no,email_id,guardianname,mothername,m_occupation,motherqualifiaction,m_nationality,m_marrital_statue,Country_Code_Mother,Mother_whatsapp_country_code,state,Present_country,Country_Code_Current_add,mobilenumber,state_permanent,mob2,payment_status,Hostel_id,Session_id,Class_id,Is_TC_Taken,Student_id,Branch_id,Student_Name_First,Father_Name_First,Mother_Name_First,StudentStatus,Pwd,Verification_Istatus,Status,College_School_Name,relation,transportationtaken,Created_by,Created_date,Created_time,Created_idate) values (@Transfer_Status,@Category_id,@SubCategory_id,@dateofadmission,@admission_idate,@session,@hosteltaken,@is_applied_dayboarding,@day_boarding_with_lunch,@class,@rollnumber,@Section,@admissionserialnumber,@house,@studentname,@dob,@Is_birth_certificate,@birth_certificate_number,@gender,@blood_group,@Student_nationality,@religion,@ration_type,@cast,@Is_cast_certificate,@cast_certificate_no,@aadharno,@student_mother_tounge,@is_illness,@illness_remark,@Staff_employee_code,@RTE,@staff_ward,@Staff_name,@Personal_Identymarks,@identifacationmark,@Old_Admission_Date,@OLd_Admission_Idate,@Prev_board_type,@fathername,@occuption,@fatherqualification,@f_nationality,@f_marrital_statue,@Country_Code_Father,@father_mob,@Father_whatsapp_country_code,@Father_whatsApp_no,@email_id,@guardianname,@mothername,@m_occupation,@motherqualifiaction,@m_nationality,@m_marrital_statue,@Country_Code_Mother,@Mother_whatsapp_country_code,@state,@Present_country,@Country_Code_Current_add,@mobilenumber,@state_permanent,@mob2,@payment_status,@Hostel_id,@Session_id,@Class_id,@Is_TC_Taken,@Student_id,@Branch_id,@Student_Name_First,@Father_Name_First,@Mother_Name_First,@StudentStatus,@Pwd,@Verification_Istatus,@Status,@College_School_Name,@relation,@transportationtaken,@Created_by,@Created_date,@Created_time,@Created_idate)";
                        cmd = new SqlCommand(query2);
                        //Academic Details

                        cmd.Parameters.AddWithValue("@Transfer_Status", "NT");
                        cmd.Parameters.AddWithValue("@Category_id", 3);
                        cmd.Parameters.AddWithValue("@SubCategory_id", 4);



                        cmd.Parameters.AddWithValue("@dateofadmission", dateofadmission);
                        cmd.Parameters.AddWithValue("@admission_idate", mycode.ConvertStringToiDate(dateofadmission));
                        cmd.Parameters.AddWithValue("@session", ddl_session.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@hosteltaken", "No");
                        cmd.Parameters.AddWithValue("@is_applied_dayboarding", false);
                        cmd.Parameters.AddWithValue("@day_boarding_with_lunch", false);
                        cmd.Parameters.AddWithValue("@class", classname);
                        cmd.Parameters.AddWithValue("@rollnumber", rollnumber);
                        cmd.Parameters.AddWithValue("@Section", Section);
                        cmd.Parameters.AddWithValue("@admissionserialnumber", admissionserialnumber);
                        cmd.Parameters.AddWithValue("@house", 0);
                        //Student Info 
                        cmd.Parameters.AddWithValue("@studentname", studentname);
                        cmd.Parameters.AddWithValue("@dob", dob);
                        cmd.Parameters.AddWithValue("@Is_birth_certificate", "0");
                        cmd.Parameters.AddWithValue("@birth_certificate_number", "N/A");
                        cmd.Parameters.AddWithValue("@gender", gender);
                        cmd.Parameters.AddWithValue("@blood_group", "NA");
                        cmd.Parameters.AddWithValue("@Student_nationality", "INDIAN");
                        cmd.Parameters.AddWithValue("@religion", "N/A");
                        cmd.Parameters.AddWithValue("@ration_type", "0");
                        cmd.Parameters.AddWithValue("@cast", "OTHERS");
                        cmd.Parameters.AddWithValue("@Is_cast_certificate", "NO");
                        cmd.Parameters.AddWithValue("@cast_certificate_no", "N/A");
                        cmd.Parameters.AddWithValue("@aadharno", "N/A");
                        cmd.Parameters.AddWithValue("@student_mother_tounge", "Not Applicable");
                        cmd.Parameters.AddWithValue("@is_illness", "NO");
                        cmd.Parameters.AddWithValue("@illness_remark", "");
                        cmd.Parameters.AddWithValue("@Staff_employee_code", "");
                        cmd.Parameters.AddWithValue("@RTE", "NO");
                        cmd.Parameters.AddWithValue("@staff_ward", "NO");
                        cmd.Parameters.AddWithValue("@Staff_name", "");
                        cmd.Parameters.AddWithValue("@Personal_Identymarks", "");
                        cmd.Parameters.AddWithValue("@identifacationmark", "");
                        //Previous School Details
                        cmd.Parameters.AddWithValue("@Old_Admission_Date", dateofadmission);
                        cmd.Parameters.AddWithValue("@OLd_Admission_Idate", mycode.ConvertStringToiDate(dateofadmission));
                        cmd.Parameters.AddWithValue("@Prev_board_type", "STATE");
                        //Father Details
                        cmd.Parameters.AddWithValue("@fathername", fathername);
                        cmd.Parameters.AddWithValue("@occuption", "NA");

                        cmd.Parameters.AddWithValue("@fatherqualification", "NA");
                        cmd.Parameters.AddWithValue("@f_nationality", "INDIAN");
                        cmd.Parameters.AddWithValue("@f_marrital_statue", "N/A");
                        cmd.Parameters.AddWithValue("@Country_Code_Father", "+91");
                        cmd.Parameters.AddWithValue("@father_mob", mobilenumber);
                        cmd.Parameters.AddWithValue("@Father_whatsapp_country_code", "+91");
                        cmd.Parameters.AddWithValue("@Father_whatsApp_no", mobilenumber);
                        cmd.Parameters.AddWithValue("@email_id", "");
                        cmd.Parameters.AddWithValue("@guardianname", fathername);
                        //Mother Details
                        cmd.Parameters.AddWithValue("@mothername", mothername);
                        cmd.Parameters.AddWithValue("@m_occupation", "NA");
                        cmd.Parameters.AddWithValue("@motherqualifiaction", "NA");
                        cmd.Parameters.AddWithValue("@m_nationality", "INDIAN");
                        cmd.Parameters.AddWithValue("@m_marrital_statue", "N/A");
                        cmd.Parameters.AddWithValue("@Country_Code_Mother", "+91");
                        cmd.Parameters.AddWithValue("@Mother_whatsapp_country_code", "+91");
                        //Present Address Details
                        cmd.Parameters.AddWithValue("@state", "Bihar");
                        cmd.Parameters.AddWithValue("@Present_country", "INDIAN");
                        cmd.Parameters.AddWithValue("@Country_Code_Current_add", "+91");
                        cmd.Parameters.AddWithValue("@mobilenumber", mobilenumber);
                        //Permanent Address Details
                        cmd.Parameters.AddWithValue("@state_permanent", "Bihar");

                        cmd.Parameters.AddWithValue("@mob2", mobilenumber);
                        //=======================
                        cmd.Parameters.AddWithValue("@payment_status", "Unpaid");
                        cmd.Parameters.AddWithValue("@Hostel_id", "0");
                        cmd.Parameters.AddWithValue("@Session_id", ddl_session.SelectedValue);
                        cmd.Parameters.AddWithValue("@Class_id", Class_id);
                        cmd.Parameters.AddWithValue("@Is_TC_Taken", false);
                        cmd.Parameters.AddWithValue("@Student_id", admissionserialnumber);
                        cmd.Parameters.AddWithValue("@Branch_id", "1");
                        cmd.Parameters.AddWithValue("@Student_Name_First", studentname);
                        cmd.Parameters.AddWithValue("@Father_Name_First", fathername);
                        cmd.Parameters.AddWithValue("@Mother_Name_First", mothername);
                        cmd.Parameters.AddWithValue("@StudentStatus", "AV");
                        cmd.Parameters.AddWithValue("@Pwd", My.create_random_no_otp());
                        cmd.Parameters.AddWithValue("@Verification_Istatus", "0");
                        cmd.Parameters.AddWithValue("@Status", "1");
                        cmd.Parameters.AddWithValue("@College_School_Name", "B.D. Academy");
                        cmd.Parameters.AddWithValue("@relation", "Not Available");
                        cmd.Parameters.AddWithValue("@transportationtaken", "No");
                        cmd.Parameters.AddWithValue("@Created_by", "1");
                        cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                        cmd.Parameters.AddWithValue("@Created_time", mycode.time());
                        cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
                        if (My.InsertUpdateData(cmd))
                        {

                        }
                    }
                    else
                    {

                    }
                }

            }
        }
    }
}