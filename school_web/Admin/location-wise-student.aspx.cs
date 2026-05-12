using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class location_wise_student : System.Web.UI.Page
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
                    if (!IsPostBack)
                    {
                        btn_excels.Visible = false;
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();
                        find_firm_details();
                        mycode.bind_all_ddl_with_id(ddlsession, "Select  Session,session_id from session_details order by session_id asc");
                        mycode.bind_all_ddl_with_id_cap_All(ddlclass, "Select Course_Name,course_id from Add_course_table order by Position");
                        ddlsession.SelectedValue = My.get_session_id();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Transfer_certificate");
            }
        }
        private void find_firm_details()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
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

        private void bind_student()
        {
            lbl_class22.Text = "";
            btn_excels.Visible = false;
            string query = "";
            if (ddlclass.SelectedItem.Text.ToUpper() == "ALL")
            {
                lbl_class22.Text = " Session : " + ddlsession.SelectedItem.Text;
                query = "select t1.* from admission_registor t1 join Add_course_table t2 on t1.Class_id=t2.course_id where t1.Session_id='" + ddlsession.SelectedValue + "' and careof like '%" + txt_area.Text + "%' order by t2.Position,t1.Section,t1.rollnumber asc";
            }
            else
            {
                lbl_class22.Text = " Session : " + ddlsession.SelectedItem.Text + " Class : " + ddlclass.SelectedItem.Text;
                query = "select t1.* from admission_registor t1 join Add_course_table t2 on t1.Class_id=t2.course_id where t1.Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and careof like '%" + txt_area.Text + "%' order by t2.Position,t1.Section,t1.rollnumber asc";
            }
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count == 0)
            {
                rd_view.DataSource = null;
                rd_view.DataBind();
                Alertme("Data Not Found...", "warning");
            }
            else
            {
                btn_excels.Visible = true;
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            if (ddlsession.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
            }
            else
            {
                bind_student();
            }
        }
        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;
                string excelName = My.with_excel_name("student-list");
                Response.AddHeader("content-disposition", "attachment;filename="+ excelName + ".xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                    Panel1.RenderControl(hw);
                    string style = @"<style> TABLE { border: 1px solid black; } TD { border: 1px solid black; } </style> ";
                    Response.Write(style);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End(); 
                }
            }
            catch
            {
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }



        [WebMethod]
        public static List<string> GetRooPath(string PathRooT, string Session_id, string class_id)
        {
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(My.conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string qry = "";
                    if (class_id == "0")
                    {
                        qry = "select distinct careof from admission_registor where careof LIKE '%'+@SearchMobNo+'%'   and Status='1' and Session_id='" + Session_id + "'";
                    }
                    else
                    {
                        qry = "select distinct careof from admission_registor where careof LIKE '%'+@SearchMobNo+'%'   and Status='1' and Session_id='" + Session_id + "' and Class_id='" + class_id + "'";
                    }
                    cmd.CommandText = qry;
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchMobNo", PathRooT);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        MobResult.Add(dr["careof"].ToString());
                    }
                    con.Close();
                    return MobResult;
                }
            }
        }
    }
}