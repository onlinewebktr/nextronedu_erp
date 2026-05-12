<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="misc-fee-month-wise.aspx.cs" Inherits="school_web.Admin.misc_fee_month_wise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Miscellaneous Fees for Month  
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
                        <li><a href="misc-fee-month-wise.aspx" class="sub-mnu-p-a-active">Month Misc Fee</a></li>
                        <li><a href="misc-fee-student-wise.aspx">Student-Wise Misc Fee</a></li>
                    </ul>
                </div>
                <hr />
                <div class="col-xl-12">
                    <div class="card">
                        <div class="card-body">
                            <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Add Misc. Fees Month Wise"></asp:Label>
                            <hr />
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation" novalidate="">
                                    <div class="col-md-2">
                                        <label for="validationCustom01" class="form-label">Session<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_session" runat="server" class="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddl_session_SelectedIndexChanged"></asp:DropDownList>
                                    </div>

                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Particular<sup>*</sup></label>
                                        <asp:TextBox ID="txt_perticular" runat="server" class="form-control"></asp:TextBox>
                                    </div>

                                    <div class="col-md-2">
                                        <label for="validationCustom01" class="form-label">Total Amount<sup>*</sup></label>
                                        <asp:TextBox ID="txt_amount" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <label for="validationCustom01" class="form-label">Apply For<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_apply_for" runat="server" class="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddl_apply_for_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3" id="pnl_hostel" runat="server">
                                        <label for="validationCustom01" class="form-label">Select Hostel<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_hostel" runat="server" class="form-select"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3" id="pnl_day_boarding" runat="server" visible="false">
                                        <label for="validationCustom01" class="form-label">Boarding Type<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_day_boarding" runat="server" class="form-select"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Ledger effected<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_ledger" runat="server" class="form-select"></asp:DropDownList>
                                    </div>

                                    <div class="col-md-12">
                                        <label for="validationCustom01" class="form-label">Choose Class<sup>*</sup></label>
                                         <span class="chkbx-all">
                                            <asp:CheckBox ID="chk_all" runat="server" Text="Select All" OnCheckedChanged="chk_all_CheckedChanged" AutoPostBack="true" /></span>
                                        <br /> 
                                        <asp:Repeater ID="rp_class" runat="server" OnItemDataBound="rp_class_ItemDataBound">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_class" class="chkstle" runat="server" Text='<%#Eval("Course_Name") %>' />
                                                <asp:Label ID="lbl_class_id" runat="server" Visible="false" Text='<%#Bind("course_id")%>'></asp:Label>
                                                <asp:Label ID="lbl_course_name" runat="server" Visible="false" Text='<%#Bind("Course_Name")%>'></asp:Label>
                                                <asp:Label ID="lbl_status" runat="server" Visible="false" Text='<%#Bind("status")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>

                                    <div class="col-md-12">
                                        <label for="validationCustom01" class="form-label">Choose Month<sup>*</sup></label>
                                       <span class="chkbx-all">
                                            <asp:CheckBox ID="chk_all_month" runat="server" Text="Select All" OnCheckedChanged="chk_all_month_CheckedChanged" AutoPostBack="true" /></span>
                                        <br />
                                        <asp:Repeater ID="rp_month" runat="server" OnItemDataBound="rp_month_ItemDataBound">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_month_name" class="chkstle" runat="server" Text='<%#Eval("Month") %>' />
                                                <asp:Label ID="lbl_value" runat="server" Visible="false" Text='<%#Bind("Value")%>'></asp:Label>
                                                <asp:Label ID="lbl_month_name" runat="server" Visible="false" Text='<%#Bind("Month")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:Repeater>
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
                            <h6 class="mb-0 text-uppercase">Added Misc. Fees Month Wise</h6>
                            <hr />
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="find-dv">
                                                <div class="row">
                                                    <div class="col-sm-4">
                                                        <label for="validationCustom01" class="find-dv-lbl">Session</label>
                                                        <asp:DropDownList ID="ddlsession" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddlsession_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label for="validationCustom01" class="find-dv-lbl">Class</label>
                                                        <asp:DropDownList ID="ddlclass" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                    </div>


                                                    <div class="col-sm-1">
                                                        <asp:Button ID="btn_fnd_by_class" runat="server" Text="find" class="btn btn-primary find-dv-btn" OnClick="btn_fnd_by_class_Click" />
                                                    </div>

                                                </div>
                                            </div>
                                            <table id="example2" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                <thead>
                                                    <tr>
                                                        <th>#</th>
                                                        <th>Session Class</th>
                                                        <th>Class</th>
                                                        <th>Particular</th>
                                                        <th>Month</th>
                                                        <th>Apply For</th>
                                                        <th>Hostel Name</th>
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
                                                                    <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("Session")%>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("Class")%>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="lbl_particular" runat="server" Text='<%#Bind("Particular")%>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="lbl_month" runat="server" Text='<%#Bind("Month")%>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="lbl_apply_for" runat="server" Text='<%#Bind("Apply_for")%>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="lbl_hostel_name" runat="server" Text='<%#Bind("Hostel_Name")%>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("Amount")%>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Edit"> <i class="lni lni-pencil-alt"> </i></asp:LinkButton>
                                                                    <asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"> </i></asp:LinkButton>

                                                                    <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lbl_class_id" runat="server" Text='<%#Bind("class_id")%>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lbl_session_id" runat="server" Text='<%#Bind("session_id")%>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lbl_hostel_id" runat="server" Text='<%#Bind("Hostel_id")%>' Visible="false"></asp:Label>

                                                                    <asp:Label ID="lbl_day_boarding" runat="server" Text='<%#Bind("day_boarding")%>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lbl_day_boarding_with_lunch" runat="server" Text='<%#Bind("day_boarding_with_lunch")%>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lbl_Ledger" runat="server" Text='<%#Bind("Ledger")%>' Visible="false"></asp:Label>
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

    <!--end page wrapper -->
    <asp:HiddenField ID="hd_id" runat="server" />
</asp:Content>
