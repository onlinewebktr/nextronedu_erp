<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Hostel_Summary.aspx.cs" Inherits="school_web.Admin.Hostel_Summary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Hostel Summary
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
     <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="https://fonts.googleapis.com/css2?family=Dosis:wght@200;300;400;500;600;700;800&family=Montserrat:ital,wght@0,300;0,400;0,500;0,600;0,700;0,800;0,900;1,200;1,300;1,400;1,500;1,600;1,700;1,800;1,900&family=Roboto:ital,wght@0,100;0,300;0,400;0,500;0,700;0,900;1,100;1,300;1,400;1,500;1,700;1,900&display=swap" rel="stylesheet"/><link href="../assets/css/Print.css" rel="stylesheet" type="text/css" /><link href="../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />');
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
    <style>
        /*====================================*/
        .hstl-smry-25 {
            margin: 0px;
            padding: 0px 1px;
            width: 25%;
            float: left;
        }

        .hstl-smry-bx-wpr {
            margin: 0px;
            padding: 5px 10px;
            width: 100%;
            float: left;
            text-align: center;
            background: #fff5f5;
            border: 1px solid #ffe0e0;
            border-radius: 3px;
        }

            .hstl-smry-bx-wpr img {
                margin: 0px auto;
                width: 110px;
            }

        .hstl-smry-bx-wpr-txt-p {
            margin: 10px 0px 0px 0px;
            padding: 0px;
            width: 100%;
            float: left;
            font-size: 14px;
            line-height: 25px;
        }

            .hstl-smry-bx-wpr-txt-p span {
                margin: 0px;
                padding: 0px 3px;
                background: #ff8888;
                font-weight: 500;
                color: #fff;
                font-size: 15px;
                border-radius: 2px;
            }

        .hstl-smry-bx-wpr-hstlname-h {
            width: 100%;
            padding: 4px 0px 3px 0px;
            float: left;
            margin: 0px 0px;
            background: #fff5f5;
            border: 1px solid #ffe0e0;
            text-align: center;
            font-size: 19px;
            font-weight: 400;
            line-height: 30px;
            border-bottom: 0px;
        }

        .table-responsive {
            overflow-x: inherit;
        }
        .head {
            display: none;
        }

        #pageFooter {
            display: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--start page wrapper -->
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
                <div class="breadcrumb-title pe-3">Hostel</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Hostel Summary</li>
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
                                                    <div class="col-sm-3">
                                                        <label for="validationCustom01" class="find-dv-lbl">Hostel Name</label>
                                                        <asp:DropDownList ID="ddl_hostel" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-1">
                                                        <asp:Button ID="btn_find" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_find_Click" />
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <asp:LinkButton ID="btn_excels" runat="server" Style="margin: 20px 0px 6px 0px;" OnClick="btn_excels_Click" class="btn btn-primary find-dv-btn">  <i class='bx bx-download'></i> Excel</asp:LinkButton>
                                                        <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" Style="margin: 20px 0px 6px 10px;" CssClass="btn btn-primary find-dv-btn" runat="server"
                                                            ToolTip="Print" Visible="false"><i class='bx bx-printer'></i></asp:LinkButton>
                                                    </div>

                                                </div>
                                            </div>


                                            <asp:Label ID="lbl_hostels_name" class="hstl-smry-bx-wpr-hstlname-h" runat="server" Visible="false" Style="display: none"></asp:Label>
                                            <div class="grd-wpr">

                                                <div style="display: none">


                                                    <div class="hstl-smry-25">
                                                        <div class="hstl-smry-bx-wpr">
                                                            <img src="../images/room.png" />

                                                            <p class="hstl-smry-bx-wpr-txt-p">
                                                                Total No. of Rooms :
                                                            <asp:Label ID="lbl_rooms" runat="server" Text="00"></asp:Label>
                                                            </p>
                                                        </div>
                                                    </div>
                                                    <div class="hstl-smry-25">
                                                        <div class="hstl-smry-bx-wpr">
                                                            <img src="../images/room.png" />

                                                            <p class="hstl-smry-bx-wpr-txt-p">
                                                                Total No. of Beds :
                                                            <asp:Label ID="lbl_ttl_beds" runat="server" Text="00"></asp:Label>
                                                            </p>
                                                        </div>
                                                    </div>
                                                    <div class="hstl-smry-25">
                                                        <div class="hstl-smry-bx-wpr">
                                                            <img src="../images/room.png" />

                                                            <p class="hstl-smry-bx-wpr-txt-p">
                                                                Total No. of Allocated Beds :
                                                            <asp:Label ID="lbl_ttl_allocated_beds" runat="server" Text="00"></asp:Label>
                                                            </p>
                                                        </div>
                                                    </div>
                                                    <div class="hstl-smry-25">
                                                        <div class="hstl-smry-bx-wpr">
                                                            <img src="../images/room.png" />

                                                            <p class="hstl-smry-bx-wpr-txt-p">
                                                                Total No. of Available Beds :
                                                            <asp:Label ID="lbl_aval_beds" runat="server" Text="00"></asp:Label>
                                                            </p>
                                                        </div>
                                                    </div>
                                                </div>







                                            </div>

                                            <div class="grd-wpr">
                                                <div id="tblPrintIQ" runat="server">
                                                    <div class="prnt-dv-wpr">
                                                        <div id="content">

                                                            <div class="pgslry-head-div head">

                                                                <div style="margin: 0px; padding: 0px; height: 110px; width: 20%; float: left;">
                                                                    <asp:Image ID="imglogo" runat="server" Style="height: 100px; width: 100px; margin: 0px 0px 0px 10px;" />
                                                                </div>
                                                                <div style="margin: 0px; padding: 0px; height: 110px; width: 80%; float: left;">
                                                                    <h1 style="margin: 0px 0px 0px 0px; width: 100%; padding: 0px; font-weight: bold; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 20px; text-decoration: underline;">
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
                                                                        <span style="font-size: 14px; text-transform: uppercase; font-weight: bold;">Hostel Summary
                                                                        <asp:Label ID="lbl_class22" runat="server"></asp:Label></span>


                                                                    </div>
                                                                </div>


                                                            </div>

                                                            <asp:Panel ID="Panel1" runat="server">
                                                                <table id="example21" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                                    <thead>
                                                                        <tr>
                                                                            <th>#</th>
                                                                            <th>Hostel Name</th>
                                                                            <th>Total Room</th>
                                                                            <th>Total Bed</th>
                                                                            <th>Total No. of Allocated Bed</th>
                                                                            <th>Total No. of Available Bed</th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <asp:Repeater ID="rd_view" runat="server">
                                                                            <ItemTemplate>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_hostel_name" runat="server" Text='<%#Bind("Hostel_name")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="Label4" runat="server" Text='<%#Bind("ttl_rooms")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="Label6" runat="server" Text='<%#Bind("total_bed")%>'></asp:Label>
                                                                                    </td>


                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_total_occupied_bed" runat="server" Text='<%#Bind("Total_occupied_bed")%>'></asp:Label>
                                                                                    </td>
                                                                                    <td style="text-align: left;">
                                                                                        <asp:Label ID="lbl_total_empty_bed" runat="server" Text='<%#Bind("Total_available_bed")%>'></asp:Label>
                                                                                    </td>

                                                                                </tr>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                    </tbody>
                                                                </table>
                                                            </asp:Panel>

                                                            
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
</asp:Content>
