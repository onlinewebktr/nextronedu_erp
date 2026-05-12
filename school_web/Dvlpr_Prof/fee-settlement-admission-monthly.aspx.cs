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

namespace school_web.Dvlpr_Prof
{
    public partial class fee_settlement_admission_monthly : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["branchid"] = "1";
            ViewState["Userid"] = "1";
        }

        My mycode = new My();
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtP = My.dataTable("select * from AaaTempBulkPayment");
                if (dtP.Rows.Count > 0)
                {
                    foreach (DataRow drP in dtP.Rows)
                    {
                        txttotalbill.Text = drP["Amount"].ToString();
                        txt_payment_date.Text = drP["Payment_date"].ToString();
                        lbl_payment_mode.Text = drP["Payment_mode"].ToString();

                        string query = "select * from admission_registor where admissionserialnumber='" + drP["Admission_no"].ToString() + "' and Session='15' and Status='1' order by id asc";
                        ViewState["Admission_Annual_Amount"] = "0";
                        SqlDataAdapter ad_contactus = new SqlDataAdapter(query, My.conn);
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
                                ViewState["parameterDisc"] = "4";
                                if (dt.Rows[0]["hosteltaken"].ToString() == "")
                                {
                                    ViewState["hostaltakenDues"] = "No";
                                }
                                else if (dt.Rows[0]["hosteltaken"].ToString().ToLower() == "no")
                                {
                                    ViewState["hostaltakenDues"] = "No";
                                }
                                else
                                {
                                    ViewState["hostaltakenDues"] = dt.Rows[0]["hosteltaken"].ToString();
                                }


                                lbl_class.Text = dr["class"].ToString();
                                lbl_session.Text = dr["session"].ToString();
                                txt_payment_date.Text = mycode.date();
                                lbl_admissionno.Text = dr["admissionserialnumber"].ToString();
                                txt_admission_no.Text = dr["admissionserialnumber"].ToString();
                                lbl_name.Text = dr["studentname"].ToString();


                                ViewState["Section"] = dr["Section"].ToString();
                                ViewState["class_name"] = dr["class"].ToString();
                                ViewState["class_id"] = dr["Class_id"].ToString();
                                ViewState["parameter"] = dr["hosteltaken"].ToString().ToUpper() == "YES" ? "HostelMonthlyFee" : "MonthlyFee";
                                ViewState["parameter_id"] = dr["hosteltaken"].ToString().ToUpper() == "YES" ? "3" : "4";
                                ViewState["Admission_no"] = dr["admissionserialnumber"].ToString();
                                ViewState["hostel_id"] = My.toint(dr["Hostel_id"].ToString());

                                // confussion 
                                ViewState["day_bording"] = My.toBool(dr["is_applied_dayboarding"]);
                                ViewState["day_bording_with_lunch"] = My.toBool(dr["day_boarding_with_lunch"]);

                                lblhostel.Text = dr["hosteltaken"].ToString();

                                ViewState["group_id"] = "3";
                                ViewState["category_id"] = dr["category_id"].ToString();
                                ViewState["sub_category_id"] = dr["SubCategory_id"].ToString();
                                ViewState["classid"] = dr["Class_id"].ToString();
                                ViewState["Section"] = dr["Section"].ToString();
                                ViewState["sessionIDs"] = dr["Session_id"].ToString();
                                ViewState["sessionid"] = dr["Session_id"].ToString();
                                ViewState["session"] = dr["session"].ToString();
                                ViewState["Transfer_Status"] = dr["Transfer_Status"].ToString();

                                Dictionary<string, object> dc1 = mycode.Bind_hostel_data_for_assined_student(ViewState["sessionIDs"].ToString(), ViewState["classid"].ToString(), ViewState["Admission_no"].ToString());
                                ViewState["Hostel_id"] = (String)dc1["Hostel_id"];
                                ViewState["Room_Category_id"] = (String)dc1["Room_Category_id"];
                                ViewState["From_month_name"] = (String)dc1["From_month_name"];
                                ViewState["From_month_id"] = (String)dc1["From_month_id"];
                                ViewState["Assined_Year_Month"] = (String)dc1["Assined_Year_Month"];
                                ViewState["Hostel_assign_id"] = (String)dc1["Hostel_assign_id"];


                                if (ViewState["Transfer_Status"].ToString() == "New")
                                {
                                    //Admission
                                    find_admission_dues_fee();
                                }
                                else
                                {
                                    //Annual
                                    find_annual_dues_fee();
                                }






                                //===============================
                                #region For Admission 
                                ViewState["Section"] = dr["Section"].ToString();
                                ViewState["category_id"] = dr["Category_id"].ToString();
                                ViewState["SubCategory_id"] = dr["SubCategory_id"].ToString();
                                ViewState["rollnumber"] = dr["rollnumber"].ToString();
                                if (dr["hosteltaken"].ToString() == "")
                                {
                                    ViewState["hostaltaken"] = "No";
                                }
                                else if (dr["hosteltaken"].ToString().ToLower() == "no")
                                {
                                    ViewState["hostaltaken"] = "No";
                                }
                                else
                                {
                                    ViewState["hostaltaken"] = dr["hosteltaken"].ToString();
                                }

                                #endregion

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


                                if (dr["Transportation_Id"].ToString() == "")
                                {
                                    ViewState["transportID"] = "0";
                                }
                                else
                                {
                                    ViewState["transportID"] = dr["Transportation_Id"].ToString();
                                }
                                Dictionary<string, object> dc2 = mycode.Bind_Transport_data_for_assined_student(ViewState["sessionIDs"].ToString(), ViewState["classid"].ToString(), ViewState["Admission_no"].ToString());
                                ViewState["Transport_id"] = (String)dc2["Transport_id"];
                                ViewState["TransportPath_id"] = (String)dc2["TransportPath_id"];
                                ViewState["Boarding_Point_id"] = (String)dc2["Boarding_Point_id"];
                                ViewState["Transport_Assigned_Id"] = (String)dc2["Transport_Assigned_Id"];
                                ViewState["Month_name"] = (String)dc2["Month_name"];
                                ViewState["Month_id"] = (String)dc2["Month_id"];
                                ViewState["Year_month"] = (String)dc2["Year_month"];
                                ViewState["Sheet_Id"] = (String)dc2["Sheet_Id"];






                                My.exeSql("delete from Typewise_fee_collection where admission_no='" + dr["admissionserialnumber"].ToString() + "' and (parameter='MonthlyFee' or parameter='HostelMonthlyFee') and  transection=''");
                                // select transection from Typewise_fee_collection where parameter = 'MonthlyFee' order by Id desc

                                make_payments();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }



        private void find_admission_dues_fee()
        {
            string qry = "";
            string parameter_id = "";
            string parameter_id2 = "";
            string parameter = "";
            string parameter2 = "";

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



            if (ViewState["Hostel_id"].ToString() == "0")
            {

                qry = "select *,(payable-cast(disc_amount as float)-cast(paid as float)) net_payable from(select '0' payable_after_disc,session,feetype,cast(payable as float) payable,paid,dues,status,content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + ViewState["Admission_no"].ToString() + "'  and (parameter_id='" + parameter_id + "' or parameter_id='" + parameter_id2 + "') and session='" + ViewState["session"].ToString() + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and (parameter_id='" + parameter_id + "' or parameter_id='" + parameter_id2 + "') and session='" + ViewState["session"].ToString() + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'))),0) disc_amount from dbo.[Typewise_fee_collection] WHERE admission_no='" + ViewState["Admission_no"].ToString() + "' and (parameter='" + parameter + "')  and session='" + ViewState["session"].ToString() + "' and class_id=" + ViewState["classid"].ToString() + ")t";

            }
            else
            {
                qry = "select *,(payable-cast(disc_amount as float)-cast(paid as float)) net_payable from(select '0' payable_after_disc,session,feetype,cast(payable as float) payable,paid,dues,status,content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where Hostel_id=" + ViewState["Hostel_id"].ToString() + " and admission_no='" + ViewState["Admission_no"].ToString() + "'  and (parameter_id='" + parameter_id + "' or parameter_id='" + parameter_id2 + "') and session='" + ViewState["session"].ToString() + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Hostel_id=" + ViewState["Hostel_id"].ToString() + " and Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and (parameter_id='" + parameter_id + "' or parameter_id='" + parameter_id2 + "') and session='" + ViewState["session"].ToString() + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'))),0) disc_amount from dbo.[Typewise_fee_collection] WHERE admission_no='" + ViewState["Admission_no"].ToString() + "' and (parameter='" + parameter + "' )  and session='" + ViewState["session"].ToString() + "' and class_id=" + ViewState["classid"].ToString() + ")t";

            }


            DataTable fee_dt = My.dataTable(qry);
            if (fee_dt.Rows.Count == 0)
            {
                if (ViewState["Hostel_id"].ToString() == "0")
                {
                    qry = "select *,(payable-cast(disc_amount as float)) net_payable from(select '0' payable_after_disc,fmc.session,cm.content feetype,cast(fmc.amount as float) payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + ViewState["Admission_no"].ToString() + "'  and (parameter_id='" + parameter_id + "' or parameter_id='" + parameter_id2 + "') and session='" + ViewState["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and (parameter_id='" + parameter_id + "' or parameter_id='" + parameter_id2 + "') and session='" + ViewState["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'))),0) disc_amount   from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where (parameter='" + parameter + "' )  and session='" + ViewState["session"].ToString() + "' and class_id='" + ViewState["classid"].ToString() + "') t  UNION ALL select '0' as payable_after_disc, session as Session,Perticular as feetype,Amount as payable,  '0' as paid, Amount as dues,'Dues' as status,'ADM01' as content_id,'0' as disc_amount,Amount as net_payable from Misc_Fee_Master_Studentwise where (Type_Mode='AdmissionFee' or Type_Mode='AnnualFee') and session='" + ViewState["session"].ToString() + "' and Admission_No='" + ViewState["Admission_no"].ToString() + "'";
                }
                else
                {
                    qry = "select *,(payable-cast(disc_amount as float)) net_payable from(select '0' payable_after_disc,fmc.session,cm.content feetype,cast(fmc.amount as float) payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where Hostel_id=" + ViewState["Hostel_id"].ToString() + " and admission_no='" + ViewState["Admission_no"].ToString() + "'  and (parameter_id='" + parameter_id + "' or parameter_id='" + parameter_id2 + "') and session='" + ViewState["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Hostel_id=" + ViewState["Hostel_id"].ToString() + " and Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and (parameter_id='" + parameter_id + "' or parameter_id='" + parameter_id2 + "') and session='" + ViewState["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'))),0) disc_amount   from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where (parameter='" + parameter + "' )  and session='" + ViewState["session"].ToString() + "' and class_id='" + ViewState["classid"].ToString() + "' and  fmc.Hostel_Id='" + ViewState["Hostel_id"].ToString() + "' ) t  UNION ALL select '0' as payable_after_disc, session as Session,Perticular as feetype,Amount as payable,  '0' as paid, Amount as dues,'Dues' as status,'ADM01' as content_id,'0' as disc_amount,Amount as net_payable from Misc_Fee_Master_Studentwise where (Type_Mode='AdmissionFee' or Type_Mode='AnnualFee') and session='" + ViewState["session"].ToString() + "' and Admission_No='" + ViewState["Admission_no"].ToString() + "'";
                }
                fee_dt = My.dataTable(qry);
            }


            DataTable dt = PayrollMy.dataTable(qry);
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

                ViewState["Admission_Annual_Amount"] = payble_after_disc.ToString();

                txt_adm_ann_fee.Text = "0";
                if (payble_after_disc > 0)
                {
                    txt_adm_ann_fee.Text = payble_after_disc.ToString();
                }
            }
        }


        private void find_annual_dues_fee()
        {
            string qry = "";
            string parameter_id = "";
            string parameter_id2 = "";
            string parameter = "";
            if (ViewState["Transfer_Status"].ToString() == "New")
            {
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
                if (ViewState["Hostel_id"].ToString() == "0")
                {
                    parameter = "AnnualFee";

                }
                else
                {
                    parameter = "HostelAnnualFee";
                }


            }



            parameter_id = "2";// annulfee
            parameter_id2 = "6";// admission fee for hostel

            if (ViewState["Hostel_id"].ToString() == "0")
            {

                qry = "select *,(payable-cast(disc_amount as float)-cast(paid as float)) net_payable from(select '0' payable_after_disc,session,feetype,cast(payable as float) payable,paid,dues,status,content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + ViewState["Admission_no"].ToString() + "'  and (parameter_id='" + parameter_id + "' or parameter_id='" + parameter_id2 + "') and session='" + ViewState["session"].ToString() + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='Annual' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and (parameter_id='" + parameter_id + "' or parameter_id='" + parameter_id2 + "') and session='" + ViewState["session"].ToString() + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='Annual' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'))),0) disc_amount from dbo.[Typewise_fee_collection] WHERE admission_no='" + ViewState["Admission_no"].ToString() + "' and (parameter='" + parameter + "')  and session='" + ViewState["session"].ToString() + "' and class_id=" + ViewState["classid"].ToString() + ")t";

            }
            else
            {
                parameter = "HostelAnnualFee";
                qry = "select *,(payable-cast(disc_amount as float)-cast(paid as float)) net_payable from(select '0' payable_after_disc,session,feetype,cast(payable as float) payable,paid,dues,status,content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where Hostel_id=" + ViewState["Hostel_id"].ToString() + " and admission_no='" + ViewState["Admission_no"].ToString() + "'  and (parameter_id='" + parameter_id + "' or parameter_id='" + parameter_id2 + "') and session='" + ViewState["session"].ToString() + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='Annual' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Hostel_id=" + ViewState["Hostel_id"].ToString() + " and Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and (parameter_id='" + parameter_id + "' or parameter_id='" + parameter_id2 + "') and session='" + ViewState["session"].ToString() + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='Annual' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'))),0) disc_amount from dbo.[Typewise_fee_collection] WHERE admission_no='" + ViewState["Admission_no"].ToString() + "' and (parameter='" + parameter + "' )  and session='" + ViewState["session"].ToString() + "' and class_id=" + ViewState["classid"].ToString() + ")t";

            }


            DataTable fee_dt = My.dataTable(qry);
            if (fee_dt.Rows.Count == 0)
            {
                if (ViewState["Hostel_id"].ToString() == "0")
                {
                    qry = "select *,(payable-cast(disc_amount as float)) net_payable from(select '0' payable_after_disc,fmc.session,cm.content feetype,cast(fmc.amount as float) payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + ViewState["Admission_no"].ToString() + "'  and (parameter_id='" + parameter_id + "' or parameter_id='" + parameter_id2 + "') and session='" + ViewState["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='Annual' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and (parameter_id='" + parameter_id + "' or parameter_id='" + parameter_id2 + "') and session='" + ViewState["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='Annual' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'))),0) disc_amount   from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where (parameter='" + parameter + "' )  and session='" + ViewState["session"].ToString() + "' and class_id='" + ViewState["classid"].ToString() + "') t  UNION ALL select '0' as payable_after_disc, session as Session,Perticular as feetype,Amount as payable,  '0' as paid, Amount as dues,'Dues' as status,'ANN01' as content_id,'0' as disc_amount,Amount as net_payable from Misc_Fee_Master_Studentwise where (Type_Mode='AdmissionFee' or Type_Mode='AnnualFee') and session='" + ViewState["session"].ToString() + "' and Admission_No='" + ViewState["Admission_no"].ToString() + "'";
                }
                else
                {

                    qry = "select *,(payable-cast(disc_amount as float)) net_payable from(select '0' payable_after_disc,fmc.session,cm.content feetype,cast(fmc.amount as float) payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where Hostel_id=" + ViewState["Hostel_id"].ToString() + " and admission_no='" + ViewState["Admission_no"].ToString() + "'  and (parameter_id='" + parameter_id + "' or parameter_id='" + parameter_id2 + "') and session='" + ViewState["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='Annual' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Hostel_id=" + ViewState["Hostel_id"].ToString() + " and Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and (parameter_id='" + parameter_id + "' or parameter_id='" + parameter_id2 + "') and session='" + ViewState["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='Annual' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'))),0) disc_amount   from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where (parameter='" + parameter + "' )  and session='" + ViewState["session"].ToString() + "' and class_id='" + ViewState["classid"].ToString() + "' and  fmc.Hostel_Id='" + ViewState["Hostel_id"].ToString() + "') t  UNION ALL select '0' as payable_after_disc, session as Session,Perticular as feetype,Amount as payable,  '0' as paid, Amount as dues,'Dues' as status,'ANN01' as content_id,'0' as disc_amount,Amount as net_payable from Misc_Fee_Master_Studentwise where (Type_Mode='AdmissionFee' or Type_Mode='AnnualFee') and session='" + ViewState["session"].ToString() + "' and Admission_No='" + ViewState["Admission_no"].ToString() + "'";
                }
                fee_dt = My.dataTable(qry);
            }



            DataTable dt = PayrollMy.dataTable(qry);
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
                string prev_dues = gt_previous_amount();
                double totalpay = payble_after_disc + My.toDouble(prev_dues);

                ViewState["Admission_Annual_Amount"] = totalpay.ToString();
                txt_adm_ann_fee.Text = "0";
                if (totalpay > 0)
                {
                    txt_adm_ann_fee.Text = payble_after_disc.ToString();
                }
            }
        }

        private string gt_previous_amount()
        {
            string ReturN = "0";
            DataTable dt = PayrollMy.dataTable("select Dues_Amount from Previous_Year_Dues  where Session_id='" + ViewState["sessionIDs"].ToString() + "' and AdmissionNumber='" + ViewState["Admission_no"].ToString() + "'  and Class_id='" + ViewState["classid"].ToString() + "' and Status='Unpaid'");
            if (dt.Rows.Count > 0)
            {
                ReturN = dt.Rows[0][0].ToString();
            }
            return ReturN;
        }

        private void make_payments()
        {
            try
            {
                string paid_amount = "0";
                List<string> month_lst = new List<string>();
                bool payment = false;

                string slipno = "", entry_id = "";
                bool flag = false; double admissionPaid = 0; string paymentType = "Monthly"; double monthlYPaid = 0;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
                {
                    SqlConnection con = new SqlConnection(My.conn);
                    con.Open();
                    #region payCode 

                    monthlYPaid = My.toDouble(paid_amount);
                    #region AdmissionAnnuaL
                    string feeTypes = "";

                    if (My.toDouble(paid_amount) > My.toDouble(txt_adm_ann_fee.Text))
                    {
                        monthlYPaid = My.toDouble(paid_amount) - My.toDouble(txt_adm_ann_fee.Text);
                        admissionPaid = My.toDouble(txt_adm_ann_fee.Text);
                        paymentType = "MonthlyAdmission";
                    }
                    else
                    {
                        monthlYPaid = 0;
                        admissionPaid = My.toDouble(paid_amount);
                        paymentType = "Admission";
                    }

                    if (ViewState["Transfer_Status"].ToString() == "New")
                    {
                        feeTypes = "Admission";
                        make_admission_fee(con, admissionPaid);
                    }
                    else
                    {
                        feeTypes = "Annual";
                        make_annual_fee(con, admissionPaid);
                    }


                    #endregion 

                    if (monthlYPaid > 0)
                    {
                        string total_paid = monthlYPaid.ToString();
                        slipno = payments.invoice_monthly("slip_no", con);

                        entry_id = payments.auto_serialS("entry_id", con);
                        calculate_dues_for_new_month(slipno, con, monthlYPaid);
                        payments.exeSql("update Typewise_fee_collection set position=( select top 1 Position from dbo.[Month_Index] where Month=Typewise_fee_collection.month)", con);
                        send_data_in_student_payment_history("Monthly", slipno, entry_id, month_lst, total_paid, con);
                        payments.send_data_to_user_log_history(ViewState["Userid"].ToString(), ViewState["Userid"].ToString() + " received Monthly fee :-" + total_paid + " rs,Slip No :- " + slipno + " from " + lbl_name.Text + ", Admission No :-" + txt_admission_no.Text, con);
                    }

                    flag = true;
                    con.Close();
                    scope.Complete();

                    #endregion
                }

                //TRANSITION COMPLETE
                if (flag == true)
                {
                }
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }


        void send_data_in_student_payment_history(string type, string slip_no, string entry_id, List<string> month_lst, string total_paid, SqlConnection con)
        {
            SqlDataAdapter ad = new SqlDataAdapter("select top 1 * from Student_Payment_History where Addmission_no='" + txt_admission_no.Text + "'", con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Student_Payment_History");
            DataTable dt = ds.Tables[0];
            DataRow dr = dt.NewRow();
            dr["Addmission_no"] = txt_admission_no.Text;
            dr["Session"] = ViewState["session"].ToString();
            dr["Date"] = txt_payment_date.Text;
            dr["Idate"] = Convert.ToDateTime(txt_payment_date.Text).ToString("yyyyMMdd");
            dr["Description"] = type + " fee collection for " + "WAHAB" + " Month " + String.Join(",", month_lst) + ", Paid Amount : " + total_paid + " /-";
            dr["Entry_id"] = entry_id;
            dr["Slip_no"] = slip_no;
            dr["Amount"] = My.toDouble(total_paid).ToString("0.00");
            dr["Type"] = type;
            dr["mode"] = "CASH";
            dr["Pay_mode_transaction_no"] = "";
            dr["discount"] = My.toDouble(txt_discount.Text).ToString("0.00");
            dr["Discoun_in_School"] = 0;
            dr["Discoun_in_Hostel"] = 0;
            dr["Discoun_in_Transport"] = 0;
            dr["fine"] = "0";
            dr["is_ofline_sync"] = true;
            dr["Is_online_sync"] = false;
            dr["time"] = mycode.time();
            dr["user_id"] = ViewState["Userid"].ToString();
            dr["Remarks"] = "";
            dr["Class_id"] = ViewState["classid"].ToString();
            dr["Transection_in"] = "Software";
            dr["Branch"] = ViewState["Branchid"].ToString();
            dr["parameter_New"] = ViewState["parameter"].ToString();
            dr["Bank_name"] = "";
            dr["Bank_date"] = "";

            dt.Rows.Add(dr);
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
            //send data in school ledger
            update_School_ledger(slip_no, entry_id, total_paid, con);

            //
            string app_payment_type = "Software";//My.session("App_fee_collection_type");
            DataTable sdt = payments.dataTable("select Section,class,rollnumber,Session_id,Class_id,Transfer_Status,hosteltaken,Hostel_id from dbo.[admission_registor] where admissionserialnumber='" + txt_admission_no.Text + "' and session='" + ViewState["session"].ToString() + "'", con);

            #region update type wise fee collection
            // fine calculation has been zero
            submit_transection_in_typewise(txt_admission_no.Text, ViewState["session"].ToString(), 0, txt_payment_date.Text, My.DateConvertToIdate(txt_payment_date.Text).ToString(), My.toDouble(total_paid), slip_no, entry_id, sdt.Rows[0]["class"].ToString(), sdt.Rows[0]["Section"].ToString(), sdt.Rows[0]["Class_id"].ToString(), sdt.Rows[0]["hosteltaken"].ToString(), sdt.Rows[0]["Hostel_id"].ToString(), "", app_payment_type, con);
            #endregion
        }


        private void submit_transection_in_typewise(string adno, string session, double fine, string date, string idate, double paid_amount, string slip_no, string entry_id, string classs, string sction, string class_id, string hostel_taken, string hostel_id, string app_transection_id, string app_payment_type, SqlConnection con)
        {
            #region update dues amount in typewise fee collection
            string parameter = "", month = "", late_fine_month = "", month_position = "";
            string qry = " select *  from Typewise_fee_collection  where admission_no='" + adno + "' and session='" + session + "' and status='Dues' and parameter like '%" + ViewState["parameter"].ToString() + "%' order by cast(Position as float)";
            SqlDataAdapter ad = new SqlDataAdapter(qry, con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Typewise_fee_collection");
            DataTable tdt = ds.Tables[0];
            if (tdt.Rows.Count == 0)
            {
            }
            else
            {
                late_fine_month = tdt.Rows[0]["month"].ToString();
                month_position = tdt.Rows[0]["position"].ToString();
                string prev_month = "";
                foreach (DataRow dr in tdt.Rows)
                {
                    if (paid_amount >= 0)
                    {
                        month = dr["month"].ToString();
                        parameter = dr["parameter"].ToString();
                        string group_id = parameter.Contains("Monthly") ? "3" : parameter.Contains("Annual") ? "2" : "1";

                        double dues = My.toDouble(dr["payable"]) - My.toDouble(dr["paid"]) - My.toDouble(dr["Disc"]);
                        dr["Date"] = date;
                        dr["idate"] = idate;
                        if (paid_amount >= dues) // && paid_amount > 0
                        {
                            string prevpaid = My.toDouble(dr["paid"].ToString()).ToString("0.00");
                            paid_amount = paid_amount - dues;
                            //paid amt is gratter than dues so dues amt is actual paid.
                            string paid = dr["dues"].ToString();
                            dr["Payable_after_disc"] = My.toDouble(My.toDouble(dr["payable"]) - My.toDouble(dr["Disc"])).ToString("0.00");
                            dr["paid"] = My.toDouble(dr["Payable_after_disc"]).ToString("0.00");
                            dr["dues"] = "0";
                            dr["status"] = "Paid";
                            #region send in collection slip
                            send_data_in_fee_collection_slip(dr["payable"].ToString(), dues.ToString(), dr["parameter"], dr["feetype"], dr["content_id"], slip_no, entry_id, dr["Ledger"], adno, session, class_id, sction, dr["Disc"].ToString(), dr["month"], dr["position"], prevpaid, con);
                            #endregion
                        }
                        else
                        {
                            if (paid_amount >= 0 || (prev_month != "" && prev_month == month))
                            {
                                string prevpaid = dr["paid"].ToString();
                                dr["Payable_after_disc"] = My.toDouble(My.toDouble(dr["payable"]) - My.toDouble(dr["Disc"])).ToString("0.00");
                                dr["paid"] = My.toDouble(My.toDouble(dr["paid"]) + paid_amount).ToString("0.00");
                                dr["dues"] = My.toDouble(My.toDouble(dr["Payable_after_disc"]) - My.toDouble(dr["paid"])).ToString("0.00");
                                if (My.toDouble(dr["dues"]) <= 0)
                                {
                                    dr["status"] = "Paid";
                                }
                                else
                                {
                                    dr["status"] = "Dues";
                                }

                                #region send in collection slip
                                send_data_in_fee_collection_slip(dr["payable"].ToString(), paid_amount.ToString(), dr["parameter"], dr["feetype"], dr["content_id"], slip_no, entry_id, dr["Ledger"], adno, session, class_id, sction, dr["Disc"].ToString(), dr["month"], dr["position"], prevpaid, con);
                                #endregion 
                                paid_amount = 0;
                            }
                            else
                            {
                                break;
                            }
                        }
                        dr["transection"] = slip_no;
                        dr["is_readyfor_sync"] = true;
                        dr["is_sync"] = false;
                        dr["group_id"] = group_id;
                        dr["class_id"] = class_id;
                        prev_month = month;
                    }
                    else
                    {
                        break;
                    }
                }
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(tdt);
            }


            //#region submit fine amount in type wise collection
            //if (fine > 0)
            //{
            //    double paid = fine;
            //    double dues = 0;
            //    string pstatus = "Paid";
            //    My.exeSql("insert into Typewise_fee_collection(admission_no,class,session,section,parameter,Date,idate,feetype,payable,paid,dues,status,month,content_id,transection,Ledger,group_id,class_id,position,user_id,branchid) values ('" + adno + "','" + classs + "','" + session + "','" + sction + "','" + parameter + "','" + date + "',N'" + idate + "','Late Fine','" + fine + "','" + paid + "','" + dues + "','" + pstatus + "','" + late_fine_month + "','6121','" + slip_no + "','School','" + ViewState["group_id"].ToString() + "','" + class_id + "','" + month_position + "','" + ViewState["Userid"].ToString() + "','" + ViewState["firm_id"].ToString() + "');insert into Monthly_Fee_Collection_Slip(adno,session,class,Section,parameter,Content,content_id,payable,paid,slipno,Ledger,Month,disc_amt,previously_paid,month_position,Date,Idate) values ('" + adno + "','" + session + "','" + class_id + "','" + sction + "','" + parameter + "','Late Fine','6121','" + fine + "','" + paid + "','" + slip_no + "','School','" + late_fine_month + "','0','0','" + month_position + "','" + txt_payment_date.Text + "','" + My.DateConvertToIdate(txt_payment_date.Text) + "'); insert into Fine_Fees_collection(Admission_no,Session_id,Date,idate,Description,Slip_no,Amount,Total_no_of_fine_day_month,Fine_date_from,Fine_date_to,Branch_id,User_id,Class_id,Fine_type) values ('" + txt_admission_no.Text + "','" + My.get_session_id() + "','" + mycode.date() + "','" + mycode.idate() + "','Late Fine Fees','" + slip_no + "','" + txtfineamount.Text + "','" + ViewState["late_fine_no_of_day_month"].ToString() + "','" + ViewState["fine_date_From"].ToString() + "','" + ViewState["fine_date_To"].ToString() + "','" + ViewState["firm_id"].ToString() + "','" + ViewState["Userid"].ToString() + "','" + ViewState["classid"].ToString() + "','" + ViewState["FineType"].ToString() + "')");
            //}
            //#endregion
            #endregion



            if (fine > 0)
            {
                payments.exeSql("insert into Fine_Fees_collection(Admission_no,Session_id,Date,idate,Description,Slip_no,Amount,Total_no_of_fine_day_month,Fine_date_from,Fine_date_to,Branch_id,User_id,Class_id,Fine_type) values ('" + adno + "','" + ViewState["sessionIDs"].ToString() + "','" + mycode.date() + "','" + mycode.idate() + "','Late Fine Fees','" + slip_no + "','" + My.toDouble(fine).ToString("0.00") + "','" + ViewState["late_fine_no_of_day_month"].ToString() + "','" + ViewState["fine_date_From"].ToString() + "','" + ViewState["fine_date_To"].ToString() + "','" + ViewState["firm_id"].ToString() + "','" + ViewState["Userid"].ToString() + "','" + ViewState["classid"].ToString() + "','" + ViewState["FineType"].ToString() + "')", con);
            }
        }

        private void update_School_ledger(string slip_no, string entry_id, string paid, SqlConnection con)
        {
            string total_payment = "0";
            string Names = "WAHAB";
            SqlDataAdapter ad_contactus = new SqlDataAdapter("select top 1 * from SchoolLedger ", con);
            DataSet ds = new DataSet();
            ad_contactus.Fill(ds, "SchoolLedger");
            DataTable dt = ds.Tables[0];
            DataRow dr = dt.NewRow();
            dr[0] = "Month Wise Fee Collection";
            dr[1] = "Monthly  Fee  for  " + Names + "  Adm.No:-" + txt_admission_no.Text + "Total Bill:- " + total_payment + " , Paid Amount :-" + paid.ToString() + " ,  Discount Given:-" + txt_discount.Text + " Dues:-" + txt_total_dues.Text + " Slip No:-" + slip_no;
            dr[2] = My.toDouble(paid).ToString("0.00");
            dr[3] = "0";
            dr[4] = My.toDateTime(txt_payment_date.Text).ToString("yyyyMMdd");
            dr[5] = txt_payment_date.Text;
            dr[6] = slip_no;
            dr["entry_id"] = entry_id;
            dr["session"] = ViewState["session"].ToString();
            dr["Ledger_Type"] = "School";
            dr["time"] = mycode.time();
            dr["user_id"] = ViewState["Userid"].ToString();
            dr["Addmission_no"] = txt_admission_no.Text;
            dr["branchid"] = ViewState["Branchid"].ToString();
            dt.Rows.Add(dr);
            SqlCommandBuilder cb = new SqlCommandBuilder(ad_contactus);
            ad_contactus.Update(dt);
        }


        private void calculate_dues_for_new_month(string slipno, SqlConnection con, double monthlYPaid)
        {
            DataTable dtM = payments.dataTable("select * from Month_Index order by Position asc", con);
            if (dtM.Rows.Count > 0)
            {
                DataTable fdt = new DataTable();
                foreach (DataRow drM in dtM.Rows)
                {
                    if (monthlYPaid > 0)
                    {
                        string months = drM["Month"].ToString();
                        if (ViewState["IsBoarding"].ToString() == "1")
                        {
                            int mnthids = My.tomonth_number(months);
                            if (My.toint(ViewState["LunchMnthId"].ToString()) <= mnthids)
                            {
                                ViewState["parameteridS"] = "44";
                            }
                            else
                            {
                                ViewState["parameteridS"] = "4";
                            }
                        }

                        if (payments.dataTable("select  * from dbo.[Typewise_fee_collection] where admission_no='" + txt_admission_no.Text + "' and session='" + ViewState["session"].ToString() + "' and month='" + months + "' and parameter='" + ViewState["parameter"].ToString() + "' and transection!=''", con).Rows.Count > 0)
                        {
                            continue;
                        }
                        else
                        {
                            Dictionary<string, object> dc = new Dictionary<string, object>();
                            dc["admission_no"] = txt_admission_no.Text;
                            dc["session_id"] = ViewState["sessionIDs"].ToString();
                            dc["class"] = lbl_class.Text;
                            dc["session"] = ViewState["session"].ToString();
                            dc["class_id"] = ViewState["classid"].ToString();
                            dc["hosteltaken"] = lblhostel.Text;
                            dc["months"] = months;
                            dc["tr_ledger"] = My.is_combine ? "School" : "Transport";
                            dc["hostel_id"] = ViewState["Hostel_id"].ToString();
                            dc["Room_Category_id"] = ViewState["Room_Category_id"].ToString();
                            dc["Hostel_assig_id"] = ViewState["Hostel_assign_id"].ToString();
                            dc["day_boarding"] = ViewState["day_bording"].ToString();
                            dc["day_boarding_lunch"] = ViewState["day_bording_with_lunch"].ToString();

                            dc["category_id"] = ViewState["category_id"].ToString();
                            dc["sub_category_id"] = ViewState["sub_category_id"].ToString();
                            dc["TransportationPath_id"] = ViewState["TransportPath_id"].ToString();
                            dc["transportportation_id"] = ViewState["Transport_id"].ToString();
                            dc["Boarding_Point_id"] = ViewState["Boarding_Point_id"].ToString();

                            dc["parameter_id"] = ViewState["parameteridS"].ToString();
                            //new08/08/2022

                            string cunrt_session = ViewState["session"].ToString();
                            string session_frst_year = cunrt_session.Substring(0, 4);
                            int session_s_year = My.toint(session_frst_year);
                            int s_year = My.toint(session_frst_year);
                            string monthid = My.tomonth_numberstring(months);
                            int pay_month = My.toint(monthid);
                            s_year = payments.check_start_months(pay_month, s_year, con);
                            dc["monthid"] = s_year + monthid;
                            DataTable feedt = payments.dataTableSP("sp_fetch_monthly_fee", dc, con);
                            send_in_typewise_fee(feedt, months, slipno, con);



                            //=======================================
                            feedt.Columns.Add("total_payable");
                            string month = "";
                            double total = 0, fee = 0, disc = 0, paid_prev = 0;
                            foreach (DataRow dr in feedt.Rows)
                            {
                                month = dr["months"].ToString();
                                dr["total_payable"] = My.toDouble(dr["amount"]) - My.toDouble(dr["disc_amount"]) - My.toDouble(dr["previously_paid"]);
                                fee += My.toDouble(dr["amount"]);
                                disc += My.toDouble(dr["disc_amount"]);
                                paid_prev += My.toDouble(dr["previously_paid"]);
                                total += My.toDouble(dr["total_payable"]);
                                monthlYPaid = (monthlYPaid - total);
                            }

                            foreach (DataRow dr in feedt.Rows)
                            {
                                try
                                {
                                    fdt.Rows.Add(dr.ItemArray);
                                }
                                catch
                                {
                                    foreach (DataColumn dcc in feedt.Columns)
                                    {
                                        fdt.Columns.Add(dcc.ColumnName);
                                    }
                                    fdt.Rows.Add(dr.ItemArray);
                                }
                            }
                        }
                    }
                }

                if (fdt.Rows.Count.ToString() != "0")
                {
                    bind_ttl_fee(fdt);
                }
                else
                {
                    lbl_fee_amount.Text = "0";
                    lbl_discount.Text = "0";
                    lbl_paid_prev.Text = "0";
                    lbl_total.Text = "0";
                    txttotal.Text = "0";
                    txt_paid_prev.Text = "0";
                    txt_discount.Text = "0";
                }
            }
        }

        private void bind_ttl_fee(DataTable fdt)
        {
            int i;
            double totalAmt = 0; double totaldisc = 0; double totalPrepAid = 0; double totalpblE = 0;
            for (i = 0; i < fdt.Rows.Count; i++)
            {
                if (fdt.Rows[i]["amount"].ToString() != "")
                {
                    totalAmt = totalAmt + Convert.ToDouble(fdt.Rows[i]["amount"].ToString());
                }
                if (fdt.Rows[i]["disc_amount"].ToString() != "")
                {
                    totaldisc = totaldisc + Convert.ToDouble(fdt.Rows[i]["disc_amount"].ToString());
                }
                if (fdt.Rows[i]["previously_paid"].ToString() != "")
                {
                    totalPrepAid = totalPrepAid + Convert.ToDouble(fdt.Rows[i]["previously_paid"].ToString());
                }
                if (fdt.Rows[i]["total_payable"].ToString() != "")
                {
                    totalpblE = totalpblE + Convert.ToDouble(fdt.Rows[i]["total_payable"].ToString());
                }
            }
            lbl_fee_amount.Text = totalAmt.ToString();
            lbl_discount.Text = totaldisc.ToString();
            lbl_paid_prev.Text = totalPrepAid.ToString();
            lbl_total.Text = totalpblE.ToString();
            txttotal.Text = totalAmt.ToString();
            txt_paid_prev.Text = totalPrepAid.ToString();
            txt_discount.Text = totaldisc.ToString();
            txt_monthlyFee.Text = totalpblE.ToString();


            if (0 >= My.toDouble(txt_monthlyFee.Text))
            {
                txt_paid_amount.Text = "0";
                txt_total_dues.Text = "0";
            }
        }

        private void send_in_typewise_fee(DataTable feedt, string month_name, string slipno, SqlConnection con)
        {
            foreach (DataRow dr in feedt.Rows)
            {
                string parm = "";
                if (dr["parameter"].ToString() == "AdditionalServices")
                {
                    parm = "HostelMonthlyFee";
                }
                else if (dr["parameter"].ToString() == "DamageCharges")
                {
                    parm = "HostelMonthlyFee";
                }
                else
                {
                    parm = dr["parameter"].ToString();
                }
                payments.exeSql("insert into Typewise_fee_collection(admission_no,class,session,parameter,Date,idate,feetype,payable,paid,dues,status,month,content_id,transection,Ledger,is_readyfor_sync,is_sync,is_sync_dues_diary,Disc,section,user_id,branchid,parameter2) values ('" + dr["admission_no"].ToString() + "','" + dr["class"].ToString() + "','" + dr["session"].ToString() + "','" + parm + "','" + mycode.date() + "','" + mycode.idate() + "','" + dr["content"].ToString() + "','" + My.toDouble(dr["amount"].ToString()).ToString("0.00") + "','0','" + My.toDouble(dr["amount"].ToString()).ToString("0.00") + "','Dues','" + dr["months"].ToString() + "','" + dr["content_id"].ToString() + "','" + slipno + "','School','false','false','false','" + My.toDouble(dr["disc_amount"].ToString()).ToString("0.00") + "','" + ViewState["Section"].ToString() + "','" + ViewState["Userid"].ToString() + "','" + ViewState["Branchid"].ToString() + "','" + dr["parameter"].ToString() + "')", con);
            }
        }

        #region MakeAdmissionFEE
        private void make_admission_fee(SqlConnection con, double PaidAmt)
        {
            string parameter = "";
            string parameter_id = "";
            if (ViewState["Transfer_Status"].ToString() == "New")
            {
                parameter = ViewState["hostaltaken"].ToString().ToUpper() == "NO" ? "AdmissionFee" : "HostelAdmissionFee";
                parameter_id = ViewState["hostaltaken"].ToString().ToUpper() == "NO" ? "1" : "5";
            }
            else
            {
                parameter = ViewState["hostaltaken"].ToString().ToUpper() == "NO" ? "AnnualFee" : "HostelAnnualFee";
                parameter_id = ViewState["hostaltaken"].ToString().ToUpper() == "NO" ? "2" : "6";
            }
            string qry = "select *,(payable-cast(disc_amount as float)-cast(paid as float)) net_payable from(select '0' payable_after_disc,session,feetype,cast(payable as float) payable,paid,dues,status,content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + ViewState["Admission_no"].ToString() + "'  and parameter_id='" + parameter_id + "' and session='" + ViewState["session"].ToString() + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and parameter_id='" + parameter_id + "' and session='" + ViewState["session"].ToString() + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'))),0) disc_amount from dbo.[Typewise_fee_collection] WHERE admission_no='" + ViewState["Admission_no"].ToString() + "' and parameter='" + parameter + "' and session='" + ViewState["session"].ToString() + "')t";
            DataTable fee_dt = payments.dataTable(qry, con);
            if (fee_dt.Rows.Count == 0)
            {
                if (ViewState["Hostel_id"].ToString() == "0")
                {
                    qry = "select *,(payable-cast(disc_amount as float)) net_payable from(select '0' payable_after_disc,fmc.session,cm.content feetype,cast(fmc.amount as float) payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where    admission_no='" + ViewState["Admission_no"].ToString() + "'  and parameter_id='" + parameter_id + "' and session='" + ViewState["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where  Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and parameter_id='" + parameter_id + "' and session='" + ViewState["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'))),0) disc_amount   from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='" + parameter + "'  and session='" + ViewState["session"].ToString() + "' and class_id='" + ViewState["classid"].ToString() + "' ) t  UNION ALL select '0' as payable_after_disc, session as Session,Perticular as feetype,Amount as payable,  '0' as paid, Amount as dues,'Dues' as status,'ADM01' as content_id,'0' as disc_amount,Amount as net_payable from Misc_Fee_Master_Studentwise where (Type_Mode='AdmissionFee' or Type_Mode='AnnualFee') and session='" + ViewState["session"].ToString() + "' and Admission_No='" + ViewState["Admission_no"].ToString() + "'";
                }
                else
                {
                    qry = "select *,(payable-cast(disc_amount as float)) net_payable from(select '0' payable_after_disc,fmc.session,cm.content feetype,cast(fmc.amount as float) payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where   Hostel_Id=" + ViewState["Hostel_id"].ToString() + " and admission_no='" + ViewState["Admission_no"].ToString() + "'  and parameter_id='" + parameter_id + "' and session='" + ViewState["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Hostel_Id=" + ViewState["Hostel_id"].ToString() + " and Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and parameter_id='" + parameter_id + "' and session='" + ViewState["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'))),0) disc_amount   from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='" + parameter + "'  and session='" + ViewState["session"].ToString() + "' and class_id='" + ViewState["classid"].ToString() + "' and fmc.Hostel_Id='" + ViewState["Hostel_id"].ToString() + "') t  UNION ALL select '0' as payable_after_disc, session as Session,Perticular as feetype,Amount as payable,  '0' as paid, Amount as dues,'Dues' as status,'ADM01' as content_id,'0' as disc_amount,Amount as net_payable from Misc_Fee_Master_Studentwise where (Type_Mode='AdmissionFee' or Type_Mode='AnnualFee') and session='" + ViewState["session"].ToString() + "' and Admission_No='" + ViewState["Admission_no"].ToString() + "'";
                }
                fee_dt = payments.dataTable(qry, con);
            }

            DataTable dt = payments.dataTable(qry, con);
            if (dt.Rows.Count > 0)
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

                hd_paybaleamount.Value = payble_after_disc.ToString("0.00");
                hd_adjustamount.Value = payble_after_disc.ToString("0.00");
                hd_totalamount.Value = payable.ToString("0.00");
                hd_total_discount.Value = disc.ToString("0.00");


                string slip_no = "";
                string ad_no = ViewState["Admission_no"].ToString();
                string entry_id = "AD" + cretesessionid(con);
                string type = "";
                if (ViewState["Transfer_Status"].ToString() == "New")
                {
                    type = "Admission";
                    slip_no = payments.invoice_format_admssion("slip_no", con);
                }
                else
                {
                    type = "Annual";
                    slip_no = payments.invoice_readmission("slip_no", con);
                }
                ViewState["yearlYSLipNo"] = slip_no;
                admission_payment(slip_no, entry_id, ad_no, con, PaidAmt, type);
                //payments.send_data_to_user_log_history(ViewState["Userid"].ToString(), ViewState["Userid"].ToString() + " received " + type + " fee :-" + PaidAmt.ToString() + " Adjust Amount" + hd_total_discount.Value + " rs,Slip No :- " + slip_no + " from " + lbl_studentname.Text + ", Admission No :-" + ad_no, con);
            }
        }


        payments pays = new payments();
        private string cretesessionid(SqlConnection con)
        {
            bool duplicate = false;
            string Slip_no = pays.auto_serial("admfee_id", con);
            while (!duplicate)
            {
                DataTable cdt = payments.dataTable("select Slip_no from dbo.[Student_Payment_History] where Slip_no='" + Slip_no + "'", con);
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    duplicate = true;
                }
                else
                {
                    duplicate = false;
                    Slip_no = pays.auto_serial("admfee_id", con);
                }
            }
            return Slip_no;
        }

        private void admission_payment(string slip_no, string entry_id, string ad_no, SqlConnection con, double PaidAmt, string type)
        {
            string student_name = ""; string payment_date = mycode.date();
            string Uid = slip_no;
            string Tag = PaidAmt.ToString();
            string parameter = "";
            if (type == "Admission")
            {
                parameter = ViewState["hostaltaken"].ToString().ToUpper() == "NO" ? "AdmissionFee" : "HostelAdmissionFee";
            }
            else
            {
                parameter = ViewState["hostaltaken"].ToString().ToUpper() == "NO" ? "AnnualFee" : "HostelAnnualFee";
            }

            send_data_in_student_payment_history_admission(type, slip_no, entry_id, ad_no, parameter, con, PaidAmt);
            send_data_to_school_ledger_admission(slip_no, entry_id, con, PaidAmt, type, student_name, payment_date);
            create_admission_annual_dues_admission(parameter, con, PaidAmt, type, student_name, payment_date);
            send_data_in_feetypewise_collection_admission(slip_no, entry_id, parameter, con, PaidAmt, type, student_name, payment_date);
            if (type == "Admission")
            {
                send_data_to_admission_fee_collection_admission(slip_no, entry_id, parameter, type, student_name, payment_date, con);
            }
            else
            {
                send_data_to_annual_fee_collection(slip_no, entry_id, parameter, type, student_name, payment_date, con);
            }

            // update_data_to_admission_registor(con);
        }
        //private void update_data_to_admission_registor(SqlConnection con)
        //{
        //    SqlDataAdapter ad = new SqlDataAdapter("select * from admission_registor where admissionserialnumber = '" + ViewState["Admission_no"].ToString() + "' and session='" + ViewState["session"].ToString() + "' and Class_id='" + ViewState["classid"].ToString() + "'", con);
        //    DataSet ds = new DataSet();
        //    ad.Fill(ds);
        //    DataTable dt = ds.Tables[0];
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        if (My.toDouble(lbl_totaldues.Text) <= 0)
        //        {
        //            dr["payment_status"] = "Paid";
        //        }
        //        else if (My.toDouble(lbl_totaldues.Text) > 0)
        //        {
        //            dr["payment_status"] = "Dues";
        //        }
        //    }
        //    SqlCommandBuilder cb = new SqlCommandBuilder(ad);
        //    ad.Update(dt);
        //}
        private void send_data_to_annual_fee_collection(string slip_no, string entry_id, string parameter, string type, string student_name, string payment_date, SqlConnection con)
        {
            DataTable pdt = payments.dataTable("select sum(cast(paid as float)) paid,sum(cast(dues as float)) dues from dbo.[Typewise_fee_collection] where admission_no='" + ViewState["Admission_no"].ToString() + "' and session='" + ViewState["session"].ToString() + "' and  parameter like '%'" + parameter + "'%' and feetype!='Previous Dues'", con);
            //.ToString("0.00") 
            SqlDataAdapter ad = new SqlDataAdapter("select * from Annual_fee_collection where Admission_no='" + ViewState["Admission_no"].ToString() + "' and session='" + ViewState["session"].ToString() + "'", con);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                DataRow dr = dt.NewRow();
                dr[1] = ViewState["Admission_no"].ToString();
                dr[2] = My.toDouble(hd_totalamount.Value).ToString("0.00");
                dr[3] = "0";
                dr[4] = My.toDouble(payment_date).ToString("0.00");
                dr[5] = My.toDouble(pdt.Rows[0]["paid"].ToString()).ToString("0.00");
                dr[6] = My.toDouble(My.toDouble(dr[4]) - My.toDouble(dr[5])).ToString("0.00");
                dr[7] = payment_date;
                dr[8] = "Cash";
                dr[9] = slip_no;
                dr["session"] = ViewState["session"].ToString(); ;
                dr["idate"] = Convert.ToDateTime(payment_date).ToString("yyyyMMdd");
                dr["remark"] = "";
                dr["time"] = mycode.time();
                dr["user_id"] = ViewState["Userid"].ToString(); ;
                dr["Acamedic_Semester_Id"] = "0";
                dr["branchid"] = "1";
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
        private void send_data_to_admission_fee_collection_admission(string slip_no, string entry_id, string parameter, string type, string student_name, string payment_date, SqlConnection con)
        {
            DataTable pdt = payments.dataTable("select sum(cast(paid as float)) paid,sum(cast(dues as float)) dues from dbo.[Typewise_fee_collection] where admission_no='" + ViewState["Admission_no"].ToString() + "' and session='" + ViewState["session"].ToString() + "' and  parameter like '%" + parameter + "%' and feetype!='Previous Dues'", con);
            SqlDataAdapter ad = new SqlDataAdapter("select * from Admission_fee_collection where Admission_no='" + ViewState["Admission_no"].ToString() + "' and session='" + ViewState["session"].ToString() + "'", con);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                DataRow dr = dt.NewRow();
                dr[1] = ViewState["Admission_no"].ToString();
                dr[2] = My.toDouble(hd_totalamount.Value).ToString("0.00");
                dr[3] = My.toDouble(hd_total_discount.Value).ToString("0.00");
                dr[4] = My.toDouble(hd_paybaleamount.Value).ToString("0.00");
                dr[5] = My.toDouble(pdt.Rows[0]["paid"].ToString()).ToString("0.00");
                dr[6] = My.toDouble(My.toDouble(dr[4]) - My.toDouble(dr[5])).ToString("0.00");
                dr[7] = payment_date;
                dr[8] = "Cash";
                dr[9] = slip_no;
                dr["session"] = ViewState["session"].ToString();
                dr["idate"] = Convert.ToDateTime(payment_date).ToString("yyyyMMdd");
                dr["remark"] = "";
                dr["entry_id"] = entry_id;
                dr["time"] = mycode.time();
                dr["user_id"] = "1";
                dr["Slip_no"] = slip_no;
                dr["Acamedic_Semester_Id"] = "0";
                dr["branchid"] = "1";
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



        private void send_data_in_feetypewise_collection_admission(string slip_no, string entry_id, string parameter, SqlConnection con, double PaidAmt, string type, string student_name, string payment_date)
        {
            string class_id = ViewState["classid"].ToString();
            double paid_amount = PaidAmt;
            SqlDataAdapter ad = new SqlDataAdapter(" select * from Typewise_fee_collection  where admission_no='" + ViewState["Admission_no"].ToString() + "' and session='" + ViewState["session"].ToString() + "' and parameter='" + parameter + "'", con);
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
                        dr["Date"] = payment_date;
                        dr["idate"] = My.toDateTime(payment_date).ToString("yyyyMMdd");
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
                            send_data_in_fee_collection_slip_adm(My.toDouble(dr["payable"].ToString()).ToString("0.00"), dues.ToString("0.00"), dr["parameter"], dr["feetype"], dr["content_id"], slip_no, entry_id, dr["Ledger"], ViewState["Admission_no"].ToString(), ViewState["session"].ToString(), ViewState["classid"].ToString(), ViewState["Section"].ToString(), My.toDouble(dr["Disc"].ToString()).ToString("0.00"), dr["month"], dr["position"], prevpaid, type, student_name, payment_date, con);
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
                            send_data_in_fee_collection_slip_adm(My.toDouble(dr["payable"].ToString()).ToString("0.00"), paid_amount.ToString("0.00"), dr["parameter"], dr["feetype"], dr["content_id"], slip_no, entry_id, dr["Ledger"], lbl_admissionno.Text, ViewState["session"].ToString(), ViewState["classid"].ToString(), ViewState["Section"].ToString(), My.toDouble(dr["Disc"].ToString()).ToString("0.00"), dr["month"], dr["position"], prevpaid, type, student_name, payment_date, con);
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

        private void send_data_in_fee_collection_slip_adm(string payable, string paid, object parameter, object content, object content_id, string slip_no, string entry_id, object ledger, string adno, string session, string classs, string sction, string disc_on_feehead, object month, object monthid, string prevpaid, string type, string student_name, string payment_date, SqlConnection con)
        {
            if (ledger == "")
            {
                ledger = "School";
            }
            string qry = "insert into Monthly_Fee_Collection_Slip(adno,session,class,Section,parameter,Content,content_id,payable,paid,slipno,Ledger,disc_amt,Month,month_position,previously_paid,branchid,academicyear,Date,Idate,Hostel_id) values ('" + adno + "','" + session + "','" + classs + "','" + sction + "','" + parameter + "','" + content + "','" + content_id + "','" + payable + "','" + paid + "','" + slip_no + "','" + ledger + "','" + disc_on_feehead + "','" + month + "','" + monthid + "','" + prevpaid + "','1','0','" + payment_date + "','" + My.DateConvertToIdate(payment_date) + "','" + ViewState["Hostel_id"].ToString() + "');";
            payments.exeSql(qry, con);
        }


        private void create_admission_annual_dues_admission(string parameter, SqlConnection con, double PaidAmt, string type, string student_name, string payment_date)
        {
            if (payments.dataTable("select session,feetype,payable,paid,dues,status,content_id from dbo.[Typewise_fee_collection] WHERE admission_no='" + ViewState["Admission_no"].ToString() + "' and parameter='" + parameter + "' and session='" + ViewState["session"].ToString() + "'", con).Rows.Count == 0)
            {
                string query = "select '0' payable_after_disc,fmc.session,cm.content feetype,fmc.amount payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, (isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + ViewState["Admission_no"].ToString() + "'  and (parameter_id='1' or parameter_id='5') and session='" + ViewState["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and (parameter_id='1' or parameter_id='5') and session='" + ViewState["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'))) disc_amount  from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='" + parameter + "' and session='" + ViewState["session"].ToString() + "' and class_id='" + ViewState["classid"].ToString() + "' UNION ALL select '0' as payable_after_disc, session as Session,Perticular as feetype,Amount as payable,  '0' as paid, Amount as dues,'Dues' as status,'ADM01' as content_id,'0' as disc_amount from Misc_Fee_Master_Studentwise where (Type_Mode='AdmissionFee' or Type_Mode='AnnualFee') and session='" + ViewState["session"].ToString() + "' and Admission_No='" + ViewState["Admission_no"].ToString() + "'";
                DataTable dt = payments.dataTable(query, con);
                if (dt.Rows.Count == 0)
                {
                }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        payments.exeSql("insert into Typewise_fee_collection(admission_no,class,session,section,parameter,Date,idate,feetype,payable,paid,dues,status,month,content_id,transection,Ledger,is_readyfor_sync,is_sync,is_sync_dues_diary,Disc,Payable_after_disc,branchid,Acamedic_Semester_Id,class_id,user_id) values ('" + ViewState["Admission_no"].ToString() + "','" + ViewState["class_name"].ToString() + "','" + dr["session"].ToString() + "','" + ViewState["Section"].ToString() + "','" + parameter + "','" + mycode.date() + "','" + mycode.idate() + "','" + dr["feetype"].ToString() + "','" + My.toDouble(dr["payable"].ToString()).ToString("0.00") + "','0','" + My.toDouble(dr["payable"].ToString()).ToString("0.00") + "','Dues','" + payments.get_start_month(con) + "','" + dr["content_id"].ToString() + "','','School','false','false','false','" + My.toDouble(dr["disc_amount"].ToString()).ToString("0.00") + "','" + My.toDouble(dr["payable_after_disc"].ToString()).ToString("0.00") + "','1','0','" + ViewState["class_id"].ToString() + "','1')", con);
                    }
                }
            }
        }


        private void send_data_to_school_ledger_admission(string transcation, string entry_id, SqlConnection con, double PaidAmt, string type, string student_name, string payment_date)
        {
            SqlDataAdapter ad = new SqlDataAdapter("select * from SchoolLedger ", con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "SchoolLedger");
            DataTable dt = ds.Tables[0];
            DataRow dr = dt.NewRow();
            dr["Particulars"] = type + " Fee Collection";
            dr["Discription"] = type + " fee for " + student_name + " Adm.No:-" + ViewState["Admission_no"].ToString() + " Total Bill :-" + PaidAmt.ToString() + " Paid Amount :-  " + PaidAmt.ToString();
            dr["AllInput"] = PaidAmt.ToString("0.00");
            dr["AllOutput"] = "0";
            dr["IDate"] = Convert.ToDateTime(payment_date).ToString("yyyyMMdd");
            dr["Date"] = payment_date;
            dr["TransactionId"] = transcation;
            dr["entry_id"] = entry_id;
            dr["session"] = ViewState["session"].ToString();
            dr["Ledger_Type"] = "School";
            dr["time"] = mycode.time();
            dr["user_id"] = ViewState["Userid"].ToString();
            dr["Addmission_no"] = ViewState["Admission_no"].ToString();
            dr["branchid"] = ViewState["branchid"].ToString();
            dt.Rows.Add(dr);
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
        }


        private void send_data_in_student_payment_history_admission(string type, string slip_no, string entry_id, string ad_no, string parameter_New, SqlConnection con, double PaidAmt)
        {
            string payment_date = mycode.date(); string name = ""; string payment_mode = "Cash";
            SqlCommand cmd;
            string query = "insert into Student_Payment_History(Addmission_no,Session,Date,Idate,Description,Entry_id,Slip_no,Amount,Type,mode,discount,Discoun_in_School,Discoun_in_Hostel,Discoun_in_Transport,fine,is_ofline_sync,Is_online_sync,is_update_in_online,Previous_admission_no,App_Transection_id,time,user_id,Acamedic_Semester_Id,Branch,Class_id,User_Slip_no,Pay_mode_transaction_no,Remarks,Transection_in,parameter_New,Bank_name,Bank_date) values (@Addmission_no,@Session,@Date,@Idate,@Description,@Entry_id,@Slip_no,@Amount,@Type,@mode,@discount,@Discoun_in_School,@Discoun_in_Hostel,@Discoun_in_Transport,@fine,@is_ofline_sync,@Is_online_sync,@is_update_in_online,@Previous_admission_no,@App_Transection_id,@time,@user_id,@Acamedic_Semester_Id,@Branch,@Class_id,@User_Slip_no,@Pay_mode_transaction_no,@Remarks,@Transection_in,@parameter_New,@Bank_name,@Bank_date)";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Addmission_no", ad_no);
            cmd.Parameters.AddWithValue("@Session", ViewState["session"].ToString());
            cmd.Parameters.AddWithValue("@Date", payment_date);
            cmd.Parameters.AddWithValue("@Idate", Convert.ToDateTime(payment_date).ToString("yyyyMMdd"));
            cmd.Parameters.AddWithValue("@Description", type + " fee collection for " + name + " Paid Amount : " + PaidAmt.ToString() + " /-");
            cmd.Parameters.AddWithValue("@Entry_id", entry_id);
            cmd.Parameters.AddWithValue("@Slip_no", slip_no);
            cmd.Parameters.AddWithValue("@Amount", PaidAmt.ToString("0.00"));
            cmd.Parameters.AddWithValue("@Type", type);
            cmd.Parameters.AddWithValue("@mode", payment_mode);
            cmd.Parameters.AddWithValue("@discount", My.toDouble(hd_total_discount.Value).ToString("0.00"));
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
            cmd.Parameters.AddWithValue("@Branch", ViewState["branchid"].ToString());
            cmd.Parameters.AddWithValue("@Class_id", ViewState["classid"].ToString());
            cmd.Parameters.AddWithValue("@Remarks", "");
            cmd.Parameters.AddWithValue("@User_Slip_no", "");
            cmd.Parameters.AddWithValue("@Pay_mode_transaction_no", "");
            cmd.Parameters.AddWithValue("@Transection_in", "Software");
            cmd.Parameters.AddWithValue("@parameter_New", parameter_New);
            cmd.Parameters.AddWithValue("@Bank_name", "");
            cmd.Parameters.AddWithValue("@Bank_date", "");
            if (payments.InsertUpdateData(cmd, con))
            {
                // money recpit
                SqlDataAdapter ad = new SqlDataAdapter("select * from SchoolLedger ", con);
                DataSet ds = new DataSet();
                ad.Fill(ds, "SchoolLedger");
                DataTable dt = ds.Tables[0];
                DataRow dr = dt.NewRow();
                dr["Particulars"] = type + " Money Receipt";
                dr["Discription"] = type + " Money Receipt " + name + " Adm.No:-" + ViewState["Admission_no"].ToString() + " Paid Amount :-  " + PaidAmt.ToString("0.00");
                dr["AllInput"] = PaidAmt.ToString("0.00");
                dr["AllOutput"] = "0";
                dr["IDate"] = Convert.ToDateTime(payment_date).ToString("yyyyMMdd");
                dr["Date"] = payment_date;
                dr["TransactionId"] = slip_no;
                dr["entry_id"] = entry_id;
                dr["session"] = ViewState["session"].ToString();
                dr["Ledger_Type"] = "School";
                dr["time"] = mycode.time();
                dr["user_id"] = ViewState["Userid"].ToString();
                dr["Addmission_no"] = ViewState["Admission_no"].ToString();
                dr["branchid"] = ViewState["branchid"].ToString();
                dr["Unique_id"] = entry_id;
                dt.Rows.Add(dr);
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(dt);
            }
        }
        #endregion



        #region MakeAnnualFEE
        private void make_annual_fee(SqlConnection con, double PaidAmt)
        {
            string student_name = "WAHAB"; string payment_date = mycode.date();
            string parameter = ViewState["hostaltaken"].ToString().ToUpper() == "NO" ? "AnnualFee" : "HostelAnnualFee";
            string qry = "select *,(payable-cast(disc_amount as float)-cast(paid as float)) net_payable from(select '0' payable_after_disc,session,feetype,cast(payable as float) payable,paid,dues,status,content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + ViewState["Admission_no"].ToString() + "'  and (parameter_id='2' or parameter_id='6') and session='" + ViewState["session"].ToString() + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='Annual' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and (parameter_id='2' or parameter_id='6') and session='" + ViewState["session"].ToString() + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='Annual' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'))),0) disc_amount from dbo.[Typewise_fee_collection] WHERE admission_no='" + ViewState["Admission_no"].ToString() + "' and parameter='" + parameter + "' and session='" + ViewState["session"].ToString() + "')t";
            DataTable fee_dt = payments.dataTable(qry, con);
            if (fee_dt.Rows.Count == 0)
            {
                if (ViewState["Hostel_id"].ToString() == "0")
                {
                    qry = "select *,(payable-cast(disc_amount as float)) net_payable from(select '0' payable_after_disc,fmc.session,cm.content feetype,cast(fmc.amount as float) payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where    admission_no='" + ViewState["Admission_no"].ToString() + "'  and (parameter_id='2' or parameter_id='6') and session='" + ViewState["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='Annual' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where   Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and (parameter_id='2' or parameter_id='6') and session='" + ViewState["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='Annual' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'))),0) disc_amount from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='" + parameter + "' and session='" + ViewState["session"].ToString() + "' and class_id='" + ViewState["classid"].ToString() + "') t  UNION ALL select '0' as payable_after_disc, session as Session,Perticular as feetype,Amount as payable,  '0' as paid, Amount as dues,'Dues' as status,'ADM01' as content_id,'0' as disc_amount,Amount as net_payable from Misc_Fee_Master_Studentwise where (Type_Mode='AdmissionFee' or Type_Mode='AnnualFee') and session='" + ViewState["session"].ToString() + "' and Admission_No='" + ViewState["Admission_no"].ToString() + "'";
                }
                else
                {
                    qry = "select *,(payable-cast(disc_amount as float)) net_payable from(select '0' payable_after_disc,fmc.session,cm.content feetype,cast(fmc.amount as float) payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where  Hostel_id=" + ViewState["Hostel_id"].ToString() + " and admission_no='" + ViewState["Admission_no"].ToString() + "'  and (parameter_id='2' or parameter_id='6') and session='" + ViewState["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='Annual' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Hostel_Id=" + ViewState["Hostel_id"].ToString() + " and Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and (parameter_id='2' or parameter_id='6') and session='" + ViewState["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='Annual' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'))),0) disc_amount from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='" + parameter + "' and session='" + ViewState["session"].ToString() + "' and class_id='" + ViewState["classid"].ToString() + "' and fmc.Hostel_Id='" + ViewState["Hostel_id"].ToString() + "') t  UNION ALL select '0' as payable_after_disc, session as Session,Perticular as feetype,Amount as payable,  '0' as paid, Amount as dues,'Dues' as status,'ADM01' as content_id,'0' as disc_amount,Amount as net_payable from Misc_Fee_Master_Studentwise where (Type_Mode='AdmissionFee' or Type_Mode='AnnualFee') and session='" + ViewState["session"].ToString() + "' and Admission_No='" + ViewState["Admission_no"].ToString() + "'";
                }
                fee_dt = payments.dataTable(qry, con);
            }
            DataTable dt = payments.dataTable(qry, con);
            if (dt.Rows.Count > 0)
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
                gt_previous_dues_amount(con);
                double totalpay = payble_after_disc + My.toDouble(lbl_previous_year_dues.Text);


                hd_paybaleamount.Value = totalpay.ToString("0.00");
                hd_adjustamount.Value = payble_after_disc.ToString("0.00");
                hd_totalamount.Value = payable.ToString("0.00");
                hd_total_discount.Value = disc.ToString("0.00");



                ///=============================
                ///
                string type = "Annual";
                string slip_no = "";
                string ad_no = ViewState["Admission_no"].ToString();
                string entry_id = "AD" + cretesessionid(con);
                slip_no = payments.invoice_readmission("slip_no", con);
                ViewState["yearlYSLipNo"] = slip_no;
                annual_payment(slip_no, entry_id, ad_no, con, PaidAmt, type, student_name, payment_date);
                payments.send_data_to_user_log_history("1", "1" + " received " + type + " fee :-" + PaidAmt + " Adjust Amount" + hd_total_discount.Value + " rs,Slip No :- " + slip_no + " from " + "StudentNAME" + ", Admission No :-" + ad_no, con);
                //try
                //{
                //    string notice_type = "3"; string notice_type_name = "Annual Fee Deposite";
                //    My.send_success_notice_fee(lbl_studentname.Text, lbl_class.Text, ViewState["Section"].ToString(), ViewState["rollnumber"].ToString(), lbl_admissionno.Text, txt_paid_adm_ann_fee_payment.Text, notice_type, notice_type_name, ViewState["sessionid"].ToString());
                //}
                //catch (Exception ex)
                //{
                //}
            }
        }



        private void gt_previous_dues_amount(SqlConnection con)
        {
            DataTable dt = payments.dataTable("select Dues_Amount from Previous_Year_Dues  where Session_id='" + ViewState["sessionid"].ToString() + "' and AdmissionNumber='" + lbl_admissionno.Text + "'  and Class_id='" + ViewState["classid"].ToString() + "' and Status='Unpaid'", con);
            if (dt.Rows.Count == 0)
            {
                lbl_previous_year_dues.Text = "0.00";
            }
            else
            {
                lbl_previous_year_dues.Text = dt.Rows[0][0].ToString();
            }
        }

        private void annual_payment(string slip_no, string entry_id, string ad_no, SqlConnection con, double PaidAmt, string type, string student_name, string payment_date)
        {
            string Uid = slip_no;
            string Tag = PaidAmt.ToString();
            string parameter = "";
            parameter = ViewState["hostaltaken"].ToString().ToUpper() == "NO" ? "AnnualFee" : "HostelAnnualFee";
            send_data_in_student_payment_history_annual(type, slip_no, entry_id, ad_no, parameter, con, PaidAmt, student_name, payment_date);
            send_data_to_school_ledger_annual(slip_no, entry_id, con, PaidAmt, type, student_name, payment_date);
            create_admission_annual_dues_annual(parameter, con, type, student_name, payment_date);
            send_data_in_feetypewise_collection_annual(slip_no, entry_id, parameter, con, PaidAmt, type, student_name, payment_date);
            send_data_to_annual_fee_collection_annual(slip_no, entry_id, parameter, con);
            //update_data_to_admission_registor(con);
        }

        private void send_data_to_annual_fee_collection_annual(string slip_no, string entry_id, string parameter, SqlConnection con)
        {
            double old_dues_amount = 0;
            DataTable pdt = payments.dataTable("select sum(cast(paid as float)) paid,sum(cast(dues as float)) dues from dbo.[Typewise_fee_collection] where admission_no='" + lbl_admissionno.Text + "' and session='" + ViewState["session"].ToString() + "' and  parameter like '%" + parameter + "%' and feetype!='Previous Dues' ", con);
            SqlDataAdapter ad = new SqlDataAdapter("select * from Annual_fee_collection where Admission_no='" + lbl_admissionno.Text + "' and session='" + ViewState["session"].ToString() + "'", con);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                DataRow dr = dt.NewRow();
                dr[1] = lbl_admissionno.Text;
                dr[2] = My.toDouble(hd_totalamount.Value).ToString("0.00");
                dr[3] = My.toDouble(hd_total_discount.Value).ToString("0.00");
                dr[4] = My.toDouble(hd_paybaleamount.Value).ToString("0.00");
                dr[5] = My.toDouble(pdt.Rows[0]["paid"].ToString()).ToString("0.00");
                dr[6] = My.toDouble(My.toDouble(dr[4]) - My.toDouble(dr[5])).ToString("0.00");
                dr[7] = txt_payment_date.Text;
                dr[8] = "CASH";
                dr[9] = slip_no;
                dr["session"] = ViewState["session"].ToString();
                dr["idate"] = Convert.ToDateTime(txt_payment_date.Text).ToString("yyyyMMdd");
                dr["remark"] = "";
                dr["time"] = mycode.time();
                dr["user_id"] = ViewState["Userid"].ToString(); ;
                dr["Slip_no"] = slip_no;
                dr["Acamedic_Semester_Id"] = "0";
                dr["branchid"] = ViewState["branchid"].ToString();
                dt.Rows.Add(dr);
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    dr[4] = My.toDouble(My.toDouble(dr[4]) + My.toDouble(old_dues_amount)).ToString("0.00");
                    dr[5] = My.toDouble(pdt.Rows[0]["paid"].ToString()).ToString("0.00");
                    dr[6] = My.toDouble(My.toDouble(dr[4]) - My.toDouble(dr[5])).ToString("0.00");
                }
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
        }

        private void send_data_in_feetypewise_collection_annual(string slip_no, string entry_id, string parameter, SqlConnection con, double PaidAmt, string type, string student_name, string payment_date)
        {
            string class_id = ViewState["classid"].ToString();
            double paid_amount = PaidAmt;
            if (My.toDouble(lbl_previous_year_dues.Text) > 0)
            {
                string previusyear = "Previous Year";
                string previousyearcontent_id = "101";
                double paid = 0;
                if (paid_amount > My.toDouble(lbl_previous_year_dues.Text))
                {
                    paid = My.toDouble(lbl_previous_year_dues.Text);
                    //insert
                    paid_amount = paid_amount - My.toDouble(lbl_previous_year_dues.Text);
                    payments.exeSql("insert into Typewise_fee_collection(admission_no,class,session,section,parameter,Date,idate,feetype,payable,paid,dues,status,month,content_id,transection,Ledger,is_readyfor_sync,is_sync,is_sync_dues_diary,Disc,Payable_after_disc,branchid,Acamedic_Semester_Id,class_id,user_id) values ('" + ViewState["Admission_no"].ToString() + "','" + lbl_class.Text + "','" + lbl_session.Text + "','" + ViewState["Section"].ToString() + "','" + parameter + "','" + mycode.date() + "','" + mycode.idate() + "','" + previusyear + "','" + My.toDouble(lbl_previous_year_dues.Text).ToString("0.00") + "','" + paid.ToString("0.00") + "','" + "0.00" + "','Paid','" + payments.get_start_month(con) + "','" + previousyearcontent_id + "','" + slip_no + "','School','false','false','false','" + 0 + "','" + paid.ToString("0.00") + "','" + ViewState["branchid"].ToString() + "','0','" + ViewState["classid"].ToString() + "','" + ViewState["Userid"].ToString() + "')", con);
                    payments.exeSql("update Previous_Year_Dues set Status='Paid' where  Session_id='" + ViewState["sessionid"].ToString() + "' and AdmissionNumber='" + ViewState["Admission_no"].ToString() + "' and Class_id='" + ViewState["classid"].ToString() + "'", con);

                    string qry = "insert into Monthly_Fee_Collection_Slip(adno,session,class,Section,parameter,Content,content_id,payable,paid,slipno,Ledger,disc_amt,Month,month_position,previously_paid,branchid,academicyear,Date,Idate,Hostel_id) values ('" + ViewState["Admission_no"].ToString() + "','" + lbl_session.Text + "','" + ViewState["classid"].ToString() + "','" + ViewState["Section"] + "','" + parameter + "','" + previusyear + "','" + previousyearcontent_id + "','" + paid.ToString("0.00") + "','" + paid.ToString("0.00") + "','" + slip_no + "','School','" + "0.00" + "','','0','0','" + ViewState["branchid"].ToString() + "','0','" + txt_payment_date.Text + "','" + mycode.ConvertStringToiDate(txt_payment_date.Text) + "','" + ViewState["Hostel_id"].ToString() + "');";
                    payments.exeSql(qry, con);
                }
                else
                {
                    paid = paid_amount;
                    //insert
                    double duesamount = My.toDouble(lbl_previous_year_dues.Text) - paid;
                    payments.exeSql("insert into Typewise_fee_collection(admission_no,class,session,section,parameter,Date,idate,feetype,payable,paid,dues,status,month,content_id,transection,Ledger,is_readyfor_sync,is_sync,is_sync_dues_diary,Disc,Payable_after_disc,branchid,Acamedic_Semester_Id,class_id,user_id) values ('" + ViewState["Admission_no"].ToString() + "','" + lbl_class.Text + "','" + lbl_session.Text + "','" + ViewState["Section"].ToString() + "','" + parameter + "','" + mycode.date() + "','" + mycode.idate() + "','" + previusyear + "','" + My.toDouble(lbl_previous_year_dues.Text).ToString("0.00") + "','" + paid.ToString("0.00") + "','" + duesamount.ToString("0.00") + "','Dues','" + payments.get_start_month(con) + "','" + previousyearcontent_id + "','" + slip_no + "','School','false','false','false','" + 0 + "','" + paid.ToString("0.00") + "','" + ViewState["branchid"].ToString() + "','0','" + ViewState["classid"].ToString() + "','" + ViewState["Userid"].ToString() + "')", con);
                    payments.exeSql("update Previous_Year_Dues set Status='Dues' where Session_id='" + ViewState["sessionid"].ToString() + "' and AdmissionNumber='" + ViewState["Admission_no"].ToString() + "' and Class_id='" + ViewState["classid"].ToString() + "' ", con);

                    string qry = "insert into Monthly_Fee_Collection_Slip(adno,session,class,Section,parameter,Content,content_id,payable,paid,slipno,Ledger,disc_amt,Month,month_position,previously_paid,branchid,academicyear,Date,Idate) values ('" + ViewState["Admission_no"].ToString() + "','" + lbl_session.Text + "','" + ViewState["classid"].ToString() + "','" + ViewState["Section"] + "','" + parameter + "','" + previusyear + "','" + previousyearcontent_id + "','" + paid.ToString("0.00") + "','" + My.toDouble(paid).ToString("0.00") + "','" + slip_no + "','School','" + "0.00" + "','','0','0','" + ViewState["branchid"].ToString() + "','0','" + txt_payment_date.Text + "','" + mycode.ConvertStringToiDate(txt_payment_date.Text) + "');";
                    payments.exeSql(qry, con);
                    paid_amount = 0;
                }
            }


            SqlDataAdapter ad = new SqlDataAdapter(" select * from Typewise_fee_collection  where admission_no='" + ViewState["Admission_no"].ToString() + "' and session='" + ViewState["session"].ToString() + "' and status='Dues' and parameter='" + parameter + "'", con);
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
                        dr["Date"] = txt_payment_date.Text;
                        dr["idate"] = My.toDateTime(txt_payment_date.Text).ToString("yyyyMMdd");
                        if (paid_amount >= dues)
                        {
                            string prevpaid = My.toDouble(dr["paid"].ToString()).ToString("0.00");
                            paid_amount = paid_amount - dues;
                            //paid amt is gratter than dues so dues amt is actual paid.
                            string paid = My.toDouble(dr["dues"].ToString()).ToString("0.00");
                            dr["Payable_after_disc"] = My.toDouble(My.toDouble(dr["payable"]) - My.toDouble(dr["Disc"])).ToString("0.00");
                            dr["paid"] = My.toDouble(dr["Payable_after_disc"]);
                            dr["dues"] = "0";
                            dr["status"] = "Paid";
                            #region send in collection slip
                            send_data_in_fee_collection_slip(My.toDouble(dr["payable"].ToString()).ToString("0.00"), My.toDouble(dues.ToString()).ToString("0.00"), dr["parameter"], dr["feetype"], dr["content_id"], slip_no, entry_id, dr["Ledger"], lbl_admissionno.Text, ViewState["session"].ToString(), ViewState["classid"].ToString(), ViewState["Section"].ToString(), dr["Disc"].ToString(), dr["month"], dr["position"], prevpaid, con);
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
                            send_data_in_fee_collection_slip(My.toDouble(dr["payable"].ToString()).ToString("0.00"), paid_amount.ToString("0.00"), dr["parameter"], dr["feetype"], dr["content_id"], slip_no, entry_id, dr["Ledger"], lbl_admissionno.Text, ViewState["session"].ToString(), ViewState["classid"].ToString(), ViewState["Section"].ToString(), dr["Disc"].ToString(), dr["month"], dr["position"], prevpaid, con);
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
        private void send_data_in_fee_collection_slip(string payable, string paid, object parameter, object content, object content_id, string slip_no, string entry_id, object ledger, string adno, string session, string classs, string sction, string disc_on_feehead, object month, object monthid, string prevpaid, SqlConnection con)
        {
            if (ledger == "")
            {
                ledger = "School";
            }
            string qry = "insert into Monthly_Fee_Collection_Slip(adno,session,class,Section,parameter,Content,content_id,payable,paid,slipno,Ledger,disc_amt,Month,month_position,previously_paid,Date,Idate,branchid,Hostel_id,Room_category) values ('" + adno + "','" + session + "','" + classs + "','" + sction + "','" + parameter + "','" + content + "','" + content_id + "','" + My.toDouble(payable).ToString("0.00") + "','" + My.toDouble(paid).ToString("0.00") + "','" + slip_no + "','" + ledger + "','" + disc_on_feehead + "','" + month + "','" + monthid + "','" + My.toDouble(prevpaid).ToString("0.00") + "','" + txt_payment_date.Text + "','" + My.DateConvertToIdate(txt_payment_date.Text) + "','" + ViewState["Branchid"].ToString() + "','" + ViewState["Hostel_id"].ToString() + "','" + ViewState["Room_Category_id"].ToString() + "');";
            payments.exeSql(qry, con);
        }

        private void create_admission_annual_dues_annual(string parameter, SqlConnection con, string type, string student_name, string payment_date)
        {
            if (payments.dataTable("select session,feetype,payable,paid,dues,status,content_id from dbo.[Typewise_fee_collection] WHERE admission_no='" + ViewState["Admission_no"].ToString() + "' and parameter='" + parameter + "' and session='" + ViewState["session"].ToString() + "'", con).Rows.Count == 0)
            {
                string query = "select '0' payable_after_disc,fmc.session,cm.content feetype,fmc.amount payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, (isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + ViewState["Admission_no"].ToString() + "'  and (parameter_id='2' or parameter_id='6') and session='" + ViewState["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='Annual' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and (parameter_id='2' or parameter_id='6') and session='" + ViewState["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='Annual' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'))) disc_amount  from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='" + parameter + "' and session='" + ViewState["session"].ToString() + "' and class_id='" + ViewState["classid"].ToString() + "' UNION ALL select '0' as payable_after_disc, session as Session,Perticular as feetype,Amount as payable,  '0' as paid, Amount as dues,'Dues' as status,'ADM01' as content_id,'0' as disc_amount from Misc_Fee_Master_Studentwise where (Type_Mode='AdmissionFee' or Type_Mode='AnnualFee') and session='" + ViewState["session"].ToString() + "' and Admission_No='" + ViewState["Admission_no"].ToString() + "'";
                DataTable dt = payments.dataTable(query, con);
                if (dt.Rows.Count == 0)
                { }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        payments.exeSql("insert into Typewise_fee_collection(admission_no,class,session,section,parameter,Date,idate,feetype,payable,paid,dues,status,month,content_id,transection,Ledger,is_readyfor_sync,is_sync,is_sync_dues_diary,Disc,Payable_after_disc,branchid,Acamedic_Semester_Id,class_id,user_id) values ('" + ViewState["Admission_no"].ToString() + "','" + ViewState["class_name"].ToString() + "','" + dr["session"].ToString() + "','" + ViewState["Section"].ToString() + "','" + parameter + "','" + mycode.date() + "','" + mycode.idate() + "','" + dr["feetype"].ToString() + "','" + My.toDouble(dr["payable"].ToString()).ToString("0.00") + "','0','" + My.toDouble(dr["payable"].ToString()).ToString("0.00") + "','Dues','" + payments.get_start_month(con) + "','" + dr["content_id"].ToString() + "','','School','false','false','false','" + My.toDouble(dr["disc_amount"].ToString()).ToString("0.00") + "','" + My.toDouble(dr["payable_after_disc"].ToString()).ToString("0.00") + "','1','0','" + ViewState["classid"].ToString() + "','1')", con);
                    }
                }
            }
        }

        private void send_data_to_school_ledger_annual(string transcation, string entry_id, SqlConnection con, double PaidAmt, string type, string student_name, string payment_date)
        {
            SqlDataAdapter ad = new SqlDataAdapter("select * from SchoolLedger ", con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "SchoolLedger");
            DataTable dt = ds.Tables[0];
            DataRow dr = dt.NewRow();
            dr["Particulars"] = type + " Fee Collection";
            dr["Discription"] = type + " fee for " + student_name + " Adm.No:-" + ViewState["Admission_no"].ToString() + " Total Bill :-" + PaidAmt.ToString() + " Paid Amount :-  " + PaidAmt.ToString();
            dr["AllInput"] = PaidAmt.ToString("0.00");
            dr["AllOutput"] = "0";
            dr["IDate"] = Convert.ToDateTime(payment_date).ToString("yyyyMMdd");
            dr["Date"] = payment_date;
            dr["TransactionId"] = transcation;
            dr["entry_id"] = entry_id;
            dr["session"] = ViewState["session"].ToString();
            dr["Ledger_Type"] = "School";
            dr["time"] = mycode.time();
            dr["user_id"] = ViewState["Userid"].ToString();
            dr["Addmission_no"] = ViewState["Admission_no"].ToString();
            dr["branchid"] = ViewState["branchid"].ToString();
            dt.Rows.Add(dr);
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
        }

        private void send_data_in_student_payment_history_annual(string type, string slip_no, string entry_id, string ad_no, string parameter_New, SqlConnection con, double PaidAmt, string student_name, string payment_date)
        {
            SqlCommand cmd;
            string query = "insert into Student_Payment_History(Addmission_no,Session,Date,Idate,Description,Entry_id,Slip_no,Amount,Type,mode,discount,Discoun_in_School,Discoun_in_Hostel,Discoun_in_Transport,fine,is_ofline_sync,Is_online_sync,is_update_in_online,Previous_admission_no,App_Transection_id,time,user_id,Acamedic_Semester_Id,Branch,Class_id,User_Slip_no,Pay_mode_transaction_no,Remarks,Transection_in,parameter_New) values (@Addmission_no,@Session,@Date,@Idate,@Description,@Entry_id,@Slip_no,@Amount,@Type,@mode,@discount,@Discoun_in_School,@Discoun_in_Hostel,@Discoun_in_Transport,@fine,@is_ofline_sync,@Is_online_sync,@is_update_in_online,@Previous_admission_no,@App_Transection_id,@time,@user_id,@Acamedic_Semester_Id,@Branch,@Class_id,@User_Slip_no,@Pay_mode_transaction_no,@Remarks,@Transection_in,@parameter_New)";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Addmission_no", ad_no);
            cmd.Parameters.AddWithValue("@Session", ViewState["session"].ToString());
            cmd.Parameters.AddWithValue("@Date", payment_date);
            cmd.Parameters.AddWithValue("@Idate", Convert.ToDateTime(payment_date).ToString("yyyyMMdd"));
            cmd.Parameters.AddWithValue("@Description", type + " fee collection for " + student_name + " Paid Amount : " + PaidAmt.ToString() + " /-");
            cmd.Parameters.AddWithValue("@Entry_id", entry_id);
            cmd.Parameters.AddWithValue("@Slip_no", slip_no);
            cmd.Parameters.AddWithValue("@Amount", PaidAmt.ToString("0.00"));
            cmd.Parameters.AddWithValue("@Type", type);
            cmd.Parameters.AddWithValue("@mode", "CASH");
            cmd.Parameters.AddWithValue("@discount", hd_total_discount.Value);
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
            cmd.Parameters.AddWithValue("@Branch", ViewState["branchid"].ToString());
            cmd.Parameters.AddWithValue("@Class_id", ViewState["classid"].ToString());
            cmd.Parameters.AddWithValue("@Remarks", "");
            cmd.Parameters.AddWithValue("@User_Slip_no", "");
            cmd.Parameters.AddWithValue("@Pay_mode_transaction_no", "");
            cmd.Parameters.AddWithValue("@Transection_in", "Software");
            cmd.Parameters.AddWithValue("@parameter_New", parameter_New);
            if (payments.InsertUpdateData(cmd, con))
            {
                SqlDataAdapter ad = new SqlDataAdapter("select * from SchoolLedger ", con);
                DataSet ds = new DataSet();
                ad.Fill(ds, "SchoolLedger");
                DataTable dt = ds.Tables[0];
                DataRow dr = dt.NewRow();
                dr["Particulars"] = type + " Money Receipt";
                dr["Discription"] = type + " Money Receipt " + student_name + " Adm.No:-" + ViewState["Admission_no"].ToString() + " Paid Amount :-  " + PaidAmt.ToString("0.00");
                dr["AllInput"] = PaidAmt.ToString("0.00");
                dr["AllOutput"] = "0";
                dr["IDate"] = Convert.ToDateTime(payment_date).ToString("yyyyMMdd");
                dr["Date"] = payment_date;
                dr["TransactionId"] = slip_no;
                dr["entry_id"] = entry_id;
                dr["session"] = ViewState["session"].ToString();
                dr["Ledger_Type"] = "School";
                dr["time"] = mycode.time();
                dr["user_id"] = "1";
                dr["Addmission_no"] = ViewState["Admission_no"];
                dr["branchid"] = "1";
                dr["Unique_id"] = entry_id;
                dt.Rows.Add(dr);
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(dt);
            }
        }
        #endregion
    }
}