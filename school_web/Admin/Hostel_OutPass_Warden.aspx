<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Hostel_OutPass_Warden.aspx.cs" Inherits="school_web.Admin.Hostel_OutPass_Warden" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Hostel OutPass Request for Warden
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
        .stdlst-count-bx-dv {
            width: 25% !important;
        }

        .btn-group, .btn-group-vertical {
            position: relative;
            display: inline-flex;
            vertical-align: middle;
            display: none !important;
        }

        .head {
            display: none;
        }


        .home-grph-wpr {
            width: 114%;
            margin: 0px 0px 0px -110px;
        }

        .modal.fade .modal-dialog {
            transition: transform .3s ease-out;
            transform: translate(0, 0px);
        }

        #notification {
            z-index: 999999999!important;
        }

        .modal {
            background: rgb(0 0 0 / 0%);
        }

        .stdcontTbl {
            margin: 0px 0px 15px 0px;
            padding: 2px 2px;
            width: 100%;
            float: left;
            background-color: #f0f8ff;
            border: 1px solid #bac7d3;
        }

            .stdcontTbl table {
                width: 100%;
            }

                .stdcontTbl table tr td {
                    padding: 3px 5px 3px 5px;
                    font-weight: 500;
                    font-size: 15px;
                    background-color: #f0f8ff;
                    border: 1px solid #bac7d3;
                }

                    .stdcontTbl table tr td p {
                        margin: 0px;
                    }

        .mb-3 {
            margin-bottom: 0rem !important;
        }
    </style>
    <link rel="stylesheet" href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:opsz,wght,FILL,GRAD@24,400,0,0" />
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
                <div class="breadcrumb-title pe-3">Hotel</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Hostel OutPass Request for Warden</li>
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
                                            <div class="collapse multi-collapse" id="multiCollapseExample1" style="display: block">
                                                <div class="row">

                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">From Date</label>
                                                        <asp:TextBox ID="txt_from_date" runat="server" class="form-control find-dv-txtbx datepicker"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">To Date</label>
                                                        <asp:TextBox ID="txt_to_date" runat="server" class="form-control find-dv-txtbx datepicker"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1">
                                                        <asp:Button ID="tn_find_by_date" runat="server" OnClick="tn_find_by_date_Click" Text="Find" class="btn btn-primary find-dv-btn" />
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <asp:LinkButton ID="btn_excels" runat="server" OnClick="btn_excels_Click" class="btn btn-primary find-dv-btn">  <i class='bx bx-download'></i> Excel</asp:LinkButton>
                                                        <asp:LinkButton ID="print1" Visible="false" OnClientClick="return PrintPanel()" CssClass="btn btn-primary find-dv-btn" Style="margin-left: 10px;" runat="server"
                                                            ToolTip="Print"><i class='bx bx-printer'></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>






                                        <div class="prnt-dv-wpr printborder">
                                            <div class="stdlst-count-dv">
                                                <div class="row">
                                                    <div class="col-xl-12">
                                                        <div class="stdlst-count-bx-dv brd-rdius-lft">
                                                            <div class="stdlst-count-bx-dv-ico">
                                                                <i class="material-symbols-outlined">person_check</i>
                                                            </div>
                                                            <h2 class="stdlst-count-bx-name">Total Pending Request</h2>
                                                            <p class="stdlst-count-bx-count" id="lbl_total_Assined_Warden" runat="server">0</p>
                                                        </div>

                                                        <div class="stdlst-count-bx-dv" style="background-color: rgb(72 163 215 / 15%) !important;">
                                                            <div class="stdlst-count-bx-dv-ico">
                                                                <i class="material-symbols-outlined" style="background: #48a3d7;">person_check</i>
                                                            </div>
                                                            <h2 class="stdlst-count-bx-name">Total On going</h2>
                                                            <p class="stdlst-count-bx-count" id="lbl_total_ongoing" runat="server">0</p>
                                                        </div>
                                                        <div class="stdlst-count-bx-dv" style="background-color: rgb(32 234 139 / 15%) !important;">
                                                            <div class="stdlst-count-bx-dv-ico">
                                                                <i class="material-symbols-outlined" style="background: #6c757d;">person_check</i>
                                                            </div>
                                                            <h2 class="stdlst-count-bx-name">Total Reject</h2>
                                                            <p class="stdlst-count-bx-count" id="lbl_total_reject" runat="server">0</p>
                                                        </div>


                                                        <div class="stdlst-count-bx-dv" style="background-color: rgb(143 255 0 / 15%) !important;">
                                                            <div class="stdlst-count-bx-dv-ico">
                                                                <i class="material-symbols-outlined" style="background: #6c757d;">person_check</i>
                                                            </div>
                                                            <h2 class="stdlst-count-bx-name">Total Complete</h2>
                                                            <p class="stdlst-count-bx-count" id="lbl_total_Complite" runat="server">0</p>
                                                        </div>



                                                    </div>
                                                </div>
                                            </div>

                                            <div class="grd-wpr" style="overflow: hidden">
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
                                                                <span style="font-size: 14px; font-weight: bold;">Hostel Out Pass -<asp:Label ID="lbl_class22" runat="server"></asp:Label></span>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <asp:Panel ID="Panel1" runat="server">
                                                        <table id="example2" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                            <thead>
                                                                <tr>
                                                                    <th>#</th>
                                                                    <%if (this.IsChecked)
                                                                        { %>
                                                                    <th class="hiddenOnPrint">Action</th>
                                                                    <%} %>

                                                                    <th>Request ID </th>
                                                                    <th>Admission No.</th>
                                                                    <th>Student Name</th>
                                                                    <th>Course</th>
                                                                    <th>Roll No.</th>
                                                                    <th>Request Date</th>
                                                                    <th>Out Pass Date</th>
                                                                    <th>Returen Date</th>
                                                                    <th>No. of Day </th>
                                                                    <th>Status</th>
                                                                    <th>Last Remarks</th>
                                                                    <th>Last Reply Date</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <asp:Repeater ID="rd_view" runat="server"  OnItemDataBound="rd_view_ItemDataBound">
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                            </td>
                                                                            <%if (this.IsChecked)
                                                                                { %>
                                                                            <td class="hiddenOnPrint">
                                                                                <div class="user-box dropdown" style="float: left; display: inherit; height: auto; border-left: 0px solid #f0f0f0; margin-left: 0px;">
                                                                                    <a class="d-flex align-items-center nav-link dropdown-toggle dropdown-toggle-nocaret" style="font-size: 29px; padding: 0px; border: 0px;"
                                                                                        href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                                                                                        <div class="user-info ps-3" style="padding: 0px !important; border: 0px !important;">
                                                                                            <i class="bx bx-grid-horizontal"></i>
                                                                                        </div>
                                                                                    </a>
                                                                                    <ul class="dropdown-menu dropdown-menu-end">

                                                                                        <li>
                                                                                            <asp:LinkButton ID="lnk_process" class="dropdown-item" runat="server" OnClick="lnk_process_Click"><i class='bx bx-memory-card'></i>Process</asp:LinkButton>

                                                                                        </li>
                                                                                        <li id="printgetpass" runat="server">
                                                                                             <a class="dropdown-item" href="slip/print_getpass.aspx?Request_id=<%#Eval("Request_id")%>" target="_blank"><i class='bx bx-printer'></i><span>Print Getpass</span></a>

                                                                                        </li>


                                                                                    </ul>
                                                                                </div>

                                                                                <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>



                                                                            </td>
                                                                            <%} %>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lbl_Request_id" runat="server" Text='<%#Bind("Request_id")%>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lbl_Adm_No" runat="server" Text='<%#Bind("Adm_No")%>'></asp:Label>
                                                                            </td>


                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>

                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lbl_course" runat="server" Text='<%#Bind("classname")%>'></asp:Label>

                                                                            </td>


                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="Label1" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                            </td>

                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lbl_request_date" runat="server" Text='<%#Bind("applydate")%>'></asp:Label>
                                                                            </td>


                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lbl_outpass_date" runat="server" Text='<%#Bind("Start_datetime")%>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lbl_Return_Pass_Date_time" runat="server" Text='<%#Bind("End_Datetime")%>'></asp:Label>
                                                                            </td>


                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lbl_no_of_date" runat="server" Text='<%#Bind("No_of_day")%>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lbl_status" runat="server" Text='<%#Bind("Last_status")%>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lbl_remarks" Style="word-break: break-all;" runat="server" Text='<%#Bind("Last_remarks")%>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lbl_Last_reply_date_time" Style="word-break: break-all;" runat="server" Text='<%#Bind("Last_replydatetime")%>'></asp:Label>
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
        <!--end row-->
    </div>

    <asp:HiddenField ID="hd_id" runat="server" />
    <!--end page wrapper -->
    <script type="text/javascript">

        function openModal2() {

            $('#myModal2').modal('show');
        }
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
    <div class="modal fade" id="myModal2" role="dialog" style="top: 0px">
        <div class="modal-dialog md-width" style="max-width: 1159px; margin: 5.75rem auto;">
            <!-- Modal content-->
            <div class="modal-content" style="position: relative">
                <div class="modal-header">
                    <h3 class="modal-title" style="font-size: 20px;">Hostel Out Pass Process</h3>
                    <button type="button" class="mdl-close-btn" data-dismiss="modal"><i class="fa fa-times" aria-hidden="true"></i></button>
                </div>
                <div class="modal-body md-bdy" style="padding: 0px 1rem;">
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-8">
                                <div class="mdl-frm-row">

                                    <div class="row" runat="server" visible="false" style="margin: 0px; padding: 0px; background-color: #fff63eb0" id="row1">

                                        <div class="row" style="margin-top: 10px">

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


                                        <div class="row" style="margin-top: 10px">
                                            <div class="col-sm-3">
                                                <label for="validationCustom01" class="find-dv-lbl">Status<sup>*</sup></label>
                                            </div>
                                            <div class="col-sm-5">
                                                <asp:DropDownList ID="ddl_status" runat="server" class="form-select find-dv-txtbx">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 10px" id="pnl_mode_t_nS">
                                            <div class="col-sm-3">
                                                <label for="validationCustom01" class="find-dv-lbl">Hostel Warden<sup>*</sup></label>
                                            </div>
                                            <div class="col-sm-5">
                                                <asp:DropDownList ID="ddl_hostel_warden" runat="server" class="form-select find-dv-txtbx">
                                                </asp:DropDownList>
                                            </div>
                                        </div>


                                        <div class="row" style="margin-top: 17px;">
                                            <div class="col-sm-12" style="text-align: center">
                                                <asp:Button ID="btn_save" OnClick="btn_save_Click" runat="server" Style="float: none; margin-bottom: 10px;"
                                                    Text="Save" ValidationGroup="b" class="btn btn-success" />
                                            </div>
                                        </div>
                                    </div>

                                    <script type="text/javascript">
                                        $(document).ready(function () {

                                            on_select_user_selection();
                                            $("#<%=ddl_status.ClientID%>").on('change', function () {
                                                on_select_user_selection();
                                            })
                                        });

                                        function on_select_user_selection() {



                                            if ($('#<%= ddl_status.ClientID %> option:selected').val() == "Select") {
                                                $("#pnl_mode_t_nS").hide();
                                            }
                                            else if ($('#<%= ddl_status.ClientID %> option:selected').val() == "Assign to Hostel Warden") {
                                                $("#pnl_mode_t_nS").show();
                                            }
                                            else {
                                                $("#pnl_mode_t_nS").hide();
                                            }
                                        }

                                    </script>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <asp:GridView ID="GrdView_Follow_Up" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Next_Follow_Up_Date" runat="server" Text='<%#Bind("Date_timechat")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_status" runat="server" Text='<%#Bind("Status")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Remarks">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Remarks" runat="server" Text='<%#Bind("Remarks")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="By">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Replay_By" runat="server" Text='<%#Bind("Reply_By")%>'></asp:Label>{
                                                            <asp:Label ID="lbl_Reply_By" runat="server" Text='<%#Bind("Reply_By_User")%>'></asp:Label>
                                                            }
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
                                            <span class="text-dark">Request Date:</span>
                                            <asp:Label ID="lbl_Enquirydate" runat="server" Style="margin-left: 17px;"></asp:Label>
                                        </h5>
                                    </div>
                                    <div class="task-info task-single-inline-wrap task-info-start-date">
                                        <h5><i class="fapull-left fa fa-calendar-o"></i>
                                            <span class="text-dark">Last Reply Date:</span>
                                            <asp:Label ID="lbl_lastfloupdate" runat="server" Style="margin-left: 4px;"></asp:Label>
                                        </h5>
                                    </div>


                                    <div class="task-info task-single-inline-wrap ptt10">
                                        <h5><span class="text-dark">Name:</span>
                                            <asp:Label ID="lbl_name" runat="server"></asp:Label>
                                        </h5>
                                        <h5><span class="text-dark">Admission No.:</span>
                                            <asp:Label ID="lbl_admi_no" runat="server"></asp:Label>
                                        </h5>
                                        <h5><span class="text-dark">Phone:</span><asp:Label ID="lbl_mobile_no" runat="server"></asp:Label></h5>


                                        <h5><span class="text-dark">Email:</span><asp:Label ID="lbl_email" runat="server"></asp:Label>
                                        </h5>

                                        <h5><span class="text-dark">Course:</span>
                                            <asp:Label ID="lbl_class" runat="server"></asp:Label></h5>
                                        <h5><span class="text-dark">Roll No.:</span>
                                            <asp:Label ID="lbl_roll_no" runat="server"></asp:Label></h5>
                                        <h5><span class="text-dark">Hostel Room No.:</span>
                                                <asp:Label ID="lbl_hostel_romm_no" runat="server"></asp:Label>
                                            </h5>
                                             <h5><span class="text-dark">Hostel Bed No.:</span>
                                                <asp:Label ID="lbl_hostel_bed_no" runat="server"></asp:Label>
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
