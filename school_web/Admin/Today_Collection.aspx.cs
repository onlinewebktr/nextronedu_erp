using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class Today_Collection : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
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
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();
                        ViewState["branchid"] = Session["branchid"].ToString();
                        txt_s_date.Text = mycode.date();
                        txt_e_date.Text = mycode.date();


                        hd_session.Value = My.get_session_id();
                        hd_sessions.Value = My.get_session();
                        mycode.bind_all_ddl_with_id_cap_All(ddl_session, "select Session,session_id from session_details order by Session asc");
                        //ddl_session.SelectedValue = hd_session.Value;

                        Bind_data_pageload_date_wise(); 
                        find_firm_details();

                        string pagename_current = "fee-report.aspx";
                        ViewState["Userid"] = Session["Admin"].ToString();
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        if (ViewState["Is_Print"].ToString() == "1")
                        {
                            print1.Visible = true;
                        }
                        else
                        {
                            print1.Visible = false;
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Admission_Fee_Collection_Report");
            }
        }


        private void find_firm_details()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count == 0)
            {
                imglogo.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_emaiid.Text = dt.Rows[0]["email"].ToString();
                lbl_website.Text = dt.Rows[0]["website"].ToString();
                lbl_contact_details.Text = dt.Rows[0]["contact_no"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();
            }
        }

        string scrpt;
        private void Alertme(string msg, string panel)
        {
            scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            if (panel == "success")
            {
                lbl_success.Text = msg;
                success.Visible = true;
                warning.Visible = false;
            }
            if (panel == "warning")
            {
                lbl_warning.Text = msg;
                success.Visible = false;
                warning.Visible = true;
            }
        }

        private void Bind_data_pageload_date_wise()
        {
            if (txt_s_date.Text == "")
            {
                Alertme("Please choose from date", "warning");
                txt_s_date.Focus();
            }
            else if (txt_e_date.Text == "")
            {
                Alertme("Please choose to date", "warning");
                txt_e_date.Focus();
            }
            else
            {
                int idate = mycode.ConvertStringToiDate(txt_s_date.Text);
                int idate2 = mycode.ConvertStringToiDate(txt_e_date.Text);
                if (idate > idate2)
                {
                    Alertme("End date cannot be less than start date.", "warning");
                }
                else
                {
                    string sdate = txt_s_date.Text;
                    string sday = sdate.Substring(0, 2);
                    string smonth = sdate.Substring(3, 2);
                    string syear = sdate.Substring(6, 4);

                    string edate = txt_e_date.Text;
                    string eday = edate.Substring(0, 2);
                    string emonth = edate.Substring(3, 2);
                    string eyear = edate.Substring(6, 4);

                    int idate1 = Convert.ToInt32(syear + smonth + sday);
                    int idate21 = Convert.ToInt32(eyear + emonth + eday);

                    if (idate > idate2)
                    {
                        Alertme("End date cannot be less than start date.", "warning");
                    }
                    else
                    {
                        final_find_report_by_date(idate1, idate21);
                    }
                }
            }
        }

        private void final_find_report_by_date(int idate1, int idate21)
        {
            hd_from_date.Value = idate1.ToString();
            hd_to_date.Value = idate21.ToString();

            string qrySS = "";
            if (ddl_session.SelectedItem.Text == "ALL")
            {
                qrySS = "select sum(convert(float, Amount)) as Amount,mode from (select t1.mode,t1.Amount from Student_Payment_History t1 join admission_registor t2 on  t1.Addmission_no=t2.admissionserialnumber  and t1.Session=t2.session and t1.Class_id=t2.Class_id where t1.Idate>='" + idate1 + "' and t1.Idate<='" + idate21 + "' union all select t1.Payment_mode as mode,t1.Paid_amount as Amount from Special_fee_collection t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id and t1.Admission_no=t2.admissionserialnumber where t1.Idate>='" + idate1 + "' and t1.Idate<='" + idate21 + "') t group by mode";
                bind_grd_view("select t2.rollnumber, t2.mobilenumber, t2.studentname,t1.Addmission_no,t2.class,t2.session,t1.Date,t1.Slip_no,t1.mode,t2.Session_id,t1.Class_id,t1.Amount,t1.Pay_mode_transaction_no from Student_Payment_History t1 join admission_registor t2 on  t1.Addmission_no=t2.admissionserialnumber and t1.Session=t2.session and t1.Class_id=t2.Class_id where t1.Idate>='" + idate1 + "' and t1.Idate<='" + idate21 + "' union all select t2.rollnumber, t2.mobilenumber, t2.studentname,t1.Admission_no,t2.class,t2.session,t1.Date,t1.Receipt_no as Slip_no,t1.Payment_mode as mode,t2.Session_id,t1.Class_id,t1.Paid_amount as Amount,t1.Payment_mode as Pay_mode_transaction_no from Special_fee_collection t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id and t1.Admission_no=t2.admissionserialnumber  where t1.Idate>='" + idate1 + "' and t1.Idate<='" + idate21 + "' order by Slip_no asc", qrySS);
            }
            else
            {
                qrySS = "select sum(convert(float, Amount)) as Amount,mode from (select t1.mode,t1.Amount from Student_Payment_History t1 join admission_registor t2 on  t1.Addmission_no=t2.admissionserialnumber and t1.Session=t2.session and t1.Class_id=t2.Class_id where t2.Session_id='" + ddl_session.SelectedValue + "' and t1.Idate>='" + idate1 + "' and t1.Idate<='" + idate21 + "'  union all select t1.Payment_mode as mode,t1.Paid_amount as Amount from Special_fee_collection t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id and t1.Admission_no=t2.admissionserialnumber  where t1.Session_id='" + ddl_session.SelectedValue + "' and  t1.Idate>='" + idate1 + "' and t1.Idate<='" + idate21 + "') t group by mode";
                bind_grd_view("select t2.rollnumber, t2.mobilenumber, t2.studentname,t1.Addmission_no,t2.class,t2.session,t1.Date,t1.Slip_no,t1.mode,t2.Session_id,t1.Class_id,t1.Amount,t1.Pay_mode_transaction_no from Student_Payment_History t1 join admission_registor t2 on  t1.Addmission_no=t2.admissionserialnumber  and t1.Session=t2.session and t1.Class_id=t2.Class_id where t2.Session_id='" + ddl_session.SelectedValue + "' and t1.Idate>='" + idate1 + "' and t1.Idate<='" + idate21 + "'  union all select t2.rollnumber, t2.mobilenumber, t2.studentname,t1.Admission_no,t2.class,t2.session,t1.Date,t1.Receipt_no as Slip_no,t1.Payment_mode as mode,t2.Session_id,t1.Class_id,t1.Paid_amount as Amount,t1.Payment_mode as Pay_mode_transaction_no from Special_fee_collection t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id and t1.Admission_no=t2.admissionserialnumber where t1.Session_id='" + ddl_session.SelectedValue + "' and  t1.Idate>='" + idate1 + "' and t1.Idate<='" + idate21 + "' order by Slip_no asc", qrySS);
            }
        }


        private void bind_grd_view(string query, string qrySS)
        {
            lbl_date_period.Text = "Start Date : " + txt_s_date.Text + ", End Date : " + txt_e_date.Text;
            lbl_fnl_paid.Text = "0.00";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
                lbl_fnl_paid.Text = "0.00";
            }
            else
            {
                double total = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    total = total + My.toDouble(dt.Rows[i]["Amount"].ToString());
                }

                lbl_fnl_paid.Text = total.ToString("0.00");
                rd_view.DataSource = dt;
                rd_view.DataBind(); 


                //=====================================
                rp_modewise.DataSource = null;
                rp_modewise.DataBind();
                DataTable dtSS = mycode.FillData(qrySS);
                if (dtSS.Rows.Count > 0)
                {
                    rp_modewise.DataSource = dtSS;
                    rp_modewise.DataBind();
                } 
            }
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            Bind_data_pageload_date_wise();
        }

        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Download"].ToString() == "1")
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=datewise-collection.xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel"; 
                    using (StringWriter sw = new StringWriter())
                    {
                        HtmlTextWriter hw = new HtmlTextWriter(sw);
                        Panel1.RenderControl(hw);
                        string style = @"<style> TABLE { border: 1px solid black; } TD { border: 1px solid black; } </style> ";
                        Response.Write(style);
                        Response.Output.Write(sw.ToString()); 
                        Response.Flush();
                        Response.End();
                    }
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            catch
            {
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }



        //===========================

        [WebMethod]
        public static List<object> GetChartData(string Session, string From_date, string To_date)
        {
            string fee_type = get_fee_type(Session, From_date, To_date);
            string query = "";

            if (Session == "0")
            {
                if (From_date == "0")
                {
                    query = "Select DISTINCT ar.Session_id,ar.Class_id,ad.Course_Name as Class,ad.Position,sm.Status,sm.Status as Fee_type,0 as Total from Chart_status_master sm CROSS JOIN admission_registor ar join Add_course_table ad on ar.Class_id=ad.course_id join Student_Payment_History t1 on t1.Addmission_no=ar.admissionserialnumber and t1.Class_id=ar.Class_id and t1.Session=ar.Session where t1.Adjust_type is null and  sm.Status in (" + fee_type + ") order by ad.Position asc";
                }
                else
                {
                    query = "Select DISTINCT ar.Session_id,ar.Class_id,ad.Course_Name as Class,ad.Position,sm.Status,sm.Status as Fee_type,0 as Total from Chart_status_master sm CROSS JOIN admission_registor ar join Add_course_table ad on ar.Class_id=ad.course_id join Student_Payment_History t1 on t1.Addmission_no=ar.admissionserialnumber and t1.Class_id=ar.Class_id and t1.Session=ar.Session where t1.Adjust_type is null and t1.Idate>='" + From_date + "' and t1.Idate<='" + To_date + "' and  sm.Status in (" + fee_type + ") order by ad.Position asc";
                }
            }
            else
            {
                if (From_date == "0")
                {
                    query = "Select DISTINCT ar.Session_id,ar.Class_id,ad.Course_Name as Class,ad.Position,sm.Status,sm.Status as Fee_type,0 as Total from Chart_status_master sm CROSS JOIN admission_registor ar join Add_course_table ad on ar.Class_id=ad.course_id join Student_Payment_History t1 on t1.Addmission_no=ar.admissionserialnumber and t1.Class_id=ar.Class_id and t1.Session=ar.Session where t1.Adjust_type is null and ar.Session_id='" + Session + "' and  sm.Status in (" + fee_type + ") order by ad.Position asc";
                }
                else
                {
                    query = "Select DISTINCT ar.Session_id,ar.Class_id,ad.Course_Name as Class,ad.Position,sm.Status,sm.Status as Fee_type,0 as Total from Chart_status_master sm CROSS JOIN admission_registor ar join Add_course_table ad on ar.Class_id=ad.course_id join Student_Payment_History t1 on t1.Addmission_no=ar.admissionserialnumber and t1.Class_id=ar.Class_id and t1.Session=ar.Session where t1.Adjust_type is null and ar.Session_id='" + Session + "' and t1.Idate>='" + From_date + "' and t1.Idate<='" + To_date + "' and  sm.Status in (" + fee_type + ") order by ad.Position asc";
                }
            }



            SqlDataAdapter ad = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Cart_Table");
            DataTable dt = ds.Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                if (Session == "0")
                {
                    int total_count = get_total("0", dr["Class_id"].ToString(), dr["Fee_type"].ToString(), From_date, To_date);
                    dr["Total"] = total_count;
                }
                else
                {
                    int total_count = get_total(dr["Session_id"].ToString(), dr["Class_id"].ToString(), dr["Fee_type"].ToString(), From_date, To_date);
                    dr["Total"] = total_count;
                }
            }

            List<object> chartData = new List<object>();
            List<string> countries = (from p in dt.AsEnumerable()
                                      select p.Field<string>("Fee_type")).Distinct().ToList();

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




        private static int get_total(string session_id, string class_id, string FeeType, string From_date, string To_date)
        {
            string query = "";
            if (session_id == "0")
            {
                if (From_date == "0")
                {
                    query = "select  sum(isnull(convert(float, Total_paid_amt),0)) as Total_paid_amt from (select  isnull((select  sum(isnull(convert(float, paid),0)) from Typewise_fee_collection where transection=t1.Slip_no),0) as Total_paid_amt from Student_Payment_History t1 join admission_registor t2 on t1.Addmission_no=t2.admissionserialnumber and t1.Class_id=t2.Class_id and t1.Session=t2.Session where t1.Type='" + FeeType + "' and t1.Adjust_type is null and t2.Class_id='" + class_id + "') t";
                }
                else
                {
                    query = "select  sum(isnull(convert(float, Total_paid_amt),0)) as Total_paid_amt from (select  isnull((select  sum(isnull(convert(float, paid),0)) from Typewise_fee_collection where transection=t1.Slip_no),0) as Total_paid_amt from Student_Payment_History t1 join admission_registor t2 on t1.Addmission_no=t2.admissionserialnumber and t1.Class_id=t2.Class_id and t1.Session=t2.Session where t1.Type='" + FeeType + "' and t1.Adjust_type is null and t1.Idate>='" + From_date + "' and t1.Idate<='" + To_date + "' and t2.Class_id='" + class_id + "') t";
                }
            }
            else
            {
                if (From_date == "0")
                {
                    query = "select  sum(isnull(convert(float, Total_paid_amt),0)) as Total_paid_amt from (select  isnull((select  sum(isnull(convert(float, paid),0)) from Typewise_fee_collection where transection=t1.Slip_no),0) as Total_paid_amt from Student_Payment_History t1 join admission_registor t2 on t1.Addmission_no=t2.admissionserialnumber and t1.Class_id=t2.Class_id and t1.Session=t2.Session where t1.Type='" + FeeType + "' and t1.Adjust_type is null and t2.Session_id='" + session_id + "' and t2.Class_id='" + class_id + "') t";
                }
                else
                {
                    query = "select  sum(isnull(convert(float, Total_paid_amt),0)) as Total_paid_amt from (select  isnull((select  sum(isnull(convert(float, paid),0)) from Typewise_fee_collection where transection=t1.Slip_no),0) as Total_paid_amt from Student_Payment_History t1 join admission_registor t2 on t1.Addmission_no=t2.admissionserialnumber and t1.Class_id=t2.Class_id and t1.Session=t2.Session where t2.Session_id='" + session_id + "' and t1.Type='" + FeeType + "' and t1.Adjust_type is null and t1.Idate>='" + From_date + "' and t1.Idate<='" + To_date + "' and t2.Class_id='" + class_id + "') t";
                }
            }
            SqlDataAdapter ad = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Student_Payment_History");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                return 0;
            }
            else
            {
                double ttl = My.toDouble(dt.Rows[0][0].ToString());
                int count = My.toIntS(Math.Round(ttl).ToString());
                return count;
            }
        }

        private static string get_fee_type(string Session, string From_date, string To_date)
        {
            string query = "";
            if (Session == "0")
            {
                if (From_date == "0")
                {
                    query = "select  DISTINCT t1.Type from Student_Payment_History t1 join admission_registor t2 on t1.Addmission_no=t2.admissionserialnumber and t1.Class_id=t2.Class_id and t1.Session=t2.Session where t1.Adjust_type is null order by t1.Type asc";
                }
                else
                {
                    query = "select  DISTINCT t1.Type from Student_Payment_History t1 join admission_registor t2 on t1.Addmission_no=t2.admissionserialnumber and t1.Class_id=t2.Class_id and t1.Session=t2.Session where t1.Adjust_type is null and t1.Adjust_type is null and t1.Idate>='" + From_date + "' and t1.Idate<='" + To_date + "' order by t1.Type asc";
                }
            }
            else
            {
                if (From_date == "0")
                {
                    query = "select  DISTINCT t1.Type from Student_Payment_History t1 join admission_registor t2 on t1.Addmission_no=t2.admissionserialnumber and t1.Class_id=t2.Class_id and t1.Session=t2.Session where t1.Adjust_type is null and t2.Session_id='" + Session + "' order by t1.Type asc";
                }
                else
                {
                    query = "select  DISTINCT t1.Type from Student_Payment_History t1 join admission_registor t2 on t1.Addmission_no=t2.admissionserialnumber and t1.Class_id=t2.Class_id and t1.Session=t2.Session where t1.Adjust_type is null and t2.Session_id='" + Session + "' and t1.Adjust_type is null and t1.Idate>='" + From_date + "' and t1.Idate<='" + To_date + "' order by t1.Type asc";
                }
            }
            
            SqlDataAdapter ad = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "admission_registor");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                return "'Admission','Annual','Monthly'";
            }
            else
            {
                string FeeType = "";
                foreach (DataRow dr in dt.Rows)
                {
                    FeeType = FeeType + "'" + dr["Type"].ToString() + "',";
                }

                FeeType = FeeType.Remove(FeeType.Length - 1);
                return FeeType;
            }
        }
    }
}