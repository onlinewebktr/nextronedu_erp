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
    public partial class Update_HMS_Inventory_Bill_Payment_Tracking : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        My mycode = new My();
        protected void btn_update_Click(object sender, EventArgs e)
        {
            string query = "select *,format(Date, 'dd/MM/yyyy') as Date2 from HMS_INVETORY_SELL_DETAILS_BILLWISE ";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string party_id = dt.Rows[i]["party_id"].ToString();
                    string Bill_No = dt.Rows[i]["Bill_No"].ToString();
                    string NetPayable = dt.Rows[i]["NetPayable"].ToString();
                    string Payment_Mode = dt.Rows[i]["Payment_Mode"].ToString();
                    string Payment_Remarks = dt.Rows[i]["Payment_Remarks"].ToString();
                    string Payment_TransactionId = dt.Rows[i]["Payment_TransactionId"].ToString();
                    string user_id = dt.Rows[i]["user_id"].ToString();
                    string Date = dt.Rows[i]["Date2"].ToString();

                    Update_admission_data(party_id, Bill_No, NetPayable, Payment_Mode, Payment_Remarks, Payment_TransactionId, user_id, Date);
                }
            }
        }

        private void Update_admission_data(string party_id, string Bill_No, string NetPayable, string Payment_Mode, string Payment_Remarks, string Payment_TransactionId, string user_id, string Date)
        {
            DateTime date_time = My.toDateTime(Sale_Purchase.toDateWithTime(Date));
            int idate = My.DateConvertToIdate(Date);
            DataTable dt = mycode.FillData("Select * from HMS_Inventory_Bill_Payment_Tracking where party_id='" + party_id + "' and Bill_No='" + Bill_No + "' ");
            if (dt.Rows.Count == 0)
            {
                SqlCommand cmd;
                string query = "INSERT INTO HMS_Inventory_Bill_Payment_Tracking (party_id,Bill_No,Payment_Vochar_id,Payable_Amount,Total_Paid_Amount,Duse_Amount,Received_from_Cash,Received_from_Bank,Bank_Payment_Mode,Date_time,Idate,Payment_transaction,Remarks,User_Id,Is_Settlement) values (@party_id,@Bill_No,@Payment_Vochar_id,@Payable_Amount,@Total_Paid_Amount,@Duse_Amount,@Received_from_Cash,@Received_from_Bank,@Bank_Payment_Mode,@Date_time,@Idate,@Payment_transaction,@Remarks,@User_Id,@Is_Settlement)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@party_id", party_id);
                cmd.Parameters.AddWithValue("@Bill_No", Bill_No);
                cmd.Parameters.AddWithValue("@Payment_Vochar_id", Bill_No);
                cmd.Parameters.AddWithValue("@Payable_Amount", My.toDouble(NetPayable).ToString("0.00"));
                cmd.Parameters.AddWithValue("@Total_Paid_Amount", My.toDouble(NetPayable).ToString("0.00"));
                cmd.Parameters.AddWithValue("@Duse_Amount", "0.00");

                if (Payment_Mode == "Cash")
                {
                    cmd.Parameters.AddWithValue("@Received_from_Cash", My.toDouble(NetPayable).ToString("0.00"));
                    cmd.Parameters.AddWithValue("@Received_from_Bank", "0.00");
                    cmd.Parameters.AddWithValue("@Bank_Payment_Mode", "Cash");
                    cmd.Parameters.AddWithValue("@Payment_transaction", "N/A");
                }
                else
                {

                    cmd.Parameters.AddWithValue("@Received_from_Cash", "0.00");
                    cmd.Parameters.AddWithValue("@Received_from_Bank", My.toDouble(NetPayable).ToString("0.00"));
                    cmd.Parameters.AddWithValue("@Bank_Payment_Mode", Payment_Mode);
                    cmd.Parameters.AddWithValue("@Payment_transaction", Payment_TransactionId);



                }


                cmd.Parameters.AddWithValue("@Date_time", date_time);
                cmd.Parameters.AddWithValue("@Idate", idate);
                cmd.Parameters.AddWithValue("@Remarks", Payment_Remarks);
                cmd.Parameters.AddWithValue("@User_Id", user_id);
                cmd.Parameters.AddWithValue("@Is_Settlement", 1);

                if (My.InsertUpdateData(cmd))
                {

                }
            }
            else
            {
                string id = dt.Rows[0]["id"].ToString();
                SqlCommand cmd;
                string query = "update HMS_Inventory_Bill_Payment_Tracking set Date_time=@Date_time,Idate=@Idate where Id";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Date_time", date_time);
                cmd.Parameters.AddWithValue("@Idate", idate);
                if (My.InsertUpdateData(cmd))
                {

                }
            }
        }
    }
}