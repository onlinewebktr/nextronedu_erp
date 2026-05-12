
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

namespace school_web.Student_Profile
{
    public partial class Topic_Details : System.Web.UI.Page
    {
        UsesCode code = new UsesCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["TopicId"] != null && Request.QueryString["SectionID"] != null)
                    {
                        hdUserId.Value = Request.QueryString["UserID"].ToString();
                        hd_TopicId.Value = Request.QueryString["TopicId"].ToString();
                        hd_SectionID.Value = Request.QueryString["SectionID"].ToString();
                        hd_CourseId.Value = code.Find_Name("select CourseID from TopicMaster where Istatus=1 and TopicID='" + hd_TopicId.Value + "'");
                        LtCourseName.Text = code.Find_Name("select TopicName from TopicMaster where Istatus=1 and TopicID='" + hd_TopicId.Value + "'");
                        code.BindRepeater("select ROW_NUMBER() OVER(ORDER BY SectionID) as slno , * from TopicMaster  where Istatus=1 and TopicID='" + hd_TopicId.Value + "'", Rp_TopicDetails);
                        string queryBindAllYopics = "select ROW_NUMBER() OVER(ORDER BY SectionID) as slno , TopicName, TopicID, SectionID from TopicMaster where SectionID = '" + hd_SectionID.Value + "' ORDER BY Id ASC";
                        DataTable dtc = code.FillTable(queryBindAllYopics);
                        if (dtc.Rows.Count > 0)
                        {
                            string topicides = "";
                            int i = 0;
                            foreach (DataRow row in dtc.Rows)
                            {

                                if (topicides.Length == 0)
                                {
                                    topicides = dtc.Rows[i]["TopicID"].ToString();
                                }
                                else
                                {
                                    topicides = topicides + "," + dtc.Rows[i]["TopicID"].ToString();
                                }
                                i = i + 1;

                            }

                            AllTopicIDs.Value = topicides;
                            //RpTopicLesson.DataSource = dtc;
                            //RpTopicLesson.DataBind();
                            //  UpdateTraking(hd_TopicId.Value);
                            // Disablelink();
                        }
                        else
                        {
                            //RpTopicLesson.DataSource = null;
                            //RpTopicLesson.DataBind();
                        }
                        //code.BindRepeater("select ROW_NUMBER() OVER(ORDER BY SectionID) as slno , TopicName, TopicID, SectionID from TopicMaster where SectionID = '" + hd_SectionID.Value + "' ORDER BY Id ASC", RpTopicLesson);

                    }
                }
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        private void UpdateTraking(string topicid)
        {
            try
            {
                if (code.IsExist("Select * from TrackingMaster where TopicID='" + topicid + "' and UserID='" + hdUserId.Value + "'"))
                {
                    SqlCommand cmd;
                    string query = "Select * from TrackingMaster where CourseID='" + hd_CourseId.Value + "' and UserID='" + hdUserId.Value + "'";
                    DataTable dtc = code.FillTable(query);
                    string SectionId = dtc.Rows[0]["SectionID"].ToString();
                    foreach (DataRow row in dtc.Rows)
                    {
                        if (SectionId == "")
                        {
                            cmd = new SqlCommand("Update TrackingMaster set  SectionID='" + hd_SectionID.Value + "'  where UserID='" + hdUserId.Value + "' and CourseID='" + hd_CourseId.Value + "'");
                            InsertUpdate.InsertUpdateData(cmd);
                        }
                        else
                        {
                            string Firsttopic = dtc.Rows[0]["TopicID"].ToString();
                            if (Firsttopic == "")
                            {
                                cmd = new SqlCommand("Update TrackingMaster set ReadOut=1, TopicID='" + hd_TopicId.Value + "'  where UserID='" + hdUserId.Value + "' and CourseID='" + hd_CourseId.Value + "' and  SectionID='" + hd_SectionID.Value + "'");
                                InsertUpdate.InsertUpdateData(cmd);
                            }
                            else
                            {
                                string catId = code.Find_Name("select CategoryID from Course_or_Subject_Master where CourseID='" + hd_CourseId.Value + "'");
                                cmd = new SqlCommand("insert into TrackingMaster (UserID, CategoryID, CourseID, SectionID,TopicID, Date, Idate, Time, Status,ReadOut)" +
                                   "Values ('" + hdUserId.Value + "','" + catId + "','" + hd_CourseId.Value + "','" + hd_SectionID.Value + "', '" + topicid + "','" + code.date() + "','" + code.idate() + "','" + code.time() + "','Enrolled','1')");
                                InsertUpdate.InsertUpdateData(cmd); return;
                            }
                        }
                    }

                }
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }

        }


        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            string Path = "Course_Details.aspx?CourseId=" + hd_CourseId.Value + "&UserID=" + hdUserId.Value;
            Response.Redirect(Path);
        }

        protected void Rp_TopicDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    Label lbl_VideoLink = (Label)e.Item.FindControl("lbl_VideoLink");
                    HiddenField Video = (HiddenField)e.Item.FindControl("hd_Video");
                    HtmlGenericControl topVideo = e.Item.FindControl("topVideo") as HtmlGenericControl;
                    HtmlGenericControl bottomvideo = e.Item.FindControl("bottomvideo") as HtmlGenericControl;
                    if (Video.Value == "Top")
                    {
                        if (lbl_VideoLink.Text == "")
                        {
                            topVideo.Visible = false;
                            bottomvideo.Visible = false;

                        }
                        else
                        {
                            topVideo.Visible = true;
                            bottomvideo.Visible = false;
                        }

                    }
                    else
                    {
                        if (lbl_VideoLink.Text == "")
                        {
                            topVideo.Visible = false;
                            bottomvideo.Visible = false;
                        }
                        else
                        {
                            topVideo.Visible = false;
                            bottomvideo.Visible = true;
                        }
                    }

                    HiddenField hd_AudioFile = (HiddenField)e.Item.FindControl("hd_AudioFile");
                    Panel Panel1 = (Panel)e.Item.FindControl("Panel1");

                    if (hd_AudioFile.Value == "")
                    {
                        Panel1.Visible = false;
                    }
                    else
                    {
                        Panel1.Visible = true;
                    }


                    HiddenField hd_SectionID = (HiddenField)e.Item.FindControl("hd_SectionID");
                    HiddenField hd_CategoryID = (HiddenField)e.Item.FindControl("hd_CategoryID");
                    HiddenField hd_subjectid = (HiddenField)e.Item.FindControl("hd_subjectid");
                    HiddenField hd_TopicID = (HiddenField)e.Item.FindControl("hd_TopicID");
                    GridView grd_doclist = (GridView)e.Item.FindControl("grd_doclist");




                    Bind_doc_list(hd_SectionID.Value, hd_CategoryID.Value, hd_subjectid.Value, hd_TopicID.Value, grd_doclist);
                }
            }
            catch (Exception ex) { UsesCode.submitexception(ex.ToString()); }
        }

        private void Bind_doc_list(string SectionID, string CategoryID, string subjectid, string TopicID, GridView grd_doclist)
        {
            DataTable dt = code.FillTable("Select Images,'http://docs.google.com/gview?url='+Images+'&amp;embedded=true' as file1 from Topic_Uploaded_images where Topic_Id='" + TopicID + "' and SectionID='" + SectionID + "' and Course_Id='" + subjectid + "' and Class_Id='" + CategoryID + "'");
            if (dt.Rows.Count == 0)
            {
                grd_doclist.DataSource = null;
                grd_doclist.EmptyDataText = "There is no doc list available";
                grd_doclist.DataBind();
            }
            else
            {
                grd_doclist.DataSource = dt;

                grd_doclist.DataBind();
            }
        }

        protected void btn_Next_Click(object sender, EventArgs e)
        {
            string currenttopic = hd_TopicId.Value;
            string[] strTopicids = AllTopicIDs.Value.Split(',');
            string nexttopicid = "";
            int i = 0;
            foreach (string curtopic in strTopicids)
            {

                if (curtopic == currenttopic)
                {
                    if (strTopicids.Length - 1 > i)
                    {
                        nexttopicid = strTopicids[i + 1];
                        break;
                    }
                }
                i = i + 1;
            }
            if (!string.IsNullOrEmpty(nexttopicid))
            {
                code.BindRepeater("select ROW_NUMBER() OVER(ORDER BY SectionID) as slno , * from TopicMaster  where Istatus=1 and TopicID='" + nexttopicid + "'", Rp_TopicDetails);
                UpdateTraking(nexttopicid);
            }

        }


    }
}