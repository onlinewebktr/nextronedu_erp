<%@ Page Title="" Language="C#" MasterPageFile="~/Payroll/Adminmaster.Master" AutoEventWireup="true" CodeBehind="overall-daily-attendance-report.aspx.cs" Inherits="school_web.Payroll.overall_daily_attendance_report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Over All Daily Attendance Report
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
                <div class="breadcrumb-title pe-3">Attendance</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Overall Daily Attendance Report</li>
                        </ol>
                    </nav>
                </div>
            </div>


            <div class="row">
                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase">Overall Daily Attendance Report</h6>
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
                                                        <label for="validationCustom01" class="find-dv-lbl">Select Grade</label>
                                                        <asp:DropDownList ID="ddl_grade" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Date</label>
                                                        <asp:TextBox ID="txt_date" runat="server" class="form-control find-dv-txtbx datepicker"></asp:TextBox>
                                                    </div>


                                                    <div class="col-sm-1">
                                                        <asp:Button ID="btn_find" runat="server" Text="find" class="btn btn-primary find-dv-btn" OnClick="btn_find_Click" />
                                                    </div>

                                                    <div class="col-sm-3"></div>

                                                    <div class="col-sm-3" style="display: none">
                                                        <label for="validationCustom01" class="find-dv-lbl">Filter by Employee Name</label>
                                                        <asp:DropDownList ID="ddl_employee" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-1" style="display: none">
                                                        <asp:Button ID="btn_find_by_emp" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_find_by_emp_Click" />
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
                                                                    <td style="padding: 5px;">Date</td>
                                                                    <td style="padding: 5px;">
                                                                        <asp:Label ID="lbl_date" runat="server" Text="" Font-Bold="true"></asp:Label></td>
                                                                    <td style="padding: 5px;">No of employee</td>
                                                                    <td style="padding: 5px;">
                                                                        <asp:Label ID="lbl_no_of_emp" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="padding: 5px;">Total Absent</td>
                                                                    <td style="padding: 5px;">
                                                                        <asp:Label ID="lbl_absent" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                    </td>
                                                                    <td style="padding: 5px;">Total Present</td>
                                                                    <td style="padding: 5px;">
                                                                        <asp:Label ID="lbl_ttl_present" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="padding: 5px;">Total On leave</td>
                                                                    <td style="padding: 5px;">
                                                                        <asp:Label ID="lbl_on_leave" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                    </td>
                                                                    <td style="padding: 5px;"></td>
                                                                    <td style="padding: 5px;"></td>
                                                                </tr>


                                                            </table>
                                                        </div>







                                                        <asp:GridView ID="grd_attendance" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="False" AllowPaging="false" Font-Bold="False" Style="margin-top: 0; width: 100%; overflow: scroll" class="table table-striped table-bordered dataTable">
                                                            <RowStyle />
                                                            <Columns>
                                                                <%--<asp:TemplateField HeaderText="#">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSRNO" runat="server" Text="<%#Container.DataItemIndex+1 %>"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                                <asp:TemplateField HeaderText="Employee Name" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Employee_Name" runat="server" Text='<%#Bind("Employee_Name") %>'></asp:Label>

                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
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
                                                                <asp:TemplateField HeaderText="Shift 1 in">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Shift_1_in" runat="server" Text='<%#Bind("Shift_1_in") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Shift 1 out">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Shift_1_out" runat="server" Text='<%#Bind("Shift_1_out") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>



                                                                <asp:TemplateField HeaderText="Shift 2 in">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Shift_2_in" runat="server" Text='<%#Bind("Shift_2_in") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Shift 2 out">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Shift_2_out" runat="server" Text='<%#Bind("Shift_2_out") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Attendance">
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

    <!--end page wrapper -->
</asp:Content>
