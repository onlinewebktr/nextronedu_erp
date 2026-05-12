<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Send_Message_to_Student.aspx.cs" Inherits="school_web.Admin.Send_Message_to_Student" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Send Message to Student 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .ui-datepicker .ui-datepicker-title select {
            font-size: 1em;
            margin: 1px 0;
            COLOR: #000;
            z-index: 99999 !important;
        }

        .calender-icon {
            margin: 0px 0px 0px 0px;
            position: relative;
        }

        .clndr-icon {
            font-size: 16px !important;
            color: #ff2956;
            position: absolute;
            top: 247px;
            right: 342px;
        }

        input[type=checkbox], input[type=radio] {
            background: #000;
            border-style: none;
            width: 20px;
            height: 20px;
            position: relative;
            top: 2.6px;
            left: 0px;
            margin: 0px 10px 0px 0px;
            z-index: 0;
        }
    </style>
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
                <div class="breadcrumb-title pe-3"><a href="Message.aspx" class="backlnk-css"><i class="bx bx-arrow-back"></i></a>Message</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Send Message to Students</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">


                <div class="col-xl-12">
                    <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text=""></asp:Label>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="row">


                                <div class="col-xl-3"></div>
                                <div class="col-xl-6">
                                    <div class="p-4 border rounded">
                                        <div class="row g-3 needs-validation" novalidate="">
                                            <div class="col-md-12">
                                                <label>Send Type<sup>*</sup></label>
                                                <div class="input-group input-group-icon">
                                                    <asp:RadioButton ID="rd_Class_wise" runat="server" Text="Class Wise" Checked="true" GroupName="ak" AutoPostBack="true" OnCheckedChanged="rd_Class_wise_CheckedChanged" />
                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                          <asp:RadioButton ID="rd_individual" runat="server" Text="Individual" AutoPostBack="true" GroupName="ak" OnCheckedChanged="rd_individual_CheckedChanged" />
                                                </div>
                                            </div>

                                            <div id="class1" runat="server">
                                                <div class="col-md-12">
                                                    <label for="validationCustom01" class="form-label">Class<sup></sup></label>
                                                    <asp:DropDownList ID="ddl_class" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_class_SelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                                <div class="col-md-12">
                                                    <label for="validationCustom01" class="form-label">Section<sup>*</sup></label>
                                                    <asp:DropDownList ID="ddl_section" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div id="admissionno" runat="server" visible="false">


                                                <div class="col-md-12">
                                                    <label for="validationCustom01" class="form-label">Admission No.<sup>* </sup></label>
                                                    <asp:TextBox ID="txt_admission_no" runat="server" class="form-control"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="col-md-12">
                                                <label for="validationCustom01" class="form-label">Date<sup>* </sup></label>
                                                <asp:TextBox ID="txt_date" runat="server" CssClass="form-control calender-icon"></asp:TextBox>
                                                <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                                <link href="../Autocomplete/jquery-ui.css" rel="stylesheet" />

                                                <script src="../Autocomplete/jquery-ui.js"></script>
                                                <script>
                                                    $(function () {
                                                        $("#<%=txt_date.ClientID %>").datepicker({
                                                            dateFormat: "dd/mm/yy",
                                                            changeMonth: true,
                                                            changeYear: true,
                                                            yearRange: "1900:2100",
                                                            minDate: 0

                                                        }).attr("readonly", "true");
                                                    });
                                                </script>
                                            </div>

                                            <div class="col-md-12">
                                                <label for="validationCustom01" class="form-label">Subject<sup>*<asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" runat="server" ErrorMessage="*" ValidationGroup="a" ControlToValidate="txt_notice_subject"></asp:RequiredFieldValidator></sup></label>
                                                <asp:TextBox ID="txt_notice_subject" runat="server" class="form-control"></asp:TextBox>
                                            </div>

                                            <div class="col-md-12">
                                                <label>Link</label>
                                                <asp:TextBox ID="txt_link" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-12">
                                                <label>Message Details<sup>*</sup></label>

                                                <asp:TextBox ID="txt_notice_details" runat="server" CssClass="form-control" TextMode="MultiLine" Height="150px"></asp:TextBox>



                                            </div>
                                            <div class="col-md-12">
                                                <label>Choose File (<sup> jpg,png,pdf </sup>)</label>

                                                <asp:FileUpload ID="fl_Photo" runat="server" CssClass="form-control" />
                                                <asp:HiddenField ID="Hd_Photo" runat="server" />
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator7"
                                                    runat="server" ControlToValidate="fl_Photo"
                                                    ErrorMessage="Invalid File. Please upload a File with extension: png, jpg, jpeg" ForeColor="Red"
                                                    ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.jpeg|JPG|.JPEG|.PNG|.pdf|.PDF )$"
                                                    ValidationGroup="A" SetFocusOnError="true" Display="Dynamic" CssClass="error"></asp:RegularExpressionValidator>
                                            </div>



                                            <div class="col-12" style="text-align: center">
                                                <asp:Button ID="btn_Submit" runat="server" Text="Submit" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btn_Submit_Click" />
                                                <asp:Button ID="btn_cancel" runat="server" Text="Cancel" class="btn btn-dark" Visible="false" CausesValidation="false" OnClick="btn_cancel_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-xl-3"></div>
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
