<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="stock-entry.aspx.cs" Inherits="school_web.Admin.stock_entry" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Stock Entry
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            $("#<%=txt_item_code.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'stock-entry.aspx/GetRooPath',
                        data: "{ 'PathRooT': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            if (data.d.length > 0) {
                                response($.map(data.d, function (item) {
                                    return {
                                        label: item
                                    };
                                }))
                            } else {
                                response([{ label: 'No results found.' }]);
                            }
                        }
                    });
                },
                select: function (e, u) {
                    if (u.item.val == -1) {
                        return false;
                    }
                }
            });
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
        .head {
            display: none;
        }

        #pageFooter {
            display: none;
        }
    </style>
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
                <div class="breadcrumb-title pe-3">Inventory</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active">Stock Master</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Add Stock"></asp:Label>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation">
                                    <div class="col-md-2">
                                        <label for="validationCustom01" class="form-label">Date<sup>*</sup></label>
                                        <asp:TextBox ID="txt_date" runat="server" class="form-control"></asp:TextBox>
                                        <script>
                                            $(function () {
                                                $("#<%=txt_date.ClientID %>").datepicker({
                                                    dateFormat: "dd/mm/yy",
                                                    changeMonth: true,
                                                    changeYear: true,
                                                    yearRange: "1900:2100",
                                                }).attr("readonly", "true");
                                            });
                                        </script>
                                    </div>

                                    <div class="col-md-2">
                                        <label for="validationCustom01" class="form-label">Store in<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_store_in" runat="server" class="form-select">
                                            <asp:ListItem>Central Stock</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Find Item<sup>*</sup></label>
                                        <div class="row">
                                            <div class="col-xl-8">
                                                <asp:TextBox ID="txt_item_code" runat="server" class="form-control" placeholder="Item Code"></asp:TextBox>
                                            </div>
                                            <div class="col-xl-4" style="padding-left: 0px">
                                                <asp:Button ID="btn_find" runat="server" Text="Find" CssClass="btn btn-primary" Style="padding: 8px 10px 6px 10px" OnClick="btn_find_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div style="float: left; width: 100%"></div>
                                <div class="row g-3 needs-validation">
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Item Name<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_items" runat="server" class="form-select" OnSelectedIndexChanged="ddl_items_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Brand Name<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_brand_name" runat="server" class="form-select"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <label for="validationCustom01" class="form-label">Material Type<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_material_type" runat="server" class="form-select"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <label for="validationCustom01" class="form-label">Serial No.<sup>*</sup></label>
                                        <asp:TextBox ID="txt_serial_no" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <label for="validationCustom01" class="form-label">Value(RS.)<sup>*</sup></label>
                                        <asp:TextBox ID="txt_value" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <label for="validationCustom01" class="form-label">Quantity<sup>*</sup></label>
                                        <asp:TextBox ID="txt_qnt" runat="server" class="form-control" Text="1"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <label for="validationCustom01" class="form-label">Unit<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_unit" runat="server" class="form-select"></asp:DropDownList>
                                    </div>

                                    <div class="col-md-2">
                                        <label for="validationCustom01" class="form-label">Is Warranty Available<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_is_waranty_avalS" runat="server" class="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddl_is_waranty_aval_SelectedIndexChanged">
                                            <asp:ListItem>No</asp:ListItem>
                                            <asp:ListItem>Yes</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>


                                    <div class="col-md-2" runat="server" visible="false" id="wrntyDV">
                                        <label for="validationCustom01" class="form-label">Warranty End Date<sup>*</sup></label>
                                        <asp:TextBox ID="txt_warnty_end_Date" runat="server" class="form-control"></asp:TextBox>
                                        <script>
                                            $(function () {
                                                $("#<%=txt_warnty_end_Date.ClientID %>").datepicker({
                                                    dateFormat: "dd/mm/yy",
                                                    changeMonth: true,
                                                    changeYear: true,
                                                    yearRange: "1900:2100",
                                                }).attr("readonly", "true");
                                            });
                                        </script>
                                    </div>


                                    <div class="col-md-2">
                                        <label for="validationCustom01" class="form-label">Working Status<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_working_status" runat="server" class="form-select">
                                            <asp:ListItem>Working</asp:ListItem>
                                            <asp:ListItem>Not Working</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-4" runat="server" id="remrkSDV">
                                        <label for="validationCustom01" class="form-label">Remarks<sup>*</sup></label>
                                        <asp:TextBox ID="txt_remarks" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-12">
                                        <asp:Button ID="btn_Submit" runat="server" Text="Add" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btn_Submit_Click1" />
                                        <asp:Button ID="btn_cancel" runat="server" Text="Cancel" class="btn btn-dark" Visible="false" CausesValidation="false" OnClick="btn_cancel_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:HiddenField ID="hd_id" runat="server" />

                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase">Added Stock</h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <asp:LinkButton ID="btn_excels" runat="server" OnClick="btn_excels_Click" Style="margin-left: 10px;" class="btn btn-primary find-dv-btn">  <i class='bx bx-download'></i> Excel</asp:LinkButton>
                                            <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" CssClass="btn btn-primary find-dv-btn" Style="margin-left: 10px;" runat="server"
                                                ToolTip="Print"><i class='bx bx-printer'></i></asp:LinkButton>
                                        </div>
                                        <div class="col-sm-12">
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
                                                                    <span style="font-size: 14px; font-weight: bold;">Stock Details 
                                                                        <asp:Label ID="lbl_class22" runat="server"></asp:Label></span>


                                                                </div>
                                                            </div>


                                                        </div>

                                                        <asp:Panel ID="Panel1" runat="server">
                                                            <table id="example21" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                                <thead>
                                                                    <tr>
                                                                        <th>#</th>
                                                                        <th>Date</th>
                                                                        <th>Item Code</th>
                                                                        <th>Item Name</th>
                                                                        <th>Brand</th>
                                                                        <th>Material</th>
                                                                        <th>Serial no.</th>
                                                                        <th>Value</th>
                                                                        <th>Unit</th>
                                                                        <th>Stock In</th>
                                                                        <th>Stock Out</th>
                                                                        <th>Remarks</th>
                                                                        <th>Action</th>
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
                                                                                        <asp:Label ID="lbl_date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_item_ids" runat="server" Text='<%#Bind("Item_id")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_item_name" runat="server" Text='<%#Bind("Item_name")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="Label1" runat="server" Text='<%#Bind("Brand_name")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="Label2" runat="server" Text='<%#Bind("Material_name")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_serial_no" runat="server" Text='<%#Bind("Serial_no")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="llb_value_rs" runat="server" Text='<%#Bind("Value_rs")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_unit_name" runat="server" Text='<%#Bind("Unit_name")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_stock_qnt" runat="server" Text='<%#Bind("Stock_qnt")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_transfer_qnt" runat="server" Text='<%#Bind("Transfer_qnts")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_remarks" runat="server" Text='<%#Bind("Remarks")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                       


                                                                                        <div class="user-box dropdown" style="float: left; display: inherit; height: auto; border-left: 0px solid #f0f0f0; margin-left: 0px;">
                                                                                            <a class="d-flex align-items-center nav-link dropdown-toggle dropdown-toggle-nocaret" style="font-size: 29px; padding: 0px; border: 0px;"
                                                                                                href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                                                                                                <div class="user-info ps-3" style="padding: 0px !important; border: 0px !important;">
                                                                                                    <i class="bx bx-grid-horizontal"></i>
                                                                                                </div>
                                                                                            </a>
                                                                                            <ul class="dropdown-menu dropdown-menu-end">
                                                                                                <li>
                                                                                                    <asp:LinkButton ID="lnkEdit" class="dropdown-item" runat="server" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Edit"> <i class="lni lni-pencil-alt"> </i> <span>Edit</span></asp:LinkButton>
                                                                                                </li>

                                                                                                <%--<li>
                                                                                                    <a href="#!" class="dropdown-item" target="_blank"><i class='bx bx-printer'></i><span>Print Slip</span></a>
                                                                                                </li>--%>
                                                                                            </ul>
                                                                                        </div>

                                                                                        <%--<asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"> </i></asp:LinkButton>--%>
                                                                                        <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                                        <asp:Label ID="lbl_unit_id" runat="server" Text='<%#Bind("Unit_id")%>' Visible="false"></asp:Label>
                                                                                        <asp:Label ID="lbl_item_id" runat="server" Text='<%#Bind("Item_id")%>' Visible="false"></asp:Label>

                                                                                        <asp:Label ID="lbl_entry_type" runat="server" Text='<%#Bind("Entry_type")%>' Visible="false"></asp:Label>
                                                                                        <asp:Label ID="lbl_is_warranty_available" runat="server" Text='<%#Bind("Is_warranty_available")%>' Visible="false"></asp:Label>
                                                                                        <asp:Label ID="lbl_working_status" runat="server" Text='<%#Bind("Working_status")%>' Visible="false"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                            </asp:Panel>
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
        <!--end row-->
    </div>

    <!--end page wrapper -->
</asp:Content>
