<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Payroll_Login2.aspx.cs" Inherits="school_web.Admin.Payroll_Login2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .loading {
            padding: 14px 0px 0px 0px;
            line-height: 49px;
            font-family: Arial;
            font-size: 23pt;
            border: 5px solid #67CFF5;
            background-color: White;
        }
    </style>
    <script type="text/javascript">
        function ShowProgress() {
            // alert('sdsjgdhsdgfsd');
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }
        function ShowProgress_hide() {
            // alert('sdsjgdhsdgfsd');

            document.getElementsByClassName('loading').style.visibility = 'hidden';

        }
        $('form').on("submit", function () {
            // alert('sdsjgdhsdgfsd');
            ShowProgress();
        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="page-wrapper">
        <div class="page-content">
            <div id="notification">
                <div id="pan" class="notificationpan">

                    <div id="success" runat="server" visible="false" style="float: left; width: 100%; height: auto;" class="alert alert-success border-0 bg-success alert-dismissible fade show py-2">
                        <div class="d-flex align-items-center">
                            <div class="font-35 text-white">
                                <i class='bx bxs-check-circle'></i>
                            </div>
                            <div class="ms-3">
                                <h6 class="mb-0 text-white">Success Alerts</h6>
                                <asp:Label ID="lbl_success" runat="server" ForeColor="White" class="text-white"></asp:Label>
                            </div>
                        </div>
                        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                    </div>

                    <div id="warning" runat="server" visible="false" class="alert alert-warning border-0 bg-warning alert-dismissible fade show py-2">
                        <div class="d-flex align-items-center">
                            <div class="font-35 text-dark">
                                <i class='bx bx-info-circle'></i>
                            </div>
                            <div class="ms-3">
                                <h6 class="mb-0 text-dark">Warning Alerts</h6>
                                <asp:Label ID="lbl_warning" runat="server" ForeColor="White" class="text-white"></asp:Label>
                            </div>
                        </div>
                        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                    </div>
                </div>
            </div>
            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-2"></div>
                <div class="col-xl-8">
                    <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Going to Payroll Admin...."></asp:Label>
                    <hr />
                    <div class="card">
                        <div class="card-body">

                            <div class="p-4 border rounded loading" id="a1" runat="server">

                                <img src="../images/login-animate.gif" />


                            </div>


                        </div>
                    </div>
                </div>

                <div class="col-xl-2"></div>

            </div>
        </div>
        <!--end row-->
    </div>
</asp:Content>
