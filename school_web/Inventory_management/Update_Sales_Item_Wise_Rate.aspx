<%@ Page Title="" Language="C#" MasterPageFile="~/Inventory_management/Site3.Master" AutoEventWireup="true" CodeBehind="Update_Sales_Item_Wise_Rate.aspx.cs" Inherits="school_web.Inventory_management.Update_Sales_Item_Wise_Rate" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Update sales rate

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="https://fonts.googleapis.com/css2?family=Dosis:wght@200;300;400;500;600;700;800&family=Montserrat:ital,wght@0,300;0,400;0,500;0,600;0,700;0,800;0,900;1,200;1,300;1,400;1,500;1,600;1,700;1,800;1,900&family=Roboto:ital,wght@0,100;0,300;0,400;0,500;0,700;0,900;1,100;1,300;1,400;1,500;1,700;1,900&display=swap" rel="stylesheet"/><link href="Slip/print.css" rel="stylesheet" type="text/css" /><link href="../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />');
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

        .disp {
            display: none;
        }
    </style>
    <script language="Javascript">
       <!--
    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode != 46 && charCode > 31
            && (charCode < 48 || charCode > 57))
            return false;

        return true;
    }
    //-->
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
            <div class="breadcrumb-title pe-3">Sale Entry </div>
            <div class="ps-3">
                <nav aria-label="breadcrumb">
                    <ol class="breadcrumb mb-0 p-0">
                        <li class="breadcrumb-item"><a href="Dashboard.aspx"><i class="bx bx-home-alt"></i></a>
                        </li>
                        <li class="breadcrumb-item active" aria-current="page">Update sales rate  </li>
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
                                        <div style="margin: 0px; padding: 0px; float: left; height: auto; width: 75%">
                                            <asp:LinkButton ID="btn_excels" runat="server" OnClick="btn_excels_Click" class="btn btn_excel find-dv-btn">  <i class='bx bx-download'></i> Excel</asp:LinkButton>

                                            <input type="button" id="btnPrint" value="Print" onclick="JavaScript:printPartOfPage();" style="display: none; margin: 0px 0px 6px 10px; padding: 5px 0px 5px 5px;" class="btn btn-primary find-dv-btn" />

                                            <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" Style="margin: 0px 0px 6px 10px; padding: 5px 0px 5px 5px;" CssClass="btn btn-primary find-dv-btn" runat="server"
                                                ToolTip="Print"><i class='bx bx-printer'></i></asp:LinkButton>


                                        </div>


                                    </div>
                                    <div id="tblPrintIQ" runat="server">

                                        <div class="pgslry-head-div" style="width: 100%;">
                                            <div class="row" id="SearchData">

                                                <h6 class="mb-0 text-uppercase disp" style="font-size: 15px; margin: 0px 0px 10px 0px;">Update sales rate</h6>

                                                <asp:Panel ID="pnl_grid" runat="server" Style="width: 100%;">
                                                    <table style="width: 100%;" id="example" class=" table table-hover table-striped table-bordered">
                                                        <thead>
                                                            <tr>
                                                                <th>#</th>
                                                                <th>Item Name</th>
                                                                <th>Brand Name</th>
                                                                <th>Unit</th>
                                                                <th>Old Rate</th>
                                                                <th>New Rate</th>
                                                                <th class="allCheckbox" style="width: 86px;">
                                                                    <asp:CheckBox ID="hdrChkBox" runat="server" />Check All
                                                                </th>
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
                                                                            <asp:Label ID="lbl_unit" runat="server" Text='<%#Bind("unit")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_Sale_rate" runat="server" Text='<%#Bind("Sale_ratenew")%>'  ></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            
                                                                            <asp:Label ID="lbl_Item_Code" runat="server" Text='<%#Bind("Item_Code")%>' Visible="false"></asp:Label>
                                                                            <asp:Label ID="lbl_Brand_Id" runat="server" Text='<%#Bind("Brand_Id")%>' Visible="false"></asp:Label>
                                                                            <asp:Label ID="lbl_Unit_id" runat="server" Text='<%#Bind("Unit_id")%>' Visible="false"></asp:Label>
                                                                            <asp:TextBox ID="txt_Sale_rate" runat="server" Text='<%#Bind("Sale_ratenew")%>' Style="width: 40px;" onkeypress="return isNumberKey(event)"></asp:TextBox>

                                                                        </td>


                                                                        <td class="singleCheckbox" style="width: 43px!important;">
                                                                            <asp:CheckBox ID="rowChkBox" runat="server" />
                                                                        </td>


                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </tbody>
                                                    </table>
                                                </asp:Panel>

                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 noPrint">
                                                    <label style="width: 100%"></label>
                                                    <asp:Button ID="btn_submit" runat="server" Text="Final Submit" CssClass="btn btn-primary noPrint" OnClick="btn_submit_Click" ValidationGroup="a" Style="margin: 0px!important; float: right" OnClientClick="Confirm()" />
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
    <script type="text/javascript">
        $(function () {
            var $allCheckbox = $('.allCheckbox :checkbox');
            var $checkboxes = $('.singleCheckbox :checkbox');
            $allCheckbox.change(function () {
                if ($allCheckbox.is(':checked')) {
                    $checkboxes.attr('checked', 'checked');
                }
                else {
                    $checkboxes.removeAttr('checked');
                }
            });
            $checkboxes.change(function () {
                if ($checkboxes.not(':checked').length) {
                    $allCheckbox.removeAttr('checked');
                }
                else {
                    $allCheckbox.attr('checked', 'checked');
                }
            });
        });
    </script>
    <script type="text/javascript" language="javascript">
        function checkAllRows(obj) {

            var objGridview = obj.parentNode.parentNode.parentNode;
            var list = objGridview.getElementsByTagName("input");

            for (var i = 0; i < list.length; i++) {
                var objRow = list[i].parentNode.parentNode;
                if (list[i].type == "checkbox" && obj != list[i]) {
                    if (obj.checked) {

                        //If the header checkbox is checked then check all 
                        //checkboxes and highlight all rows.

                        objRow.style.backgroundColor = "#99E5E5";
                        list[i].checked = true;
                    }
                    else {
                        objRow.style.backgroundColor = "#FFFFFF";
                        list[i].checked = false;
                    }
                }
            }
        }

        function checkUncheckHeaderCheckBox(obj) {
            var objRow = obj.parentNode.parentNode;

            if (obj.checked) {
                objRow.style.backgroundColor = "#99E5E5";
            }
            else {
                objRow.style.backgroundColor = "#FFFFFF";
            }
            var objGridView = objRow.parentNode;

            //Get all input elements in Gridview
            var list = objGridView.getElementsByTagName("input");
            for (var i = 0; i < list.length; i++) {
                var objHeaderChkBox = list[0];

                //Based on all or none checkboxes are checked check/uncheck Header Checkbox
                var checked = true;

                if (list[i].type == "checkbox" && list[i] != objHeaderChkBox) {
                    if (!list[i].checked) {
                        checked = false;
                        break;
                    }
                }
            }
            objHeaderChkBox.checked = checked;
        }
    </script>
    <script type="text/javascript">
        function Confirm() {
            var confirm_value
            var isSubmitted = false;
            confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to submit final?")) {
                confirm_value.value = "Yes";
                if (!isSubmitted) {
                    $('#<%=btn_submit.ClientID %>').val('Submitting.. Please Wait..');
                    isSubmitted = true;
                }
                else {
                    alert("Please Wait.. due to process is running");
                }
            }
            else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
</asp:Content>
