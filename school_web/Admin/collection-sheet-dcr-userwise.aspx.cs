using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class collection_sheet_dcr_userwise : System.Web.UI.Page
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
                        mycode.bind_all_ddl_with_id_cap_All(ddl_session, "select Session,session_id from session_details order by Session asc");
                        My.bind_ddl_all_Cap(ddl_payment_mode, "select distinct mode from Student_Payment_History order by mode asc");
                        mycode.bind_all_ddl_with_id_cap_All(ddl_user, "select * from (select distinct (select top 1 name from user_details where user_id=Student_Payment_History.user_id) as User_name,user_id from Student_Payment_History where user_id in (select user_id from user_details)) t order by User_name asc");
                        ddl_session.SelectedValue = My.get_session_id();
                        txt_s_date.Text = mycode.date();
                        txt_e_date.Text = mycode.date();

                        string pagename_current = Path.GetFileName("collection-sheet-dcr.aspx");
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
                        find_students_dues();
                    }
                }
            }
            catch (Exception ex)
            {
                //  My.saveExceptionDetails(ex, "StudentList");
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


        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_s_date.Text == "")
                {
                    Alertme("Please choose from date.", "warning");
                    txt_s_date.Focus();
                }
                else if (txt_e_date.Text == "")
                {
                    Alertme("Please choose end date.", "warning");
                    txt_e_date.Focus();
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
            int idate1 = My.DateConvertToIdate(txt_s_date.Text);
            int idate2 = My.DateConvertToIdate(txt_e_date.Text);
            string qry = "";

            if (ddl_session.SelectedItem.Text == "ALL")
            {
                if (ddl_payment_mode.SelectedItem.Text == "ALL" && ddl_user.SelectedItem.Text == "ALL")
                {
                    lbl_class22.Text = "User-Wise Collection Sheet - Date : " + txt_s_date.Text + " To " + txt_e_date.Text;
                    qry = "select  ad.Id,(select top 1 name from user_details where user_id=ad.user_id) as CollectionBy,ar.admissionserialnumber,ar.session,ar.class,ar.Section,ar.rollnumber,ar.studentname,ar.fathername,ar.mobilenumber,convert(float, ad.Amount) as Amount,ad.mode,ad.Date,ad.Idate,ad.Slip_no,ad.parameter_New,ad.user_id from admission_registor ar  join Student_Payment_History ad on ar.admissionserialnumber=ad.Addmission_no and ar.session=ad.Session and ar.Class_id=ad.Class_id where ad.Idate>='" + idate1 + "' and ad.Idate<='" + idate2 + "' order by ad.Id asc";
                }
                else if (ddl_payment_mode.SelectedItem.Text == "ALL" && ddl_user.SelectedItem.Text != "ALL")
                {
                    lbl_class22.Text = "User-Wise Collection Sheet - Date : " + txt_s_date.Text + " To " + txt_e_date.Text;
                    qry = "select  ad.Id,(select top 1 name from user_details where user_id=ad.user_id) as CollectionBy,ar.admissionserialnumber,ar.session,ar.class,ar.Section,ar.rollnumber,ar.studentname,ar.fathername,ar.mobilenumber,convert(float, ad.Amount) as Amount,ad.mode,ad.Date,ad.Idate,ad.Slip_no,ad.parameter_New,ad.user_id from admission_registor ar  join Student_Payment_History ad on ar.admissionserialnumber=ad.Addmission_no and ar.session=ad.Session and ar.Class_id=ad.Class_id join Payment_Mode_Master pmm on pmm.Type_Mode=ad.mode where ad.Idate>='" + idate1 + "' and ad.Idate<='" + idate2 + "' and ad.user_id='" + ddl_user.SelectedValue + "' order by ad.Id asc";
                }
                else if (ddl_payment_mode.SelectedItem.Text != "ALL" && ddl_user.SelectedItem.Text == "ALL")
                {
                    lbl_class22.Text = "User-Wise Collection Sheet - Date : " + txt_s_date.Text + " To " + txt_e_date.Text;
                    qry = "select  ad.Id,(select top 1 name from user_details where user_id=ad.user_id) as CollectionBy,ar.admissionserialnumber,ar.session,ar.class,ar.Section,ar.rollnumber,ar.studentname,ar.fathername,ar.mobilenumber,convert(float, ad.Amount) as Amount,ad.mode,ad.Date,ad.Idate,ad.Slip_no,ad.parameter_New,ad.user_id from admission_registor ar  join Student_Payment_History ad on ar.admissionserialnumber=ad.Addmission_no and ar.session=ad.Session and ar.Class_id=ad.Class_id join Payment_Mode_Master pmm on pmm.Type_Mode=ad.mode where ad.Idate>='" + idate1 + "' and ad.Idate<='" + idate2 + "' and ad.mode='" + ddl_payment_mode.SelectedValue + "' order by ad.Id asc";
                }
                else
                {
                    lbl_class22.Text = "User-Wise Collection Sheet - Date : " + txt_s_date.Text + " To " + txt_e_date.Text;
                    qry = "select  ad.Id,(select top 1 name from user_details where user_id=ad.user_id) as CollectionBy,ar.admissionserialnumber,ar.session,ar.class,ar.Section,ar.rollnumber,ar.studentname,ar.fathername,ar.mobilenumber,convert(float, ad.Amount) as Amount,ad.mode,ad.Date,ad.Idate,ad.Slip_no,ad.parameter_New,ad.user_id from admission_registor ar  join Student_Payment_History ad on ar.admissionserialnumber=ad.Addmission_no and ar.session=ad.Session and ar.Class_id=ad.Class_id join Payment_Mode_Master pmm on pmm.Type_Mode=ad.mode where ad.Idate>='" + idate1 + "' and ad.Idate<='" + idate2 + "' and ad.user_id='" + ddl_user.SelectedValue + "' and ad.mode='" + ddl_payment_mode.SelectedValue + "' order by ad.Id asc";
                }
            }
            else
            {
                if (ddl_payment_mode.SelectedItem.Text == "ALL" && ddl_user.SelectedItem.Text == "ALL")
                {
                    lbl_class22.Text = "User-Wise Collection Sheet - Date : " + txt_s_date.Text + " To " + txt_e_date.Text;
                    qry = "select  ad.Id,(select top 1 name from user_details where user_id=ad.user_id) as CollectionBy,ar.admissionserialnumber,ar.session,ar.class,ar.Section,ar.rollnumber,ar.studentname,ar.fathername,ar.mobilenumber,convert(float, ad.Amount) as Amount,ad.mode,ad.Date,ad.Idate,ad.Slip_no,ad.parameter_New,ad.user_id from admission_registor ar  join Student_Payment_History ad on ar.admissionserialnumber=ad.Addmission_no and ar.session=ad.Session and ar.Class_id=ad.Class_id where ad.Session='" + ddl_session.SelectedItem.Text + "' and ad.Idate>='" + idate1 + "' and ad.Idate<='" + idate2 + "' order by ad.Id asc";
                }
                else if (ddl_payment_mode.SelectedItem.Text == "ALL" && ddl_user.SelectedItem.Text != "ALL")
                {
                    lbl_class22.Text = "User-Wise Collection Sheet - Date : " + txt_s_date.Text + " To " + txt_e_date.Text;
                    qry = "select  ad.Id,(select top 1 name from user_details where user_id=ad.user_id) as CollectionBy,ar.admissionserialnumber,ar.session,ar.class,ar.Section,ar.rollnumber,ar.studentname,ar.fathername,ar.mobilenumber,convert(float, ad.Amount) as Amount,ad.mode,ad.Date,ad.Idate,ad.Slip_no,ad.parameter_New,ad.user_id from admission_registor ar  join Student_Payment_History ad on ar.admissionserialnumber=ad.Addmission_no and ar.session=ad.Session and ar.Class_id=ad.Class_id join Payment_Mode_Master pmm on pmm.Type_Mode=ad.mode where ad.Session='" + ddl_session.SelectedItem.Text + "' and ad.Idate>='" + idate1 + "' and ad.Idate<='" + idate2 + "' and ad.user_id='" + ddl_user.SelectedValue + "' order by ad.Id asc";
                }
                else if (ddl_payment_mode.SelectedItem.Text != "ALL" && ddl_user.SelectedItem.Text == "ALL")
                {
                    lbl_class22.Text = "User-Wise Collection Sheet - Date : " + txt_s_date.Text + " To " + txt_e_date.Text;
                    qry = "select  ad.Id,(select top 1 name from user_details where user_id=ad.user_id) as CollectionBy,ar.admissionserialnumber,ar.session,ar.class,ar.Section,ar.rollnumber,ar.studentname,ar.fathername,ar.mobilenumber,convert(float, ad.Amount) as Amount,ad.mode,ad.Date,ad.Idate,ad.Slip_no,ad.parameter_New,ad.user_id from admission_registor ar  join Student_Payment_History ad on ar.admissionserialnumber=ad.Addmission_no and ar.session=ad.Session and ar.Class_id=ad.Class_id join Payment_Mode_Master pmm on pmm.Type_Mode=ad.mode where ad.Session='" + ddl_session.SelectedItem.Text + "' and ad.Idate>='" + idate1 + "' and ad.Idate<='" + idate2 + "' and ad.mode='" + ddl_payment_mode.SelectedValue + "' order by ad.Id asc";
                }
                else
                {
                    lbl_class22.Text = "User-Wise Collection Sheet - Date : " + txt_s_date.Text + " To " + txt_e_date.Text;
                    qry = "select  ad.Id,(select top 1 name from user_details where user_id=ad.user_id) as CollectionBy,ar.admissionserialnumber,ar.session,ar.class,ar.Section,ar.rollnumber,ar.studentname,ar.fathername,ar.mobilenumber,convert(float, ad.Amount) as Amount,ad.mode,ad.Date,ad.Idate,ad.Slip_no,ad.parameter_New,ad.user_id from admission_registor ar  join Student_Payment_History ad on ar.admissionserialnumber=ad.Addmission_no and ar.session=ad.Session and ar.Class_id=ad.Class_id join Payment_Mode_Master pmm on pmm.Type_Mode=ad.mode where ad.Session='" + ddl_session.SelectedItem.Text + "' and ad.Idate>='" + idate1 + "' and ad.Idate<='" + idate2 + "' and ad.user_id='" + ddl_user.SelectedValue + "' and ad.mode='" + ddl_payment_mode.SelectedValue + "' order by ad.Id asc";
                }
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
                String Total_mrp = Convert.ToDouble(dt.Compute("SUM(Amount)", string.Empty)).ToString();
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
                    Response.AddHeader("content-disposition", "attachment;filename=Collection_list" + mycode.date() + "_" + mycode.time() + ".xls");
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
    }
}