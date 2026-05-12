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
    public partial class period_master : System.Web.UI.Page
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
                        string pagename_current = "period-master.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];

                        ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        ViewState["Sessionid"] = My.get_session_id();
                        mycode.bind_all_ddl_with_id_cap_All(ddl_class, "Select Course_Name,course_id from Add_course_table order by Position");
                        mycode.bind_all_ddl_with_id_cap_All(ddlclasssearch, "Select Course_Name,course_id from Add_course_table order by Position");
                        bind_grd_view_all();
                        ViewState["flag"] = "0";

                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_Strength_Master");
            }
        }

        private void bind_grd_view_all()
        {
            bind_grd_view("select *,(select top 1 Course_Name from Add_course_table where course_id=Class_Routine_period_Master.Class_id) as Class_name from Class_Routine_period_Master where Session_id=" + ViewState["Sessionid"].ToString() + " order by Period_no asc");
        }

        private void bind_grd_view(string qry)
        {
            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
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

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("period-master.aspx", false);

        }
        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                save_periods();
            }
            catch (Exception ex)
            {
            }
        }

        string period_id;
        private void save_periods()
        {
            if (txt_period_name.Text == "")
            {
                Alertme("Please enter period name", "warning");
            }
            else if (txt_period_no.Text == "")
            {
                Alertme("Please enter period no.", "warning");
            }
            else
            {
                if (btn_Submit.Text == "Add")
                {
                    if (ViewState["Is_add"].ToString() == "1")
                    {
                        if (ddl_class.SelectedItem.Text == "ALL")
                        {
                            DataTable dtc = mycode.FillData("Select Course_Name,course_id from Add_course_table order by Position");
                            foreach (DataRow dr in dtc.Rows)
                            {
                                create_sl_no();
                                string query = "Select * from Class_Routine_period_Master where Session_id=" + ViewState["Sessionid"].ToString() + " and Branch_id=" + ViewState["branchid"].ToString() + " and Period_no=" + txt_period_no.Text + " and Class_id=" + dr["course_id"].ToString() + "";
                                DataTable dt = mycode.FillData(query);
                                if (dt.Rows.Count == 0)
                                {
                                    SqlCommand cmd;
                                    string querys = "INSERT INTO Class_Routine_period_Master (Period_Name,Period,Period_type,Period_no,Session_id,Branch_id,Class_id) values (@Period_Name,@Period,@Period_type,@Period_no,@Session_id,@Branch_id,@Class_id)";
                                    cmd = new SqlCommand(querys);
                                    cmd.Parameters.AddWithValue("@Period_Name", txt_period_name.Text);
                                    cmd.Parameters.AddWithValue("@Period", period_id);
                                    cmd.Parameters.AddWithValue("@Period_type", ddl_period_Type.Text);
                                    cmd.Parameters.AddWithValue("@Period_no", txt_period_no.Text);
                                    cmd.Parameters.AddWithValue("@Session_id", ViewState["Sessionid"].ToString());
                                    cmd.Parameters.AddWithValue("@Branch_id", ViewState["branchid"].ToString());
                                    cmd.Parameters.AddWithValue("@Class_id", dr["course_id"].ToString());
                                    if (InsertUpdate.InsertUpdateData(cmd))
                                    {
                                        Alertme("Period has been saved successfully", "success");
                                    }
                                }
                                else
                                {
                                    Alertme("Sorry you can't add this period because already added with same period no.", "warning");
                                }
                            }
                            empty_data();
                            if (ViewState["flag"].ToString() == "0")
                            {
                                bind_grd_view_all();
                            }
                            if (ViewState["flag"].ToString() == "1")
                            {
                                bind_grd_view_by_class();
                            }
                        }
                        else
                        {
                            create_sl_no();
                            string query = "Select * from Class_Routine_period_Master where Session_id=" + ViewState["Sessionid"].ToString() + " and Branch_id=" + ViewState["branchid"].ToString() + " and Period_no=" + txt_period_no.Text + " and Class_id=" + ddl_class.SelectedValue + "";
                            DataTable dt = mycode.FillData(query);
                            if (dt.Rows.Count == 0)
                            {
                                SqlCommand cmd;
                                string querys = "INSERT INTO Class_Routine_period_Master (Period_Name,Period,Period_type,Period_no,Session_id,Branch_id,Class_id) values (@Period_Name,@Period,@Period_type,@Period_no,@Session_id,@Branch_id,@Class_id)";
                                cmd = new SqlCommand(querys);
                                cmd.Parameters.AddWithValue("@Period_Name", txt_period_name.Text);
                                cmd.Parameters.AddWithValue("@Period", period_id);
                                cmd.Parameters.AddWithValue("@Period_type", ddl_period_Type.Text);
                                cmd.Parameters.AddWithValue("@Period_no", txt_period_no.Text);
                                cmd.Parameters.AddWithValue("@Session_id", ViewState["Sessionid"].ToString());
                                cmd.Parameters.AddWithValue("@Branch_id", ViewState["branchid"].ToString());
                                cmd.Parameters.AddWithValue("@Class_id", ddl_class.SelectedValue);
                                if (InsertUpdate.InsertUpdateData(cmd))
                                {
                                    Alertme("Period has been saved successfully", "success");
                                    empty_data();
                                    if (ViewState["flag"].ToString() == "0")
                                    {
                                        bind_grd_view_all();
                                    }
                                    if (ViewState["flag"].ToString() == "1")
                                    {
                                        bind_grd_view_by_class();
                                    }
                                }
                            }
                            else
                            {
                                Alertme("Sorry you can't add this period because already added with same period no.", "warning");
                            }
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
                        string query = "Select * from Class_Routine_period_Master where Session_id=" + ViewState["Sessionid"].ToString() + " and Branch_id=" + ViewState["branchid"].ToString() + " and Period_no=" + txt_period_no.Text + " and Class_id=" + ddl_class.SelectedValue + " and Id!=" + HdID.Value + "";
                        DataTable dt = mycode.FillData(query);
                        if (dt.Rows.Count == 0)
                        {
                            SqlCommand cmd;
                            query = "Update Class_Routine_period_Master set Period_Name=@Period_Name,Period_type=@Period_type,Period_no=@Period_no,Session_id=@Session_id,Class_id=@Class_id  where Id = @Id";
                            cmd = new SqlCommand(query);
                            cmd.Parameters.AddWithValue("@Period_Name", txt_period_name.Text);
                            cmd.Parameters.AddWithValue("@Period_type", ddl_period_Type.Text);
                            cmd.Parameters.AddWithValue("@Period_no", txt_period_no.Text);
                            cmd.Parameters.AddWithValue("@Session_id", ViewState["Sessionid"].ToString());
                            cmd.Parameters.AddWithValue("@Class_id", ddl_class.SelectedValue);
                            cmd.Parameters.AddWithValue("@Id", HdID.Value);
                            if (InsertUpdate.InsertUpdateData(cmd))
                            {
                                Alertme("Period has been saved successfully", "success");
                                empty_data();
                                if (ViewState["flag"].ToString() == "0")
                                {
                                    bind_grd_view_all();
                                }
                                if (ViewState["flag"].ToString() == "1")
                                {
                                    bind_grd_view_by_class();
                                }
                            }
                        }
                        else
                        {
                            Alertme("Sorry you can't add this period because already added with same period no.", "warning");
                        }
                    }
                    else
                    {
                        Alertme(My.get_restricted_message(), "warning");

                    }
                }
            }
        }


        private void create_sl_no()
        {
            bool duplicate = true;
            period_id = My.auto_serialS("Global_sl_id"); 
            while (duplicate)
            {
                DataTable cdt = My.dataTable("select Period from Class_Routine_period_Master where Period='" + period_id + "'");
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    duplicate = false;
                }
                else
                {
                    period_id = My.auto_serialS("Global_sl_id"); 
                }
            }
        }

        private void empty_data()
        {
            btn_Submit.Text = "Add";
            btn_cancel.Visible = false;
            txt_period_name.Text = "";
            txt_period_no.Text = "";
        }


        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_class_id = (Label)row.FindControl("lbl_class_id");
                    Label lbl_period_Name = (Label)row.FindControl("lbl_period_Name");
                    Label lbl_period_type = (Label)row.FindControl("lbl_period_type");
                    Label lbl_Period_no = (Label)row.FindControl("lbl_Period_no");

                    Label lbl_Id = (Label)row.FindControl("lbl_Id");
                    HdID.Value = lbl_Id.Text;
                    ddl_class.SelectedValue = lbl_class_id.Text;
                    txt_period_name.Text = lbl_period_Name.Text;
                    ddl_period_Type.Text = lbl_period_type.Text;
                    txt_period_no.Text = lbl_Period_no.Text;

                    btn_cancel.Visible = true;
                    btn_Submit.Text = "Update";
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
            if (ViewState["Is_Edit"].ToString() == "1")
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                Label lbl_class_id = (Label)row.FindControl("lbl_class_id");
                Label lbl_period = (Label)row.FindControl("lbl_period");
                Label lbl_session_id = (Label)row.FindControl("lbl_session_id");

                if (My.dataTable("select Period from Class_Routine_period where Period='" + lbl_period.Text + "' and course_id='" + lbl_class_id.Text + "' and Session_id='" + lbl_session_id.Text + "'").Rows.Count == 0)
                {
                    My.exeSql("delete from Class_Routine_period_Master where Id='" + lbl_Id.Text + "'");
                    Alertme("Item has been deleted successfully.", "success");
                    if (ViewState["flag"].ToString() == "0")
                    {
                        bind_grd_view_all();
                    }
                    if (ViewState["flag"].ToString() == "1")
                    {
                        bind_grd_view_by_class();
                    }
                }
                else
                {
                    Alertme("You can't delete this Period. There is a data associated with period time.", "warning");
                }
            }
            else
            {
                Alertme(My.get_restricted_message(), "warning");
            }
        }

        protected void btn_fnd_by_class_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlclasssearch.SelectedItem.Text == "ALL")
                {
                    bind_grd_view_all();
                    ViewState["flag"] = "0";
                }
                else
                {
                    bind_grd_view_by_class();
                    ViewState["flag"] = "1";
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void bind_grd_view_by_class()
        {
            bind_grd_view("select *,(select top 1 Course_Name from Add_course_table where course_id=Class_Routine_period_Master.Class_id) as Class_name from Class_Routine_period_Master where Class_id=" + ddlclasssearch.SelectedValue + " and  Session_id=" + ViewState["Sessionid"].ToString() + " order by Period_no asc");
        }
    }
}