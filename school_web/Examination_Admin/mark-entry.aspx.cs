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
    public partial class mark_entry1 : System.Web.UI.Page
    {
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

        UsesCode mycode = new UsesCode();
        My my = new My();
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
                    ViewState["Userid"] = Session["Admin"].ToString();

                    ViewState["Branchid"] = mycode.getbranchid(Session["Admin"].ToString());
                    mycode.bind_all_ddl_with_id(ddlsession, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                    ddlsession.SelectedValue = My.get_session_id();

                    mycode.bind_all_ddl_with_id(ddl_CourseCat, "Select   Course_Name, course_id from Add_course_table order by Position asc");

                    mycode.bind_ddl(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddl_CourseCat.SelectedValue + "'  order by Section");

                    mycode.bind_all_ddl_with_id(ddl_remarks, "select Short_Name,Exam_Marks_Entry_Label_Id from Exam_Marks_Entry_Label where Branch_Id='" + ViewState["Branchid"].ToString() + "' order by Short_Name asc");

                    find_firm_details();
                }
            }
        }


        private void find_firm_details()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                imglogo.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();
            }
        }


        private void get_max_marks()
        {
            string query = "select Maximum_Marks from Exam_Subject_Sub_Level where Session_Id='" + ddlsession.SelectedValue + "' and Branch_Id='" + ViewState["Branchid"].ToString() + "' and Exam_Term_Id='" + ddl_term.SelectedValue + "' and Assessment_Id='" + ddl_assesment.SelectedValue + "' and Class_id=" + ddl_CourseCat.SelectedValue + " and Subject_id=" + ddl_subject.SelectedValue + " and Subject_Sub_Level_Id=" + ddl_exam_level.SelectedValue + " ";
            DataTable dt = mycode.FillTable(query);
            if (dt.Rows.Count != 0)
            {
                lbl_activity_type.Text = ddl_exam_level.SelectedItem.Text + " (" + dt.Rows[0]["Maximum_Marks"].ToString() + ")";
                txt_mm.Text = dt.Rows[0]["Maximum_Marks"].ToString();
            }
        }
        protected void ddl_CourseCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                RPDetails.DataSource = null;
                RPDetails.DataBind();
                if (ddl_CourseCat.SelectedItem.Text == "Select")
                {

                    Alertme("Please select class.", "warning");

                }
                else
                {
                    mycode.bind_ddl(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddl_CourseCat.SelectedValue + "'  order by Section");

                    mycode.bind_all_ddl_with_id(ddl_term, "select Term_Name,Exam_Term_Id from Exam_Term_Details where Class_id='" + ddl_CourseCat.SelectedValue + "' and Session_Id='" + ddlsession.SelectedValue + "' and Branch_Id='" + ViewState["Branchid"].ToString() + "' order by Sequence_No asc");
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void ddl_section_SelectedIndexChanged(object sender, EventArgs e)
        {
            RPDetails.DataSource = null;
            RPDetails.DataBind();
            if (ddl_CourseCat.SelectedItem.Text == "Select")
            {
                Alertme("Please select class.", "warning");
            }
            else if (ddl_section.Text == "Select")
            {
                Alertme("Please select section.", "warning");
            }
            else
            {
                mycode.bind_all_ddl_with_id(ddl_subject, "Select distinct sm.Subject_name,sm.Subject_id,sm.Subject_position from Subject_Master sm where sm.course_id='" + ddl_CourseCat.SelectedValue + "' and sm.Is_mandatory=1 order by sm.Subject_position  ");
            }
        }

        protected void ddl_term_SelectedIndexChanged(object sender, EventArgs e)
        {
            RPDetails.DataSource = null;
            RPDetails.DataBind();
            mycode.bind_all_ddl_with_id(ddl_assesment, "select Assessment_Name,Assessment_Id from Exam_Assessment_Details where Class_id='" + ddl_CourseCat.SelectedValue + "' and Exam_Term_Id='" + ddl_term.SelectedValue + "' and Session_Id='" + ddlsession.SelectedValue + "' and Branch_Id='" + ViewState["Branchid"].ToString() + "' order by Assessment_Name asc");
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                RPDetails.DataSource = null;
                RPDetails.DataBind();
                if (ddl_section.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session.", "warning");
                    ddl_section.Focus();
                }
                else if (ddl_CourseCat.SelectedItem.Text == "Select")
                {
                    Alertme("Please select class.", "warning");
                    ddl_CourseCat.Focus();
                }
                else if (ddl_section.SelectedItem.Text == "Select")
                {
                    Alertme("Please select section.", "warning");
                    ddl_section.Focus();
                }
                else if (ddl_term.SelectedItem.Text == "Select")
                {
                    Alertme("Please select term.", "warning");
                    ddl_term.Focus();
                }
                else if (ddl_assesment.SelectedItem.Text == "Select")
                {
                    Alertme("Please select Assessment.", "warning");
                    ddl_assesment.Focus();
                }
                else if (ddl_subject.SelectedItem.Text == "Select")
                {
                    Alertme("Please select subject.", "warning");
                    ddl_subject.Focus();
                }
                else if (ddl_exam_level.SelectedItem.Text == "Select")
                {
                    Alertme("Please select subject activity.", "warning");
                    ddl_exam_level.Focus();
                }
                else
                {
                    lbl_subject.Text = "Marks sheet for Class : " + ddl_CourseCat.SelectedItem.Text + ",  Section : " + ddl_section.SelectedItem.Text + ", Term : " + ddl_term.SelectedItem.Text + ", Exam : " + ddl_assesment.SelectedItem.Text + ", Subject : " + ddl_subject.SelectedItem.Text + ", Subject Activity : " + ddl_exam_level.SelectedItem.Text;
                    find_students();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_students()
        {
            lbl_activity_type.Text = ddl_exam_level.SelectedItem.Text;
            get_max_marks();
            string query = "Select distinct ar.admissionserialnumber,ar.studentname,ar.rollnumber   from admission_registor ar join Subject_Mapping_New smn on ar.admissionserialnumber=smn.Admission_no and ar.Session_Id=smn.Session_id and ar.Branch_id=smn.Branch_id and ar.Class_Id=smn.Class_id where ar.Session_id='" + ddlsession.SelectedValue + "' and ar.Class_id='" + ddl_CourseCat.SelectedValue + "' and ar.Section='" + ddl_section.Text + "'  and ar.Branch_Id='" + ViewState["Branchid"].ToString() + "' and  smn.Sub_id='" + ddl_subject.SelectedValue + "' and  ar.StudentStatus!='TC' and  ar.Status='1' order by ar.rollnumber";

            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = mycode.FillTable(query);
            if (dt.Rows.Count == 0)
            {
                print1.Visible = false;
                RPDetails.DataSource = null;
                RPDetails.DataBind();
            }
            else
            {
                print1.Visible = true;
                RPDetails.DataSource = dt;
                RPDetails.DataBind();
            }
        }

        protected void ddl_subject_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                RPDetails.DataSource = null;
                RPDetails.DataBind();
                if (ddl_CourseCat.SelectedItem.Text == "Select")
                {
                    Alertme("Please select class.", "warning");
                    ddl_CourseCat.Focus();
                }
                else if (ddl_section.SelectedItem.Text == "Select")
                {
                    Alertme("Please select section.", "warning");
                    ddl_section.Focus();
                }
                else if (ddl_term.SelectedItem.Text == "Select")
                {
                    Alertme("Please select term.", "warning");
                    ddl_term.Focus();
                }
                else if (ddl_assesment.SelectedItem.Text == "Select")
                {
                    Alertme("Please select Assessment.", "warning");
                    ddl_assesment.Focus();
                }
                else if (ddl_subject.SelectedItem.Text == "Select")
                {
                    Alertme("Please select subject.", "warning");
                    ddl_subject.Focus();
                }
                else
                {
                    mycode.bind_all_ddl_with_id(ddl_exam_level, "select Subject_Activity_Name,Subject_Sub_Level_Id from Exam_Subject_Sub_Level where Session_Id='" + ddlsession.SelectedValue + "' and Branch_Id='" + ViewState["Branchid"].ToString() + "' and Exam_Term_Id='" + ddl_term.SelectedValue + "' and Assessment_Id='" + ddl_assesment.SelectedValue + "' and Class_id='" + ddl_CourseCat.SelectedValue + "' and Subject_id='" + ddl_subject.SelectedValue + "' order by Subject_Activity_Name asc");
                }
            }
            catch (Exception ex)
            {
            }
        }



        protected void lnk_remarks_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_adm_no = (Label)row.FindControl("lbl_adm_no");
                TextBox txt_marks = (TextBox)row.FindControl("txt_marks");
                ViewState["admNo"] = lbl_adm_no.Text;
                myModalSS.Visible = true;
            }
            catch (Exception ex)
            {
            }
        }

        protected void btn_save_rmrks_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_remarks.SelectedItem.Text == "Select")
                {
                    Alertme("please select remark type.", "warning");
                    myModalSS.Visible = true;
                }
                else
                {
                    int i;
                    int gridview_rowcount = RPDetails.Items.Count;
                    for (i = 0; i < gridview_rowcount; i++)
                    {
                        Label lbl_adm_no = (Label)RPDetails.Items[i].FindControl("lbl_adm_no");
                        TextBox txt_marks = (TextBox)RPDetails.Items[i].FindControl("txt_marks");
                        Label lbl_mark_ids = (Label)RPDetails.Items[i].FindControl("lbl_mark_ids");
                        if (lbl_adm_no.Text == ViewState["admNo"].ToString())
                        {
                            txt_marks.Text = ddl_remarks.SelectedItem.Text;
                            lbl_mark_ids.Text = ddl_remarks.SelectedValue;
                            myModalSS.Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_CourseCat.SelectedItem.Text == "Select")
                {
                    Alertme("Please select class.", "warning");
                    ddl_CourseCat.Focus();
                }
                else if (ddl_section.SelectedItem.Text == "Select")
                {
                    Alertme("Please select section.", "warning");
                    ddl_section.Focus();
                }
                else if (ddl_term.SelectedItem.Text == "Select")
                {
                    Alertme("Please select term.", "warning");
                    ddl_term.Focus();
                }
                else if (ddl_assesment.SelectedItem.Text == "Select")
                {
                    Alertme("Please select Assessment.", "warning");
                    ddl_assesment.Focus();
                }
                else if (ddl_subject.SelectedItem.Text == "Select")
                {
                    Alertme("Please select subject.", "warning");
                    ddl_subject.Focus();
                }
                else if (ddl_exam_level.SelectedItem.Text == "Select")
                {
                    Alertme("Please subject activity.", "warning");
                    ddl_exam_level.Focus();
                }
                else
                {
                    //int i;
                    //int gridview_rowcount = RPDetails.Items.Count;
                    //for (i = 0; i < gridview_rowcount; i++)
                    //{
                    //    TextBox txt_marks = (TextBox)RPDetails.Items[i].FindControl("txt_marks");
                    //    if (txt_marks.Text == "")
                    //    {
                    //        txt_marks.Focus();
                    //        txt_marks.CssClass += " grd-txtbx-clas txtbxError";
                    //        Alertme("Please fill marks in all the text box. Empty fields are not allowed.", "warning");
                    //        return;
                    //    }
                    //}


                    save_marks();
                    Alertme("Marks has been saved successfully.", "success");
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void save_marks()
        {
            string Is_character = "0";
            int i;
            int gridview_rowcount = RPDetails.Items.Count;
            for (i = 0; i < gridview_rowcount; i++)
            {
                Label lbl_adm_no = (Label)RPDetails.Items[i].FindControl("lbl_adm_no");
                TextBox txt_marks = (TextBox)RPDetails.Items[i].FindControl("txt_marks");
                Label lbl_mark_ids = (Label)RPDetails.Items[i].FindControl("lbl_mark_ids");

                string qrys = "delete from Exam_marks where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddl_CourseCat.SelectedValue + "' and Section='" + ddl_section.Text + "' and Term='" + ddl_term.SelectedValue + "' and Assessment='" + ddl_assesment.SelectedValue + "' and Subject='" + ddl_subject.SelectedValue + "' and Subject_activity='" + ddl_exam_level.SelectedValue + "' and Branch_id='" + ViewState["Branchid"].ToString() + "' and Admission_no='" + lbl_adm_no.Text + "'";
                My.exeSql(qrys);

                string valueinput = "";
                if (txt_marks.Text != "")
                {
                    valueinput = txt_marks.Text;

                    if (My.cheknum_fine(valueinput))
                    {
                        Is_character = "0";
                    }
                    else
                    {
                        Is_character = "1";
                    }
                }
                else
                {
                    valueinput = "";
                    Is_character = "0";
                }

                DataTable dt = mycode.FillData("select Id from Exam_marks where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddl_CourseCat.SelectedValue + "' and Section='" + ddl_section.Text + "' and Term='" + ddl_term.SelectedValue + "' and Assessment='" + ddl_assesment.SelectedValue + "' and Subject='" + ddl_subject.SelectedValue + "' and Subject_activity='" + ddl_exam_level.SelectedValue + "' and Admission_no='" + lbl_adm_no.Text + "' and Branch_id='" + ViewState["Branchid"].ToString() + "'");
                if (dt.Rows.Count == 0)
                {
                    if (valueinput == "")
                    {
                    }
                    else
                    {
                        SqlCommand cmd;
                        string query = "INSERT INTO Exam_marks (Session_id,Class_id,Section,Term,Assessment,Subject,Subject_activity,Admission_no,Marks,Branch_id,Created_by,Created_date,Created_idate,Mark_id,Is_character) values (@Session_id,@Class_id,@Section,@Term,@Assessment,@Subject,@Subject_activity,@Admission_no,@Marks,@Branch_id,@Created_by,@Created_date,@Created_idate,@Mark_id,@Is_character)";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@Session_id", ddlsession.SelectedValue);
                        cmd.Parameters.AddWithValue("@Class_id", ddl_CourseCat.SelectedValue);
                        cmd.Parameters.AddWithValue("@Section", ddl_section.Text);
                        cmd.Parameters.AddWithValue("@Term", ddl_term.SelectedValue);
                        cmd.Parameters.AddWithValue("@Assessment", ddl_assesment.SelectedValue);
                        cmd.Parameters.AddWithValue("@Subject", ddl_subject.SelectedValue);
                        cmd.Parameters.AddWithValue("@Subject_activity", ddl_exam_level.SelectedValue);
                        cmd.Parameters.AddWithValue("@Admission_no", lbl_adm_no.Text);
                        cmd.Parameters.AddWithValue("@Marks", valueinput);
                        cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branchid"].ToString());
                        cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                        cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                        cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
                        cmd.Parameters.AddWithValue("@Mark_id", lbl_mark_ids.Text);
                        cmd.Parameters.AddWithValue("@Is_character", Is_character);



                        if (My.InsertUpdateData(cmd))
                        {
                            mycode.executequery("update Exam_Subject_Sub_Level set  Is_save_marks=1 where Session_Id=" + ddlsession.SelectedValue + " and Branch_Id=" + ViewState["Branchid"].ToString() + " and Class_id=" + ddl_CourseCat.SelectedValue + " and Exam_Term_Id=" + ddl_term.SelectedValue + " and Assessment_Id=" + ddl_assesment.SelectedValue + " and Subject_id=" + ddl_subject.SelectedValue + " and Subject_Sub_Level_Id=" + ddl_exam_level.SelectedValue + "");
                            mycode.executequery("update Exam_marks set Is_save_marks=1 where Subject_activity=" + ddl_exam_level.SelectedValue + " and Session_id=" + ddlsession.SelectedValue + " and Branch_id='" + ViewState["Branchid"].ToString() + "' and Section='" + ddl_section.Text + "'");
                        }
                    }
                }
                else
                {
                    string id = dt.Rows[0]["Id"].ToString();

                    if (txt_marks.Text != "")
                    {
                        valueinput = txt_marks.Text;

                        if (My.cheknum_fine(valueinput))
                        {
                            Is_character = "0";
                        }
                        else
                        {
                            Is_character = "1";
                        }
                    }
                    else
                    {
                        valueinput = "";
                        Is_character = "0";
                    }
                    SqlCommand cmd;
                    string query = "Update Exam_marks set Marks=@Marks,Mark_id=@Mark_id,Is_character=@Is_character,Updated_by=@Updated_by,Updated_date=@Updated_date,Updated_idate=@Updated_idate where Id=" + id + " ";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Marks", valueinput);
                    cmd.Parameters.AddWithValue("@Mark_id", lbl_mark_ids.Text);
                    cmd.Parameters.AddWithValue("@Is_character", Is_character);
                    //if (lbl_mark_ids.Text == "0")
                    //{
                    //    cmd.Parameters.AddWithValue("@Is_character", "0");
                    //}
                    //else
                    //{
                    //    cmd.Parameters.AddWithValue("@Is_character", "1");
                    //}
                    cmd.Parameters.AddWithValue("@Updated_by", ViewState["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@Updated_date", mycode.date());
                    cmd.Parameters.AddWithValue("@Updated_idate", mycode.idate());
                    if (My.InsertUpdateData(cmd))
                    {
                        My.exeSql("update Exam_marks set Created_by='" + ViewState["Userid"].ToString() + "' where id=" + id + " and Created_by='AI'");
                        mycode.executequery("update Exam_Subject_Sub_Level set  Is_save_marks=1 where Session_Id=" + ddlsession.SelectedValue + " and Branch_Id=" + ViewState["Branchid"].ToString() + " and Class_id=" + ddl_CourseCat.SelectedValue + " and Exam_Term_Id=" + ddl_term.SelectedValue + " and Assessment_Id=" + ddl_assesment.SelectedValue + " and Subject_id=" + ddl_subject.SelectedValue + " and Subject_Sub_Level_Id=" + ddl_exam_level.SelectedValue + "");
                        mycode.executequery("update Exam_marks set Is_save_marks=1 where Subject_activity=" + ddl_exam_level.SelectedValue + " and Session_id=" + ddlsession.SelectedValue + " and Branch_id='" + ViewState["Branchid"].ToString() + "' and Section='" + ddl_section.Text + "'");
                    }
                }

            }
        }


        protected void lnk_close_popup_Click(object sender, EventArgs e)
        {
            myModalSS.Visible = false;
        }

        protected void RPDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbl_adm_no = ((Label)e.Item.FindControl("lbl_adm_no")) as Label;
                TextBox txt_marks = ((TextBox)e.Item.FindControl("txt_marks")) as TextBox;
                Label lbl_mark_ids = ((Label)e.Item.FindControl("lbl_mark_ids")) as Label;
                DataTable dt = mycode.FillData("select * from Exam_marks where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddl_CourseCat.SelectedValue + "' and Section='" + ddl_section.Text + "' and Term='" + ddl_term.SelectedValue + "' and Assessment='" + ddl_assesment.SelectedValue + "' and Subject='" + ddl_subject.SelectedValue + "' and Subject_activity='" + ddl_exam_level.SelectedValue + "' and Admission_no='" + lbl_adm_no.Text + "' and Branch_id='" + ViewState["Branchid"].ToString() + "' and Created_by!='AI'");
                if (dt.Rows.Count == 0)
                {
                    txt_marks.Text = "";
                    lbl_mark_ids.Text = "0";
                }
                else
                {
                    txt_marks.Text = dt.Rows[0]["Marks"].ToString();
                    lbl_mark_ids.Text = dt.Rows[0]["Mark_id"].ToString();
                }
            }
        }

        protected void ddl_assesment_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddl_CourseCat.SelectedItem.Text == "Select")
            {
                Alertme("Please select class.", "warning");
                ddl_CourseCat.Focus();
            }
            else if (ddl_section.SelectedItem.Text == "Select")
            {
                Alertme("Please select section.", "warning");
                ddl_section.Focus();
            }
            else if (ddl_term.SelectedItem.Text == "Select")
            {
                Alertme("Please select term.", "warning");
                ddl_term.Focus();
            }
            else if (ddl_assesment.SelectedItem.Text == "Select")
            {
                Alertme("Please select Assessment.", "warning");
                ddl_assesment.Focus();
            }
            else
            {
                mycode.bind_all_ddl_with_id(ddl_subject, "Select distinct sm.Subject_name,sm.Subject_id,sm.Subject_position from Subject_Master sm join Exam_Assessment_Subject_Mapping_Details easm on sm.Subject_id=easm.Subject_id where sm.course_id='" + ddl_CourseCat.SelectedValue + "' and easm.Session_Id=" + ddlsession.SelectedValue + " and easm.Branch_Id=" + ViewState["Branchid"].ToString() + " and easm.Exam_Term_Id=" + ddl_term.SelectedValue + " and easm.Assessment_Id=" + ddl_assesment.SelectedValue + "  order by sm.Subject_position  ");
            }

        }
    }
}