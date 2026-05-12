<%@ Page Title="" Language="C#" MasterPageFile="~/Examination_Admin/Admin.Master" AutoEventWireup="true" CodeBehind="print-internal-exam-admit-card.aspx.cs" Inherits="school_web.Examination_Admin.print_internal_exam_admit_card" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Print Admit Card
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        table tr th input[type="checkbox"] {
            background-color: #fff;
            margin: 0;
            font: inherit;
            color: currentColor;
            width: 22px;
            height: 22px;
            border: 0.15em solid currentColor;
            border-radius: 0.15em;
            transform: translateY(-0.075em);
        }

        table tr th label {
            width: 100%;
        }

        table tr td input[type="checkbox"] {
            background-color: #fff;
            margin: 0;
            font: inherit;
            color: currentColor;
            width: 20px;
            height: 20px;
            border: 1px solid #646464;
            border-radius: 0.15em;
            transform: translateY(-0.075em);
        }

        .form-control + .form-control {
            margin-top: 1em;
        }

        tfoot, th, thead {
            border-color: inherit;
            border-style: solid;
            border-width: 0;
            vertical-align: middle;
            background: #0c6eff !important;
            text-align: left;
            font-size: 13px;
            color: #fff;
        }

        tbody, td, tfoot, th, thead, tr {
            border-color: inherit;
            border-style: solid;
            border-width: 0;
            vertical-align: middle;
            text-align: left;
            font-size: 13px;
        }

        .wrapper.toggled:not(.sidebar-hovered) .sidebar-wrapper {
            width: 70px;
        }

        table.dataTable > thead > tr > th:not(.sorting_disabled), table.dataTable > thead > tr > td:not(.sorting_disabled) {
            padding-right: inherit;
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
                <div class="breadcrumb-title pe-3">Internal Exam</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Print Admit Card</li>
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
                                            <div class="row g-3 needs-validation" novalidate="">
                                                <div class="col-sm-2">
                                                    <label for="validationCustom01" class="find-dv-lbl">Session</label>
                                                    <asp:DropDownList ID="ddlsession" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                </div>
                                                <div class="col-sm-3">
                                                    <label for="validationCustom01" class="find-dv-lbl">Exam</label>
                                                    <asp:DropDownList ID="ddl_exam" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_exam_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-2">
                                                    <label for="validationCustom01" class="find-dv-lbl">Class</label>
                                                    <asp:DropDownList ID="ddl_class" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_class_SelectedIndexChanged1">
                                                    </asp:DropDownList>
                                                </div>  
                                                <div class="col-sm-2">
                                                    <label for="validationCustom01" class="find-dv-lbl">Section</label>
                                                    <asp:DropDownList ID="ddl_section" runat="server" class="form-select find-dv-txtbx">
                                                    </asp:DropDownList>
                                                </div> 
                                                <div class="col-sm-1">
                                                    <asp:Button ID="btn_find" runat="server" Text="Find" OnClick="btn_find_Click" class="btn btn-primary find-dv-btn" />
                                                </div>
                                                <div class="col-sm-1">
                                                    <asp:Button ID="btn_print_all" Visible="false" runat="server" Text="Print All" class="btn btn-primary find-dv-btn" OnClick="btn_print_all_Click" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="grd-wpr">
                                            <table id="example2" data-page-length='100000' class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                <thead>
                                                    <tr>
                                                        <th>#</th>
                                                        <th style="padding: 7px 0px 0px 0px; text-align: center;">
                                                            <asp:CheckBox ID="chkAll" runat="server" /></th>
                                                        <th>Adm. No.</th>
                                                        <th>Class</th>
                                                        <th>Session</th>
                                                        <th>Section</th>
                                                        <th>Roll No.</th>
                                                        <th>Student Name</th>
                                                        <th>Father Name</th>
                                                        <th>Action</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="rd_view" runat="server" OnItemDataBound="rd_view_ItemDataBound">
                                                        <ItemTemplate>
                                                            <tr id="trR" runat="server">
                                                                <td>
                                                                    <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: center;">
                                                                    <asp:CheckBox ID="chkRowData" runat="server" /></td>
                                                                <td>
                                                                    <asp:Label ID="lbl_admission_no" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbl_roll_no" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="lbl_student_name" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%#Bind("fathername")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbl_session_id" Visible="false" runat="server" Text='<%#Bind("Session_id")%>'></asp:Label>
                                                                    <asp:Label ID="lbl_class_id" Visible="false" runat="server" Text='<%#Bind("Class_id")%>'></asp:Label>
                                                                    <%--<asp:Label ID="lbl_exam_id" Visible="false" runat="server" Text='<%#Bind("Exam_id")%>'></asp:Label>--%>
                                                                    <asp:Label ID="lbl_id" Visible="false" runat="server" Text='<%#Bind("Id")%>'></asp:Label>
                                                                    <%--<asp:Label ID="lbl_shift_type" runat="server" Text='<%#Bind("Shift_type") %>' Visible="false"></asp:Label>--%>
                                                                    <a id="admitcardLink" class="button-61 nowordbreak collect-feesss" style="background-color: #f7f100; min-width: 30px; color: #000;" runat="server" target="_blank"><span class="material-symbols-outlined">print</span></a>

                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>
                                            <script>
                                                $(document).ready(function () {
                                                    // CHECK-UNCHECK ALL CHECKBOXES IN THE REPEATER 
                                                    // WHEN USER CLICKS THE HEADER CHECKBOX.
                                                    $('table [id*=chkAll]').click(function () {
                                                        if ($(this).is(':checked'))
                                                            $('table [id*=chkRowData]').prop('checked', true)
                                                        else
                                                            $('table [id*=chkRowData]').prop('checked', false)
                                                    });

                                                    // NOW CHECK THE HEADER CHECKBOX, IF ALL THE ROW CHECKBOXES ARE CHECKED.
                                                    $('table [id*=chkRowData]').click(function () {

                                                        var total_rows = $('table [id*=chkRowData]').length;
                                                        var checked_Rows = $('table [id*=chkRowData]:checked').length;

                                                        if (checked_Rows == total_rows)
                                                            $('table [id*=chkAll]').prop('checked', true);
                                                        else
                                                            $('table [id*=chkAll]').prop('checked', false);
                                                    });
                                                });
                                            </script>
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
