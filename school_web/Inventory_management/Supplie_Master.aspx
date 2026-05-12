<%@ Page Title="" Language="C#" MasterPageFile="~/Inventory_management/Site3.Master" AutoEventWireup="true" CodeBehind="Supplie_Master.aspx.cs" Inherits="school_web.Inventory_management.Supplie_Master" MaintainScrollPositionOnPostback="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Supplier Details
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
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
                        <li class="breadcrumb-item active" aria-current="page">Supplier List </li>
                    </ol>
                </nav>
            </div>
        </div>



        <asp:HiddenField ID="HdID" runat="server" />
        <div class="row">



            <div class="col-xl-12">
                <h6 class="mb-0 text-uppercase"> </h6>
                <hr />
                <div class="card">
                    <div class="card-body">


                        <div style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%">
                            <div style="margin: 0px; padding: 0px; float: left; height: auto; width: 75%">
                                <asp:LinkButton ID="btn_excels" runat="server" OnClick="btn_excels_Click" class="btn btn_excel find-dv-btn">  <i class='bx bx-download'></i> Excel</asp:LinkButton>

                                <input type="button" id="btnPrint" value="Print" onclick="JavaScript:printPartOfPage();" style="display: none; margin: 0px 0px 6px 10px; padding: 5px 0px 5px 5px;" class="btn btn-primary find-dv-btn" />

                                <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" Style="margin: 0px 0px 6px 10px; padding: 5px 0px 5px 5px;" CssClass="btn btn-primary find-dv-btn" runat="server"
                                    ToolTip="Print"><i class='bx bx-printer'></i></asp:LinkButton>

                                <a class="btn btn-success find-dv-btn" href="Create_Supplier.aspx" style="margin: 0px 13px 1px 0px !important; float: left; padding: 3px 6px 6px 11px; float: left; padding: 3px 6px 6px 11px;"
                                    title="Add Supplier"><i class="bx bx-plus"></i></a>
                            </div>

                      
                        <div style="margin: 0px; padding: 0px; float: left; height: auto; width: 25%">
                            <p style="text-align: right;">
                                <span style="font-weight: bold;">Search:</span>
                                <input type="text" id="txtSearch" name="txtSearch" placeholder="type search text" maxlength="50" style="height: 25px; font: 100" />
                            </p>
                        </div>
                              </div>
                        <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                            <div id="tblPrintIQ" runat="server">

                                <div class="pgslry-head-div" style="width: 100%;">
                                    <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;" class="head">
                                        <span style="font-size: 14px; font-weight: bold;">Supplier List </span>


                                    </div>
                                    <div class="row" id="SearchData" style="width: 100%">

                                        <br />

                                        <div class="table-responsive">
                                            <asp:GridView ID="GrdView_Add_Details" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_party_id" runat="server" Text='<%#Bind("party_id")%>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lbl_party_name" runat="server" Text='<%#Bind("party_name")%>'></asp:Label>
                                                            <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lbl_State_Code" runat="server" Text='<%#Bind("State_Code")%>' Visible="false"></asp:Label>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Address">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Address" runat="server" Text='<%#Bind("address")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="City">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_city" runat="server" Text='<%#Bind("city")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="State">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_State" runat="server" Text='<%#Bind("State")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Mobile">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Mobile" runat="server" Text='<%#Bind("mobile")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Care of">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCareof" runat="server" Text='<%#Bind("Care_of")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="GSTIN No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgstin" runat="server" Text='<%#Bind("gstin")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="GST Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgsttype" runat="server" Text='<%#Bind("Registration_Type")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="PAN NO.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblpan_no" runat="server" Text='<%#Bind("pan_no")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Bank Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblbankname" runat="server" Text='<%#Bind("Bank_Name")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Account No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAccount_No" runat="server" Text='<%#Bind("Account_No")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="IFSC Code.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIFSC_Code" runat="server" Text='<%#Bind("IFSC_Code")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEdit" runat="server" CssClass="lnkEdit" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Edit"> <i class="lni lni-pencil-alt"> </i></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkDel" CssClass="lnkdelete" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"> </i></asp:LinkButton>

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
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
    <script src="../assets/js/Custom.js"></script>
</asp:Content>
