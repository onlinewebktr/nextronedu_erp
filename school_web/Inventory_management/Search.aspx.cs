using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Inventory_management
{
    public partial class Search : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static List<object> TestNameAndCharges(string itemName)
        {
            List<object> itemResult = new List<object>();
            SqlConnection con = new SqlConnection(My.conn);
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string qry = @"select Test_Name,tm.Test_id,Test_Code,isnull(Charges,0) Charges  from dbo.[HMS_Test_Master] tm left join HMS_Test_charges tc on tm.Test_id=tc.Test_id and tc.Room_categoryid=0 where  Test_Name like '%'+@SearchprojectName+'%'";

                    cmd.CommandText = qry;
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchprojectName", itemName);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        itemResult.Add(new
                        {
                            Test_Name = dr["Test_Name"].ToString(),
                            Test_id = dr["Test_id"].ToString(),
                            Test_Code = dr["Test_Code"].ToString(),
                            Charges = dr["Charges"].ToString(),
                        });
                    }
                    con.Close();
                    return itemResult;
                }
            }
        }


        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static List<string> Search_Cilinical_provisional_diagnosis(string itemName)
        {
            List<string> itemResult = new List<string>();
            SqlConnection con = new SqlConnection(My.conn);
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string qry = @"select  Distinct Cilinical_provisional_diagnosis from dbo.[HMS_cilinical_provisional_diagnosis_Master] where  Cilinical_provisional_diagnosis like '%'+@SearchprojectName+'%'";

                    cmd.CommandText = qry;
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchprojectName", itemName);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        itemResult.Add(dr["Cilinical_provisional_diagnosis"].ToString());
                    }
                    con.Close();
                    return itemResult;
                }
            }
        }

        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static List<string> Search_all_lab_test(string itemName)
        {
            List<string> itemResult = new List<string>();
            SqlConnection con = new SqlConnection(My.conn);
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string qry = @"select  Test_Name as Test_Name from dbo.[HMS_Test_Master] where  Test_Name like '%'+@SearchprojectName+'%'";

                    cmd.CommandText = qry;
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchprojectName", itemName);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        itemResult.Add(dr["Test_Name"].ToString());
                    }
                    con.Close();
                    return itemResult;
                }
            }
        }

        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static List<string> Search_inventory_item(string itemName)
        {
            List<string> itemResult = new List<string>();
            SqlConnection con = new SqlConnection(My.conn);
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = " select Item_Name  from dbo.[HMS_Invetory_item_Master] where Item_Name LIKE ''+@SearchprojectName+'%' order by Item_Name asc";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchprojectName", itemName);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        itemResult.Add(dr["Item_Name"].ToString());
                    }
                    con.Close();
                    return itemResult;
                }
            }
        }

        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static List<string> Search_admitted_patient(string itemName)
        {
            List<string> itemResult = new List<string>();
            SqlConnection con = new SqlConnection(My.conn);
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = " select (Patient_Registration_no+'|'+Firstname) as ptname from dbo.[HMS_IPD_admition_details] t1 join HMS_Patient_registration t2  on t1.Patient_regno=t2.Patient_Registration_no where is_discharge=0 and (Firstname LIKE ''+@SearchprojectName+'%' or Patient_Registration_no LIKE '%'+@SearchprojectName+'%')";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchprojectName", itemName);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        itemResult.Add(dr["ptname"].ToString());
                    }
                    con.Close();
                    return itemResult;
                }
            }
        }


        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static List<string> Search_supplier_name(string itemName)
        {
            List<string> itemResult = new List<string>();
            SqlConnection con = new SqlConnection(My.conn);
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string qry = @"select party_name+', Mob.-'+mobile  as party_name  from party_details where type='Supplier' and party_name like '%'+@SearchprojectName+'%' ";
                    cmd.CommandText = qry;
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchprojectName", itemName);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        itemResult.Add(dr["party_name"].ToString());
                    }
                    con.Close();
                    return itemResult;
                }
            }
        }



        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static List<string> Search_txt_area(string itemName)
        {
            List<string> itemResult = new List<string>();
            SqlConnection con = new SqlConnection(My.conn);
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string qry = @"(select distinct Area from HMS_Patient_registration where Area like '%'+@SearchprojectName+'%') Union All (select distinct VIllage_name from HMS_Village_Master where VIllage_name like '%'+@SearchprojectName+'%')";
                    cmd.CommandText = qry;
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchprojectName", itemName);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        itemResult.Add(dr["Area"].ToString());
                    }
                    con.Close();
                    return itemResult;
                }
            }
        }

        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static List<string> Search_txt_village(string itemName)
        {
            List<string> itemResult = new List<string>();
            SqlConnection con = new SqlConnection(My.conn);
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string qry = @"select distinct VIllage_name from HMS_Village_Master where VIllage_name like '%'+@SearchprojectName+'%'";
                    cmd.CommandText = qry;
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchprojectName", itemName);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        itemResult.Add(dr["VIllage_name"].ToString());
                    }
                    con.Close();
                    return itemResult;
                }
            }
        }


        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static List<string> Search_cause_of_injury(string itemName)
        {
            List<string> itemResult = new List<string>();
            SqlConnection con = new SqlConnection(My.conn);
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = " select distinct Cause_of_injury from dbo.[HMS_Patient_Police_info]  where  Cause_of_injury LIKE ''+@SearchprojectName+'%' ";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchprojectName", itemName);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        itemResult.Add(dr["Cause_of_injury"].ToString());
                    }
                    con.Close();
                    return itemResult;
                }
            }
        }

        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static List<string> Search_patient_condition(string itemName)
        {
            List<string> itemResult = new List<string>();
            SqlConnection con = new SqlConnection(My.conn);
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = " select distinct Patient_condition from  dbo.[HMS_Patient_Police_info]  where  Patient_condition LIKE ''+@SearchprojectName+'%' ";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchprojectName", itemName);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        itemResult.Add(dr["Patient_condition"].ToString());
                    }
                    con.Close();
                    return itemResult;
                }
            }
        }


        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static List<string> searching_by_ledger_name(string Ledgername)
        {
            List<string> itemResult = new List<string>();
            SqlConnection con = new SqlConnection(My.conn);
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = " select distinct isnull(Account_Name+','+pd.mobile,Account_Name) as Account,Account_Name from dbo.[Account_Ledger_Details] ald left join party_details pd on ald.firm=pd.firm and ald.Account_id=pd.party_id  where  ald.firm='" + My.get_firm_id() + "' and Account_Name LIKE ''+@SearchprojectName+'%' ";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchprojectName", Ledgername);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        itemResult.Add(dr["Account_Name"].ToString());
                    }
                    con.Close();
                    return itemResult;
                }
            }
        }


        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static List<string> search_genric_name(string itemName)
        {
            List<string> itemResult = new List<string>();
            SqlConnection con = new SqlConnection(My.conn);
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select Generic_Name from dbo.[Generic_Name_Master] where Generic_Name LIKE ''+@SearchprojectName+'%'";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchprojectName", itemName);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        itemResult.Add(dr["Generic_Name"].ToString());
                    }
                    con.Close();
                    return itemResult;
                }
            }
        }



        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static List<string> Search_suppliername(string itemName)
        {
            List<string> itemResult = new List<string>();
            SqlConnection con = new SqlConnection(My.conn);
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select party_name+','+address+','+mobile  as Description from dbo.[party_details] where party_name LIKE ''+@SearchprojectName+'%' and type='Supplier'";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchprojectName", itemName);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        itemResult.Add(dr["Description"].ToString());
                    }
                    con.Close();
                    return itemResult;
                }
            }
        }

        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static List<string> Search_itemname(string itemName)
        {
            List<string> itemResult = new List<string>();
            SqlConnection con = new SqlConnection(My.conn);
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select Description+', Brand : '+brand  as Description from dbo.[item_master] where Description LIKE ''+@SearchprojectName+'%' and firm='" + My.get_firm_id() + "'";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchprojectName", itemName);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        itemResult.Add(dr["Description"].ToString());
                    }
                    con.Close();
                    return itemResult;
                }
            }
        }

        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static List<string> Search_customer_name(string itemName)
        {
            List<string> itemResult = new List<string>();
            SqlConnection con = new SqlConnection(My.conn);
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct party_name+'|'+party_id from dbo.[party_details] where party_name LIKE ''+@SearchprojectName+'%' and firm='" + My.get_firm_id() + "'";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchprojectName", itemName);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        itemResult.Add(dr["party_name"].ToString());
                    }
                    con.Close();
                    return itemResult;
                }
            }
        }


        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static List<string> Search_pharmecy_item_name(string itemName)
        {
            List<string> itemResult = new List<string>();
            SqlConnection con = new SqlConnection(My.conn);
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct Description from dbo.[item_master] where Description LIKE ''+@SearchprojectName+'%'";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchprojectName", itemName);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        itemResult.Add(dr["Description"].ToString());
                    }
                    con.Close();
                    return itemResult;
                }
            }
        }


        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static List<string> Search_pharmecy_group_name(string itemName)
        {
            List<string> itemResult = new List<string>();
            SqlConnection con = new SqlConnection(My.conn);
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct Category_Name from dbo.[Category_Details] where firm='" + My.get_firm_id() + "' and Category_Name LIKE ''+@SearchprojectName+'%'";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchprojectName", itemName);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        itemResult.Add(dr["Category_Name"].ToString());
                    }
                    con.Close();
                    return itemResult;
                }
            }
        }


        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static List<string> Search_pharmecy_serial_no(string itemName)
        {
            List<string> itemResult = new List<string>();
            SqlConnection con = new SqlConnection(My.conn);
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct serial_no from dbo.[purchase_entry_billwise] where firm='" + My.get_firm_id() + "' and session='" + My.get_session() + "' and serial_no LIKE ''+@SearchprojectName+'%'";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchprojectName", itemName);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        itemResult.Add(dr["serial_no"].ToString());
                    }
                    con.Close();
                    return itemResult;
                }
            }
        }


        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static List<string> search_Medicine_note(string itemName)
        {
            List<string> itemResult = new List<string>();
            SqlConnection con = new SqlConnection(My.conn);
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select  Notes from HMS_Medicine_note where Notes LIKE ''+@SearchprojectName+'%'";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchprojectName", itemName);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        itemResult.Add(dr["Notes"].ToString());
                    }
                    con.Close();
                    return itemResult;
                }
            }
        }
        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static List<string> search_hospital_Medicinename(string itemName)
        {
            List<string> itemResult = new List<string>();
            SqlConnection con = new SqlConnection(My.conn);
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select  Item_Name from HMS_Medicine_Master where Item_Name LIKE ''+@SearchprojectName+'%'";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchprojectName", itemName);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        itemResult.Add(dr["Item_Name"].ToString());
                    }
                    con.Close();
                    return itemResult;
                }
            }
        }

        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static List<string> search_All_Medicinename(string itemName)
        {
            List<string> itemResult = new List<string>();
            SqlConnection con = new SqlConnection(My.conn);
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select  Description from item_master where Description LIKE ''+@SearchprojectName+'%'";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchprojectName", itemName);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        itemResult.Add(dr["Description"].ToString());
                    }
                    con.Close();
                    return itemResult;
                }
            }
        }

        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static List<string> Search_by_disease(string itemName)
        {
            List<string> itemResult = new List<string>();
            SqlConnection con = new SqlConnection(My.conn);
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select Disease_Name from HMS_Disease_master where  Disease_Name LIKE ''+@SearchprojectName+'%'";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchprojectName", itemName);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        itemResult.Add(dr["Disease_Name"].ToString());
                    }
                    con.Close();
                    return itemResult;
                }
            }
        }


        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static List<string> search_Medicinename(string genericname, string itemName)
        {
            List<string> itemResult = new List<string>();
            SqlConnection con = new SqlConnection(My.conn);
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select  Description from item_master where generic_name='" + genericname + "' and Description LIKE ''+@SearchprojectName+'%'";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchprojectName", itemName);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        itemResult.Add(dr["Description"].ToString());
                    }
                    con.Close();
                    return itemResult;
                }
            }
        }


        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static List<string> Search_operation_name(string itemName, string heading)
        {
            List<string> itemResult = new List<string>();
            SqlConnection con = new SqlConnection(My.conn);
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select * from HMS_Operation_Name_master where Operation_head_id='" + heading + "' and Operation_name LIKE ''+@SearchprojectName+'%'";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchprojectName", itemName);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        itemResult.Add(dr["Operation_name"].ToString());
                    }
                    con.Close();
                    return itemResult;
                }
            }
        }



        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static List<string> search_chief_complaint(string itemName)
        {
            List<string> itemResult = new List<string>();
            SqlConnection con = new SqlConnection(My.conn);
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select  Chief_complaint from HMS_Chief_complaints where Chief_complaint LIKE ''+@SearchprojectName+'%'";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchprojectName", itemName);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        itemResult.Add(dr["Chief_complaint"].ToString());
                    }
                    con.Close();
                    return itemResult;
                }
            }
        }

        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static List<string> search_txt_Allergiestype(string itemName)
        {
            List<string> itemResult = new List<string>();
            SqlConnection con = new SqlConnection(My.conn);
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select  Allergy_Type from HMS_Allergy_Type where Allergy_Type LIKE ''+@SearchprojectName+'%'";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchprojectName", itemName);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        itemResult.Add(dr["Allergy_Type"].ToString());
                    }
                    con.Close();
                    return itemResult;
                }
            }
        }

        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static List<string> search_txt_AllergiesITEM(string itemName)
        {
            List<string> itemResult = new List<string>();
            SqlConnection con = new SqlConnection(My.conn);
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select  Allergy_Item from HMS_Allergy_Item where Allergy_Item LIKE ''+@SearchprojectName+'%'";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchprojectName", itemName);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        itemResult.Add(dr["Allergy_Item"].ToString());
                    }
                    con.Close();
                    return itemResult;
                }
            }
        }


        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static List<string> search_txt_Allergiesname(string itemName)
        {
            List<string> itemResult = new List<string>();
            SqlConnection con = new SqlConnection(My.conn);
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select  Allergy_name from HMS_Allergy_name where Allergy_name LIKE ''+@SearchprojectName+'%'";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchprojectName", itemName);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        itemResult.Add(dr["Allergy_name"].ToString());
                    }
                    con.Close();
                    return itemResult;
                }
            }
        }


        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static List<string> Search_OT_by_name_and_reg(string itemName)
        {
            List<string> itemResult = new List<string>();
            SqlConnection con = new SqlConnection(My.conn);
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = " select (Patient_Registration_no+'|'+Firstname) as ptname from dbo.[HMS_IPD_admition_details] t1 join HMS_Patient_registration t2  on t1.Patient_regno=t2.Patient_Registration_no where is_discharge=0 and t1.Patient_regno not in (select Patient_id from dbo.[HMS_Operation_schedule] where Status in('Process','Pending')) and (Firstname LIKE ''+@SearchprojectName+'%' or Patient_Registration_no LIKE '%'+@SearchprojectName+'%')";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchprojectName", itemName);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        itemResult.Add(dr["ptname"].ToString());
                    }
                    con.Close();
                    return itemResult;
                }
            }
        }


        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static List<string> Search_by_name_and_reg(string itemName)
        {
            List<string> itemResult = new List<string>();
            SqlConnection con = new SqlConnection(My.conn);
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "select (Patient_Registration_no+'|'+Firstname) as ptname from HMS_Patient_registration where Firstname LIKE ''+@SearchprojectName+'%' or Patient_Registration_no LIKE '%'+@SearchprojectName+'%' OR Mobile_no LIKE '%'+@SearchprojectName+'%'";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchprojectName", itemName);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        itemResult.Add(dr["ptname"].ToString());
                    }
                    con.Close();
                    return itemResult;
                }
            }
        }



        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static List<string> Search_by_name_and_reg_help(string itemName, string docotor)
        {
            List<string> itemResult = new List<string>();
            SqlConnection con = new SqlConnection(My.conn);
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string qry = "select (Patient_Registration_no+'|'+Firstname+'|'+Mobile_no) as ptname from HMS_Patient_registration where Firstname LIKE ''+@SearchprojectName+'%' or Patient_Registration_no LIKE '%'+@SearchprojectName+'%' OR Mobile_no LIKE '%'+@SearchprojectName+'%'";
                    if (docotor != "0")
                        qry = "Select * from (select Patient_Registration_no,(Patient_Registration_no+'|'+Firstname+'|'+Mobile_no) as ptname from HMS_Patient_registration where Firstname LIKE ''+@SearchprojectName+'%' or Patient_Registration_no LIKE '%'+@SearchprojectName+'%' OR Mobile_no LIKE '%'+@SearchprojectName+'%' ) T where Patient_Registration_no in ( select Patient_id from dbo.[HMS_OPD_appointment_tb] where Doctor_id ='" + docotor + "')"; ;

                    cmd.CommandText = qry;
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchprojectName", itemName);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        itemResult.Add(dr["ptname"].ToString());
                    }
                    con.Close();
                    return itemResult;
                }
            }
        }


        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static List<string> Search_doctor(string itemName)
        {
            List<string> itemResult = new List<string>();
            SqlConnection con = new SqlConnection(My.conn);
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string qry = @"select * from (select Initial_name+' '+Name as 'doctorname' from (SELECT (Select top 1 Initial_name from HMS_Initial_Master where Initial_id=t1.Initial_id) as Initial_name,(Select Dept_name from HMS_Department_Master where Dept_id=t1.Dept_id) as Dept_name,(Select Desg_name from HMS_Designation_Master where Desg_id=t1.Desg_id) as Desg_name,* FROM HMS_Employee_Details t1 where   Type='Consultant') T1 ) T2  where doctorname like '%'+@SearchprojectName+'%'";

                    cmd.CommandText = qry;
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchprojectName", itemName);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        itemResult.Add(dr["doctorname"].ToString());
                    }
                    con.Close();
                    return itemResult;
                }
            }
        }

        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static List<string> Search_lab_dctor(string itemName)
        {
            List<string> itemResult = new List<string>();
            SqlConnection con = new SqlConnection(My.conn);
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string qry = @"select * from (select Initial_name+' '+Name as 'doctorname',* from (SELECT ISNULL((Select top 1 Initial_name from HMS_Initial_Master where Initial_id=t1.Initial_id),'') as Initial_name,(Select Dept_name from HMS_Department_Master where Dept_id=t1.Dept_id) as Dept_name,(Select Desg_name from HMS_Designation_Master where Desg_id=t1.Desg_id) as Desg_name,* FROM HMS_Employee_Details t1 where Type in ('Consultant','Anesthesia') and Dept_id in(select Dept_id from HMS_Department_Master where IsLab=1)) T1 ) T2 where doctorname like '%'+@SearchprojectName+'%'";

                    cmd.CommandText = qry;
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchprojectName", itemName);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        itemResult.Add(dr["doctorname"].ToString());
                    }
                    con.Close();
                    return itemResult;
                }
            }
        }


        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static List<string> Search_lab_test(string itemName)
        {
            List<string> itemResult = new List<string>();
            SqlConnection con = new SqlConnection(My.conn);
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string qry = @"select  Test_Name as Test_Name from dbo.[HMS_Test_Master] where  Test_Name like '%'+@SearchprojectName+'%' and Heading_id   in ( select Heading_id from dbo.[HMS_Lab_heading] where Lab_typeid=1) ";

                    cmd.CommandText = qry;
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchprojectName", itemName);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        itemResult.Add(dr["Test_Name"].ToString());
                    }
                    con.Close();
                    return itemResult;
                }
            }
        }

        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static List<string> Search_radiaology_lab_test(string itemName, string lab_test_heading)
        {
            List<string> itemResult = new List<string>();
            SqlConnection con = new SqlConnection(My.conn);
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string qry = "";
                    if (lab_test_heading == "0")
                    {
                        qry = @"select  Test_Name as Test_Name from dbo.[HMS_Test_Master] where  Test_Name like '%'+@SearchprojectName+'%'   and Heading_id   in ( select Heading_id from dbo.[HMS_Lab_heading] where Lab_typeid=2) ";
                    }
                    else
                    {
                        qry = @"select  Test_Name as Test_Name from dbo.[HMS_Test_Master] where  Test_Name like '%'+@SearchprojectName+'%'  and Heading_id='" + lab_test_heading + "'";
                    }
                    cmd.CommandText = qry;
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchprojectName", itemName);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        itemResult.Add(dr["Test_Name"].ToString());
                    }
                    con.Close();
                    return itemResult;
                }
            }
        }


        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static List<string> Search_outsource_lab_test(string itemName)
        {
            List<string> itemResult = new List<string>();
            SqlConnection con = new SqlConnection(My.conn);
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string qry = @"select Test_Name as Test_Name from dbo.[HMS_Test_Master] where Test_Name like '%'+@SearchprojectName+'%' and Test_id in ( select Test_id from dbo.[HMS_Out_source_test])";

                    cmd.CommandText = qry;
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchprojectName", itemName);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        itemResult.Add(dr["Test_Name"].ToString());
                    }
                    con.Close();
                    return itemResult;
                }
            }
        }



        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static List<string> Search_reffral_doctor(string itemName)
        {
            List<string> itemResult = new List<string>();
            SqlConnection con = new SqlConnection(My.conn);
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string qry = @"select distinct Doctorname from HMS_Referral_doctor_details where Doctorname like '%'+@SearchprojectName+'%'";

                    cmd.CommandText = qry;
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchprojectName", itemName);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        itemResult.Add(dr["Doctorname"].ToString());
                    }
                    con.Close();
                    return itemResult;
                }
            }
        }

        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static List<string> Search_reffral_patho_doctor(string itemName)
        {
            List<string> itemResult = new List<string>();
            SqlConnection con = new SqlConnection(My.conn);
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string qry = @"select distinct Name from Referral_details where Name like '%'+@SearchprojectName+'%'";

                    cmd.CommandText = qry;
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchprojectName", itemName);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        itemResult.Add(dr["Name"].ToString());
                    }
                    con.Close();
                    return itemResult;
                }
            }
        }

        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static List<string> Search_by_name_and_reg_for_consumption(string itemName, string roomcat, string Consumpted_by)
        {
            List<string> itemResult = new List<string>();

            SqlConnection con = new SqlConnection(My.conn);
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    if (Consumpted_by == "Patient")
                    {
                        if (roomcat == "IPD")
                        {
                            cmd.CommandText = "select (Patient_Registration_no+'|'+Firstname+' '+Middlename+' '+Lastname) as ptname from dbo.[HMS_IPD_admit_details] T1 Join HMS_Patient_registration T2 On T1.Patient_regno=T2.Patient_Registration_no where Room_categoryid in( select Room_categoryid from dbo.[HMS_Room_Category]) and  (Firstname LIKE ''+@SearchprojectName+'%' or Patient_Registration_no LIKE '%'+@SearchprojectName+'%')";
                        }
                        else if (roomcat == "EMERGENCY")
                        {
                            cmd.CommandText = "select (Patient_Registration_no+'|'+Firstname+' '+Middlename+' '+Lastname) as ptname from dbo.[HMS_emergency_appointment_tb] T1 Join HMS_Patient_registration T2 On T1.Patient_id=T2.Patient_Registration_no where is_bill_clear=0 and is_bill_cancel=0 and (Firstname LIKE ''+@SearchprojectName+'%' or Patient_Registration_no LIKE ''+@SearchprojectName+'%')";
                        }
                        else if (roomcat == "OT")
                        {
                            cmd.CommandText = "select (Patient_Registration_no+'|'+Firstname+' '+Middlename+' '+Lastname) as ptname from dbo.[HMS_OT_admition_details] T1 Join HMS_Patient_registration T2 On T1.Patient_id=T2.Patient_Registration_no where (Firstname LIKE ''+@SearchprojectName+'%' or Patient_Registration_no LIKE ''+@SearchprojectName+'%')";
                        }
                        else if (roomcat == "OPD")
                        {
                            cmd.CommandText = "select (Patient_Registration_no+'|'+Firstname+' '+Middlename+' '+Lastname) as ptname from dbo.[HMS_OPD_appointment_tb] T1 Join HMS_Patient_registration T2 On T1.Patient_id=T2.Patient_Registration_no where  (Firstname LIKE ''+@SearchprojectName+'%' or Patient_Registration_no LIKE ''+@SearchprojectName+'%')";
                        }
                        else if (roomcat == "LAB")
                        {
                            cmd.CommandText = "select (Patient_Registration_no+'|'+Firstname+' '+Middlename+' '+Lastname) as ptname from dbo.[HMS_OPD_appointment_tb] T1 Join HMS_Patient_registration T2 On T1.Patient_id=T2.Patient_Registration_no where  (Firstname LIKE ''+@SearchprojectName+'%' or Patient_Registration_no LIKE ''+@SearchprojectName+'%')";
                        }
                        else
                        {
                            cmd.CommandText = "select (Patient_Registration_no+'|'+Firstname) as ptname from HMS_Patient_registration T2 where  (Firstname LIKE ''+@SearchprojectName+'%' or Patient_Registration_no LIKE ''+@SearchprojectName+'%')";

                        }
                    }
                    if (Consumpted_by == "Staff")
                    {
                        cmd.CommandText = "Select (Employee_id+'|'+Name) as ptname from dbo.[HMS_Employee_Details] T1  where Name LIKE ''+@SearchprojectName+'%'";
                    }
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchprojectName", itemName);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        itemResult.Add(dr["ptname"].ToString());
                    }
                    con.Close();
                    return itemResult;
                }
            }
        }
        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static List<string> Search_by_expense_head(string itemName)
        {
            List<string> itemResult = new List<string>();
            SqlConnection con = new SqlConnection(My.conn);
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct Expense_head from HMS_Expense_Entry where  Expense_head LIKE ''+@SearchprojectName+'%'";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchprojectName", itemName);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        itemResult.Add(dr["Expense_head"].ToString());
                    }
                    con.Close();
                    return itemResult;
                }
            }
        }



        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static List<string> Search_general_patient(string itemName)
        {
            List<string> itemResult = new List<string>();
            SqlConnection con = new SqlConnection(My.conn);
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select  Firstname from HMS_General_Patient_registration where Firstname LIKE ''+@SearchprojectName+'%'";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchprojectName", itemName);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        itemResult.Add(dr["Firstname"].ToString());
                    }
                    con.Close();
                    return itemResult;
                }
            }
        }

        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static List<string> search_eye_chief_complaint(string itemName)
        {
            List<string> itemResult = new List<string>();
            SqlConnection con = new SqlConnection(My.conn);
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select  complaints from HMS_Patient_initial_finding_for_eye_care where complaints LIKE ''+@SearchprojectName+'%'";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchprojectName", itemName);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        itemResult.Add(dr["complaints"].ToString());
                    }
                    con.Close();
                    return itemResult;
                }
            }
        }
        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static List<string> search_eye_systemic_illness(string itemName)
        {
            List<string> itemResult = new List<string>();
            SqlConnection con = new SqlConnection(My.conn);
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select  systemic_illness from HMS_Patient_initial_finding_for_eye_care where systemic_illness LIKE ''+@SearchprojectName+'%'";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchprojectName", itemName);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        itemResult.Add(dr["systemic_illness"].ToString());
                    }
                    con.Close();
                    return itemResult;
                }
            }
        }
        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static List<string> search_eye_past_history(string itemName)
        {
            List<string> itemResult = new List<string>();
            SqlConnection con = new SqlConnection(My.conn);
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select  past_history from HMS_Patient_initial_finding_for_eye_care where past_history LIKE ''+@SearchprojectName+'%'";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchprojectName", itemName);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        itemResult.Add(dr["past_history"].ToString());
                    }
                    con.Close();
                    return itemResult;
                }
            }
        }
        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static List<string> search_eye_diagnosis(string itemName)
        {
            List<string> itemResult = new List<string>();
            SqlConnection con = new SqlConnection(My.conn);
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select  diagnosis from HMS_Patient_initial_finding_for_eye_care where diagnosis LIKE ''+@SearchprojectName+'%'";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchprojectName", itemName);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        itemResult.Add(dr["diagnosis"].ToString());
                    }
                    con.Close();
                    return itemResult;
                }
            }
        }
        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static List<string> search_eye_advice(string itemName)
        {
            List<string> itemResult = new List<string>();
            SqlConnection con = new SqlConnection(My.conn);
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select advice from HMS_Patient_initial_finding_for_eye_care where advice LIKE ''+@SearchprojectName+'%'";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchprojectName", itemName);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        itemResult.Add(dr["advice"].ToString());
                    }
                    con.Close();
                    return itemResult;
                }
            }
        }


        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static List<string> Search_surgery_etimation_pname(string itemName, string heading)
        {
            List<string> itemResult = new List<string>();
            SqlConnection con = new SqlConnection(My.conn);
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select * from HMS_ESTIMATION_COST_FOR_SURGERY where  P_name LIKE ''+@SearchprojectName+'%'";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchprojectName", itemName);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        itemResult.Add(dr["P_name"].ToString());
                    }
                    con.Close();
                    return itemResult;
                }
            }
        }

        //For Medicine 

        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static List<string> Search_pharmacy_doctor_name(string itemName)
        {
            List<string> itemResult = new List<string>();
            SqlConnection con = new SqlConnection(My.conn);
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "  select distinct DoctorName from dbo.[Doctor_Details] where DoctorName like '%'+@SearchprojectName+'%'";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchprojectName", itemName);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        itemResult.Add(dr["DoctorName"].ToString());
                    }
                    con.Close();
                    return itemResult;
                }
            }
        }

        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static List<string> Search_pharmacy_patient(string itemName)
        {
            List<string> itemResult = new List<string>();
            SqlConnection con = new SqlConnection(My.conn);
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "select top 50 party_name from dbo.[party_details] where type='Customer' and party_name like '%'+@SearchprojectName+'%' ";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchprojectName", itemName);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        itemResult.Add(dr["party_name"].ToString());
                    }
                    con.Close();
                    return itemResult;
                }
            }
        }
    }
}