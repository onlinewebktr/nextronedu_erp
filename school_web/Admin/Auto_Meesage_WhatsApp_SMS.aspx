<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Auto_Meesage_WhatsApp_SMS.aspx.cs" Inherits="school_web.Admin.Auto_Meesage_WhatsApp_SMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    SMS/WhatsApp Setting
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .buttons-print {
            display: none;
        }

        #notification {
            z-index: 99999999999 !important;
        }

        .find-dv-lbl {
            margin: 0px;
            padding: 0px;
            width: 100%;
            float: left;
            font-size: 12px;
            color: #000;
        }

        .logo-icon {
            width: 54px;
            height: 55px;
        }

        img {
            height: 13px;
            width: 13px;
            margin-right: 12px;
        }

        .btn-group, .btn-group-vertical {
            position: relative;
            display: inline-flex;
            vertical-align: middle;
            display: none;
        }
    </style>
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

        @media print {
            .noPrint {
                display: none;
            }
        }

        .modal {
            position: fixed;
            top: 0;
            left: 0;
            z-index: 1050;
            display: none;
            width: 100%;
            height: 100%;
            overflow: hidden;
            outline: 0;
            background: #00000052 !important;
        }

        .buttons-print {
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
                <div class="breadcrumb-title pe-3">SMS</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">SMS/WhatsApp Auto Send Setting  </li>
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
                            <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                <div class="row">
                                    <div class="find-dv">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <asp:LinkButton ID="btn_excels" Visible="false" runat="server" Style="margin: 0px 1px 6px 15px !important; float: right;"
                                                    OnClick="btn_excels_Click" class="btn btn-primary find-dv-btn">  <i class='bx bx-download'></i> Excel</asp:LinkButton>
                                                <asp:LinkButton ID="print11" Visible="false" OnClientClick="return PrintPanel()" Style="margin: 0px 0px 6px 10px !important; float: right;" CssClass="btn btn-primary find-dv-btn" runat="server"
                                                    ToolTip="Print"><i class='bx bx-printer'></i></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
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
                                                                <span style="font-size: 14px; font-weight: bold;">SMS/WhatsApp Auto Setting<asp:Label ID="lbl_class22" runat="server"></asp:Label></span>


                                                            </div>
                                                        </div>


                                                    </div>

                                                    <asp:Panel ID="Panel1" runat="server">
                                                        <table id="example2" class="table table-bordered dataTable" role="grid" aria-describedby="example2_info" style="border: 1px solid #000;">
                                                            <thead>
                                                                <tr>
                                                                    <th>#</th>
                                                                    <th>Message Type</th>
                                                                    <th>Template</th>
                                                                    <th>Variable</th>
                                                                    <th>Is Auto SMS</th>
                                                                    <th>Is Auto WhatsApp  </th>

                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <asp:Repeater ID="rd_view" runat="server" OnItemDataBound="rd_view_ItemDataBound">
                                                                    <ItemTemplate>

                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lbl_Send_From" runat="server" Text='<%#Bind("Send_From")%>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lbl_SMS_Tempate" runat="server" Style="word-break: break-all" Text='<%#Bind("SMS_Tempate")%>'></asp:Label>
                                                                            </td>

                                                                             <td style="text-align: left;">
                                                                                <asp:Label ID="lbl_Variable" runat="server" Style="word-break: break-all" Text='<%#Bind("VariableName")%>'></asp:Label>
                                                                            </td>

                                                                            <td style="text-align: left;">
                                                                                <asp:CheckBox ID="chk_autosms" runat="server" />
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:CheckBox ID="chk_auto_whatsaap" runat="server" />
                                                                                <asp:Label ID="lbl_id" runat="server" Visible="false" Style="word-break: break-all" Text='<%#Bind("Id")%>'></asp:Label>
                                                                                <asp:Label ID="lbl_Is_Send_SMS" Visible="false" runat="server" Style="word-break: break-all" Text='<%#Bind("Is_Send_SMS")%>'></asp:Label>
                                                                                <asp:Label ID="lbl_Is_Send_WhatsApp" Visible="false" runat="server" Style="word-break: break-all" Text='<%#Bind("Is_Send_WhatsApp")%>'></asp:Label>
                                                                            </td> 
                                                                        </tr> 
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </tbody>
                                                        </table> 
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                        </div>
                                        <asp:Button ID="btn_update" runat="server" Text="Update" class="noPrint btn btn-danger find-dv-btn pdispnone" OnClick="btn_update_Click" OnClientClick="return confirm('Are you sure want to submit?');" Style="float: right;" />
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
