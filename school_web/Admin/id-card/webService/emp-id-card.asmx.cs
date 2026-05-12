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

namespace school_web.Admin.id_card.webService
{
    /// <summary>
    /// Summary description for emp_id_card
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class emp_id_card : System.Web.Services.WebService
    {
        //=======================================================
        public class Fatch_id_cards_for_employee
        {
            public string Aadhar_no { get; set; }
            public string Id_card_no { get; set; }
            public string Employee_Name { get; set; }
            public string Date_of_birth { get; set; }
            public string Blood_group { get; set; }
            public string Date_of_Joining { get; set; }
            public string Emp_Code { get; set; }
            public string Designation { get; set; }
            public string Mobile { get; set; }
            public string Address { get; set; }
            public string Employee_image { get; set; }
            public string idcard_template { get; set; }
            public string Sessions { get; set; }
            public string Template_back { get; set; }


            public string Email_Id { get; set; }
            public string Mobile_no { get; set; }
        }

        List<Fatch_id_cards_for_employee> Show_id_cards_for_employee = new List<Fatch_id_cards_for_employee>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_id_cards_details_for_employee(string UserType, string Emp_id, string Branch_id, string IdType)
        {
            string sessioN = My.get_session();
            string query = "";
            if (IdType == "CHECK")
            {
                query = "select *,(select top 1 User_Type from user_details where  user_id=t1.Emp_Code) as employee_type1, (select top 1 Template_filepath from Id_card_template_setting where Branch_id='" + Branch_id + "' and Type='Employee' and Front_back='Front') as idcard_template,(select top 1 Template_filepath from Id_card_template_setting where Branch_id='" + Branch_id + "' and Type='Employee' and Front_back='Back') as idcard_template_back  from PRL_Employee_Master t1 where t1.Id in(" + Emp_id + ") order by t1.Employee_Name asc";
            }
            else
            {
                if (IdType == "SINGLE")
                {
                    query = "select *,(select top 1 User_Type from user_details where  user_id=t1.Emp_Code) as employee_type1,(select top 1 Template_filepath from Id_card_template_setting where Branch_id='" + Branch_id + "' and Type='Employee' and Front_back='Front') as idcard_template,(select top 1 Template_filepath from Id_card_template_setting where Branch_id='" + Branch_id + "' and Type='Employee' and Front_back='Back') as idcard_template_back  from PRL_Employee_Master t1 where t1.Emp_Code='" + Emp_id + "' order by t1.Employee_Name asc";
                }
                else
                {
                    if (UserType == "ALL" && Emp_id == "ALL")
                    {
                        query = "select *,(select top 1 User_Type from user_details where  user_id=t1.Emp_Code) as employee_type1,(select top 1 Template_filepath from Id_card_template_setting where Branch_id='" + Branch_id + "' and Type='Employee' and Front_back='Front') as idcard_template,(select top 1 Template_filepath from Id_card_template_setting where Branch_id='" + Branch_id + "' and Type='Employee' and Front_back='Back') as idcard_template_back from PRL_Employee_Master t1 order by t1.Employee_Name asc";
                    }
                    else if (UserType != "ALL" && Emp_id == "ALL")
                    {
                        query = "select *,(select top 1 User_Type from user_details where  user_id=t1.Emp_Code) as employee_type1,(select top 1 Template_filepath from Id_card_template_setting where Branch_id='" + Branch_id + "' and Type='Employee' and Front_back='Front') as idcard_template,(select top 1 Template_filepath from Id_card_template_setting where Branch_id='" + Branch_id + "' and Type='Employee' and Front_back='Back') as idcard_template_back  from PRL_Employee_Master t1 where t1.employee_type='" + UserType + "' order by t1.Employee_Name asc";
                    }
                    else if (UserType == "ALL" && Emp_id != "ALL")
                    {
                        query = "select *,(select top 1 User_Type from user_details where  user_id=t1.Emp_Code) as employee_type1,(select top 1 Template_filepath from Id_card_template_setting where Branch_id='" + Branch_id + "' and Type='Employee' and Front_back='Front') as idcard_template,(select top 1 Template_filepath from Id_card_template_setting where Branch_id='" + Branch_id + "' and Type='Employee' and Front_back='Back') as idcard_template_back  from PRL_Employee_Master t1  where t1.Emp_Code='" + Emp_id + "' order by t1.Employee_Name asc";
                    }
                    else
                    {
                        query = "select *,(select top 1 User_Type from user_details where  user_id=t1.Emp_Code) as employee_type1,(select top 1 Template_filepath from Id_card_template_setting where Branch_id='" + Branch_id + "' and Type='Employee' and Front_back='Front') as idcard_template,(select top 1 Template_filepath from Id_card_template_setting where Branch_id='" + Branch_id + "' and Type='Employee' and Front_back='Back') as idcard_template_back  from PRL_Employee_Master t1 order by t1.Employee_Name asc";
                    }
                }
            }

            SqlDataAdapter ad = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Registration_db");
            DataTable dt = ds.Tables[0];
            int rowcount1 = dt.Rows.Count;
            if (rowcount1 == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DataTable dtUpdated = My.dataTable("select * from Id_card_update_employee where Employee_code='" + dr["Emp_Code"].ToString() + "'");
                    if (dtUpdated.Rows.Count > 0)
                    {
                        Show_id_cards_for_employee.Add(new Fatch_id_cards_for_employee
                        {
                            Aadhar_no = dtUpdated.Rows[0]["Aadhar_no"].ToString(),
                            Id_card_no = dtUpdated.Rows[0]["Id_card_no"].ToString(),
                            Employee_Name = dtUpdated.Rows[0]["Name"].ToString(),
                            Date_of_birth = dtUpdated.Rows[0]["Date_of_birth"].ToString(),
                            Date_of_Joining = dtUpdated.Rows[0]["Date_of_joining"].ToString(),
                            Emp_Code = dtUpdated.Rows[0]["Employee_code"].ToString(),
                            Designation = dtUpdated.Rows[0]["Department_name"].ToString(),
                            Mobile = dtUpdated.Rows[0]["Mobile_no"].ToString(),
                            Address = dtUpdated.Rows[0]["Address"].ToString(),
                            Employee_image = dr["Employee_image"].ToString(),
                            idcard_template = dr["idcard_template"].ToString(),
                            Blood_group = dtUpdated.Rows[0]["Blood_group"].ToString(),
                            Template_back = dr["idcard_template_back"].ToString(),
                            Email_Id = dtUpdated.Rows[0]["Email_id"].ToString(),
                            Mobile_no = dtUpdated.Rows[0]["Mobile_no"].ToString(),
                            Sessions = sessioN,
                        });
                    }
                    else
                    {
                        Show_id_cards_for_employee.Add(new Fatch_id_cards_for_employee
                        {
                            Aadhar_no = "",
                            Id_card_no = "",
                            Employee_Name = dr["Employee_Name"].ToString(),
                            Date_of_birth = dr["Date_of_birth"].ToString(),
                            Date_of_Joining = dr["Date_of_Joining"].ToString(),
                            Emp_Code = dr["Emp_Code"].ToString(),
                            Designation = dr["employee_type1"].ToString(),
                            Mobile = dr["Mobile"].ToString(),
                            Address = dr["Address"].ToString(),
                            Employee_image = dr["Employee_image"].ToString(),
                            idcard_template = dr["idcard_template"].ToString(),
                            Blood_group = dr["Blood_group"].ToString(),
                            Template_back = dr["idcard_template_back"].ToString(),
                            Email_Id = dr["Email"].ToString(),
                            Mobile_no = dr["Mobile"].ToString(),
                            Sessions = sessioN,
                        });
                    }
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_id_cards_for_employee));
            }
        }
    }
}
