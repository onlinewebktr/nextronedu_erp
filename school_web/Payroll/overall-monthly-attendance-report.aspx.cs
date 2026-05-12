using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Payroll
{
    public partial class overall_monthly_attendance_report : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
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
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();



                        ddl_month.DataSource = My.bindMonthName();
                        string mm = String.Format("{0:MMMM}", DateTime.Now);
                        ddl_month.Text = mm;
                        ddl_month.DataBind();

                        ddl_year.DataSource = My.bindYear();
                        ddl_year.Text = DateTime.Now.ToString("yyyy");
                        ddl_year.DataBind();

                        find_firm_details();

                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Individual_Monthly_Attendance_Report");
            }
        }
        private void find_firm_details()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count == 0)
            {




            }
            else
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



        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_year.SelectedItem.Text == "Select")
                {
                    Alertme("Please choose year", "warning");
                    ddl_year.Focus();
                    return;
                }
                if (ddl_month.SelectedItem.Text == "Select")
                {
                    Alertme("Please choose month", "warning");
                    ddl_month.Focus();
                    return;
                }
                find_data();
            }
            catch (Exception ex)
            {
            }
        }


        DataTable dt = new DataTable();
        DataTable emp_dt = new DataTable();
        DataTable att_dt = new DataTable();
        string month = "";
        string year = "";
        private void find_data()
        {
            dt = null;
            emp_dt = null;
            att_dt = null;
            dt = new DataTable();
            dt.Columns.Add("Emp_Code");
            dt.Columns.Add("Emp_Name");
            dt.Columns.Add("one");
            dt.Columns.Add("two");
            dt.Columns.Add("three");
            dt.Columns.Add("four");
            dt.Columns.Add("five");
            dt.Columns.Add("six");
            dt.Columns.Add("seven");
            dt.Columns.Add("eight");
            dt.Columns.Add("nine");
            dt.Columns.Add("ten");
            dt.Columns.Add("eleven");
            dt.Columns.Add("twelve");
            dt.Columns.Add("thirteen");
            dt.Columns.Add("fourteen");
            dt.Columns.Add("fifteen");
            dt.Columns.Add("sixteen");
            dt.Columns.Add("seventeen");
            dt.Columns.Add("eighteen");
            dt.Columns.Add("nineteen");
            dt.Columns.Add("twenty");
            dt.Columns.Add("twenty_one");
            dt.Columns.Add("twenty_two");
            dt.Columns.Add("twenty_three");
            dt.Columns.Add("twenty_four");
            dt.Columns.Add("twenty_five");
            dt.Columns.Add("twenty_six");
            dt.Columns.Add("twenty_seven");
            dt.Columns.Add("twenty_eight");
            dt.Columns.Add("twenty_nine");
            dt.Columns.Add("thirty");
            dt.Columns.Add("thirty_one");
            month = (ddl_month.SelectedIndex + 1).ToString();
            year = ddl_year.Text;

            try
            {

                fetch_attendance_report();

            }
            catch (Exception Ex)
            {
                My.Save_Exception(Ex.StackTrace);
            }
            lbl_monthS.Text = ddl_month.SelectedItem.Text;
            lbl_year.Text = ddl_year.SelectedItem.Text;

            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data list exist", "warning");
                grd_attendance.DataSource = null;
                grd_attendance.DataBind();
                pnl_grids.Visible = false;

            }
            else
            {
               
                grd_attendance.DataSource = dt.DefaultView;
                grd_attendance.DataBind();
                pnl_grids.Visible = true;
            }
        }


        private void fetch_attendance_report()
        {
            emp_dt = My.dataTable("select Employee_Name,Employee_id,Emp_Code from dbo.[PRL_Employee_Master] where Status='Active' order by Employee_Name");
           // att_dt = My.dataTable(" select  * from dbo.[PRL_Employee_Daily_attendance_chart] where idate like '%" + year + My.getMonthS_twoDigit(month) + "%' ");

            att_dt = My.dataTable("Select id, format(DateTime,'hh:mm tt') time,format(DateTime,'yyyyMMdd') idate,DateTime,Employee_id    from dbo.[PRL_Attendance_Log]  where format(DateTime,'yyyyMM') = '" + year + My.getMonthS_twoDigit(month) + "' order by DateTime ");
            foreach (DataRow dr in emp_dt.Rows)
            {
                DataRow dr1 = dt.NewRow();
                dr1["Emp_Code"] = dr["Emp_Code"].ToString();
                dr1["Emp_Name"] = dr["Employee_Name"].ToString();
                dr1["one"] = find_aattendande(dr["Employee_id"].ToString(), year + My.getMonthS_twoDigit(month) + "01");
                dr1["two"] = find_aattendande(dr["Employee_id"].ToString(), year + My.getMonthS_twoDigit(month) + "02");
                dr1["three"] = find_aattendande(dr["Employee_id"].ToString(), year + My.getMonthS_twoDigit(month) + "03");
                dr1["four"] = find_aattendande(dr["Employee_id"].ToString(), year + My.getMonthS_twoDigit(month) + "04");
                dr1["five"] = find_aattendande(dr["Employee_id"].ToString(), year + My.getMonthS_twoDigit(month) + "05");
                dr1["six"] = find_aattendande(dr["Employee_id"].ToString(), year + My.getMonthS_twoDigit(month) + "06");
                dr1["seven"] = find_aattendande(dr["Employee_id"].ToString(), year + My.getMonthS_twoDigit(month) + "07");
                dr1["eight"] = find_aattendande(dr["Employee_id"].ToString(), year + My.getMonthS_twoDigit(month) + "08");
                dr1["nine"] = find_aattendande(dr["Employee_id"].ToString(), year + My.getMonthS_twoDigit(month) + "09");
                dr1["ten"] = find_aattendande(dr["Employee_id"].ToString(), year + My.getMonthS_twoDigit(month) + "10");
                dr1["eleven"] = find_aattendande(dr["Employee_id"].ToString(), year + My.getMonthS_twoDigit(month) + "11");
                dr1["twelve"] = find_aattendande(dr["Employee_id"].ToString(), year + My.getMonthS_twoDigit(month) + "12");
                dr1["thirteen"] = find_aattendande(dr["Employee_id"].ToString(), year + My.getMonthS_twoDigit(month) + "13");
                dr1["fourteen"] = find_aattendande(dr["Employee_id"].ToString(), year + My.getMonthS_twoDigit(month) + "14");
                dr1["fifteen"] = find_aattendande(dr["Employee_id"].ToString(), year + My.getMonthS_twoDigit(month) + "15");
                dr1["sixteen"] = find_aattendande(dr["Employee_id"].ToString(), year + My.getMonthS_twoDigit(month) + "16");
                dr1["seventeen"] = find_aattendande(dr["Employee_id"].ToString(), year + My.getMonthS_twoDigit(month) + "17");
                dr1["eighteen"] = find_aattendande(dr["Employee_id"].ToString(), year + My.getMonthS_twoDigit(month) + "18");
                dr1["nineteen"] = find_aattendande(dr["Employee_id"].ToString(), year + My.getMonthS_twoDigit(month) + "19");
                dr1["twenty"] = find_aattendande(dr["Employee_id"].ToString(), year + My.getMonthS_twoDigit(month) + "20");
                dr1["twenty_one"] = find_aattendande(dr["Employee_id"].ToString(), year + My.getMonthS_twoDigit(month) + "21");
                dr1["twenty_two"] = find_aattendande(dr["Employee_id"].ToString(), year + My.getMonthS_twoDigit(month) + "22");
                dr1["twenty_three"] = find_aattendande(dr["Employee_id"].ToString(), year + My.getMonthS_twoDigit(month) + "23");
                dr1["twenty_four"] = find_aattendande(dr["Employee_id"].ToString(), year + My.getMonthS_twoDigit(month) + "24");
                dr1["twenty_five"] = find_aattendande(dr["Employee_id"].ToString(), year + My.getMonthS_twoDigit(month) + "25");
                dr1["twenty_six"] = find_aattendande(dr["Employee_id"].ToString(), year + My.getMonthS_twoDigit(month) + "26");
                dr1["twenty_seven"] = find_aattendande(dr["Employee_id"].ToString(), year + My.getMonthS_twoDigit(month) + "27");
                dr1["twenty_eight"] = find_aattendande(dr["Employee_id"].ToString(), year + My.getMonthS_twoDigit(month) + "28");
                dr1["twenty_nine"] = find_aattendande(dr["Employee_id"].ToString(), year + My.getMonthS_twoDigit(month) + "29");
                dr1["thirty"] = find_aattendande(dr["Employee_id"].ToString(), year + My.getMonthS_twoDigit(month) + "30");
                dr1["thirty_one"] = find_aattendande(dr["Employee_id"].ToString(), year + My.getMonthS_twoDigit(month) + "31");
                dt.Rows.Add(dr1);
            }
        }



        private void load_report()
        {
            Dictionary<String, object> param = new Dictionary<string, object>();
            param["firm_name"] = mycode.get_firm_name();
            param["address"] = My.address1;
            param["month"] = ddl_month.Text;
            param["year"] = year;
            //reportViewer.load_report(dt, "OverAll_Monthly_Attendance_Chart.rdlc", param);
        }


        string attendance = "";
        string in_t;
        DateTime d;
        string s_in;

        string out_t;
        DateTime d1;
        string s_out;
        private object find_aattendande(string emp_id, string idate)
        {
            attendance = "";
            //DataView dv = att_dt.DefaultView;

            DataView dv1 = att_dt.DefaultView;
            dv1.RowFilter = " idate='" + idate + "' and Employee_id='" + mycode.get_acutal_empid(emp_id) + "'";
            dv1.Sort = "DateTime ASC";
            DataTable aadt1 = dv1.ToTable();


            //----------------------------------------------
            DataView dv2 = att_dt.DefaultView;
            dv2.RowFilter = " idate='" + idate + "' and Employee_id='" + mycode.get_acutal_empid(emp_id) + "' ";
            dv2.Sort = "DateTime DESC";
            DataTable aadt2 = dv1.ToTable();

            try
            {
                //dv.RowFilter = (" idate='" + idate + "' and Employee_id='" + emp_id + "' ");
                //DataTable temp = dv.ToTable();


                in_t = aadt1.Rows[0]["time"].ToString();
                d = DateTime.Parse(in_t);
                s_in = d.ToString("HH:mm");

                if (aadt2.Rows.Count > 1)
                {
                    out_t = aadt2.Rows[0]["time"].ToString();
                    d1 = DateTime.Parse(out_t);
                    s_out = d1.ToString("HH:mm");
                }
                else
                {
                    s_out = "A";

                }
                attendance = s_in + " " + s_out;


                if (attendance == "")
                {
                    attendance = "A";
                }
            }
            catch
            {
                attendance = "A";
            }
            return attendance;
        }

    }
}