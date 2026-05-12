<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="discount-reports.aspx.cs" Inherits="school_web.Admin.discount_reports" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Discount Report
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="../assets/css/Print.css" rel="stylesheet" type="text/css" />');
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
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a></li>
                            <li class="breadcrumb-item active" aria-current="page">Discount Report</li>
                        </ol>
                    </nav>
                </div>
            </div>
            <div class="row">
                <div class="col-xl-12">
                    <div class="ints-loader-wpr" id="intsLoader">
                        <div class="ints-loader-wpr-inr">
                            <div class="ints-loader">
                                <p class="ints-loader-txt">
                                    <img src="../assets/images/icons/loader-ico.gif" class="ints-loader-img" />
                                    <asp:Label ID="lblmessage" runat="server"></asp:Label>
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xl-12">
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="find-dv">
                                <div class="row  g-3 needs-validation">
                                    <div class="col-sm-5">
                                        <div class="row">
                                            <div class="col-sm-4">
                                                <label for="validationCustom01" class="find-dv-lbl">Session</label>
                                                <asp:DropDownList ID="ddl_session" runat="server" class="form-select find-dv-txtbx txtbx-ddl-style"></asp:DropDownList>
                                            </div>
                                            <div class="col-sm-4">
                                                <label for="validationCustom01" class="find-dv-lbl">Class</label>
                                                <asp:DropDownList ID="ddl_class" runat="server" class="form-select find-dv-txtbx txtbx-ddl-style" AutoPostBack="true" OnSelectedIndexChanged="ddl_class_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                            <div class="col-sm-4">
                                                <label for="validationCustom01" class="find-dv-lbl">Section</label>
                                                <asp:DropDownList ID="ddl_section" runat="server" class="form-select find-dv-txtbx txtbx-ddl-style"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-sm-3">
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <label for="validationCustom01" class="find-dv-lbl">From Date</label>
                                                <asp:TextBox ID="txt_from_date" runat="server" class="form-control find-dv-txtbx datepicker txtbx-ddl-style"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-6">
                                                <label for="validationCustom01" class="find-dv-lbl">To Date</label>
                                                <asp:TextBox ID="txt_to_date" runat="server" class="form-control find-dv-txtbx datepicker txtbx-ddl-style"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-sm-4">
                                        <asp:Button ID="btn_find" OnClick="btn_find_Click" class="btn btn-primary find-dv-btn" runat="server" Text="Find Discount" />
                                         
                                        <asp:LinkButton ID="lnk_excel" OnClick="lnk_excel_Click" OnClientClick="return PrintPanel()" class="btn btn-primary find-dv-btn" Style="margin-left: 10px;" runat="server"
                                            ToolTip="Print"><i class='bx bx-download'></i>Excel</asp:LinkButton>

                                        <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" class="btn btn-primary find-dv-btn" Style="margin-left: 10px;" runat="server"
                                            ToolTip="Print"><i class='bx bx-printer'></i> Print</asp:LinkButton>
                                    </div>
                                </div>
                            </div>


                            <div class="grd-wpr" id="tblCustomers">
                                <div class="col-sm-12">
                                    <div id="tblPrintIQ" runat="server">
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
                                                    &nbsp;&nbsp;  website :
                                                                    <asp:Label ID="lbl_website" runat="server"></asp:Label>
                                                </div>
                                                <div style="display: none; margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                    Contact Details :<asp:Label ID="lbl_contact_details" runat="server"></asp:Label>
                                                </div>
                                                <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                    <span style="font-size: 14px; font-weight: bold;">Discount report for -
                                                                        <asp:Label ID="lbl_class22" runat="server"></asp:Label></span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="prnt-dv-wpr printborder">
                                            <asp:Panel ID="Panel1" runat="server">
                                                <table id="datatable" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                    <thead>
                                                        <tr>
                                                            <th>#</th>
                                                            <th>Adm. No.</th>
                                                            <th>Student's Name</th>
                                                            <th>Class</th>
                                                            <th>Section</th>
                                                            <th>Roll No.</th>
                                                            <th>Father's Name</th>
                                                            <th>Mobile No.</th>
                                                            <%--<th>Total Fee</th>--%>
                                                            <th>Discount Amt.</th>
                                                            <th>Date</th>
                                                            <%--<th>Bill No.</th>--%>
                                                            <th>Discount By</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="rd_view" runat="server">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lbl_admission_no" runat="server" Text='<%#Bind("Admission_no")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label2" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lbl_class_name" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lbl_rollnumber" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lbl_fathername" runat="server" Text='<%#Bind("fathername")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label4" runat="server" Text='<%#Bind("mobilenumber")%>'></asp:Label>
                                                                    </td>
                                                                    <%--<td>
                                                                        <asp:Label ID="lbl_bill_amt" runat="server" Text='<%#Bind("Bill_amt")%>'></asp:Label>
                                                                    </td>--%>
                                                                    <td>
                                                                        <asp:Label ID="lbl_discount_amt" runat="server" Text='<%#Bind("Discount_amt")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label3" runat="server" Text='<%#Bind("Created_date")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lbl_discount_by" runat="server" Text='<%#Bind("Discount_by")%>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </tbody>
                                                    <tr>
                                                        <td colspan="8" style="text-align: right; font-weight: 700;">Total</td>
                                                        <td colspan="3" style="font-weight: 700;">
                                                            <asp:Label ID="lbl_total_disc" runat="server"></asp:Label></td>
                                                    </tr>
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
        <!--end row-->
    </div>
</asp:Content>
