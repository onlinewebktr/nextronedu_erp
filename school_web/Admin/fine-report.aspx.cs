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
    public partial class fine_report : System.Web.UI.Page
    {
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
                        string pagename_current = "fee-report.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        mycode.bind_all_ddl_with_id(ddlsession, "Select  Session,session_id from session_details");
                        mycode.bind_all_ddl_with_id_All(ddl_month, "select Month,Month_Id from Month_Index order by Position asc");
                        find_firm_details();
                        ddl_month.SelectedValue = mycode.get_current_month_id();
                        ddlsession.SelectedValue = My.get_session_id();
                        hd_session.Value = My.get_session_id();
                        find_fine_monthly();
                        ViewState["flag"] = "1";
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "StudentList");
            }
        }
        private void find_firm_details()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count > 0)
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
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session.", "warning");
                    ddlsession.Focus();
                }

                else
                {
                    find_fine_monthly();
                    ViewState["flag"] = "1";
                }
            }
            catch (Exception ex)
            {
            }
        }



        private void find_fine_monthly()
        {
            if (ddl_month.SelectedItem.Text == "All")
            {
                hd_months.Value = "0";
                bind_grd_view("select t2.*,t1.studentname,t1.class,t1.Session_id,t1.Class_id from admission_registor t1 join Typewise_fee_collection t2 on t1.session=t2.session and t1.admissionserialnumber=t2.admission_no and t1.class_id=t2.class_id where t2.parameter='MonthlyFee' and  t2.feetype='Late Fine' and t2.session='" + ddlsession.SelectedItem.Text + "'");
            }
            else
            {
                hd_months.Value = ddl_month.SelectedItem.Text;
                bind_grd_view("select t2.*,t1.studentname,t1.class,t1.Session_id,t1.Class_id from admission_registor t1 join Typewise_fee_collection t2 on t1.session=t2.session and t1.admissionserialnumber=t2.admission_no and t1.class_id=t2.class_id where t2.parameter='MonthlyFee' and  t2.feetype='Late Fine' and t2.session='" + ddlsession.SelectedItem.Text + "' and t2.month='" + ddl_month.SelectedItem.Text + "'");
            }
        }



        My mycode = new My();
        private void bind_grd_view(string qry)
        {
            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data list exist", "warning");
                GrdView.DataSource = null;
                GrdView.DataBind();
                btn_excels.Visible = false;
            }
            else
            {
                btn_excels.Visible = true;
                GrdView.DataSource = dt;
                GrdView.DataBind();

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
        double fineAmt = 0;
       



        //===========================

        [WebMethod]
        public static List<object> GetChartData(string Session, string Months)
        {
            string sections = get_sections(Session, Months);
            string query = "";

            if (Months == "0")
            {
                query = "Select DISTINCT ar.Session_id,ar.Class_id,ad.Course_Name as Class,ad.Position,sm.Status,'Section '+sm.Status as Section,0 as Total from Chart_status_master sm CROSS JOIN admission_registor ar join Add_course_table ad on ar.Class_id=ad.course_id join Typewise_fee_collection t1 on t1.admission_no=ar.admissionserialnumber and t1.Class_id=ar.Class_id and t1.session=ar.Session where ar.Session_id='" + Session + "' and  sm.Status in (" + sections + ") order by ad.Position asc";
            }
            else
            {
                query = "Select DISTINCT ar.Session_id,ar.Class_id,ad.Course_Name as Class,ad.Position,sm.Status,'Section '+sm.Status as Section,0 as Total from Chart_status_master sm CROSS JOIN admission_registor ar join Add_course_table ad on ar.Class_id=ad.course_id join Typewise_fee_collection t1 on t1.admission_no=ar.admissionserialnumber and t1.Class_id=ar.Class_id and t1.session=ar.Session where ar.Session_id='" + Session + "' and t1.month='" + Months + "' and sm.Status in (" + sections + ") order by ad.Position asc";
            }



            SqlDataAdapter ad = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Cart_Table");
            DataTable dt = ds.Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                int total_count = get_total(dr["Session_id"].ToString(), dr["Class_id"].ToString(), dr["Status"].ToString(), Months);
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




        private static int get_total(string session_id, string class_id, string section, string Months)
        {
            string query = "";
            if (Months == "0")
            { 
                query = "select sum(cast(payable as float)) as Fine_amt from Typewise_fee_collection t1 join admission_registor t2 on t1.admission_no=t2.admissionserialnumber and t1.class_id=t2.Class_id and t1.session=t2.Session where t1.feetype='Late Fine' and t2.Session_id='" + session_id + "' and t2.Class_id='" + class_id + "' and t2.Section='" + section + "'"; 
            }
            else
            {
                query = "select sum(cast(payable as float)) as Fine_amt from Typewise_fee_collection t1 join admission_registor t2 on t1.admission_no=t2.admissionserialnumber and t1.class_id=t2.Class_id and t1.session=t2.Session where t1.feetype='Late Fine' and t2.Session_id='" + session_id + "' and t2.Class_id='" + class_id + "' and t2.Section='" + section + "' and t1.month='" + Months + "'"; 
            }
            SqlDataAdapter ad = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Cart_Table");
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

        private static string get_sections(string Session, string Months)
        {
            string query = "";

            if (Months == "0")
            {
                query = "select  DISTINCT t2.Section from Typewise_fee_collection t1 join admission_registor t2 on t1.admission_no=t2.admissionserialnumber and t1.Class_id=t2.Class_id and t1.session=t2.Session where t1.feetype='Late Fine' and t2.Session_id='" + Session + "' order by t2.Section asc";
            }
            else
            {
                query = "select  DISTINCT t2.Section from Typewise_fee_collection t1 join admission_registor t2 on t1.admission_no=t2.admissionserialnumber and t1.Class_id=t2.Class_id and t1.session=t2.Session where t1.feetype='Late Fine' and t2.Session_id='" + Session + "' and t1.month='" + Months + "' order by t2.Section asc";
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
        double total_collection = 0;
        protected void GrdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_ActualDues = (Label)e.Row.FindControl("lbl_ActualDues");
                if (lbl_ActualDues.Text != "")
                {
                    total_collection = total_collection + My.toDouble(lbl_ActualDues.Text);
                }


                decimal value;
                if (decimal.TryParse(lbl_ActualDues.Text.Trim(), out value))
                {
                    lbl_ActualDues.Text = value.ToString("0.00");
                }

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_total_dues = (Label)e.Row.FindControl("lbl_total_dues");
                lbl_total_dues.Text = total_collection.ToString("0.00");
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
                    Response.AddHeader("content-disposition", "attachment;filename=Export.xls");
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
    }
}