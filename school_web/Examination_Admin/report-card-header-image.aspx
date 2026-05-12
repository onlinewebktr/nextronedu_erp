<%@ Page Title="" Language="C#" MasterPageFile="~/Examination_Admin/Admin.Master" AutoEventWireup="true" CodeBehind="report-card-header-image.aspx.cs" Inherits="school_web.Examination_Admin.report_card_header_image" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Update Header Template
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
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
                <div class="breadcrumb-title pe-3">Exam Setup</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Template Setting</li>
                        </ol>
                    </nav>
                </div>
            </div>


            <div class="row">
                <div class="col-xl-4">
                    <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Update Template"></asp:Label>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation" novalidate="">
                                    <div class="col-md-12">
                                        <label for="validationCustom01" class="form-label">Type<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_type" runat="server" class="form-control">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem Value="Admit_card">Admit Card</asp:ListItem>
                                            <asp:ListItem Value="Report_card">Report Card</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-12">
                                        <label for="validationCustom01" class="form-label">
                                            Header Template<sup>*<span><asp:RegularExpressionValidator ID="RegularExpressionValidator7"
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
                    <h6 class="mb-0 text-uppercase">Added Template </h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <table id="example21" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                            <thead>
                                                <tr>
                                                    <th>#</th>
                                                    <th>Name</th>
                                                    <th>Templete</th>
                                                    <th>Status</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rd_view" runat="server" OnItemDataBound="rd_view_ItemDataBound1">
                                                    <ItemTemplate>
                                                        <asp:Panel ID="Panel1" runat="server">
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbl_id" Visible="false" runat="server" Text='<%#Bind("id")%>'></asp:Label>
                                                                    <asp:Label ID="lbl_Name" runat="server" Text='<%#Bind("Name")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <a target="_blank" href='<%#Eval("Path") %>'>
                                                                        <asp:Image ID="myImg" runat="server" ImageUrl='<%# Bind("Path") %>' Style="width: 150px; margin: 0px; border: 2px solid #f93; padding: 2px" />
                                                                    </a>
                                                                </td>
                                                                <td>
                                                                    <asp:LinkButton ID="lnk_bnr_status" class="lnk-btn" runat="server" Text="Size" Style="min-width: 50px;" OnClick="lnk_bnr_status_Click1"></asp:LinkButton>
                                                                    <asp:Label ID="lbl_show_status" Visible="false" runat="server" Text='<%#Bind("Status") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </asp:Panel>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
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
