<%@ Page Title="" Language="C#" MasterPageFile="~/Library_Admin/Library_Master.Master" AutoEventWireup="true" CodeBehind="New_Book_Entry_N.aspx.cs" Inherits="school_web.Library_Admin.New_Book_Entry_N" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    New Book Entry
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .clndr-icon {
            font-size: 14px !important;
            color: #ff2956;
            position: absolute;
            top: 18px;
            right: 22px;
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
    <script type="text/javascript">
        $(function () {
            $("#<%=txt_purchasedate.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                readOnly: true,
                yearRange: "1900:2100",
            }).attr("readonly", "true");
        });
    </script>


    <script type="text/javascript">
        function Confirm() {

            var confirm_value
            var isSubmitted = false;
            confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";

            if (confirm("Do you want to submit ?")) {
                confirm_value.value = "Yes";

            }
            else {
                confirm_value.value = "No";
            }

            document.forms[0].appendChild(confirm_value);
        }


        function save_data() {
            var valsubmit = $('#<%=btn_Submit_final.ClientID %>').val();
            if (valsubmit == "Final Submit") {

                $('#<%=btn_Submit_final.ClientID %>').val('Submitting.. Please Wait..');

                Confirm();
                document.getElementById("<%=btn_Submit_final.ClientID %>").click();

            }
            else if (valsubmit == "Update") {

                $('#<%=btn_Submit_final.ClientID %>').val('Submitting.. Please Wait..');

                Confirm();
                document.getElementById("<%=btn_Submit_final.ClientID %>").click();

            }


            else {


                alert("Already submitted ")
            }

        }
    </script>
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
                <div class="breadcrumb-title pe-3">Book Management Master</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">New Book Entry</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text=" "></asp:Label>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation" novalidate="">

                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Book Type </label>
                                        <asp:DropDownList ID="ddl_book_type" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Class<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_class" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Book Status<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="ddl_book_status"></asp:RequiredFieldValidator></sup></label>
                                        <asp:DropDownList ID="ddl_book_status" runat="server" class="form-control"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Book Subject<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_book_catogery" runat="server" class="form-control"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Name Of Book<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_name_of_the_book"></asp:RequiredFieldValidator></sup></label>
                                        <asp:TextBox ID="txt_name_of_the_book" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Author Name<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_Author_name"></asp:RequiredFieldValidator></sup></label>
                                        <asp:TextBox ID="txt_Author_name" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <label for="validationCustom01" class="form-label">Publication<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_Publication"></asp:RequiredFieldValidator></sup></label>
                                        <asp:TextBox ID="txt_Publication" runat="server" class="form-control"></asp:TextBox>
                                    </div>

                                    <div class="col-md-2">
                                        <label for="validationCustom01" class="form-label">Location<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator5" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_Publication"></asp:RequiredFieldValidator></sup></label>

                                        <asp:DropDownList ID="ddl_location" AutoPostBack="true" OnSelectedIndexChanged="ddl_location_SelectedIndexChanged" runat="server" class="form-select"></asp:DropDownList>
                                    </div>

                                     <div class="col-md-2">
                                        <label for="validationCustom01" class="form-label">Sub-Location<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator6" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_Publication"></asp:RequiredFieldValidator></sup></label>

                                        <asp:DropDownList ID="ddl_sublocation" runat="server" class="form-select"></asp:DropDownList>
                                    </div>


                                    <div class="row">
                                        <div class="col-md-12">
                                            <table style="margin: 10px 0px 0px 0px; padding: 0px; float: left; height: auto; width: 100%">
                                                <tr>
                                                    <td style="padding: 5px 5px 5px 5px;border-top: 0px dashed #000;background: #d3d3d3;" colspan="6"><b>Volume Information</b></td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px 5px 5px 5px">Volume Name<sub>*</sub>
                                                    </td>
                                                    <td style="padding: 5px 5px 5px 5px" colspan="5">
                                                        <asp:TextBox ID="txt_volumename" runat="server" class="form-control"></asp:TextBox>
                                                    </td>





                                                </tr>

                                                <tr>
                                                    <td style="padding: 5px 5px 5px 5px">Edition<sub>*</sub>
                                                    </td>
                                                    <td style="padding: 5px 5px 5px 5px">
                                                        <asp:TextBox ID="txt_edition" runat="server" class="form-control"></asp:TextBox>
                                                    </td>

                                                    <td style="padding: 5px 5px 5px 5px">Publication Year<sub>*</sub>
                                                    </td>
                                                    <td style="padding: 5px 5px 5px 5px">
                                                        <asp:DropDownList ID="ddl_publication_year" runat="server" class="form-control"></asp:DropDownList>
                                                    </td>


                                                    <td style="padding: 5px 5px 5px 5px">No.Of Pages<sub>*</sub>
                                                    </td>
                                                    <td style="padding: 5px 5px 5px 5px">
                                                        <asp:TextBox ID="txt_no_of_pages" runat="server" class="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                    </td>
                                                </tr>


                                                <tr>
                                                    <td style="padding: 5px 5px 5px 5px">Quantity<sub>*</sub>
                                                    </td>
                                                    <td style="padding: 5px 5px 5px 5px">
                                                        <asp:TextBox ID="txt_qty" runat="server" class="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                    </td>

                                                    <td style="padding: 5px 5px 5px 5px">ISBN No.<sub>*</sub>
                                                    </td>
                                                    <td style="padding: 5px 5px 5px 5px">
                                                        <asp:TextBox ID="txt_isbn" runat="server" class="form-control"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px 5px 5px 5px">Book Security Code. 
                                                    </td>
                                                    <td style="padding: 5px 5px 5px 5px">
                                                        <asp:TextBox ID="txt_Book_Security_Code" runat="server" class="form-control"></asp:TextBox>
                                                    </td>
                                                </tr>


                                                <tr>
                                                    <td style="padding: 5px 5px 5px 5px">Invoice No. 
                                                    </td>
                                                    <td style="padding: 5px 5px 5px 5px">
                                                        <asp:TextBox ID="txt_Invoice_No" runat="server" class="form-control"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px 5px 5px 5px">Price<sub>*</sub>
                                                    </td>
                                                    <td style="padding: 5px 5px 5px 5px">
                                                        <asp:TextBox ID="txt_Price" runat="server" class="form-control"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px 5px 5px 5px">Purchase Date <sub>*</sub>
                                                    </td>
                                                    <td style="padding: 5px 5px 5px 5px; position: relative">
                                                        <asp:TextBox ID="txt_purchasedate" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                        <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                                    </td>



                                                </tr>

                                                <tr>
                                                    <td style="padding: 5px 5px 5px 5px" colspan="6">
                                                        <p style="color: red; font-size: 11px;">If Volume More than One then you will  add multiple volume</p>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px 5px 5px 5px" colspan="6">
                                                        <asp:Button ID="btn_add_submit_add_book_item" runat="server" Text="Add Book Item" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btn_add_submit_add_book_item_Click" />

                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td colspan="6" style="padding: 5px 5px 5px 5px">

                                                        <asp:GridView ID="GrdView" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="#">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Volume Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_EnterVolumePart" runat="server" Text='<%#Bind("Volumename")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderText="Edition">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_edition" runat="server" Text='<%#Bind("edition")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Publication Year">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_publication_year" runat="server" Text='<%#Bind("publication_year")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="No. Of Pages">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_no_of_pages" runat="server" Text='<%#Bind("no_of_pages")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Quantity">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_qty" runat="server" Text='<%#Bind("qty")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Book Security Code">
                                                                    <ItemTemplate>
                                                                        <asp:Label  ID="lbl_Book_Security_Code" runat="server" Text='<%#Bind("Book_Security_Code")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="ISBN No">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_isbn" runat="server" Text='<%#Bind("isbn")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Invoice No.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Invoice_No" runat="server" Text='<%#Bind("Invoice_No")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Price">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Price" runat="server" Text='<%#Bind("Price")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="purchase Date">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_purchasedate" runat="server" Text='<%#Bind("purchasedate")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Action">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Edit"> <i class="lni lni-pencil-alt"> </i></asp:LinkButton>
                                                                        <asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                                        <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                        <asp:Label ID="lbl_BookId" runat="server" Text='<%#Bind("BookId")%>' Visible="false"></asp:Label>
                                                                        <asp:Label Visible="false" ID="lbl_start_volumerange" runat="server" Text='<%#Bind("start_volumerange")%>'></asp:Label>
                                                                        <asp:Label Visible="false" ID="lbl_end_volumerange" runat="server" Text='<%#Bind("end_volumerange")%>'></asp:Label>
                                                                           
                                                                        
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                            </Columns>
                                                        </asp:GridView>


                                                    </td>
                                                </tr>

                                            </table>
                                        </div>
                                    </div>


                                    <div class="col-3" id="finalsubmitpnl" runat="server" visible="false">

                                        <a onclick="save_data()" class="btn btn-primary" style="width: 155px; margin: 0px 0px 0px 5px;">Final Submit</a>

                                        <asp:Button ID="btn_Submit_final" runat="server" Text="Final Submit" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btn_Submit_final_Click" Style="display: none" />
                                        <asp:Button ID="btn_cancel" runat="server" Text="Cancel" class="btn btn-dark" Visible="false" CausesValidation="false" OnClick="btn_cancel_Click" />
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
    <asp:HiddenField ID="hd_id" runat="server" />
    <asp:HiddenField ID="hd_temp_id" runat="server" />
</asp:Content>
