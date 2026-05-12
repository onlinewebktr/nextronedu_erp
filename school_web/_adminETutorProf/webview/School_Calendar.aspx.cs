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

namespace school_web._adminETutorProf.webview
{
    public partial class School_Calendar : System.Web.UI.Page
    {
        UsesCode imp = new UsesCode();
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["regid"] != null)
                    {
                        DateTime dtm = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                        string date = dtm.ToString("dd/MM/yyyy");
                        string day = date.Substring(0, 2);
                        string month = date.Substring(3, 2);
                        string year = date.Substring(6, 4);
                        mycode.bind_all_ddl_with_id(ddl_year, "Select  Session,session_id from session_details");
                        ddl_year.SelectedValue = My.get_session_id();


                        ViewState["regid"] = Request.QueryString["regid"].ToString();
                        ViewState["Sessionid"] = My.get_session_id();
                        ViewState["session"] = mycode.get_session(ViewState["Sessionid"].ToString());
                        ViewState["Branchid"] = mycode.get_branch_id(ViewState["regid"].ToString());
                        mycode.bind_all_ddl_with_id(ddl_month, "Select  Month,Month_Id from Month_Index order by Month_Id asc");
                        ddl_month.SelectedValue = mycode.get_current_month_id();
                        mycode.bind_all_ddl_with_id(ddl_class, "Select   Course_Name, course_id from Add_course_table Session_id='" + ViewState["Sessionid"].ToString() + "')  order by Position asc");
                        ddl_class.SelectedValue = My.get_top_one_class();//My.get_top_one_class_teacher(ViewState["regid"].ToString());
                        BindGrid();
                    }

                }
            }
            catch
            {
            }
        }
        Hashtable HolidayList;
        private void BindGrid()
        { 
            try
            {
                //  lbl_session.Text = "Month : " + ddl_month.SelectedItem.Text + " Session : " + ddl_year.SelectedItem.Text + " Class : " + ddl_class.SelectedItem.Text;

                lbl_monthname.Text = ddl_month.SelectedItem.Text;
                // lbl_section.Text = 
                lbl_class.Text = ddl_class.SelectedItem.Text;
                lbl_session.Text = ddl_year.SelectedItem.Text;

                string session_frst_year = ddl_year.SelectedItem.Text.Substring(0, 4); 
                HolidayList = Getholiday(); 
                Calendar1.VisibleDate = Convert.ToDateTime("01/" + ddl_month.SelectedItem.Text + "/" + session_frst_year); 
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
            catch (Exception ex)
            {
            }
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

                    if (label1.Text == "Class")
                    {
                        e.Cell.ForeColor = Color.White;
                        e.Cell.BackColor = Color.Green;
                        e.Cell.ToolTip = label1.Text;
                    }
                    else if (label1.Text == "Holiday")
                    {
                        e.Cell.ForeColor = Color.Black;
                        e.Cell.Font.Bold = true;
                        e.Cell.BackColor = Color.IndianRed;
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
                        e.Cell.BackColor = Color.Maroon;
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
        private Hashtable Getholiday()
        {
            Hashtable holiday = new Hashtable(); 
            string query = "Select Date as new_date,Type,Details,Day from School_Holiday_Calendar where Session_id=" + ddl_year.SelectedValue + " and Branchi_id='" + ViewState["Branchid"].ToString() + "' and Class_id=" + ddl_class.SelectedValue + " and Month='" + ddl_month.SelectedValue + "' order by Idate asc";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "School_Holiday_Calendar");
            ViewState["School_Holiday_Calendar"] = ds;
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                Calendar1.Visible = false;
                Alert("Sorry there are no school calendar list exists");
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
        private void Alert(string Message)
        {
            lbl_msg.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
    }
}