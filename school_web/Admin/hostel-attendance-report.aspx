<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="hostel-attendance-report.aspx.cs" Inherits="school_web.Admin.hostel_attendance_report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Hostel Attendance Report
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="../assets/css/Print.css" rel="stylesheet" />
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
                <div class="breadcrumb-title pe-3">Hostel</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Hostel Attendance Report</li>
                        </ol>
                    </nav>
                </div>
            </div>


            <div class="row">
                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase">Hostel Attendance Report</h6>
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
                                                        <asp:DropDownList ID="ddlsession" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">House</label>
                                                        <asp:DropDownList ID="ddl_house" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Date</label>
                                                        <asp:TextBox ID="txt_date" runat="server" class="form-control find-dv-txtbx datepicker"></asp:TextBox>
                                                    </div>

                                                    <div class="col-sm-3">
                                                        <asp:Button ID="btn_find" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_find_Click" />
                                                        <asp:ImageButton ID="imgexcel2" runat="server" Visible="false" ImageUrl="~/images/excel_con.png" CssClass="excelbutton22" OnClick="imgexcel2_Click"
                                                            Style="height: 26px; width: 32px; margin-top: 1px; margin: 18px 0px 0px 13px;" />

                                                        <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" CssClass="print-btn" runat="server" Style="background-color: #009f25; border: 5px solid #009f25; cursor: pointer; float: none; display: inline-block; width: 32px; height: 26px; margin: 22px 0px 0px 13px;"
                                                            ToolTip="Print"></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>

                                            <asp:Panel ID="pnl_grids" runat="server" Visible="false">
                                                <div id="tblPrintIQ" runat="server">
                                                    <div class="prnt-dv-wpr">
                                                        <div class="pgslry-head-div head">
                                                            <div style="margin: 0px; padding: 0px; height: 128px; width: 20%; float: left;">
                                                                <asp:Image ID="imglogo" runat="server" Style="height: 100px; width: 100px" />
                                                            </div>
                                                            <div style="margin: 0px; padding: 0px; height: 128px; width: 80%; float: left;">
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
                                                                    Hostel Attendance Report :<asp:Label ID="lbll_section_and_class" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div style="margin: 0px; padding: 0px; float: left; width: 100%">
                                                            <table style="margin: 0px; padding: 0px; width: 100%" border="1">
                                                                <tr>
                                                                    <td style="padding: 5px; text-align: right!important; font-weight: bold!important;">Total Student
                                                                    </td>

                                                                    <td style="background-color: #020dea; color: #000!important; padding: 0px 6px 0px 8px; text-align: center;">
                                                                        <asp:Label ID="lbltotal_student" Font-Bold="true" runat="server" ForeColor="White">0</asp:Label>
                                                                    </td>
                                                                    <td style="padding: 5px; text-align: right!important; font-weight: bold!important;">Total Present Student
                                                                    </td>

                                                                    <td style="background-color: #009f25; color: #000!important; padding: 0px 6px 0px 8px; text-align: center;">
                                                                        <asp:Label ID="lbl_persenstudent" Font-Bold="true" runat="server" ForeColor="White">0</asp:Label>
                                                                    </td>

                                                                    <td style="padding: 5px; text-align: right!important; font-weight: bold!important;">Total Absent Student
                                                                    </td>

                                                                    <td style="background-color: #f00; color: #000!important; padding: 0px 6px 0px 8px; text-align: center;">
                                                                        <asp:Label ID="lbl_totalabsentstudent" Font-Bold="true" runat="server" ForeColor="White">0</asp:Label>
                                                                    </td>
                                                                    <td style="padding: 5px; text-align: right!important; font-weight: bold!important;">Total Leave Student</td>

                                                                    <td style="background-color: #ff6a00; color: #000!important; padding: 0px 6px 0px 8px; text-align: center;">
                                                                        <asp:Label ID="lbl_leave_student" Font-Bold="true" runat="server" ForeColor="White">0</asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <asp:GridView ID="GrdView" runat="server" class="table table-hover table-bordered" AutoGenerateColumns="False" Width="100%" OnRowDataBound="GrdView_RowDataBound">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Sl. No.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Student Name">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_FullName" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Admission No.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_reg_id" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>


                                                                    <asp:TemplateField HeaderText="Class">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_classname" runat="server" Text='<%#Bind("classname")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Section">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Roll No.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_roll_no" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Date">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_date" runat="server" Text='<%#Bind("Attendance_Date")%>'></asp:Label>
                                                                            <asp:Label ID="lbl_time" runat="server" Text='<%#Bind("time")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Day">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_day" runat="server" Text='<%#Bind("Day")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>


                                                                    <asp:TemplateField HeaderText="Attendance Status">
                                                                        <ItemTemplate>

                                                                            <asp:Label ID="lbl_Attendance_Status" runat="server" Text='<%#Bind("Attendance_Status")%>'></asp:Label>


                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
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
