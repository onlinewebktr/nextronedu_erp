<%@ Page Title="" Language="C#" MasterPageFile="~/Library_Admin/Library_Master.Master" AutoEventWireup="true" CodeBehind="Student_History.aspx.cs" Inherits="school_web.Library_Admin.Student_History" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Student's History
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
    </style>
    <script>
        $(function () {
            $("#<%=Txt_startdate.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                readOnly: true,
                yearRange: "1900:2100",
            }).attr("readonly", "true");
        });
        $(function () {
            $("#<%=txt_end_date.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                readOnly: true,
                yearRange: "1900:2100",
            }).attr("readonly", "true");
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
                <div class="breadcrumb-title pe-3">Report</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Lib_Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">
                                <asp:Label ID="lbl_title" runat="server">Student Book History</asp:Label></li>
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
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="find-dv">
                                                <div class="row">
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Start Date</label>
                                                        <div class="clndr-div">
                                                            <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                                            <asp:TextBox ID="Txt_startdate" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                        </div>

                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">End Date</label>
                                                        <div class="clndr-div">
                                                            <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                                            <asp:TextBox ID="txt_end_date" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                        </div>


                                                    </div>
                                                    <div class="col-sm-1">
                                                        <asp:Button ID="btn_find" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_find_Click" />
                                                    </div>



                                                    <div class="col-sm-3">
                                                        <asp:LinkButton ID="print1" Visible="false" OnClientClick="return PrintPanel()" CssClass="btn btn-primary find-dv-btn" Style="margin-left: 10px;" runat="server"
                                                            ToolTip="Print"><i class='bx bx-printer'></i></asp:LinkButton>

                                                        <asp:LinkButton ID="btn_excels" runat="server" Visible="false" class="btn btn-primary find-dv-btn" OnClick="btn_excels_Click">  <i class='bx bx-download'></i> Excel</asp:LinkButton>
                                                    </div>


                                                </div>
                                            </div>
                                            <div class="grd-wpr">
                                                <div id="tblPrintIQ" runat="server">
                                                    <div class="prnt-dv-wpr">
                                                        <div class="pgslry-head-div head">

                                                            <div style="margin: 0px; padding: 0px; height: 110px; width: 20%; float: left;">
                                                                <asp:Image ID="imglogo" runat="server" Style="height: 100px; width: 100px; margin: 0px 0px 0px 10px;" />
                                                            </div>
                                                            <div style="margin: 0px; padding: 0px; height: 141px; width: 80%; float: left;">
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
                                                                <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                    <span style="font-size: 14px; font-weight: bold;">
                                                                        <asp:Label ID="lbl_heading_print" runat="server">All Issued Book List</asp:Label>
                                                                        <asp:Label ID="lbl_class22" runat="server"></asp:Label></span>


                                                                </div>


                                                            </div>


                                                        </div>

                                                        <asp:Panel ID="Panel2" runat="server">
                                                            <table id="example2" data-page-length='1500' class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                                <thead>
                                                                    <tr>
                                                                        <th>#</th>
                                                                        <th>Student Name</th>
                                                                        <th>Admission No</th>
                                                                        <th>Class</th>
                                                                        <th>Section</th>
                                                                        <th>Roll</th>
                                                                        <th>Book Issue No.</th>
                                                                          <th>Item Code</th>
                                                                        <th>Book Name</th>
                                                                        <th>ISBN No.</th>
                                                                        <th>Issue Date</th>
                                                                        <th>Due Date</th>
                                                                        <th>Book Return No.</th>
                                                                        <th>Return Date</th>
                                                                        <th>Book Status</th>
                                                                        <th>Print</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <asp:Repeater ID="rd_view" runat="server" OnItemDataBound="rd_view_ItemDataBound">
                                                                        <ItemTemplate>

                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                </td>

                                                                                <td>
                                                                                    <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname") %>'></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lbladmissionserialnumber" runat="server" Text='<%#Bind("admissionserialnumber") %>'></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class") %>'></asp:Label></td>
                                                                                <td>
                                                                                    <asp:Label ID="lbl_Section" runat="server" Text='<%#Bind("Section") %>'></asp:Label></td>
                                                                                <td>
                                                                                    <asp:Label ID="lbl_rollnumber" runat="server" Text='<%#Bind("rollnumber") %>'></asp:Label></td>

                                                                                <td>
                                                                                    <asp:Label ID="lbl_transaction_no" runat="server" Text='<%#Bind("transaction_no")%>'></asp:Label>
                                                                                </td>

                                                                                <td>
                                                                                    <asp:Label ID="lbl_BookId" runat="server" Text='<%#Bind("BookId")%>'></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label4" runat="server" Text='<%#Bind("NameOfBook")%>'></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label14" runat="server" Text='<%#Bind("ISBN_Num")%>'></asp:Label>
                                                                                </td>





                                                                                <td>
                                                                                    <asp:Label ID="lbl_book_issue_date" runat="server" Text='<%#Bind("issue_date") %>'></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lbl_book_due_date" runat="server" Text='<%#Bind("due_date") %>'></asp:Label>
                                                                                    <asp:Label ID="lbl_Publication" runat="server" Text='<%#Bind("Publication")%>' Visible="false"></asp:Label>
                                                                                    <asp:Label ID="Label5" runat="server" Text='<%#Bind("AuthorName")%>' Visible="false"></asp:Label>
                                                                                </td>


                                                                                <td>
                                                                                    <asp:Label ID="lbl_Book_reurn_slip_id" runat="server" Text='<%#Bind("Book_reurn_slip_id")%>'></asp:Label>
                                                                                </td>


                                                                                <td>
                                                                                    <asp:Label ID="lbl_returned_date" runat="server" Text='<%#Bind("returned_date")%>'></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lbl_Book_status" runat="server" Text='<%#Bind("Book_status")%>'></asp:Label>
                                                                                </td>

                                                                                <td>

                                                                                    <asp:Panel ID="Panel1" runat="server">


                                                                                        <a href="Print_Bar_Code_Issue_book.aspx?issuebook=<%#Eval("transaction_no") %>&type=Student&code=<%#Eval("admissionserialnumber") %>" target="_blank" style="text-decoration: underline;float: left;" title="Print Issue Book Bar Code"><i class="bx bx-printer"></i></a>
                                                                                    </asp:Panel>


                                                                                    <asp:Panel ID="pnl_print_issuebook_slip_id" runat="server">

                                                                                                                                                        <a href="print/Print_Issue_Book_Invoice_Student.aspx?adm=<%#Eval("admissionserialnumber") %>&Slip_no=<%#Eval("transaction_no") %>" target="_blank"  style="text-decoration: underline; color:red;float: left;" title="Print Issue Book Invoice"><i class="bx bx-printer"></i></a>
                                                                                    </asp:Panel>

                                                                                      <asp:Panel ID="pnl_print_returnbook_slip_id" runat="server">

                                                                                            <a href="print/Print_Return_Book_Invoice_Student.aspx?adm=<%#Eval("admissionserialnumber") %>&Slip_no=<%#Eval("Book_reurn_slip_id") %>" target="_blank" style="text-decoration: underline; color:green;float: left;"  title="Print Return Book Invoice" ><i class="bx bx-printer"></i> </a>
                                                                                    </asp:Panel>
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
            </div>
        </div>
        <!--end row-->
    </div>

</asp:Content>
