
using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin
{
    public partial class house_master : System.Web.UI.Page
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
                        find_firm_details();
                        ViewState["Userid"] = Session["Admin"].ToString();
                        ViewState["firm_id"] = Session["firm"].ToString();
                        mycode.bind_all_ddl_with_id(ddl_house_color, "select Color_name,Color_id from Color_master order by Color_name asc");
                        string pagename_current = Path.GetFileName(Request.Path);
                        Dictionary<string, object> dc1 = My.get_user_menu_permission_data(ViewState["Userid"].ToString(), pagename_current);
                        ViewState["Is_Edit"] = (String)dc1["Is_Edit"];
                        ViewState["Is_delete"] = (String)dc1["Is_delete"];
                        ViewState["Is_Download"] = (String)dc1["Is_Download"];
                        ViewState["Is_Print"] = (String)dc1["Is_Print"];
                        ViewState["Is_add"] = (String)dc1["Is_add"];

                        if(ViewState["Is_Print"].ToString()=="1")
                        {
                            print1.Visible = true;
                        }
                        else
                        {
                            print1.Visible = false;
                        }
                            


                        //bond_color();
                        bind_grd_view();
                    }
                }
            }
            catch (Exception ex)
            {
                My.submitException(ex, "Add_Strength_Master");
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
                lbl_emaiid.Text = dt.Rows[0]["email"].ToString();
                lbl_website.Text = dt.Rows[0]["website"].ToString();
                lbl_contact_details.Text = dt.Rows[0]["contact_no"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();
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
                        Panel1.RenderControl(hw);
                        Response.Output.Write(sw.ToString());
                        Response.Flush();
                        Response.End();
                    }
                }
                else
                {
                    Alertme("SORRY! You have not permission for this work.", "warning");
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
        private void bond_color()
        {
            ddl_house_color.Items.Add("AliceBlue");
            ddl_house_color.Items.Add("AntiqueWhite");
            ddl_house_color.Items.Add("Aqua");
            ddl_house_color.Items.Add("Aquamarine");
            ddl_house_color.Items.Add("Azure");
            ddl_house_color.Items.Add("Beige");
            ddl_house_color.Items.Add("Bisque");
            ddl_house_color.Items.Add("Black");
            ddl_house_color.Items.Add("BlanchedAlmond");
            ddl_house_color.Items.Add("Blue");
            ddl_house_color.Items.Add("BlueViolet");
            ddl_house_color.Items.Add("Brown");
            ddl_house_color.Items.Add("BurlyWood");
            ddl_house_color.Items.Add("CadetBlue");
            ddl_house_color.Items.Add("Chartreuse");
            ddl_house_color.Items.Add("Chocolate");
            ddl_house_color.Items.Add("Coral");
            ddl_house_color.Items.Add("CornflowerBlue");
            ddl_house_color.Items.Add("Cornsilk");
            ddl_house_color.Items.Add("Crimson");
            ddl_house_color.Items.Add("Cyan");
            ddl_house_color.Items.Add("DarkBlue");
            ddl_house_color.Items.Add("DarkCyan");
            ddl_house_color.Items.Add("DarkGoldenrod");
            ddl_house_color.Items.Add("DarkGray");
            ddl_house_color.Items.Add("DarkGreen");
            ddl_house_color.Items.Add("DarkKhaki");
            ddl_house_color.Items.Add("DarkMagenta");
            ddl_house_color.Items.Add("DarkOliveGreen");
            ddl_house_color.Items.Add("DarkOrange");
            ddl_house_color.Items.Add("DarkOrchid");
            ddl_house_color.Items.Add("DarkRed");
            ddl_house_color.Items.Add("DarkSalmon");
            ddl_house_color.Items.Add("DarkSeaGreen");
            ddl_house_color.Items.Add("DarkSlateBlue");
            ddl_house_color.Items.Add("DarkSlateGray");
            ddl_house_color.Items.Add("DarkTurquoise");
            ddl_house_color.Items.Add("DarkViolet");
            ddl_house_color.Items.Add("DeepPink");
            ddl_house_color.Items.Add("DeepSkyBlue");
            ddl_house_color.Items.Add("DimGray");
            ddl_house_color.Items.Add("DodgerBlue");
            ddl_house_color.Items.Add("Firebrick");
            ddl_house_color.Items.Add("FloralWhite");
            ddl_house_color.Items.Add("ForestGreen");
            ddl_house_color.Items.Add("Fuchsia");
            ddl_house_color.Items.Add("Gainsboro");
            ddl_house_color.Items.Add("GhostWhite");
            ddl_house_color.Items.Add("Gold");
            ddl_house_color.Items.Add("Goldenrod");
            ddl_house_color.Items.Add("Gray");
            ddl_house_color.Items.Add("Green");
            ddl_house_color.Items.Add("GreenYellow");
            ddl_house_color.Items.Add("Honeydew");
            ddl_house_color.Items.Add("HotPink");
            ddl_house_color.Items.Add("IndianRed");
            ddl_house_color.Items.Add("Indigo");
            ddl_house_color.Items.Add("Ivory");
            ddl_house_color.Items.Add("Khaki");
            ddl_house_color.Items.Add("Lavender");
            ddl_house_color.Items.Add("LavenderBlush");
            ddl_house_color.Items.Add("LawnGreen");
            ddl_house_color.Items.Add("LemonChiffon");
            ddl_house_color.Items.Add("LightCyan");
            ddl_house_color.Items.Add("LightGoldenrodYellow");
            ddl_house_color.Items.Add("LightGray");
            ddl_house_color.Items.Add("LightGreen");
            ddl_house_color.Items.Add("LightPink");
            ddl_house_color.Items.Add("LightSalmon");
            ddl_house_color.Items.Add("LightSeaGreen");
            ddl_house_color.Items.Add("LightSkyBlue");
            ddl_house_color.Items.Add("LightSlateGray");
            ddl_house_color.Items.Add("LightSteelBlue");
            ddl_house_color.Items.Add("LightYellow");
            ddl_house_color.Items.Add("Lime");
            ddl_house_color.Items.Add("LimeGreen");
            ddl_house_color.Items.Add("Linen");
            ddl_house_color.Items.Add("Magenta");
            ddl_house_color.Items.Add("Maroon");
            ddl_house_color.Items.Add("MediumAquamarine");
            ddl_house_color.Items.Add("MediumBlue");
            ddl_house_color.Items.Add("MediumOrchid");
            ddl_house_color.Items.Add("MediumPurple");
            ddl_house_color.Items.Add("MediumSeaGreen");
            ddl_house_color.Items.Add("MediumSlateBlue");
            ddl_house_color.Items.Add("MediumSpringGreen");
            ddl_house_color.Items.Add("MediumTurquoise");
            ddl_house_color.Items.Add("MediumVioletRed");
            ddl_house_color.Items.Add("MidnightBlue");
            ddl_house_color.Items.Add("MintCream");
            ddl_house_color.Items.Add("MistyRose");
            ddl_house_color.Items.Add("Moccasin");
            ddl_house_color.Items.Add("Navy");
            ddl_house_color.Items.Add("OldLace");
            ddl_house_color.Items.Add("Olive");
            ddl_house_color.Items.Add("OliveDrab");
            ddl_house_color.Items.Add("Orange");
            ddl_house_color.Items.Add("OrangeRed");
            ddl_house_color.Items.Add("Orchid");
            ddl_house_color.Items.Add("PaleGoldenrod");
            ddl_house_color.Items.Add("PaleGreen");
            ddl_house_color.Items.Add("PaleTurquoise");
            ddl_house_color.Items.Add("PaleVioletRed");
            ddl_house_color.Items.Add("PapayaWhip");
            ddl_house_color.Items.Add("PeachPuff");
            ddl_house_color.Items.Add("Peru");
            ddl_house_color.Items.Add("Pink");
            ddl_house_color.Items.Add("Plum");
            ddl_house_color.Items.Add("PowderBlue");
            ddl_house_color.Items.Add("Purple");
            ddl_house_color.Items.Add("Red");
            ddl_house_color.Items.Add("RosyBrown");
            ddl_house_color.Items.Add("RoyalBlue");
            ddl_house_color.Items.Add("SaddleBrown");
            ddl_house_color.Items.Add("Salmon");
            ddl_house_color.Items.Add("SandyBrown");
            ddl_house_color.Items.Add("SeaGreen");
            ddl_house_color.Items.Add("SeaShell");
            ddl_house_color.Items.Add("Sienna");
            ddl_house_color.Items.Add("Silver");
            ddl_house_color.Items.Add("SkyBlue");
            ddl_house_color.Items.Add("SlateBlue");
            ddl_house_color.Items.Add("SlateGray");
            ddl_house_color.Items.Add("Snow");
            ddl_house_color.Items.Add("SpringGreen");
            ddl_house_color.Items.Add("SteelBlue");
            ddl_house_color.Items.Add("Tan");
            ddl_house_color.Items.Add("Teal");
            ddl_house_color.Items.Add("Thistle");
            ddl_house_color.Items.Add("Tomato");
            ddl_house_color.Items.Add("Transparent");
            ddl_house_color.Items.Add("Turquoise");
            ddl_house_color.Items.Add("Violet");
            ddl_house_color.Items.Add("Wheat");
            ddl_house_color.Items.Add("White");
            ddl_house_color.Items.Add("WhiteSmoke");
            ddl_house_color.Items.Add("Yellow");
            ddl_house_color.Items.Add("YellowGreen");
            ddl_house_color.SelectedIndex = 0;
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
                lbl_success.Text = msg;
                success.Visible = false;
                warning.Visible = true;

            }
        }

        String id, house_id;
        My mycode = new My();
        private void bind_grd_view()
        {
            DataTable dt = mycode.FillData("select *,(select top 1 Color_code from Color_master where Color_id=house_master.Color_id) as Color_code  from house_master order by house_name asc");
            if (dt.Rows.Count == 0)
            {
                Alertme("Sorry there are no branch list exist", "warning");
                rd_view.DataSource = null;
                rd_view.DataBind();

            }
            else
            {

                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }



        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            txt_house_name.Text = "";
            btn_cancel.Visible = false;
            btn_Submit.Text = "Add";
        }

        protected void btn_Submit_Click1(object sender, EventArgs e)
        {
            if (txt_house_name.Text == "")
            {
                Alertme("Please Enter House Name", "warning");
                txt_house_name.Focus();
                return;
            }


            if (btn_Submit.Text == "Add")
            {
                if (ViewState["Is_add"].ToString() == "1")
                {
                    submit_details();
                    My.send_data_to_user_log_history(ViewState["Userid"].ToString(), ViewState["Userid"].ToString() + " create a house master ");
                    empty_form();
                    bind_grd_view();
                }
                else
                {
                    Alertme("SORRY! You have not permission for this work.", "warning");
                }
            }
            else
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    update_update_details();
                    empty_form();
                    bind_grd_view();
                }
                else
                {
                    Alertme("SORRY! You have not permission for this work.", "warning");

                }
            }
        }

        private void update_update_details()
        {
            SqlConnection conn = new SqlConnection(My.conn);
            SqlDataAdapter ad = new SqlDataAdapter("select * from house_master where Id='" + hd_id.Value + "'", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                dr[1] = txt_house_name.Text;
                dr[3] = ddl_house_color.SelectedItem.Text;
                dr["Color_id"] = ddl_house_color.SelectedValue;
            }
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
            Alertme("House Master  Updated Successfully", "success");
        }

        string house_id_sl;
        private void submit_details()
        {
            create_sl_no();
            SqlConnection conn = new SqlConnection(My.conn);
            SqlDataAdapter ad = new SqlDataAdapter("select * from house_master", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            DataTable dt = ds.Tables[0];
            DataRow dr = dt.NewRow();
            dr[1] = txt_house_name.Text;
            dr[2] = house_id_sl;
            dr[3] = ddl_house_color.SelectedItem.Text;
            dr["Color_id"] = ddl_house_color.SelectedValue;
            dt.Rows.Add(dr);
            SqlCommandBuilder cb = new SqlCommandBuilder(ad);
            ad.Update(dt);
            Alertme("House Master Created Successfully", "success");
        }

        private void create_sl_no()
        {
            bool duplicate = true;
            house_id_sl = My.auto_serialS("house_id");
            while (duplicate)
            {
                DataTable cdt = My.dataTable(" select house_id from dbo.[house_master] where house_id='" + house_id_sl + "'");
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    duplicate = false;
                }
                else
                {
                    house_id_sl = My.auto_serialS("house_id");
                }
            }
        }

        private void empty_form()
        {
            txt_house_name.Text = "";
            btn_cancel.Visible = false;
            btn_Submit.Text = "Add";
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["Is_Edit"].ToString() == "1")
                {
                    LinkButton lnk = (LinkButton)sender;
                    RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                    Label lbl_house_name = (Label)row.FindControl("lbl_house_name");
                    Label lbl_color = (Label)row.FindControl("lbl_color");
                    Label lbl_Id = (Label)row.FindControl("lbl_Id");
                    Label lbl_color_id = (Label)row.FindControl("lbl_color_id");
                    hd_id.Value = lbl_Id.Text;
                    txt_house_name.Text = lbl_house_name.Text;
                    ddl_house_color.SelectedValue = lbl_color_id.Text;

                    btn_cancel.Visible = true;
                    btn_Submit.Text = "Update";
                }
                else
                {
                    Alertme("SORRY! You have not permission for this work.", "warning");
                }
            }
            catch
            {
            }
        }

        protected void lnkDel_Click(object sender, EventArgs e)
        {
            if (ViewState["Is_delete"].ToString() == "1")
            {
                SqlCommand cmd;
                LinkButton lnk = (LinkButton)sender;
                RepeaterItem row = (RepeaterItem)lnk.NamingContainer;
                Label lbl_Id = (Label)row.FindControl("lbl_Id");
                string query = "delete from  house_master where Id=@Id";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Id", lbl_Id.Text);
                if (My.InsertUpdateData(cmd))
                {
                    Alertme("House Master deleted Successfully.", "success");
                    bind_grd_view();
                }
            }
            else
            {
                Alertme("SORRY! You have not permission for this work.", "warning");

            }
        }

        

        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbl_color_code = ((Label)(e.Item.FindControl("lbl_color_code")));
                ((Label)(e.Item.FindControl("lbl_color_class"))).Style.Add("background", lbl_color_code.Text);
            }
        }



    }
}