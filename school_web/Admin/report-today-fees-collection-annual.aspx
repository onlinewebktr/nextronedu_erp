<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="report-today-fees-collection-annual.aspx.cs" Inherits="school_web.Admin.report_today_fees_collection_annual" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Today Annual Fees Collection
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .table-responsive {
            overflow-x: inherit;
        }
    </style>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="https://fonts.googleapis.com/css2?family=Dosis:wght@200;300;400;500;600;700;800&family=Montserrat:ital,wght@0,300;0,400;0,500;0,600;0,700;0,800;0,900;1,200;1,300;1,400;1,500;1,600;1,700;1,800;1,900&family=Roboto:ital,wght@0,100;0,300;0,400;0,500;0,700;0,900;1,100;1,300;1,400;1,500;1,700;1,900&display=swap" rel="stylesheet"/><link href="../assets/css/Print.css" rel="stylesheet" type="text/css" />');
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
                <div class="breadcrumb-title pe-3"><a href="fee-report.aspx" class="backlnk-css"><i class="bx bx-arrow-back"></i></a>Report</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Today Annual Fee Collection</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase"></h6>
                    <ul class="sub-pag-menu-ul">
                        <li><a href="report-today-fees-collection-annual.aspx" class="sub-mnu-p-a-active">Today Fee Collection Summary</a></li>
                        <li><a href="annual-fee-collection-report.aspx">Today Fees Collection</a></li>
                        <li><a href="report-headwise-fee-collection-annual.aspx">Head wise Fee Collection</a></li>
                        <li><a href="report-student-wise-annual-fee-collection.aspx">Accumulated Student Wise Fee Collection</a></li>
                        <li><a href="report-student-headwise-annual-fee-collection_N.aspx">Student & Head wise Fee Collection</a></li>
                    </ul>
                </div>
                <div class="col-xl-12">
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="find-dv">
                                                <div class="row">
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">From Date</label>
                                                        <asp:TextBox ID="txt_s_date" runat="server" class="form-control find-dv-txtbx datepicker"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">To Date</label>
                                                        <asp:TextBox ID="txt_e_date" runat="server" class="form-control find-dv-txtbx datepicker"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1">
                                                        <asp:Button ID="btn_find" runat="server" Text="find" class="btn btn-primary find-dv-btn" OnClick="btn_find_Click" />
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <asp:LinkButton ID="btn_excels" runat="server" OnClick="btn_excels_Click" Style="margin-left: 10px;" class="btn btn-primary find-dv-btn">  <i class='bx bx-download'></i> Excel</asp:LinkButton>
                                                        <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" CssClass="btn btn-primary find-dv-btn" Style="margin-left: 10px;" runat="server"
                                                            ToolTip="Print"><i class='bx bx-printer'></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="grd-wpr">
                                                <div class="col-sm-12">
                                                    <div id="tblPrintIQ" runat="server">
                                                        <div class="prnt-dv-wpr printborder">


                                                            <div class="head-printdv" style="border-bottom: 1px solid #000; margin: 0px; float: left; width: 100%;">

                                                                <div style="margin: 0px; padding: 0px; height: 110px; width: 20%; float: left;">
                                                                    <asp:Image ID="imglogo" runat="server" Style="height: 100px; width: 100px; margin: 0px 0px 0px 10px;" />
                                                                </div>
                                                                <div style="margin: 0px; padding: 0px; height: 110px; width: 80%; float: left;">
                                                                    <h1 style="margin: 10px 0px 0px 0px; width: 100%; padding: 0px; font-weight: bold; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 20px; text-decoration: underline;">
                                                                        <asp:Label ID="lbl_heading" runat="server"></asp:Label>


                                                                    </h1>

                                                                    <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                        <asp:Label ID="lbl_address" runat="server"></asp:Label>


                                                                    </div>
                                                                    <div style="display: none; margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                        Email Id. :<asp:Label ID="lbl_emaiid" runat="server"></asp:Label>

                                                                        &nbsp;&nbsp;

                            website :
                            <asp:Label ID="lbl_website" runat="server"></asp:Label>
                                                                    </div>
                                                                    <div style="display: none; margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                        Contact Details :<asp:Label ID="lbl_contact_details" runat="server"></asp:Label>
                                                                    </div>
                                                                    <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                        <span style="font-size: 14px; font-weight: bold;">Time Period-<asp:Label ID="lbl_class22" runat="server"></asp:Label></span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <asp:Panel ID="pnl_grid" runat="server" Style="width: 100%;">
                                                                <asp:GridView ID="GrdView" runat="server" class="table table-bordered" OnRowDataBound="GrdView_RowDataBound" ShowFooter="true" AutoGenerateColumns="False" Width="100%">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Sl No.">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Particular">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_particular" runat="server" Text='<%#Bind("mode")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lbltotalamts" runat="server" Style="font-weight: 600" Text="Total Amount"></asp:Label>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Amount">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbl_amount" runat="server" Text='<%#Bind("Paid_amt")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lbltotal" runat="server" Style="font-weight: 600"></asp:Label>
                                                                            </FooterTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>

                                                            </asp:Panel>



                                                            <div class="col-12 col-lg-12">
                                                                <div id="paid_summary" style="width: 115%; height: 420px; -webkit-tap-highlight-color: transparent; user-select: none; position: relative; padding: 0px; margin: 0px 0px 0px -35px;">
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
                </div>
            </div>
        </div>
        <!--end row-->
    </div>

    <asp:HiddenField ID="hd_from_date" runat="server" />
    <asp:HiddenField ID="hd_to_date" runat="server" />
    <script src="../Echart/echarts.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            var FroMdatE = $('#<%= hd_from_date.ClientID %>').val();
            var ToDatE = $('#<%= hd_to_date.ClientID %>').val();
            var Type = "Annual";



            //=================Order SummarY
            var myChart6 = echarts.init(document.getElementById('paid_summary'));
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
                    nameGap: 5,
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

                    name: 'Payment Mode',
                    type: 'bar',
                    data: [],
                    label: {
                        normal: {
                            show: true,
                            position: 'inside'
                        }
                    }
                }]
            });


            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "graph.asmx/find_paid_summary_report",
                data: '{"FromDate":"' + FroMdatE + '", "ToDate":"' + ToDatE + '", "TYPE":"' + Type + '"}',
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
                            data: ['Payment Mode']
                        },
                        xAxis: {

                            data: JSONObject["xaxis"]
                        },
                        series: [{
                            // find series by name
                            name: 'Payment Mode',
                            type: 'bar',
                            data: JSONObject["yaxis"],
                            color: ['#4A9529'],
                            label: {
                                normal: {
                                    show: true,
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
