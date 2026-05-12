<%@ Page Title="" Language="C#" MasterPageFile="~/Payroll/Adminmaster.Master" AutoEventWireup="true" CodeBehind="Attendance_Timming_Setting.aspx.cs" Inherits="school_web.Payroll.Attendance_Timming_Setting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Attendance Timing Setting
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
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
                <div class="breadcrumb-title pe-3">Setup Master</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Income Type Master</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-4">
                    <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Add Attendance Timing Setting"></asp:Label>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation" novalidate="">
                                    <div class="col-md-12">
                                        <label for="validationCustom01" class="form-label">Select Grade<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_gradename" runat="server" class="form-select find-dv-txtbx">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-12">
                                        <label for="validationCustom01" class="form-label">Attendance Timing<sup>*</sup></label>

                                    </div>

                                    <div class="col-md-6">
                                        <label for="validationCustom01" class="form-label">In Time<sup>*</sup></label>
                                        <div class='input-group date' id='datetimepicker1'>
                                            <asp:TextBox ID="txt_In_times" runat="server" placeholder="Time" CssClass="form-control" Style="pointer-events: none !important;"></asp:TextBox>
                                            <span class="input-group-addon" style="padding: 4px 3px; font-size: 25px; font-weight: 400; color: #555; text-align: center; background-color: #eee; border: 1px solid #ccc; border-radius: 4px; line-height: 20px;">
                                                <span class="bx bx-timer"></span>
                                            </span>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <label for="validationCustom01" class="form-label">Out Time<sup>*</sup></label>
                                        <div class='input-group date' id='datetimepicker2'>
                                            <asp:TextBox ID="txt_outtime" runat="server" placeholder="Time" CssClass="form-control" Style="pointer-events: none !important;"></asp:TextBox>
                                            <span class="input-group-addon" style="padding: 4px 3px; font-size: 25px; font-weight: 400; color: #555; text-align: center; background-color: #eee; border: 1px solid #ccc; border-radius: 4px; line-height: 20px;">
                                                <span class="bx bx-timer"></span>
                                            </span>
                                        </div>
                                    </div>

                                    <div class="col-md-12">
                                        <label for="validationCustom01" class="form-label">Attendance Grace Time(In Minutes)<sup>*</sup></label>
                                        <asp:TextBox ID="txt_gracetime" runat="server" class="form-control find-dv-txtbx" onkeypress="return isNumberKey(event)"></asp:TextBox>

                                    </div>



                                    <div class="col-12">
                                        <asp:Button ID="btn_Submit" runat="server" Text="Add" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btn_Submit_Click" />
                                        <asp:Button ID="btn_cancel" runat="server" Text="Cancel" class="btn btn-dark" Visible="false" CausesValidation="false" OnClick="btn_cancel_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


                <div class="col-xl-8">
                    <h6 class="mb-0 text-uppercase">Added Attemdance Timming </h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <table id="example21" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                <thead>
                                                    <tr>
                                                        <th>#</th>
                                                        <th>Grdae</th>
                                                        <th>In Time</th>
                                                        <th>Out Time</th>
                                                        <th>Grace Time</th>
                                                        <th>Action</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="rd_view" runat="server">
                                                        <ItemTemplate>
                                                            <asp:Panel ID="Panel1" runat="server">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_name" runat="server" Text='<%#Bind("grade_name")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_Morning_in" runat="server" Text='<%#Bind("Morning_in")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_Morning_out" runat="server" Text='<%#Bind("Morning_out")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_Grace_time" runat="server" Text='<%#Bind("Grace_time")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Edit"> <i class="lni lni-pencil-alt"> </i></asp:LinkButton>
                                                                        <asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                                        <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>

                                                                    </td>
                                                                </tr>
                                                            </asp:Panel>
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

    <!--end page wrapper -->
    <asp:HiddenField ID="hd_id" runat="server" />
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
</asp:Content>
