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
    public partial class month_wise_estimate_fee : System.Web.UI.Page
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
                        hd_find_status.Value = "0";
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();

                        mycode.bind_all_ddl_with_id(ddlsession, "Select  Session,session_id from session_details");
                        ddlsession.SelectedValue = My.get_session_id();
                        bind_class();
                        mycode.bind_all_ddl_with_id(ddl_month, "select Month,Month_Id from Month_Index order by Position asc");


                        fetch_fee_head();
                        find_firm_details();

                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "StudentList");
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

        private void fetch_fee_head()
        {
            using (SqlConnection conn = new SqlConnection(My.conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select distinct content,content_id from Fee_master_content_wise where parameter='MonthlyFee' order by content asc", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                ddl_fee_head.DataTextField = "content";
                ddl_fee_head.DataValueField = "content_id";
                ddl_fee_head.DataSource = reader;
                ddl_fee_head.DataBind();
            }
        }

        private void bind_class()
        {
            using (SqlConnection conn = new SqlConnection(My.conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Select Course_Name,course_id from Add_course_table order by Position", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                ddl_classs.DataTextField = "Course_Name";
                ddl_classs.DataValueField = "course_id";
                ddl_classs.DataSource = reader;
                ddl_classs.DataBind();
            }
        }

        private void find_firm_details()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details");
            if (dt.Rows.Count > 0)
            {
                imglogo.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();
            }
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    ddlsession.Focus();
                    Alertme("Please select session.", "warning");
                    return;
                }
                if (ddl_month.SelectedItem.Text == "Select")
                {
                    ddl_month.Focus();
                    Alertme("Please select month.", "warning");
                    return;
                }

                //For Class
                bool isClassSelectd = false; string selectClassid = "";
                foreach (ListItem item in ddl_classs.Items)
                {
                    if (item.Selected)
                    {
                        selectClassid = selectClassid + item.Value + ",";
                        isClassSelectd = true;
                    }
                }
                if (isClassSelectd == false)
                {
                    ddl_classs.Focus();
                    Alertme("Please select class.", "warning");
                    return;
                }
                if (isClassSelectd == true)
                {
                    selectClassid = selectClassid.Remove(selectClassid.Length - 1);
                }



                //For Fee Head
                bool isFeeSelectd = false; string selectFeeid = "";
                foreach (ListItem item in ddl_fee_head.Items)
                {
                    if (item.Selected)
                    {
                        selectFeeid = selectFeeid + item.Value + ",";
                        isFeeSelectd = true;
                    }
                }
                if (isFeeSelectd == false)
                {
                    ddl_fee_head.Focus();
                    Alertme("Please select fee head.", "warning");
                    return;
                }
                if (isFeeSelectd == true)
                {
                    selectFeeid = selectFeeid.Remove(selectFeeid.Length - 1);
                }
                lbl_month.Text = ddl_month.SelectedItem.Text;
                hd_find_status.Value = "1";
                hd_session_id.Value = ddlsession.SelectedValue;
                hd_month.Value = ddl_month.SelectedItem.Text;
                hd_class_id.Value = selectClassid;
                hd_fee_head_id.Value = selectFeeid;


            }
            catch (Exception ex)
            {
            }
        }
    }
}