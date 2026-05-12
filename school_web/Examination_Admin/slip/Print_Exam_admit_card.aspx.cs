using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Examination_Admin.slip
{
    public partial class Print_Exam_admit_card : System.Web.UI.Page
    {

        My mycode = new My();
        Examination ec = new Examination();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["classid"] != null && Request.QueryString["session_Id"] != null && Request.QueryString["branch_id"] != null && Request.QueryString["examterm"] != null && Request.QueryString["section"] != null)
                {
                    ViewState["classid"] = Request.QueryString["classid"].ToString();
                    ViewState["session_Id"] = Request.QueryString["session_Id"].ToString();
                    ViewState["examtermid"] = Request.QueryString["examterm"].ToString();
                    ViewState["section"] = Request.QueryString["section"].ToString();
                    ViewState["branch_id"] = Request.QueryString["branch_id"].ToString();
                    ViewState["session"] = mycode.get_session(ViewState["session_Id"].ToString());
                    ViewState["admin"] = Request.QueryString["admin"].ToString();
                     

                    top_one_data();
                    fatch_student_list();
                }
                else
                {


                }




            }
        }
        string classname = "select top 1 Course_Name from Add_course_table where course_id=Exam_Time_Table.Class_id";
        string examtermname = "select top 1 Term_Name from Exam_Term_Details where Class_id=Exam_Time_Table.Class_id and Exam_Term_Id=Exam_Time_Table.Exam_Term_Id";
        private void top_one_data()
        {
            string query = "Select distinct Class_id,Section,Session_Id,Exam_Term_Id,(" + classname + ") as classname,(" + examtermname + ") as Term_Name,Exam_Date from dbo.[Exam_Time_Table] where Session_Id='" + ViewState["session_Id"].ToString() + "' and Branch_id='" + ViewState["branch_id"].ToString() + "' and Class_id=" + ViewState["classid"].ToString() + " and Section='" + ViewState["section"].ToString() + "' and Exam_Term_Id=" + ViewState["examtermid"].ToString() + "";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {

            }
            else
            {

                ViewState["classname"] = dt.Rows[0]["classname"].ToString();
                ViewState["Term_Name"] = dt.Rows[0]["Term_Name"].ToString();

                ViewState["session"] = mycode.get_session(dt.Rows[0]["Session_Id"].ToString());
                ViewState["Exam_Date"] = dt.Rows[0]["Exam_Date"].ToString();
                string cunrt_session = ViewState["session"].ToString();
                string session_frst_year = cunrt_session.Substring(0, 4);
                int session_s_year = My.toint(session_frst_year);
                int s_year = My.toint(session_frst_year);

                string month = ViewState["Exam_Date"].ToString().Substring(3, 2);

                int pay_month = My.toint(month);
                s_year = My.check_start_months(pay_month, s_year);


                ViewState["year"] = s_year;


            }


        }

        private void fatch_student_list()
        {
            string query;
            if (ViewState["admin"].ToString() == "0")
            {
                query = "Select CASE WHEN ar.studentimagepath is null THEN '/images/dummy-student.jpg'  WHEN ar.studentimagepath is not null THEN ar.studentimagepath END AS Student_img,ar.class,ar.Class_id,ar.studentname,ar.admissionserialnumber,ar.rollnumber,ar.Section,ar.session,ar.dob,ar.mothername,ar.fathername,ar.father_mob,ar.mother_mob,ar.mobilenumber as selfmobileno,(Select top 1 house_name from house_master where house_id=ar.house) as house_name from admission_registor ar where ar.Status='1' and ar.Class_Id=" + ViewState["classid"].ToString() + " and ar.Section='" + ViewState["section"].ToString() + "' and ar.Session_Id=" + ViewState["session_Id"].ToString() + " order by ar.rollnumber";
            }
            else
            {
                query = "Select CASE WHEN ar.studentimagepath is null THEN '/images/dummy-student.jpg'  WHEN ar.studentimagepath is not null THEN ar.studentimagepath END AS Student_img,ar.class,ar.Class_id,ar.studentname,ar.admissionserialnumber,ar.rollnumber,ar.Section,ar.session,ar.dob,ar.mothername,ar.fathername,ar.father_mob,ar.mother_mob,ar.mobilenumber as selfmobileno,(Select top 1 house_name from house_master where house_id=ar.house) as house_name from admission_registor ar where ar.Status='1' and ar.Class_Id=" + ViewState["classid"].ToString() + " and ar.Section='" + ViewState["section"].ToString() + "' and ar.Session_Id=" + ViewState["session_Id"].ToString() + " and ar.admissionserialnumber='" + ViewState["admin"].ToString() + "' order by ar.rollnumber";
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


                    if (dt.Rows[0]["firm_name"].ToString() == "Burdwan Holy Child School")
                    {
                        ((Label)e.Item.FindControl("lbl_affilation_no")).Text = "CISCE Affiliation Number-" + dt.Rows[0]["Affiliation"].ToString();
                    }

                    ((Label)e.Item.FindControl("lbl_address")).Text = "" + dt.Rows[0]["address1"].ToString();
                    ((Label)e.Item.FindControl("lbl_mobileno_emailid")).Text = "Telephone No :" + dt.Rows[0]["contact_no"].ToString() + " , E-mail Address :" + dt.Rows[0]["email"].ToString();



                }

                Label Class_id = ((Label)e.Item.FindControl("lbl_class_id")) as Label;
                Label Section = ((Label)e.Item.FindControl("lbl_section")) as Label;

                //================= 
                string signatures = Examination.get_signature_admit_card(My.get_session_id(), Class_id.Text, Section.Text, ViewState["branch_id"].ToString());
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

                //+ "-" + ViewState["year"].ToString()
                ((Label)e.Item.FindControl("lbl_exam_years")).Text = ViewState["year"].ToString();
                ((Label)e.Item.FindControl("lbl_exam_years1")).Text = ViewState["year"].ToString();
                ((Label)e.Item.FindControl("lbl_examtermname")).Text = ViewState["Term_Name"].ToString();

                ((Label)e.Item.FindControl("lbl_termname")).Text = "ADMIT CARD FOR " + ViewState["Term_Name"].ToString() + " EXAMINATION " + ViewState["year"].ToString();
                ((Label)e.Item.FindControl("lbl_session")).Text = "ACADEMIC SESSION: " + ViewState["session"].ToString();

                string asmissionno = ((Label)e.Item.FindControl("lbl_admission_no")).Text;


                GridView grid_grade = (GridView)e.Item.FindControl("grid_grade");

                Bind_subject_list(grid_grade, asmissionno);




            }
        }

        private void Bind_subject_list(GridView grid_grade, string asmissionno)
        {
            string query = " Select es.Day,sm.Subject_name,format(es.Exam_Date_time, 'dd/MM/yyyy') as Exam_datetime1,format(es.Exam_Date_time, 'hh:mm tt') as Exam_time1 from Subject_Master sm join Exam_Time_Table es on es.Subject_id=sm.Subject_id and es.Class_id=sm.course_id join   Subject_Mapping_New smn on smn.Class_id=es.Class_id and smn.Sub_id=es.Subject_id and smn.Session_id=es.Session_Id  where es.Class_id=" + ViewState["classid"].ToString() + " and es.Branch_id='" + ViewState["branch_id"].ToString() + "' and es.Session_Id=" + ViewState["session_Id"].ToString() + " and es.Section='" + ViewState["section"].ToString() + "' and smn.Admission_no='" + asmissionno + "' and es.Exam_Term_Id=" + ViewState["examtermid"].ToString() + "  order by cast( (Substring (Exam_Date,7,4)+Substring (Exam_Date,4,2)+Substring (Exam_Date,1,2)) as int) asc ";

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

        protected void ImageButton_print_Click(object sender, ImageClickEventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "printit", "printit()", true);
        }
    }
}