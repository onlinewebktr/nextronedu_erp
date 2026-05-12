<%@ Page Title="" Language="C#" MasterPageFile="~/Inventory_management/Site3.Master" AutoEventWireup="true" CodeBehind="Firm_Details.aspx.cs" Inherits="school_web.Inventory_management.Firm_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .btn {
            padding: 5px 20px;
            font-size: 15px;
        }

        .form-control {
            display: block;
            width: 100%;
            padding: 5px 10px;
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
            <div class="breadcrumb-title pe-3">Master</div>
            <div class="ps-3">
                <nav aria-label="breadcrumb">
                    <ol class="breadcrumb mb-0 p-0">
                        <li class="breadcrumb-item"><a href="Dashboard.aspx"><i class="bx bx-home-alt"></i></a>
                        </li>
                        <li class="breadcrumb-item active" aria-current="page">Firm Details</li>
                    </ol>
                </nav>
            </div>
        </div>



        <asp:HiddenField ID="HdID" runat="server" />
        <div class="row">
            <div class="col-xl-12">
                <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text=" "></asp:Label>
                <hr />
                <div class="card">
                    <div class="card-body">
                        <div class="p-4 border rounded">
                            <div class="row g-3 needs-validation" novalidate="">
                                <div class="col-lg-12" id="storeDiv" runat="server">
                                    <div class="position-relative form-group">
                                        <label>Firm Name</label>
                                        <asp:TextBox ID="txt_Colleg_name" runat="server" CssClass="form-control" oncopy="return false" onpaste="return false" oncut="return false"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-12">
                                    <div class="position-relative form-group">
                                        <label>Address</label>
                                        <asp:TextBox ID="txt_address1" runat="server" CssClass="form-control" TextMode="MultiLine" style="height:100px"></asp:TextBox>
                                    </div>
                                </div>








                                <div class="col-lg-6">
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <div class="position-relative form-group">
                                                <label>Contact No.</label>
                                                <asp:TextBox ID="txt_contactno" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <div class="position-relative form-group">
                                                <label>Email Id</label>
                                                <asp:TextBox ID="txt_emailid" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-xl-3">
                                    <div class="position-relative form-group">
                                        <label>State</label>
                                        <asp:DropDownList ID="ddl_state" runat="server" CssClass="form-select"></asp:DropDownList>
                                    </div>
                                </div>











                                <div class="col-lg-12">
                                    <div class="row">
                                        <div class="col-xl-3">
                                            <div class="position-relative form-group">
                                                <label>Logo(file size 1 mb)</label>
                                                <asp:FileUpload ID="FileUpload1" runat="server" class="form-control" />
                                                <asp:Label ID="lbl_img_path" runat="server" Visible="false" Text="Label"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-xl-6">
                                            <div class="position-relative form-group">
                                                <asp:Image ID="Image1" runat="server" Style="padding: 2px; height: 133px; width: 197px;" class="form-control" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <asp:Button ID="btn_Submit" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btn_Submit_Click" Style="padding: 7px 40px;" />

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
