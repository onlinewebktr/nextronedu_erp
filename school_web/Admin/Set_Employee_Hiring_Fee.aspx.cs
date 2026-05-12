using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
using System.IO;

namespace school_web.Admin
{
    public partial class Set_Employee_Hiring_Fee : System.Web.UI.Page
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
                        ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        string pagename_current = "Employee_Haring_Step.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        find_firm_details();

                        mycode.bind_all_ddl_with_id(ddl_session, "Select Session,session_id from session_details order by Session asc");
                        ddl_session.SelectedValue = My.get_session_id_onlinereg();

                        mycode.bind_all_ddl_with_id(ddl_session_fee, "Select Session,session_id from session_details order by Session asc");
                        ddl_session_fee.SelectedValue = My.get_session_id_onlinereg();

                        mycode.bind_all_ddl_with_id(ddl_Hiring_Type, "Select Hiring_name,Hiring_id from Employees_Create_Hiring order by Hiring_name asc");
                        mycode.bind_all_ddl_with_id(ddl_Hiring_Type_add, "Select Hiring_name,Hiring_id from Employees_Create_Hiring order by Hiring_name asc");

                        lbl_msg.Text = "";

                        bind_grd_view();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Set_Employee_Hiring_Fee");
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

        private void bind_grd_view()
        {

            string query = " Select orf.*,(Select top  1 Session from session_details where session_id=orf.Session_id) as Session,CASE WHEN Isactive = '1' THEN 'Active' WHEN Isactive = '0' THEN 'Inactive'  WHEN Isactive = '' THEN 'Inactive' END AS activestatus,(Select top  1 Hiring_name from Employees_Create_Hiring where Session_id=orf.Session_id and Hiring_id=orf.Hiring_id) as Hiring_name   from Employee_Hiring_Fee_and_Seat orf    where Session_id='" + ddl_session.SelectedValue + "' and Branchi_id='" + ViewState["branchid"].ToString() + "' order by orf.Employee_Hiring_For asc";

            bind_final_grid_data(query);


        }

        private void bind_final_grid_data(string query)
        {
            btn_excels.Visible = false;
            print1.Visible = false;
            DataTable dt = mycode.FillData(query);
            ViewState["query"] = query;
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                btn_excels.Visible = true;
                print1.Visible = true;
                rd_view.DataSource = dt;
                rd_view.DataBind();

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

        protected void ddl_Hiring_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Hiring_Type.SelectedItem.Text == "Select")
            {
                Alertme("Please select hiring type", "warning");
            }
            else
            {
                string query = " Select orf.*,(Select top  1 Session from session_details where session_id=orf.Session_id) as Session,CASE WHEN Isactive = '1' THEN 'Active' WHEN Isactive = '0' THEN 'Inactive'  WHEN Isactive = '' THEN 'Inactive' END AS activestatus,(Select top  1 Hiring_name from Employees_Create_Hiring where Session_id=orf.Session_id and Hiring_id=orf.Hiring_id) as Hiring_name   from Employee_Hiring_Fee_and_Seat orf    where orf.Session_id='" + ddl_session.SelectedValue + "' and orf.Branchi_id='" + ViewState["branchid"].ToString() + "' and orf.Hiring_id=" + ddl_Hiring_Type.SelectedValue + " order by orf.Employee_Hiring_For asc";
                bind_final_grid_data(query);
            }
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    lbl_msg.Text = "";
                    txt_coursefee.Text = "";
                    hd_id.Value = "";
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_Fees = (Label)row.FindControl("lbl_Fees");
                    Label lbl_Isactive = (Label)row.FindControl("lbl_Isactive");
                    Label lbl_Start_Date = (Label)row.FindControl("lbl_Start_Date");
                    Label lbl_end_date = (Label)row.FindControl("lbl_end_date");
                    Label lbl_Hiring_id = (Label)row.FindControl("lbl_Hiring_id");
                    Label lbl_no_seat = (Label)row.FindControl("lbl_no_seat");
                    Label lbl_no_application = (Label)row.FindControl("lbl_no_application");
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");
                    Label lbl_sessions_ids = (Label)row.FindControl("lbl_sessions_ids");
                    Label lbl_Employee_Hiring_For = (Label)row.FindControl("lbl_Employee_Hiring_For");
                    Label lbl_General_instructions = (Label)row.FindControl("lbl_General_instructions");
                    Label lbl_attechment = (Label)row.FindControl("lbl_attechment");

