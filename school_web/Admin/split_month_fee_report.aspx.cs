using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class split_month_fee_report : System.Web.UI.Page
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
                        lbl_date.Text = mycode.date();
                        mycode.bind_all_ddl_with_id(ddl_session, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int)");
                        ddl_session.SelectedValue = My.get_session_id();
                        mycode.bind_all_ddl_with_id_All(ddl_class, "Select Course_Name,course_id from Add_course_table order by Position");
                        mycode.bind_ddlall_sm(ddl_section, "Select distinct Section from admission_registor order by Section");
                        string pagename_current = "fee-report.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];

                        bind_month();
                        find_firm_details();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "MonthlyFeePayment");
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

        private void bind_month()
        {
            DataTable dt = mycode.FillData("select Month,'false' as Value,Month_Id from Month_Index order by Position asc");
            if (dt.Rows.Count == 0)
            {
                rp_month.DataSource = null;
                rp_month.DataBind();
            }
            else
            {
                rp_month.DataSource = dt;
                rp_month.DataBind();
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

        protected void chk_all_month_CheckedChanged(object sender, EventArgs e)
        {
            for (int j = 0; j < rp_month.Items.Count; j++)
            {
                CheckBox chk_month_name = rp_month.Items[j].FindControl("chk_month_name") as CheckBox;
                if (chk_all_month.Checked)
                {
                    chk_month_name.Checked = true;
                }
                else
                {
                    chk_month_name.Checked = false;
                }
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
                    Response.AddHeader("content-disposition", "attachment;filename=Dues_List_Export.xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    using (StringWriter sw = new StringWriter())
                    {
                        HtmlTextWriter hw = new HtmlTextWriter(sw);
                        GridView2.RenderControl(hw);
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
        protected void btn_find_Click1(object sender, EventArgs e)
        {


            if (ddl_session.SelectedItem.Text == "Select")
            {
                Alertme("Please select session.", "warning");
                ddl_session.Focus();
            }
            else
            {

                find_student_by_month();
            }



        }

        private void find_student_by_month()
        {
            DataTable fdt = new DataTable();
            fdt.Columns.Add("Admission_no");
            fdt.Columns.Add("Session");
            fdt.Columns.Add("Class");

            string lbl_class_id = ddl_class.SelectedValue;
            string qry = "";


            if (txt_admission_no.Text != "")
            {
                lbl_date.Text = " " + mycode.date() + " Admission No -" + txt_admission_no.Text ;

                qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.class,t1.session,t1.Section,t1.rollnumber,t1.studentname as Student_Name,t1.Session_id,t1.Class_id from admission_registor t1 where      t1.Session_id='" + ddl_session.SelectedValue + "'  and Status='1' and t1.admissionserialnumber in (select distinct Admission_no from Split_Month_Fee_Student  where Session_id=" + ddl_session.SelectedValue + " and Admission_no='"+txt_admission_no.Text+"' ) order by rollnumber";
            }
            else
            {




                if (ddl_class.SelectedItem.Text == "All" && ddl_section.Text == "All")
                {
                    lbl_date.Text = " " + mycode.date() + " Class -" + ddl_class.SelectedItem.Text + " Section - " + ddl_section.Text;

                    qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.class,t1.session,t1.Section,t1.rollnumber,t1.studentname as Student_Name,t1.Session_id,t1.Class_id from admission_registor t1 where      t1.Session_id='" + ddl_session.SelectedValue + "'  and Status='1' and t1.admissionserialnumber in (select distinct Admission_no from Split_Month_Fee_Student  where Session_id=" + ddl_session.SelectedValue + " ) order by rollnumber";

                }
                else if (ddl_class.SelectedItem.Text == "All" && ddl_section.Text != "All")
                {
                    lbl_date.Text = " " + mycode.date() + " " + ddl_class.SelectedItem.Text + " " + ddl_section.Text;

                    qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.class,t1.session,t1.Section,t1.rollnumber,t1.studentname as Student_Name,t1.Session_id,t1.Class_id from admission_registor t1 where      t1.Session_id='" + ddl_session.SelectedValue + "' and t1.Class_id='" + lbl_class_id + "'  and Status='1' and t1.admissionserialnumber in (select distinct Admission_no from Split_Month_Fee_Student  where Session_id=" + ddl_session.SelectedValue + " ) order by rollnumber";

                }
                else
                {
                    lbl_date.Text = " " + mycode.date() + " " + ddl_class.SelectedItem.Text + " " + ddl_section.Text;

                    qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl, t1.admissionserialnumber as Admission_no,t1.class,t1.session,t1.Section,t1.rollnumber,t1.studentname as Student_Name,t1.Session_id,t1.Class_id from admission_registor t1 where  t1.Session_id='" + ddl_session.SelectedValue + "' and t1.Class_id='" + lbl_class_id + "' and Section='" + ddl_section.Text + "' and Status='1' and t1.admissionserialnumber in (select distinct Admission_no from Split_Month_Fee_Student  where Session_id=" + ddl_session.SelectedValue + " ) order by rollnumber";


                }
            }
            txt_admission_no.Text = "";
            SqlDataAdapter ad_contactus = new SqlDataAdapter(qry, My.conn);
            DataSet ds = new DataSet();
            ad_contactus.Fill(ds);
            DataTable dt = ds.Tables[0];
            int srowcount = dt.Rows.Count;
            int mgrowcount1 = rp_month.Items.Count;
            if (srowcount > 0)
            {
                int kls = 0;
                for (int ixi = 0; ixi < mgrowcount1; ixi++)
                {
                    CheckBox chkM = (CheckBox)rp_month.Items[ixi].FindControl("chk_month_name");
                    if (chkM.Checked == true)
                    {
                        Label lbl_month_id = (Label)rp_month.Items[ixi].FindControl("lbl_month_id");
                        Label lbl_month_name = (Label)rp_month.Items[ixi].FindControl("lbl_month_name");
                        dt.Columns.Add(lbl_month_name.Text, Type.GetType("System.Double"));
                        fdt.Columns.Add(lbl_month_name.Text, Type.GetType("System.Double"));
                    }
                    else
                    {
                        kls++;
                    }
                }
                if (kls == mgrowcount1)
                {
                    Alertme("Please check minimum one month.", "warning");
                    return;
                }


                foreach (DataRow dr in dt.Rows)
                {
                    int mgrowcount = rp_month.Items.Count;
                    for (int ixi = 0; ixi < mgrowcount; ixi++)
                    {
                        CheckBox chkM = (CheckBox)rp_month.Items[ixi].FindControl("chk_month_name");
                        if (chkM.Checked == true)
                        {
                            Label lbl_month_id = (Label)rp_month.Items[ixi].FindControl("lbl_month_id");
                            Label lbl_month_name = (Label)rp_month.Items[ixi].FindControl("lbl_month_name");
                            dr[lbl_month_name.Text] = find_paid(lbl_month_name.Text, lbl_month_id.Text, dr);
                        }
                    }
                }
                dt.Columns.Add("Total", Type.GetType("System.Double"));
                //================

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        double total2 = 0;
                        for (int i = 9; i < dt.Columns.Count - 1; i++)
                        {
                            total2 += My.toDouble(dr[i]);
                        }
                        dr["Total"] = total2.ToString("0.00");
                    }

                    GridView2.DataSource = dt.DefaultView;
                    GridView2.DataBind();
                    btn_excels.Visible = true;
                    if (ViewState["Is_Print"].ToString() == "1")
                    {
                        print1.Visible = true;
                    }
                    else
                    {
                        print1.Visible = false;
                    }


                    //GridView2.Columns.FromKey()
                    //GridView2.Columns[7].Visible = false;



                    // String Total_mrp = Convert.ToDouble(dt.Compute("SUM(March)", string.Empty)).ToString();


                    double total = 0; ;
                    GridView2.FooterRow.Cells[6].Text = "Total";
                    GridView2.FooterRow.Cells[6].Font.Bold = true;
                    GridView2.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Left;
                    for (int k = 9; k < dt.Columns.Count; k++)
                    {
                        total = dt.AsEnumerable().Sum(row => row.Field<Double>(dt.Columns[k].ToString()));
                        GridView2.FooterRow.Cells[k].Text = total.ToString("0.00");
                        GridView2.FooterRow.Cells[k].Font.Bold = true;
                        GridView2.FooterRow.BackColor = System.Drawing.Color.Beige;
                    }

                }
            }
            else
            {
                Alertme("Student not found.", "warning");
                GridView2.DataSource = null;
                GridView2.DataBind();
            }
        }
        protected void GridView2_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[8].Visible = false;
            //e.Row.Cells[9].Visible = false;
            //e.Row.Cells[10].Visible = false;
            //e.Row.Cells[11].Visible = false;
            //e.Row.Cells[12].Visible = false;
            //e.Row.Cells[13].Visible = false;
            //e.Row.Cells[14].Visible = false;
            //e.Row.Cells[15].Visible = false;
            //e.Row.Cells[16].Visible = false;
            //e.Row.Cells[17].Visible = false;
        }

        private string find_paid(string month, string month_id, DataRow dr)
        {
            DataTable feedt = new DataTable();
            double paid_amts = 0.00;
            feedt = My.dataTable("select  Amount from Split_Month_Fee_Student  where Admission_no='" + dr["Admission_no"].ToString() + "' and Session_id='" + ddl_session.SelectedValue + "' and Month='" + month + "'");
            if (feedt.Rows.Count.ToString() != "0")
            {
                paid_amts = My.toDouble(feedt.Rows[0]["Amount"].ToString());
            }
            else
            {
                paid_amts = 0.00;
            }
            return paid_amts.ToString("0.00");
        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    for (int i = 9; i < e.Row.Cells.Count; i++)
                    {
                        decimal value;
                        if (decimal.TryParse(e.Row.Cells[i].Text.Trim(), out value))
                        {
                            e.Row.Cells[i].Text = value.ToString("0.00");
                        }
                    }
                }


            }
            catch
            { }

        }
    }
}