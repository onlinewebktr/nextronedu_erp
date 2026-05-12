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

namespace school_web.Admin
{
    public partial class students_subject_mapping_New : System.Web.UI.Page
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
                        string pagename_current = Path.GetFileName(Request.Path);
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];

                        ViewState["chek"] = "0";
                        if (Session["MsgS"] != null)
                        {
                            Alertme(Session["MsgS"].ToString(), "success");
                            Session["MsgS"] = null;
                        }

                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());

                        mycode.bind_all_ddl_with_id(ddlsession, " select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                        ddlsession.SelectedValue = My.get_session_id();
                        mycode.bind_all_ddl_with_id(ddlclass, "Select Course_Name,course_id from Add_course_table order by Position asc");
                        ddlclass.SelectedValue = My.get_top_one_class();
                        mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and   Branch_id='" + ViewState["branchid"].ToString() + "'");

                        My.bind_ddl_all_Cap(ddl_religion, "select distinct religion from admission_registor order by religion asc");

                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "students_subject_mapping_New");
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

        protected void ddlclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlsession.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
            }
            else if (ddlclass.SelectedItem.Text == "")
            {
                Alertme("Please select class", "warning");
            }
            else
            {
                mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and   Branch_id='" + ViewState["branchid"].ToString() + "'");
            }
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
                    find_student();
                }
            }
            catch (Exception ex)
            {
            }
        }



        private void find_student()
        {
            string qry = "";
            if (ddl_section.SelectedItem.Text == "ALL")
            {
                if (ddl_religion.SelectedItem.Text == "ALL")
                {
                    qry = "select * from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Branch_id='" + ViewState["branchid"].ToString() + "' and Status='1' order by Section,rollnumber asc";
                }
                else
                {
                    qry = "select * from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Branch_id='" + ViewState["branchid"].ToString() + "' and Status='1' and religion='" + ddl_religion.Text + "' order by Section,rollnumber asc";
                }
            }
            else
            {
                if (ddl_religion.SelectedItem.Text == "ALL")
                {
                    qry = "select * from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Branch_id='" + ViewState["branchid"].ToString() + "' and Status='1' and Section='" + ddl_section.SelectedItem.Text + "' order by rollnumber asc";
                }
                else
                {
                    qry = "select * from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Branch_id='" + ViewState["branchid"].ToString() + "' and Status='1' and Section='" + ddl_section.SelectedItem.Text + "' and religion='" + ddl_religion.Text + "' order by rollnumber asc";
                }
            }

            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no student data list exist", "warning");
                GrdView_data.DataSource = null;
                GrdView_data.DataBind();
            }
            else
            {
                bind_subject("class_wise");
            }
        }

        protected void btn_find_by_admission_Click(object sender, EventArgs e)
        {
            if (ddlsession.SelectedItem.Text == "")
            {
                Alertme("Please select session", "warning");
            }
            else if (txt_admission_no.Text == "")
            {
                Alertme("Please enter admission no.", "warning");
            }
            else
            {
                string qry = "select * from admission_registor where admissionserialnumber='" + txt_admission_no.Text + "' and Status='1'   and Branch_id='" + ViewState["branchid"].ToString() + "' and Session_id='" + ddlsession.SelectedValue + "' order by id desc";
                DataTable dt = mycode.FillData(qry);
                if (dt.Rows.Count == 0)
                {
                    Alertme("Sorry there are no student data list exist", "warning");
                    GrdView_data.DataSource = null;
                    GrdView_data.DataBind();
                }
                else
                {
                    bind_subject("admission_no");
                }
            }
        }

        private void bind_subject(string type)
        {
            string query = mycode.get_subject_heading_subjective(ddlclass.SelectedValue, ddl_section.Text, ddlsession.SelectedValue, ViewState["branchid"].ToString(), type, txt_admission_no.Text,ddl_religion.SelectedItem.Text);
            Final_bind_grid_data(query);
        }

        private void Final_bind_grid_data(string query)
        {
            ViewState["query"] = query;
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no student data list exist", "warning");
                GrdView_data.DataSource = null;
                GrdView_data.DataBind();
                btn_map.Visible = false;
                btn_chek_all.Visible = false;
                btn_uncheck_all.Visible = false;
            }
            else
            {
                btn_uncheck_all.Visible = true;
                btn_map.Visible = true;
                GrdView_data.DataSource = dt;
                GrdView_data.DataBind();
                btn_chek_all.Visible = true;
                scrpt = "<script> click_empty();</script>";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);

            }
        }

        protected void GrdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox cbx = new CheckBox();
                //cbx.ID = e.Row.Cells[3].Text;
                //// bind checkbox control with gridview :
                //e.Row.Cells[3].Controls.Add(cbx);

                cbx = new CheckBox();
                cbx.ID = e.Row.Cells[4].Text;
                e.Row.Cells[4].Controls.Add(cbx);
                cbx = new CheckBox();
                cbx.ID = e.Row.Cells[5].Text;
                e.Row.Cells[5].Controls.Add(cbx);
                try
                {
                    cbx = new CheckBox();
                    cbx.ID = e.Row.Cells[6].Text;
                    e.Row.Cells[6].Controls.Add(cbx);
                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    cbx.ID = e.Row.Cells[7].Text;
                    e.Row.Cells[7].Controls.Add(cbx);
                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    cbx.ID = e.Row.Cells[8].Text;
                    e.Row.Cells[8].Controls.Add(cbx);
                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    cbx.ID = e.Row.Cells[9].Text;
                    e.Row.Cells[9].Controls.Add(cbx);

                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    cbx.ID = e.Row.Cells[10].Text;
                    e.Row.Cells[10].Controls.Add(cbx);
                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    cbx.ID = e.Row.Cells[11].Text;
                    e.Row.Cells[11].Controls.Add(cbx);

                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    cbx.ID = e.Row.Cells[12].Text;
                    e.Row.Cells[12].Controls.Add(cbx);

                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    cbx.ID = e.Row.Cells[13].Text;
                    e.Row.Cells[13].Controls.Add(cbx);
                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    cbx.ID = e.Row.Cells[14].Text;
                    e.Row.Cells[14].Controls.Add(cbx);
                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    cbx.ID = e.Row.Cells[15].Text;
                    e.Row.Cells[15].Controls.Add(cbx);
                }
                catch
                {
                }
                try
                {
                    cbx = new CheckBox();
                    cbx.ID = e.Row.Cells[16].Text;
                    e.Row.Cells[16].Controls.Add(cbx);
                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    cbx.ID = e.Row.Cells[17].Text;
                    e.Row.Cells[17].Controls.Add(cbx);
                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    cbx.ID = e.Row.Cells[18].Text;
                    e.Row.Cells[18].Controls.Add(cbx);
                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    cbx.ID = e.Row.Cells[19].Text;
                    e.Row.Cells[19].Controls.Add(cbx);
                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    cbx.ID = e.Row.Cells[20].Text;
                    e.Row.Cells[20].Controls.Add(cbx);
                }
                catch
                {

                }

                //===========================
                try
                {
                    cbx = new CheckBox();
                    cbx.ID = e.Row.Cells[21].Text;
                    e.Row.Cells[21].Controls.Add(cbx);
                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    cbx.ID = e.Row.Cells[22].Text;
                    e.Row.Cells[22].Controls.Add(cbx);
                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    cbx.ID = e.Row.Cells[23].Text;
                    e.Row.Cells[23].Controls.Add(cbx);
                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    cbx.ID = e.Row.Cells[24].Text;
                    e.Row.Cells[24].Controls.Add(cbx);
                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    cbx.ID = e.Row.Cells[25].Text;
                    e.Row.Cells[25].Controls.Add(cbx);
                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    cbx.ID = e.Row.Cells[26].Text;
                    e.Row.Cells[26].Controls.Add(cbx);
                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    cbx.ID = e.Row.Cells[27].Text;
                    e.Row.Cells[27].Controls.Add(cbx);
                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    cbx.ID = e.Row.Cells[28].Text;
                    e.Row.Cells[28].Controls.Add(cbx);
                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    cbx.ID = e.Row.Cells[29].Text;
                    e.Row.Cells[29].Controls.Add(cbx);
                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    cbx.ID = e.Row.Cells[30].Text;
                    e.Row.Cells[30].Controls.Add(cbx);
                }
                catch
                {

                }
                //try
                //{
                //    cbx = new CheckBox();
                //    cbx.ID = e.Row.Cells[31].Text;
                //    e.Row.Cells[31].Controls.Add(cbx);
                //}
                //catch
                //{

                //}
                //try
                //{
                //    cbx = new CheckBox();
                //    cbx.ID = e.Row.Cells[32].Text;
                //    e.Row.Cells[31].Controls.Add(cbx);
                //}
                //catch
                //{

                //}

                //var d = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
                //if (d != null) lblGridRowsCount.Text = d.Count.ToString();
            }
        }

        protected void btn_map_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    DataTable dtConf = mycode.FillData("select Password from Confirmation_setting where Page='MapSubjStudent' and Is_confirmation=1");
                    if (dtConf.Rows.Count > 0)
                    {
                        ViewState["ConfPwd"] = dtConf.Rows[0]["Password"].ToString();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalPwd();", true);
                        return;
                    }
                }
                catch (Exception ex)
                {
                }
                update_subj_mapping();
            }
            catch (Exception ex)
            {
            }
        }
        protected void btn_password_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["ConfPwd"].ToString() == txt_pwd_code.Text)
                {
                    update_subj_mapping();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalPwd();", true);
                    Alertme("Incorrect password.", "warning");
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void update_subj_mapping()
        {
            if (ViewState["Is_add"].ToString() == "1")
            {
                ViewState["statusUp"] = "0";
                save_map_course();
                Final_bind_grid_data(ViewState["query"].ToString());
                Alertme("Subject has been mapped successfully.", "success");
                if (ViewState["statusUp"].ToString() == "1")
                {
                    //Alertme("Subject has been mapped successfully.", "success");
                    Session["MsgS"] = "Subject has been mapped successfully.";
                    Response.Redirect("students_subject_mapping_New.aspx", false);
                }
            }
            else if (ViewState["Is_Edit"].ToString() == "1")
            {
                ViewState["statusUp"] = "0";
                save_map_course();
                Final_bind_grid_data(ViewState["query"].ToString());
                Alertme("Subject has been mapped successfully.", "success");
                if (ViewState["statusUp"].ToString() == "1")
                {
                    //Alertme("Subject has been mapped successfully.", "success");
                    Session["MsgS"] = "Subject has been mapped successfully.";
                    Response.Redirect("students_subject_mapping_New.aspx", false);
                }
            }
            else
            {
                Alertme(My.get_restricted_message(), "warning");
            }
        }

        private void save_map_course()
        {
            try
            {
                foreach (GridViewRow row in GrdView_data.Rows)
                {
                    ViewState["statusUp"] = "1";
                    string admission_no = row.Cells[1].Text;
                    Dictionary<string, object> dc1 = My.get_selected_studentinfo_SubjMap(admission_no, ddlsession.SelectedValue, ViewState["branchid"].ToString());
                    string Class_Id = (String)dc1["Class_id"];
                    string section = (String)dc1["Section"];
                    My.exeSql("delete from Subject_Mapping_New where Admission_no='" + admission_no + "' and Session_id='" + ddlsession.SelectedValue + "' ");

                    try
                    {
                        string cell4 = row.Cells[4].Text;
                        var chk4 = row.Cells[4].Controls[0] as CheckBox;
                        if (chk4.Checked == true)
                        {
                            save_data_student_subject_mapping(admission_no, cell4, Class_Id, section);
                        }
                        else
                        {
                            remove_subject(admission_no, cell4, Class_Id, section);
                        }
                    }
                    catch
                    {

                    }
                    try
                    {
                        string cell5 = row.Cells[5].Text;
                        var chk5 = row.Cells[5].Controls[0] as CheckBox;
                        if (chk5.Checked == true)
                        {
                            save_data_student_subject_mapping(admission_no, cell5, Class_Id, section);

                        }
                        else
                        {
                            remove_subject(admission_no, cell5, Class_Id, section);
                        }
                    }
                    catch
                    {

                    }


                    try
                    {
                        string cell6 = row.Cells[6].Text;
                        var chk6 = row.Cells[6].Controls[0] as CheckBox;
                        if (chk6.Checked == true)
                        {
                            save_data_student_subject_mapping(admission_no, cell6, Class_Id, section);
                        }
                        else
                        {
                            remove_subject(admission_no, cell6, Class_Id, section);
                        }
                    }
                    catch
                    {

                    }

                    try
                    {
                        string cell7 = row.Cells[7].Text;
                        var chk7 = row.Cells[7].Controls[0] as CheckBox;
                        if (chk7.Checked == true)
                        {
                            save_data_student_subject_mapping(admission_no, cell7, Class_Id, section);
                        }
                        else
                        {
                            remove_subject(admission_no, cell7, Class_Id, section);
                        }
                    }
                    catch
                    {

                    }

                    try
                    {
                        string cell8 = row.Cells[8].Text;
                        var chk8 = row.Cells[8].Controls[0] as CheckBox;
                        if (chk8.Checked == true)
                        {
                            save_data_student_subject_mapping(admission_no, cell8, Class_Id, section);
                        }
                        else
                        {
                            remove_subject(admission_no, cell8, Class_Id, section);
                        }
                    }
                    catch
                    {
                    }


                    try
                    {
                        string cell9 = row.Cells[9].Text;
                        var chk9 = row.Cells[9].Controls[0] as CheckBox;
                        if (chk9.Checked == true)
                        {
                            save_data_student_subject_mapping(admission_no, cell9, Class_Id, section);
                        }
                        else
                        {
                            remove_subject(admission_no, cell9, Class_Id, section);
                        }
                    }
                    catch
                    {

                    }

                    try
                    {
                        string cell10 = row.Cells[10].Text;
                        var chk10 = row.Cells[10].Controls[0] as CheckBox;
                        if (chk10.Checked == true)
                        {
                            save_data_student_subject_mapping(admission_no, cell10, Class_Id, section);
                        }
                        else
                        {
                            remove_subject(admission_no, cell10, Class_Id, section);
                        }
                    }
                    catch
                    {

                    }


                    try
                    {
                        string cell11 = row.Cells[11].Text;
                        var chk11 = row.Cells[11].Controls[0] as CheckBox;
                        if (chk11.Checked == true)
                        {
                            save_data_student_subject_mapping(admission_no, cell11, Class_Id, section);
                        }
                        else
                        {
                            remove_subject(admission_no, cell11, Class_Id, section);
                        }

                    }
                    catch
                    {

                    }

                    try
                    {
                        string cell12 = row.Cells[12].Text;
                        var chk12 = row.Cells[12].Controls[0] as CheckBox;
                        if (chk12.Checked == true)
                        {
                            save_data_student_subject_mapping(admission_no, cell12, Class_Id, section);
                        }
                        else
                        {
                            remove_subject(admission_no, cell12, Class_Id, section);
                        }

                    }
                    catch
                    {

                    }

                    try
                    {
                        string cell13 = row.Cells[13].Text;
                        var chk13 = row.Cells[13].Controls[0] as CheckBox;
                        if (chk13.Checked == true)
                        {
                            save_data_student_subject_mapping(admission_no, cell13, Class_Id, section);
                        }
                        else
                        {
                            remove_subject(admission_no, cell13, Class_Id, section);
                        }

                    }
                    catch
                    {

                    }

                    try
                    {
                        string cell14 = row.Cells[14].Text;
                        var chk14 = row.Cells[14].Controls[0] as CheckBox;
                        if (chk14.Checked == true)
                        {
                            save_data_student_subject_mapping(admission_no, cell14, Class_Id, section);
                        }
                        else
                        {
                            remove_subject(admission_no, cell14, Class_Id, section);
                        }

                    }
                    catch
                    {

                    }

                    try
                    {
                        string cell15 = row.Cells[15].Text;
                        var chk15 = row.Cells[15].Controls[0] as CheckBox;
                        if (chk15.Checked == true)
                        {
                            save_data_student_subject_mapping(admission_no, cell15, Class_Id, section);
                        }
                        else
                        {
                            remove_subject(admission_no, cell15, Class_Id, section);
                        }
                    }
                    catch
                    {

                    }

                    try
                    {
                        string cell16 = row.Cells[16].Text;
                        var chk16 = row.Cells[16].Controls[0] as CheckBox;
                        if (chk16.Checked == true)
                        {
                            save_data_student_subject_mapping(admission_no, cell16, Class_Id, section);
                        }
                        else
                        {
                            remove_subject(admission_no, cell16, Class_Id, section);
                        }

                    }
                    catch
                    {

                    }

                    try
                    {
                        string cell17 = row.Cells[17].Text;
                        var chk17 = row.Cells[17].Controls[0] as CheckBox;
                        if (chk17.Checked == true)
                        {
                            save_data_student_subject_mapping(admission_no, cell17, Class_Id, section);
                        }
                        else
                        {
                            remove_subject(admission_no, cell17, Class_Id, section);
                        }
                    }
                    catch
                    {

                    }


                    try
                    {
                        string cell18 = row.Cells[18].Text;
                        var chk18 = row.Cells[18].Controls[0] as CheckBox;
                        if (chk18.Checked == true)
                        {
                            save_data_student_subject_mapping(admission_no, cell18, Class_Id, section);
                        }
                        else
                        {
                            remove_subject(admission_no, cell18, Class_Id, section);
                        }
                    }
                    catch
                    {

                    }
                    try
                    {
                        string cell19 = row.Cells[19].Text;
                        var chk19 = row.Cells[19].Controls[0] as CheckBox;
                        if (chk19.Checked == true)
                        {
                            save_data_student_subject_mapping(admission_no, cell19, Class_Id, section);
                        }
                        else
                        {
                            remove_subject(admission_no, cell19, Class_Id, section);
                        }

                    }
                    catch
                    {
                    }

                    try
                    {
                        string cell20 = row.Cells[20].Text;
                        var chk20 = row.Cells[20].Controls[0] as CheckBox;
                        if (chk20.Checked == true)
                        {
                            save_data_student_subject_mapping(admission_no, cell20, Class_Id, section);
                        }
                        else
                        {
                            remove_subject(admission_no, cell20, Class_Id, section);
                        }
                    }
                    catch
                    {

                    }
                    //=======================================================
                    try
                    {
                        string cell21 = row.Cells[21].Text;
                        var chk21 = row.Cells[21].Controls[0] as CheckBox;
                        if (chk21.Checked == true)
                        {
                            save_data_student_subject_mapping(admission_no, cell21, Class_Id, section);
                        }
                        else
                        {
                            remove_subject(admission_no, cell21, Class_Id, section);
                        }
                    }
                    catch
                    {
                    }

                    try
                    {
                        string cell22 = row.Cells[22].Text;
                        var chk22 = row.Cells[22].Controls[0] as CheckBox;
                        if (chk22.Checked == true)
                        {
                            save_data_student_subject_mapping(admission_no, cell22, Class_Id, section);
                        }
                        else
                        {
                            remove_subject(admission_no, cell22, Class_Id, section);
                        }
                    }
                    catch
                    {
                    }

                    try
                    {
                        string cell23 = row.Cells[23].Text;
                        var chk23 = row.Cells[23].Controls[0] as CheckBox;
                        if (chk23.Checked == true)
                        {
                            save_data_student_subject_mapping(admission_no, cell23, Class_Id, section);
                        }
                        else
                        {
                            remove_subject(admission_no, cell23, Class_Id, section);
                        }
                    }
                    catch
                    {
                    }

                    try
                    {
                        string cell24 = row.Cells[24].Text;
                        var chk24 = row.Cells[24].Controls[0] as CheckBox;
                        if (chk24.Checked == true)
                        {
                            save_data_student_subject_mapping(admission_no, cell24, Class_Id, section);
                        }
                        else
                        {
                            remove_subject(admission_no, cell24, Class_Id, section);
                        }
                    }
                    catch
                    {
                    }

                    try
                    {
                        string cell25 = row.Cells[25].Text;
                        var chk25 = row.Cells[25].Controls[0] as CheckBox;
                        if (chk25.Checked == true)
                        {
                            save_data_student_subject_mapping(admission_no, cell25, Class_Id, section);
                        }
                        else
                        {
                            remove_subject(admission_no, cell25, Class_Id, section);
                        }
                    }
                    catch
                    {
                    }

                    try
                    {
                        string cell26 = row.Cells[26].Text;
                        var chk26 = row.Cells[26].Controls[0] as CheckBox;
                        if (chk26.Checked == true)
                        {
                            save_data_student_subject_mapping(admission_no, cell26, Class_Id, section);
                        }
                        else
                        {
                            remove_subject(admission_no, cell26, Class_Id, section);
                        }
                    }
                    catch
                    {
                    }

                    try
                    {
                        string cell27 = row.Cells[27].Text;
                        var chk27 = row.Cells[27].Controls[0] as CheckBox;
                        if (chk27.Checked == true)
                        {
                            save_data_student_subject_mapping(admission_no, cell27, Class_Id, section);
                        }
                        else
                        {
                            remove_subject(admission_no, cell27, Class_Id, section);
                        }
                    }
                    catch
                    {
                    }

                    try
                    {
                        string cell28 = row.Cells[28].Text;
                        var chk28 = row.Cells[28].Controls[0] as CheckBox;
                        if (chk28.Checked == true)
                        {
                            save_data_student_subject_mapping(admission_no, cell28, Class_Id, section);
                        }
                        else
                        {
                            remove_subject(admission_no, cell28, Class_Id, section);
                        }
                    }
                    catch
                    {

                    }
                    try
                    {
                        string cell29 = row.Cells[29].Text;
                        var chk29 = row.Cells[29].Controls[0] as CheckBox;
                        if (chk29.Checked == true)
                        {
                            save_data_student_subject_mapping(admission_no, cell29, Class_Id, section);
                        }
                        else
                        {
                            remove_subject(admission_no, cell29, Class_Id, section);
                        }
                    }
                    catch
                    {

                    }
                    try
                    {
                        string cell30 = row.Cells[30].Text;
                        var chk30 = row.Cells[30].Controls[0] as CheckBox;
                        if (chk30.Checked == true)
                        {
                            save_data_student_subject_mapping(admission_no, cell30, Class_Id, section);
                        }
                        else
                        {
                            remove_subject(admission_no, cell30, Class_Id, section);
                        }
                    }
                    catch
                    {
                    }
                }
            }
            catch
            {
            }
        }

        private void remove_subject(string admission_no, string subjectid, string class_Id, string section)
        {
            My.exeSql("delete from Subject_Mapping_New where Session_id='" + ddlsession.SelectedValue + "' and Admission_no='" + admission_no + "' and Sub_id=" + subjectid + " and Class_id='" + class_Id + "' and Section='" + section + "'");
        }

        private void save_data_student_subject_mapping(string admission_no, string subjectid, string Class_Id, string section)
        {
            if (mycode.IsUserExist("select Admission_no from Subject_Mapping_New where Session_id='" + ddlsession.SelectedValue + "' and Admission_no='" + admission_no + "' and Sub_id=" + subjectid + " and   Section='" + section + "' and Class_id='" + Class_Id + "'"))
            {
                SqlCommand cmd;
                string query = "INSERT INTO Subject_Mapping_New (Class_id,Section,Admission_no,Sub_id,Session,date,idate,type,Type_id,Session_id,Branch_id,Send_status) values (@Class_id,@Section,@Admission_no,@Sub_id,@Session,@date,@idate,@type,@Type_id,@Session_id,@Branch_id,@Send_status)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Class_id", Class_Id);
                cmd.Parameters.AddWithValue("@Section", section);
                cmd.Parameters.AddWithValue("@Admission_no", admission_no);
                cmd.Parameters.AddWithValue("@Sub_id", subjectid);
                cmd.Parameters.AddWithValue("@Session", ddlsession.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@date", mycode.date());
                cmd.Parameters.AddWithValue("@idate", mycode.idate());
                cmd.Parameters.AddWithValue("@type", "0");
                cmd.Parameters.AddWithValue("@Type_id", "0");
                cmd.Parameters.AddWithValue("@Session_id", ddlsession.SelectedValue);
                cmd.Parameters.AddWithValue("@Branch_id", ViewState["branchid"].ToString());
                cmd.Parameters.AddWithValue("@Send_status", "Send");
                if (My.InsertUpdateData(cmd))
                {
                }
            }
        }

        protected void GrdView_data_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                CheckBox cbx = new CheckBox();
                //cbx.Text = e.Row.Cells[3].Text;
                //cbx.ID = e.Row.Cells[3].Text;
                //cbx.InputAttributes.Add("onchange", "subSelect('sub_1','" + cbx.ClientID + "')");
                //e.Row.Cells[3].Controls.Add(cbx);

                try
                {
                    cbx = new CheckBox();
                    // cbx.Checked = true;
                    cbx.Text = e.Row.Cells[4].Text;
                    cbx.ID = e.Row.Cells[4].Text;
                    cbx.InputAttributes.Add("onchange", "subSelect('sub_2','" + cbx.ClientID + "')");
                    e.Row.Cells[4].Controls.Add(cbx);

                }
                catch
                {

                }


                try
                {
                    cbx = new CheckBox();
                    cbx.ID = e.Row.Cells[5].Text;
                    cbx.Text = e.Row.Cells[5].Text;
                    cbx.InputAttributes.Add("onchange", "subSelect('sub_3','" + cbx.ClientID + "')");
                    e.Row.Cells[5].Controls.Add(cbx);

                }
                catch
                {

                }

                try
                {
                    cbx = new CheckBox();
                    //cbx.Checked = true;
                    cbx.ID = e.Row.Cells[6].Text;
                    cbx.Text = e.Row.Cells[6].Text;
                    cbx.InputAttributes.Add("onchange", "subSelect('sub_4','" + cbx.ClientID + "')");
                    e.Row.Cells[6].Controls.Add(cbx);

                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    // cbx.Checked = true;
                    cbx.ID = e.Row.Cells[7].Text;
                    cbx.Text = e.Row.Cells[7].Text;
                    cbx.InputAttributes.Add("onchange", "subSelect('sub_5','" + cbx.ClientID + "')");
                    e.Row.Cells[7].Controls.Add(cbx);

                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    // cbx.Checked = true;
                    cbx.ID = e.Row.Cells[8].Text;
                    cbx.Text = e.Row.Cells[8].Text;
                    cbx.InputAttributes.Add("onchange", "subSelect('sub_6','" + cbx.ClientID + "')");
                    e.Row.Cells[8].Controls.Add(cbx);

                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    // cbx.Checked = true;
                    cbx.ID = e.Row.Cells[9].Text;
                    cbx.Text = e.Row.Cells[9].Text;
                    cbx.InputAttributes.Add("onchange", "subSelect('sub_7','" + cbx.ClientID + "')");
                    e.Row.Cells[9].Controls.Add(cbx);


                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    //cbx.Checked = true;
                    cbx.ID = e.Row.Cells[10].Text;
                    cbx.Text = e.Row.Cells[10].Text;
                    cbx.InputAttributes.Add("onchange", "subSelect('sub_8','" + cbx.ClientID + "')");
                    e.Row.Cells[10].Controls.Add(cbx);

                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    //cbx.Checked = true;
                    cbx.ID = e.Row.Cells[11].Text;
                    cbx.Text = e.Row.Cells[11].Text;
                    cbx.InputAttributes.Add("onchange", "subSelect('sub_9','" + cbx.ClientID + "')");
                    e.Row.Cells[11].Controls.Add(cbx);


                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    // cbx.Checked = true;
                    cbx.ID = e.Row.Cells[12].Text;
                    cbx.Text = e.Row.Cells[12].Text;
                    cbx.InputAttributes.Add("onchange", "subSelect('sub_10','" + cbx.ClientID + "')");
                    e.Row.Cells[12].Controls.Add(cbx);


                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    //cbx.Checked = true;
                    cbx.ID = e.Row.Cells[13].Text;
                    cbx.Text = e.Row.Cells[13].Text;
                    cbx.InputAttributes.Add("onchange", "subSelect('sub_11','" + cbx.ClientID + "')");
                    e.Row.Cells[13].Controls.Add(cbx);

                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    //cbx.Checked = true;
                    cbx.ID = e.Row.Cells[14].Text;
                    cbx.Text = e.Row.Cells[14].Text;
                    cbx.InputAttributes.Add("onchange", "subSelect('sub_12','" + cbx.ClientID + "')");
                    e.Row.Cells[14].Controls.Add(cbx);

                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    // cbx.Checked = true;
                    cbx.ID = e.Row.Cells[15].Text;
                    cbx.Text = e.Row.Cells[15].Text;
                    cbx.InputAttributes.Add("onchange", "subSelect('sub_13','" + cbx.ClientID + "')");
                    e.Row.Cells[15].Controls.Add(cbx);

                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    //cbx.Checked = true;
                    cbx.ID = e.Row.Cells[16].Text;
                    cbx.Text = e.Row.Cells[16].Text;
                    cbx.InputAttributes.Add("onchange", "subSelect('sub_14','" + cbx.ClientID + "')");
                    e.Row.Cells[16].Controls.Add(cbx);

                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    // cbx.Checked = true;
                    cbx.ID = e.Row.Cells[17].Text;
                    cbx.Text = e.Row.Cells[17].Text;
                    cbx.InputAttributes.Add("onchange", "subSelect('sub_15','" + cbx.ClientID + "')");
                    e.Row.Cells[17].Controls.Add(cbx);

                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    // cbx.Checked = true;
                    cbx.ID = e.Row.Cells[18].Text;
                    cbx.Text = e.Row.Cells[18].Text;
                    cbx.InputAttributes.Add("onchange", "subSelect('sub_16','" + cbx.ClientID + "')");
                    e.Row.Cells[18].Controls.Add(cbx);

                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    // cbx.Checked = true;
                    cbx.ID = e.Row.Cells[19].Text;
                    cbx.Text = e.Row.Cells[19].Text;
                    cbx.InputAttributes.Add("onchange", "subSelect('sub_17','" + cbx.ClientID + "')");
                    e.Row.Cells[19].Controls.Add(cbx);

                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    //cbx.Checked = true;
                    cbx.ID = e.Row.Cells[20].Text;
                    cbx.Text = e.Row.Cells[20].Text;
                    cbx.InputAttributes.Add("onchange", "subSelect('sub_18','" + cbx.ClientID + "')");
                    e.Row.Cells[20].Controls.Add(cbx);
                }
                catch
                {

                }

                //===================================================
                try
                {
                    cbx = new CheckBox();
                    cbx.ID = e.Row.Cells[21].Text;
                    cbx.Text = e.Row.Cells[21].Text;
                    cbx.InputAttributes.Add("onchange", "subSelect('sub_19','" + cbx.ClientID + "')");
                    e.Row.Cells[21].Controls.Add(cbx);
                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    cbx.ID = e.Row.Cells[22].Text;
                    cbx.Text = e.Row.Cells[22].Text;
                    cbx.InputAttributes.Add("onchange", "subSelect('sub_20','" + cbx.ClientID + "')");
                    e.Row.Cells[22].Controls.Add(cbx);
                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    //cbx.Checked = true;
                    cbx.ID = e.Row.Cells[23].Text;
                    cbx.Text = e.Row.Cells[23].Text;
                    cbx.InputAttributes.Add("onchange", "subSelect('sub_21','" + cbx.ClientID + "')");
                    e.Row.Cells[23].Controls.Add(cbx);
                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    //cbx.Checked = true;
                    cbx.ID = e.Row.Cells[24].Text;
                    cbx.Text = e.Row.Cells[24].Text;
                    cbx.InputAttributes.Add("onchange", "subSelect('sub_22','" + cbx.ClientID + "')");
                    e.Row.Cells[24].Controls.Add(cbx);
                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    //cbx.Checked = true;
                    cbx.ID = e.Row.Cells[25].Text;
                    cbx.Text = e.Row.Cells[25].Text;
                    cbx.InputAttributes.Add("onchange", "subSelect('sub_23','" + cbx.ClientID + "')");
                    e.Row.Cells[25].Controls.Add(cbx);
                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    //cbx.Checked = true;
                    cbx.ID = e.Row.Cells[26].Text;
                    cbx.Text = e.Row.Cells[26].Text;
                    cbx.InputAttributes.Add("onchange", "subSelect('sub_24','" + cbx.ClientID + "')");
                    e.Row.Cells[26].Controls.Add(cbx);
                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    //cbx.Checked = true;
                    cbx.ID = e.Row.Cells[27].Text;
                    cbx.Text = e.Row.Cells[27].Text;
                    cbx.InputAttributes.Add("onchange", "subSelect('sub_25','" + cbx.ClientID + "')");
                    e.Row.Cells[27].Controls.Add(cbx);
                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    //cbx.Checked = true;
                    cbx.ID = e.Row.Cells[28].Text;
                    cbx.Text = e.Row.Cells[28].Text;
                    cbx.InputAttributes.Add("onchange", "subSelect('sub_26','" + cbx.ClientID + "')");
                    e.Row.Cells[28].Controls.Add(cbx);
                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    //cbx.Checked = true;
                    cbx.ID = e.Row.Cells[29].Text;
                    cbx.Text = e.Row.Cells[29].Text;
                    cbx.InputAttributes.Add("onchange", "subSelect('sub_27','" + cbx.ClientID + "')");
                    e.Row.Cells[29].Controls.Add(cbx);
                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    //cbx.Checked = true;
                    cbx.ID = e.Row.Cells[30].Text;
                    cbx.Text = e.Row.Cells[30].Text;
                    cbx.InputAttributes.Add("onchange", "subSelect('sub_28','" + cbx.ClientID + "')");
                    e.Row.Cells[30].Controls.Add(cbx);
                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    //cbx.Checked = true;
                    cbx.ID = e.Row.Cells[31].Text;
                    cbx.Text = e.Row.Cells[31].Text;
                    cbx.InputAttributes.Add("onchange", "subSelect('sub_29','" + cbx.ClientID + "')");
                    e.Row.Cells[31].Controls.Add(cbx);
                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    //cbx.Checked = true;
                    cbx.ID = e.Row.Cells[32].Text;
                    cbx.Text = e.Row.Cells[32].Text;
                    cbx.InputAttributes.Add("onchange", "subSelect('sub_30','" + cbx.ClientID + "')");
                    e.Row.Cells[32].Controls.Add(cbx);
                }
                catch
                {

                }

                try
                {
                    cbx = new CheckBox();
                    //cbx.Checked = true;
                    cbx.ID = e.Row.Cells[33].Text;
                    cbx.Text = e.Row.Cells[33].Text;
                    cbx.InputAttributes.Add("onchange", "subSelect('sub_31','" + cbx.ClientID + "')");
                    e.Row.Cells[33].Controls.Add(cbx);
                }
                catch
                {

                }

                try
                {
                    cbx = new CheckBox();
                    //cbx.Checked = true;
                    cbx.ID = e.Row.Cells[34].Text;
                    cbx.Text = e.Row.Cells[34].Text;
                    cbx.InputAttributes.Add("onchange", "subSelect('sub_32','" + cbx.ClientID + "')");
                    e.Row.Cells[34].Controls.Add(cbx);
                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    //cbx.Checked = true;
                    cbx.ID = e.Row.Cells[35].Text;
                    cbx.Text = e.Row.Cells[35].Text;
                    cbx.InputAttributes.Add("onchange", "subSelect('sub_33','" + cbx.ClientID + "')");
                    e.Row.Cells[35].Controls.Add(cbx);
                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    //cbx.Checked = true;
                    cbx.ID = e.Row.Cells[36].Text;
                    cbx.Text = e.Row.Cells[36].Text;
                    cbx.InputAttributes.Add("onchange", "subSelect('sub_34','" + cbx.ClientID + "')");
                    e.Row.Cells[36].Controls.Add(cbx);
                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    //cbx.Checked = true;
                    cbx.ID = e.Row.Cells[37].Text;
                    cbx.Text = e.Row.Cells[37].Text;
                    cbx.InputAttributes.Add("onchange", "subSelect('sub_35','" + cbx.ClientID + "')");
                    e.Row.Cells[37].Controls.Add(cbx);
                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    //cbx.Checked = true;
                    cbx.ID = e.Row.Cells[38].Text;
                    cbx.Text = e.Row.Cells[38].Text;
                    cbx.InputAttributes.Add("onchange", "subSelect('sub_36','" + cbx.ClientID + "')");
                    e.Row.Cells[38].Controls.Add(cbx);
                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    //cbx.Checked = true;
                    cbx.ID = e.Row.Cells[39].Text;
                    cbx.Text = e.Row.Cells[39].Text;
                    cbx.InputAttributes.Add("onchange", "subSelect('sub_37','" + cbx.ClientID + "')");
                    e.Row.Cells[39].Controls.Add(cbx);
                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    //cbx.Checked = true;
                    cbx.ID = e.Row.Cells[40].Text;
                    cbx.Text = e.Row.Cells[40].Text;
                    cbx.InputAttributes.Add("onchange", "subSelect('sub_38','" + cbx.ClientID + "')");
                    e.Row.Cells[40].Controls.Add(cbx);
                }
                catch
                {

                }

                try
                {
                    cbx = new CheckBox();
                    //cbx.Checked = true;
                    cbx.ID = e.Row.Cells[41].Text;
                    cbx.Text = e.Row.Cells[41].Text;
                    cbx.InputAttributes.Add("onchange", "subSelect('sub_39','" + cbx.ClientID + "')");
                    e.Row.Cells[41].Controls.Add(cbx);
                }
                catch
                {

                }

                try
                {
                    cbx = new CheckBox();
                    //cbx.Checked = true;
                    cbx.ID = e.Row.Cells[42].Text;
                    cbx.Text = e.Row.Cells[42].Text;
                    cbx.InputAttributes.Add("onchange", "subSelect('sub_40','" + cbx.ClientID + "')");
                    e.Row.Cells[42].Controls.Add(cbx);
                }
                catch
                {

                }

            }


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox cbx = new CheckBox();
                //cbx.ID = e.Row.Cells[3].Text;
                //cbx.InputAttributes.Add("class", "sub_1");
                //e.Row.Cells[3].Controls.Add(cbx);

                try
                {
                    cbx = new CheckBox();
                    cbx.InputAttributes.Add("class", "sub_2");
                    // cbx.Checked = true;
                    cbx.ID = e.Row.Cells[4].Text;
                    e.Row.Cells[4].Controls.Add(cbx);


                }
                catch
                {

                }


                try
                {
                    cbx = new CheckBox();
                    cbx.InputAttributes.Add("class", "sub_3");
                    /// cbx.Checked = true;
                    cbx.ID = e.Row.Cells[5].Text;
                    e.Row.Cells[5].Controls.Add(cbx);

                }
                catch
                {

                }

                try
                {
                    cbx = new CheckBox();
                    cbx.InputAttributes.Add("class", "sub_4");
                    //cbx.Checked = true;
                    cbx.ID = e.Row.Cells[6].Text;
                    e.Row.Cells[6].Controls.Add(cbx);

                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    cbx.InputAttributes.Add("class", "sub_5");
                    // cbx.Checked = true;
                    cbx.ID = e.Row.Cells[7].Text;
                    e.Row.Cells[7].Controls.Add(cbx);

                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    cbx.InputAttributes.Add("class", "sub_6");
                    // cbx.Checked = true;
                    cbx.ID = e.Row.Cells[8].Text;
                    e.Row.Cells[8].Controls.Add(cbx);

                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    cbx.InputAttributes.Add("class", "sub_7");
                    // cbx.Checked = true;
                    cbx.ID = e.Row.Cells[9].Text;
                    e.Row.Cells[9].Controls.Add(cbx);


                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    cbx.InputAttributes.Add("class", "sub_8");
                    //cbx.Checked = true;
                    cbx.ID = e.Row.Cells[10].Text;
                    e.Row.Cells[10].Controls.Add(cbx);

                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    cbx.InputAttributes.Add("class", "sub_9");
                    //cbx.Checked = true;
                    cbx.ID = e.Row.Cells[11].Text;
                    e.Row.Cells[11].Controls.Add(cbx);


                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    cbx.InputAttributes.Add("class", "sub_10");
                    // cbx.Checked = true;
                    cbx.ID = e.Row.Cells[12].Text;
                    e.Row.Cells[12].Controls.Add(cbx);


                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    cbx.InputAttributes.Add("class", "sub_11");
                    //cbx.Checked = true;
                    cbx.ID = e.Row.Cells[13].Text;
                    e.Row.Cells[13].Controls.Add(cbx);

                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    cbx.InputAttributes.Add("class", "sub_12");
                    //cbx.Checked = true;
                    cbx.ID = e.Row.Cells[14].Text;
                    e.Row.Cells[14].Controls.Add(cbx);

                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    cbx.InputAttributes.Add("class", "sub_13");
                    // cbx.Checked = true;
                    cbx.ID = e.Row.Cells[15].Text;
                    e.Row.Cells[15].Controls.Add(cbx);

                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    cbx.InputAttributes.Add("class", "sub_14");
                    //cbx.Checked = true;
                    cbx.ID = e.Row.Cells[16].Text;
                    e.Row.Cells[16].Controls.Add(cbx);

                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    cbx.InputAttributes.Add("class", "sub_15");
                    // cbx.Checked = true;
                    cbx.ID = e.Row.Cells[17].Text;
                    e.Row.Cells[17].Controls.Add(cbx);

                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    cbx.InputAttributes.Add("class", "sub_16");
                    // cbx.Checked = true;
                    cbx.ID = e.Row.Cells[18].Text;
                    e.Row.Cells[18].Controls.Add(cbx);

                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    cbx.InputAttributes.Add("class", "sub_17");
                    // cbx.Checked = true;
                    cbx.ID = e.Row.Cells[19].Text;
                    e.Row.Cells[19].Controls.Add(cbx);

                }
                catch
                {

                }
                try
                {
                    cbx = new CheckBox();
                    cbx.InputAttributes.Add("class", "sub_18");
                    //cbx.Checked = true;
                    cbx.ID = e.Row.Cells[20].Text;
                    e.Row.Cells[20].Controls.Add(cbx);
                }
                catch
                {
                }

                //=====================================================
                try
                {
                    cbx = new CheckBox();
                    cbx.InputAttributes.Add("class", "sub_19");
                    //cbx.Checked = true;
                    cbx.ID = e.Row.Cells[21].Text;
                    e.Row.Cells[21].Controls.Add(cbx);
                }
                catch
                {
                }
                try
                {
                    cbx = new CheckBox();
                    cbx.InputAttributes.Add("class", "sub_20");
                    //cbx.Checked = true;
                    cbx.ID = e.Row.Cells[22].Text;
                    e.Row.Cells[22].Controls.Add(cbx);
                }
                catch
                {
                }
                try
                {
                    cbx = new CheckBox();
                    cbx.InputAttributes.Add("class", "sub_21");
                    //cbx.Checked = true;
                    cbx.ID = e.Row.Cells[23].Text;
                    e.Row.Cells[23].Controls.Add(cbx);
                }
                catch
                {
                }
                try
                {
                    cbx = new CheckBox();
                    cbx.InputAttributes.Add("class", "sub_22");
                    //cbx.Checked = true;
                    cbx.ID = e.Row.Cells[24].Text;
                    e.Row.Cells[24].Controls.Add(cbx);
                }
                catch
                {
                }
                try
                {
                    cbx = new CheckBox();
                    cbx.InputAttributes.Add("class", "sub_23");
                    //cbx.Checked = true;
                    cbx.ID = e.Row.Cells[25].Text;
                    e.Row.Cells[25].Controls.Add(cbx);
                }
                catch
                {
                }
                try
                {
                    cbx = new CheckBox();
                    cbx.InputAttributes.Add("class", "sub_24");
                    //cbx.Checked = true;
                    cbx.ID = e.Row.Cells[26].Text;
                    e.Row.Cells[26].Controls.Add(cbx);
                }
                catch
                {
                }
                try
                {
                    cbx = new CheckBox();
                    cbx.InputAttributes.Add("class", "sub_25");
                    //cbx.Checked = true;
                    cbx.ID = e.Row.Cells[27].Text;
                    e.Row.Cells[27].Controls.Add(cbx);
                }
                catch
                {
                }
                try
                {
                    cbx = new CheckBox();
                    cbx.InputAttributes.Add("class", "sub_26");
                    //cbx.Checked = true;
                    cbx.ID = e.Row.Cells[28].Text;
                    e.Row.Cells[28].Controls.Add(cbx);
                }
                catch
                {
                }
                try
                {
                    cbx = new CheckBox();
                    cbx.InputAttributes.Add("class", "sub_27");
                    //cbx.Checked = true;
                    cbx.ID = e.Row.Cells[29].Text;
                    e.Row.Cells[29].Controls.Add(cbx);
                }
                catch
                {
                }
                try
                {
                    cbx = new CheckBox();
                    cbx.InputAttributes.Add("class", "sub_28");
                    //cbx.Checked = true;
                    cbx.ID = e.Row.Cells[30].Text;
                    e.Row.Cells[30].Controls.Add(cbx);
                }
                catch
                {
                }

                try
                {
                    cbx = new CheckBox();
                    cbx.InputAttributes.Add("class", "sub_29");
                    //cbx.Checked = true;
                    cbx.ID = e.Row.Cells[31].Text;
                    e.Row.Cells[31].Controls.Add(cbx);
                }
                catch
                {
                }
                try
                {
                    cbx = new CheckBox();
                    cbx.InputAttributes.Add("class", "sub_30");
                    //cbx.Checked = true;
                    cbx.ID = e.Row.Cells[32].Text;
                    e.Row.Cells[32].Controls.Add(cbx);
                }
                catch
                {
                }

                try
                {
                    cbx = new CheckBox();
                    cbx.InputAttributes.Add("class", "sub_31");
                    //cbx.Checked = true;
                    cbx.ID = e.Row.Cells[33].Text;
                    e.Row.Cells[33].Controls.Add(cbx);
                }
                catch
                {
                }

                try
                {
                    cbx = new CheckBox();
                    cbx.InputAttributes.Add("class", "sub_32");
                    //cbx.Checked = true;
                    cbx.ID = e.Row.Cells[34].Text;
                    e.Row.Cells[34].Controls.Add(cbx);
                }
                catch
                {
                }

                try
                {
                    cbx = new CheckBox();
                    cbx.InputAttributes.Add("class", "sub_33");
                    //cbx.Checked = true;
                    cbx.ID = e.Row.Cells[35].Text;
                    e.Row.Cells[35].Controls.Add(cbx);
                }
                catch
                {
                }


                try
                {
                    cbx = new CheckBox();
                    cbx.InputAttributes.Add("class", "sub_34");
                    //cbx.Checked = true;
                    cbx.ID = e.Row.Cells[36].Text;
                    e.Row.Cells[36].Controls.Add(cbx);
                }
                catch
                {
                }

                try
                {
                    cbx = new CheckBox();
                    cbx.InputAttributes.Add("class", "sub_35");
                    //cbx.Checked = true;
                    cbx.ID = e.Row.Cells[37].Text;
                    e.Row.Cells[37].Controls.Add(cbx);
                }
                catch
                {
                }

                try
                {
                    cbx = new CheckBox();
                    cbx.InputAttributes.Add("class", "sub_36");
                    //cbx.Checked = true;
                    cbx.ID = e.Row.Cells[38].Text;
                    e.Row.Cells[38].Controls.Add(cbx);
                }
                catch
                {
                }

                try
                {
                    cbx = new CheckBox();
                    cbx.InputAttributes.Add("class", "sub_37");
                    //cbx.Checked = true;
                    cbx.ID = e.Row.Cells[39].Text;
                    e.Row.Cells[39].Controls.Add(cbx);
                }
                catch
                {
                }

                try
                {
                    cbx = new CheckBox();
                    cbx.InputAttributes.Add("class", "sub_38");
                    //cbx.Checked = true;
                    cbx.ID = e.Row.Cells[40].Text;
                    e.Row.Cells[40].Controls.Add(cbx);
                }
                catch
                {
                }

                try
                {
                    cbx = new CheckBox();
                    cbx.InputAttributes.Add("class", "sub_39");
                    //cbx.Checked = true;
                    cbx.ID = e.Row.Cells[41].Text;
                    e.Row.Cells[41].Controls.Add(cbx);
                }
                catch
                {
                }

                try
                {
                    cbx = new CheckBox();
                    cbx.InputAttributes.Add("class", "sub_40");
                    //cbx.Checked = true;
                    cbx.ID = e.Row.Cells[42].Text;
                    e.Row.Cells[42].Controls.Add(cbx);
                }
                catch
                {
                }

            }
            ViewState["IsLoad"] = "1";
        }

        protected void btn_empty_Click(object sender, EventArgs e)
        {
            check_mapped_std();
        }

        protected void btn_chek_all_Click(object sender, EventArgs e)
        {
            ViewState["chek"] = "0";
            bool val = true;

            bind_data_chek_and_unchek(val);


        }

        private void bind_data_chek_and_unchek(bool val)
        {
            {
                GridViewRow row = GrdView_data.HeaderRow;
                //try
                //{

                //    var chk3 = row.Cells[3].Controls[0] as CheckBox;
                //    chk3.Checked = val;
                //}
                //catch
                //{

                //}

                try
                {

                    var chk4 = row.Cells[4].Controls[0] as CheckBox;
                    chk4.Checked = val;

                }
                catch
                {

                }
                try
                {

                    var chk5 = row.Cells[5].Controls[0] as CheckBox;
                    chk5.Checked = val;

                }
                catch
                {

                }


                try
                {

                    var chk6 = row.Cells[6].Controls[0] as CheckBox;
                    chk6.Checked = val;

                }
                catch
                {

                }

                try
                {

                    var chk7 = row.Cells[7].Controls[0] as CheckBox;
                    chk7.Checked = val;

                }
                catch
                {

                }

                try
                {

                    var chk8 = row.Cells[8].Controls[0] as CheckBox;
                    chk8.Checked = val;

                }
                catch
                {

                }


                try
                {

                    var chk9 = row.Cells[9].Controls[0] as CheckBox;
                    chk9.Checked = val;

                }
                catch
                {

                }

                try
                {

                    var chk10 = row.Cells[10].Controls[0] as CheckBox;
                    chk10.Checked = val;

                }
                catch
                {

                }


                try
                {
                    var chk11 = row.Cells[11].Controls[0] as CheckBox;
                    chk11.Checked = val;

                }
                catch
                {

                }

                try
                {
                    var chk12 = row.Cells[12].Controls[0] as CheckBox;
                    chk12.Checked = val;

                }
                catch
                {

                }

                try
                {
                    var chk13 = row.Cells[13].Controls[0] as CheckBox;
                    chk13.Checked = val;
                }
                catch
                {

                }

                try
                {
                    var chk14 = row.Cells[14].Controls[0] as CheckBox;
                    chk14.Checked = val;

                }
                catch
                {

                }

                try
                {
                    var chk15 = row.Cells[15].Controls[0] as CheckBox;
                    chk15.Checked = val;

                }
                catch
                {

                }

                try
                {
                    var chk16 = row.Cells[16].Controls[0] as CheckBox;
                    chk16.Checked = val;

                }
                catch
                {

                }

                try
                {
                    var chk17 = row.Cells[17].Controls[0] as CheckBox;
                    chk17.Checked = val;

                }
                catch
                {

                }


                try
                {
                    var chk18 = row.Cells[18].Controls[0] as CheckBox;
                    chk18.Checked = val;

                }
                catch
                {

                }
                try
                {
                    var chk19 = row.Cells[19].Controls[0] as CheckBox;
                    chk19.Checked = val;

                }
                catch
                {

                }

                try
                {
                    var chk20 = row.Cells[20].Controls[0] as CheckBox;
                    chk20.Checked = val;
                }
                catch
                {

                }


                ///=========================================
                ///
                try
                {
                    var chk21 = row.Cells[21].Controls[0] as CheckBox;
                    chk21.Checked = val;
                }
                catch
                {

                }
                try
                {
                    var chk22 = row.Cells[22].Controls[0] as CheckBox;
                    chk22.Checked = val;
                }
                catch
                {

                }
                try
                {
                    var chk23 = row.Cells[23].Controls[0] as CheckBox;
                    chk23.Checked = val;
                }
                catch
                {

                }
                try
                {
                    var chk24 = row.Cells[24].Controls[0] as CheckBox;
                    chk24.Checked = val;
                }
                catch
                {

                }
                try
                {
                    var chk25 = row.Cells[25].Controls[0] as CheckBox;
                    chk25.Checked = val;
                }
                catch
                {

                }
                try
                {
                    var chk26 = row.Cells[26].Controls[0] as CheckBox;
                    chk26.Checked = val;
                }
                catch
                {

                }
                try
                {
                    var chk27 = row.Cells[27].Controls[0] as CheckBox;
                    chk27.Checked = val;
                }
                catch
                {

                }
                try
                {
                    var chk28 = row.Cells[28].Controls[0] as CheckBox;
                    chk28.Checked = val;
                }
                catch
                {

                }
                try
                {
                    var chk29 = row.Cells[29].Controls[0] as CheckBox;
                    chk29.Checked = val;
                }
                catch
                {

                }
                try
                {
                    var chk30 = row.Cells[30].Controls[0] as CheckBox;
                    chk30.Checked = val;
                }
                catch
                {

                }
            }
            //Data
            foreach (GridViewRow row in GrdView_data.Rows)
            {
                //try
                //{
                //    var chk3 = row.Cells[3].Controls[0] as CheckBox;
                //    chk3.Checked = val;
                //}
                //catch
                //{

                //}

                try
                {
                    var chk4 = row.Cells[4].Controls[0] as CheckBox;
                    chk4.Checked = val;

                }
                catch
                {

                }
                try
                {
                    var chk5 = row.Cells[5].Controls[0] as CheckBox;
                    chk5.Checked = val;

                }
                catch
                {

                }


                try
                {
                    var chk6 = row.Cells[6].Controls[0] as CheckBox;
                    chk6.Checked = val;

                }
                catch
                {

                }

                try
                {
                    var chk7 = row.Cells[7].Controls[0] as CheckBox;
                    chk7.Checked = val;

                }
                catch
                {

                }

                try
                {
                    var chk8 = row.Cells[8].Controls[0] as CheckBox;
                    chk8.Checked = val;

                }
                catch
                {

                }


                try
                {
                    var chk9 = row.Cells[9].Controls[0] as CheckBox;
                    chk9.Checked = val;

                }
                catch
                {

                }

                try
                {
                    var chk10 = row.Cells[10].Controls[0] as CheckBox;
                    chk10.Checked = val;

                }
                catch
                {

                }


                try
                {

                    var chk11 = row.Cells[11].Controls[0] as CheckBox;
                    chk11.Checked = val;

                }
                catch
                {

                }

                try
                {

                    var chk12 = row.Cells[12].Controls[0] as CheckBox;
                    chk12.Checked = val;

                }
                catch
                {

                }

                try
                {

                    var chk13 = row.Cells[13].Controls[0] as CheckBox;
                    chk13.Checked = val;
                }
                catch
                {

                }

                try
                {
                    var chk14 = row.Cells[14].Controls[0] as CheckBox;
                    chk14.Checked = val;

                }
                catch
                {

                }

                try
                {
                    var chk15 = row.Cells[15].Controls[0] as CheckBox;
                    chk15.Checked = val;

                }
                catch
                {

                }

                try
                {
                    var chk16 = row.Cells[16].Controls[0] as CheckBox;
                    chk16.Checked = val;

                }
                catch
                {

                }

                try
                {
                    var chk17 = row.Cells[17].Controls[0] as CheckBox;
                    chk17.Checked = val;

                }
                catch
                {

                }


                try
                {
                    var chk18 = row.Cells[18].Controls[0] as CheckBox;
                    chk18.Checked = val;

                }
                catch
                {

                }
                try
                {
                    var chk19 = row.Cells[19].Controls[0] as CheckBox;
                    chk19.Checked = val;

                }
                catch
                {

                }

                try
                {
                    var chk20 = row.Cells[20].Controls[0] as CheckBox;
                    chk20.Checked = val;
                }
                catch
                {
                }

                //================================================
                try
                {
                    var chk21 = row.Cells[21].Controls[0] as CheckBox;
                    chk21.Checked = val;
                }
                catch
                {
                }
                try
                {
                    var chk22 = row.Cells[22].Controls[0] as CheckBox;
                    chk22.Checked = val;
                }
                catch
                {
                }
                try
                {
                    var chk23 = row.Cells[23].Controls[0] as CheckBox;
                    chk23.Checked = val;
                }
                catch
                {
                }
                try
                {
                    var chk24 = row.Cells[24].Controls[0] as CheckBox;
                    chk24.Checked = val;
                }
                catch
                {
                }
                try
                {
                    var chk25 = row.Cells[25].Controls[0] as CheckBox;
                    chk25.Checked = val;
                }
                catch
                {
                }
                try
                {
                    var chk26 = row.Cells[26].Controls[0] as CheckBox;
                    chk26.Checked = val;
                }
                catch
                {
                }
                try
                {
                    var chk27 = row.Cells[27].Controls[0] as CheckBox;
                    chk27.Checked = val;
                }
                catch
                {
                }
                try
                {
                    var chk28 = row.Cells[28].Controls[0] as CheckBox;
                    chk28.Checked = val;
                }
                catch
                {
                }
                try
                {
                    var chk29 = row.Cells[29].Controls[0] as CheckBox;
                    chk29.Checked = val;
                }
                catch
                {
                }
                try
                {
                    var chk30 = row.Cells[30].Controls[0] as CheckBox;
                    chk30.Checked = val;
                }
                catch
                {
                }
            }

            scrpt = "<script> click_empty();</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }

        protected void btn_uncheck_all_Click(object sender, EventArgs e)
        {
            bool val = false;
            bind_data_chek_and_unchek(val);
        }





        //====================================

        private void check_mapped_std()
        {
            try
            {
                foreach (GridViewRow row in GrdView_data.Rows)
                {
                    string admission_no = row.Cells[1].Text;

                    Dictionary<string, object> dc1 = My.get_selected_studentinfo_SubjMap(admission_no, ddlsession.SelectedValue, ViewState["branchid"].ToString());
                    string Class_Id = (String)dc1["Class_id"];
                    string section = (String)dc1["Section"];

                    //try
                    //{
                    //    string cell3 = row.Cells[3].Text;
                    //    var chk3 = row.Cells[3].Controls[0] as CheckBox;
                    //    string check_subj = find_allocated_subj(admission_no, cell3, Class_Id, section);
                    //    if (check_subj == "1")
                    //    {
                    //        chk3.Checked = true;
                    //    }
                    //}
                    //catch
                    //{
                    //}

                    //====================================
                    try
                    {
                        string cell4 = row.Cells[4].Text;
                        var chk4 = row.Cells[4].Controls[0] as CheckBox;
                        string check_subj = find_allocated_subj(admission_no, cell4, Class_Id, section);
                        if (check_subj == "1")
                        {
                            chk4.Checked = true;
                        }
                    }
                    catch
                    {
                    }
                    //====================================
                    try
                    {
                        string cell5 = row.Cells[5].Text;
                        var chk5 = row.Cells[5].Controls[0] as CheckBox;
                        string check_subj = find_allocated_subj(admission_no, cell5, Class_Id, section);
                        if (check_subj == "1")
                        {
                            chk5.Checked = true;
                        }
                    }
                    catch
                    {
                    }
                    //====================================
                    try
                    {
                        string cell6 = row.Cells[6].Text;
                        var chk6 = row.Cells[6].Controls[0] as CheckBox;
                        string check_subj = find_allocated_subj(admission_no, cell6, Class_Id, section);
                        if (check_subj == "1")
                        {
                            chk6.Checked = true;
                        }
                    }
                    catch
                    {
                    }

                    //====================================
                    try
                    {
                        string cell7 = row.Cells[7].Text;
                        var chk7 = row.Cells[7].Controls[0] as CheckBox;
                        string check_subj = find_allocated_subj(admission_no, cell7, Class_Id, section);
                        if (check_subj == "1")
                        {
                            chk7.Checked = true;
                        }
                    }
                    catch
                    {
                    }
                    //====================================
                    try
                    {
                        string cell8 = row.Cells[8].Text;
                        var chk8 = row.Cells[8].Controls[0] as CheckBox;
                        string check_subj = find_allocated_subj(admission_no, cell8, Class_Id, section);
                        if (check_subj == "1")
                        {
                            chk8.Checked = true;
                        }
                    }
                    catch
                    {
                    }

                    //====================================
                    try
                    {
                        string cell9 = row.Cells[9].Text;
                        var chk9 = row.Cells[9].Controls[0] as CheckBox;
                        string check_subj = find_allocated_subj(admission_no, cell9, Class_Id, section);
                        if (check_subj == "1")
                        {
                            chk9.Checked = true;
                        }
                    }
                    catch
                    {
                    }

                    try
                    {
                        string cell10 = row.Cells[10].Text;
                        var chk10 = row.Cells[10].Controls[0] as CheckBox;
                        string check_subj = find_allocated_subj(admission_no, cell10, Class_Id, section);
                        if (check_subj == "1")
                        {
                            chk10.Checked = true;
                        }
                    }
                    catch
                    {
                    }
                    try
                    {
                        string cell11 = row.Cells[11].Text;
                        var chk11 = row.Cells[11].Controls[0] as CheckBox;
                        string check_subj = find_allocated_subj(admission_no, cell11, Class_Id, section);
                        if (check_subj == "1")
                        {
                            chk11.Checked = true;
                        }
                    }
                    catch
                    {
                    }
                    try
                    {
                        string cell12 = row.Cells[12].Text;
                        var chk12 = row.Cells[12].Controls[0] as CheckBox;
                        string check_subj = find_allocated_subj(admission_no, cell12, Class_Id, section);
                        if (check_subj == "1")
                        {
                            chk12.Checked = true;
                        }
                    }
                    catch
                    {
                    }
                    try
                    {
                        string cell13 = row.Cells[13].Text;
                        var chk13 = row.Cells[13].Controls[0] as CheckBox;
                        string check_subj = find_allocated_subj(admission_no, cell13, Class_Id, section);
                        if (check_subj == "1")
                        {
                            chk13.Checked = true;
                        }
                    }
                    catch
                    {
                    }
                    try
                    {
                        string cell14 = row.Cells[14].Text;
                        var chk14 = row.Cells[14].Controls[0] as CheckBox;
                        string check_subj = find_allocated_subj(admission_no, cell14, Class_Id, section);
                        if (check_subj == "1")
                        {
                            chk14.Checked = true;
                        }
                    }
                    catch
                    {
                    }
                    try
                    {
                        string cell15 = row.Cells[15].Text;
                        var chk15 = row.Cells[15].Controls[0] as CheckBox;
                        string check_subj = find_allocated_subj(admission_no, cell15, Class_Id, section);
                        if (check_subj == "1")
                        {
                            chk15.Checked = true;
                        }
                    }
                    catch
                    {
                    }
                    try
                    {
                        string cell16 = row.Cells[16].Text;
                        var chk16 = row.Cells[16].Controls[0] as CheckBox;
                        string check_subj = find_allocated_subj(admission_no, cell16, Class_Id, section);
                        if (check_subj == "1")
                        {
                            chk16.Checked = true;
                        }
                    }
                    catch
                    {
                    }
                    try
                    {
                        string cell17 = row.Cells[17].Text;
                        var chk17 = row.Cells[17].Controls[0] as CheckBox;
                        string check_subj = find_allocated_subj(admission_no, cell17, Class_Id, section);
                        if (check_subj == "1")
                        {
                            chk17.Checked = true;
                        }
                    }
                    catch
                    {
                    }
                    try
                    {
                        string cell18 = row.Cells[18].Text;
                        var chk18 = row.Cells[18].Controls[0] as CheckBox;
                        string check_subj = find_allocated_subj(admission_no, cell18, Class_Id, section);
                        if (check_subj == "1")
                        {
                            chk18.Checked = true;
                        }
                    }
                    catch
                    {
                    }

                    try
                    {
                        string cell19 = row.Cells[19].Text;
                        var chk19 = row.Cells[19].Controls[0] as CheckBox;
                        string check_subj = find_allocated_subj(admission_no, cell19, Class_Id, section);
                        if (check_subj == "1")
                        {
                            chk19.Checked = true;
                        }
                    }
                    catch
                    {
                    }
                    try
                    {
                        string cell20 = row.Cells[20].Text;
                        var chk20 = row.Cells[20].Controls[0] as CheckBox;
                        string check_subj = find_allocated_subj(admission_no, cell20, Class_Id, section);
                        if (check_subj == "1")
                        {
                            chk20.Checked = true;
                        }
                    }
                    catch
                    {
                    }
                    try
                    {
                        string cell21 = row.Cells[21].Text;
                        var chk21 = row.Cells[21].Controls[0] as CheckBox;
                        string check_subj = find_allocated_subj(admission_no, cell21, Class_Id, section);
                        if (check_subj == "1")
                        {
                            chk21.Checked = true;
                        }
                    }
                    catch
                    {
                    }
                    try
                    {
                        string cell22 = row.Cells[22].Text;
                        var chk22 = row.Cells[22].Controls[0] as CheckBox;
                        string check_subj = find_allocated_subj(admission_no, cell22, Class_Id, section);
                        if (check_subj == "1")
                        {
                            chk22.Checked = true;
                        }
                    }
                    catch
                    {
                    }
                    try
                    {
                        string cell23 = row.Cells[23].Text;
                        var chk23 = row.Cells[23].Controls[0] as CheckBox;
                        string check_subj = find_allocated_subj(admission_no, cell23, Class_Id, section);
                        if (check_subj == "1")
                        {
                            chk23.Checked = true;
                        }
                    }
                    catch
                    {
                    }
                    try
                    {
                        string cell24 = row.Cells[24].Text;
                        var chk24 = row.Cells[24].Controls[0] as CheckBox;
                        string check_subj = find_allocated_subj(admission_no, cell24, Class_Id, section);
                        if (check_subj == "1")
                        {
                            chk24.Checked = true;
                        }
                    }
                    catch
                    {
                    }
                    try
                    {
                        string cell25 = row.Cells[25].Text;
                        var chk25 = row.Cells[25].Controls[0] as CheckBox;
                        string check_subj = find_allocated_subj(admission_no, cell25, Class_Id, section);
                        if (check_subj == "1")
                        {
                            chk25.Checked = true;
                        }
                    }
                    catch
                    {
                    }
                    try
                    {
                        string cell26 = row.Cells[26].Text;
                        var chk26 = row.Cells[26].Controls[0] as CheckBox;
                        string check_subj = find_allocated_subj(admission_no, cell26, Class_Id, section);
                        if (check_subj == "1")
                        {
                            chk26.Checked = true;
                        }
                    }
                    catch
                    {
                    }
                    try
                    {
                        string cell27 = row.Cells[27].Text;
                        var chk27 = row.Cells[27].Controls[0] as CheckBox;
                        string check_subj = find_allocated_subj(admission_no, cell27, Class_Id, section);
                        if (check_subj == "1")
                        {
                            chk27.Checked = true;
                        }
                    }
                    catch
                    {
                    }
                    try
                    {
                        string cell28 = row.Cells[28].Text;
                        var chk28 = row.Cells[28].Controls[0] as CheckBox;
                        string check_subj = find_allocated_subj(admission_no, cell28, Class_Id, section);
                        if (check_subj == "1")
                        {
                            chk28.Checked = true;
                        }
                    }
                    catch
                    {
                    }

                    try
                    {
                        string cell29 = row.Cells[29].Text;
                        var chk29 = row.Cells[29].Controls[0] as CheckBox;
                        string check_subj = find_allocated_subj(admission_no, cell29, Class_Id, section);
                        if (check_subj == "1")
                        {
                            chk29.Checked = true;
                        }
                    }
                    catch
                    {
                    }

                    try
                    {
                        string cell30 = row.Cells[30].Text;
                        var chk30 = row.Cells[30].Controls[0] as CheckBox;
                        string check_subj = find_allocated_subj(admission_no, cell30, Class_Id, section);
                        if (check_subj == "1")
                        {
                            chk30.Checked = true;
                        }
                    }
                    catch
                    {
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private string find_allocated_subj(string admission_no, string subject_id, string class_Id, string section)
        {
            string returN = "0";
            string query = "select Admission_no from Subject_Mapping_New where Session_id='" + ddlsession.SelectedValue + "' and Admission_no='" + admission_no + "' and Sub_id=" + subject_id + " and Section='" + section + "' and Class_id=" + class_Id + " and Branch_id='" + ViewState["branchid"].ToString() + "'";
            DataTable dt = My.dataTable(query);
            if (dt.Rows.Count == 0)
            {
                return returN;
            }
            else
            {
                returN = "1";
                return returN;
            }
        }

    }
}