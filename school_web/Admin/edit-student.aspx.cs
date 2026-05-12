using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class edit_student : System.Web.UI.Page
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
                        if (Session["MsgeS"] != null)
                        {
                            Alertme(Session["MsgeS"].ToString(), "success");
                            Session["MsgeS"] = null;
                        }

                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["branch_id"] = mycode.get_branch_id(Session["Admin"].ToString());
                        ViewState["Branch_id"] = mycode.get_branch_id(Session["Admin"].ToString());

                        try
                        {
                            mycode.bind_all_ddl_with_id(ddlsessionad, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                            mycode.bind_all_ddl_with_id(ddl_session_student, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                            ddlsessionad.SelectedValue = My.get_session_id();
                            ddl_session_student.SelectedValue = My.get_session_id();


                            if (Request.QueryString["admno"] != null && Request.QueryString["sesnid"] != null)
                            {
                                txt_admission_no.Text = Request.QueryString["admno"];
                                ddl_session_student.SelectedValue = Request.QueryString["sesnid"];
                                find_by_admission_no();
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Mapping_Transportation_with_Student");
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


        #region WebMethoD
        [WebMethod]
        public static List<string> GetRooPath(string PathRooT, string Session_id)
        {
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(My.conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct studentname from admission_registor where studentname LIKE '%'+@SearchMobNo+'%'  and  Transfer_Status in ('NT','New' ) and  Status='1' and Session_id=" + Session_id + "";
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
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(My.conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct admissionserialnumber from admission_registor where admissionserialnumber LIKE '%'+@SearchMobNo+'%'   and  Transfer_Status in ('NT','New' ) and  Status='1' and Session_id=" + Session_id + "";
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

        protected void btn_find_admission_no_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlsessionad.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session.", "warning");
                    ddlsessionad.Focus();
                }
                else if (txt_admission_no.Text == "")
                {
                    Alertme("Please enter admission no.", "warning");
                    txt_admission_no.Focus();
                }
                else
                {
                    find_by_admission_no();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_by_admission_no()
        {
            string qry = "select * from admission_registor where admissionserialnumber='" + txt_admission_no.Text + "' and Session_id='" + ddlsessionad.SelectedValue + "' and Status='1' order by class,rollnumber asc";
            bind_gird(qry);
        }

        private void bind_gird(string qry)
        {
            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {
                Alertme("Student not found.", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }

        protected void btn_find_name_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_session_student.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session.", "warning");
                    ddl_session_student.Focus();
                }
                else if (txt_student_name.Text == "")
                {
                    Alertme("Please enter student name.", "warning");
                    txt_student_name.Focus();
                }
                else
                {
                    find_by_student_name();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_by_student_name()
        {
            string qry = "select * from admission_registor where studentname LIKE '%" + txt_student_name.Text + "%' and Session_id='" + ddl_session_student.SelectedValue + "' and Status='1' order by class,rollnumber asc";
            bind_gird(qry);
        }
    }
}