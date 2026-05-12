<%@ Page Title="" Language="C#" MasterPageFile="~/Payroll/Adminmaster.Master" AutoEventWireup="true" CodeBehind="salary-chart.aspx.cs" Inherits="school_web.Payroll.salary_chart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Salary Chart
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
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
                            <li class="breadcrumb-item active" aria-current="page">Salary Chart</li>
                        </ol>
                    </nav>
                </div>
            </div>

            <div class="row">
                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase">Salary Chart</h6>
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
                                                        <asp:Button ID="btn_find" runat="server" Text="find" class="btn btn-primary find-dv-btn" OnClick="btn_find_Click" />
                                                    </div>
                                                </div>
                                            </div>

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
                                                        <asp:TemplateField HeaderText="Designation">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Designation" runat="server" Text='<%#Bind("Designation") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Month Days">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Days_in_month" runat="server" Text='<%#Bind("Days_in_month") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Working days">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_total_working_days" runat="server" Text='<%#Bind("total_working_days") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Calculate Salary On">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_calc_salary_on" runat="server" Text='<%#Bind("total_working_days") %>'></asp:Label>
                                                                Days
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Present">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_present_days" runat="server" Text='<%#Bind("present_days") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Half Days">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_half_days" runat="server" Text='<%#Bind("half_days") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Leave">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_leave" runat="server" Text='<%#Bind("leave") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Overtime">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_over_time_working_days" runat="server" Text='<%#Bind("over_time_working_days") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_total" runat="server" Text='<%#Bind("total") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>



                                                        <asp:TemplateField HeaderText="Salary">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Salary" runat="server" Text='<%#Bind("Salary") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Salary Basic Fix">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_salary_Basic_fix" runat="server" Text='<%#Bind("Salary_Basic_fix") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Salary Basic">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Salary_Basic" runat="server" Text='<%#Bind("Salary_Basic") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Gross Salary">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Gross_Salary" runat="server" Text='<%#Bind("Gross_Salary") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total Salary">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Total_Salary" runat="server" Text='<%#Bind("Total_Salary") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PF@ 12%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_emp_PF" runat="server" Text='<%#Bind("emp_PF") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ESI@ 0.75%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_emp_esi" runat="server" Text='<%#Bind("emp_esi") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Employee Contribution">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_emp_contribution" runat="server" Text='<%#Bind("emp_contribution") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PF @13.36%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_com_PF" runat="server" Text='<%#Bind("com_PF") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ESI@ 3.75%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_com_ESI" runat="server" Text='<%#Bind("com_ESI") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Employer Contribution">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_com_contribution" runat="server" Text='<%#Bind("com_contribution") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Salary after Pf & Esi">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Salary_After_PF_ESI" runat="server" Text='<%#Bind("Salary_After_PF_ESI") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Advance Adjust">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Advance" runat="server" Text='<%#Bind("Advance") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Net Salary">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Net_Salary" runat="server" Text='<%#Bind("Net_Salary") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Through Bank">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Through_Bank" runat="server" Text='<%#Bind("Through_Bank") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Through Cheque">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Through_Cheque" runat="server" Text='<%#Bind("Through_Cheque") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Through Cash">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Through_Cash" runat="server" Text='<%#Bind("Through_Cash") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Salary Slip">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_employee_id" Visible="false" runat="server" Text='<%#Bind("Employee_id") %>'></asp:Label>
                                                                <asp:Label ID="lbl_month" Visible="false" runat="server" Text='<%#Bind("month") %>'></asp:Label>
                                                                <asp:Label ID="lbl_year" Visible="false" runat="server" Text='<%#Bind("year") %>'></asp:Label>
                                                                <a target="_blank" href="print/salary-slip.aspx?empID=<%# Eval("Employee_id") %>&month=<%# Eval("month") %>&year=<%# Eval("year") %>">
                                                                    <asp:Label ID="lbl_addressprint" Style="min-width: 45px;" class="lnk-btn" runat="server" Text="Print"></asp:Label></a>


                                                                <asp:LinkButton Style="display: none" ID="lnk_print_slip" OnClick="lnk_print_slip_Click" class="lnk-btn" runat="server">Print</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                 
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
