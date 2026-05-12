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
    public partial class installment_master : System.Web.UI.Page
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
                    if (!IsPostBack)
                    {
                        ViewState["MonthId"] = "0,";
                        bind_month(); Bind_installmet();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "InstallmentMaster");
            }
        }

        private void Bind_installmet()
        {
            DataTable dt = mycode.FillData("Select * from Fee_installment_master");
            if (dt.Rows.Count == 0)
            {
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }


        private void bind_month()
        {
            DataTable dt = mycode.FillData("select * from Month_Index order by Position asc");
            if (dt.Rows.Count == 0)
            {
                rp_month.DataSource = null;
                rp_month.DataBind();
            }
            else
            {
                rp_month.DataSource = dt;
                rp_month.DataBind();
            }
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_ins_name.Text == "")
                {
                    Alertme("Please enter the name of installment.", "warning");
                    txt_ins_name.Focus();
                }
                else
                {
                    bool isMonthChecked = false;
                    if (btn_Submit.Text == "Update")
                    {
                        int mgrowcount = rp_month.Items.Count;
                        for (int ixi = 0; ixi < mgrowcount; ixi++)
                        {
                            CheckBox chkM = (CheckBox)rp_month.Items[ixi].FindControl("chk_month_name");
                            if (chkM.Checked == true)
                            {
                                isMonthChecked = true;
                                Label lbl_month_name = (Label)rp_month.Items[ixi].FindControl("lbl_month_name");
                                DataTable dt = My.dataTable("select Intstallment_name from Fee_installment_master where Month_name like '%" + lbl_month_name.Text + "%' and Id!=" + ViewState["rowId"].ToString() + "");
                                if (dt.Rows.Count > 0)
                                {
                                    Alertme("Your selected month is already mapped with " + dt.Rows[0]["Intstallment_name"].ToString() + ".", "warning");
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        int mgrowcount = rp_month.Items.Count;
                        for (int ixi = 0; ixi < mgrowcount; ixi++)
                        {
                            CheckBox chkM = (CheckBox)rp_month.Items[ixi].FindControl("chk_month_name");
                            if (chkM.Checked == true)
                            {
                                isMonthChecked = true;

                                Label lbl_month_name = (Label)rp_month.Items[ixi].FindControl("lbl_month_name");
                                DataTable dt = My.dataTable("select Intstallment_name from Fee_installment_master where Month_name like '%" + lbl_month_name.Text + "%'");
                                if (dt.Rows.Count > 0)
                                {
                                    Alertme("Your selected month is already mapped with " + dt.Rows[0]["Intstallment_name"].ToString() + ".", "warning");
                                    return;
                                }
                            }
                        }
                    }

                    if (isMonthChecked == true)
                    {
                        save_installment_month();
                    }
                    else
                    {
                        Alertme("Select the month you want to assign for this installment.", "warning");
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void save_installment_month()
        {
            if (btn_Submit.Text == "Update")
            {
                My.exeSql("delete from Fee_installment_master where Id='" + ViewState["rowId"].ToString() + "'");
            }
            string month_name = ""; string month_id = ""; string month_posotion = "";
            int mgrowcount = rp_month.Items.Count;
            for (int ixi = 0; ixi < mgrowcount; ixi++)
            {
                CheckBox chkM = (CheckBox)rp_month.Items[ixi].FindControl("chk_month_name");
                if (chkM.Checked == true)
                {
                    Label lbl_month_id = (Label)rp_month.Items[ixi].FindControl("lbl_month_id");
                    Label lbl_month_name = (Label)rp_month.Items[ixi].FindControl("lbl_month_name");
                    Label lbl_position = (Label)rp_month.Items[ixi].FindControl("lbl_position");

                    month_name = month_name + lbl_month_name.Text + ",";
                    month_id = month_id + lbl_month_id.Text + ",";
                    month_posotion = month_posotion + lbl_position.Text + ",";
                }
            }


            month_name = month_name.Remove(month_name.Length - 1);
            month_id = month_id.Remove(month_id.Length - 1);
            month_posotion = month_posotion.Remove(month_posotion.Length - 1);

            SqlCommand cmd;
            string query = "INSERT INTO Fee_installment_master (Intstallment_name,Month_name,Month_id,Month_position_no,Updated_by,Updated_date,Updated_time) values (@Intstallment_name,@Month_name,@Month_id,@Month_position_no,@Updated_by,@Updated_date,@Updated_time)";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Intstallment_name", txt_ins_name.Text);
            cmd.Parameters.AddWithValue("@Month_name", month_name);
            cmd.Parameters.AddWithValue("@Month_id", month_id);
            cmd.Parameters.AddWithValue("@Month_position_no", month_posotion);
            cmd.Parameters.AddWithValue("@Updated_by", Session["Admin"].ToString());
            cmd.Parameters.AddWithValue("@Updated_date", mycode.date());
            cmd.Parameters.AddWithValue("@Updated_time", mycode.idate());
            if (My.InsertUpdateData(cmd))
            {
                txt_ins_name.Text = "";
                btn_cancel.Visible = false;
                btn_Submit.Text = "Add";
                ViewState["MonthId"] = "0,";
                bind_month();
                Bind_installmet();
                if (btn_Submit.Text == "Update")
                {
                    Alertme("Installment has been updated successfully", "success");
                }
                else
                {
                    Alertme("Installment has been added successfully", "success");
                }
            }
        }


        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                Label lbl_month_id = (Label)row.FindControl("lbl_month_id");
                Label lbl_month_position_no = (Label)row.FindControl("lbl_month_position_no");
                Label lbl_month_name = (Label)row.FindControl("lbl_month_name");
                Label lbl_intstallment_name = (Label)row.FindControl("lbl_intstallment_name");
                txt_ins_name.Text = lbl_intstallment_name.Text;
                ViewState["rowId"] = lbl_Id.Text;
                ViewState["MonthId"] = lbl_month_id.Text;
                bind_month();
                btn_cancel.Visible = true;
                btn_Submit.Text = "Update";
            }
            catch
            {

            }
        }


        protected void rp_month_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                try
                {
                    string[] stringSeparatorss = new string[] { "," };
                    string[] arr = ViewState["MonthId"].ToString().Split(stringSeparatorss, StringSplitOptions.None);
                    foreach (string value in arr)
                    {
                        if (((Label)e.Item.FindControl("lbl_month_id")).Text == value)
                        {
                            ((CheckBox)e.Item.FindControl("chk_month_name")).Checked = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            SqlCommand cmd;
            LinkButton lnk = (LinkButton)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            Label lbl_Id = (Label)row.FindControl("lbl_Id");

            string query = "delete from Fee_installment_master where Id=@Id";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Id", lbl_Id.Text);
            if (My.InsertUpdateData(cmd))
            {
                Alertme("Intstallment has been delete successfully.", "success");
                Bind_installmet();
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            txt_ins_name.Text = "";
            btn_cancel.Visible = false;
            btn_Submit.Text = "Add";
            ViewState["MonthId"] = "0,";
            bind_month();
        }
    }
}