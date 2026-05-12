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
    public partial class Set_Student_Wise_Discount_type_head : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Session["Admin"] = "edunext2021";
            //Session["firm"] = "1";
            Session["branchid"] = "1";
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
                        string pagename_current = "set-discount-on-admission-fee.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        bind_grd_view();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_Strength_Master");
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
            DataTable dt = mycode.FillData("select * from dbo.[Student_Discunt_Type]");
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
            txt_category_name.Text = "";
            btn_cancel.Visible = false;
            btn_Submit.Text = "Add";
        }

        protected void btn_Submit_Click1(object sender, EventArgs e)
        {
            try
            {


                if (txt_category_name.Text == "")
                {
                    Alertme("Please Enter Discunt Mode", "warning");
                    txt_category_name.Focus();
                    return;
                }

                if (btn_Submit.Text == "Add")
                {
                    if (ViewState["Is_add"].ToString() == "1")
                    {
                        submit_details();
                        empty_form();
                        bind_grd_view();
                    }
                    else
                    {
                        Alertme("SORRY! You have not permission for this work.", "warning");
                    }
                }
                else
                {
                    if (ViewState["Is_Edit"].ToString() == "1")
                    {
                        update_update_details();
                        empty_form();
                        bind_grd_view();

                    }
                    else
                    {
                        Alertme("SORRY! You have not permission for this work.", "warning");

                    }

                }
            }
            catch
            {

            }
        }

        private void update_update_details()
        {
            DataTable dt = mycode.FillData("Select * from Student_Discunt_Type where Discunt_Type='" + txt_category_name.Text + "' and Id!=" + hd_id.Value + "");
            if (dt.Rows.Count == 0)
            {
                My.Update("Student_Discunt_Type", new
                {
                    Id = hd_id.Value,
                    Discunt_Type = txt_category_name.Text,
                    Date_updated = My.getdate1(),
                    updated_by = ViewState["Userid"].ToString(),
                }, where: $"Id='{hd_id.Value}'");

                Alertme("Data has been successfully update", "success");
            }
            else
            {
                Alertme("Sorry your entered discount mode already exists", "warning");
            }
        }

        private void submit_details()
        {
            DataTable dt = mycode.FillData("Select * from Student_Discunt_Type where Discunt_Type='" + txt_category_name.Text + "'");
            if (dt.Rows.Count == 0)
            {

                string Student_Discunt_Type_id = My.auto_serialS("group_id");
                My.Insert("Student_Discunt_Type", new
                {

                    Discunt_Type = txt_category_name.Text,
                    Student_Discunt_Type_id = Student_Discunt_Type_id,
                    Date_created = My.getdate1(),
                    Created_by= ViewState["Userid"].ToString(),

                });

                Alertme("Data has been successfully added", "success");
            }
            else
            {
                Alertme("Sorry your entered discount mode already exists", "warning");
            }


        }

        private void empty_form()
        {
            txt_category_name.Text = "";
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
                    Label lbl_Discunt_Type = (Label)row.FindControl("lbl_Discunt_Type");
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");
                    hd_id.Value = lbl_Id.Text;

                    txt_category_name.Text = lbl_Discunt_Type.Text;
                    btn_cancel.Visible = true;
                    btn_Submit.Text = "Update";
                }
                else
                {
                    Alertme("SORRY! You have not permission for this work.", "warning");
                }
            }
            catch
            {

            }
        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            if (ViewState["Is_delete"].ToString() == "1")
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                Label lbl_Student_Discunt_Type_id = (Label)row.FindControl("lbl_Student_Discunt_Type_id");
                DataTable dsgdt = My.dataTable("select * from Discount_Master where Student_Discunt_Type_id ='" + lbl_Student_Discunt_Type_id.Text + "'");
                if (dsgdt.Rows.Count == 0)
                {
                    SqlConnection conn = new SqlConnection(My.conn);
                    SqlDataAdapter ad = new SqlDataAdapter("select * from dbo.[Student_Discunt_Type] where  id='" + lbl_Id.Text + "'", conn);
                    DataSet ds = new DataSet();
                    ad.Fill(ds);
                    DataTable dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        dr.Delete();
                    }
                    SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                    ad.Update(dt);
                    Alertme("Record has been deleted successfully.", "success");
                    bind_grd_view();
                }
                else
                {
                    Alertme("You can't delete this discount mode because this discount mode already associated with student ", "warning");
                }
            }
            else
            {
                Alertme("SORRY! You have not permission for this work.", "warning");
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
                    Alertme("SORRY! You have not permission for this work.", "warning");
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