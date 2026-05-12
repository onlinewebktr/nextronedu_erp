using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class print_bonafide_certificate : System.Web.UI.Page
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

                        string pagename_current = "certificate-master.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);

                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];

                        mycode.bind_all_ddl_with_id(ddlsession, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int)");
                        mycode.bind_all_ddl_with_id_cap_All(ddlclass, "Select Course_Name,course_id from Add_course_table order by Position");
                        ddlsession.SelectedValue = My.get_session_id();
                        ddlclass.SelectedValue = My.get_top_one_class();

                        bind_created_certificate();
                        find_firm_details();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Character_certificate");
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

        private void bind_created_certificate()
        {
            string query = "";
            if (ddlclass.SelectedItem.Text == "ALL")
            {
                query = "select *,(select top 1 Certificate_no from Certificate_Master where Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id and Admission_no=admission_registor.admissionserialnumber and Certificate_type='Bonafide') as Certificate_no from admission_registor where admissionserialnumber IN  (SELECT Admission_no FROM Certificate_Master WHERE Admission_no=admission_registor.admissionserialnumber and Class_id=admission_registor.Class_id and Session_id=admission_registor.Session_id and Certificate_type='Bonafide') and Session_id=" + ddlsession.SelectedValue + "  order by rollnumber asc";
            }
            else

            {
                query = "select *,(select top 1 Certificate_no from Certificate_Master where Session_id=admission_registor.Session_id and Class_id=admission_registor.Class_id and Admission_no=admission_registor.admissionserialnumber and Certificate_type='Bonafide') as Certificate_no from admission_registor where admissionserialnumber IN  (SELECT Admission_no FROM Certificate_Master WHERE Admission_no=admission_registor.admissionserialnumber and Class_id=admission_registor.Class_id and Session_id=admission_registor.Session_id  and Certificate_type='Bonafide') and Session_id=" + ddlsession.SelectedValue + " and Class_id='" + ddlclass.SelectedValue + "' order by rollnumber asc";
            }
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count == 0)
            {
                print1.Visible = false;
                lbl_class22.Text = "";
                btn_excels.Visible = false;
                rd_view.DataSource = null;
                rd_view.DataBind();
                Alertme("Data Not Found...", "warning");
            }
            else
            {
                lbl_class22.Text = " Session :" + ddlsession.SelectedItem.Text + " Class :" + ddlclass.SelectedItem.Text;
                btn_excels.Visible = true;
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
            }
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            if (ddlsession.SelectedItem.Text == "")
            {
                Alertme("Data Not Found...", "warning");
            }
            else
            {
                bind_created_certificate();

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
                        Panel1.RenderControl(hw);
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

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            if (ViewState["Is_delete"].ToString() == "1")
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_adm_no = (Label)row.FindControl("lbl_adm_no");
                Label lbl_session_id = (Label)row.FindControl("lbl_session_id");
                Label lbl_class_id = (Label)row.FindControl("lbl_class_id");
                Label lbl_certificate_no = (Label)row.FindControl("lbl_certificate_no");
                mycode.executequery("delete from Certificate_Master where Session_id='" + lbl_session_id.Text + "' and Class_id='" + lbl_class_id.Text + "' and Admission_no='" + lbl_adm_no.Text + "' and Certificate_no='" + lbl_certificate_no.Text + "' and Certificate_type='Bonafide'");
                string desc = "Bonafide certificate has been delete for certificate no. " + lbl_certificate_no.Text + ".";
                log_hostory.delete_log(lbl_session_id.Text, lbl_class_id.Text, lbl_adm_no.Text, "Bonafide Certificate", desc, "print-bonafide-certificate.aspx", ViewState["Userid"].ToString());
                Alertme("Record has been deleted successfully.", "success");
                bind_created_certificate();
            }
            else
            {
                Alertme("SORRY! You have not permission for this work.", "warning");
            }
        }

        protected void lnk_edit_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            Label lbl_adm_no = (Label)row.FindControl("lbl_adm_no");
            Label lbl_session_id = (Label)row.FindControl("lbl_session_id");
            Label lbl_class_id = (Label)row.FindControl("lbl_class_id");
            Label lbl_certificate_no = (Label)row.FindControl("lbl_certificate_no");

            ViewState["sessionids"] = lbl_session_id.Text;
            ViewState["class_id"] = lbl_class_id.Text;
            ViewState["admnos"] = lbl_adm_no.Text;
            ViewState["cetifitNo"] = lbl_certificate_no.Text;
            fetch_certificate_data(lbl_adm_no.Text, lbl_session_id.Text, lbl_class_id.Text, lbl_certificate_no.Text);
        }

        private void fetch_certificate_data(string adm_no, string session_id, string class_id, string certificate_no)
        {
            DataTable dt = mycode.FillData("select t2.Section,t2.studentname,t2.fathername,t2.class,t2.session,t2.studentimagepath,t1.* from dbo.[Certificate_Master] t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id and t1.Admission_no=t2.admissionserialnumber and t1.Certificate_type='Bonafide' and t1.Session_id='" + session_id + "' and t1.Class_id='" + class_id + "' and t1.Admission_no='" + adm_no + "' and t1.Certificate_no='" + certificate_no + "'");
            if (dt.Rows.Count > 0)
            {
                lbl_session.Text = dt.Rows[0]["session"].ToString();
                lbl_admisn_no.Text = dt.Rows[0]["Issue_date"].ToString();
                lbl_std_name.Text = dt.Rows[0]["studentname"].ToString();
                lbl_father_names.Text = dt.Rows[0]["fathername"].ToString();
                lbl_classss.Text = dt.Rows[0]["class"].ToString();
                txt_dob.Text = dt.Rows[0]["Date_of_birth"].ToString();
                txt_dob_in_word.Text = dt.Rows[0]["Dob_in_word"].ToString();
                txt_address.Text = dt.Rows[0]["Address_certificate"].ToString();
                txt_issue_date.Text = dt.Rows[0]["Issue_date"].ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
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
                    SqlCommand cmd;
                    string query = "Update Certificate_Master set Date_of_birth=@Date_of_birth,Dob_in_word=@Dob_in_word,Address_certificate=@Address_certificate,Issue_date=@Issue_date where Session_id = '" + ViewState["sessionids"].ToString() + "' and Class_id='" + ViewState["class_id"].ToString() + "' and Admission_no='" + ViewState["admnos"].ToString() + "' and Certificate_no='" + ViewState["cetifitNo"].ToString() + "'";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Date_of_birth", txt_dob.Text);
                    cmd.Parameters.AddWithValue("@Dob_in_word", txt_dob_in_word.Text);
                    cmd.Parameters.AddWithValue("@Address_certificate", txt_address.Text);
                    cmd.Parameters.AddWithValue("@Issue_date", txt_issue_date.Text);
                    if (My.InsertUpdateData(cmd))
                    {
                        string desc = "Bonafide certificate has been updated for certificate no. " + ViewState["cetifitNo"].ToString() + ".";
                        log_hostory.delete_log(ViewState["sessionids"].ToString(), ViewState["class_id"].ToString(), ViewState["admnos"].ToString(), "Bonafide Certificate", desc, "print-bonafide-certificate.aspx", ViewState["Userid"].ToString());
                        Response.Redirect("slip/bonafide/bonafide.aspx?adm_no=" + ViewState["admnos"].ToString() + "&clssid=" + ViewState["class_id"].ToString() + "&sessnid=" + ViewState["sessionids"].ToString() + "&type=1", false);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}