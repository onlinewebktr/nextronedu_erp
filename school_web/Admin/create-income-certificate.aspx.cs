using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class create_income_certificate : System.Web.UI.Page
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
                        DataTable dtF = My.dataTable("select firm_name,firm_id from Firm_Details");
                        ViewState["firm_id"] = dtF.Rows[0]["firm_id"].ToString();
                        string pagename_current = "certificate-creation.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);

                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];

                        ViewState["gcmid"] = "";
                        ViewState["stdnT"] = "0";
                        mycode.bind_all_ddl_with_id(ddl_session, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                        ddl_session.SelectedValue = My.get_session_id();
                        mycode.bind_all_ddl_with_id(ddlclass, "Select Course_Name,course_id from Add_course_table order by Position asc");
                        Bind_all_pending_list_for_approved();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "DOB_certificate");
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

        #region Approval For pending character certificate admisn
        private void Bind_all_pending_list_for_approved()
        {
            string query = " select ar.*,aft.Reply_remarks,format(aft.Reply_datetime, 'dd/MM/yyyy hh:mm:ss tt') as Reply_datetime1,aft.Id from dbo.[admission_registor] ar join Apply_For_TC aft on aft.Admission_no = ar.admissionserialnumber and aft.Session_id = ar.Session_id  where ar.Session_id = '" + ddl_session.SelectedValue + "' and ar.Transfer_Status in ('New', 'NT') and ar.Status = '1' and ar.admissionserialnumber not IN(SELECT Admission_no FROM Certificate_Master WHERE Admission_no=ar.admissionserialnumber and Class_id=ar.Class_id and Session_id=ar.Session_id and Certificate_type='Income') and ar.admissionserialnumber in(Select Admission_no from Apply_For_TC where Apply_For = 'Income Certificate') and aft.Apply_For = 'Income Certificate'  order by ar.id desc";
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count == 0)
            {
                pendingforgeneratedata.Visible = false;
                Repeater2.DataSource = null;
                Repeater2.DataBind();
            }
            else
            {
                pendingforgeneratedata.Visible = true;
                Repeater2.DataSource = dt;
                Repeater2.DataBind();

            }

        }
        protected void lnk_process_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                Label lbl_admissionserialnumber = (Label)row.FindControl("lbl_admissionserialnumber");
                Label lbl_studentname = (Label)row.FindControl("lbl_studentname");
                ViewState["id_process"] = lbl_Id.Text;

                string qry = "select top 1 * from dbo.[admission_registor] where admissionserialnumber='" + lbl_admissionserialnumber.Text + "' and   Status='1' and admissionserialnumber not IN  (SELECT Admission_no FROM Certificate_Master WHERE Admission_no=admission_registor.admissionserialnumber and Class_id=admission_registor.Class_id and Session_id=admission_registor.Session_id and Certificate_type='Income') order by id desc";
                find_details(qry);

            }
            catch
            {
            }
        }
        #endregion

        protected void btn_find_session_Click(object sender, EventArgs e)
        {
            if (ddl_session.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
            }
            else
            {
                string qry = "select * from dbo.[admission_registor] where Session_id='" + ddl_session.SelectedValue + "' and Transfer_Status in ('New','NT') and Status='1' and admissionserialnumber not IN  (SELECT Admission_no FROM Certificate_Master WHERE Admission_no=admission_registor.admissionserialnumber and Class_id=admission_registor.Class_id and Session_id=admission_registor.Session_id and Certificate_type='Income') order by id desc";
                find_details(qry);
            }

        }

        #region FindStudenT
        protected void btn_find_admission_no_Click1(object sender, EventArgs e)
        {
            try
            {
                if (ddl_session.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session", "warning");
                    ddl_session.Focus();
                    return;
                }
                if (txt_admission_no.Text == "")
                {
                    Alertme("Please Enter Admission No", "warning");
                    txt_admission_no.Focus();
                    return;
                }
                string qry = "select top 1 * from dbo.[admission_registor] where admissionserialnumber='" + txt_admission_no.Text + "' and Session_id='" + ddl_session.SelectedValue + "' and  Status='1' and admissionserialnumber not IN  (SELECT Admission_no FROM Certificate_Master WHERE Admission_no=admission_registor.admissionserialnumber and Class_id=admission_registor.Class_id and Session_id=admission_registor.Session_id and Certificate_type='Income') order by id desc";
                find_details(qry);
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnfind_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_session.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session", "warning");
                    ddl_session.Focus();
                    return;
                }
                if (ddlclass.SelectedItem.Text == "Select")
                {
                    Alertme("Please select class.", "warning");
                    ddlclass.Focus();
                    return;
                }
                if (txtrollnumber.Text == "")
                {
                    Alertme("Please enter roll no.", "warning");
                    txtrollnumber.Focus();
                    return;
                }
                string query = "select * from dbo.[admission_registor] where rollnumber='" + txtrollnumber.Text + "'  and Session_id='" + ddl_session.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'";
                DataTable dt = My.dataTable(query);
                if (dt.Rows.Count == 0)
                {
                    rp_std.DataSource = null;
                    rp_std.DataBind();
                    Alertme("Data Not Found...", "warning");
                }
                else
                {
                    rp_std.DataSource = dt;
                    rp_std.DataBind();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalSTD();", true);
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void btn_find_name_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_session.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session", "warning");
                    ddl_session.Focus();
                    return;
                }
                if (txt_student_name.Text == "")
                {
                    Alertme("Please enter student name.", "warning");
                    txt_student_name.Focus();
                    return;
                }
                string query = "select * from admission_registor where studentname like '%" + txt_student_name.Text + "%' and   Status='1' and Session_id='" + ddl_session.SelectedValue + "' order by id asc";
                DataTable dt = My.dataTable(query);
                if (dt.Rows.Count == 0)
                {
                    rp_std.DataSource = null;
                    rp_std.DataBind();
                    Alertme("Data Not Found...", "warning");
                }
                else
                {
                    rp_std.DataSource = dt;
                    rp_std.DataBind();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalSTD();", true);
                }
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }


        protected void lnk_select_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbladmissionserialnumber = (Label)row.FindControl("lbladmissionserialnumber");
                Label lbl_session_id = (Label)row.FindControl("lbl_session_id");
                string query = "select top 1 * from dbo.[admission_registor] where admissionserialnumber='" + lbladmissionserialnumber.Text + "'  and Session_id='" + lbl_session_id.Text + "' and admissionserialnumber not IN  (SELECT Admission_no FROM Certificate_Master WHERE Admission_no=admission_registor.admissionserialnumber and Class_id=admission_registor.Class_id and Session_id=admission_registor.Session_id and Certificate_type='Income') order by id desc";
                find_details(query);
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }



        private void find_details(string qryS)
        {
            DataTable dt = My.dataTable(qryS);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data list exist or certificate already created.", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
                //txt_admission_no.Text = dt.Rows[0]["admissionserialnumber"].ToString();
                //ddlclass.SelectedValue = dt.Rows[0]["Class_id"].ToString();
                //txtrollnumber.Text = dt.Rows[0]["rollnumber"].ToString();
                //txt_student_name.Text = dt.Rows[0]["studentname"].ToString();
            }
        }

        #endregion


        protected void lnkSelectStd_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_add"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_studentname = (Label)row.FindControl("lbl_studentname");
                    Label lbl_class = (Label)row.FindControl("lbl_class");
                    Label lbl_adm_no = (Label)row.FindControl("lbl_adm_no");
                    Label lbl_session_id = (Label)row.FindControl("lbl_session_id");
                    Label lbl_class_id = (Label)row.FindControl("lbl_class_id");
                    Label lbl_session_name = (Label)row.FindControl("lbl_session");
                    Label lbl_vill_city = (Label)row.FindControl("lbl_vill_city");
                    Label lbl_post_office = (Label)row.FindControl("lbl_post_office");
                    Label lbl_police_station = (Label)row.FindControl("lbl_police_station");
                    Label lbl_district = (Label)row.FindControl("lbl_district");
                    Label lbl_pincode = (Label)row.FindControl("lbl_pincode");
                    Label lbl_dob = (Label)row.FindControl("lbl_dob");
                    Label lbl_gcmid = (Label)row.FindControl("lbl_gcmid");
                    Label lbl_address = (Label)row.FindControl("lbl_address");
                    ViewState["gcmid"] = lbl_gcmid.Text;
                    ViewState["adm_no"] = lbl_adm_no.Text;
                    ViewState["sessioN"] = lbl_session_id.Text;
                    ViewState["class_id"] = lbl_class_id.Text;
                    ViewState["session_name"] = lbl_session_name.Text;
                    lbl_admisn_no.Text = lbl_adm_no.Text;
                    lbl_std_name.Text = lbl_studentname.Text;
                    lbl_classss.Text = lbl_class.Text;
                    txt_dob.Text = lbl_dob.Text;

                    string address = "";
                    //txt_address.Text = "City/Vill. : " + lbl_vill_city.Text + ", PO : " + lbl_post_office.Text + ", PS : " + lbl_police_station.Text + ", District : " + lbl_district.Text + ", Pincode : " + lbl_pincode.Text;
                    //txt_address.Text = address;

                    if (ViewState["firm_id"].ToString() == "NAVY-001")
                    {
                        txt_address.Text = lbl_address.Text;
                    }
                    else
                    {
                        if (lbl_vill_city.Text != "")
                        {
                            address = address + "City/Vill. : " + lbl_vill_city.Text + ", ";
                        }
                        if (lbl_post_office.Text != "")
                        {
                            address = address + "PO : " + lbl_post_office.Text + ", ";
                        }
                        if (lbl_police_station.Text != "")
                        {
                            address = address + "PS : " + lbl_police_station.Text + ", ";
                        }
                        if (lbl_police_station.Text != "")
                        {
                            address = address + "District : " + lbl_district.Text + ", ";
                        }
                        if (lbl_police_station.Text != "")
                        {
                            address = address + "Pincode : " + lbl_pincode.Text + ", ";
                        }
                        if (address != "")
                        {
                            try
                            {
                                txt_address.Text = address.Remove(address.Length - 2);
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                    }



                    fetch_fee_details(lbl_session_id.Text, lbl_adm_no.Text);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }

        private void fetch_fee_details(string session_id, string admission_no)
        {
            Dictionary<string, object> dc1 = My.getstudentinfo(admission_no, session_id);
            string Name = (String)dc1["Name"];
            string Session_id = (String)dc1["Session_id"];
            string Session = (String)dc1["Session"];
            string category_id = (String)dc1["category_id"];
            string sub_category_id = (String)dc1["sub_category_id"];
            string Transfer_Status = (String)dc1["Transfer_Status"];
            string Admission_no = (String)dc1["Admission_no"];
            string classname = (String)dc1["classname"];

            string hostaltaken = (String)dc1["hosteltaken"];
            string Branch_id = (String)dc1["Branch_id"];
            string Section = (String)dc1["Section"];
            string Hostel_id = (String)dc1["Hostel_id"];
            bool day_bording = My.toBool((String)dc1["is_applied_dayboarding"]);
            bool day_bording_with_lunch = My.toBool((String)dc1["day_boarding_with_lunch"]);

            string Transportationtaken = (String)dc1["transportationtaken"];


            try
            {
                string[] stringSeparatorss = new string[] { "-" };
                string[] arrs = Session.Split(stringSeparatorss, StringSplitOptions.None);
                string s_session = arrs[0];
                string e_session = arrs[1];

                string smonth = My.get_start_month();
                string emonth = My.get_end_month();

                txt_period_from.Text = smonth + " " + s_session;
                txt_year_end_month.Text = emonth + " " + e_session;
            }
            catch (Exception ex)
            {
            }


            string parameterM = ""; string parameterAD = "";
            if (Transfer_Status == "New")
            {
                parameterM = hostaltaken.ToUpper() == "YES" ? "HostelMonthlyFee" : "MonthlyFee";
                parameterAD = hostaltaken.ToUpper() == "YES" ? "HostelAdmissionFee" : "AdmissionFee";
            }
            else
            {
                parameterM = hostaltaken.ToUpper() == "YES" ? "HostelMonthlyFee" : "MonthlyFee";
                parameterAD = hostaltaken.ToUpper() == "YES" ? "HostelAnnualFee" : "AnnualFee";
            }

            ViewState["Hostel_or_transport"] = "NO";

            hostelTransportFee.Attributes.Add("class", "mdl-frm-row hidden");
            txt_hostel_or_transport.Text = "0";
            if (Transportationtaken.ToUpper() == "YES")
            {
                hsotelTransporT.InnerText = "Transport Fee";
                hostelTransportFee.Attributes.Add("class", "mdl-frm-row showd");
                ViewState["Hostel_or_transport"] = "TRNSPORT";
            }
            else if (hostaltaken.ToUpper() == "YES")
            {
                hsotelTransporT.InnerText = "Hostel Fee";
                hostelTransportFee.Attributes.Add("class", "mdl-frm-row showd");
                ViewState["Hostel_or_transport"] = "HOSTEL";
            }
            else
            {
                hostelTransportFee.Attributes.Add("class", "hidden");
                ViewState["Hostel_or_transport"] = "NO";
            }

            if (ViewState["firm_id"].ToString() == "DIS-01")
            {
                DataTable dt = My.dataTable("select isnull(sum(convert(float, paid)),0) as Total_tuition_fee from Monthly_Fee_Collection_Slip where parameter='" + parameterM + "' and adno='" + ViewState["adm_no"].ToString() + "' and class='" + ViewState["class_id"].ToString() + "' and session='" + ViewState["session_name"].ToString() + "' and content_id!='1002' and content_id not in (select Content_id from Fee_type_for_hostel)");
                if (dt.Rows.Count > 0)
                {
                    txt_tution_fee.Text = dt.Rows[0]["Total_tuition_fee"].ToString();
                }

                if (ViewState["Hostel_or_transport"].ToString() == "NO") { }
                else
                {
                    if (ViewState["Hostel_or_transport"].ToString() == "TRNSPORT")
                    {
                        DataTable dtT = My.dataTable("select isnull(sum(convert(float, paid)),0) as Total_transportation_fee from Monthly_Fee_Collection_Slip where parameter='" + parameterM + "' and adno='" + ViewState["adm_no"].ToString() + "' and class='" + ViewState["class_id"].ToString() + "' and session='" + ViewState["session_name"].ToString() + "' and content_id = '1002'");
                        if (dtT.Rows.Count > 0)
                        {
                            txt_hostel_or_transport.Text = dtT.Rows[0]["Total_transportation_fee"].ToString();
                        }
                    }
                    if (ViewState["Hostel_or_transport"].ToString() == "HOSTEL")
                    {
                        DataTable dtH = My.dataTable("select isnull(sum(convert(float, paid)),0) as Total_hostel_fee from Monthly_Fee_Collection_Slip where parameter='" + parameterM + "' and adno='" + ViewState["adm_no"].ToString() + "' and class='" + ViewState["class_id"].ToString() + "' and session='" + ViewState["session_name"].ToString() + "' and content_id in (select Content_id from Fee_type_for_hostel where Is_For='Hostel')");
                        if (dt.Rows.Count > 0)
                        {
                            txt_hostel_or_transport.Text = dtH.Rows[0]["Total_hostel_fee"].ToString();
                        }
                    }
                }


                DataTable dts = My.dataTable("select  isnull(sum(convert(float, paid)),0) as Total_Amount from Monthly_Fee_Collection_Slip where parameter='" + parameterAD + "' and adno='" + ViewState["adm_no"].ToString() + "' and class='" + ViewState["class_id"].ToString() + "' and session='" + ViewState["session_name"].ToString() + "' and content_id in (select Content_id from Fee_type_for_hostel where (Is_For='Admission' or Is_For='Annual'))");
                if (dts.Rows.Count > 0)
                {
                    txt_term_fee.Text = dts.Rows[0]["Total_Amount"].ToString();
                }

                double ttl_fee = My.toDouble(txt_tution_fee.Text) + My.toDouble(txt_term_fee.Text) + My.toDouble(txt_hostel_or_transport.Text);
                txt_final_amt.Text = ttl_fee.ToString();
            }
            else
            {
                DataTable dt = My.dataTable("select  isnull(sum(convert(float, Amount)),0) as Total_Amount from Student_Payment_History where Type='Monthly' and Addmission_no='" + ViewState["adm_no"].ToString() + "' and Class_id='" + ViewState["class_id"].ToString() + "' and Session='" + ViewState["session_name"].ToString() + "'");
                if (dt.Rows.Count > 0)
                {
                    txt_tution_fee.Text = dt.Rows[0]["Total_Amount"].ToString();
                }

                DataTable dts = My.dataTable("select  isnull(sum(convert(float, Amount)),0) as Total_Amount from Student_Payment_History where (Type='Annual' or Type='Admission')  and Addmission_no='" + ViewState["adm_no"].ToString() + "' and Class_id='" + ViewState["class_id"].ToString() + "' and Session='" + ViewState["session_name"].ToString() + "'");
                if (dts.Rows.Count > 0)
                {
                    txt_term_fee.Text = dts.Rows[0]["Total_Amount"].ToString();
                }

                double ttl_fee = My.toDouble(txt_tution_fee.Text) + My.toDouble(txt_term_fee.Text);
                txt_final_amt.Text = ttl_fee.ToString();
            }
        }

        protected void btn_create_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_dob.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    Alertme("Please enter date of birth.", "warning");
                    txt_dob.Focus();
                }
                else if (txt_address.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    Alertme("Please enter address.", "warning");
                    txt_address.Focus();
                }
                else if (txt_period_from.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    Alertme("Please enter period from.", "warning");
                    txt_period_from.Focus();
                }
                else if (txt_year_end_month.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    Alertme("Please enter period to.", "warning");
                    txt_year_end_month.Focus();
                }
                else if (txt_tution_fee.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    Alertme("Please enter tuition fee.", "warning");
                    txt_tution_fee.Focus();
                }
                else if (txt_term_fee.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    Alertme("Please enter term fee.", "warning");
                    txt_term_fee.Focus();
                }
                else if (txt_final_amt.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    Alertme("Please enter final amount.", "warning");
                    txt_final_amt.Focus();
                }
                else if (txt_issue_date.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    Alertme("Please enter issue date.", "warning");
                    txt_issue_date.Focus();
                }
                else
                {
                    string certificate_no = My.income_format("Income_sl", ViewState["firm_id"].ToString());
                    SqlDataAdapter ad = new SqlDataAdapter("select * from Certificate_Master where Session_id=" + ViewState["sessioN"].ToString() + " and Class_id='" + ViewState["class_id"].ToString() + "' and Admission_no='" + ViewState["adm_no"].ToString() + "' and Certificate_type='Income'", My.conn);
                    DataSet ds = new DataSet();
                    ad.Fill(ds, "Certificate_Master");
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count == 0)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Session_id"] = ViewState["sessioN"].ToString();
                        dr["Class_id"] = ViewState["class_id"].ToString();
                        dr["Admission_no"] = ViewState["adm_no"].ToString();
                        dr["Certificate_type"] = "Income";
                        dr["Certificate_no"] = certificate_no;
                        dr["Date_of_birth"] = txt_dob.Text;
                        dr["Create_date"] = mycode.date();
                        dr["Create_idate"] = mycode.idate();
                        dr["User_id"] = Session["Admin"].ToString();
                        dr["Firm_id"] = Session["firm"].ToString();
                        dr["Address_certificate"] = txt_address.Text;
                        dr["Period_from"] = txt_period_from.Text;
                        dr["End_month"] = txt_year_end_month.Text;
                        dr["Tuition_fee"] = txt_tution_fee.Text;
                        dr["Term_fee"] = txt_term_fee.Text;
                        dr["Final_fee"] = txt_final_amt.Text;
                        if (ViewState["Hostel_or_transport"].ToString() == "TRNSPORT")
                        {
                            dr["Transportation_fee"] = txt_hostel_or_transport.Text;
                            dr["Hostel_fee"] = "0";
                        }
                        else if (ViewState["Hostel_or_transport"].ToString() == "HOSTEL")
                        {
                            dr["Transportation_fee"] = "0";
                            dr["Hostel_fee"] = txt_hostel_or_transport.Text;
                        }
                        else
                        {
                            dr["Transportation_fee"] = "0";
                            dr["Hostel_fee"] = "0";
                        } 
                        string inword_number = mycode.NumberToWords(My.toIntS(txt_final_amt.Text));
                        dr["Final_fee_in_word"] = inword_number;
                        dr["Issue_date"] = txt_issue_date.Text;
                        dt.Rows.Add(dr);
                        SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                        ad.Update(dt);
                        //try
                        //{
                        //    string sub = "Income Certificate";
                        //    string messge = "Dear student your income certificate has been generated please come to the school and collect the certificate.";
                        //    if (ViewState["gcmid"].ToString() != "")
                        //    {
                        //        Dictionary<String, String> ss = new Dictionary<string, string>();
                        //        ss["notification_id"] = Guid.NewGuid().ToString();
                        //        ss["message"] = messge;
                        //        ss["title"] = sub;
                        //        ss["messagetype"] = "Message";
                        //        ss["url"] = "";
                        //        ss["link_url"] = "";
                        //        ss["UserId"] = ViewState["adm_no"].ToString();
                        //        UsesCode.SendNotification(ViewState["gcmid"].ToString(), ss);
                        //    }
                        //}
                        //catch
                        //{
                        //}

                        Session["CCreatioN"] = "1";
                        if (ViewState["firm_id"].ToString() == "GPSKTR")
                        {
                            Response.Redirect("slip/bonafide-certificate1.aspx?adm_no=" + ViewState["adm_no"].ToString() + "&clssid=" + ViewState["class_id"].ToString() + "&sessnid=" + ViewState["sessioN"].ToString() + "&crtificateno=" + certificate_no, false);
                        }
                        else
                        {
                            Response.Redirect("slip/income-certificate.aspx?adm_no=" + ViewState["adm_no"].ToString() + "&clssid=" + ViewState["class_id"].ToString() + "&sessnid=" + ViewState["sessioN"].ToString() + "&type=1", false);
                        }
                        //Alertme("Certificate has been created successfully.", "success");
                        //string qry = "select * from dbo.[admission_registor] where Session_id='" + ddl_session.SelectedValue + "' and Transfer_Status in ('New','NT') and Status='1' and admissionserialnumber not IN  (SELECT Admission_no FROM Certificate_Master WHERE Admission_no=admission_registor.admissionserialnumber and Class_id=admission_registor.Class_id and Session_id=admission_registor.Session_id and Certificate_type='Income') order by id desc";
                        //find_details(qry);
                    }
                    else
                    {
                        Alertme("Certificate already created of this student.", "warning");
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }


        #region WebMethoD
        [WebMethod]
        public static List<string> GetRooPath(string PathRooT, string Session_id)
        {
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(My.conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct studentname from admission_registor where studentname LIKE '%'+@SearchMobNo+'%'   and Status='1' and Session_id='" + Session_id + "'";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchMobNo", PathRooT);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        MobResult.Add(dr["studentname"].ToString());
                    }
                    con.Close();
                    return MobResult;
                }
            }
        }

        [WebMethod]

        public static List<string> GetRooPathAdmNo(string PathRooT, string Session_id)
        {
            string sessionid = Session_id;
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(My.conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct admissionserialnumber from admission_registor where admissionserialnumber LIKE '%'+@SearchMobNo+'%'   and Status='1' and Session_id='" + Session_id + "'";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchMobNo", PathRooT);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        MobResult.Add(dr["admissionserialnumber"].ToString());
                    }
                    con.Close();
                    return MobResult;
                }
            }
        }
        #endregion
    }
}