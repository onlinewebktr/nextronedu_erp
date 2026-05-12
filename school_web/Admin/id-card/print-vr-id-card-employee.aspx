<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="print-vr-id-card-employee.aspx.cs" Inherits="school_web.Admin.id_card.print_vr_id_card_employee" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Id Card</title>
    <link href="https://fonts.googleapis.com/css?family=Open+Sans:100,200,300,400,500,600,700" rel="stylesheet" />
    <link href="css/custom.css" rel="stylesheet" />
    <link href="css/burdhwan-emp.css" rel="stylesheet" />
    <script src="../../assets/js/jquery-1.10.2.min.js"></script>
    <script src="../../assets/Angular/angular.min.js"></script>



    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="css/burdhwan-emp.css" rel="stylesheet" type="text/css" />');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body ></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
    </script>

    <style media="print" type="text/css">
        @media print {
            .id-card-vr-wpr {
                margin: 0px;
                padding: 0px;
                width: 271px;
                height: auto;
                float: left;
                background: url(../images/id-bg-hor.jpg) no-repeat;
                background-size: 255%;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hd_userType" runat="server" />
        <asp:HiddenField ID="hd_emp_id" runat="server" />
        <asp:HiddenField ID="hd_branch" runat="server" />
        <asp:HiddenField ID="hd_type" runat="server" />

        <div class="id-card-page-sec" data-ng-app="IdCardvrApp">
            <div class="id-card-page-wpr" data-ng-controller="IdCardvrctrl">

                <div class="row">
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
                </div>

                <div class="id-card-page-print-btn-sec">
                    <asp:Button ID="btn_back" CssClass="form-btn" runat="server" Text="Back" OnClick="btn_back_Click" Style="width: 100px;" />
                    <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" runat="server" ToolTip="Print" CssClass="form-btn" Text="Print" Style="width: 100px; text-align: center; float: right;"></asp:LinkButton>
                </div>
                <div class="id-card-hr-sec" id="tblPrintIQ" runat="server">
                    <div class="print-div">
                        <div class="id-card-vr-sec-inr">
                            <div class="ids-center-vr" data-ng-repeat="x in vrid">
                                <div class="ids-back-img-vr">
                                    <img src="{{x.idcard_template}}" class="ids-back-img-wpr-vr" />
                                </div>
                                <div class="id-card-vr-prnt-wpr">
                                    <div class="id-card-vr-wpr-bg-color">
                                        <div class="id-card-vr-head-sec">
                                        </div>

                                        <div class="id-card-vr-content-sec">
                                            <div class="id-card-vr-stu-img-sec">
                                                <img src="{{x.Employee_image}}" class="id-card-vr-stu-img" />
                                            </div>
                                            <div class="id-card-vr-child-details-sec">
                                                <div class="emp-id-contnt-wpr">
                                                    <p class="emp-id-contnt-wpr-name">{{x.Employee_Name}}</p>
                                                    <div class="id-devider"><span></span></div>
                                                    <p class="emp-id-contnt-wpr-desig">Designation : {{x.Designation}}</p>
                                                    <p class="emp-id-contnt-wpr-desig">Session : {{x.Sessions}}</p>
                                                    <p class="emp-id-contnt-wpr-add"><span>Address : </span>{{x.Address}}</p>

                                                </div>
                                            </div>
                                        </div> 
                                    </div>
                                    <div class="btm-txts">
                                        <p class="emp-id-contnt-wpr-contact"><span>Contact No. : </span>{{x.Mobile}}</p>
                                        <p class="emp-id-contnt-wpr-id-no"><span>Id No. : </span>{{x.Emp_Code}}</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <script type="text/javascript">
            var app = angular.module('IdCardvrApp', []);
            app.controller('IdCardvrctrl', function ($scope, $http) {

                messge("Please Wait...");
                $("#intsLoader").removeClass("hidden");

                var user_type = $("#<%=hd_userType.ClientID%>").val();
                var emp_id = $("#<%=hd_emp_id.ClientID%>").val();
                var branch_id = $("#<%=hd_branch.ClientID%>").val();
                var idType = $("#<%=hd_type.ClientID%>").val();



                $http.get("WebService1.asmx/fetch_id_cards_details_for_employee", { params: { "UserType": user_type, "Emp_id": emp_id, "Branch_id": branch_id, "IdType": idType } }).then(function (response) {
                    $scope.vrid = response.data;
                    $("#intsLoader").addClass("hidden");
                })
            });
            function messge(msg) {
                $("#<%=lblmessage.ClientID%>").text(msg);
                $('.ints-loader-wpr').hide().slideDown(0);
                //$('.notificationpan').delay(4000).show().slideUp(1000);
            }


        </script>

        <style>
            .hidden {
                display: none !important;
            }
        </style>
    </form>
</body>
</html>
