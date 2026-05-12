using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
 
using System.Data;
using school_web.AppCode;
namespace school_web.Admin
{
    public partial class Add_Branch : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
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
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();

                        if (Request.QueryString["id"] != null)
                        {

                            hd_id.Value = Request.QueryString["id"];
                            fetch_req_detsils();
                            ltUsertop.Text = "Update Branch Details";
                            btn_cancele.Visible = true;
                            btn_Submit.Text = "Update";
                        }
                        else
                        {
                            ltUsertop.Text = "Add Branch Details";
                            btn_cancele.Visible = false;
                        }



                        // BindDetails();
                    }
                }
                catch (Exception ex)
                {
                    My.submitException(ex, "Add_Branch");
                }
            }

        }

        private void fetch_req_detsils()
        {
            DataTable dt = mycode.FillData("Select * from Firm_Branch where Id='" + hd_id.Value + "'  ");
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                txt_Branch_name.Text = dt.Rows[0]["Branch_name"].ToString();
                txt_address1.Text = dt.Rows[0]["Branch_address"].ToString();
                txt_contactno.Text = dt.Rows[0]["Mobile_no"].ToString();
                txt_emailid.Text = dt.Rows[0]["Email_id"].ToString();
                txt_BranchCode.Text = dt.Rows[0]["Branch_code"].ToString();
                btn_cancele.Visible = true;
            }

        }

        protected void btn_cancele_Click(object sender, EventArgs e)
        {
            btn_Submit.Text = "Add";
            btn_cancele.Visible = false;
            txt_Branch_name.Text = "";
            txt_address1.Text = "";
            txt_contactno.Text = "";
            txt_emailid.Text = "";
            txt_BranchCode.Text = "";
            Response.Redirect("Branch_List.aspx", false);

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
        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            SqlCommand cmd;
            try
            {
                if (txt_Branch_name.Text == "")
                {
                    txt_Branch_name.Focus();
                    Alertme("Please enter branch name.", "warning");
                    return;
                }
                else if (txt_address1.Text == "")
                {
                    txt_address1.Focus();
                    Alertme("Please enter adress.", "warning");
                    return;

                }
                else if (txt_contactno.Text == "")
                {
                    txt_contactno.Focus();
                    Alertme("Please enter contact no.", "warning");
                    return;
                }
                else if (txt_contactno.Text == "")
                {
                    txt_contactno.Focus();
                    Alertme("Please enter contact no.", "warning");
                    return;
                }

                else if (txt_emailid.Text == "")
                {
                    txt_emailid.Focus();
                    Alertme("Please enter email id", "warning");
                    return;
                }
                else
                {

                    if (btn_Submit.Text == "Add")
                    {
                        DataTable dt = mycode.FillData("Select Branch_name from Firm_Branch where Branch_name='" + txt_Branch_name.Text + "'  ");
                        if (dt.Rows.Count == 0)
                        {
                            DataTable dt1 = mycode.FillData("Select Branch_name from Firm_Branch where Branch_code='" + txt_BranchCode.Text + "'  ");
                            if (dt1.Rows.Count == 0)
                            {
                                string query = "INSERT INTO Firm_Branch (Firm_id,Branch_id,Branch_name,Branch_address,Mobile_no,Email_id,Branch_code,User_id,Date,idate,Time) values (@Firm_id,@Branch_id,@Branch_name,@Branch_address,@Mobile_no,@Email_id,@Branch_code,@User_id,@Date,@idate,@Time)";
                                cmd = new SqlCommand(query);
                                cmd.Parameters.AddWithValue("@Firm_id", ViewState["firm_id"].ToString());
                                cmd.Parameters.AddWithValue("@Branch_id", mycode.auto_serial("Branch_id"));
                                cmd.Parameters.AddWithValue("@Branch_name", txt_Branch_name.Text.Trim());
                                cmd.Parameters.AddWithValue("@Branch_address", txt_address1.Text);
                                cmd.Parameters.AddWithValue("@Mobile_no", txt_contactno.Text);
                                cmd.Parameters.AddWithValue("@Email_id", txt_emailid.Text);
                                cmd.Parameters.AddWithValue("@Branch_code", txt_BranchCode.Text);
                                cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                                cmd.Parameters.AddWithValue("@Date", mycode.date());
                                cmd.Parameters.AddWithValue("@idate", mycode.idate());
                                cmd.Parameters.AddWithValue("@Time", mycode.time());
                                if (My.InsertUpdateData(cmd))
                                {
                                    Alertme("Branch details has been save Successfully.", "success");
                                    btn_Submit.Text = "Add";
                                    btn_cancele.Visible = false;
                                    txt_Branch_name.Text = "";
                                    txt_address1.Text = "";
                                    txt_contactno.Text = "";
                                    txt_emailid.Text = "";
                                    txt_BranchCode.Text = "";
                                }

                            }
                            else
                            {
                                txt_BranchCode.Focus();
                                Alertme("Branch code already exist", "warning");
                                return;
                            }
                        }
                        else
                        {
                            txt_Branch_name.Focus();
                            Alertme("Branch name already exist", "warning");
                            return;
                        }

                    }
                    else
                    {

                        DataTable dt = mycode.FillData("Select Branch_name from Firm_Branch where Branch_name='" + txt_Branch_name.Text + "' and Id!=" + hd_id.Value + "  ");
                        if (dt.Rows.Count == 0)
                        {
                            DataTable dt1 = mycode.FillData("Select Branch_name from Firm_Branch where Branch_code='" + txt_BranchCode.Text + "' and Id!=" + hd_id.Value + "   ");
                            if (dt1.Rows.Count == 0)
                            {
                                string query = "Update Firm_Branch set Branch_name=@Branch_name,Branch_address=@Branch_address,Mobile_no=@Mobile_no,Email_id=@Email_id,Branch_code=@Branch_code,User_id=@User_id,Date=@Date,idate=@idate,Time=@Time where Id = @Id";
                                cmd = new SqlCommand(query);

                                //cmd.Parameters.AddWithValue("@Branch_id", mycode.auto_serial("Branch_id"));
                                cmd.Parameters.AddWithValue("@Branch_name", txt_Branch_name.Text.Trim());
                                cmd.Parameters.AddWithValue("@Branch_address", txt_address1.Text);
                                cmd.Parameters.AddWithValue("@Mobile_no", txt_contactno.Text);
                                cmd.Parameters.AddWithValue("@Email_id", txt_emailid.Text);
                                cmd.Parameters.AddWithValue("@Branch_code", txt_BranchCode.Text);
                                cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                                cmd.Parameters.AddWithValue("@Date", mycode.date());
                                cmd.Parameters.AddWithValue("@idate", mycode.idate());
                                cmd.Parameters.AddWithValue("@Time", mycode.time());
                                cmd.Parameters.AddWithValue("@Id", hd_id.Value);
                                if (My.InsertUpdateData(cmd))
                                {
                                    Alertme("Branch details has been update Successfully.", "success");
                                    btn_Submit.Text = "Add";
                                    btn_cancele.Visible = false;
                                    txt_Branch_name.Text = "";
                                    txt_address1.Text = "";
                                    txt_contactno.Text = "";
                                    txt_emailid.Text = "";
                                    txt_BranchCode.Text = "";
                                }

                            }
                            else
                            {
                                txt_BranchCode.Focus();
                                Alertme("Branch code already exist", "warning");
                                return;
                            }
                        }
                        else
                        {
                            txt_Branch_name.Focus();
                            Alertme("Branch name already exist", "warning");
                            return;
                        }







                    }



                }

            }
            catch
            {
            }
        }
    }
}