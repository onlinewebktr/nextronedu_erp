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
    public partial class Set_Exam_Teram : System.Web.UI.Page
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
                    ViewState["count"] = "0";
                    ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                    ViewState["courseID"] = "0";
                    mycode.bind_all_ddl_with_id(ddl_class, "Select Course_Name,course_id from Add_course_table order by Position asc");
                   // mycode.bind_all_checkboxlist(ddl_list_class, "Select Course_Name,course_id from Add_course_table order by Position asc");
                    Bind_class_name();
                    mycode.bind_all_ddl_with_id(ddl_gradesystem, "Select distinct egs.Grade_Name,egs.Grade_System_Id from Exam_Grade_System  egs join Exam_Grade_System_Mapping_with_Class egsmc on egs.Grade_System_Id = egsmc.Grade_System_Id  where egs.Session_Id='" + ViewState["sessionid"].ToString() + "'  order by egs.Grade_Name asc");


                    if (Request.QueryString["Exam_Term_Id"] != null)
                    {
                        ViewState["Exam_Term_Id"] = Request.QueryString["Exam_Term_Id"].ToString();
                        ViewState["update"] = "1";
                        process_active("1");
                        Bind_Data_edit();
                        rd_view.Visible = false;
                        ddl_class.Visible = true;
                        chk_all.Visible = false;

                    }
                    else
                    {
                        ViewState["update"] = "0";
                        ViewState["Exam_Term_Id"] = Examination.auto_serialS("Exam_Term_Id", ViewState["branchid"].ToString());
                        rd_view.Visible = true;
                        ddl_class.Visible = false;
                        chk_all.Visible = true;
                        process_active("1");
                        Bind_Data_edit();
                    }



                }
            }
        }

        private void Bind_class_name()
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

        private void Bind_Data_edit()
        {
            string query = "Select *,(Select top 1 Session from session_details where session_id=Exam_Term_Details.Session_Id) as sessionname from Exam_Term_Details where Exam_Term_Id=" + ViewState["Exam_Term_Id"].ToString() + " and Branch_Id=" + ViewState["branchid"].ToString() + " and Session_Id=" + ViewState["sessionid"].ToString() + " ";

            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                year.InnerText = My.get_session();
                examname.InnerText = "";

            }
            else
            {
                ddl_class.SelectedValue = dt.Rows[0]["Class_id"].ToString().ToUpper();


                txt_no_of_class.Text = dt.Rows[0]["No_of_Class"].ToString().ToUpper();



                examname.InnerText = dt.Rows[0]["Term_Name"].ToString().ToUpper();
                year.InnerText = dt.Rows[0]["sessionname"].ToString();

                txt_Name.Text = dt.Rows[0]["Term_Name"].ToString();

                txt_Short_Name.Text = dt.Rows[0]["Short_Name"].ToString();

                txt_Sequence_No.Text = dt.Rows[0]["Sequence_No"].ToString();
                mycode.bind_all_ddl_with_id_no_select(ddl_gradesystem, "Select distinct egs.Grade_Name,egs.Grade_System_Id from Exam_Grade_System  egs join Exam_Grade_System_Mapping_with_Class egsmc on egs.Grade_System_Id = egsmc.Grade_System_Id where egsmc.Course_id='" + ddl_class.SelectedValue + "' and  egs.Session_Id='" + ViewState["sessionid"].ToString() + "'   order by egs.Grade_Name asc");

                ddl_gradesystem.SelectedValue = dt.Rows[0]["Grade_System_Id"].ToString();


                txt_maximummarks.Text = dt.Rows[0]["Maximum_Marks"].ToString();
                txt_cutoff.Text = dt.Rows[0]["Cut_Off_Percentage"].ToString();


                ddl_calculation_logic.Text = dt.Rows[0]["Calculation_Type"].ToString();


                if (dt.Rows[0]["Istatus"].ToString() == "True")
                {
                    chk_isactive.Checked = true;
                }
                else
                {
                    chk_isactive.Checked = false;
                }
                if (dt.Rows[0]["Is_Mandatory_to_pass"].ToString() == "True")
                {
                    chk_mandatory.Checked = true;

                }
                else
                {
                    chk_mandatory.Checked = false;

                }

                string query2 = "Select * from Exam_Assessment_Details where Exam_Term_Id=" + ViewState["Exam_Term_Id"].ToString() + " and Branch_Id=" + ViewState["branchid"].ToString() + " and Session_Id=" + ViewState["sessionid"].ToString() + " and Scholastic_Co_scholastic='Scholastic' order by Sequence_No asc";

                DataTable dt2 = mycode.FillData(query2);
                if (dt2.Rows.Count == 0)
                {
                    ViewState["countgrid"] = "0";
                    sublevel.Visible = false;
                    grid_assessment.DataSource = null;
                    grid_assessment.DataBind();
                }
                else
                {
                    ViewState["countgrid"] = dt2.Rows.Count.ToString();
                    sublevel.Visible = true;
                    grid_assessment.DataSource = dt2;
                    grid_assessment.DataBind();
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
                    CheckBox chk_per = (CheckBox)grid_assessment.Rows[i].FindControl("chk_per");


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
                    CheckBox chk_per = (CheckBox)grid_assessment.Rows[i].FindControl("chk_per");


                    if (chk_per.Checked == true)
                    {
                        string item = l.ToString() + " OF " + (k);
                        ddlbind2.Add(item.ToString());

                        ddl_pass_criteria.DataSource = ddlbind2;
                        ddl_pass_criteria.DataBind();
                        l++;
                    }

                }

                try
                {
                    ddl_consider_best.Text = dt.Rows[0]["Is_Mandatory_to_pass"].ToString();

                    ddl_pass_criteria.Text = dt.Rows[0]["Pass_criteria"].ToString();
                }
                catch
                {
                }



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




            }
        }
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
        protected void btn_back_3_2_Click(object sender, EventArgs e)
        {
            process_active("2");
        }



        protected void btn_back_2_1_Click(object sender, EventArgs e)
        {
            process_active("1");
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
                Alertme("Please enter Pass Marks", "warning");
            }
            else
            {

                if (My.toDouble(txt_maximummarks.Text) > My.toDouble(txt_cutoff.Text))
                {

                    process_active("3");
                }
                else
                {
                    Alertme("Please enter pass marks always less then maximum marks", "warning");
                }
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Exam_Teram.aspx", false);
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
        protected void btn_next_1_2_Click(object sender, EventArgs e)
        {

            if (ViewState["update"].ToString() == "1")
            {


                SqlCommand cmd;
                if (ddl_class.SelectedItem.Text == "Select")
                {
                    Alertme("Please select class", "warning");
                }
                else
                {
                    DataTable dt1 = mycode.FillData("Select * from Exam_Term_Details where  Branch_Id=" + ViewState["branchid"].ToString() + " and Session_Id=" + ViewState["sessionid"].ToString() + "  and  Term_Name='" + txt_Name.Text + "' and Class_id='" + ddl_class.SelectedValue + "'");
                    if (dt1.Rows.Count == 0)
                    {
                        examname.InnerText = txt_Name.Text;
                        process_active("2");
                    }

                    else
                    {
                        DataTable dt2 = mycode.FillData("Select * from Exam_Term_Details where  Branch_Id=" + ViewState["branchid"].ToString() + " and Session_Id=" + ViewState["sessionid"].ToString() + "  and  Term_Name='" + txt_Name.Text + "' and Exam_Term_Id!=" + ViewState["Exam_Term_Id"].ToString() + " and Class_id='" + ddl_class.SelectedValue + "'");
                        if (dt2.Rows.Count == 0)
                        {
                            examname.InnerText = txt_Name.Text;
                            process_active("2");

                            string id = dt1.Rows[0]["Id"].ToString();



                            string query = "Update Exam_Term_Details set  Istatus=@Istatus,Term_Name=@Term_Name,Short_Name=@Short_Name,Sequence_No=@Sequence_No, Updated_By=@Updated_By,Updated_Date_Time=@Updated_Date_Time,Is_Mandatory_to_pass=@Is_Mandatory_to_pass,Class_id=@Class_id where Id = @Id";
                            cmd = new SqlCommand(query);

                            if (chk_isactive.Checked == true)
                            {
                                cmd.Parameters.AddWithValue("@Istatus", 1);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Istatus", 0);
                            }
                            cmd.Parameters.AddWithValue("@Term_Name", txt_Name.Text);
                            cmd.Parameters.AddWithValue("@Short_Name", txt_Short_Name.Text);
                            cmd.Parameters.AddWithValue("@Sequence_No", txt_Sequence_No.Text);
                            cmd.Parameters.AddWithValue("@Class_id", ddl_class.SelectedValue);
                            cmd.Parameters.AddWithValue("@Updated_By", ViewState["Userid"].ToString());
                            cmd.Parameters.AddWithValue("@Updated_Date_Time", My.getdate1());
                            cmd.Parameters.AddWithValue("@Id", id);
                            if (chk_mandatory.Checked == true)
                            {
                                cmd.Parameters.AddWithValue("@Is_Mandatory_to_pass", 1);
                            }

                            else
                            {
                                cmd.Parameters.AddWithValue("@Is_Mandatory_to_pass", 0);
                            }
                            if (My.InsertUpdateData(cmd))
                            {
                            }

                        }
                        else
                        {
                            examname.InnerText = txt_Name.Text;
                            process_active("2");

                          



                            string query = "Update Exam_Term_Details set  Istatus=@Istatus,Term_Name=@Term_Name,Short_Name=@Short_Name,Sequence_No=@Sequence_No, Updated_By=@Updated_By,Updated_Date_Time=@Updated_Date_Time,Is_Mandatory_to_pass=@Is_Mandatory_to_pass where Exam_Term_Id = @Exam_Term_Id";
                            cmd = new SqlCommand(query);

                            if (chk_isactive.Checked == true)
                            {
                                cmd.Parameters.AddWithValue("@Istatus", 1);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Istatus", 0);
                            }
                            cmd.Parameters.AddWithValue("@Term_Name", txt_Name.Text);
                            cmd.Parameters.AddWithValue("@Short_Name", txt_Short_Name.Text);
                            cmd.Parameters.AddWithValue("@Sequence_No", txt_Sequence_No.Text);
                          
                            cmd.Parameters.AddWithValue("@Updated_By", ViewState["Userid"].ToString());
                            cmd.Parameters.AddWithValue("@Updated_Date_Time", My.getdate1());
                            cmd.Parameters.AddWithValue("@Exam_Term_Id", ViewState["Exam_Term_Id"].ToString());
                            if (chk_mandatory.Checked == true)
                            {
                                cmd.Parameters.AddWithValue("@Is_Mandatory_to_pass", 1);
                            }

                            else
                            {
                                cmd.Parameters.AddWithValue("@Is_Mandatory_to_pass", 0);
                            }
                            if (My.InsertUpdateData(cmd))
                            {
                            }
                        }


                    }
                }
            }
            else
            {
                examname.InnerText = txt_Name.Text;
                process_active("2");

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

        protected void btn_final_submit_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["deletemode"] = "0";

                if (ViewState["update"].ToString() == "1")
                {
                    // update code
                    new_insert_data(ddl_class.SelectedValue, ViewState["Exam_Term_Id"].ToString());
                    process_active("4");
                    Alertme("Exam Term has been saved Successfully.", "success");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Exam Term has been saved Successfully.')", true);
                    Bind_Data_edit();

                }
                else// insert data
                {
                    //foreach (ListItem item in ddl_list_class.Items)


                    int growcount = rd_view.Items.Count;
                    int k = 0;
                    for (int ix = 0; ix < growcount; ix++)
                    {
                        CheckBox chk = (CheckBox)rd_view.Items[ix].FindControl("chk_class");
                        if (chk.Checked == true)
                        {
                            Label lbl_class_id = (Label)rd_view.Items[ix].FindControl("lbl_class_id");
                            string Exam_Term_Id = Examination.auto_serialS("Exam_Term_Id", ViewState["branchid"].ToString());


                            new_insert_data(lbl_class_id.Text, Exam_Term_Id);

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
                        ViewState["Exam_Term_Id"] = Examination.auto_serialS("Exam_Term_Id", ViewState["branchid"].ToString());
                        Alertme("Exam Term has been saved Successfully.", "success");
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Exam Term has been saved Successfully.')", true);

                        process_active("4");
                        empty_data();
                    }

                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Set exam Term");
            }

        }

        private void new_insert_data(string classid, string Exam_Term_Id)
        {
            SqlCommand cmd;
            DataTable dt1 = mycode.FillData("Select * from Exam_Term_Details where  Branch_Id=" + ViewState["branchid"].ToString() + " and Session_Id=" + ViewState["sessionid"].ToString() + "  and Exam_Term_Id=" + Exam_Term_Id + "");
            if (dt1.Rows.Count == 0)
            {
                string query = "INSERT INTO Exam_Term_Details (Session_Id,Branch_Id,Istatus,Term_Name,Short_Name,Sequence_No,Grade_System_Id,Maximum_Marks,Cut_Off_Percentage,Calculation_Type,Is_Advanced_Advanced_Setting,Consider_best,Pass_criteria,Created_By,Created_Date_Time,Exam_Term_Id,Is_Mandatory_to_pass,No_of_Class,Class_id) values (@Session_Id,@Branch_Id,@Istatus,@Term_Name,@Short_Name,@Sequence_No,@Grade_System_Id,@Maximum_Marks,@Cut_Off_Percentage,@Calculation_Type,@Is_Advanced_Advanced_Setting,@Consider_best,@Pass_criteria,@Created_By,@Created_Date_Time,@Exam_Term_Id,@Is_Mandatory_to_pass,@No_of_Class,@Class_id)";

                cmd = new SqlCommand(query);
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
                cmd.Parameters.AddWithValue("@Term_Name", txt_Name.Text);
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
                cmd.Parameters.AddWithValue("@Created_By", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Created_Date_Time", My.getdate1());
                cmd.Parameters.AddWithValue("@Exam_Term_Id", Exam_Term_Id);

                if (chk_mandatory.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@Is_Mandatory_to_pass", 1);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Is_Mandatory_to_pass", 0);
                }
                cmd.Parameters.AddWithValue("@No_of_Class", txt_no_of_class.Text);
                cmd.Parameters.AddWithValue("@Class_id", classid);

                if (My.InsertUpdateData(cmd))
                {

                }



            }
            else
            {
                string id = dt1.Rows[0]["Id"].ToString();
                string query = "Update Exam_Term_Details set  Istatus=@Istatus,Term_Name=@Term_Name,Short_Name=@Short_Name,Sequence_No=@Sequence_No,Grade_System_Id=@Grade_System_Id,Maximum_Marks=@Maximum_Marks,Cut_Off_Percentage=@Cut_Off_Percentage,Calculation_Type=@Calculation_Type,Is_Advanced_Advanced_Setting=@Is_Advanced_Advanced_Setting,Consider_best=@Consider_best,Pass_criteria=@Pass_criteria,Updated_By=@Updated_By,Updated_Date_Time=@Updated_Date_Time,Is_Mandatory_to_pass=@Is_Mandatory_to_pass,Class_id=@Class_id,No_of_Class=@No_of_Class where Id = @Id";

                cmd = new SqlCommand(query);
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
                cmd.Parameters.AddWithValue("@Term_Name", txt_Name.Text);
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
                cmd.Parameters.AddWithValue("@Updated_By", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Updated_Date_Time", My.getdate1());
                cmd.Parameters.AddWithValue("@Id", id);

                if (chk_mandatory.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@Is_Mandatory_to_pass", 1);
                }

                else
                {
                    cmd.Parameters.AddWithValue("@Is_Mandatory_to_pass", 0);
                }
                cmd.Parameters.AddWithValue("@Class_id", classid);
                cmd.Parameters.AddWithValue("@No_of_Class", txt_no_of_class.Text);
                if (My.InsertUpdateData(cmd))
                {
                    //   update_Exam_Assessment_Details();


                }
            }
        }

        private void update_Exam_Assessment_Details()
        {

            try
            {
                for (var i = 0; i < Convert.ToInt32(ViewState["countgrid"].ToString()); i++)
                {

                    CheckBox chk_per = (CheckBox)grid_assessment.Rows[i].FindControl("chk_per");

                    Label lbl_Id = (Label)grid_assessment.Rows[i].FindControl("lbl_Id");
                    if (chk_per.Checked == true)
                    {
                        mycode.executequery("update Exam_Assessment_Details set Select_Data=" + 1 + " where id=" + lbl_Id.Text + "");
                    }
                }
            }
            catch
            {
            }


        }

        private void empty_data()
        {
            process_active("1");

            ViewState["countgrid"] = "0";
            sublevel.Visible = false;
            chk_per.Checked = false;
            grid_assessment.DataSource = null;
            grid_assessment.DataBind();


            txt_Name.Text = "";
            txt_Short_Name.Text = "";
            txt_Sequence_No.Text = "";
            txt_maximummarks.Text = "";
            txt_cutoff.Text = "";
            chk_per.Checked = false;
            chk_mandatory.Checked = false;
            chk_isactive.Checked = false;

        }

        protected void grid_assessment_RowDataBound(object sender, GridViewRowEventArgs e)
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
                CheckBox chk_per = (CheckBox)grid_assessment.Rows[i].FindControl("chk_per");


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
                CheckBox chk_per = (CheckBox)grid_assessment.Rows[i].FindControl("chk_per");


                if (chk_per.Checked == true)
                {
                    string item = l.ToString() + " OF " + (k);
                    ddlbind2.Add(item.ToString());

                    ddl_pass_criteria.DataSource = ddlbind2;
                    ddl_pass_criteria.DataBind();
                    l++;
                }

            }






            try
            {
                if (ddl_consider_best.Text == "1")
                {
                }
                else
                {
                    //  chk_per.Checked = false;
                    //  advancedSetting.Visible = false;
                }
            }
            catch
            {
                // chk_per.Checked = false;
                //  advancedSetting.Visible = false;
            }




        }

        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_class.SelectedItem.Text == "Select")
            {
                Alertme("Please select class", "warning");
            }
            else
            {
                mycode.bind_all_ddl_with_id_no_select(ddl_gradesystem, "Select distinct egs.Grade_Name,egs.Grade_System_Id from Exam_Grade_System  egs join Exam_Grade_System_Mapping_with_Class egsmc on egs.Grade_System_Id = egsmc.Grade_System_Id where egsmc.Course_id='" + ddl_class.SelectedValue + "' and egs.Session_Id='"+ ViewState["sessionid"].ToString() + "'  order by egs.Grade_Name asc");
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

    }
}