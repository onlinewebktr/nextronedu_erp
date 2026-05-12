<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="student-list.aspx.cs" Inherits="school_web.Admin.student_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Student’s List
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

        .modal {
            background: rgb(0 0 0 / 43%);
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

        .printbx-sec {
            float: right;
            position: absolute;
            right: 0px;top: -5px;
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
            <div class="row">
                <div class="page-breadcrumb d-sm-flex align-items-center mb-3">
                    <div class="breadcrumb-title pe-3"><a href="student-report-home.aspx" runat="server" id="backbtns" class="backlnk-css"><i class="bx bx-arrow-back"></i>Reports</a></div>
                    <div class="ps-3">
                        <nav aria-label="breadcrumb">
                            <ol class="breadcrumb mb-0 p-0">
                                <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                                </li>
                                <li class="breadcrumb-item active" aria-current="page">Student’s List</li>
                            </ol>
                        </nav>
                    </div>
                    <div class="printbx-sec">
                        <asp:LinkButton ID="btn_excels" runat="server" OnClick="btn_excels_Click" class="btn btn-primary find-dv-btn" ToolTip="Excel" Style="padding: 4px 6px 3px 5px;"><img src="../assets/images/icons/excel-ico.png" style="width: 18px;" /></asp:LinkButton>
                        <asp:LinkButton ID="print1" Visible="false" OnClientClick="return PrintPanel()" CssClass="btn btn-primary find-dv-btn"  Style="margin-left: 10px; padding: 3px 0px 5px 5px;" runat="server"
                            ToolTip="Print">
                        <i class='bx bx-printer'></i>
                        </asp:LinkButton>
                    </div>
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
                                                    <label for="validationCustom01" class="find-dv-lbl">Session</label>
                                                    <asp:DropDownList ID="ddlsession" runat="server" class="form-control find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddlsession_SelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                                <div class="col-sm-1">
                                                    <label for="validationCustom01" class="find-dv-lbl">Class</label>
                                                    <asp:DropDownList ID="ddlclass" runat="server" class="form-control find-dv-txtbx" OnSelectedIndexChanged="ddlclass_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                </div>
                                                <div class="col-sm-1">
                                                    <label for="validationCustom01" class="find-dv-lbl">Section</label>
                                                    <asp:DropDownList ID="ddl_section" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>
                                                </div>
                                                <div class="col-sm-2">
                                                    <label for="validationCustom01" class="find-dv-lbl">Student Status</label>
                                                    <asp:DropDownList ID="ddl_student_status" runat="server" class="form-control find-dv-txtbx">
                                                        <asp:ListItem Value="ALL">ALL</asp:ListItem>
                                                        <asp:ListItem Value="1">Active</asp:ListItem>
                                                        <asp:ListItem Value="0">Inactive</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>


                                                <div class="col-sm-2">
                                                    <label for="validationCustom01" class="find-dv-lbl">Student Type</label>
                                                    <asp:DropDownList ID="ddl_studenttype" runat="server" class="form-control find-dv-txtbx">
                                                        <asp:ListItem Value="ALL">ALL</asp:ListItem>
                                                        <asp:ListItem Value="New">New Admission</asp:ListItem>
                                                        <asp:ListItem Value="NT">Old Admission</asp:ListItem>
                                                        <asp:ListItem Value="Transferred">Transferred to Next Session</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>


                                                <div class="col-sm-1">
                                                    <asp:Button ID="btn_find" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_find_Click" />
                                                </div>

                                                <div class="col-sm-2">
                                                    <label for="validationCustom01" class="find-dv-lbl" style="width: auto;">Admission Type</label>
                                                    <asp:DropDownList ID="ddl_days_hostel" runat="server" class="form-control find-dv-txtbx">
                                                        <asp:ListItem>Select</asp:ListItem>
                                                        <%--<asp:ListItem Value="Yes">Hostel</asp:ListItem>--%>
                                                        <%--<asp:ListItem Value="No">Day Scholar</asp:ListItem> --%>
                                                        <asp:ListItem Value="1">Hostel</asp:ListItem>
                                                        <asp:ListItem Value="2">Day Scholar</asp:ListItem>
                                                        <asp:ListItem Value="3">Day Boarding with Lunch</asp:ListItem>
                                                        <asp:ListItem Value="4">Bus</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-1">
                                                    <asp:Button ID="btn_fnd_by_days" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_fnd_by_days_Click" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="std-info-fnd" data-bs-toggle="collapse" href="#multiCollapseExample1" role="button" aria-expanded="false" aria-controls="multiCollapseExample1">
                                            <span class="material-symbols-outlined fullscreenIco">close_fullscreen</span>
                                        </div>



                                        <div class="find-dv">
                                            <div class="collapse multi-collapse" id="multiCollapseExample1">
                                                <div class="row">
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Payment Status</label>
                                                        <asp:DropDownList ID="ddl_pay_status" runat="server" class="form-select find-dv-txtbx">
                                                            <asp:ListItem>ALL</asp:ListItem>
                                                            <asp:ListItem>Paid</asp:ListItem>
                                                            <asp:ListItem>Dues</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-1">
                                                        <asp:Button ID="btn_find_by_pay_status" runat="server" OnClick="btn_find_by_pay_status_Click" Text="Find" class="btn btn-primary find-dv-btn" />
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">From Adm. Date</label>
                                                        <asp:TextBox ID="txt_from_date" runat="server" class="form-control find-dv-txtbx datepicker"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">To Adm. Date</label>
                                                        <asp:TextBox ID="txt_to_date" runat="server" class="form-control find-dv-txtbx datepicker"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1">
                                                        <asp:Button ID="tn_find_by_date" runat="server" OnClick="tn_find_by_date_Click" Text="Find" class="btn btn-primary find-dv-btn" />
                                                    </div>

                                                </div>
                                            </div>
                                        </div>


                                        <%--<div class="home-grph-wpr">
                                            <div class="row">
                                                <div class="col-xl-9">
                                                    <div id="chart"></div>
                                                </div>
                                                <div class="col-xl-3">
                                                    <div class="home-grph-wpr-smll">
                                                        <div id="daily_collection" class="card card-statistic-2" style="width: 100%; height: 320px; -webkit-tap-highlight-color: transparent; user-select: none; position: relative; padding: 10px;">
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>--%>

                                        <div class="prnt-dv-wpr printborder">
                                            <div class="stdlst-count-dv">
                                                <div class="row">
                                                    <div class="col-xl-12">
                                                        <div class="stdlst-count-bx-dv brd-rdius-lft" style="background-color: rgb(143 255 0 / 15%) !important;">
                                                            <div class="stdlst-count-bx-dv-ico">
                                                                <i class="material-symbols-outlined" style="background: #11d61e;">home_work</i>
                                                            </div>
                                                            <h2 class="stdlst-count-bx-name">Total Student</h2>
                                                            <p class="stdlst-count-bx-count" id="total_act_inactive" runat="server">0</p>
                                                        </div>

                                                        <div class="stdlst-count-bx-dv" style="background-color: rgb(143 255 0 / 15%) !important; position: relative;">
                                                            <div class="stdlst-count-bx-dv-ico">
                                                                <i class="material-symbols-outlined" style="background: #11d61e;">home_work</i>
                                                            </div>
                                                            <h2 class="stdlst-count-bx-name">Total Student (Inactive)</h2>
                                                            <span style="position: absolute; top: 10px;"></span>
                                                            <p class="stdlst-count-bx-count" id="total_inactive" runat="server">0</p>
                                                        </div>

                                                        <div class="stdlst-count-bx-dv">
                                                            <div class="stdlst-count-bx-dv-ico">
                                                                <i class="material-symbols-outlined">person_check</i>
                                                            </div>
                                                            <h2 class="stdlst-count-bx-name">Total Student (Active) </h2>
                                                            <span style="position: absolute; top: 10px;"></span>
                                                            <p class="stdlst-count-bx-count" id="ttlStudent" runat="server">0</p>
                                                        </div>

                                                        <div class="stdlst-count-bx-dv" style="background-color: rgb(72 163 215 / 15%) !important; position: relative;">
                                                            <div class="stdlst-count-bx-dv-ico">
                                                                <i class="material-symbols-outlined" style="background: #48a3d7;">person_check</i>
                                                            </div>
                                                            <h2 class="stdlst-count-bx-name">New Student</h2>
                                                            <p class="stdlst-count-bx-count" id="ttlnewStudent" runat="server">0</p>
                                                        </div>

                                                        <div class="stdlst-count-bx-dv" style="background-color: rgb(215 119 72 / 15%) !important;">
                                                            <div class="stdlst-count-bx-dv-ico">
                                                                <i class="material-symbols-outlined" style="background: #d74882;">person_check</i>
                                                            </div>
                                                            <h2 class="stdlst-count-bx-name">Old Student</h2>
                                                            <p class="stdlst-count-bx-count" id="ttloldStudent" runat="server">0</p>
                                                        </div>
                                                        <div class="stdlst-count-bx-dv" style="background-color: rgb(32 234 139 / 15%) !important;">
                                                            <div class="stdlst-count-bx-dv-ico">
                                                                <i class="material-symbols-outlined" style="background: #6c757d;">person_check</i>
                                                            </div>
                                                            <h2 class="stdlst-count-bx-name">Upgraded Student</h2>
                                                            <p class="stdlst-count-bx-count" id="ttlTransferStudent" runat="server">0</p>
                                                        </div>


                                                        <div class="stdlst-count-bx-dv" style="background-color: rgb(143 255 0 / 15%) !important;">
                                                            <div class="stdlst-count-bx-dv-ico">
                                                                <i class="material-symbols-outlined" style="background: #11d61e;">home_work</i>
                                                            </div>
                                                            <h2 class="stdlst-count-bx-name">Hostel Student</h2>
                                                            <p class="stdlst-count-bx-count" id="ttlHostelStudent" runat="server">0</p>
                                                        </div>
                                                        <div class="stdlst-count-bx-dv brd-rdius-rght" style="background-color: rgb(10 115 174 / 70%) !important;">
                                                            <div class="stdlst-count-bx-dv-ico">
                                                                <i class="material-symbols-outlined" style="background: #ddd809;">airport_shuttle</i>
                                                            </div>
                                                            <h2 class="stdlst-count-bx-name" style="color: #fffdfd;">Bus Student</h2>
                                                            <p class="stdlst-count-bx-count" style="color: #fffdfd;" id="ttlBusStudent" runat="server">0</p>
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
                                                                <span style="font-size: 14px; font-weight: bold;">Student List -<asp:Label ID="lbl_class22" runat="server"></asp:Label></span>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="stdcontTbl" style="display: none">
                                                        <table class="table-bordered">
                                                            <thead>
                                                                <tr>
                                                                    <td>
                                                                        <p>
                                                                            Total Student :
                                                                                <asp:Label ID="lbl_total_student" runat="server"></asp:Label>
                                                                        </p>
                                                                    </td>
                                                                    <td>
                                                                        <p>
                                                                            Total Readmission : 
                                    <asp:Label ID="lbltotal_readmission" runat="server">0</asp:Label>

                                                                        </p>
                                                                    </td>
                                                                    <td>
                                                                        <p>
                                                                            Total New Admission : 
                                  <asp:Label ID="lbl_newadmission" runat="server">0</asp:Label>
                                                                        </p>
                                                                    </td>
                                                                    <td>
                                                                        <p>
                                                                            Total Transferred To Next Session : 
                                  <asp:Label ID="lbl_total_trasfer_tonextsession" runat="server">0</asp:Label>
                                                                        </p>
                                                                    </td>
                                                                </tr>
                                                            </thead>
                                                        </table>
                                                    </div>
                                                    <table id="datatable" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                        <thead>
                                                            <tr>
                                                                <th>#</th>
                                                                <th class="hiddenOnPrint">Action</th>
                                                                <th>Session</th>
                                                                <th>Admission No.</th>
                                                                <th>Class</th>
                                                                <th>Section</th>
                                                                <th>Roll No.</th>
                                                                <th>Student Name</th>
                                                                <th>Father Name</th>
                                                                <th>Mobile No</th>
                                                                <th>DOB</th>
                                                                <th>Admission Date</th>
                                                                <th>Admission Type</th>
                                                                <th>Pwd</th>
                                                                <th>Status</th>
                                                                <th>Old Admission No.</th>
                                                                <th>Entry Date</th>
                                                                <th>Gender</th>
                                                                <th>Blood Group</th>

                                                                <th>Mother Name</th>
                                                                <th>Address</th>
                                                                <th>City</th>
                                                                <th>District</th>
                                                                <th>Post Office</th>
                                                                <th>Police Station</th>
                                                                <th>Pin Code</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:Repeater ID="rd_view" runat="server" OnItemDataBound="rd_view_ItemDataBound">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                        </td>
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
                                                                                            <a class="dropdown-item" href="slip/Admission_Print.aspx?session_Id=<%#Eval("Session_id") %>&admission_no=<%#Eval("admissionserialnumber") %>" target="_blank" title="Re-Print Adm. Slip"><i class='bx bx-printer'></i><span>Re-Print Adm. Slip</span> </a>
                                                                                        </li> 
                                                                                    <li>
                                                                                        <asp:Panel ID="pnl_print_student" runat="server">
                                                                                            <a class="dropdown-item" href="student-details.aspx?admNo=<%#Eval("admissionserialnumber") %>&ssion=<%#Eval("Session_id") %>&clss=<%#Eval("Class_id") %>" target="_blank"><i class='bx bx-printer'></i><span>Print Student Details</span></a>
                                                                                        </asp:Panel>

                                                                                        <asp:Panel ID="pnl_print_student_new" runat="server">
                                                                                            <a class="dropdown-item" href="slip/print-student-info.aspx?admNo=<%#Eval("admissionserialnumber") %>&ssion=<%#Eval("Session_id") %>&clss=<%#Eval("Class_id") %>" target="_blank"><i class='bx bx-printer'></i><span>Print Student Details</span></a>
                                                                                        </asp:Panel>
                                                                                    </li>
                                                                                    <li>
                                                                                        <asp:Panel ID="pnl_print_student_ladger" runat="server">
                                                                                            <a class="dropdown-item" href="slip/Student_Payment_History.aspx?admNo=<%#Eval("admissionserialnumber") %>&Session=<%#Eval("Session_id") %>&class=<%#Eval("Class_id") %>" target="_blank"><i class='bx bx-printer'></i><span>Print Payment History</span></a>
                                                                                        </asp:Panel>
                                                                                    </li>
                                                                                    <li>
                                                                                        <asp:LinkButton ID="lnkEdit" class="dropdown-item" runat="server" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Edit"><i class="lni lni-pencil-alt"></i><span>Edit Student Details</span></asp:LinkButton>
                                                                                    </li>

                                                                                    <li>
                                                                                        <asp:LinkButton ID="lnk_upload_image" class="dropdown-item" runat="server" CausesValidation="false" OnClick="lnk_upload_image_Click" ToolTip="Documents"><i class="bx bxs-image-add"></i><span>Document Details</span></asp:LinkButton>
                                                                                    </li>
                                                                                </ul>
                                                                            </div>
                                                                            <%--<asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"> </i></asp:LinkButton>--%>
                                                                            <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>

                                                                            <asp:Label ID="Label3" runat="server" Text='<%#Bind("fathername")%>' Visible="false"></asp:Label>
                                                                            <asp:Label ID="lbl_session_id" runat="server" Text='<%#Bind("Session_id")%>' Visible="false"></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_admissionserialnumber" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                        </td>




                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                        </td>


                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="Label1" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="Label2" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="Label11" runat="server" Text='<%#Bind("fathername")%>'></asp:Label>
                                                                        </td>

                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_mobile2" runat="server" Text='<%#Bind("father_mob")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_dob" runat="server" Text='<%#Bind("dob")%>'></asp:Label>
                                                                        </td>


                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="Label4" runat="server" Text='<%#Bind("dateofadmission")%>'></asp:Label>
                                                                        </td>

                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_admissiontype" runat="server" Text='<%#Bind("Admission_Type")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_pwd" runat="server" Text='<%#Bind("Pwd")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_status" runat="server" Text='<%#Bind("Status_name")%>'></asp:Label>
                                                                        </td>
                                                                         <td style="text-align: left;">
                                                                            <asp:Label ID="Label15" runat="server" Text='<%#Bind("Admission_no_date")%>'></asp:Label>
                                                                        </td>

                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_pament" Visible="false" runat="server"></asp:Label>
                                                                            <asp:Label ID="lbl_pay_status" Visible="false" runat="server" Text='<%#Bind("payment_status")%>'></asp:Label>
                                                                            <asp:Label ID="lbl_entry_date" runat="server" Text='<%#Bind("Created_date")%>'></asp:Label>
                                                                        </td>

                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="Label13" runat="server" Text='<%#Bind("gender")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="Label14" runat="server" Text='<%#Bind("blood_group")%>'></asp:Label>
                                                                        </td>


                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="Label12" runat="server" Text='<%#Bind("mothername")%>'></asp:Label>
                                                                        </td>


                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="Label5" runat="server" Text='<%#Bind("careof")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="Label6" runat="server" Text='<%#Bind("city")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="Label7" runat="server" Text='<%#Bind("district")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="Label8" runat="server" Text='<%#Bind("postoffice")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="Label9" runat="server" Text='<%#Bind("policestation")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="Label10" runat="server" Text='<%#Bind("pin")%>'></asp:Label>
                                                                        </td>
                                                                    </tr>
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
            </div>
        </div>
        <!--end row-->
    </div>






    <div id="MdlDocumentS" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog" style="max-width: 750px;">
            <div class="modal-content">
                <div class="modal-header" style="padding: 3px 17px;">
                    <h5 class="modal-title" style="font-size: 18px; padding: 5px 0px 0px 0px;">Uploaded Images</h5>
                    <a href="#!" data-dismiss="modal" style="margin: 5px 0px 0px 0px !important; padding: 3px 5px 4px 5px; background: #fa2020; border: #9b0202;"
                        class="btn btn-primary find-dv-btn">Close</a>
                </div>
                <div class="modal-body">
                    <div class="p-4 border rounded" style="float: left; width: 100%; padding: 0px !important;">
                        <div class="disc-tbl-wprs">
                            <table class="table table-striped table-bordered dataTable">
                                <asp:Repeater ID="rd_view_docs" runat="server" OnItemDataBound="rd_view_docs_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:Label ID="lbl_Name" runat="server" Text='<%#Bind("Document_type")%>'></asp:Label>
                                            </td>

                                            <td style="text-align: left;">
                                                <asp:FileUpload ID="FileUpload1" runat="server" />
                                                <asp:Label ID="lbl_column_name" runat="server" Text='<%#Bind("Column_name")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lbl_column_name_for_online_reg" runat="server" Text='<%#Bind("Online_reg_column")%>' Visible="false"></asp:Label>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:LinkButton ID="btn_upload_image" OnClick="btn_upload_image_Click" runat="server" Style="background: #c203e5; padding: 3px 10px 6px 10px; color: #fff; font-weight: 600; font-size: 14px; border-radius: 3px;">Save</asp:LinkButton>
                                            </td>
                                            <td style="text-align: left;">
                                                <a target="_blank" runat="server" id="stdimgateg">
                                                    <img runat="server" id="stdimages" style="max-width: 100px; max-height: 80px;" />
                                                </a>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>






    <asp:HiddenField ID="hd_id" runat="server" />
    <!--end page wrapper -->



    <asp:HiddenField ID="hd_session" runat="server" />
    <asp:HiddenField ID="hd_class" runat="server" />
    <asp:HiddenField ID="hd_section" runat="server" />
    <asp:HiddenField ID="hd_student_Type" runat="server" />

    <%-- <script src="../Echart/echarts.min.js"></script>
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>--%>
    <%--<script type="text/javascript">
        google.load("visualization", "1", { packages: ["corechart"] });
        google.setOnLoadCallback(drawChart);
        function drawChart() {
            var options = {
                title: 'Student Summary',
                width: 980,
                height: 420,
                legend: { position: 'top', maxLines: 3 },
                bar: { groupWidth: '95%' },
                isStacked: true,
                is3D: true,
                colors: ['#5470C6', '#91CC75', '#ec8f6e', '#f3b49f', '#f6c7b6'],
                hAxis: {
                    textStyle: {
                        fontSize: 10, // or the number you want
                        is3D: true,
                        italic: true
                    }
                }
            };
            $.ajax({

                type: "POST",
                url: "student-list.aspx/GetChartData",
                data: "{Session: '" + $('#<%=hd_session.ClientID%>').val() + "', Class_id: '" + $('#<%=hd_class.ClientID%>').val() + "', Section: '" + $('#<%=hd_section.ClientID%>').val() + "', Student_type: '" + $('#<%=hd_student_Type.ClientID%>').val() + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    var data = google.visualization.arrayToDataTable(r.d);
                    var chart = new google.visualization.ColumnChart($("#chart")[0]);


                    chart.draw(data, options);
                },
                failure: function (r) {
                    alert(r.d);
                },
                error: function (r) {
                    alert(r.d);
                }
            });
        }


        //==============================Order Status SummarY

        $(document).ready(function () {

            var Session = $('#<%= hd_session.ClientID %>').val();
            var Class = $('#<%= hd_class.ClientID %>').val();
            var Section = $('#<%= hd_section.ClientID %>').val();
            var Student_Type = $('#<%= hd_student_Type.ClientID %>').val();


            var myChart2 = echarts.init(document.getElementById('daily_collection'));
            myChart2.setOption({

                title: {
                    text: '',
                    subtext: '',
                    left: 'center'
                },
                tooltip: {
                    trigger: 'item',
                    formatter: '{a} <br/>{b} : {c} ({d}%)'
                },
                legend: {
                    orient: 'vertical',
                    left: 'left',
                    data: ['IPD', 'OPD', 'EMERGNCY', 'PROCEDURE', 'LAB']
                },
                series: [
                    {
                        name: 'Student',
                        type: 'pie',
                        radius: '55%',
                        center: ['50%', '60%'],
                        data: [],
                        emphasis: {
                            itemStyle: {
                                shadowBlur: 10,
                                shadowOffsetX: 0,
                                shadowColor: 'rgba(0, 0, 0, 0.5)'
                            }
                        }
                    }
                ]
            });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "graph.asmx/Get_student_status_summary_counts",
                data: "{Session: '" + Session + "', Class_id: '" + Class + "', Section: '" + Section + "', Student_type: '" + Student_Type + "'}",
                dataType: "json",
                success: function (response) {
                    var JSONObject = JSON.parse(response.d);
                    //alert(response.d);
                    myChart2.setOption({
                        legend: {

                            data: JSONObject["nmv"],
                        },

                        series: [{
                            data: JSONObject["nmv"],
                        }
                        ]
                    });
                },

            });
        });
    </script>--%>
</asp:Content>
