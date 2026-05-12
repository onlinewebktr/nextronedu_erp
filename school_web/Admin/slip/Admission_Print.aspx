<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admission_Print.aspx.cs" Inherits="school_web.Admin.slip.Admission_Print" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admission Slip</title>
    <link href="../../Examination_Admin/slip/assets/css/admitcardS.css" rel="stylesheet" />
    <script src="../../assets/js/jquery-1.10.2.min.js"></script>

    <script src="../../assets/Angular/angular.min.js"></script>
    <script src="../../assets/Angular/angular-sanitize.min.js"></script>

    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="../../Examination_Admin/slip/assets/css/admitcardS.css" rel="stylesheet" type="text/css" />');
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
                                <img src="../../assets/images/icons/loader-ico.gif" class="ints-loader-img" />
                                <asp:Label ID="lblmessage" runat="server"></asp:Label>
                            </p>
                        </div>
                    </div>
                </div>
                <div id="tblPrintIQ" runat="server">
                    <div class="invoice-inr-sec" data-ng-repeat="x in reportAdmitS track by $index">
                        <div class="printheadse333">
                            <div class="invoice-wpr">
                                <div class="printpage-sec-main_admision">
                                    <div class="headdivv {{x.MySchoolDetailsItem[0].Content_header}}">
                                        <div class="printlogo4455">
                                            <asp:Image ID="schoollogo" runat="server" ImageUrl="{{x.MySchoolDetailsItem[0].LogoSchool}}" class="printlogo" />
                                        </div>
                                        <div class="schoolnameheadin">
                                            <asp:Label ID="lbl_school_name" CssClass="informatchild22-lab" Text="{{x.MySchoolDetailsItem[0].School_name}}" runat="server"></asp:Label>
                                            <asp:Label ID="lbl_under" CssClass="informatchild22-pp33 {{x.MySchoolDetailsItem[0].UnderShow}}" runat="server" Text="Under the aegis of Delhi Public School Society, New Delhi"></asp:Label>
                                            <asp:Label ID="lbl_affilation_no" CssClass="informatchild22-pp33 pp33italic" runat="server" Text="{{x.MySchoolDetailsItem[0].Affiliation_no}}"></asp:Label>
                                            <asp:Label ID="lbl_address" CssClass="informatchild22-pp33 pp33italic" Text="{{x.MySchoolDetailsItem[0].Address}}" runat="server"></asp:Label>
                                            <asp:Label ID="lbl_mobileno_emailid" CssClass="informatchild22-pp33 pp33italic" Text="{{x.MySchoolDetailsItem[0].Mobile_no_email}}" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="image-headers {{x.MySchoolDetailsItem[0].Header_templete}}">
                                        <img src="{{x.MySchoolDetailsItem[0].Header_templete}}" />
                                    </div>
                                    <div class="acardd">
                                        <asp:Label ID="Label6" CssClass="admitcarterm1" runat="server" Text="ADMISSION SLIP- {{x.Session_name}}"></asp:Label>
                                    </div>

                                    <div class="nammarggg">
                                        <div class="name1sec1sts">
                                            <div class="namehead">
                                                <p class="nameheadp">STUDENT'S NAME <span class="spdottt">:</span> </p>
                                                <asp:Label ID="lbl_student_name" runat="server" CssClass="nameheadll" Text="{{x.Student_name}}"></asp:Label>
                                            </div>
                                            <div class="namehead">
                                                <p class="nameheadp">DATE OF BIRTH <span class="spdottt">:</span></p>
                                                <asp:Label ID="lbl_dob" runat="server" CssClass="nameheadll" Text="{{x.Subject_DOB}}"></asp:Label>
                                            </div>
                                            <div class="namehead">
                                                <p class="nameheadp">MOTHER'S NAME <span class="spdottt">:</span></p>
                                                <asp:Label ID="Label1" runat="server" CssClass="nameheadll" Text="{{x.Mother_name}}"></asp:Label>
                                            </div>
                                            <div class="namehead">
                                                <p class="nameheadp">FATHER'S NAME <span class="spdottt">:</span></p>
                                                <asp:Label ID="lbl_father_name" runat="server" CssClass="nameheadll" Text="{{x.Father_name}}"></asp:Label>
                                            </div>
                                            <div class="namehead">
                                                <p class="nameheadp">DATE OF REG. <span class="spdottt">:</span></p>
                                                <asp:Label ID="lbl_doa_reg_admissionserialnumber" runat="server" CssClass="nameheadll" Text="{{x.dateofadmission}}"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="name1sec">
                                            <div class="namehead">
                                                <p class="nameheadp">ADMISSION NO. <span class="spdottt">:</span> </p>
                                                <asp:Label ID="lbl_admission_no" runat="server" CssClass="nameheadll" Text="{{x.Registration_id}}"></asp:Label>
                                            </div>
                                            <div class="namehead">
                                                <p class="nameheadp">CLASS <span class="spdottt">:</span></p>
                                                <asp:Label ID="lbl_class_sec" runat="server" CssClass="nameheadll" Style="padding: 0px 10px 0px 10px;" Text="{{x.Class_name}}"></asp:Label>
                                            </div>
                                            <div class="namehead">
                                                <p class="nameheadp">SECTION<span class="spdottt">:</span></p>
                                                <asp:Label ID="Label5" runat="server" CssClass="nameheadll" Style="padding: 0px 10px 0px 10px;" Text="{{x.Section_name}}"></asp:Label>
                                            </div>
                                            <div class="namehead">
                                                <p class="nameheadp">ROLL NO. <span class="spdottt">:</span></p>
                                                <asp:Label ID="Label3" runat="server" CssClass="nameheadll" Text="{{x.Roll_no}}"></asp:Label>
                                            </div>
                                            <div class="namehead">
                                                <p class="nameheadp">CATEGORY <span class="spdottt">:</span></p>
                                                <asp:Label ID="Label2" runat="server" CssClass="nameheadll" Text="{{x.cast}}"></asp:Label>
                                            </div>
                                            <%--<div class="namehead">
                                            <p class="nameheadp">PARENT'S SIGNATURE <span class="spdottt">:</span></p>
                                        </div>--%>
                                        </div>
                                        <div class="studimggr">
                                            <asp:Image ID="studentlogo" runat="server" ImageUrl="{{x.Student_img}}" class="studimggrt {{x.Student_img}}" />
                                            <p class="affixPhototxt {{x.Std_dummy_imgs}}">Affix a passport-size recent photograph.</p>
                                        </div>
                                    </div>
                                    <div class="administration-paragraph">
                                        <div class="courses-sec33">
                                            <table>
                                                <tr>
                                                    <td><b>IMPORTANT NOTE  : </b><p data-ng-bind-html="x.Remarks" style="text-transform: none!important;line-height: 24px!important;"></p></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lbl_imp_note" runat="server"></asp:Label></td>
                                                </tr>
                                            </table>
                                        </div>


                                        <div class="sig-dv">
                                            <div class="sig-left">
                                            </div>
                                            <div class="sig-left">
                                            </div>
                                            <div class="sig-left_p">
                                                <div class="rght-sig-img-dv">
                                                    <img src="{{x.MySigDetailsItem[0].Principal_sig}}" class="rght-sig-img {{x.MySigDetailsItem[0].Principal_sig}}" />
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
            <asp:HiddenField ID="hd_session_id" runat="server" />
            <asp:HiddenField ID="hd_admission_no" runat="server" />
            <asp:HiddenField ID="hd_print_from" runat="server" />
            <script type="text/javascript">
                var app = angular.module('RpCardApp', ['ngSanitize']);
                app.controller('RpCardAppCtrl', function ($scope, $http) {
                    var session_id = $("#<%=hd_session_id.ClientID%>").val();
                    var admission_no = $("#<%=hd_admission_no.ClientID%>").val();
                    messge("Please Wait...");
                    $("#intsLoader").removeClass("hidden");
                    $http.get("webServices/admitCard.asmx/fetch_admission_cards", { params: { "Session_id": session_id,"Admission_no": admission_no } }).then(function (response) {
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
