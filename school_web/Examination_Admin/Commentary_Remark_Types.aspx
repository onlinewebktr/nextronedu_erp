<%@ Page Title="" Language="C#" MasterPageFile="~/Examination_Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Commentary_Remark_Types.aspx.cs" Inherits="school_web.Examination_Admin.Commentary_Remark_Types" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Commentary Remark Types
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
                <div class="breadcrumb-title pe-3">Exam Setup</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Commentary Remark Types</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase"></h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">

                            <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="find-dv">
                                            <div class="row">
                                                <div class="col-sm-12">


                                                    <a class="btn btn-success find-dv-btn" onclick="openModal()"  style="margin: 14px 7px 1px 0px !important; float: right; padding: 3px 6px 6px 11px; font-size: 14px;" title="Commentary Remark Types"><i class="bx bx-plus-medical"></i></a>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="grd-wpr">
                                            <asp:GridView ID="grid_grade" runat="server" AutoGenerateColumns="False" Style="width: 100%" class="table table-striped table-bordered dataTable">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="Sl. No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_sln" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remark Types">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Remark_Types" runat="server" Text='<%#Bind("Commentary_Remark_Types") %>'></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_id" runat="server" Text='<%#Bind("Id") %>' Visible="false"></asp:Label>

                                                            <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Edit"> <i class="lni lni-pencil-alt"></i><span> </span></asp:LinkButton>

                                                            <asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"> </i></asp:LinkButton>



                                                        </ItemTemplate>


                                                    </asp:TemplateField>

                                                </Columns>

                                            </asp:GridView>

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



    <%--------------------------------------------------------------%>

    <style>
        @media (min-width: 576px) {
            .modal-dialog {
                max-width: 916px;
                margin: 1.75rem auto;
            }
        }
    </style>
    <!-------popupadd year----->
    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Remark Type</h5>
                    <asp:Button ID="btnclose" runat="server" class="btn btn-secondary" OnClientClick="return close()" Text="Close" />
                </div>
                <div class="modal-body">

                    <div class="p-4 border rounded">
                        <div class="row g-3 needs-validation" novalidate="">
                            <div class="col-md-6">
                                <label for="validationCustom01" class="form-label">Remark Types <sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_Remark_Types"></asp:RequiredFieldValidator></sup></label>
                                <asp:TextBox ID="txt_Remark_Types" runat="server" class="form-control"></asp:TextBox>



                            </div>





                            <div class="col-12">
                                <asp:Button ID="btn_Submit" runat="server" Text="Add" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btn_Submit_Click" />
                                <asp:Button ID="btn_cancel" runat="server" Text="Cancel" OnClientClick="return close()" class="btn btn-dark" Visible="false" CausesValidation="false" OnClick="btn_cancel_Click" />
                            </div>
                        </div>
                    </div>


                </div>
            </div>
        </div>
    </div>
    <div id="fadeup"></div>

    <link href="../Autocomplete/jquery-ui.css" rel="stylesheet" />
    <script src="../Autocomplete/jquery-ui.js"></script>
    <script type="text/javascript">
        function openModal() {
            $("#myModal").show();
            $('#myModal').addClass('show');
            $('#fadeup').addClass('modal-backdrop fade show');
        }
        function close() {
            $("#myModal").hide();
            $('#myModal').removeClass('show');
            $('#fadeup').removeClass('modal-backdrop fade show');
        }
    </script>
    <asp:HiddenField ID="hd_id" runat="server" />
</asp:Content>
