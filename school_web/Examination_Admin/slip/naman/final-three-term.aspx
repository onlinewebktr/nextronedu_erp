<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="final-three-term.aspx.cs" Inherits="school_web.Examination_Admin.slip.naman.final_three_term" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Report Card</title>
    <link href="css/final-three-term.css" rel="stylesheet" />
    <script src="../../../assets/js/jquery-1.10.2.min.js"></script>
    <script src="../../../assets/Angular/angular.min.js"></script>

    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="css/final-three-term.css" rel="stylesheet" type="text/css" />');
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
                <div class="invoice-inr-sec {{x.MySubjectMarkShowItem[0].PrintShow}}" data-ng-repeat="x in reportCardS track by $index">
                    <div class="invoice-wpr">
                        <img src="{{x.Watermar_image}}" class="wtr-mrk-img" />
                        <div class="report-card-wpr" style="height: {{x.MySubjectMarkShowItem[0].Height_dv}}">
                            <div class="report-card-head {{x.Content_header}}">
                                <div class="report-card-left-dv">
                                    <img src="{{x.Frim_logo}}" />
                                    <asp:Label ID="lbl_estd" runat="server" Text="{{x.Estd}}" class="estdTextP v-false {{x.MySubjectMarkShowItemII[0].Is_estd_show}}"></asp:Label>
                                </div>
                                <div class="report-card-cntr-dv">
                                    <asp:Label ID="lbl_school_name" class="report-card-schlname" runat="server" Text="{{x.Firm_name}}"></asp:Label>
                                    <asp:Label ID="lbl_aff_text" class="report-card-schl-affno-by" runat="server" Text="{{x.Affiliated_by_full_text}}"></asp:Label>
                                    <asp:Label ID="lbl_address" runat="server" class="report-card-schl-add" Text="{{x.Firm_address}}"></asp:Label>
                                    <asp:Label ID="lbl_contact_no" runat="server" class="report-card-schl-cont v-false {{x.MySubjectMarkShowItemII[0].Is_contact_no_show}}" Text="{{x.Firm_contact_no}}"></asp:Label>
                                    <p class="report-card-schl-emil v-false {{x.MySubjectMarkShowItemII[0].Is_email_show}}">
                                        Email : 
                                        <asp:Label ID="lbl_email" runat="server" Text="{{x.Firm_email}}"></asp:Label>
                                    </p>

                                    <p class="report-card-schl-emil v-false {{x.MySubjectMarkShowItemII[0].Is_website_show}}">
                                        Website : 
                                        <asp:Label ID="lbl_website" runat="server" Text="{{x.Website}}"></asp:Label>
                                    </p>
                                </div>
                                <asp:Label ID="lbl_aff_no" class="report-card-schl-affno {{x.Frim_aff_no}}" runat="server" Text="{{x.MySubjectMarkShowItemII[0].Aff_text}} : {{x.Frim_aff_no}}"></asp:Label>
                            </div>




                            <div class="image-headers {{x.Header_templete}}">
                                <img src="{{x.Header_templete}}" />
                            </div>
                            <div class="hdr-exm-info">
                                <%--<h2 class="hdr-exm-info-name">PROGRESS REPORT</h2>--%>
                                <p class="hdr-exm-info-session">ANNUAL REPORT CARD FOR ACADEMIC SESSION : {{x.Session}}</p>
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
                                    <p class="stds-info-p  {{x.MySubjectMarkShowItem[0].Father_name1}}">
                                        <i class="stds-info-p-i">FATHER'S NAME</i>  <i class="stds-info-p-doti">:</i>
                                        <asp:Label ID="lbl_father_name" runat="server" Text="{{x.Father_name}}"></asp:Label>
                                    </p>
                                </div>
                                <div class="{{x.MySubjectMarkShowItem[0].duesclassdesabled2}}"></div>
                                <div class="wdth37">
                                    <%--<p class="stds-info-p {{x.MySubjectMarkShowItem[0].Ranksdv}}">
                                        <i class="stds-info-p-i">Rank</i>  <i class="stds-info-p-doti">:</i>
                                        <asp:Label ID="lbl_rank" runat="server">{{x.MyRpRanksItem[3].Rank}}</asp:Label>
                                    </p>--%>
                                    <p class="stds-info-p">
                                        <i class="stds-info-p-i wdth45">ADMISSION NUMBER</i>  <i class="stds-info-p-doti">:</i>
                                        <asp:Label ID="lbl_admission_no" runat="server" Text="{{x.Admission_no}}"></asp:Label>
                                    </p>
                                    <p class="stds-info-p">
                                        <i class="stds-info-p-i wdth45">Class </i><i class="stds-info-p-doti">:</i>
                                        <asp:Label ID="lbl_class" runat="server" Text="{{x.For_class}}"></asp:Label>
                                    </p>
                                    <p class="stds-info-p {{x.MySubjectMarkShowItem[0].Is_std_section_hide}}">
                                        <i class="stds-info-p-i wdth45">SECTION</i>  <i class="stds-info-p-doti">:</i>
                                        <asp:Label ID="lbl_section" runat="server" Text="{{x.Section}}"></asp:Label>
                                    </p>
                                    <p class="stds-info-p">
                                        <i class="stds-info-p-i wdth45">ROLL NO.</i>  <i class="stds-info-p-doti">:</i>
                                        <asp:Label ID="lbl_roll_no" runat="server" Text="{{x.Roll_no}}"></asp:Label>
                                    </p>
                                </div>
                                <div class="report-card-std-img-dv">
                                    <div class="sdt-img-dv">
                                        <img src="{{x.Student_image}}" class="{{x.MySubjectMarkShowItemII[0].Is_std_img_hide}} {{x.Student_image}}" />
                                    </div>
                                </div>
                            </div>

                            <div class="subs-mrks-area-dv">
                                <table>
                                    <tr>
                                        <th colspan="19" class="{{x.MySubjectMarkShowItem[0].Is_text_center}}">SCHOLASTIC AREA</th>
                                    </tr>
                                    <tr>
                                        <th>SN</th>
                                        <th style="width: 175px;">SUBJECTS</th>
                                        <th class=" {{x.MySubjectMarkShowItem[0].Is_text_center}}">MAX. MARKS</th>
                                        <th class="{{x.MySubjectMarkShowItem[0].If_is_grade_ttl_mark_hide}}  {{x.MySubjectMarkShowItem[0].Is_text_center}}">{{x.MySubjectHeading[0].Term_Name}} <%--({{x.MyRpTermNumberItem[0].Term_Maximum_Marks}})--%></th>
                                        <th class="{{x.MySubjectMarkShowItem[0].If_is_grade_ttl_mark_hide}} {{x.MySubjectMarkShowItem[0].Is_text_center}}">{{x.MySubjectHeading[1].Term_Name}} <%--({{x.MyRpTermNumberItem[1].Term_Maximum_Marks}})--%></th>
                                        <th class="{{x.MySubjectMarkShowItem[0].If_is_grade_ttl_mark_hide}}  {{x.MySubjectMarkShowItem[0].Is_text_center}}">{{x.MySubjectHeading[2].Term_Name}} <%--({{x.MyRpTermNumberItem[2].Term_Maximum_Marks}})--%></th>
                                        <th class="{{x.MySubjectMarkShowItem[0].Overall_av_marks}} {{x.MySubjectMarkShowItem[0].Is_text_center}}">GRAND TOTAL<%--({{x.MySubjectHeading[0].Total_Marks}})--%></th>
                                        <th class="{{x.MySubjectMarkShowItem[0].Overall_av_marks}} {{x.MySubjectMarkShowItem[0].Is_text_center}}">AVERAGE</th>
                                        <th class="{{x.MySubjectMarkShowItem[0].Overall_av_marks}} {{x.MySubjectMarkShowItem[0].Is_text_center}}">GR.</th>
                                    </tr>
                                    <tr data-ng-repeat="item in x.MySubjectMarkShowItem track by $index">
                                        <td>{{$index+1}}</td>
                                        <td>{{item.Subject_name}}</td>
                                        <td class=" {{x.MySubjectMarkShowItem[0].Is_text_center}}">{{item.Fub_full_mark}}</td>
                                        <td class="{{x.MySubjectMarkShowItem[0].If_is_grade_ttl_mark_hide}} {{x.MySubjectMarkShowItem[0].Is_text_center}}">{{item.Total_mark_of_a_subject_for_termI}}</td>
                                        <td class="{{x.MySubjectMarkShowItem[0].If_is_grade_ttl_mark_hide}} {{x.MySubjectMarkShowItem[0].Is_text_center}}">{{item.Total_mark_of_a_subject_for_termII}}</td>
                                        <td class="{{x.MySubjectMarkShowItem[0].If_is_grade_ttl_mark_hide}} {{x.MySubjectMarkShowItem[0].Is_text_center}}">{{item.Total_mark_of_a_subject_for_termIII}}</td>
                                        <td class="{{x.MySubjectMarkShowItem[0].Overall_av_marks}} {{x.MySubjectMarkShowItem[0].Is_text_center}}">{{item.Total_marks_of_a_subject}}</td>
                                        <td class="{{x.MySubjectMarkShowItem[0].Overall_av_marks}} {{x.MySubjectMarkShowItem[0].Is_text_center}}">{{item.termI_termIi_average_percent}}</td>
                                        <td class="{{x.MySubjectMarkShowItem[0].Overall_av_marks}} {{x.MySubjectMarkShowItem[0].Is_text_center}}">{{item.termI_termII_grade}}</td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="txtrght font-weight600">TOTAL</td>
                                        <td class="{{x.MySubjectMarkShowItem[0].Is_text_center}} font-weight600">{{x.MySubjectMarkShowItem[x.MySubjectMarkShowItem[0].RowscountS].TotalMaxMarks}}</td>
                                        <td class="{{x.MySubjectMarkShowItem[0].Is_text_center}} font-weight600">{{x.MyRpTotalCalculationItem[0].Obtained_Mark_TermI}}</td>
                                        <td class="{{x.MySubjectMarkShowItem[0].Is_text_center}} font-weight600">{{x.MyRpTotalCalculationItem[0].Obtained_Mark_TermII}}</td>
                                        <td class="{{x.MySubjectMarkShowItem[0].Is_text_center}} font-weight600">{{x.MyRpTotalCalculationItem[0].Obtained_Mark_TermIII}}</td>
                                        <td class="{{x.MySubjectMarkShowItem[0].Is_text_center}} font-weight600">{{x.MySubjectMarkShowItem[x.MySubjectMarkShowItem[0].RowscountS].totaloverALLobtMarkSubj}}</td>
                                        <td class="{{x.MySubjectMarkShowItem[0].Is_text_center}} font-weight600">{{x.MySubjectMarkShowItem[x.MySubjectMarkShowItem[0].RowscountS].OverallAverage}}</td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="txtrght font-weight600">TOTAL PERCENT</td>
                                        <td></td>
                                        <td class=" {{x.MySubjectMarkShowItem[0].Is_text_center}} font-weight600">{{x.MyRpTotalCalculationItem[0].TermI_perc}}</td>
                                        <td class=" {{x.MySubjectMarkShowItem[0].Is_text_center}} font-weight600">{{x.MyRpTotalCalculationItem[0].TermII_perc}}</td>
                                        <td class=" {{x.MySubjectMarkShowItem[0].Is_text_center}} font-weight600">{{x.MyRpTotalCalculationItem[0].TermIII_perc}}</td>
                                        <td class=" {{x.MySubjectMarkShowItem[0].Is_text_center}} font-weight600">{{x.MyRpTotalCalculationItem[0].Overall_percents}}</td>

                                        <td colspan="2"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="txtrght font-weight600">OVERALL GRADE</td>
                                        <td></td>
                                        <td class=" {{x.MySubjectMarkShowItem[0].Is_text_center}} font-weight600">{{x.MyRpTotalCalculationItem[0].TermGradeT1}}</td>
                                        <td class=" {{x.MySubjectMarkShowItem[0].Is_text_center}} font-weight600">{{x.MyRpTotalCalculationItem[0].TermGradeT2}}</td>
                                        <td class=" {{x.MySubjectMarkShowItem[0].Is_text_center}} font-weight600">{{x.MyRpTotalCalculationItem[0].TermGradeT3}}</td>
                                        <td class=" {{x.MySubjectMarkShowItem[0].Is_text_center}} font-weight600">{{x.MyRpTotalCalculationItem[0].Grade}}</td>
                                        <td colspan="2"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="txtrght font-weight600">RANK</td>
                                        <td></td>
                                        <td class=" {{x.MySubjectMarkShowItem[0].Is_text_center}} font-weight600">{{x.MyRpTotalCalculationItem[0].RankT1}}</td>
                                        <td class=" {{x.MySubjectMarkShowItem[0].Is_text_center}} font-weight600">{{x.MyRpTotalCalculationItem[0].RankT2}}</td>
                                        <td class=" {{x.MySubjectMarkShowItem[0].Is_text_center}} font-weight600">{{x.MyRpTotalCalculationItem[0].RankT3}}</td>
                                        <td class=" {{x.MySubjectMarkShowItem[0].Is_text_center}} font-weight600">{{x.MyRpTotalCalculationItem[0].RankFinal}}</td>
                                        <td colspan="2"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="txtrght font-weight600">ATTENDANCE</td>
                                        <td></td>
                                        <td class=" {{x.MySubjectMarkShowItem[0].Is_text_center}} font-weight600">{{x.MySubjectMarkShowItem[0].Total_Present_class_of_term_I}}/{{x.MySubjectMarkShowItem[0].Total_class_of_term_I}}</td>
                                        <td class=" {{x.MySubjectMarkShowItem[0].Is_text_center}} font-weight600">{{x.MySubjectMarkShowItem[0].Total_Present_class_of_term_II}}/{{x.MySubjectMarkShowItem[0].Total_class_of_term_II}}</td>
                                        <td class=" {{x.MySubjectMarkShowItem[0].Is_text_center}} font-weight600">{{x.MySubjectMarkShowItem[0].Total_Present_class_of_term_III}}/{{x.MySubjectMarkShowItem[0].Total_class_of_term_III}}</td>
                                        <td class=" {{x.MySubjectMarkShowItem[0].Is_text_center}} font-weight600">{{x.MySubjectMarkShowItem[0].Final_persent_days}}/{{x.MySubjectMarkShowItem[0].Final_working_days}}</td>
                                        <td colspan="2"></td>
                                    </tr>
                                </table>
                            </div>

                            <div class="subs-mrks-area-lft-dv">
                                <div class="subs-mrks-area-b-dv" style="margin-top: 5px">
                                    <table>
                                        <%--<tr>
                                            <th colspan="5" class=" {{x.MySubjectMarkShowItem[0].Is_text_center}}">CO-SCHOLASTIC AREAS</th>
                                        </tr>--%>
                                        <tr>
                                            <th>SN</th>
                                            <th>CO-SCHOLASTIC AREAS</th>
                                            <th class="{{x.MySubjectMarkShowItem[0].Is_text_center}}">{{x.MySubjectHeading[0].Term_Name}}</th>
                                            <th class="{{x.MySubjectMarkShowItem[0].Is_text_center}}">{{x.MySubjectHeading[1].Term_Name}}</th>
                                            <th class="{{x.MySubjectMarkShowItem[0].Is_text_center}}">{{x.MySubjectHeading[2].Term_Name}}</th>
                                        </tr>
                                        <tr data-ng-repeat="itemx in x.MyRpCoScholosticItem track by $index">
                                            <td>{{$index+1}}</td>
                                            <td>{{itemx.Subject_Name}}</td>
                                            <td class=" {{x.MySubjectMarkShowItem[0].Is_text_center}}">{{itemx.Total_marks_t1}}</td>
                                            <td class=" {{x.MySubjectMarkShowItem[0].Is_text_center}}">{{itemx.Total_marks_t2}}</td>
                                            <td class=" {{x.MySubjectMarkShowItem[0].Is_text_center}}">{{itemx.Total_marks_t3}}</td>
                                        </tr>


                                    </table>
                                </div>
                            </div>



                            <div class="rp-card-graph-sec {{x.IsgraphShow}}" style="display: none !important">
                                <p style="text-align: center; margin: 0px 0px 10px 0px; padding: 0px; width: 100%; float: left;">
                                    Subject Wise Position
                                </p>
                                <div class="rp-card-graph-wpr">
                                    <p class="rp-card-graph-txt0">0</p>
                                    <p class="rp-card-graph-txt50">{{x.MySubjectMarkShowItem[0].TermSubj_hlf_mark}}</p>
                                    <p class="rp-card-graph-txt100">{{x.MySubjectMarkShowItem[0].SubjFulmarks}}</p>
                                    <div class="rp-card-graph-tbl-wpr" style="height: {{x.MySubjectMarkShowItem[0].GraphHeight}}">
                                        <div class="rp-card-graph-tbl-inr" data-ng-repeat="item in x.MyGraphDetailData track by $index" style="width: {{item.Grph_width}}%;">
                                            <div class="rp-card-graph-tbl-nobg" style="height: {{item.BlankHeight}}%"></div>
                                            <div class="rp-card-graph-tbl" style="background: {{item.bg_color}}; height: {{item.final_perc}}%"></div>
                                            <p>{{item.Subject_name}}</p>
                                        </div>
                                    </div>
                                </div>
                            </div>




                            <div class="techer-sec">
                                <table>
                                    <tr>
                                        <th colspan="2">CLASS TEACHER'S REMARKS</th>
                                    </tr>
                                    <tr>
                                        <td style="width: 120px;">ANNUAL</td>
                                        <td><span class="{{x.MyRpTotalCalculationItem[0].P_remark}}">{{x.MyRpTotalCalculationItem[0].P_remark}}</span></td>
                                    </tr>
                                    <%--<tr>
                                        <td colspan="2">NEXT ACADEMIC SESSION COMMENCES ON <span style="text-transform: uppercase;">{{x.MyRpTotalCalculationItem[0].Session_start_from}}</span></td>
                                    </tr>--%>
                                </table>

                                <%--<p class="issue-datepp">Issue Date : {{x.MyRpTotalCalculationItem[0].Issue_date}}</p>--%>
                            </div>


                            <%--<div class="instruction-dv-fr-auto-bottom">
                                <div class="instruction-50 instruction-50-pr">
                                    <div class="instruction-tbls">
                                        <table>
                                            <tr>
                                                <th colspan="2" class="txt-center">INSTRUCTIONS</th>
                                            </tr>
                                            <tr>
                                                <td colspan="2" class="txt-center">(Scholastic and Co-Scholastic Areas {{x.MyRpMarksRangeItem[0].RowCount}}-point grading scale)</td>
                                            </tr>
                                            <tr>
                                                <td>MARKS RANGE</td>
                                                <td class="{{x.MySubjectMarkShowItem[0].Is_text_center}}">GRADE</td>
                                            </tr>
                                            <tr data-ng-repeat="itemxx in x.MyRpMarksRangeItem track by $index">
                                                <td>{{itemxx.Lower_Range}}-{{itemxx.Upper_Range}}</td>
                                                <td class="{{x.MySubjectMarkShowItem[0].Is_text_center}}">{{itemxx.Grade}}</td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <div class="instruction-50 instruction-50-pl">
                                    <div class="instruction-tbls floatrght">
                                        <div class="blank-boxs" style="margin-top: {{reportCardSubS[0].Ins1_area_margn}}">
                                            <p class="backgrounds txtcenter">Result</p>
                                        </div>  
                                    </div>
                                </div>
                            </div>--%>


                            <div class="grding-scale-dv-sec">
                                <p class="grding-scale-dv-p">{{x.MyRpMarksRangeItem[0].RowCount}} Point Grading Scale : <span data-ng-repeat="itemxx in x.MyRpMarksRangeItem track by $index">{{itemxx.Grade}} [{{itemxx.Lower_Range}}-{{itemxx.Upper_Range}}]</span></p>
                            </div>

                            <div class="sig-dv v-false {{x.MySubjectMarkShowItem[0].Sign_bottom}}">
                                <div class="sig-left">
                                    <div class="lft-sig-img-dv">
                                        <img class="lft-sig-img {{x.Sign1}}" src="{{x.Sign1}}" />
                                    </div>
                                    <p class="sig-ps">{{x.SignName1}}</p>
                                </div>
                                <div class="sig-left">
                                    <div class="cntr-sig-img-dv">
                                        <%--<img src="{{x.Sign2}}" class="cntr-sig-img {{x.Sign2}}" />--%>
                                    </div>
                                    <%--<p class="sig-ps">{{x.SignName2}}</p>--%>
                                </div>

                                <div class="sig-left">
                                    <div class="rght-sig-img-dv">
                                        <img src="{{x.Sign3}}" class="rght-sig-img {{x.Sign3}}" />
                                    </div>
                                    <p class="sig-ps">{{x.SignName3}}</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <asp:HiddenField ID="hd_adm_no" runat="server" />
        <asp:HiddenField ID="hd_term1" runat="server" />
        <asp:HiddenField ID="hd_term2" runat="server" />
        <asp:HiddenField ID="hd_term3" runat="server" />
        <asp:HiddenField ID="hd_section" runat="server" />
        <asp:HiddenField ID="hd_session_id" runat="server" />
        <asp:HiddenField ID="hd_class_id" runat="server" />
        <asp:HiddenField ID="hd_branch_id" runat="server" />
        <asp:HiddenField ID="hd_user_type" runat="server" />

        <script type="text/javascript">
            var app = angular.module('RpCardApp', []);
            app.controller('RpCardAppCtrl', function ($scope, $http) {
                var adm_no = $("#<%=hd_adm_no.ClientID%>").val();
                var session_id = $("#<%=hd_session_id.ClientID%>").val();
                var class_id = $("#<%=hd_class_id.ClientID%>").val();
                var section = $("#<%=hd_section.ClientID%>").val();
                var branch_id = $("#<%=hd_branch_id.ClientID%>").val();
                var term_id1 = $("#<%=hd_term1.ClientID%>").val();
                var term_id2 = $("#<%=hd_term2.ClientID%>").val();
                var term_id3 = $("#<%=hd_term3.ClientID%>").val();
                var userType = $("#<%=hd_user_type.ClientID%>").val();


                messge("Please Wait...");
                $("#intsLoader").removeClass("hidden");
                $http.get("api/final-three-term.asmx/fetch_rp_card_bulks", { params: { "Session_id": session_id, "Class_id": class_id, "Section": section, "Branch_id": branch_id, "Term_idI": term_id1, "Term_idII": term_id2, "Term_idIII": term_id3, "UserType": userType, "Adm_no": adm_no } }).then(function (response) {
                    $scope.reportCardS = response.data;
                    $("#intsLoader").addClass("hidden");
                }).catch(function (error) {
                    // Handle the error here
                    console.error("Error fetching report card data:", error);
                    alert("An error occurred while fetching the report card data. Please try again later.");
                    $("#intsLoader").addClass("hidden"); // Hide the loader even if there's an error
                });
            });


            function messge(msg) {
                $("#<%=lblmessage.ClientID%>").text(msg);
                $('.ints-loader-wpr').hide().slideDown(0);
            }

             //document.addEventListener('contextmenu', event => event.preventDefault());
        </script>
    </form>
</body>
</html>
