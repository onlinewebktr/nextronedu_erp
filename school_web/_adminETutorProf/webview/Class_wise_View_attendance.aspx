<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Class_wise_View_attendance.aspx.cs" Inherits="school_web._adminETutorProf.webview.Class_wise_View_attendance" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Class Wise View Attendance</title>
    <link href="../../assets/css/bootstrap.min.css" rel="stylesheet" />
    <script src="//code.jquery.com/jquery-1.11.1.min.js"></script>
    <script src="../../assets/js/bootstrap.min.js"></script>
    <style>
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #f9fafb;
            margin: 0;
            padding: 0px;
            color: #333;
        }

        h1 {
            text-align: center;
            color: #0d6efd;
            font-size: 24px;
            margin-bottom: 20px;
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

        .table-container {
            overflow-x: auto;
            background: #ffffff;
            border-radius: 12px;
            box-shadow: 0 2px 8px rgba(0, 0, 0, 0.05);
        }

        table {
            width: 100%;
            border-collapse: collapse;
            animation: fadeInTable 0.6s ease-in-out;
        }

        thead {
            background-color: #f1f5f9;
        }

        th,
        td {
            padding: 8px 5px;
            text-align: left;
            border: 1px solid #e5e7eb;
            font-size: 13px;
        }

        th {
            color: #374151;
            text-transform: uppercase;
            letter-spacing: 0.03em;
            font-weight: 600;
        }

        tbody tr:nth-child(even) {
            background-color: #f9fafc;
        }

        tbody tr:hover {
            background-color: #f0f4ff;
            transition: background-color 0.3s ease;
        }

        .status {
            display: inline-block;
            padding: 4px 12px;
            border-radius: 20px;
            font-size: 12px;
            font-weight: 500;
            text-align: center;
        }

        .present {
            background-color: #e6f4ea;
            color: #198754;
        }

        .absent {
            background-color: #f8d7da;
            color: #dc3545;
        }

        .leave {
            background-color: #fff3cd;
            color: #856404;
        }

        @keyframes fadeInTable {
            from {
                opacity: 0;
                transform: translateY(10px);
            }

            to {
                opacity: 1;
                transform: translateY(0);
            }
        }

        @keyframes fadeInRow {
            from {
                opacity: 0;
                transform: translateY(8px);
            }

            to {
                opacity: 1;
                transform: translateY(0);
            }
        }

        tbody tr {
            animation: fadeInRow 0.5s ease forwards;
        }

            tbody tr:nth-child(1) {
                animation-delay: 0.1s;
            }

            tbody tr:nth-child(2) {
                animation-delay: 0.2s;
            }

            tbody tr:nth-child(3) {
                animation-delay: 0.3s;
            }

            tbody tr:nth-child(4) {
                animation-delay: 0.4s;
            }


        /* FILTER SECTION */
        .filter-section {
            background-color: #ffffff;
            border: 1px solid #e5e7eb;
            border-radius: 12px;
            padding: 10px 10px 0px 10px;
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
        <div class="fullinfo">
            <div style="margin: 0px; padding: 0px 5px; float: left; height: auto; width: 100%; position: relative">
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
                            <asp:TextBox ID="txt_date" runat="server" name="date" Style="padding: 6px 10px;"></asp:TextBox>
                        </div>
                        <div class="filter-item">
                            <asp:Button ID="btn_find" runat="server" Text="Find" class="Buttoncss" Style="padding: 6px 10px;"
                                OnClick="btn_find_Click" />
                        </div>
                        <asp:Label ID="lbl_message" runat="server" Style="float: left; width: 100%; color: #f00; margin: 0px  0px 10px 0px"></asp:Label>
                    </div>
                </div>
                <div class="stats-grid">
                    <div class="stat-card total-card">
                        <div class="stat-title">Total Students</div>
                        <div class="stat-value" id="totalStudents" runat="server">0</div>
                    </div>
                    <div class="stat-card present-card">
                        <div class="stat-title">Present</div>
                        <div class="stat-value" id="presentStudents" runat="server">0</div>
                    </div>
                    <div class="stat-card absent-card">
                        <div class="stat-title">Absent</div>
                        <div class="stat-value" id="absentStudents" runat="server">0</div>
                    </div>
                    <div class="stat-card leave-card">
                        <div class="stat-title">Leave</div>
                        <div class="stat-value" id="leaveStudents" runat="server">0</div>
                    </div>
                </div>
                <div class="table-container">
                    <table>
                        <thead>
                            <tr>
                                <th>#</th>
                                <th>Student Name</th>
                                <th>Adm. No.</th>
                                <th>Roll</th>
                                <th>Status</th>
                            </tr>
                        </thead>
                        <tbody id="attendanceTableBody">
                            <asp:Repeater ID="rd_view" runat="server" OnItemDataBound="rd_view_ItemDataBound">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label></td>
                                        <td><%#Eval("studentname")%></td>
                                        <td><%#Eval("admissionserialnumber")%></td>
                                        <td><%#Eval("rollnumber")%></td>
                                        <td>
                                            <asp:Label ID="lbl_Attendance_Status" Class="status" runat="server" Text='<%#Bind("Attendance_Status")%>'></asp:Label></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
