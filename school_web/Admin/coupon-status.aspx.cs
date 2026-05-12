using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class coupon_status : System.Web.UI.Page
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
                        find_firm_details();
                        find_all_data();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Admission_Fee_Collection");
            }
        }

        private void find_all_data()
        {
            string query = "select t2.session,t2.class,t2.Section,t2.rollnumber,t2.studentname,t2.fathername,t2.mothername,t2.Father_whatsApp_no,t1.* from Coupon_applied_list t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id and t1.Admission_no=t2.admissionserialnumber where t1.Session_id='" + ddl_session_serach.SelectedValue + "' order by t1.Id desc";
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




                Dictionary<string, object> autosms = mycode.get_auto_message_template("Pay Fee");
                ViewState["SMS_Tempate"] = (String)autosms["SMS_Tempate"];
                ViewState["VariableName"] = (String)autosms["VariableName"];
                ViewState["SMSType"] = (String)autosms["SMSType"];
                ViewState["Send_From"] = (String)autosms["Send_From"];
                ViewState["Is_Send_SMS"] = (String)autosms["Is_Send_SMS"];
                ViewState["Is_Send_WhatsApp"] = (String)autosms["Is_Send_WhatsApp"];
                var vrls = ViewState["VariableName"].ToString().Split(',');
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
                Alertme("Sorry there are no data list exists", "warning");
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
                    Response.AddHeader("content-disposition", "attachment;filename=Coupon_list" + mycode.date() + "_" + mycode.time() + ".xls");
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
                if (ddl_session_serach.SelectedItem.Text == "Select")
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

        private void find_bill_by_date()
        {
            string query = "select t2.session,t2.class,t2.Section,t2.rollnumber,t2.studentname,t2.fathername,t2.mothername,t2.Father_whatsApp_no,t1.* from Coupon_applied_list t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id and t1.Admission_no=t2.admissionserialnumber where t1.Session_id='" + ddl_session_serach.SelectedValue + "' and t1.Created_idate>='" + My.DateConvertToIdate(txt_s_date.Text) + "' and t1.Created_idate<='" + My.DateConvertToIdate(txt_s_date.Text) + "' order by t1.Id desc";
            Bind_data(query);
        }

        protected void lnk_status_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label Id = (Label)row.FindControl("lbl_id");
                Label lbl_coupon_id = (Label)row.FindControl("lbl_coupon_id");
                Label lbl_std_name = (Label)row.FindControl("lbl_std_name");
                Label lbl_mobile_no = (Label)row.FindControl("lbl_mobile_no");
                Label lbl_disc_perc = (Label)row.FindControl("lbl_disc_perc");
                Label lbl_disc_amt = (Label)row.FindControl("lbl_disc_amt");

                ViewState["rowID"] = Id.Text;
                ViewState["couponCodE"] = lbl_coupon_id.Text;

                ViewState["std_name"] = lbl_std_name.Text;
                ViewState["mobile_no"] = lbl_mobile_no.Text;
                ViewState["disc_perc"] = lbl_disc_perc.Text;
                ViewState["disc_amt"] = lbl_disc_amt.Text;


                DataTable dt = My.dataTable("select *,(select top 1 content from Content_master where content_id=Discount_Master_temp.fee_head_id) as Fee_head_name from Discount_Master_temp where Student_discount_type_entry_id='" + lbl_coupon_id.Text + "'");
                if (dt.Rows.Count > 0)
                {
                    rd_coupon_head.DataSource = dt;
                    rd_coupon_head.DataBind();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalC();", true);
                }
            }
            catch (Exception ex)
            {
            }
        }


        protected void btn_update_status_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_status.SelectedItem.Text == "Select")
                {
                    ddl_status.Focus();
                    Alertme("Please select status.", "warning");
                }
                else if (txt_Remarks.Text == "")
                {
                    txt_Remarks.Focus();
                    Alertme("Please enter ramarks.", "warning");
                }
                else
                {
                    if (ddl_status.Text == "Approved")
                    {
                        DataTable dt = My.dataTable("select *,(select top 1 content from Content_master where content_id=Discount_Master_temp.fee_head_id) as Fee_head_name from Discount_Master_temp where Student_discount_type_entry_id='" + ViewState["couponCodE"].ToString() + "'");
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                SqlCommand cmd;
                                string queryD = "INSERT INTO Discount_Master (Class_id,Discount_on,session,Discount_per,group_id,admission_no,month,fee_head_id,disc_amount,parameter_id,session_id,Branch_id,User_id,Date,time,discount_for,category_id,sub_category_id,Upload_from,Hostel_id,Room_Category_id,Student_Discunt_Type_id,Student_Discunt_Remarks,Student_discount_type_entry_id) values (@Class_id,@Discount_on,@session,@Discount_per,@group_id,@admission_no,@month,@fee_head_id,@disc_amount,@parameter_id,@session_id,@Branch_id,@User_id,@Date,@time,@discount_for,@category_id,@sub_category_id,@Upload_from,@Hostel_id,@Room_Category_id,@Student_Discunt_Type_id,@Student_Discunt_Remarks,@Student_discount_type_entry_id)";
                                cmd = new SqlCommand(queryD);
                                cmd.Parameters.AddWithValue("@Class_id", dr["Class_id"].ToString());
                                cmd.Parameters.AddWithValue("@Discount_on", dr["Discount_on"].ToString());
                                cmd.Parameters.AddWithValue("@session", dr["session"].ToString());
                                cmd.Parameters.AddWithValue("@Discount_per", dr["Discount_per"].ToString());
                                cmd.Parameters.AddWithValue("@group_id", dr["group_id"].ToString());
                                cmd.Parameters.AddWithValue("@admission_no", dr["admission_no"].ToString());
                                cmd.Parameters.AddWithValue("@month", dr["month"].ToString());
                                cmd.Parameters.AddWithValue("@fee_head_id", dr["fee_head_id"].ToString());
                                cmd.Parameters.AddWithValue("@disc_amount", dr["disc_amount"].ToString());
                                cmd.Parameters.AddWithValue("@parameter_id", dr["parameter_id"].ToString());
                                cmd.Parameters.AddWithValue("@session_id", dr["session_id"].ToString());
                                cmd.Parameters.AddWithValue("@Branch_id", dr["Branch_id"].ToString());
                                cmd.Parameters.AddWithValue("@User_id", ViewState["Userid"].ToString());
                                cmd.Parameters.AddWithValue("@Date", mycode.date());
                                cmd.Parameters.AddWithValue("@time", mycode.time());
                                cmd.Parameters.AddWithValue("@discount_for", dr["discount_for"].ToString());
                                cmd.Parameters.AddWithValue("@category_id", dr["category_id"].ToString());
                                cmd.Parameters.AddWithValue("@sub_category_id", dr["sub_category_id"].ToString());
                                cmd.Parameters.AddWithValue("@Upload_from", "");
                                cmd.Parameters.AddWithValue("@Hostel_id", dr["Hostel_id"].ToString());
                                cmd.Parameters.AddWithValue("@Room_Category_id", dr["Room_Category_id"].ToString());
                                cmd.Parameters.AddWithValue("@Student_Discunt_Type_id", dr["Student_Discunt_Type_id"].ToString());
                                cmd.Parameters.AddWithValue("@Student_Discunt_Remarks", dr["Student_Discunt_Remarks"].ToString());
                                cmd.Parameters.AddWithValue("@Student_discount_type_entry_id", dr["Student_discount_type_entry_id"].ToString());
                                if (My.InsertUpdateData(cmd))
                                {
                                    DataTable dtF = My.dataTable("select * from Typewise_fee_collection where admission_no='" + dr["admission_no"].ToString() + "' and session='" + dr["session"].ToString() + "' and (group_id='1' or group_id='2') and content_id='" + dr["fee_head_id"].ToString() + "'");
                                    if (dtF.Rows.Count > 0)
                                    {
                                        double payableAfterDisc = My.toDouble(dtF.Rows[0]["payable"].ToString()) - My.toDouble(dr["disc_amount"].ToString());
                                        My.exeSql("update Typewise_fee_collection set Disc='" + My.toDouble(dr["disc_amount"].ToString()).ToString("0.00") + "',Payable_after_disc='" + payableAfterDisc.ToString("0.00") + "' where Id=" + dtF.Rows[0]["Id"].ToString() + "");
                                    }
                                }
                            }

                            ///=========================================
                            if (ViewState["Is_Send_WhatsApp"].ToString().ToUpper() == "TRUE")
                            {
                                try
                                {
                                    var dtW = My.dataTable("select top 1 * from Whatsapp_api_config where Status='running'");
                                    if (dtW.Rows.Count == 1)
                                    {
                                        ViewState["whatsapp_mobile_no"] = dtW.Rows[0]["SMS_API"].ToString();
                                        ViewState["Whatsapp_api_url"] = dtW.Rows[0]["url"].ToString();
                                    }
                                    else
                                    {
                                        ViewState["whatsapp_mobile_no"] = "";
                                        ViewState["Whatsapp_api_url"] = "";
                                    }

                                    if (ViewState["father_mob"].ToString().Length > 9)
                                    {
                                        string playStore = My.get_single_column_data("select top 1 Update_Url as Column_Name from Update_details");
                                        //  string message = "Congratulations! Dear Studnet " + ViewState["std_name"].ToString() + " you have won a discount voucher worth. Your voucher code is " + ViewState["couponCodE"].ToString() + " and it will be applicable for your next transaction.  Regards " + lbl_heading.Text;
                                        string message = "Congratulations! Dear Student, " + ViewState["std_name"].ToString() + ", you have won a discount voucher! Your voucher code is " + ViewState["couponCodE"].ToString() + ", and it will be applicable on your next transaction. For more details, please download our school app by clicking the link " + playStore + " Regards " + lbl_heading.Text;
                                        string message1 = Uri.EscapeDataString(message);
                                        string mobile_no = "91" + ViewState["mobile_no"].ToString();
                                        string _url = "";
                                        if (ViewState["Whatsapp_api_url"].ToString().Contains("app.allexpert.in"))
                                        {
                                            _url = String.Format(ViewState["Whatsapp_api_url"].ToString(), ViewState["whatsapp_mobile_no"].ToString(), mobile_no, message1);  //+  + "&message=" + message + "&phone=91" + mobile_no;
                                        }
                                        if (ViewState["Whatsapp_api_url"].ToString().Contains("api4ws.com"))
                                        {
                                            _url = String.Format(ViewState["Whatsapp_api_url"].ToString(), ViewState["whatsapp_mobile_no"].ToString(), message1, mobile_no);  //+  + "&message=" + message + "&phone=91" + mobile_no;
                                        }
                                        else
                                        {
                                            _url = String.Format(ViewState["Whatsapp_api_url"].ToString(), ViewState["whatsapp_mobile_no"].ToString(), message1, mobile_no);  //+  + "&message=" + message + "&phone=91" + mobile_no;
                                        }
                                        SqlCommand cmd;
                                        string querychk = "INSERT INTO WhatsApp_send (Mobile_no,Message,Message_url,Session_id,Admission_no,Status,Date,Idate,Time,Send_by,Mesage_Type) values (@Mobile_no,@Message,@Message_url,@Session_id,@Admission_no,@Status,@Date,@Idate,@Time,@Send_by,@Mesage_Type)";
                                        cmd = new SqlCommand(querychk);
                                        cmd.Parameters.AddWithValue("@Mobile_no", ViewState["mobile_no"].ToString());
                                        cmd.Parameters.AddWithValue("@Message", message);
                                        cmd.Parameters.AddWithValue("@Message_url", _url);
                                        cmd.Parameters.AddWithValue("@Session_id", "");
                                        cmd.Parameters.AddWithValue("@Admission_no", "");
                                        cmd.Parameters.AddWithValue("@Status", "Pending");
                                        cmd.Parameters.AddWithValue("@Date", mycode.date());
                                        cmd.Parameters.AddWithValue("@Idate", mycode.idate());
                                        cmd.Parameters.AddWithValue("@Time", mycode.time());
                                        cmd.Parameters.AddWithValue("@Send_by", ViewState["Userid"].ToString());
                                        cmd.Parameters.AddWithValue("@Mesage_Type", "english");
                                        if (My.InsertUpdateData(cmd))
                                        {
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    //My.submitexception("Exception from Whatsapp Message =" + ex.ToString());
                                }
                            }
                        }
                    }


                    My.exeSql("update Coupon_applied_list set Status='" + ddl_status.Text + "',Remarks='" + txt_Remarks.Text + "',Updated_By='" + ViewState["Userid"].ToString() + "',Updated_date='" + mycode.date() + "',Updated_time='" + mycode.time() + "',Updated_idate='" + mycode.idate() + "' where Id='" + ViewState["rowID"].ToString() + "'; update Discount_Master_temp set Status='" + ddl_status.Text + "' where Student_discount_type_entry_id='" + ViewState["couponCodE"].ToString() + "' ");
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbl_status = ((Label)e.Item.FindControl("lbl_status")) as Label;
                if (lbl_status.Text == "Pending")
                {
                    lbl_status.Attributes.Add("class", "statusPending");
                }
                if (lbl_status.Text == "Approved")
                {
                    lbl_status.Attributes.Add("class", "statusApproved");
                }
                if (lbl_status.Text == "Reject")
                {
                    lbl_status.Attributes.Add("class", "statusReject");
                }
            }
        }
    }
}