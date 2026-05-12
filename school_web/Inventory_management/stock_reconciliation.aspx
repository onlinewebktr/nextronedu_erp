<%@ Page Title="" Language="C#" MasterPageFile="~/Inventory_management/Site3.Master" AutoEventWireup="true" CodeBehind="stock_reconciliation.aspx.cs" Inherits="school_web.Inventory_management.stock_reconciliation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">stock Reconciliation
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

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

            #Header, #Footer {
                display: none !important;
            }
        }

        .printheading {
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

     


        <asp:HiddenField ID="HdID" runat="server" />
        <div class="row">
       
            <div class="col-xl-12">
                <h6 class="mb-0 text-uppercase"> Stock Reconciliation

                  

                </h6>
                <hr />
                <div class="card">
                    <div class="card-body">
                        <div class="table-responsive">

                            <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                <div class="row">
                                     <div class="col-xl-11" style="margin: 0px auto;">
                <%--   <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Generate PO"></asp:Label>
                <hr />--%>

                <div class="card">
                    <div class="card-body">
                        <div class="p-4 border rounded" style="background: #d0fbff66;">
                            
                            <div class="row g-3 needs-validation" novalidate="" style="margin-top: 10px;">
                                <div class="col-md-4">
                                    <label for="validationCustom01" class="form-label">
                                        Item <sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_Item"></asp:RequiredFieldValidator></sup>

                                    </label>
                                    <asp:TextBox ID="txt_Item" runat="server" class="form-control find-dv-txtbx" AutoPostBack="true" OnTextChanged="txt_Item_TextChanged"></asp:TextBox>
                                    <asp:HiddenField ID="hd_itemcode" runat="server" />
                                   
                                </div>
                                 <div class="col-md-2">
                                    <label for="validationCustom01" class="form-label">Avl. Qty. <sup></sup></label>
                                    <asp:TextBox ID="txt_avlqty"  runat="server" ReadOnly="true" class="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label for="validationCustom01" class="form-label">Change Qty. <sup>  </sup></label>
                                    <asp:TextBox ID="txt_qty" onkeypress="return isNumberKey(event)" runat="server" class="form-control"></asp:TextBox>
                                </div>
                                 <div class="col-md-2">
                                    <label for="validationCustom01" class="form-label">Action<sup> <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="ddl_action"></asp:RequiredFieldValidator></sup></label>
                                    <asp:DropDownList ID="ddl_action" runat="server" class="form-select">
                                        <asp:ListItem>Select</asp:ListItem>
                                        <asp:ListItem>DECREASE</asp:ListItem>
                                        <asp:ListItem>INCREASE</asp:ListItem>
                                    </asp:DropDownList>
                                </div>




                               
                            </div>
                            <div class="row g-3 needs-validation" novalidate="">
                                <div class="col-md-7">
                                    <label for="validationCustom01" class="form-label" style="margin-top: 12px;">
                                        Remarks <sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator5" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_remarks"></asp:RequiredFieldValidator></sup>


                                    </label>
                                    <asp:TextBox ID="txt_remarks" runat="server" class="form-control find-dv-txtbx" style="min-height:150px" TextMode="MultiLine"></asp:TextBox>
                                </div>

                                 <div class="col-md-2" style="padding: 40px 0px 0px 0px;">
                                    <asp:Button ID="Btn_Add" runat="server" Text="Add" OnClick="Btn_Add_Click" CssClass="btn btn-primary" ValidationGroup="a" />

                                    
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
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
      <script type="text/javascript">
        $(document).ready(function () {

            searching();
        });
        function searching() {



            $("#<%=txt_Item.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'stock_reconciliation.aspx/Search_inventory_item',
                        data: "{ 'itemName': '" + request.term + "'}",
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
                                //response([{ label: 'No results found.' }]);
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
        }
      </script>
</asp:Content>
