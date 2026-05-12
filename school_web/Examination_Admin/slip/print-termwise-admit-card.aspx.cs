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

namespace school_web.Examination_Admin.slip
{
    public partial class print_termwise_admit_card : System.Web.UI.Page
    {
        My mycode = new My();
        Examination ec = new Examination();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["classid"] != null && Request.QueryString["examterm"] != null && Request.QueryString["section"] != null && Request.QueryString["examid"] != null)
                {
                    try
                    {
                        hd_session_id.Value = Request.QueryString["session_Id"].ToString();
                    }
                    catch 
                    {
                        hd_session_id.Value = Request.QueryString["sessionId"].ToString();
                    }

                    try
                    {
                        hd_branch_id.Value = Request.QueryString["branch_id"].ToString();
                    }
                    catch  
                    {
                        hd_branch_id.Value = Request.QueryString["branchid"].ToString();
                    }

                    hd_class_id.Value = Request.QueryString["classid"].ToString();
                    hd_term_id.Value = Request.QueryString["examterm"].ToString();
                    hd_section.Value = Request.QueryString["section"].ToString();
                    
                    hd_admission_no.Value = Request.QueryString["admin"].ToString();
                    hd_exam_id.Value = Request.QueryString["examid"].ToString();
                    hd_session_name.Value = mycode.get_session(hd_session_id.Value);
                    hd_page.Value = "1";
                    hd_print_from.Value = "0";
                    hd_checked.Value = "0";

                    try
                    {
                        if (Request.QueryString["checked"] != null)
                        {
                            hd_checked.Value = Request.QueryString["checked"].ToString();
                            if (hd_checked.Value == "1")
                            {
                                hd_admission_no.Value = hd_admission_no.Value.Remove(hd_admission_no.Value.Length - 1);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                    }


                    string isMobile = "0";
                    try
                    {
                        if (Request.QueryString["ismob"].ToString() == "1")
                        {
                            isMobile = "1";
                            btn_back.Visible = false;
                            printBtns.Visible = true;


                            string isDues_show = rpcard_for_term_general.is_calculate_dues_for_admit_card(hd_session_id.Value, hd_class_id.Value, hd_term_id.Value);
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
                                dues_update_headwise_transaction.update_student_dues(hd_session_id.Value, hd_class_id.Value, hd_admission_no.Value, dtf.Rows[0]["firm_id"].ToString(), isFineRepeate, con);
                                con.Close();
                                dues_amt = Exam_setting.get_dues_amt(hd_session_id.Value, hd_class_id.Value, hd_admission_no.Value, dues_month_id, dues_month_name, dues_cal_type, month_position);
                            } 

                            if (My.toDouble(dues_amt) > 0)
                            {
                                pnl_dues.Visible = true; 
                                admitcrdDV.Visible = false;
                            }


                            string isAttendanceNeeded= rpcard_for_term_general.isAttendanceNeeded(hd_session_id.Value, hd_class_id.Value, hd_term_id.Value, hd_exam_id.Value, hd_admission_no.Value);

                            if (isAttendanceNeeded == "0")
                            {
                                pnl_dues.Visible = true;
                                admitcrdDV.Visible = false;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        btn_back.Visible = true;
                        printBtns.Visible = true;
                        pnl_dues.Visible =false;
                        admitcrdDV.Visible = true;

                    }


                    hd_user_type.Value = "User";
                    if (isMobile == "0")
                    {
                        try
                        {
                            if (Request.QueryString["from"] != null)
                            {
                                hd_print_from.Value = Request.QueryString["from"].ToString();
                                if (hd_print_from.Value == "stdwise")
                                {
                                    lnk_back.Visible = false;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                        }

                        try
                        {
                            hd_user_type.Value = Session["userType"].ToString();
                        }
                        catch (Exception ex)
                        {
                        }
                    }



                    fetch_imp_note();
                }
                else
                {
                }
            }
        }

        private void fetch_imp_note()
        {
            DataTable dt = My.dataTable("select * from Exam_admitcard_guideline  where Session_id=" + hd_session_id.Value + " and Class_id=" + hd_class_id.Value + " and Exam_id='" + hd_exam_id.Value + "'");
            if (dt.Rows.Count > 0)
            {
                lbl_imp_note.Text = dt.Rows[0]["Guideline"].ToString();
            }
        }
        protected void lnk_back_Click(object sender, EventArgs e)
        {
            if (hd_print_from.Value == "stdwise")
            {
                Response.Redirect("~/Examination_Admin/admit-card-student-wise.aspx", false);
            }
            else
            {
                Response.Redirect("~/Examination_Admin/Exam_Time_Table_List.aspx", false);
            }
        }
    }
}