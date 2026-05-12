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
    public partial class Commentary_Remarks_Entry : System.Web.UI.Page
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
                    mycode.bind_all_ddl_with_id(ddlsession, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                    ddlsession.SelectedValue = My.get_session_id();

                     
                    ViewState["Branchid"] = mycode.getbranchid(Session["Admin"].ToString());
                    mycode.bind_all_ddl_with_id(ddl_CourseCat, "Select   Course_Name, course_id from Add_course_table order by Position asc");
                    mycode.bind_ddl(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddl_CourseCat.SelectedValue + "'  order by Section");

                    mycode.bind_all_ddl_with_id_manually(ddl_remarkstype, "Select   Commentary_Remark_Types, Remaks_Id from Exam_Commentary_Remark_Types where Branch_Id='" + ViewState["Branchid"].ToString() + "' order by Commentary_Remark_Types asc");
                }
            }
        }

        protected void ddl_CourseCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddl_CourseCat.SelectedItem.Text == "Select")
                {

                    Alertme("Please select class.", "warning");

                }
                else
                {
                    mycode.bind_ddl(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddl_CourseCat.SelectedValue + "'  order by Section");

                    mycode.bind_all_ddl_with_id(ddl_term, "select Term_Name,Exam_Term_Id from Exam_Term_Details where Class_id='" + ddl_CourseCat.SelectedValue + "' and Session_Id='" + ddlsession.SelectedValue + "' and Branch_Id='" + ViewState["Branchid"].ToString() + "' order by Term_Name asc");
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void RPDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    Label lbl_adm_no = ((Label)e.Item.FindControl("lbl_adm_no")) as Label;
                    TextBox txt_marks = ((TextBox)e.Item.FindControl("txt_marks")) as TextBox;
                    DropDownList ddl_remarkstypenew = ((DropDownList)e.Item.FindControl("ddl_remarkstypenew")) as DropDownList;

                    mycode.bind_ddl(ddl_remarkstypenew, "Select   Commentary_Remark_Types from Exam_Commentary_Remark_Types where Branch_Id='" + ViewState["Branchid"].ToString() + "' order by Commentary_Remark_Types asc");

                    DataTable dt = mycode.FillData("select Remarks from Exam_Commentary_Remark_Term_Wise_Entry where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddl_CourseCat.SelectedValue + "' and Section='" + ddl_section.Text + "' and Exam_Term_Id='" + ddl_term.SelectedValue + "'  and Admission_no='" + lbl_adm_no.Text + "' and Branch_id='" + ViewState["Branchid"].ToString() + "'  ");
                    if (dt.Rows.Count == 0)
                    {

                        if (ddl_remarkstype.SelectedItem.Text == "Manually")
                        {
                            txt_marks.Text = "";
                            txt_marks.Visible = true;
                            ddl_remarkstypenew.Visible = false;
                        }
                        else
                        {
                            txt_marks.Visible = false;
                            ddl_remarkstypenew.Visible = true;
                            txt_marks.Text = ddl_remarkstype.SelectedItem.Text;
                            ddl_remarkstypenew.Text = ddl_remarkstype.SelectedItem.Text;
                        }

                    }
                    else
                    {
                        if (ddl_remarkstype.SelectedItem.Text == "Manually")
                        {
                            txt_marks.Text = dt.Rows[0]["Remarks"].ToString();
                            ddl_remarkstypenew.Text = dt.Rows[0]["Remarks"].ToString();
                            txt_marks.Visible = true;
                            ddl_remarkstypenew.Visible = false;

                        }
                        else
                        {
                            txt_marks.Visible = false;
                            ddl_remarkstypenew.Visible = true;
                            txt_marks.Text = dt.Rows[0]["Remarks"].ToString();
                            ddl_remarkstypenew.Text = dt.Rows[0]["Remarks"].ToString();
                        }

                    }
                }
            }
            catch
            {

            }
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_CourseCat.SelectedItem.Text == "Select")
                {

                    Alertme("Please select class.", "warning");
                    ddl_CourseCat.Focus();
                }
                else if (ddl_section.Text == "Select")
                {
                    Alertme("Please select section", "warning");
                    ddl_section.Focus();
                }
                else if (ddl_term.SelectedItem.Text == "Select")
                {
                    Alertme("Please select term", "warning");
                    ddl_term.Focus();
                }
                else
                {
                    string query = "Select admissionserialnumber,studentname,rollnumber  from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddl_CourseCat.SelectedValue + "' and Section='" + ddl_section.Text + "' and Branch_Id='" + ViewState["Branchid"].ToString() + "' and  Status='1' and StudentStatus!='TC' order by rollnumber";
                    if(ddl_remarkstype.SelectedItem.Text=="Select")
                    {
                        lbl_activity_type.Text = "Remarks";
                    }
                    else
                    {
                        lbl_activity_type.Text = ddl_remarkstype.SelectedItem.Text;
                    } 
                    DataTable dt = mycode.FillTable(query);
                    if (dt.Rows.Count == 0)
                    {
                        RPDetails.DataSource = null;
                        RPDetails.DataBind();
                    }
                    else
                    {
                        RPDetails.DataSource = dt;
                        RPDetails.DataBind();
                    }
                }
            }
            catch { }
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            if (ddl_CourseCat.SelectedItem.Text == "Select")
            {

                Alertme("Please select class.", "warning");
                ddl_CourseCat.Focus();
            }
            else if (ddl_section.Text == "Select")
            {
                Alertme("Please select section", "warning");
                ddl_section.Focus();
            }
            else if (ddl_term.SelectedItem.Text == "Select")
            {
                Alertme("Please select term", "warning");
                ddl_term.Focus();
            }
           
            else
            {
                try
                {
                    save_marks();
                    Alertme("Commentary Remarks has been saved successfully", "success");
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void save_marks()
        {
            int i;
            int gridview_rowcount = RPDetails.Items.Count;
            for (i = 0; i < gridview_rowcount; i++)
            {
                Label lbl_adm_no = (Label)RPDetails.Items[i].FindControl("lbl_adm_no");
                TextBox txt_marks = (TextBox)RPDetails.Items[i].FindControl("txt_marks");
               // DropDownList ddl_remarkstypenew = (DropDownList)RPDetails.Items[i].FindControl("ddl_remarkstypenew"); 

                DataTable dt = mycode.FillData("select Id from Exam_Commentary_Remark_Term_Wise_Entry where Session_id=" + ddlsession.SelectedValue + " and Class_id=" + ddl_CourseCat.SelectedValue + " and Section='" + ddl_section.Text + "' and Exam_Term_Id=" + ddl_term.SelectedValue + "   and Admission_no='" + lbl_adm_no.Text + "' and Branch_id='" + ViewState["Branchid"].ToString() + "'  ");
                if (dt.Rows.Count == 0)
                {
                    SqlCommand cmd;
                    string query = "INSERT INTO Exam_Commentary_Remark_Term_Wise_Entry (Session_id,Branch_id,Section,Class_id,Exam_Term_Id,Remarks_id,Admission_no,Remarks,Cretaed_by,Created_date) values (@Session_id,@Branch_id,@Section,@Class_id,@Exam_Term_Id,@Remarks_id,@Admission_no,@Remarks,@Cretaed_by,@Created_date)";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Session_id", ddlsession.SelectedValue);
                    cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branchid"].ToString());
                    cmd.Parameters.AddWithValue("@Section", ddl_section.Text);
                    cmd.Parameters.AddWithValue("@Class_id", ddl_CourseCat.SelectedValue);
                    cmd.Parameters.AddWithValue("@Exam_Term_Id", ddl_term.SelectedValue);
                    cmd.Parameters.AddWithValue("@Remarks_id", "0");
                    cmd.Parameters.AddWithValue("@Admission_no", lbl_adm_no.Text);
                    cmd.Parameters.AddWithValue("@Remarks", txt_marks.Text);
                    cmd.Parameters.AddWithValue("@Cretaed_by", ViewState["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@Created_date", mycode.getdate1()); 
                    if (My.InsertUpdateData(cmd))
                    {
                    }
                }
                else
                {
                    string id = dt.Rows[0]["Id"].ToString();
                    SqlCommand cmd;
                    string query = "Update Exam_Commentary_Remark_Term_Wise_Entry set  Remarks=@Remarks,Cretaed_by=@Cretaed_by,Created_date=@Created_date  where  Id= @Id";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Remarks", txt_marks.Text);
                    cmd.Parameters.AddWithValue("@Cretaed_by", ViewState["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@Created_date", mycode.getdate1());
                    cmd.Parameters.AddWithValue("@Id", id); 
                    if (My.InsertUpdateData(cmd))
                    {
                    }
                }

            }
        }
    }
}