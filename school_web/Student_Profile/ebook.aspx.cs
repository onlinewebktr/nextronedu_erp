using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile
{
    public partial class ebook : System.Web.UI.Page
    {
        My imp = new My();
        UsesCode mycode = new UsesCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Session["User"] != null)
                    {
                        ViewState["sesssionid"] = My.get_session_id();
                        ViewState["regid"] = Session["User"].ToString();
                        Bind_student_details();

                    }
                    else
                    {
                        Session.Abandon();
                        Session.Clear();
                        Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
                        Response.Write("<script language=javascript>wnd.close();</script>");
                        Response.Write("<script language=javascript>window.open('../Default.aspx','_parent',replace=true);</script>");
                    }
                }
            }
            catch (Exception ex)
            {
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

        private void Bind_student_details()
        {
            string query = "select top 1 * from admission_registor where admissionserialnumber='" + ViewState["regid"].ToString() + "'   and StudentStatus='AV'    and Transfer_Status in ('New','NT') order by id desc";
            SqlDataAdapter ad_contactus = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad_contactus.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                Alertme("Something is wrong", "warning");
                return;
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ViewState["Session_id"] = dr["Session_id"].ToString();
                    ViewState["class_id"] = dr["Class_id"].ToString();
                    ViewState["Section"] = dr["Section"].ToString();
                    ViewState["Branch_id"] = dr["Branch_id"].ToString();
                }
                bind_subject();
                bind_ebooks("select * from EBook_Details where Class_id='" + ViewState["class_id"].ToString() + "' and Session_id='" + ViewState["Session_id"].ToString() + "' order by id desc");
            }
        }

        private void bind_ebooks(string qrys)
        {
            DataTable dt = mycode.FillData(qrys);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data list exist", "warning");
                rp_ebboks.DataSource = null;
                rp_ebboks.DataBind();
            }
            else
            {
                rp_ebboks.DataSource = dt;
                rp_ebboks.DataBind();
            }
        }

        private void bind_subject()
        {
            imp.bind_all_ddl_with_id_cap_All(ddl_subject, "Select   sm.Subject_name,sm.Subject_id,sm.Subject_position from Subject_Master sm join  Subject_Mapping_New smn on sm.course_id=smn.Class_id and sm.Subject_id=smn.Sub_id   where smn.Admission_no='" + ViewState["regid"].ToString() + "' and smn.Class_id='" + ViewState["class_id"].ToString() + "' and smn.Session_id='" + ViewState["Session_id"].ToString() + "' and smn.Section='" + ViewState["Section"].ToString() + "' order by sm.Subject_position");
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_subject.SelectedItem.Text == "ALL")
                {
                    bind_ebooks("select * from EBook_Details where Class_id='" + ViewState["class_id"].ToString() + "' and Session_id='" + ViewState["Session_id"].ToString() + "' order by id desc");
                }
                else
                {
                    bind_ebooks("select * from EBook_Details where Class_id='" + ViewState["class_id"].ToString() + "' and Subject_id='" + ddl_subject.SelectedValue + "' and Session_id='" + ViewState["Session_id"].ToString() + "' order by id desc");
                }
            }
            catch (Exception ex)
            {
            }
        }
         
    }
}