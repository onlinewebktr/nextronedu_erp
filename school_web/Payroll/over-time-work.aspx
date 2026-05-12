<%@ Page Title="" Language="C#" MasterPageFile="~/Payroll/Adminmaster.Master" AutoEventWireup="true" CodeBehind="over-time-work.aspx.cs" Inherits="school_web.Payroll.over_time_work" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    OverTime Work
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .table-responsive {
            overflow-x: inherit;
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
                <div class="breadcrumb-title pe-3">Attendance</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">OverTime Work</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase">OverTime Work</h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5">
                                    <div class="row">
                                        <div class="col-sm-12">

                                            <div class="row">
                                                <div class="col-xl-7">
                                                    <div class="find-dv">
                                                        <div class="row">
                                                            <div class="col-sm-3">
                                                                <label for="validationCustom01" class="find-dv-lbl">Employee Name</label>
                                                                <asp:DropDownList ID="ddl_employee" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                            </div>
                                                            <div class="col-sm-3">
                                                                <label for="validationCustom01" class="find-dv-lbl">Date</label>
                                                                <asp:TextBox ID="txt_date" runat="server" class="form-control find-dv-txtbx datepicker"></asp:TextBox>
                                                            </div>


                                                            <div class="col-xl-5">
                                                                <div class="row">
                                                                    <div class="col-sm-6">
                                                                        <label for="validationCustom01" class="find-dv-lbl">In Time</label>
                                                                        <div class='input-group date' id='datetimepicker1'>
                                                                            <asp:TextBox ID="txt_in_time" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                                            <span class="input-group-addon" style="padding: 4px 3px; font-size: 25px; font-weight: 400; color: #555; text-align: center; background-color: #eee; border: 1px solid #ccc; border-radius: 4px; line-height: 20px;">
                                                                                <span class="bx bx-timer"></span>
                                                                            </span>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-6">
                                                                        <label for="validationCustom01" class="find-dv-lbl">Out Time</label>
                                                                        <div class='input-group date' id='datetimepicker2'>
                                                                            <asp:TextBox ID="txt_out_time" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                                            <span class="input-group-addon" style="padding: 4px 3px; font-size: 25px; font-weight: 400; color: #555; text-align: center; background-color: #eee; border: 1px solid #ccc; border-radius: 4px; line-height: 20px;">
                                                                                <span class="bx bx-timer"></span>
                                                                            </span>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-1">
                                                                <asp:Button ID="btn_save" runat="server" Text="Save" Style="margin: 21px 0px 0px -14px;" class="btn btn-primary find-dv-btn" OnClick="btn_Submit_Click1" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-xl-5">
                                                    <div class="find-dv">
                                                        <div class="row">
                                                            <div class="col-sm-4">
                                                                <label for="validationCustom01" class="find-dv-lbl">Employee Name</label>
                                                                <asp:DropDownList ID="ddl_employee1" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                            </div>
                                                            <div class="col-sm-3">
                                                                <label for="validationCustom01" class="find-dv-lbl">From Date</label>
                                                                <asp:TextBox ID="txt_start_date" runat="server" class="form-control find-dv-txtbx datepicker"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-3">
                                                                <label for="validationCustom01" class="find-dv-lbl">To Date</label>
                                                                <asp:TextBox ID="txt_end_date" runat="server" class="form-control find-dv-txtbx datepicker"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-1">
                                                                <asp:Button ID="btn_find" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_find_Click" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="grd-wpr">
                                                <table id="example21" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                    <thead>
                                                        <tr>
                                                            <th>#</th>
                                                            <th>Name</th>
                                                            <th>Date</th>
                                                            <th>In Time</th>
                                                            <th>Out Time</th>
                                                            <th>Working Hour</th>
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
                                                                        <asp:Label ID="Label6" runat="server" Text='<%#Bind("Employee_Name")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                    </td>

                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_In_Time" runat="server" Text='<%#Bind("In_Time")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_Out_Time" runat="server" Text='<%#Bind("Out_Time")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_working_hour" runat="server" Text='<%#Bind("working_hour")%>'></asp:Label>
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

    <link href="../timePicker/glaphycon.css" rel="stylesheet" />
    <link href="../timePicker/bootstrap-datetimepicker.css" rel="stylesheet" />
    <script src="../timePicker/moment-with-locales.js"></script>
    <script src="../timePicker/bootstrap-datetimepicker.js"></script>

    <script type="text/javascript">

        $(function () {
            $('#datetimepicker1').datetimepicker({
                format: 'LT'
            });
        });
        $(function () {
            $('#datetimepicker2').datetimepicker({
                format: 'LT'
            });
        });
    </script>
</asp:Content>
