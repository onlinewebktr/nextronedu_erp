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

namespace school_web.Library_Admin
{
    public partial class Generate_Library_Card_Forteacher : System.Web.UI.Page
    {
        Library lb = new Library();
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
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = My.get_firm_id();
                        ViewState["Branch_id"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        ViewState["flag"] = "0";

                        bind_all_data();

                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Generate_Library_Card_For_teacher");
            }

        }
        private void bind_all_data()
        {
            bind_grd_view("select * from user_details where   Branch_id = '" + ViewState["Branch_id"].ToString() + "' and User_Type = 'Teacher' and Istatus='1' order by id desc");
        }
        #region CountDataA

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
        #endregion

        private void bind_grd_view(string qry)
        {
            ViewState["query"] = qry;
            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {
                btn_excels.Visible = false;
                finalsubmitpnl.Visible = false;
                Alertme("Sorry there are no data list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                btn_excels.Visible = true;
                finalsubmitpnl.Visible = true;
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }
        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                Label lbl_admissionserialnumber = (Label)row.FindControl("lbl_admissionserialnumber");
                hd_id.Value = lbl_Id.Text;



            }
            catch
            {

            }
        }

       
        #region ExcelDownloaD
        protected void btn_excels_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=Export.xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    using (StringWriter sw = new StringWriter())
                    {
                        HtmlTextWriter hw = new HtmlTextWriter(sw);
                        Panel2.RenderControl(hw);
                        Response.Output.Write(sw.ToString());
                        Response.Flush();
                        Response.End();
                    }
                }
                catch
                {
                }



            }
            catch (Exception ex)
            {
            }
        }

     
        #endregion
        protected void Btn_Generate_Click(object sender, EventArgs e)
        {
            try
            {
                string confirmValue = string.Empty;
                confirmValue = Request.Form["confirm_value"];
                if (confirmValue == "Yes")
                {
                    bool update = false;
                    string Prefix = lb.get_prefix(ViewState["Branch_id"].ToString(), "Teacher");
                    string Postfix = lb.get_postfix(ViewState["Branch_id"].ToString(), "Teacher");
                    string serialNo = lb.get_serialNo(ViewState["Branch_id"].ToString(), "Teacher");
                    DataTable dt = mycode.FillData(ViewState["query"].ToString());
                    if (dt.Rows.Count == 0)
                    {
                    }
                    else
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            if ((dr["Library_Card_No"].ToString()) == "")
                            {
                                update = true;
                                string lib_card_no = Library.lib_card_format_teacher_staff(Prefix, Postfix, serialNo, "lib_card_no", ViewState["Branch_id"].ToString()).ToString();
                                Library.exeSql("update user_details set Library_Card_No='" + lib_card_no + "' where User_Type = 'Teacher' and Branch_id='" + ViewState["Branch_id"].ToString() + "' and user_id='"+ dr["user_id"].ToString() + "' ");

                                ////send_push_notification(lib_card_no, dr["Admission_No"].ToString(), dr["Name"].ToString(), dr["Session"].ToString(), dr["Course"].ToString(), dr["section"].ToString());
                            }
                        }
                        if (update == true)
                        {

                            bind_grd_view(ViewState["query"].ToString());


                        }

                    }
                }

            }
            catch
            {

            }

        }

        #region WebMethoD
        [WebMethod]
        public static List<string> Getusername(string username)
        {
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(My.conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct name from user_details where name LIKE '%'+@name+'%' and Istatus=1 ";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@name", username);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        MobResult.Add(dr["name"].ToString());
                    }
                    con.Close();
                    return MobResult;
                }
            }
        }
        #endregion


        #region WebMethoD mobile no
        [WebMethod]
        public static List<string> GetMobile(string Mobile)
        {
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(My.conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct mobile from user_details where mobile LIKE '%'+@mobile+'%' and Istatus=1 ";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@mobile", Mobile);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        MobResult.Add(dr["mobile"].ToString());
                    }
                    con.Close();
                    return MobResult;
                }
            }
        }
        #endregion
        #region WebMethoD user id no
        [WebMethod]
        public static List<string> get_user_id(string user_id)
        {
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(My.conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct user_id from user_details where user_id LIKE '%'+@user_id+'%' and Istatus=1  ";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@user_id", user_id);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        MobResult.Add(dr["user_id"].ToString());
                    }
                    con.Close();
                    return MobResult;
                }
            }
        }
        #endregion
        protected void Txt_Name_TextChanged(object sender, EventArgs e)
        {
            if (Txt_Name.Text == "")
            {
                Alertme("Please enter name", "warning");

                finalsubmitpnl.Visible = false;
                
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                bind_grd_view("select * from user_details where  name ='" + Txt_Name.Text + "'  and User_Type = 'Teacher' and Branch_id = '" + ViewState["Branch_id"].ToString() + "' and  Istatus='1'  order by id desc");
            }
        }

        protected void Txt_Mobile_TextChanged(object sender, EventArgs e)
        {
            if (Txt_Mobile.Text == "")
            {
                Alertme("Please enter mobile no.", "warning");
                finalsubmitpnl.Visible = false;

                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                bind_grd_view("select * from user_details where  mobile ='" + Txt_Mobile.Text + "'  and User_Type = 'Teacher' and Branch_id = '" + ViewState["Branch_id"].ToString() + "' and  Istatus='1'  order by id desc");
            }


           
        }

        protected void txt_employid_TextChanged(object sender, EventArgs e)
        {
            if(txt_employid.Text=="")
            {
                Alertme("Please enter employee id", "warning");
                finalsubmitpnl.Visible = false;

                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                bind_grd_view("select * from user_details where  user_id ='" + txt_employid.Text + "'  and User_Type = 'Teacher' and Branch_id = '" + ViewState["Branch_id"].ToString() + "' and  Istatus='1'  order by id desc");

            }
          
        }

        protected void btn_find_All_Click(object sender, EventArgs e)
        {
            try
            {
                bind_grd_view("select * from user_details where    User_Type = 'Teacher' and Branch_id = '" + ViewState["Branch_id"].ToString() + "' and  Istatus='1'  order by id desc");
            }
            catch
            {

            }
        }
    }
}