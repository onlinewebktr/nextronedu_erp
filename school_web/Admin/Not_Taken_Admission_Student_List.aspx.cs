using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class Not_Taken_Admission_Student_List : System.Web.UI.Page
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
                        string pagename_current = "student-report-home.aspx";
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
                        mycode.bind_all_ddl_with_id(ddl_session, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int)");
                        ddl_session.SelectedValue = My.get_session_id();
                        mycode.bind_all_ddl_with_id_list_select(ddl_classs, "select Course_Name,course_id from Add_course_table order by Position asc");
                        int ii = 0;
                        foreach (ListItem item in ddl_classs.Items)
                        {
                            if (ii < 3)
                            {
                                item.Selected = true;
                                ii++;
                            } 
                        }
                        find_students();
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
                if (ddl_session.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session", "warning");
                    ddl_session.Focus();
                }
                else
                {
                    find_students();
                    ViewState["flag"] = "1";
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void find_students()
        {
            final_find_report();
        }


        private void final_find_report()
        {
            string mainquery = "select * from (select admissionserialnumber, dateofadmission,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission' END AS Admission_Type,session,class,Section,rollnumber,studentname,gender,dob,fathername,father_mob,mothername,careof,city,district,pin,(select isnull(sum(convert(float, amount)),0) from STUDENT_WISE_DUES_AMOUNT where Session_id='" + ddl_session.SelectedValue + "' and admission_no=admission_registor.admissionserialnumber) as Dues_amt,admission_idate from admission_registor where ";
            string qoute = "'";
            string qry = "";
            bool isClassSelectd = false; string selectClassid = "";
            foreach (ListItem item in ddl_classs.Items)
            {
                if (item.Selected)
                {
                    selectClassid = selectClassid + qoute + item.Value + qoute + ",";
                    isClassSelectd = true;
                }
            }
            if (isClassSelectd == false)
            {
                ddl_classs.Focus();
                Alertme("Please select class.", "warning");
                return;
            }
            if (isClassSelectd == true)
            {
                selectClassid = selectClassid.Remove(selectClassid.Length - 1);
            }

            bool flag = false;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
            {
                SqlConnection con = new SqlConnection(My.conn);
                con.Open();
                string qrys = "";
                if (ddl_studenttype.Text == "ALL")
                {
                    qrys = "select admissionserialnumber,Session_id,Class_id from admission_registor where Session_id='" + ddl_session.SelectedValue + "' and  Class_id in (" + selectClassid + ") and (Transfer_Status='New' or Transfer_Status='NT') and Status='1' and admissionserialnumber not in (select distinct Addmission_no from dbo.[Student_Payment_History] where  Session='" + ddl_session.SelectedItem.Text + "')";
                }
                else
                {
                    qry = "select admissionserialnumber,Session_id,Class_id from admission_registor where Session_id='" + ddl_session.SelectedValue + "' and  Class_id in (" + selectClassid + ") and Transfer_Status='" + ddl_studenttype.SelectedValue + "' and Status='1' and admissionserialnumber not in (select distinct Addmission_no from dbo.[Student_Payment_History] where Session='" + ddl_session.SelectedItem.Text + "')";
                }
                DataTable dtc = payments.dataTable(qrys, con);
                if (dtc.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtc.Rows)
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
                if (ddl_studenttype.Text == "ALL")
                {
                    qry = mainquery + " Session_id='" + ddl_session.SelectedValue + "' and  Class_id in (" + selectClassid + ") and (Transfer_Status='New' or Transfer_Status='NT') and Status='1' and admissionserialnumber not in (select distinct Addmission_no from dbo.[Student_Payment_History] where  Session='" + ddl_session.SelectedItem.Text + "')) t where Dues_amt>0 order by admission_idate,class,Section,rollnumber asc";
                }
                else
                {
                    qry = mainquery + " Session_id='" + ddl_session.SelectedValue + "' and  Class_id in (" + selectClassid + ") and Transfer_Status='" + ddl_studenttype.SelectedValue + "' and Status='1' and admissionserialnumber not in (select distinct Addmission_no from dbo.[Student_Payment_History] where  Session='" + ddl_session.SelectedItem.Text + "')) t where Dues_amt>0 order by admission_idate,class,Section,rollnumber asc";
                }

                bind_grd_view(qry);
            }
            else
            {
                Alertme("Something went wrong. Please try again.", "warning");
            }
        }
        My mycode = new My();
        private void bind_grd_view(string qry)
        {
            print1.Visible = false;
            btn_excels.Visible = false;
            lbl_class22.Text = "";
            DataTable dt = mycode.FillData(qry);
            ViewState["datatable"] = dt;
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
                if (ViewState["Is_Print"].ToString() == "1")
                {
                    print1.Visible = true;
                }
                else
                {
                    print1.Visible = false;
                }
                if (ViewState["Is_Download"].ToString() == "1")
                {
                    btn_excels.Visible = true;
                }
                else
                {
                    btn_excels.Visible = false;
                }
            }
        }
        protected void btn_excels_Click(object sender, EventArgs e)
        {
            string excelname = My.with_excel_name("Student_List_Not_Taken_Admission");
            if (ViewState["Is_Download"].ToString() == "1")
            {
                try
                {
                    if (ViewState["datatable"] != null)
                    {
                        DataTable dt = ViewState["datatable"] as DataTable;
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            // Build CSV content
                            StringBuilder sb = new StringBuilder();
                            // Add column headers
                            for (int i = 0; i < dt.Columns.Count; i++)
                            {
                                sb.Append(dt.Columns[i].ColumnName);
                                if (i < dt.Columns.Count - 1)
                                    sb.Append(",");
                            }
                            sb.AppendLine();
                            // Add rows
                            foreach (DataRow row in dt.Rows)
                            {
                                for (int i = 0; i < dt.Columns.Count; i++)
                                {
                                    // Escape commas and quotes
                                    string value = row[i].ToString().Replace("\"", "\"\"");
                                    sb.Append("\"" + value + "\"");

                                    if (i < dt.Columns.Count - 1)
                                        sb.Append(",");
                                }
                                sb.AppendLine();
                            }
                            // Send CSV file to browser
                            Response.Clear();
                            Response.Buffer = true;
                            Response.AddHeader("content-disposition", "attachment;filename=" + excelname + ".csv");
                            Response.Charset = "";
                            Response.ContentType = "text/csv";
                            Response.Output.Write(sb.ToString());
                            Response.Flush();
                            Response.End();
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
            else
            {
                Alertme(My.get_restricted_message(), "warning");
            }
        }
    }
}