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
    public partial class House_wise_student_list : System.Web.UI.Page
    {
        My mycode = new My();
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
                        ViewState["firm_id"] = Session["firm"].ToString();

                        string pagename_current = "student-report-home.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        find_firm_details();
                        if (ViewState["Is_Print"].ToString() == "1")
                        {
                            print1.Visible = true;
                        }
                        else
                        {
                            print1.Visible = false;
                        }
                        mycode.bind_all_ddl_with_id(ddlsession, "Select  Session,session_id from session_details");
                        ddlsession.SelectedValue = My.get_session_id();
                        bind_class();
                        mycode.bind_all_ddl_with_id_cap_All_name(ddl_house, "Select  house_name,house_id from house_master order by house_name");

                        Bind_data_student();




                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Active_Inactive_Student");
            }
        }
        protected void btn_find_Click(object sender, EventArgs e)
        {
            Bind_data_student();
        }

        private void bind_class()
        {
            using (SqlConnection conn = new SqlConnection(My.conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Select Course_Name,course_id from Add_course_table order by Position", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                ddl_classs.DataTextField = "Course_Name";
                ddl_classs.DataValueField = "course_id";
                ddl_classs.DataSource = reader;
                ddl_classs.DataBind();
            }
            foreach (ListItem item in ddl_classs.Items)
            {
                item.Selected = true;
            }
            if (ViewState["FrmId"].ToString() == "NNI-01")
            {
                foreach (ListItem item in ddl_classs.Items)
                {
                    if (Convert.ToInt32(item.Value) > 15)
                    {
                        item.Selected = false;
                    }
                }
            }
        }

        private void Bind_data_student()
        {
            if (ddlsession.SelectedItem.Text == "Select")
            {
                Alertme("Please select session.", "warning");
                return;
            }

            string qoute = "'";
            //For Class
            bool isClassSelectd = false; string selectClassid = "";
            foreach (ListItem item in ddl_classs.Items)
            {
                if (item.Selected)
                {
                    selectClassid = selectClassid + qoute + item.Value + qoute + ",";
                    isClassSelectd = true;
                }
            }
            if (isClassSelectd == false)
            {
                ddl_classs.Focus();
                Alertme("Please select class.", "warning");
                return;
            }
            if (isClassSelectd == true)
            {
                selectClassid = selectClassid.Remove(selectClassid.Length - 1);
            }



            var condition = "where 1=1 ";
            condition += $" and  Session_id='{ddlsession.SelectedValue}' and Class_id in (" + selectClassid + ") ";
            if (ddl_house.SelectedItem.Text == "NA")
            {
                condition += $" and (house is null or house='') ";
            }
            else if (ddl_house.SelectedItem.Text != "ALL")
            {
                condition += $" and house='{ddl_house.SelectedValue}' ";
            }

            DataTable dt = My.MydataTable($@"select class,admissionserialnumber,rollnumber,Section,session,studentname,gender,fathername,(Select top 1 house_name from house_master where house_id=admission_registor.house) as housename from admission_registor join  Add_course_table ad  on admission_registor.Class_id=ad.course_id  {condition} order by ad.Position, admission_registor.Section, admission_registor.rollnumber");
            if (dt.Rows.Count == 0)
            {
                btn_excels.Visible = false;
                Alertme("Sorry there are no data list exist", "warning");
                GrdView.DataSource = null;
                GrdView.DataBind();
                lbl_class222.Text = "";
            }
            else
            {
                lbl_class222.Text = "Session : " + ddlsession.SelectedItem.Text + " House : " + ddl_house.SelectedItem.Text;
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

        private void find_firm_details()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count > 0)
            {
                imglogo.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_emaiid.Text = dt.Rows[0]["email"].ToString();
                lbl_website.Text = dt.Rows[0]["website"].ToString();
                lbl_contact_details.Text = dt.Rows[0]["contact_no"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();
                ViewState["FrmId"] = dt.Rows[0]["firm_id"].ToString();
            }
        }

        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                string erport = My.with_excel_name("Housewisestudent");
                if (ViewState["Is_Download"].ToString() == "1")
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=" + erport + ".xls");
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


    }
}