                    if (lbl_attechment.Text == "")
                    {
                        ViewState["attechment"] = "";
                        file1.Visible = false;

                    }
                    else
                    {
                        ViewState["attechment"] = lbl_attechment.Text;
                        file1.Visible = true;

                        file1.HRef = lbl_attechment.Text;
                    }

                    txt_info.Value = lbl_General_instructions.Text;

                    ddl_session_fee.SelectedValue = lbl_sessions_ids.Text;
                    ddl_Hiring_Type_add.SelectedValue = lbl_Hiring_id.Text;
                    ddl_Hiring_For.SelectedValue = lbl_Employee_Hiring_For.Text;

                    ddl_session_fee.Enabled = false;
                    ddl_Hiring_Type_add.Enabled = false;
                    ddl_Hiring_For.Enabled = false;
                    ddl_session_fee.CssClass = "form-select";
                    ddl_Hiring_For.CssClass = "form-select";
                    ddl_Hiring_Type_add.CssClass = "form-select";
                    txt_coursefee.Text = lbl_Fees.Text;

                    txt_no_seat.Text = lbl_no_seat.Text;
                    txt_no_application.Text = lbl_no_application.Text;
                    txt_start_date.Text = lbl_Start_Date.Text;
                    txt_end_date.Text = lbl_end_date.Text;
                    hd_id.Value = lbl_Id.Text;
                    btn_cancel.Visible = true;
                    btn_Submit.Text = "Update";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
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


        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            if (ViewState["Is_add"].ToString() == "1")
            {
                final_submit_data();
            }
            else if (ViewState["Is_Edit"].ToString() == "1")
            {
                final_submit_data();
            }
            else
            {
                Alertme(My.get_restricted_message(), "warning");
            }



        }

        private void final_submit_data()
        {

            lbl_msg.Text = "";
            if (ddl_session_fee.SelectedItem.Text == "Select")
            {
                ddl_session_fee.Focus();
                lbl_msg.Text = "Please select session";
                Alertme("Please select session", "warning");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            else if (ddl_Hiring_Type_add.SelectedItem.Text == "Select")
            {
                ddl_Hiring_Type_add.Focus();
                lbl_msg.Text = "Please select hiring type";
                Alertme("Please select session", "warning");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            else if (ddl_Hiring_For.SelectedItem.Text == "Select")
            {
                ddl_Hiring_For.Focus();
                lbl_msg.Text = "Please select hiring for";
                Alertme("Please select session", "warning");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            else if (txt_coursefee.Text == "")
            {
                lbl_msg.Text = "Please enter fees";
                Alertme("Please enter fees", "warning");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            else if (txt_no_seat.Text == "")
            {
                lbl_msg.Text = "Please enter no. of seat";
                Alertme("Please enter fees", "warning");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            else if (txt_no_application.Text == "")
            {
                lbl_msg.Text = "Please enter number of application fill";
                Alertme("Please enter fees", "warning");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }

            else if (txt_start_date.Text == "")
            {
                lbl_msg.Text = "Please enter start date";
                Alertme("Please enter fees", "warning");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }

            else if (txt_end_date.Text == "")
            {
                lbl_msg.Text = "Please enter end date";
                Alertme("Please enter fees", "warning");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            else
            {


                int idate = mycode.ConvertStringToiDate(txt_start_date.Text);
                int idate2 = mycode.ConvertStringToiDate(txt_end_date.Text);
                if (idate > idate2)
                {
                    Alertme("End date cannot be less than start date.", "warning");
                    lbl_msg.Text = "End date cannot be less than start date.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                else
                {
                    string filepath = "";
                    if (FileUpload1.HasFile)
                    {
                        if (FileUpload1.FileBytes.Length < 5000000)
                        {
                            filepath = upload_thumb_attachment();
                            if (filepath == "")
                            {
                                Alertme("Please choose valid image.", "warning");
                                return;
                            }
                            else

                            {
                                if (btn_Submit.Text == "Add")
                                {

                                    SqlCommand cmd;
                                    DataTable dt = mycode.FillData("Select * from Employee_Hiring_Fee_and_Seat where  Session_id='" + ddl_session.SelectedValue + "' and Branchi_id='" + ViewState["branchid"].ToString() + "'  and Employee_Hiring_For='" + ddl_Hiring_For.Text + "' and Hiring_id='" + ddl_Hiring_Type_add.SelectedValue + "' ");
                                    if (dt.Rows.Count == 0)
                                    {
                                        double amot = My.toDouble(txt_coursefee.Text);
                                        string query = "INSERT INTO Employee_Hiring_Fee_and_Seat (Session_id,Employee_Hiring_For,Fees,Branchi_id,User_id,Date_time,no_seat,no_application,start_date,start_Idate,end_date,end_Idate,Isactive,Hiring_id,General_instructions,attechment) values (@Session_id,@Employee_Hiring_For,@Fees,@Branchi_id,@User_id,@Date_time,@no_seat,@no_application,@start_date,@start_Idate,@end_date,@end_Idate,@Isactive,@Hiring_id,@General_instructions,@attechment)";
                                        cmd = new SqlCommand(query);
                                        cmd.Parameters.AddWithValue("@Session_id", ddl_session_fee.SelectedValue);
                                        cmd.Parameters.AddWithValue("@Employee_Hiring_For", ddl_Hiring_For.Text);
                                        cmd.Parameters.AddWithValue("@Fees", amot.ToString("0.00"));
                                        cmd.Parameters.AddWithValue("@Branchi_id", ViewState["branchid"].ToString());
                                        cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                                        cmd.Parameters.AddWithValue("@Date_time", My.getdate1());
                                        cmd.Parameters.AddWithValue("@no_seat", txt_no_seat.Text);
                                        cmd.Parameters.AddWithValue("@no_application", txt_no_application.Text);
                                        cmd.Parameters.AddWithValue("@start_date", txt_start_date.Text);
                                        cmd.Parameters.AddWithValue("@start_Idate", mycode.ConvertStringToiDate(txt_start_date.Text));
                                        cmd.Parameters.AddWithValue("@end_date", txt_end_date.Text);
                                        cmd.Parameters.AddWithValue("@end_Idate", mycode.ConvertStringToiDate(txt_end_date.Text));
                                        cmd.Parameters.AddWithValue("@Isactive", 1);
                                        cmd.Parameters.AddWithValue("@Hiring_id", ddl_Hiring_Type_add.SelectedValue);
                                        cmd.Parameters.AddWithValue("@General_instructions", txt_info.Value);
                                        cmd.Parameters.AddWithValue("@attechment", filepath);


                                        if (My.InsertUpdateData(cmd))
                                        {
                                            lbl_msg.Text = "Employee Hiring Fee has been saved successfully";
                                            Alertme("Employee Hiring Fee has been saved successfully", "success");
                                            txt_coursefee.Text = "";
                                            ddl_session_fee.Enabled = true;
                                            ddl_Hiring_Type_add.Enabled = true;
                                            ddl_Hiring_For.Enabled = true;
                                            txt_no_seat.Text = "";
                                            txt_no_application.Text = "";
                                            txt_start_date.Text = "";
                                            txt_end_date.Text = "";
                                            hd_id.Value = "0";
                                            btn_Submit.Text = "Add";
                                            txt_info.Value = "";
                                            bind_grd_view();
                                        }
                                    }
                                    else
                                    {
                                        lbl_msg.Text = "Sorry your selected hiring for fees already added";
                                        Alertme("Sorry your selected hiring for fees already added", "warning");
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                                    }
                                }
                                else
                                {

                                    SqlCommand cmd;
                                    double amot = My.toDouble(txt_coursefee.Text);

                                    string query = "Update Employee_Hiring_Fee_and_Seat set  Fees=@Fees,Date_time=@Date_time,no_seat=@no_seat,no_application=@no_application,start_date=@start_date,start_Idate=@start_Idate,end_date=@end_date,end_Idate=@end_Idate,Hiring_id=@Hiring_id,General_instructions=@General_instructions,attechment=@attechment where Id = @Id";
                                    cmd = new SqlCommand(query);
                                    cmd.Parameters.AddWithValue("@Fees", amot.ToString("0.00"));
                                    cmd.Parameters.AddWithValue("@Date_time", My.getdate1());
                                    cmd.Parameters.AddWithValue("@no_seat", txt_no_seat.Text);
                                    cmd.Parameters.AddWithValue("@no_application", txt_no_application.Text);
                                    cmd.Parameters.AddWithValue("@start_date", txt_start_date.Text);
                                    cmd.Parameters.AddWithValue("@start_Idate", mycode.ConvertStringToiDate(txt_start_date.Text));
                                    cmd.Parameters.AddWithValue("@end_date", txt_end_date.Text);
                                    cmd.Parameters.AddWithValue("@end_Idate", mycode.ConvertStringToiDate(txt_end_date.Text));
                                    cmd.Parameters.AddWithValue("@Hiring_id", ddl_Hiring_Type_add.SelectedValue);
                                    cmd.Parameters.AddWithValue("@General_instructions", txt_info.Value);
                                    cmd.Parameters.AddWithValue("@Id", hd_id.Value);
                                    cmd.Parameters.AddWithValue("@attechment", filepath);
                                    if (My.InsertUpdateData(cmd))
                                    {

                                        ddl_session_fee.Enabled = true;
                                        ddl_Hiring_Type_add.Enabled = true;
                                        ddl_Hiring_For.Enabled = true;
                                        lbl_msg.Text = "Fees has been saved successfully";
                                        Alertme("Fees has been saved successfully", "success");
                                        txt_coursefee.Text = "";
                                        txt_no_seat.Text = "";
                                        txt_no_application.Text = "";
                                        txt_start_date.Text = "";
                                        txt_end_date.Text = "";
                                        hd_id.Value = "0";
                                        btn_Submit.Text = "Add";
                                        txt_info.Value = "";
                                        bind_grd_view();
                                    }

                                }

                            }

                        }
                        else
                        {
                            Alertme("Please Reduce or compress size of image max(500 kb).", "warning");
                        }

                    }
                    else

                    {

                        if (btn_Submit.Text == "Add")
                        {

                            SqlCommand cmd;
                            DataTable dt = mycode.FillData("Select * from Employee_Hiring_Fee_and_Seat where  Session_id='" + ddl_session.SelectedValue + "' and Branchi_id='" + ViewState["branchid"].ToString() + "'  and Employee_Hiring_For='" + ddl_Hiring_For.Text + "' and Hiring_id='" + ddl_Hiring_Type_add.SelectedValue + "' ");
                            if (dt.Rows.Count == 0)
                            {
                                double amot = My.toDouble(txt_coursefee.Text);
                                string query = "INSERT INTO Employee_Hiring_Fee_and_Seat (Session_id,Employee_Hiring_For,Fees,Branchi_id,User_id,Date_time,no_seat,no_application,start_date,start_Idate,end_date,end_Idate,Isactive,Hiring_id,General_instructions,attechment) values (@Session_id,@Employee_Hiring_For,@Fees,@Branchi_id,@User_id,@Date_time,@no_seat,@no_application,@start_date,@start_Idate,@end_date,@end_Idate,@Isactive,@Hiring_id,@General_instructions,@attechment)";
                                cmd = new SqlCommand(query);
                                cmd.Parameters.AddWithValue("@Session_id", ddl_session_fee.SelectedValue);
                                cmd.Parameters.AddWithValue("@Employee_Hiring_For", ddl_Hiring_For.Text);
                                cmd.Parameters.AddWithValue("@Fees", amot.ToString("0.00"));
                                cmd.Parameters.AddWithValue("@Branchi_id", ViewState["branchid"].ToString());
                                cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                                cmd.Parameters.AddWithValue("@Date_time", My.getdate1());
                                cmd.Parameters.AddWithValue("@no_seat", txt_no_seat.Text);
                                cmd.Parameters.AddWithValue("@no_application", txt_no_application.Text);
                                cmd.Parameters.AddWithValue("@start_date", txt_start_date.Text);
                                cmd.Parameters.AddWithValue("@start_Idate", mycode.ConvertStringToiDate(txt_start_date.Text));
                                cmd.Parameters.AddWithValue("@end_date", txt_end_date.Text);
                                cmd.Parameters.AddWithValue("@end_Idate", mycode.ConvertStringToiDate(txt_end_date.Text));
                                cmd.Parameters.AddWithValue("@Isactive", 1);
                                cmd.Parameters.AddWithValue("@Hiring_id", ddl_Hiring_Type_add.SelectedValue);
                                cmd.Parameters.AddWithValue("@General_instructions", txt_info.Value);
                                cmd.Parameters.AddWithValue("@attechment", txt_info.Value);


                                if (My.InsertUpdateData(cmd))
                                {
                                    lbl_msg.Text = "Employee Hiring Fee has been saved successfully";
                                    Alertme("Employee Hiring Fee has been saved successfully", "success");
                                    txt_coursefee.Text = "";
                                    ddl_session_fee.Enabled = true;
                                    ddl_Hiring_Type_add.Enabled = true;
                                    ddl_Hiring_For.Enabled = true;
                                    txt_no_seat.Text = "";
                                    txt_no_application.Text = "";
                                    txt_start_date.Text = "";
                                    txt_end_date.Text = "";
                                    hd_id.Value = "0";
                                    btn_Submit.Text = "Add";
                                    txt_info.Value = "";
                                    bind_grd_view();
                                }
                            }
                            else
                            {
                                lbl_msg.Text = "Sorry your selected hiring for fees already added";
                                Alertme("Sorry your selected hiring for fees already added", "warning");
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                            }
                        }
                        else
                        {

                            SqlCommand cmd;
                            double amot = My.toDouble(txt_coursefee.Text);

                            string query = "Update Employee_Hiring_Fee_and_Seat set  Fees=@Fees,Date_time=@Date_time,no_seat=@no_seat,no_application=@no_application,start_date=@start_date,start_Idate=@start_Idate,end_date=@end_date,end_Idate=@end_Idate,Hiring_id=@Hiring_id,General_instructions=@General_instructions where Id = @Id";
                            cmd = new SqlCommand(query);
                            cmd.Parameters.AddWithValue("@Fees", amot.ToString("0.00"));
                            cmd.Parameters.AddWithValue("@Date_time", My.getdate1());
                            cmd.Parameters.AddWithValue("@no_seat", txt_no_seat.Text);
                            cmd.Parameters.AddWithValue("@no_application", txt_no_application.Text);
                            cmd.Parameters.AddWithValue("@start_date", txt_start_date.Text);
                            cmd.Parameters.AddWithValue("@start_Idate", mycode.ConvertStringToiDate(txt_start_date.Text));
                            cmd.Parameters.AddWithValue("@end_date", txt_end_date.Text);
                            cmd.Parameters.AddWithValue("@end_Idate", mycode.ConvertStringToiDate(txt_end_date.Text));
                            cmd.Parameters.AddWithValue("@Hiring_id", ddl_Hiring_Type_add.SelectedValue);
                            cmd.Parameters.AddWithValue("@General_instructions", txt_info.Value);
                            cmd.Parameters.AddWithValue("@Id", hd_id.Value);
                            if (My.InsertUpdateData(cmd))
                            {

                                ddl_session_fee.Enabled = true;
                                ddl_Hiring_Type_add.Enabled = true;
                                ddl_Hiring_For.Enabled = true;
                                lbl_msg.Text = "Fees has been saved successfully";
                                Alertme("Fees has been saved successfully", "success");
                                txt_coursefee.Text = "";
                                txt_no_seat.Text = "";
                                txt_no_application.Text = "";
                                txt_start_date.Text = "";
                                txt_end_date.Text = "";
                                hd_id.Value = "0";
                                btn_Submit.Text = "Add";
                                txt_info.Value = "";
                                bind_grd_view();
                            }

                        }

                    }




                }




            }
        }

        private string upload_thumb_attachment()
        {
            string dbfilePath = "";
            DateTime dt = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string date = dt.ToString("dd_MM_yyyy");
            string time = dt.ToString("hh_mm_ss");
            String filerename = date + time;
            Boolean FileOK = false;
            Boolean FileSaved = false;
            int k = 0;
            if (FileUpload1.HasFile)
            {
                if (FileUpload1.FileBytes.Length < 500000)
                {
                    Session["WorkingImage"] = FileUpload1.FileName;
                    string FileExtension = Path.GetExtension(Session["WorkingImage"].ToString()).ToLower();
                    Session["renamedfile"] = filerename + "PIMG1" + FileExtension;
                    string[] allowedExtension = { ".png", ".jpeg", ".jpg", ".pdf" };
                    for (int i = 0; i < allowedExtension.Length; i++)
                    {
                        k++;
                        if (FileExtension == allowedExtension[i])
                        {
                            FileOK = true;
                            break;
                        }
                    }
                }
                else
                {
                    Alertme("Please image size maximum 500kb.", "warning");
                }
            }
            else
            {
                Alertme("Please upload image first.", "warning");
            }
            if (FileOK)
            {
                try
                {
                    string path = (Server.MapPath("../UploadedImage/schoollogo")).ToString();
                    FileUpload1.SaveAs(path + "/" + Session["renamedfile"]);
                    FileSaved = true;
                }
                catch (Exception ex)
                {
                    FileSaved = false;
                    Alertme("File has not save.", "warning");
                }
            }
            else
            {

            }
            if (FileSaved)
            {
                string originalPath2 = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, ""); string[] New_originalPath1 = originalPath2.Split('?'); string originalPath1 = New_originalPath1[0].ToString();
                string fileName = Path.GetFileName(Session["renamedfile"].ToString());
                dbfilePath = originalPath1 + "/UploadedImage/schoollogo/" + fileName;
            }
            return dbfilePath;
        }



        protected void btn_add_Click(object sender, EventArgs e)
        {
            ddl_session_fee.Enabled = true;
            ddl_Hiring_Type_add.Enabled = true;
            ddl_Hiring_For.Enabled = true;
            lbl_msg.Text = "";
            txt_coursefee.Text = "";
            txt_no_seat.Text = "";
            txt_no_application.Text = "";
            txt_start_date.Text = "";
            txt_end_date.Text = "";
            hd_id.Value = "0";

            if (ViewState["Is_add"].ToString() == "1")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            else
            {
                Alertme(My.get_restricted_message(), "warning");
            }

            if (ViewState["Is_Edit"].ToString() == "1")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            else
            {
                Alertme(My.get_restricted_message(), "warning");

            }
           


        }

        protected void ddl_session_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddl_session.SelectedItem.Text == "Select")
            {
                Alertme("Please select session", "warning");
            }
            else
            {
                bind_grd_view();
            }
        }

        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {

                    if (((Label)e.Item.FindControl("lbl_Isactive")).Text == "1")
                    {
                        ((LinkButton)e.Item.FindControl("lnkActive")).Text = "Inactive";
                    }
                    else
                    {
                        ((LinkButton)e.Item.FindControl("lnkActive")).Text = "Active";

                    }


                }
            }
            catch { }
        }

        #region active and inactive
        protected void lnkActive_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_add"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");
                    Label lbl_Isactive = (Label)row.FindControl("lbl_Isactive");
                    if (lbl_Isactive.Text == "")// Inactive
                    {
                        mycode.executequery("update Employee_Hiring_Fee_and_Seat set Isactive='1' where Id='" + lbl_Id.Text + "'");
                        Alertme("Your select item has been activated successfully", "success");
                    }
                    else if (lbl_Isactive.Text == "0")// Inactive
                    {
                        mycode.executequery("update Employee_Hiring_Fee_and_Seat set Isactive='1' where Id='" + lbl_Id.Text + "'");
                        Alertme("Your select item has been activated successfully", "success");
                    }
                    else if (lbl_Isactive.Text == "1")// Inactive
                    {
                        mycode.executequery("update Employee_Hiring_Fee_and_Seat set Isactive='0' where Id='" + lbl_Id.Text + "'");

                        Alertme("Your select item has been Inactivated successfully", "success");
                    }


                    bind_grd_view();

                }
                else if(ViewState["Is_Edit"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");
                    Label lbl_Isactive = (Label)row.FindControl("lbl_Isactive");
                    if (lbl_Isactive.Text == "")// Inactive
                    {
                        mycode.executequery("update Employee_Hiring_Fee_and_Seat set Isactive='1' where Id='" + lbl_Id.Text + "'");
                        Alertme("Your select item has been activated successfully", "success");
                    }
                    else if (lbl_Isactive.Text == "0")// Inactive
                    {
                        mycode.executequery("update Employee_Hiring_Fee_and_Seat set Isactive='1' where Id='" + lbl_Id.Text + "'");
                        Alertme("Your select item has been activated successfully", "success");
                    }
                    else if (lbl_Isactive.Text == "1")// Inactive
                    {
                        mycode.executequery("update Employee_Hiring_Fee_and_Seat set Isactive='0' where Id='" + lbl_Id.Text + "'");

                        Alertme("Your select item has been Inactivated successfully", "success");
                    }


                    bind_grd_view();
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");

                }
               
            }
            catch (Exception ex)
            {


            }


        }
        #endregion

        protected void ddl_session_fee_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                mycode.bind_all_ddl_with_id(ddl_Hiring_Type_add, "Select Hiring_name,Hiring_id from Employees_Create_Hiring  where Session_id='" + ddl_session_fee.SelectedValue + "' and  Is_active=1  order by Hiring_name asc");


            }
            catch (Exception ex)
            {
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