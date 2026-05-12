using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class add_subject : System.Web.UI.Page
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
                        ViewState["courseID"] = "0";
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();
                        ViewState["branchid"] = Session["branchid"].ToString();
                        mycode.bind_all_ddl_with_id_cap_All(ddl_course_search, "Select Course_Name,course_id from Add_course_table order by Position");
                        ddl_course_search.SelectedValue = My.get_top_one_class();

                        string pagename_current = Path.GetFileName(Request.Path);
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];


                        Bind_class_details();

                        if (ViewState["Is_Print"].ToString() == "1")
                        {
                            print1.Visible = true;
                        }
                        else
                        {
                            print1.Visible = false;
                        }
                        Bind_All_subject();

                    }
                    // BindDetails();
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_User");
            }

        }

        private void Bind_class_details()
        {
            DataTable dt = mycode.FillData("Select Course_Name,course_id from Add_course_table order by Position");
            if (dt.Rows.Count == 0)
            {
                rd_class.DataSource = null;
                rd_class.DataBind();
            }
            else
            {
                rd_class.DataSource = dt;
                rd_class.DataBind();
            }
        }
        protected void chk_all_CheckedChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            for (int j = 0; j < rd_class.Items.Count; j++)
            {
                CheckBox chk_class = rd_class.Items[j].FindControl("chk_class") as CheckBox;
                if (chk_all.Checked)
                {
                    chk_class.Checked = true;
                }
                else
                {
                    chk_class.Checked = false;
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

        private void Bind_All_subject()
        {
            string query = "";
            if (ddl_course_search.SelectedItem.Text == "ALL")
            {
                query = "Select sm.*,cm.Course_Name from Subject_Master sm join Add_course_table cm on cm.course_id=sm.course_id where sm.Branch_id='" + ViewState["branchid"].ToString() + "' order BY sm.Subject_position";
                finalgride(query);
            }
            else
            {
                query = "Select sm.*,cm.Course_Name from Subject_Master sm join Add_course_table cm on cm.course_id=sm.course_id where cm.course_id=" + ddl_course_search.SelectedValue + " and sm.Branch_id='" + ViewState["branchid"].ToString() + "' order BY sm.Subject_position";
                finalgride(query);
            }
        }

        public void finalgride(string query)
        {
            ViewState["query"] = query;
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no subject list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_Subject_id = (Label)row.FindControl("lbl_Subject_id");
                    Label lbl_Class_id = (Label)row.FindControl("lbl_Class_id");
                    Label lbl_Subject_name = (Label)row.FindControl("lbl_Subject_name");
                    Label lbl_Subject_type = (Label)row.FindControl("lbl_Subject_type");


                    Label lbl_Subject_Type_Scholastic_Co_Scholastic = (Label)row.FindControl("lbl_Subject_Type_Scholastic_Co_Scholastic");
                    Label lbl_Subject_Short_Name = (Label)row.FindControl("lbl_Subject_Short_Name");
                    Label lbl_Subject_Code = (Label)row.FindControl("lbl_Subject_Code");


                    txt_sujectshortname.Text = lbl_Subject_Short_Name.Text;
                    txt_sujectcode.Text = lbl_Subject_Code.Text;

                    ViewState["courseID"] = lbl_Class_id.Text;
                    Bind_class_details();

                    try
                    {
                        if (lbl_Subject_Type_Scholastic_Co_Scholastic.Text == "")
                        {
                            chk_mandatory.Checked = false;
                        }
                        else if (lbl_Subject_Type_Scholastic_Co_Scholastic.Text == "0")
                        {
                            chk_mandatory.Checked = false;
                        }
                        else
                        {
                            chk_mandatory.Checked = true;
                        }
                    }
                    catch
                    {
                    }
                    txt_subject.Text = lbl_Subject_name.Text;
                    hd_subjectid.Value = lbl_Subject_id.Text;
                    btn_cancel.Visible = true;
                    btn_Submit.Text = "Update";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
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

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_delete"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_Subject_id = (Label)row.FindControl("lbl_Subject_id");
                    mycode.executequery("delete from Subject_Master where Subject_id='" + lbl_Subject_id.Text + "' ");
                    finalgride(ViewState["query"].ToString());
                    txt_subject.Text = "";
                    txt_subjectposition.Text = "";
                    txt_sujectcode.Text = "";
                    txt_sujectshortname.Text = "";
                    chk_mandatory.Checked = false;

                    btn_cancel.Visible = false;
                    btn_Submit.Text = "Add";
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


        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            int growcount = rd_class.Items.Count;
            int k = 0;
            for (int ix = 0; ix < growcount; ix++)
            {
                CheckBox chk = (CheckBox)rd_class.Items[ix].FindControl("chk_class");
                if (chk.Checked == true)
                {
                }
                else
                {
                    k++;
                }
            }
            if (k == growcount)
            {
                Alertme("Please check minimum one course.", "warning");
                return;
            }



            if (txt_subject.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                txt_subject.Focus();
                Alertme("Please enter subject name.", "warning");
            }
            else
            {
                try
                {
                    DataTable dtConf = mycode.FillData("select Password from Confirmation_setting where Page='AddSubject' and Is_confirmation=1");
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

                ViewState["IsSuccess"] = "0";
                add_update_subjects();

                if (ViewState["IsSuccess"].ToString() == "1")
                {
                    if (btn_Submit.Text == "Add")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                        txt_subject.Text = "";
                        txt_subjectposition.Text = "";
                        Alertme("Subject has been sucessfully added", "success");
                        
                        btn_cancel.Visible = false;
                        txt_sujectcode.Text = "";
                        txt_sujectshortname.Text = "";
                        chk_mandatory.Checked = false;
                        btn_Submit.Text = "Add";
                        Bind_All_subject();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                        txt_subject.Text = "";
                        txt_subjectposition.Text = "";
                        Alertme("Subject has been sucessfully updated", "success");
                       
                        txt_subject.Text = "";
                        txt_subjectposition.Text = "";
                        txt_sujectcode.Text = "";
                        txt_sujectshortname.Text = "";
                        chk_mandatory.Checked = false;
                        btn_cancel.Visible = false;
                        btn_Submit.Text = "Add";
                        Bind_All_subject();
                    }
                }
            }
        }

        private void add_update_subjects()
        {
            string mainstring = txt_subject.Text.Trim();
            string result2 = "";
            string[] multiArray = mainstring.Split(new Char[] { ' ', '&', '.', '-', '(', ')' });
            String seperator2 = "_";
            foreach (string author in multiArray)
            {
                if (author.Trim() != "")
                {
                    result2 += author + "_";
                }

            }
            string Something = result2.TrimEnd('_');

            int mandatory = 0;
            if (chk_mandatory.Checked == true)
            {
                mandatory = 1;
            }
            else
            {
                mandatory = 0;
            }


            if (btn_Submit.Text == "Add")
            {
                if (ViewState["Is_add"].ToString() == "1")
                {
                    int growcount = rd_class.Items.Count;
                    int k = 0;
                    for (int ix = 0; ix < growcount; ix++)
                    {
                        CheckBox chk = (CheckBox)rd_class.Items[ix].FindControl("chk_class");
                        if (chk.Checked == true)
                        {
                            Label lbl_class_id = (Label)rd_class.Items[ix].FindControl("lbl_class_id");
                            SqlCommand cmd;
                            DataTable dt = mycode.FillData("Select * from Subject_Master where course_id='" + lbl_class_id.Text + "' and Subject_name='" + txt_subject.Text.Trim() + "' and  Branch_id='" + ViewState["branchid"].ToString() + "' and Subject_Type_Scholastic_Co_Scholastic='" + ddl_subjecttype.Text + "'");
                            if (dt.Rows.Count == 0)
                            {
                                int position_id = get_position_id(lbl_class_id.Text);
                                string subjectid = create_subjectid();
                                string query = " INSERT INTO Subject_Master (course_id,Subject_id,Branch_id,Subject_name,Subject_type,Subject_position,Date,Idate,Time,Created_by,subject2,Subject_Code,Subject_Short_Name,Subject_Type_Scholastic_Co_Scholastic,Is_mandatory) values (@course_id,@Subject_id,@Branch_id,@Subject_name,@Subject_type,@Subject_position,@Date,@Idate,@Time,@Created_by,@subject2,@Subject_Code,@Subject_Short_Name,@Subject_Type_Scholastic_Co_Scholastic,@Is_mandatory)";
                                cmd = new SqlCommand(query);
                                cmd.Parameters.AddWithValue("@course_id", lbl_class_id.Text);
                                cmd.Parameters.AddWithValue("@Subject_id", subjectid);
                                cmd.Parameters.AddWithValue("@Branch_id", ViewState["branchid"].ToString());
                                cmd.Parameters.AddWithValue("@Subject_name", txt_subject.Text.Trim());
                                cmd.Parameters.AddWithValue("@Subject_type", ddl_subjecttype.Text);
                                cmd.Parameters.AddWithValue("@Subject_position", position_id);
                                cmd.Parameters.AddWithValue("@Date", mycode.date());
                                cmd.Parameters.AddWithValue("@Idate", mycode.idate());
                                cmd.Parameters.AddWithValue("@Time", mycode.time());
                                cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                                cmd.Parameters.AddWithValue("@subject2", Something);

                                cmd.Parameters.AddWithValue("@Subject_Code", txt_sujectcode.Text);
                                cmd.Parameters.AddWithValue("@Subject_Short_Name", txt_sujectshortname.Text);
                                cmd.Parameters.AddWithValue("@Subject_Type_Scholastic_Co_Scholastic", ddl_subjecttype.Text);
                                cmd.Parameters.AddWithValue("@Is_mandatory", mandatory);
                                if (My.InsertUpdateData(cmd))
                                {
                                    ViewState["IsSuccess"] = "1";
                                    string msg = ViewState["Userid"].ToString() + " Create Subject, subject id=" + subjectid + " subject name=" + txt_subject.Text.Trim() + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MM/yyyy hh:mm:ss tt");
                                    mycode.insert_data_logfile(ViewState["Userid"].ToString(), ViewState["firm_id"].ToString(), msg, ViewState["branchid"].ToString());
                                }
                            }
                            else
                            {
                                Alertme("Sorry your subject name already exit ", "warning");
                            }
                        }
                        else
                        {
                            k++;
                        }
                    }
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            else
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    int growcount = rd_class.Items.Count;
                    int k = 0;
                    for (int ix = 0; ix < growcount; ix++)
                    {
                        CheckBox chk = (CheckBox)rd_class.Items[ix].FindControl("chk_class");
                        if (chk.Checked == true)
                        {
                            Label lbl_class_id = (Label)rd_class.Items[ix].FindControl("lbl_class_id");
                            SqlCommand cmd;
                            DataTable dt = mycode.FillData("Select * from Subject_Master where course_id='" + lbl_class_id.Text + "' and Subject_name='" + txt_subject.Text.Trim() + "' and  Branch_id='" + ViewState["branchid"].ToString() + "' and Subject_Type_Scholastic_Co_Scholastic='" + ddl_subjecttype.Text + "' and Subject_id!=" + hd_subjectid.ValidateRequestMode + " ");
                            if (dt.Rows.Count == 0)
                            {
                                string query = "Update Subject_Master set course_id=@course_id,Subject_id=@Subject_id,Branch_id=@Branch_id,Subject_name=@Subject_name,Subject_type=@Subject_type ,Date=@Date,Idate=@Idate,Time=@Time,Subject_Code=@Subject_Code,Subject_Short_Name=@Subject_Short_Name,Subject_Type_Scholastic_Co_Scholastic=@Subject_Type_Scholastic_Co_Scholastic,Is_mandatory=@Is_mandatory where Subject_id = @Subject_id";
                                cmd = new SqlCommand(query);
                                cmd.Parameters.AddWithValue("@course_id", lbl_class_id.Text);
                                cmd.Parameters.AddWithValue("@Subject_id", hd_subjectid.Value);
                                cmd.Parameters.AddWithValue("@Branch_id", ViewState["branchid"].ToString());
                                cmd.Parameters.AddWithValue("@Subject_name", txt_subject.Text.Trim());
                                cmd.Parameters.AddWithValue("@Subject_type", ddl_subjecttype.Text);
                                cmd.Parameters.AddWithValue("@Date", mycode.date());
                                cmd.Parameters.AddWithValue("@Idate", mycode.idate());
                                cmd.Parameters.AddWithValue("@Time", mycode.time());
                                cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                                cmd.Parameters.AddWithValue("@Subject_Code", txt_sujectcode.Text);
                                cmd.Parameters.AddWithValue("@Subject_Short_Name", txt_sujectshortname.Text);
                                cmd.Parameters.AddWithValue("@Subject_Type_Scholastic_Co_Scholastic", ddl_subjecttype.Text);
                                cmd.Parameters.AddWithValue("@Is_mandatory", mandatory);
                                if (My.InsertUpdateData(cmd))
                                {
                                    ViewState["IsSuccess"] = "1";
                                    string msg = ViewState["Userid"].ToString() + " Create Subject, subject id=" + hd_subjectid.Value + " subject name=" + txt_subject.Text.Trim() + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MM/yyyy hh:mm:ss tt");
                                    mycode.insert_data_logfile(ViewState["Userid"].ToString(), ViewState["firm_id"].ToString(), msg, ViewState["branchid"].ToString());
                                }
                            }
                            else
                            {
                                Alertme("Sorry your subject name already exit ", "warning");
                            }
                        }
                        else
                        {
                            k++;
                        }
                    }
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
        }

        private int get_position_id(string class_id)
        {
            DataTable dt = mycode.FillData("Select top 1 Subject_position  from Subject_Master where course_id='" + class_id + "' order by Subject_position desc");
            if (dt.Rows.Count == 0)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(dt.Rows[0][0].ToString()) + 1;
            }
        }

        private string create_subjectid()
        {
            bool duplicate = false;
            string subject_id = mycode.auto_serial("subject_id");
            while (!duplicate)
            {
                DataTable cdt = mycode.FillData("  select Subject_id from dbo.[Subject_Master] where Subject_id='" + subject_id + "'");
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    duplicate = true;
                }
                else
                {
                    duplicate = false;
                    subject_id = mycode.auto_serial("subject_id");
                }
            }
            return subject_id;
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            txt_subject.Text = "";
            txt_subjectposition.Text = "";
            btn_cancel.Visible = false;
            btn_Submit.Text = "Add";
        }

        protected void ddl_course_search_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_course_search.SelectedItem.Text == "ALL")
            {
                Bind_All_subject();
            }
            else
            {
                Bind_All_subject();
            }
        }
        protected void ddl_course_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_course_search.SelectedItem.Text == "ALL" && ddl_course_type.SelectedItem.Text == "ALL")
            {
                Bind_All_subject();
            }
            else if (ddl_course_search.SelectedItem.Text == "ALL" && ddl_course_type.SelectedItem.Text != "ALL")
            {
                find_by_subject_only();
            }
            else if (ddl_course_search.SelectedItem.Text != "ALL" && ddl_course_type.SelectedItem.Text == "ALL")
            {
                find_by_class_subject_all();
            }
            else
            {
                string query = "Select sm.*,cm.Course_Name from Subject_Master sm join Add_course_table cm on cm.course_id=sm.course_id where cm.course_id=" + ddl_course_search.SelectedValue + " and sm.Branch_id='" + ViewState["branchid"].ToString() + "' and Subject_Type_Scholastic_Co_Scholastic='" + ddl_course_type.Text + "' order BY Course_Name";
                finalgride(query);
            }
        }

        private void find_by_class_subject_all()
        {
            string query = "Select sm.*,cm.Course_Name from Subject_Master sm join Add_course_table cm on cm.course_id=sm.course_id where sm.Branch_id='" + ViewState["branchid"].ToString() + "' and cm.course_id=" + ddl_course_search.SelectedValue + " order BY Course_Name";
            finalgride(query);
        }

        private void find_by_subject_only()
        {
            string query = "Select sm.*,cm.Course_Name from Subject_Master sm join Add_course_table cm on cm.course_id=sm.course_id where sm.Branch_id='" + ViewState["branchid"].ToString() + "' and Subject_Type_Scholastic_Co_Scholastic='" + ddl_course_type.Text + "' order BY Course_Name";
            finalgride(query);
        }


        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (((Label)e.Item.FindControl("lbl_Is_mandatory")).Text == "True")
                {
                    ((Label)e.Item.FindControl("lbl_midetry")).Text = "Mandatory";
                    ((Label)e.Item.FindControl("lbl_midetry")).CssClass = "badge badge-success ml-2";
                }
                else
                {
                    ((Label)e.Item.FindControl("lbl_midetry")).Text = "N/A";
                    ((Label)e.Item.FindControl("lbl_midetry")).CssClass = "badge badge-danger ml-2";
                }
                if (((Label)e.Item.FindControl("lbl_Subject_type")).Text == "Scholastic")
                {
                    ((Label)e.Item.FindControl("lbl_Subject_type")).Text = "Scholastic";
                    ((Label)e.Item.FindControl("lbl_Subject_type")).CssClass = "badge badge-Scholastic ml-2";
                }
                else
                {
                    ((Label)e.Item.FindControl("lbl_Subject_type")).Text = "Co-Scholastic";
                    ((Label)e.Item.FindControl("lbl_Subject_type")).CssClass = "badge badge-coScholastic ml-2";
                }
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
                        Panel1.RenderControl(hw);
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

        protected void btn_password_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["ConfPwd"].ToString() == txt_pwd_code.Text)
                {
                    add_update_subjects();
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

        protected void rd_class_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (((Label)e.Item.FindControl("lbl_class_id")).Text == ViewState["courseID"].ToString())
                {
                    ((CheckBox)e.Item.FindControl("chk_class")).Checked = true;
                }
            }
        }
    }
}