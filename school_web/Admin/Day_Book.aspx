<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Day_Book.aspx.cs" Inherits="school_web.Admin.Day_Book" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Day Book
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .ddttxx {
            margin: 0px 0px 0px 0px;
            padding: 0px;
            width: 100%;
            height: auto;
            float: left;
        }

            .ddttxx div {
                width: 100% !important;
            }

        .paddingleft {
            padding: 0px 0px 0px 20px;
        }

        .lnksActvbtns {
            margin: 0px;
            padding: 3px 5px 3px 5px;
            background: #1bb300;
            color: #fff;
            font-size: 14px;
            border-radius: 2px;
            border: 1px solid #189f00;
        }

            .lnksActvbtns:hover {
                margin: 0px;
                padding: 3px 5px 3px 5px;
                background: #1bb300;
                color: #fff;
            }

        .lnksDactvbtns {
            margin: 0px;
            padding: 3px 5px 3px 5px;
            background: #f00;
            color: #fff;
            font-size: 14px;
            border-radius: 2px;
            border: 1px solid #bf0000;
        }

            .lnksDactvbtns:hover {
                margin: 0px;
                padding: 3px 5px 3px 5px;
                background: #f00;
                color: #fff;
            }
    </style>

    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
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
                <div class="breadcrumb-title pe-3"><a href="#" runat="server" id="backbtns" class="backlnk-css">Account</a></div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Day Book</li>
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
                                    <div class="col-sm-12">
                                        <div class="find-dv">
                                            <div class="row">
                                                <div class="col-sm-2">
                                                    <label for="validationCustom01" class="find-dv-lbl">Date</label>
                                                    <asp:TextBox ID="txt_s_date" runat="server" class="form-control find-dv-txtbx datepicker"></asp:TextBox>
                                                </div>


                                                <div class="col-sm-2">
                                                    <asp:Button ID="btn_search" runat="server" Text="Find" CssClass="btn btn-primary find-dv-btn" OnClick="btn_search_Click" Style="margin: 0px;" />

                                                </div>

                                                <div class="col-sm-2">
                                                    <asp:LinkButton ID="btn_excels" Visible="false" runat="server" OnClick="lnk_excel_download_Click" class="btn btn-primary find-dv-btn">  <i class='bx bx-download'></i> Excel</asp:LinkButton>
                                                    <asp:LinkButton ID="print1" Visible="false" OnClientClick="return PrintPanel()" CssClass="btn btn-primary find-dv-btn" Style="margin-left: 10px;" runat="server"
                                                        ToolTip="Print"><i class='bx bx-printer'></i></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="grd-wpr">
                                            <div id="tblPrintIQ" runat="server">
                                                <div class="pgslry-head-div head" style="border-bottom: 1px solid #000; margin: 0px; float: left; width: 100%;">
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
                                                            <span style="font-size: 14px; font-weight: bold;">Day-Book
                                                                <asp:Label ID="lbl_class22" runat="server"></asp:Label></span>


                                                        </div>
                                                    </div>
                                                </div>
                                                <asp:Panel ID="Panel1" runat="server" Visible="false">


                                                    <table id="datatable1" data-page-length='100000' class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                        <thead>
                                                            <tr>
                                                                <th colspan="5" style="text-align: left">
                                                                    <span>Day Book :</span><asp:Label ID="txt_ledger_name" runat="server" Text=""></asp:Label>
                                                                </th>
                                                                <th colspan="4" style="text-align: right">
                                                                    <asp:Label ID="txt_period" runat="server"></asp:Label>


                                                                </th>

                                                            </tr>


                                                            <tr>
                                                                <th style="width:60px;">Sr. No.</th>

                                                                <th>Date</th>
                                                               
                                                                <th style="width: 120px;">Ledger</th>
                                                                 <th>Particular</th>
                                                                <th style="width: 363px;">Remark</th>
                                                                <th>Vouchar Type</th>
                                                                <th >Vouchar No.</th>
                                                                <th>Debit Amount(Inwards)</th>
                                                                <th>Credit(Outwards)</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:Repeater ID="grd_view" runat="server">
                                                                <ItemTemplate>
                                                                    <tr>

                                                                        <td>
                                                                            <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                            <asp:Label ID="lbl_User_name" Visible="false" runat="server" Text='<%#Bind("User_name")%>'></asp:Label>
                                                                        </td>

                                                                        <td>
                                                                            <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                        </td>
                                                                       
                                                                            <td>
                                                                            <asp:Label ID="lbl_Ledger" style="word-break:break-all" runat="server" Text='<%#Bind("accountledger")%>'></asp:Label>
                                                                            
                                                                        </td>
                                                                         <td>
                                                                            <asp:Label ID="lbl_particular" style="word-break:break-all" runat="server" Text='<%#Bind("particular")%>'></asp:Label>
                                                                            
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_Description" style="word-break:break-all" runat="server" Text='<%#Bind("Description")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_VoucherType" runat="server" Text='<%#Bind("VoucherType")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_VoucherNo" runat="server" Text='<%#Bind("VoucherNo")%>'></asp:Label>
                                                                        </td>

                                                                        <td>
                                                                            <asp:Label ID="lbl_Debit" runat="server" Text='<%#Bind("Debit","{0:0.00}")%>'></asp:Label>
                                                                        </td>

                                                                        <td>
                                                                            <asp:Label ID="lbl_Credit" runat="server" Text='<%#Bind("Credit","{0:0.00}")%>'></asp:Label>
                                                                        </td>

                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </tbody>
                                                        <tfoot>

                                                            <tr>
                                                                <td colspan="6"></td>
                                                                <td>Total :
                                            </td>
                                                                <td>
                                                                    <asp:Label ID="txt_total_debit" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="txt_total_credit" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>

                                                        </tfoot>


                                                    </table>
                                                </asp:Panel>



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
