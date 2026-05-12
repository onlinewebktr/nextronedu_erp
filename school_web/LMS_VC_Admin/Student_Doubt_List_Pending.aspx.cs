using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using school_web.AppCode;
using System.Data;
namespace school_web.LMS_VC_Admin
{
    public partial class Student_Doubt_List_Pending : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
        public string teachername = "Select top 1 Name  from user_details where user_id=t1.Teacher_Id";
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
                        txt_date.Text = code.daysback15();
                        txt_enddate.Text = code.date();
                        ViewState["Admin"] = Session["Admin"].ToString();
                        code.bind_all_ddl_with_all(ddl_class, "Select Course_Name, course_id from Add_course_table order by Position");
                        code.bind_ddl_all1(ddl_section, "Select distinct  csm.Subject_name, csm.Subject_id,csm.Subject_position from Subject_Master csm    order by csm.Subject_position");
                        code.bind_all_ddl_with_all(ddl_subject, " csm.Subject_position");
                        ddl_subject.Enabled = false;
                        try
                        {
                            if (Session["query"] == null)
                            {
                                Bind_gride_data();
                            }
                            else
                            {
                                if (Session["Type"].ToString() == "Pending")
                                {
                                    rd_pendin_req.Checked = true;
                                    rd_closed_req.Checked = false;
                                }
                                else
                                {
                                    rd_pendin_req.Checked = false;
                                    rd_closed_req.Checked = true;
                                    Session["Type"] = "Replied";
                                }
                                BindRepeater(Session["query"].ToString());
                            }
                        }
                        catch
                        {
                            Bind_gride_data();
                        }
                    }
                }

            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }
        public void Alert(string Message)
        {
            lbl_msg.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }
        protected void ddl_class_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_class.SelectedItem.Text == "ALL")
            {
                code.bind_ddl_all1(ddl_section, "Select distinct Section  from admission_registor  order by Section");
            }
            else
            {
                code.bind_ddl_all1(ddl_section, "Select distinct Section  from admission_registor where Class_id ='" + ddl_class.SelectedValue + "'    order by Section");
            }
        }

        protected void ddl_section_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_section.SelectedItem.Text == "ALL")
            {
                if (ddl_class.SelectedItem.Text == "ALL")
                {
                    code.bind_all_ddl_with_all(ddl_subject, "Select  distinct csm.Subject_name, csm.Subject_id,csm.Subject_position from Subject_Master csm  order by csm.Subject_position  ");
                    ddl_subject.Enabled = false;
                    ddl_subject.SelectedValue = "0";
                }
                else
                {
                    code.bind_all_ddl_with_all(ddl_subject, " Select   csm.Subject_name, csm.Subject_id from Subject_Master csm  where csm.course_id='" + ddl_class.SelectedValue + "'  order by csm.Subject_position  ");
                    ddl_subject.Enabled = false;
                    ddl_subject.SelectedValue = "0";
                }

            }
            else
            {
                if (ddl_class.SelectedItem.Text != "ALL")
                {
                    if (ddl_section.SelectedItem.Text == "ALL")
                    {
                        code.bind_all_ddl_with_all(ddl_subject, "Select  distinct csm.Subject_name, csm.Subject_id,csm.Subject_position from Subject_Master csm  order by csm.Subject_position  ");
                        ddl_subject.Enabled = false;
                        ddl_subject.SelectedValue = "0";
                    }
                    else
                    {
                        ddl_subject.Enabled = true;
                        code.bind_all_ddl_with_all(ddl_subject, " Select   csm.Subject_name, csm.Subject_id from Subject_Master csm  where csm.course_id='" + ddl_class.SelectedValue + "'  order by csm.Subject_position  ");
                    }

                }
                else
                {
                    if (ddl_class.SelectedItem.Text == "ALL")
                    {
                        if (ddl_section.Text == "ALL")
                        {
                            code.bind_all_ddl_with_all(ddl_subject, "Select  distinct csm.Subject_name, csm.Subject_id,csm.Subject_position from Subject_Master csm  order by csm.Subject_position  ");
                            ddl_subject.Enabled = false;
                            ddl_subject.SelectedValue = "0";
                        }
                        else
                        {
                            code.bind_all_ddl_with_all(ddl_subject, " Select  distinct csm.Subject_name, csm.Subject_id,csm.Subject_position from Subject_Master csm  order by csm.Subject_position  ");
                            ddl_subject.Enabled = false;
                            ddl_subject.SelectedValue = "0";
                        }
                    }
                    else
                    {
                        ddl_subject.Enabled = true;
                        code.bind_all_ddl_with_all(ddl_subject, "Select   csm.Subject_name, csm.Subject_id from Subject_Master csm  where csm.course_id='" + ddl_class.SelectedValue + "'  order by csm.Subject_position ");
                    }
                }
            }
        }

        protected void Btn_Find_Click(object sender, EventArgs e)
        {
            Bind_gride_data();




        }

        private void Bind_gride_data()
        {


            if (txt_date.Text == "")
            {
                Alert("Please select start date");
            }
            else if (txt_enddate.Text == "")
            {
                Alert("Please select end date");
            }
            else
            {
                if (rd_pendin_req.Checked == true)
                {
                    Session["Type"] = "Pending";
                    if (Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) <= Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)))
                    {
                        if (ddl_class.SelectedItem.Text == "ALL" && ddl_section.Text == "ALL")
                        {
                            try
                            {
                                if (ddl_subject.SelectedItem.Text == "ALL")
                                {
                                    string sql = " select (" + teachername + ") as teachername,t1.*,t2.admissionserialnumber,t2.rollnumber,t2.Section,t2.studentname+',  (Roll No. -'+CONVERT(varchar(10),rollnumber)+')' as askedby,t3.Course_Name as CategoryName  from dbo.[Student_doubt_list] t1 join admission_registor t2 on t1.User_Id=t2.admissionserialnumber   left join  Add_course_table t3 on t1.Class_Id=t3.course_id where t1.Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and t1.Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + "  and t1.Status='Pending' order by t1.Idate desc ; ";
                                    sql = sql + "select  t1.Status,count(t1.Id) as total  from Student_doubt_list t1   where    t1.Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and t1.Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + "   group by Status; ";
                                    BindRepeater(sql);
                                }
                                else
                                {
                                    string sql = "select (" + teachername + ") as teachername,t1.*,t2.admissionserialnumber,t2.rollnumber,t2.Section,t2.studentname+',  (Roll No. -'+CONVERT(varchar(10),rollnumber)+')' as askedby,t3.Course_Name as CategoryName  from dbo.[Student_doubt_list] t1 join admission_registor t2 on t1.User_Id=t2.admissionserialnumber   left join  Add_course_table t3 on t1.Class_Id=t3.course_id where t1.Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and t1.Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + "  and t1.Cource_Id='" + ddl_subject.SelectedValue + "' and t1.Status='Pending' order by t1.Idate desc  ; ";
                                    sql = sql + "select  t1.Status,count(t1.Id) as total  from Student_doubt_list t1   where    t1.Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and t1.Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + "  and  t1.Cource_Id='" + ddl_subject.SelectedValue + "'  group by Status; ";
                                    BindRepeater(sql);
                                }
                            }
                            catch
                            {
                                string sql = " select (" + teachername + ") as teachername,t1.*,t2.admissionserialnumber,t2.rollnumber,t2.Section,t2.studentname+',  (Roll No. -'+CONVERT(varchar(10),rollnumber)+')' as askedby,t3.Course_Name as CategoryName  from dbo.[Student_doubt_list] t1 join admission_registor t2 on t1.User_Id=t2.admissionserialnumber   left join  Add_course_table t3 on t1.Class_Id=t3.course_id where t1.Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and t1.Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + "  and t1.Status='Pending' order by t1.Idate desc ; ";
                                sql = sql + "select  t1.Status,count(t1.Id) as total  from Student_doubt_list t1   where    t1.Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and t1.Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + "   group by Status; ";
                                BindRepeater(sql);

                            }
                        }
                        else if (ddl_class.SelectedItem.Text != "ALL" && ddl_section.Text == "ALL")
                        {
                            try
                            {
                                if (ddl_subject.SelectedItem.Text == "ALL")
                                {
                                    string sql = " select (" + teachername + ") as teachername,t1.*,t2.admissionserialnumber,t2.rollnumber,t2.Section,t2.studentname+',  (Roll No. -'+CONVERT(varchar(10),rollnumber)+')' as askedby,t3.Course_Name as CategoryName  from dbo.[Student_doubt_list] t1 join admission_registor t2 on t1.User_Id=t2.admissionserialnumber   left join  Add_course_table t3 on t1.Class_Id=t3.course_id where t1.Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and t1.Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + "  and t2.Class_Id='" + ddl_class.SelectedValue + "' and t1.Status='Pending' order by t1.Idate desc  ; ";
                                    sql = sql + "select  t1.Status,count(t1.Id) as total  from Student_doubt_list t1   where    t1.Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and t1.Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + " and  t2.Class_Id='" + ddl_class.SelectedValue + "'  group by Status; ";
                                    BindRepeater(sql);
                                }
                                else
                                {
                                    string sql = " select (" + teachername + ") as teachername,t1.*,t2.admissionserialnumber,t2.rollnumber,t2.Section,t2.studentname+',  (Roll No. -'+CONVERT(varchar(10),rollnumber)+')' as askedby,t3.Course_Name as CategoryName  from dbo.[Student_doubt_list] t1 join admission_registor t2 on t1.User_Id=t2.admissionserialnumber   left join  Add_course_table t3 on t1.Class_Id=t3.course_id where t1.Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and t1.Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + "  and t1.Class_Id='" + ddl_class.SelectedValue + "' and t1.Cource_Id='" + ddl_subject.SelectedValue + "' and t.Status='Pending' order by t1.Idate desc ; ";
                                    sql = sql + "select  t1.Status,count(t1.Id) as total  from Student_doubt_list t1   where    t1.Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and t1.Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + "  and  t2.Class_Id='" + ddl_class.SelectedValue + "' and t1.Cource_Id='" + ddl_subject.SelectedValue + "'   group by Status; ";
                                    BindRepeater(sql);
                                }
                            }
                            catch
                            {
                                string sql = " select (" + teachername + ") as teachername,t1.*,t2.admissionserialnumber,t2.rollnumber,t2.Section,t2.studentname+',  (Roll No. -'+CONVERT(varchar(10),rollnumber)+')' as askedby,t3.Course_Name as CategoryName  from dbo.[Student_doubt_list] t1 join admission_registor t2 on t1.User_Id=t2.admissionserialnumber   left join  Add_course_table t3 on t1.Class_Id=t3.course_id where t1.Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and t1.Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + "  and t2.Class_Id='" + ddl_class.SelectedValue + "' and t1.Status='Pending' order by t1.Idate desc  ; ";
                                sql = sql + "select  t1.Status,count(t1.Id) as total  from Student_doubt_list t1   where    t1.Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and t1.Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + " and  t2.Class_Id='" + ddl_class.SelectedValue + "'  group by Status; ";
                                BindRepeater(sql);
                            }
                            
                        }
                        else if (ddl_class.SelectedItem.Text == "ALL" && ddl_section.Text != "ALL")
                        {
                            try
                            {
                                if (ddl_subject.SelectedItem.Text == "ALL")
                                {
                                    string sql = " select (" + teachername + ") as teachername,t1.*,t2.admissionserialnumber,t2.rollnumber,t2.Section,t2.studentname+',  (Roll No. -'+CONVERT(varchar(10),rollnumber)+')' as askedby,t3.Course_Name as CategoryName  from dbo.[Student_doubt_list] t1 join admission_registor t2 on t1.User_Id=t2.admissionserialnumber   left join  Add_course_table t3 on t1.Class_Id=t3.course_id where t1.Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and t1.Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + "  and t2.Section='" + ddl_section.Text + "' and t1.Status='Pending' order by t1.Idate desc  ; ";
                                    sql = sql + "select  t1.Status,count(t1.Id) as total  from Student_doubt_list t1   where    t1.Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and t1.Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + "  and  t1.User_Id in (select admissionserialnumber from admission_registor where  Section='" + ddl_section.Text + "') and t1.Cource_Id='" + ddl_subject.SelectedValue + "'   group by Status; ";
                                    BindRepeater(sql);
                                }
                                else
                                {
                                    string sql = "select (" + teachername + ") as teachername,t1.*,t2.admissionserialnumber,t2.rollnumber,t2.Section,t2.studentname+',  (Roll No. -'+CONVERT(varchar(10),rollnumber)+')' as askedby,t3.Course_Name as CategoryName  from dbo.[Student_doubt_list] t1 join admission_registor t2 on t1.User_Id=t2.admissionserialnumber   left join  Add_course_table t3 on t1.Class_Id=t3.course_id where t1.Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and t1.Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + "  and t1.Cource_Id='" + ddl_subject.SelectedValue + "'  and t2.Section='" + ddl_section.Text + "' and t1.Status='Pending' order by t1.Idate desc  ; ";
                                    sql = sql + "select  t1.Status,count(t1.Id) as total  from Student_doubt_list t1   where    t1.Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and t1.Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + "  and   t1.User_Id in (select admissionserialnumber from admission_registor where  Section='" + ddl_section.Text + "') and t1.Cource_Id='" + ddl_subject.SelectedValue + "'   group by Status; ";
                                    BindRepeater(sql);
                                }
                            }
                            catch
                            {
                                string sql = " select (" + teachername + ") as teachername,t1.*,t2.admissionserialnumber,t2.rollnumber,t2.Section,t2.studentname+',  (Roll No. -'+CONVERT(varchar(10),rollnumber)+')' as askedby,t3.Course_Name as CategoryName  from dbo.[Student_doubt_list] t1 join admission_registor t2 on t1.User_Id=t2.admissionserialnumber   left join  Add_course_table t3 on t1.Class_Id=t3.course_id where t1.Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and t1.Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + "  and t2.Section='" + ddl_section.Text + "' and t1.Status='Pending' order by t1.Idate desc  ; ";
                                sql = sql + "select  t1.Status,count(t1.Id) as total  from Student_doubt_list t1   where    t1.Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and t1.Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + "  and  t1.User_Id in (select admissionserialnumber from admission_registor where  Section='" + ddl_section.Text + "') and t1.Cource_Id='" + ddl_subject.SelectedValue + "'   group by Status; ";
                                BindRepeater(sql);

                            }
                        }
                        else if (ddl_class.SelectedItem.Text != "ALL" && ddl_section.Text != "ALL")
                        {
                            try
                            {
                                if (ddl_subject.SelectedItem.Text == "ALL")
                                {
                                    string sql = " select (" + teachername + ") as teachername,t1.*,t2.admissionserialnumber,t2.rollnumber,t2.Section,t2.studentname+',  (Roll No. -'+CONVERT(varchar(10),rollnumber)+')' as askedby,t3.Course_Name as CategoryName  from dbo.[Student_doubt_list] t1 join admission_registor t2 on t1.User_Id=t2.admissionserialnumber   left join  Add_course_table t3 on t1.Class_Id=t3.course_id where t1.Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and t1.Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + "  and t2.Section='" + ddl_section.Text + "' and t1.Class_Id='" + ddl_class.SelectedValue + "' and t1.Status='Pending' order by t1.Idate desc  ; ";
                                    sql = sql + "select  t1.Status,count(t1.Id) as total  from Student_doubt_list t1   where    t1.Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and t1.Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + "  and  t1.User_Id in (select admissionserialnumber from admission_registor where Class_id='" + ddl_class.SelectedValue + "' and Section='" + ddl_section.Text + "') and t1.Class_Id='" + ddl_class.SelectedValue + "'  group by Status; ";
                                    BindRepeater(sql);
                                }
                                else
                                {
                                    string sql = "select (" + teachername + ") as teachername,t1.*,t2.admissionserialnumber,t2.rollnumber,t2.Section,t2.studentname+',  (Roll No. -'+CONVERT(varchar(10),rollnumber)+')' as askedby,t3.Course_Name as CategoryName  from dbo.[Student_doubt_list] t1 join admission_registor t2 on t1.User_Id=t2.admissionserialnumber   left join  Add_course_table t3 on t1.Class_Id=t3.course_id where t1.Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and t1.Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + "  and t1.Cource_Id='" + ddl_subject.SelectedValue + "'  and t2.Section='" + ddl_section.Text + "' and  t1.Class_Id='" + ddl_class.SelectedValue + "' order by t1.Idate desc  ; ";
                                    sql = sql + "select  t1.Status,count(t1.Id) as total  from Student_doubt_list t1   where    t1.Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and t1.Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + "  and    t1.User_Id in (select admissionserialnumber from admission_registor where Class_id='" + ddl_class.SelectedValue + "' and Section='" + ddl_section.Text + "') and  t1.Class_Id='" + ddl_class.SelectedValue + "'  and   t1.Cource_Id='" + ddl_subject.SelectedValue + "'   group by Status; ";
                                    BindRepeater(sql);
                                }
                            }
                            catch
                            {
                                string sql = " select (" + teachername + ") as teachername,t1.*,t2.admissionserialnumber,t2.rollnumber,t2.Section,t2.studentname+',  (Roll No. -'+CONVERT(varchar(10),rollnumber)+')' as askedby,t3.Course_Name as CategoryName  from dbo.[Student_doubt_list] t1 join admission_registor t2 on t1.User_Id=t2.admissionserialnumber   left join  Add_course_table t3 on t1.Class_Id=t3.course_id where t1.Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and t1.Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + "  and t2.Section='" + ddl_section.Text + "' and t1.Class_Id='" + ddl_class.SelectedValue + "' and t1.Status='Pending' order by t1.Idate desc  ; ";
                                sql = sql + "select  t1.Status,count(t1.Id) as total  from Student_doubt_list t1   where    t1.Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and t1.Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + "  and  t1.User_Id in (select admissionserialnumber from admission_registor where Class_id='" + ddl_class.SelectedValue + "' and Section='" + ddl_section.Text + "') and t1.Class_Id='" + ddl_class.SelectedValue + "'  group by Status; ";
                                BindRepeater(sql);

                            }
                        }
                    }
                    else
                    {
                        Alert("Please select date valid");
                    }
                }
                else if (rd_closed_req.Checked == true)
                {
                    Session["Type"] = "Replied";
                    if (Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) <= Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)))
                    {
                        if (ddl_class.SelectedItem.Text == "ALL" && ddl_section.Text == "ALL")
                        {
                            try
                            {
                                if (ddl_subject.SelectedItem.Text == "ALL")
                                {
                                    string sql = "select (" + teachername + ") as teachername,t1.*,t2.admissionserialnumber,t2.rollnumber,t2.Section,t2.studentname+',  (Roll No. -'+CONVERT(varchar(10),rollnumber)+')' as askedby,t3.Course_Name as CategoryName  from dbo.[Student_doubt_list] t1 join admission_registor t2 on t1.User_Id=t2.admissionserialnumber   left join  Add_course_table t3 on t1.Class_Id=t3.course_id where t1.Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and t1.Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + "  and t1.Status!='Pending'  order by t1.Idate desc ; ";
                                    sql = sql + "select  t1.Status,count(t1.Id) as total  from Student_doubt_list t1   where    t1.Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and t1.Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + "  group by Status; ";
                                    BindRepeater(sql);
                                }
                                else
                                {
                                    string sql = "select (" + teachername + ") as teachername,t1.*,t2.admissionserialnumber,t2.rollnumber,t2.Section,t2.studentname+',  (Roll No. -'+CONVERT(varchar(10),rollnumber)+')' as askedby,t3.Course_Name as CategoryName  from dbo.[Student_doubt_list] t1 join admission_registor t2 on t1.User_Id=t2.admissionserialnumber   left join  Add_course_table t3 on t1.Class_Id=t3.course_id where t1.Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and t1.Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + "  and t1.Cource_Id='" + ddl_subject.SelectedValue + "' and t1.Status!='Pending' order by t1.Idate desc  ; ";
                                    sql = sql + "select  t1.Status,count(t1.Id) as total  from Student_doubt_list t1   where    t1.Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and t1.Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + "  and  t1.Cource_Id='" + ddl_subject.SelectedValue + "'  group by Status; ";
                                    BindRepeater(sql);
                                }
                            }
                            catch
                            {
                                string sql = "select (" + teachername + ") as teachername,t1.*,t2.admissionserialnumber,t2.rollnumber,t2.Section,t2.studentname+',  (Roll No. -'+CONVERT(varchar(10),rollnumber)+')' as askedby,t3.Course_Name as CategoryName  from dbo.[Student_doubt_list] t1 join admission_registor t2 on t1.User_Id=t2.admissionserialnumber   left join  Add_course_table t3 on t1.Class_Id=t3.course_id where t1.Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and t1.Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + "  and t1.Status!='Pending'  order by t1.Idate desc ; ";
                                sql = sql + "select  t1.Status,count(t1.Id) as total  from Student_doubt_list t1   where    t1.Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and t1.Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + "  group by Status; ";
                                BindRepeater(sql);
                            }
                        }
                        else if (ddl_class.SelectedItem.Text != "ALL" && ddl_section.Text == "ALL")
                        {
                            try
                            {
                                if (ddl_subject.SelectedItem.Text == "ALL")
                                {
                                    string sql = "select (" + teachername + ") as teachername,t1.*,t2.admissionserialnumber,t2.rollnumber,t2.Section,t2.studentname+',  (Roll No. -'+CONVERT(varchar(10),rollnumber)+')' as askedby,t3.Course_Name as CategoryName  from dbo.[Student_doubt_list] t1 join admission_registor t2 on t1.User_Id=t2.admissionserialnumber   left join  Add_course_table t3 on t1.Class_Id=t3.course_id where t1.Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and t1.Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + "  and t2.Class_Id='" + ddl_class.SelectedValue + "' and t1.Status!='Pending' order by t1.Idate desc  ; ";
                                    sql = sql + "select  t1.Status,count(t1.Id) as total  from Student_doubt_list t1   where    t1.Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and t1.Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + "  and  t1.Class_Id='" + ddl_class.SelectedValue + "'  group by Status; ";
                                    BindRepeater(sql);
                                }
                                else
                                {
                                    string sql = "select (" + teachername + ") as teachername,t1.*,t2.admissionserialnumber,t2.rollnumber,t2.Section,t2.studentname+',  (Roll No. -'+CONVERT(varchar(10),rollnumber)+')' as askedby,t3.Course_Name as CategoryName  from dbo.[Student_doubt_list] t1 join admission_registor t2 on t1.User_Id=t2.admissionserialnumber   left join  Add_course_table t3 on t1.Class_Id=t3.course_id where t1.Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and t1.Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + "  and t1.Class_Id='" + ddl_class.SelectedValue + "' and t1.Cource_Id='" + ddl_subject.SelectedValue + "' and t1.Status!='Pending' order by t1.Idate desc ; ";
                                    sql = sql + "select  t1.Status,count(t1.Id) as total  from Student_doubt_list t1   where    t1.Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and t1.Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + "  and  t1.Class_Id='" + ddl_class.SelectedValue + "' and t1.Cource_Id='" + ddl_subject.SelectedValue + "'   group by Status; ";
                                    BindRepeater(sql);
                                }
                            }
                            catch
                            {
                                string sql = "select (" + teachername + ") as teachername,t1.*,t2.admissionserialnumber,t2.rollnumber,t2.Section,t2.studentname+',  (Roll No. -'+CONVERT(varchar(10),rollnumber)+')' as askedby,t3.Course_Name as CategoryName  from dbo.[Student_doubt_list] t1 join admission_registor t2 on t1.User_Id=t2.admissionserialnumber   left join  Add_course_table t3 on t1.Class_Id=t3.course_id where t1.Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and t1.Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + "  and t2.Class_Id='" + ddl_class.SelectedValue + "' and t1.Status!='Pending' order by t1.Idate desc  ; ";
                                sql = sql + "select  t1.Status,count(t1.Id) as total  from Student_doubt_list t1   where    t1.Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and t1.Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + "  and  t1.Class_Id='" + ddl_class.SelectedValue + "'  group by Status; ";
                                BindRepeater(sql);
                            }
                        }
                        else if (ddl_class.SelectedItem.Text == "ALL" && ddl_section.Text != "ALL")
                        {
                            try
                            {
                                if (ddl_subject.SelectedItem.Text == "ALL")
                                {
                                    string sql = "select (" + teachername + ") as teachername,t1.*,t2.admissionserialnumber,t2.rollnumber,t2.Section,t2.studentname+',  (Roll No. -'+CONVERT(varchar(10),rollnumber)+')' as askedby,t3.Course_Name as CategoryName  from dbo.[Student_doubt_list] t1 join admission_registor t2 on t1.User_Id=t2.admissionserialnumber   left join  Add_course_table t3 on t1.Class_Id=t3.course_id where t1.Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and t1.Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + "  and t2.Section='" + ddl_section.Text + "' and t1.Status!='Pending' order by t1.Idate desc  ; ";
                                    sql = sql + "select  t1.Status,count(t1.Id) as total  from Student_doubt_list t1   where    t1.Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and t1.Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + " and t1.Teacher_Id='" + ViewState["teacher"].ToString() + "' and  t1.User_Id in (select admissionserialnumber from admission_registor where  Section='" + ddl_section.Text + "') and t1.Cource_Id='" + ddl_subject.SelectedValue + "'   group by Status; ";
                                    BindRepeater(sql);
                                }
                                else
                                {
                                    string sql = " select (" + teachername + ") as teachername,t1.*,t2.admissionserialnumber,t2.rollnumber,t2.Section,t2.studentname+',  (Roll No. -'+CONVERT(varchar(10),rollnumber)+')' as askedby,t3.Course_Name as CategoryName  from dbo.[Student_doubt_list] t1 join admission_registor t2 on t1.User_Id=t2.admissionserialnumber   left join  Add_course_table t3 on t1.Class_Id=t3.course_id where t1.Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and t1.Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + "  and t1.Cource_Id='" + ddl_subject.SelectedValue + "'  and t2.Section='" + ddl_section.Text + "' and t1.Status!='Pending' order by t1.Idate desc  ; ";
                                    sql = sql + "select  t1.Status,count(t1.Id) as total  from Student_doubt_list t1   where    t1.Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and t1.Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + "  and   t1.User_Id in (select admissionserialnumber from admission_registor where  Section='" + ddl_section.Text + "') and t1.Cource_Id='" + ddl_subject.SelectedValue + "'    group by Status; ";
                                    BindRepeater(sql);
                                }
                            }
                            catch
                            {
                                string sql = "select (" + teachername + ") as teachername,t1.*,t2.admissionserialnumber,t2.rollnumber,t2.Section,t2.studentname+',  (Roll No. -'+CONVERT(varchar(10),rollnumber)+')' as askedby,t3.Course_Name as CategoryName  from dbo.[Student_doubt_list] t1 join admission_registor t2 on t1.User_Id=t2.admissionserialnumber   left join  Add_course_table t3 on t1.Class_Id=t3.course_id where t1.Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and t1.Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + "  and t2.Section='" + ddl_section.Text + "' and t1.Status!='Pending' order by t1.Idate desc  ; ";
                                sql = sql + "select  t1.Status,count(t1.Id) as total  from Student_doubt_list t1   where    t1.Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and t1.Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + " and t1.Teacher_Id='" + ViewState["teacher"].ToString() + "' and  t1.User_Id in (select admissionserialnumber from admission_registor where  Section='" + ddl_section.Text + "') and t1.Cource_Id='" + ddl_subject.SelectedValue + "'   group by Status; ";
                                BindRepeater(sql);

                            }
                        }
                        else if (ddl_class.SelectedItem.Text != "ALL" && ddl_section.Text != "ALL")
                        {
                            try
                            {
                                if (ddl_subject.SelectedItem.Text == "ALL")
                                {
                                    string sql = " select (" + teachername + ") as teachername,t1.*,t2.admissionserialnumber,t2.rollnumber,t2.Section,t2.studentname+',  (Roll No. -'+CONVERT(varchar(10),rollnumber)+')' as askedby,t3.Course_Name as CategoryName  from dbo.[Student_doubt_list] t1 join admission_registor t2 on t1.User_Id=t2.admissionserialnumber   left join  Add_course_table t3 on t1.Class_Id=t3.course_id where t1.Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and t1.Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + "  and t2.Section='" + ddl_section.Text + "' and t1.Class_Id='" + ddl_class.SelectedValue + "' and t1.Status!='Pending'  order by t1.Idate desc  ; ";
                                    sql = sql + "select  t1.Status,count(t1.Id) as total  from Student_doubt_list t1   where    t1.Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and t1.Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + "  and  t1.User_Id in (select admissionserialnumber from admission_registor where Class_id='" + ddl_class.SelectedValue + "' and Section='" + ddl_section.Text + "') and t1.Class_Id='" + ddl_class.SelectedValue + "'      group by Status; ";
                                    BindRepeater(sql);
                                }
                                else
                                {
                                    string sql = "select (" + teachername + ") as teachername,t1.*,t2.admissionserialnumber,t2.rollnumber,t2.Section,t2.studentname+',  (Roll No. -'+CONVERT(varchar(10),rollnumber)+')' as askedby,t3.Course_Name as CategoryName  from dbo.[Student_doubt_list] t1 join admission_registor t2 on t1.User_Id=t2.admissionserialnumber   left join  Add_course_table t3 on t1.Class_Id=t3.course_idwhere t1.Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and t1.Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + " and t1.Teacher_Id='" + ViewState["teacher"].ToString() + "' and t1.Cource_Id='" + ddl_subject.SelectedValue + "'  and t2.Section='" + ddl_section.Text + "' and  t1.Class_Id='" + ddl_class.SelectedValue + "' and t1.Status!='Pending' order by t1.Idate desc  ; ";
                                    sql = sql + "select  t1.Status,count(t1.Id) as total  from Student_doubt_list t1   where    t1.Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and t1.Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + "  and    t1.User_Id in (select admissionserialnumber from admission_registor where Class_id='" + ddl_class.SelectedValue + "' and Section='" + ddl_section.Text + "')  and t1.Class_Id='" + ddl_class.SelectedValue + "'  and   t1.Cource_Id='" + ddl_subject.SelectedValue + "'   group by Status; ";
                                    BindRepeater(sql);
                                }
                            }
                            catch
                            {
                                string sql = " select (" + teachername + ") as teachername,t1.*,t2.admissionserialnumber,t2.rollnumber,t2.Section,t2.studentname+',  (Roll No. -'+CONVERT(varchar(10),rollnumber)+')' as askedby,t3.Course_Name as CategoryName  from dbo.[Student_doubt_list] t1 join admission_registor t2 on t1.User_Id=t2.admissionserialnumber   left join  Add_course_table t3 on t1.Class_Id=t3.course_id where t1.Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and t1.Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + "  and t2.Section='" + ddl_section.Text + "' and t1.Class_Id='" + ddl_class.SelectedValue + "' and t1.Status!='Pending'  order by t1.Idate desc  ; ";
                                sql = sql + "select  t1.Status,count(t1.Id) as total  from Student_doubt_list t1   where    t1.Idate>=" + Convert.ToInt32(code.ConvertStringToiDate(txt_date.Text)) + " and t1.Idate<=" + Convert.ToInt32(code.ConvertStringToiDate(txt_enddate.Text)) + "  and  t1.User_Id in (select admissionserialnumber from admission_registor where Class_id='" + ddl_class.SelectedValue + "' and Section='" + ddl_section.Text + "') and t1.Class_Id='" + ddl_class.SelectedValue + "'      group by Status; ";
                                BindRepeater(sql);

                            }
                        }
                    }
                    else
                    {
                        Alert("Please select date valid");
                    }
                }







            }
        }

        private void BindRepeater(string query)
        {
            try
            {
                Session["query"] = query;
                DataSet ds = code.Fill_Data_set(query);
                DataTable dt = ds.Tables[0];
                code.RptrData(dt, RPDetails);
                if (Convert.ToString(dt.Rows.Count) == "0")
                {
                    lbl_total_data.Text = dt.Rows.Count.ToString();
                    lbl_pending.Text = "0";
                    lbl_replied_Questions.Text = "0";
                    Alert("Data Not Available");
                    RPDetails.DataSource = null;
                    RPDetails.DataBind();



                }
                else
                {

                   
                    if (ds.Tables[1].Rows.Count > 0)
                        dt = ds.Tables[1];
                    lbl_pending.Text = "0";
                    lbl_replied_Questions.Text = "0";
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["Status"].ToString() == "Pending")
                        {
                            lbl_pending.Text = dr["total"].ToString();
                        }
                        else
                        {
                            lbl_replied_Questions.Text = dr["total"].ToString();
                        }
                    }

                    lbl_total_data.Text = Convert.ToString(Convert.ToDouble(code.ValidateNumberint(lbl_pending.Text)) + code.ValidateNumberint(lbl_replied_Questions.Text));

                }
            }
            catch
            {
            }
        }

        protected void lnk_reply_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;

            Label lbl_id = (Label)row.FindControl("lbl_id");
            if (rd_pendin_req.Checked == true)
            {
                Session["Type"] = "Pending";
            }
            else
            {
                Session["Type"] = "Replied";
            }
            Response.Redirect("Reply_Ask_Doubt.aspx?id=" + lbl_id.Text, false);

        }

        protected void RPDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                if (((Label)e.Item.FindControl("lbl_status")).Text == "Pending")
                {



                    ((Label)e.Item.FindControl("lbl_status_disply")).Text = "Pending";
                    ((Label)e.Item.FindControl("lbl_status_disply")).CssClass = "badge badge-danger ml-2";
                }
                else
                {

                    ((Label)e.Item.FindControl("lbl_status_disply")).Text = "Replyid";
                    ((Label)e.Item.FindControl("lbl_status_disply")).CssClass = "badge badge-success ml-2";
                }

            }
        }

        protected void rd_pendin_req_CheckedChanged(object sender, EventArgs e)
        {
            Bind_gride_data();
        }

        protected void rd_closed_req_CheckedChanged(object sender, EventArgs e)
        {
            Bind_gride_data();
        }





    }
}