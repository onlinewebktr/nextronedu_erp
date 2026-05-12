using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
namespace school_web.LMS_VC_Admin
{
    public partial class Added_Sub_Subject_list : System.Web.UI.Page
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
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();
                        ViewState["branchid"] = Session["branchid"].ToString();

                        mycode.bind_all_ddl_with_id(ddl_SearchCategory, "Select Course_Name,course_id from Add_course_table order by Position");
                         


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

        
        private void Bind_All_subject()
        {
            string query = "";
            if (ddl_SearchCategory.SelectedItem.Text == "Select")
            {
                query = "Select ssm.*,cm.Course_Name,sm.Subject_name from Subject_Master sm join Add_course_table cm on cm.course_id=sm.course_id join Sub_Subject_Master ssm on ssm.course_id=sm.course_id and ssm.Subject_id=sm.Subject_id order BY Course_Name";
            }
            else
            {
                query = "Select ssm.*,cm.Course_Name,sm.Subject_name from Subject_Master sm join Add_course_table cm on cm.course_id=sm.course_id join Sub_Subject_Master ssm on ssm.course_id=sm.course_id and ssm.Subject_id=sm.Subject_id  where cm.course_id=" + ddl_SearchCategory.SelectedValue + " order BY Course_Name";
            }

            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                Alert("Sorry there are no. sub subject list exist");
                RPDetails.DataSource = null;
                RPDetails.DataBind();
            }
            else
            {
                RPDetails.DataSource = dt;
                RPDetails.DataBind();
            }
        }
        string scrpt;
        public void Alert(string Message)
        {

            lbl_msg.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }

        protected void btn_Find_Click(object sender, EventArgs e)
        {
            if (ddl_SearchCategory.SelectedItem.Text == "Select")
            {
                Alert("Sorry your class name already exit ");
            }
            else
            {
                Bind_All_subject();
            }
        }

        
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Sub_Subject_id = (Label)row.FindControl("lbl_Sub_Subject_id");
                Response.Redirect("Add_Sub_Subject.aspx?subsubject=" + lbl_Sub_Subject_id.Text, false);
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
                Bind_All_subject();

                Alert("Sub-subject  name has been deleted  done ");
            }
            catch
            {
            }

        }



    }
}