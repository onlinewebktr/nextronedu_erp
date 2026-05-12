using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin.slip
{
    public partial class reprint_payment_voucher_all : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Admin"] == null)
            {
                Session.Abandon();
                Session.Clear();
                Response.Write("<script language=javascript>var wnd=window.open('','newWin','height=1,width=1,left=900,top=700,status=no,toolbar=no,menubar=no,scrollbars=no,maximize=false,resizable=1');</script>");
                Response.Write("<script language=javascript>wnd.close();</script>");
                Response.Write("<script language=javascript>window.open('../../Default.aspx','_parent',replace=true);</script>");
            }
            else
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["Session"] != null || Request.QueryString["Class"] != null || Request.QueryString["Section"] != null)
                    {

                        ViewState["sessionid"] = Request.QueryString["Session"];
                        ViewState["classid"] = Request.QueryString["Class"];
                        ViewState["Section"] = Request.QueryString["Section"];

                        ViewState["session"] = mycode.get_session(ViewState["sessionid"].ToString());
                        bind_vouchers();
                    }
                    else
                    {
                        Response.Redirect("../home.aspx", false);
                    }


                }
            }
        }

        private void bind_vouchers()
        {
            string qrys = "";
            if (ViewState["Section"].ToString() == "0")
            {
                qrys = "select t1.*,t2.class,t2.rollnumber,t2.session,t2.studentname,t2.father_mob,t2.fathername,t2.Old_Admission_Date,t2.dateofadmission,t2.mobilenumber,t3.Affiliation,t3.school_no,t3.logo,t3.address1,t3.email,t3.website,t3.contact_no,t3.firm_name,t3.Is_slip_header,t3.Header_images from Payment_voucher_slip t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id and t1.Section=t2.Section and t1.Admission_no=t2.admissionserialnumber  join Firm_Details t3 on t1.Firm_id=t3.firm_id where t1.Session_id='" + ViewState["sessionid"].ToString() + "' and t1.Class_id='" + ViewState["classid"].ToString() + "'";
            }
            else
            {
                qrys = "select t1.*,t2.class,t2.rollnumber,t2.session,t2.studentname,t2.father_mob,t2.fathername,t2.Old_Admission_Date,t2.dateofadmission,t2.mobilenumber,t3.Affiliation,t3.school_no,t3.logo,t3.address1,t3.email,t3.website,t3.contact_no,t3.firm_name,t3.Is_slip_header,t3.Header_images from Payment_voucher_slip t1 join admission_registor t2 on t1.Session_id=t2.Session_id and t1.Class_id=t2.Class_id and t1.Section=t2.Section and t1.Admission_no=t2.admissionserialnumber  join Firm_Details t3 on t1.Firm_id=t3.firm_id where t1.Session_id='" + ViewState["sessionid"].ToString() + "' and t1.Class_id='" + ViewState["classid"].ToString() + "' and t1.Section='" + ViewState["Section"].ToString() + "'";
            }
            DataTable dt = mycode.FillData(qrys);
            if (dt.Rows.Count == 0)
            {
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = dt;
                rd_view.DataBind();
            }
        }




        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("../reprint-payment-voucher.aspx", false);
        }


        protected void btn_print_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "printit", "printit()", true);
        }

        protected void rd_view_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rd_viewsss = ((Repeater)e.Item.FindControl("rd_viewsss")) as Repeater;
                Repeater rd_notes = ((Repeater)e.Item.FindControl("rd_notes")) as Repeater;
                Label lbl_rd_viewsss = ((Label)e.Item.FindControl("lbl_rd_viewsss")) as Label;
                Label lbl_rd_notes = ((Label)e.Item.FindControl("lbl_rd_notes")) as Label;

                Label lbl_Header_images = ((Label)e.Item.FindControl("lbl_Header_images")) as Label;
                Label lbl_Is_slip_header = ((Label)e.Item.FindControl("lbl_Is_slip_header")) as Label;

                Panel textheader = ((Panel)e.Item.FindControl("textheader")) as Panel;
                Panel printheader = ((Panel)e.Item.FindControl("printheader")) as Panel;


                Panel textheader1 = ((Panel)e.Item.FindControl("textheader1")) as Panel;
                Panel printheader1 = ((Panel)e.Item.FindControl("printheader1")) as Panel;


                Image img_header = ((Image)e.Item.FindControl("img_header")) as Image;
                Image img_header1 = ((Image)e.Item.FindControl("img_header1")) as Image;

                DataTable dt = mycode.FillData("select * from Slip_note where Type_id='1'");
                if (dt.Rows.Count == 0)
                {
                    rd_viewsss.DataSource = null;
                    rd_viewsss.DataBind();

                    rd_notes.DataSource = null;
                    rd_notes.DataBind();
                    lbl_rd_viewsss.Visible = false;
                    lbl_rd_notes.Visible = false;

                }
                else
                {
                    rd_viewsss.DataSource = dt;
                    rd_viewsss.DataBind();

                    rd_notes.DataSource = dt;
                    rd_notes.DataBind();
                    lbl_rd_viewsss.Visible = true;
                    lbl_rd_notes.Visible = true;
                }

                try
                {
                    if (lbl_Is_slip_header.Text == "True")
                    {
                        textheader.Visible = textheader1.Visible = false;
                       
                        printheader.Visible = printheader1.Visible = true;
                        img_header.ImageUrl = img_header1.ImageUrl = lbl_Header_images.Text;
                    }
                    else
                    {
                        textheader.Visible = textheader1.Visible = true;
                        printheader.Visible = printheader1.Visible = false;
                    }
                }
                catch
                {
                    textheader.Visible = textheader1.Visible = true;
                    printheader.Visible = printheader1.Visible = false;
                }



            }
        }
    }
}