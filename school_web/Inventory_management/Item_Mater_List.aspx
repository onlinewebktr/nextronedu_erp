<%@ Page Title="" Language="C#" MasterPageFile="~/Inventory_management/Site3.Master" AutoEventWireup="true" CodeBehind="Item_Mater_List.aspx.cs" Inherits="school_web.Inventory_management.Item_Mater_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Item List
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

    <%-- <script type="text/javascript">
        function printPartOfPage() {
            $("#btnPrint").click(function () {
                var contents = $("#tblPrintIQ").html();
                var frame1 = $('<iframe />');
                frame1[0].name = "frame1";
                frame1.css({ "position": "absolute", "top": "-1000000px" });
                $("body").append(frame1);
                var frameDoc = frame1[0].contentWindow ? frame1[0].contentWindow : frame1[0].contentDocument.document ? frame1[0].contentDocument.document : frame1[0].contentDocument;
                frameDoc.document.open();
                //Create a new HTML document.
                frameDoc.document.write('<html><head><title>Attendance Report</title>');
                frameDoc.document.write('</head><body>');
                //Append the external CSS file.
                frameDoc.document.write('<link href="https://fonts.googleapis.com/css2?family=Dosis:wght@200;300;400;500;600;700;800&family=Montserrat:ital,wght@0,300;0,400;0,500;0,600;0,700;0,800;0,900;1,200;1,300;1,400;1,500;1,600;1,700;1,800;1,900&family=Roboto:ital,wght@0,100;0,300;0,400;0,500;0,700;0,900;1,100;1,300;1,400;1,500;1,700;1,900&display=swap" rel="stylesheet"/>');
                frameDoc.document.write('<link href="css/Print.css" rel="stylesheet" type="text/css" />');
                frameDoc.document.write('<link href="../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />');
                //Append the DIV contents.
                frameDoc.document.write(contents);
                frameDoc.document.write('</body></html>');
                frameDoc.document.close();
                setTimeout(function () {
                    window.frames["frame1"].focus();
                    window.frames["frame1"].print();
                    frame1.remove();
                }, 500);
            });
        }
</script>--%>

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

        <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
            <div class="breadcrumb-title pe-3">Master</div>
            <div class="ps-3">
                <nav aria-label="breadcrumb">
                    <ol class="breadcrumb mb-0 p-0">
                        <li class="breadcrumb-item"><a href="Dashboard.aspx"><i class="bx bx-home-alt"></i></a>
                        </li>
                        <li class="breadcrumb-item active" aria-current="page">Item List  </li>
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
                        <div class="table-responsive">
                            <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                <div class="row">

                                    <br />

                                    <div class="col-sm-12">
                                        <div style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%">
                                            <div style="margin: 0px; padding: 0px; float: left; height: auto; width: 75%">
                                                <asp:LinkButton ID="btn_excels" runat="server" OnClick="btn_excels_Click" class="btn btn_excel find-dv-btn">  <i class='bx bx-download'></i> Excel</asp:LinkButton>

                                                <input type="button" id="btnPrint" value="Print" onclick="JavaScript:printPartOfPage();" style="display: none; margin: 0px 0px 6px 10px; padding: 5px 0px 5px 5px;" class="btn btn-primary find-dv-btn" />

                                                <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" Style="margin: 0px 0px 6px 10px; padding: 5px 0px 5px 5px;" CssClass="btn btn-primary find-dv-btn" runat="server"
                                                    ToolTip="Print"><i class='bx bx-printer'></i></asp:LinkButton>

                                                <a class="btn btn-success find-dv-btn" href="Create_Item.aspx" style="margin: 0px 13px 1px 0px !important; float: left; padding: 3px 6px 6px 11px; float: left; padding: 3px 6px 6px 11px;"
                                                    title="Add Item"><i class="bx bx-plus"></i></a>
                                            </div>


                                        </div>
                                        <div id="tblPrintIQ" runat="server">

                                            <div class="pgslry-head-div" style="width: 100%;">
                                                <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;" class="head">
                                                    <span style="font-size: 14px; font-weight: bold;">Item List </span>


                                                </div>

                                                <div class="row" id="SearchData">

                                                    <br />
                                                    <asp:Panel ID="pnl_grid" runat="server" Style="width: 100%;">

                                                        <table style="width: 100%;" id="example" class=" table table-hover table-striped table-bordered">
                                                            <thead>
                                                                <tr>
                                                                    <th>#</th>
                                                                    <th>Action</th>
                                                                    <th>Group Name</th>
                                                                    <th>Item Name</th>
                                                                    <th>Brand Name</th>
                                                                    <th>Unit Name</th>
                                                                    <th>Color</th>
                                                                    <th>Size</th>
                                                                    <th>HSN</th>
                                                                    <th>GST</th>
                                                                    <th>Item Type</th>
                                                                    <th>Date</th>
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
                                                                                <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" CssClass="lnkEdit" OnClick="lnkEdit_Click" ToolTip="Edit"><i class="lni lni-pencil-alt"></i></asp:LinkButton>
                                                                                <asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" CssClass="lnkdelete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"></i></asp:LinkButton>
                                                                                <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lbl_Item_id" runat="server" Text='<%#Bind("Item_id")%>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lbl_Unit_Id" runat="server" Text='<%#Bind("Unit_id")%>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lbl_Brand_Id" runat="server" Text='<%#Bind("Brand_id")%>' Visible="false"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Groupname" runat="server" Text='<%#Bind("Group_Name")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Item_Name" runat="server" Text='<%#Bind("Item_Name")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Brand_Name" runat="server" Text='<%#Bind("Brand_name")%>'></asp:Label>
                                                                            </td>


                                                                            <td>
                                                                                <asp:Label ID="lbl_Unit_Name" runat="server" Text='<%#Bind("Unit")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Color" runat="server" Text='<%#Bind("Color")%>'></asp:Label>
                                                                            </td>

                                                                            <td>
                                                                                <asp:Label ID="lbl_Size" runat="server" Text='<%#Bind("Size")%>'></asp:Label>
                                                                            </td>

                                                                            <td>
                                                                                <asp:Label ID="lbl_HSN" runat="server" Text='<%#Bind("HSN")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_GST" runat="server" Text='<%#Bind("GST")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Item_type" runat="server" Text='<%#Bind("Item_type")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Created_Date")%>'></asp:Label>
                                                                            </td>
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
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">

     
</asp:Content>
