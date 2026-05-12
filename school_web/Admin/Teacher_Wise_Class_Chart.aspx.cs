using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using school_web.AppCode;
namespace school_web.Admin
{
    public partial class Teacher_Wise_Class_Chart : System.Web.UI.Page
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
                        ViewState["firm_id"] = Session["firm"].ToString();
                        ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        ViewState["Sessionid"] = My.get_session_id();
                        routineUpdate.update_teacher_routine(ViewState["Sessionid"].ToString(), ViewState["branchid"].ToString(), ViewState["Userid"].ToString());
                        
                        

                        mycode.bind_all_ddl_with_id(ddlclass, "Select Course_Name,course_id from Add_course_table order by Position");
                        ddlclass.SelectedValue = My.get_top_one_class();
                        mycode.bind_ddl(ddl_section, "Select distinct Section  from Class_Routine_Master  order by Section");
                        ddl_section.Text = My.get_top_one_section();

                        lbl_useridandname.Text = My.get_empname(ViewState["Userid"].ToString()) + "-(" + ViewState["Userid"].ToString() + ")";
                        find_firm_details();

                        ViewState["periodcount"] = My.get_period(My.get_session_id());


                        mycode.bind_all_ddl_with_id(ddl_teacher, " Select distinct ud.name,ud.user_id from user_details ud join Class_Routine_Master_Teacher crmt on crmt.Teacher_id=ud.user_id where crmt.Class_id='" + ddlclass.SelectedValue + "' and crmt.Section='" + ddl_section.Text + "' and crmt.Session_id=" + ViewState["Sessionid"].ToString() + " order by ud.name");

                        ddl_teacher.SelectedValue = My.get_top_one_class_teacher(ddlclass.SelectedValue, ddl_section.Text, ViewState["Sessionid"].ToString());

                        find_data();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_Strength_Master");
            }
        }


         

        private void map_teachers()
        {
            mycode.executequery("delete from Class_Routine_Master_Teacher where Session_id='1'");

            DataTable dt = My.dataTable("select t1.UserID as teacher_id,t1.AssignCourseID as Subject_id,t1.CategoryID as Class_id,t1.section,t2.Class_period,t2.Day from TeacherCourseSubjectMaping t1 join Class_routine_period_subject_mapping t2 on t1.Session_id=t2.Session_id and t1.CategoryID=t2.Class_id and t1.AssignCourseID=t2.Subject_id and t1.section=t2.Section where t1.Session_id=" + ViewState["Sessionid"].ToString() + " and t1.Branch_id='" + ViewState["branchid"].ToString() + "' and t1.Istatus=1");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    // if(mycode.IsUserExist(""))
                    SqlCommand cmd;
                    string queryt = "INSERT INTO Class_Routine_Master_Teacher (Teacher_id,Session_id,Class_id,Section,Subject_id,Class_period,Day,Created_By,Date,Idate,time) values (@Teacher_id,@Session_id,@Class_id,@Section,@Subject_id,@Class_period,@Day,@Created_By,@Date,@Idate,@time )";
                    cmd = new SqlCommand(queryt);
                    cmd.Parameters.AddWithValue("@Teacher_id", dr["teacher_id"].ToString());
                    cmd.Parameters.AddWithValue("@Session_id", ViewState["Sessionid"].ToString());
                    cmd.Parameters.AddWithValue("@Class_id", dr["Class_id"].ToString());
                    cmd.Parameters.AddWithValue("@Section", dr["section"].ToString());
                    cmd.Parameters.AddWithValue("@Subject_id", dr["Subject_id"].ToString());
                    cmd.Parameters.AddWithValue("@Class_period", dr["Class_period"].ToString());
                    cmd.Parameters.AddWithValue("@Day", dr["Day"].ToString());
                    cmd.Parameters.AddWithValue("@Created_By", ViewState["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@Date", mycode.date());
                    cmd.Parameters.AddWithValue("@Idate", mycode.idate());
                    cmd.Parameters.AddWithValue("@time", mycode.time());
                    if (InsertUpdate.InsertUpdateData(cmd))
                    {
                    }
                }
            }
        }

        private void find_firm_details()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count == 0)
            {




            }
            else
            {
                imglogo.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_emaiid.Text = dt.Rows[0]["email"].ToString();
                lbl_website.Text = dt.Rows[0]["website"].ToString();
                lbl_contact_details.Text = dt.Rows[0]["contact_no"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();
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


        protected void btn_excels_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Class_Routine" + "_" + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                grd_classchart.RenderControl(hw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }


        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected void ddlclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlclass.SelectedItem.Text == "Select")
            {

                Alertme("Please select class", "warning");
            }
            else
            {
                mycode.bind_ddl(ddl_section, "Select distinct Section  from Class_Routine_Master where Class_id='" + ddlclass.SelectedValue + "' order by Section");
            }
        }
        protected void ddl_section_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlclass.SelectedItem.Text == "Select")
            {

                Alertme("Please select class", "warning");
            }
            else if (ddl_section.Text == "Select")
            {
                Alertme("Please select section", "warning");
            }
            else
            {
                mycode.bind_all_ddl_with_id(ddl_teacher, " Select distinct ud.name,ud.user_id from user_details ud join Class_Routine_Master_Teacher crmt on crmt.Teacher_id=ud.user_id where crmt.Class_id='" + ddlclass.SelectedValue + "' and crmt.Section='" + ddl_section.Text + "' and crmt.Session_id=" + ViewState["Sessionid"].ToString() + " order by ud.name");
            }
        }
        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {

                find_data();

            }
            catch
            {
            }
        }

        private void find_data()
        {
            if (ddlclass.SelectedItem.Text == "Select")
            {

                Alertme("Please select class", "warning");
            }
            else if (ddl_section.SelectedItem.Text == "Select")
            {
                Alertme("Please select section", "warning");
            }
            else if (ddl_teacher.SelectedItem.Text == "Select")
            {
                Alertme("Please select teacher", "warning");
            }
            else
            {
                // string query = mycode.get_class_routine();
                DataTable dt = mycode.get_class_routine_teacher_wise(ViewState["Sessionid"].ToString(), ddlclass.SelectedValue, ddl_section.Text, ddl_teacher.SelectedValue);

                if (dt.Rows.Count == 0)
                {
                    btn_excels.Visible = false;
                    print1.Visible = false;
                    tblPrintIQ.Visible = false;
                    grd_classchart.DataSource = null;
                    grd_classchart.DataBind();
                    Alertme("Sorry! No  ny routine chart found", "warning");
                }
                else
                {
                    lbl_date.Text = "Teacher : " + ddl_teacher.SelectedItem.Text + " " + ddlclass.SelectedItem.Text + " Section: " + ddl_section.SelectedItem.Text;
                    btn_excels.Visible = true;
                    print1.Visible = true;
                    tblPrintIQ.Visible = true;
                    grd_classchart.DataSource = dt;
                    grd_classchart.DataBind();
                }
            }
        }

        protected void grd_classchart_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            string get_dayname = System.DateTime.Now.ToString("dddd");
            if (e.Row.Cells[0].Text == get_dayname)
            {




                //for (int i = 0; i <= Convert.ToInt32(ViewState["periodcount"].ToString()); i++)
                //{
                //    e.Row.Cells[i].BackColor = Color.Red;
                //}  







            }
        }


    }
}