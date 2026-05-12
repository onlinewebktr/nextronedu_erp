<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="payment-detail.aspx.cs" Inherits="school_web.Admin.slip.payment_detail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Payment Details</title>
    <link href="css/paydt.css" rel="stylesheet" />
    <script src="../../assets/js/jquery-1.10.2.min.js"></script>
    <script src="../../assets/Angular/angular.min.js"></script>

    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="css/paydt.css" rel="stylesheet" type="text/css" />');
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
                            <img src="../../Examination_Admin/slip/assets/images/loader-ico.gif" class="ints-loader-img" />
                            <asp:Label ID="lblmessage" runat="server"></asp:Label>
                        </p>
                    </div>
                </div>
            </div>
            <div id="tblPrintIQ" runat="server">
                <div class="invoice-inr-sec" data-ng-repeat="x in reportCardS track by $index">
                    <div class="invoice-wpr">
                        <div class="rportwprs" style="height: 1294px">
                            <div class="hdr-sec">
                                <div class="hdr-log-dv">
                                    <img src="{{x.Logo}}" />
                                </div>
                                <div class="hdr-content-dv">
                                    <h2 class="hdr-school-name">{{x.SchoolName}}</h2>
                                    <p class="hdr-school-add">{{x.SchoolAddress}}</p>
                                    <p class="hdr-report-name">Details of Payment ({{x.Session_name}})</p>
                                </div>
                            </div>

                            <div class="std-sec">
                                <div class="std-dt-rows">
                                    <p class="std-dt-name-frst width50">Student's Name : <span>{{x.Student_name}}</span></p>
                                    <p class="std-dt-name-frst width50">Category : <span>{{x.castCaltegory}}</span></p>
                                </div>
                                <div class="std-dt-rows">
                                    <p class="std-dt-name-frst width50">Father's Name : <span>{{x.Father_name}}</span></p>
                                    <p class="std-dt-name-frst width50">Mother's Name : <span>{{x.Mother_name}}</span></p>
                                </div>
                                <div class="std-dt-rows">
                                    <p class="std-dt-name-frst width33">Class : <span>{{x.Class_name}}</span></p>
                                    <p class="std-dt-name-frst width33">Section : <span>{{x.Section}}</span></p>
                                    <p class="std-dt-name-frst width33">Roll No. : <span>{{x.Rollnumber}}</span></p>
                                </div>
                                <div class="std-dt-rows">
                                    <p class="std-dt-name-frst width33">D.O.B. : <span>{{x.Date_of_birth}}</span></p>
                                    <p class="std-dt-name-frst width33">Blood Group : <span>{{x.Blood_group}}</span></p>
                                    <p class="std-dt-name-frst width33">Aadhar No. : <span>{{x.Aadharno}}</span></p>
                                </div>
                                <div class="std-dt-rows">
                                    <p class="std-dt-name-frst width66">Address : <span>{{x.Address}}</span></p>
                                    <p class="std-dt-name-frst width33">Pincode : <span>{{x.Pincode}}</span></p>
                                </div>
                                <div class="std-dt-rows">
                                    <p class="std-dt-name-frst width33">Adm. No. : <span>{{x.Admission_no}}</span></p>
                                    <p class="std-dt-name-frst width33">Adm. Date : <span>{{x.Date_of_admission}}</span></p>
                                    <p class="std-dt-name-frst width33">Contact No. : <span>{{x.Mobile_no}}</span></p>
                                </div>
                            </div>

                            <div class="fee-details-dv">
                                <table>
                                    <tr>
                                        <th>Months</th>
                                        <th data-ng-repeat="item in x.MyGetMonthsItem track by $index" class="txtxntr td-width">{{item.Month_name}}</th>
                                    </tr>
                                    <tr data-ng-repeat="item in x.MyGetMonthwiseFeeItem track by $index">
                                        <td>{{item.Contents}}</td>
                                        <td data-ng-repeat="itemx in item.MyGetMonthwiseFeeAmountsItem track by $index" class="txtxntr">{{itemx.Contents}}</td>
                                    </tr>
                                    <tr>
                                        <td class="trbg">Total</td>
                                        <td class="trbg txtxntr" data-ng-repeat="item in x.MyGetMonthwiseFeeTotalItem track by $index" >{{item.Contents}}</td>
                                    </tr>
                                    <tr>
                                        <td>Discount</td>
                                        <td data-ng-repeat="item in x.MyGetMonthwiseFeeTotalItem track by $index" class="txtxntr">{{item.ContentsDisc}}</td>
                                    </tr>
                                    <tr>
                                        <td>Payable</td>
                                        <td data-ng-repeat="item in x.MyGetMonthwiseFeeTotalItem track by $index" class="txtxntr">{{item.Fee_payable_amount}}</td>
                                    </tr>
                             
                                    <tr>
                                        <td>Paid</td>
                                        <td data-ng-repeat="item in x.MyGetPaidDetailsItem track by $index" class="txtxntr break-text">{{item.Paid_amt}}</td>
                                    </tr>
                                    <tr>
                                        <td>Dues</td>
                                        <td data-ng-repeat="item in x.MyGetPaidDetailsItem track by $index" class="txtxntr break-text">{{item.Duesamts}}</td>
                                    </tr>
                                     
                                    <tr>
                                        <td>Receipt No.</td>
                                        <td data-ng-repeat="item in x.MyGetPaidDetailsItem track by $index" class="txtxntr break-text fontsizs">{{item.Bill_no}}</td>
                                    </tr>
                                    <tr>
                                        <td>Receipt Date</td>
                                        <td data-ng-repeat="item in x.MyGetPaidDetailsItem track by $index" class="txtxntr break-text fontsizs">{{item.Payment_mode}}</td>
                                    </tr>
                                    <tr>
                                        <td>Payment Mode</td>
                                        <td data-ng-repeat="item in x.MyGetPaidDetailsItem track by $index" class="txtxntr break-text fontsizs">{{item.Payment_date}}</td>
                                    </tr>
                                    <tr>
                                        <td>Received By</td>
                                        <td data-ng-repeat="item in x.MyGetPaidDetailsItem track by $index" class="txtxntr break-text fontsizs">{{item.Received_by}}</td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <asp:HiddenField ID="hd_session_id" runat="server" />
        <asp:HiddenField ID="hd_session_name" runat="server" />
        <asp:HiddenField ID="hd_class_id" runat="server" />
        <asp:HiddenField ID="hd_section" runat="server" />
        <asp:HiddenField ID="hd_adm_no" runat="server" />

        <script type="text/javascript">
            var app = angular.module('RpCardApp', []);
            app.controller('RpCardAppCtrl', function ($scope, $http) {
                var session_id = $("#<%=hd_session_id.ClientID%>").val();
                var session_name = $("#<%=hd_session_name.ClientID%>").val();
                var class_id = $("#<%=hd_class_id.ClientID%>").val();
                var section = $("#<%=hd_section.ClientID%>").val();
                var adm_no = $("#<%=hd_adm_no.ClientID%>").val();

                messge("Please Wait...");
                $("#intsLoader").removeClass("hidden");
                $http.get("webServices/paymentDetails.asmx/fetch_payments_of_student", { params: { "Session_id": session_id, "Session_name": session_name, "Class_id": class_id, "Section": section, "Adm_no": adm_no } }).then(function (response) {
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
