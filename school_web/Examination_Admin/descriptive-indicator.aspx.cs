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
    public partial class descriptive_indicator1 : System.Web.UI.Page
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


                        ViewState["std_type"] = "1";
                        ViewState["ActivitY"] = "0";
                        check_status();
                        find_grades();

                        ViewState["Grade"] = "A";
                        grade_active();

                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_User");
            }
        }

        private void grade_active()
        {
            int growcount = rp_grades.Items.Count;
            for (int ix = 0; ix < growcount; ix++)
            {
                LinkButton lnk_grades = (LinkButton)rp_grades.Items[ix].FindControl("lnk_grades");
                if (ViewState["Grade"].ToString() == lnk_grades.Text)
                {
                    lnk_grades.CssClass = "chosen-btns-active chosen-btns";
                }
            }
        }

        private void find_grades()
        {
            string query = "select distinct Grade as Grade_name from Exam_Grade_System_Range_Grade where Branch_Id='" + ViewState["branchid"].ToString() + "'   ";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                rp_grades.DataSource = null;
                rp_grades.DataBind();
            }
            else
            {
                rp_grades.DataSource = dt;
                rp_grades.DataBind();
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
            }
            catch (Exception ex)
            {
            }
        }

        private void find_term()
        {
            mycode.bind_all_ddl_with_id(ddl_term, "select Term_Name,Exam_Term_Id from Exam_Term_Details where Class_id='" + ddl_class.SelectedValue + "' and Session_Id='" + ViewState["sesssionid"].ToString() + "' and Branch_Id='" + ViewState["branchid"].ToString() + "' and Istatus=1 order by Term_Name asc");
        }

        protected void ddl_term_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                find_exam_structure();
            }
            catch (Exception ex)
            {
            }
        }


        private void find_exam_structure()
        {
            mycode.bind_all_ddl_with_id(ddl_exam_structure, "select Assessment_Name,Assessment_Id from Exam_Assessment_Details where Class_id='" + ddl_class.SelectedValue + "' and Exam_Term_Id='" + ddl_term.SelectedValue + "' and Session_Id='" + ViewState["sesssionid"].ToString() + "' and Branch_Id='" + ViewState["branchid"].ToString() + "' and Istatus=1");
        }

        protected void lnk_scholastic_Click(object sender, EventArgs e)
        {
            ViewState["std_type"] = "1";
            check_status(); find_subjects();
        }

        private void check_status()
        {
            if (ViewState["std_type"].ToString() == "1")
            {
                lnk_scholastic.CssClass = "chosen-btns-active chosen-btns";
                lnk_co_scholastic.CssClass = "chosen-btns";
            }
            else
            {
                lnk_co_scholastic.CssClass = "chosen-btns-active chosen-btns";
                lnk_scholastic.CssClass = "chosen-btns";
            }
        }

        protected void lnk_co_scholastic_Click(object sender, EventArgs e)
        {
            ViewState["std_type"] = "2";
            check_status(); find_subjects();
        }

        protected void ddl_exam_structure_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                find_subjects();
            }
            catch (Exception ex)
            { }
        }

        private void find_subjects()
        {
            string std_type = "Co-Scholastic";
            if (ViewState["std_type"].ToString() == "1")
            {
                std_type = "Scholastic";
            }
            string query = "";
            if (chk_activity.Checked == true)
            {
                query = "Select sm.Subject_id,sbl.Subject_Activity_Name as Subject_name,sbl.Subject_Sub_Level_Id from Subject_Master sm join Exam_Subject_Sub_Level sbl on sm.Subject_id=sbl.Subject_id where sbl.Class_id='" + ddl_class.SelectedValue + "' and sm.Subject_Type_Scholastic_Co_Scholastic='" + std_type + "' and sbl.Exam_Term_Id='" + ddl_term.SelectedValue + "' and sbl.Assessment_Id='" + ddl_exam_structure.SelectedValue + "' and sbl.Session_Id='" + ViewState["sesssionid"].ToString() + "' and sbl.Branch_Id='" + ViewState["branchid"].ToString() + "' order by sbl.Sequence_No asc";
            }
            else
            {
                query = "Select DISTINCT sm.*,'0' as Subject_Sub_Level_Id from Subject_Master sm join Exam_Subject_Sub_Level sbl on sm.Subject_id=sbl.Subject_id where sbl.Class_id='" + ddl_class.SelectedValue + "' and sm.Subject_Type_Scholastic_Co_Scholastic='" + std_type + "' and sbl.Exam_Term_Id='" + ddl_term.SelectedValue + "' and sbl.Assessment_Id='" + ddl_exam_structure.SelectedValue + "' and sbl.Session_Id='" + ViewState["sesssionid"].ToString() + "' and sbl.Branch_Id='" + ViewState["branchid"].ToString() + "'";
            }
            DataTable dt = mycode.FillData(query);
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

        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbl_first_letter = ((Label)e.Item.FindControl("lbl_first_letter")) as Label;
                Label lbl_subject_name = ((Label)e.Item.FindControl("lbl_subject_name")) as Label;
                lbl_first_letter.Text = lbl_subject_name.Text.Substring(0, 1);
            }
        }

        protected void chk_activity_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chk_activity.Checked == true)
                {
                    ViewState["ActivitY"] = "1";
                    bind_subject();
                    ddl_subject.Enabled = true;

                    if (ddl_subject.SelectedItem.Text == "Select")
                    {

                    }
                    else
                    {
                        find_subjects();
                    }

                }
                else
                {
                    ViewState["ActivitY"] = "0";
                    ddl_subject.DataSource = null;
                    ddl_subject.DataBind();
                    ddl_subject.Items.Insert(0, new ListItem("Select", "0"));
                    ddl_subject.Enabled = false;
                    ddl_subject.CssClass = "form-select find-dv-txtbx";
                    if (ddl_subject.SelectedItem.Text == "Select")
                    {

                    }
                    else
                    {
                        find_subjects();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void bind_subject()
        {
            mycode.bind_all_ddl_with_id(ddl_subject, "Select DISTINCT sm.Subject_name,sm.Subject_id from Subject_Master sm join Exam_Subject_Sub_Level sbl on sm.Subject_id=sbl.Subject_id where sbl.Class_id='" + ddl_class.SelectedValue + "' and sm.Subject_Type_Scholastic_Co_Scholastic='Scholastic' and sbl.Exam_Term_Id='" + ddl_term.SelectedValue + "' and sbl.Assessment_Id='" + ddl_exam_structure.SelectedValue + "' and sbl.Session_Id='" + ViewState["sesssionid"].ToString() + "' and sbl.Branch_Id='" + ViewState["branchid"].ToString() + "'");
        }

        protected void ddl_subject_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                find_subjects();
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnk_grades_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                LinkButton lnk_grades = (LinkButton)row.FindControl("lnk_grades") as LinkButton;

                foreach (RepeaterItem item in rp_grades.Items)
                {
                    LinkButton lnk_gradess = (LinkButton)item.FindControl("lnk_grades") as LinkButton;
                    lnk_gradess.CssClass = "chosen-btns";
                }
                lnk_grades.CssClass = "chosen-btns-active chosen-btns";
                ViewState["Grade"] = lnk_grades.Text;
                bind_remarks();
            }
            catch
            {
            }
        }

        protected void btn_save_remarks_Click(object sender, EventArgs e)
        {
            if (txt_remark.Text == "")
            {
                Alertme("Please enter remark.", "Warning");
                txt_remark.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            else
            {
                save_remarks();
                emplty_remark_form();
                bind_remarks();
                Alertme("Record has been created successfully.", "success");
            }
        }



        private void save_remarks()
        {

            SqlCommand cmd;
            if (btn_save_remarks.Text == "Save")
            {
                string query = "INSERT INTO Exam_remark_master (Class_id,Term_id,Exam_assesment,Subject_id,Is_activity,Activity,Session_id,Branch,Created_by,Created_date,Created_idate,Grade,Remark,Assesment_Id) values (@Class_id,@Term_id,@Exam_assesment,@Subject_id,@Is_activity,@Activity,@Session_id,@Branch,@Created_by,@Created_date,@Created_idate,@Grade,@Remark,@Assesment_Id)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Session_id", ViewState["sesssionid"].ToString());
                cmd.Parameters.AddWithValue("@Class_id", ddl_class.SelectedValue);
                cmd.Parameters.AddWithValue("@Term_id", ddl_term.SelectedValue);
                cmd.Parameters.AddWithValue("@Exam_assesment", ddl_exam_structure.SelectedValue);
                cmd.Parameters.AddWithValue("@Subject_id", ViewState["SubjectiD"].ToString());
                if (ViewState["ActivitY"].ToString() == "0")
                {
                    cmd.Parameters.AddWithValue("@Is_activity", 0);
                    cmd.Parameters.AddWithValue("@Activity", "0");
                    cmd.Parameters.AddWithValue("@Assesment_Id", "0");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Is_activity", 1);
                    cmd.Parameters.AddWithValue("@Activity", ViewState["AssesMentName"].ToString());
                    cmd.Parameters.AddWithValue("@Assesment_Id", ViewState["AssesMentId"].ToString());
                }
                cmd.Parameters.AddWithValue("@Grade", ViewState["Grade"].ToString());
                cmd.Parameters.AddWithValue("@Branch", ViewState["branchid"].ToString());
                cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
                cmd.Parameters.AddWithValue("@Remark", txt_remark.Text);
                if (My.InsertUpdateData(cmd))
                {
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            else
            {
                string query = "update Exam_remark_master set Remark=@Remark,Updated_by=@Updated_by,Updated_date=@Updated_date,Updated_idate=@Updated_idate where Id=@Id";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Remark", txt_remark.Text);
                cmd.Parameters.AddWithValue("@Updated_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Updated_date", mycode.date());
                cmd.Parameters.AddWithValue("@Updated_idate", mycode.idate());
                cmd.Parameters.AddWithValue("@Id", hd_id.Value);
                if (My.InsertUpdateData(cmd))
                {
                }
            }
        }

        private void emplty_remark_form()
        {
            txt_remark.Text = "";
            btn_save_remarks.Text = "Save";
        }
        protected void lnk_edt_for_remarks_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_subject_id = (Label)row.FindControl("lbl_subject_id") as Label;
                Label lbl_subject_name = (Label)row.FindControl("lbl_subject_name") as Label;
                Label lbl_subject_sub_level_Id = (Label)row.FindControl("lbl_subject_sub_level_Id") as Label;

                lbl_head_on_update.Text = lbl_subject_name.Text;
                ViewState["SubjectiD"] = lbl_subject_id.Text;
                ViewState["AssesMentName"] = lbl_subject_name.Text;
                ViewState["AssesMentId"] = lbl_subject_sub_level_Id.Text;
                bind_remarks();
                pnl_remarks_panel.Visible = true;
                pnl_choose_subjects.Visible = false;
            }
            catch
            {
            }
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_remark = (Label)row.FindControl("lbl_remark");
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                hd_id.Value = lbl_Id.Text;
                txt_remark.Text = lbl_remark.Text;
                btn_save_remarks.Text = "Update";
                btn_cancel.Visible = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch
            {

            }
        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            Label lbl_Id = (Label)row.FindControl("lbl_Id");

            SqlConnection conn = new SqlConnection(My.conn);
            SqlDataAdapter ad = new SqlDataAdapter("select * from Exam_remark_master where  id='" + lbl_Id.Text + "'", conn);
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
            bind_remarks();
        }

        private void bind_remarks()
        {
            string qry = "";
            if (ViewState["ActivitY"].ToString() == "0")
            {
                qry = "Select * from Exam_remark_master where Session_id='" + ViewState["sesssionid"].ToString() + "' and Class_id='" + ddl_class.SelectedValue + "' and Term_id='" + ddl_term.SelectedValue + "' and Exam_assesment='" + ddl_exam_structure.SelectedValue + "' and Subject_id='" + ViewState["SubjectiD"].ToString() + "' and  Branch='" + ViewState["branchid"].ToString() + "' and Grade='" + ViewState["Grade"].ToString() + "'";
            }
            else
            {
                qry = "Select * from Exam_remark_master where Session_id='" + ViewState["sesssionid"].ToString() + "' and Class_id='" + ddl_class.SelectedValue + "' and Term_id='" + ddl_term.SelectedValue + "' and Exam_assesment='" + ddl_exam_structure.SelectedValue + "' and Subject_id='" + ViewState["SubjectiD"].ToString() + "' and  Branch='" + ViewState["branchid"].ToString() + "' and Grade='" + ViewState["Grade"].ToString() + "' and Assesment_Id='" + ViewState["AssesMentId"].ToString() + "'";
            }
            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {
                rp_remarks.DataSource = null;
                rp_remarks.DataBind();
            }
            else
            {
                rp_remarks.DataSource = dt;
                rp_remarks.DataBind();
            }
        }

        protected void lnk_back_to_choose_sub_Click(object sender, EventArgs e)
        {
            pnl_remarks_panel.Visible = false;
            pnl_choose_subjects.Visible = true;
        }

        protected void rd_assign_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton lnk = (RadioButton)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            RadioButton rd_assign = (RadioButton)row.FindControl("rd_assign") as RadioButton;
            Label lbl_remark = (Label)row.FindControl("lbl_remark") as Label;
            Label lbl_Id = (Label)row.FindControl("lbl_Id") as Label;

            foreach (RepeaterItem item in rp_remarks.Items)
            {
                RadioButton rd_assigns = (RadioButton)item.FindControl("rd_assign") as RadioButton;
                rd_assigns.Checked = false;
            }
            rd_assign.Checked = true;


            save_assigned_remarks(lbl_remark.Text, lbl_Id.Text);
            Alertme("Auto Assignee Value Is Updated Successfully.", "success");
            //=================================  
        }

        private void save_assigned_remarks(string Remarks, string remrk_row_id)
        {
            string std_type = "Co-Scholastic";
            if (ViewState["std_type"].ToString() == "1")
            {
                std_type = "Scholastic";
            }

            string query = "";
            if (chk_activity.Checked == true)
            {
                query = "Select * from Subject_Master sm join Exam_Subject_Sub_Level sbl on sm.Subject_id=sbl.Subject_id where sbl.Class_id='" + ddl_class.SelectedValue + "' and sm.Subject_Type_Scholastic_Co_Scholastic='" + std_type + "' and sbl.Exam_Term_Id='" + ddl_term.SelectedValue + "' and sbl.Assessment_Id='" + ddl_exam_structure.SelectedValue + "' and sbl.Session_Id='" + ViewState["sesssionid"].ToString() + "' and sbl.Branch_Id='" + ViewState["branchid"].ToString() + "' and sm.Subject_id='" + ddl_subject.SelectedValue + "' and sbl.Subject_Sub_Level_Id='" + ViewState["AssesMentId"].ToString() + "' order by sbl.Sequence_No asc";
            }
            else
            {
                query = "Select * from Subject_Master sm join Exam_Subject_Sub_Level sbl on sm.Subject_id=sbl.Subject_id where sbl.Class_id='" + ddl_class.SelectedValue + "' and sm.Subject_Type_Scholastic_Co_Scholastic='" + std_type + "' and sbl.Exam_Term_Id='" + ddl_term.SelectedValue + "'  and sm.Subject_id='" + ViewState["SubjectiD"].ToString() + "' and sbl.Assessment_Id='" + ddl_exam_structure.SelectedValue + "' and sbl.Session_Id='" + ViewState["sesssionid"].ToString() + "' and sbl.Branch_Id='" + ViewState["branchid"].ToString() + "'";
            }
            SqlDataAdapter ad = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Brand_Master");
            DataTable dt = ds.Tables[0];
            int rowcount = ds.Tables[0].Rows.Count;
            if (rowcount == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    SqlCommand cmd;
                    string qrys = "select Id from Exam_assigned_remark_master where Session_id='" + ViewState["sesssionid"].ToString() + "' and Class_id='" + ddl_class.SelectedValue + "' and Term_id='" + ddl_term.SelectedValue + "' and Exam_assesment='" + ddl_exam_structure.SelectedValue + "' and Assesment_Id='" + dr["Subject_Sub_Level_Id"].ToString() + "' and Grade='" + ViewState["Grade"].ToString() + "' and Branch='" + ViewState["branchid"].ToString() + "' ";
                    DataTable dts = mycode.FillData(qrys);
                    if (dts.Rows.Count == 0)
                    {
                        string querys = "INSERT INTO Exam_assigned_remark_master (Class_id,Term_id,Exam_assesment,Subject_id,Activity,Is_activity,Session_id,Branch,Created_by,Created_date,Created_idate,Grade,Remark,Assesment_Id,Remark_id) values (@Class_id,@Term_id,@Exam_assesment,@Subject_id,@Activity,@Is_activity,@Session_id,@Branch,@Created_by,@Created_date,@Created_idate,@Grade,@Remark,@Assesment_Id,@Remark_id)";
                        cmd = new SqlCommand(querys);
                        cmd.Parameters.AddWithValue("@Session_id", ViewState["sesssionid"].ToString());
                        cmd.Parameters.AddWithValue("@Class_id", ddl_class.SelectedValue);
                        cmd.Parameters.AddWithValue("@Term_id", ddl_term.SelectedValue);
                        cmd.Parameters.AddWithValue("@Exam_assesment", ddl_exam_structure.SelectedValue);
                        cmd.Parameters.AddWithValue("@Subject_id", dr["Subject_id"].ToString());
                        cmd.Parameters.AddWithValue("@Activity", dr["Subject_Activity_Name"].ToString());
                        cmd.Parameters.AddWithValue("@Assesment_Id", dr["Subject_Sub_Level_Id"].ToString());

                        cmd.Parameters.AddWithValue("@Grade", ViewState["Grade"].ToString());
                        cmd.Parameters.AddWithValue("@Branch", ViewState["branchid"].ToString());
                        cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                        cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                        cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
                        cmd.Parameters.AddWithValue("@Remark", Remarks);
                        cmd.Parameters.AddWithValue("@Is_activity", ViewState["ActivitY"].ToString());
                        cmd.Parameters.AddWithValue("@Remark_id", remrk_row_id);
                        if (My.InsertUpdateData(cmd))
                        {
                        }
                    }
                    else
                    {
                        string querys = "update Exam_assigned_remark_master set Remark=@Remark,Is_activity=@Is_activity,Remark_id=@Remark_id, Updated_by=@Updated_by, Updated_date=@Updated_date, Updated_idate=@Updated_idate where Id=@Id";
                        cmd = new SqlCommand(querys);
                        cmd.Parameters.AddWithValue("@Updated_by", ViewState["Userid"].ToString());
                        cmd.Parameters.AddWithValue("@Updated_date", mycode.date());
                        cmd.Parameters.AddWithValue("@Updated_idate", mycode.idate());
                        cmd.Parameters.AddWithValue("@Remark", Remarks);
                        cmd.Parameters.AddWithValue("@Is_activity", ViewState["ActivitY"].ToString());
                        cmd.Parameters.AddWithValue("@Remark_id", remrk_row_id);
                        cmd.Parameters.AddWithValue("@Id", dt.Rows[0]["Id"].ToString());
                        if (My.InsertUpdateData(cmd))
                        {
                        }
                    }
                }
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            emplty_remark_form();
        }

        protected void rp_remarks_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                RadioButton rd_assign = ((RadioButton)e.Item.FindControl("rd_assign")) as RadioButton;
                Label lbl_Id = ((Label)e.Item.FindControl("lbl_Id")) as Label;
                string qrys = "";
                if (chk_activity.Checked == true)
                {
                    qrys = "select Remark_id from Exam_assigned_remark_master where Session_id='" + ViewState["sesssionid"].ToString() + "' and Class_id='" + ddl_class.SelectedValue + "' and Term_id='" + ddl_term.SelectedValue + "' and Exam_assesment='" + ddl_exam_structure.SelectedValue + "' and Subject_id='" + ddl_subject.SelectedValue + "' and Assesment_Id='" + ViewState["AssesMentId"].ToString() + "' and Grade='" + ViewState["Grade"].ToString() + "' and Branch='" + ViewState["branchid"].ToString() + "'  and Is_activity=" + ViewState["ActivitY"].ToString() + "";
                }
                else
                {
                    qrys = "select Remark_id from Exam_assigned_remark_master where Session_id='" + ViewState["sesssionid"].ToString() + "' and Class_id='" + ddl_class.SelectedValue + "' and Term_id='" + ddl_term.SelectedValue + "' and Exam_assesment='" + ddl_exam_structure.SelectedValue + "' and Subject_id='" + ViewState["SubjectiD"].ToString() + "' and Grade='" + ViewState["Grade"].ToString() + "' and Branch='" + ViewState["branchid"].ToString() + "'  and Is_activity=" + ViewState["ActivitY"].ToString() + "";
                }
                DataTable dts = mycode.FillData(qrys);
                if (dts.Rows.Count != 0)
                {
                    if (dts.Rows[0]["Remark_id"].ToString() == lbl_Id.Text)
                    {
                        rd_assign.Checked = true;
                    }
                }
            }
        }
    }
}