<%@ Page Title="" Language="C#" MasterPageFile="~/Student_Profile/main.Master" AutoEventWireup="true" CodeBehind="apply-for-leave.aspx.cs" Inherits="school_web.Student_Profile.apply_for_leave" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Apply For Leave
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pagemainhh">
        <div class="container">

            <div id="notification">
                <div id="pan" class="notificationpan">
                    <div id="success" runat="server" visible="false" style="float: left; width: 100%; height: auto;" class="alert alert-success border-0 bg-success alert-dismissible fade show py-2">
                        <div class="d-flex align-items-center">
                            <div class="font-35 text-white">
                                <i class='bx bxs-check-circle'></i>
                            </div>
                            <div class="ms-3">
                                <asp:Label ID="lbl_success" runat="server" ForeColor="White" class="text-white"></asp:Label>
                            </div>
                        </div>
                        <asp:LinkButton ID="LinkButton1" class="btn-closes" runat="server" Style="color: #fff">X</asp:LinkButton>
                    </div>

                    <div id="warning" runat="server" visible="false" class="alert alert-warning border-0 bg-warning alert-dismissible fade show py-2">
                        <div class="d-flex align-items-center">
                            <div class="font-35 text-dark">
                                <i class='bx bx-info-circle'></i>
                            </div>
                            <div class="ms-3">
                                <asp:Label ID="lbl_warning" runat="server" ForeColor="White" class="text-white"></asp:Label>
                            </div>
                        </div>
                        <asp:LinkButton ID="LinkButton2" class="btn-closes" runat="server" Style="color: #fff">X</asp:LinkButton>
                    </div>
                </div>
            </div>



            <div class="headingtablee">
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="main-card mb-3 card">
                            <div class="card-header">
                                <h4 class="card-title">Apply For Leave</h4>
                            </div>
                            <div class="card-body" style="padding-top: 0px;">
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="txtbx-dv-wpr">
                                            <p class="form-txtbx-p">Your Name</p>
                                            <asp:TextBox ID="txt_name" runat="server" CssClass="form-control pontr-non" Style="pointer-events: none;"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3  col-width-50">
                                        <div class="txtbx-dv-wpr">
                                            <p class="form-txtbx-p">Your Class Name</p>
                                            <asp:TextBox ID="txt_class_name" runat="server" CssClass="form-control pontr-non" Style="pointer-events: none;"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2 col-xs-4  col-width-50">
                                        <div class="txtbx-dv-wpr">
                                            <p class="form-txtbx-p">Your Section</p>
                                            <asp:TextBox ID="txt_section" runat="server" CssClass="form-control pontr-non" Style="pointer-events: none;"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2 col-xs-4  col-width-50">
                                        <div class="txtbx-dv-wpr">
                                            <p class="form-txtbx-p">Admission No.</p>
                                            <asp:TextBox ID="txt_admission_no" runat="server" CssClass="form-control pontr-non" Style="pointer-events: none;"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2 col-xs-4  col-width-50">
                                        <div class="txtbx-dv-wpr">
                                            <p class="form-txtbx-p">Roll No.</p>
                                            <asp:TextBox ID="txt_roll_no" runat="server" CssClass="form-control pontr-non" Style="pointer-events: none;"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3 col-xs-6  col-width-50">
                                        <div class="txtbx-dv-wpr">
                                            <p class="form-txtbx-p">From Date</p>
                                            <div class="clndr-dv-wpr">
                                                <asp:TextBox ID="txt_from_date" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txt_from_date_TextChanged"></asp:TextBox>
                                                <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3 col-xs-6  col-width-50">
                                        <div class="txtbx-dv-wpr">
                                            <p class="form-txtbx-p">To Date</p>
                                            <div class="clndr-dv-wpr">
                                                <asp:TextBox ID="txt_to_date" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txt_from_date_TextChanged"></asp:TextBox>
                                                <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="txtbx-dv-wpr">
                                            <p class="form-txtbx-p">No. of Days</p>
                                            <asp:TextBox ID="txt_ttl_leave" runat="server" CssClass="form-control pontr-non" Style="pointer-events: none;"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="txtbx-dv-wpr">
                                            <p class="form-txtbx-p">Subject</p>
                                            <asp:TextBox ID="txt_leave_subject" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="txtbx-dv-wpr">
                                            <p class="form-txtbx-p">Leave Detail</p>
                                            <asp:TextBox ID="txt_leave_details" runat="server" CssClass="form-control" TextMode="MultiLine" Style="border: rgba(29,37,59,.5)1px solid; padding: 5px 5px 0 5px; height: 70px; border-radius: 4px;"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="txtbx-dv-wpr">
                                            <p class="form-txtbx-p">Add Attachment (if any)</p>
                                            <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" />
                                        </div>
                                    </div>

                                    <div class="col-md-12">
                                        <div class="btns-dv-wpr">
                                            <asp:Button ID="btn_Submit" runat="server" Text="Apply For Leave" class="mt-2 btn btn-primary" OnClick="btn_Submit_Click" />
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


    <link href="../Autocomplete/jquery-ui.css" rel="stylesheet" />
    <script src="../Autocomplete/jquery-ui.js"></script>

    <script>
        $(function () {
            $("#<%=txt_from_date.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "1900:2100"
            });
        });
    </script>
    <script>
        $(function () {
            $("#<%=txt_to_date.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "1900:2100"
            });
        });
    </script>
    <link href="assets/css/calender-modified.css" rel="stylesheet" />

</asp:Content>
