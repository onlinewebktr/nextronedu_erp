<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Print-hr-id-card.aspx.cs" Inherits="school_web.Admin.id_card.Print_hr_id_card" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Id Card</title>
    <link href="https://fonts.googleapis.com/css?family=Open+Sans:100,200,300,400,500,600,700" rel="stylesheet" />

    <link href="css/custom.css" rel="stylesheet" />
    <link href="css/id-print.css" rel="stylesheet" />

    <script src="../../assets/js/jquery-1.10.2.min.js"></script>
    <script src="../../assets/Angular/angular.min.js"></script>



    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="css/id-print.css" rel="stylesheet" type="text/css" />');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body ></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
    </script>

    <style>
        @media print {
            body {
                content: url(../images/id-bg-hor.jpg);
            }


            .id-card-hr-child-details-p {
                color: #001fb5;
                text-shadow: 0 0 0 #ccc;
            }

            @media print and (-webkit-min-device-pixel-ratio:0) {
                h1 {
                    color: #ccc;
                    -webkit-print-color-adjust: exact;
                }
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hd_admission_no" runat="server" />
        <asp:HiddenField ID="hd_session_id" runat="server" />
        <asp:HiddenField ID="hd_class_id" runat="server" />
        <asp:HiddenField ID="hd_branch" runat="server" />
        <asp:HiddenField ID="hd_section" runat="server" />
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
                        <div class="id-card-hr-sec">
                            <div class="ids-center" data-ng-repeat="x in vrid">
                                <div class="ids-back-img">
                                    <img src="{{x.idcard_template}}" class="ids-back-img-wpr" />
                                </div>
                                <div class="id-card-hr-prnt-wpr">
                                    <div class="id-card-hr-wpr-bg-color">
                                        <div class="id-card-hr-head-sec">
                                        </div>

                                        <div class="id-card-hr-content-sec">
                                            <div class="id-card-hr-stu-img-sec">
                                                <img src="{{x.photo}}" class="id-card-hr-stu-img" />
                                            </div>
                                            <div class="id-card-hr-child-details-sec">
                                                <div class="id-card-hr-child-details-wpr">
                                                    <p class="id-card-hr-child-details-p" style="width: 45px;">Name : </p>
                                                    <p class="id-card-hr-child-details-psss" style="font-weight: 600 !important">{{x.name}}</p>
                                                </div>
                                                <div class="id-card-hr-child-details-wpr">
                                                    <p class="id-card-hr-child-details-p" style="width: 45px;">{{x.So_or_Do}} : </p>
                                                    <p class="id-card-hr-child-details-psss">{{x.father_name}}</p>
                                                </div>
                                                <div class="id-card-hr-child-details-wpr">
                                                    <p class="id-card-hr-child-details-p" style="width: 60px;">Reg. No. : </p>
                                                    <p class="id-card-hr-child-details-psss" style="width: 70px;">{{x.Admission_no}}</p>


                                                    <p class="id-card-hr-child-details-p" style="width: 65px;">Roll No. : </p>
                                                    <p class="id-card-hr-child-details-psss" style="width: 40px;">{{x.roll_no}}</p>

                                                    <p class="id-card-hr-child-details-p" style="width: 45px;">Sec. : </p>
                                                    <p class="id-card-hr-child-details-psss" style="width: 30px;">{{x.sec}}</p>

                                                    <p class="id-card-hr-child-details-p" style="width: 50px;">Class : </p>
                                                    <p class="id-card-hr-child-details-psss" style="width: 110px;">{{x.sclass}}</p>

                                                </div>
                                                <%--<div class="id-card-hr-child-details-wpr">
                                                        <p class="id-card-hr-child-details-p" style="width: 45px;">D.O.B. :-</p>
                                                        <p class="id-card-hr-child-details-p id-card-hr-txt-color" style="width: 70px;">{{x.dob}}</p>

                                                        <p class="id-card-hr-child-details-p" style="width: 93px;">Blood Group :-</p>
                                                        <p class="id-card-hr-child-details-p id-card-hr-txt-color">{{x.Blood_group}}</p>
                                                    </div>--%>
                                                <div class="id-card-hr-child-details-wpr">
                                                    <p class="id-card-hr-child-details-p" style="width: 37px;">Add : </p>
                                                    <p class="id-card-hr-child-details-psss" style="width: 197px;">{{x.address}}</p>
                                                </div>
                                                <div class="id-card-hr-child-details-wpr">
                                                    <p class="id-card-hr-child-details-p" style="width: 54px;">Mobile : </p>
                                                    <p class="id-card-hr-child-details-psss">{{x.mobile}}</p>
                                                </div>
                                                 <div class="id-card-hr-child-details-wpr">
                                                    <p class="id-card-hr-child-details-p" style="width: 83px;">Transport : </p>
                                                    <p class="id-card-hr-child-details-psss">{{x.TransportationtakenYesNo}}</p>
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
        </div>
        <script type="text/javascript">
            var app = angular.module('IdCardvrApp', []);
            app.controller('IdCardvrctrl', function ($scope, $http) {

                messge("Please Wait...");
                $("#intsLoader").removeClass("hidden");


                var admission_no = $("#<%=hd_admission_no.ClientID%>").val();
                var session_id = $("#<%=hd_session_id.ClientID%>").val();
                var class_id = $("#<%=hd_class_id.ClientID%>").val();
                var branch_id = $("#<%=hd_branch.ClientID%>").val();
                var sections = $("#<%=hd_section.ClientID%>").val();
                var idType = $("#<%=hd_type.ClientID%>").val();


                // if (idType == "BULK") {
                $http.get("WebService1.asmx/fetch_all_verticle_id_card_details", { params: { "Session_id": session_id, "Class_id": class_id, "Sections": sections, "Branch_id": branch_id, "IdType": idType, "Admission_no": admission_no } }).then(function (response) {
                    $scope.vrid = response.data;
                    $("#intsLoader").addClass("hidden");
                })
                // } 
                //else {
                //    $http.get("WebService1.asmx/fetch_all_verticle_id_card_details", { params: { "School_id": school_id, "School_class": school_class, "student_id": student_id } }).then(function (response) {
                //        $scope.vrid = response.data;
                //        $("#intsLoader").addClass("hidden");
                //    })
                //}
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
