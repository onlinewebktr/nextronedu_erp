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
    public partial class Scholarship_Publish_Result : System.Web.UI.Page
    {
        My mycode = new My();

        string centername = "Select top 1 Centre_Name from Scholarship_Exam_Centre where Exam_Centre_Id=ore.Exam_Centre_Id and Test_id=ore.Test_id ";
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
                        ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        string pagename_current = "Scholarship_Publish_Result.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        ViewState["session_id"] = My.get_session_id_onlinereg();
                        mycode.bind_all_ddl_with_id(ddl_Scholarship_name, "select Test_name,Test_id from Scholarship_Program   order by  Test_name asc");
                        ddl_Scholarship_name.SelectedValue = My.get_top_one_Scholarship_name(My.get_session_id_onlinereg());

                        mycode.bind_all_ddl_with_id(ddl_class, "Select distinct ac.Course_Name,ac.course_id,ac.Position from Add_course_table  ac join Scholarship_Admission ad  on ac.course_id=ad.Class_id where Test_id=" + ddl_Scholarship_name.SelectedValue + " order by ac.Position asc");

                        find_firm_details();


                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Scholarship_Publish_Result");
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

        protected void ddl_Scholarship_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Scholarship_name.SelectedItem.Text == "Select")
            {
                Alertme("Please select scholorship name", "warning");
            }
            else
            {
                mycode.bind_all_ddl_with_id_All_New(ddl_class, "Select distinct ac.Course_Name,ac.course_id,ac.Position from Add_course_table  ac join Scholarship_Admission ad  on ac.course_id=ad.Class_id where Test_id=" + ddl_Scholarship_name.SelectedValue + " order by ac.Position asc");

            }
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            if (ddl_Scholarship_name.SelectedItem.Text == "")
            {
                Alertme("Please select scholorship name", "warning");
            }
            else if (ddl_class.SelectedItem.Text == "")
            {
                Alertme("Please select scholorship for", "warning");
            }
            else
            {
                find_data();
            }
        }

        private void find_data()
        {
            string query = "Select oa.*,'0'  as admissionnumber,ore.Roll_no,format(ore.Exam_Date_time, 'dd/MM/yyyy') as Created_datetime1,ore.Exam_Time,(select top 1 Session from session_details where session_id=oa.Session_id) as Session_name,(select top 1 Course_Name from Add_course_table where course_id=oa.Class_id) as Course_Name,ore.Exam_Type,format(Exam_Date_time, 'hh:mm') as exm_time,format(Exam_Date_time, 'tt') as exm_ampm,format(Reporting_datetime, 'hh:mm') as rp_time,format(Reporting_datetime, 'tt') as rp_ampm,format(Gate_close_datetime, 'hh:mm') as gc_time,format(Gate_close_datetime, 'tt') as gc_ampm,format(Exam_end_date_time, 'hh:mm') as ed_time,format(Exam_end_date_time, 'tt') as ed_ampm,ore.Roll_start_from,ore.Exam_Type,ore.Exam_Date,ore.Remarks,ore.Exam_Shift,ore.Exam_end_time,ore.Reporting_time,Room_no,(" + centername + ") as Centre_Name,ore.Exam_Centre_Id,er.Full_Marks,er.Obtain_Marks,er.Obtain_percentage,er.Obtain_rank,er.Attendance_Status,er.Exam_Result,er.Admission_date as Admission_date1,er.Admission_time as Admission_time1,CASE WHEN er.Is_published = 1 THEN 'Published' WHEN er.Is_published = 0 THEN 'Unpublished'  WHEN er.Is_published = '' THEN 'Unpublished' END AS activestatus,er.Id as resultid from Scholarship_Admission oa join Scholarship_Exam_Time_Table ore on oa.Registration_id=ore.Admission_no and oa.Session_id=ore.Session_Id  join Scholarship_Exam_Result er on er.Admission_no=ore.Admission_no and er.Test_id=ore.Test_id   where   oa.Test_id=" + ddl_Scholarship_name.SelectedValue + "   and ore.Class_id=" + ddl_class.SelectedValue + " order by er.Obtain_rank ";
            ViewState["query"] = query;
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = mycode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                btn_inactive.Visible = false;
                btn_active.Visible = false;
                print1.Visible = false;
                btn_excels.Visible = false;
                Alertme("Sorry there are no scholarship student result exist", "warning");
                GrdView.DataSource = null;
                GrdView.DataBind();
            }
            else
            {
                btn_inactive.Visible = true;
                btn_active.Visible = true;

                btn_excels.Visible = true; 
                GrdView.DataSource = dt;
                GrdView.DataBind();
                if (ViewState["Is_Print"].ToString() == "1")
                {
                    print1.Visible = true;
                }
                else
                {
                    print1.Visible = false;
                }
            }

        }
        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Download"].ToString() == "1")
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=Export.xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    using (StringWriter sw = new StringWriter())
                    {
                        HtmlTextWriter hw = new HtmlTextWriter(sw);
                        GrdView.RenderControl(hw);
                        Response.Output.Write(sw.ToString());
                        Response.Flush();
                        Response.End();
                    }
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            catch
            {
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }


        protected void btn_inactive_Click(object sender, EventArgs e)
        {
            try
            {

                if (ViewState["Is_add"].ToString() == "1")
                {

                    int growcount = GrdView.Rows.Count;
                    int k = 0;
                    for (int i = 0; i < growcount; i++)
                    {
                        CheckBox chk = (CheckBox)GrdView.Rows[i].FindControl("rowChkBox");
                        if (chk.Checked == true)
                        {

                            Label lbl_id = (Label)GrdView.Rows[i].FindControl("lbl_id");
                            mycode.executequery("update Scholarship_Parameter_fees set Isactive='0' where id=" + lbl_id.Text + "");


                        }
                        else
                        {
                            k++;
                        }
                    }
                    if (k == growcount)
                    {
                        Alertme("Please select at least one student from list", "warning");
                    }
                    else
                    {

                        Alertme("Scholarship result has been successfully unpublished", "success");
                        find_data();
                    }
                }
                else if (ViewState["Is_Edit"].ToString() == "1")
                {
                    int growcount = GrdView.Rows.Count;
                    int k = 0;
                    for (int i = 0; i < growcount; i++)
                    {
                        CheckBox chk = (CheckBox)GrdView.Rows[i].FindControl("rowChkBox");
                        if (chk.Checked == true)
                        {

                            Label lbl_id = (Label)GrdView.Rows[i].FindControl("lbl_id");
                            mycode.executequery("update Scholarship_Parameter_fees set Isactive='0' where id=" + lbl_id.Text + "");


                        }
                        else
                        {
                            k++;
                        }
                    }
                    if (k == growcount)
                    {
                        Alertme("Please select at least one student from list", "warning");
                    }
                    else
                    {
                        Alertme("Scholarship result has been successfully unpublished", "success");
                        find_data();
                    }
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            catch
            {
            }
        }

        protected void btn_active_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_add"].ToString() == "1")
                {
                    int growcount = GrdView.Rows.Count;
                    int k = 0;
                    for (int i = 0; i < growcount; i++)
                    {
                        CheckBox chk = (CheckBox)GrdView.Rows[i].FindControl("rowChkBox");
                        if (chk.Checked == true)
                        {

                            Label lbl_id = (Label)GrdView.Rows[i].FindControl("lbl_id");
                            mycode.executequery("update Scholarship_Exam_Result set Is_published=1  where id=" + lbl_id.Text + "");


                        }
                        else
                        {
                            k++;
                        }
                    }
                    if (k == growcount)
                    {
                        Alertme("Please select at least one student from list", "warning");
                    }
                    else
                    {
                        Alertme("Scholarship result has been successfully published", "success");
                        find_data();
                    }
                }
                else if (ViewState["Is_Edit"].ToString() == "1")
                {
                    int growcount = GrdView.Rows.Count;
                    int k = 0;
                    for (int i = 0; i < growcount; i++)
                    {
                        CheckBox chk = (CheckBox)GrdView.Rows[i].FindControl("rowChkBox");
                        if (chk.Checked == true)
                        {

                            Label lbl_id = (Label)GrdView.Rows[i].FindControl("lbl_id");
                            mycode.executequery("update Scholarship_Exam_Result set Is_published=1 where id=" + lbl_id.Text + "");


                        }
                        else
                        {
                            k++;
                        }
                    }
                    if (k == growcount)
                    {
                        Alertme("Please select at least one student from list", "warning");
                    }
                    else
                    {
                        Alertme("Scholarship result has been successfully published", "success");
                        find_data();
                    }
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            catch
            {
            }
        }


    }
}