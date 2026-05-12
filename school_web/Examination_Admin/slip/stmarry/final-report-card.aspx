<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="final-report-card.aspx.cs" Inherits="school_web.Examination_Admin.slip.stmarry.final_report_card" %>

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
                        <div class="noPrint {{reportCardSubS[0].PrintBTN}}" style="float: right">
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
                    <div class="invoice-wpr">
                        <asp:Image ID="img_watermark" runat="server" class="wtr-mrk-img {{reportCardSubS[0].Is_watermark_show}}" />
                        <div class="report-card-wpr" style="height: {{reportCardSubS[0].Height_dv}}">
                            <div class="report-card-head {{reportCardSubS[0].Hdr_frst}}">
                                <div class="report-card-left-dv">
                                    <asp:Image ID="Image1" runat="server" />
                                    <asp:Label ID="lbl_estd" runat="server" class="estdTextP v-false {{reportCardSubS[0].Is_estd_show}}"></asp:Label>
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
                                    <h2 class="report-card-ac-sson">ACADEMIC SESSION:
                                        <asp:Label ID="lbl_sessions" runat="server"></asp:Label></h2>
                                    <h2 class="report-card-rprt-crd">
                                        <asp:Label ID="lbl_for_term" runat="server"></asp:Label>
                                        REPORT CARD  <span class="{{reportCardSubS[0].Class_in_new_line}}"><span class="v-false {{reportCardSubS[0].Is_class_text_show}}">CLASS</span>
                                            <asp:Label ID="lbl_for_class" runat="server"></asp:Label></span></h2>
                                </div>
                                <asp:Label ID="lbl_aff_no" class="report-card-schl-affno" runat="server"></asp:Label>
                                <div class="report-card-rght-dv">
                                    <asp:Image ID="img_student_img" runat="server" class="{{reportCardSubS[0].Is_std_img_hide}}" />
                                </div>
                            </div>
                            <div class="{{reportCardSubS[0].duesclassdesabled2}}"></div>
                            <div class="report-card-head hidden {{reportCardSubS[0].Hdr_scnd}}">
                                <div class="report-card-left-dv">
                                    <asp:Image ID="Image2" runat="server" />
                                    <asp:Label ID="lbl_estd1" runat="server" class="estdTextP v-false {{reportCardSubS[0].Is_estd_show}}"></asp:Label>
                                </div>
                                <div class="report-card-cntr-dv">
                                    <asp:Label ID="lbl_school_name1" class="report-card-schlname" runat="server"></asp:Label>
                                    <asp:Label ID="lbl_aff_text1" class="report-card-schl-affno-by" runat="server"></asp:Label>
                                    <asp:Label ID="lbl_address1" runat="server" class="report-card-schl-add"></asp:Label>

                                    <asp:Label ID="lbl_aff_no1" class="report-card-schl-affno1" runat="server"></asp:Label>
                                    <asp:Label ID="lbl_school_code" class="report-card-schl-code1" runat="server"></asp:Label>


                                    <asp:Label ID="lbl_contact_no1" runat="server" Text="" class="report-card-schl-cont1 v-false {{reportCardSubS[0].Is_contact_no_show}}"></asp:Label>
                                    <p class="report-card-schl-emil1 v-false {{reportCardSubS[0].Is_email_show}}">
                                        Email : 
                                        <asp:Label ID="lbl_email1" runat="server"></asp:Label>
                                    </p>

                                    <p class="report-card-schl-website1 v-false {{reportCardSubS[0].Is_website_show}}">
                                        Website : 
                                        <asp:Label ID="lbl_website1" runat="server"></asp:Label>
                                    </p>
                                    <h2 class="report-card-ac-sson">ACADEMIC SESSION:
                                        <asp:Label ID="lbl_sessions1" runat="server"></asp:Label></h2>
                                    <h2 class="report-card-rprt-crd">
                                        <asp:Label ID="lbl_for_term1" runat="server"></asp:Label>
                                        REPORT CARD  <span class="{{reportCardSubS[0].Class_in_new_line}}"><span class="v-false {{reportCardSubS[0].Is_class_text_show}}">CLASS</span>
                                            <asp:Label ID="lbl_for_class1" runat="server"></asp:Label></span>
                                    </h2>
                                </div>

                                <div class="report-card-rght-dv1">
                                    <asp:Image ID="img_extra_log" runat="server" class="class_extra_logo" />

                                    <asp:Label ID="lbl_aff_year" class="report-card-schl-aff-year {{reportCardSubS[0].Is_aff_year_show}}" runat="server"></asp:Label>

                                    <div class="std_imgsdv">
                                        <asp:Image ID="img_student_img1" runat="server" class="{{reportCardSubS[0].Is_std_img_hide}}" />
                                    </div>
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
                                    <p class="stds-info-p">
                                        <i class="stds-info-p-i">MOTHER'S NAME</i>  <i class="stds-info-p-doti">:</i>
                                        <asp:Label ID="lbl_mother_name" runat="server" Text="JELI BIBI"></asp:Label>
                                    </p>
                                    <p class="stds-info-p {{reportCardSubS[0].Father_name1}}" runat="server" id="FtherDV1">
                                        <i class="stds-info-p-i">FATHER'S NAME</i>  <i class="stds-info-p-doti">:</i>
                                        <asp:Label ID="lbl_father_name" runat="server" Text="TOFAIL SK."></asp:Label>
                                    </p>
                                </div>

                                <div class="report-card-std-info-dv-50">
                                    <p class="stds-info-p  {{reportCardSubS[0].Father_name2}}" runat="server" id="FtherDV2">
                                        <i class="stds-info-p-i">FATHER'S NAME</i>  <i class="stds-info-p-doti">:</i>
                                        <asp:Label ID="lbl_father_name2" runat="server" Text=""></asp:Label>
                                    </p>

                                    <p class="stds-info-p" runat="server" id="ranksDV" visible="false">
                                        <i class="stds-info-p-i">Rank</i>  <i class="stds-info-p-doti">:</i>
                                        <asp:Label ID="lbl_rank" runat="server">{{ranks[3].Rank}}</asp:Label>
                                    </p>


                                    <p class="stds-info-p">
                                        <i class="stds-info-p-i">ADMISSION NUMBER</i>  <i class="stds-info-p-doti">:</i>
                                        <asp:Label ID="lbl_admission_no" runat="server" Text="5805"></asp:Label>
                                    </p>
                                    <p class="stds-info-p {{reportCardSubS[0].Is_std_section_hide}}">
                                        <i class="stds-info-p-i">SECTION</i>  <i class="stds-info-p-doti">:</i>
                                        <asp:Label ID="lbl_section" runat="server" Text="A"></asp:Label>
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
                                        <th>A</th>
                                        <th colspan="9">SCHOLASTIC AREA</th>
                                        <%--<th class="txt-center">
                                            <asp:Label ID="termITexts" runat="server"></asp:Label>TERM-I</th>
                                        <th colspan="{{reportCardSubS[0].ColspanFive}}" class="txt-center">
                                            <asp:Label ID="termIITexts" runat="server"></asp:Label>TERM-II</th>--%>
                                        <%--<th colspan="{{reportCardSubS[0].ColspanOneTwo}}" class="txt-center">OVERALL</th>--%>
                                    </tr>
                                    <tr>
                                        <th>SN</th>
                                        <th style="width: 175px;">SUBJECTS</th>

                                        <%--<th data-ng-repeat="x in reportCard">{{x.Short_Name}} ({{x.Maximum_Marks}})</th>--%>
                                        <th class="{{reportCardSubS[0].If_is_grade_ttl_mark_hide}}  {{reportCardSubS[0].Is_text_center}}">{{reportCardTerms[0].Term_Name}} ({{reportCard[0].Term_Maximum_Marks}})</th>
                                        <%--<th>{{reportCardSubS[0].TermI_grade_head}} <span class="{{reportCardSubS[0].If_is_mrk_hide}}">({{reportCard[0].Term_Maximum_Marks}})</span></th>--%>


                                        <%--<th data-ng-repeat="x in reportCardTermII">{{x.Short_Name}} ({{x.Maximum_Marks}})</th>--%>
                                        <th class="{{reportCardSubS[0].If_is_grade_ttl_mark_hide}} {{reportCardSubS[0].Is_text_center}}">{{reportCardTerms[1].Term_Name}} ({{reportCardTermII[0].Term_Maximum_Marks}})</th>
                                        <th class="{{reportCardSubS[0].If_is_grade_ttl_mark_hide}}  {{reportCardSubS[0].Is_text_center}}">{{reportCardTerms[2].Term_Name}} ({{reportCardTermIII[0].Term_Maximum_Marks}})</th>

                                        <th class="{{reportCardSubS[0].Overall_av_marks}} {{reportCardSubS[0].Is_text_center}}">TOTAL MARKS ({{reportCardTerms[0].Total_Marks}})</th>

                                        <th class="{{reportCardSubS[0].Overall_av_marks}} {{reportCardSubS[0].Is_text_center}}">PERCENTAGE</th>
                                        <%--<th class=" {{reportCardSubS[0].Is_text_center}}">{{reportCardSubS[0].Overall_ab_grade}}</th>--%>
                                        <th class=" {{reportCardSubS[0].Is_text_center}}">HIGHEST MARK</th>
                                    </tr>

                                    <tr data-ng-repeat="x in reportCardSubS track by $index">
                                        <td>{{$index+1}}</td>
                                        <td>{{x.Subject_name}}</td>
                                        <%--<td data-ng-repeat="item in x.MySubjectMarkItem track by $index">{{item.Marks_term_I}}</td>--%>
                                        <td class="{{reportCardSubS[0].If_is_grade_ttl_mark_hide}} {{reportCardSubS[0].Is_text_center}}"><span class="{{x.IsPassI}}">{{x.Total_mark_of_a_subject_for_termI}}</span></td>
                                        <%--<td class="{{x.Grade}}">{{x.grade_of_a_subject_for_termI}}</td>--%>
                                        <%--<td data-ng-repeat="item in x.MySubjectMarkItem track by $index">{{item.Marks_term_II}}</td>--%>
                                        <td class="{{reportCardSubS[0].If_is_grade_ttl_mark_hide}} {{reportCardSubS[0].Is_text_center}}"><span class="{{x.IsPassII}}">{{x.Total_mark_of_a_subject_for_termII}}</span></td>
                                        <td class="{{reportCardSubS[0].If_is_grade_ttl_mark_hide}} {{reportCardSubS[0].Is_text_center}}"><span class="{{x.IsPassIII}}">{{x.Total_mark_of_a_subject_for_termIII}}</span></td>

                                        <td class="{{reportCardSubS[0].Overall_av_marks}} {{reportCardSubS[0].Is_text_center}}">{{x.Total_marks_of_a_subject}}</td>
                                        <td class="{{reportCardSubS[0].Overall_av_marks}} {{reportCardSubS[0].Is_text_center}}">{{x.termI_termIi_average_percent}}</td>

                                        <%--<td class="{{x.Grade}} {{reportCardSubS[0].Is_text_center}}">{{x.termI_termII_grade}}</td>--%>
                                        <td class=" {{reportCardSubS[0].Is_text_center}}">{{x.Subj_highest_mark}}</td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="txtrght font-weight600">TOTAL</td>
                                        <td class=" {{reportCardSubS[0].Is_text_center}} font-weight600">{{ttlNos[0].Obtained_Mark_TermI}}</td>
                                        <td class=" {{reportCardSubS[0].Is_text_center}} font-weight600">{{ttlNos[0].Obtained_Mark_TermII}}</td>
                                        <td class=" {{reportCardSubS[0].Is_text_center}} font-weight600">{{ttlNos[0].Obtained_Mark_TermIII}}</td>
                                        <td colspan="4"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="txtrght font-weight600">TOTAL PERCENT</td>
                                        <td class=" {{reportCardSubS[0].Is_text_center}} font-weight600">{{ttlNos[0].TermI_perc}}</td>
                                        <td class=" {{reportCardSubS[0].Is_text_center}} font-weight600">{{ttlNos[0].TermII_perc}}</td>
                                        <td class=" {{reportCardSubS[0].Is_text_center}} font-weight600">{{ttlNos[0].TermIII_perc}}</td>
                                        <td colspan="4"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="txtrght font-weight600">RANK</td>
                                        <td class=" {{reportCardSubS[0].Is_text_center}} font-weight600">{{ranks[0].Rank}}</td>
                                        <td class=" {{reportCardSubS[0].Is_text_center}} font-weight600">{{ranks[1].Rank}}</td>
                                        <td class=" {{reportCardSubS[0].Is_text_center}} font-weight600">{{ranks[2].Rank}}</td>
                                        <td colspan="4"></td>
                                    </tr>
                                </table>
                            </div>

                            <div class="subs-mrks-area-lft-dv">
                                <div class="subs-mrks-area-b-dv" style="margin-top: 5px">
                                    <table>
                                        <tr>
                                            <th>B</th>
                                            <th colspan="4">CO-SCHOLASTIC AREAS [{{reportCardTermII_coscholastic_data[0].RowCount}} - POINT GRADING SCALE]</th>
                                        </tr>
                                        <tr>
                                            <th class="th-bg-rmov">SN</th>
                                            <th class="th-bg-rmov">ACTIVITIES</th>
                                            <th class="th-bg-rmov {{reportCardSubS[0].Is_text_center}}">{{reportCardTerms[0].Term_Name}}</th>
                                            <th class="th-bg-rmov {{reportCardSubS[0].Is_text_center}}">{{reportCardTerms[1].Term_Name}}</th>
                                            <th class="th-bg-rmov {{reportCardSubS[0].Is_text_center}}">{{reportCardTerms[2].Term_Name}}</th>
                                        </tr>
                                        <tr data-ng-repeat="x in reportCardTermII_coscholastic_data">
                                            <td>{{$index+1}}</td>
                                            <td>{{x.Subject_Name}}</td>
                                            <td class=" {{reportCardSubS[0].Is_text_center}}">{{x.Total_marks_t1}}</td>
                                            <td class=" {{reportCardSubS[0].Is_text_center}}">{{x.Total_marks_t2}}</td>
                                            <td class=" {{reportCardSubS[0].Is_text_center}}">{{x.Total_marks_t3}}</td>
                                        </tr>
                                        <tr>
                                            <th>C</th>
                                            <th colspan="4">DISCIPLINE [{{reportCardTermII_descipline_data[0].RowCount}} - POINT GRADING SCALE]</th>
                                        </tr>
                                        <tr>
                                            <th class="th-bg-rmov">SN</th>
                                            <th class="th-bg-rmov">ACTIVITIES</th>
                                            <th class="th-bg-rmov {{reportCardSubS[0].Is_text_center}}">{{reportCardTerms[0].Term_Name}}</th>
                                            <th class="th-bg-rmov {{reportCardSubS[0].Is_text_center}}">{{reportCardTerms[1].Term_Name}}</th>
                                            <th class="th-bg-rmov {{reportCardSubS[0].Is_text_center}}">{{reportCardTerms[2].Term_Name}}</th>
                                        </tr>
                                        <tr data-ng-repeat="x in reportCardTermII_descipline_data">
                                            <td>{{$index+1}}</td>
                                            <td>{{x.Subject_name}}</td>
                                            <td class=" {{reportCardSubS[0].Is_text_center}}">{{x.Term_I_grade}}</td>
                                            <td class=" {{reportCardSubS[0].Is_text_center}}">{{x.Term_II_grade}}</td>
                                            <td class=" {{reportCardSubS[0].Is_text_center}}">{{x.Term_III_grade}}</td>
                                        </tr>
                                    </table>
                                </div>

                            </div>

                            <div class="subs-mrks-area-rght-dv">
                                <div class="subs-mrks-area-ovrall-dv" style="margin-top: 5px">
                                    <table>
                                        <tr class="{{reportCardSubS[0].Overall_obt_mark}}">
                                            <td>TOTAL OBTAINED MARKS</td>
                                            <td>{{ttlNos[0].Overall_obt_marks}}/{{ttlNos[0].Overall_full_marks}}</td>
                                        </tr>
                                        <tr>
                                            <td>OVERALL {{ttlNos[0].Mark_type}}</td>
                                            <td>{{ttlNos[0].Overall_percents}} <span class="{{reportCardSubS[0].Overall_obt_mark}}">(%)</span></td>
                                        </tr>
                                        <%--<tr class="{{reportCardSubS[0].Ranksdv}}">
                                            <td>OVERALL RANK</td>
                                            <td>{{ranks[3].Rank}}</td>
                                        </tr>--%>
                                        <tr>
                                            <td>OVERALL RANK</td>
                                            <td>{{ranks[3].Rank}}</td>
                                        </tr>

                                        <%--<tr class="{{reportCardSubS[0].Overall_obt_mark}}">
                                            <td>OVERALL GRADE</td>
                                            <td>{{ttlNos[0].Grade}}</td>
                                        </tr>
                                        <tr class=" {{reportCardSubS[0].Is_attandance_show}}">
                                            <td>TOTAL WORKING DAYS</td> 
                                        </tr>
                                        <tr class=" {{reportCardSubS[0].Is_attandance_show}}">
                                            <td>TOTAL PRESENT DAYS</td> 
                                        </tr>
                                        <tr class=" {{reportCardSubS[0].Is_attandance_show}}">
                                            <td>ATTENDANCE PERCENT</td> 
                                        </tr>
                                         <tr class=" {{reportCardSubS[0].SpecialNote}}">
                                            <td class="spcial-notes" colspan="2"></td>
                                        </tr>--%>
                                    </table>

                                    <div class="attandances-dvs">
                                        <table style="margin: 3px 0px 0px 0px;">
                                            <tr>
                                                <th class="backgrounds">ATTENDANCE</th>
                                                <th class="backgrounds {{reportCardSubS[0].Is_text_center}}">
                                                    <asp:Label ID="termITexts1" runat="server"></asp:Label></th>
                                                <th class="backgrounds {{reportCardSubS[0].Is_text_center}}">
                                                    <asp:Label ID="termITexts2" runat="server"></asp:Label></th>

                                                <th class="backgrounds {{reportCardSubS[0].Is_text_center}}">
                                                    <asp:Label ID="termITexts3" runat="server"></asp:Label></th>
                                                <th class="backgrounds {{reportCardSubS[0].Is_text_center}}">FINAL</th>
                                            </tr>
                                            <tr>
                                                <td><%--TOTAL--%>WORKING DAYS</td>
                                                <td class="{{reportCardSubS[0].Is_text_center}}">{{reportCardSubS[0].Total_class_of_term_I}}</td>
                                                <td class="{{reportCardSubS[0].Is_text_center}}">{{reportCardSubS[0].Total_class_of_term_II}}</td>
                                                <td class="{{reportCardSubS[0].Is_text_center}}">{{reportCardSubS[0].Total_class_of_term_III}}</td>
                                                <td class="{{reportCardSubS[0].Is_text_center}}">{{reportCardSubS[0].Final_working_days}}</td>
                                            </tr>

                                            <tr>
                                                <td><%--TOTAL--%>PRESENT DAYS</td>
                                                <td class="{{reportCardSubS[0].Is_text_center}}">{{reportCardSubS[0].Total_Present_class_of_term_I}}</td>
                                                <td class="{{reportCardSubS[0].Is_text_center}}">{{reportCardSubS[0].Total_Present_class_of_term_II}}</td>
                                                <td class="{{reportCardSubS[0].Is_text_center}}">{{reportCardSubS[0].Total_Present_class_of_term_III}}</td>
                                                <td class="{{reportCardSubS[0].Is_text_center}}">{{reportCardSubS[0].Final_persent_days}}</td>
                                            </tr>
                                            <tr>
                                                <td>ATTENDANCE PERCENT</td>
                                                <td class="{{reportCardSubS[0].Is_text_center}}">{{reportCardSubS[0].Total_Precentage_class_of_term_I}}%</td>
                                                <td class="{{reportCardSubS[0].Is_text_center}}">{{reportCardSubS[0].Total_Precentage_class_of_term_II}}%</td>
                                                <td class="{{reportCardSubS[0].Is_text_center}}">{{reportCardSubS[0].Total_Precentage_class_of_term_III}}%</td>
                                                <td class="{{reportCardSubS[0].Is_text_center}}">{{reportCardSubS[0].Final_percent_days}}%</td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>

                                <div class="qr-dvs v-false {{reportCardSubS[0].qr_div_true}}" style="display: none !important">
                                    <img src="{{reportCardSubS[0].qr_code_Show}}" />
                                </div>
                                <div class="prcntage-remrk-dv {{ttlNos[0].Comentory_remarks}}">
                                    <p>Remark :</p>
                                    <span>{{ttlNos[0].Comentory_remarks}}</span>
                                </div>
                                <%--<div class="remarks-rp  {{reportCardSubS[0].Remarkss}}">
                                    <p>REMARKS : <span class="{{reportCardSubS[0].Remarkss}}">{{reportCardSubS[0].Remarkss}}</span></p>
                                </div>--%>

                                <%--<div class="promot-dv  {{promoT[0].ShowStatuS}}">
                                    <p><span>Promoted to - </span>Session : {{promoT[0].Session}}, Class :  {{promoT[0].Class_name}}, Section :  {{promoT[0].Section}}</p>
                                </div>--%>
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

                            <div class="sig-dv v-false {{reportCardSubS[0].Sign_top}}">
                                <div class="sig-left">
                                    <div class="lft-sig-img-dv">
                                        <img class="lft-sig-img {{reportSig[0].Signature_image}}" src="{{reportSig[0].Signature_image}}" />
                                    </div>
                                    <p class="sig-ps">{{reportSig[0].Signature_name}}</p>
                                </div>
                                <div class="sig-left">
                                    <div class="cntr-sig-img-dv">
                                        <img src="{{reportSig[1].Signature_image}}" class="cntr-sig-img {{reportSig[1].Signature_image}}" />
                                    </div>
                                    <p class="sig-ps">{{reportSig[1].Signature_name}}</p>
                                </div>

                                <div class="sig-left">
                                    <div class="rght-sig-img-dv">
                                        <img src="{{reportSig[2].Signature_image}}" class="rght-sig-img {{reportSig[2].Signature_image}}" />
                                    </div>
                                    <p class="sig-ps">{{reportSig[2].Signature_name}}</p>
                                </div>
                            </div>
                            <div class="{{reportCardSubS[0].duesclassdesabled}}">{{reportCardSubS[0].Desabletext}}</div>

                            <div class="instruction-dv-fr-auto-bottom">
                                <div class="instruction-50 instruction-50-pr">
                                    <div class="instruction-tbls">
                                        <table>
                                            <tr>
                                                <th colspan="2" class="txt-center">INSTRUCTIONS</th>
                                            </tr>
                                            <tr>
                                                <td colspan="2" class="txt-center">(Scholastic and Co-Scholastic Areas {{reportCardmarkRange[0].RowCount}}-point grading scale)</td>
                                            </tr>
                                            <tr>
                                                <td>MARKS RANGE</td>
                                                <td class="{{reportCardSubS[0].Is_text_center}}">GRADE</td>
                                            </tr>
                                            <tr data-ng-repeat="x in reportCardmarkRange">
                                                <td>{{x.Lower_Range}}-{{x.Upper_Range}}</td>
                                                <td class="{{reportCardSubS[0].Is_text_center}}">{{x.Grade}}</td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <div class="instruction-50 instruction-50-pl">
                                    <div class="instruction-tbls floatrght">

                                        <div class="blank-boxs">
                                            <p class="backgrounds txtcenter">Result</p>
                                        </div>


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
                                            </table>
                                        </div>
                                        <div style="display: none !important" class="v-false {{reportCardSubS[0].Ranksdv}}" style="margin-top: {{reportCardSubS[0].Toppers_area_margn}}">
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
                                        <img src="{{reportSig[1].Signature_image}}" class="cntr-sig-img {{reportSig[1].Signature_image}}" />
                                    </div>
                                    <p class="sig-ps">{{reportSig[1].Signature_name}}</p>
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

        <asp:HiddenField ID="hd_term1" runat="server" />
        <asp:HiddenField ID="hd_term2" runat="server" />
        <asp:HiddenField ID="hd_term3" runat="server" />
        <asp:HiddenField ID="hd_admission_no" runat="server" />
        <asp:HiddenField ID="hd_session_id" runat="server" />
        <asp:HiddenField ID="hd_class_id" runat="server" />
        <asp:HiddenField ID="hd_branch_id" runat="server" />
        <asp:HiddenField ID="hd_user_type" runat="server" />

        <script type="text/javascript">
            var app = angular.module('RpCardApp', []);
            app.controller('RpCardAppCtrl', function ($scope, $http) {

                var session_id = $("#<%=hd_session_id.ClientID%>").val();
                var class_id = $("#<%=hd_class_id.ClientID%>").val();
                var admission_no = $("#<%=hd_admission_no.ClientID%>").val();
                var branch_id = $("#<%=hd_branch_id.ClientID%>").val();
                var term_id1 = $("#<%=hd_term1.ClientID%>").val();
                var term_id2 = $("#<%=hd_term2.ClientID%>").val();
                var term_id3 = $("#<%=hd_term3.ClientID%>").val();
                var userType = $("#<%=hd_user_type.ClientID%>").val();

                messge("Please Wait...");
                $("#intsLoader").removeClass("hidden");

                $http.get("final_report_card.asmx/fetch_report_card_terms_dt", { params: { "Session_id": session_id, "Class_id": class_id, "Branch_id": branch_id } }).then(function (response) {
                    $scope.reportCardTerms = response.data;
                })


                $http.get("final_report_card.asmx/fetch_report_card", { params: { "Session_id": session_id, "Class_id": class_id, "Admission_no": admission_no, "Branch_id": branch_id, "Term_id": term_id1 } }).then(function (response) {
                    $scope.reportCard = response.data;
                })

                //TERMII HEADING
                $http.get("final_report_card.asmx/fetch_report_card", { params: { "Session_id": session_id, "Class_id": class_id, "Admission_no": admission_no, "Branch_id": branch_id, "Term_id": term_id2 } }).then(function (response) {
                    $scope.reportCardTermII = response.data;
                })

                //TERMIII HEADING
                $http.get("final_report_card.asmx/fetch_report_card", { params: { "Session_id": session_id, "Class_id": class_id, "Admission_no": admission_no, "Branch_id": branch_id, "Term_id": term_id3 } }).then(function (response) {
                    $scope.reportCardTermIII = response.data;
                })

                $http.get("final_report_card.asmx/fetch_rp_card_subjects", { params: { "Session_id": session_id, "Class_id": class_id, "Admission_no": admission_no, "Branch_id": branch_id, "Term_idI": term_id1, "Term_idII": term_id2, "Term_idIII": term_id3, "UserType": userType } }).then(function (response) {
                    $scope.reportCardSubS = response.data;
                    $("#intsLoader").addClass("hidden");
                    if ($scope.reportCardSubS == "") {
                    }
                    else {
                        ////========================OverAll No.
                        $http.get("final_report_card.asmx/fetch_rp_card_total_no", { params: { "Session_id": session_id, "Class_id": class_id, "Admission_no": admission_no, "Branch_id": branch_id, "Term_idI": term_id1, "Term_idII": term_id2, "Term_idIII": term_id3 } }).then(function (response) {
                            $scope.ttlNos = response.data;
                        })


                        $http.get("final_report_card.asmx/fetch_rp_card_ranks", { params: { "Session_id": session_id, "Class_id": class_id, "Admission_no": admission_no, "Branch_id": branch_id, "Term_idI": term_id1, "Term_idII": term_id2, "Term_idIII": term_id3 } }).then(function (response) {
                            $scope.ranks = response.data;
                        })

                        //CO-SCHOLASTIC DATA
                        $http.get("final_report_card.asmx/fetch_report_card_coscholastic", { params: { "Session_id": session_id, "Class_id": class_id, "Admission_no": admission_no, "Branch_id": branch_id, "Term_idI": term_id1, "Term_idII": term_id2, "Term_idIII": term_id3 } }).then(function (response) {
                            $scope.reportCardTermII_coscholastic_data = response.data;
                        })

                        /*//DESCIPLINE DATA*/
                        $http.get("final_report_card.asmx/fetch_report_card_descipline", { params: { "Session_id": session_id, "Class_id": class_id, "Admission_no": admission_no, "Branch_id": branch_id, "Term_idI": term_id1, "Term_idII": term_id2, "Term_idIII": term_id3 } }).then(function (response) {
                            $scope.reportCardTermII_descipline_data = response.data;
                        })
                    }
                })










                $http.get("../webService/report-card.asmx/fetch_rp_card_marks_range", { params: { "Session_id": session_id, "Branch_id": branch_id, "Term_id": term_id1 } }).then(function (response) {
                    $scope.reportCardmarkRange = response.data;
                })



                $http.get("../webService/report-card.asmx/fetch_rp_card_grade_remark", { params: { "Session_id": session_id, "Class_id": class_id, "Admission_no": admission_no, "Branch_id": branch_id, "Term_id": term_id1 } }).then(function (response) {
                    $scope.reportCardgeadeRemark = response.data;
                })

                $http.get("final_report_card.asmx/fetch_rp_card_assesment_full_name").then(function (response) {
                    $scope.reportCardass_full_name = response.data;
                })

                $http.get("../webService/report-card.asmx/fetch_rp_card_signature", { params: { "Session_id": session_id, "Class_id": class_id, "Admission_no": admission_no, "Branch_id": branch_id } }).then(function (response) {
                    $scope.reportSig = response.data;
                })



                $http.get("final_report_card.asmx/fetch_rp_card_promoted_to", { params: { "Session_id": session_id, "Class_id": class_id, "Admission_no": admission_no, "Branch_id": branch_id } }).then(function (response) {
                    $scope.promoT = response.data;
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
