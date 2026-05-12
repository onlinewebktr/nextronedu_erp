using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Scholarship
{
    public partial class Scholarship_Apply : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lbl_session.Text = My.get_reg_apply_session();
                ViewState["sessionid"] = My.get_session_id_onlinereg();
                fetch_company_name();
                fetch_data_Scholarship();
            }
        }

        private void fetch_data_Scholarship()
        {
            DataTable dt = mycode.FillData("  Select *  from Scholarship_Program     where    Is_active=1   and   Session_id='" + ViewState["sessionid"].ToString() + "' ");
            if (dt.Rows.Count == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Sorry there are no scholarship program  exist')", true);
                grid_Scholarship.DataSource = null;
                grid_Scholarship.DataBind();
            }
            else
            {
                grid_Scholarship.DataSource = dt;
                grid_Scholarship.DataBind();
            }
        }

        protected void grid_Scholarship_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_Test_id = (Label)e.Row.FindControl("lbl_Test_id");
                GridView GrdView = (GridView)e.Row.FindControl("GrdView");
                Bind_list_Scholarship(lbl_Test_id.Text, GrdView);
            }

        }

        private void Bind_list_Scholarship(string testid, GridView grdView)
        {

            DataTable dt = mycode.FillData(" Select orf.*,act.Course_Name,(Select top  1 Session from session_details where session_id=orf.Session_id) as Session,CASE WHEN Isactive = '1' THEN 'Active' WHEN Isactive = '0' THEN 'Inactive'  WHEN Isactive = '' THEN 'Inactive' END AS activestatus   from Scholarship_Parameter_fees orf   join Add_course_table  act on act.course_id=orf.Class_id where orf.Session_id='" + ViewState["sessionid"].ToString() + "' and  orf.Test_id in ( Select Test_id from Scholarship_Program where Session_id='" + ViewState["sessionid"].ToString() + "' and Test_id='" + testid + "' and Is_active=1)  and orf.Isactive=1 and orf.Test_id='" + testid + "'  order by act.Position asc");
            if (dt.Rows.Count == 0)
            {
                 
                grdView.DataSource = null;
                grdView.DataBind();
            }
            else
            {
                grdView.DataSource = dt;
                grdView.DataBind();
            }
        }

        My mycode = new My();
        private void fetch_company_name()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count == 0)
            { }
            else
            { 
                school_logo.Src = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_address2.Visible = false;
                try
                {
                    if (dt.Rows[0]["Is_2nd_address"].ToString() == "True")
                    {
                        lbl_address2.Visible = true;
                        lbl_address2.Text = dt.Rows[0]["address2"].ToString();
                    }
                    else
                    {
                        lbl_address2.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                }
                
                lbl_emaiid.Text = dt.Rows[0]["email"].ToString();
                lbl_website.Text = dt.Rows[0]["website"].ToString();
                lbl_contact_details.Text = dt.Rows[0]["contact_no"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();
            }
        }

        protected void GrdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_Isactive = (Label)e.Row.FindControl("lbl_Isactive");
                Label lbl_start_Idate = (Label)e.Row.FindControl("lbl_start_Idate");
                Label lbl_end_Idate = (Label)e.Row.FindControl("lbl_end_Idate");
                Label lbl_no_seat_avl = (Label)e.Row.FindControl("lbl_no_seat_avl");

                Label no_seat = (Label)e.Row.FindControl("lbl_no_application");
                Label lbl_Class_id = (Label)e.Row.FindControl("lbl_Class_id");
                Label lbl_Test_id = (Label)e.Row.FindControl("lbl_Test_id");


                Button btn_Submit = (Button)e.Row.FindControl("btn_Submit");

                if (lbl_Isactive.Text == "0")
                {
                    btn_Submit.Enabled = false;
                    btn_Submit.BackColor = Color.Gray;
                    e.Row.BackColor = Color.Red;

                }
                else
                {
                    int fill_no_seat = My.get_no_seat_current_session_Scholarship(ViewState["sessionid"].ToString(), lbl_Class_id.Text, lbl_Test_id.Text);

                    int avl = Convert.ToInt32(no_seat.Text) - fill_no_seat;
                    lbl_no_seat_avl.Text = avl.ToString();

                    if (Convert.ToInt32(no_seat.Text) > fill_no_seat)
                    {
                        DateTime dtm = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                        string Datetimesystem = dtm.ToString("dd/MM/yyyy hh:mm:ss tt");
                        if (Convert.ToInt32(lbl_end_Idate.Text) >= Convert.ToInt32(mycode.idate()))
                        {
                            if (Convert.ToInt32(mycode.idate()) >= Convert.ToInt32(lbl_start_Idate.Text))
                            {

                                btn_Submit.Enabled = true;
                                btn_Submit.BackColor = Color.Green;
                                e.Row.BackColor = Color.LightGreen;

                            }
                            else// upcomming
                            {
                                btn_Submit.Enabled = false;
                                btn_Submit.BackColor = Color.Gray;
                                e.Row.BackColor = Color.Orange;

                            }

                        }
                        else
                        {
                            btn_Submit.Enabled = false;
                            btn_Submit.BackColor = Color.Gray;
                            e.Row.BackColor = Color.Red;

                        }




                    }
                    else
                    {
                        btn_Submit.Enabled = false;
                        btn_Submit.BackColor = Color.Gray;
                        e.Row.BackColor = Color.Red;

                    }


                }
            }
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            Button lnk = (Button)sender;
            GridViewRow row = (GridViewRow)lnk.Parent.Parent;
            Label lbl_Class_id = (Label)row.FindControl("lbl_Class_id");
            Label lbl_Session_id = (Label)row.FindControl("lbl_Session_id");
            Label lbl_Test_id = (Label)row.FindControl("lbl_Test_id");

            string url = "Scholarship-guidelines.aspx?sdjhfdsgfhjasbdagdagdshdghsgdsgdg=" + lbl_Class_id.Text + "&sfhsdfghjdncjszhfyshfcjzshdyusahds=" + lbl_Session_id.Text + "&testid=" + lbl_Test_id.Text;

            Response.Redirect(url, false);



        }
    }
}