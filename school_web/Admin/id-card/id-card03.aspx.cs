using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin.id_card
{
    public partial class id_card03 : System.Web.UI.Page
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

                        fetch_id_cards();
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

        private void fetch_id_cards()
        {
            string query = "";
            if (hd_type.Value == "CHECK")
            {
                query = "select *,(select top 1 Template_filepath from Id_card_template_setting where Branch_id='" + hd_branch.Value + "' and Type='Student') as idcard_template,CASE WHEN transportationtaken = 'Yes' THEN 'BUS' WHEN transportationtaken = 'No' THEN 'WALK-IN' END AS TransportStatus from admission_registor where Session_Id='" + hd_session_id.Value + "' and Class_id='" + hd_class_id.Value + "' and Branch_id='" + hd_branch.Value + "' and Status='1' and Id in(" + hd_admission_no.Value + ")";
            }
            else
            {
                if (hd_type.Value == "BULK")
                {
                    if (hd_section.Value == "ALL")
                    {
                        query = "select *,(select top 1 Template_filepath from Id_card_template_setting where Branch_id='" + hd_branch.Value + "' and Type='Student') as idcard_template,CASE WHEN transportationtaken = 'Yes' THEN 'BUS' WHEN transportationtaken = 'No' THEN 'WALK-IN' END AS TransportStatus from admission_registor where Session_Id='" + hd_session_id.Value + "' and Class_id='" + hd_class_id.Value + "' and Branch_id='" + hd_branch.Value + "' and Status='1'";
                    }
                    else
                    {
                        query = "select *,(select top 1 Template_filepath from Id_card_template_setting where Branch_id='" + hd_branch.Value + "' and Type='Student') as idcard_template,CASE WHEN transportationtaken = 'Yes' THEN 'BUS' WHEN transportationtaken = 'No' THEN 'WALK-IN' END AS TransportStatus from admission_registor where Session_Id='" + hd_session_id.Value + "' and Class_id='" + hd_class_id.Value + "' and Branch_id='" + hd_branch.Value + "' and Section='" + hd_section.Value + "' and Status='1'";
                    }
                }
                else
                {
                    query = "select *,(select top 1 Template_filepath from Id_card_template_setting where Branch_id='" + hd_branch.Value + "' and Type='Student') as idcard_template,CASE WHEN transportationtaken = 'Yes' THEN 'BUS' WHEN transportationtaken = 'No' THEN 'WALK-IN' END AS TransportStatus from admission_registor where admissionserialnumber='" + hd_admission_no.Value + "' and Session_Id='" + hd_session_id.Value + "' and Class_id='" + hd_class_id.Value + "' and Branch_id='" + hd_branch.Value + "' and Status='1'";
                }
            }
            DataTable dtS = My.dataTable(query);
            if (dtS.Rows.Count > 0)
            {
                rd_view.DataSource = dtS;
                rd_view.DataBind();
            }
            else
            {
                rd_view.DataSource = null;
                rd_view.DataBind();
            }
        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Id-card-print.aspx", false);
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
    }
}