<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="syllabus.aspx.cs" Inherits="school_web.Student_Profile.webview.syllabus" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Syllabus Downloads</title>
    <script src="https://unpkg.com/lucide@latest"></script>
    <style>
        body {
            margin: 0;
            padding: 1rem;
            font-family: 'Segoe UI', sans-serif;
            background: linear-gradient(to bottom, #e0f2ff, #ede9fe);
        }

        .container {
            max-width: 500px;
            margin: 0 auto;
        }

        h1 {
            text-align: center;
            color: #4338ca;
            font-size: 1.8rem;
            display: flex;
            justify-content: center;
            align-items: center;
            gap: 8px;
            margin-bottom: 2rem;
            animation: fadeInUp 0.6s ease-out forwards;
        }

        .card {
            background: white;
            border-radius: 16px;
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
            padding: 1rem;
            margin-bottom: 1.5rem;
            animation: fadeInUp 0.6s ease-out forwards;
        }

        .card-content {
            display: flex;
            align-items: center;
            gap: 1rem;
            margin-bottom: 0.8rem;
        }

        .icon-circle {
            background-color: #e0e7ff;
            padding: 10px;
            border-radius: 50%;
            display: flex;
            align-items: center;
            justify-content: center;
        }

        .subject h2 {
            margin: 0;
            font-size: 1rem;
            color: #1f2937;
        }

        .subject p {
            margin: 0;
            font-size: 0.85rem;
            color: #6b7280;
        }

        .download-btn {
            display: inline-flex;
            align-items: center;
            gap: 6px;
            font-size: 0.85rem;
            padding: 6px 12px 8px;
            border-radius: 8px;
            color: white;
            background-color: #4f46e5;
            text-decoration: none;
            transition: transform 0.2s ease, box-shadow 0.2s ease;
        }

            .download-btn:hover {
                transform: scale(1.05);
                box-shadow: 0 3px 8px rgba(0, 0, 0, 0.2);
            }

                .download-btn:hover svg {
                    transform: rotate(360deg);
                    transition: transform 0.6s ease;
                }

        @keyframes fadeInUp {
            from {
                opacity: 0;
                transform: translateY(20px);
            }

            to {
                opacity: 1;
                transform: translateY(0);
            }
        }

        /* Responsive text sizing */
        @media (max-width: 480px) {
            h1 {
                font-size: 1.5rem;
            }

            .subject h2 {
                font-size: 0.95rem;
            }

            .download-btn {
                font-size: 0.75rem;
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
                <asp:Repeater ID="rp_syllabus" runat="server">
                    <ItemTemplate>
                        <div class="card">
                            <div class="card-content">
                                <div class="icon-circle">
                                    <i data-lucide="atom" class="icon" style="width: 20px; height: 20px; color: #4f46e5;"></i>
                                    <%--<i data-lucide="calculator" class="icon" style="width: 20px; height: 20px; color: #4f46e5;"></i>--%>
                                </div>
                                <div class="subject">
                                    <h2><%#Eval("Syllabus_info") %></h2>
                                    <p>PDF Format</p>
                                </div>
                            </div>
                            <a style="margin-left: 58px" href="<%#Eval("Syllabus_filepath") %>" download class="download-btn">
                                <i data-lucide="download" class="icon" style="width: 16px; height: 16px;"></i>Download
                            </a>
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
                    <p>Sorry, we couldn’t find any syllabus available at the moment.</p>
                </div>
            </asp:Panel>
            <!-- Card 2 -->
            <%--<div class="card">
                <div class="card-content">
                    <div class="icon-circle" style="background-color: #d1fae5;">
                        <i data-lucide="atom" class="icon" style="width: 20px; height: 20px; color: #10b981;"></i>
                    </div>
                    <div class="subject">
                        <h2>Science - Grade 10</h2>
                        <p>PDF Format</p>
                    </div>
                </div>
                <a style="margin-left: 58px" href="syllabus/science-grade10.pdf" download class="download-btn" style="background-color: #10b981;">
                    <i data-lucide="download" class="icon" style="width: 16px; height: 16px;"></i>Download
                </a>
            </div>--%>
            <!-- Add more cards as needed -->
        </div>

        <script>
            lucide.createIcons();
        </script>
    </form>
</body>
</html>
