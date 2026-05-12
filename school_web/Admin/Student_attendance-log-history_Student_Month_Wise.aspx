<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Student_attendance-log-history_Student_Month_Wise.aspx.cs" Inherits="school_web.Admin.Student_attendance_log_history_Student_Month_Wise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Student Biometric Month wise Attendance
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="../assets/css/Print.css" rel="stylesheet" />
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
    <style>
        .head {
            display: none;
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
                <div class="breadcrumb-title pe-3">Student Attendance</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Student Biometric Month wise Attendance</li>
                        </ol>
                    </nav>
                </div>
            </div>


            <div class="row">
                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase">Student Biometric Month wise Attendance</h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="find-dv">
                                                <div class="row">


                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Session</label>
                                                        <asp:DropDownList ID="ddlsession" runat="server" class="form-control find-dv-txtbx"  ></asp:DropDownList>
                                                    </div>

                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Class</label>
                                                        <asp:DropDownList ID="ddlclass" runat="server" class="form-control find-dv-txtbx" OnSelectedIndexChanged="ddlclass_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl" style="width: auto;">Section</label>
                                                       
                                                        <asp:DropDownList ID="ddl_section" OnSelectedIndexChanged="ddl_section_SelectedIndexChanged" AutoPostBack="true"  runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>
                                                    </div>



                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Year</label>
                                                        <asp:DropDownList ID="ddl_year" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Month</label>
                                                        <asp:DropDownList ID="ddl_month" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <label for="validationCustom01" class="find-dv-lbl">Student</label>
                                                        <asp:DropDownList ID="ddl_employee_name" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-1">
                                                        <asp:Button ID="btn_find" runat="server" Text="find" class="btn btn-primary find-dv-btn" OnClick="btn_find_Click" />
                                                    </div>
                                                </div>
                                            </div>

                                            <asp:Panel ID="pnl_grids" runat="server" Visible="false">
                                                <div class="prnt-btn-sec">
                                                    <div class="prnt-btn-wpr">
                                                        <div class="print-btn-sec" style="text-align: center">
                                                            <div class="noPrint">
                                                                <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" CssClass="print-btn" runat="server" Style="background-color: #009f25; border: 5px solid #009f25; cursor: pointer; float: none; display: inline-block; width: 40px; height: 40px;"
                                                                    ToolTip="Print"></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>




                                                <div id="tblPrintIQ" runat="server">
                                                    <div class="prnt-dv-wpr">
                                                        <div class="pgslry-head-div head">

                                                            <div style="margin: 0px; padding: 0px; height: 110px; width: 20%; float: left;">
                                                                <asp:Image ID="imglogo" runat="server" Style="height: 100px; width: 100px" />
                                                            </div>
                                                            <div style="margin: 0px; padding: 0px; height: 110px; width: 80%; float: left;">
                                                                <h1 style="margin: 0px 0px 0px 0px; width: 100%; padding: 0px; font-weight: bold; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 20px; text-decoration: underline;">
                                                                    <asp:Label ID="lbl_heading" runat="server"></asp:Label>


                                                                </h1>

                                                                <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                    <asp:Label ID="lbl_address" runat="server"></asp:Label>


                                                                </div>
                                                                <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                    Email Id. :<asp:Label ID="lbl_emaiid" runat="server"></asp:Label>

                                                                    &nbsp;&nbsp;

                            website :
                            <asp:Label ID="lbl_website" runat="server"></asp:Label>
                                                                </div>
                                                                <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                    Contact Details :<asp:Label ID="lbl_contact_details" runat="server"></asp:Label>


                                                                </div>
                                                            </div>


                                                        </div>

                                                        <div style="margin: 0px; padding: 0px; float: left; width: 100%">

                                                            <table style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%;">

                                                                <tr>
                                                                    <td style="padding: 5px;">Student Name</td>
                                                                    <td style="padding: 5px;">

                                                                        <asp:Label ID="lbl_ttl_studentname" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                    </td>
                                                                    <td style="padding: 5px;">Admission No.</td>
                                                                    <td style="padding: 5px;">

                                                                        <asp:Label ID="lbl_admission_no" runat="server" Font-Bold="true"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="padding: 5px;">Session </td>
                                                                    <td style="padding: 5px;">

                                                                        <asp:Label ID="lbl_session" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                    </td>
                                                                    <td style="padding: 5px;">Roll No. </td>
                                                                    <td style="padding: 5px;">

                                                                        <asp:Label ID="lbl_roll_no" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>

                                                                    <td style="padding: 5px;">Section</td>
                                                                    <td style="padding: 5px;">

                                                                        <asp:Label ID="lbl_section" runat="server" Font-Bold="true"></asp:Label>
                                                                    </td>
                                                                    <td style="padding: 5px;">Date</td>
                                                                    <td style="padding: 5px;">

                                                                        <asp:Label ID="lbl_date" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                    </td>
                                                                </tr>

                                                            </table>
                                                        </div>


                                                        <asp:GridView ID="grd_attendance" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="False" AllowPaging="false" Font-Bold="False" Style="margin-top: 0; width: 100%; overflow: scroll" class="table table-striped table-bordered dataTable">
                                                            <RowStyle />
                                                            <Columns>

                                                                <asp:TemplateField HeaderText="Date">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_date" runat="server" Text='<%#Bind("Date") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Day">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Day" runat="server" Text='<%#Bind("Day") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Working">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Working" runat="server" Text='<%#Bind("Working") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Employee Attendance Log Detail">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_attendance" runat="server" Text='<%#Bind("Attendance") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
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
