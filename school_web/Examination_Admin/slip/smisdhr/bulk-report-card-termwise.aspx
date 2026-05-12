<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bulk-report-card-termwise.aspx.cs" Inherits="school_web.Examination_Admin.slip.smisdhr.bulk_report_card_termwise" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Report Card</title>
    <link href="css/report-card.css" rel="stylesheet" />
    <script src="../../../assets/js/jquery-1.10.2.min.js"></script>
    <script src="../../../assets/Angular/angular.min.js"></script>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="css/report-card.css" rel="stylesheet" type="text/css" />');
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
        <div class="invoice-sec" data-ng-app="RpCardApp" data-ng-controller="RpCardAppCtrl">
            <div class="prnt-btn-sec" runat="server" id="printBtns">
                <div class="prnt-btn-wpr">
                    <div class="print-btn-sec">
                        <div class="noPrint" style="float: left">
                            <asp:Button ID="btn_back" CssClass="back-btn" runat="server" OnClick="btn_back_Click1" />
                        </div>
                        <div class="noPrint" style="float: right">
                            <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" CssClass="print-btn" runat="server" ToolTip="Print"></asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>

            <div class="ints-loader-wpr" id="intsLoader">
                <div class="ints-loader-wpr-inr">
                    <div class="ints-loader">
                        <p class="ints-loader-txt">
                            <img src="../assets/images/loader-ico.gif" class="ints-loader-img" />
                            <asp:Label ID="lblmessage" runat="server"></asp:Label>
                        </p>
                    </div>
                </div>
            </div>


            <div id="tblPrintIQ" runat="server">
                <div class="invoice-inr-sec" data-ng-repeat="x in reportCardS track by $index">
                    <div class="invoice-height-manage">
                        <div class="invoice-wpr">
                            <img src="{{x.MyFirmDetailData[0].Watermar_image}}" class="wtr-mrk-img v-false {{x.MySubjectMarkShowItemII[0].Is_watermark_show}}" />
                            <div class="report-card-wpr" style="height: {{x.MySubjectMarkShowItemII[0].Height_dv}}">
                                <div class="report-card-head {{x.MySubjectMarkShowItemII[0].Hdr_frst}}">
                                    <div class="report-card-left-dv">
                                        <img src="{{x.MyFirmDetailData[0].Frim_logo}}" />
                                        <asp:Label ID="lbl_estd" runat="server" Text="{{x.MyFirmDetailData[0].Estd}}" class="estdTextP v-false {{x.MySubjectMarkShowItemII[0].Is_estd_show}}"></asp:Label>
                                    </div>
                                    <div class="report-card-cntr-dv">
                                        <asp:Label ID="lbl_school_name" class="report-card-schlname" runat="server" Text="{{x.MyFirmDetailData[0].Firm_name}}"></asp:Label>
                                        <asp:Label ID="lbl_aff_text" class="report-card-schl-affno-by" runat="server" Text="{{x.MyFirmDetailData[0].Affiliated_by_full_text}}"></asp:Label>
                                        <asp:Label ID="lbl_address" runat="server" class="report-card-schl-add" Text="{{x.MyFirmDetailData[0].Firm_address}}"></asp:Label>
                                        <asp:Label ID="lbl_contact_no" runat="server" class="report-card-schl-cont v-false {{x.MySubjectMarkShowItemII[0].Is_contact_no_show}}" Text="{{x.MyFirmDetailData[0].Firm_contact_no}}"></asp:Label>
                                        <p class="report-card-schl-emil v-false {{x.MySubjectMarkShowItemII[0].Is_email_show}}">
                                            Email : 
                                        <asp:Label ID="lbl_email" runat="server" Text="{{x.MyFirmDetailData[0].Firm_email}}"></asp:Label>
                                        </p>

                                        <p class="report-card-schl-emil v-false {{x.MySubjectMarkShowItemII[0].Is_website_show}}">
                                            Website : 
                                        <asp:Label ID="lbl_website" runat="server" Text="{{x.MyFirmDetailData[0].Website}}"></asp:Label>
                                        </p>
                                        <h2 class="report-card-ac-sson">ACADEMIC SESSION:
                                        <asp:Label ID="lbl_sessions" runat="server" Text="{{x.Session}}"></asp:Label></h2>
                                        <h2 class="report-card-rprt-crd">{{x.Term_name}} REPORT CARD <span class="{{x.MySubjectMarkShowItemII[0].Class_in_new_line}}"><span class="v-false {{x.MySubjectMarkShowItemII[0].Is_class_text_show}}">CLASS</span> {{x.For_class}}</span></h2>
                                    </div>
                                    <asp:Label ID="lbl_aff_no" class="report-card-schl-affno" runat="server" Text="{{x.MySubjectMarkShowItemII[0].Aff_text}} : {{x.MyFirmDetailData[0].Frim_aff_no}}"></asp:Label>

                                    <div class="report-card-rght-dv">
                                        <div class="sdt-img-dv {{x.Student_image}}">
                                            <img src="{{x.Student_image}}" class="{{x.MySubjectMarkShowItemII[0].Is_std_img_hide}}" />
                                        </div>
                                    </div>
                                </div>


                                <div class="report-card-std-info-dv">
                                    <div class="report-card-std-info-dv-50">
                                        <p class="stds-info-p">
                                            <i class="stds-info-p-i">Student's Name</i>  <i class="stds-info-p-doti">:</i>
                                            <asp:Label ID="lbl_student_name" runat="server" Text="{{x.Student_name}}"></asp:Label>
                                        </p>
                                        <p class="stds-info-p">
                                            <i class="stds-info-p-i">DATE OF BIRTH</i>  <i class="stds-info-p-doti">:</i>
                                            <asp:Label ID="lbl_date_of_birth" runat="server" Text="{{x.Date_of_birth}}"></asp:Label>
                                        </p>
                                        <p class="stds-info-p">
                                            <i class="stds-info-p-i">MOTHER'S NAME</i>  <i class="stds-info-p-doti">:</i>
                                            <asp:Label ID="lbl_mother_name" runat="server" Text="{{x.Mother_name}}"></asp:Label>
                                        </p>
                                        <p class="stds-info-p {{x.MySubjectMarkShowItemII[0].Father_name1}}">
                                            <i class="stds-info-p-i">FATHER'S NAME</i>  <i class="stds-info-p-doti">:</i>
                                            <asp:Label ID="lbl_father_name" runat="server" Text="{{x.Father_name}}"></asp:Label>
                                        </p>
                                        <p class="stds-info-p">
                                            <i class="stds-info-p-i">HEIGHT</i>  <i class="stds-info-p-doti">:</i>
                                            <asp:Label ID="Label6" runat="server" Text="{{x.Height}}"></asp:Label>
                                        </p>
                                    </div>

                                    <div class="report-card-std-info-dv-50">
                                        <p class="stds-info-p {{x.MySubjectMarkShowItemII[0].Father_name2}}">
                                            <i class="stds-info-p-i">FATHER'S NAME</i>  <i class="stds-info-p-doti">:</i>
                                            <asp:Label ID="Label2" runat="server" Text="{{x.Father_name}}"></asp:Label>
                                        </p>
                                        <p class="stds-info-p {{x.MySubjectMarkShowItemII[0].Ranksdv}}">
                                            <i class="stds-info-p-i">RANK</i>  <i class="stds-info-p-doti">:</i>
                                            <asp:Label ID="Label1" runat="server" Text="{{x.MyRank}}"></asp:Label>
                                        </p>
                                        <p class="stds-info-p">
                                            <i class="stds-info-p-i">ADMISSION NUMBER</i>  <i class="stds-info-p-doti">:</i>
                                            <asp:Label ID="lbl_admission_no" runat="server" Text="{{x.Admission_no}}"></asp:Label>
                                        </p>
                                        <p class="stds-info-p {{x.MySubjectMarkShowItemII[0].Is_std_section_hide}}">
                                            <i class="stds-info-p-i">SECTION</i>  <i class="stds-info-p-doti">:</i>
                                            <asp:Label ID="lbl_section" runat="server" Text="{{x.Section}}"></asp:Label>
                                        </p>
                                        <p class="stds-info-p">
                                            <i class="stds-info-p-i">ROLL NO.</i>  <i class="stds-info-p-doti">:</i>
                                            <asp:Label ID="lbl_roll_no" runat="server" Text="{{x.Roll_no}}"></asp:Label>
                                        </p>
                                        <p class="stds-info-p">
                                            <i class="stds-info-p-i">AGE</i>  <i class="stds-info-p-doti">:</i>
                                            <asp:Label ID="Label5" runat="server" Text="{{x.Age}}"></asp:Label>
                                        </p>
                                        <p class="stds-info-p">
                                            <i class="stds-info-p-i">WEIGHT</i>  <i class="stds-info-p-doti">:</i>
                                            <asp:Label ID="Label7" runat="server" Text="{{x.Weight}}"></asp:Label>
                                        </p>
                                    </div>
                                </div>


                                <div class="subs-mrks-area-dv">
                                    <table>
                                        <tr>
                                            <th class="{{x.MySubjectMarkShowItemII[0].Is_text_center}} bgcolors" colspan="8">GRADES</th>
                                        </tr>
                                        <tr>
                                            <%--<th class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}">SN</th>
                                            <th class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}">SUBJECT CODE</th>--%>
                                            <th>SUBJECTS</th>
                                            <th class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}" data-ng-repeat="item in x.MySubjectHeading track by $index">{{item.Short_Name}} <span class="{{x.MySubjectMarkShowItemII[0].Max_mark_show}}">({{item.Maximum_Marks}})</span></th>
                                            <%--<th class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}">{{x.MySubjectMarkShowItemII[0].grade_head_text}} ({{x.MySubjectHeading[0].Term_Maximum_Marks}})</th>
                                            <th class="{{x.MySubjectMarkShowItemII[0].Is_text_center}} {{x.MySubjectMarkShowItemII[0].Grade}}">GRADE</th>--%>
                                        </tr>

                                        <tr data-ng-repeat="item in x.MySubjectMarkShowItemII track by $index">
                                            <%-- <td class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}">{{$index+1}}</td>
                                            <td class="{{x.MySubjectMarkShowItemII[0].Is_text_center}} {{x.MySubjectMarkShowItemII[0].Is_subj_code_hide}}">{{item.Subject}}</td>--%>
                                            <td>{{item.Subject_name}}</td>
                                            <td class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}" data-ng-repeat="itemx in item.MySubjectMarkItemIII track by $index">{{itemx.Marks}}</td>
                                            <%--<td class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}">{{item.Total_marks}}</td>
                                            <td class="{{x.MySubjectMarkShowItemII[0].Is_text_center}} {{item.Grade}}">{{item.Grade}}</td>--%>
                                        </tr>
                                    </table>
                                </div>


                                <div class="subs-mrks-area-lft-dv" style="width: 100%">
                                    <div class="subs-mrks-area-b-dv" style="margin-top: {{x.MySubjectMarkShowItemII[0].Co_sch_area_margn}}">
                                        <table>
                                            <%--<tr>
                                                <th>B</th>
                                                <th colspan="2">CO-SCHOLASTIC AREAS</th>
                                            </tr>
                                            <tr>
                                                <th class="th-bg-rmov">SN</th>
                                                <th class="th-bg-rmov">ACTIVITIES</th>
                                                <th class="th-bg-rmov">GRADE</th>
                                            </tr>
                                            <tr data-ng-repeat="item in x.MyCoScholasticData track by $index">
                                                <td>{{$index+1}}</td>
                                                <td>{{item.Subject_Name}}</td>
                                                <td>{{item.Total_marks}}</td>
                                            </tr> 
                                            <tr>
                                                <th>C</th>
                                                <th colspan="2">DISCIPLINE [ 5 - POINT GRADING SCALE]</th>
                                            </tr>
                                            <tr>
                                                <th class="th-bg-rmov">SN</th>
                                                <th class="th-bg-rmov">ACTIVITIES</th>
                                                <th class="th-bg-rmov">GRADE</th>
                                            </tr>--%>
                                            <tr data-ng-repeat="item in x.MyDescplineData track by $index">
                                                <td style="width: 50%;"><span class="actbld">{{item.Activity_name}}</span>
                                                    <br />
                                                    <span class="actnrml">({{item.Activity_details}})</span>
                                                </td>
                                                <td>{{item.Term_grade}}</td>
                                            </tr>
                                        </table>
                                    </div>

                                    <%--<div class="prcntage-remrk-dv {{x.MySubjectMarkShowItemII[0].Prcnt_remark}}" style="margin-top: {{x.MySubjectMarkShowItemII[0].Percent_remark_area_margn}}">
                                        <p>Remark :</p>
                                        <span>{{x.MySubjectMarkShowItemII[0].P_remark}}</span>
                                    </div>--%>
                                </div>



                                <div class="instruction-dv">
                                    <div class="instruction-50 instruction-50-pr">
                                        <div class="instruction-tbls" style="margin-top: {{x.MySubjectMarkShowItemII[0].Ins1_area_margn}}">
                                            <table>
                                                <tr>
                                                    <th colspan="2" class="txt-center">GRADATION</th>
                                                </tr>
                                                <%--<tr>
                                                    <td colspan="2" class="txt-center">(Scholastic & Co-Scholastic Areas {{x.MyMarkRangeData[0].RowCount}}-point grading scale)</td>
                                                </tr>--%>
                                                <tr>
                                                    <td>MARKS RANGE</td>
                                                    <td>GRADE</td>
                                                </tr>
                                                <tr data-ng-repeat="item in x.MyMarkRangeData track by $index">
                                                    <td>{{item.Lower_Range}}-{{item.Upper_Range}}</td>
                                                    <td>{{item.Grade}}</td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="instruction-50  instruction-50-pl">
                                        <div class="instruction-tbls floatrght">
                                            <div style="margin-top: {{x.MySubjectMarkShowItemII[0].Ins2_area_margn}}">
                                                <table>
                                                    <tr>
                                                        <th colspan="2" class="txt-center">FINAL TERM</th>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 150px; height: 88px; vertical-align: top;">Teacher's Remark</td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 150px; height: 28px; vertical-align: top;">No. of Working Days</td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 150px; height: 28px; vertical-align: top;">No. of Days Present</td>
                                                        <td></td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="sig-dv v-false {{x.MySubjectMarkShowItemII[0].Sign_bottom}}">
                                    <div class="sig-left">
                                        <div class="lft-sig-img-dv">
                                            <img class="lft-sig-img {{x.MySignatureDetailData[0].Signature_image}}" src="{{x.MySignatureDetailData[0].Signature_image}}" />
                                        </div>
                                        <p class="sig-ps">{{x.MySignatureDetailData[0].Signature_name}}</p>
                                    </div>
                                    <div class="sig-left">
                                        <div class="cntr-sig-img-dv">
                                            <img src="{{x.MySignatureDetailData[1].Signature_image}}" class="cntr-sig-img {{x.MySignatureDetailData[1].Signature_image}}" />
                                        </div>
                                        <p class="sig-ps">{{x.MySignatureDetailData[1].Signature_name}}</p>
                                    </div>

                                    <div class="sig-left">
                                        <div class="rght-sig-img-dv">
                                            <img src="{{x.MySignatureDetailData[2].Signature_image}}" class="rght-sig-img {{x.MySignatureDetailData[2].Signature_image}}" />
                                        </div>
                                        <p class="sig-ps">{{x.MySignatureDetailData[2].Signature_name}}</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <asp:HiddenField ID="hd_admission_no" runat="server" />
        <asp:HiddenField ID="hd_session_id" runat="server" />
        <asp:HiddenField ID="hd_class_id" runat="server" />
        <asp:HiddenField ID="hd_branch_id" runat="server" />
        <asp:HiddenField ID="hd_term_id" runat="server" />
        <asp:HiddenField ID="hd_section" runat="server" />

        <script type="text/javascript">
            var app = angular.module('RpCardApp', []);
            app.controller('RpCardAppCtrl', function ($scope, $http) {

                var session_id = $("#<%=hd_session_id.ClientID%>").val();
                var class_id = $("#<%=hd_class_id.ClientID%>").val();
                var admission_no = $("#<%=hd_admission_no.ClientID%>").val();
                var branch_id = $("#<%=hd_branch_id.ClientID%>").val();
                var term_id = $("#<%=hd_term_id.ClientID%>").val();
                var section = $("#<%=hd_section.ClientID%>").val();


                messge("Please Wait...");
                $("#intsLoader").removeClass("hidden");

                messge("Please Wait...");
                $("#intsLoader").removeClass("hidden");
                $http.get("api/bulk-report-card.asmx/fetch_rp_card_bulks", { params: { "Session_id": session_id, "Class_id": class_id, "Section": section, "Branch_id": branch_id, "Term_id": term_id, "Admission_no": admission_no } }).then(function (response) {
                    $scope.reportCardS = response.data;
                    $("#intsLoader").addClass("hidden");
                })

            });


            function messge(msg) {
                $("#<%=lblmessage.ClientID%>").text(msg);
                $('.ints-loader-wpr').hide().slideDown(0);
            }
        </script>
    </form>
</body>
</html>
