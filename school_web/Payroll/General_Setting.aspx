<%@ Page Title="" Language="C#" MasterPageFile="~/Payroll/Adminmaster.Master" AutoEventWireup="true" CodeBehind="General_Setting.aspx.cs" Inherits="school_web.Payroll.General_Setting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                <div class="breadcrumb-title pe-3">Admin</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">General Setting</li>
                        </ol>
                    </nav>
                </div>
            </div>


            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text=" "></asp:Label>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded" style="padding: 0px !important;">
                                <h2 class="form-ttleS">General Setting</h2>
                                <div class="row g-3 needs-validation" style="padding: 0px 10px 10px 10px;">
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Session<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_session" runat="server" class="form-select find-dv-txtbx">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-6">
                                        <label for="validationCustom01" class="form-label">Employee Code Formate<sup>*</sup></label>
                                        <div class="row">
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txt_emp_prefix" placeholder="Prefix" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txt_employecode" runat="server" placeholder="Employe Code" class="form-control find-dv-txtbx"></asp:TextBox>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txt_employe_postfix" runat="server" placeholder="Postfix" class="form-control find-dv-txtbx"></asp:TextBox>
                                            </div>
                                        </div>

                                    </div>

                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Working Sift</label>
                                        <asp:DropDownList ID="ddl_workingshift" runat="server" class="form-select find-dv-txtbx">
                                            <asp:ListItem>Single</asp:ListItem>
                                           
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Disconnect Device<sup>*</sup></label>

                                        <asp:RadioButton ID="rd_disconnect_device_No" runat="server"  Style="margin: 0px 10px 0px 0px;"  Text="No" GroupName="a1" />
                                        <asp:RadioButton ID="rd_disconnect_device_Yes" runat="server"  Text="Yes" GroupName="a1"/>


                                    </div>
                                    <div class="col-md-6">
                                        <label for="validationCustom01" class="form-label">Set minimum working hours to calculate present <sup>*</sup></label>
                                        <asp:TextBox ID="txt_workinghours" runat="server" onkeypress="return isNumberKey(event)" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>

                                     <div class="col-md-6">
                                        <label for="validationCustom01" class="form-label">Hour limit(1,2.....9) for Full day attendance calculation <sup>*</sup></label>
                                        <asp:TextBox ID="txt_limit_fullday" runat="server" onkeypress="return isNumberKey(event)" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>


                                     <div class="col-md-6">
                                        <label for="validationCustom01" class="form-label">Hour limit(1,2.....9) for Half day attendance calculation <sup>*</sup></label>
                                        <asp:TextBox ID="txt_limit_halfday" runat="server" onkeypress="return isNumberKey(event)" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>






                                    <div class="row" style="margin-top:20px">
                                        <div class="col-md-3">
                                            <label for="validationCustom01" class="form-label">HR Name</label>
                                            <asp:TextBox ID="txt_hrname" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3">
                                            <label for="validationCustom01" class="form-label">Position Name</label>
                                            <asp:TextBox ID="txt_positionname" runat="server" class="form-control find-dv-txtbx" MaxLength="10"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3">
                                            <label for="validationCustom01" class="form-label">LOI Serial Prefix</label>
                                            <asp:TextBox ID="txt_loiserialprifix" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                        </div>
                                          <div class="col-md-3">
                                            <label for="validationCustom01" class="form-label">LOI Serial Postfix</label>
                                            <asp:TextBox ID="txt_loiserialpostfix" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                        </div>
                                    </div>





                                </div>
                            </div>

                          


              


                            <div class="p-4 border rounded" style="padding: 0px !important; margin: 10px 0px 0px 0px;">
                                <div class="row g-3 needs-validation" style="padding: 0px 10px 10px 10px;">
                                  
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
</asp:Content>
