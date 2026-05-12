using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.IO;
using System.Data;
namespace school_web.LMS_VC_Admin
{
    public partial class Student_view_Attendance_Chart : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
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
                    ViewState["Admin"] = Session["Admin"].ToString();
                    code.bind_all_ddl_with_id(ddl_session, "Select Session, session_id from session_details  ");
                    code.bind_ddl_month(ddl_month);
                    string monthval = code.getval();
                    ddl_month.SelectedValue = monthval;

                    ddl_session.SelectedValue = code.get_session_id_use();

                    code.bind_all_ddl_with_id(ddl_class, "public void GrdData(DataTable sourcetable, GridView gridContainer)");
                }

            }
        }
        public void Alert(string Message)
        {
            lblmessage.Text = Message;

            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }
        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_class.SelectedItem.Text == "Select")
            {
                Alert("Please select class");
            }
            else
            {

                code.bind_ddl(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddl_class.SelectedValue + "'    order by Section");

            }
        }
        protected void imgexcel2_Click(object sender, ImageClickEventArgs e)
        {

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=AttendanceExport" + "_" + ".xls");
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

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        #region find data
        protected void btn_find_Click(object sender, EventArgs e)
        {
            if (ddl_session.SelectedItem.Text == "Select")
            {
                Alert("Please select session");
            }
            else if (ddl_month.Text == "Select")
            {
                Alert("Please select month");
            }
            else if (ddl_class.SelectedItem.Text == "Select")
            {
                Alert("Please select class");
            }
            else if (ddl_section.Text == "Select")
            {
                Alert("Please select section");
            }
            else
            {
                string query = " Select ar.admissionserialnumber,ar.rollnumber,ar.Section, ar.class  as classname,ar.studentname,sacc.* from admission_registor ar join Student_attendence_chart_column_wise sacc on ar.admissionserialnumber=sacc.Admission_no where ar.Class_id='" + ddl_class.SelectedValue + "' and ar.section='" + ddl_section.Text + "' and ar.session='" + ddl_session.SelectedItem.Text + "' and  sacc.month='" + ddl_month.SelectedValue + "'  order by ar.rollnumber";
                SqlCommand cmd = new SqlCommand(query);
                DataTable dt = code.FillTable(query);
                if (dt.Rows.Count == 0)
                {
                   
                    imgexcel2.Visible = false;
                    GrdView.DataSource = null;
                    GrdView.DataBind();
                    grid111.Visible = false;
                    lbltotal_student.Text = "0";
                    lbl_persenstudent.Text = "0";
                    lbl_totalabsentstudent.Text = "0";
                    Alert("Sorry your selected month is not calculated attendance.");
                }
                else
                {
                     
                    imgexcel2.Visible = true;
                    lbltotal_student.Text = dt.Rows.Count.ToString();
                    GrdView.DataSource = dt;
                    GrdView.DataBind();
                    grid111.Visible = true;



                    int year = Convert.ToInt32(code.get_firstyear(ddl_session.SelectedItem.Text));
                    int days = DateTime.DaysInMonth(year, Convert.ToInt32(ddl_month.SelectedValue));
                    if (days == 27)
                    {

                        Hid_filed(31, 32, 33, 34, false, false, false, false);
                    }
                    else if (days == 28)
                    {
                        Hid_filed(31, 32, 33, 34, true, false, false, false);
                    }
                    else if (days == 30)
                    {
                        Hid_filed(31, 32, 33, 34, true, true, true, false);
                       
                    }
                    else if (days == 31)
                    {
                        Hid_filed(31, 32, 33, 34, true, true, true, true);
                    }
                 
                    // bind_student_count();



                }
            }
        }

        private void Hid_filed(int a, int b, int c, int d, bool v1, bool v2, bool v3, bool v4)
        {
            GrdView.Columns[a].Visible = v1;
            GrdView.Columns[b].Visible = v2;
            GrdView.Columns[c].Visible = v3;
            GrdView.Columns[d].Visible = v4;
        }

        private void bind_student_count()
        {

        }
        #endregion
    }
}