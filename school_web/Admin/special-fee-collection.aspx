<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="special-fee-collection.aspx.cs" Inherits="school_web.Admin.special_fee_collection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Special Fee Collection
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        th {
            font-weight: 500;
        }

        input[type=checkbox], input[type=radio] {
            background: #000;
            border-style: none;
            width: 25px;
            height: 25px;
            position: relative;
            top: 8.6px;
            left: 0px;
            margin: 0px 10px 0px 0px;
            z-index: 9999;
        }

        .rowdevider {
            margin: 0px;
            padding: 5px 0px 5px 0px;
            width: 100%;
            float: left;
            border-bottom: 1px solid #ddd;
        }
    </style>

    <script type="text/javascript">
        $(function () {
            var sessionid = $("#<%=ddl_session_student.ClientID%>").val();
            $("#<%=txt_student_name.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'Taken_Other_Fee_From_Student.aspx/GetRooPath',
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
            var sessionid = $("#<%=ddlsessionad.ClientID%>").val();
            $("#<%=txt_admission_no.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'Taken_Other_Fee_From_Student.aspx/GetRooPathAdmNo',
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
            $("#<%=txt_payee_bank_name.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'special-fee-collection.aspx/GetBankName',
                        data: "{ 'PathRooT': '" + request.term + "'}",
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
                            }
                            else {
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
                <div class="breadcrumb-title pe-3">Fees Collections</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Special Fee Collection</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row" id="plnStdFindDv" runat="server">
                                    <div class="col-xl-3">
                                        <div class="fnd-box-wpr">
                                            <h2 class="fnd-box-row-wpr-h">Find by Admission No.</h2>
                                            <div class="fnd-box-wpr-inr">
                                                <div class="fnd-box-row-wpr">
                                                    <div class="row">
                                                        <div class="col-xl-6 padd-rght-5">
                                                            <label for="validationCustom01" class="form-label-fnds">Session</label>
                                                        </div>
                                                        <div class="col-xl-6 padd-lft-5">
                                                            <asp:DropDownList ID="ddlsessionad" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="fnd-box-row-wpr">
                                                    <div class="row">
                                                        <div class="col-xl-6 padd-rght-5">
                                                            <label for="validationCustom01" class="form-label-fnds">Admission No.</label>
                                                        </div>
                                                        <div class="col-xl-6 padd-lft-5">
                                                            <asp:TextBox ID="txt_admission_no" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="fnd-box-row-wpr">
                                                    <div class="row">
                                                        <div class="col-xl-6 padd-rght-5">
                                                        </div>
                                                        <div class="col-xl-6 padd-lft-5">
                                                            <asp:Button ID="btn_find_admission_no" runat="server" Text="Find" CssClass="btn btn-primary form-fnd-btns" OnClick="btn_find_admission_no_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-xl-5">
                                        <div class="fnd-box-wpr">
                                            <h2 class="fnd-box-row-wpr-h">Find by Roll No.</h2>
                                            <div class="fnd-box-wpr-inr">
                                                <div class="row">
                                                    <div class="col-xl-6 padd-rght-5">
                                                        <div class="fnd-box-row-wpr">
                                                            <div class="row">
                                                                <div class="col-xl-5 padd-rght-5">
                                                                    <label for="validationCustom01" class="form-label-fnds">Class</label>
                                                                </div>
                                                                <div class="col-xl-7 padd-lft-5">
                                                                    <asp:DropDownList ID="ddlclass" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-xl-6 padd-lft-5">
                                                        <div class="fnd-box-row-wpr">
                                                            <div class="row">
                                                                <div class="col-xl-5 padd-rght-5">
                                                                    <label for="validationCustom01" class="form-label-fnds">Section</label>
                                                                </div>
                                                                <div class="col-xl-7 padd-lft-5">
                                                                    <asp:TextBox ID="txt_section" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-xl-6 padd-rght-5">
                                                        <div class="fnd-box-row-wpr">
                                                            <div class="row">
                                                                <div class="col-xl-5 padd-rght-5">
                                                                    <label for="validationCustom01" class="form-label-fnds">Session</label>
                                                                </div>
                                                                <div class="col-xl-7 padd-lft-5">
                                                                    <asp:DropDownList ID="ddlsession" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-xl-6 padd-lft-5">
                                                        <div class="fnd-box-row-wpr">
                                                            <div class="row">
                                                                <div class="col-xl-5 padd-rght-5">
                                                                    <label for="validationCustom01" class="form-label-fnds">Roll No.</label>
                                                                </div>
                                                                <div class="col-xl-7 padd-lft-5">
                                                                    <asp:TextBox ID="txtrollnumber" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-xl-6 padd-lft-5">
                                                        <div class="fnd-box-row-wpr">
                                                            <div class="row">
                                                                <div class="col-xl-5">
                                                                </div>
                                                                <div class="col-xl-7 padd-lft-5">
                                                                    <asp:Button ID="btnfind" runat="server" Text="Find" CssClass="btn btn-primary form-fnd-btns" OnClick="btnfind_Click" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-xl-4">
                                        <div class="fnd-box-wpr">
                                            <h2 class="fnd-box-row-wpr-h">Find by Student Name</h2>
                                            <div class="fnd-box-wpr-inr">
                                                <div class="fnd-box-row-wpr">
                                                    <div class="row">
                                                        <div class="col-xl-5 padd-rght-5">
                                                            <label for="validationCustom01" class="form-label-fnds">Session</label>
                                                        </div>
                                                        <div class="col-xl-7 padd-lft-5">
                                                            <asp:DropDownList ID="ddl_session_student" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="fnd-box-row-wpr">
                                                    <div class="row">
                                                        <div class="col-xl-5 padd-rght-5">
                                                            <label for="validationCustom01" class="form-label-fnds">Student Name</label>
                                                        </div>
                                                        <div class="col-xl-7 padd-lft-5">
                                                            <asp:TextBox ID="txt_student_name" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="fnd-box-row-wpr">
                                                    <div class="row">
                                                        <div class="col-xl-5 padd-rght-5">
                                                        </div>
                                                        <div class="col-xl-7 padd-lft-5">
                                                            <asp:Button ID="btn_find_name" runat="server" Text="Find" CssClass="btn btn-primary form-fnd-btns" OnClick="btn_find_name_Click" />
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
                                                <h2 class="fnd-box-row-wpr-h">Student Basic Information<a href="special-fee-collection.aspx" class="btn btn-primary form-fnd-btns" style="background: #f00; border: 1px solid #ddd; width: auto; margin: -3px 0px -2px 0px; padding: 3px 10px 2px; float: right;">Find New Student</a></h2>
                                                <div class="fnd-box-wpr-inr">
                                                    <div class="fnd-box-row-wpr">
                                                        <div class="row">
                                                            <div class="col-xl-5 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-4 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Student Name : </label>
                                                                    </div>
                                                                    <div class="col-xl-8 padd-lft-5">
                                                                        <asp:Label ID="lbl_name" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-5 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-4 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Father's Name : </label>
                                                                    </div>
                                                                    <div class="col-xl-8 padd-lft-5">
                                                                        <asp:Label ID="lbl_father_name" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-2 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-5 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Class : </label>
                                                                    </div>
                                                                    <div class="col-xl-7 padd-lft-5">
                                                                        <asp:Label ID="lblclass" runat="server" Font-Bold="true" Text=" " class="stdnt-info-fnds"></asp:Label>
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
                                                                        <asp:Label ID="txtsection" runat="server" Text=" " Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
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
                                                            <div class="col-xl-2 padd-rght-5">
                                                                <div class="row">
                                                                    <div class="col-xl-4 padd-rght-5">
                                                                        <label for="validationCustom01" class="stdnt-info-fnds">Roll no. : </label>
                                                                    </div>
                                                                    <div class="col-xl-8 padd-lft-5">
                                                                        <asp:Label ID="lbl_roll_no" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
                                                                        <asp:Label ID="lbl_phone" style="display:none" runat="server" Font-Bold="true" class="stdnt-info-fnds"></asp:Label>
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
                                        <div class="col-md-2"></div>
                                        <div class="col-md-8">
                                            <div class="card" style="margin: 10px 0px 0px 0px;">
                                                <div class="card-body">
                                                    <div class="p-4 border rounded" style="width: 100%; float: left">
                                                        <div class="rowdevider">
                                                            <div class="row">
                                                                <div class="col-xl-6 padd-rght-5">
                                                                    <label for="validationCustom01" class="form-label-fnds">Select Fees Type</label>
                                                                </div>
                                                                <div class="col-xl-6 padd-lft-5">
                                                                    <asp:DropDownList ID="ddl_feetype" runat="server" class="form-select find-dv-txtbx" OnSelectedIndexChanged="ddl_feetype_SelectedIndexChanged" AutoPostBack="true">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="rowdevider">
                                                            <div class="row">
                                                                <div class="col-xl-6 padd-rght-5">
                                                                    <label for="validationCustom01" class="form-label-fnds">Fee Amount</label>
                                                                </div>
                                                                <div class="col-xl-6 padd-lft-5">
                                                                    <asp:TextBox ID="txt_fee_amount" ReadOnly="true" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>

                                                                    <asp:HiddenField ID="hd_content_name" runat="server" />
                                                                    <asp:HiddenField ID="hd_content_id" runat="server" />
                                                                    <asp:HiddenField ID="hd_fee_amount" runat="server" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="rowdevider">
                                                            <div class="row">
                                                                <div class="col-xl-6 padd-rght-5">
                                                                    <label for="validationCustom01" class="form-label-fnds">Payment Date</label>
                                                                </div>
                                                                <div class="col-xl-6 padd-lft-5">
                                                                    <div class="clndr-div">
                                                                        <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                                                        <asp:TextBox ID="txt_payment_date" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="rowdevider">
                                                            <div class="row">
                                                                <div class="col-xl-6 padd-rght-5">
                                                                    <label for="validationCustom01" class="form-label-fnds">Payment Mode</label>
                                                                </div>
                                                                <div class="col-xl-6 padd-lft-5">
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
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div id="bank_dt">
                                                            <div class="rowdevider">
                                                                <div class="row">
                                                                    <div class="col-xl-6 padd-rght-5">
                                                                        <label for="validationCustom01" class="form-label-fnds">Bank Name</label>
                                                                    </div>
                                                                    <div class="col-xl-6 padd-lft-5">
                                                                        <asp:DropDownList ID="ddl_bank" runat="server" class="form-select"></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="rowdevider">
                                                                <div class="row">
                                                                    <div class="col-xl-6 padd-rght-5">
                                                                        <label for="validationCustom01" class="form-label-fnds">Bank Date</label>
                                                                    </div>
                                                                    <div class="col-xl-6 padd-lft-5">
                                                                        <div class="clndr-div">
                                                                            <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                                                            <asp:TextBox ID="txt_bank_date" runat="server" class="form-control"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="rowdevider">
                                                                <div class="row">
                                                                    <div class="col-xl-6 padd-rght-5">
                                                                        <label for="validationCustom01" class="form-label-fnds">Payee Bank Name</label>
                                                                    </div>
                                                                    <div class="col-xl-6 padd-lft-5">
                                                                        <asp:TextBox ID="txt_payee_bank_name" runat="server" class="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="rowdevider" id="pnl_mode_t_nS">
                                                            <div class="row">
                                                                <div class="col-xl-6 padd-rght-5">
                                                                    <asp:Label ID="lbl_mode_trns_no" runat="server" class="form-label" Style="font-size: 14px; margin: 8px 0px 3px 0px; float: left; width: 100%;" Text="Transaction No."></asp:Label>
                                                                </div>
                                                                <div class="col-xl-6 padd-lft-5">
                                                                    <asp:TextBox ID="txt_transaction_no" runat="server" class="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="rowdevider">
                                                            <div class="row">
                                                                <div class="col-xl-6 padd-rght-5">
                                                                    <label for="validationCustom01" class="form-label-fnds">Remarks</label>
                                                                </div>
                                                                <div class="col-xl-6 padd-lft-5">
                                                                    <asp:TextBox ID="txt_remarks" runat="server" class="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="rowdevider">
                                                            <div class="row">
                                                                <div class="col-xl-6 padd-rght-5"></div>
                                                                <div class="col-xl-6 padd-lft-5">
                                                                    <asp:Button ID="btn_make_payment" runat="server" Text="Pay" CssClass="btn btn-primary form-fnd-btns" OnClick="btn_make_payment_Click" Style="margin: 8px 0px 10px -4px;" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-3"></div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--end row-->

    <div class="conf-alrt-sec" id="myModal2" runat="server" visible="false">
        <div class="conf-alrt-inr" style="width: 750px;">
            <div class="popupTablWpR">
                <div class="row">
                    <div class="col-md-6">
                        <h2 class="popup-dt-h">Student Details</h2>
                    </div>
                    <div class="col-md-6">
                        <ul class="conf-btn-ul" style="margin: 0px 0px 0px 0px;">
                            <li>
                                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Close</asp:LinkButton>
                            </li>
                        </ul>
                    </div>
                </div>

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
    <style>
        .conf-btn-ul li a {
            margin: 0px 5px;
            padding: 0px 0px 1px;
            text-decoration: none;
            background: #ff0000;
            color: #fff;
            width: 50px;
            float: right;
            text-align: center;
            border-radius: 3px;
            font-size: 13px;
            line-height: 25px;
            font-weight: 500;
        }

        table tr th {
            padding: 10px 5px !important;
        }
    </style>
    <asp:HiddenField ID="hd_user_Type" runat="server" />




    <script type="text/javascript">
        $(function () {
            $("#<%=txt_payment_date.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "2020:2030",
                maxDate: '0',
            }).attr("readonly", "true");
        });
        $(function () {
            $("#<%=txt_bank_date.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "2020:2030",
                maxDate: '0',
            }).attr("readonly", "true");
        });

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
</asp:Content>
