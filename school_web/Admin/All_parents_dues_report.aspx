<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="All_parents_dues_report.aspx.cs" Inherits="school_web.Admin.All_parents_dues_report" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    All Parents Dues Report
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

            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3" style="position: relative">
                <div class="breadcrumb-title pe-3">Reports</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Parents Wise Dues </li>
                        </ol>
                    </nav>
                </div>

            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <ul class="sub-pag-menu-ul" style="margin: 0px 0px 6px 0px;">
                        <li><a href="Parents_Wise_Dues_Report.aspx">Individual Parents Details</a></li>
                        <li><a href="All_parents_dues_report.aspx" class="sub-mnu-p-a-active">All Parents Dues</a></li>


                    </ul>
                </div>

                <div class="row">
                    <div class="col-xl-12">
                        <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text=" "></asp:Label>
                        <hr style="margin: 5px 0px 6px 0px; height: 1.5px;" />
                        <div class="card">
                            <div class="card-body">
                                <div class="p-4 border rounded">
                                    <div class="row g-3 needs-validation" novalidate="">
                                        <div class="col-md-2">
                                            <label for="validationCustom01" class="form-label Llabel">Session</label>
                                            <asp:DropDownList ID="ddl_session" runat="server" class="form-select">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                            <label for="validationCustom01" class="form-label Llabel">Till Month</label>
                                            <asp:DropDownList ID="ddl_month" runat="server" class="form-select">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-1">
                                            <asp:Button ID="btn_find_parentsid" runat="server" Style="margin-top: 24px;" Text="Find" CssClass="btn btn-primary form-fnd-btns" OnClick="btn_find_parentsid_Click" />
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:LinkButton ID="btn_excels" Visible="false" runat="server" Style="margin: 20px 0px 6px 0px;" OnClientClick="return Export();" class="btn btn-primary find-dv-btn">  <i class='bx bx-download'></i> Excel</asp:LinkButton>
                                            <asp:LinkButton ID="print1" Visible="false" OnClientClick="return PrintPanel()" Style="margin: 20px 0px 6px 10px;" CssClass="btn btn-primary find-dv-btn" runat="server"
                                                ToolTip="Print"><i class='bx bx-printer'></i></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-xl-12">
                        <asp:Panel ID="Panel2" runat="server" Visible="false">
                            <div class="card">
                                <div class="card-body">
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
                                                <div class="grd-wpr-new" id="tabledata1" style="overflow: unset!important;">
                                                    <asp:Panel ID="Panel1" runat="server"> 
                                                        <table id="example2" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                            <thead>
                                                                <tr>
                                                                    <th>#</th>
                                                                    <th style="padding: 7px 0px 0px 0px; text-align: center;" class="hiddenOnPrint">
                                                                        <asp:CheckBox ID="chkAll" runat="server" /></th> 
                                                                    <th>Parents Name</th>
                                                                    <th>Parents Id</th>
                                                                    <th>Till Monts</th>
                                                                    <th>Contact No.(whatsApp No.)</th> 
                                                                    <th>Student</th>
                                                                    <th>Total Dues</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <asp:Repeater ID="rd_view" runat="server" OnItemDataBound="rd_view_ItemDataBound">
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: center;" class="hiddenOnPrint">
                                                                                <asp:CheckBox ID="chkRowData" runat="server" /></td> 
                                                                            <td>
                                                                                <asp:Label ID="lbl_fathername" runat="server" Text='<%#Bind("fathername")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_Parent_id" runat="server" Text='<%#Bind("Parent_id")%>'></asp:Label>
                                                                            </td>

                                                                            <td>
                                                                                <asp:Label ID="lbl_monthname" runat="server"></asp:Label>
                                                                            </td>
                                                                            <td> 
                                                                                <asp:Label ID="lbl_father_mob" runat="server" Text='<%#Bind("Father_whatsApp_no")%>'></asp:Label>,

                                                                                <asp:Label ID="lbl_mother_mob" runat="server" Text='<%#Bind("father_mob")%>'></asp:Label>
                                                                               
                                                                            </td>

                                                                            <td>
                                                                                <asp:Label ID="lbl_AdmissionWise_Dues" runat="server" Text='<%#Bind("AdmissionWise_Dues")%>'></asp:Label>
                                                                            </td>
                                                                            <td>

                                                                                <asp:Label ID="lbl_total_amount" runat="server" Text='<%#Bind("Total_dues","{0:n}") %>'></asp:Label>
                                                                                <asp:LinkButton ID="lnk_student_view" Visible="false" runat="server" CausesValidation="false" Style="display: none" OnClick="lnk_student_view_Click" ToolTip="View"><i class="lni lni-eye"></i><span>View</span></asp:LinkButton>
                                                                            </td>

                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>

                                                            </tbody>

                                                            <tr>
                                                                <td colspan="8"
                                                                    style="text-align: right">

                                                                    <asp:Label ID="lbl_ttl_dues" runat="server" Text="0.00"></asp:Label></td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                    <script>
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
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="grd-wpr" id="sendsms" runat="server" visible="false" style="display: block">
                                                <table style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%;">
                                                    <tr>
                                                        <td style="padding: 5px; font-weight: bold;">Send Dues Message 
                                                                        <asp:RadioButton ID="rd_whatassp" Checked="true" runat="server" GroupName="ak" Text="Whatsapp" />
                                                            <asp:RadioButton Visible="false" ID="rd_sms" Checked="false" runat="server" GroupName="ak" Text="SMS" />
                                                            <asp:RadioButton Visible="false" ID="rd_notification" Checked="false" runat="server" GroupName="ak" Text="Notification" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding: 5px;">
                                                            <div class="col-md-12">
                                                                <asp:Label ID="lbl_msg" runat="server"></asp:Label>
                                                            </div>
                                                            <div class="row">

                                                                <asp:Label runat="server" ID="lbl_0" class="form-label">Pay Date</asp:Label>
                                                                <asp:TextBox ID="txt_paydate" runat="server" Style="width: 200px; margin-left: 10px; margin-top: 4px;"
                                                                    class="form-control"></asp:TextBox>
                                                                <link href="../../Autocomplete/jquery-ui.css" rel="stylesheet" />
                                                                <script src="../../Autocomplete/jquery-ui.js"></script>
                                                                <script>
                                                                    $(function () {
                                                                        $("#<%=txt_paydate.ClientID %>").datepicker({
                                                                            dateFormat: "dd/mm/yy",
                                                                            changeMonth: true,
                                                                            changeYear: true,
                                                                            minDate: 0 // 0 means today; past dates disabled
                                                                        }).attr("readonly", "true");
                                                                    });
                                                                </script>
                                                                <%--  --%>
                                                            </div>


                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td style="padding: 5px;">

                                                            <div style="overflow: hidden; height: 1px">
                                                                <asp:Button ID="btn_Submit" runat="server" Text="Send" class="btn btn-success find-dv-btn" Style="margin-left: 20px;" OnClick="btn_Submit_Click" />
                                                            </div>
                                                            <a onclick="save_data()" class="btn btn-primary" style="width: 146px; margin: 10px 0px 0px 0px;">Final Send SMS</a>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div> 
                                </div>
                            </div>
                        </asp:Panel>
                    </div> 
                </div>
                 
            </div>
        </div>
    </div>
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

    <script type="text/javascript">


        function openModal3() {

            $('#myModal3').modal('show');

        }
    </script>
    <style>
        .head {
            display: none;
        }

        #pageFooter {
            display: none;
        }

        .modal-open .modal {
            background: rgb(0 0 0 / 48%);
        }

        @media print {
            .noPrint {
                display: none;
            }
        }
    </style>


    <style>
        .cardsp {
            width: 100%;
            padding: 10px;
            margin: 0px 0px 10px 0px;
            float: left;
            background-color: #f7ffd6;
            border: 1px solid #d8ef7b;
            border-radius: 3px;
            box-shadow: 0 4px 12px rgb(0 0 0 / 3%);
        }

            .cardsp h2 {
                margin: 0px 0px 5px 0px;
                color: #333;
                padding: 0px 0px 3px 0px;
                font-size: 17px;
                border-bottom: 1px solid #d8ef7b;
            }

            .cardsp p {
                margin: 8px 0;
                color: #555;
                font-size: 13px;
            }

        .modal-body {
            padding: 10px 10px;
        }

        .modal-header {
            padding: 5px 10px;
        }

        .ui-widget-content {
            z-index: 9999999 !important;
        }
    </style>


    <div class="modal fade" id="myModal3" role="dialog" style="top: 0px">
        <div class="modal-dialog md-width" style="max-width: 1050px; margin: 5.75rem auto;">
            <!-- Modal content-->
            <div class="modal-content" style="position: relative">
                <div class="modal-header">
                    <h3 class="modal-title" style="font-size: 20px;">
                        <asp:Label ID="lbl_heading_name" runat="server" Text='Student Wise Dues'></asp:Label></h3>
                    <button type="button" class="mdl-close-btn" data-dismiss="modal"><i class="fa fa-times" aria-hidden="true"></i></button>
                </div>
                <div class="modal-body md-bdy">
                    <div class="mdl-frm-row">
                        <div class="row" id="studentdetails" runat="server" visible="false">
                            <div class="col-sm-12">
                                <table id="example21" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th>Adm. No.</th>
                                            <th>Student Name</th>
                                            <th>Class</th>
                                            <th>Section</th>
                                            <th>Roll No.</th>
                                            <th>Total Dues</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="Repeater1" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                    </td>


                                                    <td>
                                                        <asp:Label ID="lbl_admissionserialnumber" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label2" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                    </td>

                                                    <td>
                                                        <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                    </td>

                                                    <td>
                                                        <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label1" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                    </td>

                                                    <td>

                                                        <asp:Label ID="Label6" runat="server" Text='<%#Bind("Total_dues","{0:n}") %>'></asp:Label>

                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>

                                    </tbody>

                                    <tr>
                                        <td colspan="6" style="text-align: right">Total</td>
                                        <td>
                                            <asp:Label ID="lbl_total_dues_student_wise" runat="server" Text="0.00"></asp:Label></td>
                                    </tr>
                                </table>
                            </div>



                        </div>

                    </div>



                </div>
            </div>
        </div>
    </div>
</asp:Content>
