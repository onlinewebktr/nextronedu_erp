using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace school_web.Payroll
{
     
    public partial class Apply_Career : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
                fetch_company_name();
                fetch_data();
            }

        } 
        private void fetch_company_name()
        {
            var dt = PayrollMy.dataTable("select * from Firm_Details ");
            if (dt.Rows.Count == 0)
            {




            }
            else
            {

                school_logo.Src = dt.Rows[0]["logo"].ToString();
                lbl_address.Text = dt.Rows[0]["address1"].ToString();
                lbl_emaiid.Text = dt.Rows[0]["email"].ToString();
                lbl_website.Text = dt.Rows[0]["website"].ToString();
                lbl_contact_details.Text = dt.Rows[0]["contact_no"].ToString();
                lbl_heading.Text = dt.Rows[0]["firm_name"].ToString();
            }
        }
        private void fetch_data()
        {
            var dt = PayrollMy.dataTable($"select  hv.HiringName,d.name ,sl.SessionId,hv.IsActive,sl.SessionName,hps.* from HR_HiringParameterSetup hps left join HR_HiringVacancy hv on hps.Vacancy_id=hv.Vacancy_id left join HR_Designation_Master  d on hps.HiringFor=d.Designation_id left join HR_SessionList sl on sl.SessionId=hv.SessionId where EndIdate>='{PayrollMy.Now.ToString("yyyyMMdd")}'");
            GrdView.DataSource = dt;
            GrdView.DataBind();
        }


        protected void GrdView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_Isactive = (Label)e.Row.FindControl("lbl_Isactive");
                Label lbl_start_Idate = (Label)e.Row.FindControl("lbl_start_Idate");
                Label lbl_end_Idate = (Label)e.Row.FindControl("lbl_end_Idate");
                Label lbl_no_seat_avl = (Label)e.Row.FindControl("lbl_no_seat_avl");

                Label lbl_no_application = (Label)e.Row.FindControl("lbl_no_application");
                Label lbl_Designation = (Label)e.Row.FindControl("lbl_Designation");
                Label lbl_Apply_for = (Label)e.Row.FindControl("lbl_Apply_for");
                Label lbl_Vacancy_Id = (Label)e.Row.FindControl("lbl_Vacancy_Id");

                Button btn_Submit = (Button)e.Row.FindControl("btn_Submit");

                if (lbl_Isactive.Text == "0")
                {
                    btn_Submit.Enabled = false;

                    e.Row.BackColor = Color.Red;

                }
                else
                {
                    int fill_no_seat = PayrollMy.data("Select count(Id) from HR_Employee_Online_Apply where   DesignationId='" + lbl_Designation.Text + "' and Hiring_id='" + lbl_Vacancy_Id.Text + "' and Payment_Status='Paid'  ").ToInt();   

                    if (Convert.ToInt32(lbl_no_application.Text) > fill_no_seat)
                    {
                        DateTime dtm = DateTime.UtcNow.AddHours(5).AddMinutes(30);
                        string Datetimesystem = dtm.ToString("dd/MM/yyyy hh:mm:ss tt");
                        if (Convert.ToInt32(lbl_end_Idate.Text) >= Convert.ToInt32(PayrollMy.Now.idate()))
                        {
                            if (Convert.ToInt32(PayrollMy.Now.idate()) >= Convert.ToInt32(lbl_start_Idate.Text))
                            {

                                btn_Submit.Enabled = true;
                                e.Row.BackColor = Color.LightGreen;

                            }
                            else// upcomming
                            {
                                btn_Submit.Enabled = false;
                                e.Row.BackColor = Color.Orange;

                            }

                        }
                        else
                        {
                            btn_Submit.Enabled = false;
                            e.Row.BackColor = Color.Red;

                        }

                    }
                    else
                    {
                        btn_Submit.Enabled = false;
                        e.Row.BackColor = Color.Red;

                    }


                }
            }
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            Button lnk = (Button)sender;
            GridViewRow row = (GridViewRow)lnk.Parent.Parent;
            Label lbl_Vacancy_Id = (Label)row.FindControl("lbl_Vacancy_Id");
            Label lbl_Designation = (Label)row.FindControl("lbl_Designation");
            Label lbl_HiringParameterId = (Label)row.FindControl("lbl_HiringParameterId");
            Label lbl_Apply_for = (Label)row.FindControl("lbl_Apply_for");
            Label lbl_Session_id = (Label)row.FindControl("lbl_Session_id");
            string url = "Apply_career_Guidelines.aspx?sdjhfdsgfhjasbdagdag=" + lbl_Designation.Text + "&sfhsdfghjdncjszhfyshf=" + lbl_Session_id.Text + "&zhfyshfcjzshdyusahds=" + lbl_HiringParameterId.Text+ "&ddrdefzshdyusahds=" + lbl_Apply_for.Text+ "&pqrshfcjzshdyusahds=" + lbl_Vacancy_Id.Text;

            Response.Redirect(url, false);


            //sdjhfdsgfhjasbdagdag=14&  Designation
            //sfhsdfghjdncjszhfyshf=1& Session
            //zhfyshfcjzshdyusahds=4& HiringParameterId
            //ddrdefzshdyusahds=Application Developer& Apply_for
            //pqrshfcjzshdyusahds=1 //Vacancy_Id
        }
    }
}