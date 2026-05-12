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
    public partial class Add_Session_Master : System.Web.UI.Page
    {
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
                    ViewState["Userid"] = Session["Admin"].ToString();
                    ViewState["firm_id"] = Session["firm"].ToString();
                    string pagename_current = Path.GetFileName(Request.Path);
                    Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                    ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                    ViewState["Is_delete"] = (String)dc1["Is_delete"];
                    ViewState["Is_Download"] = (String)dc1["Is_Download"];
                    ViewState["Is_Print"] = (String)dc1["Is_Print"];
                    ViewState["Is_add"] = (String)dc1["Is_add"];



                    Bind_Session();

                    // BindDetails();
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_Session_Master");
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
        private void Bind_Session()
        {
            DataTable dt = mycode.FillData("Select * from session_details order by Session asc   ");
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no branch list exist", "warning");
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
            btn_Submit.Text = "Add";
            btn_cancel.Visible = false;
            txt_session_from.Text = "";
            txtsession_to.Text = "";
            Bind_Session();

        }

        protected void btn_Submit_Click1(object sender, EventArgs e)
        {
            SqlCommand cmd;

            if (btn_Submit.Text == "Add")
            {
                if (ViewState["Is_add"].ToString() == "1")
                {
                    string session = txt_session_from.Text + "-" + txtsession_to.Text;
                    DataTable dt = mycode.FillData("Select * from session_details where Session='" + session + "'  ");
                    if (dt.Rows.Count == 0)
                    {
                        string createsessionid = cretesessionid();
                        string query = "INSERT INTO session_details (Session,session_id,User_id,Date,idate,Time) values (@Session,@session_id,@User_id,@Date,@idate,@Time)";
                        cmd = new SqlCommand(query);

                        cmd.Parameters.AddWithValue("@session_id", createsessionid);
                        cmd.Parameters.AddWithValue("@Session", session);
                        cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                        cmd.Parameters.AddWithValue("@Date", mycode.date());
                        cmd.Parameters.AddWithValue("@idate", mycode.idate());
                        cmd.Parameters.AddWithValue("@Time", mycode.time());
                        if (My.InsertUpdateData(cmd))
                        {
                            Alertme("Session has been save Successfully.", "success");
                            btn_Submit.Text = "Add";
                            txt_session_from.Text = "";
                            txtsession_to.Text = "";
                            Bind_Session();
                        }

                    }
                    else
                    {
                        txt_session_from.Focus();
                        Alertme("This Session already exist", "warning");
                        return;
                    }
                }
                else

                {
                    Alertme("SORRY! You have not permission for this work.", "warning");
                }

            }
            else
            {

                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    string session = txt_session_from.Text + "-" + txtsession_to.Text;
                    DataTable dt = mycode.FillData("Select * from session_details where Session='" + session + "' and  session_id!=" + hd_id.Value + " ");
                    if (dt.Rows.Count == 0)
                    {
                        string query = "Update session_details set Session=@Session,session_id=@session_id,User_id=@User_id,Date=@Date,idate=@idate,Time=@Time where session_id = @session_id";
                        cmd = new SqlCommand(query);

                        cmd.Parameters.AddWithValue("@session_id", hd_id.Value);
                        cmd.Parameters.AddWithValue("@Session", session);
                        cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                        cmd.Parameters.AddWithValue("@Date", mycode.date());
                        cmd.Parameters.AddWithValue("@idate", mycode.idate());
                        cmd.Parameters.AddWithValue("@Time", mycode.time());
                        if (My.InsertUpdateData(cmd))
                        {
                            Alertme("Session has been update Successfully.", "success");
                            btn_Submit.Text = "Add";
                            btn_cancel.Visible = false;
                            txt_session_from.Text = "";
                            txtsession_to.Text = "";
                            Bind_Session();

                        }

                    }
                    else
                    {
                        txt_session_from.Focus();
                        Alertme("This Session already exist", "warning");
                        return;
                    }
                }
                else
                {
                    Alertme("SORRY! You have not permission for this work.", "warning");
                }


            }


        }
        private string cretesessionid()
        {
            bool duplicate = false;
            string session_id = mycode.auto_serial("session_id");
            while (!duplicate)
            {
                DataTable cdt = mycode.FillData("  select session_id from dbo.[session_details] where session_id='" + session_id + "'");
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    duplicate = true;
                }
                else
                {
                    duplicate = false;
                    session_id = mycode.auto_serial("session_id");
                }
            }
            return session_id;
        }
        protected void lnkDel_Click(object sender, EventArgs e)
        {
            if (ViewState["Is_delete"].ToString() == "1")
            {
                SqlCommand cmd;
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_sessionid = (Label)row.FindControl("lbl_sessionid");
                Label lbl_session = (Label)row.FindControl("lbl_session");
                if (is_true(lbl_session.Text))
                {
                    string query = "delete from  session_details where session_id=@session_id";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@session_id", lbl_sessionid.Text);
                    if (My.InsertUpdateData(cmd))
                    {
                        Alertme("Session has been delete Successfully.", "success");
                        Bind_Session();

                    }
                }
                else
                {
                    Alertme("You can't delete this session", "warning");
                    return;
                }
            }
            else
            {
                Alertme("SORRY! You have not permission for this work.", "warning");
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
                    Label lbl_sessionid = (Label)row.FindControl("lbl_sessionid");
                    Label lbl_session = (Label)row.FindControl("lbl_session");
                    hd_id.Value = lbl_sessionid.Text;
                    if (is_true(lbl_session.Text))
                    {
                        txt_session_from.Text = lbl_session.Text.Split('-')[0];
                        txtsession_to.Text = lbl_session.Text.Split('-')[1];
                        btn_cancel.Visible = true;
                        btn_Submit.Text = "Update";
                    }
                    else
                    {
                        Alertme("You can't edit this session", "warning");
                        return;
                    }
                }
                else
                {
                    Alertme("SORRY! You have not permission for this work.", "warning");
                }


            }
            catch
            {

            }
        }
        private bool is_true(string session)
        {
            if (mycode.FillData(" select top 1 * from dbo.[admission_registor] where session='"+ session + "'").Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #region region create year
        protected void btn_Create_Year_Click(object sender, EventArgs e)
        {
            Button lnk = (Button)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            Label lbl_sessionid = (Label)row.FindControl("lbl_sessionid");
            hd_sessionid.Value = lbl_sessionid.Text;
            Label lbl_session = (Label)row.FindControl("lbl_session");
            lbl_session_view.Text = lbl_session.Text;
            Bind_gride_data();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            lbl_msg.Text = "";
            btn_add_academic_year.Text = "Add";
            btn_cancel_acadmic_year.Visible = false;
        }

        private void Bind_gride_data()
        {
            DataTable dt = mycode.FillData("Select * from Session_Academic where Session_Id='" + hd_sessionid.Value + "' and Type='Yearwise'  order by Academic_Year ");
            if (dt.Rows.Count == 0)
            {
                grid_session_year.DataSource = null;
                grid_session_year.DataBind();
            }
            else
            {
                grid_session_year.DataSource = dt;
                grid_session_year.DataBind();
            }
        }
        protected void btn_add_academic_year_Click(object sender, EventArgs e)
        {
            try
            {
                lbl_msg.Text = "";
                if (txt_year.Text == "")
                {
                    lbl_msg.Text = "Please enter academic year";
                }
                else
                {
                    if (btn_add_academic_year.Text == "Add")
                    {
                        SqlCommand cmd;
                        DataTable dt = mycode.FillData("Select * from Session_Academic where Session_Id='" + hd_sessionid.Value + "' and  Academic_Year=" + txt_year.Text + " and Type='Yearwise' ");
                        if (dt.Rows.Count == 0)
                        {
                            string academicyer = create_sl_no();
                            string query = "INSERT INTO Session_Academic (Session,Session_Id,Academic_Year,Academic_Year_Id,user_id,Type) values (@Session,@Session_Id,@Academic_Year,@Academic_Year_Id,@user_id,@Type)";
                            cmd = new SqlCommand(query);
                            cmd.Parameters.AddWithValue("@Session", lbl_session_view.Text);
                            cmd.Parameters.AddWithValue("@Session_Id", hd_sessionid.Value);
                            cmd.Parameters.AddWithValue("@Academic_Year", txt_year.Text);
                            cmd.Parameters.AddWithValue("@Academic_Year_Id", academicyer);
                            cmd.Parameters.AddWithValue("@user_id", ViewState["Userid"].ToString());
                            cmd.Parameters.AddWithValue("@Type", "Yearwise");

                            if (My.InsertUpdateData(cmd))
                            {
                                lbl_msg.Text = "Academic year has been added Successfully.";
                                btn_add_academic_year.Text = "Add";
                                btn_cancel_acadmic_year.Visible = false;
                                txt_year.Text = "";
                                Bind_gride_data();

                            }

                        }
                        else
                        {
                            lbl_msg.Text = "Sorry! Your academic year already added";

                        }
                    }
                    else
                    {
                        SqlCommand cmd;
                        DataTable dt = mycode.FillData("Select * from Session_Academic where Session_Id='" + hd_sessionid.Value + "' and  Academic_Year=" + txt_year.Text + " and Type='Yearwise' and Academic_Year_Id!=" + hd_id_Session_Academic + " ");
                        if (dt.Rows.Count == 0)
                        {
                            string query = "Update Session_Academic set Session=@Session,Session_Id=@Session_Id,Academic_Year=@Academic_Year, user_id=@user_id where Academic_Year_Id = @Academic_Year_Id";
                            cmd = new SqlCommand(query);
                            cmd.Parameters.AddWithValue("@Session", lbl_session_view.Text);
                            cmd.Parameters.AddWithValue("@Session_Id", hd_sessionid.Value);
                            cmd.Parameters.AddWithValue("@Academic_Year", txt_year.Text);
                            cmd.Parameters.AddWithValue("@Academic_Year_Id", hd_id_Session_Academic.Value);
                            cmd.Parameters.AddWithValue("@user_id", ViewState["Userid"].ToString());
                            cmd.Parameters.AddWithValue("@Type", "Yearwise");
                            if (My.InsertUpdateData(cmd))
                            {
                                lbl_msg.Text = "Academic year has been added Successfully.";
                                btn_add_academic_year.Text = "Add";
                                btn_cancel_acadmic_year.Visible = false;
                                txt_year.Text = "";
                                Bind_gride_data();

                            }

                        }
                        else
                        {
                            lbl_msg.Text = "Sorry! Your academic year already added";

                        }


                    }

                }
            }
            catch
            {
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        private string create_sl_no()
        {

            bool duplicate = false;
            string acamedic_id = mycode.auto_serial("Academic_Year_Id");
            while (!duplicate)
            {
                DataTable cdt = mycode.FillData("  select Academic_Year_Id from dbo.[Session_Academic] where Academic_Year_Id='" + acamedic_id + "'");
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    duplicate = true;
                }
                else
                {
                    duplicate = false;
                    acamedic_id = mycode.auto_serial("Academic_Year_Id");
                }
            }
            return acamedic_id;
        }
        protected void btn_cancel_acadmic_year_Click(object sender, EventArgs e)
        {
            btn_add_academic_year.Text = "Add";
            btn_cancel_acadmic_year.Visible = false;
            txt_year.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        protected void lnkEdit_sessionyear_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lnk.Parent.Parent;
                Label lbl_Academic_Year_Id = (Label)row.FindControl("lbl_Academic_Year_Id");
                Label lbl_Session_Id = (Label)row.FindControl("lbl_Session_Id");
                Label Session = (Label)row.FindControl("lbl_Session");
                Label Academic_Year = (Label)row.FindControl("lbl_Academic_Year");
                lbl_session_view.Text = Session.Text;
                txt_year.Text = Academic_Year.Text;
                hd_id_Session_Academic.Value = lbl_Academic_Year_Id.Text;
                hd_sessionid.Value = lbl_Session_Id.Text;
                lbl_msg.Text = "";
                btn_add_academic_year.Text = "Update";
                btn_cancel_acadmic_year.Visible = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            catch
            {
            }


        }

        protected void lnkDel_sessionyear_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lnk.Parent.Parent;
                Label lbl_Academic_Year_Id = (Label)row.FindControl("lbl_Academic_Year_Id");
                Label lbl_Session_Id = (Label)row.FindControl("lbl_Session_Id");
                hd_sessionid.Value = lbl_Session_Id.Text;
                mycode.executequery("delete from Session_Academic where Academic_Year_Id=" + lbl_Academic_Year_Id.Text + "");
                lbl_msg.Text = "Academic year has been deleted successfully";
                Bind_gride_data();

                lbl_msg.Text = "";
                btn_add_academic_year.Text = "Update";
                btn_cancel_acadmic_year.Visible = true;
                txt_year.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

            }
            catch
            {
            }
        }
        #endregion

        #region add semester
        protected void btn_create_Sem_Click(object sender, EventArgs e)
        {
            Button lnk = (Button)sender;
            RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
            Label lbl_sessionid = (Label)row.FindControl("lbl_sessionid");
            hd_sessionid.Value = lbl_sessionid.Text;
            Label lbl_session = (Label)row.FindControl("lbl_session");
            lbl_viewsession_sem.Text = lbl_session.Text;
            Bind_gride_data_acd_sem();
            btn_add_semester.Text = "Add";
            btn_semester_cancel.Visible = false;
            lbl_msg1.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);
        }

        private void Bind_gride_data_acd_sem()
        {
            DataTable dt = mycode.FillData("Select * from Session_Academic where Session_Id='" + hd_sessionid.Value + "' and Type='Semesterwise'  order by Acamedic_Semester ");
            if (dt.Rows.Count == 0)
            {
                grid_Semester.DataSource = null;
                grid_Semester.DataBind();
            }
            else
            {
                grid_Semester.DataSource = dt;
                grid_Semester.DataBind();
            }
        }
        protected void btn_add_semester_Click(object sender, EventArgs e)
        {
            try
            {
                lbl_msg1.Text = "";
                if (txt_academic_semester.Text == "")
                {
                    lbl_msg1.Text = "Please enter academic semester";
                }
                else
                {
                    if (btn_add_semester.Text == "Add")
                    {
                        SqlCommand cmd;
                        DataTable dt = mycode.FillData("Select * from Session_Academic where Session_Id='" + hd_sessionid.Value + "' and  Acamedic_Semester=" + txt_academic_semester.Text + " and Type='Semesterwise' ");
                        if (dt.Rows.Count == 0)
                        {
                            string AcamedicSemester = create_Acamedic_Semesterid();
                            string query = "INSERT INTO Session_Academic (Session,Session_Id,Acamedic_Semester,Acamedic_Semester_Id,user_id,Type) values (@Session,@Session_Id,@Acamedic_Semester,@Acamedic_Semester_Id,@user_id,@Type)";
                            cmd = new SqlCommand(query);
                            cmd.Parameters.AddWithValue("@Session", lbl_viewsession_sem.Text);
                            cmd.Parameters.AddWithValue("@Session_Id", hd_sessionid.Value);
                            cmd.Parameters.AddWithValue("@Acamedic_Semester", txt_academic_semester.Text);
                            cmd.Parameters.AddWithValue("@Acamedic_Semester_Id", AcamedicSemester);
                            cmd.Parameters.AddWithValue("@user_id", ViewState["Userid"].ToString());
                            cmd.Parameters.AddWithValue("@Type", "Semesterwise");

                            if (My.InsertUpdateData(cmd))
                            {
                                lbl_msg1.Text = "Academic semester has been added Successfully.";
                                btn_add_semester.Text = "Add";
                                btn_semester_cancel.Visible = false;
                                txt_academic_semester.Text = "";
                                Bind_gride_data_acd_sem();

                            }

                        }
                        else
                        {
                            lbl_msg1.Text = "Sorry! Your academic semester already added";

                        }
                    }
                    else
                    {
                        SqlCommand cmd;
                        DataTable dt = mycode.FillData("Select * from Session_Academic where Session_Id='" + hd_sessionid.Value + "' and  Acamedic_Semester=" + txt_year.Text + " and Type='Semesterwise' and Id!=" + hd_id_Session_Academic + " ");
                        if (dt.Rows.Count == 0)
                        {
                            string query = "Update Session_Academic set Session=@Session,Session_Id=@Session_Id,Acamedic_Semester=@Acamedic_Semester, user_id=@user_id where Id = @Id";
                            cmd = new SqlCommand(query);
                            cmd.Parameters.AddWithValue("@Session", lbl_viewsession_sem.Text);
                            cmd.Parameters.AddWithValue("@Session_Id", hd_sessionid.Value);
                            cmd.Parameters.AddWithValue("@Academic_Year", txt_year.Text);
                            cmd.Parameters.AddWithValue("@Id", hd_id_Session_Academic.Value);
                            cmd.Parameters.AddWithValue("@user_id", ViewState["Userid"].ToString());
                            cmd.Parameters.AddWithValue("@Type", "Semesterwise");
                            if (My.InsertUpdateData(cmd))
                            {
                                lbl_msg1.Text = "Academic year has been added Successfully.";
                                btn_add_semester.Text = "Add";
                                btn_semester_cancel.Visible = false;
                                txt_academic_semester.Text = "";
                                Bind_gride_data_acd_sem();

                            }

                        }
                        else
                        {
                            lbl_msg1.Text = "Sorry! Your academic year already added";

                        }


                    }

                }
            }
            catch
            {
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);
        }

        private string create_Acamedic_Semesterid()
        {
            bool duplicate = false;
            string acamedic_id = mycode.auto_serial("Acamedic_Semester_Id");
            while (!duplicate)
            {
                DataTable cdt = mycode.FillData("  select Acamedic_Semester_Id from dbo.[Session_Academic] where Acamedic_Semester_Id='" + acamedic_id + "'");
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    duplicate = true;

                }
                else
                {
                    duplicate = false;
                    acamedic_id = mycode.auto_serial("Acamedic_Semester_Id");
                }
            }
            return acamedic_id;
        }

        protected void btn_semester_cancel_Click(object sender, EventArgs e)
        {
            try
            {
                btn_add_semester.Text = "Add";
                btn_semester_cancel.Visible = false;
                txt_academic_semester.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);
            }
            catch
            {
            }
        }

        protected void lnkEdit_Semester_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lnk.Parent.Parent;
                Label lbl_Acamedic_Semester_Id = (Label)row.FindControl("lbl_Acamedic_Semester_Id");
                Label lbl_Session_Id_sem = (Label)row.FindControl("lbl_Session_Id_sem");
                Label lbl_Session = (Label)row.FindControl("lbl_Session");
                Label lbl_Acamedic_Semester = (Label)row.FindControl("lbl_Acamedic_Semester");
                lbl_viewsession_sem.Text = lbl_Session.Text;
                txt_academic_semester.Text = lbl_Acamedic_Semester.Text;
                hd_id_Session_Academic.Value = lbl_Acamedic_Semester_Id.Text;
                hd_sessionid.Value = lbl_Session_Id_sem.Text;
                lbl_msg1.Text = "";
                btn_add_semester.Text = "Update";
                btn_semester_cancel.Visible = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);
            }
            catch
            {
            }
        }

        protected void lnkDel_Semesterr_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lnk.Parent.Parent;
                Label lbl_Acamedic_Semester_Id = (Label)row.FindControl("lbl_Acamedic_Semester_Id");
                mycode.executequery("delete from Session_Academic where Acamedic_Semester_Id=" + lbl_Acamedic_Semester_Id.Text + "");
                lbl_msg1.Text = "Academic semester has been deleted successfully";
                Bind_gride_data_acd_sem();
                btn_add_semester.Text = "Add";
                btn_semester_cancel.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);
            }
            catch
            {
            }
        }
        #endregion
    }
}