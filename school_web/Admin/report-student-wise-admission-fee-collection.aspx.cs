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
    public partial class report_student_wise_admission_fee_collection : System.Web.UI.Page
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
                        ViewState["Flter"] = "0";
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();
                        ViewState["branchid"] = Session["branchid"].ToString();
                        find_firm_details();
                        string pagename_current = "fee-report.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];

                        mycode.bind_all_ddl_with_id(ddl_class, "Select Course_Name,course_id from Add_course_table order by Position");

                        ddl_class.SelectedValue = My.get_top_one_class();
                         
                        mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor order by Section");
                        ddl_section.Text = My.get_top_one_section();
                        mycode.bind_all_ddl_with_id(ddl_session, " select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int)");
                        mycode.bind_all_ddl_with_id(ddl_session_c, " select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int)");
                        ddl_session.SelectedValue = My.get_session_id();
                        ddl_session_c.SelectedValue = ddl_session.SelectedValue;

                        Bind_data_date_wise("OnlyClasS"); 
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Fee_Collection_Report");
            }
        }

        private void find_firm_details()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count > 0)
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
        private void Bind_data_date_wise(string type)
        {
            final_find_report_by_date(type);
        }

        private void final_find_report_by_date(string type)
        {
            SqlCommand cmd = new SqlCommand("sp_Fee_collection_report_new");  
            if (type == "OnlyClasS")
            {
                cmd.Parameters.AddWithValue("@Session_id ", ddl_session_c.SelectedValue);
                cmd.Parameters.AddWithValue("@class_id ", ddl_class.SelectedValue);
                cmd.Parameters.AddWithValue("@sp_status ", "12");
            }

            if (type == "ClasSSectioN")
            {
                cmd.Parameters.AddWithValue("@Session_id ", ddl_session_c.SelectedValue);
                cmd.Parameters.AddWithValue("@section ", ddl_section.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@class_id ", ddl_class.SelectedValue);
                cmd.Parameters.AddWithValue("@sp_status ", "13");
            }
            DataSet ds = My.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            int rowcount = ds.Tables[0].Rows.Count;
            if (rowcount > 0)
            {
                GrdView.DataSource = dt;
                GrdView.DataBind();

                btn_excels.Visible = true;
                print1.Visible = true;

                NotFoundS.Visible = false;
                tblPrintIQ.Visible = true;

                if (ViewState["Is_Print"].ToString() == "1")
                {
                    print1.Visible = true;
                }
                else
                {
                    print1.Visible = false;
                } 
            }
            else
            {
                GrdView.DataSource = null;
                GrdView.DataBind();

                btn_excels.Visible = false;
                print1.Visible = false;

                NotFoundS.Visible = true;
                tblPrintIQ.Visible = false;
            }
        }
         



        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Download"].ToString() == "1")
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=Export.xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    using (StringWriter sw = new StringWriter())
                    {
                        HtmlTextWriter hw = new HtmlTextWriter(sw);
                        pnl_grid.RenderControl(hw);
                        Response.Output.Write(sw.ToString());
                        Response.Flush();
                        Response.End();
                    }
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
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


        protected void btn_find_by_class_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_session.SelectedItem.Text == "Select")
                {
                    Alertme("Please choose session.", "warning");
                    ddl_session.Focus();
                }
                else if (ddl_class.SelectedItem.Text == "Select")
                {
                    Alertme("Please choose class.", "warning");
                    ddl_class.Focus();
                }
                else
                {
                    find_data_by_class();
                }
            }

            catch (Exception ex)
            {
            }
        }

        private void find_data_by_class()
        {
            if (ddl_section.SelectedItem.Text == "ALL")
            {
                Bind_data_date_wise("OnlyClasS");
            }
            else
            {
                Bind_data_date_wise("ClasSSectioN");
            }
        }

        protected void btn_find_by_adm_no_Click(object sender, EventArgs e)
        {
            try
            { 
                if (txt_admission_no.Text == "")
                {
                    Alertme("Please enter admission no.", "warning");
                    txt_admission_no.Focus();
                }
                else
                {
                    find_adm_no();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_adm_no()
        {
            SqlCommand cmd = new SqlCommand("sp_Fee_collection_report_new");
            cmd.Parameters.AddWithValue("@Session_id ", ddl_session.SelectedValue); 
            cmd.Parameters.AddWithValue("@Admission_no ", txt_admission_no.Text);
            cmd.Parameters.AddWithValue("@sp_status ", "14");
            DataSet ds = My.executeReaderDataSet(cmd);
            DataTable dt = ds.Tables[0];
            int rowcount = ds.Tables[0].Rows.Count;
            if (rowcount > 0)
            {
                GrdView.DataSource = dt;
                GrdView.DataBind();

                btn_excels.Visible = true;
                print1.Visible = true;

                NotFoundS.Visible = false;
                tblPrintIQ.Visible = true;

            }
            else
            {
                GrdView.DataSource = null;
                GrdView.DataBind();

                btn_excels.Visible = false;
                print1.Visible = false;

                NotFoundS.Visible = true;
                tblPrintIQ.Visible = false;
            }
        }

        protected void btn_find_by_student_name_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_session.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session", "warning");
                    ddl_session.Focus();
                }
                else if (txt_student_name.Text == "")
                {
                    Alertme("Please enter student name.", "warning");
                    txt_student_name.Focus();
                }
                else
                {
                    find_by_std_name();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_by_std_name()
        {
            string query = "select t1.* from admission_registor t1 join Admission_fee_collection t2 on  t1.admissionserialnumber=t2.Admission_no and t1.session=t2.session where t1.studentname like '%" + txt_student_name.Text + "%' and t1.Session_id='" + ddl_session.SelectedValue + "' and  t1.Status='1' order by t1.studentname asc";
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count == 0)
            {
                rp_std.DataSource = null;
                rp_std.DataBind();
                Alertme("Data Not Found...", "warning");
                myModal2.Visible = false;
            }
            else
            {
                rp_std.DataSource = dt;
                rp_std.DataBind();
                myModal2.Visible = true;
            }
        }


        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            myModal2.Visible = false;
        }

        protected void lnk_select_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbladmissionserialnumber = (Label)row.FindControl("lbladmissionserialnumber");
                Label lbl_session = (Label)row.FindControl("lbl_session");
                Label lbl_class_id = (Label)row.FindControl("lbl_class_id"); 
                txt_admission_no.Text = lbladmissionserialnumber.Text;

                find_adm_no();
                myModal2.Visible = false;
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }

        protected void lnks_filter_Click(object sender, EventArgs e)
        {
            filtersss();
        }

        private void filtersss()
        {
            if (ViewState["Flter"].ToString() == "0")
            { 
                fnds2.Visible = false;
                fnds3.Visible = true;
                fnds4.Visible = true;
                ViewState["Flter"] = "1";
            }
            else
            { 
                fnds2.Visible = true;
                fnds3.Visible = false;
                fnds4.Visible = false;
                ViewState["Flter"] = "0";
            }
        }


        #region WebMethoD
        [WebMethod]
        public static List<string> GetRooPath(string PathRooT, string Session_id)
        {
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(My.conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct studentname from admission_registor where studentname LIKE '%'+@SearchMobNo+'%' and Status='1' and Session_id='" + Session_id + "' and Transfer_Status='New'";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchMobNo", PathRooT);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        MobResult.Add(dr["studentname"].ToString());
                    }
                    con.Close();
                    return MobResult;
                }
            }
        }

        [WebMethod]

        public static List<string> GetRooPathAdmNo(string PathRooT, string Session_id)
        {
            string sessionid = Session_id;
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(My.conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct admissionserialnumber from admission_registor where admissionserialnumber LIKE ''+@SearchMobNo+'%'   and Status='1' and Session_id='" + Session_id + "' and Transfer_Status='New'";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchMobNo", PathRooT);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        MobResult.Add(dr["admissionserialnumber"].ToString());
                    }
                    con.Close();
                    return MobResult;
                }
            }
        }
        #endregion
    }
}