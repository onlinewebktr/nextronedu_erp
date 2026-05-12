using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class Custome_Student_List_Download_Excel : System.Web.UI.Page
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
                        ViewState["saved_feildDT"] = "0";
                        ViewState["widthtable"] = "1019";
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();
                        ViewState["flag"] = "0";
                        mycode.bind_all_ddl_with_id(ddlsession, "Select  Session,session_id from session_details");
                        ddlsession.SelectedValue = My.get_session_id();
                        //mycode.bind_all_ddl_with_id_cap_All(ddlclass, "Select Course_Name,course_id,Position from Add_course_table order by Position");
                        //ddlclass.SelectedValue = My.get_top_one_class();
                        //mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor order by Section");
                        //ddl_section.Text = My.get_top_one_section();

                        bind_class(); bind_section();

                        fetch_saved_feild();
                        Bind_popup_data();

                        lbl_class22.Text = "Session: " + ddlsession.SelectedItem.Text;
                        find_firm_details();
                        string pagename_current = "student-report-home.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        bind_all_data();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "StudentList");
            }
        }

        private void bind_section()
        {
            using (SqlConnection conn = new SqlConnection(My.conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select distinct Section,Section as Sections from section_master order by Section asc", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                ddl_sections.DataTextField = "Section";
                ddl_sections.DataValueField = "Sections";
                ddl_sections.DataSource = reader;
                ddl_sections.DataBind();
            }
        }

        private void bind_class()
        {
            using (SqlConnection conn = new SqlConnection(My.conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Select Course_Name,course_id from Add_course_table order by Position", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                ddl_classs.DataTextField = "Course_Name";
                ddl_classs.DataValueField = "course_id";
                ddl_classs.DataSource = reader;
                ddl_classs.DataBind();
            }
        }
        private void fetch_saved_feild()
        {
            ViewState["saved_feildDT"] = "0";
            DataTable dt = My.dataTable("select * from Userwise_custom_std_feild where User_id='" + ViewState["Userid"].ToString() + "'");
            if (dt.Rows.Count > 0)
            {
                ViewState["saved_feildDT"] = dt;
            }
        }

        private void bind_all_data()
        {
            //lbl_class22.Text = ddlclass.SelectedItem.Text;
            // bind_grd_view("select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS Sl,admissionserialnumber as [Adm No or User id],Pwd as [Password],studentname as [Student Name],class as [Class],Section,rollnumber as [Roll No],gender as [Gender],dob as [DOB],CASE WHEN Transfer_Status = 'New' THEN 'New' WHEN Transfer_Status = 'NT' THEN 'Old'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS [Student Type],fathername as [Father Name],mothername as [Mother Name],father_mob as [Mobile No],careof as [Address] from admission_registor where (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred') and Status='1'   and Session_id='" + ddlsession.SelectedValue + "' and Class_Id=" + ddlclass.SelectedValue + " and Section='" + ddl_section.Text + "' order by Section,rollnumber asc");
            lbl_class22.Text = "Session: " + ddlsession.SelectedItem.Text;
        }

        private void total_count_grid_list(string query2)
        {
            try
            {
                lbl_newadmission.Text = "0";
                lbltotal_readmission.Text = "0";
                lbl_total_trasfer_tonextsession.Text = "0";
                DataSet ds = mycode.Fill_Data_set(query2);
                DataTable dt = ds.Tables[0];
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["Transfer_Status"].ToString() == "New")
                        {
                            lbl_newadmission.Text = dr["total"].ToString();
                        }
                        else if (dr["Transfer_Status"].ToString() == "NT")
                        {
                            lbltotal_readmission.Text = dr["total"].ToString();
                        }
                        else
                        {
                            lbl_total_trasfer_tonextsession.Text = dr["total"].ToString();
                        }
                    }
                }
            }
            catch
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


        private void bind_grd_view(string query)
        {
            try
            {
                //lbl_class22.Text = "Session : " + ddlsession.SelectedItem.Text + " Class : " + ddlclass.SelectedItem.Text + " Section : " + ddl_section.Text + " Student Type : " + ddl_studenttype.SelectedItem.Text;
                ViewState["query"] = query;
                DataTable dt = mycode.FillData(query);
                if (dt.Rows.Count == 0)
                {
                    Alertme("Sorry there are no data list exist", "warning");
                    grd_studentlist.DataSource = null;
                    grd_studentlist.DataBind();
                    lbl_total_student.Text = "0";
                }
                else
                {
                    lbl_total_student.Text = dt.Rows.Count.ToString();
                    grd_studentlist.DataSource = dt;
                    grd_studentlist.DataBind();
                    grd_studentlist.Style.Add("width", ViewState["widthtable"].ToString() + "px");
                }
            }
            catch
            {
            }
        }
        private void find_firm_details()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count > 0)
            {
                imglogo.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();
            }
        }
        private void Bind_popup_data()
        {
            try
            {
                DataTable dt = mycode.FillData("select * from Admission_custon_report where Is_Status=1 order by Group_id,Position asc");
                if (dt.Rows.Count == 0)
                {
                    RpList1.DataSource = null;
                    RpList1.DataBind();
                }
                else
                {
                    RpList1.DataSource = dt;
                    RpList1.DataBind();
                }
            }
            catch
            {
            }
        }


        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Download"].ToString() == "1")
                {
                    DataTable dt = mycode.FillData(ViewState["query"].ToString());
                    export_to_excel(dt, "Student_list");
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

        private void export_to_excel(DataTable dt, string file)
        {
            if (dt.Rows.Count > 0)
            {
                DateTime dTimet = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                string date = dTimet.ToString("dd_MM_yyyy");
                string time = dTimet.ToString("hh_mm_ss");
                String filerename = file + date + time;
                string attachment = "attachment; filename=" + filerename + ".csv";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "text/csv";
                var csvContent = My.DataTableToCsv(dt);
                Response.Write(csvContent);
                Response.End();
            }
            else
            {
                Alertme("Data not found.", "warning");
            }
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            lbl_class22.Text = "";
            try
            {
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session.", "warning");
                    ddlsession.Focus();
                    return;
                }

                string qoute = "'";
                //For Class
                bool isClassSelectd = false; string selectClassid = "";
                foreach (ListItem item in ddl_classs.Items)
                {
                    if (item.Selected)
                    {
                        selectClassid = selectClassid + qoute + item.Value + qoute + ",";
                        isClassSelectd = true;
                    }
                }
                if (isClassSelectd == false)
                {
                    ddl_classs.Focus();
                    Alertme("Please select class.", "warning");
                    return;
                }
                if (isClassSelectd == true)
                {
                    selectClassid = selectClassid.Remove(selectClassid.Length - 1);
                }

                //===========================
                bool isSectionSelectd = false; string selectSection = "";
                foreach (ListItem item in ddl_sections.Items)
                {
                    if (item.Selected)
                    {
                        selectSection = selectSection + qoute + item.Value + qoute + ",";
                        isSectionSelectd = true;
                    }
                }
                if (isSectionSelectd == false)
                {
                    ddl_sections.Focus();
                    Alertme("Please select section.", "warning");
                    return;
                }
                if (isSectionSelectd == true)
                {
                    selectSection = selectSection.Remove(selectSection.Length - 1);
                }



                //lbl_class22.Text = "Session : " + ddlsession.SelectedItem.Text + " Class : " + ddlclass.SelectedItem.Text + " Section : " + ddl_section.Text + " Student Type : " + ddl_studenttype.SelectedItem.Text;
                find_by_c_s_a(selectClassid, selectSection);
                save_checked_feild();
                fetch_saved_feild();
                ViewState["flag"] = "1";

            }
            catch (Exception ex)
            {
            }
        }

        private void save_checked_feild()
        {
            string qry = "delete from Userwise_custom_std_feild where User_id='" + ViewState["Userid"] + "'";
            My.exeSql(qry);
            int growcount = RpList1.Items.Count;
            for (int i = 0; i < growcount; i++)
            {
                CheckBox chk_column_name = (CheckBox)RpList1.Items[i].FindControl("chk_column_name");
                Label lbl_column = (Label)RpList1.Items[i].FindControl("lbl_column");
                if (chk_column_name.Checked == true)
                {
                    SqlCommand cmd;
                    string query = "INSERT INTO Userwise_custom_std_feild (Name,Columns_name,User_id,Updated_date) values (@Name,@Columns_name,@User_id,@Updated_date)";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Name", chk_column_name.Text);
                    cmd.Parameters.AddWithValue("@Columns_name", lbl_column.Text);
                    cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                    cmd.Parameters.AddWithValue("@Updated_date", mycode.date() + " " + mycode.time());
                    if (My.InsertUpdateData(cmd))
                    {
                    }
                }
            }
        }

        public void bind_column()
        {
            string cloumn = "";
            int growcount = RpList1.Items.Count;
            int k = 0; double widthtable = 0;
            for (int i = 0; i < growcount; i++)
            {
                CheckBox chk_column_name = (CheckBox)RpList1.Items[i].FindControl("chk_column_name");
                Label lbl_column = (Label)RpList1.Items[i].FindControl("lbl_column");
                if (chk_column_name.Checked == true)
                {
                    widthtable = widthtable + 150;
                    if (lbl_column.Text == "house")
                    {
                        string house_name = "Select top 1 house_name from house_master where house_id=admission_registor.house";
                        cloumn = cloumn + "(" + house_name + ") as [" + chk_column_name.Text + "],";
                    }
                    else if (lbl_column.Text == "Category_id")
                    {
                        string Category = "Select top 1 Category_Name from Category_Details where Category_Id=admission_registor.Category_id";
                        cloumn = cloumn + "(" + Category + ") as [" + chk_column_name.Text + "],";
                    }
                    else if (lbl_column.Text == "SubCategory_id")
                    {
                        string Sub_CategoryName = "Select top 1 Sub_CategoryName from Sub_Category_Details where Category_Id=admission_registor.Category_id and Sub_CategoryId=admission_registor.SubCategory_id";
                        cloumn = cloumn + "(" + Sub_CategoryName + ") as [" + chk_column_name.Text + "],";
                    }
                    else if (lbl_column.Text == "Std_type_d_h_bus")
                    {
                        string student_types = "CASE WHEN hosteltaken = 'Yes' THEN 'Hostel' WHEN transportationtaken = 'Yes' THEN 'Bus' ELSE 'Day Scholar' END";
                        cloumn = cloumn + student_types + " as [" + chk_column_name.Text + "],";
                    }
                    else if (lbl_column.Text == "Transfer_Status")
                    {
                        string trnsfr_status = "CASE WHEN Transfer_Status = 'New' THEN 'New' WHEN Transfer_Status = 'NT' THEN 'Old'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END";
                        cloumn = cloumn + trnsfr_status + " as [" + chk_column_name.Text + "],";
                    }
                    else if (lbl_column.Text == "Old_class_id")
                    {
                        string passoutClass = "Select top 1 Course_Name from Add_course_table where course_id=admission_registor.Old_class_id";
                        cloumn = cloumn + "(" + passoutClass + ") as [" + chk_column_name.Text + "],";
                    }
                    else if (lbl_column.Text == "0000")
                    {
                    }
                    else if (lbl_column.Text == "hosteltaken")
                    {
                        cloumn = cloumn + lbl_column.Text + " as [" + chk_column_name.Text + "],";
                        cloumn = cloumn + "(select   (select top 1 Room_name from HOSTEL_ROOM_MASTER    where Hostel_id=asm.Hostel_id and Category_id=asm.Category_id and Room_id= asm.Room_id ) from HOSTEL_ASSIGN_MASTER asm where asm.Admission_no=admission_registor.admissionserialnumber and asm.Session_id='" + ddlsession.SelectedValue + "' and asm.Status='1') as [Room Name],";
                        cloumn = cloumn + "(select  (select top 1 Bed_name from HOSTEL_ROOM_BED_MASTER    where Hostel_id=asm.Hostel_id and Category_id=asm.Category_id and Room_id= asm.Room_id ) from HOSTEL_ASSIGN_MASTER asm where asm.Admission_no=admission_registor.admissionserialnumber and asm.Session_id='" + ddlsession.SelectedValue + "' and asm.Status='1') as [Bad Name],";
                    }
                    else if (lbl_column.Text == "transportationtaken")
                    {
                        cloumn = cloumn + lbl_column.Text + " as [" + chk_column_name.Text + "],";
                        cloumn = cloumn + "(select   (Select top 1 Boarding_Point from Transportation_Boarding_Point  where Session_Id=asm.Session_id and Boarding_Point_id=asm.Boarding_Point_id) from Student_mapping_with_TransportPath asm where asm.Admission_no=admission_registor.admissionserialnumber and asm.Session_id='" + ddlsession.SelectedValue + "') as [Boarding Point],";
                    }
                    else
                    {
                        cloumn = cloumn + lbl_column.Text + " as [" + chk_column_name.Text + "],";
                    }
                }
            }
            if (cloumn == "")
            {
                ViewState["widthtable"] = "1019";
                cloumn = "admissionserialnumber as [Adm. No.],studentname as [Student Name],class as [Class],Section,rollnumber as [Roll No],gender as [Gender],dob as [DOB],CASE WHEN Transfer_Status = 'New' THEN 'New' WHEN Transfer_Status = 'NT' THEN 'Old'  WHEN Transfer_Status = 'Transferred' THEN 'Transferred' END AS [Student Type],fathername as [Father Name],mothername as [Mother Name],father_mob as [Mobile No],careof as [Address]";
            }
            else
            {
                ViewState["widthtable"] = widthtable;
                cloumn = cloumn.Remove(cloumn.Length - 1);
            }
            ViewState["q1"] = cloumn;
        }


        private void find_by_c_s_a(string selectClassid, string selectSection)
        {
            bind_column();
            if (ddl_studenttype.Text == "ALL")
            {
                string query = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS Sl, " + ViewState["q1"].ToString() + " from admission_registor join Add_course_table t2 on admission_registor.Class_id=t2.course_id where Session_id='" + ddlsession.SelectedValue + "' and Class_id in (" + selectClassid + ") and Section in (" + selectSection + ") and Status='1' order by t2.Position,Section,rollnumber asc";
                bind_grd_view(query);
            }
            else
            {
                if (ddl_studenttype.Text.ToUpper() == "RTE")
                {
                    string query = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS Sl, " + ViewState["q1"].ToString() + " from admission_registor join Add_course_table t2 on admission_registor.Class_id=t2.course_id where Session_id='" + ddlsession.SelectedValue + "' and Class_id in (" + selectClassid + ") and Section in (" + selectSection + ") and Status='1'  and RTE in ('YES','Yes','yes') order by t2.Position,Section,rollnumber asc";
                    bind_grd_view(query);
                }
                else
                {
                    string query = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS Sl, " + ViewState["q1"].ToString() + " from admission_registor join Add_course_table t2 on admission_registor.Class_id=t2.course_id where Session_id='" + ddlsession.SelectedValue + "' and Class_id in (" + selectClassid + ") and Section in (" + selectSection + ") and Status='1'  and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' order by t2.Position,Section,rollnumber asc";
                    bind_grd_view(query);
                }
            }


            #region CommentedCode
            //if (ddlclass.SelectedItem.Text == "ALL")
            //{
            //    if (ddl_section.Text == "ALL" && ddl_studenttype.Text == "ALL")
            //    {
            //        string query = "Select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS Sl, " + ViewState["q1"].ToString() + " from admission_registor join Add_course_table t2 on admission_registor.Class_id=t2.course_id where Session_id='" + ddlsession.SelectedValue + "' and Status='1'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  order by t2.Position,Section,rollnumber asc";
            //        bind_grd_view(query);
            //        //query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' group by Transfer_Status ";
            //        //total_count_grid_list(query2); 
            //    }
            //    else if (ddl_section.Text != "ALL" && ddl_studenttype.Text == "ALL")
            //    {
            //        string query = "Select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS Sl, " + ViewState["q1"].ToString() + " from admission_registor join Add_course_table t2 on admission_registor.Class_id=t2.course_id where  Session_id='" + ddlsession.SelectedValue + "' and Section='" + ddl_section.Text + "'  and Status='1' and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  order by t2.Position,Section,rollnumber asc";
            //        bind_grd_view(query);
            //        //query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where where Session_id='" + ddlsession.SelectedValue + "' and Section='" + ddl_section.Text + "'   and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')   group by Transfer_Status ";
            //        //total_count_grid_list(query2);
            //    }
            //    else if (ddl_section.Text != "ALL" && ddl_studenttype.Text != "ALL")
            //    {
            //        string query = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS Sl, " + ViewState["q1"].ToString() + " from admission_registor join Add_course_table t2 on admission_registor.Class_id=t2.course_id where Session_id='" + ddlsession.SelectedValue + "' and Section='" + ddl_section.Text + "' and Status='1'  and Transfer_Status ='" + ddl_studenttype.SelectedValue + "'   order by t2.Position,Section,rollnumber asc";
            //        bind_grd_view(query);
            //        //query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where   where Session_id='" + ddlsession.SelectedValue + "' and Section='" + ddl_section.Text + "'  and Transfer_Status ='" + ddl_studenttype.SelectedValue + "'   group by Transfer_Status ";
            //        //total_count_grid_list(query2);
            //    }
            //    else if (ddl_section.Text == "ALL" && ddl_studenttype.Text != "ALL")
            //    {
            //        string query = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS Sl, " + ViewState["q1"].ToString() + " from admission_registor join Add_course_table t2 on admission_registor.Class_id=t2.course_id where Session_id='" + ddlsession.SelectedValue + "' and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1'  order by t2.Position,Section,rollnumber asc";
            //        bind_grd_view(query);
            //        //query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where  Session_id='" + ddlsession.SelectedValue + "' and Transfer_Status ='" + ddl_studenttype.SelectedValue + "'  group by Transfer_Status ";
            //        //total_count_grid_list(query2);
            //    }
            //}
            //else
            //{
            //    if (ddl_section.Text == "ALL" && ddl_studenttype.Text == "ALL")
            //    {
            //        string query = "Select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS Sl, " + ViewState["q1"].ToString() + " from admission_registor join Add_course_table t2 on admission_registor.Class_id=t2.course_id where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Status='1'  and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  order by t2.Position,Section,rollnumber asc";
            //        bind_grd_view(query);
            //        //query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'   group by Transfer_Status ";
            //        //total_count_grid_list(query2);


            //    }
            //    else if (ddl_section.Text != "ALL" && ddl_studenttype.Text == "ALL")
            //    {
            //        string query = "Select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS Sl, " + ViewState["q1"].ToString() + "  from admission_registor join Add_course_table t2 on admission_registor.Class_id=t2.course_id where  Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'  and Status='1' and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')  order by t2.Position,Section,rollnumber asc";
            //        bind_grd_view(query);
            //        //query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'   and (Transfer_Status='New' or Transfer_Status='NT' or Transfer_Status='Transferred')   group by Transfer_Status ";
            //        //total_count_grid_list(query2);
            //    }
            //    else if (ddl_section.Text != "ALL" && ddl_studenttype.Text != "ALL")
            //    {
            //        string query = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS Sl, " + ViewState["q1"].ToString() + " from admission_registor join Add_course_table t2 on admission_registor.Class_id=t2.course_id where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "' and Status='1'  and Transfer_Status ='" + ddl_studenttype.SelectedValue + "'   order by t2.Position,Section,rollnumber asc";
            //        bind_grd_view(query);
            //        //query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where   where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Section='" + ddl_section.Text + "'  and Transfer_Status ='" + ddl_studenttype.SelectedValue + "'   group by Transfer_Status ";
            //        //total_count_grid_list(query2);
            //    }
            //    else if (ddl_section.Text == "ALL" && ddl_studenttype.Text != "ALL")
            //    {
            //        string query = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS Sl, " + ViewState["q1"].ToString() + " from admission_registor join Add_course_table t2 on admission_registor.Class_id=t2.course_id where Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'    and Transfer_Status ='" + ddl_studenttype.SelectedValue + "' and Status='1'  order by t2.Position,Section,rollnumber asc";
            //        bind_grd_view(query);
            //        //query2 = "select  Transfer_Status,count(Id) as total  from admission_registor  where  Session_id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "'    and Transfer_Status ='" + ddl_studenttype.SelectedValue + "'  group by Transfer_Status ";
            //        //total_count_grid_list(query2);
            //    }
            //}
            #endregion
        }

        protected void chk_all_CheckedChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            for (int j = 0; j < RpList1.Items.Count; j++)
            {
                CheckBox chk_column_name = RpList1.Items[j].FindControl("chk_column_name") as CheckBox;
                if (chk_all.Checked)
                {
                    chk_column_name.Checked = true;
                }
                else
                {
                    chk_column_name.Checked = false;
                }
            }
        }




        protected void RpList1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                CheckBox chk_column_name = (CheckBox)e.Item.FindControl("chk_column_name");
                Label lbl_column = (Label)e.Item.FindControl("lbl_column");
                Label lbl_heading_name = (Label)e.Item.FindControl("lbl_heading_name");
                HtmlGenericControl tdDVS = e.Item.FindControl("tdDVS") as HtmlGenericControl;
                if (lbl_column.Text == "0000")
                {
                    lbl_heading_name.Visible = true;
                    chk_column_name.Visible = false;
                    lbl_heading_name.CssClass = "popupheadinG";
                    tdDVS.Attributes.Add("class", "fullWidthDV");
                }
                else
                {
                    lbl_heading_name.Visible = false;
                }

                try
                {
                    if (ViewState["saved_feildDT"].ToString() == "0") { }
                    else
                    {
                        DataTable dt = ViewState["saved_feildDT"] as DataTable;
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (dr["Columns_name"].ToString() == lbl_column.Text)
                            {
                                chk_column_name.Checked = true;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        protected void btn_reset_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "delete from Userwise_custom_std_feild where User_id='" + ViewState["Userid"] + "'";
                My.exeSql(qry);
                fetch_saved_feild();
                Bind_popup_data();
                Alertme("The setting was reset successfully.", "success");
            }
            catch (Exception ex)
            {
            }
        }
    }
}