<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="id-card-back-image-emp001.aspx.cs" Inherits="school_web.Admin.id_card.id_card_back_image_emp001" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Id Card</title>
    <link href="https://fonts.googleapis.com/css2?family=Sofia+Sans:ital,wght@0,1..1000;1,1..1000&display=swap" rel="stylesheet" />
    <link href="css/custom.css" rel="stylesheet" />
    <link href="css/emp-id-card-back.css" rel="stylesheet" />
    <script src="../../assets/Angular/angular.min.js"></script>
    <script src="../../assets/js/jquery-1.10.2.min.js"></script>
    <script src="../../assets/js/html2canvas.min.js"></script>
    <script type="text/javascript">  
        function ConvertToImage(btnExport) {
            debugger;
            html2canvas($("#dvTable")[0]).then(function (canvas) {
                debugger;
                var base64 = canvas.toDataURL();
                $("[id*=hfImageData]").val(base64);
                debugger;
                __doPostBack(btnExport.name, "");

            });
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hd_userType" runat="server" />
        <asp:HiddenField ID="hd_emp_id" runat="server" />
        <asp:HiddenField ID="hd_branch" runat="server" />
        <asp:HiddenField ID="hd_type" runat="server" />
        <asp:HiddenField ID="hfImageData" runat="server" ClientIDMode="Static" />

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
                    <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" runat="server" ToolTip="Print" CssClass="form-btn" Text="Print" Style="width: 100px; text-align: center; float: right; display: none"></asp:LinkButton>
                </div>
                <asp:Button ID="btnExport" Style="margin: 0px 0px 5px 0px;" Text="Export to Image" runat="server" UseSubmitBehavior="false" OnClick="ExportToImage" OnClientClick="return ConvertToImage(this)" />

                <div class="id-card-hr-sec">
                    <div class="print-div">
                        <div class="id-card-vr-sec-inr">
                            <div class="ids-center-vr" data-ng-repeat="x in vrid"  id="dvTable">
                                <div class="ids-back-img-vr">
                                    <img src="{{x.Template_back}}" class="ids-back-img-wpr-vr" />
                                </div>
                                <div class="id-card-vr-prnt-wpr">
                                    <div class="id-card-vr-wpr-bg-color">
                                        <div class="id-card-vr-head-sec"></div>
                                        <div class="id-card-vr-content-sec">
                                            <div class="id-card-vr-child-details-sec"> 
                                                <div class="emp-id-contnt-wpr">

                                                    <p class="id-card-vr-child-details-p"><span>Id Card No.</span> <i>:</i> </p>
                                                    <p class="id-card-hr-child-details-psss">{{x.Id_card_no}}</p>
                                                    <div class="devider"></div>
                                                    <p class="id-card-vr-child-details-p"><span>Aadhar No.</span> <i>:</i> </p>
                                                    <p class="id-card-hr-child-details-psss">{{x.Aadhar_no}}</p>
                                                    <div class="devider"></div> 
                                                    <p class="id-card-vr-child-details-p"><span>Contact No.</span> <i>:</i> </p>
                                                    <p class="id-card-hr-child-details-psss">{{x.Mobile_no}}</p>
                                                    <div class="devider"></div>
                                                    <p class="id-card-vr-child-details-p"><span>Email Id</span> <i>:</i> </p>
                                                    <p class="id-card-hr-child-details-psss">{{x.Email_Id}}</p>
                                                    <div class="devider"></div>
                                                   

                                                    <div class="id-card-vr-child-details-wpr">
                                                        <p class="id-card-vr-child-details-p"><span>Address</span> <i>:</i></p>
                                                        <p class="id-card-hr-child-details-address">{{x.Address}}</p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%--<div class="btm-txts">
                                        <p class="emp-id-contnt-wpr-contact"><span>Contact No. : </span>{{x.Mobile}}</p>
                                        <p class="emp-id-contnt-wpr-id-no"><span>Id No. : </span>{{x.Emp_Code}}</p>
                                    </div>--%>
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



                $http.get("webService/emp-id-card.asmx/fetch_id_cards_details_for_employee", { params: { "UserType": user_type, "Emp_id": emp_id, "Branch_id": branch_id, "IdType": idType } }).then(function (response) {
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
