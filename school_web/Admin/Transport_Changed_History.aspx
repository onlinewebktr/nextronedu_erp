<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Transport_Changed_History.aspx.cs" Inherits="school_web.Admin.Transport__Changed_History" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server"> Transport Changed History
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
       <script type="text/javascript">
           function PrintPanel() {
               var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
               var printWindow = window.open('', '', 'height=400,width=800');
               printWindow.document.write('<html><head>');
               printWindow.document.write('</head><body>');
               printWindow.document.write('<link href="https://fonts.googleapis.com/css2?family=Dosis:wght@200;300;400;500;600;700;800&family=Montserrat:ital,wght@0,300;0,400;0,500;0,600;0,700;0,800;0,900;1,200;1,300;1,400;1,500;1,600;1,700;1,800;1,900&family=Roboto:ital,wght@0,100;0,300;0,400;0,500;0,700;0,900;1,100;1,300;1,400;1,500;1,700;1,900&display=swap" rel="stylesheet"/><link href="../assets/css/Print.css" rel="stylesheet" type="text/css" /><link href="../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />');
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

        .home-grph-wpr {
            width: 114%;
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
                <div class="breadcrumb-title pe-3"><a href="#" class="backlnk-css"></a>Transportation</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Transport Changed History</li>
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
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="find-dv">
                                                <div class="row">
                                                    <div class="col-sm-1">
                                                        <label for="validationCustom01" class="find-dv-lbl">Session</label>
                                                        <asp:DropDownList ID="ddlsession" runat="server" class="form-control find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddlsession_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Class</label>
                                                       <asp:DropDownList ID="ddlclass" runat="server" class="form-control find-dv-txtbx" OnSelectedIndexChanged="ddlclass_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Section</label>
                                                        <asp:DropDownList ID="ddl_section" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>
                                                    </div>
                                                  
                                                    <div class="col-sm-1">
                                                            <asp:Button ID="btn_find" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_find_Click" />
                                                    </div>

                                                    <div class="col-sm-1">
                                                      OR
                                                    </div>

                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Admission No</label>
                                                        <asp:TextBox ID="txt_admission_no" runat="server"></asp:TextBox>
                                                    </div>
                                                  
                                                    <div class="col-sm-1">
                                                            <asp:Button ID="btn_find_admission_no" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_find_admission_no_Click" />
                                                    </div>



                                                    <div class="col-sm-2">
                                                        <asp:LinkButton ID="btn_excels" runat="server" OnClick="btn_excels_Click" class="btn btn-primary find-dv-btn">  <i class='bx bx-download'></i> Excel</asp:LinkButton>
                                                        <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" CssClass="btn btn-primary find-dv-btn" Style="margin-left: 10px;" runat="server"
                                                            ToolTip="Print"><i class='bx bx-printer'></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="grd-wpr">
                                                <div id="tblPrintIQ" runat="server">
                                                    <div class="prnt-dv-wpr">
                                                        <div id="content">

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
                                                                    <div style="display: none; margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                        Email Id. :<asp:Label ID="lbl_emaiid" runat="server"></asp:Label>

                                                                        &nbsp;&nbsp;

                            website :
                            <asp:Label ID="lbl_website" runat="server"></asp:Label>
                                                                    </div>
                                                                    <div style="display: none; margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                        Contact Details :<asp:Label ID="lbl_contact_details" runat="server"></asp:Label>


                                                                    </div>
                                                                    <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                        <span style="font-size: 14px; font-weight: bold;">Transport Changed History<asp:Label ID="lbl_class22" runat="server"></asp:Label></span>


                                                                    </div>
                                                                </div>


                                                            </div>


                                                            <asp:GridView ID="GrdView" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%"  >
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Sl No.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Name">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label2" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Adm. No.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label6" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Class">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    
                                                                    <asp:TemplateField HeaderText="Section">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Roll No.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_rollnumber" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                       <asp:TemplateField HeaderText="Assined Date">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_Assined_Date" runat="server" Text='<%#Bind("Created_date")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Transport Name(Old)">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_Pathname" runat="server" Text='<%#Bind("Pathnameold")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                   <asp:TemplateField HeaderText="Transport Name(New)">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_Pathnamenew" runat="server" Text='<%#Bind("Pathname_Pathnamenew")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Old Month">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_Month_name" runat="server" Text='<%#Bind("Old_Monthname")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="New Month">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_Month_name" runat="server" Text='<%#Bind("Month_name")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Changed Date">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_Created_datenew" runat="server" Text='<%#Bind("Change_date")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Changed Type">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_changetypedatenew" runat="server" Text='<%#Bind("Changed_type")%>'></asp:Label>
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
            </div>
        </div>
        <!--end row-->
    </div>
</asp:Content>
