<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Create-Certificate-Master.aspx.cs" Inherits="school_web.Admin.Create_Certificate_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Create Certificate Master
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

            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3" style="position: relative">
                <div class="breadcrumb-title pe-3">Setting</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Certificate Master</li>
                        </ol>
                    </nav>
                </div>

            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <ul class="sub-pag-menu-ul" style="margin: 0px 0px 6px 0px;">
                        <li><a href="Create-Certificate-Master.aspx" class="sub-mnu-p-a-active">Certificate Master</a></li>
                        <li><a href="Add-Certificate-Signature.aspx">Upload Signature</a></li>
                        <li><a href="Upload_Header_Certificate.aspx">Certificate Header</a></li>

                    </ul>
                </div>

                <div class="row">
                    <div class="col-xl-4">
                        <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Certificate Master"></asp:Label>
                        <hr style="margin: 5px 0px 6px 0px; height: 1.5px;" />
                        <div class="card">
                            <div class="card-body">
                                <div class="p-4 border rounded">
                                    <div class="row g-3 needs-validation" novalidate="">
                                        <div class="col-md-12">
                                            <label for="validationCustom01" class="form-label">
                                                Certificate Name<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Required Field."
                                                    ControlToValidate="txt_Certificate_name" ValidationGroup="A"></asp:RequiredFieldValidator></sup></label>
                                            <div class="select2-dv">
                                                <asp:TextBox ID="txt_Certificate_name" runat="server" class="form-control"></asp:TextBox>


                                            </div>
                                        </div>



                                        <div class="col-md-12">
                                            <label for="validationCustom01" class="form-label">
                                                Certificate Icon(file type png/jpg)<sup>*<span><asp:RegularExpressionValidator ID="RegularExpressionValidator7"
                                                    runat="server" ControlToValidate="FileUpload1"
                                                    ErrorMessage="Invalid File. Please upload a File with extension: png, jpg, jpeg" ForeColor="Red"
                                                    ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.jpeg|JPG|.JPEG|.PNG)$"
                                                    ValidationGroup="A" SetFocusOnError="true" Display="Dynamic" CssClass="error"></asp:RegularExpressionValidator></span></sup></label>
                                            <asp:FileUpload ID="FileUpload1" runat="server" class="form-control" />
                                        </div>




                                        <div class="col-12">
                                            <asp:Button ID="btn_Submit" runat="server" Text="Save" ValidationGroup="A" CssClass="btn btn-primary" OnClick="btn_Submit_Click" />
                                            <asp:Button ID="btn_cancel" runat="server" Text="Cancel" class="btn btn-dark" Visible="false" CausesValidation="false" OnClick="btn_cancel_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>


                    <div class="col-xl-8">
                        <h6 class="mb-0 text-uppercase">Added Certificate Master </h6>
                        <hr style="margin: 9px 0px 6px 0px; height: 1.5px;" />
                        <div class="card">
                            <div class="card-body">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <table id="example21" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                <thead>
                                                    <tr>
                                                        <th style="text-align: center;">#</th>
                                                        <th style="text-align: center;">Certificate Name</th>
                                                        <th style="text-align: center;">Certificate Icon</th>
                                                        <th style="text-align: center;">Action</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="rd_view" runat="server">
                                                        <ItemTemplate>

                                                            <tr>
                                                                <td style="text-align: center;">
                                                                    <asp:Label ID="lbl_id" Visible="false" runat="server" Text='<%#Bind("id")%>'></asp:Label>
                                                                     <asp:Label ID="lbl_Is_bydefault" Visible="false" runat="server" Text='<%#Bind("Is_bydefault")%>'></asp:Label>

                                                                    <asp:Label ID="lbl_Menu_id" Visible="false" runat="server" Text='<%#Bind("Menu_id")%>'></asp:Label>
                                                                    <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: center;">
                                                                    <asp:Label ID="lbl_Menu_name" runat="server" Text='<%#Bind("Menu_name")%>'></asp:Label>

                                                                </td>
                                                                <td style="text-align: center;">
                                                                    <a target="_blank" href='<%#Eval("Menu_icon") %>'>
                                                                        <asp:Image ID="myImg" runat="server" ImageUrl='<%# Bind("Menu_icon") %>' Style="width: 50%; height: 105px; margin: 0px; border: 2px solid #f93; padding: 2px" />
                                                                    </a>
                                                                </td>
                                                                <td style="text-align: center;">

                                                                    <div class="user-box dropdown" style="float: left; display: inherit; height: auto; border-left: 0px solid #f0f0f0; margin-left: 0px;">
                                                                        <a class="d-flex align-items-center nav-link dropdown-toggle dropdown-toggle-nocaret" style="font-size: 29px; padding: 0px; border: 0px;"
                                                                            href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                                                                            <div class="user-info ps-3" style="padding: 0px !important; border: 0px !important;">
                                                                                <i class="bx bx-grid-horizontal"></i>
                                                                            </div>
                                                                        </a>
                                                                        <ul class="dropdown-menu dropdown-menu-end">
                                                                            <li>

                                                                                <asp:LinkButton ID="lnkEdit" runat="server" class="dropdown-item" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Edit"> <i class="lni lni-pencil-alt"> </i><span>Edit</span></asp:LinkButton>
                                                                            </li>
                                                                            <li>
                                                                                <asp:LinkButton ID="lnkDel" runat="server" class="dropdown-item" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"> </i><span>Delete</span></asp:LinkButton>
                                                                            </li>
                                                                            <li>
                                                                                <asp:LinkButton ID="lnk_Signature_Required" class="dropdown-item" runat="server" CausesValidation="false" OnClick="lnk_Signature_Required_Click" ToolTip="Edit"><i class="lni lni-pencil-alt"></i><span>Signature Required</span></asp:LinkButton>
                                                                            </li>
                                                                            <asp:Label ID="lbl_Menu_icon" Visible="false" runat="server" Text='<%#Bind("Menu_icon")%>'></asp:Label>


                                                                        </ul>
                                                                    </div>
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
    </div>
    <style>
        .modal.fade .modal-dialog {
            transition: transform .3s ease-out;
            transform: translate(0, 0px);
        }

        .modal {
            background: #00000082;
        }
    </style>
    <div id="myModal1" class="modal fade" role="dialog">
        <div class="modal-dialog" style="max-width: 517px; margin-top: 76px;">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" style="width: 76%;">Add Designation</h5>


                    <a href="#!" data-dismiss="modal" style="margin: 5px 0px 0px 0px !important; padding: 3px 5px 4px 5px; background: #fa2020; border: #9b0202;"
                        class="btn btn-secondary noPrint">Close</a>
                </div>
                <div class="modal-body" style="max-height: 400px; overflow: auto;">
                    <div class="row" id="tdprint1">
                        <table style="width: 100%;" class="table">
                            <tr>
                                <td colspan="2">
                                    <table style="width: 100%;" class="table table-bordered">
                                        <tr>
                                            <td>Select Designation   

                                            </td>
                                            <td style="width: 266px;">

                                                <div class="select2-dv">
                                                    <asp:DropDownList ID="ddl_UserName" runat="server" CssClass="single-select"></asp:DropDownList>
                                                </div>
                                            </td>
                                            <td style="text-align: center">
                                                <asp:Button ID="btn_Designation_name" runat="server" Text="Add" OnClick="btn_Designation_name_Click" CssClass="btn btn-primary" ValidationGroup="bb" />
                                            </td>


                                        </tr>

                                    </table>
                                </td>
                            </tr>


                            <tr>
                                <td colspan="2">

                                    <table id="example211" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info" data-page-length='1500'>
                                        <thead>
                                            <tr>
                                                <th>#</th>
                                                <th>Designation</th>

                                                <th style="text-align: center">Action</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="rd_view_Designation" runat="server">
                                                <ItemTemplate>

                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:Label ID="lbl_Designation" runat="server" Text='<%#Bind("Designation_name")%>'></asp:Label>
                                                        </td>


                                                        <td style="text-align: center; padding-right: 0px;">

                                                            <asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click1"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                            <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblId" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                        </td>
                                                    </tr>

                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </td>

                            </tr>

                        </table>

                    </div>

                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">

        function openModal1() {
            $('#myModal1').modal('show');

        }

    </script>
</asp:Content>
