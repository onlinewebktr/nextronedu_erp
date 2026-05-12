<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="student-details.aspx.cs" Inherits="school_web.Admin.student_details" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print</title>
    <link href="https://fonts.googleapis.com/css2?family=Dosis:wght@200;300;400;500;600;700;800&display=swap" rel="stylesheet" />
    <link href="../assets/css/Print.css" rel="stylesheet" />
    <script src="../assets/js/jquery-1.10.2.min.js"></script>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="../assets/css/Print.css" rel="stylesheet" type="text/css" /><link href="https://fonts.googleapis.com/css2?family=Dosis:wght@200;300;400;500;600;700;800&display=swap" rel="stylesheet"/>');
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

    <style>
        .print_form-inr {
            margin: 0px 0px 30px 0px;
            padding: 10px 10px;
            width: 100%;
            height: auto;
            float: left;
            border: 1px solid #ddd;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <section class="section">
            <div class="print_form">
                <div class="prnt-btn-sec">
                    <div class="prnt-btn-sec-cntr">
                        <div class="print-btn-sec">
                            <div style="float: left">
                                <asp:Button ID="btn_back" runat="server" ToolTip="Back" CssClass="back-btn" OnClick="btn_back_Click" />
                            </div>
                            <div style="float: right">
                                <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" runat="server" ToolTip="Print" CssClass="print-btn"></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="print-pg-sec" id="tblPrintIQ" runat="server">
                    <div class="print_form-inr">
                        <div class="print_form-img">
                            <div class="t-crtifcate-hdr-sec">
                                <div class="t-crtifcate-logo-sec">
                                    <asp:Image ID="Image1" runat="server" style="width: 70%;" />
                                </div>
                                <div class="t-crtifcate-hdr-contnt-sec">
                                    <asp:Label ID="lbl_school_name" class="t-certificate-comp-name-h" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lbl_address" runat="server" Text="" class="t-certificate-comp-add-p"></asp:Label>
                                    <asp:Label ID="lbl_contact_no" runat="server" Text="" class="t-certificate-comp-add-p"></asp:Label>
                                    <p class="t-certificate-comp-mail-p">
                                        Email : 
                                        <asp:Label ID="lbl_email_school" runat="server" Text=""></asp:Label>
                                    </p>
                                </div>
                            </div>
                            <div class="certificate-type-name-t-sec">
                                <h1 class="t-certificate-type-name-h">Student Details -
                                    <asp:Label ID="lbl_session1" runat="server" Text=""></asp:Label></h1>
                            </div>
                        </div>
                        <div class="print_form-dtls">
                            <div class="content-wpr-pnrt">
                                <table class="print_table">
                                    <tr>
                                        <td colspan="4">
                                            <h2 class="messbox-sec-h2" style="width: 100%;">Admission Information</h2>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="fontwt400">Student Type : </td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_student_type" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td class="fontwt400">Admission date :</td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_admissiondate" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="fontwt400">Discount Group :</td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_cat" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td class="fontwt400">Discount Sub-Group :</td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_sub_cat" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="fontwt400">Form Sl.No. : </td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_formno" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td class="fontwt400">UID/Board/APAAR No. :</td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_uid_no" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="fontwt400">Index No. :</td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_index_no" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td class="fontwt400">Student's PEN No. :</td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_pen_no" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="fontwt400">Session :</td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_session" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td class="fontwt400">Admission in :</td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_admissionin" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="fontwt400">Boarding Type :</td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_boarding_type" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td class="fontwt400">Standard :</td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_course" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="fontwt400">Section :</td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_section" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td class="fontwt400">Admission no.  :</td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_admissionno" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td class="fontwt400">Roll No. :</td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_rollno" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td class="fontwt400">Transportation :</td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_transportation" runat="server" Text=""></asp:Label>
                                            <asp:Label ID="lbl_academic_year" runat="server" Visible="false" Text=""></asp:Label>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td class="fontwt400">House :</td>
                                        <td colspan="3">
                                            <asp:Label ID="lbl_house" class="fontwt700" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <%-- ======================= --%>
                                <table class="print_table">
                                    <tr>
                                        <td colspan="5">
                                            <h2 class="messbox-sec-h2" style="width: 100%;">Student Information</h2>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="fontwt400">Student Name : </td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_student_name" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td class="fontwt400">Date of Birth :</td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_dateofbirth" runat="server" Text=""></asp:Label>
                                        </td>

                                        <td rowspan="5" style="width: 130px; padding: 1px 1px;">
                                            <asp:Image ID="img_s_image" runat="server" class="studnt-img" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="fontwt400">Birth Certificate no. : </td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_CertificateNostud" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td class="fontwt400">Place of Birth :</td>
                                        <td class="fontwt700" colspan="2">
                                            <asp:Label ID="lbl_palceof_birth" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="fontwt400">Gender : </td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_gender" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td class="fontwt400">Blood Group : </td>
                                        <td class="fontwt700" colspan="2">
                                            <asp:Label ID="lbl_bloodgroup" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="fontwt400">Nationalty : </td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_nationalty" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td class="fontwt400">Religion :</td>
                                        <td class="fontwt700" colspan="2">
                                            <asp:Label ID="lbl_religion" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="fontwt400">Ration Type : </td>
                                        <td>
                                            <asp:Label ID="lbl_ration_type" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td class="fontwt400">Category :</td>
                                        <td class="fontwt700" colspan="2">
                                            <asp:Label ID="lbl_catogery" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="fontwt400">Caste : </td>
                                        <td colspan="5">
                                            <asp:Label ID="lbl_caste_jati" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="fontwt400">Caste Certificate No. : </td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_certificateno" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td class="fontwt400">Aadhar No. :</td>
                                        <td class="fontwt700" colspan="2">
                                            <asp:Label ID="lbl_aadharno" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td class="fontwt400">Mother Tongue :</td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_mother_tongue" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td class="fontwt400">If any illness : </td>
                                        <td class="fontwt700" colspan="2">
                                            <asp:Label ID="lbl_anyillness" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="fontwt400">illness Remark :</td>
                                        <td class="fontwt700" colspan="4">
                                            <asp:Label ID="lbl_Illness_Remark" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="fontwt400">Previous School : </td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_prev_school" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td class="fontwt400">Caste :</td>
                                        <td class="fontwt700" colspan="2">
                                            <asp:Label ID="lbl_cast" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="fontwt400">RTE Student : </td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_rte_student" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td class="fontwt400">Staff Ward :</td>
                                        <td class="fontwt700" colspan="2">
                                            <asp:Label ID="lbl_staff_ward" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="fontwt400">Height : </td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_height" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td class="fontwt400">Weight :</td>
                                        <td class="fontwt700" colspan="2">
                                            <asp:Label ID="lbl_weight" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="fontwt400">Birth mark : </td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_identy" runat="server" Text=""></asp:Label>
                                        </td>

                                        <td class="fontwt400">Hobbies/Field of interest :</td>
                                        <td class="fontwt700" colspan="2">
                                            <asp:Label ID="lbl_hobbie" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>

                                <table class="print_table">
                                    <tr>
                                        <td colspan="5">
                                            <h2 class="messbox-sec-h2" style="width: 100%;">Sibling Details</h2>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th style="text-align: left;">S. No.</th>
                                        <th style="text-align: left;">Name of Sibling</th>
                                        <th style="text-align: left;">Age</th>
                                        <th style="text-align: left;">School/College</th>
                                        <th style="text-align: left;">Class</th>
                                    </tr>
                                    <tr>
                                        <td>1.</td>
                                        <td>
                                            <asp:Label ID="lbl_sb_name1" runat="server" Text=""></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lbl_sb_age1" runat="server" Text=""></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lbl_sb_school_name1" runat="server" Text=""></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lbl_sb_class1" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>2.</td>
                                        <td>
                                            <asp:Label ID="lbl_sb_name2" runat="server" Text=""></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lbl_sb_age2" runat="server" Text=""></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lbl_sb_school_name2" runat="server" Text=""></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lbl_sb_class2" runat="server" Text=""></asp:Label></td>
                                    </tr>

                                </table>

                                <table class="print_table">
                                    <tr>
                                        <td colspan="4">
                                            <h2 class="messbox-sec-h2" style="width: 100%;">Previous School Details</h2>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="fontwt400">Name of Last School  : </td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_last_school" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td class="fontwt400">Admission_date :</td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_prev_admission_date" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td class="fontwt400">Board Type : </td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_prev_board_type" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td class="fontwt400">Board :</td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_prev_board" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="fontwt400">Last class attended : </td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_prev_passout_class" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td class="fontwt400">Percentage % :</td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_prev_precentage" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="fontwt400">Reason for Shift : </td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_prev_reason_for_shift" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td class="fontwt400">Year :</td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_prev_year" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td class="fontwt400">Last class attendance : </td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_last_class_attended" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td class="fontwt400">Status :</td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_prev_class_pass_fail_status" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>

                                <table class="print_table">
                                    <tr>
                                        <td colspan="5">
                                            <h2 class="messbox-sec-h2" style="width: 100%;">Father’s Information</h2>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="fontwt400">Father's Name  : </td>
                                        <td class="fontwt700" colspan="3">
                                            <asp:Label ID="lbl_fathername" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td rowspan="6" style="width: 130px; padding: 1px 1px;">
                                            <asp:Image ID="img_father" runat="server" class="fthers-img" />
                                            <div class="fthers-signDV">
                                                <asp:Image ID="img_father_sig" runat="server" class="fthers-sign" />
                                            </div>
                                        </td>
                                    </tr>


                                    <tr>
                                        <td class="fontwt400">Occupation :</td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_occupation" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td class="fontwt400">Qualification  : </td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_qulification" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td class="fontwt400">Nationality :</td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_Nationality" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td class="fontwt400">Marital Status  : </td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_martialstatus" runat="server" Text=""></asp:Label>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td class="fontwt400">Mobile No. :</td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_mobile_no" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td class="fontwt400">WhatsApp No. : </td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_father_whatsapp" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="fontwt400">Age :</td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_father_age" runat="server" Text=""></asp:Label>
                                        </td>

                                        <td class="fontwt400">Annual Income : </td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_parent_income" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="fontwt400">Mail id : </td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_emailid" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td class="fontwt400">Aadhar No. : </td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_father_aadhar_no" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>




                                <%-- ======================= --%>
                                <table class="print_table">
                                    <tr>
                                        <td colspan="5">
                                            <h2 class="messbox-sec-h2" style="width: 100%;">Mother's Information</h2>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="fontwt400">Mother's Name  : </td>
                                        <td class="fontwt700" colspan="3">
                                            <asp:Label ID="lbl_mother_name" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td rowspan="6" style="width: 130px; padding: 1px 1px;">
                                            <asp:Image ID="img_mother" runat="server" class="mothrrs-img" />
                                            <div class="mothrrs-signDV">
                                                <asp:Image ID="img_mother_sign" runat="server" class="mothrrs-sign" />
                                            </div>
                                        </td>
                                    </tr>


                                    <tr>
                                        <td class="fontwt400">Occupation :</td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_occupation_mother" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td class="fontwt400">Qualification  : </td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_motherqulification" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td class="fontwt400">Nationality :</td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_mother_nationalty" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td class="fontwt400">Marital Status  : </td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_marital_status" runat="server" Text=""></asp:Label>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td class="fontwt400">Mobile No. :</td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_mobile_mother" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td class="fontwt400">WhatsApp No. :</td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_mom_whatsapp" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="fontwt400">Mail Id : </td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_emailid_mother" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td class="fontwt400">Aadhar No. : </td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_mom_aadhar_no" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="fontwt400">Age : </td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_mother_age" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td class="fontwt400">Annual Income : </td>
                                        <td class="fontwt700" colspan="2">
                                            <asp:Label ID="lbl_mother_annual_incom" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>

                                <table class="print_table">
                                    <tr>
                                        <td colspan="4">
                                            <h2 class="messbox-sec-h2" style="width: 100%;">Guardian's Information</h2>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="fontwt400">Guardian's Name :</td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_guardianname" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td class="fontwt400">Relation with student :</td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_relation_with_student" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td class="fontwt400">Occupation :</td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_guardian_occupation" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td class="fontwt400">Qualification  : </td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_guardian_qualification" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td class="fontwt400">Mobile No. :</td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_guardian_mobile_no" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td class="fontwt400">Aadhar No. : </td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_guardian_aadhar_no" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="fontwt400">Annual Income :</td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_guardian_annual_income" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td class="fontwt400">Address :</td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_guardian_address" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>

                                <%-- ======================= --%>
                                <table class="print_table">
                                    <tr>
                                        <td colspan="4">
                                            <h2 class="messbox-sec-h2" style="width: 100%;">Current Address</h2>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="fontwt400">Address  : </td>
                                        <td class="fontwt700" colspan="3">
                                            <asp:Label ID="lbl_current" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="fontwt400">P. O.  : </td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_po_current" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td class="fontwt400">P. S. :</td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_ps_current" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td class="fontwt400">District :</td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_distict_current" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td class="fontwt400">City/Village  : </td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_cityvillage_current" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>


                                    <tr>
                                        <td class="fontwt400">State : </td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_state_current" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td class="fontwt400">Pin Code : </td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_pincode" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="fontwt400">Country :</td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_country" runat="server"></asp:Label>
                                        </td>
                                        <td class="fontwt400">Mobile No. :</td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_mobile_no_current" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>

                                <%-- ======================= --%>
                                <table class="print_table">
                                    <tr>
                                        <td colspan="4">
                                            <h2 class="messbox-sec-h2" style="width: 100%;">Permanent Address</h2>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="fontwt400">Address  : </td>
                                        <td class="fontwt700" colspan="3">
                                            <asp:Label ID="lbl_permanent_address" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="fontwt400">P. O.  : </td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_po_permanent" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td class="fontwt400">P. S. :</td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_ps_permanent" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td class="fontwt400">District :</td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_distict_permanent" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td class="fontwt400">City/Village  : </td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_cityvillage_permanent" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>


                                    <tr>
                                        <td class="fontwt400">State : </td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_state_permanent" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td class="fontwt400">Pin Code : </td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_pincode_permanent" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="fontwt400">Country :</td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_countrt_permanent" runat="server"></asp:Label>
                                        </td>
                                        <td class="fontwt400">Mobile No. :</td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_mobile_no_permanent" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>


                                <%-- ======================= --%>
                                <table class="print_table">
                                    <tr>
                                        <td colspan="4">
                                            <h2 class="messbox-sec-h2" style="width: 100%;">Bank Details</h2>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="fontwt400">Account Holder Name  : </td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_account_holder_name" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td class="fontwt400">Bank Name :</td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_bankname" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="fontwt400">Account No. :</td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_account_no" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td class="fontwt400">IFSC Code  : </td>
                                        <td class="fontwt700">
                                            <asp:Label ID="lbl_IFSCCode" runat="server" Text=""></asp:Label>
                                        </td>

                                    </tr>

                                    <tr>
                                        <td class="fontwt400">Branch Name : </td>
                                        <td class="fontwt700" colspan="3">
                                            <asp:Label ID="lbl_branch_name" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>

                            <div class="images-wprs" runat="server" id="decuments">
                                <h2 class="messbox-sec-h2" style="width: 100%;">Document Details</h2>
                                <asp:Repeater ID="rd_view" runat="server" OnItemDataBound="rd_view_ItemDataBound">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_img_dv" runat="server">
                                            <asp:Label ID="lbl_file_type" Visible="false" runat="server" Text='<%#Bind("Image_type")%>'></asp:Label>
                                            <div class="images-dv-wprs">
                                                <div class="images-wprsimges">
                                                    <img src="<%#Eval("Image_path")%>" />
                                                </div>
                                                <div class="images-dv-wprs-name-pdv">
                                                    <p class="images-dv-wprs-name-p"><%#Eval("Image_name")%></p>
                                                </div>
                                            </div>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>

                            <div class="images-wprs" runat="server" id="doc_submited" visible="false">
                                <h2 class="messbox-sec-h2" style="width: 100%;">Document Submitted</h2>
                                <table class="print_table">
                                    <asp:Repeater ID="rp_document_submit" runat="server" OnItemDataBound="rp_document_submit_ItemDataBound">
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbl_doc_id" Visible="false" runat="server" Text='<%#Bind("Doc_id")%>'></asp:Label>
                                                    <asp:Label ID="lbl_doc_name" runat="server" Text='<%#Bind("Document_name")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_is_submit" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </div>


                            <div style="width: 100%; float: left; margin: 70px 0px 0px 0px;">
                                <p style="width: 33%; float: left; margin: 0px; font-size: 14px;">
                                    Checked By
                                </p>
                                <p style="width: 33%; float: left; margin: 0px; font-size: 14px; text-align: center;">
                                    Parent Signature
                                </p>
                                <p style="width: 33%; float: left; margin: 0px; font-size: 14px; text-align: right;">
                                    Principal Signature
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>


    </form>
</body>
</html>
