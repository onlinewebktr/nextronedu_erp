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
    /// Summary description for id_card_new
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class id_card_new : System.Web.Services.WebService
    {

        public class Fatch_all_vertical_id_card
        {
            public string Admission_no { get; set; }
            public string photo { get; set; }
            public string name { get; set; }
            public string father_name { get; set; }
            public string Mothername { get; set; }
            public string sclass { get; set; }
            public string roll_no { get; set; }
            public string dob { get; set; }
            public string Blood_group { get; set; }
            public string address { get; set; }
            public string mobile { get; set; }
            public string Mother_mob { get; set; }
            public string idcard_template { get; set; }
            public string sec { get; set; }
            public string gender { get; set; }
            public string So_or_Do { get; set; }
            public string Transports { get; set; }
            public string Session { get; set; }
            public string InfotxtColor { get; set; }

            public string Father_img { get; set; }
            public string Mother_img { get; set; }

            public string Father_mob { get; set; }
            public string Mother_mobBD { get; set; }
            public string House_name { get; set; }
        }

        List<Fatch_all_vertical_id_card> Show_all_vertical_id_card = new List<Fatch_all_vertical_id_card>();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void fetch_all_verticle_id_card_details(string Session_id, string Class_id, string Sections, string Branch_id, string IdType, string Admission_no)
        {
            string query = "";
            if (IdType == "CHECK")
            {
                query = "select *,(select top 1 Template_filepath from Id_card_template_setting where Branch_id='" + Branch_id + "' and Type='Student') as idcard_template,(select top 1 Image_path from Student_image_new where Admission_no=admission_registor.admissionserialnumber and Image_type='Mother_image') as Mother_img,(select top 1 Image_path from Student_image_new where Admission_no=admission_registor.admissionserialnumber and Image_type='Father_image') as Father_img,isnull((select top 1 house_name from house_master where house_id=admission_registor.house),'NA') as House_name from admission_registor where Session_Id='" + Session_id + "' and Class_id='" + Class_id + "' and Branch_id='" + Branch_id + "' and Status='1' and Id in(" + Admission_no + ")";
            }
            else
            {
                if (IdType == "BULK")
                {
                    if (Sections == "ALL")
                    {
                        query = "select *,(select top 1 Template_filepath from Id_card_template_setting where Branch_id='" + Branch_id + "' and Type='Student') as idcard_template,(select top 1 Image_path from Student_image_new where Admission_no=admission_registor.admissionserialnumber and Image_type='Mother_image') as Mother_img,(select top 1 Image_path from Student_image_new where Admission_no=admission_registor.admissionserialnumber and Image_type='Father_image') as Father_img,isnull((select top 1 house_name from house_master where house_id=admission_registor.house),'NA') as House_name from admission_registor where Session_Id='" + Session_id + "' and Class_id='" + Class_id + "' and Branch_id='" + Branch_id + "' and Status='1'";
                    }
                    else
                    {
                        query = "select *,(select top 1 Template_filepath from Id_card_template_setting where Branch_id='" + Branch_id + "' and Type='Student') as idcard_template,(select top 1 Image_path from Student_image_new where Admission_no=admission_registor.admissionserialnumber and Image_type='Mother_image') as Mother_img,(select top 1 Image_path from Student_image_new where Admission_no=admission_registor.admissionserialnumber and Image_type='Father_image') as Father_img,isnull((select top 1 house_name from house_master where house_id=admission_registor.house),'NA') as House_name from admission_registor where Session_Id='" + Session_id + "' and Class_id='" + Class_id + "' and Branch_id='" + Branch_id + "' and Section='" + Sections + "' and Status='1'";
                    }
                }
                else
                {
                    query = "select *,(select top 1 Template_filepath from Id_card_template_setting where Branch_id='" + Branch_id + "' and Type='Student') as idcard_template,(select top 1 Image_path from Student_image_new where Admission_no=admission_registor.admissionserialnumber and Image_type='Mother_image') as Mother_img,(select top 1 Image_path from Student_image_new where Admission_no=admission_registor.admissionserialnumber and Image_type='Father_image') as Father_img,isnull((select top 1 house_name from house_master where house_id=admission_registor.house),'NA') as House_name from admission_registor where admissionserialnumber='" + Admission_no + "' and Session_Id='" + Session_id + "' and Class_id='" + Class_id + "' and Branch_id='" + Branch_id + "' and Status='1'";
                }
            }

            string firmId = My.get_single_column_data("select firm_id as Column_Name from Firm_Details");
            string infotxtColor = "#ffffff";
            if (firmId == "STJ-01")
            {
                infotxtColor = "#000000 !important;";
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
                    string transportationtaken = "SELF";
                    string gender = dr["gender"].ToString();

                    string so_do;
                    if (gender.ToUpper() == "FEMALE")
                    {
                        so_do = "D/O";
                    }
                    else
                    {
                        so_do = "S/O";
                    }
                    if (dr["transportationtaken"].ToString().ToUpper() == "YES")
                    {
                        transportationtaken = "VAN";
                    }


                    string mommoB = " / " + dr["mother_mob"].ToString();
                    if (dr["mother_mob"].ToString() == "")
                    {
                        mommoB = "";
                    }

                    DataTable dtUpdated = My.dataTable("select *,Mother_full_name as mothername, Mobile_No_mother as mother_mob from Id_card_updated_student where Session_id='" + dr["Session_Id"].ToString() + "' and Admission_no='" + dr["admissionserialnumber"].ToString() + "' order by Id desc");
                    if (dtUpdated.Rows.Count > 0)
                    {
                        mommoB = " / " + dtUpdated.Rows[0]["mother_mob"].ToString();
                        if (dtUpdated.Rows[0]["Transport_taken"].ToString().ToUpper() == "YES")
                        {
                            try
                            {
                                transportationtaken = dtUpdated.Rows[0]["Transport_type"].ToString();
                            }
                            catch (Exception ex)
                            {
                            }


                            if (transportationtaken == "")
                            {
                                transportationtaken = "VAN";
                            }
                        }

                        if (dtUpdated.Rows[0]["mother_mob"].ToString() == "")
                        {
                            mommoB = "";
                        }

                        Show_all_vertical_id_card.Add(new Fatch_all_vertical_id_card
                        {
                            Admission_no = dr["admissionserialnumber"].ToString(),
                            photo = dr["studentimagepath"].ToString(),
                            name = dtUpdated.Rows[0]["Name"].ToString(),
                            father_name = dtUpdated.Rows[0]["Father_name"].ToString(),
                            Mothername = dtUpdated.Rows[0]["mothername"].ToString(),
                            sclass = dtUpdated.Rows[0]["Class_name"].ToString(),
                            sec = dtUpdated.Rows[0]["Section"].ToString(),
                            roll_no = dtUpdated.Rows[0]["Roll_no"].ToString(),
                            dob = dtUpdated.Rows[0]["Date_of_birth"].ToString(),
                            Blood_group = dr["blood_group"].ToString(),
                            address = dtUpdated.Rows[0]["Address"].ToString(),
                            mobile = dtUpdated.Rows[0]["Mobile_no"].ToString(),
                            Mother_mob = mommoB,
                            idcard_template = dr["idcard_template"].ToString(),
                            So_or_Do = so_do,
                            Transports = transportationtaken,
                            Session = "Session : " + dr["session"].ToString(),
                            InfotxtColor = infotxtColor,

                            Father_img = dr["Father_img"].ToString(),
                            Mother_img = dr["Mother_img"].ToString(), 
                            Father_mob = dtUpdated.Rows[0]["Mobile_no"].ToString(),
                            Mother_mobBD = dtUpdated.Rows[0]["mother_mob"].ToString(),
                            House_name = "NA",
                        });
                    }
                    else
                    {
                        Show_all_vertical_id_card.Add(new Fatch_all_vertical_id_card
                        {
                            Admission_no = dr["admissionserialnumber"].ToString(),
                            photo = dr["studentimagepath"].ToString(),
                            name = dr["studentname"].ToString(),
                            father_name = dr["fathername"].ToString(),
                            Mothername = dr["mothername"].ToString(),
                            sclass = dr["class"].ToString(),
                            sec = dr["Section"].ToString(),
                            roll_no = dr["rollnumber"].ToString(),
                            dob = dr["dob"].ToString(),
                            Blood_group = dr["blood_group"].ToString(),
                            address = dr["careof"].ToString(),
                            mobile = dr["mobilenumber"].ToString(),
                            Mother_mob = mommoB,
                            idcard_template = dr["idcard_template"].ToString(),
                            So_or_Do = so_do,
                            Transports = transportationtaken,
                            Session = "Session : " + dr["session"].ToString(),
                            InfotxtColor= infotxtColor,

                            Father_img = dr["Father_img"].ToString(),
                            Mother_img = dr["Mother_img"].ToString(),
                            Father_mob = dr["father_mob"].ToString(),
                            Mother_mobBD = dr["mother_mob"].ToString(),
                            House_name = dr["House_name"].ToString(),
                        });
                    }
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_all_vertical_id_card));
            }
        }


        //=======================================================
        public class Fatch_id_cards_for_employee
        {
            public string Employee_Name { get; set; }
            public string Emp_Code { get; set; }
            public string Designation { get; set; }
            public string Mobile { get; set; }
            public string Address { get; set; }
            public string Employee_image { get; set; }
            public string idcard_template { get; set; }
            public string Sessions { get; set; }
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
                query = "select *,(select top 1 Template_filepath from Id_card_template_setting where Branch_id='" + Branch_id + "' and Type='Employee') as idcard_template  from PRL_Employee_Master t1 join user_details t2 on t1.Emp_Code=t2.user_id where t2.Branch_id='" + Branch_id + "' and t2.Id in(" + Emp_id + ")   and t2.Istatus='1' order by t2.name asc";
            }
            else
            {
                if (IdType == "SINGLE")
                {
                    query = "select *,(select top 1 Template_filepath from Id_card_template_setting where Branch_id='" + Branch_id + "' and Type='Employee') as idcard_template  from PRL_Employee_Master t1 join user_details t2 on t1.Emp_Code=t2.user_id where t2.Branch_id='" + Branch_id + "' and t2.user_id='" + Emp_id + "'   and t2.Istatus='1' order by t2.name asc";
                }
                else
                {
                    if (UserType == "ALL" && Emp_id == "ALL")
                    {
                        query = "select *,(select top 1 Template_filepath from Id_card_template_setting where Branch_id='" + Branch_id + "' and Type='Employee') as idcard_template from PRL_Employee_Master t1 join user_details t2 on t1.Emp_Code=t2.user_id where t2.Branch_id='" + Branch_id + "' and t2.Istatus='1' order by t2.name asc";
                    }
                    else if (UserType != "ALL" && Emp_id == "ALL")
                    {
                        query = "select *,(select top 1 Template_filepath from Id_card_template_setting where Branch_id='" + Branch_id + "' and Type='Employee') as idcard_template  from PRL_Employee_Master t1 join user_details t2 on t1.Emp_Code=t2.user_id where t2.Branch_id='" + Branch_id + "' and t2.User_Type='" + UserType + "' and t2.Istatus='1' order by t2.name asc";
                    }
                    else if (UserType == "ALL" && Emp_id != "ALL")
                    {
                        query = "select *,(select top 1 Template_filepath from Id_card_template_setting where Branch_id='" + Branch_id + "' and Type='Employee') as idcard_template  from PRL_Employee_Master t1 join user_details t2 on t1.Emp_Code=t2.user_id where t2.Branch_id='" + Branch_id + "' and t2.user_id='" + Emp_id + "' and t2.Istatus='1' order by t2.name asc";
                    }
                    else
                    {
                        query = "select *,(select top 1 Template_filepath from Id_card_template_setting where Branch_id='" + Branch_id + "' and Type='Employee') as idcard_template  from PRL_Employee_Master t1 join user_details t2 on t1.Emp_Code=t2.user_id where t2.Branch_id='" + Branch_id + "' and t2.Istatus='1' order by t2.name asc";
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

                    Show_id_cards_for_employee.Add(new Fatch_id_cards_for_employee
                    {
                        Employee_Name = dr["Employee_Name"].ToString(),
                        Emp_Code = dr["Emp_Code"].ToString(),
                        Designation = dr["employee_type"].ToString(),
                        Mobile = dr["Mobile"].ToString(),
                        Address = dr["Address"].ToString(),
                        Employee_image = dr["Employee_image"].ToString(),
                        idcard_template = dr["idcard_template"].ToString(),
                        Sessions = sessioN,
                    });
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(Show_id_cards_for_employee));
            }
        }
    }
}
