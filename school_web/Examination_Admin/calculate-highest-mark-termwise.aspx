<%@ Page Title="" Language="C#" MasterPageFile="~/Examination_Admin/Admin.Master" AutoEventWireup="true" CodeBehind="calculate-highest-mark-termwise.aspx.cs" Inherits="school_web.Examination_Admin.calculate_highest_mark_termwise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Calculate Highest Mark
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script language="Javascript">
       <!--
    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode != 46 && charCode > 31
            && (charCode < 48 || charCode > 57))
            return false;

        return true;
    }
    //-->
    </script>

    <style type="text/css">
        .table td, .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            padding: 8px !important;
        }

        .modal-backdrop.fade, .fade.blockOverlay {
            opacity: 0;
            display: none;
        }

        .txtbxError {
            border-bottom: 2px solid #ef0000 !important;
            padding: 3px 7px 2px !important;
        }
    </style>
    <script type="text/javascript">
        function openModal() {
            $('#myModal').modal('show');
        }
    </script>


    <style>
        .modal {
            background: rgb(0 0 0 / 50%);
            padding-right: 0px !important;
            padding: 50px 0px 0px 0px;
        }

        .mdl-frm-row {
            margin: 0px 0px 10px 0px;
            padding: 0px;
            width: 100%;
            float: left;
        }

        .mdl-close-btn {
            margin: 0px;
            padding: 0px 5px 0px 5px;
            border: 0px;
            background: #ed0000;
            font-size: 18px;
            color: #fff;
            line-height: 25px;
            border-radius: 2px;
        }

        .modal-header {
            padding: 7px 15px;
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
                <div class="breadcrumb-title pe-3"><a href="student-report-home.aspx" runat="server" id="backbtns" class="backlnk-css"><i class="bx bx-arrow-back"></i></a>Result</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Highest Mark Calculation</li>
                        </ol>
                    </nav>
                </div>
            </div>

            <asp:HiddenField ID="hd_term1" runat="server" />
            <asp:HiddenField ID="hd_term2" runat="server" />
            <asp:HiddenField ID="hd_term3" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase" style="position: relative;">Highest Mark Calculation<a href="#" data-toggle="modal" data-target="#myModal" class="add-forpopup-btn">Calculate Highest Mark</a></h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                    <div class="find-dv">
                                        <div class="row">
                                            <div class="col-sm-2">
                                                <label for="validationCustom01" class="find-dv-lbl">Session</label>
                                                <asp:DropDownList ID="ddlsession" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                            </div>
                                            <div class="col-sm-2">
                                                <label for="validationCustom01" class="find-dv-lbl">Class</label>
                                                <asp:DropDownList ID="ddlclass" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddlclass_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                            <div class="col-sm-2">
                                                <label for="validationCustom01" class="find-dv-lbl">Section</label>
                                                <asp:DropDownList ID="ddl_section" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                            </div>
                                            <div class="col-sm-2">
                                                <label for="validationCustom01" class="find-dv-lbl">Subject</label>
                                                <asp:DropDownList ID="ddl_subject" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                            </div>
                                            <div class="col-sm-2">
                                                <label for="validationCustom01" class="find-dv-lbl">Term</label>
                                                <asp:DropDownList ID="ddl_term" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                            </div>

                                            <div class="col-sm-1">
                                                <asp:Button ID="btn_find" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_find_Click" />
                                            </div>
                                        </div>
                                    </div>

                                    <div style="float: left; width: 100%">
                                        <div class="row">
                                            <div class="col-xl-12">
                                                <hr />
                                                <table style="width: 100%;" id="example1" class="table table-hover table-striped table-bordered">
                                                    <thead>
                                                        <tr>
                                                            <th>#</th>
                                                            <th>Class</th>
                                                            <th>Subject Name</th>
                                                            <th>Heighest Mark</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="RPDetails" runat="server">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lbl_class_name" runat="server" Font-Names="Arial" Text='<%#Bind("Class_name") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lbl_subject_name" runat="server" Font-Names="Arial" Text='<%#Bind("Subject_name") %>'></asp:Label>
                                                                        <asp:Label ID="lbl_subject_id" runat="server" Font-Names="Arial" Visible="false" Text='<%#Bind("Subject_id") %>'></asp:Label>
                                                                        <asp:Label ID="lbl_course_id" runat="server" Font-Names="Arial" Visible="false" Text='<%#Bind("Class_id") %>'></asp:Label>

                                                                        <asp:Label ID="lbl_session_id" runat="server" Font-Names="Arial" Visible="false" Text='<%#Bind("Session_id") %>'></asp:Label>
                                                                        <asp:Label ID="lbl_term_id" runat="server" Font-Names="Arial" Visible="false" Text='<%#Bind("Term_id") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txt_marks" runat="server" class="grd-txtbx-clas" Text='<%#Bind("Marks") %>' onkeypress="return isNumberKey(event)"></asp:TextBox>
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
    <!--end page wrapper -->


    <div class="modal fade" id="myModal" role="dialog" style="top: 0px">
        <div class="modal-dialog md-width">
            <!-- Modal content-->
            <div class="modal-content" style="position: relative">
                <div class="modal-header">
                    <h3 class="modal-title" style="font-size: 20px;">Calculate Highest Mark</h3>
                    <button type="button" class="mdl-close-btn" data-dismiss="modal"><i class="fa fa-times" aria-hidden="true"></i></button>
                </div>
                <div class="modal-body md-bdy">
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Session</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddl_session_p" runat="server" class="form-select"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Class</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddl_CourseCat_p" runat="server" class="form-select" OnSelectedIndexChanged="ddl_CourseCat_p_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Section</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddl_section_p" runat="server" class="form-select"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Term</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddl_term_p" runat="server" class="form-select"></asp:DropDownList>
                            </div>
                        </div>
                    </div>


                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4"></div>
                            <div class="col-sm-8">
                                <asp:Button ID="btn_catculate_heighest_mark" runat="server" Text="Submit" class="btn btn-primary" OnClick="btn_catculate_heighest_mark_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
