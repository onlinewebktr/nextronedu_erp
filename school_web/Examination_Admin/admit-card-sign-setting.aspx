<%@ Page Title="" Language="C#" MasterPageFile="~/Examination_Admin/Admin.Master" AutoEventWireup="true" CodeBehind="admit-card-sign-setting.aspx.cs" Inherits="school_web.Examination_Admin.admit_card_sign_setting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Signature Setting
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
                <div class="breadcrumb-title pe-3">Admit Card</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Signature Setting</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-4">
                    <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Add Signature"></asp:Label>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation" novalidate="">
                                    <div class="col-sm-12">
                                        <label for="validationCustom01" class="find-dv-lbl">Signature Name</label>
                                        <asp:TextBox ID="txt_sign_name" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-12">
                                        <label for="validationCustom01" class="find-dv-lbl">Position</label>
                                        <asp:DropDownList ID="ddl_position" runat="server" class="form-select">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem Value="1">Left</asp:ListItem>
                                            <asp:ListItem Value="2">Middle</asp:ListItem>
                                            <asp:ListItem Value="3">Right</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-12">
                                        <label for="validationCustom01" class="find-dv-lbl">Upload Sign</label>
                                        <asp:FileUpload ID="FileUpload1" runat="server" class="form-control" />

                                        <div style="display: none">
                                            <asp:Button ID="btn_upload_sign_image" runat="server" OnClick="btn_upload_sign_image_Click" Text="Upload Sign Image" Style="height: 29px; font-size: 12px; font-weight: bold;" />
                                        </div>
                                        <asp:Label ID="lbl_std_img" runat="server" Visible="false"></asp:Label>
                                    </div>

                                    <div class="col-sm-12">
                                        <div class="online_frm-grp">
                                            <asp:Image ID="img_sign_image" runat="server" Visible="false" Style="height: 150px; width: 150px; padding: 2px; border: 2px solid #000;" />
                                        </div>
                                    </div>

                                    <script type="text/javascript">
                                        $('#<%=FileUpload1.ClientID%>').on('change', function () {
                                            $('#<%=btn_upload_sign_image.ClientID%>').click();
                                        })
                                    </script>

                                    <div class="col-sm-12">
                                        <asp:Button ID="btn_Submit" Style="margin: 0px 7px 1px 0px !important;" runat="server" OnClick="btn_Submit_Click" Text="Add" class="btn btn-primary find-dv-btn" />
                                        <asp:Button ID="btn_cancel" runat="server" Text="Cancel" class="btn btn-dark" Visible="false" CausesValidation="false" OnClick="btn_cancel_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:HiddenField ID="hd_id" runat="server" />

                <div class="col-xl-8">
                    <h6 class="mb-0 text-uppercase">Added Signature</h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                    <div class="row">
                                        <div class="col-sm-12"> 
                                            <div id="tblPrintIQ" runat="server">
                                                <div class="prnt-dv-wpr">
                                                    <div id="content">
                                                        <table id="example21" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                            <thead>
                                                                <tr>
                                                                    <th>#</th>
                                                                    <th>Signature Name</th>
                                                                    <th>Position</th>
                                                                    <th>Sign path</th>
                                                                    <th>Action</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <asp:Repeater ID="rd_view" runat="server">
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_sign_name" runat="server" Text='<%#Bind("Sign_name")%>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lbl_position" runat="server" Text='<%#Bind("Position")%>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <a target="_blank" href='<%#Eval("Sign_path") %>'>
                                                                                    <asp:Image ID="myImg" runat="server" ImageUrl='<%# Bind("Sign_path") %>' Style="width: 100px; margin: 0px; border: 2px solid #f93; padding: 2px" />
                                                                                </a>
                                                                                <asp:Label ID="lbl_sign_path" Visible="false" runat="server" Text='<%#Bind("Sign_path")%>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Edit"> <i class="lni lni-pencil-alt"> </i></asp:LinkButton>
                                                                                <asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                                                <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
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


            </div>
        </div>
        <!--end row-->
    </div>
</asp:Content>
