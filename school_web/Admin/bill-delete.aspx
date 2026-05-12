<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="bill-delete.aspx.cs" Inherits="school_web.Admin.bill_delete" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Delete Bill
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        label {
            display: inline-block;
            font-size: 19px;
        }

        input[type=checkbox], input[type=radio] {
            background: #000;
            border-style: none;
            width: 30px;
            height: 30px;
            position: relative;
            top: 8.6px;
            left: 0px;
            margin: 0px 10px 0px 0px;
            z-index: 9999;
        }

        .mdl-frm-row {
            margin: 0px 0px 10px 0px;
            padding: 0px;
            width: 100%;
            float: left;
        }

        .modal {
            background: rgb(0 0 0 / 39%);
        }

            .modal.fade .modal-dialog {
                transition: transform .3s ease-out;
                transform: translate(0, 0px);
            }

        .modal-header {
            padding: .5rem 1rem;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            var sessionid = $("#<%=ddl_session_name.ClientID%>").val();
            $("#<%=txt_student_name.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'bill-delete.aspx/GetRooPath',
                        data: "{ 'PathRooT': '" + request.term + "',Session_id:'" + sessionid + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            if (data.d.length > 0) {
                                response($.map(data.d, function (item) {
                                    return {
                                        label: item
                                    };
                                }))
                            } else {
                                response([{ label: 'No results found.' }]);
                            }
                        }
                    });
                },
                select: function (e, u) {
                    if (u.item.val == -1) {
                        return false;
                    }
                }
            });
        });

        $(function () {
            var sessionid = $("#<%=ddl_session.ClientID%>").val();
            $("#<%=txt_admission_no.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'bill-delete.aspx/GetRooPathAdmNo',
                        data: "{ 'PathRooT': '" + request.term + "',Session_id:'" + sessionid + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            if (data.d.length > 0) {
                                response($.map(data.d, function (item) {
                                    return {
                                        label: item
                                    };
                                }))
                            } else {
                                response([{ label: 'No results found.' }]);
                            }
                        }
                    });
                },
                select: function (e, u) {
                    if (u.item.val == -1) {
                        return false;
                    }
                }
            });
        });
    </script>
    <script>
        $(function () {
            $("#<%=txt_date_new.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                readOnly: true,
                yearRange: "1900:2100",
            }).attr("readonly", "true");
        });

        $(function () {
            $("#<%=txt_bank_date.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                readOnly: true,
                yearRange: "1900:2100",
                maxDate: 0   // आज से आगे की date disable
            }).attr("readonly", "true");
        });
        $(function () {
            $("#<%=txt_bank_dateRevised.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                readOnly: true,
                yearRange: "1900:2100",
                maxDate: 0   // आज से आगे की date disable
            }).attr("readonly", "true");
        });
        function openModalDeleteBill() {
            $('#myModal1').modal('show');
        }
        function openModalEditBill() {
            $('#myModalEdit').modal('show');
        }
        function studentInfo() {
            $('#myModalStudentInfo').modal('show');
        }
    </script>
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
                <div class="breadcrumb-title pe-3">Student Update</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Update/Delete Payment Slip</li>
                        </ol>
                    </nav>
                </div>
            </div>


            <div class="row">
                <div class="col-xl-12">
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row">
                                    <div class="col-xl-6">
                                        <div class="fnd-box-wpr">
                                            <h2 class="fnd-box-row-wpr-h">Find by Admission No.</h2>
                                            <div class="fnd-box-wpr-inr">
                                                <div class="fnd-box-row-wpr">
                                                    <div class="row">
                                                        <div class="col-xl-4">
                                                            <label for="validationCustom01" class="form-label-fnds" style="font-size: 14px">Session</label>
                                                            <asp:DropDownList ID="ddl_session" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true"></asp:DropDownList>
                                                        </div>
                                                        <div class="col-xl-5">
                                                            <label for="validationCustom01" class="form-label-fnds" style="font-size: 14px">Admission No.</label>
                                                            <asp:TextBox ID="txt_admission_no" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                        </div>
                                                        <div class="col-xl-3">
                                                            <asp:Button ID="btn_find_admission_no" runat="server" Text="Find" CssClass="btn btn-primary form-fnd-btns" Style="margin: 22px 0px 0px 0px;" OnClick="btn_find_admission_no_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-xl-6">
                                        <div class="fnd-box-wpr">
                                            <h2 class="fnd-box-row-wpr-h">Find by Student Name</h2>
                                            <div class="fnd-box-wpr-inr">
                                                <div class="fnd-box-row-wpr">
                                                    <div class="row">
                                                        <div class="col-xl-4">
                                                            <label for="validationCustom01" class="form-label-fnds" style="font-size: 14px">Session</label>
                                                            <asp:DropDownList ID="ddl_session_name" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true"></asp:DropDownList>
                                                        </div>
                                                        <div class="col-xl-5">
                                                            <label for="validationCustom01" class="form-label-fnds" style="font-size: 14px">Student Name</label>
                                                            <asp:TextBox ID="txt_student_name" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                        </div>
                                                        <div class="col-xl-3">
                                                            <asp:Button ID="btn_find_by_name" runat="server" Text="Find" CssClass="btn btn-primary form-fnd-btns" Style="margin: 22px 0px 0px 0px;" OnClick="btn_find_by_name_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <asp:Panel ID="std_basic_infoS" runat="server" Visible="false">
                                    <div class="row">
                                        <div class="col-xl-12">
                                            <div class="fnd-box-wpr">
                                                <h2 class="fnd-box-row-wpr-h">Student Basic Information</h2>
                                                <div class="fnd-box-wpr-inr">
                                                    <div class="fnd-box-row-wpr">
                                                        <div class="row">
                                                            <div class="col-xl-5 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-4 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Student Name : </label>
                                                                    </div>
                                                                    <div class="col-xl-8 padd-lft-5">
                                                                        <asp:Label ID="lbl_name" runat="server" class="stdnt-info-fnds" Font-Bold="true"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-5 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-4 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Father's Name : </label>
                                                                    </div>
                                                                    <div class="col-xl-8 padd-lft-5">
                                                                        <asp:Label ID="lbl_father_name" runat="server" class="stdnt-info-fnds " Font-Bold="true"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-2 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-5 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Class : </label>
                                                                    </div>
                                                                    <div class="col-xl-7 padd-lft-5">
                                                                        <asp:Label ID="lblclass" runat="server" class="stdnt-info-fnds" Font-Bold="true"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-xl-5 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-4 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Section : </label>
                                                                    </div>
                                                                    <div class="col-xl-8 padd-lft-5">
                                                                        <asp:Label ID="txtsection" runat="server" class="stdnt-info-fnds" Font-Bold="true"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-5 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-4 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Roll No. : </label>
                                                                    </div>
                                                                    <div class="col-xl-8 padd-lft-5">
                                                                        <asp:Label ID="lbl_old_roll_no" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-5 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-4 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Admission No. : </label>
                                                                    </div>
                                                                    <div class="col-xl-8 padd-lft-5">
                                                                        <asp:Label ID="lbl_admission_no" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-5 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-4 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Hostel : </label>
                                                                    </div>
                                                                    <div class="col-xl-8 padd-lft-5">
                                                                        <asp:Label ID="lblhostel" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-xl-5 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-4 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Transportation : </label>
                                                                    </div>
                                                                    <div class="col-xl-8 padd-lft-5">
                                                                        <asp:Label ID="lbltransporttion" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-5 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-4 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Contact no. : </label>
                                                                    </div>
                                                                    <div class="col-xl-8 padd-lft-5">
                                                                        <asp:Label ID="lbl_phone" runat="server" Text="7250408680" class="stdnt-info-fnds"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>



                                    <div class="row">
                                        <div class="col-xl-12">
                                            <div style="margin: 0px; padding: 0%; float: left; height: auto; width: 100%" id="pnl_payment_history" runat="server" visible="false">
                                                <table class="table">
                                                    <tr>
                                                        <td colspan="8" style="padding: 0px 0px 0px 5px;"><b>Payment History</b></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="8">
                                                            <asp:Label ID="lbl_msg" runat="server" Font-Bold="true" ForeColor="Black"></asp:Label>
                                                            <asp:GridView ID="grd_fee" runat="server" AutoGenerateColumns="False" Style="width: 100%" class="table table-striped table-bordered dataTable" OnRowDataBound="grd_fee_RowDataBound" ShowFooter="True">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Sl. No.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_sln" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Slip No.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_slipno" runat="server" Text='<%#Bind("Slip_no") %>'></asp:Label>
                                                                            <asp:Label ID="lbl_Addmission_no" runat="server" Text='<%#Bind("Addmission_no") %>' Visible="false"></asp:Label>
                                                                            <asp:Label ID="lbl_Class_id" runat="server" Text='<%#Bind("Class_id") %>' Visible="false"></asp:Label>
                                                                            <asp:Label ID="lbl_Branchid" runat="server" Text='<%#Bind("Branch") %>' Visible="false"></asp:Label>
                                                                            <asp:Label ID="lbl_Session" runat="server" Text='<%#Bind("Session") %>' Visible="false"></asp:Label>

                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Date">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_date" runat="server" Text='<%#Bind("Date") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Payment Mode">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_paymenetmode" runat="server" Text='<%#Bind("mode") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>Total</FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Type" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_Type" runat="server" Text='<%#Bind("Type") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>Total</FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Total Amount">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_Amount" runat="server" Text='<%#Bind("Amount","{0:n}") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="td2" Width="100px" />
                                                                        <HeaderStyle CssClass="td2" />
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lbl_totalamount" runat="server" Font-Bold="true"></asp:Label>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Action">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_Transection_in" Visible="false" runat="server" Font-Bold="true" Text='<%#Bind("Transection_in")%>'> </asp:Label>
                                                                            <asp:Label ID="lbl_bank_name" Visible="false" runat="server" Font-Bold="true" Text='<%#Bind("Bank_name")%>'> </asp:Label>
                                                                            <asp:Label ID="lbl_bank_date" Visible="false" runat="server" Font-Bold="true" Text='<%#Bind("Bank_date")%>'> </asp:Label>
                                                                            <asp:Label ID="lbl_transaction_no" Visible="false" runat="server" Font-Bold="true" Text='<%#Bind("Pay_mode_transaction_no")%>'> </asp:Label>
                                                                            <asp:Label ID="lbl_remarkss" Visible="false" runat="server" Font-Bold="true" Text='<%#Bind("Remarks")%>'> </asp:Label>
                                                                            <asp:Label ID="lbl_id" Visible="false" runat="server" Font-Bold="true" Text='<%#Bind("Id")%>'> </asp:Label>
                                                                            <asp:LinkButton ID="lnk_edit_bill" OnClick="lnk_edit_bill_Click" runat="server" class="button-61 nowordbreak collect-feesss" Style="min-width: 36px;"><span class="material-symbols-outlined">edit_square</span></asp:LinkButton>
                                                                            <asp:LinkButton ID="lnk_delete_bill" OnClick="lnk_delete_bill_Click" CausesValidation="false" runat="server" Style="background-color: #f7f100; min-width: 30px; color: #000;" class="button-61 nowordbreak collect-feesss"><span class="material-symbols-outlined">delete</span></asp:LinkButton>

                                                                            <asp:LinkButton ID="lnk_amount_edit" ToolTip="Pay Amount Update" OnClick="lnk_amount_edit_Click" runat="server" class="button-61 nowordbreak collect-feesss" Style="min-width: 36px; display: none"><span class="material-symbols-outlined">credit_score
