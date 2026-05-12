<%@ Page Title="" Language="C#" MasterPageFile="~/Inventory_management/Site3.Master" AutoEventWireup="true" CodeBehind="Upload_Item_by_excel.aspx.cs" Inherits="school_web.Inventory_management.Upload_Item_by_excel" MaintainScrollPositionOnPostback="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Create Item
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

    <style>
        sup {
            color: red;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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

        <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
            <div class="breadcrumb-title pe-3">Upload Item By Excel    </div>
            <div class="ps-3">
                <nav aria-label="breadcrumb">
                    <ol class="breadcrumb mb-0 p-0">
                        <li class="breadcrumb-item"><a href="Dashboard.aspx"><i class="bx bx-home-alt"></i></a>
                        </li>
                        <li class="breadcrumb-item active" aria-current="page">Upload Item By Excel</li>
                    </ol>
                </nav>
            </div>
        </div>



        <asp:HiddenField ID="HdID" runat="server" />
        <div class="row">
            <div class="col-xl-10" style="margin: 0px auto;">
                <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Create Item"></asp:Label>
                <a href="Create_Item.aspx" style="float: right">View Items</a>
                <hr />
                <div class="card">
                    <div class="card-body">
                        <div class="p-4 border rounded">
                            <div class="row g-3 needs-validation" novalidate="">
                                <div class="col-md-12">
                                    <a href="../important_docx/inverntoryitem.csv">Download Item Format</a>
                                </div>
                                <div class="col-md-6">
                                    <label for="validationCustom01" class="form-label">Choose CSV File  <sup>*</sup></label>
                                    <asp:FileUpload ID="FileUpload1" runat="server" class="form-control find-dv-txtbx" />

                                </div>
                                <div class="col-md-6">
                                    <asp:Button ID="btn_Uplaod" runat="server" Text="Upload CSV" CssClass="btn btn-success" OnClick="btn_Uplaod_Click" Style="margin: 30px 0px 0px 0px;" />

                                </div>

                            </div>
                            <div class="col-md-12" style="overflow:auto;">
                                <asp:GridView ID="grd_services" runat="server"
                                    Style="margin: 0px; float: left; font-size: 13px; padding: 0px; width: 100%; text-align: center;"  class="table table-bordered">
                                </asp:GridView>
                            </div>
                            <div class="col-md-12">
                                <asp:Button ID="btn_final_submit" runat="server" Text="Final Submit" OnClick="btn_final_submit_Click" CssClass="btn btn-primary" ValidationGroup="a" Style="margin: 30px 0px 0px 0px;"/>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>



    </div>


</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
    <script src="../assets/js/Custom.js"></script>
</asp:Content>
