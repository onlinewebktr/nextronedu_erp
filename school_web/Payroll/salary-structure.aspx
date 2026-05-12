<%@ Page Title="" Language="C#" MasterPageFile="~/Payroll/Adminmaster.Master" AutoEventWireup="true" CodeBehind="salary-structure.aspx.cs" Inherits="school_web.Payroll.salary_structure" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Salary Structure
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
                <div class="breadcrumb-title pe-3">Employee</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Salary Structure</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase"></h6>
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
                                                        <asp:DropDownList ID="ddl_grade" runat="server" class="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddl_grade_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <label for="validationCustom01" class="find-dv-lbl">Employee Name</label>
                                                        <asp:DropDownList ID="ddl_employee_name" runat="server" class="form-select"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-1">
                                                        <asp:Button ID="btn_find" runat="server" Text="find" class="btn btn-primary find-dv-btn" OnClick="btn_find_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="grd-wpr">
                                                <table id="example21" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                    <thead>
                                                        <tr>
                                                            <th>#</th>
                                                            <th>Name</th>
                                                            <th>Gender</th>
                                                            <th>DOB</th>
                                                            <th>Emp. Code</th>
                                                            <th>Mobile No.</th>
                                                            <th>Department</th>
                                                            <th>Designation</th>

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
                                                                        <asp:Label ID="lbl_Gender" runat="server" Text='<%#Bind("Gender")%>'></asp:Label>
                                                                    </td>

                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_date_of_birth" runat="server" Text='<%#Bind("Date_of_birth")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_emp_code" runat="server" Text='<%#Bind("Emp_Code")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_mobile" runat="server" Text='<%#Bind("Mobile")%>'></asp:Label>
                                                                    </td>

                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="Label2" runat="server" Text='<%#Bind("Department_name")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="Label3" runat="server" Text='<%#Bind("Designation_name")%>'></asp:Label>
                                                                        <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </tbody>
                                                </table>
                                            </div>

                                            <div class="row">
                                                <div class="col-xl-5">
                                                    <div class="grd-wpr">
                                                        <table id="Table1" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                            <thead>
                                                                <tr>
                                                                    <th>#</th>
                                                                    <th>Income Head</th>
                                                                    <th>Amount to be paid under head</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <asp:Repeater ID="rp_income" runat="server">
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lbl_inc_head" runat="server" Text='<%#Bind("Income_head")%>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:TextBox ID="txt_paid_amt" runat="server" class="form-control find-dv-txtbx" AutoPostBack="true" OnTextChanged="txt_paid_amt_TextChanged" Text='<%#Bind("Paid_Amount")%>'></asp:TextBox>


                                                                                <asp:Label ID="lbl_id" runat="server" Visible="false" Text='<%#Bind("Id")%>'></asp:Label>
                                                                                <asp:Label ID="lbl_formula_type" Visible="false" runat="server" Text='<%#Bind("Formula_Type")%>'></asp:Label>
                                                                                <asp:Label ID="lbl_formula" Visible="false" runat="server" Text='<%#Bind("Formula")%>'></asp:Label>
                                                                                <asp:Label ID="lbl_is_active" Visible="false" runat="server" Text='<%#Bind("is_active")%>'></asp:Label>
                                                                                <asp:Label ID="lbl_income_type_id" Visible="false" runat="server" Text='<%#Bind("Income_Type")%>'></asp:Label>
                                                                                <asp:Label ID="lbl_grade_id" Visible="false" runat="server" Text='<%#Bind("Grade_id")%>'></asp:Label>
                                                                                <asp:Label ID="lbl_employee_id" Visible="false" runat="server" Text='<%#Bind("Employee_id")%>'></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                                <div class="col-xl-7">
                                                    <div class="grd-wpr">
                                                        <table id="Table2" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                            <thead>
                                                                <tr>
                                                                    <th>#</th>
                                                                    <th>Deduction Head</th>
                                                                    <th>Amount to be deducted</th>
                                                                    <th>Employee's Contribution</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <asp:Repeater ID="rp_emp_deduction" runat="server">
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lbl_deduct_head" runat="server" Text='<%#Bind("Deduction_head")%>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lbl_amt_to_deducted" runat="server" Text='<%#Bind("Deducted_Amount")%>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lbl_emp_contribution" runat="server" Text='<%#Bind("Employer_Contribution")%>'></asp:Label>

                                                                                <asp:Label ID="lbl_is_active" runat="server" Visible="false" Text='<%#Bind("is_active")%>'></asp:Label>
                                                                                <asp:Label ID="lbl_deduction_type" Visible="false" runat="server" Text='<%#Bind("Deduction_Type")%>'></asp:Label>
                                                                                <asp:Label ID="lbl_employer_contribution" Visible="false" runat="server" Text='<%#Bind("Employer_Contribution")%>'></asp:Label>
                                                                                <asp:Label ID="lbl_deducted_amount" runat="server" Visible="false" Text='<%#Bind("Deducted_Amount")%>'></asp:Label>
                                                                                <asp:Label ID="lbl_deduction_type_id" Visible="false" runat="server" Text='<%#Bind("Deduction_Type")%>'></asp:Label>
                                                                                <asp:Label ID="lbl_d_grade_id" Visible="false" runat="server" Text='<%#Bind("Grade_id")%>'></asp:Label>
                                                                                <asp:Label ID="lbl_d_employee_id" Visible="false" runat="server" Text='<%#Bind("Employee_id")%>'></asp:Label>
                                                                                <asp:Label ID="lbl_d_formula" runat="server" Visible="false" Text='<%#Bind("Formula")%>'></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>

                                            <asp:Panel ID="pnl_calculation" runat="server" Visible="false">
                                                <div class="row">
                                                    <div class="col-xl-6">
                                                        <div class="frms-rows">
                                                            <div class="row">
                                                                <div class="col-xl-4">
                                                                    <label for="validationCustom01" class="form-label">Gross Salary<sup>*</sup></label>
                                                                </div>
                                                                <div class="col-xl-5">
                                                                    <asp:TextBox ID="txt_gross_salary" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="frms-rows">
                                                            <div class="row">
                                                                <div class="col-xl-4">
                                                                    <label for="validationCustom01" class="form-label">P.F. Type<sup>*</sup></label>
                                                                </div>
                                                                <div class="col-xl-5">
                                                                    <asp:RadioButton ID="chk_pf_both" runat="server" Text="Both" GroupName="aa" />
                                                                    <asp:RadioButton ID="chk_pf_employer" OnCheckedChanged="chk_pf_employer_CheckedChanged" GroupName="aa" AutoPostBack="true" runat="server" Text="Employer" />
                                                                    <asp:RadioButton ID="chk_pf_without_limit" runat="server" Visible="false" Text="Without Limit" GroupName="aa" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="frms-rows">
                                                            <div class="row">
                                                                <div class="col-xl-4">
                                                                    <label for="validationCustom01" class="form-label">Payment Type<sup>*</sup></label>
                                                                </div>
                                                                <div class="col-xl-5">
                                                                    <asp:DropDownList ID="ddl_payment_type" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="frms-rows">
                                                            <div class="row">
                                                                <div class="col-xl-4">
                                                                    <label for="validationCustom01" class="form-label">Total Deduction<sup>*</sup></label>
                                                                </div>
                                                                <div class="col-xl-5">
                                                                    <asp:TextBox ID="txt_deduction" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <asp:Panel ID="pnl_voluntry_pf" runat="server" Visible="false">
                                                            <div class="frms-rows">
                                                                <div class="row">
                                                                    <div class="col-xl-4">
                                                                        <label for="validationCustom01" class="form-label">Voluntry P.F.<sup>*</sup></label>
                                                                    </div>
                                                                    <div class="col-xl-5">
                                                                        <asp:CheckBox ID="chk_voluntry_pf" runat="server" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </asp:Panel>
                                                        <asp:Panel ID="Panel2" runat="server" Visible="false">
                                                            <div class="frms-rows">
                                                                <div class="row">
                                                                    <div class="col-xl-4">
                                                                        <label for="validationCustom01" class="form-label">Deduct Prof. Tax<sup>*</sup></label>
                                                                    </div>
                                                                    <div class="col-xl-5">
                                                                        <asp:CheckBox ID="chk_deduct_prof_tax" runat="server" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </asp:Panel>


                                                        <div class="frms-rows">
                                                            <div class="row">
                                                                <div class="col-xl-4">
                                                                    <label for="validationCustom01" class="form-label">Employer Contribution<sup>*</sup></label>
                                                                </div>
                                                                <div class="col-xl-5">
                                                                    <asp:TextBox ID="txt_emp_cont" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="frms-rows">
                                                            <div class="row">
                                                                <div class="col-xl-4">
                                                                    <label for="validationCustom01" class="form-label">Net Salary<sup>*</sup></label>
                                                                </div>
                                                                <div class="col-xl-5">
                                                                    <asp:TextBox ID="txt_net_salary" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="frms-rows">
                                                            <div class="row">
                                                                <div class="col-xl-4">
                                                                    <asp:CheckBox ID="chk_ot_applicable" runat="server" Style="font-weight: 500" Text="Is O.T. Applicable" AutoPostBack="true" OnCheckedChanged="chk_ot_applicable_CheckedChanged" />
                                                                </div>
                                                                <div class="col-xl-5">
                                                                    <asp:DropDownList ID="ddl_ot_type" runat="server" class="form-select find-dv-txtbx" OnSelectedIndexChanged="ddl_ot_type_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <asp:Panel ID="Panel1" runat="server" Visible="false">
                                                            <div class="frms-rows">
                                                                <div class="row">
                                                                    <div class="col-xl-4">
                                                                        <label for="validationCustom01" class="form-label">Monthly CTC</label>
                                                                    </div>
                                                                    <div class="col-xl-5">
                                                                        <asp:TextBox ID="txt_monthly_ctc" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="frms-rows">
                                                                <div class="row">
                                                                    <div class="col-xl-4">
                                                                        <label for="validationCustom01" class="form-label">Incentive applicable</label>
                                                                    </div>
                                                                    <div class="col-xl-5">
                                                                        <asp:CheckBox ID="chk_incentive_applicable" runat="server" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </asp:Panel>

                                                        <asp:Panel ID="pnl_ot" runat="server">
                                                            <asp:Panel ID="pnl_ot_hourly" runat="server" Visible="false">
                                                                <div class="frms-rows">
                                                                    <div class="row">
                                                                        <div class="col-xl-4">
                                                                            <label for="validationCustom01" class="form-label">Rate/Hr</label>
                                                                        </div>
                                                                        <div class="col-xl-5">
                                                                            <asp:TextBox ID="txt_ot_formula" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </asp:Panel>
                                                            <asp:Panel ID="pnl_ot_calculated" runat="server" Visible="false">
                                                                <div class="frms-rows">
                                                                    <div class="row">
                                                                        <div class="col-xl-4">
                                                                            <label for="validationCustom01" class="form-label">Net Salary  X</label>

                                                                        </div>
                                                                        <div class="col-xl-5">
                                                                            <asp:TextBox ID="txt_multiplier" runat="server" class="form-control find-dv-txtbx" AutoPostBack="true" OnTextChanged="txt_multiplier_TextChanged"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <p>Total working days X Min working hour</p>
                                                                </div>
                                                            </asp:Panel>
                                                        </asp:Panel>
                                                    </div>


                                                    <div class="col-12">
                                                        <asp:Button ID="btn_Submit" runat="server" Text="Save" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btn_Submit_Click" />
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
        <!--end row-->
    </div>
    <asp:HiddenField ID="hd_id" runat="server" />
    <!--end page wrapper -->


    <style>
        .frms-rows {
            margin: 10px 0px 2px 0px;
            padding: 0px;
            width: 100%;
            float: left;
        }

        .grd-wpr {
            margin: 5px 0px 5px 0px;
            padding: 0px;
            width: 100%;
            float: left;
            border: 1px solid #ddd;
            border-right: 0px;
        }

        table.dataTable {
            clear: both;
            margin-top: 0px !important;
            margin-bottom: 0px !important;
        }
    </style>
</asp:Content>
