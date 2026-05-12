using school_web.AppCode;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile
{
    public partial class school_calendar : System.Web.UI.Page
    {
        UsesCode imp = new UsesCode();
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Session["User"] != null)
                    {
                        ViewState["regid"] = Session["User"].ToString();
                        ViewState["Sessionid"] = My.get_session_id();


                        student_info(ViewState["regid"].ToString());
                        mycode.bind_all_ddl_with_id(ddl_month, "Select  Month,Month_Id from Month_Index order by Month_Id asc");
                        ddl_month.SelectedValue = mycode.get_current_month_id();
                        BindGridclander();
                    }
                    else
                    {
                        Session.Abandon();
                        Session.Clear();
                        Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
                        Response.Write("<script language=javascript>wnd.close();</script>");
                        Response.Write("<script language=javascript>window.open('../Default.aspx','_parent',replace=true);</script>");
                    }
                }
            }
            catch (Exception ex)
            {
            } 
        }

        private void student_info(string regid)
        {
            string query = "select top 1 Session_id,Class_id,Section,Branch_id,session,class from admission_registor where admissionserialnumber='" + ViewState["regid"].ToString() + "'    order by id desc";
            SqlDataAdapter ad_contactus = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad_contactus.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {

            }
            else
            {

                ViewState["class_id"] = dt.Rows[0]["Class_id"].ToString();
                ViewState["Section"] = dt.Rows[0]["Section"].ToString();
                ViewState["Branch_id"] = dt.Rows[0]["Branch_id"].ToString();
                ViewState["session"] = dt.Rows[0]["session"].ToString();
                ViewState["class"] = dt.Rows[0]["class"].ToString();

            }
        }



        Hashtable HolidayList;
        private void BindGridclander()
        {
            lbl_session.Text = "Month: " + ddl_month.SelectedItem.Text + " Session: " + ViewState["session"].ToString() + " Class: " + ViewState["class"].ToString();
            string session_frst_year = ViewState["session"].ToString().Substring(0, 4);

            int year = My.check_start_months(Convert.ToInt32(ddl_month.SelectedValue), Convert.ToInt32(session_frst_year));

            HolidayList = Getholiday();
            Calendar1.VisibleDate = Convert.ToDateTime("01/" + ddl_month.SelectedItem.Text + "/" + year.ToString());
            Calendar1.FirstDayOfWeek = FirstDayOfWeek.Sunday;
            Calendar1.NextPrevFormat = NextPrevFormat.ShortMonth;
            Calendar1.TitleFormat = TitleFormat.Month;
            Calendar1.ShowGridLines = true;
            Calendar1.DayStyle.Height = new Unit(50);
            Calendar1.DayStyle.Width = new Unit(150);
            Calendar1.DayStyle.HorizontalAlign = HorizontalAlign.Center;
            Calendar1.DayStyle.VerticalAlign = VerticalAlign.Middle;
            Calendar1.OtherMonthDayStyle.BackColor = System.Drawing.Color.Gray;
        }
        private Hashtable Getholiday()
        {

            Hashtable holiday = new Hashtable();
            string query = "Select Date as new_date,Type,Details,Day from School_Holiday_Calendar where Session_id=" + ViewState["Sessionid"].ToString() + " and Branchi_id='" + ViewState["Branch_id"].ToString() + "' and Class_id=" + ViewState["class_id"].ToString() + " and Month='" + ddl_month.SelectedValue + "' order by Idate asc";

            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "School_Holiday_Calendar");
            ViewState["School_Holiday_Calendar"] = ds;
            DataTable dt = ds.Tables[0];

            if (dt.Rows.Count == 0)
            {
                Calendar1.Visible = false;
                Alertme("Sorry there are no school calendar list exists", "warning");
            }
            else
            {
                Calendar1.Visible = true;
                foreach (DataRow dr in dt.Rows)
                {
                    string date = dr["new_date"].ToString();
                    if (dr["Details"].ToString() == "")
                    {
                        holiday[date] = dr["Type"].ToString() + "~" + "-";
                    }
                    else
                    {
                        holiday[date] = dr["Type"].ToString() + "~" + dr["Details"].ToString();
                    }
                }

            }
            return holiday;
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

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            BindGridclander();
        }
        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            try
            {
                if (HolidayList[e.Day.Date.ToShortDateString()] != null)
                {
                    Literal literal1 = new Literal();
                    literal1.Text = "<br/>";
                    e.Cell.Controls.Add(literal1);
                    Label label1 = new Label();

                    string types = (string)HolidayList[e.Day.Date.ToShortDateString()];
                    string[] stringSeparatorss = new string[] { "~" };
                    string[] arrs = types.Split(stringSeparatorss, StringSplitOptions.None);
                    string types1 = arrs[0];
                    string types2 = arrs[1];

                    label1.Text = types1;
                    label1.Font.Size = new FontUnit(FontSize.Small);
                    label1.CssClass = "clnderType";
                    e.Cell.Controls.Add(label1);

                    if (types1.ToUpper() == types2.ToUpper())
                    {
                        types2 = "-";
                    }
                    if (types1.ToUpper() == "HOLIDAY")
                    {
                        if (types2.ToUpper() == "CLASS" || types2.ToUpper() == "CLASSES")
                        {
                            types2 = "-";
                        }
                    }

                    if (types2 != "-")
                    {
                        Label label2 = new Label();
                        label2.Text = "(" + types2 + ")";
                        label2.Font.Size = new FontUnit(FontSize.Small);
                        label2.CssClass = "clnderDesc";
                        e.Cell.Controls.Add(label2);
                    }


                    if (label1.Text == "Class")
                    {
                        e.Cell.ForeColor = Color.White;
                        e.Cell.BackColor = Color.Green;
                        e.Cell.ToolTip = label1.Text;
                    }
                    else if (label1.Text == "Holiday")
                    {
                        e.Cell.ForeColor = Color.White;
                        e.Cell.BackColor = Color.Maroon;
                        e.Cell.ToolTip = label1.Text;
                    }
                    else if (label1.Text == "Events")
                    {
                        e.Cell.ForeColor = Color.White;
                        e.Cell.BackColor = Color.Orange;
                        e.Cell.ToolTip = label1.Text;
                    }
                    else if (label1.Text == "Examination")
                    {
                        e.Cell.ForeColor = Color.White;
                        e.Cell.BackColor = Color.OrangeRed;
                        e.Cell.ToolTip = label1.Text;
                    }
                    else if (label1.Text.ToUpper() == "NON-ACADEMIC")
                    {
                        e.Cell.ForeColor = Color.White;
                        e.Cell.BackColor = Color.HotPink;
                        e.Cell.ToolTip = label1.Text;
                    }
                    else
                    {
                        e.Cell.ForeColor = Color.White;
                        e.Cell.BackColor = Color.Maroon;
                        e.Cell.ToolTip = label1.Text;
                    }
                }
            }
            catch
            {
            }
        }
    }
}