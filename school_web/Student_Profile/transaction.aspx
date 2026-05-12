<%@ Page Title="" Language="C#" MasterPageFile="~/Student_Profile/main.Master" AutoEventWireup="true" CodeBehind="transaction.aspx.cs" Inherits="school_web.Student_Profile.transaction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    My Transaction
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .trnaction-qs-bx-wpr-ps {
            width: 72% !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pagemainhh">
        <div class="container">

            <div id="notification">
                <div id="pan" class="notificationpan">
                    <div id="success" runat="server" visible="false" style="float: left; width: 100%; height: auto;" class="alert alert-success border-0 bg-success alert-dismissible fade show py-2">
                        <div class="d-flex align-items-center">
                            <div class="font-35 text-white">
                                <i class='bx bxs-check-circle'></i>
                            </div>
                            <div class="ms-3">
                                <asp:Label ID="lbl_success" runat="server" ForeColor="White" class="text-white"></asp:Label>
                            </div>
                        </div>
                        <asp:LinkButton ID="LinkButton1" class="btn-closes" runat="server" Style="color: #fff">X</asp:LinkButton>
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
                        <asp:LinkButton ID="LinkButton2" class="btn-closes" runat="server" Style="color: #fff">X</asp:LinkButton>
                    </div>
                </div>
            </div>


            <div class="main-card mb-3 card">
                <div class="card-header">
                    <h4 class="card-title">My Transaction</h4>
                </div>
                <div class="card-body" style="padding-top: 0px;">
                    <div class="headingtablee">
                        <div class="row">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <div class="grd-wpr">
                                    <div class="ledger-qs-wpr">
                                        <div class="row">
                                            <asp:Repeater ID="rd_view" runat="server" OnItemDataBound="rd_view_ItemDataBound1">
                                                <ItemTemplate>
                                                    <div class="col-md-4">
                                                        <div class="ledger-qs-bx-wpr">
                                                            <div class="ledger-qs-bx-wpr-inrs">
                                                                <div class="ledger-qs-bx-trnaction-one">
                                                                    <p class="ledger-qs-bx-trnaction-amt">₹<asp:Label ID="lbl_amt" runat="server" Text='<%#Bind("Amount") %>'></asp:Label></p>
                                                                    <p class="trnaction-qs-bx-wpr-p">Slip No. : </p>
                                                                    <asp:Label ID="lbl_slip_no" class="trnaction-qs-bx-wpr-ps" runat="server" Text='<%#Bind("Slip_no") %>'></asp:Label>

                                                                    <p class="trnaction-qs-bx-wpr-p">Date : </p>
                                                                    <asp:Label ID="Label1" class="trnaction-qs-bx-wpr-ps" runat="server" Text='<%#Bind("Date") %>'></asp:Label>
                                                                    <asp:Label ID="lbl_trnsaction_in" Visible="false" runat="server" Text='<%#Bind("Transection_in") %>'></asp:Label>
                                                                     <asp:Label ID="Label2" class="trnaction-qs-bx-wpr-ps" runat="server" Text='<%#Bind("Type") %>'></asp:Label>
                                                                </div>

                                                                <asp:Label ID="lbl_ofline" runat="server" Visible="false">
                                                                <div class="ledger-qs-bx-trnaction-two">
                                                                    <p class="trnaction-qs-bx-wpr-of">
                                                                        <i class="fa fa-laptop" aria-hidden="true"></i>Offline
                                                                    </p>
                                                                    <p class="trnaction-qs-bx-wpr-inschool">In School</p>
                                                                </div>
                                                                </asp:Label>
                                                                <asp:Label ID="lbl_online" runat="server" Visible="false">
                                                                 <div class="ledger-qs-bx-trnaction-two">
                                                                    <p class="trnaction-qs-bx-wpr-online">
                                                                        <i class="fa fa-mobile" aria-hidden="true"></i>Online
                                                                    </p>
                                                                    <p class="trnaction-qs-bx-wpr-inschool">In App</p>
                                                                </div></asp:Label>
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
                    </div>
                </div>
            </div>
        </div>
        <!--end row-->
    </div>
</asp:Content>
