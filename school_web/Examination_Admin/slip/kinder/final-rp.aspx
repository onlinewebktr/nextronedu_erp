<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="final-rp.aspx.cs" Inherits="school_web.Examination_Admin.slip.kinder.final_rp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Report Card</title>
    <link href="css/finalRP.css" rel="stylesheet" />
    <script src="../../../assets/js/jquery-1.10.2.min.js"></script>
    <script src="../../../assets/Angular/angular.min.js"></script>

    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="css/finalRP.css" rel="stylesheet" type="text/css" />');
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
            <div class="prnt-btn-sec" id="printBtns" runat="server">
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
                            <img src="{{x.Watermar_image}}" class="wtr-mrk-img v-false {{x.MySubjectMarkShowItemII[0].Is_watermark_show}}" />
                            <div class="report-card-wpr" style="height: {{x.MySubjectMarkShowItemII[0].Height_dv}}">
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
                                    <h2 class="hdr-exm-info-name">Progress REPORT SESSION  ({{x.Session}}) </h2> 
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
                                        <p class="stds-info-p {{x.MySubjectMarkShowItemII[0].Ranksdv}}">
                                            <i class="stds-info-p-i">RANK</i>  <i class="stds-info-p-doti">:</i>
                                            <asp:Label ID="Label1" runat="server" Text="{{x.MyRank}}"></asp:Label>
                                        </p>
                                        <p class="stds-info-p">
                                            <i class="stds-info-p-i">ADMISSION NO.</i>  <i class="stds-info-p-doti">:</i>
                                            <asp:Label ID="lbl_admission_no" runat="server" Text="{{x.Admission_no}}"></asp:Label>
                                        </p>
                                        <p class="stds-info-p">
                                            <i class="stds-info-p-i">CLASS</i>  <i class="stds-info-p-doti">:</i>
                                            <asp:Label ID="Label5" runat="server" Text="{{x.For_class}}"></asp:Label>
                                        </p>
                                        <p class="stds-info-p {{x.MySubjectMarkShowItemII[0].Is_std_section_hide}}">
                                            <i class="stds-info-p-i">SECTION</i>  <i class="stds-info-p-doti">:</i>
                                            <span>{{x.Section}}
                                                <panel class="{{x.MySubjectMarkShowItemII[0].Ranksdv}}">/ ROLL NO. : {{x.Roll_no}}</panel></span>
                                        </p>
                                        <p class="stds-info-p {{x.MySubjectMarkShowItemII[0].Roll_show_new_line}}">
                                            <i class="stds-info-p-i">ROLL NO.</i>  <i class="stds-info-p-doti">:</i>
                                            <asp:Label ID="lbl_roll_no" runat="server" Text="{{x.Roll_no}}"></asp:Label>
                                        </p>
                                    </div>

                                    <div class="report-card-std-img-dv">
                                        <div class="sdt-img-dv">
                                            <img src="{{x.Student_image}}" class="{{x.MySubjectMarkShowItemII[0].Is_std_img_hide}}  {{x.Student_image}}" />
                                        </div>
                                    </div>
                                </div>


                                <div class="subs-mrks-area-dv">
                                    <table>
                                        <tr>
                                            <th class="{{x.MySubjectMarkShowItemII[0].Is_text_center}} {{x.MySubjectMarkShowItemII[0].Is_subj_code_hide}}">SUBJECT CODE</th>
                                            <th>MAIN SUBJECTS</th>
                                            <th class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}">
                                                <asp:Label ID="termITexts" runat="server"></asp:Label></th>
                                            <th class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}">
                                                <asp:Label ID="termIITexts" runat="server"></asp:Label></th>
                                        </tr>

                                        <tr data-ng-repeat="item in x.MySubjectMarkShowItemII track by $index">
                                            <td class="{{x.MySubjectMarkShowItemII[0].Is_text_center}} {{x.MySubjectMarkShowItemII[0].Is_subj_code_hide}}">{{item.Subject}}</td>
                                            <td>{{item.Subject_name}}</td>
                                            <td class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}" data-ng-repeat="itemx in item.MySubjectMarkItemIII track by $index"><span class="{{itemx.Gbgclass}}" style="{{itemx.gradebgss}}">{{itemx.Marks}}</span></td>
                                            <td class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}" data-ng-repeat="itemx in item.MySubjectMarkItemIIISem2 track by $index"><span class="{{itemx.Gbgclass}}" style="{{itemx.gradebgss}}">{{itemx.Marks}}</span></td>
                                        </tr>
                                        <tr>
                                            <td>Overall Grade</td>
                                            <%--<td class="{{x.MySubjectMarkShowItemII[0].Is_text_center}} {{x.MyOverallNoDetailData[0].IfbgColorR}}"><span class="{{x.MyOverallNoDetailData[0].Grade_bg_class}}" style="{{x.MyOverallNoDetailData[0].gradebgall}}">{{x.MyOverallNoDetailData[0].Grade}}</span></td>
                                            <td class="{{x.MySubjectMarkShowItemII[0].Is_text_center}} {{x.MyOverallNoDetailData[0].IfbgColorLT2}}"><span class="{{x.MyOverallNoDetailData[0].Grade_bg_classT2}}" style="{{x.MyOverallNoDetailData[0].gradebgallT2}}">{{x.MyOverallNoDetailData[0].GradeT2}}</span></td>--%>
                                            <td class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}">{{x.MyOverallNoDetailData[0].Grade}}</td>
                                            <td class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}">{{x.MyOverallNoDetailData[0].GradeT2}}</td>
                                        </tr>
                                        <tr>
                                            <td>Attendance</td>
                                            <td class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}">{{x.MyOverallNoDetailData[0].TotalPClassesT1}}/{{x.MyOverallNoDetailData[0].TotalClassesT1}}</td>
                                            <td class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}">{{x.MyOverallNoDetailData[0].TotalPClassesT2}}/{{x.MyOverallNoDetailData[0].TotalClassesT2}}</td>
                                        </tr>
                                    </table>

                                    <%--<div class="ovrll-sec">
                                        <div class="ovrll-ttl-marks-dv">
                                            <p class="ovrll-ttl-marks-cnt-p">Total Marks </p>
                                            <p class="ovrll-ttl-marks-num-p">{{x.MyOverallNoDetailData[0].Overall_obt_marks}}/{{x.MyOverallNoDetailData[0].Overall_full_marks}}</p>
                                        </div>
                                        <div class="ovrll-ttl-marks-dv">
                                            <p class="ovrll-ttl-marks-cnt-p">Percentage </p>
                                            <p class="ovrll-ttl-marks-num-p">{{x.MyOverallNoDetailData[0].Overall_percents}} (%)</p>
                                        </div>
                                        <div class="ovrll-ttl-marks-dv" style="margin: 0px 0px 0px 0px; float: right;">
                                            <p class="ovrll-ttl-marks-cnt-p">Overall Grade </p>
                                            <p class="ovrll-ttl-marks-num-p {{x.MyOverallNoDetailData[0].IfbgColorR}}"><span class="{{x.MyOverallNoDetailData[0].Grade_bg_class}}" style="{{x.myoverallnodetaildata[0].GradeBG}}">{{x.MyOverallNoDetailData[0].Grade}}</span></p>
                                        </div>
                                    </div>--%>

                                    <%--<div class="att-ovrll-sec">
                                        <div class="att-ovrll-ttl-marks-dv">
                                            <p class="att-ovrll-ttl-marks-cnt-p">TOTAL WORKING DAYS</p>
                                            <p class="att-ovrll-ttl-marks-num-p">{{x.MySubjectMarkShowItemII[0].Total_no_of_class}}</p>
                                        </div>
                                        <div class="att-ovrll-ttl-marks-dv">
                                            <p class="att-ovrll-ttl-marks-cnt-p">TOTAL PRESENT DAYS</p>
                                            <p class="att-ovrll-ttl-marks-num-p">{{x.MySubjectMarkShowItemII[0].Present_class}}</p>
                                        </div>
                                        <div class="att-ovrll-ttl-marks-dv" style="margin: 0px 0px 0px 0px; float: right;">
                                            <p class="att-ovrll-ttl-marks-cnt-p">ATTENDANCE</p>
                                            <p class="att-ovrll-ttl-marks-num-p"><span class="{{x.MySubjectMarkShowItemII[0].AttendanceColor}}">{{x.MySubjectMarkShowItemII[0].Percent_of_attandance}} (%)</span></p>
                                        </div>
                                    </div>--%>
                                </div>


                                <div class="subs-mrks-area-lft-dv">
                                    <div class="subs-mrks-area-b-dv {{x.MyDescplineData[0].Isvisiable}}" style="margin-top: {{x.MySubjectMarkShowItemII[0].Co_sch_area_margn}}">
                                        <table>
                                            <tr>
                                                <th>Social Habits</th>
                                                <th class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}">
                                                    <asp:Label ID="termITexts3" runat="server"></asp:Label></th>
                                                <th class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}">
                                                    <asp:Label ID="termITexts4" runat="server"></asp:Label></th>
                                            </tr>
                                            <tr data-ng-repeat="item in x.MyDescplineData track by $index">
                                                <td>{{item.Activity_name}}</td>
                                                <td class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}">{{item.Term_grade}}</td>
                                                <td class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}">{{item.Term_gradeT2}}</td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>


                                <div class="clsstchr-remark">
                                    <table>
                                        <tr>
                                            <th colspan="2">Class Teachers Remarks</th>
                                        </tr>
                                        <tr>
                                            <td style="width: 120px; height: 93px;">
                                                <asp:Label ID="termITexts5" runat="server"></asp:Label></td>
                                            <td><span class="{{x.MyOverallNoDetailData[0].RemarkssT1}}">{{x.MyOverallNoDetailData[0].RemarkssT1}}</span></td>
                                        </tr>
                                        <tr>
                                            <td style="height: 93px;">
                                                <asp:Label ID="termITexts6" runat="server"></asp:Label></td>
                                            <td><span class="{{x.MyOverallNoDetailData[0].RemarkssT2}}">{{x.MyOverallNoDetailData[0].RemarkssT2}}</span></td>
                                        </tr>
                                    </table>
                                </div>

                                <div class="subs-mrks-area-rght-dv">
                                    <div class="marks-range-tbls">
                                        <table>
                                            <tr>
                                                <th class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}">Grade</th>
                                                <th>Level of Performance</th>
                                                <th class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}">Percentage</th>
                                            </tr>
                                            <tr data-ng-repeat="item in x.MyMarkRangeData track by $index">
                                                <td class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}">{{item.Grade}}</td>
                                                <td>{{item.Remarks}}</td>
                                                <td class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}">{{item.Lower_Range}}% - {{item.Upper_Range}}%</td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="resuntsec">
                                        <table>
                                            <tr>
                                                <th>Result</th>
                                            </tr>
                                            <tr>
                                                <td style="height: 40px;">{{x.MyOverallNoDetailData[0].Promot_to_class}}</td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>


                                <div class="newsesnbegin">
                                    <p class="newsesnbegin-isue-date">Issue Date : {{x.RP_issue_date}}</p>
                                    <p class="newsesnbegin-new-se-date">New Session Begins On : {{x.MyOverallNoDetailData[0].School_reopen_on}}</p>
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
                                        <p class="sig-ps {{x.ParantSign}}">{{x.MySignatureDetailData[1].Signature_name}}</p>
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

        <asp:HiddenField ID="hd_req_from" runat="server" />
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
                $http.get("api/finalRP.asmx/fetch_rp_card_bulks", { params: { "Session_id": session_id, "Class_id": class_id, "Section": section, "Branch_id": branch_id, "Term_idI": term_id, "Term_idII": term_id2, "Adm_no": adm_no } }).then(function (response) {
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
        </script>
    </form>
</body>
</html>
