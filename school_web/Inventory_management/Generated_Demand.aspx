<%@ Page Title="" Language="C#" MasterPageFile="~/Inventory_management/Site3.Master" AutoEventWireup="true" CodeBehind="Generated_Demand.aspx.cs" Inherits="school_web.Inventory_management.Generated_Demand" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Generated Demand</asp:Content>
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
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class='popup1'>
        <div class='content1' style="top: 10px; min-width: 600px; height: auto; width: 650px;">

            <table style="width:100%;">

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
                                        <td>Demand No:-
                                            <asp:Label ID="lbl_demand_no" runat="server"></asp:Label>
                                        </td>
                                        <td>Date:-
                                            <asp:Label ID="lbl_demand_date" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:GridView ID="GrdView_Generate_PO" runat="server" class="data-Table table table-bordered" AutoGenerateColumns="False" Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Item_name" runat="server" Text='<%#Bind("Item_Name")%>'></asp:Label>
                                                            <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Unit_name" runat="server" Text='<%#Bind("Unit")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Quantity">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_qty" runat="server" Text='<%#Bind("Qty")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Accept Quantity">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txt_qty" runat="server" Text='<%#Bind("Qty")%>' Style="width: 50px;"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remark">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txt_remarks" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:Button ID="btn_accept_demand" runat="server" Text="Accept" OnClick="btn_accept_demand_Click" />
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
            <div class="breadcrumb-title pe-3">Generated Demand</div>
            <div class="ps-3">
                <nav aria-label="breadcrumb">
                    <ol class="breadcrumb mb-0 p-0">
                        <li class="breadcrumb-item"><a href="Dashboard.aspx"><i class="bx bx-home-alt"></i></a>
                        </li>
                        <li class="breadcrumb-item active" aria-current="page">Generated Demand</li>
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
                <h6 class="mb-0 text-uppercase">Generated Demand </h6>
                <hr />
                <div class="card">
                    <div class="card-body">
                        <div class="table-responsive">
                            <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                <div class="row">

                                    <br />

                                    <div class="col-sm-12">


                                        <table style="width: 100%;" id="example" class=" table table-hover table-striped table-bordered">
                                            <thead>
                                                <tr>
                                                    <th>#</th>
                                                    <th>Store Name</th>
                                                    <th>Store Type</th>
                                                    <th>Date</th>
                                                    <th>Demand No.</th>
                                                    <th>Total Items</th>
                                                    <th>Total Qty</th>
                                                    <th>Status</th>
                                                    <th>Action</th>
                                                    <th>Print</th>
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
                                                                <asp:Label ID="lbl_Store_name" runat="server" Text='<%#Bind("Store_name")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbl_Sector" runat="server" Text='<%#Bind("Sector")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbl_date" runat="server" Text='<%#Bind("date")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbl_Demand_no" runat="server" Text='<%#Bind("Demand_no")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbl_Total_items" runat="server" Text='<%#Bind("Total_items")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbl_Qty" runat="server" Text='<%#Bind("Qty")%>'></asp:Label>
                                                            </td>

                                                            <td>
                                                                <asp:Label ID="lbl_status" runat="server"></asp:Label>
                                                                <asp:Label ID="lbl_Is_accept" runat="server" Text='<%#Bind("Is_accept")%>' Visible="false"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:LinkButton ID="lnk_View" runat="server" ToolTip="View" OnClick="lnk_View_Click" Style="color: red;">View</asp:LinkButton>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbl_Cart_id" runat="server" Text='<%#Bind("Store_id")%>' Visible="false"></asp:Label>
                                                                <asp:LinkButton ID="lnk_print" runat="server" ToolTip="Print" OnClick="lnk_print_Click" Style="color: red;">Print</asp:LinkButton>
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
