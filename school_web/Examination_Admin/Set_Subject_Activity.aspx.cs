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
    public partial class Set_Subject_Activity : System.Web.UI.Page
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
                    chk_isactive.Checked = true;
                    ViewState["Userid"] = Session["Admin"].ToString();
                    ViewState["sessionid"] = My.get_session_id();
                    txt_start_date.Text = mycode.date();
                    txt_enddate.Text = mycode.fiftendaysnext();
                    ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                    mycode.bind_all_ddl_with_id(ddl_class, "Select Course_Name,course_id from Add_course_table order by Position asc");
                    if (Request.QueryString["Subject_Sub_Level_Id"] != null)
                    {
                        ViewState["Subject_Sub_Level_Id"] = Request.QueryString["Subject_Sub_Level_Id"].ToString();
                        process_active("1");
                        Bind_Data_edit();
                    }
                    else
                    {
                        ViewState["Subject_Sub_Level_Id"] = Examination.auto_serialS("Subject_Sub_Level_Id", ViewState["branchid"].ToString());
                        process_active("1");
                        Bind_Data_edit();
                    }
                }
            }
        }

        private void Bind_Data_edit()
        {
            string query = "Select essl.*,(Select top 1 Session from session_details where session_id=essl.Session_Id) as sessionname,(Select top 1 Subject_name from Subject_Master where Subject_id=essl.Subject_id and course_id=essl.Class_id) as Subject_name  from Exam_Subject_Sub_Level essl where   essl.Session_Id='" + ViewState["sessionid"].ToString() + "' and essl.Branch_Id=" + ViewState["branchid"].ToString() + "  and essl.Subject_Sub_Level_Id=" + ViewState["Subject_Sub_Level_Id"].ToString() + "  ";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                ViewState["countgrid"] = "0";
                year.InnerText = My.get_session();
                examname.InnerText = "";
                // Bind_chalid_data();
            }
            else
            {
                try
                {
                    txt_start_date.Text = dt.Rows[0]["Start_Date_Marks"].ToString();
                    txt_enddate.Text = dt.Rows[0]["End_Date_Marks"].ToString();
                }
                catch
                {
                }
                ddl_assessment.SelectedValue = dt.Rows[0]["Assessment_Id"].ToString();
                ViewState["Subject_Sub_Level_Id"] = dt.Rows[0]["Subject_Sub_Level_Id"].ToString();
                ViewState["Subject_id"] = dt.Rows[0]["Subject_id"].ToString();
                ViewState["Assessment_Subject_Id"] = dt.Rows[0]["Assessment_Subject_Id"].ToString();
                examname.InnerText = dt.Rows[0]["Subject_Activity_Name"].ToString();
                year.InnerText = dt.Rows[0]["sessionname"].ToString();
                ViewState["Exam_Term_Id"] = dt.Rows[0]["Exam_Term_Id"].ToString();
                ViewState["Assessment_Id"] = dt.Rows[0]["Assessment_Id"].ToString();
                txt_Name.Text = dt.Rows[0]["Subject_Activity_Name"].ToString();
                txt_Short_Name.Text = dt.Rows[0]["Short_Name"].ToString();
                txt_Sequence_No.Text = dt.Rows[0]["Sequence_No"].ToString();

                //try
                //{
                //    ddl_gradesystem.SelectedValue = dt.Rows[0]["Grade_System_Id"].ToString();
                //}
                //catch (Exception ex) { }
                txt_maximummarks.Text = dt.Rows[0]["Maximum_Marks"].ToString();
                txt_cutoff.Text = dt.Rows[0]["Cut_Off_Percentage"].ToString();
                ddl_calculation_logic.Text = dt.Rows[0]["Calculation_Type"].ToString();
                if (dt.Rows[0]["Is_Advanced_Advanced_Setting"].ToString() == "True")
                {
                    chk_per.Checked = true;
                    advancedSetting.Visible = true;
                    ddl_consider_best.Text = dt.Rows[0]["Consider_best"].ToString();
                    ddl_pass_criteria.Text = dt.Rows[0]["Pass_criteria"].ToString();
                }
                else
                {
                    chk_per.Checked = false;
                    advancedSetting.Visible = false;

                }
                if (dt.Rows[0]["Istatus"].ToString() == "True")
                {
                    chk_isactive.Checked = true;
                }
                else
                {
                    chk_isactive.Checked = false;
                }

                if (dt.Rows[0]["Is_Distinction"].ToString() == "True")
                {
                    distinction.Visible = true;
                    chk_add_distinction.Checked = true;
                }
                else
                {
                    distinction.Visible = false;
                    chk_add_distinction.Checked = false;
                }
                ddl_class.SelectedValue = dt.Rows[0]["Class_id"].ToString();
                mycode.bind_all_ddl_with_id(ddl_examterm, "Select Term_Name,Exam_Term_Id from Exam_Term_Details where Class_id='" + ddl_class.SelectedValue + "' and Session_Id='" + ViewState["sessionid"].ToString() + "' and Branch_Id=" + ViewState["branchid"].ToString() + " order by Sequence_No asc");
                ddl_examterm.SelectedValue = dt.Rows[0]["Exam_Term_Id"].ToString();
                mycode.bind_all_ddl_with_id(ddl_assessment, "Select Assessment_Name,Assessment_Id from Exam_Assessment_Details where Class_id='" + ddl_class.SelectedValue + "' and Session_Id='" + ViewState["sessionid"].ToString() + "' and Branch_Id=" + ViewState["branchid"].ToString() + " and Exam_Term_Id=" + ddl_examterm.SelectedValue + " order by Sequence_No asc");
                ddl_assessment.SelectedValue = dt.Rows[0]["Assessment_Id"].ToString();
                mycode.bind_all_ddl_with_id(ddl_subject, "Select sm.Subject_name,sm.Subject_id from Exam_Assessment_Subject_Mapping_Details easm join Subject_Master sm on easm.Class_id=sm.course_id and easm.Subject_id=sm.Subject_id and easm.Branch_Id=sm.Branch_id where easm.Class_id='" + ddl_class.SelectedValue + "' and easm.Session_Id='" + ViewState["sessionid"].ToString() + "' and easm.Branch_Id=" + ViewState["branchid"].ToString() + " and easm.Exam_Term_Id=" + ddl_examterm.SelectedValue + " and easm.Assessment_Id=" + ddl_assessment.SelectedValue + " order by easm.Sequence_No asc");
                ddl_subject.SelectedValue = dt.Rows[0]["Subject_id"].ToString();
                mycode.bind_all_ddl_with_id_no_select(ddl_gradesystem, "Select distinct egs.Grade_Name,egs.Grade_System_Id from Exam_Grade_System  egs join Exam_Grade_System_Mapping_with_Class egsmc on egs.Grade_System_Id = egsmc.Grade_System_Id where egsmc.Course_id='" + ddl_class.SelectedValue + "' and  egs.Session_Id='" + ViewState["sessionid"].ToString() + "'  order by egs.Grade_Name asc");
                try
                {
                    ddl_gradesystem.SelectedValue = dt.Rows[0]["Grade_System_Id"].ToString();
                }
                catch (Exception ex) { }

                Bind_chalid_data();

                try
                {
                    ddl_consider_best.Text = dt.Rows[0]["Is_Mandatory_to_pass"].ToString();
                    ddl_pass_criteria.Text = dt.Rows[0]["Pass_criteria"].ToString();
                }
                catch
                {
                }

            }


        }

        private void Bind_chalid_data()
        {
            string query2 = "Select easmd.* from Exam_Subject_Sub_Level easmd  where easmd.Exam_Term_Id=" + ViewState["Exam_Term_Id"].ToString() + " and easmd.Assessment_Id='" + ViewState["Assessment_Id"].ToString() + "' and easmd.Branch_Id=" + ViewState["branchid"].ToString() + " and easmd.Session_Id=" + ViewState["sessionid"].ToString() + "  and Subject_Sub_Level_Id!=" + ViewState["Subject_Sub_Level_Id"].ToString() + " and Subject_id='" + ddl_subject.SelectedValue + "'  order by easmd.Sequence_No asc";



            DataTable dt2 = mycode.FillData(query2);
            if (dt2.Rows.Count == 0)
            {
                ViewState["countgrid"] = "0";
                sublevel.Visible = false;
                grid_subject_assesment.DataSource = null;
                grid_subject_assesment.DataBind();
            }
            else
            {
                ViewState["countgrid"] = dt2.Rows.Count.ToString();
                sublevel.Visible = true;
                grid_subject_assesment.DataSource = dt2;
                grid_subject_assesment.DataBind();
            }

            List<string> ddlbind = new List<string>();
            List<string> ddlbind2 = new List<string>();

            ddl_consider_best.Items.Clear();
            ddl_pass_criteria.Items.Clear();
            ddlbind2.Add("None");
            ddl_pass_criteria.DataSource = ddlbind2;
            ddl_pass_criteria.DataBind();
            int j = 1;
            int k = 0;
            for (var i = 0; i < Convert.ToInt32(ViewState["countgrid"].ToString()); i++)
            {
                CheckBox chk_per = (CheckBox)grid_subject_assesment.Rows[i].FindControl("chk_per");


                if (chk_per.Checked == true)
                {





                    ddlbind.Add(j.ToString());
                    ddl_consider_best.DataSource = ddlbind;
                    ddl_consider_best.DataBind();



                    j++;
                    k++;

                }


            }

            int l = 1;
            for (var i = 0; i < Convert.ToInt32(ViewState["countgrid"].ToString()); i++)
            {
                CheckBox chk_per = (CheckBox)grid_subject_assesment.Rows[i].FindControl("chk_per");


                if (chk_per.Checked == true)
                {
                    string item = l.ToString() + " OF " + (k);
                    ddlbind2.Add(item.ToString());

                    ddl_pass_criteria.DataSource = ddlbind2;
                    ddl_pass_criteria.DataBind();
                    l++;
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

        #region ddl selection
        private void process_active(string type)
        {
            if (type == "1")
            {
                a1.Attributes.Add("class", "stepper-item active");
                a2.Attributes.Add("class", "stepper-item");
                a3.Attributes.Add("class", "stepper-item");






                pnl_Basic.Visible = true;
                pnl_Define_Logic.Visible = false;
                pn_Calculation.Visible = false;

            }
            else if (type == "2")
            {
                a1.Attributes.Add("class", "stepper-item completed");
                a2.Attributes.Add("class", "stepper-item active");
                a3.Attributes.Add("class", "stepper-item");

                pnl_Basic.Visible = false;
                pnl_Define_Logic.Visible = true;
                pn_Calculation.Visible = false;

            }
            else if (type == "3")
            {
                a1.Attributes.Add("class", "stepper-item completed");
                a2.Attributes.Add("class", "stepper-item completed");
                a3.Attributes.Add("class", "stepper-item active");




                pnl_Basic.Visible = false;
                pnl_Define_Logic.Visible = false;
                pn_Calculation.Visible = true;

            }
            else if (type == "4")
            {
                a1.Attributes.Add("class", "stepper-item completed");
                a2.Attributes.Add("class", "stepper-item completed");
                a3.Attributes.Add("class", "stepper-item completed");




                pnl_Basic.Visible = false;
                pnl_Define_Logic.Visible = false;
                pn_Calculation.Visible = true;

            }

        }



        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_class.SelectedItem.Text == "")
            {
                Alertme("Please select class", "warning");
            }
            else
            {
                mycode.bind_all_ddl_with_id(ddl_examterm, "Select Term_Name,Exam_Term_Id from Exam_Term_Details where Class_id='" + ddl_class.SelectedValue + "' and Session_Id='" + ViewState["sessionid"].ToString() + "' and Branch_Id=" + ViewState["branchid"].ToString() + " order by Sequence_No asc");
                mycode.bind_all_ddl_with_id_no_select(ddl_gradesystem, "Select distinct egs.Grade_Name,egs.Grade_System_Id from Exam_Grade_System  egs join Exam_Grade_System_Mapping_with_Class egsmc on egs.Grade_System_Id = egsmc.Grade_System_Id where egsmc.Course_id='" + ddl_class.SelectedValue + "' and egs.Session_Id='" + ViewState["sessionid"].ToString() + "' order by egs.Grade_Name asc");
            }
        }

        protected void ddl_examterm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_class.SelectedItem.Text == "")
            {
                Alertme("Please select class", "warning");
            }
            else if (ddl_examterm.SelectedItem.Text == "Select")
            {
                Alertme("Please select exam term", "warning");
            }
            else
            {
                mycode.bind_all_ddl_with_id(ddl_assessment, "Select Assessment_Name,Assessment_Id from Exam_Assessment_Details where Class_id='" + ddl_class.SelectedValue + "' and Session_Id='" + ViewState["sessionid"].ToString() + "' and Branch_Id=" + ViewState["branchid"].ToString() + " and Exam_Term_Id=" + ddl_examterm.SelectedValue + " order by Sequence_No asc");
            }
        }

        protected void ddl_assessment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_class.SelectedItem.Text == "")
            {
                Alertme("Please select class", "warning");
            }
            else if (ddl_examterm.SelectedItem.Text == "Select")
            {
                Alertme("Please select exam term", "warning");
            }
            else if (ddl_assessment.SelectedItem.Text == "Select")
            {
                Alertme("Please select assessment", "warning");
            }
            else
            {
                mycode.bind_all_ddl_with_id(ddl_subject, "Select sm.Subject_name,sm.Subject_id from Exam_Assessment_Subject_Mapping_Details easm join Subject_Master sm on easm.Class_id=sm.course_id and easm.Subject_id=sm.Subject_id and easm.Branch_Id=sm.Branch_id where easm.Class_id='" + ddl_class.SelectedValue + "' and easm.Session_Id='" + ViewState["sessionid"].ToString() + "' and easm.Branch_Id=" + ViewState["branchid"].ToString() + " and easm.Exam_Term_Id=" + ddl_examterm.SelectedValue + " and easm.Assessment_Id=" + ddl_assessment.SelectedValue + " order by easm.Sequence_No asc");
            }
        }
        #endregion

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Subject_Activity.aspx", false);
        }

        protected void btn_next_1_2_Click(object sender, EventArgs e)
        {
            if (ddl_class.SelectedItem.Text == "")
            {
                Alertme("Please select class", "warning");
            }
            else if (ddl_examterm.SelectedItem.Text == "Select")
            {
                Alertme("Please select exam term", "warning");
            }
            else if (ddl_assessment.SelectedItem.Text == "Select")
            {
                Alertme("Please select assessment", "warning");
            }
            else if (ddl_subject.SelectedItem.Text == "Select")
            {
                Alertme("Please select subject", "warning");
            }

            else if (txt_start_date.Text == "")
            {
                Alertme("Please enter marks entry date from ", "warning");
            }
            else if (txt_enddate.Text == "")
            {
                Alertme("Please enter marks entry date from ", "warning");
            }

            else
            {
                int idate = mycode.ConvertStringToiDateup(txt_start_date.Text);
                int idate2 = mycode.ConvertStringToiDateup(txt_enddate.Text);

                if (idate > idate2)
                {
                    Alertme("End date cannot be less than start date.", "warning");
                }
                else
                {

                    ViewState["Assessment_Subject_Id"] = em.get_Assessment_Subject_Id(ViewState["sessionid"].ToString(), ViewState["branchid"].ToString(), ddl_class.SelectedValue, ddl_examterm.SelectedValue, ddl_assessment.SelectedValue, ddl_subject.SelectedValue);
                    ViewState["Exam_Term_Id"] = ddl_examterm.SelectedValue;
                    ViewState["Assessment_Id"] = ddl_assessment.SelectedValue;
                    Bind_chalid_data();
                    string query = "Select essl.*  from Exam_Subject_Sub_Level essl where   essl.Session_Id='" + ViewState["sessionid"].ToString() + "' and essl.Branch_Id=" + ViewState["branchid"].ToString() + " and Class_id=" + ddl_class.SelectedValue + " and Exam_Term_Id=" + ddl_examterm.SelectedValue + " and Assessment_Id=" + ddl_assessment.SelectedValue + " and Subject_id=" + ddl_subject.SelectedValue + " and Assessment_Subject_Id=" + ViewState["Assessment_Subject_Id"].ToString() + " and Subject_Activity_Name='" + txt_Name.Text + "'      and essl.Subject_Sub_Level_Id!=" + ViewState["Subject_Sub_Level_Id"].ToString() + "  ";
                    DataTable dt2 = mycode.FillData(query);
                    if (dt2.Rows.Count == 0)
                    {
                        examname.InnerText = txt_Name.Text;
                        process_active("2");
                    }
                    else
                    {
                        Alertme("Sorry! Your enterd subject activity alrady added", "warning");

                    }
                }
            }


        }
        protected void btn_back_2_1_Click(object sender, EventArgs e)
        {
            process_active("1");
        }
        protected void btn_back_3_2_Click(object sender, EventArgs e)
        {
            process_active("2");
        }
        protected void btn_Next_2_3_Click(object sender, EventArgs e)
        {
            if (ddl_gradesystem.SelectedItem.Text == "Select")
            {
                Alertme("Please select grade system", "warning");
            }
            else if (txt_maximummarks.Text == "")
            {
                Alertme("Please enter maximum marks", "warning");
            }
            else if (txt_cutoff.Text == "")
            {
                Alertme("Please enter cut off percentage", "warning");
            }
            else
            {

                //if (My.toDouble(txt_maximummarks.Text) > My.toDouble(txt_cutoff.Text))
                //{

                if (chk_add_distinction.Checked == true)
                {
                    if (txt_distinctionmarks.Text == "")
                    {
                        Alertme("Please enter distinction marks", "warning");
                    }
                    else
                    {
                        process_active("3");

                    }
                }
                else
                {
                    process_active("3");
                }

                //}
                //else
                //{
                //    Alertme("Please enter pass marks always less then maximum marks", "warning");
                //}
            }
        }

        protected void chk_per_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_per.Checked == true)
            {
                advancedSetting.Visible = true;
            }
            else
            {
                advancedSetting.Visible = false;

            }
        }

        protected void chk_per_CheckedChanged1(object sender, EventArgs e)
        {
            List<string> ddlbind = new List<string>();
            List<string> ddlbind2 = new List<string>();
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.Parent.Parent;
            ddl_consider_best.Items.Clear();
            ddl_pass_criteria.Items.Clear();
            ddlbind2.Add("None");
            ddl_pass_criteria.DataSource = ddlbind2;
            ddl_pass_criteria.DataBind();
            int j = 1;
            int k = 0;
            for (var i = 0; i < Convert.ToInt32(ViewState["countgrid"].ToString()); i++)
            {
                CheckBox chk_per = (CheckBox)grid_subject_assesment.Rows[i].FindControl("chk_per");


                if (chk_per.Checked == true)
                {





                    ddlbind.Add(j.ToString());
                    ddl_consider_best.DataSource = ddlbind;
                    ddl_consider_best.DataBind();



                    j++;
                    k++;

                }


            }

            int l = 1;
            for (var i = 0; i < Convert.ToInt32(ViewState["countgrid"].ToString()); i++)
            {
                CheckBox chk_per = (CheckBox)grid_subject_assesment.Rows[i].FindControl("chk_per");


                if (chk_per.Checked == true)
                {
                    string item = l.ToString() + " OF " + (k);
                    ddlbind2.Add(item.ToString());

                    ddl_pass_criteria.DataSource = ddlbind2;
                    ddl_pass_criteria.DataBind();
                    l++;
                }

            }

        }

        protected void grid_subject_assesment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_Is_Mandatory_to_pass = (Label)e.Row.FindControl("lbl_Is_Mandatory_to_pass");

                CheckBox chk_per1 = (CheckBox)e.Row.FindControl("chk_per1");


                if (lbl_Is_Mandatory_to_pass.Text == "True")
                {
                    chk_per1.Checked = true;

                }
                else
                {
                    chk_per1.Checked = false;

                }

                Label lbl_Select_Data = (Label)e.Row.FindControl("lbl_Select_Data");

                CheckBox chk_per = (CheckBox)e.Row.FindControl("chk_per");


                if (lbl_Select_Data.Text == "True")
                {
                    chk_per.Checked = true;

                }

                else
                {
                    chk_per.Checked = false;

                }

            }
        }

        protected void btn_final_submit_Click(object sender, EventArgs e)
        {
            string query2 = "";
            SqlCommand cmd;
            try
            {


                string query = "Select essl.*  from Exam_Subject_Sub_Level essl where   essl.Session_Id='" + ViewState["sessionid"].ToString() + "' and essl.Branch_Id=" + ViewState["branchid"].ToString() + "  and essl.Subject_Sub_Level_Id=" + ViewState["Subject_Sub_Level_Id"].ToString() + "  ";
                DataTable dt2 = mycode.FillData(query);
                if (dt2.Rows.Count == 0)
                {
                    ViewState["Assessment_Subject_Id"] = em.get_Assessment_Subject_Id(ViewState["sessionid"].ToString(), ViewState["branchid"].ToString(), ddl_class.SelectedValue, ddl_examterm.SelectedValue, ddl_assessment.SelectedValue, ddl_subject.SelectedValue);
                    query2 = "INSERT INTO Exam_Subject_Sub_Level (Session_Id,Branch_Id,Istatus,Exam_Term_Id,Assessment_Id,Subject_id,Assessment_Subject_Id,Short_Name,Sequence_No,Grade_System_Id,Maximum_Marks,Cut_Off_Percentage,Calculation_Type,Is_Advanced_Advanced_Setting,Consider_best,Pass_criteria,Created_By,Created_Date_Time,Is_Mandatory_to_pass,Class_id,Subject_Sub_Level_Id,Subject_Activity_Name,Is_Distinction,Distinction_Marks,Start_Date_Marks,End_Date_Marks,Start_IDate_Marks,End_IDate_Marks,Is_save_marks) values (@Session_Id,@Branch_Id,@Istatus,@Exam_Term_Id,@Assessment_Id,@Subject_id,@Assessment_Subject_Id,@Short_Name,@Sequence_No,@Grade_System_Id,@Maximum_Marks,@Cut_Off_Percentage,@Calculation_Type,@Is_Advanced_Advanced_Setting,@Consider_best,@Pass_criteria,@Created_By,@Created_Date_Time,@Is_Mandatory_to_pass,@Class_id,@Subject_Sub_Level_Id,@Subject_Activity_Name,@Is_Distinction,@Distinction_Marks,@Start_Date_Marks,@End_Date_Marks,@Start_IDate_Marks,@End_IDate_Marks,@Is_save_marks)";
                    cmd = new SqlCommand(query2);
                    cmd.Parameters.AddWithValue("@Is_save_marks", 0);

                    cmd.Parameters.AddWithValue("@Class_id", ddl_class.SelectedValue);
                    cmd.Parameters.AddWithValue("@Session_Id", ViewState["sessionid"].ToString());
                    cmd.Parameters.AddWithValue("@Branch_Id", ViewState["branchid"].ToString());
                    if (chk_isactive.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@Istatus", 1);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Istatus", 0);
                    }

                    cmd.Parameters.AddWithValue("@Exam_Term_Id", ddl_examterm.SelectedValue);
                    cmd.Parameters.AddWithValue("@Assessment_Id", ddl_assessment.SelectedValue);
                    cmd.Parameters.AddWithValue("@Subject_id", ddl_subject.SelectedValue);
                    cmd.Parameters.AddWithValue("@Assessment_Subject_Id", ViewState["Assessment_Subject_Id"].ToString());
                    cmd.Parameters.AddWithValue("@Short_Name", txt_Short_Name.Text);
                    cmd.Parameters.AddWithValue("@Sequence_No", txt_Sequence_No.Text);
                    cmd.Parameters.AddWithValue("@Grade_System_Id", ddl_gradesystem.SelectedValue);
                    cmd.Parameters.AddWithValue("@Maximum_Marks", txt_maximummarks.Text);
                    cmd.Parameters.AddWithValue("@Cut_Off_Percentage", txt_cutoff.Text);
                    cmd.Parameters.AddWithValue("@Calculation_Type", ddl_calculation_logic.Text);

                    if (chk_per.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@Is_Advanced_Advanced_Setting", 1);
                        cmd.Parameters.AddWithValue("@Consider_best", ddl_consider_best.SelectedValue);
                        cmd.Parameters.AddWithValue("@Pass_criteria", ddl_pass_criteria.Text);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Is_Advanced_Advanced_Setting", 0);
                        cmd.Parameters.AddWithValue("@Consider_best", 0);
                        cmd.Parameters.AddWithValue("@Pass_criteria", 0);
                    }
                    if (chk_mandatory.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@Is_Mandatory_to_pass", 1);
                    }

                    else
                    {
                        cmd.Parameters.AddWithValue("@Is_Mandatory_to_pass", 0);
                    }




                    cmd.Parameters.AddWithValue("@Created_By", ViewState["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@Created_Date_Time", My.getdate1());




                    cmd.Parameters.AddWithValue("@Subject_Sub_Level_Id", ViewState["Subject_Sub_Level_Id"].ToString());
                    cmd.Parameters.AddWithValue("@Subject_Activity_Name", txt_Name.Text);

                    if (chk_add_distinction.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@Distinction_Marks", txt_distinctionmarks.Text);
                        cmd.Parameters.AddWithValue("@Is_Distinction", 1);
                    }

                    else
                    {
                        cmd.Parameters.AddWithValue("@Distinction_Marks", txt_distinctionmarks.Text);
                        cmd.Parameters.AddWithValue("@Is_Distinction", 0);
                    }



                    int idate = mycode.ConvertStringToiDateup(txt_start_date.Text);
                    int idate2 = mycode.ConvertStringToiDateup(txt_enddate.Text);

                    cmd.Parameters.AddWithValue("@Start_Date_Marks", txt_start_date.Text);
                    cmd.Parameters.AddWithValue("@End_Date_Marks", txt_enddate.Text);

                    cmd.Parameters.AddWithValue("@Start_IDate_Marks", idate);
                    cmd.Parameters.AddWithValue("@End_IDate_Marks", idate2);


                    if (My.InsertUpdateData(cmd))
                    {
                        empty_data();
                        update_Exam_Assessment_Details();
                        process_active("1");
                        Alertme("Subject activity name has been saved Successfully.", "success");

                    }




                }

                else
                {
                    string id = dt2.Rows[0]["Id"].ToString();

                    query2 = "Update Exam_Subject_Sub_Level set Session_Id=@Session_Id,Branch_Id=@Branch_Id,Istatus=@Istatus,Exam_Term_Id=@Exam_Term_Id,Assessment_Id=@Assessment_Id,Subject_id=@Subject_id,Assessment_Subject_Id=@Assessment_Subject_Id,Short_Name=@Short_Name,Sequence_No=@Sequence_No,Grade_System_Id=@Grade_System_Id,Maximum_Marks=@Maximum_Marks,Cut_Off_Percentage=@Cut_Off_Percentage,Calculation_Type=@Calculation_Type,Is_Advanced_Advanced_Setting=@Is_Advanced_Advanced_Setting,Consider_best=@Consider_best,Pass_criteria=@Pass_criteria,Updated_By=@Updated_By,Updated_Date_Time=@Updated_Date_Time,Is_Mandatory_to_pass=@Is_Mandatory_to_pass,Class_id=@Class_id,Subject_Activity_Name=@Subject_Activity_Name,Is_Distinction=@Is_Distinction,Distinction_Marks=@Distinction_Marks,Start_Date_Marks= @Start_Date_Marks,End_Date_Marks=@End_Date_Marks,Start_IDate_Marks= @Start_IDate_Marks,End_IDate_Marks= @End_IDate_Marks where Id = @Id";






                    cmd = new SqlCommand(query2);
                    cmd.Parameters.AddWithValue("@Id", id);

                    cmd.Parameters.AddWithValue("@Class_id", ddl_class.SelectedValue);
                    cmd.Parameters.AddWithValue("@Session_Id", ViewState["sessionid"].ToString());
                    cmd.Parameters.AddWithValue("@Branch_Id", ViewState["branchid"].ToString());
                    if (chk_isactive.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@Istatus", 1);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Istatus", 0);
                    }

                    cmd.Parameters.AddWithValue("@Exam_Term_Id", ddl_examterm.SelectedValue);
                    cmd.Parameters.AddWithValue("@Assessment_Id", ddl_assessment.SelectedValue);
                    cmd.Parameters.AddWithValue("@Subject_id", ddl_subject.SelectedValue);
                    cmd.Parameters.AddWithValue("@Assessment_Subject_Id", "");
                    cmd.Parameters.AddWithValue("@Short_Name", txt_Short_Name.Text);
                    cmd.Parameters.AddWithValue("@Sequence_No", txt_Sequence_No.Text);
                    cmd.Parameters.AddWithValue("@Grade_System_Id", ddl_gradesystem.SelectedValue);
                    cmd.Parameters.AddWithValue("@Maximum_Marks", txt_maximummarks.Text);
                    cmd.Parameters.AddWithValue("@Cut_Off_Percentage", txt_cutoff.Text);
                    cmd.Parameters.AddWithValue("@Calculation_Type", ddl_calculation_logic.Text);

                    if (chk_per.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@Is_Advanced_Advanced_Setting", 1);
                        cmd.Parameters.AddWithValue("@Consider_best", ddl_consider_best.SelectedValue);
                        cmd.Parameters.AddWithValue("@Pass_criteria", ddl_pass_criteria.Text);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Is_Advanced_Advanced_Setting", 0);
                        cmd.Parameters.AddWithValue("@Consider_best", 0);
                        cmd.Parameters.AddWithValue("@Pass_criteria", 0);
                    }
                    if (chk_mandatory.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@Is_Mandatory_to_pass", 1);
                    }

                    else
                    {
                        cmd.Parameters.AddWithValue("@Is_Mandatory_to_pass", 0);
                    }


                    cmd.Parameters.AddWithValue("@Subject_Activity_Name", txt_Name.Text);

                    if (chk_add_distinction.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@Distinction_Marks", txt_distinctionmarks.Text);
                        cmd.Parameters.AddWithValue("@Is_Distinction", 1);
                    }

                    else
                    {
                        cmd.Parameters.AddWithValue("@Distinction_Marks", txt_distinctionmarks.Text);
                        cmd.Parameters.AddWithValue("@Is_Distinction", 0);
                    }

                    cmd.Parameters.AddWithValue("@Updated_By", ViewState["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@Updated_Date_Time", My.getdate1());

                    int idate = mycode.ConvertStringToiDateup(txt_start_date.Text);
                    int idate2 = mycode.ConvertStringToiDateup(txt_enddate.Text);

                    cmd.Parameters.AddWithValue("@Start_Date_Marks", txt_start_date.Text);
                    cmd.Parameters.AddWithValue("@End_Date_Marks", txt_enddate.Text);

                    cmd.Parameters.AddWithValue("@Start_IDate_Marks", idate);
                    cmd.Parameters.AddWithValue("@End_IDate_Marks", idate2);

                    if (My.InsertUpdateData(cmd))
                    {
                        Bind_Data_edit();
                        update_Exam_Assessment_Details();
                        process_active("4");
                        Alertme("Subject activity name has been saved Successfully.", "success");

                    }
                }

            }
            catch
            {
            }

        }

        private void empty_data()
        {
            examname.InnerText = "";
            ViewState["Subject_Sub_Level_Id"] = Examination.auto_serialS("Subject_Sub_Level_Id", ViewState["branchid"].ToString());
            txt_Name.Text = "";
            txt_Short_Name.Text = "";
            txt_Sequence_No.Text = "";
            txt_maximummarks.Text = "";
            txt_cutoff.Text = "";
            txt_distinctionmarks.Text = "";
            chk_per.Checked = false;
            chk_mandatory.Checked = true;
            chk_isactive.Checked = true;
            chk_add_distinction.Checked = false;


        }

        private void update_Exam_Assessment_Details()
        {
            try
            {
                for (var i = 0; i < Convert.ToInt32(ViewState["countgrid"].ToString()); i++)
                {

                    CheckBox chk_per = (CheckBox)grid_subject_assesment.Rows[i].FindControl("chk_per");

                    Label lbl_Id = (Label)grid_subject_assesment.Rows[i].FindControl("lbl_Id");
                    if (chk_per.Checked == true)
                    {
                        mycode.executequery("update Exam_Subject_Sub_Level set Select_Data=" + 1 + " where id=" + lbl_Id.Text + "");
                    }
                }
            }
            catch
            {
            }
        }

        protected void chk_add_distinction_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_add_distinction.Checked == true)
            {
                distinction.Visible = true;
            }
            else
            {
                distinction.Visible = false;
            }
        }
    }
}