<%@ Page Title="" Language="C#" MasterPageFile="~/Inventory_management/Site3.Master" AutoEventWireup="true" CodeBehind="Purchase_Tax_Summary.aspx.cs" Inherits="school_web.Inventory_management.Purchase_Tax_Summary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
   Purchase Tax Summary
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .select2-container--default .select2-selection--single {
            background-color: #fff;
            border: 0px solid #aaa;
            border-radius: 4px;
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
            transition: border-color .15s ease-in-out, box-shadow .15s ease-in-out;
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
    </style>
    <script type="text/javascript">
        function PrintPanel(secid) {

            var panel = document.getElementById(secid);
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write(' <style  type="text/css" media="print">  td,th { padding: 2px 2px 2px 2px!important; font-size: 12px !important;} </style><link href="../MasterAdmin/Slip/print.css" rel="stylesheet" type="text/css" /><link href="https://fonts.googleapis.com/css?family=Dosis:200,300,400,500,600,700,800&display=swap" rel="stylesheet"/><link rel="stylesheet" href="//maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css" />');
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
                        <li class="breadcrumb-item active" aria-current="page">Purchase Tax Summary</li>
                    </ol>
                </nav>
            </div>
        </div>

        <div class="row">
            <div class="col-xl-6">
                <hr />
                <div class="card">
                    <div class="card-body">
                        <div class="p-4 border rounded serchbox">
                            <div class="row g-3 needs-validation" novalidate="">


                                <div class="col-md-3">
                                    <label for="validationCustom01" class="form-label">From Date <sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_from_Date"></asp:RequiredFieldValidator></sup></label>
                                    <asp:TextBox ID="txt_from_Date" runat="server" class="form-control find-dv-txtbx datepicker"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <label for="validationCustom01" class="form-label">To Date <sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator5" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_to_Date"></asp:RequiredFieldValidator></sup></label>
                                    <asp:TextBox ID="txt_to_Date" runat="server" class="form-control find-dv-txtbx datepicker"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <label for="validationCustom01" class="form-label">GST%<sup></sup></label>

                                    <asp:DropDownList ID="ddl_GST" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>

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
                <h6 class="mb-0 text-uppercase">Purchase Tax Summary
                       <asp:LinkButton ID="lnk_excel_download" runat="server" OnClick="lnk_excel_download_Click" class="btn-print noPrint" Style="float: right"><i class="bx bx-export"> </i></asp:LinkButton>
                    <asp:LinkButton ID="print1" OnClientClick="return PrintPanel('tblPrintIQ')" runat="server" ToolTip="Print" class="btn-print noPrint" Style="float: right"><i class="bx bx-printer" aria-hidden="true"></i></asp:LinkButton>

                </h6>
                <hr />
                <div class="card">
                    <div class="card-body">
                        <div class="table-responsive">
                            <asp:Panel ID="pnl_grid" runat="server" Style="width: 100%;">
                                <table id="example21" border="1" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                    <thead>

                                        <tr>
                                            <th colspan="14" style="margin: 0px 0px 5px 0px; padding: 0px; text-align: center; line-height: 16px; border-bottom: 1px solid #000">
                                                <p style="margin: 0px; padding: 0px; width: 100%; text-align: center; padding: 5px 0px 0px 0px;">
                                                    <asp:Label ID="lbl_hospital_name" runat="server" Font-Bold="true" Style="font-size: 17px;"></asp:Label>
                                                </p>
                                                <asp:Label ID="lbl_address1" runat="server" Font-Bold="true" Style="margin: 0px 0px 5px 0px; width: 100%; float: left; padding: 0px; text-align: center; line-height: 13px;"></asp:Label>


                                                <asp:Label ID="lbl_address2" runat="server" Font-Bold="true" Style="margin: 0px 0px 5px 0px; width: 100%; float: left; padding: 0px; text-align: center; line-height: 15px;"></asp:Label>

                                                <p style="font-size: 15px; text-align: center; width: 100%; float: left">
                                                    <asp:Label ID="lbl_duration" runat="server" Font-Bold="true"></asp:Label>
                                                </p>
                                            </th>
                                        </tr>
                                          <tr>
                                    <th style="width: 50px!important;">#</th>

                                    <th style="width: 200px!important;">Date</th>
                                    <th style="width: 200px!important;">Invoice No</th>
                                    <th style="width: 400px!important;">Party Name</th>
                                    
                                    <th style="width: 400px!important;">Item Name</th>
                                    <th style="width: 200px!important;">Rate</th>
                                    <th style="width: 100px!important;">Quantity</th>
                                    <th style="width: 150px!important;">Taxable</th>
                                    <th style="width: 150px!important;">GST %</th>
                                    <th style="width: 150px!important;">IGST</th>
                                    <th style="width: 150px!important;">CGST</th>
                                    <th style="width: 150px!important;">SGST</th>
                                   
                                    <th style="width: 200px!important;">Total</th>

                                </tr>
                                    </thead>
                                    <tbody>

                                        <asp:Repeater ID="RPDetails" runat="server" OnItemDataBound="RPDetails_ItemDataBound">
                                                 <ItemTemplate>

                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                </td>

                                                <td>
                                                    <asp:Label ID="lbl_date" runat="server" Text='<%#Bind("date")%>'></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lbl_inv_no" runat="server" Text='<%#Bind("invoice_no")%>'></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lbl_party_Name" runat="server" Text='<%#Bind("party_name")%>'></asp:Label></td>
                                                 
                                                <td>
                                                    <asp:Label ID="lbl_item_name" runat="server" Text='<%#Bind("Item_Code")%>'></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lbl_rate" runat="server" Text='<%#Bind("Rate")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_qty" runat="server" Text='<%#Bind("quantity")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_taxable" runat="server" Text='<%#Bind("Taxable")%>'></asp:Label>
                                                  
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_gstper" runat="server" Text='<%#Bind("gst_percent")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_igst" runat="server" Text='<%#Bind("IGST")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_cgst" runat="server" Text='<%#Bind("CGST")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_sgst" runat="server" Text='<%#Bind("SGST")%>'></asp:Label>
                                                </td>
                                               
                                               
                                                <td>
                                                    <asp:Label ID="lbl_total" runat="server" Text='<%#Bind("NetTotal")%>'></asp:Label>
                                                </td>
                                               

                                            </tr>
                                        </ItemTemplate>
                                            <FooterTemplate>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                             
                                            <td style="border-top: 2px solid #000!important; font-size: 12px;"><b>Total:-</b></td>
                                            <td style="border-top: 2px solid #000!important; font-size: 12px;">
                                                <asp:Label ID="lbl_total_taxable" runat="server" Font-Bold="true"></asp:Label>
                                            </td>
                                                 <td></td>
                                            <td style="border-top: 2px solid #000!important; font-size: 12px;">
                                                <asp:Label ID="lbl_total_igst" runat="server" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td style="border-top: 2px solid #000!important; font-size: 12px;">
                                                <asp:Label ID="lbl_total_cgst" runat="server" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td style="border-top: 2px solid #000!important; font-size: 12px; ">
                                                <asp:Label ID="lbl_total_sgst" runat="server" Font-Bold="true"></asp:Label>
                                            </td>
                                           
                                            <td style="border-top: 2px solid #000!important; font-size: 12px; display:none ">
                                                <asp:Label ID="lbl_total_cess" runat="server" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td style="border-top: 2px solid #000!important; font-size: 12px;">
                                                <asp:Label ID="lbl_net_total" runat="server" Font-Bold="true"></asp:Label>
                                            </td>
                                        </FooterTemplate>
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
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
