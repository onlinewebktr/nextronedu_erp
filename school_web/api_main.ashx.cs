using Newtonsoft.Json;
using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using System.Web;

namespace school_web
{
    /// <summary>
    /// Summary description for api_main
    /// </summary>
    public class api_main : IHttpHandler
    { 

        public void ProcessRequest(HttpContext context)
        {
            string action = context.Request["action"];

            if (action == "UpdateStudentDues")
            {

                try
                {
                    var student = My.get_student_info_by_adm(context.Request["regid"].ToString());

                    string Session_id = student["Session_id"].ToString();
                    string Class_id = student["Class_id"].ToString();

                    bool flag = false;
                    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
                    {
                        SqlConnection con = new SqlConnection(My.conn);
                        con.Open();
                        dues_update_headwise_transaction.update_student_dues(Session_id, Class_id, context.Request["regid"].ToString(), "0", "0", con);
                        flag = true;
                        con.Close();
                        scope.Complete();
                    }


                    string qryy =
                    "select " +
                    "sum(convert(float, amount)) as Total_fee," +
                    "sum(convert(float, disc_amount)) as Total_disc," +
                    "sum(convert(float, Prev_paid)) as Total_paid," +
                    "sum(convert(float, Dues_amt)) as Total_dues " +
                    "from STUDENT_WISE_DUES_AMOUNT " +
                    "where Session_id='" + Session_id + "' " +
                    "and Class_id='" + Class_id + "' " +
                    "and admission_no='" + context.Request["regid"] + "'";

                    //My.submitException(qryy + "-----p4", "ata");
                    context.Response.Write(
 JsonConvert.SerializeObject(
 My.dataTable(qryy)
 ));

                }
                catch (Exception ex)
                {
                    // My.submitException(ex.ToString() + "-----p5","UpdateStudentDues");
                    context.Response.Write(
                   "{\"error\":true,\"status\":\"failed\"}"
                   );

                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }


    }
}