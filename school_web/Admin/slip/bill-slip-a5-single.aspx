<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bill-slip-a5-single.aspx.cs" Inherits="school_web.Admin.slip.bill_slip_a5_single" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Slip</title>
    <script src="../../js/jquery-1.10.2.min.js"></script>
    <link href="css/receipt-a5-single.css" rel="stylesheet" />
    <script src="../../assets/Angular/angular.min.js"></script>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="css/receipt-a5-single.css" rel="stylesheet" type="text/css" />');
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
        <div class="main">
            <div class="prnt-btn-sec" runat="server" id="printBtns">
                <div class="prnt-btn-wpr">
                    <div class="print-btn-sec">
                        <div class="noPrint" style="float: left">
                            <asp:Button ID="Button1" CssClass="back-btn" runat="server" OnClick="btn_back_Click" />
                        </div>
                        <div class="chckbx-sec" id="midredio" runat="server">
                            <div class="chckbx-sec-inr">
                                <div class="chckbx--span">
                                    <asp:RadioButton ID="rdo_both" onclick="myFunction('1')" runat="server" GroupName="aA" Text="Both Copy" />
                                </div>
                                <div class="chckbx--span">
                                    <asp:RadioButton ID="rdo_office_copy" onclick="myFunction('2')" runat="server" GroupName="aA" Text="School Copy" />
                                </div>
                                <div class="chckbx--span">
                                    <asp:RadioButton ID="rdo_student_copy" onclick="myFunction('3')" runat="server" GroupName="aA" Text="Student Copy" />
                                </div>
                            </div>
                        </div>
                        <div class="noPrint" style="float: right">
                            <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" CssClass="print-btn" runat="server" ToolTip="Print"></asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>

            <div class="printpagewprs" data-ng-app="RpCardApp" data-ng-controller="RpCardAppCtrl">
                <div id="tblPrintIQ" runat="server">
                    <div class="mainautot">
                        <div class="printlefts" id="studentcopY">
                            <div class="certificate-wpr1">
                                <asp:Image ID="Image2" runat="server" class="watermarklogos" />
                                <div class="certificate-wpr2">
                                    <div class="mainwith">
                                        <div class="heading" id="ContentHeader" runat="server">
                                            <div class="leftlogoheading">
                                                <asp:Image ID="imglogo" runat="server" />
                                            </div>
                                            <div class="righttextheading">
                                                <h1 class="firm-name-h">
                                                    <asp:Label ID="lbl_heading" runat="server"></asp:Label>
                                                </h1>
                                            </div>
                                        </div>
                                        <div class="heading-template" id="TempleteHeader" runat="server" visible="false">
                                            <asp:Image ID="img_header" runat="server" />
                                        </div>

                                        <div class="studentdetails">
                                            <div class="slipHeaddv">
                                                <h2>RECEIPT</h2>
                                            </div>

                                            <div class="student-info-row">
                                                <div class="student_left-p-info">
                                                    <p>Receipt No</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_slipno" runat="server" Font-Bold="true"></asp:Label>
                                                </div>

                                                <div class="student-rght-p-info">
                                                    <p>Date</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_paymentdate" runat="server" Font-Bold="true"></asp:Label>
                                                </div>
                                            </div>


                                            <div class="student-info-row">
                                                <div class="student_left-p-info">
                                                    <p>Student's Name</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_studentname" runat="server"></asp:Label>
                                                </div>
                                                <div class="student-rght-p-info">
                                                    <p>Month</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_months" runat="server" class="lowercase" Font-Bold="true"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="student-info-row">
                                                <div class="student_left-p-info">
                                                    <p>Father's Name</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_fathername" runat="server"></asp:Label>
                                                </div>
                                                <div class="student-rght-p-info">
                                                    <p>Session</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_session" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="student-info-row">
                                                <div class="student_left-p-info">
                                                    <p>Class</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_class" runat="server"></asp:Label>
                                                </div>
                                                <div class="student-rght-p-info">
                                                    <p>Section</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_section" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="student-info-row">
                                                <div class="student_left-p-info">
                                                    <p>Admission No.</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_aadmissionno" runat="server"></asp:Label>
                                                </div>
                                                <div class="student-rght-p-info">
                                                    <p>Pay Mode</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_mode" Text="suraj kumar" runat="server"></asp:Label>
                                                </div>
                                            </div>

                                            <%--<div class="student-info-row">
                                                <div class="student_left-p-info">
                                                    <p>Mode</p>
                                                    <i>:</i>
                                                    x
                                                </div>
                                                <div class="student-rght-p-info">
                                                    <p>Issued By</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_issued_by" Text="suraj kumar" runat="server"></asp:Label>
                                                </div>
                                            </div>--%>


                                            <div class="student-info-row" runat="server" id="payChekOnline">
                                                <div class="student_left-p-info">
                                                    <p id="paytrnoname" runat="server"></p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_tr_no" class="txttrfrmingert" Text="" runat="server"></asp:Label>
                                                </div>
                                                <div class="student-rght-p-info">
                                                    <p>Tr. Date</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_tr_date" runat="server"></asp:Label>
                                                    <asp:Label ID="lbl_tr_bank" runat="server" style="width:100%;"></asp:Label>
                                                </div>
                                            </div>

                                            <%--<div class="student-info-row" runat="server" id="payBankName">
                                                <div class="student_left-p-info slipnodv">
                                                    <p class="slipnonam">Bank Name</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_bank_name" Text="Bank of India" runat="server"></asp:Label>
                                                </div>
                                            </div>--%>
                                        </div>

                                        <div class="pay_particular">
                                            <table class="table table-bordered table-striped">
                                                <thead>
                                                    <tr>
                                                        <th class="txtcnter" style="width: 40px;">S. N.</th>
                                                        <th>Particulars</th>
                                                        <th class="txtcnter">To Pay</th>
                                                        <th class="txtcnter">Paid</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td class="txtcnter">
                                                            <div style="min-height: 240px;">
                                                                <span class="incomeLeft txtcntr" data-ng-repeat="x in feeDetails">{{$index+1}}</span>
                                                            </div>
                                                        </td>
                                                        <td>
                                                            <span class="incomeLeft" data-ng-repeat="x in feeDetails">{{x.Particular}}</span>
                                                        </td>
                                                        <td class="txtcnter">
                                                            <span class="incomeLeft txtcntr" data-ng-repeat="x in feeDetails">{{x.Fee_after_disc}}</span>
                                                        </td>
                                                        <td class="txtcnter">
                                                            <span class="incomeLeft txtcntr" data-ng-repeat="x in feeDetails">{{x.Paid}}</span>
                                                        </td>
                                                    </tr>
                                                </tbody>

                                                <tr>
                                                    <td></td>
                                                    <td class="fntbold txtrght">Total</td>
                                                    <td class="txtcnter">
                                                        <asp:Label ID="lbl_ttl_to_pay" class="fntbold" runat="server"></asp:Label></td>
                                                    <td class="txtcnter">
                                                        <asp:Label ID="lbl_ttl_paid" class="fntbold" runat="server"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td colspan="3">
                                                        <asp:Label ID="lbl_amt_in_word" class="fntbold" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>






                                            <table class="table table-bordered table-striped" style="display: none">
                                                <thead>
                                                    <tr>
                                                        <th class="txtcnter">S. N.</th>
                                                        <th>Fee Head</th>
                                                        <th class="txtcnter">To Pay</th>
                                                        <th class="txtcnter">Paid</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="grd_fees" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td class="txtcnter">
                                                                    <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbl_feetype" runat="server" Text='<%#Bind("Particular") %>'></asp:Label>
                                                                </td>
                                                                <td class="txtcnter">
                                                                    <asp:Label ID="lbl_payable" runat="server" Text='<%#Bind("fee_amount") %>'></asp:Label>
                                                                </td>
                                                                <td class="txtcnter">
                                                                    <asp:Label ID="lbl_paid" runat="server" Text='<%#Bind("paid") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                                <tr>
                                                    <td></td>
                                                    <td class="fntbold txtrght">Rebate</td>
                                                    <td class="txtcnter">-</td>
                                                    <td class="txtcnter">
                                                        <asp:Label ID="lbl_discount_amt" class="fntbold" runat="server"></asp:Label></td>

                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td class="fntbold txtrght">Remaining Amount</td>
                                                    <td class="txtcnter">-</td>
                                                    <td class="txtcnter">
                                                        <asp:Label ID="lbl_remaining_amt" class="fntbold" runat="server"></asp:Label></td>
                                                </tr>
                                            </table>
                                            <p class="remarksp" id="rmrkdV" runat="server">
                                                Remarks :
                                                <asp:Label ID="lbl_remarks" runat="server"></asp:Label>
                                            </p>
                                        </div>



                                        <%--<div class="footer_auth_sig">
                                            <p>Signature <span></span></p>
                                        </div>--%>

                                        <div class="notediv">
                                            <p>1. Fee once received is non-transferable and non-refundable.</p>
                                            <p>2. Fee must be deposited before the 15th of each month.</p>
                                        </div>
                                        <p class="signbtms">Signature</p>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="printlefts" id="officecopY">
                            <div class="certificate-wpr1">
                                <asp:Image ID="Image1" runat="server" class="watermarklogos" />
                                <div class="certificate-wpr2">
                                    <div class="mainwith">
                                        <div class="heading" id="ContentHeader1" runat="server">
                                            <div class="leftlogoheading">
                                                <asp:Image ID="imglogo1" runat="server" />
                                            </div>
                                            <div class="righttextheading">
                                                <h1 class="firm-name-h">
                                                    <asp:Label ID="lbl_heading1" runat="server"></asp:Label>
                                                </h1>
                                            </div>
                                        </div>
                                        <div class="heading-template" id="TempleteHeader1" runat="server" visible="false">
                                            <asp:Image ID="img_header1" runat="server" />
                                        </div>
                                        <p class="officecopytxt">(Office Copy)</p>
                                        <div class="studentdetails">
                                            <div class="slipHeaddv">
                                                <h2>RECEIPT</h2>
                                            </div>

                                            <div class="student-info-row">
                                                <div class="student_left-p-info">
                                                    <p>Receipt No</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_slipno1" runat="server" Font-Bold="true"></asp:Label>
                                                </div>

                                                <div class="student-rght-p-info">
                                                    <p>Date</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_paymentdate1" runat="server" Font-Bold="true"></asp:Label>
                                                </div>
                                            </div>


                                            <div class="student-info-row">
                                                <div class="student_left-p-info">
                                                    <p>Student's Name</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_studentname1" runat="server"></asp:Label>
                                                </div>
                                                <div class="student-rght-p-info">
                                                    <p>Month</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_months1" runat="server" class="lowercase" Font-Bold="true"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="student-info-row">
                                                <div class="student_left-p-info">
                                                    <p>Father's Name</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_fathername1" runat="server"></asp:Label>
                                                </div>
                                                <div class="student-rght-p-info">
                                                    <p>Session</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_session1" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="student-info-row">
                                                <div class="student_left-p-info">
                                                    <p>Class</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_class1" runat="server"></asp:Label>
                                                </div>
                                                <div class="student-rght-p-info">
                                                    <p>Section</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_section1" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="student-info-row">
                                                <div class="student_left-p-info">
                                                    <p>Admission No.</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_aadmissionno1" runat="server"></asp:Label>
                                                </div>
                                                <div class="student-rght-p-info">
                                                    <p>Pay Mode</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_mode1" Text="suraj kumar" runat="server"></asp:Label>
                                                </div>
                                            </div>

                                            <%--<div class="student-info-row">
                                                <div class="student_left-p-info">
                                                    <p>Mode</p>
                                                    <i>:</i>
                                                    x
                                                </div>
                                                <div class="student-rght-p-info">
                                                    <p>Issued By</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_issued_by" Text="suraj kumar" runat="server"></asp:Label>
                                                </div>
                                            </div>--%>


                                            <div class="student-info-row" runat="server" id="payChekOnline1">
                                                <div class="student_left-p-info">
                                                    <p id="paytrnoname1" runat="server"></p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_tr_no1" class="txttrfrmingert" Text="" runat="server"></asp:Label>
                                                </div>
                                                <div class="student-rght-p-info">
                                                    <p>Tr. Date</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_tr_date1" runat="server"></asp:Label>

                                                    <asp:Label ID="lbl_tr_bank1" runat="server" style="width:100%;"></asp:Label>
                                                </div>
                                            </div>

                                            <%--<div class="student-info-row" runat="server" id="payBankName">
                                                <div class="student_left-p-info slipnodv">
                                                    <p class="slipnonam">Bank Name</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_bank_name" Text="Bank of India" runat="server"></asp:Label>
                                                </div>
                                            </div>--%>
                                        </div>

                                        <div class="pay_particular">
                                            <table class="table table-bordered table-striped">
                                                <thead>
                                                    <tr>
                                                        <th class="txtcnter" style="width: 40px;">S. N.</th>
                                                        <th>Particulars</th>
                                                        <th class="txtcnter">To Pay</th>
                                                        <th class="txtcnter">Paid</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td class="txtcnter">
                                                            <div style="min-height: 240px;">
                                                                <span class="incomeLeft txtcntr" data-ng-repeat="x in feeDetails">{{$index+1}}</span>
                                                            </div>
                                                        </td>
                                                        <td>
                                                            <span class="incomeLeft" data-ng-repeat="x in feeDetails">{{x.Particular}}</span>
                                                        </td>
                                                        <td class="txtcnter">
                                                            <span class="incomeLeft txtcntr" data-ng-repeat="x in feeDetails">{{x.Fee_after_disc}}</span>
                                                        </td>
                                                        <td class="txtcnter">
                                                            <span class="incomeLeft txtcntr" data-ng-repeat="x in feeDetails">{{x.Paid}}</span>
                                                        </td>
                                                    </tr>
                                                </tbody>

                                                <tr>
                                                    <td></td>
                                                    <td class="fntbold txtrght">Total</td>
                                                    <td class="txtcnter">
                                                        <asp:Label ID="lbl_ttl_to_pay1" class="fntbold" runat="server"></asp:Label></td>
                                                    <td class="txtcnter">
                                                        <asp:Label ID="lbl_ttl_paid1" class="fntbold" runat="server"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td colspan="3">
                                                        <asp:Label ID="lbl_amt_in_word1" class="fntbold" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>






                                            <table class="table table-bordered table-striped" style="display: none">
                                                <tr>
                                                    <td></td>
                                                    <td class="fntbold txtrght">Rebate</td>
                                                    <td class="txtcnter">-</td>
                                                    <td class="txtcnter">
                                                        <asp:Label ID="lbl_discount_amt1" class="fntbold" runat="server"></asp:Label></td>

                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td class="fntbold txtrght">Remaining Amount</td>
                                                    <td class="txtcnter">-</td>
                                                    <td class="txtcnter">
                                                        <asp:Label ID="lbl_remaining_amt1" class="fntbold" runat="server"></asp:Label></td>
                                                </tr>
                                            </table>
                                            <p class="remarksp" id="rmrkdV1" runat="server">
                                                Remarks :
                                                <asp:Label ID="lbl_remarks1" runat="server"></asp:Label>
                                            </p>
                                        </div>



                                        <%--<div class="footer_auth_sig">
                                            <p>Signature <span></span></p>
                                        </div>--%>

                                        <div class="notediv">
                                            <p>1. Fee once received is non-transferable and non-refundable.</p>
                                            <p>2. Fee must be deposited before the 15th of each month.</p>
                                        </div>
                                        <p class="signbtms">Signature</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <asp:HiddenField ID="hd_print_type" runat="server" />


        <asp:HiddenField ID="hd_session" runat="server" />
        <asp:HiddenField ID="hd_adm_no" runat="server" />
        <asp:HiddenField ID="hd_slip_no" runat="server" />


        <script type="text/javascript">
            var app = angular.module('RpCardApp', []);
            app.controller('RpCardAppCtrl', function ($scope, $http) {
                var session = $("#<%=hd_session.ClientID%>").val();
                var admission_no = $("#<%=hd_adm_no.ClientID%>").val();
                var slip_no = $("#<%=hd_slip_no.ClientID%>").val();

                // Fetch income data
                $http.get("webServices/payment-receipt.asmx/fetch_report_billing", { params: { "Session": session, "Admission_no": admission_no, "Slip_no": slip_no } })
                    .then(function (response) {
                        $scope.feeDetails = response.data;
                    });
            });

        </script>


        <script type="text/javascript">
            $(document).ready(function () {
                var PrintType = $('#<%= hd_print_type.ClientID %>').val();
                if (PrintType == "1") {
                    var OfficecopY = document.getElementById("officecopY");
                    OfficecopY.className += " showd";

                    var StudentcopY = document.getElementById("studentcopY");
                    StudentcopY.className += " showd";
                }
                else if (PrintType == "2") {
                    var OfficecopY = document.getElementById("officecopY");
                    OfficecopY.className += " showd";

                    var StudentcopY = document.getElementById("studentcopY");
                    StudentcopY.className += " hidden";
                }
                else {
                    var OfficecopY = document.getElementById("officecopY");
                    OfficecopY.className += " hidden";

                    var StudentcopY = document.getElementById("studentcopY");
                    StudentcopY.className += " showd";
                }
            });





            function myFunction(PrintType) {
                if (PrintType == "1") {
                    var OfficecopY = document.getElementById("officecopY");
                    OfficecopY.classList.remove("showd");
                    OfficecopY.classList.remove("hidden");
                    OfficecopY.className += " showd";

                    var StudentcopY = document.getElementById("studentcopY");
                    StudentcopY.classList.remove("showd");
                    StudentcopY.classList.remove("hidden");
                    StudentcopY.className += " showd";

                    //var StudentcopYInr = document.getElementById("studentcopYInr");
                    //OfficecopY.classList.remove("hidden");
                    //StudentcopY.classList.remove("hidden");
                    //StudentcopYInr.classList.remove("extrClass");
                }
                else if (PrintType == "2") {
                    var OfficecopY = document.getElementById("officecopY");
                    OfficecopY.classList.remove("showd");
                    OfficecopY.classList.remove("hidden");
                    OfficecopY.className += " showd";

                    var StudentcopY = document.getElementById("studentcopY");
                    StudentcopY.classList.remove("showd");
                    StudentcopY.classList.remove("hidden");
                    StudentcopY.className += " hidden";

                    //var studentcopYInr = document.getElementById("studentcopYInr");
                    //OfficecopY.classList.remove("hidden");
                    //StudentcopY.classList.remove("showd");
                    //studentcopYInr.classList.remove("extrClass");
                }
                else { 
                    var OfficecopY = document.getElementById("officecopY");
                    OfficecopY.classList.remove("showd");
                    OfficecopY.classList.remove("hidden");
                    OfficecopY.className += " hidden";

                    var StudentcopY = document.getElementById("studentcopY");
                    StudentcopY.classList.remove("showd");
                    StudentcopY.classList.remove("hidden");
                    StudentcopY.className += " showd";

                    //var studentcopYInr = document.getElementById("studentcopYInr");
                    //studentcopYInr.className += " extrClass";
                    //OfficecopY.classList.remove("showd");
                    //StudentcopY.classList.remove("hidden");
                }
            }
        </script>
    </form>
</body>
</html>
