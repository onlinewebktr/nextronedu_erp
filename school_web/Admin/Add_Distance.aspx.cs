using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
namespace school_web.Admin
{
    public partial class Add_Distance : System.Web.UI.Page
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
                        ViewState["firm_id"] = Session["firm"].ToString();
                        string pagename_current = "Update_Class_and_Admission_No.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];


                        ViewState["branch_id"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        bind_grd_view();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_Distance");
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

        My mycode = new My();
        private void bind_grd_view()
        {
            DataTable dt = mycode.FillData("select * from dbo.[Transport_distance_Meter]");
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

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            txt_end_km.Text = "";
            btn_cancel.Visible = false;
            btn_Submit.Text = "Add";
        }

        protected void btn_Submit_Click1(object sender, EventArgs e)
        {

            if (txt_end_km.Text == "")
            {
                Alertme("Please Enter End km", "warning");
                txt_end_km.Focus();
                return;
            }

            if (btn_Submit.Text == "Add")
            {
                if (ViewState["Is_add"].ToString() == "1")
                {
                    submit_details();
                    empty_form();
                    bind_grd_view();
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            else
            {
                if (ViewState["Is_add"].ToString() == "1")
                {
                    update_update_details();
                    empty_form();
                    bind_grd_view();
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
        }

        private void update_update_details()
        {
            DataTable class_dt = My.dataTable("select End_KM from dbo.[Transport_distance_Meter] where End_KM='" + txt_end_km.Text + "' and Id!='"+hd_id.Value+"'");
            if (class_dt.Rows.Count == 0)
            {
                SqlConnection conn = new SqlConnection(My.conn);
                SqlDataAdapter ad = new SqlDataAdapter(" select * from dbo.[Transport_distance_Meter] where  id='" + hd_id.Value + "'", conn);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                DataTable dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    dr["Start_KM"] = txt_startkm.Text;
                    dr["End_KM"] = txt_end_km.Text;
                    dr["Updated_By"] = ViewState["Userid"].ToString();
                    dr["Updated_Date"] = My.getdate1();
                }
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(dt);
                Alertme("Catogery Details Updated Successfully", "success");
            }
            else
            {
                Alertme("this distance already added  ", "warning");
            }
         
        }

        private void submit_details()
        {
            DataTable class_dt = My.dataTable("select End_KM from dbo.[Transport_distance_Meter] where End_KM='" + txt_end_km.Text + "'");
            if (class_dt.Rows.Count == 0)
            {
                SqlConnection conn = new SqlConnection(My.conn);
                SqlDataAdapter ad = new SqlDataAdapter(" select * from dbo.[Transport_distance_Meter]   ", conn);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                DataTable dt = ds.Tables[0];

                DataRow dr = dt.NewRow();
                dr["Start_KM"] = txt_startkm.Text;
                dr["End_KM"] = txt_end_km.Text;
                dr["Created_by"] = ViewState["Userid"].ToString();
                dr["Created_Date"] = My.getdate1();
                dr["Branch_id"] = ViewState["branch_id"].ToString();
                dr["Distance_id"] = My.auto_serialS("group_id");
                dt.Rows.Add(dr);
                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                ad.Update(dt);
                Alertme("Distanc master has been added sucessfully", "success");
            }
            else
            {
                Alertme("this distance already added  ", "warning");
            }

        }

        private void empty_form()
        {
            txt_end_km.Text = "";
            btn_cancel.Visible = false;
            btn_Submit.Text = "Add";
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_startkm = (Label)row.FindControl("lbl_startkm");
                    Label lbl_end_km = (Label)row.FindControl("lbl_end_km");
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");
                    hd_id.Value = lbl_Id.Text;
                    txt_end_km.Text = lbl_end_km.Text;
                    txt_startkm.Text = lbl_startkm.Text;
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

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            if (ViewState["Is_delete"].ToString() == "1")
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                Label lbl_Distance_id = (Label)row.FindControl("lbl_Distance_id");

                DataTable dsgdt = My.dataTable("select * from Transport_distance_Meter where Distance_id ='" + lbl_Distance_id.Text + "'");
                if (dsgdt.Rows.Count == 0)
                {

                    Alertme("Distance master has been deleted Successfully", "success");
                    bind_grd_view();
                }
                else
                {
                    Alertme("You can't delete this distance because this distance in Transportation Route", "warning");
                }
            }
            else
            {
                Alertme(My.get_restricted_message(), "warning");

            }
        }
    }
}