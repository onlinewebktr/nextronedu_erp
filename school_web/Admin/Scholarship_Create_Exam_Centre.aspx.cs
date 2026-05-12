using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class Scholarship_Create_Exam_Centre : System.Web.UI.Page
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
                        ViewState["branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        string pagename_current = "Scholarship_Create_Exam_Centre.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];
                        find_firm_details();


                        bind_test();
                        bind_grd_view();




                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Set_Course_Fee");
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
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                imglogo.ImageUrl = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();
            }
        }
        private void bind_test()
        {

            mycode.bind_all_ddl_with_id(ddl_test_name, "select Test_name,Test_id from Scholarship_Program where Is_active=1 order by  Test_name asc");

         



            mycode.bind_all_ddl_with_id(ddl_test, "select Test_name,Test_id from Scholarship_Program where Is_active=1 order by  Test_name asc");
        }

        private void bind_grd_view()
        {
            string query = " Select orf.*,(Select top  1 Test_name from Scholarship_Program where  Test_id=orf.Test_id) as Test_name,ecr.Room_no   from Scholarship_Exam_Centre orf join Scholarship_Exam_Centre_room_no ecr on   orf.Test_id=ecr.Test_id and orf.Exam_centre_id=ecr.Exam_centre_id where  orf.Branchi_id='" + ViewState["branchid"].ToString() + "' order by ecr.Room_no asc ";

            bind_final_grid_data(query);
        }
        protected void ddl_test_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_test_name.SelectedItem.Text == "Select")
            {
                Alertme("Please select scholarship name", "warning");
            }
            else
            {
                string query = " Select orf.*,(Select top  1 Test_name from Scholarship_Program where  Test_id=orf.Test_id) as Test_name,ecr.Room_no   from Scholarship_Exam_Centre orf join Scholarship_Exam_Centre_room_no ecr on   orf.Test_id=ecr.Test_id and orf.Exam_centre_id=ecr.Exam_centre_id where Branchi_id='" + ViewState["branchid"].ToString() + "' and  orf.Test_id='" + ddl_test_name.SelectedValue + "' order by ecr.Room_no asc";
                bind_final_grid_data(query);
            }

        }
        private void bind_final_grid_data(string query)
        {
            btn_excels.Visible = false;
            DataTable dt = mycode.FillData(query);
            ViewState["query"] = query;
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no scholarship exam center list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                btn_excels.Visible = true;
                rd_view.DataSource = dt;
                rd_view.DataBind();
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

        protected void lnk_delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_delete"].ToString() == "1")
                {
                    hd_id.Value = "";
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");
                    Label lbl_test_id = (Label)row.FindControl("lbl_test_id");
                    Label lbl_Exam_Centre_Id = (Label)row.FindControl("lbl_Exam_Centre_Id");
                    Label lbl_Room_no = (Label)row.FindControl("lbl_Room_no");

                    bool chek_use_room_number = get_room_number(lbl_test_id.Text, lbl_Exam_Centre_Id.Text, lbl_Room_no.Text);

                    if (chek_use_room_number == true)
                    {
                        My.exeSql("Delete from Scholarship_Exam_Centre_room_no where Test_id='" + lbl_test_id.Text + "' and Exam_centre_id='" + lbl_Exam_Centre_Id.Text + "' and Room_no='" + lbl_Room_no.Text + "'");

                        Alertme("Exam centre has been deleted successfully", "success");
                        bind_final_grid_data(ViewState["query"].ToString());


                        string query = "Select count(Id) from Scholarship_Exam_Centre_room_no where  Exam_centre_id=" + lbl_Exam_Centre_Id.Text + " and Test_id=" + lbl_test_id.Text + "";
                        DataTable dt = mycode.FillData(query);
                        if (dt.Rows.Count == 0)
                        {
                            My.exeSql("Delete from Scholarship_Exam_Centre where Id='" + lbl_Id.Text + "'");
                        }
                        else
                        {
                            if(dt.Rows[0][0].ToString()=="0")
                            {
                                My.exeSql("Delete from Scholarship_Exam_Centre where Id='" + lbl_Id.Text + "'");
                            }
                        }
                            



                    }
                    else
                    {
                        Alertme("Sorry you can't delete  room number because  this room number is associated with admit card", "warning");

                    }
                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    hd_id.Value = "";
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");
                    Label lbl_test_id = (Label)row.FindControl("lbl_test_id");
                    Label lbl_Exam_Centre = (Label)row.FindControl("lbl_Exam_Centre");
                    Label lbl_Exam_Centre_Id = (Label)row.FindControl("lbl_Exam_Centre_Id");

                    Label lbl_Room_no = (Label)row.FindControl("lbl_Room_no");
                    Label lbl_Address = (Label)row.FindControl("lbl_Address");
                    Label lbl_Number_room = (Label)row.FindControl("lbl_Number_room");
                    Label lbl_Start_From = (Label)row.FindControl("lbl_Start_From");
                    txt_enter_of_number_of_room.Text = lbl_Number_room.Text;
                    txt_start_from_room_no.Text = lbl_Start_From.Text;
                    ddl_test.SelectedValue = lbl_test_id.Text;
                    txt_Center_Name.Text = lbl_Exam_Centre.Text;
                    txt_address.Text = lbl_Address.Text;
                    hd_id.Value = lbl_Id.Text;
                    hd_centerid.Value = lbl_Exam_Centre_Id.Text;
                    btn_Submit.Text = "Update";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                    bool chek_use_room_number = get_room_number_edit(lbl_test_id.Text, lbl_Exam_Centre_Id.Text, lbl_Room_no.Text);
                    if (chek_use_room_number == true)
                    {
                        txt_enter_of_number_of_room.ReadOnly = false;
                        txt_start_from_room_no.ReadOnly = false;
                    }
                    else
                    {
                        txt_enter_of_number_of_room.ReadOnly = true;
                        txt_start_from_room_no.ReadOnly = true;

                    }

                }
                else
                {
                    Alertme(My.get_restricted_message(), "warning");
                }
            }
            catch (Exception ex)
            {
            }

        }

        private bool get_room_number_edit(string test_id, string Exam_Centre_Id, string Room_no)
        {
            string query = "Select * from Scholarship_Exam_Time_Table where Test_id='" + test_id + "' and Exam_Centre_Id='" + Exam_Centre_Id + "'  ";

            DataTable dt = mycode.FillData(query);

            if (dt.Rows.Count == 0)
            {
                return true;
            }
            else
            {
                return false;

            }
        }
        private bool get_room_number(string test_id, string Exam_Centre_Id, string Room_no)
        {
            string query = "Select * from Scholarship_Exam_Time_Table where Test_id='" + test_id + "' and Exam_Centre_Id='" + Exam_Centre_Id + "' and Room_no='" + Room_no + "'";

            DataTable dt = mycode.FillData(query);

            if (dt.Rows.Count == 0)
            {
                return true;
            }
            else
            {
                return false;

            }
        }




        #region create_exam_center_name
        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            if (ddl_test.SelectedItem.Text == "Select")
            {
                Alertme("Please select scholarship program name", "warning");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            else
            {
                if (btn_Submit.Text == "Add")
                {
                    create_Exam_Center_name();
                }
                else
                {
                    update_Exam_Center_name();
                }
            }

        }

        private void update_Exam_Center_name()
        {
            DataTable dsgdt = My.dataTable("select Test_id from Scholarship_Exam_Centre where Test_id ='" + ddl_test.SelectedValue + "' and Centre_Name='" + txt_Center_Name.Text.Trim() + "' and id!='" + hd_id.Value + "'");
            if (dsgdt.Rows.Count == 0)
            {

                My.Update("Scholarship_Exam_Centre", new
                {
                    Test_id = ddl_test.SelectedValue,
                    Centre_Name = txt_Center_Name.Text.Trim(),
                    Centre_Address = txt_address.Text,
                    Updated_by = ViewState["Userid"].ToString(),
                    Updated_Date = My.getdate1(),
                    Number_room = txt_enter_of_number_of_room.Text,
                    Start_From = txt_start_from_room_no.Text,

                }, where: $"Id='{hd_id.Value}'");
                create_room_no(txt_enter_of_number_of_room.Text, txt_start_from_room_no.Text, hd_centerid.Value);
                ViewState["IsMsgShow"] = "1";
                Alertme("Exam centre name has been successfully update", "success");
                empty_form();
                bind_grd_view();
            }
            else
            {

                Alertme("Sorry your entered centre name name is already exist", "warning");
            }
        }

        private void create_Exam_Center_name()
        {
            string Exam_Centre_Id = "";
            bool chek_create_or_no = check_exam_center_create();
            if (chek_create_or_no == false)
            {
                Alertme("Sorry your exam centre already created", "warning");
            }
            else
            {
                DataTable dsgdt = My.dataTable("select Test_id from Scholarship_Exam_Centre where Test_id ='" + ddl_test.SelectedValue + "' and Centre_Name='" + txt_Center_Name.Text.Trim() + "'");
                if (dsgdt.Rows.Count == 0)
                {
                    Exam_Centre_Id = My.auto_serialS("Scholarship_Exam_Centre_Id");

                    My.Insert("Scholarship_Exam_Centre", new
                    {
                        Branchi_id = ViewState["branchid"].ToString(),
                        Test_id = ddl_test.SelectedValue,
                        Centre_Name = txt_Center_Name.Text.Trim(),
                        Centre_Address = txt_address.Text,
                        Created_date = My.getdate1(),
                        Created_by = ViewState["Userid"].ToString(),
                        Exam_Centre_Id = Exam_Centre_Id,
                        Number_room = txt_enter_of_number_of_room.Text,
                        Start_From = txt_start_from_room_no.Text,
                    }); 

                    create_room_no(txt_enter_of_number_of_room.Text, txt_start_from_room_no.Text, Exam_Centre_Id);
                    ViewState["IsMsgShow"] = "1";
                    Alertme("Exam centre name has been successfully created", "success");
                    empty_form();
                    bind_grd_view();
                }
                else
                {

                    Alertme("Sorry your entered centre name name is already exist", "warning");
                }








            }
        }

        private void empty_form()
        {
            btn_Submit.Text = "Add";
            txt_Center_Name.Text = "";
            txt_address.Text = "";
            txt_enter_of_number_of_room.Text = "";
            txt_start_from_room_no.Text = "";
            bind_final_grid_data(ViewState["query"].ToString());
        }

        private bool check_exam_center_create()
        {
            string query = "Select * from Scholarship_Exam_Centre where Test_id='" + ddl_test.SelectedValue + "' and Centre_Name='" + txt_Center_Name.Text + "'";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private void create_room_no(string no_of_std, string room_start_from, string Exam_Centre_Id)
        {
            int no_of_std1 = My.toint(no_of_std);
            int room_start_from1 = My.toint(room_start_from);

            for (int i = 0; i < no_of_std1; i++)
            {
                

                DataTable dt = My.dataTable("Select Room_no from Scholarship_Exam_Centre_room_no where Room_no='" + room_start_from1 + "' and   Test_id='" + ddl_test.SelectedValue + "' and Exam_centre_id='" + Exam_Centre_Id + "'");
                if (dt.Rows.Count == 0)
                {
                    My.Insert("Scholarship_Exam_Centre_room_no", new
                    {

                        Test_id = ddl_test.SelectedValue,
                        Exam_centre_id = Exam_Centre_Id,
                        Room_no = room_start_from1
                    });

                  
                }
                else
                {

                }
                room_start_from1 = room_start_from1 + 1;

            }



            //string roomno = "";
            //bool duplicateid = false;
            //Random rn = new Random();
            //int i = room_start_from1;
            //int j = no_of_std1 + i;
            //do
            //{
            //    int k = rn.Next(i, j);
            //    roomno = k.ToString();
            //    //if (roomno.Length == 1)
            //    //{
            //    //    roomno = roomno /*"000" +*/ ;
            //    //}
            //    //else if (roomno.Length == 2)
            //    //{
            //    //    roomno = "00" + roomno;
            //    //}
            //    //else if (roomno.Length == 3)
            //    //{
            //    //    roomno = "0" + roomno;
            //    //}
            //    duplicateid = check_dauplicate_ids(roomno, ddl_test.SelectedValue, Exam_Centre_Id);
            //    if (duplicateid == true)
            //    {
            //    }
            //}

            //while (duplicateid == false);
            //return roomno;
        }

        private bool check_dauplicate_ids(string roomno, string Test_id, string Exam_centre_id)
        {
            DataTable dt = My.dataTable("Select Room_no from Scholarship_Exam_Centre_room_no where Room_no='" + roomno + "' and   Test_id='" + Test_id + "' and Exam_centre_id='" + Exam_centre_id + "'");
            if (dt.Rows.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            txt_Center_Name.Text = "";
            txt_address.Text = "";
            txt_enter_of_number_of_room.Text = "";
            txt_start_from_room_no.Text = "";

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
                        Panel1.RenderControl(hw);
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
    }
}