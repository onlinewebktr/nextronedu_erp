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
    public partial class CourseDetails : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["CourseId"] != null)
                    {
                        hd_CourseId.Value = Request.QueryString["CourseId"].ToString();
                        hdUserId.Value = Session["User"].ToString();
                        hd_sectionid.Value = Request.QueryString["sectionid"].ToString();
                        LtCourseName.Text = code.Find_Name("select Subject_name from Subject_Master where   course_id='" + hd_CourseId.Value + "' ");
                         
                        code.BindRepeater(" select LessonNo,SetionName,SectionID from SectionMaster where Istatus=1 and CourseID='" + hd_CourseId.Value + "' and Section_Subject='" + hd_sectionid.Value + "'", RpLesson);
                    }
                    else { Response.Redirect("CourseView.aspx"); return; }
                }
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        protected void RpLesson_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    Repeater childRepeater = (Repeater)e.Item.FindControl("RpSection");
                    HiddenField SectionID = (HiddenField)e.Item.FindControl("hdSectionID");
                    int ISectionID = Int32.Parse(SectionID.Value);
                    code.BindRepeater("select ROW_NUMBER() OVER(ORDER BY TM.SectionID) as slno , TM.TopicName, TM.TopicID, TM.SectionID, ISNUll((select top 1 TopicID from TrackingMaster where TopicID=TM.TopicID and UserID='" + hdUserId.Value + "'),0) ReadTopic from TopicMaster TM  where TM.SectionID = '" + ISectionID + "' ORDER BY TM.Id ASC", childRepeater);
                    TopicReadorNot(childRepeater);
                }
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        private void TopicReadorNot(Repeater childRepeater)
        {
            try
            {
                foreach (RepeaterItem item in childRepeater.Items)
                {
                    Image tick = (Image)item.FindControl("ImageTick");
                    HiddenField hdTopicID = (HiddenField)item.FindControl("hdTopicID");
                    if (hdTopicID.Value == "0") { tick.Visible = false; }
                    else { tick.Visible = true; }
                }
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }
    }
}