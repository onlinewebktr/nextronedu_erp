<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bulk-report-card-termI-P2.aspx.cs" Inherits="school_web.Examination_Admin.slip.greenv.bulk_report_card_termI_P2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Report Card</title>
    <link href="https://fonts.googleapis.com/css?family=Montserrat:100,200,300,400,500,600,700" rel="stylesheet" />
    <link href="../assets/css/report-card.css" rel="stylesheet" /> 
    <script src="../../../assets/js/jquery-1.10.2.min.js"></script>
    <script src="../../../assets/Angular/angular.min.js"></script>

    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="../assets/css/report-card.css" rel="stylesheet" type="text/css" />');
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
            <div class="prnt-btn-sec">
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
                                <div class="report-card-head">
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
                                        <asp:Label ID="Label2" runat="server" Text="{{x.MyFirmDetailData[0].Website}}"></asp:Label>
                                        </p>
                                        <h2 class="report-card-ac-sson">ACADEMIC SESSION:
                                        <asp:Label ID="lbl_sessions" runat="server" Text="{{x.Session}}"></asp:Label></h2>
                                        <h2 class="report-card-rprt-crd">{{x.Term_name}} REPORT CARD <span class="{{x.MySubjectMarkShowItemII[0].Class_in_new_line}}"><span class="v-false {{x.MySubjectMarkShowItemII[0].Is_class_text_show}}">CLASS</span> {{x.For_class}}</span></h2>
                                    </div><asp:Label ID="lbl_aff_no" class="report-card-schl-affno" runat="server" Text="{{x.MyFirmDetailData[0].Frim_aff_no}}"></asp:Label>
                                    <div class="report-card-rght-dv">
                                        

                                        <%--<div class="sdt-img-dv {{x.Student_image}}">
                                            <img src="{{x.Student_image}}" />
                                        </div>--%>
                                    </div>
                                </div>

                                <div class="report-card-std-info-dv">
                                    <div class="report-card-std-info-dv-50">
                                        <p class="stds-info-p">
                                            <i class="stds-info-p-i">Student's Name</i>  <i class="stds-info-p-doti">:</i>
                                            <asp:Label ID="lbl_student_name" runat="server" Text="{{x.Student_name}}"></asp:Label>
                                        </p>
                                        <p class="stds-info-p" style="display: none !important">
                                            <i class="stds-info-p-i">DATE OF BIRTH</i>  <i class="stds-info-p-doti">:</i>
                                            <asp:Label ID="lbl_date_of_birth" runat="server" Text="{{x.Date_of_birth}}"></asp:Label>
                                        </p>
                                        <p class="stds-info-p" style="display: none !important">
                                            <i class="stds-info-p-i">MOTHER'S NAME</i>  <i class="stds-info-p-doti">:</i>
                                            <asp:Label ID="lbl_mother_name" runat="server" Text="{{x.Mother_name}}"></asp:Label>
                                        </p>
                                        <p class="stds-info-p" style="display: none !important">
                                            <i class="stds-info-p-i">FATHER'S NAME</i>  <i class="stds-info-p-doti">:</i>
                                            <asp:Label ID="lbl_father_name" runat="server" Text="{{x.Father_name}}"></asp:Label>
                                        </p>
                                        <p class="stds-info-p">
                                            <i class="stds-info-p-i">ADMISSION NUMBER</i>  <i class="stds-info-p-doti">:</i>
                                            <asp:Label ID="lbl_admission_no" runat="server" Text="{{x.Admission_no}}"></asp:Label>
                                        </p>
                                    </div>

                                    <div class="report-card-std-info-dv-50">
                                        <p class="stds-info-p {{x.MySubjectMarkShowItemII[0].Ranksdv}}">
                                            <i class="stds-info-p-i">RANK</i>  <i class="stds-info-p-doti">:</i>
                                            <asp:Label ID="Label1" runat="server" Text="{{x.MyRank}}"></asp:Label>
                                        </p>
                                        
                                        <p class="stds-info-p">
                                            <i class="stds-info-p-i">SECTION</i>  <i class="stds-info-p-doti">:</i>
                                            <asp:Label ID="lbl_section" runat="server" Text="{{x.Section}}"></asp:Label>
                                        </p>
                                        <p class="stds-info-p">
                                            <i class="stds-info-p-i">ROLL NO.</i>  <i class="stds-info-p-doti">:</i>
                                            <asp:Label ID="lbl_roll_no" runat="server" Text="{{x.Roll_no}}"></asp:Label>
                                        </p>
                                    </div>
                                </div>

                                <div class="subs-mrks-area-dv">
                                    <table>
                                        <tr>
                                            <th class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}">A</th>
                                            <th class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}">SCHOLASTIC AREAS</th>
                                            <th class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}" data-ng-repeat="item in x.MySubjectHeadingAssesment track by $index" colspan="2">{{item.Assessment_Name}} <span class="{{x.MySubjectMarkShowItemII[0].Max_mark_show}}">({{item.Maximum_Marks}})</span></th>
                                            <th colspan="2" class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}"></th>
                                        </tr>
                                        <tr>
                                            <th class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}">SN</th>
                                            <th class="{{x.MySubjectMarkShowItemII[0].Is_text_center}} {{x.MySubjectMarkShowItemII[0].Is_subj_code_hide}}">SUBJECT CODE</th>
                                            <th>SUBJECTS</th>
                                            <th class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}" data-ng-repeat="item in x.MySubjectHeading track by $index">{{item.Short_Name}} <%--<span class="{{x.MySubjectMarkShowItemII[0].Max_mark_show}}">({{item.Maximum_Marks}})</span>--%></th>
                                            <%--<th class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}">{{x.MySubjectMarkShowItemII[0].grade_head_text}} ({{x.MySubjectHeadingAssesment[0].Term_Maximum_Marks}})</th>--%>
                                            <th class="{{x.MySubjectMarkShowItemII[0].Is_text_center}} {{x.MySubjectMarkShowItemII[0].Grade}}">GRADE</th>
                                        </tr>
                                        <tr data-ng-repeat="item in x.MySubjectMarkShowItemII track by $index">
                                            <td class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}">{{$index+1}}</td>
                                            <td class="{{x.MySubjectMarkShowItemII[0].Is_text_center}} {{x.MySubjectMarkShowItemII[0].Is_subj_code_hide}}">{{item.Subject}}</td>
                                            <td>{{item.Subject_name}}</td>
                                            <td class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}" data-ng-repeat="itemx in item.MySubjectMarkItemIII track by $index">{{itemx.Marks}}</td>
                                            <%--<td class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}">{{item.Total_marks}}</td>--%>
                                            <td class="{{x.MySubjectMarkShowItemII[0].Is_text_center}} {{item.Grade}}">{{item.Grade}}</td>
                                        </tr>
                                    </table>
                                </div>


                                <div class="subs-mrks-area-lft-dv">
                                    <div class="subs-mrks-area-b-dv" style="margin-top: {{x.MySubjectMarkShowItemII[0].Co_sch_area_margn}}">
                                        <table>
                                            <tr>
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
                                            </tr>
                                            <tr data-ng-repeat="item in x.MyDescplineData track by $index">
                                                <td>{{$index+1}}</td>
                                                <td>{{item.Activity_name}}</td>
                                                <td>{{item.Term_grade}}</td>
                                            </tr>
                                        </table>
                                    </div>

                                    <div class="prcntage-remrk-dv {{x.MySubjectMarkShowItemII[0].Prcnt_remark}}" style="margin-top: {{x.MySubjectMarkShowItemII[0].Percent_remark_area_margn}}">
                                        <p>Remark :</p>
                                        <span>{{x.MySubjectMarkShowItemII[0].P_remark}}</span>
                                    </div>
                                </div>

                                <div class="subs-mrks-area-rght-dv">
                                    <div class="subs-mrks-area-ovrall-dv" style="margin-top: {{x.MySubjectMarkShowItemII[0].Overall_area_margn}}">
                                        <table>
                                            <tr class="{{x.MySubjectMarkShowItemII[0].Overall_obt_mark}}">
                                                <td>TOTAL OBTAINED MARKS</td>
                                                <td>{{x.MyOverallNoDetailData[0].Overall_obt_marks}}/{{x.MyOverallNoDetailData[0].Overall_full_marks}} <%--{{x.MySubjectMarkShowItemII[0].Overall_obt_mark}}/{{x.MySubjectMarkShowItemII[0].Overall_full_mark}}--%></td>
                                            </tr>
                                            <tr>
                                                <td>OVERALL {{ttlNos[0].Mark_type}} <%--{{x.MySubjectMarkShowItemII[0].Marks_type}}--%></td>
                                                <td>{{x.MyOverallNoDetailData[0].Overall_percents}} <%--{{x.MySubjectMarkShowItemII[0].Overall_percent}}--%> <span class="{{x.MySubjectMarkShowItemII[0].Overall_obt_mark}}">(%)</span></td>
                                            </tr>
                                            <tr class="{{x.MySubjectMarkShowItemII[0].Overall_obt_mark}}">
                                                <td>OVERALL GRADE</td>
                                                <td>{{x.MyOverallNoDetailData[0].Grade}}</td>
                                            </tr>
                                            <tr class="{{x.MySubjectMarkShowItemII[0].Is_attandance_show}}">
                                                <td>TOTAL WORKING DAYS</td>
                                                <td>{{x.MySubjectMarkShowItemII[0].Total_no_of_class}}</td>
                                            </tr>
                                            <tr class="{{x.MySubjectMarkShowItemII[0].Is_attandance_show}}">
                                                <td>TOTAL PRESENT DAYS</td>
                                                <td>{{x.MySubjectMarkShowItemII[0].Present_class}}</td>
                                            </tr>
                                            <tr class="{{x.MySubjectMarkShowItemII[0].Is_attandance_show}}">
                                                <td>ATTENDANCE PERCENT</td>
                                                <td>{{x.MySubjectMarkShowItemII[0].Percent_of_attandance}} (%)</td>
                                            </tr>
                                            <tr class="{{x.MySubjectMarkShowItemII[0].SpecialNote}}">
                                                <td class="spcial-notes" colspan="2">SPECIAL NOTES (IF ANY)</td>
                                            </tr>
                                        </table>
                                    </div>

                                    <div class="qr-dvs {{x.MySubjectMarkShowItemII[0].qr_div_true}}">
                                        <img src="{{x.MySubjectMarkShowItemII[0].qr_code_Show}}" />
                                    </div>

                                    <div class="remarks-rp {{x.MySubjectMarkShowItemII[0].Remarkss}}">
                                        <p>REMARKS : {{x.MySubjectMarkShowItemII[0].Remarkss}}</p>
                                    </div>


                                    <div class="rp-card-graph-sec {{x.MySubjectMarkShowItemII[0].Graph}}" style="margin-top: {{x.MySubjectMarkShowItemII[0].Graph_area_margn}}">
                                        <div class="rp-card-graph-wpr">
                                            <p class="rp-card-graph-txt0">0</p>
                                            <p class="rp-card-graph-txt50">50</p>
                                            <p class="rp-card-graph-txt100">100</p>
                                            <div class="rp-card-graph-tbl-wpr" style="height: {{x.MySubjectMarkShowItemII[0].GraphHeight}}">
                                                <div class="rp-card-graph-tbl-inr" data-ng-repeat="item in x.MyGraphDetailData track by $index" style="width: {{item.Grph_width}}%;">
                                                    <div class="rp-card-graph-tbl-nobg" style="height: {{item.BlankHeight}}%"></div>
                                                    <div class="rp-card-graph-tbl" style="height: {{item.Total_obtained_mark}}%"></div>
                                                    <p>{{item.Subject_name}}</p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="sig-dv v-false {{x.MySubjectMarkShowItemII[0].Sign_top}}">
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

                                <div class="instruction-dv">
                                    <div class="instruction-50 instruction-50-pr">
                                        <div class="instruction-tbls" style="margin-top: {{x.MySubjectMarkShowItemII[0].Ins1_area_margn}}">
                                            <table>
                                                <tr>
                                                    <th colspan="2" class="txt-center">INSTRUCTIONS</th>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" class="txt-center">(Scholastic & Co-Scholastic Areas {{x.MyMarkRangeData[0].RowCount}}-point grading scale)</td>
                                                </tr>
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
                                            <div class="v-false {{x.MySubjectMarkShowItemII[0].Instruction2}}" style="margin-top: {{x.MySubjectMarkShowItemII[0].Ins2_area_margn}}">
                                                <table>
                                                    <tr>
                                                        <th colspan="2" class="txt-center">INSTRUCTIONS</th>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" class="txt-center">(Co-Scholastic Areas {{x.MyGradeRemarkData[0].RowCount}}-point grading scale)</td>
                                                    </tr>
                                                    <tr>
                                                        <td>GRADE</td>
                                                        <td>REMARKS</td>
                                                    </tr>
                                                    <tr data-ng-repeat="item in x.MyGradeRemarkData track by $index">
                                                        <td>{{item.Grade_name}}</td>
                                                        <td>{{item.Grade_remark}}</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="na-td-inst" colspan="2">* NA = Not Applicable</td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="v-false {{x.MySubjectMarkShowItemII[0].Ranksdv}}" style="margin-top: {{x.MySubjectMarkShowItemII[0].Toppers_area_margn}}">
                                                <table>
                                                    <tr>
                                                        <th colspan="5" class="txt-center">TOPPERS</th>
                                                    </tr>
                                                    <tr>
                                                        <th>Rank</th>
                                                        <th>Name</th>
                                                        <th>Marks</th>
                                                        <th>Marks(%)</th>
                                                        <th>Grade</th>
                                                    </tr>
                                                    <tr data-ng-repeat="item in x.MyRankDetailData track by $index">
                                                        <td>{{item.Rank}}</td>
                                                        <td>{{item.Student_name}}</td>
                                                        <td>{{item.Total_obtained_mark}}</td>
                                                        <td>{{item.Mark_percentage}}</td>
                                                        <td>{{item.Grade}}</td>
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

        <asp:HiddenField ID="hd_section" runat="server" />
        <asp:HiddenField ID="hd_session_id" runat="server" />
        <asp:HiddenField ID="hd_class_id" runat="server" />
        <asp:HiddenField ID="hd_branch_id" runat="server" />
        <asp:HiddenField ID="hd_term_id" runat="server" />

        <script type="text/javascript">
            var app = angular.module('RpCardApp', []);
            app.controller('RpCardAppCtrl', function ($scope, $http) {

                var session_id = $("#<%=hd_session_id.ClientID%>").val();
                var class_id = $("#<%=hd_class_id.ClientID%>").val();
                var section = $("#<%=hd_section.ClientID%>").val();
                var branch_id = $("#<%=hd_branch_id.ClientID%>").val();
                var term_id = $("#<%=hd_term_id.ClientID%>").val();


                messge("Please Wait...");
                $("#intsLoader").removeClass("hidden");
                $http.get("api/bulk-report-card-termI-P2.asmx/fetch_rp_card_bulks", { params: { "Session_id": session_id, "Class_id": class_id, "Section": section, "Branch_id": branch_id, "Term_id": term_id } }).then(function (response) {
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
