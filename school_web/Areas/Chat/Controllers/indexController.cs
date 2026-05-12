using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using static school_web.Areas.Chat.MyModel;

namespace school_web.Areas.Chat.Controllers
{
    [RouteArea("Chat")]

    public class indexController : Controller
    {
        // GET: Chat/index

        [Route("logout")]
        //[AllowAnonymous]
        //public ActionResult Logout()
        //{
        //    if (User.Identity.IsAuthenticated)
        //        FormsAuthentication.SignOut();
        //    Session.Abandon();
        //    Session.Clear();
        //    Session.RemoveAll();
        //    return Redirect("~/Default.aspx");
        //}
        [Route("home")]
        public ActionResult Index()
        {
            //if (User.Identity.IsAuthenticated)
            //{
            My mycode = new My();
            // var regid = User.Identity.Name;
            var regid = Request["regid"];
            if (regid == "")
            {
            }
            else
            {

                var dt1 = My.data($" Select  top 1 Id  from Internal_chat_Notification where  Receiver_id = '{regid}' and Is_read = 0 order by Id desc").ToInt();

                ViewBag.lastid = dt1;


                var dt = My.dataTable($"select *   from user_details where user_id='{regid}'");
                ViewBag.User_Type = dt.Rows[0]["User_Type"].ToString();

                if (dt.Rows[0]["ProfilePhoto"].ToString() == "")
                {
                    ViewBag.ProfilePhoto = "../images/blank.png";
                }
                else
                {
                    ViewBag.ProfilePhoto = dt.Rows[0]["ProfilePhoto"].ToString();
                }
                ViewBag.user_name = dt.Rows[0]["name"].ToString();


                DataTable dt_all_user = My.dataTable($" select name as username,user_id,gcm_id,CASE WHEN ProfilePhoto != '' THEN ProfilePhoto WHEN ProfilePhoto = '' THEN '../images/blank.png'  END AS profile_photo,  (select top 1 Content from Internal_chat_history where Sender_id = user_details.user_id and Chat_type = 'User' and Receiver_id='{regid}' order by id desc ) as last_message,'User' as typeuser,(select top 1 format(Time_stamp, 'dd/MM/yyyy hh:mm tt') from Internal_chat_history where Sender_id = user_details.user_id and Chat_type = 'User' and Receiver_id='{regid}' order by id desc ) as last_replytime,(select top 1 format(Time_stamp, 'yyyyMMdd') from Internal_chat_history where Sender_id = user_details.user_id and Chat_type = 'User' and Receiver_id='{regid}' order by id desc ) as last_idate,(select count(*) from dbo.[Internal_chat_Notification] icn join Internal_chat_history ich on icn.Message_id = ich.Message_id where ich.Sender_id =user_details.user_id  and icn.Receiver_id = '{regid}' and Is_read = 0 and   ich.Chat_type='User' and ich.Sender_id!= '{regid}') as messagecount,'' as addcss,'' as  Is_Predefined_Group,(select top 1 format(icn.Time_stamp, 'yyyyMMddhhmmss') from dbo.[Internal_chat_Notification] icn join Internal_chat_history ich on icn.Message_id = ich.Message_id where ich.Sender_id =user_details.user_id  and icn.Receiver_id = '{regid}'  and   ich.Chat_type='User' and ich.Sender_id!= '{regid}' order by icn.Time_stamp desc) as lasttattime from user_details where Istatus = 1 and User_Type!= 'Visitor' and user_id!= '{regid}' order by  lasttattime desc, username     ");

                if (dt_all_user.Rows.Count == 0)
                {
                    ViewBag.dt_all_user = null;
                }
                else
                {
                    foreach (DataRow row in dt_all_user.Rows)
                    {
                        if (row["last_idate"].ToString() == mycode.idate())
                        {
                            row["last_replytime"] = row["last_replytime"];

                        }
                        else if (row["last_idate"].ToString() == mycode.onedayago())
                        {
                            row["last_replytime"] = "Yesterday";

                        }
                        else if (row["last_idate"].ToString() == mycode.twodayago())
                        {
                            row["last_replytime"] = "2 days ago";

                        }
                        else if (row["last_idate"].ToString() == mycode.threedayago())
                        {
                            row["last_replytime"] = "3 days ago";

                        }
                        else
                        {
                            row["last_replytime"] = row["last_replytime"].ToString();

                        }

                        if (row["Is_Predefined_Group"].ToString() == "1")
                        {
                            row["addcss"] = "group_name_hide";
                        }
                        else
                        {
                            row["addcss"] = "";
                        }
                    }

                    ViewBag.dt_all_user = dt_all_user;

                }


                var dt2 = My.dataTable($"select *,CASE WHEN ProfilePhoto != '' THEN ProfilePhoto WHEN ProfilePhoto = '' THEN '../images/blank.png'  END AS profile_photo from user_details where  Istatus = 1 and User_Type!= 'Visitor' and user_id!= '{regid}' order by name");
                if (dt2.Rows.Count == 0)
                {
                    ViewBag.dt_alluserbyname = null;
                }
                else
                {
                    ViewBag.dt_alluserbyname = dt2;
                }

            }
            return View("chat_index");
            //}
            //else
            //{
            //    return Redirect("~/Default.aspx");
            //}


        }
        [Route("data/{method_id}")]
        public ActionResult getdata(string method_id, Dictionary<string, object> data)
        {
            var resp = (new JavaScriptSerializer()).Serialize(new
            {
                message = "Some thing goes worng",
                error = true,
                dataId = method_id,
                data = data,
            });
            if (method_id == "get_user_name_profile")
            {
                var dt = My.dataTable($"select * from user_details where user_id='{data["regid"]}'");
                resp = (new JavaScriptSerializer()).Serialize(new
                {
                    message = "success",
                    error = false,
                    data = dt.toJsonObject(),
                });
                return Json(resp, JsonRequestBehavior.AllowGet);
            }
            if (method_id == "create_group")
            {
                var creatby = data["regid"].ToString();
                var groupname = data["group_name"].ToString();
                var filename = data["filename"].ToString();
                string[] userid = data["selecteduser"].ToString().Split(',');
                var action = data["action"].ToString();
                var groupid = data["groupid"].ToString();
                var lasttattime = My.lasttattime();




                if (action == "Add")
                {
                    DateTime dtm = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                    string date = dtm.ToString("yyyyMMddhhmmss");
                    Random random = new Random();
                    int tempo = random.Next(1, 9999);
                    groupid = tempo.ToString() + date;
                    bool check_groupname = get_group_name(groupname, groupid);
                    if (check_groupname == true)
                    {

                        var dtmsgcount = My.dataTable($"select count(*) from dbo.[Internal_chat_Notification] icn join Internal_chat_history ich on icn.Message_id = ich.Message_id where ich.Receiver_id ='{groupid}'   and icn.Receiver_id = '{creatby}' and Is_read = 0 and ich.Chat_type='Group'");


                        bool flag = false;
                        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
                        {
                            SqlConnection con = new SqlConnection(My.conn);
                            con.Open();
                            MyModel.Internal_chat_group_list(creatby, groupname, filename, groupid, con);
                            MyModel.Internal_Chat_group_user(groupid, creatby, con);
                            foreach (var userid_selected in userid)
                            {
                                MyModel.Internal_Chat_group_user(groupid, userid_selected, con);

                            }

                            string Remarks = "Group has been created by " + MyModel.get_user_name(creatby) + " Group name " + groupname;
                            payments.exeSql("insert into Internal_chat_log(User_id,Message,Time_stamp) values ('" + creatby + "',N'" + Remarks + "',N'" + My.getdate1() + "')", con);
                            flag = true;
                            con.Close();
                            scope.Complete();
                        }
                        if (flag == true)
                        {

                            resp = (new JavaScriptSerializer()).Serialize(new
                            {
                                message = "Group name is created",
                                error = false,
                                data = groupid,

                                messagecount = dtmsgcount.Rows[0][0].ToString(),
                                lasttattime = lasttattime.ToString(),
                            });
                        }
                        else
                        {
                            resp = (new JavaScriptSerializer()).Serialize(new
                            {
                                message = "Something is wrong",
                                error = true,
                                data = groupid,
                                messagecount = dtmsgcount.Rows[0][0].ToString(),
                                lasttattime = lasttattime.ToString(),
                            });
                        }

                    }
                    else
                    {
                        var dtmsgcount = My.dataTable($"select count(*) from dbo.[Internal_chat_Notification] icn join Internal_chat_history ich on icn.Message_id = ich.Message_id where ich.Receiver_id ='{groupid}'   and icn.Receiver_id = '{creatby}' and Is_read = 0 and ich.Chat_type='Group'");

                        resp = (new JavaScriptSerializer()).Serialize(new
                        {
                            message = "Group name is already exist",
                            error = true,
                            data = groupid,
                            messagecount = dtmsgcount.Rows[0][0].ToString(),
                            lasttattime = lasttattime.ToString(),
                        });
                    }

                }
                else
                {
                    var dtmsgcount = My.dataTable($"select count(*) from dbo.[Internal_chat_Notification] icn join Internal_chat_history ich on icn.Message_id = ich.Message_id where ich.Receiver_id ='{groupid}'   and icn.Receiver_id = '{creatby}' and Is_read = 0 and ich.Chat_type='Group'");
                    bool check_groupname = get_group_name(groupname, groupid);
                    if (check_groupname == true)
                    {

                        bool flag = false;
                        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
                        {
                            SqlConnection con = new SqlConnection(My.conn);
                            con.Open();
                            MyModel.Internal_chat_group_list_update(creatby, groupname, filename, groupid, con);

                            string Remarks = "Group name has been updated by " + MyModel.get_user_name(creatby) + " Group name " + groupname + " Group id:" + groupid;
                            payments.exeSql("insert into Internal_chat_log(User_id,Message,Time_stamp) values ('" + creatby + "',N'" + Remarks + "',N'" + My.getdate1() + "')", con);
                            flag = true;
                            con.Close();
                            scope.Complete();
                        }
                        if (flag == true)
                        {
                            resp = (new JavaScriptSerializer()).Serialize(new
                            {
                                message = "Group name is updated",
                                error = false,
                                data = groupid,
                                messagecount = dtmsgcount.Rows[0][0].ToString(),
                                lasttattime = lasttattime.ToString(),
                            });
                        }
                        else
                        {
                            resp = (new JavaScriptSerializer()).Serialize(new
                            {
                                message = "Something is wrong",
                                error = true,
                                data = groupid,
                                messagecount = dtmsgcount.Rows[0][0].ToString(),
                                lasttattime = lasttattime.ToString(),
                            });
                        }
                    }
                    else
                    {
                        resp = (new JavaScriptSerializer()).Serialize(new
                        {
                            message = "Group name is already exist",
                            error = true,
                            data = groupid,
                            messagecount = dtmsgcount.Rows[0][0].ToString(),
                            lasttattime = lasttattime.ToString(),
                        });
                    }
                }
                return Json(resp, JsonRequestBehavior.AllowGet);
            }
            if (method_id == "UploadFile")
            {
                string idate = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("yyyyMMdd");
                string time = DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("hhmmss");
                string value = Guid.NewGuid().ToString();
                string url = My.url();

                var file = Request.Files["file"];
                string filePath = "";
                string filerename = "";
                if (file != null && file.ContentLength > 0)
                {
                    string uploadsFolder = Server.MapPath("/Master_Img/Chat_file");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    string extension = new FileInfo(file.FileName).Extension;
                    filerename = idate + time + extension;
                    filePath = (Server.MapPath("~/Master_Img/Chat_file")).ToString();

                    file.SaveAs(filePath + "/" + filerename);

                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                return Json(js.Serialize(new { Path = url + "/Master_Img/Chat_file/" + filerename }), JsonRequestBehavior.AllowGet);

            }
            if (method_id == "fetch_data_for_update_group_name")
            {
                var regid = data["regid"].ToString();
                var groupid = data["groupid"].ToString();
                var dt2 = My.dataTable($"select * from Internal_chat_group_list where  Group_Id= '{groupid}' ");
                var dt3 = My.dataTable($"select * from Internal_Chat_group_users_list where  Group_Id= '{groupid}'");
                if (dt2.Rows.Count == 0)
                {
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "Something is wrong",
                        error = false,
                        data = "",
                    });
                }
                else
                {
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "success",
                        error = false,
                        groupname = dt2.Rows[0]["Group_name"],
                        Group_ProfilePhoto = dt2.Rows[0]["Group_ProfilePhoto"],
                        details = dt3.toJsonObject(),
                    });
                }

