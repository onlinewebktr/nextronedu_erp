<%@ Page Title="" Language="C#" MasterPageFile="~/Dvlpr_Prof/Site1.Master" AutoEventWireup="true" CodeBehind="UpdateProgress.aspx.cs" Inherits="school_web.Dvlpr_Prof.UpdateProgress" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Update Progress
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script src="../../assets/js/jquery-1.10.2.min.js"></script>
    <style>
        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }

        .loading {
            padding: 14px 0px 0px 0px;
            line-height: 49px;
            font-family: Arial;
            font-size: 23pt;
            border: 5px solid #67CFF5;
            width: 67%;
            height: 254px;
            display: block;
            position: fixed;
            background-color: White;
            z-index: 999;
            left: 400px;
            top: 102px;
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

    <div class="app-main__inner">
        <div class="app-page-title">
            <div class="page-title-wrapper">
                <div class="page-title-heading">
                    <div class="page-title-icon">
                        <i class="pe-7s-rocket icon-gradient bg-mean-fruit"></i>
                    </div>
                    <div>
                        Update Progress
                    </div>
                </div>
            </div>
        </div>
        <div id="notification">
            <div id="pan" class="notificationpan">
                <div style="float: left; width: 100%; height: auto;">
                    <asp:Label ID="lblmessage" runat="server" Font-Bold="True" ForeColor="White"></asp:Label>
                </div>
            </div>
        </div>


        <div class="row">
            <div class="col-lg-12">
                <div class="main-card mb-3 card">
                    <div class="card-body">
                        <h5 class="card-title"></h5>
                        <div class="form-row">
                            <div class="col-md-12">
                                <div style="margin: 0px; float: left; height: auto; width: 100%;">
                                    <div style="margin: 0px; padding: 0%; float: left; height: auto; width: 100%" id="pnl_update_progress" runat="server">
                                         <asp:Label ID="lbl_update_mesage" runat="server" Font-Bold="True"  ></asp:Label>
                                        

                                        <div style="height: 1px; overflow: hidden">
                                            <asp:Button ID="btnSubmit" runat="server" Text="Pay"
                                                OnClientClick="retun ShowProgress();" />
                                        </div>
                                        <div class="loading" align="center" id="a1" runat="server">
                                            Please wait. File updating is in process. Please don't close the application. wait until the update file process is complete.
        <br />
                                            <br />
                                            <img src="../../images/loader.gif" />
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
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
