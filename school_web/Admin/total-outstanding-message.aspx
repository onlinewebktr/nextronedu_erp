<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="total-outstanding-message.aspx.cs" Inherits="school_web.Admin.total_outstanding_message" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Total Outstanding Message
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


        function openModalDocs() {
            $('#MdlDocumentS').modal('show');

        }
    </script>
    <style>
        .sub-pag-menu-ul li a {
            color: #312F7F;
            border: 1px solid #312F7F;
        }

            .sub-pag-menu-ul li a:hover {
                background: #312F7F;
                color: #fff;
                border: 1px solid #312F7F;
            }

        .sub-mnu-p-a-active {
            background: #312F7F;
            color: #ffffff !important;
        }

        table tr th input[type="checkbox"] {
            background-color: #fff;
            margin: 0;
            font: inherit;
            color: currentColor;
            width: 22px;
            height: 22px;
            border: 0.15em solid currentColor;
            border-radius: 0.15em;
            transform: translateY(-0.075em);
        }

        table tr th label {
            width: 100%;
        }

        table tr td input[type="checkbox"] {
            background-color: #fff;
            margin: 0;
            font: inherit;
            color: currentColor;
            width: 20px;
            height: 20px;
            border: 1px solid #646464;
            border-radius: 0.15em;
            transform: translateY(-0.075em);
        }

        .form-control + .form-control {
            margin-top: 1em;
        }

        tfoot, th, thead {
            border-color: inherit;
            border-style: solid;
            border-width: 0;
            vertical-align: middle;
            text-align: left;
            font-size: 13px;
        }

        tbody, td, tfoot, th, thead, tr {
            border-color: inherit;
            border-style: solid;
            border-width: 0;
            vertical-align: middle;
            text-align: left;
            font-size: 13px;
        }

        .wrapper.toggled:not(.sidebar-hovered) .sidebar-wrapper {
            width: 70px;
        }

        table.dataTable > thead > tr > th:not(.sorting_disabled), table.dataTable > thead > tr > td:not(.sorting_disabled) {
            padding-right: inherit;
        }

        .prnt-dv-wpr {
            float: left;
            width: 100%;
        }
    </style>
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
                <div class="breadcrumb-title pe-3">Report</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Total Outstanding</li>
                        </ol>
                    </nav>
                </div>
            </div>


            <div class="row" data-ng-app="ComplainApp" data-ng-controller="ComplainAppCtrl">
                <div class="col-xl-12">
                    <ul class="sub-pag-menu-ul">
                        <li><a href="total-outstanding.aspx">Total Outstanding</a></li>
                        <li><a href="total-outstanding-hostel.aspx">Hostel Outstanding</a></li>
                        <li><a href="total-outstanding-transport-student.aspx">Transport Student Total Outstanding</a></li>
                        <li><a href="dues-boarding-point-wise.aspx">Boarding Point-Wise Outstanding</a></li>
                        <li><a href="total-outstanding-transport.aspx">Transport Outstanding</a></li>
                        <li><a href="total-outstanding-message.aspx" class="sub-mnu-p-a-active">Send Dues Message</a></li>
                    </ul>
                </div>

                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase"></h6>
                    <hr />
                    <div class="grd-wpr">
                        <div class="card">
                            <div class="card-body">
                                <div class="find-dv">
                                    <div class="row">
                                        <div class="col-sm-2" style="display: none">
                                            <label for="validationCustom01" class="find-dv-lbl">Session</label>
                                            <asp:DropDownList ID="ddlsession" runat="server" class="form-control txtbx-ddl-style"></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2">
                                            <label for="validationCustom01" class="find-dv-lbl">Class</label>
                                            <asp:DropDownList ID="ddlclass" runat="server" class="form-control txtbx-ddl-style" OnSelectedIndexChanged="ddlclass_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2">
                                            <label for="validationCustom01" class="find-dv-lbl">Section</label>
                                            <asp:DropDownList ID="ddl_section" runat="server" class="form-control txtbx-ddl-style"></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2">
                                            <label for="validationCustom01" class="find-dv-lbl">Type</label>
                                            <asp:DropDownList ID="ddl_std_type" runat="server" class="form-control txtbx-ddl-style">
                                                <asp:ListItem Value="0">ALL</asp:ListItem>
                                                <asp:ListItem Value="1">On-Foot</asp:ListItem>
                                                <asp:ListItem Value="2">Transport</asp:ListItem>
                                                <asp:ListItem Value="3">Hostel</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2">
                                            <label for="validationCustom01" class="find-dv-lbl">Month</label>
                                            <asp:DropDownList ID="ddl_month" runat="server" class="form-control txtbx-ddl-style"></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:Button ID="btn_find" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="lnk_calculate_Click" />
                                        </div>

                                        <div class="col-sm-3">
                                            <asp:LinkButton ID="btn_excels" runat="server" Style="margin: 20px 0px 6px 0px;" OnClientClick="return Export();" class="btn btn-primary find-dv-btn">  <i class='bx bx-download'></i> Excel</asp:LinkButton>
                                            <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" Style="margin: 20px 0px 6px 10px;" CssClass="btn btn-primary find-dv-btn" runat="server"
                                                ToolTip="Print"><i class='bx bx-printer'></i></asp:LinkButton>

                                            <%--<asp:LinkButton ID="lnk_calculate" OnClick="lnk_calculate_Click" Style="line-height: 0px; padding: 2px 5px 2px 5px;"
                                                        CssClass="btn btn-primary find-dv-btn" ToolTip="Calculate Dues" runat="server"><span class="material-symbols-outlined">cached</span></asp:LinkButton>--%>
                                        </div>
                                    </div>
                                </div>

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
                                                        <span style="font-size: 14px; font-weight: bold;">
                                                            <asp:Label ID="lbl_class22" runat="server"></asp:Label></span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="grd-wpr-new" id="tabledata1">
                                                <asp:Panel ID="Panel1" runat="server">
                                                    <table id="example2" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                        <thead>
                                                            <tr>
                                                                <th>#</th>
                                                                <th style="padding: 7px 0px 0px 0px; text-align: center;">
                                                                    <asp:CheckBox ID="chkAll" runat="server" /></th>
                                                                <th>Adm. No.</th>
                                                                <th>Student Name</th>
                                                                <th>Type</th>
                                                                <th>Father's Name</th>
                                                                <th>Class</th>
                                                                <th>Section</th>
                                                                <th>Roll No.</th>
                                                                <th>WhatsApp No.</th>
                                                                <th>Total Dues</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:Repeater ID="rd_view" runat="server">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: center;">
                                                                            <asp:CheckBox ID="chkRowData" runat="server" /></td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_admissionserialnumber" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_student_name" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="Label6" runat="server" Text='<%#Bind("Type")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="Label3" runat="server" Text='<%#Bind("fathername")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                        </td>

                                                                        <td>
                                                                            <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_roll_no" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_f_mobile_no" runat="server" Text='<%#Bind("Father_whatsApp_no")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="Label5" runat="server" Text='<%#Bind("Total_dues")%>'></asp:Label>

                                                                            <asp:Label ID="lbl_gcm_id" Visible="false" runat="server" Text='<%#Bind("gcm_id")%>'></asp:Label>
                                                                            <asp:Label ID="lbl_todal_dues_amt" Visible="false" runat="server" Text='<%#Bind("Total_dues")%>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>

                                                        </tbody>

                                                        <tr>
                                                            <td colspan="10" style="text-align: right">Total</td>
                                                            <td>
                                                                <asp:Label ID="lbl_ttl_dues" runat="server" Text="0.00"></asp:Label></td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>

                                                <script type="text/javascript">
                                                    $(document).ready(function () {
                                                        // CHECK-UNCHECK ALL CHECKBOXES IN THE REPEATER 
                                                        // WHEN USER CLICKS THE HEADER CHECKBOX.
                                                        $('table [id*=chkAll]').click(function () {
                                                            if ($(this).is(':checked'))
                                                                $('table [id*=chkRowData]').prop('checked', true)
                                                            else
                                                                $('table [id*=chkRowData]').prop('checked', false)
                                                        });

                                                        // NOW CHECK THE HEADER CHECKBOX, IF ALL THE ROW CHECKBOXES ARE CHECKED.
                                                        $('table [id*=chkRowData]').click(function () {

                                                            var total_rows = $('table [id*=chkRowData]').length;
                                                            var checked_Rows = $('table [id*=chkRowData]:checked').length;

                                                            if (checked_Rows == total_rows)
                                                                $('table [id*=chkAll]').prop('checked', true);
                                                            else
                                                                $('table [id*=chkAll]').prop('checked', false);
                                                        });
                                                    });
                                                </script>
                                            </div>
                                        </div>


                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="grd-wpr" id="sendsms" runat="server" visible="false" style="display: block">
                                                    <table style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%;">
                                                        <tr>
                                                            <td style="padding: 5px; font-weight: bold;">Send Dues Message 
                                                                        <asp:RadioButton ID="rd_whatassp" runat="server" GroupName="ak" Text="Whatsapp" />
                                                                <asp:RadioButton ID="rd_sms" Checked="true" runat="server" GroupName="ak" Text="SMS" />
                                                                <asp:RadioButton ID="rd_notification" Checked="true" runat="server" GroupName="ak" Text="Notification" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 5px;">
                                                                <div class="col-md-12">
                                                                    <asp:Label ID="lbl_msg" runat="server"></asp:Label>
                                                                </div>
                                                                <div class="row" style="display: none">
                                                                    <asp:Panel runat="server" Visible="false" ID="pnl_0" CssClass="col-lg-6">
                                                                        <asp:Label runat="server" ID="lbl_0" class="form-label">Message</asp:Label>
                                                                        <asp:TextBox ID="txt_0" runat="server" class="form-control"></asp:TextBox>
                                                                    </asp:Panel>
                                                                    <asp:Panel runat="server" Visible="false" ID="pnl_1" CssClass="col-lg-6">
                                                                        <asp:Label runat="server" ID="lbl_1" class="form-label">Message</asp:Label>
                                                                        <asp:TextBox ID="txt_1" runat="server" class="form-control"></asp:TextBox>
                                                                    </asp:Panel>
                                                                </div>
                                                                <div class="row" style="display: none">
                                                                    <asp:Panel runat="server" Visible="false" ID="pnl_2" CssClass="col-lg-6">
                                                                        <asp:Label runat="server" ID="lbl_2" class="form-label">Message</asp:Label>
                                                                        <asp:TextBox ID="txt_2" runat="server" class="form-control"></asp:TextBox>
                                                                    </asp:Panel>
                                                                    <asp:Panel runat="server" Visible="false" ID="pnl_3" CssClass="col-lg-6">
                                                                        <asp:Label runat="server" ID="lbl_3" class="form-label">Message</asp:Label>
                                                                        <asp:TextBox ID="txt_3" runat="server" class="form-control"></asp:TextBox>
                                                                    </asp:Panel>
                                                                </div>
                                                                <div class="row" style="display: none">
                                                                    <asp:Panel runat="server" Visible="false" ID="pnl_4" CssClass="col-lg-6" Style="display: block">
                                                                        <asp:Label runat="server" ID="lbl_4" class="form-label">Message</asp:Label>
                                                                        <asp:TextBox ID="txt_4" runat="server" class="form-control"></asp:TextBox>
                                                                    </asp:Panel>
                                                                    <asp:Panel runat="server" Visible="false" ID="pnl_5" CssClass="col-lg-6">
                                                                        <asp:Label runat="server" ID="lbl_5" class="form-label">Message</asp:Label>
                                                                        <asp:TextBox ID="txt_5" runat="server" class="form-control"></asp:TextBox>
                                                                    </asp:Panel>
                                                                </div>
                                                                <div class="row" style="display: block">
                                                                    <asp:Panel runat="server" Style="display: none" Visible="false" ID="pnl_6" CssClass="col-lg-6">
                                                                        <asp:Label runat="server" ID="lbl_6" class="form-label">Message</asp:Label>
                                                                        <asp:TextBox ID="txt_6" runat="server" class="form-control"></asp:TextBox>
                                                                    </asp:Panel>
                                                                    <asp:Panel runat="server" Visible="false" ID="pnl_7" Style="display: none" CssClass="col-lg-6">
                                                                        <asp:Label runat="server" ID="lbl_7" class="form-label">Message</asp:Label>
                                                                        <asp:TextBox ID="txt_7" runat="server" class="form-control"></asp:TextBox>
                                                                    </asp:Panel>
                                                                </div>
                                                                <div class="row">
                                                                    <asp:Panel runat="server" Visible="false" ID="pnl_8" CssClass="col-lg-6">
                                                                        <asp:Label runat="server" ID="lbl_8" class="form-label">Message</asp:Label>
                                                                        <asp:TextBox ID="txt_8" runat="server" class="form-control"></asp:TextBox>

                                                                    </asp:Panel>
                                                                </div>
                                                                <div class="row">
                                                                    <asp:Panel runat="server" Visible="false" ID="pnl_msg" class="col-lg-12">
                                                                        <label for="validationCustom01" class="form-label">Message</label>
                                                                        <asp:TextBox ID="txt_message" ReadOnly="true" TextMode="MultiLine" runat="server" Style="height: 100px" class="form-control"></asp:TextBox>
                                                                    </asp:Panel>
                                                                </div>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td style="padding: 5px;">
                                                                <asp:Button ID="btn_msgPreview" runat="server" Text="SMS Preview" class="btn btn-danger find-dv-btn" OnClick="btn_msgPreview_Click" />
                                                                <div style="overflow: hidden; height: 1px">
                                                                    <asp:Button ID="btn_Submit" runat="server" Text="Send" class="btn btn-success find-dv-btn" Style="margin-left: 20px;" OnClick="btn_Submit_Click" />
                                                                </div>
                                                                <a onclick="save_data()" class="btn btn-primary" style="width: 146px; margin: 21px 0px 0px 9px;">Final Send SMS</a>
                                                            </td>
                                                        </tr>
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
            </div>
        </div>
    </div>

    <!--end page wrapper -->

    <script src="../assets/js/table2excel.js"></script>
    <script type="text/javascript">
        function Export() {
            var class_name = "Dues_list";
            $("[id*=tabledata1]").table2excel({
                filename: class_name + ".xls",
                sheetName: class_name + "-"
            });
            return false;
        }
    </script>


    <script type="text/javascript">
        function Confirm() {

            var confirm_value
            var isSubmitted = false;
            confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";

            if (confirm("Do you want to submit?")) {
                confirm_value.value = "Yes";

            }
            else {
                confirm_value.value = "No";
            }

            document.forms[0].appendChild(confirm_value);
        }


        function save_data() {
            var valsubmit = $('#<%=btn_Submit.ClientID %>').val();
            if (valsubmit == "Send") {

                $('#<%=btn_Submit.ClientID %>').val('Submitting.. Please Wait..');
               <%--   $('#<%=btn_Submit.ClientID %>').click(); --%>
                Confirm();
                document.getElementById("<%=btn_Submit.ClientID %>").click();

            }
            else {
                alert("Already submitted")
            }

        }
    </script>
</asp:Content>
