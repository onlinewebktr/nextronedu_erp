<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Scholarship_All_Qualified_Student_List.aspx.cs" Inherits="school_web.Admin.Scholarship_All_Qualified_Student_List" EnableEventValidation="false" ValidateRequest="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Qualified Scholarship Student List
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
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

        tfoot, th, thead {
            border-color: inherit;
            border-style: solid;
            border-width: 0;
            vertical-align: middle;
            background: #1c77fd !important;
            font-size: 12px !important;
            font-weight: bold;
        }

        td {
            font-size: 12px !important;
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
                <div class="breadcrumb-title pe-3">Scholarship</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Qualified Scholarship Student List  </li>
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

                            <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">



                                <div class="row">



                                    <div class="col-sm-12">
                                        <div class="row">
                                            <div class="col-md-3">
                                                <label for="validationCustom01" class="form-label">Scholarship <sup>*</sup></label>
                                                <asp:DropDownList ID="ddl_Scholarship_name" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_Scholarship_name_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <label for="validationCustom01" class="form-label">Scholarship For<sup>*</sup></label>
                                                <asp:DropDownList ID="ddl_class" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">
                                                <label for="validationCustom01" class="form-label">Status<sup>*</sup></label>
                                                <asp:DropDownList ID="ddl_Transferred" runat="server" class="form-select find-dv-txtbx">
                                                    <asp:ListItem>ALL</asp:ListItem>
                                                    <asp:ListItem>Transferred</asp:ListItem>
                                                    <asp:ListItem>Not Transferred</asp:ListItem>

                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-1">
                                                <asp:Button ID="btn_find" runat="server" Text="Find" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btn_find_Click" Style="margin: 24px 0px 0px 0px; padding: 3px 10px; width: 60px!important; height: 31px!important;" />
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:LinkButton ID="btn_excels" runat="server" Style="margin: 24px 0px 6px 0px !important;" OnClick="btn_excels_Click" class="btn btn-primary find-dv-btn" Visible="false"><i class='bx bx-download'></i> Excel</asp:LinkButton>
                                                <asp:LinkButton ID="print1" Visible="false" OnClientClick="return PrintPanel()" Style="margin: 24px 0px 6px 9px !important;" CssClass="btn btn-primary find-dv-btn" runat="server"
                                                    ToolTip="Print"><i class='bx bx-printer'></i></asp:LinkButton>
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
                                                            <span style="font-size: 14px; font-weight: bold;">All Qualified Scholarship Student List 
                                                                        <asp:Label ID="lbl_class22" runat="server"></asp:Label></span>


                                                        </div>
                                                    </div>


                                                </div>




                                            </div>
                                            <asp:Panel ID="Panel1" runat="server">
                                                <table id="example2" class="table table-striped table-bordered dataTable" data-page-length='2000' role="grid" aria-describedby="example2_info">
                                                    <thead>
                                                        <tr>
                                                            <th>#</th>

                                                            <th>Reg. No.</th>
                                                            <th>Name</th>
                                                            <th>Scholarship for</th>

                                                            <th>Admission Date</th>
                                                            <th>Time</th>
                                                            <th>Attendance Status</th>
                                                            <th>Exam Result</th>

                                                            <th>Full Marks</th>
                                                            <th>Obtain Marks</th>
                                                            <th>Obtain(%)</th>
                                                            <th>Rank</th>
                                                            <th>Is Transferred To Admission</th>
                                                            <th>Action</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="rd_view" runat="server" OnItemDataBound="rd_view_ItemDataBound">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                    </td>

                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_Registration_id" runat="server" Text='<%#Bind("Registration_id")%>'></asp:Label>
                                                                        <asp:Label ID="lbl_testid" runat="server" Text='<%#Bind("Test_id")%>' Visible="false"></asp:Label>
                                                                        <asp:Label ID="Label2" runat="server" Text='<%#Bind("Class_id")%>' Visible="false"></asp:Label>
                                                                        <asp:Label ID="lbl_Session_id" runat="server" Text='<%#Bind("Session_id")%>' Visible="false"></asp:Label>
                                                                        <asp:Label ID="lbl_id" runat="server" Text='<%#Bind("resultid")%>' Visible="false"></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_Name" runat="server" Text='<%#Bind("Name")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("Class")%>'></asp:Label>
                                                                    </td>


                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_Admission_date" runat="server" Text='<%#Bind("Admission_date1")%>'></asp:Label>

                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_Admission_time" runat="server" Text='<%#Bind("Admission_time1")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_attendance_status" runat="server" Text='<%#Bind("Attendance_Status")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_Exam_Result" runat="server" Text='<%#Bind("Exam_Result")%>'></asp:Label>

                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_Full_Marks" runat="server" Text='<%#Bind("Full_Marks")%>'></asp:Label>
                                                                    </td>


                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_Obtain_Marks" runat="server" Text='<%#Bind("Obtain_Marks")%>'></asp:Label>
                                                                    </td>

                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_Obtain_percentage" runat="server" Text='<%#Bind("Obtain_percentage")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_Obtain_rank" runat="server" Text='<%#Bind("Obtain_rank")%>'></asp:Label>
                                                                    </td>

                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_is_transfer" runat="server"></asp:Label>
                                                                    </td>


                                                                    <td>


                                                                        <div class="user-box dropdown" style="float: left; display: inherit; height: auto; border-left: 0px solid #f0f0f0; margin-left: 0px;">
                                                                            <a class="d-flex align-items-center nav-link dropdown-toggle dropdown-toggle-nocaret" style="font-size: 29px; padding: 0px; border: 0px;"
                                                                                href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                                                                                <div class="user-info ps-3" style="padding: 0px !important; border: 0px !important;">
                                                                                    <i class="bx bx-grid-horizontal"></i>
                                                                                </div>
                                                                            </a>
                                                                            <ul class="dropdown-menu dropdown-menu-end">


                                                                                <li>
                                                                                    <asp:Panel ID="Panel2" runat="server">
                                                                                        <a class="dropdown-item" href="admission.aspx?regiDs=<%#Eval("Registration_id") %>&transfrScholarship=1" target="_blank"><span>Take Admission</span> </a>

                                                                                    </asp:Panel>
                                                                                    <asp:Label ID="lbl_Is_admission_no_updated" runat="server" Text='<%#Bind("Is_admission_no_updated")%>' Visible="false"></asp:Label>
                                                                                </li>

                                                                            </ul>
                                                                        </div>
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
</asp:Content>
