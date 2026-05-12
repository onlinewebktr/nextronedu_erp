using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Payroll
{
    public partial class add_device_details : System.Web.UI.Page
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
                        txt_port.Text = "4370";
                        txt_machine_number.Text = "1";
                        bind_grd_view();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_Device");
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
            DataTable dt = mycode.FillData("select * from PRL_Device_Details");
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
            txt_device_name.Text = "";
            txt_device_ip.Text = "";
            txt_port.Text = "4370";
            txt_machine_number.Text = "1";
            btn_cancel.Visible = false;
            btn_Submit.Text = "Add";
        }

        protected void btn_Submit_Click1(object sender, EventArgs e)
        {
            if (txt_device_name.Text == "")
            {
                Alertme("Please Enter Device Name", "warning");
                txt_device_name.Focus();
                return;
            }
            if (txt_device_ip.Text == "")
            {
                Alertme("Please Enter Device Ip", "warning");
                txt_device_ip.Focus();
                return;
            }
            if (txt_port.Text == "")
            {
                Alertme("Please Enter Port", "warning");
                txt_port.Focus();
                return;
            }
            if (txt_machine_number.Text == "")
            {
                Alertme("Please Enter Machine Number", "warning");
                txt_machine_number.Focus();
                return;
            }


            if (btn_Submit.Text == "Add")
            {
                submit_details();
                empty_form();
                bind_grd_view();
                bind_grd_view();
            }
            else
            {
                update_update_details();
                empty_form();
                bind_grd_view();
            }
        }

        private void update_update_details()
        {
            SqlConnection conn = new SqlConnection(My.conn);
            SqlDataAdapter ad = new SqlDataAdapter("select * from PRL_Device_Details where Id='" + hd_id.Value + "'", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                dr[1] = txt_device_name.Text;
                dr[2] = txt_device_ip.Text;
                dr[3] = txt_port.Text;
                dr[4] = txt_machine_number.Text;
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
            Alertme("Device Details  Updated Successfully", "success");
        }

        private void submit_details()
        {
            SqlConnection conn = new SqlConnection(My.conn);
            SqlDataAdapter ad = new SqlDataAdapter("select * from PRL_Device_Details", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            DataRow dr = dt.NewRow();
            dr[1] = txt_device_name.Text;
            dr[2] = txt_device_ip.Text;
            dr[3] = txt_port.Text;
            dr[4] = txt_machine_number.Text;
            dt.Rows.Add(dr);
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
            Alertme("Device Details Add Successfully", "success");
        }

        private void empty_form()
        {
            txt_device_name.Text = "";
            txt_device_ip.Text = "";
            txt_port.Text = "4370";
            txt_machine_number.Text = "1";
            btn_cancel.Visible = false;
            btn_Submit.Text = "Add";
        }


        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Device_Name = (Label)row.FindControl("lbl_Device_Name");
                Label lbl_device_ip = (Label)row.FindControl("lbl_device_ip");
                Label lbl_port = (Label)row.FindControl("lbl_port");
                Label lbl_machine_number = (Label)row.FindControl("lbl_machine_number");

                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                hd_id.Value = lbl_Id.Text;

                txt_device_name.Text = lbl_Device_Name.Text;
                txt_device_ip.Text = lbl_device_ip.Text;
                txt_port.Text = "4370";
                txt_machine_number.Text = "1";

                btn_cancel.Visible = true;
                btn_Submit.Text = "Update";
            }
            catch
            {
            }
        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            Label lbl_Id = (Label)row.FindControl("lbl_Id");

            My.exeSql("delete from PRL_Device_Details where Id='" + lbl_Id.Text + "'");
            Alertme("Department has been deleted successfully", "success");
            bind_grd_view(); 
        }

         
    }
}