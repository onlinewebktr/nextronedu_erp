using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
using System.Collections;
using System.Drawing;
using System.IO;
namespace school_web.Admin
{
    public partial class View_Class_Wise_Calendaraspx : System.Web.UI.Page
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
                        find_firm_details();
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["courseID"] = "0";
                        ViewState["Sessionid"] = My.get_session_id();
                        ViewState["Branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        mycode.bind_all_ddl_with_id(ddlsession, "Select  Session,session_id from session_details");
                        ddlsession.SelectedValue = My.get_session_id();
                        mycode.bind_all_ddl_with_id(ddlclass, "Select Course_Name,course_id from Add_course_table order by Position");
                        mycode.bind_all_ddl_with_id(ddlmonth, "Select  Month,Month_Id from Month_Index order by Position asc");
                        ddlmonth.SelectedValue = mycode.get_current_month_id();
                        ddlclass.SelectedValue = My.get_top_one_class();
                        Bind_data_class_data();

                        string pagename_current = Path.GetFileName(Request.Path);
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
                My.submitException(ex, "View_Class_Wise_Calendaraspx");
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
        Hashtable HolidayList;
        protected void btn_find_Click(object sender, EventArgs e)
        {
            if (ddlsession.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
            }
            else if (ddlmonth.SelectedItem.Text == "Select")
            {
                Alertme("Please select month", "warning");
            }
            else if (ddlclass.SelectedItem.Text == "Select")
            {
                Alertme("Please select class", "warning");
            }
            else
            {
                Bind_data_class_data();


            }
        }

        private void Bind_data_class_data()
        {
            lbl_session.Text = "Month: " + ddlmonth.SelectedItem.Text + " Session" + ddlsession.SelectedItem.Text + " Class: " + ddlclass.SelectedItem.Text;
            string session_frst_year = ddlsession.SelectedItem.Text.Substring(0, 4);

            int year = My.check_start_months(Convert.ToInt32(ddlmonth.SelectedValue), Convert.ToInt32(session_frst_year));
            HolidayList = Getholiday();
            // Calendar1.SelectedDate = Convert.ToDateTime("01/"+ ddlmonth.SelectedItem.Text+"/"+mycode.year());
            Calendar1.VisibleDate = Convert.ToDateTime("01/" + ddlmonth.SelectedItem.Text + "/" + year.ToString());
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
        DataSet ds = new DataSet();
        private Hashtable Getholiday()
        {
            Hashtable holiday = new Hashtable();
            string query = "Select Date as new_date,Type,Details,Day from School_Holiday_Calendar where Session_id=" + ddlsession.SelectedValue + " and Branchi_id='" + ViewState["Branchid"].ToString() + "' and Class_id=" + ddlclass.SelectedValue + " and Month='" + ddlmonth.SelectedValue + "' order by Idate asc";

            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "School_Holiday_Calendar");
            ViewState["School_Holiday_Calendar"] = ds;
            DataTable dt = ds.Tables[0];

            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data list exist", "warning");
                //   rd_view.DataSource = null;
                //  rd_view.DataBind();

                GrdView.DataSource = null;
                GrdView.DataBind();
            }
            else
            {
                GrdView.DataSource = dt;
                GrdView.DataBind();
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
        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {

        }

        protected void Calendar1_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {

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
                //DataSet ds = (DataSet)ViewState["School_Holiday_Calendar"];
                //for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                //{

                //    if ()
                //    {

                //        if (e.Day.Date.DayOfWeek == DayOfWeek.Saturday || e.Day.Date.DayOfWeek == DayOfWeek.Sunday)
                //        {

                //            e.Cell.ToolTip = ds.Tables[0].Rows[i][1].ToString();

                //        }

                //        else
                //        {

                //            e.Cell.ForeColor = Color.White;

                //            e.Cell.BackColor = Color.Red;

                //            e.Cell.ToolTip = ds.Tables[0].Rows[i][1].ToString();//for tooltip

                //        }

                //    }
                //}
            }
            catch
            {
            }
        }
        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Download"].ToString() == "1")
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=Export" + ddlclass.SelectedItem.Text + ".xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    using (StringWriter sw = new StringWriter())
                    {
                        HtmlTextWriter hw = new HtmlTextWriter(sw);
                        GrdView.RenderControl(hw);
                        Response.Output.Write(sw.ToString());
                        Response.Flush();
                        Response.End();
                    }
                }
                else
                {
                    Alertme("SORRY! You have not permission for this work.", "warning");
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

    }
}