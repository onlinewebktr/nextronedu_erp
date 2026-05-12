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
    public partial class Student_Attendence_Reprot : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (!IsPostBack)
                    {
                        txt_startdate.Text = code.date();
                        txt_enddate.Text = code.date();
                        code.bind_all_ddl_with_all(ddl_class, "Select Course_Name, course_id from Add_course_table order by Position");
                        code.bind_ddl_all1(dd_section, "Select distinct Section  from admission_registor    order by section");
                        
                        search_data();
                    }

                }
                catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
            }
        }
        public void Alert(string Message)
        {
            lbl_msg.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }
        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_class.SelectedItem.Text == "ALL")
            {
                code.bind_ddl_all1(dd_section, "Select distinct Section  from admission_registor    order by section");
            }
            else
            {
                code.bind_ddl_all1(dd_section, "Select distinct Section  from admission_registor where Class_id ='" + ddl_class.SelectedValue + "'    order by Section");
            }

        }
       
        protected void btn_find_Click(object sender, EventArgs e)
        {
            search_data();
        }
        private void search_data()
        {
            if (txt_startdate.Text == "")
            {
                Alert("Please select start date");
            }
            else if (txt_enddate.Text == "")
            {
                Alert("Please select end date");
            }
            else
            {
                SqlCommand cmd = new SqlCommand();
                if (Convert.ToInt32(code.ConvertStringToiDate(txt_startdate.Text)) <= Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)))
                {
                    if (ddl_class.SelectedItem.Text == "ALL" && dd_section.Text == "ALL")
                    {

                     
                        lbl_month_year.Text = "Start Date -" + txt_startdate.Text + "-" + "End Date -" + txt_enddate.Text;
                        cmd.Parameters.AddWithValue("@cmdstatus", "8");

                    }
                    else if (ddl_class.SelectedItem.Text != "ALL" && dd_section.Text == "ALL")
                    {
                        //  lbl_month_year.Text = "Class -" + ddl_class.SelectedItem.Text + "Section -" + dd_section.Text + "-" + "Start Date -" + txt_startdate.Text + "-" + "End Date -" + txt_enddate.Text;
                        lbl_month_year.Text = "Start Date -" + txt_startdate.Text + "-" + "End Date -" + txt_enddate.Text;
                        cmd.Parameters.AddWithValue("@cmdstatus", "9");
                        cmd.Parameters.AddWithValue("@Class", ddl_class.SelectedValue);

                    }
                    else if (ddl_class.SelectedItem.Text == "ALL" && dd_section.Text != "ALL")
                    {
                        //  lbl_month_year.Text = "Class -" + ddl_class.SelectedItem.Text + "Section -" + dd_section.Text + "-" + "Start Date -" + txt_startdate.Text + "-" + "End Date -" + txt_enddate.Text;
                        lbl_month_year.Text = "Start Date -" + txt_startdate.Text + "-" + "End Date -" + txt_enddate.Text;
                        cmd.Parameters.AddWithValue("@cmdstatus", "10");
                        cmd.Parameters.AddWithValue("@section", dd_section.Text);


                    }
                    else if (ddl_class.SelectedItem.Text != "ALL" && dd_section.Text != "ALL")
                    {
                         
                            //  lbl_month_year.Text = "Class -" + ddl_class.SelectedItem.Text + "Section -" + dd_section.Text + "-" + "Start Date -" + txt_startdate.Text + "-" + "End Date -" + txt_enddate.Text;
                            lbl_month_year.Text = "Start Date -" + txt_startdate.Text + "-" + "End Date -" + txt_enddate.Text;
                            cmd.Parameters.AddWithValue("@cmdstatus", "11");
                            cmd.Parameters.AddWithValue("@Class", ddl_class.SelectedValue);
                            cmd.Parameters.AddWithValue("@section", dd_section.Text);
                         


                    }


                    cmd.Parameters.AddWithValue("@startdate", code.ConvertStringToiDate(txt_startdate.Text));
                    cmd.Parameters.AddWithValue("@enddate", code.ConvertStringToiDate(txt_enddate.Text));

                    cmd.CommandText = "sp_VC_class_report";
                    DataTable dt = UsesCode.Getdata_sp(cmd);
                    if (Convert.ToString(dt.Rows.Count) == "0")
                    {
                        lbl_total.Text = "0";
                        Alert("Data Not Available");

                        RpDetailsStudent.DataSource = null;
                        RpDetailsStudent.DataBind();

                        pnl_view.Visible = false;

                    }
                    else
                    {
                        lbl_total.Text = dt.Rows.Count.ToString();
                        pnl_view.Visible = true;


                        RpDetailsStudent.DataSource = dt;
                        RpDetailsStudent.DataBind();
                    }

                }
                else
                {
                    Alert("Please select date valid");
                }


            }
        }

        protected void btn_find_admission_no_Click(object sender, EventArgs e)
        {
            if (txt_admissiono.Text == "")
            {
                Alert("Please enter admission no.");
            }
            else
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@studentid", txt_admissiono.Text);
                cmd.Parameters.AddWithValue("@cmdstatus", "12");

                cmd.CommandText = "sp_VC_class_report";
                DataTable dt = UsesCode.Getdata_sp(cmd);
                if (Convert.ToString(dt.Rows.Count) == "0")
                {
                    lbl_total.Text = "0";
                    Alert("Data Not Available");

                    RpDetailsStudent.DataSource = null;
                    RpDetailsStudent.DataBind();

                    pnl_view.Visible = false;

                }
                else
                {
                    lbl_total.Text = dt.Rows.Count.ToString();
                    pnl_view.Visible = true;


                    RpDetailsStudent.DataSource = dt;
                    RpDetailsStudent.DataBind();
                }
            }
        }
    }
}