<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="registrations-form.aspx.cs" Inherits="school_web.Admin.slip.registrations_form" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registration Form</title>
    <link href="css/reg-form.css" rel="stylesheet" />
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="css/reg-form.css" rel="stylesheet" type="text/css" />');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body ></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
        jQuery(".sn-bill-head-text").fitText(0.38);
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="invoice-sec">
            <div class="prnt-btn-sec" runat="server" id="printBtns">
                <div class="prnt-btn-wpr">
                    <div class="print-btn-sec">
                        <div class="noPrint" style="float: left">
                            <asp:Button ID="btn_back" CssClass="back-btn" runat="server" OnClick="btn_back_Click" />
                        </div>
                        <div class="noPrint" style="float: right">
                            <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" CssClass="print-btn" runat="server" ToolTip="Print"></asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>


            <div id="tblPrintIQ" runat="server">
                <div class="invoice-inr-sec">
                    <div class="invoice-wpr">
                        <asp:Image ID="img_watermark" Visible="false" runat="server" class="wtr-mrk-img {{reportCardSubS[0].Is_watermark_show}}" />
                        <div class="report-card-wpr" style="height: {{reportCardSubS[0].Height_dv}}">

                            <div id="textheader" runat="server" style="width: 100%">
                                <div class="report-card-head {{reportCardSubS[0].Hdr_frst}}">
                                    <div class="report-card-left-dv">
                                        <asp:Image ID="Image1" runat="server" />
                                        <asp:Label ID="lbl_estd" runat="server" Style="display: none" class="estdTextP v-false {{reportCardSubS[0].Is_estd_show}}"></asp:Label>
                                    </div>
                                    <div class="report-card-cntr-dv">
                                        <asp:Label ID="lbl_school_name" class="report-card-schlname" runat="server"></asp:Label>
                                        <asp:Label ID="lbl_aff_text" class="report-card-schl-affno-by" runat="server"></asp:Label>
                                        <asp:Label ID="lbl_address" runat="server" class="report-card-schl-add"></asp:Label>
                                        <asp:Label ID="lbl_contact_no" runat="server" Text="" class="report-card-schl-cont v-false {{reportCardSubS[0].Is_contact_no_show}}"></asp:Label>
                                        <p class="report-card-schl-emil v-false {{reportCardSubS[0].Is_email_show}}">
                                            Email : 
                                        <asp:Label ID="lbl_email" runat="server"></asp:Label>
                                        </p>

                                        <p class="report-card-schl-emil v-false {{reportCardSubS[0].Is_website_show}}">
                                            Website : 
                                        <asp:Label ID="lbl_website" runat="server"></asp:Label>
                                        </p>
                                        <h2 class="report-card-ac-sson">Application Form (SESSION :
                                        <asp:Label ID="lbl_sessions" runat="server" Text="2026-2027"></asp:Label>)</h2>
                                    </div>


                                    <asp:Label ID="lbl_aff_no" class="report-card-schl-affno" runat="server"></asp:Label>
                                    <div class="report-card-rght-dv">
                                        <p>Affix passport size photo of the student</p>
                                    </div>

                                </div>
                            </div>

                            <div id="printheader" runat="server" style="width: 100%; text-align: center">
                                <asp:Image ID="img_header" runat="server" /> 
                            </div>




                            <div class="form-wprs-dv">
                                <h2 class="form-wprs-dv-title-h">Personal details of the student</h2>
                                <div class="form-wprs-contnt-dv">
                                    <div style="float: left; width: 100%;" id="personaldetails" runat="server">
                                        <div class="form-wprs-contnt-row">
                                            <div class="wdth50" style="width: 80%;">
                                                <p class="form-wprs-contnt-p">
                                                </p>
                                            </div>
                                            <div class="wdth50" style="width: 20%;">
                                                <p class="form-wprs-contnt-p">
                                                    Date  :
                                                <asp:Label ID="lbl_date" runat="server" Style="width: 73%; right: 0px;"></asp:Label>
                                                </p>
                                            </div>
                                        </div>


                                        <div class="form-wprs-contnt-row">
                                            <div class="wdth50">
                                                <p class="form-wprs-contnt-p">
                                                    Application No.  :
                                                <asp:Label ID="lbl_application_no" runat="server" Style="width: 67%;"></asp:Label>
                                                </p>
                                            </div>
                                            <div class="wdth50">
                                                <p class="form-wprs-contnt-p">
                                                    Seeking admission to class  :
                                                <asp:Label ID="lbl_adm_class" runat="server" Style="width: 48%; right: 0px;"></asp:Label>
                                                </p>
                                            </div>
                                        </div>

                                        <div class="form-wprs-contnt-row">
                                            <div class="wdth100">
                                                <p class="form-wprs-contnt-p">
                                                    Candidate Name :
                                                <asp:Label ID="lbl_candidates_name" runat="server" Style="right: 0px; width: 84%;"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                        <div class="form-wprs-contnt-row">
                                            <div class="wdth50">
                                                <p class="form-wprs-contnt-p">
                                                    Father's Name :
                                                <asp:Label ID="lbl_father_name" runat="server" Style="width: 66%;"></asp:Label>
                                                </p>
                                            </div>
                                            <div class="wdth50">
                                                <p class="form-wprs-contnt-p">
                                                    Mother's Name :
                                                <asp:Label ID="lbl_mother_name" runat="server" Style="right: 0px; width: 70%;"></asp:Label>
                                                </p>
                                            </div>
                                        </div>

                                        <div class="form-wprs-contnt-row">
                                            <div class="wdth100">
                                                <p class="form-wprs-contnt-p">
                                                    Guardian's Name :
                                                <asp:Label ID="lbl_guardian_name" runat="server" Style="right: 0px; width: 84%;"></asp:Label>
                                                </p>
                                            </div>
                                        </div>

                                        <div class="form-wprs-contnt-row">
                                            <div class="wdth33" style="width: 40%;">
                                                <p class="form-wprs-contnt-p">
                                                    Nationality  :
                                                <asp:Label ID="lbl_nationality" runat="server" Style="width: 67%;"></asp:Label>
                                                </p>
                                            </div>
                                            <div class="wdth33" style="width: 44%;">
                                                <p class="form-wprs-contnt-p">
                                                    Date of Birth (dd/mm/yyyy)  :
                                                <asp:Label ID="lbl_dob" runat="server" Style="width: 36%;"></asp:Label>
                                                </p>
                                            </div>
                                            <div class="wdth33" style="width: 16%;">
                                                <p class="form-wprs-contnt-p">
                                                    Age  :
                                                <asp:Label ID="lbl_age" runat="server" Style="width: 70%; right: 0px;"></asp:Label>
                                                </p>
                                            </div>
                                        </div>

                                        <div class="form-wprs-contnt-row">
                                            <div class="wdth100">
                                                <p class="form-wprs-contnt-p">
                                                    Place of birth : <span style="right: 0px; width: 86%;"></span>
                                                </p>
                                            </div>
                                        </div>

                                        <div class="form-wprs-contnt-row">
                                            <div class="wdth33">
                                                <p class="form-wprs-contnt-p">
                                                    Height  :
                                                <asp:Label ID="lbl_height" runat="server" Style="width: 75%;"></asp:Label><i class="othrinfos">cms</i>
                                                </p>
                                            </div>
                                            <div class="wdth33">
                                                <p class="form-wprs-contnt-p">
                                                    Weight  :
                                                <asp:Label ID="lbl_weight" runat="server" Style="width: 73%;"></asp:Label><i class="othrinfos">kg.</i>
                                                </p>
                                            </div>
                                            <div class="wdth33">
                                                <p class="form-wprs-contnt-p">
                                                    Blood Group  :
                                                <asp:Label ID="lbl_blood_group" runat="server" Style="width: 62%; right: 0px;"></asp:Label>
                                                </p>
                                            </div>
                                        </div>

                                        <div class="form-wprs-contnt-row">
                                            <div class="wdth50">
                                                <p class="form-wprs-contnt-p">
                                                    Category (General/OBC/SC/ST) :
                                                <asp:Label ID="lbl_category" runat="server" Style="width: 37%;"></asp:Label>
                                                </p>
                                            </div>
                                            <div class="wdth50">
                                                <p class="form-wprs-contnt-p">
                                                    Religion  :
                                                <asp:Label ID="lbl_religion" runat="server" Style="width: 80%; right: 0px;"></asp:Label>
                                                </p>
                                            </div>
                                        </div>

                                        <div class="form-wprs-contnt-row">
                                            <div class="wdth100">
                                                <p class="form-wprs-contnt-p">
                                                    Gender (Male/Female/Transgender) :
                                                <asp:Label ID="lbl_gender" runat="server" Style="width: 65%; right: 0px;"></asp:Label>
                                                </p>
                                            </div>
                                        </div>

                                        <div class="form-wprs-contnt-row">
                                            <div class="wdth50">
                                                <p class="form-wprs-contnt-p">
                                                    Aadhaar No.  :
                                                <asp:Label ID="lbl_aadhar_no" runat="server" Style="width: 75%;"></asp:Label>
                                                </p>
                                            </div>
                                            <div class="wdth50">
                                                <p class="form-wprs-contnt-p">
                                                    Mother Tongue   :
                                                <asp:Label ID="lbl_mother_tongue" runat="server" Style="width: 68%; right: 0px;"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                        <div class="form-wprs-contnt-row">
                                            <div class="wdth100">
                                                <p class="form-wprs-contnt-p">
                                                    Second Language : 
                                                <asp:Label ID="lbl_Second_Language" runat="server" Style="right: 0px; width: 83%;"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                        <div class="form-wprs-contnt-row" id="fPhoneNodV" runat="server">
                                            <div class="wdth50">
                                                <p class="form-wprs-contnt-p">
                                                    Father's Phone No.  :
                                                <asp:Label ID="Label12" runat="server" Style="width: 60%;"></asp:Label>
                                                </p>
                                            </div>
                                            <div class="wdth50">
                                                <p class="form-wprs-contnt-p">
                                                    Mother's Phone No.  :
                                                <asp:Label ID="Label13" runat="server" Style="right: 0px; width: 66%;"></asp:Label>
                                                </p>
                                            </div>
                                        </div>

                                        <div class="form-wprs-contnt-row" id="localGuardianNo" runat="server">
                                            <div class="wdth100">
                                                <p class="form-wprs-contnt-p">
                                                    Local Guardian's Phone No. : 
                                                <asp:Label ID="Label14" runat="server" Style="right: 0px; width: 75%;"></asp:Label>
                                                </p>
                                            </div>
                                        </div>

                                        <div class="form-wprs-contnt-row" id="languageSpokentAtHomeDv" runat="server">
                                            <div class="wdth100">
                                                <p class="form-wprs-contnt-p">
                                                    Language spoken at home : 
                                                <asp:Label ID="lbl_language_spoken" runat="server" Style="right: 0px; width: 75%;"></asp:Label>
                                                </p>
                                            </div>
                                        </div>

                                        <div class="form-wprs-contnt-row" id="parentAccNoDv" runat="server">
                                            <div class="wdth50">
                                                <p class="form-wprs-contnt-p">Parent’s Account Details  :  <span style="width: 50%;"></span></p>
                                            </div>
                                            <div class="wdth50">
                                                <p class="form-wprs-contnt-p">
                                                    Student’s Account Details : <span style="width: 48%; right: 0px;"></span>
                                                </p>
                                            </div>
                                        </div>


                                        <div class="form-wprs-contnt-row" id="doe" runat="server" visible="false">
                                            <div class="wdth50" style="width: 30%;">
                                                <p class="form-wprs-contnt-p">
                                                    Exam Date  :
                                                    <asp:Label ID="lbl_date_exam" runat="server" Style="width: 62%; right: 0px;"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                        <div class="tablepagee-sec" id="Sibling_Details" runat="server" visible="false">
                                            <table style="width: 100%;">
                                                <tr class="bgcolorS">
                                                    <th colspan="5">Sibling Details</th>
                                                </tr>
                                                <tr>
                                                    <td style="width: 80px;">S. No.</td>
                                                    <td>Name of Sibling</td>
                                                    <td>Age</td>
                                                    <td>School/College</td>
                                                    <td>Class</td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 30px">1</td>
                                                    <td>
                                                        <asp:Label ID="lbl_name_of_sibling" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbl_age_of_sibling" runat="server"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lbl_school_of_sibling" runat="server"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lbl_class_of_sibling" runat="server"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 30px">2</td>
                                                    <td>
                                                        <asp:Label ID="lbl_name_of_sibling1" runat="server"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lbl_age_of_sibling1" runat="server"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lbl_school_of_sibling1" runat="server"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lbl_class_of_sibling1" runat="server"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 30px">3</td>
                                                    <td>
                                                        <asp:Label ID="Label4" runat="server"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="Label5" runat="server"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="Label6" runat="server"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="Label7" runat="server"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 30px">4</td>
                                                    <td>
                                                        <asp:Label ID="Label8" runat="server"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="Label9" runat="server"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="Label10" runat="server"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="Label11" runat="server"></asp:Label></td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div class="tablepagee-sec">
                                            <table style="width: 100%;">
                                                <tr class="bgcolorS">
                                                    <th colspan="8">Particulars Of Family</th>
                                                </tr>
                                                <tr>
                                                    <td style="width: 10%;">Particular</td>
                                                    <td style="width: 20%;">Aadhaar No.</td>
                                                    <td>Age</td>
                                                    <td style="width: 9%;">Edu. Qualification </td>

                                                    <td>Occupation</td>
                                                    <td runat="server" id="FannualIncome">Annual Income</td>
                                                    <td style="width: 16%;" runat="server" id="FContactNo">Contact No.</td>
                                                    <td style="width: 26%;">Email ID</td>
                                                </tr>

                                                <tr>
                                                    <td style="height: 48px; vertical-align: top;">Father</td>
                                                    <td>
                                                        <asp:Label ID="lbl_f_aadhar" runat="server"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="Label1" runat="server"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lbl_f_qualification" runat="server"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lbl_f_occupation" runat="server"></asp:Label></td>
                                                    <td runat="server" id="FannualIncome1">
                                                        <asp:Label ID="lbl_f_annual_income" runat="server"></asp:Label></td>
                                                    <td runat="server" id="FContactNo1">
                                                        <asp:Label ID="lbl_f_contact_no" runat="server"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lbl_f_email" runat="server"></asp:Label></td>
                                                </tr>

                                                <tr>
                                                    <td style="height: 48px; vertical-align: top;">Mother</td>
                                                    <td>
                                                        <asp:Label ID="lbl_m_aadhar" runat="server"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="Label2" runat="server"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lbl_m_qualification" runat="server"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lbl_m_occupation" runat="server"></asp:Label></td>
                                                    <td runat="server" id="FannualIncome2">
                                                        <asp:Label ID="lbl_m_annual_income" runat="server"></asp:Label></td>
                                                    <td runat="server" id="FContactNo2">
                                                        <asp:Label ID="lbl_m_contact_no" runat="server"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lbl_m_email" runat="server"></asp:Label></td>
                                                </tr>

                                                <tr>
                                                    <td style="height: 48px; vertical-align: top;">Local Guardian</td>
                                                    <td>
                                                        <asp:Label ID="lbl_g_aadhar" runat="server"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="Label3" runat="server"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lbl_g_qualification" runat="server"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lbl_g_occupation" runat="server"></asp:Label></td>
                                                    <td runat="server" id="FannualIncome3">
                                                        <asp:Label ID="lbl_g_annual_income" runat="server"></asp:Label></td>
                                                    <td runat="server" id="FContactNo3">
                                                        <asp:Label ID="lbl_g_contact_no" runat="server"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lbl_g_email" runat="server"></asp:Label></td>
                                                </tr>

                                            </table>
                                        </div>
                                        <div class="tablepagee-sec" id="Previous_Classes" runat="server" visible="false" style="display: none">
                                            <table style="width: 100%;">

                                                <tr class="bgcolorS">
                                                    <th colspan="10">Record of Previous Classes Attended</th>
                                                </tr>
                                                <tr>
                                                    <td rowspan="2" style="width: 80px;">S.No.</td>
                                                    <td rowspan="2">Name of School</td>
                                                    <td rowspan="2">Address of School</td>
                                                    <td colspan="2">Class</td>
                                                    <td colspan="2">Year</td>
                                                    <td rowspan="2">Board  CBSE/ISE/State </td>
                                                    <td rowspan="2">Medium of Instruction</td>
                                                    <td rowspan="2">% of Marks</td>
                                                </tr>
                                                <tr>
                                                    <td>From</td>
                                                    <td>To</td>
                                                    <td>From</td>
                                                    <td>To</td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 30px">1</td>
                                                    <td>
                                                        <asp:Label ID="lbl_prev_school_name" runat="server"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lbl_prev_school_address" runat="server"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lbl_prev_school_from_class" runat="server"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lbl_prev_school_to_class" runat="server"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lbl_prev_school_from_year" runat="server"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lbl_prev_school_to_year" runat="server"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lbl_prev_school_board" runat="server"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lbl_medium" runat="server"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lbl_percent" runat="server"></asp:Label></td>
                                                </tr>

                                                <tr>
                                                    <td style="height: 30px">2</td>
                                                    <td>
                                                        <asp:Label ID="lbl_prev_school_name1" runat="server"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lbl_prev_school_address1" runat="server"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lbl_prev_school_from_class1" runat="server"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lbl_prev_school_to_class1" runat="server"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lbl_prev_school_from_year1" runat="server"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lbl_prev_school_to_year1" runat="server"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lbl_prev_school_board1" runat="server"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lbl_medium1" runat="server"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lbl_percent1" runat="server"></asp:Label></td>
                                                </tr>
                                            </table>
                                        </div>

                                        <div class="tablepagee-sec" style="display: none">
                                            <table style="width: 100%;">
                                                <tr class="bgcolorS">
                                                    <th colspan="4">Why do now wish to enroll your child in this school</th>
                                                </tr>

                                                <tr>
                                                    <td>a) Transfer Case</td>
                                                    <td style="width: 80px;"></td>
                                                    <td>f) Parental Engagement</td>
                                                    <td style="width: 80px;"></td>
                                                </tr>

                                                <tr>
                                                    <td>b) Integrated Curriculum At all levels		</td>
                                                    <td></td>
                                                    <td>g)	Siblings</td>
                                                    <td></td>
                                                </tr>

                                                <tr>
                                                    <td>c) Visibility of School</td>
                                                    <td></td>
                                                    <td>h)	Programmes at School</td>
                                                    <td></td>
                                                </tr>

                                                <tr>
                                                    <td>d) Co-Curricular activities at School		</td>
                                                    <td></td>
                                                    <td>i) Sports activities offered at School</td>
                                                    <td></td>
                                                </tr>

                                                <tr>
                                                    <td>e) Not satisfied with the previous school		</td>
                                                    <td></td>
                                                    <td>j) Nearness to place of residence/Home</td>
                                                    <td></td>
                                                </tr>

                                                <tr>
                                                    <td></td>
                                                    <td></td>
                                                    <td>K) Any other</td>
                                                    <td></td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>



                                    <div style="float: left; width: 100%;" runat="server" id="Achievementid">
                                        <div class="tablepagee-sec" style="display: none">
                                            <table style="width: 100%;">
                                                <tr class="bgcolorS">
                                                    <th colspan="6">Medical Details</th>
                                                </tr>
                                                <tr>
                                                    <td>Physical Disability</td>
                                                    <td style="width: 100px;"></td>
                                                    <td>Past Illness</td>
                                                    <td style="width: 80px;"></td>
                                                    <td>Chrome Medical Problem</td>
                                                    <td style="width: 80px;"></td>
                                                </tr>
                                                <tr>
                                                    <td>Mental Illness</td>
                                                    <td></td>
                                                    <td>Ailergy</td>
                                                    <td></td>
                                                    <td>Any Other</td>
                                                    <td></td>
                                                </tr>

                                                <tr>
                                                    <td>Details</td>
                                                    <td></td>
                                                    <td colspan="3"></td>
                                                    <td></td>
                                                </tr>
                                            </table>
                                        </div>

                                        <div class="tablepagee-sec" runat="server" visible="false" id="Achievement" style="display: none">
                                            <table style="width: 100%;">
                                                <tr class="bgcolorS">
                                                    <th colspan="6">Field of Interest in academic/Sports/Co-Curricular field</th>
                                                </tr>
                                                <tr>
                                                    <td>S. No.</td>
                                                    <td>Field</td>
                                                    <td>Year</td>
                                                    <td>Event </td>
                                                    <td>Details of Prize/Awards/Position</td>
                                                    <td style="width: 80px;"></td>
                                                </tr>

                                                <tr>
                                                    <td style="height: 30px;">1</td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                </tr>

                                                <tr>
                                                    <td style="height: 30px;">2</td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 30px;">3</td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                </tr>

                                            </table>
                                        </div>



                                        <div class="tablepagee-sec">
                                            <table style="width: 100%;">
                                                <tr class="bgcolorS">
                                                    <th colspan="3">Address</th>
                                                </tr>
                                                <tr>
                                                    <td style="width: 38%">Present Address</td>
                                                    <td style="width: 33.33%">Permanent Address</td>
                                                    <td style="width: 33.33%">Local Guardian’s Address</td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 70px; vertical-align: top">
                                                        <asp:Label ID="lbl_p_address" runat="server"></asp:Label>
                                                    </td>
                                                    <td></td>
                                                    <td></td>
                                                </tr>
                                            </table>
                                        </div>

                                        <div class="tablepagee-sec" id="about_School" runat="server" visible="false" style="display: none">
                                            <table style="width: 100%;">
                                                <tr class="bgcolorS">
                                                    <th colspan="4">How did you come to know about School</th>
                                                </tr>
                                                <tr>
                                                    <td>A)	Radio</td>
                                                    <td style="width: 150px"></td>
                                                    <td>H) Pamphlets</td>
                                                    <td style="width: 150px"></td>
                                                </tr>

                                                <tr>
                                                    <td>B)	Neighbour</td>
                                                    <td></td>
                                                    <td>I) Television</td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td>C)	News Paper</td>
                                                    <td></td>
                                                    <td>J) Facebook</td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td>D)	Website</td>
                                                    <td></td>
                                                    <td>K) Google Advertisement</td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td>E)	Hoarding</td>
                                                    <td></td>
                                                    <td>L) Word of Mouth</td>
                                                    <td></td>
                                                </tr>

                                                <tr>
                                                    <td>F) Sibling</td>
                                                    <td></td>
                                                    <td>M) Alumni</td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td>G) Friends</td>
                                                    <td></td>
                                                    <td>N) School Exhibition</td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td></td>
                                                    <td>O) If any other, please specify</td>
                                                    <td></td>
                                                </tr>
                                            </table>
                                        </div>


                                        <div class="tablepagee-sec" id="Knowabout_School" runat="server" visible="false" style="display: none">
                                            <table style="width: 100%;">
                                                <tr class="bgcolorS">
                                                    <th colspan="4">How did you come to know about School</th>
                                                </tr>
                                                <tr>
                                                    <td>A)	Neighbour</td>
                                                    <td style="width: 15%;"></td>
                                                    <td>G) Pamphlets</td>
                                                    <td style="width: 150px"></td>
                                                </tr>
                                                <tr>
                                                    <td>B)	Website</td>
                                                    <td></td>
                                                    <td>H) Facebook</td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td>C)	Hoarding</td>
                                                    <td></td>
                                                    <td>I) Google Advertisement</td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td>D) Sibling</td>
                                                    <td></td>
                                                    <td>J) Word of Mouth</td>
                                                    <td></td>
                                                </tr>

                                                <tr>
                                                    <td>E) Alumni</td>
                                                    <td></td>
                                                    <td>K) If any other, please specify</td>
                                                    <td></td>
                                                </tr>

                                                <tr>
                                                    <td>F) Friends</td>
                                                    <td></td>

                                                    <td></td>
                                                    <td></td>
                                                </tr>
                                            </table>
                                        </div>

                                        <div class="tablepagee-sec" id="docSubmitted" runat="server">
                                            <table style="width: 100%;">
                                                <tr class="bgcolorS">
                                                    <th colspan="4">The following documents to be submitted at the time of admission, failing which the admission stands cancelled</th>
                                                </tr>
                                                <tr>
                                                    <td>A) Photocopy of birth certificate</td>
                                                    <td style="width: 100px"></td>
                                                    <td>F) Transfer Certificate original</td>
                                                    <td style="width: 100px"></td>
                                                </tr>

                                                <tr>
                                                    <td>B) Photocopy of Address Proof</td>
                                                    <td></td>
                                                    <td>G) Photocopy of Aadhaar Card of Parents</td>
                                                    <td></td>
                                                </tr>

                                                <tr>
                                                    <td>C) Photocopy of Last 2 years Report Card (for class 2 and Awards)/10th</td>
                                                    <td></td>
                                                    <td>H) Medical Report</td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td>D) Aadhaar Card of Student</td>
                                                    <td></td>
                                                    <td>I) In Case of OBC/SC/ST (Relevant attested documents)</td>
                                                    <td></td>
                                                </tr>


                                                <tr>
                                                    <td>E) Family Photo</td>
                                                    <td></td>
                                                    <td>J)	Disability Certificate (If any)</td>
                                                    <td></td>
                                                </tr>



                                            </table>
                                            <div class="form-wprs-contnt-row">
                                                <div class="wdth100">
                                                    <p class="form-wprs-contnt-p">
                                                        If the parents are divorced, living separately, or widowed, with whom is the child living? 
                                                <br />
                                                        <br />
                                                        <asp:Label ID="lbl1" runat="server" Style="right: 0px; width: 100%; margin-top: 16px; float: left; top: 29px;"></asp:Label>
                                                    </p>
                                                </div>
                                            </div>

                                            <div style="float: left; width: 100%;" id="subject_takenpnl" runat="server">

                                                <div class="tablepagee-sec" id="subject_taken" runat="server" visible="false" style="display: none">
                                                    <table style="width: 100%;">
                                                        <tr class="bgcolorS">
                                                            <th colspan="8">Subjects Taken</th>
                                                        </tr>
                                                        <tr>
                                                            <td>English</td>
                                                            <td style="width: 60px"></td>
                                                            <td>Hindi</td>
                                                            <td style="width: 60px"></td>
                                                            <td>Bengali</td>
                                                            <td style="width: 60px"></td>
                                                            <td>Physics</td>
                                                            <td style="width: 60px"></td>
                                                        </tr>
                                                        <tr>
                                                            <td>Chemistry</td>
                                                            <td></td>
                                                            <td>Biology</td>
                                                            <td></td>
                                                            <td>Mathematics</td>
                                                            <td></td>
                                                            <td>Accountancy</td>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td>Business Studies</td>
                                                            <td></td>
                                                            <td>Economics</td>
                                                            <td></td>
                                                            <td>Sankrit</td>
                                                            <td></td>
                                                            <td>History</td>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td>Sociology</td>
                                                            <td></td>
                                                            <td>Political Science</td>
                                                            <td></td>
                                                            <td>Geography</td>
                                                            <td></td>
                                                            <td>Computer Science</td>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td>Informatics Practices</td>
                                                            <td></td>
                                                            <td>Home Science</td>
                                                            <td></td>
                                                            <td>Physical Education</td>
                                                            <td></td>
                                                            <td>Commercial Arts</td>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td>Entrepreneurship</td>
                                                            <td></td>
                                                            <td>Psychology</td>
                                                            <td></td>
                                                            <td>Science</td>
                                                            <td></td>
                                                            <td>EVS</td>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td>Social Science</td>
                                                            <td></td>
                                                            <td>Commerce</td>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                            <td></td>
                                                        </tr>
                                                    </table>
                                                </div>

                                                <h1 class="themevg">Undertaking</h1>
                                                <p class="textorginp22">
                                                    <b>*hereby certify the following:</b>
                                                </p>





                                                <div class="rules-mainse-box" style="margin: -1px 0px 2px 0px;">
                                                    <ol type="a">
                                                        <li>Come to school on time and regularly.</li>
                                                        <li>Wear proper uniform and ID card daily.</li>
                                                        <li>Respect teachers, staff, and classmates.</li>
                                                        <li>Complete homework and bring books as per timetable.</li>
                                                        <li>Keep the school clean; do not damage property.</li>
                                                        <li>Mobile phones and costly items are not allowed.</li>
                                                        <li>Maintain discipline in class and in transport.</li>
                                                        <li>Parents must sign the diary and attend meetings.</li> 
                                                        <li>Fee must be paid between 1st to 7th of every month.</li>
                                                        <li>One month grace period allowed; after that ₹50 per month late fine.</li>
                                                        <li>5% discount if fee is paid on or before the 1st of every month.</li> 
                                                    </ol>
                                                </div>


                                                <div class="form-wprs-contnt-row">
                                                    <div class="report-card-rght-dv" style="margin: 1px 0px 0px 23px;">
                                                        <p>Affix a passport-size photo of the father</p>
                                                    </div>
                                                    <div class="report-card-rght-dv" style="margin: 0px 0px 18px 89px;">
                                                        <p>Affix a passport-size photo of the mother</p>
                                                    </div>
                                                    <div class="report-card-rght-dv" style="margin: 0px 0px 18px 93px;">
                                                        <p>
                                                            Photo of the local guardian<br />
                                                            If needed.
                                                        </p>
                                                    </div>
                                                </div>
                                                <div class="form-wprs-contnt-row">


                                                    <div class="wdth33" style="width: 25%;">
                                                        <p class="form-wprs-contnt-p" style="margin: -11px 0px 0px 0px;">
                                                            ..................................................
                                                <br />
                                                            <b>Signature of Father</b>
                                                        </p>
                                                    </div>
                                                    <div class="wdth33" style="width: 25%;">
                                                        <p class="form-wprs-contnt-p" style="margin: -11px 0px 0px 0px;">
                                                            ..................................................
                                                <br />
                                                            <b>Signature of Mother</b>
                                                        </p>
                                                    </div>
                                                    <div class="wdth33" style="width: 25%;">
                                                        <p class="form-wprs-contnt-p" style="margin: -11px 0px 0px 0px;">
                                                            ..................................................
                                                <br />
                                                            <b>Signature of Guardian</b>
                                                        </p>
                                                    </div>
                                                    <div class="wdth33" style="width: 25%;">
                                                        <p class="form-wprs-contnt-p" style="margin: -11px 0px 0px 0px;">
                                                            ..................................................
                                                <br />
                                                            <b>Signature of Candidate</b>
                                                        </p>
                                                    </div>
                                                </div>




                                                <h2 class="form-wprs-dv-title-h">For office use only</h2>
                                                <div class="form-wprs-contnt-row">
                                                    <div class="wdth50">
                                                        <p class="form-wprs-contnt-p"><b>Application Verified by:</b>..................................................................</p>
                                                    </div>
                                                    <div class="wdth50">
                                                        <p class="form-wprs-contnt-p">
                                                            <b>Sign:</b>..................................................................
                                                        </p>
                                                    </div>
                                                </div>
                                                <div class="form-wprs-contnt-row">
                                                    <div class="wdth50">
                                                        <p class="form-wprs-contnt-p"><b>REGISTRATION NO.:</b>....................................................................................</p>
                                                    </div>
                                                    <div class="wdth50">
                                                        <p class="form-wprs-contnt-p">
                                                            <b>Date:</b> ................................................................ 
                                                        </p>
                                                    </div>
                                                </div>
                                                <div class="form-wprs-contnt-row">
                                                    <div class="wdth50">
                                                        <p class="form-wprs-contnt-p">
                                                            <b>Place:</b> ............................................................................................................
                                                        </p>
                                                    </div>


                                                </div>


                                                <div class="form-wprs-contnt-row" style="margin: 0px 0px 8px 0px;">

                                                    <div class="wdth100">
                                                        <p class="form-wprs-contnt-p" style="text-align: right; margin: 5px 0px 0px 0px;">
                                                            ..........................................................
                                                <br />
                                                            <b>Signature of Principal</b>
                                                        </p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="tablepagee-sec" id="docSubmitted1" runat="server" visible="false">
                                            <table style="width: 100%;">
                                                <tr class="bgcolorS">
                                                    <th colspan="4">The following documents to be submitted at the time of admission, failing which the admission stands cancelled</th>
                                                </tr>
                                                <tr>
                                                    <td>A) Photocopy of birth certificate</td>
                                                    <td style="width: 100px"></td>
                                                    <td>E) Transfer Certificate original</td>
                                                    <td style="width: 100px"></td>
                                                </tr>
                                                <tr>
                                                    <td>B) Photocopy of Address Proof</td>
                                                    <td></td>
                                                    <td>F) Photocopy of Aadhaar Card of Parents</td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td>C) Aadhaar Card of Student</td>
                                                    <td></td>
                                                    <td>G) In Case of OBC/SC/ST (Relevant attested documents)</td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td>D) Family Photo</td>
                                                    <td></td>
                                                    <td>H)	Disability Certificate (If any)</td>
                                                    <td></td>
                                                </tr>
                                            </table>
                                            <div class="form-wprs-contnt-row">
                                                <div class="wdth100">
                                                    <p class="form-wprs-contnt-p">
                                                        If the parents are divorced, living separately, or widowed, with whom is the child living? 
                                                <br />
                                                        <br />
                                                        <asp:Label ID="Label15" runat="server" Style="right: 0px; width: 100%; margin-top: 16px; float: left; top: 29px;"></asp:Label>
                                                    </p>
                                                </div>
                                            </div>
                                            <p class="textorginp" style="margin: 0px 0px 6px 0px;">
                                                Original TC and Photocopy of Mark Sheet of previous class to be submitted with 10 days of beginning of the new academic session for which the admission of your ward has been thought for.
                                            </p>


                                            <div class="form-wprs-contnt-row">
                                                <div class="wdth100">
                                                    <p class="form-wprs-contnt-p">
                                                        Transportion or child collection form school
                                                <br />
                                                        <br />

                                                    </p>
                                                    <style>
                                                        .slide-box {
                                                            width: 47px;
                                                            height: 30px;
                                                            border: 1px solid #000;
                                                        }
                                                    </style>
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td style="border: 0px solid #000;">Guardian
                                                            </td>
                                                            <td class="slide-box"></td>

                                                            <td style="border: 0px solid #000;">child himself 
                                                            </td>
                                                            <td class="slide-box"></td>
                                                            <td style="border: 0px solid #000;">School Van
                                                            </td>

                                                            <td class="slide-box"></td>

                                                            <td style="border: 0px solid #000;">Privat Auto/ VAN

                                                            </td>
                                                            <td class="slide-box"></td>

                                                        </tr>

                                                    </table>
                                                </div>
                                            </div>



                                        </div>
                                    </div>



                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
