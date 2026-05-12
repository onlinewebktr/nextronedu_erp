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
    public partial class Create_ledger_for_student : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        My mycode = new My();
        protected void btn_Create_ledger_Click(object sender, EventArgs e)
        {
            string query = "Select * from admission_registor where Transfer_Status in('NT','New')  ";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    string party_id = dt.Rows[i]["admissionserialnumber"].ToString();
                    string studentname = dt.Rows[i]["studentname"].ToString();
                    string gender = dt.Rows[i]["gender"].ToString();

                    string dob = dt.Rows[i]["dob"].ToString();

                    string city = dt.Rows[i]["city"].ToString();// adress
                    string district = dt.Rows[i]["district"].ToString();
                    string pin = dt.Rows[i]["pin"].ToString();
                    string father_mob = dt.Rows[i]["father_mob"].ToString();// mobileno
                    string state = dt.Rows[i]["state"].ToString();
                    string fathername = dt.Rows[i]["fathername"].ToString();

                    string dateofadmission = dt.Rows[i]["dateofadmission"].ToString();


                    send_data_Create_ledger(party_id, studentname, gender, dob, city, district, pin, father_mob, state, fathername, dateofadmission);
                }
            }
        }

        private void send_data_Create_ledger(string party_id, string studentname, string gender, string dob, string city, string district, string pin, string father_mob, string state, string fathername, string dateofadmission)
        {
           
            try
            {

                DateTime temp;
                if (DateTime.TryParse(dateofadmission, out temp))
                {
                    
                   // return true;
                }
                else
                {
                    dateofadmission = mycode.date();
                }
                
            }
            catch
            {

            }



            string statename = mycode.getstatename(state);
            string getstatecode = mycode.getstatename(state);
            SqlConnection conn = new SqlConnection(My.conn);
            SqlDataAdapter ad = new SqlDataAdapter("select * from party_details where  party_id='" + party_id + "'", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                DataRow dr = dt.NewRow();
                dr[1] = studentname;
                dr[2] = city;
                dr[3] = district;
                dr[4] = father_mob;
                dr["gstin"] = "UnRegistered";
                dr[6] = party_id;
                dr[7] = mycode.date();
                dr["firm"] = My.firm_id();
                dr["Registration_Type"] = "Customer";
                dr["State_Code"] = getstatecode;
                dr["State"] = statename;
                dr["type"] = "Customer";
                dr["Care_of"] = fathername;
                dr["pan_no"] = "";
                dr["Account_No"] = "";
                dr["Bank_Name"] = "";
                dr["IFSC_Code"] = "";
                dr[12] = "0";
                dr[13] = "NA";
                dt.Rows.Add(dr);
                //My.firm_wise_auto_serial("party_id");
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(dt);
                My.save_Account_Ledger_Details(studentname, party_id, "26");
                 
                My.update_Ledger_Opening_Balance(studentname, party_id, "26", "Dr", "0.00", dateofadmission, My.get_session());
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    dr[1] = studentname;
                    dr[2] = city;
                    dr[3] = district;
                    dr[4] = father_mob;

                }
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(dt);
            }
        }

        protected void btn_create_legder_Click(object sender, EventArgs e)
        {
            string query = "Select * from PRL_Employee_Master";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    string party_id = dt.Rows[i]["Emp_Code"].ToString();
                    string Employee_Name = dt.Rows[i]["Employee_Name"].ToString();
                    string gender = dt.Rows[i]["Gender"].ToString();
                    string dob = dt.Rows[i]["Date_of_birth"].ToString();
                    string city = dt.Rows[i]["City"].ToString();// adress
                    string district = "";
                    string pin = "0";
                    string Mobile = dt.Rows[i]["Mobile"].ToString();// mobileno
                    string state = dt.Rows[i]["State"].ToString();
                    string fathername = dt.Rows[i]["Father_Name"].ToString();
                    string dateofadmission = dt.Rows[i]["Date_of_Joining"].ToString();

                    try
                    {

                        DateTime temp;
                        if (DateTime.TryParse(dateofadmission, out temp))
                        {

                            // return true;
                        }
                        else
                        {
                            dateofadmission = mycode.date();
                        }

                    }
                    catch
                    {

                    }

                    try
                    {

                        DateTime temp;
                        if (DateTime.TryParse(dob, out temp))
                        {

                            // return true;
                        }
                        else
                        {
                            dob = mycode.date();
                        }

                    }
                    catch
                    {

                    }

                    mycode.send_data_Create_employee(party_id, Employee_Name, gender, dob, city, district, pin, Mobile, state, fathername, dateofadmission);
                }
            }
        }

      



        string VoucherType = "Receipt";
        protected void btn_student_amount_send_data_receipt_voucher_Click(object sender, EventArgs e)
        {

            string query = "Select *,format (time,'hh:mm:ss tt') time1,(select top 1 studentname from admission_registor where admissionserialnumber=Student_Payment_History.Addmission_no ) as studentname from Student_Payment_History where Session='2024-2025'";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string Addmission_no = dt.Rows[i]["Addmission_no"].ToString();
                    string user_id = dt.Rows[i]["user_id"].ToString();
                   
                    string Date1 = dt.Rows[i]["Date"].ToString();
                    string time1 = dt.Rows[i]["time1"].ToString();
                    string Date = Date1 + " " + time1;

                    string Idate = dt.Rows[i]["Idate"].ToString();
                    string Slip_no = dt.Rows[i]["Slip_no"].ToString();
                    string Amount = dt.Rows[i]["Amount"].ToString();
                    string studentname = dt.Rows[i]["studentname"].ToString();

                    string input = Date;  // DD/MM/YYYY
                    string FNsession = My.GetFinancialSessionFromString(input);
                    string Description = "Fee collection from " + studentname + " Amount : " + Amount + "/-";  //dt.Rows[i]["Description"].ToString();
                    string Type = "Student Fee Payment"; //dt.Rows[i]["Type"].ToString();//fee type
                    string mode = dt.Rows[i]["mode"].ToString();//payment type
                    string Session = FNsession;//payment type
                    string alternetacc_id = Addmission_no;
                    string unique_entry_id = My.unique_id();
                    string Bank_name = dt.Rows[i]["Bank_name"].ToString(); 
                    string VoucherNo = Slip_no; 
                    bool checkbiilentery = My.check_dup_bill_no_entry(VoucherNo, Session);
                    if(checkbiilentery==true)
                    {
                        if (mode.ToUpper() == "CASH")
                        {
                            My.send_to_cash_payment_Voucher_Details(alternetacc_id, "cash", Amount, VoucherNo, unique_entry_id, Description, Date, Idate, VoucherType, My.firm_id(), Session, user_id, "SCHOOL PAY", Type, Slip_no,"");
                        }
                        else
                        {
                            string toponebank = My.get_bank_id(Bank_name);
                            My.send_to_bank_payment_Voucher_Details(alternetacc_id, toponebank, Amount, VoucherNo, unique_entry_id, Description, Date, Idate, VoucherType, My.firm_id(), Session, user_id, "SCHOOL PAY", Type, Slip_no,"");
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