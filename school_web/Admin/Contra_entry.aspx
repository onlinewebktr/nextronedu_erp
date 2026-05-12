<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Contra_entry.aspx.cs" Inherits="school_web.Admin.Contra_entry" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Contra Entry
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .Accounts {
            background-color: #d92550;
        }

        .Payment_Voucher {
            color: red !important;
        }

        tbody, td, tfoot, th, thead, tr {
            border-color: inherit;
            border-style: solid;
            border-width: 0;
            vertical-align: middle;
            font-size: 13px;
            padding: 4px;
            background: #312f7f00 !important;
        }

        .select2-container {
            display: block;
            width: 100% !important;
        }

        tfoot, th, thead {
            color: #000000;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#Contra_entry").addClass("dcjq-parent active");
        });
    </script>
    <style>
        .maste-p {
            width: 88%;
            float: left;
            padding: 0px 0px 0px 8px;
            margin: 0px;
            font-size: 14px;
            line-height: 20px;
        }

        .chck-box {
            float: left;
        }

        .all-check {
            padding: 0px 0px 0px 10px;
            width: 50% !important;
            float: left;
        }

        .form-group {
            margin-bottom: 0rem;
        }
    </style>
    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 46 && charCode > 31
                && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }

        $(document).ready(function () {
            $("#<%=ddl_account_cr.ClientID%>").select2();

        });

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
                <div class="breadcrumb-title pe-3">Accounts</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Contra Entry</li>
                        </ol>
                    </nav>
                </div>
            </div>

            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-4">
                    <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text=" Add Contra Entry"></asp:Label>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <table style="width: 100%">

                                    <tr>
                                        <td>
                                            <div class="position-relative form-group">
                                                <label>Account(Cr): </label>
                                            </div>
                                        </td>
                                        <td>
                                            <div class="position-relative form-group">
                                                <asp:DropDownList ID="ddl_account_cr" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_account_cr_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="position-relative form-group">
                                                <label>Particular: </label>
                                            </div>
                                        </td>
                                        <td>
                                            <div class="position-relative form-group">
                                                <asp:DropDownList ID="ddl_particular" runat="server" CssClass="form-select find-dv-txtbx txtbx-ddl-style">
                                                </asp:DropDownList>


                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="position-relative form-group">
                                                <label>Amount:  </label>
                                            </div>
                                        </td>
                                        <td>
                                            <div class="position-relative form-group">
                                                <asp:TextBox ID="txt_amount" runat="server" CssClass="form-control" Text="0" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                            </div>
                                        </td>
                                    </tr>

                                    <asp:Panel ID="pnl_datetime" runat="server" Visible="false">
                                        <tr>
                                            <td>
                                                <div class="position-relative form-group">
                                                    <label>Date</label>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="position-relative form-group">
                                                    <asp:TextBox ID="txt_date" runat="server" CssClass="form-control datecalender" placeholder="dd/MM/yyyy"></asp:TextBox>

                                                </div>
                                            </td>

                                        </tr>
                                    </asp:Panel>
                                    <tr>
                                        <td>
                                            <div class="position-relative form-group">
                                                <label>Remark</label>
                                            </div>
                                        </td>
                                        <td>
                                            <div class="position-relative form-group">
                                                <asp:TextBox ID="txt_remarks" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </td>
                                    </tr>
                                </table>

                                <asp:Button ID="btn_Submit" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btn_Submit_Click" ValidationGroup="a" Style="margin-top: 12px;" />
                                <asp:Button ID="btn_update" runat="server" Text="Update" CssClass="btn btn-primary" Visible="false" OnClick="btn_update_Click" ValidationGroup="a" Style="margin-top: 12px;" />
                                <asp:Button ID="btn_cancel" runat="server" Text="Cancel" class="btn btn-dark" OnClick="btn_cancel_Click" CausesValidation="false" Style="margin-top: 12px; float: right;" />

                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-xl-8">
                    <h6 class="mb-0 text-uppercase">Payment History</h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">

                            <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                <div class="row">
                                    <div class="col-sm-12">

                                        <div class="find-dv">
                                            <div class="row">

                                                <table style="width: 700px; margin: 0px auto; background-color: beige;" class="noPrint">
                                                    <tr>
                                                        <td colspan="3">
                                                            <asp:RadioButton ID="rd_without_time" runat="server" GroupName="g1" AutoPostBack="true" OnCheckedChanged="rd_without_time_CheckedChanged" Checked="true" Text="Report Search Without Time" />

                                                            <asp:RadioButton ID="rd_with_time" runat="server" GroupName="g1" AutoPostBack="true" OnCheckedChanged="rd_with_time_CheckedChanged" Text="Report Search With Time" />
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div class="position-relative form-group">
                                                                <div class="col-lg-12" style="padding: 0px;">
                                                                    <label>From Date</label>
                                                                </div>
                                                                <div class="col-lg-6" style="padding: 0px;">
                                                                    <asp:TextBox ID="txt_fromdate" runat="server" CssClass="form-control datecalender" placeholder="dd/MM/yyyy"></asp:TextBox>
                                                                </div>
                                                                <div id="fromtime" runat="server" visible="false" class="col-lg-6" style="padding: 0px;">
                                                                    <asp:TextBox ID="txt_fromtime" data-provide="timepicker" CssClass="form-control timepicker" runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </td>
                                                        <td>
                                                            <div class="position-relative form-group">
                                                                <div class="col-lg-12" style="padding: 0px;">
                                                                    <label>To Date</label>
                                                                </div>
                                                                <div class="col-lg-6" style="padding: 0px;">
                                                                    <asp:TextBox ID="txt_to_date" runat="server" CssClass="form-control datecalender" placeholder="dd/MM/yyyy"></asp:TextBox>
                                                                </div>
                                                                <div id="totime" runat="server" visible="false" class="col-lg-6" style="padding: 0px;">
                                                                    <asp:TextBox ID="txt_to_time" data-provide="timepicker" CssClass="form-control timepicker" runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </td>
                                                        <td>
                                                            <div class="position-relative form-group">

                                                                <asp:Button ID="btn_search" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btn_search_Click" Style="margin: 20px 8px 0px 0px; float: left;" />
                                                                <asp:LinkButton ID="btn_excels" Visible="false" runat="server" Style="margin: 0px 0px 6px 0px;" OnClick="lnk_excel_download_Click" class="btn btn-primary find-dv-btn"><i class='bx bx-download'></i>Excel</asp:LinkButton>
                                                                <asp:LinkButton ID="print1" Visible="false" OnClientClick="return PrintPanel()" Style="margin: 0px 0px 6px 10px;" CssClass="btn btn-primary find-dv-btn" runat="server"
                                                                    ToolTip="Print">
                                                                <i class='bx bx-printer'></i>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
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
                                                                    <span style="font-size: 14px; font-weight: bold;">Receipt History
                                                                        <asp:Label ID="lbl_class22" runat="server"></asp:Label>
                                                                    </span>


                                                                </div>
                                                            </div>


                                                        </div>
                                                        <asp:Panel ID="pnl_grid" runat="server" Style="width: 100%;">
                                                            <asp:GridView ID="grd_bill_trac" runat="server" class="mb-0 table table-bordered" ShowFooter="true" OnRowDataBound="grd_bill_trac_RowDataBound" AutoGenerateColumns="False" Width="100%">
                                                                <Columns>

                                                                    <asp:TemplateField HeaderText="Date" HeaderStyle-ForeColor="Maroon">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                        </ItemTemplate>

                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Voucher No." HeaderStyle-ForeColor="Maroon">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_VoucherNo" runat="server" Text='<%# Eval("VoucherNo") %>'></asp:Label>
                                                                            <asp:Label ID="lbl_v1" runat="server" Text='<%# Eval("v1") %>' Visible="false"></asp:Label>

                                                                        </ItemTemplate>

                                                                    </asp:TemplateField>



                                                                    <asp:TemplateField HeaderText="Account(Cr)" HeaderStyle-ForeColor="Maroon">
                                                                        <ItemTemplate>

                                                                            <asp:Label ID="lbl_cr_account" runat="server" Text='<%#Eval("Account_CR") %>'></asp:Label>
                                                                        </ItemTemplate>

                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Account(Dr)" HeaderStyle-ForeColor="Maroon">
                                                                        <ItemTemplate>

                                                                            <asp:Label ID="lbl_dr_account" runat="server" Text='<%#Eval("Account_Dr") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lbl_total1" runat="server" Font-Bold="true">Sub Total</asp:Label>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Amount" HeaderStyle-ForeColor="Maroon">
                                                                        <ItemTemplate>

                                                                            <asp:Label ID="lbl_Amount" runat="server" Text='<%#Eval("Amount") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lbl_total" runat="server" Font-Bold="true"></asp:Label>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>


                                                                    <asp:TemplateField HeaderText="Remark" HeaderStyle-ForeColor="Maroon">
                                                                        <ItemTemplate>

                                                                            <asp:Label ID="lbl_Description" runat="server" Text='<%#Bind("Description")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Action" ControlStyle-CssClass="noPrint" FooterStyle-CssClass="noPrint" HeaderStyle-CssClass="noPrint" ItemStyle-CssClass="noPrint">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnk_edit" runat="server" OnClick="lnk_edit_Click"> <i class="lni lni-pencil-alt"></i></asp:LinkButton>
                                                                            <asp:Label ID="lbl_Unique_entry_id" runat="server" Text='<%#Bind("unique_entry_id")%>' Visible="false"></asp:Label>
                                                                            <asp:LinkButton ID="lnk_print" runat="server" CssClass="myButton" OnClick="lnk_print_Click"> <i class="fa fa-print"></i></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                </Columns>
                                                            </asp:GridView>
                                                        </asp:Panel>

                                                        <div style="height: 1px; overflow: hidden">
                                                            <asp:GridView ID="GridView1" runat="server" class="mb-0 table table-bordered" ShowFooter="true" OnRowDataBound="GridView1_RowDataBound" AutoGenerateColumns="False" Width="100%">
                                                                <Columns>

                                                                    <asp:TemplateField HeaderText="Date" HeaderStyle-ForeColor="Maroon">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                        </ItemTemplate>

                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Voucher No." HeaderStyle-ForeColor="Maroon">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_VoucherNo" runat="server" Text='<%# Eval("VoucherNo") %>'></asp:Label>
                                                                            <asp:Label ID="lbl_v1" runat="server" Text='<%# Eval("v1") %>' Visible="false"></asp:Label>

                                                                        </ItemTemplate>

                                                                    </asp:TemplateField>



                                                                    <asp:TemplateField HeaderText="Account(Cr)" HeaderStyle-ForeColor="Maroon">
                                                                        <ItemTemplate>

                                                                            <asp:Label ID="lbl_cr_account" runat="server" Text='<%#Eval("Account_CR") %>'></asp:Label>
                                                                        </ItemTemplate>

                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Account(Dr)" HeaderStyle-ForeColor="Maroon">
                                                                        <ItemTemplate>

                                                                            <asp:Label ID="lbl_dr_account" runat="server" Text='<%#Eval("Account_Dr") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lbl_total1" runat="server" Font-Bold="true">Sub Total</asp:Label>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Amount" HeaderStyle-ForeColor="Maroon">
                                                                        <ItemTemplate>

                                                                            <asp:Label ID="lbl_Amount" runat="server" Text='<%#Eval("Amount") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lbl_total" runat="server" Font-Bold="true"></asp:Label>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateField>


                                                                    <asp:TemplateField HeaderText="Remark" HeaderStyle-ForeColor="Maroon">
                                                                        <ItemTemplate>

                                                                            <asp:Label ID="lbl_Description" runat="server" Text='<%#Bind("Description")%>'></asp:Label>
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
                </div>
            </div>
        </div>
        <!--end row-->
    </div>
    <script>
        //On Page Load.
        $(document).ready(function () {
            SetDatePicker();

            var prm = Sys.WebForms.PageRequestManager.getInstance();
            if (prm != null) {
                prm.add_endRequest(function (sender, e) {
                    if (sender._postBackSettings.panelsToUpdate != null) {
                        SetDatePicker();

                    }
                });
            };
        });


        function SetDatePicker() {
            $('.datecalender').datepicker(
                {
                    dateFormat: "dd/M/yy",
                    changeMonth: true,
                    changeYear: true,
                    yearRange: "1900:2100",
                    maxDate: 0
                }).attr("readonly", "true");


        }


    </script>

</asp:Content>
