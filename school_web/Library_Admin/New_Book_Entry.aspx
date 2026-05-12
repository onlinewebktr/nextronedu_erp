<%@ Page Title="" Language="C#" MasterPageFile="~/Library_Admin/Library_Master.Master" AutoEventWireup="true" CodeBehind="New_Book_Entry.aspx.cs" Inherits="school_web.Library_Admin.New_Book_Entry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    New Book Entry
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
    <script>


        function printpage() {
            var panel = document.getElementById("<%=Panel2.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="https://fonts.googleapis.com/css2?family=Dosis:wght@200;300;400;500;600;700;800&family=Montserrat:ital,wght@0,300;0,400;0,500;0,600;0,700;0,800;0,900;1,200;1,300;1,400;1,500;1,600;1,700;1,800;1,900&family=Roboto:ital,wght@0,100;0,300;0,400;0,500;0,700;0,900;1,100;1,300;1,400;1,500;1,700;1,900&display=swap" rel="stylesheet"/><link href="../assets/css/Print.css" rel="stylesheet" type="text/css" />');
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

        #pageFooter {
            display: none;
        }
    </style>
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
                <div class="breadcrumb-title pe-3">Book Management Master</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Lib_Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Book Management Master</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-4">
                    <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text="Book Entry"></asp:Label>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation" novalidate="">

                                    <div class="col-sm-12">
                                        <label for="validationCustom01" class="form-label">Type<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="DropDownList1"></asp:RequiredFieldValidator></sup></label>
                                        <asp:DropDownList ID="DropDownList1" runat="server" class="form-control" AutoPostBack="true"></asp:DropDownList>



                                    </div>
                                    <div class="col-sm-12">
                                        <label for="validationCustom01" class="form-label">Book Status<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="DropDownList2"></asp:RequiredFieldValidator></sup></label>
                                        <asp:DropDownList ID="DropDownList2" runat="server" class="form-control"></asp:DropDownList>
                                    </div>
                                    <div class="col-sm-12">
                                        <label for="validationCustom01" class="form-label">Select Class<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="DropDownList3"></asp:RequiredFieldValidator></sup></label>
                                        <asp:DropDownList ID="DropDownList3" runat="server" class="form-control"></asp:DropDownList>
                                    </div>
                                    <div class="col-sm-12">
                                        <label for="validationCustom01" class="form-label">Subject<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="DropDownList4"></asp:RequiredFieldValidator></sup></label>
                                        <asp:DropDownList ID="DropDownList4" runat="server" class="form-control"></asp:DropDownList>
                                    </div>
                                    <div class="col-sm-12">
                                        <label for="validationCustom01" class="form-label">Name Of Book<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator5" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="TextBox1"></asp:RequiredFieldValidator></sup></label>
                                        <asp:TextBox ID="TextBox1" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-12">
                                        <label for="validationCustom01" class="form-label">Author Name<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator6" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="TextBox2"></asp:RequiredFieldValidator></sup></label>
                                        <asp:TextBox ID="TextBox2" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-12">
                                        <label for="validationCustom01" class="form-label">Publication<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator7" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="TextBox3"></asp:RequiredFieldValidator></sup></label>
                                        <asp:TextBox ID="TextBox3" runat="server" class="form-control"></asp:TextBox>

                                    </div>
                                    <div class="col-sm-12">
                                        <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged" AutoPostBack="true" />
                                        <label for="validationCustom01" class="form-label">If Volume More Than One</label>
                                    </div>
                                    <asp:Panel ID="Panel3" runat="server">
                                        <div class="col-sm-12">
                                            <label for="validationCustom01" class="form-label">Enter Volume Part<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator17" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="TextBox13"></asp:RequiredFieldValidator></sup></label>
                                            <asp:TextBox ID="TextBox13" runat="server" class="form-control"></asp:TextBox>

                                        </div>
                                    </asp:Panel>
                                    <asp:Panel ID="Panel4" runat="server">
                                        <div class="col-sm-12">
                                            <label for="validationCustom01" class="form-label">Start Volume Range<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator18" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="TextBox8"></asp:RequiredFieldValidator></sup></label>
                                            <asp:TextBox ID="TextBox8" runat="server" class="form-control"></asp:TextBox>

                                        </div>
                                        <div class="col-sm-12">
                                            <label for="validationCustom01" class="form-label">End Volume Range<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator19" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="TextBox14"></asp:RequiredFieldValidator></sup></label>
                                            <asp:TextBox ID="TextBox14" runat="server" class="form-control"></asp:TextBox>

                                        </div>
                                    </asp:Panel>
                                    <div class="col-sm-12">
                                        <label for="validationCustom01" class="form-label">Edition<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator8" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="TextBox4"></asp:RequiredFieldValidator></sup></label>
                                        <asp:TextBox ID="TextBox4" runat="server" class="form-control"></asp:TextBox>

                                    </div>
                                    <div class="col-sm-12">
                                        <label for="validationCustom01" class="form-label">Publication Year<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator9" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="TextBox5"></asp:RequiredFieldValidator></sup></label>
                                        <asp:TextBox ID="TextBox5" runat="server" class="form-control"></asp:TextBox>

                                    </div>
                                    <div class="col-sm-12">
                                        <label for="validationCustom01" class="form-label">No.Of Pages<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator10" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="TextBox6"></asp:RequiredFieldValidator></sup></label>
                                        <asp:TextBox ID="TextBox6" runat="server" class="form-control"></asp:TextBox>

                                    </div>
                                    <div class="col-sm-12">
                                        <label for="validationCustom01" class="form-label">Enter Quantity<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator11" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="TextBox7"></asp:RequiredFieldValidator></sup></label>
                                        <asp:TextBox ID="TextBox7" runat="server" class="form-control"></asp:TextBox>

                                    </div>
                                    <div class="col-sm-12">
                                        <label for="validationCustom01" class="form-label">Location<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator12" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="DropDownList5"></asp:RequiredFieldValidator></sup></label>
                                        <asp:DropDownList ID="DropDownList5" runat="server" class="form-control"></asp:DropDownList>

                                    </div>
                                    <div class="col-sm-12">
                                        <label for="validationCustom01" class="form-label">ISBN Num<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator13" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="TextBox9"></asp:RequiredFieldValidator></sup></label>
                                        <asp:TextBox ID="TextBox9" runat="server" class="form-control"></asp:TextBox>

                                    </div>
                                    <div class="col-sm-12">
                                        <label for="validationCustom01" class="form-label">Invoice No.<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator14" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="TextBox10"></asp:RequiredFieldValidator></sup></label>
                                        <asp:TextBox ID="TextBox10" runat="server" class="form-control"></asp:TextBox>

                                    </div>
                                    <div class="col-sm-12">
                                        <label for="validationCustom01" class="form-label">Price<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator15" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="TextBox11"></asp:RequiredFieldValidator></sup></label>
                                        <asp:TextBox ID="TextBox11" runat="server" class="form-control"></asp:TextBox>

                                    </div>
                                    <div class="col-sm-12">
                                        <label for="validationCustom01" class="form-label">Note<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator16" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="TextBox12"></asp:RequiredFieldValidator></sup></label>
                                        <asp:TextBox ID="TextBox12" runat="server" class="form-control"></asp:TextBox>
                                    </div>

                                    <div class="col-12">
                                        <asp:Button ID="btn_Submit" runat="server" Text="Add" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btn_Submit_Click1" />
                                        <asp:Button ID="btn_cancel" runat="server" Text="Cancel" class="btn btn-dark" Visible="false" CausesValidation="false" OnClick="btn_cancel_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


                <div class="col-xl-8">
                    <h6 class="mb-0 text-uppercase">Book List</h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                    <div class="row">
                                        <div class="row">
                                            <div class="col-sm-2">


                                                <asp:Button ID="Btnprint" runat="server" Text="Print" CssClass="btn btn-primary find-dv-btn " OnClientClick="return printpage();" Visible="False" />

                                            </div>
                                            <div class=" col-sm-2">
                                                <asp:Button ID="Btnexcel" runat="server" Text="Excel" CssClass="btn btn-primary find-dv-btn" OnClick="Btnexcel_Click" Visible="False" />
                                            </div>

                                            <div class=" col-sm-2">
                                                <asp:Button ID="Button1" runat="server" Text="Find" CssClass="btn btn-primary find-dv-btn" OnClick="Button1_Click" Visible="False" />
                                            </div>

                                            <div class="col-sm-2">
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="TextBox16" runat="server" class="form-control" Visible="False"></asp:TextBox>

                                            </div>
                                        </div>
                                        <br />
                                        <asp:Panel ID="Panel2" runat="server">
                                            <div class="pgslry-head-div head">

                                                <div style="margin: 0px; padding: 0px; height: 110px; width: 20%; float: left;">
                                                    <asp:Image ID="imglogo" runat="server" Style="height: 100px; width: 100px; margin: 0px 0px 0px 10px;" />
                                                </div>
                                                <div style="margin: 0px; padding: 0px; height: 110px; width: 80%; float: left;">
                                                    <h1 style="margin: 0px 0px 0px 0px; width: 100%; padding: 0px; font-weight: bold; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 20px; text-decoration: underline;">
                                                        <asp:Label ID="lbl_heading" runat="server"></asp:Label>


                                                    </h1>

                                                    <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                        <asp:Label ID="lbl_address" runat="server"></asp:Label>


                                                    </div>
                                                    <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                        Email Id. :<asp:Label ID="lbl_emaiid" runat="server"></asp:Label>

                                                        &nbsp;&nbsp;

                            website :
                            <asp:Label ID="lbl_website" runat="server"></asp:Label>
                                                    </div>
                                                    <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                        Contact Details :<asp:Label ID="lbl_contact_details" runat="server"></asp:Label>


                                                    </div>
                                                </div>


                                            </div>
                                            <div class="col-sm-12">
                                                <table id="example21" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                    <thead>
                                                        <tr>
                                                            <th>#</th>

                                                            <th>Subject</th>
                                                            <th>Name Of Book</th>
                                                            <th>Author Name</th>
                                                            <th>Publication</th>
                                                            <th>Enter Quantity</th>
                                                            <th>Location</th>
                                                            <th>ISBN Num</th>
                                                            <th>Invoice no.</th>
                                                            <th>Price</th>

                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="rd_view" runat="server">
                                                            <ItemTemplate>
                                                                <asp:Panel ID="Panel1" runat="server">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                        </td>

                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="Label3" runat="server" Text='<%#Bind("Subject")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="Label4" runat="server" Text='<%#Bind("NameOfBook")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="Label5" runat="server" Text='<%#Bind("AuthorName")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_Publication" runat="server" Text='<%#Bind("Publication")%>'></asp:Label>
                                                                        </td>

                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="Label12" runat="server" Text='<%#Bind("EnterQuantity")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="Label13" runat="server" Text='<%#Bind("Location")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="Label14" runat="server" Text='<%#Bind("ISBN_Num")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="Label15" runat="server" Text='<%#Bind("InvoiceNo")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="Label16" runat="server" Text='<%#Bind("Price")%>'></asp:Label>
                                                                        </td>

                                                                        <td style="text-align: left;">
                                                                            <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Edit"> <i class="lni lni-pencil-alt"> </i></asp:LinkButton>
                                                                            <asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                                            <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                            <asp:Label ID="lbl_BookId" runat="server" Text='<%#Bind("BookId")%>' Visible="false"></asp:Label>

                                                                        </td>




                                                                    </tr>
                                                                </asp:Panel>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </asp:Panel>
                                        <div class="col-sm-6">
                                            <asp:GridView ID="GrdView" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("Type")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Book Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_sl" runat="server" Text='<%#Bind("BookStatus")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Class">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("SelectClass")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Subject">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_sl" runat="server" Text='<%#Bind("Subject")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Name Of Book">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("NameOfBook")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Author Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_sl" runat="server" Text='<%#Bind("AuthorName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Publication">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("Publication")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Enter Volume Part">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("EnterVolumePart")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Start Volume Range">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_sl" runat="server" Text='<%#Bind("StartVolumeRange")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="End Volume Range">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("EndVolumeRange")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Edition">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_sl" runat="server" Text='<%#Bind("Edition")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Publication Year">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("PublicationYear")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="No Of Pages">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_sl" runat="server" Text='<%#Bind("NoOfPages")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Quantity">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("EnterQuantity")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Location">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("Location")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ISBN_NUM)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_sl" runat="server" Text='<%#Bind("ISBN_Num")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Invoice No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("InvoiceNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Price">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("Price")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Note">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("Note")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_action" runat="server" Text=''></asp:Label>
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
        <!--end row-->
    </div>
</asp:Content>
