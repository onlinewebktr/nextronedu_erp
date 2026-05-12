using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class employee_list : System.Web.UI.Page
    {
        string password = "Select top 1 password from user_details where user_id=PRL_Employee_Master.Emp_Code";
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
                      
                        bind_all_data();
                        ViewState["flag"] = "0";
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
                        find_firm_details();



                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "StudentList");
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
                        Panel1.RenderControl(hw);
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


        private void bind_all_data()
        {

            bind_grd_view("select *,("+ password + ") as password,(select top 1 grade_name from PRL_Grade_Master where grade_id=PRL_Employee_Master.Grade_id) as Grade_name,(select top 1 name from PRL_Department_Master where department_id=PRL_Employee_Master.Department_id) as Department_name,(select top 1 name from PRL_Designation_Master where description_id=PRL_Employee_Master.Designation_id) as Designation_name from dbo.[PRL_Employee_Master] order by id desc");
        } 
         

        My mycode = new My();
        private void bind_grd_view(string qry)
        {
            ViewState["qry"] = qry;
            DataTable dt = mycode.FillData(qry);
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
            }
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");
                    Label lbl_emp_code = (Label)row.FindControl("lbl_emp_code");
                    hd_id.Value = lbl_Id.Text;
                    Response.Redirect("add-employee.aspx?empCodE=" + lbl_emp_code.Text, false);
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
        protected void lnkDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_delete"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_emp_code = (Label)row.FindControl("lbl_emp_code");
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");

                    mycode.executequery("delete from user_details where user_id='" + lbl_emp_code.Text + "'");
                    mycode.executequery("delete from PRL_Employee_Master where Id='" + lbl_Id.Text + "'");
                    Alertme("Selected employee has been deleted successfully", "success");
                    bind_all_data();
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


        #region active and inactive
        protected void lnkActive_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_emp_code = (Label)row.FindControl("lbl_emp_code");
                    Label lbl_active_status = (Label)row.FindControl("lbl_active_status");

                    if (lbl_active_status.Text == "")// Inactive
                    {
                        mycode.executequery("update PRL_Employee_Master set Status='Active' where Emp_Code='" + lbl_emp_code.Text + "'");
                        mycode.executequery("update user_details set Istatus='1' where user_id='" + lbl_emp_code.Text + "'");

                        Alertme("Your select user has been activated successfully", "success");

                    }
                    else if (lbl_active_status.Text == "Inactive")// Inactive
                    {
                        mycode.executequery("update PRL_Employee_Master set Status='Active' where Emp_Code='" + lbl_emp_code.Text + "'");
                        mycode.executequery("update user_details set Istatus='1' where user_id='" + lbl_emp_code.Text + "'");
                        Alertme("Your select user has been activated successfully", "success");
                    }
                    else if (lbl_active_status.Text == "Active")// Inactive
                    {
                        mycode.executequery("update PRL_Employee_Master set Status='Inactive' where Emp_Code='" + lbl_emp_code.Text + "'");
                        mycode.executequery("update user_details set Istatus='0' where user_id='" + lbl_emp_code.Text + "'");
                        Alertme("Your select user has been Inactivated successfully", "success");
                    }
                    bind_grd_view(ViewState["qry"].ToString());
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            catch(Exception ex)
            {
                

            }


        }
        #endregion

        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {

                    if (((Label)e.Item.FindControl("lbl_active_status")).Text == "Active")
                    {
                        ((LinkButton)e.Item.FindControl("lnkActive")).Text = "Inactive";
                    }
                    else
                    {
                        ((LinkButton)e.Item.FindControl("lnkActive")).Text = "Active";
                        
                    }
                }
            }
            catch { }
        }

        #region WebMethoD
        [WebMethod]
        public static List<string> get_emptype(string employeetype)
        {
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(My.conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct User_Type from user_details where User_Type LIKE '%'+@SearchMobNo+'%'   and status='Active' ";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchMobNo", employeetype);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        MobResult.Add(dr["User_Type"].ToString());
                    }
                    con.Close();
                    return MobResult;
                }
            }
        }

        [WebMethod]

        public static List<string> get_emp_mobile_no(string mob_no)
        {
             
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(My.conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct mobile from user_details where mobile LIKE '%'+@SearchMobNo+'%'   and status='Active' ";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchMobNo", mob_no);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        MobResult.Add(dr["mobile"].ToString());
                    }
                    con.Close();
                    return MobResult;
                }
            }
        }
        [WebMethod]
        public static List<string> get_emp_code(string emp_code)
        {

            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(My.conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct user_id from user_details where user_id LIKE '%'+@SearchMobNo+'%'   and status='Active' ";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchMobNo", emp_code);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        MobResult.Add(dr["user_id"].ToString());
                    }
                    con.Close();
                    return MobResult;
                }
            }
        }

        #endregion

        protected void txt_employe_type_TextChanged(object sender, EventArgs e)
        {
            try
            {
                bind_grd_view("select *,(" + password + ") as password,(select top 1 grade_name from PRL_Grade_Master where grade_id=PRL_Employee_Master.Grade_id) as Grade_name,(select top 1 name from PRL_Department_Master where department_id=PRL_Employee_Master.Department_id) as Department_name,(select top 1 name from PRL_Designation_Master where description_id=PRL_Employee_Master.Designation_id) as Designation_name from dbo.[PRL_Employee_Master] where employee_type='"+txt_employe_type.Text+"' order by id desc");

            }
            catch
            {

            }
        }

        protected void txt_mobile_no_TextChanged(object sender, EventArgs e)
        {
            try
            {
                bind_grd_view("select *,(" + password + ") as password,(select top 1 grade_name from PRL_Grade_Master where grade_id=PRL_Employee_Master.Grade_id) as Grade_name,(select top 1 name from PRL_Department_Master where department_id=PRL_Employee_Master.Department_id) as Department_name,(select top 1 name from PRL_Designation_Master where description_id=PRL_Employee_Master.Designation_id) as Designation_name from dbo.[PRL_Employee_Master] where Mobile='" + txt_mobile_no.Text + "' order by id desc");
            }
            catch
            {

            }

        }

        protected void txt_employee_code_TextChanged(object sender, EventArgs e)
        {
            try
            {
                bind_grd_view("select *,(" + password + ") as password,(select top 1 grade_name from PRL_Grade_Master where grade_id=PRL_Employee_Master.Grade_id) as Grade_name,(select top 1 name from PRL_Department_Master where department_id=PRL_Employee_Master.Department_id) as Department_name,(select top 1 name from PRL_Designation_Master where description_id=PRL_Employee_Master.Designation_id) as Designation_name from dbo.[PRL_Employee_Master] where Emp_Code='" + txt_employee_code.Text + "' order by id desc");
            }
            catch
            {

            }


        }

        protected void btn_find_emptype_Click(object sender, EventArgs e)
        {
            try
            {
                
                txt_mobile_no.Text = "";
                txt_employee_code.Text = "";
                bind_grd_view("select *,(" + password + ") as password,(select top 1 grade_name from PRL_Grade_Master where grade_id=PRL_Employee_Master.Grade_id) as Grade_name,(select top 1 name from PRL_Department_Master where department_id=PRL_Employee_Master.Department_id) as Department_name,(select top 1 name from PRL_Designation_Master where description_id=PRL_Employee_Master.Designation_id) as Designation_name from dbo.[PRL_Employee_Master] where employee_type='" + txt_employe_type.Text + "' order by id desc");

            }
            catch
            {

            }
        }

        protected void btn_find_mobile_no_Click(object sender, EventArgs e)
        {
            try
            {
                txt_employe_type.Text = "";
                
                txt_employee_code.Text = "";
                bind_grd_view("select *,(" + password + ") as password,(select top 1 grade_name from PRL_Grade_Master where grade_id=PRL_Employee_Master.Grade_id) as Grade_name,(select top 1 name from PRL_Department_Master where department_id=PRL_Employee_Master.Department_id) as Department_name,(select top 1 name from PRL_Designation_Master where description_id=PRL_Employee_Master.Designation_id) as Designation_name from dbo.[PRL_Employee_Master] where Mobile='" + txt_mobile_no.Text + "' order by id desc");
            }
            catch
            {

            }

        }

        protected void btn_find_emp_code_Click(object sender, EventArgs e)
        {
            try
            {
                txt_employe_type.Text = "";
                txt_mobile_no.Text = "";
                 
                bind_grd_view("select *,(" + password + ") as password,(select top 1 grade_name from PRL_Grade_Master where grade_id=PRL_Employee_Master.Grade_id) as Grade_name,(select top 1 name from PRL_Department_Master where department_id=PRL_Employee_Master.Department_id) as Department_name,(select top 1 name from PRL_Designation_Master where description_id=PRL_Employee_Master.Designation_id) as Designation_name from dbo.[PRL_Employee_Master] where Emp_Code='" + txt_employee_code.Text + "' order by id desc");
            }
            catch
            {

            }
        }

        protected void btn_reset_Click(object sender, EventArgs e)
        {
            txt_employe_type.Text = "";
            txt_mobile_no.Text = "";
            txt_employee_code.Text = "";
            bind_all_data();
        }
    }
}