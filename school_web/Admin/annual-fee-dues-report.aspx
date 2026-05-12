<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="annual-fee-dues-report.aspx.cs" Inherits="school_web.Admin.annual_fee_dues_report" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Annual Fees Dues Report
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

        .home-grph-wpr {
            width: 114%;
            margin: 0px 0px 0px -50px;
        }

        .home-grph-wpr-smll {
            margin: 50px 0px 0px -50px;
        }

        .home-grph-wprdv {
            float: left;
            width: 100%;
            overflow: hidden;
        }
    </style>

    <script type="text/javascript">
        $(function () {
            $("#<%=txt_student_name.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: 'admission-fee-dues-report.aspx/GetRooPath',
                        data: "{ 'PathRooT': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            if (data.d.length > 0) {
                                response($.map(data.d, function (item) {
                                    return {
                                        label: item
                                    };
                                }))
                            } else {
                                response([{ label: 'No results found.' }]);
                            }
                        }
                    });
                },
                select: function (e, u) {
                    if (u.item.val == -1) {
                        return false;
                    }
                }
            });
        });
    </script>
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
                <div class="breadcrumb-title pe-3"><a href="fee-report.aspx" class="backlnk-css"><i class="bx bx-arrow-back"></i></a>Report</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Annual Fees Dues List</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <ul class="sub-pag-menu-ul">
                        <li><a href="admission-fee-dues-report.aspx">Admission Fees Dues List</a></li>
                        <li><a href="annual-fee-dues-report.aspx" class="sub-mnu-p-a-active">Annual Fees Dues List</a></li>
                        <li><a href="monthly-dues.aspx">Monthly Dues List</a></li>
                        <li><a href="dues-list.aspx">Transport Dues</a></li>
                        <li><a href="overall-dues.aspx">All Dues List</a></li>
                    </ul>
                </div>
                <div class="col-xl-12">
                    <hr />
                    <h6 class="mb-0 text-uppercase"></h6>
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
                                                        <label for="validationCustom01" class="find-dv-lbl">Session</label>
                                                        <asp:DropDownList ID="ddlsession" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Class</label>
                                                        <asp:DropDownList ID="ddlclass" runat="server" class="form-control find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddlclass_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Section</label>
                                                        <asp:DropDownList ID="ddl_section" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>
                                                    </div>

                                                    <div class="col-sm-1">
                                                        <asp:Button ID="btn_find" runat="server" Text="find" class="btn btn-primary find-dv-btn" OnClick="btn_find_Click" />
                                                    </div>
                                                    <div class="col-sm-1"></div>
                                                    <div class="col-sm-3">
                                                        <label for="validationCustom01" class="find-dv-lbl">Find by Student Name</label>
                                                        <asp:TextBox ID="txt_student_name" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1">
                                                        <asp:Button ID="btn_fnd_student" runat="server" Text="find" class="btn btn-primary find-dv-btn" OnClick="btn_fnd_student_Click" />
                                                    </div>

                                                    <div class="col-sm-2">
                                                        <asp:LinkButton ID="btn_excels" runat="server" OnClick="btn_excels_Click" Style="margin-left: 10px;" class="btn btn-primary find-dv-btn">  <i class='bx bx-download'></i> Excel</asp:LinkButton>
                                                        <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" CssClass="btn btn-primary find-dv-btn" Style="margin-left: 10px;" runat="server"
                                                            ToolTip="Print"><i class='bx bx-printer'></i></asp:LinkButton>
                                                    </div>

                                                </div>
                                            </div>
                                             
                                            <div class="grd-wpr">
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
                                                                    <span style="font-size: 14px; font-weight: bold;">Annual Fees Dues List-<asp:Label ID="lbl_class22" runat="server"></asp:Label></span>
                                                                </div>
                                                            </div>
                                                        </div>


                                                        <asp:GridView ID="GrdView" runat="server" class="table table-bordered" ShowFooter="True" AutoGenerateColumns="False" Width="100%" OnRowDataBound="GrdView_RowDataBound">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Sl No.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Student Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Father's Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_fathername" runat="server" Text='<%#Bind("fathername")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Mobile No.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_mobileno" runat="server" Text='<%#Bind("father_mob")%>'></asp:Label>

                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="WhatsApp No.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_whatsapp" runat="server" Text='<%#Bind("Father_whatsApp_no")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Admission No.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_admissionserialnumber" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Session">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Class">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Section">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Roll No.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_rollnumber" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <b>Total</b>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Dues Amt.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_ActualDues" runat="server" Text='<%#Bind("ActualDues")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lbl_total_dues" runat="server"></asp:Label>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>



                                                            </Columns>
                                                        </asp:GridView>




                                                    </div>
                                                </div>

                                            </div>

                                            <div class="grd-wpr" id="sendsms" runat="server" visible="false" style="display: block">

                                                <table style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%;">
                                                    <tr>
                                                        <td style="padding: 5px; font-weight: bold;">Send Dues Message
                                                                            
                                                                            <asp:RadioButton ID="rd_whatassp" runat="server" GroupName="ak" Text="Whatsapp" />
                                                            <asp:RadioButton ID="rd_sms" Checked="true" runat="server" GroupName="ak" Text="SMS" />
                                                        </td>


                                                    </tr>

                                                    <tr>
                                                        <td style="padding: 5px;">
                                                            <div class="col-md-12">
                                                                <asp:Label ID="lbl_msg" runat="server"></asp:Label>
                                                            </div>
                                                            <div class="row" style="display: none">
                                                                <asp:Panel runat="server" Visible="false" ID="pnl_0" CssClass="col-lg-6">
                                                                    <asp:Label runat="server" ID="lbl_0" class="form-label">Message</asp:Label>
                                                                    <asp:TextBox ID="txt_0" runat="server" class="form-control"></asp:TextBox>
                                                                </asp:Panel>
                                                                <asp:Panel runat="server" Visible="false" ID="pnl_1" CssClass="col-lg-6">
                                                                    <asp:Label runat="server" ID="lbl_1" class="form-label">Message</asp:Label>
                                                                    <asp:TextBox ID="txt_1" runat="server" class="form-control"></asp:TextBox>
                                                                </asp:Panel>
                                                            </div>

                                                            <div class="row" style="display: none">
                                                                <asp:Panel runat="server" Visible="false" ID="pnl_2" CssClass="col-lg-6">
                                                                    <asp:Label runat="server" ID="lbl_2" class="form-label">Message</asp:Label>
                                                                    <asp:TextBox ID="txt_2" runat="server" class="form-control"></asp:TextBox>
                                                                </asp:Panel>

                                                                <asp:Panel runat="server" Visible="false" ID="pnl_3" CssClass="col-lg-6">
                                                                    <asp:Label runat="server" ID="lbl_3" class="form-label">Message</asp:Label>
                                                                    <asp:TextBox ID="txt_3" runat="server" class="form-control"></asp:TextBox>
                                                                </asp:Panel>
                                                            </div>
                                                            <div class="row" style="display: none">
                                                                <asp:Panel runat="server" Visible="false" ID="pnl_4" CssClass="col-lg-6" Style="display: block">
                                                                    <asp:Label runat="server" ID="lbl_4" class="form-label">Message</asp:Label>
                                                                    <asp:TextBox ID="txt_4" runat="server" class="form-control"></asp:TextBox>
                                                                </asp:Panel>

                                                                <asp:Panel runat="server" Visible="false" ID="pnl_5" CssClass="col-lg-6">
                                                                    <asp:Label runat="server" ID="lbl_5" class="form-label">Message</asp:Label>
                                                                    <asp:TextBox ID="txt_5" runat="server" class="form-control"></asp:TextBox>
                                                                </asp:Panel>
                                                            </div>

                                                            <div class="row" style="display: block">
                                                                <asp:Panel runat="server" Visible="false" ID="pnl_6" CssClass="col-lg-6">
                                                                    <asp:Label runat="server" ID="lbl_6" class="form-label">Message</asp:Label>
                                                                    <asp:TextBox ID="txt_6" runat="server" class="form-control" Style="width: 46%"></asp:TextBox>
                                                                </asp:Panel>

                                                                <asp:Panel runat="server" Visible="false" ID="pnl_7" Style="display: none" CssClass="col-lg-6">
                                                                    <asp:Label runat="server" ID="lbl_7" class="form-label">Message</asp:Label>
                                                                    <asp:TextBox ID="txt_7" runat="server" class="form-control"></asp:TextBox>
                                                                </asp:Panel> 
                                                            </div>
                                                            <div class="row">
                                                                <asp:Panel runat="server" Visible="false" ID="pnl_8" CssClass="col-lg-6">
                                                                    <asp:Label runat="server" ID="lbl_8" class="form-label">Message</asp:Label>
                                                                    <asp:TextBox ID="txt_8" runat="server" class="form-control"></asp:TextBox>
                                                                </asp:Panel>
                                                            </div>
                                                            <div class="row">
                                                                <asp:Panel runat="server" Visible="false" ID="pnl_msg" class="col-lg-12">
                                                                    <label for="validationCustom01" class="form-label">Message</label>
                                                                    <asp:TextBox ID="txt_message" ReadOnly="true" TextMode="MultiLine" runat="server" Style="height: 100px" class="form-control"></asp:TextBox>
                                                                </asp:Panel>
                                                            </div> 
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding: 5px;">
                                                            <asp:Button ID="btn_msgPreview" runat="server" Text="SMS Preview" class="btn btn-danger find-dv-btn" OnClick="btn_msgPreview_Click" />
                                                            <div style="overflow: hidden; height: 1px">
                                                                <asp:Button ID="btn_Submit" runat="server" Text="Send" class="btn btn-success find-dv-btn" Style="margin-left: 20px;" OnClick="btn_Submit_Click" />
                                                            </div>
                                                            <a onclick="save_data()" class="btn btn-primary" style="width: 182px; margin: 21px 0px 0px 9px;">Final Send Message</a>
                                                        </td>
                                                    </tr>
                                                </table>
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
    <asp:HiddenField ID="hd_id" runat="server" />
    <!--end page wrapper -->


    <asp:HiddenField ID="hd_flag" runat="server" />
    <asp:HiddenField ID="hd_session" runat="server" />
    <asp:HiddenField ID="hd_class" runat="server" />
    <asp:HiddenField ID="hd_stunt_name" runat="server" />

 
</asp:Content>
