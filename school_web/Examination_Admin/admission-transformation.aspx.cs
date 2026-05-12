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
    public partial class admission_transformation : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
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
                        ViewState["firm_id"] = Session["firm"].ToString();
                        ViewState["flagPosition"] = "1";
                        txt_date.Text = mycode.date();
                        ViewState["flag"] = "0";
                        mycode.bind_all_ddl_with_id(ddl_session, " Select Session,session_id from session_details order by Session ");
                        mycode.bind_all_ddl_with_id(ddl_session_adm, "Select Session,session_id from session_details order by Session ");

                        mycode.bind_all_ddl_with_id(ddl_course, "Select Course_Name,course_id from Add_course_table order by Position asc");

                        mycode.bind_ddl(ddl_section, "Select distinct Section from admission_registor ");

                        //mycode.bind_all_ddl_with_id(ddl_session_tranfser2, "Select Session,session_id from session_details where Use_mode='1' order by Session");
                        mycode.bind_all_ddl_with_id(ddl_course_transfer2, "Select Course_Name,course_id from Add_course_table order by Position asc");
                        mycode.bind_ddl(ddl_section_transfer_2, "Select distinct Section from admission_registor");
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Fee_Type_Master");
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
        protected void ddl_course_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_course.SelectedItem.Text == "Select")
            {
                Alertme("Please select class.", "warning");
            }
            else
            {

            }
        }


        protected void ddl_section_SelectedIndexChanged(object sender, EventArgs e)
        {
            mycode.bind_all_ddl_with_id(ddl_session_tranfser2, "Select Session,session_id from session_details where session_id!=" + ddl_session.SelectedValue + " order by Session");
            find_data();
            ViewState["flag"] = "1";
        }

        private void find_data()
        {
            if (ddl_session.SelectedItem.Text == "Select")
            {
                Alertme("Please select session ", "warning");
            }
            else if (ddl_course.SelectedItem.Text == "Select")
            {
                Alertme("Please select course name", "warning");
            }

            else if (ddl_section.Text == "Select")
            {
                Alertme("Please select section", "warning");
            }
            else
            {
                string query = "Select * from admission_registor where session='" + ddl_session.SelectedItem.Text + "' and Section='" + ddl_section.Text + "' and Class_id=" + ddl_course.SelectedValue + "   and (Transfer_Status='New' or Transfer_Status='NT') and Status='1'  order by rollnumber asc";
                bind_grids(query);
            }
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        { 
            if (ddl_session_tranfser2.SelectedItem.Text == "Select")
            {
                Alertme("Please select session ", "warning");
            }
            else if (ddl_course_transfer2.SelectedItem.Text == "Select")
            {
                Alertme("Please select course name", "warning");
            }

            else if (ddl_section_transfer_2.Text == "Select")
            {
                Alertme("Please select section", "warning");
            }
            else if (ddl_session_tranfser2.SelectedValue == ddl_course_transfer2.SelectedItem.Text)
            {
                Alertme("Sorry! Your old session and current session is match. ", "warning");
            }
            else if (txt_date.Text == "")
            {
                Alertme("Please select admission date", "warning");
            }
            else
            {
                int grdcount = grd_studentdetails.Rows.Count;
                int j = 0;
                for (int i = 0; i < grdcount; i++)
                {
                    Label lbl_admission_no = (Label)grd_studentdetails.Rows[i].FindControl("lbl_admission_no");
                    Label lbl_status = (Label)grd_studentdetails.Rows[i].FindControl("lbl_status");
                    Label lbl_id = (Label)grd_studentdetails.Rows[i].FindControl("lbl_id");

                    CheckBox check = (CheckBox)grd_studentdetails.Rows[i].FindControl("rowChkBox");

                    if (check.Checked == true)
                    {

                        submit_lblmessage(lbl_admission_no.Text, lbl_status.Text, lbl_id.Text);
                    }
                    else
                    {
                        j++;
                    }
                }

                if (j == grdcount)
                {
                    Alertme("Please check any one checkbox of admission list list", "warning");
                    scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
                }
                else
                {
                    Alertme("Student has been sucessfully transferd", "success");
                    scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
                    find_data();
                }
            }
        }

        private void submit_lblmessage(string admission_no, string status, string id)
        {
            bool is_transfer_dues = true;
            double dues = 0;
            //and  payment_status!='Unpaid'========================******************
            string query = "Select * from admission_registor where session='" + ddl_session.SelectedItem.Text + "' and Section='" + ddl_section.Text + "' and Class_id=" + ddl_course.SelectedValue + "  and (Transfer_Status='New' or Transfer_Status='NT') and admissionserialnumber='" + admission_no + "'  ";
            SqlDataAdapter ad = new SqlDataAdapter(query, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "admission_registor");
            DataTable dt = ds.Tables[0];
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
            }
            else
            {
                if (is_transfer_dues)
                {
                    DataTable dues_dt = My.dataTableSP("sp_find_all_dues", ddl_course.SelectedValue, ddl_session.SelectedValue, admission_no);
                    if (dues_dt.Rows.Count > 0)
                    {

                        try
                        {
                            dues = My.toDouble(dues_dt.Rows[0]["total"].ToString());
                        }
                        catch
                        {
                            dues = 0;
                        }
                    }
                    else
                    {
                        dues = 0;
                    }
                    if (dues > 0)
                    {
                        mycode.executequery("insert into Previous_Year_Dues(Session,AdmissionNumber,Name,Dues_Amount,Status,Session_id,Class_id,Old_class_id,Old_session_id) values ('" + ddl_session_tranfser2.SelectedItem.Text + "','" + dt.Rows[0]["admissionserialnumber"].ToString() + "','" + dt.Rows[0]["studentname"].ToString() + "','" + dues.ToString("0.00") + "','Unpaid','" + ddl_session_tranfser2.SelectedValue + "','" + ddl_course_transfer2.SelectedValue + "','" + ddl_course.SelectedValue + "','" + ddl_session.SelectedValue + "'); update Previous_Year_Dues set Status='Paid' where AdmissionNumber='" + dt.Rows[0]["admissionserialnumber"].ToString() + "' and Class_id='" + ddl_course.SelectedValue + "' and Session_id='" + ddl_session.SelectedValue + "' and (Status='Dues' or Status='Unpaid')");

                    }
                }
                DataRow dr = dt.NewRow();
                dr[0] = ddl_course_transfer2.SelectedItem.Text;
                dr[1] = dt.Rows[0][1].ToString();
                dr[2] = dt.Rows[0][2].ToString();
                dr[3] = dt.Rows[0][3].ToString(); ;
                dr[4] = ddl_section_transfer_2.Text;
                dr[5] = ddl_session_tranfser2.SelectedItem.Text;
                dr[6] = txt_date.Text;// session date current date
                dr[7] = dt.Rows[0][7].ToString();
                dr[10] = dt.Rows[0][10].ToString();
                dr[11] = dt.Rows[0][11].ToString();
                dr[12] = dt.Rows[0][12].ToString();
                dr[13] = dt.Rows[0][13].ToString();
                dr[14] = dt.Rows[0][14].ToString();
                dr[15] = dt.Rows[0][15].ToString();
                dr[16] = dt.Rows[0][16].ToString();
                dr[17] = dt.Rows[0][17].ToString();
                dr[18] = dt.Rows[0][18].ToString();
                dr[19] = dt.Rows[0][19].ToString();
                dr[20] = dt.Rows[0][20].ToString();
                dr[21] = dt.Rows[0][21].ToString();
                dr[22] = dt.Rows[0][22].ToString();
                dr[23] = dt.Rows[0][23].ToString();
                dr[24] = dt.Rows[0][24].ToString();
                dr[25] = dt.Rows[0][25].ToString();
                dr[26] = dt.Rows[0][26].ToString();
                dr[27] = dt.Rows[0][27].ToString();
                dr[28] = dt.Rows[0][28].ToString();
                dr[29] = dt.Rows[0][29].ToString();
                dr[30] = dt.Rows[0][30].ToString();
                dr[31] = dt.Rows[0][31].ToString();
                dr[32] = dt.Rows[0][32].ToString();
                dr[33] = dt.Rows[0][33].ToString();
                dr[34] = dt.Rows[0][34].ToString();
                dr[35] = dt.Rows[0][35].ToString();
                dr[36] = dt.Rows[0][36].ToString();
                dr[37] = dt.Rows[0][37].ToString();
                dr[38] = dt.Rows[0][38].ToString();
                dr[43] = "Unpaid";


                dr[47] = "NT";
                dr[48] = dt.Rows[0][48].ToString();
                dr[49] = dt.Rows[0][49].ToString();
                dr[50] = dt.Rows[0][50].ToString();
                dr[52] = "AV";
                dr[53] = dt.Rows[0][53].ToString(); ;

                try
                {
                    dr[55] = Convert.ToDateTime(txt_date.Text).ToString("yyyyMMdd");
                }
                catch
                {
                }
                dr["staff_ward"] = dt.Rows[0]["staff_ward"].ToString();
                dr["jati"] = dt.Rows[0]["jati"].ToString();
                dr["mob2"] = dt.Rows[0]["mob2"].ToString();
                dr["Hostel_id"] = My.toDouble(dt.Rows[0]["Hostel_id"].ToString());
                dr["Session_id"] = ddl_session_tranfser2.SelectedValue;
                dr["Class_id"] = ddl_course_transfer2.SelectedValue;
                dr["Is_TC_Taken"] = false;
                dr["Student_id"] = dt.Rows[0]["Student_id"].ToString();
                dr["Academic_Sem_or_Year"] = "";
                dr["Academic_Sem_or_Year_id"] = "0";
                dr["Category_id"] = dt.Rows[0]["Category_id"].ToString();
                dr["SubCategory_id"] = dt.Rows[0]["SubCategory_id"].ToString();
                dr["Branch_id"] = dt.Rows[0]["Branch_id"].ToString();

                dr["studentimagepath"] = dt.Rows[0]["studentimagepath"].ToString();

                if (dt.Rows[0]["is_applied_dayboarding"].ToString() == "")
                {
                    dr["is_applied_dayboarding"] = 0;
                }
                else
                {
                    dr["is_applied_dayboarding"] = 1;
                }
                if (dt.Rows[0]["day_boarding_with_lunch"].ToString() == "")
                {
                    dr["day_boarding_with_lunch"] = 0;
                }
                else
                {
                    dr["day_boarding_with_lunch"] = 1;
                }
                dr["email_id"] = dt.Rows[0]["email_id"].ToString();
                dr["birth_certificate_number"] = dt.Rows[0]["birth_certificate_number"].ToString();
                dr["place_of_birth"] = dt.Rows[0]["place_of_birth"].ToString();
                dr["blood_group"] = dt.Rows[0]["blood_group"].ToString();
                dr["cast_certificate_no"] = dt.Rows[0]["cast_certificate_no"].ToString();
                dr["student_mother_tounge"] = dt.Rows[0]["student_mother_tounge"].ToString();
                dr["is_illness"] = dt.Rows[0]["is_illness"].ToString();
                dr["f_nationality"] = dt.Rows[0]["f_nationality"].ToString();
                dr["f_marrital_statue"] = dt.Rows[0]["f_marrital_statue"].ToString();
                dr["m_marrital_statue"] = dt.Rows[0]["m_marrital_statue"].ToString();
                dr["m_nationality"] = dt.Rows[0]["m_nationality"].ToString();
                dr["m_occupation"] = dt.Rows[0]["m_occupation"].ToString();
                dr["ration_type"] = dt.Rows[0]["ration_type"].ToString();
                dr["illness_remark"] = dt.Rows[0]["illness_remark"].ToString();
                dr["father_mob"] = dt.Rows[0]["father_mob"].ToString();
                dr["mother_mob"] = dt.Rows[0]["mother_mob"].ToString();
                dr["mother_email"] = dt.Rows[0]["mother_email"].ToString();
                dr["Account_Holder_name"] = dt.Rows[0]["Account_Holder_name"].ToString();
                dr["Bnk_Name"] = dt.Rows[0]["Bnk_Name"].ToString();
                dr["IFSC_Code"] = dt.Rows[0]["IFSC_Code"].ToString();
                dr["Branch_Name"] = dt.Rows[0]["Branch_Name"].ToString();
                dr["lib_card_no"] = dt.Rows[0]["lib_card_no"].ToString();
                dr["Course_Type"] = dt.Rows[0]["Course_Type"].ToString();

                dr["CET_ROLL_NO"] = dt.Rows[0]["CET_ROLL_NO"].ToString();
                dr["CET_RANK"] = dt.Rows[0]["CET_RANK"].ToString();
                dr["CET_CATEGORY"] = dt.Rows[0]["CET_CATEGORY"].ToString();
                dr["MAKAUT_Student_ID"] = dt.Rows[0]["MAKAUT_Student_ID"].ToString();
                dr["MAKAUT_Student_password"] = dt.Rows[0]["MAKAUT_Student_password"].ToString();
                dr["Student_Whatsapp_No"] = dt.Rows[0]["Student_Whatsapp_No"].ToString();
                dr["Student_Other_Interest_Area"] = dt.Rows[0]["Student_Other_Interest_Area"].ToString();

                dr["Student_Name_First"] = dt.Rows[0]["Student_Name_First"].ToString();
                dr["Student_Middle_Name"] = dt.Rows[0]["Student_Middle_Name"].ToString();
                dr["Student_Name_Last"] = dt.Rows[0]["Student_Name_Last"].ToString();
                dr["Father_Name_First"] = dt.Rows[0]["Father_Name_First"].ToString();
                dr["Father_Name_Middle"] = dt.Rows[0]["Father_Name_Middle"].ToString();
                dr["Father_Name_Last"] = dt.Rows[0]["Father_Name_Last"].ToString();
                dr["Mother_Name_First"] = dt.Rows[0]["Mother_Name_First"].ToString();
                dr["Mother_Name_Middle"] = dt.Rows[0]["Mother_Name_Middle"].ToString();
                dr["Mother_Name_Last"] = dt.Rows[0]["Mother_Name_Last"].ToString();
                dr["Personal_Identymarks"] = dt.Rows[0]["Personal_Identymarks"].ToString();
                dr["Country_Code_Father"] = dt.Rows[0]["Country_Code_Father"].ToString();
                dr["Country_Code_Mother"] = dt.Rows[0]["Country_Code_Mother"].ToString();
                dr["Country_Code_Current_add"] = dt.Rows[0]["Country_Code_Current_add"].ToString();
                dr["Country_Code_Current_Perm_add"] = dt.Rows[0]["Country_Code_Current_Perm_add"].ToString();
                dr["gcm_id"] = dt.Rows[0]["gcm_id"].ToString();
                dr["Pwd"] = dt.Rows[0]["Pwd"].ToString();
                dr["Bank_acount_no"] = dt.Rows[0]["Bank_acount_no"].ToString();

                dr["Device_Id"] = dt.Rows[0]["Device_Id"].ToString();
                dr["Status"] = dt.Rows[0]["Status"].ToString();
                dr["Edit_Istatus"] = "0";
                dr["Old_Admission_Date"] = dt.Rows[0]["Old_Admission_Date"].ToString();
                dr["OLd_Admission_Idate"] = mycode.ConvertStringToiDate(dt.Rows[0]["OLd_Admission_Idate"].ToString());

                dr["Old_class_id"] = My.toInt(dt.Rows[0]["Old_class_id"].ToString());

                dr["UID_No"] = dt.Rows[0]["UID_No"].ToString();



                dt.Rows.Add(dr);
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(dt);
                update_transfer_status(admission_no);
            }

        }

        private void update_transfer_status(string admission_no)
        {
            SqlDataAdapter ad = new SqlDataAdapter("Select * from admission_registor where class='" + ddl_course.SelectedItem.Text + "' and session='" + ddl_session.SelectedItem.Text + "'  and admissionserialnumber='" + admission_no + "' and Section='" + ddl_section.Text + "' and Academic_Sem_or_Year_id='" + 0 + "'  ", My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "admission_registor");
            DataTable dt = ds.Tables[0];
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    dr[47] = "Transferred";


                    SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                    ad.Update(dt);
                    break;
                }
            }
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_session.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session.", "warning");
                }
                else if (ddl_course.SelectedItem.Text == "Select")
                {
                    Alertme("Please select class.", "warning");
                }
                else if (ddl_section.SelectedItem.Text == "Select")
                {
                    Alertme("Please select section.", "warning");
                }
                else
                {
                    mycode.bind_all_ddl_with_id(ddl_session_tranfser2, "Select Session,session_id from session_details where session_id!=" + ddl_session.SelectedValue + " order by Session");
                    find_data();
                    ViewState["flag"] = "1";
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void btn_find_by_adm_no_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_session_adm.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session.", "warning");
                }

                else if (txt_Adm_no.Text == "")
                {
                    Alertme("Please enter admission no.", "warning");
                }
                else
                {
                    mycode.bind_all_ddl_with_id(ddl_session_tranfser2, "Select Session,session_id from session_details where session_id!=" + ddl_session_adm.SelectedValue + " order by Session");
                    find_by_admission();
                    ViewState["flag"] = "2";
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_by_admission()
        {
            string query = "Select * from admission_registor where session='" + ddl_session_adm.SelectedItem.Text + "' and admissionserialnumber='" + txt_Adm_no.Text + "'   and (Transfer_Status='New' or Transfer_Status='NT') and   ar.Status='1'  order by rollnumber asc ";
            bind_grids(query);
        }

        private void bind_grids(string query)
        {
            DataTable dt1 = mycode.FillData(query);
            if (dt1.Rows.Count == 0)
            {
                grd_studentdetails.DataSource = null;
                grd_studentdetails.DataBind();
            }
            else
            {
                grd_studentdetails.DataSource = dt1;
                grd_studentdetails.DataBind();
            }
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
            if (ViewState["flag"].ToString() == "1")
            {
                if (ViewState["flagPosition"].ToString() == "1")
                {
                    string query = "Select * from admission_registor where session='" + ddl_session.SelectedItem.Text + "' and Section='" + ddl_section.Text + "' and Class_id=" + ddl_course.SelectedValue + "   and (Transfer_Status='New' or Transfer_Status='NT') and  Status='1'  order by studentname asc";
                    bind_grids(query);
                    ViewState["flagPosition"] = "0";
                }
                else
                {
                    string query = "Select * from admission_registor where session='" + ddl_session.SelectedItem.Text + "' and Section='" + ddl_section.Text + "' and Class_id=" + ddl_course.SelectedValue + "   and (Transfer_Status='New' or Transfer_Status='NT') and Status='1'  order by studentname desc";
                    bind_grids(query);
                    ViewState["flagPosition"] = "1";
                }
            }

        }
    }
}