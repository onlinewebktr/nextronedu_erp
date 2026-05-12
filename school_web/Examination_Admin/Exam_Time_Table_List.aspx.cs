using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
using System.Web.UI.HtmlControls;

namespace school_web.Examination_Admin
{
    public partial class Exam_Time_Table_List : System.Web.UI.Page
    {
        Examination em = new Examination();
        My mycode = new My();
        string classname = "select top 1 Course_Name from Add_course_table where course_id=Exam_Time_Table.Class_id";
        string examtermname = "select top 1 Term_Name from Exam_Term_Details where Class_id=Exam_Time_Table.Class_id and Exam_Term_Id=Exam_Time_Table.Exam_Term_Id";
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
                    ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                    ViewState["FirmID"] = My.get_firm_id();
                    Session["userType"] = "Admin";


                    mycode.bind_all_ddl_with_id(ddlsession, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                    ddlsession.SelectedValue = My.get_session_id();
                    mycode.bind_all_ddl_with_id(ddl_class, "Select Course_Name,course_id from Add_course_table order by Position asc");
                    Bind_all_data();
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


        private void Bind_all_data()
        {
            string query = "select distinct et.Shift_type,et.Branch_id,et.Class_id,et.Section,et.Session_Id,et.Exam_Term_Id,(select top 1 Term_Name from Exam_Term_Details where Class_id=et.Class_id and Exam_Term_Id=et.Exam_Term_Id) as examtermname,(select top 1 Assessment_Name from Exam_Assessment_Details where Class_id=et.Class_id and Exam_Term_Id=et.Exam_Term_Id and Session_Id=et.Session_Id and Assessment_Id=et.Exam_id) as exam_name,et.Exam_id,ad.Course_Name classname,format(Created_datetime, 'dd/MM/yyyy') as Created_datetime1,ad.Position,'0'  as admissionnumber  from dbo.[Exam_Time_Table] et join Add_course_table ad on ad.course_id=et.Class_id  where et.Session_Id=" + ddlsession.SelectedValue + " and et.Branch_id='" + ViewState["branchid"].ToString() + "' order by ad.Position,et.Section ";
            bind_grid_data(query);
        }

        private void bind_grid_data(string query)
        {
            ViewState["query"] = query;
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = mycode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry no any data found", "warning");
                grid_grade.DataSource = null;
                grid_grade.DataBind();
            }
            else
            {
                grid_grade.DataSource = dt;
                grid_grade.DataBind();
            }
        }

        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlsession.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
            }
            else if (ddl_class.SelectedItem.Text == "Select")
            {
                Alertme("Please select class", "warning");
            }
            else
            {
                mycode.bind_all_ddl_with_id(ddl_exam_term, "Select Term_Name,Exam_Term_Id from Exam_Term_Details  where Class_id=" + ddl_class.SelectedValue + " and Session_Id=" + ddlsession.SelectedValue + " and Branch_Id='" + ViewState["branchid"].ToString() + "' order by Sequence_No asc");
                mycode.bind_ddl(ddl_section, "Select distinct Section from admission_registor where Class_Id=" + ddl_class.SelectedValue + "  and Session_Id=" + ddlsession.SelectedValue + " and Branch_id='" + ViewState["branchid"].ToString() + "'");
            }
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            if (ddlsession.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
            }
            else if (ddl_class.SelectedItem.Text == "Select")
            {
                Alertme("Please select class", "warning");
            }
            else if (ddl_exam_term.SelectedItem.Text == "Select")
            {
                Alertme("Please select exam term", "warning");
            }
            else if (ddl_exam.SelectedItem.Text == "Select")
            {
                Alertme("Please select exam", "warning");
            }
            else if (ddl_section.Text == "Select")
            {
                Alertme("Please select section", "warning");
            }
            else
            {
                string query = "select  distinct et.Shift_type,et.Branch_id,et.Class_id,et.Section,et.Session_Id,et.Exam_Term_Id,(select top 1 Term_Name from Exam_Term_Details where Class_id=et.Class_id and Exam_Term_Id=et.Exam_Term_Id) as examtermname,(select top 1 Assessment_Name from Exam_Assessment_Details where Class_id=et.Class_id and Exam_Term_Id=et.Exam_Term_Id and Session_Id=et.Session_Id and Assessment_Id=et.Exam_id) as exam_name,et.Exam_id,ad.Course_Name classname,format(Created_datetime, 'dd/MM/yyyy') as Created_datetime1,ad.Position,(" + 0 + ")  as admissionnumber  from dbo.[Exam_Time_Table] et join Add_course_table ad on ad.course_id=et.Class_id  where et.Session_Id='" + ddlsession.SelectedValue + "' and et.Branch_id='" + ViewState["branchid"].ToString() + "' and et.Class_id='" + ddl_class.SelectedValue + "' and et.Exam_Term_Id='" + ddl_exam_term.SelectedValue + "' and et.Section='" + ddl_section.Text + "' and et.Exam_id='" + ddl_exam.SelectedValue + "' order by  ad.Position,et.Section ";
                bind_grid_data(query);
            }

        }

        protected void lnk_view_Click(object sender, EventArgs e)
        {

        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lnk.Parent.Parent;
                Label lbl_Class_id = (Label)row.FindControl("lbl_Class_id");
                Label lbl_Exam_Term_Id = (Label)row.FindControl("lbl_Exam_Term_Id");
                Label lbl_section = (Label)row.FindControl("lbl_section");
                Label lbl_shift_type = (Label)row.FindControl("lbl_shift_type");
                Label lbl_exam_id = (Label)row.FindControl("lbl_exam_id");
                Response.Redirect("Create_Exam_Time_Table.aspx?Exam_Term_Id=" + lbl_Exam_Term_Id.Text + "&Class_id=" + lbl_Class_id.Text + "&Section=" + lbl_section.Text + "&Shifts=" + lbl_shift_type.Text + "&examid=" + lbl_exam_id.Text, false);
            }
            catch
            {
            }
        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lnk.Parent.Parent;
                Label lbl_Class_id = (Label)row.FindControl("lbl_Class_id");
                Label lbl_Exam_Term_Id = (Label)row.FindControl("lbl_Exam_Term_Id");
                Label lbl_exam_id = (Label)row.FindControl("lbl_exam_id");
                Label lbl_section = (Label)row.FindControl("lbl_section");
                mycode.executequery("delete from Exam_Time_Table where Session_Id='" + ddlsession.SelectedValue + "' and Branch_id='" + ViewState["branchid"].ToString() + "' and Class_id=" + lbl_Class_id.Text + " and Section='" + lbl_section.Text + "' and Exam_Term_Id=" + lbl_Exam_Term_Id.Text + " and Exam_id='" + lbl_exam_id.Text + "'");
                bind_grid_data(ViewState["query"].ToString());
                Alertme("Deletion process has been successfully done", "success");
            }
            catch
            {

            }
        }

        protected void btn_find_by_admission_no_Click(object sender, EventArgs e)
        {
            if (ddlsession.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
            }
            else
            {
                bool chek = Examination.chek_create_or_no_cerate_admitcard(txt_admission_no.Text, ViewState["branchid"].ToString(), ddlsession.SelectedValue);
                if (chek == true)
                {
                    Dictionary<string, object> dc1 = My.get_selected_studentinfo(txt_admission_no.Text, ddlsession.SelectedValue, ViewState["branchid"].ToString());
                    string Class_id = (String)dc1["Class_id"];
                    string Section = (String)dc1["Section"];
                    string query = "select  distinct et.Shift_type,et.Branch_id,et.Class_id,et.Section,et.Session_Id,et.Exam_Term_Id,(select top 1 Term_Name from Exam_Term_Details where Class_id=et.Class_id and Exam_Term_Id=et.Exam_Term_Id) as examtermname,(select top 1 Assessment_Name from Exam_Assessment_Details where Class_id=et.Class_id and Exam_Term_Id=et.Exam_Term_Id and Session_Id=et.Session_Id and Assessment_Id=et.Exam_id) as exam_name,et.Exam_id,ad.Course_Name classname,format(Created_datetime, 'dd/MM/yyyy') as Created_datetime1,ad.Position,('" + txt_admission_no.Text + "')  as admissionnumber  from dbo.[Exam_Time_Table] et join Add_course_table ad on ad.course_id=et.Class_id  where et.Session_Id='" + ddlsession.SelectedValue + "' and et.Branch_id='" + ViewState["branchid"].ToString() + "' and et.Class_id='" + Class_id + "'  and et.Section='" + Section + "' and et.Exam_id='" + ddl_exam.SelectedValue + "' order by et.Section";
                    bind_grid_data(query);
                }
                else
                {
                    Alertme("Sorry there are no created admit card at", "warning");
                }
            }
        }

        protected void grid_grade_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_Session_Id = (Label)e.Row.FindControl("lbl_Session_Id");
                Label lbl_Branch_id = (Label)e.Row.FindControl("lbl_Branch_id");
                Label lbl_Class_id = (Label)e.Row.FindControl("lbl_Class_id");
                Label lbl_shift_type = (Label)e.Row.FindControl("lbl_shift_type");
                Label lbl_Exam_Term_Id = (Label)e.Row.FindControl("lbl_Exam_Term_Id");
                Label lbl_exam_id = (Label)e.Row.FindControl("lbl_exam_id");
                Label lbl_section = (Label)e.Row.FindControl("lbl_section");
                Label lbl_admission_no = (Label)e.Row.FindControl("lbl_admission_no");
                HtmlAnchor admitcardLink = (HtmlAnchor)e.Row.FindControl("admitcardLink");

                if (lbl_shift_type.Text == "2")
                {
                    if (ddl_page.Text == "2"|| ddl_page.Text == "4")
                    {
                        admitcardLink.HRef = "slip/print-admit-card-double-shift-a5.aspx?session_Id=" + lbl_Session_Id.Text + "&branch_id=" + lbl_Branch_id.Text + "&classid=" + lbl_Class_id.Text + "&section=" + lbl_section.Text + "&examterm=" + lbl_Exam_Term_Id.Text + "&admin=0&examid=" + lbl_exam_id.Text + "&from=clsswise&page=" + ddl_page.Text;
                    }
                    else if (ddl_page.Text == "5")
                    {
                        admitcardLink.HRef = "slip/print-admit-d-shift-portrait.aspx?session_Id=" + lbl_Session_Id.Text + "&branch_id=" + lbl_Branch_id.Text + "&classid=" + lbl_Class_id.Text + "&section=" + lbl_section.Text + "&examterm=" + lbl_Exam_Term_Id.Text + "&admin=0&examid=" + lbl_exam_id.Text + "&from=clsswise&page=" + ddl_page.Text;
                    }
                    else
                    {
                        admitcardLink.HRef = "slip/print-termwise-admit-card-double-shift.aspx?session_Id=" + lbl_Session_Id.Text + "&branch_id=" + lbl_Branch_id.Text + "&classid=" + lbl_Class_id.Text + "&section=" + lbl_section.Text + "&examterm=" + lbl_Exam_Term_Id.Text + "&admin=0&examid=" + lbl_exam_id.Text + "&from=clsswise&page=" + ddl_page.Text;
                    }
                }
                else if (lbl_shift_type.Text == "3")
                {
                    if (ddl_page.Text == "2" || ddl_page.Text == "4")
                    {
                        admitcardLink.HRef = "slip/print-admit-card-three-shift-a5.aspx?session_Id=" + lbl_Session_Id.Text + "&branch_id=" + lbl_Branch_id.Text + "&classid=" + lbl_Class_id.Text + "&section=" + lbl_section.Text + "&examterm=" + lbl_Exam_Term_Id.Text + "&admin=0&examid=" + lbl_exam_id.Text + "&from=clsswise&page=" + ddl_page.Text;
                    }
                    else
                    {
                        admitcardLink.HRef = "slip/print-admit-card-three-shift.aspx?session_Id=" + lbl_Session_Id.Text + "&branch_id=" + lbl_Branch_id.Text + "&classid=" + lbl_Class_id.Text + "&section=" + lbl_section.Text + "&examterm=" + lbl_Exam_Term_Id.Text + "&admin=0&examid=" + lbl_exam_id.Text + "&from=clsswise&page=" + ddl_page.Text;
                    }
                }
                else
                {
                    if (ddl_page.Text == "2" || ddl_page.Text == "4")
                    {
                        admitcardLink.HRef = "slip/print-admit-card-a5.aspx?session_Id=" + lbl_Session_Id.Text + "&branch_id=" + lbl_Branch_id.Text + "&classid=" + lbl_Class_id.Text + "&section=" + lbl_section.Text + "&examterm=" + lbl_Exam_Term_Id.Text + "&admin=0&examid=" + lbl_exam_id.Text + "&from=clsswise&page=" + ddl_page.Text;
                    }
                    else
                    {
                        admitcardLink.HRef = "slip/print-termwise-admit-card.aspx?session_Id=" + lbl_Session_Id.Text + "&branch_id=" + lbl_Branch_id.Text + "&classid=" + lbl_Class_id.Text + "&section=" + lbl_section.Text + "&examterm=" + lbl_Exam_Term_Id.Text + "&admin=0&examid=" + lbl_exam_id.Text + "&from=clsswise&page=" + ddl_page.Text;
                    } 
                } 
            }
        }

        protected void ddl_exam_term_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                mycode.bind_all_ddl_with_id(ddl_exam, "select Assessment_Name,Assessment_Id from Exam_Assessment_Details where Session_Id='" + ddlsession.SelectedValue + "' and Branch_Id='" + ViewState["branchid"].ToString() + "' and Istatus=1 and Exam_Term_Id='" + ddl_exam_term.SelectedValue + "' and Class_id='" + ddl_class.SelectedValue + "' order by Sequence_No asc");
            }
            catch (Exception ex)
            {
            }
        }
    }
}