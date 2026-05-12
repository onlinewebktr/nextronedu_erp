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
    public partial class Add_Teacher : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
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
                    if (!IsPostBack)
                    { 
                        bind_gridvew();


                        // Bind_data_list();
                        ViewState["Admin"] = Session["Admin"].ToString();
                    }
                }
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        private void Bind_data_list()
        {
            string query = "select user_id as  UserID,name as Name, IIF(Istatus='0', 'Inactive', 'Active') as StatusD, password as Pswd from user_details where (User_Type='Teacher' or User_Type='Principal')  order by Name  asc";

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

            string query = "  select *, IIF(Istatus='0', 'Inactive', 'Active') as StatusD,password as Pswd,(Select top 1  User_ID from Zoom_API where teacher_id=user_details.user_id order by id desc) as templateid,(Select top 1 Password from Zoom_API where teacher_id=user_details.user_id order by id desc) as roomid,( Select top 1 lcc.Template_Name  from LiveClassCredential lcc join Zoom_API zapi on lcc.TemplateID=zapi.User_ID and zapi.teacher_id=user_details.user_id ) as Template_Name from user_details where (User_Type='Teacher' or User_Type='Principal' or User_Type='Coordinator') order by Id Desc";
            Bind_gridedata(query);




        }

        private void Bind_gridedata(string query)
        {
            lbl_teachername.Text = "";
            lbl_old_temaplatename.Text = "";
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
                    Alert("Please enter user id"); txt_UserName.Focus(); return;
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
                    string qry = @"Update InstructorProfile set Password =  '" + hdpwd.Value + "',Name =  '" + txt_Name.Text + "',EmailID =  '" + txt_EmailID.Text + "',PhoneNo =  '" + txt_Phone.Text + "' where UserID =  '" + txt_UserName.Text + "'";
                    // qry = qry + @"Update user_details set password =  '" + hdpwd.Value + "',name =  '" + txt_Name.Text + "',mobile =  '" + txt_Phone.Text + "' where user_id =  '" + txt_UserName.Text + "' ;";


                    SqlCommand cmd = new SqlCommand(qry);
                    InsertUpdate.InsertUpdateData(cmd);

                    if (code.IsExist("Select * from LoginMaster where UserId='" + txt_UserName.Text + "'"))
                    {
                        string qry1 = "insert into LoginMaster (UserId, Pswd, UserName,Type,Istatus) Values ('" + txt_UserName.Text + "','" + txt_Pswd.Text + "','" + txt_Name.Text + "','Data Operator','1')";

                        SqlCommand cmd1 = new SqlCommand(qry1);
                        InsertUpdate.InsertUpdateData(cmd1);
                    }
                    else
                    {
                        string qry2 = @"Update LoginMaster set Pswd =  '" + hdpwd.Value + "' where UserId =  '" + txt_UserName.Text + "' ;";

                        SqlCommand cmd2 = new SqlCommand(qry2);
                        InsertUpdate.InsertUpdateData(cmd2);
                    }

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
            cmd = new SqlCommand("update user_details set " + FieldName + " = '" + Value + "' where user_id='" + ID + "'");
            if (InsertUpdate.InsertUpdateData(cmd))
            {

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
                string query = "select *, IIF(Istatus='0', 'Inactive', 'Active') as StatusD, password as Pswd,(Select User_ID from Zoom_API where teacher_id=user_details.user_id) as zoomuserid,(Select Password from Zoom_API where teacher_id=user_details.user_id) as zoompwd from user_details where (user_id='" + txt_userid_mobile_no.Text + "' or mobile='" + txt_userid_mobile_no.Text + "') order by Id Desc";
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
            if (ddl_new_Template.SelectedItem.Text == "Select")
            {
                Alert("Please select template id first");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            else
            { 
                string query = "Select * from Zoom_API where teacher_id='" + hd_teacheruserid.Value + "'  ";
                DataTable dt = code.FillTable(query);
                if (dt.Rows.Count == 0)
                {
                    SqlCommand cmd;
                    string query1 = "INSERT INTO Zoom_API (User_ID,Password,teacher_id) values (@User_ID,@Password,@teacher_id)";
                    cmd = new SqlCommand(query1);
                    cmd.Parameters.AddWithValue("@User_ID", ddl_new_Template.SelectedValue);
                    cmd.Parameters.AddWithValue("@Password", code.get_room_id_live(ddl_new_Template.SelectedValue));
                    cmd.Parameters.AddWithValue("@teacher_id", hd_teacheruserid.Value);
                    if (InsertUpdate.InsertUpdateData(cmd))
                    {

                        DataTable dt1 = code.FillTable("Select * from LiveClassTeacherMapping where TeacherId = '" + hd_teacheruserid.Value + "'  ");
                        if (dt1.Rows.Count == 0)
                        {
                            SqlCommand cmd2;
                            string query2 = "INSERT INTO LiveClassTeacherMapping (TeacherId,TemplateId,RoomID,date_time,createdby) values (@TeacherId,@TemplateId,@RoomID,@date_time,@createdby)";
                            cmd2 = new SqlCommand(query2);
                            cmd2.Parameters.AddWithValue("@TeacherId", hd_teacheruserid.Value);
                            cmd2.Parameters.AddWithValue("@TemplateId", ddl_new_Template.SelectedValue);
                            cmd2.Parameters.AddWithValue("@RoomID", code.get_room_id_live(ddl_new_Template.SelectedValue));
                            cmd2.Parameters.AddWithValue("@date_time", My.getdate1());
                            cmd2.Parameters.AddWithValue("@createdby", ViewState["Admin"].ToString());
                            if (InsertUpdate.InsertUpdateData(cmd2))
                            {
                                Alert(" Live creadination has been insert successfully");

                            }
                        }
                        else
                        {

                            
                           

                            SqlCommand cmd2;
                            string query2 = "Update LiveClassTeacherMapping set TemplateId=@TemplateId,RoomID=@RoomID,date_time=@date_time where TeacherId = @TeacherId";
                            cmd2 = new SqlCommand(query2);
                            cmd2.Parameters.AddWithValue("@TeacherId", hd_teacheruserid.Value);
                            cmd2.Parameters.AddWithValue("@TemplateId", ddl_new_Template.SelectedValue);
                            cmd2.Parameters.AddWithValue("@RoomID", code.get_room_id_live(ddl_new_Template.SelectedValue));
                            cmd2.Parameters.AddWithValue("@date_time", My.getdate1());
                            cmd2.Parameters.AddWithValue("@createdby", ViewState["Admin"].ToString());
                            if (InsertUpdate.InsertUpdateData(cmd2))
                            {
                                Alert("Live creadination has been updated successfully");
                            } 
                        }
                    } 
                }
                else
                {
                     My.exeSql("update Zoom_API set User_ID='" + ddl_new_Template.SelectedValue + "' ,Password='" + code.get_room_id_live(ddl_new_Template.SelectedValue) + "' where teacher_id='" + hd_teacheruserid.Value + "'");

                    //SqlCommand cmd;
                    //string query1 = "INSERT INTO Zoom_API (User_ID,Password,teacher_id) values (@User_ID,@Password,@teacher_id)";
                    //cmd = new SqlCommand(query1);
                    //cmd.Parameters.AddWithValue("@User_ID", ddl_new_Template.SelectedValue);
                    //cmd.Parameters.AddWithValue("@Password", code.get_room_id_live(ddl_new_Template.SelectedValue));
                    //cmd.Parameters.AddWithValue("@teacher_id", hd_teacheruserid.Value);
                    //if (InsertUpdate.InsertUpdateData(cmd))
                    //{

                        DataTable dt1 = code.FillTable("Select * from LiveClassTeacherMapping where TeacherId = '" + hd_teacheruserid.Value + "'  ");
                        if (dt1.Rows.Count == 0)
                        {
                            SqlCommand cmd2;
                            string query2 = "INSERT INTO LiveClassTeacherMapping (TeacherId,TemplateId,RoomID,date_time,createdby) values (@TeacherId,@TemplateId,@RoomID,@date_time,@createdby)";
                            cmd2 = new SqlCommand(query2);
                            cmd2.Parameters.AddWithValue("@TeacherId", hd_teacheruserid.Value);
                            cmd2.Parameters.AddWithValue("@TemplateId", ddl_new_Template.SelectedValue);
                            cmd2.Parameters.AddWithValue("@RoomID", code.get_room_id_live(ddl_new_Template.SelectedValue));
                            cmd2.Parameters.AddWithValue("@date_time", My.getdate1());
                            cmd2.Parameters.AddWithValue("@createdby", ViewState["Admin"].ToString());
                            if (InsertUpdate.InsertUpdateData(cmd2))
                            {
                                Alert(" Live creadination has been insert successfully");

                            }
                        }
                        else
                        {
                            SqlCommand cmd2;
                            string query2 = "Update LiveClassTeacherMapping set TemplateId=@TemplateId,RoomID=@RoomID,date_time=@date_time where TeacherId = @TeacherId";
                            cmd2 = new SqlCommand(query2);
                            cmd2.Parameters.AddWithValue("@TeacherId", hd_teacheruserid.Value);
                            cmd2.Parameters.AddWithValue("@TemplateId", ddl_new_Template.SelectedValue);
                            cmd2.Parameters.AddWithValue("@RoomID", code.get_room_id_live(ddl_new_Template.SelectedValue));
                            cmd2.Parameters.AddWithValue("@date_time", My.getdate1());
                            cmd2.Parameters.AddWithValue("@createdby", ViewState["Admin"].ToString());
                            if (InsertUpdate.InsertUpdateData(cmd2))
                            {
                                Alert("Live creadination has been updated successfully");
                            }

                        }
                    //}
                }
            }
            bind_gridvew();
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
                cmd = new SqlCommand("delete from user_details where user_id='" + hdfID.Value + "'");
                if (InsertUpdate.InsertUpdateData(cmd))
                {
                    //cmd = new SqlCommand("delete from LoginMaster where UserId='" + hdfID.Value + "'");
                    //InsertUpdate.InsertUpdateData(cmd);
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
                lbl_teachername.Text = "";
                lbl_old_temaplatename.Text = "";
                lblmsg.Text = "";
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                HiddenField hdfID = (HiddenField)row.FindControl("hdUserID");
                Label lbl_tempateid = (Label)row.FindControl("lbl_tempateid");
                Label lbl_Zoom_Api_Sl_No = (Label)row.FindControl("lbl_Zoom_Api_Sl_No");
                Label lbl_Template_Name = (Label)row.FindControl("lbl_Template_Name");
                Label lbl_Name = (Label)row.FindControl("lbl_Name");
                hd_teacheruserid.Value = hdfID.Value;
                lbl_teachername.Text = lbl_Name.Text;

                lbluserid.Text = hdfID.Value;

                lbl_old_temaplatename.Text = lbl_Template_Name.Text;
                code.bind_all_ddl_with_id(ddl_new_Template, "Select distinct Template_Name,TemplateID from LiveClassCredential where Is_delete=1 and Status=1 and TemplateID!='" + lbl_tempateid.Text + "' order by Template_Name asc ");
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
                    UsesCode.exeSql("update user_details set  Zoom_Api_Sl_No= '" + getslid + "',Individual_universal='Individual',Allow_Virtual_class_creation='1' where user_id='" + hd_teacheruserid.Value + "'");
                    Alert("Zoom user id and password has been updated");
                    lblmsg.Text = "Zoom user id and password has been updated";
                    txt_emailid1.Text = "";
                    txt_password.Text = "";
                }
                else
                {
                    UsesCode.exeSql("update Zoom_API set  User_ID= '" + txt_emailid1.Text + "', Password='" + txt_password.Text + "',sl_no='" + getslid + "'   where teacher_id='" + hd_teacheruserid.Value + "'");
                    UsesCode.exeSql("update user_details set  Zoom_Api_Sl_No= '" + getslid + "',Individual_universal='Individual',Allow_Virtual_class_creation='1' where user_id='" + hd_teacheruserid.Value + "'");
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
                Label lbl_tempateid = (Label)row.FindControl("lbl_tempateid");
                Label lbl_roomid = (Label)row.FindControl("lbl_roomid");
                Label lbl_Name = (Label)row.FindControl("lbl_Name");

                hd_teacheruserid.Value = hdfID.Value;
                lbl_teachername.Text = lbl_Name.Text;
                // lbl_old_zoomid.Text = lbl_Zoom_Api_Sl_No.Text;
                lbluserid.Text = hdfID.Value;
                hd_teacherid.Value = hdfID.Value;
                lbl_teachername1.Text = lbl_Name.Text;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal1();", true);
            }
            catch { }
        }
    }
}