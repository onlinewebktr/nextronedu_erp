<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="report-mode-headwise-fee-collection.aspx.cs" Inherits="school_web.Admin.report_mode_headwise_fee_collection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Head & Payment Mode wise Monthly Fee Collection
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        tbody, td, tfoot, th, thead, tr {
            vertical-align: top;
        }

        .table-responsive {
            overflow-x: inherit;
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
                <div class="breadcrumb-title pe-3"><a href="fee-report.aspx" class="backlnk-css"><i class="bx bx-arrow-back"></i></a>Report</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Payment Mode & Head wise Fee Collection</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase"></h6>
                    <ul class="sub-pag-menu-ul">
                        <li><a href="report-today-fees-collection-monthly.aspx">Today Monthly Fee Collection Summary</a></li>
                        <li><a href="report-today-fees-collection-monthly.aspx">Today Monthly Fee Collection</a></li>
                        <li><a href="report-headwise-fee-collection-monthly.aspx">Head wise Fee Collection</a></li>
                        <li><a href="report-mode-headwise-fee-collection.aspx" class="sub-mnu-p-a-active">Payment Mode & Head wise Fee Collection</a></li>
                    </ul>
                </div>
                <div class="col-xl-12">
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper2" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="find-dv">
                                                <div class="row">
                                                    <div class="col-sm-4">
                                                        <div class="row">
                                                            <div class="col-sm-5">
                                                                <label for="validationCustom01" class="find-dv-lbl">From Date</label>
                                                                <asp:TextBox ID="txt_s_date" runat="server" class="form-control find-dv-txtbx datepicker"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-5">
                                                                <label for="validationCustom01" class="find-dv-lbl">To Date</label>
                                                                <asp:TextBox ID="txt_e_date" runat="server" class="form-control find-dv-txtbx datepicker"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-2">
                                                                <asp:Button ID="btn_find" runat="server" Text="find" class="btn btn-primary find-dv-btn" OnClick="btn_find_Click" />
                                                            </div>
                                                        </div>
                                                    </div>



                                                    <div class="col-sm-4" style="display: none">
                                                        <div class="row">
                                                            <div class="col-sm-5">
                                                                <label for="validationCustom01" class="find-dv-lbl">Class</label>
                                                                <asp:DropDownList ID="ddl_class" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                            </div>
                                                            <div class="col-sm-5">
                                                                <label for="validationCustom01" class="find-dv-lbl">Section</label>
                                                                <asp:DropDownList ID="ddl_section" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                            </div>

                                                            <div class="col-sm-2">
                                                                <asp:Button ID="btn_find_by_class" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_find_by_class_Click" />
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-1"></div>
                                                    <div class="col-sm-3">
                                                        <asp:LinkButton ID="btn_excels" runat="server" OnClick="btn_excels_Click" Style="margin-left: 10px;" class="btn btn-primary find-dv-btn">  <i class='bx bx-download'></i> Excel</asp:LinkButton>
                                                        <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" CssClass="btn btn-primary find-dv-btn" Style="margin-left: 10px;" runat="server"
                                                            ToolTip="Print"><i class='bx bx-printer'></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="grd-wpr">
                                                <div class="col-sm-12">
                                                    <div id="tblPrintIQ" runat="server">
                                                        <div class="prnt-dv-wpr printborder">
                                                            <div class="head-printdv" style="border-bottom: 1px solid #000; margin: 0px; float: left; width: 100%;">
                                                                <div style="margin: 0px; padding: 0px; height: 110px; width: 20%; float: left;">
                                                                    <asp:Image ID="imglogo" runat="server" Style="height: 100px; width: 100px; margin: 0px 0px 0px 10px;" />
                                                                </div>
                                                                <div style="margin: 0px; padding: 0px; height: 110px; width: 80%; float: left;">
                                                                    <h1 style="margin: 10px 0px 0px 0px; width: 100%; padding: 0px; font-weight: bold; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 20px; text-decoration: underline;">
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
                                                                        <span style="font-size: 14px; font-weight: bold;">Time Period-<asp:Label ID="lbl_class22" runat="server"></asp:Label></span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <asp:Panel ID="pnl_grid" runat="server" Style="width: 100%;">
                                                                <asp:GridView ID="GrdView" runat="server" class="table table-bordered" ShowFooter="false" AutoGenerateColumns="False" Width="100%" OnRowDataBound="GrdView_RowDataBound">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Sl No.">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_date" runat="server" Text='<%#Bind("Date")%>'></asp:Label>
                                                                                <asp:Label ID="lbl_idate" Visible="false" runat="server" Text='<%#Bind("Idate")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>


                                                                        <asp:TemplateField HeaderText="Payment Mode / Head / Amount">
                                                                            <ItemTemplate>
                                                                                <%-- ===================== --%>
                                                                                <asp:GridView ID="GrdViews" runat="server" ShowHeader="false" OnRowDataBound="GrdViews_RowDataBound" class="table table-bordered" ShowFooter="true" AutoGenerateColumns="False" Width="100%" Style="width: 103%; border-collapse: collapse; margin: -9px;">
                                                                                    <Columns>
                                                                                        <asp:TemplateField ControlStyle-Width="220">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lbl_paymode" runat="server" Visible="false" Text='<%#Bind("mode")%>'></asp:Label>
                                                                                                <asp:Label ID="lbl_idates" Visible="false" runat="server" Text='<%#Bind("idate")%>'></asp:Label>
                                                                                                <asp:Label ID="lbl_paids_amt" Visible="false" runat="server" Text='<%#Bind("Paid_amt")%>'></asp:Label>
                                                                                                <asp:LinkButton ID="lnk_view_details" Style="text-decoration: underline;" OnClick="lnk_view_details_Click" Text='<%#Bind("mode")%>' runat="server"></asp:LinkButton>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>


                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <%-- ===================== --%>
                                                                                                <asp:GridView ID="Grd_head_amounts" runat="server" ShowHeader="false" OnRowDataBound="Grd_head_amounts_RowDataBound" class="table table-bordered" ShowFooter="true" AutoGenerateColumns="False" Width="100%" Style="width: 103%; border-collapse: collapse; margin: -9px;">
                                                                                                    <Columns>
                                                                                                        <asp:TemplateField HeaderStyle-Width="100px" ControlStyle-Width="150">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="lbl_content" runat="server" Text='<%#Bind("Content")%>'></asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                            <FooterTemplate>
                                                                                                                <asp:Label ID="lbltotalamts" runat="server" Style="font-weight: 600" Text="Total Amount"></asp:Label>
                                                                                                            </FooterTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderStyle-Width="100px" ControlStyle-Width="150">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("Paid_amt")%>'></asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                            <FooterTemplate>
                                                                                                                <asp:Label ID="lbltotal" runat="server" Style="font-weight: 600"></asp:Label>
                                                                                                            </FooterTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                    </Columns>
                                                                                                </asp:GridView>
                                                                                            </ItemTemplate>

                                                                                            <FooterTemplate>
                                                                                                <asp:Label ID="lbltotalfgff" runat="server" Text="Grand Total : " Style="font-weight: 600; width: 50%; float: left;"></asp:Label>
                                                                                                <asp:Label ID="lbltotal" runat="server" Style="font-weight: 600; width: 50%; float: left; padding-left: 7px;"></asp:Label>
                                                                                            </FooterTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </asp:Panel>
                                                        </div>
                                                    </div>



                                                    <div class="not-found-dv" runat="server" id="NotFoundS">
                                                        <p>There is no matching data related to your filters.</p>
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



    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" style="line-height: 21px;">Fee Details
                        <br />
                        <asp:Label ID="lbl_pop_info" runat="server" Style="font-size: 12px; line-height: 23px; color: #f00; font-weight: 500;"></asp:Label>
                    </h5>
                    <asp:Button ID="btnclose" runat="server" class="btn btn-secondary" Text="Close" />
                </div>
                <div class="modal-body">
                    <div class="p-4 border rounded" style="max-height: 500px; overflow: auto;">
                        <div class="row g-3 needs-validation" novalidate="">
                            <div class="col-md-12">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                    <div class="poptbl-wpr">
                                        <table id="example2" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                            <thead>
                                                <tr>
                                                    <th>#</th>
                                                    <th>Student name</th>
                                                    <th>Admission No.</th>
                                                    <th>Class</th>
                                                    <th>Section</th>
                                                    <th>Payment Head</th>
                                                    <th>Amount</th>
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
                                                                    <asp:Label ID="lbl_student_name" runat="server" Text='<%#Bind("Student_name")%>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="lbl_adno" runat="server" Text='<%#Bind("adno")%>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="lbl_class_name" runat="server" Text='<%#Bind("Class_name")%>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="lbl_Content" runat="server" Text='<%#Bind("Content")%>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="lbl_paid_amt" runat="server" Text='<%#Bind("paid")%>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </asp:Panel>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function openModal() {
            $('#myModal').modal('show');
        }

        function openModal() {
            $("#myModal").show();
            $('#myModal').addClass('show');
            $('#fadeup').addClass('modal-backdrop fade show');
        }
        function close() {
            $("#myModal").hide();
            $('#myModal').removeClass('show');
            $('#fadeup').removeClass('modal-backdrop fade show');
        }
    </script>


    <style type="text/css">
        .ftrbgcolors {
            background-color: rgb(231 247 143);
        }

        tbody, td, tfoot, th, thead, tr {
            vertical-align: middle;
        }

        .modal {
            background: rgb(0 0 0 / 54%);
        }

        .modal-dialog {
            max-width: 900px;
        }

        .modal-header {
            padding: 0.5rem 1rem;
        }

        .poptbl-wpr {
            margin: 0px;
            padding: 0px;
            width: 100%;
            float: left;
        }

            .poptbl-wpr table {
                width: 100%;
            }

                .poptbl-wpr table tr th {
                    padding: 5px 10px;
                    font-weight: 700 !important;
                    font-size: 13px !important;
                }

                .poptbl-wpr table tr td {
                    padding: 5px 10px;
                    font-size: 13px;
                }
    </style>
</asp:Content>
