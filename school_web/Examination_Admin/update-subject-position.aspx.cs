using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Examination_Admin
{
    public partial class update_subject_position : System.Web.UI.Page
    {
        My mycode = new My();
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
                        ViewState["Userid"] = Session["Admin"].ToString();
                      
                        ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        mycode.bind_all_ddl_with_id(ddl_course_search, "Select Course_Name,course_id from Add_course_table order by Position asc");
                        ddl_course_search.SelectedValue = My.get_top_one_class();
                        find_subjects();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "update_subject_position");
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


        public void finalgride(string query)
        {
            ViewState["query"] = query;
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no subject list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }





        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                if (((Label)e.Item.FindControl("lbl_Is_mandatory")).Text == "True")
                {

                    ((Label)e.Item.FindControl("lbl_midetry")).Text = "Mandatory";
                    ((Label)e.Item.FindControl("lbl_midetry")).CssClass = "badge badge-success ml-2";

                }

                else
                {
                    ((Label)e.Item.FindControl("lbl_midetry")).Text = "N/A";
                    ((Label)e.Item.FindControl("lbl_midetry")).CssClass = "badge badge-danger ml-2";

                }
                if (((Label)e.Item.FindControl("lbl_Subject_type")).Text == "Scholastic")
                {

                    ((Label)e.Item.FindControl("lbl_Subject_type")).Text = "Scholastic";
                    ((Label)e.Item.FindControl("lbl_Subject_type")).CssClass = "badge badge-Scholastic ml-2";

                }

                else
                {
                    ((Label)e.Item.FindControl("lbl_Subject_type")).Text = "Co-Scholastic";
                    ((Label)e.Item.FindControl("lbl_Subject_type")).CssClass = "badge badge-coScholastic ml-2";

                }
            }
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_course_search.SelectedItem.Text == "Select")
                {
                    Alertme("Please select class,", "warning");
                    return;
                }
                find_subjects();
                
            }
            catch (Exception ex)
            {
            }
        }

        private void find_subjects()
        {
            string query = "Select sm.*,cm.Course_Name from Subject_Master sm join Add_course_table cm on cm.course_id=sm.course_id where cm.course_id=" + ddl_course_search.SelectedValue + " and sm.Branch_id='" + ViewState["branchid"].ToString() + "' order BY Subject_position asc";
            finalgride(query);
        }


        //==========================================
        protected void btn_update_sl_Click(object sender, EventArgs e)
        {
            try
            {
                update_subject_positions();
                find_subjects();
                Alertme("Position updated successfully.", "success");
            }
            catch (Exception ex)
            {
                My.submitException(ex, "HomeSlideAdded");
            }
        }

        private void update_subject_positions()
        { 
            int growcount = rd_view.Items.Count;
            int k = 0;
            for (int i = 0; i < growcount; i++)
            {
                Label lbl_Id = (Label)rd_view.Items[i].FindControl("lbl_id");
                TextBox txtsl_position = (TextBox)rd_view.Items[i].FindControl("txt_position_number");
                SqlDataAdapter ad = new SqlDataAdapter("select * from Subject_Master where Id='" + lbl_Id.Text + "'", My.conn);
                DataSet ds = new DataSet();
                ad.Fill(ds, "Subject_Master");
                DataTable dt = ds.Tables[0];
                int rowcount = ds.Tables[0].Rows.Count;
                if (rowcount == 0)
                {
                }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (txtsl_position.Text != "")
                        {
                            dr["Subject_position"] = txtsl_position.Text;
                            SqlCommandBuilder cmb = new SqlCommandBuilder(ad);
                            ad.Update(dt);
                        }
                    }
                }
                k++;
            }
        } 
    }
}