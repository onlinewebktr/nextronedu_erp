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
    public partial class set_student_wise_discount_s_month : System.Web.UI.Page
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
                        if (Session["MsgeS"] != null)
                        {
                            Alertme(Session["MsgeS"].ToString(), "success");
                            Session["MsgeS"] = null;
                        }
                        ViewState["Is_quarterwise_payment"] = "0";
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["Branch_id"] = mycode.get_branch_id(Session["Admin"].ToString());
                        find_firm_details();
                        try
                        {
                            mycode.bind_all_ddl_with_id(ddlsessionad, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");
                            mycode.bind_all_ddl_with_id(ddl_session_student, "select Session,session_id  from dbo.[session_details] order by cast((Substring (Session,1,4)) as int) desc");

                            ddlsessionad.SelectedValue = My.get_session_id();
                            ddl_session_student.SelectedValue = My.get_session_id();
                        }
                        catch
                        {
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Mapping_Transportation_with_Student");
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

        private void find_firm_details()
        {
            DataTable dt = mycode.FillData("select * from Firm_Details");
            if (dt.Rows.Count > 0)
            {
                try
                {
                    if (dt.Rows[0]["Is_quarterwise_payment"].ToString() == "True")
                    {
                        ViewState["Is_quarterwise_payment"] = "1";
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }


        #region WebMethoD
        [WebMethod]
        public static List<string> GetRooPath(string PathRooT, string Session_id)
        {
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(My.conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct studentname from admission_registor where studentname LIKE ''+@SearchMobNo+'%'  and  Transfer_Status in ('NT','New' ) and  Status='1' and Session_id=" + Session_id + "";
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@SearchMobNo", PathRooT);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        MobResult.Add(dr["studentname"].ToString());
                    }
                    con.Close();
                    return MobResult;
                }
            }
        }

        [WebMethod]
        public static List<string> GetRooPathAdmNo(string PathRooT, string Session_id)
        {
            List<string> MobResult = new List<string>();
            using (SqlConnection con = new SqlConnection(My.conn))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct admissionserialnumber from admission_registor where admissionserialnumber LIKE ''+@SearchMobNo+'%'   and  Transfer_Status in ('NT','New' ) and  Status='1' and Session_id=" + Session_id + "";
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

        #endregion

        protected void btn_find_admission_no_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlsessionad.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session.", "warning");
                    ddlsessionad.Focus();
                }
                else if (txt_admission_no.Text == "")
                {
                    Alertme("Please enter admission no.", "warning");
                    txt_admission_no.Focus();
                }
                else
                {
                    find_by_admission_no();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_by_admission_no()
        {
            string qry = "select * from admission_registor where admissionserialnumber='" + txt_admission_no.Text + "' and Session_id='" + ddlsessionad.SelectedValue + "' and Status='1' order by class,rollnumber asc";
            find_details(qry);
        }

        private void bind_gird(string qry)
        {
            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {
                Alertme("Student not found.", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalSTD();", true);
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }

        protected void btn_find_name_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_session_student.SelectedItem.Text == "Select")
                {
                    Alertme("Please select session.", "warning");
                    ddl_session_student.Focus();
                }
                else if (txt_student_name.Text == "")
                {
                    Alertme("Please enter student name.", "warning");
                    txt_student_name.Focus();
                }
                else
                {
                    find_by_student_name();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void find_by_student_name()
        {
            string qry = "select * from admission_registor where studentname LIKE '%" + txt_student_name.Text + "%' and Session_id='" + ddl_session_student.SelectedValue + "' and Status='1' order by class,rollnumber asc";
            bind_gird(qry);
        }


        UsesCode myusecode = new UsesCode();
        private void find_details(string query)
        {
            SqlDataAdapter ad_contactus = new SqlDataAdapter(query, My.conn);
            DataSet ds = new DataSet();
            ad_contactus.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                fndSectionDV.Visible = true;
                std_basic_infoS.Visible = false;
                Alertme("Student details not found...", "warning");
                return;
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ViewState["parameterDisc"] = "3";
                    if (dt.Rows[0]["hosteltaken"].ToString() == "")
                    {
                        ViewState["hostaltakenDues"] = "No";
                    }
                    else if (dt.Rows[0]["hosteltaken"].ToString().ToLower() == "no")
                    {
                        ViewState["hostaltakenDues"] = "No";
                    }
                    else
                    {
                        ViewState["hostaltakenDues"] = dt.Rows[0]["hosteltaken"].ToString();
                    }

                    fndSectionDV.Visible = false;
                    std_basic_infoS.Visible = true;
                    ddl_session_student.SelectedValue = dr["Session_id"].ToString();
                    ddlsessionad.SelectedValue = dr["Session_id"].ToString();
                    lbl_name.Text = dr["studentname"].ToString();
                    txt_student_name.Text = dr["studentname"].ToString();
                    lbl_father_name.Text = dr["fathername"].ToString();
                    lblclass_show.Text = dr["class"].ToString();
                    lblclass.Text = dr["class"].ToString() + " / " + dr["Section"].ToString();
                    ViewState["class_id"] = dr["Class_id"].ToString();
                    txtroll_no.Text = dr["rollnumber"].ToString();
                    lbl_admission_no.Text = dr["admissionserialnumber"].ToString();
                    txt_admission_no.Text = dr["admissionserialnumber"].ToString();
                    hd_admission_no.Value = dr["admissionserialnumber"].ToString();
                    lblhostel.Text = dr["hosteltaken"].ToString();
                    Image1.ImageUrl = dr["studentimagepath"].ToString();
                    if (dr["studentimagepath"].ToString() == "")
                    { Image1.Visible = false; }
                    else
                    { Image1.Visible = true; }

                    //lbl_admission_no_c.Text = dr["admissionserialnumber"].ToString();
                    //lbl_name_c.Text = dr["studentname"].ToString();
                    //lblclass_show_c.Text = dr["class"].ToString() + " / " + dr["Section"].ToString();
                    //lbl_transport_c.Text = dr["transportationtaken"].ToString();
                    //lbl_hostel_c.Text = dr["hosteltaken"].ToString();

                    ViewState["parameter"] = dr["hosteltaken"].ToString().ToUpper() == "YES" ? "HostelMonthlyFee" : "MonthlyFee";
                    ViewState["parameter_id"] = dr["hosteltaken"].ToString().ToUpper() == "YES" ? "3" : "4";
                    ViewState["Admission_no"] = dr["admissionserialnumber"].ToString();
                    lbl_phone.Text = dr["mobilenumber"].ToString();
                    ViewState["hostel_id"] = My.toint(dr["Hostel_id"].ToString());

                    // confussion 
                    ViewState["day_bording"] = My.toBool(dr["is_applied_dayboarding"]);
                    ViewState["day_bording_with_lunch"] = My.toBool(dr["day_boarding_with_lunch"]);


                    ViewState["group_id"] = "3";
                    ViewState["category_id"] = dr["category_id"].ToString();
                    ViewState["sub_category_id"] = dr["SubCategory_id"].ToString();
                    ViewState["classid"] = dr["Class_id"].ToString();
                    ViewState["Section"] = dr["Section"].ToString();
                    ViewState["sessionIDs"] = dr["Session_id"].ToString();
                    ViewState["sessionid"] = dr["Session_id"].ToString();
                    ViewState["session"] = dr["session"].ToString();

                    ViewState["Transfer_Status"] = dr["Transfer_Status"].ToString();
                    if (dr["Transfer_Status"].ToString().ToUpper() == "TRANSFERRED")
                    {
                        ViewState["Transfer_Status"] = dr["Transfer_Status_Old"].ToString();
                    }

                    if (ViewState["Transfer_Status"].ToString() == "New")
                    {
                        lbl_studentype.Text = "New";
                        hd_fee_group.Value = "1";
                    }
                    else
                    {
                        lbl_studentype.Text = "Old";
                        hd_fee_group.Value = "2";
                    }

                    Dictionary<string, object> dc1 = mycode.Bind_hostel_data_for_assined_student(ViewState["sessionIDs"].ToString(), ViewState["classid"].ToString(), ViewState["Admission_no"].ToString());
                    ViewState["Hostel_id"] = (String)dc1["Hostel_id"];
                    ViewState["Room_Category_id"] = (String)dc1["Room_Category_id"];
                    ViewState["From_month_name"] = (String)dc1["From_month_name"];
                    ViewState["From_month_id"] = (String)dc1["From_month_id"];
                    ViewState["Assined_Year_Month"] = (String)dc1["Assined_Year_Month"];
                    ViewState["Hostel_assign_id"] = (String)dc1["Hostel_assign_id"];
                    ViewState["Hostel_Bed_id"] = (String)dc1["Bed_id"];
                    if (ViewState["Hostel_id"].ToString() == "0")
                    {
                        ViewState["parameterDisc"] = "4";
                    }
                    else
                    {
                        if (lblhostel.Text.ToUpper() == "YES")
                        {
                            ViewState["IsHostelAssign"] = "1";
                        }
                    }




                    ViewState["IsBoarding"] = "0";
                    ViewState["parameteridS"] = "4";
                    string queryS = "select * from Student_mapping_with_boarding_with_lunch where Session_id='" + dr["Session_id"].ToString() + "' and Admission_no='" + dr["admissionserialnumber"].ToString() + "' and Class_id='" + dr["Class_id"].ToString() + "'";
                    DataTable dts = My.dataTable(queryS);
                    if (dts.Rows.Count != 0)
                    {
                        ViewState["LunchMnthName"] = dts.Rows[0]["Month_name"].ToString();
                        ViewState["LunchMnthId"] = dts.Rows[0]["Month_id"].ToString();
                        ViewState["IsBoarding"] = "1";
                    }

                    lbl_catogery.Text = mycode.get_catogery(dr["Category_id"].ToString());
                    lbl_subcatogery.Text = mycode.get_subcatogery(dr["Category_id"].ToString(), dr["SubCategory_id"].ToString());

                    lbltransporttion.Text = dr["transportationtaken"].ToString();
                    if (dr["Transportation_Id"].ToString() == "")
                    {
                        ViewState["transportID"] = "0";
                    }
                    else
                    {
                        ViewState["transportID"] = dr["Transportation_Id"].ToString();
                    }
                    Dictionary<string, object> dc2 = mycode.Bind_Transport_data_for_assined_student(ViewState["sessionIDs"].ToString(), ViewState["classid"].ToString(), ViewState["Admission_no"].ToString());
                    ViewState["Transport_id"] = (String)dc2["Transport_id"];
                    ViewState["TransportPath_id"] = (String)dc2["TransportPath_id"];
                    ViewState["Boarding_Point_id"] = (String)dc2["Boarding_Point_id"];
                    ViewState["Transport_Assigned_Id"] = (String)dc2["Transport_Assigned_Id"];
                    ViewState["Month_name"] = (String)dc2["Month_name"];
                    ViewState["Month_id"] = (String)dc2["Month_id"];
                    ViewState["Year_month"] = (String)dc2["Year_month"];
                    ViewState["Sheet_Id"] = (String)dc2["Sheet_Id"];


                    if (ViewState["Transport_Assigned_Id"].ToString() == "0")
                    {
                        transport_data.Visible = false;
                    }
                    else
                    {
                        transport_data.Visible = true;
                        lbl_transportname.Text = (String)dc2["Transportname"];
                        lbl_transport_Route.Text = (String)dc2["Transportpathpath"];
                        lbl_boarding_point.Text = (String)dc2["Boarding_Point"];
                        lbl_start_month.Text = (String)dc2["Month_name"];
                        lbl_seatno.Text = (String)dc2["seatname"];
                        ViewState["IsTransportAssign"] = "1";
                    }

                }


                for_discount(ViewState["session"].ToString(), ViewState["sessionIDs"].ToString(), ViewState["classid"].ToString(), ViewState["Admission_no"].ToString(), lblhostel.Text, lbltransporttion.Text, ViewState["Hostel_id"].ToString(), ViewState["Room_Category_id"].ToString(), ViewState["TransportPath_id"].ToString(), ViewState["Boarding_Point_id"].ToString());

            }
        }


        private void fetch_disc_head()
        {
            ViewState["condition"] = "";
            

            string query1 = "";
            if (lblhostel.Text.ToUpper() == "YES")  // hostel month fee
            {
                query1 = "select fmc.content,fmc.content_id,fmc.amount,cm.group_id,'0' as discount from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id  where fmc.class_id='" + ViewState["classid"].ToString() + "' and fmc.session_id='" + ViewState["sessionIDs"].ToString() + "' and fmc.parameter_id='3'  and fmc.Hostel_Id=" + ViewState["Hostel_id"].ToString() + " and fmc.Room_Category_id=" + ViewState["Room_Category_id"].ToString() + " and fmc.Month='" + ddl_disc_month.SelectedItem.Text + "'";
            }
            else
            {
                if (lbltransporttion.Text.ToUpper() == "YES")
                {
                    query1 = "select fmc.content,fmc.content_id,fmc.amount,cm.group_id,'0' as discount from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id  where fmc.class_id='" + ViewState["classid"].ToString() + "' and fmc.session_id='" + ViewState["sessionIDs"].ToString() + "' and fmc.parameter_id='4' and fmc.Month='" + ddl_disc_month.SelectedItem.Text + "' UNION all select t1.Parameter as content,parameter_id as content_id,Amount as amount,'0' as group_id,'0' as discount from Transportation_Fee_Master t1 join Month_Index t2 on t1.Month_id=t2.Month_Id where t1.session_id='" + ViewState["sessionIDs"].ToString() + "' and t1.Transportation_path_id='" + ViewState["TransportPath_id"].ToString() + "'and t1.Boarding_Point_id='" + ViewState["Boarding_Point_id"].ToString() + "' and t1.Month='" + ddl_disc_month.SelectedItem.Text + "'";
                }
                else
                {
                    query1 = "select fmc.content,fmc.content_id,fmc.amount,cm.group_id,'0' as discount from dbo.[Fee_master_content_wise] fmc join Content_master cm on fmc.content_id=cm.content_id  where fmc.class_id='" + ViewState["classid"].ToString() + "' and fmc.session_id='" + ViewState["sessionIDs"].ToString() + "' and fmc.parameter_id='4' and fmc.Month='" + ddl_disc_month.SelectedItem.Text + "'";
                }
            }
            DataTable dt1 = mycode.FillData(query1);
            if (dt1.Rows.Count == 0)
            {
                rd_discount_fee_head.DataSource = null;
                rd_discount_fee_head.DataBind();
            }
            else
            {
                rd_discount_fee_head.DataSource = dt1;
                rd_discount_fee_head.DataBind();
            }
        }

        //=======================DISCOUNTS
        #region DISCOUNT
        private void for_discount(string session_name, string session_id, string class_id, string admission_no, string hostel_taken, string transport_taken, string hostel_id, string room_category_id, string TransportPath_id, string Boarding_Point_id)
        {
            mycode.bind_all_ddl_with_id_no_select(ddl_discount_mode, "select Discunt_Type,Student_Discunt_Type_id from Student_Discunt_Type order by Discunt_Type");
            string qryDiscMonth = "select Month,Month_Id from Month_Index where Month in (select month from Typewise_fee_collection where admission_no = '" + admission_no + "' and(parameter = 'MonthlyFee'  or parameter = 'HostelMonthlyFee') and session = '" + session_name + "' and class_id = '" + class_id + "' and status = 'Dues') or Month not in (select month from Typewise_fee_collection where admission_no = '" + admission_no + "' and(parameter = 'MonthlyFee'  or parameter = 'HostelMonthlyFee') and session = '" + session_name + "' and class_id = '" + class_id + "' and status = 'Paid') order by Position asc";
            if (ViewState["Is_quarterwise_payment"].ToString() == "1")
            {
                qryDiscMonth = "select Month,Month_Id from (select *,(select isnull(sum(convert(float, amount)),0) from Fee_master_content_wise where (parameter = 'MonthlyFee'  or parameter = 'HostelMonthlyFee') and session_id='" + ViewState["sessionIDs"].ToString() + "' and class_id='" + ViewState["classid"].ToString() + "' and Month=Month_Index.Month)  as Total_fee from Month_Index where Month in (select month from Typewise_fee_collection where admission_no = '" + admission_no + "' and(parameter = 'MonthlyFee'  or parameter = 'HostelMonthlyFee') and session = '" + session_name + "' and class_id = '" + class_id + "' and status = 'Dues') or Month not in (select month from Typewise_fee_collection where admission_no = '" + admission_no + "' and(parameter = 'MonthlyFee'  or parameter = 'HostelMonthlyFee') and session = '" + session_name + "' and class_id = '" + class_id + "' and status = 'Paid')) t where convert(float, Total_fee)>0 order by Position asc";
            }
            mycode.bind_all_ddl_with_id(ddl_disc_month, qryDiscMonth);
            fetch_given_discount();
        }

        private void fetch_given_discount()
        {
            string qrydiscount = "select MTH.Position,ar.session,ar.Session_id,ar.Class_id,ar.admissionserialnumber as Admission_no,dm.Date,dm.month,dm.Discount_on,(Select Discunt_Type from Student_Discunt_Type where Student_Discunt_Type_id=dm.Student_Discunt_Type_id) as Discunt_Type,fmc.content,dm.fee_head_id as content_id,fmc.amount,dm.disc_amount, (cast(amount as float)-cast(disc_amount as float)) after_disc from dbo.[Discount_Master] dm  join Fee_master_content_wise fmc on dm.fee_head_id=fmc.content_id and dm.Class_id=fmc.class_id and dm.session=fmc.session and dm.parameter_id=fmc.parameter_id and dm.month=fmc.Month join Add_course_table ac on dm.Class_id=ac.course_id join admission_registor ar on dm.admission_no=ar.admissionserialnumber and dm.session=ar.session join Month_Index MTH on dm.month=MTH.Month where dm.group_id='3' and dm.session='" + ViewState["session"].ToString() + "' and ar.admissionserialnumber='" + ViewState["Admission_no"].ToString() + "' and cast(dm.disc_amount as float) >0 UNION all select MTH.Position,ar.session,ar.Session_id,ar.Class_id,ar.admissionserialnumber as Admission_no,dm.Date,dm.month,dm.Discount_on,(Select Discunt_Type from Student_Discunt_Type where Student_Discunt_Type_id = dm.Student_Discunt_Type_id) as Discunt_Type,dm.Discount_on as content,CONVERT(varchar(100), tfm.parameter_id) as content_id,tfm.amount,dm.disc_amount, (cast(amount as float) - cast(disc_amount as float)) after_disc from Discount_Master_for_bus dm join admission_registor ar on dm.admission_no = ar.admissionserialnumber and dm.session = ar.session join Transportation_Fee_Master tfm on dm.session_id = tfm.session_id and dm.Month = tfm.Month and dm.TransportationPath_id = tfm.Transportation_path_id and dm.Boarding_Point_id = tfm.Boarding_Point_id  join Month_Index MTH on dm.month=MTH.Month where admission_no = '" + ViewState["Admission_no"].ToString() + "' and dm.session = '" + ViewState["session"].ToString() + "' and dm.TransportationPath_id= '" + ViewState["TransportPath_id"].ToString() + "' and dm.Boarding_Point_id= '" + ViewState["Boarding_Point_id"].ToString() + "' and cast(dm.disc_amount as float) > 0 UNION all select '0' as Position,ar.session,ar.Session_id,ar.Class_id,ar.admissionserialnumber as Admission_no,dm.Date,dm.month,dm.Discount_on,(Select Discunt_Type from Student_Discunt_Type where Student_Discunt_Type_id=dm.Student_Discunt_Type_id) as Discunt_Type,fmc.content,dm.fee_head_id as content_id,fmc.amount,dm.disc_amount, (cast(amount as float)-cast(disc_amount as float)) after_disc from dbo.[Discount_Master] dm  join Fee_master_content_wise fmc on dm.fee_head_id=fmc.content_id and dm.Class_id=fmc.class_id and dm.session=fmc.session and dm.parameter_id=fmc.parameter_id join Add_course_table ac on dm.Class_id=ac.course_id join admission_registor ar on dm.admission_no=ar.admissionserialnumber and dm.session=ar.session where dm.group_id='" + hd_fee_group.Value + "' and dm.session='" + ViewState["session"].ToString() + "' and ar.admissionserialnumber='" + ViewState["Admission_no"].ToString() + "' and cast(dm.disc_amount as float) >0 order by Position asc";
            DataTable dtdiscount = mycode.FillData(qrydiscount);
            if (dtdiscount.Rows.Count == 0)
            {
                discgridDV.Visible = false;
                rd_discount.DataSource = null;
                rd_discount.DataBind();
            }
            else
            {
                discgridDV.Visible = true;
                rd_discount.DataSource = dtdiscount;
                rd_discount.DataBind();
            }
        }

        protected void btn_save_discount_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["issubmit"] = "0";
                save_data();
                if (ViewState["issubmit"].ToString() == "1")
                {
                    fetch_given_discount();
                    Session["IsDiscountOpen"] = "0";
                    Alertme("Discount Applied successfully for " + txt_student_name.Text, "success");
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void save_data()
        {

            string query = " ";
            bool isamountfill = false;
            for (int i = 0; i < rd_discount_fee_head.Items.Count; i++)
            {
                TextBox txt_disc_fee = (TextBox)rd_discount_fee_head.Items[i].FindControl("txt_disc_fee");
                if (My.toDouble(txt_disc_fee.Text) > 0)
                {
                    isamountfill = true;
                }
            }


            if (isamountfill == true)
            {
                #region MonthLY 
                string Month = ddl_disc_month.SelectedItem.Text;
                double totla = 0;
                for (int i = 0; i < rd_discount_fee_head.Items.Count; i++)
                {
                    TextBox txt_fee = (TextBox)rd_discount_fee_head.Items[i].FindControl("txt_head_fee");
                    TextBox txt_disc_fee = (TextBox)rd_discount_fee_head.Items[i].FindControl("txt_disc_fee");
                    Label lbl_content_id = (Label)rd_discount_fee_head.Items[i].FindControl("lbl_content_id");
                    Label lbl_content = (Label)rd_discount_fee_head.Items[i].FindControl("lbl_content");
                    totla = totla + My.toDouble(txt_fee.Text);
                    //==============*************** 

                    if (lbl_content_id.Text == "1002") // FOR TRANSPORT
                    {
                        if (My.toDouble(txt_disc_fee.Text) > 0)
                        {
                            save_bus_discount();
                        }
                    }
                    else
                    {
                        if (My.toDouble(txt_disc_fee.Text) > 0)
                        {
                            string discount_fee = txt_disc_fee.Text;
                            if (My.toDouble(txt_disc_fee.Text) > My.toDouble(txt_fee.Text))
                            {
                                discount_fee = txt_fee.Text;
                            }

                            DataTable dtF = My.dataTable("select * from Typewise_fee_collection where admission_no='" + hd_admission_no.Value + "' and session='" + ViewState["session"].ToString() + "' and group_id='3' and content_id='" + lbl_content_id.Text + "' and month='" + Month + "' and status='Paid'");
                            if (dtF.Rows.Count == 0)
                            {
                                //CHECK IN TYPEWISE
                                DataTable dtT = My.dataTable("select * from Typewise_fee_collection where admission_no='" + hd_admission_no.Value + "' and session='" + ViewState["session"].ToString() + "' and group_id='3' and content_id='" + lbl_content_id.Text + "' and month='" + Month + "' and status='Dues'");
                                if (dtT.Rows.Count > 0)
                                {
                                    if (My.toDouble(dtT.Rows[0]["dues"].ToString()) > 0)
                                    {
                                        double duesamts = (My.toDouble(dtT.Rows[0]["payable"].ToString()) - My.toDouble(dtT.Rows[0]["paid"].ToString()));
                                        if (My.toDouble(discount_fee) > duesamts)
                                        {
                                            discount_fee = duesamts.ToString();
                                        }
                                    }
                                }


                                //CHECK IN TYPEWISE 
                                if (ViewState["parameterDisc"].ToString() == "3") // hostel
                                {
                                    query = "select * from Discount_Master where Class_id=" + ViewState["class_id"].ToString() + " and session='" + ViewState["session"].ToString() + "' and group_id='3' and month='" + Month + "' and admission_no='" + txt_admission_no.Text + "' and fee_head_id='" + lbl_content_id.Text + "' and parameter_id='" + ViewState["parameterDisc"].ToString() + "' and Hostel_id=" + ViewState["Hostel_id"].ToString() + " and Room_Category_id=" + ViewState["Room_Category_id"].ToString() + "";
                                }
                                else
                                {
                                    query = "select * from Discount_Master where Class_id=" + ViewState["class_id"].ToString() + " and session='" + ViewState["session"].ToString() + "' and group_id='3' and month='" + Month + "' and admission_no='" + txt_admission_no.Text + "' and fee_head_id='" + lbl_content_id.Text + "' and parameter_id='" + ViewState["parameterDisc"].ToString() + "'";
                                }

                                SqlDataAdapter ad = new SqlDataAdapter(query, My.conn);
                                DataSet ds = new DataSet();
                                ad.Fill(ds, "Discount_Master");
                                DataTable dt = ds.Tables[0];
                                if (dt.Rows.Count == 0)
                                {
                                    DataRow dr = dt.NewRow();
                                    dr["Class_id"] = ViewState["class_id"].ToString();
                                    dr["Discount_on"] = "Monthly";
                                    dr["session"] = ViewState["session"].ToString();
                                    dr["Discount_per"] = My.Round((My.toDouble(discount_fee) * 100) / My.toDouble(txt_fee.Text), 2);
                                    dr["group_id"] = "3";
                                    dr["admission_no"] = txt_admission_no.Text;
                                    dr["month"] = Month;
                                    dr["fee_head_id"] = lbl_content_id.Text;
                                    dr["disc_amount"] = My.toDouble(discount_fee).ToString("0.00");
                                    dr["parameter_id"] = ViewState["parameterDisc"].ToString();
                                    dr["category_id"] = ViewState["category_id"].ToString();
                                    dr["sub_category_id"] = ViewState["sub_category_id"].ToString();
                                    dr["session_id"] = ViewState["sessionIDs"].ToString();
                                    dr["Branch_id"] = ViewState["Branch_id"].ToString();
                                    dr["User_id"] = ViewState["Userid"].ToString();
                                    dr["Date"] = mycode.date();
                                    dr["time"] = mycode.time();
                                    dr["Hostel_id"] = ViewState["Hostel_id"].ToString();
                                    dr["Room_Category_id"] = ViewState["Room_Category_id"].ToString();
                                    dr["Student_Discunt_Type_id"] = ddl_discount_mode.SelectedValue;
                                    dr["Student_Discunt_Remarks"] = txt_discount_Remarks.Text;
                                    dt.Rows.Add(dr);

                                    DataTable dtFF = My.dataTable("select * from Typewise_fee_collection where admission_no='" + hd_admission_no.Value + "' and session='" + ViewState["session"].ToString() + "' and group_id='3' and content_id='" + lbl_content_id.Text + "' and month='" + Month + "'");
                                    if (dtFF.Rows.Count > 0)
                                    {
                                        double payableAfterDisc = My.toDouble(dtFF.Rows[0]["payable"].ToString()) - My.toDouble(discount_fee);
                                        My.exeSql("update Typewise_fee_collection set Disc='" + My.toDouble(discount_fee).ToString("0.00") + "',Payable_after_disc='" + payableAfterDisc.ToString("0.00") + "' where Id=" + dtFF.Rows[0]["Id"].ToString() + "");
                                    }
                                }
                                else
                                {
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        dr["Discount_per"] = My.Round((My.toDouble(discount_fee) * 100) / My.toDouble(txt_fee.Text), 2);
                                        dr["disc_amount"] = My.toDouble(discount_fee).ToString("0.00");
                                        dr["Student_Discunt_Type_id"] = ddl_discount_mode.SelectedValue;
                                        dr["Student_Discunt_Remarks"] = txt_discount_Remarks.Text;
                                    }

                                    DataTable dtFF = My.dataTable("select * from Typewise_fee_collection where admission_no='" + hd_admission_no.Value + "' and session='" + ViewState["session"].ToString() + "' and group_id='3' and content_id='" + lbl_content_id.Text + "' and month='" + Month + "'");
                                    if (dtFF.Rows.Count > 0)
                                    {
                                        double payableAfterDisc = My.toDouble(dtFF.Rows[0]["payable"].ToString()) - My.toDouble(discount_fee);
                                        My.exeSql("update Typewise_fee_collection set Disc='" + My.toDouble(discount_fee).ToString("0.00") + "',Payable_after_disc='" + payableAfterDisc.ToString("0.00") + "' where Id=" + dtFF.Rows[0]["Id"].ToString() + "");
                                    }
                                }
                                SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                                ad.Update(dt);
                                ViewState["issubmit"] = "1";
                            }
                        }
                    }
                }
                #endregion



            }
            else
            {
                Alertme("Please select the month during which you want to apply the discount.", "warning");
                return;
            }
        }



        private void save_bus_discount()
        {
            #region #fff
            double totla = 0;
            for (int i = 0; i < rd_discount_fee_head.Items.Count; i++)
            {
                TextBox txt_fee = (TextBox)rd_discount_fee_head.Items[i].FindControl("txt_head_fee");
                TextBox txt_disc_fee = (TextBox)rd_discount_fee_head.Items[i].FindControl("txt_disc_fee");
                Label lbl_content_id = (Label)rd_discount_fee_head.Items[i].FindControl("lbl_content_id");
                Label lbl_content = (Label)rd_discount_fee_head.Items[i].FindControl("lbl_content");
                totla = totla + My.toDouble(txt_fee.Text);

                string Month = ddl_disc_month.SelectedItem.Text;
                //==============*************** 
                if (lbl_content_id.Text == "1002") // FOR TRANSPORT
                {
                    if (My.toDouble(txt_disc_fee.Text) > 0)
                    {
                        string discount_fee = txt_disc_fee.Text;
                        if (My.toDouble(txt_disc_fee.Text) > My.toDouble(txt_fee.Text))
                        {
                            discount_fee = txt_fee.Text;
                        }

                        DataTable dtF = My.dataTable("select * from Typewise_fee_collection where admission_no='" + hd_admission_no.Value + "' and session='" + ViewState["session"].ToString() + "' and group_id='3' and content_id='" + lbl_content_id.Text + "' and month='" + Month + "' and status='Paid'");
                        if (dtF.Rows.Count == 0)
                        {
                            //CHECK IN TYPEWISE
                            DataTable dtT = My.dataTable("select * from Typewise_fee_collection where admission_no='" + hd_admission_no.Value + "' and session='" + ViewState["session"].ToString() + "' and group_id='3' and content_id='" + lbl_content_id.Text + "' and month='" + Month + "' and status='Dues'");
                            if (dtT.Rows.Count > 0)
                            {
                                if (My.toDouble(dtT.Rows[0]["dues"].ToString()) > 0)
                                {
                                    double duesamts = (My.toDouble(dtT.Rows[0]["payable"].ToString()) - My.toDouble(dtT.Rows[0]["paid"].ToString()));
                                    if (My.toDouble(discount_fee) > duesamts)
                                    {
                                        discount_fee = duesamts.ToString();
                                    }
                                }
                            }

                            //CHECK IN TYPEWISE 
                            ViewState["discount_on"] = "TransportFee";
                            SqlDataAdapter ad = new SqlDataAdapter("select * from Discount_Master_for_bus where Bus_path=" + ViewState["TransportPath_id"].ToString() + " and session_id='" + ViewState["sessionIDs"].ToString() + "' and month='" + ddl_disc_month.SelectedItem.Text + "' and admission_no='" + txt_admission_no.Text + "' and Class_id='" + ViewState["class_id"].ToString() + "'", My.conn);
                            DataSet ds = new DataSet();
                            ad.Fill(ds, "Discount_Master_for_bus");
                            DataTable dt = ds.Tables[0];
                            if (dt.Rows.Count == 0)
                            {
                                DataRow dr = dt.NewRow();
                                dr["discount_for"] = "TransportFee";
                                dr["Class_id"] = ViewState["class_id"].ToString();
                                dr["Discount_on"] = ViewState["discount_on"].ToString();
                                dr["session"] = ViewState["session"].ToString();
                                dr["Discount_per"] = My.Round((My.toDouble(discount_fee) * 100) / My.toDouble(txt_fee.Text), 2);
                                dr["group_id"] = "51";
                                dr["admission_no"] = txt_admission_no.Text;
                                dr["month"] = ddl_disc_month.SelectedItem.Text;
                                dr["fee_head_id"] = lbl_content_id.Text;
                                dr["disc_amount"] = My.toDouble(discount_fee).ToString("0.00");
                                dr["parameter_id"] = "0";
                                dr["category_id"] = "0";
                                dr["sub_category_id"] = "0";
                                dr["Bus_path"] = ViewState["TransportPath_id"].ToString();
                                dr["TransportationPath_id"] = ViewState["TransportPath_id"].ToString();
                                dr["session_id"] = ViewState["sessionIDs"].ToString();
                                dr["Branch_id"] = ViewState["Branch_id"].ToString();
                                dr["User_id"] = ViewState["Userid"].ToString();
                                dr["Date"] = mycode.date();
                                dr["time"] = mycode.time();
                                dr["Boarding_Point_id"] = ViewState["Boarding_Point_id"].ToString();
                                dr["Transportation_Id"] = ViewState["Transport_id"].ToString();
                                dr["Student_Discunt_Type_id"] = ddl_discount_mode.SelectedValue;
                                dr["Student_Discunt_Remarks"] = txt_discount_Remarks.Text;
                                dt.Rows.Add(dr);

                                DataTable dtFF = My.dataTable("select * from Typewise_fee_collection where admission_no='" + hd_admission_no.Value + "' and session='" + ViewState["session"].ToString() + "' and group_id='3' and content_id='" + lbl_content_id.Text + "' and month='" + Month + "'");
                                if (dtFF.Rows.Count > 0)
                                {
                                    double payableAfterDisc = My.toDouble(dtFF.Rows[0]["payable"].ToString()) - My.toDouble(discount_fee);
                                    My.exeSql("update Typewise_fee_collection set Disc='" + My.toDouble(discount_fee).ToString("0.00") + "',Payable_after_disc='" + payableAfterDisc.ToString("0.00") + "' where Id=" + dtFF.Rows[0]["Id"].ToString() + "");
                                }
                                ViewState["issubmit"] = "1";
                            }
                            else
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    dr["Discount_per"] = My.Round((My.toDouble(discount_fee) * 100) / My.toDouble(txt_fee.Text), 2);
                                    dr["disc_amount"] = My.toDouble(discount_fee).ToString("0.00");
                                    dr["TransportationPath_id"] = ViewState["TransportPath_id"].ToString();
                                    dr["Student_Discunt_Type_id"] = ddl_discount_mode.SelectedValue;
                                    dr["Student_Discunt_Remarks"] = txt_discount_Remarks.Text;
                                }
                                DataTable dtFF = My.dataTable("select * from Typewise_fee_collection where admission_no='" + hd_admission_no.Value + "' and session='" + ViewState["session"].ToString() + "' and group_id='3' and content_id='" + lbl_content_id.Text + "' and month='" + Month + "'");
                                if (dtFF.Rows.Count > 0)
                                {
                                    double payableAfterDisc = My.toDouble(dtFF.Rows[0]["payable"].ToString()) - My.toDouble(discount_fee);
                                    My.exeSql("update Typewise_fee_collection set Disc='" + My.toDouble(discount_fee).ToString("0.00") + "',Payable_after_disc='" + payableAfterDisc.ToString("0.00") + "' where Id=" + dtFF.Rows[0]["Id"].ToString() + "");
                                }
                                ViewState["issubmit"] = "1";
                            }
                            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
                            ad.Update(dt);
                        }
                    }
                }
            }
            #endregion


        }


        protected void lnkDel_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                //Label lbl_Id = (Label)row.FindControl("lbl_Id");
                Label lbl_session_id = (Label)row.FindControl("lbl_session_id");
                Label lbl_admission_no = (Label)row.FindControl("lbl_admission_no");
                Label lbl_discount_on = (Label)row.FindControl("lbl_discount_on");
                Label lbl_month = (Label)row.FindControl("lbl_month");
                Label lbl_session = (Label)row.FindControl("lbl_session");
                Label lbl_class_id = (Label)row.FindControl("lbl_class_id");
                Label lbl_content_id = (Label)row.FindControl("lbl_content_id");
                if (lbl_discount_on.Text == "Monthly" || lbl_discount_on.Text == "TransportFee")
                {
                    DataTable dt = My.dataTable("select * from Typewise_fee_collection where admission_no='" + lbl_admission_no.Text + "' and (parameter='MonthlyFee'  or parameter='HostelMonthlyFee') and session='" + lbl_session.Text + "' and class_id='" + lbl_class_id.Text + "' and month='" + lbl_month.Text + "' and content_id='" + lbl_content_id.Text + "' and status='Paid'");
                    if (dt.Rows.Count == 0)
                    {
                        mycode.executequery("delete from Discount_Master where admission_no='" + lbl_admission_no.Text + "' and Class_id='" + lbl_class_id.Text + "'  and session_id='" + lbl_session_id.Text + "' and Discount_on='Monthly' and month='" + lbl_month.Text + "' and fee_head_id='" + lbl_content_id.Text + "'; delete from Discount_Master_for_bus where admission_no='" + lbl_admission_no.Text + "' and Class_id='" + lbl_class_id.Text + "'  and session_id='" + lbl_session_id.Text + "' and Discount_on='TransportFee' and month='" + lbl_month.Text + "' and fee_head_id='" + lbl_content_id.Text + "'");
                        //CheckinTypewise
                        DataTable dtT = My.dataTable("select * from Typewise_fee_collection where admission_no='" + lbl_admission_no.Text + "' and (parameter='MonthlyFee'  or parameter='HostelMonthlyFee') and session='" + lbl_session.Text + "' and class_id='" + lbl_class_id.Text + "' and month='" + lbl_month.Text + "' and content_id='" + lbl_content_id.Text + "'");
                        if (dtT.Rows.Count > 0)
                        {
                            double duesAmt = My.toDouble(dtT.Rows[0]["payable"].ToString()) - My.toDouble(dtT.Rows[0]["paid"].ToString());
                            My.exeSql("update Typewise_fee_collection set Disc='0.00',Payable_after_disc=payable,dues='" + duesAmt.ToString("0.00") + "' where Id=" + dtT.Rows[0]["Id"].ToString() + "");
                        }
                        Alertme("Deletion process has been successfully done.", "success");
                        fetch_given_discount();
                    }
                    else
                    {
                        Alertme("You can't delete because fee has been taken for this head.", "warning");
                        return;
                    }
                }
                else if (lbl_discount_on.Text == "Annual" || lbl_discount_on.Text == "Admission")
                {
                    DataTable dt = My.dataTable("select * from Typewise_fee_collection where admission_no='" + lbl_admission_no.Text + "' and session='" + lbl_session.Text + "' and class_id='" + lbl_class_id.Text + "' and group_id='" + hd_fee_group.Value + "' and content_id='" + lbl_content_id.Text + "' and status='Paid'");
                    if (dt.Rows.Count == 0)
                    {
                        mycode.executequery("delete from Discount_Master where admission_no='" + lbl_admission_no.Text + "' and Class_id='" + lbl_class_id.Text + "'  and session_id='" + lbl_session_id.Text + "' and Discount_on='" + lbl_discount_on.Text + "' and fee_head_id='" + lbl_content_id.Text + "'");
                        //CheckinTypewise
                        DataTable dtT = My.dataTable("select * from Typewise_fee_collection where admission_no='" + lbl_admission_no.Text + "' and session='" + lbl_session.Text + "' and class_id='" + lbl_class_id.Text + "' and group_id='" + hd_fee_group.Value + "' and content_id='" + lbl_content_id.Text + "'");
                        if (dtT.Rows.Count > 0)
                        {
                            double duesAmt = My.toDouble(dtT.Rows[0]["payable"].ToString()) - My.toDouble(dtT.Rows[0]["paid"].ToString());
                            My.exeSql("update Typewise_fee_collection set Disc='0.00',Payable_after_disc=payable,dues='" + duesAmt.ToString("0.00") + "' where Id=" + dtT.Rows[0]["Id"].ToString() + "");
                        }
                        Alertme("Deletion process has been successfully done.", "success");
                        fetch_given_discount();
                    }
                    else
                    {
                        Alertme("You can't delete because fee has been taken for this head.", "warning");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        protected void lnk_select_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "";
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbladmissionserialnumber = (Label)row.FindControl("lbl_admissionserialnumber");
                Label lbl_session = (Label)row.FindControl("lbl_session");
                query = "select * from admission_registor where admissionserialnumber='" + lbladmissionserialnumber.Text + "' and session='" + lbl_session.Text + "' and Status='1' order by id asc";
                find_details(query);
            }
            catch (Exception ex)
            {
                My.Save_Exception(ex.ToString());
            }
        }



        protected void ddl_disc_month_SelectedIndexChanged(object sender, EventArgs e)
        {
            fetch_disc_head();
        }


    }
}