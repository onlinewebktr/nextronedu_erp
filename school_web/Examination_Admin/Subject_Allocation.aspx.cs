using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
namespace school_web.Examination_Admin
{
    public partial class Subject_Allocation : System.Web.UI.Page
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
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                      
                        bind_session();
                        bind_class();
                        lbl_subject_name.Text = "Subject List";
                        ddlsession.SelectedValue = My.get_session_id();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_Strength_Master");
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

        private void bind_session()
        {
            mycode.bind_all_ddl_with_id(ddlsession, "Select  Session,session_id from session_details");
        }
        private void bind_class()
        {
            mycode.bind_all_ddl_with_id(ddlclass, "Select Course_Name,course_id from Add_course_table order by Position asc");
        }

        protected void btn_find_Click(object sender, EventArgs e)
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

                else
                {
                    lbl_subject_name.Text = "Subject List(Class:- " + ddlclass.SelectedItem.Text + ")";
                    find_student();
                    bind_subject();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void bind_subject()
        {
            ViewState["classid"] = ddlclass.SelectedValue;
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
            string qry = "";
            if (ddl_section.SelectedItem.Text == "ALL")
            {
                qry = "select * from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Branch_id='" + ViewState["branchid"].ToString() + "' and Status='1' order by id desc";
            }
            else
            {
                qry = "select * from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Branch_id='" + ViewState["branchid"].ToString() + "' and Status='1' and Section='" + ddl_section.SelectedItem.Text + "' order by id desc";
            }

            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }

        protected void btn_map_Click(object sender, EventArgs e)
        {
            try
            {

                ViewState["statusUp"] = "0";
                save_map_course();
                if (ViewState["statusUp"].ToString() == "1")
                {
                    Alertme("Subject has been mapped successfully.", "success");
                }

            }
            catch (Exception ex)
            {
            }
        }

        private void save_map_course()
        {
            int growcount = rd_view.Items.Count;
            int k = 0;
            for (int i = 0; i < growcount; i++)
            {
                ViewState["statusUp"] = "0";
                CheckBox chk = (CheckBox)rd_view.Items[i].FindControl("rowChkBox");
                if (chk.Checked == true)
                {
                    Label lbl_adm_no = (Label)rd_view.Items[i].FindControl("lbl_adm_no");
                    Label lbl_section = (Label)rd_view.Items[i].FindControl("lbl_section");
                    Label lbl_Session_id = (Label)rd_view.Items[i].FindControl("lbl_Session_id");
                    Label lbl_Class_id = (Label)rd_view.Items[i].FindControl("lbl_Class_id");
                    Label lbl_session = (Label)rd_view.Items[i].FindControl("lbl_session");
                    //============
                    int growcountS = rp_subJ_list.Items.Count;
                    int kS = 0;
                    for (int iS = 0; iS < growcountS; iS++)
                    {

                        CheckBox chkS = (CheckBox)rp_subJ_list.Items[iS].FindControl("rowChkBox1");
                        if (chkS.Checked == true)
                        {
                            Label lbl_subject_id = (Label)rp_subJ_list.Items[iS].FindControl("lbl_subject_id");
                            Label lbl_subject_name = (Label)rp_subJ_list.Items[iS].FindControl("lbl_subject_name");
                            ViewState["statusUp"] = "1";
                            //===============
                            if (mycode.IsUserExist("select Admission_no from Subject_Mapping_New where Class_id='" + lbl_Class_id.Text + "' and Session_id='" + lbl_Session_id.Text + "' and Admission_no='" + lbl_adm_no.Text + "' and Sub_id=" + lbl_subject_id.Text + ""))
                            {
                                SqlCommand cmd;
                                string query = "INSERT INTO Subject_Mapping_New (Class_id,Section,Admission_no,Sub_id,Session,date,idate,type,Type_id,Session_id,Branch_id) values (@Class_id,@Section,@Admission_no,@Sub_id,@Session,@date,@idate,@type,@Type_id,@Session_id,@Branch_id)";
                                cmd = new SqlCommand(query);
                                cmd.Parameters.AddWithValue("@Class_id", lbl_Class_id.Text);
                                cmd.Parameters.AddWithValue("@Section", lbl_section.Text);
                                cmd.Parameters.AddWithValue("@Admission_no", lbl_adm_no.Text);
                                cmd.Parameters.AddWithValue("@Sub_id", lbl_subject_id.Text);
                                cmd.Parameters.AddWithValue("@Session", lbl_session.Text);
                                cmd.Parameters.AddWithValue("@date", mycode.date());
                                cmd.Parameters.AddWithValue("@idate", mycode.idate());
                                cmd.Parameters.AddWithValue("@type", "0");
                                cmd.Parameters.AddWithValue("@Type_id", "0");
                                cmd.Parameters.AddWithValue("@Session_id", lbl_Session_id.Text);
                                cmd.Parameters.AddWithValue("@Branch_id", ViewState["branchid"].ToString());
                                if (My.InsertUpdateData(cmd))
                                {
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
                           // ViewState["statusUp"] = "0";
                        }
                        else
                        {

                        }
                    }
                }
                else
                {
                    k++;
                }
            }

            if (k == growcount)
            {
                Alertme("Please check minimum one student list.", "warning");
                ViewState["statusUp"] = "0";
            }
            else
            {
            }
        }

        protected void ddlclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlsession.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
            }
            else if (ddlclass.SelectedItem.Text == "")
            {
                Alertme("Please select session", "warning");
            }
            else
            {
                mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'");
            }
        }

        protected void btn_find_by_admission_Click(object sender, EventArgs e)
        {
            if (txt_admission_no.Text == "")
            {
                Alertme("Please enter admission no.", "warning");
            }
            else
            {
                string qry = "select * from admission_registor where    admissionserialnumber='" + txt_admission_no.Text + "' and Status='1' order by id desc";


                DataTable dt = mycode.FillData(qry);
                if (dt.Rows.Count == 0)
                {
                    Alertme("Sorry there are no data list exist", "warning");
                    rd_view.DataSource = null;
                    rd_view.DataBind();
                }
                else
                {
                    rd_view.DataSource = dt;
                    rd_view.DataBind();



                    ViewState["classid"] = dt.Rows[0]["Class_id"].ToString();
                    lbl_subject_name.Text = "Subject List(Class:- " + dt.Rows[0]["class"].ToString() + ")";
                    DataTable dt1 = mycode.FillData("select * from Subject_Master where course_id='" + dt.Rows[0]["Class_id"].ToString() + "' and Branch_id='" + ViewState["branchid"].ToString() + "'");
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

        protected void rp_subJ_list_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {



            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

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