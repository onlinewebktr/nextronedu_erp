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
    public partial class Create_Brand : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Admin"] == null)
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
                Session["name"]= Session["Admin"].ToString();
                if (!IsPostBack)
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

                    bind_all_data();

                }
            }
        }
        private void bind_all_data()
        {
            bind_grd_view("select * from  HMS_Invetory_Brand_Master ");
        }

        private void bind_grd_view(string qry)
        {
            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {
                //Alertme("Sorry there are no data exist", "warning");
                GrdView_Create_Brand.DataSource = null;
                GrdView_Create_Brand.DataBind();
            }
            else
            {
                GrdView_Create_Brand.DataSource = dt;
                GrdView_Create_Brand.DataBind();
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


        protected void Btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_add"].ToString() == "1")
                {
                    if (mycode.FillData("select * from HMS_Invetory_Brand_Master where Brand_name ='" + Txt_Brand_Name.Text + "' ").Rows.Count == 0)
                    {
                        save_data();
                    }
                    else
                    {
                        Alertme("Sorry you entered brand name is duplicate", "warning");
                    }
                }

                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());
            }

        }

        private void save_data()
        {


            string Brand_Id = My.auto_serialS("Brand_Id");
            SqlConnection cn = new SqlConnection(My.conn);
            SqlCommand cmd = new SqlCommand("sp_HMS_Invetory_Brand_Master", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@sp_status", "Insert");
            cmd.Parameters.AddWithValue("@Brand_Id", Brand_Id);
            cmd.Parameters.AddWithValue("@Brand_Name", Txt_Brand_Name.Text);
            cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
            cmd.Parameters.AddWithValue("@Created_Date", My.datetime_new());
            cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
            int rowsAffected = Store_procedure_code.executeNonQuery(cmd);
            if (rowsAffected > 0)
            {
                My.send_data_to_user_log_history(Session["name"].ToString() + " Add new brand : " + Txt_Brand_Name.Text, Session["Admin"].ToString());

                Txt_Brand_Name.Text = "";
                Alertme("Data Inserted Sucessfully", "success");

                bind_all_data();
            }

        }
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    GridViewRow row = (GridViewRow)lnk.NamingContainer;
                    Label lbl_Brand_id = (Label)row.FindControl("lbl_Brand_id");
                    Label lbl_Brand_name = (Label)row.FindControl("lbl_Brand_name");
                    HdID.Value = lbl_Brand_id.Text;
                    ViewState["Brand_name"] = lbl_Brand_name.Text;
                    Txt_Brand_Name.Text = lbl_Brand_name.Text.Split('-')[0];
                    Btn_Cancel.Visible = true;
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
                My.submitexception(ex.ToString());
            }

        }
        protected void Btn_Update_Click(object sender, EventArgs e)

        {
            try
            {
                if (mycode.FillData("select * from HMS_Invetory_Brand_Master where Brand_name ='" + Txt_Brand_Name.Text + "' and  Brand_Id!="+ HdID.Value + "").Rows.Count == 0)
                {
                    Update_Data();
                }
                else
                {
                    Alertme("Sorry you entered brand name is duplicate", "warning");
                }
            }
            catch (Exception ex)
            {
                My.submitexception(ex.ToString());
            }


        }
        private void Update_Data()
        {
            SqlConnection cn = new SqlConnection(My.conn);
            SqlCommand cmd = new SqlCommand("sp_HMS_Invetory_Brand_Master", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@sp_status", "Update");
            cmd.Parameters.AddWithValue("@Brand_Name", Txt_Brand_Name.Text);
            cmd.Parameters.AddWithValue("@Brand_Id", HdID.Value);
            int rowsAffected = Store_procedure_code.executeNonQuery(cmd);
            if (rowsAffected > 0)
            {
                My.send_data_to_user_log_history(Session["name"].ToString() + " Update brand : " + ViewState["Brand_name"].ToString() + " to " + Txt_Brand_Name.Text, Session["Admin"].ToString());

                Alertme("Record updated successfully", "success");
                Btn_Cancel.Visible = false;
                Btn_Add.Visible = true;
                Btn_Update.Visible = false;
                bind_all_data();
                Txt_Brand_Name.Text = "";
            }
        }

        protected void Btn_Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Create_Brand.aspx");
        }
        private bool is_true(string Brand_id)
        {
            if (mycode.FillData("select * from HMS_Invetory_item_Master where Brand_id ='" + Brand_id + "' ").Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }


        protected void lnkDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_delete"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    GridViewRow row = (GridViewRow)lnk.NamingContainer;
                    Label lbl_Brand_id = (Label)row.FindControl("lbl_Brand_id");
                    Label lbl_Brand_name = (Label)row.FindControl("lbl_Brand_name");
                    ViewState["Brand_name"] = lbl_Brand_name.Text;
                    if (is_true(lbl_Brand_id.Text))
                    {
                        delete_data(lbl_Brand_id.Text);
                    }
                    else
                    {
                        Alertme("You can't delete this Brand Name. because this brand is use in item master.", "warning");
                        return;
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

        private void delete_data(string Brand_id)
        {
            SqlCommand cmd;
            string query = "delete from  HMS_Invetory_Brand_Master where Brand_id = @Brand_id ";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Brand_id", Brand_id);
            if (My.InsertUpdateData(cmd))
            {
                My.send_data_to_user_log_history(Session["name"].ToString() + " Delete brand : " + ViewState["Brand_name"].ToString(), Session["Admin"].ToString());

                Alertme("Brand Name has been delete Successfully.", "success");
                bind_all_data();
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
                        GrdView_Create_Brand.RenderControl(hw);
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