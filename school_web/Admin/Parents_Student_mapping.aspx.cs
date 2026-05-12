using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

using System.Data;
using System.IO;
using System.Web.UI.HtmlControls;
using school_web.AppCode;
using System.Transactions;

namespace school_web.Admin
{
    public partial class Parents_Student_mapping : System.Web.UI.Page
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
                        ViewState["Applicant_Image"] = "";
                        ViewState["Userid"] = Session["Admin"].ToString();

                        string pagename_current = Path.GetFileName(Request.Path);
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        ViewState["session_id"] = My.get_session_id();
                        bind_data_view();

                        Bind_data_firm_detials();



                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Parents_Student_mapping");
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
        private void bind_data_view()
        {
            string query = "";
            if (ddl_status.Text == "ALL")
            {
                query = "Select pl.*,ps.Student_id ,(select  top 1 studentname from   admission_registor where admissionserialnumber=ps.Student_id ) as studentname from Parent_Login_Details pl join Parent_Student_Mapping ps on pl.User_id=ps.Parent_id join  admission_registor ar on ar.admissionserialnumber=ps.Student_id  where ar.Status='1' and ar.Session_id='"+My.get_session_id()+"'  order by pl.Name asc";
            }
            else
            {
               


                query = "Select pl.*,ps.Student_id ,(select  top 1 studentname from   admission_registor where admissionserialnumber=ps.Student_id ) as studentname from Parent_Login_Details pl join Parent_Student_Mapping ps on pl.User_id=ps.Parent_id join  admission_registor ar on ar.admissionserialnumber=ps.Student_id  where ar.Status='1' and ar.Session_id='" + My.get_session_id() + "'  and pl.Status='" + ddl_status.Text + "'  order by pl.Name asc";
            }
            bind_grd_view(query);
        }
        protected void btn_find_Click(object sender, EventArgs e)
        {
            bind_data_view();
        }
        private void bind_grd_view(string query)
        {
            ViewState["query"] = query;
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                btn_excels.Visible = false;
                print1.Visible = false;
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                btn_excels.Visible = true;
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

        private void Bind_data_firm_detials()
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
        protected void lnk_open_parents_mapping_Click(object sender, EventArgs e)
        {
            Repeater2_parents.DataSource = null;
            Repeater2_parents.DataBind();
            ViewState["Parentsname"] = "";
            ViewState["father_mob"] = "";
            hd_userid.Value = "0";
            lbl_heading_name.Text = "Mapping Parents With Student";
            studentdetails.Visible = false;
            txt_find_mobile_no.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal3();", true);
        }
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            if (ViewState["Is_Edit"].ToString() == "1")
            {

                lbl_heading_name.Text = "Mapping Parents With Student";
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_User_id = (Label)row.FindControl("lbl_User_id");
                string query = "Select *   from Parent_Login_Details where User_id='" + lbl_User_id.Text + "'";
                DataTable dt = mycode.FillData(query);
                if (dt.Rows.Count == 0)
                {

                }
                else
                {
                    ViewState["parentsid"] = dt.Rows[0]["User_id"].ToString();
                    hd_userid.Value = dt.Rows[0]["User_id"].ToString();
                    btn_map_new.Text = "Update";
                    ViewState["father_mob"] = dt.Rows[0]["Mobile"].ToString();
                    studentdetails.Visible = false;
                    string query2 = " Select ad.Course_Name as class ,ar.admissionserialnumber,ar.session,ar.studentname,ar.rollnumber,ar.Section,ar.fathername,ar.father_mob from admission_registor ar join  Add_course_table ad on ad.course_id=ar.Class_id where ar.Session_id='" + ViewState["session_id"].ToString() + "' and ar.father_mob='" + ViewState["father_mob"].ToString() + "' and ar.Status=1  order by ar.rollnumber,ad.Position asc";
                    DataTable dt1 = mycode.FillData(query2);
                    if (dt1.Rows.Count == 0)
                    {

                        Repeater2_parents.DataSource = null;
                        Repeater2_parents.DataBind();
                        ViewState["Parentsname"] = "";
                        ViewState["father_mob"] = "";
                        hd_userid.Value = "0";
                    }
                    else
                    {
                        ViewState["Parentsname"] = dt1.Rows[0]["fathername"].ToString();
                        ViewState["father_mob"] = dt1.Rows[0]["father_mob"].ToString();
                        studentdetails.Visible = true;
                        Repeater2_parents.DataSource = dt1;
                        Repeater2_parents.DataBind();

                    }






                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal3();", true);
                }
            }

            else
            {
                Alertme(My.get_restricted_message(), "warning");
            }
        }













