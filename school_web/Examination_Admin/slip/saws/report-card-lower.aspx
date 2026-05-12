<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="report-card-lower.aspx.cs" Inherits="school_web.Examination_Admin.slip.saws.report_card_lower" %>

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
        <div class="invoice-sec" data-ng-app="RpCardApp" data-ng-controller="RpCardAppCtrl" runat="server" id="reportcrdDV">
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
                                    <h2 class="hdr-exm-info-name">{{x.Term_name}} REPORT CARD</h2>
                                    <p class="hdr-exm-info-session">SESSION : {{x.Session}}</p>
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
                                            <th class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}">A</th>
                                            <th class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}" colspan="18">SCHOLASTIC AREAS</th>
                                        </tr>
                                        <tr>
                                            <th class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}">SN</th>
                                            <th class="{{x.MySubjectMarkShowItemII[0].Is_text_center}} {{x.MySubjectMarkShowItemII[0].Is_subj_code_hide}}">SUBJECT CODE</th>
                                            <th>SUBJECTS</th>
                                            <th class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}" data-ng-repeat="item in x.MySubjectHeading track by $index">{{item.Short_Name}} <span class="{{x.MySubjectMarkShowItemII[0].Max_mark_show}}">({{item.Maximum_Marks}})</span></th>
                                            <th class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}">{{x.MySubjectMarkShowItemII[0].grade_head_text}} ({{x.MySubjectMarkShowItemII[0].SubjFulmarks}})</th>
                                            <th class="{{x.MySubjectMarkShowItemII[0].Is_text_center}} {{x.MySubjectMarkShowItemII[0].Grade}}">GRADE</th>
                                        </tr>

                                        <tr data-ng-repeat="item in x.MySubjectMarkShowItemII track by $index">
                                            <td class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}">{{$index+1}}</td>
                                            <td class="{{x.MySubjectMarkShowItemII[0].Is_text_center}} {{x.MySubjectMarkShowItemII[0].Is_subj_code_hide}}">{{item.Subject}}</td>
                                            <td>{{item.Subject_name}}</td>
                                            <td class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}" data-ng-repeat="itemx in item.MySubjectMarkItemIII track by $index">{{itemx.Marks}}</td>
                                            <td class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}">{{item.Total_marks}}</td>
                                            <td class="{{x.MySubjectMarkShowItemII[0].Is_text_center}} {{item.Grade}}"><span class="{{item.Gbgclass}}" style="{{item.GradeBG}}">{{item.Grade}}</span></td>
                                        </tr>
                                    </table>

                                    <div class="ovrll-sec">
                                        <div class="ovrll-ttl-marks-dv">
                                            <p class="ovrll-ttl-marks-cnt-p">Total Marks </p>
                                            <p class="ovrll-ttl-marks-num-p">{{x.MyOverallNoDetailData[0].Overall_obt_marks}}/{{x.MyOverallNoDetailData[0].Overall_full_marks}}</p>
                                        </div>
                                        <div class="ovrll-ttl-marks-dv">
                                            <p class="ovrll-ttl-marks-cnt-p">Percentage </p>
                                            <p class="ovrll-ttl-marks-num-p">{{x.MyOverallNoDetailData[0].Overall_percents}} (%)</p>
                                        </div>
                                        <div class="ovrll-ttl-marks-dv" style="margin: 0px 0px 0px 0px; float: right;">
                                            <p class="ovrll-ttl-marks-cnt-p">Grade </p>
                                            <p class="ovrll-ttl-marks-num-p {{x.MyOverallNoDetailData[0].IfbgColorR}}"><span class="{{x.MyOverallNoDetailData[0].Grade_bg_class}}" style="{{x.MyOverallNoDetailData[0].gradebg}}">{{x.MyOverallNoDetailData[0].Grade}}</span></p>
                                        </div>
                                    </div>

                                    <div class="att-ovrll-sec {{x.MySubjectMarkShowItemII[0].Is_attandance_show}}">
                                        <div class="att-ovrll-ttl-marks-dv" style="width: 31.5%;">
                                            <p class="att-ovrll-ttl-marks-cnt-p">TOTAL WORKING DAYS</p>
                                            <p class="att-ovrll-ttl-marks-num-p">{{x.MySubjectMarkShowItemII[0].Total_no_of_class}}</p>
                                        </div>
                                        <div class="att-ovrll-ttl-marks-dv" style="width: 31%;">
                                            <p class="att-ovrll-ttl-marks-cnt-p">TOTAL PRESENT DAYS</p>
                                            <p class="att-ovrll-ttl-marks-num-p">{{x.MySubjectMarkShowItemII[0].Present_class}}</p>
                                        </div>
                                        <div class="att-ovrll-ttl-marks-dv" style="margin: 0px 0px 0px 0px; float: right; width: 35%;">
                                            <p class="att-ovrll-ttl-marks-cnt-p">ATTENDANCE PERCENTAGE</p>
                                            <p class="att-ovrll-ttl-marks-num-p"><span class="{{x.MySubjectMarkShowItemII[0].AttendanceColor}}">{{x.MySubjectMarkShowItemII[0].Percent_of_attandance}} (%)</span></p>
                                        </div>
                                    </div>
                                </div>


                                <div class="subs-mrks-area-lft-dv" style="width: 100%">
                                    <div class="subs-mrks-area-b-dv" style="margin-top: 5px;">
                                        <table>
                                            <tr>
                                                <th>B</th>
                                                <th colspan="2">CO-SCHOLASTIC AREAS</th>
                                            </tr>
                                            <tr>
                                                <th class="th-bg-rmov">SN</th>
                                                <th class="th-bg-rmov">ACTIVITIES</th>
                                                <th class="th-bg-rmov {{x.MySubjectMarkShowItemII[0].Is_text_center}}">GRADE</th>
                                            </tr>
                                            <tr data-ng-repeat="item in x.MyCoScholasticData track by $index">
                                                <td>{{$index+1}}</td>
                                                <td>{{item.Subject_Name}}</td>
                                                <td class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}">{{item.Total_marks}}</td>
                                            </tr>
                                        </table>
                                    </div>


                                    <div class="subs-mrks-area-b-dvpT" style="margin-top: 5px;">
                                        <table>
                                            <tr>
                                                <th>C</th>
                                                <th colspan="2">Personality Trait</th>
                                            </tr>
                                            <tr>
                                                <th class="th-bg-rmov">SN</th>
                                                <th class="th-bg-rmov">ACTIVITIES</th>
                                                <th class="th-bg-rmov {{x.MySubjectMarkShowItemII[0].Is_text_center}}">GRADE</th>
                                            </tr>
                                            <tr data-ng-repeat="item in x.MyDescplineData track by $index">
                                                <td>{{$index+1}}</td>
                                                <td>{{item.Activity_name}}</td>
                                                <td class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}">{{item.Term_grade}}</td>
                                            </tr>
                                        </table>
                                    </div>

                                    <div class="remarks-rp {{x.MySubjectMarkShowItemII[0].Remarkss}}">
                                        <p>REMARKS :<span>{{x.MySubjectMarkShowItemII[0].Remarkss}}</span></p>
                                    </div>
                                    <%--<div class="prcntage-remrk-dv">
                                        <p>Remark : <span>{{x.MySubjectMarkShowItemII[0].P_remark}}</span></p>
                                    </div>--%>
                                </div>

                                <div class="subs-mrks-area-rght-dv">
                                    <%--<div class="qr-dvs {{x.MySubjectMarkShowItemII[0].qr_div_true}}">
                                        <img src="{{x.MyOverallNoDetailData[0].Graphurl}}" />
                                    </div> 

                                    <div class="rp-card-graph-sec {{x.MySubjectMarkShowItemII[0].Graph}}">
                                        <p style="text-align: center; margin: 0px 0px 10px 0px; padding: 0px; width: 100%; float: left;">
                                            Performance Analysis Chart
                                        </p>
                                        <div class="rp-card-graph-wpr">
                                            <p class="rp-card-graph-txt0">0</p>
                                            <p class="rp-card-graph-txt50">{{x.MySubjectMarkShowItemII[0].TermSubj_hlf_mark}}</p>
                                            <p class="rp-card-graph-txt100">{{x.MySubjectMarkShowItemII[0].SubjFulmarks}}</p>
                                            <div class="rp-card-graph-tbl-wpr" style="height: {{x.MySubjectMarkShowItemII[0].GraphHeight}}">
                                                <div class="rp-card-graph-tbl-inr" data-ng-repeat="item in x.MyGraphDetailData track by $index" style="width: {{item.Grph_width}}%;">
                                                    <div class="rp-card-graph-tbl-nobg" style="height: {{item.BlankHeight}}%"></div>
                                                    <div class="rp-card-graph-tbl" style="background: {{item.bg_color}}; height: {{item.final_perc}}%"></div>
                                                    <p>{{item.Subject_name}}</p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>--%>

                                    <div class="toppers-tbls v-false {{x.MySubjectMarkShowItemII[0].Ranksdv}}">
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





                                <div class="grding-scale-dv-sec">
                                    <p class="issueDate">Issue Date : {{x.Issue_date}}</p>
                                    <p class="grding-scale-dv-p">{{x.MyMarkRangeData[0].RowCount}} Point Grading Scale : <span data-ng-repeat="item in x.MyMarkRangeData track by $index">{{item.Grade}} [{{item.Lower_Range}}-{{item.Upper_Range}}]</span></p>
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

        <asp:Panel ID="pnl_dues" runat="server" Visible="false">
            <div class="duesmsg-dv">
                <div class="duesmsg-dv-inrs">
                    <img src="error-con.png" />
                    <p class="duesmsg-p">Please clear your dues or reach out to the school.</p>
                </div>
            </div>
        </asp:Panel>


        <style>
            .duesmsg-dv {
                width: 100%;
                float: left;
                margin: 0px;
                padding: 150px 0px 0px 0px;
            }

            .duesmsg-dv-inrs {
                width: 100%;
                float: left;
                margin: 0px;
                padding: 0px;
                text-align: center;
            }

                .duesmsg-dv-inrs img {
                    width: 200px;
                    margin: 0px auto;
                }

            .duesmsg-p {
                width: 100%;
                float: left;
                margin: 10px 0px 0px 0px;
                padding: 0px;
                color: #f00;
                text-align: center;
                font-size: 40px;
                font-weight: 700;
                letter-spacing: 1px;
            }
        </style>
        <asp:HiddenField ID="hd_req_from" runat="server" />
        <asp:HiddenField ID="hd_adm_no" runat="server" />
        <asp:HiddenField ID="hd_section" runat="server" />
        <asp:HiddenField ID="hd_session_id" runat="server" />
        <asp:HiddenField ID="hd_class_id" runat="server" />
        <asp:HiddenField ID="hd_branch_id" runat="server" />
        <asp:HiddenField ID="hd_term_id" runat="server" />


        <script type="text/javascript">
            var app = angular.module('RpCardApp', []);
            app.controller('RpCardAppCtrl', function ($scope, $http) {
                var adm_no = $("#<%=hd_adm_no.ClientID%>").val();
                var session_id = $("#<%=hd_session_id.ClientID%>").val();
                var class_id = $("#<%=hd_class_id.ClientID%>").val();
                var section = $("#<%=hd_section.ClientID%>").val();
                var branch_id = $("#<%=hd_branch_id.ClientID%>").val();
                var term_id = $("#<%=hd_term_id.ClientID%>").val();

                messge("Please Wait...");
                $("#intsLoader").removeClass("hidden");
                $http.get("api/reportCard.asmx/fetch_rp_card_bulks", { params: { "Session_id": session_id, "Class_id": class_id, "Section": section, "Branch_id": branch_id, "Term_id": term_id, "Adm_no": adm_no } }).then(function (response) {
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
