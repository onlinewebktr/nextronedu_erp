using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Library_Admin.images
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                lbl_con.Text = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

                SqlDataAdapter ad = new SqlDataAdapter("select TABLE_NAME FROM INFORMATION_SCHEMA.Tables order by TABLE_NAME", con);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                grd_tables.DataSource = ds;
                grd_tables.DataBind();
            }


        }

        public String con = My.conn;
        public void execSql(string query)
        {
            lbl_message.Text = "";
            try
            {

                SqlDataAdapter ad = new SqlDataAdapter(query, con);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                if (ds.Tables.Count > 0)
                {
                    grd_data.DataSource = ds;
                    grd_data.DataBind();
                }
                else
                {
                    grd_data.DataSource = null;
                    grd_data.DataBind();

                }
                lbl_message.Text = "Executed Successfully";
            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.ToString();
            }

        }

        protected void btn_show_data_Click(object sender, EventArgs e)
        {

            LinkButton ddl = (LinkButton)sender;
            GridViewRow row = (GridViewRow)ddl.Parent.Parent;
            int idx = row.RowIndex;
            Label lbl_table = (Label)row.FindControl("lbl_data");
            string ssss = lbl_table.Text;
            txt_query.Text = "select * from " + lbl_table.Text;
            execSql("select * from " + lbl_table.Text);

            find_table_structure(ssss);
        }

        private void find_table_structure(string ssss)
        {
            string query = "SELECT COLUMN_NAME, IS_NULLABLE, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH FROM INFORMATION_SCHEMA.COLUMNS  where TABLE_NAME='" + ssss + "'";
            SqlDataAdapter ad = new SqlDataAdapter(query, con);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            if (ds.Tables.Count > 0)
            {
                string ddd = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (ddd == "")
                    {
                        ddd = dt.Rows[i][0].ToString();
                    }
                    else
                    {
                        ddd = ddd + ", " + dt.Rows[i][0].ToString();
                    }

                }
                lbl_table.Text = "Table Structure :- " + (ssss) + "- " + ddd;
            }
        }
        protected void btn_table_structure_Click(object sender, EventArgs e)
        {
            LinkButton ddl = (LinkButton)sender;
            GridViewRow row = (GridViewRow)ddl.Parent.Parent;
            int idx = row.RowIndex;
            Label lbl_table = (Label)row.FindControl("lbl_data");
            string ssss = lbl_table.Text;
            execSql("SELECT    COLUMN_NAME ,  IS_NULLABLE, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH FROM INFORMATION_SCHEMA.COLUMNS    where TABLE_NAME='" + lbl_table.Text + "'");
        }
        protected void btn_execute_Click(object sender, EventArgs e)
        {
            execSql(txt_query.Text);
        }


        protected void grd_data_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            execSql(txt_query.Text);
            grd_data.PageIndex = e.NewPageIndex;
            grd_data.DataBind();
        }
        #region download_in_excel
        protected void btn_excel_Click(object sender, EventArgs e)
        {


            try
            {
                DateTime dtm = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                string date = dtm.ToString("dd/MM/yyyy");
                Session["today"] = date;
                string excelname = Session["today"] + "query.xls";
                export_to_excel(grd_data, excelname);
            }
            catch (Exception ex)
            {
            }
        }

        private void export_to_excel(GridView grd_view, string excelname)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", excelname));
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grd_view.AllowPaging = false;
            grd_view.AllowSorting = false;
            execSql(txt_query.Text);
            grd_view.HeaderRow.Style.Add("background-color", "#FFFFFF");

            grd_view.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        #endregion download_in_excel

        protected void btn_selerate_script_Click(object sender, EventArgs e)
        {
            try
            {
                insert_data_table(txt_query.Text);
            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.ToString();
            }

        }
        string identitycolumn;
        int i = 0;
        void insert_data_table(string table_name)
        {
            StringBuilder values = new StringBuilder();
            SqlDataAdapter ad = new SqlDataAdapter("SELECT * from " + table_name, My.con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "table");
            DataTable dt = ds.Tables[0];
            int cnt = 0;
            if (dt.Rows.Count > 0)
            {
                if (identitycolumn != "")
                {
                    values.Append("SET IDENTITY_INSERT dbo." + table_name + " ON\n");
                }
                string columns_name = "";
                for (i = 0; i < dt.Columns.Count; i++)
                {
                    columns_name += "[" + dt.Columns[i].ColumnName + "], ";
                }
                columns_name = columns_name.Remove(columns_name.Length - 2);

                foreach (DataRow dr in dt.Rows)
                {
                    cnt++;
                    string query = "INSERT INTO dbo." + table_name + " ( " + columns_name + " ) VALUES ( ";
                    for (i = 0; i < dr.ItemArray.Length; i++)
                    {
                        query += "N'" + dr[i].ToString().Replace("'", "''") + "', ";
                    }
                    query = query.Remove(query.Length - 2);
                    query += " )\n";
                    values.Append(query);
                    if (cnt == 500)
                    {
                        cnt = 0;
                        if (identitycolumn != "")
                        {
                            values.Append("SET IDENTITY_INSERT dbo." + table_name + " OFF\n");
                            values.Append("/******line break******/");
                            values.Append("SET IDENTITY_INSERT dbo." + table_name + " ON\n");
                        }
                        else
                        {
                            values.Append("/******line break******/");
                        }

                    }
                }
                if (identitycolumn != "")
                {
                    values.Append("SET IDENTITY_INSERT dbo." + table_name + " OFF\n");
                }
            }
            txt_code.Text = values.ToString(); txt_code.Visible = true;
        }

        protected void lnk_generatescript_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                GridViewRow row = (GridViewRow)lnk.Parent.Parent;
                int idx = row.RowIndex;
                Label lbl_table = (Label)row.FindControl("lbl_data");

                insert_data_table(lbl_table.Text);
            }
            catch (Exception ex)
            {
                lbl_message.Text = ex.ToString();
            }
        }



    }
}