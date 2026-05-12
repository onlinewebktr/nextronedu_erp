using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Payroll
{
    public partial class salary_structure : System.Web.UI.Page
    {

        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
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
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();
                        mycode.bind_all_ddl_with_id(ddl_grade, "select grade_name,grade_id from dbo.[PRL_Grade_Master]");


                        ddl_payment_type.DataSource = new string[] { "Cash", "Bank", "Cheque" };
                        ddl_payment_type.SelectedIndex = 0;
                        ddl_ot_type.DataSource = new string[] { "Fixed", "Calculated" };
                        ddl_payment_type.DataBind();
                        ddl_ot_type.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "StudentList");
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

        protected void ddl_grade_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bind_emp_ddl();
            }
            catch (Exception ex)
            {
            }
        }

        private void bind_emp_ddl()
        {
            mycode.bind_all_ddl_with_id(ddl_employee_name, "select (Employee_Name+', '+Emp_Code) as Employee_Name,Employee_id from PRL_Employee_Master where Grade_id='" + ddl_grade.SelectedValue + "' order by Employee_Name asc");
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_grade.SelectedItem.Text == "Select")
                {
                }
                else if (ddl_employee_name.SelectedItem.Text == "Select")
                {
                }
                else
                {
                    find_employee();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_employee()
        {
            bind_grd_view("select *,(select top 1 grade_name from PRL_Grade_Master where grade_id=PRL_Employee_Master.Grade_id) as Grade_name,(select top 1 name from PRL_Department_Master where department_id=PRL_Employee_Master.Department_id) as Department_name,(select top 1 name from PRL_Designation_Master where description_id=PRL_Employee_Master.Designation_id) as Designation_name from dbo.[PRL_Employee_Master] where Grade_id='" + ddl_grade.SelectedValue + "' and Employee_id='" + ddl_employee_name.SelectedValue + "' order by id desc");
        }


        private void bind_grd_view(string qry)
        {
            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
                pnl_calculation.Visible = false;
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
                pnl_calculation.Visible = true;
                ViewState["employee_id"] = dt.Rows[0]["Employee_id"].ToString();
                ViewState["grade_id"] = dt.Rows[0]["Grade_id"].ToString();
                DataTable st = My.dataTable("select * from PRL_Employee_wise_salary_structure where Employee_id='" + ViewState["employee_id"].ToString() + "'");
                if (st.Rows.Count > 0)
                {
                    DataRow dr = st.Rows[0];
                    if (dr["PF_type"].ToString() == "Both")
                    {
                        chk_pf_both.Checked = true;
                    }
                    else
                    {
                        chk_pf_employer.Checked = true;
                    }
                    chk_ot_applicable.Checked = My.toBool(dr["OT_applicable"]);

                    ddl_payment_type.Text = dr["Payment_mode"].ToString();
                    ddl_ot_type.Text = dr["OT_rate_type"].ToString();
                    txt_ot_formula.Text = dr["OT_Rate_Formula"].ToString(); ;
                }

                string query = " select Row_Number() over(order by t.id) sl,t.*,iss.Income_head_id,cast(iss.Paid_Amount as float) Paid_Amount,iss.Mode_of_payment ,case when Variable_Head=0 then 'false' else 'true' end is_active from (select am.*,em.Employee_id from PRL_Allowance_Master am join PRL_Employee_Master em on am.Grade_id=em.Grade_id where em.Employee_id='" + ViewState["employee_id"].ToString() + "' )t left join PRL_Employee_Income_head_wise_salary_Structure iss on t.Income_Type=iss.Income_head_id and t.Employee_id=iss.Employee_id";
                income_dt = My.dataTable(query);
                Session["income_dt"] = income_dt;
                if (income_dt.Rows.Count == 0)
                {
                    rp_income.DataSource = null;
                    rp_income.DataBind();
                    Alertme("Please create Income Head First", "warning");
                    return;
                }
                else
                {
                    rp_income.DataSource = income_dt;
                    rp_income.DataBind();
                }
                string query1 = "select Row_Number() over(order by t.id) sl,t.*,cast(dss.Deducted_Amount as float) Deducted_Amount, cast(dss.Employer_Contribution as float) Employer_Contribution,dss.Mode_of_deduction ,case when Variable_Head=0 then 'false' else 'true' end is_active    from(select em.Employee_id,dm.*from PRL_Deduction_Master dm join PRL_Employee_Master em on dm.Grade_id=em.Grade_id where Employee_id='" + ViewState["employee_id"].ToString() + "')t  left join PRL_Employee_deduction_head_wise_salary_Structure dss on t.Deduction_Type=dss.Deduction_head_id  and t.Employee_id=dss.Employee_id";
                deduction_dt = My.dataTable(query1);
                rp_emp_deduction.DataSource = deduction_dt;
                rp_emp_deduction.DataBind();

                calculate_salary();
            }
        }

        protected void chk_pf_employer_CheckedChanged(object sender, EventArgs e)
        {
            if (deduction_dt != null)
            {
                double base_salary = 0;
                foreach (DataRow dr in deduction_dt.Rows)
                {
                    if (dr["is_active"].ToString() == "false")
                    {
                        if (dr["Deduction_Type"].ToString() == "2")
                        {

                            base_salary = find_total_base("P_Tax");
                        }
                        else if (dr["Deduction_Type"].ToString() == "3")
                        {

                            base_salary = find_total_base("ESI");
                        }
                        else if (dr["Deduction_Type"].ToString() == "4")
                        {

                            base_salary = find_total_base("PF");
                        }
                        else
                        {
                            base_salary = 0;
                            foreach (DataRow dr1 in income_dt.Rows)
                            {
                                base_salary += My.toDouble(dr1["Paid_Amount"]);
                            }
                        }
                        double val = Math.Round(base_salary * My.toDouble(dr["Formula"]) * 0.01, 0);
                        if (chk_pf_employer.Checked == true)
                        {
                            if (dr["Deduction_Type"].ToString() == "3")
                            {
                                //esi
                                double e_esi = Math.Round(base_salary * My.employer_esi * 0.01);
                                dr["Employer_Contribution"] = e_esi;
                                dr["Deducted_Amount"] = 0;
                            }
                            else if (dr["Deduction_Type"].ToString() == "4")
                            {
                                //pf
                                double e_pf = Math.Round(base_salary * My.employer_pf * 0.01);
                                dr["Employer_Contribution"] = e_pf;
                                dr["Deducted_Amount"] = 0;
                            }
                            else
                            {
                                dr["Deducted_Amount"] = val;
                            }
                        }
                        else
                        {
                            if (dr["Deduction_Type"].ToString() == "3")
                            {
                                //esi
                                double e_esi = Math.Round(base_salary * My.employer_esi * 0.01);
                                dr["Employer_Contribution"] = e_esi;

                            }
                            else if (dr["Deduction_Type"].ToString() == "4")
                            {
                                //pf
                                double e_pf = Math.Round(base_salary * My.employer_pf * 0.01);
                                dr["Employer_Contribution"] = e_pf;
                            }
                            dr["Deducted_Amount"] = val;
                        }
                    }
                }
                calculate_salary();
            }
        }



        private double find_total_base(string deduct_type)
        {
            double val = 0;
            foreach (DataRow dr in income_dt.Rows)
            {
                if (dr[deduct_type].ToString() == "True")
                {
                    val += My.toDouble(dr["Paid_Amount"]);
                }

            }
            return val;
        }

        private double find_base(string Formula_Type)
        {
            double val = 0;
            foreach (DataRow dr in income_dt.Rows)
            {
                if (dr["Income_head"].ToString() == Formula_Type)
                {
                    val = My.toDouble(dr["Paid_Amount"]);
                    break;
                }

            }
            return val;
        }
        private void calculate_salary()
        {
            int iX;
            double gross_salary = 0; double total_deduction = 0; double emp_cont = 0; double net_salary = 0;
            int gridview_rowcount = rp_income.Items.Count;
            for (iX = 0; iX < gridview_rowcount; iX++)
            {
                TextBox txt_paid_amt = (TextBox)rp_income.Items[iX].FindControl("txt_paid_amt");

                if (txt_paid_amt.Text != "")
                {
                    gross_salary = gross_salary + Convert.ToDouble(txt_paid_amt.Text);
                }
            }



            int iXi;
            int gridview_rowcountS = rp_emp_deduction.Items.Count;
            for (iXi = 0; iXi < gridview_rowcountS; iXi++)
            {
                Label lbl_amt_to_deducted = (Label)rp_emp_deduction.Items[iXi].FindControl("lbl_amt_to_deducted");
                Label lbl_emp_contribution = (Label)rp_emp_deduction.Items[iXi].FindControl("lbl_emp_contribution");

                if (lbl_amt_to_deducted.Text != "")
                {
                    total_deduction = total_deduction + Convert.ToDouble(lbl_amt_to_deducted.Text);
                }
                if (lbl_emp_contribution.Text != "")
                {
                    emp_cont = emp_cont + Convert.ToDouble(lbl_emp_contribution.Text);
                }
            }


            net_salary = gross_salary - total_deduction;
            txt_gross_salary.Text = gross_salary.ToString();
            txt_deduction.Text = total_deduction.ToString();
            txt_emp_cont.Text = emp_cont.ToString();
            txt_net_salary.Text = (gross_salary - total_deduction).ToString();
        }

        protected void chk_ot_applicable_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_ot_applicable.Checked == true)
            {
                txt_ot_formula.ReadOnly = false;
                txt_multiplier.ReadOnly = false;
            }
            else
            {
                txt_ot_formula.ReadOnly = true;
                txt_multiplier.ReadOnly = true;
                txt_ot_formula.Text = "";
            }
        }

        protected void txt_multiplier_TextChanged(object sender, EventArgs e)
        {
            txt_ot_formula.Text = txt_multiplier.Text;
        }


        DataTable income_dt = new DataTable();
        DataTable deduction_dt = new DataTable();
        protected void txt_paid_amt_TextChanged(object sender, EventArgs e)
        {
            double base_salary = 0;
            int growcount = rp_income.Items.Count;
            int k = 0;
            for (int i = 0; i < growcount; i++)
            {
                Label lbl_id = (Label)rp_income.Items[i].FindControl("lbl_id");
                Label lbl_inc_head = (Label)rp_income.Items[i].FindControl("lbl_inc_head");
                TextBox txt_paid_amt = (TextBox)rp_income.Items[i].FindControl("txt_paid_amt");
                Label lbl_formula_type = (Label)rp_income.Items[i].FindControl("lbl_formula_type");
                Label lbl_formula = (Label)rp_income.Items[i].FindControl("lbl_formula");
                Label lbl_is_active = (Label)rp_income.Items[i].FindControl("lbl_is_active");

                Label lbl_income_type_id = (Label)rp_income.Items[i].FindControl("lbl_income_type_id");
                Label lbl_grade_id = (Label)rp_income.Items[i].FindControl("lbl_grade_id");
                Label lbl_employee_id = (Label)rp_income.Items[i].FindControl("lbl_employee_id");


                if (lbl_is_active.Text == "false")
                {
                    base_salary = find_base(lbl_formula_type.Text);
                    double val = Math.Round(base_salary * My.toDouble(lbl_formula.Text) * 0.01);

                    SqlDataAdapter ad = new SqlDataAdapter("select * from PRL_Employee_Income_head_wise_salary_Structure where Employee_id='" + lbl_employee_id.Text + "' and Grade_id='" + lbl_grade_id.Text + "' and Income_head_id='" + lbl_income_type_id.Text + "'", My.conn);
                    DataSet ds = new DataSet();
                    ad.Fill(ds, "PRL_Employee_Income_head_wise_salary_Structure");
                    DataTable dt = ds.Tables[0];
                    int rowcount = ds.Tables[0].Rows.Count;
                    if (rowcount == 0)
                    {
                        string qry = "insert into PRL_Employee_Income_head_wise_salary_Structure(Employee_id,Grade_id,Income_head_id,Paid_Amount) values ('" + lbl_employee_id.Text + "','" + lbl_grade_id.Text + "','" + lbl_income_type_id.Text + "','" + val + "')";
                        mycode.executequery(qry);
                    }
                    else
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            dr["Paid_Amount"] = val;
                            SqlCommandBuilder cmb = new SqlCommandBuilder(ad);
                            ad.Update(dt);
                        }
                    }
                }
                k++;
            }



            int growcountS = rp_emp_deduction.Items.Count;
            int kSS = 0;
            for (int ii = 0; ii < growcountS; ii++)
            {
                Label lbl_deduction_type = (Label)rp_emp_deduction.Items[ii].FindControl("lbl_deduction_type");
                Label lbl_employer_contribution = (Label)rp_emp_deduction.Items[ii].FindControl("lbl_employer_contribution");
                Label lbl_deducted_amount = (Label)rp_emp_deduction.Items[ii].FindControl("lbl_deducted_amount");

                Label lbl_deduction_type_id = (Label)rp_emp_deduction.Items[ii].FindControl("lbl_deduction_type_id");
                Label lbl_d_grade_id = (Label)rp_emp_deduction.Items[ii].FindControl("lbl_d_grade_id");
                Label lbl_d_employee_id = (Label)rp_emp_deduction.Items[ii].FindControl("lbl_d_employee_id");
                Label lbl_is_active = (Label)rp_emp_deduction.Items[ii].FindControl("lbl_is_active");
                Label lbl_d_formula = (Label)rp_emp_deduction.Items[ii].FindControl("lbl_d_formula");

                SqlDataAdapter ad = new SqlDataAdapter("select * from PRL_Employee_deduction_head_wise_salary_Structure where Employee_id='" + lbl_d_employee_id.Text + "' and Grade_id='" + lbl_d_grade_id.Text + "' and Deduction_head_id='" + lbl_deduction_type_id.Text + "'", My.conn);
                DataSet ds = new DataSet();
                ad.Fill(ds, "PRL_Employee_deduction_head_wise_salary_Structure");
                DataTable dt = ds.Tables[0];
                int rowcount = ds.Tables[0].Rows.Count;
                if (rowcount == 0)
                {
                    SqlConnection con = new SqlConnection(My.conn);
                    SqlCommand cmd;
                    string strQuery = "INSERT INTO PRL_Employee_deduction_head_wise_salary_Structure (Employee_id,Grade_id,Deduction_head_id,Deducted_Amount,Employer_Contribution) values (@Employee_id,@Grade_id,@Deduction_head_id,@Deducted_Amount,@Employer_Contribution)";
                    cmd = new SqlCommand(strQuery);
                    cmd.Parameters.AddWithValue("@Employee_id", lbl_d_employee_id.Text);
                    cmd.Parameters.AddWithValue("@Grade_id", lbl_d_grade_id.Text);
                    cmd.Parameters.AddWithValue("@Deduction_head_id", lbl_deduction_type_id.Text);


                    if (lbl_is_active.Text == "false")
                    {
                        if (lbl_deduction_type.Text == "2")
                        {
                            base_salary = find_total_base("P_Tax");
                        }
                        else if (lbl_deduction_type.Text == "3")
                        {
                            base_salary = find_total_base("ESI");
                        }
                        else if (lbl_deduction_type.Text == "4")
                        {
                            base_salary = find_total_base("PF");
                        }
                        else
                        {
                            base_salary = 0;
                            int iX;
                            double totalrate = 0;
                            int gridview_rowcount = rp_income.Items.Count;
                            for (iX = 0; iX < gridview_rowcount; iX++)
                            {
                                TextBox txt_paid_amt = (TextBox)rp_income.Items[iX].FindControl("txt_paid_amt");

                                if (txt_paid_amt.Text != "")
                                {
                                    totalrate = totalrate + Convert.ToDouble(txt_paid_amt.Text);
                                }
                            }
                            base_salary += My.toDouble(totalrate);
                        }


                        double val = Math.Round(base_salary * My.toDouble(lbl_d_formula.Text) * 0.01);
                        if (chk_pf_employer.Checked == true)
                        {
                            if (lbl_deduction_type.Text == "3")
                            {
                                //esi
                                double e_esi = Math.Round(base_salary * My.employer_esi * 0.01);
                                cmd.Parameters.AddWithValue("@Employer_Contribution", e_esi);
                                cmd.Parameters.AddWithValue("@Deducted_Amount", 0);
                            }
                            else if (lbl_deduction_type.Text == "4")
                            {
                                //pf
                                double e_pf = Math.Round(base_salary * My.employer_pf * 0.01);
                                cmd.Parameters.AddWithValue("@Employer_Contribution", e_pf);
                                cmd.Parameters.AddWithValue("@Deducted_Amount", 0);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Employer_Contribution", "");
                                cmd.Parameters.AddWithValue("@Deducted_Amount", val);
                            }
                        }
                        else
                        {
                            if (lbl_deduction_type.Text == "3")
                            {
                                //esi
                                double e_esi = Math.Round(base_salary * My.employer_esi * 0.01);
                                cmd.Parameters.AddWithValue("@Employer_Contribution", e_esi);
                                cmd.Parameters.AddWithValue("@Deducted_Amount", val);
                            }
                            else if (lbl_deduction_type.Text == "4")
                            {
                                //pf
                                double e_pf = Math.Round(base_salary * My.employer_pf * 0.01);
                                cmd.Parameters.AddWithValue("@Employer_Contribution", e_pf);
                                cmd.Parameters.AddWithValue("@Deducted_Amount", val);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Employer_Contribution", "");
                                cmd.Parameters.AddWithValue("@Deducted_Amount", val);
                            }
                        }
                    }
                    if (My.InsertUpdateData(cmd))
                    {
                    }
                }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (lbl_is_active.Text == "false")
                        {
                            if (lbl_deduction_type.Text == "2")
                            {
                                base_salary = find_total_base("P_Tax");
                            }
                            else if (lbl_deduction_type.Text == "3")
                            {
                                base_salary = find_total_base("ESI");
                            }
                            else if (lbl_deduction_type.Text == "4")
                            {
                                base_salary = find_total_base("PF");
                            }
                            else
                            {
                                base_salary = 0;
                                int iX;
                                double totalrate = 0;
                                int gridview_rowcount = rp_income.Items.Count;
                                for (iX = 0; iX < gridview_rowcount; iX++)
                                {
                                    TextBox txt_paid_amt = (TextBox)rp_income.Items[iX].FindControl("txt_paid_amt");

                                    if (txt_paid_amt.Text != "")
                                    {
                                        totalrate = totalrate + Convert.ToDouble(txt_paid_amt.Text);
                                    }
                                }
                                base_salary += My.toDouble(totalrate);
                            }


                            double val = Math.Round(base_salary * My.toDouble(lbl_d_formula.Text) * 0.01);
                            if (chk_pf_employer.Checked == true)
                            {
                                if (lbl_deduction_type.Text == "3")
                                {
                                    //esi
                                    double e_esi = Math.Round(base_salary * My.employer_esi * 0.01);
                                    dr["Employer_Contribution"] = e_esi;
                                    dr["Deducted_Amount"] = 0;
                                }
                                else if (lbl_deduction_type.Text == "4")
                                {
                                    //pf
                                    double e_pf = Math.Round(base_salary * My.employer_pf * 0.01);
                                    dr["Employer_Contribution"] = e_pf;
                                    dr["Deducted_Amount"] = 0;
                                }
                                else
                                {
                                    dr["Employer_Contribution"] = "";
                                    dr["Deducted_Amount"] = val;
                                }
                            }
                            else
                            {
                                if (lbl_deduction_type.Text == "3")
                                {
                                    //esi
                                    double e_esi = Math.Round(base_salary * My.employer_esi * 0.01);
                                    dr["Employer_Contribution"] = e_esi;
                                    dr["Deducted_Amount"] = val;
                                }
                                else if (lbl_deduction_type.Text == "4")
                                {
                                    //pf
                                    double e_pf = Math.Round(base_salary * My.employer_pf * 0.01);
                                    dr["Employer_Contribution"] = e_pf;
                                    dr["Deducted_Amount"] = val;
                                }
                            }
                        }
                        SqlCommandBuilder cmb = new SqlCommandBuilder(ad);
                        ad.Update(dt);
                    }
                }

                kSS++;
            }
            calculate_salary();
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                //if (rd_view.DataSource == null)
                //{
                //    Alertme("Please find employee details first!", "warning");
                //    return;
                //}

                try
                {
                    send_in_employee();
                    send_income();
                    send_deduction();
                    Alertme("Saved Successfully", "success");
                    empty_salary_structure();
                    pnl_calculation.Visible = false;
                }
                catch (Exception ex)
                {
                    My.Save_Exception(ex.StackTrace);
                }

            }
            catch (Exception ex)
            {
            }
        }


        private void empty_salary_structure()
        {
            txt_gross_salary.Text = "";
            txt_deduction.Text = "";
            txt_net_salary.Text = "";
            txt_monthly_ctc.Text = "";
            txt_ot_formula.Text = ""; 
            chk_voluntry_pf.Checked = false;
            chk_ot_applicable.Checked = false;
            chk_deduct_prof_tax.Checked = false;
            chk_incentive_applicable.Checked = false;
        }


        private void send_in_employee()
        {
            string type = "";
            if (chk_pf_both.Checked == true)
            {
                type = "Both";
            }
            if (chk_pf_employer.Checked == true)
            {
                type = "Employer";
            }
            if (chk_pf_without_limit.Checked == true)
            {
                type = "Without Limit";
            }
            SqlDataAdapter ad = new SqlDataAdapter("select * from PRL_Employee_wise_salary_structure where Employee_id='" + ViewState["employee_id"].ToString() + "'", My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Employee_wise_salary_structure");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                DataRow dr = dt.NewRow();
                dr["Employee_id"] = ViewState["employee_id"].ToString();
                dr["Grade_id"] = ViewState["grade_id"].ToString();
                dr["Gross_Salary"] = My.toDouble(txt_gross_salary.Text);
                dr["PF_type"] = type;
                dr["Payment_mode"] = ddl_payment_type.Text;
                dr["OT_applicable"] = chk_ot_applicable.Checked;
                dr["Total_Deduction"] = My.toDouble(txt_deduction.Text);
                dr["Net_Salary"] = My.toDouble(txt_net_salary.Text);
                dr["Monthly_CTC"] = My.toDouble(txt_monthly_ctc.Text);
                dr["OT_rate_type"] = ddl_ot_type.Text;
                dr["OT_Rate_Formula"] = My.toDouble(txt_ot_formula.Text);
                dr["Voluntry_PF"] = chk_voluntry_pf.Checked;
                dr["Prof_tax"] = chk_deduct_prof_tax.Checked;
                dr["Incentive_applicable"] = chk_incentive_applicable.Checked;
                dt.Rows.Add(dr);
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    dr["Gross_Salary"] = My.toDouble(txt_gross_salary.Text);
                    dr["PF_type"] = type;
                    dr["Payment_mode"] = ddl_payment_type.Text;
                    dr["OT_applicable"] = chk_ot_applicable.Checked;
                    dr["Total_Deduction"] = My.toDouble(txt_deduction.Text);
                    dr["Net_Salary"] = My.toDouble(txt_net_salary.Text);
                    dr["Monthly_CTC"] = My.toDouble(txt_monthly_ctc.Text);
                    dr["OT_rate_type"] = ddl_ot_type.Text;
                    dr["OT_Rate_Formula"] = My.toDouble(txt_ot_formula.Text);
                    dr["Voluntry_PF"] = chk_voluntry_pf.Checked;
                    dr["Prof_tax"] = chk_deduct_prof_tax.Checked;
                    dr["Incentive_applicable"] = chk_incentive_applicable.Checked;
                }
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
        }

        private void send_income()
        {
            int growcount = rp_income.Items.Count;
            int k = 0;
            for (int i = 0; i < growcount; i++)
            {
                TextBox txt_paid_amt = (TextBox)rp_income.Items[i].FindControl("txt_paid_amt");
                Label lbl_income_type_id = (Label)rp_income.Items[i].FindControl("lbl_income_type_id");
                Label lbl_grade_id = (Label)rp_income.Items[i].FindControl("lbl_grade_id");
                Label lbl_employee_id = (Label)rp_income.Items[i].FindControl("lbl_employee_id");

                SqlDataAdapter ad = new SqlDataAdapter("select * from PRL_Employee_Income_head_wise_salary_Structure where Employee_id='" + lbl_employee_id.Text + "' and Grade_id='" + lbl_grade_id.Text + "' and Income_head_id='" + lbl_income_type_id.Text + "'", My.con);
                DataSet ds = new DataSet();
                ad.Fill(ds, "Employee_Income_head_wise_salary_Structure");
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    DataRow dr = dt.NewRow();
                    dr["Employee_id"] = lbl_employee_id.Text;
                    dr["Grade_id"] = lbl_grade_id.Text;
                    dr["Income_head_id"] = lbl_income_type_id.Text;
                    dr["Paid_Amount"] = My.toDouble(txt_paid_amt.Text);
                    //dr["Mode_of_payment"] = inc_dr["Mode_of_payment"].ToString();
                    dt.Rows.Add(dr);
                }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        dr["Grade_id"] = lbl_grade_id.Text;
                        dr["Income_head_id"] = lbl_income_type_id.Text;
                        dr["Paid_Amount"] = My.toDouble(txt_paid_amt.Text);
                    }
                }
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(dt);
                k++;
            }
        }



        private void send_deduction()
        {
            int growcountS = rp_emp_deduction.Items.Count;
            int kSS = 0;
            for (int ii = 0; ii < growcountS; ii++)
            { 
                Label lbl_employer_contribution = (Label)rp_emp_deduction.Items[ii].FindControl("lbl_employer_contribution");
                Label lbl_deducted_amount = (Label)rp_emp_deduction.Items[ii].FindControl("lbl_deducted_amount");
                Label lbl_deduction_type_id = (Label)rp_emp_deduction.Items[ii].FindControl("lbl_deduction_type_id");
                Label lbl_d_grade_id = (Label)rp_emp_deduction.Items[ii].FindControl("lbl_d_grade_id");
                Label lbl_d_employee_id = (Label)rp_emp_deduction.Items[ii].FindControl("lbl_d_employee_id");

                SqlDataAdapter ad = new SqlDataAdapter("select * from PRL_Employee_deduction_head_wise_salary_Structure where Employee_id='" + lbl_d_employee_id.Text + "' and Grade_id='" + lbl_d_grade_id.Text + "' and Deduction_head_id='" + lbl_deduction_type_id.Text + "'", My.con);
                DataSet ds = new DataSet();
                ad.Fill(ds, "PRL_Employee_deduction_head_wise_salary_Structure");
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    DataRow dr = dt.NewRow();
                    dr["Employee_id"] = lbl_d_employee_id.Text;
                    dr["Grade_id"] = lbl_d_grade_id.Text;
                    dr["Deduction_head_id"] = lbl_deduction_type_id.Text;
                    dr["Deducted_Amount"] = My.toDouble(lbl_deducted_amount.Text);
                    dr["Employer_Contribution"] = lbl_employer_contribution.Text;
                    //dr["Mode_of_deduction"] = dec_dr["Mode_of_deduction"].ToString();
                    dt.Rows.Add(dr);
                }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        dr["Deduction_head_id"] = lbl_deduction_type_id.Text;
                        dr["Deducted_Amount"] = My.toDouble(lbl_deducted_amount.Text);
                        dr["Employer_Contribution"] = lbl_employer_contribution.Text;
                    }
                }
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(dt);
                kSS++;
            }
        }

        protected void ddl_ot_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_ot_type.SelectedItem.Text == "Fixed")
            {
                pnl_ot_hourly.Visible = true;
                pnl_ot_calculated.Visible = false;


                if (chk_ot_applicable.Checked == true)
                {
                    txt_ot_formula.ReadOnly = false;
                    txt_multiplier.ReadOnly = false;

                }
                else
                {
                    txt_ot_formula.ReadOnly = true;
                    txt_multiplier.ReadOnly = true;
                    txt_ot_formula.Text = "";
                }
            }
            else
            {
                txt_ot_formula.Text = "1";
                txt_multiplier.Text = "1";
                pnl_ot_calculated.Visible = true;
                pnl_ot_hourly.Visible = false;
                if (chk_ot_applicable.Checked == true)
                {
                    txt_ot_formula.ReadOnly = false;
                    txt_multiplier.ReadOnly = false;

                }
                else
                {
                    txt_ot_formula.ReadOnly = true;
                    txt_multiplier.ReadOnly = true;
                    txt_ot_formula.Text = "";
                }
            }
        }
    }
}