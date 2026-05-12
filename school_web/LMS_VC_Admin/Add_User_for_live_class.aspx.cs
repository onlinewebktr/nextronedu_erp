using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using school_web.AppCode;
using System.Data;
namespace school_web.LMS_VC_Admin
{
    public partial class Add_User_for_live_class : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
        protected void Page_Load(object sender, EventArgs e)
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
                    code.bind_all_ddl_with_id(ddl_Designation, "Select Designation_Name, Designation_id from Designation_master order by Designation_Name");
                    ViewState["User"] = Session["Admin"].ToString();
                    Bind_Class();
                    Bid_grid();
                }
            }
        }

        private void Bind_Class()
        {

            DataTable dt = code.FillTable("Select  Course_Name,course_id from Add_course_table order by Position asc");

            if (dt.Rows.Count == 0)
            {

                grd_class.DataSource = null;
                grd_class.DataBind();
            }
            else
            {
                //gridview.Visible = true;
                grd_class.DataSource = dt;
                grd_class.DataBind();
            }
        }
        private void Bid_grid()
        {
            DataTable dt = code.FillTable("Select  *,(select top 1 Designation_Name from Designation_master where Designation_id=Live_Class_User_Master.Designation_id ) as Designation_Name from Live_Class_User_Master   order by Name asc");

            if (dt.Rows.Count == 0)
            {

                RPDetails.DataSource = null;
                RPDetails.DataBind();
            }
            else
            {
                //gridview.Visible = true;
                RPDetails.DataSource = dt;
                RPDetails.DataBind();
            }
        }

        #region add
        string scrpt;
        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {

                if (ddl_Designation.SelectedItem.Text == "Select")
                {
                    lblmessage.Text = "Please select designation";
                    scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
                }

                else
                {
                    if (btn_Submit.Text == "Create")
                    {


                        if (code.IsExist("select User_Id from Live_Class_User_Master where User_Id='" + txt_usrid.Text + "'  "))
                        {

                            SqlCommand cmd;
                            string strQuery = @"INSERT INTO Live_Class_User_Master (User_Id,Passowrd,Name,Designation_id,Mobile_no,Istatus,Created_By,Date,Idate,time) values (@User_Id,@Passowrd,@Name,@Designation_id,@Mobile_no,@Istatus,@Created_By,@Date,@Idate,@time)";
                            cmd = new SqlCommand(strQuery);
                            cmd.Parameters.AddWithValue("@User_Id", txt_usrid.Text);
                            cmd.Parameters.AddWithValue("@Passowrd", txt_password.Text);
                            cmd.Parameters.AddWithValue("@Name", txt_name.Text);
                            cmd.Parameters.AddWithValue("@Designation_id", ddl_Designation.SelectedValue);
                            cmd.Parameters.AddWithValue("@Mobile_no", txt_mobileno.Text);
                            cmd.Parameters.AddWithValue("@Istatus", "1");
                            cmd.Parameters.AddWithValue("@Created_By", ViewState["User"].ToString());
                            cmd.Parameters.AddWithValue("@Date", code.date());
                            cmd.Parameters.AddWithValue("@Idate", code.idate());
                            cmd.Parameters.AddWithValue("@time", code.time());
                            if (InsertUpdate.InsertUpdateData(cmd))
                            {
                                maped_class_with_user(txt_usrid.Text);
                                lblmessage.Text = "User details has been added sucssfully";
                                scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
                                hd_regid.Value = "0";
                                code.ClearInputs(Page.Controls);
                                Bind_Class();
                                Bid_grid();
                            }

                        }
                        else
                        {
                            lblmessage.Text = "Sorry, this user id already exists in our system. So Please change the user id";
                            scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
                        }

                    }
                    else
                    {

                        if (code.IsExist("select User_Id from Live_Class_User_Master where User_Id='" + txt_usrid.Text + "' and   Id!=" + HdID.Value + "   "))
                        {
                            string query = "Update Live_Class_User_Master set  Passowrd=@Passowrd,Name=@Name,Designation_id=@Designation_id,Mobile_no=@Mobile_no,Istatus=@Istatus,Date=@Date,Idate=@Idate,time=@time where Id = @Id";
                            SqlCommand cmd = new SqlCommand(query);

                            cmd.Parameters.AddWithValue("@Passowrd", txt_password.Text);
                            cmd.Parameters.AddWithValue("@Name", txt_name.Text);
                            cmd.Parameters.AddWithValue("@Designation_id", ddl_Designation.SelectedValue);
                            cmd.Parameters.AddWithValue("@Mobile_no", txt_mobileno.Text);
                            cmd.Parameters.AddWithValue("@Istatus", "1");
                            cmd.Parameters.AddWithValue("@Created_By", ViewState["User"].ToString());
                            cmd.Parameters.AddWithValue("@Date", code.date());
                            cmd.Parameters.AddWithValue("@Idate", code.idate());
                            cmd.Parameters.AddWithValue("@time", code.time());
                            cmd.Parameters.AddWithValue("@Id", HdID.Value);

                            if (InsertUpdate.InsertUpdateData(cmd))
                            {
                                maped_class_with_user(txt_usrid.Text);

                                lblmessage.Text = "User details has been updated sucssfully";
                                scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
                                btn_Submit.Text = "Create";
                                btn_cancel.Visible = false;
                                txt_usrid.Enabled = true;
                                hd_regid.Value = "0";
                                HdID.Value = "0";
                              
                               
                                code.ClearInputs(Page.Controls);
                                Bind_Class();
                                Bid_grid();
                            }
                        }
                        else
                        {
                            lblmessage.Text = "Sorry, this user id already exists in our system. So Please change the user id";
                            scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
                        }



                    }

                }

            }
            catch (Exception ex)
            {
                UsesCode.submitexception(ex.ToString());
            }
        }

        private void maped_class_with_user(string userid)
        {

            int growcount = grd_class.Rows.Count;
            int k = 0;
            code.executequery("delete  from Live_Class_User_maped_with_class where User_Id='" + userid + "'  ");
            for (int i = 0; i < growcount; i++)
            {
                CheckBox chk = (CheckBox)grd_class.Rows[i].FindControl("rowChkBox");
                if (chk.Checked == true)
                {

                    Label lbl_CategoryID = (Label)grd_class.Rows[i].FindControl("lbl_CategoryID");

                    finalmapped(lbl_CategoryID.Text, userid);



                }
                else
                {
                    k++;
                }
            }

            if (k == growcount)
            {

                lblmessage.Text = "Please check minimum one class in the class list.";
                scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
            }
            else
            {


                Bind_Class();
            }
        }

        private void finalmapped(string classid, string userid)
        {

            DataTable dt = code.FillTable("Select * from Live_Class_User_maped_with_class where User_Id='" + userid + "' and Class_id=" + classid + "  ");
            if (dt.Rows.Count == 0)
            {
                SqlCommand cmd1;
                string strQuery = @"INSERT INTO Live_Class_User_maped_with_class (User_Id,Class_id) values (@User_Id,@Class_id);";
                cmd1 = new SqlCommand(strQuery);
                cmd1.Parameters.AddWithValue("@User_Id", userid);
                cmd1.Parameters.AddWithValue("@Class_id", classid);

                if (InsertUpdate.InsertUpdateData(cmd1))
                {

                }
            }
            else
            {

            }
        }


        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            btn_Submit.Text = "Create"; btn_cancel.Visible = false;
            ltUsertop.Text = "Create User"; code.ClearInputs(Page.Controls);

            txt_usrid.Enabled = true;
            hd_regid.Value = "0";
            HdID.Value = "0";
           
            Bind_Class();
        }




        #endregion

        #region edit
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_User_Id = (Label)row.FindControl("lbl_User_Id");
                Label lblName = (Label)row.FindControl("lblName");
                Label lbl_Mobile_no = (Label)row.FindControl("lbl_Mobile_no");
                Label lbl_desgnatioid = (Label)row.FindControl("lbl_desgnatioid");
                Label lbl_Passowrd = (Label)row.FindControl("lbl_Passowrd");
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                code.bind_all_ddl_with_id(ddl_Designation, "Select Designation_Name, Designation_id from Designation_master order by Designation_Name");
                ddl_Designation.SelectedValue = lbl_desgnatioid.Text;
                HdID.Value = lbl_Id.Text;
                txt_name.Text = lblName.Text;
                txt_mobileno.Text = lbl_Mobile_no.Text;
                txt_usrid.Text = lbl_User_Id.Text;
                txt_usrid.Enabled = false;
                txt_password.Text = lbl_Passowrd.Text;
                btn_Submit.Text = "Update";
                btn_cancel.Visible = true;
                ltUsertop.Text = "Edit User";
                Bind_Class();
            }
            catch (Exception EX)
            {

            }
        }
        #endregion

        protected void grd_class_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_CategoryID = (Label)e.Row.FindControl("lbl_CategoryID");
                CheckBox rowChkBox = (CheckBox)e.Row.FindControl("rowChkBox");
                bind_check_maped_or_not_maped(rowChkBox, lbl_CategoryID.Text);
            }
        }

        private void bind_check_maped_or_not_maped(CheckBox rowChkBox, string clssid)
        {

            try
            {

                DataTable dt = code.FillTable("Select *  from Live_Class_User_maped_with_class where User_Id='" + txt_usrid.Text + "' and   Class_Id=" + clssid + "");

                if (dt.Rows.Count == 0)
                {
                    rowChkBox.Checked = false;
                }
                else
                {
                    rowChkBox.Checked = true;
                }
            }
            catch
            {
                rowChkBox.Checked = false;
            }
        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            try
            {

                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_User_Id = (Label)row.FindControl("lbl_User_Id");
                code.executequery("delete Live_Class_User_Master  where User_Id='" + lbl_User_Id.Text + "' ");
                code.executequery("delete Live_Class_User_maped_with_class  where User_Id='" + lbl_User_Id.Text + "' ");
                lblmessage.Text = "User details has been deleted successfully";
                scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
                Bid_grid();

            }
            catch (Exception ex)
            {
               
            }
        }

        protected void lnk_view_Click(object sender, EventArgs e)
        {
            try
            {

                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;

                Label lblName = (Label)row.FindControl("lblName");
                Label lbl_Designation_Name = (Label)row.FindControl("lbl_Designation_Name");
                Label lbl_User_Id = (Label)row.FindControl("lbl_User_Id");
                lbl_username.Text = lblName.Text;
                lblDesignation.Text = lbl_Designation_Name.Text;
                Bind_mapedclass(lbl_User_Id.Text);
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "SHow();", true);
            }
            catch (Exception ex)
            {

            }
        }

        private void Bind_mapedclass(string User_Id)
        {
            SqlCommand cmd = new SqlCommand("Select cm.CategoryName,cm.CategoryID from ClassMaster cm join Live_Class_User_maped_with_class sem  on sem.Class_id=cm.CategoryID    where sem.User_Id='" + User_Id + "'  order by cm.Position asc");
            DataTable dt = UsesCode.GetData(cmd);

            if (dt.Rows.Count == 0)
            {

                grd_view.DataSource = null;
                grd_view.DataBind();
            }
            else
            {
                //gridview.Visible = true;
                grd_view.DataSource = dt;
                grd_view.DataBind();
            }
        }


    }
}