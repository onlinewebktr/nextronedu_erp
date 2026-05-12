<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="student-label.aspx.cs" Inherits="school_web.Admin.slip.student_label" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Label</title>
    <link href="css/label.css" rel="stylesheet" />
    <script src="../../assets/js/jquery-1.10.2.min.js"></script>
    <script src="../../assets/Angular/angular.min.js"></script>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="css/label.css" rel="stylesheet" type="text/css" />');
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
<body data-ng-app="RpLabelApp" data-ng-controller="RpLabelAppCtrl">
    <form id="form1" runat="server">
        <div class="main">
            <div class="prnt-btn-sec" runat="server" id="printBtns">
                <div class="prnt-btn-wpr">
                    <div class="print-btn-sec">
                        <div class="noPrint" style="float: left">
                            <asp:Button ID="Button1" CssClass="back-btn" runat="server" OnClick="Button1_Click" />
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
                <div class="mainautot">
                    <div class="mainwith" data-ng-repeat="x in reportLabels">
                        <div class="lbl-wprs">
                            <p class="lbl-wprs-session-p"><span>Session : </span>{{x.Session}}</p>
                            <p class="lbl-wprs-name-p"><span>Name : </span><i>{{x.Student_name}}</i> </p>
                            <p class="lbl-wprs-std-p"><span>Std : </span><i>{{x.Class_name}}</i> </p>
                            <p class="lbl-wprs-roll-p"><span>Roll No. : </span><i>{{x.Roll_number}}</i></p>
                            <p class="lbl-wprs-adm-p"><span>Adm No. : </span><i>{{x.Admission_serial_number}}</i></p>
                            <p class="lbl-wprs-fthr-p"><span>Father's Name : </span><i>{{x.Father_name}}</i></p>
                            <p class="lbl-wprs-add-p"><span>Address : </span><i>{{x.Address}}</i></p>
                            <p class="lbl-wprs-mob-p"><span>Mobile No. : </span><i>{{x.Mobile_number}}</i></p>
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <asp:HiddenField ID="hd_type" runat="server" />
        <asp:HiddenField ID="hd_session_id" runat="server" />
        <asp:HiddenField ID="hd_class_id" runat="server" />
        <asp:HiddenField ID="hd_admission_no" runat="server" />
        <asp:HiddenField ID="hd_section" runat="server" />
        <asp:HiddenField ID="hd_print_type" runat="server" />
        <script type="text/javascript">
            var app = angular.module('RpLabelApp', []);
            app.controller('RpLabelAppCtrl', function ($scope, $http) {

                var session_id = $("#<%=hd_session_id.ClientID%>").val();
                var class_id = $("#<%=hd_class_id.ClientID%>").val();
                var admission_no = $("#<%=hd_admission_no.ClientID%>").val();
                var section = $("#<%=hd_section.ClientID%>").val();
                var type = $("#<%=hd_print_type.ClientID%>").val();
                var check_type = $("#<%=hd_type.ClientID%>").val();
                messge("Please Wait...");
                $("#intsLoader").removeClass("hidden");
                if (check_type == "1") {
                    $http.get("webServices/labels.asmx/fetch_report_details_of_labels", { params: { "Session_id": session_id, "Class_id": class_id, "Admission_no": admission_no, "Section": section, "Type": type } }).then(function (response) {
                        $scope.reportLabels = response.data;
                        $("#intsLoader").addClass("hidden");
                    })
                }
                else { 
                    $http.get("webServices/labels.asmx/fetch_report_details_of_labels_check", { params: { "Session": session_id, "Admission_no": admission_no } }).then(function (response) {
                        $scope.reportLabels = response.data;
                        $("#intsLoader").addClass("hidden");
                    })
                }
            });


            function messge(msg) {
                $("#<%=lblmessage.ClientID%>").text(msg);
                $('.ints-loader-wpr').hide().slideDown(0);
            }
        </script>
    </form>
</body>
</html>
