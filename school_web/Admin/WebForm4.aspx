<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm4.aspx.cs" Inherits="school_web.Admin.WebForm4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="https://ajax.googleapis.com/ajax/libs/angularjs/1.3.15/angular.min.js"></script>
    <script src="../assets/js/jquery-1.10.2.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">


            <div data-ng-app="RpCardApp" data-ng-controller="RpCardAppCtrl">
                <table  id="myTable" class="display">
                    <thead>
                        <tr>
                            <th>#</th>
                            <th class="hiddenOnPrint"></th>
                            <th>Admission No.</th>
                            <th>Name</th>
                            <th>Class</th>
                            <th>Section</th>
                            <th>Roll No.</th>
                            <th>Mobile No.</th>
                            <th>Payable Amount</th>
                            <th>Paid Amount </th>
                            <th>Dues Amount</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr data-ng-repeat="x in reportAmountS">
                            <td>{{$index+1}}</td>
                            <td class="hiddenOnPrint"><a class="button-61 nowordbreak collect-feesss" href="{{x.Payment_link}}">Collect Fee</a></td>
                            <td>{{x.Admission_no}}</td>
                            <td>{{x.Studentname}}</td>
                            <td>{{x.Class_name}}</td>
                            <td>{{x.Section}}</td>
                            <td>{{x.Rollnumber}}</td>
                            <td>{{x.Mobilenumber}}</td>
                            <td>{{x.Payable_amount}}</td>
                            <td>{{x.Paid_amount}}</td>
                            <td>{{x.Dues_amount}}</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>

        <script type="text/javascript">
            var app = angular.module('RpCardApp', []);
            app.controller('RpCardAppCtrl', function ($scope, $http) {

                var session_id = "2";
                var class_id = "0";

                $http.get("webServices/student-report.asmx/fetch_report_of_students", {
                    params: { "Session_id": session_id, "Class_id": class_id }
                }).then(function (response) {
                    $scope.reportAmountS = response.data;
                    $("#intsLoader").addClass("hidden");
                    if ($scope.reportAmountS == "") {
                        $("#grdsdatA").addClass("hidden");
                        $("#NotFounD").removeClass("hidden");
                        $("#excelbtnS").addClass("hidden");
                    }
                    else {
                        $("#grdsdatA").removeClass("hidden");
                        $("#NotFounD").addClass("hidden");
                        $("#excelbtnS").removeClass("hidden");
                    }
                })
            });
        </script>


        <link rel="stylesheet" href="https://cdn.datatables.net/2.0.0/css/dataTables.dataTables.css" />

        <script src="https://cdn.datatables.net/2.0.0/js/dataTables.js"></script>

        <script>
            $(document).ready(function () {
                $('#myTable').DataTable();
            });
        </script>
    </form>
</body>
</html>