        protected void lnk_active_inactive_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_User_id = (Label)row.FindControl("lbl_User_id");
                Label lbl_status = (Label)row.FindControl("lbl_status");
                if (lbl_status.Text == "Active")
                {
                    mycode.executequery("update Parent_Login_Details set Status='Inactive'  where User_id='" + lbl_User_id.Text + "'");
                }
                else
                {
                    mycode.executequery("update Parent_Login_Details set Status='Active'  where User_id='" + lbl_User_id.Text + "'");
                }
                bind_data_view();
            }
            catch
            {

            }
        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_delete"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_User_id = (Label)row.FindControl("lbl_User_id");
                    Label lbl_student_admission_no = (Label)row.FindControl("lbl_student_admission_no");

                    // mycode.executequery("delete from Parent_Login_Details where User_id='" + lbl_User_id.Text + "'");
                    mycode.executequery("delete from Parent_Student_Mapping where Parent_id='" + lbl_User_id.Text + "' and Student_id='" + lbl_student_admission_no.Text + "'");
                    Alertme("Parents details have been deleted successfully.", "success");
                    bind_data_view();
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            catch (Exception ex)
            {
                Alertme(ex.ToString(), "warning");
            }

        }

        protected bool IsChecked = true;
        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                this.IsChecked = false;
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=Parents_list" + mycode.date() + "_" + mycode.itime() + ".xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                    Panel1.RenderControl(hw);
                    string style = @"<style> TABLE { border: 1px solid black; } TD { border: 1px solid black; } </style> ";
                    Response.Write(style);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }

            }
            catch
            {
            }
            this.IsChecked = true;
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // Image Image1 = e.Item.FindControl("Image1") as Image;
                if (((Label)e.Item.FindControl("lbl_status")).Text.ToUpper() == "ACTIVE")
                {
                    //LinkButton bb = (System.Web.UI.WebControls.LinkButton)pagerTable.Rows[0].Cells[0].Controls[0];
                    //HtmlImage i = bb.Controls[0] as HtmlImage;
                    //i.Src = "~/NewImages/tick.jpg";


                    ((LinkButton)e.Item.FindControl("lnk_active_inactive")).Controls.Add(new Image { ImageUrl = "~/images/inactiveicon.png", });
                    ((LinkButton)e.Item.FindControl("lnk_active_inactive")).Controls.Add(new Label { Text = "Inactive" });





                    // ((LinkButton)e.Item.FindControl("lnk_active_inactive")).Text = "Deactive";
                    ((Label)e.Item.FindControl("lbl_status")).BackColor = System.Drawing.Color.Green;
                    ((Label)e.Item.FindControl("lbl_status")).ForeColor = System.Drawing.Color.White;

                }
                else
                {
                    ((LinkButton)e.Item.FindControl("lnk_active_inactive")).Controls.Add(new Image { ImageUrl = "~/images/activeicon.png" });

                    ((LinkButton)e.Item.FindControl("lnk_active_inactive")).Controls.Add(new Label { Text = "Active" });

                    //  ((LinkButton)e.Item.FindControl("lnk_active_inactive")).Text = "Active";
                    ((Label)e.Item.FindControl("lbl_status")).BackColor = System.Drawing.Color.Red;
                    ((Label)e.Item.FindControl("lbl_status")).ForeColor = System.Drawing.Color.White;
                }
            }
        }



        protected void lnk_student_view_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            Label lbl_User_id = (Label)row.FindControl("lbl_User_id");

            Label lbl_Name = (Label)row.FindControl("lbl_Name");
            Label lbl_Phone = (Label)row.FindControl("lbl_Phone");
            Label lbl_userid = (Label)row.FindControl("lbl_userid");
            Label lbl_Password = (Label)row.FindControl("lbl_Password");

            lbl_parntname.Text = lbl_Name.Text;
            lbl_parent_mb_no.Text = lbl_Phone.Text;
            lbl_user_id_parent.Text = lbl_userid.Text;
            lbl_password_parent.Text = lbl_Password.Text;
            hd_session_id.Value = ViewState["session_id"].ToString();
            ViewState["parentId"] = lbl_User_id.Text;
            fetch_alredt_mapped();
        }

        private void fetch_alredt_mapped()
        {
            string query = "Select ad.Course_Name as class ,ar.admissionserialnumber,ar.session,ar.studentname,ar.rollnumber,ar.Section from admission_registor ar join  Add_course_table ad on ad.course_id=ar.Class_id  where ar.Session_id='" + ViewState["session_id"].ToString() + "' and ar.admissionserialnumber in (select Student_id from Parent_Student_Mapping where Parent_id ='" + ViewState["parentId"].ToString() + "') and ar.Status=1  order by ar.rollnumber,ad.Position asc";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry, no student is mapped to these parents.", "warning");
                Repeater1.DataSource = null;
                Repeater1.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal2();", true);
                Repeater1.DataSource = dt;
                Repeater1.DataBind();
            }
        }



        #region find and maap
        protected void btn_map_new_Click(object sender, EventArgs e)
        {
            try
            {
                bool isChecked = false;
                int growcounts = Repeater2_parents.Items.Count;
                for (int i = 0; i < growcounts; i++)
                {
                    CheckBox chk = (CheckBox)Repeater2_parents.Items[i].FindControl("chkRowData");
                    if (chk.Checked == true)
                    {
                        isChecked = true;
                    }
                }
                if (isChecked == true)
                {
                    if (btn_map_new.Text == "Save")
                    {
                        if (ViewState["Is_add"].ToString() == "1")
                        {
                            bool chekastudent_add = get_student_data();
                            if (chekastudent_add == true)
                            {


                                string parentsid = My.get_rendome_id();
                                string pwd = "123";
                                bool flag = false;
                                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
                                {
                                    SqlConnection con = new SqlConnection(My.conn);
                                    con.Open();
                                    int growcount = Repeater2_parents.Items.Count;
                                    for (int j = 0; j < growcount; j++)
                                    {
                                        CheckBox chk = (CheckBox)Repeater2_parents.Items[j].FindControl("chkRowData");
                                        if (chk.Checked == true)
                                        {
                                            Label lbl_admissionserialnumber = (Label)Repeater2_parents.Items[j].FindControl("lbl_admissionserialnumber");
                                            send_dataParent_StudentMapping(lbl_admissionserialnumber.Text, parentsid, con);
                                        }
                                    }
                                    send_Parent_Login_Details(parentsid, pwd, con);

                                    flag = true;
                                    con.Close();
                                    scope.Complete();


                                }
                                if (flag == true)
                                {
                                    lbl_heading_name.Text = "Mapping Parents With Student";
                                    Alertme("The student has been successfully mapped with parents", "success");
                                    studentdetails.Visible = flag;
                                    Repeater2_parents.DataSource = null;
                                    Repeater2_parents.DataBind();
                                    txt_find_mobile_no.Text = "";
                                    btn_map_new.Text = "Save";

                                    hd_userid.Value = "";
                                    ViewState["Parentsname"] = "";
                                    ViewState["father_mob"] = "";
                                    bind_data_view();
                                }
                            }
                            else
                            {
                                Alertme("The student is already mapped with the parents.", "warning");
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal3();", true);
                            }
                        }
                        else
                        {
                            Alertme(My.get_restricted_message(), "warning");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal3();", true);
                        }
                    }
                    else
                    {

                        if (ViewState["Is_Edit"].ToString() == "1")
                        {

                            string parentsid = ViewState["parentsid"].ToString();
                            bool flag = false;
                            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
                            {
                                SqlConnection con = new SqlConnection(My.conn);
                                con.Open();

                                payments.exeSql("delete from Parent_Student_Mapping where Parent_id='" + parentsid + "'", con);
                                int growcount = Repeater2_parents.Items.Count;
                                for (int j = 0; j < growcount; j++)
                                {
                                    CheckBox chk = (CheckBox)Repeater2_parents.Items[j].FindControl("chkRowData");
                                    if (chk.Checked == true)
                                    {
                                        Label lbl_admissionserialnumber = (Label)Repeater2_parents.Items[j].FindControl("lbl_admissionserialnumber");
                                        send_dataParent_StudentMapping(lbl_admissionserialnumber.Text, parentsid, con);
                                    }
                                }
                                flag = true;
                                con.Close();
                                scope.Complete();

                            }

                            if (flag == true)
                            {
                                lbl_heading_name.Text = "Mapping Parents With Student";
                                Alertme("The student has been successfully mapped with student details", "success");
                                studentdetails.Visible = flag;
                                Repeater2_parents.DataSource = null;
                                Repeater2_parents.DataBind();
                                txt_find_mobile_no.Text = "";
                                btn_map_new.Text = "Save";
                                hd_userid.Value = "";
                                ViewState["Parentsname"] = "";
                                ViewState["father_mob"] = "";
                                bind_data_view();
                            }
                        }
                        else
                        {
                            Alertme(My.get_restricted_message(), "warning");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal3();", true);
                        }
                    }
                }
                else
                {
                    Alertme("Please select the student you would like to mape", "warning");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal3();", true);

                }

            }
            catch (Exception ex)
            {
                Alertme(ex.ToString(), "warning");
            }
        }

        private bool get_student_data()
        {
            string querychk = "Select * from Parent_Login_Details where Mobile='" + txt_find_mobile_no.Text + "'";
            DataTable dt = mycode.FillData(querychk);
            if (dt.Rows.Count == 0)
            {
                return true;
            }
            else
            {
                return false;

            }
        }

        private void send_Parent_Login_Details(string parentsid, string pwd, SqlConnection con)
        {

            string querychk = "Select *   from Parent_Login_Details where User_id='" + parentsid + "'";
            DataTable dt = payments.dataTable(querychk, con);
            if (dt.Rows.Count == 0)
            {
                string query = "INSERT INTO Parent_Login_Details (Name,User_id,Password,Mobile,Status,Created_by,Created_date) values (@Name,@User_id,@Password,@Mobile,@Status,@Created_by,DATEADD(hour,5,DATEADD(Minute,30,GETutcDATE())))";
                SqlCommand cmd;
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Name", ViewState["Parentsname"].ToString());
                cmd.Parameters.AddWithValue("@User_id", parentsid);
                cmd.Parameters.AddWithValue("@Password", pwd);
                cmd.Parameters.AddWithValue("@Mobile", ViewState["father_mob"].ToString());
                cmd.Parameters.AddWithValue("@Status", "Active");
                cmd.Parameters.AddWithValue("@Created_by", ViewState["Userid"].ToString());
                if (payments.InsertUpdateData(cmd, con))
                {

                }
            }
        }

        private void send_dataParent_StudentMapping(string Student_id, string Parent_id, SqlConnection con)
        {
            DataTable dt = payments.dataTable("select * from Parent_Student_Mapping where Student_id='" + Student_id + "'", con);
            if (dt.Rows.Count == 0)
            {
                string query = "INSERT INTO Parent_Student_Mapping (Parent_id,Student_id) values (@Parent_id,@Student_id)";
                SqlCommand cmd;
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Parent_id", Parent_id);
                cmd.Parameters.AddWithValue("@Student_id", Student_id);
                if (payments.InsertUpdateData(cmd, con))
                {
                }
            }
        }

        protected void btn_find_mobile_no_Click(object sender, EventArgs e)
        {
            hd_userid.Value = "";
            ViewState["Parentsname"] = "";
            ViewState["father_mob"] = "";
            studentdetails.Visible = false;
            if (txt_find_mobile_no.Text == "")
            {
                Alertme("Please enter valid parents mobile no.", "warning");
            }
            else
            {
                studentdetails.Visible = false;
                string query = " Select ad.Course_Name as class ,ar.admissionserialnumber,ar.session,ar.studentname,ar.rollnumber,ar.Section,ar.fathername,ar.father_mob from admission_registor ar join  Add_course_table ad on ad.course_id=ar.Class_id  where ar.Session_id='" + ViewState["session_id"].ToString() + "' and ar.father_mob='" + txt_find_mobile_no.Text.Trim() + "' and   ar.Status=1 order by ar.rollnumber,ad.Position asc";
                DataTable dt = mycode.FillData(query);
                if (dt.Rows.Count == 0)
                { 
                    Alertme("Sorry! There is no student associated with the given parents mobile number.", "warning");
                    Repeater2_parents.DataSource = null;
                    Repeater2_parents.DataBind();
                }
                else
                {
                    ViewState["Parentsname"] = dt.Rows[0]["fathername"].ToString();
                    ViewState["father_mob"] = dt.Rows[0]["father_mob"].ToString();
                    studentdetails.Visible = true;
                    Repeater2_parents.DataSource = dt;
                    Repeater2_parents.DataBind();

                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal3();", true);
        }
        #endregion
        protected void Repeater2_parents_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string admissionserialnumber = ((Label)e.Item.FindControl("lbl_admissionserialnumber")).Text;
                bool chkemape_student = find_student_id(admissionserialnumber, hd_userid.Value);

                if (chkemape_student == true)
                {
                    ((CheckBox)e.Item.FindControl("chkRowData")).Checked = true;
                }
                else
                {
                    ((CheckBox)e.Item.FindControl("chkRowData")).Checked = false;
                }
            }
        }
        private bool find_student_id(string admissionserialnumber, string parentsid)
        {
            DataTable dt = mycode.FillData("Select * from Parent_Student_Mapping where Parent_id='" + parentsid + "' and Student_id='" + admissionserialnumber + "'");
            if (dt.Rows.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        protected void btn_find_admission_no_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal2();", true);
                if (txt_admission_no.Text == "")
                {
                    Alertme("Please enter admission no.", "warning");
                    txt_admission_no.Focus();
                }
                else
                {
                    find_by_adm_no();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_by_adm_no()
        {
            find_Std_for_map("select * from admission_registor where admissionserialnumber='" + txt_admission_no.Text + "' and Session_id='" + hd_session_id.Value + "' and Status='1'  and admissionserialnumber not in (select Student_id from Parent_Student_Mapping) order by Section,rollnumber asc");
        }


        protected void btn_find_by_name_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal2();", true);
                if (txt_student_name.Text == "")
                {
                    Alertme("Please enter admission no.", "warning");
                    txt_student_name.Focus();
                }
                else
                {
                    find_by_std_name();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_by_std_name()
        {
            find_Std_for_map("select * from admission_registor where studentname like '%" + txt_student_name.Text.Trim() + "%' and Session_id='" + hd_session_id.Value + "' and Status='1' and admissionserialnumber not in (select Student_id from Parent_Student_Mapping) order by Section,rollnumber asc");
        }



        private void find_Std_for_map(string qry)
        {
            try
            {
                DataTable dt = My.dataTable(qry);
                if (dt.Rows.Count > 0)
                {
                    btn_map_more_std.Visible = true;
                    rp_add_more_std.DataSource = dt;
                    rp_add_more_std.DataBind();
                }
                else
                {
                    btn_map_more_std.Visible = false;
                    Alertme("Student not found or is already mapped to a parent.", "warning");
                }
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        } 
        protected void btn_map_more_std_Click(object sender, EventArgs e)
        {
            try
            {
                bool isChecked = false;
                int growcounts = rp_add_more_std.Items.Count;
                for (int i = 0; i < growcounts; i++)
                {
                    CheckBox chk = (CheckBox)rp_add_more_std.Items[i].FindControl("chkRowData");
                    if (chk.Checked == true)
                    {
                        isChecked = true;
                    }
                }
                if (isChecked == true)
                {
                    if (ViewState["Is_add"].ToString() == "1")
                    {
                        bool flag = false;
                        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
                        {
                            SqlConnection con = new SqlConnection(My.conn);
                            con.Open();
                            int growcount = rp_add_more_std.Items.Count;
                            for (int j = 0; j < growcount; j++)
                            {
                                CheckBox chk = (CheckBox)rp_add_more_std.Items[j].FindControl("chkRowData");
                                if (chk.Checked == true)
                                {
                                    Label lbl_admissionserialnumber = (Label)rp_add_more_std.Items[j].FindControl("lbl_admissionserialnumber");
                                    send_dataParent_StudentMapping(lbl_admissionserialnumber.Text, ViewState["parentId"].ToString(), con);
                                }
                            }
                            flag = true;
                            con.Close();
                            scope.Complete();
                        }
                        if (flag == true)
                        {
                            btn_map_more_std.Visible = false;
                            Alertme("The student has been successfully mapped with parent.", "success");
                            rp_add_more_std.DataSource = null;
                            rp_add_more_std.DataBind();
                            fetch_alredt_mapped();
                        } 
                    }
                    else
                    {
                        Alertme(My.get_restricted_message(), "warning");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal2();", true);
                    }
                }
                else
                {
                    Alertme("Please select the student you would like to mape", "warning");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal2();", true);
                }
            }
            catch (Exception ex)
            {
                Alertme(ex.ToString(), "warning");
            }
        }
    }
}
