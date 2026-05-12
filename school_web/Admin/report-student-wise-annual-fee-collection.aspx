<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="report-student-wise-annual-fee-collection.aspx.cs" Inherits="school_web.Admin.report_student_wise_annual_fee_collection" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Accumulated Student Wise Fee Collection
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .table-responsive {
            overflow-x: inherit;
        }
    </style>

    <script type="text/javascript">
        $(function () {
            var sessionid = $("#<%=ddl_session.ClientID%>").val();
            $("#<%=txt_student_name.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'report-student-wise-annual-fee-collection.aspx/GetRooPath',
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
                        url: 'report-student-wise-annual-fee-collection.aspx/GetRooPathAdmNo',
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
                            <li class="breadcrumb-item active" aria-current="page">Accumulated Student Wise Fee Collection</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase"></h6>
                    <ul class="sub-pag-menu-ul">
                        <li><a href="report-today-fees-collection-annual.aspx">Today Fee Collection Summary</a></li>
                        <li><a href="annual-fee-collection-report.aspx">Today Fees Collection</a></li>
                        <li><a href="report-headwise-fee-collection-annual.aspx">Head wise Fee Collection</a></li>
                        <li><a href="report-student-wise-annual-fee-collection.aspx" class="sub-mnu-p-a-active">Accumulated Student Wise Fee Collection</a></li>
                        <li><a href="report-student-headwise-annual-fee-collection_N.aspx">Student & Head wise Fee Collection</a></li>
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
                                            <div class="find-dv">
                                                <div class="row">

                                                    <div class="col-sm-6" runat="server" id="fnds2">
                                                        <div class="row">
                                                            <div class="col-sm-3">
                                                                <label for="validationCustom01" class="find-dv-lbl">Session</label>
                                                                <asp:DropDownList ID="ddl_session_c" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                            </div>
                                                            <div class="col-sm-4">
                                                                <label for="validationCustom01" class="find-dv-lbl">Class</label>
                                                                <asp:DropDownList ID="ddl_class" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                            </div>
                                                            <div class="col-sm-3">
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
                                                            <div class="col-sm-6">
                                                                <label for="validationCustom01" class="find-dv-lbl">Admission No.</label>
                                                                <asp:TextBox ID="txt_admission_no" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-2">
                                                                <asp:Button ID="btn_find_by_adm_no" runat="server" Text="find" class="btn btn-primary find-dv-btn" OnClick="btn_find_by_adm_no_Click" />
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4" runat="server" id="fnds4" visible="false">
                                                        <div class="row">
                                                            <div class="col-sm-9">
                                                                <label for="validationCustom01" class="find-dv-lbl">Student Name</label>
                                                                <asp:TextBox ID="txt_student_name" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-3">
                                                                <asp:Button ID="btn_find_by_student_name" runat="server" Text="find" class="btn btn-primary find-dv-btn" OnClick="btn_find_by_student_name_Click" />
                                                            </div>
                                                        </div>
                                                    </div>




                                                    <div class="col-sm-1">
                                                        <asp:LinkButton ID="lnks_filter" runat="server" class="filters-btn" OnClick="lnks_filter_Click" Style="margin: 16px 0px 0px 0px; float: left; font-size: 23px;"><i class="bx bx-filter-alt"></i></asp:LinkButton>
                                                    </div>
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
                                                                <asp:GridView ID="GrdView" runat="server" class="table table-bordered" ShowFooter="True" AutoGenerateColumns="False" Width="100%" OnRowDataBound="GrdView_RowDataBound">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Sl No.">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Admission Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_dateofadmission" runat="server" Text='<%#Bind("dateofadmission")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="FY Year">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Admission No.">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_admissionserialnumber" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Class">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Section">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_Section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Roll No.">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_rollnumber" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Student's Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                            </ItemTemplate> 
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Father's Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_father" runat="server" Text='<%#Bind("fathername")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Mother's Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_mother" runat="server" Text='<%#Bind("mothername")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Mobile No.">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_mobile_no" runat="server" Text='<%#Bind("mobilenumber")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <b>Total</b>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Total Fees">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_Payable_amount" runat="server" Text='<%#Bind("Payable_amount")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lbl_total_payable_amount" runat="server" Font-Bold="true"></asp:Label>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Paid Amount">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_Paid_amount" runat="server" Text='<%#Bind("Paid_amount")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lbl_total_paid_amount" runat="server" Font-Bold="true"></asp:Label>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Balance Amount">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_Dues_amount" runat="server" Text='<%#Bind("Dues_amount")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lbl_total_Dues_amount" runat="server" Font-Bold="true"></asp:Label>
                                                                            </FooterTemplate>
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
