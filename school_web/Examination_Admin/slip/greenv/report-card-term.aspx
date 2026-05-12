<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="report-card-term.aspx.cs" Inherits="school_web.Examination_Admin.slip.greenv.report_card_term" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Report Card</title>
    <link href="https://fonts.googleapis.com/css?family=Montserrat:100,200,300,400,500,600,700" rel="stylesheet" />
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
                    <div class="invoice-wpr">
                        <asp:Image ID="img_watermark" runat="server" class="wtr-mrk-img {{reportCardSubS[0].Is_watermark_show}}" />
                        <div class="report-card-wpr" style="height: {{reportCardSubS[0].Height_dv}}">
                            <div class="report-card-head {{reportCardSubS[0].Hdr_frst}}" style="display: none !important">
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

                            <div class="report-card-head">
                                <div class="report-card-left-dv">
                                    <asp:Image ID="Image2" runat="server" />
                                    <asp:Label ID="lbl_estd1" runat="server" class="estdTextP v-false {{reportCardSubS[0].Is_estd_show}}"></asp:Label>
                                </div>
                                <div class="report-card-cntr-dv">
                                    <asp:Label ID="lbl_school_name1" class="report-card-schlname" runat="server"></asp:Label>
                                    <asp:Label ID="lbl_aff_text1" class="report-card-schl-affno-by" runat="server"></asp:Label>
                                    <asp:Label ID="lbl_address1" runat="server" class="report-card-schl-add fontsize13"></asp:Label>

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
                                            <asp:Label ID="lbl_for_class1" runat="server"></asp:Label></span></h2>
                                </div>

                                <div class="report-card-rght-dv1">
                                    <asp:Image ID="img_extra_log" runat="server" class="class_extra_logo" />


                                    <div class="std_imgsdv" style="display: none !important">
                                        <asp:Image ID="img_student_img1" runat="server" class="{{reportCardSubS[0].Is_std_img_hide}}" />
                                    </div>
                                </div>
                            </div>

                            <div class="report-card-std-info-dv">
                                <div class="report-card-std-info-dv-50">
                                    <p class="stds-info-p">
                                        <i class="stds-info-p-i">Student's Name</i>  <i class="stds-info-p-doti">:</i>
                                        <asp:Label ID="lbl_student_name" runat="server"></asp:Label>
                                    </p>
                                    <p class="stds-info-p" style="display: none !important">
                                        <i class="stds-info-p-i">DATE OF BIRTH</i>  <i class="stds-info-p-doti">:</i>
                                        <asp:Label ID="lbl_date_of_birth" runat="server"></asp:Label>
                                    </p>
                                    <p class="stds-info-p" style="display: none !important">
                                        <i class="stds-info-p-i">MOTHER'S NAME</i>  <i class="stds-info-p-doti">:</i>
                                        <asp:Label ID="lbl_mother_name" runat="server"></asp:Label>
                                    </p>
                                    <p class="stds-info-p {{reportCardSubS[0].Father_name1}}" runat="server" id="FtherDV1" style="display: none !important">
                                        <i class="stds-info-p-i">FATHER'S NAME</i>  <i class="stds-info-p-doti">:</i>
                                        <asp:Label ID="lbl_father_name" runat="server"></asp:Label>
                                    </p>
                                    <p class="stds-info-p">
                                        <i class="stds-info-p-i">ADMISSION NUMBER</i>  <i class="stds-info-p-doti">:</i>
                                        <asp:Label ID="lbl_admission_no" runat="server"></asp:Label>
                                    </p>
                                </div>

                                <div class="report-card-std-info-dv-50">
                                    <p class="stds-info-p  {{reportCardSubS[0].Father_name2}}" runat="server" id="FtherDV2" style="display: none !important">
                                        <i class="stds-info-p-i">FATHER'S NAME</i>  <i class="stds-info-p-doti">:</i>
                                        <asp:Label ID="lbl_father_name2" runat="server"></asp:Label>
                                    </p>

                                    <p class="stds-info-p" runat="server" id="ranksDV" visible="false" style="display: none">
                                        <i class="stds-info-p-i">Rank</i>  <i class="stds-info-p-doti">:</i>
                                        <asp:Label ID="lbl_rank" runat="server"></asp:Label>
                                    </p>


                                    
                                    <p class="stds-info-p {{reportCardSubS[0].Is_std_section_hide}}">
                                        <i class="stds-info-p-i">SECTION</i>  <i class="stds-info-p-doti">:</i>
                                        <asp:Label ID="lbl_section" runat="server"></asp:Label>
                                    </p>
                                    <p class="stds-info-p">
                                        <i class="stds-info-p-i">ROLL NO.</i>  <i class="stds-info-p-doti">:</i>
                                        <asp:Label ID="lbl_roll_no" runat="server"></asp:Label>
                                    </p>
                                </div>
                            </div>

                            <div class="subs-mrks-area-dv">
                                <table>
                                    <tr>
                                        <th class="{{reportCardSubS[0].Is_text_center}}">A</th>
                                        <th colspan="30" class="{{reportCardSubS[0].Is_text_center}}">SCHOLASTIC AREAS</th>
                                    </tr>
                                    <tr>
                                        <th class="{{reportCardSubS[0].Is_text_center}}">SN</th>
                                        <th class="{{reportCardSubS[0].Is_text_center}} {{reportCardSubS[0].Is_subj_code_hide}}">SUBJECT CODE</th>
                                        <th>SUBJECTS</th>
                                        <th class="{{reportCardSubS[0].Is_text_center}}" data-ng-repeat="x in reportCard" colspan="{{x.ColSpanS}}">{{x.Short_Name}}
                                            <br />
                                            <span class="{{reportCardSubS[0].Max_mark_show}}">{{x.Maximum_Marks}}</span></th>
                                        <th class="{{reportCardSubS[0].Is_text_center}}" colspan="2">{{reportCardSubS[0].grade_head_text}}
                                            <br />
                                            {{reportCardSubS[0].Full_no_of_a_subj}}</th>
                                        <%--<th class="{{reportCardSubS[0].Is_text_center}} {{reportCardSubS[0].Grade}}">GRADE</th>--%>
                                    </tr>
                                    <tr>
                                        <td colspan="2"></td>
                                        <td class="{{reportCardSubS[0].Is_text_center}}" data-ng-repeat="x in reportCard_MG">{{x.TextShow}}</td>
                                    </tr>
                                    <tr data-ng-repeat="x in reportCardSubS track by $index">
                                        <td class="{{reportCardSubS[0].Is_text_center}}">{{$index+1}}</td>
                                        <td class="{{reportCardSubS[0].Is_text_center}} {{reportCardSubS[0].Is_subj_code_hide}}">{{x.Subject}}</td>
                                        <td>{{x.Subject_name}}</td>
                                        <td class="{{reportCardSubS[0].Is_text_center}}" data-ng-repeat="item in x.MySubjectMarkItem track by $index">{{item.Marks}}</td>
                                        <td class="{{reportCardSubS[0].Is_text_center}}">{{x.Total_marks}}</td>
                                        <td class="{{reportCardSubS[0].Is_text_center}} {{x.Grade}}">{{x.Grade}}</td>
                                    </tr>

                                    <tr>
                                        <th class="{{reportCardSubS[0].Is_text_center}}">B</th>
                                        <th colspan="30" class="{{reportCardSubS[0].Is_text_center}}">CO-SCHOLASTIC ACTIVITIES </th>
                                    </tr>
                                    <tr data-ng-repeat="x in reportCardCoScholastic track by $index">
                                        <td class="{{reportCardSubS[0].Is_text_center}}">{{$index+1}}</td>
                                        <td class="{{reportCardSubS[0].Is_text_center}}">{{x.Subject_Name}}</td>
                                        <td class="{{reportCardSubS[0].Is_text_center}}" data-ng-repeat="item in x.MySubjectMarkItemCoScholestic track by $index" colspan="{{item.ColspaN}}">{{item.Marks}}</td>
                                        <td class="{{reportCardSubS[0].Is_text_center}}" colspan="2">-</td>
                                    </tr>
                                </table>
                            </div>


                            <div class="ttl-tbls-wprs">
                                <table>
                                    <tr>
                                        <td>OVERALL OBTAINED MARKS</td>
                                        <td class="{{reportCardSubS[0].Is_text_center}}">{{ttlNos[0].Overall_obt_marks}}/{{ttlNos[0].Overall_full_marks}}   <%--({{ttlNos[0].ObtMarkInWord}})--%></td>
                                    </tr>
                                    <tr>
                                        <td>OVERALL {{ttlNos[0].Mark_type}}</td>
                                        <td class="{{reportCardSubS[0].Is_text_center}}">{{ttlNos[0].Overall_percents}} <span class="{{reportCardSubS[0].Overall_obt_mark}}">(%)</span></td>
                                    </tr>
                                    <tr>
                                        <td>OVERALL GRADE</td>
                                        <td class="{{reportCardSubS[0].Is_text_center}}">{{ttlNos[0].Grade}}</td>
                                    </tr>
                                </table>
                            </div>

                            <div class="remrk-promot-to ">
                                <div class="prcntage-remrk-dv prcntage-remrk-dv-custom v-false {{reportCardSubS[0].Prcnt_remark}}" style="margin-top: {{reportCardSubS[0].Percent_remark_area_margn}}">
                                    <p>Remark :</p>
                                    <span>{{ttlNos[0].P_remark}}</span>
                                </div>

                                <div class="promot-dvcustom" style="margin-top: {{reportCardSubS[0].Percent_remark_area_margn}}">
                                    <p>Promoted to : <span class="{{promoT[0].ShowStatuS}}">Session : {{promoT[0].Session}}, Class :  {{promoT[0].Class_name}}, Section :  {{promoT[0].Section}}</span></p>
                                </div>
                            </div>

                            <div class="session-startts-dv">
                                <p>Next Session Begins on: 04/04/2024</p>
                            </div>

                            <%--<div class="remarks-rp  {{reportCardSubS[0].Remarkss}}">
                                <p>REMARKS : {{reportCardSubS[0].Remarkss}}</p>
                            </div>--%>

                            <%--<div class="subs-mrks-area-rght-dv">
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
                                        <tr class="{{reportCardSubS[0].Overall_obt_mark}} {{reportCardSubS[0].Is_overall_grade_show}}">
                                            <td>OVERALL GRADE</td>
                                            <td>{{ttlNos[0].Grade}}</td>
                                        </tr>
                                        <tr class=" {{reportCardSubS[0].Is_attandance_show}}">
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
                                        </tr>
                                        <tr class=" {{reportCardSubS[0].SpecialNote}}">
                                            <td class="spcial-notes" colspan="2">SPECIAL NOTES (IF ANY)</td>
                                        </tr>
                                    </table>
                                </div>

                                <div class="qr-dvs v-false {{reportCardSubS[0].qr_div_true}}">
                                    <img src="{{reportCardSubS[0].qr_code_Show}}" />
                                </div> 
                            </div>--%>





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


                                <div class="instruction-tbls-custom lft-50">
                                    <table>
                                        <tr>
                                            <th colspan="2" class="txt-center">SCHOLASTIC AREAS</th>
                                        </tr>
                                        <tr>
                                            <td>GRADE</td>
                                            <td>MARKS</td>
                                        </tr>
                                        <tr data-ng-repeat="x in reportCardmarkRange">
                                            <td>{{x.Grade}}</td>
                                            <td>{{x.Lower_Range}}-{{x.Upper_Range}}</td>

                                        </tr>
                                    </table>
                                </div>
                                <div class="instruction-tbls-custom rght-50">
                                    <table>
                                        <tr>
                                            <th colspan="2" class="txt-center">CO-SCHOLASTIC AREAS</th>
                                        </tr>
                                        <tr>
                                            <td>GRADE</td>
                                            <td>MARKS</td>
                                        </tr>
                                        <tr data-ng-repeat="x in reportCardgeadeRemark">
                                            <td>{{x.Grade}}</td>
                                            <td>{{x.Remark}}</td>
                                        </tr>
                                    </table>
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

                $http.get("api/report-card.asmx/fetch_report_card", { params: { "Session_id": session_id, "Class_id": class_id, "Admission_no": admission_no, "Branch_id": branch_id, "Term_id": term_id } }).then(function (response) {
                    $scope.reportCard = response.data;
                })
                $http.get("api/report-card.asmx/fetch_report_card_M_G", { params: { "Session_id": session_id, "Class_id": class_id, "Admission_no": admission_no, "Branch_id": branch_id, "Term_id": term_id } }).then(function (response) {
                    $scope.reportCard_MG = response.data;
                })


                $http.get("api/report-card.asmx/fetch_rp_card_subjects", { params: { "Session_id": session_id, "Class_id": class_id, "Admission_no": admission_no, "Branch_id": branch_id, "Term_id": term_id } }).then(function (response) {
                    $scope.reportCardSubS = response.data;
                    $("#intsLoader").addClass("hidden");


                    if ($scope.reportCardSubS == "") {
                    }
                    else {
                        ////========================GRAPH
                        $http.get("api/report-card.asmx/fetch_rp_card_graph", { params: { "Session_id": session_id, "Class_id": class_id, "Admission_no": admission_no, "Branch_id": branch_id, "Term_id": term_id } }).then(function (response) {
                            $scope.reportGraphs = response.data;
                        })


                        ////========================OverAll No.
                        $http.get("api/report-card.asmx/fetch_rp_card_total_no", { params: { "Session_id": session_id, "Class_id": class_id, "Admission_no": admission_no, "Branch_id": branch_id, "Term_id": term_id } }).then(function (response) {
                            $scope.ttlNos = response.data;
                        })
                    }
                })


                $http.get("api/report-card.asmx/fetch_rp_card_marks_range", { params: { "Session_id": session_id, "Branch_id": branch_id, "Term_id": term_id } }).then(function (response) {
                    $scope.reportCardmarkRange = response.data;
                })


                ////========================Get Scholastic Data
                $http.get("api/report-card.asmx/fetch_report_card_coscholastic", { params: { "Session_id": session_id, "Class_id": class_id, "Admission_no": admission_no, "Branch_id": branch_id, "Term_id": term_id } }).then(function (response) {
                    $scope.reportCardCoScholastic = response.data;
                })


                $http.get("api/report-card.asmx/fetch_rp_card_promoted_to", { params: { "Session_id": session_id, "Class_id": class_id, "Admission_no": admission_no, "Branch_id": branch_id } }).then(function (response) {
                    $scope.promoT = response.data;
                })


                $http.get("api/report-card.asmx/fetch_rp_card_grade_remark").then(function (response) {
                    $scope.reportCardgeadeRemark = response.data;
                })

                $http.get("api/report-card.asmx/fetch_rp_card_signature", { params: { "Session_id": session_id, "Class_id": class_id, "Admission_no": admission_no, "Branch_id": branch_id } }).then(function (response) {
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
