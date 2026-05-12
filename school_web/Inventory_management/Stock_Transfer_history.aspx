<%@ Page Title="" Language="C#" MasterPageFile="~/Inventory_management/Site3.Master" AutoEventWireup="true" CodeBehind="Stock_Transfer_history.aspx.cs" Inherits="school_web.Inventory_management.Stock_Transfer_history" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Stock Transfer History
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="../MasterAdmin/css/popup.css" rel="stylesheet" />
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

            .header_image {
                display: block !important;
            }

            #Header, #Footer {
                display: none !important;
            }
        }
    </style>
    <script type="text/javascript">
        function PrintPanel(secid) {
            var panel = document.getElementById(secid);
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="Slip/print.css" rel="stylesheet" type="text/css" /><link href="https://fonts.googleapis.com/css?family=Dosis:200,300,400,500,600,700,800&display=swap" rel="stylesheet"/><link rel="stylesheet" href="//maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css" /> <style> td,th{ padding: 3px 3px 3px 3px; font-size:10px; border:1px solid #000;} span { font-family: Verdana; font-size: 10px!important; font-weight: normal; } </style> ');

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
    <div class='popup1'>
        <div class='content1' style="top: 10px; min-width: 600px; height: auto; width: 850px;">

            <table style="width: 100%;">

                <tr>
                    <td>

                        <div style="width: 100%; margin-top: 0px">
                            <fieldset class="fieldset" style="background-color: White">
                                <legend class="legendLogin" align="left" dir="ltr">
                                    <p class="divbg"><i class="pe-7s-config icon-gradient bg-mean-fruit"></i></p>
                                    <span style="line-height: 32px;">Item Details</span>
                                    <img src="../images/remove.png" alt='quit' class='close1' id='Img1' style="float: right; width: 25px;" />
                                </legend>
                                <table>
                                    <tr>
                                        <td>Store Name:-
                                            <asp:Label ID="lbl_storename" runat="server"></asp:Label>
                                        </td>
                                        <td>Transfer No:-
                                            <asp:Label ID="lbl_demand_no" runat="server"></asp:Label>
                                        </td>
                                        <td>Date:-
                                            <asp:Label ID="lbl_demand_date" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:GridView ID="grd_view" runat="server" class="data-Table table table-bordered" AutoGenerateColumns="False" Width="100%">

                                                <Columns>
                                                    <asp:TemplateField HeaderText="#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Item_name" runat="server" Text='<%#Bind("Item_Name")%>'></asp:Label>
                                                            <asp:Label ID="lbl_Item_Code" runat="server" Text='<%#Bind("Item_Code")%>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lbl_Brand_Id" runat="server" Text='<%#Bind("Brand_Id")%>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lbl_Unit_id" runat="server" Text='<%#Bind("Unit_id")%>' Visible="false"></asp:Label>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Brand">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_brand_name" runat="server" Text='<%#Bind("Brand_name")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Unit_name" runat="server" Text='<%#Bind("unit")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="HSN">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Hsn_no" runat="server" Text='<%#Bind("hsn_no")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Purchase Rate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Purchase_Rate" runat="server" Text='<%#Bind("Purchase_Rate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_qty" runat="server" Text='<%#Bind("Quantity")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Rate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Total_amount" runat="server" Text='<%#Bind("Total_amount")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Gst Amt.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Gts_amount" runat="server" Text='<%#Bind("Gts_amount")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Final Amt.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_final_amount" runat="server" Text='<%#Bind("Final_amount")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                            <asp:LinkButton ID="lnk_edit" runat="server" ToolTip="Edit" CausesValidation="false" OnClick="lnk_edit_Click"><i class="lni lni-pencil"> </i></asp:LinkButton>
                                                            <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                </Columns>

                                            </asp:GridView>

                                        </td>
                                    </tr>

                                </table>


                            </fieldset>
                        </div>

                    </td>
                </tr>

            </table>
        </div>
    </div>
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
            <div class="breadcrumb-title pe-3">Stock Transfer</div>
            <div class="ps-3">
                <nav aria-label="breadcrumb">
                    <ol class="breadcrumb mb-0 p-0">
                        <li class="breadcrumb-item"><a href="Dashboard.aspx"><i class="bx bx-home-alt"></i></a>
                        </li>
                        <li class="breadcrumb-item active" aria-current="page">Stock Transfer History</li>
                    </ol>
                </nav>
            </div>
        </div>



        <asp:HiddenField ID="HdID" runat="server" />
        <div class="row">
            <div class="col-xl-6" style="margin: 0px auto;">
                <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Search Transfer History
"></asp:Label>
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
            </div>
            <div class="col-xl-12" id="tblPrintIQ">
                <h6 class="mb-0 text-uppercase">Stock Transfer History 

                     <asp:LinkButton ID="lnk_excel_download" runat="server" OnClick="lnk_excel_download_Click" class="btn-print noPrint" Style="float: right"><i class="bx bx-export"> </i></asp:LinkButton>
                    <asp:LinkButton ID="print1" OnClientClick="return PrintPanel('tblPrintIQ')" runat="server" ToolTip="Print" class="btn-print noPrint" Style="float: right"><i class="bx bx-printer" aria-hidden="true"></i></asp:LinkButton>


                </h6>
                <hr />
                <div class="card">
                    <div class="card-body">
                        <div class="table-responsive">
                            <asp:Panel ID="pnl_grid" runat="server" Style="width: 100%;">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                    <div class="row">

                                        <br />

                                        <div class="col-sm-12">


                                            <table style="width: 100%;" id="example" class=" table table-hover table-striped table-bordered">
                                                <thead>
                                                    <tr>
                                                        <th>#</th>
                                                        <th>Transfer From</th>
                                                        <th>Transefer To</th>
                                                        <th>Date</th>
                                                        <th>Demand/Transfer No.</th>
                                                        <th>Total Qty</th>
                                                        <th>Net Amount</th>
                                                        <th>Items</th>
                                                        <th>Action</th>

                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="RPDetails" runat="server" OnItemDataBound="RPDetails_ItemDataBound">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbl_Store_name" runat="server" Text='<%#Bind("Transfer_from")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbl_Sector" runat="server" Text='<%#Bind("Transfer_to")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbl_date" runat="server" Text='<%#Bind("date")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbl_Demand_id" runat="server" Text='<%#Bind("Demand_id")%>'></asp:Label>
                                                                    <asp:Label ID="lbl_unique_entry_id" runat="server" Text='<%#Bind("unique_entry_id")%>' Visible="false"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbl_Qty" runat="server" Text='<%#Bind("_tot_qty")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbl_tot_purchase_rate" runat="server" Text='<%#Bind("tot_purchase_rate")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:GridView ID="grd_view" runat="server" class="data-Table table table-bordered" AutoGenerateColumns="False" Width="100%">

                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="#">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Item Name">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbl_Item_name" runat="server" Text='<%#Bind("Item_Name")%>'></asp:Label>
                                                                                    <asp:Label ID="lbl_Item_Code" runat="server" Text='<%#Bind("Item_Code")%>' Visible="false"></asp:Label>
                                                                                    <asp:Label ID="lbl_Brand_Id" runat="server" Text='<%#Bind("Brand_Id")%>' Visible="false"></asp:Label>
                                                                                    <asp:Label ID="lbl_Unit_id" runat="server" Text='<%#Bind("Unit_id")%>' Visible="false"></asp:Label>

                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Brand">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbl_brand_name" runat="server" Text='<%#Bind("Brand_name")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Unit">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbl_Unit_name" runat="server" Text='<%#Bind("unit")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="HSN">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbl_Hsn_no" runat="server" Text='<%#Bind("hsn_no")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Purchase Rate">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbl_Purchase_Rate" runat="server" Text='<%#Bind("Purchase_Rate")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Qty">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbl_qty" runat="server" Text='<%#Bind("Quantity")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Total Rate">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbl_Total_amount" runat="server" Text='<%#Bind("Total_amount")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Gst Amt.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbl_Gts_amount" runat="server" Text='<%#Bind("Gts_amount")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Final Amt.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbl_final_amount" runat="server" Text='<%#Bind("Final_amount")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                        </Columns>

                                                                    </asp:GridView>
                                                                </td>
                                                                <td>
                                                                    <asp:LinkButton ID="lnk_view" runat="server" ToolTip="View" OnClick="lnk_view_Click"><i class="lni lni-eye"> </i></asp:LinkButton>
                                                                    <asp:LinkButton ID="lnk_print" runat="server" ToolTip="Print" OnClick="lnk_print_Click"><i class="lni lni-printer"> </i></asp:LinkButton>
                                                                    <asp:LinkButton ID="lbk_edit" runat="server" ToolTip="Modify" OnClick="lbk_edit_Click" Visible="false"><i class="lni lni-pencil"> </i></asp:LinkButton>
                                                                </td>

                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>
                                        </div>

                                    </div>
                                </div>
                            </asp:Panel>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:HiddenField ID="hd_view" runat="server" Value="0" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
    <script type='text/javascript'>
        $(function () {

            var hd_view = document.getElementById('<%= hd_view.ClientID %>').value;
            if (hd_view != '0') {
                var overlay = $('<div id="overlay1"></div>');
                overlay.show();
                overlay.appendTo(document.body);
                $('.popup1').show();
            }
            $('.close1').click(function () {
                $('.popup1').hide();
                overlay.appendTo(document.body).remove();
                return false;
            });

        });
    </script>
</asp:Content>
