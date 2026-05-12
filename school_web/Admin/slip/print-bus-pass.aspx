<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="print-bus-pass.aspx.cs" Inherits="school_web.Admin.slip.print_bus_pass" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print</title>
    <link href="css/bus-pass.css" rel="stylesheet" />
    <script src="../../assets/js/jquery-1.10.2.min.js"></script>
    <script src="../../assets/Angular/angular.min.js"></script>

    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="css/bus-pass.css" rel="stylesheet" type="text/css" />');
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
                <div class="print-dv-sec">
                    <div data-ng-repeat="x in reportDemandBll" class="invoice-inr-sec {{x.WhatDiv}}">
                        <div class="printheadse333">
                            <div class="invoice-wpr">
                                <div class="printpage-sec-main">
                                    <div class="headerImage {{x.IsHeaderImageShow}}">
                                        <img src="{{x.Header_images}}" />
                                    </div>
                                    <div class="headdivv {{x.IsHeaderContentShow}}">
                                        <div class="printlogo4455">
                                            <asp:Image ID="schoollogo" runat="server" ImageUrl="{{x.LogoSchool}}" class="printlogo" />
                                        </div>
                                        <div class="schoolnameheadin">
                                            <asp:Label ID="lbl_school_name" CssClass="informatchild22-lab" Text="{{x.School_name}}" runat="server"></asp:Label>
                                            <asp:Label ID="lbl_address" CssClass="informatchild22-pp33 pp33italic" Text="{{x.Address}}" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="nammarggg">

                                        <h2 class="buspass-h"><span>Bus Pass</span></h2>
                                        <div class="name1sec1sts">
                                            <div class="namehead">
                                                <p class="nameheadp">Name of Student <span class="spdottt">:</span> </p>
                                                <asp:Label ID="lbl_student_name" runat="server" CssClass="nameheadll" Text="{{x.Student_name}}"></asp:Label>
                                            </div>
                                            <div class="namehead">
                                                <p class="nameheadp">Father's Name<span class="spdottt">:</span></p>
                                                <asp:Label ID="lbl_father_name" runat="server" CssClass="nameheadll" Text="{{x.Father_name}}"></asp:Label>
                                            </div>
                                            <div class="namehead" style="width: 60%;">
                                                <p class="nameheadprght" style="width: 140px;">Class<span class="spdottt">:</span></p>
                                                <asp:Label ID="lbl_class_sec" runat="server" CssClass="nameheadll" Style="padding: 0px 10px 0px 10px;" Text="{{x.Course_Name}}"></asp:Label>
                                            </div>
                                            <div class="namehead" style="width: 40%;">
                                                <p class="nameheadprght" style="width: 74px;">Roll No. <span class="spdottt">:</span></p>
                                                <asp:Label ID="Label3" runat="server" CssClass="nameheadll" Text="{{x.Rollnumber}}"></asp:Label>
                                            </div>
                                            <div class="namehead">
                                                <p class="nameheadp">Admission No. <span class="spdottt">:</span> </p>
                                                <asp:Label ID="lbl_admission_no" runat="server" CssClass="nameheadll" Text="{{x.Registration_id}}"></asp:Label>
                                            </div>
                                            <div class="namehead">
                                                <p class="nameheadp">Stoppege <span class="spdottt">:</span> </p>
                                                <asp:Label ID="Label1" runat="server" CssClass="nameheadll" Text="{{x.Boarding_point}}"></asp:Label>
                                            </div>
                                            <div class="namehead" style="width: 60%;">
                                                <p class="nameheadp">Contact No. <span class="spdottt">:</span> </p>
                                                <asp:Label ID="Label2" runat="server" CssClass="nameheadll" Text="{{x.Mobile_no}}"></asp:Label>
                                            </div>
                                            <div class="namehead" style="width: 40%;">
                                                <p class="nameheadp" style="width: 74px;">Bus  No. <span class="spdottt">:</span> </p>
                                                <asp:Label ID="Label4" runat="server" CssClass="nameheadll" Text="{{x.Vehicle_no}}"></asp:Label>
                                            </div>




                                            <div class="namehead" style="position: absolute; bottom: 10px; right: 0px; left: 0px;">
                                                <p class="nameheadp" style="width: 50%; padding: 0px 0px 0px 5px; font-weight: 600; letter-spacing: 0.3px; color: #000;">
                                                    Sign of Transport Incharge
                                                </p>
                                                <p class="nameheadp" style="width: 50%; text-align: right; padding: 0px 5px 0px 0px; font-weight: 600; letter-spacing: 0.3px; color: #000;">
                                                    Principal
                                                </p>
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
        <asp:HiddenField ID="hd_section" runat="server" />
        <asp:HiddenField ID="hd_chcked" runat="server" />
        <asp:HiddenField ID="hd_session_name" runat="server" />
        <script type="text/javascript">
            var app = angular.module('RpCardApp', []);
            app.controller('RpCardAppCtrl', function ($scope, $http) {
                var session_id = $("#<%=hd_session_id.ClientID%>").val();
                var class_id = $("#<%=hd_class_id.ClientID%>").val();
                var admission_no = $("#<%=hd_admission_no.ClientID%>").val();
                var section = $("#<%=hd_section.ClientID%>").val();
                var checked = $("#<%=hd_chcked.ClientID%>").val();
                var session_name = $("#<%=hd_session_name.ClientID%>").val();
                messge("Please Wait...");
                $("#intsLoader").removeClass("hidden");
                $http.get("webServices/busPass.asmx/fetch_demand_bill", { params: { "Session_id": session_id, "Class_id": class_id, "Admission_no": admission_no, "Section": section, "Checked": checked } }).then(function (response) {
                    $scope.reportDemandBll = response.data;
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
