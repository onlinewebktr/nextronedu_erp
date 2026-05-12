using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class online_reg_create_test : System.Web.UI.Page
    {
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
                        string pagename_current = "online-reg-create-test.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];

                        mycode.bind_all_ddl_with_id(ddl_session, "Select Session,session_id from session_details order by Session asc");
                        ddl_session.SelectedValue = My.get_session_id_onlinereg();
                        bind_grd_view();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_Test_Master");
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

        My mycode = new My();
        private void bind_grd_view()
        {
            DataTable dt = mycode.FillData("select *,(select top 1 Session from session_details where session_id=Online_reg_exam_test_master.Session_id) as Session_name from Online_reg_exam_test_master order by Test_name asc");
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();


            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            ddl_session.Enabled = true;
            txt_test_name.Text = "";
            btn_cancel.Visible = false;
            btn_Submit.Text = "Add";
        }

        protected void btn_Submit_Click1(object sender, EventArgs e)
        {

            if (ddl_session.SelectedItem.Text == "Select")
            {
                Alertme("Please select session.", "warning");
                ddl_session.Focus();
                return;
            }
            if (txt_test_name.Text == "")
            {
                Alertme("Please enter admission name", "warning");
                txt_test_name.Focus();
                return;
            }

            if (btn_Submit.Text == "Add")
            {
                if (ViewState["Is_add"].ToString() == "1")
                {
                    submit_details();
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            else
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    update_update_details();
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");

                }
            }
        }

        private void update_update_details()
        {
            DataTable dsgdt = My.dataTable("select Test_name from Online_reg_exam_test_master where Test_name ='" + txt_test_name.Text + "' and Session_id='" + ddl_session.SelectedValue + "' and id!='" + hd_id.Value + "'");
            if (dsgdt.Rows.Count == 0)
            {
                SqlConnection conn = new SqlConnection(My.conn);
                SqlDataAdapter ad = new SqlDataAdapter("select * from Online_reg_exam_test_master where  id='" + hd_id.Value + "'", conn);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                DataTable dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    dr["Test_name"] = txt_test_name.Text;
                }
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(dt);
                Alertme("Test details updated successfully", "success");
                empty_form();
                bind_grd_view();
            }
            else
            {
                Alertme("Admission name already exist with this name.", "warning");
            }
        }

        private void submit_details()
        {
            DataTable dsgdt = My.dataTable("select Test_name from Online_reg_exam_test_master where Test_name ='" + txt_test_name.Text + "' and Session_id='" + ddl_session.SelectedValue + "'");
            if (dsgdt.Rows.Count == 0)
            {
                string Online_reg_test_id = My.auto_serialS("Online_reg_test_id");
                SqlConnection conn = new SqlConnection(My.conn);
                SqlDataAdapter ad = new SqlDataAdapter(" select * from Online_reg_exam_test_master", conn);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                DataTable dt = ds.Tables[0];
                DataRow dr = dt.NewRow();
                dr["Session_id"] = ddl_session.SelectedValue;
                dr["Test_id"] = Online_reg_test_id;
                dr["Test_name"] = txt_test_name.Text;
                dr["Created_date"] = mycode.date();
                dr["Created_idate"] = mycode.idate();
                dr["Is_active"] = 1;

                dt.Rows.Add(dr);
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(dt);
                Alertme("Test details created successfully", "success");
                mycode.executequery ("update Online_reg_exam_test_master set Is_active=0 where Test_id!="+ Online_reg_test_id + " and Session_id='"+ ddl_session.SelectedValue + "'");
                empty_form();
                bind_grd_view();
            }
            else
            {
                Alertme("Admission name already exist with this name.", "warning");
            }
        }

        private void empty_form()
        {
            ddl_session.Enabled = true;
            txt_test_name.Text = "";
            btn_cancel.Visible = false;
            btn_Submit.Text = "Add";
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_session_id = (Label)row.FindControl("lbl_session_id");
                    Label lbl_test_name = (Label)row.FindControl("lbl_test_name");
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");
                    hd_id.Value = lbl_Id.Text;

                    ddl_session.SelectedValue = lbl_session_id.Text;
                    txt_test_name.Text = lbl_test_name.Text;
                    ddl_session.Enabled = false;
                    ddl_session.CssClass = "form-control";
                    btn_cancel.Visible = true;
                    btn_Submit.Text = "Update";
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

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            if (ViewState["Is_Edit"].ToString() == "1")
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                Label lbl_test_id = (Label)row.FindControl("lbl_test_id");

                DataTable dsgdt = My.dataTable("select * from Online_Admission where Test_id ='" + lbl_test_id.Text + "'");
                if (dsgdt.Rows.Count == 0)
                {
                    SqlConnection conn = new SqlConnection(My.conn);
                    SqlDataAdapter ad = new SqlDataAdapter("select * from Online_reg_exam_test_master where  id='" + lbl_Id.Text + "'", conn);
                    DataSet ds = new DataSet();
                    ad.Fill(ds);
                    DataTable dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        dr.Delete();
                    }
                    SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                    ad.Update(dt);
                    Alertme("Test deleted Successfully", "success");
                    bind_grd_view();
                }
                else
                {
                    Alertme("You can't delete this Test because there is a data associated with student.", "warning");
                }
            }
            else
            {
                Alertme(My.get_restricted_message(), "warning");
            }
        }
    }
}