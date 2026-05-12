using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class Student_complain_list : System.Web.UI.Page
    {
        UsesCode mycode = new UsesCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Admin"] != null)
                {
                    ViewState["Admin"] = Session["Admin"].ToString();
                    string pagename_current = "Student_complain_list.aspx";
                    Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Admin"].ToString(), pagename_current);
                    ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                    ViewState["Is_delete"] = (String)dc1["Is_delete"];
                    ViewState["Is_Download"] = (String)dc1["Is_Download"];
                    ViewState["Is_Print"] = (String)dc1["Is_Print"];
                    ViewState["Is_add"] = (String)dc1["Is_add"];
                    mycode.bind_all_ddl_with_all(ddl_class, "Select Course_Name, course_id from Add_course_table order by Position");
                    mycode.bind_ddl_all1(dd_section, "Select distinct Section  from admission_registor  order by Section");
                    BindGrid_pending();
                }
                else
                {
                    Session.Abandon();
                    Session.Clear();
                    Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
                    Response.Write("<script language=javascript>wnd.close();</script>");
                    Response.Write("<script language=javascript>window.open('../Default.aspx','_parent',replace=true);</script>");
                }
            }
        }
        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_class.SelectedItem.Text == "ALL")
            {
                mycode.bind_ddl_all1(dd_section, "Select Course_Name, course_id from Add_course_table order by Position");
                dd_section.Text = "ALL";
            }
            else
            {
                mycode.bind_ddl_all1(dd_section, "Select distinct Section  from admission_registor where Class_id ='" + ddl_class.SelectedValue + "'    order by Section");
            }

        }
        private void BindGrid_pending()// pending
        {
            pnl_view.Visible = true;
            if (rd_all.Checked == true)
            {
                string sql = @"select (select count(Id) from Complain_feedback_chat where Request_id=t1.Request_id and ShowStatus is null) as msgCount,t1.*,t2.studentname as Full_name,t2.mobilenumber as Mobile_no,t2.class,t2.rollnumber,t2.Section,t2.admissionserialnumber,'hidden' as HideShow  from Complain_feedback t1 join admission_registor t2 on t1.User_id=t2.admissionserialnumber where t1.Status=0 and t1.Type=1 and  t2.Transfer_Status in('New','NT')  order by t1.Last_reply_idate desc; ";
                sql = sql + "select  Status,count(Id) as total  from Complain_feedback t1 where   Type=1    group by Status; ";

                bind_req(sql);

            }
            else if (rd_Twodate_wise.Checked == true)
            {
                if (Convert.ToInt32(mycode.ConvertStringToiDate(txt_startdate.Text)) <= Convert.ToInt32(mycode.ConvertStringToiDate(txt_enddate.Text)))
                {

                    string sql = @"select (select count(Id) from Complain_feedback_chat where Request_id=t1.Request_id and ShowStatus is null) as msgCount,t1.*,t2.studentname as Full_name,t2.mobilenumber as Mobile_no,t2.class,t2.rollnumber,t2.Section,t2.admissionserialnumber,'hidden' as HideShow  from Complain_feedback t1 join admission_registor t2 on t1.User_id=t2.admissionserialnumber where t1.Status=0 and Type=1 and  t1.Created_idate>=" + mycode.ConvertStringToiDate(txt_startdate.Text) + " and t1.Created_idate<=" + mycode.ConvertStringToiDate(txt_enddate.Text) + "  and  t2.Transfer_Status in('New','NT')  order by t1.Last_reply_idate desc; ";
                    sql = sql + "select  Status,count(Id) as total  from Complain_feedback t1 where  t1.Created_idate>=" + mycode.ConvertStringToiDate(txt_startdate.Text) + " and t1.Created_idate<=" + mycode.ConvertStringToiDate(txt_enddate.Text) + "   and Type=1  group by Status; ";
                    bind_req(sql);


                }
                else
                {
                    Alert("Please select valid date", "success");
                }
            }

            else if (rd_class_and_section_wise.Checked == true)
            {
                if (Convert.ToInt32(mycode.ConvertStringToiDate(txt_start_date_cl.Text)) <= Convert.ToInt32(mycode.ConvertStringToiDate(txt_end_date_cl.Text)))
                {
                    if (ddl_class.SelectedItem.Text == "ALL" && dd_section.Text == "ALL")
                    {
                        string sql = @"select (select count(Id) from Complain_feedback_chat where Request_id=t1.Request_id and ShowStatus is null) as msgCount,t1.*,t2.studentname as Full_name,t2.mobilenumber as Mobile_no,t2.class,t2.rollnumber,t2.Section,t2.admissionserialnumber,'hidden' as HideShow  from Complain_feedback t1 join admission_registor t2 on t1.User_id=t2.admissionserialnumber where t1.Status=0 and Type=1 and  t1.Created_idate>=" + mycode.ConvertStringToiDate(txt_start_date_cl.Text) + " and t1.Created_idate<=" + mycode.ConvertStringToiDate(txt_end_date_cl.Text) + "  and  t2.Transfer_Status in('New','NT')  order by t1.Last_reply_idate desc; ";
                        sql = sql + "select  Status,count(Id) as total  from Complain_feedback t1 where  t1.Created_idate>=" + mycode.ConvertStringToiDate(txt_start_date_cl.Text) + " and t1.Created_idate<=" + mycode.ConvertStringToiDate(txt_end_date_cl.Text) + " and  Type=1  group by Status; ";
                        bind_req(sql);

                    }

                    else if (ddl_class.SelectedItem.Text != "ALL" && dd_section.Text == "ALL")
                    {

                        string sql = @"select (select count(Id) from Complain_feedback_chat where Request_id=t1.Request_id and ShowStatus is null) as msgCount,t1.*,t2.studentname as Full_name,t2.mobilenumber as Mobile_no,t2.class,t2.rollnumber,t2.Section,t2.admissionserialnumber,'hidden' as HideShow  from Complain_feedback t1 join admission_registor t2 on t1.User_id=t2.admissionserialnumber where t1.Status=0 and Type=1 and  t1.Created_idate>=" + mycode.ConvertStringToiDate(txt_start_date_cl.Text) + " and t1.Created_idate<=" + mycode.ConvertStringToiDate(txt_end_date_cl.Text) + "  and t2.Class_id='" + ddl_class.SelectedValue + "'  and  t2.Transfer_Status in('New','NT') order by t1.Last_reply_idate desc; ";
                        sql = sql + "select  Status,count(Id) as total  from Complain_feedback t1 where  t1.Created_idate>=" + mycode.ConvertStringToiDate(txt_start_date_cl.Text) + " and t1.Created_idate<=" + mycode.ConvertStringToiDate(txt_end_date_cl.Text) + " and   t2.Class_id='" + ddl_class.SelectedValue + "'  and Type=1   group by Status; ";
                        bind_req(sql);

                    }

                    else if (ddl_class.SelectedItem.Text == "ALL" && dd_section.Text != "ALL")
                    {

                        string sql = @"select (select count(Id) from Complain_feedback_chat where Request_id=t1.Request_id and ShowStatus is null) as msgCount,t1.*,t2.studentname as Full_name,t2.mobilenumber as Mobile_no,t2.class,t2.rollnumber,t2.Section,t2.admissionserialnumber,'hidden' as HideShow  from Complain_feedback t1 join admission_registor t2 on t1.User_id=t2.admissionserialnumber where t1.Status=0 and Type=1 and  t1.Created_idate>=" + mycode.ConvertStringToiDate(txt_start_date_cl.Text) + " and t1.Created_idate<=" + mycode.ConvertStringToiDate(txt_end_date_cl.Text) + " and t2.Section='" + dd_section.Text + "'  and  t2.Transfer_Status in('New','NT') order by t1.Last_reply_idate desc; ";
                        sql = sql + "select  Status,count(Id) as total  from Complain_feedback t1 where  t1.Created_idate>=" + mycode.ConvertStringToiDate(txt_start_date_cl.Text) + " and t1.Created_idate<=" + mycode.ConvertStringToiDate(txt_end_date_cl.Text) + " and  t2.Section='" + dd_section.Text + "'   and Type=1   group by Status; ";
                        bind_req(sql);




                    }
                    else if (ddl_class.SelectedItem.Text != "ALL" && dd_section.Text != "ALL")
                    {
                        string sql = @"select (select count(Id) from Complain_feedback_chat where Request_id=t1.Request_id and ShowStatus is null) as msgCount,t1.*,t2.studentname as Full_name,t2.mobilenumber as Mobile_no,t2.class,t2.rollnumber,t2.Section,t2.admissionserialnumber,'hidden' as HideShow  from Complain_feedback t1 join admission_registor t2 on t1.User_id=t2.admissionserialnumber where t1.Status=0 and Type=1 and  t1.Created_idate>=" + mycode.ConvertStringToiDate(txt_start_date_cl.Text) + " and t1.Created_idate<=" + mycode.ConvertStringToiDate(txt_end_date_cl.Text) + "  and t2.Section='" + dd_section.Text + "' and t2.Class_id='" + ddl_class.SelectedValue + "'  and  t2.Transfer_Status in('New','NT') order by t1.Last_reply_idate desc; ";
                        sql = sql + "select Status,count(Id) as total  from Complain_feedback t1 where  t1.Created_idate>=" + mycode.ConvertStringToiDate(txt_start_date_cl.Text) + " and t1.Created_idate<=" + mycode.ConvertStringToiDate(txt_end_date_cl.Text) + " and  t2.Section='" + dd_section.Text + "' and t2.Class_id='" + ddl_class.SelectedValue + "'   and Type=1   group by Status; ";
                        bind_req(sql);

                    }

                }
                else
                {
                    Alert("Please select valid date", "success");
                }


            }
            else if (rd_Admission_no_wise.Checked == true)
            {
                if (txt_admissiono.Text == "")
                {
                    Alert("Please enter valid admission no.", "success");
                }
                else
                {
                    string sql = @"select (select count(Id) from Complain_feedback_chat where Request_id=t1.Request_id and ShowStatus is null) as msgCount,t1.*,t2.studentname as Full_name,t2.mobilenumber as Mobile_no,t2.class,t2.rollnumber,t2.Section,t2.admissionserialnumber,'hidden' as HideShow  from Complain_feedback t1 join admission_registor t2 on t1.User_id=t2.admissionserialnumber where t1.Status=0 and Type=1 and  t2.admissionserialnumber='" + txt_admissiono.Text + "'  and  t2.Transfer_Status in('New','NT') ; ";
                    sql = sql + "select  t1.Status,count(t1.Id) as total  from Complain_feedback t1 where  t1.User_id='" + txt_admissiono.Text + "' and   t1.Type=1   group by t1.Status; ";
                    bind_req(sql);



                }
            }

        }

        private void bind_req(string sql)
        {
            Session["sql"] = sql;
            //DataTable dt = mycode.FillTable(sql);
            DataSet ds = mycode.Fill_Data_set(sql);
            DataTable dt = ds.Tables[0];
            mycode.RptrData(dt, RPDetails);
            if (dt.Rows.Count.ToString() != "0")
            {
                pnl_view.Visible = true;
                // bind_count();
                if (dt.Rows[0]["HideShow"].ToString() == "hidden")
                {
                    closedDate.Visible = false;
                    closedDate.Attributes.Add("class", "hidden");
                }
                else
                {
                    closedDate.Visible = true;
                    closedDate.Attributes.Add("class", "show");
                }

                lbl_total_data.Text = dt.Rows.Count.ToString();
                if (ds.Tables[1].Rows.Count > 0)
                    dt = ds.Tables[1];
                lbl_pending_comp.Text = "0";
                lbl_closed_comp.Text = "0";
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["Status"].ToString() == "0")
                    {
                        lbl_pending_comp.Text = dr["total"].ToString();
                    }
                    else
                    {
                        lbl_closed_comp.Text = dr["total"].ToString();
                    }
                }
            }





            else
            {
                pnl_view.Visible = true;
                lbl_total_data.Text = "0";
                lbl_pending_comp.Text = "0";
                lbl_closed_comp.Text = "0";
            }
        }

        private void bind_count()
        {
            string sql = @"select count (case when Status = 1 then Id End) as Closed,  count (case when Status = 0 then Id End) Pending, (count (case when Status = 1 then Id End) + count (case when Status = 0 then Id End)) as Total  from Complain_feedback where Type=1";
            DataTable dt = mycode.FillTable(sql);
            lbl_total_data.Text = dt.Rows[0]["Total"].ToString();
            lbl_pending_comp.Text = dt.Rows[0]["Pending"].ToString();
            lbl_closed_comp.Text = dt.Rows[0]["Closed"].ToString();
        }

        protected void RPDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (((Label)e.Item.FindControl("lbl_Is_related_with_order")).Text == "1")
                {
                    ((Label)e.Item.FindControl("lbl_related_with_order")).Text = "Yes";
                    ((Label)e.Item.FindControl("lbl_order_id")).Visible = true;
                    ((Label)e.Item.FindControl("lbl_show_order_id")).Visible = false;
                }
                else
                {
                    ((Label)e.Item.FindControl("lbl_related_with_order")).Text = "No";
                    ((Label)e.Item.FindControl("lbl_order_id")).Visible = false;
                    ((Label)e.Item.FindControl("lbl_show_order_id")).Visible = true;
                }

                if (((Label)e.Item.FindControl("lbl_status")).Text == "1")
                {
                    ((LinkButton)e.Item.FindControl("btn_go_to_chat")).CssClass = "lnk-green";
                }
                else
                {
                    ((LinkButton)e.Item.FindControl("btn_go_to_chat")).CssClass = "lnk-red";
                }

                if (((Label)e.Item.FindControl("lbl_msg_count")).Text == "0")
                {
                    ((Label)e.Item.FindControl("lbl_unread_message")).Visible = false;
                }
                else
                {
                    ((Label)e.Item.FindControl("lbl_unread_message")).Visible = true;
                }
            }
        }

        protected void btn_go_to_chat_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_add"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_req_id = (Label)row.FindControl("lbl_req_id");
                    Label lbl_User_id = (Label)row.FindControl("lbl_User_id");
                    Response.Redirect("chat.aspx?reqid=" + lbl_req_id.Text + "&studentid=" + lbl_User_id.Text, false);
                }
                else if (ViewState["Is_Edit"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_req_id = (Label)row.FindControl("lbl_req_id");
                    Label lbl_User_id = (Label)row.FindControl("lbl_User_id");
                    Response.Redirect("chat.aspx?reqid=" + lbl_req_id.Text + "&studentid=" + lbl_User_id.Text, false);
                }
                else
                {
                    Alert(My.get_restricted_message(), "warning");
                }
               
            }
            catch { }
        }
        protected void rd_pendin_req_CheckedChanged1(object sender, EventArgs e)
        {
            try
            {
                BindGrid_pending();
            }
            catch (Exception ex)
            {
            }
        }

        protected void rd_closed_req_CheckedChanged1(object sender, EventArgs e)
        {
            BindGrid_closed();
        }



        private void BindGrid_closed()
        {
            if (rd_all.Checked == true)
            {
                string sql = @"select (select count(Id) from Complain_feedback_chat where Request_id=t1.Request_id and ShowStatus is null) as msgCount,t1.*,t2.studentname as Full_name,t2.mobilenumber as Mobile_no,t2.class,t2.rollnumber,t2.Section,t2.admissionserialnumber,'show' as HideShow  from Complain_feedback t1 join admission_registor t2 on t1.User_id=t2.admissionserialnumber where t1.Status=1 and Type=1  and  t2.Transfer_Status in('New','NT') order by t1.Last_reply_idate desc; ";
                sql = sql + "select  Status,count(Id) as total  from Complain_feedback t1  where  Type=1    group by Status; ";

                bind_req(sql);


            }
            else if (rd_Twodate_wise.Checked == true)
            {
                if (Convert.ToInt32(mycode.ConvertStringToiDate(txt_startdate.Text)) <= Convert.ToInt32(mycode.ConvertStringToiDate(txt_enddate.Text)))
                {

                    string sql = @"select (select count(Id) from Complain_feedback_chat where Request_id=t1.Request_id and ShowStatus is null) as msgCount,t1.*,t2.studentname as Full_name,t2.mobilenumber as Mobile_no,t2.class,t2.rollnumber,t2.Section,t2.admissionserialnumber,'show' as HideShow  from Complain_feedback t1 join admission_registor t2 on t1.User_id=t2.admissionserialnumber where t1.Status=1 and Type=1 and  t1.Closed_idate>=" + mycode.ConvertStringToiDate(txt_startdate.Text) + " and t1.Closed_idate<=" + mycode.ConvertStringToiDate(txt_enddate.Text) + "  and  t2.Transfer_Status in('New','NT')  order by t1.Last_reply_idate desc; ";
                    sql = sql + "select  Status,count(Id) as total  from Complain_feedback t1 where  t1.Closed_idate>=" + mycode.ConvertStringToiDate(txt_startdate.Text) + " and t1.Closed_idate<=" + mycode.ConvertStringToiDate(txt_enddate.Text) + "   and Type=1  group by Status; ";
                    bind_req(sql);


                }
                else
                {
                    Alert("Please select valid date", "success");
                }
            }

            else if (rd_class_and_section_wise.Checked == true)
            {
                if (Convert.ToInt32(mycode.ConvertStringToiDate(txt_start_date_cl.Text)) <= Convert.ToInt32(mycode.ConvertStringToiDate(txt_end_date_cl.Text)))
                {
                    if (ddl_class.SelectedItem.Text == "ALL" && dd_section.Text == "ALL")
                    {
                        string sql = @"select (select count(Id) from Complain_feedback_chat where Request_id=t1.Request_id and ShowStatus is null) as msgCount,t1.*,t2.studentname as Full_name,t2.mobilenumber as Mobile_no,t2.class,t2.rollnumber,t2.Section,t2.admissionserialnumber,'show' as HideShow  from Complain_feedback t1 join admission_registor t2 on t1.User_id=t2.admissionserialnumber where t1.Status=1 and Type=1 and  t1.Closed_idate>=" + mycode.ConvertStringToiDate(txt_start_date_cl.Text) + " and t1.Closed_idate<=" + mycode.ConvertStringToiDate(txt_end_date_cl.Text) + "  and  t2.Transfer_Status in('New','NT')  order by t1.Last_reply_idate desc; ";
                        sql = sql + "select  Status,count(Id) as total  from Complain_feedback t1 where  t1.Closed_idate>=" + mycode.ConvertStringToiDate(txt_start_date_cl.Text) + " and t1.Closed_idate<=" + mycode.ConvertStringToiDate(txt_end_date_cl.Text) + "     and Type=1   group by Status; ";
                        bind_req(sql);

                    }

                    else if (ddl_class.SelectedItem.Text != "ALL" && dd_section.Text == "ALL")
                    {

                        string sql = @"select (select count(Id) from Complain_feedback_chat where Request_id=t1.Request_id and ShowStatus is null) as msgCount,t1.*,t2.studentname as Full_name,t2.mobilenumber as Mobile_no,t2.class,t2.rollnumber,t2.Section,t2.admissionserialnumber,'show' as HideShow  from Complain_feedback t1 join admission_registor t2 on t1.User_id=t2.admissionserialnumber where t1.Status=1 and Type=1 and  t1.Closed_idate>=" + mycode.ConvertStringToiDate(txt_start_date_cl.Text) + " and t1.Created_idate<=" + mycode.ConvertStringToiDate(txt_end_date_cl.Text) + "  and t2.Closed_idate='" + ddl_class.SelectedValue + "'  and  t2.Transfer_Status in('New','NT') order by t1.Last_reply_idate desc; ";
                        sql = sql + "select  Status,count(Id) as total  from Complain_feedback_chat t1 where  t1.Closed_idate>=" + mycode.ConvertStringToiDate(txt_start_date_cl.Text) + " and t1.Closed_idate<=" + mycode.ConvertStringToiDate(txt_end_date_cl.Text) + " and   t2.Class_id='" + ddl_class.SelectedValue + "'  and Type=1  group by Status; ";
                        bind_req(sql);

                    }

                    else if (ddl_class.SelectedItem.Text == "ALL" && dd_section.Text != "ALL")
                    {

                        string sql = @"select (select count(Id) from Complain_feedback_chat where Request_id=t1.Request_id and ShowStatus is null) as msgCount,t1.*,t2.studentname as Full_name,t2.mobilenumber as Mobile_no,t2.class,t2.rollnumber,t2.Section,t2.admissionserialnumber,'show' as HideShow  from Complain_feedback t1 join admission_registor t2 on t1.User_id=t2.admissionserialnumber where t1.Status=1 and Type=1 and  t1.Closed_idate>=" + mycode.ConvertStringToiDate(txt_start_date_cl.Text) + " and t1.Closed_idate<=" + mycode.ConvertStringToiDate(txt_end_date_cl.Text) + " and t2.Section='" + dd_section.Text + "'  and  t2.Transfer_Status in('New','NT') order by t1.Last_reply_idate desc; ";
                        sql = sql + "select  Status,count(Id) as total  from Complain_feedback_chat t1 where  t1.Closed_idate>=" + mycode.ConvertStringToiDate(txt_start_date_cl.Text) + " and t1.Closed_idate<=" + mycode.ConvertStringToiDate(txt_end_date_cl.Text) + " and  t2.Section='" + dd_section.Text + "' and Type=1  group by Status; ";
                        bind_req(sql);




                    }
                    else if (ddl_class.SelectedItem.Text != "ALL" && dd_section.Text != "ALL")
                    {
                        string sql = @"select (select count(Id) from Complain_feedback_chat where Request_id=t1.Request_id and ShowStatus is null) as msgCount,t1.*,t2.studentname as Full_name,t2.mobilenumber as Mobile_no,t2.class,t2.rollnumber,t2.Section,t2.admissionserialnumber,'show' as HideShow  from Complain_feedback t1 join admission_registor t2 on t1.User_id=t2.admissionserialnumber where t1.Status=1 and Type=1 and  t1.Closed_idate>=" + mycode.ConvertStringToiDate(txt_start_date_cl.Text) + " and t1.Closed_idate<=" + mycode.ConvertStringToiDate(txt_end_date_cl.Text) + "  and t2.Section='" + dd_section.Text + "' and t2.Class_id='" + ddl_class.SelectedValue + "'  and  t2.Transfer_Status in('New','NT') order by t1.Last_reply_idate desc; ";
                        sql = sql + "select  Status,count(Id) as total  from Complain_feedback_chat t1 where  t1.Closed_idate>=" + mycode.ConvertStringToiDate(txt_start_date_cl.Text) + " and t1.Closed_idate<=" + mycode.ConvertStringToiDate(txt_end_date_cl.Text) + " and  t2.Section='" + dd_section.Text + "' and t2.Class_id='" + ddl_class.SelectedValue + "' and  Type=1   group by Status; ";
                        bind_req(sql);

                    }

                }
                else
                {
                    Alert("Please select valid date", "success");
                }


            }
            else if (rd_Admission_no_wise.Checked == true)
            {
                if (txt_admissiono.Text == "")
                {
                    Alert("Please enter valid admission no.", "success");
                }
                else
                {
                    string sql = @"select (select count(Id) from Complain_feedback_chat where Request_id=t1.Request_id and ShowStatus is null) as msgCount,t1.*,t2.studentname as Full_name,t2.mobilenumber as Mobile_no,t2.class,t2.rollnumber,t2.Section,t2.admissionserialnumber,'show' as HideShow  from Complain_feedback t1 join admission_registor t2 on t1.User_id=t2.admissionserialnumber where t1.Status=1 and Type=1 and  t2.admissionserialnumber='" + txt_admissiono.Text + "'  and  t2.Transfer_Status in('New','NT') order by t1.Last_reply_idate desc; ";
                    sql = sql + "select  t1.Status,count(t1.Id) as total  from Complain_feedback t1 where   t1.User_id='" + txt_admissiono.Text + "' and Type=1   group by t1.Status; ";
                    bind_req(sql);




                }
            }
        }

        protected void rd_all_CheckedChanged(object sender, EventArgs e)
        {
            if (rd_pendin_req.Checked == true)
            {
                BindGrid_pending();// pending
            }
            else
            {
                BindGrid_closed();
            }

            btn_find.Visible = false;
            pnl_Twodate_wise.Visible = false;
            pnl_class_and_section_wise.Visible = false;
            pnl_admission_report_wise.Visible = false;
        }

        protected void rd_Twodate_wise_CheckedChanged(object sender, EventArgs e)
        {
            pnl_Twodate_wise.Visible = true;
            pnl_class_and_section_wise.Visible = false;
            pnl_admission_report_wise.Visible = false;
            pnl_view.Visible = false;

            RPDetails.DataSource = null;
            RPDetails.DataBind();
            btn_find.Visible = true;

        }

        protected void rd_class_and_section_wise_CheckedChanged(object sender, EventArgs e)
        {
            pnl_Twodate_wise.Visible = false;
            pnl_class_and_section_wise.Visible = true;
            pnl_admission_report_wise.Visible = false;
            pnl_view.Visible = false;
            btn_find.Visible = true;
            RPDetails.DataSource = null;
            RPDetails.DataBind();
        }

        protected void rd_Admission_no_wise_CheckedChanged(object sender, EventArgs e)
        {
            pnl_Twodate_wise.Visible = false;
            pnl_class_and_section_wise.Visible = false;
            pnl_admission_report_wise.Visible = true;
            pnl_view.Visible = false;
            btn_find.Visible = true;
            RPDetails.DataSource = null;
            RPDetails.DataBind();
        }


        private void search_data()
        {
            if (rd_pendin_req.Checked == true)
            {
                BindGrid_pending();// pending
            }
            else
            {
                BindGrid_closed();//closed
            }
        }


        string scrpt;
        private void Alert(string msg, string panel)
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
        protected void btn_find_Click(object sender, EventArgs e)
        {
            search_data();
        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_delete"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    GridViewRow row = (GridViewRow)lnk.Parent.Parent;
                    Label lbl_req_id = (Label)row.FindControl("lbl_req_id");
                    mycode.executequery("delete from Complain_feedback_chat where Request_id='" + lbl_req_id.Text + "'");
                    mycode.executequery("delete from Complain_feedback where Request_id='" + lbl_req_id.Text + "'");
                    bind_req(Session["sql"].ToString());
                    Alert("Deletion process has been successfully", "success");

                }
                else
                {
                    Alert(My.get_restricted_message(), "warning");
                }
            }
            catch
            {

            }
        }
    }
}