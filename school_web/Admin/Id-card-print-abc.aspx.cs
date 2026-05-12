using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class Id_card_print_abc : System.Web.UI.Page
    {
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
                        ViewState["IsPlusTwoChecked"] = "NO";
                        ViewState["Userid"] = Session["Admin"].ToString();
                        string pagename_current = "Id-card-print-abc.aspx";
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];

                        ViewState["Branchid"] = mycode.get_branch_id(ViewState["Userid"].ToString());
                        bind_session();

                        ddlsession.SelectedValue = My.get_session_id();

                        bind_class();
                      
                        bind_Section();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Student_Result");
            }
        }

        private void bind_Section()
        {
            mycode.bind_ddlall(ddl_section, "Select distinct Section from admission_registor where Session_id='" + ddlsession.SelectedValue + "' order by Section");

        }

        private void bind_session()
        {
            mycode.bind_all_ddl_with_id(ddlsession, "Select  Session,session_id from session_details");
        }
        private void bind_class()
        {
            using (SqlConnection conn = new SqlConnection(My.conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Select Course_Name,course_id from Add_course_table order by Position", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                ddl_classs.DataTextField = "Course_Name";
                ddl_classs.DataValueField = "course_id";
                ddl_classs.DataSource = reader;
                ddl_classs.DataBind();
            }
            // mycode.bind_all_ddl_with_id(ddlclass, "Select Course_Name,course_id,Position from Add_course_table order by Position");
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

        My mycode = new My();
        private void bind_grd_view()
        {
            string qry = "";

            string qoute = "'";
            //For Class
            bool isClassSelectd = false; string selectClassid = "";
            foreach (ListItem item in ddl_classs.Items)
            {
                if (item.Selected)
                {
                    selectClassid = selectClassid + qoute + item.Value + qoute + ",";
                    isClassSelectd = true;
                }
            }
            if (isClassSelectd == false)
            {
                ddl_classs.Focus();
                Alertme("Please select class.", "warning");
                return;
            }
            if (isClassSelectd == true)
            {
                selectClassid = selectClassid.Remove(selectClassid.Length - 1);
            }


            if (ddl_section.SelectedItem.Text == "ALL")
            {
                qry = "select * from admission_registor where Session_id=" + ddlsession.SelectedValue + " and Class_id in (" + selectClassid + ") and Status='1' order by rollnumber asc";
            }
            else
            {
                qry = "select * from admission_registor where Session_id=" + ddlsession.SelectedValue + " and Class_id in (" + selectClassid + ") and Section='" + ddl_section.SelectedItem.Text + "' and Status='1' order by rollnumber asc";
            }
            DataTable dt = mycode.FillData(qry);
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no data list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }


        protected void btn_find_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlsession.SelectedItem.Text == "Select")
                {
                    Alertme("please select session.", "warning");
                    ddlsession.Focus();
                }
                
                else if (ddl_id_type.SelectedValue == "0")
                {
                    Alertme("please select type.", "warning");
                    ddl_id_type.Focus();
                }
                else
                {
                    bind_grd_view();
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void btn_print_all_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Print"].ToString() == "1")
                {
                    if (ddlsession.SelectedItem.Text == "Select")
                    {
                        Alertme("Please select session", "warning");
                        ddlsession.Focus();
                    }
                    string qoute = "'";
                    bool isClassSelectd = false; string selectClassid = "";
                    foreach (ListItem item in ddl_classs.Items)
                    {
                        if (item.Selected)
                        {
                            selectClassid = selectClassid + qoute + item.Value + qoute + ",";
                            isClassSelectd = true;
                        }
                    }

                    if (isClassSelectd == false)
                    {
                        ddl_classs.Focus();
                        Alertme("Please select class.", "warning");
                        return;
                    }
                    //if (ddl_classs.GetSelectedIndices().Length == 1)
                    //{
                        // One value is selected
                        string selectedclasss = ddl_classs.SelectedValue;
                        string adm_ids = "";
                        int growcount = rd_view.Items.Count;
                        int k = 0;
                        for (int i = 0; i < growcount; i++)
                        {
                            CheckBox chk = (CheckBox)rd_view.Items[i].FindControl("chkRowData");
                            if (chk.Checked == true)
                            {
                                Label lbl_id = (Label)rd_view.Items[i].FindControl("lbl_id");
                                adm_ids = adm_ids += lbl_id.Text + ",";
                            }
                            else
                            {
                                k++;
                            }
                        }
                        if (k == growcount)
                        {
                            if (lbl_is_check.Text == "1")
                            {
                                if (chk_is_ckeck.Checked == true)
                                {
                                    if (ddl_id_type.SelectedValue == "1")
                                    {
                                        Response.Redirect("id-card/abc/verticle-id-card.aspx?admNo=0&ssion_id=" + ddlsession.SelectedValue + "&clss_id=" + selectedclasss + "&Branch_id=" + ViewState["Branchid"].ToString() + "&Section=" + ddl_section.SelectedItem.Text + "&Type=BULK", false);
                                    }
                                    else
                                    {
                                        Response.Redirect("id-card/abc/horizontal-id.aspx?admNo=0&ssion_id=" + ddlsession.SelectedValue + "&clss_id=" + selectedclasss + "&Branch_id=" + ViewState["Branchid"].ToString() + "&Section=" + ddl_section.SelectedItem.Text + "&Type=BULK", false);
                                    }
                                }
                                else
                                {
                                    if (ddl_id_type.SelectedValue == "1")
                                    {
                                        Response.Redirect("id-card/abc/verticle-id-card.aspx?admNo=0&ssion_id=" + ddlsession.SelectedValue + "&clss_id=" + selectedclasss + "&Branch_id=" + ViewState["Branchid"].ToString() + "&Section=" + ddl_section.SelectedItem.Text + "&Type=BULK", false);
                                    }
                                    else
                                    {
                                        Response.Redirect("id-card/abc/horizontal-id.aspx?admNo=0&ssion_id=" + ddlsession.SelectedValue + "&clss_id=" + selectedclasss + "&Branch_id=" + ViewState["Branchid"].ToString() + "&Section=" + ddl_section.SelectedItem.Text + "&Type=BULK", false);
                                    }
                                }
                            }
                            else
                            {
                                if (ddl_id_type.SelectedValue == "1")
                                {
                                    Response.Redirect("id-card/abc/verticle-id-card.aspx?admNo=0&ssion_id=" + ddlsession.SelectedValue + "&clss_id=" + selectedclasss + "&Branch_id=" + ViewState["Branchid"].ToString() + "&Section=" + ddl_section.SelectedItem.Text + "&Type=BULK", false);
                                }
                                else
                                {
                                    Response.Redirect("id-card/abc/horizontal-id.aspx?admNo=0&ssion_id=" + ddlsession.SelectedValue + "&clss_id=" + selectedclasss + "&Branch_id=" + ViewState["Branchid"].ToString() + "&Section=" + ddl_section.SelectedItem.Text + "&Type=BULK", false);
                                }
                            }
                        }
                        else
                        {
                            if (lbl_is_check.Text == "1")
                            {
                                if (chk_is_ckeck.Checked == true)
                                {
                                    if (ddl_id_type.SelectedValue == "1")
                                    {
                                        string reslink = "id-card/abc/verticle-id-card.aspx?Type=CHECK&ssion_id=" + ddlsession.SelectedValue + "&clss_id=" + selectedclasss + "&Branch_id=" + ViewState["Branchid"].ToString() + "&admNo=" + adm_ids;
                                        Response.Redirect(reslink, false);
                                    }
                                    else
                                    {
                                        string reslink = "id-card/abc/horizontal-id.aspx?Type=CHECK&ssion_id=" + ddlsession.SelectedValue + "&clss_id=" + selectedclasss + "&Branch_id=" + ViewState["Branchid"].ToString() + "&admNo=" + adm_ids;
                                        Response.Redirect(reslink, false);
                                    }
                                }
                                else
                                {
                                    if (ddl_id_type.SelectedValue == "1")
                                    {
                                        string reslink = "id-card/abc/verticle-id-card.aspx?Type=CHECK&ssion_id=" + ddlsession.SelectedValue + "&clss_id=" + selectedclasss + "&Branch_id=" + ViewState["Branchid"].ToString() + "&admNo=" + adm_ids;
                                        Response.Redirect(reslink, false);
                                    }
                                    else
                                    {
                                        string reslink = "id-card/abc/horizontal-id.aspx?Type=CHECK&ssion_id=" + ddlsession.SelectedValue + "&clss_id=" + selectedclasss + "&Branch_id=" + ViewState["Branchid"].ToString() + "&admNo=" + adm_ids;
                                        Response.Redirect(reslink, false);
                                    }
                                }
                            }
                            else
                            {
                                if (ddl_id_type.SelectedValue == "1")
                                {
                                    string reslink = "id-card/abc/verticle-id-card.aspx?Type=CHECK&ssion_id=" + ddlsession.SelectedValue + "&clss_id=" + selectedclasss + "&Branch_id=" + ViewState["Branchid"].ToString() + "&admNo=" + adm_ids;
                                    Response.Redirect(reslink, false);
                                }
                                else
                                {
                                    string reslink = "id-card/abc/horizontal-id.aspx?Type=CHECK&ssion_id=" + ddlsession.SelectedValue + "&clss_id=" + selectedclasss + "&Branch_id=" + ViewState["Branchid"].ToString() + "&admNo=" + adm_ids;
                                    Response.Redirect(reslink, false);
                                }
                            }
                        }
                    //}
                    //else
                    //{
                    //    Alertme("Please select only any one class", "warning");
                    //}
                    
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



        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("trR");
                Label lbl_admissionserialnumber = ((Label)e.Item.FindControl("lbl_admissionserialnumber")) as Label;
                Label lbl_session_id = ((Label)e.Item.FindControl("lbl_session_id")) as Label;
                Label lbl_class_id = ((Label)e.Item.FindControl("lbl_class_id")) as Label;
                Label lbl_branch_id = ((Label)e.Item.FindControl("lbl_branch_id")) as Label;
                HtmlAnchor idcard_link = (HtmlAnchor)e.Item.FindControl("idcard_link");
                Label lbl_Is_Allow_edit = ((Label)e.Item.FindControl("lbl_Is_Allow_edit")) as Label;
                Label lbl_edit_permission = ((Label)e.Item.FindControl("lbl_edit_permission")) as Label;
                if (ViewState["Is_Print"].ToString() == "1")
                {
                    if (ddl_id_type.SelectedValue == "1")
                    {
                        idcard_link.HRef = "id-card/abc/verticle-id-card.aspx?admNo=" + lbl_admissionserialnumber.Text + "&ssion_id=" + lbl_session_id.Text + "&clss_id=" + lbl_class_id.Text + "&Branch_id=" + lbl_branch_id.Text + "&Section=ALL&Type=SINGLE";
                    }
                    else
                    {
                        idcard_link.HRef = "id-card/abc/horizontal-id.aspx?admNo=" + lbl_admissionserialnumber.Text + "&ssion_id=" + lbl_session_id.Text + "&clss_id=" + lbl_class_id.Text + "&Branch_id=" + lbl_branch_id.Text + "&Section=ALL&Type=SINGLE";
                    }
                }
                else
                {
                    idcard_link.InnerText = "Not permission for print";
                }

                if (lbl_Is_Allow_edit.Text == "1")
                {
                    tr.Attributes.Add("style", "background-color:#e8ffe8;color:#000000;");
                    lbl_edit_permission.Text = "Yes";
                }

                else
                {
                    tr.Attributes.Add("style", "background-color:#ffe5c3;color:#000000;");
                    lbl_edit_permission.Text = "No";
                }
            }
        }

        protected void ddlclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            bind_Section();
        }

    }
}