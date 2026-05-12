<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="atendance-summary.aspx.cs" Inherits="school_web._adminETutorProf.principle_profile.atendance_summary" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <title>Classwise Attendance Summary</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <style type="text/css">
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #f3f4f6;
            margin: 0;
            padding: 16px;
        }

        h1 {
            text-align: center;
            color: #111827;
            font-size: 1.8rem;
            margin-bottom: 24px;
        }

        /* FILTER SECTION */
        .filter-section {
            background-color: #ffffff;
            border: 1px solid #e5e7eb;
            border-radius: 12px;
            padding: 10px 10px 0px 10px;
            margin-bottom: 10px;
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
                font-size: 0.85rem;
                color: #374151;
                margin-bottom: 6px;
                display: block;
                font-weight: 500;
            }

            .filter-item select,
            .filter-item input[type="date"] {
                width: 100%;
                padding: 8px 10px;
                border: 1px solid #d1d5db;
                border-radius: 6px;
                font-size: 0.9rem;
                background-color: #f9fafb;
                transition: border-color 0.2s ease;
            }

                .filter-item select:focus,
                .filter-item input[type="date"]:focus {
                    outline: none;
                    border-color: #2563eb;
                    box-shadow: 0 0 0 2px rgba(37, 99, 235, 0.2);
                }

        .Buttoncss {
            background-color: #2563eb;
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

        /* CARD SECTION */
        .card {
            background-color: #ffffff;
            border: 1px solid #e5e7eb;
            border-radius: 12px;
            padding: 10px;
            margin-bottom: 10px !important;
            box-shadow: 0 1px 4px rgba(0, 0, 0, 0.04);
            transition: box-shadow 0.3s ease, transform 0.2s ease;
        }

            .card:hover {
                box-shadow: 0 6px 12px rgba(0, 0, 0, 0.1);
                transform: translateY(-3px);
            }

        .card-header {
            display: flex;
            justify-content: space-between;
            align-items: baseline;
            margin-bottom: 12px;
        }

            .card-header div {
                font-weight: 600;
                color: #111827;
                font-size: 1.05rem;
            }

            .card-header small {
                color: #6b7280;
                font-size: 0.85rem;
            }

        .info-grid {
            display: grid;
            grid-template-columns: repeat(2, 1fr);
            gap: 10px;
        }

        .info-box {
            border-radius: 6px;
            padding: 8px 10px;
            display: flex;
            justify-content: space-between;
            align-items: center;
            font-size: 0.95rem;
            opacity: 0;
            transform: translateY(10px);
            animation: fadeSlideUp 0.5s forwards;
            font-weight: 500;
        }

            .info-box:nth-child(1) {
                animation-delay: 0.1s;
            }

            .info-box:nth-child(2) {
                animation-delay: 0.2s;
            }

            .info-box:nth-child(3) {
                animation-delay: 0.3s;
            }

            .info-box:nth-child(4) {
                animation-delay: 0.4s;
            }

        @keyframes fadeSlideUp {
            to {
                opacity: 1;
                transform: translateY(0);
            }
        }

        .present {
            background-color: #e0f2f1;
            color: #00695c;
            border: 1px solid #80cbc4;
        }

        .absent {
            background-color: #ffebee;
            color: #b71c1c;
            border: 1px solid #ef9a9a;
        }

        .leave {
            background-color: #fff8e1;
            color: #f57f17;
            border: 1px solid #ffe082;
        }

        .total {
            background-color: #e3f2fd;
            color: #0d47a1;
            border: 1px solid #90caf9;
        }
    </style>
    <script src="../../assets/Angular/angular.min.js"></script>
    <link href="../../assets/css/bootstrap.min.css" rel="stylesheet" />
    <script src="../../assets/js/jquery.js"></script>
    <script src="../../assets/js/bootstrap.min.js"></script>
    <link href="../../assets/dropdownmultiselection/style.css" rel="stylesheet" />
    <script src="../../assets/dropdownmultiselection/bootstrap-multiselect.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#<%=ddl_classs.ClientID%>").multiselect({
                includeSelectAllOption: true
            });
        });
    </script>

    <style>
        .custom-select {
            display: inline-block;
            width: 100%;
            height: 39px;
        }

        .card-header {
            padding: 0px 0px 5px 0px;
            margin-bottom: 0;
            background-color: rgb(255 255 255 / 3%);
            border-bottom: 1px solid rgb(255 255 255 / 13%);
        }
    </style>
    <style>
        :root {
            --primary: #6366f1;
            --present: #10b981;
            --absent: #f43f5e;
            --leave: #fbbf24;
            --overlay: rgba(15, 23, 42, 0.6);
            --white: #ffffff;
        }

        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }


        /* --- Trigger Link --- */
        .trigger-link {
            font-weight: 600;
            color: var(--primary);
            text-decoration: none;
            border-bottom: 2px solid var(--primary);
            cursor: pointer;
            transition: 0.3s;
        }

            .trigger-link:hover {
                opacity: 0.7;
            }

        /* --- Modal Overlay --- */
        .modal-overlay {
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background: var(--overlay);
            backdrop-filter: blur(10px);
            z-index: 9999;
            display: none; /* Controlled by JS */
            padding: 25px 20px; /* Forces space around the popup */
            overflow-y: auto; /* Allows the whole popup to shift if needed */
        }

            .modal-overlay.active {
                display: flex;
            }

        /* --- Modal Content Box --- */
        .modal-content {
            background: #f8fafc;
            width: 100%;
            max-width: 480px;
            margin: auto; /* MAGIC: Keeps top AND bottom space equal */
            border-radius: 32px;
            display: flex;
            flex-direction: column;
            max-height: calc(100vh - 45px); /* Ensures it never hits screen edges */
            box-shadow: 0 25px 50px -12px rgba(0, 0, 0, 0.4);
            animation: modalPop 0.4s cubic-bezier(0.175, 0.885, 0.32, 1.1);
            overflow: hidden;
        }

        @keyframes modalPop {
            from {
                opacity: 0;
                transform: scale(0.9) translateY(20px);
            }

            to {
                opacity: 1;
                transform: scale(1) translateY(0);
            }
        }

        /* Fixed Header */
        .modal-header {
            padding: 10px 15px;
            background: #ffffff;
            display: flex;
            justify-content: space-between;
            align-items: center;
            border-bottom: 1px solid #f1f5f9;
        }

            .modal-header h3 {
                font-size: 1.2rem;
                color: #1e293b;
                font-weight: 700;
            }

        .close-btn {
            background: #f1f5f9;
            border: none;
            width: 35px;
            height: 35px;
            padding: 0px 0px 3px 0px;
            border-radius: 50%;
            cursor: pointer;
            font-size: 21px;
            color: #64748b;
            display: flex;
            align-items: center;
            justify-content: center;
            transition: 0.2s;
            text-decoration: none;
        }

            .close-btn:hover {
                background: #e2e8f0;
                color: #000;
            }

        /* --- Scrollable Body --- */
        .modal-body {
            padding: 15px;
            overflow-y: auto;
            flex: 1;
        }

            /* Custom Scrollbar for the list */
            .modal-body::-webkit-scrollbar {
                width: 5px;
            }

            .modal-body::-webkit-scrollbar-thumb {
                background: #cbd5e1;
                border-radius: 10px;
            }

        /* --- Attendance Cards --- */
        .activity-card {
            background: var(--white);
            border-radius: 20px;
            padding: 12px 15px;
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 5px;
            position: relative;
            box-shadow: 0 4px 6px -1px rgba(0, 0, 0, 0.02);
            border: 1px solid #10b981;
        }

            /* The Colored Curved Bar on Left */
            .activity-card::before {
                content: "";
                position: absolute;
                left: 0;
                top: 15%;
                width: 6px;
                height: 70%;
                border-radius: 0 10px 10px 0;
            }

        ._present::before {
            background: var(--present);
        }

        ._absent::before {
            background: var(--absent);
        }

        ._leave::before {
            background: var(--leave);
        }
        ._not_taken::before{
            background: #94a3b8;
        }

        .date-info h4 {
            font-size: 1rem;
            color: #1e293b;
            margin-bottom: 4px;
        }

        .date-info p {
            font-size: 0.85rem;
            color: #94a3b8;
            font-weight: 400;
        }

        /* Badges */
        .badge {
            padding: 7px 16px;
            border-radius: 12px;
            font-size: 0.75rem;
            font-weight: 800;
            letter-spacing: 0.5px;
        }

        .tag_present {
            background: #dcfce7;
            color: var(--present);
        }

        .tag_absent {
            background: #fee2e2;
            color: var(--absent);
        }

        .tag_leave {
            background: #fef3c7;
            color: var(--leave);
        }

        .tag_not_taken {
            background: #94a3b8;
            color: #1e539f;
        }

        .bdr_absent {
            border: 1px solid #10b981;
        }

        .bdr_leave {
            border: 1px solid #fbbf24;
        }

        .bdr_absent {
            border: 1px solid #f43f5e;
        }
        .bdr_not_taken{
          border: 1px solid #94a3b8;
        }

        /* Spacer at the very bottom of the list */
        .scroll-spacer {
            height: 10px;
        }

        .mdl-p {
            margin: 0px 0px 0px 0px;
        } 
        .empty-state-wrapper {
            display: flex;
            flex-direction: column;
            align-items: center;
            justify-content: center;
            padding: 60px 30px;
            text-align: center;
            background: #ffffff;
            border-radius: 32px;
            border: 1px dashed #e2e8f0;
            margin: 20px;
            animation: fadeInUp 0.5s ease-out;
        }

        /* Iconic Background */
        .glass-circle {
            width: 100px;
            height: 100px;
            background: rgba(99, 102, 241, 0.05);
            border-radius: 50%;
            display: flex;
            align-items: center;
            justify-content: center;
            margin-bottom: 24px;
            position: relative;
        }

            .glass-circle::after {
                content: '';
                position: absolute;
                width: 120px;
                height: 120px;
                border: 1px solid rgba(99, 102, 241, 0.1);
                border-radius: 50%;
                animation: pulse 2s infinite;
            }

        .empty-icon {
            width: 48px;
            height: 48px;
        }

        /* Typography */
        .empty-state-wrapper h2 {
            color: #1e293b;
            font-size: 1.4rem;
            font-weight: 700;
            margin-bottom: 12px;
            letter-spacing: -0.5px;
        }

        .empty-state-wrapper p {
            color: #64748b;
            font-size: 0.95rem;
            line-height: 1.6;
            max-width: 320px;
            margin-bottom: 30px;
        }

        /* Buttons */
        .action-footer {
            display: flex;
            gap: 12px;
        }

        .btn-primary {
            background: linear-gradient(135deg, #6366f1 0%, #4f46e5 100%);
            color: white;
            border: none;
            padding: 12px 24px;
            border-radius: 14px;
            font-weight: 600;
            font-family: inherit;
            cursor: pointer;
            box-shadow: 0 10px 15px -3px rgba(79, 70, 229, 0.3);
            transition: all 0.3s ease;
        }

        .btn-secondary {
            background: #f1f5f9;
            color: #475569;
            border: none;
            padding: 12px 24px;
            border-radius: 14px;
            font-weight: 600;
            cursor: pointer;
            transition: 0.3s;
        }

        .btn-primary:hover {
            transform: translateY(-2px);
            box-shadow: 0 15px 20px -5px rgba(79, 70, 229, 0.4);
        }

        .btn-secondary:hover {
            background: #e2e8f0;
        }

        /* Animations */
        @keyframes fadeInUp {
            from {
                opacity: 0;
                transform: translateY(30px);
            }

            to {
                opacity: 1;
                transform: translateY(0);
            }
        }

        @keyframes pulse {
            0% {
                transform: scale(1);
                opacity: 1;
            }

            100% {
                transform: scale(1.3);
                opacity: 0;
            }
        }

        .hidden {
            display: none;
        }
    </style>

    <style>
        :root {
            --bg: #f8fafc;
            --total: #6366f1;
            --present: #10b981;
            --absent: #f43f5e;
            --leave: #f59e0b;
            --not: #94a3b8;
        }


        .smrycontainer {
            display: grid;
            grid-template-columns: repeat(2, 1fr); /* 2 per row on mobile */
            gap: 12px;
            width: 100%;
            max-width: 100%;
            margin: 0px 0px 5px 0px;
        }

        @media (min-width: 768px) {
            .smrycontainer {
                grid-template-columns: repeat(5, 1fr); /* 1 row on desktop */
            }
        }

        .smrycard {
            background: #ffffff;
            padding: 10px 16px 3px 16px;
            border-radius: 24px;
            box-shadow: 0 4px 20px rgba(0, 0, 0, 0.03);
            display: flex;
            flex-direction: column;
            align-items: flex-start;
            transition: all 0.3s ease;
            border: 1px solid rgba(0, 0, 0, 0.02);
        }

            .smrycard:hover {
                transform: translateY(-5px);
                box-shadow: 0 12px 30px rgba(0, 0, 0, 0.06);
            }

        /* The little colored dot */
        .dot {
            width: 8px;
            height: 8px;
            border-radius: 50%;
            margin-bottom: 8px;
            box-shadow: 0 0 12px rgba(0,0,0,0.1);
        }

        .label {
            color: #64748b;
            font-size: 0.7rem;
            font-weight: 500;
            margin-bottom: 0px;
        }

        .value {
            color: #0f172a;
            font-size: 1.4rem;
            font-weight: 800;
            letter-spacing: -0.5px;
        }

        /* Color assignments */
        .c-total .dot {
            background-color: var(--total);
            box-shadow: 0 0 8px var(--total);
        }

        .c-present .dot {
            background-color: var(--present);
            box-shadow: 0 0 8px var(--present);
        }

        .c-absent .dot {
            background-color: var(--absent);
            box-shadow: 0 0 8px var(--absent);
        }

        .c-leave .dot {
            background-color: var(--leave);
            box-shadow: 0 0 8px var(--leave);
        }

        .c-not .dot {
            background-color: var(--not);
            box-shadow: 0 0 8px var(--not);
        }

        /* Make the 5th card look clean on mobile */
        @media (max-width: 767px) {
            .c-not {
                grid-column: span 2;
                align-items: center;
                text-align: center;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <%--<h1>Classwise Attendance Summary</h1>--%>
        <div data-ng-app="myApp" data-ng-controller="myctrlAttndnce">
            <!-- FILTER SECTION (Class + Date + Filter in one line) -->
            <div class="filter-section">
                <div class="filter-form">
                    <div class="filter-item">
                        <label for="class">Class</label>
                        <asp:ListBox ID="ddl_classs" runat="server" CssClass="form-select" SelectionMode="Multiple"></asp:ListBox>
                        <%--<asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
                    <select id="class" name="class">
                        <option value="">Select</option>
                        <option value="9">Class 9</option>
                        <option value="10">Class 10</option>
                    </select>--%>
                    </div>
                    <div class="filter-item">
                        <label for="date">Date</label>
                        <asp:TextBox ID="txt_date" runat="server" type="date" name="date"></asp:TextBox>
                        <%--<input type="date" id="date" name="date" style="width: 84%;" />--%>
                    </div>
                    <div class="filter-item">
                        <asp:Button ID="btn_find" runat="server" Text="Filter" class="Buttoncss" OnClick="btn_find_Click" />
                    </div>

                    <asp:Label ID="lbl_message" runat="server" Style="float: left; width: 100%;"></asp:Label>
                </div>
            </div>



            <div class="smrycontainer">
                <div class="smrycard c-total" style="border: 2px solid rgb(99 102 241);" onclick="toggleModal()" data-ng-click="ButtonClickTtlAll()">
                    <div class="dot"></div>
                    <span class="label">Total Students</span>
                    <span class="value" runat="server" id="ttlStd"></span>
                </div>

                <div class="smrycard c-present" style="border: 2px solid #10b981;" onclick="toggleModal()" data-ng-click="ButtonClickTtlPersent()">
                    <div class="dot"></div>
                    <span class="label">Present</span>
                    <span class="value" runat="server" id="ttlpers"></span>
                </div>

                <div class="smrycard c-absent" style="border: 2px solid #f43f5e;" onclick="toggleModal()" data-ng-click="ButtonClickTtlAbsent()">
                    <div class="dot"></div>
                    <span class="label">Absent</span>
                    <span class="value" runat="server" id="ttlAbsnt"></span>
                </div>

                <div class="smrycard c-leave" style="border: 2px solid #f59e0b" onclick="toggleModal()" data-ng-click="ButtonClickTtlLeave()">
                    <div class="dot"></div>
                    <span class="label">Leave</span>
                    <span class="value" runat="server" id="ttlleavE"></span>
                </div>

                <div class="smrycard c-not" style="border: 2px solid #94a3b8" onclick="toggleModal()" data-ng-click="ButtonClickTtlNotTaken()">
                    <div class="dot"></div>
                    <span class="label">Not Taken</span>
                    <span class="value" runat="server" id="ttllNotTkn"></span>
                </div>
            </div>

            <!-- SAMPLE CARD 1 -->
            <asp:Repeater ID="rd_view" runat="server" OnItemDataBound="rd_view_ItemDataBound">
                <ItemTemplate>
                    <asp:Label ID="lbl_ttl_std" runat="server" Visible="false" Text='<%#Bind("TtlStd")%>'></asp:Label>
                    <asp:Label ID="lbl_ttl_std_att" runat="server" Visible="false" Text='<%#Bind("TtlStudents")%>'></asp:Label>
                    <div class="card">
                        <div class="card-header">
                            <div>Class : <%#Eval("class")%> <small>Section : <%#Eval("Section")%></small></div>
                            <small>Marked :
                            <asp:Label ID="lbl_is_marked" runat="server"></asp:Label></small>
                        </div>
                        <div class="info-grid">
                            <div class="info-box present" onclick="toggleModal()" data-ng-click="ButtonClickPersent(<%#Eval("Session_id")%>,<%#Eval("Class_id")%>,'<%#Eval("Section")%>')"><span>Present</span><strong><%#Eval("TtlPresent")%></strong></div>
                            <div class="info-box absent" onclick="toggleModal()" data-ng-click="ButtonClickAbsent(<%#Eval("Session_id")%>,<%#Eval("Class_id")%>,'<%#Eval("Section")%>')"><span>Absent</span><strong><%#Eval("TtlAbsent")%></strong></div>
                            <div class="info-box leave" onclick="toggleModal()" data-ng-click="ButtonClickLeave(<%#Eval("Session_id")%>,<%#Eval("Class_id")%>,'<%#Eval("Section")%>')"><span>Leave</span><strong><%#Eval("TtlLeave")%></strong></div>
                            <div class="info-box total" onclick="toggleModal()" data-ng-click="ButtonClickTotal(<%#Eval("Session_id")%>,<%#Eval("Class_id")%>,'<%#Eval("Section")%>')"><span>Total Students</span><strong><%#Eval("TtlStudents")%></strong></div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <asp:HiddenField ID="hd_idate" runat="server" />
            <asp:HiddenField ID="hd_session_id" runat="server" />
            <asp:HiddenField ID="hd_class_id" runat="server" />

            <div class="modal-overlay" id="overlay" onclick="handleOutsideClick(event)">
                <div class="modal-content">
                    <div class="modal-header">
                        <h3>Attendance Status</h3>
                        <a href="#!" class="close-btn" onclick="toggleModal()">&times;</a>
                    </div>

                    <div class="modal-body">
                        <div id="foundStd hidden">
                            <div class="activity-card {{x.Status}} {{x.Status_bdr}}" data-ng-repeat="x in reportAtt">
                                <div class="date-info">
                                    <h4>{{x.Studentname}}</h4>
                                    <p class="mdl-p">Adm No. : {{x.Admission_no}}</p>

                                    <p class="mdl-p {{x.Class_name}}">Class : {{x.Class_name}},  Sec. : {{x.Sections}}</p>


                                    <p class="mdl-p">Roll No. : {{x.Roll_number}}</p>
                                </div>
                                <div class="badge {{x.Status_teg}}">{{x.Attendance_Status}}</div>
                            </div>
                        </div>




                        <div id="noStudentFound" class="empty-state-wrapper hidden">
                            <div class="glass-circle">
                                <svg class="empty-icon" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                                    <path d="M11 19C15.4183 19 19 15.4183 19 11C19 6.58172 15.4183 3 11 3C6.58172 3 3 6.58172 3 11C3 15.4183 6.58172 19 11 19Z" stroke="url(#paint0_linear)" stroke-width="2" />
                                    <path d="M21 21L17 17" stroke="#94a3b8" stroke-width="2" stroke-linecap="round" />
                                    <path d="M9 9L13 13M13 9L9 13" stroke="#f43f5e" stroke-width="2" stroke-linecap="round" />
                                    <defs>
                                        <linearGradient id="paint0_linear" x1="3" y1="3" x2="19" y2="19" gradientUnits="userSpaceOnUse">
                                            <stop stop-color="#6366f1" />
                                            <stop offset="1" stop-color="#a855f7" />
                                        </linearGradient>
                                    </defs>
                                </svg>
                            </div>

                            <h2>No Match Found</h2>
                            <p>No match found. Please check your search filters or change the class/section.</p>

                            <div class="action-footer">
                                <%--<button class="btn-secondary" onclick="resetSearch()">Clear Search</button>--%>
                                <a href="#!" onclick="toggleModal()" class="btn-primary" style="text-decoration: none">Close</a>
                            </div>
                        </div>

                        <div class="scroll-spacer"></div>
                    </div>
                </div>
            </div>
        </div>

        <script type="text/javascript">
            const overlay = document.getElementById('overlay');
            function toggleModal() {
                overlay.classList.toggle('active');

                // Prevent main page from scrolling when popup is open
                if (overlay.classList.contains('active')) {
                    document.body.style.overflow = 'hidden';
                } else {
                    document.body.style.overflow = 'auto';
                }
            }

            // Close when clicking on the dark blurred area
            function handleOutsideClick(e) {
                if (e.target === overlay) {
                    toggleModal();
                }
            }
        </script>
        <script type="text/javascript">
            var app = angular.module('myApp', []);
            app.controller('myctrlAttndnce', function ($scope, $http, $exceptionHandler) {
                var idate = $("#<%=hd_idate.ClientID%>").val();
                var sessions_id = $("#<%=hd_session_id.ClientID%>").val();
                var classes_id = $("#<%=hd_class_id.ClientID%>").val();
                $scope.ButtonClickPersent = function (Session_id, Class_id, Sec) {
                    var statusS = "Present";
                    $http.get("WebService1.asmx/fetch_attendance_status_of_std", { params: { "Session_id": Session_id, "Class_id": Class_id, "Section": Sec, "Idate": idate, "StatusS": statusS } }).then(function (response) {
                        $scope.reportAtt = response.data;
                        if ($scope.reportAtt == "") {
                            $("#foundStd").addClass("hidden");
                            $("#noStudentFound").removeClass("hidden");
                        }
                        else {
                            $("#foundStd").removeClass("hidden");
                            $("#noStudentFound").addClass("hidden");
                        }
                    }).catch(function (error) {
                        // Handle the error here
                        console.error("Error fetching report card data:", error);
                        alert("Something went wrong. Please try again.");
                    });
                }
                $scope.ButtonClickAbsent = function (Session_id, Class_id, Sec) {
                    var statusS = "Absent";
                    $http.get("WebService1.asmx/fetch_attendance_status_of_std", { params: { "Session_id": Session_id, "Class_id": Class_id, "Section": Sec, "Idate": idate, "StatusS": statusS } }).then(function (response) {
                        $scope.reportAtt = response.data;
                        if ($scope.reportAtt == "") {
                            $("#foundStd").addClass("hidden");
                            $("#noStudentFound").removeClass("hidden");
                        }
                        else {
                            $("#foundStd").removeClass("hidden");
                            $("#noStudentFound").addClass("hidden");
                        }
                    }).catch(function (error) {
                        // Handle the error here
                        console.error("Error fetching report card data:", error);
                        alert("Something went wrong. Please try again.");
                    });
                }
                $scope.ButtonClickLeave = function (Session_id, Class_id, Sec) {
                    var statusS = "Leave";
                    $http.get("WebService1.asmx/fetch_attendance_status_of_std", { params: { "Session_id": Session_id, "Class_id": Class_id, "Section": Sec, "Idate": idate, "StatusS": statusS } }).then(function (response) {
                        $scope.reportAtt = response.data;
                        if ($scope.reportAtt == "") {
                            $("#foundStd").addClass("hidden");
                            $("#noStudentFound").removeClass("hidden");
                        }
                        else {
                            $("#foundStd").removeClass("hidden");
                            $("#noStudentFound").addClass("hidden");
                        }
                    }).catch(function (error) {
                        // Handle the error here
                        console.error("Error fetching report card data:", error);
                        alert("Something went wrong. Please try again.");
                    });
                }
                $scope.ButtonClickTotal = function (Session_id, Class_id, Sec) {
                    var statusS = "TotalS";
                    $http.get("WebService1.asmx/fetch_attendance_status_of_std", { params: { "Session_id": Session_id, "Class_id": Class_id, "Section": Sec, "Idate": idate, "StatusS": statusS } }).then(function (response) {
                        $scope.reportAtt = response.data;
                        if ($scope.reportAtt == "") {
                            $("#foundStd").addClass("hidden");
                            $("#noStudentFound").removeClass("hidden");
                        }
                        else {
                            $("#foundStd").removeClass("hidden");
                            $("#noStudentFound").addClass("hidden");
                        }
                    }).catch(function (error) {
                        // Handle the error here
                        console.error("Error fetching report card data:", error);
                        alert("Something went wrong. Please try again.");
                    });
                }



                /*============================================*/
                $scope.ButtonClickTtlPersent = function () {
                    var statusS = "Present";
                    var Sec = "OverALL";
                    $http.get("WebService1.asmx/fetch_attendance_status_of_std", { params: { "Session_id": sessions_id, "Class_id": classes_id, "Section": Sec, "Idate": idate, "StatusS": statusS } }).then(function (response) {
                        $scope.reportAtt = response.data;
                        if ($scope.reportAtt == "") {
                            $("#foundStd").addClass("hidden");
                            $("#noStudentFound").removeClass("hidden");
                        }
                        else {
                            $("#foundStd").removeClass("hidden");
                            $("#noStudentFound").addClass("hidden");
                        }
                    }).catch(function (error) {
                        // Handle the error here
                        console.error("Error fetching report card data:", error);
                        alert("Something went wrong. Please try again.");
                    });
                }

                $scope.ButtonClickTtlAbsent = function () {
                    var statusS = "Absent";
                    var Sec = "OverALL";
                    $http.get("WebService1.asmx/fetch_attendance_status_of_std", { params: { "Session_id": sessions_id, "Class_id": classes_id, "Section": Sec, "Idate": idate, "StatusS": statusS } }).then(function (response) {
                        $scope.reportAtt = response.data;
                        if ($scope.reportAtt == "") {
                            $("#foundStd").addClass("hidden");
                            $("#noStudentFound").removeClass("hidden");
                        }
                        else {
                            $("#foundStd").removeClass("hidden");
                            $("#noStudentFound").addClass("hidden");
                        }
                    }).catch(function (error) {
                        // Handle the error here
                        console.error("Error fetching report card data:", error);
                        alert("Something went wrong. Please try again.");
                    });
                }
                $scope.ButtonClickTtlLeave = function () {
                    var statusS = "Leave";
                    var Sec = "OverALL";
                    $http.get("WebService1.asmx/fetch_attendance_status_of_std", { params: { "Session_id": sessions_id, "Class_id": classes_id, "Section": Sec, "Idate": idate, "StatusS": statusS } }).then(function (response) {
                        $scope.reportAtt = response.data;
                        if ($scope.reportAtt == "") {
                            $("#foundStd").addClass("hidden");
                            $("#noStudentFound").removeClass("hidden");
                        }
                        else {
                            $("#foundStd").removeClass("hidden");
                            $("#noStudentFound").addClass("hidden");
                        }
                    }).catch(function (error) {
                        // Handle the error here
                        console.error("Error fetching report card data:", error);
                        alert("Something went wrong. Please try again.");
                    });
                }

                $scope.ButtonClickTtlNotTaken = function () {
                    var statusS = "NotTaken";
                    var Sec = "OverALL";
                    $http.get("WebService1.asmx/fetch_attendance_status_of_std", { params: { "Session_id": sessions_id, "Class_id": classes_id, "Section": Sec, "Idate": idate, "StatusS": statusS } }).then(function (response) {
                        $scope.reportAtt = response.data;
                        if ($scope.reportAtt == "") {
                            $("#foundStd").addClass("hidden");
                            $("#noStudentFound").removeClass("hidden");
                        }
                        else {
                            $("#foundStd").removeClass("hidden");
                            $("#noStudentFound").addClass("hidden");
                        }
                    }).catch(function (error) {
                        // Handle the error here
                        console.error("Error fetching report card data:", error);
                        alert("Something went wrong. Please try again.");
                    });
                }

                $scope.ButtonClickTtlAll = function () {
                    var statusS = "TotalALL";
                    var Sec = "OverALL";
                    $http.get("WebService1.asmx/fetch_attendance_status_of_std", { params: { "Session_id": sessions_id, "Class_id": classes_id, "Section": Sec, "Idate": idate, "StatusS": statusS } }).then(function (response) {
                        $scope.reportAtt = response.data;
                        if ($scope.reportAtt == "") {
                            $("#foundStd").addClass("hidden");
                            $("#noStudentFound").removeClass("hidden");
                        }
                        else {
                            $("#foundStd").removeClass("hidden");
                            $("#noStudentFound").addClass("hidden");
                        }
                    }).catch(function (error) {
                        // Handle the error here
                        console.error("Error fetching report card data:", error);
                        alert("Something went wrong. Please try again.");
                    });
                }
            });
        </script>
    </form>
</body>
</html>
