<%@ Page Title="" Language="C#" MasterPageFile="~/Inventory_management/Site3.Master" AutoEventWireup="true" CodeBehind="Add_Stock.aspx.cs" Inherits="school_web.Inventory_management.Add_Stock" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Add Stock
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                <div class="breadcrumb-title pe-3">Create Stock</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Dashboard.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Create Stock</li>
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
                                <div class="row g-3 needs-validation" novalidate="">
                                    <div class="col-md-3">
                                      <label for="validationCustom01" class="form-label">Purchase From<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator9" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="ddl_supplier"></asp:RequiredFieldValidator></sup></label>
                                     
                                         <asp:DropDownList ID="ddl_supplier" runat="server" class="form-control find-dv-txtbx"  AutoPostBack="true"></asp:DropDownList>
                                                    

                                    </div>
 
                                    <div class="col-md-3">
                                      <label for="validationCustom01" class="form-label">Invoice No.<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="Txt_Invoice"></asp:RequiredFieldValidator></sup></label>
                                     
                                        <asp:TextBox ID="Txt_Invoice" runat="server" class="form-control"></asp:TextBox>

                                    </div>
                                     <div class="col-md-3">
                                      <label for="validationCustom01" class="form-label">PO No.<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="Txt_PO"></asp:RequiredFieldValidator></sup></label>
                                     
                                        <asp:TextBox ID="Txt_PO" runat="server" class="form-control"></asp:TextBox>
             

                                    </div>
                                     <div class="col-md-3">
                                      <label for="validationCustom01" class="form-label">Item Name<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="Ddl_Item_name"></asp:RequiredFieldValidator></sup></label>
                                     
                                       <asp:DropDownList ID="Ddl_Item_name" runat="server" class="form-control find-dv-txtbx"  AutoPostBack="true" OnSelectedIndexChanged="Ddl_Item_name_SelectedIndexChanged"></asp:DropDownList>
                                                    

                                    </div>
                                      <div class="col-md-3">
                                      <label for="validationCustom01" class="form-label">Brand name<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="Ddl_select_brand"></asp:RequiredFieldValidator></sup></label>
                                     
                                       <asp:DropDownList ID="Ddl_select_brand" runat="server" class="form-control find-dv-txtbx"  AutoPostBack="true"></asp:DropDownList>
                                                    

                                    </div>
                                     <div class="col-md-3">
                                      <label for="validationCustom01" class="form-label">Unit<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator7" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="Ddl_unit"></asp:RequiredFieldValidator></sup></label>
                                     
                                       <asp:DropDownList ID="Ddl_unit" runat="server" class="form-control find-dv-txtbx"  AutoPostBack="true"></asp:DropDownList>
                                                    

                                    </div>

                                   <div class="col-md-3">
                                      <label for="validationCustom01" class="form-label">HSN<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator22" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="Txt_HSN"></asp:RequiredFieldValidator></sup></label>
                                     
                                        <asp:TextBox ID="Txt_HSN" runat="server" class="form-control"></asp:TextBox>
             

                                    </div>
                                     <div class="col-md-3">
                                      <label for="validationCustom01" class="form-label">Quantity<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator6" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="Txt_Quantity"></asp:RequiredFieldValidator></sup></label>
                                     
                                     
                                        <asp:TextBox ID="Txt_Quantity" runat="server" class="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
               

                                    </div>

                                    
                                     <div class="col-md-3">
                                      <label for="validationCustom01" class="form-label">Cost Rate<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator5" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="Txt_Cost"></asp:RequiredFieldValidator></sup></label>
                                     
                                     
                                        <asp:TextBox ID="Txt_Cost" runat="server" class="form-control" ></asp:TextBox>
               

                                    </div>

                                     <div class="col-md-3">
                                      <label for="validationCustom01" class="form-label">Sell Price<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator8" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="Txt_Sell_Price"></asp:RequiredFieldValidator></sup></label>
                                     
                                     
                                        <asp:TextBox ID="Txt_Sell_Price" runat="server" class="form-control" ></asp:TextBox>
               

                                    </div>

                                     <div class="col-md-3">
                                      <label for="validationCustom01" class="form-label">Total<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator10" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="Txt_Total"></asp:RequiredFieldValidator></sup></label>
                                     
                                     
                                        <asp:TextBox ID="Txt_Total" runat="server" class="form-control"></asp:TextBox>
               

                                    </div>

                                     <div class="col-md-3">
                                      <label for="validationCustom01" class="form-label">GST Type<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator11" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="Txt_GST_Type"></asp:RequiredFieldValidator></sup></label>
                                     
                                     
                                        <asp:TextBox ID="Txt_GST_Type" runat="server" class="form-control" ></asp:TextBox>
               

                                    </div>
                                      <div class="col-md-4">
                                      <label for="validationCustom01" class="form-label">GST %<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator12" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="Txt_GST"></asp:RequiredFieldValidator></sup></label>
                                     
                                     
                                        <asp:TextBox ID="Txt_GST" runat="server" class="form-control" ></asp:TextBox>
               

                                    </div>
                                   <div class="col-md-4">
                                      <label for="validationCustom01" class="form-label">GST Value<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator13" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="Txt_GST_Value"></asp:RequiredFieldValidator></sup></label>
                                     
                                     
                                        <asp:TextBox ID="Txt_GST_Value" runat="server" class="form-control" ></asp:TextBox>
               

                                    </div>
                                  <div class="col-md-4">
                                      <label for="validationCustom01" class="form-label">Net Total<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator14" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="Txt_Net_Total"></asp:RequiredFieldValidator></sup></label>
                                     
                                     
                                        <asp:TextBox ID="Txt_Net_Total" runat="server" class="form-control" ></asp:TextBox>
               

                                    </div>

                                    

                                    <div class="col-12">
                                        <asp:Button ID="Btn_Add" runat="server" Text="Add" OnClick="Btn_Add_Click" CssClass="btn btn-primary" ValidationGroup="a"  />
                                  <asp:Button ID="Btn_Update" runat="server" Text="Update" OnClick="Btn_Update_Click" class="btn btn-primary" Visible="false" ValidationGroup="a" />
                                <asp:Button ID="Btn_Cancel" runat="server" Text="Cancel" OnClick="Btn_Cancel_Click" class="btn btn-primary" Visible="true" ValidationGroup="a" />
                                  
                                        </div>
                                  </div>
                            </div>
                        </div>
                    </div>
                </div>


                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase">Added Stock</h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                    <div class="row">
                                  
                                        <br />
                                        
                                        <div class="col-sm-6">
                                     <asp:GridView ID="GrdView_Create_Stock" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%" >
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="#">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Date">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Entry_date")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                              
                                                                    <asp:TemplateField HeaderText="Item Name">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_Item_Name" runat="server" Text='<%#Bind("Item_name")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Brand Name">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_Brand_Name" runat="server" Text='<%#Bind("Brand_name")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                
                                                                    <asp:TemplateField HeaderText="Unit Name">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_Unit_Name" runat="server" Text='<%#Bind("Unit_name")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Quantity">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_Quantity" runat="server" Text='<%#Bind("Quantity")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Remarks">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_Remarks" runat="server" Text='<%#Bind("Remarks")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    
                                                                   
                                                                    
                                                                   
                                                                   <asp:TemplateField HeaderText="Action">
                                                                        <ItemTemplate>
                                                                           <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Edit"> <i class="lni lni-pencil-alt"> </i></asp:LinkButton>
                                                                         <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                        <asp:Label ID="lbl_Item_id" runat="server" Text='<%#Bind("Item_id")%>' Visible="false"></asp:Label>
                                                                         <asp:Label ID="lbl_Unit_Id" runat="server" Text='<%#Bind("Unit_id")%>' Visible="false"></asp:Label>
                                                                        <asp:Label ID="lbl_Brand_Id" runat="server" Text='<%#Bind("Brand_id")%>' Visible="false"></asp:Label>
                                                                       <asp:Label ID="lbl_Stock_id" runat="server" Text='<%#Bind("Stock_id")%>' Visible="false"></asp:Label>
                                                                        <asp:Label ID="lbl_Store_id" runat="server" Text='<%#Bind("Store_id")%>' Visible="false"></asp:Label>
                                                                      
                                                                            </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                </Columns>
                                                            </asp:GridView>


                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                 <div class="col-xl-12">
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation" novalidate="">
                                    <div class="col-md-3">
                                      <label for="validationCustom01" class="form-label">Total Item:  <sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator15" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="Txt_Item"></asp:RequiredFieldValidator></sup></label>
                                     
                                        <asp:TextBox ID="Txt_Item" runat="server" class="form-control"></asp:TextBox>

                                    </div>
                                    
                                     <div class="col-md-3">
                                      <label for="validationCustom01" class="form-label">Total QTY:  <sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator16" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="Txt_QTY"></asp:RequiredFieldValidator></sup></label>
                                     
                                        <asp:TextBox ID="Txt_QTY" runat="server" class="form-control"></asp:TextBox>

                                    </div>
                                    <div class="col-md-3">
                                      <label for="validationCustom01" class="form-label">Total:   <sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator17" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="Txt_Totall"></asp:RequiredFieldValidator></sup></label>
                                     
                                        <asp:TextBox ID="Txt_Totall" runat="server" class="form-control"></asp:TextBox>

                                    </div>
                                    <div class="col-md-3">
                                      <label for="validationCustom01" class="form-label">Total Taxable: <sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator18" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="Txt_Taxable"></asp:RequiredFieldValidator></sup></label>
                                     
                                        <asp:TextBox ID="Txt_Taxable" runat="server" class="form-control"></asp:TextBox>

                                    </div>
                                   
                                     <div class="col-md-4">
                                      <label for="validationCustom01" class="form-label">Total Tax:  <sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator19" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="Txt_Total_tax"></asp:RequiredFieldValidator></sup></label>
                                     
                                        <asp:TextBox ID="Txt_Total_tax" runat="server" class="form-control"></asp:TextBox>

                                    </div>
                                    <div class="col-md-4">
                                      <label for="validationCustom01" class="form-label">Round OFF:   <sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator20" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="Txt_Round_OFF"></asp:RequiredFieldValidator></sup></label>
                                     
                                        <asp:TextBox ID="Txt_Round_OFF" runat="server" class="form-control"></asp:TextBox>

                                    </div>
                                    <div class="col-md-4">
                                      <label for="validationCustom01" class="form-label">Grand Total: <sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator21" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="Txt_Grand_Total"></asp:RequiredFieldValidator></sup></label>
                                     
                                        <asp:TextBox ID="Txt_Grand_Total" runat="server" class="form-control"></asp:TextBox>

                                    </div>
  <div class="col-12">
                                        <asp:Button ID="Btn_submit" runat="server" Text="Final Submit" OnClick="Btn_submit_Click"  CssClass="btn btn-primary" ValidationGroup="a"  />
                                  
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
 
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
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
     <script type="text/javascript">
         $(document).ready(function () {
             $("#<%=Ddl_Item_name.ClientID%>").select2();

          });
     </script>

      <script type="text/javascript">
          $(document).ready(function () {
              $("#<%=ddl_supplier.ClientID%>").select2();

         });
      </script>

</asp:Content>
