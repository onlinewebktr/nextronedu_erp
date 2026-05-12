<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="vendor-general-expense.aspx.cs" Inherits="school_web.Admin.vendor_general_expense" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    General Expense 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

    <script>
        $(function () {
            $("#<%=txt_date.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                readOnly: true,
                yearRange: "1900:2100",
            }).attr("readonly", "true");
        });

        $(function () {
            $("#<%=txt_bill_date.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                readOnly: true,
                yearRange: "1900:2100",
            }).attr("readonly", "true");
        });

        $(function () {
            $("#<%=txt_cheque_date.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                readOnly: true,
                yearRange: "1900:2100",
            }).attr("readonly", "true");
        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hd_user_Type" runat="server" />
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
                <div class="breadcrumb-title pe-3">General Expense</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active">General Expense </li>
                        </ol>
                    </nav>
                </div>
            </div>


            <div class="row">
                <div class="col-xl-12">
                    <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Add General Expense"></asp:Label>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation">
                                    <div class="col-md-3">
                                        <label class="form-label">Financial Year<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_financial_year" runat="server" class="form-select"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3" runat="server" id="dateDV">
                                        <label class="form-label">Date<sup>*</sup></label>
                                        <asp:TextBox ID="txt_date" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6"></div>
                                    <div class="col-md-3">
                                        <label class="form-label">Vendor<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_vendor" runat="server" class="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddl_vendor_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="form-label">Contact Person<sup>*</sup></label>
                                        <asp:TextBox ID="txt_contact_person" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="form-label">Contact No.<sup>*</sup></label>
                                        <asp:TextBox ID="txt_contact_no" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="form-label">Address<sup>*</sup></label>
                                        <asp:TextBox ID="txt_address" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>



                                    <div class="col-md-3">
                                        <label class="form-label">Is Bill No.<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_is_bill_no" runat="server" class="form-select">
                                            <asp:ListItem>No</asp:ListItem>
                                            <asp:ListItem>Yes</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-3" id="billnoDV">
                                        <label class="form-label">Bill No.<sup>*</sup></label>
                                        <asp:TextBox ID="txt_bill_no" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3" id="billdateDV">
                                        <label class="form-label">Bill Date<sup>*</sup></label>
                                        <asp:TextBox ID="txt_bill_date" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3" id="billamtDV">
                                        <label class="form-label">Bill Amount<sup>*</sup></label>
                                        <asp:TextBox ID="txt_bill_amount" runat="server"  onkeypress="return isNumberKey(event)" class="form-control"></asp:TextBox>
                                    </div>




                                    <div class="col-md-3">
                                        <label class="form-label">Payment Amount<sup>*</sup></label>
                                        <asp:TextBox ID="txt_payment_amount"  onkeypress="return isNumberKey(event)" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label class="form-label">Payment Mode<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_payment_mode" runat="server" class="form-select">
                                            <asp:ListItem>Cash</asp:ListItem>
                                            <asp:ListItem>Cheque</asp:ListItem>
                                            <asp:ListItem>NEFT</asp:ListItem>
                                            <asp:ListItem>Deposote In Bank</asp:ListItem>
                                            <asp:ListItem>Netabanking</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>



                                    <div class="col-md-3" id="checknoDv">
                                        <label class="form-label">Cheque No.<sup>*</sup></label>
                                        <asp:TextBox ID="txt_cheque_no" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3" id="checkdateDv">
                                        <label class="form-label">Cheque Date<sup>*</sup></label>
                                        <asp:TextBox ID="txt_cheque_date" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3" id="checkbanknameDv">
                                        <label class="form-label">Bank Name<sup>*</sup></label>
                                        <asp:TextBox ID="txt_bank_name" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3" id="utrnoDv">
                                        <label class="form-label">UTR No./Transaction No.<sup>*</sup></label>
                                        <asp:TextBox ID="txt_utr_no" runat="server" class="form-control"></asp:TextBox>
                                    </div>

                                     <div class="col-md-3">
                                        <label class="form-label">Payment Type<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_vendor_type" runat="server" class="form-select">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-3"  >
                                        <label class="form-label">Remarks<sup>*</sup></label>
                                        <asp:TextBox ID="txt_remarks" runat="server" class="form-control" TextMode="MultiLine"  style="height:50px"></asp:TextBox>
                                    </div>


                                </div>


                                <div id="payhndovrDV" style="padding: 5px; border: 1px solid #ddd; margin: 15px 0px 0px 0px; border-radius: 4px;">
                                    <h2 style="font-size: 18px; font-weight: 500;">Payment Handover</h2>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label class="form-label">Name<sup>*</sup></label>
                                            <asp:TextBox ID="txt_pay_hndover_name" runat="server" class="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-4" id="empcodEdV">
                                            <label class="form-label">Employee Code<sup></sup></label>
                                            <asp:TextBox ID="txt_emp_code" runat="server" class="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-4" id="empmbdV">
                                            <label class="form-label">Mobile No.<sup>*</sup></label>
                                            <asp:TextBox ID="txt_emp_mobile_no"  onkeypress="return isNumberKey(event)" MaxLength="10" runat="server" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div style="margin: 10px 0px 0px 0px;">
                                    <div class="row">
                                        <div class="col-12">
                                            <asp:Button ID="btn_Submit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btn_Submit_Click" />
                                            <asp:Button ID="btn_cancel" runat="server" Text="Cancel" class="btn btn-dark" Visible="false" CausesValidation="false" OnClick="btn_cancel_Click" />
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

    <asp:HiddenField ID="hd_id" runat="server" />
    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 46 && charCode > 31
              && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }

        //============================
        $(document).ready(function () {
            on_is_bill_selection();
            $("#<%=ddl_is_bill_no.ClientID%>").on('change', function () {
                on_is_bill_selection();
            })
        });

        function on_is_bill_selection() {
            $("#sectors").show();
            if ($('#<%= ddl_is_bill_no.ClientID %> option:selected').val() == "Yes") {
                    $("#billnoDV").show();
                    $("#billdateDV").show();
                    $("#billamtDV").show();
                }
                else {
                    $("#billnoDV").hide();
                    $("#billdateDV").hide();
                    $("#billamtDV").hide();
                }
            }


            //============================
            $(document).ready(function () {
                on_is_paymode_selection();
                $("#<%=ddl_payment_mode.ClientID%>").on('change', function () {
                    on_is_paymode_selection();
                })
            });

            function on_is_paymode_selection() {
                $("#sectors").show();
                if ($('#<%= ddl_payment_mode.ClientID %> option:selected').val() == "Cheque") {
                    $("#checknoDv").show();
                    $("#checkdateDv").show();
                    $("#checkbanknameDv").show();
                    $("#utrnoDv").hide();
                    $("#payhndovrDV").show();
                    $("#empcodEdV").show();
                    $("#empmbdV").show();
                }
                else if ($('#<%= ddl_payment_mode.ClientID %> option:selected').val() == "NEFT") {
                    $("#checknoDv").hide();
                    $("#checkdateDv").hide();
                    $("#checkbanknameDv").hide();
                    $("#utrnoDv").show();
                    $("#payhndovrDV").show();
                    $("#empcodEdV").show();
                    $("#empmbdV").show();
                }
                else if ($('#<%= ddl_payment_mode.ClientID %> option:selected').val() == "Deposote In Bank") {
                    $("#checknoDv").hide();
                    $("#checkdateDv").hide();
                    $("#checkbanknameDv").hide();
                    $("#utrnoDv").show();
                    $("#payhndovrDV").show();
                    $("#empcodEdV").show();
                    $("#empmbdV").show();
                }
                else if ($('#<%= ddl_payment_mode.ClientID %> option:selected').val() == "Netabanking") {
                    $("#checknoDv").hide();
                    $("#checkdateDv").hide();
                    $("#checkbanknameDv").hide();
                    $("#utrnoDv").show();
                    $("#payhndovrDV").show();
                    $("#empcodEdV").show();
                    $("#empmbdV").show();
                }
                else {
                    $("#checknoDv").hide();
                    $("#checkdateDv").hide();
                    $("#checkbanknameDv").hide();
                    $("#utrnoDv").hide();
                    $("#payhndovrDV").show();
                    $("#empcodEdV").show();
                    $("#empmbdV").show();
                }
            }
    </script>
</asp:Content>
