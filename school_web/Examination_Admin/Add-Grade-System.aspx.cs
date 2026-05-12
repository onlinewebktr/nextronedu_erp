using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Examination_Admin
{
    public partial class Add_Grade_System : System.Web.UI.Page
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
                    ViewState["sessionid"] = My.get_session_id();

                    ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());


                    if (Request.QueryString["Grade_System_Id"] != null)
                    {
                        ViewState["Grade_System_Id"] = Request.QueryString["Grade_System_Id"].ToString();

                        process_active("1");

                        Bind_Data_edit();
                    }
                    else
                    {

                        ViewState["Grade_System_Id"] = Examination.auto_serialS("Grade_System_Id", ViewState["branchid"].ToString());
                        withdecimal.Visible = true;
                        rd_Round_up.Checked = true;

                        rd_Round_upPer.Checked = true;
                        rd_With_DecimalPer.Checked = true;
                        withdecimal_per.Visible = true;
                        process_active("1");
                    }


                    Bind_grade_range();
                    Bind_class_list();
                }
            }
        }

        private void Bind_Data_edit()
        {
            string query = "Select * from Exam_Grade_System where  Session_Id='" + ViewState["sessionid"].ToString() + "' and Branch_id='" + ViewState["branchid"].ToString() + "' and Grade_System_Id='" + ViewState["Grade_System_Id"].ToString() + "'";

            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {

            }
            else
            {





                txt_Name.Text = dt.Rows[0]["Grade_Name"].ToString();
                ddl_input.Text = dt.Rows[0]["Input_Type"].ToString();
                ddl_output.Text = dt.Rows[0]["Output"].ToString();
                ddl_output.Text = dt.Rows[0]["Output"].ToString();

                if (dt.Rows[0]["With_Decimal"].ToString() == "True")
                {
                    withdecimal.Visible = true;

                    rd_With_Decimal.Checked = true;




                    if (dt.Rows[0]["Round_up"].ToString() == "True")
                    {

                        rd_Round_up.Checked = true;

                        width_decimal_Round_up.Visible = true;
                        width_decimal_Round_down.Visible = false;
                        width_decimal_Half_Round_Up.Visible = false;
                        width_decimal_Half_Round_down.Visible = false;



                        Without_decimal_Round_up.Visible = false;
                        Without_decimal_Round_down.Visible = false;
                        Without_decimal_half_round_up.Visible = false;
                        Without_decimal_half_round_down.Visible = false;

                    }
                    else
                    {
                        rd_Round_up.Checked = false;
                    }

                    if (dt.Rows[0]["Round_down"].ToString() == "True")
                    {
                        width_decimal_Round_up.Visible = false;
                        width_decimal_Round_down.Visible = true;
                        width_decimal_Half_Round_Up.Visible = false;
                        width_decimal_Half_Round_down.Visible = false;



                        Without_decimal_Round_up.Visible = false;
                        Without_decimal_Round_down.Visible = false;
                        Without_decimal_half_round_up.Visible = false;
                        Without_decimal_half_round_down.Visible = false;

                        rd_Round_down.Checked = true;
                    }
                    else
                    {
                        rd_Round_down.Checked = false;
                    }

                    if (dt.Rows[0]["Half_Round_Up"].ToString() == "True")
                    {

                        width_decimal_Round_up.Visible = false;
                        width_decimal_Round_down.Visible = false;
                        width_decimal_Half_Round_Up.Visible = true;
                        width_decimal_Half_Round_down.Visible = false;



                        Without_decimal_Round_up.Visible = false;
                        Without_decimal_Round_down.Visible = false;
                        Without_decimal_half_round_up.Visible = false;
                        Without_decimal_half_round_down.Visible = false;




                        rd_Half_Round_Up.Checked = true;
                    }
                    else
                    {
                        rd_Half_Round_Up.Checked = false;
                    }
                    if (dt.Rows[0]["Half_Round_Down"].ToString() == "True")
                    {
                        width_decimal_Round_up.Visible = false;
                        width_decimal_Round_down.Visible = false;
                        width_decimal_Half_Round_Up.Visible = false;
                        width_decimal_Half_Round_down.Visible = true;


                        Without_decimal_Round_up.Visible = false;
                        Without_decimal_Round_down.Visible = false;
                        Without_decimal_half_round_up.Visible = false;
                        Without_decimal_half_round_down.Visible = false;

                        rd_Half_Round_Down.Checked = true;
                    }
                    else
                    {
                        rd_Half_Round_Down.Checked = false;
                    }






                }
                else
                {
                    withdecimal.Visible = false;
                    rd_With_Decimal.Checked = false;
                }

                if (dt.Rows[0]["Without_Decimal"].ToString() == "True")
                {

                    rd_Without_Decimal.Checked = true;


                    if (dt.Rows[0]["Round_up"].ToString() == "True")
                    {

                        rd_Round_up.Checked = true;

                        width_decimal_Round_up.Visible = false;
                        width_decimal_Round_down.Visible = false;
                        width_decimal_Half_Round_Up.Visible = false;
                        width_decimal_Half_Round_down.Visible = false;



                        Without_decimal_Round_up.Visible = true;
                        Without_decimal_Round_down.Visible = false;
                        Without_decimal_half_round_up.Visible = false;
                        Without_decimal_half_round_down.Visible = false;

                    }
                    else
                    {
                        rd_Round_up.Checked = false;
                    }

                    if (dt.Rows[0]["Round_down"].ToString() == "True")
                    {
                        width_decimal_Round_up.Visible = false;
                        width_decimal_Round_down.Visible = false;
                        width_decimal_Half_Round_Up.Visible = false;
                        width_decimal_Half_Round_down.Visible = false;



                        Without_decimal_Round_up.Visible = false;
                        Without_decimal_Round_down.Visible = true;
                        Without_decimal_half_round_up.Visible = false;
                        Without_decimal_half_round_down.Visible = false;

                        rd_Round_down.Checked = true;
                    }
                    else
                    {
                        rd_Round_down.Checked = false;
                    }

                    if (dt.Rows[0]["Half_Round_Up"].ToString() == "True")
                    {

                        width_decimal_Round_up.Visible = false;
                        width_decimal_Round_down.Visible = true;
                        width_decimal_Half_Round_Up.Visible = false;
                        width_decimal_Half_Round_down.Visible = false;



                        Without_decimal_Round_up.Visible = false;
                        Without_decimal_Round_down.Visible = false;
                        Without_decimal_half_round_up.Visible = true;
                        Without_decimal_half_round_down.Visible = false;




                        rd_Half_Round_Up.Checked = true;
                    }
                    else
                    {
                        rd_Half_Round_Up.Checked = false;
                    }
                    if (dt.Rows[0]["Half_Round_Down"].ToString() == "True")
                    {
                        width_decimal_Round_up.Visible = false;
                        width_decimal_Round_down.Visible = false;
                        width_decimal_Half_Round_Up.Visible = false;
                        width_decimal_Half_Round_down.Visible = true;


                        Without_decimal_Round_up.Visible = false;
                        Without_decimal_Round_down.Visible = false;
                        Without_decimal_half_round_up.Visible = false;
                        Without_decimal_half_round_down.Visible = true;

                        rd_Half_Round_Down.Checked = true;
                    }
                    else
                    {
                        rd_Half_Round_Down.Checked = false;
                    }



                }
                else
                {
                    rd_Without_Decimal.Checked = false;
                }



                //---------------------Percentage--------------------------------

                if (dt.Rows[0]["With_Decimal_Per"].ToString() == "True")
                {
                    withdecimal_per.Visible = true;
                    rd_With_DecimalPer.Checked = true;



                    if (dt.Rows[0]["Round_up_Per"].ToString() == "True")
                    {

                        rd_Round_upPer.Checked = true;

                        width_decimal_Round_upPer.Visible = true;
                        width_decimal_Round_downPer.Visible = false;
                        width_decimal_Half_Round_UpPer.Visible = false;
                        width_decimal_Half_Round_downPer.Visible = false;



                        Without_decimal_Round_upPer.Visible = false;
                        Without_decimal_Round_downPer.Visible = false;
                        Without_decimal_half_round_upPer.Visible = false;
                        Without_decimal_half_round_downPer.Visible = false;

                    }
                    else
                    {
                        rd_Round_upPer.Checked = false;
                    }

                    if (dt.Rows[0]["Round_down_Per"].ToString() == "True")
                    {
                        width_decimal_Round_upPer.Visible = false;
                        width_decimal_Round_downPer.Visible = true;
                        width_decimal_Half_Round_UpPer.Visible = false;
                        width_decimal_Half_Round_downPer.Visible = false;



                        Without_decimal_Round_upPer.Visible = false;
                        Without_decimal_Round_downPer.Visible = false;
                        Without_decimal_half_round_upPer.Visible = false;
                        Without_decimal_half_round_downPer.Visible = false;

                        rd_Round_downPer.Checked = true;
                    }
                    else
                    {
                        rd_Round_downPer.Checked = false;
                    }

                    if (dt.Rows[0]["Half_Round_Up_Per"].ToString() == "True")
                    {

                        width_decimal_Round_upPer.Visible = false;
                        width_decimal_Round_downPer.Visible = false;
                        width_decimal_Half_Round_UpPer.Visible = true;
                        width_decimal_Half_Round_downPer.Visible = false;



                        Without_decimal_Round_upPer.Visible = false;
                        Without_decimal_Round_downPer.Visible = false;
                        Without_decimal_half_round_upPer.Visible = false;
                        Without_decimal_half_round_downPer.Visible = false;




                        rd_Half_Round_UpPer.Checked = true;
                    }
                    else
                    {
                        rd_Half_Round_UpPer.Checked = false;
                    }
                    if (dt.Rows[0]["Half_Round_Down_Per"].ToString() == "True")
                    {
                        width_decimal_Round_upPer.Visible = false;
                        width_decimal_Round_downPer.Visible = false;
                        width_decimal_Half_Round_UpPer.Visible = false;
                        width_decimal_Half_Round_downPer.Visible = true;


                        Without_decimal_Round_upPer.Visible = false;
                        Without_decimal_Round_downPer.Visible = false;
                        Without_decimal_half_round_upPer.Visible = false;
                        Without_decimal_half_round_downPer.Visible = false;

                        rd_Half_Round_DownPer.Checked = true;
                    }
                    else
                    {
                        rd_Half_Round_DownPer.Checked = false;
                    }

                }
                else
                {
                    withdecimal_per.Visible = false;
                    rd_With_DecimalPer.Checked = false;
                }



                if (dt.Rows[0]["Without_Decimal_Per"].ToString() == "True")
                {

                    rd_Without_DecimalPer.Checked = true;



                    if (dt.Rows[0]["Round_up_Per"].ToString() == "True")
                    {

                        rd_Round_upPer.Checked = true;

                        width_decimal_Round_upPer.Visible = false;
                        width_decimal_Round_downPer.Visible = false;
                        width_decimal_Half_Round_UpPer.Visible = false;
                        width_decimal_Half_Round_downPer.Visible = false;



                        Without_decimal_Round_upPer.Visible = true;
                        Without_decimal_Round_downPer.Visible = false;
                        Without_decimal_half_round_upPer.Visible = false;
                        Without_decimal_half_round_downPer.Visible = false;

                    }
                    else
                    {
                        rd_Round_upPer.Checked = false;
                    }

                    if (dt.Rows[0]["Round_down_Per"].ToString() == "True")
                    {
                        width_decimal_Round_upPer.Visible = false;
                        width_decimal_Round_downPer.Visible = false;
                        width_decimal_Half_Round_UpPer.Visible = false;
                        width_decimal_Half_Round_downPer.Visible = false;



                        Without_decimal_Round_upPer.Visible = false;
                        Without_decimal_Round_downPer.Visible = true;
                        Without_decimal_half_round_upPer.Visible = false;
                        Without_decimal_half_round_downPer.Visible = false;

                        rd_Round_downPer.Checked = true;
                    }
                    else
                    {
                        rd_Round_downPer.Checked = false;
                    }

                    if (dt.Rows[0]["Half_Round_Up_Per"].ToString() == "True")
                    {

                        width_decimal_Round_upPer.Visible = false;
                        width_decimal_Round_downPer.Visible = false;
                        width_decimal_Half_Round_UpPer.Visible = false;
                        width_decimal_Half_Round_downPer.Visible = false;



                        Without_decimal_Round_upPer.Visible = false;
                        Without_decimal_Round_downPer.Visible = false;
                        Without_decimal_half_round_upPer.Visible = true;
                        Without_decimal_half_round_downPer.Visible = false;




                        rd_Half_Round_UpPer.Checked = true;
                    }
                    else
                    {
                        rd_Half_Round_UpPer.Checked = false;
                    }
                    if (dt.Rows[0]["Half_Round_Down_Per"].ToString() == "True")
                    {
                        width_decimal_Round_upPer.Visible = false;
                        width_decimal_Round_downPer.Visible = false;
                        width_decimal_Half_Round_UpPer.Visible = false;
                        width_decimal_Half_Round_downPer.Visible = false;


                        Without_decimal_Round_upPer.Visible = false;
                        Without_decimal_Round_downPer.Visible = false;
                        Without_decimal_half_round_upPer.Visible = false;
                        Without_decimal_half_round_downPer.Visible = true;

                        rd_Half_Round_DownPer.Checked = true;
                    }
                    else
                    {
                        rd_Half_Round_DownPer.Checked = false;
                    }


                }
                else
                {
                    rd_Without_DecimalPer.Checked = false;
                }



                if (dt.Rows[0]["Round_Percentage_Checked"].ToString() == "True")
                {
                    chk_per.Checked = true;

                }
                else
                {
                    chk_per.Checked = false;
                }




            }
        }

        private void Bind_class_list()
        {
            string query = "Select * from Add_course_table  order by Position asc";

            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {

                datalist_classgrid.DataSource = null;
                datalist_classgrid.DataBind();

            }
            else
            {

                datalist_classgrid.DataSource = dt;
                datalist_classgrid.DataBind();
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
        private void Bind_grade_range()
        {
            string query = "Select * from Exam_Grade_System_Range_Grade where Grade_System_Id=" + ViewState["Grade_System_Id"].ToString() + " and Branch_Id=" + ViewState["branchid"].ToString() + " and Session_Id=" + ViewState["sessionid"].ToString() + " order by id asc";

            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {

                grid_range.DataSource = null;
                grid_range.DataBind();

            }
            else
            {

                grid_range.DataSource = dt;
                grid_range.DataBind();
            }



        }

        private void process_active(string type)
        {
            if (type == "1")
            {
                a1.Attributes.Add("class", "stepper-item active");
                a2.Attributes.Add("class", "stepper-item");
                a3.Attributes.Add("class", "stepper-item");






                pnl_Basic.Visible = true;
                pnl_Define_Logic.Visible = false;
                pn_MapClass.Visible = false;

            }
            else if (type == "2")
            {
                a1.Attributes.Add("class", "stepper-item completed");
                a2.Attributes.Add("class", "stepper-item active");
                a3.Attributes.Add("class", "stepper-item");

                pnl_Basic.Visible = false;
                pnl_Define_Logic.Visible = true;
                pn_MapClass.Visible = false;

            }
            else if (type == "3")
            {
                a1.Attributes.Add("class", "stepper-item completed");
                a2.Attributes.Add("class", "stepper-item completed");
                a3.Attributes.Add("class", "stepper-item active");




                pnl_Basic.Visible = false;
                pnl_Define_Logic.Visible = false;
                pn_MapClass.Visible = true;

            }


        }



        #region Basic
        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("grade-system.aspx", false);
        }
        protected void btn_next_1_2_Click(object sender, EventArgs e)
        {
            DataTable dt1 = mycode.FillData("Select * from Exam_Grade_System where  Branch_id=" + ViewState["branchid"].ToString() + " and Session_Id=" + ViewState["sessionid"].ToString() + "  and  Grade_Name='" + txt_Name.Text + "'");
            if (dt1.Rows.Count == 0)
            {

                process_active("2");
            }

            else
            {
                DataTable dt2 = mycode.FillData("Select * from Exam_Grade_System where  Branch_id=" + ViewState["branchid"].ToString() + " and Session_Id=" + ViewState["sessionid"].ToString() + "  and  Grade_Name='" + txt_Name.Text + "' and Grade_System_Id!=" + ViewState["Grade_System_Id"].ToString() + "");
                if (dt2.Rows.Count == 0)
                {
                    process_active("2");
                }
                else
                {
                    Alertme("Sorry your basic grade name already exists", "success");
                }


            }


        }
        #endregion


        #region Define_Logic

        protected void rd_With_Decimal_CheckedChanged(object sender, EventArgs e)
        {
            withdecimal.Visible = true;


            if (rd_Round_up.Checked == true)
            {
                width_decimal_Round_up.Visible = true;
                width_decimal_Round_down.Visible = false;
                width_decimal_Half_Round_Up.Visible = false;
                width_decimal_Half_Round_down.Visible = false;


                Without_decimal_Round_up.Visible = false;
                Without_decimal_Round_down.Visible = false;
                Without_decimal_half_round_up.Visible = false;
                Without_decimal_half_round_down.Visible = false;

            }
            else if (rd_Round_down.Checked == true)
            {
                width_decimal_Round_up.Visible = false;
                width_decimal_Round_down.Visible = true;
                width_decimal_Half_Round_Up.Visible = false;
                width_decimal_Half_Round_down.Visible = false;


                Without_decimal_Round_up.Visible = false;
                Without_decimal_Round_down.Visible = false;
                Without_decimal_half_round_up.Visible = false;
                Without_decimal_half_round_down.Visible = false;

            }
            else if (rd_Half_Round_Up.Checked == true)
            {
                width_decimal_Round_up.Visible = false;
                width_decimal_Round_down.Visible = false;
                width_decimal_Half_Round_Up.Visible = true;
                width_decimal_Half_Round_down.Visible = false;


                Without_decimal_Round_up.Visible = false;
                Without_decimal_Round_down.Visible = false;
                Without_decimal_half_round_up.Visible = false;
                Without_decimal_half_round_down.Visible = false;

            }
            else if (rd_Half_Round_Down.Checked == true)
            {
                width_decimal_Round_up.Visible = false;
                width_decimal_Round_down.Visible = false;
                width_decimal_Half_Round_Up.Visible = false;
                width_decimal_Half_Round_down.Visible = true;


                Without_decimal_Round_up.Visible = false;
                Without_decimal_Round_down.Visible = false;
                Without_decimal_half_round_up.Visible = false;
                Without_decimal_half_round_down.Visible = false;

            }

        }

        protected void rd_Without_Decimal_CheckedChanged(object sender, EventArgs e)
        {
            withdecimal.Visible = false;
            txt_with_decimal.Text = "0";



            if (rd_Round_up.Checked == true)
            {
                width_decimal_Round_up.Visible = false;
                width_decimal_Round_down.Visible = false;
                width_decimal_Half_Round_Up.Visible = false;
                width_decimal_Half_Round_down.Visible = false;


                Without_decimal_Round_up.Visible = true;
                Without_decimal_Round_down.Visible = false;
                Without_decimal_half_round_up.Visible = false;
                Without_decimal_half_round_down.Visible = false;

            }
            else if (rd_Round_down.Checked == true)
            {
                width_decimal_Round_up.Visible = false;
                width_decimal_Round_down.Visible = false;
                width_decimal_Half_Round_Up.Visible = false;
                width_decimal_Half_Round_down.Visible = false;


                Without_decimal_Round_up.Visible = false;
                Without_decimal_Round_down.Visible = true;
                Without_decimal_half_round_up.Visible = false;
                Without_decimal_half_round_down.Visible = false;

            }
            else if (rd_Half_Round_Up.Checked == true)
            {
                width_decimal_Round_up.Visible = false;
                width_decimal_Round_down.Visible = false;
                width_decimal_Half_Round_Up.Visible = false;
                width_decimal_Half_Round_down.Visible = false;


                Without_decimal_Round_up.Visible = false;
                Without_decimal_Round_down.Visible = false;
                Without_decimal_half_round_up.Visible = true;
                Without_decimal_half_round_down.Visible = false;

            }
            else if (rd_Half_Round_Down.Checked == true)
            {
                width_decimal_Round_up.Visible = false;
                width_decimal_Round_down.Visible = false;
                width_decimal_Half_Round_Up.Visible = false;
                width_decimal_Half_Round_down.Visible = false;


                Without_decimal_Round_up.Visible = false;
                Without_decimal_Round_down.Visible = false;
                Without_decimal_half_round_up.Visible = false;
                Without_decimal_half_round_down.Visible = true;

            }

        }

        protected void rd_Round_up_CheckedChanged(object sender, EventArgs e)
        {
            if (rd_With_Decimal.Checked == true)
            {

                width_decimal_Round_up.Visible = true;
                width_decimal_Round_down.Visible = false;
                width_decimal_Half_Round_Up.Visible = false;
                width_decimal_Half_Round_down.Visible = false;


                Without_decimal_Round_up.Visible = false;
                Without_decimal_Round_down.Visible = false;
                Without_decimal_half_round_up.Visible = false;
                Without_decimal_half_round_down.Visible = false;


            }
            else
            {
                width_decimal_Round_up.Visible = false;
                width_decimal_Round_down.Visible = false;
                width_decimal_Half_Round_Up.Visible = false;
                width_decimal_Half_Round_down.Visible = false;

                Without_decimal_Round_up.Visible = true;
                Without_decimal_Round_down.Visible = false;
                Without_decimal_half_round_up.Visible = false;
                Without_decimal_half_round_down.Visible = false;



            }




        }

        protected void rd_Round_down_CheckedChanged(object sender, EventArgs e)
        {

            if (rd_With_Decimal.Checked == true)
            {

                width_decimal_Round_up.Visible = false;
                width_decimal_Round_down.Visible = true;
                width_decimal_Half_Round_Up.Visible = false;
                width_decimal_Half_Round_down.Visible = false;


                Without_decimal_Round_up.Visible = false;
                Without_decimal_Round_down.Visible = false;
                Without_decimal_half_round_up.Visible = false;
                Without_decimal_half_round_down.Visible = false;


            }
            else
            {
                width_decimal_Round_up.Visible = false;
                width_decimal_Round_down.Visible = false;
                width_decimal_Half_Round_Up.Visible = false;
                width_decimal_Half_Round_down.Visible = false;

                Without_decimal_Round_up.Visible = false;
                Without_decimal_Round_down.Visible = true;
                Without_decimal_half_round_up.Visible = false;
                Without_decimal_half_round_down.Visible = false;



            }


        }

        protected void rd_Half_Round_Up_CheckedChanged(object sender, EventArgs e)
        {
            if (rd_With_Decimal.Checked == true)
            {

                width_decimal_Round_up.Visible = false;
                width_decimal_Round_down.Visible = false;
                width_decimal_Half_Round_Up.Visible = true;
                width_decimal_Half_Round_down.Visible = false;


                Without_decimal_Round_up.Visible = false;
                Without_decimal_Round_down.Visible = false;
                Without_decimal_half_round_up.Visible = false;
                Without_decimal_half_round_down.Visible = false;


            }
            else
            {
                width_decimal_Round_up.Visible = false;
                width_decimal_Round_down.Visible = false;
                width_decimal_Half_Round_Up.Visible = false;
                width_decimal_Half_Round_down.Visible = false;

                Without_decimal_Round_up.Visible = false;
                Without_decimal_Round_down.Visible = false;
                Without_decimal_half_round_up.Visible = true;
                Without_decimal_half_round_down.Visible = false;



            }
        }

        protected void rd_Half_Round_Down_CheckedChanged(object sender, EventArgs e)
        {
            if (rd_With_Decimal.Checked == true)
            {

                width_decimal_Round_up.Visible = false;
                width_decimal_Round_down.Visible = false;
                width_decimal_Half_Round_Up.Visible = false;
                width_decimal_Half_Round_down.Visible = true;


                Without_decimal_Round_up.Visible = false;
                Without_decimal_Round_down.Visible = false;
                Without_decimal_half_round_up.Visible = false;
                Without_decimal_half_round_down.Visible = false;


            }
            else
            {
                width_decimal_Round_up.Visible = false;
                width_decimal_Round_down.Visible = false;
                width_decimal_Half_Round_Up.Visible = false;
                width_decimal_Half_Round_down.Visible = false;

                Without_decimal_Round_up.Visible = false;
                Without_decimal_Round_down.Visible = false;
                Without_decimal_half_round_up.Visible = false;
                Without_decimal_half_round_down.Visible = true;



            }
        }

        protected void btn_back_2_1_Click(object sender, EventArgs e)
        {
            process_active("1");
        }

        protected void btn_Next_2_3_Click(object sender, EventArgs e)
        {
            if (rd_With_Decimal.Checked == false && rd_Without_Decimal.Checked == false)
            {
                Alertme("Please select round off", "warning");
            }
            if (rd_Round_up.Checked == false && rd_Round_down.Checked == false && rd_Half_Round_Up.Checked == false && rd_Half_Round_Down.Checked == false)
            {
                Alertme("Please select Round-up,Round-down,Half Round Up or Half Round Down", "warning");
            }

            else
            {
                if (grid_range.Rows.Count == 0)
                {
                    Alertme("Please add grade range", "warning");
                }
                else
                {
                    pn_MapClass.Visible = true;
                    process_active("3");
                }


            }




        }
        #endregion



        #region MapClass
        protected void btn_back_3_2_Click(object sender, EventArgs e)
        {
            process_active("2");
        }


        protected void btn_final_submit_Click(object sender, EventArgs e)
        {
            ViewState["deletemode"] = "0";
            SqlCommand cmd;

            DataTable dt1 = mycode.FillData("Select * from Exam_Grade_System where  Branch_id=" + ViewState["branchid"].ToString() + " and Session_Id=" + ViewState["sessionid"].ToString() + "  and Grade_System_Id=" + ViewState["Grade_System_Id"].ToString() + "");
            if (dt1.Rows.Count == 0)
            {
                int i = 0, j = 0;
                foreach (DataListItem item in datalist_classgrid.Items)
                {

                    CheckBox chk_per = (CheckBox)item.FindControl("chk_per");
                    if (chk_per.Checked == true)
                    {


                        if (ViewState["deletemode"].ToString() == "0")
                        {
                            mycode.executequery("delete from Exam_Grade_System_Mapping_with_Class where Grade_System_Id=" + ViewState["Grade_System_Id"].ToString() + "");
                        }
                        Label lbl_classid = (Label)item.FindControl("lbl_classid");
                        add_update_Exam_Grade_System_Mapping_with_Class(lbl_classid.Text);

                        ViewState["deletemode"] = "1";
                    }
                    else
                    {
                        j++;
                    }
                    i++;

                }
                if (i == j)
                {
                    Alertme("Please select class name", "warning");
                }
                else
                {

                    #region add
                    string query = "INSERT INTO Exam_Grade_System (Session_Id,Grade_Name,Input_Type,Output,Grade_System_Id,With_Decimal,Without_Decimal,Round_up,Round_down,Half_Round_Up,Half_Round_Down,With_Decimal_Per,Without_Decimal_Per,Round_up_Per,Round_down_Per,Half_Round_Up_Per,Half_Round_Down_Per,Branch_id,Created_by,Created_date,Round_Percentage_Checked) values (@Session_Id,@Grade_Name,@Input_Type,@Output,@Grade_System_Id,@With_Decimal,@Without_Decimal,@Round_up,@Round_down,@Half_Round_Up,@Half_Round_Down,@With_Decimal_Per,@Without_Decimal_Per,@Round_up_Per,@Round_down_Per,@Half_Round_Up_Per,@Half_Round_Down_Per,@Branch_id,@Created_by,@Created_date,@Round_Percentage_Checked)";
                    cmd = new SqlCommand(query);


                    cmd.Parameters.AddWithValue("@Session_Id", ViewState["sessionid"].ToString());
                    cmd.Parameters.AddWithValue("@Grade_Name", txt_Name.Text);
                    cmd.Parameters.AddWithValue("@Input_Type", ddl_input.Text);
                    cmd.Parameters.AddWithValue("@Output", ddl_output.Text);
                    cmd.Parameters.AddWithValue("@Grade_System_Id", ViewState["Grade_System_Id"].ToString());


                    if (rd_With_Decimal.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@With_Decimal", 1);
                        cmd.Parameters.AddWithValue("@Without_Decimal", 0);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@With_Decimal", 0);
                        cmd.Parameters.AddWithValue("@Without_Decimal", 1);
                    }

                    if (rd_Round_up.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@Round_up", 1);
                        cmd.Parameters.AddWithValue("@Round_down", 0);
                        cmd.Parameters.AddWithValue("@Half_Round_Up", 0);
                        cmd.Parameters.AddWithValue("@Half_Round_Down", 0);
                    }
                    else if (rd_Round_down.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@Round_down", 1);
                        cmd.Parameters.AddWithValue("@Round_up", 0);
                        cmd.Parameters.AddWithValue("@Half_Round_Up", 0);
                        cmd.Parameters.AddWithValue("@Half_Round_Down", 0);
                    }
                    else if (rd_Half_Round_Up.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@Half_Round_Up", 1);

                        cmd.Parameters.AddWithValue("@Round_down", 0);
                        cmd.Parameters.AddWithValue("@Round_up", 0);
                        cmd.Parameters.AddWithValue("@Half_Round_Down", 0);

                    }
                    else if (rd_Half_Round_Down.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@Half_Round_Down", 1);
                        cmd.Parameters.AddWithValue("@Half_Round_Up", 0);
                        cmd.Parameters.AddWithValue("@Round_down", 0);
                        cmd.Parameters.AddWithValue("@Round_up", 0);
                    }


                    if (chk_per.Checked)
                    {
                        cmd.Parameters.AddWithValue("@Round_Percentage_Checked", 1);

                        if (rd_With_Decimal.Checked == true)
                        {
                            cmd.Parameters.AddWithValue("@With_Decimal_Per", 1);
                            cmd.Parameters.AddWithValue("@Without_Decimal_Per", 0);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@With_Decimal_Per", 0);
                            cmd.Parameters.AddWithValue("@Without_Decimal_Per", 1);
                        }
                        if (rd_Round_up.Checked == true)
                        {
                            cmd.Parameters.AddWithValue("@Round_up_Per", 1);
                            cmd.Parameters.AddWithValue("@Round_down_Per", 0);
                            cmd.Parameters.AddWithValue("@Half_Round_Up_Per", 0);
                            cmd.Parameters.AddWithValue("@Half_Round_Down_Per", 0);
                        }
                        else if (rd_Round_down.Checked == true)
                        {
                            cmd.Parameters.AddWithValue("@Round_down_Per", 1);
                            cmd.Parameters.AddWithValue("@Round_up_Per", 0);
                            cmd.Parameters.AddWithValue("@Half_Round_Up_Per", 0);
                            cmd.Parameters.AddWithValue("@Half_Round_Down_Per", 0);
                        }
                        else if (rd_Half_Round_Up.Checked == true)
                        {
                            cmd.Parameters.AddWithValue("@Half_Round_Up_Per", 1);

                            cmd.Parameters.AddWithValue("@Round_down_Per", 0);
                            cmd.Parameters.AddWithValue("@Round_up_Per", 0);
                            cmd.Parameters.AddWithValue("@Half_Round_Down_Per", 0);

                        }
                        else if (rd_Half_Round_Down.Checked == true)
                        {
                            cmd.Parameters.AddWithValue("@Half_Round_Down_Per", 1);
                            cmd.Parameters.AddWithValue("@Half_Round_Up_Per", 0);
                            cmd.Parameters.AddWithValue("@Round_down_Per", 0);
                            cmd.Parameters.AddWithValue("@Round_up_Per", 0);
                        }

                    }
                    else
                    {
                        if (rd_With_DecimalPer.Checked == true)
                        {
                            cmd.Parameters.AddWithValue("@With_Decimal_Per", 1);
                            cmd.Parameters.AddWithValue("@Without_Decimal_Per", 0);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@With_Decimal_Per", 0);
                            cmd.Parameters.AddWithValue("@Without_Decimal_Per", 1);
                        }
                        if (rd_Round_upPer.Checked == true)
                        {
                            cmd.Parameters.AddWithValue("@Round_up_Per", 1);
                            cmd.Parameters.AddWithValue("@Round_down_Per", 0);
                            cmd.Parameters.AddWithValue("@Half_Round_Up_Per", 0);
                            cmd.Parameters.AddWithValue("@Half_Round_Down_Per", 0);
                        }
                        else if (rd_Round_downPer.Checked == true)
                        {
                            cmd.Parameters.AddWithValue("@Round_down_Per", 1);
                            cmd.Parameters.AddWithValue("@Round_up_Per", 0);
                            cmd.Parameters.AddWithValue("@Half_Round_Up_Per", 0);
                            cmd.Parameters.AddWithValue("@Half_Round_Down_Per", 0);
                        }
                        else if (rd_Half_Round_UpPer.Checked == true)
                        {
                            cmd.Parameters.AddWithValue("@Half_Round_Up_Per", 1);

                            cmd.Parameters.AddWithValue("@Round_down_Per", 0);
                            cmd.Parameters.AddWithValue("@Round_up_Per", 0);
                            cmd.Parameters.AddWithValue("@Half_Round_Down_Per", 0);

                        }
                        else if (rd_Half_Round_DownPer.Checked == true)
                        {
                            cmd.Parameters.AddWithValue("@Half_Round_Down_Per", 1);
                            cmd.Parameters.AddWithValue("@Half_Round_Up_Per", 0);
                            cmd.Parameters.AddWithValue("@Round_down_Per", 0);
                            cmd.Parameters.AddWithValue("@Round_up_Per", 0);
                        }
                        cmd.Parameters.AddWithValue("@Round_Percentage_Checked", 0);
                    }


                    cmd.Parameters.AddWithValue("@Branch_id", ViewState["branchid"].ToString());
                    cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@Created_date", My.getdate1());
                    if (My.InsertUpdateData(cmd))
                    {
                        ViewState["Grade_System_Id"] = Examination.auto_serialS("Grade_System_Id", ViewState["branchid"].ToString());
                        Alertme("Grade System has been saved successfully.", "success");
                        Bind_Data_edit();
                    }
                    #endregion
                }
            }

            else
            {
                int i = 0, j = 0;
                foreach (DataListItem item in datalist_classgrid.Items)
                {
                    CheckBox chk_per = (CheckBox)item.FindControl("chk_per");
                    if (chk_per.Checked == true)
                    {

                        if (ViewState["deletemode"].ToString() == "0")
                        {
                            mycode.executequery("delete from Exam_Grade_System_Mapping_with_Class where Grade_System_Id=" + ViewState["Grade_System_Id"].ToString() + "");
                        }
                        Label lbl_classid = (Label)item.FindControl("lbl_classid");
                        add_update_Exam_Grade_System_Mapping_with_Class(lbl_classid.Text);
                        ViewState["deletemode"] = "1";

                    }
                    else
                    {
                        j++;
                    }
                    i++;
                }
                if (i == j)
                {
                    Alertme("Please select class name", "warning");
                }
                else
                {
                    #region update

                    string query = "Update Exam_Grade_System set  Grade_Name=@Grade_Name,Input_Type=@Input_Type,Output=@Output ,With_Decimal=@With_Decimal,Without_Decimal=@Without_Decimal,Round_up=@Round_up,Round_down=@Round_down,Half_Round_Up=@Half_Round_Up,Half_Round_Down=@Half_Round_Down,With_Decimal_Per=@With_Decimal_Per,Without_Decimal_Per=@Without_Decimal_Per,Round_up_Per=@Round_up_Per,Round_down_Per=@Round_down_Per,Half_Round_Up_Per=@Half_Round_Up_Per,Half_Round_Down_Per=@Half_Round_Down_Per  ,Updated_by=@Updated_by,Updated_date=@Updated_date,Round_Percentage_Checked=@Round_Percentage_Checked where Grade_System_Id=@Grade_System_Id";
                    cmd = new SqlCommand(query);

                    cmd.Parameters.AddWithValue("@Grade_System_Id", ViewState["Grade_System_Id"].ToString());

                    cmd.Parameters.AddWithValue("@Grade_Name", txt_Name.Text);
                    cmd.Parameters.AddWithValue("@Input_Type", ddl_input.Text);
                    cmd.Parameters.AddWithValue("@Output", ddl_output.Text);



                    if (rd_With_Decimal.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@With_Decimal", 1);
                        cmd.Parameters.AddWithValue("@Without_Decimal", 0);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@With_Decimal", 0);
                        cmd.Parameters.AddWithValue("@Without_Decimal", 1);
                    }

                    if (rd_Round_up.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@Round_up", 1);
                        cmd.Parameters.AddWithValue("@Round_down", 0);
                        cmd.Parameters.AddWithValue("@Half_Round_Up", 0);
                        cmd.Parameters.AddWithValue("@Half_Round_Down", 0);
                    }
                    else if (rd_Round_down.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@Round_down", 1);
                        cmd.Parameters.AddWithValue("@Round_up", 0);
                        cmd.Parameters.AddWithValue("@Half_Round_Up", 0);
                        cmd.Parameters.AddWithValue("@Half_Round_Down", 0);
                    }
                    else if (rd_Half_Round_Up.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@Half_Round_Up", 1);

                        cmd.Parameters.AddWithValue("@Round_down", 0);
                        cmd.Parameters.AddWithValue("@Round_up", 0);
                        cmd.Parameters.AddWithValue("@Half_Round_Down", 0);

                    }
                    else if (rd_Half_Round_Down.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@Half_Round_Down", 1);
                        cmd.Parameters.AddWithValue("@Half_Round_Up", 0);
                        cmd.Parameters.AddWithValue("@Round_down", 0);
                        cmd.Parameters.AddWithValue("@Round_up", 0);
                    }


                    if (chk_per.Checked)
                    {
                        cmd.Parameters.AddWithValue("@Round_Percentage_Checked", 1);

                        if (rd_With_Decimal.Checked == true)
                        {
                            cmd.Parameters.AddWithValue("@With_Decimal_Per", 1);
                            cmd.Parameters.AddWithValue("@Without_Decimal_Per", 0);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@With_Decimal_Per", 0);
                            cmd.Parameters.AddWithValue("@Without_Decimal_Per", 1);
                        }
                        if (rd_Round_up.Checked == true)
                        {
                            cmd.Parameters.AddWithValue("@Round_up_Per", 1);
                            cmd.Parameters.AddWithValue("@Round_down_Per", 0);
                            cmd.Parameters.AddWithValue("@Half_Round_Up_Per", 0);
                            cmd.Parameters.AddWithValue("@Half_Round_Down_Per", 0);
                        }
                        else if (rd_Round_down.Checked == true)
                        {
                            cmd.Parameters.AddWithValue("@Round_down_Per", 1);
                            cmd.Parameters.AddWithValue("@Round_up_Per", 0);
                            cmd.Parameters.AddWithValue("@Half_Round_Up_Per", 0);
                            cmd.Parameters.AddWithValue("@Half_Round_Down_Per", 0);
                        }
                        else if (rd_Half_Round_Up.Checked == true)
                        {
                            cmd.Parameters.AddWithValue("@Half_Round_Up_Per", 1);

                            cmd.Parameters.AddWithValue("@Round_down_Per", 0);
                            cmd.Parameters.AddWithValue("@Round_up_Per", 0);
                            cmd.Parameters.AddWithValue("@Half_Round_Down_Per", 0);

                        }
                        else if (rd_Half_Round_Down.Checked == true)
                        {
                            cmd.Parameters.AddWithValue("@Half_Round_Down_Per", 1);
                            cmd.Parameters.AddWithValue("@Half_Round_Up_Per", 0);
                            cmd.Parameters.AddWithValue("@Round_down_Per", 0);
                            cmd.Parameters.AddWithValue("@Round_up_Per", 0);
                        }

                    }
                    else
                    {
                        if (rd_With_DecimalPer.Checked == true)
                        {
                            cmd.Parameters.AddWithValue("@With_Decimal_Per", 1);
                            cmd.Parameters.AddWithValue("@Without_Decimal_Per", 0);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@With_Decimal_Per", 0);
                            cmd.Parameters.AddWithValue("@Without_Decimal_Per", 1);
                        }
                        if (rd_Round_upPer.Checked == true)
                        {
                            cmd.Parameters.AddWithValue("@Round_up_Per", 1);
                            cmd.Parameters.AddWithValue("@Round_down_Per", 0);
                            cmd.Parameters.AddWithValue("@Half_Round_Up_Per", 0);
                            cmd.Parameters.AddWithValue("@Half_Round_Down_Per", 0);
                        }
                        else if (rd_Round_downPer.Checked == true)
                        {
                            cmd.Parameters.AddWithValue("@Round_down_Per", 1);
                            cmd.Parameters.AddWithValue("@Round_up_Per", 0);
                            cmd.Parameters.AddWithValue("@Half_Round_Up_Per", 0);
                            cmd.Parameters.AddWithValue("@Half_Round_Down_Per", 0);
                        }
                        else if (rd_Half_Round_UpPer.Checked == true)
                        {
                            cmd.Parameters.AddWithValue("@Half_Round_Up_Per", 1);

                            cmd.Parameters.AddWithValue("@Round_down_Per", 0);
                            cmd.Parameters.AddWithValue("@Round_up_Per", 0);
                            cmd.Parameters.AddWithValue("@Half_Round_Down_Per", 0);

                        }
                        else if (rd_Half_Round_DownPer.Checked == true)
                        {
                            cmd.Parameters.AddWithValue("@Half_Round_Down_Per", 1);
                            cmd.Parameters.AddWithValue("@Half_Round_Up_Per", 0);
                            cmd.Parameters.AddWithValue("@Round_down_Per", 0);
                            cmd.Parameters.AddWithValue("@Round_up_Per", 0);
                        }
                        cmd.Parameters.AddWithValue("@Round_Percentage_Checked", 0);
                    }



                    cmd.Parameters.AddWithValue("@Updated_by", ViewState["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@Updated_date", My.getdate1());
                    if (My.InsertUpdateData(cmd))
                    {
                        ViewState["Grade_System_Id"] = Examination.auto_serialS("Grade_System_Id", ViewState["branchid"].ToString());
                        Alertme("Grade System has been saved successfully.", "success");
                        Bind_Data_edit();

                    }
                    #endregion
                }


            }


        }

        private void add_update_Exam_Grade_System_Mapping_with_Class(string classid)
        {
            SqlCommand cmd;
            string query = "INSERT INTO Exam_Grade_System_Mapping_with_Class (Grade_System_Id,Course_id) values (@Grade_System_Id,@Course_id)";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Grade_System_Id", ViewState["Grade_System_Id"].ToString());
            cmd.Parameters.AddWithValue("@Course_id", classid);

            if (My.InsertUpdateData(cmd))
            {
            }
        }
        #endregion


        #region percentage
        protected void chk_per_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_per.Checked == true)
            {
                pnl_percentage.Visible = false;

            }
            else
            {
                pnl_percentage.Visible = true;
            }
        }


        protected void rd_With_DecimalPer_CheckedChanged(object sender, EventArgs e)
        {
            withdecimal_per.Visible = true;




            if (rd_Round_upPer.Checked == true)
            {
                width_decimal_Round_upPer.Visible = true;
                width_decimal_Round_downPer.Visible = false;
                width_decimal_Half_Round_UpPer.Visible = false;
                width_decimal_Half_Round_downPer.Visible = false;


                Without_decimal_Round_upPer.Visible = false;
                Without_decimal_Round_downPer.Visible = false;
                Without_decimal_half_round_upPer.Visible = false;
                Without_decimal_half_round_downPer.Visible = false;

            }
            else if (rd_Round_downPer.Checked == true)
            {
                width_decimal_Round_upPer.Visible = false;
                width_decimal_Round_downPer.Visible = true;
                width_decimal_Half_Round_UpPer.Visible = false;
                width_decimal_Half_Round_downPer.Visible = false;


                Without_decimal_Round_upPer.Visible = false;
                Without_decimal_Round_downPer.Visible = false;
                Without_decimal_half_round_upPer.Visible = false;
                Without_decimal_half_round_downPer.Visible = false;

            }
            else if (rd_Half_Round_UpPer.Checked == true)
            {
                width_decimal_Round_upPer.Visible = false;
                width_decimal_Round_downPer.Visible = false;
                width_decimal_Half_Round_UpPer.Visible = true;
                width_decimal_Half_Round_downPer.Visible = false;


                Without_decimal_Round_upPer.Visible = false;
                Without_decimal_Round_downPer.Visible = false;
                Without_decimal_half_round_upPer.Visible = false;
                Without_decimal_half_round_downPer.Visible = false;

            }
            else if (rd_Half_Round_DownPer.Checked == true)
            {
                width_decimal_Round_upPer.Visible = false;
                width_decimal_Round_downPer.Visible = false;
                width_decimal_Half_Round_UpPer.Visible = false;
                width_decimal_Half_Round_downPer.Visible = true;


                Without_decimal_Round_upPer.Visible = false;
                Without_decimal_Round_downPer.Visible = false;
                Without_decimal_half_round_upPer.Visible = false;
                Without_decimal_half_round_downPer.Visible = false;

            }
        }


        protected void rd_Without_DecimalPer_CheckedChanged(object sender, EventArgs e)
        {
            withdecimal_per.Visible = false;
            txt_with_decimalPer.Text = "0";



            if (rd_Round_upPer.Checked == true)
            {
                width_decimal_Round_upPer.Visible = false;
                width_decimal_Round_downPer.Visible = false;
                width_decimal_Half_Round_UpPer.Visible = false;
                width_decimal_Half_Round_downPer.Visible = false;


                Without_decimal_Round_upPer.Visible = true;
                Without_decimal_Round_downPer.Visible = false;
                Without_decimal_half_round_upPer.Visible = false;
                Without_decimal_half_round_downPer.Visible = false;

            }
            else if (rd_Round_downPer.Checked == true)
            {
                width_decimal_Round_upPer.Visible = false;
                width_decimal_Round_downPer.Visible = false;
                width_decimal_Half_Round_UpPer.Visible = false;
                width_decimal_Half_Round_downPer.Visible = false;


                Without_decimal_Round_upPer.Visible = false;
                Without_decimal_Round_downPer.Visible = true;
                Without_decimal_half_round_upPer.Visible = false;
                Without_decimal_half_round_downPer.Visible = false;

            }
            else if (rd_Half_Round_UpPer.Checked == true)
            {
                width_decimal_Round_upPer.Visible = false;
                width_decimal_Round_downPer.Visible = false;
                width_decimal_Half_Round_UpPer.Visible = false;
                width_decimal_Half_Round_downPer.Visible = false;


                Without_decimal_Round_upPer.Visible = false;
                Without_decimal_Round_downPer.Visible = false;
                Without_decimal_half_round_upPer.Visible = true;
                Without_decimal_half_round_downPer.Visible = false;

            }
            else if (rd_Half_Round_DownPer.Checked == true)
            {
                width_decimal_Round_upPer.Visible = false;
                width_decimal_Round_downPer.Visible = false;
                width_decimal_Half_Round_UpPer.Visible = false;
                width_decimal_Half_Round_downPer.Visible = false;


                Without_decimal_Round_upPer.Visible = false;
                Without_decimal_Round_downPer.Visible = false;
                Without_decimal_half_round_upPer.Visible = false;
                Without_decimal_half_round_downPer.Visible = true;

            }
        }






        protected void rd_Round_upPer_CheckedChanged(object sender, EventArgs e)
        {
            if (rd_With_DecimalPer.Checked == true)
            {

                width_decimal_Round_upPer.Visible = true;
                width_decimal_Round_downPer.Visible = false;
                width_decimal_Half_Round_UpPer.Visible = false;
                width_decimal_Half_Round_downPer.Visible = false;
                Without_decimal_Round_upPer.Visible = false;
                Without_decimal_Round_downPer.Visible = false;
                Without_decimal_half_round_upPer.Visible = false;
                Without_decimal_half_round_downPer.Visible = false;


            }
            else
            {
                width_decimal_Round_upPer.Visible = false;
                width_decimal_Round_downPer.Visible = false;
                width_decimal_Half_Round_UpPer.Visible = false;
                width_decimal_Half_Round_downPer.Visible = false;
                Without_decimal_Round_upPer.Visible = true;
                Without_decimal_Round_downPer.Visible = false;
                Without_decimal_half_round_upPer.Visible = false;
                Without_decimal_half_round_downPer.Visible = false;
            }
        }

        protected void rd_Round_downPer_CheckedChanged(object sender, EventArgs e)
        {
            if (rd_With_DecimalPer.Checked == true)
            {

                width_decimal_Round_upPer.Visible = false;
                width_decimal_Round_downPer.Visible = true;
                width_decimal_Half_Round_UpPer.Visible = false;
                width_decimal_Half_Round_downPer.Visible = false;


                Without_decimal_Round_upPer.Visible = false;
                Without_decimal_Round_downPer.Visible = false;
                Without_decimal_half_round_upPer.Visible = false;
                Without_decimal_half_round_downPer.Visible = false;


            }
            else
            {
                width_decimal_Round_upPer.Visible = false;
                width_decimal_Round_downPer.Visible = false;
                width_decimal_Half_Round_UpPer.Visible = false;
                width_decimal_Half_Round_downPer.Visible = false;

                Without_decimal_Round_upPer.Visible = false;
                Without_decimal_Round_downPer.Visible = true;
                Without_decimal_half_round_upPer.Visible = false;
                Without_decimal_half_round_downPer.Visible = false;
            }
        }

        protected void rd_Half_Round_UpPer_CheckedChanged(object sender, EventArgs e)
        {
            if (rd_With_DecimalPer.Checked == true)
            {

                width_decimal_Round_upPer.Visible = false;
                width_decimal_Round_downPer.Visible = false;
                width_decimal_Half_Round_UpPer.Visible = true;
                width_decimal_Half_Round_downPer.Visible = false;


                Without_decimal_Round_upPer.Visible = false;
                Without_decimal_Round_downPer.Visible = false;
                Without_decimal_half_round_upPer.Visible = false;
                Without_decimal_half_round_downPer.Visible = false;


            }
            else
            {
                width_decimal_Round_upPer.Visible = false;
                width_decimal_Round_downPer.Visible = false;
                width_decimal_Half_Round_UpPer.Visible = false;
                width_decimal_Half_Round_downPer.Visible = false;

                Without_decimal_Round_upPer.Visible = false;
                Without_decimal_Round_downPer.Visible = false;
                Without_decimal_half_round_upPer.Visible = true;
                Without_decimal_half_round_downPer.Visible = false;



            }
        }

        protected void rd_Half_Round_DownPer_CheckedChanged(object sender, EventArgs e)
        {
            if (rd_With_DecimalPer.Checked == true)
            {

                width_decimal_Round_up.Visible = false;
                width_decimal_Round_downPer.Visible = false;
                width_decimal_Half_Round_UpPer.Visible = false;
                width_decimal_Half_Round_downPer.Visible = true;


                Without_decimal_Round_upPer.Visible = false;
                Without_decimal_Round_downPer.Visible = false;
                Without_decimal_half_round_upPer.Visible = false;
                Without_decimal_half_round_downPer.Visible = false;


            }
            else
            {
                width_decimal_Round_upPer.Visible = false;
                width_decimal_Round_downPer.Visible = false;
                width_decimal_Half_Round_UpPer.Visible = false;
                width_decimal_Half_Round_downPer.Visible = false;

                Without_decimal_Round_upPer.Visible = false;
                Without_decimal_Round_downPer.Visible = false;
                Without_decimal_half_round_upPer.Visible = false;
                Without_decimal_half_round_downPer.Visible = true;

            }
        }

        protected void btn_save_range_Click(object sender, EventArgs e)
        {
            string bgColor = txtCCode.Text;
            if (txtCCode.Text.ToUpper() == "#FFFFFF")
            {
                bgColor = "";
            }

            SqlCommand cmd;
            if (btn_save_range.Text == "Add")
            { 
                #region add range
                double Lower_Range = Convert.ToDouble(txt_lowerrange.Text);
                double upper_Range = Convert.ToDouble(txt_upper_range.Text);


                bool gradadd = em.chk_grade_exist(ViewState["Grade_System_Id"].ToString(), ViewState["branchid"].ToString(), ViewState["sessionid"].ToString(), txt_grade.Text);

                if (gradadd == true)
                {
                    DataTable dt = mycode.FillData("Select * from Exam_Grade_System_Range_Grade where Grade_System_Id=" + ViewState["Grade_System_Id"].ToString() + " and Branch_Id=" + ViewState["branchid"].ToString() + " and Session_Id=" + ViewState["sessionid"].ToString() + "  and cast(Lower_Range as float)=" + Lower_Range + "");
                    if (dt.Rows.Count == 0)
                    {

                        DataTable dt1 = mycode.FillData("Select * from Exam_Grade_System_Range_Grade where Grade_System_Id=" + ViewState["Grade_System_Id"].ToString() + " and Branch_Id=" + ViewState["branchid"].ToString() + " and Session_Id=" + ViewState["sessionid"].ToString() + "  and cast(Upper_Range as float)=" + upper_Range + "");
                        if (dt1.Rows.Count == 0)
                        { 
                            string query = "INSERT INTO Exam_Grade_System_Range_Grade (Grade_System_Id,Lower_Range,Upper_Range,Grade,Credits,Creted_date,Creted_by,Branch_Id,Session_Id,Background_color) values (@Grade_System_Id,@Lower_Range,@Upper_Range,@Grade,@Credits,@Creted_date,@Creted_by,@Branch_Id,@Session_Id,@Background_color)";
                            cmd = new SqlCommand(query); 
                            cmd.Parameters.AddWithValue("@Grade_System_Id", ViewState["Grade_System_Id"].ToString());
                            cmd.Parameters.AddWithValue("@Lower_Range", txt_lowerrange.Text);
                            cmd.Parameters.AddWithValue("@Upper_Range", txt_upper_range.Text);
                            cmd.Parameters.AddWithValue("@Grade", txt_grade.Text);
                            cmd.Parameters.AddWithValue("@Credits", "0");
                            cmd.Parameters.AddWithValue("@Creted_date", My.getdate1());
                            cmd.Parameters.AddWithValue("@Creted_by", ViewState["Userid"]);
                            cmd.Parameters.AddWithValue("@Background_color", bgColor);
                            cmd.Parameters.AddWithValue("@Branch_Id", ViewState["branchid"].ToString());
                            cmd.Parameters.AddWithValue("@Session_Id", ViewState["sessionid"].ToString()); 
                            if (My.InsertUpdateData(cmd))
                            {
                                Alertme("Range has been add Successfully.", "success");
                                btn_save_range.Text = "Add";
                                txt_lowerrange.Text = "";
                                txt_upper_range.Text = "";
                                txt_grade.Text = "";  
                                Bind_grade_range();
                            } 
                        }
                        else
                        {
                            Alertme("Upper range already exists", "warning");
                        }
                    }
                    else
                    {
                        Alertme("Lower range already exists", "warning");

                    }
                }
                else
                {
                    Alertme("Your enter grade already exists", "warning");
                }
                #endregion
            }
            else if (btn_save_range.Text == "Update")
            {
                #region update range
                double Lower_Range = Convert.ToDouble(txt_lowerrange.Text);
                double upper_Range = Convert.ToDouble(txt_upper_range.Text);


                bool gradadd = em.chk_grade_exist_edit(ViewState["Grade_System_Id"].ToString(), ViewState["branchid"].ToString(), ViewState["sessionid"].ToString(), txt_grade.Text, hd_rang_id.Value);

                if (gradadd == true)
                {
                    DataTable dt = mycode.FillData("Select * from Exam_Grade_System_Range_Grade where Grade_System_Id=" + ViewState["Grade_System_Id"].ToString() + " and Branch_Id=" + ViewState["branchid"].ToString() + " and Session_Id=" + ViewState["sessionid"].ToString() + "  and cast(Lower_Range as float)=" + Lower_Range + " and Id!=" + hd_rang_id.Value + "");
                    if (dt.Rows.Count == 0)
                    {
                        DataTable dt1 = mycode.FillData("Select * from Exam_Grade_System_Range_Grade where Grade_System_Id=" + ViewState["Grade_System_Id"].ToString() + " and Branch_Id=" + ViewState["branchid"].ToString() + " and Session_Id=" + ViewState["sessionid"].ToString() + "  and cast(Upper_Range as float)=" + upper_Range + " and Id!=" + hd_rang_id.Value + "");
                        if (dt1.Rows.Count == 0)
                        {
                            string query = "Update Exam_Grade_System_Range_Grade set  Lower_Range=@Lower_Range,Upper_Range=@Upper_Range,Grade=@Grade,Background_color=@Background_color,Updated_date=@Updated_date,Updated_By=@Updated_By   where Id = @Id";
                            cmd = new SqlCommand(query);
                            cmd.Parameters.AddWithValue("@Lower_Range", txt_lowerrange.Text);
                            cmd.Parameters.AddWithValue("@Upper_Range", txt_upper_range.Text);
                            cmd.Parameters.AddWithValue("@Grade", txt_grade.Text);
                            cmd.Parameters.AddWithValue("@Background_color", bgColor);
                            cmd.Parameters.AddWithValue("@Updated_date", My.getdate1());
                            cmd.Parameters.AddWithValue("@Updated_By", ViewState["Userid"]);
                            cmd.Parameters.AddWithValue("@Id", hd_rang_id.Value);
                            if (My.InsertUpdateData(cmd))
                            {
                                Alertme("Range has been add Successfully.", "success");
                                btn_save_range.Text = "Add";
                                txt_lowerrange.Text = "";
                                txt_upper_range.Text = "";
                                txt_grade.Text = ""; 
                                Bind_grade_range();
                            } 
                        }
                        else
                        {
                            Alertme("Upper range already exists", "warning");
                        }
                    }
                    else
                    {
                        Alertme("Lower range already exists", "warning");

                    }
                }
                else
                {
                    Alertme("Your enter grade already exists", "warning");
                }
                #endregion
            }
            else
            {
            }
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lnk.Parent.Parent;
                Label lbl_id = (Label)row.FindControl("lbl_id");
                Label lbl_Lower_Range = (Label)row.FindControl("lbl_Lower_Range");
                Label lbl_Upper_Range = (Label)row.FindControl("lbl_Upper_Range");
                Label lbl_Grade = (Label)row.FindControl("lbl_Grade");
                Label lbl_background_color = (Label)row.FindControl("lbl_background_color");
                hd_rang_id.Value = lbl_id.Text;

                txt_lowerrange.Text = lbl_Lower_Range.Text;
                txt_upper_range.Text = lbl_Upper_Range.Text;
                txt_grade.Text = lbl_Grade.Text;
                txtCCode.Text = lbl_background_color.Text;
                btn_save_range.Text = "Update";
            }
            catch
            {
            }

        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lnk.Parent.Parent;
                Label lbl_id = (Label)row.FindControl("lbl_id");
                mycode.executequery("delete from  Exam_Grade_System_Range_Grade where Id='" + lbl_id.Text + "'");
                btn_save_range.Text = "Add";
                txt_lowerrange.Text = "";
                txt_upper_range.Text = "";
                txt_grade.Text = ""; 
                Bind_grade_range();
            }
            catch
            {
            }
        }
        #endregion

        protected void datalist_classgrid_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbl_classid = (Label)e.Item.FindControl("lbl_classid");
                CheckBox chk_per = (CheckBox)e.Item.FindControl("chk_per");

                bool classyesorno = get_status_maped(lbl_classid.Text);
                if (classyesorno == true)
                {
                    chk_per.Checked = true;
                }
                else
                {
                    chk_per.Checked = false;
                }
            }

        }

        private bool get_status_maped(string classid)
        {
            string query = "Select * from Exam_Grade_System_Mapping_with_Class where Grade_System_Id=" + ViewState["Grade_System_Id"].ToString() + " and Course_id=" + classid + "  ";

            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        } 
    }
}