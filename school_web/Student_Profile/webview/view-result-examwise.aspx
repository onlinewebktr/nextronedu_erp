<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="view-result-examwise.aspx.cs" Inherits="school_web.Student_Profile.webview.view_result_examwise" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Exam Results</title>
    <style>
        /* Reset and Base */
        * {
            box-sizing: border-box;
            margin: 0;
            padding: 0;
        }

        body {
            font-family: 'Segoe UI', sans-serif;
            background: linear-gradient(to right top, #f5f7fa, #c3cfe2);
            min-height: 100vh;
            padding: 1.5rem;
            /*display: flex;
            justify-content: center;*/
        }

        .container {
            width: 100%;
        }

        h2 {
            text-align: center;
            margin-bottom: 1.5rem;
            font-size: 1.5rem;
            color: #34495e;
            animation: fadeInDown 0.6s ease-in-out;
        }

        .exam-card {
            background: #ffffff;
            border-radius: 12px;
            box-shadow: 0 6px 15px rgba(0, 0, 0, 0.1);
            padding: 1.2rem 1.5rem;
            margin-bottom: 1.2rem;
            display: flex;
            justify-content: space-between;
            align-items: center;
            transition: transform 0.3s ease, box-shadow 0.3s ease;
            animation: slideUp 0.6s ease forwards;
            opacity: 0;
        }

            .exam-card:nth-child(2) {
                animation-delay: 0.2s;
            }

            .exam-card:nth-child(3) {
                animation-delay: 0.4s;
            }

            .exam-card:nth-child(4) {
                animation-delay: 0.6s;
            }

            .exam-card:hover {
                transform: translateY(-4px);
                box-shadow: 0 10px 18px rgba(0, 0, 0, 0.15);
            }

        .exam-name {
            font-size: 1rem;
            color: #2c3e50;
            font-weight: 600;
        }

        .btn-view {
            background: linear-gradient(135deg, #4facfe 0%, #00f2fe 100%);
            border: none;
            color: #fff;
            padding: 5px 15px 7px;
            border-radius: 8px;
            font-size: 0.9rem;
            cursor: pointer;
            transition: all 0.3s ease;
            font-weight: 500;
        }

            .btn-view:hover {
                background: linear-gradient(135deg, #00c6ff 0%, #0072ff 100%);
                transform: scale(1.05);
            }

        @media (max-width: 480px) {
            .exam-card {
                flex-direction: column;
                align-items: flex-start;
                gap: 0.8rem;
            }

            .btn-view {
                width: 100%;
                text-align: center;
            }
        }

        /* Animations */
        @keyframes slideUp {
            0% {
                transform: translateY(20px);
                opacity: 0;
            }

            100% {
                transform: translateY(0);
                opacity: 1;
            }
        }

        @keyframes fadeInDown {
            0% {
                transform: translateY(-20px);
                opacity: 0;
            }

            100% {
                transform: translateY(0);
                opacity: 1;
            }
        }




        .no-data-box {
            max-width: 400px;
            margin: 3rem auto;
            background: white;
            border-radius: 16px;
            padding: 2rem;
            box-shadow: 0 4px 16px rgba(0, 0, 0, 0.08);
            text-align: center;
            animation: fadeIn 0.6s ease-out forwards;
        }

            .no-data-box .icon-circle {
                width: 60px;
                height: 60px;
                background-color: #fef3c7;
                color: #f59e0b;
                display: flex;
                justify-content: center;
                align-items: center;
                border-radius: 50%;
                margin: 0 auto 1rem;
            }

            .no-data-box h2 {
                margin: 0.5rem 0;
                font-size: 1.3rem;
                color: #1f2937;
            }

            .no-data-box p {
                color: #6b7280;
                font-size: 0.95rem;
                margin-top: 0.2rem;
            }

        @keyframes fadeIn {
            from {
                opacity: 0;
                transform: translateY(20px);
            }

            to {
                opacity: 1;
                transform: translateY(0);
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">

            <asp:Panel ID="pnl_syllabus" runat="server">
                <asp:Repeater ID="rp_syllabus" runat="server" OnItemDataBound="rp_syllabus_ItemDataBound">
                    <ItemTemplate>
                        <div class="exam-card">
                            <div class="exam-name"><%#Eval("Exam_name") %></div>
                            <a id="rpcard_link" target="_blank" runat="server" class="btn-view">View Result</a>

                            <asp:Label ID="lbl_session_id" Visible="false" runat="server" Text='<%#Bind("Session_id") %>'></asp:Label>
                            <asp:Label ID="lbl_class_id" runat="server" Visible="false" Text='<%#Eval("Class_id") %>'></asp:Label>
                            <asp:Label ID="lbl_term_id" runat="server" Visible="false" Text='<%#Eval("Term_id") %>'></asp:Label>
                            <asp:Label ID="lbl_assesment_id" runat="server" Visible="false" Text='<%#Eval("Assesment_id") %>'></asp:Label>
                            <asp:Label ID="lbl_admissionserialnumber" runat="server" Visible="false" Text='<%#Eval("Admission_no") %>'></asp:Label>
                             <asp:Label ID="lbl_section" runat="server" Visible="false" Text='<%#Eval("Section") %>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </asp:Panel>


            <asp:Panel ID="pnl_bo_found" runat="server" Visible="false">
                <div class="no-data-box">
                    <div class="icon-circle">
                        <i data-lucide="alert-circle" style="width: 28px; height: 28px;"></i>
                    </div>
                    <h2>No Data Found</h2>
                    <p>Sorry, we couldn’t find any result available at the moment.</p>
                </div>
            </asp:Panel>
        </div>

        <script>
            function viewResult(examId) {
                // Redirect or fetch logic here
                alert("Redirecting to result for: " + examId);
                // window.location.href = `/result?exam=${examId}`;
            }
        </script>
    </form>
</body>
</html>
