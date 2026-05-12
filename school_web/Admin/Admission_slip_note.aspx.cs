using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class Admission_slip_note : System.Web.UI.Page
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
                        try
                        {
                            const string quote = "\"";
                            string tinyMC = My.get_single_column_data("select TinyMC_link as Column_Name from Firm_Details");
                            if (tinyMC != "")
                            {
                                lt_meata.Text = "<script src=" + quote + tinyMC + quote + " referrerpolicy=" + quote + "origin" + quote + " ></script>";
                            }
                            else
                            {
                                lt_meata.Text = "<script src=" + quote + "https://cdn.tiny.cloud/1/h64ik3cu5x1uocom2fuu89jbo1ah2yqk1rtvpwu420y3ye4w/tinymce/7/tinymce.min.js" + quote + " referrerpolicy=" + quote + "origin" + quote + "></script>";
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                        ViewState["attechment"] = "0";
                        ViewState["courseID"] = "0";
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();
                        string pagename_current = "Online_Terms_Condition.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        mycode.bind_all_ddl_with_id(ddl_session, "Select Session,session_id from session_details order by Session");

                        ddl_session.SelectedValue = My.get_session_id();

                        fetch_data();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Online_Terms_Condition");
            }
        }

        private void fetch_data()
        {
            string query = "Select * from Admission_slip_Remarks where Session_id='" + ddl_session.SelectedValue + "'";


            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count != 0)
            {
                txt_info.Value = dt.Rows[0]["Remarks"].ToString();
                hd_id.Value = dt.Rows[0]["Id"].ToString();
                btn_add.Text = "Update";


            }
            else
            {
                txt_info.Value = "";
                btn_add.Text = "Add";
                hd_id.Value = "";

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

        protected void btn_add_Click(object sender, EventArgs e)
        {
            try
            {

                if (ViewState["Is_add"].ToString() == "1")
                {


                    update_images();
                    fetch_data();

                }
                else if (ViewState["Is_Edit"].ToString() == "1")
                {




                    update_images();
                    fetch_data();

                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }

            }
            catch (Exception ex)
            {
            }
        }

        private void update_images()
        {
            try
            {
                ViewState["isSuccess"] = "0";
                save_guidelines();

            }
            catch (Exception ex)
            {
            }
        }

        private void save_guidelines()
        {
            DataTable dtD = My.dataTable("select * from Admission_slip_Remarks where Session_id='" + ddl_session.SelectedValue + "'  ");
            if (dtD.Rows.Count > 0)
            {
                SqlCommand cmd;
                string query = "Update Admission_slip_Remarks set Remarks=@Remarks,created_date=@created_date,User_id=@User_id where Session_id='" + ddl_session.SelectedValue + "' ";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Remarks", txt_info.Value);
                cmd.Parameters.AddWithValue("@created_date", mycode.date());
                cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                if (InsertUpdate.InsertUpdateData(cmd))
                {
                    Alertme("The admission slip note has been successfully updated.", "success");
                }
            }
            else
            {
                SqlCommand cmd;
                string query = "INSERT INTO Admission_slip_Remarks (Remarks,created_date,User_id,Session_id) values (@Remarks,@created_date,@User_id,@Session_id)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Remarks", txt_info.Value);
                cmd.Parameters.AddWithValue("@created_date", mycode.date());
                cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                cmd.Parameters.AddWithValue("@Session_id", ddl_session.SelectedValue);

                if (InsertUpdate.InsertUpdateData(cmd))
                {
                    Alertme("The admission slip note has been successfully added.", "success");
                }
            }
        }
    }
}