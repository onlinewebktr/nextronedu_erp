using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
using System.IO;

namespace school_web.Admin
{
    public partial class Scholarship_Status : System.Web.UI.Page
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
                        ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        //string pagename_current = "Scholarship_set_Parameter_of_Scholarship.aspx";
                        //Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = "1";//(String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = "1";
                        ViewState["Is_Download"] = "1";
                        ViewState["Is_Print"] = "1";
                        ViewState["Is_add"] = "1";
                        find_firm_details();

                        mycode.bind_all_ddl_with_id(ddl_session, "Select Session,session_id from session_details order by Session asc");
                        ddl_session.SelectedValue = My.get_session_id_onlinereg();
                        bind_test();
                        bind_grd_view();

                        //try
                        //{
                        //    if (Session["msgs"] == null)
                        //    {

                        //    }
                        //    else
                        //    {
                        //        Alertme(Session["msgs"].ToString(), "success");
                        //        Session["msgs"] = null;

                        //    }
                        //}
                        //catch
                        //{

                        //}


                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Set_Course_Fee");
            }
        }


        private void bind_grd_view()
        {
            
            string query = " Select orf.*,act.Course_Name,(Select top  1 Session from session_details where session_id=orf.Session_id) as Session,CASE WHEN Isactive = '1' THEN 'Published' WHEN Isactive = '0' THEN 'Stopped'  WHEN Isactive = '' THEN 'Stopped' END AS activestatus,(Select top  1 Test_name from Scholarship_Program where Session_id=orf.Session_id and Test_id=orf.Test_id) as Test_name   from Scholarship_Parameter_fees orf   join Add_course_table  act on act.course_id=orf.Class_id where Session_id='" + ddl_session.SelectedValue + "' and Branchi_id='" + ViewState["branchid"].ToString() + "'  order by act.Position asc";

            bind_final_grid_data(query);
        }

        private void bind_final_grid_data(string query)
        {
            ViewState["query"] = query;
            btn_excels.Visible = false;
            DataTable dt = mycode.FillData(query);
            ViewState["query"] = query;
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no scholarship program parameter data exist", "warning");
                GrdView.DataSource = null;
                GrdView.DataBind();
                btn_active.Visible = false;
                btn_inactive.Visible=false;
                print1.Visible = false;
            }
            else
            {
                btn_active.Visible = true;
                btn_inactive.Visible = true;
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

                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();
            }
        }
        private void bind_test()
        {

            mycode.bind_all_ddl_with_id(ddl_test_name, "select Test_name,Test_id from Scholarship_Program where Session_id='" + ddl_session.SelectedValue + "' order by  Test_name asc");

        }

        protected void ddl_test_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_test_name.SelectedItem.Text == "Select")
            {
                Alertme("Please select  scholarship name", "warning");
            }
            else
            {
                string query = " Select orf.*,act.Course_Name,(Select top  1 Session from session_details where session_id=orf.Session_id) as Session,CASE WHEN Isactive = '1' THEN 'Published' WHEN Isactive = '0' THEN 'Stopped'  WHEN Isactive = '' THEN 'Stopped' END AS activestatus,(Select top  1 Test_name from Scholarship_Program where Session_id=orf.Session_id and Test_id=orf.Test_id) as Test_name   from Scholarship_Parameter_fees orf   join Add_course_table  act on act.course_id=orf.Class_id where Session_id='" + ddl_session.SelectedValue + "' and Branchi_id='" + ViewState["branchid"].ToString() + "' and orf.Test_id=" + ddl_test_name.SelectedValue + " order by act.Position asc";
                bind_final_grid_data(query);
            }

        }

        protected void ddl_session_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_session.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
            }
            else
            {
                mycode.bind_all_ddl_with_id(ddl_test_name, "select Test_name,Test_id from Scholarship_Program where Session_id='" + ddl_session.SelectedValue + "' order by  Test_name asc");
                bind_grd_view();
            }
        }

        protected void GrdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_Isactive = (Label)e.Row.FindControl("lbl_Isactive");
                
                LinkButton lnkEdit = (LinkButton)e.Row.FindControl("lnkEdit");
                if (lbl_Isactive.Text == "")
                {
                     
                    lnkEdit.Text = "Publish";  
                    lnkEdit.BackColor = System.Drawing.Color.Green;
                    lnkEdit.ForeColor = System.Drawing.Color.White;
                }
                else if (lbl_Isactive.Text == "0")
                {
                   
                    lnkEdit.Text = "Publish";
                    lnkEdit.BackColor = System.Drawing.Color.Green;
                    lnkEdit.ForeColor = System.Drawing.Color.White;
                }
                else
                {
                    
                    lnkEdit.Text = "Stop";
                    lnkEdit.BackColor = System.Drawing.Color.Red;
                    lnkEdit.ForeColor = System.Drawing.Color.White;
                }
            }

        }
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            if (ViewState["Is_add"].ToString() == "1")
            {
                LinkButton lnk = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lnk.Parent.Parent;
                Label lbl_id = (Label)row.FindControl("lbl_id");
                Label lbl_Isactive = (Label)row.FindControl("lbl_Isactive");

                if (lbl_Isactive.Text == "")
                {
                    mycode.executequery("update Scholarship_Parameter_fees set Isactive='1' where id=" + lbl_id.Text + "");

                    Alertme("This scholarship program has been successfully published", "success");

                }

                else if (lbl_Isactive.Text == "0")
                {
                    mycode.executequery("update Scholarship_Parameter_fees set Isactive='1' where id=" + lbl_id.Text + "");
                    Alertme("This scholarship program has been successfully published", "success");
                }
                else
                {
                    mycode.executequery("update Scholarship_Parameter_fees set Isactive='0' where id=" + lbl_id.Text + "");
                    Alertme("This scholarship program has been successfully stopped", "success");
                }

                bind_final_grid_data(ViewState["query"].ToString());
            }
            else if (ViewState["Is_Edit"].ToString() == "1")
            {
                LinkButton lnk = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lnk.Parent.Parent;
                Label lbl_id = (Label)row.FindControl("lbl_id");
                Label lbl_Isactive = (Label)row.FindControl("lbl_Isactive");

                if (lbl_Isactive.Text == "")
                {
                    mycode.executequery("update Scholarship_Parameter_fees set Isactive='1' where id=" + lbl_id.Text + "");

                    Alertme("This scholarship program has been successfully published", "success");

                }

                else if (lbl_Isactive.Text == "0")
                {
                    mycode.executequery("update Scholarship_Parameter_fees set Isactive='1' where id=" + lbl_id.Text + "");
                    Alertme("This scholarship program has been successfully published", "success");
                }
                else
                {
                    mycode.executequery("update Scholarship_Parameter_fees set Isactive='0' where id=" + lbl_id.Text + "");
                    Alertme("This scholarship program has been successfully stopped", "success");
                }
                bind_final_grid_data(ViewState["query"].ToString());

            }
            else
            {
                Alertme(My.get_restricted_message(), "warning");
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
                        Alertme("Please select at least one scholarship program from list", "warning");
                    }
                    else
                    {
                         
                        Alertme("Scholarship program has been successfully stopped", "success");
                        bind_final_grid_data(ViewState["query"].ToString());
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
                        Alertme("Please select at least one scholarship program from list", "warning");
                    }
                    else
                    {
                        Alertme("Scholarship program has been successfully stopped", "success");
                        bind_final_grid_data(ViewState["query"].ToString());
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
                            mycode.executequery("update Scholarship_Parameter_fees set Isactive='1' where id=" + lbl_id.Text + "");


                        }
                        else
                        {
                            k++;
                        }
                    }
                    if (k == growcount)
                    {
                        Alertme("Please select at least one scholarship program from list", "warning");
                    }
                    else
                    {
                        Alertme("Scholarship program has been successfully published", "success");
                        bind_final_grid_data(ViewState["query"].ToString());
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
                            mycode.executequery("update Scholarship_Parameter_fees set Isactive='1' where id=" + lbl_id.Text + "");


                        }
                        else
                        {
                            k++;
                        }
                    }
                    if (k == growcount)
                    {
                        Alertme("Please select at least one scholarship program from list", "warning");
                    }
                    else
                    {
                        Alertme("Scholarship program has been successfully published", "success");
                        bind_final_grid_data(ViewState["query"].ToString());
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