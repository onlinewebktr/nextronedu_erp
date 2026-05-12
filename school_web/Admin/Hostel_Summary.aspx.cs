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
    public partial class Hostel_Summary : System.Web.UI.Page
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
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["userType"] = My.get_user_type(ViewState["Userid"].ToString());
                        string pagename_current = Path.GetFileName(Request.Path);
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        find_firm_details();
                        //if (ViewState["userType"].ToString() == "User")
                        //{
                        mycode.bind_all_ddl_with_id_cap_All(ddl_hostel, "select Hostel_name,Hostel_id from Hostels_master order by Hostel_name asc");
                        ddl_hostel.SelectedValue = My.get_top_one_hostel_id();
                        bind_hostel_summary();
                        //}
                        //else
                        //{
                        //    mycode.bind_all_ddl_with_id(ddl_hostel, "select t2.Hostel_name,t1.Hostel_id from Hostel_warden_mapping t1 join Hostels_master t2 on t1.Hostel_id=t2.Hostel_id where t1.Warden_id='" + ViewState["Userid"].ToString() + "' order by t2.Hostel_name asc");
                        //}

                       

                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Hostel_Summary");
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

        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_hostel.SelectedItem.Text == "Select")
                {
                    Alertme("Please select hostel.", "warning");
                    ddl_hostel.Focus();
                }
                else
                {
                    bind_hostel_summary();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void bind_hostel_summary()
        {
            lbl_hostels_name.Visible = true;
            lbl_hostels_name.Text = "Hostel Name : " + ddl_hostel.SelectedItem.Text;
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@Hostel_id", ddl_hostel.SelectedValue);

            if(ddl_hostel.SelectedItem.Text=="ALL")
            {
                lbl_class22.Text = "";
                cmd.Parameters.AddWithValue("@sp_status", 8);
            }
            else
            {
                lbl_class22.Text = ddl_hostel.SelectedItem.Text;
                cmd.Parameters.AddWithValue("@sp_status", 9);

            }
            cmd.CommandText = "sp_Hostel_reports_new";

            DataTable dt = My.Getdata_sp(cmd);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no hostel summary exist.", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();

                
                //lbl_rooms.Text = "00";
                //lbl_ttl_beds.Text = "00";
                //lbl_ttl_allocated_beds.Text = "00";
                //lbl_aval_beds.Text = "00";
            }
            else
            {
                
                rd_view.DataSource = dt;
                rd_view.DataBind();

               
                
                //lbl_rooms.Text = dt.Rows[0]["ttl_rooms"].ToString();
                //lbl_ttl_beds.Text = dt.Rows[0]["total_bed"].ToString();
                //lbl_ttl_allocated_beds.Text = dt.Rows[0]["Total_occupied_bed"].ToString();
                //lbl_aval_beds.Text = dt.Rows[0]["Total_available_bed"].ToString();
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
                    Response.AddHeader("content-disposition", "attachment;filename=Export_Hostel_Summary.xls");
                    Response.Charset = "";
                     
                    Response.ContentType = "application/vnd.ms-excel";
                    using (StringWriter sw = new StringWriter())
                    {
                        HtmlTextWriter hw = new HtmlTextWriter(sw);
                        Panel1.RenderControl(hw);
                        string style = @"<style> TABLE { border: 1px solid black; } TD { border: 1px solid black; } </style> ";
                        Response.Write(style);
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