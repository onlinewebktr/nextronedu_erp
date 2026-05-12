<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Day_End_Report_Summary.aspx.cs" Inherits="school_web.Admin.Day_End_Report_Summary" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Day End Report Summery
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .modal-dialog {
            max-width: 800px;
        }

        .modal {
            background: rgb(0 0 0 / 31%);
        }

        .table-responsive {
            overflow-x: inherit;
        }


        .card {
            position: relative;
            display: flex;
            flex-direction: column;
            min-width: 0;
            word-wrap: break-word;
            background-color: #fff;
            background-clip: border-box;
            border: 0 solid transparent;
            border-radius: 0.25rem;
            margin-bottom: 1.5rem;
            box-shadow: 0 0px 0px 0 rgb(218 218 253 / 65%), 0 0px 0px 0 rgb(206 206 238 / 54%);
        }

        .td2 {
            text-align: right;
            padding: 1px 5px 3px 0px;
            color: #000;
        }

        .td3 {
            text-align: left;
            padding: 3px 0px 3px 5px;
            color: #000;
        }

        .bodrerf1 th {
            border: 1px solid #000;
            color: #000;
            padding: 1px 5px 2px 3px;
            text-align: left;
        }

        .bodrerf1 td {
            border: 1px solid #000;
            color: #000;
        }

        .home-grph-wpr {
            margin: 0px 0px 5px 0px;
            background: #fff;
            padding: 15px 0px 15px 20px;
        }
    </style>
    <script>
        $(function () {
            $("#<%=txt_date1.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                readOnly: true,
                yearRange: "1900:2100",
            }).attr("readonly", "true");
        });
    </script>

    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="../assets/css/Print.css" rel="stylesheet" type="text/css" /><link href="https://fonts.googleapis.com/css2?family=Dosis:wght@200;300;400;500;600;700;800&display=swap" rel="stylesheet"/>');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body ></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
        jQuery(".sn-bill-head-text").fitText(0.38);
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-wrapper">
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



            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                <div class="breadcrumb-title pe-3"><a href="daily-closing-report.aspx" class="backlnk-css"><i class="bx bx-arrow-back"></i></a>Report</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Day End Report Summery</li>
                        </ol>
                    </nav>
                </div>
            </div>

            <div class="row">
                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase"></h6>
                    <hr />


                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="find-dv">
                                                <div class="row">
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Date</label>
                                                        <div class="clndr-div">
                                                            <asp:TextBox ID="txt_date1" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                            <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                                        </div>
                                                    </div>


                                                    <div class="col-sm-1">
                                                        <asp:Button ID="btn_find" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_find_Click" />
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" runat="server" ToolTip="Print" class="btn btn-primary find-dv-btn" Text=""><i class="bx bx-printer" style="padding:0px 0px 0px 0px"></i>  Print</asp:LinkButton>
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




            <div class="row row-cols-1 row-cols-md-2 row-cols-xl-4">

                <div style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%;" id="tblPrintIQ" runat="server">
                    <asp:Label ID="lbl_print_headS" runat="server" class="pntsHeadS"></asp:Label>
                    <asp:Label ID="lbl_print_dateS" runat="server" class="pntsDatesS"></asp:Label>


                    <div class="home-grph-wpr">
                        <div class="row">
                            <div class="col-xl-12">
                                <div id="order_summary" style="width: 110%; height: 420px; -webkit-tap-highlight-color: transparent; user-select: none; position: relative; padding: 0px; margin: 0px 0px 0px -35px;">
                                </div>
                            </div>
                        </div>
                    </div>


                    <table style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%; background-color: white; border: 1px solid #000; color: black;" border="1">
                        <tr>
                            <td style="padding: 5px;">Total Admission Fee:
                                 
                                 <asp:Label ID="lbl_totaladmissionfee1" runat="server" Font-Bold="true">0.00</asp:Label>

                            </td>
                            <td style="padding: 5px;">Total Annual Fee:
                                <asp:Label ID="lbl_readmissionfeecolection1" runat="server" Font-Bold="true">0.00</asp:Label>
                            </td>

                            <td style="padding: 5px;">Total Monthley Fee:
                                <asp:Label ID="lbl_monthley" runat="server" Font-Bold="true">0.00</asp:Label>
                            </td>
                        </tr>
                    </table>

                    <table style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%; background-color: white; border: 1px solid #000; color: black;" border="1">
                        <tr>
                            <td style="padding: 5px; border-bottom: 1px solid #000">Payment Mode Collection 



                            </td>

                        </tr>
                        <tr>

                            <td style="padding: 5px;">
                                <asp:GridView ID="grid_payment_mode" runat="server" CssClass="bodrerf1" border="1" AutoGenerateColumns="False" Width="100%" Style="text-align: center;" GridLines="Vertical" OnRowDataBound="grid_payment_mode_RowDataBound" ShowFooter="True">
                                    <Columns>

                                        <asp:TemplateField HeaderText="Sl. No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_sln" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="td3" Width="70px" />
                                            <HeaderStyle CssClass="td3" Width="70px" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Payment Mode">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_mode" runat="server" Text='<%#Bind("mode") %>'></asp:Label>

                                            </ItemTemplate>
                                            <ItemStyle CssClass="td3" />
                                        </asp:TemplateField>





                                        <asp:TemplateField HeaderText="Total Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Amount" runat="server" Text='<%# Getamount_comma_seperated(Eval("amount1").ToString())%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="td2" Width="100px" />
                                            <HeaderStyle CssClass="td2" />
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_totalamount" runat="server" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle CssClass="td2" />
                                        </asp:TemplateField>

                                    </Columns>

                                </asp:GridView>

                            </td>

                        </tr>


                    </table>
                    <table style="margin: 10px 0px 0px 0px; padding: 0px; float: left; height: auto; width: 100%; background-color: white; border: 1px solid #000; color: black;" border="0">
                        <tr>
                            <td style="padding: 5px; border-bottom: 1px solid #000">Head wise Total Collection 



                            </td>

                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="grd_fee" runat="server" CssClass="bodrerf1" border="1" AutoGenerateColumns="False" Width="100%" Style="text-align: center;" GridLines="Vertical" OnRowDataBound="grd_fee_RowDataBound" ShowFooter="True">
                                    <Columns>

                                        <asp:TemplateField HeaderText="Sl. No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_sln" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="td3" Width="70px" />
                                            <HeaderStyle CssClass="td3" Width="70px" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Head Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_headname" runat="server" Text='<%#Bind("Content") %>'></asp:Label>

                                            </ItemTemplate>
                                            <ItemStyle CssClass="td3" />
                                        </asp:TemplateField>





                                        <asp:TemplateField HeaderText="Total Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Amount" runat="server" Text='<%# Getamount_comma_seperated(Eval("amount1").ToString())%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="td2" Width="100px" />
                                            <HeaderStyle CssClass="td2" />
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_totalamount" runat="server" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle CssClass="td2" />
                                        </asp:TemplateField>

                                    </Columns>

                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <!--end row-->
    </div>


    <asp:HiddenField ID="hd_session" runat="server" />
    <asp:HiddenField ID="hd_date" runat="server" />

    <script src="../Echart/echarts.min.js"></script>



    <script type="text/javascript">
        //==============================Order Status SummarY 
        $(document).ready(function () { 
            var Day_date = $('#<%= hd_date.ClientID %>').val();


            //=================Order SummarY
            var myChart6 = echarts.init(document.getElementById('order_summary'));
            myChart6.setOption({
                title: {
                    //text: 'Date Wise LAB Patient',
                    textAlign: 'Right',
                    textStyle: { fontSize: 13 }
                },
                tooltip: {},
                legend: {
                },
                xAxis: {
                    data: [],
                    type: 'category',
                    nameRotate: 'xAxis',
                    axisTick: { show: false },
                    nameGap: 1,
                    min: 'dataMin',
                    interval: 0,


                    axisLabel: {
                        interval: 0,
                        inside: false,
                        rotate: 25,
                        showMaxLabel: true,
                        verticalAlign: "top",
                        lineHeight: 5,
                        fontSize: 10,
                        extraCssText: "width:100px; white-space:pre-wrap;"
                    },
                },
                yAxis: {},
                series: [{
                    name: 'Fee Summary',
                    type: 'bar',
                    data: [],
                    label: {
                        normal: {
                            show: true,
                            position: 'top'
                        }
                    }
                }]
            });

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "graph.asmx/find_fee_day_end_summary",
                data: "{Day_date: '" + Day_date + "'}",
                dataType: "json",
                success: function (response) {
                    var JSONObject = JSON.parse(response.d);
                    //alert(response.d);
                    myChart6.setOption({
                        tooltip: {
                            trigger: 'axis',
                            axisPointer: {
                                type: 'shadow'
                            }
                        },
                        legend: {
                            selectedMode: 'multiple',
                            data: ['Fee']
                        },
                        xAxis: {

                            data: JSONObject["xaxis"]
                        },
                        series: [{
                            // find series by name
                            name: 'Amount',
                            type: 'bar',
                            data: JSONObject["yaxis"],
                            color: ['#91CC75'],
                            bar: { groupWidth: '100%' },
                            label: {
                                normal: {
                                    show: false,
                                    position: 'inside'
                                }
                            }
                        }
                        ]
                    });
                },
            }); 
        }); 
    </script>
</asp:Content>
