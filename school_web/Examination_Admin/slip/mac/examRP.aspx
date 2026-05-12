<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="examRP.aspx.cs" Inherits="school_web.Examination_Admin.slip.mac.examRP" %>

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
        <div class="invoice-sec" data-ng-app="RpCardApp">
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


            <div id="tblPrintIQ" runat="server" data-ng-controller="RpCardAppCtrl">
                <div class="invoice-inr-sec" data-ng-repeat="x in reportCardS track by $index">
                    <div class="invoice-height-manage">
                        <div class="invoice-wpr">
                            <img src="{{x.MyFirmDetailData[0].Watermar_image}}" class="wtr-mrk-img v-false {{x.MySubjectMarkShowItemII[0].Is_watermark_show}}" />
                            <div class="report-card-wpr" style="height: {{x.MySubjectMarkShowItemII[0].Height_dv}}">
                                <div class="report-card-head {{x.MyFirmDetailData[0].Content_header}}">
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
                                    </div>
                                    <asp:Label ID="lbl_aff_no" class="report-card-schl-affno {{x.MyFirmDetailData[0].Frim_aff_no}}" runat="server" Text="{{x.MySubjectMarkShowItemII[0].Aff_text}} : {{x.MyFirmDetailData[0].Frim_aff_no}}"></asp:Label>

                                    <div class="report-card-rght-dv">
                                    </div>
                                </div>

                                <div class="image-headers {{x.MyFirmDetailData[0].Header_templete}}">
                                    <img src="{{x.MyFirmDetailData[0].Header_templete}}" />
                                </div>
                                <div class="hdr-exm-info">
                                    <h2 class="hdr-exm-info-name">{{x.Assesment_name}} REPORT CARD</h2>
                                    <%--<p class="hdr-exm-info-session">SESSION : {{x.Session}}</p>--%>
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
                                    </div>

                                    <div class="report-card-std-info-dv-50">
                                        <p class="stds-info-p {{x.MySubjectMarkShowItemII[0].Father_name2}}">
                                            <i class="stds-info-p-i">FATHER'S NAME</i>  <i class="stds-info-p-doti">:</i>
                                            <asp:Label ID="Label2" runat="server" Text="{{x.Father_name}}"></asp:Label>
                                        </p>
                                        <p class="stds-info-p">
                                            <i class="stds-info-p-i">ADMISSION NO.</i>  <i class="stds-info-p-doti">:</i>
                                            <asp:Label ID="lbl_admission_no" runat="server" Text="{{x.Admission_no}}"></asp:Label>
                                        </p>
                                        <p class="stds-info-p">
                                            <i class="stds-info-p-i">CLASS</i>  <i class="stds-info-p-doti">:</i>
                                            <asp:Label ID="Label3" runat="server" Text="{{x.For_class}}"></asp:Label>
                                        </p>
                                        <p class="stds-info-p {{x.MySubjectMarkShowItemII[0].Is_std_section_hide}}">
                                            <i class="stds-info-p-i">SECTION</i>  <i class="stds-info-p-doti">:</i>
                                            <asp:Label ID="lbl_section" runat="server" Text="{{x.Section}}"></asp:Label>
                                        </p>
                                        <p class="stds-info-p">
                                            <i class="stds-info-p-i">ROLL NO.</i>  <i class="stds-info-p-doti">:</i>
                                            <asp:Label ID="lbl_roll_no" runat="server" Text="{{x.Roll_no}}"></asp:Label>
                                        </p>
                                    </div>

                                    <div class="report-card-std-img-dv">
                                        <div class="sdt-img-dv {{x.Student_image}}">
                                            <img src="{{x.Student_image}}" class="{{x.MySubjectMarkShowItemII[0].Is_std_img_hide}}" />
                                        </div>
                                    </div>
                                </div>


                                <div class="subs-mrks-area-dv">
                                    <table>
                                        <tr>
                                            <th colspan="2"></th>
                                            <th class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}" data-ng-repeat="item in x.MySubjectHeadingAssesment track by $index" colspan="2">{{item.Assessment_Name}}</th>
                                            <%--<th colspan="2" class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}"></th>--%>
                                        </tr>
                                        <tr>
                                            <th class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}">SN</th>
                                            <%--<th class="{{x.MySubjectMarkShowItemII[0].Is_text_center}} {{x.MySubjectMarkShowItemII[0].Is_subj_code_hide}}">SUBJECT CODE</th>--%>
                                            <th>SUBJECTS</th>
                                            <th class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}" data-ng-repeat="item in x.MySubjectHeading track by $index">{{item.Short_Name}}</th>
                                            <%--<th class="{{x.MySubjectMarkShowItemII[0].Is_text_center}} {{x.MySubjectMarkShowItemII[0].Grade}}">GRADE</th>--%>
                                        </tr>
                                        <tr data-ng-repeat="item in x.MySubjectMarkShowItemII track by $index">
                                            <td class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}">{{$index+1}}</td>
                                            <%--<td class="{{x.MySubjectMarkShowItemII[0].Is_text_center}} {{x.MySubjectMarkShowItemII[0].Is_subj_code_hide}}">{{item.Subject}}</td>--%>
                                            <td>{{item.Subject_name}}</td>
                                            <td class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}" data-ng-repeat="itemx in item.MySubjectMarkItemIII track by $index">{{itemx.Marks}}</td>
                                            <%--<td class="{{x.MySubjectMarkShowItemII[0].Is_text_center}} {{item.Grade}}"><span class="{{item.Gbgclass}}" style="{{item.GradeBG}}">{{item.Grade}}</span></td>--%>
                                        </tr>
                                    </table>



                                    <%--<div class="ovrll-sec">
                                        <div class="ovrll-ttl-marks-dv">
                                            <p class="ovrll-ttl-marks-cnt-p">Total Marks </p>
                                            <p class="ovrll-ttl-marks-num-p">{{x.MyOverallNoDetailData[0].Overall_obt_marks}}/{{x.MyOverallNoDetailData[0].Overall_full_marks}}</p>
                                        </div>
                                        <div class="ovrll-ttl-marks-dv">
                                            <p class="ovrll-ttl-marks-cnt-p">Percentage </p>
                                            <p class="ovrll-ttl-marks-num-p">{{x.MyOverallNoDetailData[0].Overall_percents}}</p>
                                        </div>
                                        <div class="ovrll-ttl-marks-dv" style="margin: 0px 0px 0px 0px; float: right;">
                                            <p class="ovrll-ttl-marks-cnt-p">Grade </p>
                                            <p class="ovrll-ttl-marks-num-p {{x.MyOverallNoDetailData[0].IfbgColorR}}"><span class="{{x.MyOverallNoDetailData[0].Grade_bg_class}}" style="{{x.MyOverallNoDetailData[0].GradeBG}}">{{x.MyOverallNoDetailData[0].Grade}}</span></p>
                                        </div>
                                    </div>--%>
                                </div>




                                <div class="subs-mrks-area-rght-dv">

                                    <div class="rp-card-graph-sec {{x.MySubjectMarkShowItemII[0].Graph}}" style="margin-top: {{x.MySubjectMarkShowItemII[0].Graph_area_margn}}">
                                        <p style="text-align: center; margin: 0px 0px 10px 0px; padding: 0px; width: 100%; float: left;">
                                            Subject Wise Position
                                        </p>
                                        <div class="rp-card-graph-wpr">
                                            <p class="rp-card-graph-txt0">0</p>
                                            <p class="rp-card-graph-txt50">{{x.Assmnt_hlf_mark}}</p>
                                            <p class="rp-card-graph-txt100">{{x.Assesment_maximum_marks}}</p>
                                            <div class="rp-card-graph-tbl-wpr" style="height: {{x.MySubjectMarkShowItemII[0].GraphHeight}}">
                                                <div class="rp-card-graph-tbl-inr" data-ng-repeat="item in x.MyGraphDetailData track by $index" style="width: {{item.Grph_width}}%;">
                                                    <div class="rp-card-graph-tbl-nobg" style="height: {{item.BlankHeight}}%"></div>
                                                    <div class="rp-card-graph-tbl" style="background: {{item.bg_color}}; height: {{item.final_perc}}%"></div>
                                                    <p>{{item.Subject_name}}</p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>



                                <div class="instruction-dv-fr-auto-bottom">
                                    <%--<div class="instruction-50 instruction-50-pr">
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
                                    </div>--%>
                                </div>
                                <%--<div class="grding-scale-dv-sec">
                                    <p class="grding-scale-dv-p"><span data-ng-repeat="item in x.MyMarkRangeData track by $index">{{item.Grade}} [{{item.Lower_Range}}-{{item.Upper_Range}}]</span></p>
                                </div>--%>

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
        <asp:HiddenField ID="hd_adm_no" runat="server" />
        <asp:HiddenField ID="hd_section" runat="server" />
        <asp:HiddenField ID="hd_session_id" runat="server" />
        <asp:HiddenField ID="hd_class_id" runat="server" />
        <asp:HiddenField ID="hd_branch_id" runat="server" />
        <asp:HiddenField ID="hd_term_id" runat="server" />
        <asp:HiddenField ID="hd_assesment_id" runat="server" />

        <script type="text/javascript">
            var app = angular.module('RpCardApp', []);
            app.controller('RpCardAppCtrl', function ($scope, $http) {
                var session_id = $("#<%=hd_session_id.ClientID%>").val();
                var class_id = $("#<%=hd_class_id.ClientID%>").val();
                var section = $("#<%=hd_section.ClientID%>").val();
                var branch_id = $("#<%=hd_branch_id.ClientID%>").val();
                var term_id = $("#<%=hd_term_id.ClientID%>").val();
                var adm_no = $("#<%=hd_adm_no.ClientID%>").val();
                var assesment_id = $("#<%=hd_assesment_id.ClientID%>").val();

                messge("Please Wait...");
                $("#intsLoader").removeClass("hidden");
                $http.get("api/examRP.asmx/fetch_rp_card_bulks", { params: { "Session_id": session_id, "Class_id": class_id, "Section": section, "Branch_id": branch_id, "Term_id": term_id, "Adm_no": adm_no, "Assesment_id": assesment_id } }).then(function (response) {
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
