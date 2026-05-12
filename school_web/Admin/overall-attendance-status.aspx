<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="overall-attendance-status.aspx.cs" Inherits="school_web.Admin.overall_attendance_status" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Attendance Status
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="../assets/dropdownmultiselection/style.css" rel="stylesheet" />
    <script src="../assets/dropdownmultiselection/bootstrap-multiselect.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#<%=ddl_classs.ClientID%>").multiselect({
                includeSelectAllOption: true
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--start page wrapper -->
    <div class="page-wrapper" data-ng-app="RpFinanceApp" data-ng-controller="RpFinanceAppCtrl">
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

            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <div class="card">
                        <div class="card-body">
                            <div class="find-dv">
                                <div class="row g-3 needs-validation">
                                    <div class="col-sm-2">
                                        <label for="validationCustom01" class="find-dv-lbl">Class</label>
                                        <asp:ListBox ID="ddl_classs" runat="server" CssClass="form-select" SelectionMode="Multiple"></asp:ListBox>
                                    </div>
                                    <div class="col-sm-2">
                                        <label for="validationCustom01" class="find-dv-lbl">Date</label>
                                        <asp:TextBox ID="txt_find_date" runat="server" class="form-control find-dv-txtbx datepicker txtbx-ddl-style"></asp:TextBox>
                                    </div>

                                    <div class="col-sm-6">
                                        <asp:Button ID="btn_find" OnClick="btn_find_Click" class="btn btn-primary find-dv-btn" runat="server" Text="Find" />
                                        <%--<div id="excel" runat="server" visible="false">
                                            <a href="javascript:" class="btn btn-primary find-dv-btn" id="excelbtnS" data-ng-click="Export()"><i class='bx bx-download'></i>Excel</a>
                                        </div>

                                        <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" class="btn btn-primary find-dv-btn" Style="margin-left: 10px;" runat="server"
                                            ToolTip="Print">
                                                <i class='bx bx-printer'></i>Print</asp:LinkButton>--%>
                                    </div>
                                </div>
                            </div>

                            <div class="p-4 border rounded" style="float: left; width: 100%">
                                <%--<div class="f-dashbrd-sec">
                                    <div class="row g-3 needs-validation" novalidate="">
                                        <div class="col-md-3">
                                            <div class="f-dashbrd-bx">
                                                <p class="f-dashbrd-bx-txt1">Today's Collection</p>
                                                <h2 class="f-dashbrd-bx-txt2" id="todyCollc" runat="server"></h2>
                                                <p class="f-dashbrd-bx-txt3" id="todyCollcnobill" runat="server"></p>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="f-dashbrd-bx" style="background: linear-gradient(to bottom right, #63d457 0, #3cbf2d 100%);">
                                                <p class="f-dashbrd-bx-txt1">Current month till date</p>
                                                <h2 class="f-dashbrd-bx-txt2" id="collcofmonth" runat="server"></h2>
                                                <p class="f-dashbrd-bx-txt3" id="monthCollcnobill" runat="server"></p>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="f-dashbrd-bx" style="background: linear-gradient(to bottom right, #ff9e27 0, #d47d13 100%);">
                                                <p class="f-dashbrd-bx-txt1">Today's Discount</p>
                                                <h2 class="f-dashbrd-bx-txt2" runat="server" id="todaysDisc">₹0</h2>
                                                <p class="f-dashbrd-bx-txt3" id="ttlbildisc" runat="server"></p>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="f-dashbrd-bx" style="background: linear-gradient(to bottom right, #6cc8ff 0, #008d72 100%);">
                                                <p class="f-dashbrd-bx-txt1">Today's Online Collection</p>
                                                <h2 class="f-dashbrd-bx-txt2" id="appcollc" runat="server"></h2>
                                                <p class="f-dashbrd-bx-txt3" id="AppCollcnobill" runat="server"></p>
                                            </div>
                                        </div>
                                    </div>
                                </div>--%> 
                                <div class="f-dashbrd-fee-tkn-his">
                                    <div class="row g-3 needs-validation" novalidate="">
                                        <div class="col-md-12">
                                            <div class="f-dashbrd-fee-tkn-his-inr" style="margin: 0px 0px 0px 0px;">
                                                <div class="f-dashbrd-fee-tkn-his-inr-h">Attendance Analysis Chart</div>
                                                <div class="f-dashbrd-fee-tkn-his-inr-wpr">
                                                    <canvas id="myBarChart" width="400" height="130"></canvas>
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
        </div>
    </div>

    <asp:HiddenField ID="hd_session_id" runat="server" />
    <asp:HiddenField ID="hd_class_id" runat="server" />
    <asp:HiddenField ID="hd_idate" runat="server" />
    <script>
        $(document).ready(function () {
            var session_id = $('#<%= hd_session_id.ClientID %>').val();
            var class_ids = $('#<%= hd_class_id.ClientID %>').val();
            var findIdate = $('#<%= hd_idate.ClientID %>').val();
            $(function () {
                var type = "MonthlyFee";
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "webServices/attendance.asmx/find_attendance_status_report",
                    data: '{"Session_id":"' + session_id + '","Class_ids":"' + class_ids + '","FindIdate":"' + findIdate + '"}',
                    dataType: "json",
                    success: function (response) {
                        var chartData = JSON.parse(response.d);
                        load_chart(chartData)
                    },
                });
            })



            //==================
            $(function () {
                var type = "Modewise Payment";
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "webServices/finance.asmx/find_modewise_amount",
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
    </script>
</asp:Content>
