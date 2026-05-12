<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bulk-final-report-card.aspx.cs" Inherits="school_web.Examination_Admin.slip.nni.bulk_final_report_card" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Report Card</title>
    <link href="https://fonts.googleapis.com/css?family=Montserrat:100,200,300,400,500,600,700" rel="stylesheet" />
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
                    <div class="invoice-wpr" style="border-color: {{x.MySubjectMarkShowItem[0].ThemeColor}}">
                        <img src="{{x.MyFirmDetailData[0].Watermar_image}}" class="wtr-mrk-img v-false {{x.MySubjectMarkShowItem[0].Is_watermark_show}}" />
                        <div class="report-card-wpr" style="height: {{x.MySubjectMarkShowItem[0].Height_dv}}; border-color: {{x.MySubjectMarkShowItem[0].ThemeColor}};">
                            <div class="report-card-head {{x.MySubjectMarkShowItem[0].Hdr_frst}}">
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
                                    <h2 class="report-card-ac-sson" style="background: {{x.MySubjectMarkShowItem[0].ThemeColor}}">ACADEMIC SESSION:
                                        <asp:Label ID="lbl_sessions" runat="server" Text="{{x.Session}}"></asp:Label></h2>
                                    <h2 class="report-card-rprt-crd">FINAL REPORT CARD  <span class="{{x.MySubjectMarkShowItem[0].Class_in_new_line}}"><span class="v-false {{x.MySubjectMarkShowItem[0].Is_class_text_show}}">CLASS</span> {{x.For_class}}</span></h2>
                                </div>
                                <asp:Label ID="lbl_aff_no" class="report-card-schl-affno" runat="server" Text="{{x.MySubjectMarkShowItem[0].Aff_text}} : {{x.MyFirmDetailData[0].Frim_aff_no}}"></asp:Label>
                                <div class="report-card-rght-dv {{x.Student_image}}">
                                    <img src="{{x.Student_image}}" class="{{x.MySubjectMarkShowItem[0].Is_std_img_hide}} {{x.Student_image}}" />
                                </div>
                            </div>

                            <div class="report-card-head hidden {{x.MySubjectMarkShowItem[0].Hdr_scnd}}">
                                <div class="report-card-left-dv">
                                    <img src="{{x.MyFirmDetailData[0].Frim_logo}}" />
                                    <asp:Label ID="Label1" runat="server" Text="{{x.MyFirmDetailData[0].Estd}}" class="estdTextP v-false {{x.MySubjectMarkShowItem[0].Is_estd_show}}"></asp:Label>
                                </div>
                                <div class="report-card-cntr-dv">
                                    <asp:Label ID="lbl_school_name1" class="report-card-schlname schlName" Text="{{x.MyFirmDetailData[0].Firm_name}}" runat="server"></asp:Label>
                                    <asp:Label ID="lbl_aff_text1" class="report-card-schl-affno-by" Text="{{x.MyFirmDetailData[0].Affiliated_by_full_text}}" runat="server"></asp:Label>
                                    <asp:Label ID="lbl_address1" runat="server" class="report-card-schl-add" Text="{{x.MyFirmDetailData[0].Firm_address}}"></asp:Label>

                                    <asp:Label ID="lbl_aff_no1" class="report-card-schl-affno1" runat="server" Text="{{x.MyFirmDetailData[0].Aff_text1}}"></asp:Label>
                                    <asp:Label ID="lbl_school_code" class="report-card-schl-code1" runat="server" Text="{{x.MyFirmDetailData[0].SchoolCode}}"></asp:Label>


                                    <asp:Label ID="lbl_contact_no1" runat="server" Text="{{x.MyFirmDetailData[0].Firm_contact_no}}" class="report-card-schl-cont1 v-false {{x.MySubjectMarkShowItem[0].Is_contact_no_show}}"></asp:Label>
                                    <p class="report-card-schl-emil1 v-false {{x.MySubjectMarkShowItem[0].Is_email_show}}">
                                        Email : 
                                        <asp:Label ID="lbl_email1" runat="server" Text="{{x.MyFirmDetailData[0].Firm_email}}"></asp:Label>
                                    </p>

                                    <p class="report-card-schl-website1 v-false {{x.MySubjectMarkShowItem[0].Is_website_show}}">
                                        Website : 
                                        <asp:Label ID="lbl_website1" runat="server" Text="{{x.MyFirmDetailData[0].Website}}"></asp:Label>
                                    </p>
                                    <h2 class="report-card-ac-sson" style="background: {{x.MySubjectMarkShowItem[0].ThemeColor}}">ACADEMIC SESSION:
                                        <asp:Label ID="lbl_sessions1" runat="server" Text="{{x.Session}}"></asp:Label></h2>
                                    <h2 class="report-card-rprt-crd">FINAL REPORT CARD  <span class="{{x.MySubjectMarkShowItem[0].Class_in_new_line}}"><span class="v-false {{x.MySubjectMarkShowItem[0].Is_class_text_show}}">CLASS</span> {{x.For_class}}</span></h2>
                                </div>

                                <div class="report-card-rght-dv1">
                                    <img src="{{x.MyFirmDetailData[0].ExtraLogo}}" class="class_extra_logo" />
                                    <asp:Label ID="lbl_aff_year" class="report-card-schl-aff-year {{x.MySubjectMarkShowItem[0].Is_aff_year_show}}" Text="{{x.MyFirmDetailData[0].AffYear}}" runat="server"></asp:Label>

                                    <div class="std_imgsdv">
                                        <img src="{{x.Student_image}}" class="{{x.MySubjectMarkShowItem[0].Is_std_img_hide}} {{x.Student_image}}" />
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
                                    <p class="stds-info-p {{x.MySubjectMarkShowItem[0].Father_name1}}" runat="server" id="FtherDV1">
                                        <i class="stds-info-p-i">FATHER'S NAME</i>  <i class="stds-info-p-doti">:</i>
                                        <asp:Label ID="lbl_father_name" runat="server" Text="{{x.Father_name}}"></asp:Label>
                                    </p>
                                </div>

                                <div class="report-card-std-info-dv-50">
                                    <p class="stds-info-p  {{x.MySubjectMarkShowItem[0].Father_name2}}" runat="server" id="FtherDV2">
                                        <i class="stds-info-p-i">FATHER'S NAME</i>  <i class="stds-info-p-doti">:</i>
                                        <asp:Label ID="lbl_father_name2" runat="server" Text="{{x.Father_name}}"></asp:Label>
                                    </p>

                                    <p class="stds-info-p {{x.MySubjectMarkShowItem[0].Ranksdv}}">
                                        <i class="stds-info-p-i">RANK</i>  <i class="stds-info-p-doti">:</i>
                                        <asp:Label ID="Label3" runat="server" Text="{{x.Rank}}"></asp:Label>
                                    </p>
                                    <p class="stds-info-p">
                                        <i class="stds-info-p-i">ADMISSION NUMBER</i>  <i class="stds-info-p-doti">:</i>
                                        <asp:Label ID="lbl_admission_no" runat="server" Text="{{x.Admission_no}}"></asp:Label>
                                    </p>
                                    <p class="stds-info-p">
                                        <i class="stds-info-p-i">ROLL NO.</i>  <i class="stds-info-p-doti">:</i>
                                        <asp:Label ID="lbl_roll_no" runat="server" Text="{{x.Roll_no}}"></asp:Label>
                                    </p>


                                    <p class="stds-info-p" style="display: none">
                                        <i class="stds-info-p-i">CLASS</i>  <i class="stds-info-p-doti">:</i>
                                        <asp:Label ID="Label2" runat="server" Text="{{x.For_class}}"></asp:Label>
                                    </p>
                                    <p class="stds-info-p {{x.MySubjectMarkShowItem[0].Is_std_section_hide}}">
                                        <i class="stds-info-p-i">SECTION</i>  <i class="stds-info-p-doti">:</i>
                                        <asp:Label ID="lbl_section" runat="server" Text="{{x.Section}}"></asp:Label>
                                    </p>
                                </div>
                            </div>

                            <div class="subs-mrks-area-dv">
                                <table>
                                    <tr>
                                        <th style="background: {{x.MySubjectMarkShowItem[0].ThemeColor}}" class="{{x.MySubjectMarkShowItem[0].Is_text_center}}">A</th>
                                        <th style="background: {{x.MySubjectMarkShowItem[0].ThemeColor}}" colspan="18" class="{{x.MySubjectMarkShowItem[0].Is_text_center}}">SCHOLASTIC AREAS</th>
                                    </tr>
                                    <tr>
                                        <th style="background: {{x.MySubjectMarkShowItem[0].ThemeColor}}" class="{{x.MySubjectMarkShowItem[0].Is_text_center}}">SN</th>
                                        <%--<th class="{{x.MySubjectMarkShowItem[0].Is_text_center}} {{x.MySubjectMarkShowItem[0].Is_subj_code_hide}}">SUBJECT CODE</th>--%>
                                        <th style="background: {{x.MySubjectMarkShowItem[0].ThemeColor}}" class="{{x.MySubjectMarkShowItem[0].Is_text_center}}">SUBJECTS</th>
                                        <th style="background: {{x.MySubjectMarkShowItem[0].ThemeColor}}" class="{{x.MySubjectMarkShowItem[0].Is_text_center}}" data-ng-repeat="item in x.MySubjectHeading track by $index">{{item.Assessment_Name}}</th>


                                        <th style="background: {{x.MySubjectMarkShowItem[0].ThemeColor}}" class="{{x.MySubjectMarkShowItem[0].Is_text_center}}">Grand Total (A+B+C=100)</th>
                                        <th style="background: {{x.MySubjectMarkShowItem[0].ThemeColor}}" class="{{x.MySubjectMarkShowItem[0].Is_text_center}} {{x.MySubjectMarkShowItem[0].Grade}}">GRADE</th>
                                    </tr>
                                    <tr data-ng-repeat="item in x.MySubjectMarkShowItem track by $index">
                                        <td class="{{x.MySubjectMarkShowItem[0].Is_text_center}}">{{$index+1}}</td>
                                        <%--<td class="{{x.MySubjectMarkShowItem[0].Is_text_center}} {{x.MySubjectMarkShowItem[0].Is_subj_code_hide}}">{{x.Subject}}</td>--%>
                                        <td class="{{x.MySubjectMarkShowItem[0].Is_text_center}}">{{item.Subject_name}}</td>
                                        <td class="{{x.MySubjectMarkShowItem[0].Is_text_center}}" data-ng-repeat="items in item.MySubjectMarkItem track by $index">{{items.Marks}}</td>


                                        <td class="{{x.MySubjectMarkShowItem[0].Is_text_center}}">{{item.Total_mark_of_a_subject}}</td>
                                        <td class="{{x.MySubjectMarkShowItem[0].Is_text_center}} {{item.Grade}}">{{item.grade_of_a_subject}}</td>
                                    </tr>
                                </table>
                            </div>

                            <div class="subs-mrks-area-lft-dv">
                                <div class="subs-mrks-area-b-dv" style="margin-top: {{x.MySubjectMarkShowItem[0].Co_sch_area_margn}}">
                                    <table>
                                        <tr>
                                            <th style="background: {{x.MySubjectMarkShowItem[0].ThemeColor}}">B</th>
                                            <th style="background: {{x.MySubjectMarkShowItem[0].ThemeColor}}" colspan="4">CO-SCHOLASTIC AREAS [{{x.MyCoScholesticDetailsItem[0].RowCount}} - POINT GRADING SCALE]</th>
                                        </tr>
                                        <tr>
                                            <th style="background: {{x.MySubjectMarkShowItem[0].ThemeColor}}" class="th-bg-rmov">SN</th>
                                            <th style="background: {{x.MySubjectMarkShowItem[0].ThemeColor}}" class="th-bg-rmov">ACTIVITIES</th>
                                            <th style="background: {{x.MySubjectMarkShowItem[0].ThemeColor}}" class="th-bg-rmov {{x.MySubjectMarkShowItem[0].Is_text_center}}">TERM-I</th>
                                            <th style="background: {{x.MySubjectMarkShowItem[0].ThemeColor}}" class="th-bg-rmov {{x.MySubjectMarkShowItem[0].Is_text_center}}">TERM-II</th>
                                        </tr>
                                        <tr data-ng-repeat="item in x.MyCoScholesticDetailsItem track by $index">
                                            <td>{{$index+1}}</td>
                                            <td>{{item.Subject_Name}}</td>
                                            <td class=" {{x.MySubjectMarkShowItem[0].Is_text_center}}">{{item.Total_marks_t1}}</td>
                                            <td class=" {{x.MySubjectMarkShowItem[0].Is_text_center}}">{{item.Total_marks_t2}}</td>
                                        </tr>

                                        <%--<tr>
                                            <th style="background: {{x.MySubjectMarkShowItem[0].ThemeColor}}">C</th>
                                            <th style="background: {{x.MySubjectMarkShowItem[0].ThemeColor}}" colspan="3">DISCIPLINE [{{x.MyDecplineDetailsItem[0].RowCount}} - POINT GRADING SCALE]</th>
                                        </tr>
                                        <tr>
                                            <th style="background: {{x.MySubjectMarkShowItem[0].ThemeColor}}" class="th-bg-rmov">SN</th>
                                            <th style="background: {{x.MySubjectMarkShowItem[0].ThemeColor}}" class="th-bg-rmov">ACTIVITIES</th>
                                            <th style="background: {{x.MySubjectMarkShowItem[0].ThemeColor}}" class="th-bg-rmov {{x.MySubjectMarkShowItem[0].Is_text_center}}">TERM-I</th>
                                            <th style="background: {{x.MySubjectMarkShowItem[0].ThemeColor}}" class="th-bg-rmov {{x.MySubjectMarkShowItem[0].Is_text_center}}">TERM-II</th>
                                        </tr>
                                        <tr data-ng-repeat="item in x.MyDecplineDetailsItem track by $index">
                                            <td>{{$index+1}}</td>
                                            <td>{{item.Activity_name}}</td>
                                            <td class="{{x.MySubjectMarkShowItem[0].Is_text_center}}">{{item.Term_grade1}}</td>
                                            <td class="{{x.MySubjectMarkShowItem[0].Is_text_center}}">{{item.Term_grade2}}</td>
                                        </tr>--%>
                                    </table>
                                </div>
                                <div class="prcntage-remrk-dv v-false {{x.MySubjectMarkShowItem[0].Prcnt_remark}}" style="margin-top: {{x.MySubjectMarkShowItem[0].Percent_remark_area_margn}}">
                                    <p>Remark :</p>
                                    <span>{{x.MyTotalsDetailsItem[0].P_remark}}</span>
                                </div>
                            </div>

                            <div class="subs-mrks-area-rght-dv">
                                <div class="subs-mrks-area-ovrall-dv" style="margin-top: {{x.MySubjectMarkShowItem[0].Overall_area_margn}}">
                                    <table>
                                        <tr class="{{x.MySubjectMarkShowItem[0].Overall_obt_mark}}">
                                            <td>TOTAL OBTAINED MARKS</td>
                                            <td>{{x.MyTotalsDetailsItem[0].Overall_obt_marks}}/{{x.MyTotalsDetailsItem[0].Overall_full_marks}}</td>
                                        </tr>
                                        <tr>
                                            <td>OVERALL PERCENTAGE</td>
                                            <td>{{x.MyTotalsDetailsItem[0].Overall_percents}} <span class="{{x.MySubjectMarkShowItem[0].Overall_obt_mark}}">(%)</span></td>
                                        </tr>
                                        <tr class="{{x.MySubjectMarkShowItem[0].Overall_obt_mark}} {{x.MySubjectMarkShowItem[0].Is_overall_grade_show}}">
                                            <td>OVERALL GRADE</td>
                                            <td>{{x.MyTotalsDetailsItem[0].Grade}}</td>
                                        </tr>
                                        <%-- <tr class=" {{x.MySubjectMarkShowItem[0].Is_attandance_show}}">
                                            <td>TOTAL WORKING DAYS</td>
                                            <td>{{x.MySubjectMarkShowItem[0].Total_class}}</td>
                                        </tr>
                                        <tr class=" {{x.MySubjectMarkShowItem[0].Is_attandance_show}}">
                                            <td>TOTAL PRESENT DAYS</td>
                                            <td>{{x.MySubjectMarkShowItem[0].Total_Present_class}}</td>
                                        </tr>
                                        <tr class=" {{x.MySubjectMarkShowItem[0].Is_attandance_show}}">
                                            <td>ATTENDANCE PERCENT</td>
                                            <td>{{x.MySubjectMarkShowItem[0].Total_att_percent}} (%)</td>
                                        </tr> --%>
                                        <%--<tr class=" {{x.MySubjectMarkShowItem[0].SpecialNote}}">
                                            <td class="spcial-notes" colspan="2">SPECIAL NOTES (IF ANY)</td>
                                        </tr>--%>
                                    </table>
                                </div>


                                <div class="qr-dvs v-false {{x.MySubjectMarkShowItem[0].qr_div_true}}">
                                    <img src="{{x.MySubjectMarkShowItem[0].qr_code_Show}}" />
                                </div>

                                <div class="remarks-rp  {{x.MySubjectMarkShowItem[0].Remarkss}}">
                                    <p>REMARKS : {{x.MySubjectMarkShowItem[0].Remarkss}}</p>
                                </div>


                                <div class="rp-card-graph-sec v-false {{x.MySubjectMarkShowItem[0].Graph}}" style="margin-top: {{x.MySubjectMarkShowItem[0].Graph_area_margn}}">
                                    <div class="rp-card-graph-wpr">
                                        <p class="rp-card-graph-txt0">0</p>
                                        <p class="rp-card-graph-txt50">50</p>
                                        <p class="rp-card-graph-txt100">100</p>
                                        <div class="rp-card-graph-tbl-wpr" style="height: {{x.MySubjectMarkShowItem[0].GraphHeight}}">
                                            <div class="rp-card-graph-tbl-inr" data-ng-repeat="x in reportGraphs" style="width: {{x.Grph_width}}%;">
                                                <div class="rp-card-graph-tbl-nobg" style="height: {{x.BlankHeight}}%"></div>
                                                <div class="rp-card-graph-tbl" style="height: {{x.Total_obtained_mark}}%"></div>
                                                <p>{{x.Subject_name}}</p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>



                            <div class="instruction-dv-fr-auto-bottom">
                                <div class="instruction-50 instruction-50-pr">
                                    <div class="instruction-tbls" style="margin-top: {{x.MySubjectMarkShowItem[0].Ins1_area_margn}}">
                                        <table>
                                            <tr>
                                                <th style="background: {{x.MySubjectMarkShowItem[0].ThemeColor}}" colspan="2" class="txt-center">INSTRUCTIONS</th>
                                            </tr>
                                            <tr>
                                                <td colspan="2"  style="background: {{x.MySubjectMarkShowItem[0].ThemeColor}}" class="txt-center">(Scholastic and Co-Scholastic Areas {{x.MyMarkRangeDetailsItem[0].RowCount}}-point grading scale)</td>
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
                                    <div class="subs-mrks-area-ovrall-dv" style="padding: 2px 0px 0px 0px;">
                                        <table>
                                            <tr>
                                                <th colspan="4" style="background: {{x.MySubjectMarkShowItem[0].ThemeColor}}" class="txt-center">ATTENDANCE</th>
                                            </tr>
                                            <tr>
                                                <th style="background: {{reportCardSubS[0].ThemeColor}}">EXAM</th>
                                                <th class="txt-center" style="background: {{reportCardSubS[0].ThemeColor}}">WORKING DAYS</th>
                                                <th class="txt-center" style="background: {{reportCardSubS[0].ThemeColor}}">PRESENT DAYS</th>
                                                <th class="txt-center" style="background: {{reportCardSubS[0].ThemeColor}}">ATTENDANCE(%)</th>
                                            </tr>
                                            <tr  data-ng-repeat="item in x.Fetch_Details_of_attendance_Item track by $index" class="{{x.Attendance_class_style}}">
                                                <td>{{item.Exam_name}}</td>
                                                <td class="txt-center">{{item.Total_working_days}}</td>
                                                <td class="txt-center">{{item.Total_persent_days}}</td>
                                                <td class="txt-center">{{item.Attendance_percentage}}</td>
                                            </tr>
                                        </table>
                                    </div>
                                    <%--<div class="instruction-tbls floatrght">
                                        <div class="v-false {{x.MySubjectMarkShowItem[0].Instruction2}}" style="margin-top: {{x.MySubjectMarkShowItem[0].Ins2_area_margn}}">
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
                                        <div class="v-false {{x.MySubjectMarkShowItem[0].Ranksdv}}" style="margin-top: {{x.MySubjectMarkShowItem[0].Toppers_area_margn}}">
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
                                    </div>--%>
                                </div>
                            </div>

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
        <asp:HiddenField ID="hd_section" runat="server" />
        <asp:HiddenField ID="hd_session_id" runat="server" />
        <asp:HiddenField ID="hd_class_id" runat="server" />
        <asp:HiddenField ID="hd_branch_id" runat="server" />
        <asp:HiddenField ID="hd_term1" runat="server" />
        <asp:HiddenField ID="hd_term2" runat="server" />

        <script type="text/javascript">
            var app = angular.module('RpCardApp', []);
            app.controller('RpCardAppCtrl', function ($scope, $http) {
                var section = $("#<%=hd_section.ClientID%>").val();
                var session_id = $("#<%=hd_session_id.ClientID%>").val();
                var class_id = $("#<%=hd_class_id.ClientID%>").val();
                var branch_id = $("#<%=hd_branch_id.ClientID%>").val();
                var term_idI = $("#<%=hd_term1.ClientID%>").val();
                var term_idII = $("#<%=hd_term2.ClientID%>").val();

                messge("Please Wait...");
                $("#intsLoader").removeClass("hidden");

                $http.get("api/bulk-final-report-card.asmx/fetch_rp_card_bulks", { params: { "Session_id": session_id, "Class_id": class_id, "Section": section, "Branch_id": branch_id, "Term_idI": term_idI, "Term_idII": term_idII } }).then(function (response) {
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
