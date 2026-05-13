using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class total_outstanding_message : System.Web.UI.Page
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
                        Dictionary<string, object> dc3 = My.get_push_credantial();
                        ViewState["type"] = (String)dc3["type"];
                        ViewState["project_id"] = (String)dc3["project_id"];
                        ViewState["private_key_id"] = (String)dc3["private_key_id"];
                        ViewState["client_email"] = (String)dc3["client_email"];
                        ViewState["client_id"] = (String)dc3["client_id"];
                        ViewState["private_key"] = dc3["private_key"].ToString().Replace("\\n", "\n");


                        Dictionary<string, object> dc2 = mycode.Firm_details();
                        ViewState["firm_name"] = (String)dc2["firm_name"];
                        #region sms
                        Dictionary<string, object> autosms = mycode.get_auto_message_template("Month Dues");
                        ViewState["SMS_Tempate"] = (String)autosms["SMS_Tempate"];
                        ViewState["VariableName"] = (String)autosms["VariableName"];
                        ViewState["SMSType"] = (String)autosms["SMSType"];
                        ViewState["Send_From"] = (String)autosms["Send_From"];
                        ViewState["Is_Send_SMS"] = (String)autosms["Is_Send_SMS"];
                        ViewState["Is_Send_WhatsApp"] = (String)autosms["Is_Send_WhatsApp"];
                        ViewState["Wid"] = (String)autosms["Wid"];
                        pnl_0.Visible = pnl_1.Visible = pnl_2.Visible = pnl_3.Visible = pnl_4.Visible = pnl_5.Visible = pnl_6.Visible = pnl_7.Visible = pnl_8.Visible = false;
                        var vrls = ViewState["VariableName"].ToString().Split(',');
                        txt_message.Text = ViewState["SMS_Tempate"].ToString();
                        if (vrls.Length > 0)
                        {
                            pnl_0.Visible = true;
                            lbl_0.Text = vrls[0];
                            txt_0.Text = "";
                        }
                        if (vrls.Length > 1)
                        {
                            pnl_1.Visible = true;
                            lbl_1.Text = vrls[1];
                            txt_1.Text = "";
                        }
                        if (vrls.Length > 2)
                        {
                            pnl_2.Visible = true;
                            lbl_2.Text = vrls[2];
                            txt_2.Text = "";
                        }
                        if (vrls.Length > 3)
                        {
                            pnl_3.Visible = true;
                            lbl_3.Text = vrls[3];
                            txt_3.Text = "";
                        }
                        if (vrls.Length > 4)
                        {
                            pnl_4.Visible = true;
                            lbl_4.Text = vrls[4];
                            txt_4.Text = "";
                        }
                        if (vrls.Length > 5)
                        {
                            pnl_5.Visible = true;
                            lbl_5.Text = vrls[5];
                            txt_5.Text = "";
                        }
                        if (vrls.Length > 6)
                        {
                            pnl_6.Visible = true;
                            lbl_6.Text = vrls[6];
                            txt_6.Text = "";
                        }
                        if (vrls.Length > 7)
                        {
                            pnl_7.Visible = true;
                            lbl_7.Text = vrls[7];
                            txt_7.Text = "";
                        }
                        if (vrls.Length > 8)
                        {
                            pnl_8.Visible = true;
                            lbl_8.Text = vrls[8];
                            txt_8.Text = "";
                        }
                        #endregion
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
            if (dt.Rows.Count > 0)
            {
                imglogo.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_emaiid.Text = dt.Rows[0]["email"].ToString();
                lbl_website.Text = dt.Rows[0]["website"].ToString();
                lbl_contact_details.Text = dt.Rows[0]["contact_no"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();
                ViewState["firm_idS"] = dt.Rows[0]["firm_id"].ToString();

                try
                {
                    ViewState["Fee_collect_mode"] = "Monthly";
                    if (dt.Rows[0]["Fee_collect_mode"].ToString() == "Installment")
                    {
                        ViewState["Fee_collect_mode"] = "Installment";
                        mycode.bind_all_ddl_with_id(ddl_month, "select Intstallment_name,Intstallment_name as Position from Fee_installment_master");
                    }
                    else
                    {
                        mycode.bind_all_ddl_with_id(ddl_month, "select Month,Position from Month_Index order by Position asc");
                    }
                }
                catch (Exception ex)
                {
                }
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
                    Alertme("Please select month/installment.", "warning");
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
            string monthPosition = ddl_month.SelectedValue;
            if (ViewState["Fee_collect_mode"].ToString() == "Installment")
            {
                DataTable dtinst = My.dataTable("select * from Fee_installment_master where Intstallment_name='" + ddl_month.SelectedItem.Text + "'");
                if (dtinst.Rows.Count > 0)
                {
                    foreach (DataRow drinst in dtinst.Rows)
                    {
                        string Month_position_no = drinst["Month_position_no"].ToString();
                        string[] stringSeparatorss = new string[] { "," };
                        string[] arr = Month_position_no.Split(stringSeparatorss, StringSplitOptions.None);
                        foreach (string value in arr)
                        {
                            monthPosition = value;
                        }
                    }
                }
            }

            string qry = "";
            if (ddlclass.SelectedItem.Text == "ALL")
            {
                lbl_class22.Text = "Dues List Up To " + ddl_month.SelectedItem.Text;
                if (ddl_std_type.SelectedValue == "1")  // onFoot
                {
                    qry = "select CASE WHEN hosteltaken = 'Yes' THEN 'Hostel' WHEN transportationtaken = 'Yes' THEN 'Bus' ELSE 'Days' END AS Type,t1.mobilenumber,t1.class,t1.admissionserialnumber,t1.rollnumber,t1.Section,t1.session,t1.Father_whatsApp_no,t1.gcm_id,t1.studentname,t1.fathername,t1.Hostel_roll_no,convert(float, (select sum(convert(float, Dues_amt)) from Student_wise_dues_amount where Session_id=t1.Session_id and Class_id=t1.Class_id and Admission_no=t1.admissionserialnumber and Month_position<=" + monthPosition + ")) as Total_dues from admission_registor t1 join Add_course_table t2 on t1.Class_id=t2.course_id where Session_id='" + ddlsession.SelectedValue + "' and Status=1 and t1.transportationtaken in ('No', 'NO') and t1.hosteltaken in ('No', 'NO') order by t2.Position,t1.Section,t1.rollnumber asc";
                }
                else if (ddl_std_type.SelectedValue == "2")  // Transport
                {
                    qry = "select CASE WHEN hosteltaken = 'Yes' THEN 'Hostel' WHEN transportationtaken = 'Yes' THEN 'Bus' ELSE 'Days' END AS Type,t1.mobilenumber,t1.class,t1.admissionserialnumber,t1.rollnumber,t1.Section,t1.session,t1.Father_whatsApp_no,t1.gcm_id,t1.studentname,t1.fathername,t1.Hostel_roll_no,convert(float, (select sum(convert(float, Dues_amt)) from Student_wise_dues_amount where Session_id=t1.Session_id and Class_id=t1.Class_id and Admission_no=t1.admissionserialnumber and Month_position<=" + monthPosition + ")) as Total_dues from admission_registor t1 join Add_course_table t2 on t1.Class_id=t2.course_id where Session_id='" + ddlsession.SelectedValue + "' and Status=1 and t1.transportationtaken='Yes' order by t2.Position,t1.Section,t1.rollnumber asc";
                }
                else if (ddl_std_type.SelectedValue == "3")  // Hostel
                {
                    qry = "select CASE WHEN hosteltaken = 'Yes' THEN 'Hostel' WHEN transportationtaken = 'Yes' THEN 'Bus' ELSE 'Days' END AS Type,t1.mobilenumber,t1.class,t1.admissionserialnumber,t1.rollnumber,t1.Section,t1.session,t1.Father_whatsApp_no,t1.gcm_id,t1.studentname,t1.fathername,t1.Hostel_roll_no,convert(float, (select sum(convert(float, Dues_amt)) from Student_wise_dues_amount where Session_id=t1.Session_id and Class_id=t1.Class_id and Admission_no=t1.admissionserialnumber and Month_position<=" + monthPosition + ")) as Total_dues from admission_registor t1 join Add_course_table t2 on t1.Class_id=t2.course_id where Session_id='" + ddlsession.SelectedValue + "' and Status=1 and t1.hosteltaken='Yes' order by t2.Position,t1.Section,t1.rollnumber asc";
                }
                else
                {
                    qry = "select CASE WHEN hosteltaken = 'Yes' THEN 'Hostel' WHEN transportationtaken = 'Yes' THEN 'Bus' ELSE 'Days' END AS Type,t1.mobilenumber,t1.class,t1.admissionserialnumber,t1.rollnumber,t1.Section,t1.session,t1.Father_whatsApp_no,t1.gcm_id,t1.studentname,t1.fathername,t1.Hostel_roll_no,convert(float, (select sum(convert(float, Dues_amt)) from Student_wise_dues_amount where Session_id=t1.Session_id and Class_id=t1.Class_id and Admission_no=t1.admissionserialnumber and Month_position<=" + monthPosition + ")) as Total_dues from admission_registor t1 join Add_course_table t2 on t1.Class_id=t2.course_id where Session_id='" + ddlsession.SelectedValue + "' and Status=1 order by t2.Position,t1.Section,t1.rollnumber asc";
                }
            }
            else if (ddlclass.SelectedItem.Text != "ALL" && ddl_section.SelectedItem.Text == "ALL")
            {
                lbl_class22.Text = "Dues List for Class " + ddlclass.SelectedItem.Text + " Up To " + ddl_month.SelectedItem.Text;
                if (ddl_std_type.SelectedValue == "1")  // onFoot
                {
                    qry = "select CASE WHEN hosteltaken = 'Yes' THEN 'Hostel' WHEN transportationtaken = 'Yes' THEN 'Bus' ELSE 'Days' END AS Type,t1.mobilenumber,t1.class,t1.admissionserialnumber,t1.rollnumber,t1.Section,t1.session,t1.studentname,t1.Father_whatsApp_no,t1.gcm_id,t1.fathername,t1.Hostel_roll_no,convert(float, (select sum(convert(float, Dues_amt)) from Student_wise_dues_amount where Session_id=t1.Session_id and Class_id=t1.Class_id and Admission_no=t1.admissionserialnumber and Month_position<=" + monthPosition + ")) as Total_dues from admission_registor t1 join Add_course_table t2 on t1.Class_id=t2.course_id where  Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Status=1 and t1.transportationtaken in ('No', 'NO') and t1.hosteltaken in ('No', 'NO') order by t2.Position,t1.Section,t1.rollnumber asc";
                }
                else if (ddl_std_type.SelectedValue == "2")  // Transport
                {
                    qry = "select CASE WHEN hosteltaken = 'Yes' THEN 'Hostel' WHEN transportationtaken = 'Yes' THEN 'Bus' ELSE 'Days' END AS Type,t1.mobilenumber,t1.class,t1.admissionserialnumber,t1.rollnumber,t1.Section,t1.session,t1.studentname,t1.Father_whatsApp_no,t1.gcm_id,t1.fathername,t1.Hostel_roll_no,convert(float, (select sum(convert(float, Dues_amt)) from Student_wise_dues_amount where Session_id=t1.Session_id and Class_id=t1.Class_id and Admission_no=t1.admissionserialnumber and Month_position<=" + monthPosition + ")) as Total_dues from admission_registor t1 join Add_course_table t2 on t1.Class_id=t2.course_id where  Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Status=1 and t1.transportationtaken='Yes' order by t2.Position,t1.Section,t1.rollnumber asc";
                }
                else if (ddl_std_type.SelectedValue == "3")  // Hostel
                {
                    qry = "select CASE WHEN hosteltaken = 'Yes' THEN 'Hostel' WHEN transportationtaken = 'Yes' THEN 'Bus' ELSE 'Days' END AS Type,t1.mobilenumber,t1.class,t1.admissionserialnumber,t1.rollnumber,t1.Section,t1.session,t1.studentname,t1.Father_whatsApp_no,t1.gcm_id,t1.fathername,t1.Hostel_roll_no,convert(float, (select sum(convert(float, Dues_amt)) from Student_wise_dues_amount where Session_id=t1.Session_id and Class_id=t1.Class_id and Admission_no=t1.admissionserialnumber and Month_position<=" + monthPosition + ")) as Total_dues from admission_registor t1 join Add_course_table t2 on t1.Class_id=t2.course_id where  Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Status=1 and t1.hosteltaken='Yes' order by t2.Position,t1.Section,t1.rollnumber asc";
                }
                else
                {
                    qry = "select CASE WHEN hosteltaken = 'Yes' THEN 'Hostel' WHEN transportationtaken = 'Yes' THEN 'Bus' ELSE 'Days' END AS Type,t1.mobilenumber,t1.class,t1.admissionserialnumber,t1.rollnumber,t1.Section,t1.session,t1.studentname,t1.Father_whatsApp_no,t1.gcm_id,t1.fathername,t1.Hostel_roll_no,convert(float, (select sum(convert(float, Dues_amt)) from Student_wise_dues_amount where Session_id=t1.Session_id and Class_id=t1.Class_id and Admission_no=t1.admissionserialnumber and Month_position<=" + monthPosition + ")) as Total_dues from admission_registor t1 join Add_course_table t2 on t1.Class_id=t2.course_id where  Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Status=1 order by t2.Position,t1.Section,t1.rollnumber asc";
                }
            }
            else
            {
                lbl_class22.Text = "Dues List for Class " + ddlclass.SelectedItem.Text + " Section " + ddl_section.Text + " Up To " + ddl_month.SelectedItem.Text;
                if (ddl_std_type.SelectedValue == "1")  // onFoot
                {
                    qry = "select CASE WHEN hosteltaken = 'Yes' THEN 'Hostel' WHEN transportationtaken = 'Yes' THEN 'Bus' ELSE 'Days' END AS Type,t1.mobilenumber,t1.class,t1.admissionserialnumber,t1.rollnumber,t1.Section,t1.session,t1.studentname,t1.Father_whatsApp_no,t1.gcm_id,t1.fathername,t1.Hostel_roll_no,convert(float, (select sum(convert(float, Dues_amt)) from Student_wise_dues_amount where Session_id=t1.Session_id and Class_id=t1.Class_id and Admission_no=t1.admissionserialnumber and Month_position<=" + monthPosition + ")) as Total_dues from admission_registor t1 join Add_course_table t2 on t1.Class_id=t2.course_id where  Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and Status=1 and t1.transportationtaken in ('No', 'NO') and t1.hosteltaken in ('No', 'NO') order by t2.Position,t1.Section,t1.rollnumber asc";
                }
                else if (ddl_std_type.SelectedValue == "2")  // Transport
                {
                    qry = "select CASE WHEN hosteltaken = 'Yes' THEN 'Hostel' WHEN transportationtaken = 'Yes' THEN 'Bus' ELSE 'Days' END AS Type,t1.mobilenumber,t1.class,t1.admissionserialnumber,t1.rollnumber,t1.Section,t1.session,t1.studentname,t1.Father_whatsApp_no,t1.gcm_id,t1.fathername,t1.Hostel_roll_no,convert(float, (select sum(convert(float, Dues_amt)) from Student_wise_dues_amount where Session_id=t1.Session_id and Class_id=t1.Class_id and Admission_no=t1.admissionserialnumber and Month_position<=" + monthPosition + ")) as Total_dues from admission_registor t1 join Add_course_table t2 on t1.Class_id=t2.course_id where  Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and Status=1 and t1.transportationtaken='Yes' order by t2.Position,t1.Section,t1.rollnumber asc";
                }
                else if (ddl_std_type.SelectedValue == "3")  // Hostel
                {
                    qry = "select CASE WHEN hosteltaken = 'Yes' THEN 'Hostel' WHEN transportationtaken = 'Yes' THEN 'Bus' ELSE 'Days' END AS Type,t1.mobilenumber,t1.class,t1.admissionserialnumber,t1.rollnumber,t1.Section,t1.session,t1.studentname,t1.Father_whatsApp_no,t1.gcm_id,t1.fathername,t1.Hostel_roll_no,convert(float, (select sum(convert(float, Dues_amt)) from Student_wise_dues_amount where Session_id=t1.Session_id and Class_id=t1.Class_id and Admission_no=t1.admissionserialnumber and Month_position<=" + monthPosition + ")) as Total_dues from admission_registor t1 join Add_course_table t2 on t1.Class_id=t2.course_id where  Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and Status=1  and t1.hosteltaken='Yes' order by t2.Position,t1.Section,t1.rollnumber asc";
                }
                else
                {
                    qry = "select CASE WHEN hosteltaken = 'Yes' THEN 'Hostel' WHEN transportationtaken = 'Yes' THEN 'Bus' ELSE 'Days' END AS Type,t1.mobilenumber,t1.class,t1.admissionserialnumber,t1.rollnumber,t1.Section,t1.session,t1.studentname,t1.Father_whatsApp_no,t1.gcm_id,t1.fathername,t1.Hostel_roll_no,convert(float, (select sum(convert(float, Dues_amt)) from Student_wise_dues_amount where Session_id=t1.Session_id and Class_id=t1.Class_id and Admission_no=t1.admissionserialnumber and Month_position<=" + monthPosition + ")) as Total_dues from admission_registor t1 join Add_course_table t2 on t1.Class_id=t2.course_id where  Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and Status=1 order by t2.Position,t1.Section,t1.rollnumber asc";
                } 
            }

            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {
                sendsms.Visible = false;
                lbl_ttl_dues.Text = "0.00";
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                sendsms.Visible = true;
                ViewState["monthName"] = ddl_month.SelectedItem.Text;
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
                    Response.AddHeader("content-disposition", "attachment;filename=Dues_list" + mycode.date() + "_" + mycode.time() + ".xls");
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
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session.", "warning");
                    ddlsession.Focus();
                    return;
                }
                else if (ddl_month.SelectedItem.Text == "Select")
                {
                    Alertme("Please select month/installment.", "warning");
                    ddl_month.Focus();
                    return;
                }
                bool flag = false;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
                {
                    SqlConnection con = new SqlConnection(My.conn);
                    con.Open();
                    string qry = "";
                    if (ddlclass.SelectedItem.Text == "ALL")
                    {
                        if (ddl_std_type.SelectedValue == "1")  // onFoot
                        {
                            qry = "select Session_id,admissionserialnumber,Class_id from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Status=1 and transportationtaken in ('No', 'NO') and hosteltaken in ('No', 'NO')";
                        }
                        else if (ddl_std_type.SelectedValue == "2")  // Transport
                        {
                            qry = "select Session_id,admissionserialnumber,Class_id from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Status=1 and transportationtaken='Yes'";
                        }
                        else if (ddl_std_type.SelectedValue == "3")  // Hostel
                        {
                            qry = "select Session_id,admissionserialnumber,Class_id from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Status=1 and hosteltaken='Yes'";
                        }
                        else
                        {
                            qry = "select Session_id,admissionserialnumber,Class_id from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Status=1";
                        }
                    }
                    else if (ddlclass.SelectedItem.Text != "ALL" && ddl_section.SelectedItem.Text == "ALL")
                    {
                        if (ddl_std_type.SelectedValue == "1")  // onFoot
                        {
                            qry = "select Session_id,admissionserialnumber,Class_id from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Status=1 and transportationtaken in ('No', 'NO') and hosteltaken in ('No', 'NO')";
                        }
                        else if (ddl_std_type.SelectedValue == "2")  // Transport
                        {
                            qry = "select Session_id,admissionserialnumber,Class_id from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Status=1 and transportationtaken='Yes'";
                        }
                        else if (ddl_std_type.SelectedValue == "3")  // Hostel
                        {
                            qry = "select Session_id,admissionserialnumber,Class_id from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Status=1 and hosteltaken='Yes'";
                        }
                        else
                        {
                            qry = "select Session_id,admissionserialnumber,Class_id from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Status=1";
                        }
                        //qry = "select Session_id,admissionserialnumber,Class_id from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Status=1";
                    }
                    else
                    {
                        if (ddl_std_type.SelectedValue == "1")  // onFoot
                        {
                            qry = "select Session_id,admissionserialnumber,Class_id from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and Status=1 and transportationtaken in ('No', 'NO') and hosteltaken in ('No', 'NO')";
                        }
                        else if (ddl_std_type.SelectedValue == "2")  // Transport
                        {
                            qry = "select Session_id,admissionserialnumber,Class_id from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and Status=1 and transportationtaken='Yes'";
                        }
                        else if (ddl_std_type.SelectedValue == "3")  // Hostel
                        {
                            qry = "select Session_id,admissionserialnumber,Class_id from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and Status=1 and hosteltaken='Yes'";
                        }
                        else
                        {
                            qry = "select Session_id,admissionserialnumber,Class_id from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and Status=1";
                        }
                        // qry = "select Session_id,admissionserialnumber,Class_id from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and Status=1";
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



        #region MESSAGE SENDING
        protected void btn_msgPreview_Click(object sender, EventArgs e)
        {
            try
            {
                var vrls = ViewState["VariableName"].ToString().Split(',');
                var lst = new String[vrls.Length];
                if (vrls.Length > 0)
                {
                    lst[0] = txt_0.Text;
                }
                if (vrls.Length > 1)
                {
                    lst[1] = txt_1.Text;
                }
                if (vrls.Length > 2)
                {
                    lst[2] = txt_2.Text;
                }
                if (vrls.Length > 3)
                {
                    lst[3] = txt_3.Text;
                }
                if (vrls.Length > 4)
                {
                    lst[4] = txt_4.Text;
                }
                if (vrls.Length > 5)
                {

                    lst[5] = txt_5.Text;
                }

                txt_message.Text = String.Format(ViewState["SMS_Tempate"].ToString(), lst);
                //if (ViewState["firm_idS"].ToString() == "DEEP-1")
                //{
                //    txt_message.Text = "From : Deep English School, Kadampur Mohallah, Katihar. \nDear parent, \nThe bill of Master / Miss xxxx, Class xxxx, Adm. no. xxxx, Roll no.Xxx for / upto the Month xxx  is Rs. Xxx,  \nSo, please pay the total bill soon. 🙏";
                //}
                //if (ViewState["firm_idS"].ToString() == "DEEP-2")
                //{
                //    txt_message.Text = "From : Deep A+ Classes, Kadampur Mohallah, Katihar. \nDear parent, \nThe bill of Master / Miss xxxx, Class xxxx, Adm. no. xxxx, Roll no.Xxx for / upto the Month xxx  is Rs. Xxx,  \nSo, please pay the total bill soon. 🙏";
                //}
                //if (ViewState["firm_idS"].ToString() == "My-001" || ViewState["firm_idS"].ToString() == "My-003" || ViewState["firm_idS"].ToString() == "Ndeep-001" || ViewState["firm_idS"].ToString() == "Ndeep-002")
                //{
                //    txt_message.Text = "From : New Deep English School, Amla Tola, Katihar. \nDear parent, \nThe bill of Master / Miss xxxx, Class xxxx, Adm. no. xxxx, Roll no.Xxx for / upto the Month xxx  is Rs. Xxx,  \nSo, please pay the total bill soon. 🙏";
                //}
                pnl_msg.Visible = true;
            }
            catch (Exception ex)
            {

            }
        }
        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                bool send = false;
                if (rd_sms.Checked == true)
                {
                    var dt = mycode.FillData("select top 1 * from message_config where Status='running'");
                    if (dt.Rows.Count == 1)
                    {
                        ViewState["api_key"] = dt.Rows[0]["uid"].ToString();
                        ViewState["Sender_id"] = dt.Rows[0]["sender"].ToString();
                        send = true;
                    }
                    else
                    {
                        this.Alertme("Please set sms configuration", "warning");
                        return;

                    }
                }
                else if (rd_whatassp.Checked == true)
                {
                    var dt = mycode.FillData("select top 1 * from Whatsapp_api_config where Status='running'");
                    if (dt.Rows.Count == 1)
                    {
                        send = true;
                        ViewState["whatsapp_mobile_no"] = dt.Rows[0]["SMS_API"].ToString();
                        ViewState["Whatsapp_api_url"] = dt.Rows[0]["url"].ToString();
                    }
                    else
                    {
                        this.Alertme("Please set Whatsapp configuration", "warning");
                        return;
                    }
                }
                else
                {
                    send = true;
                }



                if (send == true)
                {
                    string confirmValue = string.Empty;
                    confirmValue = Request.Form["confirm_value"];
                    if (confirmValue == "Yes")
                    {
                        bool isChecked = false;
                        int growcounts = rd_view.Items.Count;
                        for (int i = 0; i < growcounts; i++)
                        {
                            CheckBox chk = (CheckBox)rd_view.Items[i].FindControl("chkRowData");
                            if (chk.Checked == true)
                            {
                                isChecked = true;
                            }
                        }

                        if (isChecked == true)
                        {
                            int growcount = rd_view.Items.Count;
                            for (int j = 0; j < growcount; j++)
                            {
                                CheckBox chk = (CheckBox)rd_view.Items[j].FindControl("chkRowData");
                                if (chk.Checked == true)
                                {
                                    Label lbl_admissionserialnumber = (Label)rd_view.Items[j].FindControl("lbl_admissionserialnumber");
                                    Label lbl_student_name = (Label)rd_view.Items[j].FindControl("lbl_student_name");
                                    Label lbl_class = (Label)rd_view.Items[j].FindControl("lbl_class");
                                    Label lbl_section = (Label)rd_view.Items[j].FindControl("lbl_section");
                                    Label lbl_roll_no = (Label)rd_view.Items[j].FindControl("lbl_roll_no");
                                    Label lbl_f_mobile_no = (Label)rd_view.Items[j].FindControl("lbl_f_mobile_no");
                                    Label lbl_gcm_id = (Label)rd_view.Items[j].FindControl("lbl_gcm_id");
                                    Label lbl_todal_dues_amt = (Label)rd_view.Items[j].FindControl("lbl_todal_dues_amt");

                                    if (My.toDouble(lbl_todal_dues_amt.Text) > 0)
                                    {
                                        if (rd_notification.Checked == true)
                                        {
                                            sendSMS(lbl_admissionserialnumber.Text, lbl_class.Text, lbl_f_mobile_no.Text, lbl_section.Text, lbl_roll_no.Text, lbl_student_name.Text, lbl_todal_dues_amt.Text, ViewState["monthName"].ToString(), lbl_gcm_id.Text);
                                        }
                                        else
                                        {
                                            if (lbl_f_mobile_no.Text != "")
                                            {
                                                sendSMS(lbl_admissionserialnumber.Text, lbl_class.Text, lbl_f_mobile_no.Text, lbl_section.Text, lbl_roll_no.Text, lbl_student_name.Text, lbl_todal_dues_amt.Text, ViewState["monthName"].ToString(), lbl_gcm_id.Text);
                                            }
                                        }
                                    }
                                }
                            }
                            this.Alertme("SMS Send Successfully", "success");
                            pnl_msg.Visible = false;
                            sendsms.Visible = false;
                        }
                        else
                        {
                            Alertme("Please select the student you would like to send the message to.", "warning");
                        }
                    }
                    else
                    {
                        Alertme("Sorry you have clicked no", "warning");
                    }
                }
                else
                {
                    this.Alertme("Please check sms and whatsapp configuration", "warning");
                }
            }
            catch
            {

            }


        }

        private void sendSMS(string admission_no, string class_name, string father_mob, string section, string rollnumber, string student_Name, string total, string monthname, string gcmId)
        {
            if (father_mob == "N/A")
            {

            }
            else if (father_mob == "")
            {

            }
            else if (My.toDouble(total) == 0)
            {

            }
            else
            {
                string mobno = father_mob;
                string admissionId = admission_no;
                txt_0.Text = student_Name;
                txt_1.Text = class_name;
                txt_2.Text = admissionId;
                txt_3.Text = rollnumber;
                txt_4.Text = monthname;
                txt_5.Text = total;


                string type = "";
                type = "english";
                try
                {
                    if (ViewState["SMSType"].ToString() == "Unicode")
                    {
                        type = "unicode";
                    }
                    else
                    {
                        type = "english";
                    }
                }
                catch (Exception ex)
                {
                }


                ViewState["type"] = type;
                var vrls = ViewState["VariableName"].ToString().Split(',');
                var lst = new String[vrls.Length];
                if (vrls.Length > 0)
                {
                    lst[0] = txt_0.Text;
                }
                if (vrls.Length > 1)
                {
                    lst[1] = txt_1.Text;
                }
                if (vrls.Length > 2)
                {
                    lst[2] = txt_2.Text;
                }
                if (vrls.Length > 3)
                {

                    lst[3] = txt_3.Text;
                }
                if (vrls.Length > 4)
                {

                    lst[4] = txt_4.Text;
                }
                if (vrls.Length > 5)
                {

                    lst[5] = txt_5.Text;
                }
                txt_message.Text = String.Format(ViewState["SMS_Tempate"].ToString(), lst);
                try
                {
                    if (rd_sms.Checked == true)
                    {
                        string api_key = ViewState["api_key"].ToString();
                        string Sender_id = ViewState["Sender_id"].ToString();
                        string msgtype = ViewState["type"].ToString();
                        string url = "http://mysms.msgclub.net/rest/services/sendSMS/sendGroupSms?AUTH_KEY=" + api_key + "&message=" + txt_message.Text + "&senderId=" + Sender_id + "&routeId=1&mobileNos=" + mobno + "&smsContentType=" + type;
                        HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(url);
                        HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();
                        StreamReader sr = new StreamReader(httpres.GetResponseStream());
                        string results = sr.ReadToEnd();
                        sr.Close();
                        this.Alertme("SMS Send Successfully", "success");
                        My.Insert("Message_Details", new
                        {
                            Mobile_No = mobno,
                            Message = txt_message.Text,
                            Date = mycode.date(),
                            Idate = mycode.idate(),
                            Time = mycode.time(),
                            Result = results,
                            User_id = ViewState["Userid"].ToString(),
                            Mesage_Type = msgtype,
                            Groupcode = "SMS",
                            Status = "SEND",
                            Url = url,
                            Message_to_Type = "Student",
                            admin_user_id = admissionId,
                        });
                    }
                    else if (rd_whatassp.Checked == true)
                    {


                        var query = new Dictionary<string, string>()
    {
        { "authkey", ViewState["whatsapp_mobile_no"].ToString() },
        { "mobile", mobno },
        { "country_code", "91" },
        { "wid", ViewState["Wid"].ToString()},
        { "1", lst[0] },
        { "2", lst[1]},
        { "3", lst[2] },
        { "4", lst[3] },
        { "5", lst[4] },
        { "6", lst[5] },

                };
                        string sms = String.Format(ViewState["SMS_Tempate"].ToString(), lst);
                        string url = ViewState["Whatsapp_api_url"].ToString();
                        string fullUrl = url + "?" + string.Join("&",
                        query.Select(kvp => kvp.Key + "=" + Uri.EscapeDataString(kvp.Value)));
                        txt_message.Text = sms;





                        //if (ViewState["firm_idS"].ToString() == "DEEP-1")
                        //{
                        //    sms = "From : Deep English School, Kadampur Mohallah, Katihar. \nDear parent, \nThe bill of Master/Miss " + student_Name + ", Class " + class_name + ", Adm. no. " + admission_no + ", Roll no. " + rollnumber + " / upto the Month " + monthname + "  is Rs. " + total + ", \nSo, please pay the total bill soon. 🙏";
                        //    txt_message.Text = sms;
                        //}
                        //if (ViewState["firm_idS"].ToString() == "DEEP-2")
                        //{
                        //    sms = "From : Deep A+ Classes, Kadampur Mohallah, Katihar. \nDear parent, \nThe bill of Master/Miss " + student_Name + ", Class " + class_name + ", Adm. no. " + admission_no + ", Roll no. " + rollnumber + " / upto the Month " + monthname + "  is Rs. " + total + ", \nSo, please pay the total bill soon. 🙏";
                        //    txt_message.Text = sms;
                        //}
                        //if (ViewState["firm_idS"].ToString() == "My-001" || ViewState["firm_idS"].ToString() == "My-003" || ViewState["firm_idS"].ToString() == "Ndeep-001" || ViewState["firm_idS"].ToString() == "Ndeep-002")
                        //{
                        //    sms = "From : New Deep English School, Amla Tola, Katihar. \nDear parent, \nThe bill of Master/Miss " + student_Name + ", Class " + class_name + ", Adm. no. " + admission_no + ", Roll no. " + rollnumber + " / upto the Month " + monthname + "  is Rs. " + total + ", \nSo, please pay the total bill soon. 🙏";
                        //    txt_message.Text = sms;
                        //}
                        try
                        {
                            if (father_mob.Length > 9)
                            {
                                My.Insert("WhatsApp_send", new
                                {
                                    Mobile_no = mobno,
                                    Message = txt_message.Text,
                                    Message_url = fullUrl,
                                    Session_id = ddlsession.SelectedValue,
                                    Admission_no = admissionId,
                                    Status = "Pending",
                                    Date = mycode.date(),
                                    Idate = mycode.idate(),
                                    Time = mycode.time(),
                                    Send_by = ViewState["Userid"].ToString(),
                                    Mesage_Type = ViewState["type"].ToString(),
                                });
                            }
                            //return true;
                        }
                        catch (Exception ex)
                        {
                            My.submitexception("Exception from Whatsapp Message =" + ex.ToString());
                            //return false;
                        }
                    }
                    else
                    {
                        if (gcmId == "")
                        {
                            gcmId = "0";
                        }
                        if (gcmId != "")
                        {
                            Dictionary<String, String> ss = new Dictionary<string, string>();
                            ss["notification_id"] = Guid.NewGuid().ToString();
                            ss["message"] = txt_message.Text;
                            ss["title"] = "Dues Message";
                            ss["messagetype"] = "Message";
                            ss["url"] = "";
                            ss["link_url"] = "";
                            ss["UserId"] = admissionId;
                            ss["type"] = "service_account";
                            ss["project_id"] = ViewState["project_id"].ToString();
                            ss["private_key_id"] = ViewState["private_key_id"].ToString();
                            ss["client_email"] = ViewState["client_email"].ToString();
                            ss["client_id"] = ViewState["client_id"].ToString();
                            ss["private_key"] = ViewState["private_key"].ToString();
                            My.onlypush(gcmId, ss);
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.Alertme(ex.Message, "warning");
                }
            }
        }
        #endregion
    }
}