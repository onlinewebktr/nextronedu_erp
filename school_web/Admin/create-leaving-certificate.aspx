<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="create-leaving-certificate.aspx.cs" Inherits="school_web.Admin.create_leaving_certificate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Create School Leaving Certificate
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript"> 
        $(function () {
            var sessionid = $("#<%=ddl_session.ClientID%>").val();
            $("#<%=txt_student_name.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'create-leaving-certificate.aspx/GeModalTC3tRooPath',
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
                        url: 'create-leaving-certificate.aspx/GetRooPathAdmNo',
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

        function openModalStudentS() {
            $('#MdlStudentS').modal('show');
        }
        function myMdlCertificateP_8() {
            $('#myMdlCertificate_8').modal('show');
        }

        function openModalTCTEN() {
            $('#ModalTCTEN').modal('show');
        }

        function openModalPay() {
            $('#ModalTCFeePay').modal('show');

        }
    </script>

    <style>
        /* .modal.fade .modal-dialog {
            transition: transform .3s ease-out;
            transform: translate(0, 0px);
        }

        .modal {
            background: rgb(0 0 0 / 43%);
        }*/
        .modal.fade .modal-dialog {
            transition: transform .3s ease-out;
            transform: translate(0, 0px);
        }

        .modal {
            background: rgb(0 0 0 / 50%);
            padding-right: 0px !important;
            padding: 50px 0px 0px 0px;
        }
    </style>

    <script type="text/javascript">
        $(function () {
            $("#<%=txt_payee_bank_name.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'create-transfer-certificate.aspx/GetBankName',
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
                <div class="breadcrumb-title pe-3"><a href="certificate-creation.aspx" runat="server" id="backbtns" class="backlnk-css"><i class="bx bx-arrow-back"></i></a>Certificate</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Create School Leaving Certificate</li>
                        </ol>
                    </nav>
                </div>
            </div>


            <div class="row">
                <div class="col-xl-12">
                    <div class="card">
                        <div class="card-body">
                            <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Create School Leaving Certificate"></asp:Label>
                            <hr />
                            <div class="p-4 border rounded">
                                <div class="row" style="border-bottom: 1px solid #ddd; padding: 0px 0px 10px 0px;">
                                    <div class="col-xl-4">
                                        <div class="fnd-box-wpr">
                                            <h2 class="fnd-box-row-wpr-h">Find by Session</h2>
                                            <div class="fnd-box-wpr-inr">
                                                <div class="fnd-box-row-wpr">
                                                    <div class="row">
                                                        <div class="col-xl-4 padd-rght-5">
                                                            <label for="validationCustom01" class="form-label-fnds">Session</label>
                                                        </div>
                                                        <div class="col-xl-4 padd-lft-5">
                                                            <asp:DropDownList ID="ddl_session" runat="server" class="form-select find-dv-txtbx">
                                                            </asp:DropDownList>


                                                        </div>
                                                        <div class="col-xl-4 padd-lft-5">
                                                            <asp:Button ID="btn_find_session" runat="server" Text="Find" CssClass="btn btn-primary form-fnd-btns" OnClick="btn_find_session_Click" />
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="border-bottom: 1px solid #ddd; padding: 0px 0px 10px 0px;">
                                    <div class="col-xl-3">
                                        <div class="fnd-box-wpr">
                                            <h2 class="fnd-box-row-wpr-h">Find by Admission No.</h2>
                                            <div class="fnd-box-wpr-inr">
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
                                                            <asp:Button ID="btn_find_admission_no" runat="server" Text="Find" CssClass="btn btn-primary form-fnd-btns" OnClick="btn_find_admission_no_Click1" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-xl-5">
                                        <div class="fnd-box-wpr">
                                            <h2 class="fnd-box-row-wpr-h">Find by Roll No.</h2>
                                            <div class="fnd-box-wpr-inr" style="padding: 5px 5px 0px 5px;">
                                                <div class="row">
                                                    <div class="col-xl-6 padd-rght-5">
                                                        <div class="fnd-box-row-wpr">
                                                            <div class="row">
                                                                <div class="col-xl-5 padd-rght-5">
                                                                    <label for="validationCustom01" class="form-label-fnds">Class</label>
                                                                </div>
                                                                <div class="col-xl-7 padd-lft-5">
                                                                    <asp:DropDownList ID="ddlclass" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                                    <asp:TextBox ID="txt_admission_no_student_wise" runat="server" Visible="false"></asp:TextBox>
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
                                                </div>
                                                <div class="fnd-box-row-wpr">
                                                    <div class="row">
                                                        <div class="col-xl-6 padd-rght-5">
                                                            <div class="fnd-box-row-wpr">
                                                                <div class="row">
                                                                    <div class="col-xl-5 padd-rght-5">
                                                                    </div>
                                                                    <div class="col-xl-7 padd-lft-5">
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-xl-6 padd-lft-5">
                                                            <div class="fnd-box-row-wpr" style="margin: 2px 0px 3px 0px;">
                                                                <div class="row">
                                                                    <div class="col-xl-5 padd-rght-5">
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
                                    </div>
                                    <div class="col-xl-4">
                                        <div class="fnd-box-wpr">
                                            <h2 class="fnd-box-row-wpr-h">Find by Student Name</h2>
                                            <div class="fnd-box-wpr-inr">
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





                                <div class="table-responsive" style="margin: 10px 0px 0px 0px">
                                    <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <table id="example21" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                    <thead>
                                                        <tr>
                                                            <th>#</th>
                                                            <th>Student Name</th>
                                                            <th>Father's Name</th>
                                                            <th>Adm No.</th>
                                                            <th>Class</th>
                                                            <th>Section</th>
                                                            <th>Session</th>
                                                            <th>Roll No.</th>
                                                            <th>Date of Admission</th>
                                                            <th>Action</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="rd_view" runat="server" OnItemDataBound="rd_view_ItemDataBound">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="Label1" runat="server" Text='<%#Bind("fathername")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_admissionserialnumber" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                    </td>

                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lbl_rollnumber" runat="server" Text='<%#Bind("rollnumber") %>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_dateofadmission" runat="server" Text='<%#Bind("dateofadmission")%>'></asp:Label>
                                                                    </td>

                                                                    <td style="text-align: left;">
                                                                        <asp:LinkButton ID="lnk_pay_fee" runat="server" CausesValidation="false" OnClick="lnk_pay_fee_Click" Style="background: #009b05; padding: 2px 5px 1px 5px; display: inline-flex; color: #fff; border-radius: 2px; width: 78px;"
                                                                            ToolTip="Create certificate">₹ Pay Fee</asp:LinkButton>

                                                                        <asp:LinkButton ID="lnkCreate" runat="server" CausesValidation="false" OnClick="lnkCreate_Click" Style="background: #f00; padding: 2px 5px 1px 5px; display: inline-flex; color: #fff; border-radius: 2px;"
                                                                            ToolTip="Create certificate"> <i class="bx bx-certification"> </i> Create</asp:LinkButton>

                                                                        <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                        <asp:Label ID="lbl_adm_no" runat="server" Text='<%#Bind("admissionserialnumber")%>' Visible="false"></asp:Label>
                                                                        <asp:Label ID="lbl_session_id" runat="server" Text='<%#Bind("Session_id")%>' Visible="false"></asp:Label>
                                                                        <asp:Label ID="lbl_class_id" runat="server" Text='<%#Bind("Class_id")%>' Visible="false"></asp:Label>
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
        </div>
        <!--end row-->
    </div>
    <asp:HiddenField ID="hd_id" runat="server" />
    <asp:HiddenField ID="hd_tc_type" runat="server" />
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

        .rowdevider {
            margin: 0px;
            padding: 5px 0px 5px 0px;
            width: 100%;
            float: left;
            border-bottom: 1px solid #ddd;
        }

        .ttpp-wprs {
            margin: 0px;
            padding: 0px 0px 0px 0px;
            width: 100%;
            height: 400px;
            overflow: scroll;
        }
    </style>

    <!--end page wrapper -->

    <div id="MdlStudentS" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog" style="max-width: 850px;">
            <div class="modal-content">
                <div class="modal-header" style="padding: 3px 17px;">
                    <h5 class="modal-title" style="font-size: 18px; padding: 5px 0px 0px 0px;">Student Details</h5>
                    <a href="#!" data-dismiss="modal" style="margin: 5px 0px 0px 0px !important; padding: 3px 5px 4px 5px; background: #fa2020; border: #9b0202;"
                        class="btn btn-primary find-dv-btn">Close</a>
                </div>
                <div class="modal-body">
                    <div class="p-4 border rounded" style="float: left; width: 100%; padding: 0px !important;">
                        <div class="disc-tbl-wprs" style="overflow: auto;">
                            <table style="width: 100%;" id="Table1" class="table table-hover table-bordered ">
                                <tr>
                                    <th>Student Name</th>
                                    <th>Admission No</th>
                                    <th>Class</th>
                                    <th>Section</th>
                                    <th>Roll</th>
                                    <th>Session</th>
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
                                                <asp:Label ID="Label2" runat="server" Text='<%#Bind("session")%>'></asp:Label>
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
    </div>



    <div id="myMdlCertificate_8" class="modal fade" role="dialog">
        <div class="modal-dialog" style="max-width: 961px;">
            <div class="modal-content">
                <div class="modal-header" style="padding: 3px 17px;">
                    <h5 class="modal-title" style="font-size: 18px; padding: 5px 0px 0px 0px;">Create Certificate</h5>
                    <a href="#!" data-dismiss="modal" style="margin: 5px 0px 0px 0px !important; padding: 3px 5px 4px 5px; background: #fa2020; border: #9b0202;"
                        class="btn btn-primary find-dv-btn">Close</a>
                </div>
                <div class="modal-body">
                    <div class="p-4 border rounded" style="float: left; width: 100%;">
                        <div class="ttpp-wprs" style="height: auto; overflow: inherit;">
                            <table style="width: 100%;" id="Table211" class="table table-hover table-bordered ">
                                <tr>
                                    <th>Student Name</th>
                                    <th>Gender</th>
                                    <th>Admission No.</th>
                                    <th>Dob</th>
                                    <th>Class</th>
                                    <th>Section</th>
                                    <th>Roll</th>
                                    <th>Session</th>
                                    <th>Father's Name</th>
                                    <th>Mother's Name</th>
                                    <th>Address</th>
                                </tr>

                                <asp:Repeater ID="Repeater_8" runat="server">
                                    <ItemTemplate>
                                        <tr id="row" runat="server">
                                            <td>
                                                <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_gender" runat="server" Text='<%#Bind("gender") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbladmissionserialnumber" runat="server" Text='<%#Bind("admissionserialnumber") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_don" runat="server" Text='<%#Bind("dob") %>'></asp:Label>
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
                                                <asp:Label ID="Label2" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_fathername" runat="server" Text='<%#Bind("fathername") %>'></asp:Label>
                                                <asp:Label ID="lbl_session" Visible="false" runat="server" Text='<%#Bind("session") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_mothername" runat="server" Text='<%#Bind("mothername")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_careof" runat="server" Text='<%#Bind("careof")%>'></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>



                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">PEN No.</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_pen_no_8" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Whether the candidate belongs to schedule caste or schedule trible or OBC</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_belong_to_sc_st_obc_8" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>



                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Date of first admission in the school with Class</label>
                                    </div>
                                    <div class="col-xl-4 padd-lft-5">
                                        <asp:TextBox ID="txt_date_of_admission_8" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                    <div class="col-xl-2 padd-lft-5">
                                        <asp:DropDownList ID="ddl_class_c_8" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Class in which the pupil last studied</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">

                                        <asp:TextBox ID="txt_class_in_last_studied_8" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">School/Board Annual examination last taken with result</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_annual_exam_taken_with_result_8" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Whether failed, if so once/twice in the same class</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_whether_failed_in_same_class_8" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>


                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Subject studied</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_subject_studied_8" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Whether qualified for promotion to the higher class</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_whether_qualified_for_promotion_8" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">if so, to which class</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">

                                        <asp:TextBox ID="txt_permote_class" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>



                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Month Upto which the pupil has paid school</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_monthuptopaid_8" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Any fee concession availed of. if so, the nature of such concession</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_concession" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Total working days in the academic session</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_ttl_working_days_8" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Total no. of days pupil present in the school</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_present_days_8" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Whether NCC Cadet/Boy Scout/Girl Guide</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_ncc_cadet_8" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Games played or extra curricular activites in which the pupil usually took part</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_game_played_of_extra_8" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">General conduct</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_general_conduct_8" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>



                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Date of application for certificate</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_date_of_application_8" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Date of issue for certificate</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_date_of_issue_certificate_8" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Reason for leaving the school</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_reason_for_leaving_8" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>



                            <%-- ==================================== --%>
                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Any other remarks</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_any_other_remarks_8" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xl-12 padd-lft-5" style="text-align: center">
                                <asp:Button ID="btn_create_certificate_8" runat="server" Text="Create" CssClass="btn btn-primary form-fnd-btns" OnClick="btn_create_certificate_8_Click" Style="margin: 8px 0px 10px -4px;" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="ModalTCTEN" class="modal fade" role="dialog">
        <div class="modal-dialog" style="max-width: 800px;">
            <div class="modal-content">
                <div class="modal-header" style="padding: 3px 17px;">
                    <h5 class="modal-title" style="font-size: 18px; padding: 5px 0px 0px 0px;">Create Certificate</h5>
                    <a href="#!" data-dismiss="modal" style="margin: 5px 0px 0px 0px !important; padding: 3px 5px 4px 5px; background: #fa2020; border: #9b0202;"
                        class="btn btn-primary find-dv-btn">Close</a>
                </div>
                <div class="modal-body">
                    <div class="p-4 border rounded" style="float: left; width: 100%;">
                        <div class="ttpp-wprs" style="height: auto; overflow: inherit;">
                            <table style="width: 100%;" class="table table-hover table-bordered ">
                                <tr>
                                    <th>Session</th>
                                    <th>Student Name</th>
                                    <th>Admission No</th>
                                    <th>Class</th>
                                    <th>Section</th>
                                    <th>Roll</th>
                                    <th>Father's Name</th>
                                    <th>Mother's Name</th>
                                </tr>
                                <asp:Repeater ID="rp_trnsf_six" runat="server">
                                    <ItemTemplate>
                                        <tr id="row" runat="server">
                                            <td>
                                                <asp:Label ID="Label2" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                            </td>
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
                                                <asp:Label ID="Label3" runat="server" Text='<%#Bind("mothername") %>'></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>


                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Date of Birth (in Christian Era) according to Admission & Withdrawal Register (in figures)</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_dob_ten" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Date of Birth (in Word)</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_dob_in_word" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Nationality</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_nationality_ten" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Whether the candidate belongs to SC/ST/OBC</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_belongs_to_scst_ten" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Date of first admission in the school with class</label>
                                    </div>
                                    <div class="col-xl-4 padd-lft-5">
                                        <asp:TextBox ID="txt_date_of_adm_ten" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                    <div class="col-xl-2 padd-lft-5">
                                        <asp:TextBox ID="txt_adm_class_ten" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Class in which the pupil last studied</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_class_last_studies_ten" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>



                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">School/Board Annual Examination last taken with result</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_school_board_exam_taken_ten" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Whether failed, if so once/twice in the same class</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_failed_once_twice_ten" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>


                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Subject Studied</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_subj_studies_ten" runat="server" TextMode="MultiLine" class="form-control find-dv-txtbx" Style="height: 150px"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Whether qualified for promotion to the higher class if so, to witch class</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_qualified_higher_class_ten" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Month upto which the pupil has paid school dues</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_paid_fee_till_month_ten" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Any fee concession availed of. If so, the nature of such concession</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_any_fee_concession_ten" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Total no. of working days in the academic session</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_ttl_no_of_workind_days_ten" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Total no. of working days pupil present in the school</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_ttl_persent_days" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Whether NCC Cadet/Boy Scout/Girl Guide</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_ncc_cadet_by_ten" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Games played or extra curricular activites in which the pupil usually took apart</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_games_played" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">General conduct</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_general_conduct_ten" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Date of application for certificate</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_date_of_application_ten" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Date of issue of certificate</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_date_of_issue_ten" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Reason for leaving the school</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_reason_for_leaving_ten" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="rowdevider">
                                <div class="row">
                                    <div class="col-xl-6 padd-rght-5">
                                        <label for="validationCustom01" class="form-label-fnds">Any other remarks</label>
                                    </div>
                                    <div class="col-xl-6 padd-lft-5">
                                        <asp:TextBox ID="txt_any_other_remarks_ten" runat="server" TextMode="MultiLine" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xl-6 padd-lft-5">
                                <asp:Button ID="btn_make_tcTen" runat="server" Text="Create" CssClass="btn btn-primary form-fnd-btns" OnClick="btn_make_tcTen_Click" Style="margin: 8px 0px 10px -4px;" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="ModalTCFeePay" class="modal fade" role="dialog">
        <div class="modal-dialog" style="max-width: 800px;">
            <div class="modal-content">
                <div class="modal-header" style="padding: 3px 17px;">
                    <h5 class="modal-title" style="font-size: 18px; padding: 5px 0px 0px 0px;">Pay Fee</h5>
                    <a href="#!" data-dismiss="modal" style="margin: 5px 0px 0px 0px !important; padding: 3px 5px 4px 5px; background: #fa2020; border: #9b0202;"
                        class="btn btn-primary find-dv-btn">Close</a>
                </div>
                <div class="modal-body">
                    <div class="p-4 border rounded" style="float: left; width: 100%;">
                        <div class="ttpp-wprs" style="height: auto; overflow: inherit;">
                            <table style="width: 100%;" class="table table-hover table-bordered ">
                                <tr>
                                    <th>Session</th>
                                    <th>Student Name</th>
                                    <th>Admission No</th>
                                    <th>Class</th>
                                    <th>Section</th>
                                    <th>Roll</th>
                                    <th>Father's Name</th>
                                </tr>
                                <asp:Repeater ID="rp_pay_fee" runat="server">
                                    <ItemTemplate>
                                        <tr id="row" runat="server">
                                            <td>
                                                <asp:Label ID="Label2" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                            </td>
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
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>


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
        </div>
    </div>
    <asp:HiddenField ID="hd_firm_id" runat="server" />
    <asp:HiddenField ID="hd_is_pay_slc_fee" runat="server" />

    <script type="text/javascript"> 
        $(function () {
            $("#<%=txt_date_of_application_8.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "2020:2030",
            });
        }); 
        $(function () {
            $("#<%=txt_date_of_issue_certificate_8.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "2020:2030",
            });
        });
        $(function () {
            $("#<%=txt_date_of_admission_8.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "2020:2030",
            });
        }); 
        $(function () {
            $("#<%=txt_payment_date.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "2020:2030",
            });
        });
        $(function () {
            $("#<%=txt_bank_date.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "2020:2030",
            });
        }); 
    </script>
</asp:Content>
