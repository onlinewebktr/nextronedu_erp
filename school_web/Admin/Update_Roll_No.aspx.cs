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
    public partial class Update_Roll_No : System.Web.UI.Page
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
                        ViewState["flagPosition"] = "1";
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();

                        mycode.bind_all_ddl_with_id(ddlsession, "Select  Session,session_id from session_details order by cast((Substring (Session,1,4)) as int) desc ");
                        ddlsession.SelectedValue = My.get_session_id();

                        mycode.bind_all_ddl_with_id(ddlclass, "Select Course_Name,course_id from Add_course_table order by Position");
                        ddlclass.SelectedValue = My.get_top_one_class();
                        fetch_section();

                        string pagename_current = Path.GetFileName(Request.Path);
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
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
        protected void btn_fnd_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session.", "warning");
                    ddlsession.Focus();
                }
                else if (ddlclass.SelectedItem.Text == "Select")
                {
                    Alertme("Please select class.", "warning");
                    ddlclass.Focus();
                }
                else if (ddl_secion.Text == "Select")
                {
                    Alertme("Please select section.", "warning");
                    ddlclass.Focus();
                }
                else
                {
                    if (ViewState["Is_add"].ToString() == "1")
                    {
                        find_by_c_s_a();
                    }
                    else if (ViewState["Is_Edit"].ToString() == "1")
                    {
                        find_by_c_s_a();
                    }
                    else
                    {
                        Alertme(My.get_restricted_message(), "warning");
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_by_c_s_a()
        {
            bind_grd_view("select * from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_secion.Text + "'    and (Transfer_Status='New' or Transfer_Status='NT') and  Status='1' and StudentStatus='AV' order by rollnumber asc");
        }

        private void bind_grd_view(string query)
        {
            ViewState["flag"] = query;
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                btn_save.Visible = false;
                Alertme("Sorry there are no data list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();

            }
            else
            {
                btn_save.Visible = true;
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }
        protected void btn_save_Click(object sender, EventArgs e)
        {
            ViewState["msg"] = "0";
            int growcountS = rd_view.Items.Count;
            for (int i = 0; i < growcountS; i++)
            {
                Label lbl_admission_no = (Label)rd_view.Items[i].FindControl("lbl_admission_no");
                Label lbl_Session_id = (Label)rd_view.Items[i].FindControl("lbl_Session_id");
                Label lbl_Class_id = (Label)rd_view.Items[i].FindControl("lbl_Class_id");
                Label lbl_Section = (Label)rd_view.Items[i].FindControl("lbl_Section");
                TextBox txt_roll_no = (TextBox)rd_view.Items[i].FindControl("txt_roll_no");
                TextBox txt_section = (TextBox)rd_view.Items[i].FindControl("txt_section");
                TextBox txt_date_of_birth = (TextBox)rd_view.Items[i].FindControl("txt_date_of_birth");
                TextBox txt_Father_whatsApp_no = (TextBox)rd_view.Items[i].FindControl("txt_Father_whatsApp_no");
                TextBox txt_Mother_whatsApp_no = (TextBox)rd_view.Items[i].FindControl("txt_Mother_whatsApp_no");



                TextBox txt_pen_no = (TextBox)rd_view.Items[i].FindControl("txt_pen_no");
                DropDownList ddl_gender = (DropDownList)rd_view.Items[i].FindControl("ddl_gender");
                DropDownList ddl_blood_group = (DropDownList)rd_view.Items[i].FindControl("ddl_blood_group");
                DropDownList ddl_religion = (DropDownList)rd_view.Items[i].FindControl("ddl_religion");
                DropDownList ddl_cast = (DropDownList)rd_view.Items[i].FindControl("ddl_cast");

                TextBox txt_aadharno = (TextBox)rd_view.Items[i].FindControl("txt_aadharno");
                TextBox txt_father_mobile = (TextBox)rd_view.Items[i].FindControl("txt_father_mobile");
                TextBox txt_mother_mobile_no = (TextBox)rd_view.Items[i].FindControl("txt_mother_mobile_no");
                TextBox txt_date_of_admission = (TextBox)rd_view.Items[i].FindControl("txt_date_of_admission");
                TextBox txt_father_email_id = (TextBox)rd_view.Items[i].FindControl("txt_father_email_id");

                TextBox txt_height = (TextBox)rd_view.Items[i].FindControl("txt_height");
                TextBox txt_weight = (TextBox)rd_view.Items[i].FindControl("txt_weight");


                TextBox txt_student_name = (TextBox)rd_view.Items[i].FindControl("txt_student_name");
                TextBox txt_father_name = (TextBox)rd_view.Items[i].FindControl("txt_father_name");
                TextBox txt_mother_name = (TextBox)rd_view.Items[i].FindControl("txt_mother_name");
                TextBox txt_adm_no_reg_no = (TextBox)rd_view.Items[i].FindControl("txt_adm_no_reg_no");


                int admission_idate = 0;
                try
                {
                    admission_idate = My.DateConvertToIdate(txt_date_of_admission.Text);
                }
                catch (Exception ex)
                {
                }

                // studentname,fathername,mothername,Admission_no_date

                SqlCommand cmd;
                string query = "update admission_registor set studentname=@studentname,fathername=@fathername,mothername=@mothername,Admission_no_date=@Admission_no_date,rollnumber=@rollnumber,Section=@Section,Father_whatsApp_no=@Father_whatsApp_no,Mother_whatsApp_no=@Mother_whatsApp_no,dob=@dob,Student_pen_no=@Student_pen_no,gender=@gender,blood_group=@blood_group,religion=@religion,cast=@cast,aadharno=@aadharno,father_mob=@father_mob,mother_mob=@mother_mob,dateofadmission=@dateofadmission,admission_idate=@admission_idate,email_id=@email_id,Height=@Height,Weight=@Weight where admissionserialnumber=@admissionserialnumber and Class_id=@Class_id  and Session_id=@Session_id";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@rollnumber", txt_roll_no.Text);
                cmd.Parameters.AddWithValue("@Section", txt_section.Text);
                cmd.Parameters.AddWithValue("@Father_whatsApp_no", txt_Father_whatsApp_no.Text);
                cmd.Parameters.AddWithValue("@Mother_whatsApp_no", txt_Mother_whatsApp_no.Text);
                cmd.Parameters.AddWithValue("@dob", txt_date_of_birth.Text);
                cmd.Parameters.AddWithValue("@Student_pen_no", txt_pen_no.Text);
                cmd.Parameters.AddWithValue("@gender", ddl_gender.Text);
                cmd.Parameters.AddWithValue("@blood_group", ddl_blood_group.Text);
                cmd.Parameters.AddWithValue("@religion", ddl_religion.Text);
                cmd.Parameters.AddWithValue("@cast", ddl_cast.Text);
                cmd.Parameters.AddWithValue("@aadharno", txt_aadharno.Text);
                cmd.Parameters.AddWithValue("@father_mob", txt_father_mobile.Text);
                cmd.Parameters.AddWithValue("@mother_mob", txt_mother_mobile_no.Text);
                cmd.Parameters.AddWithValue("@dateofadmission", txt_date_of_admission.Text);
                cmd.Parameters.AddWithValue("@admission_idate", admission_idate);
                cmd.Parameters.AddWithValue("@email_id", txt_father_email_id.Text);
                cmd.Parameters.AddWithValue("@Height", txt_height.Text);
                cmd.Parameters.AddWithValue("@Weight", txt_weight.Text);
                cmd.Parameters.AddWithValue("@admissionserialnumber", lbl_admission_no.Text);
                cmd.Parameters.AddWithValue("@Class_id", lbl_Class_id.Text);
                cmd.Parameters.AddWithValue("@Session_id", lbl_Session_id.Text);

                cmd.Parameters.AddWithValue("@studentname", txt_student_name.Text);
                cmd.Parameters.AddWithValue("@fathername", txt_father_name.Text);
                cmd.Parameters.AddWithValue("@mothername", txt_mother_name.Text);
                cmd.Parameters.AddWithValue("@Admission_no_date", txt_adm_no_reg_no.Text);
                if (My.InsertUpdateData(cmd))
                {
                }
                ViewState["msg"] = "1";
            } 


            if (ViewState["msg"].ToString() == "1")
            {
                Alertme("Student details has been updated sucessfully.", "success");
                // bind_grd_view(ViewState["flag"].ToString());
            }
            //}
        }

        protected void btn_find_admission_no_Click(object sender, EventArgs e)
        {
            if (txt_admission_no.Text == "")
            {
                Alertme("Please enter admission no.", "warning");
                txt_admission_no.Focus();
            }
            else
            {
                if (ViewState["Is_add"].ToString() == "1")
                {
                    find_by_admission_no();
                }
                else if (ViewState["Is_Edit"].ToString() == "1")
                {
                    find_by_admission_no();
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }

        }

        private void find_by_admission_no()
        {
            bind_grd_view("select top 1 * from admission_registor where admissionserialnumber='" + txt_admission_no.Text + "'   and (Transfer_Status='New' or Transfer_Status='NT') and  Status='1' and StudentStatus='AV' and Session_id='" + My.get_session_id() + "' order by id desc");
        }

        protected void lnk_position_Click(object sender, EventArgs e)
        {
            try
            {
                featch_by_acs_desc();
            }
            catch (Exception exc)
            {
            }
        }

        private void featch_by_acs_desc()
        {
            //if (ViewState["flag"].ToString() == "1")
            //{
            if (ViewState["flagPosition"].ToString() == "1")
            {
                string query = "Select * from admission_registor where session='" + ddlsession.SelectedItem.Text + "' and Section='" + ddl_secion.Text + "' and Class_id=" + ddlclass.SelectedValue + "   and (Transfer_Status='New' or Transfer_Status='NT') and  Status='1'  order by studentname asc";
                bind_grd_view(query);
                ViewState["flagPosition"] = "0";
            }
            else
            {
                string query = "Select * from admission_registor where session='" + ddlsession.SelectedItem.Text + "' and Section='" + ddl_secion.Text + "' and Class_id=" + ddlclass.SelectedValue + "   and (Transfer_Status='New' or Transfer_Status='NT') and Status='1'  order by studentname desc";
                bind_grd_view(query);
                ViewState["flagPosition"] = "1";
            }
            //}

        }

        protected void ddlclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                fetch_section();
            }
            catch (Exception ex)
            {
            }
        }

        private void fetch_section()
        {
            mycode.bind_ddl(ddl_secion, "Select distinct Section from admission_registor where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'  and Status='1' order by Section");
        }


        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    //========================

                    DropDownList ddl_gender = (DropDownList)e.Item.FindControl("ddl_gender");
                    DropDownList ddl_blood_group = (DropDownList)e.Item.FindControl("ddl_blood_group");
                    DropDownList ddl_religion = (DropDownList)e.Item.FindControl("ddl_religion");
                    DropDownList ddl_cast = (DropDownList)e.Item.FindControl("ddl_cast");


                    Label lbl_gender = (Label)e.Item.FindControl("lbl_gender");
                    Label lbl_blood_group = (Label)e.Item.FindControl("lbl_blood_group");
                    Label lbl_religion = (Label)e.Item.FindControl("lbl_religion");
                    Label lbl_caste_category = (Label)e.Item.FindControl("lbl_caste_category");
                    try
                    {
                        if (lbl_gender.Text.ToUpper() == "MALE")
                        {
                            ddl_gender.Text = "MALE";
                        }
                        else if (lbl_gender.Text.ToUpper() == "FEMALE")
                        {
                            ddl_gender.Text = "FEMALE";
                        }
                        else
                        {
                            ddl_gender.Text = lbl_gender.Text;
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                    try
                    {
                        ddl_blood_group.Text = lbl_blood_group.Text;
                    }
                    catch (Exception ex)
                    {
                    }
                    try
                    {
                        ddl_religion.Text = lbl_religion.Text.ToUpper();
                    }
                    catch (Exception ex)
                    {
                    }
                    try
                    {
                        ddl_cast.Text = lbl_caste_category.Text.ToUpper();
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            catch { }
        }
    }
}