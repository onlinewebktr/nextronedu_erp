<%@ Page Title="" Language="C#" MasterPageFile="~/Payroll/Adminmaster.Master" AutoEventWireup="true" CodeBehind="add-employee.aspx.cs" Inherits="school_web.Payroll.add_employee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Add Employee
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .form-ttleS {
            margin: 0px 0px 0px 0px;
            padding: 8px 10px 5px 10px;
            width: 100%;
            float: left;
            font-size: 18px;
            color: #0296bd;
            border-bottom: 1px solid #ddd;
        }

        .form-label {
            margin-bottom: 2px;
        }

        th {
            font-weight: 500;
        }

        .form-control:disabled, .form-control[readonly] {
            background-color: #ffffff;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hd_temp_id" runat="server" />
    <asp:DropDownList ID="ddl_employee" runat="server"></asp:DropDownList>
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
                <div class="breadcrumb-title pe-3">Admission</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Add Employee</li>
                        </ol>
                    </nav>
                </div>
            </div>


            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Add Employee"></asp:Label>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded" style="padding: 0px !important;">
                                <h2 class="form-ttleS">Personal Details</h2>
                                <div class="row g-3 needs-validation" style="padding: 0px 10px 10px 10px;">
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Employee Type<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_emp_type" runat="server" class="form-select find-dv-txtbx">
                                            <asp:ListItem>Admin</asp:ListItem>
                                            <asp:ListItem>User</asp:ListItem>
                                            <asp:ListItem>Principal</asp:ListItem>
                                            <asp:ListItem>Coordinator</asp:ListItem>
                                            <asp:ListItem>Teacher</asp:ListItem>
                                            <asp:ListItem>Operator</asp:ListItem>
                                            <asp:ListItem>Non Teaching Staff</asp:ListItem>
                                            <asp:ListItem>HOD</asp:ListItem>
                                            <asp:ListItem>Councillor</asp:ListItem>
                                            <asp:ListItem>Trainer</asp:ListItem>
                                            <asp:ListItem>Lab Assistant</asp:ListItem>
                                            <asp:ListItem>Clerk</asp:ListItem>
                                            <asp:ListItem>Chairman</asp:ListItem>
                                            <asp:ListItem>Secretary</asp:ListItem>
                                            <asp:ListItem>Director</asp:ListItem>
                                            <asp:ListItem>Advisor</asp:ListItem>
                                            <asp:ListItem>Member of Committee</asp:ListItem>
                                            <asp:ListItem>Peon</asp:ListItem>
                                            <asp:ListItem>Helper</asp:ListItem>
                                            <asp:ListItem>Examination Incharge</asp:ListItem>
                                            <asp:ListItem>Accountant</asp:ListItem>
                                            <asp:ListItem>Vice Principle</asp:ListItem>
                                            <asp:ListItem>P.R.O.</asp:ListItem>
                                            <asp:ListItem>Receptionist</asp:ListItem>
                                            <asp:ListItem>Office Secretary</asp:ListItem>
                                            <asp:ListItem>Attendant</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Employee Name<sup>*</sup></label>
                                        <asp:TextBox ID="txt_emp_name" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>

                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Gender</label>
                                        <asp:DropDownList ID="ddl_gender" runat="server" class="form-select find-dv-txtbx">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>Male</asp:ListItem>
                                            <asp:ListItem>Female</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Date of Birth<sup>*</sup></label>
                                        <div class="clndr-div">
                                            <asp:TextBox ID="txt_dob" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                            <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Blood Group <sup></sup></label>
                                        <asp:DropDownList ID="ddl_blood_group" runat="server" class="form-select find-dv-txtbx">
                                            <asp:ListItem>A+</asp:ListItem>
                                            <asp:ListItem>A-</asp:ListItem>
                                            <asp:ListItem>B+</asp:ListItem>
                                            <asp:ListItem>B-</asp:ListItem>
                                            <asp:ListItem>O+</asp:ListItem>
                                            <asp:ListItem>O-</asp:ListItem>
                                            <asp:ListItem>AB+</asp:ListItem>
                                            <asp:ListItem>AB-</asp:ListItem>
                                            <asp:ListItem>N/A</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label" style="width: 100%;">Religion</label>
                                        <asp:DropDownList ID="ddl_religion" runat="server" class="form-select find-dv-txtbx">
                                            <asp:ListItem>Hindu</asp:ListItem>
                                            <asp:ListItem>Islam</asp:ListItem>
                                            <asp:ListItem>Sikh</asp:ListItem>
                                            <asp:ListItem>Christian</asp:ListItem>
                                            <asp:ListItem>Buddhism</asp:ListItem>
                                            <asp:ListItem>Jain</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label" style="width: 100%;">Marital Status</label>
                                        <asp:DropDownList ID="ddl_marital_status" runat="server" class="form-select find-dv-txtbx">
                                            <asp:ListItem>Unmarried</asp:ListItem>
                                            <asp:ListItem>Married</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Father's/Husband Name</label>
                                        <asp:TextBox ID="txt_father_name" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">PAN</label>
                                        <asp:TextBox ID="txt_pan" runat="server" class="form-control find-dv-txtbx" MaxLength="10"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Address</label>
                                        <asp:TextBox ID="txt_address" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">City</label>
                                        <asp:TextBox ID="txt_city" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Pincode</label>
                                        <asp:TextBox ID="txt_pin" runat="server" class="form-control find-dv-txtbx" MaxLength="6" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">State</label>
                                        <asp:DropDownList ID="ddl_state" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Email</label>
                                        <asp:TextBox ID="txt_email" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Mobile No.</label>
                                        <asp:TextBox ID="txt_mobile" runat="server" class="form-control find-dv-txtbx" MaxLength="10" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Bank Name</label>
                                        <asp:TextBox ID="txt_bank" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Branch </label>
                                        <asp:TextBox ID="txt_branch" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Account No.</label>
                                        <asp:TextBox ID="txt_ac_no" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">IFSC</label>
                                        <asp:TextBox ID="txt_ifsc" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">MICR</label>
                                        <asp:TextBox ID="txt_micr" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <%-- ========== --%>
                            <div class="p-4 border rounded" style="padding: 0px !important; margin: 10px 0px 0px 0px;">
                                <h2 class="form-ttleS">Organization Details</h2>
                                <div class="row g-3 needs-validation" style="padding: 0px 10px 10px 10px;">
                                    <asp:CheckBox ID="chk_auto_generate_emp_code" runat="server" OnCheckedChanged="chk_auto_generate_emp_code_CheckedChanged" AutoPostBack="true" Text="If you want to auto create Employee Code" HorizontalAlignment="Left" />
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Employee Code </label>
                                        <asp:TextBox ID="txt_emp_code" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Punch Card No</label>
                                        <asp:TextBox ID="txt_punch_card_no" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Official Email Id</label>
                                        <asp:TextBox ID="txt_offifial_email" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Qualification</label>
                                        <asp:TextBox ID="txt_qualification" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>


                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Grade </label>
                                        <asp:DropDownList ID="ddl_grade" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Department</label>
                                        <asp:DropDownList ID="ddl_department" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Designation</label>
                                        <asp:DropDownList ID="ddl_designation" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3"></div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">E.P.F. No.</label>
                                        <asp:TextBox ID="txt_epf_no" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>


                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Join Date </label>
                                        <div class="clndr-div">
                                            <asp:TextBox ID="txt_joining_date" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                            <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">PF leaving Date</label>
                                        <div class="clndr-div">
                                            <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                            <asp:TextBox ID="txt_pf_leaving_date" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Reason</label>
                                        <asp:TextBox ID="txt_reason" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">ESIC No.</label>
                                        <asp:TextBox ID="txt_esic_no" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>


                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Join Date </label>
                                        <div class="clndr-div">
                                            <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                            <asp:TextBox ID="txt_join_date" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">ESIC Leaving Date</label>
                                        <div class="clndr-div">
                                            <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                            <asp:TextBox ID="txt_esic_leaving_date" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Reason</label>
                                        <asp:TextBox ID="txt_reason_esic" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Date of Joining</label>
                                        <div class="clndr-div">
                                            <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                            <asp:TextBox ID="txt_emp_join_date" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Password<sup>*</sup></label>
                                        <asp:TextBox ID="txt_pwd" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>


                            <div class="p-4 border rounded" style="padding: 0px !important; margin: 10px 0px 0px 0px;">
                                <h2 class="form-ttleS">Upload Document</h2>
                                <div class="row g-3 needs-validation" style="padding: 0px 10px 10px 10px;">
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Document Name</label>
                                        <asp:TextBox ID="txt_doc_name" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Choose Document</label>
                                        <asp:FileUpload ID="FileUpload2" runat="server" class="form-control find-dv-txtbx" />
                                    </div>
                                    <div class="col-md-3">
                                        <asp:Button ID="btn_upload_doc" runat="server" Text="Upload" CssClass="btn btn-primary" OnClick="btn_upload_doc_Click" Style="margin: 23px 0px 0px 0px;" />
                                    </div>


                                    <div class="col-md-12" id="documentS" runat="server" visible="false">
                                        <label for="validationCustom01" class="form-label">Uploaded Document</label>
                                        <table id="example21" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                            <thead>
                                                <tr>
                                                    <th>#</th>
                                                    <th>Document Name</th>
                                                    <th>Document</th>
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
                                                                    <asp:Label ID="lbl_doc_name" runat="server" Text='<%#Bind("Document_Name")%>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <a href='<%# Eval("Document") %>' download>View</a>
                                                                </td>
                                                                <td style="text-align: left;">
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


                            <div class="p-4 border rounded" style="padding: 0px !important; margin: 10px 0px 0px 0px;">
                                <div class="row g-3 needs-validation" style="padding: 0px 10px 10px 10px;">
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Choose Image</label>
                                        <asp:FileUpload ID="FileUpload1" runat="server" class="form-control find-dv-txtbx" />
                                    </div>
                                    <div class="col-12">
                                        <asp:Button ID="btn_Submit" runat="server" Text="Final Submit" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btn_Submit_Click" />
                                        <asp:Button ID="btn_cancel" runat="server" Text="Cancel" class="btn btn-dark" Visible="false" CausesValidation="false" OnClick="btn_cancel_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>



    <script>
        $(function () {
            $("#<%=txt_dob.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                readOnly: true,
                yearRange: "1900:2100",
            }).attr("readonly", "true");
        });
        $(function () {
            $("#<%=txt_joining_date.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                readOnly: true,
                yearRange: "1900:2100",
            }).attr("readonly", "true");
        });
        $(function () {
            $("#<%=txt_join_date.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                readOnly: true,
                yearRange: "1900:2100",
            }).attr("readonly", "true");
        });

        $(function () {
            $("#<%=txt_esic_leaving_date.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                readOnly: true,
                yearRange: "1900:2100",
            }).attr("readonly", "true");
        });
        $(function () {
            $("#<%=txt_emp_join_date.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                readOnly: true,
                yearRange: "1900:2100",
            }).attr("readonly", "true");
        });
        $(function () {
            $("#<%=txt_pf_leaving_date.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                readOnly: true,
                yearRange: "1900:2100",
            }).attr("readonly", "true");
        });
    </script>
</asp:Content>
