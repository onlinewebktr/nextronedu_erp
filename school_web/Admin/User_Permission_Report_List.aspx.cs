using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
using System.IO;

namespace school_web.Admin
{
    public partial class User_Permission_Report_List : System.Web.UI.Page
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
                        find_firm_details();
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();
                        string pagename_current = Path.GetFileName(Request.Path);
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        if(ViewState["Is_Print"].ToString()=="1")
                        {
                            print1.Visible = true;
                        }
                        else
                        {
                            print1.Visible = false;
                        }

                        DataTable dt = mycode.FillData(" select user_id, name from user_details where status='Active'  order by Name asc");//and  User_Type not in  ('Teacher','Principal')
                        mycode.PopulateDropDown(ddl_employee_name, dt, "name", "user_id");

                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "User_Permission_Report_List");
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

        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Download"].ToString() == "1")
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=Export.xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    using (StringWriter sw = new StringWriter())
                    {
                        HtmlTextWriter hw = new HtmlTextWriter(sw);
                        GrdView.RenderControl(hw);
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
        protected void btn_find_Click(object sender, EventArgs e)
        {
            if (ddl_employee_name.SelectedItem.Text == "Select")
            {
                Alertme("Please select employee name", "success");
            }
            else
            {
                Bind_data();
            }
        }

        private void Bind_data()
        {
            string query = " select mp.*,mgl.Group_name as main_menu,mm.Menu_name as submenu from dbo.[MenuPermissionForUser_web] mp join MenuMaster_web mm on mp.MenuID=mm.MenuID and mp.MainMenuId=mm.Group_id join Menu_Group_List_web mgl  on mm.Group_id=mgl.Group_id  where mp.UserID='" + ddl_employee_name.SelectedValue + "' and mm.Type=1 order by mgl.Position";

            ViewState["query"] = query;
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                lbl_class22.Text = "";

                btn_excels.Visible = false;
                print1.Visible = false;
                Alertme("Sorry there are no data list exist", "warning");
                GrdView.DataSource = null;
                GrdView.DataBind();

            }
            else
            {
                lbl_class22.Text = ddl_employee_name.SelectedItem.Text;
                btn_excels.Visible = true;
                print1.Visible = true;
                GrdView.DataSource = dt;
                GrdView.DataBind();
            }



        }

        protected void GrdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_Is_Edit = (Label)e.Row.FindControl("lbl_Is_Edit");
                Label lbl_Is_delete = (Label)e.Row.FindControl("lbl_Is_delete");
                Label lbl_Is_Download = (Label)e.Row.FindControl("lbl_Is_Download");
                Label lbl_Is_Print = (Label)e.Row.FindControl("lbl_Is_Print");

                Label lbl_Is_Edit_show = (Label)e.Row.FindControl("lbl_Is_Edit_show");
                Label lbl_Is_delete_show = (Label)e.Row.FindControl("lbl_Is_delete_show");
                Label lbl_Is_Download_show = (Label)e.Row.FindControl("lbl_Is_Download_show");
                Label lbl_Is_Print_show = (Label)e.Row.FindControl("lbl_Is_Print_show");


                if (lbl_Is_Edit.Text == "1")
                {
                    lbl_Is_Edit_show.Text = "Yes";

                }
                 
                else
                {
                    lbl_Is_Edit_show.Text = "No";
                }

                if (lbl_Is_delete.Text == "1")
                {
                    lbl_Is_delete_show.Text = "Yes";

                }
                
                else
                {
                    lbl_Is_delete_show.Text = "No";
                }


                if (lbl_Is_Download.Text == "1")
                {
                    lbl_Is_Download_show.Text = "Yes";

                }
                else
                {
                    lbl_Is_Download_show.Text = "No";
                }
                if (lbl_Is_Print.Text == "1")
                {
                    lbl_Is_Print_show.Text = "Yes";

                }
                else
                {
                    lbl_Is_Print_show.Text = "No";

                }

            }
        }
    }
}