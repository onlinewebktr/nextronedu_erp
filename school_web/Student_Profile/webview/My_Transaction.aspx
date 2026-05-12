<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="My_Transaction.aspx.cs" Inherits="school_web.Student_Profile.webview.My_Transaction" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">

    <title>Animated Payment History</title>

    <link href="https://fonts.googleapis.com/css2?family=Poppins:wght@400;500;600;700;800&display=swap" rel="stylesheet">

    <style>
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }

        body {
            background: #edf3ff;
            font-family: 'Poppins',sans-serif;
            overflow-x: hidden;
            padding-bottom: 20px;
        }

        /* BACKGROUND EFFECT */

        .bg-circle {
            position: fixed;
            border-radius: 50%;
            filter: blur(90px);
            z-index: -1;
            animation: float 8s infinite ease-in-out;
        }

        .bg1 {
            width: 220px;
            height: 220px;
            background: #4f7dff;
            top: -60px;
            left: -50px;
        }

        .bg2 {
            width: 220px;
            height: 220px;
            background: #ff70b8;
            bottom: -40px;
            right: -50px;
            animation-delay: 2s;
        }

        .bg3 {
            width: 170px;
            height: 170px;
            background: #5df0c7;
            top: 45%;
            left: 35%;
            animation-delay: 4s;
        }

        @keyframes float {

            0% {
                transform: translateY(0px);
            }

            50% {
                transform: translateY(-20px);
            }

            100% {
                transform: translateY(0px);
            }
        }

        /* HEADER */

        .header {
            background: linear-gradient(135deg,#132c8d,#0f58d8,#29b6f6);
            padding: 20px 16px 85px;
            border-radius: 0 0 28px 28px;
            position: relative;
            overflow: hidden;
        }

            .header::before {
                content: '';
                position: absolute;
                width: 240px;
                height: 240px;
                border-radius: 50%;
                background: rgba(255,255,255,.08);
                top: -120px;
                right: -70px;
            }

        .header-flex {
            display: flex;
            align-items: center;
            gap: 14px;
            color: #fff;
        }

        .back-btn {
            width: 42px;
            height: 42px;
            border-radius: 14px;
            background: rgba(255,255,255,.15);
            display: flex;
            align-items: center;
            justify-content: center;
            font-size: 24px;
            backdrop-filter: blur(10px);
            cursor: pointer;
        }

        .header h2 {
            font-size: 24px;
            font-weight: 700;
        }

        .header p {
            font-size: 13px;
            opacity: .85;
        }

        /* MAIN */

        .container {
            padding: 0 14px;
            margin-top: -55px;
        }

        /* FILTER */

        .filter-box {
            background: rgba(255,255,255,.78);
            backdrop-filter: blur(18px);
            border-radius: 24px;
            padding: 16px;
            box-shadow: 0 8px 30px rgba(0,0,0,.08);
            margin-bottom: 18px;
            animation: fadeUp .6s ease;
        }

        .filter-row {
            display: flex;
            gap: 10px;
        }

        .select-box {
            flex: 1;
            height: 48px;
            border: none;
            outline: none;
            border-radius: 14px;
            padding: 0 14px;
            background: #f5f8ff;
            font-size: 15px;
            font-weight: 600;
            color: #22355c;
        }

        .search-btn {
            border: none;
            padding: 0 20px;
            border-radius: 14px;
            background: linear-gradient(135deg,#ff5ea8,#ff7b54);
            color: #fff;
            font-weight: 700;
            font-size: 14px;
            cursor: pointer;
            transition: .3s;
        }

            .search-btn:hover {
                transform: scale(1.05);
            }

        /* PAYMENT CARD */

        .payment-card {
            position: relative;
            overflow: hidden;
            background: rgba(255,255,255,.85);
            backdrop-filter: blur(20px);
            border-radius: 24px;
            padding: 16px;
            margin-bottom: 16px;
            box-shadow: 0 10px 35px rgba(0,0,0,.08);
            animation: fadeUp .7s ease;
            transition: .35s;
        }

            .payment-card:hover {
                transform: translateY(-5px);
            }

            /* LEFT BORDER */

            .payment-card::before {
                content: '';
                position: absolute;
                top: 0;
                left: 0;
                width: 6px;
                height: 100%;
                border-radius: 20px;
            }

        /* MULTI COLORS */

        .card-blue::before {
            background: linear-gradient(to bottom,#0f58d8,#29b6f6);
        }

        .card-pink::before {
            background: linear-gradient(to bottom,#ff4fa0,#ff7b54);
        }

        .card-green::before {
            background: linear-gradient(to bottom,#00b894,#55efc4);
        }

        /* TOP */

        .card-top {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 12px;
        }

        .receipt {
            display: flex;
            gap: 12px;
            align-items: center;
        }

        .icon-box {
            width: 48px;
            height: 48px;
            border-radius: 16px;
            display: flex;
            align-items: center;
            justify-content: center;
            font-size: 22px;
            color: #fff;
        }

        .blue {
            background: linear-gradient(135deg,#0f58d8,#29b6f6);
        }

        .pink {
            background: linear-gradient(135deg,#ff4fa0,#ff7b54);
        }

        .green {
            background: linear-gradient(135deg,#00b894,#55efc4);
        }

        .receipt h3 {
            font-size: 15px;
            font-weight: 700;
            color: #20345d;
        }

        .receipt p {
            font-size: 11px;
            color: #7c8ca9;
        }

        .status {
            padding: 6px 12px;
            border-radius: 30px;
            font-size: 11px;
            font-weight: 700;
            color: #fff;
        }

        .success {
            background: linear-gradient(135deg,#00b894,#00d68f);
        }

        /* DETAILS */

        .details {
            display: grid;
            grid-template-columns: 1fr 1fr;
            gap: 10px;
        }

        .detail-box {
            background: #f7faff;
            border-radius: 14px;
            padding: 10px;
        }

        .label {
            font-size: 10px;
            color: #7f8da8;
            margin-bottom: 4px;
            font-weight: 600;
        }

        .value {
            font-size: 13px;
            color: #213860;
            font-weight: 700;
        }

        /* FOOTER */

        .card-footer {
            margin-top: 14px;
            border-radius: 18px;
            padding: 14px;
            display: flex;
            justify-content: space-between;
            align-items: center;
            color: #fff;
        }

        .footer-blue {
            background: linear-gradient(135deg,#0f58d8,#29b6f6);
        }

        .footer-pink {
            background: linear-gradient(135deg,#ff4fa0,#ff7b54);
        }

        .footer-green {
            background: linear-gradient(135deg,#00b894,#55efc4);
        }

        .amount small {
            font-size: 10px;
            opacity: .85;
        }

        .amount h2 {
            margin-top: 2px;
            font-size: 24px;
        }

        .download-btn {
            border: none;
            background: rgba(255,255,255,.18);
            color: #fff;
            padding: 10px 14px;
            border-radius: 12px;
            font-size: 12px;
            font-weight: 700;
            backdrop-filter: blur(10px);
            transition: .3s;
            cursor: pointer;
            text-decoration: none;
        }

            .download-btn:hover {
                background: #fff;
                color: #111;
            }

        /* ANIMATION */

        @keyframes fadeUp {

            from {
                opacity: 0;
                transform: translateY(25px);
            }

            to {
                opacity: 1;
                transform: translateY(0);
            }
        }

        /* MOBILE */

        @media(max-width:480px) {

            .details {
                grid-template-columns: 1fr 1fr;
            }
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">

        <!-- BACKGROUND -->

        <div class="bg-circle bg1"></div>
        <div class="bg-circle bg2"></div>
        <div class="bg-circle bg3"></div>

        <!-- HEADER -->

        <div class="header">
            <div class="header-flex">
                <%--<div class="back-btn">
                    ←
                </div>--%>
                <div>
                    <h2>Payment History</h2>
                    <p>Student fee transaction records</p>
                </div>
            </div>
        </div>

        <!-- MAIN -->

        <div class="container">
            <!-- FILTER -->
            <div class="filter-box">
                <div class="filter-row">
                    <asp:DropDownList ID="ddl_session" runat="server" class="select-box"></asp:DropDownList>
                    <asp:Button ID="btn_find_session" OnClick="btn_find_session_Click" runat="server" class="search-btn" Text="Search" />
                </div>
            </div>


            <div id="yeSData" runat="server">
                <asp:Repeater ID="RPDetails" runat="server" OnItemDataBound="RPDetails_ItemDataBound">
                    <ItemTemplate>
                        <div class="payment-card card-blue" id="paycard" runat="server">
                            <div class="card-top">
                                <div class="receipt">
                                    <div class="icon-box blue" id="iconBox" runat="server">
                                        💳
                                    </div>
                                    <div>
                                        <h3><%#Eval("Slip_no")%></h3>
                                        <p>Slip No.</p>
                                    </div>
                                </div>
                                <div class="status success">
                                    SUCCESS
                                </div>
                            </div>
                            <div class="details">
                                <div class="detail-box">
                                    <div class="label">DATE</div>
                                    <div class="value"><%#Eval("Date")%></div>
                                </div>
                                <div class="detail-box">
                                    <div class="label">MODE</div>
                                    <div class="value"><%#Eval("mode")%></div>
                                </div>
                                <div class="detail-box">
                                    <div class="label">PAYMENT IN</div>
                                    <div class="value"><%#Eval("Paymentin")%></div>
                                </div>
                                <div class="detail-box">
                                    <div class="label">STATUS</div>
                                    <div class="value">Completed</div>
                                </div>
                            </div>
                            <div class="card-footer footer-blue" id="cardFooter" runat="server">
                                <div class="amount">
                                    <small>Total Amount</small>
                                    <h2>₹ <%#Eval("Amount")%></h2>
                                </div>

                                <a class="download-btn" href="#!" runat="server" id="printLnk">
                                    <span>⬇ Receipt</span>
                                </a>
                            </div>
                        </div>
                        <asp:Label ID="lbl_Slip_no" runat="server" Text='<%#Bind("Slip_no")%>' Visible="false"></asp:Label>
                        <asp:Label ID="lbl_Addmission_no" runat="server" Text='<%#Bind("Addmission_no")%>' Visible="false"></asp:Label>
                        <asp:Label ID="lbl_Class_id" runat="server" Text='<%#Bind("Class_id")%>' Visible="false"></asp:Label>
                        <asp:Label ID="lbl_session_id" runat="server" Text='<%#Bind("session_id")%>' Visible="false"></asp:Label>
                    </ItemTemplate>
                </asp:Repeater>
            </div>

            <div id="noDataSection" style="text-align: center; padding: 30px;" runat="server" visible="false">
                <img src="https://cdn-icons-png.flaticon.com/512/4076/4076478.png" width="120" />
                <h3>No Record Found</h3>
                <p>No payment transactions found.</p>
            </div>
        </div>
    </form>
</body>
</html>
