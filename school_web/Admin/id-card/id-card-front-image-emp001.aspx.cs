using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin.id_card
{
    public partial class id_card_front_image_emp001 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Request.QueryString["Type"] != null)
                    {
                        if (Request.QueryString["Type"].ToString() == "CHECK")
                        {
                            hd_userType.Value = Request.QueryString["UserType"];
                            hd_branch.Value = Request.QueryString["Branch_id"];
                            hd_type.Value = Request.QueryString["Type"];
                            string empid = Request.QueryString["empid"].ToString();
                            hd_emp_id.Value = empid.Remove(empid.Length - 1);
                        }
                        else
                        {
                            if (Request.QueryString["UserType"] != null || Request.QueryString["empid"] != null || Request.QueryString["Branch_id"] != null || Request.QueryString["Type"] != null)
                            {

                                hd_userType.Value = Request.QueryString["UserType"];
                                hd_emp_id.Value = Request.QueryString["empid"];
                                hd_branch.Value = Request.QueryString["Branch_id"];
                                hd_type.Value = Request.QueryString["Type"];
                            }
                            else
                            {
                                Response.Redirect("../Id-card-employee-print.aspx", false);
                            }
                        }
                    }
                    else
                    {
                        Response.Redirect("../Id-card-employee-print.aspx", false);
                    }
                }
                catch (Exception exe)
                {
                }
            }
        }

        protected void ExportToImage(object sender, EventArgs e)
        {
            string base64 = Request.Form[hfImageData.UniqueID].Split(',')[1];
            byte[] bytes = Convert.FromBase64String(base64);
            Response.Clear();
            Response.ContentType = "image/png";
            Response.AddHeader("Content-Disposition", "attachment; filename=HTML.png");
            Response.Buffer = true;
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(bytes);
            MemoryStream storeStream = new MemoryStream();
            MemoryStream ms = new MemoryStream(bytes);
            //write to file  
            FileStream file = new FileStream(Server.MapPath("~/Images/") + "HTML100.png", FileMode.Create,
            FileAccess.Write);
            ms.WriteTo(file);
            file.Close();
            ms.Close();
            Response.End();
        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Id-card-employee-print.aspx", false);
        }
    }
}