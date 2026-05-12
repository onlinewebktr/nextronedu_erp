<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="print-demand-bill.aspx.cs" Inherits="school_web.Admin.slip.print_demand_bill" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Demand Bill</title>
    <link href="css/demand-bill.css" rel="stylesheet" />
    <script src="../../assets/js/jquery-1.10.2.min.js"></script>
    <script src="../../assets/Angular/angular.min.js"></script>

    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="css/demand-bill.css" rel="stylesheet" type="text/css" />');
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
                    <div class="invoice-inr-sec {{x.Dues_zero}} {{x.WhatDiv}}" data-ng-repeat="x in reportDemandBll track by $index">
                        <div class="printheadse333">
                            <div class="invoice-wpr {{x.WhatDivTB}}">
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
                                    <p class="demandbllhead">Demand Bill (Session : {{x.Session_name}})</p>
                                    <div class="nammarggg">
                                        <div class="name1sec1sts">
                                            <div class="namehead">
                                                <p class="nameheadp">Admission No. <span class="spdottt">:</span> </p>
                                                <asp:Label ID="lbl_admission_no" runat="server" CssClass="nameheadll" Text="{{x.Registration_id}}"></asp:Label>
                                            </div>
                                            <div class="namehead">
                                                <p class="nameheadp">Student's Name <span class="spdottt">:</span> </p>
                                                <asp:Label ID="lbl_student_name" runat="server" CssClass="nameheadll" Text="{{x.Student_name}}"></asp:Label>
                                            </div>
                                            <div class="namehead">
                                                <p class="nameheadp">Section <span class="spdottt">:</span></p>
                                                <asp:Label ID="Label4" runat="server" CssClass="nameheadll" Text="{{x.Section}}"></asp:Label>
                                            </div>
                                            <div class="namehead">
                                                <p class="nameheadp">Father's Name<span class="spdottt">:</span></p>
                                                <asp:Label ID="lbl_father_name" runat="server" CssClass="nameheadll" Text="{{x.Father_name}}"></asp:Label>
                                            </div>
                                            <div class="namehead">
                                                <p class="nameheadp">Month <span class="spdottt">:</span> </p>
                                                <asp:Label ID="Label5" runat="server" CssClass="nameheadll" Text="{{x.Months}}"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="name1sec">
                                            <div class="namehead">
                                                <p class="nameheadprght">Date <span class="spdottt">:</span></p>
                                                <asp:Label ID="Label2" runat="server" CssClass="nameheadll" Style="padding: 0px 10px 0px 10px;" Text="{{x.Print_date}}"></asp:Label>
                                            </div>
                                            <div class="namehead">
                                                <p class="nameheadprght">Class<span class="spdottt">:</span></p>
                                                <asp:Label ID="lbl_class_sec" runat="server" CssClass="nameheadll" Style="padding: 0px 10px 0px 10px;" Text="{{x.Course_Name}}"></asp:Label>
                                            </div>
                                            <div class="namehead">
                                                <p class="nameheadprght">Roll No. <span class="spdottt">:</span></p>
                                                <asp:Label ID="Label3" runat="server" CssClass="nameheadll" Text="{{x.Rollnumber}}"></asp:Label>
                                            </div>
                                            <div class="namehead">
                                                <p class="nameheadprght">Mobile No. <span class="spdottt">:</span></p>
                                                <asp:Label ID="Label1" runat="server" CssClass="nameheadll" Text="{{x.Mobile_no}}"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="administration-paragraph ">
                                        <table>
                                            <tr>
                                                <th colspan="8" style="text-align: center;">Fee Details</th>
                                            </tr>
                                            <tr>
                                                <th class="txtcntr">Sl No.</th>
                                                <th>Particular</th>
                                                <th class="txtcntr">Amount</th>
                                            </tr>
                                            <tr data-ng-repeat="item in x.MyFeeDetailsItem track by $index" class="{{item.rowsHidden}}">
                                                <td class="txtcntr">{{$index+1}}</td>
                                                <td>{{item.Content}}</td>
                                                <td class="txtcntr">{{item.Amount}}</td>
                                            </tr>

                                            <tr class="{{x.lateFine_zero}}">
                                                <td colspan="2" class="txt-right txt-bold">Late Fine</td>
                                                <td class="txt-bold txtcntr">{{x.FIne_amt}}</td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" class="txt-right txt-bold">Total</td>
                                                <td class="txt-bold txtcntr">{{x.Total_dues_amt}}</td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    <p class="amt-in-word">{{x.Inword_number}}</p>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    <p class="remarks-note-p">Parents are requested to clear the tuition fee, other charges and dues before {{x.PayDate}}</p>

                                                    <p class="remarks-note-p" runat="server" visible="false" id="reqHin">माता-पिता से अनुरोध है कि कृपया {{x.PayDate}} से पहले ट्यूशन फीस, अन्य शुल्क और बकाया राशि का भुगतान कर दें।</p>
                                                </td>
                                            </tr>
                                            <tr id="moreInfo" runat="server" visible="false">
                                                <td colspan="3">
                                                    <p class="remarks-note-p">Get a discount of ₹200 on the school fee and ₹200 on the hostel fee if the full payment is made before {{x.PayDate}}.</p>
                                                </td>
                                            </tr>
                                        </table>

                                        <div class="sig-dv">
                                            <div class="sig-left">
                                                <p class="sig-ps">Signature (Seal)</p>
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
        <asp:HiddenField ID="hd_section" runat="server" /> 
        <asp:HiddenField ID="hd_chcked" runat="server" />
        <asp:HiddenField ID="hd_month" runat="server" />
        <asp:HiddenField ID="hd_paydate" runat="server" />
        <asp:HiddenField ID="hd_session_name" runat="server" />
        <asp:HiddenField ID="hd_with" runat="server" />
        <asp:HiddenField ID="hd_std_school_copy" runat="server" />
         <asp:HiddenField ID="hd_student_type" runat="server" />
        <script type="text/javascript">
            var app = angular.module('RpCardApp', []);
            app.controller('RpCardAppCtrl', function ($scope, $http) {
                var session_id = $("#<%=hd_session_id.ClientID%>").val();
                var class_id = $("#<%=hd_class_id.ClientID%>").val();
                var admission_no = $("#<%=hd_admission_no.ClientID%>").val();
                var branch_id = $("#<%=hd_branch_id.ClientID%>").val();
                var section = $("#<%=hd_section.ClientID%>").val();
                var checked = $("#<%=hd_chcked.ClientID%>").val();
                var month_name = $("#<%=hd_month.ClientID%>").val();
                var paydate = $("#<%=hd_paydate.ClientID%>").val();
                var session_name = $("#<%=hd_session_name.ClientID%>").val();
                var bill_type = $("#<%=hd_with.ClientID%>").val();
                var std_school_copy = $("#<%=hd_std_school_copy.ClientID%>").val();
                var std_type = $("#<%=hd_student_type.ClientID%>").val();
                messge("Please Wait...");
                $("#intsLoader").removeClass("hidden");
                $http.get("webServices/demandBillN.asmx/fetch_demand_bill", { params: { "Session_id": session_id, "Class_id": class_id, "Admission_no": admission_no, "Branch_id": branch_id, "Section": section, "Checked": checked, "Month_name": month_name, "Paydate": paydate, "Session_name": session_name, "Bill_type": bill_type, "Std_school_copy": std_school_copy, "Std_type": std_type } }).then(function (response) {
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
