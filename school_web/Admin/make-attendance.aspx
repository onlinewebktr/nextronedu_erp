<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="make-attendance.aspx.cs" Inherits="school_web.Admin.make_attendance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Make Attendance
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

    <style>
        .rdobtnS {
            margin: 0px 0px 0px 0px;
            font-size: 12px;
        }

            .rdobtnS tr {
                padding: 0px 2px;
                width: 33px;
                float: left;
            }

                .rdobtnS tr td {
                    padding: 0px;
                    width: 30px;
                    margin: 0px;
                    height: 30px;
                    float: left;
                    text-align: center;
                    font-weight: 800;
                    color: #000;
                }

                    .rdobtnS tr td label {
                        position: relative;
                        top: -28px;
                    }


        .att-imgs {
            padding: 3px;
            width: 44px;
            height: 44px;
            border: 1px solid #a1a1a1;
            border-radius: 50%;
        }

        .att-name {
            padding: 0px;
            width: 100%;
            float: left;
            font-size: 12px;
            font-weight: 500;
        }

        .att-adm_no {
            padding: 0px;
            width: 100%;
            float: left;
        }

        .att-roll {
            padding: 0px;
            width: 100%;
            float: left;
        }

        .img-dv {
            padding: 0px;
            width: 45px;
            float: left;
        }

        .contnt-dv {
            padding: 0px 0px 0px 10px;
            float: left;
            width: 52%;
            text-align: left;
        }

        .action-dv {
            padding: 8px 0px 0px 0px;
            float: right;
        }

        .ui-datepicker-trigger {
            display: none;
        }

        .container {
            padding-right: 10px;
            padding-left: 10px;
        }


        .rdobtnS tr:nth-child(1) {
        }

        .rdobtnS tr:nth-child(2) {
        }

        .rdobtnS tr:nth-child(3) {
        }


        .rdobtnS tr:nth-child(1) input[type="radio"] {
            margin: 0px;
            width: 30px;
            height: 30px;
            border-radius: 15px;
            border: 1px solid #39c500;
            background-color: white;
            -webkit-appearance: none; /*to disable the default appearance of radio button*/
            -moz-appearance: none;
        }

            .rdobtnS tr:nth-child(1) input[type="radio"]:focus { /*no need, if you don't disable default appearance*/
                outline: none; /*to remove the square border on focus*/
            }

            .rdobtnS tr:nth-child(1) input[type="radio"]:checked { /*no need, if you don't disable default appearance*/
                background-color: #3ed700;
            }

                .rdobtnS tr:nth-child(1) input[type="radio"]:checked ~ span:first-of-type {
                    color: white;
                }

                .rdobtnS tr:nth-child(1) input[type="radio"]:checked label {
                    color: #fff;
                }

        /*========================SecondRoW============================*/
        .rdobtnS tr:nth-child(2) input[type="radio"] {
            margin: 0px;
            width: 30px;
            height: 30px;
            border-radius: 15px;
            border: 1px solid #ff0000;
            background-color: white;
            -webkit-appearance: none; /*to disable the default appearance of radio button*/
            -moz-appearance: none;
        }

            .rdobtnS tr:nth-child(2) input[type="radio"]:focus { /*no need, if you don't disable default appearance*/
                outline: none; /*to remove the square border on focus*/
            }

            .rdobtnS tr:nth-child(2) input[type="radio"]:checked { /*no need, if you don't disable default appearance*/
                background-color: #ff0000;
            }

                .rdobtnS tr:nth-child(2) input[type="radio"]:checked ~ span:first-of-type {
                    color: white;
                }


        /*========================THIRDROWS============================*/
        .rdobtnS tr:nth-child(3) input[type="radio"] {
            margin: 0px;
            width: 30px;
            height: 30px;
            border-radius: 15px;
            border: 1px solid #ffa500;
            background-color: white;
            -webkit-appearance: none; /*to disable the default appearance of radio button*/
            -moz-appearance: none;
        }

            .rdobtnS tr:nth-child(3) input[type="radio"]:focus { /*no need, if you don't disable default appearance*/
                outline: none; /*to remove the square border on focus*/
            }

            .rdobtnS tr:nth-child(3) input[type="radio"]:checked { /*no need, if you don't disable default appearance*/
                background-color: #ffa500;
            }

                .rdobtnS tr:nth-child(3) input[type="radio"]:checked ~ span:first-of-type {
                    color: white;
                }


        th {
            display: none;
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
            top: 5px;
            left: -23px;
        }



        /* Button to open popup */
        /* Button to open popup */
        .open-btn {
            display: block;
            margin: 120px auto;
            padding: 14px 28px;
            font-size: 18px;
            border: none;
            background-color: #4F46E5;
            color: white;
            border-radius: 10px;
            cursor: pointer;
            box-shadow: 0 4px 6px rgba(0,0,0,0.1);
            transition: background-color 0.2s ease;
        }

            .open-btn:hover {
                background-color: #4338ca;
            }

        /* Modal overlay */
        .modal {
            display: none;
            position: fixed;
            z-index: 1000;
            left: 0;
            top: 0;
            width: 100%;
            height: 100%;
            overflow: auto;
            background-color: rgba(0,0,0,0.5);
        }

        /* Modal content */
        .modal-content {
            background-color: #ffffff;
            margin: 5% auto;
            padding: 24px;
            border-radius: 16px;
            width: 92%;
            max-width: 420px;
            box-shadow: 0 6px 16px rgba(0,0,0,0.15);
            animation: fadeIn 0.3s ease;
        }

        @keyframes fadeIn {
            from {
                opacity: 0;
                transform: scale(0.95);
            }

            to {
                opacity: 1;
                transform: scale(1);
            }
        }

        /* Attendance summary */
        .summary h3 {
            text-align: center;
            margin-bottom: 20px;
            font-size: 21px;
            color: #111827;
            margin-top: 0px;
        }

        .summary ul {
            list-style: none;
            padding: 0;
            margin: 0;
        }

            .summary ul li {
                padding: 12px 0;
                border-bottom: 1px solid #e5e7eb;
                display: flex;
                justify-content: space-between;
                align-items: center;
            }

                .summary ul li span {
                    font-size: 15px;
                    color: #000000;
                }

        /* Colored badges */
        .badge {
            padding: 6px 12px;
            border-radius: 9999px;
            font-size: 14px;
            color: white;
            min-width: 40px;
            text-align: center;
        }

            .badge.total {
                background-color: #006dcb;
            }

            .badge.present {
                background-color: #10B981;
            }

            .badge.absent {
                background-color: #EF4444;
            }

            .badge.leave {
                background-color: #F59E0B;
            }

        /* Buttons */
        .button-group {
            display: flex;
            flex-direction: column;
            gap: 12px;
            margin-top: 24px;
        }

            .button-group button {
                padding: 9px 0px 9px;
                border: none;
                border-radius: 10px;
                font-size: 15px;
                cursor: pointer;
                transition: all 0.2s ease;
            }

        .button-groupnotify {
            padding: 9px 0px 9px;
            border: none;
            border-radius: 10px;
            font-size: 15px;
            cursor: pointer;
            transition: all 0.2s ease;
        }

        .send-btn {
            background-color: #28a745;
            color: white;
        }

            .send-btn:hover {
                background-color: #059669;
            }

        .close-btn {
            background-color: #EF4444;
            color: white;
        }

            .close-btn:hover {
                background-color: #dc2626;
            }

        /* FILTER SECTION */
        .filter-section {
            background-color: #ffffff;
            border: 1px solid #e5e7eb;
            border-radius: 12px;
            padding: 10px 10px 10px 10px;
            margin-bottom: 10px;
            margin-top: 10px;
            box-shadow: 0 1px 4px rgba(0, 0, 0, 0.04);
        }

        .filter-form {
            display: flex;
            flex-wrap: wrap;
            gap: 12px;
            align-items: flex-end;
        }

        .filter-item {
            flex: 1 1 130px;
            min-width: 130px;
        }

            .filter-item label {
                font-size: 14px;
                color: #374151;
                margin-bottom: 6px;
                display: block;
                font-weight: 500;
            }

            .filter-item select,
            .filter-item input {
                width: 100%;
                padding: 5px 10px;
                border: 1px solid #d1d5db;
                border-radius: 6px;
                font-size: 14px;
                height: auto;
                background-color: #f9fafb;
                transition: border-color 0.2s ease;
            }

                .filter-item select:focus,
                .filter-item input:focus {
                    outline: none;
                    border-color: #2563eb;
                    box-shadow: 0 0 0 2px rgba(37, 99, 235, 0.2);
                }

        .Buttoncss {
            background-color: #2563eb !important;
            color: #fff;
            padding: 10px 16px;
            border: none;
            border-radius: 6px;
            cursor: pointer;
            font-size: 0.9rem;
            transition: background-color 0.3s ease, transform 0.2s ease;
            width: 100%;
        }

            .Buttoncss:hover {
                background-color: #1d4ed8;
            }

            .Buttoncss:active {
                transform: scale(0.98);
            }
    </style>


    <link href="../Autocomplete/jquery-ui.css" rel="stylesheet" />
    <script src="../Autocomplete/jquery-ui.js"></script>
    <script>
        $(function () {
            $("#<%=txt_date.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
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
                <div class="breadcrumb-title pe-3"><a href="fee-report.aspx" class="backlnk-css"><i class="bx bx-arrow-back"></i></a>Student Attndance</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Make Attendance</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="find-dv">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="row">
                                                            <div class="col-sm-2" style="display: none">
                                                                <label for="validationCustom01" class="find-dv-lbl">Session</label>
                                                                <asp:DropDownList ID="ddl_session" runat="server" class="form-control  find-dv-txtbx"></asp:DropDownList>
                                                            </div>
                                                            <div class="col-sm-2">
                                                                <label for="validationCustom01" class="find-dv-lbl">Date</label>
                                                                <asp:TextBox ID="txt_date" runat="server" class="form-control "></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-2">
                                                                <label for="validationCustom01" class="find-dv-lbl">Teacher</label>
                                                                <asp:DropDownList ID="ddl_teacher" runat="server" CssClass="form-control  find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_teacher_SelectedIndexChanged"></asp:DropDownList>
                                                            </div>
                                                            <div class="col-sm-2">
                                                                <label for="validationCustom01" class="find-dv-lbl">Class</label>
                                                                <asp:DropDownList ID="ddl_class" runat="server" CssClass="form-control  find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_class_SelectedIndexChanged"></asp:DropDownList>
                                                            </div>
                                                            <div class="col-sm-2">
                                                                <label for="validationCustom01" class="find-dv-lbl">Section</label>
                                                                <asp:DropDownList ID="ddl_section" runat="server" CssClass="form-control  find-dv-txtbx"></asp:DropDownList>
                                                            </div>


                                                            <div class="col-sm-2">
                                                                <asp:Button ID="btn_find" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_find_Click" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="grd-wpr">

                                                <div class="clearfix"></div>
                                                <div class="texbox-border">
                                                    <div runat="server" visible="false" id="grid111">
                                                        <asp:GridView ID="GrdView" runat="server" class="table-bordered" AutoGenerateColumns="False" Width="100%" OnRowDataBound="GrdView_RowDataBound">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <div class="img-dv">
                                                                            <img src="<%#Eval("Student_img") %>" class="att-imgs" />
                                                                        </div>
                                                                        <div class="contnt-dv">
                                                                            <asp:Label ID="lbl_FullName" runat="server" class="att-name" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                            <asp:Label ID="lbl_reg_id" runat="server" class="att-adm_no" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                            <asp:Label ID="lbl_roll_no" runat="server" class="att-roll" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                            <asp:Label ID="lbl_father_mob" Visible="false" runat="server" class="att-roll" Text='<%#Bind("father_mob")%>'></asp:Label>
                                                                            <asp:Label ID="lbl_Father_whatsApp_no" Visible="false" runat="server" class="att-roll" Text='<%#Bind("Father_whatsApp_no")%>'></asp:Label>
                                                                            <asp:Label ID="lbl_classname" Visible="false" runat="server" class="att-roll" Text='<%#Bind("classname")%>'></asp:Label>



                                                                        </div>
                                                                        <div class="action-dv">
                                                                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" class="rdobtnS" OnDataBound="RadioButtonList1_DataBound"></asp:RadioButtonList>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                        <asp:Button ID="btn_save_all" runat="server" Style="width: 100px; height: 37px; float: right" CssClass="mt-2 btn btn-primary" Text="Save" OnClick="btn_save_all_Click" OnClientClick='return confirm("Are you sure want to save all ?")' />
                                                    </div>
                                                </div>
                                                <asp:HiddenField ID="hd_id" runat="server" />
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



    <!-- The Popup Modal -->
    <div id="myModal" class="modal">
        <div class="modal-content">
            <div class="summary">
                <h3>Attendance Summary</h3>
                <ul>
                    <li><span>Total Students</span><span class="badge total" style="color: #fff;" id="totalStudents" runat="server">0</span></li>
                    <li><span>Present</span><span class="badge present" style="color: #fff;" id="presentStudents" runat="server">0</span></li>
                    <li><span>Absent</span><span class="badge absent" style="color: #fff;" id="absentStudents" runat="server">0</span></li>
                    <li><span>Leave</span><span class="badge leave" style="color: #fff;" id="leaveStudents" runat="server">0</span></li>
                </ul>
            </div>
            <div class="button-group">
                <asp:Button ID="btn_send_notification" class="send-btn button-groupnotify" runat="server" Text="Send Notification" OnClick="btn_send_notification_Click" />
                <button type="button" class="close-btn" onclick="closeModal()">Close</button>
            </div>
        </div>
    </div>

    <script>
        // Open modal
        function openModal() {
            document.getElementById("myModal").style.display = "block";
        }

        // Close modal
        function closeModal() {
            document.getElementById("myModal").style.display = "none";
        }


        // Close modal if clicking outside of content
        window.onclick = function (event) {
            var modal = document.getElementById("myModal");
            if (event.target == modal) {
                modal.style.display = "none";
            }
        }
    </script>
</asp:Content>
