using school_web.AppCode;
using school_web.AppCode.Exam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Examination_Admin
{
    public partial class auto_mark_update : System.Web.UI.Page
    {
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

                        ViewState["Branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        bind_session();

                        ddlsession.SelectedValue = My.get_session_id();
                        bind_class();
                        ddlclass.SelectedValue = My.get_top_one_class();
                        mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor order by Section");
                        ddl_section.Text = My.get_top_one_section();

                        mycode.bind_all_ddl_with_id(ddl_term, "select Term_Name,Exam_Term_Id from Exam_Term_Details where Session_Id=" + ddlsession.SelectedValue + " and Branch_Id='" + ViewState["Branchid"].ToString() + "' and Class_id=" + ddlclass.SelectedValue + " order by Term_Name asc");

                        //ddl_term.Items.Insert(0, new ListItem("ALL", "0"));
                        //bind_grd_view();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Student_Result");
            }
        }


        private void bind_session()
        {
            mycode.bind_all_ddl_with_id(ddlsession, "Select  Session,session_id from session_details");
        }
        private void bind_class()
        {
            mycode.bind_all_ddl_with_id(ddlclass, "Select Course_Name,course_id,Position from Add_course_table order by Position");
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

        My mycode = new My();
        private void bind_grd_view()
        {
            string qry = "";
            if (ddl_section.SelectedItem.Text == "ALL")
            {
                qry = "select DISTINCT em.Admission_no,em.Session_id,em.Class_id,em.Branch_id,em.Term,ar.class,ar.session,ar.Section,ar.rollnumber,ar.studentname, (select top 1 Short_Name from Exam_Term_Details where Session_Id=em.Session_id and Branch_id=em.Branch_id and Class_id=em.Class_id and Exam_Term_Id=em.Term) as Term_name from Exam_marks em join admission_registor ar on em.Session_id=ar.Session_id and em.Branch_id=ar.Branch_id and em.Class_id=ar.Class_id and em.Admission_no=ar.admissionserialnumber where em.Session_id=" + ddlsession.SelectedValue + " and em.Class_id=" + ddlclass.SelectedValue + " and  em.Term=" + ddl_term.SelectedValue + " and (Marks='LA' or Marks='ML' or Marks='OD' or Marks='LA' or Marks='LG' or Marks='SL')  order by ar.rollnumber asc";
            }
            else
            {
                qry = "select DISTINCT em.Admission_no,em.Session_id,em.Class_id,em.Branch_id,em.Term,ar.class,ar.session,ar.Section,ar.rollnumber,ar.studentname, (select top 1 Short_Name from Exam_Term_Details where Session_Id=em.Session_id and Branch_id=em.Branch_id and Class_id=em.Class_id and Exam_Term_Id=em.Term) as Term_name from Exam_marks em join admission_registor ar on em.Session_id=ar.Session_id and em.Branch_id=ar.Branch_id and em.Class_id=ar.Class_id and em.Admission_no=ar.admissionserialnumber where em.Session_id=" + ddlsession.SelectedValue + " and em.Class_id=" + ddlclass.SelectedValue + " and ar.Section='" + ddl_section.SelectedItem.Text + "' and  em.Term=" + ddl_term.SelectedValue + " and (Marks='LA' or Marks='ML' or Marks='OD' or Marks='LA' or Marks='LG' or Marks='SL') order by ar.rollnumber asc";
            }
            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {
                btn_update_mark.Visible = false;
                Alertme("Sorry there are no data list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                btn_update_mark.Visible = true;
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }

        protected void ddlclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("please select session.", "warning");
                    ddlsession.Focus();
                }
                else
                {
                    ViewState["IsPlusTwoChecked"] = "NO";
                    bind_terms();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void bind_terms()
        {
            mycode.bind_all_ddl_with_id(ddl_term, "select Term_Name,Exam_Term_Id from Exam_Term_Details where Session_Id=" + ddlsession.SelectedValue + " and Branch_Id='" + ViewState["Branchid"].ToString() + "' and Class_id=" + ddlclass.SelectedValue + " order by Term_Name asc");
        }


        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("please select session.", "warning");
                    ddlsession.Focus();
                }
                if (ddlclass.SelectedItem.Text == "Select")
                {
                    Alertme("please select class.", "warning");
                    ddlclass.Focus();
                }
                if (ddl_term.SelectedItem.Text == "Select")
                {
                    Alertme("please select term.", "warning");
                    ddl_term.Focus();
                }
                else
                {
                    bind_grd_view();
                }
            }
            catch (Exception ex)
            {
            }
        }




        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbl_admissionserialnumber = ((Label)e.Item.FindControl("lbl_admissionserialnumber")) as Label;
                Label lbl_session_id = ((Label)e.Item.FindControl("lbl_session_id")) as Label;
                Label lbl_class_id = ((Label)e.Item.FindControl("lbl_class_id")) as Label;
                Label lbl_branch_id = ((Label)e.Item.FindControl("lbl_branch_id")) as Label;
                Label lbl_term_id = ((Label)e.Item.FindControl("lbl_term_id")) as Label;
            }
        }



        protected void btn_update_mark_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("please select session.", "warning");
                    ddlsession.Focus();
                }
                if (ddlclass.SelectedItem.Text == "Select")
                {
                    Alertme("please select class.", "warning");
                    ddlclass.Focus();
                }
                if (ddl_term.SelectedItem.Text == "Select")
                {
                    Alertme("please select term.", "warning");
                    ddl_term.Focus();
                }
                else
                {
                    update_marks();
                    Alertme("Marks has been updated", "success");
                    rd_view.DataSource = null;
                    rd_view.DataBind();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void update_marks()
        {
            int growcount = rd_view.Items.Count;
            int k = 0;
            for (int i = 0; i < growcount; i++)
            {
                Label lbl_admissionserialnumber = (Label)rd_view.Items[i].FindControl("lbl_admissionserialnumber");
                Label lbl_session_id = (Label)rd_view.Items[i].FindControl("lbl_session_id");
                Label lbl_class_id = (Label)rd_view.Items[i].FindControl("lbl_class_id");
                Label lbl_branch_id = (Label)rd_view.Items[i].FindControl("lbl_branch_id");
                Label lbl_term_id = (Label)rd_view.Items[i].FindControl("lbl_term_id");


                string qrys = "select DISTINCT t1.Session_id,t1.Class_id,t1.Term,t1.Subject,t1.Admission_no,t1.Branch_id,t2.Subject_name,t2.Subject_Code,t2.Subject_position,(select top 1 class from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and Branch_id=t1.Branch_id and admissionserialnumber=t1.Admission_no) as Class_name,(select top 1 studentname from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and Branch_id=t1.Branch_id and admissionserialnumber=t1.Admission_no) as Student_name,(select top 1 rollnumber from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and Branch_id=t1.Branch_id and admissionserialnumber=t1.Admission_no) as Roll_number,(select top 1 Section from admission_registor where Session_id=t1.Session_id and Class_id=t1.Class_id and Branch_id=t1.Branch_id and admissionserialnumber=t1.Admission_no) as Section,isnull((select top 1 Rank from Exam_rank_master where Session_id=t1.Session_id and Class_id=t1.Class_id and Branch_id=t1.Branch_id and Admission_no=t1.Admission_no and Term_id=" + lbl_term_id.Text + "),'0') as Rank from Exam_marks t1 join Subject_Master t2 on t1.Subject=t2.Subject_id where t1.Session_id=" + lbl_session_id.Text + " and t1.Class_id=" + lbl_class_id.Text + " and t1.Branch_id='" + lbl_branch_id.Text + "' and t1.Admission_no='" + lbl_admissionserialnumber.Text + "' and t1.Term=" + lbl_term_id.Text + " and t2.Subject_Type_Scholastic_Co_Scholastic='Scholastic' order by t2.Subject_position asc";
                DataTable dts = mycode.FillData(qrys);
                foreach (DataRow dr in dts.Rows)
                {

                    ///======================
                    string obt_marks = "";
                    string full_marks = "";
                    string qry = "select DISTINCT top 1 t1.Session_id,t1.Class_id,t1.Exam_Term_Id as Term,t1.Assessment_Id as Assessment,t1.Branch_Id,t1.Assessment_Name,t1.Sequence_No from Exam_Assessment_Details t1 where t1.Session_Id=" + lbl_session_id.Text + " and t1.Branch_id='" + lbl_branch_id.Text + "' and t1.Class_id=" + lbl_class_id.Text + " and t1.Exam_Term_Id=" + lbl_term_id.Text + " and t1.Scholastic_Co_scholastic='Scholastic' order by t1.Sequence_No desc";
                    DataTable dt = mycode.FillData(qry);
                    if (dt.Rows.Count > 0)
                    {
                        string marks = Auto_mark_update.get_marks(lbl_session_id.Text, lbl_class_id.Text, lbl_admissionserialnumber.Text, lbl_term_id.Text, dt.Rows[0]["Assessment"].ToString(), dr["Subject"].ToString(), lbl_branch_id.Text, "Scholastic");

                        string[] stringSeparatorss = new string[] { "/" };
                        string[] arrs = marks.Split(stringSeparatorss, StringSplitOptions.None);
                        obt_marks = arrs[0];
                        full_marks = arrs[1];

                        string marks_type = arrs[2];
                        string total_no = arrs[3];
                    }

                    if (obt_marks == "AB" || obt_marks == "ML" || obt_marks == "OD" || obt_marks == "LA" || obt_marks == "LG" || obt_marks == "SL")
                    {
                    }
                    else
                    {
                        double markpercent = (My.toDouble(obt_marks) / My.toDouble(full_marks)) * 100;
                        //==================== 

                        string qryy = "select DISTINCT t1.Session_id,t1.Class_id,t1.Exam_Term_Id as Term,t1.Assessment_Id as Assessment,t1.Branch_Id,t1.Assessment_Name,t1.Sequence_No,t1.Maximum_Marks from Exam_Assessment_Details t1 where t1.Session_Id=" + lbl_session_id.Text + " and t1.Branch_id='" + lbl_branch_id.Text + "' and t1.Class_id=" + lbl_class_id.Text + " and t1.Exam_Term_Id=" + lbl_term_id.Text + " and t1.Scholastic_Co_scholastic='Scholastic' and t1.Assessment_Id!='" + dt.Rows[0]["Assessment"].ToString() + "' order by t1.Sequence_No asc";
                        DataTable dty = mycode.FillData(qryy);
                        if (dty.Rows.Count > 0)
                        {
                            foreach (DataRow dry in dty.Rows)
                            {
                                Auto_mark_update.update_auto_marks(lbl_session_id.Text, lbl_class_id.Text, lbl_admissionserialnumber.Text, lbl_term_id.Text, dry["Assessment"].ToString(), dr["Subject"].ToString(), lbl_branch_id.Text, "Scholastic", markpercent);
                            }
                        }
                    }
                } 

                k++;
            }
        }
    }
}