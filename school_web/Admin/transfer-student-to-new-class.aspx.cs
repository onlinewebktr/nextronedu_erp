using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class transfer_student_to_new_class : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
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
                        if (Session["smsGS"] != null)
                        {
                            Alertme(Session["smsGS"].ToString(), "success");
                            Session["smsGS"] = null;
                        }
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["Branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        hd_user_Type.Value = Session["userTypeFee"].ToString();
                        Session["reprint_otherfee"] = "2";
                        mycode.bind_all_ddl_with_id(ddlclass, "Select Course_Name,course_id from Add_course_table order by Position asc");
                        mycode.bind_all_ddl_with_id(ddlsession, " select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                        mycode.bind_all_ddl_with_id(ddl_session_student, " select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                        mycode.bind_all_ddl_with_id(ddlsessionad, " select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                        ddlsessionad.SelectedValue = My.get_session_id();
                        ddlsession.SelectedValue = My.get_session_id();
                        ddl_session_student.SelectedValue = My.get_session_id();
                        mycode.bind_all_ddl_with_id(ddl_month, "select Month,Position from Month_Index order by Position asc");
                    }
                }
                catch
                {
                }
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

        #region WebMethoD
        [WebMethod]
        public static List<string> GetRooPath(string PathRooT, string Session_id)
        {
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(My.conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct studentname from admission_registor where studentname LIKE ''+@SearchMobNo+'%' and Session_id='" + Session_id + "' and Status='1'";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchMobNo", PathRooT);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        MobResult.Add(dr["studentname"].ToString());
                    }
                    con.Close();
                    return MobResult;
                }
            }
        }

        [WebMethod]

        public static List<string> GetRooPathAdmNo(string PathRooT, string Session_id)
        {
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(My.conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct admissionserialnumber from admission_registor where admissionserialnumber LIKE ''+@SearchMobNo+'%' and Session_id='" + Session_id + "' and Status='1'";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchMobNo", PathRooT);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        MobResult.Add(dr["admissionserialnumber"].ToString());
                    }
                    con.Close();
                    return MobResult;
                }
            }
        }
        #endregion

        protected void btn_find_admission_no_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "";
                query = "select * from admission_registor where admissionserialnumber='" + txt_admission_no.Text + "' and Session='" + ddlsessionad.SelectedItem.Text + "' and StudentStatus='AV'  and  Is_TC_Taken!='true' and Status='1' order by id asc";
                find_details(query);
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
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
                    ddlclass.SelectedValue = dr["Class_id"].ToString();
                    ddlsession.SelectedValue = dr["Session_id"].ToString();
                    ViewState["Sessionid"] = dr["Session_id"].ToString();
                    ddlsessionad.SelectedValue = dr["Session_id"].ToString();
                    ddl_session_student.SelectedValue = dr["Session_id"].ToString();
                    txt_student_name.Text = dr["studentname"].ToString();
                    txt_section.Text = dr["Section"].ToString();
                    txtrollnumber.Text = dr["rollnumber"].ToString();
                    lbl_name.Text = dr["studentname"].ToString();
                    lbl_father_name.Text = dr["fathername"].ToString();
                    lblclass.Text = dr["class"].ToString();
                    ViewState["class_id"] = dr["Class_id"].ToString();
                    txtsection.Text = dr["Section"].ToString();
                    lbl_admission_no.Text = dr["admissionserialnumber"].ToString();
                    txt_admission_no.Text = dr["admissionserialnumber"].ToString();
                    lblhostel.Text = dr["hosteltaken"].ToString();
                    ViewState["parameter"] = dr["hosteltaken"].ToString().ToUpper() == "YES" ? "HostelMonthlyFee" : "MonthlyFee";
                    ViewState["parameter_id"] = dr["hosteltaken"].ToString().ToUpper() == "YES" ? "3" : "4";

                    lbl_phone.Text = dr["mobilenumber"].ToString();
                    ViewState["hostel_id"] = My.toint(dr["Hostel_id"].ToString());
                    ViewState["day_bording"] = My.toBool(dr["is_applied_dayboarding"]);
                    ViewState["day_bording_with_lunch"] = My.toBool(dr["day_boarding_with_lunch"]);
                    ViewState["group_id"] = "3";
                    ViewState["category_id"] = dr["category_id"].ToString();
                    ViewState["SubCategory_id"] = dr["SubCategory_id"].ToString();
                    ViewState["classid"] = dr["Class_id"].ToString();
                    ViewState["Section"] = dr["Section"].ToString();
                    ViewState["sessionIDs"] = dr["Session_id"].ToString();

                    ViewState["Transfer_Status"] = dr["Transfer_Status"].ToString();

                    if (ViewState["Transfer_Status"].ToString() == "New")
                    {
                        lbl_studentype.Text = "New";
                    }
                    else
                    {
                        lbl_studentype.Text = "Old";
                    }
                    ViewState["IsBoarding"] = "0";
                    ViewState["parameteridS"] = "4";
                    string queryS = "select * from Student_mapping_with_boarding_with_lunch where Session_id='" + dr["Session_id"].ToString() + "' and Admission_no='" + dr["admissionserialnumber"].ToString() + "' and Class_id='" + dr["Class_id"].ToString() + "'";
                    DataTable dts = My.dataTable(queryS);
                    if (dts.Rows.Count != 0)
                    {
                        ViewState["LunchMnthName"] = dts.Rows[0]["Month_name"].ToString();
                        ViewState["LunchMnthId"] = dts.Rows[0]["Month_id"].ToString();
                        ViewState["IsBoarding"] = "1";
                    }

                    lbl_catogery.Text = mycode.get_catogery(dr["Category_id"].ToString());
                    lbl_subcatogery.Text = mycode.get_subcatogery(dr["Category_id"].ToString(), dr["SubCategory_id"].ToString());
                    lbltransporttion.Text = dr["transportationtaken"].ToString();
                    if (dr["Transportation_Id"].ToString() == "")
                    {
                        ViewState["transportID"] = "0";
                    }
                    else
                    {
                        ViewState["transportID"] = dr["Transportation_Id"].ToString();
                    }

                    if (dt.Rows[0]["hosteltaken"].ToString() == "")
                    {
                        ViewState["hostaltaken"] = "No";
                    }
                    else if (dt.Rows[0]["hosteltaken"].ToString().ToLower() == "no")
                    {
                        ViewState["hostaltaken"] = "No";
                    }
                    else
                    {
                        ViewState["hostaltaken"] = dt.Rows[0]["hosteltaken"].ToString();
                    }



                    mycode.bind_all_ddl_with_id(ddl_transfer_class, "select Course_Name,course_id from Add_course_table where course_id!='" + dr["Class_id"].ToString() + "' order by Position asc");
                }
            }
        }



        protected void btnfind_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlclass.SelectedItem.Text == "Select")
                {
                    Alertme("Please select class", "warning");
                    ddlclass.Focus();
                    return;
                }
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session", "warning");
                    ddlsession.Focus();
                    return;
                }
                if (txt_section.Text == "")
                {
                    Alertme("Please enter section", "warning");
                    txt_section.Focus();
                    return;
                }
                if (txtrollnumber.Text == "")
                {
                    Alertme("Please enter roll no", "warning");
                    txtrollnumber.Focus();
                    return;
                }
                string query;
                query = "select * from admission_registor where class='" + ddlclass.SelectedItem.Text + "' and Session='" + ddlsession.SelectedItem.Text + "' and section='" + txt_section.Text + "' and rollnumber='" + txtrollnumber.Text + "' and StudentStatus='AV'    and Is_TC_Taken!='true' and   Status='1' order by id asc";
                find_details(query);
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }

        protected void btn_find_name_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_session_student.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session", "warning");
                    ddl_session_student.Focus();
                    return;
                }
                if (txt_student_name.Text == "")
                {
                    Alertme("Please enter student name.", "warning");
                    txt_student_name.Focus();
                    return;
                }
                string query = "";

                query = "select * from admission_registor where studentname like '%" + txt_student_name.Text + "%' and session='" + ddl_session_student.SelectedItem.Text + "' and StudentStatus='AV'   and Is_TC_Taken!='true' and  Status='1' order by id asc";



                DataTable dt = My.dataTable(query);
                if (dt.Rows.Count == 0)
                {
                    rp_std.DataSource = null;
                    rp_std.DataBind();
                    Alertme("Data Not Found...", "warning");
                    myModal2.Visible = false;
                }
                else
                {
                    rp_std.DataSource = dt;
                    rp_std.DataBind();
                    myModal2.Visible = true;
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

                query = "select * from admission_registor where admissionserialnumber='" + lbladmissionserialnumber.Text + "' and Session='" + lbl_session.Text + "' and StudentStatus='AV'   and Is_TC_Taken!='true' order by id asc";

                find_details(query);
                myModal2.Visible = false;
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            myModal2.Visible = false;
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_month.SelectedItem.Text == "Select")
                {
                    ddl_month.Focus();
                    Alertme("Please select month.", "warning");
                }
                else if (ddl_transfer_class.SelectedItem.Text == "Select")
                {
                    ddl_transfer_class.Focus();
                    Alertme("Please select class.", "warning");
                }
                else
                {
                    ViewState["IsUpdated"] = "0";
                    fetch_admission_fee();
                    if (ViewState["IsUpdated"].ToString() == "1")
                    {
                        Session["smsGS"] = "Student has been transferred successfully.";
                        Response.Redirect("transfer-student-to-new-class.aspx", false);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void fetch_admission_fee()
        {
            ViewState["Totl_admAnnulFee"] = "0";
            string parameter = ""; string type = "";
            if (ViewState["Transfer_Status"].ToString() == "New")
            {
                type = "Admission";
                parameter = ViewState["hostaltaken"].ToString().ToUpper() == "NO" ? "AdmissionFee" : "HostelAdmissionFee";
                string qryx = "select  sum(convert(float, net_payable)) as net_payable from (select (payable-cast(disc_amount as float)) net_payable from(select '0' payable_after_disc,fmc.session,cm.content feetype,cast(fmc.amount as float) payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + txt_admission_no.Text + "'  and (parameter_id='1' or parameter_id='5') and session='" + ddlsession.SelectedItem.Text + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id='" + ddl_transfer_class.SelectedValue + "' and admission_no='All'  and (parameter_id='1' or parameter_id='5') and session='" + ddlsession.SelectedItem.Text + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'))),0) disc_amount   from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='" + parameter + "'  and session='" + ddlsession.SelectedItem.Text + "' and class_id='" + ddl_transfer_class.SelectedValue + "')t)y";
                DataTable fee_dt = My.dataTable(qryx);
                if (fee_dt.Rows.Count > 0)
                {
                    ViewState["Totl_admAnnulFee"] = fee_dt.Rows[0]["net_payable"].ToString();
                }
                My.exeSql("delete from Admission_fee_collection where Admission_no='" + txt_admission_no.Text + "' and session='" + ddlsession.SelectedItem.Text + "'");

            }
            else
            {
                type = "Annual";
                parameter = ViewState["hostaltaken"].ToString().ToUpper() == "NO" ? "AnnualFee" : "HostelAnnualFee";
                string qryx = "select  sum(convert(float, net_payable)) as net_payable from (select (payable-cast(disc_amount as float)) net_payable from(select '0' payable_after_disc,fmc.session,cm.content feetype,cast(fmc.amount as float) payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + txt_admission_no.Text + "'  and parameter_id='2' and session='" + ddlsession.SelectedItem.Text + "' and fee_head_id=cm.content_id and Discount_on='Annual' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id='" + ddl_transfer_class.SelectedValue + "' and admission_no='All'  and parameter_id='2' and session='" + ddlsession.SelectedItem.Text + "' and fee_head_id=cm.content_id and Discount_on='Annual' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'))),0) disc_amount from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='AnnualFee' and session='" + ddlsession.SelectedItem.Text + "' and class_id='" + ddl_transfer_class.SelectedValue + "')t)y";
                DataTable fee_dt = My.dataTable(qryx);
                if (fee_dt.Rows.Count > 0)
                {
                    ViewState["Totl_admAnnulFee"] = fee_dt.Rows[0]["net_payable"].ToString();
                }
                My.exeSql("delete from Annual_fee_collection where Admission_no='" + txt_admission_no.Text + "' and session='" + ddlsession.SelectedItem.Text + "'");
            }
            My.exeSql("update admission_registor set  StudentStatus='Transferred', Transfer_Status='Transferred' where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and admissionserialnumber='" + txt_admission_no.Text + "'");
            My.exeSql("INSERT INTO admission_registor(class,formserialnumber,admissionserialnumber,rollnumber,Section,session,dateofadmission,studentname,dobverificationdocument1,dobverificationdocument2,gender,identifacationmark,currentschool,dob,fathername,fatherqualification,mothername,motherqualifiaction,guardianname,relation,occuption,religion,cast,parentincome,careof,city,postoffice,policestation,district,state,pin,mobilenumber,careof_permanent,city_permanent,postoffice_permanent,policestation_permanent,district_permanent,state_permanent,pincode,studentimagepath,signatureimagepath,transfercertifaceimagepath,dobproof,payment_status,hosteltaken,transportationtaken,Transportationpath,Transfer_Status,Busno,aadharno,RTE,StudentStatus,house,Transportation_Id,admission_idate,Pre_vious_rollnumber,Pre_vious_Section,roll_used,staff_ward,jati,mob2,Hostel_id,Session_id,Class_id,Is_TC_Taken,is_sync,Previous_admission_no,Student_id,is_applied_dayboarding,day_boarding_with_lunch,email_id,birth_certificate_number,place_of_birth,blood_group,cast_certificate_no,student_mother_tounge,is_illness,f_nationality,f_marrital_statue,m_marrital_statue,m_nationality,m_occupation,ration_type,illness_remark,father_mob,mother_mob,mother_email,Account_Holder_name,Bnk_Name,IFSC_Code,Branch_Name,lib_card_no,Course_Type,Academic_Sem_or_Year,Academic_Sem_or_Year_id,CET_ROLL_NO,CET_RANK,CET_CATEGORY,MAKAUT_Student_ID,MAKAUT_Student_password,Student_Whatsapp_No,Student_Other_Interest_Area,Category_id,SubCategory_id,Student_Name_First,Student_Middle_Name,Student_Name_Last,Father_Name_First,Father_Name_Middle,Father_Name_Last,Mother_Name_First,Mother_Name_Middle,Mother_Name_Last,Personal_Identymarks,Branch_id,Country_Code_Father,Country_Code_Mother,Country_Code_Current_add,Country_Code_Current_Perm_add,gcm_id,Pwd,Bank_acount_no,Status,Device_Id,Edit_Istatus,OTP,Verification_Istatus,Modelname,Version_name,Current_Semester_or_Year_id,Current_Semester_or_Year,User_id,College_School_Name,Divie_added_or_not,Old_Admission_Date,OLd_Admission_Idate,day_bording_with_lunch,Old_class_id,UID_No,mother_signature,Registration_type,Transfer_date,Transfer_idate,Country_Name_CA,Country_Name_PA,Index_no,Is_birth_certificate,Student_nationality,Is_cast_certificate,Staff_name,Prev_school_name) SELECT '" + ddl_transfer_class.SelectedItem.Text + "',formserialnumber,admissionserialnumber,rollnumber,Section,session,dateofadmission,studentname,dobverificationdocument1,dobverificationdocument2,gender,identifacationmark,currentschool,dob,fathername,fatherqualification,mothername,motherqualifiaction,guardianname,relation,occuption,religion,cast,parentincome,careof,city,postoffice,policestation,district,state,pin,mobilenumber,careof_permanent,city_permanent,postoffice_permanent,policestation_permanent,district_permanent,state_permanent,pincode,studentimagepath,signatureimagepath,transfercertifaceimagepath,dobproof,payment_status,hosteltaken,transportationtaken,Transportationpath,Transfer_Status,Busno,aadharno,RTE,'AV',house,Transportation_Id,admission_idate,Pre_vious_rollnumber,Pre_vious_Section,roll_used,staff_ward,jati,mob2,Hostel_id,Session_id,'" + ddl_transfer_class.SelectedValue + "',Is_TC_Taken,is_sync,Previous_admission_no,Student_id,is_applied_dayboarding,day_boarding_with_lunch,email_id,birth_certificate_number,place_of_birth,blood_group,cast_certificate_no,student_mother_tounge,is_illness,f_nationality,f_marrital_statue,m_marrital_statue,m_nationality,m_occupation,ration_type,illness_remark,father_mob,mother_mob,mother_email,Account_Holder_name,Bnk_Name,IFSC_Code,Branch_Name,lib_card_no,Course_Type,Academic_Sem_or_Year,Academic_Sem_or_Year_id,CET_ROLL_NO,CET_RANK,CET_CATEGORY,MAKAUT_Student_ID,MAKAUT_Student_password,Student_Whatsapp_No,Student_Other_Interest_Area,Category_id,SubCategory_id,Student_Name_First,Student_Middle_Name,Student_Name_Last,Father_Name_First,Father_Name_Middle,Father_Name_Last,Mother_Name_First,Mother_Name_Middle,Mother_Name_Last,Personal_Identymarks,Branch_id,Country_Code_Father,Country_Code_Mother,Country_Code_Current_add,Country_Code_Current_Perm_add,gcm_id,Pwd,Bank_acount_no,Status,Device_Id,Edit_Istatus,OTP,Verification_Istatus,Modelname,Version_name,Current_Semester_or_Year_id,Current_Semester_or_Year,User_id,College_School_Name,Divie_added_or_not,Old_Admission_Date,OLd_Admission_Idate,day_bording_with_lunch,Old_class_id,UID_No,mother_signature,Registration_type,Transfer_date,Transfer_idate,Country_Name_CA,Country_Name_PA,Index_no,Is_birth_certificate,Student_nationality,Is_cast_certificate,Staff_name,Prev_school_name FROM admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and admissionserialnumber='" + txt_admission_no.Text + "'");

            // mycode.executequery("update Student_Payment_History set Delete_status='555' where Addmission_no='" + txt_admission_no.Text + "' and Session='" + ddlsession.SelectedItem.Text + "' and parameter_New='" + parameter + "'; update Typewise_fee_collection set Delete_status=555 where admission_no='" + txt_admission_no.Text + "' and session='" + ddlsession.SelectedItem.Text + "' and parameter='" + parameter + "'");

            DataTable dt = My.dataTable("select * from Student_Payment_History where Addmission_no='" + txt_admission_no.Text + "' and Session='" + ddlsession.SelectedItem.Text + "' and parameter_New='" + parameter + "' and Class_id='" + ddlclass.SelectedValue + "'");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    send_data_in_student_payment_history(type, dr["Slip_no"].ToString(), dr["Entry_id"].ToString(), txt_admission_no.Text, parameter, dr["Date"].ToString(), dr["Amount"].ToString(), dr["mode"].ToString(), dr["Pay_mode_transaction_no"].ToString());
                    create_admission_annual_dues(parameter, dr["Date"].ToString(), type);
                    send_data_in_feetypewise_collection(dr["Slip_no"].ToString(), dr["Entry_id"].ToString(), parameter, dr["Date"].ToString(), dr["Amount"].ToString(), dr["mode"].ToString(), dr["Pay_mode_transaction_no"].ToString());
                    if (type == "Admission")
                    {
                        send_data_to_admission_fee_collection(dr["Slip_no"].ToString(), dr["Entry_id"].ToString(), parameter, dr["Date"].ToString(), dr["Amount"].ToString(), dr["mode"].ToString(), dr["Pay_mode_transaction_no"].ToString());
                    }
                    else
                    {
                        send_data_to_annual_fee_collection(dr["Slip_no"].ToString(), dr["Entry_id"].ToString(), parameter, dr["Date"].ToString(), dr["Amount"].ToString(), dr["mode"].ToString(), dr["Pay_mode_transaction_no"].ToString());
                    }
                }
            }
            update_data_to_admission_registor(type);
            update_monthly_fee();
            My.exeSql("delete from Typewise_fee_collection where admission_no='" + txt_admission_no.Text + "' and class='" + ddlclass.SelectedItem.Text + "' and session='" + ddlsession.SelectedItem.Text + "' and parameter='" + parameter + "'; delete from Student_Payment_History where Addmission_no='" + txt_admission_no.Text + "' and Class_id='" + ddlclass.SelectedValue + "' and Session='" + ddlsession.SelectedItem.Text + "' and parameter_New='" + parameter + "'; delete from Monthly_Fee_Collection_Slip where adno='" + txt_admission_no.Text + "' and class='" + ddlclass.SelectedItem.Text + "' and session='" + ddlsession.SelectedItem.Text + "' and parameter='" + parameter + "'");
            ViewState["IsUpdated"] = "1";
        }

        private void update_monthly_fee()
        {
            DataTable dt = mycode.FillData(" select *  from dbo.[Month_Index] where Position<" + ddl_month.SelectedValue + " order by Position");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    string Month_Id = dt.Rows[j]["Month_Id"].ToString();
                    string Month = dt.Rows[j]["Month"].ToString();
                    string Position = dt.Rows[j]["Position"].ToString();
                    Update_month_type_wise_fee_class_wise(Month_Id, Month, Position, ddlsession.SelectedValue, txt_admission_no.Text, lblhostel.Text, ddlsession.SelectedItem.Text, ddl_transfer_class.SelectedItem.Text, ViewState["Section"].ToString(), ddl_transfer_class.SelectedValue);
                }
            }
        }



        private void Update_month_type_wise_fee_class_wise(string month_Id, string month, string Position, string session_id, string admissionserialnumber, string hosteltaken, string session, string classname, string section, string class_id)
        {
            ViewState["parameter"] = hosteltaken.ToLower() == "yes" ? "HostelMonthlyFee" : "MonthlyFee";
            string query = "Select * from Typewise_fee_collection where admission_no='" + admissionserialnumber + "' and parameter='" + ViewState["parameter"].ToString() + "' and session='" + session + "' and month='" + month + "'";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                SqlCommand cmd;
                string query2 = "INSERT INTO Typewise_fee_collection (admission_no,class,session,section,parameter,Date,idate,feetype,payable,paid,dues,status,month,user_id,content_id,transection,Ledger,is_readyfor_sync,is_sync,is_sync_dues_diary,Previous_admission_no,group_id,class_id,position,Disc,Payable_after_disc,delete_sync,branchid,Acamedic_Semester_Id,Is_month_skip) values (@admission_no,@class,@session,@section,@parameter,@Date,@idate,@feetype,@payable,@paid,@dues,@status,@month,@user_id,@content_id,@transection,@Ledger,@is_readyfor_sync,@is_sync,@is_sync_dues_diary,@Previous_admission_no,@group_id,@class_id,@position,@Disc,@Payable_after_disc,@delete_sync,@branchid,@Acamedic_Semester_Id,@Is_month_skip)";
                cmd = new SqlCommand(query2);
                cmd.Parameters.AddWithValue("@admission_no", admissionserialnumber);
                cmd.Parameters.AddWithValue("@class", classname);
                cmd.Parameters.AddWithValue("@session", session);
                cmd.Parameters.AddWithValue("@section", section);
                cmd.Parameters.AddWithValue("@parameter", ViewState["parameter"].ToString());
                cmd.Parameters.AddWithValue("@Date", mycode.date());
                cmd.Parameters.AddWithValue("@idate", mycode.idate());
                cmd.Parameters.AddWithValue("@feetype", "");
                cmd.Parameters.AddWithValue("@payable", "00.00");
                cmd.Parameters.AddWithValue("@paid", "0.00");
                cmd.Parameters.AddWithValue("@dues", "0.00");
                cmd.Parameters.AddWithValue("@status", "Paid");
                cmd.Parameters.AddWithValue("@month", month);
                cmd.Parameters.AddWithValue("@user_id", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@content_id", "0");
                cmd.Parameters.AddWithValue("@transection", "0");
                cmd.Parameters.AddWithValue("@Ledger", "School");
                cmd.Parameters.AddWithValue("@is_readyfor_sync", 0);
                cmd.Parameters.AddWithValue("@is_sync", 0);
                cmd.Parameters.AddWithValue("@is_sync_dues_diary", 0);
                cmd.Parameters.AddWithValue("@Previous_admission_no", 0);
                cmd.Parameters.AddWithValue("@group_id", "3");
                cmd.Parameters.AddWithValue("@class_id", class_id);
                cmd.Parameters.AddWithValue("@position", Position);
                cmd.Parameters.AddWithValue("@Disc", "0");
                cmd.Parameters.AddWithValue("@Payable_after_disc", "0");
                cmd.Parameters.AddWithValue("@delete_sync", "0");
                cmd.Parameters.AddWithValue("@branchid", ViewState["Branchid"].ToString());
                cmd.Parameters.AddWithValue("@Acamedic_Semester_Id", "0");
                cmd.Parameters.AddWithValue("@Is_month_skip", 1);
                if (My.InsertUpdateData(cmd))
                {
                }
            }
            else
            {
            }
        }

        private void update_data_to_admission_registor(string typeS)
        {
            DataTable dtDues = new DataTable();
            if (typeS == "Admission")
            {
                dtDues = My.dataTable("select ad.Payable_amount,ad.Paid_amount,ad.Dues_amount from admission_registor ar left join Admission_fee_collection ad on ar.admissionserialnumber=ad.Admission_no and ar.session=ad.session where ar.Session_id ='" + ddlsession.SelectedValue + "' and (ar.payment_status!= 'Paid' or ar.payment_status='Dues' or ar.payment_status='Unpaid') and ar.Transfer_Status='New' and ar.Status='1' and ar.admissionserialnumber='" + txt_admission_no.Text + "' and Class_id='" + ddl_transfer_class.SelectedValue + "'");
            }
            else
            {
                dtDues = My.dataTable("select ad.Payable_amount,ad.Paid_amount,ad.Dues_amount from admission_registor ar left join Annual_fee_collection ad on ar.admissionserialnumber=ad.Admission_no and ar.session=ad.session where ar.Session_id ='" + ddlsession.SelectedValue + "' and (ar.payment_status!= 'Paid' or ar.payment_status='Dues' or ar.payment_status='Unpaid') and ar.Transfer_Status='NT' and ar.Status='1' and ar.admissionserialnumber='" + txt_admission_no.Text + "' and Class_id='" + ddl_transfer_class.SelectedValue + "'");
            }
            string ttldues = "1";
            if (dtDues.Rows.Count > 0)
            {
                ttldues = dtDues.Rows[0]["Dues_amount"].ToString();
            }
            SqlConnection conn = new SqlConnection(My.conn);
            SqlDataAdapter ad = new SqlDataAdapter("select * from admission_registor where admissionserialnumber = '" + txt_admission_no.Text + "' and session='" + ddlsession.SelectedItem.Text + "' and Class_id='" + ddl_transfer_class.SelectedValue + "'", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                if (My.toDouble(ttldues) <= 0)
                {
                    dr["payment_status"] = "Paid";
                }
                else if (My.toDouble(ttldues) > 0)
                {
                    dr["payment_status"] = "Dues";
                }
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
        }

        private void send_data_to_annual_fee_collection(string slip_no, string entry_id, string parameter, string Date, string Amount, string mode, string Pay_mode_transaction_no)
        {
            DataTable pdt = My.dataTable("select sum(cast(paid as float)) paid,sum(cast(dues as float)) dues from Typewise_fee_collection where admission_no='" + txt_admission_no.Text + "' and session='" + ddlsession.SelectedItem.Text + "' and  parameter like '%AnnualFee%' and feetype!='Previous Dues' and class='" + ddl_transfer_class.SelectedItem.Text + "'");

            //.ToString("0.00")
            SqlConnection conn = new SqlConnection(My.conn);
            SqlDataAdapter ad = new SqlDataAdapter("select * from Annual_fee_collection where Admission_no='" + txt_admission_no.Text + "' and session='" + ddlsession.SelectedItem.Text + "'", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                DataRow dr = dt.NewRow();
                dr[1] = txt_admission_no.Text;
                dr[2] = My.toDouble(ViewState["Totl_admAnnulFee"].ToString()).ToString("0.00");
                dr[3] = "0";
                dr[4] = My.toDouble(ViewState["Totl_admAnnulFee"].ToString()).ToString("0.00");
                dr[5] = My.toDouble(pdt.Rows[0]["paid"].ToString()).ToString("0.00");
                dr[6] = My.toDouble(My.toDouble(dr[4]) - My.toDouble(dr[5])).ToString("0.00");
                dr[7] = Date;
                dr[8] = mode;
                dr[9] = slip_no;
                dr["session"] = ddlsession.SelectedItem.Text;
                dr["idate"] = Convert.ToDateTime(Date).ToString("yyyyMMdd");
                dr["remark"] = "Transfer to new class";
                dr["time"] = mycode.time();
                dr["user_id"] = ViewState["Userid"].ToString(); ;
                dr["Acamedic_Semester_Id"] = "0";
                dr["branchid"] = ViewState["Branchid"].ToString();
                dt.Rows.Add(dr);
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    dr[5] = My.toDouble(pdt.Rows[0]["paid"].ToString()).ToString("0.00");
                    dr[6] = My.toDouble(My.toDouble(dr[4]) - My.toDouble(dr[5])).ToString("0.00");
                }
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
        }

        private void send_data_to_admission_fee_collection(string slip_no, string entry_id, string parameter, string Date, string Amount, string mode, string Pay_mode_transaction_no)
        {
            DataTable pdt = My.dataTable("select sum(cast(paid as float)) paid,sum(cast(dues as float)) dues from dbo.[Typewise_fee_collection] where admission_no='" + txt_admission_no.Text + "' and session='" + ddlsession.SelectedItem.Text + "' and  parameter like '%" + parameter + "%' and feetype!='Previous Dues' and class='" + ddl_transfer_class.SelectedItem.Text + "' ");

            SqlConnection conn = new SqlConnection(My.conn);
            SqlDataAdapter ad = new SqlDataAdapter("select * from Admission_fee_collection where Admission_no='" + txt_admission_no.Text + "' and session='" + ddlsession.SelectedItem.Text + "'", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                DataRow dr = dt.NewRow();
                dr[1] = txt_admission_no.Text;
                dr[2] = My.toDouble(ViewState["Totl_admAnnulFee"].ToString()).ToString("0.00");
                dr[3] = My.toDouble("0").ToString("0.00");
                dr[4] = My.toDouble(ViewState["Totl_admAnnulFee"].ToString()).ToString("0.00");
                dr[5] = My.toDouble(pdt.Rows[0]["paid"].ToString()).ToString("0.00");
                dr[6] = My.toDouble(My.toDouble(dr[4]) - My.toDouble(dr[5])).ToString("0.00");
                dr[7] = Date;
                dr[8] = mode;
                dr[9] = slip_no;
                dr["session"] = ddlsession.SelectedItem.Text;
                dr["idate"] = Convert.ToDateTime(Date).ToString("yyyyMMdd");
                dr["remark"] = "Transfer to new class.";
                dr["entry_id"] = entry_id;
                dr["time"] = mycode.time();
                dr["user_id"] = ViewState["Userid"].ToString(); ;
                dr["Slip_no"] = slip_no;
                dr["Acamedic_Semester_Id"] = "0";
                dr["branchid"] = ViewState["Branchid"].ToString();
                dt.Rows.Add(dr);
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    dr[5] = My.toDouble(pdt.Rows[0]["paid"].ToString()).ToString("0.00");
                    dr[6] = My.toDouble(My.toDouble(dr[4]) - My.toDouble(dr[5])).ToString("0.00");
                }
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
        }
        private void send_data_in_feetypewise_collection(string slip_no, string entry_id, string parameter, string dates, string Amount, string PaymodE, string Pay_mode_transaction_no)
        {
            // monyricpt
            string class_id = ddl_transfer_class.SelectedValue;
            double paid_amount = My.toDouble(Amount);
            SqlDataAdapter ad = new SqlDataAdapter("select * from Typewise_fee_collection where admission_no='" + txt_admission_no.Text + "' and session='" + ddlsession.SelectedItem.Text + "' and status='Dues' and parameter='" + parameter + "' and class='" + ddl_transfer_class.SelectedItem.Text + "'", My.conn);
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
                    if (paid_amount >= 0)
                    {
                        string group_id = parameter.Contains("Monthly") ? "3" : parameter.Contains("Annual") ? "2" : "1";
                        double dues = My.toDouble(dr["payable"]) - My.toDouble(dr["paid"]) - My.toDouble(dr["Disc"]);
                        dr["Date"] = dates;
                        dr["idate"] = My.toDateTime(dates).ToString("yyyyMMdd");
                        if (paid_amount >= dues)
                        {
                            string prevpaid = dr["paid"].ToString();
                            paid_amount = paid_amount - dues;
                            //paid amt is gratter than dues so dues amt is actual paid.
                            string paid = dr["dues"].ToString();
                            dr["Payable_after_disc"] = My.toDouble(My.toDouble(dr["payable"]) - My.toDouble(dr["Disc"])).ToString("0.00");
                            dr["paid"] = My.toDouble(dr["Payable_after_disc"]).ToString("0.00");
                            dr["dues"] = "0";
                            dr["status"] = "Paid";
                            #region send in collection slip
                            send_data_in_fee_collection_slip(My.toDouble(dr["payable"].ToString()).ToString("0.00"), My.toDouble(dr["paid"].ToString()).ToString("0.00"), dr["parameter"], dr["feetype"], dr["content_id"], slip_no, entry_id, dr["Ledger"], txt_admission_no.Text, ddlsession.SelectedItem.Text, ddl_transfer_class.SelectedValue, ViewState["Section"].ToString(), My.toDouble(dr["Disc"].ToString()).ToString("0.00"), dr["month"], dr["position"], prevpaid, dates);
                            #endregion
                        }
                        else
                        {
                            string prevpaid = dr["paid"].ToString();
                            dr["Payable_after_disc"] = My.toDouble(My.toDouble(dr["payable"]) - My.toDouble(dr["Disc"])).ToString("0.00");
                            dr["paid"] = My.toDouble(My.toDouble(dr["paid"]) + paid_amount).ToString("0.00");
                            dr["dues"] = My.toDouble(My.toDouble(dr["Payable_after_disc"]) - My.toDouble(dr["paid"])).ToString("0.00");
                            dr["status"] = "Dues";

                            #region send in collection slip
                            send_data_in_fee_collection_slip(My.toDouble(dr["payable"].ToString()).ToString("0.00"), paid_amount.ToString("0.00"), dr["parameter"], dr["feetype"], dr["content_id"], slip_no, entry_id, dr["Ledger"], txt_admission_no.Text, ddlsession.SelectedItem.Text, ddl_transfer_class.SelectedValue, ViewState["Section"].ToString(), My.toDouble(dr["Disc"].ToString()).ToString("0.00"), dr["month"], dr["position"], prevpaid, dates);
                            #endregion
                            paid_amount = 0;
                        }
                        dr["transection"] = slip_no;
                        dr["is_readyfor_sync"] = true;
                        dr["is_sync"] = false;
                        dr["group_id"] = group_id;
                        dr["class_id"] = class_id;
                    }
                    else
                    {
                        break;
                    }
                }
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(tdt);
            }
        }

        private void send_data_in_fee_collection_slip(string payable, string paid, object parameter, object content, object content_id, string slip_no, string entry_id, object ledger, string adno, string session, string classs, string sction, string disc_on_feehead, object month, object monthid, string prevpaid, string dates)
        {
            if (ledger == "")
            {
                ledger = "School";
            }
            string qry = "insert into Monthly_Fee_Collection_Slip(adno,session,class,Section,parameter,Content,content_id,payable,paid,slipno,Ledger,disc_amt,Month,month_position,previously_paid,branchid,academicyear,Date,Idate) values ('" + adno + "','" + session + "','" + classs + "','" + sction + "','" + parameter + "','" + content + "','" + content_id + "','" + payable + "','" + paid + "','" + slip_no + "','" + ledger + "','" + disc_on_feehead + "','" + month + "','" + monthid + "','" + prevpaid + "','" + ViewState["Branchid"].ToString() + "','0','" + dates + "','" + My.DateConvertToIdate(dates) + "');";
            My.exeSql(qry);
        }


        private void create_admission_annual_dues(string parameter, string date, string type)
        {
            if (type == "Admission")
            {
                if (My.dataTable("select session,feetype,payable,paid,dues,status,content_id from Typewise_fee_collection WHERE admission_no='" + txt_admission_no.Text + "' and parameter='" + parameter + "' and session='" + ddlsession.SelectedItem.Text + "'  and class='" + ddl_transfer_class.SelectedItem.Text + "'").Rows.Count == 0)
                {
                    string query = "select '0' payable_after_disc,fmc.session,cm.content feetype,fmc.amount payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, (isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + txt_admission_no.Text + "'  and (parameter_id='1' or parameter_id='5') and session='" + ddlsession.SelectedItem.Text + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id='" + ddl_transfer_class.SelectedValue + "' and admission_no='All'  and (parameter_id='1' or parameter_id='5') and session='" + ddlsession.SelectedItem.Text + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'))) disc_amount  from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='" + parameter + "' and session='" + ddlsession.SelectedItem.Text + "' and class_id='" + ddl_transfer_class.SelectedValue + "'";
                    DataTable dt = mycode.FillData(query);
                    if (dt.Rows.Count == 0)
                    {
                    }
                    else
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            My.exeSql("insert into Typewise_fee_collection(admission_no,class,session,section,parameter,Date,idate,feetype,payable,paid,dues,status,month,content_id,transection,Ledger,is_readyfor_sync,is_sync,is_sync_dues_diary,Disc,Payable_after_disc,branchid,Acamedic_Semester_Id,class_id,user_id) values ('" + txt_admission_no.Text + "','" + ddl_transfer_class.SelectedItem.Text + "','" + ddlsession.SelectedItem.Text + "','" + ViewState["Section"].ToString() + "','" + parameter + "','" + date + "','" + My.DateConvertToIdate(date) + "','" + dr["feetype"].ToString() + "','" + My.toDouble(dr["payable"].ToString()).ToString("0.00") + "','0','" + My.toDouble(dr["payable"].ToString()).ToString("0.00") + "','Dues','" + My.get_start_month() + "','" + dr["content_id"].ToString() + "','','School','false','false','false','" + My.toDouble(dr["disc_amount"].ToString()).ToString("0.00") + "','" + My.toDouble(dr["payable_after_disc"].ToString()).ToString("0.00") + "','" + ViewState["Branchid"].ToString() + "','0','" + ViewState["classid"].ToString() + "','" + ViewState["Userid"].ToString() + "')");
                        }
                    }
                }
            }
            else
            {
                if (My.dataTable("select session,feetype,payable,paid,dues,status,content_id from dbo.[Typewise_fee_collection] WHERE admission_no='" + txt_admission_no.Text + "' and parameter='" + parameter + "' and session='" + ddlsession.SelectedItem.Text + "' and class='" + ddl_transfer_class.SelectedItem.Text + "'").Rows.Count == 0)
                {
                    string query = "select '0' payable_after_disc,fmc.session,cm.content feetype,fmc.amount payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, (isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + txt_admission_no.Text + "' and parameter_id='2' and session='" + ddlsession.SelectedItem.Text + "' and fee_head_id=cm.content_id and Discount_on='Annual' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id='" + ddl_transfer_class.SelectedValue + "' and admission_no='All'  and parameter_id='2' and session='" + ddlsession.SelectedItem.Text + "' and fee_head_id=cm.content_id and Discount_on='Annual' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'))) disc_amount from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='" + parameter + "' and session='" + ddlsession.SelectedItem.Text + "' and class_id='" + ddl_transfer_class.SelectedValue + "'";
                    DataTable dt = mycode.FillData(query);
                    if (dt.Rows.Count == 0)
                    {
                    }
                    else
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            My.exeSql("insert into Typewise_fee_collection(admission_no,class,session,section,parameter,Date,idate,feetype,payable,paid,dues,status,month,content_id,transection,Ledger,is_readyfor_sync,is_sync,is_sync_dues_diary,Disc,Payable_after_disc,branchid,Acamedic_Semester_Id,class_id,user_id) values ('" + txt_admission_no.Text + "','" + ddl_transfer_class.SelectedItem.Text + "','" + dr["session"].ToString() + "','" + ViewState["Section"].ToString() + "','" + parameter + "','" + date + "','" + My.DateConvertToIdate(date) + "','" + dr["feetype"].ToString() + "','" + My.toDouble(dr["payable"].ToString()).ToString("0.00") + "','0','" + My.toDouble(dr["payable"].ToString()).ToString("0.00") + "','Dues','" + My.get_start_month() + "','" + dr["content_id"].ToString() + "','','School','false','false','false','" + My.toDouble(dr["disc_amount"].ToString()).ToString("0.00") + "','" + My.toDouble(dr["payable_after_disc"].ToString()).ToString("0.00") + "','" + ViewState["Branchid"].ToString() + "','0','" + ViewState["classid"].ToString() + "','" + ViewState["Userid"].ToString() + "')");
                        }
                    }
                }
            }
        }


        private void send_data_in_student_payment_history(string type, string slip_no, string entry_id, string ad_no, string parameter_New, string dates, string Amount, string PaymodE, string Pay_mode_transaction_no)
        {
            SqlCommand cmd;
            string query = "insert into Student_Payment_History(Addmission_no,Session,Date,Idate,Description,Entry_id,Slip_no,Amount,Type,mode,discount,Discoun_in_School,Discoun_in_Hostel,Discoun_in_Transport,fine,is_ofline_sync,Is_online_sync,is_update_in_online,Previous_admission_no,App_Transection_id,time,user_id,Acamedic_Semester_Id,Branch,Class_id,User_Slip_no,Pay_mode_transaction_no,Remarks,Transection_in,parameter_New) values (@Addmission_no,@Session,@Date,@Idate,@Description,@Entry_id,@Slip_no,@Amount,@Type,@mode,@discount,@Discoun_in_School,@Discoun_in_Hostel,@Discoun_in_Transport,@fine,@is_ofline_sync,@Is_online_sync,@is_update_in_online,@Previous_admission_no,@App_Transection_id,@time,@user_id,@Acamedic_Semester_Id,@Branch,@Class_id,@User_Slip_no,@Pay_mode_transaction_no,@Remarks,@Transection_in,@parameter_New)";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Addmission_no", ad_no);
            cmd.Parameters.AddWithValue("@Session", ddlsession.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@Date", dates);
            cmd.Parameters.AddWithValue("@Idate", Convert.ToDateTime(dates).ToString("yyyyMMdd"));
            cmd.Parameters.AddWithValue("@Description", type + " fee collection for " + lbl_name.Text + " Paid Amount : " + Amount + " /-");
            cmd.Parameters.AddWithValue("@Entry_id", entry_id);
            cmd.Parameters.AddWithValue("@Slip_no", slip_no);
            cmd.Parameters.AddWithValue("@Amount", My.toDouble(Amount).ToString("0.00"));
            cmd.Parameters.AddWithValue("@Type", type);
            cmd.Parameters.AddWithValue("@mode", PaymodE);
            cmd.Parameters.AddWithValue("@discount", My.toDouble("0").ToString("0.00"));
            cmd.Parameters.AddWithValue("@Discoun_in_School", "0.00");
            cmd.Parameters.AddWithValue("@Discoun_in_Hostel", "0.00");
            cmd.Parameters.AddWithValue("@Discoun_in_Transport", "0.00");
            cmd.Parameters.AddWithValue("@fine", "0.00");
            cmd.Parameters.AddWithValue("@is_ofline_sync", 0);
            cmd.Parameters.AddWithValue("@Is_online_sync", 0);
            cmd.Parameters.AddWithValue("@is_update_in_online", 0);
            cmd.Parameters.AddWithValue("@Previous_admission_no", 0);
            cmd.Parameters.AddWithValue("@App_Transection_id", "");
            cmd.Parameters.AddWithValue("@time", mycode.time());
            cmd.Parameters.AddWithValue("@user_id", ViewState["Userid"].ToString());
            cmd.Parameters.AddWithValue("@Acamedic_Semester_Id", "0");
            cmd.Parameters.AddWithValue("@Branch", ViewState["Branchid"].ToString());
            cmd.Parameters.AddWithValue("@Class_id", ddl_transfer_class.SelectedValue);
            cmd.Parameters.AddWithValue("@Remarks", "Transfer in new class");
            cmd.Parameters.AddWithValue("@User_Slip_no", "");
            cmd.Parameters.AddWithValue("@Pay_mode_transaction_no", Pay_mode_transaction_no);
            cmd.Parameters.AddWithValue("@Transection_in", "Software");
            cmd.Parameters.AddWithValue("@parameter_New", parameter_New);
            if (My.InsertUpdateData(cmd))
            {
            }
        }

    }
}