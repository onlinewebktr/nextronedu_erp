<%@ Page Title="" Language="C#" MasterPageFile="~/Student_Profile/main.Master" AutoEventWireup="true" CodeBehind="ledger.aspx.cs" Inherits="school_web.Student_Profile.ledger" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Ledger
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
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
                    <h4 class="card-title">Ledger</h4>
                </div>
                <div class="card-body" style="padding-top: 0px;">
                    <div class="headingtablee">
                        <div class="row">
                            <div class="col-lg-2 col-width-50 pads-rght-5">
                                <label for="validationCustom01" class="lebelheadpp">Date From</label>
                                <div class="clndr-dv-wpr" style="position: relative;">
                                    <asp:TextBox ID="txt_from_date" runat="server" class="form-control"></asp:TextBox>
                                    <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                </div>
                            </div>
                            <div class="col-lg-2 col-width-50 pads-lft-5">
                                <label for="validationCustom01" class="lebelheadpp">Date To</label>
                                <div class="clndr-dv-wpr" style="position: relative;">
                                    <asp:TextBox ID="txt_to_date" runat="server" class="form-control"></asp:TextBox>
                                    <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                </div>
                            </div>

                            <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                                <asp:Button ID="btn_find" runat="server" Text="Find" class="mt-2 btn btn-primary fnd-btnmrgn" OnClick="btn_find_Click" />
                            </div>
                        </div>



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

                                                                <div class="ledger-qs-bx-wpr-row">
                                                                    <p class="ledger-qs-bx-wpr-p">Date : </p>
                                                                    <asp:Label ID="lbl_date" runat="server" Text='<%#Bind("Date") %>'></asp:Label>
                                                                </div>
                                                                <div class="ledger-qs-bx-wpr-row">
                                                                    <p class="ledger-qs-bx-wpr-p">Slip No. : </p>
                                                                    <asp:Label ID="lbl_slip_no" runat="server" Text='<%#Bind("Slip_no") %>'></asp:Label>
                                                                </div>
                                                                <div class="ledger-qs-bx-wpr-row">
                                                                    <p class="ledger-qs-bx-wpr-p">Admission No. : </p>
                                                                    <asp:Label ID="lbl_adm_No" class="ledger-qs-bx-wpr-p-spn" Text='<%#Bind("Addmission_no") %>' runat="server"></asp:Label>
                                                                </div>
                                                                <div class="ledger-qs-bx-wpr-row">
                                                                    <p class="ledger-qs-bx-wpr-p">Remarks : </p>
                                                                    <asp:Label ID="Label1" class="ledger-qs-bx-wpr-p-spn" Text='<%#Bind("Remarks") %>' runat="server"></asp:Label>
                                                                </div>
                                                                <div class="ledger-qs-bx-wpr-row">
                                                                    <p class="ledger-qs-bx-wpr-p">Payment Mode : </p>
                                                                    <asp:Label ID="Label2" class="ledger-qs-bx-wpr-p-spn" Text='<%#Bind("mode") %>' runat="server"></asp:Label>
                                                                </div>
                                                                <div class="ledger-qs-bx-wpr-row">
                                                                    <p class="ledger-qs-bx-wpr-p">Description : </p>
                                                                    <asp:Label ID="Label3" class="ledger-qs-bx-wpr-p-spn mnhghts" Text='<%#Bind("Description") %>' runat="server"></asp:Label>
                                                                </div>
                                                                <div class="ledger-qs-bx-wpr-row">
                                                                    <p class="ledger-qs-bx-wpr-p">Amount : </p>
                                                                    <asp:Label ID="lbl_amts" class="ledger-qs-bx-wpr-p-spn" Text='<%#Bind("Amount") %>' runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>


                                        <asp:Label ID="lbl_ttl_amts" class="ledger-ttl-amts" runat="server" Text=""></asp:Label>
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

    <link href="../Autocomplete/jquery-ui.css" rel="stylesheet" />
    <script src="../Autocomplete/jquery-ui.js"></script>

    <script>
        $(function () {
            $("#<%=txt_from_date.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "1900:2100"
            });
        });
    </script>
    <script>
        $(function () {
            $("#<%=txt_to_date.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "1900:2100"
            });
        });
    </script>
    <link href="assets/css/calender-modified.css" rel="stylesheet" />
</asp:Content>
