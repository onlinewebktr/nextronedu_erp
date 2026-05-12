<%@ Page Title="" Language="C#" MasterPageFile="~/Library_Admin/Library_Master.Master" AutoEventWireup="true" CodeBehind="Return_Book.aspx.cs" Inherits="school_web.Library_Admin.Return_Book" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Return Book
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
            $("#<%=Txt_returndate.ClientID %>").datepicker({
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

          input[type=radio] {
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
        input[type=checkbox]{
            background: #000;
            border-style: none;
            width: 20px;
            height: 20px;
            position: relative;
            top: 4.6px;
            left: 0px;
            margin: 0px 10px 0px 0px;
            z-index: 9999;
        }
        .find-dv-lbl {
            margin: 0px;
            padding: 0px;
            width: 100%;
            float: left;
            font-weight: bold;
        }

        .page-content {
            padding: 0rem 1.5rem 0.7rem 1.5rem;
        }

        .card-body {
            flex: 1 1 auto;
            padding: 0rem 1rem 1rem 1rem;
        }

        .ui-datepicker .ui-datepicker-title select {
            font-size: 1em;
            margin: 1px 0;
            COLOR: #000;
            z-index: 99999 !important;
        }

        .ui-datepicker .ui-datepicker-title select {
            font-size: 1em;
            margin: 1px 0;
            COLOR: #000;
            z-index: 99999 !important;
        }

        .calender-icon {
            margin: 0px 0px 0px 0px;
            position: relative;
            font-size: 13px;
            font-weight: normal;
            width: 100%;
        }

        .clndr-icon {
            font-size: 14px !important;
            color: #ff2956;
            position: absolute;
            top: 2px;
            left: 118px;
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

            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3" style="margin-bottom: 0rem!important;">
                <div class="breadcrumb-title pe-3">Book Management</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Lib_Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Return Book</li>
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

                                            <div class="grd-wpr">




                                                <div class="col-sm-12" id="user_and_book_issue_panl" runat="server" visible="true">
                                                    <div class="row">
                                                        <div style="border-top: 2px solid #000; border-bottom: 0px solid #000; margin: 0px; float: left; height: auto; width: 100%; padding: 0px 0px 7px 0px;">



                                                            <div style="margin: 0px; float: left; height: auto; width: 100%; border-bottom: 2px solid #000; padding: 0px 0px 9px 0px;">
                                                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" Style="margin: 0px auto;">
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
                                                                                        <asp:Label ID="lbl_Mobile" runat="server" Text='<%#Bind("Mobile")%>'></asp:Label>
                                                                                    </td>



                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("gender")%>'></asp:Label>
                                                                                        <asp:Label ID="lbl_Library_Card_No" Visible="false" runat="server" Text='<%#Bind("Library_Card_No")%>'></asp:Label>
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

                                                <div style="margin: 0px; padding: 0px; float: left; height: 500px; width: 100%; overflow-y: scroll" id="book_details" runat="server" visible="false">



                                                    <table id="example26" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                        <thead>
                                                            <tr>
                                                                <th>#</th>
                                                                <th>Book Id</th>
                                                                <th>Book Status</th>

                                                                <th>Name Of Book</th>
                                                                <th>Author Name</th>
                                                                <th>Publication</th>
                                                                <th>Publication Year</th>

                                                                <th>ISBN Num</th>
                                                                <th>Book Issue Date</th>
                                                                <th>Book Due Date</th>
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
                                                                            <asp:Label ID="lbl_ISBN_Num" runat="server" Text='<%#Bind("ISBN_Num")%>'></asp:Label>
                                                                        </td>

                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_book_issue_date" runat="server" Text='<%#Bind("issue_date")%>'></asp:Label>



                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_book_due_date" runat="server" Text='<%#Bind("due_date")%>'></asp:Label>



                                                                        </td>
                                                                        <td style="text-align: left;">



                                                                            <asp:Button ID="btn_select" runat="server" Text="Select Book" OnClick="btn_select_Click" Style="background: #0dff65;" />

                                                                            <asp:Label ID="lbl_location_new" runat="server" Text='<%#Bind("location_new")%>' Visible="false"></asp:Label>
                                                                            <asp:Label ID="lbl_transaction_no" runat="server" Text='<%#Bind("transaction_no")%>' Visible="false"></asp:Label>

                                                                        </td>

                                                                    </tr>

                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </tbody>
                                                    </table>




                                                    <asp:Panel ID="Panel4" runat="server" Visible="false">
                                                        <div class="row" style="border-top: 2px solid #000; padding-top: 13px;">
                                                            <asp:HiddenField ID="hd_book_id" runat="server" />
                                                            <asp:HiddenField ID="hd_extra_day_fine_amount" runat="server" />
                                                            <div class="col-sm-3">
                                                                <label for="validationCustom01" class="find-dv-lbl">Is Late Fine Apply</label>
                                                                <div class="clndr-div">
                                                                    
                                                                    <asp:CheckBox ID="chk_latefineapplay" runat="server" Text="Is Late Fine Applied" Checked="false" AutoPostBack="true" OnCheckedChanged="chk_latefineapplay_CheckedChanged" />
                                                                </div>
                                                            </div>

                                                            <div class="col-sm-3">
                                                                <label for="validationCustom01" class="find-dv-lbl">Transaction No.</label>
                                                                <div class="clndr-div">
                                                                    <asp:Label ID="Lbl_Transaction" runat="server" Text=""></asp:Label>
                                                                </div>
                                                            </div>

                                                            <div class="col-sm-1">
                                                                <label for="validationCustom01" class="find-dv-lbl">Issue Date</label>
                                                                <div class="clndr-div">
                                                                    <asp:Label ID="lbl_issue_ate" runat="server" Text=""></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-1">
                                                                <label for="validationCustom01" class="find-dv-lbl">Due Date</label>
                                                                <div class="clndr-div">
                                                                    <asp:Label ID="lbl_due_date_date" runat="server" Text=""></asp:Label>
                                                                </div>
                                                            </div>

                                                            <div class="col-sm-2">
                                                                <label for="validationCustom01" class="find-dv-lbl">Extra Day</label>
                                                                <asp:TextBox ID="txt_extra_day" Enabled="false" runat="server" class="form-control find-dv-txtbx" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-2">
                                                                <label for="validationCustom01" class="find-dv-lbl">Extra Day Book Fine  </label>
                                                                <asp:TextBox ID="txt_extraday_book_fine" Enabled="false" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                            </div>
                                                          






                                                        </div>
                                                        <div class="row" style="padding-top: 13px;">
                                                              <div class="col-sm-2">
                                                                <label for="validationCustom01" class="find-dv-lbl">Book Status</label>
                                                                <asp:DropDownList ID="Ddl_Book_Status" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Ddl_Book_Status_SelectedIndexChanged" Style="height: 26px;">
                                                                    <asp:ListItem>Return Book</asp:ListItem>
                                                                    <asp:ListItem>Return Damage Book</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="col-sm-2" id="Damagepnl" runat="server" visible="false">
                                                                <label for="validationCustom01" class="find-dv-lbl">Damage Book Fine  </label>
                                                                <asp:TextBox ID="Txt_dimage_book_fineamount" runat="server" AutoPostBack="true" class="form-control" OnTextChanged="Txt_dimage_book_fineamount_TextChanged" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-2">
                                                                <label for="validationCustom01" class="find-dv-lbl">Total Amount </label>
                                                                <asp:Label ID="lbl_total_payment" runat="server" Text=""></asp:Label>
                                                            </div>

                                                            <div class="col-sm-2" style="position: relative">
                                                                <label for="validationCustom01" class="find-dv-lbl">Return Date:</label>
                                                                <asp:TextBox ID="Txt_returndate" runat="server" CssClass="form-control calender-icon"></asp:TextBox>
                                                                <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                                            </div>

                                                            <div class="col-sm-2">
                                                                <label for="validationCustom01" class="find-dv-lbl">Remarks</label>
                                                                <asp:TextBox ID="Txt_Remarks" runat="server" class="form-control find-dv-txtbx" TextMode="MultiLine"></asp:TextBox>
                                                            </div>


                                                            <div class="col-sm-2">



                                                                <asp:Button ID="Btn_Issue_Book" runat="server" Text="Add" class="btn btn-primary find-dv-btn" OnClick="Btn_Issue_Book_Click" />


                                                            </div>
                                                        </div>




                                                        <div style="margin: 0px; padding: 0px; float: left;" runat="server" visible="false" id="addedbook">
                                                            <table id="example261" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                                <thead>
                                                                    <tr>
                                                                        <th>#</th>
                                                                        <th>Book Id</th>

                                                                        <th>Name Of Book</th>

                                                                        <th>ISBN Num</th>
                                                                        <th>Issue Date</th>
                                                                        <th>Due Date</th>
                                                                        <th>Return Date</th>
                                                                        <th>Extra Day</th>
                                                                        <th>Fine/Day</th>
                                                                        <th>Damage Book Fine</th>
                                                                        <th>Total</th>
                                                                        <th>Action</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <asp:Repeater ID="Repeater1" runat="server">
                                                                        <ItemTemplate>

                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lbl_uniq_bookid" runat="server" Text='<%#Bind("Book_id")%>'></asp:Label>

                                                                                </td>




                                                                                <td>
                                                                                    <asp:Label ID="lbl_NameOfBook" runat="server" Text='<%#Bind("NameOfBook")%>'></asp:Label>
                                                                                </td>





                                                                                <td>
                                                                                    <asp:Label ID="lbl_ISBN_Num" runat="server" Text='<%#Bind("ISBN_Num")%>'></asp:Label>
                                                                                </td>

                                                                                <td>
                                                                                    <asp:Label ID="lbl_Issue_date" runat="server" Text='<%#Bind("Issue_date")%>'></asp:Label>

                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lbl_Due_date" runat="server" Text='<%#Bind("Due_date")%>'></asp:Label>

                                                                                </td>

                                                                                <td>
                                                                                    <asp:Label ID="lbl_Return_date" runat="server" Text='<%#Bind("Return_date")%>'></asp:Label>

                                                                                </td>

                                                                                <td>
                                                                                    <asp:Label ID="lbl_Extra_day" runat="server" Text='<%#Bind("Extra_day")%>'></asp:Label>

                                                                                </td>

                                                                                <td>
                                                                                    <asp:Label ID="lbl_Extra_day_fine_amount" runat="server" Text='<%#Bind("Extra_day_fine_amount")%>'></asp:Label>

                                                                                </td>



                                                                                <td>
                                                                                    <asp:Label ID="lbl_Damage_Book_Fine" runat="server" Text='<%#Bind("Damage_Book_Fine")%>'></asp:Label>

                                                                                </td>


                                                                                <td>

                                                                                    <asp:Label ID="lbl_Total" runat="server" Text='<%#Bind("Total")%>'></asp:Label></td>



                                                                                <td>
                                                                                    <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                                    <asp:LinkButton ID="lnkDel_temp" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_temp_Click"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                </tbody>
                                                            </table>




                                                            <a onclick="save_data()" class="btn btn-primary" style="width: 177px; margin: 16px 0px 0px 0px;">Final Submit</a>

                                                            <div style="overflow: hidden; height: 1px;">
                                                                <asp:Button ID="btn_final_submit_data" runat="server" Text="Final Submit" class="btn btn-primary find-dv-btn" OnClick="btn_final_submit_data_Click" />
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

            var valsubmit = $('#<%=btn_final_submit_data.ClientID %>').val();
            //alert(valsubmit);
            if (valsubmit == "Final Submit") {

                $('#<%=btn_final_submit_data.ClientID %>').val('Submitting.. Please Wait..');

                Confirm();
                document.getElementById("<%=btn_final_submit_data.ClientID %>").click();

            }
            else {
                alert("Already submitted")
            }

        }
    </script>
</asp:Content>
