using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class Student_Strength : System.Web.UI.Page
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
                        if (Request.QueryString["adm"] != null)
                        {
                            backbtns.HRef = "admission-report.aspx";
                        }
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();
                        mycode.bind_all_ddl_with_id(ddlsession, "Select  Session,session_id from session_details");
                        ddlsession.SelectedValue = My.get_session_id();
                        mycode.bind_all_ddl_with_id_All_New(ddlclass, "Select Course_Name,course_id,Position from Add_course_table order by Position");
                        mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor order by Section");
                        string pagename_current = "student-report-home.aspx";
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

                        find_data_list();

                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "StudentList");
            }
        }
        My mycode = new My();
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

        int classMaleTotal = 0;
        int classFemaleTotal = 0;

        int classMaleTotal_final = 0;
        int classFemaleTotal_final = 0;
        protected void rptDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                StudentData data = (StudentData)e.Item.DataItem;
                classMaleTotal += data.Male;
                classFemaleTotal += data.Female;
            }

            if (e.Item.ItemType == ListItemType.Footer)
            {
                Label lblClassMale = (Label)e.Item.FindControl("lblClassMale");
                Label lblClassFemale = (Label)e.Item.FindControl("lblClassFemale");
                Label lblClassTotal = (Label)e.Item.FindControl("lblClassTotal");

                lblClassMale.Text = classMaleTotal.ToString();
                lblClassFemale.Text = classFemaleTotal.ToString();
                lblClassTotal.Text = (classMaleTotal + classFemaleTotal).ToString();


                // fianl total
                classMaleTotal_final +=classMaleTotal;
                classFemaleTotal_final +=+classFemaleTotal;

                lblClassTotal_final.Text = (classMaleTotal_final + classFemaleTotal_final).ToString();



                lblClassMale_final.Text = classMaleTotal_final.ToString();
                lblClassFemale_final.Text =classFemaleTotal_final.ToString();

                // Reset for next group
                classMaleTotal = 0;
                classFemaleTotal = 0;
            }
        }
        public class StudentData
        {
            public string Class { get; set; }
            public string Section { get; set; }
            public int Male { get; set; }
            public int Female { get; set; }
            public int Total => Male + Female;
        }
        protected void btn_Find_Click(object sender, EventArgs e)
        {






            find_data_list();
        }

        private void find_data_list()
        {
            /*  var data = new List<StudentData>
             {


          new StudentData { Class = "Class-1", Section = "A", Male = 10, Female = 20 },
          new StudentData { Class = "Class-1", Section = "B", Male = 20, Female = 30 },
          new StudentData { Class = "Class-2", Section = "A", Male = 10, Female = 20 },
          new StudentData { Class = "Class-2", Section = "B", Male = 20, Female = 30 }


             };*/

            var condition = "where 1=1 ";
            condition += $" and t1.Status='1'  and  t1.Session_id='{ddlsession.SelectedValue}' ";
            if (ddlclass.SelectedItem.Text != "ALL")
            {
                condition += $" and t1.Class_id='{ddlclass.SelectedValue}' ";
            }
            if (ddl_section.Text != "ALL")
            {
                condition += $" and t1.Section='{ddlclass.SelectedValue}' ";
            }

            DataTable dataTable = My.MydataTable($@"SELECT 
    t2.Course_Name AS Class,
    t1.Section,
    SUM(CASE WHEN t1.gender = 'Male' THEN 1 ELSE 0 END) AS Male,
    SUM(CASE WHEN t1.gender = 'Female' THEN 1 ELSE 0 END) AS Female
FROM 
    admission_registor t1
JOIN 
    Add_course_table t2 ON t1.Class_id = t2.course_id
 {condition}
GROUP BY 
    t2.Course_Name, t1.Section, t2.Position
ORDER BY 
    t2.Position, t1.Section;");

            List<StudentData> data = new List<StudentData>();

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow row = dataTable.Rows[i];

                StudentData student = new StudentData
                {
                    Class = row["Class"].ToString(),
                    Section = row["Section"].ToString(),
                    Male = Convert.ToInt32(row["Male"]),
                    Female = Convert.ToInt32(row["Female"])
                };

                data.Add(student);
            }

    //        var grouped = data
    //.GroupBy(x => x.Class)
    //.Select(g => new
    //{
    //    Class = g.Key,
    //    Sections = g.ToList()
    //}).ToList();


            var grouped = data.GroupBy(x => x.Class).ToList();
            rptGrouped.DataSource = grouped;
            rptGrouped.DataBind();
        }

        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                string excelname = My.with_excel_name("Student_strength");
                if (ViewState["Is_Download"].ToString() == "1")
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename="+ excelname + ".xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    using (StringWriter sw = new StringWriter())
                    {
                        HtmlTextWriter hw = new HtmlTextWriter(sw);
                        pnl_grid.RenderControl(hw);
                        Response.Output.Write(sw.ToString());
                        Response.Flush();
                        Response.End();
                    }
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

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
    }
}