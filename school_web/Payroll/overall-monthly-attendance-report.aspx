<%@ Page Title="" Language="C#" MasterPageFile="~/Payroll/Adminmaster.Master" AutoEventWireup="true" CodeBehind="overall-monthly-attendance-report.aspx.cs" Inherits="school_web.Payroll.overall_monthly_attendance_report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Overall Monthly Attendance Report
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
                            <li class="breadcrumb-item active" aria-current="page">Overall Monthly Attendance Report</li>
                        </ol>
                    </nav>
                </div>
            </div>


            <div class="row">
                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase">Overall Monthly Attendance Report</h6>
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
                                                        <label for="validationCustom01" class="find-dv-lbl">Select Year</label>
                                                        <asp:DropDownList ID="ddl_year" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Month</label>
                                                        <asp:DropDownList ID="ddl_month" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                    </div>


                                                    <div class="col-sm-1">
                                                        <asp:Button ID="btn_find" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_find_Click" />
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
                                                    <div class="prnt-dv-wpr-mth">
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
                                                                    <td style="padding: 5px;">Month/Year</td>
                                                                    <td style="padding: 5px;">
                                                                        <asp:Label ID="lbl_monthS" runat="server" Text="" Font-Bold="true"></asp:Label>/ <asp:Label ID="lbl_year" runat="server" Text="" Font-Bold="true"></asp:Label></td>

                                                                </tr>
                                                               
                                                            </table>
                                                        </div>


                                                      



                                                        <asp:GridView ID="grd_attendance" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="False" AllowPaging="false" Font-Bold="False" Style="margin-top: 0; width: 100%; overflow: scroll" class="table table-striped table-bordered dataTable">
                                                            <RowStyle />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Emp. Code">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Emp_Code" runat="server" Text='<%#Bind("Emp_Code") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Emp_name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_Emp_Name" runat="server" Text='<%#Bind("Emp_Name") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="1">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_one" runat="server" Text='<%#Bind("one") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="2">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_two" runat="server" Text='<%#Bind("two") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="3">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_three" runat="server" Text='<%#Bind("three") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="4">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_fourn" runat="server" Text='<%#Bind("four") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="5">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_five" runat="server" Text='<%#Bind("five") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="6">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_six" runat="server" Text='<%#Bind("six") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="7">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_seven" runat="server" Text='<%#Bind("seven") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="8">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_eight" runat="server" Text='<%#Bind("eight") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="9">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_nine" runat="server" Text='<%#Bind("nine") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="10">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_ten" runat="server" Text='<%#Bind("ten") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="11">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_eleven" runat="server" Text='<%#Bind("eleven") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="12">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_twelve" runat="server" Text='<%#Bind("twelve") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="13">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_thirteen" runat="server" Text='<%#Bind("thirteen") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="14">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_fourteen" runat="server" Text='<%#Bind("fourteen") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="15">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_fifteen" runat="server" Text='<%#Bind("fifteen") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="16">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_sixteen" runat="server" Text='<%#Bind("sixteen") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="17">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_seventeen" runat="server" Text='<%#Bind("seventeen") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="18">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_eighteen" runat="server" Text='<%#Bind("eighteen") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="19">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_nineteen" runat="server" Text='<%#Bind("nineteen") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="20">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_twenty" runat="server" Text='<%#Bind("twenty") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="21">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_twenty_one" runat="server" Text='<%#Bind("twenty_one") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="22">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_twenty_two" runat="server" Text='<%#Bind("twenty_two") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="23">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_twenty_three" runat="server" Text='<%#Bind("twenty_three") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="24">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_twenty_four" runat="server" Text='<%#Bind("twenty_four") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="25">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_twenty_five" runat="server" Text='<%#Bind("twenty_five") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="26">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_twenty_six" runat="server" Text='<%#Bind("twenty_six") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="27">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_twenty_seven" runat="server" Text='<%#Bind("twenty_seven") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="28">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_twenty_eight" runat="server" Text='<%#Bind("twenty_eight") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="29">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_twenty_nine" runat="server" Text='<%#Bind("twenty_nine") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="30">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_thirty" runat="server" Text='<%#Bind("thirty") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="31">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_thirty_one" runat="server" Text='<%#Bind("thirty_one") %>'></asp:Label>
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
