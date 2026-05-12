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
    public partial class App_Install_Report : System.Web.UI.Page
    {
        My mycode = new My();
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
                        find_firm_details();
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();

                        string pagename_current = "student-report-home.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"]; 
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
                        mycode.bind_all_ddl_with_id_All(ddlclass, "Select Course_Name,course_id from Add_course_table order by Position");
                        ddlclass.SelectedValue = My.get_top_one_class();
                        mycode.bind_ddlall_sm(ddl_Section, "Select distinct Section from admission_registor order by Section ");
                        ddl_Section.Text = My.get_top_one_section();
                        find_data();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "App_Install_Report");
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
        private void bind_grd_view(string query)
        {
            btn_excels.Visible = false;
            print1.Visible = false;

            ViewState["query"] = query;
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {

                btn_excels.Visible = false;
                Alertme("Sorry there are no data list exist", "warning");
                GrdView.DataSource = null;
                GrdView.DataBind();

            }
            else
            {

                btn_excels.Visible = true;
                GrdView.DataSource = dt;
                GrdView.DataBind();
                btn_excels.Visible = true;
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
        private void find_data()
        {
            if (ddlsession.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
                return;
            }

            string query = @"

    SELECT 

        ar.session,
        ar.admissionserialnumber,
        ar.class,
        ar.Section,
        ar.rollnumber,
        ar.studentname,
        ar.Status,
        ar.id,
        ar.gcm_id,
        ar.mobilenumber,
        ar.father_mob,

      ISNULL(COUNT(sd.DeviceId),0) AS Total_Device

    FROM admission_registor ar

    JOIN Add_course_table ad 
        ON ar.Class_id = ad.course_id

    LEFT JOIN Student_Device sd
        ON sd.admissionserialnumber = ar.admissionserialnumber

    WHERE 

        (ar.Transfer_Status='New' OR ar.Transfer_Status='NT')

        AND ar.StudentStatus='AV'

        AND ar.Status = 1

        AND ar.Session_id = " + ddlsession.SelectedValue;


            // CLASS FILTER
            if (ddlclass.SelectedItem.Text != "All")
            {
                query += " AND ar.Class_id = " + ddlclass.SelectedValue;
            }

            // SECTION FILTER
            if (ddl_Section.SelectedItem.Text != "All")
            {
                query += " AND ar.Section = '" + ddl_Section.SelectedItem.Text + "'";
            }

            // DOWNLOAD STATUS
            if (ddl_downloadstatus.Text == "Yes")
            {
                query += " AND ISNULL(ar.gcm_id,'') <> '' ";
            }
            else if (ddl_downloadstatus.Text == "No")
            {
                query += " AND ISNULL(ar.gcm_id,'') = '' ";
            }

            query += @"

    GROUP BY 

        ar.session,
        ar.admissionserialnumber,
        ar.class,
        ar.Section,
        ar.rollnumber,
        ar.studentname,
        ar.Status,
        ar.id,
        ar.gcm_id,
        ar.mobilenumber,
        ar.father_mob,
        ad.Position

    ORDER BY 

        ad.Position,
        ar.Section,
        ar.rollnumber ASC";



            lbl_class22.Text =
                "Session : " + ddlsession.SelectedItem.Text +
                " Class : " + ddlclass.SelectedItem.Text +
                " Section : " + ddl_Section.SelectedItem.Text +
                " Download Status : " + ddl_downloadstatus.Text;

            bind_grd_view(query);
        }
        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                find_data();
            }
            catch (Exception ex)
            {
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

        protected void GrdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_gcm_id =
                    (Label)e.Row.FindControl("lbl_gcm_id");

                Label lbl_status =
                    (Label)e.Row.FindControl("lbl_status");

                Label lbl_total_device =
                    (Label)e.Row.FindControl("lbl_total_device");

                LinkButton lnk_view =
                    (LinkButton)e.Row.FindControl("lnk_view");


                // DOWNLOAD STATUS

                if (string.IsNullOrWhiteSpace(lbl_gcm_id.Text))
                {
                    lbl_status.Text = "No";
                    lbl_status.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    lbl_status.Text = "Yes";
                    lbl_status.ForeColor = System.Drawing.Color.Green;
                }


                // TOTAL DEVICE

                int totalDevice = 0;

                int.TryParse(lbl_total_device.Text, out totalDevice);


                // BADGE COLOR

                if (totalDevice > 0)
                {
                    lbl_total_device.CssClass = "badge badge-success";

                    lnk_view.Visible = true;

                    lnk_view.Text = "View (" + totalDevice + ")";
                }
                else
                {
                    lbl_total_device.CssClass = "badge badge-zero";

                    lnk_view.Visible = false;
                }
            }
        }

         

        protected void lnk_view_Click(object sender, EventArgs e)
        {
            LinkButton lnk =
                (LinkButton)sender;

            GridViewRow row =
                (GridViewRow)lnk.NamingContainer;

            string admissionNo =
                ((Label)row.FindControl("Label6")).Text;


            DataTable dt = mycode.FillData(@"

        SELECT 
admissionserialnumber,
            DeviceId,
            LoginTime,
            Brand,
            Model,
            DeviceName,
            AndroidVersion

        FROM Student_Device

        WHERE admissionserialnumber = '" + admissionNo + @"'

        ORDER BY LoginTime DESC

    ");


            grd_device.DataSource = dt;

            grd_device.DataBind();


            // OPEN MODAL

            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "popup",
                "$('#deviceModal').modal('show');",
                true);
        }

        protected void lnk_remove_Click(object sender, EventArgs e)
        {
            

            LinkButton lnk = (LinkButton)sender;

            string[] data = lnk.CommandArgument.Split(',');

            string deviceId = data[0];
            string admno = data[1];

            mycode.executeQuery(@"

        DELETE FROM Student_Device WHERE DeviceId = '" + deviceId + @"' and admissionserialnumber='"+ admno + "'");


            // REFRESH MAIN GRID

            find_data();


            // REFRESH DEVICE GRID

            DataTable dt = mycode.FillData(@"

        SELECT 

            DeviceId,
            LoginTime,
            Brand,
            Model,
            DeviceName,
            AndroidVersion,admissionserialnumber

        FROM Student_Device where admissionserialnumber='"+ admno + "'  ORDER BY LoginTime DESC");

            grd_device.DataSource = dt;
            grd_device.DataBind();
            // REOPEN MODAL
            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "popup",
                "$('#deviceModal').modal('show');",
                true);
        }

        protected void lnk_close_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "closepopup",
                "$('#deviceModal').modal('hide');",
                true);

            find_data();
        }
    }
}