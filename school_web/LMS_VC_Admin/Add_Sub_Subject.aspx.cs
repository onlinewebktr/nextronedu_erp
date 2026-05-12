using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.LMS_VC_Admin
{
    public partial class Add_Sub_Subject : System.Web.UI.Page
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
                        btn_cncel.Visible = false;
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();
                        ViewState["branchid"] = Session["branchid"].ToString();
                        mycode.bind_all_ddl_with_id(ddl_class, "Select Course_Name,course_id from Add_course_table order by Position");

                        if (Request.QueryString["subsubject"] != null)
                        {
                            btn_cncel.Visible = true;
                            ViewState["Sub_Subject_id"] = Request.QueryString["subsubject"].ToString();
                            BindDetails_ald();
                           
                        }
                        else
                        {

                            Bind_All_subject("Select  top 10 ssm.*,cm.Course_Name,sm.Subject_name from Subject_Master sm join Add_course_table cm on cm.course_id=sm.course_id join Sub_Subject_Master ssm on ssm.course_id=sm.course_id and ssm.Subject_id=sm.Subject_id order BY id desc");
                        }
                     

                    }
                    // BindDetails();
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_User");
            }
        }

        private void BindDetails_ald()
        {
            DataTable dt = mycode.FillData("select * from Sub_Subject_Master where Sub_Subject_id='" + ViewState["Sub_Subject_id"].ToString() + "'");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {


                hd_id.Value = dt.Rows[0]["id"].ToString();
                txt_subsubject.Text = dt.Rows[0]["Sub_Subject_name"].ToString();
                ddl_class.SelectedValue = dt.Rows[0]["course_id"].ToString();
                mycode.bind_all_ddl_with_id(ddl_subject, "Select Subject_name,Subject_id from Subject_Master where course_id=" + ddl_class.SelectedValue + " order by  Subject_position");
                ddl_subject.SelectedValue = dt.Rows[0]["Subject_id"].ToString();
                hd_subjectid.Value = dt.Rows[0]["Subject_id"].ToString();

                hd_sub_subjectid.Value = dt.Rows[0]["Sub_Subject_id"].ToString();
                btn_submit.Text = "Update";
                Bind_All_subject("Select  top 10 ssm.*,cm.Course_Name,sm.Subject_name from Subject_Master sm join Add_course_table cm on cm.course_id=sm.course_id join Sub_Subject_Master ssm on ssm.course_id=sm.course_id and ssm.Subject_id=sm.Subject_id where ssm.course_id='" + dt.Rows[0]["course_id"].ToString() + "' and ssm.Subject_id='" + dt.Rows[0]["Subject_id"].ToString() + "' order BY ssm.Sub_Subject_position asc");

            }
        }
        public void Alert(string Message)
        {


            lbl_msg.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }
        private void Bind_All_subject(string query)
        {
          

            ViewState["query"] = query;
           


            DataTable dt = mycode.FillData(ViewState["query"].ToString());
            if (dt.Rows.Count == 0)
            {
                Alert("Sorry there are no subject list exist");
                RPDetails.DataSource = null;
                RPDetails.DataBind();
            }
            else
            {
                RPDetails.DataSource = dt;
                RPDetails.DataBind();
            }
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            if (ddl_class.SelectedItem.Text == "Select")
            {
                Alert("Please select course name");
            }
            else if (ddl_subject.SelectedValue == "")
            {
                Alert("Please select subject name");
            }
            else
            {
                if (btn_submit.Text == "Add")
                {
                    SqlCommand cmd;
                    DataTable dt = mycode.FillData("Select * from Sub_Subject_Master where course_id='" + ddl_class.SelectedValue + "' and Subject_id='" + ddl_subject.SelectedValue + "' and Sub_Subject_name='" + txt_subsubject.Text + "' ");
                    if (dt.Rows.Count == 0)
                    {
                        int position_id = get_position_id();
                        string subsubjectid = create_subjectid();
                        string query = "INSERT INTO Sub_Subject_Master (course_id,Subject_id,Sub_Subject_id,Branch_id,Sub_Subject_name,Sub_Subject_position,Date,Idate,Time,Created_by) values (@course_id,@Subject_id,@Sub_Subject_id,@Branch_id,@Sub_Subject_name,@Sub_Subject_position,@Date,@Idate,@Time,@Created_by)";
                        cmd = new SqlCommand(query);

                        cmd.Parameters.AddWithValue("@course_id", ddl_class.SelectedValue);
                        cmd.Parameters.AddWithValue("@Subject_id", ddl_subject.SelectedValue);
                        cmd.Parameters.AddWithValue("@Sub_Subject_id", subsubjectid);
                        cmd.Parameters.AddWithValue("@Branch_id", ViewState["branchid"].ToString());
                        cmd.Parameters.AddWithValue("@Sub_Subject_name", txt_subsubject.Text.Trim());
                        cmd.Parameters.AddWithValue("@Sub_Subject_position", position_id);
                        cmd.Parameters.AddWithValue("@Date", mycode.date());
                        cmd.Parameters.AddWithValue("@Idate", mycode.idate());
                        cmd.Parameters.AddWithValue("@Time", mycode.time());
                        cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                        if (My.InsertUpdateData(cmd))
                        {

                            Alert("Sub-Subject has been sucessfully added");
                            string msg = ViewState["Userid"].ToString() + " Create Subject, subject id=" + subsubjectid + " subject name=" + txt_subsubject.Text.Trim() + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MM/yyyy hh:mm:ss tt");
                            mycode.insert_data_logfile(ViewState["Userid"].ToString(), ViewState["firm_id"].ToString(), msg, ViewState["branchid"].ToString());
                            txt_subsubject.Text = "";


                            btn_submit.Text = "Add";
                            Bind_All_subject(ViewState["query"].ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                        }
                    }
                    else
                    {
                        Alert("Sorry your sub-subject name already exit ");
                    }
                }
                else
                {
                    SqlCommand cmd;
                    DataTable dt = mycode.FillData("Select * from Sub_Subject_Master where course_id='" + ddl_class.SelectedValue + "' and Subject_id='" + ddl_subject.SelectedValue + "'  and Sub_Subject_name='" + txt_subsubject.Text.Trim() + "' and Sub_Subject_id!=" + hd_sub_subjectid.Value + " ");
                    if (dt.Rows.Count == 0)
                    {
                        string query = "Update Sub_Subject_Master set course_id=@course_id,Subject_id=@Subject_id,Sub_Subject_id=@Sub_Subject_id,Branch_id=@Branch_id,Sub_Subject_name=@Sub_Subject_name,Date=@Date,Idate=@Idate,Time=@Time where Sub_Subject_id = @Sub_Subject_id";
                        cmd = new SqlCommand(query);
                        cmd.Parameters.AddWithValue("@course_id", ddl_class.SelectedValue);
                        cmd.Parameters.AddWithValue("@Subject_id", ddl_subject.SelectedValue);
                        cmd.Parameters.AddWithValue("@Sub_Subject_id", hd_sub_subjectid.Value);
                        cmd.Parameters.AddWithValue("@Branch_id", ViewState["branchid"].ToString());
                        cmd.Parameters.AddWithValue("@Sub_Subject_name", txt_subsubject.Text.Trim());


                        cmd.Parameters.AddWithValue("@Date", mycode.date());
                        cmd.Parameters.AddWithValue("@Idate", mycode.idate());
                        cmd.Parameters.AddWithValue("@Time", mycode.time());
                        cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());

                        if (My.InsertUpdateData(cmd))
                        {


                            Alert("Sub-Subject has been sucessfully updated");
                            string msg = ViewState["Userid"].ToString() + " Create Subject, subject id=" + hd_subjectid.Value + " subject name=" + txt_subsubject.Text.Trim() + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MM/yyyy hh:mm:ss tt");
                            mycode.insert_data_logfile(ViewState["Userid"].ToString(), ViewState["firm_id"].ToString(), msg, ViewState["branchid"].ToString());
                            txt_subsubject.Text = "";


                            btn_submit.Text = "Add";
                            Bind_All_subject(ViewState["query"].ToString());
                        }
                    }
                }
            }
        }

        private int get_position_id()
        {
            DataTable dt = mycode.FillData("Select top 1 Sub_Subject_position  from Sub_Subject_Master where course_id='" + ddl_class.SelectedValue + "' and Subject_id='" + ddl_subject.SelectedValue + "' order by  Sub_Subject_position desc  ");
            if (dt.Rows.Count == 0)
            {
                return 1;
            }
            else
            {

                return Convert.ToInt32(dt.Rows[0][0].ToString()) + 1;
            }
        }
        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_class.SelectedItem.Text == "Select")
            {
                Alert("Please select class name");
            }
            else
            {
                mycode.bind_all_ddl_with_id(ddl_subject, "Select Subject_name,Subject_id from Subject_Master where course_id=" + ddl_class.SelectedValue + " order by  Subject_position");
            }
        }

        private string create_subjectid()
        {
            bool duplicate = false;
            string subject_id = mycode.auto_serial("Sub_Subject_id");
            while (!duplicate)
            {
                DataTable cdt = mycode.FillData("  select Sub_Subject_id from dbo.[Sub_Subject_Master] where Sub_Subject_id='" + subject_id + "'");
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    duplicate = true;
                }
                else
                {
                    duplicate = false;
                    subject_id = mycode.auto_serial("Sub_Subject_id");
                }
            }
            return subject_id;
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Subject_id = (Label)row.FindControl("lbl_Subject_id");
                Label lbl_Class_id = (Label)row.FindControl("lbl_Class_id");
                Label lbl_Sub_Subject_Master = (Label)row.FindControl("lbl_Sub_Subject_Master");
                Label lbl_Sub_Subject_id = (Label)row.FindControl("lbl_Sub_Subject_id");
              
                ddl_class.SelectedValue = lbl_Class_id.Text;
                mycode.bind_all_ddl_with_id(ddl_subject, "Select Subject_name,Subject_id from Subject_Master where course_id=" + ddl_class.SelectedValue + " order by  Subject_position");
                ddl_subject.SelectedValue = lbl_Subject_id.Text;
                txt_subsubject.Text = lbl_Sub_Subject_Master.Text;

                hd_subjectid.Value = lbl_Subject_id.Text;
                hd_sub_subjectid.Value = lbl_Sub_Subject_id.Text;
                btn_submit.Text = "Update";

            }
            catch
            {
            }
        }

        protected void lnk_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Sub_Subject_id = (Label)row.FindControl("lbl_Sub_Subject_id");
                mycode.executequery("delete from Sub_Subject_Master where Sub_Subject_id='" + lbl_Sub_Subject_id.Text + "' ");
                Bind_All_subject(ViewState["query"].ToString());
                txt_subsubject.Text = "";

                btn_submit.Text = "Add";
                Alert("Sub-subject  name has been deleted  done ");
            }
            catch
            {
            }
        }

        protected void btn_cncel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Added_Sub_Subject_list.aspx", false);
        }


    }
}