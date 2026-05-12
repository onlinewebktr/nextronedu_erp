<%@ Page Title="" Language="C#" MasterPageFile="~/Examination_Admin/Admin.Master" AutoEventWireup="true" CodeBehind="attendace-for-admitcard.aspx.cs" Inherits="school_web.Examination_Admin.attendace_for_admitcard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Attendance Validation For Admit Card
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
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
                <div class="breadcrumb-title pe-3">Admit Card</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Attendance Validation For Admit Card</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-4">
                    <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text=" Add Attendance Validation For Admit Card"></asp:Label>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation" novalidate="">
                                    <div class="col-sm-12">
                                        <label for="validationCustom01" class="find-dv-lbl">Session</label>
                                        <asp:DropDownList ID="ddlsession" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                    </div>
                                    <div class="col-sm-12">
                                        <label for="validationCustom01" class="find-dv-lbl">Class</label>
                                        <asp:DropDownList ID="ddl_class" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_class_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="col-sm-12">
                                        <label for="validationCustom01" class="find-dv-lbl">Exam Term</label>
                                        <asp:DropDownList ID="ddl_exam_term" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_exam_term_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="col-sm-12">
                                        <label for="validationCustom01" class="find-dv-lbl">Exam</label>
                                        <asp:DropDownList ID="ddl_exam" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                    </div>
                                    <div class="col-sm-12">
                                        <label for="validationCustom01" class="find-dv-lbl">Attendance(%) Needed</label>
                                        <asp:TextBox ID="txt_attendance_per_need" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-12">
                                        <label for="validationCustom01" class="find-dv-lbl">Attendance From Date</label>
                                        <asp:TextBox ID="txt_from_date" runat="server" class="form-control find-dv-txtbx datepicker"></asp:TextBox>
                                    </div>

                                    <div class="col-sm-12">
                                        <label for="validationCustom01" class="find-dv-lbl">Attendance To Date</label>
                                        <asp:TextBox ID="txt_to_date" runat="server" class="form-control find-dv-txtbx datepicker"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-12">
                                        <asp:Button ID="btn_Submit" Style="margin: 0px 7px 1px 0px !important;" runat="server" OnClick="btn_Submit_Click" Text="Submit" class="btn btn-primary find-dv-btn" />
                                        <asp:Button ID="btn_cancel" runat="server" Text="Cancel" class="btn btn-dark" Visible="false" CausesValidation="false" OnClick="btn_cancel_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:HiddenField ID="hd_id" runat="server" />

                <div class="col-xl-8">
                    <h6 class="mb-0 text-uppercase">Added Attendance Validation For Admit Card</h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="find-dv">
                                                <div class="row g-3 needs-validation" novalidate="">
                                                    <div class="col-sm-3">
                                                        <label for="validationCustom01" class="find-dv-lbl">Session</label>
                                                        <asp:DropDownList ID="ddl_find_session" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-1">
                                                        <asp:Button ID="btn_find" runat="server" Text="Find" OnClick="btn_find_Click" class="btn btn-primary find-dv-btn" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="tblPrintIQ" runat="server">
                                                <div class="prnt-dv-wpr">
                                                    <div id="content">
                                                        <table id="example21" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                            <thead>
                                                                <tr>
                                                                    <th>#</th>
                                                                    <th>Session</th>
                                                                    <th>Class</th>
                                                                    <th>Term Name</th>
                                                                    <th>Exam Name</th>
                                                                    <th>Attendance(%)</th>
                                                                    <th>Date</th>
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
                                                                                <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("Session_name")%>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lbl_class_name" runat="server" Text='<%#Bind("Class_name")%>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lbl_term_name" runat="server" Text='<%#Bind("Term_name")%>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lbl_exam_name" runat="server" Text='<%#Bind("Exam_name")%>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lbl_attendance_percent" runat="server" Text='<%#Bind("Attendance_percent")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label1" runat="server" Text='<%#Bind("From_date")%>'></asp:Label>
                                                                                to
                                                                                <asp:Label ID="Label2" runat="server" Text='<%#Bind("To_date")%>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:LinkButton ID="lnkEdit" Visible="false" runat="server" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Edit"> <i class="lni lni-pencil-alt"> </i></asp:LinkButton>
                                                                                <asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                                                <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>

                                                                                <asp:Label ID="lbl_session_id" runat="server" Text='<%#Bind("Session_id")%>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lbl_class_id" runat="server" Text='<%#Bind("Class_id")%>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lbl_term_id" runat="server" Text='<%#Bind("Term_id")%>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lbl_exam_id_id" runat="server" Text='<%#Bind("Exam_id")%>' Visible="false"></asp:Label>

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
            </div>
        </div>
        <!--end row-->
    </div>
</asp:Content>
