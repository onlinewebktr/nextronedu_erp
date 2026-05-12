<%@ Page Title="" Language="C#" MasterPageFile="~/Student_Profile/main.Master" AutoEventWireup="true" CodeBehind="Myorder.aspx.cs" Inherits="school_web.Student_Profile.Myorder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    My Order 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .texbox-border {
            margin: 6px 0px 0px 0px;
            padding: 0px;
            height: auto;
            width: 100%;
            border-bottom: 1px solid #00000038;
        }


        .navbar-expand-lg .navbar-nav {
            flex-direction: row;
            float: right !important;
        }


        .dropdown-menu > li > a {
            font-size: 14px !important;
        }

        .notificationpan {
            top: 58px !important;
        }

        table.dataTable > tbody > tr.child ul.dtr-details > li {
            border-bottom: 1px solid #efefef;
            padding: 0 !important;
        }
    </style>
     <script language="javascript">
         var popupWindow = null;
         function positionedPopup(url, winName, w, h, t, l, scroll) {
             settings =
                 'height=' + h + ',width=' + w + ',top=' + t + ',left=' + l + ',scrollbars=' + scroll + ',resizable'
             popupWindow = window.open(url, winName, settings)
         }
     </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pagemainhh">
        <div class="container">

            <div id="notification">
                <div id="pan" class="notificationpan">
                    <div id="success" runat="server" visible="false" style="float: left; width: 100%; height: auto;" class="alert alert-success border-0 bg-success alert-dismissible fade show py-2">
                        <div class="d-flex align-items-center">
                            <div class="font-35 text-white">
                                <i class='bx bxs-check-circle'></i>
                            </div>
                            <div class="ms-3">
                                <asp:Label ID="lbl_success" runat="server" ForeColor="White" class="text-white"></asp:Label>
                            </div>
                        </div>
                        <asp:LinkButton ID="LinkButton1" class="btn-close" runat="server" Style="color: #fff">X</asp:LinkButton>
                    </div>

                    <div id="warning" runat="server" visible="false" class="alert alert-warning border-0 bg-warning alert-dismissible fade show py-2">
                        <div class="d-flex align-items-center">
                            <div class="font-35 text-dark">
                                <i class='bx bx-info-circle'></i>
                            </div>
                            <div class="ms-3">
                                <asp:Label ID="lbl_warning" runat="server" ForeColor="White" class="text-white"></asp:Label>
                            </div>
                        </div>
                        <asp:LinkButton ID="LinkButton2" class="btn-close" runat="server" Style="color: #fff">X</asp:LinkButton>
                    </div>
                </div>
            </div>


            <div class="main-card mb-3 card">
                <div class="card-header">
                    <h4 class="card-title">My Order</h4>
                </div>
                <div class="card-body" style="padding-top: 0px;">
                    <div class="headingtablee">



                        <table id="datatable" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                            <thead>

                                <tr>

                                    <th>App Order No.</th>
                                    <th>No of Item</th>
                                    <th>Order Date</th>
                                    <th>Order Status</th>
                                    <th>Total Amount</th>
                                    <th></th>

                                </tr>
                            </thead>
                            <tbody>
                                <asp:HiddenField ID="hdUserID" runat="server" />
                                <asp:Repeater ID="RPDetails" runat="server" OnItemDataBound="RPDetails_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>

                                            <td>
                                                <asp:Label ID="lbl_Bill_No" runat="server" Text='<%#Bind("Bill_No") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Total_Quantity" runat="server" Text='<%#Bind("Total_Quantity") %>'></asp:Label>
                                            </td>
                                            <td>

                                                <asp:Label ID="lbl_date" runat="server" Text='<%#Bind("Date", "{0:d}") %>'></asp:Label>
                                            </td>
                                            <td>

                                                <asp:Label ID="lbl_Order_Status" runat="server" Text='<%#Bind("Order_Status") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Total_amount" runat="server" Text='<%#Eval("Total_amount") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_userid" runat="server" Visible="false" Text='<%#Eval("user_id") %>'></asp:Label>
                                                <asp:Label ID="lbl_unique_no" runat="server" Visible="false" Text='<%#Eval("unique_no") %>'></asp:Label>
                                                <asp:Button Class="btn btn-primary btn-add" Style="text-align: center; padding: 4px 8px 4px 9px; float: left; margin: 0px 10px 0px 0px;"
                                                    ID="btn_view" runat="server" Text="View" OnClick="btn_view_Click" />

                                                <asp:Panel ID="Panel1" runat="server">
                                                    <a id="a1" runat="server" class="btn btn-primary btn-add" style="text-align: center; padding: 4px 8px 4px 9px; margin: -34px 0px 0px 0px;" onclick="positionedPopup(this.href,'myWindow','950','650','200','200','yes');return false" target="_blank"><i class='bx bx-printer'></i><span>Invoice</span></a>
                                                </asp:Panel>
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

    <!-------popupadd year----->
    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog" style="max-width: 800px;">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" style="font-size: 20px;">Item Details</h5>
                    <a onclick="return PrintPanel('tdprint')" runat="server" class="btn noPrint" style="float: right; display: none; color: #fff; background-color: #2292f1; border-color: #51585e; cursor: pointer;">Print</a>

                    <asp:Button ID="btnclose" runat="server" class="btn btn-secondary noPrint" OnClientClick="return close()" Text="Close" />
                </div>
                <div class="modal-body" style="max-height: 400px; overflow: auto;">
                    <div class="row" id="tdprint">

                        <table border="1" style="width: 100%; border: 1px solid #000;" class=" table table-hover table-bordered">
                            <thead>
                                <tr>
                                    <th>#</th>
                                    <th>Item Name</th>
                                    <th>Unit</th>
                                    <th>Qty.</th>
                                    <th>Rate</th>
                                    <th>Total</th>

                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="GrdView_Generate_PO" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Item_name" runat="server" Text='<%#Bind("Item_name")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_unitname" runat="server" Text='<%#Bind("Unit_name")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_qty" runat="server" Text='<%#Bind("Quantity")%>'></asp:Label>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="lbl_Rate" runat="server" Text='<%#Bind("Rate")%>'></asp:Label>
                                            </td>

                                            <td style="text-align: right;">
                                                <asp:Label ID="lbl_Total" runat="server" Text='<%#Bind("Total")%>'></asp:Label>
                                            </td>







                                        </tr>

                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                            <tfoot>
                                <tr>
                                    <th colspan="6" style="text-align: right;">
                                        <asp:Label ID="lbl_total_value" runat="server" Style="color: #000;" Font-Bold="true"></asp:Label>
                                    </th>
                                </tr>
                            </tfoot>
                        </table>
                    </div>

                </div>
            </div>
        </div>
    </div>
    <div id="fadeup"></div>
    <script type="text/javascript">
        function openModal() {
            $("#myModal").show();
            $('#myModal').addClass('show');
            $('#fadeup').addClass('modal-backdrop fade show');
        }
        function close() {
            $("#myModal").hide();
            $('#myModal').removeClass('show');
            $('#fadeup').removeClass('modal-backdrop fade show');
        }
    </script>
</asp:Content>
