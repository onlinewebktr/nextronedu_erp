<%@ Page Title="" Language="C#" MasterPageFile="~/Library_Admin/Library_Master.Master" AutoEventWireup="true" CodeBehind="Lib_Home.aspx.cs" Inherits="school_web.Library_Admin.Lib_Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Home
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .border-info {
            border-color: #35e35b !important;
            background-color: #f8d408;
        }

        .border-danger {
            border-color: #f41127 !important;
            background-color: #ffe9bb;
        }

        .border-success {
            border-color: #17a00e !important;
            background-color: #bfffa1db;
        }

        .border-warning {
            border-color: #ffc107 !important;
            background-color: #caeff5;
        }

        .border-dark {
            border-color: #212529 !important;
            background-color: #ffeb3b6e;
        }

        .text-info {
            color: #ffffff !important;
        }

        .text-danger {
            color: #f41127 !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hd_TenDayS" runat="server" />
    <asp:HiddenField ID="hd_SevenDayS" runat="server" />
    <asp:HiddenField ID="hd_session" runat="server" />
    <!--start page wrapper -->
    <div class="page-wrapper">
        <div class="page-content">
            <div class="row row-cols-1 row-cols-md-2 row-cols-xl-4">
                <div class="col-xl-3">
                    <a href="All_Available_Book.aspx">
                        <div class="card radius-10 border-start border-0 border-3 border-info">
                            <div class="card-body">
                                <div class="d-flex align-items-center">
                                    <div>
                                        <p class="mb-0 text-secondary">Total Available Book</p>
                                        <h4 class="my-1 text-info" runat="server" id="lbl_tolat_Available_book">00</h4>

                                    </div>
                                    <div class="widgets-icons-2 rounded-circle bg-gradient-scooter text-white ms-auto">
                                        <i class='bx bx-book'></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>
                <div class="col-xl-3">
                    <div class="card radius-10 border-start border-0 border-3 border-danger">
                        <a href="Student_History.aspx?page=home&type=issued">
                            <div class="card-body">
                                <div class="d-flex align-items-center">
                                    <div>
                                        <p class="mb-0 text-secondary">Total Issue Book(Student)</p>
                                        <h4 class="my-1 text-danger" runat="server" id="lbl_total_issue_book_student">00</h4>

                                    </div>
                                    <div class="widgets-icons-2 rounded-circle bg-gradient-bloody text-white ms-auto">
                                        <i class='bx bx-book'></i>
                                    </div>
                                </div>
                            </div>
                        </a>
                    </div>
                </div>
                <div class="col-xl-3">
                    <a href="Staff_Book_History.aspx?page=home&type=issued">
                        <div class="card radius-10 border-start border-0 border-3 border-danger">
                            <div class="card-body">
                                <div class="d-flex align-items-center">
                                    <div>
                                        <p class="mb-0 text-secondary">Total Issue Book(Staff)</p>
                                        <h4 class="my-1 text-danger" runat="server" id="lbl_total_issue_book_staff">00</h4>

                                    </div>
                                    <div class="widgets-icons-2 rounded-circle bg-gradient-ohhappiness text-white ms-auto">
                                        <i class='bx bx-book'></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>
                <div class="col-xl-3">
                    <a href="Student_History.aspx?page=home&type=return">
                        <div class="card radius-10 border-start border-0 border-3 border-success">
                            <div class="card-body">
                                <div class="d-flex align-items-center">
                                    <div>
                                        <p class="mb-0 text-secondary">Total Return Book(Student)</p>
                                        <h4 class="my-1 text-danger" runat="server" id="lbl_total_return_book_student">00</h4>

                                    </div>
                                    <div class="widgets-icons-2 rounded-circle bg-gradient-scooter text-white ms-auto">
                                        <i class='bx bx-book'></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>
            </div>
            <div class="row row-cols-1 row-cols-md-2 row-cols-xl-4">
                <div class="col-xl-3">
                    <div class="card radius-10 border-start border-0 border-3 border-success">
                        <a href="Staff_Book_History.aspx?page=home&type=return">
                            <div class="card-body">
                                <div class="d-flex align-items-center">
                                    <div>
                                        <p class="mb-0 text-secondary">Total Return Book(Staff)</p>
                                        <h4 class="my-1 text-danger" runat="server" id="lbl_total_return_book_staff">00</h4>

                                    </div>
                                    <div class="widgets-icons-2 rounded-circle bg-gradient-bloody text-white ms-auto">
                                        <i class='bx bx-book'></i>
                                    </div>
                                </div>
                            </div>
                        </a>
                    </div>
                </div>

                <div class="col-xl-3">
                    <a href="Fine_Collection_For_Student.aspx?page=home&type=All">
                        <div class="card radius-10 border-start border-0 border-3 border-warning">
                            <div class="card-body">
                                <div class="d-flex align-items-center">
                                    <div>
                                        <p class="mb-0 text-secondary">Total Fine Collection(Student)</p>
                                        <h4 class="my-1 text-success" runat="server" id="lbl_total_return_fine_student">00</h4>

                                    </div>
                                    <div class="widgets-icons-2 rounded-circle bg-gradient-ohhappiness text-white ms-auto">
                                        <i class='bx bx-rupee'></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>



                <div class="col-xl-3">
                    <a href="Fine_Collection_For_staff.aspx?page=home&type=All">
                        <div class="card radius-10 border-start border-0 border-3 border-warning">
                            <div class="card-body">
                                <div class="d-flex align-items-center">
                                    <div>
                                        <p class="mb-0 text-secondary">Total Fine Collection(Staff)</p>
                                        <h4 class="my-1 text-success" runat="server" id="lbl_total_retun_fine_staff">00</h4>

                                    </div>
                                    <div class="widgets-icons-2 rounded-circle bg-gradient-scooter text-white ms-auto">
                                        <i class='bx bx-rupee'></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>



                <div class="col-xl-3">
                    <div class="card radius-10 border-start border-0 border-3 border-success">
                        <a href="Print_Student_Library_card.aspx?page=home&type=All">
                            <div class="card-body">
                                <div class="d-flex align-items-center">
                                    <div>
                                        <p class="mb-0 text-secondary">Total Issue Card(Student)</p>
                                        <h4 class="my-1 text-danger" runat="server" id="lbl_total_issued_Card_student">00</h4>

                                    </div>
                                    <div class="widgets-icons-2 rounded-circle bg-gradient-bloody text-white ms-auto">
                                        <i class='bx bx-book'></i>
                                    </div>
                                </div>
                            </div>
                        </a>
                    </div>
                </div>

            </div>
            <!--end row-->
            <div style="margin: 0px 0px 20px 0px; padding: 4px 0px 4px 0px; float: left; width: 100%; border-top: 2px solid #000; text-align: center; font-size: 15px; font-weight: bold; border-bottom: 2px solid #000; color: #f00;">
                Today :-<asp:Label ID="lbl_date" runat="server" Text="xx/xx/xxxx"></asp:Label>
            </div>

            <!--end row-->

            <div class="row row-cols-1 row-cols-md-2 row-cols-xl-4">

                <div class="col-xl-3">
                    <div class="card radius-10 border-start border-0 border-3 border-success">
                        <a href="Over_Due_List_For_Student.aspx?page=home&type=Today">
                            <div class="card-body">
                                <div class="d-flex align-items-center">
                                    <div>
                                        <p class="mb-0 text-secondary">Today Return Book(Pending for student) </p>
                                        <h4 class="my-1 text-danger" runat="server" id="lbl_today_penind_book_student">00</h4>

                                    </div>
                                    <div class="widgets-icons-2 rounded-circle bg-gradient-bloody text-white ms-auto">
                                        <i class='bx bx-book'></i>
                                    </div>
                                </div>
                            </div>
                        </a>
                    </div>
                </div>

                <div class="col-xl-3">
                    <div class="card radius-10 border-start border-0 border-3 border-info">
                        <a href="Staff_Book_History.aspx?page=todayreturn&type=todayreturn">
                            <div class="card-body">
                                <div class="d-flex align-items-center">
                                    <div>
                                        <p class="mb-0 text-secondary">Today Return Book (Pending for Staff) </p>
                                        <h4 class="my-1 text-danger" runat="server" id="lbl_today_penind_book_staf">00</h4>

                                    </div>
                                    <div class="widgets-icons-2 rounded-circle bg-gradient-bloody text-white ms-auto">
                                        <i class='bx bx-book'></i>
                                    </div>
                                </div>
                            </div>
                        </a>
                    </div>
                </div>

                <div class="col-xl-3">
                    <div class="card radius-10 border-start border-0 border-3 border-danger">
                        <a href="Student_History.aspx?page=today&type=issued">
                            <div class="card-body">
                                <div class="d-flex align-items-center">
                                    <div>
                                        <p class="mb-0 text-secondary">Total Issue Book(Student)</p>
                                        <h4 class="my-1 text-danger" runat="server" id="lbl_total_issue_book_student_today">00</h4>

                                    </div>
                                    <div class="widgets-icons-2 rounded-circle bg-gradient-bloody text-white ms-auto">
                                        <i class='bx bx-book'></i>
                                    </div>
                                </div>
                            </div>
                        </a>
                    </div>
                </div>
                <div class="col-xl-3">
                    <a href="Staff_Book_History.aspx?page=today&type=issued">
                        <div class="card radius-10 border-start border-0 border-3 border-danger">
                            <div class="card-body">
                                <div class="d-flex align-items-center">
                                    <div>
                                        <p class="mb-0 text-secondary">Total Issue Book(Staff)</p>
                                        <h4 class="my-1 text-danger" runat="server" id="lbl_total_issue_book_staff_today">00</h4>

                                    </div>
                                    <div class="widgets-icons-2 rounded-circle bg-gradient-ohhappiness text-white ms-auto">
                                        <i class='bx bx-book'></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>



            </div>
            <div class="row row-cols-1 row-cols-md-2 row-cols-xl-4">
                <div class="col-xl-3">
                    <a href="Student_History.aspx?page=today&type=return">
                        <div class="card radius-10 border-start border-0 border-3 border-success">
                            <div class="card-body">
                                <div class="d-flex align-items-center">
                                    <div>
                                        <p class="mb-0 text-secondary">Total Return Book(Student)</p>
                                        <h4 class="my-1 text-danger" runat="server" id="lbl_total_return_book_student_today">00</h4>

                                    </div>
                                    <div class="widgets-icons-2 rounded-circle bg-gradient-scooter text-white ms-auto">
                                        <i class='bx bx-book'></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>

                <div class="col-xl-3">
                    <div class="card radius-10 border-start border-0 border-3 border-success">
                        <a href="Staff_Book_History.aspx?page=today&type=return">
                            <div class="card-body">
                                <div class="d-flex align-items-center">
                                    <div>
                                        <p class="mb-0 text-secondary">Total Return Book(Staff)</p>
                                        <h4 class="my-1 text-danger" runat="server" id="lbl_total_return_book_staff_today">00</h4>

                                    </div>
                                    <div class="widgets-icons-2 rounded-circle bg-gradient-bloody text-white ms-auto">
                                        <i class='bx bx-book'></i>
                                    </div>
                                </div>
                            </div>
                        </a>
                    </div>
                </div>
                <div class="col-xl-3">
                    <a href="Fine_Collection_For_Student.aspx?page=home&type=today">
                        <div class="card radius-10 border-start border-0 border-3 border-warning">
                            <div class="card-body">
                                <div class="d-flex align-items-center">
                                    <div>
                                        <p class="mb-0 text-secondary">Total Fine Collection(Student)</p>
                                        <h4 class="my-1 text-success" runat="server" id="lbl_total_return_fine_student_today">00</h4>

                                    </div>
                                    <div class="widgets-icons-2 rounded-circle bg-gradient-ohhappiness text-white ms-auto">
                                        <i class='bx bx-rupee'></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>


                <div class="col-xl-3">
                    <a href="Fine_Collection_For_staff.aspx?page=home&type=today">
                        <div class="card radius-10 border-start border-0 border-3 border-warning">
                            <div class="card-body">
                                <div class="d-flex align-items-center">
                                    <div>
                                        <p class="mb-0 text-secondary">Total Fine Collection(Staff)</p>
                                        <h4 class="my-1 text-success" runat="server" id="lbl_total_retun_fine_staff_today">00</h4>

                                    </div>
                                    <div class="widgets-icons-2 rounded-circle bg-gradient-scooter text-white ms-auto">
                                        <i class='bx bx-rupee'></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>



            </div>
            <div class="row row-cols-1 row-cols-md-2 row-cols-xl-4">
                <div class="col-xl-3">
                    <div class="card radius-10 border-start border-0 border-3 border-dark">
                        <a href="Print_Student_Library_card.aspx?page=home&type=today">
                            <div class="card-body">
                                <div class="d-flex align-items-center">
                                    <div>
                                        <p class="mb-0 text-secondary">Total Issue Card(Student)</p>
                                        <h4 class="my-1 text-danger" runat="server" id="lbl_total_issued_Card_student_today">00</h4>

                                    </div>
                                    <div class="widgets-icons-2 rounded-circle bg-gradient-bloody text-white ms-auto">
                                        <i class='bx bx-book'></i>
                                    </div>
                                </div>
                            </div>
                        </a>
                    </div>
                </div>
            </div>

            <div style="margin: 0px 0px 20px 0px; padding: 4px 0px 4px 0px; float: left; width: 100%; border-top: 2px solid #000; text-align: center; font-size: 15px; font-weight: bold; border-bottom: 2px solid #000; color: #f00;">
                Over Due List  
            </div>

            <div class="row row-cols-1 row-cols-md-2 row-cols-xl-4">
                <div class="col-xl-3">
                    <div class="card radius-10 border-start border-0 border-3 border-warning">
                        <a href="Over_Due_List_For_Student.aspx?page=home&type=Last3Days">
                            <div class="card-body">
                                <div class="d-flex align-items-center">
                                    <div>
                                        <p class="mb-0 text-secondary">Last 3 Days(Student)</p>
                                        <h4 class="my-1 text-danger" runat="server" id="lbl_over_3days_student">00</h4>

                                    </div>
                                    <div class="widgets-icons-2 rounded-circle bg-gradient-bloody text-white ms-auto">
                                        <i class='bx bx-rupee'></i>
                                    </div>
                                </div>
                            </div>
                        </a>
                    </div>
                </div>
                <div class="col-xl-3">
                    <a href="Over_Due_List_For_Student.aspx?page=home&type=Last7Days">
                        <div class="card radius-10 border-start border-0 border-3 border-dark">
                            <div class="card-body">
                                <div class="d-flex align-items-center">
                                    <div>
                                        <p class="mb-0 text-secondary">Last 7 Days(Student)</p>
                                        <h4 class="my-1 text-danger" runat="server" id="lbl_over_7days_student">00</h4>

                                    </div>
                                    <div class="widgets-icons-2 rounded-circle bg-gradient-ohhappiness text-white ms-auto">
                                        <i class='bx bx-rupee'></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>
                <div class="col-xl-3">
                    <a href="Over_Due_List_For_Student.aspx?page=home&type=Last15Days">
                        <div class="card radius-10 border-start border-0 border-3 border-success">
                            <div class="card-body">
                                <div class="d-flex align-items-center">
                                    <div>
                                        <p class="mb-0 text-secondary">Last 15 Days(Student)</p>
                                        <h4 class="my-1 text-danger" runat="server" id="lbl_over_15days_student">00</h4>

                                    </div>
                                    <div class="widgets-icons-2 rounded-circle bg-gradient-scooter text-white ms-auto">
                                        <i class='bx bx-rupee'></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>

                <div class="col-xl-3">
                    <div class="card radius-10 border-start border-0 border-3 border-info">
                        <a href="Over_Due_List_For_Student.aspx?page=home&type=Last30Days">
                            <div class="card-body">
                                <div class="d-flex align-items-center">
                                    <div>
                                        <p class="mb-0 text-secondary">Last 1 Month(Student)</p>
                                        <h4 class="my-1 text-danger" runat="server" id="lbl_over_30days_student">00</h4>

                                    </div>
                                    <div class="widgets-icons-2 rounded-circle bg-gradient-bloody text-white ms-auto">
                                        <i class='bx bx-rupee'></i>
                                    </div>
                                </div>
                            </div>
                        </a>
                    </div>
                </div>
            </div>


        </div>
    </div>




</asp:Content>
