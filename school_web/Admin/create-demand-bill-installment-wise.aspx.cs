using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class create_demand_bill_installment_wise : System.Web.UI.Page
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
                    ViewState["Demand_bill_with_month"] = "No";
                    ViewState["Userid"] = Session["Admin"].ToString();
                    ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());


                    mycode.bind_all_ddl_with_id(ddl_installment_name, "select Intstallment_name,Month_position_no from Fee_installment_master");
                    mycode.bind_all_ddl_with_id(ddlsession, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                    ddlsession.SelectedValue = My.get_session_id();
                    mycode.bind_all_ddl_with_id(ddl_class, "Select Course_Name,course_id from Add_course_table order by Position asc");
                    //Bind_all_data();

                    Firm_Details();
                }
            }
        }

        private void Firm_Details()
        {
            DataTable dt = My.dataTable("select firm_id,Is_fine_repeat,Demand_bill_with_month from Firm_Details");
            ViewState["firm_id"] = dt.Rows[0]["firm_id"].ToString();
            try
            {
                if (dt.Rows[0]["Is_fine_repeat"].ToString() == "True")
                {
                    ViewState["Is_fine_repeat"] = "Yes";
                }
                else
                {
                    ViewState["Is_fine_repeat"] = "No";
                }
            }
            catch (Exception ex)
            {
            }

            try
            {
                if (dt.Rows[0]["Demand_bill_with_month"].ToString() == "True")
                {
                    ViewState["Demand_bill_with_month"] = "Yes";
                }
                else
                {
                    ViewState["Demand_bill_with_month"] = "No";
                }
            }
            catch (Exception ex)
            {
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



        private void bind_grid_data(string query)
        {
            ViewState["query"] = query;
            SqlCommand cmd = new SqlCommand(query);
            DataTable dt = mycode.GetData(cmd);
            if (dt.Rows.Count == 0)
            {
                printDBDV.Visible = false; 
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                printDBDV.Visible = true;
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }

        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlsession.SelectedItem.Text == "Select")
            {
                ddlsession.Focus();
                Alertme("Please select session", "warning");
            }
            else if (ddl_class.SelectedItem.Text == "Select")
            {
                ddl_class.Focus();
                Alertme("Please select class", "warning");
            }
            else
            {
                mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor where Class_Id=" + ddl_class.SelectedValue + "  and Session_Id=" + ddlsession.SelectedValue + " and Branch_id='" + ViewState["branchid"].ToString() + "'");
            }
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            if (ddlsession.SelectedItem.Text == "Select")
            {
                ddlsession.Focus();
                Alertme("Please select session", "warning");
            }
            else if (ddl_class.SelectedItem.Text == "Select")
            {
                ddl_class.Focus();
                Alertme("Please select class", "warning");
            }
            else
            {
                string query = "";
                if (ddl_section.Text.ToUpper() == "ALL")
                {
                    if (ddl_student_type.SelectedValue == "1")
                    {
                        query = "select * from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddl_class.SelectedValue + "' and Status='1' and hosteltaken='Yes' order by rollnumber asc";
                    }
                    else if (ddl_student_type.SelectedValue == "2")
                    {
                        query = "select * from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddl_class.SelectedValue + "' and Status='1' and hosteltaken not in ('Yes') order by rollnumber asc";
                    }
                    else
                    {
                        query = "select * from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddl_class.SelectedValue + "' and Status='1' order by rollnumber asc";
                    }
                }
                else
                {
                    if (ddl_student_type.SelectedValue == "1")
                    {
                        query = "select * from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddl_class.SelectedValue + "' and Section='" + ddl_section.Text + "' and Status='1' and hosteltaken='Yes' order by rollnumber asc";
                    }
                    else if (ddl_student_type.SelectedValue == "2")
                    {
                        query = "select * from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddl_class.SelectedValue + "' and Section='" + ddl_section.Text + "' and Status='1' and hosteltaken not in ('Yes') order by rollnumber asc";
                    }
                    else
                    {
                        query = "select * from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddl_class.SelectedValue + "' and Section='" + ddl_section.Text + "' and Status='1' order by rollnumber asc";
                    }
                }
                bind_grid_data(query);
            }
        }





        protected void btn_final_submit_Click(object sender, EventArgs e)
        {
            try
            {
                string respPage = "print-demand-bill-inst.aspx";
                if (ddl_installment_name.SelectedItem.Text == "Select")
                {
                    Alertme("Please choose installment.", "warning");
                    ddl_installment_name.Focus();
                }
                else if (txt_payment_date.Text == "")
                {
                    Alertme("Please choose last date of payment.", "warning");
                    txt_payment_date.Focus();
                }
                else
                {
                    //CalculateDues 
                    bool flag = false;
                    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
                    {
                        SqlConnection con = new SqlConnection(My.conn);
                        con.Open();
                        int kc = 0;
                        int growcounts = rd_view.Items.Count;
                        for (int i = 0; i < growcounts; i++)
                        {
                            CheckBox chk = (CheckBox)rd_view.Items[i].FindControl("chkRowData");
                            if (chk.Checked == true)
                            {
                                Label lbl_admission_no = (Label)rd_view.Items[i].FindControl("lbl_admission_no");
                                Label lbl_class_id = (Label)rd_view.Items[i].FindControl("lbl_class_id");
                                Label lbl_session_id = (Label)rd_view.Items[i].FindControl("lbl_session_id");
                                demandBill.update_student_dues(lbl_session_id.Text, lbl_class_id.Text, lbl_admission_no.Text, txt_payment_date.Text, ViewState["firm_id"].ToString(), ViewState["Is_fine_repeat"].ToString(), con);
                            }
                            else
                            {
                                kc++;
                            }
                        }
                        if (kc == growcounts)
                        {
                            if (ddl_section.SelectedItem.Text.ToUpper() == "ALL")
                            {
                                DataTable dt = payments.dataTable("select Session_id,Class_id,admissionserialnumber from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddl_class.SelectedValue + "' and Status='1'", con);
                                foreach (DataRow dr in dt.Rows)
                                {
                                    demandBill.update_student_dues(dr["Session_id"].ToString(), dr["Class_id"].ToString(), dr["admissionserialnumber"].ToString(), txt_payment_date.Text, ViewState["firm_id"].ToString(), ViewState["Is_fine_repeat"].ToString(), con);
                                }
                            }
                            else
                            {
                                DataTable dt = payments.dataTable("select Session_id,Class_id,admissionserialnumber from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddl_class.SelectedValue + "' and Section='" + ddl_section.Text + "' and Status='1'", con);
                                foreach (DataRow dr in dt.Rows)
                                {
                                    demandBill.update_student_dues(dr["Session_id"].ToString(), dr["Class_id"].ToString(), dr["admissionserialnumber"].ToString(), txt_payment_date.Text, ViewState["firm_id"].ToString(), ViewState["Is_fine_repeat"].ToString(), con);
                                }
                            }

                            flag = true;
                            con.Close();
                            scope.Complete();
                        }
                        else
                        {
                            flag = true;
                            con.Close();
                            scope.Complete();
                        }
                    }







                    if (flag == true)
                    {
                        string adm_ids = "";
                        int growcount = rd_view.Items.Count;
                        int k = 0;
                        for (int i = 0; i < growcount; i++)
                        {
                            CheckBox chk = (CheckBox)rd_view.Items[i].FindControl("chkRowData");
                            if (chk.Checked == true)
                            {
                                Label lbl_id = (Label)rd_view.Items[i].FindControl("lbl_id");
                                adm_ids = adm_ids += lbl_id.Text + ",";
                            }
                            else
                            {
                                k++;
                            }
                        }
                        if (k == growcount)
                        {

                            string reslink = "";
                            if (ddl_section.SelectedItem.Text.ToUpper() == "ALL")
                            {
                                reslink = "slip/" + respPage + "?session_Id=" + ddlsession.SelectedValue + "&branch_id=" + ViewState["branchid"].ToString() + "&classid=" + ddl_class.SelectedValue + "&section=0&admno=0&checked=0&month=" + ddl_installment_name.SelectedValue + "&paydate=" + txt_payment_date.Text + "&billType=" + ddl_with.SelectedValue + "&stdType=" + ddl_student_type.SelectedValue;
                            }
                            else
                            {
                                reslink = "slip/" + respPage + "?session_Id=" + ddlsession.SelectedValue + "&branch_id=" + ViewState["branchid"].ToString() + "&classid=" + ddl_class.SelectedValue + "&section=" + ddl_section.Text + "&admno=0&checked=0&month=" + ddl_installment_name.SelectedValue + "&paydate=" + txt_payment_date.Text + "&billType=" + ddl_with.SelectedValue + "&stdType=" + ddl_student_type.SelectedValue;
                            }
                            Response.Redirect(reslink, false);
                        }
                        else
                        {
                            string reslink = "";
                            if (ddl_section.SelectedItem.Text.ToUpper() == "ALL")
                            {
                                reslink = "slip/" + respPage + "?session_Id=" + ddlsession.SelectedValue + "&branch_id=" + ViewState["branchid"].ToString() + "&classid=" + ddl_class.SelectedValue + "&section=0&admno=" + adm_ids + "&checked=1&month=" + ddl_installment_name.SelectedValue + "&paydate=" + txt_payment_date.Text + "&billType=" + ddl_with.SelectedValue + "&stdType=" + ddl_student_type.SelectedValue;
                            }
                            else
                            {
                                reslink = "slip/" + respPage + "?session_Id=" + ddlsession.SelectedValue + "&branch_id=" + ViewState["branchid"].ToString() + "&classid=" + ddl_class.SelectedValue + "&section=" + ddl_section.Text + "&admno=" + adm_ids + "&checked=1&month=" + ddl_installment_name.SelectedValue + "&paydate=" + txt_payment_date.Text + "&billType=" + ddl_with.SelectedValue + "&stdType=" + ddl_student_type.SelectedValue;
                            }
                            Response.Redirect(reslink, false);
                        }
                    }
                    else
                    {
                        Alertme("Sorry something went wrong. please try again.", "warning");
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "create-demand-bill-installment-wise.aspx");
            }
        }
    }
}