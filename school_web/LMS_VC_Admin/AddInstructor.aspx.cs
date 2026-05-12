using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.LMS_VC_Admin
{
    public partial class AddInstructor : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    code.bind_ddl(ddl_zoomsl, "Select distinct sl_no from Zoom_API order by sl_no asc ");
                    bind_gridvew();
                    code.bind_all_list_with_id(lst_Teacher, "select Name,UserID from InstructorProfile order by Name  asc");

                   // Bind_data_list();
                }
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        private void Bind_data_list()
        {
            string query = "select UserID,Name, IIF(Istatus='0', 'Inactive', 'Active') as StatusD,(select Pswd from LoginMaster where UserId =InstructorProfile.UserID ) as Pswd from   InstructorProfile where  (Type!='Super Admin' or Type is null) order by Name  asc";

            DataTable dt = code.FillTable(query);
            if (dt.Rows.Count == 0)
            {


                datl_teacherlist.DataSource = null;

                datl_teacherlist.DataBind();

            }
            else
            {


                datl_teacherlist.DataSource = dt;
                datl_teacherlist.DataBind();
            }

        }

        private void bind_gridvew()
        {

            string query = "select *, IIF(Istatus='0', 'Inactive', 'Active') as StatusD,(select Pswd from LoginMaster where UserId =InstructorProfile.UserID ) as Pswd,(Select User_ID from Zoom_API where teacher_id=InstructorProfile.UserID) as zoomuserid,(Select Password from Zoom_API where teacher_id=InstructorProfile.UserID) as zoompwd from InstructorProfile where (Type!='Super Admin' or Type is null) order by Id Desc";
            Bind_gridedata(query);




        }

        private void Bind_gridedata(string query)
        {
            lbl_teachername.Text = "";
            lbl_old_zoomid.Text = "";
            lbluserid.Text = "";
            try
            {
                ViewState["query"] = query;

                DataTable dt = code.FillTable(query);
                if (dt.Rows.Count == 0)
                {
                    img_expor_excel.Visible = false;

                    RPDetails.DataSource = null;
                    RPDetails.DataBind();
                    ViewState["Data"] = null;
                }
                else
                {
                    ViewState["Data"] = dt;
                    img_expor_excel.Visible = true;
                    RPDetails.DataSource = dt;
                    RPDetails.DataBind();
                }
            }
            catch (Exception ex)
            {
                UsesCode.submitexception(ex.ToString());
            }


        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_Name.Text == "")
                {
                    Alert("Please enter teacher name."); txt_Name.Focus(); return;
                }

                if (txt_Phone.Text == "")
                {
                    Alert("Please enter phone no.."); txt_Phone.Focus(); return;
                }
                if (txt_UserName.Text == "")
                {
                    Alert("Please enter userid"); txt_UserName.Focus(); return;
                }

                if (btn_submit.Text == "Submit")
                {
                    if (txt_Pswd.Text == "")
                    {
                        Alert("Please enter password"); txt_Pswd.Focus(); return;
                    }

                    SqlCommand cmd;
                    if (code.IsExist("Select * from LoginMaster where UserId='" + txt_UserName.Text + "'"))
                    {
                        //if (code.IsExist("Select * from InstructorProfile where EmailID='" + txt_EmailID.Text + "'"))
                        //{

                        cmd = new SqlCommand("insert into InstructorProfile (UserID, Password, Name,EmailID,PhoneNo,StateID,RegDate,RegTime,Idate,Istatus,Allow_Virtual_class_creation) " +
                           "Values ('" + txt_UserName.Text + "','" + txt_Pswd.Text + "','" + txt_Name.Text + "','" + txt_EmailID.Text + "','" + txt_Phone.Text + "'," +
                           "'','" + code.date() + "','" + code.time() + "','" + code.idate() + "','0','1')");
                        if (InsertUpdate.InsertUpdateData(cmd))
                        {
                            cmd = new SqlCommand("insert into LoginMaster (UserId, Pswd, UserName,Type,Istatus) " +
                            "Values ('" + txt_UserName.Text + "','" + txt_Pswd.Text + "','" + txt_Name.Text + "','Data Operator','0')");
                            if (InsertUpdate.InsertUpdateData(cmd))
                            {
                            //    Global.update_teacher_list();
                                Alert("Registered successfully. Approval needed for login.");
                                code.bind_all_list_with_id(lst_Teacher, "select Name,UserID from InstructorProfile order by Name  asc");
                            }
                        }


                        //}
                        //else { Alert("Duplicate Email ID."); }
                    }
                    else { Alert("Duplicate UserId."); }
                }
                if (btn_submit.Text == "Update")
                {
                    if (txt_Pswd.Text != "")
                        hdpwd.Value = txt_Pswd.Text;
                    string qry = @"Update InstructorProfile set Password =  '" + hdpwd.Value + "',Name =  '" + txt_Name.Text + "',EmailID =  '" + txt_EmailID.Text + "',PhoneNo =  '" + txt_Phone.Text + "' where UserID =  '" + txt_UserName.Text + "' ;";
                   // qry = qry + @"Update user_details set password =  '" + hdpwd.Value + "',name =  '" + txt_Name.Text + "',mobile =  '" + txt_Phone.Text + "' where user_id =  '" + txt_UserName.Text + "' ;";
                    qry = qry + @"Update LoginMaster set Pswd =  '" + hdpwd.Value + "' where UserId =  '" + txt_UserName.Text + "' ;";

                    SqlCommand cmd = new SqlCommand(qry);
                    InsertUpdate.InsertUpdateData(cmd);
                    btn_submit.Text = "Submit";
                    Alert("Teacher info has been successfully Updated.");
                }
                txt_UserName.Text = ""; txt_EmailID.Text = ""; txt_Phone.Text = "";
                txt_Pswd.Text = ""; txt_Name.Text = "";
                bind_gridvew();
                Bind_data_list();
                //code.bind_gridview(GridView1, "select *, IIF(Istatus='0', 'Inactive', 'Active') as StatusD from InstructorProfile order by Id Desc");

            }

            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }
        public void Alert(string Message)
        {
            lbl_msg.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }





        private void UpdateDetail(string FieldName, string Value, string ID)
        {
            SqlCommand cmd;
            cmd = new SqlCommand("update InstructorProfile set " + FieldName + " = '" + Value + "' where UserID='" + ID + "'");
            if (InsertUpdate.InsertUpdateData(cmd))
            {
                cmd = new SqlCommand("update LoginMaster set Istatus = '" + Value + "' where UserId='" + ID + "'");
                InsertUpdate.InsertUpdateData(cmd); 
            }
            Bind_data_list();

        }


        protected void btn_cncel_Click(object sender, EventArgs e)
        {
            try
            {
                txt_UserName.Text = ""; txt_EmailID.Text = ""; txt_Phone.Text = "";
                txt_Pswd.Text = ""; txt_Name.Text = "";
                bind_gridvew();
                btn_submit.Text = "Submit";
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        #region search data
        protected void btn_find_dtudent_regid_Click(object sender, EventArgs e)
        {
            if (txt_userid_mobile_no.Text == "")
            {
                Alert("Please enter user id or mobile .");
            }
            else
            {
                string query = "select *, IIF(Istatus='0', 'Inactive', 'Active') as StatusD,(select Pswd from LoginMaster where UserId =InstructorProfile.UserID ) as Pswd from InstructorProfile where (UserID='" + txt_userid_mobile_no.Text + "' or PhoneNo='" + txt_userid_mobile_no.Text + "') order by Id Desc";
                Bind_gridedata(query);
            }
        }

        #endregion

        protected void img_expor_excel_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dt = ViewState["Data"] as DataTable;
            export_to_excel(dt, "TeacherList");
        }
        private void export_to_excel(DataTable dt, string file)
        {

            string FileName = file + DateTime.Now + ".xls";

            string attachment = "attachment; filename=" + FileName;

            Response.ClearContent();

            Response.AddHeader("content-disposition", attachment);

            Response.ContentType = "application/vnd.ms-excel";

            Response.ContentEncoding = System.Text.Encoding.Unicode;

            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());

            string tab = "";

            foreach (DataColumn dc in dt.Columns)
            {

                Response.Write(tab + dc.ColumnName);

                tab = "\t";

            }

            Response.Write("\n");

            int i;

            foreach (DataRow dr in dt.Rows)
            {

                tab = "";

                for (i = 0; i < dt.Columns.Count; i++)
                {

                    Response.Write(tab + dr[i].ToString());

                    tab = "\t";

                }

                Response.Write("\n");

            }

            Response.End();

        }




        protected void btn_reset_Click(object sender, EventArgs e)
        {
            txt_userid_mobile_no.Text = "";
            bind_gridvew();
        }

        #region update zoom id
        protected void btn_update_zoomid_Click(object sender, EventArgs e)
        {
            if (ddl_zoomsl.Text == "Select")
            {
                Alert("Please select zoom id");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            else
            {
                UsesCode.exeSql("update InstructorProfile set  Zoom_Api_Sl_No= '" + ddl_zoomsl.Text + "' where UserID='" + hd_teacheruserid.Value + "'");
                Alert("Zoom Sl. No. has been updated");
                Bind_gridedata(ViewState["query"].ToString());

            }

        }
        #endregion


        #region find data via datalist
        protected void btn_find_multipal_Click(object sender, EventArgs e)
        {
            try
            {
                string userid = "";
                string userid1 = "";
                int count = datl_teacherlist.Items.Count;
                //for (int i = 0; i < count; i++)
                //{
                //    Label lbl_UserID = datl_teacherlist.Items[i].FindControl("lbl_UserID") as Label;
                //    CheckBox chk = datl_teacherlist.Items[i].FindControl("chk_subjectname") as CheckBox;
                foreach (ListItem item in lst_Teacher.Items)
                {
                    if (item.Selected)
                    {
                        //if (chk.Checked == true)
                        //{
                        if (userid == "")
                        {
                            userid = "'" + item.Value + "'";
                        }
                        else
                        {
                            userid = userid + "," + "'" + item.Value + "'";

                        }
                    }


                }
                string query = "select *, IIF(Istatus='0', 'Inactive', 'Active') as StatusD,(select Pswd from LoginMaster where UserId =InstructorProfile.UserID ) as Pswd from InstructorProfile where UserID in (" + userid + ")  order by Id Desc";
                Bind_gridedata(query);


            }
            catch
            {
            }
        }

        #endregion

        protected void lnk_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                SqlCommand cmd;
                HiddenField hdfID = (HiddenField)row.FindControl("hdUserID");
                cmd = new SqlCommand("delete from InstructorProfile where UserID='" + hdfID.Value + "'");
                if (InsertUpdate.InsertUpdateData(cmd))
                {
                    cmd = new SqlCommand("delete from LoginMaster where UserId='" + hdfID.Value + "'");
                    InsertUpdate.InsertUpdateData(cmd);
                }
                Alert("Deleted successfully.");
                bind_gridvew();
                Bind_data_list();
            }
            catch { }
        }

        protected void lnk_edit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                HiddenField hdfID = (HiddenField)row.FindControl("hdUserID");
                HiddenField hdId = (HiddenField)row.FindControl("hdId");
                Label lbl_Email = (Label)row.FindControl("lbl_Email");
                Label lbl_PhoneNo = (Label)row.FindControl("lbl_PhoneNo");
                Label lbl_Password = (Label)row.FindControl("lbl_Password");
                Label lbl_UserID = (Label)row.FindControl("lbl_UserID");
                Label lbl_Name = (Label)row.FindControl("lbl_Name");
                hd_UserID.Value = hdfID.Value;
                hdid.Value = hdId.Value;
                hdpwd.Value = lbl_Password.Text;
                txt_UserName.Text = lbl_UserID.Text;
                txt_UserName.ReadOnly = true;
                txt_UserName.Enabled = false;
                txt_EmailID.Text = lbl_Email.Text;
                txt_Phone.Text = lbl_PhoneNo.Text;
                txt_Name.Text = lbl_Name.Text;
                btn_submit.Text = "Update";
                Bind_data_list();
            }
            catch { }
        }

        protected void lnkActive_Click(object sender, EventArgs e)
        {
            try
            {
                lblmsg.Text = "";
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                HiddenField hdfStatus = (HiddenField)row.FindControl("hdfActive");
                HiddenField hdUID = (HiddenField)row.FindControl("hdUserID");
                if (hdfStatus.Value == "1") { hdfStatus.Value = "0"; UpdateDetail("Istatus", hdfStatus.Value, hdUID.Value); }
                else { hdfStatus.Value = "1"; UpdateDetail("Istatus", hdfStatus.Value, hdUID.Value); }
                Alert("status has been changed.");
                bind_gridvew();
                Bind_data_list();
            }
            catch { }
        }

        protected void lnkedit_zoom_Click(object sender, EventArgs e)
        {
            try
            {
                lblmsg.Text = "";
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                HiddenField hdfID = (HiddenField)row.FindControl("hdUserID");
                Label lbl_Zoom_Api_Sl_No = (Label)row.FindControl("lbl_Zoom_Api_Sl_No");
                Label lbl_Name = (Label)row.FindControl("lbl_Name");
                hd_teacheruserid.Value = hdfID.Value;
                lbl_teachername.Text = lbl_Name.Text;
                lbl_old_zoomid.Text = lbl_Zoom_Api_Sl_No.Text;
                lbluserid.Text = hdfID.Value;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

            }
            catch { }
        }

        protected void RPDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {

                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {

                    if (((HiddenField)e.Item.FindControl("hdfActive")).Value == "1")
                    {
                        ((LinkButton)e.Item.FindControl("lnkActive")).Text = "Inactive";
                        ((Label)e.Item.FindControl("lblStatusD")).CssClass = "badge badge-success ml-2";


                    }
                    else
                    {
                        ((LinkButton)e.Item.FindControl("lnkActive")).Text = "Active";
                        ((Label)e.Item.FindControl("lblStatusD")).CssClass = "badge badge-danger ml-2";
                    }



                }
            }
            catch { }
        }

        protected void btn_update_emailidnad_password_Click(object sender, EventArgs e)
        {
            lblmsg.Text = "";
            int getslid = code.get_slid_max();
            if (txt_emailid1.Text == "Select")
            {
                Alert("Please enter password");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            else if (txt_password.Text == "")
            {
                Alert("Please enter password");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

            }
            else
            {
                
                DataTable dt = code.FillTable("Select sl_no from Zoom_API where teacher_id='" + hd_teacherid.Value + "'");
                if (dt.Rows.Count == 0)
                {

                    UsesCode.exeSql("INSERT INTO Zoom_API (User_ID,Password,teacher_id,Status,sl_no) values ('" + txt_emailid1.Text + "','" + txt_password.Text + "','" + hd_teacheruserid.Value + "','1','" + getslid + "')");
                    UsesCode.exeSql("update InstructorProfile set  Zoom_Api_Sl_No= '" + getslid + "',Individual_universal='Individual',Allow_Virtual_class_creation='1' where UserID='" + hd_teacheruserid.Value + "'");
                    Alert("Zoom user id and password has been updated");
                    lblmsg.Text = "Zoom user id and password has been updated";
                    txt_emailid1.Text = "";
                    txt_password.Text = "";
                }
                else
                {
                    UsesCode.exeSql("update Zoom_API set  User_ID= '" + txt_emailid1.Text + "', Password='" + txt_password.Text + "',sl_no='" + getslid + "'   where teacher_id='" + hd_teacheruserid.Value + "'");
                    UsesCode.exeSql("update InstructorProfile set  Zoom_Api_Sl_No= '" + getslid + "',Individual_universal='Individual',Allow_Virtual_class_creation='1' where UserID='" + hd_teacheruserid.Value + "'");
                    Alert("Zoom user id and password has been updated");
                    lblmsg.Text = "Zoom user id and password has been updated";
                    txt_emailid1.Text = "";
                    txt_password.Text = "";
                }


                Bind_gridedata(ViewState["query"].ToString());



            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);

        }
        protected void lnk_update_zoomuserid_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                HiddenField hdfID = (HiddenField)row.FindControl("hdUserID");
                Label lbl_Zoom_Api_Sl_No = (Label)row.FindControl("lbl_Zoom_Api_Sl_No");
                Label lbl_Name = (Label)row.FindControl("lbl_Name");
                hd_teacheruserid.Value = hdfID.Value;
                lbl_teachername.Text = lbl_Name.Text;
                lbl_old_zoomid.Text = lbl_Zoom_Api_Sl_No.Text;
                lbluserid.Text = hdfID.Value;
                hd_teacherid.Value = hdfID.Value;
                lbl_teachername1.Text = lbl_Name.Text;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);
            }
            catch { }
        }
    }
}