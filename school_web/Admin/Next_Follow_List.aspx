<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Next_Follow_List.aspx.cs" Inherits="school_web.Admin.Next_Flowup_List" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Follow Up Enquiry List
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script>
        $(function () {
            $("#<%=txt_startdate.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                readOnly: true,
                yearRange: "1900:2100",
            }).attr("readonly", "true");
        });
        $(function () {
            $("#<%=txt_enddate.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                readOnly: true,
                yearRange: "1900:2100",
            }).attr("readonly", "true");
        });

        $(function () {
            $("#<%=txt_date_from.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                readOnly: true,
                yearRange: "1900:2100",
            }).attr("readonly", "true");
        });

        $(function () {
            $("#<%=txt_nextflowingdate.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                readOnly: true,
                yearRange: "1900:2100",
            }).attr("readonly", "true");
        });
        $(function () {
            $("#<%=txt_followupdate.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                readOnly: true,
                yearRange: "1900:2100",
            }).attr("readonly", "true");
        });
        $(function () {
            $("#<%=txtnextfllowupdate.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                readOnly: true,
                yearRange: "1900:2100",
            }).attr("readonly", "true");
        });
    </script>
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
            font-weight: bold;
        }

        .modal-header {
            padding: 5px 14px 6px 14px !important;
        }

        .mdl-close-btn {
            display: block;
        }

        .modal {
            background: rgb(0 0 0 / 75%);
        }

        .modal-header {
            display: flex;
            flex-shrink: 0;
            align-items: center;
            justify-content: space-between;
            padding: 1rem 1rem;
            border-bottom: 1px solid #dee2e6;
            border-top-left-radius: calc(.3rem - 1px);
            border-top-right-radius: calc(.3rem - 1px);
            background-color: #001d7e;
        }


        .h5, h5 {
            margin-top: 0;
            margin-bottom: .5rem;
            font-weight: 500;
            line-height: 1.2;
            color: #fff;
        }


        .modal-title {
            margin-bottom: 0;
            line-height: 1.5;
            color: #FFF !important;
        }

        .task-info h5 {
            font-size: 13px;
            color: #000 !important;
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

        .stdlst-count-bx-dv {
            width: 16.6%;
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
                <div class="breadcrumb-title pe-3">Front Office</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Follow Up Enquiry List</li>
                        </ol>
                    </nav>
                </div>
            </div>
            <div class="row">
                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase"></h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                    <div class="row">
                                        <div class="find-dv">
                                            <div class="row">
                                                <div class="col-sm-1">
                                                    <label for="validationCustom01" class="find-dv-lbl">Class</label>
                                                    <asp:DropDownList ID="ddl_Courses" runat="server" class="form-control find-dv-txtbx">
                                                    </asp:DropDownList>
                                                </div>

                                                <div class="col-sm-1">
                                                    <label for="validationCustom01" class="find-dv-lbl">Purpose</label>
                                                    <asp:DropDownList ID="ddl_Source" Visible="false" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>
                                                    <asp:DropDownList ID="ddl_Purpose_search" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>
                                                </div>
                                                <div class="col-sm-2">
                                                    <label for="validationCustom01" class="find-dv-lbl">Search By</label>
                                                    <asp:DropDownList ID="ddl_searchby" runat="server" class="form-control find-dv-txtbx">
                                                        <asp:ListItem>Last Follow Up</asp:ListItem>
                                                        <asp:ListItem>Next Follow Up</asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>

                                                <div class="col-sm-2">
                                                    <label for="validationCustom01" class="find-dv-lbl">Follow Up From Date</label>
                                                    <div class="clndr-div">
                                                        <asp:TextBox ID="txt_startdate" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                        <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <label for="validationCustom01" class="find-dv-lbl" style="width: auto;">Follow Up  To Date</label>
                                                    <div class="clndr-div">
                                                        <asp:TextBox ID="txt_enddate" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                        <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                                    </div>
                                                </div>
                                                <div class="col-sm-1">
                                                    <label for="validationCustom01" class="find-dv-lbl" style="width: auto;">Status</label>
                                                    <asp:DropDownList ID="ddl_status" runat="server" class="form-control find-dv-txtbx">
                                                        <asp:ListItem>ALL</asp:ListItem>
                                                        <asp:ListItem>Prospect</asp:ListItem>
                                                        <asp:ListItem>Hot prospect</asp:ListItem>
                                                        <asp:ListItem>Deferred</asp:ListItem>
                                                        <asp:ListItem>Form Sold</asp:ListItem>
                                                        <asp:ListItem>Admission Done</asp:ListItem>
                                                        <asp:ListItem>Interview Done</asp:ListItem>
                                                        <asp:ListItem>Closed</asp:ListItem>
                                                        <asp:ListItem>Lost</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-3">
                                                    <asp:Button ID="btn_find" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_find_Click" />
                                                    <asp:LinkButton ID="btn_excels" Visible="false" runat="server" Style="margin: 0px 0px 6px 0px;" OnClick="btn_excels_Click" class="btn btn-primary find-dv-btn">  <i class='bx bx-download'></i> Excel</asp:LinkButton>
                                                    <asp:LinkButton ID="print1" Visible="false" OnClientClick="return PrintPanel()" Style="margin: 0px 0px 6px 10px;" CssClass="btn btn-primary find-dv-btn" runat="server"
                                                        ToolTip="Print"><i class='bx bx-printer'></i></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="grd-wpr">
                                            <div class="stdlst-count-dv">
                                                <div class="row">
                                                    <div class="col-xl-12">
                                                        <div class="stdlst-count-bx-dv" style="background-color: rgb(143 255 0 / 15%) !important;">
                                                            <div class="stdlst-count-bx-dv-ico">
                                                                <i class="material-symbols-outlined" style="background: #11d61e;">person_check</i>
                                                            </div>
                                                            <h2 class="stdlst-count-bx-name">Total Enq.</h2>
                                                            <p class="stdlst-count-bx-count" id="tttotalenq" runat="server">0</p>
                                                        </div>

                                                        <div class="stdlst-count-bx-dv brd-rdius-lft">
                                                            <div class="stdlst-count-bx-dv-ico">
                                                                <i class="material-symbols-outlined">person_check</i>
                                                            </div>
                                                            <h2 class="stdlst-count-bx-name">Prospect Enq.</h2>
                                                            <p class="stdlst-count-bx-count" id="ttlProspect" runat="server">0</p>
                                                        </div>

                                                        <div class="stdlst-count-bx-dv" style="background-color: rgb(72 163 215 / 15%) !important;">
                                                            <div class="stdlst-count-bx-dv-ico">
                                                                <i class="material-symbols-outlined" style="background: #48a3d7;">person_check</i>
                                                            </div>
                                                            <h2 class="stdlst-count-bx-name">Hot prospect Enq.</h2>
                                                            <p class="stdlst-count-bx-count" id="ttHotprospect" runat="server">0</p>
                                                        </div>

                                                        <div class="stdlst-count-bx-dv" style="background-color: rgb(215 119 72 / 15%) !important;">
                                                            <div class="stdlst-count-bx-dv-ico">
                                                                <i class="material-symbols-outlined" style="background: #d74882;">person_check</i>
                                                            </div>
                                                            <h2 class="stdlst-count-bx-name">Deferred Enq. </h2>
                                                            <p class="stdlst-count-bx-count" id="ttDeferred" runat="server">0</p>
                                                        </div>
                                                        <div class="stdlst-count-bx-dv" style="background-color: rgb(32 234 139 / 15%) !important;">
                                                            <div class="stdlst-count-bx-dv-ico">
                                                                <i class="material-symbols-outlined" style="background: #6c757d;">person_check</i>
                                                            </div>
                                                            <h2 class="stdlst-count-bx-name">Closed Enq.</h2>
                                                            <p class="stdlst-count-bx-count" id="ttClosed" runat="server">0</p>
                                                        </div>


                                                        <div class="stdlst-count-bx-dv" style="background-color: rgb(143 255 0 / 15%) !important;">
                                                            <div class="stdlst-count-bx-dv-ico">
                                                                <i class="material-symbols-outlined" style="background: #11d61e;">person_check</i>
                                                            </div>
                                                            <h2 class="stdlst-count-bx-name">Lost Enq.</h2>
                                                            <p class="stdlst-count-bx-count" id="ttLost" runat="server">0</p>
                                                        </div>

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
                                                                    <asp:Label ID="Label1" runat="server"></asp:Label>


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
                                                                    <span style="font-size: 14px; font-weight: bold;">Enquiry List
                                                                        <asp:Label ID="lbl_class22" runat="server"></asp:Label></span>
                                                                </div>
                                                            </div>
                                                        </div>






                                                        <asp:Panel ID="Panel1" runat="server">
                                                            <table id="example2" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                                <thead>
                                                                    <tr>
                                                                        <th>#</th>
                                                                        <th>Name</th>
                                                                        <th>Father Name</th>
                                                                        <th>Phone</th>
                                                                        <th>Address</th>
                                                                        <th>Class</th>
                                                                        <th>Purpose</th>
                                                                        <th>Reference</th>
                                                                        <th>Enquiry Date</th>
                                                                        <th>Last Follow Up Date</th>
                                                                        <th>Next Follow Up Date</th>
                                                                        <th>Status</th>
                                                                        <th class="hiddenOnPrint">Action</th>

                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <asp:Repeater ID="rd_view" runat="server" OnItemDataBound="rd_view_ItemDataBound">
                                                                        <ItemTemplate>
                                                                            <asp:Panel ID="Panel1" runat="server">
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_Name" runat="server" Text='<%#Bind("Name")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_father_name" runat="server" Text='<%#Bind("Father_name")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_Phone" runat="server" Text='<%#Bind("Phone")%>'></asp:Label>
                                                                                    </td>

                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_Address" Style="word-break: break-all" runat="server" Text='<%#Bind("Address")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_class" Style="word-break: break-all" runat="server" Text='<%#Bind("Course_Name")%>'></asp:Label>
                                                                                    </td>




                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_Purpose" runat="server" Text='<%#Bind("Purpose")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_Reference" runat="server" Text='<%#Bind("Reference_name")%>'></asp:Label>
                                                                                        <asp:Label ID="lbl_Source" Visible="false" runat="server" Text='<%#Bind("Source")%>'></asp:Label>
                                                                                    </td>



                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="Created_date1" runat="server" Text='<%#Bind("Created_date1")%>'></asp:Label>

                                                                                    </td>


                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_last_Follow_Up_Date" runat="server" Text='<%#Bind("Follow_Up_Datelast")%>'></asp:Label>
                                                                                    </td>

                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_Next_Follow_Up_Date1" runat="server" Text='<%#Bind("Next_Follow_Up_Date1")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_status" runat="server" Text='<%#Bind("Status")%>'></asp:Label>
                                                                                    </td>

                                                                                    <td style="text-align: left;" class="hiddenOnPrint">

                                                                                        <div class="user-box dropdown" style="float: left; display: inherit; height: auto; border-left: 0px solid #f0f0f0; margin-left: 0px;">
                                                                                            <a class="d-flex align-items-center nav-link dropdown-toggle dropdown-toggle-nocaret" style="font-size: 29px; padding: 0px; border: 0px;"
                                                                                                href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                                                                                                <div class="user-info ps-3" style="padding: 0px !important; border: 0px !important;">
                                                                                                    <i class="bx bx-grid-horizontal"></i>
                                                                                                </div>
                                                                                            </a>
                                                                                            <ul class="dropdown-menu dropdown-menu-end">
                                                                                                <li>
                                                                                                    <asp:LinkButton ID="lnk_Flow_up" class="dropdown-item" runat="server" OnClick="lnk_Flow_up_Click"><i class='bx bx-memory-card'></i>Follow-up</asp:LinkButton>
                                                                                                </li>
                                                                                                <li>
                                                                                                    <asp:LinkButton ID="lnk_form_sale" class="dropdown-item" runat="server" CausesValidation="false" OnClick="lnk_form_sale_Click" ToolTip="Edit"><i class="lni lni-pencil-alt"></i><span>Make Form Sale</span></asp:LinkButton>
                                                                                                </li>
                                                                                                <li>
                                                                                                    <asp:LinkButton ID="lnk_make_admission" class="dropdown-item" runat="server" CausesValidation="false" OnClick="lnk_make_admission_Click" ToolTip="Edit"><i class="lni lni-pencil-alt"></i><span>Make Admission</span></asp:LinkButton>
                                                                                                </li>

                                                                                                <asp:Label ID="lbl_Enquiry_Id" runat="server" Text='<%#Bind("Enquiry_Id")%>' Visible="false"></asp:Label>

                                                                                            </ul>
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                            </asp:Panel>
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
    </div>



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
    <style>
        .mdl-frm-row {
            margin: 2px 0px 6px 0px !important;
        }

        .col-eq {
            flex: 1;
            background: #f4f4f4;
        }

        .taskside {
            padding: 10px;
        }

            .taskside h4 {
                margin-bottom: 0;
                font-size: 18px;
            }

        .taskseparator {
            border-top: 1px solid #e6e6e6;
            -webkit-box-shadow: 0 1px 6px 1px rgba(255,255,255,.5);
            box-shadow: 0 1px 6px 1px rgba(255,255,255,.5);
        }

        .taskside hr {
            margin-top: 9px;
            margin-bottom: 9px;
            border: 0;
            border-top: 1px solid #655f5f !important;
        }

        .task-info {
            float: left;
            width: 100%;
        }

            .task-info h5 {
                font-size: 13px;
            }

        .text-dark {
            font-weight: bold;
        }

        .fa.pull-left {
            margin-right: 0.3em;
        }

        .task-info h5 span {
        }
    </style>

    <div class="modal fade" id="myModal1" role="dialog" style="top: 0px">
        <div class="modal-dialog md-width" style="max-width: 1050px; margin: 5.75rem auto;">
            <!-- Modal content-->
            <div class="modal-content" style="position: relative; background: #1affff;">
                <div class="modal-header">
                    <h3 class="modal-title" style="font-size: 20px;">Add Enquiry</h3>
                    <button type="button" class="mdl-close-btn" data-dismiss="modal"><i class="fa fa-times" aria-hidden="true"></i></button>
                </div>
                <div class="modal-body md-bdy">
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-1">
                                <label for="validationCustom01" class="find-dv-lbl">Purpose<sup>*</sup></label>
                            </div>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="ddl_Purpose" runat="server" class="form-select find-dv-txtbx">
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-1">
                                <label for="validationCustom01" class="find-dv-lbl">
                                    Name<sup>*
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required Field."
                                            ControlToValidate="txt_name" ValidationGroup="A">
                                        </asp:RequiredFieldValidator>
                                    </sup>
                                </label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txt_name" runat="server" class="form-control"></asp:TextBox>
                            </div>

                            <div class="col-sm-1">
                                <label for="validationCustom01" class="find-dv-lbl">
                                    Phone<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Required Field."
                                        ControlToValidate="txt_phone_no" ValidationGroup="A"></asp:RequiredFieldValidator></sup>
                                </label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txt_phone_no" runat="server" class="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                            </div>
                        </div>



                    </div>

                    <div class="mdl-frm-row">
                        <div class="row">



                            <div class="col-sm-1">
                                <label for="validationCustom01" class="find-dv-lbl">Email Id<sup> </sup></label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txt_email" runat="server" class="form-control"></asp:TextBox>
                            </div>
                            <div class="col-sm-1">
                                <label for="validationCustom01" class="find-dv-lbl">Address<sup> </sup></label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txt_address" runat="server" class="form-control" TextMode="MultiLine"></asp:TextBox>
                            </div>
                            <div class="col-sm-1">
                                <label for="validationCustom01" class="find-dv-lbl">
                                    District<sup>*
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Required Field."
                                            ControlToValidate="txt_name" ValidationGroup="A">
                                        </asp:RequiredFieldValidator>
                                    </sup>
                                </label>
                            </div>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="ddl_district" runat="server" class="form-select find-dv-txtbx">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="mdl-frm-row">
                        <div class="row">


                            <div class="col-sm-1">
                                <label for="validationCustom01" class="find-dv-lbl">Pin Code.<sup> </sup></label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txt_pincode" runat="server" class="form-control"></asp:TextBox>
                            </div>
                            <div class="col-sm-1">
                                <label for="validationCustom01" class="find-dv-lbl">
                                    Remarks<sup>*
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Required Field."
                                            ControlToValidate="txt_Remarks" ValidationGroup="A">
                                        </asp:RequiredFieldValidator>
                                    </sup>
                                </label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txt_Remarks" runat="server" class="form-control" TextMode="MultiLine"></asp:TextBox>
                            </div>
                            <div class="col-sm-1">
                                <label for="validationCustom01" class="find-dv-lbl">
                                    Date <sup>*
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Required Field."
                                            ControlToValidate="txt_date_from" ValidationGroup="A">
                                        </asp:RequiredFieldValidator></sup></label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txt_date_from" runat="server" class="form-control"></asp:TextBox>
                            </div>

                        </div>
                    </div>

                    <div class="mdl-frm-row">
                        <div class="row">


                            <div class="col-sm-1">
                                <label for="validationCustom01" class="find-dv-lbl">
                                    Next Follow Up Date<sup> *<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Required Field."
                                        ControlToValidate="txt_nextflowingdate" ValidationGroup="A">
                                    </asp:RequiredFieldValidator></sup>
                                </label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txt_nextflowingdate" runat="server" class="form-control"></asp:TextBox>
                            </div>



                        </div>
                    </div>


                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-1">
                                <label for="validationCustom01" class="find-dv-lbl">Reference <sup></sup></label>
                            </div>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="ddl_Reference" runat="server" class="form-select find-dv-txtbx">
                                </asp:DropDownList>
                            </div>


                            <div class="col-sm-1">
                                <label for="validationCustom01" class="find-dv-lbl">Course<sup></sup></label>
                            </div>

                            <div class="col-sm-3">
                                <asp:DropDownList ID="ddl_class_form" runat="server" class="form-select find-dv-txtbx">
                                </asp:DropDownList>
                            </div>


                        </div>
                    </div>
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-1">
                                <label for="validationCustom01" class="find-dv-lbl">Upload passport size (JPG/PNG) <sup></sup></label>
                            </div>
                            <div class="col-sm-3">
                                <h2 class="online_frm-grp-h"><sup></sup></h2>


                                <asp:FileUpload ID="file_passport_photo" runat="server" CssClass="form_control" />
                                <br />
                                <asp:Button ID="btn_passport_photo" runat="server" OnClick="btn_passport_photo_Click" Text="Upload Passport Photo" Style="height: 29px; font-size: 12px; font-weight: bold; display: none" />

                                <script>
                                    $('#<%=file_passport_photo.ClientID%>').on('change', function () {
                                        $('#<%=btn_passport_photo.ClientID%>').click();
                                    })
                                </script>
                            </div>
                            <div class="col-sm-3">
                                <div class="online_frm-grp">
                                    <asp:Image ID="img_passport_photo" Visible="false" runat="server" Style="height: 150px; width: 150px; padding: 2px; border: 2px solid #000;" />

                                </div>
                            </div>

                        </div>

                    </div>
                </div>
                <div class="mdl-frm-row">
                    <div class="row">
                        <div class="col-sm-4"></div>
                        <div class="col-sm-8">
                            <asp:Button ID="btn_save" OnClick="btn_save_Click" runat="server" Style="float: left; margin-bottom: 10px;"
                                Text="Save" ValidationGroup="A" class="btn btn-success" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:HiddenField ID="hd_enqid" runat="server" />
    <script type="text/javascript">
        function openModal1() {

            $('#myModal1').modal('show');
        }
        function openModal2() {

            $('#myModal2').modal('show');
        }
    </script>


    <div class="modal fade" id="myModal2" role="dialog" style="top: 0px">
        <div class="modal-dialog md-width" style="max-width: 1159px; margin: 5.75rem auto;">
            <!-- Modal content-->
            <div class="modal-content" style="position: relative; background: #fff;">
                <div class="modal-header">
                    <h3 class="modal-title" style="font-size: 20px;">Follow Up Enquiry</h3>
                    <button type="button" class="mdl-close-btn" data-dismiss="modal"><i class="fa fa-times" aria-hidden="true"></i></button>
                </div>
                <div class="modal-body md-bdy" style="padding: 0px 1rem;">
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-8">
                                <div class="mdl-frm-row">
                                    <div class="row" style="margin-top: 10px">

                                        <div class="col-sm-3">
                                            <label for="validationCustom01" class="find-dv-lbl">
                                                Enquiry Purpose  
                                            </label>
                                        </div>
                                        <div class="col-sm-9">
                                            <asp:Label ID="lbl_enquiry_Purpose" Style="color: #000;" runat="server"></asp:Label>
                                        </div>
                                    </div>


                                    <div class="row" style="margin-top: 10px" id="row1" runat="server">

                                        <div class="col-sm-3">
                                            <label for="validationCustom01" class="find-dv-lbl">
                                                Remarks<sup> *<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Required Field."
                                                    ControlToValidate="txt_remarks_floup" ValidationGroup="b">
                                                </asp:RequiredFieldValidator></sup>
                                            </label>
                                        </div>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="txt_remarks_floup" runat="server" class="form-control" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 10px" id="row2" runat="server">
                                        <div class="col-sm-3">
                                            <label for="validationCustom01" class="find-dv-lbl">Status<sup>*</sup></label>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddl_status_fllowup" runat="server" class="form-select find-dv-txtbx">
                                                <asp:ListItem>Prospect</asp:ListItem>
                                                <asp:ListItem>Hot prospect</asp:ListItem>
                                                <asp:ListItem>Deferred</asp:ListItem>
                                                <asp:ListItem>Interview Done</asp:ListItem>
                                                <asp:ListItem>Closed</asp:ListItem>
                                                <asp:ListItem>Lost</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-3" id="nextflolowupdate1">
                                            <label for="validationCustom01" class="find-dv-lbl">
                                                Next Follow Up Date<sup>*</sup>
                                            </label>
                                        </div>
                                        <div class="col-sm-3" id="nextflolowupdate2">
                                            <asp:TextBox ID="txtnextfllowupdate" runat="server" class="form-control"></asp:TextBox>

                                            <asp:TextBox ID="txt_followupdate" runat="server" class="form-control" Visible="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <script>
                                        $(document).ready(function () {

                                            on_next_flowupdate();
                                            $("#<%=ddl_status_fllowup.ClientID%>").on('change', function () {
                                                on_next_flowupdate();
                                            })
                                        });

                                        function on_next_flowupdate() {
                                            if ($('#<%= ddl_status_fllowup.ClientID %> option:selected').val() == "Closed") {
                                                $("#nextflolowupdate1").hide();
                                                $("#nextflolowupdate2").hide();
                                            }
                                            else if ($('#<%= ddl_status_fllowup.ClientID %> option:selected').val() == "Lost") {
                                                $("#nextflolowupdate1").hide();
                                                $("#nextflolowupdate2").hide();
                                            }
                                            else {

                                                $("#nextflolowupdate1").show();
                                                $("#nextflolowupdate2").show();
                                            }
                                        }

                                    </script>



                                    <div class="row" id="row3" runat="server">

                                        <div class="col-sm-12" style="text-align: center">
                                            <asp:Button ID="btn_FollowUp" OnClick="btn_FollowUp_Click" runat="server" Style="float: none; margin-top: 16px;"
                                                Text="Save" ValidationGroup="b" class="btn btn-success" />
                                        </div>
                                    </div>

                                    <div class="row" style="margin-top: 10px; border-top: 1px solid; padding: 6px 0px 0px 0px;">
                                        <div class="col-sm-12">
                                            <label for="validationCustom01" class="find-dv-lbl" style="padding: 0px 0px 6px 0px;">
                                                Remarks History  
                                            </label>
                                        </div>
                                        <div class="col-sm-12">
                                            <asp:GridView ID="GrdView_Follow_Up" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Follow Up Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Follow_Up_Date" runat="server" Text='<%#Bind("Follow_Up_Date1q")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Next Follow Up Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Next_Follow_Up_Date" runat="server" Text='<%#Bind("Next_Follow_Up_Date1q")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remarks">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Response_Remarks" runat="server" Text='<%#Bind("Response_Remarks")%>' Style="word-break: break-all"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblstatus" runat="server" Text='<%#Bind("Status")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="By">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_by_user" runat="server" Text='<%#Bind("Created_by")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>




                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>



                            </div>
                            <div class="col-sm-4 col-eq">
                                <div class="taskside">
                                    <h4>Summary               
                                       
                                    </h4>
                                    <!-- /.box-tools -->

                                    <hr class="taskseparator" />

                                    <div style="text-align: center; padding: 0px; float: left; height: auto; width: 100%; margin: 0px 0px 8px 0px;">

                                        <asp:Image ID="Image1" runat="server" Style="height: 120px; width: 120px; padding: 2px; border: 1px solid #000;" />

                                    </div>


                                    <div class="task-info task-single-inline-wrap task-info-start-date">
                                        <h5><i class="fapull-left fa fa-calendar-o"></i>
                                            <span class="text-dark">Enquiry Date:</span>
                                            <asp:Label ID="lbl_Enquirydate" runat="server" Style="margin-left: 56px;"></asp:Label>
                                        </h5>
                                    </div>
                                    <div class="task-info task-single-inline-wrap task-info-start-date">
                                        <h5><i class="fapull-left fa fa-calendar-o"></i>
                                            <span class="text-dark">Last Follow Up Date:</span>
                                            <asp:Label ID="lbl_lastfloupdate" runat="server" Style="margin-left: 9px;"></asp:Label>
                                        </h5>
                                    </div>
                                    <div class="task-info task-single-inline-wrap task-info-start-date">
                                        <h5><i class="fapull-left fa fa-calendar-o"></i>
                                            <span class="text-dark">
                                                <asp:Label ID="lbl_nextflwodatetitle" runat="server"> Next Follow Up Date :</asp:Label></span>
                                            <asp:Label ID="lbl_nextfloupdate" runat="server" Style="margin-left: 4px;"></asp:Label>
                                        </h5>
                                    </div>
                                    <div class="task-info task-single-inline-wrap ptt10">
                                        <h5><span class="text-dark">Name :</span>
                                            <asp:Label ID="lbl_name" runat="server"></asp:Label>
                                        </h5>
                                        <h5><span class="text-dark">Father Name :</span>
                                            <asp:Label ID="lbl_father_name" runat="server"></asp:Label>
                                        </h5>
                                        <h5><span class="text-dark">Phone :</span><asp:Label ID="lbl_mobile_no" runat="server"></asp:Label></h5>

                                        <h5><span class="text-dark">Purpose :</span>
                                            <asp:Label ID="lbl_Purpose" runat="server"></asp:Label></h5>
                                        <h5><span class="text-dark">Reference :</span>
                                            <asp:Label ID="lbl_reffrence" runat="server"></asp:Label></h5>
                                        <h5 style="display: none"><span class="text-dark">Source :</span>
                                            <asp:Label ID="lbl_sources" runat="server"></asp:Label></h5>
                                        <h5><span class="text-dark">Email :</span><asp:Label ID="lbl_email" runat="server"></asp:Label>
                                        </h5>
                                        <h5><span class="text-dark">Address :</span><asp:Label ID="lbl_address" runat="server"></asp:Label>
                                        </h5>
                                        <h5><span class="text-dark">Class :</span>
                                            <asp:Label ID="lbl_class" runat="server"></asp:Label></h5>


                                        <h5><span class="text-dark">Note :</span><asp:Label ID="lbl_remarks" runat="server"></asp:Label>
                                        </h5>
                                        <h5><span class="text-dark">Created By :</span>


                                            <asp:Label ID="lbl_created_by" runat="server"></asp:Label>
                                        </h5>
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
