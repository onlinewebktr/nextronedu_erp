<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bulk-final-report-card.aspx.cs" Inherits="school_web.Examination_Admin.slip.stmarry.bulk_final_report_card" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Report Card</title>
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
                        <img src="{{x.MyFirmDetailData[0].Watermar_image}}" class="wtr-mrk-img v-false {{x.MySubjectMarkShowItem[0].Is_watermark_show}}" />
                        <div class="report-card-wpr" style="height: {{x.MySubjectMarkShowItem[0].Height_dv}}">
                            <div class="report-card-head">
                                <div class="report-card-left-dv">
                                    <img src="{{x.MyFirmDetailData[0].Frim_logo}}" />
                                    <asp:Label ID="lbl_estd" runat="server" Text="{{x.MyFirmDetailData[0].Estd}}" class="estdTextP v-false {{x.MySubjectMarkShowItem[0].Is_estd_show}}"></asp:Label>
                                </div>
                                <div class="report-card-cntr-dv">
                                    <asp:Label ID="lbl_school_name" class="report-card-schlname" runat="server" Text="{{x.MyFirmDetailData[0].Firm_name}}"></asp:Label>
                                    <asp:Label ID="lbl_aff_text" class="report-card-schl-affno-by" runat="server" Text="{{x.MyFirmDetailData[0].Affiliated_by_full_text}}"></asp:Label>
                                    <asp:Label ID="lbl_address" runat="server" class="report-card-schl-add" Text="{{x.MyFirmDetailData[0].Firm_address}}"></asp:Label>
                                    <asp:Label ID="lbl_contact_no" runat="server" class="report-card-schl-cont v-false {{x.MySubjectMarkShowItem[0].Is_contact_no_show}}" Text="{{x.MyFirmDetailData[0].Firm_contact_no}}"></asp:Label>
                                    <p class="report-card-schl-emil v-false {{x.MySubjectMarkShowItem[0].Is_email_show}}">
                                        Email : 
                                        <asp:Label ID="lbl_email" runat="server" Text="{{x.MyFirmDetailData[0].Firm_email}}"></asp:Label>
                                    </p>

                                    <p class="report-card-schl-emil v-false {{x.MySubjectMarkShowItem[0].Is_website_show}}">
                                        Website : 
                                        <asp:Label ID="lbl_website" runat="server" Text="{{x.MyFirmDetailData[0].Website}}"></asp:Label>
                                    </p>
                                    <h2 class="report-card-ac-sson">ACADEMIC SESSION:
                                        <asp:Label ID="lbl_sessions" runat="server" Text="{{x.Session}}"></asp:Label></h2>
                                    <h2 class="report-card-rprt-crd">FINAL ASSESSMENT REPORT CARD  <span class="{{x.MySubjectMarkShowItem[0].Class_in_new_line}}"><span class="v-false {{x.MySubjectMarkShowItem[0].Is_class_text_show}}">CLASS</span> {{x.For_class}}</span></h2>
                                </div>
                                <asp:Label ID="lbl_aff_no" class="report-card-schl-affno {{x.MyFirmDetailData[0].Frim_aff_no}}" runat="server" Text="{{x.MySubjectMarkShowItem[0].Aff_text}} : {{x.MyFirmDetailData[0].Frim_aff_no}}"></asp:Label>
                                <div class="report-card-rght-dv {{x.Student_image}}">
                                    <img src="{{x.Student_image}}" class="{{x.MySubjectMarkShowItem[0].Is_std_img_hide}} {{x.Student_image}}" />
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
                                    <p class="stds-info-p  {{x.MySubjectMarkShowItem[0].Father_name1}}">
                                        <i class="stds-info-p-i">FATHER'S NAME</i>  <i class="stds-info-p-doti">:</i>
                                        <asp:Label ID="lbl_father_name" runat="server" Text="{{x.Father_name}}"></asp:Label>
                                    </p>
                                </div>
                                <div class="{{x.MySubjectMarkShowItem[0].duesclassdesabled2}}"></div>
                                <div class="report-card-std-info-dv-50">
                                    <p class="stds-info-p  {{x.MySubjectMarkShowItem[0].Father_name2}}">
                                        <i class="stds-info-p-i">FATHER'S NAME</i>  <i class="stds-info-p-doti">:</i>
                                        <asp:Label ID="lbl_father_name2" runat="server" Text="{{x.Father_name}}"></asp:Label>
                                    </p>

                                    <p class="stds-info-p {{x.MySubjectMarkShowItem[0].Ranksdv}}">
                                        <i class="stds-info-p-i">Rank</i>  <i class="stds-info-p-doti">:</i>
                                        <asp:Label ID="lbl_rank" runat="server">{{x.MyRpRanksItem[3].Rank}}</asp:Label>
                                    </p>


                                    <p class="stds-info-p">
                                        <i class="stds-info-p-i">ADMISSION NUMBER</i>  <i class="stds-info-p-doti">:</i>
                                        <asp:Label ID="lbl_admission_no" runat="server" Text="{{x.Admission_no}}"></asp:Label>
                                    </p>
                                    <p class="stds-info-p {{x.MySubjectMarkShowItem[0].Is_std_section_hide}}">
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
                                        <th>A</th>
                                        <th colspan="9">SCHOLASTIC AREA</th>
                                    </tr>
                                    <tr>
                                        <th>SN</th>
                                        <th style="width: 175px;">SUBJECTS</th>
                                        <th class="{{x.MySubjectMarkShowItem[0].If_is_grade_ttl_mark_hide}}  {{x.MySubjectMarkShowItem[0].Is_text_center}}">{{x.MySubjectHeading[0].Term_Name}} ({{x.MyRpTermNumberItem[0].Term_Maximum_Marks}})</th>
                                        <th class="{{x.MySubjectMarkShowItem[0].If_is_grade_ttl_mark_hide}} {{x.MySubjectMarkShowItem[0].Is_text_center}}">{{x.MySubjectHeading[1].Term_Name}} ({{x.MyRpTermNumberItem[1].Term_Maximum_Marks}})</th>
                                        <th class="{{x.MySubjectMarkShowItem[0].If_is_grade_ttl_mark_hide}}  {{x.MySubjectMarkShowItem[0].Is_text_center}}">{{x.MySubjectHeading[2].Term_Name}} ({{x.MyRpTermNumberItem[2].Term_Maximum_Marks}})</th>
                                        <th class="{{x.MySubjectMarkShowItem[0].Overall_av_marks}} {{x.MySubjectMarkShowItem[0].Is_text_center}}">TOTAL MARKS ({{x.MySubjectHeading[0].Total_Marks}})</th>
                                        <th class="{{x.MySubjectMarkShowItem[0].Overall_av_marks}} {{x.MySubjectMarkShowItem[0].Is_text_center}}">PERCENTAGE</th>
                                        <th class=" {{x.MySubjectMarkShowItem[0].Is_text_center}}">HIGHEST MARK</th>
                                    </tr>

                                    <tr data-ng-repeat="item in x.MySubjectMarkShowItem track by $index">
                                        <td>{{$index+1}}</td>
                                        <td>{{item.Subject_name}}</td>
                                        <td class="{{x.MySubjectMarkShowItem[0].If_is_grade_ttl_mark_hide}} {{x.MySubjectMarkShowItem[0].Is_text_center}}"><span class="{{item.IsPassI}}">{{item.Total_mark_of_a_subject_for_termI}}</span></td>
                                        <td class="{{x.MySubjectMarkShowItem[0].If_is_grade_ttl_mark_hide}} {{x.MySubjectMarkShowItem[0].Is_text_center}}"><span class="{{item.IsPassII}}">{{item.Total_mark_of_a_subject_for_termII}}</span></td>
                                        <td class="{{x.MySubjectMarkShowItem[0].If_is_grade_ttl_mark_hide}} {{x.MySubjectMarkShowItem[0].Is_text_center}}"><span class="{{item.IsPassIII}}">{{item.Total_mark_of_a_subject_for_termIII}}</span></td>
                                        <td class="{{x.MySubjectMarkShowItem[0].Overall_av_marks}} {{x.MySubjectMarkShowItem[0].Is_text_center}}">{{item.Total_marks_of_a_subject}}</td>
                                        <td class="{{x.MySubjectMarkShowItem[0].Overall_av_marks}} {{x.MySubjectMarkShowItem[0].Is_text_center}}">{{item.termI_termIi_average_percent}}</td>
                                        <td class=" {{x.MySubjectMarkShowItem[0].Is_text_center}}">{{item.Subj_highest_mark}}</td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="txtrght font-weight600">TOTAL</td>
                                        <td class=" {{x.MySubjectMarkShowItem[0].Is_text_center}} font-weight600">{{x.MyRpTotalCalculationItem[0].Obtained_Mark_TermI}}</td>
                                        <td class=" {{x.MySubjectMarkShowItem[0].Is_text_center}} font-weight600">{{x.MyRpTotalCalculationItem[0].Obtained_Mark_TermII}}</td>
                                        <td class=" {{x.MySubjectMarkShowItem[0].Is_text_center}} font-weight600">{{x.MyRpTotalCalculationItem[0].Obtained_Mark_TermIII}}</td>
                                        <td colspan="4"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="txtrght font-weight600">TOTAL PERCENT</td>
                                        <td class=" {{x.MySubjectMarkShowItem[0].Is_text_center}} font-weight600">{{x.MyRpTotalCalculationItem[0].TermI_perc}}</td>
                                        <td class=" {{x.MySubjectMarkShowItem[0].Is_text_center}} font-weight600">{{x.MyRpTotalCalculationItem[0].TermII_perc}}</td>
                                        <td class=" {{x.MySubjectMarkShowItem[0].Is_text_center}} font-weight600">{{x.MyRpTotalCalculationItem[0].TermIII_perc}}</td>
                                        <td colspan="4"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="txtrght font-weight600">RANK</td>
                                        <td class=" {{x.MySubjectMarkShowItem[0].Is_text_center}} font-weight600">{{x.MyRpRanksItem[0].Rank}}</td>
                                        <td class=" {{x.MySubjectMarkShowItem[0].Is_text_center}} font-weight600">{{x.MyRpRanksItem[1].Rank}}</td>
                                        <td class=" {{x.MySubjectMarkShowItem[0].Is_text_center}} font-weight600">{{x.MyRpRanksItem[2].Rank}}</td>
                                        <td colspan="4"></td>
                                    </tr>
                                </table>
                            </div>

                            <div class="subs-mrks-area-lft-dv">
                                <div class="subs-mrks-area-b-dv" style="margin-top: {{x.MySubjectMarkShowItem[0].Co_sch_area_margn}}">
                                    <table>
                                        <tr>
                                            <th>B</th>
                                            <th colspan="4">CO-SCHOLASTIC AREAS [{{x.MyRpCoScholosticItem[0].RowCount}} - POINT GRADING SCALE]</th>
                                        </tr>
                                        <tr>
                                            <th class="th-bg-rmov">SN</th>
                                            <th class="th-bg-rmov">ACTIVITIES</th>
                                            <th class="th-bg-rmov {{x.MySubjectMarkShowItem[0].Is_text_center}}">{{x.MySubjectHeading[0].Term_Name}}</th>
                                            <th class="th-bg-rmov {{x.MySubjectMarkShowItem[0].Is_text_center}}">{{x.MySubjectHeading[1].Term_Name}}</th>
                                            <th class="th-bg-rmov {{x.MySubjectMarkShowItem[0].Is_text_center}}">{{x.MySubjectHeading[2].Term_Name}}</th>
                                        </tr>
                                        <tr data-ng-repeat="itemx in x.MyRpCoScholosticItem track by $index">
                                            <td>{{$index+1}}</td>
                                            <td>{{itemx.Subject_Name}}</td>
                                            <td class=" {{x.MySubjectMarkShowItem[0].Is_text_center}}">{{itemx.Total_marks_t1}}</td>
                                            <td class=" {{x.MySubjectMarkShowItem[0].Is_text_center}}">{{itemx.Total_marks_t2}}</td>
                                            <td class=" {{x.MySubjectMarkShowItem[0].Is_text_center}}">{{itemx.Total_marks_t3}}</td>
                                        </tr>

                                        <tr>
                                            <th>C</th>
                                            <th colspan="4">DISCIPLINE [{{x.MyRpDescplineItem[0].RowCount}} - POINT GRADING SCALE]</th>
                                        </tr>
                                        <tr>
                                            <th class="th-bg-rmov">SN</th>
                                            <th class="th-bg-rmov">ACTIVITIES</th>
                                            <th class="th-bg-rmov {{x.MySubjectMarkShowItem[0].Is_text_center}}">{{x.MySubjectHeading[0].Term_Name}}</th>
                                            <th class="th-bg-rmov {{x.MySubjectMarkShowItem[0].Is_text_center}}">{{x.MySubjectHeading[1].Term_Name}}</th>
                                            <th class="th-bg-rmov {{x.MySubjectMarkShowItem[0].Is_text_center}}">{{x.MySubjectHeading[2].Term_Name}}</th>
                                        </tr>
                                        <tr data-ng-repeat="itemx in x.MyRpDescplineItem track by $index">
                                            <td>{{$index+1}}</td>
                                            <td>{{itemx.Subject_Name}}</td>
                                            <td class=" {{x.MySubjectMarkShowItem[0].Is_text_center}}">{{itemx.Total_marks_t1}}</td>
                                            <td class=" {{x.MySubjectMarkShowItem[0].Is_text_center}}">{{itemx.Total_marks_t2}}</td>
                                            <td class=" {{x.MySubjectMarkShowItem[0].Is_text_center}}">{{itemx.Total_marks_t3}}</td>
                                        </tr>
                                    </table>
                                </div> 
                            </div>

                            <div class="subs-mrks-area-rght-dv">
                                <div class="subs-mrks-area-ovrall-dv" style="margin-top: {{x.MySubjectMarkShowItem[0].Overall_area_margn}}">
                                    <table>
                                        <tr class="{{x.MySubjectMarkShowItem[0].Overall_obt_mark}}">
                                            <td>TOTAL OBTAINED MARKS</td>
                                            <td>{{x.MyRpTotalCalculationItem[0].Overall_obt_marks}}/{{x.MyRpTotalCalculationItem[0].Overall_full_marks}}</td>
                                        </tr>
                                        <tr>
                                            <td>OVERALL {{x.MyRpTotalCalculationItem[0].Mark_type}}</td>
                                            <td>{{x.MyRpTotalCalculationItem[0].Overall_percents}} <span class="{{x.MySubjectMarkShowItem[0].Overall_obt_mark}}">(%)</span></td>
                                        </tr>
                                        <%--<tr class="{{x.MySubjectMarkShowItem[0].Ranksdv}}">
                                            <td>OVERALL RANK</td>
                                            <td>{{x.MyRpRanksItem[3].Rank}}</td>
                                        </tr>--%>
                                        <tr>
                                            <td>OVERALL RANK</td>
                                            <td>{{x.MyRpRanksItem[3].Rank}}</td>
                                        </tr>
                                    </table>

                                    <div class="attandances-dvs">
                                        <table style="margin: 3px 0px 0px 0px;">
                                            <tr>
                                                <th class="backgrounds">ATTENDANCE</th>
                                                <th class="backgrounds {{reportCardSubS[0].Is_text_center}}">
                                                    <asp:Label ID="termITexts1" runat="server" Text="{{x.MySubjectHeading[0].Term_Name}}"></asp:Label></th>
                                                <th class="backgrounds {{reportCardSubS[0].Is_text_center}}">
                                                    <asp:Label ID="termITexts2" runat="server" Text="{{x.MySubjectHeading[1].Term_Name}}"></asp:Label></th>

                                                <th class="backgrounds {{reportCardSubS[0].Is_text_center}}">
                                                    <asp:Label ID="termITexts3" runat="server" Text="{{x.MySubjectHeading[2].Term_Name}}"></asp:Label></th>
                                                <th class="backgrounds {{reportCardSubS[0].Is_text_center}}">FINAL</th>
                                            </tr>
                                            <tr>
                                                <td><%--TOTAL--%>WORKING DAYS</td>
                                                <td class="{{x.MySubjectMarkShowItem[0].Is_text_center}}">{{x.MySubjectMarkShowItem[0].Total_class_of_term_I}}</td>
                                                <td class="{{x.MySubjectMarkShowItem[0].Is_text_center}}">{{x.MySubjectMarkShowItem[0].Total_class_of_term_II}}</td>
                                                <td class="{{x.MySubjectMarkShowItem[0].Is_text_center}}">{{x.MySubjectMarkShowItem[0].Total_class_of_term_III}}</td>
                                                <td class="{{x.MySubjectMarkShowItem[0].Is_text_center}}">{{x.MySubjectMarkShowItem[0].Final_working_days}}</td>
                                            </tr>

                                            <tr>
                                                <td><%--TOTAL--%>PRESENT DAYS</td>
                                                <td class="{{x.MySubjectMarkShowItem[0].Is_text_center}}">{{x.MySubjectMarkShowItem[0].Total_Present_class_of_term_I}}</td>
                                                <td class="{{x.MySubjectMarkShowItem[0].Is_text_center}}">{{x.MySubjectMarkShowItem[0].Total_Present_class_of_term_II}}</td>
                                                <td class="{{x.MySubjectMarkShowItem[0].Is_text_center}}">{{x.MySubjectMarkShowItem[0].Total_Present_class_of_term_III}}</td>
                                                <td class="{{x.MySubjectMarkShowItem[0].Is_text_center}}">{{x.MySubjectMarkShowItem[0].Final_persent_days}}</td>
                                            </tr>
                                            <tr>
                                                <td>ATTENDANCE PERCENT</td>
                                                <td class="{{x.MySubjectMarkShowItem[0].Is_text_center}}">{{x.MySubjectMarkShowItem[0].Total_Precentage_class_of_term_I}}%</td>
                                                <td class="{{x.MySubjectMarkShowItem[0].Is_text_center}}">{{x.MySubjectMarkShowItem[0].Total_Precentage_class_of_term_II}}%</td>
                                                <td class="{{x.MySubjectMarkShowItem[0].Is_text_center}}">{{x.MySubjectMarkShowItem[0].Total_Precentage_class_of_term_III}}%</td>
                                                <td class="{{x.MySubjectMarkShowItem[0].Is_text_center}}">{{x.MySubjectMarkShowItem[0].Final_percent_days}}%</td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>

                                <div class="qr-dvs v-false {{x.MySubjectMarkShowItem[0].qr_div_true}}" style="display: none !important">
                                    <img src="{{x.MySubjectMarkShowItem[0].qr_code_Show}}" />
                                </div>
                                 <div class="prcntage-remrk-dv {{x.MyRpTotalCalculationItem[0].Comentory_remarks}}">
                                    <p>Remark :</p>
                                    <span>{{x.MyRpTotalCalculationItem[0].Comentory_remarks}}</span>
                                </div>
                                <%--<div class="remarks-rp  {{x.MySubjectMarkShowItem[0].Remarkss}}">
                                    <p>REMARKS : <span class="{{x.MySubjectMarkShowItem[0].Remarkss}}">{{x.MySubjectMarkShowItem[0].Remarkss}}</span></p>
                                </div>--%>

                                <%--<div class="promot-dv  {{x.MyRpPromotToItem[0].ShowStatuS}}">
                                    <p><span>Promoted to - </span>Session : {{x.MyRpPromotToItem[0].Session}}, Class :  {{x.MyRpPromotToItem[0].Class_name}}, Section :  {{x.MyRpPromotToItem[0].Section}}</p>
                                </div>--%>
                            </div>


                            <div class="{{x.MySubjectMarkShowItem[0].duesclassdesabled}}">{{x.MySubjectMarkShowItem[0].Desabletext}}</div>
                            <div class="instruction-dv-fr-auto-bottom">
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
                                        <%--<div class="v-false {{x.MySubjectMarkShowItem[0].Instruction2}}" style="margin-top: {{x.MySubjectMarkShowItem[0].Ins2_area_margn}}">
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
                                            </table>
                                        </div>--%>
                                        <%--<div class="v-false {{x.MySubjectMarkShowItem[0].Ranksdv}}" style="margin-top: {{x.MySubjectMarkShowItem[0].Toppers_area_margn}}">
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
                                        </div>--%>
                                    </div>
                                </div>
                            </div>

                            <div class="sig-dv v-false {{x.MySubjectMarkShowItem[0].Sign_bottom}}">
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
                $http.get("api/bulk-final-report-card.asmx/fetch_rp_card_bulks", { params: { "Session_id": session_id, "Class_id": class_id, "Section": section, "Branch_id": branch_id, "Term_idI": term_id1, "Term_idII": term_id2, "Term_idIII": term_id3, "UserType": userType } }).then(function (response) {
                    $scope.reportCardS = response.data;
                    $("#intsLoader").addClass("hidden");
                })
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
