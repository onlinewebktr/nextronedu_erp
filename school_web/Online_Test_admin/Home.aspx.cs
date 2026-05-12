using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Online_Test_admin
{
    public partial class Home : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            //Session["Admin"] = "mces2025";
            //Session["firm"] = "MCES-001";
            if (Session["Admin"] == null)
            {
                Session.Abandon();
                Session.Clear();
                Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
                Response.Write("<script language=javascript>wnd.close();</script>");
                Response.Write("<script language=javascript>window.open('../Default.aspx','_parent',replace=true);</script>");
            }
            else
            {
                if (!IsPostBack)
                {
                   

                    Session["branchid"] = "1";
                     

                    ViewState["sesionid"] = My.get_session_id();
                    mycode.bind_all_ddl_with_id(ddlsession, "select Session,session_id from session_details");
                    ddlsession.SelectedValue = ViewState["sesionid"].ToString();
                    mycode.bind_all_ddl_with_id_notselect(ddl_months, "select Month,Month_Id from Month_Index order by Position asc");
                    ddl_months.SelectedValue = mycode.get_current_month_id();

                    hd_months.Value = ddl_months.SelectedItem.Text;
                    fetch_data();
                }
            }

        }

        private void fetch_data()
        {
            var condition = "where 1=1 ";

            condition += $" and  Session_id='{ViewState["sesionid"].ToString()}' ";
            DataTable dt = My.MydataTable($@"select top 1 id,(select  count(Id) as totalactiveteat from OLINETEST_EXAM_NAME_Murge_Section where Status='Active' and  Session_Id=OLINETEST_EXAM_NAME_Murge_Section.Session_Id) as totalactiveteat,( select count(oen.id)  from  OlineTest_Exam_name oen  join admission_registor ar on  oen.Session_id=ar.Session_id and oen.Class_id=ar.Class_id and oen.Section=ar.Section  where  oen.Session_id=OlineTest_Exam_name.Session_Id and oen.Status='Active' and  oen.Session_id=OlineTest_Exam_name.Session_Id and  ar.admissionserialnumber not  in (Select Studentid from user_test_total_marks_details where Exam_code=oen.Exam_id and Session_Id=oen.Session_id and Class_Id =oen.Class_id and  Section=oen.Section   )) as notattpted,( select count(oen.id)  from  OlineTest_Exam_name oen  join admission_registor ar on  oen.Session_id=ar.Session_id and oen.Class_id=ar.Class_id and oen.Section=ar.Section  where  oen.Session_id=OlineTest_Exam_name.Session_Id and oen.Status='Active' and  oen.Session_id=OlineTest_Exam_name.Session_Id and  ar.admissionserialnumber    in (Select Studentid from user_test_total_marks_details where Exam_code=oen.Exam_id and Session_Id=oen.Session_id and Class_Id =oen.Class_id and  Section=oen.Section   )) as  attpted,(select  count(Id)  from OLINETEST_EXAM_NAME_Murge_Section where Status='Inactive' and  Session_Id=OLINETEST_EXAM_NAME_Murge_Section.Session_Id) as totalinactiveteat from OlineTest_Exam_name  {condition}  ");
            if (dt.Rows.Count == 0)
            {
                lbl_total_test.InnerText = "0";
                lbl_TotalAttempted.InnerText = "0";
                lbl_TotalnotAttempted.InnerText = "0";
                lbl_inactivetotal_test.InnerText = "0";
            }
            else
            {
                lbl_total_test.InnerText = dt.Rows[0]["totalactiveteat"].ToString();
                lbl_TotalAttempted.InnerText = dt.Rows[0]["attpted"].ToString();
                lbl_TotalnotAttempted.InnerText = dt.Rows[0]["notattpted"].ToString();
                lbl_inactivetotal_test.InnerText = dt.Rows[0]["totalinactiveteat"].ToString();


            }


        }
        private static string get_sections(string Session, string Class_id, string Section, string Student_type)
        {
            string query = "";
            if (Class_id == "0")
            {
                if (Student_type == "ALL")
                {
                    query = "select DISTINCT Section from admission_registor where Session_id='" + Session + "' and (Transfer_Status='NT' or Transfer_Status='Transferred' or Transfer_Status='New')";
                }
                else
                {
                    query = "select DISTINCT Section from admission_registor where Session_id='" + Session + "' and Transfer_Status='" + Student_type + "'";
                }
            }
            else
            {
                if (Section == "0" || Section == "ALL")
                {
                    if (Student_type == "ALL")
                    {
                        query = "select DISTINCT Section from admission_registor where Session_id='" + Session + "'  and Class_id='" + Class_id + "' and (Transfer_Status='NT' or Transfer_Status='Transferred' or Transfer_Status='New')";
                    }
                    else
                    {
                        query = "select DISTINCT Section from admission_registor where Session_id='" + Session + "'  and Class_id='" + Class_id + "' and Transfer_Status='" + Student_type + "'";
                    }
                }
                else
                {
                    if (Student_type == "ALL")
                    {
                        query = "select DISTINCT Section from admission_registor where Session_id='" + Session + "'  and Class_id='" + Class_id + "' and Section='" + Section + "' and (Transfer_Status='NT' or Transfer_Status='Transferred' or Transfer_Status='New')";
                    }
                    else
                    {
                        query = "select DISTINCT Section from admission_registor where Session_id='" + Session + "'  and Class_id='" + Class_id + "' and Section='" + Section + "' and Transfer_Status='" + Student_type + "'";
                    }
                }
            }


            SqlDataAdapter ad = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "admission_registor");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                return "'A'";
            }
            else
            {
                string section = "";
                foreach (DataRow dr in dt.Rows)
                {
                    section = section + "'" + dr["Section"].ToString() + "',";
                }

                section = section.Remove(section.Length - 1);
                return section;
            }
        }
        [WebMethod]
        public static List<object> GetChartData(string Session, string month)
        {

            string query = "";
            query = "Select DISTINCT ar.Session_id,ar.Class_id,ad.Course_Name as Class,ad.Position,sm.Status,sm.Status as Section,0 as Total from Chart_status_master sm CROSS JOIN admission_registor ar join Add_course_table ad on ar.Class_id=ad.course_id where ar.Session_id='" + Session + "' and  sm.Type in (7) order by ad.Position asc ";



            SqlDataAdapter ad = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Cart_Table");
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                int total_count = get_total(dr["Session_id"].ToString(), dr["Class_id"].ToString(), dr["Status"].ToString(), month);
                dr["Total"] = total_count;
            }

            List<object> chartData = new List<object>();
            List<string> countries = (from p in dt.AsEnumerable()
                                      select p.Field<string>("Section")).Distinct().ToList();

            countries.Insert(0, "Status");

            //Add the Countries Array to the Chart Array.
            chartData.Add(countries.ToArray());


            //Get the DISTINCT Date.
            List<string> years = (from p in dt.AsEnumerable()
                                  select p.Field<string>("Class")).Distinct().ToList();

            //Loop through the Date.
            foreach (string year in years)
            {

                //Get the Total of Orders for each Status for the Date.
                List<object> totals = (from p in dt.AsEnumerable()
                                       where p.Field<string>("Class") == year
                                       select p.Field<Int32>("Total")).Cast<object>().ToList();

                //Insert the Year value as Label in First position.
                totals.Insert(0, year.ToString());

                //Add the Years Array to the Chart Array.
                chartData.Add(totals.ToArray());
            }
            return chartData;
        }

        private static int get_total(string Session_id, string Class_id, string status, string month)
        {
            string cunrt_session = My.get_session_static(Session_id);
            string[] stringSeparators = new string[] { "-" };
            string[] arr = cunrt_session.Split(stringSeparators, StringSplitOptions.None);
            string session_frst_year = arr[0];
            string session_last_year = arr[1];
            int session_s_year = My.toint(session_frst_year);
            int s_year = My.toint(session_frst_year);

            string monthid = My.tomonth_numberstring(month);
            int pay_month = My.toint(monthid);
            s_year = My.check_start_months(pay_month, s_year);
            string yermonth =  monthid+"/"+ s_year;


            string query = "";

            if (status == "Not Attempted")
            {
                query = "select count(oen.id) from OlineTest_Exam_name oen  join admission_registor ar on oen.Session_id = ar.Session_id and oen.Class_id = ar.Class_id and oen.Section = ar.Section  where  oen.Class_id = '"+ Class_id + "' and oen.Session_id = " + Session_id + " and oen.Status = 'Active' and   ar.admissionserialnumber not  in (Select Studentid from user_test_total_marks_details where  Session_Id = '"+ Session_id + "' and Class_Id = '" + Class_id + "' and   created_date like '%" + yermonth + "' and Test_code=oen.Exam_id )";
            }
            else
            {
                query = "select count(oen.id) from OlineTest_Exam_name oen  join admission_registor ar on oen.Session_id = ar.Session_id and oen.Class_id = ar.Class_id and oen.Section = ar.Section  where oen.Class_id = '" + Class_id + "' and oen.Session_id = " + Session_id + " and oen.Status = 'Active' and   ar.admissionserialnumber  in (Select Studentid from user_test_total_marks_details where  Session_Id = '"+ Session_id + "' and Class_Id = '" + Class_id + "' and   created_date like '%" + yermonth + "' and Test_code=oen.Exam_id )";

            }

            SqlDataAdapter ad = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "OlineTest_Exam_name");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                return 0;
            }
            else
            {
                int count = My.toIntS(dt.Rows[0][0].ToString());
                return count;
            }




        }

        protected void ddl_months_SelectedIndexChanged(object sender, EventArgs e)
        {
            hd_months.Value = ddl_months.SelectedItem.Text;
        }
    }
}