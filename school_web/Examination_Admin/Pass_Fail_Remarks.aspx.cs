using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
namespace school_web.Examination_Admin
{
    public partial class Pass_Fail_Remarks : System.Web.UI.Page
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
                    ViewState["sessionid"] = My.get_session_id();
                    ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                    mycode.bind_all_ddl_with_id(ddl_class, "Select Course_Name,course_id from Add_course_table order by Position asc");
                    ddl_class.SelectedValue = My.get_top_one_class();

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
        #region add save

        protected void btn_Submit_Click(object sender, EventArgs e)
        {

            if (ddl_class.SelectedItem.Text == "Select")
            {
                Alertme("Please select classs", "warning");
            }
            else
            {



                try
                {

                    string query = "Select * from  Exam_Pass_Fail_Remarks where Session_Id=" + ViewState["sessionid"].ToString() + " and Branch_id=" + ViewState["branchid"].ToString() + " and Class_Id=" + ddl_class.SelectedValue + "";
                    SqlCommand cmd;
                    DataTable dt1 = mycode.FillData(query);
                    if (dt1.Rows.Count == 0)
                    {

                        string query2 = "INSERT INTO Exam_Pass_Fail_Remarks (Session_Id,Branch_id,Class_Id,Pass_Remarks,Failed_Remarks,Is_Checked_Failed_Subject,Grace_marks,Is_Checked_Grace_marks_Subject,Grace_Marks_Indicator,Created_By,Created_Date) values (@Session_Id,@Branch_id,@Class_Id,@Pass_Remarks,@Failed_Remarks,@Is_Checked_Failed_Subject,@Grace_marks,@Is_Checked_Grace_marks_Subject,@Grace_Marks_Indicator,@Created_By,@Created_Date)";

                        cmd = new SqlCommand(query2);
                        cmd.Parameters.AddWithValue("@Session_Id", ViewState["sessionid"].ToString());
                        cmd.Parameters.AddWithValue("@Branch_id", ViewState["branchid"].ToString());
                        cmd.Parameters.AddWithValue("@Class_Id", ddl_class.SelectedValue);
                        cmd.Parameters.AddWithValue("@Pass_Remarks", txt_pass.Text);
                        cmd.Parameters.AddWithValue("@Failed_Remarks", txt_failed.Text);
                        if (chk_remarks_failed.Checked == true)
                        {
                            cmd.Parameters.AddWithValue("@Is_Checked_Failed_Subject", 1);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Is_Checked_Failed_Subject", 0);
                        }
                        cmd.Parameters.AddWithValue("@Grace_marks", txt_grace_remarks.Text);

                        if (chk_graceremarks.Checked == true)
                        {
                            cmd.Parameters.AddWithValue("@Is_Checked_Grace_marks_Subject", 1);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Is_Checked_Grace_marks_Subject", 0);
                        }
                        cmd.Parameters.AddWithValue("@Grace_Marks_Indicator", txt_grace_marks_indicator.Text);

                        cmd.Parameters.AddWithValue("@Created_By", ViewState["Userid"].ToString());
                        cmd.Parameters.AddWithValue("@Created_Date", My.getdate1());

                        if (My.InsertUpdateData(cmd))
                        {

                            Alertme("Pass and Fail Remarks has been saved successfully done", "success");

                            Bind_data_for_remarks();


                        }
                    }
                    else
                    {
                        string id = dt1.Rows[0]["Id"].ToString();
                        string query2 = "Update Exam_Pass_Fail_Remarks set Session_Id=@Session_Id,Branch_id=@Branch_id,Class_Id=@Class_Id,Pass_Remarks=@Pass_Remarks,Failed_Remarks=@Failed_Remarks,Is_Checked_Failed_Subject=@Is_Checked_Failed_Subject,Grace_marks=@Grace_marks,Is_Checked_Grace_marks_Subject=@Is_Checked_Grace_marks_Subject,Grace_Marks_Indicator=@Grace_Marks_Indicator,Updated_by=@Updated_by,Updated_date=@Updated_date where Id = @Id";

                        cmd = new SqlCommand(query2);
                        cmd.Parameters.AddWithValue("@Session_Id", ViewState["sessionid"].ToString());
                        cmd.Parameters.AddWithValue("@Branch_id", ViewState["branchid"].ToString());
                        cmd.Parameters.AddWithValue("@Class_Id", ddl_class.SelectedValue);
                        cmd.Parameters.AddWithValue("@Pass_Remarks", txt_pass.Text);
                        cmd.Parameters.AddWithValue("@Failed_Remarks", txt_failed.Text);
                        if (chk_remarks_failed.Checked == true)
                        {
                            cmd.Parameters.AddWithValue("@Is_Checked_Failed_Subject", 1);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Is_Checked_Failed_Subject", 0);
                        }
                        cmd.Parameters.AddWithValue("@Grace_marks", txt_grace_remarks.Text);

                        if (chk_graceremarks.Checked == true)
                        {
                            cmd.Parameters.AddWithValue("@Is_Checked_Grace_marks_Subject", 1);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Is_Checked_Grace_marks_Subject", 0);
                        }
                        cmd.Parameters.AddWithValue("@Grace_Marks_Indicator", txt_grace_marks_indicator.Text);
                        cmd.Parameters.AddWithValue("@Updated_by", ViewState["Userid"].ToString());
                        cmd.Parameters.AddWithValue("@Updated_date", My.getdate1());
                        cmd.Parameters.AddWithValue("@Id", id);
                        if (My.InsertUpdateData(cmd))
                        {
                            Alertme("Pass and Fail Remarks has been saved successfully done", "success");
                            Bind_data_for_remarks();
                        }
                    }
                }
                catch
                {

                }




            }

        }

