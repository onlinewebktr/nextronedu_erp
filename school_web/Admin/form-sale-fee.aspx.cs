using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class form_sale_fee : System.Web.UI.Page
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
                        if (Session["msgS"] != null)
                        {
                            Alertme(Session["msgS"].ToString(), "success");
                            Session["msgS"] = null;
                        }
                        ViewState["courseID"] = "0";
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();
                        string pagename_current = Path.GetFileName(Request.Path);
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        string formSaleSession = My.get_single_column_data("select top 1 session_id as Column_Name from session_details where Form_sale_active=1");
                        mycode.bind_all_ddl_with_id(ddl_session_serach, "Select Session,session_id from session_details  order by cast((Substring (Session,1,4)) as int) ");
                        mycode.bind_all_ddl_with_id(ddl_session, "Select Session,session_id from session_details order by cast((Substring (Session,1,4)) as int) ");
                        Bind_course_fee_details();

                        try
                        {
                            ddl_session.SelectedValue = formSaleSession;
                        }
                        catch (Exception ex)
                        {
                        }
                        try
                        {
                            ddl_session_serach.SelectedValue = formSaleSession;
                        }
                        catch (Exception ex)
                        {
                        }
                      
                        find_data();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_Strength_Master");
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
        private void Bind_course_fee_details()
        {
            DataTable dt = mycode.FillData("Select Course_Name,course_id from Add_course_table order by Position");
            if (dt.Rows.Count == 0)
            {
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }


        My mycode = new My();
        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (((Label)e.Item.FindControl("lbl_class_id")).Text == ViewState["courseID"].ToString())
                {
                    ((CheckBox)e.Item.FindControl("chk_class")).Checked = true;
                }
            }
        }



        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("form-sale-fee.aspx", false);
        }

        protected void btn_Submit_Click1(object sender, EventArgs e)
        {
            try
            {
                ViewState["Is_add"] = "0";
                if (ddl_session.Text == "Select")
                {
                    Alertme("Please Select Session", "warning");
                    ddl_session.Focus();
                    return;
                }


                if (txt_fee.Text == "")
                {
                    Alertme("Please Enter Amount", "warning");
                    txt_fee.Focus();
                    return;
                }

                bool isamountfill = false;
                int growcountc = rd_view.Items.Count;
                for (int ix = 0; ix < growcountc; ix++)
                {
                    CheckBox chk = (CheckBox)rd_view.Items[ix].FindControl("chk_class");
                    if (chk.Checked == true)
                    {
                        isamountfill = true;
                    }
                }
                if (isamountfill == true)
                {
                    int growcount = rd_view.Items.Count;
                    int k = 0;
                    for (int ix = 0; ix < growcount; ix++)
                    {
                        CheckBox chk = (CheckBox)rd_view.Items[ix].FindControl("chk_class");
                        if (chk.Checked == true)
                        {
                            Label lbl_class_id = (Label)rd_view.Items[ix].FindControl("lbl_class_id");
                            Label lbl_course_name = (Label)rd_view.Items[ix].FindControl("lbl_course_name");
                            insert_fee_to_form_sale(txt_fee.Text, lbl_class_id.Text, lbl_course_name.Text);
                        }
                    }
                }
                else
                {
                    Alertme("Please select the class to which you want to add the fee.", "warning");
                }

                if (ViewState["Is_add"].ToString() == "1")
                {
                    Session["msgS"] = "Fee details has been updated successfully.";
                    Response.Redirect("form-sale-fee.aspx", false);
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void chk_all_CheckedChanged(object sender, EventArgs e)
        {
            for (int j = 0; j < rd_view.Items.Count; j++)
            {
                CheckBox chk_class = rd_view.Items[j].FindControl("chk_class") as CheckBox;
                if (chk_all.Checked)
                {
                    chk_class.Checked = true;
                }
                else
                {
                    chk_class.Checked = false;
                }
            }
        }

        private void insert_fee_to_form_sale(string fee, string class_id, string class_name)
        {
            SqlCommand cmd;
            DataTable dt = mycode.FillData("Select * from Form_sale_fee_classwise where Session_id='" + ddl_session.SelectedValue + "' and Class_id='" + class_id + "'");
            if (dt.Rows.Count == 0)
            {
                string query = "INSERT INTO Form_sale_fee_classwise (Session_id,Class_id,Fee_amount,Updated_by,Updated_date,Time,Status) values (@Session_id,@Class_id,@Fee_amount,@Updated_by,@Updated_date,@Time,@Status)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Session_id", ddl_session.SelectedValue);
                cmd.Parameters.AddWithValue("@Class_id", class_id);
                cmd.Parameters.AddWithValue("@Fee_amount", My.toDouble(fee).ToString("0.00"));
                cmd.Parameters.AddWithValue("@Updated_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Updated_date", mycode.date());
                cmd.Parameters.AddWithValue("@Time", mycode.time());
                cmd.Parameters.AddWithValue("@Status", "1");
                if (My.InsertUpdateData(cmd))
                {
                    ViewState["Is_add"] = "1";
                }
            }
            else
            {
                string query = "update Form_sale_fee_classwise set Fee_amount=@Fee_amount,Updated_by=@Updated_by,Updated_date=@Updated_date,Time=@Time where Session_id=@Session_id and Class_id=@Class_id";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Session_id", ddl_session.SelectedValue);
                cmd.Parameters.AddWithValue("@Class_id", class_id);
                cmd.Parameters.AddWithValue("@Fee_amount", My.toDouble(fee).ToString("0.00"));
                cmd.Parameters.AddWithValue("@Updated_by", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Updated_date", mycode.date());
                cmd.Parameters.AddWithValue("@Time", mycode.time());
                if (My.InsertUpdateData(cmd))
                {
                    ViewState["Is_add"] = "1";
                }
            }
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_session_serach.SelectedItem.Text == "Select")
                {
                    ddl_session_serach.Focus();
                    Alertme("Please select session.", "warning");
                }
                else
                {
                    find_data();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_data()
        {
            try
            {
                string query1 = "Select t1.*,(select top 1 Session from session_details where session_id=t1.Session_id) as Session_name,t2.Course_Name from Form_sale_fee_classwise t1 join Add_course_table t2 on t1.Class_id=t2.course_id where t1.session_id='" + ddl_session_serach.SelectedValue + "' and t1.Status=1  order by t2.Position asc";
                DataTable dt1 = mycode.FillData(query1);
                if (dt1.Rows.Count == 0)
                {
                    rd_viewaddedfee.DataSource = null;
                    rd_viewaddedfee.DataBind();
                }
                else
                {
                    rd_viewaddedfee.DataSource = dt1;
                    rd_viewaddedfee.DataBind();
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
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");
                    mycode.executequery("update Form_sale_fee_classwise set Status=555 where Id=" + lbl_Id.Text + "");
                    Alertme("Deletion process has been successfully done.", "success");
                    find_data();
                    btn_cancel.Visible = false;
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

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");
                    Label lbl_session_id = (Label)row.FindControl("lbl_session_id");
                    Label lbl_class_id = (Label)row.FindControl("lbl_class_id");
                    Label lbl_Amount = (Label)row.FindControl("lbl_Amount");

                    ddl_session.SelectedValue = lbl_session_id.Text;
                    //ddl_course.SelectedValue = lbl_class_id.Text;
                    ViewState["courseID"] = lbl_class_id.Text; 
                    Bind_course_fee_details();
                    txt_fee.Text= lbl_Amount.Text;
                    btn_cancel.Visible = true;
                    btn_Submit.Text = "Update";
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
    }
}