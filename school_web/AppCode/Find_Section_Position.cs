using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace school_web.AppCode
{
    public class Find_Section_Position
    {
        public string no_position(string test_id, SqlConnection con)
        {


            SqlDataAdapter ad = new SqlDataAdapter("Select MAX(CAST(Position as int )) from Section_Arranging where Test_id='" + test_id + "'", con);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Section_Arranging");
            DataTable dt = ds.Tables[0];
            int rowcount = dt.Rows.Count;

            if (rowcount == 0)
            {

                return "1";
            }
            else
            {
                if (dt.Rows[0][0].ToString() == "")
                {
                    return "1";
                }
                if (dt.Rows[0][0].ToString() == "0")
                {
                    return "1";
                }
                else
                {
                    return (Convert.ToInt32(dt.Rows[0][0].ToString()) + 1).ToString();
                }

            }


        }



        public int no_position_find(string test_id, string section)
        {

            SqlDataAdapter ad = new SqlDataAdapter("Select Position from Section_Arranging where Test_id='" + test_id + "' and Section_name='" + section + "'", My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Section_Arranging");
            DataTable dt = ds.Tables[0];
            int rowcount = dt.Rows.Count;
            if (rowcount == 0)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(dt.Rows[0][0].ToString());
            }
        }

        public int fin_max_q_no_testid_section(string testid, string section)
        {
            int maxq_no=0;
            SqlDataAdapter ad = new SqlDataAdapter("Select MAX(CAST(Question_no as int )) from question_info where test_id='" + testid + "' and Section='" + section + "' ", My.conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "Section_Arranging");
            DataTable dt = ds.Tables[0];
            int rowcount = dt.Rows.Count;

            if (rowcount == 0)
            {

                maxq_no= 1;
            }
            else
            {
                if (dt.Rows[0][0].ToString() == "")
                {
                    maxq_no= 1;
                }
                if (dt.Rows[0][0].ToString() == "0")
                {
                    maxq_no =1;
                }

            }
            return maxq_no = 1;
        }

    }
}