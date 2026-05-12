using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Examination_Admin
{
    public partial class personality_traits : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
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
                        ViewState["Userid"] = Session["Admin"].ToString();
                        mycode.bind_all_ddl_with_id(ddl_class, "select Course_Name,course_id from Add_course_table order by Position asc");
                        bind_activity();
                    }
                }
                catch (Exception ex)
                {
                    My.submitException(ex, "Add_personality_trate");
                }
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
        private void bind_activity()
        {
            DataTable dt = mycode.FillData("Select *,(select top 1 Course_Name from Add_course_table where course_id=Exam_Personality_Traits.Class_id) as Class_name  from Exam_Personality_Traits order by Position asc");
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no branch list exist", "warning");
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
            btn_Submit.Text = "Add";
            btn_cancel.Visible = false;
            txt_activity_name.Text = "";
            txt_position.Text = "";
            bind_activity();
        }


        protected void btn_Submit_Click1(object sender, EventArgs e)
        {
            try
            {
                if (ddl_class.SelectedItem.Text == "Select")
                {
                    Alertme("Please select class.", "warning");
                    ddl_class.Focus();
                }
                else if (txt_activity_name.Text == "")
                {
                    Alertme("Please enter activity name.", "warning");
                    txt_activity_name.Focus();
                }
                else
                {
                    save_records();
                }
            }
            catch (Exception ex)
            {
            }
        }


        private void save_records()
        {
            SqlCommand cmd;
            if (btn_Submit.Text == "Add")
            {
                DataTable dt = mycode.FillData("Select Id from Exam_Personality_Traits where Class_id='" + ddl_class.SelectedValue + "' and Activity_name='" + txt_activity_name.Text + "'");
                if (dt.Rows.Count == 0)
                {
                    string personality_id = cretepersonality_id();
                    string query = "INSERT INTO Exam_Personality_Traits (Activity_name,Activity_Id,Position,Class_id) values (@Activity_name,@Activity_Id,@Position,@Class_id)";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Activity_name", txt_activity_name.Text);
                    cmd.Parameters.AddWithValue("@Activity_Id", personality_id);
                    cmd.Parameters.AddWithValue("@Position", txt_position.Text);
                    cmd.Parameters.AddWithValue("@Class_id", ddl_class.SelectedValue);
                    if (My.InsertUpdateData(cmd))
                    {
                        Alertme("Record has been added successfully.", "success");
                        btn_Submit.Text = "Add";
                        txt_activity_name.Text = "";
                        txt_position.Text = "";
                        bind_activity();
                    }
                }
                else
                {
                    txt_activity_name.Focus();
                    Alertme("This activity already exist with this name.", "warning");
                    return;
                }
            }
            else
            {
                DataTable dt = mycode.FillData("Select Id from Exam_Personality_Traits where Class_id='" + ddl_class.SelectedValue + "' and Activity_name='" + txt_activity_name.Text + "' and Id!=" + hd_id.Value + "");
                if (dt.Rows.Count == 0)
                {
                    string query = "Update Exam_Personality_Traits set Activity_name=@Activity_name,Position=@Position,Class_id=@Class_id where Id=@Id";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Activity_name", txt_activity_name.Text);
                    cmd.Parameters.AddWithValue("@Position", txt_position.Text);
                    cmd.Parameters.AddWithValue("@Class_id", ddl_class.SelectedValue);
                    cmd.Parameters.AddWithValue("@Id", hd_id.Value);
                    if (My.InsertUpdateData(cmd))
                    {
                        Alertme("Record has been update successfully.", "success");
                        btn_Submit.Text = "Add";
                        btn_cancel.Visible = false;
                        txt_activity_name.Text = "";
                        txt_position.Text = "";
                        bind_activity();
                    }
                }
                else
                {
                    txt_activity_name.Focus();
                    Alertme("This activity already exist with this name.", "warning");
                    return;
                }
            }
        }

        private string cretepersonality_id()
        {
            bool duplicate = false;
            string personality_id = mycode.auto_serial("Global_sl_id");
            while (!duplicate)
            {
                DataTable cdt = mycode.FillData("select Activity_Id from Exam_Personality_Traits where Activity_Id='" + personality_id + "'");
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    duplicate = true;
                }
                else
                {
                    duplicate = false;
                    personality_id = mycode.auto_serial("Global_sl_id");
                }
            }
            return personality_id;
        }


        protected void lnkDel_Click(object sender, EventArgs e)
        {
            SqlCommand cmd;
            LinkButton lnk = (LinkButton)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            Label lbl_Id = (Label)row.FindControl("lbl_Id");
            Label lbl_activity_id = (Label)row.FindControl("lbl_activity_id");
            if (is_true(lbl_activity_id.Text))
            {
                string query = "delete from Exam_Personality_Traits where Id=@Id";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Id", lbl_Id.Text);
                if (My.InsertUpdateData(cmd))
                {
                    Alertme("Activity has been deleted successfully.", "success");
                    bind_activity();
                }
            }
            else
            {
                Alertme("You can't delete this activity. There is a data associated with mark entry.", "warning");
                return;
            }
        }


        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                Label lbl_activity_id = (Label)row.FindControl("lbl_activity_id");
                hd_id.Value = lbl_Id.Text;
                Label lbl_class_id = (Label)row.FindControl("lbl_class_id");
                Label lbl_activity_name = (Label)row.FindControl("lbl_activity_name");
                TextBox txt_position_number = (TextBox)row.FindControl("txt_position_number");
                if (is_true(lbl_activity_id.Text))
                {
                    ddl_class.SelectedValue = lbl_class_id.Text;
                    txt_activity_name.Text = lbl_activity_name.Text;
                    txt_position.Text = txt_position_number.Text;
                    btn_cancel.Visible = true;
                    btn_Submit.Text = "Update";
                }
                else
                {
                    Alertme("You can't edit this activity. There is a data associated with mark entry.", "warning");
                    return;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private bool is_true(string activity_id)
        {
            if (mycode.FillData("select Activity_Id from Exam_Personality_Traits_Term_Wise where Activity_Id='" + activity_id + "'").Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        protected void btn_update_sl_Click(object sender, EventArgs e)
        {
            try
            {
                update_activity_position();
                bind_activity();
                Alertme("Position has been updated successfully.", "success");

            }
            catch (Exception ex)
            {
                My.submitException(ex, "HomeSlideAdded");
            }
        }

        private void update_activity_position()
        {
            int growcount = rd_view.Items.Count;
            int k = 0;
            for (int i = 0; i < growcount; i++)
            {
                Label lbl_Id = (Label)rd_view.Items[i].FindControl("lbl_id");
                TextBox txtsl_position = (TextBox)rd_view.Items[i].FindControl("txt_position_number");
                SqlDataAdapter ad = new SqlDataAdapter("select * from Exam_Personality_Traits where Id='" + lbl_Id.Text + "'", My.conn);
                DataSet ds = new DataSet();
                ad.Fill(ds, "Exam_Personality_Traits");
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


    }
}