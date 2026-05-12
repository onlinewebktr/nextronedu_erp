<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Set_Employee_Hiring_Fee.aspx.cs" Inherits="school_web.Admin.Set_Employee_Hiring_Fee" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Set Employee Hiring Fee
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
    <script src="https://cdn.tiny.cloud/1/h64ik3cu5x1uocom2fuu89jbo1ah2yqk1rtvpwu420y3ye4w/tinymce/5/tinymce.min.js" referrerpolicy="origin"></script>
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

        .dropdown-item {
            display: block;
            width: 100%;
            padding: 0.25rem 1rem;
            clear: both;
            font-weight: 400;
            color: #212529;
            text-align: inherit;
            text-decoration: none;
            white-space: nowrap;
            background-color: transparent;
            border: 0;
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
                <div class="breadcrumb-title pe-3">Employees Hiring </div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Employee Hiring Fee  </li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">



                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase">Added Employee Hiring Fee  </h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5">
                                    <div class="row">
                                        <div class="col-md-2">
                                            <label for="validationCustom01" class="find-dv-lbl">Session</label>
                                            <asp:DropDownList ID="ddl_session" runat="server" class="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddl_session_SelectedIndexChanged"></asp:DropDownList>
                                        </div>

                                        <div class="col-md-2">
                                            <label for="validationCustom01" class="find-dv-lbl">Employee Hiring Type</label>
                                            <asp:DropDownList ID="ddl_Hiring_Type" runat="server" class="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddl_Hiring_Type_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:LinkButton ID="btn_excels" runat="server" Style="margin: 22px 0px 6px 0px;" OnClick="btn_excels_Click" class="btn btn-primary find-dv-btn">  <i class='bx bx-download'></i> Excel</asp:LinkButton>
                                            <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" Style="margin: 22px 0px 6px 10px;" CssClass="btn btn-primary find-dv-btn" runat="server"
                                                ToolTip="Print"><i class='bx bx-printer'></i></asp:LinkButton>
                                        </div>

                                        <div class="col-md-6" style="text-align: right">
                                            <asp:Button ID="btn_add" runat="server" Text="Add Employee Hiring Fee & Seat" CssClass="btn btn-primary" CausesValidation="false" OnClick="btn_add_Click" Style="margin: 26px 0px 0px 0px;" />


                                        </div>



                                        <div class="col-sm-12">
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
                                                                    <span style="font-size: 14px; font-weight: bold;">Employee Hiring Fee
                                                                        <asp:Label ID="lbl_class22" runat="server"></asp:Label></span>


                                                                </div>
                                                            </div>


                                                        </div>
                                                        <asp:Panel ID="Panel1" runat="server">
                                                            <table id="example21" class="table table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                                <thead>
                                                                    <tr>
                                                                        <th>#</th>
                                                                        <th>Session</th>
                                                                        <th>Hiring Type</th>
                                                                        <th>Hiring For</th>
                                                                        <th>Fee</th>
                                                                        <th>Start Date</th>
                                                                        <th>End Date</th>
                                                                        <th>Number of Seat</th>
                                                                        <th>Number of Application Fill</th>
                                                                        <th>Is Active</th>
                                                                        <th>Action</th>
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
                                                                                    <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("Session")%>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_Hiring_name" runat="server" Text='<%#Bind("Hiring_name")%>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_Employee_Hiring_For" runat="server" Text='<%#Bind("Employee_Hiring_For")%>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_Fees" runat="server" Text='<%#Bind("Fees")%>'></asp:Label>
                                                                                </td>

                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_Start_Date" runat="server" Text='<%#Bind("start_date")%>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_end_date" runat="server" Text='<%#Bind("end_date")%>'></asp:Label>
                                                                                </td>

                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_no_seat" runat="server" Text='<%#Bind("no_seat")%>'></asp:Label>
                                                                                </td>

                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_no_application" runat="server" Text='<%#Bind("no_application")%>'></asp:Label>
                                                                                </td>


                                                                                <td style="text-align: left;">

                                                                                    <asp:Label ID="lbl_isactivenew" runat="server" Text='<%#Bind("activestatus")%>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left;">

                                                                                    <div class="user-box dropdown" style="float: left; display: inherit; height: auto; border-left: 0px solid #f0f0f0; margin-left: 0px;">
                                                                                        <a class="d-flex align-items-center nav-link dropdown-toggle dropdown-toggle-nocaret" style="font-size: 29px; padding: 0px; border: 0px;"
                                                                                            href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                                                                                            <div class="user-info ps-3" style="padding: 0px !important; border: 0px !important;">
                                                                                                <i class="bx bx-grid-horizontal"></i>
                                                                                            </div>
                                                                                        </a>
                                                                                        <ul class="dropdown-menu dropdown-menu-end">

                                                                                            <li>
                                                                                                <asp:LinkButton ID="lnkEdit" Style="display: block; width: 100%; padding: 0.25rem 1rem; clear: both; font-weight: 400; color: #212529; text-align: inherit; text-decoration: none; white-space: nowrap; background-color: transparent; border: 0;"
                                                                                                    runat="server" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Edit"> <i class="lni lni-pencil-alt"> </i>Edit</asp:LinkButton>
                                                                                            </li>
                                                                                            <li>
                                                                                                <asp:LinkButton Style="display: block; width: 100%; padding: 0.25rem 1rem; clear: both; font-weight: 400; color: #212529; text-align: inherit; text-decoration: none; white-space: nowrap; background-color: transparent; border: 0;"
                                                                                                    ID="lnkActive" runat="server" OnClick="lnkActive_Click"><i class="bx bx-happy-heart-eyes"></i>Active</asp:LinkButton>
                                                                                            </li>
                                                                                        </ul>
                                                                                    </div>





                                                                                    <asp:Label ID="lbl_Isactive" runat="server" Text='<%#Bind("Isactive")%>' Visible="false"></asp:Label>
                                                                                    <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                                    <asp:Label ID="lbl_Hiring_id" runat="server" Text='<%#Bind("Hiring_id")%>' Visible="false"></asp:Label>
                                                                                    <asp:Label ID="lbl_sessions_ids" runat="server" Text='<%#Bind("Session_id")%>' Visible="false"></asp:Label>
                                                                                    <asp:Label ID="lbl_attechment" runat="server" Text='<%#Bind("attechment")%>' Visible="false"></asp:Label>

                                                                                    <asp:Label ID="lbl_General_instructions" runat="server" Text='<%#Bind("General_instructions")%>' Visible="false"></asp:Label>
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

    <%--------------------------------------------------------------%>

    <style>
        @media (min-width: 576px) {
            .modal-dialog {
                max-width: 1150px;
                margin: 1.75rem auto;
            }
        }

        .form-label {
            margin-bottom: 4px;
            font-weight: 500;
            margin-top: 13px;
        }

        .ui-datepicker-trigger {
            display: none;
        }

        .ui-datepicker .ui-datepicker-title select {
            font-size: 1em;
            margin: 1px 0;
            COLOR: #000;
            z-index: 99999 !important;
        }

        .calender-icon {
            margin: 0px 0px 0px 0px;
            position: relative;
            font-size: 13px;
            font-weight: normal;
            width: 100%;
        }

        .clndr-icon {
            font-size: 14px !important;
            color: #ff2956;
            position: absolute;
            top: 7px;
            right: 10px;
        }
    </style>
    <link href="../Autocomplete/jquery-ui.css" rel="stylesheet" />
    <script src="../Autocomplete/jquery-ui.js"></script>
    <script>
        $(function () {
            $("#<%=txt_start_date.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "2022:2023",

            }).attr("readonly", "true");
        });
    </script>

    <script>
        $(function () {
            $("#<%=txt_end_date.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "2022:2023",

            }).attr("readonly", "true");
        });
    </script>
    <!-------popupadd year----->
    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Employee hiring set apply Fees </h5>
                    <asp:Button ID="btnclose" runat="server" class="btn btn-secondary" OnClientClick="return close()" Text="Close" />
                </div>
                <div class="modal-body">
                    <div class="p-4 border rounded">
                        <div class="row g-3 needs-validation" novalidate="">

                            <div class="row">


                                <div class="col-md-3">
                                    <label for="validationCustom01" class="form-label">Session <sup>* </sup></label>
                                    <asp:DropDownList ID="ddl_session_fee" runat="server" class="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddl_session_fee_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <label for="validationCustom01" class="form-label">Hiring Type <sup>* </sup></label>
                                    <asp:DropDownList ID="ddl_Hiring_Type_add" runat="server" class="form-select"></asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <label for="validationCustom01" class="form-label">Hiring For <sup>* </sup></label>
                                    <asp:DropDownList ID="ddl_Hiring_For" runat="server" class="form-select">
                                        <asp:ListItem>Teacher</asp:ListItem>
                                        <asp:ListItem>Pre Primary Teacher</asp:ListItem>

                                        <asp:ListItem>Tabla player </asp:ListItem>
                                        <asp:ListItem>Jr. Accountant</asp:ListItem>
                                        <asp:ListItem>Lower Division Clerk</asp:ListItem>
                                        <asp:ListItem>Multi - Tasking Staff (Male)</asp:ListItem>
                                        <asp:ListItem>Multi - Tasking Staff (Female)</asp:ListItem>
                                        <asp:ListItem>Principal</asp:ListItem>
                                        <asp:ListItem>Coordinator</asp:ListItem>
                                        <asp:ListItem>Operator</asp:ListItem>
                                        <asp:ListItem>Non Teaching Staff</asp:ListItem>
                                        <asp:ListItem>HOD</asp:ListItem>
                                        <asp:ListItem>Counsellor</asp:ListItem>
                                        <asp:ListItem>Trainer</asp:ListItem>
                                        <asp:ListItem>Lab Assistant</asp:ListItem>
                                        <asp:ListItem>Clerk</asp:ListItem>
                                        <asp:ListItem>Chairman</asp:ListItem>
                                        <asp:ListItem>Secretary</asp:ListItem>
                                        <asp:ListItem>Director</asp:ListItem>
                                        <asp:ListItem>Advisor</asp:ListItem>
                                        <asp:ListItem>Member of Committee</asp:ListItem>
                                        <asp:ListItem>Peon</asp:ListItem>
                                        <asp:ListItem>Helper</asp:ListItem>
                                        <asp:ListItem>Examination Incharge</asp:ListItem>
                                        <asp:ListItem>Accountant</asp:ListItem>

                                    </asp:DropDownList>
                                </div>

                                <div class="col-md-3">
                                    <label for="validationCustom01" class="form-label">Fee <sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_coursefee"></asp:RequiredFieldValidator></sup></label>
                                    <asp:TextBox ID="txt_coursefee" runat="server" class="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>

                                </div>
                            </div>
                            <div class="row">

                                <div class="col-md-3">
                                    <label for="validationCustom01" class="form-label">Number of Seat <sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_no_seat"></asp:RequiredFieldValidator></sup></label>
                                    <asp:TextBox ID="txt_no_seat" runat="server" class="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>

                                </div>
                                <div class="col-md-3">
                                    <label for="validationCustom01" class="form-label">Number of Application Fill <sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_no_application"></asp:RequiredFieldValidator></sup></label>
                                    <asp:TextBox ID="txt_no_application" runat="server" class="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>

                                </div>




                                <div class="col-md-3">
                                    <label for="validationCustom01" class="form-label">Start Date <sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_no_seat"></asp:RequiredFieldValidator></sup></label>
                                    <div class="clndr-div">
                                        <asp:TextBox ID="txt_start_date" runat="server" class="form-control"></asp:TextBox>
                                        <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <label for="validationCustom01" class="form-label">End Date<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator5" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_no_application"></asp:RequiredFieldValidator></sup></label>
                                    <div class="clndr-div">
                                        <asp:TextBox ID="txt_end_date" runat="server" class="form-control"></asp:TextBox>
                                        <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                    </div>


                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <label for="validationCustom01" class="form-label">Attachment (Only .pdf, .jpg 500 kb file size) <sup></sup></label>
                                    <asp:FileUpload ID="FileUpload1" runat="server" />
                                    <a id="file1" runat="server" visible="false" target="_blank">Download File</a>
                                </div>
                            </div>

                            <div class="col-md-12" style="margin-top: 0px; margin-bottom: 0px;">
                                <label for="validationCustom01" class="form-label">General instructions  <sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator6" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_no_application"></asp:RequiredFieldValidator></sup></label>



                                <script>
                                    $(function () {
                                        tinymce.init({
                                            selector: $('#<%=txt_info.ClientID%>').selector,
                                            plugins: [
                                                "advlist autolink autosave link image lists charmap print preview hr anchor pagebreak spellchecker",
                                                "searchreplace wordcount visualblocks visualchars code fullscreen insertdatetime media nonbreaking",
                                                "table contextmenu directionality emoticons template textcolor paste fullpage textcolor colorpicker textpattern", "imagetools"
                                            ],

                                            toolbar1: "bold italic underline strikethrough | fontselect fontsizeselect | alignleft aligncenter alignright alignjustify",
                                            toolbar2: "| bullist numlist | outdent indent blockquote | undo redo | link unlink image media | forecolor backcolor | table",
                                            toolbar3: "table | hr removeformat | subscript superscript | charmap emoticons | print fullscreen | ltr rtl | spellchecker | visualchars visualblocks nonbreaking template pagebreak restoredraft",

                                            menubar: true,
                                            toolbar_items_size: 'small',

                                            //link_list: [
                                            //         { title: 'Terms & Condition', value: '' }
                                            //],

                                            paste_data_images: true,


                                            style_formats: [
                                                { title: 'Bold text', inline: 'b' },
                                                { title: 'Red text', inline: 'span', styles: { color: '#ff0000' } },
                                                { title: 'Red header', block: 'h1', styles: { color: '#ff0000' } },
                                                { title: 'Example 1', inline: 'span', classes: 'example1' },
                                                { title: 'Example 2', inline: 'span', classes: 'example2' },
                                                { title: 'Table styles' },
                                                { title: 'Table row 1', selector: 'tr', classes: 'tablerow1' }
                                            ],

                                            templates: [
                                                { title: 'Test template 1', content: 'Test 1' },
                                                { title: 'Test template 2', content: 'Test 2' }
                                            ]
                                        });


                                    });


                                </script>
                                <textarea id="txt_info" runat="server" name="area" class="form-control" style="min-height: 300px; width: 100%"></textarea>

                            </div>



                            <div class="col-12">
                                <asp:Button ID="btn_Submit" runat="server" Text="Add" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btn_Submit_Click" />
                                <asp:Button ID="btn_cancel" runat="server" Text="Cancel" OnClientClick="return close()" class="btn btn-dark" Visible="false" CausesValidation="false" />
                                <br />
                                <asp:Label ID="lbl_msg" runat="server" ForeColor="Maroon" Font-Bold="true"></asp:Label>

                            </div>
                        </div>
                    </div>


                </div>
            </div>
        </div>
    </div>
    <div id="fadeup"></div>

    <link href="../Autocomplete/jquery-ui.css" rel="stylesheet" />
    <script src="../Autocomplete/jquery-ui.js"></script>
    <script type="text/javascript">
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
</asp:Content>
