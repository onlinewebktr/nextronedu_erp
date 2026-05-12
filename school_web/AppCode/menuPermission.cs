using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace school_web.AppCode
{
    public class menuPermission
    {
        internal static void update_menu_permission_for_user(string user_id, string user_type)
        {
            DataTable dt = My.dataTable("select * from MenuPermissionForUser_Groupwise where UserType='" + user_type + "'");
            if (dt.Rows.Count > 0)
            {
                My.exeSql("delete from MenuPermissionForUser_web where UserID='" + user_id + "';delete from HR_MenuPermission where Employee_id='" + user_id + "'; delete from SALE_PURCHASE_MENUPERMISSIONFORUSER_WEB where  UserID='" + user_id + "'; delete from Exam_MenuPermissionForUser_web where UserID='" + user_id + "'");
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["MainMenuId"].ToString() == "12")// payroll menu permission
                    {
                        InsertMenu_payrolls(user_id, dr["MenuID"].ToString(), dr["MainMenuId"].ToString());
                    }
                    else if (dr["MainMenuId"].ToString() == "33")// sale purchase
                    {
                        InsertMenu_sale_purchase(user_id, dr["MenuID"].ToString(), dr["MainMenuId"].ToString());
                    }
                    else if (dr["MainMenuId"].ToString() == "21")// Examination
                    {
                        InsertMenu_Exam_MenuPermissionForUser_web(user_id, dr["MenuID"].ToString(), dr["MainMenuId"].ToString());
                    }
                    else
                    {
                        InsertMenu(user_id, dr["MenuID"].ToString(), dr["MainMenuId"].ToString(), dr["Is_Edit"].ToString(), dr["Is_delete"].ToString(), dr["Is_Download"].ToString(), dr["Is_Print"].ToString(), dr["Is_add"].ToString());
                    }
                }
            }
        }


        private static void InsertMenu(string UserId, string menu_id, string MainMenu_id, string Is_Edit, string Is_delete, string Is_Download, string Is_Print, string Is_add)
        {
            My imp = new My();
            try
            {
                int edit_permission = My.toIntS(Is_Edit);
                int delete_permission = My.toIntS(Is_delete);
                int delete_Download = My.toIntS(Is_Download);
                int delete_print = My.toIntS(Is_Print);
                int delete_add = My.toIntS(Is_add);

                string strQuery = "Select  * from MenuPermissionForUser_web where MenuID='" + menu_id + "' and UserID='" + UserId + "'  ";
                SqlCommand cmd = new SqlCommand(strQuery);
                DataTable dt = imp.GetData(cmd);
                int count = dt.Rows.Count;
                if (count == 0)
                {
                    string sqlstring = " INSERT INTO MenuPermissionForUser_web(MenuID, UserID,MainMenuId,Is_Edit,Is_delete,Is_Download,Is_Print,Is_add,Created_by,Created_date,Created_time) VALUES  ('" + menu_id + "', '" + UserId + "', '" + MainMenu_id + "','" + edit_permission + "','" + delete_permission + "','" + delete_Download + "','" + delete_print + "','" + delete_add + "','" + "Admin" + "','" + imp.date() + "','" + imp.time() + "')";
                    imp.executequery(sqlstring);
                    if (menu_id == "253") //new payroll menu
                    {
                        My.exeSql("update HR_UserProfile set IsHr=1 where UserId='" + UserId + "'");
                    }
                }
                else
                {
                    if (menu_id == "253")// new payroll menu
                    {
                        My.exeSql("update HR_UserProfile set IsHr=1 where UserId='" + UserId + "'");
                    }
                    string query = "Update MenuPermissionForUser_web set Is_Edit=@Is_Edit,Is_delete=@Is_delete,Is_Download=@Is_Download,Is_Print=@Is_Print,Is_add=@Is_add,Updated_by=@Updated_by,Updated_date=@Updated_date,Updated_time=@Updated_time,MainMenuId=@MainMenuId where MenuID=@MenuID and UserID=@UserID";
                    cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Is_Edit", edit_permission);
                    cmd.Parameters.AddWithValue("@Is_delete", delete_permission);
                    cmd.Parameters.AddWithValue("@Is_Download", delete_Download);
                    cmd.Parameters.AddWithValue("@Is_Print", delete_print);
                    cmd.Parameters.AddWithValue("@MenuID", menu_id);
                    cmd.Parameters.AddWithValue("@Is_add", delete_add);
                    cmd.Parameters.AddWithValue("@UserID", UserId);
                    cmd.Parameters.AddWithValue("@MainMenuId", MainMenu_id);
                    cmd.Parameters.AddWithValue("@Updated_by", "Admin");
                    cmd.Parameters.AddWithValue("@Updated_date", imp.date());
                    cmd.Parameters.AddWithValue("@Updated_time", imp.time());
                    if (My.InsertUpdateData(cmd))
                    {
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }


        private static void InsertMenu_Exam_MenuPermissionForUser_web(string UserID, string menu_id, string MainMenu_id)
        {
            My imp = new My();
            string strQuery = "Select * from Exam_MenuPermissionForUser_web where MenuID='" + menu_id + "' and UserID='" + UserID + "'  ";
            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = imp.GetData(cmd);
            int count = dt.Rows.Count;
            if (count == 0)
            {
                string sqlstring = " INSERT INTO Exam_MenuPermissionForUser_web(MenuID, UserID,MainMenuId) VALUES  ('" + menu_id + "', '" + UserID + "', '" + MainMenu_id + "')";
                imp.executequery(sqlstring);

                string strQuery1 = "Select  * from MenuPermissionForUser_web where MainMenuId='" + MainMenu_id + "' and UserID='" + UserID + "'  ";
                SqlCommand cmd1 = new SqlCommand(strQuery1);
                DataTable dt1 = imp.GetData(cmd1);
                int count1 = dt1.Rows.Count;
                if (count1 == 0)
                {
                    string sqlstring3 = " INSERT INTO MenuPermissionForUser_web(MenuID, UserID,MainMenuId,Is_Edit,Is_delete,Is_Download,Is_Print,Is_add) VALUES  ('148', '" + UserID + "', '" + MainMenu_id + "','1','1','1','1','1')";
                    imp.executequery(sqlstring3);
                }
            }
            else
            {
            }
        }

        private static void InsertMenu_sale_purchase(string UserID, string menu_id, string MainMenu_id)
        {
            My imp = new My();
            string strQuery = "Select  * from SALE_PURCHASE_MENUPERMISSIONFORUSER_WEB where MenuID='" + menu_id + "' and UserID='" + UserID + "'  ";
            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = imp.GetData(cmd);
            int count = dt.Rows.Count;
            if (count == 0)
            {
                string sqlstring = " INSERT INTO SALE_PURCHASE_MENUPERMISSIONFORUSER_WEB(MenuID, UserID,MainMenuId) VALUES  ('" + menu_id + "', '" + UserID + "', '" + MainMenu_id + "')";
                imp.executequery(sqlstring);

                string strQuery1 = "Select  * from MenuPermissionForUser_web where MainMenuId='" + MainMenu_id + "' and UserID='" + UserID + "'  ";
                SqlCommand cmd1 = new SqlCommand(strQuery1);
                DataTable dt1 = imp.GetData(cmd1);
                int count1 = dt1.Rows.Count;
                if (count1 == 0)
                {
                    string sqlstring3 = " INSERT INTO MenuPermissionForUser_web(MenuID, UserID,MainMenuId,Is_Edit,Is_delete,Is_Download,Is_Print,Is_add) VALUES  ('254', '" + UserID + "', '" + MainMenu_id + "','1','1','1','1','1')";
                    imp.executequery(sqlstring3);
                }
            }
        }

        private static void InsertMenu_payrolls(string UserId, string menu_id, string MainMenu_id)
        {
            My imp = new My();
            string Employee_id = PayrollMy.get_employee_id_from_employee_code(UserId);
            string strQuery = "Select * from HR_MenuPermission where MenuID='" + menu_id + "' and Employee_id='" + Employee_id + "'  ";
            SqlCommand cmd = new SqlCommand(strQuery);
            DataTable dt = imp.GetData(cmd);
            int count = dt.Rows.Count;
            if (count == 0)
            {
                string sqlstring = " INSERT INTO HR_MenuPermission(MenuID, UserID,MainMenuId,Employee_id) VALUES  ('" + menu_id + "', '" + UserId + "', '" + MainMenu_id + "','" + Employee_id + "')";
                imp.executequery(sqlstring);

                My.exeSql("update HR_UserProfile set IsHr=1,IsAdmin=1 where UserId='" + Employee_id + "'");
                string strQuery1 = "Select  * from HR_MenuPermission where MenuID='1' and Employee_id='" + Employee_id + "'  ";
                SqlCommand cmd1 = new SqlCommand(strQuery1);
                DataTable dt1 = imp.GetData(cmd1);
                int count1 = dt1.Rows.Count;
                if (count1 == 0)
                {
                    string sqlstring2 = " INSERT INTO HR_MenuPermission(MenuID, UserID,MainMenuId,Employee_id) VALUES  ('1', '" + UserId + "', '" + MainMenu_id + "','" + Employee_id + "')";
                    imp.executequery(sqlstring2);
                    string sqlstring4 = " INSERT INTO HR_MenuPermission(MenuID, UserID,MainMenuId,Employee_id) VALUES  ('33', '" + UserId + "', '" + MainMenu_id + "','" + Employee_id + "')";
                    imp.executequery(sqlstring4);
                    string sqlstring5 = " INSERT INTO HR_MenuPermission(MenuID, UserID,MainMenuId,Employee_id) VALUES  ('47', '" + UserId + "', '" + MainMenu_id + "','" + Employee_id + "')";
                    imp.executequery(sqlstring5);
                    string sqlstring3 = " INSERT INTO MenuPermissionForUser_web(MenuID, UserID,MainMenuId,Is_Edit,Is_delete,Is_Download,Is_Print,Is_add) VALUES  ('253', '" + UserId + "', '12','1','1','1','1','1')";
                    imp.executequery(sqlstring3);
                }
            }
            else
            { 
                My.exeSql("update HR_UserProfile set IsHr=1,IsAdmin=1 where UserId='" + Employee_id + "'; delete from MenuPermissionForUser_web where UserID='" + UserId + "' and MenuID='253' and MainMenuId='12'");
                string sqlstring3 = " INSERT INTO MenuPermissionForUser_web(MenuID, UserID,MainMenuId,Is_Edit,Is_delete,Is_Download,Is_Print,Is_add) VALUES  ('253', '" + UserId + "', '12','1','1','1','1','1')";
                imp.executequery(sqlstring3);
            }
        }


    }
}