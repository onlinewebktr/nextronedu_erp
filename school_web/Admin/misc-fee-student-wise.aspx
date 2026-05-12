<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="misc-fee-student-wise.aspx.cs" Inherits="school_web.Admin.misc_fee_student_wise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Miscellaneous Fees Student Wise
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

    <style>
        .modal {
            background: rgb(0 0 0 / 50%);
            padding-right: 0px !important;
            padding: 50px 0px 0px 0px;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            var sessionid = $("#<%=ddl_session_st.ClientID%>").val();
            $("#<%=txt_admission_no.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'fee-collection-monthly-wise.aspx/GetRooPathAdmNo',
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
                <div class="breadcrumb-title pe-3">Fees Setup</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Miscellaneous Fees</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <ul class="sub-pag-menu-ul sub-pag-menu-ul-mrgn">
                        <li><a href="form-sale-fee.aspx">Form Sale Fees</a></li>
                        <li><a href="misc-fee-month-wise.aspx">Month Misc Fee</a></li>
                        <li><a href="misc-fee-student-wise.aspx" class="sub-mnu-p-a-active">Student-Wise Misc Fee</a></li>
                    </ul>
                </div>
                <hr />
                <div class="col-xl-12">
                    <div class="card">
                        <div class="card-body">
                            <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Add Misc. Fees Student Wise"></asp:Label>
                            <hr />
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation" novalidate="">
                                    <div class="col-md-2">
                                        <label for="validationCustom01" class="form-label">Session<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_session_st" runat="server" class="form-select" AutoPostBack="true"></asp:DropDownList>
                                    </div>

                                    <div class="col-md-2">
                                        <label for="validationCustom01" class="form-label">Admission No<sup>*</sup></label>
                                        <asp:TextBox ID="txt_admission_no" runat="server" class="form-control" AutoPostBack="true" OnTextChanged="txt_admission_no_TextChanged"></asp:TextBox>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:Button ID="btn_find_studnt" runat="server" Text="Find" CssClass="btn btn-primary" OnClick="btn_find_studnt_Click" Style="margin: 24px 0px 0px 0px; padding: 4px 10px 3px;" />
                                    </div>

                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Name<sup>*</sup></label>
                                        <asp:TextBox ID="txt_name" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <label for="validationCustom01" class="form-label">Class<sup>*</sup></label>
                                        <asp:TextBox ID="txt_class" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <label for="validationCustom01" class="form-label">Section<sup>*</sup></label>
                                        <asp:TextBox ID="txt_section" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <label for="validationCustom01" class="form-label">Roll<sup>*</sup></label>
                                        <asp:TextBox ID="txt_roll" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>

                                    <div class="col-md-2">
                                        <label for="validationCustom01" class="form-label">Month<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_month" runat="server" class="form-select"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label" style="position: relative; width: 100%;">Particular<sup>*</sup> <a href="#!" data-toggle="modal" data-target="#myModalHead" class="addmore-btns"><span class="material-symbols-outlined" style="font-size: 17px;">add</span></a></label>
                                        <asp:DropDownList ID="ddl_misc_fee_Type" runat="server" class="form-select"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <label for="validationCustom01" class="form-label">Amount<sup>*</sup></label>
                                        <asp:TextBox ID="txt_amount_st" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3" style="display: none">
                                        <label for="validationCustom01" class="form-label">Ledger effected<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_ledger_st" runat="server" class="form-select"></asp:DropDownList>
                                    </div>
                                    <div class="col-12">
                                        <asp:Button ID="btn_Submit" runat="server" Text="Add" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btn_Submit_Click1" />
                                        <asp:Button ID="btn_cancel" runat="server" Text="Cancel" class="btn btn-dark" Visible="false" CausesValidation="false" OnClick="btn_cancel_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


                <div class="col-xl-12">
                    <div class="card">
                        <div class="card-body">
                            <h6 class="mb-0 text-uppercase">Added Misc. Fees Student Wise</h6>
                            <hr />
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <table id="example2" data-page-length='1500000' class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                <thead>
                                                    <tr>
                                                        <th>#</th>
                                                        <th>Admission No.</th>
                                                        <th>Month</th>
                                                        <th>Session</th>
                                                        <th>Particular</th>
                                                        <th>Amount</th>
                                                        <th>Action</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="rd_view" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="lbl_admission_no" runat="server" Text='<%#Bind("Admission_No")%>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="lbl_month" runat="server" Text='<%#Bind("Month")%>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("Session")%>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="lbl_perticular" runat="server" Text='<%#Bind("Perticular")%>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("Amount")%>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Edit"> <i class="lni lni-pencil-alt"> </i></asp:LinkButton>
                                                                    <asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                                    <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lbl_session_id" runat="server" Text='<%#Bind("Session_id")%>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lbl_ledger" runat="server" Text='<%#Bind("Ledger")%>' Visible="false"></asp:Label>
                                                                </td>
                                                            </tr>
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
    <div class="modal fade" id="myModalHead" role="dialog" style="top: 0px">
        <div class="modal-dialog md-width" style="max-width: 500px; margin: 5.75rem auto;">
            <!-- Modal content-->
            <div class="modal-content" style="position: relative">
                <div class="modal-header" style="padding: 3px 10px;">
                    <h5 class="modal-title" style="font-size: 18px; padding: 5px 0px 0px 0px;">Add Fee Head</h5>
                    <a href="#!" data-dismiss="modal" style="margin: 5px 0px 0px 0px !important; padding: 3px 5px 4px 5px; background: #fa2020; border: #9b0202;"
                        class="btn btn-primary find-dv-btn">Close</a>
                </div>
                <div class="modal-body" style="padding: 5px 5px;">
                    <div class="p-4 border rounded" style="float: left; width: 100%; padding: 5px 5px !important;">
                        <div class="mdl-frm-row">
                            <label for="validationCustom01" class="find-dv-lbl">Fee Head Name</label>
                            <div class="row">
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txt_fee_head" runat="server" class="form-control"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                    <asp:Button ID="btn_add_fee_head" OnClick="btn_add_fee_head_Click" runat="server" Text="Save" class="button-6161 disc-pop-save_disc" Style="margin: 0px 0px 0px 0px; height: 30px;" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--end page wrapper -->
    <asp:HiddenField ID="hd_id" runat="server" />


    <script type="text/javascript">
        function openModalHead() {
            $('#myModalHead').modal('show');
        }
    </script>
</asp:Content>
