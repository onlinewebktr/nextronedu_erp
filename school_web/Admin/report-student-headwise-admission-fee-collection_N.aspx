<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="report-student-headwise-admission-fee-collection_N.aspx.cs" Inherits="school_web.Admin.report_student_headwise_admission_fee_collection_N" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Student Wise Admission Fee Collection
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        tbody, td, tfoot, th, thead, tr {
            vertical-align: top;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            var sessionid = $("#<%=ddl_session.ClientID%>").val();
            $("#<%=txt_student_name.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'report-student-headwise-admission-fee-collection_N.aspx/GetRooPath',
                        data: "{ 'PathRooT': '" + request.term + "',Session_id:'" + sessionid + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            if (data.d.length > 0) {
                                response($.map(data.d, function (item) {
                                    return {
                                        label: item
                                    };
                                }))
                            } else {
                                response([{ label: 'No results found.' }]);
                            }
                        }
                    });
                },
                select: function (e, u) {
                    if (u.item.val == -1) {
                        return false;
                    }
                }
            });
        });

        $(function () {
            var sessionid = $("#<%=ddl_session.ClientID%>").val();
            $("#<%=txt_admission_no.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'report-student-headwise-admission-fee-collection_N.aspx/GetRooPathAdmNo',
                        data: "{ 'PathRooT': '" + request.term + "',Session_id:'" + sessionid + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            if (data.d.length > 0) {
                                response($.map(data.d, function (item) {
                                    return {
                                        label: item
                                    };
                                }))
                            } else {
                                response([{ label: 'No results found.' }]);
                            }
                        }
                    });
                },
                select: function (e, u) {
                    if (u.item.val == -1) {
                        return false;
                    }
                }
            });
        });
    </script>


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
                <div class="breadcrumb-title pe-3"><a href="fee-report.aspx" class="backlnk-css"><i class="bx bx-arrow-back"></i></a>Report</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Student & Head wise Admission Fees Collection</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">



                <div class="col-xl-12">
                    <ul class="sub-pag-menu-ul">
                        <li><a href="report-today-fees-collection.aspx">Today Fee Collection</a></li>
                        <li><a href="admission-fee-collection-report.aspx">Today Fees Collection</a></li>
                        <li><a href="report-headwise-fee-collection.aspx">Head wise Fee Collection</a></li>
                        <li><a href="report-student-wise-admission-fee-collection.aspx">Total Accumulated Student Wise Fee Collection</a></li>
                        <li><a href="report-student-headwise-admission-fee-collection_N.aspx" class="sub-mnu-p-a-active">Student & Head Wise Fee Collection</a></li>
                    </ul>
                    </div>
                <div class="col-xl-12">
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <asp:Repeater ID="rp_month" runat="server" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_content_id" runat="server" Visible="false" Text='<%#Bind("content_id")%>'></asp:Label>
                                                    <asp:Label ID="lbl_feetype" runat="server" Visible="false" Text='<%#Bind("feetype")%>'></asp:Label> 
                                                </ItemTemplate>
                                            </asp:Repeater>


                                            <div class="find-dv">
                                                <div class="row">
                                                    <div class="col-sm-2" >
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <label for="validationCustom01" class="find-dv-lbl">Payment Mode</label>
                                                                <asp:DropDownList ID="ddl_paymentmode" runat="server" class="form-select find-dv-txtbx">
                                                                    <asp:ListItem>All</asp:ListItem>
                                                                    <asp:ListItem>Cash</asp:ListItem>
                                                                    <asp:ListItem>Pos</asp:ListItem>
                                                                    <asp:ListItem>Netbanking</asp:ListItem>
                                                                    <asp:ListItem>Deposited In Bank</asp:ListItem>
                                                                    <asp:ListItem>Sbdebit</asp:ListItem>
                                                                    <asp:ListItem>Cheque</asp:ListItem>
                                                                    <asp:ListItem>NEFT</asp:ListItem>
                                                                    <asp:ListItem>Debitcard</asp:ListItem>
                                                                    <asp:ListItem>Creditcard</asp:ListItem>
                                                                    <asp:ListItem>Otherdcard</asp:ListItem>
                                                                    <asp:ListItem>UPI</asp:ListItem>
                                                                    <asp:ListItem>Online</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4" runat="server" id="fnds1">
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
                                                                <asp:Button ID="btn_find" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_find_Click" />
                                                            </div>
                                                        </div>
                                                    </div>



                                                    <div class="col-sm-4" runat="server" id="fnds2">
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




                                                    <div class="col-sm-4" runat="server" id="fnds3" visible="false">
                                                        <div class="row">
                                                           <div class="col-sm-4">
                                                                <label for="validationCustom01" class="find-dv-lbl">Session</label>
                                                                <asp:DropDownList ID="ddl_session" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true"></asp:DropDownList>
                                                            </div>
                                                            <div class="col-sm-5" style="display: none">
                                                                <label for="validationCustom01" class="find-dv-lbl">Class</label>
                                                                <asp:DropDownList ID="ddl_adm_class" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <label for="validationCustom01" class="find-dv-lbl">Admission No.</label>
                                                                <asp:TextBox ID="txt_admission_no" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-2">
                                                                <asp:Button ID="btn_find_by_adm_no" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_find_by_adm_no_Click" />
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-3" runat="server" id="fnds4" visible="false">
                                                        <div class="row"> 
                                                            <div class="col-sm-9">
                                                                <label for="validationCustom01" class="find-dv-lbl">Student Name</label>
                                                                <asp:TextBox ID="txt_student_name" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-3">
                                                                <asp:Button ID="btn_find_by_student_name" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_find_by_student_name_Click" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-1">
                                                        <asp:LinkButton ID="lnks_filter" runat="server" class="filters-btn" OnClick="lnks_filter_Click" Style="margin: 14px 0px 0px 0px; float: left; font-size: 27px;"><i class="bx bx-filter-alt"></i></asp:LinkButton>
                                                    </div>
                                                    <div class="col-sm-2">
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
                                                                <asp:GridView ID="GridView2" runat="server" CssClass="table table-bordered table-striped" Style="width: 100%" AutoGenerateColumns="true" OnRowCreated="GridView2_RowCreated" ShowFooter="true" OnRowDataBound="GridView2_RowDataBound1">
                                                                </asp:GridView>
                                                                    <div class="paid-cat-div">
                                                                    <div class="row">
                                                                        <div class="col-sm-4">
                                                                            <p class="paid-cat-div-p">
                                                                                <i>Cash </i>:
                                                            <asp:Label ID="lbl_by_cash" runat="server" Text="00"></asp:Label>
                                                                            </p>
                                                                            <p class="paid-cat-div-p">
                                                                                <i>Pos </i>:
                                                            <asp:Label ID="lbl_pos" runat="server" Text="00"></asp:Label>
                                                                            </p>
                                                                            <p class="paid-cat-div-p">
                                                                                <i>Netbanking </i>:
                                                            <asp:Label ID="lbl_by_netbanking" runat="server" Text="00"></asp:Label>
                                                                            </p>
                                                                            <p class="paid-cat-div-p">
                                                                                <i>Deposited In Bank</i> :
                                                            <asp:Label ID="lbl_by_deposit" runat="server" Text="00"></asp:Label>
                                                                            </p>

                                                                        </div>
                                                                        <div class="col-sm-4">
                                                                            <p class="paid-cat-div-p">
                                                                                <i>Sbdebit </i>:
                                                            <asp:Label ID="lbl_by_sb" runat="server" Text="00"></asp:Label>
                                                                            </p>
                                                                            <p class="paid-cat-div-p">
                                                                                <i>Cheque </i>:
                                                            <asp:Label ID="lbl_by_chequeS" runat="server" Text="00"></asp:Label>
                                                                            </p>
                                                                            <p class="paid-cat-div-p">
                                                                                <i>NEFT </i>:
                                                            <asp:Label ID="lbl_by_neft" runat="server" Text="00"></asp:Label>
                                                                            </p>
                                                                            <p class="paid-cat-div-p">
                                                                                <i>Debitcard </i>:
                                                            <asp:Label ID="lbl_by_debitcard" runat="server" Text="00"></asp:Label>
                                                                            </p>

                                                                        </div>

                                                                        <div class="col-sm-4">
                                                                            <p class="paid-cat-div-p">
                                                                                <i>Creditcard </i>:
                                                            <asp:Label ID="lbl_by_credit_card" runat="server" Text="00"></asp:Label>
                                                                            </p>
                                                                            <p class="paid-cat-div-p">
                                                                                <i>Otherdcard </i>:
                                                            <asp:Label ID="lbl_by_other_card" runat="server" Text="00"></asp:Label>
                                                                            </p>
                                                                            <p class="paid-cat-div-p">
                                                                                <i>UPI </i>:
                                                            <asp:Label ID="lbl_by_upi" runat="server" Text="00"></asp:Label>
                                                                            </p>
                                                                            <p class="paid-cat-div-p">
                                                                                <i>Branch</i> :
                                                            <asp:Label ID="lbl_by_branch" runat="server" Text="00"></asp:Label>
                                                                            </p>

                                                                        </div>
                                                                        <div class="col-sm-4">
                                                                            
                                                                            <p class="paid-cat-div-p">
                                                                                <i>Online </i>:
                                                            <asp:Label ID="lbl_online" runat="server" Text="00"></asp:Label>
                                                                            </p>
                                                                            <p class="paid-cat-div-p">
                                                                            </p>
                                                                        </div>

                                                                    </div>






                                                                </div>
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


    <div class="conf-alrt-sec" id="myModal2" runat="server" visible="false">
        <div class="conf-alrt-inr" style="width: 750px;">
            <div class="popupTablWpR">
                <div class="row">
                    <div class="col-md-6">
                        <h2 class="popup-dt-h">Student Details</h2>
                    </div>
                    <div class="col-md-6">
                        <ul class="conf-btn-ul" style="margin: 0px 0px 0px 0px;">
                            <li>
                                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Close</asp:LinkButton>
                            </li>
                        </ul>
                    </div>
                </div>

                <table style="width: 100%;" id="Table1" class="table table-hover table-bordered ">

                    <tr>
                        <th>Student Name</th>
                        <th>Admission No</th>
                        <th>Class</th>
                        <th>Section</th>
                        <th>Roll</th>
                        <th>Father's Name</th>
                        <th>Action</th>
                    </tr>


                    <asp:Repeater ID="rp_std" runat="server">
                        <ItemTemplate>
                            <tr id="row" runat="server">
                                <td>
                                    <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbladmissionserialnumber" runat="server" Text='<%#Bind("admissionserialnumber") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbl_Section" runat="server" Text='<%#Bind("Section") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbl_rollnumber" runat="server" Text='<%#Bind("rollnumber") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbl_fathername" runat="server" Text='<%#Bind("fathername") %>'></asp:Label>
                                    <asp:Label ID="lbl_session" Visible="false" runat="server" Text='<%#Bind("session") %>'></asp:Label>
                                    <asp:Label ID="lbl_class_id" Visible="false" runat="server" Text='<%#Bind("Class_id") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnk_select" runat="server" OnClick="lnk_select_Click">Select</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
    </div>
    <style>
        .conf-btn-ul li a {
            margin: 0px 5px;
            padding: 0px 0px 1px;
            text-decoration: none;
            background: #ff0000;
            color: #fff;
            width: 50px;
            float: right;
            text-align: center;
            border-radius: 3px;
            font-size: 13px;
            line-height: 25px;
            font-weight: 500;
        }

        table tr th {
            padding: 10px 5px !important;
        }

        table tr td {
            padding: 10px 5px !important;
        }
    </style>
</asp:Content>
