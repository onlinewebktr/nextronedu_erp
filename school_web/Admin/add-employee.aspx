<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="add-employee.aspx.cs" Inherits="school_web.Admin.add_employee" %>

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
                <div class="breadcrumb-title pe-3">User Permission</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Add/Update Users</li>
                        </ol>
                    </nav>
                </div>
            </div>


            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Add Users"></asp:Label>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded" style="padding: 0px !important;">
                                <h2 class="form-ttleS">Personal Details</h2>
                                <div class="row g-3 needs-validation" style="padding: 0px 10px 10px 10px;">
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Employee Type<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_emp_type" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                    </div>

                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Employee Name<sup>*</sup></label>
                                        <asp:TextBox ID="txt_emp_name" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>

                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Gender</label>
                                        <asp:DropDownList ID="ddl_gender" runat="server" class="form-select find-dv-txtbx">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>MALE</asp:ListItem>
                                            <asp:ListItem>FEMALE</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Date of Birth<sup> </sup></label>
                                        <div class="clndr-div">
                                            <asp:TextBox ID="txt_dob" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                            <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Blood Group <sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_blood_group" runat="server" class="form-select find-dv-txtbx">
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
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label" style="width: 100%;">Religion</label>
                                        <asp:DropDownList ID="ddl_religion" runat="server" class="form-select find-dv-txtbx">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>HINDU</asp:ListItem>
                                            <asp:ListItem>ISLAM</asp:ListItem>
                                            <asp:ListItem>SIKH</asp:ListItem>
                                            <asp:ListItem>CHRISTIAN</asp:ListItem>
                                            <asp:ListItem>BUDDHISM</asp:ListItem>
                                            <asp:ListItem>JAIN</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label" style="width: 100%;">Marital Status</label>
                                        <asp:DropDownList ID="ddl_marital_status" runat="server" class="form-select find-dv-txtbx">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>UNMARRIED</asp:ListItem>
                                            <asp:ListItem>MARRIED</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Email.<sup>*</sup></label>
                                        <asp:TextBox ID="txt_email" runat="server" type="email" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Mobile No.<sup>*</sup></label>
                                        <asp:TextBox ID="txt_mobile" runat="server" class="form-control find-dv-txtbx" MaxLength="10" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    </div>

                                    <div class="col-md-6">
                                        <label for="validationCustom01" class="form-label">Address<sup> </sup></label>
                                        <asp:TextBox ID="txt_address" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>


                                </div>
                            </div>

                            <%-- ========== --%>
                            <div class="p-4 border rounded" style="padding: 0px !important; margin: 10px 0px 0px 0px;">
                                <h2 class="form-ttleS">User Id and Password</h2>
                                <div class="row g-3 needs-validation" style="padding: 0px 10px 10px 10px;">
                                    <asp:CheckBox ID="chk_auto_generate_emp_code" runat="server" OnCheckedChanged="chk_auto_generate_emp_code_CheckedChanged" AutoPostBack="true" Text="If you want to auto create Employee Code" HorizontalAlignment="Left" Visible="false" />
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Employee Code </label>
                                        <asp:TextBox ID="txt_emp_code" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Password<sup>*</sup></label>
                                        <asp:TextBox ID="txt_pwd" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                </div>
                            </div>





                            <div class="p-4 border rounded" style="padding: 0px !important; margin: 10px 0px 0px 0px;">
                                <div class="row g-3 needs-validation" style="padding: 0px 10px 10px 10px;">
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Choose Image</label>
                                        <asp:FileUpload ID="FileUpload1" runat="server" class="form-control find-dv-txtbx" />
                                    </div>

                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Choose Signature</label>
                                        <asp:FileUpload ID="FileUpload3" runat="server" class="form-control find-dv-txtbx" />
                                        <asp:Label ID="lbl_signature_path" Visible="false" runat="server"></asp:Label>
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
      





    </script>
</asp:Content>
