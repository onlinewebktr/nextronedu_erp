using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using school_web.AppCode;
namespace school_web.LMS_VC_Admin
{
    public partial class Live_Meeting_info : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
        public void Alert(string Message)
        {
            lbl_msg.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }
        UsesCode mycode = new UsesCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txt_date.Text = code.date();
                code.bind_ddl(ddl_zoomsl, "Select distinct sl_no from Zoom_API order by sl_no asc ");
                code.bind_all_list_with_id(lst_Teacher, "select name as Name,user_id as  UserID from user_details where (User_Type='Teacher' or User_Type='Principal')  order by Name  asc");
              //  code.bind_ddl(ddl_zoomsl, "Select distinct sl_no from Zoom_API order by sl_no asc ");
                // bind_teacher_list();
                Pageload_data();
            }
        }
        string query = "";
        private void Pageload_data()
        {
            query = "select zvcs.section,ip.user_id as UserID,ip.Name, IIF(ip.Istatus='0', 'Inactive', 'Active') as StatusD,zvcs.Id,zvcs.Date,zvcs.Topic,zvcs.Meeting_start_at,zvcs.End_Time,zvcs.CreatedOn,zvcs.Duration,zvcs.Zoom_id,zvcs.Start_Itime,zvcs.Status,zvcs.Zoom_Meeting_id,zvcs.Zoom_Metting_Password,ip.Zoom_Api_Sl_No,(Select top 1 Subject_name from Subject_Master where course_id=zvcs.Class and Subject_id=zvcs.Subject )as CourseName,(Select top 1 Course_Name from Add_course_table where course_id=zvcs.Class) as classname from user_details  ip left join Zoom_Virtual_class_schedule zvcs on ip.user_id=zvcs.Teacher_Id     where  format(zvcs.Meeting_start_at, 'dd/MM/yyyy')='" + code.date() + "'  order by Id Desc";
            Bind_gridedata(query);

        }

        private void bind_teacher_list()
        {
            string query = "select name as Name,user_id as  UserID from user_details where (User_Type='Teacher' or User_Type='Principal')  order by Name  asc";

            DataTable dt = mycode.FillTable(query);
            if (dt.Rows.Count == 0)
            {


                datl_teacherlist.DataSource = null;
                datl_teacherlist.DataBind();

            }
            else
            {
                datl_teacherlist.DataSource = dt;
                datl_teacherlist.DataBind();
            }
        }

        protected void btn_search_Click(object sender, EventArgs e)
        {
            if (txt_date.Text == "")
            {
                Alert("PLease enter date");
            }
            else
            {
                try
                {
                    string userid = "";
                    string userid1 = "";
                    int count = datl_teacherlist.Items.Count;
                    //for (int i = 0; i < count; i++)
                    //{
                    //    Label lbl_UserID = datl_teacherlist.Items[i].FindControl("lbl_UserID") as Label;
                    //    CheckBox chk = datl_teacherlist.Items[i].FindControl("chk_subjectname") as CheckBox;
                    foreach (ListItem item in lst_Teacher.Items)
                    {
                        if (item.Selected)
                        {
                            //if (chk.Checked == true)
                            //{
                            if (userid == "")
                            {
                                userid = "'" + item.Value + "'";
                            }
                            else
                            {
                                userid = userid + "," + "'" + item.Value + "'";

                            }
                        }


                    }

                    if (userid == "")
                    {
                        query = "select zvcs.section,ip.user_id as UserID,ip.Name, IIF(ip.Istatus='0', 'Inactive', 'Active') as StatusD,zvcs.Id,zvcs.Date,zvcs.Topic,zvcs.Meeting_start_at,zvcs.End_Time,zvcs.CreatedOn,zvcs.Duration,zvcs.Zoom_id,zvcs.Start_Itime,zvcs.Status,zvcs.Zoom_Meeting_id,zvcs.Zoom_Metting_Password,ip.Zoom_Api_Sl_No,(Select top 1 Subject_name from Subject_Master where course_id=zvcs.Class and Subject_id=zvcs.Subject )as CourseName,(Select top 1 Course_Name from Add_course_table where course_id=zvcs.Class) as classname from user_details  ip left join Zoom_Virtual_class_schedule zvcs on ip.user_id=zvcs.Teacher_Id     where   format(zvcs.Meeting_start_at, 'dd/MM/yyyy')='" + txt_date.Text + "'  order by Id Desc";
                        Bind_gridedata(query);
                    }
                    else
                    {

                        query = "select zvcs.section,ip.user_id as UserID,ip.Name, IIF(ip.Istatus='0', 'Inactive', 'Active') as StatusD,zvcs.Id,zvcs.Date,zvcs.Topic,zvcs.Meeting_start_at,zvcs.End_Time,zvcs.CreatedOn,zvcs.Duration,zvcs.Zoom_id,zvcs.Start_Itime,zvcs.Status,zvcs.Zoom_Meeting_id,zvcs.Zoom_Metting_Password,ip.Zoom_Api_Sl_No,(Select top 1 Subject_name from Subject_Master where course_id=zvcs.Class and Subject_id=zvcs.Subject )as CourseName,(Select top 1 Course_Name from Add_course_table where course_id=zvcs.Class) as classname from user_details  ip left join Zoom_Virtual_class_schedule zvcs on ip.user_id=zvcs.Teacher_Id     where ip.UserID in (" + userid + ") and format(zvcs.Meeting_start_at, 'dd/MM/yyyy')='" + txt_date.Text + "'  order by Id Desc";
                        Bind_gridedata(query);
                    }


                }
                catch
                {
                }
            }

        }

        private void Bind_gridedata(string query)
        {
            try
            {
                ViewState["query"] = query;

                DataTable dt = code.FillTable(query);
                if (dt.Rows.Count == 0)
                {
                    lnk_excel_download.Visible = false;
                    Alert(" There is no vc class list  available");
                    RpDetailsStudent.DataSource = null;
                    RpDetailsStudent.DataBind();
                    ViewState["Data"] = null;
                }
                else
                {
                    ViewState["Data"] = dt;
                    lnk_excel_download.Visible = true;
                    RpDetailsStudent.DataSource = dt;
                    RpDetailsStudent.DataBind();
                }
            }
            catch (Exception ex)
            {
                RpDetailsStudent.DataSource = null;
                RpDetailsStudent.DataBind();
                ViewState["Data"] = null;
                UsesCode.submitexception(ex.ToString());
            }

        }
        protected void lnk_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton img = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)img.NamingContainer;
                Label lbl_teacherid = (Label)row.FindControl("lbl_teacherid");
                Label lvl_vcid = (Label)row.FindControl("lvl_vcid");
                UsesCode.exeSql("delete from Zoom_Virtual_class_schedule where Id='" + lvl_vcid.Text + "'");
                Alert("Data has been deleted successfully");
                Bind_gridedata(ViewState["query"].ToString());
            }
            catch (Exception ex)
            {
                UsesCode.submitexception(ex.ToString());
            }
        }
     

       

        

       

       

        #region update zoom id
        protected void btn_update_zoomid_Click(object sender, EventArgs e)
        {
            if (ddl_zoomsl.Text == "Select")
            {
                Alert("Please select zoom id");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            else
            {
                UsesCode.exeSql("update user_details set  Zoom_Api_Sl_No= '" + ddl_zoomsl.Text + "' where user_id='" + hd_teacheruserid.Value + "'");
                Alert("Zoom Sl. No. has been updated");
                Bind_gridedata(ViewState["query"].ToString());

            }

        }
        protected void lnkedit_zoom_Click(object sender, EventArgs e)// edit  zoom sl id teacher profile
        {

            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_teacherid = (Label)row.FindControl("lbl_teacherid");
                Label lbl_Zoom_Api_Sl_No = (Label)row.FindControl("lbl_Zoom_Api_Sl_No");
                Label lbl_Name = (Label)row.FindControl("lbl_teacherName1");
                hd_teacheruserid.Value = lbl_teacherid.Text;
                lbl_teachername.Text = lbl_Name.Text;
                lbl_old_zoomid.Text = lbl_Zoom_Api_Sl_No.Text;
                lbluserid.Text = lbl_teacherid.Text;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch { }
        }
        #endregion
 

        #region edit pwd zoom
        protected void btn_change_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_zmid_edit.Text == "")
                {
                    Alert("Please enter zm id");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                else if (txt_zmpwd_edit.Text == "")
                {
                    Alert("Please enter zm pwd");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                else if (ddl_status.Text == "Select")
                {
                    Alert("Please select status");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                else
                {
                    SqlCommand cmd;
                    string strQuery = "Update     Zoom_Virtual_class_schedule  set Status=@Status,Zoom_Meeting_id=@Zoom_Meeting_id,Zoom_Metting_Password=@Zoom_Metting_Password where Teacher_Id=@Teacher_Id and Id=@Id";
                    cmd = new SqlCommand(strQuery);
                    cmd.Parameters.AddWithValue("@Status", ddl_status.Text);
                    cmd.Parameters.AddWithValue("@Zoom_Meeting_id", txt_zmid_edit.Text);
                    cmd.Parameters.AddWithValue("@Zoom_Metting_Password", txt_zmpwd_edit.Text);

                    cmd.Parameters.AddWithValue("@Teacher_Id", hd_teacherid_edit.Value);
                    cmd.Parameters.AddWithValue("@Id", hd_tacher_cell_id_edit.Value);
                    if (InsertUpdate.InsertUpdateData(cmd))
                    {
                        Alert("ZM Id has been update");
                        Bind_gridedata(ViewState["query"].ToString());

                        txt_zmid_edit.Text = "";
                        txt_zmpwd_edit.Text = "";
                        ddl_status.Text = "Select";
                    }

                }
            }
            catch (Exception ex)
            {
                UsesCode.submitexception(ex.ToString());
            }
        }

        protected void lnk_edit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton img = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)img.NamingContainer;
                Label lbl_teacherid = (Label)row.FindControl("lbl_teacherid");
                Label lbl_teacherName = (Label)row.FindControl("lbl_teacherName1");
                Label lbl_status = (Label)row.FindControl("lbl_status");
                Label lbl_zmid = (Label)row.FindControl("lbl_zmid");
                Label lbl_zmpwd = (Label)row.FindControl("lbl_zmpwd");

                Label lvl_vcid = (Label)row.FindControl("lvl_vcid");
                hd_tacher_cell_id_edit.Value = lvl_vcid.Text;

                lbl_teachername_edit_m.Text = lbl_teacherName.Text;
                lbl_userid_teacher_edit_m.Text = lbl_teacherid.Text;
                txt_zmid_edit.Text = lbl_zmid.Text;
                txt_zmpwd_edit.Text = lbl_zmpwd.Text;
                hd_teacherid_edit.Value = lbl_teacherid.Text;


                ddl_status.Text = lbl_status.Text;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);



            }
            catch (Exception ex)
            {
                UsesCode.submitexception(ex.ToString());
            }
        }
        #endregion

        protected void RpDetailsStudent_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {

                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {

                    if (((Label)e.Item.FindControl("lbl_status")).Text == "Approved")
                    {

                        ((Label)e.Item.FindControl("lbl_status")).CssClass = "badge badge-success ml-2";


                    }
                    else
                    {

                        ((Label)e.Item.FindControl("lbl_status")).CssClass = "badge badge-danger ml-2";
                    }



                }
            }
            catch { }
        }

    }
}