        private void Bind_data_for_remarks()
        {
            string query = "Select * from  Exam_Pass_Fail_Remarks where Session_Id=" + ViewState["sessionid"].ToString() + " and Branch_id=" + ViewState["branchid"].ToString() + " and Class_Id=" + ddl_class.SelectedValue + "";
            SqlCommand cmd;
            DataTable dt1 = mycode.FillData(query);
            if (dt1.Rows.Count == 0)
            {
                txt_pass.Text = "";
                txt_failed.Text = "";
                chk_remarks_failed.Checked = false;
                txt_grace_remarks.Text="";
                chk_graceremarks.Checked = false;
                txt_grace_marks_indicator.Text = "";
            }
            else
            {
                txt_pass.Text = dt1.Rows[0]["Pass_Remarks"].ToString();
                txt_failed.Text = dt1.Rows[0]["Failed_Remarks"].ToString();
                if (dt1.Rows[0]["Is_Checked_Failed_Subject"].ToString() == "True")
                {
                    chk_remarks_failed.Checked = true;
                }
                else
                {
                    chk_remarks_failed.Checked = false;
                }

                txt_grace_remarks.Text = dt1.Rows[0]["Grace_marks"].ToString();
                if (dt1.Rows[0]["Is_Checked_Grace_marks_Subject"].ToString() == "True")
                {
                    chk_graceremarks.Checked = true;
                    
                }
                else
                {
                    chk_graceremarks.Checked = false;
                }


                txt_grace_marks_indicator.Text = dt1.Rows[0]["Grace_Marks_Indicator"].ToString();
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {

        }
        #endregion









        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lnk.Parent.Parent;
                Label lbl_id = (Label)row.FindControl("lbl_id");
                Label lbl_Lower_Range = (Label)row.FindControl("lbl_Lower_Range");
                Label lbl_Upper_Range = (Label)row.FindControl("lbl_Upper_Range");
                Label lbl_Remarks = (Label)row.FindControl("lbl_Remarks");
                hd_id.Value = lbl_id.Text;

                txt_lowerrange.Text = lbl_Lower_Range.Text;
                txt_upper_range.Text = lbl_Upper_Range.Text;
                txt_remarks.Text = lbl_Remarks.Text;
                btn_save_range.Text = "Update";
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
                mycode.executequery("delete from  Exam_Pass_Fail_Percentage_Range where Id='" + lbl_id.Text + "'");
                btn_save_range.Text = "Add";
                txt_lowerrange.Text = "";
                txt_upper_range.Text = "";
                txt_remarks.Text = "";
                Bind_grade_range();
                Alertme("Range has been deleted sucessfully", "success");
            }
            catch
            {
            }
        }

