using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class events_activities : System.Web.UI.Page
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
                        string pagename_current = Path.GetFileName(Request.Path);
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        ViewState["courseID"] = "0";
                        mycode.bind_all_ddl_with_id(ddl_session, "Select Session,session_id from session_details order by Session asc");
                        mycode.bind_all_ddl_with_id(ddl_session_search, "Select Session,session_id from session_details order by Session asc");
                        mycode.bind_all_ddl_with_id(ddl_class, "Select Course_Name,course_id from Add_course_table order by Position asc");
                        mycode.bind_all_ddl_with_id(ddl_class_search, "Select Course_Name,course_id from Add_course_table order by Position asc");
                        ddl_session.SelectedValue = My.get_session_id();
                        ddl_session_search.SelectedValue = ddl_session.SelectedValue;
                        ViewState["Branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        txt_startdate.Text = mycode.date();
                        Bind_course_fee_details();

                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Fee_Master");
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

        private void Bind_course_fee_details()
        {
            DataTable dt = mycode.FillData("Select Course_Name,course_id from Add_course_table order by Position");
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
        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            if (ViewState["Is_add"].ToString() == "1")
            {
                add_update_add();
            }
            else if (ViewState["Is_Edit"].ToString() == "1")
            {
                add_update_add();
            }
            else
            {
                Alertme("SORRY! You have not permission for this work.", "warning");
            }

        }

        private void add_update_add()
        {
            try
            {
                if (ddl_session.SelectedValue == "Select")
                {
                    ddl_session.Focus();
                    Alertme("Please select session", "warning");
                    return;
                }
                if (txt_startdate.Text == "")
                {
                    txt_startdate.Focus();
                    Alertme("Please enter date.", "warning");
                    return;
                }
                if (ddl_is_festival.Text == "Yes")
                {
                    if (txt_festival_name.Text == "")
                    {
                        txt_festival_name.Focus();
                        Alertme("Please enter festival name.", "warning");
                        return;
                    }
                }
                if (txt_details.Text == "")
                {
                    txt_details.Focus();
                    Alertme("Please enter activity details.", "warning");
                    return;
                }

                if (btn_Submit.Text == "Add")
                {
                    add_School_events();
                }
                else
                {
                    update_School_events();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void update_School_events()
        {
            SqlCommand cmd;
            string query = "update Festival_events set Session_id=@Session_id,Class_id=@Class_id,Event_date=@Event_date,Event_idate=@Event_idate,Is_festeval=@Is_festeval,Festival=@Festival,Event_details=@Event_details,Updated_by=@Updated_by,Updated_date=@Updated_date,Updated_time=@Updated_time where Id=@Id";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Session_id", ddl_session.SelectedValue);
            cmd.Parameters.AddWithValue("@Class_id", ddl_class.SelectedValue);
            cmd.Parameters.AddWithValue("@Event_date", txt_startdate.Text);
            cmd.Parameters.AddWithValue("@Event_idate", My.DateConvertToIdate(txt_startdate.Text));
            cmd.Parameters.AddWithValue("@Is_festeval", ddl_is_festival.Text);
            if (ddl_is_festival.Text == "Yes")
            {
                cmd.Parameters.AddWithValue("@Festival", txt_festival_name.Text);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Festival", "-");
            }
            cmd.Parameters.AddWithValue("@Event_details", txt_details.Text);
            cmd.Parameters.AddWithValue("@Updated_by", ViewState["Userid"].ToString());
            cmd.Parameters.AddWithValue("@Updated_date", mycode.date());
            cmd.Parameters.AddWithValue("@Updated_time", mycode.time());
            cmd.Parameters.AddWithValue("@Id", ViewState["RowId"].ToString());
            if (InsertUpdate.InsertUpdateData(cmd))
            {
                edtClassDV.Visible = false;
                classDV.Visible = true;
                txt_festival_name.Text = "";
                txt_details.Text = "";
                btn_cancel.Visible = false;
                btn_Submit.Text = "Add";
                Alertme("Events has been updated successfully.", "success");
                bind_grd_view();
            }
        }

        private void add_School_events()
        {
            int growcount = rd_view.Items.Count;
            int k = 0;
            for (int ix = 0; ix < growcount; ix++)
            {
                CheckBox chk = (CheckBox)rd_view.Items[ix].FindControl("chk_class");
                if (chk.Checked == true)
                {
                    Label lbl_class_id = (Label)rd_view.Items[ix].FindControl("lbl_class_id");
                    Label lbl_course_name = (Label)rd_view.Items[ix].FindControl("lbl_course_name");
                    insert_into_events(lbl_class_id.Text);
                    ViewState["statusUp"] = "1";
                }
                else
                {
                    k++;
                }
            }
            if (k == growcount)
            {
                Alertme("Please check minimum one class.", "warning");
                return;
            }
            else
            {
                Alertme("Events has been saved successfully.", "success");
                txt_festival_name.Text = "";
                txt_details.Text = "";
                chk_all.Checked = false;
                bind_grd_view();
            }
        }
        private void insert_into_events(string classid)
        {
            SqlCommand cmd;
            string query = "INSERT INTO Festival_events (Session_id,Class_id,Event_date,Event_idate,Is_festeval,Festival,Event_details,Created_by,Created_date,Created_time) values (@Session_id,@Class_id,@Event_date,@Event_idate,@Is_festeval,@Festival,@Event_details,@Created_by,@Created_date,@Created_time)";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Session_id", ddl_session.SelectedValue);
            cmd.Parameters.AddWithValue("@Class_id", classid);
            cmd.Parameters.AddWithValue("@Event_date", txt_startdate.Text);
            cmd.Parameters.AddWithValue("@Event_idate", My.DateConvertToIdate(txt_startdate.Text));
            cmd.Parameters.AddWithValue("@Is_festeval", ddl_is_festival.Text);
            if (ddl_is_festival.Text == "Yes")
            {
                cmd.Parameters.AddWithValue("@Festival", txt_festival_name.Text);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Festival", "-");
            }
            cmd.Parameters.AddWithValue("@Event_details", txt_details.Text);
            cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
            cmd.Parameters.AddWithValue("@Created_date", mycode.date());
            cmd.Parameters.AddWithValue("@Created_time", mycode.time());
            if (InsertUpdate.InsertUpdateData(cmd))
            {
            }
        }


        protected void chk_all_CheckedChanged(object sender, EventArgs e)
        {
            for (int j = 0; j < rd_view.Items.Count; j++)
            {
                CheckBox chk_class = rd_view.Items[j].FindControl("chk_class") as CheckBox;
                if (chk_all.Checked)
                {
                    chk_class.Checked = true;
                }
                else
                {
                    chk_class.Checked = false;
                }
            }
        }
        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (((Label)e.Item.FindControl("lbl_class_id")).Text == ViewState["courseID"].ToString())
                {
                    ((CheckBox)e.Item.FindControl("chk_class")).Checked = true;
                }
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            edtClassDV.Visible = false;
            classDV.Visible = true;
            txt_festival_name.Text = "";
            txt_details.Text = "";
            btn_cancel.Visible = false;
            btn_Submit.Text = "Add";
        }


        private void bind_grd_view()
        {
            DataTable dt = mycode.FillData("select *,(select top 1 Course_Name from Add_course_table where course_id=Festival_events.Class_id) as Class_name, Format(convert(DateTime,Event_date,103), 'dddd') as event_day from Festival_events where Session_id='" + ddl_session_search.SelectedValue + "' and Class_id='" + ddl_class_search.SelectedValue + "' order by Event_idate asc");
            if (dt.Rows.Count == 0)
            {
                Repeater1.DataSource = null;
                Repeater1.DataBind();
            }
            else
            {
                Repeater1.DataSource = dt;
                Repeater1.DataBind();
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
                    Label lbl_session_id = (Label)row.FindControl("lbl_session_id");
                    Label lbl_class_id = (Label)row.FindControl("lbl_class_id");
                    Label lbl_date = (Label)row.FindControl("lbl_date");
                    Label lbl_is_festeval = (Label)row.FindControl("lbl_is_festeval");
                    Label lbl_festival = (Label)row.FindControl("lbl_festival");
                    Label lbl_event_details = (Label)row.FindControl("lbl_event_details");
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");

                    ddl_session.SelectedValue = lbl_session_id.Text;
                    ddl_class.SelectedValue = lbl_class_id.Text;
                    txt_startdate.Text = lbl_date.Text;
                    ddl_is_festival.Text = lbl_is_festeval.Text;
                    txt_festival_name.Text = lbl_festival.Text;
                    txt_details.Text = lbl_event_details.Text;
                    ViewState["RowId"] = lbl_Id.Text;
                    edtClassDV.Visible = true;
                    classDV.Visible = false;
                    btn_cancel.Visible = true;
                    btn_Submit.Text = "Update";
                }
                else
                {
                    Alertme("SORRY! You have not permission for this work.", "warning");
                }
            }
            catch
            {

            }
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_session_search.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session", "warning");
                    ddl_session_search.Focus();
                }
                else if (ddl_class_search.SelectedItem.Text == "Select")
                {
                    Alertme("Please select class", "warning");
                    ddl_class_search.Focus();
                }
                else
                {
                    bind_grd_view();
                }
            }
            catch (Exception ex)
            {
            }
        }


        protected void lnkDel_Click(object sender, EventArgs e)
        {
            if (ViewState["Is_delete"].ToString() == "1")
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                SqlConnection conn = new SqlConnection(My.conn);
                SqlDataAdapter ad = new SqlDataAdapter("select * from Festival_events where  id='" + lbl_Id.Text + "'", conn);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                DataTable dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    dr.Delete();
                }
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(dt);
                Alertme("Events has been deleted Successfully", "success");
                bind_grd_view(); 
            }
            else
            {
                Alertme("SORRY! You have not permission for this work.", "warning");
            }
        }

    }
}