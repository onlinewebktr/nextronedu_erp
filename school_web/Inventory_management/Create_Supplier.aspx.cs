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

namespace school_web.Inventory_management
{
    public partial class Create_Supplier : System.Web.UI.Page
    {

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

        My Mycode = new My();

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
                if (!IsPostBack)
                {
                    ViewState["isupdate"] = "0";
                    ViewState["Userid"] = Session["Admin"].ToString();
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


                    txt_Date.Text = Mycode.date();
                    ddl_state.SelectedValue = "10";
                    Mycode.bind_all_ddl_with_id(ddl_route, "Select Route_name,Route_id from HMS_Invetory_Route_Master order by Route_name");
                    bind_state();

                    try
                    {
                        if (Request.QueryString["partyid"] != null)
                        {
                            string partyid = Request.QueryString["partyid"];
                            ViewState["party_id"] = partyid;
                            ViewState["isupdate"] = "1";
                            Bind_data();
                        }
                    }
                    catch
                    {

                    }


                }
            }
        }

        private void Bind_data()
        {
            DataTable dt = My.dataTable("select * from party_details where party_id='" + ViewState["party_id"].ToString() + "' ;");
            if (dt.Rows.Count == 0)
            {


            }
            else
            {

                ViewState["id"] = dt.Rows[0]["id"].ToString();
                txt_suppliername.Text = dt.Rows[0]["party_name"].ToString();
                txt_address.Text = dt.Rows[0]["address"].ToString();
                txt_city.Text = dt.Rows[0]["city"].ToString();
                txt_mobile_no.Text = dt.Rows[0]["mobile"].ToString();
                ddl_state.SelectedValue = dt.Rows[0]["State_Code"].ToString(); ;
                HdID.Value = dt.Rows[0]["party_id"].ToString();

                txt_gstin.Text = dt.Rows[0]["gstin"].ToString();
                ddl_gstin_type.Text = dt.Rows[0]["Registration_Type"].ToString();

                txt_Care_of.Text = dt.Rows[0]["Care_of"].ToString();
                txt_pan_no.Text = dt.Rows[0]["pan_no"].ToString();
                txt_account_no.Text = dt.Rows[0]["Account_No"].ToString();
                txt_bank_name.Text = dt.Rows[0]["Bank_Name"].ToString();
                txt_ifsc_code.Text = dt.Rows[0]["IFSC_Code"].ToString();
                try
                {
                    ddl_route.SelectedValue = dt.Rows[0]["route_id"].ToString();
                }
                catch
                {

                }



                Btn_Cancel.Visible = true;
                Btn_Add.Visible = false;
                Btn_Update.Visible = true;
            }
        }

        private void bind_state()
        {
            DataTable dt = My.dataTable("select * from StateList");
            if (dt.Rows.Count > 0)
            {
                ddl_state.DataTextField = "State";
                ddl_state.DataValueField = "Code";
                ddl_state.DataSource = dt;
                ddl_state.DataBind();
                ddl_state.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        protected void Btn_Cancel_Click(object sender, EventArgs e)
        {
            if (ViewState["isupdate"].ToString() == "0")
            {
                Response.Redirect("Create_Supplier.aspx", false);
            }
            else
            {
                Response.Redirect("Supplie_Master.aspx", false);

            }
            empty_form();
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
                if (txt_suppliername.Text == "")
                {
                    Alertme("Please Enter party_name", "warning");
                    txt_suppliername.Focus();
                    return;
                }
                if (ddl_state.SelectedItem.ToString() == "Select")
                {
                    Alertme("Please Select State", "warning");
                    ddl_state.Focus();
                    return;
                }
                if (My.get_table_data("select Account_Name  from dbo.[Account_Ledger_Details] where firm='" + My.firm_id() + "' and Account_Name='" + txt_suppliername.Text.Trim() + "'") != "")
                {
                    Alertme("Ledger Already Created for " + txt_suppliername.Text + ".\n Please choose diffrent name", "warning");
                    txt_suppliername.Focus();
                    return;
                }
                try
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
                catch (Exception ex)
                {

                }

            }
            catch (Exception ex)
            {
                Alertme("Please try again..", "warning");
                My.submitexception(ex.ToString());
            }

        }

        private void submit_details()
        {
            string party_id = "sp" + My.auto_serialS("party_id");
            SqlConnection conn = new SqlConnection(My.conn);
            SqlDataAdapter ad = new SqlDataAdapter("select * from party_details where  party_id='" + party_id + "'", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                DataRow dr = dt.NewRow();
                dr[1] = txt_suppliername.Text;
                dr[2] = txt_address.Text;
                dr[3] = txt_city.Text;
                dr[4] = txt_mobile_no.Text;
                dr["gstin"] = txt_gstin.Text;
                dr[6] = party_id;
                dr[7] = Mycode.date();
                dr["firm"] = My.firm_id();
                dr[9] = ddl_gstin_type.Text;
                dr[10] = ddl_state.Text;
                dr[11] = ddl_state.SelectedValue.ToString();
                dr["type"] = "Supplier";
                dr["Care_of"] = txt_Care_of.Text;
                dr["pan_no"] = txt_pan_no.Text;
                dr["Account_No"] = txt_account_no.Text;
                dr["Bank_Name"] = txt_bank_name.Text;
                dr["IFSC_Code"] = txt_ifsc_code.Text;
                dr[12] = ddl_route.SelectedValue;
                dr[13] = ddl_route.SelectedItem.Text;
                //dr[14] = type;
                //dr[15] = txt_aadhar_number.Text;
                //dr[16] = txt_date_of_birth.Text;
                //dr[17] = txt_date_of_marriage.Text;
                //dr[18] = txt_party_nick_name.Text;

                //dr["idob"] = idate(txt_date_of_birth.Text);
                //dr["idom"] = idate(txt_date_of_marriage.Text);
                //dr["dl_no"] = txt_dl_no.Text;

                //dr["customer_type"] = ddl_customer_type.Text;
                //if (type == "Customer")
                //{
                //    My.save_Account_Ledger_Details(txt_party_name.Text, dr[6].ToString(), "26");
                //    Opeinig_balance_details(dr[6].ToString(), "26");
                //}
                //else
                //{

                My.save_Account_Ledger_Details(txt_suppliername.Text, party_id, "22");
                My.update_Ledger_Opening_Balance(txt_suppliername.Text, party_id, "22", ddl_type.Text, txt_opening_balance.Text, txt_Date.Text, My.get_session());
                //}

                dt.Rows.Add(dr);

                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(dt);
                Alertme("Supplier Details Created Successfully", "success");

                My.send_data_to_user_log_history(Session["name"].ToString() + " added a new Party with Party id : " + party_id + " Party name : " + txt_suppliername.Text, Session["Admin"].ToString());
                empty_form();
            }
            else
            {
                party_id = "sp" + My.auto_serialS("party_id");
                submit_details();
            }
        }


        #region update 

        protected void Btn_Update_Click(object sender, EventArgs e)
        {
            if (txt_suppliername.Text == "")
            {
                Alertme("Please Enter party_name", "warning");
                txt_suppliername.Focus();
                return;
            }
            if (ddl_state.SelectedItem.ToString() == "Select")
            {
                Alertme("Please Select State", "warning");
                ddl_state.Focus();
                return;
            }
            if (My.get_table_data("select Account_Name  from dbo.[Account_Ledger_Details] where firm='" + My.firm_id() + "' and Account_Name='" + txt_suppliername.Text.Trim() + "' and account_id!='" + ViewState["party_id"].ToString() + "'") != "")
            {

                Alertme("Ledger Already Created for " + txt_suppliername.Text + ".\n Please choose diffrent name", "warning");
                txt_suppliername.Focus();
                return;
            }
            try
            {
                update_update_details();
                if (ViewState["isupdate"].ToString() == "0")
                {
                    Response.Redirect("Create_Supplier.aspx", false);
                }
                else
                {
                    Response.Redirect("Supplie_Master.aspx", false);

                }
                empty_form();
            }
            catch (Exception ex)
            {
                Alertme(ex.Message, "warning");
            }

        }
        private void update_update_details()
        {

            SqlConnection conn = new SqlConnection(My.conn);
            SqlDataAdapter ad = new SqlDataAdapter("select * from party_details where  id='" + ViewState["id"].ToString() + "'", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                dr[1] = txt_suppliername.Text;
                dr[2] = txt_address.Text;
                dr[3] = txt_city.Text;
                dr[4] = txt_mobile_no.Text;
                dr[7] = Mycode.date();
                dr["gstin"] = txt_gstin.Text;
                dr[9] = ddl_gstin_type.Text;
                dr[10] = ddl_state.Text;
                dr[11] = ddl_state.SelectedValue.ToString();
                dr["Care_of"] = txt_Care_of.Text;
                dr["pan_no"] = txt_pan_no.Text;
                dr["Account_No"] = txt_account_no.Text;
                dr["Bank_Name"] = txt_bank_name.Text;
                dr["IFSC_Code"] = txt_ifsc_code.Text;
                dr[12] = ddl_route.SelectedValue;
                dr[13] = ddl_route.SelectedItem.Text;
                My.send_data_to_user_log_history(Session["name"].ToString() + " updated details of Party with Party id : " + ViewState["party_id"].ToString() + " Party name : " + txt_suppliername.Text, Session["Admin"].ToString());

            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
            Session["msg"] = "Supplier Details has been updated successfully";
            Alertme("Supplier Details has been updated successfully", "Success");

        }
        #endregion





    }
}