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
    public partial class Set_Boarding_Point_Wise_Fee : System.Web.UI.Page
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
                        ViewState["firm_id"] = My.get_firm_id();
                        ViewState["session_id"] = My.get_session_id();
                        ViewState["session"] = My.get_session();
                        ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        mycode.bind_all_ddl_with_id(ddl_vechile_name, "select transport_name+'-{BUS NO-'+Bus_no+'}',transport_id from  Transport_Master order by transport_name");



                        mycode.bind_all_ddl_with_id(ddl_distance_km, "select Start_KM+' Km To '+cast(End_KM as varchar(50))+' Km',Distance_id from  Transport_distance_Meter order by End_KM");
                        mycode.bind_all_ddl_with_id(ddl_session, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int)");
                        ddl_session.SelectedValue = My.get_session_id();


                        string pagename_current = "Set_Boarding_Point_Wise_Fee.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];

                        mycode.bind_all_ddl_with_id(ddl_current_session, "Select Session,session_id from session_details  order by cast((Substring (Session,1,4)) as int) ");

                        mycode.bind_all_ddl_with_id(ddl_copy_to_session, "Select Session,session_id from session_details  order by cast((Substring (Session,1,4)) as int) ");


                    }
                    // BindDetails();
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Set_Boarding_Point_With_Route");
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


        protected void ddl_vechile_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_vechile_name.SelectedItem.Text == "Select")
            {
                Alertme("Please select Vehicle name", "warning");
            }
            else
            {
                Bind_bus_no();
                mycode.bind_all_ddl_with_id(ddl_Vehicle_Raute, "select Rootname,TransportationPath_id from  TransportationPath where Transportation_Id=" + ddl_vechile_name.SelectedValue + " order by Rootname");
            }
        }

        private void Bind_bus_no()
        {
            DataTable cdt = My.dataTable(" select Bus_no from dbo.[Transport_Master] where transport_id='" + ddl_vechile_name.SelectedValue + "'");
            int rowcount = cdt.Rows.Count;
            if (rowcount == 0)
            {
                txt_vehicle_no.Text = "";
            }
            else
            {
                txt_vehicle_no.Text = cdt.Rows[0]["Bus_no"].ToString();
            }
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            SqlCommand cmd;
            try
            {
                if (ddl_vechile_name.SelectedItem.Text == "Select")
                {
                    Alertme("Please select Vehicle name", "warning");
                }
                else if (ddl_Vehicle_Raute.SelectedItem.Text == "Select")
                {
                    Alertme("Please select Vehicle rute", "warning");
                }
                else if (ddl_distance_km.SelectedItem.Text == "Select")
                {
                    Alertme("Please select Vehicle rute", "warning");
                }
                else
                {
                    if (btn_add_boarding_point.Text == "Add")
                    {
                        if (ViewState["Is_add"].ToString() == "1")
                        {
                            DataTable class_dt = My.dataTable("select Boarding_Point_id from dbo.[Transportation_Boarding_Point] where Boarding_Point='" + txt_boardingpoint.Text + "' and Transportation_Id=" + ddl_vechile_name.SelectedValue + " and TransportationPath_id=" + ddl_Vehicle_Raute.SelectedValue + " and Session_Id=" + ddl_session.SelectedValue + " ");
                            if (class_dt.Rows.Count == 0)
                            {
                                string Boarding_Point_id = create_sl_no1();
                                string query = "INSERT INTO Transportation_Boarding_Point (Boarding_Point,KM,Amount,Transportation_Id,TransportationPath_id,Created_by,Created_Date_time,Boarding_Point_id,Distance_id,Session_Id) values (@Boarding_Point,@KM,@Amount,@Transportation_Id,@TransportationPath_id,@Created_by,@Created_Date_time,@Boarding_Point_id,@Distance_id,@Session_Id)";
                                cmd = new SqlCommand(query);
                                cmd.Parameters.AddWithValue("@Boarding_Point", txt_boardingpoint.Text);
                                cmd.Parameters.AddWithValue("@KM", ddl_distance_km.SelectedItem.Text);
                                cmd.Parameters.AddWithValue("@Amount", My.toDouble(txt_transportfee.Text).ToString("0.00"));
                                cmd.Parameters.AddWithValue("@Transportation_Id", ddl_vechile_name.SelectedValue);
                                cmd.Parameters.AddWithValue("@TransportationPath_id", ddl_Vehicle_Raute.SelectedValue);
                                cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                                cmd.Parameters.AddWithValue("@Created_Date_time", My.getdate1());
                                cmd.Parameters.AddWithValue("@Boarding_Point_id", Boarding_Point_id);
                                cmd.Parameters.AddWithValue("@Distance_id", ddl_distance_km.SelectedValue);
                                cmd.Parameters.AddWithValue("@Session_Id", ddl_session.SelectedValue);

                                if (InsertUpdate.InsertUpdateData(cmd))
                                {
                                    entryTransportation_Fee_Master(Boarding_Point_id, txt_transportfee.Text, ddl_session.SelectedValue, ddl_session.SelectedItem.Text, ddl_vechile_name.SelectedValue, ddl_Vehicle_Raute.SelectedValue);
                                    Alertme("Boarding point fee created Successfully", "success");
                                    My.send_data_to_user_log_history(ViewState["Userid"].ToString(), ViewState["Userid"].ToString() + " created a boarding point  " + txt_boardingpoint.Text + " boardingpointid" + Boarding_Point_id);
                                    empty_data();
                                    Bind_grid();
                                }

                            }
                            else
                            {
                                Alertme("Sorry you entered boarding point already added", "warning");
                            }

                        }
                        else
                        {
                            Alertme(My.get_restricted_message(), "warning");
                        }
                    }
                    else
                    {

                        if (ViewState["Is_Edit"].ToString() == "1")
                        {
                            bool chek_fee = My.find_mnthly_transp_fee_collected_N(ddl_session.SelectedValue, ddl_vechile_name.SelectedValue, ddl_Vehicle_Raute.SelectedValue, HdID.Value);
                            if (chek_fee == false)
                            {
                                Alertme("You can't add/update fee  because fee has been taken.", "warning");
                                return;
                            }
                            else
                            {
                                DataTable class_dt = My.dataTable("select Boarding_Point_id from dbo.[Transportation_Boarding_Point] where Boarding_Point='" + txt_boardingpoint.Text + "' and Transportation_Id=" + ddl_vechile_name.SelectedValue + " and TransportationPath_id=" + ddl_Vehicle_Raute.SelectedValue + " and Boarding_Point_id!=" + HdID.Value + " and Session_Id='" + ddl_session.SelectedValue + "' ");
                                if (class_dt.Rows.Count == 0)
                                {
                                    string query = "Update Transportation_Boarding_Point set Boarding_Point=@Boarding_Point,KM=@KM,Amount=@Amount,Transportation_Id=@Transportation_Id,TransportationPath_id=@TransportationPath_id,Updated_by=@Updated_by,Updated_date=@Updated_date,Distance_id=@Distance_id  where Boarding_Point_id=@Boarding_Point_id and Session_Id=@Session_Id";
                                    cmd = new SqlCommand(query);
                                    cmd.Parameters.AddWithValue("@Boarding_Point", txt_boardingpoint.Text);
                                    cmd.Parameters.AddWithValue("@KM", ddl_distance_km.SelectedItem.Text);
                                    cmd.Parameters.AddWithValue("@Amount", My.toDouble(txt_transportfee.Text).ToString("0.00"));
                                    cmd.Parameters.AddWithValue("@Transportation_Id", ddl_vechile_name.SelectedValue);
                                    cmd.Parameters.AddWithValue("@TransportationPath_id", ddl_Vehicle_Raute.SelectedValue);
                                    cmd.Parameters.AddWithValue("@Updated_by", ViewState["Userid"].ToString());
                                    cmd.Parameters.AddWithValue("@Updated_date", My.getdate1());
                                    cmd.Parameters.AddWithValue("@Boarding_Point_id", HdID.Value);
                                    cmd.Parameters.AddWithValue("@Distance_id", ddl_distance_km.SelectedValue);
                                    cmd.Parameters.AddWithValue("@Session_Id", ddl_session.SelectedValue);
                                    if (InsertUpdate.InsertUpdateData(cmd))
                                    {
                                        entryTransportation_Fee_Master(HdID.Value, txt_transportfee.Text, ddl_session.SelectedValue, ddl_session.SelectedItem.Text, ddl_vechile_name.SelectedValue, ddl_Vehicle_Raute.SelectedValue);
                                        Alertme("Boarding point fee updated Successfully", "success");
                                        My.send_data_to_user_log_history(ViewState["Userid"].ToString(), ViewState["Userid"].ToString() + " update a boarding point  " + txt_boardingpoint.Text + " boardingpointid" + HdID.Value);
                                        empty_data();
                                        Bind_grid();
                                    }
                                }
                            }
                        }
                        else
                        {
                            Alertme(My.get_restricted_message(), "warning");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void entryTransportation_Fee_Master(string boarding_Point_id, string amount, string sessionid, string session, string transportation_Id, string transportationPath_id)
        {
            SqlCommand cmd;
            DataTable dt = mycode.FillData(" select Month,Month_Id   from dbo.[Month_Index] order by Position");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string Monthname = dt.Rows[i]["Month"].ToString();
                    string Month_Id = dt.Rows[i]["Month_Id"].ToString();

                    DataTable class_dt = My.dataTable("select Boarding_Point_id,Id from dbo.[Transportation_Fee_Master] where Boarding_Point_id='" + boarding_Point_id + "' and Transportation_path_id=" + transportationPath_id + "   and session_id='" + sessionid + "' and Month_id=" + Month_Id + "");
                    if (class_dt.Rows.Count == 0)
                    {
                        string query = "INSERT INTO Transportation_Fee_Master (Session,Parameter,Amount,session_id,parameter_id,Type,User_id,Date,time,Ledger,Month,Month_id,Transportation_path_id,Boarding_Point_id) values (@Session,@Parameter,@Amount,@session_id,@parameter_id,@Type,@User_id,@Date,@time,@Ledger,@Month,@Month_id,@Transportation_path_id,@Boarding_Point_id)";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@Session", session);
                        cmd.Parameters.AddWithValue("@Parameter", "TransportFee");
                        cmd.Parameters.AddWithValue("@Amount", My.toDouble(amount).ToString("0.00"));
                        cmd.Parameters.AddWithValue("@session_id", sessionid);
                        cmd.Parameters.AddWithValue("@parameter_id", "1002");
                        cmd.Parameters.AddWithValue("@Type", "Monthwise");
                        cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                        cmd.Parameters.AddWithValue("@Date", mycode.date());
                        cmd.Parameters.AddWithValue("@time", mycode.time());
                        cmd.Parameters.AddWithValue("@Ledger", "School");
                        cmd.Parameters.AddWithValue("@Month", Monthname);
                        cmd.Parameters.AddWithValue("@Month_id", Month_Id);
                        cmd.Parameters.AddWithValue("@Transportation_path_id", transportationPath_id);
                        cmd.Parameters.AddWithValue("@Boarding_Point_id", boarding_Point_id);
                        if (InsertUpdate.InsertUpdateData(cmd))
                        {
                        }
                    }
                    else
                    {
                        string id = class_dt.Rows[0]["Id"].ToString();
                        string query = "update Transportation_Fee_Master set Amount=@Amount,Date=@Date,time=@time where Id=@Id";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@Amount", My.toDouble(txt_transportfee.Text).ToString("0.00"));
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.Parameters.AddWithValue("@Date", mycode.date());
                        cmd.Parameters.AddWithValue("@time", mycode.time());
                        if (InsertUpdate.InsertUpdateData(cmd))
                        {
                        }
                    }
                }
            }
        }

        private void Bind_grid()
        {
            string query = "Select * from Transportation_Boarding_Point where Transportation_Id=" + ddl_vechile_name.SelectedValue + " and TransportationPath_id=" + ddl_Vehicle_Raute.SelectedValue + " and Session_Id=" + ddl_session.SelectedValue + " order by KM asc";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                btn_excels.Visible = false;
                GrdView.DataSource = null;
                GrdView.DataBind();
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
            else
            {
                btn_excels.Visible = true;
                GrdView.DataSource = dt;
                GrdView.DataBind();

                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }

        private void empty_data()
        {
            txt_boardingpoint.Text = "";
            txt_transportfee.Text = "";
            ddl_distance_km.SelectedValue = "0";
            btn_add_boarding_point.Text = "Add";
        }

        private string create_sl_no1()
        {
            bool duplicate = true;
            string Boarding_Point_id = mycode.auto_serial("Boarding_Point_id");
            while (duplicate)
            {
                DataTable cdt = My.dataTable(" select Boarding_Point_id from dbo.[Transportation_Boarding_Point] where Boarding_Point_id='" + Boarding_Point_id + "'");
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    duplicate = false;
                }
                else
                {
                    duplicate = true;
                    Boarding_Point_id = mycode.auto_serial("Boarding_Point_id");
                }
            }
            return Boarding_Point_id;
        }

        protected void ddl_Vehicle_Raute_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_vechile_name.SelectedItem.Text == "Select")
            {
                Alertme("Please select Vehicle name", "warning");
            }
            else if (ddl_Vehicle_Raute.SelectedItem.Text == "Select")
            {
                Alertme("Please select Vehicle rute", "warning");
            }
            else
            {
                Bind_grid();
            }
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    GridViewRow row = (GridViewRow)lnk.Parent.Parent;
                    Label lbl_Boarding_Point = (Label)row.FindControl("lbl_Boarding_Point");
                    Label lbl_Boarding_Point_id = (Label)row.FindControl("lbl_Boarding_Point_id");
                    Label lbl_km_id_Distance_id = (Label)row.FindControl("lbl_km_id_Distance_id");
                    Label lbl_transportfee = (Label)row.FindControl("lbl_transportfee");
                    HdID.Value = lbl_Boarding_Point_id.Text;
                    Label lbl_TransportationPath_id = (Label)row.FindControl("lbl_TransportationPath_id");
                    Label lbl_Transportation_Id = (Label)row.FindControl("lbl_Transportation_Id");
                    Label lbl_Session_Id = (Label)row.FindControl("lbl_Session_Id");
                    bool chek_fee = My.find_mnthly_transp_fee_collected_N(lbl_Session_Id.Text, lbl_Transportation_Id.Text, lbl_TransportationPath_id.Text, lbl_Boarding_Point_id.Text);
                    if (chek_fee == false)
                    {
                        Alertme("You can't edit fee  because fee has been taken.", "warning");
                        return;
                    }
                    else
                    {
                        ddl_session.SelectedValue = lbl_Session_Id.Text;
                        ddl_vechile_name.SelectedValue = lbl_Transportation_Id.Text;
                        ddl_Vehicle_Raute.SelectedValue = lbl_TransportationPath_id.Text;
                        txt_boardingpoint.Text = lbl_Boarding_Point.Text;
                        ddl_distance_km.SelectedValue = lbl_km_id_Distance_id.Text;
                        txt_transportfee.Text = lbl_transportfee.Text;
                        btn_add_boarding_point.Text = "Update";
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

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_delete"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    GridViewRow row = (GridViewRow)lnk.Parent.Parent;
                    Label lbl_Boarding_Point_id = (Label)row.FindControl("lbl_Boarding_Point_id");
                    Label lbl_Boarding_Point = (Label)row.FindControl("lbl_Boarding_Point");
                    Label lbl_Transportation_Id = (Label)row.FindControl("lbl_Transportation_Id");
                    Label lbl_TransportationPath_id = (Label)row.FindControl("lbl_TransportationPath_id");
                    Label lbl_Session_Id = (Label)row.FindControl("lbl_Session_Id");
                    bool chek_fee = My.check_assigned_boarding_point(lbl_Session_Id.Text, lbl_Transportation_Id.Text, lbl_TransportationPath_id.Text, lbl_Boarding_Point_id.Text);
                    if (chek_fee == false)
                    {
                        Alertme("You can't delete because the boarding point has been assigned student.", "warning");
                        return;


                    }
                    else
                    {
                        mycode.executequery("delete from Transportation_Boarding_Point where Boarding_Point_id=" + lbl_Boarding_Point_id.Text + " and Session_Id=" + lbl_Session_Id.Text + " and Transportation_Id=" + lbl_Transportation_Id.Text + " and TransportationPath_id=" + lbl_TransportationPath_id.Text + "");

                        mycode.executequery("delete from Transportation_Fee_Master where Boarding_Point_id=" + lbl_Boarding_Point_id.Text + " and session_id=" + lbl_Session_Id.Text + " and Transportation_path_id=" + lbl_TransportationPath_id.Text + "");

                        string msg = ViewState["Userid"].ToString() + " Delete boarding point boarding point:-" + lbl_Boarding_Point.Text + "  Boarding_Point id" + lbl_Boarding_Point_id.Text + " User id=" + ViewState["Userid"].ToString() + " Name=" + mycode.get_user(ViewState["Userid"].ToString()) + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MM/yyyy hh:mm:ss tt");
                        mycode.insert_data_logfile(ViewState["Userid"].ToString(), "1", msg, ViewState["branchid"].ToString());
                        Bind_grid();
                        Alertme("Boarding point has been Successfully deleted", "success");

                        empty_data();
                    }
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            catch (Exception ex)
            {
            }
        }


        #region copy boarding fee for next year
        protected void btn_copy_fee_for_boarding_Click1(object sender, EventArgs e)
        {
            if (ViewState["Is_add"].ToString() == "1")
            {

                copy_fee_for_boarding();
            }
            else if (ViewState["Is_Edit"].ToString() == "1")
            {
                copy_fee_for_boarding();
            }

            else
            {
                Alertme(My.get_restricted_message(), "warning");
            }
        }

        private void copy_fee_for_boarding()
        {
            try
            {
                if (ddl_current_session.Text == "Select")
                {
                    Alertme("Please Select Copy From Session", "warning");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);
                }
                else if (ddl_copy_to_session.SelectedItem.Text == "Select")
                {
                    Alertme("Please Select Copy From Session", "warning");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);
                }
                else
                {
                    bool check_fee_added = My.get_fee_added_boarding(ddl_current_session.SelectedValue);
                    if (check_fee_added == false)
                    {
                        Alertme("Apologies, the chosen session does not cover the boarding point fee, So it cannot be copied", "warning");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);
                    }
                    else
                    {
                        SqlCommand cmd;
                        string query = "INSERT INTO Copy_Fee_session_Wise_Log_history (User_Id,Copy_Session_From,Copy_Session_To,Copy_Type,Copy_data_date_time) values (@User_Id,@Copy_Session_From,@Copy_Session_To,@Copy_Type,@Copy_data_date_time)";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@User_Id", ViewState["Userid"].ToString());
                        cmd.Parameters.AddWithValue("@Copy_Session_From", ddl_current_session.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@Copy_Session_To", ddl_copy_to_session.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@Copy_Type", "Transports Boarding Point");
                        cmd.Parameters.AddWithValue("@Copy_data_date_time", My.getdate1());
                        if (My.InsertUpdateData(cmd))
                        {
                            //fee copy content_wise
                            string query1 = " select * from dbo.[Transportation_Boarding_Point] where  Session_Id='" + ddl_current_session.SelectedValue + "'";
                            DataTable dt = mycode.FillData(query1);
                            if (dt.Rows.Count == 0)
                            {

                            }
                            else
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {

                                    string Boarding_Point = dt.Rows[i]["Boarding_Point"].ToString();
                                    string KM = dt.Rows[i]["KM"].ToString();
                                    string Transportation_Id = dt.Rows[i]["Transportation_Id"].ToString();
                                    string TransportationPath_id = dt.Rows[i]["TransportationPath_id"].ToString();
                                    string Boarding_Point_id = dt.Rows[i]["Boarding_Point_id"].ToString();
                                    string Distance_id = dt.Rows[i]["Distance_id"].ToString();
                                    string Amount = dt.Rows[i]["Amount"].ToString();
                                    copy_data_new_session_Transportation_Boarding_Point(Boarding_Point, KM, Transportation_Id, TransportationPath_id, Boarding_Point_id, Distance_id, Amount);
                                }
                            }
                        }
                        Alertme("Your boarding point fees has been copied successfully done", "success");
                    }
                }

            }
            catch (Exception ex)
            {
                My.submitException(ex, "Copy hostel admission fee Master day");

            }
        }

        private void copy_data_new_session_Transportation_Boarding_Point(string boarding_Point, string kM, string transportation_Id, string transportationPath_id, string boarding_Point_id, string distance_id, string Amount)
        {
            SqlCommand cmd;
            DataTable class_dt = My.dataTable("select Boarding_Point_id from dbo.[Transportation_Boarding_Point] where boarding_Point_id='" + boarding_Point_id + "' and Transportation_Id=" + transportation_Id + " and TransportationPath_id=" + transportationPath_id + " and Session_Id=" + ddl_copy_to_session.SelectedValue + " ");
            if (class_dt.Rows.Count == 0)
            {
                string query = "INSERT INTO Transportation_Boarding_Point (Boarding_Point,KM,Amount,Transportation_Id,TransportationPath_id,Created_by,Created_Date_time,Boarding_Point_id,Distance_id,Session_Id) values (@Boarding_Point,@KM,@Amount,@Transportation_Id,@TransportationPath_id,@Created_by,@Created_Date_time,@Boarding_Point_id,@Distance_id,@Session_Id)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Boarding_Point", boarding_Point);
                cmd.Parameters.AddWithValue("@KM", kM);
                cmd.Parameters.AddWithValue("@Amount", My.toDouble(Amount).ToString("0.00"));
                cmd.Parameters.AddWithValue("@Transportation_Id", transportation_Id);
                cmd.Parameters.AddWithValue("@TransportationPath_id", transportationPath_id);
                cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Created_Date_time", My.getdate1());
                cmd.Parameters.AddWithValue("@Boarding_Point_id", boarding_Point_id);
                cmd.Parameters.AddWithValue("@Distance_id", distance_id);
                cmd.Parameters.AddWithValue("@Session_Id", ddl_copy_to_session.SelectedValue);
                if (InsertUpdate.InsertUpdateData(cmd))
                {
                    entryTransportation_Fee_Master(boarding_Point_id, Amount, ddl_copy_to_session.SelectedValue, ddl_copy_to_session.SelectedItem.Text, transportation_Id, transportationPath_id);
                }

            }
            else
            {

            }
        }
        #endregion


        #region export pboarding_point
        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Download"].ToString() == "1")
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=ExportBoardingpoint.xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    using (StringWriter sw = new StringWriter())
                    {
                        HtmlTextWriter hw = new HtmlTextWriter(sw);
                        GridView1.RenderControl(hw);
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
        #endregion

        protected void lnk_change_vehicle_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    GridViewRow row = (GridViewRow)lnk.Parent.Parent;
                    Label lbl_Boarding_Point = (Label)row.FindControl("lbl_Boarding_Point");
                    Label lbl_Boarding_Point_id = (Label)row.FindControl("lbl_Boarding_Point_id");
                    Label lbl_km_id_Distance_id = (Label)row.FindControl("lbl_km_id_Distance_id");
                    Label lbl_transportfee = (Label)row.FindControl("lbl_transportfee");
                    Label lbl_TransportationPath_id = (Label)row.FindControl("lbl_TransportationPath_id");
                    Label lbl_Transportation_Id = (Label)row.FindControl("lbl_Transportation_Id");
                    Label lbl_Session_Id = (Label)row.FindControl("lbl_Session_Id");

                    ViewState["vehicle_id"] = lbl_Transportation_Id.Text;
                    ViewState["boardingpointId"] = lbl_Boarding_Point_id.Text;
                    ViewState["sessionId"] = lbl_Session_Id.Text;
                    ViewState["sessions"] = My.get_single_column_data("select top 1 Session as Column_Name from session_details where session_id='" + lbl_Session_Id.Text + "'");
                    ViewState["transportFee"] = lbl_transportfee.Text;


                    txt_boarding_point_update.Text = lbl_Boarding_Point.Text;
                    mycode.bind_all_ddl_with_id(ddl_vehicle_update, "select transport_name+'-{BUS NO-'+Bus_no+'}',transport_id from  Transport_Master order by transport_name");
                    mycode.bind_all_ddl_with_id(ddl_distance_km, "select Start_KM+' Km To '+cast(End_KM as varchar(50))+' Km',Distance_id from  Transport_distance_Meter order by End_KM");
                    ddl_vehicle_update.SelectedValue = lbl_Transportation_Id.Text;
                    fetch_vehicle_route_updt();
                    ddl_route_update.SelectedValue = lbl_TransportationPath_id.Text;
                    mycode.bind_all_ddl_with_id(ddl_distance_km_update, "select Start_KM+' Km To '+cast(End_KM as varchar(50))+' Km',Distance_id from  Transport_distance_Meter order by End_KM");
                    ddl_distance_km_update.SelectedValue = lbl_km_id_Distance_id.Text;

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalVehicle();", true);
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

        private void fetch_vehicle_route_updt()
        {
            Bind_bus_no_update();
            mycode.bind_all_ddl_with_id(ddl_route_update, "select Rootname,TransportationPath_id from  TransportationPath where Transportation_Id=" + ddl_vehicle_update.SelectedValue + " order by Rootname");
        }

        private void Bind_bus_no_update()
        {
            txt_vehicle_no.Text = "";
            DataTable cdt = My.dataTable("select Bus_no from Transport_Master where transport_id='" + ddl_vehicle_update.SelectedValue + "'");
            int rowcount = cdt.Rows.Count;
            if (rowcount > 0)
            {
                txt_vehicle_no_update.Text = cdt.Rows[0]["Bus_no"].ToString();
            }
        }

        protected void ddl_vehicle_update_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalVehicle();", true);
                fetch_vehicle_route_updt();
            }
            catch (Exception ex)
            {
            }
        }

        protected void btn_change_vehicle_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_vehicle_update.SelectedItem.Text == "Select")
                {
                    Alertme("Please choose vehicle.", "warning");
                    ddl_vehicle_update.Focus();
                }
                else if (ddl_route_update.SelectedItem.Text == "Select")
                {
                    Alertme("Please choose route.", "warning");
                    ddl_route_update.Focus();
                }
                else
                {
                    update_vehicle_boarding_point();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void update_vehicle_boarding_point()
        {
            SqlCommand cmd;
            string query = "Update Transportation_Boarding_Point set Boarding_Point=@Boarding_Point,KM=@KM,Transportation_Id=@Transportation_Id,TransportationPath_id=@TransportationPath_id,Updated_by=@Updated_by,Updated_date=@Updated_date,Distance_id=@Distance_id where Boarding_Point_id=@Boarding_Point_id and Session_Id=@Session_Id";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Boarding_Point", txt_boarding_point_update.Text);
            cmd.Parameters.AddWithValue("@KM", ddl_distance_km_update.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@Transportation_Id", ddl_vehicle_update.SelectedValue);
            cmd.Parameters.AddWithValue("@TransportationPath_id", ddl_route_update.SelectedValue);
            cmd.Parameters.AddWithValue("@Updated_by", ViewState["Userid"].ToString());
            cmd.Parameters.AddWithValue("@Updated_date", My.getdate1());
            cmd.Parameters.AddWithValue("@Boarding_Point_id", ViewState["boardingpointId"].ToString());
            cmd.Parameters.AddWithValue("@Distance_id", ddl_distance_km_update.SelectedValue);
            cmd.Parameters.AddWithValue("@Session_Id", ViewState["sessionId"].ToString());
            if (InsertUpdate.InsertUpdateData(cmd))
            {
                update_TFee_Master(ViewState["boardingpointId"].ToString(), ViewState["transportFee"].ToString(), ViewState["sessionId"].ToString(), ViewState["sessions"].ToString(), ddl_vehicle_update.SelectedValue, ddl_route_update.SelectedValue);
                update_student();

                Alertme("Vehicle updated to selected boarding poing.", "success");
                My.send_data_to_user_log_history(ViewState["Userid"].ToString(), ViewState["Userid"].ToString() + "Treansfer a boarding point to new vehicle " + ddl_vehicle_update.SelectedItem.Text + " boardingpointid" + ViewState["boardingpointId"].ToString());
                txt_boarding_point_update.Text = "";
                txt_vehicle_no_update.Text = "";
                Bind_grid();
            }
        }

        private void update_student()
        {
            DataTable dt = My.dataTable("select t1.* from Student_mapping_with_TransportPath t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Admission_no=t2.admissionserialnumber and t1.Class_id=t2.Class_id and t1.branch_id=t2.Branch_id where t1.Session_id='" + ViewState["sessionId"].ToString() + "' and t2.Transfer_Status in ('NT','New') and t2.Status='1' and t1.Boarding_Point_id='" + ViewState["boardingpointId"].ToString() + "' order by t2.rollnumber asc");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    My.exeSql("update Student_mapping_with_TransportPath set transport_id='" + ddl_vehicle_update.SelectedValue + "',TransportPath_id='" + ddl_route_update.SelectedValue + "',Boarding_Point_id='" + ViewState["boardingpointId"].ToString() + "' where Id='" + dr["Id"].ToString() + "'; update admission_registor set Transportation_Id='" + ddl_vehicle_update.SelectedValue + "',Transportationpath='" + ddl_route_update.SelectedValue + "' where Session_id='" + dr["Session_id"].ToString() + "' and Class_id='" + dr["Class_id"].ToString() + "' and admissionserialnumber='" + dr["Admission_no"].ToString() + "'");
                }
            }
        }


        private void update_TFee_Master(string boarding_Point_id, string amount, string sessionid, string session, string transportation_Id, string transportationPath_id)
        {
            SqlCommand cmd;
            DataTable dt = mycode.FillData(" select Month,Month_Id   from dbo.[Month_Index] order by Position");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string Monthname = dt.Rows[i]["Month"].ToString();
                    string Month_Id = dt.Rows[i]["Month_Id"].ToString();
                    My.exeSql("update Transportation_Fee_Master set Transportation_path_id='" + transportationPath_id + "' where Boarding_Point_id='" + boarding_Point_id + "' and session_id='" + sessionid + "' and Month_id=" + Month_Id + "");
                }
            }
        }
    }
}