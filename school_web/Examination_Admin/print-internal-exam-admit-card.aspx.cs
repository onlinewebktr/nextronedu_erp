using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace school_web.Examination_Admin
{
    public partial class print_internal_exam_admit_card : System.Web.UI.Page
    {
        Examination em = new Examination();
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
                    ViewState["Userid"] = Session["Admin"].ToString();
                    ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                    ViewState["FirmID"] = My.get_firm_id();



                    mycode.bind_all_ddl_with_id(ddlsession, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                    ddlsession.SelectedValue = My.get_session_id();
                    mycode.bind_all_ddl_with_id(ddl_exam, "select Exam_name,Exam_id from Internal_Exam_master order by Exam_name asc");


                    //Bind_all_data();
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
                btn_print_all.Visible = false;
                Alertme("Sorry admit not created for this section.", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                btn_print_all.Visible = true;
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }


        protected void btn_find_Click(object sender, EventArgs e)
        {
            if (ddlsession.SelectedItem.Text == "Select")
            {
                ddlsession.Focus();
                Alertme("Please select session", "warning");
            }
            else if (ddl_exam.SelectedItem.Text == "Select")
            {
                ddl_exam.Focus();
                Alertme("Please select exam", "warning");
            }
            else if (ddl_class.SelectedItem.Text == "Select")
            {
                ddl_class.Focus();
                Alertme("Please select class", "warning");
            }
            else if (ddl_section.Text == "Select")
            {
                ddl_section.Focus();
                Alertme("Please select section", "warning");
            }
            else
            {
                string query = "select * from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddl_class.SelectedValue + "' and Section='" + ddl_section.Text + "' and Status='1' and Class_id in (select Class_id from Internal_exam_admit_card where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddl_class.SelectedValue + "' and Exam_id='" + ddl_exam.SelectedValue + "') order by rollnumber asc";
                bind_grid_data(query);
            }
        }


        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("trR");
                Label lbl_Session_Id = (Label)e.Item.FindControl("lbl_Session_Id");
                Label lbl_Class_id = (Label)e.Item.FindControl("lbl_Class_id");
                Label lbl_admission_no = (Label)e.Item.FindControl("lbl_admission_no");
                //Label lbl_exam_id = (Label)e.Item.FindControl("lbl_exam_id");
                HtmlAnchor admitcardLink = (HtmlAnchor)e.Item.FindControl("admitcardLink");

                admitcardLink.HRef = "slip/print-admit-card-internal-exam.aspx?session_Id=" + lbl_Session_Id.Text + "&classid=" + lbl_Class_id.Text + "&section=" + ddl_section.Text + "&admin=" + lbl_admission_no.Text + "&examid=" + ddl_exam.SelectedValue + "&from=stdwises&checked=0";
            }
        }




        protected void btn_print_all_Click(object sender, EventArgs e)
        {
            try
            {
                string adm_ids = "";
                int growcount = rd_view.Items.Count;
                int k = 0;
                for (int i = 0; i < growcount; i++)
                {
                    CheckBox chk = (CheckBox)rd_view.Items[i].FindControl("chkRowData");
                    if (chk.Checked == true)
                    {
                        Label lbl_id = (Label)rd_view.Items[i].FindControl("lbl_id");
                        adm_ids = adm_ids += lbl_id.Text + ",";
                    }
                    else
                    {
                        k++;
                    }
                }

                if (k == growcount)
                {
                    string reslink = "slip/print-admit-card-internal-exam.aspx?session_Id=" + ddlsession.SelectedValue + "&classid=" + ddl_class.SelectedValue + "&section=" + ddl_section.Text + "&admin=0&examid=" + ddl_exam.SelectedValue + "&from=stdwises&checked=0";
                    Response.Redirect(reslink, false);
                }
                else
                { 
                    string reslink = "slip/print-admit-card-internal-exam.aspx?session_Id=" + ddlsession.SelectedValue + "&classid=" + ddl_class.SelectedValue + "&section=" + ddl_section.Text + "&admin="+ adm_ids + "&examid=" + ddl_exam.SelectedValue + "&from=stdwises&checked=1"; 
                    Response.Redirect(reslink, false);
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void ddl_exam_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bind_class();
            }
            catch (Exception ex)
            {
            }
        }

        private void bind_class()
        {
            mycode.bind_all_ddl_with_id(ddl_class, "select t2.Course_Name,t1.Class_id from Internal_exam_admit_card t1 join Add_course_table t2 on t1.Class_id=t2.course_id where t1.Session_id='" + ddlsession.SelectedValue + "' and t1.Exam_id='" + ddl_exam.SelectedValue + "' order by t2.Position asc");
        }

        protected void ddl_class_SelectedIndexChanged1(object sender, EventArgs e)
        {
            try
            {
                My.bind_ddl_select(ddl_section, "select distinct Section from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddl_class.SelectedValue + "' order by Section asc");
            }
            catch (Exception ex)
            {
            }

        }
    }
}