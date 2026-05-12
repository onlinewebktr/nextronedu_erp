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
    public partial class vendor_master : System.Web.UI.Page
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

        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ViewState["Userid"] = Session["Admin"].ToString();
                    ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                    mycode.bind_ddl(ddl_state, "select State from StateList order by State asc");
                    mycode.bind_all_ddl_with_id(ddl_vendor_Type, "select Vendor_type,Vendor_type_id from Vendor_type_master order by Vendor_type asc");
                    string pagename_current = "vendor-type-master.aspx";
                    Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                    ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                    ViewState["Is_delete"] = (String)dc1["Is_delete"];
                    ViewState["Is_Download"] = (String)dc1["Is_Download"];
                    ViewState["Is_Print"] = (String)dc1["Is_Print"];
                    ViewState["Is_add"] = (String)dc1["Is_add"];


                    if (Request.QueryString["edtsiD"] != null)
                    {
                        btn_Submit.Text = "Update";
                        btn_cancel.Visible = true;
                        ltUsertop.Text = "Edit Vendor";
                        hd_id.Value = Request.QueryString["edtsiD"].ToString();
                        fetch_data_for_edit();
                    }
                    else
                    {
                        btn_Submit.Text = "Add";
                        btn_cancel.Visible = false;
                        ltUsertop.Text = "Add Vendor";
                    }
                }
            }
            catch (Exception exc)
            {
            }
        }

        private void fetch_data_for_edit()
        {
            DataTable dt = mycode.FillData("select * from Vendor_master where Id=" + hd_id.Value + "");
            if (dt.Rows.Count > 0)
            {
                txt_company_name.Text = dt.Rows[0]["Company_name"].ToString();
                txt_contact_person_name.Text = dt.Rows[0]["Person_name"].ToString();
                txt_mobile.Text = dt.Rows[0]["Mobile_no"].ToString();
                txt_address.Text = dt.Rows[0]["Address"].ToString();
                txt_pincode.Text = dt.Rows[0]["Pincode"].ToString();
                ddl_state.Text = dt.Rows[0]["State"].ToString();
                txt_district.Text = dt.Rows[0]["District"].ToString();
                ddl_vendor_Type.SelectedValue = dt.Rows[0]["Type_of_vendor"].ToString();
                ddl_is_gst.Text = dt.Rows[0]["Is_gst_no"].ToString();
                txt_gst_no.Text = dt.Rows[0]["Gst_no"].ToString();
                ddl_is_pan_no.Text = dt.Rows[0]["Is_pan_no"].ToString();
                txt_pan_no.Text = dt.Rows[0]["Pan_no"].ToString();
            }
        }



        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_company_name.Text == "")
                {
                    Alertme("Please enter company name.", "warning");
                    txt_company_name.Focus();
                    return;
                }
                if (txt_contact_person_name.Text == "")
                {
                    Alertme("Please enter contact person name.", "warning");
                    txt_contact_person_name.Focus();
                    return;
                }
                if (txt_mobile.Text == "")
                {
                    Alertme("Please enter mobile no.", "warning");
                    txt_mobile.Focus();
                    return;
                }
                if (txt_address.Text == "")
                {
                    Alertme("Please enter address.", "warning");
                    txt_address.Focus();
                    return;
                }
                if (txt_pincode.Text == "")
                {
                    Alertme("Please enter pincode.", "warning");
                    txt_pincode.Focus();
                    return;
                }
                if (txt_district.Text == "")
                {
                    Alertme("Please enter distric.", "warning");
                    txt_district.Focus();
                    return;
                }
                if (ddl_state.Text == "")
                {
                    Alertme("Please select state.", "warning");
                    ddl_state.Focus();
                    return;
                }
                if (ddl_vendor_Type.SelectedItem.Text == "Select")
                {
                    Alertme("Please select vendor type.", "warning");
                    ddl_vendor_Type.Focus();
                    return;
                }
                if (ddl_is_gst.SelectedItem.Text == "Yes")
                {
                    if (txt_gst_no.Text == "")
                    {
                        Alertme("Please enter GST no.", "warning");
                        txt_gst_no.Focus();
                        return;
                    }
                }

                if (ddl_is_pan_no.SelectedItem.Text == "Yes")
                {
                    if (txt_pan_no.Text == "")
                    {
                        Alertme("Please enter PAN no.", "warning");
                        txt_pan_no.Focus();
                        return;
                    }
                }


                if (btn_Submit.Text == "Add")
                {
                    if (ViewState["Is_add"].ToString() == "1")
                    {
                        save_vendor_master();
                    }
                    else
                    {
                        Alertme(My.get_restricted_message(), "warning");
                    }
                }
                else
                {
                    if (ViewState["Is_add"].ToString() == "1")
                    {
                        update_vendor_details();
                    }
                    else
                    {
                        Alertme(My.get_restricted_message(), "warning");

                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void update_vendor_details()
        {
            if (mycode.IsUserExist("select Id from Vendor_master where Mobile_no='" + txt_mobile.Text + "' and Id!= '" + hd_id.Value + "'"))
            {
                SqlCommand cmd;
                string query = "Update Vendor_master set Company_name=@Company_name,Person_name=@Person_name,Mobile_no=@Mobile_no,Address=@Address,Pincode=@Pincode,State=@State,District=@District,Type_of_vendor=@Type_of_vendor,Is_gst_no=@Is_gst_no,Gst_no=@Gst_no,Is_pan_no=@Is_pan_no,Pan_no=@Pan_no,Updated_by=@Updated_by,Updated_date=@Updated_date,Updated_idate=@Updated_idate where Id = '" + hd_id.Value + "'";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Company_name", txt_company_name.Text);
                cmd.Parameters.AddWithValue("@Person_name", txt_contact_person_name.Text);
                cmd.Parameters.AddWithValue("@Mobile_no", txt_mobile.Text);
                cmd.Parameters.AddWithValue("@Address", txt_address.Text);
                cmd.Parameters.AddWithValue("@Pincode", txt_pincode.Text);
                cmd.Parameters.AddWithValue("@State", ddl_state.Text);
                cmd.Parameters.AddWithValue("@District", txt_district.Text);
                cmd.Parameters.AddWithValue("@Type_of_vendor", ddl_vendor_Type.SelectedValue);
                cmd.Parameters.AddWithValue("@Is_gst_no", ddl_is_gst.Text);
                if (ddl_is_gst.Text == "Yes")
                {
                    cmd.Parameters.AddWithValue("@Gst_no", txt_gst_no.Text);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Gst_no", "NA");
                }
                cmd.Parameters.AddWithValue("@Is_pan_no", ddl_is_pan_no.Text);
                if (ddl_is_pan_no.Text == "Yes")
                {
                    cmd.Parameters.AddWithValue("@Pan_no", txt_pan_no.Text);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Pan_no", "NA");
                }

                cmd.Parameters.AddWithValue("@Updated_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Updated_date", mycode.date());
                cmd.Parameters.AddWithValue("@Updated_idate", mycode.idate());
                if (My.InsertUpdateData(cmd))
                {
                    empty_form();
                    Session["successMsgs"] = "Vendor details has been updated successfully";
                    Response.Redirect("vendor-list.aspx", false);
                }
            }
            else
            {
                Alertme("Vendor already added with this mobile no.", "warning");
            }
        }


        string Item_id;
        private void save_vendor_master()
        {
            if (mycode.IsUserExist("select Id from Vendor_master where Mobile_no='" + txt_mobile.Text + "'"))
            {
                create_sl_no();
                SqlCommand cmd;
                string query = "INSERT INTO Vendor_master (Vendor_id,Company_name,Person_name,Mobile_no,Address,Pincode,State,District,Type_of_vendor,Is_gst_no,Gst_no,Is_pan_no,Pan_no,Created_by,Created_date,Created_idate) values (@Vendor_id,@Company_name,@Person_name,@Mobile_no,@Address,@Pincode,@State,@District,@Type_of_vendor,@Is_gst_no,@Gst_no,@Is_pan_no,@Pan_no,@Created_by,@Created_date,@Created_idate)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Vendor_id", Item_id);
                cmd.Parameters.AddWithValue("@Company_name", txt_company_name.Text);
                cmd.Parameters.AddWithValue("@Person_name", txt_contact_person_name.Text);
                cmd.Parameters.AddWithValue("@Mobile_no", txt_mobile.Text);
                cmd.Parameters.AddWithValue("@Address", txt_address.Text);
                cmd.Parameters.AddWithValue("@Pincode", txt_pincode.Text);
                cmd.Parameters.AddWithValue("@State", ddl_state.Text);
                cmd.Parameters.AddWithValue("@District", txt_district.Text);
                cmd.Parameters.AddWithValue("@Type_of_vendor", ddl_vendor_Type.SelectedValue);
                cmd.Parameters.AddWithValue("@Is_gst_no", ddl_is_gst.Text);
                if (ddl_is_gst.Text == "Yes")
                {
                    cmd.Parameters.AddWithValue("@Gst_no", txt_gst_no.Text);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Gst_no", "NA");
                }
                cmd.Parameters.AddWithValue("@Is_pan_no", ddl_is_pan_no.Text);
                if (ddl_is_pan_no.Text == "Yes")
                {
                    cmd.Parameters.AddWithValue("@Pan_no", txt_pan_no.Text);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Pan_no", "NA");
                }
                cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
                if (My.InsertUpdateData(cmd))
                {
                    Alertme("Vendor master has been created successfully.", "success");
                    empty_form();
                }
            }
            else
            {
                Alertme("Vendor already added with this mobile no.", "warning");
            }
        }


        private void empty_form()
        {
            txt_company_name.Text = "";
            txt_contact_person_name.Text = "";
            txt_mobile.Text = "";
            txt_address.Text = "";
            txt_pincode.Text = "";
            txt_district.Text = "";
            txt_gst_no.Text = "";
            txt_pan_no.Text = "";
            btn_cancel.Visible = false;
            btn_Submit.Text = "Add";
        }

        private void create_sl_no()
        {
            bool duplicate = true;
            string Items_id = My.auto_serialS("Global_sl_id");

            if (Items_id.Length == 1)
            {
                Item_id = "000" + Items_id;
            }
            if (Items_id.Length == 2)
            {
                Item_id = "00" + Items_id;
            }
            if (Items_id.Length == 3)
            {
                Item_id = "0" + Items_id;
            }
            while (duplicate)
            {
                DataTable cdt = My.dataTable(" select Vendor_id from Vendor_master where Vendor_id='" + Item_id + "'");
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    duplicate = false;
                }
                else
                {
                    string Item_ids = My.auto_serialS("Global_sl_id");

                    if (Items_id.Length == 1)
                    {
                        Item_id = "000" + Items_id;
                    }
                    if (Items_id.Length == 2)
                    {
                        Item_id = "00" + Items_id;
                    }
                    if (Items_id.Length == 3)
                    {
                        Item_id = "0" + Items_id;
                    }
                }
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("vendor-list.aspx", false);
        }
    }
}