<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="id-card03.aspx.cs" Inherits="school_web.Admin.id_card.id_card03" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Id Card</title> 
    <link href="css/custom.css" rel="stylesheet" />
    <link href="css/id-card02.css" rel="stylesheet" />
    <%--<script src="../../assets/js/jquery-1.10.2.min.js"></script>--%>
    <%--<script src="../../assets/Angular/angular.min.js"></script>--%>



    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="css/id-card02.css" rel="stylesheet" type="text/css" />');
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

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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
        <asp:HiddenField ID="hd_admission_no" runat="server" />
        <asp:HiddenField ID="hd_session_id" runat="server" />
        <asp:HiddenField ID="hd_class_id" runat="server" />
        <asp:HiddenField ID="hd_branch" runat="server" />
        <asp:HiddenField ID="hd_section" runat="server" />
        <asp:HiddenField ID="hd_type" runat="server" />

        <div class="id-card-page-sec">
            <div class="id-card-page-wpr">

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
                    <%--<asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" runat="server" ToolTip="Print" CssClass="form-btn" Text="Print" Style="width: 100px; text-align: center; float: right;"></asp:LinkButton>--%>

                    <input id="btn-Preview-Image" type="button" class="form-btn" value="Download" style="width: 115px; text-align: center; float: right;" />

                </div>

                <asp:HiddenField ID="hfImageData" runat="server" ClientIDMode="Static" />
                <div class="id-card-hr-sec" id="tblPrintIQ" runat="server">
                    <div class="print-div">
                        <div class="id-card-vr-sec-inr">

                            <asp:Repeater ID="rd_view" runat="server">
                                <ItemTemplate>

                                    <div class="ids-center-vr" id="dvTable" runat="server">
                                        <div class="ids-back-img-vr">
                                            <img src="<%#Eval("idcard_template") %>" class="ids-back-img-wpr-vr" />
                                        </div>
                                        <div class="id-card-vr-prnt-wpr">
                                            <div class="id-card-vr-wpr-bg-color">
                                                <div class="id-card-vr-head-sec">
                                                </div>
                                                <div class="id-card-vr-content-sec">
                                                    <div class="regnosec">
                                                        <p class="regnosec-id-card-vr-child-details-p"><span>Reg. No.</span>  <i>:</i>  </p>
                                                        <p class="regnosec-id-card-hr-child-details-psss"><%#Eval("admissionserialnumber") %></p>
                                                    </div>
                                                    <div class="id-card-vr-child-details-wpr">
                                                        <p class="regnosec-id-card-vr-child-details-p"><span>Name</span>  <i>:</i> </p>
                                                        <p class="regnosec-id-card-hr-child-details-psss"><%#Eval("studentname") %></p>
                                                    </div>
                                                    <div class="id-card-vr-stu-img-sec">
                                                        <img src="<%#Eval("studentimagepath") %>" class="id-card-vr-stu-img" />
                                                    </div>
                                                    <div class="id-card-vr-child-details-sec">
                                                        <%--<div class="id-card-vr-child-details-wpr">
                                                    <p class="id-card-vr-child-details-p"><span>{{x.So_or_Do}}</span>  <i>:</i>  </p>
                                                    <p class="id-card-hr-child-details-psss">{{x.father_name}}</p>
                                                </div>--%>
                                                        <div class="id-card-vr-child-details-wpr">
                                                            <p class="id-card-vr-child-details-p"><span>Class</span> <i>:</i> </p>
                                                            <p class="id-card-hr-child-details-psss"><%#Eval("class") %></p>
                                                            <div class="brs"></div>
                                                            <p class="id-card-vr-child-details-p"><span>Section</span> <i>:</i> </p>
                                                            <p class="id-card-hr-child-details-psss"><%#Eval("Section") %></p>
                                                            <div class="brs"></div>
                                                            <p class="id-card-vr-child-details-p"><span>Roll No.</span> <i>:</i> </p>
                                                            <p class="id-card-hr-child-details-psss" style="width: 30px;"><%#Eval("rollnumber") %></p>

                                                            <div class="brs"></div>
                                                            <p class="id-card-vr-child-details-p"><span>Blood</span> <i>:</i> </p>
                                                            <%--<p class="id-card-vr-child-details-p" style="width: 157px;">Blood Group :</p>--%>
                                                            <p class="id-card-vr-child-details-p id-card-hr-txt-color"><%#Eval("blood_group") %></p>
                                                            <div class="brs"></div>
                                                            <div class="id-card-vr-child-details-wpr">
                                                                <p class="id-card-vr-child-details-p"><span>Mobile</span> <i>:</i>  </p>
                                                                <p class="id-card-hr-child-details-psss"><%#Eval("mobilenumber") %></p>
                                                            </div>
                                                            <div class="brs"></div>
                                                            <p class="id-card-vr-child-details-p"><span>By</span> <i>:</i> </p>
                                                            <p class="id-card-vr-child-details-p id-card-hr-txt-color"><%#Eval("TransportStatus") %></p>
                                                        </div>
                                                        <%--<div class="id-card-vr-child-details-wpr">
                                                    <p class="id-card-vr-child-details-p" style="width: 46px;">D.O.B. :-</p>
                                                    <p class="id-card-vr-child-details-p id-card-hr-txt-color" style="width: 75px;">{{x.dob}}</p>
                                                </div>--%>
                                                    </div>
                                                    <div class="id-card-vr-child-details-wpr">
                                                        <p class="id-card-address-dts" style="width: 100%">Address : <%#Eval("city_permanent") %></p>
                                                        <p class="id-card-if-found-dts">(If found please return to the above mentioned address.)</p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>


                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:Button ID="btnExport" Text="Export to Image" runat="server" UseSubmitBehavior="false" OnClick="ExportToImage" OnClientClick="return ConvertToImage(this)" />

        <%--<script type="text/javascript">
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



                $http.get("WebService1.asmx/fetch_all_verticle_id_card_details", { params: { "Session_id": session_id, "Class_id": class_id, "Sections": sections, "Branch_id": branch_id, "IdType": idType, "Admission_no": admission_no } }).then(function (response) {
                    $scope.vrid = response.data;
                    $("#intsLoader").addClass("hidden");
                })
            });
            function messge(msg) {
                $("#<%=lblmessage.ClientID%>").text(msg);
                $('.ints-loader-wpr').hide().slideDown(0);
            }


        </script>--%>

        <style>
            .hidden {
                display: none !important;
            }
        </style>


   
    </form>
</body>
</html>
