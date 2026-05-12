<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Previeus_Year_Dues_Calculation.aspx.cs" Inherits="school_web.Admin.Previeus_Year_Dues_Calculation" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Previeus Year Dues Calculation
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        input[type=checkbox], input[type=radio] {
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

        label {
            display: inline-block;
            font-weight: bold;
        }

        .form-label {
            font-weight: bold;
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

            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                <div class="breadcrumb-title pe-3">Fees Collection</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Previous Year Dues Calculation</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text=""></asp:Label>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="form-rowdv">

                                    <div class="row">
                                        <div class="col-md-12">
                                            <label for="validationCustom01" class="form-label" style="font-weight: 500; font-size: 1rem; background: #ffd700; padding: 5px 5px 5px 5px; width: 100%; text-align: center; color: #000;">
                                                Download the calculated dues Excel file and check it. After checking the Excel file, click the save button. Once you save, the dues will not be recalculated. 
                                            </label>
                                            <label for="validationCustom01" class="form-label" style="font-weight: 500; font-size: 1rem; background: #ffd700; padding: 5px 5px 5px 5px; width: 100%; text-align: center; color: #000;">
                                                Please do not refresh the page after clicking the save button.
                                            </label>
                                        </div>
                                    </div>
                                </div>

                                <div class="row g-3 needs-validation" novalidate="">

                                    <div class="col-md-2">
                                        <label for="validationCustom01" class="form-label">Session <sup>*</sup></label>
                                        <asp:DropDownList ID="ddlsession" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddlsession_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <label for="validationCustom01" class="form-label">Class<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_Class" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                    </div>
                                     <div class="col-md-4">
                                        <label for="validationCustom01" class="form-label">Is student transferred in the new session?<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_is_transfered" runat="server" class="form-select find-dv-txtbx">
                                            <asp:ListItem Value="1">No</asp:ListItem>
                                            <asp:ListItem Value="2">Yes</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <label for="validationCustom01" class="form-label">With Admission<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_with_admission" runat="server" class="form-select find-dv-txtbx">
                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                            <asp:ListItem Value="2">No</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Button ID="btn_Submit" runat="server" Text="Find" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btn_Submit_Click" Style="margin: 21px 0px 0px 0px; padding: 4px 10px;" />
                                        <asp:Button ID="btn_reset" runat="server" Text="Reset" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btn_reset_Click" Style="margin: 21px 0px 0px 0px; padding: 4px 10px; background: #bbb; border: 1px solid #ababab;" />

                                        <asp:Button ID="btn_excel2" runat="server" Text="Excel Download" CssClass="btn btn-primary" Visible="false" OnClick="btn_excel_Click" Style="margin: 21px 0px 0px 0px; padding: 4px 10px;" />

                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>


                <div class="col-xl-12" id="transferdues" runat="server" visible="false">
                    <h6 class="mb-0 text-uppercase"></h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5">
                                    <div class="row">

                                        <div class="col-xl-12">
                                            <h6 class="mb-0 text-uppercase">Do you want to transfer the dues amount to an annual fee or a monthly fee? </h6>
                                            <hr />
                                            <div class="card">
                                                <div class="card-body">
                                                    <div class="p-4 border rounded">
                                                        <div class="row g-3 needs-validation" novalidate="">

                                                            <div class="col-md-12">
                                                                <div style="margin: 0px; padding: 8px 0px 0px 0px; height: 43px; width: 100%; border-bottom: 1px solid #e3d4d4; text-align: center;">
                                                                    <asp:RadioButton ID="rd_trasfer_anual" runat="server" Text="Annual Fee" GroupName="ab" AutoPostBack="true" OnCheckedChanged="rd_trasfer_anual_CheckedChanged" />
                                                                    <asp:RadioButton ID="rd_transfer_Monthlyfee" runat="server" Text="Monthly Fee" GroupName="ab" AutoPostBack="true" OnCheckedChanged="rd_transfer_Monthlyfee_CheckedChanged" />




                                                                </div>



                                                            </div>
                                                            <div class="col-md-12">

                                                                <div class="col-md-3">
                                                                    <label for="validationCustom01" class="form-label">Session <sup></sup></label>
                                                                    <asp:Label ID="lbl_next_session" runat="server"></asp:Label>
                                                                </div>

                                                                <div class="col-md-3" id="month" runat="server" visible="false">


                                                                    <label for="validationCustom01" class="form-label">Month <sup></sup></label>
                                                                    <asp:DropDownList ID="ddl_month" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                                </div>

                                                            </div>



                                                            <div class="col-md-4">
                                                                &nbsp;<asp:Button ID="btn_excel" runat="server" Text="Excel Download" CssClass="btn btn-primary" Visible="true" OnClick="btn_excel_Click" Style="margin: 21px 0px 0px 0px; padding: 4px 10px;" />

                                                                <asp:Button ID="btn_fina_transfer" runat="server" Text="Save" OnClientClick="return confirm('Are you sure you want to save ?');" OnClick="btn_fina_transfer_Click" CssClass="btn btn-primary" Style="width: 137px; margin: 24px 0px 0px 10px; padding: 4px 0px 4px 0px;" />
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
            </div>
        </div>
        <!--end row-->
    </div>
</asp:Content>
