using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
using System.IO;
using System.Transactions;

namespace school_web.Admin
{
    public partial class Transport_Taken_Students_List : System.Web.UI.Page
    {
        string path = "Select top 1 Pathname from TransportationPath where TransportationPath_id=t1.TransportPath_id";
        string transportname = "Select top 1 transport_name from Transport_Master where transport_id=t1.transport_id";
        string seatname = "Select top 1 Sheet_No from Transport_Path_Mapping_With_Sheet where Transportation_Id=t1.transport_id and TransportationPath_id=t1.TransportPath_id  and Sheet_Id=t1.Sheet_Id";



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
                        string pagename_current = "Transport_Taken_Students_List.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        find_firm_details();
                        ViewState["branch_id"] = mycode.get_branch_id(ViewState["Userid"].ToString());

                        mycode.bind_all_ddl_with_id(ddlsession, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                        ddlsession.SelectedValue = My.get_session_id();
                        mycode.bind_all_ddl_with_id_All_New(ddlclass, "Select Course_Name,course_id,Position from Add_course_table order by Position");
                        //ddlclass.SelectedValue = My.get_top_one_class();
                        mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor where Session_id='00'");

                        mycode.bind_all_ddl_with_id(ddl_monthname, "select Month,Month_Id from Month_Index order by Position asc");


                        mycode.bind_all_ddl_with_id(ddl_vehicle, "select transport_name,transport_id from Transport_Master order by transport_name asc");
                        get_boarding_point();

                        //bind_all_data();
                        //ViewState["flag"] = "1";
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Transport_Taken_Students_List");
            }
        }

