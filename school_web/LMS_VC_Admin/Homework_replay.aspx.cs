using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using school_web.AppCode;
namespace school_web.LMS_VC_Admin
{
    public partial class Homework_replay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["homework_id"] != null)
                {
                    hd_homeworkid.Value = Request.QueryString["homework_id"];
                    get_class_and_section();
                    fetchdata();
                    Bind_count();
                }
                else
                {
                    Response.Redirect("Dashboard.aspx");
                }
            }
        }

        private void Bind_count()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@cmdstatus", "5");
            cmd.Parameters.AddWithValue("@Homework_id", hd_homeworkid.Value);
            cmd.Parameters.AddWithValue("@Class_id", ViewState["Class"].ToString());
            cmd.Parameters.AddWithValue("@Section", ViewState["Section"].ToString());
            cmd.CommandText = "sp_ReplayHomework";
            DataTable dt = UsesCode.Getdata_sp(cmd);
            if (Convert.ToString(dt.Rows.Count) == "0")
            {


            }
            else
            {
                lbl_total_data.Text = dt.Rows[0]["total"].ToString();
                lbl_replied.Text = dt.Rows[0]["replied"].ToString();
                lbl_notreplyed.Text = dt.Rows[0]["notreplied"].ToString();
                lbl_Checked.Text = dt.Rows[0]["Checked"].ToString();
            }

        }
        UsesCode mycode = new UsesCode();
        private void get_class_and_section()
        {
            string sql = @"select Class,Section,Subject  from Homework_Details  where Home_Work_id='" + hd_homeworkid.Value + "'";
            DataTable dt = mycode.FillTable(sql);
            if (dt.Rows.Count.ToString() == "0")
            {
                ViewState["Class"] = "0";
                ViewState["Section"] = "0";
                ViewState["Subject"] = "0";
            }
            else
            {
                ViewState["Class"] = dt.Rows[0]["Class"].ToString();
                ViewState["Section"] = dt.Rows[0]["Section"].ToString();
                ViewState["Subject"] = dt.Rows[0]["Subject"].ToString();
            }
        }

        private void fetchdata()// all
        {
            SqlCommand cmd = new SqlCommand();
            if (rd_all.Checked == true)
            {
                cmd.Parameters.AddWithValue("@cmdstatus", "1");
            }
            else if (rd_Replied.Checked == true)
            {
                cmd.Parameters.AddWithValue("@cmdstatus", "2");

            }
            else if (rd_no_replyed.Checked == true)
            {
                cmd.Parameters.AddWithValue("@cmdstatus", "3");

            }
            else if (rd_Checked.Checked == true)
            {
                cmd.Parameters.AddWithValue("@cmdstatus", "4");
            }
            cmd.Parameters.AddWithValue("@Homework_id", hd_homeworkid.Value);
            cmd.Parameters.AddWithValue("@Class_id", ViewState["Class"].ToString());
            cmd.Parameters.AddWithValue("@Section", ViewState["Section"].ToString());
            cmd.CommandText = "sp_ReplayHomework";
            DataTable dt = UsesCode.Getdata_sp(cmd);
            if (Convert.ToString(dt.Rows.Count) == "0")
            {
                RPDetails.DataSource = null;
                RPDetails.DataBind();

            }
            else
            {
                RPDetails.DataSource = dt;
                RPDetails.DataBind();
            }
        }

        protected void lnk_view_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_admissionserialnumber = (Label)row.FindControl("lbl_admissionserialnumber");
                Response.Redirect("Home_work_Remarks.aspx?homeworkid=" + hd_homeworkid.Value + "&studentid=" + lbl_admissionserialnumber.Text, false);
            }
            catch
            {
            }
        }

        protected void RPDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (((Label)e.Item.FindControl("lbl_replystatus")).Text == "Replied")
                {
                    ((Label)e.Item.FindControl("lbl_replystatus")).Text = "Replied";
                    ((Label)e.Item.FindControl("lbl_replystatus")).CssClass = "badge badge-success ml-2";
                }
                else if (((Label)e.Item.FindControl("lbl_replystatus")).Text == "Checked")
                {
                    ((Label)e.Item.FindControl("lbl_replystatus")).Text = "Checked";
                    ((Label)e.Item.FindControl("lbl_replystatus")).CssClass = "badge badge-success ml-2";
                }
                else if (((Label)e.Item.FindControl("lbl_replystatus")).Text == "")
                {
                    ((Label)e.Item.FindControl("lbl_replystatus")).Text = "No";
                    ((Label)e.Item.FindControl("lbl_replystatus")).CssClass = "badge badge-danger ml-2";
                }
            }
        }

        protected void rd_Replied_CheckedChanged(object sender, EventArgs e)
        {
            fetchdata();
        }

        protected void rd_Checked_CheckedChanged(object sender, EventArgs e)
        {
            fetchdata();
        }

        protected void rd_no_replyed_CheckedChanged(object sender, EventArgs e)
        {
            fetchdata();
        }

        protected void rd_all_CheckedChanged(object sender, EventArgs e)
        {
            fetchdata();
        }

        protected void lnk_view_Click1(object sender, EventArgs e)
        {

        }
    }
}