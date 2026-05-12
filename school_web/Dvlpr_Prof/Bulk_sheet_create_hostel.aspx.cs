using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Dvlpr_Prof
{
    public partial class Bulk_sheet_create_hostel : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                mycode.bind_all_ddl_with_id(ddl_hostel, "select Hostel_name,Hostel_id from Hostels_master order by Hostel_name asc");
                mycode.bind_all_ddl_with_id(ddl_hostel_catogery, "select Category_name,Category_id from Hostel_room_category_master order by Category_name asc");
            }
        }

        protected void btn_create_Click(object sender, EventArgs e)
        {
            if (ddl_hostel.SelectedItem.Text == "Select")
            {
                Alertme("Please select hostel.");
                ddl_hostel.Focus();

            }
            else if (ddl_hostel_catogery.SelectedItem.Text == "Select")
            {
                Alertme("Please select room category.");
                ddl_hostel_catogery.Focus();

            }
            else if (txt_no_sheet.Text == "")
            {
                Alertme("Please no of sheet");
                txt_no_sheet.Focus();

            }
            else
            {
                int num = My.toint(txt_no_sheet.Text);

                int sheet = 1;
                for (int i = 0; i < num; i++)
                {
                    int shetname = sheet;
                    string hostelid = ddl_hostel.SelectedValue;
                    string catogery = ddl_hostel_catogery.SelectedValue;
                    create_data(shetname, hostelid, catogery);
                    sheet++;
                }
            }
        }

        private void create_data(int shetname, string hostelid, string catogery)
        {
            if (mycode.IsUserExist("select Bed_id from Hostel_room_bed_master where Hostel_id='" + hostelid + "' and Category_id='" + catogery + "' and Room_id='" + ddl_room.SelectedValue + "' and Bed_name='" + shetname + "'"))
            {

                int bed_id = create_sl_no();
                SqlCommand cmd;
                string query = "INSERT INTO Hostel_room_bed_master (Hostel_id,Category_id,Room_id,Bed_id,Bed_name,Created_by,Created_date,Created_idate,Wardrobes_NO,Bed_Position) values (@Hostel_id,@Category_id,@Room_id,@Bed_id,@Bed_name,@Created_by,@Created_date,@Created_idate,@Wardrobes_NO,@Bed_Position)";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Hostel_id", hostelid);
                cmd.Parameters.AddWithValue("@Category_id", catogery);
                cmd.Parameters.AddWithValue("@Room_id", ddl_room.SelectedValue);
                cmd.Parameters.AddWithValue("@Bed_id", bed_id);
                cmd.Parameters.AddWithValue("@Bed_name", shetname);

                cmd.Parameters.AddWithValue("@Created_by", "Developer");
                cmd.Parameters.AddWithValue("@Created_date", mycode.date());
                cmd.Parameters.AddWithValue("@Created_idate", mycode.idate());
                cmd.Parameters.AddWithValue("@Wardrobes_NO", shetname);
                cmd.Parameters.AddWithValue("@Bed_Position", "Left");

                if (My.InsertUpdateData(cmd))
                {


                    Alertme("Bed master has been created successfully.");

                }

            }
            else
            {
                Alertme("Bed already added with this name to this room.");
            }
        }

        private int create_sl_no()
        {
            bool duplicate = true;
            int bed_id = My.toint(My.auto_serialS("Bed_id"));
            while (duplicate)
            {
                DataTable cdt = My.dataTable(" select Bed_id from Hostel_room_bed_master where Bed_id='" + bed_id + "'");
                int rowcount = cdt.Rows.Count;
                if (rowcount == 0)
                {
                    duplicate = false;
                }
                else
                {
                    bed_id = My.toint(My.auto_serialS("Bed_id"));
                }
            }

            return bed_id;
        }
        string scrpt;
        private void Alertme(string p)
        {
            lblmessage.Text = "Please enter admission";
            scrpt = "<script>$( function () { $('.notificationpan').hide().slideDown(1000);  $('.notificationpan').delay(10000).show().slideUp(1000);});</script>";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", scrpt, false);
        }

        protected void ddl_hostel_catogery_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_hostel.SelectedItem.Text == "Select")
            {
                Alertme("Please select hostel.");
                ddl_hostel.Focus();

            }
            else if (ddl_hostel_catogery.SelectedItem.Text == "Select")
            {
                Alertme("Please select room category.");
                ddl_hostel_catogery.Focus();

            }
            else
            {
                mycode.bind_all_ddl_with_id(ddl_room, "select Room_name,Room_id from Hostel_room_master where Hostel_id='" + ddl_hostel.SelectedValue + "' and Category_id='" + ddl_hostel_catogery.SelectedValue + "' order by Room_name asc");
            }


        }
    }
}