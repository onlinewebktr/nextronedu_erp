using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.LMS_VC_Admin
{
    public partial class InstructorCourseMapping : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
        string scrpt;
        protected void Page_Load(object sender, EventArgs e)
        {
            try { if (!IsPostBack) { BindCourse(); BindGridView(); AddMultipleSUb(); };}
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }
        private void BindCourse()
        {

            code.bind_all_ddl_with_id(ddl_CourseCat, "Select CategoryName, CategoryID from ClassMaster where Istatus='1' order by Position asc");
            code.bind_all_ddl_with_all(ddl_SearchCategory, "Select CategoryName, CategoryID from ClassMaster where Istatus='1' order by Position asc");
            code.bind_all_ddl_with_id(ddl_instructor, "Select  Name+','+PhoneNo , UserID from InstructorProfile where Istatus='1' and   (Type!='Super Admin' or Type is null) order by Name");
            code.bind_all_ddl_with_all(ddl_searchInstructor, "Select  Name+','+PhoneNo , UserID from InstructorProfile where Istatus='1' and   (Type!='Super Admin' or Type is null) order by Name");

            // code.bind_all_ddl_with_id(ddl_multipal_techer, "Select Name, UserID from InstructorProfile where Istatus='1' order by Name");

        }

        private void BindGridView()
        {
            try
            {
                string query = " Select cm.CategoryName,cm.CategoryID,csm.CourseName,tcsm.AssignCourseID,tcsm.section,tcsm.UserID,tcsm.Id,(select Name from InstructorProfile where UserID=tcsm.UserID) as UserName  from TeacherCourseSubjectMaping tcsm join ClassMaster cm on tcsm.CategoryID=cm.CategoryID join Course_or_Subject_Master csm on tcsm.CategoryID=csm.CategoryID and tcsm.AssignCourseID=csm.CourseID and tcsm.section=csm.section order by cm.Position,tcsm.section asc";
                code.BindRepeater(query, RPDetails);
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }
        public void Alert(string Message)
        {
            lbl_msg.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }

        protected void ddl_Course_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                code.bind_ddl(ddl_section, "Select distinct section  from Course_or_Subject_Master where CategoryID ='" + ddl_CourseCat.SelectedValue + "' and Istatus='1' order by section");
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }
        protected void ddl_section_SelectedIndexChanged(object sender, EventArgs e)
        {
            code.bind_all_ddl_with_id(ddl_Course, "Select CourseName, CourseID from Course_or_Subject_Master where CategoryID ='" + ddl_CourseCat.SelectedValue + "' and section='" + ddl_section + "' and Istatus='1' order by CourseName");
        }


        protected void btn_submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_instructor.SelectedValue == "0")
                {
                    lbl_msg.Text = "Please Select Teacher Name";
                    scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false); return;
                }
                else
                {
                    if (btn_submit.Text == "Submit")
                    {
                        send_Info();
                        AddMultipleSUb();
                        BindGridView();
                        //foreach (ListItem item in lst_Course.Items)
                        //{
                        //    if (item.Selected)
                        //    {

                        //    }
                        //}
                    }
                    if (btn_submit.Text == "Update")
                    {
                        if (code.IsExist("Select * from TeacherCourseSubjectMaping where AssignCourseID='" + ddl_Course.SelectedValue + "' and UserID='" + ddl_instructor.SelectedValue + "' and CategoryID='" + ddl_CourseCat.SelectedValue + "' and section='" + ddl_section.SelectedValue + "'  and Id!='" + Session["lbl_id"] + "'"))
                        {
                            SqlCommand cmd = new SqlCommand("Update TeacherCourseSubjectMaping set CategoryID='" + ddl_CourseCat.SelectedValue + "', AssignCourseID= '" + ddl_Course.SelectedValue + "'," +
                                "UserID='" + ddl_instructor.SelectedValue + "',sync_status=0  where Id='" + Session["lbl_id"] + "'");
                            InsertUpdate.InsertUpdateData(cmd);
                            code.BindRepeater(" select I.*, (select CategoryName from ClassMaster where CategoryID=I.CategoryID) as CatName,(select CourseName from Course_or_Subject_Master where CourseID=I.AssignCourseID) as CourseName,(select Name from InstructorProfile where UserID=I.UserID) as UserName from TeacherCourseSubjectMaping I where I.CategoryID='" + ddl_CourseCat.SelectedValue + "' order by CatName Asc", RPDetails);

                            btn_submit.Text = "Submit"; panlEdit.Visible = false; GrdSub.Visible = true;
                            Alert("Subject successfully Updated.");
                        }

                    }
                }
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                btn_submit.Text = "Update"; panlEdit.Visible = true; GrdSub.Visible = false;
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_id = (Label)row.FindControl("lbl_Id");
                Label lbl_section = (Label)row.FindControl("lbl_section");
                HiddenField CategoryID = (HiddenField)row.FindControl("hd_CategoryID");
                HiddenField AssignCourse = (HiddenField)row.FindControl("hdAssignCourseID");
                HiddenField UserID = (HiddenField)row.FindControl("hdUserID");
                Image Image = (Image)row.FindControl("Image1");
                Session["lbl_id"] = lbl_id.Text;
                code.bind_all_ddl_with_id(ddl_CourseCat, "Select CategoryName, CategoryID from ClassMaster order by Position asc");
                ddl_CourseCat.SelectedValue = CategoryID.Value;
                code.bind_ddl(ddl_section, "Select distinct section  from Course_or_Subject_Master where CategoryID ='" + ddl_CourseCat.SelectedValue + "' and Istatus='1' order by section");
                ddl_section.Text = lbl_section.Text;
                code.bind_all_ddl_with_id(ddl_Course, "Select CourseName, CourseID from Course_or_Subject_Master where CategoryID='" + ddl_CourseCat.SelectedValue + "' and section='" + lbl_section.Text + "'   and Istatus='1' order by CourseName");
                ddl_Course.SelectedValue = AssignCourse.Value;


                code.bind_all_ddl_with_id(ddl_instructor, "Select Name, UserID from InstructorProfile where Istatus='1' and    (Type!='Super Admin' or Type is null) order by Name");
                ddl_instructor.SelectedValue = UserID.Value;
                BindGridView();
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        protected void lnk_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_id = (Label)row.FindControl("lbl_Id");
                SqlCommand cmd = new SqlCommand("Delete from TeacherCourseSubjectMaping where Id='" + lbl_id.Text + "'");
                InsertUpdate.InsertUpdateData(cmd);
                BindGridView(); Alert("successfully Deleted.");
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        protected void btn_Find_Click(object sender, EventArgs e)
        {
            string query = "";
            try
            {
                if (ddl_SearchCategory.SelectedItem.Text == "Select")
                {
                    Alert("Please select class");

                }
                if (ddl_searchInstructor.SelectedItem.Text == "Select")
                {
                    Alert("Please select teacher");

                }
                else if (ddl_SearchCategory.SelectedItem.Text != "ALL" && ddl_searchInstructor.SelectedItem.Text != "ALL")
                {
                    if (ddl_section_serch.Text == "ALL")
                    {
                        query = " Select cm.CategoryName,cm.CategoryID,csm.CourseName,tcsm.AssignCourseID,tcsm.section,tcsm.UserID,tcsm.Id,(select Name from InstructorProfile where UserID=tcsm.UserID) as UserName  from TeacherCourseSubjectMaping tcsm join ClassMaster cm on tcsm.CategoryID=cm.CategoryID join Course_or_Subject_Master csm on tcsm.CategoryID=csm.CategoryID and tcsm.AssignCourseID=csm.CourseID and tcsm.section=csm.section where  tcsm.UserID='" + ddl_searchInstructor.SelectedValue + "' and   tcsm.CategoryID='" + ddl_SearchCategory.Text + "' order by cm.Position,tcsm.section asc";

                        code.BindRepeater(query, RPDetails);
                    }
                    else if (ddl_section_serch.Text == "")
                    {
                        query = " Select cm.CategoryName,cm.CategoryID,csm.CourseName,tcsm.AssignCourseID,tcsm.section,tcsm.UserID,tcsm.Id,(select Name from InstructorProfile where UserID=tcsm.UserID) as UserName  from TeacherCourseSubjectMaping tcsm join ClassMaster cm on tcsm.CategoryID=cm.CategoryID join Course_or_Subject_Master csm on tcsm.CategoryID=csm.CategoryID and tcsm.AssignCourseID=csm.CourseID and tcsm.section=csm.section where  tcsm.UserID='" + ddl_searchInstructor.SelectedValue + "' and   tcsm.CategoryID='" + ddl_SearchCategory.Text + "' order by cm.Position,tcsm.section asc";

                        code.BindRepeater(query, RPDetails);
                    }
                    else
                    {
                        query = " Select cm.CategoryName,cm.CategoryID,csm.CourseName,tcsm.AssignCourseID,tcsm.section,tcsm.UserID,tcsm.Id,(select Name from InstructorProfile where UserID=tcsm.UserID) as UserName  from TeacherCourseSubjectMaping tcsm join ClassMaster cm on tcsm.CategoryID=cm.CategoryID join Course_or_Subject_Master csm on tcsm.CategoryID=csm.CategoryID and tcsm.AssignCourseID=csm.CourseID and tcsm.section=csm.section where  tcsm.UserID='" + ddl_searchInstructor.SelectedValue + "' and   tcsm.CategoryID='" + ddl_SearchCategory.Text + "' and tcsm.section= '" + ddl_section_serch.Text + "' order by cm.Position,tcsm.section asc";




                        code.BindRepeater(query, RPDetails);
                    }


                }
                else if (ddl_SearchCategory.SelectedItem.Text == "ALL" && ddl_searchInstructor.SelectedItem.Text != "ALL")
                {
                    if (ddl_section_serch.Text == "ALL")
                    {
                        query = " Select cm.CategoryName,cm.CategoryID,csm.CourseName,tcsm.AssignCourseID,tcsm.section,tcsm.UserID,tcsm.Id,(select Name from InstructorProfile where UserID=tcsm.UserID) as UserName  from TeacherCourseSubjectMaping tcsm join ClassMaster cm on tcsm.CategoryID=cm.CategoryID join Course_or_Subject_Master csm on tcsm.CategoryID=csm.CategoryID and tcsm.AssignCourseID=csm.CourseID and tcsm.section=csm.section where  tcsm.UserID='" + ddl_searchInstructor.SelectedValue + "'  order by cm.Position,tcsm.section asc";

                    }
                    else if (ddl_section_serch.Text == "")
                    {
                        query = " Select cm.CategoryName,cm.CategoryID,csm.CourseName,tcsm.AssignCourseID,tcsm.section,tcsm.UserID,tcsm.Id,(select Name from InstructorProfile where UserID=tcsm.UserID) as UserName  from TeacherCourseSubjectMaping tcsm join ClassMaster cm on tcsm.CategoryID=cm.CategoryID join Course_or_Subject_Master csm on tcsm.CategoryID=csm.CategoryID and tcsm.AssignCourseID=csm.CourseID and tcsm.section=csm.section where  tcsm.UserID='" + ddl_searchInstructor.SelectedValue + "'  order by cm.Position,tcsm.section asc";
                    }
                    else
                    {
                        query = " Select cm.CategoryName,cm.CategoryID,csm.CourseName,tcsm.AssignCourseID,tcsm.section,tcsm.UserID,tcsm.Id,(select Name from InstructorProfile where UserID=tcsm.UserID) as UserName  from TeacherCourseSubjectMaping tcsm join ClassMaster cm on tcsm.CategoryID=cm.CategoryID join Course_or_Subject_Master csm on tcsm.CategoryID=csm.CategoryID and tcsm.AssignCourseID=csm.CourseID and tcsm.section=csm.section where  tcsm.UserID='" + ddl_searchInstructor.SelectedValue + "' and   tcsm.section='" + ddl_section_serch.Text + "' order by cm.Position,tcsm.section asc";
                    }

                    code.BindRepeater(query, RPDetails);

                }
                else if (ddl_SearchCategory.SelectedItem.Text != "ALL" && ddl_searchInstructor.SelectedItem.Text == "ALL")
                {
                    if (ddl_section_serch.Text == "ALL")
                    {
                        query = " Select cm.CategoryName,cm.CategoryID,csm.CourseName,tcsm.AssignCourseID,tcsm.section,tcsm.UserID,tcsm.Id,(select Name from InstructorProfile where UserID=tcsm.UserID) as UserName  from TeacherCourseSubjectMaping tcsm join ClassMaster cm on tcsm.CategoryID=cm.CategoryID join Course_or_Subject_Master csm on tcsm.CategoryID=csm.CategoryID and tcsm.AssignCourseID=csm.CourseID and tcsm.section=csm.section where  tcsm.CategoryID='" + ddl_SearchCategory.SelectedValue + "' order by cm.Position,tcsm.section asc";


                        code.BindRepeater(query, RPDetails);
                    }
                    else if (ddl_section_serch.Text == "")
                    {
                        query = " Select cm.CategoryName,cm.CategoryID,csm.CourseName,tcsm.AssignCourseID,tcsm.section,tcsm.UserID,tcsm.Id,(select Name from InstructorProfile where UserID=tcsm.UserID) as UserName  from TeacherCourseSubjectMaping tcsm join ClassMaster cm on tcsm.CategoryID=cm.CategoryID join Course_or_Subject_Master csm on tcsm.CategoryID=csm.CategoryID and tcsm.AssignCourseID=csm.CourseID and tcsm.section=csm.section where  tcsm.CategoryID='" + ddl_SearchCategory.SelectedValue + "' order by cm.Position,tcsm.section asc";


                        code.BindRepeater(query, RPDetails);
                    }
                    else
                    {
                        query = " Select cm.CategoryName,cm.CategoryID,csm.CourseName,tcsm.AssignCourseID,tcsm.section,tcsm.UserID,tcsm.Id,(select Name from InstructorProfile where UserID=tcsm.UserID) as UserName  from TeacherCourseSubjectMaping tcsm join ClassMaster cm on tcsm.CategoryID=cm.CategoryID join Course_or_Subject_Master csm on tcsm.CategoryID=csm.CategoryID and tcsm.AssignCourseID=csm.CourseID and tcsm.section=csm.section where tcsm.section='" + ddl_section_serch.Text + "' and tcsm.CategoryID='" + ddl_SearchCategory.SelectedValue + "' order by cm.Position,tcsm.section asc";



                        code.BindRepeater(query, RPDetails);
                    }
                }

                else if (ddl_SearchCategory.SelectedItem.Text == "ALL" && ddl_searchInstructor.SelectedItem.Text == "ALL")
                {
                    if (ddl_section_serch.Text == "ALL")
                    {
                        query = " Select cm.CategoryName,cm.CategoryID,csm.CourseName,tcsm.AssignCourseID,tcsm.section,tcsm.UserID,tcsm.Id,(select Name from InstructorProfile where UserID=tcsm.UserID) as UserName   from TeacherCourseSubjectMaping tcsm join ClassMaster cm on tcsm.CategoryID=cm.CategoryID join Course_or_Subject_Master csm on tcsm.CategoryID=csm.CategoryID and tcsm.AssignCourseID=csm.CourseID and tcsm.section=csm.section   order by cm.Position,tcsm.section asc";
                        code.BindRepeater(query, RPDetails);
                    }
                    else if (ddl_section_serch.Text == "")
                    {
                        query = " Select cm.CategoryName,cm.CategoryID,csm.CourseName,tcsm.AssignCourseID,tcsm.section,tcsm.UserID,tcsm.Id,(select Name from InstructorProfile where UserID=tcsm.UserID) as UserName   from TeacherCourseSubjectMaping tcsm join ClassMaster cm on tcsm.CategoryID=cm.CategoryID join Course_or_Subject_Master csm on tcsm.CategoryID=csm.CategoryID and tcsm.AssignCourseID=csm.CourseID and tcsm.section=csm.section  order by cm.Position,tcsm.section asc";
                        code.BindRepeater(query, RPDetails);
                    }
                    else
                    {


                        query = " Select cm.CategoryName,cm.CategoryID,csm.CourseName,tcsm.AssignCourseID,tcsm.section,tcsm.UserID,tcsm.Id,(select Name from InstructorProfile where UserID=tcsm.UserID) as UserName   from TeacherCourseSubjectMaping tcsm join ClassMaster cm on tcsm.CategoryID=cm.CategoryID join Course_or_Subject_Master csm on tcsm.CategoryID=csm.CategoryID and tcsm.AssignCourseID=csm.CourseID and tcsm.section=csm.section where tcsm.section='" + ddl_section_serch.Text + "' order by cm.Position,tcsm.section asc";

                        code.BindRepeater(query, RPDetails);
                    }
                }


            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }



        protected void img_expor_excel_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dt = ViewState["Data"] as DataTable;
            export_to_excel(dt, "TeacherList");
        }
        private void export_to_excel(DataTable dt, string file)
        {

            string FileName = file + DateTime.Now + ".xls";

            string attachment = "attachment; filename=" + FileName;

            Response.ClearContent();

            Response.AddHeader("content-disposition", attachment);

            Response.ContentType = "application/vnd.ms-excel";

            Response.ContentEncoding = System.Text.Encoding.Unicode;

            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());

            string tab = "";

            foreach (DataColumn dc in dt.Columns)
            {

                Response.Write(tab + dc.ColumnName);

                tab = "\t";

            }

            Response.Write("\n");

            int i;

            foreach (DataRow dr in dt.Rows)
            {

                tab = "";

                for (i = 0; i < dt.Columns.Count; i++)
                {

                    Response.Write(tab + dr[i].ToString());

                    tab = "\t";

                }

                Response.Write("\n");

            }

            Response.End();

        }

        protected void btn_find_multipl_Click(object sender, EventArgs e)
        {

        }


        #region SubInfo
        private void AddMultipleSUb()
        {
            DataTable dt;
            dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("CategoryID", typeof(string)));
            dt.Columns.Add(new DataColumn("section", typeof(string)));
            dt.Columns.Add(new DataColumn("AssignCourseID", typeof(string)));
            dr = dt.NewRow();
            dr["CategoryID"] = string.Empty;
            dr["section"] = string.Empty;
            dr["AssignCourseID"] = string.Empty;

            dt.Rows.Add(dr);
            ViewState["SubInfo"] = dt;
            GrdSub.DataSource = ViewState["SubInfo"];
            GrdSub.DataBind();
            DropDownList ddl1 = (DropDownList)GrdSub.Rows[0].Cells[0].FindControl("ddl_CourseCat");
            code.bind_all_ddl_with_id(ddl1, "Select CategoryName, CategoryID from ClassMaster where Istatus='1' order by CategoryName");
        }
        protected void img_btnFam_add_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                AddNewRowToGrid();
            }
            catch (Exception exc)
            {

            }
        }


        private void AddNewRowToGrid()
        {
            int rowIndex = 0;
            if (ViewState["SubInfo"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["SubInfo"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        DropDownList CourseName = (DropDownList)GrdSub.Rows[rowIndex].Cells[0].FindControl("ddl_CourseCat");
                        DropDownList lstsection = (DropDownList)GrdSub.Rows[rowIndex].Cells[1].FindControl("ddl_section");
                        DropDownList lstCourse = (DropDownList)GrdSub.Rows[rowIndex].Cells[2].FindControl("ddlCourse");
                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i - 1]["CategoryID"] = CourseName.SelectedValue;

                        code.bind_ddl(lstsection, "Select distinct section  from Course_or_Subject_Master where CategoryID ='" + CourseName.SelectedValue + "' and Istatus='1' order by section");
                        dtCurrentTable.Rows[i - 1]["section"] = lstsection.Text;

                        code.bind_all_ddl_with_id(lstCourse, "Select distinct CourseName, CourseID from Course_or_Subject_Master where CategoryID ='" + CourseName.SelectedValue + "' and section='" + lstsection.Text + "' and Istatus='1' order by CourseName");

                        dtCurrentTable.Rows[i - 1]["AssignCourseID"] = lstCourse.SelectedValue;
                        rowIndex++;


                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    GrdSub.DataSource = dtCurrentTable;
                    GrdSub.DataBind();
                    ViewState["SubInfo"] = dtCurrentTable;
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            SetPreviousData();
        }

        private void SetPreviousData()
        {
            int rowIndex = 0;
            if (ViewState["SubInfo"] != null)
            {
                DataTable dt = (DataTable)ViewState["SubInfo"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DropDownList CourseName = (DropDownList)GrdSub.Rows[rowIndex].Cells[0].FindControl("ddl_CourseCat");
                        DropDownList lstsection = (DropDownList)GrdSub.Rows[rowIndex].Cells[1].FindControl("ddl_section");
                        DropDownList lstCourse = (DropDownList)GrdSub.Rows[rowIndex].Cells[2].FindControl("ddlCourse");
                        code.bind_all_ddl_with_id(CourseName, "Select CategoryName, CategoryID from ClassMaster where Istatus='1' order by Position asc");
                        CourseName.SelectedValue = dt.Rows[i]["CategoryID"].ToString();

                        code.bind_ddl(lstsection, "Select distinct section  from Course_or_Subject_Master where CategoryID ='" + CourseName.SelectedValue + "' and Istatus='1' order by section");
                        lstsection.Text = dt.Rows[i]["section"].ToString();



                        code.bind_all_ddl_with_id(lstCourse, "Select distinct CourseName, CourseID from Course_or_Subject_Master where CategoryID ='" + CourseName.SelectedValue + "' and section='" + lstsection.Text + "' and Istatus='1' order by CourseName");
                        lstCourse.SelectedValue = dt.Rows[i]["AssignCourseID"].ToString();
                        rowIndex++;
                    }
                }

            }
        }
        #endregion

        private void send_Info()
        {
            try
            {
                int growcount = GrdSub.Rows.Count;
                int k = 0;
                if (growcount != 0)
                {
                    for (int i = 0; i < growcount; i++)
                    {
                        DropDownList ddl_Course = (DropDownList)GrdSub.Rows[i].FindControl("ddl_CourseCat");
                        DropDownList ddl_section = (DropDownList)GrdSub.Rows[i].FindControl("ddl_section");
                        DropDownList subject = (DropDownList)GrdSub.Rows[i].FindControl("ddlCourse");
                        if (ddl_Course.SelectedItem.Text != "Select" && subject.SelectedItem.Text != "Select")
                        {
                            sendItToDB(ddl_Course.SelectedValue, subject.SelectedValue, ddl_section.Text);
                        }
                        else
                        {
                            k++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }



        private void sendItToDB(string CourseID, string SubjectID, string section)
        {
            try
            {
                if (code.IsExist("Select * from TeacherCourseSubjectMaping where CategoryID='" + CourseID + "' and UserID='" + ddl_instructor.SelectedValue + "' and AssignCourseID='" + SubjectID + "' and section='" + section + "' "))
                {
                    SqlCommand cmd = new SqlCommand("insert into TeacherCourseSubjectMaping (UserID, AssignCourseID, Istatus,Date,Idate,CategoryID,section) " +
                                         "Values ('" + ddl_instructor.SelectedValue + "','" + SubjectID + "','1','" + code.date() + "','" + code.idate() + "', '" + CourseID + "','" + section + "')");
                    InsertUpdate.InsertUpdateData(cmd);
                    code.BindRepeater(" select I.*, (select CategoryName from ClassMaster where CategoryID=I.CategoryID) as CatName,(select CourseName from Course_or_Subject_Master where CourseID=I.AssignCourseID) as CourseName,(select Name from InstructorProfile where UserID=I.UserID) as UserName from TeacherCourseSubjectMaping I where I.CategoryID='" + ddl_CourseCat.SelectedValue + "' order by CatName Asc", RPDetails);
                    Alert("successfully assigned Subject.");
                }
                else { }
            }
            catch { }
        }

        protected void ddl_CourseCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList dropDownList = (DropDownList)sender;
                GridViewRow row = (GridViewRow)dropDownList.Parent.Parent;
                DropDownList listcourse = (DropDownList)row.FindControl("ddlCourse");
                DropDownList ddl_section = (DropDownList)row.FindControl("ddl_section");
                code.bind_ddl(ddl_section, "Select distinct section  from Course_or_Subject_Master where CategoryID ='" + dropDownList.SelectedValue + "'  order by section");
            }
            catch { }
        }



        protected void ddl_section_SelectedIndexChanged1(object sender, EventArgs e)
        {
            try
            {
                DropDownList dropDownList = (DropDownList)sender;
                GridViewRow row = (GridViewRow)dropDownList.Parent.Parent;
                DropDownList listcourse = (DropDownList)row.FindControl("ddlCourse");
                DropDownList ddl_CourseCat = (DropDownList)row.FindControl("ddl_CourseCat");
                code.bind_all_ddl_with_id(listcourse, "Select CourseName, CourseID from Course_or_Subject_Master where CategoryID ='" + ddl_CourseCat.SelectedValue + "' and section='" + dropDownList.Text + "' and Istatus='1' order by CourseName");
            }
            catch { }
        }

        protected void ddl_SearchCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_SearchCategory.SelectedItem.Text == "ALL")
            {
                ddl_section_serch.Text = "ALL";
                ddl_section_serch.Enabled = false;
            }
            else
            {
                ddl_section_serch.Enabled = true;
                code.bind_ddl_all1(ddl_section_serch, "Select distinct section  from Course_or_Subject_Master where CategoryID ='" + ddl_SearchCategory.SelectedValue + "'  order by section");
            }
        }




    }
}