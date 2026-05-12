using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class fee_collectionJs : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
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
                        hd_is_quarterwise_payment.Value = "0";
                        ViewState["Userid"] = Session["Admin"].ToString();
                        hd_user_Type.Value = My.get_user_type(ViewState["Userid"].ToString());
                        find_firm_details();



                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        hd_branch_id.Value = mycode.get_branch_id(ViewState["Userid"].ToString());
                        hd_user_id.Value = ViewState["Userid"].ToString();
                        mycode.bind_all_ddl_with_id(ddl_session, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                        ddl_session.SelectedValue = My.get_session_id();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "MonthlyFeePayment");
            }

        }


        private void find_firm_details()
        {
            DataTable dt = PayrollMy.dataTable("select * from Firm_Details");
            if (dt.Rows.Count > 0)
            {
                //imglogo.ImageUrl = dt.Rows[0]["logo"].ToString();
                //lbl_address.Text = dt.Rows[0]["address1"].ToString();
                //lbl_emaiid.Text = dt.Rows[0]["email"].ToString();
                //lbl_website.Text = dt.Rows[0]["website"].ToString();
                //lbl_contact_details.Text = dt.Rows[0]["contact_no"].ToString();
                //lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();
                try
                {
                    if (dt.Rows[0]["Is_quarterwise_payment"].ToString() == "True")
                    {
                        hd_is_quarterwise_payment.Value = "1";
                    }
                }
                catch (Exception ex)
                {
                }

                //try
                //{
                //    if (dt.Rows[0]["Is_auto_assign_sec_roll_no"].ToString() == "True")
                //    {
                //        ViewState["Is_auto_assign_sec_roll_no"] = "1";
                //        ViewState["No_of_student_in_a_section"] = dt.Rows[0]["No_of_student_in_a_section"].ToString();
                //    }
                //}
                //catch (Exception ex)
                //{
                //}

                //try
                //{
                //    ViewState["Is_check_payment_verify"] = "0";
                //    if (dt.Rows[0]["Is_check_payment_verify"].ToString() == "True")
                //    {
                //        ViewState["Is_check_payment_verify"] = "1";
                //    }
                //}
                //catch (Exception ex)
                //{
                //}
                //try
                //{
                //    ViewState["Monthly_bill_type"] = dt.Rows[0]["Monthly_bill_type"].ToString();
                //}
                //catch (Exception ex)
                //{
                //}

                //ViewState["Is_month_selection_free"] = "1";
                //try
                //{
                //    if (dt.Rows[0]["Is_month_selection_freez"].ToString() == "True")
                //    {
                //        ViewState["Is_month_selection_free"] = "0";
                //    }
                //}
                //catch (Exception ex)
                //{
                //}



                //try
                //{
                //    if (hd_user_Type.Value == "Admin")
                //    {
                //        txt_payment_date.Enabled = true;
                //        txt_payment_date.CssClass = "form-control find-dv-txtbx";
                //        paydateDVS.Attributes.Add("class", "col-md-3 padd-lft0");

                //        //====
                //        txt_date.Enabled = true;
                //        txt_date.CssClass = "calender-icon form-control find-dv-txtbx";
                //        admpayDateDV.Attributes.Add("class", "col-md-3");
                //    }
                //    else
                //    {
                //        if (dt.Rows[0]["Is_user_change_pay_date"].ToString() == "True")
                //        {
                //            txt_payment_date.Enabled = false;
                //            txt_payment_date.CssClass = "form-control find-dv-txtbx noclick";
                //            paydateDVS.Attributes.Add("class", "col-md-3 padd-lft0 noclick");

                //            //====
                //            txt_date.Enabled = false;
                //            txt_date.CssClass = "calender-icon form-control find-dv-txtbx noclick";
                //            admpayDateDV.Attributes.Add("class", "col-md-3 padd-lft0 noclick");
                //        }
                //        else
                //        {
                //            txt_payment_date.Enabled = true;
                //            txt_payment_date.CssClass = "form-control find-dv-txtbx";
                //            paydateDVS.Attributes.Add("class", "col-md-3 padd-lft0");

                //            //====
                //            txt_date.Enabled = true;
                //            txt_date.CssClass = "calender-icon form-control find-dv-txtbx";
                //            admpayDateDV.Attributes.Add("class", "col-md-3");
                //        }
                //    }
                //}
                //catch (Exception ex)
                //{
                //}
            }
        }


        [WebMethod]
        public static List<string> GetRooPathAdmNo(string PathRooT, string Session_id)
        {
            string sessionid = Session_id;
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(My.conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct admissionserialnumber from admission_registor where admissionserialnumber LIKE '%'+@SearchMobNo+'%'   and Status='1' and Session_id='" + Session_id + "'";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchMobNo", PathRooT);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        MobResult.Add(dr["admissionserialnumber"].ToString());
                    }
                    con.Close();
                    return MobResult;
                }
            }
        }
    }
}