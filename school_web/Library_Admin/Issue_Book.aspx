<%@ Page Title="" Language="C#" MasterPageFile="~/Library_Admin/Library_Master.Master" AutoEventWireup="true" CodeBehind="Issue_Book.aspx.cs" Inherits="school_web.Library_Admin.Issue_Book" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Issue Book
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script language="Javascript">
       <!--
    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode != 46 && charCode > 31
            && (charCode < 48 || charCode > 57))
            return false;

        return true;
    }
    //-->
    </script>
    <script>
        $(function () {
            $("#<%=Txt_date.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                readOnly: true,
                yearRange: "1900:2100",
            }).attr("readonly", "true");
        });
    </script>
    <style>
        .head {
            display: none;
        }

        #pageFooter {
            display: none;
        }

        input[type=checkbox], input[type=radio] {
            background: #000;
            border-style: none;
            width: 30px;
            height: 30px;
            position: relative;
            top: 8.6px;
            left: 0px;
            margin: 0px 10px 0px 0px;
            z-index: 9999;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $("#<%=Txt_bookname.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'Issue_Book.aspx/Getbookname',
                        data: "{ 'bookname': '" + request.term + "'}",
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
            $("#<%=Txt_Author.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'Issue_Book.aspx/GetAuthor',
                        data: "{ 'Author': '" + request.term + "'}",
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
            $("#<%=Txt_ISBN.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'Issue_Book.aspx/GetISBN',
                        data: "{ 'ISBN': '" + request.term + "'}",
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
            $("#<%=txt_barcode.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'Issue_Book.aspx/GetBarcode',
                        data: "{ 'barcode': '" + request.term + "'}",
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
            $("#<%=txt_item_code.ClientID%>").autocomplete({
                 source: function (request, response) {
                     $.ajax({
                         url: 'Issue_Book.aspx/Getitem_code',
                         data: "{ 'BookId': '" + request.term + "'}",
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

            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3" style="margin-bottom: 0rem!important;">
                <div class="breadcrumb-title pe-3">Book Management</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Lib_Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Issue Book</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase"></h6>
                    <hr style="margin: 3px;" />
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive1">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="find-dv">
                                                <div class="row">
                                                    
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Barcode</label>
                                                        <asp:TextBox ID="txt_barcode" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                    </div>

                                                    <div class="col-sm-1">
                                                        <asp:Button ID="Btn_find_barcode" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="Btn_find_barcode_Click" />
                                                    </div>

                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Class</label>
                                                        <asp:DropDownList ID="ddlclass" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddlclass_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>

                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Book Category</label>
                                                        <asp:DropDownList ID="ddl_subject" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_subject_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>





                                                    <div class="col-sm-3">
                                                        <label for="validationCustom01" class="find-dv-lbl">Book Name</label>
                                                        <asp:TextBox ID="Txt_bookname" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                    </div>

                                                    <div class="col-sm-1">
                                                        <asp:Button ID="Btn_find_Name" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="Btn_find_Name_Click" />
                                                    </div>


                                                   



                                                </div>
                                                <div class="row" style="margin-top: 14px;">
                                                     <div class="col-sm-3">
                                                        <label for="validationCustom01" class="find-dv-lbl">Author Name</label>
                                                        <asp:TextBox ID="Txt_Author" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                    </div>

                                                    <div class="col-sm-1">
                                                        <asp:Button ID="Btn_find_Author" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="Btn_find_Author_Click" />
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">ISBN NUM</label>
                                                        <asp:TextBox ID="Txt_ISBN" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                    </div>

                                                    <div class="col-sm-1">
                                                        <asp:Button ID="Btn_find_ISBN" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="Btn_find_ISBN_Click" />
                                                    </div>
                                                     <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Item Code</label>
                                                        <asp:TextBox ID="txt_item_code" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                    </div>
                                                       <div class="col-sm-1">
                                                        <asp:Button ID="btn_item_code" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_item_code_Click" />
                                                    </div>

                                                </div>
                                            </div>
                                            <div class="grd-wpr">

                                                <div style="margin: 0px; padding: 0px; float: left; height: 300px; width: 100%; overflow-y: scroll" id="book_details" runat="server" visible="false">



                                                    <table id="example26" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                        <thead>
                                                            <tr>
                                                                <th>#</th>
                                                                <th>Book Id</th>
                                                                <th>Book Status</th>

                                                                <th>Book Catogery</th>
                                                                <th>Name Of Book</th>
                                                                <th>Author Name</th>
                                                                <th>Publication</th>
                                                                <th>Publication Year</th>
                                                                <th>Location/Sub Location</th>
                                                                <th>ISBN Num</th>


                                                                <th></th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:Repeater ID="rd_view" runat="server">
                                                                <ItemTemplate>

                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_uniq_bookid" runat="server" Text='<%#Bind("BookId")%>'></asp:Label>

                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_BookStatus" runat="server" Text='<%#Bind("Book_Status")%>'></asp:Label>

                                                                        </td>


                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_Book_Category" runat="server" Text='<%#Bind("Book_Category")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_NameOfBook" runat="server" Text='<%#Bind("NameOfBook")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_AuthorName" runat="server" Text='<%#Bind("AuthorName")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_Publication" runat="server" Text='<%#Bind("Publication")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_PublicationYear" runat="server" Text='<%#Bind("PublicationYear")%>'></asp:Label>
                                                                        </td>


                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_location_new" runat="server" Text='<%#Bind("location_new")%>'></asp:Label>
                                                                            <asp:Label ID="lbl_Sub_Location" runat="server" Text='<%#Bind("Sub_Location")%>'></asp:Label>

                                                                          
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_ISBN_Num" runat="server" Text='<%#Bind("ISBN_Num")%>'></asp:Label>

                                                                             <asp:Label ID="lbl_InvoiceNo" Visible="false" runat="server" Text='<%#Bind("InvoiceNo")%>'></asp:Label>
                                                                            <asp:Label ID="lbl_Price" Visible="false" runat="server" Text='<%#Bind("Price")%>'></asp:Label>
                                                                        </td>


                                                                        <td style="text-align: left;" class="singleCheckbox">
                                                                           
                                                                         <asp:CheckBox ID="rowChkBox" runat="server" />
                                                                        </td>

                                                                    </tr>

                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </tbody>
                                                    </table>
                                               <%--     <script type="text/javascript">
                                                        function checkAllRows(obj) {

                                                            var objGridview = obj.parentNode.parentNode.parentNode;
                                                            var list = objGridview.getElementsByTagName("input");

                                                            for (var i = 0; i < list.length; i++) {
                                                                var objRow = list[i].parentNode.parentNode;
                                                                if (list[i].type == "checkbox" && obj != list[i]) {
                                                                    if (obj.checked) {

                                                                    

                                                                        objRow.style.backgroundColor = "#0baf36";
                                                                        objRow.style.Color = "#fff";
                                                                        list[i].checked = true;
                                                                    }
                                                                    else {
                                                                        objRow.style.backgroundColor = "#FFFFFF";
                                                                        list[i].checked = false;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        function checkUncheckHeaderCheckBox(obj) {
                                                            var objRow = obj.parentNode.parentNode;

                                                            if (obj.checked) {
                                                                objRow.style.backgroundColor = "#0baf36";
                                                                objRow.style.Color = "#fff";
                                                            }
                                                            else {
                                                                objRow.style.backgroundColor = "#FFFFFF";
                                                            }
                                                            var objGridView = objRow.parentNode;

                                                            //Get all input elements in Gridview
                                                            var list = objGridView.getElementsByTagName("input");
                                                            for (var i = 0; i < list.length; i++) {
                                                                var objHeaderChkBox = list[0];

                                                                //Based on all or none checkboxes are checked check/uncheck Header Checkbox
                                                                var checked = true;

                                                                if (list[i].type == "checkbox" && list[i] != objHeaderChkBox) {
                                                                    if (!list[i].checked) {
                                                                        checked = false;
                                                                        break;
                                                                    }
                                                                }
                                                            }
                                                            objHeaderChkBox.checked = checked;
                                                        }
                                                    </script>--%>

                                                     <script type="text/javascript">
                                                         $(function () {
                                                             var $allCheckbox = $('.allCheckbox :checkbox');
                                                             var $checkboxes = $('.singleCheckbox :checkbox');
                                                             $allCheckbox.change(function () {
                                                                 if ($allCheckbox.is(':checked')) {
                                                                     $checkboxes.attr('checked', 'checked');
                                                                 }
                                                                 else {
                                                                     $checkboxes.removeAttr('checked');
                                                                 }
                                                             });
                                                             $checkboxes.change(function () {
                                                                 if ($checkboxes.not(':checked').length) {
                                                                     $allCheckbox.removeAttr('checked');
                                                                 }
                                                                 else {
                                                                     $allCheckbox.attr('checked', 'checked');
                                                                 }
                                                             });
                                                         });
                                                     </script>
                                                </div>


                                                <div class="col-sm-12" id="user_and_book_issue_panl" runat="server" visible="false">
                                                    <div class="row">
                                                        <div style="border-top: 2px solid #000; border-bottom: 2px solid #000; margin: 0px; float: left; height: auto; width: 100%; padding: 0px 0px 7px 0px;">



                                                            <div style="margin: 0px; float: left; height: auto; width: 100%; border-bottom: 2px solid #000;padding: 0px 0px 9px 0px;">
                                                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" style="margin:0px auto">
                                                                    <asp:ListItem Value="Student">Student</asp:ListItem>
                                                                    <asp:ListItem Value="Non Teaching/Teaching Staff">Non Teaching/Teaching Staff</asp:ListItem>
                                                                </asp:RadioButtonList>

                                                            </div>

                                                            <div class="row" id="txtbox" runat="server" visible="false" style="margin: 13px 0px 0px 0px; float: left; width: 94%;">
                                                                <div class="col-sm-4">
                                                                    <label for="validationCustom01" class="find-dv-lbl">
                                                                        <asp:Label ID="lbl_sercheading" runat="server" Text="Enter Admission No./Employee Code :"></asp:Label>
                                                                    </label>
                                                                    <asp:TextBox ID="Txt_Admission_No" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-2">
                                                                    <asp:Button ID="Btn_find_student_admission_no_ammployee_code" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="Btn_find_student_admission_no_ammployee_code_Click" />

                                                                </div>
                                                            </div>

                                                        </div>
                                                        <asp:Panel ID="Panel2" runat="server" Visible="false">
                                                            <div class="row">
                                                                <table id="example22" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                                    <thead>
                                                                        <tr>
                                                                            <th>#</th>
                                                                            <th>Admission No.</th>
                                                                            <th>Admission Date</th>
                                                                            <th>Class</th>
                                                                            <th>Session</th>
                                                                            <th>Section</th>
                                                                            <th>Roll No.</th>
                                                                            <th>Student Name</th>
                                                                            <th>Father Name</th>
                                                                            <th>Library Card No.</th>

                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <asp:HiddenField ID="Hd_Library_student" runat="server" />
                                                                        <asp:HiddenField ID="Hd_admission_no" runat="server" />
                                                                        <asp:Repeater ID="Rd_View1" runat="server">
                                                                            <ItemTemplate>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                    </td>

                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="Label6" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_dateofadmission" runat="server" Text='<%#Bind("dateofadmission")%>'></asp:Label>
                                                                                    </td>

                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                                    </td>

                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="Label1" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="Label2" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="Label3" runat="server" Text='<%#Bind("fathername")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="Label4" runat="server" Text='<%#Bind("lib_card_no")%>'></asp:Label>
                                                                                    </td>

                                                                                </tr>

                                                                            </ItemTemplate>
                                                                        </asp:Repeater>

                                                                    </tbody>

                                                                </table>
                                                            </div>
                                                        </asp:Panel>
                                                        <asp:Panel ID="Panel3" runat="server" Visible="false">
                                                            <div class="row">
                                                                <table id="example25" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                                    <thead>
                                                                        <tr>
                                                                            <th>#</th>
                                                                            <th>Emp ID</th>
                                                                            <th>Department</th>
                                                                            <th>Designation</th>
                                                                            <th>Name</th>
                                                                            <th>Mobile</th>
                                                                            <th>Gender</th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <asp:HiddenField ID="hd_staff_emp_code" runat="server" />
                                                                        <asp:HiddenField ID="hd_staff_libcode" runat="server" />
                                                                        <asp:Repeater ID="Repeater1_staff" runat="server">
                                                                            <ItemTemplate>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_emp_code" runat="server" Text='<%#Bind("Emp_Code")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_Department" runat="server" Text='<%#Bind("Department_name")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_Designation" runat="server" Text='<%#Bind("Designation_name")%>'></asp:Label>
                                                                                    </td>

                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="Label6" runat="server" Text='<%#Bind("Employee_Name")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_mobile" runat="server" Text='<%#Bind("mobile")%>'></asp:Label>
                                                                                    </td>



                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("Gender")%>'></asp:Label>
                                                                                        <asp:Label ID="lbl_Library_Card_No" Visible="false" runat="server" Text='<%#Bind("Library_Card_No")%>'></asp:Label>
                                                                                    </td>







                                                                                </tr>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </asp:Panel>
                                                        <asp:Panel ID="Panel4" runat="server" Visible="false">
                                                            <div class="row">
                                                                <div class="col-sm-2">
                                                                    <label for="validationCustom01" class="find-dv-lbl">Issue Date</label>
                                                                    <div class="clndr-div">
                                                                        <asp:TextBox ID="Txt_date" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                                        <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-3">
                                                                    <label for="validationCustom01" class="find-dv-lbl">Enter No. of Days To Borrow</label>
                                                                    <asp:TextBox ID="Txt_Days" runat="server" AutoPostBack="true" class="form-control find-dv-txtbx" OnTextChanged="Txt_Days_TextChanged" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-2">
                                                                    <label for="validationCustom01" class="find-dv-lbl">Due Date:</label>
                                                                    <asp:Label ID="lbl_due_date" runat="server" Text="">xx/xx/xxxx</asp:Label>
                                                                </div>

                                                                <div class="col-sm-3">

                                                                    <a onclick="save_data()" class="btn btn-primary" style="width: 177px; margin: 16px 0px 0px 0px;">Proceed to issue Book</a>
                                                                    <div style="overflow: hidden; height: 1px;">
                                                                        <asp:Button ID="Btn_Issue_Book" runat="server" Text="Issue Book" class="btn btn-primary find-dv-btn" OnClick="Btn_Issue_Book_Click" />
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


                </div>
            </div>

            <!--end row-->
        </div>

    </div>

    <script type="text/javascript">
        function Confirm() {

            var confirm_value
            var isSubmitted = false;
            confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";

            if (confirm("Do you want to submit?")) {
                confirm_value.value = "Yes";

            }
            else {
                confirm_value.value = "No";
            }

            document.forms[0].appendChild(confirm_value);
        }


        function save_data() {

            var valsubmit = $('#<%=Btn_Issue_Book.ClientID %>').val();
            //alert(valsubmit);
            if (valsubmit == "Issue Book") {

                $('#<%=Btn_Issue_Book.ClientID %>').val('Submitting.. Please Wait..');

                Confirm();
                document.getElementById("<%=Btn_Issue_Book.ClientID %>").click();

            }
            else {
                alert("Already submitted")
            }

        }
    </script>
</asp:Content>
