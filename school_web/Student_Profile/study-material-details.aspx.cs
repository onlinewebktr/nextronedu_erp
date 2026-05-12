using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Student_Profile
{
    public partial class study_material_details : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["topicid"] != null)
                {
                    ViewState["topic_id"] = Request.QueryString["topicid"];
                    fetch_topic_details(ViewState["topic_id"].ToString());
                    fetch_topic_graphic(ViewState["topic_id"].ToString());
                    fetch_topic_pdfs(ViewState["topic_id"].ToString());
                }
                else
                {
                    Response.Redirect("home.aspx", false);
                }
            }
        }

        private void fetch_topic_pdfs(string topic_id)
        {
            DataTable dt = mycode.FillData("select Images from Topic_Uploaded_images where Topic_Id='" + topic_id + "' and Type='Attachment File'");
            if (dt.Rows.Count == 0)
            {
                pdfsDV.Visible = false;
                rp_pdfs.DataSource = null;
                rp_pdfs.DataBind();
            }
            else
            {
                pdfsDV.Visible = true;
                rp_pdfs.DataSource = dt;
                rp_pdfs.DataBind();
            }
        }

        private void fetch_topic_graphic(string topic_id)
        {
            DataTable dt = mycode.FillData("select Images from Topic_Uploaded_images where Topic_Id='" + topic_id + "' and Type='Graphic'");
            if (dt.Rows.Count == 0)
            {
                graphicS.Visible = false;
                rd_graphics.DataSource = null;
                rd_graphics.DataBind();
            }
            else
            {
                graphicS.Visible = true;
                rd_graphics.DataSource = dt;
                rd_graphics.DataBind();
            }
        }


        private void fetch_topic_details(string topic_id)
        {
            DataTable dt = mycode.FillData("select *,(select top 1 Subject_name from Subject_Master where Subject_id=TopicMaster.CourseID) as Subject_name from TopicMaster where TopicID='" + topic_id + "'");
            if (dt.Rows.Count == 0)
            {
                Response.Redirect("home.aspx", false);
            }
            else
            {
                topic_p.InnerText = dt.Rows[0]["TopicName"].ToString();
                topic_desc.InnerHtml = dt.Rows[0]["Details"].ToString();
                SubjectName.InnerText = dt.Rows[0]["Subject_name"].ToString();
                if (dt.Rows[0]["VideoLink"].ToString() == "" || dt.Rows[0]["VideoLink"].ToString() == null)
                {
                    videoDV.Visible = false;
                }
                else
                {
                    videoDV.Visible = true;
                    videoS.Src = dt.Rows[0]["VideoLink"].ToString();
                }
            }
        }
    }
}