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
    public partial class Sheet_Mapping_with_Bus : System.Web.UI.Page
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

                        ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());


                        mycode.bind_all_ddl_with_id(ddl_select_buss, "select transport_name+'-{BUS NO-'+Bus_no+'}',transport_id from  Transport_Master order by transport_name");




                    }
                    // BindDetails();
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_User");
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
        protected void ddl_select_buss_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_select_buss.SelectedItem.Text == "Select")
            {
                Alertme("Please select vehicle name", "warning");

            }
            else
            {
                mycode.bind_all_ddl_with_id(ddl_path_root, " select  Rootname,TransportationPath_id from  TransportationPath where Transportation_Id=" + ddl_select_buss.SelectedValue + "");
            }
        }

        protected void ddl_path_root_SelectedIndexChanged(object sender, EventArgs e)
        {
            add_sheet.Visible = false;
            if (ddl_select_buss.SelectedItem.Text == "Select")
            {
                Alertme("Please select vehicle name", "warning");

            }
            else if (ddl_path_root.SelectedItem.Text == "Select")
            {
                Alertme("Please select Transportation Route", "warning");
            }
            else
            {
                DataTable class_dt = My.dataTable("select * from dbo.[Transport_Master] where transport_id='" + ddl_select_buss.SelectedValue + "'  ");
                if (class_dt.Rows.Count == 0)
                {
                    add_sheet.Visible = false;
                    Alertme("Sorry there are no any data found ", "warning");
                }
                else
                {
                    add_sheet.Visible = true;
                    txt_no_sheet.Text = class_dt.Rows[0]["Bus_no_sheet"].ToString();
                    Bind_sheet_deatils();
                }
            }

        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            if (ddl_select_buss.SelectedItem.Text == "Select")
            {
                Alertme("Please select Vehicle name", "warning");

            }
            else if (ddl_path_root.SelectedItem.Text == "Select")
            {
                Alertme("Please select Transportation Route", "warning");
            }

            else if (ddl_sheet_position.Text == "Select")
            {
                Alertme("Please enter seat position", "warning");
            }
            else if (ddl_seat_modle.Text == "Select")
            {
                Alertme("Please enter seat model", "warning");
            }
            else if (ddl_seattype.Text == "Select")
            {
                Alertme("Please enter seat type", "warning");
            }

            else if (txt_sheet_name.Text == "")
            {
                Alertme("Please enter seat name", "warning");
            }

            else
            {
                SqlCommand cmd;
                if (btn_Submit.Text == "Add")
                {

                    int numberofseat_create = My.toint(txt_sheet_name.Text);

                    for (int i = 1; i <= numberofseat_create; i++)
                    {


                       // insert_data_Transport_Path_Mapping_With_Sheet(i);
                       
                    }



                    bool chknosheet = get_shettno();

                    if (chknosheet == true)
                    {

                        bool check_row_no_set = My.check_row_no_avilavel_seat(ddl_select_buss.SelectedValue, ddl_path_root.SelectedValue, ddl_sheet_position.Text, ddl_seat_modle.Text);
                        if (check_row_no_set == true)
                        {

                            DataTable class_dt = My.dataTable("select * from dbo.[Transport_Path_Mapping_With_Sheet] where Transportation_Id=" + ddl_select_buss.SelectedValue + " and TransportationPath_id=" + ddl_path_root.SelectedValue + " and Sheet_No='" + txt_sheet_name.Text + "'  ");
                            if (class_dt.Rows.Count == 0)
                            {
                                string Sheet_Id = create_sl_no1(); 
                                string query = "INSERT INTO Transport_Path_Mapping_With_Sheet (TransportationPath_id,Transportation_Id,Sheet_position,Sheet_No,Created_by,Created_Date,Sheet_Id,Sheet_Status,Seat_Model,Seat_Type,Row) values (@TransportationPath_id,@Transportation_Id,@Sheet_position,@Sheet_No,@Created_by,@Created_Date,@Sheet_Id,@Sheet_Status,@Seat_Model,@Seat_Type,@Row)";
                                cmd = new SqlCommand(query);
                                cmd.Parameters.AddWithValue("@TransportationPath_id", ddl_path_root.SelectedValue);
                                cmd.Parameters.AddWithValue("@Transportation_Id", ddl_select_buss.SelectedValue);
                                cmd.Parameters.AddWithValue("@Sheet_position", ddl_sheet_position.Text);
                                cmd.Parameters.AddWithValue("@Sheet_No", txt_sheet_name.Text);
                                cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                                cmd.Parameters.AddWithValue("@Created_Date", My.getdate1());
                                cmd.Parameters.AddWithValue("@Sheet_Id", Sheet_Id);
                                cmd.Parameters.AddWithValue("@Sheet_Status", "0");// avl
                                cmd.Parameters.AddWithValue("@Seat_Model", ddl_seat_modle.Text);
                                cmd.Parameters.AddWithValue("@Seat_Type", ddl_seattype.Text);
                                cmd.Parameters.AddWithValue("@Row","0");

                                if (InsertUpdate.InsertUpdateData(cmd))
                                {
                                    Alertme("Seat details has been successfully added", "success");
                                    txt_sheet_name.Text = "";
                                    Bind_sheet_deatils();
                                }
                            }
                            else
                            {
                                Alertme("Sorry your entry seat no already exists", "warning");
                            }
                        }
                        else
                        {
                            Alertme("Sorry you can't add more seat because total number of seats allotted seat number ", "warning");

                        }
                    }
                    else
                    {
                    }
                }
                else
                {
                    DataTable class_dt = My.dataTable("select * from dbo.[Transport_Path_Mapping_With_Sheet] where Transportation_Id=" + ddl_select_buss.SelectedValue + " and TransportationPath_id=" + ddl_path_root.SelectedValue + " and Sheet_No='" + txt_sheet_name.Text + "' and Id!=" + hd_id.Value + "  ");
                    if (class_dt.Rows.Count == 0)
                    {



                        string query = "Update Transport_Path_Mapping_With_Sheet set Sheet_position=@Sheet_position,Sheet_No=@Sheet_No,Updated_By=@Updated_By,Updated_date=@Updated_date,Seat_Model=@Seat_Model,Seat_Type=@Seat_Type,Row=@Row where  Id=@Id";
                        cmd = new SqlCommand(query);
                        cmd = new SqlCommand(query);

                        cmd.Parameters.AddWithValue("@Sheet_position", ddl_sheet_position.Text);
                        cmd.Parameters.AddWithValue("@Sheet_No", txt_sheet_name.Text);
                        cmd.Parameters.AddWithValue("@Updated_By", ViewState["Userid"].ToString());
                        cmd.Parameters.AddWithValue("@Updated_date", My.getdate1());
                        cmd.Parameters.AddWithValue("@Seat_Model", ddl_seat_modle.Text);
                        cmd.Parameters.AddWithValue("@Seat_Type", ddl_seattype.Text);
                        cmd.Parameters.AddWithValue("@Row", "0");
                        cmd.Parameters.AddWithValue("@Id", hd_id.Value);

                        if (InsertUpdate.InsertUpdateData(cmd))
                        {
                            Alertme("Seat details has been successfully updated", "success");
                            txt_sheet_name.Text = "";
                            btn_Submit.Text = "Add";
                            Bind_sheet_deatils();
                        }


                    }
                    else
                    {
                        Alertme("Sorry your entry seat no already exists", "warning");
                    }
                }

            }
        }

        private bool get_shettno()
        {
            DataTable cdt = My.dataTable(" select * from dbo.[Transport_Path_Mapping_With_Sheet] where TransportationPath_id=" + ddl_path_root.SelectedValue + " and Transportation_Id=" + ddl_select_buss.SelectedValue + "");
            int rowcount = cdt.Rows.Count;
            if (rowcount == 0)
            {
                return true;
            }
            else
            {

                if (rowcount == Convert.ToInt32(txt_no_sheet.Text))
                {
                    Alertme("Sorry you can't add more seat because total number of seats allotted seat number ", "warning");
                    return false;
                }
                else if (rowcount <= Convert.ToInt32(txt_no_sheet.Text))
                {
                    //Alertme("Sorry you can't add more seat because total number of seats allotted seat number ", "warning");
                    return true;
                }
                else if (rowcount >= Convert.ToInt32(txt_no_sheet.Text))
                {
                    Alertme("Sorry you can't add more seat because total number of seats allotted seat number ", "warning");
                    return false;
                }
                else
                {
                    Alertme("Sorry you can't add more seat because total number of seats allotted seat number ", "warning");
                    return false;
                }
            }
        }

        private void Bind_sheet_deatils()
        {
            DataTable cdt = My.dataTable(" select * from dbo.[Transport_Path_Mapping_With_Sheet] where TransportationPath_id=" + ddl_path_root.SelectedValue + " and Transportation_Id=" + ddl_select_buss.SelectedValue + "");
            int rowcount = cdt.Rows.Count;
            if (rowcount == 0)
            {
                GrdView.DataSource = null;
                GrdView.DataBind();

            }
            else
            {
                GrdView.DataSource = cdt;
                GrdView.DataBind();

            }
        }

        private string create_sl_no1()
        {
            bool duplicate = true;
            string Sheet_Id = mycode.auto_serial("Seat_Id");
            while (duplicate)
            {
                DataTable cdt = My.dataTable(" select Sheet_Id from dbo.[Transport_Path_Mapping_With_Sheet] where Sheet_Id='" + Sheet_Id + "'");
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    duplicate = false;
                }
                else
                {
                    duplicate = true;
                    Sheet_Id = mycode.auto_serial("Seat_Id");
                }
            }
            return Sheet_Id;

        }




        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lnk.Parent.Parent;
                Label lbl_sheet_position = (Label)row.FindControl("lbl_sheet_position");
                Label lbl_sheetno = (Label)row.FindControl("lbl_sheetno");
                Label lbl_id = (Label)row.FindControl("lbl_id");
                Label lbl_rowname = (Label)row.FindControl("lbl_rowname");

                Label lbl_Seat_Type = (Label)row.FindControl("lbl_Seat_Type");
                Label lbl_Seat_Model = (Label)row.FindControl("lbl_Seat_Model");



                hd_id.Value = lbl_id.Text;
                ddl_sheet_position.Text = lbl_sheet_position.Text;
                txt_sheet_name.Text = lbl_sheetno.Text;
                 

                mycode.bind_ddl(ddl_seat_modle, "Select Seat_Model from Transport_Seat_Model where Position='" + ddl_sheet_position.Text + "' order by Seat_Model ");
                ddl_seat_modle.Text = lbl_Seat_Model.Text;

                ddl_seattype.Text = lbl_Seat_Type.Text;

                btn_Submit.Text = "Update";
            }
            catch
            {
            }


        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lnk.Parent.Parent;
                Label lbl_id = (Label)row.FindControl("lbl_id");
                Label lbl_sheetno = (Label)row.FindControl("lbl_sheetno");
                Label lbl_Sheet_Id = (Label)row.FindControl("lbl_Sheet_Id");
                Label lbl_Sheet_Status = (Label)row.FindControl("lbl_Sheet_Status");

                if (lbl_Sheet_Status.Text == "0")
                {

                    string msge = "User Id:-" + ViewState["Userid"].ToString() + " has been delete bus seat , seat  no." + lbl_sheetno.Text + " Sheet Id" + lbl_Sheet_Id.Text;

                    mycode.executequery("delete from Transport_Path_Mapping_With_Sheet where Id='" + lbl_id.Text + "'");
                    mycode.insert_data_logfile(ViewState["Userid"].ToString(), "1", msge, ViewState["branchid"].ToString());
                    Bind_sheet_deatils();
                }
                else
                {
                    Alertme("Sorry you can't delete this sheet because this seat already assigned student", "warning");
                }
            }
            catch
            {

            }
        }

        protected void ddl_sheet_position_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_select_buss.SelectedItem.Text == "Select")
            {
                Alertme("Please select vehicle name", "warning");

            }
            else if (ddl_path_root.SelectedItem.Text == "Select")
            {
                Alertme("Please select vehicle route", "warning");
            }
            else if (ddl_sheet_position.Text == "Select")
            {
                Alertme("Please select seat position", "warning");
            }
            else
            {
                mycode.bind_ddl(ddl_seat_modle, "Select Seat_Model from Transport_Seat_Model where Position='" + ddl_sheet_position.Text + "' order by Seat_Model ");
            }
        }
    }
}