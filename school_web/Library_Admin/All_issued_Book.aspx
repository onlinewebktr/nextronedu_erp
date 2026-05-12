<%@ Page Title="" Language="C#" MasterPageFile="~/Library_Admin/Library_Master.Master" AutoEventWireup="true" CodeBehind="All_issued_Book.aspx.cs" Inherits="school_web.Library_Admin.All_issued_Book" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    All Issued Books
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            $("#<%=txt_AuthorName.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'All_issued_Book.aspx/Getbookauthername',
                        data: "{ 'authername': '" + request.term + "'}",
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
                <div class="breadcrumb-title pe-3">Report </div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Lib_Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">All Issued Books</li>
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
                                                    <label for="validationCustom01" class="find-dv-lbl">Select Class </label>
                                                    <asp:DropDownList ID="ddl_class_wise" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_class_wise_SelectedIndexChanged"></asp:DropDownList>
                                                </div>

                                                <div class="col-sm-2" style="display: none">
                                                    <label for="validationCustom01" class="find-dv-lbl">Book Status </label>
                                                    <asp:DropDownList ID="ddl_book_status" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_book_status_SelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                                <div class="col-sm-2">
                                                    <label for="validationCustom01" class="find-dv-lbl">Location Wise </label>
                                                    <asp:DropDownList ID="ddl_lcation" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_lcation_SelectedIndexChanged"></asp:DropDownList>
                                                </div>

                                                <div class="col-sm-2" style="display: none">
                                                    <label for="validationCustom01" class="find-dv-lbl">Author Name</label>
                                                    <asp:TextBox ID="txt_AuthorName" runat="server" class="form-control"></asp:TextBox>
                                                </div>


                                                <div class="col-sm-1" style="display: none">
                                                    <asp:Button ID="btn_find_auther_wise" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_find_auther_wise_Click" />
                                                </div>

                                                <div class="col-sm-2">


                                                    <asp:LinkButton ID="print1" Visible="false" OnClientClick="return PrintPanel()" CssClass="btn btn-primary find-dv-btn" Style="margin-left: 10px;" runat="server"
                                                        ToolTip="Print"><i class='bx bx-printer'></i></asp:LinkButton>
                                                    <asp:LinkButton ID="btn_excels" runat="server" OnClick="btn_excels_Click" Visible="false" class="btn btn-primary find-dv-btn" Style="margin-left: 10px;">  <i class='bx bx-download'></i> Excel</asp:LinkButton>
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
                                                                <span style="font-size: 14px; font-weight: bold;">Issued Book List<asp:Label ID="lbl_class22" runat="server"></asp:Label></span>


                                                            </div>
                                                        </div>


                                                    </div>

                                                    <asp:Panel ID="Panel2" runat="server">
                                                        <div class="table-responsive">

                                                            <table id="example2" data-page-length='1500' class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                                <thead>
                                                                    <tr>
                                                                        <th>#</th>
                                                                        <th>Book Status</th>
                                                                        <th>Name Of Book</th>
                                                                        <th>Book Type</th>

                                                                        <th>Class</th>
                                                                        <th>Subject</th>
                                                                        <th>Author Name</th>
                                                                        <th>Publication</th>
                                                                        <th>Volume Name</th>
                                                                        <th>Edition</th>
                                                                        <th>Location</th>
                                                                        <th>Sub-Location</th>
                                                                        <th>ISBN No.</th>

                                                                        <th>Qty.</th>



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
                                                                                    <asp:Label ID="lbl_BookStatus" runat="server" Text='<%#Bind("Book_Status")%>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="Label4" runat="server" Text='<%#Bind("NameOfBook")%>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_TypeName" runat="server" Text='<%#Bind("booktype")%>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_classname" runat="server"></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_Book_Category" runat="server" Text='<%#Bind("Book_Category")%>'></asp:Label>
                                                                                </td>



                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="Label5" runat="server" Text='<%#Bind("AuthorName")%>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_Publication" runat="server" Text='<%#Bind("Publication")%>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_EnterVolumePart" runat="server" Text='<%#Bind("EnterVolumePart")%>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_Edition" runat="server" Text='<%#Bind("Edition")%>'></asp:Label>
                                                                                </td>

                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="lbl_Location" runat="server" Text='<%#Bind("location_new")%>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: left;">
                                                                                        <asp:Label ID="Label1" runat="server" Text='<%#Bind("Sub_Location")%>'></asp:Label>
                                                                                    </td>
                                                                                <td style="text-align: left;">
                                                                                    <asp:Label ID="Label14" runat="server" Text='<%#Bind("ISBN_Num")%>'></asp:Label>
                                                                                </td>

                                                                                <td style="text-align: left;">



                                                                                    <asp:LinkButton ID="lnk_view_book_issued_details" runat="server" Text='<%#Bind("count")%>' OnClick="lnk_view_book_issued_details_Click">
                                                                                      

                                                                                    </asp:LinkButton>




                                                                                    <asp:Label ID="lbl_class_id" Visible="false" runat="server" Text='<%#Bind("SelectClass")%>'></asp:Label>
                                                                                    <asp:Label ID="lbl_uniqe_book_no" Visible="false" runat="server" Text='<%#Bind("Book_Unique_Identifier")%>'></asp:Label>

                                                                                    <asp:Label ID="lbl_book_id" Visible="false" runat="server"></asp:Label>

                                                                                </td>







                                                                            </tr>

                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                </tbody>
                                                            </table>
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
            </div>
        </div>
        <!--end row-->
    </div>
    <div class="conf-alrt-sec" id="myModal2" runat="server" visible="false">
        <div class="conf-alrt-inr" style="width: 1000px;">
            <div class="popupTablWpR">
                <div class="row">
                    <div class="col-md-6">
                        <h2 class="popup-dt-h">Book Issued Details</h2>
                    </div>
                    <div class="col-md-6">
                        <ul class="conf-btn-ul" style="margin: 0px 0px 0px 0px;">
                            <li>
                                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Close</asp:LinkButton>
                            </li>
                        </ul>
                    </div>
                </div>
                <div style="margin: 0px; padding: 0px; height: auto; width: 100%;" id="student" runat="server" visible="false">
                    <div class="col-md-12">
                        <h2 class="popup-dt-h" style="font-size: 15px; margin: 8px 0px 9px 0px; border-bottom: 1px solid #000;">Student Details</h2>
                    </div>
                    <table style="width: 100%; font-size: 11px!important;" id="Table1" class="table table-hover table-bordered">
                        <tr>
                            <th>#</th>
                            <th>Student Name</th>
                            <th>Admission No</th>
                            <th>Class</th>
                            <th>Section</th>
                            <th>Roll</th>
                            <th>Book Name</th>
                            <th>ISBN No.</th>
                            <th>Book Issue Date</th>
                            <th>Book Return Date</th>
                        </tr>
                        <asp:Repeater ID="rp_std" runat="server">
                            <ItemTemplate>
                                <tr id="row" runat="server">
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
                                        <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_Section" runat="server" Text='<%#Bind("Section") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_rollnumber" runat="server" Text='<%#Bind("rollnumber") %>'></asp:Label>
                                    </td>

                                    <td>
                                        <asp:Label ID="lbl_book_name" runat="server" Text='<%#Bind("NameOfBook") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_book_isbn_no" runat="server" Text='<%#Bind("ISBN_Num") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_book_issue_date" runat="server" Text='<%#Bind("issue_date") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_book_returneddate" runat="server" Text='<%#Bind("due_date") %>'></asp:Label>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>


                <div style="margin: 0px; padding: 0px; height: auto; width: 100%;" id="emp" runat="server" visible="false">

                    <div class="col-md-12">
                        <h2 class="popup-dt-h" style="font-size: 15px; margin: 8px 0px 9px 0px; border-bottom: 1px solid #000;">Employee Details</h2>
                    </div>

                    <table style="width: 100%; font-size: 11px!important;" id="Table11" class="table table-hover table-bordered">
                        <tr>
                            <th>#</th>
                            <th>Employee Type</th>
                            <th>Employee Name</th>
                            <th>Employee No</th>
                            <th>Book Name</th>
                            <th>ISBN No.</th>
                            <th>Book Issue Date</th>
                            <th>Book Return Date</th>
                        </tr>
                        <asp:Repeater ID="rp_employee" runat="server">
                            <ItemTemplate>
                                <tr id="row" runat="server">
                                    <td>
                                        <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_User_Type" runat="server" Text='<%#Bind("User_Type") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_Emplyee_name" runat="server" Text='<%#Bind("name") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblemaplycode" runat="server" Text='<%#Bind("user_id") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_book_name" runat="server" Text='<%#Bind("NameOfBook") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_book_isbn_no" runat="server" Text='<%#Bind("ISBN_Num") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_book_issue_date" runat="server" Text='<%#Bind("issue_date") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_book_returneddate" runat="server" Text='<%#Bind("due_date") %>'></asp:Label>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
            </div>
        </div>
    </div>




    <style>
        .modal.fade .modal-dialog {
            transition: transform .3s ease-out;
            transform: translate(0, 0px);
        }

        .modal {
            background: rgb(0 0 0 / 43%);
        }

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
