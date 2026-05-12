using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin.id_card
{
    public partial class id_card_kes_image : System.Web.UI.Page
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
                            hd_admission_no.Value = Request.QueryString["admNo"];
                            hd_session_id.Value = Request.QueryString["ssion_id"];
                            hd_class_id.Value = Request.QueryString["clss_id"];
                            hd_branch.Value = Request.QueryString["Branch_id"];
                            hd_section.Value = "0";
                            hd_type.Value = Request.QueryString["Type"];

                            string admnomn = Request.QueryString["admNo"].ToString();
                            hd_admission_no.Value = admnomn.Remove(admnomn.Length - 1);
                        }
                        else
                        {
                            if (Request.QueryString["admNo"] != null || Request.QueryString["ssion_id"] != null || Request.QueryString["clss_id"] != null || Request.QueryString["Branch_id"] != null || Request.QueryString["Section"] != null || Request.QueryString["Type"] != null)
                            {

                                hd_admission_no.Value = Request.QueryString["admNo"];
                                hd_session_id.Value = Request.QueryString["ssion_id"];
                                hd_class_id.Value = Request.QueryString["clss_id"];
                                hd_branch.Value = Request.QueryString["Branch_id"];
                                hd_section.Value = Request.QueryString["Section"];
                                hd_type.Value = Request.QueryString["Type"];
                            }
                            else
                            {
                                Response.Redirect("../Id-card-print.aspx", false);
                            }
                        }
                    }
                    else
                    {
                        Response.Redirect("../Id-card-print.aspx", false);
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
            Response.Redirect("../Id-card-print.aspx", false);
        }
    }
}