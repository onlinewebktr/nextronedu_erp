<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Sheet_Mapping_with_Bus_N.aspx.cs" Inherits="school_web.Admin.Sheet_Mapping_with_Bus_N" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Create Seat
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
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
                <div class="breadcrumb-title pe-3">Transportation</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Create Seat</li>
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
                                    <div class="col-md-4">
                                        <label for="validationCustom01" class="form-label">Vehicle<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_select_buss" runat="server" class="form-control" OnSelectedIndexChanged="ddl_select_buss_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-4">
                                        <label for="validationCustom01" class="form-label">Vehicle Route<sup>*</sup></label>


                                        <asp:DropDownList ID="ddl_path_root" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_path_root_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-md-4">
                                        <label for="validationCustom01" class="form-label">No of Seat<sup>*</sup></label>
                                        <asp:TextBox ID="txt_no_sheet" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>


                                    </div>

                                    <div class="col-md-12">
                                        <div class="fnd-box-row-wpr" id="add_sheet" runat="server" visible="false"> 
                                            <div class="row">



                                                <div class="col-md-2">
                                                    <label for="validationCustom01" class="form-label" style="margin-top: 5px;">Seat Position<sup>*</sup></label>
                                                    <asp:DropDownList ID="ddl_sheet_position" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_sheet_position_SelectedIndexChanged">
                                                        <asp:ListItem>Select</asp:ListItem>
                                                        <asp:ListItem>Left</asp:ListItem>
                                                        <asp:ListItem>Right</asp:ListItem>
                                                        <asp:ListItem>Back</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-2">
                                                    <label for="validationCustom01" class="form-label" style="margin-top: 5px;">Seat Model<sup>*</sup></label>
                                                    <asp:DropDownList ID="ddl_seat_modle" runat="server" class="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-2">
                                                    <label for="validationCustom01" class="form-label" style="margin-top: 5px;">Seat Type<sup>*</sup></label>
                                                    <asp:DropDownList ID="ddl_seattype" runat="server" class="form-control">
                                                        <asp:ListItem>Select</asp:ListItem>
                                                        <asp:ListItem>Boy</asp:ListItem>
                                                        <asp:ListItem>Girl</asp:ListItem>
                                                        <asp:ListItem>Staff</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>

                                                <div class="col-md-3">
                                                    <label for="validationCustom01" class="form-label" style="margin-top: 5px;">Enter Number of Seat<sup>*</sup></label>
                                                    <asp:TextBox ID="txt_sheet_name" runat="server" class="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                </div>



                                                <div class="col-12">
                                                    <asp:Button ID="btn_Submit" runat="server" Text="Add" CssClass="btn btn-primary" Style="margin-top: 10px;" ValidationGroup="a" OnClick="btn_Submit_Click" />

                                                </div>

                                                <div class="col-12">
                                                    <asp:GridView ID="GrdView" runat="server" class="mb-0 table table-bordered" AutoGenerateColumns="False" Width="100%" Style="margin-top: 10px;">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sl No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Seat Position">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_sheet_position" runat="server" Text='<%#Bind("Sheet_position")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Seat Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Seat_Type" runat="server" Text='<%#Bind("Seat_Type")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Seat Model">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Seat_Model" runat="server" Text='<%#Bind("Seat_Model")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>



                                                            <asp:TemplateField HeaderText="Seat Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_sheetno" runat="server" Text='<%#Bind("Sheet_no")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Action">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lbl_Sheet_Status" runat="server" Text='<%#Bind("Sheet_Status")%>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lbl_Sheet_Id" runat="server" Text='<%#Bind("Sheet_Id")%>' Visible="false"></asp:Label>
                                                                    <asp:LinkButton ID="lnkEdit" runat="server" CssClass="mb-2 mr-2 btn btn-warning" OnClick="lnkEdit_Click" CausesValidation="false" Visible="false">Edit</asp:LinkButton>
                                                                    <asp:LinkButton ID="lnkDel" runat="server" CssClass="mb-2 mr-2 btn btn-danger" OnClick="lnkDel_Click" OnClientClick="return confirm('Are you sure want to delete?');" CausesValidation="false">Delete</asp:LinkButton>

                                                                    <asp:Label ID="lbl_rowname" Visible="false" runat="server" Text='<%#Bind("Row")%>'></asp:Label>
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
                </div>



            </div>
        </div>
        <!--end row-->
    </div>

    <!--end page wrapper -->
    <asp:HiddenField ID="hd_id" runat="server" />
</asp:Content>
