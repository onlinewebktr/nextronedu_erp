<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bulk-final-report-card.aspx.cs" Inherits="school_web.Examination_Admin.slip.final.bulk_final_report_card" %>

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
                    <div class="invoice-wpr" style="border-color: background: {{x.MySubjectMarkShowItem[0].ThemeColor}}">
                        <img src="{{x.MyFirmDetailData[0].Watermar_image}}" class="wtr-mrk-img v-false {{x.MySubjectMarkShowItem[0].Is_watermark_show}}" />
                        <div class="report-card-wpr" style="height: {{x.MySubjectMarkShowItem[0].Height_dv}};">
                            <div class="report-card-head {{x.MyFirmDetailData[0].Content_header}}">
                                <div class="report-card-left-dv">
                                    <img src="{{x.MyFirmDetailData[0].Frim_logo}}" />
                                    <asp:Label ID="lbl_estd" runat="server" Text="{{x.MyFirmDetailData[0].Estd}}" class="estdTextP v-false {{x.MySubjectMarkShowItem[0].Is_estd_show}}"></asp:Label>
                                </div>
                                <div class="report-card-cntr-dv">
                                    <asp:Label ID="lbl_school_name" class="report-card-schlname schlName" Text="{{x.MyFirmDetailData[0].Firm_name}}" runat="server"></asp:Label>
                                    <asp:Label ID="lbl_aff_text" class="report-card-schl-affno-by" runat="server" Text="{{x.MyFirmDetailData[0].Affiliated_by_full_text}}"></asp:Label>
                                    <asp:Label ID="lbl_address" runat="server" class="report-card-schl-add" Text="{{x.MyFirmDetailData[0].Firm_address}}"></asp:Label>
                                    <asp:Label ID="lbl_contact_no" runat="server" Text="{{x.MyFirmDetailData[0].Firm_contact_no}}" class="report-card-schl-cont v-false {{x.MySubjectMarkShowItem[0].Is_contact_no_show}}"></asp:Label>
                                    <p class="report-card-schl-emil v-false {{x.MySubjectMarkShowItem[0].Is_email_show}}">
                                        Email : 
                                        <asp:Label ID="lbl_email" runat="server" Text="{{x.MyFirmDetailData[0].Firm_email}}"></asp:Label>
                                    </p>

                                    <p class="report-card-schl-emil v-false {{x.MySubjectMarkShowItem[0].Is_website_show}}">
                                        Website : 
                                        <asp:Label ID="lbl_website" runat="server" Text="{{x.MyFirmDetailData[0].Website}}"></asp:Label>
                                    </p>
                                </div>
                                <asp:Label ID="lbl_aff_no" class="report-card-schl-affno" runat="server" Text="{{x.MySubjectMarkShowItem[0].Aff_text}} : {{x.MyFirmDetailData[0].Frim_aff_no}}"></asp:Label>

                            </div>







                            <div class="image-headers {{x.MyFirmDetailData[0].Header_templete}}">
                                <img src="{{x.MyFirmDetailData[0].Header_templete}}" />
                            </div>
                            <div class="hdr-exm-info">
                                <h2 class="hdr-exm-info-name">FINAL REPORT CARD ({{x.Session}})</h2>
                                <%--<p class="hdr-exm-info-session">SESSION : {{x.Session}}</p>--%>
                            </div>
                            <div class="report-card-std-info-dv">
                                <div class="report-card-std-info-dv-50">
                                    <p class="stds-info-p">
                                        <i class="stds-info-p-i">Student's Name</i>  <i class="stds-info-p-doti">:</i>
                                        <asp:Label ID="Label1" runat="server" Text="{{x.Student_name}}"></asp:Label>
                                    </p>
                                    <p class="stds-info-p">
                                        <i class="stds-info-p-i">DATE OF BIRTH</i>  <i class="stds-info-p-doti">:</i>
                                        <asp:Label ID="Label4" runat="server" Text="{{x.Date_of_birth}}"></asp:Label>
                                    </p>
                                    <p class="stds-info-p">
                                        <i class="stds-info-p-i">MOTHER'S NAME</i>  <i class="stds-info-p-doti">:</i>
                                        <asp:Label ID="Label5" runat="server" Text="{{x.Mother_name}}"></asp:Label>
                                    </p>
                                    <p class="stds-info-p {{x.MySubjectMarkShowItemII[0].Father_name1}}">
                                        <i class="stds-info-p-i">FATHER'S NAME</i>  <i class="stds-info-p-doti">:</i>
                                        <asp:Label ID="Label6" runat="server" Text="{{x.Father_name}}"></asp:Label>
                                    </p>
                                </div>

                                <div class="report-card-std-info-dv-50">
                                    <%--<p class="stds-info-p {{x.MySubjectMarkShowItemII[0].Ranksdv}}">
                                            <i class="stds-info-p-i">RANK</i>  <i class="stds-info-p-doti">:</i>
                                            <asp:Label ID="Label7" runat="server" Text="{{x.MyRank}}"></asp:Label>
                                        </p>--%>
                                    <p class="stds-info-p">
                                        <i class="stds-info-p-i">ADMISSION NO.</i>  <i class="stds-info-p-doti">:</i>
                                        <asp:Label ID="Label8" runat="server" Text="{{x.Admission_no}}"></asp:Label>
                                    </p>
                                    <p class="stds-info-p">
                                        <i class="stds-info-p-i">CLASS</i>  <i class="stds-info-p-doti">:</i>
                                        <asp:Label ID="Label9" runat="server" Text="{{x.For_class}}"></asp:Label>
                                    </p>
                                    <p class="stds-info-p {{x.MySubjectMarkShowItemII[0].Is_std_section_hide}}">
                                        <i class="stds-info-p-i">SECTION</i>  <i class="stds-info-p-doti">:</i>
                                        <span>{{x.Section}}
                                                <panel class="{{x.MySubjectMarkShowItemII[0].Ranksdv}}"><%--/ ROLL NO. : {{x.Roll_no}}--%></panel></span>
                                    </p>
                                    <p class="stds-info-p {{x.MySubjectMarkShowItemII[0].Roll_show_new_line}}">
                                        <i class="stds-info-p-i">ROLL NO.</i>  <i class="stds-info-p-doti">:</i>
                                        <asp:Label ID="Label10" runat="server" Text="{{x.Roll_no}}"></asp:Label>
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
                                        <th style="background: background: {{x.MySubjectMarkShowItem[0].ThemeColor}}">A</th>
                                        <th style="background: background: {{x.MySubjectMarkShowItem[0].ThemeColor}}">SCHOLASTIC AREA</th>
                                        <th class="txt-center" colspan="{{x.MySubjectHeading[0].TermColSpan}}" style="background: background: {{x.MySubjectMarkShowItem[0].ThemeColor}}">
                                            <asp:Label ID="termITexts" runat="server"></asp:Label></th>
                                        <th colspan="{{x.MySubjectHeading[0].TermColSpan}}" class="txt-center" style="background: background: {{x.MySubjectMarkShowItem[0].ThemeColor}}">
                                            <asp:Label ID="termIITexts" runat="server"></asp:Label></th>
                                        <th class="txt-center" colspan="2" style="background: background: {{x.MySubjectMarkShowItem[0].ThemeColor}}">OVERALL</th>
                                    </tr>
                                    <tr>
                                        <th style="background: background: {{x.MySubjectMarkShowItem[0].ThemeColor}}">SN</th>
                                        <th style="min-width: 130px; background: background: {{x.MySubjectMarkShowItem[0].ThemeColor}}">SUBJECTS</th>
                                        <th data-ng-repeat="item in x.MySubjectHeading track by $index" class="{{x.MySubjectMarkShowItem[0].Is_text_center}}" style="background: background: {{x.MySubjectMarkShowItem[0].ThemeColor}}">{{item.Short_Name}} ({{item.Maximum_Marks}})</th>
                                        <th style="background: background: {{x.MySubjectMarkShowItem[0].ThemeColor}}" class="{{reportCardSubS[0].If_is_grade_ttl_mark_hide}} {{x.MySubjectMarkShowItem[0].Is_text_center}}">{{x.MySubjectMarkShowItem[0].ttl_marks_head_termI}} ({{x.MySubjectHeading[0].Term_Maximum_Marks}})</th>
                                        <th style="background: background: {{x.MySubjectMarkShowItem[0].ThemeColor}}">{{x.MySubjectMarkShowItem[0].TermI_grade_head}} <span class="{{x.MySubjectMarkShowItem[0].If_is_mrk_hide}}">({{x.MySubjectHeading[0].Term_Maximum_Marks}})</span></th>

                                        <th style="background: background: {{x.MySubjectMarkShowItem[0].ThemeColor}}" data-ng-repeat="item in x.MySubjectHeadingII track by $index" class="{{x.MySubjectMarkShowItem[0].Is_text_center}}">{{item.Short_Name}} ({{item.Maximum_Marks}})</th>
                                        <th style="background: background: {{x.MySubjectMarkShowItem[0].ThemeColor}}" class="{{reportCardSubS[0].If_is_grade_ttl_mark_hide}} {{x.MySubjectMarkShowItem[0].Is_text_center}}">{{x.MySubjectMarkShowItem[0].ttl_marks_head_termII}} ({{x.MySubjectHeadingII[0].Term_Maximum_Marks}})</th>
                                        <th style="background: background: {{x.MySubjectMarkShowItem[0].ThemeColor}}">{{x.MySubjectMarkShowItem[0].TermII_grade_head}} <span class="{{x.MySubjectMarkShowItem[0].If_is_mrk_hide}}">({{x.MySubjectHeadingII[0].Term_Maximum_Marks}})</span></th>


                                        <th style="background: background: {{x.MySubjectMarkShowItem[0].ThemeColor}}" class="{{x.MySubjectMarkShowItem[0].Overall_av_marks}} {{x.MySubjectMarkShowItem[0].Is_text_center}}">TOTAL (A+B) (200)</th>
                                        <th style="background: background: {{x.MySubjectMarkShowItem[0].ThemeColor}}" class="{{x.MySubjectMarkShowItem[0].Is_text_center}}">{{x.MySubjectMarkShowItem[0].Overall_ab_grade}}</th>
                                    </tr>

                                    <tr data-ng-repeat="item in x.MySubjectMarkShowItem track by $index">
                                        <td>{{$index+1}}</td>
                                        <td>{{item.Subject_name}}</td>
                                        <td class="{{x.MySubjectMarkShowItem[0].Is_text_center}}" data-ng-repeat="items in item.MySubjectMarkItem track by $index">{{items.Marks_term_I}}</td>
                                        <td class="{{x.MySubjectMarkShowItem[0].If_is_grade_ttl_mark_hide}} {{x.MySubjectMarkShowItem[0].Is_text_center}}">{{item.Total_mark_of_a_subject_for_termI}}</td>
                                        <td class="{{x.Grade}} {{x.MySubjectMarkShowItem[0].Is_text_center}}">{{item.grade_of_a_subject_for_termI}}</td>


                                        <td class="{{x.MySubjectMarkShowItem[0].Is_text_center}}" data-ng-repeat="items in item.MySubjectMarkItemII track by $index">{{items.Marks_term_II}}</td>
                                        <td class="{{reportCardSubS[0].If_is_grade_ttl_mark_hide}} {{x.MySubjectMarkShowItem[0].Is_text_center}}">{{item.Total_mark_of_a_subject_for_termII}}</td>
                                        <td class="{{item.Grade}} {{x.MySubjectMarkShowItem[0].Is_text_center}}">{{item.grade_of_a_subject_for_termII}}</td>

                                        <td class="{{reportCardSubS[0].Overall_av_marks}} {{x.MySubjectMarkShowItem[0].Is_text_center}}">{{item.termI_termIi_average_percent}}</td>
                                        <td class="{{x.MySubjectMarkShowItem[0].Is_text_center}}">{{item.termI_termII_grade}}</td>
                                    </tr>
                                    <%--<tr>
                                        <td colspan="2" class="txt-center font-weight600">ATTENDANCE</td>
                                        <td colspan="{{reportCardSubS[0].ColspanFive}}" class="txt-center font-weight600">{{reportCardSubS[0].Total_Present_class_of_term_I}} OUT OF {{reportCardSubS[0].Total_class_of_term_I}} DAYS</td>
                                        <td colspan="{{reportCardSubS[0].ColspanFive}}" class="txt-center font-weight600">{{reportCardSubS[0].Total_Present_class_of_term_II}} OUT OF {{reportCardSubS[0].Total_class_of_term_II}} DAYS</td>
                                        <td class="txt-center font-weight600 {{reportCardSubS[0].Overall_av_marks}}">{{reportCardSubS[0].Overall_final_percent}}</td>
                                        <td class="txt-center font-weight600">{{reportCardSubS[0].Overall_final_grade}}</td>
                                    </tr>--%>
                                </table>


                                <div class="ovrll-sec">
                                    <div class="ovrll-ttl-marks-dv">
                                        <p class="ovrll-ttl-marks-cnt-p">Total Marks </p>
                                        <p class="ovrll-ttl-marks-num-p">{{x.MyTotalsDetailsItem[0].Overall_obt_marks}}/{{x.MyTotalsDetailsItem[0].Overall_full_marks}}</p>
                                    </div>
                                    <div class="ovrll-ttl-marks-dv">
                                        <p class="ovrll-ttl-marks-cnt-p">Percentage </p>
                                        <p class="ovrll-ttl-marks-num-p">{{x.MyTotalsDetailsItem[0].Overall_percents}} <span class="{{x.MyTotalsDetailsItem[0].Overall_obt_mark}}">(%)</span></p>
                                    </div>
                                    <div class="ovrll-ttl-marks-dv" style="margin: 0px 0px 0px 0px; float: right;">
                                        <p class="ovrll-ttl-marks-cnt-p">Grade </p>
                                        <p class="ovrll-ttl-marks-num-p">{{x.MyTotalsDetailsItem[0].Grade}}</p>
                                    </div>
                                </div>

                            </div>

                            <div class="subs-mrks-area-lft-dv">
                                <div class="subs-mrks-area-b-dv">
                                    <div class="subs-mrks-area-b-dv">
                                        <table>
                                            <tr>
                                                <th>B</th>
                                                <th colspan="4">CO-SCHOLASTIC AREAS [{{x.MyCoScholesticDetailsItem[0].RowCount}} - POINT GRADING SCALE]</th>
                                            </tr>
                                            <tr>
                                                <th class="th-bg-rmov">SN</th>
                                                <th class="th-bg-rmov">ACTIVITIES</th>
                                                <th class="th-bg-rmov {{x.MySubjectMarkShowItem[0].Is_text_center}}">
                                                    <asp:Label ID="termITexts3" runat="server" Text="Label"></asp:Label></th>
                                                <th class="th-bg-rmov {{x.MySubjectMarkShowItem[0].Is_text_center}}">
                                                    <asp:Label ID="termITexts4" runat="server" Text="Label"></asp:Label></th>
                                            </tr>
                                            <tr data-ng-repeat="item in x.MyCoScholesticDetailsItem track by $index">
                                                <td>{{$index+1}}</td>
                                                <td>{{item.Subject_Name}}</td>
                                                <td class=" {{x.MySubjectMarkShowItem[0].Is_text_center}}">{{item.Total_marks_t1}}</td>
                                                <td class=" {{x.MySubjectMarkShowItem[0].Is_text_center}}">{{item.Total_marks_t2}}</td>
                                            </tr>

                                            <tr>
                                                <th>C</th>
                                                <th colspan="3">DISCIPLINE [{{x.MyDecplineDetailsItem[0].RowCount}} - POINT GRADING SCALE]</th>
                                            </tr>
                                            <tr>
                                                <th class="th-bg-rmov">SN</th>
                                                <th class="th-bg-rmov">ACTIVITIES</th>
                                                <th class="th-bg-rmov {{x.MySubjectMarkShowItem[0].Is_text_center}}">
                                                    <asp:Label ID="termITexts5" runat="server" Text="Label"></asp:Label>
                                                </th>
                                                <th class="th-bg-rmov {{x.MySubjectMarkShowItem[0].Is_text_center}}">
                                                    <asp:Label ID="termITexts6" runat="server" Text="Label"></asp:Label>
                                                </th>
                                            </tr>
                                            <tr data-ng-repeat="item in x.MyDecplineDetailsItem track by $index">
                                                <td>{{$index+1}}</td>
                                                <td>{{item.Activity_name}}</td>
                                                <td class="{{x.MySubjectMarkShowItem[0].Is_text_center}}">{{item.Term_grade1}}</td>
                                                <td class="{{x.MySubjectMarkShowItem[0].Is_text_center}}">{{item.Term_grade2}}</td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <%--<div class="prcntage-remrk-dv v-false {{reportCardSubS[0].Prcnt_remark}}" style="margin-top: {{reportCardSubS[0].Percent_remark_area_margn}}">
                                    <p>Remark :</p>
                                    <span>{{ttlNos[0].P_remark}}</span>
                                </div>--%>
                            </div>

                            <div class="subs-mrks-area-rght-dv">
                                <div class="subs-mrks-area-ovrall-dv">
                                    <%--<table>
                                        <tr class="{{reportCardSubS[0].Overall_obt_mark}}">
                                            <td>TOTAL OBTAINED MARKS</td>
                                            <td class="{{x.MyTotalsDetailsItem[0].Is_text_center}}">{{x.MyTotalsDetailsItem[0].Overall_obt_marks}}/{{x.MyTotalsDetailsItem[0].Overall_full_marks}}</td>
                                        </tr>
                                        <tr>
                                            <td>OVERALL {{x.MyTotalsDetailsItem[0].Mark_type}}</td>
                                            <td class="{{x.MyTotalsDetailsItem[0].Is_text_center}}">{{x.MyTotalsDetailsItem[0].Overall_percents}} <span class="{{x.MyTotalsDetailsItem[0].Overall_obt_mark}}">(%)</span></td>
                                        </tr>
                                        <tr class="{{x.MyTotalsDetailsItem[0].Overall_obt_mark}}">
                                            <td>OVERALL GRADE</td>
                                            <td class="{{x.MyTotalsDetailsItem[0].Is_text_center}}">{{x.MyTotalsDetailsItem[0].Grade}}</td>
                                        </tr>
                                    </table>--%>


                                    <table>
                                        <%--<tr>
                                            <th colspan="5" class="{{x.MySubjectMarkShowItem[0].Is_text_center}}">ATTENDANCE</th>
                                        </tr>--%>
                                        <tr>
                                            <th>ATTENDANCE</th>
                                            <th class="{{x.MySubjectMarkShowItem[0].Is_text_center}}">
                                                <asp:Label ID="termITexts1" runat="server"></asp:Label></th>
                                            <th class="{{x.MySubjectMarkShowItem[0].Is_text_center}}">
                                                <asp:Label ID="termITexts2" runat="server"></asp:Label></th>
                                            <th class="{{x.MySubjectMarkShowItem[0].Is_text_center}}">FINAL</th>
                                        </tr>
                                        <tr>
                                            <td>WORKING DAYS</td>
                                            <td class="{{x.MySubjectMarkShowItem[0].Is_text_center}}">{{x.MySubjectMarkShowItem[0].Total_class_of_term_I}}</td>
                                            <td class="{{x.MySubjectMarkShowItem[0].Is_text_center}}">{{x.MySubjectMarkShowItem[0].Total_class_of_term_II}}</td>
                                            <td class="{{x.MySubjectMarkShowItem[0].Is_text_center}}">{{x.MySubjectMarkShowItem[0].Final_working_days}}</td>
                                        </tr>

                                        <tr>
                                            <td>PRESENT DAYS</td>
                                            <td class="{{x.MySubjectMarkShowItem[0].Is_text_center}}">{{x.MySubjectMarkShowItem[0].Total_Present_class_of_term_I}}</td>
                                            <td class="{{x.MySubjectMarkShowItem[0].Is_text_center}}">{{x.MySubjectMarkShowItem[0].Total_Present_class_of_term_II}}</td>
                                            <td class="{{x.MySubjectMarkShowItem[0].Is_text_center}}">{{x.MySubjectMarkShowItem[0].Final_persent_days}}</td>
                                        </tr>
                                        <tr>
                                            <td>ATTENDANCE PERCENT</td>
                                            <td class="{{x.MySubjectMarkShowItem[0].Is_text_center}}">{{x.MySubjectMarkShowItem[0].Total_Precentage_class_of_term_I}}%</td>
                                            <td class="{{x.MySubjectMarkShowItem[0].Is_text_center}}">{{x.MySubjectMarkShowItem[0].Total_Precentage_class_of_term_II}}%</td>
                                            <td class="{{x.MySubjectMarkShowItem[0].Is_text_center}}">{{x.MySubjectMarkShowItem[0].Final_percent_days}}%</td>
                                        </tr>
                                    </table>

                                    <div class="instruction-tbls" style="margin-top: {{x.MySubjectMarkShowItem[0].Ins1_area_margn}}">
                                        <table>
                                            <tr>
                                                <th style="background: {{x.MySubjectMarkShowItem[0].ThemeColor}}" colspan="2" class="txt-center">GRADING BENCHMARK</th>
                                            </tr>
                                            <%--<tr>
                                                <td colspan="2" class="txt-center">(Scholastic and Co-Scholastic Areas {{x.MyMarkRangeDetailsItem[0].RowCount}}-point grading scale)</td>
                                            </tr>--%>
                                            <tr>
                                                <td class="{{x.MySubjectMarkShowItem[0].Is_text_center}}">MARKS RANGE</td>
                                                <td class="{{x.MySubjectMarkShowItem[0].Is_text_center}}">GRADE</td>
                                            </tr>
                                            <tr data-ng-repeat="item in x.MyMarkRangeDetailsItem track by $index">
                                                <td class="{{x.MySubjectMarkShowItem[0].Is_text_center}}">{{item.Lower_Range}}-{{item.Upper_Range}}</td>
                                                <td class="{{x.MySubjectMarkShowItem[0].Is_text_center}}">{{item.Grade}}</td>
                                            </tr>
                                        </table>
                                    </div>

                                </div>
                                <%--<div class="qr-dvs v-false {{reportCardSubS[0].qr_div_true}}">
                                    <img src="{{reportCardSubS[0].qr_code_Show}}" />
                                </div> 

                                 <div class="remarks-rp  {{reportCardSubS[0].Remarkss}}">
                                    <p>REMARKS : {{reportCardSubS[0].Remarkss}}</p>
                                </div>--%>



                                <%--<div class="rp-card-graph-sec v-false {{reportCardSubS[0].Graph}}" style="margin-top: {{reportCardSubS[0].Graph_area_margn}}">
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
                                </div>--%>
                            </div>


                            <div class="com-record-sec">
                                <p class="com-prom-to-clss-p">Promoted to Class : <span></span></p>
                                <p class="com-prom-to-ropn-on">School Reopen on : <span></span></p>
                                <p class="com-prom-to-clss-teachr">Class Teacher's Remarks : <span><i class="{{x.MySubjectMarkShowItem[0].Remarkss}}" style="font-style: normal">{{x.MySubjectMarkShowItem[0].Remarkss}}</i></span></p>
                            </div>

                            <%--<div class="instruction-dv-fr-auto-bottom">
                                <div class="instruction-50 instruction-50-pr">
                                    <div class="instruction-tbls" style="margin-top: {{x.MySubjectMarkShowItem[0].Ins1_area_margn}}">
                                        <table>
                                            <tr>
                                                <th style="background: {{x.MySubjectMarkShowItem[0].ThemeColor}}" colspan="2" class="txt-center">Grading Benchmark</th>
                                            </tr>
                                            <tr>
                                                <td colspan="2" class="txt-center">(Scholastic and Co-Scholastic Areas {{x.MyMarkRangeDetailsItem[0].RowCount}}-point grading scale)</td>
                                            </tr>
                                            <tr>
                                                <td class="{{x.MySubjectMarkShowItem[0].Is_text_center}}">MARKS RANGE</td>
                                                <td class="{{x.MySubjectMarkShowItem[0].Is_text_center}}">GRADE</td>
                                            </tr>
                                            <tr data-ng-repeat="item in x.MyMarkRangeDetailsItem track by $index">
                                                <td class="{{x.MySubjectMarkShowItem[0].Is_text_center}}">{{item.Lower_Range}}-{{item.Upper_Range}}</td>
                                                <td class="{{x.MySubjectMarkShowItem[0].Is_text_center}}">{{item.Grade}}</td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <div class="instruction-50 instruction-50-pl">



                                    <div class="instruction-tbls floatrght">
                                        <div class="v-false {{reportCardSubS[0].Ranksdv}}" style="margin-top: {{reportCardSubS[0].Toppers_area_margn}}">
                                            <table>
                                                <tr>
                                                    <th colspan="5" class="txt-center" style="background: background: {{x.MySubjectMarkShowItem[0].ThemeColor}}">TOPPERS</th>
                                                </tr>
                                                <tr>
                                                    <th style="background: background: {{x.MySubjectMarkShowItem[0].ThemeColor}}">Rank</th>
                                                    <th style="background: background: {{x.MySubjectMarkShowItem[0].ThemeColor}}">Name</th>
                                                    <th style="background: background: {{x.MySubjectMarkShowItem[0].ThemeColor}}">Marks</th>
                                                    <th style="background: background: {{x.MySubjectMarkShowItem[0].ThemeColor}}">Marks(%)</th>
                                                    <th style="background: background: {{x.MySubjectMarkShowItem[0].ThemeColor}}">G.P.</th>
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
                            </div>--%>

                            <div class="sig-dv v-false {{x.MySubjectMarkShowItem[0].Sign_bottom}}">
                                <div class="sig-left">
                                    <div class="lft-sig-img-dv">
                                        <img class="lft-sig-img {{x.MySignatureDetailsItem[0].Signature_image}}" src="{{x.MySignatureDetailsItem[0].Signature_image}}" />
                                    </div>
                                    <p class="sig-ps">{{x.MySignatureDetailsItem[0].Signature_name}}</p>
                                </div>
                                <div class="sig-left">
                                    <div class="cntr-sig-img-dv">
                                        <img src="{{x.MySignatureDetailsItem[1].Signature_image}}" class="cntr-sig-img {{x.MySignatureDetailsItem[1].Signature_image}}" />
                                    </div>
                                    <p class="sig-ps">{{x.MySignatureDetailsItem[1].Signature_name}}</p>
                                </div>

                                <div class="sig-left">
                                    <div class="rght-sig-img-dv">
                                        <img src="{{x.MySignatureDetailsItem[2].Signature_image}}" class="rght-sig-img {{x.MySignatureDetailsItem[2].Signature_image}}" />
                                    </div>
                                    <p class="sig-ps">{{x.MySignatureDetailsItem[2].Signature_name}}</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <asp:HiddenField ID="hd_adm_no" runat="server" />
        <asp:HiddenField ID="hd_section" runat="server" />
        <asp:HiddenField ID="hd_session_id" runat="server" />
        <asp:HiddenField ID="hd_class_id" runat="server" />
        <asp:HiddenField ID="hd_branch_id" runat="server" />
        <asp:HiddenField ID="hd_term1" runat="server" />
        <asp:HiddenField ID="hd_term2" runat="server" />

        <script type="text/javascript">
            var app = angular.module('RpCardApp', []);
            app.controller('RpCardAppCtrl', function ($scope, $http) {
                var adm_no = $("#<%=hd_adm_no.ClientID%>").val();
                var session_id = $("#<%=hd_session_id.ClientID%>").val();
                var class_id = $("#<%=hd_class_id.ClientID%>").val();
                var section = $("#<%=hd_section.ClientID%>").val();
                var branch_id = $("#<%=hd_branch_id.ClientID%>").val();
                var term_id = $("#<%=hd_term1.ClientID%>").val();
                var term_id2 = $("#<%=hd_term2.ClientID%>").val();

                messge("Please Wait...");
                $("#intsLoader").removeClass("hidden");
                $http.get("api/final-rp-card-bulk.asmx/fetch_rp_card_bulks", { params: { "Session_id": session_id, "Class_id": class_id, "Section": section, "Branch_id": branch_id, "Term_idI": term_id, "Term_idII": term_id2, "Adm_no": adm_no } }).then(function (response) {
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
