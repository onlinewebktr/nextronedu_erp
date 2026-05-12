<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Id-card-print.aspx.cs" Inherits="school_web.Admin.Id_card_print" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Id Card Print
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
            text-align: center;
            font-size: 13px;
            color: #fff;
        }

        tbody, td, tfoot, th, thead, tr {
            border-color: inherit;
            border-style: solid;
            border-width: 0;
            vertical-align: middle;
            text-align: center;
            font-size: 13px;
        }

        .wrapper.toggled:not(.sidebar-hovered) .sidebar-wrapper {
            width: 70px;
        }

        table.dataTable > thead > tr > th:not(.sorting_disabled), table.dataTable > thead > tr > td:not(.sorting_disabled) {
            padding-right: inherit;
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
        function openModal() {
            $('#myModal').modal('show');
        }

        function openModalExcel() {
            $('#myModalExcel').modal('show');
        }
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
                <div class="breadcrumb-title pe-3">Id Card</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Print Id Card</li>
                        </ol>
                    </nav>
                </div>
            </div>


            <div class="row">
                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase"></h6>
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
                                            <div class="col-sm-1">
                                                <asp:Button ID="btn_find" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_find_Click" />
                                            </div>
                                            <div class="col-sm-1"></div>
                                            <div class="col-sm-4">
                                                <div style="display: none">
                                                    <asp:CheckBox ID="chk_is_ckeck" runat="server" Text="Is Print for Checking Purpose" />
                                                </div>
                                                <asp:Label ID="lbl_is_check" runat="server" Visible="false"></asp:Label>
                                                <asp:Button ID="btn_print_all" runat="server" Text="Print All" class="btn btn-primary find-dv-btn" OnClick="btn_print_all_Click" Style="float: right;" />

                                                <asp:LinkButton ID="btn_excels" runat="server" OnClick="btn_excels_Click" class="btn btn-primary find-dv-btn" Style="float: right; display: none"><i class='bx bx-download'></i>Excel</asp:LinkButton>
                                                <a href="#!" data-toggle="modal" data-target="#myModalExcel" class="btn btn-primary find-dv-btn" style="float: right;"><i class='bx bx-download'></i>Excel</a>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="grd-wpr">
                                        <table id="example21" data-page-length='100000' class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                            <thead>
                                                <tr>
                                                    <th></th>
                                                    <th>#</th>
                                                    <th style="padding: 7px 0px 0px 0px;">
                                                        <asp:CheckBox ID="chkAll" runat="server" /></th>
                                                    <th>Admission No.</th>
                                                    <th>Class</th>
                                                    <th>Session</th>
                                                    <th>Section</th>
                                                    <th>Roll No.</th>
                                                    <th>Student Name</th>
                                                    <th>Is Edit Permission</th>
                                                    <th></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rd_view" runat="server" OnItemDataBound="rd_view_ItemDataBound">
                                                    <ItemTemplate>
                                                        <tr id="trR" runat="server">
                                                            <td>
                                                                <asp:LinkButton ID="lnk_edit_std" Style="min-width: 36px;" class="button-61 nowordbreak collect-feesss" OnClick="lnk_edit_std_Click" runat="server"><span class="material-symbols-outlined">edit_square</span></asp:LinkButton>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="chkRowData" runat="server" /></td>
                                                            <td style="text-align: left;">
                                                                <asp:Label ID="lbl_admissionserialnumber" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                            </td>
                                                            <td style="text-align: left;">
                                                                <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                            </td>

                                                            <td style="text-align: left;">
                                                                <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                            </td>
                                                            <td style="text-align: left;">
                                                                <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                            </td>
                                                            <td style="text-align: left;">
                                                                <asp:Label ID="lbl_roll_no" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                            </td>

                                                            <td style="text-align: left;">
                                                                <asp:Label ID="lbl_student_name" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                            </td>

                                                            <td style="text-align: left;">
                                                                <asp:Label ID="lbl_edit_permission" runat="server"></asp:Label>
                                                                <asp:Label ID="lbl_Is_Allow_edit" Visible="false" runat="server" Text='<%#Bind("Is_Allow_edit")%>'></asp:Label>
                                                            </td>

                                                            <td style="text-align: left;">

                                                                <a id="idcard_link" runat="server" style="background-color: #f7f100; min-width: 30px; color: #000;"
                                                                    class="button-61 nowordbreak collect-feesss" target="_blank"><span class="material-symbols-outlined">print</span></a>

                                                                <asp:Label ID="lbl_session_id" Visible="false" runat="server" Text='<%#Bind("Session_id")%>'></asp:Label>
                                                                <asp:Label ID="lbl_class_id" Visible="false" runat="server" Text='<%#Bind("Class_id")%>'></asp:Label>
                                                                <asp:Label ID="lbl_branch_id" Visible="false" runat="server" Text='<%#Bind("Branch_id")%>'></asp:Label>
                                                                <asp:Label ID="lbl_id" Visible="false" runat="server" Text='<%#Bind("Id")%>'></asp:Label>
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
                                        <asp:Button ID="btn_inactive" runat="server" Text="Stop" class="btn btn-danger find-dv-btn" OnClick="btn_inactive_Click" OnClientClick="return confirm('Are you sure want to stop?');" Style="float: right;" />
                                        <asp:Button ID="btn_active" runat="server" Text="Allow" class="btn btn-success find-dv-btn" Style="margin-left: 20px; float: right;" OnClick="btn_active_Click" OnClientClick="return confirm('Are you sure want to grant permission?');" />
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
                    <h3 class="modal-title" style="font-size: 20px;">Student Info</h3>
                    <button type="button" class="mdl-close-btn" data-dismiss="modal"><i class="fa fa-times" aria-hidden="true"></i></button>
                </div>
                <div class="modal-body md-bdy">
                    <asp:HiddenField ID="hd_admission_no" runat="server" />
                    <asp:HiddenField ID="hd_session_id" runat="server" />
                    <asp:HiddenField ID="hd_class_id" runat="server" />
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Name</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txt_std_name" runat="server" class="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Class</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txt_class" runat="server" class="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Section</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txt_section" runat="server" class="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Roll No.</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txt_roll_no" runat="server" class="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Transport</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddl_transport" runat="server" class="form-select">
                                    <asp:ListItem>No</asp:ListItem>
                                    <asp:ListItem>Yes</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="mdl-frm-row" id="transport_typeDV">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Transport Type</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txt_transport_type" runat="server" class="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>


                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">DOB</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txt_dob" runat="server" class="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Caste</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddl_Category" runat="server" class="form-select"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Gender</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddl_gender" runat="server" CssClass="form-select">
                                    <asp:ListItem>Select</asp:ListItem>
                                    <asp:ListItem>MALE</asp:ListItem>
                                    <asp:ListItem>FEMALE</asp:ListItem>

                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>



                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Father's Name</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txt_father_name" runat="server" class="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Mobile No.</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txt_mobile_no" runat="server" onkeypress="return isNumberKey(event)" class="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Whatsapp No</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txt_Whatsapp_no" onkeypress="return isNumberKey(event)" runat="server" class="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>


                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Mother's Name</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txt_mothersname" runat="server" class="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Mobile No.</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txt_mother_mobile" runat="server" onkeypress="return isNumberKey(event)" class="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Whatsapp No</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txt_mother_Whatsapp" onkeypress="return isNumberKey(event)" runat="server" class="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Address</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txt_address" runat="server" class="form-control" Style="height: 80px" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </div>


                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4"></div>
                            <div class="col-sm-8">
                                <asp:Button ID="btn_update_std_info" runat="server" Text="Submit" class="btn btn-primary" OnClick="btn_update_std_info_Click" />
                                <asp:Button ID="btn_reset" runat="server" Text="Reset" class="btn btn-primary" OnClick="btn_reset_Click" Style="background: #8e8e8e; border: 1px solid #747373;" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div class="modal fade" id="myModalExcel" role="dialog" style="top: 0px">
        <div class="modal-dialog md-width">
            <!-- Modal content-->
            <div class="modal-content" style="position: relative">
                <div class="modal-header">
                    <h3 class="modal-title" style="font-size: 20px;">Download Student Info</h3>
                    <button type="button" class="mdl-close-btn" data-dismiss="modal"><i class="fa fa-times" aria-hidden="true"></i></button>
                </div>
                <div class="modal-body md-bdy">
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Session</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddl_session_exc" runat="server" class="form-select"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Class</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddl_class_exc" runat="server" class="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddl_class_exc_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Section</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddl_section_exc" runat="server" class="form-select"></asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4"></div>
                            <div class="col-sm-8">
                                <asp:Button ID="btn_download_excel" runat="server" Text="Download" class="btn btn-primary" OnClick="btn_download_excel_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            on_type_selection();
            $("#<%=ddl_transport.ClientID%>").on('change', function () {
                on_type_selection();
            })
        });

        function on_type_selection() {
            if ($('#<%= ddl_transport.ClientID %> option:selected').val() == "Yes") {
                $("#transport_typeDV").show();
            }
            else {
                $("#transport_typeDV").hide();
            }
        }
    </script>

</asp:Content>
