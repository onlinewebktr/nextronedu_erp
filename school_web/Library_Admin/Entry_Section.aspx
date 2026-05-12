<%@ Page Title="" Language="C#" MasterPageFile="~/Library_Admin/Library_Master.Master" AutoEventWireup="true" CodeBehind="Entry_Section.aspx.cs" Inherits="school_web.Library_Admin.Entry_Section" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Set Library card Format
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

    <script language="Javascript">

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 46 && charCode > 31
                && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }

    </script>
    <style>
        .head {
            display: none;
        }

        #pageFooter {
            display: none;
        }

        input[type=checkbox], input[type=radio] {
            background: #000;
            border-style: none;
            width: 30px;
            height: 30px;
            position: relative;
            top: 8.6px;
            left: 0px;
            margin: 0px 10px 0px 0px;
            z-index: 9999;
        }
    </style>
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
                <div class="breadcrumb-title pe-3">Master</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Lib_Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Set Library card Format</li>
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
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation" novalidate="">

                                    <div style="margin: 0px; float: left; height: auto;">
                                    </div>
                                    <div class="col-md-6">
                                        <label for="validationCustom01" class="form-label">
                                            Library Card No.Format<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="TextBox1"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="TextBox2"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="TextBox3"></asp:RequiredFieldValidator>
                                            </sup>
                                        </label>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-4">

                                            <div class="col-sm-6">
                                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="Student">Student</asp:ListItem>
                                                    <asp:ListItem Value="Teacher">Teacher</asp:ListItem>
                                                    <asp:ListItem Value="Staff">Staff</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-4">
                                                <label for="validationCustom01" class="form-label">Prefix</label>
                                                <asp:TextBox ID="TextBox1" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-4">
                                                <label for="validationCustom01" class="form-label">Serial No.</label>
                                                <asp:TextBox ID="TextBox2" runat="server" class="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-4">
                                                <label for="validationCustom01" class="form-label">Postfix</label>
                                                <asp:TextBox ID="TextBox3" runat="server" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>

                                        <br />
                                        <br />

                                        <div class="col-12" style="margin-top:20px;">
                                            <asp:Button ID="btn_Submit" runat="server" Text="Add" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btn_Submit_Click" />
                                            <asp:Button ID="btn_cancel" runat="server" Text="Cancel" class="btn btn-dark" Visible="false" CausesValidation="false" OnClick="btn_cancel_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-xl-8">
                <h6 class="mb-0 text-uppercase"></h6>
                <hr />
                <div class="card">
                    <div class="card-body">
                        <div class="table-responsive">
                            <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                <div class="row">
                                    <div class="row" style="display:none">
                                        <div class="col-sm-2">


                                            <asp:Button ID="Btnprint" runat="server" Text="Print" CssClass="btn btn-primary find-dv-btn " Visible="false" />

                                        </div>
                                        <div class=" col-sm-2">
                                            <asp:Button ID="Btnexcel" runat="server" Text="Excel" CssClass="btn btn-primary find-dv-btn" Visible="false" />
                                        </div>
                                    </div>
                                    
                                    <asp:Panel ID="Panel2" runat="server">
                                        <div class="pgslry-head-div head">

                                            <div style="margin: 0px; padding: 0px; height: 110px; width: 20%; float: left;">
                                                <asp:Image ID="imglogo" runat="server" Style="height: 100px; width: 100px; margin: 0px 0px 0px 10px;" />
                                            </div>
                                            <div style="margin: 0px; padding: 0px; height: 110px; width: 80%; float: left;">
                                                <h1 style="margin: 0px 0px 0px 0px; width: 100%; padding: 0px; font-weight: bold; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 20px; text-decoration: underline;">
                                                    <asp:Label ID="lbl_heading" runat="server"></asp:Label>


                                                </h1>

                                                <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                    <asp:Label ID="lbl_address" runat="server"></asp:Label>


                                                </div>
                                                <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                    Email Id. :<asp:Label ID="lbl_emaiid" runat="server"></asp:Label>

                                                    &nbsp;&nbsp;

                            website :
                            <asp:Label ID="lbl_website" runat="server"></asp:Label>
                                                </div>
                                                <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                    Contact Details :<asp:Label ID="lbl_contact_details" runat="server"></asp:Label>


                                                </div>
                                            </div>


                                        </div>
                                        <div class="col-sm-12">
                                            <table id="example21" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                <thead>
                                                    <tr>
                                                        <th>#</th>
                                                        <th>Prefix</th>
                                                        <th>Serial No.</th>
                                                        <th>Postfix</th>
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
                                                                        <asp:Label ID="lbl_Location" runat="server" Text='<%#Bind("Prefix")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="Label2" runat="server" Text='<%#Bind("serialNo")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="Label1" runat="server" Text='<%#Bind("Postfix")%>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </asp:Panel>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>
                                        </div>
                                    </asp:Panel>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!--end row-->
        </div>
        <asp:HiddenField ID="hd_id" runat="server" />
</asp:Content>
