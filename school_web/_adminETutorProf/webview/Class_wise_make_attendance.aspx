<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Class_wise_make_attendance.aspx.cs" Inherits="school_web._adminETutorProf.webview.Class_wise_make_attendance" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Make Attendance for Student</title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="Content-Language" content="en" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no, shrink-to-fit=no" />
    <meta http-equiv="cache-control" content="no-cache" />
    <meta http-equiv="expires" content="0" />
    <meta http-equiv="pragma" content="no-cache" />
    <meta name="msapplication-TileColor" content="#ffffff" />
    <meta name="msapplication-TileImage" content="favicon/ms-icon-144x144.png" />
    <meta name="theme-color" content="#ffffff" />
    <script src="//code.jquery.com/jquery-1.11.1.min.js"></script>
    <script src="../../assets/js/bootstrap.min.js"></script>
    <link href="../../css/bootstrap.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css2?family=Poppins:ital,wght@0,100;0,200;0,300;0,400;0,500;0,600;0,700;0,800;0,900;1,100;1,200;1,300;1,400;1,500;1,600;1,700;1,800;1,900&family=Raleway:ital,wght@0,100;0,200;0,300;0,400;0,500;0,600;0,700;0,800;0,900;1,100;1,200;1,300;1,400;1,500;1,600;1,700;1,800;1,900&display=swap" rel="stylesheet" />
    <link href="../../font-awesome-4.0.3/css/font-awesome.min.css" rel="stylesheet" />
    <style type="text/css">
        body {
            height: auto;
            width: 100%;
            margin: 0px 0px 0px 0px;
            padding: 0px 0px 0px 0px;
            float: left;
            font-family: 'Poppins', sans-serif;
        }

        h1, h2, h3, h4, h5, h6 {
            font-family: 'Poppins', sans-serif;
        }

        p {
            font-family: 'Poppins', sans-serif;
        }

        a {
            font-family: 'Poppins', sans-serif;
        }

        .messbox-sec-h2 {
            height: auto;
            width: 100%;
            margin: 0px 0px 0px 0px;
            padding: 0px 0px 0px 0px;
            float: left;
            font-size: 18px;
            line-height: 25px;
            font-weight: 500;
            text-align: center;
            color: #fff;
            background-color: #109be1;
        }

        .fullinfo {
            margin: 0px 0px 0px;
            padding: 0px;
            float: left;
            height: auto;
            width: 100%;
        }

        .textcont1 {
            height: auto;
            width: 100%;
            margin: 0px 0px 0px 0px;
            padding: 3px 0px 3px 0px;
            float: left;
            font-size: 13px;
            line-height: 20px;
            color: #000;
            text-align: left;
        }

        .textcont3 {
            height: auto;
            width: 100%;
            margin: 0px 0px 0px 0px;
            padding: 3px 0px 3px 0px;
            float: left;
            font-size: 12px;
            line-height: 20px;
            color: #000;
            text-align: left;
            font-weight: bold;
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

        .texbox-border {
            margin: 6px 0px 0px 0px;
            padding: 0px;
            height: auto;
            width: 100%;
            border-bottom: 1px solid #00000038;
        }

        .btn {
            padding: 2px 17px 2px 17px !important;
            margin: 5px 0px 0px 0px;
        }
        /******************Notification**********************/
        .notificationpan {
            display: none;
            width: 90%;
            background-color: rgb(255, 76, 76);
            position: fixed;
            top: 15px  !important;
            right: 10px;
            padding: 10px 10px; 
            height: auto;
            border: 1px solid rgb(162, 162, 162);
            box-shadow: 2px 7px 19px -2px rgba(154, 154, 154, 0.8);
        }


        .closenotificationpan {
            position: absolute;
            margin: 0px 0px 0px 0px;
            top: 6px;
            right: 6px;
            cursor: pointer;
        }

        #notification {
            margin: 0px;
            padding: 0px;
            position: relative;
            z-index: 999;
        }

        table {
            /*box-shadow: 0 1px 1px 0 rgb(0 0 0 / 14%), 0 7px 10px -5px rgb(244 67 54 / 40%);*/
            /*background: linear-gradient( 60deg,#f7807e,#e53935);*/
            border-radius: 0px;
            /*border: 1px dashed #d1c5c5!important;*/
            background: #fff !important;
            border-bottom: 0px solid #c8c5c5;
        }

        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            padding: 5px 5px;
            vertical-align: middle;
            border-color: #ddd0;
            font-size: 12px;
            color: #000;
            padding: 6px 0px 5px 7px !important;
            text-align: center;
        }

        .table-bordered > thead > tr > th, .table-bordered > tbody > tr > th, .table-bordered > tfoot > tr > th, .table-bordered > thead > tr > .table-bordered > tbody > tr > .table-bordered > tfoot > tr > th {
            background: #e1dddd !important;
        }

        .table-bordered > thead > tr > th, .table-bordered > tbody > tr > th, .table-bordered > tfoot > tr > th, .table-bordered > thead > tr > td, .table-bordered > tbody > tr > td, .table-bordered > tfoot > tr > td {
            border: 1px solid #e7e7e7;
            text-align: center;
            padding: 3px 4px 3px 5px;
            font-size: 11px;
        }

        .table {
            margin-bottom: 9px !important;
        }

        label {
            display: inline !important;
            max-width: 100%;
            margin-bottom: 5px;
            font-weight: bold;
        }

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



        .stats-grid {
            display: grid;
            grid-template-columns: repeat(2, 1fr);
            gap: 12px;
            margin-bottom: 20px;
        }

        .stat-card {
            background: #ffffff;
            border-radius: 12px;
            padding: 10px 10px 5px 10px;
            box-shadow: 0 2px 6px rgba(0, 0, 0, 0.05);
            text-align: center;
            transition: transform 0.3s ease;
        }

            .stat-card:hover {
                transform: translateY(-4px);
                box-shadow: 0 4px 12px rgba(0, 0, 0, 0.08);
            }

        .stat-title {
            font-size: 14px;
            color: #6b7280;
            margin-bottom: 6px;
            text-transform: uppercase;
            letter-spacing: 0.05em;
        }

        .stat-value {
            font-size: 22px;
            font-weight: bold;
            color: #0d6efd;
        }

        .present-card .stat-value {
            color: #198754;
        }

        .absent-card .stat-value {
            color: #dc3545;
        }

        .leave-card .stat-value {
            color: #ffc107;
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
            margin: 20% auto;
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

    <link href="../../Autocomplete/jquery-ui.css" rel="stylesheet" />
    <script src="../../Autocomplete/jquery-ui.js"></script>
    <script>
        $(function () {
            $("#<%=txt_date.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                maxDate: '0',
            }).attr("readonly", "true");
        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="sm" runat="server"></asp:ScriptManager>
        <div class="fullinfo">
            <div style="margin: 0px; padding: 0px 5px; float: left; height: auto; width: 100%; position: relative">
                <div id="notification">
                    <div id="pan" class="notificationpan">
                        <div style="float: left; width: 100%; height: auto;">
                            <asp:Label ID="lbl_msg" runat="server" Font-Bold="True" ForeColor="White"></asp:Label>
                        </div>
                    </div>
                </div>

                <div class="filter-section">
                    <div class="filter-form">
                        <div class="filter-item">
                            <label for="class">Session</label>
                            <asp:DropDownList ID="ddl_session" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                        <div class="filter-item">
                            <label for="class">Class</label>
                            <asp:DropDownList ID="ddl_class" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_class_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class="filter-item">
                            <label for="class">Section</label>
                            <asp:DropDownList ID="ddl_section" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="filter-item">
                            <label for="date">Date</label>
                            <asp:TextBox ID="txt_date" runat="server" name="date" style="padding: 6px 10px;"></asp:TextBox>
                        </div>
                        <div class="filter-item">
                            <asp:Button ID="btn_find" runat="server" Text="Find" class="Buttoncss" Style="padding: 6px 10px;"
                                OnClick="btn_find_Click" />
                        </div>
                    </div>
                </div>

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
                        <asp:Button ID="btn_save_all" runat="server" Style="width: 100px; height: 37px; float: right" CssClass="mt-2 btn btn-primary" Text="Save" OnClick="btn_save_all_Click" />
                    </div>
                </div>
                <asp:HiddenField ID="hd_id" runat="server" />
            </div>
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
    </form>
</body>
</html>
