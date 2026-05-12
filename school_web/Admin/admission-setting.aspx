<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="admission-setting.aspx.cs" Inherits="school_web.Admin.admission_setting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Admission No. Setting
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        sup, sub {
            color: red;
            top: -0.3em;
            font-size: 16px;
            font-weight: 600;
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
                            <li class="breadcrumb-item active" aria-current="page">Admission No. Setting</li>
                        </ol>
                    </nav>
                </div>
            </div>

            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-4">
                    <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Admission No. Setting"></asp:Label>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation" novalidate="">
                                    <div class="col-md-12">
                                        <label for="validationCustom01" class="form-label">Session<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_session" runat="server" class="form-select"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-12">
                                        <label for="validationCustom01" class="form-label">Is Admission No. Auto Create<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_is_adm_no_auto_create" runat="server" class="form-select">
                                            <asp:ListItem>NO</asp:ListItem>
                                            <asp:ListItem>YES</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-12" id="dv1">
                                        <div class="row g-3 needs-validation" novalidate="">
                                            <div class="col-md-6">
                                                <label for="validationCustom01" class="form-label">Prefix Code<sup>*</sup></label>
                                                <asp:TextBox ID="txt_prefix" runat="server" oninput="this.value = this.value.toUpperCase()" class="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-6">
                                                <label for="validationCustom01" class="form-label">Session Code<sup>*</sup></label>
                                                <asp:TextBox ID="txt_session_code" runat="server" oninput="this.value = this.value.toUpperCase()" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12" id="dv2">
                                        <label for="validationCustom01" class="form-label">Admission No. Start From<sup>*</sup></label>
                                        <asp:TextBox ID="txt_adm_start_from" runat="server" onkeypress="return isNumberKey(event)" oninput="this.value = this.value.toUpperCase()" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-12" id="dv3">
                                        <asp:Button ID="btn_Submit" runat="server" Text="Add" CssClass="btn btn-primary" OnClick="btn_Submit_Click" />
                                        <asp:Button ID="btn_cancel" runat="server" Text="Cancel" class="btn btn-dark" Visible="false" CausesValidation="false" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-xl-8">
                    <h6 class="mb-0 text-uppercase">Admission No. Setting</h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <table id="example21" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                <thead>
                                                    <tr>
                                                        <th>#</th>
                                                        <th>Session</th>
                                                        <th>Prefix Code</th>
                                                        <th>Session Code</th>
                                                        <th>S.l. Start From</th>
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
                                                                        <asp:Label ID="lbl_Session" runat="server" Text='<%#Bind("Session")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="Label1" runat="server" Text='<%#Bind("Prefix_Code")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="Label2" runat="server" Text='<%#Bind("Session_code")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="Label3" runat="server" Text='<%#Bind("Admission_no_start_from")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:LinkButton ID="lnk_bnr_status" class="lnk-btn" runat="server" Text="Size" Style="min-width: 50px;" OnClick="lnk_bnr_status_Click1"></asp:LinkButton>
                                                                        <asp:Label ID="lbl_show_status" Visible="false" runat="server" Text='<%#Bind("Status") %>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <%--<asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Edit"> <i class="lni lni-pencil-alt"> </i></asp:LinkButton>--%>
                                                                        <asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                                        <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
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
        <!--end row-->
    </div>

    <!--end page wrapper -->
    <asp:HiddenField ID="hd_id" runat="server" />
    <script type="text/javascript">
        $(document).ready(function () {
            on_is_adm_auto_generate_selection();
            $("#<%=ddl_is_adm_no_auto_create.ClientID%>").on('change', function () {
                on_is_adm_auto_generate_selection();
            })
        });

        function on_is_adm_auto_generate_selection() {
            $("#sectors").show();
            if ($('#<%= ddl_is_adm_no_auto_create.ClientID %> option:selected').val() == "YES") {
                $("#dv1").show();
                $("#dv2").show();
                $("#dv3").show();
            }
            else {
                $("#dv1").hide();
                $("#dv2").hide();
                $("#dv3").hide();
            }
        }

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 46 && charCode > 31
                && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
    </script>
</asp:Content>
