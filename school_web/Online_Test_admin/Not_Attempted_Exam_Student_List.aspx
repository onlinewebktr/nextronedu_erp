<%@ Page Title="" Language="C#" MasterPageFile="~/Online_Test_admin/Admin.Master" AutoEventWireup="true" CodeBehind="Not_Attempted_Exam_Student_List.aspx.cs" Inherits="school_web.Online_Test_admin.Not_Attempted_Exam_Student_List" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Not Attempted Exam Student List
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



            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3" style="position: relative">
                <div class="breadcrumb-title pe-3">Report</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Not Attempted Exam Student List</li>
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

                                                <div class="col-sm-2">
                                                    <label for="validationCustom01" class="find-dv-lbl">Session</label>
                                                    <asp:DropDownList ID="ddlsession" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddlsession_SelectedIndexChanged"></asp:DropDownList>
                                                </div>

                                                <div class="col-sm-2">
                                                    <label for="validationCustom01" class="find-dv-lbl">Class</label>
                                                    <asp:DropDownList ID="ddl_class" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_class_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-2">
                                                    <label for="validationCustom01" class="find-dv-lbl">Subject</label>
                                                    <asp:DropDownList ID="ddl_subject"  AutoPostBack="true" OnSelectedIndexChanged="ddl_subject_SelectedIndexChanged" runat="server" class="form-select find-dv-txtbx">
                                                    </asp:DropDownList>
                                                </div>
                                                 <div class="col-sm-3">
                                                    <label for="validationCustom01" class="find-dv-lbl">Test Name</label>
                                                    <asp:DropDownList ID="ddl_testname" runat="server" class="form-select find-dv-txtbx">
                                                    </asp:DropDownList>
                                                </div>


                                                <div class="col-sm-3">
                                                    <asp:Button ID="btn_find" runat="server" Text="Find" OnClick="btn_find_Click" class="btn btn-primary find-dv-btn" />
                                                    <asp:LinkButton ID="btn_excels" runat="server" OnClick="btn_excels_Click" class="btn btn-primary find-dv-btn">  <i class='bx bx-download'></i> Excel</asp:LinkButton>
                                                    <asp:LinkButton ID="print1" Visible="false" OnClientClick="return PrintPanel()" CssClass="btn btn-primary find-dv-btn" Style="margin-left: 10px;" runat="server"
                                                        ToolTip="Print"><i class='bx bx-printer'></i></asp:LinkButton>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="grd-wpr">
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
                                                            <span style="font-size: 14px; font-weight: bold;">
                                                                <asp:Label ID="lbl_class22" runat="server">Attempted Students List</asp:Label></span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <asp:Panel ID="Panel1" runat="server">

                                                    <table id="datatable" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                        <thead>
                                                            <tr>
                                                                <th>#</th>
                                                               
                                                                <th>Student Name</th>
                                                                <th>Adm. No.</th>
                                                                <th>Roll No.</th>
                                                                <th>Session</th>
                                                                <th>class</th>
                                                                <th>Section</th>
                                                                <th>Subject</th>
                                                                <th>Name of Exam</th>

                                                                <th>Exam Date</th>
                                                                  <th>Exam Time</th>
                                                                <th>Exam Duration</th>
                                                                <th>No. of Question</th>
                                                                

                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:Repeater ID="rd_view" runat="server" OnItemDataBound="rd_view_ItemDataBound">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                        </td>
                                                                    
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_admissionserialnumber" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_rollnumber" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                        </td>


                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                        </td>



                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("Course_Name")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_Section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                        </td>



                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_subject" Visible="false" runat="server" Text='<%#Bind("subjectname1")%>'></asp:Label>
                                                                            <asp:Label ID="lbl_subjectname_view" runat="server"></asp:Label>
                                                                        </td>

                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_exam_name" runat="server" Text='<%#Bind("Exam_name")%>'></asp:Label>
                                                                        </td>

                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbltestdate" runat="server" Text='<%#Bind("live_date_one") %>'></asp:Label>
                                                                        </td>
                                                                         <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_live_time_one" runat="server" Text='<%#Bind("live_time_one") %>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_Duration" runat="server" Font-Bold="true" Text='<%#Bind("Exam_duration") %>'></asp:Label>
                                                                            <asp:Label ID="lbl12" runat="server">(In minutes)</asp:Label>
                                                                        </td>


                                                                        
                                                                    <td style="text-align: left;">
                                                                        <asp:Label ID="lbl_noofquestion" runat="server" Text='<%#Bind("toquestion") %>'></asp:Label>
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
        <!--end row-->
    </div>
</asp:Content>
