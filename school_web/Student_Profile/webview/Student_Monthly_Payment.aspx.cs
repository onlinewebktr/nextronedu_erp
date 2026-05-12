using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Net;
using Newtonsoft.Json;
using System.Text;
using System.IO;
using System.Globalization;
using school_web.Student_Profile.webview.RazorPay;
using school_web.Student_Profile.webview.EazyPay;
using school_web.Student_Profile.webview.Get_Epay;
using school_web.Student_Profile.webview.icic;

namespace school_web.Student_Profile.webview
{
    public partial class Student_Monthly_Payment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["regid"] != null)
                {
                    ViewState["Is_quarterwise_payment"] = "0";
                    ViewState["regid"] = Request.QueryString["regid"].ToString();
                    My.exeSql("delete from Typewise_fee_collection where admission_no='" + ViewState["regid"].ToString() + "' and  transection='' and parameter in ('MonthlyFee','HostelMonthlyFee') ");
                    Session["regid"] = ViewState["regid"].ToString();
                    ViewState["sessionid"] = My.get_session_id();
                    Dictionary<string, object> dc1 = My.get_selected_studentinfo(Session["regid"].ToString(), ViewState["sessionid"].ToString(), "1");
                    string Class_Id = (String)dc1["Class_id"];
                    string section = (String)dc1["Section"];
                    find_firm_details();
                    ViewState["Getwey_Type"] = mycode.get_payment_Getwey_Type_monthly(Class_Id);
                    //check if student payment and payment has not reflected by razor
                    if (ViewState["Getwey_Type"].ToString() != "0")
                    {
                        bool status_paymnet = mycode.bind_not_paymnet_datat(ViewState["regid"].ToString(), ViewState["sessionid"].ToString());
                        if (status_paymnet == false)
                        {
                            a1.Visible = false;
                        }
                        else
                        {
                            a1.Visible = true;
                            automatic_payment();
                        }
                    }
                    else
                    {
                        a1.Visible = false;
                    }
                    a1.Visible = false;
                    //student_info(ViewState["regid"].ToString());
                    ViewState["Branchid"] = get_branch_id(Session["regid"].ToString());
                    ViewState["Isadmissionfeetekent"] = My.get_admission_condition();
                    ViewState["Admission_no"] = ViewState["regid"].ToString();
                    ViewState["late_fine_no_of_day_month"] = "0";
                    ViewState["fine_date_From"] = "0";
                    ViewState["fine_date_To"] = "0";
                    ViewState["FineType"] = "0";


                    ViewState["no_of_months"] = "1";
                    ViewState["more_months_check_status"] = "No";
                    ViewState["check_one_more_months"] = "0";
                    ViewState["checked_after_frst_mnth"] = "0";

                    Find_student_details();
                    ViewState["month"] = "";

                    ViewState["fineAmt"] = "0";
                    ViewState["checked_mnth"] = "0";
                    ViewState["flags1"] = "0";
                    ViewState["fine_inserted"] = "0";
                }
            }
        }


        private void find_firm_details()
        {
            DataTable dt = PayrollMy.dataTable("select * from Firm_Details");
            if (dt.Rows.Count > 0)
            {
                try
                {
                    if (dt.Rows[0]["Is_quarterwise_payment"].ToString() == "True")
                    {
                        ViewState["Is_quarterwise_payment"] = "1";
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }



        private string get_branch_id(string admission_no)
        {
            string branch_id = "1";
            DataTable dt = mycode.FillData("select top 1 Branch_id,Session_id from admission_registor where admissionserialnumber='" + admission_no + "' order by Id desc");
            if (dt.Rows.Count > 0)
            {
                branch_id = dt.Rows[0]["Branch_id"].ToString();
            }
            return branch_id;
        }


        protected void btn_reset_Click(object sender, EventArgs e)
        {
            Response.Redirect("Student_Monthly_Payment.aspx?regid=" + Session["regid"].ToString());
        }
        private void Find_student_details()
        {
            try
            {
                string query = "";
                //empty_form();
                if (ViewState["Isadmissionfeetekent"].ToString() == "1")// admission fee not mindtree
                {
                    query = "select top 1 * from admission_registor where admissionserialnumber='" + ViewState["regid"].ToString() + "'   and StudentStatus='AV' and  Status='1'  and Is_TC_Taken!='true' and Transfer_Status in ('New','NT') and Session_id='" + ViewState["sessionid"].ToString() + "' order by id desc";
                }
                else
                {
                    query = "select top 1 * from admission_registor where admissionserialnumber='" + ViewState["regid"].ToString() + "'   and StudentStatus='AV' and Status='1' and   Is_TC_Taken!='true' and Transfer_Status in ('New','NT') and Session_id='" + ViewState["sessionid"].ToString() + "' order by id desc";
                } 
                find_details(query);
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }


        string scrpt;
        private void Alert(string msg, string panel)
        {
            if (panel == "success")
            {
                string script = "Swal.fire({ title: 'Success', text: '" + msg + "', icon: 'success' });";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true); 
            }
            else
            {
                string script = "Swal.fire({ title: 'Error', text: '" + msg + "', icon: 'error' });";
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", script, true); 
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
                Alert("Somthing is wrong", "error");
                return;
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ViewState["sessionIDs"] = dr["Session_id"].ToString();
                    ViewState["Session"] = dr["session"].ToString();
                    ViewState["email_id"] = dr["email_id"].ToString();
                    ViewState["Session_id"] = dr["Session_id"].ToString();
                    ViewState["rollnumber"] = dr["rollnumber"].ToString();
                    ViewState["studentname"] = dr["studentname"].ToString();
                    ViewState["fathername"] = dr["fathername"].ToString();
                    ViewState["class"] = dr["class"].ToString();
                    ViewState["class_id"] = dr["Class_id"].ToString();
                    ViewState["hosteltaken"] = dr["hosteltaken"].ToString();

                    ViewState["parameter"] = dr["hosteltaken"].ToString().ToUpper() == "YES" ? "HostelMonthlyFee" : "MonthlyFee";

                    ViewState["parameter_id"] = dr["hosteltaken"].ToString().ToUpper() == "YES" ? "3" : "4";

                    Dictionary<string, object> dc1 = mycode.Bind_Transport_data_for_assined_student(ViewState["Session_id"].ToString(), ViewState["class_id"].ToString(), ViewState["regid"].ToString());
                    ViewState["Transport_id"] = (String)dc1["Transport_id"];
                    ViewState["TransportPath_id"] = (String)dc1["TransportPath_id"];
                    ViewState["Boarding_Point_id"] = (String)dc1["Boarding_Point_id"];
                    ViewState["Transport_Assigned_Id"] = (String)dc1["Transport_Assigned_Id"];
                    ViewState["Month_name"] = (String)dc1["Month_name"];
                    ViewState["Month_id"] = (String)dc1["Month_id"];
                    ViewState["Year_month"] = (String)dc1["Year_month"];
                    ViewState["Sheet_Id"] = (String)dc1["Sheet_Id"];



                    ViewState["transportationtaken"] = dr["transportationtaken"].ToString();
                    ViewState["mobilenumber"] = dr["father_mob"].ToString();
                    ViewState["hostel_id"] = My.toint(dr["Hostel_id"].ToString());
                    ViewState["day_bording"] = My.toBool(dr["is_applied_dayboarding"]);
                    ViewState["day_bording_with_lunch"] = My.toBool(dr["day_boarding_with_lunch"]);
                    ViewState["group_id"] = "3";
                    ViewState["category_id"] = dr["category_id"].ToString();
                    ViewState["sub_category_id"] = dr["SubCategory_id"].ToString();
                    ViewState["classid"] = dr["Class_id"].ToString();
                    ViewState["Section"] = dr["Section"].ToString();
                    ViewState["Transfer_Status"] = dr["Transfer_Status"].ToString();

                    Dictionary<string, object> dc2 = mycode.Bind_hostel_data_for_assined_student(ViewState["Session_id"].ToString(), ViewState["class_id"].ToString(), ViewState["regid"].ToString());
                    ViewState["Hostel_id"] = (String)dc2["Hostel_id"];
                    ViewState["Room_Category_id"] = (String)dc2["Room_Category_id"];
                    ViewState["From_month_name"] = (String)dc2["From_month_name"];
                    ViewState["From_month_id"] = (String)dc2["From_month_id"];
                    ViewState["Assined_Year_Month"] = (String)dc2["Assined_Year_Month"];
                    ViewState["Hostel_assign_id"] = (String)dc2["Hostel_assign_id"];

                    if (dr["Section"].ToString() == "")
                    {
                        ViewState["Section"] = "A";
                    }
                    else if (dr["Section"].ToString() == "&nbsp;")
                    {

                        ViewState["Section"] = "A";
                    }
                    else
                    {
                        ViewState["Section"] = dr["Section"].ToString();
                    }

                }


                ViewState["IsBoarding"] = "0";
                ViewState["parameteridS"] = "4";
                string queryS = "select * from Student_mapping_with_boarding_with_lunch where Session_id='" + ViewState["sessionIDs"].ToString() + "' and Admission_no='" + ViewState["regid"].ToString() + "' and Class_id='" + ViewState["class_id"].ToString() + "'";
                DataTable dts = My.dataTable(queryS);
                if (dts.Rows.Count != 0)
                {
                    ViewState["LunchMnthName"] = dts.Rows[0]["Month_name"].ToString();
                    ViewState["LunchMnthId"] = dts.Rows[0]["Month_id"].ToString();
                    ViewState["IsBoarding"] = "1";
                }


                find_all_due_fee();




                try
                {
                    ///====== 
                    //ViewState["Transfer_Status"] 
                    string qry = "";
                    string parameter_id = "";
                    string parameter_id2 = "";
                    string parameter = "";
                    string parameter2 = "";
                    string Discount_on = "Admission";
                    if (ViewState["Transfer_Status"].ToString() == "New")
                    {
                        parameter_id = "1";// annulfee
                        parameter_id2 = "5";// admission fee for hostel

                        if (ViewState["Hostel_id"].ToString() == "0")
                        {
                            parameter = "AdmissionFee";
                        }
                        else
                        {
                            parameter = "HostelAdmissionFee";
                        }
                    }
                    else
                    {
                        Discount_on = "Annual";
                        parameter_id = "2";// annulfee
                        parameter_id2 = "6";// admission fee for hostel 
                        if (ViewState["Hostel_id"].ToString() == "0")
                        {
                            parameter = "AnnualFee";
                        }
                        else
                        {
                            parameter = "HostelAnnualFee";
                        }
                    }




                    qry = "select isnull(sum(convert(float, net_payable)),0) as Dues_amt from(select(payable - cast(disc_amount as float) - cast(paid as float)) net_payable from(select '0' payable_after_disc, session, feetype, cast(payable as float) payable, paid, dues, status, content_id, Disc as disc_amount from Typewise_fee_collection WHERE admission_no = '" + ViewState["regid"].ToString() + "' and(parameter = '" + parameter + "')  and session = '" + ViewState["Session"].ToString() + "' and class_id =" + ViewState["class_id"].ToString() + ") t) y";
                    //qry = "select *,(payable-cast(disc_amount as float)-cast(paid as float)) net_payable from(select '0' payable_after_disc,session,feetype,cast(payable as float) payable,paid,dues,status,content_id, Disc as disc_amount from Typewise_fee_collection WHERE admission_no='" + ViewState["regid"].ToString() + "' and (parameter='" + parameter + "')  and session='" + ViewState["Session"].ToString() + "' and class_id=" + ViewState["class_id"].ToString() + ") t";

                    DataTable fee_dt = My.dataTable(qry);
                    if (My.toDouble(fee_dt.Rows[0]["Dues_amt"].ToString()) == 0)
                    {
                        string qrys = "select(payable - cast(disc_amount as float) - cast(paid as float)) net_payable from(select '0' payable_after_disc, session, feetype, cast(payable as float) payable, paid, dues, status, content_id, Disc as disc_amount from Typewise_fee_collection WHERE admission_no = '" + ViewState["regid"].ToString() + "' and(parameter = '" + parameter + "')  and session = '" + ViewState["Session"].ToString() + "' and class_id =" + ViewState["class_id"].ToString() + ") t";
                        DataTable fee_dts = My.dataTable(qrys);
                        if (fee_dts.Rows.Count == 0)
                        {
                            if (ViewState["Hostel_id"].ToString() == "0")
                            {
                                qry = "select isnull(sum(convert(float, net_payable)),0) as Dues_amt from(select Amount as net_payable from Misc_Fee_Master_Studentwise where (Type_Mode = 'AdmissionFee' or Type_Mode = 'AnnualFee') and session = '" + ViewState["Session"].ToString() + "' and Admission_No='" + ViewState["regid"].ToString() + "' UNION ALL select(payable - cast(disc_amount as float)) net_payable from(select '0' payable_after_disc,fmc.session,cm.content feetype, cast(fmc.amount as float) payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + ViewState["regid"].ToString() + "'  and (parameter_id='" + parameter_id + "' or parameter_id='" + parameter_id2 + "') and session = '" + ViewState["Session"].ToString() + "' and fee_head_id = cm.content_id and Discount_on = '" + Discount_on + "' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'), (select top 1 disc_amount from dbo.[Discount_Master] where Class_id = '" + ViewState["classid"].ToString() + "' and admission_no = 'All'  and(parameter_id='" + parameter_id + "' or parameter_id='" + parameter_id2 + "') and session = '" + ViewState["Session"].ToString() + "' and fee_head_id = cm.content_id and Discount_on = '" + Discount_on + "' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'))),0) disc_amount from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id = cm.content_id where (parameter = '" + parameter + "') and session = '" + ViewState["Session"].ToString() + "' and class_id = '" + ViewState["classid"].ToString() + "') t) y";
                                // qry = "select '0' as payable_after_disc, session as Session,Perticular as feetype,Amount as payable, '0' as paid, Amount as dues,'Dues' as status,'ADM01' as content_id,'0' as disc_amount,Amount as net_payable from Misc_Fee_Master_Studentwise where (Type_Mode='AdmissionFee' or Type_Mode='AnnualFee') and session='" + ViewState["Session"].ToString() + "' and Admission_No='" + ViewState["regid"].ToString() + "' UNION ALL select *,(payable-cast(disc_amount as float)) net_payable from(select '0' payable_after_disc,fmc.session,cm.content feetype,cast(fmc.amount as float) payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + ViewState["regid"].ToString() + "'  and (parameter_id='" + parameter_id + "' or parameter_id='" + parameter_id2 + "') and session='" + ViewState["Session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and (parameter_id='" + parameter_id + "' or parameter_id='" + parameter_id2 + "') and session='" + ViewState["Session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'))),0) disc_amount from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where (parameter='" + parameter + "' ) and session='" + ViewState["Session"].ToString() + "' and class_id='" + ViewState["classid"].ToString() + "') t";
                            }
                            else
                            {
                                qry = "select Amount as net_payable from Misc_Fee_Master_Studentwise where (Type_Mode = 'AdmissionFee' or Type_Mode = 'AnnualFee') and session = '" + ViewState["Session"].ToString() + "' and Admission_No = '" + ViewState["regid"].ToString() + "' UNION ALL select(payable - cast(disc_amount as float)) net_payable from (select '0' payable_after_disc,fmc.session,cm.content feetype, cast(fmc.amount as float) payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no = '" + ViewState["regid"].ToString() + "' and (parameter_id = '" + parameter_id + "' or parameter_id = '" + parameter_id2 + "') and session = '" + ViewState["Session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='" + Discount_on + "' and category_id = '" + ViewState["category_id"].ToString() + "' and sub_category_id = '" + ViewState["sub_category_id"].ToString() + "'), (select top 1 disc_amount from dbo.[Discount_Master] where Class_id = '" + ViewState["classid"].ToString() + "' and admission_no = 'All'  and(parameter_id = '" + parameter_id + "' or parameter_id = '" + parameter_id2 + "') and session = '" + ViewState["Session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='" + Discount_on + "' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'))),0) disc_amount from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id = cm.content_id where(parameter = '" + parameter + "')  and session = '" + ViewState["Session"].ToString() + "' and class_id = '" + ViewState["classid"].ToString() + "') t";
                                //qry = "select isnull(sum(convert(float, net_payable)),0) as Dues_amt from (select Amount as net_payable from Misc_Fee_Master_Studentwise where (Type_Mode='AdmissionFee' or Type_Mode='AnnualFee') and session='" + ViewState["Session"].ToString() + "' and Admission_No='" + ViewState["regid"].ToString() + "' UNION ALL select (payable-cast(disc_amount as float)) net_payable from(select '0' payable_after_disc,fmc.session,cm.content feetype,cast(fmc.amount as float) payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where Hostel_id=" + ViewState["Hostel_id"].ToString() + " and admission_no='" + ViewState["regid"].ToString() + "'  and (parameter_id='" + parameter_id + "' or parameter_id='" + parameter_id2 + "') and session='" + ViewState["Session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Hostel_id=" + ViewState["Hostel_id"].ToString() + " and Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and (parameter_id='" + parameter_id + "' or parameter_id='" + parameter_id2 + "') and session='" + ViewState["Session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'))),0) disc_amount from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where (parameter='" + parameter + "' ) and session='" + ViewState["Session"].ToString() + "' and class_id='" + ViewState["classid"].ToString() + "' and  fmc.Hostel_Id='" + ViewState["Hostel_id"].ToString() + "') t) y";
                            }
                            fee_dt = My.dataTable(qry);
                        }
                    }


                    if (My.toDouble(fee_dt.Rows[0]["Dues_amt"].ToString()) > 0)
                    {
                        payMentDV.Visible = false;
                        msgsDisplay.Visible = true;
                    }
                }
                catch (Exception ex) { }
            }
        }



        private void find_all_due_fee()
        {
            DataTable dtDatas;
            dtDatas = new DataTable();
            dtDatas.Columns.Add("Month");
            dtDatas.Columns.Add("value");
            dtDatas.Columns.Add("fee_amount");
            dtDatas.Columns.Add("discount_per");
            dtDatas.Columns.Add("paid_peev");
            dtDatas.Columns.Add("paid_status");
            dtDatas.Columns.Add("bac_colour");
            int temp;
            List<string> lst = new List<string>();
            string temp_month = My.get_start_month();
            //  lst.Add(temp_month);



            for (temp = 1; temp <= 12; temp++)
            {
                DataTable paid_dt = My.dataTable(" select month,status from dbo.[Typewise_fee_collection] where   session='" + ViewState["Session"].ToString() + "' and admission_no='" + ViewState["regid"].ToString() + "' and parameter like '%" + ViewState["parameter"].ToString() + "%' and month='" + temp_month + "'");
                if (paid_dt.Rows.Count > 0)
                {
                    string remove_month = "";
                    foreach (DataRow pdr in paid_dt.Rows)
                    {
                        if (pdr["status"].ToString() == "Dues")
                        {
                            lst.Add(temp_month);
                            break;
                        }
                    }
                }
                else
                {
                    if (ViewState["Is_quarterwise_payment"].ToString() == "1")
                    {
                        DataTable dtFee = My.dataTable("select isnull(sum(convert(float, amount)),0) as Total_fee from Fee_master_content_wise where parameter_id='" + ViewState["parameteridS"].ToString() + "' and session_id='" + ViewState["Session_id"].ToString() + "' and class_id='" + ViewState["class_id"].ToString() + "' and Month='" + temp_month + "'");
                        {
                            if (My.toDouble(dtFee.Rows[0]["Total_fee"].ToString()) == 0)
                            {
                            }
                            else
                            {
                                lst.Add(temp_month);
                            }
                        }
                    }
                    else
                    {
                        lst.Add(temp_month);
                    }
                }
                temp_month = find_month(temp_month);
            }

            int i = 0;
            foreach (string str in lst)
            {
                DataRow drNewRow = dtDatas.NewRow();
                drNewRow["Month"] = lst[i];
                drNewRow["value"] = false;
                //   drNewRow["discount_per"] = find_discount(lst[i].ToString(), txt_admission_no.Text, ddlsession.Text, class_id);
                drNewRow["paid_status"] = "NotCreated";
                dtDatas.Rows.Add(drNewRow);
                dtDatas.AcceptChanges();
                i++;
            }
            find_prev_dues(dtDatas);
            RPMonth.DataSource = dtDatas.DefaultView;
            RPMonth.DataBind();

            if (dtDatas.Rows.Count == 0)
            {
                Alert("Sorry, you do not have any dues amount.", "error"); 
                string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            } 
        }



        private string find_month(string month)
        {
            string next = "nn";
            switch (month)
            {
                case "January":
                    next = "February";
                    return next;

                case "February":
                    next = "March";
                    return next;

                case "March":
                    next = "April";
                    return next;

                case "April":
                    next = "May";
                    return next;

                case "May":
                    next = "June";
                    return next;

                case "June":
                    next = "July";
                    return next;

                case "July":
                    next = "August";
                    return next;

                case "August":
                    next = "September";
                    return next;

                case "September":
                    next = "October";
                    return next;

                case "October":
                    next = "November";
                    return next;

                case "November":
                    next = "December";
                    return next;

                case "December":
                    next = "January";
                    return next;

            }
            return next;
        }
        DataTable prevdues_dt = new DataTable();
        private void find_prev_dues(DataTable dtDatas)
        {
            prevdues_dt = My.dataTable(" select sum(isnull(cast(dues as float),'0')) dues,month from dbo.[Typewise_fee_collection] where session='" + ViewState["Session"].ToString() + "' and status='Dues' and Class='" + ViewState["class"].ToString() + "' and admission_no='" + ViewState["regid"].ToString() + "' and parameter='" + ViewState["parameter"].ToString() + "' group by month");
            foreach (DataRow mr in dtDatas.Rows)
            {
                var row = prevdues_dt.Select("month='" + mr["Month"].ToString() + "'");
                if (row.Length > 0)
                {
                    mr["paid_status"] = "Created";
                    mr["bac_colour"] = "Yellow";
                }
            }
            show_dues(dtDatas);
        }
        double anula_dues = 0; double prev_session_dues = 0;
        double admission_dues = 0; string adm_transection = "";
        private void show_dues(DataTable dtDatas)
        {
            double month_dues = 0;
            foreach (DataRow mr in dtDatas.Rows)
            {
                if (My.toBool(mr["Value"]))
                {
                    var row = prevdues_dt.Select("month='" + mr["Month"].ToString() + "'");
                    if (row.Length > 0)
                    {
                        DataRow dr = row[0];
                        month_dues += My.toDouble(dr["dues"]);
                    }
                }
            }
            if (month_dues + admission_dues + anula_dues + prev_session_dues == 0)
            {
                //txt_previousduesmonth.Text = "0";
                //chk_prev_dues.Visibility = Visibility.Collapsed;
                //chk_prev_dues.Uid = "0";
                //txt_view.Visibility = Visibility.Collapsed;
            }
            else
            {
                //chk_prev_dues.IsChecked = true;
                //chk_prev_dues.Visibility = Visibility.Visible;
                //txt_previousduesmonth.Text = (month_dues + admission_dues + anula_dues + prev_session_dues).ToString();
                //chk_prev_dues.Content = "Previous dues (Rs. " + txt_previousduesmonth.Text + ")";
                //chk_prev_dues.Uid = txt_previousduesmonth.Text;
                //txt_view.Visibility = Visibility.Visible;
            }
        }

        protected void chk_month_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["check_one_more_months"].ToString() == "0")
                {
                    DataTable dtf = mycode.FillData("select Month_selection,Is_fine_repeat from Firm_Details");
                    if (My.toIntS(dtf.Rows[0]["Month_selection"].ToString()) > 1)
                    {
                        ViewState["more_months_check_status"] = "Yes";
                        ViewState["no_of_months"] = My.toIntS(dtf.Rows[0]["Month_selection"].ToString());
                    }
                    else
                    {
                        ViewState["no_of_months"] = "1";
                        ViewState["more_months_check_status"] = "No";
                    }
                    if (dtf.Rows[0]["Is_fine_repeat"].ToString() == "True")
                    {
                        ViewState["RepeatFine"] = "Yes";
                    }
                    else
                    {
                        ViewState["RepeatFine"] = "No";
                    }
                    ViewState["check_one_more_months"] = "1";
                    ViewState["Dues"] = "No";
                    ViewState["DuesCalculate"] = "No";
                }



                CheckBox lnkc = (CheckBox)sender;
                RepeaterItem rowc = (RepeaterItem)lnkc.NamingContainer;
                int rowindexc = rowc.ItemIndex;

                Label lbl_Monthc = (Label)rowc.FindControl("lbl_Month");
                CheckBox chkc = (CheckBox)rowc.FindControl("chk_month");
                if (chkc.Checked == true)
                {
                    ViewState["repeatMonthPos"] = My.month_position(lbl_Monthc.Text);
                    DataTable dtms = My.dataTable("select * from Custome_month_selection_setting where Month_name='" + lbl_Monthc.Text + "'");
                    if (dtms.Rows.Count > 0)
                    {
                        ViewState["no_of_months"] = My.toIntS(dtms.Rows[0]["No_of_month_selection"].ToString());
                        ViewState["more_months_check_status"] = "Yes";
                    }
                }



                if (ViewState["more_months_check_status"].ToString() == "Yes")
                {
                    CheckBox lnkFine = (CheckBox)sender;
                    RepeaterItem rowFine = (RepeaterItem)lnkc.NamingContainer;
                    int rowindexFine = rowFine.ItemIndex;
                    Label lbl_MonthFine = (Label)rowFine.FindControl("lbl_Month");



                    ViewState["isFineUpdated"] = "0";
                    bool enable_next = true;
                    int kn = 1;
                    int growcount = RPMonth.Items.Count;
                    for (int i = 0; i < growcount; i++)
                    {
                        CheckBox chk = (CheckBox)RPMonth.Items[i].FindControl("chk_month");
                        Label lbl_Month = (Label)RPMonth.Items[i].FindControl("lbl_Month");
                        ViewState["repeatMonthPoscc"] = My.month_position(lbl_Month.Text);
                        // int checked_mnths = My.toIntS(ViewState["no_of_months"].ToString()) * My.toIntS(ViewState["check_one_more_months"].ToString());


                        if (My.toint(ViewState["repeatMonthPoscc"].ToString()) >= My.toint(ViewState["repeatMonthPos"].ToString()))
                        {
                            int checked_mnths = My.toIntS(ViewState["no_of_months"].ToString());
                            if (My.dataTable("select  * from Typewise_fee_collection  where admission_no='" + ViewState["regid"].ToString() + "' and session='" + ViewState["Session"].ToString() + "' and month='" + lbl_Month.Text + "' and parameter='" + ViewState["parameter"].ToString() + "'").Rows.Count > 0)
                            {
                                ViewState["Dues"] = "Yes";
                            }

                            if (checked_mnths > kn)
                            {
                                chk.Checked = true;
                            }
                            if (kn == checked_mnths)
                            {
                                chk.Checked = true;
                                if (enable_next == true)
                                {
                                    try
                                    {
                                        CheckBox chk_month = (CheckBox)RPMonth.Items[i + 1].FindControl("chk_month");
                                        chk_month.Enabled = true;
                                    }
                                    catch { }
                                }
                                enable_next = false;
                            }

                            if (enable_next == true)
                            {
                                try
                                {
                                    CheckBox chk_month = (CheckBox)RPMonth.Items[i + 1].FindControl("chk_month");
                                    chk_month.Enabled = true;
                                }
                                catch { }
                            }
                            if (chk.Checked == true)
                            {
                                chk.Checked = true;
                                chk.Enabled = false;
                                string month = lbl_Month.Text;
                                bind_monthly_fee();

                                if (ViewState["Dues"].ToString() == "Yes")
                                {
                                    if (ViewState["DuesCalculate"].ToString() == "No")
                                    {
                                        if (lbl_Month.Text == lbl_MonthFine.Text)
                                        {
                                            ViewState["DuesCalculate"] = "Yes";
                                            ViewState["MnthName"] = lbl_Month.Text;
                                            ViewState["isFineUpdated"] = "1";
                                            fine_calculation(lbl_Month.Text, "1");
                                        }
                                    }
                                    else
                                    {
                                        if (lbl_Month.Text == lbl_MonthFine.Text)
                                        {
                                            ViewState["MnthName"] = lbl_Month.Text;
                                            ViewState["isFineUpdated"] = "1";
                                            fine_calculation(lbl_Month.Text, "1");
                                        }
                                    }
                                }
                                else
                                {
                                    ViewState["MnthName"] = lbl_Month.Text;
                                    ViewState["isFineUpdated"] = "1";
                                    fine_calculation(lbl_Month.Text, "1");
                                }
                            }
                            else
                            {
                                bind_monthly_fee();
                                if (lbl_Month.Text == lbl_MonthFine.Text)
                                {
                                    ViewState["MnthName"] = lbl_Month.Text;
                                    ViewState["isFineUpdated"] = "1";
                                    fine_calculation(lbl_Month.Text, "1");
                                }
                            }
                            kn++;
                        }
                    }

                    //ViewState["check_one_more_months"] = My.toIntS(ViewState["check_one_more_months"].ToString()) + 1; 
                }
                else
                {

                    CheckBox lnk = (CheckBox)sender;
                    RepeaterItem row = (RepeaterItem)lnkc.NamingContainer;
                    int rowindex = row.ItemIndex;

                    try
                    {
                        CheckBox chk_month = (CheckBox)RPMonth.Items[rowindex + 1].FindControl("chk_month");
                        chk_month.Enabled = true;
                    }
                    catch { }
                    Label lbl_Month = (Label)row.FindControl("lbl_Month");
                    CheckBox chk = (CheckBox)row.FindControl("chk_month");
                    if (chk.Checked == true)
                    {
                        string month1 = lbl_Month.Text;
                        if (ViewState["month"].ToString() == "")
                        {
                            ViewState["month"] = "'" + month1 + "'";
                        }
                        else
                        {
                            ViewState["month"] = ViewState["month"] + "," + "'" + month1 + "'";
                        }
                        chk.Checked = true;
                        chk.Enabled = false;
                        string month = lbl_Month.Text;
                        bind_monthly_fee();
                        ViewState["MnthName"] = lbl_Month.Text; //lbl_Month.Text;
                        fine_calculation(lbl_Month.Text, "1");
                    }
                    else
                    {
                        bind_monthly_fee();
                        ViewState["MnthName"] = lbl_Month.Text;
                        fine_calculation(lbl_Month.Text, "1");
                    }
                }
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }


        private void bind_monthly_fee()//parameter_id=4 month 
        {
            double fee_amount = 0, disc_amount = 0, paid_previously = 0;
            bool success = false;
            DataTable fdt = new DataTable();
            int growcountS = RPMonth.Items.Count;
            for (int iS = 0; iS < growcountS; iS++)
            {
                Label lbl_Month = (Label)RPMonth.Items[iS].FindControl("lbl_Month");
                CheckBox chk_month = (CheckBox)RPMonth.Items[iS].FindControl("chk_month");

                fee_amount = 0; disc_amount = 0; paid_previously = 0;
                DataTable feedt = new DataTable();
                if (My.toBool(chk_month.Checked))
                {
                    if (ViewState["IsBoarding"].ToString() == "1")
                    {
                        int mnthids = My.tomonth_number(lbl_Month.Text);
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

                    ViewState["parameter"] = ViewState["hosteltaken"].ToString().ToUpper() == "YES" ? "HostelMonthlyFee" : "MonthlyFee";

                    if (My.dataTable("select  * from dbo.[Typewise_fee_collection]   where admission_no='" + ViewState["regid"].ToString() + "' and session='" + ViewState["Session"].ToString() + "' and month='" + lbl_Month.Text + "' and parameter='" + ViewState["parameter"].ToString() + "' and transection!=''").Rows.Count > 0)
                    {
                        feedt = My.dataTable("select id,   '0' admission_no,'0' class, '0' session,'0'parameter,      feetype content,payable amount,'0'Column1,payable amount,'0'Column2,month months,'0'content_id,'0'Ledger,'0'Column3,'0'Column4,'0'Column5,Disc as disc_amount,isnull(paid,'0') previously_paid from dbo.[Typewise_fee_collection]   where admission_no='" + ViewState["regid"].ToString() + "' and session='" + ViewState["Session"].ToString() + "' and month='" + lbl_Month.Text + "' and parameter='" + ViewState["parameter"].ToString() + "' and content_id!='6121'  and transection!=''");

                        type = "Calculated";
                    }
                    else
                    {
                        //dr["bac_colour"] = "White";
                        Dictionary<string, object> dc = new Dictionary<string, object>();
                        dc["admission_no"] = ViewState["regid"].ToString();
                        dc["session_id"] = ViewState["Session_id"].ToString();
                        dc["class"] = ViewState["class"].ToString();
                        dc["session"] = ViewState["Session"].ToString();
                        dc["class_id"] = ViewState["classid"].ToString();
                        dc["hosteltaken"] = ViewState["hosteltaken"].ToString().ToLower();
                        dc["months"] = lbl_Month.Text;
                        dc["tr_ledger"] = My.is_combine ? "School" : "Transport";
                        dc["hostel_id"] = ViewState["Hostel_id"].ToString();
                        dc["Room_Category_id"] = ViewState["Room_Category_id"].ToString();
                        dc["Hostel_assig_id"] = ViewState["Hostel_assign_id"].ToString();

                        dc["day_boarding"] = ViewState["day_bording"].ToString();
                        dc["day_boarding_lunch"] = ViewState["day_bording_with_lunch"].ToString();
                        dc["category_id"] = ViewState["category_id"].ToString();
                        dc["sub_category_id"] = ViewState["sub_category_id"].ToString();

                        dc["parameter_id"] = ViewState["parameteridS"].ToString();
                        dc["TransportationPath_id"] = ViewState["TransportPath_id"].ToString();
                        dc["transportportation_id"] = ViewState["Transport_id"].ToString();
                        dc["Boarding_Point_id"] = ViewState["Boarding_Point_id"].ToString();

                        ViewState["session"] = ViewState["Session"].ToString();
                        //new08/08/2022
                        string cunrt_session = ViewState["session"].ToString();
                        string session_frst_year = cunrt_session.Substring(0, 4);
                        int session_s_year = My.toint(session_frst_year);
                        int s_year = My.toint(session_frst_year);
                        string monthid = My.tomonth_numberstring(lbl_Month.Text);
                        int pay_month = My.toint(monthid);
                        s_year = My.check_start_months(pay_month, s_year);

                        //string cunrt_session = ViewState["Session"].ToString();
                        //string session_frst_year = cunrt_session.Substring(0, 4);
                        //int session_s_year = My.toint(session_frst_year);
                        //int s_year = My.toint(session_frst_year);

                        //string monthid = My.tomonth_numberstring(lbl_Month.Text);

                        //int pay_month = My.toint(monthid);
                        //s_year = My.check_start_months(pay_month, s_year);

                        dc["monthid"] = s_year + monthid;

                        feedt = My.dataTableSP("sp_fetch_monthly_fee", dc);
                        feedt.Columns.Add("previously_paid");
                        type = "NotCalculated";
                    }
                    feedt.Columns.Add("total_payable");
                    string month = "";
                    double total = 0, fee = 0, disc = 0, paid_prev = 0;
                    foreach (DataRow dr in feedt.Rows)
                    {
                        //if (type == "Calculated")
                        //{
                        //    My.exeSql("update dbo.[Typewise_fee_collection] set Disc='" + dr["disc_amount"].ToString() + "' where Id='" + dr["id"].ToString() + "'");
                        //}
                        month = dr["months"].ToString();
                        dr["total_payable"] = My.toDouble(dr["amount"]) - My.toDouble(dr["disc_amount"]) - My.toDouble(dr["previously_paid"]);
                        fee += My.toDouble(dr["amount"]);
                        disc += My.toDouble(dr["disc_amount"]);
                        paid_prev += My.toDouble(dr["previously_paid"]);
                        total += My.toDouble(dr["total_payable"]);
                    }

                    foreach (DataRow dr in feedt.Rows)
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

                    //lbl_fee_month.Text = month;
                    //lbl_fee_amount.Text = fee.ToString();
                    //lbl_discount.Text = disc.ToString();
                    //lbl_paid_prev.Text = paid_prev.ToString();
                    //lbl_total.Text = total.ToString();
                }
                else
                {
                }
            }

            if (fdt.Rows.Count.ToString() != "0")
            {
                rp_fee_details.DataSource = fdt.DefaultView;
                rp_fee_details.DataBind();
                bind_ttl_fee();
                pnl_month_wise_fee_details.Visible = true;
                btn_save_payment.Visible = true;
                pay.Visible = true;

            }
            else
            {
                pay.Visible = false;
                btn_save_payment.Visible = false;
                pnl_month_wise_fee_details.Visible = false;
                lbl_fee_amount.Text = "0";
                lbl_discount.Text = "0";
                lbl_paid_prev.Text = "0";
                lbl_total.Text = "0";

                txttotal.Text = "0";
                txt_paid_prev.Text = "0";
                txt_discount.Text = "0";
                txttotalbill.Text = "0";
                paybleBills.InnerText = "₹ 0";
            }
        }

        private void bind_ttl_fee()
        {
            int i;
            double totalAmt = 0; double totaldisc = 0; double totalPrepAid = 0; double totalpblE = 0;
            int gridview_rowcount = rp_fee_details.Items.Count;
            for (i = 0; i < gridview_rowcount; i++)
            {
                Label lbl_amount = (Label)rp_fee_details.Items[i].FindControl("lbl_amount");
                Label lbl_disc_amt = (Label)rp_fee_details.Items[i].FindControl("lbl_disc_amt");
                Label lbl_pre_paid = (Label)rp_fee_details.Items[i].FindControl("lbl_pre_paid");
                Label lbl_tot_pble = (Label)rp_fee_details.Items[i].FindControl("lbl_tot_pble");
                if (lbl_amount.Text != "")
                {
                    totalAmt = totalAmt + Convert.ToDouble(lbl_amount.Text);
                }
                if (lbl_disc_amt.Text != "")
                {
                    totaldisc = totaldisc + Convert.ToDouble(lbl_disc_amt.Text);
                }
                if (lbl_pre_paid.Text != "")
                {
                    totalPrepAid = totalPrepAid + Convert.ToDouble(lbl_pre_paid.Text);
                }
                if (lbl_tot_pble.Text != "")
                {
                    totalpblE = totalpblE + Convert.ToDouble(lbl_tot_pble.Text);
                }
            }
            lbl_fee_amount.Text = totalAmt.ToString();
            lbl_discount.Text = totaldisc.ToString();
            lbl_paid_prev.Text = totalPrepAid.ToString();
            lbl_total.Text = totalpblE.ToString();

            txttotal.Text = totalAmt.ToString();
            txt_paid_prev.Text = totalPrepAid.ToString();
            txt_discount.Text = totaldisc.ToString();



            txtfineamount.Text = ViewState["fineAmt"].ToString();

            txttotalbill.Text = (totalpblE + My.toDouble(ViewState["fineAmt"].ToString())).ToString();
            paybleBills.InnerText = "₹ " + txttotalbill.Text;
            ViewState["total"] = totalAmt.ToString();
            ViewState["paid_prev"] = totalPrepAid.ToString();
            ViewState["discount"] = totaldisc.ToString();
            ViewState["totalbill"] = My.toDouble(txttotalbill.Text).ToString("0.00");
        }

        int loopCount = 1;
        protected void RPMonth_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (ViewState["firstrow"] == null)
                {
                    CheckBox chk_month = (CheckBox)e.Item.FindControl("chk_month");
                    chk_month.Enabled = true;
                    ViewState["firstrow"] = "1";
                }


                HtmlGenericControl mnthColor = (HtmlGenericControl)e.Item.FindControl("mnthColor");
                if (loopCount == 1)
                {
                    mnthColor.Attributes["class"] = "month-color blue"; 
                }
                if (loopCount == 2)
                {
                    mnthColor.Attributes["class"] = "month-color pink";
                }
                if (loopCount == 3)
                { 
                    mnthColor.Attributes["class"] = "month-color orange";
                }
                if (loopCount == 4)
                {
                    mnthColor.Attributes["class"] = "month-color green";
                }
                if (loopCount == 5)
                {
                    mnthColor.Attributes["class"] = "month-color purple";
                }
                if (loopCount == 6)
                {
                    loopCount = 0;
                    mnthColor.Attributes["class"] = "month-color red";
                }
                loopCount++;
            }
        }

        My mycode = new My();
        Payment_update_after_onlinepayment mypayment = new Payment_update_after_onlinepayment();
        protected void btn_save_payment_Click(object sender, EventArgs e)
        {
            ViewState["month"] = "";
            try
            {

                bool finalsbumnit = false;
                int chekmonthpaycount = My.get_paymnet_chekseeting();

                int rowcoun = RPMonth.Items.Count;

                int seclectioncount = 0;
                for (int iS = 0; iS < rowcoun; iS++)
                {
                    CheckBox chk_month = (CheckBox)RPMonth.Items[iS].FindControl("chk_month");
                    Label month1 = (Label)RPMonth.Items[iS].FindControl("lbl_Month");
                    if (chk_month.Checked == true)
                    {
                        seclectioncount = seclectioncount + 1;

                        if (ViewState["month"].ToString() == "")
                        {
                            ViewState["month"] = "'" + month1.Text + "'";
                        }
                        else
                        {
                            ViewState["month"] = ViewState["month"] + "," + "'" + month1.Text + "'";
                        }

                    }
                }

                if (seclectioncount >= chekmonthpaycount)
                {
                    if (chekmonthpaycount == 1)
                    {
                        finalsbumnit = true;
                    }
                    else
                    {
                        if (chekmonthpaycount == 2)
                        {
                            if (!mycode.IsDivisible(seclectioncount, 2))
                            {
                                finalsbumnit = false;
                                Alert("Your monthly selection is a multiple of " + chekmonthpaycount + ". So please select valid month.", "error");

                            }
                            else
                            {
                                finalsbumnit = true;
                            }
                        }
                        else if (chekmonthpaycount == 3)
                        {
                            if (!mycode.IsDivisible(seclectioncount, 3))
                            {
                                finalsbumnit = false;
                                Alert("Your monthly selection is a multiple of " + chekmonthpaycount + ". So please select valid month.", "error");
                            }
                            else
                            {
                                finalsbumnit = true;
                            }
                        }
                        else if (chekmonthpaycount == 4)
                        {
                            if (!mycode.IsDivisible(seclectioncount, 4))
                            {
                                finalsbumnit = false;
                                Alert("Your monthly selection is a multiple of " + chekmonthpaycount + ". So please select valid month.", "error");
                            }
                            else
                            {
                                finalsbumnit = true;
                            }
                        }

                    }


                    if (finalsbumnit == true)
                    {
                        bool isonlinepymnet = My.get_status_ispaymnet_on(ViewState["class_id"].ToString());
                        if (isonlinepymnet == true)
                        {
                            Random r = new Random(DateTime.Now.Millisecond);
                            string order_id = DateTime.UtcNow.ToString("yyMMddHHMMss") + r.Next(12346, 48749);

                            SqlCommand cmd;
                            string query = " INSERT INTO Payment_transaction_process (Name,Admission_no,Session,Class_id,Session_id,totalAmount,totalpaid_perivius,totaldiscount,totallatefine,Total_pay,Date,Time,Idate,ordertrackingid,month,status,parameter,parameter_id,Class_name,hosteltaken,day_boarding,day_boarding_lunch,category_id,sub_category_id,transportportation_id,hostel_id,group_id,Section,Emailid,Mobileno,Fine_amount,Total_no_of_fine_day_month,Fine_date_from,Fine_date_to,Fine_type,Lunch_Boarding_Parmeter,Pay_from) values (@Name,@Admission_no,@Session,@Class_id,@Session_id,@totalAmount,@totalpaid_perivius,@totaldiscount,@totallatefine,@Total_pay,@Date,@Time,@Idate,@ordertrackingid,@month,@status,@parameter,@parameter_id,@Class_name,@hosteltaken,@day_boarding,@day_boarding_lunch,@category_id,@sub_category_id,@transportportation_id,@hostel_id,@group_id,@Section,@Emailid,@Mobileno,@Fine_amount,@Total_no_of_fine_day_month,@Fine_date_from,@Fine_date_to,@Fine_type,@Lunch_Boarding_Parmeter,@Pay_from)";
                            cmd = new SqlCommand(query);
                            cmd.Parameters.AddWithValue("@Name", ViewState["studentname"].ToString());
                            cmd.Parameters.AddWithValue("@Admission_no", ViewState["regid"].ToString());
                            cmd.Parameters.AddWithValue("@Session", ViewState["Session"].ToString());
                            cmd.Parameters.AddWithValue("@Class_id", ViewState["class_id"].ToString());
                            cmd.Parameters.AddWithValue("@Session_id", ViewState["Session_id"].ToString());
                            cmd.Parameters.AddWithValue("@totalAmount", ViewState["total"].ToString());
                            cmd.Parameters.AddWithValue("@totalpaid_perivius", ViewState["paid_prev"].ToString());
                            cmd.Parameters.AddWithValue("@totaldiscount", ViewState["discount"].ToString());
                            cmd.Parameters.AddWithValue("@totallatefine", txtfineamount.Text);
                            cmd.Parameters.AddWithValue("@Total_pay", ViewState["totalbill"].ToString());
                            cmd.Parameters.AddWithValue("@Date", mycode.date());
                            cmd.Parameters.AddWithValue("@Time", mycode.time());
                            cmd.Parameters.AddWithValue("@Idate", mycode.idate());
                            cmd.Parameters.AddWithValue("@ordertrackingid", order_id);
                            cmd.Parameters.AddWithValue("@month", ViewState["month"].ToString());
                            cmd.Parameters.AddWithValue("@status", "Pending");

                            cmd.Parameters.AddWithValue("@parameter", ViewState["parameter"].ToString());
                            cmd.Parameters.AddWithValue("@parameter_id", ViewState["parameter_id"].ToString());
                            cmd.Parameters.AddWithValue("@Class_name", ViewState["class"].ToString());

                            cmd.Parameters.AddWithValue("@hosteltaken", ViewState["hosteltaken"].ToString());
                            cmd.Parameters.AddWithValue("@hostel_id", ViewState["Hostel_id"].ToString());

                            cmd.Parameters.AddWithValue("@day_boarding", ViewState["day_bording"].ToString());
                            cmd.Parameters.AddWithValue("@day_boarding_lunch", ViewState["day_bording_with_lunch"].ToString());
                            cmd.Parameters.AddWithValue("@category_id", ViewState["category_id"].ToString());
                            cmd.Parameters.AddWithValue("@sub_category_id", ViewState["sub_category_id"].ToString());
                            cmd.Parameters.AddWithValue("@transportportation_id", ViewState["TransportPath_id"].ToString());
                            cmd.Parameters.AddWithValue("@group_id", ViewState["group_id"].ToString());
                            cmd.Parameters.AddWithValue("@Section", ViewState["Section"].ToString());

                            cmd.Parameters.AddWithValue("@Emailid", ViewState["email_id"].ToString());
                            cmd.Parameters.AddWithValue("@Mobileno", ViewState["mobilenumber"].ToString());


                            cmd.Parameters.AddWithValue("@Fine_amount", txtfineamount.Text);
                            cmd.Parameters.AddWithValue("@Total_no_of_fine_day_month", ViewState["late_fine_no_of_day_month"].ToString());
                            cmd.Parameters.AddWithValue("@Fine_date_from", ViewState["fine_date_From"].ToString());
                            cmd.Parameters.AddWithValue("@Fine_date_to", ViewState["fine_date_To"].ToString());
                            cmd.Parameters.AddWithValue("@Fine_type", ViewState["FineType"].ToString());
                            cmd.Parameters.AddWithValue("@Lunch_Boarding_Parmeter", ViewState["parameteridS"].ToString());
                            cmd.Parameters.AddWithValue("@Pay_from", 1);



                            if (My.InsertUpdateData(cmd))
                            {
                                //------------------------------Rozor pay---------------------------- 
                                Response.Redirect("payfinal.aspx?regid=" + ViewState["regid"].ToString() + "&orderid=" + order_id + "&payFrom=1", false);
                            }
                        }
                        else
                        {
                            Alert("Sorry! Online payment is not enabled", "error");
                        }
                    }
                    else
                    {

                    }

                }
                else
                {
                    Alert("Sorry! Please select minimum " + chekmonthpaycount + " month", "error");
                }
            }
            catch
            {

            }
        }

        protected void rp_fee_details_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string lbl_mnth = ((Label)e.Item.FindControl("lbl_mnth")).Text;
                HtmlTableRow row2 = (HtmlTableRow)e.Item.FindControl("row");

                if (lbl_mnth == "April")
                {
                    row2.Attributes["style"] = "background-color:#CCE1F2";
                }
                if (lbl_mnth == "May")
                {
                    row2.Attributes["style"] = "background-color:#C6F8E5";
                }
                if (lbl_mnth == "June")
                {
                    row2.Attributes["style"] = "background-color:#FBF7D5";
                }
                if (lbl_mnth == "July")
                {
                    row2.Attributes["style"] = "background-color:#F9DED7";
                }
                //===
                if (lbl_mnth == "August")
                {
                    row2.Attributes["style"] = "background-color:#F5CDDE";
                }
                if (lbl_mnth == "September")
                {
                    row2.Attributes["style"] = "background-color:#E2BEF1";
                }
                if (lbl_mnth == "October")
                {
                    row2.Attributes["style"] = "background-color:#FBC5B0";
                }
                if (lbl_mnth == "November")
                {
                    row2.Attributes["style"] = "background-color:#BBD5D3";
                }
                //===
                if (lbl_mnth == "December")
                {
                    row2.Attributes["style"] = "background-color:#FCD0BA";
                }
                if (lbl_mnth == "January")
                {
                    row2.Attributes["style"] = "background-color:#E8CBD9";
                }
                if (lbl_mnth == "February")
                {
                    row2.Attributes["style"] = "background-color:#C9EAE8";
                }
                if (lbl_mnth == "March")
                {
                    row2.Attributes["style"] = "background-color:#99D7D2";
                }
            }
        }




        // ---------------------------FINE Calculation-----------------------------------------
        private void fine_calculation(string monthName, string from)
        {
            int mnth_idss = My.tomonth_number(monthName);
            string month_id_in_two_dgts = My.getMonthS_twoDigit(mnth_idss.ToString());
            if (from == "1")
            {
                int mnth_ids = My.tomonth_number(monthName);
                string month_id_in_two_dgt = My.getMonthS_twoDigit(mnth_ids.ToString());
                if (ViewState["checked_mnth"].ToString() == "0")
                {
                    ViewState["checked_frst_mnth"] = month_id_in_two_dgt;
                    ViewState["checked_after_frst_mnth"] = month_id_in_two_dgt;
                    ViewState["checked_mnth"] = "1";
                }
                else
                {
                    ViewState["checked_after_frst_mnth"] = month_id_in_two_dgt;
                }
            }

            DataTable dty = mycode.FillData("select Fine_mode from globle_data where Fine_mode='2'");
            if (dty.Rows.Count != 0)
            {
                string pay_date = mycode.date();
                int payidate = My.DateConvertToIdate(pay_date);

                #region DayRanGEWise

                //Advance Payment Check
                string crnt_year = mycode.year();
                string cunrt_session = My.get_session();
                string session_frst_year = cunrt_session.Substring(0, 4);
                int session_s_year = My.toint(session_frst_year);
                int s_year = My.toint(session_frst_year);


                int pay_month = My.toint(ViewState["checked_after_frst_mnth"].ToString());
                s_year = My.check_start_months(pay_month, s_year);

                int pay_month_with_year = My.toint(s_year + ViewState["checked_after_frst_mnth"].ToString());
                int crnt_month_with_year = My.toint(mycode.year() + mycode.get_current_month_id());
                //Advance Payment Check

                if (crnt_month_with_year >= pay_month_with_year)
                {
                    DataTable dtz = mycode.FillData("select top 1 * from Fine_master_day_range");
                    if (dtz.Rows.Count != 0)
                    {
                        ViewState["FineType"] = "DayWise";
                        string last_day_of_payments = "01" + "/" + month_id_in_two_dgts + "/" + s_year;
                        // string last_day_of_payments = "01" + "/" + month_id_in_two_dgts + "/" + session_s_year;


                        DateTime startdate1 = DateTime.ParseExact(last_day_of_payments, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        DateTime enddate1 = DateTime.ParseExact(pay_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                        System.TimeSpan diff = enddate1.Subtract(startdate1);
                        int totaldays = Convert.ToInt32(diff.Days);

                        ViewState["late_fine_between_day"] = startdate1 + " to " + enddate1;
                        ViewState["late_fine_no_of_day_month"] = totaldays;
                        ViewState["fine_date_From"] = last_day_of_payments;
                        ViewState["fine_date_To"] = pay_date;



                        DataTable dt_fine = mycode.FillData("select top 1 * from Fine_master_day_range where No_of_day>" + totaldays + " order by No_of_day asc");
                        if (dt_fine.Rows.Count != 0)
                        {
                            save_fine_amount(ViewState["sessionIDs"].ToString(), ViewState["Admission_no"].ToString(), month_id_in_two_dgts, My.toDouble(dt_fine.Rows[0]["Fine_amount"].ToString()));
                            bind_ttl_fee();
                        }
                        else
                        {
                            DataTable dt_fines = mycode.FillData("select top 1 * from Fine_master_day_range order by No_of_day desc");
                            if (dt_fines.Rows.Count != 0)
                            {
                                save_fine_amount(ViewState["sessionIDs"].ToString(), ViewState["Admission_no"].ToString(), month_id_in_two_dgts, My.toDouble(dt_fines.Rows[0]["Fine_amount"].ToString()));
                                bind_ttl_fee();
                            }
                            else
                            {
                                ViewState["fineAmt"] = "0";
                                bind_ttl_fee();
                            }
                        }
                    }
                }
                #endregion 
            }
            else
            {
                DataTable dt = mycode.FillData("select * from Fine_master where Status='1' and Session_id='" + ViewState["sessionIDs"].ToString() + "'");
                if (dt.Rows.Count != 0)
                {
                    string pay_date = mycode.date();// "12/02/2025";//;
                    int payidate = My.DateConvertToIdate(pay_date);
                    string fineType = dt.Rows[0]["Fine_type"].ToString();
                    //Advance Payment Check
                    string crnt_year = mycode.year();
                    string cunrt_session = ViewState["Session"].ToString();
                    string session_frst_year = cunrt_session.Substring(0, 4);
                    int session_s_year = My.toint(session_frst_year);
                    int s_year = My.toint(session_frst_year);
                    int s_yearFx = s_year;

                    int fine_aplicable_year = My.check_start_months(My.toInt(dt.Rows[0]["Applicable_from_month_or_quater_id"].ToString()), s_year);
                    int pay_month = My.toint(ViewState["checked_after_frst_mnth"].ToString());
                    s_year = My.check_start_months(pay_month, s_year);
                    if (fineType == "DayWise")//===== Days
                    {
                        #region DayWise

                        //int pay_month_with_year = My.toint(s_year + ViewState["checked_after_frst_mnth"].ToString());
                        //int crnt_month_with_year = My.toint(mycode.year() + mycode.get_current_month_id());
                        ////Advance Payment Check

                        //if (crnt_month_with_year >= pay_month_with_year)
                        //{
                        //    ViewState["FineType"] = "DayWise";
                        //    string applicable_month = My.getMonthS_twoDigit(dt.Rows[0]["Applicable_from_month_or_quater_id"].ToString());
                        //    string last_day_of_payment = dt.Rows[0]["Last_day_to_deposit_fees"].ToString() + "/" + applicable_month + "/" + s_year;
                        //    string fine_applicable_month = fine_aplicable_year + applicable_month;
                        //    if (My.toint(fine_applicable_month) <= pay_month_with_year)
                        //    {
                        //        string last_day_of_payments = "";
                        //        if (My.toint(fine_applicable_month) >= pay_month_with_year)
                        //        {
                        //            last_day_of_payments = dt.Rows[0]["Last_day_to_deposit_fees"].ToString() + "/" + applicable_month + "/" + s_year;
                        //        }
                        //        else
                        //        {
                        //            last_day_of_payments = dt.Rows[0]["Last_day_to_deposit_fees"].ToString() + "/" + month_id_in_two_dgts + "/" + s_year;
                        //        }
                        //        DateTime startdate1 = DateTime.ParseExact(last_day_of_payments, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        //        DateTime enddate1 = DateTime.ParseExact(pay_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);


                        //        System.TimeSpan diff = enddate1.Subtract(startdate1);
                        //        int totaldays = Convert.ToInt32(diff.Days);

                        //        ViewState["late_fine_between_day"] = startdate1 + " to " + enddate1;
                        //        ViewState["late_fine_no_of_day_month"] = totaldays;
                        //        ViewState["fine_date_From"] = last_day_of_payments;
                        //        ViewState["fine_date_To"] = pay_date;
                        //        if (My.toDouble(totaldays) > 0)
                        //        {
                        //            string fine_amt = dt.Rows[0]["Fine_amt_per_day_or_month"].ToString();
                        //            double ttl_fine_amt = My.toDouble(fine_amt) * totaldays;
                        //            save_fine_amount(ViewState["sessionIDs"].ToString(), ViewState["Admission_no"].ToString(), month_id_in_two_dgts, ttl_fine_amt);
                        //            bind_ttl_fee();
                        //        }
                        //        else
                        //        {
                        //            save_fine_amount(ViewState["sessionIDs"].ToString(), ViewState["Admission_no"].ToString(), month_id_in_two_dgts, 0);
                        //            bind_ttl_fee();
                        //        }
                        //    }

                        //}
                        #endregion

                        #region DayWise

                        string qry = "delete from Temp_fine_monthwise where Session_id='" + ViewState["sessionIDs"].ToString() + "' and Admission_no='" + ViewState["Admission_no"].ToString() + "' and Branch_id='" + ViewState["Branchid"].ToString() + "'";
                        My.exeSql(qry);
                        ViewState["fineAmt"] = "0.00";
                        bind_ttl_fee();

                        string isCalculated = "0";
                        int mgrowcount = RPMonth.Items.Count;
                        for (int ixi = 0; ixi < mgrowcount; ixi++)
                        {
                            CheckBox chkM = (CheckBox)RPMonth.Items[ixi].FindControl("chk_month");
                            if (chkM.Checked == true)
                            {
                                if (isCalculated == "0")
                                {
                                    string cunrt_sessionD = ViewState["session"].ToString();
                                    string session_frst_yearD = cunrt_sessionD.Substring(0, 4);
                                    int session_s_yearD = My.toint(session_frst_yearD);
                                    int s_yearD = My.toint(session_frst_yearD);
                                    Label lbl_Month = (Label)RPMonth.Items[ixi].FindControl("lbl_Month");
                                    int mnth_idssD = My.tomonth_number(lbl_Month.Text);
                                    string month_id_in_two_dgtsD = My.getMonthS_twoDigit(mnth_idssD.ToString());

                                    int pay_monthD = My.toint(month_id_in_two_dgtsD);
                                    s_yearD = My.check_start_months(pay_monthD, s_yearD);

                                    DateTime enddate1q = DateTime.ParseExact(pay_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                    string paymentMonthno = enddate1q.ToString("MM");

                                    int pay_month_with_year = My.toint(s_yearD + month_id_in_two_dgtsD);
                                    int crnt_month_with_year = My.toint(mycode.year() + paymentMonthno);
                                    //Advance Payment Check 
                                    int fine_aplicable_yearD = My.check_start_months(My.toInt(dt.Rows[0]["Applicable_from_month_or_quater_id"].ToString()), s_yearD);

                                    if (crnt_month_with_year >= pay_month_with_year)
                                    {
                                        ViewState["FineType"] = "DayWise";
                                        string applicable_month = My.getMonthS_twoDigit(dt.Rows[0]["Applicable_from_month_or_quater_id"].ToString());
                                        string last_day_of_payment = dt.Rows[0]["Last_day_to_deposit_fees"].ToString() + "/" + applicable_month + "/" + s_yearD;



                                        string fine_applicable_month = fine_aplicable_yearD + applicable_month;
                                        if (My.toint(fine_applicable_month) <= pay_month_with_year)
                                        {
                                            string last_day_of_payments = "";
                                            if (My.toint(fine_applicable_month) >= pay_month_with_year)
                                            {
                                                last_day_of_payments = dt.Rows[0]["Last_day_to_deposit_fees"].ToString() + "/" + applicable_month + "/" + s_yearD;
                                            }
                                            else
                                            {
                                                last_day_of_payments = dt.Rows[0]["Last_day_to_deposit_fees"].ToString() + "/" + month_id_in_two_dgtsD + "/" + s_yearD;
                                            }
                                            DateTime startdate1 = DateTime.ParseExact(last_day_of_payments, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                            DateTime enddate1 = DateTime.ParseExact(pay_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                                            System.TimeSpan diff = enddate1.Subtract(startdate1);
                                            int totaldays = Convert.ToInt32(diff.Days);

                                            ViewState["late_fine_between_day"] = startdate1 + " to " + enddate1;
                                            ViewState["late_fine_no_of_day_month"] = totaldays;
                                            ViewState["fine_date_From"] = last_day_of_payments;
                                            ViewState["fine_date_To"] = pay_date;
                                            if (My.toDouble(totaldays) > 0)
                                            {
                                                isCalculated = "1";
                                                string fine_amt = dt.Rows[0]["Fine_amt_per_day_or_month"].ToString();
                                                double ttl_fine_amt = My.toDouble(fine_amt) * totaldays;
                                                save_fine_amount(ViewState["sessionIDs"].ToString(), ViewState["Admission_no"].ToString(), month_id_in_two_dgtsD, ttl_fine_amt);
                                                bind_ttl_fee();
                                            }
                                            else
                                            {
                                                isCalculated = "1";
                                                save_fine_amount(ViewState["sessionIDs"].ToString(), ViewState["Admission_no"].ToString(), month_id_in_two_dgtsD, 0);
                                                bind_ttl_fee();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    else if (fineType == "MonthWise")//===== MonthWise
                    {
                        #region MonthWise
                        int mgrowcount = RPMonth.Items.Count;
                        for (int ixi = 0; ixi < mgrowcount; ixi++)
                        {
                            CheckBox chkM = (CheckBox)RPMonth.Items[ixi].FindControl("chk_month");
                            if (chkM.Checked == true)
                            {
                                Label lbl_Month = (Label)RPMonth.Items[ixi].FindControl("lbl_Month");
                                int mnth_ids = My.tomonth_number(lbl_Month.Text);
                                string pay_month_two_digit = My.getMonthS_twoDigit(mnth_ids.ToString());

                                int updated_years = My.check_start_months(My.toInt(pay_month_two_digit), s_yearFx);

                                ViewState["FineType"] = "MonthWise";
                                string applicable_month = My.getMonthS_twoDigit(dt.Rows[0]["Applicable_from_month_or_quater_id"].ToString());
                                string last_day_of_payment = dt.Rows[0]["Last_day_to_deposit_fees"].ToString() + "/" + applicable_month + "/" + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyy");
                                string last_day_of_payments = dt.Rows[0]["Last_day_to_deposit_fees"].ToString() + "/" + pay_month_two_digit + "/" + updated_years;
                                DateTime startdate1 = DateTime.ParseExact(last_day_of_payments, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                DateTime enddate1 = DateTime.ParseExact(pay_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);


                                int monthsDifference = GetMonthsDifference(startdate1, enddate1);
                                int monthNumber = startdate1.Month;
                                string till_date_paymnt = dt.Rows[0]["Last_day_to_deposit_fees"].ToString();
                                double ttl_fine = 0; int isfinecalculated = 0;
                                for (int m = 0; m < monthsDifference; m++)
                                {
                                    string monthNumbertwodgt = My.getMonthS_twoDigit(monthNumber.ToString());
                                    string monthNames = My.getMonthS_full_name(monthNumbertwodgt);
                                    int updated_year = My.check_start_months(My.toInt(monthNumbertwodgt), s_yearFx);
                                    string last_idate_of_payments = updated_year.ToString() + monthNumbertwodgt + till_date_paymnt;


                                    int pay_month_with_years = My.toint(updated_year + monthNumbertwodgt);
                                    int fine_aplicable_years = My.check_start_months(My.toInt(applicable_month), s_yearFx);
                                    string fine_applicable_months = fine_aplicable_years + applicable_month;
                                    if (My.toint(fine_applicable_months) <= pay_month_with_years)
                                    {
                                        if (ViewState["RepeatFine"].ToString() == "Yes")
                                        {
                                            if (My.dataTable("select * from Typewise_fee_collection where admission_no='" + ViewState["Admission_no"].ToString() + "' and session='" + ViewState["Session"].ToString() + "' and month='" + monthNames + "' and parameter='" + ViewState["parameter"].ToString() + "'").Rows.Count > 0)
                                            {
                                            }
                                            else
                                            {
                                                if (My.toIntS(last_idate_of_payments) < payidate)
                                                {
                                                    ttl_fine = ttl_fine + My.toDouble(dt.Rows[0]["Fine_amt_per_day_or_month"].ToString());
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (isfinecalculated == 0)
                                            {
                                                isfinecalculated = 1;
                                                if (My.dataTable("select * from Typewise_fee_collection where admission_no='" + ViewState["Admission_no"].ToString() + "' and session='" + ViewState["Session"].ToString() + "' and month='" + monthNames + "' and parameter='" + ViewState["parameter"].ToString() + "'").Rows.Count > 0)
                                                {
                                                }
                                                else
                                                {
                                                    if (My.toIntS(last_idate_of_payments) < payidate)
                                                    {
                                                        ttl_fine = My.toDouble(dt.Rows[0]["Fine_amt_per_day_or_month"].ToString());
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    monthNumber++;
                                    if (monthNumber == 13)
                                    {
                                        monthNumber = 1;
                                    }
                                }
                                ViewState["late_fine_between_day"] = startdate1 + " to " + enddate1;
                                ViewState["late_fine_no_of_day_month"] = "1";
                                ViewState["fine_date_From"] = last_day_of_payments;
                                ViewState["fine_date_To"] = pay_date;
                                if (ttl_fine > 0)
                                {
                                    save_fine_amount(ViewState["sessionIDs"].ToString(), ViewState["Admission_no"].ToString(), pay_month_two_digit, ttl_fine);
                                    bind_ttl_fee();
                                }
                                else
                                {
                                    save_fine_amount(ViewState["sessionIDs"].ToString(), ViewState["Admission_no"].ToString(), pay_month_two_digit, 0);
                                    bind_ttl_fee();
                                }
                            }
                        }
                        #endregion
                    }
                    else if (fineType == "NextMonthWise")//===== NextMonthWise
                    {
                        #region NextMonthWise
                        string pay_month_two_digit = My.getMonthS_twoDigit((pay_month + 1).ToString());
                        ViewState["FineType"] = "MonthWise";
                        string applicable_month = My.getMonthS_twoDigit(dt.Rows[0]["Applicable_from_month_or_quater_id"].ToString());






                        int fine_apply_from_year = My.check_start_months(My.toIntS(applicable_month), s_year);
                        string fine_apply_from_year_mnth = fine_apply_from_year.ToString() + applicable_month;
                        int pay_year_mnth = My.DateConvertToIyearMonth(pay_date);

                        //if (My.toIntS(fine_apply_from_year_mnth) > pay_year_mnth) { }
                        //else
                        //{
                        string last_day_of_payment = dt.Rows[0]["Last_day_to_deposit_fees"].ToString() + "/" + applicable_month + "/" + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyy");

                        string last_day_of_payments = "";
                        int month_of_fee_year = My.check_start_months(My.toIntS(pay_month_two_digit), s_year);
                        string month_of_fee_year_month = month_of_fee_year.ToString() + pay_month_two_digit.ToString();



                        if (My.toIntS(fine_apply_from_year_mnth) > pay_year_mnth)
                        {
                            last_day_of_payments = dt.Rows[0]["Last_day_to_deposit_fees"].ToString() + "/" + applicable_month + "/" + s_year;
                        }

                        //last_day_of_payments = dt.Rows[0]["Last_day_to_deposit_fees"].ToString() + "/" + pay_month_two_digit + "/" + s_year;
                        else if (My.toIntS(fine_apply_from_year_mnth) <= pay_year_mnth)
                        {
                            last_day_of_payments = dt.Rows[0]["Last_day_to_deposit_fees"].ToString() + "/" + pay_month_two_digit + "/" + s_year;
                        }


                        DateTime startdate1 = DateTime.ParseExact(last_day_of_payments, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        DateTime enddate1 = DateTime.ParseExact(pay_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);




                        System.TimeSpan diff = enddate1.Subtract(startdate1);
                        int totaldays = Convert.ToInt32(diff.Days);
                        int totalmonths = 0;




                        if (ViewState["RepeatFine"].ToString() == "Yes")
                        {
                            if (My.dataTable("select  * from Typewise_fee_collection  where admission_no='" + ViewState["Admission_no"].ToString() + "' and session='" + ViewState["Session"].ToString() + "' and month='" + monthName + "' and parameter='" + ViewState["parameter"].ToString() + "'").Rows.Count > 0)
                            {
                            }
                            else
                            {
                                if (totaldays > 0)
                                {
                                    if (30 >= totaldays)
                                    {
                                        totalmonths = 1;
                                    }
                                    else
                                    {
                                        totalmonths = Math.Abs(enddate1.Subtract(startdate1).Days / (365 / 12));
                                        double monthdays = 31;
                                        double reminder = My.toDouble(totaldays) % monthdays;

                                        if (29 > Math.Round(reminder))
                                        {
                                            totalmonths++;
                                        }
                                    }
                                }
                            }

                            string fineStartFromMonth = My.getMonthS_twoDigit(dt.Rows[0]["Applicable_from_month_or_quater_id"].ToString());
                            string fineStartFrom = s_year + fineStartFromMonth + dt.Rows[0]["Last_day_to_deposit_fees"].ToString();
                            if (My.toIntS(fineStartFrom) > My.DateConvertToIdate(pay_date))
                            {
                                totalmonths = 0;
                            }

                            ViewState["late_fine_between_day"] = startdate1 + " to " + enddate1;
                            ViewState["late_fine_no_of_day_month"] = totalmonths;
                            ViewState["fine_date_From"] = last_day_of_payments;
                            ViewState["fine_date_To"] = pay_date;
                            if (My.toDouble(totalmonths) > 0)
                            {
                                string fine_amt = dt.Rows[0]["Fine_amt_per_day_or_month"].ToString();
                                double ttl_fine_amt = My.toDouble(fine_amt) * totalmonths;
                                save_fine_amount(ViewState["sessionIDs"].ToString(), ViewState["Admission_no"].ToString(), month_id_in_two_dgts, ttl_fine_amt);
                                bind_ttl_fee();
                            }
                            else
                            {
                                save_fine_amount(ViewState["sessionIDs"].ToString(), ViewState["Admission_no"].ToString(), month_id_in_two_dgts, 0);
                                bind_ttl_fee();
                            }
                        }
                        else
                        {
                            if (My.dataTable("select  * from Typewise_fee_collection  where admission_no='" + ViewState["Admission_no"].ToString() + "' and session='" + ViewState["Session"].ToString() + "' and month='" + monthName + "' and parameter='" + ViewState["parameter"].ToString() + "'").Rows.Count > 0)
                            {
                            }
                            else
                            {
                                if (totaldays > 0)
                                {
                                    if (30 >= totaldays)
                                    {
                                        totalmonths = 1;
                                    }
                                    else
                                    {
                                        totalmonths = Math.Abs(enddate1.Subtract(startdate1).Days / (365 / 12));
                                    }
                                }
                            }
                            ViewState["late_fine_between_day"] = startdate1 + " to " + enddate1;
                            ViewState["late_fine_no_of_day_month"] = totalmonths;
                            ViewState["fine_date_From"] = last_day_of_payment;
                            ViewState["fine_date_To"] = pay_date;
                            if (My.toDouble(totalmonths) > 0)
                            {
                                string fine_amt = dt.Rows[0]["Fine_amt_per_day_or_month"].ToString();
                                double ttl_fine_amt = My.toDouble(fine_amt) * 1;
                                save_fine_amount(ViewState["sessionIDs"].ToString(), ViewState["Admission_no"].ToString(), month_id_in_two_dgts, ttl_fine_amt);
                                bind_ttl_fee();
                            }
                            else
                            {
                                save_fine_amount(ViewState["sessionIDs"].ToString(), ViewState["Admission_no"].ToString(), month_id_in_two_dgts, 0);
                                bind_ttl_fee();
                            }
                        }
                        //}
                        #endregion
                    }
                    else
                    {

                        #region QuarterWise
                        ViewState["FineType"] = "QuarterWise";
                        double fnl_fine_amt = 0;
                        SqlDataAdapter ad = new SqlDataAdapter("select * from Fine_master where Status='1' and Session_id='" + ViewState["sessionIDs"].ToString() + "' and Q_start_month<='" + ViewState["checked_after_frst_mnth"].ToString() + "'  order by Q_start_month asc", My.conn);
                        DataSet ds = new DataSet();
                        ad.Fill(ds, "Fine_master");
                        DataTable dtm = ds.Tables[0];
                        int rowcount = ds.Tables[0].Rows.Count;
                        if (rowcount == 0)
                        {
                        }
                        else
                        {
                            foreach (DataRow dr in dtm.Rows)
                            {
                                string endmnth_in_two = My.getMonthS_twoDigit(dr["Q_end_month"].ToString());
                                string last_day_of_payment_q = dr["Last_day_to_deposit_fees"].ToString() + "/" + endmnth_in_two + "/" + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyy");




                                DateTime startdate1q = DateTime.ParseExact(last_day_of_payment_q, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                DateTime enddate1q = DateTime.ParseExact(pay_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                                if (dr["Q_payment_mode"].ToString() == "Day")
                                {
                                    System.TimeSpan diff = enddate1q.Subtract(startdate1q);
                                    int totaldays = Convert.ToInt32(diff.Days);
                                    ViewState["late_fine_no_of_day_month"] = totaldays;

                                    if (ViewState["flags1"].ToString() == "0")
                                    {
                                        ViewState["fine_date_From"] = last_day_of_payment_q;
                                        ViewState["flags1"] = "1";
                                    }

                                    ViewState["fine_date_To"] = pay_date;

                                    if (My.toDouble(totaldays) > 0)
                                    {
                                        string fine_amt = dr["Fine_amt_per_day_or_month"].ToString();
                                        double ttl_fine_amt = My.toDouble(fine_amt) * totaldays;
                                        fnl_fine_amt = fnl_fine_amt += ttl_fine_amt;
                                    }
                                    else
                                    {
                                        fnl_fine_amt = fnl_fine_amt += 0;
                                    }
                                }
                                else
                                {
                                    System.TimeSpan diff = enddate1q.Subtract(startdate1q);
                                    int totaldays = Convert.ToInt32(diff.Days);
                                    ViewState["late_fine_no_of_day_month"] = dtm.Rows.Count.ToString();

                                    if (ViewState["flags1"].ToString() == "0")
                                    {
                                        ViewState["fine_date_From"] = last_day_of_payment_q;
                                        ViewState["flags1"] = "1";
                                    }

                                    ViewState["fine_date_To"] = pay_date;
                                    if (My.toDouble(totaldays) > 0)
                                    {
                                        string fine_amt = dr["Fine_amt_per_day_or_month"].ToString();
                                        //double ttl_fine_amt = My.toDouble(fine_amt) * totaldays;
                                        double ttl_fine_amt = My.toDouble(fine_amt);
                                        fnl_fine_amt = fnl_fine_amt += ttl_fine_amt;
                                    }
                                    else
                                    {
                                        fnl_fine_amt = fnl_fine_amt += 0;
                                    }
                                }
                            }

                            ViewState["fineAmt"] = fnl_fine_amt.ToString("0.00");
                            bind_ttl_fee();
                        }
                        #endregion
                    }
                }
            }
        }
        static int GetMonthsDifference(DateTime startDate, DateTime endDate)
        {
            // Calculate the total months difference
            int monthsApart = (endDate.Year - startDate.Year) * 12 + endDate.Month - startDate.Month;

            // If the day of the end date is less than the day of the start date, subtract one month
            if (monthsApart <= 0)
            {
                monthsApart = 1;
            }
            else
            {
                monthsApart++;
            }
            return monthsApart;
        }


        private void save_fine_amount(string session_id, string admission_no, string month_id_in_two_dgts, double ttl_fine_amt)
        {
            if (ViewState["Boarding_Point_id"].ToString() == "0")
            {

            }
            else
            {
                double tranportfine_multiply = My.Get_tranport_fine_amount();
                ttl_fine_amt = ttl_fine_amt * tranportfine_multiply;
            }

            if (mycode.IsUserExist("select Id from Temp_fine_monthwise where Session_id='" + session_id + "' and Admission_no='" + admission_no + "' and Month_id='" + month_id_in_two_dgts + "' and Branch_id='" + ViewState["Branchid"].ToString() + "'"))
            {
                SqlCommand cmd;
                string query = "INSERT INTO Temp_fine_monthwise (Session_id,Admission_no,Month_id,Fine_amount,Branch_id) values (@Session_id,@Admission_no,@Month_id,@Fine_amount,@Branch_id);";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Session_id", session_id);
                cmd.Parameters.AddWithValue("@Admission_no", admission_no);
                cmd.Parameters.AddWithValue("@Month_id", month_id_in_two_dgts);
                cmd.Parameters.AddWithValue("@Fine_amount", ttl_fine_amt);
                cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branchid"].ToString());
                if (My.InsertUpdateData(cmd))
                {
                }
            }
            else
            {
                SqlCommand cmd;
                string query = "Update Temp_fine_monthwise set Fine_amount=@Fine_amount  where Session_id='" + session_id + "' and Admission_no='" + admission_no + "' and Month_id='" + month_id_in_two_dgts + "' and Branch_id='" + ViewState["Branchid"].ToString() + "'";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Fine_amount", ttl_fine_amt);
                if (My.InsertUpdateData(cmd))
                {
                }
            }


            double total_fine = 0;
            int growcountS = RPMonth.Items.Count;
            for (int iS = 0; iS < growcountS; iS++)
            {
                Label lbl_Month = (Label)RPMonth.Items[iS].FindControl("lbl_Month");
                CheckBox chk_month = (CheckBox)RPMonth.Items[iS].FindControl("chk_month");
                int mnth_idss = My.tomonth_number(lbl_Month.Text);
                string month_id = My.getMonthS_twoDigit(mnth_idss.ToString());

                if (chk_month.Checked == true)
                {
                    DataTable dt = mycode.FillData("select Fine_amount from Temp_fine_monthwise where Session_id='" + session_id + "' and Admission_no='" + admission_no + "' and Month_id='" + month_id + "' and Branch_id='" + ViewState["Branchid"].ToString() + "'");
                    if (dt.Rows.Count > 0)
                    {
                        total_fine = total_fine + My.toDouble(dt.Rows[0]["Fine_amount"].ToString());
                    }
                    else
                    {
                        total_fine = total_fine + 0;
                    }
                }
            }


            ViewState["fineAmt"] = total_fine.ToString("0.00");
        }



        #region automatic payment
        private bool bind_not_paymnet_datat(string admission_no, string session_id)
        {
            string query = " select  top  1 * from dbo.[Payment_transaction_process] where Admission_no='" + admission_no + "' and Session_id='" + session_id + "' and status='Pending' order by Id desc ";

            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                return false;
            }
            else
            {
                a1.Visible = true;
                return true;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            //automatic_payment();



        }

        private void automatic_payment()
        {
            string error_description = "";
            string order_id = "";
            string Status = "";
            string Payment_order_id = "";
            string query = " select  top  1 * from dbo.[Payment_transaction_process] where Admission_no='" + ViewState["regid"].ToString() + "' and Session_id='" + ViewState["sessionid"].ToString() + "' and status='Pending' order by Id desc ";

            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {


            }
            else
            {

                if (ViewState["Getwey_Type"].ToString() == "Razorpay")
                {
                    try
                    {
                        Dictionary<string, object> dc1 = Re_payment_check_payment.get_payment_status_Razorpay(ViewState["regid"].ToString(), ViewState["sessionid"].ToString(), dt.Rows[0]["razorpay_order_id"].ToString());
                        order_id = (String)dc1["order_id"];
                        Status = (String)dc1["Status"];
                        Payment_order_id = (String)dc1["Payment_order_id"];
                        error_description = (String)dc1["error_description"];
                    }
                    catch
                    {
                        Status = "Unpaid";
                        order_id = dt.Rows[0]["razorpay_order_id"].ToString(); ;
                        Payment_order_id = dt.Rows[0]["ordertrackingid"].ToString();

                    }

                }
                if (ViewState["Getwey_Type"].ToString() == "Razorpay_new")
                {
                    try
                    {
                        Dictionary<string, object> dc1 = Re_payment_check_payment.get_payment_status_Razorpay(ViewState["regid"].ToString(), ViewState["sessionid"].ToString(), dt.Rows[0]["razorpay_order_id"].ToString());
                        order_id = (String)dc1["order_id"];
                        Status = (String)dc1["Status"];
                        Payment_order_id = (String)dc1["Payment_order_id"];
                        error_description = (String)dc1["error_description"];
                    }
                    catch (Exception ex)
                    {
                        Status = "Unpaid";
                        order_id = dt.Rows[0]["razorpay_order_id"].ToString(); ;
                        Payment_order_id = dt.Rows[0]["ordertrackingid"].ToString();
                        error_description = "";

                    }

                }

                else if (ViewState["Getwey_Type"].ToString() == "EGPay")
                {
                    Dictionary<string, object> dc1 = Re_payment_check_payment_EZpay.get_payment_status_EZ_Pay(ViewState["regid"].ToString(), ViewState["sessionid"].ToString(), dt.Rows[0]["ordertrackingid"].ToString());
                    order_id = (String)dc1["order_id"];
                    Status = (String)dc1["Status"];
                    Payment_order_id = (String)dc1["Payment_order_id"];
                    error_description = (String)dc1["error_description"];


                }
                else if (ViewState["Getwey_Type"].ToString() == "Getepay")
                {
                    Dictionary<string, object> dc1 = Re_payment_check_payment_Get_pay.get_payment_status_Get_pay(ViewState["regid"].ToString(), ViewState["sessionid"].ToString(), dt.Rows[0]["razorpay_order_id"].ToString());
                    order_id = (String)dc1["order_id"];
                    Status = (String)dc1["Status"];
                    Payment_order_id = (String)dc1["Payment_order_id"];
                }
                else if (ViewState["Getwey_Type"].ToString() == "ICICI")
                {
                    Dictionary<string, object> dc1 = Re_payment_check_payment_ICIC.get_payment_status_ICIC_Pay(ViewState["regid"].ToString(), ViewState["sessionid"].ToString(), dt.Rows[0]["ordertrackingid"].ToString());
                    order_id = (String)dc1["order_id"];
                    Status = (String)dc1["Status"];
                    Payment_order_id = (String)dc1["Payment_order_id"];
                    error_description = (String)dc1["error_description"];
                }
                if (Status == "Success")
                {
                    My.exeSql("update Payment_transaction_process set razorpay_payment_id='" + Payment_order_id + "',status='Success',Remarks='" + error_description + "'  where  razorpay_order_id='" + order_id + "' ");
                    bool status = mypayment.save_final_payment(dt.Rows[0]["Admission_no"].ToString(), dt.Rows[0]["ordertrackingid"].ToString());
                    if (status == true)
                    {
                    }
                    else
                    {
                    }
                }
                else
                {
                    My.exeSql("update Payment_transaction_process set razorpay_payment_id='" + Payment_order_id + "',status='" + Status + "',Remarks='" + error_description + "'  where  razorpay_order_id='" + order_id + "'");
                }

            }
            a1.Visible = false;
        }

        #endregion


    }
}