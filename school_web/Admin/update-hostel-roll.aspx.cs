using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
using System.IO;
using System.Web.Services;

namespace school_web.Admin
{
    public partial class update_hostel_roll : System.Web.UI.Page
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
                        ViewState["flagPosition"] = "1";
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();

                        mycode.bind_all_ddl_with_id(ddlsession, "Select  Session,session_id from session_details order by cast((Substring (Session,1,4)) as int) desc ");
                        ddlsession.SelectedValue = My.get_session_id();

                        mycode.bind_all_ddl_with_id_cap_All(ddlclass, "Select Course_Name,course_id,Position from Add_course_table order by Position");
                        ddlclass.SelectedValue = My.get_top_one_class();
                        fetch_section();

                        string pagename_current = Path.GetFileName(Request.Path);
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
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
        protected void btn_fnd_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session.", "warning");
                    ddlsession.Focus();
                }
                else if (ddlclass.SelectedItem.Text == "Select")
                {
                    Alertme("Please select class.", "warning");
                    ddlclass.Focus();
                }
                else if (ddl_secion.Text == "Select")
                {
                    Alertme("Please select section.", "warning");
                    ddlclass.Focus();
                }
                else
                {
                    if (ddlclass.SelectedItem.Text == "ALL")
                    {
                        bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "'  and hosteltaken='Yes' and Status='1'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  order by Hostel_roll_no asc");
                    }
                    else
                    {
                        if (ddl_secion.Text == "ALL")
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "'  and hosteltaken='Yes' and Class_id='" + ddlclass.SelectedValue + "' and Status='1'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  order by Hostel_roll_no asc");
                        }
                        else
                        {
                            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "'  and hosteltaken='Yes' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_secion.Text + "'  and Status='1' and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  order by Hostel_roll_no asc");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void bind_grd_view(string query)
        {
            ViewState["flag"] = query;
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                btn_save.Visible = false;
                Alertme("Sorry there are no data list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                btn_save.Visible = true;
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }


        protected void btn_save_Click(object sender, EventArgs e)
        {
            ViewState["msg"] = "0";
            int growcountS = rd_view.Items.Count;
            for (int i = 0; i < growcountS; i++)
            {
                Label lbl_admission_no = (Label)rd_view.Items[i].FindControl("lbl_admission_no");
                Label lbl_Session_id = (Label)rd_view.Items[i].FindControl("lbl_Session_id");
                Label lbl_Class_id = (Label)rd_view.Items[i].FindControl("lbl_Class_id");
                TextBox txt_roll_no = (TextBox)rd_view.Items[i].FindControl("txt_roll_no");
                SqlCommand cmd;
                string query = "update admission_registor set Hostel_roll_no=@Hostel_roll_no where admissionserialnumber=@admissionserialnumber and Class_id=@Class_id  and Session_id=@Session_id";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Hostel_roll_no", txt_roll_no.Text);
                cmd.Parameters.AddWithValue("@admissionserialnumber", lbl_admission_no.Text);
                cmd.Parameters.AddWithValue("@Class_id", lbl_Class_id.Text);
                cmd.Parameters.AddWithValue("@Session_id", lbl_Session_id.Text);
                if (My.InsertUpdateData(cmd))
                { }
                ViewState["msg"] = "1";
            }

            if (ViewState["msg"].ToString() == "1")
            {
                Alertme("Roll no. has been updated sucessfully.", "success");
            }
        }
        protected void ddlclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                fetch_section();
            }
            catch (Exception ex)
            {
            }
        }

        private void fetch_section()
        {
            mycode.bind_ddlall(ddl_secion, "Select distinct Section from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Status='1' order by Section");
        }




        protected void btn_find_admission_no_Click(object sender, EventArgs e)
        {
            if (ddlsession.Text == "")
            {
                Alertme("Please select session.", "warning");
                ddlsession.Focus();
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

        private void find_by_admission_no()
        {
            bind_grd_view("select *,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "'  and hosteltaken='Yes' and Status='1'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and admissionserialnumber='" + txt_admission_no.Text + "' order by Hostel_roll_no asc");
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
                    cmd.CommandText = "select distinct admissionserialnumber from admission_registor where admissionserialnumber LIKE '%'+@SearchMobNo+'%'   and Status='1' and Session_id='" + Session_id + "'";
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

        protected void btn_find_with_std_name_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlsession.Text == "")
                {
                    Alertme("Please select session.", "warning");
                    ddlsession.Focus();
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
            bind_grd_view("select *,CASE WHEN Transfer_Status='New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS Admission_Type,CASE WHEN Status = '0' THEN 'Inactive' WHEN Status = '1' THEN 'Active' END AS Status_name from admission_registor  join Add_course_table ct on admission_registor.Class_id=ct.course_id where Session_id='" + ddlsession.SelectedValue + "'  and hosteltaken='Yes' and Status='1'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and studentname='" + txt_student_name.Text + "' order by Hostel_roll_no asc");
        } 
    }
}