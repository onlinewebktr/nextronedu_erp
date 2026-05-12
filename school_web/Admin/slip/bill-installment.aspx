<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bill-installment.aspx.cs" Inherits="school_web.Admin.slip.bill_installment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Slip</title>
    <script src="../../js/jquery-1.10.2.min.js"></script>
    <link href="css/receipt-a5-inst.css" rel="stylesheet" />
    <script src="../../assets/Angular/angular.min.js"></script>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="css/receipt-a5-inst.css" rel="stylesheet" type="text/css" />');
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
        <div class="main" data-ng-app="RpCardApp" data-ng-controller="RpCardAppCtrl">
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
                                    <asp:RadioButton ID="rdo_office_copy" onclick="myFunction('2')" runat="server" GroupName="aA" Text="Parent's Copy" />
                                </div>
                                <div class="chckbx--span">
                                    <asp:RadioButton ID="rdo_student_copy" onclick="myFunction('3')" runat="server" GroupName="aA" Text="School Copy" />
                                </div>
                            </div>
                        </div>
                        <div class="noPrint" style="float: right">
                            <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" CssClass="print-btn" runat="server" ToolTip="Print"></asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>

            <%--<div class="ints-loader-wpr" id="intsLoader">
                <div class="ints-loader-wpr-inr">
                    <div class="ints-loader">
                        <p class="ints-loader-txt">
                            <img src="../../assets/images/icons/loader-ico.gif" class="ints-loader-img" />
                            <asp:Label ID="lblmessage" runat="server"></asp:Label>
                        </p>
                    </div>
                </div>
            </div>--%>

            <div class="printpagewprs">
                <div id="tblPrintIQ" runat="server">
                    <div class="mainautot">
                        <div class="printlefts" runat="server" id="officecopY">
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
                                            <%--<div class="student-info-row">
                                                <div class="student_left-p-info slipnodv">
                                                    <p class="slipnonam">R.No</p>
                                                    <i>:</i>
                                                    
                                                </div>
                                            </div>--%>

                                            <div class="student-info-row">
                                                <div class="student_left-p-info">
                                                    <p>R.No</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_slipno" runat="server" Font-Bold="true"></asp:Label>
                                                </div>

                                                <div class="student_left-p-info">
                                                    <p>Date</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_paymentdate" runat="server" Font-Bold="true"></asp:Label>
                                                </div>
                                            </div>


                                            <div class="student-info-row">
                                                <div class="student_left-p-info">
                                                    <p>Name</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_studentname" runat="server"></asp:Label>
                                                </div>
                                                <div class="student_left-p-info">
                                                    <p>Session</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_session" runat="server"></asp:Label>
                                                </div>
                                                <div class="student_left-p-info" style="display: none">
                                                    <p>Month</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_months" runat="server" class="lowercase" Text="January,February" Font-Bold="true"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="student-info-row">
                                                <div class="student_left-p-info" style="width: 100%;">
                                                    <p style="width: 16%;">F. Name</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_fathername" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="student-info-row">
                                                <div class="student_left-p-info">
                                                    <p>Class</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_class" runat="server"></asp:Label>
                                                </div>
                                                <div class="student_left-p-info">
                                                    <p>Section</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_section" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="student-info-row">
                                                <div class="student_left-p-info">
                                                    <p>Adm.No.</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_aadmissionno" runat="server"></asp:Label>
                                                </div>
                                                <div class="student_left-p-info">
                                                    <p>Roll No.</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_rollno" runat="server"></asp:Label>
                                                </div>
                                            </div>

                                            <div class="student-info-row">
                                                <div class="student_left-p-info">
                                                    <p>Mode</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_mode" class="txttrfrmingert" Text="Cash" runat="server"></asp:Label>
                                                </div>
                                                <div class="student_left-p-info">
                                                    <p>Issued By</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_issued_by" Text="suraj kumar" runat="server"></asp:Label>
                                                </div>
                                            </div>


                                            <div class="student-info-row" runat="server" id="payChekOnline">
                                                <div class="student_left-p-info">
                                                    <p id="paytrnoname" runat="server">Cheque No.</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_tr_no" class="txttrfrmingert" Text="" runat="server"></asp:Label>
                                                </div>
                                                <div class="student_left-p-info">
                                                    <p>Tr. Date</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_tr_date" Text="04/02/2024" runat="server"></asp:Label>
                                                </div>
                                            </div>

                                            <div class="student-info-row" runat="server" id="payBankName">
                                                <div class="student_left-p-info slipnodv">
                                                    <p class="slipnonam">Bank Name</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_bank_name" Text="Bank of India" runat="server"></asp:Label>
                                                </div>
                                            </div> 
                                        </div>

                                        <div class="pay_particular">
                                            <table class="table table-bordered table-striped">
                                                <tr>
                                                    <th class="txtcntr" style="width: 35px">Sr. No.</th>
                                                    <th>Installment</th>
                                                    <th>Fee Head</th>
                                                    <th class="txtcntr">To Pay</th>
                                                    <th class="txtcntr">Paid</th>
                                                </tr>
                                                <tr data-ng-repeat="x in reportFee">
                                                    <td class="txtcntr {{x.RowspaNHide}}" rowspan="{{x.rowspan}}">{{x.SlNo}}<%--{{$index+1}}--%></td>
                                                    <td class="txtsizetd {{x.RowspaNHide}}" rowspan="{{x.rowspan}}">{{x.Installment_name}}<br />
                                                        <span class="{{x.IsmonthNameHide}}">({{x.Month_name}})</span></td>
                                                    <td class="txtsizetd">{{x.Fee_head}}</td>
                                                    <td class="txtcntr">{{x.Net_payble}}</td>
                                                    <td class="txtcntr">{{x.Paid_amount}}</td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td class="fntbold txtrght" colspan="2">Total</td>
                                                    <td class="txtcnter fntbold">{{ totalpaybleF }}</td>
                                                    <td class="txtcnter fntbold">{{ totalPaidF }}</td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td class="fntbold txtrght" colspan="2">Remaining Amount</td>
                                                    <td class="txtcnter">-</td>
                                                    <td class="txtcnter fntbold">{{DuesAmtF}}</td>
                                                </tr>
                                            </table>
                                            <p class="remarksp" id="rmrkdV" runat="server">
                                                Remarks :
                                                <asp:Label ID="lbl_remarks" runat="server"></asp:Label>
                                            </p>
                                        </div>



                                        <div class="footer_auth_sig">
                                            <p>Signature <span></span></p>
                                        </div>

                                        <div class="notediv">
                                            <p>[Note : If any correction, Please consult to fee counter]</p>
                                            <p>[Note : Deposited Money is not refundable.]</p>
                                        </div>
                                        <p class="whichcopypp">PARENT'S COPY</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="printrghts" id="studentcopY">
                            <div class="certificate-wpr1">
                                <asp:Image ID="Image3" runat="server" class="watermarklogos" />
                                <div class="certificate-wpr2">
                                    <div class="mainwith" id="studentcopYInr">
                                        <div class="heading" id="ContentHeader1" runat="server">
                                            <div class="leftlogoheading">
                                                <asp:Image ID="Image1" runat="server" />
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

                                        <div class="studentdetails">
                                            <div class="student-info-row">
                                                <div class="student_left-p-info">
                                                    <p>R.No</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_slipno1" runat="server" Font-Bold="true"></asp:Label>
                                                </div>

                                                <div class="student_left-p-info">
                                                    <p>Date</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_paymentdate1" runat="server" Font-Bold="true"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="student-info-row">
                                                <div class="student_left-p-info">
                                                    <p>Name</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_studentname1" runat="server"></asp:Label>
                                                </div>
                                                <div class="student_left-p-info">
                                                    <p>Session</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_session1" runat="server"></asp:Label>
                                                </div>
                                                <div class="student_left-p-info" style="display: none">
                                                    <p>Month</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_months1" runat="server" class="lowercase" Text="January,February" Font-Bold="true"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="student-info-row">
                                                <div class="student_left-p-info" style="width: 100%;">
                                                    <p style="width: 16%;">F. Name</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_fathername1" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="student-info-row">
                                                <div class="student_left-p-info">
                                                    <p>Class</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_class1" runat="server"></asp:Label>
                                                </div>
                                                <div class="student_left-p-info">
                                                    <p>Section</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_section1" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="student-info-row">
                                                <div class="student_left-p-info">
                                                    <p>Adm.No.</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_aadmissionno1" runat="server"></asp:Label>
                                                </div>
                                                <div class="student_left-p-info">
                                                    <p>Roll No.</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_rollno1" runat="server"></asp:Label>
                                                </div>
                                            </div>

                                            <div class="student-info-row">
                                                <div class="student_left-p-info">
                                                    <p>Mode</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_mode1" class="txttrfrmingert" runat="server"></asp:Label>
                                                </div>
                                                <div class="student_left-p-info">
                                                    <p>Issued By</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_issued_by1" runat="server"></asp:Label>
                                                </div>
                                            </div>


                                            <div class="student-info-row" runat="server" id="payChekOnline1">
                                                <div class="student_left-p-info">
                                                    <p id="paytrnoname1" runat="server">Cheque No.</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_tr_no1" class="txttrfrmingert" Text="" runat="server"></asp:Label>
                                                </div>
                                                <div class="student_left-p-info">
                                                    <p>Tr. Date</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_tr_date1" runat="server"></asp:Label>
                                                </div>
                                            </div>

                                            <div class="student-info-row" runat="server" id="payBankName1">
                                                <div class="student_left-p-info slipnodv">
                                                    <p class="slipnonam">Bank Name</p>
                                                    <i>:</i>
                                                    <asp:Label ID="lbl_bank_name1" Text="" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="pay_particular">
                                            <table class="table table-bordered table-striped">
                                                <tr>
                                                    <th class="txtcntr" style="width: 35px">Sr. No.</th>
                                                    <th>Installment</th>
                                                    <th>Fee Head</th>
                                                    <th class="txtcntr">To Pay</th>
                                                    <th class="txtcntr">Paid</th>
                                                </tr>
                                                <tr data-ng-repeat="x in reportFee">
                                                    <td class="txtcntr {{x.RowspaNHide}}" rowspan="{{x.rowspan}}">{{x.SlNo}}<%--{{$index+1}}--%></td>
                                                    <td class="txtsizetd {{x.RowspaNHide}}" rowspan="{{x.rowspan}}">{{x.Installment_name}}
                                                        <br />
                                                        <span class="{{x.IsmonthNameHide}}">({{x.Month_name}})</span></td>
                                                    <td class="txtsizetd">{{x.Fee_head}}</td>
                                                    <td class="txtcntr">{{x.Net_payble}}</td>
                                                    <td class="txtcntr">{{x.Paid_amount}}</td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td class="fntbold txtrght" colspan="2">Total</td>
                                                    <td class="txtcnter fntbold">{{ totalpaybleF }}</td>
                                                    <td class="txtcnter fntbold">{{ totalPaidF }}</td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td class="fntbold txtrght" colspan="2">Remaining Amount</td>
                                                    <td class="txtcnter">-</td>
                                                    <td class="txtcnter fntbold">{{DuesAmtF}}</td>
                                                </tr>
                                            </table>
                                            <p class="remarksp" id="rmrkdV1" runat="server">
                                                Remarks :
                                                <asp:Label ID="lbl_remarks1" runat="server"></asp:Label>
                                            </p>
                                        </div>



                                        <div class="footer_auth_sig">
                                            <p>Signature <span></span></p>
                                        </div>

                                        <div class="notediv">
                                            <p>[Note : If any correction, Please consult to fee counter.]</p>
                                            <p>[Note : Deposited Money is not refundable.]</p>
                                        </div>
                                        <p class="whichcopypp">SCHOOL COPY</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <asp:HiddenField ID="hd_print_type" runat="server" />




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
                    OfficecopY.className += " showd";

                    var StudentcopY = document.getElementById("studentcopY");
                    StudentcopY.className += " showd";

                    var StudentcopYInr = document.getElementById("studentcopYInr");
                    OfficecopY.classList.remove("hidden");
                    StudentcopY.classList.remove("hidden");
                    StudentcopYInr.classList.remove("extrClass");
                }
                else if (PrintType == "2") {
                    var OfficecopY = document.getElementById("officecopY");
                    OfficecopY.className += " showd";

                    var StudentcopY = document.getElementById("studentcopY");
                    StudentcopY.className += " hidden";

                    var studentcopYInr = document.getElementById("studentcopYInr");
                    OfficecopY.classList.remove("hidden");
                    StudentcopY.classList.remove("showd");
                    studentcopYInr.classList.remove("extrClass");
                }
                else {
                    var OfficecopY = document.getElementById("officecopY");
                    OfficecopY.className += " hidden";

                    var StudentcopY = document.getElementById("studentcopY");
                    StudentcopY.className += " showd";

                    var studentcopYInr = document.getElementById("studentcopYInr");
                    studentcopYInr.className += " extrClass";
                    OfficecopY.classList.remove("showd");
                    StudentcopY.classList.remove("hidden");
                }
            }
        </script>


        <asp:HiddenField ID="hd_session" runat="server" />
        <asp:HiddenField ID="hd_class_id" runat="server" />
        <asp:HiddenField ID="hd_admission_no" runat="server" />
        <asp:HiddenField ID="hd_slip_no" runat="server" />
        <asp:HiddenField ID="hd_month_name_hide" runat="server" />

        <script type="text/javascript">
            var app = angular.module('RpCardApp', []);
            app.controller('RpCardAppCtrl', function ($scope, $http) {
                var session = $("#<%=hd_session.ClientID%>").val();
                var class_id = $("#<%=hd_class_id.ClientID%>").val();
                var admission_no = $("#<%=hd_admission_no.ClientID%>").val();
                var slip_no = $("#<%=hd_slip_no.ClientID%>").val();
                var is_month_name_hide = $("#<%=hd_month_name_hide.ClientID%>").val();

                $http.get("webServices/bill-installment.asmx/fetch_bill_detail", { params: { "Session": session, "Class_id": class_id, "Admission_no": admission_no, "Slip_no": slip_no, "Is_month_name_hide": is_month_name_hide } }).then(function (response) {
                    $scope.reportFee = response.data;
                    calculateDifference();
                })

                // Function to calculate the difference
                function calculateDifference() {
                    const totalpayble = $scope.reportFee && $scope.reportFee.length > 0
                        ? $scope.reportFee[$scope.reportFee.length - 1].TotalPayble || 0
                        : 0;

                    const TtlPaid = $scope.reportFee && $scope.reportFee.length > 0
                        ? $scope.reportFee[$scope.reportFee.length - 1].TtlPaid || 0
                        : 0;

                    $scope.totalpaybleF = totalpayble; // Store the difference in scope

                    $scope.totalPaidF = TtlPaid; // Store the difference in scope

                    $scope.DuesAmtF = totalpayble - TtlPaid; // Store the difference in scope
                }
            });


            <%--function messge(msg) {
                $("#<%=lblmessage.ClientID%>").text(msg);
                $('.ints-loader-wpr').hide().slideDown(0);
            }--%>
        </script>
    </form>
</body>
</html>
