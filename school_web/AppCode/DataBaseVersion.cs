using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace school_web.AppCode
{
    public class DataBaseVersion
    {
        public static string current_version;
        internal void add_quesry_to_version(string version, string query)
        {
            List<String> query_list;
            if (My.version_query_list.ContainsKey(version))
            {
                query_list = My.version_query_list[version];
            }
            else
            {
                query_list = new List<string>();

            }
            query_list.Add(query);
            My.version_query_list[version] = query_list;
        }

        internal List<string> get_query_list(string version)
        {
            List<String> list = new List<string>();
            switch (version)
            {
    




                #region Case 1.0.0.67
                case "1.0.0.67":
                    {
                        list.Add(@"ALTER TABLE admission_registor ADD emailid_student varchar (500);");
                        list.Add(@"ALTER TABLE admission_registor ADD student_mobile varchar (500);");
                        list.Add(@"ALTER TABLE admission_registor ADD Agent_Code varchar (500);");
                        list.Add(@"Create Table dbo.[Admit_Generation_Status] (
Id int Not Null Primary Key Identity(1,1),
Session_Id int,
Class_Id int,
Section varchar (50),
Branch_id int,
Exam_Term_Id int,
Exam_Id int,
Generation_Type varchar (250),
Created_Date varchar (50),
Created_By varchar (50),
Admission_No varchar (50),
Idate int);");
                        list.Add(@"Create Table dbo.[Agent_Master] (
Agent_Code varchar (50) Not Null,
Agent_Name varchar (200) Not Null,
Mobile_No varchar (15),
Email varchar (150),
Password varchar (200) Not Null,
IsActive bit,
Created_Date datetime);");
                        list.Add(@"ALTER TABLE App_Setting ADD Max_Device_Count int;
");
                        list.Add(@"ALTER TABLE App_Setting ADD ICIC_MID varchar (500);");
                        list.Add(@"ALTER TABLE App_Setting ADD ICIC_Agg_ID varchar (500);");
                        list.Add(@"ALTER TABLE App_Setting ADD ICIC_Key varchar (500);");
                        list.Add(@"Create Table dbo.[Ask_Doubt_Files] (
Id int Not Null Primary Key Identity(1,1),
doubt_id varchar (500),
file_path varchar (500));");
                        list.Add(@"Create Table dbo.[Caste_jati] (
Id int Not Null Primary Key Identity(1,1),
Caste_name varchar (500));");
                        list.Add(@"Create Table dbo.[DeveloperUsers] (
Id int Not Null Primary Key Identity(1,1),
UserId nvarchar (200),
Password nvarchar (200));
");
                        list.Add(@"Create Table dbo.[Employee_Edit_History] (
HistoryId int Not Null Primary Key Identity(1,1),
EmployeeId varchar (50),
Emp_Code varchar (50),
FieldName varchar (200),
OldValue nvarchar (MAX),
NewValue nvarchar (MAX),
ChangedBy varchar (150),
ChangedOn datetime);");
                        list.Add(@"Create Table dbo.[Employee_Status_History] (
Id int Not Null Primary Key Identity(1,1),
Employee_Id varchar (50),
Emp_Code varchar (50),
Old_Status varchar (20),
New_Status varchar (20),
Changed_By varchar (50),
Changed_On datetime);");
                        list.Add(@"ALTER TABLE Enquiry_Details ADD second_name varchar (500);");
                        list.Add(@"ALTER TABLE Enquiry_Details ADD last_name varchar (50);");
                        list.Add(@"ALTER TABLE Enquiry_Details ADD Father_second_name varchar (50);");
                        list.Add(@"ALTER TABLE Enquiry_Details ADD Father_last_name varchar (50);");
                         
                        list.Add(@"ALTER TABLE Enquiry_Details ADD Mother_first_name varchar (50);");
                        list.Add(@"ALTER TABLE Enquiry_Details ADD Mother_second_name varchar (50);");

                        list.Add(@"ALTER TABLE Enquiry_Details ADD Mother_last_name varchar (50);");
                        list.Add(@"ALTER TABLE Enquiry_Details ADD Session_Id varchar (50);");
                        list.Add(@"ALTER TABLE Exam_Assessment_Details ADD IsVisible_RC bit;");
                        list.Add(@"ALTER TABLE Exam_Assessment_Details ADD Compare_Assessment_Id varchar (50);");
                        list.Add(@"Create Table dbo.[Exam_Generel_Guideline] (
Id int Not Null Primary Key Identity(1,1),
Guidline nvarchar (MAX),
Session_Id varchar (50),
Created_Date varchar (50),
Created_Idate varchar (50),
Created_By varchar (50));");
                        list.Add(@"Create Table dbo.[Exam_overall_no_for_rank] (
Id int Not Null Primary Key Identity(1,1),
Session_id varchar (50),
Class_id varchar (50),
Admission_no varchar (50),
Branch_id varchar (50),
Term_id varchar (50),
Full_mark float,
Obtained_mark float,
Persentage float,
Created_date varchar (50),
Created_idate int,
Section varchar (50));");
                        list.Add(@"Create Table dbo.[Exam_Personality_Traits_Exam_Wise] (
Id int Not Null Primary Key Identity(1,1),
Session_id int,
Branch_id int,
Section varchar (500),
Class_id int,
Exam_Term_Id int,
Exam_Id int,
Activity_Id int,
Admission_no varchar (500),
Term_grade varchar (500),
Cretaed_by varchar (500),
Created_date datetime);");
                        list.Add(@"Create Table dbo.[Exam_Shift_Master] (
Id int Not Null Primary Key Identity(1,1),
Shift_Name varchar (50),
Start_Time varchar (50),
End_Time varchar (50),
Created_Date varchar (50),
Created_idate varchar (50),
Created_By varchar (50));");
                        list.Add(@"ALTER TABLE EXAM_TEMP_MARK_ASSESSMENT_GROUPWISE ADD Section varchar (500);");
                        list.Add(@"ALTER TABLE EXAM_TEMP_MARK_GROUPWISE ADD Section varchar (500);");
                        list.Add(@"Create Table dbo.[Exam_Wise_Attendance] (
Id int Not Null Primary Key Identity(1,1),
Session_id int,
Branch_id varchar (500),
Section varchar (500),
Admission_no varchar (500),
Class_id int,
No_of_class_Attendance varchar (500),
Cretaed_by varchar (500),
Created_date datetime,
Exam_Term_Id int,
Exam_Id int,
Total_no_of_class varchar (500));");
                        list.Add(@"Create Table dbo.[Examwise_Commentary_Remark] (
Id int Not Null Primary Key Identity(1,1),
Session_id int,
Branch_id int,
Section varchar (500),
Class_id int,
Exam_Term_Id int,
Exam_Id int,
Remarks_id int,
Admission_no varchar (500),
Remarks nvarchar (MAX),
Cretaed_by varchar (500),
Created_date datetime);");
                        list.Add(@"Create Table dbo.[Fee_head_type_master] (
Id int Not Null Primary Key Identity(1,1),
Fee_content varchar (50),
Fee_content_id varchar (50),
Position int,
parameter varchar (50));");
                        list.Add(@"ALTER TABLE Firm_Details ADD Is_teacher_forceLogout int;");
                        list.Add(@"ALTER TABLE Firm_Details ADD Is_student_forceLogout int;");
                        list.Add(@"ALTER TABLE Form_sale_details ADD Current_Session varchar (50);");
                        list.Add(@"ALTER TABLE globle_data ADD Log_ID varchar (50);");
                        list.Add(@"ALTER TABLE HMS_EMPLOYEE_DETAILS ADD No_of_Visits int;");
                        list.Add(@"ALTER TABLE HMS_INVENTORY_BILL_PAYMENT_TRACKING ADD Branch_id varchar (50);");
                        list.Add(@"ALTER TABLE HR_EXTRA_SHIFT_WORKING ADD firm_id varchar (50);");
                        list.Add(@"ALTER TABLE HR_FIRM_DETAILS ADD Authorised_Signature varchar (5000);");
                        list.Add(@"Create Table dbo.[Inventory_Stock_Log] (
Log_ID int Not Null Primary Key Identity(1,1),
Stock_ID int,
Old_Quantity decimal,
Changed_Quantity decimal,
New_Quantity decimal,
Action_Type varchar (20),
Reason varchar (500),
Changed_By varchar (100),
Changed_On datetime,
Item_Code varchar (50));");
                        list.Add(@"Create Table dbo.[Mother_occupation] (
Id int Not Null Primary Key Identity(1,1),
Occupation varchar (50));
");
                        list.Add(@"ALTER TABLE Online_Admission ADD Admission_No varchar (50);");
                        list.Add(@"ALTER TABLE Online_Admission ADD txnDate varchar (500);
");
                        list.Add(@"Create Table dbo.[Parents_Device] (
Id int Not Null Primary Key Identity(1,1),
Parents_id varchar (500),
DeviceId varchar (5000),
LoginTime datetime,
Brand varchar (500),
Model varchar (500),
DeviceName varchar (500),
AndroidVersion varchar (500));");
                        list.Add(@"ALTER TABLE party_details ADD class_name varchar (500);");
                        list.Add(@"ALTER TABLE party_details ADD Section varchar (50);");
                        list.Add(@"ALTER TABLE Payment_transaction_process ADD txnDate varchar (500);");
                        list.Add(@"Create Table dbo.[Permission_for_hostel_attendance] (
Id int Not Null Primary Key Identity(1,1),
Session_id varchar (50),
User_id varchar (50),
Assign_by varchar (50),
Assign_date varchar (50),
Assign_time varchar (50));");
                        list.Add(@"ALTER TABLE question_info ADD sub_id varchar (50);");
                        list.Add(@"ALTER TABLE SCHOLARSHIP_ADMISSION ADD Star_Person varchar (50);");
                        list.Add(@"ALTER TABLE SCHOLARSHIP_ADMISSION ADD Payment_Receipt varchar (MAX);");
                        list.Add(@"ALTER TABLE SCHOLARSHIP_ADMISSION ADD Payment_Reference nvarchar (MAX);");
                        list.Add(@"Create Table dbo.[Scholarship_Agent_Details] (
Id int Not Null Primary Key Identity(1,1),
Name varchar (50),
Mobile varchar (50),
User_Id varchar (50),
Password varchar (50));");
                        list.Add(@"ALTER TABLE SMS_Template_Setting ADD Wid varchar (50);");
                        list.Add(@"Create Table dbo.[Student_Device] (
Id int Not Null Primary Key Identity(1,1),
admissionserialnumber varchar (500),
DeviceId varchar (5000),
LoginTime datetime,
Brand varchar (500),
Model varchar (500),
DeviceName varchar (500),
AndroidVersion varchar (500));");
                        list.Add(@"ALTER TABLE Subject_Master ADD IsAuxileary bit;");
                        list.Add(@"ALTER TABLE Subject_Master ADD Is_optional bit;");
                        list.Add(@"ALTER TABLE Transfer_certificate ADD TC_cancellation_remarks nvarchar (MAX);");
                        list.Add(@"update App_Setting set Max_Device_Count ='3' ");
                       
                        list.Add(@"delete from  App_Dashboard_Setting");

                        list.Add(@"Insert into [App_Dashboard_Setting] (UserType,Title,Image,Icon,Icon_Bg,Action,Secquence,Status,MenuType,Is_Restricted,Header_Image,Parent_Menu_Id,Menu_Id,isHostel) values 
(N'Student',N'My Profile',N'profile_stu.png',null,null,N'',11,1,N'Academic',null,null,null,null,N'0'),
(N'Student',N'My Attendance',N'attendance_stu.png',null,null,N'/StudentWebview/View_Attandance_Class_Wise.aspx?regid=#uid#',12,1,N'Academic',null,null,null,null,N'0'),
(N'Student',N'Live Class Attandance',N'ac_attendance.png',null,null,N'/Student_Profile/webview/Student_Attendance_Report.aspx?regid=#uid#',3,0,N'Academic',null,null,null,null,N'0'),
(N'Student',N'My Routine',N'classroutine_stu.png',null,null,N'/app_nextSof/My_Routine_Student.html?regid=#uid#',7,1,N'Academic',null,null,null,null,N'0'),
(N'Student',N'Calender',N'clander_stu.png',null,null,N'/StudentWebview/School_Calendar.aspx?regid=#uid#',1,1,N'Academic',null,null,null,null,N'0'),
(N'Student',N'Home Work',N'homework_stu.png',null,null,N'/app_nextSof/HomeWork_List_Student.html?regid=#uid#',2,1,N'Academic',null,null,null,null,N'0'),
(N'Student',N'Study Material',N'studymaterial_stu.png',null,null,N'',3,1,N'Academic',null,null,null,null,N'0'),
(N'Student',N'E-Book',N'ebook_stu.png',null,null,N'',4,1,N'Academic',null,null,null,null,N'0'),
(N'Student',N'Class Activity',N'class_activity_stu.png',null,null,N'/app_nextSof/ClassActivity_student.html?regid=#uid#',5,1,N'Academic',null,null,null,null,N'0'),
(N'Student',N'Ask Doubt',N'askdoubt_stu.png',null,null,N'/app_nextSof/ask-doubt.html?regid=#uid#',6,1,N'Academic',null,null,null,null,N'0');
Insert into [App_Dashboard_Setting] (UserType,Title,Image,Icon,Icon_Bg,Action,Secquence,Status,MenuType,Is_Restricted,Header_Image,Parent_Menu_Id,Menu_Id,isHostel) values 
(N'Student',N'Live Class',N'live_class.png',null,null,N'',11,0,N'Academic',null,null,null,null,N'0'),
(N'Student',N'PTM',N'online_meet.png',null,null,N'#',12,0,N'Academic',null,null,null,null,N'0'),
(N'Student',N'Event',N'events_stu.png',null,null,N'/app_nextSof/student-events-list.html?regid=#uid#',1,1,N'Notice/Circular/Events',null,null,null,null,N'0'),
(N'Student',N'Teacher Message',N'teacher_msg_stu.png',null,null,N'',5,0,N'Notice/Circular/Events',null,null,null,null,N'0'),
(N'Student',N'Notice',N'notice_stu.png',null,null,N'/app_nextSof/student-notice-list.html?regid=#uid#',2,1,N'Notice/Circular/Events',null,null,null,null,N'0'),
(N'Student',N'School Bus',N'school_bus.png',null,null,N'',10,1,N'Examination & Others',null,null,null,null,N'0'),
(N'Student',N'Bus Live Location',N'bus_live_location.png',null,null,N'/StudentWebview/bus-location.aspx?regid=#uid#',4,1,N'Examination & Others',null,null,null,null,N'0'),
(N'Student',N'Exam Routine',N'exam_routine_stu.png',null,null,N'/Student_Profile/webview/exam-routine.aspx?regid=#uid#',1,1,N'Examination & Others',null,null,null,null,N'0'),
(N'Student',N'Exam Result',N'exam_result_stu.png',null,null,N'/StudentWebview/Report_card.aspx?regid=#uid#',3,1,N'Examination & Others',null,null,null,null,N'0'),
(N'Student',N'Apply Leave',N'ac_aply_leave.png',null,null,N'/StudentWebview/apply-for-leave.aspx?regid=#uid#',5,0,N'Examination & Others',null,null,null,null,N'0');
Insert into [App_Dashboard_Setting] (UserType,Title,Image,Icon,Icon_Bg,Action,Secquence,Status,MenuType,Is_Restricted,Header_Image,Parent_Menu_Id,Menu_Id,isHostel) values 
(N'Student',N'Leave Status',N'ac_leavestatus.png',null,null,N'/StudentWebview/my-leave.aspx?regid=#uid#',6,0,N'Examination & Others',null,null,null,null,N'0'),
(N'Student',N'Certificate',N'certificate_stu.png',null,null,N'',10,1,N'Academic',null,null,null,null,N'0'),
(N'Student',N'Complaint',N'Complaint_stu.png',null,null,N'',7,1,N'Examination & Others',null,null,null,null,N'0'),
(N'Student',N'Message To Admin',N'message_question.png',null,null,N'/Student_Profile/webview/message.aspx?regid=#uid#',24,0,N'Academic',null,null,null,null,N'0'),
(N'Student',N'Monthly Fee',N'feepay_stu.png',null,null,N'/Student_Profile/webview/Student_Monthly_Payment.aspx?regid=#uid#',26,1,N'Finance',null,null,null,null,N'0'),
(N'Student',N'My Transaction',N'fi_mytransactn.png',null,null,N'',29,0,N'Finance',null,null,null,null,N'0'),
(N'Student',N'My Dues',N'fee_dues_stu.png',null,null,N'/Student_Profile/webview/Student_Fee_dues_details.aspx?regid=#uid#',27,1,N'Finance',null,null,null,null,N'0'),
(N'teacher',N'My Profile',N'profile.png',N'',N'',N'',1,0,N'Academic',null,null,null,null,N'0'),
(N'teacher',N'Make Class Attendance',N'attandance_tec.png',N'',N'',N'/_adminTutorProf/Class_wise_make_attendance.aspx?regid=#uid#',2,1,N'Academic',null,null,null,null,N'0'),
(N'teacher',N'View Class Attendance',N'attendance_stu.png',N'',N'',N'/_adminTutorProf/Class_wise_View_attendance.aspx?regid=#uid#',3,1,N'Academic',null,null,null,null,N'0');
Insert into [App_Dashboard_Setting] (UserType,Title,Image,Icon,Icon_Bg,Action,Secquence,Status,MenuType,Is_Restricted,Header_Image,Parent_Menu_Id,Menu_Id,isHostel) values 
(N'teacher',N'Assign Home Work',N'home_work_tec.png',N'',N'',N'/app_nextSof/Upload_Home_Work.html?regid=#uid#',4,1,N'Academic',null,null,null,null,N'0'),
(N'teacher',N'View Home Work',N'homework_stu.png',N'',N'',N'/app_nextSof/HomeWork_List_Teacher.html?regid=#uid#',5,1,N'Academic',null,null,null,null,N'0'),
(N'teacher',N'Upload Study Material',N'add_std_mtrl_tec.png',N'',N'',N'',6,1,N'Academic',null,null,null,null,N'0'),
(N'teacher',N'View Study Material',N'studymaterial_stu.png',N'',N'',N'',7,1,N'Academic',null,null,null,null,N'0'),
(N'teacher',N'View E-Book',N'ebook_stu.png',N'',N'',N'',8,1,N'Academic',null,null,null,null,N'0'),
(N'teacher',N'Student Doubt',N'askdoubt_stu.png',N'',N'',N'/app_nextSof/my-doubts-teacher.html?regid=#uid#',9,1,N'Academic',null,null,null,null,N'0'),
(N'teacher',N'Update Class Activity',N'Update_Class_Activity_tec.png',N'',N'',N'/_adminTutorProf/Update_Class_Activity.aspx?regid=#uid#',10,1,N'Academic',null,null,null,null,N'0'),
(N'teacher',N'Class Routine',N'classroutine_stu.png',N'',N'',N'/app_nextSof/p_routine.html',12,1,N'Academic',null,null,null,null,N'0'),
(N'teacher',N'My Routine',N'classroutine_stu.png',N'',N'',N'/app_nextSof/My_Routine_Teacher.html?regid=#uid#',12,1,N'Academic',null,null,null,null,N'0'),
(N'teacher',N'My Subject',N'sub_list_tec.png',N'',N'',N'',13,1,N'Academic',null,null,null,null,N'0');
Insert into [App_Dashboard_Setting] (UserType,Title,Image,Icon,Icon_Bg,Action,Secquence,Status,MenuType,Is_Restricted,Header_Image,Parent_Menu_Id,Menu_Id,isHostel) values 
(N'teacher',N'My Attendance',N'attendance_stu.png',N'',N'',N'/dp/teacher_attendance?regid=#uid#',34,1,N'HR',null,null,null,null,N'0'),
(N'teacher',N'Message to Student',N'message_stu.png',N'',N'',N'',15,1,N'Interaction',null,null,null,null,N'0'),
(N'teacher',N'View Sent Messages',N'view_msg_tec.png',N'',N'',N'',16,1,N'Interaction',null,null,null,null,N'0'),
(N'teacher',N'School Notice',N'notice_stu.png',N'',N'',N'/dp/teacher-notice-board?regid=#uid#',17,0,N'Interaction',null,null,null,null,N'0'),
(N'teacher',N'School Calendar',N'clander_stu.png',N'',N'',N'/_adminTutorProf/School_Calendar.aspx?regid=#uid#',18,1,N'Academic',null,null,null,null,N'0'),
(N'teacher',N'Student Info',N'student_info.png',N'',N'',N'',22,1,N'Interaction',null,null,null,null,N'0'),
(N'teacher',N'Student Dues List',N'fee_dues_stu.png',N'',N'',N'/_adminTutorProf/dues-monthwise.aspx?regid=#uid#',23,0,N'Academic',null,null,null,null,N'0'),
(N'teacher',N'Apply Leave',N'apply_leave_stu.png',N'',N'',N'/_adminTutorProf/apply-for-leave.aspx?regid=#uid#',24,1,N'HR',null,null,null,null,N'0'),
(N'teacher',N'Leave Status',N'leave_status_stu.png',N'',N'',N'/_adminTutorProf/my-leave.aspx?regid=#uid#',25,1,N'HR',null,null,null,null,N'0'),
(N'teacher',N'Student Leave Request',N'leave_status_stu.png',N'',N'',N'/_adminTutorProf/leave-request-from-student.aspx?regid=#uid#',26,1,N'HR',null,null,null,null,N'0');
Insert into [App_Dashboard_Setting] (UserType,Title,Image,Icon,Icon_Bg,Action,Secquence,Status,MenuType,Is_Restricted,Header_Image,Parent_Menu_Id,Menu_Id,isHostel) values 
(N'principal',N'Student',N'graduated.png',N'',N'',N'',1,1,null,null,null,null,null,N'0'),
(N'principal',N'Teacher',N'classroom.png',N'',N'',N'',2,1,null,null,null,null,null,N'0'),
(N'principal',N'Financial',N'stock.png',N'',N'',N'',3,1,null,null,null,null,null,N'0'),
(N'principal',N'Administrative',N'administrator.png',N'',N'',N'',4,1,null,null,null,null,null,N'0'),
(N'Coordinator',N'Dashboard',N'profile.png',N'',N'',N'',1,1,null,null,null,null,null,N'0'),
(N'Coordinator',N'LMS',N'lms.png',N'',N'',N'',2,1,null,null,null,null,null,N'0'),
(N'Coordinator',N'Home Work',N'home_work.png',N'',N'',N'',3,1,null,null,null,null,null,N'0'),
(N'Coordinator',N'Study Material',N'study_matrl.png',N'',N'',N'',4,1,null,null,null,null,null,N'0'),
(N'Coordinator',N'Time Table',N'timetable.png',N'',N'',N'/CoodinatorProfile/Class_Routing_student.aspx?regid=#uid#',5,1,null,null,null,null,null,N'0'),
(N'Coordinator',N'Examination',N'exam_sched.png',N'',N'',N'#',6,1,null,null,null,null,null,N'0');
Insert into [App_Dashboard_Setting] (UserType,Title,Image,Icon,Icon_Bg,Action,Secquence,Status,MenuType,Is_Restricted,Header_Image,Parent_Menu_Id,Menu_Id,isHostel) values 
(N'Coordinator',N'Account',N'salary.png',N'',N'',N'#',7,1,null,null,null,null,null,N'0'),
(N'Coordinator',N'Fee',N'fee_pay.png',N'',N'',N'',8,1,null,null,null,null,null,N'0'),
(N'Coordinator',N'Attendance',N'attandance.png',N'',N'',N'',9,1,null,null,null,null,null,N'0'),
(N'Coordinator',N'Library',N'library.png',N'',N'',N'#',10,1,null,null,null,null,null,N'0'),
(N'Coordinator',N'Leave',N'leave.png',N'',N'',N'#',11,1,null,null,null,null,null,N'0'),
(N'Coordinator',N'Calendar',N'calenda.png',N'',N'',N'/CoodinatorProfile/School_Calendar.aspx?regid=#uid#',12,1,null,null,null,null,null,N'0'),
(N'Coordinator',N'PTM',N'meeting_parent.png',N'',N'',N'#',13,1,null,null,null,null,null,N'0'),
(N'Coordinator',N'Student Profile',N'profile.png',N'',N'',N'',14,1,null,null,null,null,null,N'0'),
(N'Coordinator',N'Notice',N'notice_board.png',N'',N'',N'/CoodinatorProfile/Notice_Board.aspx?rigid=#uid#',15,1,null,null,null,null,null,N'0'),
(N'Coordinator',N'Message',N'message.png',N'',N'',N'/CoodinatorProfile/Message.aspx?regid=#uid#',16,1,null,null,null,null,null,N'0');
Insert into [App_Dashboard_Setting] (UserType,Title,Image,Icon,Icon_Bg,Action,Secquence,Status,MenuType,Is_Restricted,Header_Image,Parent_Menu_Id,Menu_Id,isHostel) values 
(N'Coordinator',N'Helpline',N'customer_support.png',N'',N'',N'',17,1,null,null,null,null,null,N'0'),
(N'Coordinator',N'Marks Entry',N'report_card.png',N'',N'',N'',18,1,null,null,null,null,null,N'0'),
(N'teacher',N'Marks Entry',N'MarksEntry_tec.png',N'',N'',N'/_adminTutorProf/marksentry_per.aspx?rigid=#uid#',27,1,N'Exam',null,null,null,null,N'0'),
(N'teacher',N'Personality Traits',N'PersonalityTraits_tec.png',N'',N'',N'/_adminTutorProf/MarkasEnteryPersonalityTraits_per.aspx?rigid=#uid#',28,1,N'Exam',null,null,null,null,N'0'),
(N'teacher',N'Commentary Remarks',N'CommentaryRemarks_tec.png',N'',N'',N'/_adminTutorProf/CommentaryRemarksEntry_per.aspx?rigid=#uid#',29,1,N'Exam',null,null,null,null,N'0'),
(N'teacher',N'Term Wise Attendance',N'attendance_stu.png',N'',N'',N'/_adminTutorProf/SetAttendanceStudentTermWise.aspx?rigid=#uid#',30,1,N'Exam',null,null,null,null,N'0'),
(N'teacher',N'Upload E-Book',N'e_book_tec.png',N'',N'',N'',7,1,N'Academic',null,null,null,null,N'0'),
(N'teacher',N'Send Notice',N'send_notic_tec.png',null,null,null,32,1,N'Interaction',null,null,null,null,N'0'),
(N'teacher',N'View Sent Notice',N'view_send_notice_tec.png',null,null,null,33,1,N'Interaction',null,null,null,null,N'0'),
(N'Student',N'Teacher Log Book',N'Teacher_Log_Book_stu.png',null,null,N'/app_nextSof/log_book_student.html?regid=#uid#',8,1,N'Examination & Others',null,null,null,null,N'0');
Insert into [App_Dashboard_Setting] (UserType,Title,Image,Icon,Icon_Bg,Action,Secquence,Status,MenuType,Is_Restricted,Header_Image,Parent_Menu_Id,Menu_Id,isHostel) values 
(N'Student',N'Book Taken History',N'BookTakenHistory_stu.png',null,null,N'/Student_Profile/webview/Book_Taken_History.aspx?regid=#uid#',9,1,N'Examination & Others',null,null,null,null,N'0'),
(N'teacher',N'Salary Slip',N'salary_tec.png',null,null,N'/_adminETutorProf/webview/salary_slip.aspx?regid=#uid#',35,0,N'HR',null,null,null,null,N'0'),
(N'teacher',N'Add Log Book',N'logbook_tec.png',null,null,N'/_adminTutorProf/upload-log-book.aspx?regid=#uid#',35,1,N'Academic',null,null,null,null,N'0'),
(N'teacher',N'View Log Book',N'logbookview_tec.png',null,null,N'/_adminTutorProf/view-log-book.aspx?regid=#uid#',36,1,N'Academic',null,null,null,null,N'0'),
(N'Student',N'Message',N'message_stu.png',null,null,N'/app_nextSof/student-msg-list.html?regid=#uid#',3,1,N'Notice/Circular/Events',null,null,null,null,N'0'),
(N'Student',N'Wishes',N'Wishes_stu.png',null,null,null,4,1,N'Notice/Circular/Events',null,null,null,null,N'0'),
(N'Student',N'Others',N'application.png',null,null,null,6,0,N'Notice/Circular/Events',null,null,null,null,N'0'),
(N'Student',N'Apply Leave',N'apply_leave_stu.png',null,null,N'/StudentWebview/apply-for-leave.aspx?regid=#uid#',8,1,N'Academic',null,null,null,null,N'0'),
(N'Student',N'Leave Status',N'leave_status_stu.png',null,null,N'/StudentWebview/my-leave.aspx?regid=#uid#',9,1,N'Academic',null,null,null,null,N'0'),
(N'Student',N'Payment History',N'fee_pay_history_stu.png',null,null,N'/Student_Profile/webview/My_Transaction.aspx?regid=#uid#',28,1,N'Finance',null,null,null,null,N'0');
Insert into [App_Dashboard_Setting] (UserType,Title,Image,Icon,Icon_Bg,Action,Secquence,Status,MenuType,Is_Restricted,Header_Image,Parent_Menu_Id,Menu_Id,isHostel) values 
(N'Student',N'Admission/Annual Fee',N'fi_payonline.png',null,null,null,25,0,N'Finance',null,null,null,null,N'0'),
(N'Student',N'Apply Hostel Out Pass',N'report_card.png',null,null,N'/Studentprofile/webview/Hostel_Out_Pass_Request.aspx?regid=#uid#',10,0,N'Examination & Others',null,null,null,null,N'1'),
(N'Student',N'View Apply Hostel Pass',N'approval.png',null,null,N'/Studentprofile/webview/Hostel_Out_Pass_Request_list.aspx?regid=#uid#',11,0,N'Examination & Others',null,null,null,null,N'1'),
(N'Student',N'Online Test',N'ac_aply_leave.png',null,null,N'/Student_Profile/webview/Online_Exam_List.aspx?regid=#uid#',12,0,N'Examination & Others',null,null,null,null,N'0'),
(N'Student',N'View Test Result',N'e_exam_result.png',null,null,N'/Student_Profile/webview/OnlineMyResult.aspx?regid=#uid#',13,0,N'Examination & Others',null,null,null,null,N'0'),
(N'teacher',N'View Class Activity',N'exam_result_stu.png',null,null,N'/_adminTutorProf/View_Class_Activity.aspx?regid=#uid#',11,1,N'Academic',null,null,null,null,N'0'),
(N'Student',N'Coupon',N'fi_payonline.png',null,null,N'/Student_Profile/webview/coupon-list.aspx?regid=#uid#',0,0,N'Finance',null,null,null,null,N'0'),
(N'teacher',N'Online Test Result',N'exam_result_stu.png',N'',N'',N'/_adminTutorProf/Online_Test_Student_Taken_History.aspx?regid=#uid#',37,0,N'Exam',null,null,null,null,N'0'),
(N'Student',N'Syllabus',N'syllabus_stu.png',null,null,N'/Student_Profile/webview/syllabus.aspx?regid=#uid#',2,1,N'Academic',null,null,null,null,N'0'),
(N'teacher',N'Upload Question-Online Test',N'upload_que_tec.png',null,null,N'https://app.nextronsoft.com/onlineTest/Upload_Question?regid=#uid#',38,0,N'Exam',null,null,null,null,N'0');
Insert into [App_Dashboard_Setting] (UserType,Title,Image,Icon,Icon_Bg,Action,Secquence,Status,MenuType,Is_Restricted,Header_Image,Parent_Menu_Id,Menu_Id,isHostel) values 
(N'teacher',N'View Question-Online Test',N'View_que_tec.png',null,null,N'https://app.nextronsoft.com/onlineTest/view_uploaded_question?regid=#uid#',39,0,N'Exam',null,null,null,null,N'0'),
(N'teacher',N'Syllabus',N'syllabus_stu.png',null,null,N'/_adminETutorProf/webview/syllabus.aspx?regid=#uid#',37,1,N'Academic',null,null,null,null,N'0'),
(N'Student',N'Admit Card',N'admit_card_stu.png',null,null,N'/StudentWebview/admit-card.aspx?regid=#uid#',2,1,N'Examination & Others',null,null,null,null,N'0');

");
                        list.Add(@"delete from EmployeeAppDashboard");
                        list.Add(@"Insert into [EmployeeAppDashboard] (UserType,Title,Image,Action,Sequence,Menu_Id,Parent_Menu_Id,Header_Image,Is_Restricted) values 
(N'Admin',N'Attendance',N'staff_attendance.png',N'/dp/teacher_attendance?regid=#uid#',N'1',N'1',N'0',N'',N'0'),
(N'Admin',N'Notice',N'notice_board.png',N'/app_nextSof/teacher-notice-list.html?regid=#uid#',N'2',N'2',N'0',N'',N'0'),
(N'Admin',N'Salary Slip',N'payent_history.png',N'InstructorProfile/webview/salary_slip.aspx?regid=#uid#',N'3',N'3',N'0',N'',N'1'),
(N'Admin',N'Complain',N'complain.png',N'Submenu',N'4',N'4',N'0',N'call_center.png',N'1'),
(N'Admin',N'Create Complain',N'call_center.png',N'InstructorProfile/webview/make-complain.aspx?regid=#uid#',N'1',N'5',N'4',N'',N'1'),
(N'Admin',N'Complain List',N'call_center.png',N'InstructorProfile/webview/complain-list.aspx?regid=#uid#',N'2',N'6',N'4',N'call_center.png',N'1'),
(N'Admin',N'Leave',N'notice_board.png',N'Submenu',N'4',N'7',N'0',N'call_center.png',N'0'),
(N'admin',N'Apply Leave',N'call_center.png',N'/_adminTutorProf/apply-for-leave.aspx?regid=#uid#',N'1',N'8',N'7',null,N'0'),
(N'admin',N'Leave Status',N'call_center.png',N'/_adminTutorProf/my-leave.aspx?regid=#uid#',N'2',N'9',N'7',null,N'0');");
                        list.Add(@"delete  from PrincipalAppDashboard");
                        list.Add(@"Insert into [PrincipalAppDashboard] (Name,Icon,Menu_Id,Parent_Menu_Id,Action,Sequance,Header_Image,Is_Restricted,Status,UserType) values 
(N'Information',N'student_info.png',N'1',N'0',N'Submenu',1,N'documents.png',null,1,null),
(N'Bus',N'school_bus.png',N'2',N'0',N'Submenu',2,N'bus_tracking_image.png',null,0,null),
(N'Examination',N'examination.png',N'3',N'0',N'Submenu',3,N'examination.png',null,1,null),
(N'LMS',N'ic_new_lms.png',N'4',N'0',N'Submenu',4,N'homework_image.png',null,1,null),
(N'Attendance',N'attendance.png',N'5',N'0',N'Submenu',5,N'leave_dashbord.png',null,1,null),
(N'Library',N'ic_new_library.png',N'6',N'0',N'Submenu',12,N'library_icon.png',null,1,null),
(N'Routine',N'ic_new_aclass_actv.png',N'7',N'0',N'Submenu',7,N'information_student.png',null,0,null),
(N'Leave',N'leave_img_sec.png',N'8',N'0',N'Submenu',12,N'leave_dashbord.png',null,1,null),
(N'Complain',N'complain.png',N'9',N'0',N'Submenu',9,N'finacial.png',null,0,null),
(N'Certificate',N'certificate_icon.png',N'10',N'0',N'Submenu',10,N'img_report.png',null,0,null);
Insert into [PrincipalAppDashboard] (Name,Icon,Menu_Id,Parent_Menu_Id,Action,Sequance,Header_Image,Is_Restricted,Status,UserType) values 
(N'Online Registration',N'online_test_2.png',N'11',N'0',N'Submenu',11,N'information_student.png',null,1,null),
(N'Finance',N'budget.png',N'12',N'0',N'Submenu',12,N'fee_image.png',null,1,null),
(N'Student Info',N'student_info.png',N'13',N'1',N'Submenu',13,N'documents.png',null,1,null),
(N'Employee Info',N'attendance_app.png',N'14',N'1',N'Submenu',14,N'documents.png',null,1,null),
(N'Student Profile',N'student_info.png',N'15',N'13',N'/_adminDPprof/student-list.aspx',15,N'documents.png',null,1,null),
(N'Search Student',N'icons_search.png',N'16',N'13',N'custom',16,N'documents.png',null,1,null),
(N'Alumni Student',N'sub_list.png',N'17',N'13',N'Submenu',17,N'documents.png',null,1,null),
(N'Active/Inactive student',N'ebook.png',N'18',N'13',N'Submenu',18,N'documents.png',null,1,null),
(N'Class Wise Student Graph',N'syllabus_icon.png',N'19',N'13',N'Submenu',19,N'documents.png',null,0,null),
(N'Birthday',N'birthday.png',N'20',N'13',N'/_adminDPprof/Bithday-list.aspx',20,N'documents.png',null,1,null);
Insert into [PrincipalAppDashboard] (Name,Icon,Menu_Id,Parent_Menu_Id,Action,Sequance,Header_Image,Is_Restricted,Status,UserType) values 
(N'Employee Profile',N'student_info.png',N'21',N'14',N'/_adminDPprof/Employeelist.aspx',21,N'documents.png',null,1,null),
(N'Search Employee',N'icons_search.png',N'22',N'14',N'#',22,N'documents.png',null,0,null),
(N'Active/Inactive Employee',N'sub_list.png',N'23',N'14',N'/_adminDPprof/Active_inactive_employee_list.aspx',23,N'documents.png',null,1,null),
(N'Class Teacher''s List',N'ebook.png',N'24',N'14',N'/_adminDPprof/class-teacher-list.aspx',24,N'documents.png',null,1,null),
(N'Subject Teacher',N'syllabus_icon.png',N'25',N'14',N'/_adminDPprof/Subject_Teacher.aspx',25,N'documents.png',null,1,null),
(N'Birthday',N'birthday.png',N'26',N'14',N'/_adminDPprof/Bithday-list-Employee.aspx',26,N'documents.png',null,1,null),
(N'Create Employee',N'student_info.png',N'27',N'23',null,27,N'documents.png',null,1,null),
(N'Employee List',N'icons_search.png',N'28',N'23',N'#',28,N'documents.png',null,1,null),
(N'Create Alumni',N'attendance_app.png',N'29',N'17',N'/_adminDPprof/make-alumni.aspx',29,N'documents.png',null,1,null),
(N'Alumni List',N'class_list.png',N'30',N'17',N'/_adminDPprof/alumni-student.aspx',30,N'documents.png',null,1,null);
Insert into [PrincipalAppDashboard] (Name,Icon,Menu_Id,Parent_Menu_Id,Action,Sequance,Header_Image,Is_Restricted,Status,UserType) values 
(N'Create-Active/Inactive',N'marks_entry.png',N'31',N'18',N'/_adminDPprof/ActiveStudentList.aspx',31,N'documents.png',null,1,null),
(N'Active/Inactive List',N'process_marks.png',N'32',N'18',N'/_adminDPprof/Active_Inactive_Student_List.aspx',32,N'documents.png',null,1,null),
(N'Day Boarding',N'marks_entry.png',N'33',N'19',N'#',33,N'documents.png',null,1,null),
(N'Day Boarding With Lunch',N'process_marks.png',N'34',N'19',N'#',34,N'documents.png',null,1,null),
(N'Hostel',N'report_card.png',N'35',N'19',N'#',35,N'documents.png',null,1,null),
(N'Student Wise Bus List',N'assignment_center.png',N'36',N'2',N'/_adminDPprof/Student_Transport_Mapped_List.aspx',36,N'bus_tracking_image.png',null,1,null),
(N'Bus List',N'report_card.png',N'37',N'2',N'/_adminDPprof/bus-list.aspx',37,N'bus_tracking_image.png',null,1,null),
(N'Live Tracking',N'bus_tracking.png',N'38',N'2',N'/_adminDPprof/bus-location.aspx',38,N'bus_tracking_image.png',null,1,null),
(N'Student Examination',N'img_report.png',N'39',N'3',N'Submenu',39,N'examination.png',null,1,null),
(N'Teacher Examination',N'report_card.png',N'40',N'3',N'Submenu',40,N'examination.png',null,1,null);
Insert into [PrincipalAppDashboard] (Name,Icon,Menu_Id,Parent_Menu_Id,Action,Sequance,Header_Image,Is_Restricted,Status,UserType) values 
(N'Online Test Report',N'img_report.png',N'41',N'39',N'#',41,N'examination.png',null,0,null),
(N'Report Card',N'report_card.png',N'42',N'39',N'/_adminDPprof/exam-report-card.aspx',42,N'examination.png',null,1,null),
(N'Board Paper',N'ebook.png',N'43',N'39',N'#',43,N'examination.png',null,0,null),
(N'Practice Set Report',N'marks_entry.png',N'44',N'39',N'#',44,N'examination.png',null,0,null),
(N'Surprise Test Report',N'surprise_qust.png',N'45',N'39',N'#',45,N'examination.png',null,0,null),
(N'Exam Routine',N'self_time_table.png',N'46',N'39',N'/_adminDPprof/Exam_Routine_List.aspx?rigid=#uid#',46,N'examination.png',null,1,null),
(N'Non-appearing student',N'task_duty.png',N'47',N'39',N'#',47,N'examination.png',null,0,null),
(N'Exam Routine',N'img_report.png',N'48',N'40',null,48,N'examination.png',null,0,null),
(N'Marks Entry',N'report_card.png',N'49',N'40',N'Submenu',49,N'examination.png',null,1,null),
(N'View Marks Entry',N'ebook.png',N'50',N'40',N'',50,N'examination.png',null,0,null);
Insert into [PrincipalAppDashboard] (Name,Icon,Menu_Id,Parent_Menu_Id,Action,Sequance,Header_Image,Is_Restricted,Status,UserType) values 
(N'Obtain Marks Entry',N'marks_entry.png',N'51',N'49',N'/_adminDPprof/marksentry_per.aspx',51,N'examination.png',null,1,null),
(N'Personality Traits',N'process_marks.png',N'52',N'49',N'/_adminDPprof/MarkasEnteryPersonalityTraits_per.aspx',52,N'examination.png',null,1,null),
(N'Commentary Remarks',N'report_card.png',N'53',N'49',N'/_adminDPprof/CommentaryRemarksEntry_per.aspx?rigid=#uid#',53,N'examination.png',null,1,null),
(N'Term Wise Attendance',N'attendance.png',N'54',N'49',N'/_adminDPprof/SetAttendanceStudentTermWise.aspx?rigid=#uid#',54,N'examination.png',null,1,null),
(N'Home work ',N'home_work.png',N'55',N'4',N'Submenu',55,N'homework_image.png',null,1,null),
(N'Study Material ',N'study_material_2.png',N'56',N'4',N'Submenu',56,N'homework_image.png',null,1,null),
(N'E-Book',N'timetable.png',N'57',N'4',N'Submenu',57,N'homework_image.png',null,1,null),
(N'Class Wise Subject List',N'e_book.png',N'58',N'4',N'/_adminDPprof/Subjectlistclasswise.aspx',58,N'homework_image.png',null,1,null),
(N'Syllabus',N'home_work.png',N'59',N'4',N'_adminETutorProf/webview/syllabus.aspx?regid=#uid#',59,N'homework_image.png',null,0,null),
(N'Live Class',N'study_material_2.png',N'60',N'4',null,60,N'homework_image.png',null,0,null);
Insert into [PrincipalAppDashboard] (Name,Icon,Menu_Id,Parent_Menu_Id,Action,Sequance,Header_Image,Is_Restricted,Status,UserType) values 
(N'Ask Doubt',N'timetable.png',N'61',N'4',N'/app_nextSof/my-doubts-teacher.html?regid=#uid#',61,N'',null,1,null),
(N'Time Table',N'e_book.png',N'62',N'4',N'Submenu',62,N'homework_image.png',null,0,null),
(N'Upload Homework',N'ebook.png',N'65',N'55',N'/app_nextSof/Upload_Home_Work.html?regid=#uid#',65,N'homework_image.png',null,1,null),
(N'View Homework',N'report_card.png',N'66',N'55',N'/app_nextSof/HomeWork_List_Teacher.html?regid=#uid#',66,N'homework_image.png',null,1,null),
(N'Upload Study content',N'home_work.png',N'67',N'56',N'custom',67,N'homework_image.png',null,1,null),
(N'Uploaded Content',N'study_material_2.png',N'68',N'56',N'/_adminDPprof/study-material.aspx?rigid=#uid#',68,N'homework_image.png',null,1,null),
(N'Content by Teacher',N'timetable.png',N'69',N'56',N'/_adminDPprof/study-material.aspx',69,N'homework_image.png',null,1,null),
(N'Class Wise Content',N'e_book.png',N'70',N'56',N'/_adminDPprof/View_Class_Wise_Study_Material.aspx',70,N'homework_image.png',null,1,null),
(N'Teacher Wise eBook',N'home_work.png',N'71',N'57',null,71,N'homework_image.png',null,0,null),
(N'Attemped Student eBook',N'study_material_2.png',N'72',N'57',null,72,N'homework_image.png',null,0,null);
Insert into [PrincipalAppDashboard] (Name,Icon,Menu_Id,Parent_Menu_Id,Action,Sequance,Header_Image,Is_Restricted,Status,UserType) values 
(N'Upload eBook',N'home_work.png',N'73',N'57',N'custom',73,N'homework_image.png',null,1,null),
(N'View eBook',N'study_material_2.png',N'74',N'57',N'/_adminDPprof/e-book-details.aspx',74,N'homework_image.png',null,1,null),
(N'Add Syllabus',N'img_report.png',N'75',N'59',N'/_adminDPprof/add-syllabus.aspx?regid=#uid#',75,N'homework_image.png',null,0,null),
(N'View Syllabus',N'report_card.png',N'76',N'59',N'/_adminDPprof/view-self-added-syllabus.aspx?regid=#uid#',76,N'homework_image.png',null,0,null),
(N'Upload Syllabus Status',N'img_report.png',N'77',N'76',null,77,N'homework_image.png',null,1,null),
(N'Syllabus Status Class Wise',N'report_card.png',N'78',N'76',null,78,N'homework_image.png',null,1,null),
(N'Student Doubt Class Wise',N'img_report.png',N'79',N'61',null,79,N'homework_image.png',null,1,null),
(N'Teacher Reply Class Wise',N'report_card.png',N'80',N'61',null,80,N'homework_image.png',null,1,null),
(N'Student Time Table',N'img_report.png',N'81',N'62',N'/app_nextSof/p_routine.html',81,N'homework_image.png',null,1,null),
(N'Teacher Time Table',N'report_card.png',N'82',N'62',N'/_adminDPprof/teacher-class-routing.aspx',82,N'homework_image.png',null,1,null);
Insert into [PrincipalAppDashboard] (Name,Icon,Menu_Id,Parent_Menu_Id,Action,Sequance,Header_Image,Is_Restricted,Status,UserType) values 
(N'Student Attendance',N'attendance_app.png',N'83',N'5',N'Submenu',83,N'leave_dashbord.png',null,1,null),
(N'Employee Attendance',N'biometric_recognition.png',N'84',N'5',N'Submenu',84,N'leave_dashbord.png',null,1,null),
(N'Student Wise Attendance',N'attendance_app.png',N'85',N'83',N'/_adminDPprof/Student_Attendance_class_wise.aspx',4,N'leave_dashbord.png',null,1,null),
(N'Monthly Wise Attendance',N'biometric_recognition.png',N'86',N'83',N'/_adminDPprof/Attendance_Summary_class_wise.aspx',5,N'leave_dashbord.png',null,1,null),
(N'Employee Wise',N'attendance_app.png',N'87',N'84',N'/_adminDPprof/individual-daily-attendance-report-teaching.aspx',87,N'leave_dashbord.png',null,1,null),
(N'Employee Report',N'biometric_recognition.png',N'88',N'84',N'/Visitor/monthlyAttendanceChart',88,N'leave_dashbord.png',null,1,null),
(N'Available Book',N'abhilable_book.png',N'89',N'6',N'#',89,N'library_icon.png',null,1,null),
(N'Apply Book',N'apply_book.png',N'90',N'6',N'Submenu',90,N'library_icon.png',null,1,null),
(N'Issue Book',N'e_book.png',N'91',N'6',N'Submenu',91,N'library_icon.png',null,1,null),
(N'Pending Book',N'pending_book.png',N'92',N'6',N'Submenu',92,N'library_icon.png',null,1,null);
Insert into [PrincipalAppDashboard] (Name,Icon,Menu_Id,Parent_Menu_Id,Action,Sequance,Header_Image,Is_Restricted,Status,UserType) values 
(N'National Library',N'add_home_work.png',N'93',N'6',N'custom',93,N'library_icon.png',null,1,null),
(N'Student Apply Book',N'abhilable_book.png',N'94',N'90',N'#',94,N'library_icon.png',null,1,null),
(N'Employee Apply Book',N'apply_book.png',N'95',N'90',N'#',95,N'library_icon.png',null,1,null),
(N'Student Issue Book',N'abhilable_book.png',N'96',N'91',N'#',96,N'library_icon.png',null,1,null),
(N'Employee Issue Book',N'apply_book.png',N'97',N'91',N'#',97,N'library_icon.png',null,1,null),
(N'Student Pending Book',N'abhilable_book.png',N'98',N'92',N'#',98,N'library_icon.png',null,1,null),
(N'Employee Pending Book',N'apply_book.png',N'99',N'92',N'#',99,N'library_icon.png',null,1,null),
(N'Student Routine',N'online_test_2.png',N'100',N'7',N'/dp/p_routine?regid=t1',100,N'information_student.png',null,1,null),
(N'Subject Wise Report',N'pending_book.png',N'101',N'7',N'#',101,N'information_student.png',null,0,null),
(N'Teacher Routine',N'approval.png',N'102',N'7',N'/_adminDPprof/teacher-class-routing.aspx',102,N'information_student.png',null,1,null);
Insert into [PrincipalAppDashboard] (Name,Icon,Menu_Id,Parent_Menu_Id,Action,Sequance,Header_Image,Is_Restricted,Status,UserType) values 
(N'Total Class Wise Routine',N'pending_book.png',N'103',N'7',N'#',103,N'information_student.png',null,0,null),
(N'Students Leave',N'attendance_app.png',N'104',N'8',N'Submenu',104,N'information_student.png',null,1,null),
(N'Employee Leave',N'biometric_recognition.png',N'105',N'8',N'Submenu',105,N'information_student.png',null,1,null),
(N'My Leave',N'admission.png',N'106',N'8',N'Submenu',106,N'information_student.png',null,1,null),
(N'Leave Approval',N'attendance_app.png',N'107',N'104',N'/_adminDPprof/leave-request-from-student.aspx?regid=#uid#',107,N'information_student.png',null,1,null),
(N'Leave Approval List',N'biometric_recognition.png',N'108',N'104',N'/_adminDPprof/leave-request-from-student.aspx?regid=#uid#',108,N'information_student.png',null,1,null),
(N'Leave Approval',N'attendance_app.png',N'109',N'105',N'/_adminDPprof/leave-request-from-teacher.aspx?regid=#uid#',109,N'information_student.png',null,1,null),
(N'Leave Approval List',N'biometric_recognition.png',N'110',N'105',N'/_adminDPprof/leave-request-from-teacher.aspx?regid=#uid#',110,N'information_student.png',null,1,null),
(N'Apply Leave',N'attendance_app.png',N'111',N'106',N'/_adminTutorProf/apply-for-leave.aspx?regid=#uid#',111,N'information_student.png',null,1,null),
(N'Leave Status',N'biometric_recognition.png',N'112',N'106',N'/_adminTutorProf/my-leave.aspx?regid=#uid#',112,N'information_student.png',null,1,null);
Insert into [PrincipalAppDashboard] (Name,Icon,Menu_Id,Parent_Menu_Id,Action,Sequance,Header_Image,Is_Restricted,Status,UserType) values 
(N'Apply Complain',N'pending_book_list.png',N'113',N'9',N'Submenu',113,N'finacial.png',null,1,null),
(N'Students Complain',N'approval.png',N'114',N'9',N'/_adminDPprof/Pending_Complain_List.aspx',114,N'finacial.png',null,1,null),
(N'Employee Complain',N'lms_ask_doubt.png',N'115',N'9',null,115,N'finacial.png',null,1,null),
(N'Add Complain',N'pending_book_list.png',N'116',N'113',null,116,N'finacial.png',null,1,null),
(N'Complain Status',N'approval.png',N'117',N'113',null,117,N'finacial.png',null,1,null),
(N'Applied TC Certificate',N'pending_book_list.png',N'118',N'10',N'/_adminDPprof/Pending_Applied_TC_Certificate.aspx?regid=#uid#',118,N'cirtificate_icon.png',null,1,null),
(N'Applied Character Certificate',N'pending_book_list.png',N'119',N'10',N'/_adminDPprof/Pending_Applied_Character_Certificate.aspx?regid=#uid#',120,N'cirtificate_icon.png',null,1,null),
(N'Character Certificate Status',N'approval.png',N'120',N'10',N'/_adminDPprof/Approved_Character_Certificate.aspx',121,N'cirtificate_icon.png',null,1,null),
(N'TC Certificate Status',N'approval.png',N'121',N'10',N'/_adminDPprof/Approved_TC_Certificate.aspx',119,N'cirtificate_icon.png',null,1,null),
(N'Applied Online Registration',N'online_test_2.png',N'122',N'11',N'Principle_profile/online-registration.aspx',122,N'information_student.png',null,1,null);
Insert into [PrincipalAppDashboard] (Name,Icon,Menu_Id,Parent_Menu_Id,Action,Sequance,Header_Image,Is_Restricted,Status,UserType) values 
(N'Pending Online Registration',N'pending_book.png',N'123',N'11',N'Principle_profile/online-registration.aspx',123,N'information_student.png',null,0,null),
(N'Approved Registration',N'approval.png',N'124',N'11',N'Principle_profile/online-approved-reg.aspx',124,N'information_student.png',null,1,null),
(N'Admission',N'advance_aplly.png',N'125',N'12',N'Submenu',125,N'fee_image.png',null,0,null),
(N'Annual Fee',N'slip.png',N'126',N'12',N'Submenu',126,N'fee_image.png',null,0,null),
(N'Monthly Fee',N'due_date.png',N'127',N'12',N'Submenu',127,N'fee_image.png',null,0,null),
(N'Other Fee',N'budget.png',N'128',N'12',N'Submenu',128,N'fee_image.png',null,0,null),
(N'Transpotaion Fee',N'advance_apply.png',N'129',N'12',N'Submenu',129,N'fee_image.png',null,0,null),
(N'Dues List',N'cost.png',N'130',N'12',N'Submenu',130,N'fee_image.png',null,0,null),
(N'Discount Report',N'fee_status.png',N'131',N'12',N'Submenu',131,N'fee_image.png',null,0,null),
(N'Fine Report',N'fee_payment2.png',N'132',N'12',N'Submenu',132,N'fee_image.png',null,0,null);
Insert into [PrincipalAppDashboard] (Name,Icon,Menu_Id,Parent_Menu_Id,Action,Sequance,Header_Image,Is_Restricted,Status,UserType) values 
(N'Day End Report',N'license.png',N'133',N'12',N'Submenu',133,N'fee_image.png',null,0,null),
(N'Student Payment History',N'my_trans.png',N'134',N'12',null,134,N'fee_image.png',null,0,null),
(N'Today Collection',N'advance_aplly.png',N'135',N'125',N'/_adminDPprof/report-today-fees-collections-a.aspx',135,N'fee_image.png',null,1,null),
(N'Student Wise Collection',N'slip.png',N'136',N'125',N'#',136,N'fee_image.png',null,1,null),
(N'Today',N'advance_aplly.png',N'137',N'126',null,137,N'fee_image.png',null,1,null),
(N'Student Wise Collection ',N'slip.png',N'138',N'126',null,138,N'fee_image.png',null,1,null),
(N'Today Collection',N'advance_aplly.png',N'139',N'127',null,139,N'fee_image.png',null,1,null),
(N'Student & Head Collection',N'slip.png',N'140',N'127',null,140,N'fee_image.png',null,1,null),
(N'Month Wise Collection',N'due_date.png',N'141',N'127',null,141,N'fee_image.png',null,1,null),
(N'Online Monthly Fee Collection',N'budget.png',N'142',N'127',null,142,N'fee_image.png',null,1,null);
Insert into [PrincipalAppDashboard] (Name,Icon,Menu_Id,Parent_Menu_Id,Action,Sequance,Header_Image,Is_Restricted,Status,UserType) values 
(N'Today Collection',N'advance_aplly.png',N'143',N'128',null,143,N'fee_image.png',null,1,null),
(N'Student Wise Collection',N'slip.png',N'144',N'128',null,144,N'fee_image.png',null,1,null),
(N'Today Collection',N'advance_aplly.png',N'145',N'129',null,145,N'fee_image.png',null,1,null),
(N'Month Wise Collection',N'slip.png',N'146',N'129',null,146,N'fee_image.png',null,1,null),
(N'Admission Fee',N'advance_aplly.png',N'147',N'130',null,147,N'fee_image.png',null,1,null),
(N'Annual Fee',N'slip.png',N'148',N'130',null,148,N'fee_image.png',null,1,null),
(N'Monthly Fee',N'due_date.png',N'149',N'130',null,149,N'fee_image.png',null,1,null),
(N'All Dues',N'budget.png',N'150',N'130',null,150,N'fee_image.png',null,1,null),
(N'Admission Fee',N'advance_aplly.png',N'151',N'131',null,151,N'fee_image.png',null,1,null),
(N'Annual Fee',N'slip.png',N'152',N'131',null,152,N'fee_image.png',null,1,null);
Insert into [PrincipalAppDashboard] (Name,Icon,Menu_Id,Parent_Menu_Id,Action,Sequance,Header_Image,Is_Restricted,Status,UserType) values 
(N'Monthly Fee',N'due_date.png',N'153',N'131',null,153,N'fee_image.png',null,1,null),
(N'All Dues',N'budget.png',N'154',N'131',null,154,N'fee_image.png',null,1,null),
(N'Today Collection',N'advance_aplly.png',N'155',N'132',null,155,N'fee_image.png',null,1,null),
(N'Month Wise Collection',N'slip.png',N'156',N'132',null,156,N'fee_image.png',null,1,null),
(N'All Collection',N'advance_aplly.png',N'157',N'133',null,157,N'fee_image.png',null,1,null),
(N'Monthly Fee Collection',N'slip.png',N'158',N'133',null,158,N'fee_image.png',null,1,null),
(N'Annual Fee Collection',N'due_date.png',N'159',N'133',null,159,N'fee_image.png',null,1,null),
(N'Form Sales',N'budget.png',N'160',N'133',null,160,N'fee_image.png',null,1,null),
(N'Day End Summary',N'advance_apply.png',N'161',N'133',null,161,N'fee_image.png',null,1,null),
(N'User Wise',N'cost.png',N'162',N'133',null,162,N'fee_image.png',null,1,null);
Insert into [PrincipalAppDashboard] (Name,Icon,Menu_Id,Parent_Menu_Id,Action,Sequance,Header_Image,Is_Restricted,Status,UserType) values 
(N'Today Collections',N'collection.png',N'163',N'12',N'/_adminDPprof/today-collection.aspx',1,N'',null,1,null),
(N'Total Collections',N'money_bag.png',N'164',N'12',N'/_adminDPprof/today-collection.aspx',2,N'',null,0,null),
(N'Transportation Fee Report',N'vehicles.png',N'165',N'12',N'/_adminDPprof/transportation-fee-report.aspx',3,N'',null,1,null),
(N'Dues list',N'dues_list.png',N'166',N'12',N'Submenu',4,N'fee_image.png',null,1,null),
(N'Fees Collection Summary ',N'day_report.png',N'167',N'12',N'/_adminDPprof/fee-collection-report.aspx',5,N'',null,1,null),
(N'Expenses Report',N'budget.png',N'168',N'12',N'/_adminDPprof/expense-report.aspx',6,N'',null,1,null),
(N'Other Fee',N'cost.png',N'169',N'12',N'/_adminDPprof/Other_Fee_Collection_Report.aspx',7,N'',null,1,null),
(N'Admission Fee',N'admission.png',N'170',N'166',N'/_adminDPprof/admission-fee-collection-report.aspx',1,N'',null,0,null),
(N'Today Admission Fee',N'admission.png',N'171',N'166',N'/_adminDPprof/report-today-fees-collections-a.aspx',2,N'',null,0,null),
(N'Admission Fees Dues',N'admission.png',N'172',N'166',N'/_adminDPprof/admission-fee-dues-report.aspx',3,N'',null,1,null);
Insert into [PrincipalAppDashboard] (Name,Icon,Menu_Id,Parent_Menu_Id,Action,Sequance,Header_Image,Is_Restricted,Status,UserType) values 
(N'Annual Fee',N'fee_pay.png',N'173',N'166',N'/_adminDPprof/annual-fee-collection-report.aspx',4,N'',null,0,null),
(N'Today Annual Fee',N'fee_pay.png',N'174',N'166',N'/_adminDPprof/report-today-fees-collection-annual-b.aspx',5,N'',null,0,null),
(N'Annual Fees Dues',N'fee_pay.png',N'175',N'166',N'/_adminDPprof/annual-fee-dues-report.aspx',6,N'',null,1,null),
(N'Monthly Fee Collection',N'fee_dues.png',N'176',N'166',N'/_adminDPprof/monthly-fee-collection-report.aspx',7,N'',null,0,null),
(N'Today Monthly Fee ',N'fee_dues.png',N'177',N'166',N'/_adminDPprof/report-today-fees-collection-monthly-c.aspx',8,N'',null,0,null),
(N'Monthly Dues',N'fee_dues.png',N'178',N'166',N'/_adminDPprof/monthly-dues.aspx',9,N'',null,1,null),
(N'Fine Report',N'dues_list.png',N'179',N'166',N'/_adminDPprof/fine-report.aspx',10,N'',null,0,null),
(N'Bus Fee',N'school_bus.png',N'180',N'166',N'/_adminDPprof/transport-fees.aspx',11,N'',null,0,null),
(N'Upload Syllabus',N'img_report.png',N'182',N'59',N'/_adminDPprof/UpdatedSyllabus_Chapter.aspx?regid=#uid#',77,N'homework_image.png',null,0,null),
(N'View Uploaded Syllabus',N'img_report.png',N'183',N'59',N'/_adminDPprof/syllabus-report.aspx',78,N'homework_image.png',null,0,null);
Insert into [PrincipalAppDashboard] (Name,Icon,Menu_Id,Parent_Menu_Id,Action,Sequance,Header_Image,Is_Restricted,Status,UserType) values 
(N'Applied Birth Certificate',N'pending_book_list.png',N'184',N'10',N'/_adminDPprof/Request_for_Birth_Certificate.aspx?regid=#uid#',122,N'cirtificate_icon.png',null,1,null),
(N'Birth Certificate Status',N'approval.png',N'185',N'10',N'/_adminDPprof/Birth_Certificate_Status.aspx',123,N'cirtificate_icon.png',null,1,null),
(N'Applied Income Certificate',N'pending_book_list.png',N'186',N'10',N'/_adminDPprof/Applied_Income_Certificate.aspx?regid=#uid#',124,N'cirtificate_icon.png',null,1,null),
(N'Income Certificate Status',N'approval.png',N'187',N'10',N'/_adminDPprof/Income_Certificate_Status.aspx',125,N'cirtificate_icon.png',null,1,null),
(N'Inbox',N'ic_new_message.png',N'188',N'0',N'Submenu',13,N'inbox_dash.png',null,1,null),
(N'Notice',N'msg.png',N'189',N'188',N'Submenu',1,N'inbox_dash.png',null,1,null),
(N'Message',N'ic_new_message.png',N'190',N'188',N'Submenu',2,N'inbox_dash.png',null,1,null),
(N'Events',N'events.png',N'191',N'188',N'Submenu',3,N'inbox_dash.png',null,1,null),
(N'Student''s Notice',N'notice_dashboard.png',N'192',N'189',N'Submenu',1,N'inbox_dash.png',null,1,null),
(N'Teacher''s & Staff Notice',N'notice.png',N'193',N'189',N'Submenu',2,N'inbox_dash.png',null,1,null);
Insert into [PrincipalAppDashboard] (Name,Icon,Menu_Id,Parent_Menu_Id,Action,Sequance,Header_Image,Is_Restricted,Status,UserType) values 
(N'Student''s Send Notice',N'notice_dashboard.png',N'194',N'192',N'/_adminDPprof/send-notice-to-student.aspx?regid=#uid#',1,N'inbox_dash.png',null,1,null),
(N'Student''s View Notice',N'notice.png',N'195',N'192',N'/_adminDPprof/view-student-notice.aspx?regid=#uid#',2,N'inbox_dash.png',null,1,null),
(N'Send Notice',N'notice_dashboard.png',N'196',N'193',N'/_adminDPprof/send-notice-to-teacher.aspx?regid=#uid#',1,N'inbox_dash.png',null,1,null),
(N'View Notice',N'notice.png',N'197',N'193',N'/_adminDPprof/view-all-notice-to-teacher.aspx?regid=#uid#',2,N'inbox_dash.png',null,1,null),
(N'Student''s Message',N'ic_new_message.png',N'198',N'190',N'Submenu',1,N'notice_dashboard.png',null,1,null),
(N'Teacher''s & Staff',N'msg.png',N'199',N'190',N'Submenu',2,N'notice_dashboard.png',null,1,null),
(N'Send Message',N'ic_new_message.png',N'200',N'198',N'/_adminDPprof/send-message-to-student.aspx?regid=#uid#',1,N'',null,1,null),
(N'View Message',N'msg.png',N'201',N'198',N'/_adminDPprof/view-all-message-sent-to-student.aspx?regid=#uid#',2,N'',null,1,null),
(N'Send Message',N'ic_new_message.png',N'202',N'199',N'/_adminDPprof/send-message-to-Teacher.aspx?regid=#uid#',1,N'',null,1,null),
(N'View Message',N'msg.png',N'203',N'199',N'/_adminDPprof/view-sent-message-to-teacher.aspx?regid=#uid#',2,N'',null,1,null);
Insert into [PrincipalAppDashboard] (Name,Icon,Menu_Id,Parent_Menu_Id,Action,Sequance,Header_Image,Is_Restricted,Status,UserType) values 
(N'Student''s Events',N'events.png',N'204',N'191',N'Submenu',1,N'notice_dashboard.png',null,1,null),
(N'Teacher''s & Staff Events',N'notice_dashboard.png',N'205',N'191',N'Submenu',2,N'notice_dashboard.png',null,1,null),
(N'Student''s Send Events',N'events.png',N'206',N'204',N'/_adminDPprof/send-event-to-students.aspx?regid=#uid#',1,N'notice_dashboard.png',null,1,null),
(N'Student''s View Events',N'notice_dashboard.png',N'207',N'204',N'/_adminDPprof/view-student-evetns.aspx?regid=#uid#',2,N'notice_dashboard.png',null,1,null),
(N'Send Events',N'events.png',N'208',N'205',N'/_adminDPprof/send-event-to-teacher.aspx?regid=#uid#',1,N'notice_dashboard.png',null,1,null),
(N'View Events',N'notice_dashboard.png',N'209',N'205',N'/_adminDPprof/view-teacher-events.aspx?regid=#uid#',2,N'notice_dashboard.png',null,1,null),
(N'School Calendar',N'attendance.png',N'210',N'7',N'/_adminTutorProf/School_Calendar.aspx?regid=#uid#',104,N'information_student.png',null,1,null),
(N'Attendance Summary',N'attendance_app.png',N'211',N'83',N'/_adminETutorProf/principle-profile/atendance-summary.aspx',2,N'leave_dashbord.png',null,1,null),
(N'Daily Report',N'img_report.png',N'212',N'0',N'/_adminETutorProf/principle-profile/daily-report.aspx',0,null,null,1,null),
(N'Yearly Report',N'img_report.png',N'213',N'0',N'/_adminETutorProf/principle-profile/yearly-report.aspx',0,null,null,1,null);
Insert into [PrincipalAppDashboard] (Name,Icon,Menu_Id,Parent_Menu_Id,Action,Sequance,Header_Image,Is_Restricted,Status,UserType) values 
(N'Student Strength',N'admission.png',N'214',N'0',N'/_adminETutorProf/principle-profile/student-strength.aspx',0,null,null,1,null),
(N'Make Attendance',N'attendance_app.png',N'215',N'83',N'/_adminDPprof/make-attendance-std.aspx?regid=#uid#',1,null,null,1,null),
(N'Hostel Attendance Summary',N'attendance_app.png',N'216',N'83',N'/_adminETutorProf/principle-profile/hostel-atendance-summary.aspx',3,null,null,1,null),
(N'Class Routine',N'attendance.png',N'217',N'0',N'app_nextSof/p_routine.html',11,null,null,1,null),
(N'Homework Submission Status',N'report_card.png',N'218',N'55',N'/_adminTutorProf/Report_Home_Work_Note_Send.aspx?regid=#uid#',3,N'homework_image.png',null,1,null);

");
                        list.Add(@"Alter PROCEDURE  [sp_student_class_activity]
    @AdmissionNo NVARCHAR(50)=null,
    @ClassId VARCHAR=null,
    @SessionId INT =null,
    @Section NVARCHAR(50)=null,
    @SubjectId NVARCHAR(50)=null, 
    @StartDate NVARCHAR(50)=null,
    @EndDate NVARCHAR(50)=null,
    @day NVARCHAR(50)=null,
     @teacherid NVARCHAR(50)=null,
    @Id INT=null,
    @currentYear varchar(50)=null,
    @month varchar(50)=null,
     @empid varchar(50)=null,
    @sp_status varchar(50)=null
AS
BEGIN
    
     if @sp_status='ddl_all_class'
   
   begin
       SELECT 
        sm.Course_Name,
        sm.course_id,
        sm.Position
    FROM 
        Add_course_table sm
        
        
    ORDER BY 
        sm.Position;
   End
    
    if @sp_status='ddl_all_section'
   
   begin
       select  distinct Section  from admission_registor  order by Section
   End
    
    
   if @sp_status='ddl'
   
   begin
    
       select @ClassId=Class_id , @SessionId=Session_id from dbo.[admission_registor] where admissionserialnumber=@AdmissionNo  and   Transfer_Status in ('New','NT') and  StudentStatus='AV' order by id desc   

    SELECT 
        sm.Subject_name,
        sm.Subject_id,
        sm.Subject_position
    FROM 
        Subject_Master sm
    INNER JOIN  
        Subject_Mapping_New smn 
        ON sm.course_id = smn.Class_id 
        AND sm.Subject_id = smn.Sub_id
    WHERE 
        smn.Admission_no = @AdmissionNo
        AND smn.Class_id = @ClassId
        AND smn.Session_id = @SessionId
    ORDER BY 
        sm.Subject_position;
    End
  
        if(@sp_status='find_class_log_student')
begin

    select
        @ClassId = Class_id,
        @SessionId = Session_id,
        @Section = Section
    from dbo.[admission_registor]
    where admissionserialnumber = @AdmissionNo
    and Transfer_Status in ('New','NT')
    and StudentStatus='AV'
    order by id desc;


    SELECT 
        acd.*,

        (SELECT TOP 1 Course_Name
         FROM Add_course_table
         WHERE course_id = acd.Class_id) AS Course_Name 
         

    FROM TEACHER_LOG_BOOK acd

    WHERE  
        acd.idate >= @StartDate
        and acd.idate <= @EndDate

        AND acd.Session_id = @SessionId

        AND acd.Class_id = @ClassId

        

        AND acd.Section = @Section

    ORDER BY acd.idate ASC;

end
   
    if(@sp_status='find_class_activity')
begin

    select
        @ClassId = Class_id,
        @SessionId = Session_id,
        @Section = Section
    from dbo.[admission_registor]
    where admissionserialnumber = @AdmissionNo
    and Transfer_Status in ('New','NT')
    and StudentStatus='AV'
    order by id desc;


    SELECT 
        acd.*,

        (SELECT TOP 1 Course_Name
         FROM Add_course_table
         WHERE course_id = acd.Class_id) AS Course_Name,

        (SELECT TOP 1 Subject_name
         FROM Subject_Master
         WHERE course_id = acd.Class_id
         AND Subject_id = acd.Subject_id) AS Subject_name

    FROM Activity_Class_Details acd

    WHERE  
        acd.idate >= @StartDate
        and acd.idate <= @EndDate

        AND acd.Session_id = @SessionId

        AND acd.Class_id = @ClassId

        AND (
                ISNULL(@SubjectId,'0') = '0'
                OR acd.Subject_id = @SubjectId
            )

        AND acd.Section_data = @Section

    ORDER BY acd.idate ASC;

End
     if(@sp_status='find_class_Routine')
     begin
         
         select @ClassId=Class_id , @SessionId=Session_id,@Section=Section from dbo.[admission_registor] where admissionserialnumber=@AdmissionNo  and   Transfer_Status in ('New','NT') and  StudentStatus='AV' order by id desc  
         
         
         SELECT DISTINCT
    (
        SELECT TOP 1 name 
        FROM user_details ud 
        JOIN TeacherCourseSubjectMaping tcm 
            ON tcm.UserID = ud.user_id 
        WHERE 
            tcm.AssignCourseID = t4.Subject_id
            AND tcm.CategoryID = t4.Class_id
            AND tcm.Session_id = t4.Session_id
            AND tcm.Section = t4.Section
    ) AS Teacher_name,
    
    t1.Period_Name,
    t1.Period,
    t1.Period_type,
    t1.Period_no,
    FORMAT(t2.Start_Time, 'hh:mm tt') AS time1,
    FORMAT(t2.End_time, 'hh:mm tt') AS time2,
    t2.Timespan,

    (
        SELECT TOP 1 Subject_name 
        FROM Subject_Master 
        WHERE course_id = t4.Class_id 
        AND Subject_id = t4.Subject_id
    ) AS Subject_name

FROM 
    Class_Routine_period_Master t1
JOIN 
    Class_Routine_period t2 
    ON t1.Session_id = t2.Session_id 
    AND t1.Class_id = t2.course_id 
    AND t1.Period = t2.Period
JOIN 
    Class_Routine_Master t3 
    ON t3.Session_id = t2.Session_id 
    AND t3.Class_id = t2.course_id
LEFT JOIN 
    Class_routine_period_subject_mapping t4
    ON t3.Session_id = t4.Session_id 
    AND t3.Class_id = t4.Class_id
    AND t3.Section = t4.Section
    AND t1.Period = t4.Class_period
    AND t3.Day = t4.Day

WHERE 
    t1.Class_id = @ClassId
    AND t1.Session_id =  @SessionId
    AND t3.Section = @Section
    AND t3.Day = @day

ORDER BY 
    t1.Period_no ASC;

     End


if(@sp_status='find_class_Routine_teacher')
     begin
         
         select @SessionId=session_id from dbo.[session_details] where Use_mode='1' order by id desc  
       SELECT ( SELECT TOP 1 name 
        FROM user_details  where user_id=tcm.UserID ) as Teacher_name ,crpsm.Day,(SELECT TOP 1 Subject_name FROM Subject_Master WHERE course_id = crpsm.Class_id  AND Subject_id=crpsm.Subject_id) AS Subject_name, crpsm.Section,(SELECT TOP 1 Course_Name  FROM Add_course_table  WHERE course_id = crpsm.Class_id) AS classname,rp.Period_Name,(select top 1 Course_Name from Add_course_table where course_id=course_id) as classname,FORMAT(rp.Start_Time, 'hh:mm tt') AS time1, FORMAT(rp.End_time, 'hh:mm tt') AS time2, rp.Timespan FROM  Class_routine_period_subject_mapping crpsm join Class_Routine_period rp on rp.Period=crpsm.Class_period join TeacherCourseSubjectMaping tcm on  tcm.AssignCourseID = crpsm.Subject_id
            AND tcm.CategoryID = crpsm.Class_id
            AND tcm.Session_id = crpsm.Session_id
            AND tcm.Section = crpsm.Section where crpsm.Session_id=@SessionId and Day=@day and tcm.UserID=@teacherid   order by rp.Period_Name,crpsm.Section
          
         
     End



if(@sp_status='student_notice')
begin
     select @ClassId=Class_id , @SessionId=Session_id,@Section=Section from dbo.[admission_registor] where admissionserialnumber=@AdmissionNo  and   Transfer_Status in ('New','NT') and  StudentStatus='AV' order by id desc   
     
   SELECT  
    CASE  WHEN LEN(Notice) > 100 THEN LEFT(Notice, 100) + '...'ELSE Notice 
 END AS shortnotice, *, 
    FORMAT(Date_Main, 'dd-MMM-yyyy') AS Date1,
    FORMAT(Date_Main, 'dd') AS day,
    FORMAT(Date_Main, 'MMM') AS month,
    FORMAT(Date_Main, 'yyyy') AS year  
FROM Notice_Board_Details 
WHERE 
    (
        (Class IN ('ALL', @ClassId) AND Section IN ('ALL', @Section) AND Session_Id = @SessionId)
        OR Admission_no = @AdmissionNo
    )
    AND Session_Id = @SessionId
    AND  Posted_Idate >= @StartDate AND Posted_Idate <= @EndDate  
    
    
ORDER BY Posted_Idate DESC;
    
End

if(@sp_status='student_notice_Details')
begin
     select  *,format(Date_Main, 'dd-MMM-yyyy') as Date1,format(Date_Main, 'dd') as day,format(Date_Main, 'MMM') as month,format(Date_Main, 'yyyy') as year  from Notice_Board_Details where Id=@Id  order by  Posted_Idate Desc
    
End
 
 
 if(@sp_status='student_events')
begin
    SELECT TOP 1
    @ClassId = CAST(Class_id AS VARCHAR(20)),
    @SessionId = Session_id,
    @Section = Section
FROM dbo.admission_registor
WHERE admissionserialnumber = @AdmissionNo
AND Transfer_Status IN ('New','NT')
AND StudentStatus = 'AV'
ORDER BY id DESC;


SELECT
    CASE
        WHEN LEN(Details) > 100
        THEN LEFT(Details, 100) + '...'
        ELSE Details
    END AS shortnotice,

    *,

    FORMAT(Date_Main, 'dd-MMM-yyyy') AS Date1,
    FORMAT(Date_Main, 'dd') AS day,
    FORMAT(Date_Main, 'MMM') AS month,
    FORMAT(Date_Main, 'yyyy') AS year,

    Attachments AS Attachment

FROM News_Events_Details

WHERE
(
    (
        CAST(Class AS VARCHAR(20)) IN ('ALL', @ClassId)
        AND Section IN ('ALL', @Section)
        AND Session_Id = @SessionId
    )

    OR Admission_no = @AdmissionNo
)

AND Session_Id = @SessionId
AND Posted_Idate BETWEEN @StartDate AND @EndDate

ORDER BY Posted_Idate DESC;
    
End

if(@sp_status='student_event_Details')
begin
     select  *,format(Date_Main, 'dd-MMM-yyyy') as Date1,format(Date_Main, 'dd') as day,format(Date_Main, 'MMM') as month,format(Date_Main, 'yyyy') as year  from News_Events_Details where Id=@Id  order by  Posted_Idate Desc
    
End
 
 
  if(@sp_status='student_mesage')
begin
     select @ClassId=Class_id , @SessionId=Session_id,@Section=Section from dbo.[admission_registor] where admissionserialnumber=@AdmissionNo  and   Transfer_Status in ('New','NT') and  StudentStatus='AV' order by id desc   
     
  SELECT  
    CASE  WHEN LEN(Details) > 100 THEN LEFT(Details, 100) + '...'ELSE Details 
 END AS shortnotice, *, 
     CONVERT(varchar(11), CONVERT(date, Date, 103), 106) AS Date1, -- 05 May 2026
    RIGHT('0' + CAST(DAY(CONVERT(date, Date, 103)) AS varchar), 2) AS day,
    DATENAME(MONTH, CONVERT(date, Date, 103)) AS month,
    YEAR(CONVERT(date, Date, 103)) AS year,Attachments as Attachment  
FROM Private_Messages 
WHERE 
    (
        (Class_Id IN ('ALL', @ClassId) AND Section_Id IN ('ALL', @Section) AND Session_id = @SessionId)
        OR Admission_no = @AdmissionNo
    )
    AND Session_id = @SessionId
    AND  Idate >= @StartDate AND Idate <= @EndDate
ORDER BY Idate DESC;
    
End


 
 
 
 
 
 
 if(@sp_status='teacher_attendance')
begin
     select @empid=Employee_id  from dbo.[HR_Employee_Master] where Emp_Code=@teacherid  
      DECLARE @date VARCHAR(50) = @month + '-' + CAST(@currentYear AS VARCHAR);

SELECT 
    Date, 
    In_Time, 
    Out_Time, 
    AttendanceStatus 
FROM 
    HR_Daily_Attendance_Record 
WHERE 
    Employee_id = @empid 
    AND date LIKE '%' + @date + '%' 
    AND (In_Time IS NOT NULL AND In_Time != '' and In_Time!='00:00:00 AM') 
ORDER BY 
    Idate;
    
End

if(@sp_status='teacher_notice')
begin
    
     
   SELECT  
    CASE  WHEN LEN(Notice) > 100 THEN LEFT(Notice, 100) + '...'ELSE Notice 
 END AS shortnotice, *, 
    FORMAT(Date_Main, 'dd-MMM-yyyy') AS Date1,
    FORMAT(Date_Main, 'dd') AS day,
    FORMAT(Date_Main, 'MMM') AS month,
    FORMAT(Date_Main, 'yyyy') AS year  
FROM Notice_Board_Details_Teacher 
WHERE 
    (
        (Teacher_Id IN ('ALL', @teacherid) )
      
    )
    
    AND  Posted_Idate >= @StartDate AND Posted_Idate <= @EndDate  
    
    
ORDER BY Posted_Idate DESC;
    
End

if(@sp_status='teacher_notice_Details')
begin
     select  *,format(Date_Main, 'dd-MMM-yyyy') as Date1,format(Date_Main, 'dd') as day,format(Date_Main, 'MMM') as month,format(Date_Main, 'yyyy') as year  from Notice_Board_Details_Teacher where Id=@Id  order by  Posted_Idate Desc
    
End


 if(@sp_status='teacher_message')
begin
    
     
   SELECT  
    CASE  WHEN LEN(Details) > 100 THEN LEFT(Details, 100) + '...'ELSE Details 
 END AS shortnotice, *, 
    FORMAT(Date_Main, 'dd-MMM-yyyy') AS Date1,Attachments as Attachment,Details as Notice,
    FORMAT(Date_Main, 'dd') AS day,
    FORMAT(Date_Main, 'MMM') AS month,
    FORMAT(Date_Main, 'yyyy') AS year  
FROM Private_Messages_For_Teacher 
WHERE 
    (
        (Teacher_Id IN ('ALL', @teacherid) )
      
    )
    
    AND  Idate >= @StartDate AND Idate <= @EndDate  
    
    
ORDER BY Idate DESC;
    
End

if(@sp_status='teacher_message_details')
begin
    select  *,  format(Date_Main, 'dd-MMM-yyyy') as Date1,format(Date_Main, 'dd') as day,format(Date_Main, 'MMM') as month,format(Date_Main, 'yyyy') as year,Attachments as Attachment  from Private_Messages_For_Teacher where Id=@Id  order by  Idate Desc
    
End


 if(@sp_status='teacher_events')
begin
    
     
   SELECT  
    CASE  WHEN LEN(Details) > 100 THEN LEFT(Details, 100) + '...'ELSE Details 
 END AS shortnotice, *, Details as Notice,
    FORMAT(Date_Main, 'dd-MMM-yyyy') AS Date1,Heading as Subject,
    FORMAT(Date_Main, 'dd') AS day,
    FORMAT(Date_Main, 'MMM') AS month,
    FORMAT(Date_Main, 'yyyy') AS year  
FROM News_Events_Details_Teacher 
WHERE 
    (
        (Teacher_Id IN ('ALL', @teacherid) )
      
    )
    
    AND  Posted_Idate >= @StartDate AND Posted_Idate <= @EndDate  
    
    
ORDER BY Posted_Idate DESC;
    
End

if(@sp_status='teacher_events_details')
begin
    select  *,  format(Date_Main, 'dd-MMM-yyyy') as Date1,format(Date_Main, 'dd') as day,format(Date_Main, 'MMM') as month,format(Date_Main, 'yyyy') as year,Attachment   from News_Events_Details_Teacher where Id=@Id  order by  Posted_Idate Desc
    
End




if(@sp_status='find_class_Routine_p')
     begin
         
         select top 1 @ClassId=Class_id , @SessionId=Session_id,@Section=Section from dbo.[admission_registor] where Class_id=@classid  and   Transfer_Status in ('New','NT') and  StudentStatus='AV' order by id desc  
         
         
         SELECT DISTINCT
    (
        SELECT TOP 1 name 
        FROM user_details ud 
        JOIN TeacherCourseSubjectMaping tcm 
            ON tcm.UserID = ud.user_id 
        WHERE 
            tcm.AssignCourseID = t4.Subject_id
            AND tcm.CategoryID = t4.Class_id
            AND tcm.Session_id = t4.Session_id
            AND tcm.Section = t4.Section
    ) AS Teacher_name,
    
    t1.Period_Name,
    t1.Period,
    t1.Period_type,
    t1.Period_no,
    FORMAT(t2.Start_Time, 'hh:mm tt') AS time1,
    FORMAT(t2.End_time, 'hh:mm tt') AS time2,
    t2.Timespan,

    (
        SELECT TOP 1 Subject_name 
        FROM Subject_Master 
        WHERE course_id = t4.Class_id 
        AND Subject_id = t4.Subject_id
    ) AS Subject_name

FROM 
    Class_Routine_period_Master t1
JOIN 
    Class_Routine_period t2 
    ON t1.Session_id = t2.Session_id 
    AND t1.Class_id = t2.course_id 
    AND t1.Period = t2.Period
JOIN 
    Class_Routine_Master t3 
    ON t3.Session_id = t2.Session_id 
    AND t3.Class_id = t2.course_id
LEFT JOIN 
    Class_routine_period_subject_mapping t4
    ON t3.Session_id = t4.Session_id 
    AND t3.Class_id = t4.Class_id
    AND t3.Section = t4.Section
    AND t1.Period = t4.Class_period
    AND t3.Day = t4.Day

WHERE 
    t1.Class_id = @ClassId
    AND t1.Session_id =  @SessionId
    AND t3.Section = @Section
    AND t3.Day = @day

ORDER BY 
    t1.Period_no ASC;

     End


if(@sp_status='find_myprofile_student')
begin
     
     DECLARE @school_name VARCHAR(100),
        @school_logo VARCHAR(200),
        @school_email VARCHAR(100),
        @school_mobile VARCHAR(20)

SELECT 
    @school_name = firm_name,
    @school_logo = logo,
    @school_email = email,
    @school_mobile = contact_no
FROM Firm_Details

SELECT TOP 1
    class AS classname,
    rollnumber,
    Section,
    session,
    dateofadmission,
    studentname,
    gender,
    dob,
    fathername,
    mothername,
    careof_permanent,careof,
    father_mob,
    admissionserialnumber,mother_mob,

    CASE 
        WHEN (studentimagepath IS NULL OR studentimagepath = '') 
        THEN '/images_second/blank.png'
        ELSE studentimagepath
    END AS Student_img,

    @school_name AS school_name,
    @school_logo AS school_logo,
    @school_email AS school_email,
    @school_mobile AS school_mobile

FROM admission_registor

WHERE admissionserialnumber = @AdmissionNo
AND Transfer_Status IN ('NT','New')

ORDER BY id DESC
End

 
END;");
                        break;
                    }
                    #endregion 
            }
            return list;
        }
    }
}