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
    public partial class Exam_Teram : System.Web.UI.Page
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
                    mycode.bind_all_ddl_with_id(ddlsession, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                    ddlsession.SelectedValue = My.get_session_id();

                    ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                    mycode.bind_all_ddl_with_id(ddl_class, "Select Course_Name,course_id from Add_course_table order by Position asc");
                    mycode.bind_all_ddl_with_id(ddl_current_session, "select Session,session_id from session_details");

                    Bind_All_examterm();
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
        protected void ddlsession_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlsession.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
            }
            else
            {
                Bind_All_examterm();
            }
        }

        private void Bind_All_examterm()
        {
            string query = " Select ed.*,(Select top 1 Grade_Name from Exam_Grade_System where Grade_System_Id=ed.Grade_System_Id) as Grade_Name,cd.Course_Name from Exam_Term_Details ed join Add_course_table cd   on cd.course_id=ed.Class_id  where  ed.Branch_Id=" + ViewState["branchid"].ToString() + " and ed.Session_Id=" + ddlsession.SelectedValue + " order by ed.Sequence_No, cd.Position asc";
            Bind_final_grid(query);
        }

        private void Bind_final_grid(string query)
        {
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no exam term added", "warning");
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
                Label lbl_Term_Id = (Label)row.FindControl("lbl_Term_Id");
                Response.Redirect("Set_Exam_Teram.aspx?Exam_Term_Id=" + lbl_Term_Id.Text, false);
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
                Label lbl_Term_Id = (Label)row.FindControl("lbl_Term_Id");
                mycode.executequery("delete from  Exam_Term_Details where Exam_Term_Id='" + lbl_Term_Id.Text + "' and Branch_Id='" + ViewState["branchid"].ToString() + "'");

                Alertme("Exam details has been deleted", "success");
                Bind_All_examterm();
            }
            catch
            {
            }

        }

        protected void lnk_view_Click(object sender, EventArgs e)
        {

        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            if (ddlsession.SelectedItem.Text == "")
            {
                Alertme("Please select copy from session", "warning");
            }
            else if (txt_searchby.Text == "")
            {
                Alertme("Please enter search name", "warning");
            }
            else
            {
                string query = " Select ed.*,(Select top 1 Grade_Name from Exam_Grade_System where Grade_System_Id=ed.Grade_System_Id) as Grade_Name,cd.Course_Name from Exam_Term_Details ed join Add_course_table cd on cd.course_id=ed.Class_id  where  ed.Term_Name='" + txt_searchby.Text + "' and ed.Branch_Id=" + ViewState["branchid"].ToString() + " and ed.Session_Id=" + ddlsession.SelectedValue + " order by ed.Sequence_No, cd.Position asc";
                Bind_final_grid(query);

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
                    cmd.CommandText = "select distinct Term_Name from Exam_Term_Details where Term_Name LIKE '%'+@SearchGetRooPath+'%' and Session_id='" + Session_id + "'  ";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchGetRooPath", PathRooT);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        MobResult.Add(dr["Term_Name"].ToString());
                    }
                    con.Close();
                    return MobResult;
                }
            }
        }




        #endregion

        protected void grid_grade_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_Istatus = (Label)e.Row.FindControl("lbl_Istatus");

                Label lbl_status = (Label)e.Row.FindControl("lbl_status");


                if (lbl_Istatus.Text == "True")
                {
                    lbl_status.Text = "Active";
                    lbl_status.CssClass = "badge badge-success ml-2";
                }
                else
                {
                    lbl_status.Text = "Inactive";
                    lbl_status.CssClass = "badge badge-danger ml-2";
                }



            }
        }

        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlsession.SelectedItem.Text == "")
            {
                Alertme("Please select session", "warning");
            }
            else if (ddl_class.SelectedItem.Text == "All")
            {
                Bind_All_examterm();
            }
            else
            {
                string query = " Select ed.*,(Select top 1 Grade_Name from Exam_Grade_System where Grade_System_Id=ed.Grade_System_Id) as Grade_Name,cd.Course_Name from Exam_Term_Details ed join Add_course_table cd on cd.course_id=ed.Class_id where  ed.Class_id='" + ddl_class.SelectedValue + "' and ed.Branch_Id=" + ViewState["branchid"].ToString() + " and ed.Session_Id=" + ddlsession.SelectedValue + " order by ed.Sequence_No, cd.Position asc ";
                Bind_final_grid(query);
            }
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                pnl_term_details.Visible = false;
                if (ddl_current_session.SelectedItem.Text == "Select")
                {
                    Alertme("Please copy from session", "warning");
                    ddl_current_session.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                if (ddl_copy_to.SelectedItem.Text == "Select")
                {
                    Alertme("Please choose copy to next term/session", "warning");
                    ddl_copy_to.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                else
                {
                    if (ddl_copy_to.SelectedItem.Text == "Term")
                    {
                        copy_to_SessionDV.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    }
                    else
                    {
                        mycode.bind_all_ddl_with_id(ddl_copy_to_session, "select Session,session_id from session_details where session_id!='" + ddl_current_session.SelectedValue + "'");
                        copy_to_SessionDV.Visible = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }


        protected void ddl_current_session_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                pnl_term_details.Visible = false;
                mycode.bind_all_ddl_with_id(ddl_copy_to_session, "select Session,session_id from session_details where session_id!='" + ddl_current_session.SelectedValue + "'");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (Exception ex)
            {

            }
        }

        protected void btn_find_term_for_copy_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                if (ddl_current_session.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session.", "warning");
                    ddl_current_session.Focus();
                }
                else if (ddl_copy_to.SelectedItem.Text == "Select")
                {
                    Alertme("Please select copy to.", "warning");
                    ddl_copy_to.Focus();
                }
                else
                {
                    if (ddl_copy_to.SelectedItem.Text == "Session")
                    {
                        if (ddl_copy_to_session.SelectedItem.Text == "Session")
                        {
                            Alertme("Please select copy to session.", "warning");
                            ddl_copy_to_session.Focus();
                            return;
                        }
                    }

                    string query = " Select ed.*,(Select top 1 Grade_Name from Exam_Grade_System where Grade_System_Id=ed.Grade_System_Id) as Grade_Name,cd.Course_Name from Exam_Term_Details ed join Add_course_table cd   on cd.course_id=ed.Class_id  where  ed.Branch_Id=" + ViewState["branchid"].ToString() + " and ed.Session_Id=" + ddl_current_session.SelectedValue + " order by ed.Sequence_No, cd.Position asc";
                    DataTable dt = mycode.FillData(query);
                    if (dt.Rows.Count == 0)
                    {
                        pnl_term_details.Visible = false;
                        Alertme("Sorry there are no data list exist", "warning");
                        rd_view.DataSource = null;
                        rd_view.DataBind();
                    }
                    else
                    {
                        pnl_term_details.Visible = true;
                        rd_view.DataSource = dt;
                        rd_view.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    RepeaterItem item = e.Item;
                    DropDownList ddl_grade = (DropDownList)e.Item.FindControl("ddl_grade");
                    Label lbl_grade_name = (Label)e.Item.FindControl("lbl_grade_name");
                    Label lbl_grade_id = (Label)e.Item.FindControl("lbl_grade_id");
                    if (ddl_copy_to.SelectedItem.Text == "Session")
                    {
                        mycode.bind_all_ddl_with_id(ddl_grade, "select Grade_Name,Grade_System_Id from Exam_Grade_System where Session_Id='" + ddl_copy_to_session.SelectedValue + "'");
                        try
                        {
                            string grade_id = get_exam_grade_id(lbl_grade_name.Text);
                            ddl_grade.SelectedValue = grade_id;
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    else
                    {
                        mycode.bind_all_ddl_with_id(ddl_grade, "select Grade_Name,Grade_System_Id from Exam_Grade_System where Session_Id='" + ddl_current_session.SelectedValue + "'");
                        try
                        {
                            ddl_grade.SelectedValue = lbl_grade_id.Text;
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }

        private string get_exam_grade_id(string grade_name)
        {
            string rtrNS = "0";
            DataTable dt = My.dataTable("select Grade_Name,Grade_System_Id from Exam_Grade_System where Session_Id='" + ddl_copy_to_session.SelectedValue + "' and Grade_Name='" + grade_name + "'");
            if (dt.Rows.Count > 0)
            {
                rtrNS = dt.Rows[0]["Grade_System_Id"].ToString();
            }
            return rtrNS;
        }

        protected void btn_copy_term_details_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["IsSaved"] = "0";
                copy_term_details();
                if (ViewState["IsSaved"].ToString() == "1")
                {
                    if (ddl_copy_to.Text == "Term")
                    {
                        Alertme("Term has been copied successfully for new term.", "success");
                    }
                    else
                    {
                        Alertme("Term has been copied successfully for next session.", "success");
                    }
                    Bind_All_examterm();
                }
            }
            catch (Exception ex)
            {
            }
        }



        private void copy_term_details()
        {
            string validationTerm = "1";
            int growcountS = rd_view.Items.Count;
            for (int i = 0; i < growcountS; i++)
            {
                TextBox txt_term_name = (TextBox)rd_view.Items[i].FindControl("txt_term_name");
                if (txt_term_name.Text == "")
                {
                    validationTerm = "0";
                }
            }
            if (validationTerm == "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                Alertme("Please enter term name.", "warning");
                return;
            }


            string validationGrade = "1";
            int growcount = rd_view.Items.Count;
            for (int iS = 0; iS < growcount; iS++)
            {
                DropDownList ddl_grade = (DropDownList)rd_view.Items[iS].FindControl("ddl_grade");
                if (ddl_grade.SelectedValue == "0")
                {
                    validationGrade = "0";
                }
            }
            if (validationGrade == "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                Alertme("Please choose grade.", "warning");
                return;
            }



            int growcountT = rd_view.Items.Count;
            for (int iT = 0; iT < growcountT; iT++)
            {
                TextBox txt_term_name = (TextBox)rd_view.Items[iT].FindControl("txt_term_name");
                DropDownList ddl_grade = (DropDownList)rd_view.Items[iT].FindControl("ddl_grade");
                Label lbl_Id = (Label)rd_view.Items[iT].FindControl("lbl_Id");
                Label lbl_class_id = (Label)rd_view.Items[iT].FindControl("lbl_class_id");
                if (ddl_copy_to.Text == "Term")
                {
                    if (mycode.IsUserExist("select Term_Name from Exam_Term_Details where (Term_Name='" + txt_term_name.Text + "' or Short_Name='" + txt_term_name.Text + "') and Session_Id='" + ddl_current_session.SelectedValue + "' and Class_id='" + lbl_class_id.Text + "' and Branch_id='" + ViewState["branchid"].ToString() + "'"))
                    {
                        DataTable dtS = mycode.FillData("select * from Exam_Term_Details where Id='" + lbl_Id.Text + "'");
                        if (dtS.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtS.Rows.Count; i++)
                            {
                                string Exam_Term_Id = Examination.auto_serialS("Exam_Term_Id", ViewState["branchid"].ToString());
                                SqlCommand cmd;
                                string query = "INSERT INTO Exam_Term_Details (Session_Id,Branch_Id,Istatus,Term_Name,Short_Name,Sequence_No,Grade_System_Id,Maximum_Marks,Cut_Off_Percentage,Calculation_Type,Is_Advanced_Advanced_Setting,Consider_best,Pass_criteria,Created_By,Created_Date_Time,Exam_Term_Id,Is_Mandatory_to_pass,Class_id,No_of_Class) values (@Session_Id,@Branch_Id,@Istatus,@Term_Name,@Short_Name,@Sequence_No,@Grade_System_Id,@Maximum_Marks,@Cut_Off_Percentage,@Calculation_Type,@Is_Advanced_Advanced_Setting,@Consider_best,@Pass_criteria,@Created_By,@Created_Date_Time,@Exam_Term_Id,@Is_Mandatory_to_pass,@Class_id,@No_of_Class)";
                                cmd = new SqlCommand(query);
                                cmd.Parameters.AddWithValue("@Session_Id", ddl_current_session.SelectedValue);
                                cmd.Parameters.AddWithValue("@Branch_Id", ViewState["branchid"].ToString());
                                cmd.Parameters.AddWithValue("@Istatus", dtS.Rows[i]["Istatus"].ToString());
                                cmd.Parameters.AddWithValue("@Term_Name", txt_term_name.Text);
                                cmd.Parameters.AddWithValue("@Short_Name", txt_term_name.Text);
                                cmd.Parameters.AddWithValue("@Sequence_No", dtS.Rows[i]["Sequence_No"].ToString());
                                cmd.Parameters.AddWithValue("@Grade_System_Id", ddl_grade.SelectedValue);
                                cmd.Parameters.AddWithValue("@Maximum_Marks", dtS.Rows[i]["Maximum_Marks"].ToString());
                                cmd.Parameters.AddWithValue("@Cut_Off_Percentage", dtS.Rows[i]["Cut_Off_Percentage"].ToString());
                                cmd.Parameters.AddWithValue("@Calculation_Type", dtS.Rows[i]["Calculation_Type"].ToString());
                                cmd.Parameters.AddWithValue("@Is_Advanced_Advanced_Setting", dtS.Rows[i]["Is_Advanced_Advanced_Setting"].ToString());
                                cmd.Parameters.AddWithValue("@Consider_best", dtS.Rows[i]["Consider_best"].ToString());
                                cmd.Parameters.AddWithValue("@Pass_criteria", dtS.Rows[i]["Pass_criteria"].ToString());
                                cmd.Parameters.AddWithValue("@Created_By", ViewState["Userid"].ToString());
                                cmd.Parameters.AddWithValue("@Created_Date_Time", My.getdate1());
                                cmd.Parameters.AddWithValue("@Exam_Term_Id", Exam_Term_Id);
                                cmd.Parameters.AddWithValue("@Is_Mandatory_to_pass", dtS.Rows[i]["Is_Mandatory_to_pass"].ToString());
                                cmd.Parameters.AddWithValue("@Class_id", dtS.Rows[i]["Class_id"].ToString());
                                cmd.Parameters.AddWithValue("@No_of_Class", dtS.Rows[i]["No_of_Class"].ToString());
                                if (My.InsertUpdateData(cmd))
                                {
                                    ViewState["IsSaved"] = "1";
                                }
                            }
                        }
                    }
                    else
                    {
                        Alertme("Term name or short term name already exist please enter another term name or short term name.", "warning");
                        txt_term_name.Focus();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    }
                }
                else
                {
                    DataTable dtS = mycode.FillData("select * from Exam_Term_Details where Id='" + lbl_Id.Text + "'");
                    if (dtS.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtS.Rows.Count; i++)
                        {
                            if (mycode.IsUserExist("select Term_Name from Exam_Term_Details where Term_Name='" + txt_term_name.Text + "' and Session_Id=" + ddl_copy_to_session.SelectedValue + " and Branch_Id='" + ViewState["branchid"].ToString() + "' and Class_id='" + dtS.Rows[i]["Class_id"].ToString() + "'"))
                            {
                                string Exam_Term_Id = Examination.auto_serialS("Exam_Term_Id", ViewState["branchid"].ToString());
                                SqlCommand cmd;
                                string query = "INSERT INTO Exam_Term_Details (Session_Id,Branch_Id,Istatus,Term_Name,Short_Name,Sequence_No,Grade_System_Id,Maximum_Marks,Cut_Off_Percentage,Calculation_Type,Is_Advanced_Advanced_Setting,Consider_best,Pass_criteria,Created_By,Created_Date_Time,Exam_Term_Id,Is_Mandatory_to_pass,Class_id,No_of_Class) values (@Session_Id,@Branch_Id,@Istatus,@Term_Name,@Short_Name,@Sequence_No,@Grade_System_Id,@Maximum_Marks,@Cut_Off_Percentage,@Calculation_Type,@Is_Advanced_Advanced_Setting,@Consider_best,@Pass_criteria,@Created_By,@Created_Date_Time,@Exam_Term_Id,@Is_Mandatory_to_pass,@Class_id,@No_of_Class)";
                                cmd = new SqlCommand(query);
                                cmd.Parameters.AddWithValue("@Session_Id", ddl_copy_to_session.SelectedValue);
                                cmd.Parameters.AddWithValue("@Branch_Id", ViewState["branchid"].ToString());
                                cmd.Parameters.AddWithValue("@Istatus", dtS.Rows[i]["Istatus"].ToString());
                                cmd.Parameters.AddWithValue("@Term_Name", txt_term_name.Text);
                                cmd.Parameters.AddWithValue("@Short_Name", txt_term_name.Text);
                                cmd.Parameters.AddWithValue("@Sequence_No", dtS.Rows[i]["Sequence_No"].ToString());
                                cmd.Parameters.AddWithValue("@Grade_System_Id", ddl_grade.SelectedValue);
                                cmd.Parameters.AddWithValue("@Maximum_Marks", dtS.Rows[i]["Maximum_Marks"].ToString());
                                cmd.Parameters.AddWithValue("@Cut_Off_Percentage", dtS.Rows[i]["Cut_Off_Percentage"].ToString());
                                cmd.Parameters.AddWithValue("@Calculation_Type", dtS.Rows[i]["Calculation_Type"].ToString());
                                cmd.Parameters.AddWithValue("@Is_Advanced_Advanced_Setting", dtS.Rows[i]["Is_Advanced_Advanced_Setting"].ToString());
                                cmd.Parameters.AddWithValue("@Consider_best", dtS.Rows[i]["Consider_best"].ToString());
                                cmd.Parameters.AddWithValue("@Pass_criteria", dtS.Rows[i]["Pass_criteria"].ToString());
                                cmd.Parameters.AddWithValue("@Created_By", ViewState["Userid"].ToString());
                                cmd.Parameters.AddWithValue("@Created_Date_Time", My.getdate1());
                                cmd.Parameters.AddWithValue("@Exam_Term_Id", Exam_Term_Id);
                                cmd.Parameters.AddWithValue("@Is_Mandatory_to_pass", dtS.Rows[i]["Is_Mandatory_to_pass"].ToString());
                                cmd.Parameters.AddWithValue("@Class_id", dtS.Rows[i]["Class_id"].ToString());
                                cmd.Parameters.AddWithValue("@No_of_Class", dtS.Rows[i]["No_of_Class"].ToString());
                                if (My.InsertUpdateData(cmd))
                                {
                                    ViewState["IsSaved"] = "1";
                                }
                            }
                            else
                            {
                                Alertme("Term name or short term name already exist please enter another term name or short term name.", "warning");
                                txt_term_name.Focus();
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                            }
                        }
                    }
                }
            }
        }
    }
}