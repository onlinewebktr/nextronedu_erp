using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class total_outstanding_hostel : System.Web.UI.Page
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

                        mycode.bind_all_ddl_with_id(ddl_month, "select Month,Position from Month_Index order by Position asc");

                        mycode.bind_all_ddl_with_id(ddlsession, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                        ddlsession.SelectedValue = My.get_session_id();


                        mycode.bind_all_ddl_with_id_cap_All(ddlclass, "Select Course_Name,course_id,Position from Add_course_table order by Position");
                        ddlclass.SelectedValue = My.get_top_one_class();
                        get_section_classwise();


                        string pagename_current = Path.GetFileName("total-outstanding.aspx");
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        find_firm_details();
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
                //  My.saveExceptionDetails(ex, "StudentList");
            }
        }

        private void get_section_classwise()
        {
            My.bind_ddl_all_Cap(ddl_section, "select distinct Section from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' order by Section asc");
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

        protected void ddlclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                get_section_classwise();
            }
            catch (Exception ex)
            {
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
                else if (ddl_month.SelectedItem.Text == "Select")
                {
                    Alertme("Please select month.", "warning");
                    ddl_month.Focus();
                }
                else
                {
                    find_students_dues();
                }
            }
            catch (Exception ex)
            {
            }
        }


        private void find_students_dues()
        {
            string qry = "";
            if (ddlclass.SelectedItem.Text == "ALL")
            {
                lbl_class22.Text = "Hostel Dues List Up To " + ddl_month.SelectedItem.Text;
                qry = "select t1.mobilenumber,t1.class,t1.admissionserialnumber,t1.rollnumber,t1.Section,t1.session,t1.studentname,t1.fathername,t1.Hostel_roll_no,convert(float, (select sum(convert(float, Dues_amt)) from Student_wise_dues_amount where Session_id=t1.Session_id and Class_id=t1.Class_id and Admission_no=t1.admissionserialnumber and Month_position<=" + ddl_month.SelectedValue + ")) as Total_dues from admission_registor t1 join Add_course_table t2 on t1.Class_id=t2.course_id where Session_id='" + ddlsession.SelectedValue + "' and t1.hosteltaken='Yes' and Status=1 order by t1.Hostel_roll_no asc";
            }
            else if (ddlclass.SelectedItem.Text != "ALL" && ddl_section.SelectedItem.Text == "ALL")
            {
                lbl_class22.Text = "Hostel Dues List for Class " + ddlclass.SelectedItem.Text + " Up To " + ddl_month.SelectedItem.Text;
                qry = "select t1.mobilenumber,t1.class,t1.admissionserialnumber,t1.rollnumber,t1.Section,t1.session,t1.studentname,t1.fathername,t1.Hostel_roll_no,convert(float, (select sum(convert(float, Dues_amt)) from Student_wise_dues_amount where Session_id=t1.Session_id and Class_id=t1.Class_id and Admission_no=t1.admissionserialnumber and Month_position<=" + ddl_month.SelectedValue + ")) as Total_dues from admission_registor t1 join Add_course_table t2 on t1.Class_id=t2.course_id where  Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  and t1.hosteltaken='Yes' and Status=1 order by t1.Hostel_roll_no asc";
            }
            else
            {
                lbl_class22.Text = "Hostel Dues List for Class " + ddlclass.SelectedItem.Text + " Section " + ddl_section.Text + " Up To " + ddl_month.SelectedItem.Text;
                qry = "select t1.mobilenumber,t1.class,t1.admissionserialnumber,t1.rollnumber,t1.Section,t1.session,t1.studentname,t1.fathername,t1.Hostel_roll_no,convert(float, (select sum(convert(float, Dues_amt)) from Student_wise_dues_amount where Session_id=t1.Session_id and Class_id=t1.Class_id and Admission_no=t1.admissionserialnumber and Month_position<=" + ddl_month.SelectedValue + ")) as Total_dues from admission_registor t1 join Add_course_table t2 on t1.Class_id=t2.course_id where  Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'  and t1.hosteltaken='Yes' and Status=1 order by t1.Hostel_roll_no asc";
            }

            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {
                lbl_ttl_dues.Text = "0.00";
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                String Total_mrp = Convert.ToDouble(dt.Compute("SUM(Total_dues)", string.Empty)).ToString();
                lbl_ttl_dues.Text = My.toDouble(Total_mrp).ToString("0.00");
                rd_view.DataSource = dt;
                rd_view.DataBind();
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
                    Response.AddHeader("content-disposition", "attachment;filename=Dues_list_hostel" + mycode.date() + "_" + mycode.time() + ".xls");
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


        protected void lnk_calculate_Click(object sender, EventArgs e)
        {
            try
            {
                bool flag = false;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
                {
                    SqlConnection con = new SqlConnection(My.conn);
                    con.Open();
                    string qry = "";
                    if (ddlclass.SelectedItem.Text == "ALL")
                    {
                        qry = "select Session_id,admissionserialnumber,Class_id from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and hosteltaken='Yes' and Status=1";
                    }
                    else if (ddlclass.SelectedItem.Text != "ALL" && ddl_section.SelectedItem.Text == "ALL")
                    {
                        qry = "select Session_id,admissionserialnumber,Class_id from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and hosteltaken='Yes and Status=1";
                    }
                    else
                    {
                        qry = "select Session_id,admissionserialnumber,Class_id from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and hosteltaken='Yes and Status=1";
                    }
                    DataTable dt = payments.dataTable(qry, con);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            dues_update_headwise_transaction.update_student_dues(dr["Session_id"].ToString(), dr["Class_id"].ToString(), dr["admissionserialnumber"].ToString(), "0", "0", con);
                        }
                    }
                    flag = true;
                    con.Close();
                    scope.Complete();
                }

                if (flag == true)
                {
                    Alertme("Dues has been calculated successfully.", "success");
                    find_students_dues();
                }
                else
                {
                    Alertme("Something went wrong. Please try again.", "warning");
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}