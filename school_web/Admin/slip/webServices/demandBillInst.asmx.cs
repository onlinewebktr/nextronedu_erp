using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace school_web.Admin.slip.webServices
{
    /// <summary>
    /// Summary description for demandBillInst
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class demandBillInst : System.Web.Services.WebService
    {
        public class MyAdmitCardStudent
        {
            public string Student_name { get; set; }
            public string Subject_DOB { get; set; }
            public string Father_name { get; set; }
            public string Student_mob_no { get; set; }
            public string Registration_id { get; set; }
            public string Course_Name { get; set; }
            public string Session_name { get; set; }
            public string Rollnumber { get; set; }
            public string Mobile_no { get; set; }
            public string PayDate { get; set; }
            public string Section { get; set; }
            public string Mother_name { get; set; }
            public string Mother_mobile { get; set; }
            public string Print_for { get; set; }
            public string Total_dues_amt { get; set; }
            public string FIne_amt { get; set; }
            public string Inword_number { get; set; }
            public string Dues_zero { get; set; }
            public string lateFine_zero { get; set; }
            public string Months { get; set; }
            public string School_name { get; set; }
            public string Address { get; set; }
            public string Mobile_no_email { get; set; }
            public string LogoSchool { get; set; }
            public string WhatDiv { get; set; }
            public string WhatDivTB { get; set; }
            public string Print_date { get; set; }
            public string IsHeaderImageShow { get; set; }
            public string IsHeaderContentShow { get; set; }
            public string Header_images { get; set; }
            public string Principal_sig { get; set; }
            public List<MyFeeDetails> MyFeeDetailsItem { get; set; }

            //public List<MySigDetails> MySigDetailsItem { get; set; }
        }

        public class MyFeeDetails
        {
            public string InstallmentName { get; set; }
            public string Content { get; set; }
            public string Amount { get; set; }
            public string InstHide { get; set; }
            public string RowSpan { get; set; }
            public string Month_name { get; set; }
            public string SlNo { get; set; }
        }



        List<MyAdmitCardStudent> EMySubMark = new List<MyAdmitCardStudent>();
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_demand_bill(string Session_id, string Class_id, string Admission_no, string Branch_id, string Section, string Checked, string Month_name, string Paydate, string Session_name, string Bill_type, string Std_type)
        {
            string month_position = "";
            string[] stringSeparatorss = new string[] { "," };
            string[] arr = Month_name.Split(stringSeparatorss, StringSplitOptions.None);
            foreach (string value in arr)
            {
                month_position = value;
            } 
            My mycode = new My();
            string query = "";
            if (Checked == "1")
            {
                query = "Select * from admission_registor where Session_id=" + Session_id + " and Id in (" + Admission_no + ")  and Status='1'  order by Section,rollnumber asc";
            }
            else
            {
                if (Section == "0")
                {
                    if (Std_type == "1")
                    {
                        query = "Select * from admission_registor where Session_id=" + Session_id + " and Class_id='" + Class_id + "' and Status='1' and hosteltaken='Yes' order by Section,rollnumber asc";
                    }
                    else if (Std_type == "2")
                    {
                        query = "Select * from admission_registor where Session_id=" + Session_id + " and Class_id='" + Class_id + "' and Status='1' and hosteltaken not in ('Yes') order by Section,rollnumber asc";
                    }
                    else
                    {
                        query = "Select * from admission_registor where Session_id=" + Session_id + " and Class_id='" + Class_id + "' and Status='1' order by Section,rollnumber asc";
                    }
                }
                else
                {
                    if (Std_type == "1")
                    {
                        query = "Select * from admission_registor where Session_id=" + Session_id + " and Class_id='" + Class_id + "' and Section='" + Section + "'  and Status='1' and hosteltaken='Yes' order by Section,rollnumber asc";
                    }
                    else if (Std_type == "2")
                    {
                        query = "Select * from admission_registor where Session_id=" + Session_id + " and Class_id='" + Class_id + "' and Section='" + Section + "'  and Status='1' and hosteltaken not in ('Yes') order by Section,rollnumber asc";
                    }
                    else
                    {
                        query = "Select * from admission_registor where Session_id=" + Session_id + " and Class_id='" + Class_id + "' and Section='" + Section + "'  and Status='1'  order by Section,rollnumber asc";
                    }
                }
            }

            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "admission_registor");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                string queryF = "select * from Firm_Details";
                DataTable dtfrm = My.dataTable(queryF);
                string isHeaderImageShow = "hidden"; string isHeaderContentShow = "showed";
                if (dtfrm.Rows[0]["Is_slip_header"].ToString() == "True")
                {
                    isHeaderImageShow = "showed";
                    isHeaderContentShow = "hidden";
                }

                string principal_sig = get_Principal();
                bool loops = false; int topM = 1; bool loopsTB = false;
                foreach (DataRow dr in dt.Rows)
                {
                    List<MyFeeDetails> MBExamdetails = findmyFeeDetails(dr["Class_id"].ToString(), dr["session"].ToString(), Session_id, dr["admissionserialnumber"].ToString(), month_position, Bill_type);
                    // List<MySigDetails> MBSigdetails = findmySigDetails(dr["Session_id"].ToString(), dr["Class_id"].ToString(), Branch_id);

                    string std_imgs = dr["studentimagepath"].ToString();
                    if (std_imgs == "")
                    {
                        std_imgs = "hidden";
                    }
                    string motherMob = "";
                    string Print_for = "";
                    Print_for = "Candidates' Copy";

                    string mnthQry = "";
                    string qryttl = "";
                    if (Bill_type == "1")
                    {
                        qryttl = "select isnull(sum(convert(float, Dues_amt)),0) as Toata_dues_amt from STUDENT_WISE_DUES_For_Demand_BIll where Month_position<='" + month_position + "' and Session_id='" + Session_id + "' and Class_id='" + Class_id + "' and admission_no='" + dr["admissionserialnumber"].ToString() + "'  union all  select isnull(sum(convert(float, Dues_amt)),0) as Toata_dues_amt from STUDENT_WISE_DUES_For_Demand_BIll where Month_position<='" + month_position + "' and Session_id='" + Session_id + "' and Class_id='" + Class_id + "' and admission_no='" + dr["admissionserialnumber"].ToString() + "' and content_id='6121'";
                        mnthQry = "select distinct months,Month_position from STUDENT_WISE_DUES_For_Demand_BIll where Month_position<='" + month_position + "' and Session_id='" + Session_id + "' and Class_id='" + Class_id + "' and admission_no='" + dr["admissionserialnumber"].ToString() + "' and content_id!='6121' and Dues_amt>0 order by Month_position asc";
                    }
                    else
                    {
                        qryttl = "select isnull(sum(convert(float, Dues_amt)),0) as Toata_dues_amt from STUDENT_WISE_DUES_For_Demand_BIll where Month_position<='" + month_position + "' and Session_id='" + Session_id + "' and Class_id='" + Class_id + "' and admission_no='" + dr["admissionserialnumber"].ToString() + "' and parameter in('MonthlyFee','HostelMonthlyFee') union all  select isnull(sum(convert(float, Dues_amt)),0) as Toata_dues_amt from STUDENT_WISE_DUES_For_Demand_BIll where Month_position<='" + month_position + "' and Session_id='" + Session_id + "' and Class_id='" + Class_id + "' and admission_no='" + dr["admissionserialnumber"].ToString() + "' and content_id='6121' and parameter in('MonthlyFee','HostelMonthlyFee')";
                        mnthQry = "select distinct months,Month_position from STUDENT_WISE_DUES_For_Demand_BIll where Month_position<='" + month_position + "' and Session_id='" + Session_id + "' and Class_id='" + Class_id + "' and admission_no='" + dr["admissionserialnumber"].ToString() + "' and content_id!='6121' and Dues_amt>0 and (parameter='MonthlyFee' or parameter='HostelMonthlyFee') order by Month_position asc";
                    }
                    DataTable dtTtl = My.dataTable(qryttl);
                    string fineAmt = dtTtl.Rows[1]["Toata_dues_amt"].ToString();
                    string TotalAmt = dtTtl.Rows[0]["Toata_dues_amt"].ToString();

                    string inword_number = "";
                    int number = (int)Convert.ToDouble(TotalAmt);
                    inword_number = mycode.NumberToWords(number);


                    string months = "";




                    DataTable dtM = My.dataTable(mnthQry);
                    if (dtM.Rows.Count > 0)
                    {
                        months = dtM.Rows[0]["months"].ToString();
                        if (dtM.Rows.Count == 1)
                        {
                            months = dtM.Rows[0]["months"].ToString();
                        }
                        else
                        {
                            months = months + "-" + dtM.Rows[dtM.Rows.Count - 1]["months"].ToString();
                        }
                    }

                    string dues_zero = "hidden";
                    if (My.toDouble(TotalAmt) > 0)
                    {
                        dues_zero = "showd";
                    }
                    string lateFine_zero = "hidden";
                    if (My.toDouble(fineAmt) > 0)
                    {
                        lateFine_zero = "showd";
                    }

                    string whatDiv = "";
                    if (loops == false)
                    {
                        if (My.toDouble(TotalAmt) > 0)
                        {
                            whatDiv = "lftdb";
                            loops = true;
                        }
                    }
                    else
                    {
                        if (My.toDouble(TotalAmt) > 0)
                        {
                            loops = false;
                            whatDiv = "rghtdb";
                        }
                    }

                    string whatDivTB = "";
                    if (loopsTB == false)
                    {
                        if (topM == 1)
                        {
                            if (My.toDouble(TotalAmt) > 0)
                            {
                                topM++;
                                whatDivTB = "topdb";
                            }
                        }
                        else
                        {
                            if (My.toDouble(TotalAmt) > 0)
                            {
                                loopsTB = true;
                                topM = 1;
                                whatDivTB = "topdb";
                            }
                        }
                    }
                    else
                    {
                        if (topM == 1)
                        {
                            if (My.toDouble(TotalAmt) > 0)
                            {
                                topM++;
                                whatDivTB = "bottomdb";
                            }
                        }
                        else
                        {
                            if (My.toDouble(TotalAmt) > 0)
                            {
                                loopsTB = false;
                                topM = 1;
                                whatDivTB = "bottomdb";
                            }
                        }
                    }

                    EMySubMark.Add(new MyAdmitCardStudent
                    {
                        Student_name = dr["studentname"].ToString(),
                        Subject_DOB = dr["dob"].ToString(),
                        Father_name = dr["fathername"].ToString(),
                        Student_mob_no = dr["mobilenumber"].ToString(),
                        Registration_id = dr["admissionserialnumber"].ToString(),
                        Course_Name = dr["class"].ToString(),
                        Session_name = dr["session"].ToString(),
                        Section = dr["Section"].ToString(),
                        Rollnumber = dr["rollnumber"].ToString(),
                        Mobile_no = dr["mobilenumber"].ToString(),
                        Mother_name = dr["mothername"].ToString(),
                        Mother_mobile = motherMob,
                        Print_for = Print_for,
                        PayDate = Paydate,
                        Total_dues_amt = TotalAmt,
                        FIne_amt = fineAmt,
                        Months = months,
                        Inword_number = "Rupees " + inword_number + " Only.",
                        Dues_zero = dues_zero,
                        lateFine_zero = lateFine_zero,
                        WhatDiv = whatDiv,
                        WhatDivTB = whatDivTB,
                        Print_date = mycode.date(),
                        School_name = dtfrm.Rows[0]["firm_name"].ToString(),
                        Address = dtfrm.Rows[0]["address1"].ToString(),
                        Mobile_no_email = "Telephone No : " + dtfrm.Rows[0]["contact_no"].ToString() + " , E-mail Address : " + dtfrm.Rows[0]["email"].ToString(),
                        LogoSchool = dtfrm.Rows[0]["logo"].ToString(),
                        IsHeaderImageShow = isHeaderImageShow,
                        IsHeaderContentShow = isHeaderContentShow,
                        Header_images = dtfrm.Rows[0]["Header_images"].ToString(),

                        MyFeeDetailsItem = MBExamdetails,
                        Principal_sig = principal_sig,
                        //MySigDetailsItem = MBSigdetails
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(EMySubMark));
            }
        }

        private static string get_Principal()
        {
            string returN = "hidden";
            string query = "select top 1 Signature from user_details where User_Type='Principal'and Istatus='1'";
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count == 0)
            {
                return returN;
            }
            else
            {
                if (dt.Rows[0]["Signature"].ToString() == "")
                {
                    returN = "hidden";
                }
                else
                {
                    returN = dt.Rows[0]["Signature"].ToString();
                }
            }
            return returN;
        }


        //===============================
        private List<MyFeeDetails> findmyFeeDetails(string Class_id, string session, string Session_id, string admission_no, string month_position, string Bill_type)
        {
            List<MyFeeDetails> MyFeeDetailsItem = new List<MyFeeDetails>();

            DataTable dtinst = My.dataTable("select * from Fee_installment_master");
            if (dtinst.Rows.Count > 0)
            {
                string condition = "1"; string updated_month_position = ""; int slNo = 0;
                foreach (DataRow drinst in dtinst.Rows)
                {
                    slNo++;
                    string Month_position_no = drinst["Month_position_no"].ToString();
                    string[] stringSeparatorss = new string[] { "," };
                    string[] arr = Month_position_no.Split(stringSeparatorss, StringSplitOptions.None);
                    int n = 0;
                    string from_month_position = "";
                    foreach (string value in arr)
                    {
                        if (n == 0)
                        {
                            from_month_position = value;
                            if (from_month_position == "1")
                            {
                                from_month_position = "0";
                            }
                        }
                        updated_month_position = value;
                        n++;
                    }


                    //======================================================
                    string months = "";
                    string month_name = drinst["Month_name"].ToString();
                    string month_name1 = "";
                    string[] stringSeparator = new string[] { "," };
                    string[] arrss = month_name.Split(stringSeparator, StringSplitOptions.None);
                    foreach (string value in arrss)
                    {
                        months = months + "'" + value + "',"; 
                        string month3digt = "";
                        if (value.Length >= 3)
                        {
                            month3digt = value.Substring(0, 3);
                        }
                        else
                        {
                            month3digt = value; // or handle the case where the string is shorter than 3 characters
                        }
                        month_name1 = month_name1 + month3digt + ", ";
                    }
                    months = months.Remove(months.Length - 1); 
                    month_name1 = month_name1.Remove(month_name1.Length - 2);




                    if (condition == "1")
                    {
                        string query = "select admission_no,content,content_id,sum(convert(float, Dues_amt)) as Dues_amt from STUDENT_WISE_DUES_For_Demand_BIll where Month_position>='" + from_month_position + "' and Month_position<='" + updated_month_position + "' and Session_id='" + Session_id + "' and Class_id='" + Class_id + "' and admission_no='" + admission_no + "' and content_id!='6121' and Dues_amt>0 group by admission_no,content,content_id order by content asc";
                        if (Bill_type == "1")
                        {
                            query = "select admission_no,content,content_id,sum(convert(float, Dues_amt)) as Dues_amt from STUDENT_WISE_DUES_For_Demand_BIll where Month_position>='" + from_month_position + "' and Month_position<='" + updated_month_position + "' and Session_id='" + Session_id + "' and Class_id='" + Class_id + "' and admission_no='" + admission_no + "' and content_id!='6121' and Dues_amt>0 group by admission_no,content,content_id order by content asc";
                        }
                        else
                        {
                            query = "select admission_no,content,content_id,sum(convert(float, Dues_amt)) as Dues_amt from STUDENT_WISE_DUES_For_Demand_BIll where Month_position>='" + from_month_position + "' and Month_position<='" + updated_month_position + "' and Session_id='" + Session_id + "' and Class_id='" + Class_id + "' and admission_no='" + admission_no + "' and content_id!='6121' and Dues_amt>0 and parameter in('MonthlyFee','HostelMonthlyFee') group by admission_no,content,content_id order by content asc";
                        }
                        DataTable dt = My.dataTable(query);
                        if (dt.Rows.Count > 0)
                        {
                            int count = 0;
                            foreach (DataRow dr in dt.Rows)
                            {
                                string instHide = "hidden"; string rowSpan = "";
                                if (count == 0)
                                {
                                    instHide = "";
                                    rowSpan = dt.Rows.Count.ToString();
                                }
                                count++;
                                MyFeeDetailsItem.Add(new MyFeeDetails
                                {
                                    InstallmentName = drinst["Intstallment_name"].ToString(),
                                    Content = dr["content"].ToString(),
                                    Amount = dr["Dues_amt"].ToString(),
                                    InstHide = instHide,
                                    RowSpan = rowSpan,
                                    Month_name = month_name1,
                                    SlNo = slNo.ToString(),
                                });
                            }
                        }
                        else
                        {
                            slNo--;
                        }
                    }


                    foreach (string value in arr)
                    {
                        if (month_position == value)
                        {
                            condition = "0";
                        }
                    }
                }
            }




            return MyFeeDetailsItem;
        }


        ///============================
        ///
        public class MyAdmitCardStudent_M
        {
            public string Student_name { get; set; }
            public string Subject_DOB { get; set; }
            public string Father_name { get; set; }
            public string Student_mob_no { get; set; }
            public string Registration_id { get; set; }
            public string Course_Name { get; set; }
            public string Session_name { get; set; }
            public string Rollnumber { get; set; }
            public string Mobile_no { get; set; }
            public string PayDate { get; set; }
            public string Section { get; set; }
            public string Mother_name { get; set; }
            public string Mother_mobile { get; set; }
            public string Print_for { get; set; }
            public string Total_dues_amt { get; set; }
            public string FIne_amt { get; set; }
            public string Inword_number { get; set; }
            public string Dues_zero { get; set; }
            public string lateFine_zero { get; set; }
            public string Months { get; set; }
            public string School_name { get; set; }
            public string Address { get; set; }
            public string Mobile_no_email { get; set; }
            public string LogoSchool { get; set; }
            public string WhatDiv { get; set; }
            public string WhatDivTB { get; set; }
            public string Print_date { get; set; }
            public string IsHeaderImageShow { get; set; }
            public string IsHeaderContentShow { get; set; }
            public string Header_images { get; set; }


            public List<MyFeeDetails_M> MyFeeDetailsItem_M { get; set; }
            //public List<MySigDetails_M> MySigDetailsItem_M { get; set; }
        }

        public class MyFeeDetails_M
        {
            public string Content { get; set; }
            public string Amount { get; set; }
            public string Month_name { get; set; }
        }



        List<MyAdmitCardStudent_M> EMySubMark_M = new List<MyAdmitCardStudent_M>();
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_demand_bill_M(string Session_id, string Class_id, string Admission_no, string Branch_id, string Section, string Checked, string Month_name, string Paydate, string Session_name, string Bill_type)
        {
            My mycode = new My();
            string query = "";
            if (Checked == "1")
            {
                query = "Select * from admission_registor where Session_id=" + Session_id + " and Id in (" + Admission_no + ")  and Status='1'  order by Section,rollnumber asc";
            }
            else
            {
                if (Section == "0")
                {
                    query = "Select * from admission_registor where Session_id=" + Session_id + " and Class_id='" + Class_id + "'  and Status='1'  order by Section,rollnumber asc";
                }
                else
                {
                    query = "Select * from admission_registor where Session_id=" + Session_id + " and Class_id='" + Class_id + "' and Section='" + Section + "'  and Status='1'  order by Section,rollnumber asc";
                }
            }
            string month_position = My.get_single_column_data("select Position as Column_Name from Month_Index where Month='" + Month_name + "'");
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "admission_registor");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                string queryF = "select * from Firm_Details";
                DataTable dtfrm = My.dataTable(queryF);
                string isHeaderImageShow = "hidden"; string isHeaderContentShow = "showed";
                if (dtfrm.Rows[0]["Is_slip_header"].ToString() == "True")
                {
                    isHeaderImageShow = "showed";
                    isHeaderContentShow = "hidden";
                }





                bool loops = false; int topM = 1; bool loopsTB = false;
                foreach (DataRow dr in dt.Rows)
                {
                    List<MyFeeDetails_M> MBExamdetails_M = findmyFeeDetails_M(dr["Class_id"].ToString(), dr["session"].ToString(), Session_id, dr["admissionserialnumber"].ToString(), month_position, Bill_type);

                    string std_imgs = dr["studentimagepath"].ToString();
                    if (std_imgs == "")
                    {
                        std_imgs = "hidden";
                    }
                    string motherMob = "";
                    string Print_for = "";
                    Print_for = "Candidates' Copy";

                    string mnthQry = "";
                    string qryttl = "";
                    if (Bill_type == "1")
                    {
                        qryttl = "select isnull(sum(convert(float, Dues_amt)),0) as Toata_dues_amt from STUDENT_WISE_DUES_For_Demand_BIll where Month_position<='" + month_position + "' and Session_id='" + Session_id + "' and Class_id='" + Class_id + "' and admission_no='" + dr["admissionserialnumber"].ToString() + "'  union all  select isnull(sum(convert(float, Dues_amt)),0) as Toata_dues_amt from STUDENT_WISE_DUES_For_Demand_BIll where Month_position<='" + month_position + "' and Session_id='" + Session_id + "' and Class_id='" + Class_id + "' and admission_no='" + dr["admissionserialnumber"].ToString() + "' and content_id='6121'";
                    }
                    else
                    {
                        qryttl = "select isnull(sum(convert(float, Dues_amt)),0) as Toata_dues_amt from STUDENT_WISE_DUES_For_Demand_BIll where Month_position<='" + month_position + "' and Session_id='" + Session_id + "' and Class_id='" + Class_id + "' and admission_no='" + dr["admissionserialnumber"].ToString() + "' and parameter in('MonthlyFee','HostelMonthlyFee') union all  select isnull(sum(convert(float, Dues_amt)),0) as Toata_dues_amt from STUDENT_WISE_DUES_For_Demand_BIll where Month_position<='" + month_position + "' and Session_id='" + Session_id + "' and Class_id='" + Class_id + "' and admission_no='" + dr["admissionserialnumber"].ToString() + "' and content_id='6121' and parameter in('MonthlyFee','HostelMonthlyFee')";
                    }
                    DataTable dtTtl = My.dataTable(qryttl);
                    string fineAmt = dtTtl.Rows[1]["Toata_dues_amt"].ToString();
                    string TotalAmt = dtTtl.Rows[0]["Toata_dues_amt"].ToString();

                    string inword_number = "";
                    int number = (int)Convert.ToDouble(TotalAmt);
                    inword_number = mycode.NumberToWords(number);






                    string dues_zero = "hidden";
                    if (My.toDouble(TotalAmt) > 0)
                    {
                        dues_zero = "showd";
                    }
                    string lateFine_zero = "hidden";
                    if (My.toDouble(fineAmt) > 0)
                    {
                        lateFine_zero = "showd";
                    }

                    string whatDiv = "";
                    if (loops == false)
                    {
                        if (My.toDouble(TotalAmt) > 0)
                        {
                            whatDiv = "lftdb";
                            loops = true;
                        }
                    }
                    else
                    {
                        if (My.toDouble(TotalAmt) > 0)
                        {
                            loops = false;
                            whatDiv = "rghtdb";
                        }
                    }

                    string whatDivTB = "";
                    if (loopsTB == false)
                    {
                        if (topM == 1)
                        {
                            if (My.toDouble(TotalAmt) > 0)
                            {
                                topM++;
                                whatDivTB = "topdb";
                            }
                        }
                        else
                        {
                            if (My.toDouble(TotalAmt) > 0)
                            {
                                loopsTB = true;
                                topM = 1;
                                whatDivTB = "topdb";
                            }
                        }
                    }
                    else
                    {
                        if (topM == 1)
                        {
                            if (My.toDouble(TotalAmt) > 0)
                            {
                                topM++;
                                whatDivTB = "bottomdb";
                            }
                        }
                        else
                        {
                            if (My.toDouble(TotalAmt) > 0)
                            {
                                loopsTB = false;
                                topM = 1;
                                whatDivTB = "bottomdb";
                            }
                        }
                    }

                    EMySubMark_M.Add(new MyAdmitCardStudent_M
                    {
                        Student_name = dr["studentname"].ToString(),
                        Subject_DOB = dr["dob"].ToString(),
                        Father_name = dr["fathername"].ToString(),
                        Student_mob_no = dr["mobilenumber"].ToString(),
                        Registration_id = dr["admissionserialnumber"].ToString(),
                        Course_Name = dr["class"].ToString(),
                        Session_name = dr["session"].ToString(),
                        Section = dr["Section"].ToString(),
                        Rollnumber = dr["rollnumber"].ToString(),
                        Mobile_no = dr["mobilenumber"].ToString(),
                        Mother_name = dr["mothername"].ToString(),
                        Mother_mobile = motherMob,
                        Print_for = Print_for,
                        PayDate = Paydate,
                        Total_dues_amt = TotalAmt,
                        FIne_amt = fineAmt,
                        Inword_number = inword_number,
                        Dues_zero = dues_zero,
                        lateFine_zero = lateFine_zero,
                        WhatDiv = whatDiv,
                        WhatDivTB = whatDivTB,
                        Print_date = mycode.date(),
                        School_name = dtfrm.Rows[0]["firm_name"].ToString(),
                        Address = dtfrm.Rows[0]["address1"].ToString(),
                        Mobile_no_email = "Telephone No : " + dtfrm.Rows[0]["contact_no"].ToString() + " , E-mail Address : " + dtfrm.Rows[0]["email"].ToString(),
                        LogoSchool = dtfrm.Rows[0]["logo"].ToString(),
                        IsHeaderImageShow = isHeaderImageShow,
                        IsHeaderContentShow = isHeaderContentShow,
                        Header_images = dtfrm.Rows[0]["Header_images"].ToString(),


                        MyFeeDetailsItem_M = MBExamdetails_M,
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(EMySubMark_M));
            }
        }
        private List<MyFeeDetails_M> findmyFeeDetails_M(string Class_id, string session, string Session_id, string admission_no, string month_position, string Bill_type)
        {
            List<MyFeeDetails_M> MyFeeDetailsItem_M = new List<MyFeeDetails_M>();
            string query = "select months,Month_position,admission_no,content,content_id,Dues_amt from STUDENT_WISE_DUES_For_Demand_BIll where Month_position<='" + month_position + "' and Session_id='" + Session_id + "' and Class_id='" + Class_id + "' and admission_no='" + admission_no + "' and content_id!='6121' and Dues_amt>0  order by Month_position asc";
            if (Bill_type == "1")
            {
                query = "select months,Month_position,admission_no,content,content_id,Dues_amt from STUDENT_WISE_DUES_For_Demand_BIll where Month_position<='" + month_position + "' and Session_id='" + Session_id + "' and Class_id='" + Class_id + "' and admission_no='" + admission_no + "' and content_id!='6121' and Dues_amt>0 order by Month_position asc";
            }
            else
            {
                query = "select months,Month_position,admission_no,content,content_id,Dues_amt from STUDENT_WISE_DUES_For_Demand_BIll where Month_position<='" + month_position + "' and Session_id='" + Session_id + "' and Class_id='" + Class_id + "' and admission_no='" + admission_no + "' and content_id!='6121' and Dues_amt>0 and parameter in('MonthlyFee','HostelMonthlyFee') order by Month_position asc";
            }
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string m_name = My.getMonthS_sort_name(dr["months"].ToString());
                    MyFeeDetailsItem_M.Add(new MyFeeDetails_M
                    {
                        Content = dr["content"].ToString(),
                        Amount = dr["Dues_amt"].ToString(),
                        Month_name = m_name,
                    });
                }
            }
            return MyFeeDetailsItem_M;
        }
    }
}
