<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="school_web.Payroll.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../assets/plugins/vectormap/jquery-jvectormap-2.0.2.css" rel="stylesheet" />
    <link href="../assets/plugins/simplebar/css/simplebar.css" rel="stylesheet" />
    <link href="../assets/plugins/perfect-scrollbar/css/perfect-scrollbar.css" rel="stylesheet" />
    <link href="../assets/plugins/metismenu/css/metisMenu.min.css" rel="stylesheet" />
    <link href="../assets/plugins/datatable/css/dataTables.bootstrap5.min.css" rel="stylesheet" />
    <link href="../assets/plugins/datetimepicker/css/classic.css" rel="stylesheet" />
    <link href="../assets/plugins/datetimepicker/css/classic.time.css" rel="stylesheet" />
    <link href="../assets/plugins/datetimepicker/css/classic.date.css" rel="stylesheet" />
    <link rel="stylesheet" href="../assets/plugins/bootstrap-material-datetimepicker/css/bootstrap-material-datetimepicker.min.css" />
    <link rel="stylesheet" href="https://fonts.googleapis.com/icon?family=Material+Icons" />
    <!-- loader-->
    <link href="../assets/css/pace.min.css" rel="stylesheet" />
    <script src="../assets/js/pace.min.js"></script>
    <!-- Bootstrap CSS -->
    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css2?family=Roboto:wght@400;500&display=swap" rel="stylesheet" />
    <link href="../assets/css/app.css" rel="stylesheet" />
    <link href="../assets/css/icons.css" rel="stylesheet" />
    <!-- Theme Style CSS -->
    <link rel="stylesheet" href="../assets/css/dark-theme.css" />
    <link rel="stylesheet" href="../assets/css/semi-dark.css" />
    <link rel="stylesheet" href="../assets/css/header-colors.css" />
    <%--<script src="../assets/js/jquery.min.js"></script>--%>
    <%--<script src="../assets/js/jquery-1.10.2.min.js"></script>--%>
    <link href="../font-awesome-4.0.3/css/font-awesome.min.css" rel="stylesheet" />

    <script src="../assets/js/jquery.js"></script>
    <script src="../assets/js/bootstrap.min.js"></script>
    <%--<link href="../assets/css/bootstrap.min.css" rel="stylesheet" />
    <script src="../assets/js/jquery-1.10.2.min.js"></script>--%>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class='input-group date' id='datetimepicker1'>
                <asp:TextBox ID="txt_ship_times" runat="server" placeholder="Time" CssClass="form-control" Style="pointer-events: none !important;"></asp:TextBox>
                <span class="input-group-addon" style="padding: 6px 12px; font-size: 14px; font-weight: 400; color: #555; text-align: center; background-color: #eee; border: 1px solid #ccc; border-radius: 4px; line-height: 20px;">
                    <span class="glyphicon glyphicon-time"></span>
                </span>
            </div>
        </div>

        <link href="../timePicker/bootstrap-datetimepicker.css" rel="stylesheet" />
        <script src="../timePicker/moment-with-locales.js"></script>
        <script src="../timePicker/bootstrap-datetimepicker.js"></script>


        <script type="text/javascript">
            $(function () {
                $('#datetimepicker3').datetimepicker({
                    format: 'LT'
                });
            });
            $(function () {
                $('#datetimepicker1').datetimepicker({
                    format: 'LT'
                });
            });
    </script>


        <!--end switcher-->
        <!-- Bootstrap JS -->
        <script src="../assets/js/bootstrap.bundle.min.js"></script>
        <!--plugins-->

        <%-- <script src="../assets/js/jquery.min.js"></script>--%>
        <script src="../assets/plugins/simplebar/js/simplebar.min.js"></script>
        <script src="../assets/plugins/metismenu/js/metisMenu.min.js"></script>
        <script src="../assets/plugins/perfect-scrollbar/js/perfect-scrollbar.js"></script>
        <script src="../assets/plugins/vectormap/jquery-jvectormap-2.0.2.min.js"></script>
        <script src="../assets/plugins/vectormap/jquery-jvectormap-world-mill-en.js"></script>
        <script src="../assets/plugins/chartjs/js/Chart.min.js"></script>
        <script src="../assets/plugins/chartjs/js/Chart.extension.js"></script>
        <%--<script src="../assets/js/index.js"></script>--%>
        <script src="../assets/plugins/datatable/js/jquery.dataTables.min.js"></script>
        <script src="../assets/plugins/datatable/js/dataTables.bootstrap5.min.js"></script>
        <script src="../assets/plugins/datetimepicker/js/legacy.js"></script>
        <script src="../assets/plugins/datetimepicker/js/picker.js"></script>
        <script src="../assets/plugins/datetimepicker/js/picker.time.js"></script>
        <script src="../assets/plugins/datetimepicker/js/picker.date.js"></script>
        <script src="../assets/plugins/bootstrap-material-datetimepicker/js/moment.min.js"></script>
        <script src="../assets/plugins/bootstrap-material-datetimepicker/js/bootstrap-material-datetimepicker.min.js"></script>

        <link href="../Autocomplete/jquery-ui.css" rel="stylesheet" />
        <script src="../Autocomplete/jquery-ui.js"></script>
        <script>
            $('.datepicker').pickadate({
                selectMonths: true,
                selectYears: true,
                format: 'dd/mm/yyyy',
            }),
            $('.timepicker').pickatime()
        </script>
        <script>
            $(function () {
                $('#date-time').bootstrapMaterialDatePicker({
                    format: 'YYYY-MM-DD HH:mm'
                });
                $('#date').bootstrapMaterialDatePicker({
                    time: false
                });
                $('#time').bootstrapMaterialDatePicker({
                    date: false,
                    format: 'HH:mm'
                });
            });
        </script>
        <script>
            $(document).ready(function () {
                $('#example').DataTable();
            });
        </script>
        <script>
            $(document).ready(function () {
                var table = $('#example2').DataTable({
                    lengthChange: false,
                    buttons: ['copy', 'excel', 'pdf', 'print']
                });

                table.buttons().container()
                    .appendTo('#example2_wrapper .col-md-6:eq(0)');
            });
        </script>

        <!--app JS-->
        <script src="../assets/js/app.js"></script>

        
    </form>
</body>
</html>
