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
    public partial class student_payment_summary : System.Web.UI.Page
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
                        string pagename_current = "fee-report.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        if (ViewState["Is_Print"].ToString() == "1")
                        {
                            print1.Visible = true;
                        }
                        else
                        {
                            print1.Visible = false;
                        }

                        lbl_class22.Text = mycode.date() + "-" + mycode.time();
                        ViewState["checked_after_frst_mnth"] = "0";
                        ViewState["MnthName"] = "0";
                        ViewState["checked_frst_mnth"] = "0";
                        ViewState["Isadmissionfeetekent"] = My.get_admission_condition();
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        ViewState["Branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        ViewState["fineAmt"] = "0";
                        ViewState["checked_mnth"] = "0";
                        ViewState["flags1"] = "0";
                        ViewState["fine_inserted"] = "0";
                        ViewState["Other_Fees"] = "0";
                        ViewState["late_fine_no_of_day_month"] = "0";
                        ViewState["fine_date_From"] = "0";
                        ViewState["fine_date_To"] = "0";
                        ViewState["FineType"] = "0";

                        mycode.bind_all_ddl_with_id(ddlclass, "Select Course_Name,course_id from Add_course_table order by Position asc");
                        mycode.bind_all_ddl_with_id(ddlsession, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int)");
                        mycode.bind_all_ddl_with_id(ddl_session_student, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int)");
                        mycode.bind_all_ddl_with_id(ddlsessionad, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int)");

                        ddlsessionad.SelectedValue = My.get_session_id();
                        ddlsession.SelectedValue = My.get_session_id();
                        ddl_session_student.SelectedValue = My.get_session_id();
                        find_firm_details(); 
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "MonthlyFeePayment");
            }
        }
        private void fetch_signature()
        { 
            DataTable dt = mycode.FillData("select Signature from user_details where User_Type='Accountant'");
            if (dt.Rows.Count > 0)
            {
                signDVS.Visible = true;
                Image1.ImageUrl = dt.Rows[0]["Signature"].ToString();
            }
            else
            {
                signDVS.Visible = false;
            }
        }
        private void find_firm_details()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count > 0)
            {
                imglogo.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_emaiid.Text = dt.Rows[0]["email"].ToString();
                lbl_website.Text = dt.Rows[0]["website"].ToString();
                lbl_contact_details.Text = dt.Rows[0]["contact_no"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();
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
        public static List<string> GetRooPath(string PathRooT,string Session_id)
        {
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(My.conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct studentname from admission_registor where studentname LIKE ''+@SearchMobNo+'%' and Session_id="+ Session_id + "   and Status='1'";
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
        public static List<string> GetRooPathAdmNo(string PathRooT,string Session_id)
        {
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(My.conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct admissionserialnumber from admission_registor where admissionserialnumber LIKE ''+@SearchMobNo+'%'  and Status='1' and Session_id="+ Session_id + "";
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
                //empty_form();
                string query = "";
                if (ViewState["Isadmissionfeetekent"].ToString() == "1")// admission fee not mindtree
                {
                    query = "select * from admission_registor where admissionserialnumber='" + txt_admission_no.Text + "' and Session='" + ddlsessionad.SelectedItem.Text + "' and StudentStatus='AV'  and  Is_TC_Taken!='true' and Status='1' order by id asc";
                }
                else
                {

                    query = "select * from admission_registor where admissionserialnumber='" + txt_admission_no.Text + "' and Session='" + ddlsessionad.SelectedItem.Text + "' and StudentStatus='AV'  and payment_status!='Unpaid' and Is_TC_Taken!='true' and Status='1' order by id asc";
                }



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

                    ddlsessionad.SelectedValue = dr["Session_id"].ToString();

                    ddl_session_student.SelectedValue = dr["Session_id"].ToString();


                    txt_section.Text = dr["Section"].ToString();
                    txtrollnumber.Text = dr["rollnumber"].ToString();
                    lbl_name.Text = dr["studentname"].ToString();
                    lbl_father_name.Text = dr["fathername"].ToString();
                    lblclass.Text = dr["class"].ToString();
                    ViewState["class_id"] = dr["Class_id"].ToString();
                    txtsection.Text = dr["Section"].ToString();
                    lbl_admission_no.Text = dr["admissionserialnumber"].ToString();
                    txt_admission_no.Text = dr["admissionserialnumber"].ToString();
                    if(dr["hosteltaken"].ToString()=="")
                    {
                        lblhostel.Text = "No";
                    }
                    else
                    {
                        lblhostel.Text = dr["hosteltaken"].ToString();
                    }
                   

                    ViewState["parameter"] = dr["hosteltaken"].ToString().ToUpper() == "YES" ? "HostelMonthlyFee" : "MonthlyFee";
                    ViewState["parameter_id"] = dr["hosteltaken"].ToString().ToUpper() == "YES" ? "3" : "4";
                    ViewState["Admission_no"] = dr["admissionserialnumber"].ToString();
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
                    ViewState["Session"] = dr["session"].ToString();

                    ViewState["Transfer_Status"] = dr["Transfer_Status"].ToString();

                    if (ViewState["Transfer_Status"].ToString() == "New")
                    {
                        lbl_studentype.Text = "New";
                        lbl_Fee_type.Text = "Admission Fee";
                    }
                    else
                    {
                        lbl_studentype.Text = "Annual Fee";
                        lbl_Fee_type.Text = "Annual Fee";
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


                    Dictionary<string, object> dc1 = mycode.Bind_hostel_data_for_assined_student(dr["Session_id"].ToString(), dr["Class_id"].ToString(), ViewState["Admission_no"].ToString());
                    ViewState["Hostel_id"] = (String)dc1["Hostel_id"];
                    ViewState["Room_Category_id"] = (String)dc1["Room_Category_id"];
                    ViewState["From_month_name"] = (String)dc1["From_month_name"];
                    ViewState["From_month_id"] = (String)dc1["From_month_id"];
                    ViewState["Assined_Year_Month"] = (String)dc1["Assined_Year_Month"];
                    ViewState["Hostel_assign_id"] = (String)dc1["Hostel_assign_id"];

                    Dictionary<string, object> dc2 = mycode.Bind_Transport_data_for_assined_student(ViewState["sessionIDs"].ToString(), ViewState["classid"].ToString(), ViewState["Admission_no"].ToString());
                    ViewState["Transport_id"] = (String)dc2["Transport_id"];
                    ViewState["TransportPath_id"] = (String)dc2["TransportPath_id"];
                    ViewState["Boarding_Point_id"] = (String)dc2["Boarding_Point_id"];
                    ViewState["Transport_Assigned_Id"] = (String)dc2["Transport_Assigned_Id"];
                    ViewState["Month_name"] = (String)dc2["Month_name"];
                    ViewState["Month_id"] = (String)dc2["Month_id"];
                    ViewState["Year_month"] = (String)dc2["Year_month"];
                    ViewState["Sheet_Id"] = (String)dc2["Sheet_Id"];



                    lbltransporttion.Text = dr["transportationtaken"].ToString();
                    if (dr["Transportation_Id"].ToString() == "")
                    {
                        ViewState["transportID"] = "0";
                    }
                    else
                    {
                        ViewState["transportID"] = dr["Transportation_Id"].ToString();
                    }
                }
                fetch_admission_fee();
                fetch_monthly_paid_fees();
                find_monthly_dues_fees();
                fetch_ledger(); 
                fetch_signature();
                fetch_annual_admission_fee();
            }
        }

        private void fetch_annual_admission_fee()
        {

           string  qry = "select   t1.admissionserialnumber as Admission_no,t1.studentname as Student_Name,t1.class,t1.Section,t1.rollnumber,t1.fathername as Father_Name,t1.father_mob,t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch,gcm_id from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id join Add_course_table t3 on t1.Class_id=t3.course_id where t1.Session_id='" + ViewState["sessionIDs"].ToString() + "' and t1.Status='1' and t1.admissionserialnumber='" + txt_admission_no.Text + "' and t1.Class_id='"+ ViewState["classid"].ToString() + "'  ";

            SqlDataAdapter ad_contactus = new SqlDataAdapter(qry, My.conn);
            DataSet ds = new DataSet();
            ad_contactus.Fill(ds);
            DataTable dt = ds.Tables[0];
            int srowcount = dt.Rows.Count;

            if (srowcount == 0)
            {


            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                     get_adm_annual_dues(dr);
                }



            }

        }

        private void get_adm_annual_dues(DataRow drx)
        {
           
         
            string qry = "";
            string Discount_on = "";
            string duesamt = "0";
            double payable = 0, paid = 0, dues = 0, disc = 0, payble_after_disc = 0;
            SqlDataAdapter ad_contactus = new SqlDataAdapter("select * from admission_registor where admissionserialnumber='" + drx["Admission_no"].ToString() + "' and Session_Id='" + drx["Session_Id"].ToString() + "' and StudentStatus='AV'  and  Is_TC_Taken!='true' and Status='1' order by id asc", My.conn);
            DataSet ds = new DataSet();
            ad_contactus.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
               
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dt.Rows[0]["hosteltaken"].ToString() == "")
                    {
                        ViewState["hostaltakenDues"] = "No";
                        ViewState["hostel_id"] = "0";
                    }
                    else if (dt.Rows[0]["hosteltaken"].ToString().ToLower() == "no")
                    {
                        ViewState["hostaltakenDues"] = "No";
                        ViewState["hostel_id"] = "0";
                    }
                    else
                    {
                        ViewState["hostaltakenDues"] = dt.Rows[0]["hosteltaken"].ToString();
                        if (ViewState["hostaltakenDues"].ToString().ToUpper() == "YES")
                        {
                            string hostelId = My.get_single_column_data("select Hostel_id as Column_Name from Hostel_assign_master where Session_id='" + dr["Session_id"].ToString() + "' and Admission_no='" + drx["Admission_no"].ToString() + "' and Class_id='" + dr["Class_id"].ToString() + "' and Status=1");
                            ViewState["hostel_id"] = hostelId;
                        }
                        else
                        {
                            ViewState["hostel_id"] = "0";
                        }
                    }
                    ViewState["class_id"] = dr["Class_id"].ToString();

                    ViewState["Transfer_Status"] = dr["Transfer_Status"].ToString();
                    ViewState["studenttype"] = dr["Transfer_Status"].ToString();
                    if (dr["Transfer_Status"].ToString().ToUpper() == "TRANSFERRED")
                    {
                        ViewState["studenttype"] = dr["Transfer_Status_Old"].ToString();
                        ViewState["Transfer_Status"] = dr["Transfer_Status_Old"].ToString();
                    }

                    if (ViewState["studenttype"].ToString() == "New")
                    {
                        ViewState["parameter"] = dr["hosteltaken"].ToString().ToUpper() == "YES" ? "HostelAdmissionFee" : "AdmissionFee";
                        ViewState["parameter_id"] = dr["hosteltaken"].ToString().ToUpper() == "YES" ? "5" : "1";
                        Discount_on = "Admission";
                    }
                    else
                    {
                        ViewState["parameter"] = dr["hosteltaken"].ToString().ToUpper() == "YES" ? "HostelAnnualFee" : "AnnualFee";
                        ViewState["parameter_id"] = dr["hosteltaken"].ToString().ToUpper() == "YES" ? "6" : "2";
                        Discount_on = "Annual";
                    }

                    ViewState["day_bording"] = My.toBool(dr["is_applied_dayboarding"]);
                    ViewState["day_bording_with_lunch"] = My.toBool(dr["day_boarding_with_lunch"]);
                    ViewState["group_id"] = "3";
                    ViewState["category_id"] = dr["category_id"].ToString();
                    ViewState["sub_category_id"] = dr["SubCategory_id"].ToString();
                    ViewState["classid"] = dr["Class_id"].ToString();
                    ViewState["Section"] = dr["Section"].ToString();
                    ViewState["sessionIDs"] = dr["Session_id"].ToString();
                    ViewState["session"] = dr["session"].ToString();
                }

                if (ViewState["hostel_id"].ToString() != "0")
                {
                    qry = "select *,(payable-cast(disc_amount as float)-cast(paid as float)) net_payable from(select '0' payable_after_disc,session,feetype,cast(payable as float) payable,paid,dues,status,content_id, Disc as disc_amount from dbo.[Typewise_fee_collection] WHERE admission_no='" + drx["Admission_no"].ToString() + "' and parameter='" + ViewState["parameter"].ToString() + "' and session='" + ViewState["session"].ToString() + "')t";
                }
                else
                {
                    qry = "select *,(payable-cast(disc_amount as float)-cast(paid as float)) net_payable from(select '0' payable_after_disc,session,feetype,cast(payable as float) payable,paid,dues,status,content_id, Disc as disc_amount from dbo.[Typewise_fee_collection] WHERE admission_no='" + drx["Admission_no"].ToString() + "' and parameter='" + ViewState["parameter"].ToString() + "' and session='" + ViewState["session"].ToString() + "')t";
                }
            }



            DataTable fee_dt = My.dataTable(qry);
            if (fee_dt.Rows.Count == 0)
            {
                if (ViewState["hostel_id"].ToString() == "0")
                {
                    qry = "select '0' as payable_after_disc, session as Session,Perticular as feetype,Amount as payable,  '0' as paid, Amount as dues,'Dues' as status,'ANN01' as content_id,'0' as disc_amount,Amount as net_payable from Misc_Fee_Master_Studentwise where (Type_Mode='AdmissionFee' or Type_Mode='AnnualFee') and session='" + ViewState["session"].ToString() + "' and Admission_No='" + drx["Admission_no"].ToString() + "' UNION ALL select *,(payable-cast(disc_amount as float)) net_payable from(select '0' payable_after_disc,fmc.session,cm.content feetype,cast(fmc.amount as float) payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + drx["Admission_no"].ToString() + "'  and parameter_id='" + ViewState["parameter_id"].ToString() + "' and session='" + ViewState["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='" + Discount_on + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and parameter_id='" + ViewState["parameter_id"].ToString() + "' and session='" + ViewState["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='" + Discount_on + "' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'))),0) disc_amount   from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='" + ViewState["parameter"].ToString() + "'  and session='" + ViewState["session"].ToString() + "' and class_id='" + ViewState["classid"].ToString() + "')t";
                }
                else
                {
                    qry = "select '0' as payable_after_disc, session as Session,Perticular as feetype,Amount as payable,  '0' as paid, Amount as dues,'Dues' as status,'ANN01' as content_id,'0' as disc_amount,Amount as net_payable from Misc_Fee_Master_Studentwise where (Type_Mode='AdmissionFee' or Type_Mode='AnnualFee') and session='" + ViewState["session"].ToString() + "' and Admission_No='" + drx["Admission_no"].ToString() + "' UNION ALL select *,(payable-cast(disc_amount as float)) net_payable from(select '0' payable_after_disc,fmc.session,cm.content feetype,cast(fmc.amount as float) payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where Hostel_id=" + ViewState["hostel_id"].ToString() + " and admission_no='" + drx["Admission_no"].ToString() + "'  and parameter_id='" + ViewState["parameter_id"].ToString() + "' and session='" + ViewState["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='" + Discount_on + "'),(select top 1 disc_amount from dbo.[Discount_Master] where  Hostel_id=" + ViewState["hostel_id"].ToString() + " and Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and parameter_id='" + ViewState["parameter_id"].ToString() + "' and session='" + ViewState["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='" + Discount_on + "' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'))),0) disc_amount   from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='" + ViewState["parameter"].ToString() + "'  and session='" + ViewState["session"].ToString() + "' and class_id='" + ViewState["classid"].ToString() + "' and   fmc.Hostel_Id=" + ViewState["hostel_id"].ToString() + ")t";
                }
                fee_dt = My.dataTable(qry);
            }


            DataTable dtadmdues = mycode.FillData(qry);
            if (dtadmdues.Rows.Count == 0)
            {
            }
            else
            {
                
                foreach (DataRow dr in dtadmdues.Rows)
                {
                    dr["payable_after_disc"] = My.toDouble(dr["payable"]) - My.toDouble(dr["disc_amount"]) - My.toDouble(dr["paid"]);
                    payable += My.toDouble(dr["payable"]);
                    paid += My.toDouble(dr["paid"]);
                    dues += My.toDouble(dr["dues"]);
                    disc += My.toDouble(dr["disc_amount"]);
                    payble_after_disc += My.toDouble(dr["payable_after_disc"]);
                }
                duesamt = payble_after_disc.ToString("0.00");
            }

            lbl_payple.Text = payable.ToString("0.00");
            lbl_discount_amtbe.Text = disc.ToString("0.00");
            lbl_amt_after_disc.Text = (payable- disc).ToString("0.00");

            lbl_paid_amt.Text = paid.ToString("0.00");
            lbl_dues_amt.Text= payble_after_disc.ToString("0.00");
            
        }

        private void fetch_ledger()
        {
            DataTable dt = mycode.FillData("select * from Student_Payment_History  where Session='" + ViewState["Session"].ToString() + "' and Addmission_no='" + ViewState["Admission_no"] + "' and Class_id='" + ViewState["class_id"].ToString() + "' ORDER BY Idate ASC");
            if (dt.Rows.Count == 0)
            {
                rl_ledger.DataSource = null;
                rl_ledger.DataBind();
                ledgeRDV.Visible = false;
            }
            else
            {
                rl_ledger.DataSource = dt;
                rl_ledger.DataBind();
                ledgeRDV.Visible = true;
            }
        }


        private void fetch_admission_fee()
        {
            DataTable dt = mycode.FillData("select  CASE WHEN parameter = 'AdmissionFee' THEN 'Admission Fee' WHEN parameter  = 'AnnualFee' THEN 'Annual Fee' END AS Fee_type,  parameter,isnull(sum(convert(float, payable)),0) as Total_payable,isnull(sum(convert(float, Disc)),0) as Total_disc,isnull(sum(convert(float, Payable_after_disc)),0) as Total_Payable_after_disc,isnull(sum(convert(float, paid)),0) as Total_paid,isnull(sum(convert(float, dues)),0) as Total_dues from Typewise_fee_collection where (parameter='AdmissionFee' or parameter='AnnualFee' or parameter='HostelAdmissionFee'or parameter='HostelAnnualFee') and session='" + ViewState["Session"].ToString() + "' and admission_no='" + ViewState["Admission_no"].ToString() + "' group by parameter");
            if (dt.Rows.Count == 0)
            {
                rp_adm_fees.DataSource = null;
                rp_adm_fees.DataBind();
            }
            else
            {
                rp_adm_fees.DataSource = dt;
                rp_adm_fees.DataBind();
                if (dt.Rows[0]["Total_payable"].ToString() == "0")
                {
                    adm_anul_tbls.Visible = false;
                    adm_anul_no_msgs.Visible = true;
                }
                else
                {
                    adm_anul_tbls.Visible = true;
                    adm_anul_no_msgs.Visible = false;
                }
            }
        }

        private void find_monthly_dues_fees()
        {
            DataTable fdt = new DataTable();

            DataTable dtm = mycode.FillData("select * from Month_Index order by Position asc");
            foreach (DataRow drm in dtm.Rows)
            {
                DataTable feedt = new DataTable();
               
               

                ViewState["parameteridS"] = "4";
                if (drm["Month_id"].ToString() != "")
                {
                    ViewState["LunchMnthName"] = drm["Month"].ToString();
                    ViewState["LunchMnthId"] = drm["Month_id"].ToString();
                }



                //====================
                if (ViewState["IsBoarding"].ToString() == "1")
                {
                    int mnthids = My.toint(drm["Month_id"].ToString());
                    if (My.toint(ViewState["LunchMnthId"].ToString()) <= mnthids)
                    {
                        ViewState["parameteridS"] = "44";
                    }
                    else
                    {
                        ViewState["parameteridS"] = "4";
                    }
                }
                string session = ViewState["Session"].ToString();
                string type = "";
                if (My.dataTable("select  * from dbo.[Typewise_fee_collection]   where admission_no='" + ViewState["Admission_no"].ToString() + "' and session='" + ViewState["Session"].ToString() + "' and month='" + drm["Month"].ToString() + "' and parameter='" + ViewState["parameter"].ToString() + "'").Rows.Count > 0)
                {
                    feedt = My.dataTable("select Month,isnull(sum(convert(float, amount)),0) as Amount,isnull(sum(convert(float, disc_amount)),0) as Disc_amount,(sum(convert(float, amount))-sum(convert(float, isnull((disc_amount),'0')))-sum(convert(float, isnull((previously_paid),'0')))) as Total_Dues from (select month,payable amount,Disc as disc_amount,isnull(paid,'0') previously_paid from dbo.[Typewise_fee_collection]   where admission_no='" + ViewState["Admission_no"].ToString() + "' and session='" + session + "' and month='" + drm["Month"].ToString() + "' and parameter='MonthlyFee' and content_id!='6121')t group by Month");
                }
                else
                {
                    Dictionary<string, object> dc = new Dictionary<string, object>();
                    dc["admission_no"] = ViewState["Admission_no"].ToString();
                    dc["session_id"] = ViewState["sessionIDs"].ToString();
                    dc["class"] = lblclass.Text;
                    dc["session"] = session;
                    dc["class_id"] = ViewState["classid"].ToString();

                    dc["hosteltaken"] = lblhostel.Text.ToLower();

                    dc["months"] = drm["Month"].ToString();
                    dc["tr_ledger"] = My.is_combine ? "School" : "Transport";
                    dc["hostel_id"] = ViewState["Hostel_id"].ToString();
                    dc["Room_Category_id"] = ViewState["Room_Category_id"].ToString();
                    dc["Hostel_assig_id"] = ViewState["Hostel_assign_id"].ToString();
                    dc["day_boarding"] = My.toBool(ViewState["day_bording"].ToString());
                    dc["day_boarding_lunch"] = My.toBool(ViewState["day_bording_with_lunch"].ToString());
                    dc["category_id"] = ViewState["category_id"].ToString();
                    dc["sub_category_id"] = ViewState["sub_category_id"].ToString();
                    dc["TransportationPath_id"] = ViewState["TransportPath_id"].ToString();
                    dc["transportportation_id"] = ViewState["Transport_id"].ToString();
                    dc["Boarding_Point_id"] = ViewState["Boarding_Point_id"].ToString();
                    dc["parameter_id"] = ViewState["parameteridS"].ToString();

                    string cunrt_session = session;
                    string session_frst_year = cunrt_session.Substring(0, 4);
                    int session_s_year = My.toint(session_frst_year);
                    int s_year = My.toint(session_frst_year);
                    string monthid = My.tomonth_numberstring(drm["Month"].ToString());
                    int pay_month = My.toint(monthid);
                    s_year = My.check_start_months(pay_month, s_year);

                    dc["monthid"] = s_year + monthid;
                    dc["sp_status"] = "2";
                    feedt = My.dataTableSP("sp_Fetch_month_dues", dc);
                }

                foreach (DataRow dr in feedt.Rows)
                {
                    if (My.toDouble(dr["Total_Dues"].ToString()) > 0)
                    {
                        try
                        {
                            fdt.Rows.Add(dr.ItemArray);
                        }
                        catch
                        {
                            foreach (DataColumn dc in feedt.Columns)
                            {
                                fdt.Columns.Add(dc.ColumnName);
                            }
                            fdt.Rows.Add(dr.ItemArray);
                        }
                    }
                }
            }

            if (fdt.Rows.Count.ToString() != "0")
            {
                rp_dues_fees.DataSource = fdt.DefaultView;
                rp_dues_fees.DataBind();
            }
            else
            {
                rp_dues_fees.DataSource = null;
                rp_dues_fees.DataBind();
            }
        }

        private void fetch_monthly_paid_fees()
        {
            bind_grd_view("select  t2.Position,t1.month,isnull(sum(convert(float, payable)),0) as Total_payable,isnull(sum(convert(float, Disc)),0) as Total_disc,isnull(sum(convert(float, Payable_after_disc)),0) as Total_Payable_after_disc,isnull(sum(convert(float, paid)),0) as Total_paid,isnull(sum(convert(float, dues)),0) as Total_dues,t1.Date from Typewise_fee_collection t1 join Month_Index t2 on t1.month=t2.Month where (t1.parameter='MonthlyFee' or t1.parameter='HostelMonthlyFee') and session='" + ViewState["Session"].ToString() + "' and admission_no='" + ViewState["Admission_no"].ToString() + "' group by t1.month,t2.Position,t1.Date order by t2.Position asc");
        }

        private void bind_grd_view(string qry)
        {
            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
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
                if (ViewState["Isadmissionfeetekent"].ToString() == "1")// admission fee not mindtree
                {
                    query = "select * from admission_registor where class='" + ddlclass.SelectedItem.Text + "' and Session='" + ddlsession.SelectedItem.Text + "' and section='" + txt_section.Text + "' and rollnumber='" + txtrollnumber.Text + "' and StudentStatus='AV'    and Is_TC_Taken!='true' and   Status='1' order by id asc";
                }
                else
                {

                    query = "select * from admission_registor where class='" + ddlclass.SelectedItem.Text + "' and Session='" + ddlsession.SelectedItem.Text + "' and section='" + txt_section.Text + "' and rollnumber='" + txtrollnumber.Text + "' and StudentStatus='AV'  and payment_status!='Unpaid' and Is_TC_Taken!='true' and   Status='1' order by id asc";
                }


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
                if (ViewState["Isadmissionfeetekent"].ToString() == "1")// admission fee not mindtree
                {
                    query = "select * from admission_registor where studentname like '%" + txt_student_name.Text + "%' and session='" + ddl_session_student.SelectedItem.Text + "' and StudentStatus='AV'   and Is_TC_Taken!='true' and  Status='1' order by id asc";
                }
                else
                {
                    query = "select * from admission_registor where studentname like '%" + txt_student_name.Text + "%' and session='" + ddl_session_student.SelectedItem.Text + "' and StudentStatus='AV'  and payment_status!='Unpaid' and Is_TC_Taken!='true' and  Status='1' order by id asc";
                }


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
                if (ViewState["Isadmissionfeetekent"].ToString() == "1")// admission fee not mindtree
                {
                    query = "select * from admission_registor where admissionserialnumber='" + lbladmissionserialnumber.Text + "' and Session='" + lbl_session.Text + "' and StudentStatus='AV'   and Is_TC_Taken!='true' order by id asc";
                }
                else
                {
                    query = "select * from admission_registor where admissionserialnumber='" + lbladmissionserialnumber.Text + "' and Session='" + lbl_session.Text + "' and StudentStatus='AV'  and payment_status!='Unpaid' and Is_TC_Taken!='true' order by id asc";
                }
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

        double totle_dues = 0;
        protected void rp_dues_fees_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbl_dues_amount = ((Label)e.Item.FindControl("lbl_dues_amount")) as Label;
                totle_dues = totle_dues + My.toDouble(lbl_dues_amount.Text);
            }
            lbl_total_dues.Text = totle_dues.ToString("0.00");
        }

        double ttl_payable = 0; double ttl_disc = 0; double ttl_aftr_disc = 0; double ttl_paid_amt = 0; double ttl_dues_amts = 0;
        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbl_payple = ((Label)e.Item.FindControl("lbl_payple")) as Label;
                Label lbl_discount_amtbe = ((Label)e.Item.FindControl("lbl_discount_amtbe")) as Label;
                Label lbl_amt_after_disc = ((Label)e.Item.FindControl("lbl_amt_after_disc")) as Label;
                Label lbl_paid_amt = ((Label)e.Item.FindControl("lbl_paid_amt")) as Label;
                Label lbl_dues_amt = ((Label)e.Item.FindControl("lbl_dues_amt")) as Label;


                ttl_payable = ttl_payable + My.toDouble(lbl_payple.Text);
                ttl_disc = ttl_disc + My.toDouble(lbl_discount_amtbe.Text);
                ttl_aftr_disc = ttl_aftr_disc + My.toDouble(lbl_amt_after_disc.Text);
                ttl_paid_amt = ttl_paid_amt + My.toDouble(lbl_paid_amt.Text);
                ttl_dues_amts = ttl_dues_amts + My.toDouble(lbl_dues_amt.Text);
            }
            lbl_ttl_payble.Text = ttl_payable.ToString("0.00");
            lbl_ttl_disc.Text = ttl_disc.ToString("0.00");
            lbl_ttl_after_disc.Text = ttl_aftr_disc.ToString("0.00");
            lbl_ttl_paid_amt.Text = ttl_paid_amt.ToString("0.00");
            lbl_ttl_dues.Text = ttl_dues_amts.ToString("0.00");
        }


        double ttl_payable_a = 0; double ttl_disc_a = 0; double ttl_aftr_disc_a = 0; double ttl_paid_amt_a = 0; double ttl_dues_amts_a = 0;
        protected void rp_adm_fees_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbl_payple = ((Label)e.Item.FindControl("lbl_payple")) as Label;
                Label lbl_discount_amtbe = ((Label)e.Item.FindControl("lbl_discount_amtbe")) as Label;
                Label lbl_amt_after_disc = ((Label)e.Item.FindControl("lbl_amt_after_disc")) as Label;
                Label lbl_paid_amt = ((Label)e.Item.FindControl("lbl_paid_amt")) as Label;
                Label lbl_dues_amt = ((Label)e.Item.FindControl("lbl_dues_amt")) as Label;


                ttl_payable_a = ttl_payable_a + My.toDouble(lbl_payple.Text);
                ttl_disc_a = ttl_disc_a + My.toDouble(lbl_discount_amtbe.Text);
                ttl_aftr_disc_a = ttl_aftr_disc_a + My.toDouble(lbl_amt_after_disc.Text);
                ttl_paid_amt_a = ttl_paid_amt_a + My.toDouble(lbl_paid_amt.Text);
                ttl_dues_amts_a = ttl_dues_amts_a + My.toDouble(lbl_dues_amt.Text);


            }
            lbl_ttl_payble_a.Text = ttl_payable_a.ToString("0.00");
            lbl_ttl_disc_a.Text = ttl_disc_a.ToString("0.00");
            lbl_ttl_after_disc_a.Text = ttl_aftr_disc_a.ToString("0.00");
            lbl_ttl_paid_amt_a.Text = ttl_paid_amt_a.ToString("0.00");
            lbl_ttl_dues_a.Text = ttl_dues_amts_a.ToString("0.00");
        }


        double ttl_ledger_amt = 0;
        protected void rl_ledger_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbl_amount = ((Label)e.Item.FindControl("lbl_amount")) as Label;
                ttl_ledger_amt = ttl_ledger_amt + My.toDouble(lbl_amount.Text);
            }
            lbl_ttl_ledger_amt.Text = ttl_ledger_amt.ToString("0.00");
        }
    }
}