<%@ Page Title="" Language="C#" MasterPageFile="~/Inventory_management/Site3.Master" AutoEventWireup="true" CodeBehind="Asset_List.aspx.cs" Inherits="school_web.Inventory_management.Asset_List" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Asset List
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

        .table1 tbody td {
            border-bottom: 1px solid #000 !important;
        }

        .noborder td {
            border: 0px solid #000 !important;
        }

        .noborder tbody td {
            border: 0px solid #000 !important;
        }

        .addmore-btns {
            position: absolute;
            right: 0px;
            padding: 4px 5px 2px 4px;
            top: 11px;
            background: #2292f1;
            font-size: 10px;
            font-weight: 700;
            color: #000;
            border-radius: 3px;
            border: 1px solid #cfcfcf;
            line-height: 15px;
            color: #ffff !important;
        }
    </style>



</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content">
        <div id="notification" style="z-index: 999999999999999999;">
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
                        <li class="breadcrumb-item active" aria-current="page">Asset List  </li>

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
                                                <h6 class="mb-0 text-uppercase" style="font-size: 17px; margin: 10px 0px 10px 0px;">Asset List </h6>

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
                                                                <th class="noPrint">Action</th>

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
                                                                            <asp:Label ID="lbl_Purchase_date" runat="server" Text='<%#Bind("Purchase_date")%>'></asp:Label>
                                                                        </td>


                                                                        <td>
                                                                            <asp:Label ID="lbl_party_name" runat="server" Text='<%#Bind("party_name")%>'></asp:Label>
                                                                            <asp:Label ID="lblparty_id" runat="server" Text='<%#Bind("party_id")%>' Visible="false"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_invoice_no" runat="server" Text='<%#Bind("invoice_no")%>'></asp:Label>
                                                                        </td>
                                                                        <td class="noPrint">
                                                                            <div class="user-box dropdown" style="float: left; display: inherit; height: 0px; border-left: 0px solid #f0f0f0; margin-left: 0px;">
                                                                                <a class="d-flex align-items-center nav-link dropdown-toggle dropdown-toggle-nocaret" style="font-size: 29px; padding: 0px; border: 0px;"
                                                                                    href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                                                                                    <div class="user-info ps-3" style="padding: 0px !important; border: 0px !important; margin: -12px 0px 0px 0px;">
                                                                                        <i class="bx bx-grid-horizontal"></i>
                                                                                    </div>
                                                                                </a>
                                                                                <ul class="dropdown-menu dropdown-menu-end">
                                                                                    <li>
                                                                                        <asp:LinkButton ID="lnk_Update_Asset_Attribute" class="dropdown-item" runat="server" OnClick="lnk_Update_Asset_Attribute_Click"><i class='bx bx-edit'></i>Update Asset Attribute</asp:LinkButton>
                                                                                        <asp:Label ID="lbl_unique_entry_id" runat="server" Text='<%#Bind("unique_entry_id")%>' Visible="false"></asp:Label>

                                                                                    </li>
                                                                                    <li>
                                                                                        <asp:LinkButton ID="lnk_view_Attribute" class="dropdown-item" runat="server" OnClick="lnk_view_Attribute_Click"><i class='bx bx-receipt'></i>View Asset History</asp:LinkButton>
                                                                                    </li>


                                                                                    <li>
                                                                                        <a class="dropdown-item" href="Slip/assets-attributes.aspx?unique_entry_id=<%#Eval("unique_entry_id") %>" target="_blank"><i class='bx bx-printer'></i><span>Print Full Details</span></a>

                                                                                    </li>



                                                                                </ul>
                                                                            </div>
                                                                        </td>

                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </tbody>
                                                    </table>
                                                    <div style="display: none; overflow: hidden">
                                                        <asp:GridView ID="GrdViewlist" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%">
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

    <!-------popupadd Attribute----->
    <style>
        .clndr-icon {
            font-size: 14px !important;
            color: #ff2956;
            position: absolute;
            top: 17px;
            right: 19px;
        }

        .clndr-div {
            position: relative;
        }
    </style>
    <div id="myModal" class="modal fade" role="dialog" style="z-index: 9999;"  >
        <div class="modal-dialog" style="max-width: 914px;">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" style="width: 76%;">
                        <asp:Label ID="lbl_head" runat="server" Text="Update Asset Attribute"></asp:Label>
                    </h5>
                    <a data-toggle="modal" data-target="#myModal1" class="btn noPrint" style="float: right; color: #fff; background-color: #2292f1; border-color: #51585e; cursor: pointer; display:none">Add(+)</a>



                    <a href="#!" data-dismiss="modal" style="margin: 5px 0px 0px 0px !important; padding: 3px 5px 4px 5px; background: #fa2020; border: #9b0202;"
                        class="btn btn-secondary noPrint">Close</a>

                </div>
                <div class="modal-body" style="max-height: 400px; overflow: auto;">
                    <div class="row" id="tdprint">
                        <table style="width: 100%;" class=" table">
                            <tr>
                                <td style="position: relative">Select Attribute Name  :-     <a href="#!" data-toggle="modal" data-target="#myModal1" class="addmore-btns"><i class='bx bxs-add-to-queue' style="font-size: 17px;"></i></a><sup></sup>

                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_attributename" runat="server" CssClass="form-select"></asp:DropDownList>

                                <td>Attribute No. :-<sup>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                                        ControlToValidate="txt_attributeno" ValidationGroup="b">
                                    </asp:RequiredFieldValidator></sup>

                                </td>
                                <td>
                                    <asp:TextBox ID="txt_attributeno" CssClass="form-control" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Attribute Valid Date :-<sup>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"
                                        ControlToValidate="txt_Attribute_valid_date" ValidationGroup="b">
                                    </asp:RequiredFieldValidator></sup>

                                </td>
                                <td class="clndr-div">
                                    <asp:TextBox ID="txt_Attribute_valid_date" CssClass="form-control" runat="server"></asp:TextBox>
                                    <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                    <script>
                                        $(function () {
                                            $("#<%=txt_Attribute_valid_date.ClientID %>").datepicker({
                                                dateFormat: "dd/mm/yy",
                                                changeMonth: true,
                                                changeYear: true,
                                                yearRange: "2020:2030",

                                            }).attr("readonly", "true");
                                        });
                                    </script>
                                    <td>Attribute Reminder Date :-<sup>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*"
                                            ControlToValidate="txt_attributeReminder" ValidationGroup="b">
                                        </asp:RequiredFieldValidator></sup> :-
                                     
                                    </td>
                                <td class="clndr-div">
                                    <asp:TextBox ID="txt_attributeReminder" CssClass="form-control" runat="server"></asp:TextBox>
                                    <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                    <script>
                                        $(function () {
                                            $("#<%=txt_attributeReminder.ClientID %>").datepicker({
                                                dateFormat: "dd/mm/yy",
                                                changeMonth: true,
                                                changeYear: true,
                                                yearRange: "2020:2030",

                                            }).attr("readonly", "true");
                                        });
                                    </script>
                                </td>
                            </tr>
                            <tr>
                                <td>Attchment(Multi Selection) :-
                                     
                                </td>
                                <td>

                                    <asp:FileUpload ID="FileUpload1" Multiple="Multiple" runat="server" />
                                <td></td>
                                <td>
                                    <asp:Button ID="btn_add" runat="server" Text="Add" OnClick="btn_add_Click" CssClass="btn btn-primary" ValidationGroup="a" />
                                </td>
                            </tr>

                        </table>

                    </div>

                </div>
            </div>
        </div>
    </div>
     

    <div id="myModal1" class="modal fade" role="dialog" style="z-index: 9999;">
        <div class="modal-dialog" style="max-width: 517px; margin-top: 76px;">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" style="width: 76%;">Add Attribute</h5>


                    <a href="#!" data-dismiss="modal" style="margin: 5px 0px 0px 0px !important; padding: 3px 5px 4px 5px; background: #fa2020; border: #9b0202;"
                        class="btn btn-secondary noPrint">Close</a>
                </div>
                <div class="modal-body" style="max-height: 400px; overflow: auto;">
                    <div class="row" id="tdprint1">
                        <table style="width: 100%;" class=" table">
                            <tr>
                                <td>Enter Attribute Name <sup>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                        ControlToValidate="txt_attributename_add" ValidationGroup="bb">
                                    </asp:RequiredFieldValidator></sup>

                                </td>
                                <td>
                                    <asp:TextBox ID="txt_attributename_add" CssClass="form-control" runat="server"></asp:TextBox>


                                <td>
                                    <asp:Button ID="btn_attribute_name" runat="server" Text="Add" OnClick="btn_attribute_name_Click" CssClass="btn btn-primary" ValidationGroup="bb" />
                                </td>

                            </tr>

                            <tr>
                                <td colspan="3">

                                    <table id="example2" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info" data-page-length='1500'>
                                        <thead>
                                            <tr>
                                                <th>#</th>
                                                <th>Attribute Name</th>

                                                <th style="text-align: center">Action</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="rd_view_Attribute_master" runat="server">
                                                <ItemTemplate>

                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:Label ID="lbl_name" runat="server" Text='<%#Bind("Attribute_Name")%>'></asp:Label>
                                                        </td>


                                                        <td style="text-align: center; padding-right: 0px;">
                                                            <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Edit"> <i class="lni lni-pencil-alt"> </i></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                            <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lbl_Attribute_id" runat="server" Text='<%#Bind("Attribute_id")%>' Visible="false"></asp:Label>
                                                        </td>
                                                    </tr>

                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </td>

                            </tr>

                        </table>

                    </div>

                </div>
            </div>
        </div>
    </div>
    

    <div id="myModal2" class="modal fade" role="dialog">
        <div class="modal-dialog" style="max-width: 1253px; margin-top: 76px;">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" style="width: 76%;">Attribute History</h5>
                    <a href="#!" data-dismiss="modal" style="margin: 5px 0px 0px 0px !important; padding: 3px 5px 4px 5px; background: #fa2020; border: #9b0202;"
                        class="btn btn-secondary noPrint">Close</a>
                </div>
                <div class="modal-body" style="max-height: 400px; overflow: auto;">
                    <div class="row" id="tdprint21">

                        <table id="example21" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info" data-page-length='1500' style="margin: 0px !important;">
                            <thead>
                                <tr>
                                    <th>#</th>
                                    <th>Attribute Name</th>
                                    <th>Attribute No.</th>
                                    <th>Attribute Valid Date</th>
                                    <th>Attribute Reminder Date</th>
                                    <th>Status</th>
                                    <th>Attachment</th>
                                    <th></th>
                                    <th></th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="rep_Attribute_his" runat="server" OnItemDataBound="rep_Attribute_his_ItemDataBound">
                                    <ItemTemplate>

                                        <tr>
                                            <td>
                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:Label ID="lbl_name" runat="server" Text='<%#Bind("Attribute_Name")%>'></asp:Label>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:Label ID="lbl_Attribute_no" runat="server" Text='<%#Bind("Attribute_no")%>'></asp:Label>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:Label ID="lbl_Attribute_valid_to_date" runat="server" Text='<%#Bind("Attribute_valid_to_date")%>'></asp:Label>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:Label ID="lbl_Attribute_Reminder_Date" runat="server" Text='<%#Bind("Attribute_Reminder_Date")%>'></asp:Label>
                                                <asp:Label ID="lbl_Attribute_entry_id" Visible="false" runat="server" Text='<%#Bind("Attribute_entry_id")%>'></asp:Label>

                                                <asp:Label ID="lbl_Attribute_id" Visible="false" runat="server" Text='<%#Bind("Attribute_id")%>'></asp:Label>

                                                <asp:Label ID="lbl_Attribute_valid_to_idate" Visible="false" runat="server" Text='<%#Bind("Attribute_valid_to_idate")%>'></asp:Label>
                                                <asp:Label ID="lbl_unique_entry_id" Visible="false" runat="server" Text='<%#Bind("unique_entry_id")%>'></asp:Label>

                                            </td>

                                            <td style="text-align: left;">
                                                <asp:Label ID="lbl_status" runat="server"></asp:Label>
                                            </td>

                                            <td style="border: 0px; padding: 0px; text-align: center">
                                                <asp:DataList ID="DataList1" runat="server" RepeatDirection="Horizontal" Style="border: 0px #ffffff00;">
                                                    <ItemTemplate>



                                                        <a id="lnk"
                                                            href='<%# Eval("Attachment_path") %>'
                                                            download
                                                            runat="server"
                                                            style="padding: 0px 9px 0px 9px; text-decoration: none; font-size: 21px;"
                                                            target="_blank"><i class='bx bx-cloud-download'></i></a>




                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:Button ID="Btn_Add_Asset_attribute" runat="server" Text="Add" Style="padding: 4px 7px 3px 7px;" OnClick="Btn_Add_Asset_attribute_Click" CssClass="btn btn-primary" CausesValidation="false" />
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:Button ID="btn_Update_Asset_Aattribute" runat="server" Style="padding: 4px 7px 3px 7px;" Text="Update" OnClick="btn_Update_Asset_Aattribute_Click" CssClass="btn btn-primary" CausesValidation="false" />
                                            </td>

                                        </tr>

                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>


                    </div>

                </div>
            </div>
        </div>
    </div>
     




</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
    <script type="text/javascript">
        function openModal() {
            $('#myModal').modal('show');

        }
        function openModal1() {
            $('#myModal1').modal('show');

        }
        function openModal2() {
            $("#myModal2").modal('show');
        }
    </script>




</asp:Content>
