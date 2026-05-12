<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="view-stock.aspx.cs" Inherits="school_web.Admin.view_stock" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    View Stock
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            $("#<%=txt_item_name.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'view-stock.aspx/GetRooPath',
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



        //================
        $(function () {
            $("#<%=txt_item_code_fnd.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'view-stock.aspx/GetRooPathAdmNo',
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

    <style>
        .popup-dt-h {
            margin: 0px 0px 10px 0px;
            padding: 0px;
            width: 100%;
            height: auto;
            font-size: 18px;
        }

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


        .ppup-p {
            margin: 4px 0px 4px 0px;
            padding: 0px;
            width: 100%;
            float: left;
            font-size: 14px;
            font-weight: 300;
        }

            .ppup-p i {
                margin: 0px;
                padding: 0px;
                width: 125px;
                float: left;
                font-style: inherit;
            }

            .ppup-p span {
                margin: 0px;
                padding: 0px;
                width: auto;
                float: left;
            }

        .ppup-trnsfr-dv-wpr {
            margin: 0px;
            padding: 0px;
            width: 100%;
        }

        .ppup-trnsfr-dv {
            margin: 5px 0px 0px 0px;
            padding: 0px 0px 0px 0px;
            width: 100%;
            display: inline-block;
            border-top: 1px solid #666300;
        }

        .ppup-trnsfr-rwtxtbx-dv {
            margin: 5px 0px 0px 0px;
            padding: 0px;
            width: 100%;
        }

        .ppup-trnsfr-rwtxtbx-p {
            margin: 5px 0px 0px 0px;
            padding: 0px;
            width: 100%;
        }

        .ppup-trnsfr-rwtxtbx-txtbx {
            margin: 0px;
            padding: 2px 5px;
            width: 100%;
            border: 1px solid #bfbfbf;
            border-radius: 3px;
        }
    </style>
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
                    <h6 class="mb-0 text-uppercase">View Stock</h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="find-dv">
                                                <div class="row">
                                                    <div class="col-sm-3">
                                                        <label for="validationCustom01" class="find-dv-lbl">Search By Item Code</label>
                                                        <asp:TextBox ID="txt_item_code_fnd" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1">
                                                        <asp:Button ID="btn_find_by_item" runat="server" Text="find" class="btn btn-primary find-dv-btn" OnClick="btn_find_by_group_Click" />
                                                    </div>


                                                    <div class="col-sm-3">
                                                        <label for="validationCustom01" class="find-dv-lbl">Search By Item Name</label>
                                                        <asp:TextBox ID="txt_item_name" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1">
                                                        <asp:Button ID="btn_by_item_name" runat="server" Text="find" class="btn btn-primary find-dv-btn" OnClick="btn_by_item_name_Click" />
                                                    </div>

                                                    <div class="col-sm-3">
                                                        <asp:LinkButton ID="btn_excels" runat="server" OnClick="btn_excels_Click" Style="margin-left: 10px;" class="btn btn-primary find-dv-btn">  <i class='bx bx-download'></i> Excel</asp:LinkButton>
                                                        <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" CssClass="btn btn-primary find-dv-btn" Style="margin-left: 10px;" runat="server"
                                                            ToolTip="Print"><i class='bx bx-printer'></i></asp:LinkButton>
                                                    </div>


                                                </div>
                                            </div>

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
                                                                        <th>Item Code</th>
                                                                        <th>Item Name</th>
                                                                        <th>Brand</th>
                                                                        <th>Material</th>
                                                                        <th>Serial No.</th>
                                                                        <th>Working Status</th>
                                                                        <th>Value</th>
                                                                        <th>Is Warranty</th>
                                                                        <th>Expire On</th>
                                                                        <th>Unit</th>
                                                                        <th>Stock In</th>
                                                                        <th>Transfer Stock</th>
                                                                        <th>Available Stock</th>
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
                                                                                        <asp:Label ID="lbl_item_ids" runat="server" Text='<%#Bind("Item_id")%>'></asp:Label>
                                                                                    </td>

                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_item_name" runat="server" Text='<%#Bind("Item_name")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_brand_name" runat="server" Text='<%#Bind("Brand_name")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_material" runat="server" Text='<%#Bind("Material_name")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_serial_no" runat="server" Text='<%#Bind("Serial_no")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_working_status" runat="server" Text='<%#Bind("Working_status")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_value" runat="server" Text='<%#Bind("Value_rs")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_is_warranty" runat="server" Text='<%#Bind("Is_warranty_available")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_expire_date" runat="server" Text='<%#Bind("Warranty_end_date")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_unit_name" runat="server" Text='<%#Bind("Unit_name")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_stock_qnt" runat="server" Text='<%#Bind("Stock_qnt")%>'></asp:Label>
                                                                                    </td>

                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_transfer_qnt" runat="server" Text='<%#Bind("Transfer_qnt")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_available_qnt" runat="server" Text='<%#Bind("Available_qnt")%>'></asp:Label>
                                                                                    </td>

                                                                                    <td style="text-align: left;">
                                                                                        <asp:LinkButton ID="lnkTransfer" runat="server" Style="background: #0d6efd; padding: 1px 3px 1px 3px; color: #fff; border-radius: 2px;"
                                                                                            CausesValidation="false" OnClick="lnkTransfer_Click" ToolTip="Transfer"> Transfer</asp:LinkButton>

                                                                                        <asp:Label ID="lbl_unit_id" runat="server" Text='<%#Bind("Unit_id")%>' Visible="false"></asp:Label>
                                                                                        <asp:Label ID="lbl_item_id" runat="server" Text='<%#Bind("Item_id")%>' Visible="false"></asp:Label>
                                                                                        <asp:Label ID="lbl_trnsfr_qnt" Visible="false" runat="server" Text='<%#Bind("Transfer_qnt")%>'></asp:Label>
                                                                                        <asp:Label ID="lbl_stock_id" Visible="false" runat="server" Text='<%#Bind("Stock_id")%>'></asp:Label>
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



    <div class="conf-alrt-sec" id="myModal2" runat="server" visible="false">
        <div class="conf-alrt-inr" style="width: 500px;">
            <div class="popupTablWpR">
                <div style="border-bottom: 1px solid #333">
                    <div class="row">
                        <div class="col-md-6">
                            <h2 class="popup-dt-h">Transfer Inventory</h2>
                        </div>
                        <div class="col-md-6">
                            <ul class="conf-btn-ul" style="margin: 0px 0px 0px 0px;">
                                <li>
                                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Close</asp:LinkButton>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="ppup-trnsfr-dv-wpr">
                    <p class="ppup-p">
                        <i>Item Code : </i>
                        <asp:Label ID="lbl_itmcode_p" runat="server"></asp:Label>
                    </p>
                    <p class="ppup-p">
                        <i>Item Name : </i>
                        <asp:Label ID="lbl_item_name_p" runat="server"></asp:Label>
                    </p>
                    <div class="row">
                        <div class="col-md-6">
                            <p class="ppup-p">
                                <i>Brand Name : </i>
                                <asp:Label ID="lbl_brand_name_p" runat="server"></asp:Label>
                            </p>
                        </div>
                        <div class="col-md-6">
                            <p class="ppup-p">
                                <i>Material Type : </i>
                                <asp:Label ID="lbl_material_type_p" runat="server"></asp:Label>
                            </p>
                        </div>
                    </div>
                    <p class="ppup-p">
                        <i>Serial No. : </i>
                        <asp:Label ID="lbl_serial_no_p" runat="server"></asp:Label>
                    </p>
                    <p class="ppup-p">
                        <i>Available Qnt. : </i>
                        <asp:Label ID="lbl_aval_qnt_p" runat="server"></asp:Label>
                    </p>

                    <div class="ppup-trnsfr-dv">
                        <div class="row">
                            <div class="col-md-6">
                                <p class="ppup-trnsfr-rwtxtbx-p">Transfer Date</p>
                            </div>
                            <div class="col-md-6">
                                <div class="ppup-trnsfr-rwtxtbx-dv">
                                    <asp:TextBox ID="txt_transfer_date" runat="server" class="ppup-trnsfr-rwtxtbx-txtbx"></asp:TextBox>
                                    <script>
                                        $(function () {
                                            $("#<%=txt_transfer_date.ClientID %>").datepicker({
                                                dateFormat: "dd/mm/yy",
                                                changeMonth: true,
                                                changeYear: true,
                                                yearRange: "1900:2100",
                                            }).attr("readonly", "true");
                                        });
                                    </script>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <p class="ppup-trnsfr-rwtxtbx-p">Select Floor</p>
                            </div>
                            <div class="col-md-6">
                                <div class="ppup-trnsfr-rwtxtbx-dv">
                                    <asp:DropDownList ID="ddl_floor" runat="server" class="ppup-trnsfr-rwtxtbx-txtbx">
                                        <asp:ListItem>Ground Floor</asp:ListItem>
                                        <asp:ListItem>1st Floor</asp:ListItem>
                                        <asp:ListItem>2nd Floor</asp:ListItem>
                                        <asp:ListItem>3rd Floor</asp:ListItem>
                                        <asp:ListItem>4th Floor</asp:ListItem>
                                        <asp:ListItem>5th Floor</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <p class="ppup-trnsfr-rwtxtbx-p">Room No.</p>
                            </div>
                            <div class="col-md-6">
                                <div class="ppup-trnsfr-rwtxtbx-dv">
                                    <asp:DropDownList ID="ddl_room_no_p" runat="server" class="ppup-trnsfr-rwtxtbx-txtbx"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <p class="ppup-trnsfr-rwtxtbx-p">Select Section</p>
                            </div>
                            <div class="col-md-6">
                                <div class="ppup-trnsfr-rwtxtbx-dv">
                                    <asp:DropDownList ID="ddl_section_p" runat="server" class="ppup-trnsfr-rwtxtbx-txtbx"></asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                <p class="ppup-trnsfr-rwtxtbx-p">Transfer Qnt.</p>
                            </div>
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="ppup-trnsfr-rwtxtbx-dv">
                                            <asp:TextBox ID="txt_transfer_qnt" runat="server" class="ppup-trnsfr-rwtxtbx-txtbx"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="ppup-trnsfr-rwtxtbx-dv">
                                            <asp:TextBox ID="txt_unit_p" ReadOnly="true" runat="server" class="ppup-trnsfr-rwtxtbx-txtbx"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <p class="ppup-trnsfr-rwtxtbx-p">Unique Key</p>
                            </div>
                            <div class="col-md-6">
                                <div class="ppup-trnsfr-rwtxtbx-dv">
                                    <asp:TextBox ID="txt_unique_key" runat="server" class="ppup-trnsfr-rwtxtbx-txtbx" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <p class="ppup-trnsfr-rwtxtbx-p">Transfer By</p>
                            </div>
                            <div class="col-md-6">
                                <div class="ppup-trnsfr-rwtxtbx-dv">
                                    <asp:TextBox ID="txt_transfer_by" runat="server" class="ppup-trnsfr-rwtxtbx-txtbx"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                            </div>
                            <div class="col-md-6">
                                <div class="ppup-trnsfr-rwtxtbx-dv">
                                    <asp:Button ID="btn_transfer_s" runat="server" Text="Submit" OnClick="btn_transfer_s_Click" Style="background: #0f22ff; border: 1px solid #0010cb; color: #fff; border-radius: 2px; padding: 3px 10px 2px 10px;" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
