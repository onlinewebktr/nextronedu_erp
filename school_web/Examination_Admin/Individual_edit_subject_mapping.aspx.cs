using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Examination_Admin
{
    public partial class Individual_edit_subject_mapping : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
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
                    try
                    {
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["sessionid"] = My.get_session_id();
                        ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        mycode.bind_all_ddl_with_id(ddlsession, "Select  Session,session_id from session_details");
                        mycode.bind_all_ddl_with_id(ddlclass, "Select Course_Name,course_id from Add_course_table order by Position asc");
                        if (Request.QueryString["adm"] != null)
                        {

                            ViewState["adm"] = Request.QueryString["adm"].ToString();
                            ViewState["classid"] = Request.QueryString["classid"].ToString();
                            ViewState["sessionid"] = Request.QueryString["sessionid"].ToString();

                            ddlsession.SelectedValue = ViewState["sessionid"].ToString();
                            ddlclass.SelectedValue = ViewState["classid"].ToString();
                            txt_admission_no.Text = ViewState["adm"].ToString();

                            find_student();
                            bind_subject();
                        }
                        else
                        {

                        }
                    }
                    catch
                    {

                    }
                }

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
            //try
            //{
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
            else if (txt_admission_no.Text == "")
            {
                Alertme("Please enter admisoon no.", "warning");
                txt_admission_no.Focus();
            }

            else
            {
                find_student();
                bind_subject();
            }
            //}
            //catch (Exception ex)
            //{
            //}
        }

        private void bind_subject()
        {
            DataTable dt = mycode.FillData("select * from Subject_Master where course_id='" + ddlclass.SelectedValue + "' and Branch_id='" + ViewState["branchid"].ToString() + "'");
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no subject list exist", "warning");
                rp_subJ_list.DataSource = null;
                rp_subJ_list.DataBind();
            }
            else
            {
                rp_subJ_list.DataSource = dt;
                rp_subJ_list.DataBind();
            }
        }

        private void find_student()
        {
            DataTable dt = mycode.FillData("select * from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and  admissionserialnumber='" + txt_admission_no.Text + "'  order by id desc");
            if (dt.Rows.Count == 0)
            {
                ViewState["classid"] = "0";
                Alertme("Sorry there are no data list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                ViewState["classid"] = dt.Rows[0]["Class_id"].ToString();
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }

        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }

        private bool findsubjectmpedorno(string subjectid)
        {
            string query = "Select Id from Subject_Mapping_New where Class_id='" + ddlclass.SelectedValue + "' and Sub_id='" + subjectid + "' and Session_id='" + ddlsession.SelectedValue + "' and Admission_no='" + txt_admission_no.Text + "'";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }



        protected void btn_map_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session.", "warning");
                }
                else if (ddlclass.SelectedItem.Text == "Select")
                {
                    Alertme("Please select class.", "warning");
                }
                else
                {
                    ViewState["statusUp1"] = "0";
                    save_map_course();
                    if (ViewState["statusUp1"].ToString() == "1")
                    {
                        Alertme("Subject has been mapped successfully.", "success");
                        find_student();
                        bind_subject();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void save_map_course()
        {
            string sesctionname = get_section();

            ViewState["statusUp1"] = "0";
            ViewState["statusUp"] = "0";
            //============
            int growcountS = rp_subJ_list.Items.Count;
            int kS = 0;
            for (int iS = 0; iS < growcountS; iS++)
            {

                CheckBox chkS = (CheckBox)rp_subJ_list.Items[iS].FindControl("rowChkBox1");
                if (chkS.Checked == true)
                {
                    if (ViewState["statusUp"].ToString() == "0")
                    {
                        mycode.executequery("delete from Subject_Mapping_New where Class_id='" + ddlclass.SelectedValue + "' and Session_id='" + ddlsession.SelectedValue + "' and Admission_no='" + txt_admission_no.Text + "' ");
                    }

                    else
                    {

                    }
                    Label lbl_subject_id = (Label)rp_subJ_list.Items[iS].FindControl("lbl_subject_id");
                    Label lbl_subject_name = (Label)rp_subJ_list.Items[iS].FindControl("lbl_subject_name");
                    ViewState["statusUp"] = "1";
                    //===============
                    if (mycode.IsUserExist("select Admission_no from Subject_Mapping_New where Class_id='" + ddlclass.SelectedValue + "' and Session_id='" + ddlsession.SelectedValue + "' and Admission_no='" + txt_admission_no.Text + "' and Sub_id=" + lbl_subject_id.Text + " and Section='" + sesctionname + "'"))
                    {
                        SqlCommand cmd;
                        string query = "INSERT INTO Subject_Mapping_New (Class_id,Section,Admission_no,Sub_id,Session,date,idate,type,Type_id,Session_id,Branch_id) values (@Class_id,@Section,@Admission_no,@Sub_id,@Session,@date,@idate,@type,@Type_id,@Session_id,@Branch_id)";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@Class_id", ddlclass.SelectedValue);
                        cmd.Parameters.AddWithValue("@Section", sesctionname);
                        cmd.Parameters.AddWithValue("@Admission_no", txt_admission_no.Text);
                        cmd.Parameters.AddWithValue("@Sub_id", lbl_subject_id.Text);
                        cmd.Parameters.AddWithValue("@Session", ddlsession.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@date", mycode.date());
                        cmd.Parameters.AddWithValue("@idate", mycode.idate());
                        cmd.Parameters.AddWithValue("@type", "0");
                        cmd.Parameters.AddWithValue("@Type_id", "0");
                        cmd.Parameters.AddWithValue("@Session_id", ddlsession.SelectedValue);
                        cmd.Parameters.AddWithValue("@Branch_id", ViewState["branchid"].ToString());
                        if (My.InsertUpdateData(cmd))
                        {
                            ViewState["statusUp1"] = "1";
                        }
                    }
                }
                else
                {
                    kS++;
                }
                if (kS == growcountS)
                {
                    Alertme("Please check minimum one subject list.", "warning");
                    ViewState["statusUp"] = "0";
                    ViewState["statusUp1"] = "1";
                }
                else
                {

                }
            }
        }

        private string get_section()
        {
            string query = "Select Section from admission_registor where Class_id='" + ddlclass.SelectedValue + "'   and Session_id='" + ddlsession.SelectedValue + "' and Admission_no='" + txt_admission_no.Text + "'";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                return "A";
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }
        }

        protected void rp_subJ_list_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                string subjectid = ((Label)e.Item.FindControl("lbl_subject_id")).Text;

                bool chkesubject = findsubjectmpedorno(subjectid);

                if (chkesubject == true)
                {
                    ((CheckBox)e.Item.FindControl("rowChkBox1")).Checked = true;
                }
                else
                {
                    ((CheckBox)e.Item.FindControl("rowChkBox1")).Checked = false;
                }


                if (((Label)e.Item.FindControl("lbl_midetry2")).Text == "True")
                {

                    ((Label)e.Item.FindControl("lbl_midetry")).Text = "Mandatory";
                    ((Label)e.Item.FindControl("lbl_midetry")).CssClass = "badge badge-success ml-2";

                }

                else
                {
                    ((Label)e.Item.FindControl("lbl_midetry")).Text = "N/A";
                    ((Label)e.Item.FindControl("lbl_midetry")).CssClass = "badge badge-danger ml-2";

                }


                if (((Label)e.Item.FindControl("lbl_Subject_Type_Scholastic_Co_Scholastic")).Text == "Scholastic")
                {

                    ((Label)e.Item.FindControl("lbl_Subject_Type_Scholastic_Co_Scholastic_dis")).Text = "Scholastic";
                    ((Label)e.Item.FindControl("lbl_Subject_Type_Scholastic_Co_Scholastic_dis")).CssClass = "badge badge-Scholastic ml-2";

                }

                else
                {
                    ((Label)e.Item.FindControl("lbl_Subject_Type_Scholastic_Co_Scholastic_dis")).Text = "Co-Scholastic";
                    ((Label)e.Item.FindControl("lbl_Subject_Type_Scholastic_Co_Scholastic_dis")).CssClass = "badge badge-coScholastic ml-2";

                }

            }





        }
        protected void ddl_subjecttyep_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_subjecttyep.Text == "All")
            {


                DataTable dt1 = mycode.FillData("select * from Subject_Master where course_id='" + ViewState["classid"] + "' and Branch_id='" + ViewState["branchid"].ToString() + "'");
                if (dt1.Rows.Count == 0)
                {
                    Alertme("Sorry there are no subject list exist", "warning");
                    rp_subJ_list.DataSource = null;
                    rp_subJ_list.DataBind();
                }
                else
                {
                    rp_subJ_list.DataSource = dt1;
                    rp_subJ_list.DataBind();
                }
            }
            else
            {


                DataTable dt1 = mycode.FillData("select * from Subject_Master where course_id='" + ViewState["classid"] + "' and Branch_id='" + ViewState["branchid"].ToString() + "' and Subject_Type_Scholastic_Co_Scholastic='" + ddl_subjecttyep.Text + "'");
                if (dt1.Rows.Count == 0)
                {
                    Alertme("Sorry there are no subject list exist", "warning");
                    rp_subJ_list.DataSource = null;
                    rp_subJ_list.DataBind();
                }
                else
                {
                    rp_subJ_list.DataSource = dt1;
                    rp_subJ_list.DataBind();
                }
            }
        }
    }
}