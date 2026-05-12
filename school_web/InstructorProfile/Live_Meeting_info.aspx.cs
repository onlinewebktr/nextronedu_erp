using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.InstructorProfile
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
            if (Session["teacher"] == null)
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
                    ViewState["teacher"] = Session["teacher"].ToString();
                    txt_date.Text = code.date();
                    txt_end_date.Text = code.date();

                    code.bind_all_list_with_id(lst_Teacher, "select name as Name,user_id as  UserID from user_details where (User_Type='Teaccher' or User_Type='Principal')  order by Name  asc  ");

                    Pageload_data();

                }
            }
            if (!IsPostBack)
            {

            }
        }
        string query = "";
        private void Pageload_data()
        {
            query = "select zvcs.section,ip.user_id as UserID,ip.Name, IIF(ip.Istatus='0', 'Inactive', 'Active') as StatusD,zvcs.Id,zvcs.Date,zvcs.Topic,zvcs.Meeting_start_at,zvcs.End_Time,zvcs.CreatedOn,zvcs.Duration,zvcs.Zoom_id,zvcs.Start_Itime,zvcs.Status,zvcs.Zoom_Meeting_id,zvcs.Zoom_Metting_Password,ip.Zoom_Api_Sl_No,(Select top 1 Subject_name from Subject_Master where course_id=zvcs.Class and Subject_id=zvcs.Subject )as CourseName,(Select top 1 Course_Name from Add_course_table where course_id=zvcs.Class) as classname from user_details  ip left join Zoom_Virtual_class_schedule zvcs on ip.user_id=zvcs.Teacher_Id     where  format(zvcs.Meeting_start_at, 'yyyyMMdd')>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and format(zvcs.Meeting_start_at, 'yyyyMMdd')<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_end_date.Text)) + "  and zvcs.Teacher_Id='" + ViewState["teacher"].ToString() + "'  order by Id Desc";
            Bind_gridedata(query);

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

                    if (Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) <= Convert.ToInt32(code.ConvertStringToiDate(txt_end_date.Text)))
                    {

                        query = "select zvcs.section,ip.user_id as UserID,ip.Name, IIF(ip.Istatus='0', 'Inactive', 'Active') as StatusD,zvcs.Id,zvcs.Date,zvcs.Topic,zvcs.Meeting_start_at,zvcs.End_Time,zvcs.CreatedOn,zvcs.Duration,zvcs.Zoom_id,zvcs.Start_Itime,zvcs.Status,zvcs.Zoom_Meeting_id,zvcs.Zoom_Metting_Password,ip.Zoom_Api_Sl_No,(Select top 1 Subject_name from Subject_Master where course_id=zvcs.Class and Subject_id=zvcs.Subject )as CourseName,(Select top 1 Course_Name from Add_course_table where course_id=zvcs.Class) as classname from user_details  ip left join Zoom_Virtual_class_schedule zvcs on ip.user_id=zvcs.Teacher_Id    where  format(zvcs.Meeting_start_at, 'yyyyMMdd')>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and format(zvcs.Meeting_start_at, 'yyyyMMdd')<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_end_date.Text)) + "  and zvcs.Teacher_Id='" + ViewState["teacher"].ToString() + "'  order by Id Desc";
                        Bind_gridedata(query);
                    }

                    else
                    {
                        Alert("Please select date valid");
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