<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Add-Certificate-Signature.aspx.cs" Inherits="school_web.Admin.Add_Certificate_Signature" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Add Signature
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        input[type=checkbox] {
            background: #000;
            border-style: none;
            width: 20px !important;
            height: 20px !important;
            position: relative;
            top: 2.6px;
            left: 0px;
            margin: 0px 10px 0px 0px;
            z-index: 0;
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

            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3" style="position: relative">
                <div class="breadcrumb-title pe-3">Setting</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Add Signature</li>
                        </ol>
                    </nav>
                </div>

            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <ul class="sub-pag-menu-ul" style="margin: 0px 0px 6px 0px;">
                        <li><a href="Create-Certificate-Master.aspx">Certificate Master</a></li>
                        <li><a href="Add-Certificate-Signature.aspx" class="sub-mnu-p-a-active">Upload Signature</a></li>
                        <li><a href="Upload_Header_Certificate.aspx">Certificate Header</a></li>

                    </ul>
                </div>

                <div class="row">
                    <div class="col-xl-4">
                        <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Add Signature"></asp:Label>
                         <hr style="margin: 5px 0px 6px 0px; height: 1.5px;" />
                        <div class="card">
                            <div class="card-body">
                                <div class="p-4 border rounded">
                                    <div class="row g-3 needs-validation" novalidate="">
                                        <div class="col-md-12">
                                            <label for="validationCustom01" class="form-label">Certificate<sup>*</sup></label>
                                            <div class="select2-dv">
                                                <asp:DropDownList ID="ddl_Certificate" runat="server" CssClass="single-select" AutoPostBack="true" OnSelectedIndexChanged="ddl_Certificate_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <label for="validationCustom01" class="form-label">Designation<sup>*</sup></label>
                                            <div class="select2-dv">
                                                <asp:DropDownList ID="ddl_Designation_name" runat="server" CssClass="single-select" AutoPostBack="true" OnSelectedIndexChanged="ddl_Designation_name_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>




                                        <div class="col-md-12">
                                            <label for="validationCustom01" class="form-label">Name<sup>*</sup></label>
                                            <div class="select2-dv">
                                                <asp:DropDownList ID="ddl_username" runat="server" CssClass="single-select">
                                                </asp:DropDownList>
                                            </div>

                                        </div>
                                        <div class="col-md-12">

                                            <asp:CheckBox ID="chk_class_teacher" runat="server" Text="Is Class Teacher?" />


                                        </div>

                                        <div class="col-md-12">


                                            <asp:CheckBox ID="chk_displayedcertificatesignature" runat="server" Text="Display Signature?" />


                                        </div>

                                        <div class="col-md-12">
                                            <label for="validationCustom01" class="form-label">
                                                Signature(file type png/jpg)<sup>*<span><asp:RegularExpressionValidator ID="RegularExpressionValidator7"
                                                    runat="server" ControlToValidate="FileUpload1"
                                                    ErrorMessage="Invalid File. Please upload a File with extension: png, jpg, jpeg" ForeColor="Red"
                                                    ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.jpeg|JPG|.JPEG|.PNG)$"
                                                    ValidationGroup="D" SetFocusOnError="true" Display="Dynamic" CssClass="error"></asp:RegularExpressionValidator></span></sup></label>
                                            <asp:FileUpload ID="FileUpload1" runat="server" class="form-control" />
                                        </div>


                                        <div class="col-md-12">
                                            <label for="validationCustom01" class="form-label">Position<sup>*</sup></label>

                                            <asp:DropDownList ID="ddl_position" runat="server" CssClass="form-select">
                                                <asp:ListItem>1</asp:ListItem>
                                                <asp:ListItem>2</asp:ListItem>
                                                <asp:ListItem>3</asp:ListItem>
                                            </asp:DropDownList>

                                        </div>

                                        <div class="col-12">
                                            <asp:Button ID="btn_Submit" runat="server" Text="Save" ValidationGroup="D" CssClass="btn btn-primary" OnClick="btn_Submit_Click" />
                                            <asp:Button ID="btn_cancel" runat="server" Text="Cancel" class="btn btn-dark" Visible="false" CausesValidation="false" OnClick="btn_cancel_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>


                    <div class="col-xl-8">
                        <h6 class="mb-0 text-uppercase">Added Signature </h6>
                        <hr style="margin: 9px 0px 6px 0px; height: 1.5px;" />
                        <div class="card">
                            <div class="card-body">
                                <div class="table-responsive">
                                    
                                    <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">


                                        <table id="example21" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                            <thead>
                                                <tr>
                                                    <th>#</th>
                                                    <th>Certificate</th>
                                                    <th>Designation</th>
                                                    <th>Name</th>
                                                    <th>Is Class Teacher</th>
                                                    <th>Display Signature</th>
                                                    <th>Status</th>
                                                    <th>Signature</th>
                                                    <th>Position</th>

                                                    <th>Action</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rd_view" runat="server" OnItemDataBound="rd_view_ItemDataBound">
                                                    <ItemTemplate>

                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lbl_id" Visible="false" runat="server" Text='<%#Bind("id")%>'></asp:Label>
                                                                <asp:Label ID="lbl_user_id" Visible="false" runat="server" Text='<%#Bind("user_id")%>'></asp:Label>
                                                                <asp:Label ID="lbl_Menu_id" Visible="false" runat="server" Text='<%#Bind("Menu_id")%>'></asp:Label>
                                                                <asp:Label ID="lbl_Signature" Visible="false" runat="server" Text='<%#Bind("Signature")%>'></asp:Label>
                                                                <asp:Label ID="lbl_Is_signature_display" Visible="false" runat="server" Text='<%#Bind("Is_signature_display")%>'></asp:Label>
                                                                <asp:Label ID="lbl_Is_class_teacher" Visible="false" runat="server" Text='<%#Bind("Is_class_teacher")%>'></asp:Label>
                                                                <asp:Label ID="lbl_Istatus" Visible="false" runat="server" Text='<%#Bind("Istatus")%>'></asp:Label>

                                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbl_Certificate_name" runat="server" Text='<%#Bind("Certificate_name")%>'></asp:Label>

                                                            </td>

                                                            <td>
                                                                <asp:Label ID="lbl_Designation_Name" runat="server" Text='<%#Bind("Designation_Name")%>'></asp:Label>

                                                            </td>


                                                            <td>
                                                                <asp:Label ID="lbl_Name" runat="server" Text='<%#Bind("user_name")%>'></asp:Label>
                                                            </td>

                                                            <td>
                                                                <asp:Label ID="lbl_isclass_teacher" runat="server" Text='<%#Bind("isclass_teacher")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbl_issignature_display" runat="server" Text='<%#Bind("issignature_display")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:LinkButton ID="lnk_bnr_status" class="lnk-btn" runat="server" Text="ON" Style="min-width: 50px;" OnClick="lnk_bnr_status_Click1"></asp:LinkButton>
                                                                <asp:Label ID="lbl_Status" runat="server" Text='<%#Bind("Status")%>' Visible="false"></asp:Label>
                                                            </td>


                                                            <td>
                                                                <a target="_blank" href='<%#Eval("Signature") %>'>
                                                                    <asp:Image ID="myImg" runat="server" ImageUrl='<%# Bind("Signature") %>' Style="width: 99%; height: 50px; margin: 0px; border: 2px solid #f93; padding: 2px" />
                                                                </a>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbl_Position" runat="server" Text='<%#Bind("Position")%>'></asp:Label>
                                                            </td>


                                                            <td>
                                                                <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Edit"> <i class="lni lni-pencil-alt"> </i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"> </i></asp:LinkButton>

                                                            </td>
                                                        </tr>

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
