using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Web.Services;
using System.Data;
namespace school_web.Admin
{
    public partial class Mapping_Transportation_with_Student : System.Web.UI.Page
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

                        string pagename_current = "Mapping_Transportation-with-Student.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        ViewState["branch_id"] = mycode.get_branch_id(Session["Admin"].ToString());
                        mycode.bind_all_ddl_with_id(ddlclass, "Select Course_Name,course_id from Add_course_table order by Position asc");
                        mycode.bind_all_ddl_with_id(ddlsession, "Select Session,session_id from session_details");
                        mycode.bind_all_ddl_with_id(ddl_session_student, "Select Session,session_id from session_details");
                        mycode.bind_all_ddl_with_id(ddlsessionad, "Select Session,session_id from session_details");
                        ddlsessionad.SelectedValue = My.get_session_id();
                        ddlsession.SelectedValue = My.get_session_id();
                        ddl_session_student.SelectedValue = My.get_session_id();
                        mycode.bind_all_ddl_with_id(ddl_from_month, "select Month,Month_Id from Month_Index order by Position asc");
                        mycode.bind_all_ddl_with_id(ddl_path_root, " select  Pathname,TransportationPath_id from  TransportationPath");


                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Mapping_Transportation_with_Student");
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
        public static List<string> GetRooPath(string PathRooT, string Session_id)
        {
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(My.conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct studentname from admission_registor where studentname LIKE '%'+@SearchMobNo+'%' and Session_id='" + Session_id + "' and  Transfer_Status in ('NT','New' ) and  Status='1'";
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
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(My.conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct admissionserialnumber from admission_registor where admissionserialnumber LIKE '%'+@SearchMobNo+'%' and Session_id='" + Session_id + "' and  Transfer_Status in ('NT','New' ) and  Status='1'";
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

        #region FinD StudenT
        protected void btn_find_admission_no_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_add"].ToString() == "1")
                {
                    string query = "select * from admission_registor where admissionserialnumber='" + txt_admission_no.Text + "' and Session='" + ddlsessionad.SelectedItem.Text + "' and StudentStatus='AV'   and Is_TC_Taken!='true' and   Transfer_Status in ('NT','New' ) and  Status='1' order by id asc";
                    find_details(query);
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }

                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    string query = "select * from admission_registor where admissionserialnumber='" + txt_admission_no.Text + "' and Session='" + ddlsessionad.SelectedItem.Text + "' and StudentStatus='AV'   and Is_TC_Taken!='true' and   Transfer_Status in ('NT','New' ) and  Status='1' order by id asc";
                    find_details(query);
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");

                }


                //empty_form();

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
                    string queryS = "select * from Student_mapping_with_TransportPath where Session_id='" + dr["Session_id"].ToString() + "' and Admission_no='" + dr["admissionserialnumber"].ToString() + "' and Class_id='" + dr["Class_id"].ToString() + "' and branch_id='" + ViewState["branch_id"].ToString() + "'";
                    DataTable dts = My.dataTable(queryS);
                    if (dts.Rows.Count != 0)
                    {
                        Alertme("Selected student already mapped  with transport ", "warning");
                        return;
                    }


                    std_basic_infoS.Visible = true;
                    txt_student_name.Text = dr["studentname"].ToString();
                    ddlclass.SelectedValue = dr["Class_id"].ToString();
                    ddlsession.SelectedValue = dr["Session_id"].ToString();
                    txt_section.Text = dr["Section"].ToString();
                    txtrollnumber.Text = dr["rollnumber"].ToString();
                    lbl_name.Text = dr["studentname"].ToString();
                    lbl_father_name.Text = dr["fathername"].ToString();
                    lblclass.Text = dr["class"].ToString();
                    ViewState["class_id"] = dr["Class_id"].ToString();
                    txtsection.Text = dr["Section"].ToString();
                    lbl_admission_no.Text = dr["admissionserialnumber"].ToString();
                    txt_admission_no.Text = dr["admissionserialnumber"].ToString();
                    lblhostel.Text = dr["hosteltaken"].ToString();
                    ViewState["parameter"] = dr["hosteltaken"].ToString().ToUpper() == "YES" ? "HostelMonthlyFee" : "MonthlyFee";
                    ViewState["parameter_id"] = dr["hosteltaken"].ToString().ToUpper() == "YES" ? "3" : "4";
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
                    ViewState["admSionNo"] = dr["admissionserialnumber"].ToString();
                    ViewState["session"] = dr["session"].ToString();
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

                    Bind_last_monthpayment();



                }
            }
        }

        private void Bind_last_monthpayment()
        {
            string queryS = "select * from Typewise_fee_collection where session='" + ViewState["session"].ToString() + "' and Admission_no='" + ViewState["admSionNo"].ToString() + "'  ";
            DataTable dts = My.dataTable(queryS);
            if (dts.Rows.Count == 0)
            {

            }
            else
            {

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
                if (txt_section.Text == "")
                {
                    Alertme("Please enter section", "warning");
                    txt_section.Focus();
                    return;
                }
                if (txtrollnumber.Text == "")
                {
                    Alertme("Please enter roll no", "warning");
                    txtrollnumber.Focus();
                    return;
                }

                string query = "select * from admission_registor where class='" + ddlclass.SelectedItem.Text + "' and Session='" + ddlsession.SelectedItem.Text + "' and section='" + txt_section.Text + "' and rollnumber='" + txtrollnumber.Text + "' and StudentStatus='AV'    and Is_TC_Taken!='true' and    Transfer_Status in ('NT','New' ) and  Status='1' order by id asc";

                if (ViewState["Is_add"].ToString() == "1")
                {
                    find_details(query);
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }

                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    find_details(query);
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

                if (ViewState["Is_add"].ToString() == "1")
                {
                    string query = "select * from admission_registor where studentname like '%" + txt_student_name.Text + "%' and session='" + ddl_session_student.SelectedItem.Text + "' and StudentStatus='AV'    and Is_TC_Taken!='true' and Transfer_Status in ('NT','New' ) and Status='1' order by id asc";

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
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }

                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    string query = "select * from admission_registor where studentname like '%" + txt_student_name.Text + "%' and session='" + ddl_session_student.SelectedItem.Text + "' and StudentStatus='AV'    and Is_TC_Taken!='true' and Transfer_Status in ('NT','New' ) and Status='1' order by id asc";

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


        protected void btn_save_bording_Click(object sender, EventArgs e)
        {
            //try
            //{
            if (ddl_from_month.SelectedItem.Text == "Select")
            {
                Alertme("Please select month.", "warning");
                ddl_from_month.Focus();
            }
            else if (ddl_path_root.SelectedItem.Text == "Please select transportation")
            {
                Alertme("Please select month.", "warning");
                ddl_from_month.Focus();
            }
            else
            {
                //string query = "select * from (select studentname,admissionserialnumber,class,section,rollnumber,session,isnull((select sum(cast(dues as float)) from dbo.[typewise_fee_collection] where admission_no=admission_registor.admissionserialnumber and session=admission_registor.session and month='" + ddl_from_month.SelectedItem.Text + "' and parameter='MonthlyFee' ),'0') as dues from dbo.[admission_registor]  where class_id='" + ViewState["classid"].ToString() + "' and session_id='" + ViewState["sessionIDs"].ToString() + "' and admissionserialnumber='" + ViewState["admSionNo"].ToString() + "') t";
                //DataTable dt = My.dataTable(query);
                //if (dt.Rows.Count != 0)
                //{
                //    if (My.toDouble(dt.Rows[0]["dues"].ToString()) > 0)
                //    {
                //        Alertme("can't allocate transportaion with selected month due to dues. selected month", "warning");
                //    }
                //    else
                //    {
                save_boarding_data();
                //    }
                //}
            }
            //}
            //catch (Exception ex)
            //{
            //}
        }

        private void save_boarding_data()
        {

            bool check_transportassined = chek_assined_or_not(ViewState["sessionIDs"].ToString(), ViewState["admSionNo"].ToString());
            if (check_transportassined == true)
            {


                string tranportassinedid = My.get_transport_assigned_id();


                string session = mycode.get_session(ViewState["sessionIDs"].ToString());
                bool chek_selected_monthpaid = My.get_selected_monthpaid(ViewState["admSionNo"].ToString(), session, ddl_from_month.SelectedItem.Text);

                if (chek_selected_monthpaid == true)
                {

                    string cunrt_session = My.get_session();
                    string session_frst_year = cunrt_session.Substring(0, 4);
                    int session_s_year = My.toint(session_frst_year);
                    int s_year = My.toint(session_frst_year);

                    string monthid = My.tomonth_numberstring(ddl_from_month.SelectedItem.Text);

                    int pay_month = My.toint(monthid);
                    s_year = My.check_start_months(pay_month, s_year);

                    string final = s_year + monthid;


                    SqlCommand cmd;
                    string query = "INSERT INTO Student_mapping_with_TransportPath (Session_id,Admission_no,Class_id,Month_name,Month_id,TransportPath_id,Created_by,Created_date,Created_idate,branch_id,Year_month,Transport_Assigned_Id) values (@Session_id,@Admission_no,@Class_id,@Month_name,@Month_id,@TransportPath_id,@Created_by,@Created_date,@Created_idate,@branch_id,@Year_month,@Transport_Assigned_Id); INSERT INTO Student_mapping_with_TransportPath_history (Session_id,Admission_no,Class_id,Month_name,Month_id,TransportPath_id,Remark,Created_by,Created_date,Created_idate,branch_id,Year_month,Transport_Assigned_Id,Update_Status) values (@Session_id,@Admission_no,@Class_id,@Month_name,@Month_id,@TransportPath_id,@Remark,@Created_by,@Created_date,@Created_idate,@branch_id,@Year_month,@Transport_Assigned_Id,@Update_Status)";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Session_id", ViewState["sessionIDs"].ToString());
                    cmd.Parameters.AddWithValue("@Class_id", ViewState["classid"].ToString());
                    cmd.Parameters.AddWithValue("@Admission_no", ViewState["admSionNo"].ToString());
                    cmd.Parameters.AddWithValue("@Month_name", ddl_from_month.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Month_id", ddl_from_month.SelectedValue);
                    cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                    cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
                    cmd.Parameters.AddWithValue("@Remark", "Transport assigned");
                    cmd.Parameters.AddWithValue("@branch_id", ViewState["branch_id"].ToString());
                    cmd.Parameters.AddWithValue("@TransportPath_id", ddl_path_root.SelectedValue);
                    cmd.Parameters.AddWithValue("@Year_month", final);
                    cmd.Parameters.AddWithValue("@Transport_Assigned_Id", tranportassinedid);
                    cmd.Parameters.AddWithValue("@Update_Status", "Assigned");

                    if (My.InsertUpdateData(cmd))
                    {
                        try
                        {
                            SqlCommand cmd1;
                            string query1 = "Update admission_registor set Transportation_Id=@Transportation_Id,Transportationpath=@Transportationpath,transportationtaken=@transportationtaken,hosteltaken=@hosteltaken where admissionserialnumber=@admissionserialnumber and Session_id=@Session_id ";
                            cmd1 = new SqlCommand(query1);
                            cmd1.Parameters.AddWithValue("@Transportation_Id", ddl_path_root.SelectedValue);
                            cmd1.Parameters.AddWithValue("@Transportationpath", ddl_path_root.SelectedItem.Text);
                            cmd1.Parameters.AddWithValue("@transportationtaken", "Yes");
                            cmd1.Parameters.AddWithValue("@hosteltaken", "No");
                            cmd1.Parameters.AddWithValue("@Session_id", ViewState["sessionIDs"].ToString());
                            cmd1.Parameters.AddWithValue("@admissionserialnumber", ViewState["admSionNo"].ToString());

                            if (My.InsertUpdateData(cmd1))
                            {
                                Alertme("This Student has been assigned with Transport successfully.", "success");



                                // chek if dues

                                string sessionname = mycode.get_session(ViewState["sessionIDs"].ToString());
                                DataTable dt = mycode.FillData("Select top 1 * from Typewise_fee_collection where admission_no='" + ViewState["admSionNo"].ToString() + "' and session='" + sessionname + "'    and parameter ='MonthlyFee'  and status='Dues' and   month='" + ddl_from_month.SelectedItem.Text + "' ");//
                                if (dt.Rows.Count != 0)
                                {
                                    string class1 = dt.Rows[0]["class"].ToString();
                                    string section = dt.Rows[0]["section"].ToString();
                                    string feetype = "TransportationFee";
                                    string month = ddl_from_month.SelectedItem.Text;
                                    string user_id = ViewState["Userid"].ToString();
                                    string group_id = "3";
                                    string position = dt.Rows[0]["position"].ToString();
                                    string class_id = dt.Rows[0]["class_id"].ToString();
                                    string branchid = ViewState["branch_id"].ToString();
                                    DataTable feedt = mycode.FillData(@"select 'TransportationFee' as content,tr.parameter_id as content_id,tr.Amount as amount,'Monthelyfee',tr.Ledger,
                
                
                (isnull((select top 1 disc_amount from dbo.[Discount_Master_for_bus] where admission_no='" + ViewState["admSionNo"].ToString() + "' and month='" + ddl_from_month.SelectedItem.Text + "' and session_id='" + ViewState["sessionIDs"].ToString() + "' and Bus_path='" + ddl_path_root.SelectedValue + "'),(select top 1 disc_amount from dbo.[Discount_Master_for_bus] where  admission_no='All' and month='" + ddl_from_month.SelectedItem.Text + "' and session_id='" + ViewState["sessionIDs"].ToString() + "' and Bus_path='" + ddl_path_root.SelectedValue + "'))) disc_amount from admission_registor ar join Transportation_Fee_Master tr on ar.Transportation_Id=tr.Transportation_path_id and ar.transportationtaken='yes' and ar.Session_id='" + ViewState["sessionIDs"].ToString() + "' and admissionserialnumber='" + ViewState["admSionNo"].ToString() + "' and tr.Month='" + ddl_from_month.SelectedItem.Text + "'  join Student_mapping_with_TransportPath smt on  ar.Session_id=smt.Session_id and ar.admissionserialnumber=smt.Admission_no and smt.Year_month<=" + s_year + monthid + "");


                                    if (feedt.Rows.Count != 0)
                                    {
                                        foreach (DataRow dr in feedt.Rows)
                                        {

                                            string TransportationFeetype = dr["content"].ToString();

                                            string content_id1 = dr["content_id"].ToString();
                                            string Amount = dr["amount"].ToString();
                                            string disc_amount = dr["disc_amount"].ToString();

                                            double afterdicount = My.toDouble(Amount) - My.toDouble(disc_amount);

                                            string get_onemonthback = mycode.get_one_monthback(monthid);

                                            mycode.executequery("delete from Typewise_fee_collection where admission_no='" + ViewState["admSionNo"].ToString() + "' and session='" + sessionname + "' and parameter='MonthlyFee' and feetype='TransportationFee' and month='" + get_onemonthback + "'");
                                            My.exeSql(@"insert into Typewise_fee_collection(admission_no,class,session,parameter,Date,idate,feetype,payable,paid,dues,status,month,content_id,transection,
Ledger,is_readyfor_sync,is_sync,is_sync_dues_diary,Disc,section,user_id,branchid,group_id,class_id,position,Payable_after_disc) 
values ('" + ViewState["admSionNo"].ToString() + "','" + class1 + "','" + sessionname + "','MonthlyFee','" + mycode.date() + "','" + mycode.idate() + "','" + TransportationFeetype + "','" + My.toDouble(Amount).ToString("0.00") + "','0','" + My.toDouble(afterdicount).ToString("0.00") + "','Dues','" + ddl_from_month.SelectedItem.Text + "','" + content_id1 + "','','School','false','false','false','" + My.toDouble(disc_amount).ToString("0.00") + "','" + section + "','" + ViewState["Userid"].ToString() + "','" + ViewState["branch_id"].ToString() + "','" + group_id + "','" + class_id + "','" + position + "','" + afterdicount.ToString("0.00") + "')");


                                        }

                                    }




                                }
                                else
                                {
                                    // string get_onemonthback = mycode.get_one_monthback(monthid);
                                    // mycode.executequery("delete from Typewise_fee_collection where admission_no='" + ViewState["admSionNo"].ToString() + "' and session='" + sessionname + "' and parameter='MonthlyFee' and feetype='TransportationFee' and month='" + get_onemonthback + "'");
                                }







                                empty_feilds();
                            }
                        }
                        catch
                        {
                        }



                    }
                }
                else
                {
                    Alertme("This Student has been already paid selected month so you can not assign selected month ", "warning");
                }
            }
            else
            {
                Alertme("Sorry this student already assigned transport, So you have not again assigned ", "warning");
            }
        }

        private bool chek_assined_or_not(string sessionIDs, string admSionNo)
        {
            string queryS = "select * from Student_mapping_with_TransportPath where Session_id='" + sessionIDs + "' and Admission_no='" + admSionNo + "'";
            DataTable dts = My.dataTable(queryS);
            if (dts.Rows.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void empty_feilds()
        {
            txt_admission_no.Text = "";
            txt_section.Text = "";
            txt_student_name.Text = "";
            txtrollnumber.Text = "";
            txtsection.Text = "";
            ddl_path_root.SelectedValue = "0";
            std_basic_infoS.Visible = false;
        }





    }
}