<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="discount-group-wsie-student.aspx.cs" Inherits="school_web.Admin.discount_group_wsie_student" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Discount Group-wise Student List
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server"> 
    <link href="../font-awesome-4.0.3/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../assets/dropdownmultiselection/style.css" rel="stylesheet" />
    <script src="../assets/dropdownmultiselection/bootstrap-multiselect.js"></script>
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
        .head {
            display: none;
        }

        label {
            display: inline-block !important;
            font-size: 13px !important;
            color: #000;
            margin-left: 9px !important;
            font-weight: bold;
        }

        .home-grph-wpr-dv {
            width: 100%;
            float: left;
            overflow: hidden;
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

        .modal-header {
            padding: 0.3rem 1rem;
        }

        .hdrmodify table tr th {
            text-align: left;
            font-size: 12px;
            color: #fff;
        }

        tbody, td, tfoot, th, thead, tr {
            font-size: 12px;
            text-align: left;
        }

        .popupheadinG {
            margin: -1px 0px 0px 0px;
            padding: 2px 0px 3px 5px;
            width: 100%;
            float: left;
            font-size: 16px;
            font-weight: 500;
            border-bottom: 1px solid #ddd;
            border-top: 1px solid #ddd;
            background: #ffba5f;
            color: #000;
        }

        .find-dv-lbl {
            margin: 0px !important;
        }
    </style>

    <script type="text/javascript">
        $(function () {
            $("#<%=ddl_classs.ClientID%>").multiselect({
                includeSelectAllOption: true
            });
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $("#<%=ddl_sections.ClientID%>").multiselect({
                includeSelectAllOption: true
            });
        });
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
                <div class="breadcrumb-title pe-3"><a href="student-report-home.aspx" class="backlnk-css"><i class="bx bx-arrow-back"></i></a>Report</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Discount Group-wise Student List</li>
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
                                            <div class="row">
                                                <div class="col-sm-9">
                                                    <div class="row">
                                                        <div class="col-sm-2">
                                                            <label for="validationCustom01" class="find-dv-lbl">Session</label>
                                                            <asp:DropDownList ID="ddlsession" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>
                                                        </div>
                                                        <div class="col-sm-2">
                                                            <label for="validationCustom01" class="find-dv-lbl">Class</label>
                                                            <%--<asp:DropDownList ID="ddlclass" runat="server" class="form-control find-dv-txtbx" OnSelectedIndexChanged="ddlclass_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>--%>
                                                            <asp:ListBox ID="ddl_classs" runat="server" CssClass="form-select" SelectionMode="Multiple"></asp:ListBox>
                                                        </div>
                                                        <div class="col-sm-2">
                                                            <label for="validationCustom01" class="find-dv-lbl">Section</label>
                                                            <asp:ListBox ID="ddl_sections" runat="server" CssClass="form-select" SelectionMode="Multiple"></asp:ListBox>
                                                            <%--<asp:DropDownList ID="ddl_section" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>--%>
                                                        </div>
                                                        <div class="col-sm-2">
                                                            <label for="validationCustom01" class="find-dv-lbl">Student Type</label>
                                                            <asp:DropDownList ID="ddl_studenttype" runat="server" class="form-control find-dv-txtbx">
                                                                <asp:ListItem Value="ALL">ALL</asp:ListItem>
                                                                <asp:ListItem Value="New">New Admission</asp:ListItem>
                                                                <asp:ListItem Value="NT">Old Admission</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-sm-2">
                                                            <label for="validationCustom01" class="find-dv-lbl">Dis. Group</label>
                                                            <asp:DropDownList ID="ddl_discount_group" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>
                                                        </div>
                                                        <div class="col-sm-2">
                                                            <label for="validationCustom01" class="find-dv-lbl">Dis. Sub-Group</label>
                                                            <asp:DropDownList ID="ddl_discount_sub_group" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-sm-1">
                                                    <asp:Button ID="btn_find" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_find_Click" />
                                                </div>


                                                <div class="col-sm-2">
                                                    <a id="btnExportCSV" class="btn btn-primary find-dv-btn"><i class='bx bx-download'></i>Excel</a>
                                                    <asp:LinkButton ID="btn_excels" Visible="false" Style="display: none" runat="server" OnClick="btn_excels_Click" class="btn btn-primary find-dv-btn">  <i class='bx bx-download'></i> Excel</asp:LinkButton>
                                                    <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" CssClass="btn btn-primary find-dv-btn" Style="margin-left: 10px;" runat="server"
                                                        ToolTip="Print"><i class='bx bx-printer'></i></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>


                                        <div id="tblPrintIQ" runat="server">
                                            <div class="prnt-dv-wpr printborder">
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
                                                            <span style="font-size: 14px; font-weight: bold;">Discount Group-wise Student List
                                                                <asp:Label ID="lbl_class22" runat="server"></asp:Label></span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="grd-wpr">
                                                    <div class="table-responsive">
                                                        <table id="datatable" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                            <thead>
                                                                <tr>
                                                                    <th>#</th>
                                                                    <th>Session</th>
                                                                    <th>Discount Group</th>
                                                                    <th>Discount Sub-Group</th>
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
                                                                <asp:Repeater ID="rd_view" runat="server">
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                            </td> 
                                                                            <td>
                                                                                <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                            </td>


                                                                            <td>
                                                                                <asp:Label ID="Label3" runat="server" Text='<%#Bind("Category_name")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label16" runat="server" Text='<%#Bind("SubCategory_name")%>'></asp:Label>
                                                                            </td>

                                                                            <td>
                                                                                <asp:Label ID="lbl_admissionserialnumber" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
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
                                                                                <asp:Label ID="Label2" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label11" runat="server" Text='<%#Bind("fathername")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_mobile2" runat="server" Text='<%#Bind("father_mob")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_dob" runat="server" Text='<%#Bind("dob")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label4" runat="server" Text='<%#Bind("dateofadmission")%>'></asp:Label>
                                                                            </td>

                                                                            <td>
                                                                                <asp:Label ID="lbl_admissiontype" runat="server" Text='<%#Bind("Admission_Type")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_pwd" runat="server" Text='<%#Bind("Pwd")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_status" runat="server" Text='<%#Bind("Status_name")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label15" runat="server" Text='<%#Bind("Admission_no_date")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbl_pament" Visible="false" runat="server"></asp:Label>
                                                                                <asp:Label ID="lbl_pay_status" Visible="false" runat="server" Text='<%#Bind("payment_status")%>'></asp:Label>
                                                                                <asp:Label ID="lbl_entry_date" runat="server" Text='<%#Bind("Created_date")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label13" runat="server" Text='<%#Bind("gender")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label14" runat="server" Text='<%#Bind("blood_group")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label12" runat="server" Text='<%#Bind("mothername")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label5" runat="server" Text='<%#Bind("careof")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label6" runat="server" Text='<%#Bind("city")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label7" runat="server" Text='<%#Bind("district")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label8" runat="server" Text='<%#Bind("postoffice")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label9" runat="server" Text='<%#Bind("policestation")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
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
        </div>
        <!--end row-->
    </div>


    <script>
        $("#btnExportCSV").click(function () {
            var csv = [];
            $("#datatable tr").each(function () {
                var row = [];
                $(this).find("th,td").each(function () {
                    var text = $(this).text().trim();

                    // Escape double quotes and wrap value in quotes to handle commas
                    text = '"' + text.replace(/"/g, '""') + '"';

                    row.push(text);
                });
                csv.push(row.join(","));
            });

            var csvString = csv.join("\n");
            var blob = new Blob([csvString], { type: "text/csv;charset=utf-8;" });
            var link = document.createElement("a");

            link.href = URL.createObjectURL(blob);
            link.download = "StudentList.csv";
            link.click();
        });
    </script>

</asp:Content>
