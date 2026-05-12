 
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
    public partial class Enquiry_Type : System.Web.UI.Page
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
                        lbl_type.Text = "Add Purpose";
                        lbl_type_head.Text = "Purpose";
                        lbl_type1.Text = "Purpose List";
                        lnk_Purpose.CssClass = "active";
                        lnk_Source.CssClass = "deactive";
                        lnk_Reference.CssClass = "deactive";
                        bind_grd_view();

                        string pagename_current = "Enquiry_Type.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Enquiry_Type");
            }
        }
        My mycode = new My();
        private void bind_grd_view()
        {
            DataTable dt = mycode.FillData("select * from Enquiry_Type where Setup_Type='" + lbl_type_head.Text + "' order by Enquiry_Type");
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no record ", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();


                lbl_head.Text = lbl_type_head.Text;



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

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            txt_name.Text = "";

            btn_cancel.Visible = false;
            btn_Submit.Text = "Add";
            Response.Redirect("Enquiry_Type.aspx", false);
        }

        protected void btn_Submit_Click1(object sender, EventArgs e)
        {
            if (txt_name.Text == "")
            {
                Alertme("Please enter designation name", "warning");
                txt_name.Focus();
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
                if (ViewState["Is_Edit"].ToString() == "1")
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
            DataTable dt = mycode.FillData("select * from Enquiry_Type where Enquiry_Type='" + txt_name.Text + "' and  Setup_Type='" + lbl_type_head.Text + "' and Id!=" + hd_id.Value + "");
            if (dt.Rows.Count == 0)
            {
                string Enquiry_Id = create_sl_no();
                SqlCommand cmd;
                string query = "Update Enquiry_Type set Enquiry_Type=@Enquiry_Type,Updated_date_time=DATEADD(hour,5,DATEADD(Minute,30,GETutcDATE())),Updated_By=@Updated_By,Setup_Type=@Setup_Type where Id = @Id";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Enquiry_Type", txt_name.Text.Trim());
                cmd.Parameters.AddWithValue("@Updated_By", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Id", hd_id.Value);
                cmd.Parameters.AddWithValue("@Setup_Type", lbl_type_head.Text);
                if (My.InsertUpdateData(cmd))
                {
                    Alertme("Record has been updated", "success");
                }
            }
            else
            {
                Alertme("Your record is already exist", "warning");
            }
        }

        private void submit_details()
        {
            DataTable dt = mycode.FillData("select * from Enquiry_Type where Enquiry_Type='" + txt_name.Text + "' and Setup_Type='" + lbl_type_head.Text + "'");
            if (dt.Rows.Count == 0)
            {
                string Enquiry_Id = create_sl_no();
                SqlCommand cmd;
                string query = "INSERT INTO Enquiry_Type (Enquiry_Type,Enquiry_Type_Id,Created_date_time,Created_By,Setup_Type) values (@Enquiry_Type,@Enquiry_Type_Id,DATEADD(hour,5,DATEADD(Minute,30,GETutcDATE())),@Created_By,@Setup_Type)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Enquiry_Type", txt_name.Text.Trim());
                cmd.Parameters.AddWithValue("@Enquiry_Type_Id", Enquiry_Id);
                cmd.Parameters.AddWithValue("@Created_By", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Setup_Type", lbl_type_head.Text);

                if (My.InsertUpdateData(cmd))
                {
                    Alertme("Record has been saved successfully", "success");
                }
            }
            else
            {
                Alertme("Your record is already exist", "warning");
            }
        }

        private string create_sl_no()
        {
            bool duplicate = true;
            string Enquiry_Type_Id = My.auto_serialS("Enquiry_Type_Id");
            while (duplicate)
            {
                DataTable cdt = My.dataTable(" select Enquiry_Type_Id from dbo.[Enquiry_Type] where Enquiry_Type_Id='" + Enquiry_Type_Id + "'");
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    duplicate = false;
                }
                else
                {
                    Enquiry_Type_Id = My.auto_serialS("Enquiry_Type_Id");
                }
            }
            return Enquiry_Type_Id;
        }

        private void empty_form()
        {
            txt_name.Text = "";

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
                    Label lbl_name = (Label)row.FindControl("lbl_name");
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");
                    hd_id.Value = lbl_Id.Text;
                    txt_name.Text = lbl_name.Text;
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
            if (ViewState["Is_Edit"].ToString() == "1")
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                Label lbl_Enquiry_Type_Id = (Label)row.FindControl("lbl_Enquiry_Type_Id");


                if (My.dataTable("select Enquiry_Type_Id from Enquiry_Details where Enquiry_Type_Id='" + lbl_Enquiry_Type_Id.Text + "'").Rows.Count == 0)
                {
                    My.exeSql("delete from Enquiry_Type where Id='" + lbl_Id.Text + "'");
                    My.send_data_to_user_log_history(ViewState["Userid"].ToString(), ViewState["Userid"].ToString() + " delete hostel master on " + DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("dd/MM/yyyy hh:mm:ss tt"));
                    Alertme("Recode has been deleted Successfully", "success");
                }
                else
                {

                    Alertme("You can't delete this record. There is record type associated with student enquiry", "warning");
                }
                bind_grd_view();
            }
            else
            {
                Alertme(My.get_restricted_message(), "warning");
            }
        }


        #region type heading
        protected void lnk_Purpose_Click(object sender, EventArgs e)
        {
            lbl_type.Text = "Add Purpose";
            lbl_type_head.Text = "Purpose";
            lbl_type1.Text = "Purpose List";
            lnk_Purpose.CssClass = "active";
            lnk_Source.CssClass = "deactive";
            lnk_Reference.CssClass = "deactive";

            bind_grd_view();
        }

        protected void lnk_Source_Click(object sender, EventArgs e)
        {
            lbl_type.Text = "Add Source";
            lbl_type_head.Text = "Source";
            lbl_type1.Text = "Source List";
            lnk_Source.CssClass = "active";


            lnk_Purpose.CssClass = "deactive";
            lnk_Reference.CssClass = "deactive";
            bind_grd_view();
        }

        protected void lnk_Reference_Click(object sender, EventArgs e)
        {
            lbl_type.Text = "Add Reference";
            lbl_type_head.Text = "Reference";
            lbl_type1.Text = "Reference List";
            lnk_Reference.CssClass = "active";

            lnk_Purpose.CssClass = "deactive";
            lnk_Source.CssClass = "deactive";
            bind_grd_view();
        }
        #endregion

        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {

                string heading = "Export_" + lbl_type_head.Text + " List" + mycode.date() + mycode.itime() + "xls";

                if (ViewState["Is_Download"].ToString() == "1")
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=" + heading);
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    using (StringWriter sw = new StringWriter())
                    {
                        HtmlTextWriter hw = new HtmlTextWriter(sw);
                        Panel1.RenderControl(hw);
                        string style = @"<style> TABLE { border: 1px solid black; } TD { border: 1px solid black; } </style> ";
                        Response.Write(style);
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