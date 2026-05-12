<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Id-card-employee-print.aspx.cs" Inherits="school_web.Admin.Id_card_employee_print" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Id Card For Employee
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
                                                <label for="validationCustom01" class="find-dv-lbl">Type</label>
                                                <asp:DropDownList ID="ddl_type" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_type_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                            <div class="col-sm-2">
                                                <label for="validationCustom01" class="find-dv-lbl">Employee</label>
                                                <asp:DropDownList ID="ddl_employee" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                            </div>


                                            <div class="col-sm-1">
                                                <asp:Button ID="btn_find" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_find_Click" />
                                            </div>



                                            <div class="col-sm-3">
                                                <asp:Button ID="btn_print_all" runat="server" Text="Print All" class="btn btn-primary find-dv-btn" OnClick="btn_print_all_Click" Style="float: right" />

                                                <asp:LinkButton ID="btn_excels" runat="server" OnClick="btn_excels_Click" class="btn btn-primary find-dv-btn"><i class='bx bx-download'></i>Excel</asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="grd-wpr">
                                        <table id="example2" data-page-length='100000' class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                            <thead>
                                                <tr>
                                                    <th>#</th>
                                                    <th style="padding: 7px 0px 0px 0px;">
                                                        <asp:CheckBox ID="chkAll" runat="server" />
                                                    </th>
                                                    <th>Emloyee No.</th>
                                                    <th>Name</th>
                                                    <th>Mobile No.</th>
                                                    <th>User Type</th>
                                                    <th style=" display:none">Is Edit Permission</th>
                                                    <th>Action</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rd_view" runat="server" OnItemDataBound="rd_view_ItemDataBound">
                                                    <itemtemplate>
                                                        <tr id="trR" runat="server">
                                                            <td>
                                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="chkRowData" runat="server" />
                                                            </td>
                                                            <td style="text-align: left;">
                                                                <asp:Label ID="lbl_employee_id" runat="server" Text='<%#Bind("Emp_Code")%>'></asp:Label>
                                                            </td>

                                                            <td style="text-align: left;">
                                                                <asp:Label ID="lbl_name" runat="server" Text='<%#Bind("Employee_Name")%>'></asp:Label>
                                                            </td>

                                                            <td style="text-align: left;">
                                                                <asp:Label ID="lbl_mobile" runat="server" Text='<%#Bind("Mobile")%>'></asp:Label>
                                                            </td>

                                                            <td style="text-align: left;">
                                                                <asp:Label ID="lbl_User_Type" runat="server" Text='<%#Bind("employee_type")%>'></asp:Label>
                                                            </td>
                                                            <td style="text-align: left; display:none">
                                                                <asp:Label ID="lbl_edit_permission" runat="server"></asp:Label>
                                                                <asp:Label ID="lbl_Is_Allow_edit" Visible="false" runat="server" Text='<%#Bind("Is_submit")%>'></asp:Label>
                                                            </td>
                                                            <td style="text-align: left;">
                                                                <asp:LinkButton ID="lnk_edit_emp" OnClick="lnk_edit_emp_Click" runat="server" style="display:none">Edit</asp:LinkButton>
                                                                <a id="idcard_link" runat="server" class="font-size: 17px; color: #007de9;" target="_blank"><i class='bx bx-printerbx bx-printer'></i>Print</a>
                                                                <a id="idcard_linkBack" runat="server" style="display:none" class="font-size: 17px; color: #007de9;" target="_blank"><i class='bx bx-printerbx bx-printer'></i>Back</a>
                                                                <asp:Label ID="lbl_id" Visible="false" runat="server" Text='<%#Bind("Id")%>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </itemtemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>

                                        <asp:Button ID="btn_inactive" runat="server" Text="Stop" class="btn btn-danger find-dv-btn" OnClick="btn_inactive_Click" OnClientClick="return confirm('Are you sure want to stop?');" style="float: right; display:none" />
                                        <asp:Button ID="btn_active" runat="server" Text="Allow" class="btn btn-success find-dv-btn" Style="margin-left: 20px; float: right; display:none" OnClick="btn_active_Click" OnClientClick="return confirm('Are you sure want to grant permission?');" />
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
    <!--end page wrapper -->


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



    <div class="modal fade" id="myModal" role="dialog" style="top: 0px">
        <div class="modal-dialog md-width" style="max-width: 600px;">
            <!-- Modal content-->
            <div class="modal-content" style="position: relative">
                <div class="modal-header">
                    <h3 class="modal-title" style="font-size: 20px;">Employee Info</h3>
                    <button type="button" class="mdl-close-btn" data-dismiss="modal"><i class="fa fa-times" aria-hidden="true"></i></button>
                </div>
                <div class="modal-body md-bdy">
                    <asp:HiddenField ID="hd_emp_code" runat="server" />
                    <asp:HiddenField ID="hd_id" runat="server" />
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Emp. Code</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txt_emp_code" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Id Card No.</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txt_id_card_no" runat="server" class="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Name</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txt_emp_name" runat="server" class="form-control"></asp:TextBox>
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
                                <label for="validationCustom01" class="find-dv-lbl">Department</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddl_emp_type" runat="server" class="form-select"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Aadhar No.</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txt_aadhar_no" runat="server" class="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Date of joining</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txt_doj" runat="server" class="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Date of Birth</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txt_dob" runat="server" class="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Blood group</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddl_blood_group" runat="server" class="form-select">
                                    <asp:ListItem>N/A</asp:ListItem>
                                    <asp:ListItem>A+</asp:ListItem>
                                    <asp:ListItem>A-</asp:ListItem>
                                    <asp:ListItem>B+</asp:ListItem>
                                    <asp:ListItem>B-</asp:ListItem>
                                    <asp:ListItem>O+</asp:ListItem>
                                    <asp:ListItem>O-</asp:ListItem>
                                    <asp:ListItem>AB+</asp:ListItem>
                                    <asp:ListItem>AB-</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Contact no</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txt_mobile_no" runat="server" onkeypress="return isNumberKey(event)" class="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Email Id</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txt_email_id" runat="server" class="form-control"></asp:TextBox>
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
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Choose Password Photo</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form_control" />
                                <asp:Image ID="img_student_image" runat="server" Visible="false" Style="height: 150px; width: 150px; padding: 2px; border: 2px solid #000;" />
                            </div>
                        </div>
                    </div>







                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4"></div>
                            <div class="col-sm-8">
                                <asp:Button ID="btn_update_emp_info" OnClick="btn_update_emp_info_Click" runat="server" Text="Submit" class="btn btn-primary" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
