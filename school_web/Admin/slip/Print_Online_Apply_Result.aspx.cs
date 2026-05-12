using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;

namespace school_web.Admin.slip
{
    public partial class Print_Online_Apply_Result : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["session_Id"] != null && Request.QueryString["classid"] != null && Request.QueryString["admin"] != null)
                {
                    ViewState["branch_id"] = "1";
                    ViewState["session_Id"] = Request.QueryString["session_Id"].ToString();
                    ViewState["classid"] = Request.QueryString["classid"].ToString();
                    ViewState["admissionnumber"] = Request.QueryString["admin"].ToString();
                    ViewState["admin"] = Request.QueryString["admin"].ToString();
                    try
                    {
                        A1.HRef = "../../Download_Result_Card.aspx";
                        ViewState["type"] = Request.QueryString["type"].ToString();

                    }
                    catch
                    {
                        ViewState["type"] = "In";
                        A1.HRef = "../Online_Reg_View_Result.aspx";
                    }

                    fatch_student_list();
                }
                else
                {


                }




            }

        }

        private void fatch_student_list()
        {
            string query;
            if (ViewState["admin"].ToString() == "0")
            {

                query = "Select oa.*,'0'  as admissionnumber,format(ore.Exam_Date_time, 'dd/MM/yyyy') as Created_datetime1,ore.Exam_Time,(select top 1 Session from session_details where session_id=oa.Session_id) as Session_name,(select top 1 Course_Name from Add_course_table where course_id=oa.Class_id) as Course_Name,CASE WHEN oa.Student_image is null THEN '/images/dummy-student.jpg'  WHEN oa.Student_image is not null THEN oa.Student_image END AS Student_img,(Select top 1 Terms from Online_Admit_Card_Term)as Terms  from Online_Admission oa join Admission_no ore on oa.Registration_id=ore.Admission_no and oa.Session_id=ore.Session_Id  where oa.Session_id=" + ViewState["session_Id"].ToString() + " and oa.Class_id=" + ViewState["classid"].ToString() + "  and oa.Payment_Status='Paid' order by oa.Name ";


              

            }
            else
            {

                query = "Select oa.*,'0'  as admissionnumber,format(ore.Exam_Date_time, 'dd/MM/yyyy') as Created_datetime1,ore.Exam_Time,(select top 1 Session from session_details where session_id=oa.Session_id) as Session_name,(select top 1 Course_Name from Add_course_table where course_id=oa.Class_id) as Course_Name,CASE WHEN oa.Student_image is null THEN '/images/dummy-student.jpg'  WHEN oa.Student_image is not null THEN oa.Student_image END AS Student_img,(Select top 1 Terms from Online_Admit_Card_Term)as Terms  from Online_Admission oa join Online_Reg_Exam_Time_Table ore on oa.Registration_id=ore.Admission_no and oa.Session_id=ore.Session_Id  where oa.Registration_id='" + ViewState["admin"].ToString() + "'  and oa.Payment_Status='Paid' order by oa.Name ";





            }
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = mycode.GetData(cmd);

            if (dt.Rows.Count == 0)
            {

                rd_view.DataSource = null;
                rd_view.DataBind();

            }
            else
            {

                rd_view.DataSource = dt;
                rd_view.DataBind();
            }

        }
        protected void ImageButton_print_Click(object sender, ImageClickEventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "printit", "printit()", true);
        }

        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataTable dt = mycode.FillData("select * from Firm_Details ");
                if (dt.Rows.Count == 0)
                {

                }
                else
                {
                    ((Label)e.Item.FindControl("lbl_school_name")).Text = dt.Rows[0]["firm_name"].ToString();
                    ((Image)e.Item.FindControl("schoollogo")).ImageUrl = dt.Rows[0]["logo"].ToString();

                    if (dt.Rows[0]["firm_name"].ToString() == "DELHI PUBLIC SCHOOL, NTPC FARAKKA")
                    {
                        ((Label)e.Item.FindControl("lbl_under")).Visible = true;

                    }
                    try
                    {


                        ((Label)e.Item.FindControl("lbl_affilation_no")).Text = dt.Rows[0]["Affiliation_by"].ToString() + " Affiliation Number-" + dt.Rows[0]["Affiliation"].ToString();
                    }
                    catch
                    {
                        ((Label)e.Item.FindControl("lbl_affilation_no")).Text = "CBSE Affiliation Number-" + dt.Rows[0]["Affiliation"].ToString();
                    }


                    ((Label)e.Item.FindControl("lbl_address")).Text = "" + dt.Rows[0]["address1"].ToString();
                    ((Label)e.Item.FindControl("lbl_mobileno_emailid")).Text = "Telephone No :" + dt.Rows[0]["contact_no"].ToString() + " , E-mail Address :" + dt.Rows[0]["email"].ToString();



                }

                Label Class_id = ((Label)e.Item.FindControl("lbl_class_id")) as Label;
                Label Section = ((Label)e.Item.FindControl("lbl_section")) as Label;
                Label lbl_class_sec = ((Label)e.Item.FindControl("lbl_class_sec")) as Label;


                Label lbl_Session_name = ((Label)e.Item.FindControl("lbl_Session_name")) as Label;
                //================= 
                string signatures = Examination.get_signature_admit_card(My.get_session_id(), Class_id.Text, "A", ViewState["branch_id"].ToString());
                string[] stringSeparatorss = new string[] { ">" };
                string[] arrs = signatures.Split(stringSeparatorss, StringSplitOptions.None);
                string class_teacher_sig = arrs[0];
                string principal_sig = arrs[1];
                string examinee_sig = arrs[2];


                ((Image)e.Item.FindControl("Image1")).ImageUrl = class_teacher_sig;
                ((Image)e.Item.FindControl("Image2")).ImageUrl = examinee_sig;
                ((Image)e.Item.FindControl("Image3")).ImageUrl = principal_sig;

                if (class_teacher_sig == "hidden")
                {
                    ((Image)e.Item.FindControl("Image1")).Visible = false;
                }
                else
                {
                    ((Image)e.Item.FindControl("Image1")).Visible = true;
                }

                if (examinee_sig == "hidden")
                {
                    ((Image)e.Item.FindControl("Image2")).Visible = false;
                }
                else
                {
                    ((Image)e.Item.FindControl("Image2")).Visible = true;
                }

                if (principal_sig == "hidden")
                {
                    ((Image)e.Item.FindControl("Image3")).Visible = false;
                }
                else
                {
                    ((Image)e.Item.FindControl("Image3")).Visible = true;
                }

                //
                //// ((Label)e.Item.FindControl("lbl_exam_years")).Text = ViewState["year"].ToString();
                //  ((Label)e.Item.FindControl("lbl_examtermname")).Text = ViewState["Term_Name"].ToString() + "-" + ViewState["year"].ToString();

                ((Label)e.Item.FindControl("lbl_termname")).Text = "RESULT CARD FOR " + lbl_class_sec.Text + " EXAMINATION ";
                ((Label)e.Item.FindControl("lbl_session")).Text = "ACADEMIC SESSION: " + lbl_Session_name.Text;

                string asmissionno = ((Label)e.Item.FindControl("lbl_admission_no")).Text;


                GridView grid_grade = (GridView)e.Item.FindControl("grid_grade");

                Bind_subject_list(grid_grade, asmissionno);




            }
        }

        private void Bind_subject_list(GridView grid_grade, string asmissionno)
        {
            string query = " Select * from Online_Reg_Exam_Result where Admission_no='" + asmissionno + "'  ";
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = mycode.GetData(cmd);

            if (dt.Rows.Count == 0)
            {

                grid_grade.DataSource = null;
                grid_grade.DataBind();

            }
            else
            {

                grid_grade.DataSource = dt;
                grid_grade.DataBind();
            }
        }
    }
}