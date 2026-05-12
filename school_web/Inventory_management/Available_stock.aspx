<%@ Page Title="" Language="C#" MasterPageFile="~/Inventory_management/Site3.Master" AutoEventWireup="true" CodeBehind="Available_stock.aspx.cs" Inherits="school_web.Inventory_management.Available_stock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Available Stock
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .select2-container--default .select2-selection--single {
            background-color: #fff;
            border: 0px solid #aaa;
            border-radius: 4px;
        }

        .select2-container--default .select2-selection--single {
            background-color: #fff;
            border: 0px solid #aaa;
            border-radius: 0px;
        }

        .select2-container .select2-selection--single {
            box-sizing: border-box;
            cursor: pointer;
            display: block;
            height: 25px;
            user-select: none;
            -webkit-user-select: none;
        }

        .select2-selection__rendered {
            display: block;
            width: 100%;
            padding: .375rem .75rem;
            font-size: 1rem;
            font-weight: 400;
            line-height: 1.5;
            color: #212529;
            background-color: #fff;
            background-clip: padding-box;
            border: 1px solid #ced4da;
            -webkit-appearance: none;
            -moz-appearance: none;
            appearance: none;
            border-radius: .25rem;
            transition: border-color .15s ease-in-out, box-shadow .15s ease-in-out
        }

        .select2-container--default .select2-selection--single .select2-selection__rendered {
            color: #000;
            line-height: 25px;
            font-size: 16px !important;
            font-weight: normal;
        }

        label {
            display: inline-block;
            font-weight: bold;
        }

        @media print {
            .noPrint {
                display: none;
            }

            #Header, #Footer {
                display: none !important;
            }
        }

        .printheading {
            display: none;
        }
    </style>
    <script type="text/javascript">
        function PrintPanel(secid) {
            var panel = document.getElementById(secid);
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write(' <style  type="text/css" media="print">  td,th {border: 1px solid #000; padding: 0px 0px 0px 5px!important;} </style><link href="Slip/print.css" rel="stylesheet" type="text/css" /><link href="https://fonts.googleapis.com/css?family=Dosis:200,300,400,500,600,700,800&display=swap" rel="stylesheet"/><link rel="stylesheet" href="//maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css" />');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body ></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }

    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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
            <div class="breadcrumb-title pe-3">Report</div>
            <div class="ps-3">
                <nav aria-label="breadcrumb">
                    <ol class="breadcrumb mb-0 p-0">
                        <li class="breadcrumb-item"><a href="Dashboard.aspx"><i class="bx bx-home-alt"></i></a>
                        </li>
                        <li class="breadcrumb-item active" aria-current="page">Available Stock</li>
                    </ol>
                </nav>
            </div>
        </div>



        <asp:HiddenField ID="HdID" runat="server" />
        <div class="row">
            <%--<div class="col-xl-6" style="margin: 0px auto;">
                <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Generate PO"></asp:Label>
                <hr />
                <div class="card">
                    <div class="card-body">
                        <div class="p-4 border rounded">
                            <div class="row g-3 needs-validation" novalidate="">
                                <div class="col-md-5">
                                    <label for="validationCustom01" class="form-label">From Date <sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_from_Date"></asp:RequiredFieldValidator></sup></label>
                                    <asp:TextBox ID="txt_from_Date" runat="server" class="form-control find-dv-txtbx datepicker"></asp:TextBox>
                                </div>
                                <div class="col-md-5">
                                    <label for="validationCustom01" class="form-label">To Date <sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator5" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_to_Date"></asp:RequiredFieldValidator></sup></label>
                                    <asp:TextBox ID="txt_to_Date" runat="server" class="form-control find-dv-txtbx datepicker"></asp:TextBox>
                                </div>
                                <div class="col-md-2" style="padding: 26px 0px 0px 0px;">
                                    <asp:Button ID="Btn_Find" runat="server" Text="Find" OnClick="Btn_Find_Click" CssClass="btn btn-primary" ValidationGroup="a" />

                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>--%>
            <div class="col-xl-12">
                <h6 class="mb-0 text-uppercase">Available Stock

                      <asp:LinkButton ID="lnk_excel_download" runat="server" OnClick="lnk_excel_download_Click" class="btn-print noPrint" Style="float: right"><i class="bx bx-export"> </i></asp:LinkButton>
                    <asp:LinkButton ID="print1" OnClientClick="return PrintPanel('tblPrintIQ')" runat="server" ToolTip="Print" class="btn-print noPrint" Style="float: right"><i class="bx bx-printer" aria-hidden="true"></i></asp:LinkButton>

                </h6>
                <hr />
                <div class="card">
                    <div class="card-body">
                        <div class="table-responsive">

                            <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                <div class="row">

                                    <br />

                                    <div class="col-sm-12">
                                        <table style="width: auto; margin: 0px auto;" class=" table table-hover table-striped table-bordered ">
                                            <tr>
                                                <td><b>Filter By Item Type</b></td>
                                                <td>
                                                    <asp:DropDownList ID="ddl_itemtype" runat="server" CssClass="form-control">
                                                        <asp:ListItem>Consumable</asp:ListItem>
                                                        <asp:ListItem>Assets</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btn_filter" runat="server" Text="Filter" CssClass="btn-info" OnClick="btn_filter_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                        <div id="tblPrintIQ">
                                            <asp:Panel ID="pnl_grid" runat="server" Style="width: 100%;">

                                                <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;" class="printheading">
                                                    <span style="font-size: 14px; font-weight: bold;">Available Stock List<asp:Label ID="lbl_class22" runat="server"></asp:Label></span>
                                                </div>

                                                <table style="width: 100%;" id="example" class=" table table-hover table-striped table-bordered">
                                                    <thead>
                                                        <tr>
                                                            <th>#</th>
                                                            <th>Item Name</th>
                                                            <th>Brand</th>
                                                            <th>Quantity</th>
                                                            <th>Unit</th>
                                                            <th>Total Product Value</th>
                                                            <th>Sale Rate</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="RPDetails" runat="server">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lbl_Item_Name" runat="server" Text='<%#Bind("Item_Name")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lbl_Brand_name" runat="server" Text='<%#Bind("Brand_name")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lbl_Quantity" runat="server" Text='<%#Bind("Quantity")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lbl_unit" runat="server" Text='<%#Bind("unit")%>'></asp:Label>
                                                                    </td>


                                                                    <td>
                                                                        <asp:Label ID="lbl_Purchase_Rate" runat="server" Text='<%#Bind("Purchase_Rate")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label1" runat="server" Text='<%#Bind("Sale_rate_single")%>'></asp:Label>
                                                                    </td>
                                                                    <%--<td>
                                                                <asp:Label ID="lbl_date" runat="server" Text='<%#Bind("date")%>'></asp:Label>
                                                            </td>--%>
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


</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
