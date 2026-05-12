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
    public partial class course_master : System.Web.UI.Page
    {
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
                        ViewState["branchid"] = mycode.get_branch_id(Session["Admin"].ToString());
                        string pagename_current = Path.GetFileName(Request.Path);
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];

                        bind_grd_view();
                        txt_course_name.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "course_master");
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

        My mycode = new My();
        private void bind_grd_view()
        {
            DataTable dt = mycode.FillData("select *,Row_Number() over( order by id) sl  from Add_course_table where Branch_id='" + ViewState["branchid"].ToString() + "' order by Position asc");
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }



        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            txt_course_name.Text = "";
            btn_cancel.Visible = false;
            btn_Submit.Text = "Add";
        }

        protected void btn_Submit_Click1(object sender, EventArgs e)
        {
            if (txt_course_name.Text == "")
            {
                Alertme("Please Enter Course Name.", "warning");
                txt_course_name.Focus();
                return;
            }
            if (txt_position.Text == "")
            {
                Alertme("Please Enter position.", "warning");
                txt_position.Focus();
                return;
            }
            if (btn_Submit.Text == "Add")
            {
                if (ViewState["Is_add"].ToString() == "1")
                {
                    submit_details();
                    My.send_data_to_user_log_history(ViewState["Userid"].ToString(), ViewState["Userid"].ToString() + " create a class master ");
                    empty_form();
                    bind_grd_view();
                }
                else
                {
                    Alertme("SORRY! You have not permission for this work.", "warning");
                }
            }
            else
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    update_update_details();
                    My.send_data_to_user_log_history(ViewState["Userid"].ToString(), ViewState["Userid"].ToString() + " update a class master ");
                    empty_form();
                    bind_grd_view();
                }
                else
                {
                    Alertme("SORRY! You have not permission for this work.", "warning");
                }
            }
        }

        private void update_update_details()
        {
            SqlConnection conn = new SqlConnection(My.conn);
            SqlDataAdapter ad = new SqlDataAdapter("select * from Add_course_table where ID='" + hd_id.Value + "' ", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                dr[0] = txt_course_name.Text;
                dr["Type"] = ddl_type.Text;
                dr["Position"] = txt_position.Text;
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
            Alertme("Class  Updated Successfully", "success");
        }

        DataTable cdt;

        private void submit_details()
        {
            cdt = My.dataTable("select course_id from dbo.[Add_course_table] where Course_Name='" + txt_course_name.Text + "' and Branch_id='" + ViewState["branchid"].ToString() + "'");
            int rowcount = cdt.Rows.Count;
            if (rowcount == 0)
            {
                string sl = create_sl_nonew();
                SqlConnection conn = new SqlConnection(My.conn);
                SqlDataAdapter ad = new SqlDataAdapter("select * from Add_course_table  ", conn);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                DataTable dt = ds.Tables[0];
                DataRow dr = dt.NewRow();
                dr[0] = txt_course_name.Text;
                dr[2] = sl;
                dr["Type"] = ddl_type.Text;
                dr["Position"] = txt_position.Text;
                dr["Branch_id"] = ViewState["branchid"].ToString();
                dt.Rows.Add(dr);
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(dt);
                Alertme("Class Created Successfully", "success");
            }
            else
            {
                Alertme("This class name already  added ", "success");
            }

        }


        private string create_sl_nonew()
        {

            bool duplicate = true;
            string class_id = My.auto_serialS("class_id");
            while (duplicate)
            {
                cdt = My.dataTable("select course_id from dbo.[Add_course_table] where course_id='" + class_id + "'");
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    duplicate = false;
                }
                else
                {
                    class_id = My.auto_serialS("class_id");
                }
            }
            return class_id;
        }

        private void empty_form()
        {
            txt_course_name.Text = "";
            txt_position.Text = "";
            btn_cancel.Visible = false;
            btn_Submit.Text = "Add";
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            if (ViewState["Is_Edit"].ToString() == "1")
            {
                try
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_course_Name = (Label)row.FindControl("lbl_course_Name");
                    Label lbl_type = (Label)row.FindControl("lbl_type");
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");
                    Label lbl_position = (Label)row.FindControl("lbl_position");
                    string clss = lbl_course_Name.Text;
                    if (is_true(clss))
                    {
                        hd_id.Value = lbl_Id.Text;
                        txt_course_name.Text = lbl_course_Name.Text;
                        txt_position.Text = lbl_position.Text;
                        ddl_type.Text = lbl_type.Text;
                        btn_cancel.Visible = true;
                        btn_Submit.Text = "Update";
                    }
                    else
                    {
                        Alertme("Can't update there is a data associated with student.", "warning");
                    }
                }
                catch
                {
                }
            }
            else
            {
                Alertme("SORRY! You have not permission for this work.", "warning");

            }
        }


        private bool is_true(string clss)
        {
            if (My.dataTable("select class from dbo.[admission_registor] where class='" + clss + "'").Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        protected void lnkDel_Click(object sender, EventArgs e)
        {
            if (ViewState["Is_delete"].ToString() == "1")
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                Label lbl_course_Name = (Label)row.FindControl("lbl_course_Name");
                if (is_true(lbl_course_Name.Text))
                {
                    My.exeSql("delete from Add_course_table where ID='" + lbl_Id.Text + "'");
                    Alertme("cOURSE Master deleted Successfully", "success");
                    bind_grd_view();
                }
                else
                {
                    Alertme("You can't delete this class", "warning");
                }
            }
            else
            {

            }
        }

        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (((Label)e.Item.FindControl("lbl_is_sync")).Text == "true")
                {
                    ((Label)e.Item.FindControl("lbl_show_is_synk")).Text = "Yes";
                }
                else
                {
                    ((Label)e.Item.FindControl("lbl_show_is_synk")).Text = "No";
                }
            }
        }


        protected void btn_update_sl_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    update_slider_image_position();
                    bind_grd_view();
                    Alertme("Position updated successfully.", "success");
                }
                else
                {
                    Alertme("SORRY! You have not permission for this work.", "warning");
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "HomeSlideAdded");
            }
        }

        private void update_slider_image_position()
        {
            int growcount = rd_view.Items.Count;
            int k = 0;
            for (int i = 0; i < growcount; i++)
            {
                Label lbl_Id = (Label)rd_view.Items[i].FindControl("lbl_id");
                TextBox txtsl_position = (TextBox)rd_view.Items[i].FindControl("txt_position_number");
                SqlDataAdapter ad = new SqlDataAdapter("select * from Add_course_table where Id='" + lbl_Id.Text + "'", My.conn);
                DataSet ds = new DataSet();
                ad.Fill(ds, "Add_course_table");
                DataTable dt = ds.Tables[0];
                int rowcount = ds.Tables[0].Rows.Count;
                if (rowcount == 0)
                {
                }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (txtsl_position.Text != "")
                        {
                            dr["Position"] = txtsl_position.Text;
                            SqlCommandBuilder cmb = new SqlCommandBuilder(ad);
                            ad.Update(dt);
                        }
                    }
                }
                k++;
            }
        }

        protected void txt_position_number_TextChanged(object sender, EventArgs e)
        {
            if (ViewState["Is_Edit"].ToString() == "1")
            {
                TextBox lnk = (TextBox)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                TextBox txtpositionnumber = (TextBox)row.FindControl("txt_position_number");
                Label lbl_course_id = (Label)row.FindControl("lbl_course_id");

                if (txtpositionnumber.Text == "")
                {
                    Alertme("Please enter class position No.", "warning");
                }
                else
                {
                    bool chk = get_dublicate_position(lbl_course_id.Text, txtpositionnumber.Text);
                    if (chk == false)
                    {
                        Alertme("Sorry this class position no. already allocated other class", "warning");
                    }
                }
            }
            else
            {
                Alertme("SORRY! You have not permission for this work.", "warning");
            }
        }

        private bool get_dublicate_position(string course_id, string positionnumber)
        {
            cdt = My.dataTable("select course_id from dbo.[Add_course_table] where course_id!='" + course_id + "' and Position='" + positionnumber + "'");
            int rowcount = cdt.Rows.Count;
            if (rowcount == 0)
            {
                return true;

            }
            else
            {
                return false;

            }

        }
    }
}