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
    public partial class Create_Transport_Path : System.Web.UI.Page
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
                        ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        string pagename_current = "Create_Transport_Path.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        find_firm_details();






                        txt_stoppagepoint.Text = mycode.get_firm_name();
                        mycode.bind_all_ddl_with_id(ddl_distancetype, "select Start_KM+' Km To '+cast(End_KM as varchar(50))+' Km',Distance_id from  Transport_distance_Meter order by End_KM");

                        mycode.bind_all_ddl_with_id(ddl_select_buss, " select transport_name+'-{BUS NO-'+Bus_no+'}',transport_id from  Transport_Master order by transport_name");
                        mycode.bind_all_ddl_with_id_All_New(ddl_bussno, " select transport_name+'-{BUS NO-'+Bus_no+'}',transport_id from  Transport_Master order by transport_name");


                        Bind_grid();

                    }
                    // BindDetails();
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_User");
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
        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            if (ddl_select_buss.Text == "Select")
            {
                Alertme("Please select vehicle name", "warning");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            else if (ddl_distancetype.Text == "Select")
            {
                Alertme("Please select distance", "warning");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            else
            {
                string routname = txt_routname.Text + " Vehicle No:/Van No. " + txt_busno.Text + " Distance : " + ddl_distancetype.SelectedItem.Text;
                if (btn_Submit.Text == "Add")
                {
                    DataTable class_dt = My.dataTable("select Transportation_Id from dbo.[TransportationPath] where Rootname='" + txt_routname.Text + "'  ");
                    if (class_dt.Rows.Count == 0)
                    {
                        string slno = create_sl_no1();
                        SqlConnection conn = new SqlConnection(My.con);
                        SqlDataAdapter ad = new SqlDataAdapter("select * from TransportationPath  ", conn);
                        DataSet ds = new DataSet();
                        ad.Fill(ds);
                        DataTable dt = ds.Tables[0];
                        DataRow dr = dt.NewRow();
                        dr[0] = txt_initialpoint.Text;
                        dr[1] = txt_stoppagepoint.Text;
                        dr[2] = txt_busno.Text;
                        dr[3] = routname;
                        dr[4] = "0";
                        dr[6] = ddl_select_buss.SelectedValue;
                        dr[7] = ddl_distancetype.SelectedItem.Text;
                        dr["Branch_id"] = ViewState["branchid"].ToString();
                        dr["User_id"] = ViewState["Userid"].ToString();
                        dr["date"] = mycode.date();
                        dr["time"] = mycode.time();
                        dr["TransportationPath_id"] = slno;
                        dr["Distance_id"] = ddl_distancetype.SelectedItem.Value;
                        dr["Rootname"] = txt_routname.Text;
                        dr["Live_location_path"] = txt_live_location_link.Text;
                        dt.Rows.Add(dr);
                        SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                        ad.Update(dt);
                        Alertme("Transport rute created Successfully", "success");
                        txt_routname.Text = "";
                        btn_Submit.Text = "Add";
                        txt_initialpoint.Text = "";
                       


                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

                        My.send_data_to_user_log_history(ViewState["Userid"].ToString(), ViewState["Userid"].ToString() + " create a transport path ");

                        Bind_grid();
                    }
                    else
                    {
                        Alertme("Transport rute name  already added", "warning");
                    }
                }
                else
                {


                    DataTable class_dt = My.dataTable("select Transportation_Id from dbo.[TransportationPath] where Rootname='" + txt_routname.Text + "' and  id!='" + hd_id.Value + "' ");
                    if (class_dt.Rows.Count == 0)
                    {
                        SqlConnection conn = new SqlConnection(My.con);
                        SqlDataAdapter ad = new SqlDataAdapter("select * from TransportationPath where id='" + hd_id.Value + "'", conn);
                        DataSet ds = new DataSet();
                        ad.Fill(ds);
                        DataTable dt = ds.Tables[0];

                        foreach (DataRow dr in dt.Rows)
                        {
                            dr[0] = txt_initialpoint.Text;
                            dr[1] = txt_stoppagepoint.Text;
                            dr[2] = txt_busno.Text;
                            dr[3] = routname;
                            dr[4] = "0";
                            dr[6] = ddl_select_buss.SelectedValue;
                            dr[7] = ddl_distancetype.SelectedItem.Text;
                            dr["Branch_id"] = ViewState["branchid"].ToString();
                            dr["User_id"] = ViewState["Userid"].ToString();
                            dr["date"] = mycode.date();
                            dr["time"] = mycode.time();
                            dr["Rootname"] = txt_routname.Text;
                            dr["Distance_id"] = ddl_distancetype.SelectedItem.Value;
                            dr["Live_location_path"] = txt_live_location_link.Text;
                        }

                        SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                        ad.Update(dt);
                        Alertme("Transport rute update Successfully", "success");
                        txt_initialpoint.Text = "";
                        txt_busno.Text = "";
                        txt_routname.Text = "";
                        txt_live_location_link.Text = "";
                        My.send_data_to_user_log_history(ViewState["Userid"].ToString(), ViewState["Userid"].ToString() + " update a transport path ");

                        btn_Submit.Text = "Add";
                        Bind_grid();
                    }
                    else
                    {
                        Alertme("Transport rute name  already added", "warning");
                    }
                }
            }
        }
        protected void ddl_bussno_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bind_grid();
        }
        private void Bind_grid()
        {
            string query = "";
            btn_excels.Visible = false;
            print1.Visible = false;
            if (ddl_bussno.SelectedItem.Text == "ALL")
            {
                query = "Select * from TransportationPath  order by InitialPoint asc";
                lbl_class22.Text = "ALL Bus No.";
            }
            else
            {
                lbl_class22.Text = "Bus No.:" + ddl_bussno.SelectedItem.Value;
                query = "Select * from TransportationPath where Transportation_Id='" + ddl_bussno.SelectedItem.Value + "' order by InitialPoint asc";
            }
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                print1.Visible = false;
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
                btn_excels.Visible = true;
                if (ViewState["Is_Print"].ToString() == "1")
                {
                    print1.Visible = true;
                }
                else
                {
                    print1.Visible = false;
                }
            }
        }



        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            btn_Submit.Text = "Add";
            txt_initialpoint.Text = "";
            
            txt_busno.Text = "";
            txt_routname.Text = "";
            txt_live_location_link.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "close();", true);
        }
        private string create_sl_no1()
        {

            bool duplicate = true;
            string transportation_id = mycode.auto_serial("transportation_id");
            while (duplicate)
            {
                DataTable cdt = My.dataTable(" select Transportation_Id from dbo.[TransportationPath] where Transportation_Id='" + transportation_id + "'");
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    duplicate = false;
                }
                else
                {
                    duplicate = true;
                    transportation_id = mycode.auto_serial("transportation_id");
                }
            }
            return transportation_id;




        }


        #region grid view edit and delete
        protected void lnkDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_delete"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");

                    mycode.executequery("delete from TransportationPath where id=" + lbl_Id.Text + "");

                    string msg = ViewState["Userid"].ToString() + " Delete Transportation Path, User id=" + ViewState["Userid"].ToString() + " Name=" + mycode.get_user(ViewState["Userid"].ToString()) + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MM/yyyy hh:mm:ss tt");
                    mycode.insert_data_logfile(ViewState["Userid"].ToString(), ViewState["firm_id"].ToString(), msg, ViewState["branchid"].ToString());
                    Bind_grid();
                    Alertme("Transport Path has been Successfully deleted", "success");
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

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");
                    Label lbl_InitialPoint = (Label)row.FindControl("lbl_InitialPoint");
                    Label lbl_DistinationPoint = (Label)row.FindControl("lbl_DistinationPoint");
                    Label lbl_busno = (Label)row.FindControl("lbl_busno");
                    Label lbl_Pathname = (Label)row.FindControl("lbl_Pathname");
                    Label lbl_distance = (Label)row.FindControl("lbl_distance");
                    Label lbl_Distance_id = (Label)row.FindControl("lbl_Distance_id");
                    Label lbl_Rootname = (Label)row.FindControl("lbl_Rootname");
                    Label lbl_live_location_path = (Label)row.FindControl("lbl_live_location_path");
                    Label lbl_Transportation_Id = (Label)row.FindControl("lbl_Transportation_Id");

                    btn_Submit.Text = "Update";
                    hd_id.Value = lbl_Id.Text;
                    txt_initialpoint.Text = lbl_InitialPoint.Text;
                    txt_stoppagepoint.Text = lbl_DistinationPoint.Text;
                    txt_busno.Text = lbl_busno.Text;
                    txt_routname.Text = lbl_Rootname.Text;
                    ddl_distancetype.Text = lbl_Distance_id.Text;
                    txt_live_location_link.Text = lbl_live_location_path.Text;
                    ddl_select_buss.SelectedValue = lbl_Transportation_Id.Text;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);


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
        #endregion

        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (((Label)e.Item.FindControl("lbl_live_location_path")).Text == "")
                {
                    ((Label)e.Item.FindControl("lbl_live_location")).Visible = false;
                }
                else
                {
                    ((Label)e.Item.FindControl("lbl_live_location")).Visible = true;
                }
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
        protected void ddl_select_buss_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_select_buss.SelectedItem.Text == "Select")
            {
                Alertme("Please select vehicle name", "warning");
            }
            else
            {
                DataTable cdt = My.dataTable(" select Bus_no from dbo.[Transport_Master] where transport_id='" + ddl_select_buss.SelectedValue + "'");
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    txt_busno.Text = "";
                }
                else
                {
                    txt_busno.Text = cdt.Rows[0]["Bus_no"].ToString();
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
    }
}