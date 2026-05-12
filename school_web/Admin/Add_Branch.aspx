<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Add_Branch.aspx.cs" Inherits="school_web.Admin.Add_Branch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Add Branch
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .btn {
            padding: 5px 20px;
            font-size: 15px;
        }
    </style>
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

            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                <div class="breadcrumb-title pe-3">Master</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Add Branch</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Add Branch"></asp:Label>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation" novalidate="">
                                    <div class="col-lg-12" id="storeDiv" runat="server">
                                        <div class="position-relative form-group">
                                            <label>Branch Name<sup>**</sup></label>
                                            <asp:TextBox ID="txt_Branch_name" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-12">
                                        <div class="position-relative form-group">
                                            <label>Address<sup>**</sup></label>
                                            <asp:TextBox ID="txt_address1" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>




                                    <div class="col-lg-4">
                                        <div class="row">
                                            <div class="col-lg-6">
                                                <div class="position-relative form-group">
                                                    <label>Contact No.<sup>**</sup></label>
                                                    <asp:TextBox ID="txt_contactno" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6">
                                                <div class="position-relative form-group">
                                                    <label>Email Id<sup>**</sup></label>
                                                    <asp:TextBox ID="txt_emailid" runat="server" CssClass="form-control" type="email"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-4">
                                        <div class="row">
                                            <div class="col-xl-6">
                                                <div class="position-relative form-group">
                                                    <label>Branch Code<sup>**</sup></label>
                                                    <asp:TextBox ID="txt_BranchCode" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-xl-6">
                                                <div class="position-relative form-group">
                                                 
                                                  
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                               
                                 

                                    <div class="col-md-12">
                                        <asp:Button ID="btn_Submit" runat="server" Text="Add" CssClass="btn btn-primary" OnClick="btn_Submit_Click" Style="padding: 7px 40px;" />
                                        <asp:Button ID="btn_cancele" runat="server" Text="Cancel" CssClass="btn btn-danger" OnClick="btn_cancele_Click" Style="padding: 7px 40px;" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <asp:HiddenField ID="hd_id" runat="server" />
</asp:Content>
