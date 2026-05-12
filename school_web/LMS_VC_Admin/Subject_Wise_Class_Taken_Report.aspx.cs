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
    public partial class Subject_Wise_Class_Taken_Report : System.Web.UI.Page
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
                        code.bind_ddl_all1(dd_section, "Select distinct Section  from admission_registor  order by Section");
                        code.bind_all_ddl_with_all(ddl_subject, "Select csm.Subject_name, csm.Subject_id from Subject_Master csm   where csm.course_id='" + ddl_class.SelectedValue + "'  order by csm.Subject_position   ");
                        ddl_subject.Enabled = false;
                        ddl_subject.Text = "ALL";
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
                code.bind_ddl_all1(dd_section, "Select distinct Section  from admission_registor  order by Section");
            }
            else
            {
                code.bind_ddl_all1(dd_section, "Select distinct Section  from admission_registor where Class_id ='" + ddl_class.SelectedValue + "'    order by Section");
            }

        }
        protected void dd_section_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dd_section.SelectedItem.Text == "ALL")
            {
                if (ddl_class.SelectedItem.Text == "ALL")
                {
                    code.bind_all_ddl_with_all(ddl_subject, " Select distinct csm.Subject_name, csm.Subject_id  from Subject_Master csm  where csm.course_id='" + ddl_class.SelectedValue + "'  order by csm.Subject_position   ");
                    ddl_subject.Enabled = false;
                    ddl_subject.SelectedValue = "0";
                }
                else
                {
                    code.bind_all_ddl_with_all(ddl_subject, " Select distinct csm.Subject_name, csm.Subject_id  from Subject_Master csm  where csm.course_id='" + ddl_class.SelectedValue + "'  order by csm.Subject_position  ");
                    ddl_subject.Enabled = false;
                    ddl_subject.SelectedValue = "0";
                }
            
            }
            else
            {
                if (ddl_class.SelectedItem.Text != "ALL")
                {
                    if (dd_section.SelectedItem.Text == "ALL")
                    {
                        code.bind_all_ddl_with_all(ddl_subject, "  Select  csm.Subject_name, csm.Subject_id  from Subject_Master csm  where csm.course_id='" + ddl_class.SelectedValue + "'  order by csm.Subject_position    ");
                        ddl_subject.Enabled = false;
                        ddl_subject.SelectedValue = "0";
                    }
                    else
                    {
                        ddl_subject.Enabled = true;
                        code.bind_all_ddl_with_all(ddl_subject, " Select   csm.Subject_name, csm.Subject_id  from Subject_Master csm  where csm.course_id='" + ddl_class.SelectedValue + "'  order by csm.Subject_position   ");
                    }

                }
                else
                {
                    if (ddl_class.SelectedItem.Text == "ALL")
                    {
                        if (dd_section.SelectedItem.Text == "ALL")
                        {
                            code.bind_all_ddl_with_all(ddl_subject, "  Select   csm.Subject_name, csm.Subject_id  from Subject_Master csm  where csm.course_id='" + ddl_class.SelectedValue + "'  order by csm.Subject_position  ");
                            ddl_subject.Enabled = false;
                            ddl_subject.SelectedValue = "0";
                        }
                        else
                        {
                            code.bind_all_ddl_with_all(ddl_subject, " Select   csm.Subject_name, csm.Subject_id  from Subject_Master csm  where    order by csm.Subject_position  ");
                            ddl_subject.Enabled = false;
                            ddl_subject.SelectedValue = "0";
                        }
                    }
                    else
                    {
                        ddl_subject.Enabled = true;
                        code.bind_all_ddl_with_all(ddl_subject, " Select   csm.Subject_name, csm.Subject_id  from Subject_Master csm  where csm.course_id='" + ddl_class.SelectedValue + "'  order by csm.Subject_position ");
                    }
                }
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
                       
                         //   lbl_month_year.Text = "Class -" + ddl_class.SelectedItem.Text + "Section -" + dd_section.Text + "-" + "Start Date -" + txt_startdate.Text + "-" + "End Date -" + txt_enddate.Text;
                            lbl_month_year.Text = "Start Date -" + txt_startdate.Text + "-" + "End Date -" + txt_enddate.Text;
                            cmd.Parameters.AddWithValue("@cmdstatus", "3");
                       
                    }
                    else if (ddl_class.SelectedItem.Text != "ALL" && dd_section.Text == "ALL")
                    {
                      //  lbl_month_year.Text = "Class -" + ddl_class.SelectedItem.Text + "Section -" + dd_section.Text + "-" + "Start Date -" + txt_startdate.Text + "-" + "End Date -" + txt_enddate.Text;
                        lbl_month_year.Text = "Start Date -" + txt_startdate.Text + "-" + "End Date -" + txt_enddate.Text;
                        cmd.Parameters.AddWithValue("@cmdstatus", "4");
                        cmd.Parameters.AddWithValue("@Class", ddl_class.SelectedValue);

                    }
                    else if (ddl_class.SelectedItem.Text == "ALL" && dd_section.Text != "ALL")
                    {
                      //  lbl_month_year.Text = "Class -" + ddl_class.SelectedItem.Text + "Section -" + dd_section.Text + "-" + "Start Date -" + txt_startdate.Text + "-" + "End Date -" + txt_enddate.Text;
                        lbl_month_year.Text = "Start Date -" + txt_startdate.Text + "-" + "End Date -" + txt_enddate.Text;
                        cmd.Parameters.AddWithValue("@cmdstatus", "5");
                        cmd.Parameters.AddWithValue("@section", dd_section.Text);


                    }
                    else if (ddl_class.SelectedItem.Text != "ALL" && dd_section.Text != "ALL")
                    {
                        if (ddl_subject.SelectedItem.Text == "ALL")
                        {
                          //  lbl_month_year.Text = "Class -" + ddl_class.SelectedItem.Text + "Section -" + dd_section.Text + "-" + "Start Date -" + txt_startdate.Text + "-" + "End Date -" + txt_enddate.Text;
                            lbl_month_year.Text = "Start Date -" + txt_startdate.Text + "-" + "End Date -" + txt_enddate.Text;
                            cmd.Parameters.AddWithValue("@cmdstatus", "6");
                            cmd.Parameters.AddWithValue("@Class", ddl_class.SelectedValue);
                            cmd.Parameters.AddWithValue("@section", dd_section.Text);
                        }
                        else
                        {
                           // lbl_month_year.Text = "Class -" + ddl_class.SelectedItem.Text + "Section -" + dd_section.Text + "-" + "Sbject -" + ddl_subject.SelectedItem.Text + "-" + "Start Date -" + txt_startdate.Text + "-" + "End Date -" + txt_enddate.Text;
                            lbl_month_year.Text = "Start Date -" + txt_startdate.Text + "-" + "End Date -" + txt_enddate.Text;
                            cmd.Parameters.AddWithValue("@cmdstatus", "7");
                            cmd.Parameters.AddWithValue("@Class", ddl_class.SelectedValue);
                            cmd.Parameters.AddWithValue("@section", dd_section.Text);
                            cmd.Parameters.AddWithValue("@subjectid", ddl_subject.SelectedValue);
                        }

                        
                    }

                  
                    cmd.Parameters.AddWithValue("@startdate", code.ConvertStringToiDate(txt_startdate.Text));
                    cmd.Parameters.AddWithValue("@enddate", code.ConvertStringToiDate(txt_enddate.Text));

                    cmd.CommandText = "sp_VC_class_report";
                    DataTable dt = UsesCode.Getdata_sp(cmd);
                    if (Convert.ToString(dt.Rows.Count) == "0")
                    {
                        lbl_totalfranchise.Text = "0";
                        Alert("Data Not Available");

                        RpDetailsStudent.DataSource = null;
                        RpDetailsStudent.DataBind();

                        pnl_view.Visible = false;

                    }
                    else
                    {
                        lbl_totalfranchise.Text = dt.Rows.Count.ToString();
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

        


    }
}