<%@ Page Title="" Language="C#" MasterPageFile="~/Payroll/Adminmaster.Master" AutoEventWireup="true" CodeBehind="view-salary-headwise.aspx.cs" Inherits="school_web.Payroll.view_salary_headwise" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    View Salary HeadWise
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
                <div class="breadcrumb-title pe-3">Salary</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">View Salary HeadWise</li>
                        </ol>
                    </nav>
                </div>
            </div>

            <div class="row">
                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase">View Salary HeadWise</h6>
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
                                                        <label for="validationCustom01" class="find-dv-lbl">Grade</label>
                                                        <asp:DropDownList ID="ddl_grade" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Month</label>
                                                        <asp:DropDownList ID="ddl_month" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Year</label>
                                                        <asp:DropDownList ID="ddl_year" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                    </div>

                                                    <div class="col-sm-3">
                                                        <asp:Button ID="btn_find" runat="server" Text="find" class="btn btn-primary find-dv-btn" OnClick="btn_find_Click" />
                                                    </div>

                                                    <div class="col-sm-1">
                                                        <asp:Label ID="lbl_ttl_no" Style="margin: 25px 0px 0px 0px; float: left;"
                                                            runat="server" class="prnt-date-p" Text=""></asp:Label>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <asp:LinkButton ID="lnk_excel" OnClick="lnk_excel_Click" runat="server" class="btn btn-primary find-dv-btn"><i class="bx bx-download"></i>  Excel</asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="grd-wpr">
                                                <asp:Panel ID="pnl_grids" runat="server" Visible="false">

                                                    <div id="tblPrintIQ" runat="server">
                                                        <div class="prnt-dv-wpr-mth">
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
                                                                            <asp:Label ID="lbl_Days_in_month" runat="server" Text='<%#Bind("Month_days") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Working days">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_total_working_days" runat="server" Text='<%#Bind("Working_days") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Calculate Salary On">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_salary_calculate_on" runat="server" Text='<%#Bind("salary_calculate_on") %>'></asp:Label>
                                                                            Days
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Present">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_present_days" runat="server" Text='<%#Bind("Present") %>'></asp:Label>
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
                                                                            <asp:Label ID="lbl_over_time_working_days" runat="server" Text='<%#Bind("Over_Time") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Total">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_total" runat="server" Text='<%#Bind("total") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>


                                                                    <asp:TemplateField HeaderText="Salary Basic Fix">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_Salary_Basic_fix" runat="server" Text='<%#Bind("Salary_Basic_fix") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                      


                                                                    <asp:TemplateField HeaderText="Salary Basic">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_salary_basic" runat="server" Text='<%#Bind("Salary_Basic") %>'></asp:Label>
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
                                                                    <asp:TemplateField HeaderText="PF Employee">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_PF_Employee" runat="server" Text='<%#Bind("PF_Employee") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="ESI_Employee">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_ESI_Employee" runat="server" Text='<%#Bind("ESI_Employee") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Employee Contribution">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_Employee_Contribution" runat="server" Text='<%#Bind("Employee_Contribution") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="PF Employer">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_PF_Employer" runat="server" Text='<%#Bind("PF_Employer") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="ESI Employer">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_ESI_Employer" runat="server" Text='<%#Bind("ESI_Employer") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Employer Contribution">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_Employer_Contribution" runat="server" Text='<%#Bind("Employer_Contribution") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Salary After PF ESI">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_Salary_After_PF_ESI" runat="server" Text='<%#Bind("Salary_After_PF_ESI") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Advance">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_Advance" runat="server" Text='<%#Bind("Advance") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Net_Salary">
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
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
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
        </div>
        <!--end row-->
    </div>
</asp:Content>
