using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;

namespace school_web.Admin
{
    public partial class Hostel_Damage_Item_Mapping_With_Student : System.Web.UI.Page
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
                        ViewState["session"] = My.get_session();
                        ViewState["session_id"] = My.get_session_id();
                        string pagename_current = "Hostel_Master.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        mycode.bind_all_ddl_with_id(ddl_item_name, "Select Item_name,Item_id from Hostel_Damage_Item_master order  by Item_name  ");


                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Hostel_Damage_master");
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


        protected void ddl_item_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_item_name.SelectedItem.Text == "Select")
            {
                Alertme("Please select item name", "warning");
            }
            else
            {
                txt_amount.Text = "0.00";
                DataTable dt = mycode.FillData("select * from Hostel_Damage_Item_master where Item_id=" + ddl_item_name.SelectedValue + "");
                if (dt.Rows.Count == 0)
                {

                }
                else
                {
                    double amt = My.toDouble(dt.Rows[0]["Price"].ToString());
                    txt_amount.Text = amt.ToString("0.00");
                }
            }
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            if (ddl_item_name.SelectedItem.Text == "Select")
            {
                Alertme("Please select item name", "warning");
            }
            else if (txt_amount.Text == "")
            {
                Alertme("Please select item name", "warning");
            }
            else if (txt_admission_no.Text == "")
            {
                Alertme("Please enter payable by", "warning");
            }
            else if (txt_remarks.Text == "")
            {
                Alertme("Please enter remarks", "warning");
            }
            else
            {
                string confirmValue = string.Empty;
                confirmValue = Request.Form["confirm_value"];
                if (confirmValue == "Yes")
                {
                    if (ViewState["Is_add"].ToString() == "1")
                    {
                        save_data();
                    }
                    else if (ViewState["Is_Edit"].ToString() == "1")
                    {
                        save_data();
                    }
                    else
                    {
                        Alertme(My.get_restricted_message(), "warning");
                    }
                }
                else
                {
                    Alertme("Sorry you have clicked no", "warning");
                }
            }
        }

        private void save_data()
        {

            ViewState["session"] = My.get_session();
            ViewState["session_id"] = My.get_session_id();

            //string query = "select admissionserialnumber,Hostel_assignD_id from admission_registor where admissionserialnumber='" + txt_admission_no.Text + "' and Session_id='" + ViewState["session_id"].ToString() + "' and hosteltaken='Yes' ";

            string query = "select top 1 Admission_no as admissionserialnumber,Hostel_assign_id as Hostel_assignD_id from Hostel_assign_master where Admission_no='" + txt_admission_no.Text + "' and Session_id='" + ViewState["session_id"].ToString() + "' and Status='1' order by id desc ";

            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count != 0)
            {
                ViewState["Hostel_assignD_id"] = dt.Rows[0]["Hostel_assignD_id"].ToString();
                string monthname = My.get_month_name_no_fee_taken_hostel(txt_admission_no.Text, ViewState["session"].ToString());//fetch_month_name_no_any_moth_fee_taken
                string month_id = mycode.get_sttratingmonthid(monthname);


                if (mycode.IsUserExist("select Damage_goods_item_id from Hostel_student_damage_Item_maping where Assign_id='" + ViewState["Hostel_assignD_id"].ToString() + "' and Damage_goods_item_id='" + ddl_item_name.SelectedValue + "' and Admission_no='" + txt_admission_no.Text + "'"))
                {
                    SqlCommand cmd;
                    query = "INSERT INTO Hostel_student_damage_Item_maping (Assign_id,For_motn_name,For_month_id,Damage_goods_item_id,Created_by,Created_date,Created_idate,Admission_no,Amount,Remarks) values (@Assign_id,@For_motn_name,@For_month_id,@Damage_goods_item_id,@Created_by,@Created_date,@Created_idate,@Admission_no,@Amount,@Remarks);INSERT INTO Hostel_student_damage_item_maping_history (Assign_id,For_motn_name,For_month_id,Damage_goods_item_id,Remarks,Created_by,Created_date,Created_idate,Time,Admission_no,Amount) values (@Assign_id,@For_motn_name,@For_month_id,@Damage_goods_item_id,@Remarks,@Created_by,@Created_date,@Created_idate,@Time,@Admission_no,@Amount)";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Assign_id", ViewState["Hostel_assignD_id"].ToString());
                    cmd.Parameters.AddWithValue("@For_motn_name", monthname);
                    cmd.Parameters.AddWithValue("@For_month_id", month_id);
                    cmd.Parameters.AddWithValue("@Damage_goods_item_id", ddl_item_name.SelectedValue);
                    cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                    cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
                    cmd.Parameters.AddWithValue("@Remarks",txt_remarks.Text);
                    cmd.Parameters.AddWithValue("@Admission_no", txt_admission_no.Text);
                    cmd.Parameters.AddWithValue("@Time", mycode.time());
                    cmd.Parameters.AddWithValue("@Amount", txt_amount.Text);
                    
                    if (My.InsertUpdateData(cmd))
                    {
                        Alertme("Damaged item has been successfully mapped with the student", "success");
                        ddl_item_name.SelectedValue = "0";
                        txt_amount.Text = "";
                        txt_admission_no.Text = "";
                        txt_remarks.Text = "";
                         
                    }
                }
                else
                {

                }
            }
            else
            {
                Alertme("Please first assine hostel", "warning");
            }
        }
    }
}