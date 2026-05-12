<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="student-qr.aspx.cs" Inherits="school_web.Admin.slip.student_qr" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Payment Details</title>
    <link href="css/qr-code.css" rel="stylesheet" /> 
    <script src="../../assets/js/jquery-1.10.2.min.js"></script>
    <script src="../../assets/Angular/angular.min.js"></script>

    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="css/qr-code.css" rel="stylesheet" type="text/css" />');
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
                            <img src="../../Examination_Admin/slip/assets/images/loader-ico.gif" class="ints-loader-img" />
                            <asp:Label ID="lblmessage" runat="server"></asp:Label>
                        </p>
                    </div>
                </div>
            </div>
            <div id="tblPrintIQ" runat="server">
                <div class="invoice-inr-sec">
                    <div class="invoice-wpr">
                        <div class="rportwprs">
                            <div class="hdr-sec">
                                <div class="hdr-log-dv">
                                    <img src="{{reportCardS[0].Logo}}" />
                                </div>
                                <div class="hdr-content-dv">
                                    <h2 class="hdr-school-name">{{reportCardS[0].SchoolName}}</h2>
                                    <p class="hdr-school-add">{{reportCardS[0].SchoolAddress}}</p>
                                    <p class="hdr-report-name">Student List with QR</p>
                                </div>
                            </div> 

                            <div class="fee-details-dv">
                                <table>
                                    <tr>
                                        <th>#</th>
                                        <th>Student Name</th>
                                        <th>Adm. No.</th>
                                        <th>Class</th>
                                        <th>Section</th>
                                        <th>Roll</th> 
                                        <th>Father's Name</th>
                                        <th>Mother's Name</th> 
                                        <th></th>
                                    </tr>
                                    <tr data-ng-repeat="x in reportCardS">
                                        <td>{{$index+1}}</td>
                                        <td>{{x.name}}</td>
                                        <td>{{x.Admission_no}}</td>
                                        <td>{{x.sclass}}</td> 
                                        <td>{{x.sec}}</td>
                                        <td>{{x.roll_no}}</td>
                                        <td>{{x.father_name}}</td>
                                        <td>{{x.Mothername}}</td>
                                        <td><img src="{{x.QrCode}}" style="width: 100px;" /></td>
                                    </tr> 
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div> 


        <asp:HiddenField ID="hd_admission_no" runat="server" />
        <asp:HiddenField ID="hd_session_id" runat="server" />
        <asp:HiddenField ID="hd_class_id" runat="server" />
        <asp:HiddenField ID="hd_branch" runat="server" />
        <asp:HiddenField ID="hd_section" runat="server" />
        <asp:HiddenField ID="hd_type" runat="server" />
        <script type="text/javascript">
            var app = angular.module('RpCardApp', []);
            app.controller('RpCardAppCtrl', function ($scope, $http) {
                var admission_no = $("#<%=hd_admission_no.ClientID%>").val();
                var session_id = $("#<%=hd_session_id.ClientID%>").val();
                var class_id = $("#<%=hd_class_id.ClientID%>").val();
                var branch_id = $("#<%=hd_branch.ClientID%>").val();
                var sections = $("#<%=hd_section.ClientID%>").val();
                var idType = $("#<%=hd_type.ClientID%>").val();


                messge("Please Wait...");
                $("#intsLoader").removeClass("hidden");
                $http.get("webServices/studentQR.asmx/fetch_QR_student", { params: { "Session_id": session_id, "Class_id": class_id, "Sections": sections, "Branch_id": branch_id, "IdType": idType, "Admission_no": admission_no } }).then(function (response) {
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
