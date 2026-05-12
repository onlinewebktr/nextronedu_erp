<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Whatsapp_API_Setting.aspx.cs" Inherits="school_web.Admin.Whatsapp_API_Setting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Whatsapp API Setting
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#Whatsapp_Confg").addClass("dcjq-parent active");
        });
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
                <div class="breadcrumb-title pe-3">SMS</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Whatsapp API Setting</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-7">
                    <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text=" Whatsapp API Setting"></asp:Label>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation" novalidate="">
                                    <table class="table table-primary" style="width: 100%; margin: 0px auto;">
                                        <tr>
                                            <td style="padding-left: 10px">
                                                <div class="position-relative form-group">
                                                    <label>Message URL</label>
                                                </div>
                                            </td>
                                            <td style="padding-left: 10px">
                                                <asp:TextBox ID="txt_url" runat="server" CssClass="form-control"></asp:TextBox>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td style="padding-left: 10px">
                                                <div class="position-relative form-group">
                                                    <label>API Key</label>
                                                </div>
                                            </td>
                                            <td style="padding-left: 10px">
                                                <asp:TextBox ID="txt_SMS_api" runat="server" CssClass="form-control"></asp:TextBox>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td style="padding-left: 10px">
                                                <div class="position-relative form-group">
                                                    <label>Balance URL</label>
                                                </div>
                                            </td>
                                            <td style="padding-left: 10px">
                                                <asp:TextBox ID="txt_balance_url" runat="server" CssClass="form-control"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr style="display:none">
                                            <td style="padding-left: 10px">
                                                <div class="position-relative form-group">
                                                    <label>Scan Qr Code URL</label>
                                                </div>
                                            </td>
                                            <td style="padding-left: 10px">
                                                <asp:TextBox ID="txt_scanqrcode" runat="server" CssClass="form-control"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>

                                            <td colspan="2" style="text-align:center">
                                                 
                                                    <asp:Button ID="btn_save_sms_setting" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btn_save_sms_setting_Click" />
                                                 
                                            </td>

                                        </tr>
                                        <tr>
                                            <td>
                                                <p>
                                                    Available Balance:-
                                                            <asp:Label ID="lbl_balance" runat="server" Text="0"></asp:Label>
                                                </p>
                                            </td>
                                            <td>
                                                <asp:Button ID="btn_reload_smms_balance" runat="server" Text="Refresh" OnClick="btn_reload_smms_balance_Click" CssClass="btn btn-primary" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                 <asp:Label ID="lblsmsmsg" runat="server"  ></asp:Label>
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td colspan="2" style="text-align: center;">
                                                <asp:Panel ID="Panel1" runat="server" Visible="false">
                                                    <img src="../images/whatsappiconect.png" style="    height: 200px;
    width: 200px;" />
                                                     

                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


                <div class="col-xl-7" style="display:none">
                    <h6 class="mb-0 text-uppercase">Added SMS Configration</h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="table-responsive">
                                <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <table class="table table-primary" style="width: 100%; margin: 0px auto;">
                                                <tr>
                                                    <td style="padding-left: 10px;">
                                                        <div class="position-relative form-group">
                                                            <label>Message Templete Name</label>
                                                        </div>
                                                    </td>
                                                    <td style="padding-left: 10px;">
                                                        <asp:DropDownList ID="dd_sms_format" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="dd_sms_format_SelectedIndexChanged">
                                                            <asp:ListItem>Select</asp:ListItem>
                                                            <asp:ListItem>Patient Registration</asp:ListItem>
                                                            <asp:ListItem>IPD Admission (To Patient)</asp:ListItem>
                                                            <asp:ListItem>IPD Admission(To Doctor)</asp:ListItem>
                                                            <asp:ListItem>Patient Ward Shift(To Patient)</asp:ListItem>
                                                            <asp:ListItem>Patient Ward Shift(To Doctor)</asp:ListItem>
                                                            <asp:ListItem>OT Schedule(To Patient)</asp:ListItem>
                                                            <asp:ListItem>OT Schedule(To Doctor)</asp:ListItem>
                                                            <asp:ListItem>Emergency Admission(To Doctor)</asp:ListItem>
                                                            <asp:ListItem>Patient OPD/LAB/PROC/EMERGENCY Admission</asp:ListItem>
                                                            <asp:ListItem>OPD Doctor</asp:ListItem>
                                                            <asp:ListItem>Referred By</asp:ListItem>
                                                            <asp:ListItem>Bill for OPD/LAB/PROCEDURE</asp:ListItem>
                                                            <asp:ListItem>Bill for IPD</asp:ListItem>
                                                            <asp:ListItem>Money Receipt</asp:ListItem>
                                                            <asp:ListItem>Admin Payment</asp:ListItem>
                                                            <asp:ListItem>Payment Due</asp:ListItem>
                                                            <asp:ListItem>Student Health Card Registration</asp:ListItem>
                                                            <asp:ListItem>Family Health Card Registration</asp:ListItem>
                                                            <asp:ListItem>Professional Health Card Registration</asp:ListItem>
                                                            <asp:ListItem>Festival Wish</asp:ListItem>
                                                            <asp:ListItem>Birthday Wish</asp:ListItem>
                                                            <asp:ListItem>Health Camp</asp:ListItem>
                                                            <asp:ListItem>Hospital Close</asp:ListItem>
                                                            <asp:ListItem>Feedback (OPD AND DISCHARGED)</asp:ListItem>
                                                            <asp:ListItem>Corona Awareness</asp:ListItem>
                                                            <asp:ListItem>Weather Change Awareness</asp:ListItem>
                                                            <asp:ListItem>Edit Bill Service</asp:ListItem>
                                                            <asp:ListItem>Delete Bill Service</asp:ListItem>
                                                            <asp:ListItem>OTP REQUIRED FOR REPORT</asp:ListItem>
                                                            <asp:ListItem>COLLECTION REPORT</asp:ListItem>
                                                        </asp:DropDownList>

                                                    </td>

                                                </tr>

                                                <tr style="display:none">
                                                    <td>
                                                        <asp:CheckBox ID="chk_sms_enable" runat="server" Text="Is Enable" />
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chk_send_to_admin" runat="server" Text="Is Send Message To Admin" />
                                                    </td>
                                                </tr>
                                                <tr>

                                                    <td colspan="2" style="padding-left: 10px">
                                                        <asp:TextBox ID="txt_message" runat="server" CssClass="form-control" TextMode="MultiLine" Height="200"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <p style="font-size: 13px;">
                                                            {PatientName},{PRegNo},{Age},{Sex},{CDoctorName},{ReferredBy},{ReceptNo},{IPDNo},{OPDNo},{LABNo},{PROCNo},{VACCNo},{Amount},{Date},{HospitalName},{HospitalAddress},{RoomNo},{BedNo},{OperationName}
                                                        </p>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <div class="col-md-12">
                                                            <asp:Button ID="btn_save_sms_format" runat="server" Text="Save" CssClass="myButton" OnClick="btn_save_sms_format_Click" />
                                                        </div>
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
        <!--end row-->
    </div>

    <!--end page wrapper -->
    <asp:HiddenField ID="hd_id" runat="server" />
</asp:Content>