                return Json(resp, JsonRequestBehavior.AllowGet);

            }
            if (method_id == "fetch_chat_data")
            {
                var regid = data["regid"].ToString();
                var groupid = data["groupid"].ToString();
                string chattype = get_chattype(groupid);
                try

                {

                    // update message count
                    if (chattype == "User")

                    {
                        string query = $"update Internal_chat_Notification set Is_read=1,Read_timestamp='" + My.getdate1() + "' where NotificationID in (Select icn.NotificationID   from dbo.[Internal_chat_Notification] icn join Internal_chat_history ich on icn.Message_id = ich.Message_id where  icn.Receiver_id = '" + regid + "' and    ich.Sender_id='" + groupid + "' and Is_read = 0 and ich.Chat_type = 'User')";
                        My.exeSql(query);
                    }
                    else
                    {
                        string query = $" update Internal_chat_Notification set Is_read=1,Read_timestamp='" + My.getdate1() + "' where NotificationID in (Select icn.NotificationID   from dbo.[Internal_chat_Notification] icn join Internal_chat_history ich on icn.Message_id = ich.Message_id where  icn.Receiver_id = '" + regid + "' and Is_read = 0 and ich.Chat_type = 'Group')";
                        My.exeSql(query);
                    }

                }
                catch
                {

                }





                var dt = My.dataTable($"select name as username1,name as username2, user_id ,CASE WHEN profilephoto != '' THEN ProfilePhoto WHEN ProfilePhoto = '' THEN '../images/blank.png'  END AS profile_photo from user_details where user_id='{groupid}'");
                if (dt.Rows.Count == 0)
                {
                    dt = My.dataTable($"select Group_name as username1,Group_name as username2,Group_Id as user_id,CASE WHEN Group_ProfilePhoto != '' THEN Group_ProfilePhoto WHEN Group_ProfilePhoto = '' THEN '../images/blank.png'  END AS profile_photo from Internal_chat_group_list where Group_Id='{groupid}'");

                }
                else
                {

                }


                if (dt.Rows.Count == 0)
                {

                }
                else
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string name = row["username1"].ToString();

                        try
                        {
                            string[] userid = name.ToString().Split(' ');
                            string name1 = userid[0];
                            row["username1"] = name1;
                            row["username2"] = name;

                        }
                        catch
                        {
                            row["username1"] = name;
                            row["username2"] = name;
                        }


                    }
                }

                string Created_by = "";
                var dt3 = My.dataTable($"select icm.*, Format(convert(DateTime,icm.Time_stamp,103), 'dd MMM yyyy') as new_date, Format(convert(DateTime,icm.Time_stamp,103), 'hh:mm tt') as new_time,(Select top 1 ud.name from user_details ud where ud.user_id=icm.Sender_id  )  as lastreplyby,(Select   CASE WHEN ud.ProfilePhoto != '' THEN ProfilePhoto WHEN ProfilePhoto = '' THEN '../images/blank.png'  END AS profile_photo from user_details ud  where  ud.user_id=icm.Sender_id )  as ProfilePhoto,'' as Message_By  from Internal_chat_history icm where icm.Receiver_id='" + groupid + "' and Chat_type='Group'   ");
                if (dt3.Rows.Count == 0)
                {
                    if (chattype == "User")
                    {
                        dt3 = My.dataTable($"select icm.*, Format(convert(DateTime,icm.Time_stamp,103), 'dd MMM yyyy') as new_date, Format(convert(DateTime,icm.Time_stamp,103), 'hh:mm tt') as new_time,(Select top 1 ud.name from user_details ud where ud.user_id=icm.Sender_id  )  as lastreplyby,(Select   CASE WHEN ud.ProfilePhoto != '' THEN ProfilePhoto WHEN ProfilePhoto = '' THEN '../images/blank.png'  END AS profile_photo from user_details ud  where  ud.user_id=icm.Sender_id )  as ProfilePhoto, '' as Message_By  from Internal_chat_history icm where  (icm.Sender_id='" + regid + "' and  icm.Receiver_id='" + groupid + "' ) or  (icm.Sender_id='" + groupid + "' and  icm.Receiver_id='" + regid + "' ) and Chat_type='User'  ");

                    }

                }
                else
                {

                }

                foreach (DataRow dr in dt3.Rows)
                {
                    Created_by = dr["Sender_id"].ToString();
                    if (Created_by == regid)
                    {
                        dr["Message_By"] = "1";
                    }
                    else
                    {
                        dr["Message_By"] = "2";
                    }


                    dr["new_time"] = dr["Status"].ToString() + " " + dr["new_time"].ToString();




                }
                resp = (new JavaScriptSerializer()).Serialize(new
                {
                    message = "Ok",
                    error = false,
                    data = dt.toJsonObject(),
                    messgedetails = dt3.toJsonObject(),

                });
                return Json(resp, JsonRequestBehavior.AllowGet);
            }
            if (method_id == "send_chat_messge")
            {
                var regid = data["regid"].ToString();
                var groupid = data["groupid"].ToString();
                string chattype = get_chattype(groupid);
                var chatmessage = data["chatmessage"].ToString();
                if (chatmessage == "")
                {
                    chatmessage = "";
                }
                DateTime dtm1 = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                string date1 = dtm1.ToString("yyyyMMddhhmmss");
                Random random1 = new Random();
                int tempo1 = random1.Next(999, 9999);
                string messageid = tempo1.ToString() + date1;
                bool flag = false;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
                {
                    SqlConnection con = new SqlConnection(My.conn);
                    con.Open();
                    MyModel.insert_data_Internal_chat_history(con, groupid, messageid, regid, chatmessage, chattype);





                    flag = true;
                    con.Close();
                    scope.Complete();
                }
                if (flag == true)
                {
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "success",
                        error = false,
                        details = groupid,
                        mymessage=My.dataTable($"select icm.*, Format(convert(DateTime,icm.Time_stamp,103), 'dd MMM yyyy') as new_date, Format(convert(DateTime,icm.Time_stamp,103), 'hh:mm tt') as new_time,'' lastreplyby,'' ProfilePhoto, '1' as Message_By  from Internal_chat_history icm where  Message_id='{messageid}'").toJsonObject()
                    });

                }
                return Json(resp, JsonRequestBehavior.AllowGet);
            }
            if (method_id == "update_chat_messge")
            {
                var regid = data["regid"].ToString();
                var groupid = data["groupid"].ToString();
                string messageId = data["messageId"].ToString();
                var chatmessage = data["chatmessage"].ToString();
                string chattype = get_chattype(groupid);

                if (chatmessage == "")
                {
                    chatmessage = "";
                }
                bool flag = false;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
                {
                    SqlConnection con = new SqlConnection(My.conn);
                    con.Open();
                    MyModel.update_data_Internal_chat_history(con, groupid, regid, chatmessage, messageId, chattype);

                    string Remarks = "Message has been updated by " + MyModel.get_user_name(regid) + " Message id " + messageId;
                    payments.exeSql("insert into Internal_chat_log(User_id,Message,Time_stamp) values ('" + regid + "',N'" + Remarks + "',N'" + My.getdate1() + "')", con);


                    flag = true;
                    con.Close();
                    scope.Complete();
                }
                if (flag == true)
                {
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "success",
                        error = false,
                        details = groupid,
                    });

                }
                return Json(resp, JsonRequestBehavior.AllowGet);
            }
            if (method_id == "delete_message_self")
            {
                var regid = data["regid"].ToString();
                var groupid = data["groupId"].ToString();
                var Message_id = data["messageId"].ToString();

                string Remarks = "Message has been deleted by " + MyModel.get_user_name(regid);

                bool flag = false;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(1, 0, 0)))
                {
                    SqlConnection con = new SqlConnection(My.conn);
                    con.Open();

                    string query1 = "INSERT INTO Internal_chat_history_Edit_Delete_backup (Message_id, Sender_id, Receiver_id, Content, Chat_type, Document, Status, Time_stamp, Edited_Deleted_By, Edited_Deleted_timestamp, Remarks)select Message_id,Sender_id,Receiver_id,Content,Chat_type,Document,Status,Time_stamp,'" + regid + "','" + My.getdate1() + "','" + Remarks + "' from Internal_chat_history where Message_id='" + Message_id + "'";
                    SqlCommand cmd1 = new SqlCommand(query1);
                    if (payments.InsertUpdateData(cmd1, con))
                    {
                    }

                    string query = "Update Internal_chat_history set Status=@Status,Document=@Document, Content=@Content,Time_stamp=@Time_stamp where Message_id = @Message_id ";
                    SqlCommand cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Content", Remarks);
                    cmd.Parameters.AddWithValue("@Document", "");
                    cmd.Parameters.AddWithValue("@Status", "Deleted");
                    cmd.Parameters.AddWithValue("@Message_id", Message_id);
                    cmd.Parameters.AddWithValue("@Time_stamp", My.getdate1());
                    if (payments.InsertUpdateData(cmd, con))
                    {
                    }

                    string Remarks2 = "Message has been deleted by " + MyModel.get_user_name(regid) + " Message id " + Message_id;
                    payments.exeSql("insert into Internal_chat_log(User_id,Message,Time_stamp) values ('" + regid + "',N'" + Remarks2 + "',N'" + My.getdate1() + "')", con);


                    flag = true;
                    con.Close();
                    scope.Complete();
                }
                if (flag == true)
                {
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "success delete",
                        error = false,
                        groupname = groupid,
                    });

                }
                else
                {
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "Something is wrong",
                        error = true,
                        groupname = groupid,
                    });
                }

                return Json(resp, JsonRequestBehavior.AllowGet);
            }
            if (method_id == "fetch_top_group")
            {
                var regid = data["regid"].ToString();
                DataTable dt_all_user = My.dataTable($" select Group_name as username,Group_Id as user_id,'' as gcm_id,CASE WHEN Group_ProfilePhoto != '' THEN Group_ProfilePhoto WHEN Group_ProfilePhoto = '' THEN '../images/blank.png'  END AS profile_photo , (select top 1 Content from Internal_chat_history where Receiver_id = Internal_chat_group_list.Group_Id and Chat_type = 'Group' order by id desc ) as last_message,'Group' as typeuser,(select top 1 format(Time_stamp, 'dd/MM/yyyy hh:mm tt') from Internal_chat_history where Receiver_id = Internal_chat_group_list.Group_Id and Chat_type = 'Group' order by id desc ) as last_replytime,(select top 1 format(Time_stamp, 'yyyyMMdd') from Internal_chat_history where Receiver_id = Internal_chat_group_list.Group_Id and Chat_type = 'Group'  order by id desc ) as last_idate,(select count(*) from dbo.[Internal_chat_Notification] icn join Internal_chat_history ich on icn.Message_id = ich.Message_id where ich.Receiver_id =Internal_chat_group_list.Group_Id   and icn.Receiver_id = '{regid}' and Is_read = 0 and ich.Chat_type='Group') as messagecount from Internal_chat_group_list where Istatus = '1' and Group_Id in (select Group_Id from Internal_Chat_group_users_list where User_Id= '{regid}' and Status='Active') and Is_Predefined_Group='1'     order by username ");

                if (dt_all_user.Rows.Count == 0)
                {

                }
                else
                {
                    foreach (DataRow row in dt_all_user.Rows)
                    {
                        if (row["last_idate"].ToString() == mycode.idate())
                        {
                            row["last_replytime"] = row["last_replytime"];

                        }
                        else if (row["last_idate"].ToString() == mycode.onedayago())
                        {
                            row["last_replytime"] = "Yesterday";

                        }
                        else if (row["last_idate"].ToString() == mycode.twodayago())
                        {
                            row["last_replytime"] = "2 days ago";

                        }
                        else if (row["last_idate"].ToString() == mycode.threedayago())
                        {
                            row["last_replytime"] = "3 days ago";

                        }
                        else
                        {
                            row["last_replytime"] = row["last_replytime"].ToString();

                        }
                    }
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "Added",
                        error = false,
                        data = dt_all_user.toJsonObject(),

                    });


                }
                return Json(resp, JsonRequestBehavior.AllowGet);
            }
            if (method_id == "fetch_group_user_list")
            {
                var groupid = "0";
                var regid = data["regid"].ToString();
                if (data["groupid"] == null)
                {
                    groupid = "0";
                }
                else
                {
                    groupid = data["groupid"].ToString();

                }


                DataTable dt_all_user = My.dataTable($"select name as username,user_id,gcm_id,CASE WHEN ProfilePhoto != '' THEN ProfilePhoto WHEN ProfilePhoto = '' THEN '../images/blank.png'  END AS profile_photo,  (select top 1 Content from Internal_chat_history where Sender_id = user_details.user_id and Chat_type = 'User' and Receiver_id='{regid}' order by id desc ) as last_message,'User' as typeuser,(select top 1 format(Time_stamp, 'dd/MM/yyyy hh:mm tt') from Internal_chat_history where Sender_id = user_details.user_id and Chat_type = 'User' and Receiver_id='{regid}' order by id desc ) as last_replytime,(select top 1 format(Time_stamp, 'yyyyMMdd') from Internal_chat_history where Sender_id = user_details.user_id and Chat_type = 'User'  and Receiver_id='{regid}' order by id desc ) as last_idate,(select count(*) from dbo.[Internal_chat_Notification] icn join Internal_chat_history ich on icn.Message_id = ich.Message_id where ich.Sender_id =user_details.user_id  and icn.Receiver_id = '{regid}' and Is_read = 0 and   ich.Chat_type='User' and ich.Sender_id!= '{regid}') as messagecount,'' as addcss,'' as  Is_Predefined_Group,(select top 1 format(icn.Time_stamp, 'yyyyMMddhhmmss') from dbo.[Internal_chat_Notification] icn join Internal_chat_history ich on icn.Message_id = ich.Message_id where ich.Sender_id =user_details.user_id  and icn.Receiver_id = '{regid}'  and   ich.Chat_type='User' and ich.Sender_id!= '{regid}' order by icn.id desc) as lasttattime from user_details where Istatus = 1 and User_Type!= 'Visitor' and user_id!= '{regid}' and  user_id in (Select User_Id from Internal_Chat_group_users_list where Group_Id='" + groupid + "' and Status='Active')    order by  lasttattime desc, username   ");

                if (dt_all_user.Rows.Count == 0)
                {

                }
                else
                {
                    foreach (DataRow row in dt_all_user.Rows)
                    {
                        if (row["last_idate"].ToString() == mycode.idate())
                        {
                            row["last_replytime"] = row["last_replytime"];

                        }
                        else if (row["last_idate"].ToString() == mycode.onedayago())
                        {
                            row["last_replytime"] = "Yesterday";

                        }
                        else if (row["last_idate"].ToString() == mycode.twodayago())
                        {
                            row["last_replytime"] = "2 days ago";

                        }
                        else if (row["last_idate"].ToString() == mycode.threedayago())
                        {
                            row["last_replytime"] = "3 days ago";

                        }
                        else
                        {
                            row["last_replytime"] = row["last_replytime"].ToString();

                        }
                    }
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "Added",
                        error = false,
                        data = dt_all_user.toJsonObject(),

                    });


                }
                return Json(resp, JsonRequestBehavior.AllowGet);
            }
            if (method_id == "fetch_group_list_in_tab")
            {

                var regid = data["regid"].ToString();
                DataTable dt_all_user = My.dataTable($" select Group_name as username,Group_Id as user_id,'' as gcm_id,CASE WHEN Group_ProfilePhoto != '' THEN Group_ProfilePhoto WHEN Group_ProfilePhoto = '' THEN '../images/blank.png'  END AS profile_photo , (select top 1 Content from Internal_chat_history where Receiver_id = Internal_chat_group_list.Group_Id and Chat_type = 'Group' order by id desc ) as last_message,'Group' as typeuser,(select top 1 format(Time_stamp, 'dd/MM/yyyy hh:mm tt') from Internal_chat_history where Receiver_id = Internal_chat_group_list.Group_Id and Chat_type = 'Group' order by id desc ) as last_replytime,(select top 1 format(Time_stamp, 'yyyyMMdd') from Internal_chat_history where Receiver_id = Internal_chat_group_list.Group_Id and Chat_type = 'Group'  order by id desc ) as last_idate,(select count(*) from dbo.[Internal_chat_Notification] icn join Internal_chat_history ich on icn.Message_id = ich.Message_id where ich.Receiver_id =Internal_chat_group_list.Group_Id   and icn.Receiver_id = '{regid}' and Is_read = 0 and ich.Chat_type='Group') as messagecount,(select top 1 format(icn.Time_stamp,'yyyyMMddhhmmss') from dbo.[Internal_chat_Notification] icn join Internal_chat_history ich on icn.Message_id = ich.Message_id where ich.Receiver_id =Internal_chat_group_list.Group_Id   and icn.Receiver_id = '{regid}'  and ich.Chat_type='Group' order by icn.id desc) as lasttattime,(select count(Id) from Internal_Chat_group_users_list where Status='Active' and Group_Id =Internal_chat_group_list.Group_Id) as totalmember from Internal_chat_group_list  where Istatus = '1' and Group_Id in (select Group_Id from Internal_Chat_group_users_list where User_Id= '{regid}' and Status='Active')    order by  lasttattime desc, username ");

                if (dt_all_user.Rows.Count == 0)
                {

                }
                else
                {
                    foreach (DataRow row in dt_all_user.Rows)
                    {
                        if (row["last_idate"].ToString() == mycode.idate())
                        {
                            row["last_replytime"] = row["last_replytime"];

                        }
                        else if (row["last_idate"].ToString() == mycode.onedayago())
                        {
                            row["last_replytime"] = "Yesterday";

                        }
                        else if (row["last_idate"].ToString() == mycode.twodayago())
                        {
                            row["last_replytime"] = "2 days ago";

                        }
                        else if (row["last_idate"].ToString() == mycode.threedayago())
                        {
                            row["last_replytime"] = "3 days ago";

                        }
                        else
                        {
                            row["last_replytime"] = row["last_replytime"].ToString();

                        }
                    }
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "Added",
                        error = false,
                        data = dt_all_user.toJsonObject(),

                    });


                }
                return Json(resp, JsonRequestBehavior.AllowGet);
            }
            if (method_id == "get_notification_message")
            {
                // return DateTime.UtcNow.AddHours(5).AddMinutes(30).ToString("HHmmss");
                string idatetime = mycode.idate() + mycode.itime();
                var regid = data["regid"].ToString();
                var lastid = data["lastid"].ToString();
                //var dt = My.dataTable($" Select  icn.Id ,format(icn.Time_stamp,'dd MMM yyyy hh:mm tt') as timesstamp,Content as notification,ich.Sender_id,'' as sendby,ich.Receiver_id,ich.Chat_type from Internal_chat_Notification icn join Internal_chat_history ich on icn.Message_id = ich.Message_id where icn.Receiver_id = '{regid}' and icn.Is_read = 0 and icn.Id>" + lastid + "");
                //lastreplyby

                var dt = My.dataTable($"select icm.*, Format(convert(DateTime,icm.Time_stamp,103), 'dd MMM yyyy') as new_date, Format(convert(DateTime,icm.Time_stamp,103), 'hh:mm tt') as new_time,(Select top 1 ud.name from user_details ud where ud.user_id=icm.Sender_id  )  as lastreplyby,(Select   CASE WHEN ud.ProfilePhoto != '' THEN ProfilePhoto WHEN ProfilePhoto = '' THEN '../images/blank.png'  END AS profile_photo from user_details ud  where  ud.user_id=icm.Sender_id )  as ProfilePhoto, '' as Message_By  , '' as send_By  from Internal_chat_history icm  where Message_id in (Select   Message_id from Internal_chat_Notification icn where icn.Receiver_id = '{regid}' and icn.Is_read = 0 and icn.Id>" + lastid +   ")");

                lastid = My.data($" Select  top 1 Id  from Internal_chat_Notification where  Receiver_id = '{regid}' and Is_read = 0 order by Id desc");


                var lastmessagetime = My.data($" Select  top 1 format(Time_stamp,'yyyyMMddhhmmss') as timesstamp  from Internal_chat_Notification where  Receiver_id = '{regid}'  order by Id desc");

                foreach (DataRow dr in dt.Rows)
                { 
                    if (dr["Chat_type"].ToString() == "User")
                    {
                        dr["send_By"] = get_send_by_name(dr["Sender_id"].ToString());
                    }
                    else
                    {
                        dr["send_By"] = get_send_by_name(dr["Receiver_id"].ToString());

                    }
                   // dr["Content"] = dr["Content"].ToString() + "</br>" + dr["new_date"].ToString() + "  "+ dr["new_time"].ToString();
                }
                resp = (new JavaScriptSerializer()).Serialize(new
                {
                    error = false,
                    message = "Yes",
                    data = dt.toJsonObject(),
                    lastid = lastid.ToInt(),
                    lasttattime = lastmessagetime.ToDouble(),
                });

                return Json(resp, JsonRequestBehavior.AllowGet);
            }

            return Content("invalid");
        }

        private object get_send_by_name(string sender_id)
        {
            var dt = My.dataTable($"select name  from  user_details where user_id= '{sender_id}'  ");
            if (dt.Rows.Count == 0)
            {
                var dt2 = My.dataTable($"select Group_name  from  Internal_chat_group_list where Group_Id= '{sender_id}'  ");
                if (dt2.Rows.Count == 0)
                {
                    return "";
                }
                else
                {
                    return dt2.Rows[0][0].ToString();
                }
            }
            else
            {
                return dt.Rows[0][0].ToString();
            }
        }

        private string get_chattype(string groupid)
        {
            DataTable dt = mycode.FillData(" select  top 1 * from Internal_chat_group_list where Group_Id='" + groupid + "'  ");
            if (dt.Rows.Count == 0)
            {
                return "User";
            }
            else
            {
                return "Group";
            }
        }

        My mycode = new My();
        private bool get_group_name(string groupname, string groupid)
        {
            DataTable dt = mycode.FillData("Select * from Internal_chat_group_list where user_id='" + groupname + "' and Group_Id!='" + groupid + "'");
            if (dt.Rows.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}