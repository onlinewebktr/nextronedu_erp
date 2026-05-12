<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="print-student-info.aspx.cs" Inherits="school_web.Admin.slip.print_student_info" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print</title>
    <link href="css/stdInfos.css" rel="stylesheet" />
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="css/stdInfos.css" rel="stylesheet" type="text/css" />');
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
                        <asp:Image ID="img_watermark" Visible="false" runat="server" class="wtr-mrk-img" />
                        <div class="report-card-wpr">
                            <div id="textheader" runat="server" style="width: 100%">
                                <div class="report-card-head">
                                    <div class="report-card-left-dv">
                                        <asp:Image ID="Image1" runat="server" />
                                        <asp:Label ID="lbl_estd" runat="server" Style="display: none" class="estdTextP"></asp:Label>
                                    </div>
                                    <div class="report-card-cntr-dv">
                                        <asp:Label ID="lbl_school_name" class="report-card-schlname" runat="server"></asp:Label>
                                        <asp:Label ID="lbl_aff_text" class="report-card-schl-affno-by" runat="server"></asp:Label>
                                        <asp:Label ID="lbl_address" runat="server" class="report-card-schl-add"></asp:Label>
                                        <asp:Label ID="lbl_contact_no" runat="server" Text="" class="report-card-schl-cont"></asp:Label>
                                        <p class="report-card-schl-emil">
                                            Email : 
                                        <asp:Label ID="lbl_email" runat="server"></asp:Label>
                                        </p>

                                        <p class="report-card-schl-emil">
                                            Website : 
                                        <asp:Label ID="lbl_website" runat="server"></asp:Label>
                                        </p>
                                        <h2 class="report-card-ac-sson">Student Details</h2>
                                    </div>
                                </div>
                            </div>

                            <div id="printheader" runat="server" style="width: 100%; text-align: center">
                                <asp:Image ID="img_header" runat="server" />
                            </div>


                            <div class="form-wprs-dv" style="border: 0px solid #000000;">
                                <div class="stdimages">
                                    <div class="imgslfts">
                                        <h3>Father</h3>
                                        <div class="report-card-rght-dv">
                                            <asp:Image ID="Image2" runat="server" Visible="false" />
                                            <p>Affix passport size photo of the student</p>
                                        </div>
                                    </div>
                                    <div class="imgscntr">
                                        <h3>Mother</h3>
                                        <div class="report-card-rght-dv">
                                            <asp:Image ID="Image3" runat="server" Visible="false" />
                                            <p>Affix passport size photo of the student</p>
                                        </div>
                                    </div>
                                    <div class="imgsrght">
                                        <h3>Child</h3>
                                        <div class="report-card-rght-dv">
                                            <asp:Image ID="Image4" runat="server" Visible="false" />
                                            <p>Affix passport size photo of the student</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-wprs-dv">
                                <h2 class="form-wprs-dv-title-h">Academic Information</h2>
                                <div class="form-wprs-contnt-dv">
                                    <div style="float: left; width: 100%;" id="Div1" runat="server">
                                        <div class="form-wprs-contnt-row">
                                            <div class="wdth50">
                                                <p class="form-wprs-contnt-p">
                                                    Student Type :
                                                <asp:Label ID="lbl_std_type" runat="server" Style="width: 69%;"></asp:Label>
                                                </p>
                                            </div>
                                            <div class="wdth50">
                                                <p class="form-wprs-contnt-p">
                                                    Date of Admission :
                                                <asp:Label ID="lbl_date_of_admission" runat="server" Style="width: 64%; right: 0px;"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                        <div class="form-wprs-contnt-row">
                                            <div class="wdth33">
                                                <p class="form-wprs-contnt-p">
                                                    Session :
                                                <asp:Label ID="lbl_session" runat="server" Style="width: 67%;"></asp:Label>
                                                </p>
                                            </div>
                                            <div class="wdth33">
                                                <p class="form-wprs-contnt-p">
                                                    House :
                                                <asp:Label ID="lbl_house" runat="server" Style="width: 67%;"></asp:Label>
                                                </p>
                                            </div>
                                            <div class="wdth33">
                                                <p class="form-wprs-contnt-p">
                                                    Admission No. :
                                                <asp:Label ID="lbl_adm_no" runat="server" Style="width: 58%; right: 0px;"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                        <div class="form-wprs-contnt-row">
                                            <div class="wdth33">
                                                <p class="form-wprs-contnt-p">
                                                    Class :
                                                <asp:Label ID="lbl_class" runat="server" Style="width: 67%;"></asp:Label>
                                                </p>
                                            </div>
                                            <div class="wdth33">
                                                <p class="form-wprs-contnt-p">
                                                    Section :
                                                <asp:Label ID="lbl_section" runat="server" Style="width: 67%;"></asp:Label>
                                                </p>
                                            </div>
                                            <div class="wdth33">
                                                <p class="form-wprs-contnt-p">
                                                    Roll No. :
                                                <asp:Label ID="lbl_roll_no" runat="server" Style="width: 58%; right: 0px;"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-wprs-dv">
                                <h2 class="form-wprs-dv-title-h">Information on Child</h2>
                                <div class="form-wprs-contnt-dv">
                                    <div style="float: left; width: 100%;" id="personaldetails" runat="server">
                                        <div class="form-wprs-contnt-row">
                                            <table>
                                                <tr>
                                                    <td>Name</td>
                                                    <td colspan="3">
                                                        <asp:Label ID="lbl_first_name" runat="server"></asp:Label>
                                                    </td>
                                                    <%--<td>
                                                        <asp:Label ID="lbl_middle_name" runat="server"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lbl_std_last_name" runat="server"></asp:Label>
                                                    </td>--%>
                                                </tr>
                                                <tr>
                                                    <td>Date of Birth</td>
                                                    <td>
                                                        <asp:Label ID="lbl_dob" runat="server"></asp:Label></td>
                                                    <td>Gender</td>
                                                    <td>
                                                        <asp:Label ID="lbl_gender" runat="server"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>Category</td>
                                                    <td>
                                                        <asp:Label ID="lbl_category" runat="server"></asp:Label></td>
                                                    <td>Religion</td>
                                                    <td>
                                                        <asp:Label ID="lbl_religion" runat="server"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>Blood Group</td>
                                                    <td>
                                                        <asp:Label ID="lbl_blood_group" runat="server"></asp:Label></td>
                                                    <td>Nationality</td>
                                                    <td>
                                                        <asp:Label ID="lbl_nationality" runat="server"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>Mother Tongue</td>
                                                    <td>
                                                        <asp:Label ID="lbl_mother_toung" runat="server"></asp:Label></td>
                                                    <td>Second Language the child will study</td>
                                                    <td>
                                                        <asp:Label ID="lbl_second_language" runat="server"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>Place of Birth</td>
                                                    <td colspan="3">
                                                        <asp:Label ID="lbl_place_of_birth" runat="server"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">Are there any medical conditions of your ward which the school should be aware of</td>
                                                    <td colspan="2">
                                                        <asp:Label ID="lbl_are_there_medical_problem" runat="server"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">Hobbies/Field of Interest</td>
                                                    <td colspan="2">
                                                        <asp:Label ID="lbl_hobbies" runat="server"></asp:Label></td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="wdth50" style="padding: 0px 3px 0px 0px;">
                                <div class="form-wprs-dv">
                                    <h2 class="form-wprs-dv-title-h">Residential Address</h2>
                                    <div class="form-wprs-contnt-dv">
                                        <div style="float: left; width: 100%;" id="Div2" runat="server">
                                            <div class="form-wprs-contnt-row">
                                                <table>
                                                    <tr>
                                                        <td>Address</td>
                                                        <td>
                                                            <asp:Label ID="lbl_address1" runat="server"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>P.O.</td>
                                                        <td>
                                                            <asp:Label ID="lbl_po1" runat="server"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>P.S.</td>
                                                        <td>
                                                            <asp:Label ID="lbl_ps1" runat="server"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>District</td>
                                                        <td>
                                                            <asp:Label ID="lbl_dist1" runat="server"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>State</td>
                                                        <td>
                                                            <asp:Label ID="lbl_state1" runat="server"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Pin Code</td>
                                                        <td>
                                                            <asp:Label ID="lbl_pincode1" runat="server"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Tel</td>
                                                        <td>
                                                            <asp:Label ID="lbl_tel1" runat="server"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>E-mail</td>
                                                        <td>
                                                            <asp:Label ID="lbl_email1" runat="server"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Emergency Contact No.</td>
                                                        <td>
                                                            <asp:Label ID="lbl_emerg_contact1" runat="server"></asp:Label></td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="wdth50" style="padding: 0px 0px 0px 3px;">
                                <div class="form-wprs-dv">
                                    <h2 class="form-wprs-dv-title-h">Correspondence Address</h2>
                                    <div class="form-wprs-contnt-dv">
                                        <div style="float: left; width: 100%;" id="Div3" runat="server">
                                            <div class="form-wprs-contnt-row">
                                                <table>
                                                    <tr>
                                                        <td>Address</td>
                                                        <td>
                                                            <asp:Label ID="lbl_address2" runat="server"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>P.O.</td>
                                                        <td>
                                                            <asp:Label ID="lbl_po2" runat="server"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>P.S.</td>
                                                        <td>
                                                            <asp:Label ID="lbl_ps2" runat="server"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>District</td>
                                                        <td>
                                                            <asp:Label ID="lbl_dist2" runat="server"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>State</td>
                                                        <td>
                                                            <asp:Label ID="lbl_state2" runat="server"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Pin Code</td>
                                                        <td>
                                                            <asp:Label ID="lbl_pincode2" runat="server"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Tel</td>
                                                        <td>
                                                            <asp:Label ID="lbl_tel2" runat="server"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>E-mail</td>
                                                        <td>
                                                            <asp:Label ID="lbl_email2" runat="server"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Emergency Contact No.</td>
                                                        <td>
                                                            <asp:Label ID="lbl_emerg_contact2" runat="server"></asp:Label></td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-wprs-dv">
                                <h2 class="form-wprs-dv-title-h">Academic background of child</h2>
                                <div class="form-wprs-contnt-dv">
                                    <div style="float: left; width: 100%;" id="Div4" runat="server">
                                        <div class="form-wprs-contnt-row">
                                            <table>
                                                <tr>
                                                    <td rowspan="2"><b>Previous School</b>
                                                        <br />
                                                        <asp:Label ID="lbl_prev_school" runat="server"></asp:Label>
                                                    </td>
                                                    <td><b>Final Mark of Previous Year</b></td>
                                                    <td><b>Full Mark</b></td>
                                                    <td><b>Mark Obtained</b></td>
                                                </tr>
                                                <tr>
                                                    <td>English</td>
                                                    <td>
                                                        <asp:Label ID="lbl_prev_eng_FM" runat="server"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lbl_prev_eng_OM" runat="server"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td rowspan="2"><b>Last Class Attended</b>
                                                        <br />
                                                        <asp:Label ID="lbl_last_class_attended" runat="server"></asp:Label>
                                                    </td>
                                                    <td>Hindi</td>
                                                    <td>
                                                        <asp:Label ID="lbl_prev_hin_FM" runat="server"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lbl_prev_hin_OM" runat="server"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>Math</td>
                                                    <td>
                                                        <asp:Label ID="lbl_prev_math_FM" runat="server"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lbl_prev_math_OM" runat="server"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td rowspan="2"><b>Any Outstanding Achievement</b>
                                                        <br />
                                                        <asp:Label ID="lbl_any_achievement" runat="server"></asp:Label>
                                                    </td>
                                                    <td>Social Science(Hist, Geog)</td>
                                                    <td>
                                                        <asp:Label ID="lbl_prev_sc_FM" runat="server"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lbl_prev_sc_OM" runat="server"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>Science</td>
                                                    <td>
                                                        <asp:Label ID="lbl_prev_sci_FM" runat="server"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lbl_prev_sci_OM" runat="server"></asp:Label></td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div class="form-wprs-dv">
                                <h2 class="form-wprs-dv-title-h">Father/Guardian</h2>
                                <div class="form-wprs-contnt-dv">
                                    <div style="float: left; width: 100%;" id="Div5" runat="server">
                                        <div class="form-wprs-contnt-row">
                                            <table>
                                                <tr>
                                                    <td>Name</td>
                                                    <td>
                                                        <asp:Label ID="lbl_f_name" runat="server"></asp:Label></td>
                                                    <td>Age</td>
                                                    <td>
                                                        <asp:Label ID="lbl_f_age" runat="server"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>Nationality</td>
                                                    <td>
                                                        <asp:Label ID="lbl_f_nationality" runat="server"></asp:Label></td>
                                                    <td>Educational Qualification</td>
                                                    <td>
                                                        <asp:Label ID="lbl_f_edu_qualification" runat="server"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>Institution</td>
                                                    <td>
                                                        <asp:Label ID="lbl_f_institution" runat="server"></asp:Label></td>
                                                    <td>Organisation</td>
                                                    <td>
                                                        <asp:Label ID="lbl_f_organization" runat="server"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>Working For</td>
                                                    <td>
                                                        <asp:Label ID="lbl_f_work_for" runat="server"></asp:Label></td>
                                                    <td>Office Address</td>
                                                    <td>
                                                        <asp:Label ID="lbl_f_office_add" runat="server"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>Designation</td>
                                                    <td>
                                                        <asp:Label ID="lbl_f_designation" runat="server"></asp:Label></td>
                                                    <td>Annual Income</td>
                                                    <td>
                                                        <asp:Label ID="lbl_f_annual_income" runat="server"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>Tel</td>
                                                    <td>
                                                        <asp:Label ID="lbl_f_Tel" runat="server"></asp:Label></td>
                                                    <td>No. of Hours of Interaction with the child per week</td>
                                                    <td>
                                                        <asp:Label ID="lbl_f_hour_of_interection" runat="server"></asp:Label></td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-wprs-dv">
                                <h2 class="form-wprs-dv-title-h">Mother/Guardian</h2>
                                <div class="form-wprs-contnt-dv">
                                    <div style="float: left; width: 100%;" id="Div6" runat="server">
                                        <div class="form-wprs-contnt-row">
                                            <table>
                                                <tr>
                                                    <td>Name</td>
                                                    <td>
                                                        <asp:Label ID="lbl_m_name" runat="server"></asp:Label></td>
                                                    <td>Age</td>
                                                    <td>
                                                        <asp:Label ID="lbl_m_age" runat="server"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>Nationality</td>
                                                    <td>
                                                        <asp:Label ID="lbl_m_nationality" runat="server"></asp:Label></td>
                                                    <td>Educational Qualification</td>
                                                    <td>
                                                        <asp:Label ID="lbl_m_edu_qualification" runat="server"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>Institution</td>
                                                    <td>
                                                        <asp:Label ID="lbl_m_institution" runat="server"></asp:Label></td>
                                                    <td>Organisation</td>
                                                    <td>
                                                        <asp:Label ID="lbl_m_organization" runat="server"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>Working For</td>
                                                    <td>
                                                        <asp:Label ID="lbl_m_work_for" runat="server"></asp:Label></td>
                                                    <td>Office Address</td>
                                                    <td>
                                                        <asp:Label ID="lbl_m_office_add" runat="server"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>Designation</td>
                                                    <td>
                                                        <asp:Label ID="lbl_m_designation" runat="server"></asp:Label></td>
                                                    <td>Annual Income</td>
                                                    <td>
                                                        <asp:Label ID="lbl_m_annual_income" runat="server"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>Tel</td>
                                                    <td>
                                                        <asp:Label ID="lbl_m_Tel" runat="server"></asp:Label></td>
                                                    <td>No. of Hours of Interaction with the child per week</td>
                                                    <td>
                                                        <asp:Label ID="lbl_m_hour_of_interection" runat="server"></asp:Label></td>
                                                </tr>

                                                <tr>
                                                    <td colspan="2">If parents are devorced, living saperately or widowed, with whom is the child living?</td>
                                                    <td colspan="2">
                                                        <asp:Label ID="lbl_if_parents_are_devorced" runat="server"></asp:Label></td>
                                                </tr>

                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-wprs-dv">
                                <h2 class="form-wprs-dv-title-h">Questions</h2>
                                <div class="form-wprs-contnt-dv">
                                    <div style="float: left; width: 100%;" id="Div7" runat="server">
                                        <div class="form-wprs-contnt-row">
                                            <table>
                                                <tr>
                                                    <td>What are your reasons for choosing
                                                        <asp:Label ID="lbl_shool_name1" runat="server"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lbl_reason_for_choosing" runat="server"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>How did you learn about :
                                                        <asp:Label ID="lbl_school_name1" runat="server"></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lbl_how_did_you_learn" runat="server"></asp:Label></td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div class="form-wprs-dv" id="subjDV" runat="server">
                                <h2 class="form-wprs-dv-title-h">Subject Taken</h2>
                                <div class="form-wprs-contnt-dv">
                                    <div style="float: left; width: 100%;" id="Div8" runat="server">
                                        <div class="form-wprs-contnt-row">
                                            <table>
                                                <tr>
                                                    <td>SL.</td>
                                                    <td>Subject Name</td>
                                                    <td>Subject Code</td>
                                                </tr>
                                                <asp:Repeater ID="rp_subjects" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="lbl_subject_name" runat="server" Text='<%#Bind("Subject_name")%>'></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="lbl_subj_code" runat="server" Text='<%#Bind("Subject_Code")%>'></asp:Label></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
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
    </form>
</body>
</html>
