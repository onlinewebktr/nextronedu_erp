<%@ Page Title="" Language="C#" MasterPageFile="~/Payroll/Adminmaster.Master" AutoEventWireup="true" CodeBehind="leave-entry.aspx.cs" Inherits="school_web.Payroll.leave_entry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Leave Entry
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
                            <li class="breadcrumb-item active" aria-current="page">Leave Entry</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-4">
                    <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Add Leave Entry"></asp:Label>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation" novalidate="">
                                    <div class="col-md-12">
                                        <label for="validationCustom01" class="form-label">Employee <sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_employee" runat="server" class="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddl_employee_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-12">
                                        <label for="validationCustom01" class="form-label">Leave Type <sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_leave_type" runat="server" class="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddl_leave_type_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-12" id="pnl_leave" runat="server" visible="false">
                                        <label for="validationCustom01" class="form-label">Available Leave <sup>*</sup></label>
                                        <asp:Label ID="txt_leave" runat="server" Text="0"></asp:Label>
                                    </div>

                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="col-xl-6">
                                                <label for="validationCustom01" class="form-label">Start Date<sup>*</sup></label>
                                                <asp:TextBox ID="txt_start_date" runat="server" class="form-control datepicker" OnTextChanged="txt_start_date_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                            <div class="col-xl-6">
                                                <label for="validationCustom01" class="form-label">End Date<sup>*</sup></label>
                                                <asp:TextBox ID="txt_end_date" runat="server" class="form-control datepicker" OnTextChanged="txt_end_date_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-12">
                                        <label for="validationCustom01" class="form-label">No. of leave days<sup>*</sup></label>
                                        <asp:TextBox ID="txt_total_leave" runat="server" class="form-control" Text="0"></asp:TextBox>
                                    </div>
                                    <asp:Panel ID="pnl_lop" runat="server">
                                        <div class="col-md-12">
                                            <label for="validationCustom01" class="form-label">No. of days LOP<sup>*</sup></label>
                                            <asp:TextBox ID="txt_lop_leave" runat="server" class="form-control" Text="0"></asp:TextBox>
                                        </div>
                                        <div class="col-md-12" runat="server" id="pnl_lwp_leave" visible="false">
                                            <label for="validationCustom01" class="form-label">No of Days LWP<sup>*</sup></label>
                                            <asp:TextBox ID="txt_lwp_leave" runat="server" class="form-control"></asp:TextBox>
                                        </div>
                                    </asp:Panel>
                                    <div class="col-md-12">
                                        <label for="validationCustom01" class="form-label">Remark<sup>*</sup></label>
                                        <asp:TextBox ID="txt_remark" runat="server" TextMode="MultiLine" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-12">
                                        <label for="validationCustom01" class="form-label">Approved by<sup>*</sup></label>
                                        <asp:TextBox ID="txt_approved_by" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-12">
                                        <label for="validationCustom01" class="form-label">Leave Application File<sup>*</sup></label>
                                        <asp:FileUpload ID="FileUpload1" runat="server" class="form-control" />
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
                    <h6 class="mb-0 text-uppercase">Added Leave</h6>
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
                                                        <th>Employee Name</th>
                                                        <th>Start Date</th>
                                                        <th>End Date</th>
                                                        <th>Leave Type</th>
                                                        <th>No. of Leave</th>
                                                        <th>Approved By</th>
                                                        <th>Remark</th>
                                                        <th>Status</th>
                                                        <th>Application File</th>
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
                                                                        <asp:Label ID="lbl_name" runat="server" Text='<%#Bind("Employee_Name")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_start_Date" runat="server" Text='<%#Bind("L_start_Date")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="Label1" runat="server" Text='<%#Bind("L_end_Date")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="Label2" runat="server" Text='<%#Bind("Leave_Type")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="Label3" runat="server" Text='<%#Bind("Total_leave")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="Label4" runat="server" Text='<%#Bind("Approved_by")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="Label5" runat="server" Text='<%#Bind("Remark")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="Label6" runat="server" Text='<%#Bind("Status")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <a href="<%#Eval("Application_File") %>" download="">View</a>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_employee_id" runat="server" Text='<%#Bind("Employee_id")%>' Visible="false"></asp:Label>
                                                                            <asp:Label ID="lbl_id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                            <asp:Label ID="lbl_leave_entry_id" runat="server" Text='<%#Bind("leave_entry_id")%>' Visible="false"></asp:Label>
                                                                        <div class="user-box dropdown" style="float: left; display: inherit; height: auto; border-left: 0px solid #f0f0f0; margin-left: 0px;">
                                                                            <a class="d-flex align-items-center nav-link dropdown-toggle dropdown-toggle-nocaret" style="font-size: 29px; padding: 0px; border: 0px;"
                                                                                href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                                                                                <div class="user-info ps-3" style="padding: 0px !important; border: 0px !important;">
                                                                                    <i class="bx bx-grid-horizontal"></i>
                                                                                </div>
                                                                            </a>
                                                                            <ul class="dropdown-menu dropdown-menu-end">
                                                                                 
                                                                                <li>
                                                                                    <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" class="dropdown-item" OnClick="lnkEdit_Click" ToolTip="Edit"> <i class="lni lni-pencil-alt"></i><span>Edit</span></asp:LinkButton>
                                                                                </li>
                                                                                <li>
                                                                                    <asp:LinkButton ID="lnkDel" runat="server" class="dropdown-item" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"> </i><span>Delete</span></asp:LinkButton>
                                                                                </li>

                                                                            </ul>
                                                                        </div>
                                                                        <%--<asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Edit"> <i class="lni lni-pencil-alt"> </i></asp:LinkButton>
                                                                        <asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                                        <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                        --%>
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
</asp:Content>
