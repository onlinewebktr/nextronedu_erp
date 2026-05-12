using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using school_web.AppCode;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Data.SqlClient;

namespace school_web.Areas.OnlineTest.Controllers
{

    [RouteArea("OnlineTest")]
    public class uploadquestionController : Controller
    {
        // GET: OnlineTest/uploadquestion
        [Route("Upload_Question")]
        public ActionResult Upload_Question()
        {
             
            const string quote = "\"";
            string tinyMC = My.get_single_column_data("select TinyMC_link from Firm_Details");
            if (tinyMC != "")
            {
                ViewBag.TinyMC_link = "<script src=" + quote + tinyMC + quote + " referrerpolicy=" + quote + "origin" + quote + " ></script>";
            }
            else
            {
                ViewBag.TinyMC_link = "<script src=" + quote + "https://cdn.tiny.cloud/1/h64ik3cu5x1uocom2fuu89jbo1ah2yqk1rtvpwu420y3ye4w/tinymce/7/tinymce.min.js" + quote + " referrerpolicy=" + quote + "origin" + quote + "></script>";
            }

            //return View("Upload_Question");
            return View();
        }
        [Route("view_uploaded_question")]
        public ActionResult view_uploaded_Question()
        {
            return View();
        }

            My mycode = new My();
        [Route("data/{method_id}")]
        public ActionResult getdata(string method_id, Dictionary<string, object> data)
        {
            string session_id = My.get_session_id();
            var resp = (new JavaScriptSerializer()).Serialize(new
            {
                message = "Some thing goes worng",
                error = true,
                dataId = method_id,
                data = data,
            });
            if (method_id == "ddl")
            {
               
                if (data["type"].ToString() == "gettestname")
                {

                    var testlist = My.dataTable($"Select Exam_name,Entry_id from OLINETEST_EXAM_NAME_Murge_Section where Status='Inactive' and Session_id= '{session_id}' order by Exam_name asc");
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "sucess",
                        error = false,
                        data = testlist.toJsonObject(),
                        
                    });
                    return Json(resp, JsonRequestBehavior.AllowGet);
                }
                if (data["type"].ToString() == "gettestname_all_testname")
                {

                    var testlist = My.dataTable($"Select Exam_name,Entry_id from OLINETEST_EXAM_NAME_Murge_Section where Session_id= '{session_id}' and Entry_id in (Select Exam_id from Upload_Question_Question_Teacher)  order by Exam_name asc");
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "sucess",
                        error = false,
                        data = testlist.toJsonObject(),

                    });
                    return Json(resp, JsonRequestBehavior.AllowGet);
                }
                else if (data["type"].ToString() == "getsubject")
                {
                    DataTable testlist;
                    if (data["usertype"].ToString() == "admin")
                    {
                          testlist = My.dataTable($"select  sm.Subject_name ,sm.Subject_id from Subject_Master sm join OLINETEST_EXAM_NAME_Murge_Section en on sm.course_id=en.Class_id where en.Session_id='{session_id}'   and   Entry_id='{data["testid"].ToString()}' and Is_mandatory=1  and Session_id='{session_id}'");
                    }
                    else
                    {
                          testlist = My.dataTable($"select  sm.Subject_name ,sm.Subject_id from Subject_Master sm join OLINETEST_EXAM_NAME_Murge_Section en on sm.course_id=en.Class_id where en.Session_id='{session_id}'   and   Entry_id='{data["testid"].ToString()}' and Is_mandatory=1  and sm.Subject_id in (select AssignCourseID from TeacherCourseSubjectMaping where UserID='{data["teacherid"].ToString()}'  and Session_id='{session_id}')");
                    }



                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "sucess",
                        error = false,
                        data = testlist.toJsonObject(),

                    });
                    return Json(resp, JsonRequestBehavior.AllowGet);
                }


            }
            if (method_id == "datasave")
            {
                if (data["type"].ToString() == "savequestion")
                {
                    string classid = mycode.get_classid_from_testid(data["test"].ToString());
                    string group_entryid = My.auto_serialS("Online_Objective_Entry_id");
                    string marks = mycode.get_marks_murge_Section(data["test"].ToString());
                   
                    bool chek_question_verfied = check_question_status(data["test"].ToString());
                    if (chek_question_verfied == true)
                    {
                        bool chek_question_active = check_question_status(data["test"].ToString());
                        if (chek_question_verfied == true)
                        {
                            SqlCommand cmd;
                            string strQuery = @"INSERT INTO Upload_Question_Question_Teacher (Question_name,Option1,Option2,Option3,Option4,AnswerOption,Explanation,Status,Test_id,date,idate,user_id,row_error,sub_id,section,Exam_id,class_id,Objective_Entry_id,Marks,Anstest) values (@Question_name,@Option1,@Option2,@Option3,@Option4,@AnswerOption,@Explanation,@Status,@Test_id,@date,@idate,@user_id,@row_error,@sub_id,@section,@Exam_id,@class_id,@Objective_Entry_id,@Marks,@Anstest)";
                            cmd = new SqlCommand(strQuery);
                            cmd.Parameters.AddWithValue("@Question_name", data["question"].ToString());
                            cmd.Parameters.AddWithValue("@Option1", data["option1"].ToString());
                            cmd.Parameters.AddWithValue("@Option2", data["option2"].ToString());
                            cmd.Parameters.AddWithValue("@Option3", data["option3"].ToString());
                            cmd.Parameters.AddWithValue("@Option4", data["option4"].ToString());
                            cmd.Parameters.AddWithValue("@AnswerOption", data["answer"].ToString());
                            cmd.Parameters.AddWithValue("@Explanation", data["questionexplanation"].ToString());
                            cmd.Parameters.AddWithValue("@Status", "Pending");
                            cmd.Parameters.AddWithValue("@Test_id", data["test"].ToString());
                            cmd.Parameters.AddWithValue("@date", mycode.date());
                            cmd.Parameters.AddWithValue("@idate", mycode.idate());
                            cmd.Parameters.AddWithValue("@user_id", data["teacherid"].ToString());
                            cmd.Parameters.AddWithValue("@row_error", "");
                            cmd.Parameters.AddWithValue("@sub_id", data["subject"].ToString());
                            cmd.Parameters.AddWithValue("@section", "ALL");
                            cmd.Parameters.AddWithValue("@Exam_id", data["test"].ToString());
                            cmd.Parameters.AddWithValue("@class_id", classid);
                            cmd.Parameters.AddWithValue("@Objective_Entry_id", group_entryid);
                            cmd.Parameters.AddWithValue("@Marks", marks);

                            if (data["answer"].ToString() == "Option1")
                            {
                                cmd.Parameters.AddWithValue("@Anstest", data["option1"].ToString());
                            }
                            if (data["answer"].ToString() == "Option2")
                            {
                                cmd.Parameters.AddWithValue("@Anstest", data["option2"].ToString());
                            }
                            if (data["answer"].ToString() == "Option3")
                            {
                                cmd.Parameters.AddWithValue("@Anstest", data["option3"].ToString());
                            }
                            if (data["answer"].ToString() == "Option4")
                            {
                                cmd.Parameters.AddWithValue("@Anstest", data["option4"].ToString());
                            }


                            if (My.InsertUpdateData(cmd))
                            {
                                resp = (new JavaScriptSerializer()).Serialize(new
                                {
                                    message = "sucess",
                                    error = false,
                                    data = "sucess",

                                });
                                return Json(resp, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            resp = (new JavaScriptSerializer()).Serialize(new
                            {
                                message = "This test is active, so you can't upload any more questions.",
                                error = true,
                                data = "sucess",

                            });
                            return Json(resp, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        resp = (new JavaScriptSerializer()).Serialize(new
                        {
                            message = "All questions in this test have been verified, So you can't upload any more questions.",
                            error = true,
                            data = "sucess",

                        });
                        return Json(resp, JsonRequestBehavior.AllowGet);
                    }

                }
                if (data["type"].ToString() == "data_update")
                {

                    

                        SqlCommand cmd;
                    string strQuery = @"update Upload_Question_Question_Teacher set Question_name=@Question_name,Option1=@Option1,Option2=@Option2,Option3=@Option3,Option4=@Option4,AnswerOption=@AnswerOption,Explanation=@Explanation,Modify_date=@Modify_date,Modify_by=@Modify_by,Anstest=@Anstest where id=@id";
                    cmd = new SqlCommand(strQuery);
                    cmd.Parameters.AddWithValue("@id", data["id"].ToString());
                    cmd.Parameters.AddWithValue("@Question_name", data["question"].ToString());
                    cmd.Parameters.AddWithValue("@Option1", data["option1"].ToString());
                    cmd.Parameters.AddWithValue("@Option2", data["option2"].ToString());
                    cmd.Parameters.AddWithValue("@Option3", data["option3"].ToString());
                    cmd.Parameters.AddWithValue("@Option4", data["option4"].ToString());
                    cmd.Parameters.AddWithValue("@AnswerOption", data["answer"].ToString());
                    cmd.Parameters.AddWithValue("@Explanation", data["questionexplanation"].ToString());
                    cmd.Parameters.AddWithValue("@Modify_date", My.getdate1());
                     
                    cmd.Parameters.AddWithValue("@Modify_by", data["teacherid"].ToString());
                    if (data["answer"].ToString() == "Option1")
                    {
                        cmd.Parameters.AddWithValue("@Anstest", data["option1"].ToString());
                    }
                    if (data["answer"].ToString() == "Option2")
                    {
                        cmd.Parameters.AddWithValue("@Anstest", data["option2"].ToString());
                    }
                    if (data["answer"].ToString() == "Option3")
                    {
                        cmd.Parameters.AddWithValue("@Anstest", data["option3"].ToString());
                    }
                    if (data["answer"].ToString() == "Option4")
                    {
                        cmd.Parameters.AddWithValue("@Anstest", data["option4"].ToString());
                    }
                    if (My.InsertUpdateData(cmd))
                    {
                        resp = (new JavaScriptSerializer()).Serialize(new
                        {
                            message = "sucess",
                            error = false,
                            data = "sucess",

                        });
                        return Json(resp, JsonRequestBehavior.AllowGet);
                    }
                }
                if (data["type"].ToString() == "delete_question")
                {
                    My.exeSql($"delete from Upload_Question_Question_Teacher where Id={data["Id"].ToString()}");
                    resp = (new JavaScriptSerializer()).Serialize(new
                    {
                        message = "Not data found",
                        error = false,
                        data = "0",

                    });
                    return Json(resp, JsonRequestBehavior.AllowGet);


                }
                if (data["type"].ToString() == "get_data_for_edit")
                {
                    var questionlist = My.dataTable($"select * from Upload_Question_Question_Teacher where Id={data["id"].ToString()}");

                    if (questionlist.Rows.Count == 0)
                    {
                        resp = (new JavaScriptSerializer()).Serialize(new
                        {
                            message = "Not data found",
                            error = true,
                            data = "0",

                        });
                    }
                    else
                    {
                        resp = (new JavaScriptSerializer()).Serialize(new
                        {
                            message = "sucess",
                            error = false,
                            data = questionlist.toJsonObject(),

                        });

                    }
                    return Json(resp, JsonRequestBehavior.AllowGet);
                  


                }

            }
             
            if (method_id == "get_questionlist")
            {
                
                if (data["type"].ToString() == "get_uploadedquestionlistbyteacher")
                {
                    DataTable questionlist;
                    if (data["usertype"].ToString()=="admin")
                    {
                          questionlist = My.dataTable($"SELECT (select top 1 name from user_details where user_id=up.user_id) as uploadby ,sm.Subject_name, sm.Subject_id,up.*,en.Exam_name FROM  Upload_Question_Question_Teacher up JOIN Subject_Master sm ON sm.course_id = up.class_id AND sm.Subject_id = up.sub_id JOIN OLINETEST_EXAM_NAME_Murge_Section en ON up.Exam_id = en.Entry_id WHERE en.Session_id = '{session_id}' AND en.Entry_id = '{data["testid"].ToString()}'   ");
                    }
                    else
                    {
                          questionlist = My.dataTable($"SELECT  (select top 1 name from user_details where user_id=up.user_id) as uploadby ,sm.Subject_name, sm.Subject_id,up.*,en.Exam_name FROM  Upload_Question_Question_Teacher up JOIN Subject_Master sm ON sm.course_id = up.class_id AND sm.Subject_id = up.sub_id JOIN OLINETEST_EXAM_NAME_Murge_Section en ON up.Exam_id = en.Entry_id WHERE en.Session_id = '{session_id}' AND en.Entry_id = '{data["testid"].ToString()}'  AND up.sub_id = '{data["subject_id"].ToString()}'  AND up.user_id = '{data["teacherid"].ToString()}'");
                    }

                    



                    if (questionlist.Rows.Count == 0)
                    {
                        resp = (new JavaScriptSerializer()).Serialize(new
                        {
                            message = "Not data found",
                            error = true,
                            data = "0",

                        });
                    }
                    else
                    {
                        resp = (new JavaScriptSerializer()).Serialize(new
                        {
                            message = "sucess",
                            error = false,
                            data = questionlist.toJsonObject(),

                        });

                    }
                    return Json(resp, JsonRequestBehavior.AllowGet);

                }
                    
            }


                return Content("invalid");
        }

        private bool check_question_status(string testtestid)
        {
            SqlCommand cmd = new SqlCommand("select top 1 Status from Upload_Question_Question_Teacher where Exam_id='" + testtestid + "' and Status='Verified'");//and Section='" + hd_section_id.Value + "'
            DataTable dt = mycode.GetData(cmd);
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