        private void get_boarding_point()
        {
            mycode.bind_all_ddl_with_id(ddl_Transportation_Distance, "select Boarding_Point,Boarding_Point_id from TRANSPORTATION_BOARDING_POINT where Session_Id='" + ddlsession.SelectedValue + "' and Boarding_Point_id in(select Boarding_Point_id from Student_mapping_with_TransportPath where Session_id=TRANSPORTATION_BOARDING_POINT.Session_Id) order by Boarding_Point asc");
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


        private void bind_all_data()
        {
            bind_grd_view("select t1.*,t2.session,t2.admissionserialnumber,t2.rollnumber,t2.Section,t2.studentname,t2.class,t2.Category_id,t2.SubCategory_id,t2.fathername,t2.mobilenumber,(" + path + ") as Pathname,(" + seatname + ") as seatname,(select top 1 Amount from Transportation_Boarding_Point where TransportationPath_id=t1.TransportPath_id and Transportation_Id=t1.transport_id and Boarding_Point_id=t1.Boarding_Point_id and  Session_Id=" + ddlsession.SelectedValue + " order by Id desc) as Transport_fee,(select top 1 Boarding_Point from Transportation_Boarding_Point where TransportationPath_id=t1.TransportPath_id and Transportation_Id=t1.transport_id and Boarding_Point_id=t1.Boarding_Point_id and Session_Id=" + ddlsession.SelectedValue + " order by Id desc) as Boarding_Point,(select top 1 transport_name from Transport_Master where transport_id=t1.transport_id) as Bus_name from Student_mapping_with_TransportPath t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Admission_no=t2.admissionserialnumber and t1.Class_id=t2.Class_id and t1.branch_id=t2.Branch_id where t2.Transfer_Status in ('NT','New' ) and t2.Status='1' and t2.Session_id=" + ddlsession.SelectedValue + " order by t2.rollnumber asc");
        }

        My mycode = new My();
        private void bind_grd_view(string qry)
        {
            ViewState["query"] = qry;
            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }

        // remove

        protected void btn_conf_remove_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_reason.Text == "")
                {
                    Alertme("Please enter reason.", "warning");
                    txt_reason.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalCause();", true);
                }
                else
                {
                    bool flag = false;
                    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
                    {
                        SqlConnection con = new SqlConnection(My.conn);
                        con.Open();

                        string qry = "delete from Student_mapping_with_TransportPath where Session_id='" + ViewState["session_id"].ToString() + "' and Admission_no='" + ViewState["admissionserialnumber"].ToString() + "' and Class_id='" + ViewState["class_id"].ToString() + "'; delete from Typewise_fee_collection where admission_no='" + ViewState["admissionserialnumber"].ToString() + "' and session='" + ViewState["sessionN"].ToString() + "' and parameter='MonthlyFee' and feetype='TransportationFee' and class_id='" + ViewState["class_id"].ToString() + "' and convert(float, paid)=0 and status='Dues'";
                        payments.exeSql(qry, con);

                        string qry2 = "update Transport_Path_Mapping_With_Sheet set Sheet_Status=0    where TransportationPath_id=" + ViewState["TransportPath_id"].ToString() + " and Transportation_Id=" + ViewState["transport_id"].ToString() + " and Sheet_Id=" + ViewState["Sheet_Id"].ToString() + " ";
                        payments.exeSql(qry2, con);
                        SqlCommand cmd1;
                        string query1 = "Update admission_registor set Transportation_Id=@Transportation_Id,Transportationpath=@Transportationpath,transportationtaken=@transportationtaken where admissionserialnumber=@admissionserialnumber and Session_id=@Session_id and Class_id=@Class_id ";
                        cmd1 = new SqlCommand(query1);
                        cmd1.Parameters.AddWithValue("@Transportation_Id", "0");
                        cmd1.Parameters.AddWithValue("@Transportationpath", "");
                        cmd1.Parameters.AddWithValue("@transportationtaken", "No");

                        cmd1.Parameters.AddWithValue("@Class_id", ViewState["class_id"].ToString());
                        cmd1.Parameters.AddWithValue("@admissionserialnumber", ViewState["admissionserialnumber"].ToString());
                        cmd1.Parameters.AddWithValue("@Session_id", ViewState["session_id"].ToString());
                        if (payments.InsertUpdateData(cmd1, con))
                        {
                            string query2 = "update Student_mapping_with_TransportPath_history set Removed_date=@Removed_date,Removed_Idate=@Removed_Idate,Update_Status=@Update_Status,Remove_Update_Month=@Remove_Update_Month,Remarks=@Remarks where Transport_Assigned_Id=@Transport_Assigned_Id and TransportPath_id=@TransportPath_id and Admission_no=@Admission_no and Session_id=@Session_id";
                            cmd1 = new SqlCommand(query2);
                            cmd1.Parameters.AddWithValue("@Removed_date", mycode.date());
                            cmd1.Parameters.AddWithValue("@Removed_Idate", mycode.idate());
                            cmd1.Parameters.AddWithValue("@Update_Status", "Transport Removed");
                            cmd1.Parameters.AddWithValue("@Remove_Update_Month", mycode.get_current_monthname());
                            cmd1.Parameters.AddWithValue("@Remarks", txt_reason.Text);
                            cmd1.Parameters.AddWithValue("@TransportPath_id", ViewState["TransportPath_id"].ToString());
                            cmd1.Parameters.AddWithValue("@Transport_Assigned_Id", ViewState["Transport_Assigned_Id"].ToString());
                            cmd1.Parameters.AddWithValue("@Session_id", ViewState["session_id"].ToString());
                            cmd1.Parameters.AddWithValue("@Admission_no", ViewState["admissionserialnumber"].ToString());
                            if (payments.InsertUpdateData(cmd1, con))
                            {
                            }
                        }
                        dues_update_headwise_transaction.update_student_dues(ViewState["session_id"].ToString(), ViewState["class_id"].ToString(), ViewState["admissionserialnumber"].ToString(), "0", "0", con);

                        flag = true;
                        con.Close();
                        scope.Complete();
                    }
                    if (flag == true)
                    {
                        Alertme("Student has been removed from transportation successfully.", "success");
                        bind_grd_view(ViewState["query"].ToString());
                    }
                    else
                    {
                        Alertme("Something went wrong. Please try again", "warning");
                    }
                }
            }
            catch (Exception ex)
            {
                Alertme(ex.ToString(), "warning");
            }
        }
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                Label lbl_admissionserialnumber = (Label)row.FindControl("lbl_admissionserialnumber");
                Label lbl_month_id = (Label)row.FindControl("lbl_month_id");
                Label lbl_session_id = (Label)row.FindControl("lbl_session_id");
                Label lbl_class_id = (Label)row.FindControl("lbl_class_id");
                Label lbl_monthname = (Label)row.FindControl("lbl_monthname");
                Label lbl_TransportPath_id = (Label)row.FindControl("lbl_TransportPath_id");
                Label lbl_Transport_Assigned_Id = (Label)row.FindControl("lbl_Transport_Assigned_Id");
                Label lbl_transport_id = (Label)row.FindControl("lbl_transport_id");
                Label lbl_Sheet_Id = (Label)row.FindControl("lbl_Sheet_Id");
                Label lbl_session = (Label)row.FindControl("lbl_session");
                ViewState["admissionserialnumber"] = lbl_admissionserialnumber.Text;
                ViewState["session_id"] = lbl_session_id.Text;
                ViewState["class_id"] = lbl_class_id.Text;
                ViewState["TransportPath_id"] = lbl_TransportPath_id.Text;
                ViewState["transport_id"] = lbl_transport_id.Text;
                ViewState["Sheet_Id"] = lbl_Sheet_Id.Text;
                ViewState["Transport_Assigned_Id"] = lbl_Transport_Assigned_Id.Text;
                ViewState["sessionN"] = lbl_session.Text;
                txt_reason.Text = "";
                txt_reason.Focus();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalRemove();", true);

                //=======
                //string query = "select * from (select studentname,admissionserialnumber,class,Section,rollnumber,session,isnull((select sum(cast(dues as float)) from dbo.[Typewise_fee_collection] where admission_no=admission_registor.admissionserialnumber and session=admission_registor.session and month='" + lbl_monthname.Text + "' and parameter='MonthlyFee' ),'0') as dues from dbo.[admission_registor] where Class_id='" + lbl_class_id.Text + "' and Session_id='" + lbl_session_id.Text + "' and admissionserialnumber='" + lbl_admissionserialnumber.Text + "' ) t";
                //DataTable dt = My.dataTable(query);
                //if (dt.Rows.Count != 0)
                //{
                //    if (My.toDouble(dt.Rows[0]["dues"].ToString()) > 0)
                //    {
                //        Alertme("Can't remove transportation to this student due to dues monthly fee", "warning");
                //    }
                //    else
                //    {

                //    }
                //}
            }
            catch (Exception ex)
            {
            }
        }






        protected void ddlclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'");
                //find_by_class();
                //ViewState["flag"] = "3";
            }
            catch (Exception ex)
            {
            }
        }

        private void find_by_class()
        {
            bind_grd_view(" select t1.*,t2.session,t2.admissionserialnumber,t2.rollnumber,t2.Section,t2.studentname,t2.class,t2.Category_id,t2.SubCategory_id,t2.fathername,t2.mobilenumber,(" + path + ") as Pathname,(" + seatname + ") as seatname,(select top 1 Amount from Transportation_Boarding_Point where TransportationPath_id=t1.TransportPath_id and Transportation_Id=t1.transport_id and Boarding_Point_id=t1.Boarding_Point_id and  Session_Id=" + ddlsession.SelectedValue + " order by Id desc) as Transport_fee,(select top 1 Boarding_Point from Transportation_Boarding_Point where TransportationPath_id=t1.TransportPath_id and Transportation_Id=t1.transport_id and Boarding_Point_id=t1.Boarding_Point_id and Session_Id=" + ddlsession.SelectedValue + " order by Id desc) as Boarding_Point,(select top 1 transport_name from Transport_Master where transport_id=t1.transport_id) as Bus_name from Student_mapping_with_TransportPath t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Admission_no=t2.admissionserialnumber and t1.Class_id=t2.Class_id and t1.branch_id=t2.Branch_id where t1.Session_id='" + ddlsession.SelectedValue + "' and t1.Class_id='" + ddlclass.SelectedValue + "' and  t2.Transfer_Status in ('NT','New' ) and t2.Status='1' order by t2.rollnumber asc");
        }


        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session.", "warning");
                    ddlsession.Focus();
                }
                else
                {
                    find_by_c_s_a();
                    ViewState["flag"] = "1";
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_by_c_s_a()
        {
            if (ddlclass.SelectedItem.Text != "ALL" && ddl_section.SelectedItem.Text == "ALL")
            {
                bind_grd_view(" select t1.*,t2.session,t2.admissionserialnumber,t2.rollnumber,t2.Section,t2.studentname,t2.class,t2.Category_id,t2.SubCategory_id,t2.fathername,t2.mobilenumber,(" + path + ") as Pathname,(" + seatname + ") as seatname,(select top 1 Amount from Transportation_Boarding_Point where TransportationPath_id=t1.TransportPath_id and Transportation_Id=t1.transport_id and Boarding_Point_id=t1.Boarding_Point_id and  Session_Id=" + ddlsession.SelectedValue + " order by Id desc) as Transport_fee,(select top 1 Boarding_Point from Transportation_Boarding_Point where TransportationPath_id=t1.TransportPath_id and Transportation_Id=t1.transport_id and Boarding_Point_id=t1.Boarding_Point_id and Session_Id=" + ddlsession.SelectedValue + " order by Id desc) as Boarding_Point,(select top 1 transport_name from Transport_Master where transport_id=t1.transport_id) as Bus_name from Student_mapping_with_TransportPath t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Admission_no=t2.admissionserialnumber and t1.Class_id=t2.Class_id and t1.branch_id=t2.Branch_id where t1.Session_id='" + ddlsession.SelectedValue + "' and t1.Class_id='" + ddlclass.SelectedValue + "' and t2.Transfer_Status in ('NT','New' ) and t2.Status='1' order by t2.rollnumber asc");
            }
            else if (ddlclass.SelectedItem.Text != "ALL" && ddl_section.SelectedItem.Text != "ALL")
            {
                bind_grd_view(" select t1.*,t2.session,t2.admissionserialnumber,t2.rollnumber,t2.Section,t2.studentname,t2.class,t2.Category_id,t2.SubCategory_id,t2.fathername,t2.mobilenumber,(" + path + ") as Pathname,(" + seatname + ") as seatname,(select top 1 Amount from Transportation_Boarding_Point where TransportationPath_id=t1.TransportPath_id and Transportation_Id=t1.transport_id and Boarding_Point_id=t1.Boarding_Point_id and  Session_Id=" + ddlsession.SelectedValue + " order by Id desc) as Transport_fee,(select top 1 Boarding_Point from Transportation_Boarding_Point where TransportationPath_id=t1.TransportPath_id and Transportation_Id=t1.transport_id and Boarding_Point_id=t1.Boarding_Point_id and Session_Id=" + ddlsession.SelectedValue + " order by Id desc) as Boarding_Point,(select top 1 transport_name from Transport_Master where transport_id=t1.transport_id) as Bus_name from Student_mapping_with_TransportPath t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Admission_no=t2.admissionserialnumber and t1.Class_id=t2.Class_id and t1.branch_id=t2.Branch_id where t1.Session_id='" + ddlsession.SelectedValue + "' and t1.Class_id='" + ddlclass.SelectedValue + "' and t2.Section='" + ddl_section.SelectedItem.Text + "' and   t2.Transfer_Status in ('NT','New' ) and t2.Status='1' order by t2.rollnumber asc");
            }
            else
            {
                bind_grd_view(" select t1.*,t2.session,t2.admissionserialnumber,t2.rollnumber,t2.Section,t2.studentname,t2.class,t2.Category_id,t2.SubCategory_id,t2.fathername,t2.mobilenumber,(" + path + ") as Pathname,(" + seatname + ") as seatname,(select top 1 Amount from Transportation_Boarding_Point where TransportationPath_id=t1.TransportPath_id and Transportation_Id=t1.transport_id and Boarding_Point_id=t1.Boarding_Point_id and  Session_Id=" + ddlsession.SelectedValue + " order by Id desc) as Transport_fee,(select top 1 Boarding_Point from Transportation_Boarding_Point where TransportationPath_id=t1.TransportPath_id and Transportation_Id=t1.transport_id and Boarding_Point_id=t1.Boarding_Point_id and Session_Id=" + ddlsession.SelectedValue + " order by Id desc) as Boarding_Point,(select top 1 transport_name from Transport_Master where transport_id=t1.transport_id) as Bus_name from Student_mapping_with_TransportPath t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Admission_no=t2.admissionserialnumber and t1.Class_id=t2.Class_id and t1.branch_id=t2.Branch_id where t1.Session_id='" + ddlsession.SelectedValue + "'  and   t2.Transfer_Status in ('NT','New' ) and t2.Status='1' order by t2.rollnumber asc");
            }
        }



        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            if (ddl_monthname.SelectedItem.Text == "Select")
            {
                Alertme("Please select month.", "warning");
                ddl_monthname.Focus();
            }
            else if (ddl_path_root.SelectedItem.Text == "Please select transportation ")
            {
                Alertme("Please select transportation Distance.", "warning");
                ddl_path_root.Focus();
            }
            else
            {
                bool flag = false;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
                {
                    SqlConnection con = new SqlConnection(My.conn);
                    con.Open();
                    save_boarding_data(con);

                    dues_update_headwise_transaction.update_student_dues(ViewState["sessionIDs"].ToString(), ViewState["classid"].ToString(), ViewState["admSionNo"].ToString(), "0", "0", con);


                    flag = true;
                    con.Close();
                    scope.Complete();
                }

                if (flag == true)
                {
                    Alertme("Transportation Distance has been successfully updated.", "success");
                    hd_id.Value = "0";
                    bind_grd_view(ViewState["query"].ToString());
                }


            }
        }

        private void save_boarding_data(SqlConnection con)
        {
            string cunrt_session = payments.get_session(con);
            string session_frst_year = cunrt_session.Substring(0, 4);
            int session_s_year = My.toint(session_frst_year);
            int s_year = My.toint(session_frst_year);

            string monthid = My.tomonth_numberstring(ddl_monthname.SelectedItem.Text);
            int pay_month = My.toint(monthid);
            s_year = payments.check_start_months(pay_month, s_year,con);
            string final = s_year + monthid;


            SqlCommand cmd;
            string query = "update Student_mapping_with_TransportPath set Month_name=@Month_name,Month_id=@Month_id,TransportPath_id=@TransportPath_id,Created_by=@Created_by,Year_month=@Year_month,Change_date=@Change_date,Chnage_idate=@Chnage_idate,Change_by=@Change_by,Changed_type=@Changed_type,Old_Monthname=@Old_Monthname,Old_Transpotid=@Old_Transpotid,transport_id=@transport_id,Boarding_Point_id=@Boarding_Point_id where Id=@id; INSERT INTO Student_mapping_with_TransportPath_history (Session_id,Admission_no,Class_id,Month_name,Month_id,TransportPath_id,Remark,Created_by,Created_date,Created_idate,branch_id,Year_month,Transport_Assigned_Id,Update_Status,Remove_Update_Month) values (@Session_id,@Admission_no,@Class_id,@Month_name,@Month_id,@TransportPath_id,@Remark,@Created_by,@Created_date,@Created_idate,@branch_id,@Year_month,@Transport_Assigned_Id,@Update_Status,@Remove_Update_Month)";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Session_id", ViewState["sessionIDs"].ToString());
            cmd.Parameters.AddWithValue("@Class_id", ViewState["classid"].ToString());
            cmd.Parameters.AddWithValue("@Admission_no", ViewState["admSionNo"].ToString());
            cmd.Parameters.AddWithValue("@Month_name", ddl_monthname.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@Month_id", ddl_monthname.SelectedValue);
            cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
            cmd.Parameters.AddWithValue("@Created_date", mycode.date());
            cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
            cmd.Parameters.AddWithValue("@Remark", "Transport Changed");
            cmd.Parameters.AddWithValue("@branch_id", ViewState["branch_id"].ToString());
            cmd.Parameters.AddWithValue("@TransportPath_id", ddl_path_root.SelectedValue);

            cmd.Parameters.AddWithValue("@Year_month", final);
            cmd.Parameters.AddWithValue("@Id", hd_id.Value);
            cmd.Parameters.AddWithValue("@Transport_Assigned_Id", ViewState["Transport_Assigned_Id"].ToString());

            cmd.Parameters.AddWithValue("@transport_id", ddl_bus_name.SelectedValue);
            cmd.Parameters.AddWithValue("@Boarding_Point_id", ddl_boarding_point.SelectedValue);


            cmd.Parameters.AddWithValue("@Change_date", mycode.date());
            cmd.Parameters.AddWithValue("@Chnage_idate", mycode.idate());
            cmd.Parameters.AddWithValue("@Change_by", ViewState["Userid"].ToString());

            if (rd_change_month_no.Checked == true)
            {
                cmd.Parameters.AddWithValue("@Changed_type", "Month Changed");
                cmd.Parameters.AddWithValue("@Update_Status", "Month Changed");
            }

            else if (rd_change_month_no.Checked == true)
            {
                cmd.Parameters.AddWithValue("@Changed_type", "Transport Changed");
                cmd.Parameters.AddWithValue("@Update_Status", "Transport Changed");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Changed_type", "Both Changed");
                cmd.Parameters.AddWithValue("@Update_Status", "Both Changed");
            }
            cmd.Parameters.AddWithValue("@Old_Monthname", ViewState["olmonthname"].ToString());
            cmd.Parameters.AddWithValue("@Old_Transpotid", ViewState["oldtranportid"].ToString());
            cmd.Parameters.AddWithValue("@Remove_Update_Month", mycode.get_current_monthname());

            if (payments.InsertUpdateData(cmd, con))
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
                if (payments.InsertUpdateData(cmd1, con))
                {
                    // chek if dues 
                    string sessionname = payments.get_session_by_id(ViewState["sessionIDs"].ToString(), con);
                    DataTable dt = payments.dataTable("Select top 1 * from Typewise_fee_collection where admission_no='" + ViewState["admSionNo"].ToString() + "' and session='" + sessionname + "'    and parameter ='MonthlyFee'  and status='Dues' and   month='" + ddl_monthname.SelectedItem.Text + "' ", con);//
                    if (dt.Rows.Count != 0)
                    {
                        string class1 = dt.Rows[0]["class"].ToString();
                        string section = dt.Rows[0]["section"].ToString();
                        string feetype = "TransportationFee";
                        string month = ddl_monthname.SelectedItem.Text;
                        string user_id = ViewState["Userid"].ToString();
                        string group_id = "3";
                        string position = dt.Rows[0]["position"].ToString();
                        string class_id = dt.Rows[0]["class_id"].ToString();
                        string branchid = ViewState["branch_id"].ToString();
                        DataTable feedt = payments.dataTable(@"select 'TransportationFee' as content,tr.parameter_id as content_id,tr.Amount as amount,'Monthelyfee',tr.Ledger,
                
                
                (isnull((select top 1 disc_amount from dbo.[Discount_Master_for_bus] where admission_no='" + ViewState["admSionNo"].ToString() + "' and month='" + ddl_monthname.SelectedItem.Text + "' and session_id='" + ViewState["sessionIDs"].ToString() + "' and Bus_path='" + ddl_path_root.SelectedValue + "'),(select top 1 disc_amount from dbo.[Discount_Master_for_bus] where  admission_no='All' and month='" + ddl_monthname.SelectedItem.Text + "' and session_id='" + ViewState["sessionIDs"].ToString() + "' and Bus_path='" + ddl_path_root.SelectedValue + "'))) disc_amount from admission_registor ar join Transportation_Fee_Master tr on ar.Transportation_Id=tr.Transportation_path_id and ar.transportationtaken='yes' and ar.Session_id='" + ViewState["sessionIDs"].ToString() + "' and admissionserialnumber='" + ViewState["admSionNo"].ToString() + "' and tr.Month='" + ddl_monthname.SelectedItem.Text + "'  join Student_mapping_with_TransportPath smt on  ar.Session_id=smt.Session_id and ar.admissionserialnumber=smt.Admission_no and smt.Year_month<=" + s_year + monthid + "", con);
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
                                payments.exeSql("delete from Typewise_fee_collection where admission_no='" + ViewState["admSionNo"].ToString() + "' and session='" + sessionname + "' and parameter='MonthlyFee' and feetype='TransportationFee' and month='" + get_onemonthback + "'", con);
                                payments.exeSql(@"insert into Typewise_fee_collection(admission_no,class,session,parameter,Date,idate,feetype,payable,paid,dues,status,month,content_id,transection,
Ledger,is_readyfor_sync,is_sync,is_sync_dues_diary,Disc,section,user_id,branchid,group_id,class_id,position,Payable_after_disc) 
values ('" + ViewState["admSionNo"].ToString() + "','" + class1 + "','" + sessionname + "','MonthlyFee','" + mycode.date() + "','" + mycode.idate() + "','" + TransportationFeetype + "','" + My.toDouble(Amount).ToString("0.00") + "','0','" + My.toDouble(afterdicount).ToString("0.00") + "','Dues','" + ddl_monthname.SelectedItem.Text + "','" + content_id1 + "','','School','false','false','false','" + My.toDouble(disc_amount).ToString("0.00") + "','" + section + "','" + ViewState["Userid"].ToString() + "','" + ViewState["branch_id"].ToString() + "','" + group_id + "','" + class_id + "','" + position + "','" + afterdicount.ToString("0.00") + "')", con);
                            }
                        }
                    }
                    else
                    {
                        string get_onemonthback = mycode.get_one_monthback(monthid);
                        payments.exeSql("delete from Typewise_fee_collection where admission_no='" + ViewState["admSionNo"].ToString() + "' and session='" + sessionname + "' and parameter='MonthlyFee' and feetype='TransportationFee' and month='" + get_onemonthback + "'", con);
                    }
                }
            }
        }


        protected void btn_find_by_transportpath_Click(object sender, EventArgs e)
        {
            if (ddl_Transportation_Distance.SelectedItem.Text == "Select")
            {
                Alertme("Please select boarding point.", "warning");
                ddl_Transportation_Distance.Focus();
            }
            else
            {
                find_by_transport_path();
                ViewState["flag"] = "4";
            }
        }

        private void find_by_transport_path()
        {
            bind_grd_view("select t1.*,t2.session,t2.admissionserialnumber,t2.rollnumber,t2.Section,t2.studentname,t2.class,t2.Category_id,t2.SubCategory_id,t2.fathername,t2.mobilenumber,(" + path + ") as Pathname,(" + seatname + ") as seatname,(select top 1 Amount from Transportation_Boarding_Point where TransportationPath_id=t1.TransportPath_id and Transportation_Id=t1.transport_id and Boarding_Point_id=t1.Boarding_Point_id and  Session_Id=" + ddlsession.SelectedValue + " order by Id desc) as Transport_fee,(select top 1 Boarding_Point from Transportation_Boarding_Point where TransportationPath_id=t1.TransportPath_id and Transportation_Id=t1.transport_id and Boarding_Point_id=t1.Boarding_Point_id and Session_Id=" + ddlsession.SelectedValue + " order by Id desc) as Boarding_Point,(select top 1 transport_name from Transport_Master where transport_id=t1.transport_id) as Bus_name from Student_mapping_with_TransportPath t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Admission_no=t2.admissionserialnumber and t1.Class_id=t2.Class_id and t1.branch_id=t2.Branch_id where t1.Session_id='" + ddlsession.SelectedValue + "'  and   t2.Transfer_Status in ('NT','New' ) and t2.Status='1' and t1.Boarding_Point_id='" + ddl_Transportation_Distance.SelectedValue + "' order by t2.rollnumber asc");
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
                        pnl_view.RenderControl(hw);
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

        protected void btn_find_by_vehicle_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_vehicle.SelectedItem.Text == "Select")
                {
                    Alertme("Please select Vehicle.", "warning");
                    ddl_vehicle.Focus();
                }
                else
                {
                    find_by_vehicle();
                    ViewState["flag"] = "5";
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_by_vehicle()
        {
            bind_grd_view(" select t1.*,t2.session,t2.admissionserialnumber,t2.rollnumber,t2.Section,t2.studentname,t2.class,t2.Category_id,t2.SubCategory_id,t2.fathername,t2.mobilenumber,(" + path + ") as Pathname,(" + seatname + ") as seatname,(select top 1 Amount from Transportation_Boarding_Point where TransportationPath_id=t1.TransportPath_id and Transportation_Id=t1.transport_id and Boarding_Point_id=t1.Boarding_Point_id and  Session_Id=" + ddlsession.SelectedValue + " order by Id desc) as Transport_fee,(select top 1 Boarding_Point from Transportation_Boarding_Point where TransportationPath_id=t1.TransportPath_id and Transportation_Id=t1.transport_id and Boarding_Point_id=t1.Boarding_Point_id and Session_Id=" + ddlsession.SelectedValue + " order by Id desc) as Boarding_Point,(select top 1 transport_name from Transport_Master where transport_id=t1.transport_id) as Bus_name from Student_mapping_with_TransportPath t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Admission_no=t2.admissionserialnumber and t1.Class_id=t2.Class_id and t1.branch_id=t2.Branch_id where t1.Session_id='" + ddlsession.SelectedValue + "'  and   t2.Transfer_Status in ('NT','New' ) and t2.Status='1' and t1.transport_id='" + ddl_vehicle.SelectedValue + "' order by t2.rollnumber asc");
        }

        protected void btn_find_by_adm_no_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session.", "warning");
                    ddlsession.Focus();
                }
                else if (txt_admission_no.Text == "")
                {
                    Alertme("Please enter admission no.", "warning");
                    txt_admission_no.Focus();
                }
                else
                {
                    find_by_admission_no();
                    ViewState["flag"] = "6";
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_by_admission_no()
        {
            bind_grd_view("select t1.*,t2.session,t2.admissionserialnumber,t2.rollnumber,t2.Section,t2.studentname,t2.class,t2.Category_id,t2.SubCategory_id,t2.fathername,t2.mobilenumber,(" + path + ") as Pathname,(" + seatname + ") as seatname,(select top 1 Amount from Transportation_Boarding_Point where TransportationPath_id=t1.TransportPath_id and Transportation_Id=t1.transport_id and Boarding_Point_id=t1.Boarding_Point_id and  Session_Id=" + ddlsession.SelectedValue + " order by Id desc) as Transport_fee,(select top 1 Boarding_Point from Transportation_Boarding_Point where TransportationPath_id=t1.TransportPath_id and Transportation_Id=t1.transport_id and Boarding_Point_id=t1.Boarding_Point_id and Session_Id=" + ddlsession.SelectedValue + " order by Id desc) as Boarding_Point,(select top 1 transport_name from Transport_Master where transport_id=t1.transport_id) as Bus_name from Student_mapping_with_TransportPath t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Admission_no=t2.admissionserialnumber and t1.Class_id=t2.Class_id and t1.branch_id=t2.Branch_id where t1.Session_id='" + ddlsession.SelectedValue + "'  and   t2.Transfer_Status in ('NT','New' ) and t2.Status='1' and t2.admissionserialnumber='" + txt_admission_no.Text + "' order by t2.rollnumber asc");

        }



        protected void lnk_change_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_admissionserialnumber = (Label)row.FindControl("lbl_admissionserialnumber");
                Label lbl_class_id = (Label)row.FindControl("lbl_class_id");
                Label lbl_month_id = (Label)row.FindControl("lbl_month_id");
                Label lbl_session_id = (Label)row.FindControl("lbl_session_id");
                Label lbl_TransportPath_id = (Label)row.FindControl("lbl_TransportPath_id");
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                Label lbl_Category_id = (Label)row.FindControl("lbl_Category_id");
                Label lbl_SubCategory_id = (Label)row.FindControl("lbl_SubCategory_id");
                Label lbl_monthname = (Label)row.FindControl("lbl_monthname");
                Label lbl_Transport_Assigned_Id = (Label)row.FindControl("lbl_Transport_Assigned_Id");
                Label lbl_transport_id = (Label)row.FindControl("lbl_transport_id");
                Label lbl_boarding_point_id = (Label)row.FindControl("lbl_boarding_point_id");
                hd_Category_id.Value = lbl_Category_id.Text;
                hd_SubCategory_id.Value = lbl_SubCategory_id.Text;
                ViewState["Transport_Assigned_Id"] = lbl_Transport_Assigned_Id.Text;
                ViewState["sessionIDs"] = lbl_session_id.Text;
                ViewState["classid"] = lbl_class_id.Text;
                ViewState["oldtranportid"] = lbl_TransportPath_id.Text;
                ViewState["olmonthname"] = lbl_monthname.Text;



                mycode.bind_all_ddl_with_id(ddl_bus_name, "select transport_name+'-{BUS NO-'+Bus_no+'}',transport_id from  Transport_Master order by transport_name");


                btn_cancel.Visible = true;
                btn_Submit.Text = "Update";
                hd_id.Value = lbl_Id.Text;
                ViewState["admSionNo"] = lbl_admissionserialnumber.Text;


                ddl_bus_name.SelectedValue = lbl_transport_id.Text;
                fetch_path_root();
                ddl_path_root.SelectedValue = lbl_TransportPath_id.Text;
                fetch_boarding_point();
                ddl_boarding_point.SelectedValue = lbl_boarding_point_id.Text;

                ddl_monthname.SelectedValue = lbl_month_id.Text;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalEdit();", true);
            }
            catch
            {
            }
        }

        protected void ddl_bus_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalEdit();", true);
            if (ddl_bus_name.SelectedItem.Text == "Select")
            {
                Alertme("Please select bus name", "warning");
            }
            else
            {
                fetch_path_root();
            }
        }

        private void fetch_path_root()
        {
            mycode.bind_all_ddl_with_id(ddl_path_root, "select Rootname,TransportationPath_id from TransportationPath where Transportation_Id=" + ddl_bus_name.SelectedValue + " order by Rootname asc");
        }

        protected void ddl_path_root_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalEdit();", true);
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
                fetch_boarding_point();
            }
        }

        private void fetch_boarding_point()
        {
            mycode.bind_all_ddl_with_id(ddl_boarding_point, " select  Boarding_Point,Boarding_Point_id from  Transportation_Boarding_Point where Transportation_Id=" + ddl_bus_name.SelectedValue + " and TransportationPath_id=" + ddl_path_root.SelectedValue + "  and Session_Id='" + ddlsession.SelectedValue + "' order by Boarding_Point");

            string ttl_vacant_seat = get_total_vacant_seat(ddl_bus_name.SelectedValue, ddl_path_root.SelectedValue);
            ViewState["VacantSeat"] = ttl_vacant_seat;
        }
        private string get_total_vacant_seat(string Transportation_Id, string Path_id)
        {
            DataTable dt = My.dataTable("select count(Id) as Total_vacant from dbo.[TRANSPORT_PATH_MAPPING_WITH_SHEET] where TransportationPath_id='" + Path_id + "' and Transportation_Id='" + Transportation_Id + "' and Sheet_Status='0'");
            return dt.Rows[0][0].ToString();
        }

        protected void ddlsession_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                get_boarding_point();
            }
            catch (Exception ex)
            {
            }
        }
    }
}