<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="print-admit-card-three-shift-a5.aspx.cs" Inherits="school_web.Examination_Admin.slip.print_admit_card_three_shift_a5" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Admit Card</title> 
    <link href="assets/css/AdmitCardThree.css" rel="stylesheet" />
    <script src="../../assets/js/jquery-1.10.2.min.js"></script>
    <script src="../../assets/Angular/angular.min.js"></script>


    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="assets/css/AdmitCardThree.css" rel="stylesheet" type="text/css" />');
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
            <div class="prnt-btn-sec">
                <div class="prnt-btn-wpr">
                    <div class="print-btn-sec">
                        <div class="noPrint" style="float: left">
                            <asp:LinkButton ID="lnk_back" OnClick="lnk_back_Click" class="back-btn" runat="server"></asp:LinkButton>
                        </div>
                        <div class="noPrint {{reportAdmitS[0].PrintBTN}}" style="float: right">
                            <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" CssClass="print-btn" runat="server" ToolTip="Print"></asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>

            <div class="ints-loader-wpr" id="intsLoader">
                <div class="ints-loader-wpr-inr">
                    <div class="ints-loader">
                        <p class="ints-loader-txt">
                            <img src="../../assets/images/icons/loader-ico.gif" class="ints-loader-img" />
                            <asp:Label ID="lblmessage" runat="server"></asp:Label>
                        </p>
                    </div>
                </div>
            </div>



            <div id="tblPrintIQ" runat="server">
                <div class="printpagewprs {{reportAdmitS[0].MaiNpageWidth}}">
                    <div class="invoice-inr-sec {{x.OneAdmitCardWdth}} {{x.WhatDiv}}" data-ng-repeat="x in reportAdmitS track by $index">
                        <div class="printheadse333">
                            <div class="invoice-wpr {{x.WhatDivTB}}">
                                <div class="printpage-sec-main {{x.OneAdmitCardHeight}}">
                                    <div class="headdivv  {{x.MySchoolDetailsItem[0].Content_header}}">
                                        <div class="printlogo4455">
                                            <asp:Image ID="schoollogo" runat="server" ImageUrl="{{x.MySchoolDetailsItem[0].LogoSchool}}" class="printlogo" />
                                        </div>
                                        <div class="schoolnameheadin">
                                            <asp:Label ID="lbl_school_name" CssClass="informatchild22-lab" Text="{{x.MySchoolDetailsItem[0].School_name}}" runat="server"></asp:Label>
                                            <asp:Label ID="lbl_under" CssClass="informatchild22-pp33 {{x.MySchoolDetailsItem[0].UnderShow}}" runat="server" Text="Under the aegis of Delhi Public School Society, New Delhi "></asp:Label>
                                            <asp:Label ID="lbl_affilation_no" CssClass="informatchild22-pp33 pp33italic {{x.MySchoolDetailsItem[0].Affiliation_no}}" runat="server" Text="{{x.MySchoolDetailsItem[0].Affiliation_no}}"></asp:Label>
                                            <asp:Label ID="lbl_address" CssClass="informatchild22-pp33 pp33italic" Text="{{x.MySchoolDetailsItem[0].Address}}" runat="server"></asp:Label>
                                            <asp:Label ID="lbl_mobileno_emailid" CssClass="informatchild22-pp33 pp33italic" Text="{{x.MySchoolDetailsItem[0].Mobile_no_email}}" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="image-headers {{x.MySchoolDetailsItem[0].Header_templete}}">
                                        <img src="{{x.MySchoolDetailsItem[0].Header_templete}}" />
                                    </div>
                                    <div class="acardd">
                                        <asp:Label ID="Label4" CssClass="admitcarterm1" runat="server" Text="Admit Card <br> {{x.Exam_name}} - {{x.MySchoolDetailsItem[0].SessionA5}}"></asp:Label>
                                    </div>
                                    <div class="{{x.duesclassdesabled2}}"></div>
                                    <div class="nammarggg">
                                        <div class="stdinfodv  {{x.stdInfoWdth}}">
                                            <div class="stdinfodv1sts {{x.stdInfoWdthLft}}">
                                                <div class="namehead">
                                                    <p class="nameheadp">Admission No. <span class="spdottt">:</span> </p>
                                                    <asp:Label ID="lbl_admission_no" runat="server" CssClass="nameheadll" Text="{{x.Registration_id}}"></asp:Label>
                                                </div>
                                                <div class="namehead">
                                                    <p class="nameheadp">Name of Student<span class="spdottt">:</span> </p>
                                                    <asp:Label ID="lbl_student_name" runat="server" CssClass="nameheadll" Text="{{x.Student_name}}"></asp:Label>
                                                </div>
                                                <div class="namehead">
                                                    <p class="nameheadp">Father's Name<span class="spdottt">:</span></p>
                                                    <asp:Label ID="lbl_father_name" runat="server" CssClass="nameheadll" Text="{{x.Father_name}}"></asp:Label>
                                                </div>
                                                <div class="namehead">
                                                    <p class="nameheadp">Mother's Name<span class="spdottt">:</span></p>
                                                    <asp:Label ID="Label1" runat="server" CssClass="nameheadll" Text="{{x.Mother_name}}"></asp:Label>
                                                </div>
                                            </div>

                                            <div class="stdinfodv2sec {{x.stdInfoWdthRght}}">
                                                <div class="namehead">
                                                    <p class="nameheadprght">Roll No. <span class="spdottt">:</span></p>
                                                    <asp:Label ID="Label3" runat="server" CssClass="nameheadll" Text="{{x.Roll_no}}"></asp:Label>
                                                </div>
                                                <div class="namehead">
                                                    <p class="nameheadprght">Class<span class="spdottt">:</span></p>
                                                    <asp:Label ID="lbl_class_sec" runat="server" CssClass="nameheadll" Style="padding: 0px 10px 0px 10px;" Text="{{x.Class_name}}"></asp:Label>
                                                </div>
                                                <div class="namehead">
                                                    <p class="nameheadprght">Section <span class="spdottt">:</span></p>
                                                    <asp:Label ID="Label2" runat="server" CssClass="nameheadll" Style="padding: 0px 10px 0px 10px;" Text="{{x.Section_name}}"></asp:Label>
                                                </div>
                                                <div class="namehead">
                                                    <p class="nameheadprght">D.O.B. <span class="spdottt">:</span></p>
                                                    <asp:Label ID="lbl_dob" runat="server" CssClass="nameheadll" Text="{{x.Subject_DOB}}"></asp:Label>
                                                </div>
                                            </div>
                                            <p class="dues-msg-p {{x.Dues_message_show}}">Dues till {{x.Dues_month_name}} {{x.Dues_year}}, Amount Rs. <span>{{x.Dues_amt}}</span></p>
                                        </div>
                                        <div class="studimggr">
                                            <asp:Image ID="studentlogo" runat="server" ImageUrl="{{x.Student_img}}" class="studimggrt {{x.Student_img}}" />
                                            <p class="affixPhototxt {{x.Std_dummy_imgs}}">Affix a passport-size recent photograph.</p>
                                        </div>
                                    </div>


                                    <div class="administration-paragraph">
                                        <table class="{{x.MySchoolDetailsItem[0].Programe}}">
                                            <tr>
                                                <th colspan="8" style="text-align: center;">EXAMINATION TIME TABLE </th>
                                            </tr>
                                            <tr>
                                                <th></th>
                                                <th class="txtcentr">1ST SITTING<br />
                                                    <span class="nobreak">({{x.Shift1_start}} TO {{x.Shift1_end}})</span> </th>
                                                <th class="txtcentr">2ND SITTING
                                                    <br />
                                                    <span class="nobreak">({{x.Shift2_start}} TO {{x.Shift2_end}})</span></th>
                                                <th class="txtcentr">3RD SITTING
                                                    <br />
                                                    <span class="nobreak">({{x.Shift3_start}} TO {{x.Shift3_end}})</span></th>
                                            </tr>
                                            <tr> 
                                                <th>DATE</th> 
                                                <th>SUBJECT</th>
                                                <th>SUBJECT</th>
                                                 <th>SUBJECT</th>
                                            </tr>
                                            <tr data-ng-repeat="item in x.MyRoutineDetailsItem track by $index">
                                                <%--<td>{{$index+1}}</td>--%>
                                                <td>{{item.Exam_date}}
                                                    <br /> ({{item.Day}})
                                                </td> 
                                                <td>{{item.Subject_name1}}</td>
                                                <td>{{item.Subject_name2}}</td>
                                                <td>{{item.Subject_name3}}</td>
                                            </tr>
                                        </table>

                                        <div class="courses-sec33">
                                            <table>
                                                <tr>
                                                    <td style="background: #F4F2C9;"><b>General Instructions : </b>{{x.Remarks}}</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lbl_imp_note" runat="server"></asp:Label></td>
                                                </tr>
                                            </table>
                                            <p class="duesStatusBtm {{x.DuesshowBottom}}"><i>Fee Status : </i> <span></span></p>
                                        </div>

                                        <div class="{{x.duesclassdesabled}}">{{x.Desabletext}}</div>
                                        <div class="sig-dv {{x.SignWirhSetting}}">
                                            <div class="sig-left">
                                                <div class="lft-sig-img-dv {{x.LftSignName}}">
                                                    <img src="{{x.LftSign}}" class="lft-sig-img {{x.LftSign}}" />
                                                </div>
                                                <p class="sig-ps {{x.LftSignName}}">{{x.LftSignName}}</p>
                                            </div>
                                            <div class="sig-left">
                                                <div class="cntr-sig-img-dv {{x.MdlSignName}}">
                                                    <img src="{{x.MdlSign}}" class="cntr-sig-img {{x.MdlSign}}" />
                                                </div>
                                                <p class="sig-ps {{x.MdlSignName}}">{{x.MdlSignName}}</p>
                                            </div>
                                            <div class="sig-left">
                                                <div class="rght-sig-img-dv {{x.RghtSignName}}">
                                                    <img src="{{x.RghtSign}}" class="rght-sig-img {{x.RghtSign}}" />
                                                </div>
                                                <p class="sig-ps {{x.RghtSignName}}">{{x.RghtSignName}}</p>
                                            </div>
                                        </div>
                                        <div class="sig-dv {{x.SignAuto}}">
                                            <div class="sig-left wdth30">
                                                <div class="lft-sig-img-dv">
                                                    <img src="{{x.Class_teacher_sig}}" class="lft-sig-img {{x.Class_teacher_sig}}" />
                                                </div>
                                                <p class="sig-ps">{{x.MySchoolDetailsItem[0].First_sign_text}}</p>
                                            </div>
                                            <div class="sig-left wdth40">
                                                <div class="cntr-sig-img-dv {{x.MySchoolDetailsItem[0].isMiddl_sign_hide}}">
                                                    <img src="{{x.Examinee_sig}}" class="cntr-sig-img {{x.Examinee_sig}}" />
                                                </div>
                                                <p class="sig-ps {{x.MySchoolDetailsItem[0].isMiddl_sign_hide}}" id="mddle_signdesig">EXAMINATION INCHARGE</p>
                                            </div>
                                            <div class="sig-left wdth30">
                                                <div class="rght-sig-img-dv">
                                                    <img src="{{x.Principal_sig}}" class="rght-sig-img {{x.Principal_sig}}" />
                                                </div>
                                                <p class="sig-ps">PRINCIPAL</p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>





        <asp:HiddenField ID="hd_session_id" runat="server" />
        <asp:HiddenField ID="hd_class_id" runat="server" />
        <asp:HiddenField ID="hd_admission_no" runat="server" />
        <asp:HiddenField ID="hd_branch_id" runat="server" />
        <asp:HiddenField ID="hd_term_id" runat="server" />
        <asp:HiddenField ID="hd_section" runat="server" />
        <asp:HiddenField ID="hd_session_name" runat="server" />
        <asp:HiddenField ID="hd_exam_id" runat="server" />
        <asp:HiddenField ID="hd_checked" runat="server" />
        <asp:HiddenField ID="hd_print_from" runat="server" />
        <asp:HiddenField ID="hd_user_type" runat="server" />
        <asp:HiddenField ID="hd_page" runat="server" />
        <script type="text/javascript">
            var app = angular.module('RpCardApp', []);
            app.controller('RpCardAppCtrl', function ($scope, $http) {
                var session_id = $("#<%=hd_session_id.ClientID%>").val();
                var class_id = $("#<%=hd_class_id.ClientID%>").val();
                var admission_no = $("#<%=hd_admission_no.ClientID%>").val();
                var branch_id = $("#<%=hd_branch_id.ClientID%>").val();
                var term_id = $("#<%=hd_term_id.ClientID%>").val();
                var section = $("#<%=hd_section.ClientID%>").val();
                var session_name = $("#<%=hd_session_name.ClientID%>").val();
                var exam_id = $("#<%=hd_exam_id.ClientID%>").val();
                var checkedStd = $("#<%=hd_checked.ClientID%>").val();
                var UserType = $("#<%=hd_user_type.ClientID%>").val();
                var PageNo = $("#<%=hd_page.ClientID%>").val();
                messge("Please Wait...");
                $("#intsLoader").removeClass("hidden");
                $http.get("webService/admit-card-third-shift.asmx/fetch_admit_cards", { params: { "Session_id": session_id, "Class_id": class_id, "Admission_no": admission_no, "Branch_id": branch_id, "Term_id": term_id, "Section": section, "Session_name": session_name, "Exam_id": exam_id, "CheckedStd": checkedStd, "UserType": UserType, "PageNo": PageNo } }).then(function (response) {
                    $scope.reportAdmitS = response.data;
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
