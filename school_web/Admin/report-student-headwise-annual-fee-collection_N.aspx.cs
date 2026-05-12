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
    public partial class report_student_headwise_annual_fee_collection_N : System.Web.UI.Page
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
                        ViewState["Flter"] = "0";
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();
                        ViewState["SessioNs"] = My.get_session();
                        string pagename_current = "fee-report.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];

                        mycode.bind_all_ddl_with_id(ddl_session, " select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int)");
                        ddl_session.SelectedValue = My.get_session_id();

                        mycode.bind_all_ddl_with_id(ddl_class, "Select Course_Name,course_id from Add_course_table order by Position");
                        ddl_class.SelectedValue = My.get_top_one_class();

                        mycode.bind_all_ddl_with_id(ddl_adm_class, "Select Course_Name,course_id from Add_course_table order by Position");
                        ddl_adm_class.SelectedValue = My.get_top_one_class();
                        mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor order by Section asc");
                        ddl_section.Text = My.get_top_one_section();

                        txt_s_date.Text = mycode.sevendaysback();
                        txt_e_date.Text = mycode.date(); 

                        Bind_data_date_wise("OnlyDatE");
                        find_firm_details();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_group_master");
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


        private void Bind_data_date_wise(string type)
        {
            lbl_online.Text = "00";
            lbl_by_cash.Text = "00";
            lbl_by_netbanking.Text = "00";
            lbl_by_deposit.Text = "00";
            lbl_by_sb.Text = "00";
            lbl_by_chequeS.Text = "00";
            lbl_by_neft.Text = "00";
            lbl_by_debitcard.Text = "00";
            lbl_by_credit_card.Text = "00";
            lbl_by_other_card.Text = "00";
            lbl_by_upi.Text = "00";
            lbl_by_branch.Text = "00";
            lbl_pos.Text = "00";

            if (txt_s_date.Text == "")
            {
                Alertme("Please choose from date", "warning");
                txt_s_date.Focus();
            }
            else if (txt_e_date.Text == "")
            {
                Alertme("Please choose to date", "warning");
                txt_e_date.Focus();
            }
            else
            {
                int idate = mycode.ConvertStringToiDate(txt_s_date.Text);
                int idate2 = mycode.ConvertStringToiDate(txt_e_date.Text);
                if (idate > idate2)
                {
                    Alertme("End date cannot be less than start date.", "warning");
                }
                else
                {
                    string sdate = txt_s_date.Text;
                    string sday = sdate.Substring(0, 2);
                    string smonth = sdate.Substring(3, 2);
                    string syear = sdate.Substring(6, 4);

                    string edate = txt_e_date.Text;
                    string eday = edate.Substring(0, 2);
                    string emonth = edate.Substring(3, 2);
                    string eyear = edate.Substring(6, 4);

                    int idate1 = Convert.ToInt32(syear + smonth + sday);
                    int idate21 = Convert.ToInt32(eyear + emonth + eday);

                    lbl_class22.Text = txt_s_date.Text + " To " + txt_e_date.Text;

                    if (idate > idate2)
                    {
                        Alertme("End date cannot be less than start date.", "warning");
                    }
                    else
                    {
                        final_find_report_by_date(idate1, idate21, type);
                    }
                }
            }
        }

        private void final_find_report_by_date(int idate1, int idate21, string type)
        {

            SqlCommand cmd = new SqlCommand("sp_Fee_collection_report_new");
            cmd.Parameters.AddWithValue("@fromdate ", idate1);
            cmd.Parameters.AddWithValue("@todate ", idate21);
            cmd.Parameters.AddWithValue("@session ",ddl_session.SelectedValue);
            cmd.Parameters.AddWithValue("@parameter ", "AnnualFee");
            cmd.Parameters.AddWithValue("@parameter2 ", "HostelAnnualFee");
            if (type == "OnlyDatE")
            {
                cmd.Parameters.AddWithValue("@sp_status ", "38");
            }

            if (type == "DatEClasS")
            {
                cmd.Parameters.AddWithValue("@class_id ", ddl_class.SelectedValue);
                cmd.Parameters.AddWithValue("@sp_status ", "39");
            }

            if (type == "DatEClasSSectioN")
            {
                cmd.Parameters.AddWithValue("@section ", ddl_section.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@class_id ", ddl_class.SelectedValue);
                cmd.Parameters.AddWithValue("@sp_status ", "40");
            }

            if (type == "DatEAdmNo")
            {
                cmd.Parameters.AddWithValue("@admission_no ", txt_admission_no.Text);
                cmd.Parameters.AddWithValue("@class_id ", ddl_adm_class.SelectedValue);
                cmd.Parameters.AddWithValue("@sp_status ", "41");
            }
            DataSet ds = My.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            int rowcount = ds.Tables[0].Rows.Count;
            if (rowcount > 0)
            {
                rp_month.DataSource = dt;
                rp_month.DataBind();

                btn_excels.Visible = true;
         

                if (ViewState["Is_Print"].ToString() == "1")
                {
                    print1.Visible = true;
                }
                else
                {
                    print1.Visible = false;
                }

                NotFoundS.Visible = false;
                tblPrintIQ.Visible = true;
                find_student_amount_details(idate1, idate21, type);
            }
            else
            {
                rp_month.DataSource = null;
                rp_month.DataBind();

                btn_excels.Visible = false;
                print1.Visible = false;

                NotFoundS.Visible = true;
                tblPrintIQ.Visible = false;
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





        private void find_student_amount_details(int idate1, int idate21, string type)
        {
            DataTable fdt = new DataTable();
            string lbl_class_id = ddl_class.SelectedValue;
            string qry = "";

            if (type == "OnlyDatE")
            {
                if (ddl_paymentmode.Text == "All")
                {
                    qry = " select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl,t2.Date,t1.session,t2.Addmission_no as Admission_no,t1.class as Class,t1.Section,t1.rollnumber as Roll_number,t1.studentname as Student_name,t1.Session_id,t1.Class_id,t2.Slip_no,t2.mode as Payment_Mode from admission_registor t1 join Student_Payment_History t2 on t1.admissionserialnumber=t2.Addmission_no and t1.session=t2.Session join Payment_Mode_Master pmm on pmm.Type_Mode=t2.mode where t2.Idate>=" + idate1 + " and t2.Idate<=" + idate21 + " and t2.session='" + ddl_session.SelectedItem.Text + "' and t2.Type='Annual' order by pmm.Position,t2.Idate asc";


                }
                else
                {
                    qry = " select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl,t2.Date,t1.session,t2.Addmission_no as Admission_no,t1.class as Class,t1.Section,t1.rollnumber as Roll_number,t1.studentname as Student_name,t1.Session_id,t1.Class_id,t2.Slip_no,t2.mode as Payment_Mode from admission_registor t1 join Student_Payment_History t2 on t1.admissionserialnumber=t2.Addmission_no and t1.session=t2.Session join Payment_Mode_Master pmm on pmm.Type_Mode=t2.mode where t2.Idate>=" + idate1 + " and t2.Idate<=" + idate21 + " and t2.session='" + ddl_session.SelectedItem.Text + "' and t2.mode='" + ddl_paymentmode.Text + "' and t2.Type='Annual'  order by pmm.Position,t2.Idate asc";
                }



                // qry = "select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl,t2.Date,t1.session,t2.Admission_no,t1.class as Class,t1.Section,t1.rollnumber as Roll_number,t1.studentname as Student_name,t1.Session_id,t1.Class_id from admission_registor t1 join Admission_fee_collection t2 on t1.admissionserialnumber=t2.Admission_no and t1.session=t2.session where t2.Idate>=" + idate1 + " and t2.Idate<=" + idate21 + " and t2.session='" + ViewState["SessioNs"].ToString() + "' order by t2.Idate asc";
            }
            if (type == "DatEClasS")
            {
                if (ddl_paymentmode.Text == "All")
                {
                    qry = " select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl,t2.Date,t1.session,t2.Addmission_no as Admission_no,t1.class as Class,t1.Section,t1.rollnumber as Roll_number,t1.studentname as Student_name,t1.Session_id,t1.Class_id,t2.Slip_no,t2.mode as Payment_Mode from admission_registor t1 join Student_Payment_History t2 on t1.admissionserialnumber=t2.Addmission_no and t1.session=t2.Session join Payment_Mode_Master pmm on pmm.Type_Mode=t2.mode where t2.Idate>=" + idate1 + " and t2.Idate<=" + idate21 + " and t2.session='" + ddl_session.SelectedItem.Text + "' and t1.Class_id='" + ddl_class.SelectedValue + "' and t2.Type='Annual'  order by pmm.Position,t2.Idate asc";
                }
                else
                {
                    qry = " select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl,t2.Date,t1.session,t2.Addmission_no as Admission_no,t1.class as Class,t1.Section,t1.rollnumber as Roll_number,t1.studentname as Student_name,t1.Session_id,t1.Class_id,t2.Slip_no,t2.mode as Payment_Mode from admission_registor t1 join Student_Payment_History t2 on t1.admissionserialnumber=t2.Addmission_no and t1.session=t2.Session join Payment_Mode_Master pmm on pmm.Type_Mode=t2.mode where t2.Idate>=" + idate1 + " and t2.Idate<=" + idate21 + " and t2.session='" + ddl_session.SelectedItem.Text + "' and t1.Class_id='" + ddl_class.SelectedValue + "' and t2.Type='Annual' and t2.mode='" + ddl_paymentmode.Text + "' order by pmm.Position,t2.Idate asc";

                }
            }

            if (type == "DatEClasSSectioN")
            {
                if (ddl_paymentmode.Text == "All")
                {
                    qry = " select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl,t2.Date,t1.session,t2.Addmission_no as Admission_no,t1.class as Class,t1.Section,t1.rollnumber as Roll_number,t1.studentname as Student_name,t1.Session_id,t1.Class_id,t2.Slip_no,t2.mode as Payment_Mode from admission_registor t1 join Student_Payment_History t2 on t1.admissionserialnumber=t2.Addmission_no and t1.session=t2.Session join Payment_Mode_Master pmm on pmm.Type_Mode=t2.mode where t2.Idate>=" + idate1 + " and t2.Idate<=" + idate21 + " and t2.session='" + ddl_session.SelectedItem.Text + "' and t1.Class_id='" + ddl_class.SelectedValue + "' and t2.Type='Annual' and t1.Section='" + ddl_section.SelectedItem.Text + "' order by pmm.Position,t2.Idate asc";
                }
                else
                {
                    qry = " select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl,t2.Date,t1.session,t2.Addmission_no as Admission_no,t1.class as Class,t1.Section,t1.rollnumber as Roll_number,t1.studentname as Student_name,t1.Session_id,t1.Class_id,t2.Slip_no,t2.mode as Payment_Mode from admission_registor t1 join Student_Payment_History t2 on t1.admissionserialnumber=t2.Addmission_no and t1.session=t2.Session join Payment_Mode_Master pmm on pmm.Type_Mode=t2.mode where t2.Idate>=" + idate1 + " and t2.Idate<=" + idate21 + " and t2.session='" + ddl_session.SelectedItem.Text + "' and t1.Class_id='" + ddl_class.SelectedValue + "' and t1.Section='" + ddl_section.SelectedItem.Text + "' and t2.mode='" + ddl_paymentmode.Text + "' and t2.Type='Annual' order by pmm.Position,t2.Idate asc";
                }
            }

            if (type == "DatEAdmNo")
            {
                if (ddl_paymentmode.Text == "All")
                {
                    qry = " select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl,t2.Date,t1.session,t2.Addmission_no as Admission_no,t1.class as Class,t1.Section,t1.rollnumber as Roll_number,t1.studentname as Student_name,t1.Session_id,t1.Class_id,t2.Slip_no,t2.mode as Payment_Mode from admission_registor t1 join Student_Payment_History t2 on t1.admissionserialnumber=t2.Addmission_no and t1.session=t2.Session join Payment_Mode_Master pmm on pmm.Type_Mode=t2.mode where t2.Idate>=" + idate1 + " and t2.Idate<=" + idate21 + " and t2.session='" + ddl_session.SelectedItem.Text + "' and t2.Addmission_no='" + txt_admission_no.Text + "' and t2.Type='Annual' order by pmm.Position,t2.Idate asc";
                }
                else
                {
                    qry = " select ROW_NUMBER() OVER (ORDER BY t1.Id) AS Sl,t2.Date,t1.session,t2.Addmission_no as Admission_no,t1.class as Class,t1.Section,t1.rollnumber as Roll_number,t1.studentname as Student_name,t1.Session_id,t1.Class_id,t2.Slip_no,t2.mode as Payment_Mode from admission_registor t1 join Student_Payment_History t2 on t1.admissionserialnumber=t2.Addmission_no and t1.session=t2.Session join Payment_Mode_Master pmm on pmm.Type_Mode=t2.mode where t2.Idate>=" + idate1 + " and t2.Idate<=" + idate21 + " and t2.session='" + ddl_session.SelectedItem.Text + "' and t2.Addmission_no='" + txt_admission_no.Text + "' and t2.Type='Annual' and t2.mode='" + ddl_paymentmode.Text + "' order by pmm.Position,t2.Idate asc";
                }
            }

            SqlDataAdapter ad_contactus = new SqlDataAdapter(qry, My.conn);
            DataSet ds = new DataSet();
            ad_contactus.Fill(ds);
            DataTable dt = ds.Tables[0];
            int srowcount = dt.Rows.Count;
            int mgrowcount1 = rp_month.Items.Count;
            if (srowcount > 0)
            {
                for (int ixi = 0; ixi < mgrowcount1; ixi++)
                {
                    Label lbl_content_id = (Label)rp_month.Items[ixi].FindControl("lbl_content_id");
                    Label lbl_feetype = (Label)rp_month.Items[ixi].FindControl("lbl_feetype");
                    dt.Columns.Add(lbl_feetype.Text, Type.GetType("System.Double"));
                    fdt.Columns.Add(lbl_feetype.Text, Type.GetType("System.Double"));
                }
                dt.Columns.Add("Total", Type.GetType("System.Double"));

                foreach (DataRow dr in dt.Rows)
                {
                    int mgrowcount = rp_month.Items.Count;
                    for (int ixi = 0; ixi < mgrowcount; ixi++)
                    {
                        Label lbl_content_id = (Label)rp_month.Items[ixi].FindControl("lbl_content_id");
                        Label lbl_feetype = (Label)rp_month.Items[ixi].FindControl("lbl_feetype");


                        dr[lbl_feetype.Text] = find_fees(lbl_feetype.Text, lbl_content_id.Text, dr);
                    }
                }

                //================ 
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        double total2 = 0;
                        for (int i = 12; i < dt.Columns.Count - 1; i++)
                        {
                            total2 += My.toDouble(dr[i]);
                        }
                        dr["Total"] = total2;
                    }

                    GridView2.DataSource = dt.DefaultView;
                    GridView2.DataBind();


                    lbl_online.Text = "00";
                    lbl_by_cash.Text = "00";
                    lbl_by_netbanking.Text = "00";
                    lbl_by_deposit.Text = "00";
                    lbl_by_sb.Text = "00";
                    lbl_by_chequeS.Text = "00";
                    lbl_by_neft.Text = "00";
                    lbl_by_debitcard.Text = "00";
                    lbl_by_credit_card.Text = "00";
                    lbl_by_other_card.Text = "00";
                    lbl_by_upi.Text = "00";
                    lbl_by_branch.Text = "00";
                    lbl_pos.Text = "00";

                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (dt.Rows[j]["Payment_Mode"].ToString() == "Cash")
                        {
                            double Total1 = My.toDouble(lbl_by_cash.Text) + My.toDouble(dt.Rows[j]["Total"].ToString());
                            lbl_by_cash.Text = Total1.ToString("0.00");
                        }
                        if (dt.Rows[j]["Payment_Mode"].ToString() == "Netbanking")
                        {
                            double Total1 = My.toDouble(lbl_by_netbanking.Text) + My.toDouble(dt.Rows[j]["Total"].ToString());
                            lbl_by_netbanking.Text = Total1.ToString("0.00");


                        }
                        if (dt.Rows[j]["Payment_Mode"].ToString() == "Deposited In Bank")
                        {
                            double Total1 = My.toDouble(lbl_by_deposit.Text) + My.toDouble(dt.Rows[j]["Total"].ToString());
                            lbl_by_deposit.Text = Total1.ToString("0.00");


                        }
                        if (dt.Rows[j]["Payment_Mode"].ToString() == "Sbdebit")
                        {
                            double Total1 = My.toDouble(lbl_by_sb.Text) + My.toDouble(dt.Rows[j]["Total"].ToString());
                            lbl_by_sb.Text = Total1.ToString("0.00");


                        }
                        if (dt.Rows[j]["Payment_Mode"].ToString() == "Cheque")
                        {
                            double Total1 = My.toDouble(lbl_by_chequeS.Text) + My.toDouble(dt.Rows[j]["Total"].ToString());
                            lbl_by_chequeS.Text = Total1.ToString("0.00");


                        }
                        if (dt.Rows[j]["Payment_Mode"].ToString() == "NEFT")
                        {
                            double Total1 = My.toDouble(lbl_by_neft.Text) + My.toDouble(dt.Rows[j]["Total"].ToString());
                            lbl_by_neft.Text = Total1.ToString("0.00");


                        }
                        if (dt.Rows[j]["Payment_Mode"].ToString() == "Debitcard")
                        {
                            double Total1 = My.toDouble(lbl_by_debitcard.Text) + My.toDouble(dt.Rows[j]["Total"].ToString());
                            lbl_by_debitcard.Text = Total1.ToString("0.00");


                        }
                        if (dt.Rows[j]["Payment_Mode"].ToString() == "Creditcard")
                        {
                            double Total1 = My.toDouble(lbl_by_credit_card.Text) + My.toDouble(dt.Rows[j]["Total"].ToString());
                            lbl_by_credit_card.Text = Total1.ToString("0.00");



                        }
                        if (dt.Rows[j]["Payment_Mode"].ToString() == "Otherdcard")
                        {
                            double Total1 = My.toDouble(lbl_by_other_card.Text) + My.toDouble(dt.Rows[j]["Total"].ToString());
                            lbl_by_other_card.Text = Total1.ToString("0.00");


                        }
                        if (dt.Rows[j]["Payment_Mode"].ToString() == "UPI")
                        {
                            double Total1 = My.toDouble(lbl_by_upi.Text) + My.toDouble(dt.Rows[j]["Total"].ToString());
                            lbl_by_upi.Text = Total1.ToString("0.00");


                        }
                        if (dt.Rows[j]["Payment_Mode"].ToString() == "Branch")
                        {
                            double Total1 = My.toDouble(lbl_by_branch.Text) + My.toDouble(dt.Rows[j]["Total"].ToString());
                            lbl_by_branch.Text = Total1.ToString("0.00");


                        }
                        if (dt.Rows[j]["Payment_Mode"].ToString() == "Pos")
                        {
                            double Total1 = My.toDouble(lbl_pos.Text) + My.toDouble(dt.Rows[j]["Total"].ToString());
                            lbl_pos.Text = Total1.ToString("0.00");


                        }
                        if (dt.Rows[j]["Payment_Mode"].ToString() == "Online")
                        {
                            double Total1 = My.toDouble(lbl_online.Text) + My.toDouble(dt.Rows[j]["Total"].ToString());
                            lbl_online.Text = Total1.ToString("0.00");


                        }
                    }





                    //BoundField field = new BoundField();
                    //field.HeaderText = "New Column";
                    //DataControlField col;
                    //col = field;
                    //GridView2.Columns.Add(col);
                    //GridView2.DataBind();

                    double total = 0; ;
                    GridView2.FooterRow.Cells[11].Text = "Total";
                    GridView2.FooterRow.Cells[11].Font.Bold = true;
                    GridView2.FooterRow.Cells[11].HorizontalAlign = HorizontalAlign.Left;

                    for (int k = 12; k < dt.Columns.Count; k++)
                    {
                        total = dt.AsEnumerable().Sum(row => row.Field<Double>(dt.Columns[k].ToString()));
                        GridView2.FooterRow.Cells[k].Text = total.ToString();
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

        private string find_fees(string FeeType, string content_id, DataRow dr)
        {
            DataTable feedt = new DataTable();
            string session = My.get_session();
            double headFee = 0;
            feedt = My.dataTable("select sum(isnull(convert(float, paid),0)) as Paid_amt from Monthly_Fee_Collection_Slip where (parameter='AnnualFee' or parameter='HostelAnnualFee' ) and content_id='" + content_id + "' and adno='" + dr["Admission_no"].ToString() + "' and session='" + dr["session"].ToString() + "' and class='" + dr["Class_id"].ToString() + "' and slipno='" + dr["Slip_no"].ToString() + "'");
            if (feedt.Rows.Count.ToString() != "0")
            {
                headFee = My.toDouble(feedt.Rows[0]["Paid_amt"].ToString());
            }
            else
            {
                headFee = 0;
            }

            return headFee.ToString();
        }


        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //Label lbl_footr_totalCollection = (Label)e.Row.FindControl("lbl_footr_totalCollection");
                //Label lbl_Depositr_totalCollection = (Label)e.Row.FindControl("lbl_Depositr_totalCollection");

                //lbl_footr_totalCollection.Text = total_collection.ToString();
                //lbl_Depositr_totalCollection.Text = total_deposit.ToString();
            }
        }

        protected void GridView2_RowCreated(object sender, GridViewRowEventArgs e)
        {
            //e.Row.Cells[7].Visible = false;
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[9].Visible = false;
            //e.Row.Cells[10].Visible = false;
            //e.Row.Cells[11].Visible = false;
            //e.Row.Cells[12].Visible = false;

            //e.Row.Cells[13].Visible = false;
            //e.Row.Cells[14].Visible = false;
            //e.Row.Cells[15].Visible = false;
            //e.Row.Cells[16].Visible = false;
            //e.Row.Cells[17].Visible = false;
        }



        protected void btn_find_Click(object sender, EventArgs e)
        {
            Bind_data_date_wise("OnlyDatE");
        }

        protected void lnks_filter_Click(object sender, EventArgs e)
        {
            filtersss();
        }

        private void filtersss()
        {
            if (ViewState["Flter"].ToString() == "0")
            {
                fnds1.Visible = false;
                fnds2.Visible = false;
                fnds3.Visible = true;
                fnds4.Visible = true;
                ViewState["Flter"] = "1";
            }
            else
            {
                fnds1.Visible = true;
                fnds2.Visible = true;
                fnds3.Visible = false;
                fnds4.Visible = false;
                ViewState["Flter"] = "0";
            }
        }

        protected void btn_find_by_class_Click(object sender, EventArgs e)
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
                    Alertme("Please choose to date.", "warning");
                    txt_e_date.Focus();
                }
                else if (ddl_class.SelectedItem.Text == "Select")
                {
                    Alertme("Please choose class.", "warning");
                    ddl_class.Focus();
                }
                else
                {
                    find_data_by_class();
                }
            }

            catch (Exception ex)
            {
            }
        }

        private void find_data_by_class()
        {
            if (ddl_section.SelectedItem.Text == "ALL")
            {
                Bind_data_date_wise("DatEClasS");
            }
            else
            {
                Bind_data_date_wise("DatEClasSSectioN");
            }
        }


        protected void btn_find_by_adm_no_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_adm_class.SelectedItem.Text == "Select")
                {
                    Alertme("Please select class.", "warning");
                    ddl_adm_class.Focus();
                }
                else if (txt_admission_no.Text == "")
                {
                    Alertme("Please enter admission no.", "warning");
                    txt_admission_no.Focus();
                }
                else
                {
                    Bind_data_date_wise("DatEAdmNo");
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void btn_find_by_student_name_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_session.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session.", "warning");
                    txt_student_name.Focus();
                }
                else if (txt_student_name.Text == "")
                {
                    Alertme("Please enter student name.", "warning");
                    txt_student_name.Focus();
                }
                else
                {
                    find_by_std_name();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_by_std_name()
        { 
            string query = "select DISTINCT t1.studentname,t1.admissionserialnumber,t1.class,t1.Section,t1.rollnumber,t1.fathername,t1.session,t1.Class_id from admission_registor t1 join Student_Payment_History t2 on t1.admissionserialnumber = t2.Addmission_no and t1.session = t2.Session where t1.studentname like '%" + txt_student_name.Text + "%' and t1.Session_id='" + ddl_session.SelectedValue + "' and  t1.Status='1' order by t1.studentname asc";
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count == 0)
            {
                rp_std.DataSource = null;
                rp_std.DataBind();
                Alertme("Data Not Found...", "warning");
                myModal2.Visible = false;
            }
            else
            {
                rp_std.DataSource = dt;
                rp_std.DataBind();
                myModal2.Visible = true;
            }
        }


        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            myModal2.Visible = false;
        }

        protected void lnk_select_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbladmissionserialnumber = (Label)row.FindControl("lbladmissionserialnumber");
                Label lbl_session = (Label)row.FindControl("lbl_session");
                Label lbl_class_id = (Label)row.FindControl("lbl_class_id");
                ddl_adm_class.SelectedValue = lbl_class_id.Text;
                txt_admission_no.Text = lbladmissionserialnumber.Text;
                Bind_data_date_wise("DatEAdmNo");
                myModal2.Visible = false;
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
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
                        pnl_grid.RenderControl(hw);
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

        protected void GridView2_RowDataBound1(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    for (int i = 12; i < e.Row.Cells.Count; i++)
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



        #region WebMethoD
        [WebMethod]
        public static List<string> GetRooPath(string PathRooT, string Session_id)
        {
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(My.conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct studentname from admission_registor where studentname LIKE '%'+@SearchMobNo+'%' and Status='1' and Session_id='" + Session_id + "' and Transfer_Status='NT'";
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
                    cmd.CommandText = "select distinct admissionserialnumber from admission_registor where admissionserialnumber LIKE ''+@SearchMobNo+'%'   and Status='1' and Session_id='" + Session_id + "' and Transfer_Status='NT'";
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