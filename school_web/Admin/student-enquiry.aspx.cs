using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class student_enquiry : System.Web.UI.Page
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
                        txt_s_date.Text = mycode.sevendaysback();
                        txt_e_date.Text = mycode.date();
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();
                        string pagename_current = "student-enquiry.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];

                        ViewState["Branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        mycode.bind_all_ddl_with_id(ddl_class, "Select cd.Course_Name,cd.Course_Name as course_id from Add_course_table cd order by cd.Position asc");
                        bind_grd_view();
                        txt_name.Focus();
                        find_firm_details();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_item_master");
            }
        }
        private void find_firm_details()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                imglogo.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_emaiid.Text = dt.Rows[0]["email"].ToString();
                lbl_website.Text = dt.Rows[0]["website"].ToString();
                lbl_contact_details.Text = dt.Rows[0]["contact_no"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();
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
            try
            {
                if (txt_s_date.Text == "")
                {
                    Alertme("Please choose from date", "warning");
                    txt_s_date.Focus();
                }
                else if (txt_e_date.Text == "")
                {
                    Alertme("Please choose to date", "warning");
                    txt_e_date.Focus();
                }
                else
                {
                    string sdate = txt_s_date.Text;
                    string sday = sdate.Substring(0, 2);
                    string smonth = sdate.Substring(3, 2);
                    string syear = sdate.Substring(6, 4);

                    string edate = txt_e_date.Text;
                    string eday = edate.Substring(0, 2);
                    string emonth = edate.Substring(3, 2);
                    string eyear = edate.Substring(6, 4);

                    int idate = Convert.ToInt32(syear + smonth + sday);
                    int idate2 = Convert.ToInt32(eyear + emonth + eday);

                    if (idate > idate2)
                    {
                        Alertme("End date cannot be less than start date.", "warning");
                    }
                    else
                    {
                        find_by_date(idate, idate2);
                    }




                }
            }
            catch (Exception ex)
            {
            }


            
        }
        protected void btn_find_Click(object sender, EventArgs e)
        {
            bind_grd_view();
        }
        private void find_by_date(int idate, int idate2)
        {
            print1.Visible = false;
            lbl_class22.Text = "";
            btn_excels.Visible = false;
            DataTable dt = mycode.FillData("select * from Student_enquiry where Idate>=" + idate + " and Idate<=" + idate2 + " order by Idate desc ");
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
                btn_excels.Visible = true;
                if (ViewState["Is_Print"].ToString() == "1")
                {
                    print1.Visible = true;
                }
                else
                {
                    print1.Visible = false;
                }

            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            empty_form();
        }

        protected void btn_Submit_Click1(object sender, EventArgs e)
        {

            if (txt_name.Text == "")
            {
                Alertme("Please enter student name.", "warning");
                txt_name.Focus();
                return;
            }
            if (ddl_class.SelectedItem.Text == "Select")
            {
                Alertme("Please select class.", "warning");
                ddl_class.Focus();
                return;
            }
            if (txt_father_name.Text == "")
            {
                Alertme("Please enter father's name.", "warning");
                txt_father_name.Focus();
                return;
            }
            if (txt_contact_no.Text == "")
            {
                Alertme("Please enter contact no.", "warning");
                txt_contact_no.Focus();
                return;
            }
            if (txt_address.Text == "")
            {
                Alertme("Please enter address.", "warning");
                txt_address.Focus();
                return;
            }
             
            if (btn_Submit.Text == "Add")
            {
                if (ViewState["Is_add"].ToString() == "1")
                {
                    submit_details();
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
                 
            }
            else
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    update_details();
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
                 
            }
        }


        private void update_details()
        {
            SqlCommand cmd;
            string query = "Update Student_enquiry set Name=@Name,Class=@Class,Father_name=@Father_name,Contact_no=@Contact_no,Address=@Address,Remark=@Remark,Updated_by=@Updated_by,Updated_date=@Updated_date where Id = '" + hd_id.Value + "'";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Name", txt_name.Text);
            cmd.Parameters.AddWithValue("@Class", ddl_class.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@Father_name", txt_father_name.Text);
            cmd.Parameters.AddWithValue("@Contact_no", txt_contact_no.Text);
            cmd.Parameters.AddWithValue("@Address", txt_address.Text);
            cmd.Parameters.AddWithValue("@Remark", txt_remarks.Text);
            cmd.Parameters.AddWithValue("@Updated_by", ViewState["Userid"].ToString());
            cmd.Parameters.AddWithValue("@Updated_date", mycode.date());
            if (My.InsertUpdateData(cmd))
            {
                Alertme("Student enquiry has been updated successfully", "success");
                empty_form();
                bind_grd_view();
            }
        }


        string slip_id;
        private void submit_details()
        {
            create_sl_no();
            SqlCommand cmd;
            string query = "INSERT INTO Student_enquiry (Enquiry_id,Name,Class,Father_name,Contact_no,Address,Remark,Created_by,Created_date,Created_idate,Branch_id) values (@Enquiry_id,@Name,@Class,@Father_name,@Contact_no,@Address,@Remark,@Created_by,@Created_date,@Created_idate,@Branch_id)";
            cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@Enquiry_id", slip_id);
            cmd.Parameters.AddWithValue("@Name", txt_name.Text);
            cmd.Parameters.AddWithValue("@Class", ddl_class.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@Father_name", txt_father_name.Text);
            cmd.Parameters.AddWithValue("@Contact_no", txt_contact_no.Text);
            cmd.Parameters.AddWithValue("@Address", txt_address.Text);
            cmd.Parameters.AddWithValue("@Remark", txt_remarks.Text);
            cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
            cmd.Parameters.AddWithValue("@Created_date", mycode.date());
            cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
            cmd.Parameters.AddWithValue("@Branch_id", ViewState["Branchid"].ToString());
            if (My.InsertUpdateData(cmd))
            {
                Alertme("Student enquiry has been created successfully.", "success");
                empty_form();
                bind_grd_view();
            }
        }


        private void create_sl_no()
        {
            bool duplicate = true;
            string slips_id = My.auto_serialS("Student_enquiry_no");

            if (slips_id.Length == 1)
            {
                slip_id = "000" + slips_id;
            }
            if (slips_id.Length == 2)
            {
                slip_id = "00" + slips_id;
            }
            if (slips_id.Length == 3)
            {
                slip_id = "0" + slips_id;
            }
            while (duplicate)
            {
                DataTable cdt = My.dataTable("select Slip_no from General_expense where Slip_no='" + slip_id + "'");
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    duplicate = false;
                }
                else
                {
                    string Item_ids = My.auto_serialS("Student_enquiry_no");
                    if (Item_ids.Length == 1)
                    {
                        slip_id = "000" + Item_ids;
                    }
                    if (Item_ids.Length == 2)
                    {
                        slip_id = "00" + Item_ids;
                    }
                    if (Item_ids.Length == 3)
                    {
                        slip_id = "0" + Item_ids;
                    }
                }
            }
        }



        private void empty_form()
        {
            txt_name.Text = "";
            txt_father_name.Text = ""; 
            txt_contact_no.Text = "";
            txt_address.Text = "";
            txt_remarks.Text = ""; 
            btn_cancel.Visible = false;
            btn_Submit.Text = "Add";
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_name = (Label)row.FindControl("lbl_name");
                    Label lbl_class = (Label)row.FindControl("lbl_class");
                    Label lbl_father_name = (Label)row.FindControl("lbl_father_name");
                    Label lbl_contact_no = (Label)row.FindControl("lbl_contact_no");
                    Label lbl_address = (Label)row.FindControl("lbl_address");
                    Label lbl_remark = (Label)row.FindControl("lbl_remark");
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");
                    hd_id.Value = lbl_Id.Text;
                    txt_name.Text = lbl_name.Text;
                    ddl_class.Text = lbl_class.Text;
                    txt_father_name.Text = lbl_father_name.Text;
                    txt_contact_no.Text = lbl_contact_no.Text;
                    txt_address.Text = lbl_address.Text;
                    txt_remarks.Text = lbl_remark.Text;
                    btn_cancel.Visible = true;
                    btn_Submit.Text = "Update";
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            catch
            {
            }
        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            if (ViewState["Is_delete"].ToString() == "1")
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                Label lbl_unit_id = (Label)row.FindControl("lbl_unit_id");
                Label lbl_item_id = (Label)row.FindControl("lbl_item_id");
                My.exeSql("delete from Student_enquiry where Id='" + lbl_Id.Text + "'");
                Alertme("Record has been deleted successfully.", "success");
                bind_grd_view();
            }
            else
            {
                Alertme(My.get_restricted_message(), "warning");
            }
        }

        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Download"].ToString() == "1")
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=Export.xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    using (StringWriter sw = new StringWriter())
                    {
                        HtmlTextWriter hw = new HtmlTextWriter(sw);
                        Panel1.RenderControl(hw);
                        Response.Output.Write(sw.ToString());
                        Response.Flush();
                        Response.End();
                    }
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            catch
            {
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        
    }
}