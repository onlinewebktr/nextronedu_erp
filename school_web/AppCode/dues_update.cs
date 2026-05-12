using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace school_web.AppCode
{
    public class dues_update
    {
        internal static void update_student_dues(string session_id, string class_id, string admission_no)
        {
            string Discount_on = "";
            My mycode = new My();
            DataTable dt = My.dataTable("select t1.Transfer_Status_Old,t1.Transfer_Status,t1.session,t1.admissionserialnumber as Admission_no,t1.class,t1.father_mob,t1.Section,t1.rollnumber,t1.studentname as Student_Name,t1.Session_id,t1.Class_id,t2.Month_name,t2.Month_id,t1.category_id,t1.SubCategory_id,t1.Transportation_Id,t1.hosteltaken,t1.hostel_id,t1.is_applied_dayboarding,t1.day_boarding_with_lunch from admission_registor t1 left join Student_mapping_with_boarding_with_lunch t2 on t1.admissionserialnumber=t2.Admission_no and t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id where t1.Session_id='" + session_id + "' and t1.Status='1' and t1.Class_id='" + class_id + "' and t1.admissionserialnumber='" + admission_no + "'");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DataTable feedt = new DataTable();
                    string parameter = dr["hosteltaken"].ToString().ToLower() == "yes" ? "HostelMonthlyFee" : "MonthlyFee";
                    Dictionary<string, object> dc1 = mycode.Bind_Transport_data_for_assined_student(dr["Session_id"].ToString(), dr["Class_id"].ToString(), admission_no);
                    string Transport_id = (String)dc1["Transport_id"];
                    string TransportPath_id = (String)dc1["TransportPath_id"];
                    string Boarding_Point_id = (String)dc1["Boarding_Point_id"];
                    string Transport_Assigned_Id = (String)dc1["Transport_Assigned_Id"];
                    string IsBoarding = "0";
                    string parameteridS = "4";


                    string LunchMnthName = "";
                    string LunchMnthId = "";
                    if (dr["Month_id"].ToString() != "")
                    {
                        LunchMnthName = dr["Month_name"].ToString();
                        LunchMnthId = dr["Month_id"].ToString();
                        IsBoarding = "1";
                    }


                    Dictionary<string, object> dc11 = mycode.Bind_hostel_data_for_assined_student(dr["Session_id"].ToString(), dr["Class_id"].ToString(), admission_no);
                    string Hostel_id = (String)dc11["Hostel_id"];
                    string Room_Category_id = (String)dc11["Room_Category_id"];
                    string From_month_name = (String)dc11["From_month_name"];
                    string From_month_id = (String)dc11["From_month_id"];
                    string Assined_Year_Month = (String)dc11["Assined_Year_Month"];
                    string Hostel_assign_id = (String)dc11["Hostel_assign_id"];



                    string dues = "0";
                    DataTable dtMnth = My.dataTable("select Month,Month_Id,Position from Month_Index order by Position asc");
                    if (dtMnth.Rows.Count > 0)
                    {
                        foreach (DataRow drMnth in dtMnth.Rows)
                        {
                            if (IsBoarding == "1")
                            {
                                int mnthids = My.toint(drMnth["Month_Id"].ToString());
                                if (My.toint(LunchMnthId) <= mnthids)
                                {
                                    parameteridS = "44";
                                }
                                else
                                {
                                    parameteridS = "4";
                                }
                            }


                            if (My.dataTable("select  * from dbo.[Typewise_fee_collection] where admission_no='" + admission_no + "' and session='" + dr["session"].ToString() + "' and month='" + drMnth["Month"].ToString() + "' and (parameter='" + parameter + "' or parameter='HostelMonthlyFee')").Rows.Count > 0)
                            {
                                feedt = My.dataTable("select (sum(convert(float, amount))-sum(convert(float, isnull((disc_amount),'0')))-sum(convert(float, isnull((previously_paid),'0')))) as Total_Dues from (select payable amount,isnull(paid,'0') previously_paid, Disc as disc_amount from dbo.[Typewise_fee_collection] where admission_no='" + admission_no + "' and session='" + dr["session"].ToString() + "' and month='" + drMnth["Month"].ToString() + "' and (parameter='MonthlyFee' or parameter='HostelMonthlyFee') and content_id!='6121') t");
                                if (feedt.Rows.Count.ToString() != "0")
                                {
                                    dues = feedt.Rows[0]["Total_Dues"].ToString();
                                }
                                else
                                {
                                    dues = "0";
                                }
                            }
                            else
                            {
                                Dictionary<string, object> dc = new Dictionary<string, object>();
                                dc["admission_no"] = admission_no;
                                dc["session_id"] = dr["Session_id"].ToString();
                                dc["class"] = dr["class"].ToString();
                                dc["session"] = dr["session"].ToString();
                                dc["class_id"] = dr["Class_id"].ToString();
                                dc["hosteltaken"] = dr["hosteltaken"].ToString().ToLower();
                                dc["months"] = drMnth["Month"].ToString();
                                dc["tr_ledger"] = My.is_combine ? "School" : "Transport";
                                dc["hostel_id"] = Hostel_id;
                                dc["Room_Category_id"] = Room_Category_id;
                                dc["Hostel_assig_id"] = Hostel_assign_id;
                                dc["day_boarding"] = My.toBool(dr["is_applied_dayboarding"]);
                                dc["day_boarding_lunch"] = My.toBool(dr["day_boarding_with_lunch"]);
                                dc["category_id"] = dr["category_id"].ToString();
                                dc["sub_category_id"] = dr["SubCategory_id"].ToString();

                                dc["TransportationPath_id"] = TransportPath_id;
                                dc["transportportation_id"] = Transport_id;
                                dc["Boarding_Point_id"] = Boarding_Point_id;

                                dc["parameter_id"] = parameteridS;
                                dc["sp_status"] = "1";
                                string cunrt_session = dr["session"].ToString();
                                string[] stringSeparators = new string[] { "-" };
                                string[] arr = cunrt_session.Split(stringSeparators, StringSplitOptions.None);
                                string session_frst_year = arr[0];
                                string session_last_year = arr[1];
                                int session_s_year = My.toint(session_frst_year);
                                int s_year = My.toint(session_frst_year);
                                string monthid = My.tomonth_numberstring(drMnth["Month"].ToString());
                                int pay_month = My.toint(monthid);
                                s_year = My.check_start_months(pay_month, s_year);
                                dc["monthid"] = s_year + monthid;
                                feedt = My.dataTableSP("sp_Fetch_month_dues", dc);
                                if (feedt.Rows.Count.ToString() != "0")
                                {
                                    dues = feedt.Rows[0]["Total_Dues"].ToString();
                                }
                                else
                                {
                                    dues = "0";
                                }
                                if (My.toDouble(dues) == 0)
                                {
                                    dues = "0";
                                }
                            }
                            if (mycode.IsUserExist("select  * from Student_wise_dues where Session_id='" + session_id + "' and Class_id='" + class_id + "' and Admission_no='" + admission_no + "' and Type='Monthly' and Month='" + drMnth["Month"].ToString() + "'"))
                            {
                                //INSERT
                                My.Insert("Student_wise_dues", new
                                {
                                    Session_id = session_id,
                                    Class_id = class_id,
                                    Admission_no = admission_no,
                                    Type = "Monthly",
                                    Month = drMnth["Month"].ToString(),
                                    Dues_amount = dues,
                                    Updated_date = mycode.date(),
                                    Updated_time = mycode.time(),
                                    Month_poition = drMnth["Position"].ToString(),
                                    Month_id = drMnth["Month_Id"].ToString()
                                });
                            }
                            else    //UPDATE   Month_Id,Position
                            {
                                My.exeSql("update Student_wise_dues set Dues_amount='" + dues + "',Updated_date='" + mycode.date() + "',Updated_time='" + mycode.time() + "',Month_poition='" + drMnth["Position"].ToString() + "',Month_id='" + drMnth["Month_Id"].ToString() + "' where Session_id='" + session_id + "' and Class_id='" + class_id + "' and Admission_no='" + admission_no + "' and Type='Monthly' and Month='" + drMnth["Month"].ToString() + "'");
                            }
                        }
                    }



                    //===Admission Annual


                    string duesamtAdm = "0";
                    string Transfer_Status = dr["Transfer_Status"].ToString();
                    string studenttype = dr["Transfer_Status"].ToString();
                    if (dr["Transfer_Status"].ToString().ToUpper() == "TRANSFERRED")
                    {
                        studenttype = dr["Transfer_Status_Old"].ToString();
                        Transfer_Status = dr["Transfer_Status_Old"].ToString();
                    }

                    string parameterAd = ""; string parameter_idAd = "";
                    if (studenttype == "New")
                    {
                        parameterAd = dr["hosteltaken"].ToString().ToUpper() == "YES" ? "HostelAdmissionFee" : "AdmissionFee";
                        parameter_idAd = dr["hosteltaken"].ToString().ToUpper() == "YES" ? "5" : "1";
                        Discount_on = "Admission";
                    }
                    else
                    {
                        parameterAd = dr["hosteltaken"].ToString().ToUpper() == "YES" ? "HostelAnnualFee" : "AnnualFee";
                        parameter_idAd = dr["hosteltaken"].ToString().ToUpper() == "YES" ? "6" : "2";
                        Discount_on = "Annual";
                    }



                    string qry = "";
                    if (Hostel_id != "0")
                    {
                        qry = "select *,(payable-cast(disc_amount as float)-cast(paid as float)) net_payable from(select '0' payable_after_disc,session,feetype,cast(payable as float) payable,paid,dues,status,content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where Hostel_id=" + Hostel_id + " and admission_no='" + admission_no + "'  and parameter_id='" + parameter_idAd + "' and session='" + dr["session"].ToString() + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='" + Discount_on + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Hostel_id=" + Hostel_id + " and Class_id='" + dr["Class_id"].ToString() + "' and admission_no='All'  and  parameter_id='" + parameter_idAd + "'  and session='" + dr["session"].ToString() + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='" + Discount_on + "' and category_id='" + dr["category_id"].ToString() + "' and sub_category_id='" + dr["SubCategory_id"].ToString() + "'))),0) disc_amount from dbo.[Typewise_fee_collection] WHERE admission_no='" + admission_no + "' and parameter='" + parameterAd + "' and session='" + dr["session"].ToString() + "')t";
                    }
                    else
                    {
                        qry = "select *,(payable-cast(disc_amount as float)-cast(paid as float)) net_payable from(select '0' payable_after_disc,session,feetype,cast(payable as float) payable,paid,dues,status,content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where     admission_no='" + admission_no + "'  and parameter_id='" + parameter_idAd + "' and session='" + dr["session"].ToString() + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='" + Discount_on + "'),(select top 1 disc_amount from dbo.[Discount_Master] where   Class_id='" + dr["Class_id"].ToString() + "' and admission_no='All'  and  parameter_id='" + parameter_idAd + "'  and session='" + dr["session"].ToString() + "' and fee_head_id=Typewise_fee_collection.content_id and Discount_on='" + Discount_on + "' and category_id='" + dr["category_id"].ToString() + "' and sub_category_id='" + dr["SubCategory_id"].ToString() + "'))),0) disc_amount from dbo.[Typewise_fee_collection] WHERE admission_no='" + admission_no + "' and parameter='" + parameterAd + "' and session='" + dr["session"].ToString() + "')t";
                    }

                    DataTable fee_dt = My.dataTable(qry);
                    if (fee_dt.Rows.Count == 0)
                    {
                        if (Hostel_id == "0")
                        {
                            qry = "select '0' as payable_after_disc, session as Session,Perticular as feetype,Amount as payable,  '0' as paid, Amount as dues,'Dues' as status,'ANN01' as content_id,'0' as disc_amount,Amount as net_payable from Misc_Fee_Master_Studentwise where (Type_Mode='AdmissionFee' or Type_Mode='AnnualFee') and session='" + dr["session"].ToString() + "' and Admission_No='" + admission_no + "' UNION ALL select *,(payable-cast(disc_amount as float)) net_payable from(select '0' payable_after_disc,fmc.session,cm.content feetype,cast(fmc.amount as float) payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + admission_no + "'  and parameter_id='" + parameter_idAd + "' and session='" + dr["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='" + Discount_on + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id='" + dr["Class_id"].ToString() + "' and admission_no='All'  and parameter_id='" + parameter_idAd + "' and session='" + dr["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='" + Discount_on + "' and category_id='" + dr["category_id"].ToString() + "' and sub_category_id='" + dr["SubCategory_id"].ToString() + "'))),0) disc_amount   from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='" + parameterAd + "'  and session='" + dr["session"].ToString() + "' and class_id='" + dr["Class_id"].ToString() + "')t";
                        }
                        else
                        {
                            qry = "select '0' as payable_after_disc, session as Session,Perticular as feetype,Amount as payable,  '0' as paid, Amount as dues,'Dues' as status,'ANN01' as content_id,'0' as disc_amount,Amount as net_payable from Misc_Fee_Master_Studentwise where (Type_Mode='AdmissionFee' or Type_Mode='AnnualFee') and session='" + dr["session"].ToString() + "' and Admission_No='" + admission_no + "' UNION ALL select *,(payable-cast(disc_amount as float)) net_payable from(select '0' payable_after_disc,fmc.session,cm.content feetype,cast(fmc.amount as float) payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where Hostel_id=" + Hostel_id + " and admission_no='" + admission_no + "'  and parameter_id='" + parameter_idAd + "' and session='" + dr["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='" + Discount_on + "'),(select top 1 disc_amount from dbo.[Discount_Master] where  Hostel_id=" + Hostel_id + " and Class_id='" + dr["Class_id"].ToString() + "' and admission_no='All'  and parameter_id='" + parameter_idAd + "' and session='" + dr["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='" + Discount_on + "' and category_id='" + dr["category_id"].ToString() + "' and sub_category_id='" + dr["SubCategory_id"].ToString() + "'))),0) disc_amount   from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='" + parameterAd + "'  and session='" + dr["session"].ToString() + "' and class_id='" + dr["Class_id"].ToString() + "' and   fmc.Hostel_Id=" + Hostel_id + ")t";
                        }
                        fee_dt = My.dataTable(qry);
                    }


                    DataTable dtadmdues = mycode.FillData(qry);
                    if (dtadmdues.Rows.Count == 0)
                    {
                    }
                    else
                    {
                        double payable = 0, paid = 0, duesA = 0, disc = 0, payble_after_disc = 0;
                        foreach (DataRow drad in dtadmdues.Rows)
                        {
                            drad["payable_after_disc"] = My.toDouble(drad["payable"]) - My.toDouble(drad["disc_amount"]) - My.toDouble(drad["paid"]);
                            payable += My.toDouble(drad["payable"]);
                            paid += My.toDouble(drad["paid"]);
                            duesA += My.toDouble(drad["dues"]);
                            disc += My.toDouble(drad["disc_amount"]);
                            payble_after_disc += My.toDouble(drad["payable_after_disc"]);
                        }
                        duesamtAdm = payble_after_disc.ToString("0.00");
                    }


                    if (mycode.IsUserExist("select  * from Student_wise_dues where Session_id='" + session_id + "' and Class_id='" + class_id + "' and Admission_no='" + admission_no + "' and Type='Admision'"))
                    {
                        //INSERT
                        My.Insert("Student_wise_dues", new
                        {
                            Session_id = session_id,
                            Class_id = class_id,
                            Admission_no = admission_no,
                            Type = "Admision",
                            Month = "April",
                            Dues_amount = duesamtAdm,
                            Updated_date = mycode.date(),
                            Updated_time = mycode.time(),
                            Month_poition = "1",
                            Month_id = "1"
                        });
                    }
                    else    //UPDATE
                    {
                        My.exeSql("update Student_wise_dues set Dues_amount='" + duesamtAdm + "',Updated_date='" + mycode.date() + "',Updated_time='" + mycode.time() + "',Month_poition='1',Month_id='1' where Session_id='" + session_id + "' and Class_id='" + class_id + "' and Admission_no='" + admission_no + "' and Type='Admision'");
                    }
                }
            }
        }
    }
}