using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class add_strength : System.Web.UI.Page
    {
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
                        My.bind_ddl_all(ddl_class, "select Course_Name from Add_course_table");
                        My.bind_ddl_all(ddl_section, "select Section from section_master");
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString(); 

                        bind_grd_view();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_Strength_Master");
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
        My mycode = new My();
        private void bind_grd_view()
        {
            DataTable dt = mycode.FillData("select *,Row_Number() over( order by id) sl from strength_master");
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no branch list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();

            }
            else
            {

                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }



        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            txt_no_of_student.Text = "";
            btn_cancel.Visible = false;
            btn_Submit.Text = "Add";
        }

        protected void btn_Submit_Click1(object sender, EventArgs e)
        {
            if (ddl_class.Text == "")
            {
                Alertme("Please Select Class", "warning");
                ddl_class.Focus();
                return;
            }
            if (ddl_section.Text == "")
            {
                Alertme("Please Select Section", "warning");
                ddl_section.Focus();
                return;
            }
            if (txt_no_of_student.Text == "")
            {
                Alertme("Please Enter No of Student", "warning");
                txt_no_of_student.Focus();
                return;
            }

            if (btn_Submit.Text == "Add")
            {
                if (ddl_class.Text == "All" && ddl_section.Text == "All")
                {
                    submit_all();
                }
                else if (ddl_class.Text == "All" && ddl_section.Text != "All")
                {
                    submit_all_section();
                }
                else if (ddl_class.Text != "All" && ddl_section.Text == "All")
                {
                    submit_all_class();
                }
                else
                {
                    submit_details();
                }
                My.send_data_to_user_log_history(ViewState["Userid"].ToString(), ViewState["Userid"].ToString() + " create a strength master ");
                empty_form();
                bind_grd_view();
            }
            else
            {
                update_update_details();
                empty_form();
                bind_grd_view();
            }
        }

        private void update_update_details()
        {
            DataTable dt1 = My.dataTable("select * from strength_master where Class='" + ddl_class.Text + "' and Section='" + ddl_section.Text + "' and No_of_student='" + txt_no_of_student.Text + "'");
            if (dt1.Rows.Count == 0)
            {
                SqlConnection conn = new SqlConnection(My.conn);
                SqlDataAdapter ad = new SqlDataAdapter("select * from strength_master  where Id='" + hd_id.Value + "'", conn);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                DataTable dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    dr[1] = ddl_class.Text;
                    dr[2] = ddl_section.Text;
                    dr[3] = txt_no_of_student.Text;
                }
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(dt);
                Alertme("Strength Master  Updated Successfully", "success");
            }
            else
            {
                Alertme("Strength Master Already Updated", "warning");
            }
        }


        private void submit_all_class()
        {
            DataTable class_dt = My.dataTable("select Course_Name from dbo.[Add_course_table]");
            if (class_dt.Rows.Count > 0)
            {
                foreach (DataRow cdr in class_dt.Rows)
                {
                    SqlConnection conn = new SqlConnection(My.conn);
                    SqlDataAdapter ad = new SqlDataAdapter("select * from strength_master where Class='" + cdr["Course_Name"].ToString() + "' and Section='" + ddl_section.Text + "'", conn);
                    DataSet ds = new DataSet();
                    ad.Fill(ds);
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count == 0)
                    {
                        DataRow dr = dt.NewRow();
                        dr[1] = cdr["Course_Name"].ToString();
                        dr[2] = ddl_section.Text;
                        dr[3] = txt_no_of_student.Text;
                        dt.Rows.Add(dr);
                    }
                    else
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            dr[3] = txt_no_of_student.Text;
                        }
                    }
                    SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                    ad.Update(dt);
                }
                Alertme("Saved Successfully", "success");

            }
            else
            {
                Alertme("Please create class master", "warning");
            }
        }
        private void submit_all_section()
        {
            DataTable sec_dt = My.dataTable(" select Section from dbo.[section_master]");

            if (sec_dt.Rows.Count > 0)
            {
                foreach (DataRow sdr in sec_dt.Rows)
                {
                    SqlConnection conn = new SqlConnection(My.conn);
                    SqlDataAdapter ad = new SqlDataAdapter("select * from strength_master where Class='" + ddl_class.Text + "' and Section='" + sdr["Section"].ToString() + "'", conn);
                    DataSet ds = new DataSet();
                    ad.Fill(ds);
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count == 0)
                    {
                        DataRow dr = dt.NewRow();
                        dr[1] = ddl_class.Text;
                        dr[2] = sdr["Section"].ToString();
                        dr[3] = txt_no_of_student.Text;
                        dt.Rows.Add(dr);
                    }
                    else
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            dr[3] = txt_no_of_student.Text;
                        }
                    }
                    SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                    ad.Update(dt);
                }

                Alertme("Saved Successfully", "success");

            }
            else
            {
                Alertme("Please create section master", "warning");
            }
        }


        private void submit_all()
        {
            DataTable class_dt = My.dataTable("select Course_Name from dbo.[Add_course_table]");
            DataTable sec_dt = My.dataTable(" select Section from dbo.[section_master]");
            if (class_dt.Rows.Count > 0)
            {
                if (sec_dt.Rows.Count > 0)
                {
                    foreach (DataRow cdr in class_dt.Rows)
                    {
                        foreach (DataRow sdr in sec_dt.Rows)
                        {
                            SqlConnection conn = new SqlConnection(My.conn);
                            SqlDataAdapter ad = new SqlDataAdapter("select * from strength_master where Class='" + cdr["Course_Name"].ToString() + "' and Section='" + sdr["Section"].ToString() + "'", conn);
                            DataSet ds = new DataSet();
                            ad.Fill(ds);
                            DataTable dt = ds.Tables[0];
                            if (dt.Rows.Count == 0)
                            {
                                DataRow dr = dt.NewRow();
                                dr[1] = cdr["Course_Name"].ToString();
                                dr[2] = sdr["Section"].ToString();
                                dr[3] = txt_no_of_student.Text;
                                dt.Rows.Add(dr);
                            }
                            else
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    dr[3] = txt_no_of_student.Text;
                                }
                            }
                            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                            ad.Update(dt);
                        }
                    }
                    Alertme("Saved Successfully", "success");

                }
                else
                {
                    Alertme("Please create section master", "warning");
                }
            }
            else
            {
                Alertme("Please create class master", "warning");
            }
        }

        private void submit_details()
        {
            SqlConnection conn = new SqlConnection(My.conn);
            SqlDataAdapter ad = new SqlDataAdapter("select * from strength_master where Class='" + ddl_class.Text + "' and Section='" + ddl_section.Text + "'", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                DataRow dr = dt.NewRow();
                dr[1] = ddl_class.Text;
                dr[2] = ddl_section.Text;
                dr[3] = txt_no_of_student.Text;
                dt.Rows.Add(dr);
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(dt);
                Alertme("Strength Master Created Successfully", "success");
            }
            else
            {
                Alertme("Strength Master Already Created", "warning");
            }
        }
        private void empty_form()
        {
            txt_no_of_student.Text = "";
            btn_cancel.Visible = false;
            btn_Submit.Text = "Add";
        }


        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_class = (Label)row.FindControl("lbl_class");
                Label lbl_section = (Label)row.FindControl("lbl_section");
                Label lbl_no_of_student = (Label)row.FindControl("lbl_no_of_student");
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                hd_id.Value = lbl_Id.Text;

                ddl_class.Text = lbl_class.Text;
                ddl_section.Text = lbl_section.Text;
                txt_no_of_student.Text = lbl_no_of_student.Text;
                btn_cancel.Visible = true;
                btn_Submit.Text = "Update";
            }
            catch
            {

            }
        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            SqlCommand cmd;
            LinkButton lnk = (LinkButton)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            Label lbl_Id = (Label)row.FindControl("lbl_Id");
            string query = "delete from  strength_master where Id=@Id";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Id", lbl_Id.Text);
            if (My.InsertUpdateData(cmd))
            {
                Alertme("Strength has been delete Successfully.", "success");
                bind_grd_view();
            }
        }
    }
}