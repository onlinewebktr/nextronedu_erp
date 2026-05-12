<%@ Page Title="" Language="C#" MasterPageFile="~/Examination_Admin/Admin.Master" AutoEventWireup="true" CodeBehind="bulk-mark-entry.aspx.cs" Inherits="school_web.Examination_Admin.bulk_mark_entry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Bulk Mark Entry
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .form-select {
            height: 37px;
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

            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3" style="position: relative;">
                <div class="breadcrumb-title pe-3">Marks Entry</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a></li>
                            <li class="breadcrumb-item active" aria-current="page">Marks Entry
                                <asp:LinkButton ID="print1" Visible="false" OnClientClick="return PrintPanel()" CssClass="print-btn" runat="server" ToolTip="Print" Style="position: absolute; right: 0px; top: 6px;"><span class="material-symbols-outlined">print</span></asp:LinkButton>
                            </li>
                        </ol>
                    </nav>
                </div>
            </div>
            <div class="row">
                <div class="col-xl-12">
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation" novalidate="">
                                    <div class="col-sm-3">
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <label for="validationCustom01" class="form-label">Session<sup>*</sup></label>
                                                <asp:DropDownList ID="ddlsession" runat="server" class="form-select"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-6">
                                                <label for="validationCustom01" class="form-label">Class<sup>*</sup></label>
                                                <asp:DropDownList ID="ddl_CourseCat" class="form-select find-dv-txtbx" runat="server" Style="width: 100%" AutoPostBack="true" OnSelectedIndexChanged="ddl_CourseCat_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-9">
                                        <div class="row">
                                            <div class="col-md-2">
                                                <label for="validationCustom01" class="form-label">Term<sup>*</sup></label>
                                                <asp:DropDownList ID="ddl_term" class="form-select find-dv-txtbx" runat="server" Style="width: 98%" AutoPostBack="true" OnSelectedIndexChanged="ddl_term_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-3">
                                                <label for="validationCustom01" class="form-label">Assessment<sup>*</sup></label>
                                                <asp:DropDownList ID="ddl_assesment" class="form-select find-dv-txtbx" runat="server" Style="width: 98%"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-5">
                                                <label for="validationCustom01" class="form-label">Choose excel<sup>*</sup></label>
                                                <asp:FileUpload ID="FileUpload1" runat="server" class="form-select find-dv-txtbx" />
                                            </div>
                                            <div class="col-md-1" style="padding-left: 0px">
                                                <asp:Button ID="btn_find" runat="server" Text="Submit" CssClass="btn btn-primary" Style="margin: 22px 0px 0px 0px; padding: 8px 10px 7px;" OnClick="btn_find_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


                <asp:Panel ID="pnl_grid" runat="server" Visible="false">
                    <div class="col-xl-12">
                        <h6 class="mb-0 text-uppercase">Uploaded Marks Details</h6>
                        <hr />
                        <div class="card">
                            <div class="card-body">
                                <div class="table-responsive">
                                    <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <%--<asp:Label ID="lbl_total" runat="server" Text="Label"></asp:Label>--%>
                                                <asp:GridView ID="grvExcelData" class="table table-striped table-bordered dataTable" runat="server" CssClass="table table-bordered" Width="100%">
                                                </asp:GridView>
                                                <div class="col-4">
                                                    <asp:Button ID="btn_final_submit" runat="server" OnClick="btn_final_submit_Click1" Text="Final Submit" CssClass="btn btn-primary" Style="margin: 29px 0px 0px 0px; padding: 6px 10px;" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
        <!--end row-->
    </div>




    <style>
        .grd-txtbx-clas {
            margin: 0px 10px 0px 0px;
            padding: 3px 7px;
            border: 0px;
            border-bottom: 1px solid #6208d5;
            background: rgb(255 255 255 / 0%);
            width: 100px;
            float: left;
        }

        .grd-remks {
            margin: 0px 0px 0px 0px;
            padding: 0px;
            border: 0px;
            font-size: 16px;
            color: #6208d5;
        }
        .modal {
            background: rgb(0 0 0 / 39%);
        }
    </style>
</asp:Content>
