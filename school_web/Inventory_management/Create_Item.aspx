<%@ Page Title="" Language="C#" MasterPageFile="~/Inventory_management/Site3.Master" AutoEventWireup="true" CodeBehind="Create_Item.aspx.cs" Inherits="school_web.Inventory_management.Create_Item" MaintainScrollPositionOnPostback="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Create Item
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

    <style>
        sup {
            color: red;
        }
    </style>
    <script language="JavaScript" type="text/javascript">
        function clearWord() {
            userWord = "";
            document.forms[0].result.value = "";
        }
        var userWord = "";
        function TrapKey(obj, e) {
            thekey = String.fromCharCode(event.keyCode);
            userWord += thekey;
            for (var i = 0; i < obj.options.length; i++) {
                var txt = obj.options[i].text.toUpperCase();
                document.forms[0].result.value = userWord;
                if (txt.indexOf(userWord) == 0) {
                    obj.options[i].selected = true;
                    obj.options[i].focus();
                    break;
                }
            }
            setTimeout("clearWord()", 3000)
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
            <div class="breadcrumb-title pe-3">Master </div>
            <div class="ps-3">
                <nav aria-label="breadcrumb">
                    <ol class="breadcrumb mb-0 p-0">
                        <li class="breadcrumb-item"><a href="Dashboard.aspx"><i class="bx bx-home-alt"></i></a>
                        </li>
                        <li class="breadcrumb-item active" aria-current="page">Create Item </li>
                    </ol>
                </nav>
            </div>
        </div>



        <asp:HiddenField ID="HdID" runat="server" />
        <div class="row">
            <div class="col-xl-7" style="margin: 0px auto;">
                <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Create Item"></asp:Label>

                <a href="Upload_Item_by_excel.aspx" style="float: right; display:none">Upload by Excel</a>
                <hr />
                <div class="card">
                    <div class="card-body">
                        <div class="p-4 border rounded">
                            <div class="row g-3 needs-validation" novalidate="">
                                <div class="col-md-4">
                                    <label for="validationCustom01" class="form-label">Item Group  <sup>* </sup></label>

                                    <asp:DropDownList ID="ddl_item_groupname" onKeyup="TrapKey(this, this.event)" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>

                                </div>
                                <div class="col-md-8">
                                    <label for="validationCustom01" class="form-label">Enter Item Name  <sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="Txt_Item_Name"></asp:RequiredFieldValidator></sup></label>

                                    <asp:TextBox ID="Txt_Item_Name" runat="server" class="form-control"></asp:TextBox>

                                </div>
                                <div class="col-md-4">
                                    <label for="validationCustom01" class="form-label">Select Brand  <sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="Ddl_select_Brand"></asp:RequiredFieldValidator></sup></label>

                                    <asp:DropDownList ID="Ddl_select_Brand" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>


                                </div>
                                <div class="col-md-4">
                                    <label for="validationCustom01" class="form-label">Select Unit <sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="Ddl_Unit"></asp:RequiredFieldValidator></sup></label>

                                    <asp:DropDownList ID="Ddl_Unit" runat="server" class="form-control find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="Ddl_Unit_SelectedIndexChanged"></asp:DropDownList>


                                </div>
                                 <div class="col-md-4">
                                    <label for="validationCustom01" class="form-label">Select Color <sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator8" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="ddl_color"></asp:RequiredFieldValidator></sup></label>

                                    <asp:DropDownList ID="ddl_color" runat="server" class="form-control find-dv-txtbx"  >
                                        <asp:ListItem>N/A</asp:ListItem>
                                        <asp:ListItem>white</asp:ListItem>
                                        <asp:ListItem>Red</asp:ListItem>
                                        <asp:ListItem>Blue</asp:ListItem>
                                        <asp:ListItem>Green</asp:ListItem>
                                        <asp:ListItem>Navi Blue</asp:ListItem>
                                    </asp:DropDownList>


                                </div>
                                  <div class="col-md-4">
                                    <label for="validationCustom01" class="form-label">Size<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator9" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_size"></asp:RequiredFieldValidator></sup></label>

                                    <asp:TextBox ID="txt_size" runat="server" class="form-control"></asp:TextBox>


                                </div>
                                <div class="col-md-4">
                                    <label for="validationCustom01" class="form-label">HSN<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="Txt_HSN"></asp:RequiredFieldValidator></sup></label>

                                    <asp:TextBox ID="Txt_HSN" runat="server" class="form-control"></asp:TextBox>


                                </div>
                                <div class="col-md-4">
                                    <label for="validationCustom01" class="form-label">GST <sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator5" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="ddl_GST"></asp:RequiredFieldValidator></sup></label>

                                    <asp:DropDownList ID="ddl_GST" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>


                                </div>
                                <div class="col-md-4">
                                    <label for="validationCustom01" class="form-label">Item Type <sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator6" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="ddl_item_type"></asp:RequiredFieldValidator></sup></label>

                                    <asp:DropDownList ID="ddl_item_type" runat="server" class="form-control find-dv-txtbx">
                                        <asp:ListItem>Consumable</asp:ListItem>
                                        <asp:ListItem>Assets</asp:ListItem>

                                    </asp:DropDownList>


                                </div>
                                <div class="col-md-12" style="display:none">
                                    <asp:CheckBox ID="chk_add_more_unit" runat="server" AutoPostBack="true" OnCheckedChanged="chk_add_more_unit_CheckedChanged" class="form-label" Text="Add More Unit" />
                                    <asp:Panel ID="pnl_add_moreunit" runat="server" Visible="false">
                                        <div class="col-md-6">
                                            <label for="validationCustom01" class="form-label">Select Secondry Unit <sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator7" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="Ddl_Unit"></asp:RequiredFieldValidator></sup></label>

                                            <asp:DropDownList ID="ddl_Secondry_Unit" runat="server" class="form-control find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_Secondry_Unit_SelectedIndexChanged"></asp:DropDownList>


                                        </div>
                                        <div class="col-md-6">
                                            <label for="validationCustom01" class="form-label" style="width: 100%;">
                                                Enter Unit Conversion  <sup>*</sup></label>

                                            <asp:Label ID="lbl_primaryunit" runat="server" Style="float: left;"></asp:Label>
                                            <asp:TextBox ID="txt_unit_convertiontxt_unit_convertion" runat="server" class="form-control" Width="80px" Style="width: 80px; float: left; line-height: 1; min-height: 30px!important; font-size: 13px;"></asp:TextBox>
                                            <asp:Label ID="lblsecondryunit" runat="server" Style="float: left;"></asp:Label>

                                        </div>
                                    </asp:Panel>
                                    <asp:GridView ID="grd_muntiple_unit" runat="server" AutoGenerateColumns="false" GridLines="Both" CssClass="table table-bordered" Style="width: 100%;">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDel_unit" runat="server" ToolTip="Delete" CssClass="lnkdelete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_unit_Click"><i class="lni lni-trash"></i></asp:LinkButton>
                                                    <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lblunit_id" runat="server" Text='<%#Bind("unit_id")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lblparent_unit" runat="server" Text='<%#Bind("parent_unit")%>' Visible="false"></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Unit" runat="server" Text='<%#Bind("Unit")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Description" runat="server" Text='<%#Bind("Description")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>



                                <div class="col-md-12">
                                    <asp:Button ID="Btn_Add" runat="server" Text="Add" OnClick="Btn_Add_Click" CssClass="btn btn-primary" ValidationGroup="a" />
                                    <asp:Button ID="Btn_Update" runat="server" Text="Update" OnClick="Btn_Update_Click" class="btn btn-primary" Visible="false" ValidationGroup="a" />
                                    <asp:Button ID="Btn_Cancel" runat="server" Text="Cancel" OnClick="Btn_Cancel_Click" class="btn btn-dark" Visible="true" CausesValidation="false"   />

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <div class="col-xl-12" style="display: none">
                <h6 class="mb-0 text-uppercase">Added Item</h6>
                <hr />
                <div class="card">
                    <div class="card-body">
                        <div class="table-responsive">
                            <p style="text-align: right;">
                                <span style="font-weight: bold;">Search:</span>
                                <input type="text" id="txtSearch" name="txtSearch" placeholder="type search text" maxlength="50" style="height: 25px; font: 100" />
                            </p>
                            <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                <div class="row" id="SearchData">

                                    <br />
                                    <asp:GridView ID="GrdView_Create_Item" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Edit"><i class="lni lni-pencil-alt"></i></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"></i></asp:LinkButton>
                                                    <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lbl_Item_id" runat="server" Text='<%#Bind("Item_id")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lbl_Unit_Id" runat="server" Text='<%#Bind("Unit_id")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lbl_Brand_Id" runat="server" Text='<%#Bind("Brand_id")%>' Visible="false"></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Item_Name" runat="server" Text='<%#Bind("Item_Name")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Brand Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Brand_Name" runat="server" Text='<%#Bind("Brand_name")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Unit Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Unit_Name" runat="server" Text='<%#Bind("Unit")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="HSN">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_HSN" runat="server" Text='<%#Bind("HSN")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="GST">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_GST" runat="server" Text='<%#Bind("GST")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Item Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Item_type" runat="server" Text='<%#Bind("Item_type")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Created_Date")%>'></asp:Label>
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
    </div>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
    
</asp:Content>
