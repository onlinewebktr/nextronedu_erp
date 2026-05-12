<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="student-strength.aspx.cs" Inherits="school_web._adminETutorProf.principle_profile.student_strength" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Student Strength Dashboard</title>
    <style>
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background: #eef2f7;
            margin: 0;
            padding: 0;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            color: #333;
        }

        .container {
            width: 100%;
            max-width: 100%;
            background: #ffffff;
            padding: 25px 10px;
            border-radius: 16px;
            box-shadow: 0 6px 15px rgba(0, 0, 0, 0.1);
            opacity: 0;
            animation: fadeIn 1s forwards;
        }

        h1 {
            text-align: center;
            margin-bottom: 25px;
            color: #2a4365;
            font-size: 1.8em;
        }

        .card {
            background: #edf2fa;
            margin-bottom: 14px;
            padding: 15px 20px;
            border-left: 6px solid #4a90e2;
            border-radius: 8px;
            display: flex;
            justify-content: space-between;
            align-items: center;
            box-shadow: 0 2px 6px rgba(0, 0, 0, 0.05);
            transition: transform 0.2s ease, box-shadow 0.2s ease;
            opacity: 0;
            animation: cardFadeIn 0.8s forwards;
        }

            .card:nth-child(1) {
                animation-delay: 0.2s;
            }

            .card:nth-child(2) {
                animation-delay: 0.4s;
            }

            .card:nth-child(3) {
                animation-delay: 0.6s;
            }

            .card:nth-child(4) {
                animation-delay: 0.8s;
            }

            .card:nth-child(5) {
                animation-delay: 1s;
            }

            .card:hover {
                transform: translateY(-5px);
                box-shadow: 0 6px 15px rgba(0, 0, 0, 0.1);
            }

        .label {
            font-weight: 600;
            color: #2a4365;
        }

        .value {
            font-size: 1.3em;
            font-weight: bold;
        }

        /* Different border colors + background + value colors */
        .card.new {
            border-left-color: #34a853;
            background: #e6f4ea;
        }

            .card.new .value {
                color: #34a853;
            }

        .card.old {
            border-left-color: #1a73e8;
            background: #e8f0fe;
        }

            .card.old .value {
                color: #1a73e8;
            }

        .card.tc {
            border-left-color: #fbbc04;
            background: #fff7e6;
        }

            .card.tc .value {
                color: #fbbc04;
            }

        .card.inactive {
            border-left-color: #ea4335;
            background: #fce8e6;
        }

            .card.inactive .value {
                color: #ea4335;
            }

        .card.current {
            border-left-color: #673ab7;
            background: #ede7f6;
        }

            .card.current .value {
                color: #673ab7;
            }

        @keyframes fadeIn {
            from {
                opacity: 0;
            }

            to {
                opacity: 1;
            }
        }

        @keyframes cardFadeIn {
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
            <%--<h1>📊 Student Strength</h1>--%>
            <div class="card new">
                <span class="label">🆕 New Students</span>
                <span class="value"><asp:Label ID="lbl_new_student" runat="server"></asp:Label></span>
            </div>
            <div class="card old">
                <span class="label">🧑‍🎓 Old Students</span>
                <span class="value"><asp:Label ID="lbl_old_std" runat="server"></asp:Label></span>
            </div>
            <div class="card tc">
                <span class="label">📄 TC Students</span>
                <span class="value"><asp:Label ID="lbl_tc_std" runat="server"></asp:Label></span>
            </div>
            <div class="card inactive">
                <span class="label">🚫 Inactive Students</span>
                <span class="value"><asp:Label ID="lbl_inactive_std" runat="server"></asp:Label></span>
            </div>
            <div class="card current">
                <span class="label">👥 Current Strength</span>
                <span class="value"><asp:Label ID="lbl_current_std" runat="server"></asp:Label></span>
            </div>
        </div>
    </form>
</body>
</html>
