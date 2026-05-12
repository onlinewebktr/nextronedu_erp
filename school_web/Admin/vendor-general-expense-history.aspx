<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="vendor-general-expense-history.aspx.cs" Inherits="school_web.Admin.vendor_general_expense_history" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    General Expense History 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="../assets/css/Print.css" rel="stylesheet" type="text/css" />');
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
                <div class="breadcrumb-title pe-3">General Expense</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">General Expense History </li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">


                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase">General Expense History </h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="find-dv">
                                            <div class="row">
                                                <div class="col-sm-2" runat="server" id="typeDV">
                                                    <label for="validationCustom01" class="find-dv-lbl">Type</label>
                                                    <asp:DropDownList ID="ddl_type1" runat="server" class="form-control find-dv-txtbx">
                                                        <asp:ListItem>ALL</asp:ListItem>
                                                        <asp:ListItem>Pending</asp:ListItem>
                                                        <asp:ListItem>Approved</asp:ListItem>
                                                        <asp:ListItem>Reject</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-2">
                                                    <label for="validationCustom01" class="find-dv-lbl">Vendor Type</label>
                                                    <asp:DropDownList ID="ddl_vendor_type" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>
                                                </div>
                                                <div class="col-sm-2">
                                                    <label for="validationCustom01" class="find-dv-lbl">From Date</label>
                                                    <asp:TextBox ID="txt_s_date" runat="server" class="form-control find-dv-txtbx datepicker"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2">
                                                    <label for="validationCustom01" class="find-dv-lbl">To Date</label>
                                                    <asp:TextBox ID="txt_e_date" runat="server" class="form-control find-dv-txtbx datepicker"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-2">
                                                    <asp:Button ID="btn_find" runat="server" Text="find" class="btn btn-primary find-dv-btn" OnClick="btn_find_Click" />

                                                    <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" class="btn btn-primary find-dv-btn" Style="margin-left: 10px;" runat="server"
                                                        ToolTip="Print"><i class='bx bx-printer'></i> Print</asp:LinkButton>

                                                </div>
                                            </div>
                                        </div>

                                        <div id="tblPrintIQ" runat="server">
                                            <div class="head-printdv" style="border-bottom: 1px solid #000; margin: 0px; float: left; width: 100%;">
                                                <div style="margin: 0px; padding: 0px; height: 110px; width: 20%; float: left;">
                                                    <asp:Image ID="imglogo" runat="server" Style="height: 100px; width: 100px; margin: 0px 0px 0px 10px;" />
                                                </div>
                                                <div style="margin: 0px; padding: 0px; height: 110px; width: 80%; float: left;">
                                                    <h1 style="margin: 10px 0px 0px 0px; width: 100%; padding: 0px; font-weight: bold; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 20px; text-decoration: underline;">
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
                                                        <span style="font-size: 14px; font-weight: bold;">General Expense  for -
                                                                        <asp:Label ID="lbl_date" runat="server"></asp:Label></span>
                                                    </div>
                                                </div>
                                            </div>
                                            <table id="datatable" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                <thead>
                                                    <tr>
                                                        <th>#</th>
                                                        <th>Date</th>
                                                        <th>Financial Year</th>
                                                        <th>Slip No.</th>
                                                        <th>Vendor Name</th>

                                                        <th>Payment Amount</th>
                                                        <th>Payment Mode</th>
                                                        <th>Remarks</th>
                                                        <th>Upload By</th>
                                                        <th>Verification Status</th>
                                                        <th class="noPrint">Action</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="rd_view" runat="server" OnItemDataBound="rd_view_ItemDataBound">
                                                        <ItemTemplate>
                                                            <asp:Panel ID="Panel1" runat="server">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lbl_date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label2" runat="server" Text='<%#Bind("Session_name")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lbl_slip_no" runat="server" Text='<%#Bind("Slip_no")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lbl_Vendor_Name" runat="server" Text='<%#Bind("Vendor_Name")%>'></asp:Label>
                                                                    </td>

                                                                    <td>
                                                                        <asp:Label ID="lbl_payment_amount" runat="server" Text='<%#Bind("Payment_amount")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lbl_payment_mode" runat="server" Text='<%#Bind("Payment_mode")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lbl_remarks" runat="server" Style="word-break: break-all" Text='<%#Bind("Remarks")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lbl_upload_By" runat="server" Style="word-break: break-all" Text='<%#Bind("uploadby")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lbl_Expense_Approval_Status" runat="server" Style="word-break: break-all" Text='<%#Bind("Expense_Approval_Status")%>'></asp:Label>
                                                                    </td>



                                                                    <td  class="noPrint">
                                                                        <asp:LinkButton ID="lnk_verify" runat="server" CausesValidation="false" OnClick="lnk_verify_Click" ToolTip="Verify"> Verify </asp:LinkButton>
                                                                        <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Edit"> <i class="lni lni-pencil-alt"> </i></asp:LinkButton>
                                                                        <asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                                        <a href="slip/general-expense-slip.aspx?ExpnsId=<%#Eval("Slip_no") %>" style="font-size: 16PX; padding: 0px 5px 0px 0px; float: left;"
                                                                            target="_blank"><i class='bx bx-printer'></i></a>
                                                                        <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                        <asp:Label ID="lbl_vendor_id" runat="server" Text='<%#Bind("Vendor_id")%>' Visible="false"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </asp:Panel>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                                <tbody>
                                                    <tr>
                                                        <td colspan="5" style="text-align: right; font-weight: 700; font-size: 15px;">Total Amount</td>
                                                        <td colspan="6" style="font-weight: 700; font-size: 15px;">
                                                            <asp:Label ID="lbl_ttl_amount" runat="server"></asp:Label></td>
                                                    </tr>
                                                </tbody>
                                            </table>
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
    <div class="conf-alrt-sec" id="myModal2" runat="server" visible="false">
        <div class="conf-alrt-inr" style="width: 750px;">
            <div class="popupTablWpR">
                <div class="row">
                    <div class="col-md-6">
                        <h2 class="popup-dt-h">General Expenses Verification</h2>
                    </div>
                    <div class="col-md-6">
                        <ul class="conf-btn-ul" style="margin: 0px 0px 0px 0px;">
                            <li>
                                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Close</asp:LinkButton>
                            </li>
                        </ul>
                    </div>
                </div>


                <div style="width: 100%; max-height: 400px; overflow: auto;">


                    <table id="example21" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                        <thead>
                            <tr>
                                <th>#</th>
                                <th>Financial Year</th>
                                <th>Slip No.</th>
                                <th>Vendor Name</th>
                                <th>Date</th>
                                <th>Payment Amount</th>
                                <th>Payment Mode</th>
                                <th>Remarks</th>
                                <th>Upload By</th>

                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="Repeater1" runat="server">
                                <ItemTemplate>

                                    <tr>
                                        <td>
                                            <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:Label ID="Label2" runat="server" Text='<%#Bind("Session_name")%>'></asp:Label>
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:Label ID="lbl_slip_no" runat="server" Text='<%#Bind("Slip_no")%>'></asp:Label>
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:Label ID="lbl_Vendor_Name" runat="server" Text='<%#Bind("Vendor_Name")%>'></asp:Label>
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:Label ID="lbl_date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                        </td>



                                        <td style="text-align: left;">
                                            <asp:Label ID="lbl_payment_amount" runat="server" Text='<%#Bind("Payment_amount")%>'></asp:Label>
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:Label ID="lbl_payment_mode" runat="server" Text='<%#Bind("Payment_mode")%>'></asp:Label>
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:Label ID="lbl_remarks" runat="server" Style="word-break: break-all" Text='<%#Bind("Remarks")%>'></asp:Label>
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:Label ID="lbl_upload_By" runat="server" Style="word-break: break-all" Text='<%#Bind("uploadby")%>'></asp:Label>
                                        </td>
                                    </tr>

                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>

                    <div style="margin: 0px; padding: 0px; float: left; height: auto; width: 50%">
                        <table class="table table-striped table-bordered">

                            <tr>
                                <td>Select Type
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_type" runat="server" class="form-control find-dv-txtbx">
                                        <asp:ListItem>Select</asp:ListItem>
                                        <asp:ListItem>Approved</asp:ListItem>
                                        <asp:ListItem>Reject</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>Remarks
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_remarks" runat="server" class="form-control" TextMode="MultiLine" Style="width: 100%; height: 50px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="btn_conf_remove" Style="margin: 10px 10px 0px 0px;" OnClick="btn_conf_remove_Click" runat="server" Text="Submit" class="btn btn-danger find-dv-btn" /></td>
                            </tr>

                        </table>

                    </div>





                </div>

            </div>

        </div>
    </div>
    <style>
        .conf-btn-ul li a {
            margin: 0px 5px;
            padding: 0px 0px 1px;
            text-decoration: none;
            background: #ff0000;
            color: #fff;
            width: 50px;
            float: right;
            text-align: center;
            border-radius: 3px;
            font-size: 13px;
            line-height: 25px;
            font-weight: 500;
        }

        table tr th {
            padding: 10px 5px !important;
        }

        table tr td {
            padding: 10px 5px !important;
        }
    </style>
    <!--end page wrapper -->
</asp:Content>
