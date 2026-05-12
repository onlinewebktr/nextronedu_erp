using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class reprint_bill : System.Web.UI.Page
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
                        Session["SlipBkSn"] = "MN33";
                        txt_s_date.Text = mycode.date();
                        txt_e_date.Text = mycode.date();
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();
                        ViewState["sessionid"] = My.get_session_id();
                        mycode.bind_all_ddl_with_id(ddl_session_serach, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                        ddl_session_serach.SelectedValue = My.get_session_id();


                        string pagename_current = Path.GetFileName(Request.Path);
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        if (ViewState["Is_Print"].ToString() == "1")
                        {
                            print1.Visible = true;
                        }
                        else
                        {
                            print1.Visible = false;
                        }

                        find_firm_details();

                        find_bill_by_date();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Admission_Fee_Collection");
            }
        }

        private void find_bill_by_date()
        {
            lbl_class22.Text = "Collected fee list for session :" + ddl_session_serach.SelectedItem.Text + ", from date : " + txt_s_date.Text + " To date : " + txt_e_date.Text;
            string query = "select t2.studentname,t1.Addmission_no,t2.class,t2.Section,t2.rollnumber,t2.session,t1.Date,t1.Slip_no,t1.mode,t2.Session_id,t1.Class_id,t1.Amount from Student_Payment_History t1 join admission_registor t2 on t1.Addmission_no=t2.admissionserialnumber and t1.Class_id=t2.Class_id and t1.Session=t2.Session where t1.Session='" + ddl_session_serach.SelectedItem.Text + "' and t2.Status='1' and Idate>=" + My.DateConvertToIdate(txt_s_date.Text) + " and Idate<=" + My.DateConvertToIdate(txt_e_date.Text) + "  order by t1.id desc";
            Bind_data(query);
        }

        private void find_firm_details()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                imglogo.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_emaiid.Text = dt.Rows[0]["email"].ToString();
                lbl_website.Text = dt.Rows[0]["website"].ToString();
                lbl_contact_details.Text = dt.Rows[0]["contact_no"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();

                if (dt.Rows[0]["Monthly_bill_type"].ToString() == "")
                {
                    ViewState["BillType"] = "1";
                }
                else
                {
                    ViewState["BillType"] = dt.Rows[0]["Monthly_bill_type"].ToString();
                }
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


        private void Bind_data(string query)
        {
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no students list exists", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }



        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Download"].ToString() == "1")
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=Dues_list" + mycode.date() + "_" + mycode.time() + ".xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    using (StringWriter sw = new StringWriter())
                    {
                        HtmlTextWriter hw = new HtmlTextWriter(sw);
                        Panel1.RenderControl(hw);
                        string style = @"<style> TABLE { border: 1px solid black; } TD { border: 1px solid black; } </style> ";
                        Response.Write(style);
                        Response.Output.Write(sw.ToString());
                        Response.Flush();
                        Response.End();
                    }
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            catch
            {
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_session_serach.SelectedItem.Text == "")
                {
                    ddl_session_serach.Focus();
                    Alertme("Please select session.", "warning");
                }
                else
                {
                    find_bill_by_date();
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void btn_find_by_bill_no_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_bill_no.Text == "")
                {
                    Alertme("Please enter bill no.", "warning");
                }
                else
                {
                    lbl_class22.Text = "";
                    string query = "select t2.studentname,t1.Addmission_no,t2.class,t2.Section,t2.rollnumber,t2.session,t1.Date,t1.Slip_no,t1.mode,t2.Session_id,t1.Class_id,t1.Amount from Student_Payment_History t1 join admission_registor t2 on t1.Addmission_no=t2.admissionserialnumber and t1.Class_id=t2.Class_id and t1.Session=t2.Session where t1.Slip_no='" + txt_bill_no.Text + "' order by t1.id desc";
                    Bind_data(query);
                }
            }
            catch (Exception ex)
            {
            }
        }


        [WebMethod]
        public static List<string> GetRooPath(string PathRooT)
        {
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(My.conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct Slip_no from Student_Payment_History where Slip_no LIKE '%'+@SearchMobNo+'%'";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchMobNo", PathRooT);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        MobResult.Add(dr["Slip_no"].ToString());
                    }
                    con.Close();
                    return MobResult;
                }
            }
        }

        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    RepeaterItem item = e.Item;
                    Label Label4 = (Label)e.Item.FindControl("Label4");
                    Label Label3 = (Label)e.Item.FindControl("Label3");
                    Label Label5 = (Label)e.Item.FindControl("Label5");

                    Label4.Visible = true;
                    Label3.Visible = false;
                    Label5.Visible = false;
                    try
                    {
                        if (ViewState["BillType"].ToString() == "A5S")
                        {
                            Label4.Visible = false;
                            Label3.Visible = true;
                            Label5.Visible = false;
                        }
                        if (ViewState["BillType"].ToString() == "A5I")
                        {
                            Label4.Visible = false;
                            Label3.Visible = false;
                            Label5.Visible = true;
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            catch (Exception ex) { }
        }
    }
}