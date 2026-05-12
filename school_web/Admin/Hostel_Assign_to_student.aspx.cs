using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class Hostel_Assign_to_student : System.Web.UI.Page
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
                        if (Session["msGS"] != null)
                        {
                            Alertme(Session["msGS"].ToString(), "success");
                            Session["msGS"] = null;
                        }

                        ViewState["isEDIT"] = "0";
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["branch_id"] = mycode.get_branch_id(Session["Admin"].ToString());
                        string pagename_current = "Hostel_Assign_to_student.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        mycode.bind_all_ddl_with_id(ddlclass, "Select Course_Name,course_id from Add_course_table order by Position asc");
                        mycode.bind_all_ddl_with_id(ddlsession, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int)");
                        mycode.bind_all_ddl_with_id(ddl_session_student, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int)");
                        mycode.bind_all_ddl_with_id(ddlsessionad, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int)");
                        ddlsessionad.SelectedValue = My.get_session_id();
                        ddlsession.SelectedValue = My.get_session_id();
                        ddl_session_student.SelectedValue = My.get_session_id();
                        mycode.bind_all_ddl_with_id(ddl_month, "select Month,Month_Id from Month_Index order by Position asc");

                        mycode.bind_all_ddl_with_id(ddl_hostel, "select Hostel_name,Hostel_id from Hostels_master order by Hostel_name asc");
                        mycode.bind_all_ddl_with_id(ddl_room_cat, "select Category_name,Category_id from Hostel_room_category_master order by Category_name asc");
                        if (Request.QueryString["admNo"] != null)
                        {
                            ViewState["isEDIT"] = "1";
                            string admno = Request.QueryString["admNo"].ToString();
                            string sessionid = Request.QueryString["Sessionid"].ToString();
                            string query = "select * from admission_registor where admissionserialnumber='" + admno + "' and session_id='" + sessionid + "' and Transfer_Status in('New','NT','Transferred') and StudentStatus='AV' and Status=1 order by id asc";

                            find_details(query);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_hostel_service_master");
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
        #region WebMethoD
        [WebMethod]
        public static List<string> GetRooPath(string PathRooT, string sessioN)
        {
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(My.conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct studentname from admission_registor where studentname LIKE '%'+@studentname+'%' and Session_id='" + sessioN + "' and Status=1 ";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@studentname", PathRooT);
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

        public static List<string> GetRooPathAdmNo(string PathRooT, string sessioN)
        {
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(My.conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select admissionserialnumber from admission_registor where admissionserialnumber LIKE ''+@admissionserialnumber+'%' and Session_id='" + sessioN + "' and Status='1'";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@admissionserialnumber", PathRooT);
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

        #region FinD StudenT
        protected void btn_find_admission_no_Click(object sender, EventArgs e)
        {
            try
            {
                //empty_form();
                string query = "select * from admission_registor where admissionserialnumber='" + txt_admission_no.Text + "' and Session='" + ddlsessionad.SelectedItem.Text + "' and StudentStatus='AV'   and   Transfer_Status in ('NT','New' ) and  Status='1' and hosteltaken='No' order by id asc";
                find_details(query);
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }

        private void find_details(string query)
        {
            SqlDataAdapter ad_contactus = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad_contactus.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                std_basic_infoS.Visible = false;
                Alertme("Student details not found...", "warning");
                return;
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (ViewState["isEDIT"].ToString() == "0")
                    {
                        string queryS = "select * from Hostel_assign_master where Session_id='" + dr["Session_id"].ToString() + "' and Admission_no='" + dr["admissionserialnumber"].ToString() + "' and Class_id='" + dr["Class_id"].ToString() + "' and Status=1";
                        DataTable dts = My.dataTable(queryS);
                        if (dts.Rows.Count != 0)
                        {
                            Alertme("Selected student already mapped hostel", "warning");
                            return;
                        }
                    }
                    std_basic_infoS.Visible = true;
                    txt_student_name.Text = dr["studentname"].ToString();
                    ddlclass.SelectedValue = dr["Class_id"].ToString();
                    ddlsession.SelectedValue = dr["Session_id"].ToString();

                    //txt_section.Text = dr["Section"].ToString();
                    txtrollnumber.Text = dr["rollnumber"].ToString();
                    lbl_name.Text = dr["studentname"].ToString();
                    lbl_father_name.Text = dr["fathername"].ToString();
                    lblclass.Text = dr["class"].ToString();
                    ViewState["class_id"] = dr["Class_id"].ToString();
                    //txtsection.Text = dr["Section"].ToString();
                    lbl_admission_no.Text = dr["admissionserialnumber"].ToString();
                    txt_admission_no.Text = dr["admissionserialnumber"].ToString();

                    ViewState["Addmission_no"] = dr["admissionserialnumber"].ToString();
                    //lblhostel.Text = dr["hosteltaken"].ToString();
                    ViewState["parameter"] = dr["hosteltaken"].ToString().ToUpper() == "YES" ? "HostelMonthlyFee" : "MonthlyFee";
                    ViewState["parameter_id"] = dr["hosteltaken"].ToString().ToUpper() == "YES" ? "3" : "4";
                    // lbltransporttion.Text = dr["transportationtaken"].ToString();
                    lbl_phone.Text = dr["mobilenumber"].ToString();
                    ViewState["hostel_id"] = My.toint(dr["Hostel_id"].ToString());
                    ViewState["day_bording"] = My.toBool(dr["is_applied_dayboarding"]);
                    ViewState["day_bording_with_lunch"] = My.toBool(dr["day_boarding_with_lunch"]);
                    ViewState["group_id"] = "3";
                    ViewState["category_id"] = dr["category_id"].ToString();
                    ViewState["sub_category_id"] = dr["SubCategory_id"].ToString();
                    ViewState["classid"] = dr["Class_id"].ToString();
                    ViewState["Section"] = dr["Section"].ToString();
                    ViewState["sessionIDs"] = dr["Session_id"].ToString();
                    ViewState["admSionNo"] = dr["admissionserialnumber"].ToString();
                    ViewState["session"] = dr["session"].ToString();
                    lbl_section_roll_no.Text = ViewState["Section"].ToString() + "" + txtrollnumber.Text;

                    ViewState["StudentType"] = dr["Transfer_Status"].ToString();

                    if (dr["Transportation_Id"].ToString() == "")
                    {
                        ViewState["transportID"] = "0";
                    }
                    else
                    {
                        ViewState["transportID"] = dr["Transportation_Id"].ToString();
                    }
                    if (ViewState["day_bording_with_lunch"].ToString() == "True")
                    {
                        ViewState["parameteridS"] = "44";
                    }
                    else
                    {
                        ViewState["parameteridS"] = "4";
                    }
                }

                fetch_hostel_allocaed_details(ViewState["sessionIDs"].ToString(), ViewState["classid"].ToString(), lbl_admission_no.Text);

            }
        }
        private void fetch_hostel_allocaed_details(string session_id, string class_id, string admission_no)
        {
            DataTable dt = mycode.FillData("select *,(select top 1 Bed_name from Hostel_room_bed_master where Bed_id=Hostel_assign_master.Bed_id) as Bed_name from Hostel_assign_master where Session_id='" + session_id + "' and Class_id='" + class_id + "'   and Admission_no='" + admission_no + "' and Status='1'");
            if (dt.Rows.Count != 0)
            {
                ViewState["AssignID"] = dt.Rows[0]["Hostel_assign_id"].ToString();
                ddl_month.SelectedValue = dt.Rows[0]["From_month_id"].ToString();
                ddl_hostel.SelectedValue = dt.Rows[0]["Hostel_id"].ToString();
                ddl_room_cat.SelectedValue = dt.Rows[0]["Category_id"].ToString();
                fetch_rooms();
                ddl_room.SelectedValue = dt.Rows[0]["Room_id"].ToString();
                fetch_bed_details();

                ddl_bed.SelectedValue = dt.Rows[0]["Bed_id"].ToString();






                get_additional_service_data("select *,(select count(Id) from Hostel_assigned_additional_service where Service_id=Hostel_service_master.Service_id and Assign_id='" + dt.Rows[0]["Hostel_assign_id"].ToString() + "') as Is_added from Hostel_service_master order by Service_name asc");
                calculate_fess();
            }
        }
        protected void btnfind_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlclass.SelectedItem.Text == "Select")
                {
                    Alertme("Please select class", "warning");
                    ddlclass.Focus();
                    return;
                }
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session", "warning");
                    ddlsession.Focus();
                    return;
                }

                if (txtrollnumber.Text == "")
                {
                    Alertme("Please enter roll no", "warning");
                    txtrollnumber.Focus();
                    return;
                }

                string query = "select * from admission_registor where class='" + ddlclass.SelectedItem.Text + "' and Session='" + ddlsession.SelectedItem.Text + "'   and rollnumber='" + txtrollnumber.Text + "' and StudentStatus='AV' and    Transfer_Status in ('NT','New' ) and  Status='1' order by id asc";
                find_details(query);
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }

        protected void btn_find_name_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_session_student.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session", "warning");
                    ddl_session_student.Focus();
                    return;
                }
                if (txt_student_name.Text == "")
                {
                    Alertme("Please enter student name.", "warning");
                    txt_student_name.Focus();
                    return;
                }

                string query = "select * from admission_registor where studentname like '%" + txt_student_name.Text + "%' and session='" + ddl_session_student.SelectedItem.Text + "' and StudentStatus='AV'      and Transfer_Status in ('NT','New' ) and Status='1' order by id asc";
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
                Label lbl_session = (Label)row.FindControl("lbl_session");
                string query = "select * from admission_registor where admissionserialnumber='" + lbladmissionserialnumber.Text + "' and Session='" + lbl_session.Text + "' and StudentStatus='AV'    and Is_TC_Taken!='true' order by id asc";
                find_details(query);
                myModal2.Visible = false;
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            myModal2.Visible = false;
        }

        #endregion



        #region select
        protected void ddl_room_cat_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddl_hostel.SelectedItem.Text == "Select")
                {
                    ddl_hostel.Focus();
                    Alertme("Please select hostel.", "warning");
                    ddl_hostel.Focus();
                }
                else if (ddl_room_cat.SelectedItem.Text == "Select")
                {
                    ddl_hostel.Focus();
                    Alertme("Please select category.", "warning");
                    ddl_hostel.Focus();
                }
                else
                {
                    fetch_rooms();
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_assign_student");
            }
        }
        private void fetch_rooms()
        {
            mycode.bind_all_ddl_with_id(ddl_room, "select Room_name,Room_id from Hostel_room_master where Hostel_id='" + ddl_hostel.SelectedValue + "' and Category_id='" + ddl_room_cat.SelectedValue + "' order by Room_name asc");
            DataTable dtx = mycode.FillData("select Amount from Fee_Master where session_id='" + ViewState["sessionIDs"].ToString() + "' and Hostel_id='" + ddl_hostel.SelectedValue + "' and  Room_Category_id='" + ddl_room_cat.SelectedValue + "' and Month_id='" + ddl_month.SelectedValue + "' and class_id=" + ViewState["classid"].ToString() + "");
            if (dtx.Rows.Count.ToString() != "0")
            {
                ViewState["BedCharge"] = dtx.Rows[0]["Amount"].ToString();
            }
            else
            {
                ViewState["BedCharge"] = "0";
            }
        }
        protected void ddl_room_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddl_hostel.SelectedItem.Text == "Select")
                {
                    additionalService.Visible = false;
                    ddl_hostel.Focus();
                    Alertme("Please select hostel.", "warning");
                    ddl_hostel.Focus();
                }
                else if (ddl_room_cat.SelectedItem.Text == "Select")
                {
                    additionalService.Visible = false;
                    ddl_hostel.Focus();
                    Alertme("Please select category.", "warning");
                    ddl_hostel.Focus();
                }
                else if (ddl_room.SelectedItem.Text == "Select")
                {
                    additionalService.Visible = false;
                    ddl_hostel.Focus();
                    Alertme("Please select room.", "warning");
                    ddl_room.Focus();
                }
                else
                {
                    fetch_bed_details();
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_assign_student");
            }
        }
        private void fetch_bed_details()
        {
            additionalService.Visible = true;
            if (ViewState["isEDIT"].ToString() == "0")
            {
                mycode.bind_all_ddl_with_id(ddl_bed, "select 'Bed No. :'+Bed_name+', Bed Position '+Bed_Position,Bed_id from Hostel_room_bed_master where Bed_id not in (select Bed_id from Hostel_assign_master where  Room_id='" + ddl_room.SelectedValue + "' and Status='1' and Hostel_id=" + ddl_hostel.SelectedValue + " and Category_id=" + ddl_room_cat.SelectedValue + " and Session_id='" + ViewState["sessionIDs"].ToString() + "') and Room_id='" + ddl_room.SelectedValue + "' and Hostel_id='" + ddl_hostel.SelectedValue + "'and Category_id='" + ddl_room_cat.SelectedValue + "' order by Id asc");
            }
            else
            {
                mycode.bind_all_ddl_with_id(ddl_bed, "select 'Bed No. :'+Bed_name+', Bed Position '+Bed_Position,Bed_id from Hostel_room_bed_master where Bed_id not in (select Bed_id from Hostel_assign_master where  Room_id='" + ddl_room.SelectedValue + "' and Hostel_assign_id!='" + ViewState["AssignID"].ToString() + "' and Status='1' and Session_id='" + ViewState["sessionIDs"].ToString() + "') and Room_id='" + ddl_room.SelectedValue + "' order by Bed_name asc");
            }
            DataTable dtx = mycode.FillData("select count(id) as Total_no_of_bed,(select count(id) from Hostel_assign_master where Room_id='" + ddl_room.SelectedValue + "' and Status='1') as Added_room from Hostel_room_bed_master where Room_id='" + ddl_room.SelectedValue + "'");
            if (dtx.Rows.Count.ToString() != "0")
            {
                lbl_no_of_bed.Text = dtx.Rows[0]["Total_no_of_bed"].ToString();
                double ttl_empty_bed = (My.toDouble(dtx.Rows[0]["Total_no_of_bed"].ToString()) - My.toDouble(dtx.Rows[0]["Added_room"].ToString()));
                lbl_ttl_empty_bed.Text = ttl_empty_bed.ToString();
                lbl_bed_charge.Text = ViewState["BedCharge"].ToString();
            }

            get_additional_service_data("select *,(select count(Id) from Hostel_assigned_additional_service where Service_id=Hostel_service_master.Service_id and Assign_id='0')as Is_added from Hostel_service_master order by Service_name asc");
        }
        private void get_additional_service_data(string qry)
        {
            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {
                additionalService.Visible = false;
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                additionalService.Visible = true;
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }


        #endregion
        protected void btn_calculate_Click(object sender, EventArgs e)
        {
            try
            {
                calculate_fess();
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_assign_student");
            }
        }

        private void calculate_fess()
        {
            double ttl_hostel_charge = 0;
            try
            {
                int growcount = rd_view.Items.Count;
                int k = 0;
                for (int i = 0; i < growcount; i++)
                {
                    ViewState["statusUp"] = "0";
                    CheckBox chk = (CheckBox)rd_view.Items[i].FindControl("rowChkBox");
                    if (chk.Checked == true)
                    {
                        Label lbl_service_amount = (Label)rd_view.Items[i].FindControl("lbl_service_amount");
                        ttl_hostel_charge = ttl_hostel_charge + My.toDouble(lbl_service_amount.Text);
                    }
                    else
                    {
                        k++;
                    }
                }

            }
            catch (Exception ex)
            {
            }


            lbl_final_amt.Text = (My.toDouble(ViewState["BedCharge"].ToString()) + ttl_hostel_charge).ToString("0.00");
        }

        protected void btn_save_student_dt_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_month.SelectedItem.Text == "Select")
                {
                    Alertme("Please select from month.", "warning");
                    ddl_month.Focus();
                }
                else if (ddl_hostel.SelectedItem.Text == "Select")
                {
                    Alertme("Please select hostel.", "warning");
                    ddl_hostel.Focus();
                }
                else if (ddl_room_cat.SelectedItem.Text == "Select")
                {
                    Alertme("Please select category.", "warning");
                    ddl_room_cat.Focus();
                }
                else if (ddl_room.SelectedItem.Text == "Select")
                {
                    Alertme("Please select room.", "warning");
                    ddl_room.Focus();
                }
                else if (ddl_bed.SelectedItem.Text == "Select")
                {
                    Alertme("Please select bed.", "warning");
                    ddl_bed.Focus();
                }
                else if (0 >= My.toDouble(lbl_final_amt.Text))
                {
                    Alertme("Please calculate amount before submit data", "warning");
                    ddl_bed.Focus();
                }
                else
                {
                    save_student_hostel_info();
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_assign_student");
            }
        }

        protected string Getamount_comma_seperated(string amount)
        {
            try
            {
                string amt = String.Format("{0:n}", Convert.ToDouble(amount));
                return amt;
            }
            catch (Exception ex)
            {
                return "0";
            }
        }
        private void save_student_hostel_info()
        {
            string hostel_assign_id = "";
            int pay_month = My.toint(ddl_month.SelectedValue);
            string session_frst_year = ViewState["session"].ToString().Substring(0, 4);
            int s_year = My.toint(session_frst_year);
            if (pay_month == 01 || pay_month == 02 || pay_month == 03)
            {
                s_year = s_year + 1;
            }
            string year_month_id = s_year.ToString() + mycode.get_monthid(pay_month);

            if (ViewState["isEDIT"].ToString() == "0")
            {
                bool flag = false;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
                {
                    SqlConnection con = new SqlConnection(My.conn);
                    con.Open();
                    payments.exeSql("delete from Hostel_assign_master where Session_id='" + ViewState["sessionIDs"].ToString() + "' and Admission_no='" + ViewState["Addmission_no"].ToString() + "'", con);
                    hostel_assign_id = create_sl_no(con);
                    SqlCommand cmd;
                    string query = "INSERT INTO Hostel_assign_master (Session_id,Class_id,Admission_no,From_month_name,From_month_id,Hostel_id,Category_id,Room_id,Bed_id,Hostel_assign_id,Created_by,Created_date,Created_idate,Status,Assined_Year_Month) values (@Session_id,@Class_id,@Admission_no,@From_month_name,@From_month_id,@Hostel_id,@Category_id,@Room_id,@Bed_id,@Hostel_assign_id,@Created_by,@Created_date,@Created_idate,@Status,@Assined_Year_Month)";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Session_id", ViewState["sessionIDs"].ToString());
                    cmd.Parameters.AddWithValue("@Class_id", ViewState["class_id"].ToString());
                    cmd.Parameters.AddWithValue("@Admission_no", ViewState["Addmission_no"].ToString());
                    cmd.Parameters.AddWithValue("@From_month_name", ddl_month.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@From_month_id", ddl_month.SelectedValue);
                    cmd.Parameters.AddWithValue("@Hostel_id", ddl_hostel.SelectedValue);
                    cmd.Parameters.AddWithValue("@Category_id", ddl_room_cat.SelectedValue);
                    cmd.Parameters.AddWithValue("@Room_id", ddl_room.SelectedValue);
                    cmd.Parameters.AddWithValue("@Bed_id", ddl_bed.SelectedValue);
                    cmd.Parameters.AddWithValue("@Hostel_assign_id", hostel_assign_id);
                    cmd.Parameters.AddWithValue("@Status", 1);
                    cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                    cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
                    cmd.Parameters.AddWithValue("@Assined_Year_Month", year_month_id);
                    if (payments.InsertUpdateData(cmd, con))
                    {
                        try
                        {
                            payments.exeSql("update admission_registor set hosteltaken='Yes',transportationtaken='No',Hostel_id='" + ddl_hostel.SelectedValue + "',Hostel_assignD_id='" + hostel_assign_id + "',Transportation_Id='0',Transportationpath='0' where admissionserialnumber='" + ViewState["Addmission_no"].ToString() + "'  and Session_id='" + ViewState["sessionIDs"].ToString() + "'  ", con);
                            DataTable dtTns = payments.dataTable("select * from Student_mapping_with_TransportPath where Session_id='" + ViewState["sessionIDs"].ToString() + "' and Admission_no='" + ViewState["Addmission_no"].ToString() + "'  and Class_id='" + ViewState["class_id"].ToString() + "'", con);
                            if (dtTns.Rows.Count > 0)
                            {
                                foreach (DataRow dr in dtTns.Rows)
                                {
                                    string qry = "delete from Student_mapping_with_TransportPath where Session_id='" + ViewState["sessionIDs"].ToString() + "' and Admission_no='" + ViewState["Addmission_no"].ToString() + "' and Class_id='" + ViewState["class_id"].ToString() + "'; update Transport_Path_Mapping_With_Sheet set Sheet_Status=0 where TransportationPath_id=" + dr["TransportPath_id"].ToString() + " and Transportation_Id=" + dr["transport_id"].ToString() + " and Sheet_Id=" + dr["Sheet_Id"].ToString() + "";
                                    payments.exeSql(qry, con);

                                    try
                                    {
                                        SqlCommand cmd1;
                                        string query2 = "update Student_mapping_with_TransportPath_history set Removed_date=@Removed_date,Removed_Idate=@Removed_Idate,Update_Status=@Update_Status,Remove_Update_Month=@Remove_Update_Month,Remarks=@Remarks where Transport_Assigned_Id=@Transport_Assigned_Id and TransportPath_id=@TransportPath_id and Admission_no=@Admission_no and Session_id=@Session_id";
                                        cmd1 = new SqlCommand(query2);
                                        cmd1.Parameters.AddWithValue("@Removed_date", mycode.date());
                                        cmd1.Parameters.AddWithValue("@Removed_Idate", mycode.idate());
                                        cmd1.Parameters.AddWithValue("@Update_Status", "Transport Removed");
                                        cmd1.Parameters.AddWithValue("@Remove_Update_Month", mycode.get_current_monthname());
                                        cmd1.Parameters.AddWithValue("@Remarks", "Hostel Taken");
                                        cmd1.Parameters.AddWithValue("@TransportPath_id", dr["TransportPath_id"].ToString());
                                        cmd1.Parameters.AddWithValue("@Transport_Assigned_Id", dr["Transport_Assigned_Id"].ToString());
                                        cmd1.Parameters.AddWithValue("@Session_id", ViewState["sessionIDs"].ToString());
                                        cmd1.Parameters.AddWithValue("@Admission_no", ViewState["Addmission_no"].ToString());
                                        if (payments.InsertUpdateData(cmd1, con))
                                        {
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                    }
                                }
                            }
                        }
                        catch
                        {
                        }
                        save_add_services(hostel_assign_id, con);


                        string parameter = "HostelAdmissionFee";
                        string parameter_old = "AdmissionFee";
                        string studenttype = ViewState["StudentType"].ToString();
                        string parameter_m_old = "MonthlyFee";
                        string parameter_m_new = "HostelMonthlyFee";
                        if (studenttype.ToUpper() == "NEW")
                        {
                            parameter = "HostelAdmissionFee";
                            parameter_old = "AdmissionFee";
                        }
                        else
                        {
                            parameter = "HostelAnnualFee";
                            parameter_old = "AnnualFee";
                        }

                        payments.exeSql("update Typewise_fee_collection set parameter='" + parameter_m_new + "'  where admission_no='" + ViewState["Addmission_no"].ToString() + "' and session='" + ViewState["session"].ToString() + "' and parameter='" + parameter_m_old + "'; update Typewise_fee_collection set parameter='" + parameter + "' where admission_no='" + ViewState["Addmission_no"].ToString() + "' and session='" + ViewState["session"].ToString() + "' and parameter='" + parameter_old + "' ", con);
                    }

                    dues_update_headwise_transaction.update_student_dues(ViewState["sessionIDs"].ToString(), ViewState["classid"].ToString(), ViewState["admSionNo"].ToString(), "0", "0", con);

                    flag = true;
                    con.Close();
                    scope.Complete();
                }

                if (flag == true)
                {
                    Session["msGS"] = "The hostel room assignment for student has been added successfully.";
                    empty_form();
                }
            }
            else
            {
                bool flag = false;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
                {
                    SqlConnection con = new SqlConnection(My.conn);
                    con.Open();
                    SqlCommand cmd;
                    string query = "Update Hostel_assign_master set From_month_name=@From_month_name,From_month_id=@From_month_id,Hostel_id=@Hostel_id,Category_id=@Category_id,Room_id=@Room_id,Bed_id=@Bed_id,Updated_by=@Updated_by,Updated_date=@Updated_date,Updated_idate=@Updated_idate,Assined_Year_Month=@Assined_Year_Month where Hostel_assign_id = '" + ViewState["AssignID"].ToString() + "'";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@From_month_name", ddl_month.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@From_month_id", ddl_month.SelectedValue);
                    cmd.Parameters.AddWithValue("@Hostel_id", ddl_hostel.SelectedValue);
                    cmd.Parameters.AddWithValue("@Category_id", ddl_room_cat.SelectedValue);
                    cmd.Parameters.AddWithValue("@Room_id", ddl_room.SelectedValue);
                    cmd.Parameters.AddWithValue("@Bed_id", ddl_bed.SelectedValue);
                    cmd.Parameters.AddWithValue("@Hostel_assign_id", hostel_assign_id);
                    cmd.Parameters.AddWithValue("@Updated_by", ViewState["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@Updated_date", mycode.date());
                    cmd.Parameters.AddWithValue("@Updated_idate", mycode.idate());
                    cmd.Parameters.AddWithValue("@Assined_Year_Month", year_month_id);
                    if (payments.InsertUpdateData(cmd, con))
                    {
                        try
                        {
                            payments.exeSql("update admission_registor set hosteltaken='Yes',transportationtaken='No',Hostel_id='" + ddl_hostel.SelectedValue + "' where admissionserialnumber='" + ViewState["Addmission_no"].ToString() + "'   and Session_id='" + ViewState["sessionIDs"].ToString() + "' ", con);
                        }
                        catch
                        {
                        }
                        string qry = "delete from Hostel_assigned_additional_service where Assign_id='" + ViewState["AssignID"].ToString() + "'";
                        payments.exeSql(qry, con);
                        save_add_services(ViewState["AssignID"].ToString(), con);
                    }

                    dues_update_headwise_transaction.update_student_dues(ViewState["sessionIDs"].ToString(), ViewState["classid"].ToString(), ViewState["admSionNo"].ToString(), "0", "0", con);

                    flag = true;
                    con.Close();
                    scope.Complete();
                }

                if (flag == true)
                {
                    Session["msGS"] = "Hostel room assign with student has been updated successfully.";
                    empty_form();
                }
            }
        }
        private void save_add_services(string hostel_assign_id, SqlConnection con)
        {
            int growcount = rd_view.Items.Count;
            int k = 0;
            for (int i = 0; i < growcount; i++)
            {
                ViewState["statusUp"] = "0";
                CheckBox chk = (CheckBox)rd_view.Items[i].FindControl("rowChkBox");
                if (chk.Checked == true)
                {
                    Label lbl_service_id = (Label)rd_view.Items[i].FindControl("lbl_service_id");
                    if (payments.IsUserExistS("select Assign_id from Hostel_assigned_additional_service where Assign_id='" + hostel_assign_id + "' and Service_id='" + lbl_service_id.Text + "'", con))
                    {
                        SqlCommand cmd;
                        string query = "INSERT INTO Hostel_assigned_additional_service (Assign_id,Service_id,Created_by,Created_date,Created_idate) values (@Assign_id,@Service_id,@Created_by,@Created_date,@Created_idate)";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@Assign_id", hostel_assign_id);
                        cmd.Parameters.AddWithValue("@Service_id", lbl_service_id.Text);
                        cmd.Parameters.AddWithValue("@Created_by", Session["Admin"].ToString());
                        cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                        cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
                        if (payments.InsertUpdateData(cmd, con))
                        {
                        }
                    }
                }
                else
                {
                    k++;
                }
            }
        }
        private void empty_form()
        {
            Response.Redirect("Hostel_Assign_to_student.aspx", false);
            //My.ClearInputs(Page.Controls);
            //additionalService.Visible = false;
            //std_basic_infoS.Visible = false;
        }
        private string create_sl_no(SqlConnection con)
        {
            bool duplicate = true;
            string hostel_assign_id = payments.auto_serialS("Hostel_assign_id", con);
            while (duplicate)
            {
                DataTable cdt = payments.dataTable("select Hostel_assign_id from Hostel_assign_master where Hostel_assign_id='" + hostel_assign_id + "'", con);
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    duplicate = false;
                }
                else
                {
                    hostel_assign_id = payments.auto_serialS("Hostel_assign_id", con);
                }
            }
            return hostel_assign_id;
        }
        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (((Label)e.Item.FindControl("lbl_is_assined")).Text == "1")
                {
                    ((CheckBox)e.Item.FindControl("rowChkBox")).Checked = true;
                }
                else
                {
                    ((CheckBox)e.Item.FindControl("rowChkBox")).Checked = false;
                }
            }
        }
    }
}

