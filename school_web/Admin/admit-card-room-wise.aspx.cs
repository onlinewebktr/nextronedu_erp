using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class admit_card_room_wise : System.Web.UI.Page
    {
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
                    string pagename_current = "admit-card-room-wise.aspx";
                    Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                    ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                    ViewState["Is_delete"] = (String)dc1["Is_delete"];
                    ViewState["Is_Download"] = (String)dc1["Is_Download"];
                    ViewState["Is_Print"] = (String)dc1["Is_Print"];
                    ViewState["Is_add"] = (String)dc1["Is_add"];
                    find_firm_details();

                    ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());

                    mycode.bind_all_ddl_with_id(ddl_session, "select Session,session_id from session_details order by Session asc");
                    ddl_session.SelectedValue = My.get_session_id_onlinereg();
                    mycode.bind_all_ddl_with_id(ddl_class, "Select Course_Name,course_id from Add_course_table order by Position asc");
                    ddl_class.SelectedValue = My.get_top_one_class();
                    mycode.bind_all_ddl_with_id(ddl_room, "select Room_name,Room_id from Online_admit_card_room order by Room_name asc");
                    My.bind_ddl_select(ddl_shift, "select Shift_name from Online_reg_shift_master where Status=1");
                    mycode.bind_all_ddl_with_id(ddl_test_name, "select Test_name,Test_id from Online_reg_exam_test_master where Session_id='" + ddl_session.SelectedValue + "'   order by  Test_name asc");
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
        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_class.SelectedItem.Text == "Select")
                {
                    ddl_class.Focus();
                    Alertme("Please select class.", "Warning");
                }
                else if (ddl_test_name.SelectedItem.Text == "Select")
                {
                    ddl_test_name.Focus();
                    Alertme("Please select test name.", "Warning");
                }
                else if (ddl_room.SelectedItem.Text == "Select")
                {
                    ddl_room.Focus();
                    Alertme("Please select room.", "Warning");
                }
                else if (ddl_shift.SelectedItem.Text == "Select")
                {
                    ddl_shift.Focus();
                    Alertme("Please select test name.", "Warning");
                }
                else
                {
                    find_student();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_student()
        {
            DataTable sdt = new DataTable();
            string regPre = "";
            DataTable dts = mycode.FillData("select Online_reg_prefix from Firm_Details ");
            if (dts.Rows.Count > 0)
            {
                regPre = dts.Rows[0]["Online_reg_prefix"].ToString();
            }
            DataTable dtc = mycode.FillData("Select oa.*,ore.Shift,'0'  as admissionnumber,format(ore.Exam_Date_time, 'dd/MM/yyyy') as Created_datetime1,ore.Exam_Time,ore.Exam_end_time,ore.Day,(select top 1 Session from session_details where session_id=oa.Session_id) as Session_name,(select top 1 Course_Name from Add_course_table where course_id=oa.Class_id) as Course_Name,CASE WHEN oa.Student_image is null THEN '/images/dummy-student.jpg'  WHEN oa.Student_image is not null THEN oa.Student_image END AS Student_img,(Select top 1 Terms from Online_Admit_Card_Term) as Terms,ore.Exam_Type,(select top 1 Room_name from Online_admit_card_room where Room_id=ore.Room_id) as Room_Name from Online_Admission oa join Online_Reg_Exam_Time_Table ore on oa.Registration_id=ore.Admission_no and oa.Session_id=ore.Session_Id where ore.Session_Id='" + ddl_session.SelectedValue + "' and ore.Class_id='" + ddl_class.SelectedValue + "' and ore.Room_id='" + ddl_room.SelectedValue + "' and ore.Test_id='" + ddl_test_name.SelectedValue + "' and ore.Shift='" + ddl_shift.Text + "'");
            if (dtc.Rows.Count > 0)
            {
                dtc.Columns.Add("New_Registration_id");
                foreach (DataRow dr in dtc.Rows)
                {
                    bool isNum = valid_amount(dr["Registration_id"].ToString());
                    if (isNum == true)
                    {
                        dr["New_Registration_id"] = regPre + dr["Registration_id"].ToString();
                    }
                    else
                    {
                        dr["New_Registration_id"] = dr["Registration_id"].ToString();
                    }
                }


                foreach (DataRow dr in dtc.Rows)
                {
                    try
                    {
                        sdt.Rows.Add(dr.ItemArray);
                    }
                    catch
                    {
                        foreach (DataColumn dc in dtc.Columns)
                        {
                            sdt.Columns.Add(dc.ColumnName);
                        }
                        sdt.Rows.Add(dr.ItemArray);
                    }
                }

                if (sdt.Rows.Count == 0)
                {
                    Alertme("Sorry there are no data list exist", "warning");
                    rd_view.DataSource = null;
                    btn_excels.Visible = false;
                    print1.Visible = false;
                    rd_view.DataBind();
                    lbl_class22.Text = "";
                }
                else
                {
                    lbl_class22.Text = " Session : " + ddl_session.SelectedItem.Text + " Class : " + ddl_class.SelectedItem.Text + " Room :" + ddl_room.SelectedItem.Text;
                    rd_view.DataSource = sdt;
                    rd_view.DataBind();
                    btn_excels.Visible = false;
                    print1.Visible = false;

                    if (ViewState["Is_Print"].ToString() == "1")
                    {
                        btn_excels.Visible = true;
                        print1.Visible = true;
                    }
                    else
                    {
                        btn_excels.Visible = false;
                        print1.Visible = false;
                    }
                }
            }
            else
            {
                Alertme("Sorry there are no data list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }

            #region Comented
            //DataTable dt = mycode.FillData("Select oa.*,'0'  as admissionnumber,format(ore.Exam_Date_time, 'dd/MM/yyyy') as Created_datetime1,ore.Exam_Time,ore.Exam_end_time,ore.Day,(select top 1 Session from session_details where session_id=oa.Session_id) as Session_name,(select top 1 Course_Name from Add_course_table where course_id=oa.Class_id) as Course_Name,CASE WHEN oa.Student_image is null THEN '/images/dummy-student.jpg'  WHEN oa.Student_image is not null THEN oa.Student_image END AS Student_img,(Select top 1 Terms from Online_Admit_Card_Term) as Terms,ore.Exam_Type,(select top 1 Room_name from Online_admit_card_room where Room_id=ore.Room_id) as Room_Name,('('+oa.Registration_id+')') as New_Registration_id from Online_Admission oa join Online_Reg_Exam_Time_Table ore on oa.Registration_id=ore.Admission_no and oa.Session_id=ore.Session_Id where ore.Session_Id='" + ddl_session.SelectedValue + "' and ore.Class_id='" + ddl_class.SelectedValue + "' and ore.Room_id='" + ddl_room.SelectedValue + "'");
            //if (dt.Rows.Count == 0)
            //{
            //    Alertme("Sorry there are no data list exist", "warning");
            //    rd_view.DataSource = null;
            //    rd_view.DataBind();
            //}
            //else
            //{
            //    rd_view.DataSource = dt;
            //    rd_view.DataBind();
            //}
            #endregion
        }

        private static bool valid_amount(string p)
        {
            try
            {
                Convert.ToDouble(p);
                return true;
            }
            catch
            {
                return false;
            }
        }

        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbl_dob = ((Label)e.Item.FindControl("lbl_dob")) as Label;
                Label lbl_ages = ((Label)e.Item.FindControl("lbl_ages")) as Label;

                try
                {
                    string age = get_agess(Convert.ToDateTime(lbl_dob.Text));
                    lbl_ages.Text = age;
                }
                catch (Exception ex)
                {
                }
            }
        }

        private string get_agess(DateTime dateTime)
        {
            try
            { 
                DateTime today = Convert.ToDateTime("01/04/2025");
                DateTime dob = dateTime;
                TimeSpan ts = today - dob;
                DateTime age = DateTime.MinValue + ts;
                int years = age.Year - 1;
                int months = age.Month - 1;
                int days = age.Day - 1;
                string agesss = years + " years " + months + " months";
                return agesss;
            }
            catch (Exception ex)
            {
                return "0";
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
                        Panel1.RenderControl(hw);
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

        protected void ddl_session_SelectedIndexChanged(object sender, EventArgs e)
        {
            mycode.bind_all_ddl_with_id(ddl_test_name, "select Test_name,Test_id from Online_reg_exam_test_master where Session_id='" + ddl_session.SelectedValue + "'   order by  Test_name asc");
        }
    }
}