<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="single-id-card.aspx.cs" Inherits="school_web.Admin.id_card.stjohn.single_id_card" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Id Card</title>
    <link href="https://fonts.googleapis.com/css2?family=Sofia+Sans:ital,wght@0,1..1000;1,1..1000&display=swap" rel="stylesheet" />
    <link href="css/custom.css" rel="stylesheet" />
    <link href="css/vr-id-card-new2.css" rel="stylesheet" />
    <script src="../../../assets/Angular/angular.min.js"></script>
    <script src="../../../assets/js/jquery-1.10.2.min.js"></script>
    <script src="../../../assets/js/html2canvas.min.js"></script>
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
        <asp:HiddenField ID="hd_admission_no" runat="server" />
        <asp:HiddenField ID="hd_session_id" runat="server" />
        <asp:HiddenField ID="hd_class_id" runat="server" />
        <asp:HiddenField ID="hd_branch" runat="server" />
        <asp:HiddenField ID="hd_section" runat="server" />
        <asp:HiddenField ID="hd_type" runat="server" />
        <asp:HiddenField ID="hfImageData" runat="server" ClientIDMode="Static" />

        <div class="id-card-page-sec" data-ng-app="IdCardvrApp">
            <div class="id-card-page-wpr" data-ng-controller="IdCardvrctrl">
                <div class="row">
                    <div class="ints-loader-wpr" id="intsLoader">
                        <div class="ints-loader-wpr-inr">
                            <div class="ints-loader">
                                <p class="ints-loader-txt">
                                    <img src="../../../assets/images/icons/loader-ico.gif" class="ints-loader-img" />
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

                <div class="id-card-hr-sec" id="tblPrintIQ" runat="server">
                    <div class="print-div">
                        <div class="id-card-vr-sec-inr">
                            <div data-ng-repeat="x in vrid">
                                <div id="dvTable" runat="server" class="ids-center-vr idspadd">
                                    <div class="ids-back-img-vr">
                                        <img src="{{x.idcard_template}}" class="ids-back-img-wpr-vr" />
                                    </div>
                                    <div class="id-card-vr-prnt-wpr">
                                        <div class="id-card-vr-wpr-bg-color">
                                            <div class="id-card-vr-head-sec">
                                            </div>

                                            <div class="id-card-vr-content-sec">
                                                <div class="stdimgDV">
                                                    <div class="id-card-vr-stu-img-sec">
                                                        <div class="id-card-vr-stu-img-dvinnr">
                                                            <img src="{{x.photo}}" class="id-card-vr-stu-img" />
                                                        </div>
                                                    </div>
                                                </div>


                                                <div class="id-card-vr-child-details-sec">
                                                    <p class="id-card-vr-child-details-p id-card-hr-txt-color namespp" style="font-weight: 900!important;">{{x.name}}</p>

                                                    <div class="id-card-vr-child-details-wpr">
                                                        <p class="id-card-vr-child-details-p" style="color: {{x.InfotxtColor}}"><span>Adm. N.</span> <i>:</i> </p>
                                                        <p class="id-card-hr-child-details-psss" style="color: {{x.InfotxtColor}}">{{x.Admission_no}} <span style="padding: 0px 0px 0px 10px;">Class <i>:</i>  {{x.sclass}}</span></p>
                                                        <div class="devider"></div>
                                                        <p class="id-card-vr-child-details-p" style="color: {{x.InfotxtColor}}"><span>Roll</span> <i>:</i> </p>
                                                        <p class="id-card-hr-child-details-psss" style="color: {{x.InfotxtColor}}">{{x.roll_no}} <span style="padding: 0px 0px 0px 10px;">Sec. <i>:</i>  {{x.sec}}</span></p>
                                                        <div class="devider"></div>

                                                        <p class="id-card-vr-child-details-p" style="color: {{x.InfotxtColor}}"><span>D.O.B.</span> <i>:</i> </p>
                                                        <p class="id-card-hr-child-details-psss" style="color: {{x.InfotxtColor}}">{{x.dob}}</p>
                                                        <div class="devider"></div>
                                                        <p class="id-card-vr-child-details-p" style="color: {{x.InfotxtColor}}"><span>Father</span> <i>:</i> </p>
                                                        <p class="id-card-hr-child-details-psss" style="color: {{x.InfotxtColor}}">{{x.father_name}}</p>
                                                        <div class="devider"></div>
                                                        <p class="id-card-vr-child-details-p" style="color: {{x.InfotxtColor}}"><span>Mobile N.</span> <i>:</i> </p>
                                                        <p class="id-card-hr-child-details-psss" style="color: {{x.InfotxtColor}}">{{x.mobile}}</p>
                                                        <div class="devider"></div>

                                                        <p class="id-card-vr-child-details-p line-height-18" style="width: 72%; color: {{x.InfotxtColor}}"><span>Address</span> <i>:</i> {{x.address}}</p>
                                                        <%--<p class="id-card-hr-child-details-address">{{x.address}}</p>--%>
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



                $http.get("../webService/id-card-new.asmx/fetch_all_verticle_id_card_details", { params: { "Session_id": session_id, "Class_id": class_id, "Sections": sections, "Branch_id": branch_id, "IdType": idType, "Admission_no": admission_no } }).then(function (response) {
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
