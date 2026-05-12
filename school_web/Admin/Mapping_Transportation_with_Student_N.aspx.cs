using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class Mapping_Transportation_with_Student_N : System.Web.UI.Page
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
                        ViewState["branch_id"] = mycode.get_branch_id(Session["Admin"].ToString());
                        ViewState["Branch_id"] = mycode.get_branch_id(Session["Admin"].ToString());



                        try
                        {
                            mycode.bind_all_ddl_with_id(ddlsessionad, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");

                            mycode.bind_all_ddl_with_id(ddl_session_student, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");

                            ddlsessionad.SelectedValue = My.get_session_id();
                            ddl_session_student.SelectedValue = My.get_session_id();
                        }
                        catch
                        {

                        }
                        mycode.bind_all_ddl_with_id(ddl_from_month, "select Month,Position from Month_Index order by Position asc");
                        // mycode.bind_all_ddl_with_id(ddl_path_root, " select  Pathname,TransportationPath_id from  TransportationPath");
                        mycode.bind_all_ddl_with_id(ddl_bus_name, "select transport_name+'-{BUS NO-'+Bus_no+'}',transport_id from  Transport_Master order by transport_name");

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
        protected void ddl_bus_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_bus_name.SelectedItem.Text == "Select")
            {
                Alertme("Please select bus name", "warning");

            }
            else
            {
                mycode.bind_all_ddl_with_id(ddl_path_root, " select  Rootname,TransportationPath_id from  TransportationPath where Transportation_Id=" + ddl_bus_name.SelectedValue + " order by Rootname asc");
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
                    cmd.CommandText = "select distinct studentname from admission_registor where studentname LIKE '%'+@SearchMobNo+'%'  and  Transfer_Status in ('NT','New' ) and  Status='1' and Session_id=" + Session_id + "";
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
                    cmd.CommandText = "select distinct admissionserialnumber from admission_registor where admissionserialnumber LIKE '%'+@SearchMobNo+'%'   and  Transfer_Status in ('NT','New' ) and  Status='1' and Session_id=" + Session_id + "";
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
                if (txt_admission_no.Text == "")
                {
                    Alertme("Please enter admission no.", "warning");
                }
                else
                {

                    string query = "select * from admission_registor where admissionserialnumber='" + txt_admission_no.Text + "' and Transfer_Status in ('NT','New' ) and Status='1' order by id desc";
                    DataTable dt = My.dataTable(query);
                    if (dt.Rows.Count == 0)
                    {
                        rp_std.DataSource = null;
                        rp_std.DataBind();
                        Alertme("Data Not Found...", "warning");
                        // myModal2.Visible = false;
                    }
                    else
                    {
                        find_details(query);
                        //rp_std.DataSource = dt;
                        //rp_std.DataBind();
                        //myModal2.Visible = true;
                    }
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
                if (txt_student_name.Text == "")
                {
                    Alertme("Please enter student name", "warning");
                }
                else
                {
                    string query = "select * from admission_registor where studentname like '%" + txt_student_name.Text + "%' and Transfer_Status in ('NT','New' ) and Status='1' order by id desc";
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
            }
            catch
            {
            }
        }
        #endregion

        protected void lnk_select_Click(object sender, EventArgs e)
        {
            try
            {

                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbladmissionserialnumber = (Label)row.FindControl("lbladmissionserialnumber");
                Label lbl_session = (Label)row.FindControl("lbl_session");
                Label lbl_Academic_Sem_or_Year_id = (Label)row.FindControl("lbl_Academic_Sem_or_Year_id");
                string query = "select top 1 * from admission_registor where admissionserialnumber='" + lbladmissionserialnumber.Text + "' and Session='" + lbl_session.Text + "' and Transfer_Status in ('NT','New' ) and Status='1'  order by id desc";
                find_details(query);
                myModal2.Visible = false;
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }

        private void find_details(string query)
        {
            selected_seletd.Visible = false;
            seatview.Visible = false;
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
                    string queryS = "select * from Student_mapping_with_TransportPath where Session_id='" + dr["Session_id"].ToString() + "' and Admission_no='" + dr["admissionserialnumber"].ToString() + "' and Class_id='" + dr["Class_id"].ToString() + "' and branch_id='" + ViewState["Branch_id"].ToString() + "'";
                    DataTable dts = My.dataTable(queryS);
                    if (dts.Rows.Count != 0)
                    {
                        Alertme("The selected student has already been assigned to transportation. ", "warning");
                        return;
                    }

                    //=====
                    string queryST = "select * from Hostel_assign_master where Session_id='" + dr["Session_id"].ToString() + "' and Admission_no='" + dr["admissionserialnumber"].ToString() + "' and Class_id='" + dr["Class_id"].ToString() + "' and Status='1'";
                    DataTable dtsT = My.dataTable(queryST);
                    if (dtsT.Rows.Count != 0)
                    {
                        Alertme("The student you have selected is currently residing in the hostel. Please remove him from the hostel before assigning him to transportation.", "warning");
                        return;
                    }

                    ViewState["gcm_id"] = dr["gcm_id"].ToString();
                    std_basic_infoS.Visible = true;
                    txt_student_name.Text = dr["studentname"].ToString();

                    ddl_session_student.SelectedValue = dr["Session_id"].ToString();

                    lbl_name.Text = dr["studentname"].ToString();
                    lbl_father_name.Text = dr["fathername"].ToString();
                    lblclass.Text = dr["class"].ToString();
                    ViewState["class_id"] = dr["Class_id"].ToString();
                    txtsection.Text = dr["Section"].ToString();
                    lbl_admission_no.Text = dr["admissionserialnumber"].ToString();
                    txt_admission_no.Text = dr["admissionserialnumber"].ToString();
                    //  lblhostel.Text = dr["hosteltaken"].ToString();
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
                    ViewState["Current_Semester_or_Year"] = dr["Academic_Sem_or_Year_id"].ToString();

                    lbl_yesr_smester.Text = dr["rollnumber"].ToString(); //dr["Academic_Sem_or_Year"].ToString();
                    if (dr["Transportation_Id"].ToString() == "")
                    {
                        ViewState["transportID"] = "0";
                    }
                    else
                    {
                        ViewState["transportID"] = dr["Transportation_Id"].ToString();
                    }

                    try
                    {
                        if (ViewState["day_bording_with_lunch"].ToString() == "True")
                        {
                            ViewState["parameteridS"] = "44";
                        }
                        else
                        {
                            ViewState["parameteridS"] = "4";
                        }
                    }
                    catch
                    {
                        ViewState["parameteridS"] = "4";
                    }
                }
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            myModal2.Visible = false;
        }

        protected void btn_save_bording_Click(object sender, EventArgs e)
        {
            if (ddl_from_month.Text == "Select")
            {
                Alertme("Please select month ", "warning");
            }
            else if (ddl_bus_name.SelectedItem.Text == "Select")
            {
                Alertme("Please select bus Name ", "warning");
            }
            else if (ddl_path_root.SelectedItem.Text == "Select")
            {
                Alertme("Please select transportation route ", "warning");
            }
            else if (ddl_seatname.SelectedItem.Text == "Select")
            {
                Alertme("Please select buss Seat name ", "warning");
            }
            else
            {
                Save_data_Trasport();
            }
        }

        private void Save_data_Trasport()
        {
            string session = mycode.get_session(ViewState["sessionIDs"].ToString());
            bool flag = false;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
            {
                SqlConnection con = new SqlConnection(My.conn);
                con.Open();
                string tranportassinedid = payments.get_transport_assigned_id(con);
                string cunrt_session = ViewState["session"].ToString();
                string session_frst_year = cunrt_session.Substring(0, 4);
                int s_year = My.toint(session_frst_year); 
                string monthid = My.tomonth_numberstring(ddl_from_month.SelectedItem.Text); 
                int pay_month = My.toint(monthid); 
                string final = s_year.ToString() + monthid; 
                SqlCommand cmd;
                string query = "INSERT INTO Student_mapping_with_TransportPath (Session_id,Admission_no,Class_id,Month_name,Month_id,TransportPath_id,transport_id,Created_by,Created_date,Created_idate,branch_id,Year_month,Transport_Assigned_Id,Academic_Sem_or_Year_id,Mapping_Year,Sheet_Id,Boarding_Point_id) values (@Session_id,@Admission_no,@Class_id,@Month_name,@Month_id,@TransportPath_id,@transport_id,@Created_by,@Created_date,@Created_idate,@branch_id,@Year_month,@Transport_Assigned_Id,@Academic_Sem_or_Year_id,@Mapping_Year,@Sheet_Id,@Boarding_Point_id); INSERT INTO Student_mapping_with_TransportPath_history (Session_id,Admission_no,Class_id,Month_name,Month_id,TransportPath_id,transport_id,Remark,Created_by,Created_date,Created_idate,branch_id,Year_month,Transport_Assigned_Id,Update_Status,Academic_Sem_or_Year_id,Mapping_Year,Sheet_Id,Boarding_Point_id) values (@Session_id,@Admission_no,@Class_id,@Month_name,@Month_id,@TransportPath_id,@transport_id,@Remark,@Created_by,@Created_date,@Created_idate,@branch_id,@Year_month,@Transport_Assigned_Id,@Update_Status,@Academic_Sem_or_Year_id,@Mapping_Year,@Sheet_Id,@Boarding_Point_id)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Session_id", ViewState["sessionIDs"].ToString());
                cmd.Parameters.AddWithValue("@Admission_no", ViewState["admSionNo"].ToString());
                cmd.Parameters.AddWithValue("@Class_id", ViewState["classid"].ToString());
                cmd.Parameters.AddWithValue("@Month_name", ddl_from_month.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Month_id", monthid);
                cmd.Parameters.AddWithValue("@TransportPath_id", ddl_path_root.SelectedValue);
                cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
                cmd.Parameters.AddWithValue("@branch_id", ViewState["branch_id"].ToString());
                cmd.Parameters.AddWithValue("@Year_month", final);
                cmd.Parameters.AddWithValue("@Transport_Assigned_Id", tranportassinedid);
                cmd.Parameters.AddWithValue("@Academic_Sem_or_Year_id", ViewState["Current_Semester_or_Year"].ToString());
                cmd.Parameters.AddWithValue("@Mapping_Year", s_year);
                cmd.Parameters.AddWithValue("@Sheet_Id", ddl_seatname.SelectedValue);
                cmd.Parameters.AddWithValue("@Remark", "Transport assigned");
                cmd.Parameters.AddWithValue("@Update_Status", "Assigned");
                cmd.Parameters.AddWithValue("@transport_id", ddl_bus_name.SelectedValue);
                cmd.Parameters.AddWithValue("@Boarding_Point_id", ddl_boarding_point.SelectedValue);
                if (payments.InsertUpdateData(cmd, con))
                {

                    try
                    {
                        payments.exeSql("update Transport_Path_Mapping_With_Sheet set Sheet_Status=1 where TransportationPath_id=" + ddl_path_root.SelectedValue + " and Transportation_Id=" + ddl_bus_name.SelectedValue + " and Sheet_Id=" + ddl_seatname.SelectedValue + "", con);
                        SqlCommand cmd1;
                        string query1 = "Update admission_registor set Transportation_Id=@Transportation_Id,Transportationpath=@Transportationpath,transportationtaken=@transportationtaken,hosteltaken=@hosteltaken where admissionserialnumber=@admissionserialnumber and Session_id=@Session_id and Class_id=" + ViewState["classid"].ToString() + " ";
                        cmd1 = new SqlCommand(query1);
                        cmd1.Parameters.AddWithValue("@Transportation_Id", ddl_path_root.SelectedValue);
                        cmd1.Parameters.AddWithValue("@Transportationpath", ddl_path_root.SelectedItem.Text);
                        cmd1.Parameters.AddWithValue("@transportationtaken", "Yes");
                        cmd1.Parameters.AddWithValue("@hosteltaken", "No");
                        cmd1.Parameters.AddWithValue("@Session_id", ViewState["sessionIDs"].ToString());
                        cmd1.Parameters.AddWithValue("@admissionserialnumber", ViewState["admSionNo"].ToString());
                        if (payments.InsertUpdateData(cmd1, con))
                        { 
                            string sessionname = payments.get_session_by_id(ViewState["sessionIDs"].ToString(), con);
                            DataTable dtTT = payments.dataTable("select * from Transportation_Boarding_Point where Session_Id='" + ViewState["sessionIDs"].ToString() + "' and Boarding_Point_id='" + ddl_boarding_point.SelectedValue + "'", con);
                            DataTable dtTR = payments.dataTable("Select distinct month,class,position,section,class_id from Typewise_fee_collection where admission_no='" + ViewState["admSionNo"].ToString() + "' and session='" + sessionname + "' and parameter ='MonthlyFee' and class_id=" + ViewState["classid"].ToString() + " and position>='" + ddl_from_month.SelectedValue + "' order by position asc ", con);//
                            if (dtTR.Rows.Count > 0)
                            {
                                foreach (DataRow drtr in dtTR.Rows)
                                {
                                    string class1 = drtr["class"].ToString();
                                    string section = drtr["section"].ToString();
                                    string feetype = "TransportationFee";
                                    string month = drtr["month"].ToString();
                                    string user_id = ViewState["Userid"].ToString();
                                    string group_id = "3";
                                    string position = drtr["position"].ToString();
                                    string class_id = drtr["class_id"].ToString();
                                    string branchid = ViewState["branch_id"].ToString();


                                    string TransportationFeetype = "TransportationFee";
                                    string content_id1 = dtTT.Rows[0]["TransportationPath_id"].ToString();
                                    string Amount = dtTT.Rows[0]["Amount"].ToString();
                                    string disc_amount = "0.00";
                                    double afterdicount = My.toDouble(Amount) - My.toDouble(disc_amount);


                                    payments.exeSql("delete from Typewise_fee_collection where admission_no='" + ViewState["admSionNo"].ToString() + "' and session='" + sessionname + "' and parameter='MonthlyFee' and feetype='TransportationFee' and month='" + month + "' and class_id='" + class_id + "'", con);
                                    payments.exeSql(@"insert into Typewise_fee_collection(admission_no,class,session,parameter,Date,idate,feetype,payable,paid,dues,status,month,content_id,transection,
Ledger,is_readyfor_sync,is_sync,is_sync_dues_diary,Disc,section,user_id,branchid,group_id,class_id,position,Payable_after_disc) 
values ('" + ViewState["admSionNo"].ToString() + "','" + class1 + "','" + sessionname + "','MonthlyFee','" + mycode.date() + "','" + mycode.idate() + "','" + TransportationFeetype + "','" + My.toDouble(Amount).ToString("0.00") + "','0','" + My.toDouble(afterdicount).ToString("0.00") + "','Dues','" + month + "','1002','00','School','false','false','false','" + My.toDouble(disc_amount).ToString("0.00") + "','" + section + "','" + ViewState["Userid"].ToString() + "','" + branchid + "','" + group_id + "','" + class_id + "','" + position + "','" + afterdicount.ToString("0.00") + "')", con);
                                }
                            } 
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }

                dues_update_headwise_transaction.update_student_dues(ViewState["sessionIDs"].ToString(), ViewState["classid"].ToString(), ViewState["admSionNo"].ToString(), "0", "0", con);


                flag = true;
                con.Close();
                scope.Complete();
            }

            if (flag == true)
            {
                Alertme("This Student has been assigned with Transport successfully.", "success"); 
                try
                {
                    string sub = "Transport assigned";
                    string messge = "Dear " + lbl_name.Text + " transport assigned sucessfully vehicle name :- " + ddl_bus_name.SelectedItem.Text + " transportation route :- " + ddl_path_root.SelectedItem.Text + " boarding point.:-" + ddl_boarding_point.SelectedItem.Text;
                    Dictionary<String, String> ss = new Dictionary<string, string>();
                    ss["notification_id"] = Guid.NewGuid().ToString();
                    ss["message"] = messge;
                    ss["title"] = sub;
                    ss["messagetype"] = "Message";
                    ss["url"] = "";
                    ss["link_url"] = "";
                    ss["UserId"] = ViewState["admSionNo"].ToString();
                    UsesCode.SendNotification(ViewState["gcm_id"].ToString(), ss);
                }
                catch
                {
                }

                empty_feilds();
            }
            else
            {
                Alertme("Something went wrong. Please try again", "warning");
            }
        }

        private void empty_feilds()
        {
            txt_admission_no.Text = "";
            txt_student_name.Text = "";
            txtsection.Text = "";
            ddl_path_root.SelectedValue = "0";
            std_basic_infoS.Visible = false;


            ddl_from_month.SelectedValue = "0";

            ddl_bus_name.SelectedValue = "0";
            lbl_boardingpoint.Text = "0";
            lbl_kmcoverdby.Text = "0";
            lbl_trasportfee.Text = "0";

            selected_seletd.Visible = false;
            imgbuton_2.ImageUrl = "~/images/bus_icon/Booked.png";
            lbl_byside_seat.Text = "";
        }

        protected void ddl_boarding_point_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl_boardingpoint.Text = "";
            lbl_kmcoverdby.Text = "";
            lbl_trasportfee.Text = "";
            if (ddl_bus_name.SelectedItem.Text == "Select")
            {
                Alertme("Please select bus Name ", "warning");
            }
            else if (ddl_path_root.SelectedItem.Text == "Select")
            {
                Alertme("Please select transportation route ", "warning");
            }
            else
            {
                string queryS = "select * from Transportation_Boarding_Point where Transportation_Id='" + ddl_bus_name.SelectedValue + "' and TransportationPath_id='" + ddl_path_root.SelectedValue + "' and Boarding_Point_id='" + ddl_boarding_point.SelectedValue + "' and Session_Id='" + ViewState["sessionIDs"].ToString() + "'";
                DataTable dts = My.dataTable(queryS);
                if (dts.Rows.Count != 0)
                {
                    lbl_boardingpoint.Text = dts.Rows[0]["Boarding_Point"].ToString();
                    lbl_kmcoverdby.Text = dts.Rows[0]["KM"].ToString();
                    lbl_trasportfee.Text = dts.Rows[0]["Amount"].ToString();
                }
                else
                {
                    lbl_boardingpoint.Text = "";
                    lbl_kmcoverdby.Text = "";
                    lbl_trasportfee.Text = "";
                }
            }
        }
        protected void ddl_path_root_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_bus_name.SelectedItem.Text == "Select")
            {
                Alertme("Please select bus name", "warning");

            }
            else if (ddl_path_root.SelectedItem.Text == "Select")
            {
                Alertme("Please select transportation route", "warning");
            }
            else
            {
                mycode.bind_all_ddl_with_id(ddl_seatname, " select  Sheet_No,Sheet_Id from  Transport_Path_Mapping_With_Sheet where Transportation_Id=" + ddl_bus_name.SelectedValue + " and TransportationPath_id=" + ddl_path_root.SelectedValue + " and Sheet_Status='0'");

                mycode.bind_all_ddl_with_id(ddl_boarding_point, " select  Boarding_Point,Boarding_Point_id from  Transportation_Boarding_Point where Transportation_Id=" + ddl_bus_name.SelectedValue + " and TransportationPath_id=" + ddl_path_root.SelectedValue + "  and Session_Id='" + ViewState["sessionIDs"].ToString() + "' order by Boarding_Point");
                Bind_seat_view_left();
                Bind_seat_view_right();
                Bind_seat_view_Back();

                seatview.Visible = true;

                // selected_seletd.Visible = false;

            }

        }

        private void Bind_seat_view_Back()
        {
            string queryS = "select * from Transport_Path_Mapping_With_Sheet where Transportation_Id='" + ddl_bus_name.SelectedValue + "' and TransportationPath_id='" + ddl_path_root.SelectedValue + "' and Sheet_position='Back' order by Row asc ";
            DataTable dts = My.dataTable(queryS);
            if (dts.Rows.Count == 0)
            {
                dl_bus_seat_back.DataSource = null;
                dl_bus_seat_back.DataBind();
            }
            else
            {
                dl_bus_seat_back.RepeatColumns = Convert.ToInt32(dts.Rows[0]["Seat_Model"].ToString());
                dl_bus_seat_back.DataSource = dts;
                dl_bus_seat_back.DataBind();
            }
        }

        private void Bind_seat_view_right()
        {
            string queryS = "select * from Transport_Path_Mapping_With_Sheet where Transportation_Id='" + ddl_bus_name.SelectedValue + "' and TransportationPath_id='" + ddl_path_root.SelectedValue + "' and Sheet_position='Right' order by Row asc ";
            DataTable dts = My.dataTable(queryS);
            if (dts.Rows.Count == 0)
            {
                dl_bus_seat_right.DataSource = null;
                dl_bus_seat_right.DataBind();
            }
            else
            {
                dl_bus_seat_right.RepeatColumns = Convert.ToInt32(dts.Rows[0]["Seat_Model"].ToString());
                dl_bus_seat_right.DataSource = dts;
                dl_bus_seat_right.DataBind();
            }
        }

        private void Bind_seat_view_left()
        {
            string queryS = "select * from Transport_Path_Mapping_With_Sheet where Transportation_Id='" + ddl_bus_name.SelectedValue + "' and TransportationPath_id='" + ddl_path_root.SelectedValue + "' and Sheet_position='Left' order by Row asc ";
            DataTable dts = My.dataTable(queryS);
            if (dts.Rows.Count == 0)
            {
                dlbus_seatleft.DataSource = null;
                dlbus_seatleft.DataBind();

            }
            else
            {
                dlbus_seatleft.RepeatColumns = Convert.ToInt32(dts.Rows[0]["Seat_Model"].ToString());
                dlbus_seatleft.DataSource = dts;
                dlbus_seatleft.DataBind();
            }
        }
        protected void dlbus_seatleft_ItemDataBound(object sender, DataListItemEventArgs e)
        {


            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label Sheet_Id = (Label)e.Item.FindControl("Sheet_Id");
                Label lbl_Transportation_Id = (Label)e.Item.FindControl("lbl_Transportation_Id");
                Label lbl_TransportationPath_id = (Label)e.Item.FindControl("lbl_TransportationPath_id");
                Label lbl_Seat_Type = (Label)e.Item.FindControl("lbl_Seat_Type");
                Label lbl_Row = (Label)e.Item.FindControl("lbl_Row");
                Label lbl_Sheet_Status = (Label)e.Item.FindControl("lbl_Sheet_Status");





                ImageButton imgbuton_1 = (ImageButton)e.Item.FindControl("imgbuton_1_left");

                if (lbl_Seat_Type.Text == "Boy")
                {
                    imgbuton_1.ImageUrl = "~/images/bus_icon/boy.png";

                }

                else if (lbl_Seat_Type.Text == "Girl")
                {
                    imgbuton_1.ImageUrl = "~/images/bus_icon/girle.png";
                }
                else
                {
                    imgbuton_1.ImageUrl = "~/images/bus_icon/staff.png";
                }

                if (lbl_Sheet_Status.Text == "0")
                {
                    imgbuton_1.Enabled = true;
                }
                else
                {
                    imgbuton_1.ImageUrl = "~/images/bus_icon/Booked.png";

                    imgbuton_1.Enabled = false;
                }
            }
        }

        protected void dl_bus_seat_right_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            Label Sheet_Id = (Label)e.Item.FindControl("Sheet_Id");
            Label lbl_Transportation_Id = (Label)e.Item.FindControl("lbl_Transportation_Id");
            Label lbl_TransportationPath_id = (Label)e.Item.FindControl("lbl_TransportationPath_id");
            Label lbl_Seat_Type = (Label)e.Item.FindControl("lbl_Seat_Type");
            Label lbl_Row = (Label)e.Item.FindControl("lbl_Row");
            Label lbl_Sheet_Status = (Label)e.Item.FindControl("lbl_Sheet_Status");
            ImageButton imgbuton_1 = (ImageButton)e.Item.FindControl("imgbuton_1_right");

            if (lbl_Seat_Type.Text == "Boy")
            {
                imgbuton_1.ImageUrl = "~/images/bus_icon/boy.png";

            }

            else if (lbl_Seat_Type.Text == "Girl")
            {
                imgbuton_1.ImageUrl = "~/images/bus_icon/girle.png";
            }
            else
            {
                imgbuton_1.ImageUrl = "~/images/bus_icon/staff.png";
            }
            if (lbl_Sheet_Status.Text == "0")
            {
                imgbuton_1.Enabled = true;
            }
            else
            {
                imgbuton_1.ImageUrl = "~/images/bus_icon/Booked.png";
                imgbuton_1.Enabled = false;
            }
        }

        protected void dl_bus_seat_back_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            Label Sheet_Id = (Label)e.Item.FindControl("Sheet_Id");
            Label lbl_Transportation_Id = (Label)e.Item.FindControl("lbl_Transportation_Id");
            Label lbl_TransportationPath_id = (Label)e.Item.FindControl("lbl_TransportationPath_id");
            Label lbl_Seat_Type = (Label)e.Item.FindControl("lbl_Seat_Type");
            Label lbl_Row = (Label)e.Item.FindControl("lbl_Row");
            Label lbl_Sheet_Status = (Label)e.Item.FindControl("lbl_Sheet_Status");
            ImageButton imgbuton_1 = (ImageButton)e.Item.FindControl("imgbuton_1_back");

            if (lbl_Seat_Type.Text == "Boy")
            {
                imgbuton_1.ImageUrl = "~/images/bus_icon/boy.png";

            }

            else if (lbl_Seat_Type.Text == "Girl")
            {
                imgbuton_1.ImageUrl = "~/images/bus_icon/girle.png";
            }
            else
            {
                imgbuton_1.ImageUrl = "~/images/bus_icon/staff.png";
            }
            if (lbl_Sheet_Status.Text == "0")
            {
                imgbuton_1.Enabled = true;
            }
            else
            {
                imgbuton_1.ImageUrl = "~/images/bus_icon/Booked.png";
                imgbuton_1.Enabled = false;
            }
        }



        protected void imgbuton_1_left_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                DataListItem datalist = ((ImageButton)sender).NamingContainer as DataListItem;
                string lbl_Sheet_Id = ((Label)datalist.FindControl("lbl_Sheet_Id")).Text;
                string lbl_Sheet_No = ((Label)datalist.FindControl("lbl_Sheet_No")).Text;
                string lbl_TransportationPath_id = ((Label)datalist.FindControl("lbl_TransportationPath_id")).Text;
                string lbl_Transportation_Id = ((Label)datalist.FindControl("lbl_Transportation_Id")).Text;
                string lbl_Seat_Type = ((Label)datalist.FindControl("lbl_Seat_Type")).Text;
                string lbl_Row = ((Label)datalist.FindControl("lbl_Row")).Text;
                string lbl_Sheet_position = ((Label)datalist.FindControl("lbl_Sheet_position")).Text;



                ViewState["Sheet_Id"] = lbl_Sheet_Id;
                ddl_seatname.SelectedValue = lbl_Sheet_Id;

                select_seat_click(lbl_Sheet_Id, lbl_Sheet_No, lbl_TransportationPath_id, lbl_Transportation_Id, lbl_Seat_Type, lbl_Row, lbl_Sheet_position);
            }
            catch (Exception ex)
            {

            }






        }

        private void select_seat_click(string lbl_Sheet_Id, string lbl_Sheet_No, string lbl_TransportationPath_id, string lbl_Transportation_Id, string lbl_Seat_Type, string lbl_Row, string Sheet_position)
        {
            selected_seletd.Visible = true;

            if (lbl_Seat_Type == "Boy")
            {
                imgbuton_1.ImageUrl = "~/images/bus_icon/boy.png";

            }

            else if (lbl_Seat_Type == "Girl")
            {
                imgbuton_1.ImageUrl = "~/images/bus_icon/girle.png";
            }
            else
            {
                imgbuton_1.ImageUrl = "~/images/bus_icon/staff.png";
            }

            lbl_seletedseat.Text = lbl_Sheet_No;
            //--------lbl_byside seat seatdetails---------------

            string queryS = "select top 1 * from Transport_Path_Mapping_With_Sheet where Transportation_Id='" + lbl_Transportation_Id + "' and TransportationPath_id='" + lbl_TransportationPath_id + "' and Row=" + lbl_Row + " and Seat_Type='" + lbl_Seat_Type + "' and  Sheet_Id!='" + lbl_Sheet_Id + "' and   Sheet_position='" + Sheet_position + "'  order by Sheet_No asc  ";
            DataTable dts = My.dataTable(queryS);
            if (dts.Rows.Count == 0)
            {

                imgbuton_2.Visible = false;

            }
            else
            {

                lbl_byside_seat.Text = dts.Rows[0]["Sheet_No"].ToString();
                if (dts.Rows[0]["Seat_Type"].ToString() == "Boy")
                {
                    imgbuton_2.ImageUrl = "~/images/bus_icon/boy.png";

                }
                else if (dts.Rows[0]["Seat_Type"].ToString() == "Girl")
                {
                    imgbuton_2.ImageUrl = "~/images/bus_icon/girle.png";
                }
                else
                {
                    imgbuton_2.ImageUrl = "~/images/bus_icon/staff.png";
                }
                gte_student_by_side_info(dts.Rows[0]["Sheet_Id"].ToString(), dts.Rows[0]["TransportationPath_id"].ToString(), dts.Rows[0]["Transportation_Id"].ToString());




            }
        }

        private void gte_student_by_side_info(string Sheet_Id, string TransportationPath_id, string Transportation_Id)
        {
            string query = "select t1.*,t2.session,t2.admissionserialnumber,t2.mobilenumber,t2.rollnumber,t2.Section,t2.studentname,t2.class,t2.Category_id,t2.SubCategory_id  from Student_mapping_with_TransportPath t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Admission_no=t2.admissionserialnumber and t1.Class_id=t2.Class_id and t1.branch_id=t2.Branch_id and t1.Academic_Sem_or_Year_id=t2.Academic_Sem_or_Year_id where t1.transport_id='" + Transportation_Id + "' and t1.TransportPath_id='" + TransportationPath_id + "' and  t1.Sheet_Id='" + Sheet_Id + "' order by t1.id desc";

            DataTable dts = My.dataTable(query);
            if (dts.Rows.Count == 0)
            {
                lbl_student_info.Text = "Available Seat";
            }
            else
            {
                lbl_student_info.Text = "Student Name : " + dts.Rows[0]["studentname"].ToString() + " Admission No. :" + dts.Rows[0]["admissionserialnumber"].ToString() + " Mobile No." + dts.Rows[0]["mobilenumber"].ToString();
            }
        }

        protected void imgbuton_1_right_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                DataListItem datalist = ((ImageButton)sender).NamingContainer as DataListItem;
                string lbl_Sheet_Id = ((Label)datalist.FindControl("lbl_Sheet_Id")).Text;
                string lbl_Sheet_No = ((Label)datalist.FindControl("lbl_Sheet_No")).Text;
                string lbl_TransportationPath_id = ((Label)datalist.FindControl("lbl_TransportationPath_id")).Text;
                string lbl_Transportation_Id = ((Label)datalist.FindControl("lbl_Transportation_Id")).Text;
                string lbl_Seat_Type = ((Label)datalist.FindControl("lbl_Seat_Type")).Text;
                string lbl_Row = ((Label)datalist.FindControl("lbl_Row")).Text;
                string lbl_Sheet_position = ((Label)datalist.FindControl("lbl_Sheet_position")).Text;
                ViewState["Sheet_Id"] = lbl_Sheet_Id;
                ddl_seatname.SelectedValue = lbl_Sheet_Id;

                select_seat_click(lbl_Sheet_Id, lbl_Sheet_No, lbl_TransportationPath_id, lbl_Transportation_Id, lbl_Seat_Type, lbl_Row, lbl_Sheet_position);
            }
            catch
            {

            }
        }

        protected void imgbuton_1_back_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                DataListItem datalist = ((ImageButton)sender).NamingContainer as DataListItem;
                string lbl_Sheet_Id = ((Label)datalist.FindControl("lbl_Sheet_Id")).Text;
                string lbl_Sheet_No = ((Label)datalist.FindControl("lbl_Sheet_No")).Text;
                string lbl_TransportationPath_id = ((Label)datalist.FindControl("lbl_TransportationPath_id")).Text;
                string lbl_Transportation_Id = ((Label)datalist.FindControl("lbl_Transportation_Id")).Text;
                string lbl_Seat_Type = ((Label)datalist.FindControl("lbl_Seat_Type")).Text;
                string lbl_Row = ((Label)datalist.FindControl("lbl_Row")).Text;
                string lbl_Sheet_position = ((Label)datalist.FindControl("lbl_Sheet_position")).Text;
                ViewState["Sheet_Id"] = lbl_Sheet_Id;
                ddl_seatname.SelectedValue = lbl_Sheet_Id;
                select_seat_click(lbl_Sheet_Id, lbl_Sheet_No, lbl_TransportationPath_id, lbl_Transportation_Id, lbl_Seat_Type, lbl_Row, lbl_Sheet_position);
            }
            catch
            {

            }
        }
    }
}