</span></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div id="myModal1" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Remarks</h5>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                </div>
                <div class="modal-body">

                    <div class="p-4 border rounded">
                        <div class="row">
                            <div class="col-md-12">
                                <label for="validationCustom01" class="form-label" style="font-size: 14px;">Enter Remark</label>
                                <asp:TextBox ID="txt_remark" TextMode="MultiLine" runat="server" class="form-control"></asp:TextBox>
                                <asp:Button ID="btn_conf_delete" Style="margin: 10px 10px 0px 0px;" OnClick="btn_conf_delete_Click" runat="server" Text="Submit" class="btn btn-danger find-dv-btn" />
                                <a href="#!" data-dismiss="modal" style="margin: 10px 10px 0px 0px;" class="btn btn-primary find-dv-btn">Close</a>
                            </div>
                        </div>
                    </div>


                </div>
            </div>
        </div>
    </div>


    <div id="myModalEdit" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Edit Bill</h5>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                </div>
                <div class="modal-body">
                    <div class="p-4 border rounded">
                        <div class="row">
                            <div class="col-md-12">
                                <label for="validationCustom01" class="form-label" style="font-size: 14px; margin: 0px 0px 3px 0px;">Date<sup>*</sup></label>
                                <div class="clndr-div">
                                    <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                    <asp:TextBox ID="txt_date_new" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <label for="validationCustom01" class="form-label" style="font-size: 14px; margin: 8px 0px 3px 0px;">Payement Mode<sup>*</sup></label>
                                <asp:DropDownList ID="ddl_paymentmode" runat="server" class="form-select find-dv-txtbx">
                                    <asp:ListItem>Cash</asp:ListItem>
                                    <asp:ListItem>Deposited In Bank</asp:ListItem>
                                    <asp:ListItem>UPI</asp:ListItem>
                                    <asp:ListItem>UPI_Cash</asp:ListItem>
                                    <asp:ListItem>Pos</asp:ListItem>
                                    <asp:ListItem>Pos_Cash</asp:ListItem>
                                    <asp:ListItem>Netbanking</asp:ListItem>
                                    <asp:ListItem>Sbdebit</asp:ListItem>
                                    <asp:ListItem>Cheque</asp:ListItem>
                                    <asp:ListItem>NEFT</asp:ListItem>
                                    <asp:ListItem>Debitcard</asp:ListItem>
                                    <asp:ListItem>Creditcard</asp:ListItem>
                                    <asp:ListItem>Otherdcard</asp:ListItem>
                                    <asp:ListItem>Demand Draft(DD)</asp:ListItem>
                                    <asp:ListItem>Online</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row" id="bank_dt">
                            <div class="col-md-6">
                                <label for="validationCustom01" class="form-label" style="font-size: 14px; margin: 8px 0px 3px 0px;">Bank Name<sup>*</sup></label>
                                <asp:DropDownList ID="ddl_bank" runat="server" class="form-select"></asp:DropDownList>
                            </div>
                            <div class="col-md-6">
                                <label for="validationCustom01" class="form-label" style="font-size: 14px; margin: 8px 0px 3px 0px;">Bank Date<sup>*</sup></label>
                                <div class="clndr-div">
                                    <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                    <asp:TextBox ID="txt_bank_date" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row" id="pnl_mode_t_nS">
                            <div class="col-md-12">
                                <asp:Label ID="lbl_mode_trns_no" runat="server" class="form-label" Style="font-size: 14px; margin: 8px 0px 3px 0px; float: left; width: 100%;" Text="Transaction No."></asp:Label>
                                <asp:TextBox ID="txt_transaction_no" runat="server" class="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row" id="slip_no" runat="server" visible="false">
                            <div class="col-md-12">
                                <asp:Label ID="Label1" runat="server" class="form-label" Style="font-size: 14px; margin: 8px 0px 3px 0px; float: left; width: 100%;" Text="Slip No."></asp:Label>
                                <asp:TextBox ID="txt_slip_no_popup" runat="server" class="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <asp:Label ID="Label2" runat="server" class="form-label" Style="font-size: 14px; margin: 8px 0px 3px 0px; float: left; width: 100%;" Text="Remarks"></asp:Label>
                                <asp:TextBox ID="txt_remark_update" runat="server" class="form-control" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <asp:Button ID="btn_update_final" Style="margin: 10px 10px 0px 0px; float: left;"
                                    runat="server" Text="Update" CssClass="btn btn-primary form-fnd-btns" OnClick="btn_update_final_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="myModalStudentInfo" class="modal fade" role="dialog">
        <div class="modal-dialog" style="max-width: 820px;">
            <div class="modal-content">
                <div class="modal-header" style="padding: 5px 10px;">
                    <h5 class="modal-title">Student Details</h5>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                </div>
                <div class="modal-body">
                    <div class="p-4 border rounded" style="float: left; width: 100%;">
                        <table style="width: 100%;" id="Table1" class="table table-hover table-bordered ">
                            <tr>
                                <th>Student Name</th>
                                <th>Admission No</th>
                                <th>Class</th>
                                <th>Section</th>
                                <th>Roll</th>
                                <th>Father's Name</th>
                                <th>Action</th>
                            </tr>
                            <asp:Repeater ID="rp_std" runat="server">
                                <ItemTemplate>
                                    <tr id="row" runat="server">
                                        <td>
                                            <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbladmissionserialnumber" runat="server" Text='<%#Bind("admissionserialnumber") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_Section" runat="server" Text='<%#Bind("Section") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_rollnumber" runat="server" Text='<%#Bind("rollnumber") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_fathername" runat="server" Text='<%#Bind("fathername") %>'></asp:Label>
                                            <asp:Label ID="lbl_session" Visible="false" runat="server" Text='<%#Bind("session") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lnk_select" runat="server" OnClick="lnk_select_Click">Select</asp:LinkButton>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <script type="text/javascript">
        $(document).ready(function () {
            on_payment_mode_selection();
            $("#<%=ddl_paymentmode.ClientID%>").on('change', function () {
                on_payment_mode_selection();
            })
        });

        function on_payment_mode_selection() {
            if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "Cash") {
                $("#pnl_mode_t_nS").hide();
                $("#bank_dt").hide();
            }
            if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "Paid to NBCS") {
                $("#pnl_mode_t_nS").hide();
                $("#bank_dt").hide();
            }
            if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "Netbanking") {
                $("#pnl_mode_t_nS").show();
                $("#<%=lbl_mode_trns_no.ClientID%>").text("Transaction No.");
                $("#bank_dt").show();
            }
            if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "Deposited In Bank") {
                $("#pnl_mode_t_nS").show();
                $("#<%=lbl_mode_trns_no.ClientID%>").text("Transaction No.");
                $("#bank_dt").show();
            }
            if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "Sbdebit") {
                $("#pnl_mode_t_nS").show();
                $("#<%=lbl_mode_trns_no.ClientID%>").text("Transaction No.");
                $("#bank_dt").show();
            }
            if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "Cheque") {
                $("#pnl_mode_t_nS").show();
                $("#<%=lbl_mode_trns_no.ClientID%>").text("Cheque No.");
                $("#bank_dt").show();
            }
            if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "NEFT") {
                $("#pnl_mode_t_nS").show();
                $("#<%=lbl_mode_trns_no.ClientID%>").text("UTR No.");
                $("#bank_dt").show();
            }
            if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "Debitcard") {
                $("#pnl_mode_t_nS").show();
                $("#<%=lbl_mode_trns_no.ClientID%>").text("Transaction No.");
                $("#bank_dt").show();
            }
            if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "Creditcard") {
                $("#pnl_mode_t_nS").show();
                $("#<%=lbl_mode_trns_no.ClientID%>").text("Transaction No.");
                $("#bank_dt").show();
            }
            if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "Otherdcard") {
                $("#pnl_mode_t_nS").show();
                $("#<%=lbl_mode_trns_no.ClientID%>").text("Transaction No.");
                $("#bank_dt").show();
            }
            if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "UPI") {
                $("#pnl_mode_t_nS").show();
                $("#<%=lbl_mode_trns_no.ClientID%>").text("Transaction No.");
                $("#bank_dt").show();
            }
            if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "Demand Draft(DD)") {
                $("#pnl_mode_t_nS").show();
                $("#<%=lbl_mode_trns_no.ClientID%>").text("Transaction No.");
                $("#bank_dt").show();
            }
            if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "Pos") {
                $("#pnl_mode_t_nS").show();
                $("#<%=lbl_mode_trns_no.ClientID%>").text("Transaction No.");
                $("#bank_dt").show();
            }
            if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "Other APP") {
                $("#pnl_mode_t_nS").show();
                $("#<%=lbl_mode_trns_no.ClientID%>").text("Transaction No.");
                $("#bank_dt").show();
            }
            if ($('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "UPI_Cash" || $('#<%= ddl_paymentmode.ClientID %> option:selected').val() == "Pos_Cash") {
                $("#pnl_mode_t_nS").show();
                $("#<%=lbl_mode_trns_no.ClientID%>").text("Transaction No.");
                $("#bank_dt").show();
            }
        }
    </script>

    <%-- RevisedPayment--%>

    <script>
        function openModalRevised() {
            $('#MdlRevisedPayment').modal('show');
        }

        //AdmissioN
        function save_data_admission() {
            var valsubmit = $('#<%=btn_revised_pay.ClientID %>').val();
            if (valsubmit == "Submit") {
                $('#<%=btn_revised_pay.ClientID %>').val('Submitting.. Please Wait..');
                ConfirmAdmission();
                document.getElementById("<%=btn_revised_pay.ClientID %>").click();
            }
            else {
                alert("Already submitted")
            }
        }

        function ConfirmAdmission() {
            var confirm_value_adm
            var isSubmitted = false;
            confirm_value_adm = document.createElement("INPUT");
            confirm_value_adm.type = "hidden";
            confirm_value_adm.name = "confirm_value_adm";
            if (confirm("Do you want to print bill?")) {
                confirm_value_adm.value = "Yes";
            }
            else {
                confirm_value_adm.value = "No";
            }
            document.forms[0].appendChild(confirm_value_adm);
        }
    </script>
    <div id="MdlRevisedPayment" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog" style="max-width: 800px;">
            <div class="modal-content">
                <div class="modal-header" style="padding: 3px 17px;">
                    <h5 class="modal-title" style="font-size: 18px; padding: 5px 0px 0px 0px;">Revise Payment</h5>
                    <a href="#!" data-dismiss="modal" style="margin: 5px 0px 0px 0px !important; padding: 3px 5px 4px 5px; background: #fa2020; border: #9b0202;"
                        class="btn btn-primary find-dv-btn">Close</a>
                </div>
                <div class="modal-body">
                    <div class="p-4 border rounded" style="float: left; width: 100%;">
                        <div class="disc-tbl-wprs">
                            <p style="margin: 0px 0px 5px 0px; font-size: 14px;">
                                Slip Id :
                                <asp:Label ID="lbl_rv_slip_id" runat="server"></asp:Label>
                            </p>
                            <table style="width: 100%;" class="table table-hover table-bordered ">
                                <tr>
                                    <th>Month</th>
                                    <th>Fee Head</th>
                                    <th>Fee Amt.</th>
                                    <th>Disc. Amt.</th>
                                    <th>Payable Amt.</th>
                                    <th>Paid Amt.</th>
                                    <th>Dues Amt.</th>
                                </tr>
                                <asp:Repeater ID="rp_revised" runat="server" OnItemDataBound="rp_revised_ItemDataBound">
                                    <ItemTemplate>
                                        <tr id="row" runat="server">

                                            <td>
                                                <asp:Label ID="Label3" runat="server" Text='<%#Bind("month") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Id" runat="server" Visible="false" Text='<%#Bind("Id") %>'></asp:Label>
                                                <asp:Label ID="lbl_content_id" runat="server" Visible="false" Text='<%#Bind("content_id") %>'></asp:Label>
                                                <asp:Label ID="lbl_feee_head" runat="server" Text='<%#Bind("feetype") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_payble_amt" runat="server" Text='<%#Bind("payable") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_disc_amt" runat="server" Text='<%#Bind("Disc") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_payble_after_disc" runat="server" Text='<%#Bind("Payable_after_disc") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_paid" runat="server" Text='<%#Bind("paid") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_dues" runat="server" Text='<%#Bind("dues") %>'></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <tr>
                                    <td colspan="2" style="text-align: right; font-weight: 700;">Total : </td>
                                    <td>
                                        <asp:Label ID="lbl_rv_ttl_payble_amt" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_rv_ttl_disc_amt" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_rv_ttl_payble_after_disc" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_rv_ttl_paid" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_rv_ttl_dues" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>

                            <div class="prowwprs">
                                <div class="row">
                                    <div class="col-md-3">
                                        <p>Total Amount</p>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="txt_rd_ttl_amt" ReadOnly="true" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="prowwprs">
                                <div class="row">
                                    <div class="col-md-3">
                                        <p>Paid Amount</p>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="txt_rd_paid_amt" runat="server" AutoPostBack="true" OnTextChanged="txt_rd_paid_amt_TextChanged" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="prowwprs">
                                <div class="row">
                                    <div class="col-md-3">
                                        <p>Dues Amount</p>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="txt_rv_dues_amt" ReadOnly="true" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="prowwprs">
                                <div class="row">
                                    <div class="col-md-12">
                                        <label for="validationCustom01" class="form-label" style="font-size: 14px; margin: 8px 0px 3px 0px;">Payement Mode<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_paymentmodeRevised" runat="server" class="form-select find-dv-txtbx">
                                            <asp:ListItem>Cash</asp:ListItem>
                                            <asp:ListItem>Deposited In Bank</asp:ListItem>
                                            <asp:ListItem>UPI</asp:ListItem>
                                            <asp:ListItem>UPI_Cash</asp:ListItem>
                                            <asp:ListItem>Pos</asp:ListItem>
                                            <asp:ListItem>Pos_Cash</asp:ListItem>
                                            <asp:ListItem>Netbanking</asp:ListItem>
                                            <asp:ListItem>Sbdebit</asp:ListItem>
                                            <asp:ListItem>Cheque</asp:ListItem>
                                            <asp:ListItem>NEFT</asp:ListItem>
                                            <asp:ListItem>Debitcard</asp:ListItem>
                                            <asp:ListItem>Creditcard</asp:ListItem>
                                            <asp:ListItem>Otherdcard</asp:ListItem>
                                            <asp:ListItem>Demand Draft(DD)</asp:ListItem>
                                            <asp:ListItem>Online</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row" id="bank_dtRevised">
                                    <div class="col-md-6">
                                        <label for="validationCustom01" class="form-label" style="font-size: 14px; margin: 8px 0px 3px 0px;">Bank Name<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_bankRevised" runat="server" class="form-select"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-6">
                                        <label for="validationCustom01" class="form-label" style="font-size: 14px; margin: 8px 0px 3px 0px;">Bank Date<sup>*</sup></label>
                                        <div class="clndr-div">
                                            <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                            <asp:TextBox ID="txt_bank_dateRevised" runat="server" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="row" id="pnl_mode_t_nSRevised">
                                    <div class="col-md-12">
                                        <asp:Label ID="lbl_mode_trns_noRevised" runat="server" class="form-label" Style="font-size: 14px; margin: 8px 0px 3px 0px; float: left; width: 100%;" Text="Transaction No."></asp:Label>
                                        <asp:TextBox ID="txt_transaction_noRevised" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="prowwprs" style="margin-top: 20px;">
                                <div class="row">
                                    <div class="col-md-3">
                                        <p>Remarks</p>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txt_rv_remark" TextMode="MultiLine" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="prowwprs">
                                <div class="row">
                                    <div class="col-md-3"></div>
                                    <div class="col-md-6">
                                        <div style="overflow: hidden; height: 1px;">
                                            <asp:Button ID="btn_revised_pay" OnClick="btn_revised_pay_Click" runat="server" Text="Submit" CssClass="btn btn-primary" />
                                        </div>
                                        <a onclick="save_data_admission()" class="btn btn-primary">Submit</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            on_payment_mode_selectionRevised();
            $("#<%=ddl_paymentmodeRevised.ClientID%>").on('change', function () {
                  on_payment_mode_selectionRevised();
              })
          });

        function on_payment_mode_selectionRevised() {
            if ($('#<%= ddl_paymentmodeRevised.ClientID %> option:selected').val() == "Cash") {
                  $("#pnl_mode_t_nSRevised").hide();
                  $("#bank_dtRevised").hide();
              }
              if ($('#<%= ddl_paymentmodeRevised.ClientID %> option:selected').val() == "Paid to NBCS") {
                  $("#pnl_mode_t_nSRevised").hide();
                  $("#bank_dtRevised").hide();
              }
              if ($('#<%= ddl_paymentmodeRevised.ClientID %> option:selected').val() == "Netbanking") {
                  $("#pnl_mode_t_nSRevised").show();
                  $("#<%=lbl_mode_trns_noRevised.ClientID%>").text("Transaction No.");
                  $("#bank_dtRevised").show();
              }
              if ($('#<%= ddl_paymentmodeRevised.ClientID %> option:selected').val() == "Deposited In Bank") {
                  $("#pnl_mode_t_nSRevised").show();
                  $("#<%=lbl_mode_trns_noRevised.ClientID%>").text("Transaction No.");
                  $("#bank_dtRevised").show();
              }
              if ($('#<%= ddl_paymentmodeRevised.ClientID %> option:selected').val() == "Sbdebit") {
                  $("#pnl_mode_t_nSRevised").show();
                  $("#<%=lbl_mode_trns_noRevised.ClientID%>").text("Transaction No.");
                  $("#bank_dtRevised").show();
              }
              if ($('#<%= ddl_paymentmodeRevised.ClientID %> option:selected').val() == "Cheque") {
                  $("#pnl_mode_t_nSRevised").show();
                  $("#<%=lbl_mode_trns_noRevised.ClientID%>").text("Cheque No.");
                  $("#bank_dtRevised").show();
              }
              if ($('#<%= ddl_paymentmodeRevised.ClientID %> option:selected').val() == "NEFT") {
                  $("#pnl_mode_t_nSRevised").show();
                  $("#<%=lbl_mode_trns_noRevised.ClientID%>").text("UTR No.");
                  $("#bank_dtRevised").show();
              }
              if ($('#<%= ddl_paymentmodeRevised.ClientID %> option:selected').val() == "Debitcard") {
                  $("#pnl_mode_t_nSRevised").show();
                  $("#<%=lbl_mode_trns_noRevised.ClientID%>").text("Transaction No.");
                  $("#bank_dtRevised").show();
              }
              if ($('#<%= ddl_paymentmodeRevised.ClientID %> option:selected').val() == "Creditcard") {
                  $("#pnl_mode_t_nSRevised").show();
                  $("#<%=lbl_mode_trns_noRevised.ClientID%>").text("Transaction No.");
                  $("#bank_dtRevised").show();
              }
              if ($('#<%= ddl_paymentmodeRevised.ClientID %> option:selected').val() == "Otherdcard") {
                  $("#pnl_mode_t_nSRevised").show();
                  $("#<%=lbl_mode_trns_noRevised.ClientID%>").text("Transaction No.");
                  $("#bank_dtRevised").show();
              }
              if ($('#<%= ddl_paymentmodeRevised.ClientID %> option:selected').val() == "UPI") {
                  $("#pnl_mode_t_nSRevised").show();
                  $("#<%=lbl_mode_trns_noRevised.ClientID%>").text("Transaction No.");
                  $("#bank_dtRevised").show();
              }
              if ($('#<%= ddl_paymentmodeRevised.ClientID %> option:selected').val() == "Demand Draft(DD)") {
                  $("#pnl_mode_t_nSRevised").show();
                  $("#<%=lbl_mode_trns_noRevised.ClientID%>").text("Transaction No.");
                  $("#bank_dtRevised").show();
              }
              if ($('#<%= ddl_paymentmodeRevised.ClientID %> option:selected').val() == "Pos") {
                  $("#pnl_mode_t_nSRevised").show();
                  $("#<%=lbl_mode_trns_noRevised.ClientID%>").text("Transaction No.");
                  $("#bank_dtRevised").show();
              }
              if ($('#<%= ddl_paymentmodeRevised.ClientID %> option:selected').val() == "Other APP") {
                  $("#pnl_mode_t_nSRevised").show();
                  $("#<%=lbl_mode_trns_noRevised.ClientID%>").text("Transaction No.");
                  $("#bank_dtRevised").show();
              }
              if ($('#<%= ddl_paymentmodeRevised.ClientID %> option:selected').val() == "UPI_Cash" || $('#<%= ddl_paymentmodeRevised.ClientID %> option:selected').val() == "Pos_Cash") {
                  $("#pnl_mode_t_nSRevised").show();
                  $("#<%=lbl_mode_trns_noRevised.ClientID%>").text("Transaction No.");
                $("#bank_dtRevised").show();
            }
        }
    </script>
</asp:Content>
