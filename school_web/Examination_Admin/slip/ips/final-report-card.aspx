<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="final-report-card.aspx.cs" Inherits="school_web.Examination_Admin.slip.ips.final_report_card" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Report Card</title>
    <link href="https://fonts.googleapis.com/css?family=Montserrat:100,200,300,400,500,600,700" rel="stylesheet" />

    <link href="report-card.css" rel="stylesheet" />
    <script src="../../../assets/js/jquery-1.10.2.min.js"></script>
    <script src="../../../assets/Angular/angular.min.js"></script>

    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="https://fonts.googleapis.com/css?family=Montserrat:100,200,300,400,500,600,700" rel="stylesheet" /><link href="report-card.css" rel="stylesheet" type="text/css" />');
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
                            <asp:Button ID="btn_back" CssClass="back-btn" runat="server" />
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
                            <div class="report-card-head {{reportCardSubS[0].Hdr_frst}}">
                                <div class="report-card-left-dv" style="height: 5px;">
                                    <asp:Image ID="Image1" runat="server" Style="display: none" />
                                    <asp:Label ID="lbl_estd" runat="server" Style="display: none!important;" class="estdTextP v-false {{reportCardSubS[0].Is_estd_show}}"></asp:Label>
                                </div>
                                <div class="report-card-cntr-dv">
                                    <asp:Label ID="lbl_school_name" class="report-card-schlname" runat="server" Style="display: none"></asp:Label>
                                    <asp:Label ID="lbl_aff_text" class="report-card-schl-affno-by" runat="server"></asp:Label>
                                    <asp:Label ID="lbl_address" runat="server" class="report-card-schl-add" Style="display: none"></asp:Label>
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
                                    <h2 class="report-card-rprt-crd">ANNUAL REPORT CARD FOR  <span class="{{reportCardSubS[0].Class_in_new_line}}"><span class="v-false {{reportCardSubS[0].Is_class_text_show}}">CLASS</span>
                                        <asp:Label ID="lbl_for_class" runat="server"></asp:Label></span></h2>
                                </div>
                                <asp:Label ID="lbl_aff_no" class="report-card-schl-affno" runat="server" Style="display: none"></asp:Label>
                                <div class="report-card-rght-dv">
                                    <asp:Image ID="img_student_img" runat="server" class="{{reportCardSubS[0].Is_std_img_hide}}" />
                                </div>
                            </div>

                            <div class="report-card-head hidden {{reportCardSubS[0].Hdr_scnd}}">
                                <div class="report-card-left-dv">
                                    <asp:Image ID="Image2" runat="server" />
                                    <asp:Label ID="lbl_estd1" Style="display: none!important;" runat="server" class="estdTextP v-false {{reportCardSubS[0].Is_estd_show}}"></asp:Label>
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
                                    <h2 class="report-card-ac-sson">ACADEMIC SESSION :
                                        <asp:Label ID="lbl_sessions1" runat="server"></asp:Label></h2>
                                    <h2 class="report-card-rprt-crd">ANNUAL REPORT CARD  <span class="{{reportCardSubS[0].Class_in_new_line}}"><span class="v-false {{reportCardSubS[0].Is_class_text_show}}">CLASS</span>
                                        <asp:Label ID="lbl_for_class1" runat="server"></asp:Label></span></h2>
                                </div>

                                <div class="report-card-rght-dv1">
                                    <asp:Image ID="img_extra_log" runat="server" class="class_extra_logo" />


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
                                    <p class="stds-info-p" style="display: none">
                                        <i class="stds-info-p-i">MOTHER'S NAME</i>  <i class="stds-info-p-doti">:</i>
                                        <asp:Label ID="lbl_mother_name" runat="server" Text="JELI BIBI"></asp:Label>
                                    </p>
                                    <p class="stds-info-p {{reportCardSubS[0].Father_name1}}" runat="server" id="FtherDV1" style="display: none">
                                        <i class="stds-info-p-i">FATHER'S NAME</i>  <i class="stds-info-p-doti">:</i>
                                        <asp:Label ID="lbl_father_name" runat="server" Text="TOFAIL SK."></asp:Label>
                                    </p>
                                </div>

                                <div class="report-card-std-info-dv-50">
                                    <p class="stds-info-p  {{reportCardSubS[0].Father_name2}}" runat="server" id="FtherDV2" style="display: none!important;">
                                        <i class="stds-info-p-i">FATHER'S NAME</i>  <i class="stds-info-p-doti">:</i>
                                        <asp:Label ID="lbl_father_name2" runat="server" Text=""></asp:Label>
                                    </p>

                                    <p class="stds-info-p" runat="server" id="ranksDV" visible="false">
                                        <i class="stds-info-p-i">Rank</i>  <i class="stds-info-p-doti">:</i>
                                        <asp:Label ID="lbl_rank" runat="server" Text="5805"></asp:Label>
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
                                        <th>SCHOLASTIC AREA</th>
                                        <th class="txt-center">
                                            <asp:Label ID="termITexts" runat="server"></asp:Label><%--TERM-I--%></th>
                                        <th colspan="{{reportCardSubS[0].ColspanFive}}" class="txt-center">
                                            <asp:Label ID="termIITexts" runat="server"></asp:Label><%--TERM-II--%></th>
                                        <%--<th colspan="{{reportCardSubS[0].ColspanOneTwo}}" class="txt-center">OVERALL</th>--%>
                                        <th rowspan="2">HIGHEST MARK</th>
                                    </tr>
                                    <tr>
                                        <th>SN</th>
                                        <th style="width: 190px;">SUBJECTS</th>

                                        <%--<th data-ng-repeat="x in reportCard">{{x.Short_Name}} ({{x.Maximum_Marks}})</th>--%>
                                        <th class="{{reportCardSubS[0].If_is_grade_ttl_mark_hide}}">{{reportCardSubS[0].ttl_marks_head_termI}} ({{reportCard[0].Term_Maximum_Marks}})</th>
                                        <%--<th>{{reportCardSubS[0].TermI_grade_head}} <span class="{{reportCardSubS[0].If_is_mrk_hide}}">({{reportCard[0].Term_Maximum_Marks}})</span></th>--%>


                                        <th data-ng-repeat="x in reportCardTermII">{{x.Short_Name}} ({{x.Maximum_Marks}})</th>
                                        <th class="{{reportCardSubS[0].If_is_grade_ttl_mark_hide}}">{{reportCardSubS[0].ttl_marks_head_termII}} ({{reportCardTermII[0].Term_Maximum_Marks}})</th>
                                        <th>{{reportCardSubS[0].TermII_grade_head}} <span class="{{reportCardSubS[0].If_is_mrk_hide}}">({{reportCard[0].Term_Maximum_Marks}})</span></th>


                                        <th class="{{reportCardSubS[0].Overall_av_marks}}" style="display: none">AVERAGE (A+B/2) (100)</th>

                                        <%--<th>{{reportCardSubS[0].Overall_ab_grade}}</th>--%>
                                    </tr>
                                    <tr data-ng-repeat="x in reportCardSubS track by $index">
                                        <td>{{$index+1}}</td>
                                        <td>{{x.Subject_name}}</td>
                                        <%--<td data-ng-repeat="item in x.MySubjectMarkItem track by $index">{{item.Marks_term_I}}</td>--%>
                                        <td class="{{reportCardSubS[0].If_is_grade_ttl_mark_hide}}">{{x.Total_mark_of_a_subject_for_termI}}</td>
                                        <%--<td class="{{x.Grade}}">{{x.grade_of_a_subject_for_termI}}</td>--%>


                                        <td data-ng-repeat="item in x.MySubjectMarkItem track by $index">{{item.Marks_term_II}}</td>
                                        <td class="{{reportCardSubS[0].If_is_grade_ttl_mark_hide}}">{{x.Total_mark_of_a_subject_for_termII}}</td>
                                        <td class="{{x.Grade}}">{{x.grade_of_a_subject_for_termII}}</td>

                                        <td class="{{reportCardSubS[0].Overall_av_marks}}" style="display: none">{{x.termI_termIi_average_percent}}</td>

                                        <td>{{x.Subj_highest_mark}}</td>
                                        <%--<td>{{x.termI_termII_grade}}</td>--%>
                                    </tr>
                                    <%--<tr>
                                        <td colspan="2" class="txt-center font-weight600">ATTENDANCE</td>
                                        <td colspan="{{reportCardSubS[0].ColspanFive}}" class="txt-center font-weight600">{{reportCardSubS[0].Total_Present_class_of_term_I}} OUT OF {{reportCardSubS[0].Total_class_of_term_I}} DAYS</td>
                                        <td colspan="{{reportCardSubS[0].ColspanFive}}" class="txt-center font-weight600">{{reportCardSubS[0].Total_Present_class_of_term_II}} OUT OF {{reportCardSubS[0].Total_class_of_term_II}} DAYS</td>
                                        <td class="txt-center font-weight600 {{reportCardSubS[0].Overall_av_marks}}">{{reportCardSubS[0].Overall_final_percent}}</td>
                                        <td class="txt-center font-weight600">{{reportCardSubS[0].Overall_final_grade}}</td>
                                    </tr>--%>
                                </table>
                            </div>

                            <div class="subs-mrks-area-lft-dv">
                                <div class="subs-mrks-area-b-dv" style="margin-top: {{reportCardSubS[0].Co_sch_area_margn}}">
                                    <table>
                                        <tr>
                                            <th>B</th>
                                            <th colspan="3">CO-SCHOLASTIC AREAS [{{reportCardTermII_coscholastic_data[0].RowCount}} - POINT GRADING SCALE]</th>
                                        </tr>
                                        <tr>
                                            <th class="th-bg-rmov">SN</th>
                                            <th class="th-bg-rmov">ACTIVITIES</th>
                                            <th class="th-bg-rmov">TERM-I</th>
                                            <th class="th-bg-rmov">TERM-II</th>
                                        </tr>
                                        <tr data-ng-repeat="x in reportCardTermII_coscholastic_data">
                                            <td>{{$index+1}}</td>
                                            <td>{{x.Subject_name}}</td>
                                            <td>{{x.Term_I_grade}}</td>
                                            <td>{{x.Term_II_grade}}</td>
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
                                        <%--<tr class=" {{reportCardSubS[0].Is_attandance_show}}">
                                            <td>TOTAL WORKING DAYS</td>
                                            <td>{{reportCardSubS[0].Total_no_of_class}}</td>
                                        </tr>
                                        <tr class=" {{reportCardSubS[0].Is_attandance_show}}">
                                            <td>TOTAL PRESENT DAYS</td>
                                            <td>{{reportCardSubS[0].Present_class}}</td>
                                        </tr>
                                        <tr class=" {{reportCardSubS[0].Is_attandance_show}}">
                                            <td>ATTENDANCE PERCENT</td>
                                            <td>{{reportCardSubS[0].Percent_of_attandance}} (%)</td>
                                        </tr>--%>
                                        <%--<tr class=" {{reportCardSubS[0].SpecialNote}}">
                                            <td class="spcial-notes" colspan="2"></td>
                                        </tr>--%>
                                    </table>

                                    <table style="margin: 3px 0px 0px 0px;">
                                        <tr>
                                            <th>ATTENDANCE</th>
                                            <th class="{{reportCardSubS[0].Is_text_center}}">
                                                <asp:Label ID="termIText1" runat="server"></asp:Label></th>
                                            <th class="{{reportCardSubS[0].Is_text_center}}">
                                                <asp:Label ID="termIText2" runat="server"></asp:Label></th>
                                            <th class="{{reportCardSubS[0].Is_text_center}}">FINAL</th>
                                        </tr>
                                        <tr>
                                            <td>TOTAL WORKING DAYS</td>
                                            <td class="{{reportCardSubS[0].Is_text_center}}">{{attendances[0].Total_class_of_term_I}}</td>
                                            <td class="{{reportCardSubS[0].Is_text_center}}">{{attendances[0].Total_class_of_term_II}}</td>
                                            <td class="{{reportCardSubS[0].Is_text_center}}">{{attendances[0].Final_working_days}}</td>
                                        </tr>

                                        <tr>
                                            <td>TOTAL PRESENT DAYS</td>
                                            <td class="{{reportCardSubS[0].Is_text_center}}">{{attendances[0].Total_Present_class_of_term_I}}</td>
                                            <td class="{{reportCardSubS[0].Is_text_center}}">{{attendances[0].Total_Present_class_of_term_II}}</td>
                                            <td class="{{reportCardSubS[0].Is_text_center}}">{{attendances[0].Final_persent_days}}</td>
                                        </tr>
                                        <tr>
                                            <td>ATTENDANCE PERCENTAGE</td>
                                            <td class="{{reportCardSubS[0].Is_text_center}}">{{attendances[0].Total_Precentage_class_of_term_I}}%</td>
                                            <td class="{{reportCardSubS[0].Is_text_center}}">{{attendances[0].Total_Precentage_class_of_term_II}}%</td>
                                            <td class="{{reportCardSubS[0].Is_text_center}}">{{attendances[0].Final_percent_days}}%</td>
                                        </tr>
                                    </table>
                                </div>

                                <div class="qr-dvs v-false {{reportCardSubS[0].qr_div_true}}">
                                    <img src="{{reportCardSubS[0].qr_code_Show}}" />
                                </div>

                                <div class="remarks-rp  {{reportCardSubS[0].Remarkss}}">
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


                            <p class="promtn-grntd-p"><span>Promotion :</span>  Guaranteed  <i>|</i> Not Guaranteed <i>|</i> ONTRIAL</p>

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

        <asp:HiddenField ID="hd_admission_no" runat="server" />
        <asp:HiddenField ID="hd_session_id" runat="server" />
        <asp:HiddenField ID="hd_class_id" runat="server" />
        <asp:HiddenField ID="hd_branch_id" runat="server" />

        <script type="text/javascript">
            var app = angular.module('RpCardApp', []);
            app.controller('RpCardAppCtrl', function ($scope, $http) {

                var session_id = $("#<%=hd_session_id.ClientID%>").val();
                var class_id = $("#<%=hd_class_id.ClientID%>").val();
                var admission_no = $("#<%=hd_admission_no.ClientID%>").val();
                var branch_id = $("#<%=hd_branch_id.ClientID%>").val();
                var term_id1 = $("#<%=hd_term1.ClientID%>").val();
                var term_id2 = $("#<%=hd_term2.ClientID%>").val();


                messge("Please Wait...");
                $("#intsLoader").removeClass("hidden");

                $http.get("final_report_card.asmx/fetch_report_card", { params: { "Session_id": session_id, "Class_id": class_id, "Admission_no": admission_no, "Branch_id": branch_id, "Term_id": term_id1 } }).then(function (response) {
                    $scope.reportCard = response.data;
                })

                //TERMII HEADING
                $http.get("final_report_card.asmx/fetch_report_card", { params: { "Session_id": session_id, "Class_id": class_id, "Admission_no": admission_no, "Branch_id": branch_id, "Term_id": term_id2 } }).then(function (response) {
                    $scope.reportCardTermII = response.data;
                })


                $http.get("final_report_card.asmx/fetch_rp_card_subjects", { params: { "Session_id": session_id, "Class_id": class_id, "Admission_no": admission_no, "Branch_id": branch_id, "Term_idI": term_id1, "Term_idII": term_id2 } }).then(function (response) {
                    $scope.reportCardSubS = response.data;
                    $("#intsLoader").addClass("hidden");
                    if ($scope.reportCardSubS == "") {
                    }
                    else {
                        ////========================OverAll No.
                        $http.get("final_report_card.asmx/fetch_rp_card_total_no", { params: { "Session_id": session_id, "Class_id": class_id, "Admission_no": admission_no, "Branch_id": branch_id, "Term_id": term_id2 } }).then(function (response) {
                            $scope.ttlNos = response.data;
                        })

                        ////========================ATTENDANCE
                        $http.get("final_report_card.asmx/fetch_rp_card_attendance", { params: { "Session_id": session_id, "Class_id": class_id, "Admission_no": admission_no, "Branch_id": branch_id, "Term_id": term_id1, "Term_idII": term_id2 } }).then(function (response) {
                            $scope.attendances = response.data;
                        })
                    }
                })













                //CO-SCHOLASTIC DATA
                $http.get("final_report_card.asmx/fetch_report_card_co_scholastic", { params: { "Session_id": session_id, "Class_id": class_id, "Admission_no": admission_no, "Branch_id": branch_id, "Term_idI": term_id1, "Term_idII": term_id2 } }).then(function (response) {
                    $scope.reportCardTermII_coscholastic_data = response.data;
                })

                //DESCIPLINE DATA
                //$http.get("final_report_card.asmx/fetch_report_card_descipline", { params: { "Session_id": session_id, "Class_id": class_id, "Admission_no": admission_no, "Branch_id": branch_id, "Term_idI": term_id1, "Term_idII": term_id2 } }).then(function (response) {
                //    $scope.reportCardTermII_descipline_data = response.data;
                //})


                $http.get("report-card.asmx/fetch_rp_card_marks_range", { params: { "Session_id": session_id, "Branch_id": branch_id, "Term_id": term_id1 } }).then(function (response) {
                    $scope.reportCardmarkRange = response.data;
                })



                $http.get("report-card.asmx/fetch_rp_card_grade_remark", { params: { "Session_id": session_id, "Class_id": class_id, "Admission_no": admission_no, "Branch_id": branch_id, "Term_id": term_id1 } }).then(function (response) {
                    $scope.reportCardgeadeRemark = response.data;
                })

                $http.get("final_report_card.asmx/fetch_rp_card_assesment_full_name").then(function (response) {
                    $scope.reportCardass_full_name = response.data;
                })

                $http.get("report-card.asmx/fetch_rp_card_signature", { params: { "Session_id": session_id, "Class_id": class_id, "Admission_no": admission_no, "Branch_id": branch_id } }).then(function (response) {
                    $scope.reportSig = response.data;
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
