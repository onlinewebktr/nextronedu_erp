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
    public partial class Scholarship_set_Parameter_of_Scholarship : System.Web.UI.Page
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
                        string pagename_current = "Scholarship_set_Parameter_of_Scholarship.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        find_firm_details();

                        mycode.bind_all_ddl_with_id(ddl_session, "Select Session,session_id from session_details order by Session asc");
                        ddl_session.SelectedValue = My.get_session_id_onlinereg();
                        bind_test();
                        bind_grd_view();

                        try
                        {
                            if (Session["msgs"] == null)
                            {

                            }
                            else
                            {
                                Alertme(Session["msgs"].ToString(), "success");
                                Session["msgs"] = null;

                            }
                        }
                        catch
                        {

                        }


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
            string query = " Select orf.*,act.Course_Name,(Select top  1 Session from session_details where session_id=orf.Session_id) as Session,CASE WHEN Isactive = '1' THEN 'Published' WHEN Isactive = '0' THEN 'Stopped'  WHEN Isactive = '' THEN 'Stopped' END AS activestatus,(Select top  1 Test_name from Scholarship_Program where Session_id=orf.Session_id and Test_id=orf.Test_id) as Test_name   from Scholarship_Parameter_fees orf   join Add_course_table  act on act.course_id=orf.Class_id where Session_id='" + ddl_session.SelectedValue + "' and Branchi_id='" + ViewState["branchid"].ToString() + "' order by act.Position asc";

            bind_final_grid_data(query);
        }

        private void bind_final_grid_data(string query)
        {
            btn_excels.Visible = false;
            DataTable dt = mycode.FillData(query);
            ViewState["query"] = query;
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no Scholarship Program Parameter data exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                btn_excels.Visible = true;
                rd_view.DataSource = dt;
                rd_view.DataBind();
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
                Alertme("Please select scholarship name", "warning");
            }
            else
            {
                string query = " Select orf.*,act.Course_Name,(Select top  1 Session from session_details where session_id=orf.Session_id) as Session,CASE WHEN Isactive = '1' THEN 'Published' WHEN Isactive = '0' THEN 'Stopped'  WHEN Isactive = '' THEN 'Stopped' END AS activestatus,(Select top  1 Test_name from Scholarship_Program where Session_id=orf.Session_id and Test_id=orf.Test_id) as Test_name   from Scholarship_Parameter_fees orf   join Add_course_table  act on act.course_id=orf.Class_id where Session_id='" + ddl_session.SelectedValue + "' and Branchi_id='" + ViewState["branchid"].ToString() + "' and orf.Test_id='"+ddl_test_name.SelectedValue+"' order by act.Position asc";
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

        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {

                    if (((Label)e.Item.FindControl("lbl_Isactive")).Text == "1")
                    {
                        ((LinkButton)e.Item.FindControl("lnkActive")).Text = "Stop";
                    }
                    else
                    {
                        ((LinkButton)e.Item.FindControl("lnkActive")).Text = "Publish";
                    }
                    string courseid = ((Label)e.Item.FindControl("lbl_courseid")).Text;
                    string lbl_no_application = ((Label)e.Item.FindControl("lbl_no_application")).Text;
                }
            }
            catch { }
        }
        #region active and inactive
        protected void lnkActive_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_add"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");
                    Label lbl_Isactive = (Label)row.FindControl("lbl_Isactive");

                    if (lbl_Isactive.Text == "")// Inactive
                    {
                        mycode.executequery("update Scholarship_Parameter_fees set Isactive='1' where Id='" + lbl_Id.Text + "'");
                        Alertme("Your select scholarship program has been published successfully", "success");

                    }
                    else if (lbl_Isactive.Text == "0")// Inactive
                    {
                        mycode.executequery("update Scholarship_Parameter_fees set Isactive='1' where Id='" + lbl_Id.Text + "'");
                        Alertme("Your select scholarship program has been published successfully", "success");
                    }
                    else if (lbl_Isactive.Text == "1")// Inactive
                    {
                        mycode.executequery("update Scholarship_Parameter_fees set Isactive='0' where Id='" + lbl_Id.Text + "'");

                        Alertme("Your select scholarship program has been stopped successfully", "success");
                    }
                    bind_grd_view();
                }
                else if (ViewState["Is_Edit"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");
                    Label lbl_Isactive = (Label)row.FindControl("lbl_Isactive");

                    if (lbl_Isactive.Text == "")// Inactive
                    {
                        mycode.executequery("update Scholarship_Parameter_fees set Isactive='1' where Id='" + lbl_Id.Text + "'");
                        Alertme("Your select scholarship program has been published successfully", "success");

                    }
                    else if (lbl_Isactive.Text == "0")// Inactive
                    {
                        mycode.executequery("update Scholarship_Parameter_fees set Isactive='1' where Id='" + lbl_Id.Text + "'");
                        Alertme("Your select scholarship program has been published successfully", "success");
                    }
                    else if (lbl_Isactive.Text == "1")// Inactive
                    {
                        mycode.executequery("update Scholarship_Parameter_fees set Isactive='0' where Id='" + lbl_Id.Text + "'");

                        Alertme("Your select scholarship program has been stopped successfully", "success");
                    }
                    bind_grd_view();

                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }

            }
            catch (Exception ex)
            {


            }


        }
        #endregion


        #region edit and delete
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    hd_id.Value = "";
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");
                    Response.Redirect("Scholarship_Add_Program_Parameter.aspx?id=" + lbl_Id.Text, false);
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            catch (Exception ex)
            {
            }
        }



        #endregion

        protected void lnk_delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_delete"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");
                    Label lbl_test_id = (Label)row.FindControl("lbl_test_id");
                    DataTable dsgdt = My.dataTable("select * from Scholarship_Admission where Test_id ='" + lbl_test_id.Text + "'");
                    if (dsgdt.Rows.Count == 0)
                    {
                        My.exeSql("delete from  Scholarship_Parameter_fees where id=" + lbl_Id.Text + "");

                        Alertme("Scholarship program parameter name has been deleted Successfully", "success");
                        bind_grd_view();

                    }
                    else
                    {

                        Alertme("You can't delete this scholarship program parameter name because there is a associated with student.", "warning");
                    }
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
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

        protected void lnk_view_Scholorship_Guidelines_Click(object sender, EventArgs e)
        {
            try
            {
                lbl_data_heading.Text = "Scholarship Guidelines";
                lbl_data.Text = "";
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Scholorship_Guidelines = (Label)row.FindControl("lbl_Scholorship_Guidelines");
                lbl_data.Text = lbl_Scholorship_Guidelines.Text;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch
            {

            }
        }
        protected void lnk_Scholorship_Benefit_Click(object sender, EventArgs e)
        {
            try
            {
                lbl_data_heading.Text = "Scholarship Benefits";
                lbl_data.Text = "";
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Scholorship_Benefit = (Label)row.FindControl("lbl_Scholorship_Benefit");
                lbl_data.Text = lbl_Scholorship_Benefit.Text;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch
            {

            }

        }
    }
}