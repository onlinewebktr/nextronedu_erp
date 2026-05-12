<%@ Page Title="" Language="C#" MasterPageFile="~/Inventory_management/Site3.Master" AutoEventWireup="true" CodeBehind="Asset_Reminder_List.aspx.cs" Inherits="school_web.Inventory_management.Reminder_Asset_List" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Asset Reminder List
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="https://fonts.googleapis.com/css2?family=Dosis:wght@200;300;400;500;600;700;800&family=Montserrat:ital,wght@0,300;0,400;0,500;0,600;0,700;0,800;0,900;1,200;1,300;1,400;1,500;1,600;1,700;1,800;1,900&family=Roboto:ital,wght@0,100;0,300;0,400;0,500;0,700;0,900;1,100;1,300;1,400;1,500;1,700;1,900&display=swap" rel="stylesheet"/><link href="css/Print.css" rel="stylesheet" type="text/css" /><link href="../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />');
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
        .head {
            display: none;
        }

        @media print {
            .noPrint {
                display: none;
            }

            #Header, #Footer {
                display: none !important;
            }
        }

        .modal.fade .modal-dialog {
            transition: transform .3s ease-out;
            transform: translate(0, 0px);
        }

        .modal {
            background: #00000082;
        }

        /*.table.table-bordered.dataTable, table.table-bordered.dataTable tbody td {*/
        /*  border: 0px solid #000!important;*/

        /*border-bottom: 1px solid #000 !important;
        }*/

        .table1 tbody td {
            border-bottom: 1px solid #000 !important;
        }

        .noborder td {
            border: 0px solid #000 !important;
        }

        .noborder tbody td {
            border: 0px solid #000 !important;
        }
    </style>
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

        <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3" style="margin-bottom: 0px !important;">
            <div class="breadcrumb-title pe-3">Asset</div>
            <div class="ps-3">
                <nav aria-label="breadcrumb">
                    <ol class="breadcrumb mb-0 p-0">
                        <li class="breadcrumb-item"><a href="Dashboard.aspx"><i class="bx bx-home-alt"></i></a>
                        </li>
                        <li class="breadcrumb-item active" aria-current="page">Asset Reminder List  </li>

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

                        <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                            <div class="row">

                                <br />

                                <div class="col-sm-12">
                                    <div style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%">
                                        <div style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%">
                                            <%--   <asp:LinkButton ID="btn_excels" runat="server" OnClick="btn_excels_Click" class="btn btn_excel find-dv-btn">  <i class='bx bx-download'></i> Excel</asp:LinkButton>
                                                <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" Style="margin: 0px 0px 6px 10px; padding: 5px 0px 5px 5px;" CssClass="btn btn-primary find-dv-btn" runat="server"
                                                    ToolTip="Print"><i class='bx bx-printer'></i></asp:LinkButton>--%>

                                            <asp:LinkButton ID="lnk_excel_download" runat="server" OnClick="lnk_excel_download_Click" class="btn-print noPrint" Style="float: right; font-size: 21px;"><i class="bx bx-export"> </i></asp:LinkButton>
                                            <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" runat="server" ToolTip="Print" class="btn-print noPrint" Style="float: right; font-size: 21px;"><i class="bx bx-printer" aria-hidden="true"></i></asp:LinkButton>
                                        </div>


                                    </div>
                                    <div id="tblPrintIQ" runat="server">

                                        <div class="pgslry-head-div" style="width: 100%;">
                                            <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: left; vertical-align: middle; font-size: 13px; width: 100%;" class="head">
                                                <h6 class="mb-0 text-uppercase" style="font-size: 17px; margin: 10px 0px 10px 0px;">Asset Reminder List </h6>

                                            </div>

                                            <div class="row" id="SearchData">

                                                <br />
                                                <asp:Panel ID="pnl_grid" runat="server" Style="width: 100%;">

                                                    <table style="width: 100%;" id="example" class="table table-bordered table1">
                                                        <thead>
                                                            <tr>
                                                                <th>#</th>
                                                                <th>Name</th>
                                                                <th>Purchase Date</th>
                                                                <th>Supplier</th>
                                                                <th>Invoice No.</th>
                                                                <th>Attribute Name</th>
                                                                <th>Attribute No.</th>
                                                                <th>Attribute Valid Date</th>
                                                                <th>Attribute Reminder Date</th>
                                                                <th>Status</th>
                                                                <th class="noPrint">Attchment</th>
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
                                                                            <asp:Label ID="lbl_Item_Name" runat="server" Text='<%#Bind("Item_Name")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_Purchase_date" runat="server" Text='<%#Bind("Purchase_date")%>'></asp:Label>
                                                                        </td>


                                                                        <td>
                                                                            <asp:Label ID="lbl_party_name" runat="server" Text='<%#Bind("party_name")%>'></asp:Label>
                                                                            <asp:Label ID="lblparty_id" runat="server" Text='<%#Bind("party_id")%>' Visible="false"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_invoice_no" runat="server" Text='<%#Bind("invoice_no")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_Attribute_Name" runat="server" Text='<%#Bind("Attribute_Name")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_Attribute_no" runat="server" Text='<%#Bind("Attribute_no")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_Attribute_valid_to_date" runat="server" Text='<%#Bind("Attribute_valid_to_date")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">

                                                                            <asp:Label ID="lbl_Attribute_entry_id" runat="server" Visible="false" Text='<%#Bind("Attribute_entry_id")%>'></asp:Label>
                                                                            <asp:Label ID="lbl_Attribute_Reminder_Date" runat="server" Text='<%#Bind("Attribute_Reminder_Date")%>'></asp:Label>
                                                                            <asp:Label ID="lbl_Attribute_valid_to_idate" Visible="false" runat="server" Text='<%#Bind("Attribute_valid_to_idate")%>'></asp:Label>
                                                                        </td>


                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_status" runat="server"></asp:Label>
                                                                        </td>

                                                                        <td style="border: 0px; padding: 0px; text-align: center" class="noPrint">
                                                                            <asp:DataList ID="DataList1" runat="server" RepeatDirection="Horizontal" Style="border: 0px #ffffff00;" CssClass="noborder">
                                                                                <ItemTemplate>

                                                                                    <table>
                                                                                        <tr>
                                                                                            <td style="border-bottom: 0px solid #000 !important;">
                                                                                                <a id="lnk"
                                                                                                    href='<%# Eval("Attachment_path") %>'
                                                                                                    download
                                                                                                    runat="server"
                                                                                                    style="padding: 0px 9px 0px 9px; text-decoration: none; font-size: 21px;"
                                                                                                    target="_blank"><i class='bx bx-cloud-download'></i></a>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>






                                                                                </ItemTemplate>
                                                                            </asp:DataList>
                                                                        </td>


                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </tbody>
                                                    </table>
                                                    <div style="display: none; overflow: hidden">
                                                        <asp:GridView ID="GrdViewlist" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%" OnRowDataBound="GrdViewlist_RowDataBound">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="#">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Item_Name" runat="server" Text='<%#Bind("Item_Name")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Purchase Date">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Purchase_date" runat="server" Text='<%#Bind("Purchase_date")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Supplier">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_party_name" runat="server" Text='<%#Bind("party_name")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Invoice No.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_invoice_no" runat="server" Text='<%#Bind("invoice_no")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                  <asp:TemplateField HeaderText=">Attribute Name">
                                                                    <ItemTemplate>
                                                                          <asp:Label ID="lbl_Attribute_Name" runat="server" Text='<%#Bind("Attribute_Name")%>'></asp:Label>
                                                                         
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                  <asp:TemplateField HeaderText="Attribute No.">
                                                                    <ItemTemplate>
                                                                         <asp:Label ID="lbl_Attribute_no" runat="server" Text='<%#Bind("Attribute_no")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderText="Attribute Valid Date">
                                                                    <ItemTemplate>
                                                                           <asp:Label ID="lbl_Attribute_valid_to_date" runat="server" Text='<%#Bind("Attribute_valid_to_date")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                  <asp:TemplateField HeaderText="Attribute Reminder Date">
                                                                    <ItemTemplate>
                                                                             <asp:Label ID="lbl_Attribute_entry_id" runat="server" Visible="false" Text='<%#Bind("Attribute_entry_id")%>'></asp:Label>
                                                                            <asp:Label ID="lbl_Attribute_Reminder_Date" runat="server" Text='<%#Bind("Attribute_Reminder_Date")%>'></asp:Label>
                                                                            <asp:Label ID="lbl_Attribute_valid_to_idate" Visible="false" runat="server" Text='<%#Bind("Attribute_valid_to_idate")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                  <asp:TemplateField HeaderText="Status">
                                                                    <ItemTemplate>
                                                                      <asp:Label ID="lbl_status" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                               




                                                            </Columns>
                                                        </asp:GridView>
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
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
