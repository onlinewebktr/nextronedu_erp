using school_web.AppCode;
using school_web.AppCode.Exam;
using school_web.Examination_Admin.slip.general.api;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Examination_Admin.slip.toppers
{
    public partial class termwise_report_card : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["clss_id"] != null && Request.QueryString["ssion_id"] != null && Request.QueryString["Branch_id"] != null && Request.QueryString["Term_id"] != null && Request.QueryString["admNo"] != null)
                {
                    hd_adm_no.Value = Request.QueryString["admNo"];
                    hd_section.Value = Request.QueryString["Section"];
                    hd_class_id.Value = Request.QueryString["clss_id"];
                    hd_session_id.Value = Request.QueryString["ssion_id"];
                    hd_branch_id.Value = Request.QueryString["Branch_id"];
                    hd_term_id.Value = Request.QueryString["Term_id"];


                    string isMobile = "0";
                    try
                    {
                        if (Request.QueryString["ismob"].ToString() == "1")
                        {
                            isMobile = "1";
                            btn_back.Visible = false;
                            printBtns.Visible = true;


                            string isDues_show = rpcard_for_term_general.is_calculate_dues_for_report_card(hd_session_id.Value, hd_class_id.Value, hd_term_id.Value);
                            string[] stringSeparators = new string[] { "/" };
                            string[] arr = isDues_show.Split(stringSeparators, StringSplitOptions.None);
                            isDues_show = arr[0];
                            string dues_month_id = arr[1];
                            string dues_month_name = arr[2];
                            string dues_cal_type = arr[3];
                            string month_position = arr[4];


                            string dues_amt = "0";
                            if (isDues_show == "YES")
                            {
                                DataTable dtf = My.dataTable("select firm_id,Is_fine_repeat from Firm_Details");
                                string isFineRepeate = "No";
                                if (dtf.Rows[0]["Is_fine_repeat"].ToString() == "True")
                                {
                                    isFineRepeate = "Yes";
                                }

                                SqlConnection con = new SqlConnection(My.conn);
                                con.Open();
                                dues_update_headwise_transaction.update_student_dues(hd_session_id.Value, hd_class_id.Value, hd_adm_no.Value, dtf.Rows[0]["firm_id"].ToString(), isFineRepeate, con);
                                con.Close();
                                dues_amt = Exam_setting.get_dues_amt(hd_session_id.Value, hd_class_id.Value, hd_adm_no.Value, dues_month_id, dues_month_name, dues_cal_type, month_position);
                            }


                            if (My.toDouble(dues_amt) > 0)
                            {
                                pnl_dues.Visible = true;
                                reportcrdDV.Visible = false;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                    }

                    if (isMobile == "0")
                    {
                        try
                        {
                            if (Request.QueryString["RequestFrom"].ToString() == "1")
                            {
                                printBtns.Visible = false;
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }

                    try
                    {
                        if (Request.QueryString["RequestFrom"].ToString() == "Teacher")
                        {
                            hd_req_from.Value = "Teacher";
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            if (hd_req_from.Value == "Teacher")
            {
                Response.Redirect("../../../InstructorProfile/termwise-report-card.aspx", false);
            }
            else
            {
                Response.Redirect("../../student-result.aspx", false);
            }
        }
    }
}