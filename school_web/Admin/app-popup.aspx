<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="app-popup.aspx.cs" Inherits="school_web.Admin.app_popup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Popup Image for App
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

    <style>
        .status-button {
            padding: 10px 20px;
            margin: 5px;
            border: none;
            border-radius: 8px;
            color: white;
            font-size: 16px;
            cursor: pointer;
        }

        .activated {
            background-color: #28a745; /* Green */
        }

        .inactive {
            background-color: #dc3545; /* Red */
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--start page wrapper -->
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
                <div class="breadcrumb-title pe-3">School</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Popup Image For App</li>
                        </ol>
                    </nav>
                </div>
            </div>


            <div class="row">
                <div class="col-xl-4">
                    <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Update Popup Image"></asp:Label>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation" novalidate="">
                                    <div class="col-md-12">
                                        <label for="validationCustom01" class="form-label">
                                            Choose Image<sup>*<span><asp:RegularExpressionValidator ID="RegularExpressionValidator7"
                                                runat="server" ControlToValidate="FileUpload1"
                                                ErrorMessage="Invalid File. Please upload a File with extension: png, jpg, jpeg" ForeColor="Red"
                                                ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.jpeg|JPG|.JPEG|.PNG)$"
                                                ValidationGroup="D" SetFocusOnError="true" Display="Dynamic" CssClass="error"></asp:RegularExpressionValidator></span></sup></label>
                                        <asp:FileUpload ID="FileUpload1" runat="server" class="form-control" />
                                    </div>


                                    <div class="col-12">
                                        <asp:Button ID="btn_Submit" runat="server" Text="Save" ValidationGroup="D" CssClass="btn btn-primary" OnClick="btn_Submit_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


                <div class="col-xl-8">
                    <h6 class="mb-0 text-uppercase">Added Popup Image </h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <table class="table  table-bordered" runat="server" id="grddv">
                                <tr>
                                    <td>
                                        <asp:HiddenField ID="hd_id" runat="server" />
                                        Status :
                                        <asp:LinkButton ID="lnk_status" runat="server" class="status-button active" OnClick="lnk_status_Click"></asp:LinkButton></td>
                                </tr>
                                <tr>
                                    <td runat="server" id="imgTemp">Image : 
                                         <asp:Image ID="Image1" runat="server" class="img-responsive" Style="max-width: 100%" />
                                    </td>
                                </tr>

                                
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
