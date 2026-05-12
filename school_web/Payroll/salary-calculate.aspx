<%@ Page Title="" Language="C#" MasterPageFile="~/Payroll/Adminmaster.Master" AutoEventWireup="true" CodeBehind="salary-calculate.aspx.cs" Inherits="school_web.Payroll.salary_calculate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Salary Calculate
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .table-responsive {
            overflow-x: inherit;
        }

        th {
            font-weight: 500;
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
                <div class="breadcrumb-title pe-3">Salary</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Salary Calculate</li>
                        </ol>
                    </nav>
                </div>
            </div>

            <div class="row">
                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase">Salary Calculate</h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5">
                                    <div class="row">
                                        <div class="col-sm-12">

                                            <div class="find-dv">
                                                <div class="row">

                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Month</label>
                                                        <asp:DropDownList ID="ddl_month" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Year</label>
                                                        <asp:DropDownList ID="ddl_year" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                    </div>

                                                    <div class="col-sm-1">
                                                        <asp:Button ID="btn_find" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_find_Click" />
                                                    </div>
                                                </div>
                                            </div>

                                            <asp:Panel ID="pnl_data_grid" runat="server" Visible="false">
                                                <div class="grd-wpr">
                                                    <asp:GridView ID="grd_salary" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="False" AllowPaging="false" Font-Bold="False" Style="margin-top: 0; width: 100%; overflow: scroll" class="table table-striped table-bordered dataTable">
                                                        <RowStyle />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="#">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSRNO" runat="server" Text="<%#Container.DataItemIndex+1 %>"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Employee Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Employee_Name" runat="server" Text='<%#Bind("Employee_Name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Days in month">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Days_in_month" runat="server" Text='<%#Bind("Days_in_month") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="total working days">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_total_working_days" runat="server" Text='<%#Bind("total_working_days") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Present days">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_present_days" runat="server" Text='<%#Bind("present_days") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Half days">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_half_days" runat="server" Text='<%#Bind("half_days") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Leave">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_leave" runat="server" Text='<%#Bind("leave") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Over Time">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_over_time_working_days" runat="server" Text='<%#Bind("over_time_working_days") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_total" runat="server" Text='<%#Bind("total") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Attendance Issue">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_attendance_issue" runat="server" Text='<%#Bind("attendance_issue") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-Width="125px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_employee_id" runat="server" Visible="false" Text='<%#Bind("Employee_id") %>'></asp:Label>

                                                                    <asp:LinkButton ID="link_view" runat="server" class="lnk-btn" OnClick="link_view_Click">View</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>


                                                    <asp:Button ID="btn_Submit" runat="server" Style="float: right; padding: 5px 10px 5px 10px; font-size: 15px;"
                                                        Text="Save & Calculate" CssClass="btn btn-primary" OnClick="btn_Submit_Click" />

                                                </div>
                                            </asp:Panel>
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


    <div class="conf-alrt-sec" id="alrt_calculted_slry" runat="server" visible="false">
        <div class="conf-alrt-inr" style="width: 400px;">
            <p class="conf-alrt-msg-p" runat="server" id="lbl_msg_S"></p>
            <ul class="conf-btn-ul">
                <li>
                    <asp:LinkButton ID="LinkButton1" OnClick="btn_no_Click" runat="server" Style="background: #fff; border: 1px solid #ddd; color: #0072ff;">No</asp:LinkButton></li>
                <li>
                    <asp:LinkButton ID="btn_slry_calculate" OnClick="btn_slry_calculate_Click" runat="server">Yes</asp:LinkButton></li>
            </ul>
        </div>
    </div>

    <div class="conf-alrt-sec" id="conf_alrt" runat="server" visible="false">
        <div class="conf-alrt-inr" style="width: 400px;">
            <p class="conf-alrt-msg-p" runat="server" id="alert_msg"></p>
            <ul class="conf-btn-ul">
                <li>
                    <asp:LinkButton ID="btn_no" OnClick="btn_no_Click" runat="server" Style="background: #fff; border: 1px solid #ddd; color: #0072ff;">No</asp:LinkButton></li>
                <li>
                    <asp:LinkButton ID="btn_yes" OnClick="btn_yes_Click" runat="server">Yes</asp:LinkButton></li>
            </ul>
        </div>
    </div>


    <div class="conf-alrt-sec" id="myModal2" runat="server" visible="false">
        <div class="conf-alrt-inr" style="width: 650px; height: 500px; overflow: auto;">
            <div class="popupTablWpR">
                <div class="row">
                    <div class="col-xl-8">
                        <h2 class="popup-dt-h">View Attendance</h2>
                    </div>
                    <div class="col-xl-4">
                        <ul class="conf-btn-ul" style="margin: -5px 0px 0px 0px;">
                            <li>
                                <asp:LinkButton ID="lnk_close" runat="server" OnClick="lnk_close_Click">Close</asp:LinkButton>
                            </li>
                        </ul>
                    </div>
                </div>
                <asp:GridView ID="grd_dtl" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="False" AllowPaging="false" Font-Bold="False" Style="margin-top: 0; width: 100%; overflow: scroll" class="table table-striped table-bordered dataTable">
                    <RowStyle />
                    <Columns>
                        <asp:TemplateField HeaderText="#">
                            <ItemTemplate>
                                <asp:Label ID="lblSRNO" runat="server" Text="<%#Container.DataItemIndex+1 %>"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Shift 1 in">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Shift_1_in" runat="server" Text='<%#Bind("Shift_1_in") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Shift 1 out">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Shift_1_out" runat="server" Text='<%#Bind("Shift_1_out") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Shift 2 in">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Shift_2_in" runat="server" Text='<%#Bind("Shift_2_in") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Shift 2 out">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Shift_2_out" runat="server" Text='<%#Bind("Shift_2_out") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

            </div>
        </div>
    </div>
</asp:Content>
