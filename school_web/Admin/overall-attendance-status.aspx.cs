using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class overall_attendance_status : System.Web.UI.Page
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
                        hd_session_id.Value = My.get_session_id();
                        fetch_class();
                        txt_find_date.Text = mycode.date();
                        //find_firm_details();


                        bool isClassSelectd = false; string selectClassid = "";
                        foreach (ListItem item in ddl_classs.Items)
                        {
                            if (item.Selected)
                            {
                                selectClassid = selectClassid + item.Value + ",";
                                isClassSelectd = true;
                            }
                        } 
                        if (isClassSelectd == true)
                        {
                            selectClassid = selectClassid.Remove(selectClassid.Length - 1);
                        } 
                        hd_class_id.Value = selectClassid;
                        hd_idate.Value = My.DateConvertToIdate(txt_find_date.Text).ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_group_master");
            }
        }

        //private void find_firm_details()
        //{
        //    DataTable dt = mycode.FillData("select * from Firm_Details ");
        //    if (dt.Rows.Count > 0)
        //    {
        //        imglogo.ImageUrl = dt.Rows[0]["logo"].ToString();
        //        lbl_address.Text = dt.Rows[0]["address1"].ToString();
        //        lbl_emaiid.Text = dt.Rows[0]["email"].ToString();
        //        lbl_website.Text = dt.Rows[0]["website"].ToString();
        //        lbl_contact_details.Text = dt.Rows[0]["contact_no"].ToString();
        //        lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();

        //        //if (dt.Rows[0]["firm_id"].ToString() == "NAVY-001")
        //        //{ 
        //        //}
        //        //else
        //        //{
        //        //    ddl_session.SelectedValue = My.get_session_id();
        //        //}
        //    }
        //}

        private void fetch_class()
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

            foreach (ListItem item in ddl_classs.Items)
            {
                item.Selected = true;
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
        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
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

                //lbl_class22.Text = "Date : " + txt_from_date.Text + " to " + txt_to_date.Text;
                //if (txt_from_date.Text == txt_to_date.Text)
                //{
                //    DateTime datatimes = DateTime.ParseExact(txt_from_date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //    lbl_class22.Text = "Date : " + txt_from_date.Text + " - " + datatimes.ToString("dddd");
                //}


                hd_class_id.Value = selectClassid;
                hd_idate.Value = My.DateConvertToIdate(txt_find_date.Text).ToString();
            }
            catch (Exception ex)
            {
            }
        }
    }
}