<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="notice-template-setting.aspx.cs" Inherits="school_web.Admin.notice_template_setting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Notice Template Setting
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function openModalAdds() {
            $('#Mdladd').modal('show');

        }
    </script>

    <style>
        .add-popup-ul {
            margin: -15px 0px 0px 0px;
            padding: 0px;
            float: right;
        }

            .add-popup-ul li {
                margin: 0px;
                padding: 0px;
                list-style-type: none;
            }

                .add-popup-ul li a {
                    margin: 0px;
                    padding: 5px 5px 7px 5px;
                    background: #006e8f;
                    border-radius: 2px;
                    color: #fff;
                }

        .modal.fade .modal-dialog {
            transform: translate(0, 0px);
        }

        .modal {
            background: rgb(0 0 0 / 55%);
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
                <div class="breadcrumb-title pe-3">Master</div>
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



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">

                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase">Added Template</h6>
                    <ul class="add-popup-ul">
                        <li><a href="#!" data-toggle="modal" data-target="#Mdladd"><span>+</span> Add Template</a></li>
                    </ul>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <asp:Panel ID="Panel1" runat="server">
                                                <table id="example21" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                    <thead>
                                                        <tr>
                                                            <th>#</th>
                                                            <th>Notice Type</th>
                                                            <th>Notice Template</th>
                                                            <th>Status</th>
                                                            <th>Action</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="rd_view" runat="server" OnItemDataBound="rd_view_ItemDataBound">
                                                            <ItemTemplate>
                                                                <asp:Panel ID="Panel1" runat="server">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_notice_type_name" runat="server" Text='<%#Bind("Notice_type_name")%>'></asp:Label>
                                                                            <asp:Label ID="lbl_notice_id" Visible="false" runat="server" Text='<%#Bind("Notice_type")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_notice_message" runat="server" Text='<%#Bind("Notice_message")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:LinkButton ID="lnk_status" OnClick="lnk_status_Click" runat="server"></asp:LinkButton>
                                                                            <asp:Label ID="lbl_status" Visible="false" runat="server" Text='<%#Bind("Status")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Edit"> <i class="lni lni-pencil-alt"> </i></asp:LinkButton>
                                                                            <asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                                            <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </asp:Panel>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </tbody>
                                                </table>
                                            </asp:Panel>
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


    <asp:HiddenField ID="hd_id" runat="server" />
    <div id="Mdladd" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog" style="max-width: 500px;">
            <div class="modal-content">
                <div class="modal-header" style="padding: 3px 17px;">
                    <h5 class="modal-title" style="font-size: 18px; padding: 5px 0px 0px 0px;">Add Template</h5>
                    <a href="#!" data-dismiss="modal" style="margin: 5px 0px 0px 0px !important; padding: 3px 5px 4px 5px; background: #fa2020; border: #9b0202;"
                        class="btn btn-primary find-dv-btn">Close</a>
                </div>
                <div class="modal-body">
                    <div class="p-4 border rounded" style="float: left; width: 100%;">
                        <div class="disc-tbl-wprs">
                            <div class="row g-3 needs-validation" novalidate="">
                                <div class="col-md-12">
                                    <label for="validationCustom01" class="form-label">Notice Type<sup>*</sup></label>
                                    <asp:DropDownList ID="ddl_templete_type" runat="server" class="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddl_templete_type_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-md-12">
                                    <label for="validationCustom01" class="form-label">Enter Notice<sup>*</sup></label>
                                    <asp:TextBox ID="txt_notice" TextMode="MultiLine" runat="server" class="form-control" Style="height: 160px"></asp:TextBox>
                                    <asp:Label ID="lbl_variables" runat="server" Style="background: #efff00; padding: 3px 5px; float: left; margin: 5px 0px 0px 0px; border-radius: 5px; font-weight: 500; border: 1px solid #dfe300;"></asp:Label>
                                </div>
                                <div class="col-12">
                                    <asp:Button ID="btn_Submit" runat="server" Text="Add" CssClass="btn btn-primary" OnClick="btn_Submit_Click" />
                                    <asp:Button ID="btn_cancel" runat="server" Text="Cancel" class="btn btn-dark" Visible="false" CausesValidation="false" OnClick="btn_cancel_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
