using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace school_web.Library_Admin
{
    public partial class Set_Fine : System.Web.UI.Page
    {
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
                    ViewState["Userid"] = Session["Admin"].ToString();
                    ViewState["firm_id"] = My.get_firm_id();
                    ViewState["Branch_id"] = mycode.get_branch_id(ViewState["Userid"].ToString());

                    Bind_fins();
                    find_firm_details();
                    // BindDetails();
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_Session_Master");
            }
            find_firm_details();
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
      
        private void Bind_fins()
        {
            DataTable dt = mycode.FillData("Select * from lib_fine_details order by Fine_id asc");
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are fine not exist", "warning");
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
            btn_Submit.Text = "Add";
            btn_cancel.Visible = false;
            txt_ffu.Text = "";
            txt_fft.Text = "";
            txt_ffs.Text = "";
            find_firm_details();

        }
        protected void btn_Submit_Click1(object sender, EventArgs e)
        {
            SqlCommand cmd;

            if (btn_Submit.Text == "Add")
            {
                string fineforstudent = txt_ffu.Text;
                string fineforteacher = txt_fft.Text;
                string fineforstaff = txt_ffs.Text;
                DataTable dt = mycode.FillData("Select * from lib_fine_details  where fine_for_stuent='" + fineforstudent + "' and Branch_id = '" + ViewState["Branch_id"].ToString() + "'");
                if (dt.Rows.Count == 0)
                {
                    string createsessionid = cretesessionid();
                    string query = "INSERT INTO lib_fine_details  (fine_for_stuent,fine_for_teacher,fine_for_staff,Fine_id,User_id,Date,idate,Time) values (@ffu,@fft,@ffs,@Fine_id,@User_id,@Date,@idate,@Time)";
                    cmd = new SqlCommand(query);

                    cmd.Parameters.AddWithValue("@Fine_id", createsessionid);
                    cmd.Parameters.AddWithValue("@ffu", fineforstudent);
                    cmd.Parameters.AddWithValue("@fft", fineforteacher);
                    cmd.Parameters.AddWithValue("@ffs", fineforstaff);
                    cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@Date", mycode.date());
                    cmd.Parameters.AddWithValue("@idate", mycode.idate());
                    cmd.Parameters.AddWithValue("@Time", mycode.time());
                    if (My.InsertUpdateData(cmd))
                    {
                        Alertme("Fine has been imposed Successfully.", "success");
                        btn_Submit.Text = "Add";
                        txt_ffu.Text = "";
                        txt_fft.Text = "";
                        txt_ffs.Text = "";
                        find_firm_details();
                    }

                }
                
            }
            else
            {


                string fineforstudent = txt_ffu.Text;
                string fineforteacher = txt_fft.Text;
                string fineforstaff = txt_ffs.Text;
                DataTable dt = mycode.FillData("Select * from lib_fine_details  where fine_for_stuent='" + fineforstudent + "' and Branch_id = '" + ViewState["Branch_id"].ToString() + "' and  Fine_id!=" + HdID.Value + "  ");
                if (dt.Rows.Count == 0)
                {
                    string query = "Update  lib_fine_details set fine_for_stuent=@ffu,fine_for_teacher=@fft,fine_for_staff=@ffs,Fine_id=@Fine_id,User_id=@User_id,Date=@Date,idate=@idate,Time=@Time where Fine_id = @Fine_id";
                    cmd = new SqlCommand(query);

                    cmd.Parameters.AddWithValue("@Fine_id", HdID.Value);
                    cmd.Parameters.AddWithValue("@ffu", fineforstudent);
                    cmd.Parameters.AddWithValue("@fft", fineforteacher);
                    cmd.Parameters.AddWithValue("@ffs", fineforstaff);
                    cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@Date", mycode.date());
                    cmd.Parameters.AddWithValue("@idate", mycode.idate());
                    cmd.Parameters.AddWithValue("@Time", mycode.time());
                    if (My.InsertUpdateData(cmd))
                    {
                        Alertme("Location has been update Successfully.", "success");
                        btn_Submit.Text = "Add";
                        btn_cancel.Visible = false;
                        txt_ffu.Text = "";
                        txt_fft.Text = "";
                        txt_ffs.Text = "";
                        find_firm_details();

                    }

                }
                


            }

            Bind_fins();
        }
        private string cretesessionid()
        {
            bool duplicate = false;
            string Fine_id = mycode.auto_serial("Fine_id");
            while (!duplicate)
            {
                DataTable cdt = mycode.FillData("  select Fine_id from lib_fine_details where Fine_id='" + Fine_id + "' and Branch_id = '" + ViewState["Branch_id"].ToString() + "' ");
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    duplicate = true;
                }
                else
                {
                    duplicate = false;
                    Fine_id = mycode.auto_serial("Fine_id");
                }
            }
            return Fine_id;



        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            SqlCommand cmd;
            LinkButton lnk = (LinkButton)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            Label lbl_fineid = (Label)row.FindControl("lbl_fineid");
            Label lbl_ffu = (Label)row.FindControl("lbl_ffu");
            Label lbl_fft = (Label)row.FindControl("lbl_fft");
            Label lbl_ffs = (Label)row.FindControl("lbl_ffs");
            if (is_true(lbl_fineid.Text))
            {

                string query = "delete from  lib_fine_details where Fine_id=@Fine_id";
                cmd = new SqlCommand(query);

                cmd.Parameters.AddWithValue("@session_id", lbl_fineid.Text);

                if (My.InsertUpdateData(cmd))
                {
                    Alertme("Fine has been delete Successfully.", "success");
                    find_firm_details();
                }
            }
            else
            {
                Alertme("You can't delete this Fine", "warning");
                return;
            }


        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_fineid = (Label)row.FindControl("lbl_fineid");
                Label lbl_ffu = (Label)row.FindControl("lbl_ffu");
                Label lbl_fft = (Label)row.FindControl("lbl_fft");
                Label lbl_ffs = (Label)row.FindControl("lbl_ffs");
                HdID.Value = lbl_fineid.Text;

                if (is_true(lbl_fineid.Text))
                {
                    txt_ffu.Text = lbl_ffu.Text.Split('-')[0];
                    txt_fft.Text = lbl_fft.Text.Split('-')[1];
                    txt_ffs.Text = lbl_ffs.Text.Split('-')[2];


                    btn_cancel.Visible = true;
                    btn_Submit.Text = "Update";
                }
                else
                {
                    Alertme("You can't edit this Fine", "warning");
                    return;
                }



            }
            catch
            {

            }
        }
        private bool is_true(string Location)
        {
            if (mycode.FillData("select fine_for_stuent from lib_fine_details ").Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private void find_firm_details()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details where Branch_id = '" + ViewState["Branch_id"].ToString() + "'");
            if (dt.Rows.Count == 0)
            {




            }
            else
            {
                imglogo.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_emaiid.Text = dt.Rows[0]["email"].ToString();
                lbl_website.Text = dt.Rows[0]["website"].ToString();
                lbl_contact_details.Text = dt.Rows[0]["contact_no"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();
            }
        }
        protected void Btnexcel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=Export.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                    GrdView.RenderControl(hw);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
            catch
            {
            }

        }


    }
}