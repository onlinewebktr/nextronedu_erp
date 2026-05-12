using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class Id_card_employee_print : System.Web.UI.Page
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
                        ViewState["Employee_image"] = "";
                        ViewState["IsPlusTwoChecked"] = "NO";
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["Branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());

                        mycode.bind_ddlall(ddl_type, "select DISTINCT t2.User_Type from PRL_Employee_Master t1 join user_details t2 on t1.Emp_Code=t2.user_id  order by t2.User_Type asc");

                        mycode.bind_all_ddl_with_id_cap_All(ddl_employee, "select (t2.name +' (User id : '+t2.user_id+' - '+t2.User_Type+')') as Name,t2.user_id from PRL_Employee_Master t1 join user_details t2 on t1.Emp_Code=t2.user_id  order by t2.name asc");

                        find_id_card_type();

                        bind_grd_view();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Student_Result");
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
            string qry = "";
            if (ddl_type.SelectedItem.Text == "ALL" && ddl_employee.SelectedItem.Text == "ALL")
            {
                //qry = "select em.*,dpm.name Department, dsm.name Designation, gm.grade_name from HR_Employee_Master em left join HR_Grade_Master gm on em.Grade_id = gm.grade_id left join HR_Designation_Master dsm on dsm.Designation_id = em.Designation_id left join HR_Department_Master dpm on dpm.department_id = em.Department_id";
                qry = "select * from PRL_Employee_Master order by Employee_Name asc";
            }
            else if (ddl_type.SelectedItem.Text != "ALL" && ddl_employee.SelectedItem.Text == "ALL")
            {
                //qry = "select em.*,dpm.name Department, dsm.name Designation, gm.grade_name from HR_Employee_Master em left join HR_Grade_Master gm on em.Grade_id = gm.grade_id left join HR_Designation_Master dsm on dsm.Designation_id = em.Designation_id left join HR_Department_Master dpm on dpm.department_id = em.Department_id where em.employee_type='" + ddl_type.SelectedItem.Text + "'";
                qry = "select * from PRL_Employee_Master where employee_type='" + ddl_type.SelectedItem.Text + "' order by Employee_Name asc";
            }
            else if (ddl_type.SelectedItem.Text == "ALL" && ddl_employee.SelectedItem.Text != "ALL")
            {
                //qry = "select em.*,dpm.name Department, dsm.name Designation, gm.grade_name from HR_Employee_Master em left join HR_Grade_Master gm on em.Grade_id = gm.grade_id left join HR_Designation_Master dsm on dsm.Designation_id = em.Designation_id left join HR_Department_Master dpm on dpm.department_id = em.Department_id where em.Emp_Code='" + ddl_employee.SelectedValue + "'";
                qry = "select * from PRL_Employee_Master where Emp_Code='" + ddl_employee.SelectedValue + "' order by Employee_Name asc";
            }
            else if (ddl_type.SelectedItem.Text != "ALL" && ddl_employee.SelectedItem.Text != "ALL")
            {
                //qry = "select em.*,dpm.name Department, dsm.name Designation, gm.grade_name from HR_Employee_Master em left join HR_Grade_Master gm on em.Grade_id = gm.grade_id left join HR_Designation_Master dsm on dsm.Designation_id = em.Designation_id left join HR_Department_Master dpm on dpm.department_id = em.Department_id where em.Emp_Code='" + ddl_employee.SelectedValue + "'";
                qry = "select * from PRL_Employee_Master where employee_type='" + ddl_type.SelectedItem.Text + "' and Emp_Code='" + ddl_employee.SelectedValue + "' order by Employee_Name asc";
            }
            else
            {
                qry = "select * from PRL_Employee_Master order by Employee_Name asc";
            }
            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }



        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                bind_grd_view();
            }
            catch (Exception ex)
            {
            }
        }

        protected void btn_print_all_Click(object sender, EventArgs e)
        {
            try
            {
                string emp_ids = "";
                int growcount = rd_view.Items.Count;
                int k = 0;
                for (int i = 0; i < growcount; i++)
                {
                    CheckBox chk = (CheckBox)rd_view.Items[i].FindControl("chkRowData");
                    if (chk.Checked == true)
                    {
                        Label lbl_id = (Label)rd_view.Items[i].FindControl("lbl_id");
                        emp_ids = emp_ids += lbl_id.Text + ",";
                    }
                    else
                    {
                        k++;
                    }
                }

                if (k == growcount)
                {
                    if (ddl_employee.SelectedItem.Text == "ALL")
                    {
                        Response.Redirect(ViewState["IdCardPage"].ToString() + "?UserType=" + ddl_type.Text + "&empid=ALL&Branch_id=" + ViewState["Branchid"].ToString() + "&Type=BULK", false);
                    }
                    else
                    {
                        Response.Redirect(ViewState["IdCardPage"].ToString() + "?UserType=" + ddl_type.Text + "&empid=" + ddl_employee.SelectedValue + "&Branch_id=" + ViewState["Branchid"].ToString() + "&Type=SINGLE", false);
                    }
                }
                else
                {
                    string reslink = ViewState["IdCardPage"].ToString() + "?UserType=0&empid=" + emp_ids + "&Branch_id=" + ViewState["Branchid"].ToString() + "&Type=CHECK";
                    Response.Redirect(reslink, false);
                }
            }
            catch (Exception ex)
            {
            }
        }



        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbl_employee_id = ((Label)e.Item.FindControl("lbl_employee_id")) as Label;
                Label lbl_session_id = ((Label)e.Item.FindControl("lbl_session_id")) as Label;
                Label lbl_class_id = ((Label)e.Item.FindControl("lbl_class_id")) as Label;
                Label lbl_branch_id = ((Label)e.Item.FindControl("lbl_branch_id")) as Label;
                HtmlAnchor idcard_link = (HtmlAnchor)e.Item.FindControl("idcard_link");
                HtmlAnchor idcard_linkBack = (HtmlAnchor)e.Item.FindControl("idcard_linkBack");
                Label lbl_Is_Allow_edit = ((Label)e.Item.FindControl("lbl_Is_Allow_edit")) as Label;
                Label lbl_edit_permission = ((Label)e.Item.FindControl("lbl_edit_permission")) as Label;
                HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("trR");
                idcard_link.HRef = ViewState["IdCardPageSingleFront"].ToString() + "?UserType=ALL &empid=" + lbl_employee_id.Text + "&Branch_id=" + ViewState["Branchid"].ToString() + "&Type=SINGLE";
                idcard_linkBack.HRef = ViewState["IdCardPageSingleBack"].ToString() + "?UserType=ALL &empid=" + lbl_employee_id.Text + "&Branch_id=" + ViewState["Branchid"].ToString() + "&Type=SINGLE";


                if (lbl_Is_Allow_edit.Text == "0")
                {
                    tr.Attributes.Add("style", "background-color:#ffe5c3;color:#000000;");
                    lbl_edit_permission.Text = "Yes";
                }
                else if (lbl_Is_Allow_edit.Text == "")
                {
                    tr.Attributes.Add("style", "background-color:#ffe5c3;color:#000000;");
                    lbl_edit_permission.Text = "Yes";
                }
                else
                {
                    tr.Attributes.Add("style", "background-color:#f0ab53;color:#FFFFFF;");
                    lbl_edit_permission.Text = "No";
                }
            }
        }



        private void find_id_card_type()
        {
            DataTable dtF = My.dataTable("select firm_name,firm_id from Firm_Details");
            string querym = "select * from Id_card_template_setting where  Branch_id='" + ViewState["Branchid"].ToString() + "' and Type='Employee'";
            DataTable dtm = mycode.FillData(querym);
            if (dtm.Rows.Count > 0)
            {
                if (dtm.Rows[0]["Id_card_type"].ToString() == "Horizontal")
                {
                    if (dtF.Rows[0]["firm_id"].ToString().ToUpper() == "SMSITAHAR-1" || dtF.Rows[0]["firm_id"].ToString().ToUpper() == "CKG-01" || dtF.Rows[0]["firm_id"].ToString().ToUpper() == "CCF-01" || dtF.Rows[0]["firm_id"].ToString().ToUpper() == "SMI-001")
                    {
                        ViewState["IdCardPage"] = "id-card/print-vr-id-card-employee.aspx";
                        ViewState["IdCardPageSingleFront"] = "id-card/id-card-front-image-emp001.aspx";
                        ViewState["IdCardPageSingleBack"] = "id-card/id-card-back-image-emp001.aspx";
                    }
                    else if (dtF.Rows[0]["firm_id"].ToString().ToUpper() == "KIDS-01")
                    {
                        ViewState["IdCardPage"] = "id-card/kdes/bulk-emp-id.aspx";
                        ViewState["IdCardPageSingleFront"] = "id-card/kdes/bulk-emp-id.aspx";
                        ViewState["IdCardPageSingleBack"] = "id-card/kdes/bulk-emp-id.aspx";
                    }
                    else if (dtF.Rows[0]["firm_id"].ToString().ToUpper() == "GAP-01" || dtF.Rows[0]["firm_id"].ToString().ToUpper() == "ABC-002" || dtF.Rows[0]["firm_id"].ToString().ToUpper() == "ABC-001" || dtF.Rows[0]["firm_id"].ToString().ToUpper() == "GPSKTR")
                    {
                        ViewState["IdCardPage"] = "id-card/print-vr-id-card-emp.aspx";
                        ViewState["IdCardPageSingleFront"] = "id-card/print-vr-id-card-emp.aspx";
                        ViewState["IdCardPageSingleBack"] = "id-card/print-vr-id-card-emp.aspx";
                    }
                    else
                    {
                        ViewState["IdCardPage"] = "id-card/print-vr-id-card-employee.aspx";
                        ViewState["IdCardPageSingleFront"] = "id-card/print-vr-id-card-employee.aspx";
                        ViewState["IdCardPageSingleBack"] = "id-card/print-vr-id-card-employee.aspx";
                    }
                }
                else
                {
                    if (dtF.Rows[0]["firm_id"].ToString().ToUpper() == "SMSITAHAR-1" || dtF.Rows[0]["firm_id"].ToString().ToUpper() == "CKG-01" || dtF.Rows[0]["firm_id"].ToString().ToUpper() == "CCF-01" || dtF.Rows[0]["firm_id"].ToString().ToUpper() == "SMI-001")
                    {
                        ViewState["IdCardPage"] = "id-card/print-vr-id-card-employee.aspx";
                        ViewState["IdCardPageSingleFront"] = "id-card/id-card-front-image-emp001.aspx";
                        ViewState["IdCardPageSingleBack"] = "id-card/id-card-back-image-emp001.aspx";
                    }
                    else if (dtF.Rows[0]["firm_id"].ToString().ToUpper() == "KIDS-01")
                    {
                        ViewState["IdCardPage"] = "id-card/kdes/bulk-emp-id.aspx";
                        ViewState["IdCardPageSingleFront"] = "id-card/kdes/bulk-emp-id.aspx";
                        ViewState["IdCardPageSingleBack"] = "id-card/kdes/bulk-emp-id.aspx";
                    }
                    else if (dtF.Rows[0]["firm_id"].ToString().ToUpper() == "GAP-01" || dtF.Rows[0]["firm_id"].ToString().ToUpper() == "ABC-002" || dtF.Rows[0]["firm_id"].ToString().ToUpper() == "ABC-001" || dtF.Rows[0]["firm_id"].ToString().ToUpper() == "GPSKTR")
                    {
                        ViewState["IdCardPage"] = "id-card/print-vr-id-card-emp.aspx";
                        ViewState["IdCardPageSingleFront"] = "id-card/print-vr-id-card-emp.aspx";
                        ViewState["IdCardPageSingleBack"] = "id-card/print-vr-id-card-emp.aspx";
                    }
                    else
                    {
                        ViewState["IdCardPage"] = "id-card/print-vr-id-card-employee.aspx";
                        ViewState["IdCardPageSingleFront"] = "id-card/print-vr-id-card-employee.aspx";
                        ViewState["IdCardPageSingleBack"] = "id-card/print-vr-id-card-employee.aspx";
                    }
                }
            }
            else
            {
                ViewState["IdCardPage"] = "#!";
                ViewState["IdCardPageSingleFront"] = "#!";
                ViewState["IdCardPageSingleBack"] = "#!";
            }
        }

        protected void ddl_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddl_type.Text == "ALL")
                {
                    mycode.bind_all_ddl_with_id_cap_All(ddl_employee, "select (t2.name +' (User id : '+t2.user_id+' - '+t2.User_Type+')') as Name,t2.user_id from PRL_Employee_Master t1 join user_details t2 on t1.Emp_Code=t2.user_id  order by t2.name asc");
                }
                else
                {
                    mycode.bind_all_ddl_with_id_cap_All(ddl_employee, "select (t2.name +' (User id : '+t2.user_id+' - '+t2.User_Type+')') as Name,t2.user_id from PRL_Employee_Master t1 join user_details t2 on t1.Emp_Code=t2.user_id where t2.User_Type='" + ddl_type.Text + "' order by t2.name asc");
                }
            }
            catch (Exception ex)
            {
            }
        }


        protected void lnk_edit_emp_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_employee_id = (Label)row.FindControl("lbl_employee_id");
                Label lbl_name = (Label)row.FindControl("lbl_name");
                Label lbl_mobile = (Label)row.FindControl("lbl_mobile");
                Label lbl_id = (Label)row.FindControl("lbl_id");
                hd_emp_code.Value = lbl_employee_id.Text;
                hd_id.Value = lbl_id.Text;
                ftech_emp_info();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch (Exception ex)
            {
            }
        }


        private void ftech_emp_info()
        {
            compLN comP = new compLN();
            mycode.bind_ddl(ddl_emp_type, "select Emp_type from HR_Emp_type order by Emp_type");
            DataTable dt = My.dataTable("select * from Id_card_update_employee where Employee_code='" + hd_emp_code.Value + "'");
            if (dt.Rows.Count > 0)
            {
                txt_emp_code.Text = dt.Rows[0]["Employee_code"].ToString();
                txt_id_card_no.Text = dt.Rows[0]["Id_card_no"].ToString();
                txt_emp_name.Text = dt.Rows[0]["Name"].ToString();
                txt_father_name.Text = dt.Rows[0]["Father_name"].ToString();
                ddl_emp_type.Text = dt.Rows[0]["Department_name"].ToString();
                txt_aadhar_no.Text = dt.Rows[0]["Aadhar_no"].ToString();
                txt_doj.Text = dt.Rows[0]["Date_of_joining"].ToString();
                txt_dob.Text = dt.Rows[0]["Date_of_birth"].ToString();
                ddl_blood_group.Text = dt.Rows[0]["Blood_group"].ToString();
                txt_mobile_no.Text = dt.Rows[0]["Mobile_no"].ToString();
                txt_email_id.Text = dt.Rows[0]["Email_id"].ToString();
                txt_address.Text = dt.Rows[0]["Address"].ToString();
                ViewState["Employee_image"] = dt.Rows[0]["Photo_User"].ToString();
                if (ViewState["Employee_image"].ToString() == "")
                {
                    img_student_image.Visible = false;

                }
                else
                {
                    img_student_image.Visible = true;
                    img_student_image.ImageUrl = ViewState["Employee_image"].ToString();
                }

            }
            else
            {
                DataTable dtStd = My.dataTable("select * from PRL_Employee_Master where Emp_Code='" + hd_emp_code.Value + "'");
                if (dtStd.Rows.Count > 0)
                {
                    txt_emp_code.Text = dtStd.Rows[0]["Emp_Code"].ToString();
                    txt_emp_name.Text = dtStd.Rows[0]["Employee_Name"].ToString();
                    txt_father_name.Text = dtStd.Rows[0]["Father_Name"].ToString();

                    try
                    {
                        ddl_emp_type.Text = dtStd.Rows[0]["employee_type"].ToString();
                    }
                    catch (Exception ex)
                    {
                    }

                    txt_aadhar_no.Text = "";

                    try
                    {
                        txt_doj.Text = My.toDateTime(dtStd.Rows[0]["Date_of_Joining"].ToString() + " " + mycode.time()).ToString("dd/MM/yyyy");

                    }
                    catch
                    {

                    }
                    try
                    {
                        txt_dob.Text = My.toDateTime(dtStd.Rows[0]["Date_of_birth"].ToString() + " " + mycode.time()).ToString("dd/MM/yyyy");

                    }
                    catch
                    {

                    }




                    try
                    {
                        ddl_blood_group.Text = dtStd.Rows[0]["Blood_group"].ToString();
                    }
                    catch (Exception ex)
                    {
                    }


                    txt_mobile_no.Text = dtStd.Rows[0]["Mobile"].ToString();
                    txt_email_id.Text = dtStd.Rows[0]["Email"].ToString();
                    txt_address.Text = dtStd.Rows[0]["Address"].ToString();

                    ViewState["Employee_image"] = dtStd.Rows[0]["Employee_image"].ToString();
                    if (ViewState["Employee_image"].ToString() == "")
                    {
                        img_student_image.Visible = false;

                    }
                    else
                    {
                        img_student_image.ImageUrl = ViewState["Employee_image"].ToString();
                        img_student_image.Visible = true;

                    }
                }
            }
        }

        protected void btn_update_emp_info_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_emp_name.Text == "")
                {
                    Alertme("Please enter name.", "warning");
                    txt_emp_name.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                else if (txt_id_card_no.Text == "")
                {
                    Alertme("Please enter id card no.", "warning");
                    txt_id_card_no.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                else if (ddl_emp_type.Text == "")
                {
                    Alertme("Please select department.", "warning");
                    ddl_emp_type.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                else
                {

                    string filepath1 = "";
                    if (FileUpload1.HasFile)
                    {
                        if (FileUpload1.FileBytes.Length < 600000)
                        {
                            filepath1 = upload_image(FileUpload1, "user_img");
                            if (filepath1 == "")
                            {
                                Alertme("Please upload valid passport size image.", "warning");
                                FileUpload1.Focus();
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                                return;

                            }
                            else
                            {
                                ViewState["Employee_image"] = filepath1;
                                update_emp_info();
                                Alertme("Record has been updated successfully", "success");

                            }

                        }
                        else
                        {
                            Alertme("Please Reduce or compress size of passport image size max(200kb)", "warning");
                            FileUpload1.Focus();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                        }
                    }
                    else
                    {

                        update_emp_info();
                        Alertme("Record has been updated successfully", "success");
                    }



                }
            }
            catch (Exception ex)
            {
            }
        }

        private string upload_image(FileUpload Files, string name)
        {
            string dbfilePath = "";
            DateTime dt = DateTime.UtcNow.AddHours(5).AddMinutes(30);
            string date = dt.ToString("dd_MM_yyyy");
            string time = dt.ToString("hh_mm_ss");
            String filerename = name + date + time;
            Boolean FileOK = false;
            Boolean FileSaved = false;
            int k = 0;
            Session["WorkingImage"] = Files.FileName;
            string FileExtension = Path.GetExtension(Session["WorkingImage"].ToString()).ToLower();
            Session["renamedfile"] = filerename + "PIMG1" + FileExtension;
            string[] allowedExtension = { ".png", ".jpeg", ".jpg", ".JPEG" };
            for (int i = 0; i < allowedExtension.Length; i++)
            {
                k++;
                if (FileExtension == allowedExtension[i])
                {
                    FileOK = true;

                    break;
                }
            }


            if (FileOK)
            {
                try
                {
                    string path = (Server.MapPath("../Master_Img/Apply_career")).ToString();
                    Files.SaveAs(path + "/" + Session["renamedfile"]);
                    FileSaved = true;
                }
                catch (Exception ex)
                {
                    FileSaved = false;
                    Alertme("File has not save.", "success");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
            }
            else
            {

            }
            if (FileSaved)
            {
                string originalPath2 = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.AbsolutePath, ""); string[] New_originalPath1 = originalPath2.Split('?'); string originalPath1 = New_originalPath1[0].ToString();
                string fileName = Path.GetFileName(Session["renamedfile"].ToString());
                dbfilePath = originalPath1 + "/Master_Img/Apply_career/" + fileName;
            }
            return dbfilePath;
        }

        private void update_emp_info()
        {
            DataTable dt = My.dataTable("select Id from Id_card_update_employee where Employee_code='" + hd_emp_code.Value + "' order by id desc");
            if (dt.Rows.Count > 0)
            {
                SqlCommand cmd;
                string query = "update Id_card_update_employee set Id_card_no=@Id_card_no,Name=@Name,Father_name=@Father_name,Department_name=@Department_name,Aadhar_no=@Aadhar_no,Date_of_joining=@Date_of_joining,Date_of_birth=@Date_of_birth,Blood_group=@Blood_group,Mobile_no=@Mobile_no,Email_id=@Email_id,Address=@Address,Updated_by=@Updated_by,Updated_date=@Updated_date,Updated_time=@Updated_time,Photo_User=@Photo_User where Id=@Id";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Id", dt.Rows[0]["Id"].ToString());
                cmd.Parameters.AddWithValue("@Id_card_no", txt_id_card_no.Text);
                cmd.Parameters.AddWithValue("@Name", txt_emp_name.Text);
                cmd.Parameters.AddWithValue("@Father_name", txt_father_name.Text);
                cmd.Parameters.AddWithValue("@Department_name", ddl_emp_type.Text);
                cmd.Parameters.AddWithValue("@Aadhar_no", txt_aadhar_no.Text);
                cmd.Parameters.AddWithValue("@Date_of_joining", txt_doj.Text);
                cmd.Parameters.AddWithValue("@Date_of_birth", txt_dob.Text);
                cmd.Parameters.AddWithValue("@Blood_group", ddl_blood_group.Text);
                cmd.Parameters.AddWithValue("@Mobile_no", txt_mobile_no.Text);
                cmd.Parameters.AddWithValue("@Email_id", txt_email_id.Text);
                cmd.Parameters.AddWithValue("@Address", txt_address.Text);
                cmd.Parameters.AddWithValue("@Updated_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Updated_date", mycode.date());
                cmd.Parameters.AddWithValue("@Updated_time", mycode.time());
                cmd.Parameters.AddWithValue("@Photo_User", ViewState["Employee_image"].ToString());
                if (My.InsertUpdateData(cmd))
                {

                    update_data_hr_and_prl_data();


                }
            }
            else
            {
                SqlCommand cmd;
                string query = "INSERT INTO Id_card_update_employee (Employee_code,Id_card_no,Name,Father_name,Department_name,Aadhar_no,Date_of_joining,Date_of_birth,Blood_group,Mobile_no,Email_id,Address,Updated_by,Updated_date,Updated_time,Photo_User) values (@Employee_code,@Id_card_no,@Name,@Father_name,@Department_name,@Aadhar_no,@Date_of_joining,@Date_of_birth,@Blood_group,@Mobile_no,@Email_id,@Address,@Updated_by,@Updated_date,@Updated_time,@Photo_User)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Employee_code", hd_emp_code.Value);
                cmd.Parameters.AddWithValue("@Id_card_no", txt_id_card_no.Text);
                cmd.Parameters.AddWithValue("@Name", txt_emp_name.Text);
                cmd.Parameters.AddWithValue("@Father_name", txt_father_name.Text);
                cmd.Parameters.AddWithValue("@Department_name", ddl_emp_type.Text);
                cmd.Parameters.AddWithValue("@Aadhar_no", txt_aadhar_no.Text);
                cmd.Parameters.AddWithValue("@Date_of_joining", txt_doj.Text);
                cmd.Parameters.AddWithValue("@Date_of_birth", txt_dob.Text);
                cmd.Parameters.AddWithValue("@Blood_group", ddl_blood_group.Text);
                cmd.Parameters.AddWithValue("@Mobile_no", txt_mobile_no.Text);
                cmd.Parameters.AddWithValue("@Email_id", txt_email_id.Text);
                cmd.Parameters.AddWithValue("@Address", txt_address.Text);
                cmd.Parameters.AddWithValue("@Updated_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Updated_date", mycode.date());
                cmd.Parameters.AddWithValue("@Updated_time", mycode.time());
                cmd.Parameters.AddWithValue("@Photo_User", ViewState["Employee_image"].ToString());
                if (My.InsertUpdateData(cmd))
                {
                    update_data_hr_and_prl_data();
                }
            }
        }






        #region grant permission
        protected void btn_inactive_Click(object sender, EventArgs e)
        {
            int i = 0;
            foreach (RepeaterItem row in rd_view.Items)
            {
                CheckBox chk = rd_view.Items[i].FindControl("chkRowData") as CheckBox;
                if (chk != null && chk.Checked)
                {
                    Label lbl_employee_id = rd_view.Items[i].FindControl("lbl_employee_id") as Label;


                    SqlCommand cmd = new SqlCommand("Update PRL_Employee_Master set Is_submit=1  where Emp_Code ='" + lbl_employee_id.Text + "'  ");
                    InsertUpdate.InsertUpdateData(cmd);



                }
                i++;
            }
            Alertme("Permission has been removed", "success");
            bind_grd_view();
        }


        protected void btn_active_Click(object sender, EventArgs e)
        {
            int i = 0;
            foreach (RepeaterItem row in rd_view.Items)
            {
                CheckBox chk = rd_view.Items[i].FindControl("chkRowData") as CheckBox;
                if (chk != null && chk.Checked)
                {
                    Label lbl_employee_id = rd_view.Items[i].FindControl("lbl_employee_id") as Label;



                    SqlCommand cmd = new SqlCommand("Update PRL_Employee_Master set Is_submit=0  where Emp_Code ='" + lbl_employee_id.Text + "'  ");
                    InsertUpdate.InsertUpdateData(cmd);



                }
                i++;
            }

            Alertme("Permission has been granted", "success");
            bind_grd_view();
        }
        #endregion

        private void update_data_hr_and_prl_data()
        {
            PayrollMy.Update("user_details", new
            {
                mobile = txt_mobile_no.Text,
                ProfilePhoto = ViewState["Employee_image"].ToString(),

                email = txt_email_id.Text,
            }, $"user_id='{hd_emp_code.Value}'");


            PayrollMy.Update("PRL_Employee_Master", new
            {
                Gender = ddl_blood_group.Text,
                Date_of_birth = txt_dob.Text,
                Blood_group = ddl_blood_group.Text,
                Father_Name = txt_father_name.Text,
                Address = txt_address.Text,
                Email = txt_email_id.Text,
                Mobile = txt_mobile_no.Text,
                Employee_image = ViewState["Employee_image"].ToString(),
                Date_of_Joining = txt_doj.Text,
                iDOB = Convert.ToDateTime(txt_dob.Text).ToString("yyyyMMdd"),
                Is_submit = 1,
            }, $"Emp_Code='{hd_emp_code.Value}'");

            PayrollMy.Update("HR_Employee_Master", new
            {
                Gender = ddl_blood_group.Text,
                Date_of_birth = My.toDateTime(txt_dob.Text + " " + mycode.time()).ToString("dd/MMM/yyyy hh:mm:ss tt"),
                Blood_group = ddl_blood_group.Text,
                Father_Name = txt_father_name.Text,
                Address = txt_address.Text,
                Email = txt_email_id.Text,
                Mobile = txt_mobile_no.Text,
                Employee_image = ViewState["Employee_image"].ToString(),
                Date_of_Joining = My.toDateTime(txt_doj.Text + " " + mycode.time()).ToString("dd-MMM-yyyy"),
            }, $"Emp_Code='{hd_emp_code.Value}'");

            PayrollMy.Update("HR_Employee_Master", new
            {
                Gender = ddl_blood_group.Text,
                Date_of_birth = My.toDateTime(txt_dob.Text + " " + mycode.time()).ToString("dd/MMM/yyyy hh:mm:ss tt"),
                Blood_group = ddl_blood_group.Text,
                Father_Name = txt_father_name.Text,
                Address = txt_address.Text,
                Email = txt_email_id.Text,
                Mobile = txt_mobile_no.Text,
                Employee_image = ViewState["Employee_image"].ToString(),
                Date_of_Joining = My.toDateTime(txt_doj.Text + " " + mycode.time()).ToString("dd-MMM-yyyy"),
            }, $"Emp_Code='{hd_emp_code.Value}'");
            PayrollMy.Update("HR_Employee_Online_Apply", new
            {
                Emailid = txt_email_id.Text,
                Date_birthday = My.toDateTime(txt_dob.Text + " " + mycode.time()).ToString("dd/MMM/yyyy hh:mm:ss tt"),
                Gender = ddl_blood_group.Text,
                mobile_no_CA = txt_mobile_no.Text,
                address_pa = txt_address.Text,
                fathername = txt_father_name.Text,
                passport_photo = ViewState["Employee_image"].ToString(),

            }, $"Emp_code='{hd_emp_code.Value}'");
        }


        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = mycode.FillData("select Employee_code,Id_card_no,Name,Gender,Father_name,Department_name,Aadhar_no,Date_of_joining,Date_of_birth,Blood_group,Mobile_no,Email_id,Address from ID_CARD_UPDATE_EMPLOYEE order by Name asc");
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DateTime dTimet = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                        string date = dTimet.ToString("dd_MM_yyyy");
                        string time = dTimet.ToString("hh_mm_ss");
                        String filerename = "Employee_list-" + date + time;
                        string attachment = "attachment; filename=" + filerename + ".csv";
                        Response.ClearContent();
                        Response.AddHeader("content-disposition", attachment);
                        Response.ContentType = "text/csv";
                        var csvContent = My.DataTableToCsv(dt);
                        Response.Write(csvContent);
                        Response.End();
                    }
                }
                else
                {
                    Alertme("Data not found.", "warning");
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}