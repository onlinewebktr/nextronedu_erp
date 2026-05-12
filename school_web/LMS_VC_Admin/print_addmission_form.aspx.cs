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

namespace school_web.LMS_VC_Admin
{
    public partial class print_addmission_form : System.Web.UI.Page
    {
        UsesCode mycode = new UsesCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Apply_id"] == null)
                {
                    Response.Redirect("View_Application.aspx", false);
                }
                else
                {
                    ViewState["Apply_id"] = Session["Apply_id"].ToString();
                    fatch_data();
                }
            }
        }

        private void fatch_data()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@cmdstatus", '5');
            cmd.Parameters.AddWithValue("@Apply_id", ViewState["Apply_id"].ToString());

            cmd.CommandText = "sp_Online_addmission_form";

            DataTable dt = UsesCode.Getdata_sp(cmd);
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                lbl_applicant_name_hn.Text = dt.Rows[0]["Applicant_name_HN"].ToString();

                lbl_studentapplyid.Text = dt.Rows[0]["Apply_id"].ToString();
                lbl_applicant_name_en.Text = dt.Rows[0]["Applicant_name_EN"].ToString();
                lbl_fathername.Text = dt.Rows[0]["Father_hub_name"].ToString();
                lbl_mothername.Text = dt.Rows[0]["Mother_name"].ToString();
                lbl_dob.Text = dt.Rows[0]["Dob"].ToString();
                lbl_gender.Text = dt.Rows[0]["Dob"].ToString();


                lbl_age.Text = "दिन:- " + dt.Rows[0]["day"].ToString() + " महिना:- " + dt.Rows[0]["Month"].ToString() + " वर्ष:- " + dt.Rows[0]["Year"].ToString();
                lbl_65_attend.Text = dt.Rows[0]["Attend_65"].ToString();
                lbl_65_rollno.Text = dt.Rows[0]["Roll_65"].ToString();
                lbl_sharni.Text = dt.Rows[0]["shraney_65"].ToString();
                lbl_64_attend_exam.Text = dt.Rows[0]["Attend_64"].ToString();
                lbl_main_exam_subject.Text = dt.Rows[0]["Subject"].ToString();
                lbl_tel_no.Text = dt.Rows[0]["mob_no"].ToString();
                lbl_wataasp_no.Text = dt.Rows[0]["whatsapp"].ToString();
                lbl_emailid.Text = dt.Rows[0]["email"].ToString();
                lbl_full_address_let.Text = dt.Rows[0]["Address_letter"].ToString();
                lbl_district_let.Text = dt.Rows[0]["Dis_letter"].ToString();
                lbl_state_let.Text = dt.Rows[0]["State_letter"].ToString();
                lbl_pin_let.Text = dt.Rows[0]["Pin_code"].ToString();
                lbl_address_per.Text = dt.Rows[0]["Address_per"].ToString();
                lbl_diistrict_per.Text = dt.Rows[0]["Dis_per"].ToString();
                lbl_pin_per.Text = dt.Rows[0]["Pin_code_per"].ToString();

                lbl_haj_cochin_previous.Text = dt.Rows[0]["Have_you_attand_any_other_previous"].ToString();
                lbl_type_of_language.Text = dt.Rows[0]["Exam_medium"].ToString();
                lbl_other_exam.Text = dt.Rows[0]["Exam_medium"].ToString();
                lbl_date.Text = dt.Rows[0]["Date"].ToString();
                lbl_place.Text = dt.Rows[0]["Place"].ToString();
                ViewState["Temp_id"] = dt.Rows[0]["Temp_id"].ToString();
                Bind_education();
                Bind_doc_master();


            }
        }

        private void Bind_doc_master()
        {
            SqlCommand cmd;
            string strQuery = "Select * from Doc_Master_for_online_reg order by Doc_id ASC";
            cmd = new SqlCommand(strQuery);
            DataTable dt = UsesCode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lbl_File_id = (Label)e.Row.FindControl("lbl_File_id");

                HtmlAnchor a1 = e.Row.FindControl("a1") as HtmlAnchor;
                bool file_avl = chekfileuplodornot(lbl_File_id.Text, a1);
                if (file_avl == true)
                {



                    a1.Visible = true;
                }
                else
                {

                    a1.Visible = false;
                }
            }
        }

        private bool chekfileuplodornot(string fileid, HtmlAnchor a1)
        {
            SqlCommand cmd;
            string strQuery = "Select * from Upload_doc_for_apply_addmission where File_id=" + fileid + " and Temp_id=" + ViewState["Temp_id"].ToString() + "  ";
            cmd = new SqlCommand(strQuery);
            DataTable dt = UsesCode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                a1.HRef = "";
                return false;
            }
            else
            {
                if (fileid == "1")
                {
                    Image1.ImageUrl = dt.Rows[0]["Doc_path"].ToString();
                }
                if (fileid == "2")
                {
                    Image2.ImageUrl = dt.Rows[0]["Doc_path"].ToString();
                }


                a1.HRef = dt.Rows[0]["Doc_path"].ToString();
                return true;
            }
        }
        private void Bind_education()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@cmdstatus", '2');
            cmd.Parameters.AddWithValue("@Temp_id", ViewState["Temp_id"].ToString());
            cmd.CommandText = "sp_Online_addmission_Education_Insert";
            DataTable dt = UsesCode.Getdata_sp(cmd);
            if (dt.Rows.Count == 0)
            {
                grid_education.DataSource = null;
                grid_education.DataBind();
            }
            else
            {
                grid_education.DataSource = dt;
                grid_education.DataBind();
            }

        }
    }
}