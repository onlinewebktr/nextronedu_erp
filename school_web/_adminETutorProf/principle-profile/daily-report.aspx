<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="daily-report.aspx.cs" Inherits="school_web._adminETutorProf.principle_profile.daily_report" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Academic & Collection</title>
    <script src="../../assets/js/jquery-1.10.2.min.js"></script>
    <script src="../../assets/plugins/chartjs/chart.js"></script>
    <script src="../../assets/plugins/chartjs/chartjs-plugin-datalabels.js"></script>

    <style>
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #eef2f7;
            margin: 0;
            padding: 20px;
        }

        .container {
            max-width: 100%;
            margin: auto;
            background: #ffffff;
            border-radius: 16px;
            box-shadow: 0 8px 24px rgba(0, 0, 0, 0.08);
            overflow: hidden;
        }

        .tab-buttons {
            display: flex;
            border-bottom: 1px solid #e0e0e0;
        }

            .tab-buttons button {
                flex: 1;
                padding: 14px 0;
                font-size: 16px;
                font-weight: 600;
                border: none;
                background: none;
                cursor: pointer;
                color: #555;
                border-bottom: 3px solid transparent;
                transition: all 0.25s ease;
            }

                .tab-buttons button:hover {
                    background-color: #f9f9f9;
                }

                .tab-buttons button.active {
                    color: #0d6efd;
                    border-bottom-color: #0d6efd;
                    background-color: #f0f8ff;
                }

        .tab-content {
            padding: 18px;
            opacity: 1;
            transform: translateY(0);
            transition: opacity 0.4s ease, transform 0.4s ease;
        }

            .tab-content.hidden {
                display: block;
                opacity: 0;
                transform: translateY(10px);
                pointer-events: none;
                height: 0;
                padding: 0 18px;
                overflow: hidden;
            }

        h2 {
            color: #0d6efd;
            margin-top: 0;
            font-size: 20px;
        }

        .stats-grid {
            display: grid;
            grid-template-columns: 1fr 1fr;
            gap: 12px;
            margin-bottom: 18px;
        }

        .stat-card {
            background-color: #f9fbfc;
            border-radius: 12px;
            padding: 14px 10px;
            text-align: center;
            color: white;
            box-shadow: 0 2px 6px rgba(0, 0, 0, 0.1);
            transform: translateY(0);
            transition: transform 0.3s ease, box-shadow 0.3s ease;
        }

            .stat-card:hover {
                transform: translateY(-4px);
                box-shadow: 0 6px 16px rgba(0, 0, 0, 0.15);
            }

            .stat-card h3 {
                margin: 0;
                font-size: 14px;
                color: white;
            }

            .stat-card p {
                margin: 6px 0 0;
                font-size: 20px;
                font-weight: bold;
            }

        /* Custom card colors */
        .enquiry {
            background: linear-gradient(135deg, #36d1dc, #5b86e5);
        }

        .prospectus {
            background: linear-gradient(135deg, #f7971e, #ffd200);
        }

        .registration {
            background: linear-gradient(135deg, #8e2de2, #4a00e0);
        }

        .admission {
            background: linear-gradient(135deg, #43cea2, #185a9d);
        }

        .tc {
            background: linear-gradient(135deg, #ff416c, #ff4b2b);
        }

        .inactive {
            background: linear-gradient(135deg, #862828, #d724b0);
        }

        canvas {
            width: 100% !important;
            height: auto !important;
        }




        .popup {
            display: none;
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background: rgba(0,0,0,0.5);
            z-index: 1000;
        }

            .popup:target {
                display: block;
            }

        .popup-content {
            position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            background: #ffffff;
            padding: 24px;
            border-radius: 12px;
            max-width: 90%;
            width: 80%;
            box-shadow: 0 8px 24px rgba(0,0,0,0.2);
            text-align: center;
        }

            .popup-content h2 {
                margin-top: 0;
                color: #0d6efd;
            }

        .close-popup {
            display: inline-block;
            margin-top: 16px;
            padding: 8px 16px;
            background: #0d6efd;
            color: white;
            text-decoration: none;
            border-radius: 6px;
        }

        .tablewrps {
            margin: 0px;
            padding: 0px;
            width: 100%;
            float: left;
        }

            .tablewrps table {
                width: 100%;
            }

            .tablewrps tr th {
                margin: 0px;
                padding: 5px 5px 6px 5px;
                border: 1px solid #776eff;
                font-size: 14px;
            }

            .tablewrps tr td {
                margin: 0px;
                padding: 5px 5px 6px 5px;
                border: 1px solid #776eff;
                font-size: 14px;
            }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <!-- Tab Navigation -->
            <div class="tab-buttons">
                <button type="button" id="tab-academic" class="active">Academic</button>
                <button type="button" id="tab-collection">Collection</button>
            </div>

            <!-- Academic Tab Content -->
            <div id="content-academic" class="tab-content">
                <%--<h2>Academic Overview</h2>--%>
                <div class="stats-grid">
                    <div class="stat-card enquiry">
                        <h3>Enquiry</h3>
                        <p>
                            <asp:Label ID="lbl_enquiry" runat="server"></asp:Label>
                        </p>
                    </div>

                    <a href="#popup-prospectus" style="text-decoration: none;">
                        <div class="stat-card prospectus">
                            <h3>Prospectus Sale</h3>
                            <p>
                                <asp:Label ID="lbl_prospectus" runat="server"></asp:Label>
                            </p>
                        </div>
                    </a>

                    <a href="#popup-admission" style="text-decoration: none;">
                        <div class="stat-card admission">
                            <h3>Admission</h3>
                            <p>
                                <asp:Label ID="lbl_admission" runat="server"></asp:Label>
                            </p>
                        </div>
                    </a>
                    <div class="stat-card tc">
                        <h3>TC</h3>
                        <p>
                            <asp:Label ID="lbl_tc" runat="server"></asp:Label>
                        </p>
                    </div>
                    <div class="stat-card inactive">
                        <h3>Inactive Student</h3>
                        <p>
                            <asp:Label ID="lbl_inactive" runat="server"></asp:Label>
                        </p>
                    </div>
                    <div class="stat-card registration">
                        <h3>Receipt</h3>
                        <p>
                            <asp:Label ID="lbl_receipt" runat="server"></asp:Label>
                        </p>
                    </div>
                </div>

                <!-- Graph -->
                <canvas id="myDoughnutChart1" width="400" height="400"></canvas>
                <%--<canvas id="myBarChart" width="400" height="110"></canvas>--%>
            </div>

            <!-- Collection Tab Content -->
            <div id="content-collection" class="tab-content hidden">
                <%--<h2>Collection Overview</h2>--%>

                <div class="stats-grid">
                    <div class="stat-card clean prospectus">
                        <div class="icon"><i class="fas fa-receipt"></i></div>
                        <div class="details">
                            <p class="count">₹<asp:Label ID="lbl_prospectus_fee" runat="server"></asp:Label></p>
                            <p class="label">Prospectus Fee</p>
                        </div>
                    </div>
                    <div class="stat-card clean admission">
                        <div class="icon"><i class="fas fa-university"></i></div>
                        <div class="details">
                            <p class="count">₹<asp:Label ID="lbl_school_fee" runat="server"></asp:Label></p>
                            <p class="label">School Fee</p>
                        </div>
                    </div>
                </div>

                <!-- Collection Graph -->
                <canvas id="myDoughnutChart" width="400" height="400"></canvas>
            </div>
        </div>

        <!-- Popup Modal -->
        <div id="popup-prospectus" class="popup">
            <div class="popup-content">
                <h2>Prospectus Sale Details</h2>
                <div class="tablewrps" id="formSaleList" runat="server">
                    <table>
                        <tr>
                            <th>#</th>
                            <th>Class</th>
                            <th>Student Name</th>
                        </tr>
                        <asp:Repeater ID="rd_view" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label></td>
                                    <td><%#Eval("class")%></td>
                                    <td><%#Eval("student_name")%></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
                <p id="noSale" runat="server">There have been no prospectus sales today.</p>
                <a href="#" class="close-popup">Close</a>
            </div>
        </div>


        <!-- Popup Modal -->
        <div id="popup-admission" class="popup">
            <div class="popup-content">
                <h2>New Admission Details</h2>
                <div class="tablewrps" id="newAdmissionList" runat="server">
                    <table>
                        <tr>
                            <th>#</th>
                            <th>Class</th>
                            <th>Student Name</th>
                        </tr>
                        <asp:Repeater ID="rpAdmssion" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label></td>
                                    <td><%#Eval("class")%></td>
                                    <td><%#Eval("studentname")%></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
                <p id="noAdm" runat="server">There have been no admissions today.</p>
                <a href="#" class="close-popup">Close</a>
            </div>
        </div>

        <script>
            // Tab Switching
            const tabAcademic = document.getElementById('tab-academic');
            const tabCollection = document.getElementById('tab-collection');
            const contentAcademic = document.getElementById('content-academic');
            const contentCollection = document.getElementById('content-collection');

            function switchTab(activeTab, inactiveTab, showContent, hideContent) {
                activeTab.classList.add('active');
                inactiveTab.classList.remove('active');

                hideContent.classList.add('hidden');
                setTimeout(() => {
                    showContent.classList.remove('hidden');
                }, 50);
            }

            tabAcademic.addEventListener('click', () => {
                switchTab(tabAcademic, tabCollection, contentAcademic, contentCollection);
            });

            tabCollection.addEventListener('click', () => {
                switchTab(tabCollection, tabAcademic, contentCollection, contentAcademic);
            });
        </script>



        <script type="text/javascript">

            $(document).ready(function () {
                var session_id = "1";
                //$(function () {
                //    var type = "MonthlyFee";
                //    $.ajax({
                //        type: "POST",
                //        contentType: "application/json; charset=utf-8",
                //        url: "WebService1.asmx/find_daily_report",
                //        data: '{"Session_id":"' + session_id + '"}',
                //        dataType: "json",
                //        success: function (response) {
                //            var chartData = JSON.parse(response.d);
                //            load_chart(chartData)
                //        },
                //    });
                //})



                //==================
                $(function () {
                    var type = "Modewise Payment";
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "WebService1.asmx/find_academics",
                        data: '{"Session":"' + session_id + '"}',
                        dataType: "json",
                        success: function (response) {
                            var chartData = JSON.parse(response.d);
                            load_chart11122(chartData)
                        },
                    });
                })
                //==================
                $(function () {
                    var type = "Modewise Payment";
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "WebService1.asmx/find_modewise_amount",
                        data: '{"Session":"' + session_id + '"}',
                        dataType: "json",
                        success: function (response) {
                            var chartData = JSON.parse(response.d);
                            load_chart111(chartData)
                        },
                    });
                })
            });

            function load_chart(data) {
                const options = {
                    scales: {
                        y: {
                            beginAtZero: true
                        }
                    },
                    plugins: {
                        datalabels: {
                            anchor: 'end',
                            align: 'end',
                            formatter: (value) => value,
                            color: 'black',
                            font: {
                                weight: 'bold',
                                size: 12
                            }
                        }
                    }
                };

                const ctx1 = document.getElementById('myBarChart').getContext('2d');
                const myBarChart = new Chart(ctx1, {
                    type: 'bar',
                    data: data,
                    options: options,
                    plugins: [ChartDataLabels] // Register the data labels plugin
                });
            }



            function load_chart111(data) {
                const config = {
                    options: {
                        responsive: true,
                        plugins: {
                            legend: {
                                position: 'top',
                            },
                            title: {
                                display: true,
                                text: 'Doughnut Chart with Values',
                            },
                            datalabels: {
                                color: '#fff',
                                formatter: (value, context) => {
                                    return value; // Display the value
                                },
                            },
                        },
                    },
                    plugins: [ChartDataLabels], // Register the datalabels plugin
                };

                const ctx = document.getElementById('myDoughnutChart').getContext('2d');
                const myDoughnutChart = new Chart(ctx, {
                    type: 'doughnut',
                    data: data,
                    config: config,
                    plugins: [ChartDataLabels] // Register the data labels plugin
                });
            }



            function load_chart11122(data) {
                const config = {
                    options: {
                        responsive: true,
                        plugins: {
                            legend: {
                                position: 'top',
                            },
                            title: {
                                display: true,
                                text: 'Doughnut Chart with Values',
                            },
                            datalabels: {
                                color: '#fff',
                                formatter: (value, context) => {
                                    return value; // Display the value
                                },
                            },
                        },
                    },
                    plugins: [ChartDataLabels], // Register the datalabels plugin
                };

                const ctx = document.getElementById('myDoughnutChart1').getContext('2d');
                const myDoughnutChart = new Chart(ctx, {
                    type: 'doughnut',
                    data: data,
                    config: config,
                    plugins: [ChartDataLabels] // Register the data labels plugin
                });
            }
        </script>
    </form>
</body>
</html>
