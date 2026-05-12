<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="report-card.aspx.cs" Inherits="school_web._adminETutorProf.webview.report_card" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Student List with Filters</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet"/>
    <style>
        body {
            background: #f0f4f8;
            font-family: 'Segoe UI', sans-serif;
        }

        .header-title {
            font-size: 1.5rem;
            font-weight: 600;
            color: #333;
            margin-bottom: 1.5rem;
        }

        .filter-card {
            background: #ffffff;
            padding: 1rem;
            border-radius: 12px;
            box-shadow: 0 2px 8px rgba(0, 0, 0, 0.05);
            margin-bottom: 1.5rem;
        }

        .student-card {
            background: #ffffff;
            border-left: 5px solid #0d6efd;
            border-radius: 16px;
            box-shadow: 0 2px 8px rgba(0, 0, 0, 0.05);
            padding: 1rem 1.5rem;
            margin-bottom: 1rem;
            transition: 0.3s ease-in-out;
        }

            .student-card:hover {
                transform: translateY(-2px);
                box-shadow: 0 6px 16px rgba(0, 0, 0, 0.1);
            }

        .student-info strong {
            color: #0d6efd;
        }

        .print-btn {
            background-color: #198754;
            border: none;
            padding: 0.5rem 1rem;
            border-radius: 8px;
            color: white;
            font-weight: 500;
            transition: background-color 0.3s ease;
        }

            .print-btn:hover {
                background-color: #157347;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-4">
            <div class="filter-card">
                <div id="filterForm" class="row g-2">
                    <div class="col-6">
                        <label for="session" class="form-label">Session</label>
                        <asp:DropDownList ID="ddl_session" runat="server" class="form-select"></asp:DropDownList>
                    </div>
                    <div class="col-6">
                        <label for="class" class="form-label">Class</label>
                        <asp:DropDownList ID="ddl_class" runat="server" class="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddl_class_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col-6">
                        <label for="section" class="form-label">Section</label>
                        <asp:DropDownList ID="ddl_section" runat="server" class="form-select"></asp:DropDownList>
                    </div>
                    <div class="col-6">
                        <label for="term" class="form-label">Term</label>
                        <asp:DropDownList ID="ddl_term" runat="server" class="form-select"></asp:DropDownList>
                    </div>
                    <div class="col-12 d-grid mt-3">
                        <asp:Button ID="btn_find" runat="server" class="btn btn-primary" Text="Find" OnClick="btn_find_Click" />
                        <asp:Button ID="btn_all_rp" Visible="false" runat="server" Text="Button" style="display:none" />
                    </div>
                    <div class="col-12 mt-2">
                        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Font-Bold="true" />
                    </div>
                </div>
            </div>

            <asp:Repeater ID="rd_view" runat="server" OnItemDataBound="rd_view_ItemDataBound">
                <ItemTemplate>
                    <!-- Student Card -->
                    <div class="student-card d-flex justify-content-between align-items-center">
                        <div class="student-info">
                            <div><strong>Admission No:</strong> <%#Eval("Admission_no")%></div>
                            <div><strong>Name:</strong> <%#Eval("studentname")%></div>
                            <div><strong>Section:</strong> <%#Eval("Section")%></div>
                            <div><strong>Roll No:</strong> <%#Eval("rollnumber")%></div>
                        </div>
                        <asp:Label ID="Label1" runat="server" Visible="false" Text='<%#Bind("Admission_no")%>'></asp:Label>
                        <asp:Label ID="lbl_admissionserialnumber" Visible="false" runat="server" Text='<%#Bind("Admission_no")%>'></asp:Label>
                        <asp:Label ID="lbl_session_id" runat="server" Visible="false" Text='<%#Bind("Session_id")%>'></asp:Label>
                        <asp:Label ID="lbl_class_id" runat="server" Visible="false" Text='<%#Bind("Class_id")%>'></asp:Label>
                        <asp:Label ID="lbl_branch_id" runat="server" Visible="false" Text='<%#Bind("Branch_id")%>'></asp:Label>
                        <asp:Label ID="lbl_term_id" runat="server" Visible="false" Text='<%#Bind("Term")%>'></asp:Label>
                        <asp:Label ID="lbl_term_name" runat="server" Visible="false" Text='<%#Bind("Term_name")%>'></asp:Label>
                        <a id="rpcard_link" class="print-btn" runat="server" target="_blank">View</a>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>
