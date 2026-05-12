<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Add_ledger_details.aspx.cs" Inherits="school_web.Admin.Add_ledger_details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Add Ledger Details
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script>
        $(function () {
            $("#<%=txt_date.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                readOnly: true,
                yearRange: "1900:2100",
            }).attr("readonly", "true");
        }); 
    </script>

    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="https://fonts.googleapis.com/css2?family=Dosis:wght@200;300;400;500;600;700;800&family=Montserrat:ital,wght@0,300;0,400;0,500;0,600;0,700;0,800;0,900;1,200;1,300;1,400;1,500;1,600;1,700;1,800;1,900&family=Roboto:ital,wght@0,100;0,300;0,400;0,500;0,700;0,900;1,100;1,300;1,400;1,500;1,700;1,900&display=swap" rel="stylesheet"/><link href="../assets/css/Print.css" rel="stylesheet" type="text/css" /><link href="../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body ></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
        jQuery(".sn-bill-head-text").fitText(0.38);
    </script>
    <style>
        .clndr-icon {
            font-size: 14px !important;
            color: #ff2956;
            position: absolute;
            top: 7px;
            right: 3px;
        }
    </style>
    <script>
        $(document).ready(function () {
            // var table = new DataTable('#example2');

            //$('#example2').on('click', function () {
            //    table.destroy();
            //});

            // $('#example2').DataTable().destroy();
            if ($.fn.DataTable.isDataTable('#example2')) {
                $('#example2').DataTable().clear().destroy();
            }

            var table = $('#example2').DataTable({
                retrieve: true,
                lengthChange: false,
                paging: true,
                buttons: ['copy', 'excel', 'pdf', 'print']
            });

            table.buttons().container()
                .appendTo('#example2_wrapper .col-md-6:eq(0)');
        });
    </script>

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
                <div class="breadcrumb-title pe-3">Accounts</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Add Ledger Details</li>
                        </ol>
                    </nav>
                </div>
            </div>

            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-4">
                    <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text=" Add Ledger Details"></asp:Label>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation" novalidate="">
                                    <div class="col-md-12">
                                        <label for="validationCustom01" class="form-label">Account Name<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_account_name"></asp:RequiredFieldValidator></sup></label>
                                        <asp:TextBox ID="txt_account_name" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                    </div>
                                    <div class="col-md-12">
                                        <label for="validationCustom01" class="form-label">Group Name<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_group_name" runat="server" CssClass="form-select find-dv-txtbx txtbx-ddl-style" AutoPostBack="true" OnSelectedIndexChanged="ddl_group_name_SelectedIndexChanged"></asp:DropDownList>
                                    </div>


                                    <asp:Panel ID="sundry_details" runat="server" Visible="false">
                                        <div class="col-md-12">
                                            <table class="table table-bordered">
                                                <tr>
                                                    <td>
                                                        <div class="position-relative form-group">
                                                            <label>Address<sup>*</sup></label>
                                                            <asp:TextBox ID="txt_address" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txt_address" Display="Dynamic" ValidationGroup="a" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="position-relative form-group">
                                                            <label>City<sup>*</sup></label>
                                                            <asp:TextBox ID="txt_city" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txt_city" Display="Dynamic" ValidationGroup="a" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="position-relative form-group">
                                                            <label>Mobile<sup>*</sup></label>
                                                            <asp:TextBox ID="txt_mobile" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txt_mobile" Display="Dynamic" ValidationGroup="a" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="position-relative form-group">
                                                            <label>Registration Type<sup>*</sup></label>
                                                            <asp:DropDownList ID="ddl_registration_type" runat="server" CssClass="form-control">
                                                                <asp:ListItem>Select</asp:ListItem>
                                                                <asp:ListItem>Regular</asp:ListItem>
                                                                <asp:ListItem>Composition</asp:ListItem>
                                                                <asp:ListItem>UnRegistered</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="position-relative form-group">
                                                            <label>GSTIN<sup>*</sup></label>
                                                            <asp:TextBox ID="txt_gstin" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txt_gstin" Display="Dynamic" ValidationGroup="a" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="position-relative form-group">
                                                            <label>State<sup>*</sup></label>
                                                            <asp:DropDownList ID="ddl_state" runat="server" CssClass="form-control"></asp:DropDownList>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </asp:Panel>





                                    <asp:Panel ID="pbl_bank_acc" runat="server" Visible="false">
                                        <div class="col-md-12">
                                            <table class="table table-bordered">
                                                <tr>
                                                    <td>
                                                        <div class="position-relative form-group">
                                                            <label>Account Holder Name<sup>*</sup></label>
                                                            <asp:TextBox ID="txt_account_holder" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="txt_account_holder" Display="Dynamic" ValidationGroup="a" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="position-relative form-group">
                                                            <label>Account No<sup>*</sup></label>
                                                            <asp:TextBox ID="txt_account_no" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txt_account_no" Display="Dynamic" ValidationGroup="a" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>

                                                        </div>
                                                    </td>

                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="position-relative form-group">
                                                            <label>IFSC Code </label>
                                                            <asp:TextBox ID="txt_ifc_code" runat="server" CssClass="form-control"></asp:TextBox>

                                                        </div>
                                                    </td>

                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="position-relative form-group">
                                                            <label>Branch<sup>*</sup></label>
                                                            <asp:TextBox ID="txt_branch" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txt_branch" Display="Dynamic" ValidationGroup="a" runat="server" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>

                                                        </div>
                                                    </td>

                                                </tr>
                                            </table>
                                        </div>
                                    </asp:Panel>



                                    <div class="col-md-6">
                                        <asp:TextBox ID="txt_opening_balance" runat="server" placeholder="Opening Balance" CssClass="form-control" Style="width: 68%; float: left;"></asp:TextBox>

                                        <asp:DropDownList ID="ddl_dr_cr" Style="width: 32%; float: left;"
                                            runat="server" CssClass="form-select find-dv-txtbx txtbx-ddl-style">
                                            <asp:ListItem>Dr</asp:ListItem>
                                            <asp:ListItem>Cr</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2" style="width: 38px;">On</div>
                                    <div class="col-md-4">

                                        <div class="clndr-div">
                                            <asp:TextBox ID="txt_date" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                            <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                        </div>
                                    </div>

                                    <div class="col-md-4" style="display: none;">
                                        <label>Account Id<sup>*</sup></label>
                                        <asp:TextBox ID="txt_account_id" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4" style="display: none;">
                                        <label>Alias<sup>*</sup></label>
                                        <asp:TextBox ID="txt_alias" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>



                                    <div class="col-12">
                                        <asp:Button ID="btn_Submit" runat="server" Text="Save" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btn_Submit_Click1" />
                                        <asp:Button ID="btn_cancel" runat="server" Text="Cancel" class="btn btn-dark" CausesValidation="false" OnClick="btn_cancel_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-xl-8">
                    <h6 class="mb-0 text-uppercase">View Added Ledger Details</h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                    <div class="row">
                                        <div class="col-sm-12">

                                            <div class="find-dv">
                                                <div class="row">
                                                    <div class="col-sm-4">
                                                        <asp:LinkButton ID="btn_excels" runat="server" Style="margin: 0px 0px 6px 0px;" OnClick="btn_excels_Click" class="btn btn-primary find-dv-btn"><i class='bx bx-download'></i>Excel</asp:LinkButton>
                                                        <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" Style="margin: 0px 0px 6px 10px;" CssClass="btn btn-primary find-dv-btn" runat="server"
                                                            ToolTip="Print">
                                                                <i class='bx bx-printer'></i>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="grd-wpr">
                                                <div id="tblPrintIQ" runat="server">
                                                    <div class="prnt-dv-wpr">
                                                        <div id="content">
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
                                                                    <div style="display: none; margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                        Email Id. :<asp:Label ID="lbl_emaiid" runat="server"></asp:Label>

                                                                        &nbsp;&nbsp;

                            website :
                            <asp:Label ID="lbl_website" runat="server"></asp:Label>
                                                                    </div>
                                                                    <div style="display: none; margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                        Contact Details :<asp:Label ID="lbl_contact_details" runat="server"></asp:Label>


                                                                    </div>
                                                                    <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                        <span style="font-size: 14px; font-weight: bold;">Ledger Details
                                                                        <asp:Label ID="lbl_class22" runat="server"></asp:Label>
                                                                        </span>


                                                                    </div>
                                                                </div>


                                                            </div>
                                                            <asp:Panel ID="Panel1" runat="server">
                                                                <table id="datatable" data-page-length='50' class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                                    <thead>
                                                                        <tr>
                                                                            <th>#</th>
                                                                            <th>Account Name</th>
                                                                            <th>Group Name</th>
                                                                            <th>Opening Balance</th>
                                                                            <th>Type</th>
                                                                            <%if (this.IsChecked)
                                                                                { %>
                                                                            <th class="noPrint">Action</th>
                                                                            <%}%>
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
                                                                                        <asp:Label ID="lbl_Account_Name" runat="server" Text='<%#Bind("Account_Name")%>'></asp:Label>-
                                                                                        <asp:Label ID="lbl_Account_id" runat="server" Text='<%#Bind("Account_id")%>' ></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_Group_Name" runat="server" Text='<%#Bind("Group_Name")%>'></asp:Label>
                                                                                    </td>

                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_Amount" runat="server" Text='<%#Bind("Amount")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_Debit_Credit" runat="server" Text='<%#Bind("Debit_Credit")%>'></asp:Label>
                                                                                        <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                                        
                                                                                        <asp:Label ID="lbl_Group_id" runat="server" Text='<%#Bind("Group_id")%>' Visible="false"></asp:Label>
                                                                                    </td>
                                                                                    <%if (this.IsChecked)
                                                                                        { %>
                                                                                    <td style="text-align: left;" class="noPrint">
                                                                                        <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Edit"><i class="lni lni-pencil-alt"></i></asp:LinkButton>
                                                                                        <asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"></i></asp:LinkButton>

                                                                                    </td>
                                                                                    <%} %>
                                                                                </tr>

                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                    </tbody>
                                                                </table>
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
                </div>
            </div>
        </div>
        <!--end row-->
    </div>

    <style>
        .paging_simple_numbers {
            display: block !important;
        }
    </style>


    <!--end page wrapper -->
    <asp:HiddenField ID="hd_id" runat="server" />
</asp:Content>
