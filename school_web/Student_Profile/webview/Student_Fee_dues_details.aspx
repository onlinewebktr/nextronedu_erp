<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Student_Fee_dues_details.aspx.cs" Inherits="school_web.Student_Profile.webview.Student_Fee_dues_details" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Student Fee Ledger</title>

    <link rel="preconnect" href="https://fonts.googleapis.com" />
    <link href="https://fonts.googleapis.com/css2?family=Inter:wght@400;500;600;700;800&display=swap" rel="stylesheet" />
    <script src="../../assets/Angular/angular.min.js"></script>
    <script src="../../assets/js/jquery-1.10.2.min.js"></script>
    <style>
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }

        body {
            background: #f3f6fb;
            font-family: 'Inter',sans-serif;
            padding: 10px;
            color: #13254b;
        }

        .fee-ledger-card {
            width: 100%;
            max-width: 1200px;
            margin: auto;
            background: #fff;
            border: 1px solid #dfe5ef;
            border-radius: 18px;
            overflow: hidden;
            box-shadow: 0 4px 18px rgba(0,0,0,0.05);
        }

        /* HEADER */

        .ledger-header {
            display: flex;
            justify-content: space-between;
            align-items: center;
            padding: 24px 26px 18px;
            border-bottom: 5px solid #0b2d72;
        }

        .header-left {
            display: flex;
            align-items: center;
            gap: 18px;
        }

        .logo-box {
            width: 60px;
            height: 60px;
            border-radius: 50%;
            background: #0b2d72;
            display: flex;
            align-items: center;
            justify-content: center;
            color: #fff;
            font-size: 31px;
        }

        .title-box h1 {
            font-size: 22px;
            font-weight: 800;
            color: #0b2559;
            line-height: 1;
            letter-spacing: .5px;
        }

        .title-box p {
            margin-top: 8px;
            font-size: 14px;
            color: #5f6d8d;
            font-weight: 600;
        }

        .session-box {
            min-width: 270px;
            border: 2px solid #dfe4ee;
            border-radius: 14px;
            padding: 8px 10px;
            display: flex;
            align-items: center;
            gap: 7px;
        }

        .session-icon {
            font-size: 15px;
            color: #0b2d72;
        }

        .session-text small {
            display: block;
            font-size: 16px;
            color: #66738f;
            font-weight: 600;
            margin-bottom: 6px;
        }

        .session-text h2 {
            font-size: 18px;
            color: #0b2559;
            font-weight: 800;
        }

        /* TABLE */

        .table-wrap {
            padding: 5px;
            overflow: auto;
        }

        table {
            width: 100%;
            border-collapse: collapse;
            overflow: hidden;
            border-radius: 12px;
        }

        thead th {
            background: #062b72;
            color: #fff;
            padding: 10px 4px;
            font-size: 14px;
            font-weight: 700;
            border-right: 1px solid rgba(255, 255, 255, 0.25);
        }

            thead th:last-child {
                border-right: none;
            }

        tbody td {
            border: 1px solid #e1e6ef;
            padding: 15px 5px;
            font-size: 13px;
            font-weight: 500;
            background: #fff;
        }

        .month-cell {
            width: 170px;
            font-weight: 800;
            color: #17284f;
            position: relative;
            padding-left: 10px !important;
            text-align: center;
        }

            .month-cell::before {
                content: '';
                position: absolute;
                left: 0px;
                top: 10%;
                width: 4px;
                height: 80%;
                border-radius: 10px;
            }

        .april::before {
            background: #3478f6;
        }

        .may::before {
            background: #37c6c0;
        }

        .june::before {
            background: #8d63ff;
        }

        .july::before {
            background: #ff5da8;
        }

        .august::before {
            background: #ff9f1a;
        }

        .september::before {
            background: #5bc95b;
        }

        .october::before {
            background: #3478f6;
        }

        .november::before {
            background: #8d63ff;
        }

        .december::before {
            background: #37c6c0;
        }

        .january::before {
            background: #ff9f1a;
        }

        .february::before {
            background: #5bc95b;
        }

        .march::before {
            background: #3478f6;
        }

        .fee-head {
            color: #1b2c54;
        }

        .amount {
            color: #1c56d6;
            font-weight: 600;
            text-align: center;
        }

        .discount {
            color: #f70087;
            font-weight: 600;
            text-align: center;
        }

        .paid {
            color: #0a9a48;
            font-weight: 600;
            text-align: center;
        }

        .due {
            color: #ff2b2b;
            font-weight: 600;
            text-align: center;
        }

        /* TOTAL */

        tfoot td {
            background: #052a72;
            color: #fff;
            padding: 5px 5px;
            font-size: 13px;
            font-weight: 800;
            border-right: 1px solid rgba(255, 255, 255, 0.15);
        }

            tfoot td:last-child {
                border-right: none;
            }

        .total-title {
            display: flex;
            align-items: center;
            gap: 14px;
        }

            .total-title span {
                font-size: 34px;
            }

        .total-amount {
            color: #69a9ff;
            text-align: center;
        }

        .total-discount {
            color: #ffa31a;
            text-align: center;
        }

        .total-paid {
            color: #4de27f;
            text-align: center;
        }

        .total-due {
            color: #ff4c4c;
            text-align: center;
        }

        /* FOOTER */

        .ledger-footer {
            display: flex;
            justify-content: space-between;
            align-items: center;
            padding: 0 26px 20px;
            color: #78859f;
            font-size: 10px;
            font-weight: 400;
        }

        .generated {
            display: flex;
            align-items: center;
            gap: 10px;
        }

        /* RESPONSIVE */

        @media(max-width:992px) {

            .ledger-header {
                flex-direction: column;
                gap: 20px;
                align-items: flex-start;
            }


            .session-box {
                width: 100%;
            }
        }

        .hidden {
            display: none;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div class="fee-ledger-card" data-ng-app="RpCardApp" data-ng-controller="RpCardAppCtrl">
            <!-- HEADER -->

            <div id="YesData">
                <div class="ledger-header">
                    <div class="header-left">
                        <div class="title-box">
                            <h1>Fee Ledger</h1>
                            <p>Academic Year :  {{ledgerS[0].Session_name}} ({{ledgerS[0].Month_Range}})</p>
                        </div>
                    </div>
                </div>

                <!-- TABLE -->


                <div class="table-wrap">
                    <table>
                        <thead>
                            <tr>
                                <th>📅 Month</th>
                                <th style="text-align: left;">📄 Fees Head</th>
                                <th style="background: #1c56d6;">💰 Fees (₹)</th>
                                <th style="background: #f70087;">⚙ Dis. (₹)</th>
                                <th style="background: #0a9a48;">✔ Paid (₹)</th>
                                <th style="background: #ff2b2b;">⚖ Dues (₹)</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr data-ng-repeat="x in ledgerS">
                                <td class="month-cell {{x.MonthsLower}} {{x.IsnxtRowHide}}" rowspan="{{x.RowSpan}}">{{x.FirstThreeLetters}}</td>
                                <td class="fee-head">{{x.Content}}</td>
                                <td class="amount">{{x.Amount}}</td>
                                <td class="discount">{{x.Disc_amount}}</td>
                                <td class="paid">{{x.Prev_paid}}</td>
                                <td class="due">{{x.Dues_amt}}</td>
                            </tr>
                        </tbody>
                        <tfoot>
                            <tr>
                                <td colspan="2">
                                    <div class="total-title">
                                        <span>🔖</span>
                                        TOTAL
                                    </div>
                                </td>
                                <td class="total-amount" style="background: #1c56d6; color: #fff;">{{totalAmount}}
                                </td>
                                <td class="total-discount" style="background: #f70087; color: #fff;">{{totalDiscount}}
                                </td>
                                <td class="total-paid" style="background: #0a9a48; color: #fff;">{{totalPaid}}
                                </td>
                                <td class="total-due" style="background: #ff2b2b; color: #fff;">{{totalDue}}
                                </td>
                            </tr>
                        </tfoot>
                    </table>
                </div>

                <!-- FOOTER -->

                <div class="ledger-footer">
                    <div>
                        All amounts are in ₹ (INR)
                    </div>
                    <div class="generated">
                        📅 Generated on: <asp:Label ID="lbl_date" runat="server"></asp:Label>
                    </div>
                </div>
            </div>


            <div id="noDataSection" class="hidden" style="text-align: center; padding: 30px;">
                <img src="https://cdn-icons-png.flaticon.com/512/4076/4076478.png" width="120" />
                <h3>No Record Found</h3>
                <p>No payment ledger found.</p>
            </div>
        </div>


        <asp:HiddenField ID="hd_session_id" runat="server" />
        <asp:HiddenField ID="hd_adm_no" runat="server" />
        <asp:HiddenField ID="hd_class_id" runat="server" />
        <script type="text/javascript">
            var app = angular.module('RpCardApp', []);
            app.controller('RpCardAppCtrl', function ($scope, $http) {

                var session_id = $("#<%=hd_session_id.ClientID%>").val();
                var adm_no = $("#<%=hd_adm_no.ClientID%>").val();
                var class_id = $("#<%=hd_class_id.ClientID%>").val();


                $http.get("webService/apis.asmx/fetch_PaymentLedger", { params: { "Session_id": session_id, "Adm_no": adm_no, "Class_id": class_id } }).then(function (response) {
                    $scope.ledgerS = response.data;
                    $scope.totalAmount = 0;
                    $scope.totalDiscount = 0;
                    $scope.totalPaid = 0;
                    $scope.totalDue = 0;

                    angular.forEach($scope.ledgerS, function (x) {

                        $scope.totalAmount += parseFloat(x.Amount) || 0;
                        $scope.totalDiscount += parseFloat(x.Disc_amount) || 0;
                        $scope.totalPaid += parseFloat(x.Prev_paid) || 0;
                        $scope.totalDue += parseFloat(x.Dues_amt) || 0;

                    });

                    if ($scope.ledgerS == "") {
                        $("#YesData").addClass("hidden");
                        $("#noDataSection").removeClass("hidden");
                    }
                    else {
                        $("#YesData").removeClass("hidden");
                        $("#noDataSection").addClass("hidden");
                    }
                })
            });
        </script>
    </form>
</body>
</html>
