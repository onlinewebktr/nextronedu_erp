using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
using System.Web.Services;
namespace school_web.Examination_Admin
{
    public partial class grade_system : System.Web.UI.Page
    {
        Examination em = new Examination();
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
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


                    mycode.bind_all_ddl_with_id(ddlsession, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                    ddlsession.SelectedValue = My.get_session_id();
                    Bind_All_Grade_System();

                    mycode.bind_all_ddl_with_id(ddl_current_session, "select Session,session_id from session_details");



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
        protected void btn_find_Click(object sender, EventArgs e)
        {

            if (txt_searchby.Text == "")
            {
                Alertme("Please enter search name", "warning");
            }
            else
            {
                string query = "Select * from Exam_Grade_System where  Grade_Name='" + txt_searchby.Text + "' and Branch_id=" + ViewState["branchid"].ToString() + " and Session_Id=" + ddlsession.SelectedValue + " order by Grade_Name asc";

                DataTable dt = mycode.FillData(query);
                if (dt.Rows.Count == 0)
                {
                    Alertme("Sorry there are no grade added", "warning");
                    grid_grade.DataSource = null;
                    grid_grade.DataBind();

                }
                else
                {

                    grid_grade.DataSource = dt;
                    grid_grade.DataBind();
                }
            }
        }


        private void Bind_All_Grade_System()
        {
            string query = "Select * from Exam_Grade_System where   Branch_id=" + ViewState["branchid"].ToString() + " and Session_Id=" + ddlsession.SelectedValue + " order by Grade_Name asc";

            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no grade added", "warning");
                grid_grade.DataSource = null;
                grid_grade.DataBind();

            }
            else
            {

                grid_grade.DataSource = dt;
                grid_grade.DataBind();
            }
        }






        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lnk.Parent.Parent;
                Label lbl_Grade_System_Id = (Label)row.FindControl("lbl_Grade_System_Id");
                Response.Redirect("Add-Grade-System.aspx?Grade_System_Id=" + lbl_Grade_System_Id.Text, false);
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
                Label lbl_Grade_System_Id = (Label)row.FindControl("lbl_Grade_System_Id");
                mycode.executequery("delete from  Exam_Grade_System where Grade_System_Id='" + lbl_Grade_System_Id.Text + "' and Branch_id='" + ViewState["branchid"].ToString() + "'");
                mycode.executequery("delete from  Exam_Grade_System_Range_Grade where Grade_System_Id='" + lbl_Grade_System_Id.Text + "' and Branch_id='" + ViewState["branchid"].ToString() + "'");
                mycode.executequery("delete from Exam_Grade_System_Mapping_with_Class where Grade_System_Id=" + lbl_Grade_System_Id.Text + " and Branch_id='" + ViewState["branchid"].ToString() + "'");
                Alertme("Grade details has been deleted", "warning");
                Bind_All_Grade_System();
            }
            catch
            {
            }
        }

        protected void lnk_view_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnk.Parent.Parent;
            Label lbl_Grade_System_Id = (Label)row.FindControl("lbl_Grade_System_Id");
            Bind_grid_data_range(lbl_Grade_System_Id.Text);
            Bind_Class_maped(lbl_Grade_System_Id.Text);
            Bind_daTA_GRID_grade_syem(lbl_Grade_System_Id.Text);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);



        }

        private void Bind_daTA_GRID_grade_syem(string Grade_System_Id)
        {
            string query = "Select  * from    Exam_Grade_System     where Grade_System_Id=" + Grade_System_Id + " and Branch_id=" + ViewState["branchid"].ToString() + "   ";

            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                if (dt.Rows[0]["With_Decimal"].ToString() == "True")
                {
                    txt_val1.Text = "With Decimal";
                }
                else
                {

                    txt_val1.Text = "Without Decimal";

                }

                if (dt.Rows[0]["Round_up"].ToString() == "True")
                {
                    txt_va2.Text = "Round-up";
                }
                if (dt.Rows[0]["Round_down"].ToString() == "True")
                {
                    txt_va2.Text = "Round-down";
                }
                if (dt.Rows[0]["Half_Round_Up"].ToString() == "True")
                {
                    txt_va2.Text = "Half Round Up";
                }

                if (dt.Rows[0]["Without_Decimal_Per"].ToString() == "True")
                {
                    txt_va2.Text = "Half Round Down";
                }

            }
        }

        private void Bind_Class_maped(string Grade_System_Id)
        {
            string query = "Select cm.Course_Name  from Add_course_table cm  join  Exam_Grade_System_Mapping_with_Class  gs on cm.course_id=gs.Course_id  where gs.Grade_System_Id=" + Grade_System_Id + " order by  cm.Position ";

            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {

                grid_classmaped.DataSource = null;
                grid_classmaped.DataBind();

            }
            else
            {

                grid_classmaped.DataSource = dt;
                grid_classmaped.DataBind();
            }
        }

        private void Bind_grid_data_range(string Grade_System_Id)
        {
            string query = "Select * from Exam_Grade_System_Range_Grade where Grade_System_Id=" + Grade_System_Id + " and Branch_Id=" + ViewState["branchid"].ToString() + " order by id asc";

            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {

                grid_range.DataSource = null;
                grid_range.DataBind();

            }
            else
            {

                grid_range.DataSource = dt;
                grid_range.DataBind();
            }

        }





        #region WebMethoD
        [WebMethod]
        public static List<string> GetRooPathname(string PathRooT, string Session_id)
        {
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(My.conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select Grade_Name from Exam_Grade_System where Grade_Name LIKE ''+@SearchGetRooPath+'%' and Session_id='" + Session_id + "'  ";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchGetRooPath", PathRooT);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        MobResult.Add(dr["Grade_Name"].ToString());
                    }
                    con.Close();
                    return MobResult;
                }
            }
        }









        #endregion

        protected void ddlsession_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlsession.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
            }
            else
            {
                Bind_All_Grade_System();
            }
        }

        protected void btn_copy_setting_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_current_session.SelectedItem.Text == "Select")
                {
                    Alertme("Please select copy from session.", "warning");
                    ddl_current_session.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalGradE();", true);
                }
                else if (ddl_copy_to_session.SelectedItem.Text == "Select")
                {
                    Alertme("Please select copy to session.", "warning");
                    ddl_copy_to_session.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalGradE();", true);
                }
                else
                {
                    ViewState["succeSS"] = "0";
                    transfer_grade_to_new_session();
                    if (ViewState["succeSS"].ToString() == "1")
                    {
                        Alertme("Grade has been successfully transferred to new session.", "success");
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void transfer_grade_to_new_session()
        {
            DataTable dt = My.dataTable("select * from Exam_Grade_System where Session_Id='" + ddl_current_session.SelectedValue + "' and Branch_id='" + ViewState["branchid"].ToString() + "'");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string gradeiD = Examination.auto_serialS("Grade_System_Id", ViewState["branchid"].ToString());
                    SqlCommand cmd;
                    string query = "INSERT INTO Exam_Grade_System (Session_Id,Grade_Name,Input_Type,Output,Grade_System_Id,With_Decimal,Without_Decimal,Round_up,Round_down,Half_Round_Up,Half_Round_Down,With_Decimal_Per,Without_Decimal_Per,Round_up_Per,Round_down_Per,Half_Round_Up_Per,Half_Round_Down_Per,Branch_id,Created_by,Created_date,Round_Percentage_Checked,Maximum_numbe_decimal,Scholastic_Co_scholastic) values (@Session_Id,@Grade_Name,@Input_Type,@Output,@Grade_System_Id,@With_Decimal,@Without_Decimal,@Round_up,@Round_down,@Half_Round_Up,@Half_Round_Down,@With_Decimal_Per,@Without_Decimal_Per,@Round_up_Per,@Round_down_Per,@Half_Round_Up_Per,@Half_Round_Down_Per,@Branch_id,@Created_by,@Created_date,@Round_Percentage_Checked,@Maximum_numbe_decimal,@Scholastic_Co_scholastic)";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Session_Id", ddl_copy_to_session.SelectedValue);
                    cmd.Parameters.AddWithValue("@Grade_Name", dr["Grade_Name"].ToString());
                    cmd.Parameters.AddWithValue("@Input_Type", dr["Input_Type"].ToString());
                    cmd.Parameters.AddWithValue("@Output", dr["Output"].ToString());
                    cmd.Parameters.AddWithValue("@Grade_System_Id", gradeiD);
                    cmd.Parameters.AddWithValue("@With_Decimal", dr["With_Decimal"].ToString());
                    cmd.Parameters.AddWithValue("@Without_Decimal", dr["Without_Decimal"].ToString());
                    cmd.Parameters.AddWithValue("@Round_up", dr["Round_up"].ToString());
                    cmd.Parameters.AddWithValue("@Round_down", dr["Round_down"].ToString());
                    cmd.Parameters.AddWithValue("@Half_Round_Up", dr["Half_Round_Up"].ToString());
                    cmd.Parameters.AddWithValue("@Half_Round_Down", dr["Half_Round_Down"].ToString());
                    cmd.Parameters.AddWithValue("@With_Decimal_Per", dr["With_Decimal_Per"].ToString());
                    cmd.Parameters.AddWithValue("@Without_Decimal_Per", dr["Without_Decimal_Per"].ToString());
                    cmd.Parameters.AddWithValue("@Round_up_Per", dr["Round_up_Per"].ToString());
                    cmd.Parameters.AddWithValue("@Round_down_Per", dr["Round_down_Per"].ToString());
                    cmd.Parameters.AddWithValue("@Half_Round_Up_Per", dr["Half_Round_Up_Per"].ToString());
                    cmd.Parameters.AddWithValue("@Half_Round_Down_Per", dr["Half_Round_Down_Per"].ToString());
                    cmd.Parameters.AddWithValue("@Branch_id", ViewState["branchid"].ToString());
                    cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@Created_date", mycode.datetime());
                    cmd.Parameters.AddWithValue("@Round_Percentage_Checked", dr["Round_Percentage_Checked"].ToString());
                    cmd.Parameters.AddWithValue("@Maximum_numbe_decimal", dr["Maximum_numbe_decimal"].ToString());
                    cmd.Parameters.AddWithValue("@Scholastic_Co_scholastic", dr["Scholastic_Co_scholastic"].ToString());
                    if (My.InsertUpdateData(cmd))
                    {
                        save_id_Exam_Grade_System_Range_Grade(dr["Grade_System_Id"].ToString(), gradeiD);
                        save_id_Exam_Grade_System_Mapping_with_Class(dr["Grade_System_Id"].ToString(), gradeiD);
                        ViewState["succeSS"] = "1";
                    }
                }
            }
            else
            {
                Alertme("Record not found for selected session.", "warning");
                ddl_current_session.Focus();
            }
        }

        private void save_id_Exam_Grade_System_Mapping_with_Class(string oldGrade_id, string NewgradeiD)
        {
            DataTable dt = My.dataTable("select * from Exam_Grade_System_Mapping_with_Class where Grade_System_Id='" + oldGrade_id + "'");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    SqlCommand cmd;
                    string query = "INSERT INTO Exam_Grade_System_Mapping_with_Class (Grade_System_Id,Course_id) values (@Grade_System_Id,@Course_id)";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Grade_System_Id", NewgradeiD);
                    cmd.Parameters.AddWithValue("@Course_id", dr["Course_id"].ToString());
                    if (My.InsertUpdateData(cmd))
                    {
                    }
                }
            }
        }

        private void save_id_Exam_Grade_System_Range_Grade(string oldGrade_id, string NewgradeiD)
        {
            DataTable dt = My.dataTable("select * from Exam_Grade_System_Range_Grade where Grade_System_Id='" + oldGrade_id + "' and Session_Id='" + ddl_current_session.SelectedValue + "'");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    SqlCommand cmd;
                    string query = "INSERT INTO Exam_Grade_System_Range_Grade (Grade_System_Id,Lower_Range,Upper_Range,Grade,Credits,Creted_date,Creted_by,Branch_Id,Session_Id,Maximum_numbe_decimal) values (@Grade_System_Id,@Lower_Range,@Upper_Range,@Grade,@Credits,@Creted_date,@Creted_by,@Branch_Id,@Session_Id,@Maximum_numbe_decimal)";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Grade_System_Id", NewgradeiD);
                    cmd.Parameters.AddWithValue("@Lower_Range", dr["Lower_Range"].ToString());
                    cmd.Parameters.AddWithValue("@Upper_Range", dr["Upper_Range"].ToString());
                    cmd.Parameters.AddWithValue("@Grade", dr["Grade"].ToString());
                    cmd.Parameters.AddWithValue("@Credits", dr["Credits"].ToString());
                    cmd.Parameters.AddWithValue("@Creted_date", mycode.datetime());
                    cmd.Parameters.AddWithValue("@Creted_by", ViewState["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@Branch_Id", ViewState["branchid"].ToString());
                    cmd.Parameters.AddWithValue("@Session_Id", ddl_copy_to_session.SelectedValue);
                    cmd.Parameters.AddWithValue("@Maximum_numbe_decimal", dr["Maximum_numbe_decimal"].ToString());
                    if (My.InsertUpdateData(cmd))
                    {
                    }
                }
            }
        }

        protected void ddl_current_session_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalGradE();", true);
                mycode.bind_all_ddl_with_id(ddl_copy_to_session, "select Session,session_id from session_details where session_id!='" + ddl_current_session.SelectedValue + "'");
            }
            catch (Exception ex)
            {

            }
        }

    }
}