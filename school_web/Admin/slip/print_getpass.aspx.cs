using school_web.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Admin.slip
{
    public partial class print_getpass : System.Web.UI.Page
    {
        My mycode = new My();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Request_id"] != null)
                {
                    ViewState["Request_id"] = Request.QueryString["Request_id"].ToString();
                    Bind_schoolinfo();
                    Bind_student_details();
                }
            }
        }

        private void Bind_student_details()
        {
            string query = "Select *,format (Start_date_time,'dd/MM/yyyy hh:mm:ss tt') as starttime,format (End_Date_time,'dd/MM/yyyy hh:mm:ss tt') as endtime,(Select top 1 Remarks from Hostel_Out_Pass_Chat where Request_id =Hostel_Out_Pass_Request.Request_id order by id asc) as remarks from Hostel_Out_Pass_Request where Request_id ='" + ViewState["Request_id"] + "'  ";
            DataTable dt = mycode.FillData(query);
            if (dt.Rows.Count == 0)
            {
                



            }
            else
            {
                lbl_departure.Text = dt.Rows[0]["starttime"].ToString();
                lbl_Arrival.Text = dt.Rows[0]["endtime"].ToString();
                
                lbl_leave_remarks.Text = dt.Rows[0]["remarks"].ToString() + " No. of day-" + dt.Rows[0]["No_of_day"].ToString();

                lbl_warden_emp_code.Text = dt.Rows[0]["Hostel_warden_id"].ToString();


                Dictionary<string, object> dc1 = My.get_user_info(dt.Rows[0]["Hostel_warden_id"].ToString());
                lbl_wardenn_name.Text = (String)dc1["name"];

                string query2 = "Select * from admission_registor where admissionserialnumber ='" + dt.Rows[0]["Adm_No"].ToString() + "' and Session_id='" + dt.Rows[0]["Session_id"].ToString() + "'";
                DataTable dt1 = mycode.FillData(query2);
                if (dt1.Rows.Count == 0)
                {

                }
                else
                {
                    if(dt1.Rows[0]["studentimagepath"].ToString()=="")
                    {
                        studentphoto.Src = "../../Student_Profile/images/dummy-student.jpg";
                    }
                    else
                    {
                        studentphoto.Src = dt1.Rows[0]["studentimagepath"].ToString();

                    }

                    

                    lbl_studentname.Text = dt1.Rows[0]["studentname"].ToString();
                    lbl_class.Text = dt1.Rows[0]["class"].ToString();
                    lbl_section.Text = dt1.Rows[0]["Section"].ToString();
                    lbl_rollno.Text = dt1.Rows[0]["rollnumber"].ToString();
                    lbl_fathername.Text = dt1.Rows[0]["fathername"].ToString();

                    Dictionary<string, object> dc2 = mycode.getassinedhostel_info_adm(dt.Rows[0]["Adm_No"].ToString(), dt.Rows[0]["Session_id"].ToString());
                    lbl_rom_no.Text = (String)dc2["Room_name"];
                    lbl_bead_no.Text = (String)dc2["Bed_name"];
                    lbl_hostel_name.Text = (String)dc2["hostelname"];

              }


                string query3 = "Select * from Student_image_new where Admission_no ='" + dt.Rows[0]["Adm_No"].ToString() + "'";
                DataTable dt3 = mycode.FillData(query3);
                if (dt3.Rows.Count == 0)
                {

                }
                else
                {
                    for (int i = 0; i < dt3.Rows.Count; i++)
                    {
                        string imgType = dt3.Rows[i]["Image_type"].ToString();
                        string imgPath = dt3.Rows[i]["Image_path"].ToString();

                        if (imgType == "Father_image")
                        {
                            fatherphot.Src = string.IsNullOrEmpty(imgPath) ?
                                "images/Male_dummy_photo.jpg" : imgPath;
                        }
                        else if (imgType == "Mother_image")
                        {
                            motherphot.Src = string.IsNullOrEmpty(imgPath) ?
                                "images/Female_dummy_photo.jpg" : imgPath;
                        }
                        
                    }


                }
            }

           
        }

        private void Bind_schoolinfo()
        {
            ViewState["page_reset"] = "0";
            DataTable dt = mycode.FillData("select * from Firm_Details ");
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                try
                {
                    if (dt.Rows[0]["Is_page_reset_after_fee_payment"].ToString() == "")
                    {
                        ViewState["page_reset"] = "0";
                    }
                    else
                    {
                        ViewState["page_reset"] = dt.Rows[0]["Is_page_reset_after_fee_payment"].ToString();
                    }
                }
                catch
                {
                    ViewState["page_reset"] = "0";
                }
                try
                {
                    if (dt.Rows[0]["Is_slip_header"].ToString() == "True")
                    //{
                    //    textheader.Visible = false;
                    //    printheader.Visible = true;
                    //    img_header.Visible =  true;
                    //    img_header.ImageUrl = dt.Rows[0]["Header_images"].ToString();
                    //}
                    //else
                    //{
                        printheader.Visible =  false;
                        textheader.Visible = true;
                        printheader.Visible = false;
                    //}
                }
                catch
                {
                    textheader.Visible =  true;
                    printheader.Visible =  false;
                }
                lbl_affiliation_no.Text =  dt.Rows[0]["Affiliation"].ToString();
                lbl_schoolno.Text = dt.Rows[0]["school_no"].ToString();
                imglogo.ImageUrl =  dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_emaiid.Text  = dt.Rows[0]["email"].ToString();
                lbl_website.Text  = dt.Rows[0]["website"].ToString();
                lbl_contact_details.Text = dt.Rows[0]["contact_no"].ToString();
                lbl_heading.Text  = dt.Rows[0]["firm_name"].ToString();
                Image2.ImageUrl = dt.Rows[0]["logo"].ToString();
               
                if (dt.Rows[0]["Is_mobile_no_show_in_bill"].ToString() == "True")
                {
                    contact_no.Visible = true;
                    
                }
                try
                {
                    Image2.Visible = true;
                    if (dt.Rows[0]["Is_certificate_watermark_hide"].ToString() == "True")
                    {
                        Image2.Visible = false;
                        
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }
    }
}