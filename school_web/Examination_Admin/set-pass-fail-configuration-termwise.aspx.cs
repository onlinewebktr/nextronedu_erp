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
    public partial class set_pass_fail_configuration_termwise1 : System.Web.UI.Page
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
                        ViewState["sesssionid"] = My.get_session_id();
                        mycode.bind_all_ddl_with_id(ddl_class, "Select Course_Name,course_id from Add_course_table order by position asc");
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_User");
            }
        }


        private void find_over_all_save_data()
        {
            string query = "select Percentage,Is_active from Exam_pass_fail_configuration_over_all where Session_id='" + ViewState["sesssionid"].ToString() + "' and Class_id='" + ddl_class.SelectedValue + "' and Branch_id='" + ViewState["branchid"].ToString() + "'";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                check_in_individual();
            }
            else
            {
                if (dt.Rows[0]["Is_active"].ToString() == "1")
                {
                    chk_individual_sub.Checked = false;
                    txt_percentage_o.Text = dt.Rows[0]["Percentage"].ToString();
                    on_indivisual_checked();
                }
                else
                {
                    check_in_individual();
                }
            }
        }

        private void check_in_individual()
        {
            string query = "select * from Exam_pass_fail_configuration where Session_id='" + ViewState["sesssionid"].ToString() + "' and Class_id='" + ddl_class.SelectedValue + "' and Branch_id='" + ViewState["branchid"].ToString() + "'";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                chk_individual_sub.Checked = false;
                on_indivisual_checked();
            }
            else
            {
                chk_individual_sub.Checked = true;
                on_indivisual_checked();
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


        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                find_term();
                find_over_all_save_data();
            }
            catch (Exception ex)
            {
            }
        }

        private void find_term()
        {
            string query = "select * from Exam_Term_Details where Class_id='" + ddl_class.SelectedValue + "' and Session_Id='" + ViewState["sesssionid"].ToString() + "' and Branch_Id='" + ViewState["branchid"].ToString() + "' and Istatus=1 order by Term_Name asc";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no record exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
                btn_save.Visible = false;
                btn_delete.Visible = false;
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }

        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbl_term_id = ((Label)e.Item.FindControl("lbl_term_id")) as Label;
                Repeater rp_subjects = ((Repeater)e.Item.FindControl("rp_subjects")) as Repeater;
                find_subjects(lbl_term_id.Text, rp_subjects);
            }
        }

        private void find_subjects(string term_id, Repeater rp_subjects)
        {
            string query = "Select sm.*,cm.Course_Name," + term_id + " as Term_id from Subject_Master sm join Add_course_table cm on cm.course_id=sm.course_id where sm.Subject_Type_Scholastic_Co_Scholastic='Scholastic' and sm.course_id='" + ddl_class.SelectedValue + "' and sm.Branch_id='" + ViewState["branchid"].ToString() + "'";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                rp_subjects.DataSource = null;
                rp_subjects.DataBind();
                btn_save.Visible = false;
                btn_delete.Visible = false;
            }
            else
            {
                rp_subjects.DataSource = dt;
                rp_subjects.DataBind();
                btn_save.Visible = true;
                btn_delete.Visible = true;
            }
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                save_pass_fail_data(); delete_overall();
                Alertme("Record has been saved successfully", "success");
            }
            catch (Exception ex)
            {
            }
        }

        private void save_pass_fail_data()
        {
            int growcount = rd_view.Items.Count;
            for (int ix = 0; ix < growcount; ix++)
            {
                Repeater rp_subjects = (Repeater)rd_view.Items[ix].FindControl("rp_subjects");
                int growcountx = rp_subjects.Items.Count;
                for (int ixx = 0; ixx < growcountx; ixx++)
                {
                    Label lbl_subject_Name = (Label)rp_subjects.Items[ixx].FindControl("lbl_subject_Name");
                    TextBox txt_percentage = (TextBox)rp_subjects.Items[ixx].FindControl("txt_percentage");
                    Label lbl_terms_id = (Label)rp_subjects.Items[ixx].FindControl("lbl_terms_id");
                    Label lbl_subject_id = (Label)rp_subjects.Items[ixx].FindControl("lbl_subject_id");

                    SqlCommand cmd;
                    DataTable dt = mycode.FillData("Select * from Exam_pass_fail_configuration where Session_id='" + ViewState["sesssionid"].ToString() + "' and Class_id='" + ddl_class.SelectedValue + "' and Term_id='" + lbl_terms_id.Text + "'   and Subject='" + lbl_subject_id.Text + "' and Branch_id='" + ViewState["branchid"].ToString() + "' ");
                    if (dt.Rows.Count == 0)
                    {
                        string query = "INSERT INTO Exam_pass_fail_configuration (Session_id,Class_id,Term_id,Subject,Percentage,Branch_id,Created_by,Created_date,Created_idate) values (@Session_id,@Class_id,@Term_id,@Subject,@Percentage,@Branch_id,@Created_by,@Created_date,@Created_idate)";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@Session_id", ViewState["sesssionid"].ToString());
                        cmd.Parameters.AddWithValue("@Class_id", ddl_class.SelectedValue);
                        cmd.Parameters.AddWithValue("@Term_id", lbl_terms_id.Text);
                        cmd.Parameters.AddWithValue("@Subject", lbl_subject_id.Text);
                        cmd.Parameters.AddWithValue("@Percentage", txt_percentage.Text);
                        cmd.Parameters.AddWithValue("@Branch_id", ViewState["branchid"].ToString());
                        cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                        cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                        cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
                        if (My.InsertUpdateData(cmd))
                        {
                        }
                    }
                    else
                    {
                        string query = "update Exam_pass_fail_configuration set Percentage=@Percentage,Updated_by=@Updated_by,Updated_date=@Updated_date,Updated_idate=@Updated_idate where Id=@Id";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@Percentage", txt_percentage.Text);
                        cmd.Parameters.AddWithValue("@Updated_by", ViewState["Userid"].ToString());
                        cmd.Parameters.AddWithValue("@Updated_date", mycode.date());
                        cmd.Parameters.AddWithValue("@Updated_idate", mycode.idate());
                        cmd.Parameters.AddWithValue("@Id", dt.Rows[0]["Id"].ToString());
                        if (My.InsertUpdateData(cmd))
                        {
                        }
                    }
                }
            }
        }


        //=============================
        protected void rp_subjects_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbl_terms_id = ((Label)e.Item.FindControl("lbl_terms_id")) as Label;
                Label lbl_subject_id = ((Label)e.Item.FindControl("lbl_subject_id")) as Label;
                TextBox txt_percentage = ((TextBox)e.Item.FindControl("txt_percentage")) as TextBox;

                string query = "select Percentage from Exam_pass_fail_configuration where Session_id='" + ViewState["sesssionid"].ToString() + "' and Class_id='" + ddl_class.SelectedValue + "' and Term_id='" + lbl_terms_id.Text + "' and Subject='" + lbl_subject_id.Text + "' and Branch_id='" + ViewState["branchid"].ToString() + "'";
                DataTable dt = mycode.FillData(query);
                if (dt.Rows.Count != 0)
                {
                    txt_percentage.Text = dt.Rows[0]["Percentage"].ToString();
                }
            }
        }

        protected void btn_delete_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(My.conn);
                SqlDataAdapter ad = new SqlDataAdapter("select * from Exam_pass_fail_configuration where  Session_id='" + ViewState["sesssionid"].ToString() + "' and Class_id='" + ddl_class.SelectedValue + "' and Branch_id='" + ViewState["branchid"].ToString() + "'", conn);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                DataTable dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    dr.Delete();
                }
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(dt);
                Alertme("Record has been deleted successfully", "success");
                find_term();
            }
            catch (Exception ex)
            {
            }
        }

        protected void btn_save_overall_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_percentage_o.Text == "")
                {
                    Alertme("Please enter term level subject pass %", "warning");
                    txt_percentage_o.Focus();
                }
                else
                {
                    save_over_all();
                    Alertme("Record has been saved successfully", "success");
                }
            }
            catch (Exception ex)
            {
            }
        }


        private void save_over_all()
        {
            SqlCommand cmd;
            DataTable dt = mycode.FillData("Select * from Exam_pass_fail_configuration_over_all where Session_id='" + ViewState["sesssionid"].ToString() + "' and Class_id='" + ddl_class.SelectedValue + "' and Branch_id='" + ViewState["branchid"].ToString() + "' ");
            if (dt.Rows.Count == 0)
            {
                string query = "INSERT INTO Exam_pass_fail_configuration_over_all (Session_id,Class_id,Percentage,Is_active,Branch_id,Created_by,Created_date,Created_idate) values (@Session_id,@Class_id,@Percentage,@Is_active,@Branch_id,@Created_by,@Created_date,@Created_idate)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Session_id", ViewState["sesssionid"].ToString());
                cmd.Parameters.AddWithValue("@Class_id", ddl_class.SelectedValue);
                cmd.Parameters.AddWithValue("@Percentage", txt_percentage_o.Text);
                cmd.Parameters.AddWithValue("@Is_active", 1);
                cmd.Parameters.AddWithValue("@Branch_id", ViewState["branchid"].ToString());
                cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
                if (My.InsertUpdateData(cmd))
                {
                }
            }
            else
            {
                string query = "update Exam_pass_fail_configuration_over_all set Percentage=@Percentage,Updated_by=@Updated_by,Updated_date=@Updated_date,Updated_idate=@Updated_idate where Id=@Id";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Percentage", txt_percentage_o.Text);
                cmd.Parameters.AddWithValue("@Updated_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Updated_date", mycode.date());
                cmd.Parameters.AddWithValue("@Updated_idate", mycode.idate());
                cmd.Parameters.AddWithValue("@Id", dt.Rows[0]["Id"].ToString());
                if (My.InsertUpdateData(cmd))
                {
                }
            }
        }

        protected void chk_individual_sub_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                on_indivisual_checked();
            }
            catch (Exception ex)
            {
            }
        }

        private void on_indivisual_checked()
        {
            if (chk_individual_sub.Checked == true)
            {
              
                pnl_individuasal.Visible = true;
                pnl_over_all.Visible = false;
            }
            else
            {
                pnl_individuasal.Visible = false;
                pnl_over_all.Visible = true;
            }
        }

        protected void btn_delete_overall_Click(object sender, EventArgs e)
        {
            try
            {
                delete_overall();
            }
            catch (Exception ex)
            {
            }
        }

        private void delete_overall()
        {
            SqlConnection conn = new SqlConnection(My.conn);
            SqlDataAdapter ad = new SqlDataAdapter("select * from Exam_pass_fail_configuration_over_all where  Session_id='" + ViewState["sesssionid"].ToString() + "' and Class_id='" + ddl_class.SelectedValue + "' and Branch_id='" + ViewState["branchid"].ToString() + "'", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                dr.Delete();
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
            Alertme("Record has been deleted successfully", "success");
            find_term(); find_over_all_save_data();
        }
    }
}