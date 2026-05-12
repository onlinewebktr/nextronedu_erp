using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class Set_Start_Month_Special_Case_for_Student : System.Web.UI.Page
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

                        string pagename_current = "Set_Start_Month_Special_Case_for_Student.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];


                        mycode.bind_all_ddl_with_id(ddl_session_name, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int)");
                        ddl_session_name.SelectedValue = My.get_session_id();

                        mycode.bind_all_ddl_with_id(ddl_session_class, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int)");
                        ddl_session_class.SelectedValue = My.get_session_id();

                        mycode.bind_all_ddl_with_id(ddl_class, "Select   cd.Course_Name,cd.course_id from Add_course_table cd  order by cd.Position asc");


                        ViewState["Branch_id"] = mycode.get_branch_id(ViewState["Userid"].ToString());

                        mycode.bind_all_ddl_with_id(ddl_monthname, "select Month,Position from Month_Index order by Position asc");
                        mycode.bind_all_ddl_with_id(ddl_month_name_class_wise, "select Month,Position from Month_Index order by Position asc");

                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Set_Start_Month_Special_Case_for_Student");
            }
        }

        string scrpt;

        public int Update_month_type_wise_fee_class { get; private set; }

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

        #region find student data
        protected void btn_find_admission_no_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_add"].ToString() == "1")
                {
                    find_data();
                }
                else if (ViewState["Is_Edit"].ToString() == "1")
                {
                    find_data();
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

        private void find_data()
        {
            if (ddl_session_name.SelectedValue == "Select")
            {
                Alertme("Please select session", "warning");
            }
            else if (txt_admission_no.Text == "")
            {
                Alertme("Please enter  current admission no.", "warning");
            }
            else
            {
                string query = "select * from admission_registor where admissionserialnumber='" + txt_admission_no.Text + "' and Session_id='" + ddl_session_name.SelectedValue + "' and StudentStatus='AV'  and  Status='1'    ";
                find_details(query);
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
                pnl_class_wise_student.Visible = false;
                std_basic_infoS.Visible = false;
                Alertme("Student details not found...", "warning");
                return;
            }
            else
            { 
                foreach (DataRow dr in dt.Rows)
                {
                    std_basic_infoS.Visible = true; 
                    lbl_name.Text = dr["studentname"].ToString();
                    lbl_father_name.Text = dr["fathername"].ToString();
                    lblclass.Text = dr["class"].ToString();
                    ViewState["class_id"] = dr["Class_id"].ToString();
                    txtsection.Text = dr["Section"].ToString();
                    lbl_admission_no.Text = dr["admissionserialnumber"].ToString();
                    txt_admission_no.Text = dr["admissionserialnumber"].ToString();
                    lblhostel.Text = dr["hosteltaken"].ToString();
                    ViewState["parameter"] = dr["hosteltaken"].ToString().ToLower() == "yes" ? "HostelMonthlyFee" : "MonthlyFee";
                    ViewState["parameter_id"] = dr["hosteltaken"].ToString().ToLower() == "yes" ? "3" : "4";
                    lbltransporttion.Text = dr["transportationtaken"].ToString();
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
                    ViewState["admissionserialnumber"] = dr["admissionserialnumber"].ToString();
                    ViewState["session"] = dr["session"].ToString();
                    ViewState["id"] = dr["id"].ToString();
                    lbl_old_roll_no.Text = dr["rollnumber"].ToString();

                    bool check_monthlympaymnet = GetDataItem_monthly_payment_taken_yes_not()
;
                    if (check_monthlympaymnet == true)
                    {
                        std_basic_infoS.Visible = true;
                    }
                    else
                    {
                        Alertme("This student already taken monthly fees so you can not update month", "warning");
                        std_basic_infoS.Visible = false;
                    }
                }

            }
        }

        private bool GetDataItem_monthly_payment_taken_yes_not()
        {
            return true;

            //DataTable dt = mycode.FillData("Select * from Typewise_fee_collection where admission_no='" + ViewState["admissionserialnumber"].ToString() + "' and session='" + ddl_session_name.SelectedItem.Text + "' and parameter='MonthlyFee'");
            //if (dt.Rows.Count == 0)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
        }


        #endregion

        protected void btn_update_date_Click(object sender, EventArgs e)
        {
            if (ddl_session_name.SelectedValue == "Select")
            {
                Alertme("Please select session", "warning");
            }
            else if (txt_admission_no.Text == "")
            {
                Alertme("Please enter admission no.", "warning");
            }
            else if (txt_remarks.Text == "")
            {
                Alertme("Please enter remarks", "warning");
            }
            else
            {
                DataTable dt = mycode.FillData(" select *  from dbo.[Month_Index] where Position<" + ddl_monthname.SelectedValue + " order by Position");
                if (dt.Rows.Count == 0)
                {

                }
                else
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string Month_Id = dt.Rows[i]["Month_Id"].ToString();
                        string Month = dt.Rows[i]["Month"].ToString();
                        string Position = dt.Rows[i]["Position"].ToString();

                        Update_mont_type(Month_Id, Month, Position);
                    }
                    My.Insert("admission_registor_Change_admission_no_history", new
                    {
                        Old_admission_no = lbl_admission_no.Text,
                        Current_admission_no = lbl_admission_no.Text,
                        Session_id = ViewState["sessionIDs"].ToString(),
                        Created_By = ViewState["Userid"].ToString(),
                        Date_time = My.getdate1(),
                        Change_type = "Skip Month",
                        Class_Id_New = ViewState["class_id"].ToString(),
                        Old_Class_id = ViewState["class_id"].ToString(),
                        Reason = txt_remarks.Text,
                        Remark = txt_remarks.Text,
                        Month_id = ddl_monthname.SelectedValue,
                    });



                }
                Alertme("Your selected month has been updated, You can take fee from month=" + ddl_monthname.SelectedItem.Text, "success");
            }
        }

        private void Update_mont_type(string month_Id, string month, string Position)
        {

            string query = "Select * from Typewise_fee_collection where admission_no='" + ViewState["admissionserialnumber"].ToString() + "' and parameter='" + ViewState["parameter"].ToString() + "' and session='" + ViewState["session"].ToString() + "' and month='" + month + "'";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                SqlCommand cmd;
                string query2 = "INSERT INTO Typewise_fee_collection (admission_no,class,session,section,parameter,Date,idate,feetype,payable,paid,dues,status,month,user_id,content_id,transection,Ledger,is_readyfor_sync,is_sync,is_sync_dues_diary,Previous_admission_no,group_id,class_id,position,Disc,Payable_after_disc,delete_sync,branchid,Acamedic_Semester_Id,Is_month_skip) values (@admission_no,@class,@session,@section,@parameter,@Date,@idate,@feetype,@payable,@paid,@dues,@status,@month,@user_id,@content_id,@transection,@Ledger,@is_readyfor_sync,@is_sync,@is_sync_dues_diary,@Previous_admission_no,@group_id,@class_id,@position,@Disc,@Payable_after_disc,@delete_sync,@branchid,@Acamedic_Semester_Id,@Is_month_skip)";
                cmd = new SqlCommand(query2);
                cmd.Parameters.AddWithValue("@admission_no", ViewState["admissionserialnumber"].ToString());
                cmd.Parameters.AddWithValue("@class", lblclass.Text);
                cmd.Parameters.AddWithValue("@session", ViewState["session"].ToString());
                cmd.Parameters.AddWithValue("@section", ViewState["Section"].ToString());
                cmd.Parameters.AddWithValue("@parameter", ViewState["parameter"].ToString());
                cmd.Parameters.AddWithValue("@Date", mycode.date());
                cmd.Parameters.AddWithValue("@idate", mycode.idate());
                cmd.Parameters.AddWithValue("@feetype", "");
                cmd.Parameters.AddWithValue("@payable", "00.00");
                cmd.Parameters.AddWithValue("@paid", "0.00");
                cmd.Parameters.AddWithValue("@dues", "0.00");
                cmd.Parameters.AddWithValue("@status", "Paid");
                cmd.Parameters.AddWithValue("@month", month);
                cmd.Parameters.AddWithValue("@user_id", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@content_id", "0");
                cmd.Parameters.AddWithValue("@transection", "0");
                cmd.Parameters.AddWithValue("@Ledger", "School");
                cmd.Parameters.AddWithValue("@is_readyfor_sync", 0);
                cmd.Parameters.AddWithValue("@is_sync", 0);
                cmd.Parameters.AddWithValue("@is_sync_dues_diary", 0);
                cmd.Parameters.AddWithValue("@Previous_admission_no", 0);
                cmd.Parameters.AddWithValue("@group_id", "3");
                cmd.Parameters.AddWithValue("@class_id", ViewState["class_id"].ToString());
                cmd.Parameters.AddWithValue("@position", Position);
                cmd.Parameters.AddWithValue("@Disc", "0");
                cmd.Parameters.AddWithValue("@Payable_after_disc", "0");
                cmd.Parameters.AddWithValue("@delete_sync", "0");
                cmd.Parameters.AddWithValue("@branchid", ViewState["Branch_id"].ToString());
                cmd.Parameters.AddWithValue("@Acamedic_Semester_Id", "0");
                cmd.Parameters.AddWithValue("@Is_month_skip", 1);

                if (My.InsertUpdateData(cmd))
                {

                }
            }
            else
            {
            }

        }

        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_class.SelectedItem.Text == "Select")
            {
                Alertme("Please select class", "warning");
            }
            else
            {
                mycode.bind_ddlall(ddl_section, "select distinct Section from admission_registor where Class_id='" + ddl_class.SelectedValue + "' order by Section asc");
            }

        }

        #region btn find class wise student
        protected void btn_find_classwise_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_session_class.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session", "warning");
                }
                else if (ddl_class.SelectedItem.Text == "Select")
                {
                    Alertme("Please select class", "warning");
                }
                if (ddl_section.Text == "Select")
                {
                    Alertme("Please select section", "warning");
                }
                else
                {
                    if (ViewState["Is_add"].ToString() == "1")
                    {
                        btn_find_by_class_section();
                    }
                    else if (ViewState["Is_Edit"].ToString() == "1")
                    {
                        btn_find_by_class_section();
                    }
                    else
                    {
                        Alertme(My.get_restricted_message(), "warning");
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "skip month class wise ");

            }

        }

        private void btn_find_by_class_section()
        {

            string query = "";
            if (ddl_section.Text == "ALL")
            {
                query = "select session,admissionserialnumber,class,Section,rollnumber,studentname,Status,id,Session_id,hosteltaken,Class_id from admission_registor where (Transfer_Status='New' or Transfer_Status='NT') and StudentStatus='AV' and Session_id=" + ddl_session_class.SelectedValue + " and Class_id=" + ddl_class.SelectedValue + "  order by rollnumber asc";
            }
            else
            {
                query = "select session,admissionserialnumber,class,Section,rollnumber,studentname,Status,id,Session_id,hosteltaken,Class_id from admission_registor where (Transfer_Status='New' or Transfer_Status='NT') and StudentStatus='AV' and Session_id=" + ddl_session_class.SelectedValue + " and Class_id=" + ddl_class.SelectedValue + " and Section='" + ddl_section.SelectedValue + "'  order by rollnumber asc";
            }

            ViewState["query"] = query;
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data list exist", "warning");
                grid_data_student_list.DataSource = null;
                grid_data_student_list.DataBind();

                pnl_class_wise_student.Visible = false;
                std_basic_infoS.Visible = false;
            }
            else
            { 
                std_basic_infoS.Visible = false;
                pnl_class_wise_student.Visible = true;
                grid_data_student_list.DataSource = dt;
                grid_data_student_list.DataBind();
            } 
        }

        protected void btn_class_wise_month_skip_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_month_name_class_wise.SelectedItem.Text == "Select")
                {
                    Alertme("Please select month name", "warning");
                } 
                else
                { 
                    int growcount = grid_data_student_list.Rows.Count;
                    int k = 0;
                    for (int i = 0; i < growcount; i++)
                    {
                        CheckBox chk = (CheckBox)grid_data_student_list.Rows[i].FindControl("rowChkBox");
                        if (chk.Checked == true)
                        {

                            Label lbl_admissionserialnumber = (Label)grid_data_student_list.Rows[i].FindControl("lbl_admissionserialnumber");
                            Label lbl_session_id = (Label)grid_data_student_list.Rows[i].FindControl("lbl_session_id");
                            Label lbl_hosteltaken = (Label)grid_data_student_list.Rows[i].FindControl("lbl_hosteltaken");
                            Label lbl_session = (Label)grid_data_student_list.Rows[i].FindControl("lbl_session");
                            Label lbl_class = (Label)grid_data_student_list.Rows[i].FindControl("lbl_class");
                            Label lbl_section = (Label)grid_data_student_list.Rows[i].FindControl("lbl_section");
                            Label lbl_class_id = (Label)grid_data_student_list.Rows[i].FindControl("lbl_class_id");

                            DataTable dt = mycode.FillData(" select *  from dbo.[Month_Index] where Position<" + ddl_month_name_class_wise.SelectedValue + " order by Position");
                            if (dt.Rows.Count == 0)
                            {

                            }
                            else
                            {
                                for (int j = 0; j < dt.Rows.Count; j++)
                                {
                                    string Month_Id = dt.Rows[j]["Month_Id"].ToString();
                                    string Month = dt.Rows[j]["Month"].ToString();
                                    string Position = dt.Rows[j]["Position"].ToString();

                                    Update_month_type_wise_fee_class_wise(Month_Id, Month, Position, lbl_session_id.Text, lbl_admissionserialnumber.Text, lbl_hosteltaken.Text, lbl_session.Text, lbl_class.Text, lbl_section.Text, lbl_class_id.Text);
                                }



                            }
                        }
                        else
                        {
                            k++;
                        }
                    }
                    if (k == growcount)
                    {
                        Alertme("Please select at least one student from student list", "warning");
                    }
                    else
                    {

                        Alertme("Your selected month has been updated, You can take fee from month=" + ddl_month_name_class_wise.SelectedItem.Text, "success");
                        btn_find_by_class_section();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void Update_month_type_wise_fee_class_wise(string month_Id, string month, string Position, string session_id, string admissionserialnumber, string hosteltaken, string session, string classname, string section, string class_id)
        {
            ViewState["parameter"] = hosteltaken.ToLower() == "yes" ? "HostelMonthlyFee" : "MonthlyFee";
            string query = "Select * from Typewise_fee_collection where admission_no='" + admissionserialnumber + "' and parameter='" + ViewState["parameter"].ToString() + "' and session='" + session + "' and month='" + month + "'";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                SqlCommand cmd;
                string query2 = "INSERT INTO Typewise_fee_collection (admission_no,class,session,section,parameter,Date,idate,feetype,payable,paid,dues,status,month,user_id,content_id,transection,Ledger,is_readyfor_sync,is_sync,is_sync_dues_diary,Previous_admission_no,group_id,class_id,position,Disc,Payable_after_disc,delete_sync,branchid,Acamedic_Semester_Id,Is_month_skip) values (@admission_no,@class,@session,@section,@parameter,@Date,@idate,@feetype,@payable,@paid,@dues,@status,@month,@user_id,@content_id,@transection,@Ledger,@is_readyfor_sync,@is_sync,@is_sync_dues_diary,@Previous_admission_no,@group_id,@class_id,@position,@Disc,@Payable_after_disc,@delete_sync,@branchid,@Acamedic_Semester_Id,@Is_month_skip)";
                cmd = new SqlCommand(query2);
                cmd.Parameters.AddWithValue("@admission_no", admissionserialnumber);
                cmd.Parameters.AddWithValue("@class", classname);
                cmd.Parameters.AddWithValue("@session", session);
                cmd.Parameters.AddWithValue("@section", section);
                cmd.Parameters.AddWithValue("@parameter", ViewState["parameter"].ToString());
                cmd.Parameters.AddWithValue("@Date", mycode.date());
                cmd.Parameters.AddWithValue("@idate", mycode.idate());
                cmd.Parameters.AddWithValue("@feetype", "");
                cmd.Parameters.AddWithValue("@payable", "00.00");
                cmd.Parameters.AddWithValue("@paid", "0.00");
                cmd.Parameters.AddWithValue("@dues", "0.00");
                cmd.Parameters.AddWithValue("@status", "Paid");
                cmd.Parameters.AddWithValue("@month", month);
                cmd.Parameters.AddWithValue("@user_id", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@content_id", "0");
                cmd.Parameters.AddWithValue("@transection", "0");
                cmd.Parameters.AddWithValue("@Ledger", "School");
                cmd.Parameters.AddWithValue("@is_readyfor_sync", 0);
                cmd.Parameters.AddWithValue("@is_sync", 0);
                cmd.Parameters.AddWithValue("@is_sync_dues_diary", 0);
                cmd.Parameters.AddWithValue("@Previous_admission_no", 0);
                cmd.Parameters.AddWithValue("@group_id", "3");
                cmd.Parameters.AddWithValue("@class_id", class_id);
                cmd.Parameters.AddWithValue("@position", Position);
                cmd.Parameters.AddWithValue("@Disc", "0");
                cmd.Parameters.AddWithValue("@Payable_after_disc", "0");
                cmd.Parameters.AddWithValue("@delete_sync", "0");
                cmd.Parameters.AddWithValue("@branchid", ViewState["Branch_id"].ToString());
                cmd.Parameters.AddWithValue("@Acamedic_Semester_Id", "0");
                cmd.Parameters.AddWithValue("@Is_month_skip", 1);
                if (My.InsertUpdateData(cmd))
                {

                }
            }
            else
            {
            }


        }
        #endregion


    }
}