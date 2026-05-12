using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class create_bonafide_cetrificates : System.Web.UI.Page
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


        protected void btn_find_session_Click(object sender, EventArgs e)
        {
            if (ddl_session.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
            }
            else
            {
                string qry = "select * from dbo.[admission_registor] where Session_id='" + ddl_session.SelectedValue + "' and Status='1' and admissionserialnumber not IN  (SELECT Admission_no FROM Certificate_Master WHERE Admission_no=admission_registor.admissionserialnumber and Class_id=admission_registor.Class_id and Session_id=admission_registor.Session_id and Certificate_type='Income') order by id desc";
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
                    Label lbl_father_name = (Label)row.FindControl("lbl_father_name");
                    lbl_session.Text = lbl_session_name.Text;
                    ViewState["gcmid"] = lbl_gcmid.Text;
                    ViewState["adm_no"] = lbl_adm_no.Text;
                    ViewState["sessioN"] = lbl_session_id.Text;
                    ViewState["class_id"] = lbl_class_id.Text;
                    ViewState["session_name"] = lbl_session_name.Text;
                    lbl_admisn_no.Text = lbl_adm_no.Text;
                    lbl_std_name.Text = lbl_studentname.Text;
                    lbl_classss.Text = lbl_class.Text;
                    txt_dob.Text = lbl_dob.Text;
                    txt_issue_date.Text = mycode.date();
                    lbl_father_names.Text = lbl_father_name.Text;

                    try
                    {
                        string result = ConvertDateToWords(lbl_dob.Text);
                        txt_dob_in_word.Text = result;
                    }
                    catch (Exception ex)
                    {
                    }
                    string address = "";
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

        public static string ConvertDateToWords(string dateStr)
        {
            DateTime date;
            if (!DateTime.TryParseExact(dateStr, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                return "";
            }
            string day = NumberToWords(date.Day);
            string month = date.ToString("MMMM");
            string year = NumberToWords(date.Year);
            return $"{day} of {month}, {year}";
        }

        // Converts numbers to words (up to 9999+)
        public static string NumberToWords(int number)
        {
            if (number == 0)
                return "Zero";
            if (number < 0)
                return "Minus " + NumberToWords(Math.Abs(number));
            string words = "";
            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " Thousand ";
                number %= 1000;
            }
            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " Hundred ";
                number %= 100;
            }
            if (number > 0)
            {
                if (words != "")
                    words += "and ";
                string[] unitsMap = { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten",
                                  "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen",
                                  "Seventeen", "Eighteen", "Nineteen" };
                string[] tensMap = { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty",
                                 "Sixty", "Seventy", "Eighty", "Ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }
            return words.Trim();
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
                if (txt_dob_in_word.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    Alertme("Please enter date of birth (in word).", "warning");
                    txt_dob_in_word.Focus();
                }
                else if (txt_address.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    Alertme("Please enter address.", "warning");
                    txt_address.Focus();
                }
                else if (txt_issue_date.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    Alertme("Please enter issue date.", "warning");
                    txt_issue_date.Focus();
                }
                else
                {
                    string certificate_no = My.income_format("Bonafide_sl", ViewState["firm_id"].ToString());
                    if (ViewState["firm_id"].ToString() == "NAVY-001")
                    {
                        if (certificate_no.Length == 1)
                        {
                            certificate_no = "000" + certificate_no;
                        }
                        if (certificate_no.Length == 2)
                        {
                            certificate_no = "00" + certificate_no;
                        }
                        if (certificate_no.Length == 3)
                        {
                            certificate_no = "0" + certificate_no;
                        }
                        string[] stringSeparatorss = new string[] { "-" };
                        string[] arrs = ViewState["session_name"].ToString().Split(stringSeparatorss, StringSplitOptions.None);
                        string yearF = arrs[0];
                        certificate_no = yearF + "/" + certificate_no;
                    }

                    SqlDataAdapter ad = new SqlDataAdapter("select * from Certificate_Master where Session_id=" + ViewState["sessioN"].ToString() + " and Class_id='" + ViewState["class_id"].ToString() + "' and Admission_no='" + ViewState["adm_no"].ToString() + "' and Certificate_type='Bonafide'", My.conn);
                    DataSet ds = new DataSet();
                    ad.Fill(ds, "Certificate_Master");
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count == 0)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Session_id"] = ViewState["sessioN"].ToString();
                        dr["Class_id"] = ViewState["class_id"].ToString();
                        dr["Admission_no"] = ViewState["adm_no"].ToString();
                        dr["Certificate_type"] = "Bonafide";
                        dr["Certificate_no"] = certificate_no;
                        dr["Date_of_birth"] = txt_dob.Text;
                        dr["Create_date"] = mycode.date();
                        dr["Create_idate"] = mycode.idate();
                        dr["User_id"] = Session["Admin"].ToString();
                        dr["Firm_id"] = Session["firm"].ToString();
                        dr["Address_certificate"] = txt_address.Text;
                        dr["Period_from"] = "NA";
                        dr["End_month"] = "NA";
                        dr["Tuition_fee"] = "0";
                        dr["Term_fee"] = "0";
                        dr["Final_fee"] = "0";
                        dr["Transportation_fee"] = "0";
                        dr["Hostel_fee"] = "0";
                        dr["Final_fee_in_word"] = "";
                        dr["Issue_date"] = txt_issue_date.Text;
                        dr["Dob_in_word"] = txt_dob_in_word.Text;
                        dt.Rows.Add(dr);
                        SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                        ad.Update(dt);
                        Session["CCreatioN"] = "1";
                        Response.Redirect("slip/bonafide/bonafide.aspx?adm_no=" + ViewState["adm_no"].ToString() + "&clssid=" + ViewState["class_id"].ToString() + "&sessnid=" + ViewState["sessioN"].ToString() + "&type=1", false);
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

        protected void txt_dob_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                string result = ConvertDateToWords(txt_dob.Text);
                txt_dob_in_word.Text = result;
            }
            catch (Exception ex)
            {
            }
        }
    }
}