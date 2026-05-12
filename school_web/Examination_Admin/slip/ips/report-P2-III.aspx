<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="report-P2-III.aspx.cs" Inherits="school_web.Examination_Admin.slip.ips.report_P2_III" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Report Card</title> 
    <link href="report-card.css" rel="stylesheet" /> 
    <script src="../../../assets/js/jquery-1.10.2.min.js"></script>
    <script src="../../../assets/Angular/angular.min.js"></script>

    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="report-card.css" rel="stylesheet" type="text/css" />');
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
                <div class="invoice-inr-sec">
                    <div class="invoice-wpr" style="border: 5px solid #87878700;">
                        <asp:Image ID="img_watermark" runat="server" class="wtr-mrk-img {{reportCardSubS[0].Is_watermark_show}}" />
                        <div class="report-card-wpr" style="height: {{reportCardSubS[0].Height_dv}}; border: 5px solid #87878700;">
                            <div class="report-card-head">
                                <div class="report-card-left-dv" style="height: 5px;">
                                    <asp:Image ID="Image1" runat="server" Style="display: none" />
                                    <asp:Label ID="lbl_estd" runat="server" Style="display: none" class="estdTextP v-false"></asp:Label>
                                </div>
                                <div class="report-card-cntr-dv">
                                    <asp:Label ID="lbl_school_name" class="report-card-schlname" Style="display: none" runat="server"></asp:Label>
                                    <asp:Label ID="lbl_aff_text" class="report-card-schl-affno-by" Style="display: none" runat="server"></asp:Label>
                                    <asp:Label ID="lbl_address" runat="server" Style="display: none" class="report-card-schl-add"></asp:Label>
                                    <asp:Label ID="lbl_contact_no" runat="server" Style="display: none" Text="" class="report-card-schl-cont v-false {{reportCardSubS[0].Is_contact_no_show}}"></asp:Label>
                                    <p class="report-card-schl-emil v-false {{reportCardSubS[0].Is_email_show}}" style="display: none">
                                        Email : 
                                        <asp:Label ID="lbl_email" runat="server"></asp:Label>
                                    </p>
                                    <h2 class="report-card-ac-sson">ACADEMIC SESSION:
                                        <asp:Label ID="lbl_sessions" runat="server"></asp:Label></h2>
                                    <h2 class="report-card-rprt-crd">
                                        <asp:Label ID="lbl_for_term" runat="server"></asp:Label>
                                        REPORT CARD  <span class="{{reportCardSubS[0].Class_in_new_line}}"><span class="v-false {{reportCardSubS[0].Is_class_text_show}}">CLASS</span>
                                            <asp:Label ID="lbl_for_class" runat="server"></asp:Label></span></h2>
                                </div>
                                <asp:Label ID="lbl_aff_no" class="report-card-schl-affno" style="display: none" runat="server"></asp:Label>
                                <div class="report-card-rght-dv ">
                                    <asp:Image ID="img_student_img" runat="server" class="{{reportCardSubS[0].Is_std_img_hide}}" />
                                </div>
                            </div>

                            <div class="report-card-std-info-dv">
                                <div class="report-card-std-info-dv-50">
                                    <p class="stds-info-p">
                                        <i class="stds-info-p-i">Student's Name</i>  <i class="stds-info-p-doti">:</i>
                                        <asp:Label ID="lbl_student_name" runat="server" Text="LIZZA KHATUN"></asp:Label>
                                    </p>
                                    <p class="stds-info-p">
                                        <i class="stds-info-p-i">DATE OF BIRTH</i>  <i class="stds-info-p-doti">:</i>
                                        <asp:Label ID="lbl_date_of_birth" runat="server" Text="26/04/2004"></asp:Label>
                                    </p> 
                                </div>

                                <div class="report-card-std-info-dv-50"> 
                                    <p class="stds-info-p">
                                        <i class="stds-info-p-i">ADMISSION NUMBER</i>  <i class="stds-info-p-doti">:</i>
                                        <asp:Label ID="lbl_admission_no" runat="server" Text="5805"></asp:Label>
                                    </p>  
                                    <p class="stds-info-p">
                                        <i class="stds-info-p-i">ROLL NO.</i>  <i class="stds-info-p-doti">:</i>
                                        <asp:Label ID="lbl_roll_no" runat="server" Text="25"></asp:Label>
                                    </p>
                                </div>
                            </div>

                            <div class="subs-mrks-area-dv">
                                <table>
                                    <tr>
                                        <th class="txtcenter">A</th>
                                        <th class="txtcenter">SCHOLASTIC AREAS</th>
                                        <th data-ng-repeat="x in reportCardAssesment" class="txtcenter" rowspan="{{x.RowSpan}}" colspan="{{x.Sub_level_count}}">{{x.Assessment_Name}}</th>
                                        <th rowspan="2" class="txtcenter">{{reportCardSubS[0].grade_head_text}} ({{reportCardAssesment[0].Term_Maximum_Marks}})</th>
                                        <th rowspan="2" class="{{reportCardSubS[0].Grade}} txtcenter">HIGHEST MARK</th>
                                    </tr>
                                    <tr>
                                        <th class="txtcenter">SN</th> 
                                        <th class="txtcenter">SUBJECTS</th>
                                        <th class="txtcenter" data-ng-repeat="x in reportCard">{{x.Subject_Activity_Name}}  <%--<span class="{{reportCardSubS[0].Max_mark_show}}">({{x.Maximum_Marks}})</span>--%></th>
                                    </tr>

                                    <tr data-ng-repeat="x in reportCardSubS track by $index">
                                        <td class="txtcenter">{{$index+1}}</td>
                                        <%--<td class="txtcenter">{{x.Subject}}</td>--%>
                                        <td class="txtcenter">{{x.Subject_name}}</td>
                                        <td class="txtcenter" data-ng-repeat="item in x.MySubjectMarkItem track by $index">{{item.Marks}}</td>
                                        <td class="txtcenter">{{x.Total_marks}}</td>
                                        <td class="txtcenter">{{x.Subj_highest_mark}}</td>
                                    </tr>
                                </table>
                            </div>


                            <div class="subs-mrks-area-lft-dv">
                                <div class="subs-mrks-area-b-dv" style="margin-top: {{reportCardSubS[0].Co_sch_area_margn}}">
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
                                        <tr data-ng-repeat="x in reportCardCoScholastic">
                                            <td>{{$index+1}}</td>
                                            <td>{{x.Subject_Name}}</td>
                                            <td>{{x.Total_marks}}</td>
                                        </tr>
                                        <tr class="{{reportCardSubS[0].Is_discipline_hide}}">
                                            <th>C</th>
                                            <th colspan="2">DISCIPLINE [ 5 - POINT GRADING SCALE]</th>
                                        </tr>
                                        <tr class="{{reportCardSubS[0].Is_discipline_hide}}">
                                            <th class="th-bg-rmov">SN</th>
                                            <th class="th-bg-rmov">ACTIVITIES</th>
                                            <th class="th-bg-rmov">GRADE</th>
                                        </tr>
                                        <tr class="{{reportCardSubS[0].Is_discipline_hide}}" data-ng-repeat="x in reportCardDesciplineActivitY">
                                            <td>{{$index+1}}</td>
                                            <td>{{x.Activity_name}}</td>
                                            <td>{{x.Term_grade}}</td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="prcntage-remrk-dv v-false {{reportCardSubS[0].Prcnt_remark}}" style="margin-top: {{reportCardSubS[0].Percent_remark_area_margn}}">
                                    <p>Remark :</p>
                                    <span>{{ttlNos[0].P_remark}}</span>
                                </div>
                            </div>

                            <div class="subs-mrks-area-rght-dv">
                                <div class="subs-mrks-area-ovrall-dv" style="margin-top: {{reportCardSubS[0].Overall_area_margn}}">
                                    <table>
                                        <tr class="{{reportCardSubS[0].Overall_obt_mark}}">
                                            <td>TOTAL OBTAINED MARKS</td>
                                            <td>{{ttlNos[0].Overall_obt_marks}}/{{ttlNos[0].Overall_full_marks}}</td>
                                        </tr>
                                        <tr>
                                            <td>OVERALL {{ttlNos[0].Mark_type}}</td>
                                            <td>{{ttlNos[0].Overall_percents}} <span class="{{reportCardSubS[0].Overall_obt_mark}}">(%)</span></td>
                                        </tr>
                                        <tr class="{{reportCardSubS[0].Overall_obt_mark}}">
                                            <td>OVERALL GRADE</td>
                                            <td>{{ttlNos[0].Grade}}</td>
                                        </tr>
                                        <tr class=" {{reportCardSubS[0].Is_attandance_show}} {{reportCardSubS[0].Is_attendance_hide_claaswise}}">
                                            <td>TOTAL WORKING DAYS</td>
                                            <td>{{reportCardSubS[0].Total_no_of_class}}</td>
                                        </tr>
                                        <tr class=" {{reportCardSubS[0].Is_attandance_show}} {{reportCardSubS[0].Is_attendance_hide_claaswise}}">
                                            <td>TOTAL PRESENT DAYS</td>
                                            <td>{{reportCardSubS[0].Present_class}}</td>
                                        </tr>
                                        <tr class=" {{reportCardSubS[0].Is_attandance_show}} {{reportCardSubS[0].Is_attendance_hide_claaswise}}">
                                            <td>ATTENDANCE PERCENT</td>
                                            <td>{{reportCardSubS[0].Percent_of_attandance}} (%)</td>
                                        </tr>
                                        <tr class=" {{reportCardSubS[0].SpecialNote}}">
                                            <td class="spcial-notes" colspan="2">SPECIAL NOTES (IF ANY)</td>
                                        </tr>
                                    </table>
                                </div>

                                <div class="qr-dvs v-false {{reportCardSubS[0].qr_div_true}}">
                                    <img src="{{reportCardSubS[0].qr_code_Show}}" />
                                </div>

                                <div class="remarks-rp v-false {{reportCardSubS[0].Remarkss}}">
                                    <p>REMARKS : {{reportCardSubS[0].Remarkss}}</p>
                                </div>


                                <div class="rp-card-graph-sec v-false {{reportCardSubS[0].Graph}}" style="margin-top: {{reportCardSubS[0].Graph_area_margn}}">
                                    <div class="rp-card-graph-wpr">
                                        <p class="rp-card-graph-txt0">0</p>
                                        <p class="rp-card-graph-txt50">50</p>
                                        <p class="rp-card-graph-txt100">100</p>
                                        <div class="rp-card-graph-tbl-wpr" style="height: {{reportCardSubS[0].GraphHeight}}">
                                            <div class="rp-card-graph-tbl-inr" data-ng-repeat="x in reportGraphs" style="width: {{x.Grph_width}}%;">
                                                <div class="rp-card-graph-tbl-nobg" style="height: {{x.BlankHeight}}%"></div>
                                                <div class="rp-card-graph-tbl" style="height: {{x.Total_obtained_mark}}%"></div>
                                                <p>{{x.Subject_name}}</p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>




                             







                            <div class="instruction-dv">
                                <div class="instruction-50 instruction-50-pr">
                                    <div class="instruction-tbls" style="margin-top: {{reportCardSubS[0].Ins1_area_margn}}">
                                        <table>
                                            <tr>
                                                <th colspan="2" class="txt-center">INSTRUCTIONS</th>
                                            </tr>
                                            <tr>
                                                <td colspan="2" class="txt-center">(Scholastic and Co-Scholastic Areas {{reportCardmarkRange[0].RowCount}}-point grading scale)</td>
                                            </tr>
                                            <tr>
                                                <td>MARKS RANGE</td>
                                                <td>GRADE</td>
                                            </tr>
                                            <tr data-ng-repeat="x in reportCardmarkRange">
                                                <td>{{x.Lower_Range}}-{{x.Upper_Range}}</td>
                                                <td>{{x.Grade}}</td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <div class="instruction-50 instruction-50-pl">
                                    <div class="instruction-tbls floatrght">
                                        <div class="v-false {{reportCardSubS[0].Instruction2}}" style="margin-top: {{reportCardSubS[0].Ins2_area_margn}}">
                                            <table>
                                                <tr>
                                                    <th colspan="2" class="txt-center">INSTRUCTIONS</th>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" class="txt-center">(Co-Scholastic Areas {{reportCardgeadeRemark[0].RowCount}}-point grading scale)</td>
                                                </tr>
                                                <tr>
                                                    <td>MARKS RANGE</td>
                                                    <td>GRADE</td>
                                                </tr>
                                                <tr data-ng-repeat="x in reportCardgeadeRemark">
                                                    <td>{{x.Lower_Range}}-{{x.Upper_Range}}</td>
                                                    <td>{{x.Grade}}</td>
                                                </tr>
                                                <tr>
                                                    <td class="na-td-inst" colspan="2">* NA = Not Applicable</td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div class="v-false {{reportCardSubS[0].Ranksdv}}" style="margin-top: {{reportCardSubS[0].Toppers_area_margn}}">
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
                                                <tr data-ng-repeat="x in reportRank">
                                                    <td>{{x.Rank}}</td>
                                                    <td>{{x.Student_name}}</td>
                                                    <td>{{x.Total_obtained_mark}}</td>
                                                    <td>{{x.Mark_percentage}}</td>
                                                    <td>{{x.Grade}}</td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div class="sig-dv v-false {{reportCardSubS[0].Sign_bottom}}">
                                <div class="sig-left">
                                    <div class="lft-sig-img-dv">
                                        <img class="lft-sig-img {{reportSig[0].Signature_image}}" src="{{reportSig[0].Signature_image}}" />
                                    </div>
                                    <p class="sig-ps">{{reportSig[0].Signature_name}}</p>
                                </div>
                                <div class="sig-left">
                                    <div class="cntr-sig-img-dv">
                                        <%--<img src="{{reportSig[1].Signature_image}}" class="cntr-sig-img {{reportSig[1].Signature_image}}" />--%>
                                    </div>
                                    <%--<p class="sig-ps">{{reportSig[1].Signature_name}}</p>--%>
                                </div>

                                <div class="sig-left">
                                    <div class="rght-sig-img-dv">
                                        <img src="{{reportSig[2].Signature_image}}" class="rght-sig-img {{reportSig[2].Signature_image}}" />
                                    </div>
                                    <p class="sig-ps">{{reportSig[2].Signature_name}}</p>
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

        <script type="text/javascript">
            var app = angular.module('RpCardApp', []);
            app.controller('RpCardAppCtrl', function ($scope, $http) {

                var session_id = $("#<%=hd_session_id.ClientID%>").val();
                var class_id = $("#<%=hd_class_id.ClientID%>").val();
                var admission_no = $("#<%=hd_admission_no.ClientID%>").val();
                var branch_id = $("#<%=hd_branch_id.ClientID%>").val();
                var term_id = $("#<%=hd_term_id.ClientID%>").val();


                messge("Please Wait...");
                $("#intsLoader").removeClass("hidden");

                $http.get("api/report-P2-III.asmx/fetch_report_card_assesment", { params: { "Session_id": session_id, "Class_id": class_id, "Admission_no": admission_no, "Branch_id": branch_id, "Term_id": term_id } }).then(function (response) {
                    $scope.reportCardAssesment = response.data;
                })


                $http.get("api/report-P2-III.asmx/fetch_report_card", { params: { "Session_id": session_id, "Class_id": class_id, "Admission_no": admission_no, "Branch_id": branch_id, "Term_id": term_id } }).then(function (response) {
                    $scope.reportCard = response.data;
                })


                $http.get("api/report-P2-III.asmx/fetch_rp_card_subjects", { params: { "Session_id": session_id, "Class_id": class_id, "Admission_no": admission_no, "Branch_id": branch_id, "Term_id": term_id } }).then(function (response) {
                    $scope.reportCardSubS = response.data;
                    $("#intsLoader").addClass("hidden");


                    if ($scope.reportCardSubS == "") {
                    }
                    else {
                        ////========================GRAPH
                        $http.get("api/report-P2-III.asmx/fetch_rp_card_graph", { params: { "Session_id": session_id, "Class_id": class_id, "Admission_no": admission_no, "Branch_id": branch_id, "Term_id": term_id } }).then(function (response) {
                            $scope.reportGraphs = response.data;
                        })


                        ////========================OverAll No.
                        $http.get("api/report-P2-III.asmx/fetch_rp_card_total_no", { params: { "Session_id": session_id, "Class_id": class_id, "Admission_no": admission_no, "Branch_id": branch_id, "Term_id": term_id } }).then(function (response) {
                            $scope.ttlNos = response.data;
                        })
                    }
                })


                $http.get("api/report-P2-III.asmx/fetch_rp_card_marks_range", { params: { "Session_id": session_id, "Branch_id": branch_id, "Term_id": term_id } }).then(function (response) {
                    $scope.reportCardmarkRange = response.data;
                })


                ////========================Get Scholastic Data
                $http.get("api/report-P2-III.asmx/fetch_report_card_coscholastic", { params: { "Session_id": session_id, "Class_id": class_id, "Admission_no": admission_no, "Branch_id": branch_id, "Term_id": term_id } }).then(function (response) {
                    $scope.reportCardCoScholastic = response.data;
                })


                ////========================Get Discipline Data
                $http.get("api/report-P2-III.asmx/fetch_report_card_discipline_activity", { params: { "Session_id": session_id, "Class_id": class_id, "Admission_no": admission_no, "Branch_id": branch_id, "Term_id": term_id } }).then(function (response) {
                    $scope.reportCardDesciplineActivitY = response.data;
                })



                $http.get("api/report-P2-III.asmx/fetch_rp_card_grade_remark", { params: { "Session_id": session_id, "Class_id": class_id, "Admission_no": admission_no, "Branch_id": branch_id, "Term_id": term_id } }).then(function (response) {
                    $scope.reportCardgeadeRemark = response.data;
                })

                $http.get("api/report-P2-III.asmx/fetch_rp_card_signature", { params: { "Session_id": session_id, "Class_id": class_id, "Admission_no": admission_no, "Branch_id": branch_id } }).then(function (response) {
                    $scope.reportSig = response.data;
                })


                ////========================Rank
                $http.get("api/report-P2-III.asmx/fetch_rp_card_rank", { params: { "Session_id": session_id, "Class_id": class_id, "Admission_no": admission_no, "Branch_id": branch_id, "Term_id": term_id } }).then(function (response) {
                    $scope.reportRank = response.data;
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
