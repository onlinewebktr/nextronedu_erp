using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class annual_fee_dues_report : System.Web.UI.Page
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
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();

                        find_firm_details();
                        string pagename_current = "fee-report.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        #region sms
                        Dictionary<string, object> autosms = mycode.get_auto_message_template("Annual Dues");
                        ViewState["SMS_Tempate"] = (String)autosms["SMS_Tempate"];
                        ViewState["VariableName"] = (String)autosms["VariableName"];
                        ViewState["SMSType"] = (String)autosms["SMSType"];
                        ViewState["Send_From"] = (String)autosms["Send_From"];
                        ViewState["Is_Send_SMS"] = (String)autosms["Is_Send_SMS"];
                        ViewState["Is_Send_WhatsApp"] = (String)autosms["Is_Send_WhatsApp"];






                        pnl_0.Visible = pnl_1.Visible = pnl_2.Visible = pnl_3.Visible = pnl_4.Visible = pnl_5.Visible = pnl_6.Visible = pnl_7.Visible = pnl_8.Visible = false;
                        var vrls = ViewState["VariableName"].ToString().Split(',');
                        txt_message.Text = ViewState["SMS_Tempate"].ToString();
                        if (vrls.Length > 0)
                        {
                            pnl_0.Visible = true;
                            lbl_0.Text = vrls[0];
                            txt_0.Text = "";
                        }
                        if (vrls.Length > 1)
                        {
                            pnl_1.Visible = true;
                            lbl_1.Text = vrls[1];
                            txt_1.Text = "";
                        }
                        if (vrls.Length > 2)
                        {
                            pnl_2.Visible = true;
                            lbl_2.Text = vrls[2];
                            txt_2.Text = "";
                        }
                        if (vrls.Length > 3)
                        {
                            pnl_3.Visible = true;
                            lbl_3.Text = vrls[3];
                            txt_3.Text = "";
                        }
                        if (vrls.Length > 4)
                        {
                            pnl_4.Visible = true;
                            lbl_4.Text = vrls[4];
                            txt_4.Text = "";
                        }
                        if (vrls.Length > 5)
                        {
                            pnl_5.Visible = true;
                            lbl_5.Text = vrls[5];
                            txt_5.Text = "";
                        }
                        if (vrls.Length > 6)
                        {
                            pnl_6.Visible = true;
                            lbl_6.Text = vrls[6];
                            txt_6.Text = "";
                        }
                        if (vrls.Length > 7)
                        {
                            pnl_7.Visible = true;
                            lbl_7.Text = vrls[7];
                            txt_7.Text = "";
                        }
                        if (vrls.Length > 8)
                        {
                            pnl_8.Visible = true;
                            lbl_8.Text = vrls[8];
                            txt_8.Text = "";
                        }
                        #endregion



                        bind_session();
                        ddlsession.SelectedValue = My.get_session_id();
                        bind_class();
                        ddlclass.SelectedValue = My.get_top_one_class();
                        bind_sections();
                        hd_session.Value = ddlsession.SelectedValue;
                        ViewState["flag"] = "0";
                        hd_flag.Value = "0";
                        find_data();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "StudentList");
            }
        }
        private void find_firm_details()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count > 0)
            {
                imglogo.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_emaiid.Text = dt.Rows[0]["email"].ToString();
                lbl_website.Text = dt.Rows[0]["website"].ToString();
                lbl_contact_details.Text = dt.Rows[0]["contact_no"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();
            }
        }
        private void bind_session()
        {
            mycode.bind_all_ddl_with_id(ddlsession, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int)");
        }
        private void bind_class()
        {
            mycode.bind_all_ddl_with_id_cap_All(ddlclass, "Select Course_Name,course_id from Add_course_table order by Position asc");
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
        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session.", "warning");
                    ddlsession.Focus();
                }
                else
                {
                    ViewState["flag"] = "0";
                    hd_flag.Value = "0";
                    find_data();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_data()
        {
            if (ddlclass.SelectedItem.Text == "ALL" && ddl_section.SelectedItem.Text == "ALL")
            {
                bind_grd_view("select * from admission_registor t1 join Add_course_table t2 on t1.Class_id=t2.course_id where t1.Session_Id='" + ddlsession.SelectedValue + "'  and Transfer_Status='NT' and t1.Status=1 order by t2.Position,t1.rollnumber asc");
            }
            else if (ddlclass.SelectedItem.Text != "ALL" && ddl_section.SelectedItem.Text == "ALL")
            {
                bind_grd_view("select * from admission_registor t1 join Add_course_table t2 on t1.Class_id=t2.course_id where t1.Session_Id='" + ddlsession.SelectedValue + "' and t1.Class_id='" + ddlclass.SelectedValue + "'  and Transfer_Status='NT' and t1.Status=1 order by t2.Position,t1.rollnumber asc");
            }
            else
            {
                bind_grd_view("select * from admission_registor t1 join Add_course_table t2 on t1.Class_id=t2.course_id where t1.Session_Id='" + ddlsession.SelectedValue + "' and t1.Class_id='" + ddlclass.SelectedValue + "'  and Transfer_Status='NT' and t1.Status=1 and t1.Section='" + ddl_section.Text + "' order by t2.Position,t1.rollnumber asc");
            } 
            lbl_class22.Text = "Session:" + ddlsession.SelectedItem.Text + " Class :" + ddlclass.SelectedItem.Text;
        }


        private void bind_grd_view(string qry)
        {
            DataTable fdt = new DataTable();
            fdt = mycode.FillData(qry);
            if (fdt.Rows.Count == 0)
            {

                Alertme("Sorry there are no data list exist", "warning");
                GrdView.DataSource = null;
                GrdView.DataBind();
            }
            else
            {
                fdt.Columns.Add("ActualDues", Type.GetType("System.Double"));
                foreach (DataRow dr in fdt.Rows)
                {
                    dr["ActualDues"] = find_annual_dues(dr);
                }
                GrdView.DataSource = fdt;
                GrdView.DataBind();
                bool sendsmspnl = false;
                if (ViewState["Is_Send_SMS"].ToString().ToUpper() == "TRUE")
                {
                    sendsmspnl = true;
                    rd_sms.Visible = true;
                }
                else
                {
                    rd_sms.Visible = false;
                    rd_sms.Checked = false;
                    rd_whatassp.Checked = true;
                }

                if (ViewState["Is_Send_WhatsApp"].ToString().ToUpper() == "TRUE")
                {
                    rd_whatassp.Checked = true;
                    sendsmspnl = true;
                    rd_whatassp.Visible = true;
                }
                else
                {
                    rd_whatassp.Visible = false;
                    rd_whatassp.Checked = false;
                }
                if (sendsmspnl == true)
                {
                    sendsms.Visible = true;
                }
                else
                {
                    sendsms.Visible = false;

                }
                if (ViewState["Is_Print"].ToString() == "1")
                {
                    print1.Visible = true;
                }
                else
                {
                    print1.Visible = false;
                }

            }
        }


        private object find_annual_dues(DataRow dr)
        {
            string Discount_on = "Admission"; string returN = "0";
            if (dr["hosteltaken"].ToString() == "")
            {
                ViewState["hostaltakenDues"] = "No";
                ViewState["hostel_id"] = "0";
            }
            else if (dr["hosteltaken"].ToString().ToLower() == "no")
            {
                ViewState["hostaltakenDues"] = "No";
                ViewState["hostel_id"] = "0";
            }
            else
            {
                ViewState["hostaltakenDues"] = dr["hosteltaken"].ToString();
                if (ViewState["hostaltakenDues"].ToString().ToUpper() == "YES")
                {
                    ViewState["hostel_id"] = "0";
                }
                else
                {
                    ViewState["hostel_id"] = ViewState["Hostel_id"].ToString();
                }
            }
            ViewState["class_id"] = dr["Class_id"].ToString();
            ViewState["Transfer_Status"] = dr["Transfer_Status"].ToString();
            ViewState["studenttype"] = dr["Transfer_Status"].ToString();
            if (dr["Transfer_Status"].ToString().ToUpper() == "TRANSFERRED")
            {
                ViewState["studenttype"] = dr["Transfer_Status_Old"].ToString();
                ViewState["Transfer_Status"] = dr["Transfer_Status_Old"].ToString();
            }

            if (ViewState["studenttype"].ToString() == "New")
            {
                ViewState["parameter"] = dr["hosteltaken"].ToString().ToUpper() == "YES" ? "HostelAdmissionFee" : "AdmissionFee";
                ViewState["parameter_id"] = dr["hosteltaken"].ToString().ToUpper() == "YES" ? "5" : "1";
                Discount_on = "Admission";
            }
            else
            {
                ViewState["parameter"] = dr["hosteltaken"].ToString().ToUpper() == "YES" ? "HostelAnnualFee" : "AnnualFee";
                ViewState["parameter_id"] = dr["hosteltaken"].ToString().ToUpper() == "YES" ? "6" : "2";
                Discount_on = "Annual";
            }
            ViewState["day_bording"] = My.toBool(dr["is_applied_dayboarding"]);
            ViewState["day_bording_with_lunch"] = My.toBool(dr["day_boarding_with_lunch"]);
            ViewState["group_id"] = "3";
            ViewState["category_id"] = dr["category_id"].ToString();
            ViewState["sub_category_id"] = dr["SubCategory_id"].ToString();
            ViewState["classid"] = dr["Class_id"].ToString();
            ViewState["Section"] = dr["Section"].ToString();
            ViewState["sessionIDs"] = dr["Session_id"].ToString();
            ViewState["session"] = dr["session"].ToString();

            string qry = "";
            if (ViewState["hostel_id"].ToString() != "0")
            {
                qry = "select *,(payable-cast(disc_amount as float)-cast(paid as float)) net_payable from(select '0' payable_after_disc,session,feetype,cast(payable as float) payable,paid,dues,status,content_id, Disc as disc_amount from dbo.[Typewise_fee_collection] WHERE admission_no='" + dr["admissionserialnumber"].ToString() + "' and parameter='" + ViewState["parameter"].ToString() + "' and session='" + ViewState["session"].ToString() + "')t";

            }
            else
            {
                qry = "select *,(payable-cast(disc_amount as float)-cast(paid as float)) net_payable from(select '0' payable_after_disc,session,feetype,cast(payable as float) payable,paid,dues,status,content_id, Disc as disc_amount from dbo.[Typewise_fee_collection] WHERE admission_no='" + dr["admissionserialnumber"].ToString() + "' and parameter='" + ViewState["parameter"].ToString() + "' and session='" + ViewState["session"].ToString() + "')t";
            }



            DataTable fee_dt = My.dataTable(qry);
            if (fee_dt.Rows.Count == 0)
            {
                if (ViewState["hostel_id"].ToString() == "0")
                {
                    qry = "select '0' as payable_after_disc, session as Session,Perticular as feetype,Amount as payable,  '0' as paid, Amount as dues,'Dues' as status,'ANN01' as content_id,'0' as disc_amount,Amount as net_payable from Misc_Fee_Master_Studentwise where (Type_Mode='AdmissionFee' or Type_Mode='AnnualFee') and session='" + ViewState["session"].ToString() + "' and Admission_No='" + dr["admissionserialnumber"].ToString() + "' UNION ALL select *,(payable-cast(disc_amount as float)) net_payable from(select '0' payable_after_disc,fmc.session,cm.content feetype,cast(fmc.amount as float) payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where admission_no='" + dr["admissionserialnumber"].ToString() + "'  and parameter_id='" + ViewState["parameter_id"].ToString() + "' and session='" + ViewState["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='" + Discount_on + "' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and parameter_id='" + ViewState["parameter_id"].ToString() + "' and session='" + ViewState["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='" + Discount_on + "' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'))),0) disc_amount   from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='" + ViewState["parameter"].ToString() + "'  and session='" + ViewState["session"].ToString() + "' and class_id='" + ViewState["classid"].ToString() + "')t";
                }
                else
                {
                    qry = "select '0' as payable_after_disc, session as Session,Perticular as feetype,Amount as payable,  '0' as paid, Amount as dues,'Dues' as status,'ANN01' as content_id,'0' as disc_amount,Amount as net_payable from Misc_Fee_Master_Studentwise where (Type_Mode='AdmissionFee' or Type_Mode='AnnualFee') and session='" + ViewState["session"].ToString() + "' and Admission_No='" + dr["admissionserialnumber"].ToString() + "' UNION ALL select *,(payable-cast(disc_amount as float)) net_payable from(select '0' payable_after_disc,fmc.session,cm.content feetype,cast(fmc.amount as float) payable,'0' paid,fmc.amount dues,'Dues' status,cm.content_id, isnull((isnull((select top 1 disc_amount from dbo.[Discount_Master] where Hostel_id=" + ViewState["hostel_id"].ToString() + " and admission_no='" + dr["admissionserialnumber"].ToString() + "'  and parameter_id='" + ViewState["parameter_id"].ToString() + "' and session='" + ViewState["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='" + Discount_on + "'  and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'),(select top 1 disc_amount from dbo.[Discount_Master] where and Hostel_id=" + ViewState["hostel_id"].ToString() + " Class_id='" + ViewState["classid"].ToString() + "' and admission_no='All'  and parameter_id='" + ViewState["parameter_id"].ToString() + "' and session='" + ViewState["session"].ToString() + "' and fee_head_id=cm.content_id and Discount_on='" + Discount_on + "' and category_id='" + ViewState["category_id"].ToString() + "' and sub_category_id='" + ViewState["sub_category_id"].ToString() + "'))),0) disc_amount   from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id where parameter='" + ViewState["parameter"].ToString() + "'  and session='" + ViewState["session"].ToString() + "' and class_id='" + ViewState["classid"].ToString() + "' and   fmc.Hostel_Id=" + ViewState["Hostel_id"].ToString() + ")t";
                }
                fee_dt = My.dataTable(qry);
            } 

            DataTable dtadmdues = mycode.FillData(qry);
            if (dtadmdues.Rows.Count == 0)
            {
            }
            else
            {
                double payable = 0, paid = 0, dues = 0, disc = 0, payble_after_disc = 0;
                foreach (DataRow drF in dtadmdues.Rows)
                {
                    drF["payable_after_disc"] = My.toDouble(drF["payable"]) - My.toDouble(drF["disc_amount"]) - My.toDouble(drF["paid"]);
                    payable += My.toDouble(drF["payable"]);
                    paid += My.toDouble(drF["paid"]);
                    dues += My.toDouble(drF["dues"]);
                    disc += My.toDouble(drF["disc_amount"]);
                    payble_after_disc += My.toDouble(drF["payable_after_disc"]);
                }
                returN = payble_after_disc.ToString("0.00");
            }
            return returN;
        }

        //private string gt_previous_amount(DataRow dr)
        //{
        //    string ReturN = "0";
        //    DataTable dt = mycode.FillData("select Dues_Amount from Previous_Year_Dues  where Session_id='" + ViewState["sessionIDs"].ToString() + "' and AdmissionNumber='" + dr["admissionserialnumber"].ToString() + "'  and Class_id='" + ViewState["classid"].ToString() + "' and Status='Unpaid'");
        //    if (dt.Rows.Count > 0)
        //    {
        //        ReturN = dt.Rows[0][0].ToString();
        //    }
        //    return ReturN;
        //}
         


        protected void btn_fnd_student_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_student_name.Text == "")
                {
                    txt_student_name.Focus();
                    Alertme("Please enter student name.", "warning");
                }
                else
                {
                    ViewState["flag"] = "1";
                    hd_flag.Value = "1";
                    find_data_by_student();
                }
            }
            catch (Exception ex)
            {
            }
        }


        private void find_data_by_student()
        {
            hd_stunt_name.Value = txt_student_name.Text;
            bind_grd_view("select * from admission_registor t1 join Add_course_table t2 on t1.Class_id=t2.course_id where t1.Session_Id='" + ddlsession.SelectedValue + "' and studentname='" + txt_student_name.Text + "' and Status='1' order by t2.Position,t1.rollnumber asc");
        }







        double total_collection = 0;
        protected void GrdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_ActualDues = (Label)e.Row.FindControl("lbl_ActualDues");
                if (lbl_ActualDues.Text != "")
                {
                    total_collection = total_collection + My.toDouble(lbl_ActualDues.Text);
                }


                decimal value;
                if (decimal.TryParse(lbl_ActualDues.Text.Trim(), out value))
                {
                    lbl_ActualDues.Text = value.ToString("0.00");
                }

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_total_dues = (Label)e.Row.FindControl("lbl_total_dues");
                lbl_total_dues.Text = total_collection.ToString("0.00");
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
                    Response.AddHeader("content-disposition", "attachment;filename=Export.xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    using (StringWriter sw = new StringWriter())
                    {
                        HtmlTextWriter hw = new HtmlTextWriter(sw);
                        GrdView.RenderControl(hw);
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


        #region send sms
        protected void btn_msgPreview_Click(object sender, EventArgs e)
        {
            try
            {

                var vrls = ViewState["VariableName"].ToString().Split(',');
                var lst = new String[vrls.Length];
                if (vrls.Length > 0)
                {
                    lst[0] = txt_0.Text;
                }
                if (vrls.Length > 1)
                {
                    lst[1] = txt_1.Text;
                }
                if (vrls.Length > 2)
                {
                    lst[2] = txt_2.Text;
                }
                if (vrls.Length > 3)
                {

                    lst[3] = txt_3.Text;
                }
                if (vrls.Length > 4)
                {

                    lst[4] = txt_4.Text;
                }
                if (vrls.Length > 5)
                {

                    lst[5] = txt_5.Text;
                }
                if (vrls.Length > 6)
                {

                    lst[6] = txt_6.Text;
                }
                if (vrls.Length > 7)
                {

                    lst[7] = txt_7.Text;
                }
                if (vrls.Length > 8)
                {

                    lst[8] = txt_8.Text;
                }
                txt_message.Text = String.Format(ViewState["SMS_Tempate"].ToString(), lst);
                pnl_msg.Visible = true;
            }
            catch (Exception ex)
            {

                Alertme(ex.ToString(), "warning");

            }


        }
        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                bool send = false;
                if (rd_sms.Checked == true)
                {
                    var dt = mycode.FillData("select top 1 * from message_config where Status='running'");
                    if (dt.Rows.Count == 1)
                    {
                        ViewState["api_key"] = dt.Rows[0]["uid"].ToString();
                        ViewState["Sender_id"] = dt.Rows[0]["sender"].ToString();

                        send = true;
                    }
                    else
                    {
                        this.Alertme("Please set sms configuration", "warning");
                        return;
                    }
                }
                else if (rd_whatassp.Checked == true)
                {
                    var dt = mycode.FillData("select top 1 * from Whatsapp_api_config where Status='running'");
                    if (dt.Rows.Count == 1)
                    {
                        send = true;



                        ViewState["whatsapp_mobile_no"] = dt.Rows[0]["SMS_API"].ToString();
                        ViewState["Whatsapp_api_url"] = dt.Rows[0]["url"].ToString();


                    }
                    else
                    {
                        this.Alertme("Please set Whatsapp configuration", "warning");
                        return;

                    }
                }
                else
                {
                    this.Alertme("Please select sms or whatsapp", "warning");
                    return;
                }
                if (send == true)
                {


                    string confirmValue = string.Empty;
                    confirmValue = Request.Form["confirm_value"];
                    if (confirmValue == "Yes")
                    {

                        if (GrdView.Rows.Count == 0)
                        {
                            Alertme("Please dues calculated", "warning");
                        }
                        else
                        {

                            for (int i = 0; i < GrdView.Rows.Count; i++)
                            {
                                Label lbl_studentname = (Label)GrdView.Rows[i].FindControl("lbl_studentname");
                                Label lbl_mobileno = (Label)GrdView.Rows[i].FindControl("lbl_mobileno");
                                Label lbl_whatsapp = (Label)GrdView.Rows[i].FindControl("lbl_whatsapp");
                                Label lbl_admissionserialnumber = (Label)GrdView.Rows[i].FindControl("lbl_admissionserialnumber");
                                Label lbl_class = (Label)GrdView.Rows[i].FindControl("lbl_class");
                                Label lbl_rollnumber = (Label)GrdView.Rows[i].FindControl("lbl_rollnumber");
                                Label lbl_section = (Label)GrdView.Rows[i].FindControl("lbl_section");
                                Label lbl_ActualDues = (Label)GrdView.Rows[i].FindControl("lbl_ActualDues");

                                sendSMS(lbl_studentname.Text, lbl_class.Text, lbl_section.Text, lbl_rollnumber.Text, lbl_mobileno.Text, lbl_whatsapp.Text, lbl_ActualDues.Text, lbl_admissionserialnumber.Text);



                            }
                            this.Alertme("SMS Send Successfully", "success");
                            pnl_msg.Visible = false;
                            sendsms.Visible = false;

                        }
                    }
                    else
                    {
                        Alertme("Sorry you have clicked no", "warning");
                    }
                }
                else
                {
                    this.Alertme("Please check sms and whatsapp configuration", "warning");
                }
            }
            catch
            {

            }


        }

        private void sendSMS(string student_Name, string class_name, string section, string rollnumber, string mobileno, string whatsapp_no, string totaldues, string admission_no)
        {
            if (mobileno == "N/A")
            {

            }
            else if (mobileno == "")
            {

            }
            else if (My.toDouble(totaldues) == 0)
            {

            }
            else
            {

                // Dear Parents, Your Student is { 0 }, Class: - { 1}, Section: - { 2}, Roll No:- { 3}, Admission No:- { 4}. Your School Admission Fee is Rs.- { 5}. Please ensure payment till { 6}
                //of this month.Regards PURNANK SOFTWARE SOLUTIONS

                string mobno = mobileno;
                string admissionId = admission_no;
                txt_0.Text = student_Name;
                txt_1.Text = class_name;
                txt_2.Text = section;
                txt_3.Text = rollnumber;
                txt_4.Text = admissionId;
                txt_5.Text = totaldues;


                //txt_5.Text = student_Name;
                //txt_6.Text = student_Name;
                //txt_7.Text = student_Name;
                //txt_8.Text = student_Name;


                string type = "";

                if (ViewState["SMSType"].ToString() == "Unicode")
                {
                    type = "unicode";
                }
                else
                {
                    type = "english";
                }
                ViewState["type"] = type;


                var vrls = ViewState["VariableName"].ToString().Split(',');
                var lst = new String[vrls.Length];
                if (vrls.Length > 0)
                {
                    lst[0] = txt_0.Text;
                }
                if (vrls.Length > 1)
                {
                    lst[1] = txt_1.Text;
                }
                if (vrls.Length > 2)
                {
                    lst[2] = txt_2.Text;
                }
                if (vrls.Length > 3)
                {

                    lst[3] = txt_3.Text;
                }
                if (vrls.Length > 4)
                {

                    lst[4] = txt_4.Text;
                }
                if (vrls.Length > 5)
                {

                    lst[5] = txt_5.Text;
                }
                if (vrls.Length > 6)
                {

                    lst[6] = txt_6.Text;
                }

                if (vrls.Length > 7)
                {

                    lst[7] = txt_7.Text;
                }
                if (vrls.Length > 8)
                {

                    lst[8] = txt_8.Text;
                }
                txt_message.Text = String.Format(ViewState["SMS_Tempate"].ToString(), lst);
                try
                {
                    if (rd_sms.Checked == true)
                    {
                        string api_key = ViewState["api_key"].ToString();
                        string Sender_id = ViewState["Sender_id"].ToString();
                        string msgtype = ViewState["type"].ToString();


                        string url = "http://mysms.msgclub.net/rest/services/sendSMS/sendGroupSms?AUTH_KEY=" + api_key + "&message=" + txt_message.Text + "&senderId=" + Sender_id + "&routeId=1&mobileNos=" + mobno + "&smsContentType=" + type;

                        HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(url);
                        HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();
                        StreamReader sr = new StreamReader(httpres.GetResponseStream());
                        string results = sr.ReadToEnd();
                        sr.Close();
                        this.Alertme("SMS Send Successfully", "success");
                        My.Insert("Message_Details", new
                        {
                            Mobile_No = mobno,
                            Message = txt_message.Text,
                            Date = mycode.date(),
                            Idate = mycode.idate(),
                            Time = mycode.time(),
                            Result = results,
                            User_id = ViewState["Userid"].ToString(),
                            Mesage_Type = msgtype,
                            Groupcode = "SMS",
                            Status = "SEND",
                            Url = url,
                            Message_to_Type = "Student",
                            admin_user_id = admissionId,
                        });
                    }
                    else if (rd_whatassp.Checked == true)
                    {
                        string sms = String.Format(ViewState["SMS_Tempate"].ToString(), lst);
                        try
                        {
                            if (whatsapp_no.Length > 9)
                            {
                                string message = Uri.EscapeDataString(sms);
                                string mobile_no = "91" + whatsapp_no;
                                string _url = "";




                                if (ViewState["Whatsapp_api_url"].ToString().Contains("app.allexpert.in"))
                                {
                                    //exampe url
                                    //https://app.allexpert.in/send-message?api_key=q0hRX9FA1z5S7JttEIzLtxJU3lvoOv&sender=62888xxxx&number=9162888xxxx&message=Hello World
                                    _url = String.Format(ViewState["Whatsapp_api_url"].ToString(), ViewState["whatsapp_mobile_no"].ToString(), mobile_no, message);  //+  + "&message=" + message + "&phone=91" + mobile_no;
                                }
                                if (ViewState["Whatsapp_api_url"].ToString().Contains("api4ws.com"))
                                {
                                    // _url = My.Whatsapp_api_url + My.whatsapp_mobile_no + "&message=" + message + "&phone=91" + mobile_no;
                                    //https://api4ws.com/sendMessage.php?AUTH_KEY=918877804016&message=User Defined Whatsapp Message&phone=919812345678
                                    _url = String.Format(ViewState["Whatsapp_api_url"].ToString(), ViewState["whatsapp_mobile_no"].ToString(), message, mobile_no);  //+  + "&message=" + message + "&phone=91" + mobile_no;
                                }
                                else
                                {

                                    //https://fastwsapi.com/sendMessage.php?AUTH_KEY=DEMOEDUNEXTG@123&instance_id=575051&message=User Defined Whatsapp Message&phone=919812345678

                                    _url = String.Format(ViewState["Whatsapp_api_url"].ToString(), ViewState["whatsapp_mobile_no"].ToString(), message, mobile_no);  //+  + "&message=" + message + "&phone=91" + mobile_no;
                                }


                                //ServicePointManager.Expect100Continue = true;
                                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                                HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(_url);
                                HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();
                                StreamReader sr = new StreamReader(httpres.GetResponseStream());
                                string results = sr.ReadToEnd();
                                sr.Close();

                                My.Insert("Message_Details", new
                                {
                                    Mobile_No = mobno,
                                    Message = txt_message.Text,
                                    Date = mycode.date(),
                                    Idate = mycode.idate(),
                                    Time = mycode.time(),
                                    Result = results,
                                    User_id = ViewState["Userid"].ToString(),
                                    Mesage_Type = ViewState["type"].ToString(),
                                    Groupcode = "Wahataap",
                                    Status = "SEND",
                                    Url = _url,
                                    Message_to_Type = "Student",
                                    admin_user_id = admissionId,


                                });

                            }
                            //return true;
                        }
                        catch (Exception ex)
                        {
                            My.submitexception("Exception from Whatsapp Message =" + ex.ToString());
                            //return false;
                        }


                    }
                    else
                    {

                    }

                }
                catch (Exception ex)
                {
                    this.Alertme(ex.Message, "warning");
                }

            }



        }







        #endregion

        protected void ddlclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bind_sections();
            }
            catch (Exception ex)
            {
            }
        }
        private void bind_sections()
        {
            My.bind_ddl_all_Cap(ddl_section, "select distinct Section from admission_registor where Session_Id='" + ddlsession.SelectedValue + "' and Class_id='" + ddlclass.SelectedValue + "' and Status=1 order by Section asc");
        }
    }
}