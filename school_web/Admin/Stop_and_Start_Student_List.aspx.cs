using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class Stop_and_Start_Student_List_aspx : System.Web.UI.Page
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
                        ViewState["Userid"] = Session["Admin"].ToString();


                        string pagename_current = "Stop_and_Start_Student_List.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        find_firm_details();
                        if (ViewState["Is_Print"].ToString() == "1")
                        {
                            print1.Visible = true;
                        }
                        else
                        {
                            print1.Visible = false;
                        }
                        mycode.bind_all_ddl_with_id(ddl_session, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int)");
                        ddl_session.SelectedValue = My.get_session_id();
                        mycode.bind_all_ddl_with_id_list_select(ddl_classs, "select Course_Name,course_id from Add_course_table order by Position asc");
                        mycode.bind_all_ddl_with_id_cap_All(ddl_month, "select Month,Month_Id from Month_Index order by Position asc");
                        ddl_month.SelectedValue = mycode.get_current_month_id();
                        foreach (ListItem item in ddl_classs.Items)
                        {
                            item.Selected = true;
                        }
                        find_by_date();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Stop_and_Start_Student_List");
            }
        }



        My mycode = new My();
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
        private void find_firm_details()
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
        protected void btn_find_Click(object sender, EventArgs e)
        {
            if (ddl_session.SelectedItem.Text == "")
            {
                Alertme("Please select session", "warning");
            }
            else
            {
                find_by_date();

            }
        }
        private void find_by_date()
        {

            string mainquery = "select admissionserialnumber, dateofadmission,CASE WHEN Transfer_Status = 'New' THEN 'New Admission' WHEN Transfer_Status = 'NT' THEN 'Old Admission' END AS Admission_Type,session,class,Section,rollnumber,studentname,gender,dob,fathername,father_mob,mothername,careof,city,district,pin,ac.Change_type,ac.Remark,format(Date_time,'dd/MM/yyyy') as changedate, (select top 1 name from user_details where user_id=ac.Created_By) as changeby ,(select top 1 Month from Month_Index where Month_Id=ac.Month_id) as monthname from admission_registor ar join admission_registor_Change_admission_no_history ac on ar.Session_id=ac.Session_id and ar.Class_id=ac.Class_Id_New and ar.admissionserialnumber=ac.Current_admission_no where ";
            string qoute = "'";
            string qry = "";
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
                Alertme("Please select class", "warning");
                return;
            }
            if (isClassSelectd == true)
            {
                selectClassid = selectClassid.Remove(selectClassid.Length - 1);
            }
            if (ddl_type.Text == "ALL" && ddl_month.SelectedItem.Text == "ALL")
            {

                qry = mainquery + " ar.Session_id='" + ddl_session.SelectedValue + "' and  ar.Class_id in (" + selectClassid + ") and (ar.Transfer_Status='New' or ar.Transfer_Status='NT')  and ac.Change_type in ('Student Stop','Student Start')  order by ar.admission_idate,ar.class,ar.Section,ar.rollnumber  asc";
            }
            else if (ddl_type.Text == "ALL" && ddl_month.SelectedItem.Text != "ALL")
            {

                qry = mainquery + " ar.Session_id='" + ddl_session.SelectedValue + "' and  ar.Class_id in (" + selectClassid + ") and (ar.Transfer_Status='New' or ar.Transfer_Status='NT') and ac.Change_type in ('Student Stop','Student Start') and ac.Month_id='" + ddl_month.SelectedValue + "'  order by ar.admission_idate,ar.class,ar.Section,ar.rollnumber  asc";
            }
            else if (ddl_type.Text != "ALL" && ddl_month.SelectedItem.Text == "ALL")
            {

                qry = mainquery + " ar.Session_id='" + ddl_session.SelectedValue + "' and  ar.Class_id in (" + selectClassid + ") and (ar.Transfer_Status='New' or ar.Transfer_Status='NT') and ac.Change_type='" + ddl_type.SelectedValue + "'  order by ar.admission_idate,ar.class,ar.Section,ar.rollnumber  asc";
            }
            else if (ddl_type.Text != "ALL" && ddl_month.SelectedItem.Text != "ALL")
            {

                qry = mainquery + " ar.Session_id='" + ddl_session.SelectedValue + "' and  ar.Class_id in (" + selectClassid + ") and (ar.Transfer_Status='New' or ar.Transfer_Status='NT')  and ac.Change_type='" + ddl_type.SelectedValue + "' and  ac.Month_id='" + ddl_month.SelectedValue + "'  order by ar.admission_idate,ar.class,ar.Section,ar.rollnumber  asc";
            }
            else
            {
                qry = mainquery + " ar.Session_id='" + ddl_session.SelectedValue + "' and  ar.Class_id in (" + selectClassid + ") and (ar.Transfer_Status='New' or ar.Transfer_Status='NT')  and ac.Change_type in ('Student Stop','Student Start')  order by ar.admission_idate,ar.class,ar.Section,ar.rollnumber  asc";
            }



            bind_grd_view(qry);
        }
        private void bind_grd_view(string qry)
        {
            print1.Visible = false;
            btn_excels.Visible = false;
            lbl_class22.Text = "";
            DataTable dt = mycode.FillData(qry);
            ViewState["datatable"] = dt;
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
                if (ViewState["Is_Print"].ToString() == "1")
                {
                    print1.Visible = true;
                }
                else
                {
                    print1.Visible = false;
                }

                if (ViewState["Is_Download"].ToString() == "1")
                {
                    btn_excels.Visible = true;
                }
                else
                {
                    btn_excels.Visible = false;
                }
            }
        }
        protected void btn_excels_Click(object sender, EventArgs e)
        {
            string excelname = My.with_excel_name("Stop and Start Student List");
            if (ViewState["Is_Download"].ToString() == "1")
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=Dues_list" + excelname +".xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                    Panel1.RenderControl(hw);
                  //  string style = @"<style> TABLE { border: 1px solid black; } TD { border: 1px solid black; } </style> ";
                  //  Response.Write(style);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
                /*  try
                  {
                      if (ViewState["datatable"] != null)
                      {
                          DataTable dt = ViewState["datatable"] as DataTable;
                          if (dt != null && dt.Rows.Count > 0)
                          {
                              // Build CSV content
                              StringBuilder sb = new StringBuilder();
                              // Add column headers
                              for (int i = 0; i < dt.Columns.Count; i++)
                              {
                                  sb.Append(dt.Columns[i].ColumnName);
                                  if (i < dt.Columns.Count - 1)
                                      sb.Append(",");
                              }
                              sb.AppendLine();
                              // Add rows
                              foreach (DataRow row in dt.Rows)
                              {
                                  for (int i = 0; i < dt.Columns.Count; i++)
                                  {
                                      // Escape commas and quotes
                                      string value = row[i].ToString().Replace("\"", "\"\"");
                                      sb.Append("\"" + value + "\"");

                                      if (i < dt.Columns.Count - 1)
                                          sb.Append(",");
                                  }
                                  sb.AppendLine();
                              }
                              // Send CSV file to browser
                              Response.Clear();
                              Response.Buffer = true;
                              Response.AddHeader("content-disposition", "attachment;filename=" + excelname + ".csv");
                              Response.Charset = "";
                              Response.ContentType = "text/csv";
                              Response.Output.Write(sb.ToString());
                              Response.Flush();
                              Response.End();
                          }
                      }
                  }
                  catch (Exception ex)
                  {
                  }*/


            }
            else
            {
                Alertme(My.get_restricted_message(), "warning");
            }
        }

        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string adm = ((Label)e.Item.FindControl("lbl_admissionserialnumber")).Text;
                string monthname = ((Label)e.Item.FindControl("lbl_monthname")).Text;
                string classname = ((Label)e.Item.FindControl("lbl_class")).Text;
                string session = ((Label)e.Item.FindControl("lbl_session")).Text;
                ((Label)e.Item.FindControl("lbl_dues")).Text= get_dues_amount_tillmonth(monthname, adm, classname, session);


            }

        }

        private string get_dues_amount_tillmonth(string monthname, string adm, string classname, string session)
        {
            string Session_id = My.get_sess_prm(session);
            string month_position = My.month_position(monthname);
            string qry = "select convert(float, (select sum(convert(float, Dues_amt)) from Student_wise_dues_amount where Session_id=t1.Session_id and Class_id=t1.Class_id and Admission_no=t1.admissionserialnumber and Month_position<" + month_position + ")) as Total_dues from admission_registor t1 join Add_course_table t2 on t1.Class_id=t2.course_id where  Session_id='" + Session_id + "' and t1.admissionserialnumber='" + adm + "'  ";
            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {
                return "0.00";
            }
            else
            {
                return dt.Rows[0]["Total_dues"].ToString();
            }
        }
    }
}