        protected void btn_save_range_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd;
                if (btn_save_range.Text == "Add")
                {
                    #region add range
                    double Lower_Range = Convert.ToDouble(txt_lowerrange.Text);
                    double upper_Range = Convert.ToDouble(txt_upper_range.Text);



                    DataTable dt = mycode.FillData("Select * from Exam_Pass_Fail_Percentage_Range where Class_Id=" + ddl_class.SelectedValue + " and Branch_id=" + ViewState["branchid"].ToString() + " and Session_Id=" + ViewState["sessionid"].ToString() + "  and cast(Lower_Range as float)=" + Lower_Range + "");
                    if (dt.Rows.Count == 0)
                    {

                        DataTable dt1 = mycode.FillData("Select * from Exam_Pass_Fail_Percentage_Range where Class_Id=" + ddl_class.SelectedValue + " and Branch_Id=" + ViewState["branchid"].ToString() + " and Session_Id=" + ViewState["sessionid"].ToString() + "  and cast(Upper_Range as float)=" + upper_Range + "");
                        if (dt1.Rows.Count == 0)
                        {


                            string query = "INSERT INTO Exam_Pass_Fail_Percentage_Range (Lower_Range,Upper_Range,Remarks,Created_By,Created_Date,Session_Id,Branch_id,Class_Id) values (@Lower_Range,@Upper_Range,@Remarks,@Created_By,@Created_Date,@Session_Id,@Branch_id,@Class_Id)";
                            cmd = new SqlCommand(query);


                            cmd.Parameters.AddWithValue("@Lower_Range", txt_lowerrange.Text);
                            cmd.Parameters.AddWithValue("@Upper_Range", txt_upper_range.Text);
                            cmd.Parameters.AddWithValue("@Remarks", txt_remarks.Text);

                            cmd.Parameters.AddWithValue("@Created_Date", My.getdate1());
                            cmd.Parameters.AddWithValue("@Created_By", ViewState["Userid"].ToString());

                            cmd.Parameters.AddWithValue("@Branch_id", ViewState["branchid"].ToString());
                            cmd.Parameters.AddWithValue("@Session_Id", ViewState["sessionid"].ToString());
                            cmd.Parameters.AddWithValue("@Class_Id", ddl_class.SelectedValue);
                            if (My.InsertUpdateData(cmd))
                            {
                                Alertme("range has been add Successfully.", "success");
                                btn_save_range.Text = "Add";
                                txt_lowerrange.Text = "";
                                txt_upper_range.Text = "";
                                txt_remarks.Text = "";
                                btn_save_range.Text = "Add";

                                Bind_grade_range();
                            }



                        }
                        else
                        {
                            Alertme("Upper range already exists", "warning");
                        }
                    }
                    else
                    {
                        Alertme("Lower range already exists", "warning");

                    }

                    #endregion
                }
                else if (btn_save_range.Text == "Update")
                {
                    #region update range
                    double Lower_Range = Convert.ToDouble(txt_lowerrange.Text);
                    double upper_Range = Convert.ToDouble(txt_upper_range.Text);

                    DataTable dt = mycode.FillData("Select * from Exam_Pass_Fail_Percentage_Range where  Branch_Id=" + ViewState["branchid"].ToString() + " and Session_Id=" + ViewState["sessionid"].ToString() + "  and cast(Lower_Range as float)=" + Lower_Range + " and Id!=" + hd_id.Value + "");
                    if (dt.Rows.Count == 0)
                    {

                        DataTable dt1 = mycode.FillData("Select * from Exam_Pass_Fail_Percentage_Range where   Branch_Id=" + ViewState["branchid"].ToString() + " and Session_Id=" + ViewState["sessionid"].ToString() + "  and cast(Upper_Range as float)=" + upper_Range + " and Id!=" + hd_id.Value + "");
                        if (dt1.Rows.Count == 0)
                        {


                            string query = "Update Exam_Pass_Fail_Percentage_Range set Lower_Range=@Lower_Range,Upper_Range=@Upper_Range,Remarks=@Remarks,Updated_By=@Updated_By,Updated_Date=@Updated_Date,Class_Id=@Class_Id where Id = @Id";
                            cmd = new SqlCommand(query);
                            cmd.Parameters.AddWithValue("@Lower_Range", txt_lowerrange.Text);
                            cmd.Parameters.AddWithValue("@Upper_Range", txt_upper_range.Text);
                            cmd.Parameters.AddWithValue("@Remarks", txt_remarks.Text);
                            cmd.Parameters.AddWithValue("@Updated_Date", My.getdate1());
                            cmd.Parameters.AddWithValue("@Updated_By", ViewState["Userid"].ToString());
                            cmd.Parameters.AddWithValue("@Class_Id", ddl_class.SelectedValue);
                            cmd.Parameters.AddWithValue("@Id", hd_id.Value);

                            if (My.InsertUpdateData(cmd))
                            {
                                Alertme("range has been add Successfully.", "success");
                                btn_save_range.Text = "Add";
                                txt_lowerrange.Text = "";
                                txt_upper_range.Text = "";
                                txt_remarks.Text = "";
                                btn_save_range.Text = "Add";
                                Bind_grade_range();
                            }



                        }
                        else
                        {
                            Alertme("Upper range already exists", "warning");
                        }
                    }
                    else
                    {
                        Alertme("Lower range already exists", "warning");

                    }

                    #endregion
                }
                else
                {
                }
            }
            catch
            {
            }
        }

        private void Bind_grade_range()
        {
            DataTable dt1 = mycode.FillData("Select * from Exam_Pass_Fail_Percentage_Range where Class_Id=" + ddl_class.SelectedValue + " and Branch_Id=" + ViewState["branchid"].ToString() + " and Session_Id=" + ViewState["sessionid"].ToString() + "  order by id asc");
            if (dt1.Rows.Count == 0)
            {
                grid_range.DataSource = null;
                grid_range.DataBind();
            }
            else
            {
                grid_range.DataSource = dt1;
                grid_range.DataBind();
            }
        }

        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_class.SelectedItem.Text == "Select")
            {
                Alertme("Please select Class", "warning");
            }
            else
            {
                Bind_data_for_remarks();
                Bind_grade_range();

            }
        }


    }
}