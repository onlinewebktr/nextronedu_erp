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

namespace school_web.Dvlpr_Prof
{
    public partial class add_fee_in_mid_session : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    mycode.bind_all_ddl_with_id(ddl_session, "Select Session,session_id from session_details order by cast((Substring (Session,1,4)) as int) ");
                    Bind_course_details();
                    bind_month();
                }
            }

            catch (Exception ex)
            {
                My.submitException(ex, "Fee_Master");
            }
        }
        private void Bind_course_details()
        {
            DataTable dt = mycode.FillData("Select Course_Name,course_id from Add_course_table order by Position");
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
        private void bind_month()
        {
            DataTable dt = mycode.FillData("select Month,'false' as Value,Month_Id from Month_Index order by Position asc");
            if (dt.Rows.Count == 0)
            {
                rp_month.DataSource = null;
                rp_month.DataBind();
            }
            else
            {
                rp_month.DataSource = dt;
                rp_month.DataBind();
            }
        }

        protected void chk_all_CheckedChanged(object sender, EventArgs e)
        {
            for (int j = 0; j < rd_view.Items.Count; j++)
            {
                CheckBox chk_class = rd_view.Items[j].FindControl("chk_class") as CheckBox;
                if (chk_all.Checked)
                {
                    chk_class.Checked = true;
                }
                else
                {
                    chk_class.Checked = false;
                }
            }
        }

        protected void chk_all_month_CheckedChanged(object sender, EventArgs e)
        {
            for (int j = 0; j < rp_month.Items.Count; j++)
            {
                CheckBox chk_month_name = rp_month.Items[j].FindControl("chk_month_name") as CheckBox;
                if (chk_all_month.Checked)
                {
                    chk_month_name.Checked = true;
                }
                else
                {
                    chk_month_name.Checked = false;
                }
            }
        }

        protected void ddl_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                monthDV.Visible = false;
                if (ddl_fee_group.SelectedValue == "3")
                {
                    monthDV.Visible = true;
                }
                fetch_fee_head();
            }
            catch (Exception ex)
            {
            }
        }

        private void fetch_fee_head()
        {
            mycode.bind_all_ddl_with_id(ddl_fee_head, "select content,content_id from Content_master where group_id='" + ddl_fee_group.SelectedValue + "' and Ledger='" + ddl_days_hostel.Text + "' order by content asc");
        }

        protected void ddl_days_hostel_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                fetch_fee_head();
            }
            catch (Exception ex)
            {
            }
        }
        public void Alert(string Message)
        {
            lbl_msg.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_session.SelectedItem.Text == "Select")
                {
                    Alert("Please select session.");
                }
                else if (ddl_fee_group.SelectedItem.Text == "Select")
                {
                    Alert("Please select fee group.");
                }
                else if (ddl_fee_head.SelectedItem.Text == "Select")
                {
                    Alert("Please select fee head.");
                }
                else if (txt_amount.Text == "")
                {
                    Alert("Please enter amount.");
                }
                else
                {
                    save_fee_head_mid_session();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void save_fee_head_mid_session()
        {
            bool flag = false;
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
            {
                SqlConnection con = new SqlConnection(My.conn);
                con.Open();

                int mgrowcount = rp_month.Items.Count;
                int kl = 0;
                for (int ixi = 0; ixi < mgrowcount; ixi++)
                {
                    CheckBox chkM = (CheckBox)rp_month.Items[ixi].FindControl("chk_month_name");
                    if (chkM.Checked == true)
                    {
                        Label lbl_month_id = (Label)rp_month.Items[ixi].FindControl("lbl_month_id");
                        Label lbl_month_name = (Label)rp_month.Items[ixi].FindControl("lbl_month_name");


                        #region ffffFF
                        int growcount = rd_view.Items.Count;
                        int k = 0;
                        for (int ix = 0; ix < growcount; ix++)
                        {
                            CheckBox chk = (CheckBox)rd_view.Items[ix].FindControl("chk_class");
                            if (chk.Checked == true)
                            {
                                Label lbl_class_id = (Label)rd_view.Items[ix].FindControl("lbl_class_id");
                                Label lbl_course_name = (Label)rd_view.Items[ix].FindControl("lbl_course_name");

                                string fee_parameter_id = "4"; string fee_parameter = "MonthlyFee";
                                if (ddl_days_hostel.Text == "Hostel")
                                {
                                    fee_parameter_id = "3";
                                    fee_parameter = "HostelMonthlyFee";
                                }


                                DataTable dtc = payments.dataTable("select * from Fee_master_content_wise where session_id='" + ddl_session.SelectedValue + "' and class_id='" + lbl_class_id.Text + "' and Ledger='" + ddl_days_hostel.Text + "' and content_id='" + ddl_fee_head.SelectedValue + "' and Month_id='" + lbl_month_id.Text + "' and parameter_id='" + fee_parameter_id + "'", con);
                                if (dtc.Rows.Count > 0)
                                {
                                    Alert("Fee alrady added.");
                                }
                                else
                                {
                                    if (My.toDouble(txt_amount.Text) > 0)
                                    {
                                        insert_into_Fee_master_content_wise(txt_amount.Text, fee_parameter, fee_parameter_id, ddl_fee_head.Text, ddl_fee_head.SelectedItem.Text, lbl_class_id.Text, lbl_course_name.Text, lbl_month_name.Text, lbl_month_id.Text, con);

                                    }
                                    else
                                    {
                                        Alert("Please enter fee all fee type");
                                    }
                                }
                            }
                            else
                            {
                                k++;
                            }
                        }

                        if (k == growcount)
                        {
                            Alert("Please check minimum one course.");
                            return;
                        }
                        #endregion
                    }
                    else
                    {
                        kl++;
                    }
                }

                if (kl == mgrowcount)
                {
                    Alert("Please check minimum one month.");
                    return;
                }

                flag = true;
                con.Close();
                scope.Complete();
            }
            if (flag == true)
            {
                Alert("Fee master has been created successfully.");
            }
        }



        private void insert_into_Fee_master_content_wise(string fee, string Parmametername, string Parmameternameid, string content_id, string content, string classiD, string class_name, string month_name, string month_id, SqlConnection con)
        {
            SqlCommand cmd;
            DataTable dt = payments.dataTable("Select * from Fee_master_content_wise where content_id='" + content_id + "' and session_id='" + ddl_session.SelectedValue + "' and parameter_id='" + Parmameternameid + "'   and class_id='" + classiD + "' and Month_id='" + month_id + "'  ", con);
            if (dt.Rows.Count == 0)
            {
                string ttlFee = "0";
                try
                {
                    DataTable dtttlFee = payments.dataTable("select isnull(sum(convert(float, amount)),0) as Total from Fee_master_content_wise where  session_id='" + ddl_session.SelectedValue + "' and class_id='" + classiD + "' and parameter='" + Parmametername + "' and Ledger='" + ddl_days_hostel.Text + "' and Month_id='" + month_id + "'", con);
                    ttlFee = dtttlFee.Rows[0]["Total"].ToString();
                }
                catch (Exception ex)
                {
                }
                string query = "INSERT INTO Fee_master_content_wise (content,content_id,amount,parameter,class,session,session_id,class_id,parameter_id,Ledger,Acamedic_Semester_Id,Type,User_id,Date,time,Semester_Year,Month,Month_id) values (@content,@content_id,@amount,@parameter,@class,@session,@session_id,@class_id,@parameter_id,@Ledger,@Acamedic_Semester_Id,@Type,@User_id,@Date,@time,@Semester_Year,@Month,@Month_id)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@content", content);
                cmd.Parameters.AddWithValue("@content_id", content_id);
                cmd.Parameters.AddWithValue("@amount", My.toDouble(fee).ToString("0.00"));
                cmd.Parameters.AddWithValue("@parameter", Parmametername);
                cmd.Parameters.AddWithValue("@class", class_name);
                cmd.Parameters.AddWithValue("@session", ddl_session.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@session_id", ddl_session.SelectedValue);
                cmd.Parameters.AddWithValue("@class_id", classiD);
                cmd.Parameters.AddWithValue("@parameter_id", Parmameternameid);
                cmd.Parameters.AddWithValue("@Ledger", "School");
                cmd.Parameters.AddWithValue("@Acamedic_Semester_Id", "0");
                cmd.Parameters.AddWithValue("@Type", "Yearwise");
                cmd.Parameters.AddWithValue("@User_id", "Dev");
                cmd.Parameters.AddWithValue("@Date", mycode.date());
                cmd.Parameters.AddWithValue("@time", mycode.time());
                cmd.Parameters.AddWithValue("@Semester_Year", "");
                cmd.Parameters.AddWithValue("@Month", month_name);
                cmd.Parameters.AddWithValue("@Month_id", month_id);
                if (payments.InsertUpdateData(cmd, con))
                {
                    double finalTTL = My.toDouble(ttlFee) + My.toDouble(fee);
                    payments.exeSql("update Fee_Master set Amount='" + finalTTL + "' where session_id='" + ddl_session.SelectedValue + "' and class_id='" + classiD + "' and Parameter='" + Parmametername + "' and parameter_id='" + Parmameternameid + "' and Ledger='" + ddl_days_hostel.Text + "' and Month_id='" + month_id + "'", con);
                    update_student(fee, Parmametername, Parmameternameid, content_id, content, classiD, month_name, month_id, con);
                }
            }
        }
         
        private void update_student(string fee, string parmametername, string parmameternameid, string content_id, string content, string classiD, string month_name, string month_id, SqlConnection con)
        {
            DataTable dtMposition = My.dataTable("select Position from Month_Index where Month_Id='" + month_id + "'");
            string monthPosition = dtMposition.Rows[0][0].ToString();
            string qry = "select * from admission_registor where Session_id='" + ddl_session.SelectedValue + "' and Class_id='" + classiD + "' and hosteltaken='Yes'";
            if (parmametername == "MonthlyFee")
            {
                qry = "select * from admission_registor where Session_id='" + ddl_session.SelectedValue + "' and Class_id='" + classiD + "' and hosteltaken='No'";
            }
            DataTable dtF = payments.dataTable(qry, con);
            if (dtF.Rows.Count > 0)
            {
                foreach (DataRow dr in dtF.Rows)
                {
                    string qryChkTypewise = "select Id from Typewise_fee_collection where admission_no='" + dr["admissionserialnumber"].ToString() + "' and session='" + dr["session"].ToString() + "' and parameter='" + parmametername + "'  and month='" + month_name + "'";
                    if (payments.IsUserExistS(qryChkTypewise, con))
                    { }
                    else
                    {
                        SqlCommand cmd1;
                        string query1 = "INSERT INTO Typewise_fee_collection (admission_no,class,session,section,parameter,Date,idate,feetype,payable,paid,dues,status,month,user_id,content_id,transection,Ledger,group_id,class_id,position,Disc,Payable_after_disc,branchid) values (@admission_no,@class,@session,@section,@parameter,@Date,@idate,@feetype,@payable,@paid,@dues,@status,@month,@user_id,@content_id,@transection,@Ledger,@group_id,@class_id,@position,@Disc,@Payable_after_disc,@branchid)";
                        cmd1 = new SqlCommand(query1);
                        cmd1.Parameters.AddWithValue("@admission_no", dr["admissionserialnumber"].ToString());
                        cmd1.Parameters.AddWithValue("@class", dr["class"].ToString());
                        cmd1.Parameters.AddWithValue("@session", dr["session"].ToString());
                        cmd1.Parameters.AddWithValue("@section", dr["Section"].ToString());
                        cmd1.Parameters.AddWithValue("@parameter", parmametername);
                        cmd1.Parameters.AddWithValue("@Date", mycode.date());
                        cmd1.Parameters.AddWithValue("@idate", mycode.idate());
                        cmd1.Parameters.AddWithValue("@feetype", ddl_fee_head.SelectedItem.Text);
                        cmd1.Parameters.AddWithValue("@payable", txt_amount.Text);
                        cmd1.Parameters.AddWithValue("@paid", "0.00");
                        cmd1.Parameters.AddWithValue("@dues", txt_amount.Text);
                        cmd1.Parameters.AddWithValue("@status", "Dues");
                        cmd1.Parameters.AddWithValue("@month", month_name);
                        cmd1.Parameters.AddWithValue("@user_id", "Dev");
                        cmd1.Parameters.AddWithValue("@content_id", content_id);
                        cmd1.Parameters.AddWithValue("@transection", content_id);
                        cmd1.Parameters.AddWithValue("@Ledger", ddl_days_hostel.Text);
                        cmd1.Parameters.AddWithValue("@group_id", ddl_fee_group.SelectedValue);
                        cmd1.Parameters.AddWithValue("@class_id", classiD);
                        cmd1.Parameters.AddWithValue("@position", monthPosition);
                        cmd1.Parameters.AddWithValue("@Disc", "0.00");
                        cmd1.Parameters.AddWithValue("@Payable_after_disc", txt_amount.Text);
                        cmd1.Parameters.AddWithValue("@branchid", "1");
                        if (payments.InsertUpdateData(cmd1, con))
                        {
                        }
                    }
                }
            }
        }
    }
}