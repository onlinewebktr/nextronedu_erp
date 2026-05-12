using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.LMS_VC_Admin
{
    public partial class UserReport : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                code.bind_gridview(GridView1, "select *, IIF(Istatus='0', 'Inactive', 'Active') as StatusD from UserProfile order by Id Desc");
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridView1.BottomPagerRow;
           // GridView1.PageIndex = DDLPage.SelectedIndex;
            code.bind_gridview(GridView1, "select *, IIF(Istatus='0', 'Inactive', 'Active') as StatusD from UserProfile order by Id Desc");
            GridView1.DataBind();
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
    }
}