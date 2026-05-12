<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="upload-student-details.aspx.cs" Inherits="school_web.Admin.upload_student_details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Upload Student Details via Excel
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        th {
            font-weight: 500;
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
                <div class="breadcrumb-title pe-3">Setup</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Upload Student Details via Excel</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <div class="row">
                        <div class="col-xl-8">
                            <div class="card">
                                <div class="card-body">
                                    <div class="p-4 border rounded">
                                        <div class="row g-3 needs-validation" novalidate="">
                                            <div class="col-md-3" style="display: none">
                                                <label for="validationCustom01" class="form-label">Category<sup>*</sup></label>
                                                <asp:DropDownList ID="ddl_category" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_category_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-3" style="display: none">
                                                <label for="validationCustom01" class="form-label">Sub-Category<sup>*</sup></label>
                                                <asp:DropDownList ID="ddl_subcategory" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                            </div>

                                            <div class="col-md-3">
                                                <label for="validationCustom01" class="form-label">Session <sup>*</sup></label>
                                                <asp:DropDownList ID="ddlsession" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-4">
                                                <label for="validationCustom01" class="form-label">Choose Excel(.csv file)<sup>*</sup></label>
                                                <asp:FileUpload ID="FileUpload1" runat="server" class="form-control find-dv-txtbx" />
                                            </div>

                                            <div class="col-3">
                                                <asp:Button ID="btn_Submit" runat="server" Text="Upload" CssClass="btn btn-primary" OnClick="btn_Submit_Click1" Style="margin: 23px 0px 0px 0px; padding: 4px 10px;" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-xl-4">
                            <div class="card">
                                <div class="card-body">
                                    <div class="p-4 border rounded">
                                        <div class="row g-3 needs-validation" novalidate="">
                                            <div class="col-md-12">
                                                <a href="doc/Student-details-format.csv" download="" class="btnbtn btn-1 btn-sep icon-cart">Download Excel</a>
                                                <br />
                                                <a href="doc/student_upload_user_manual.xlsx" download="" class="btnbtn btn-2 btn-sep icon-info">Download User Manual</a>


                                                <%--<button class="btn btn-1 btn-sep icon-info">Download Excel Format</button>
                                                <button class="btn btn-2 btn-sep icon-cart">Download User Manual</button>
                                                <button class="btn btn-3 btn-sep icon-heart">Button</button>
                                                <button class="btn btn-4 btn-sep icon-send">Button</button>--%>
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
                        <h6 class="mb-0 text-uppercase">Uploaded Student Details</h6>
                        <hr />
                        <div class="card">
                            <div class="card-body">
                                <div class="table-responsive">
                                    <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <asp:Label ID="lbl_total" runat="server" Text="Label"></asp:Label>
                                                <asp:GridView ID="grvExcelData" class="table table-striped table-bordered dataTable" runat="server" CssClass="table table-bordered" Width="100%">
                                                </asp:GridView>
                                                <div class="col-4">
                                                    <asp:Button ID="btn_final_submit" runat="server" Text="Final Submit" CssClass="btn btn-primary" OnClick="btn_final_submit_Click" Style="margin: 29px 0px 0px 0px; padding: 6px 10px;" />
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

    <!--end page wrapper -->
    <asp:HiddenField ID="hd_id" runat="server" />
</asp:Content>
