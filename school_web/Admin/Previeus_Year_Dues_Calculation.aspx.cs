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
    public partial class Previeus_Year_Dues_Calculation : System.Web.UI.Page
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
                        ViewState["college_name"] = My.get_college_name();
                        mycode.bind_all_ddl_with_id(ddlsession, "Select Session,session_id from session_details    order by Session ");
                        mycode.bind_all_ddl_with_id(ddl_month, "Select Month,Month_Id from Month_Index    order by Position ");


                        mycode.bind_all_ddl_with_id_cap_All(ddl_Class, "select Course_Name,course_id from Add_course_table order by Position asc");

                        string pagename_current = Path.GetFileName(Request.Path);
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];

                        // next session 
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Upload_dues");
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

        protected void btn_reset_Click(object sender, EventArgs e)
        {
            Response.Redirect("Previeus_Year_Dues_Calculation.aspx", false);
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session", "warning");
                    ddlsession.Focus();
                }
                else
                {
                    if (ViewState["Is_add"].ToString() == "1")
                    {
                        Find_data();
                    }
                    else if (ViewState["Is_Edit"].ToString() == "1")
                    {
                        Find_data();
                    }
                    else
                    {
                        Alertme(My.get_restricted_message(), "warning");
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void Find_data()
        {
            string check = "select * from Dues_calculation_status where  Session_id_current='" + ddlsession.SelectedValue + "' and Class_id='" + ddl_Class.SelectedValue + "' and Status='Sucess'";
            if (ddl_Class.SelectedItem.Text == "ALL")
            {
                check = "select * from Dues_calculation_status where  Session_id_current='" + ddlsession.SelectedValue + "' and Status='Sucess'";
            }
            DataTable dt12 = mycode.FillData(check);
            if (dt12.Rows.Count == 0)
            {
                string query = "Select * from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Status='1' and class_id='" + ddl_Class.SelectedValue + "' order by rollnumber asc";
                if (ddl_Class.SelectedItem.Text == "ALL")
                {
                    query = "Select * from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Status='1' order by rollnumber asc";
                }
                transferdues.Visible = false;
                DataTable dt1 = mycode.FillData(query);
                if (dt1.Rows.Count == 0)
                { }
                else
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        string admissionserialnumber = dt1.Rows[i]["admissionserialnumber"].ToString();
                        string studentname = dt1.Rows[i]["studentname"].ToString();
                        string Section = dt1.Rows[i]["Section"].ToString();
                        string rollnumber = dt1.Rows[i]["rollnumber"].ToString();
                        string classdata = dt1.Rows[i]["class"].ToString();
                        string session = dt1.Rows[i]["session"].ToString();
                        string Session_id = dt1.Rows[i]["Session_id"].ToString();
                        ViewState["classid"] = dt1.Rows[i]["Class_id"].ToString();
                        ViewState["Section"] = dt1.Rows[i]["Section"].ToString();
                        ViewState["category_id"] = dt1.Rows[i]["Category_id"].ToString();
                        ViewState["SubCategory_id"] = dt1.Rows[i]["SubCategory_id"].ToString();
                        ViewState["sessionid"] = dt1.Rows[i]["Session_id"].ToString();
                        ViewState["session"] = dt1.Rows[i]["session"].ToString();
                        if (dt1.Rows[i]["hosteltaken"].ToString() == "")
                        {
                            ViewState["hostaltaken"] = "No";
                        }
                        else if (dt1.Rows[i]["hosteltaken"].ToString().ToLower() == "no")
                        {
                            ViewState["hostaltaken"] = "No";
                        }
                        else if (dt1.Rows[i]["hosteltaken"].ToString().ToLower() == "yes")
                        {
                            ViewState["hostaltaken"] = "Yes";
                        }
                        else
                        {
                            ViewState["hostaltaken"] = "No";
                        }
                        string hosteltaken = ViewState["hostaltaken"].ToString();
                        string Class_id = dt1.Rows[i]["Class_id"].ToString();



                        string Transfer_Status = "";
                        if (ddl_is_transfered.SelectedValue == "1")
                        {
                            if (dt1.Rows[i]["Transfer_Status"].ToString() == "New")
                            {
                                Transfer_Status = "New";
                            }
                            else
                            {
                                Transfer_Status = "NT";
                            }
                        }
                        else
                        {
                            // === IF TRANSFERRED
                            if (dt1.Rows[i]["Transfer_Status_Old"].ToString() == "New")
                            {
                                Transfer_Status = "New";
                            }
                            else
                            {
                                Transfer_Status = "NT";
                            }
                        }


                        string transportationtaken = dt1.Rows[i]["transportationtaken"].ToString();
                        bool day_bording = My.toBool(dt1.Rows[i]["is_applied_dayboarding"].ToString());
                        bool day_bording_with_lunch = My.toBool(dt1.Rows[i]["day_boarding_with_lunch"].ToString());

                        string category_id = dt1.Rows[i]["category_id"].ToString();
                        string SubCategory_id = dt1.Rows[i]["SubCategory_id"].ToString();
                        string Transportation_Id = dt1.Rows[i]["Transportation_Id"].ToString();
                        ViewState["Transfer_Status"] = Transfer_Status;


                        Dictionary<string, object> dc2 = mycode.Bind_Transport_data_for_assined_student(dt1.Rows[i]["Session_id"].ToString(), dt1.Rows[i]["Class_id"].ToString(), admissionserialnumber);
                        ViewState["Transport_id"] = (String)dc2["Transport_id"];
                        ViewState["TransportPath_id"] = (String)dc2["TransportPath_id"];
                        ViewState["Boarding_Point_id"] = (String)dc2["Boarding_Point_id"];
                        ViewState["Transport_Assigned_Id"] = (String)dc2["Transport_Assigned_Id"];
                        ViewState["Month_name"] = (String)dc2["Month_name"];
                        ViewState["Month_id"] = (String)dc2["Month_id"];
                        ViewState["Year_month"] = (String)dc2["Year_month"];
                        ViewState["Sheet_Id"] = (String)dc2["Sheet_Id"];


                        Dictionary<string, object> dc1 = mycode.Bind_hostel_data_for_assined_student(dt1.Rows[i]["Session_id"].ToString(), dt1.Rows[i]["Class_id"].ToString(), admissionserialnumber);
                        ViewState["Hostel_id"] = (String)dc1["Hostel_id"];
                        ViewState["Room_Category_id"] = (String)dc1["Room_Category_id"];
                        ViewState["From_month_name"] = (String)dc1["From_month_name"];
                        ViewState["From_month_id"] = (String)dc1["From_month_id"];
                        ViewState["Assined_Year_Month"] = (String)dc1["Assined_Year_Month"];
                        ViewState["Hostel_assign_id"] = (String)dc1["Hostel_assign_id"];


                        string Hostel_assign_id = ViewState["Hostel_assign_id"].ToString();
                        string Hostel_id = ViewState["Hostel_id"].ToString();
                        string Room_Category_id = ViewState["Room_Category_id"].ToString();

                        string Branch_id = dt1.Rows[i]["Branch_id"].ToString();

                        ViewState["IsBoarding"] = "0";
                        ViewState["parameteridS"] = "4";
                        string queryS = "select * from Student_mapping_with_boarding_with_lunch where Session_id='" + Session_id + "' and Admission_no='" + admissionserialnumber + "' and Class_id='" + Class_id + "'";
                        DataTable dts = My.dataTable(queryS);
                        if (dts.Rows.Count != 0)
                        {
                            ViewState["LunchMnthName"] = dts.Rows[0]["Month_name"].ToString();
                            ViewState["LunchMnthId"] = dts.Rows[0]["Month_id"].ToString();
                            ViewState["IsBoarding"] = "1";
                        }


                        double totaldues_admision_fee = 0;
                        if (ddl_with_admission.SelectedValue == "1")
                        {
                            totaldues_admision_fee = find_admission_dues(Session_id, hosteltaken, admissionserialnumber, session, Class_id, Transfer_Status, Hostel_id);
                        }

                        double totaldues_monthley = find_monthley_dues(Session_id, hosteltaken, admissionserialnumber, session, Class_id, Transfer_Status, classdata, Hostel_id, transportationtaken, day_bording, day_bording_with_lunch, category_id, SubCategory_id, Transportation_Id, Section, Branch_id, Room_Category_id, Hostel_assign_id);
                        double final_total_dues_amount = totaldues_admision_fee + totaldues_monthley;
                        double latefine = 0;
                        insert_insert_data_Dues_Month_Calculation(totaldues_monthley, totaldues_admision_fee, admissionserialnumber, Session_id, latefine, Class_id);
                    }
                    transferdues.Visible = true;
                }
            }
            else
            {
                btn_excel2.Visible = true;
                Alertme("Sorry you have already calculated dues.So you cannot recalculate", "warning");
            }
        }

        private void Bind_grid_view()
        {
            string qrys = "Select ar.studentname,ar.father_mob,ar.class,ar.admissionserialnumber,ar.rollnumber,ar.Section,ar.session,dmc.Admission_fee as admission_fee_annual_fee,dmc.Month_Fee,dmc.Total_fee from Dues_Month_Calculation dmc join admission_registor ar on dmc.Admission_no=ar.admissionserialnumber and dmc.Session_id=ar.Session_id join Add_course_table ac on ar.Class_id=ac.course_id where dmc.Session_id='" + ddlsession.SelectedValue + "' and dmc.Class_id='" + ddl_Class.SelectedValue + "' order by ac.Position,ar.rollnumber";
            if (ddl_Class.SelectedItem.Text == "ALL")
            {
                qrys = "Select ar.studentname,ar.father_mob,ar.class,ar.admissionserialnumber,ar.rollnumber,ar.Section,ar.session,dmc.Admission_fee as admission_fee_annual_fee,dmc.Month_Fee,dmc.Total_fee from Dues_Month_Calculation dmc join admission_registor ar on dmc.Admission_no=ar.admissionserialnumber and dmc.Session_id=ar.Session_id join Add_course_table ac on ar.Class_id=ac.course_id where dmc.Session_id='" + ddlsession.SelectedValue + "' order by ac.Position,ar.rollnumber";
            }
            DataTable dt = mycode.FillData(qrys);
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                export_to_excel(dt, "Previusyeardues_list");
            }
        }
        protected void btn_excel_Click(object sender, EventArgs e)
        {
            try
            {
                Bind_grid_view();
            }
            catch
            {
            }
        }
        private void export_to_excel(DataTable dt, string file)
        {
            string FileName = file + DateTime.Now + ".xls";
            string attachment = "attachment; filename=" + FileName;
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            Response.ContentEncoding = System.Text.Encoding.Unicode;
            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
            string tab = "";
            foreach (DataColumn dc in dt.Columns)
            {
                Response.Write(tab + dc.ColumnName);
                tab = "\t";
            }
            Response.Write("\n");
            int i;
            foreach (DataRow dr in dt.Rows)
            {
                tab = "";
                for (i = 0; i < dt.Columns.Count; i++)
                {
                    Response.Write(tab + dr[i].ToString());
                    tab = "\t";
                }
                Response.Write("\n");
            }
            Response.End();
        }

        private void insert_insert_data_Dues_Month_Calculation(double totaldues_monthley, double totaldues_admision_fee, string admissionserialnumber, string session_id, double latefine, string Class_id)
        {
            double total = totaldues_monthley + totaldues_admision_fee + latefine;
            DataTable dt = mycode.FillData("Select * from Dues_Month_Calculation where Session_id='" + session_id + "' and Admission_no='" + admissionserialnumber + "' ");//My.FillDatastatic("Select Month from Month_Index where Position='1'  ");
            if (dt.Rows.Count == 0)
            {
                SqlCommand cmd;
                string query = "INSERT INTO Dues_Month_Calculation (Session_id,Admission_no,Admission_fee,Month_Fee,Late_fine,Date,Idate,User_id,Class_id,Total_fee) values (@Session_id,@Admission_no,@Admission_fee,@Month_Fee,@Late_fine,@Date,@Idate,@User_id,@Class_id,@Total_fee)";

                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Session_id", session_id);
                cmd.Parameters.AddWithValue("@Admission_no", admissionserialnumber);
                cmd.Parameters.AddWithValue("@Admission_fee", totaldues_admision_fee.ToString("0.00"));
                cmd.Parameters.AddWithValue("@Month_Fee", totaldues_monthley.ToString("0.00"));
                cmd.Parameters.AddWithValue("@Late_fine", latefine.ToString("0.00"));
                cmd.Parameters.AddWithValue("@Date", mycode.date());
                cmd.Parameters.AddWithValue("@Idate", mycode.date());
                cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Class_id", Class_id);
                cmd.Parameters.AddWithValue("@Total_fee", total.ToString("0.00"));
                if (My.InsertUpdateData(cmd))
                {
                }
                else
                {


                }
            }
            else
            {
                string id = dt.Rows[0]["Id"].ToString();
                SqlCommand cmd2;
                string query2 = "Update Dues_Month_Calculation set  Admission_fee=@Admission_fee,Month_Fee=@Month_Fee,Late_fine=@Late_fine,Date=@Date,Idate=@Idate,User_id=@User_id,Class_id=@Class_id,Total_fee=@Total_fee where Session_id=@Session_id and Admission_no=@Admission_no";
                cmd2 = new SqlCommand(query2);
                cmd2.Parameters.AddWithValue("@Session_id", session_id);
                cmd2.Parameters.AddWithValue("@Admission_no", admissionserialnumber);
                cmd2.Parameters.AddWithValue("@Admission_fee", totaldues_admision_fee.ToString());
                cmd2.Parameters.AddWithValue("@Month_Fee", totaldues_monthley.ToString());
                cmd2.Parameters.AddWithValue("@Late_fine", latefine);
                cmd2.Parameters.AddWithValue("@Date", mycode.date());
                cmd2.Parameters.AddWithValue("@Idate", mycode.date());
                cmd2.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                cmd2.Parameters.AddWithValue("@Class_id", Class_id);
                cmd2.Parameters.AddWithValue("@Total_fee", total.ToString("0.00"));
                if (My.InsertUpdateData(cmd2))
                {


                }
            }
        }

        private double find_admission_dues(string session_id, string hosteltaken, string admissionserialnumber, string session, string class_id, string Transfer_Status, string Hostel_id)
        {
            double totalpay = 0;
            string parameter = ""; string parameter2 = "";
            string parameter_id = "";

            string Discount_on = "";

            if (Transfer_Status == "New")
            {
                parameter2 = "AdmissionFee";
                parameter = hosteltaken.ToUpper() == "NO" ? "AdmissionFee" : "HostelAdmissionFee";
                if (parameter == "AdmissionFee")
                {
                    parameter2 = "HostelAdmissionFee";
                }

                parameter_id = hosteltaken.ToString().ToUpper() == "NO" ? "1" : "5";
                ViewState["parameter"] = parameter;
                Discount_on = "Admission";
            }
            else
            {
                parameter2 = "AnnualFee";
                parameter = hosteltaken.ToUpper() == "NO" ? "AnnualFee" : "HostelAnnualFee";
                if (parameter == "AnnualFee")
                {
                    parameter2 = "HostelAnnualFee";
                }
                parameter_id = hosteltaken.ToUpper() == "NO" ? "2" : "6";
                ViewState["parameter"] = parameter;
                Discount_on = "Annual";
            }
            string qry = "select *,(payable-cast(disc_amount as float)-cast(paid as float)) net_payable from(select '0' payable_after_disc,session,feetype,cast(payable as float) payable,paid,dues,status,content_id, isnull((Disc),0) disc_amount from dbo.[Typewise_fee_collection] WHERE admission_no='" + admissionserialnumber + "' and (parameter='" + parameter + "' or parameter='" + parameter2 + "') and session='" + session + "')t";
            DataTable fee_dt = My.dataTable(qry);
            if (fee_dt.Rows.Count == 0)
            {
                if (ViewState["Hostel_id"].ToString() == "0")
                {
                    qry = "select *,(payable-cast(disc_amount as float)) net_payable from(select '0' payable_after_disc,fmc.session,cm.content feetype,cast(fmc.amount as float) payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where    admission_no='" + admissionserialnumber + "' and   parameter_id='" + parameter_id + "' and session='" + session + "' and fee_head_id=cm.content_id and Discount_on='" + Discount_on + "' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where  Class_id='" + class_id + "' and admission_no='All'  and parameter_id='" + parameter_id + "' and session='" + session + "' and fee_head_id=cm.content_id and Discount_on='" + Discount_on + "' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'))),0) disc_amount   from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='" + parameter + "'  and session='" + session + "' and class_id='" + class_id + "' )t";
                }
                else
                {
                    qry = "select *,(payable-cast(disc_amount as float)) net_payable from(select '0' payable_after_disc,fmc.session,cm.content feetype,cast(fmc.amount as float) payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where   Hostel_Id=" + ViewState["Hostel_id"].ToString() + " and admission_no='" + admissionserialnumber + "'  and parameter_id='" + parameter_id + "' and session='" + session + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Hostel_Id=" + ViewState["Hostel_id"].ToString() + " and Class_id='" + class_id + "' and admission_no='All'  and parameter_id='" + parameter_id + "' and session='" + session + "' and fee_head_id=cm.content_id and Discount_on='Admission' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["SubCategory_id"].ToString() + "'))),0) disc_amount   from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='" + parameter + "'  and session='" + session + "' and class_id='" + class_id + "' and fmc.Hostel_Id='" + ViewState["Hostel_id"].ToString() + "')t";
                }
                fee_dt = My.dataTable(qry);
            }
            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                double payable = 0, paid = 0, dues = 0, disc = 0, payble_after_disc = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    dr["payable_after_disc"] = My.toDouble(dr["payable"]) - My.toDouble(dr["disc_amount"]) - My.toDouble(dr["paid"]);
                    payable += My.toDouble(dr["payable"]);
                    paid += My.toDouble(dr["paid"]);
                    dues += My.toDouble(dr["dues"]);
                    disc += My.toDouble(dr["disc_amount"]);
                    payble_after_disc += My.toDouble(dr["payable_after_disc"]);
                }
                string previous_dues = get_previous_amount(admissionserialnumber, session_id, class_id);
                totalpay = payble_after_disc + My.toDouble(previous_dues);

            }

            return totalpay;


        }

        private string get_previous_amount(string regid, string Session_id, string Class_id)
        {

            DataTable dt = mycode.FillData("select Dues_Amount from Previous_Year_Dues  where Session_id='" + Session_id + "' and AdmissionNumber='" + regid + "'  and Class_id='" + Class_id + "' and   Status='Unpaid'");
            if (dt.Rows.Count == 0)
            {
                return "0.00";
            }
            else
            {

                return dt.Rows[0][0].ToString();
            }
        }

        private double find_monthley_dues(string session_id, string hosteltaken, string regid, string session, string class_id, string transfer_Status, string classname, string Hostel_id, string transportationtaken, bool day_bording, bool day_bording_with_lunch, string category_id, string SubCategory_id, string Transportation_Id, string Section, string Branch_id, string Room_Category_id, string Hostel_assign_id)
        {
            string parameter = ""; string parameter2 = "MonthlyFee";
            string parameter_id = "";

            string Discount_on = "";
            if (transfer_Status == "New")
            {
                parameter = hosteltaken.ToUpper() == "NO" ? "MonthlyFee" : "HostelMonthlyFee";
                if (parameter == "MonthlyFee")
                {
                    parameter2 = "HostelMonthlyFee";
                }
                ViewState["parameter"] = parameter;
                Discount_on = "Admission";
                parameter_id = hosteltaken.ToUpper() == "NO" ? "1" : "5";
            }
            else
            {
                parameter = hosteltaken.ToUpper() == "NO" ? "MonthlyFee" : "HostelMonthlyFee";
                if (parameter == "MonthlyFee")
                {
                    parameter2 = "HostelMonthlyFee";
                }
                parameter_id = hosteltaken.ToUpper() == "NO" ? "2" : "6";
                ViewState["parameter"] = parameter;
                Discount_on = "Annual";



            }

            double total = 0;

            mycode.executequery("delete from Typewise_fee_collection_temp_dues_calculation where admission_no='" + regid + "' and session='" + session + "'");
            List<string> month_lst = new List<string>();
            string slipno = "", entry_id = "";
            DataTable dt = mycode.FillData("Select Month,Month_Id from Month_Index   order by Position asc ");//My.FillDatastatic("Select Month from Month_Index where Position='1'  ");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                for (int iS = 0; iS < dt.Rows.Count; iS++)
                {
                    DataTable feedt = new DataTable();
                    string monthname = dt.Rows[iS]["Month"].ToString();
                    if (ViewState["IsBoarding"].ToString() == "1")
                    {
                        int mnthids = My.tomonth_number(monthname);
                        if (My.toint(ViewState["LunchMnthId"].ToString()) <= mnthids)
                        {
                            ViewState["parameteridS"] = "44";
                        }
                        else
                        {
                            ViewState["parameteridS"] = "4";
                        }
                    }


                    string type = "";
                    month_lst.Add(monthname);
                    if (My.dataTable("select  * from dbo.[Typewise_fee_collection]   where admission_no='" + regid + "' and session='" + session + "' and month='" + monthname + "' and (parameter='" + parameter + "' or parameter='" + parameter2 + "')").Rows.Count > 0)
                    {
                        feedt = My.dataTable("select id, admission_no,class, session,parameter, feetype content,payable amount,'0'Column1,payable amount,'0'Column2,month months,'0'content_id,'0'Ledger,'0'Column3,'0'Column4,'0'Column5,Disc as disc_amount,isnull(paid,'0') previously_paid from dbo.[Typewise_fee_collection]   where admission_no='" + regid + "' and session='" + session + "' and month='" + monthname + "' and (parameter='" + parameter + "' or parameter='" + parameter2 + "') and content_id!='6121' and status='Dues'");
                        type = "Calculated";
                        if (feedt.Rows.Count > 0)
                        {
                            send_in_typewise_fee(feedt, Section, regid, monthname, Branch_id, session_id);
                        }
                    }
                    else
                    {
                        Dictionary<string, object> dc = new Dictionary<string, object>();
                        dc["admission_no"] = regid;
                        dc["session_id"] = session_id;
                        dc["class"] = classname;
                        dc["session"] = session;
                        dc["class_id"] = class_id;
                        dc["hosteltaken"] = hosteltaken;
                        dc["months"] = monthname;
                        dc["tr_ledger"] = My.is_combine ? "School" : "Transport";
                        dc["hostel_id"] = Hostel_id;
                        dc["Room_Category_id"] = Room_Category_id;
                        dc["Hostel_assig_id"] = Hostel_assign_id;

                        dc["day_boarding"] = day_bording;
                        dc["day_boarding_lunch"] = day_bording_with_lunch;

                        dc["category_id"] = category_id;
                        dc["sub_category_id"] = SubCategory_id;

                        dc["TransportationPath_id"] = ViewState["TransportPath_id"].ToString();
                        dc["transportportation_id"] = ViewState["Transport_id"].ToString();
                        dc["Boarding_Point_id"] = ViewState["Boarding_Point_id"].ToString();

                        dc["parameter_id"] = ViewState["parameteridS"].ToString();


                        string cunrt_session = session;
                        string[] stringSeparators = new string[] { "-" };
                        string[] arr = cunrt_session.Split(stringSeparators, StringSplitOptions.None);
                        string session_frst_year = arr[0];
                        string session_last_year = arr[1];
                        int session_s_year = My.toint(session_frst_year);
                        int s_year = My.toint(session_frst_year);
                        string monthid = My.tomonth_numberstring(monthname);
                        int pay_month = My.toint(monthid);
                        s_year = My.check_start_months(pay_month, s_year);
                        dc["monthid"] = s_year + monthid;
                        feedt = My.dataTableSP("sp_fetch_monthly_fee", dc);
                        feedt.Columns.Add("previously_paid");
                        send_in_typewise_fee(feedt, Section, regid, monthname, Branch_id, session_id);
                    }
                }

                string month = "";
                double fee = 0, disc = 0, paid_prev = 0;
                double total_payable = 0;
                string late_fine_month = "", month_position = "";
                string qry = " select *  from Typewise_fee_collection_temp_dues_calculation  where admission_no='" + regid + "' and session='" + session + "' and status='Dues' and parameter like '%" + parameter + "%' and branchid='" + Branch_id + "' order by cast(Position as float)";

                SqlDataAdapter ad = new SqlDataAdapter(qry, My.con);
                DataSet ds = new DataSet();
                ad.Fill(ds, "Typewise_fee_collection");
                DataTable tdt = ds.Tables[0];
                if (tdt.Rows.Count == 0)
                {
                }
                else
                {
                    foreach (DataRow dr in tdt.Rows)
                    {
                        month = dr["month"].ToString();
                        total_payable = My.toDouble(dr["payable"]) - My.toDouble(dr["Disc"]) - My.toDouble(dr["previously_paid"]);

                        total += My.toDouble(total_payable);
                    }
                }
            }
            return total;
        }






        private void send_in_typewise_fee(DataTable feedt, string Section, string regid, string monthname, string Branch_id, string Session_id)
        {
            double fine_amt = 0;
            double fine = My.toDouble(fine_amt);
            if (fine > 0)
            {
                int mnth_idss = My.tomonth_number(monthname);
                string month_id = My.getMonthS_twoDigit(mnth_idss.ToString());
                DataTable dt = mycode.FillData("select Fine_amount from Temp_fine_monthwise where Session_id='" + Session_id + "' and Admission_no='" + feedt.Rows[0]["admission_no"].ToString() + "' and Month_id='" + month_id + "' and Branch_id='" + Branch_id + "'");
                if (dt.Rows.Count > 0)
                {
                    fine = My.toDouble(dt.Rows[0]["Fine_amount"].ToString());
                    My.exeSql("insert into Typewise_fee_collection(admission_no,class,session,parameter,Date,idate,feetype,payable,paid,dues,status,month,content_id,transection,Ledger,is_readyfor_sync,is_sync,is_sync_dues_diary,Disc,section,user_id,branchid) values ('" + feedt.Rows[0]["admission_no"].ToString() + "','" + feedt.Rows[0]["class"].ToString() + "','" + feedt.Rows[0]["session"].ToString() + "','" + feedt.Rows[0]["parameter"].ToString() + "','" + mycode.date() + "','" + mycode.idate() + "','Late Fine','" + My.toDouble(fine).ToString("0.00") + "','0','" + My.toDouble(fine).ToString("0.00") + "','Dues','" + monthname + "','6121','','School','false','false','false','0.00','" + Section + "','" + regid + "','" + Branch_id + "')");
                }
            }


            bool entrys = false;
            foreach (DataRow dr in feedt.Rows)
            {
                if (My.dataTable("select  * from dbo.[Typewise_fee_collection]   where admission_no='" + regid + "' and session='" + Session + "' and month='" + dr["months"].ToString() + "' and parameter='" + dr["parameter"].ToString() + "'").Rows.Count == 0)
                {
                    double paidAmt = My.toDouble(dr["previously_paid"].ToString()) + My.toDouble(dr["disc_amount"].ToString());
                    if (My.toDouble(dr["amount"].ToString()) > paidAmt)
                    {
                        My.exeSql("insert into Typewise_fee_collection_temp_dues_calculation(admission_no,class,session,parameter,Date,idate,feetype,payable,paid,dues,status,month,content_id,transection,Ledger,is_readyfor_sync,is_sync,is_sync_dues_diary,Disc,section,user_id,branchid,previously_paid) values ('" + dr["admission_no"].ToString() + "','" + dr["class"].ToString() + "','" + dr["session"].ToString() + "','" + dr["parameter"].ToString() + "','" + mycode.date() + "','" + mycode.idate() + "','" + dr["content"].ToString() + "','" + My.toDouble(dr["amount"].ToString()).ToString("0.00") + "','0','" + My.toDouble(dr["amount"].ToString()).ToString("0.00") + "','Dues','" + dr["months"].ToString() + "','" + dr["content_id"].ToString() + "','','School','false','false','false','" + My.toDouble(dr["disc_amount"].ToString()).ToString("0.00") + "','" + Section + "','" + regid + "','" + Branch_id + "','" + My.toDouble(dr["previously_paid"].ToString()) + "')");
                    }
                }
                else
                {
                }
            }
        }





        protected void rd_trasfer_anual_CheckedChanged(object sender, EventArgs e)
        {
            if (rd_trasfer_anual.Checked == true)
            {
                month.Visible = false;
            }
        }

        protected void rd_transfer_Monthlyfee_CheckedChanged(object sender, EventArgs e)
        {
            if (rd_transfer_Monthlyfee.Checked == true)
            {
                month.Visible = true;
            }

        }

        protected void ddlsession_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlsession.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
            }
            else
            {
                string cunrt_session = ddlsession.SelectedItem.Text;
                string[] stringSeparators = new string[] { "-" };
                string[] arr = cunrt_session.Split(stringSeparators, StringSplitOptions.None);
                string session_frst_year = arr[0];
                string session_last_year = arr[1];

                int frst_year = My.toInt(session_frst_year) + 1;
                int last_year = My.toInt(session_last_year) + 1;

                lbl_next_session.Text = frst_year.ToString() + "-" + last_year.ToString();

            }

        }

        protected void btn_fina_transfer_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session", "warning");
                    ddlsession.Focus();
                }
                else
                {
                    try
                    {
                        if (rd_trasfer_anual.Checked == true)
                        {
                            bool avl_session = get_check_avel_sesion(lbl_next_session.Text);
                            if (avl_session == true)
                            {
                                string getsessionid = My.get_sess_prm(lbl_next_session.Text);
                                if (getsessionid == "0")
                                {
                                    Alertme("Please create session :+" + lbl_next_session.Text, "warning");
                                }
                                else
                                {
                                    final_save_data(getsessionid);
                                    Alertme("Successfully does amount transferred  in the next session annual fee.", "success");
                                    transferdues.Visible = false;
                                }
                            }
                            else
                            {
                                Alertme("Please create session :+" + lbl_next_session.Text, "warning");
                            }
                        }
                        else if (rd_transfer_Monthlyfee.Checked == true)
                        {
                            bool avl_session = get_check_avel_sesion(lbl_next_session.Text);
                            if (avl_session == true)
                            {
                                string getsessionid = My.get_sess_prm(lbl_next_session.Text);
                                if (getsessionid == "0")
                                {
                                    Alertme("Please create session :+" + lbl_next_session.Text, "warning");
                                }
                                else
                                {
                                    if (ddl_month.SelectedItem.Text == "Select")
                                    {
                                        Alertme("Please select month name", "warning");
                                    }
                                    else
                                    {
                                        //mycode.executequery("delete from Misc_Fee_Master_Studentwise where    Session_id='" + getsessionid + "' and Old_year_Dues_Type='Yes'");
                                        final_save_data(getsessionid);
                                        Alertme("Successfully does amount transferred  in the next session monthly fee.", "success");
                                        transferdues.Visible = false;
                                    }
                                }
                            }
                            else
                            {
                                Alertme("Please create session :+" + lbl_next_session.Text, "warning");
                            }
                        }
                        else
                        {
                            Alertme("Please choose annual Fee or monthly fee transfer", "warning");
                        }
                    }
                    catch (Exception ex)
                    {
                        My.submitException(ex, "deuse transfer");
                    }
                }
            }
            catch
            {

            }
        }

        private bool get_check_avel_sesion(string session)
        {
            DataTable dt = mycode.FillData("Select * from session_details where Session='" + session + "' ");
            if (dt.Rows.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void final_save_data(string newssionid)
        {
            if (ddl_Class.SelectedItem.Text == "ALL")
            {
                DataTable dtc = My.dataTable("select Course_Name,course_id from Add_course_table order by Position asc");
                if (dtc.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtc.Rows)
                    {
                        save_in_Dues_calculation_status(newssionid, dr["course_id"].ToString());
                    }
                }
            }
            else
            {
                save_in_Dues_calculation_status(newssionid, ddl_Class.SelectedValue);
            }
        }

        private void save_in_Dues_calculation_status(string newssionid, string course_id)
        {
            string check = "select * from Dues_calculation_status where  Session_id_next='" + newssionid + "' and Session_id_current='" + ddlsession.SelectedValue + "' and Class_id='" + course_id + "'";
            DataTable dt1 = mycode.FillData(check);
            if (dt1.Rows.Count == 0)
            {
                SqlCommand cmd;
                string savequery = "INSERT INTO Dues_calculation_status (Session_id_current,Session_id_next,Created_by,Created_date,Created_time,Status,Class_id) values (@Session_id_current,@Session_id_next,@Created_by,@Created_date,@Created_time,@Status,@Class_id)";
                cmd = new SqlCommand(savequery);
                cmd.Parameters.AddWithValue("@Session_id_current", ddlsession.SelectedValue);
                cmd.Parameters.AddWithValue("@Session_id_next", newssionid);
                cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                cmd.Parameters.AddWithValue("@Created_time", mycode.idate());
                cmd.Parameters.AddWithValue("@Status", "Pending");
                cmd.Parameters.AddWithValue("@Class_id", course_id);
                if (My.InsertUpdateData(cmd))
                {
                }
            }
            else
            {
                string id = dt1.Rows[0]["Id"].ToString();
                SqlCommand cmd;
                string savequery = "Update Dues_calculation_status set Session_id_current=@Session_id_current,Session_id_next=@Session_id_next,Created_date=@Created_date,Created_time=@Created_time,Status=@Status,Class_id=@Class_id where Id = @Id";
                cmd = new SqlCommand(savequery);
                cmd.Parameters.AddWithValue("@Session_id_current", ddlsession.SelectedValue);
                cmd.Parameters.AddWithValue("@Session_id_next", newssionid);
                cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                cmd.Parameters.AddWithValue("@Created_time", mycode.idate());
                cmd.Parameters.AddWithValue("@Class_id", course_id);
                cmd.Parameters.AddWithValue("@Status", "Pending");
                cmd.Parameters.AddWithValue("@Id", id);
                if (My.InsertUpdateData(cmd))
                {
                }
            }

            bool finalsubmit = false;
            string query = "select Total_fee,Admission_no,Class_id,Session_id from Dues_Month_Calculation where Session_id=" + ddlsession.SelectedValue + " and Class_id='" + course_id + "'";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string totalfee = dt.Rows[i]["Total_fee"].ToString();
                    string Admission_no = dt.Rows[i]["Admission_no"].ToString();
                    string old_classid = dt.Rows[i]["Class_id"].ToString();
                    string old_Session_id = dt.Rows[i]["Session_id"].ToString();

                    if (My.toDouble(totalfee) > 0)
                    {
                        if (rd_trasfer_anual.Checked == true)
                        {
                            final_save_data_Previous_Year_Dues(Admission_no, totalfee, old_classid, old_Session_id, newssionid);
                        }
                        else
                        {
                            final_save_data_Previous_Year_monthDues(Admission_no, totalfee, old_classid, old_Session_id, newssionid);
                        }
                    }
                    finalsubmit = true;
                }
                if (finalsubmit == true)
                {
                    My.exeSql("update Dues_calculation_status set Status='Sucess' where Session_id_next='" + newssionid + "' and Session_id_current='" + ddlsession.SelectedValue + "' and Class_id='" + course_id + "'");
                    transferdues.Visible = false;
                }
            }
        }

        private void final_save_data_Previous_Year_monthDues(string admission_no, string totalfee, string old_classid, string old_Session_id, string newssionid)
        {
            DataTable dt = mycode.FillData("select * from Misc_Fee_Master_Studentwise where Admission_No='" + admission_no + "' and Month='" + ddl_month.Text + "' and Session_id='" + newssionid + "'  and Perticular='Previous Year Dues'");
            if (dt.Rows.Count == 0)
            {
                SqlCommand cmd;
                string query = "INSERT INTO Misc_Fee_Master_Studentwise (Admission_No,Month,Session,Session_id,Perticular,Amount,Old_year_Dues_Type,Date,Idate,Created_by) values (@Admission_No,@Month,@Session,@Session_id,@Perticular,@Amount,@Old_year_Dues_Type,@Date,@Idate,@Created_by)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Admission_No", admission_no);
                cmd.Parameters.AddWithValue("@Month", ddl_month.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Session", lbl_next_session.Text);
                cmd.Parameters.AddWithValue("@Session_id", newssionid);
                cmd.Parameters.AddWithValue("@Perticular", "Previous Year Dues");
                cmd.Parameters.AddWithValue("@Amount", totalfee);
                cmd.Parameters.AddWithValue("@Old_year_Dues_Type", "Yes");
                cmd.Parameters.AddWithValue("@Date", mycode.date());
                cmd.Parameters.AddWithValue("@Idate", mycode.idate());
                cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                if (My.InsertUpdateData(cmd))
                {
                }
            }
            else
            {

                string id = dt.Rows[0]["Id"].ToString();
                SqlCommand cmd;
                string query2 = "Update Misc_Fee_Master_Studentwise set Admission_No=@Admission_No,Month=@Month,Session=@Session,Session_id=@Session_id,Amount=@Amount,Date=@Date,Idate=@Idate,Created_by=@Created_by where Id = @Id";
                cmd = new SqlCommand(query2);
                cmd.Parameters.AddWithValue("@Admission_No", admission_no);
                cmd.Parameters.AddWithValue("@Month", ddl_month.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Session", lbl_next_session.Text);
                cmd.Parameters.AddWithValue("@Session_id", newssionid);
                cmd.Parameters.AddWithValue("@Amount", totalfee);
                cmd.Parameters.AddWithValue("@Date", mycode.date());
                cmd.Parameters.AddWithValue("@Idate", mycode.idate());
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                if (My.InsertUpdateData(cmd))
                {
                }
            }
        }

        private void final_save_data_Previous_Year_Dues(string admission_no, string totalfee, string old_classid, string old_Session_id, string newssionid)
        {
            DataTable dt = mycode.FillData("select * from Misc_Fee_Master_Studentwise where Admission_No='" + admission_no + "' and Session_id='" + newssionid + "' and Perticular='Previous Year Dues'");
            if (dt.Rows.Count == 0)
            {
                SqlCommand cmd;
                string query = "INSERT INTO Misc_Fee_Master_Studentwise (Admission_No,Session,Session_id,Perticular,Amount,Old_year_Dues_Type,Date,Idate,Created_by,Type_Mode) values (@Admission_No,@Session,@Session_id,@Perticular,@Amount,@Old_year_Dues_Type,@Date,@Idate,@Created_by,@Type_Mode)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Admission_No", admission_no);
                cmd.Parameters.AddWithValue("@Session", lbl_next_session.Text);
                cmd.Parameters.AddWithValue("@Session_id", newssionid);
                cmd.Parameters.AddWithValue("@Perticular", "Previous Year Dues");
                cmd.Parameters.AddWithValue("@Amount", totalfee);
                cmd.Parameters.AddWithValue("@Old_year_Dues_Type", "Yes");
                cmd.Parameters.AddWithValue("@Date", mycode.date());
                cmd.Parameters.AddWithValue("@Idate", mycode.idate());
                cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Type_Mode", "AnnualFee");
                if (My.InsertUpdateData(cmd))
                {
                }
            }
            else
            {
                string id = dt.Rows[0]["Id"].ToString();
                SqlCommand cmd;
                string query2 = "Update Misc_Fee_Master_Studentwise set Admission_No=@Admission_No,Session=@Session,Session_id=@Session_id,Amount=@Amount,Date=@Date,Idate=@Idate,Created_by=@Created_by where Id = @Id";
                cmd = new SqlCommand(query2);
                cmd.Parameters.AddWithValue("@Admission_No", admission_no);
                cmd.Parameters.AddWithValue("@Session", lbl_next_session.Text);
                cmd.Parameters.AddWithValue("@Session_id", newssionid);
                cmd.Parameters.AddWithValue("@Amount", totalfee);
                cmd.Parameters.AddWithValue("@Date", mycode.date());
                cmd.Parameters.AddWithValue("@Idate", mycode.idate());
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                if (My.InsertUpdateData(cmd))
                {
                }
            }



        }
    }
}