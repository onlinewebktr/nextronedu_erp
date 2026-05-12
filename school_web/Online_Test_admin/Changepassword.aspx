<%@ Page Title="" Language="C#" MasterPageFile="~/Online_Test_admin/Admin.Master" AutoEventWireup="true" CodeBehind="Changepassword.aspx.cs" Inherits="school_web.Online_Test_admin.Changepassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server"> Change Password
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
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
                <div class="breadcrumb-title pe-3">Change Password</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Change Password</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-4"></div>
                <div class="col-xl-4">
                    <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text=" Change Password"></asp:Label>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation" novalidate="">
                                    <div class="col-md-12">
                                        <label for="validationCustom01" class="form-label">Old Password<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_oldpassword"></asp:RequiredFieldValidator></sup></label>
                                        <asp:TextBox ID="txt_oldpassword" runat="server"  class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-12">
                                        <label for="validationCustom01" class="form-label">New Password<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_newpaswword"></asp:RequiredFieldValidator></sup></label>
                                        <asp:TextBox ID="txt_newpaswword" runat="server" TextMode="Password" class="form-control"></asp:TextBox>
                                    </div>


                                    <div class="col-md-12">
                                        <label for="validationCustom01" class="form-label">Re-Enter Password<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_reenterpassword"></asp:RequiredFieldValidator></sup></label>
                                        <asp:TextBox ID="txt_reenterpassword" runat="server" TextMode="Password" class="form-control"></asp:TextBox>
                                    </div>

                                    <div class="col-12">
                                        <asp:Button ID="btn_Submit" runat="server" Text="Change Password" TextMode="Password" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btn_Submit_Click1" />

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-xl-4"></div>

            </div>
        </div>
        <!--end row-->
    </div>
</asp:Content>
