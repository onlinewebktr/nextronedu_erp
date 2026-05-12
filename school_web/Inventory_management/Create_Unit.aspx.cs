using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace school_web.Inventory_management
{
    public partial class Create_Unit : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Admin"] == null )
            {
                Session.Abandon();
                Session.Clear();
                Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
                Response.Write("<script language=javascript>wnd.close();</script>");
                Response.Write("<script language=javascript>window.open('logout.aspx','_parent',replace=true);</script>");
            }
            else
            {
                ViewState["Userid"] = Session["Admin"].ToString();
                if (!IsPostBack)
                {
                    try
                    {
                        //string pagename_current = Path.GetFileName(Request.Path);
                        //Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        //ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        //ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        //ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        //ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        //ViewState["Is_add"] = (String)dc1["Is_add"];
                        ViewState["Is_Edit"] = "1"; //(String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = "1";//(String)dc1["Is_delete"];
                        ViewState["Is_Download"] = "1"; //(String)dc1["Is_Download"];
                        ViewState["Is_Print"] = "1"; //(String)dc1["Is_Print"];
                        ViewState["Is_add"] = "1"; //(String)dc1["Is_add"];

                        bind_grd_view();
                        bind_UQS();
                    }
                    catch (Exception ex)
                    {
                        Alertme("Please try again..", "warning");
                        My.submitexception(ex.ToString());
                    }
                }
            }
        }

        private void bind_grd_view()
        {
            DataTable dt = My.dataTable("select * from unit_master where firm='" + My.firm_id() + "';");
            if (dt.Rows.Count > 0)
            {
                GrdView_Create_Unit.DataSource = dt;
                GrdView_Create_Unit.DataBind();
            }
            else
            {
                GrdView_Create_Unit.DataSource = dt;
                GrdView_Create_Unit.DataBind();
            }
        }
        private void bind_UQS()
        {
            DataTable dt = My.dataTable("select Unit_Name+'-'+Code as Unit,Code from dbo.[UQC_Code]  ");
            if (dt.Rows.Count > 0)
            {
                ddl_uqc.DataTextField = "Unit";
                ddl_uqc.DataValueField = "Code";
                ddl_uqc.DataSource = dt;
                ddl_uqc.DataBind();
                ddl_uqc.Items.Insert(0, new ListItem("Select", "0"));

            }
        }

        private void bind_grd_view(string qry)
        {
            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data exist", "warning");
                GrdView_Create_Unit.DataSource = null;
                GrdView_Create_Unit.DataBind();
            }
            else
            {
                GrdView_Create_Unit.DataSource = dt;
                GrdView_Create_Unit.DataBind();
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
        private void empty_form()
        {
            Btn_Add.Visible = true;
            Btn_Update.Visible = false;
            My.ClearInputs(Page.Controls);
        }
        protected void Btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                if (Txt_Unit_Name.Text == "")
                {
                    Alertme("Please Enter Unit Name", "warning");
                    Txt_Unit_Name.Focus();
                    return;
                }
                if (ddl_uqc.SelectedValue == "0")
                {
                    Alertme("Please Select UQS", "warning");
                    ddl_uqc.Focus();
                    return;
                }
                if (check_for_duplicate_tax())
                {
                    Alertme("Already added.", "warning");
                    return;
                }
                else
                {
                    if (ViewState["Is_add"].ToString() == "1")
                    {
                        Save_data();
                        empty_form();
                    }
                    else
                    {
                        Alertme(My.get_restricted_message(), "warning");
                    }

                }

            }
            catch (Exception ex)
            {
                Alertme("Please try again..", "warning");
                My.submitexception(ex.ToString());
            }
        }
        private bool check_for_duplicate_tax()
        {
            DataTable dt = My.dataTable("select * from unit_master where  Unit='" + Txt_Unit_Name.Text + "' and firm='" + My.firm_id() + "'");
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private void Save_data()
        {
            string uqc_code = ddl_uqc.SelectedItem.ToString();
            string name = uqc_code.Split('-')[0];
            string code = uqc_code.Split('-')[1];
            string unit_id = My.auto_serialS("unit_id");
            string insert_qr = "insert into unit_master (unit_id,Unit,user_id,date,uqc_code,Uqc_Unit_name,firm) values ('" + unit_id + "','" + Txt_Unit_Name.Text.Replace("'", "''") + "','" + Session["Admin"].ToString() + "','" + mycode.date() + "','" + code + "','" + name + "','" + My.firm_id() + "'); select * from unit_master where  firm ='" + My.firm_id() + "';";

            DataTable dt = My.dataTable(insert_qr);
            Alertme("Unit Created Successfully", "success");
            My.send_data_to_user_log_history(Session["name"].ToString() + " Add new unit name : " + name, Session["Admin"].ToString());

            bind_grd_view();
        }

        protected void Btn_Cancel_Click(object sender, EventArgs e)
        {
            empty_form();
        }

        protected void Btn_Update_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_uqc.SelectedValue == "0")
                {
                    Alertme("Please Select UAC Code", "warning");
                    ddl_uqc.Focus();
                    return;
                }
                if (Txt_Unit_Name.Text == "")
                {
                    Alertme("Please Enter Unit", "warning");
                    Txt_Unit_Name.Focus();
                    return;
                }

                Update_data();
                empty_form();
            }
            catch (Exception ex)
            {
                Alertme("Please try again..", "warning");
                My.submitexception(ex.ToString());
            }
        }

        private void Update_data()
        {
            string uqc_code = ddl_uqc.SelectedItem.ToString();
            string name = uqc_code.Split('-')[0];
            string code = uqc_code.Split('-')[1];
            string update_qry = "update unit_master set Unit='" + Txt_Unit_Name.Text.Replace("'", "''") + "',uqc_code='" + code + "' where id='" + ViewState["id"].ToString() + "'; select * from unit_master where  firm ='" + My.firm_id() + "';";

            DataTable dt = My.dataTable(update_qry);
            Alertme("Unit Updated Successfully", "warning");
            My.send_data_to_user_log_history(Session["name"].ToString() + " Update Unit details : " + name, Session["Admin"].ToString());
            bind_grd_view();
        }


        protected void lnkEdit_Click(object sender, EventArgs e)
        {

            try
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    GridViewRow row = (GridViewRow)lnk.NamingContainer;
                    string id = ((Label)row.FindControl("lbl_Id")).Text;
                    string Unit = ((Label)row.FindControl("lbl_Unit")).Text;
                    string date = ((Label)row.FindControl("lbl_date")).Text;
                    string uqc_code = ((Label)row.FindControl("lbl_uqc_code")).Text;

                    ViewState["id"] = id;
                    Txt_Unit_Name.Text = Unit;
                    ddl_uqc.SelectedValue = uqc_code;
                    Btn_Add.Visible = false;
                    Btn_Update.Visible = true;
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }

            }
            catch (Exception ex)
            {
                Alertme("Please try again..", "warning");
                My.submitexception(ex.ToString());
            }
        }

        //protected void lnkDel_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        LinkButton lnk = (LinkButton)sender;
        //        GridViewRow row = (GridViewRow)lnk.NamingContainer;
        //        Label lbl_Unit_id = (Label)row.FindControl("lbl_Unit_id");
        //        Label lbl_Unit_name = (Label)row.FindControl("lbl_Unit_name");
        //        if (is_true(lbl_Unit_id.Text))
        //        {

        //            delete_data(lbl_Unit_id.Text);
        //        }
        //        else
        //        {
        //            Alertme("You can't delete this Unit Name. because this unit is use in item master.", "warning");
        //            return;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //    }




        //}
        //private void delete_data(string unit_id)
        //{
        //    SqlCommand cmd;
        //    string query = "delete from  HMS_Invetory_Unit_Master where Unit_id = @Unit_id ";
        //    cmd = new SqlCommand(query);
        //    cmd.Parameters.AddWithValue("@Unit_id", unit_id);

        //    if (My.InsertUpdateData(cmd))
        //    {
        //        Alertme("Unit Name has been delete Successfully.", "success");
        //        bind_all_data();
        //    }


        //}


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
                        GrdView_Create_Unit.RenderControl(hw);
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