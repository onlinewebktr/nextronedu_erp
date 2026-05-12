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
    public partial class AddUser : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    code.bind_gridview(GridView1, "select *, IIF(Istatus='0', 'Inactive', 'Active') as StatusD from UserProfile order by Id Desc");
                }
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd;
                if (code.IsExist("Select * from UserProfile where UserID='" + txt_UserName.Text + "'"))
                {
                    if (code.IsExist("Select * from UserProfile where EmailID='" + txt_EmailID.Text + "'"))
                    {
                        cmd = new SqlCommand("insert into UserProfile (UserID, Password, Name,EmailID,Designation,Office,RegDate,RegTime,Idate,Istatus) " +
                           "Values ('" + txt_UserName.Text + "','" + txt_Pswd.Text + "','" + txt_Name.Text + "','" + txt_EmailID.Text + "','" + txt_designation.Text + "'," +
                           "'" + txt_postedin.Text + "','" + code.date() + "','" + code.time() + "','" + code.idate() + "','0')");
                        if (InsertUpdate.InsertUpdateData(cmd))
                        {
                            cmd = new SqlCommand("insert into LoginMaster (UserId, Pswd, UserName,Type,Istatus) " +
                            "Values ('" + txt_UserName.Text + "','" + txt_Pswd.Text + "','" + txt_Name.Text + "','User','0')");
                            if (InsertUpdate.InsertUpdateData(cmd))
                            {
                                Alert("Registered successfully. Approval needed for login.");
                            }
                        }

                    }
                    else { Alert("Duplicate Email ID."); }
                }
                else { Alert("Duplicate UserId."); }

                txt_UserName.Text = ""; txt_EmailID.Text = ""; txt_designation.Text = ""; txt_postedin.Text = "";
                txt_Pswd.Text = ""; txt_Name.Text = "";
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }
        public void Alert(string Message)
        {
            lbl_msg.Text = Message;
            string scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = int.Parse(e.CommandArgument.ToString()); //% GridView1.PageSize;
            string FieldName = e.CommandName;
            if (FieldName == "Istatus")
            {
                HiddenField hdfStatus = (HiddenField)GridView1.Rows[rowIndex].FindControl("hdfActive");
                HiddenField hdUID = (HiddenField)GridView1.Rows[rowIndex].FindControl("hdUserID");
                if (hdfStatus.Value == "1") { hdfStatus.Value = "0"; UpdateDetail(FieldName, hdfStatus.Value, hdUID.Value); }
                else { hdfStatus.Value = "1"; UpdateDetail(FieldName, hdfStatus.Value, hdUID.Value); }
            }
            if (FieldName == "IsDelete")
            {
                HiddenField hdfID = (HiddenField)GridView1.Rows[rowIndex].FindControl("hdId");
                SqlCommand cmd = new SqlCommand("delete from UserProfile where Id='" + hdfID.Value + "'");
                InsertUpdate.InsertUpdateData(cmd);
            }
            code.bind_gridview(GridView1, "select *, IIF(Istatus='0', 'Inactive', 'Active') as StatusD from UserProfile order by Id Desc");
        }

        private void UpdateDetail(string FieldName, string Value, string ID)
        {
            SqlCommand cmd;
            cmd = new SqlCommand("update UserProfile set " + FieldName + " = '" + Value + "' where UserID='" + ID + "'");
            if (InsertUpdate.InsertUpdateData(cmd))
            {
                cmd = new SqlCommand("update LoginMaster set " + FieldName + " = '" + Value + "' where UserId='" + ID + "'");
                InsertUpdate.InsertUpdateData(cmd);
            }

        }

        protected void lnk_View_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnk.Parent.Parent;
            HiddenField lbl_id = (HiddenField)row.FindControl("hdId");
            DataTable dt = code.FillTable("select * from UserProfile where Id='" + lbl_id.Value + "'");
           // ltDescription.Text = dt.Rows[0]["Syllabus"].ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }
    }
}