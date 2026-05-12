<%@ Page Title="" Language="C#" MasterPageFile="~/Student_Profile/main.Master" AutoEventWireup="true" CodeBehind="payment-history.aspx.cs" Inherits="school_web.Student_Profile.payment_history" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Payment History
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
    .payhis-qs-bx-wpr-inrs table tr th {
        margin: 0px 0px 0px 0px;
        padding: 5px 10px 6px 10px;
    }

  </style>
    <script src="../assets/Angular/angular.min.js"></script>

    <script language="javascript">
        var popupWindow = null;
        function positionedPopup(url, winName, w, h, t, l, scroll) {
            settings =
                'height=' + h + ',width=' + w + ',top=' + t + ',left=' + l + ',scrollbars=' + scroll + ',resizable'
            popupWindow = window.open(url, winName, settings)
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="pagemainhh" data-ng-app="PaymentApp" data-ng-controller="PaymentAppCtrl">
        <div class="container">

            <div id="notification">
                <div id="pan" class="notificationpan">
                    <div id="success" runat="server" visible="false" style="        float: left;
        width: 100%;
        height: auto;" class="alert alert-success border-0 bg-success alert-dismissible fade show py-2">
                        <div class="d-flex align-items-center">
                            <div class="font-35 text-white">
                                <i class='bx bxs-check-circle'></i>
                            </div>
                            <div class="ms-3">
                                <asp:Label ID="lbl_success" runat="server" ForeColor="White" class="text-white"></asp:Label>
                            </div>
                        </div>
                        <asp:LinkButton ID="LinkButton1" class="btn-closes" runat="server" Style="        color: #fff">X</asp:LinkButton>
                    </div>

                    <div id="warning" runat="server" visible="false" class="alert alert-warning border-0 bg-warning alert-dismissible fade show py-2">
                        <div class="d-flex align-items-center">
                            <div class="font-35 text-dark">
                                <i class='bx bx-info-circle'></i>
                            </div>
                            <div class="ms-3">
                                <asp:Label ID="lbl_warning" runat="server" ForeColor="White" class="text-white"></asp:Label>
                            </div>
                        </div>
                        <asp:LinkButton ID="LinkButton2" class="btn-closes" runat="server" Style="        color: #fff">X</asp:LinkButton>
                    </div>
                </div>
            </div>


            <div class="main-card mb-3 card">
                <div class="card-header">
                    <h4 class="card-title">Payment History  <asp:DropDownList ID="ddl_session" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_session_SelectedIndexChanged"></asp:DropDownList></h4>
                </div>
                <div class="card-body" style="padding-top: 0px;" id="pnl1" runat="server" visible="false">
                    <div class="headingtablee">
                        <div class="row">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <div class="grd-wpr">
                                    <div class="payhis-qs-wpr">

                                        <div class="payhis-qs-bx-wpr" data-ng-repeat="x in pymenyHis track by $index">
                                            <div class="payhis-qs-bx-wpr-inrs">
                                                <p class="payhis-qs-bx-wpr-slip-no-p" style="        width: 90%">Slip No : <span>{{x.Slip_no}}</span> Date : <span>{{x.Date}}</span></p>
                                                <p><a href="{{x.payment_link}}" onclick="positionedPopup(this.href,'myWindow','950','650','200','200','yes');return false" style="        width: 10%;
        text-decoration: dashed;
        color: red"><i class="fa fa-print" aria-hidden="true"></i> Print Slip</a></p>
                                                <table class="table-bordered" style="        width: 100%">
                                                    <tr>
                                                        <th>Particular</th>
                                                        <th>Amount</th>
                                                        <th>Disc.</th>
                                                        <th>Paid</th>
                                                    </tr>
                                                    <tr data-ng-repeat="item in x.MyPatmentItem track by $index">
                                                        <td>{{item.Content}}</td>
                                                        <td>{{item.payable}}</td>
                                                        <td>{{item.disc_amt}}</td>
                                                        <td>{{item.paid}}</td>
                                                    </tr>
                                                </table>
                                                <div class="payhis-ttl-btn-dv-wpr">
                                                    <div class="payhis-ttl-btn-dv">
                                                        <p class="payhis-ttl-btn-dvp">Total : <span>{{x.Total_amt}}</span> </p>
                                                    </div>
                                                    <div class="payhis-ttl-btn-dv">
                                                        <p class="payhis-ttl-btn-dvp">Discount : <span>{{x.Total_disc}}</span> </p>
                                                    </div>
                                                    <div class="payhis-ttl-btn-dv">
                                                        <p class="payhis-ttl-btn-dvp">G. Total : <span>{{x.Yotal_amt_after_disc}}</span> </p>
                                                    </div>
                                                    <div class="payhis-ttl-btn-dv">
                                                        <p class="payhis-ttl-btn-dvp">Paid : <span>{{x.Total_paid_amt}}</span> </p>
                                                    </div>
                                                    <div class="payhis-ttl-btn-dv">
                                                        <p class="payhis-ttl-btn-dvp">Dues : <span>{{x.Total_dues}}</span> </p>
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
        <!--end row-->
    </div>


    <asp:HiddenField ID="hd_admission_no" runat="server" />
    <asp:HiddenField ID="hd_session" runat="server" />
    <asp:HiddenField ID="hd_branch" runat="server" />

    <script type="text/javascript">
        var app = angular.module('PaymentApp', []);
        app.controller('PaymentAppCtrl', function ($scope, $http) {

            var admission_no = $("#<%=hd_admission_no.ClientID%>").val();
            var session = $("#<%=hd_session.ClientID%>").val();
            var branch_id = $("#<%=hd_branch.ClientID%>").val();



            $http.get("WebService1.asmx/fetch_payment_history", { params: { "Session": session, "Branch_id": branch_id, "Admission_no": admission_no } }).then(function (response) {
                $scope.pymenyHis = response.data;
            })

        });

    </script>
</asp:Content>
