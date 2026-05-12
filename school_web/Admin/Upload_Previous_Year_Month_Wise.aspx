<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Upload_Previous_Year_Month_Wise.aspx.cs" Inherits="school_web.Admin.Upload_Previous_Year_Month_Wise" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Upload Previous Year Dues Month Wise
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
                <div class="breadcrumb-title pe-3">Admission</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Upload Student Dues</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <ul class="sub-pag-menu-ul sub-pag-menu-ul-mrgn">
                        <li><a href="form-sale-fee.aspx">Set Form Sale Fees</a></li>
                        <li><a href="misc-fee-month-wise.aspx">Set Miscellaneous Fees Month Wise</a></li>
                        <li><a href="misc-fee-student-wise.aspx">Set Miscellaneous Fees Student Wise</a></li>
                        <li><a href="Upload_Previous_Year_Month_Wise.aspx" class="sub-mnu-p-a-active">Upload Previous Year Dues Month Wise</a></li>
                        <li><a href="View_Monthly_Previous_Dues.aspx">View Added Dues Month Wise</a></li>
                        <li style="display: none"><a href="set-cheque-bounce-fee.aspx">Set Cheque Bounce Fees</a></li>
                    </ul>

                    <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Upload Student Dues"></asp:Label>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation" novalidate="">

                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Session <sup>*</sup></label>
                                        <asp:DropDownList ID="ddlsession" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                    </div>

                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Month<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_month" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                    </div>

                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Browse Excel(.csv file)<sup>*</sup></label>
                                        <asp:FileUpload ID="FileUpload1" runat="server" class="form-control find-dv-txtbx" />
                                        <a href="doc/dues_format_month_wise.csv" download="" style="margin: 5px 0px 0px 0px; float: left; font-weight: 500;">Download Excel Formate</a>
                                    </div>

                                    <div class="col-4">
                                        <asp:Button ID="btn_Submit" runat="server" Text="Upload" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btn_Submit_Click" Style="margin: 0px 0px 0px 0px; padding: 6px 10px;" />
                                        <asp:Button ID="btn_reset" runat="server" Text="Reset" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btn_reset_Click" Style="margin: 0px 0px 0px 0px; padding: 6px 10px; background: #bbb; border: 1px solid #ababab;" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase">Uploaded Details</h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <asp:Panel ID="pnl_grid" runat="server" Visible="false">
                                                <asp:Label ID="lbl_total" runat="server" Text="Label"></asp:Label>

                                                <asp:GridView ID="grvExcelData" class="table table-striped table-bordered dataTable" runat="server" CssClass="table table-bordered" Width="100%">
                                                </asp:GridView>

                                                <div class="col-4">
                                                    <asp:Button ID="btn_final_submit" runat="server" Text="Final Submit" CssClass="btn btn-primary" OnClick="btn_final_submit_Click" Style="margin: 29px 0px 0px 0px; padding: 6px 10px;" />
                                                    <asp:Button ID="btn_excel" runat="server" Text="Excel Download" CssClass="btn btn-primary" Visible="false" OnClick="btn_excel_Click" Style="margin: 29px 0px 0px 20px; padding: 6px 10px;" />
                                                </div>
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

    <!--end page wrapper -->
    <asp:HiddenField ID="hd_id" runat="server" />
</asp:Content>
