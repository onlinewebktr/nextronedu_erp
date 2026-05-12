<%@ Page Title="" Language="C#" MasterPageFile="~/LMS_VC_Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Send_Notice_To_Teacher.aspx.cs" Inherits="school_web.LMS_VC_Admin.Send_Notice_To_Teacher" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Send Notice  to Teacher
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script>
        function openModal() {

            $("#modalConfirm").modal('show');
        }
    </script>
    <style>
        .notificationpan {
            display: none;
            top: 66px!important;
            right: 10px!important;
            padding: 10px 10px;
            width: 337px;
            height: auto;
            border: 1px solid rgb(162, 162, 162);
            box-shadow: 2px 7px 19px -2px rgb(154 154 154 / 80%);
        }

        .calender-icon {
            margin: 0px 0px 0px 0px;
            position: relative;
        }

        .clndr-icon {
            font-size: 16px !important;
            color: #ff2956;
            position: absolute;
            top: 6px;
            left: 43px;
        }
    </style>
    <style>
        .multiselect-container {
            top: 0px !important;
            width: 300px;
            left: -18px!important;
        }

        .input-group > .form-control, .input-group > .form-control-plaintext, .input-group > .custom-select, .input-group > .custom-file {
            position: relative;
            flex: 1 1 auto;
            width: 1%;
            margin-bottom: 0;
            z-index: 99999!important;
        }

        .dropdown-menu.show {
            animation: fade-in2 0.2s cubic-bezier(0.39, 0.575, 0.565, 1) both;
            top: 10px!important;
            height: 408px!important;
            overflow-x: hidden!important;
            overflow-y: scroll!important;
        }

        .dt-button-collection {
            margin-top: -59.4px!important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="app-main__inner">
        <div class="app-page-title">
            <div class="page-title-wrapper">
                <div class="page-title-heading">
                    <div class="page-title-icon">
                        <i class="pe-7s-users icon-gradient bg-mean-fruit"></i>
                    </div>
                    <div>
                        <asp:Literal ID="ltUsertop" runat="server"> Notice Board</asp:Literal>

                    </div>
                </div>
                <div class="page-title-actions">
                </div>
            </div>
        </div>
        <div id="notification">
            <div id="pan" class="notificationpan">
                <div style="float: left; width: 100%; height: auto;">
                    <asp:Label ID="lbl_msg" runat="server" Font-Bold="True" ForeColor="White"></asp:Label>
                </div>
                <img src="../images/close.png" onclick="$(function () { $('.notificationpan').show().slideUp(1000);});"
                    class="closenotificationpan" alt="" />
            </div>
        </div>
        <div class="row">
            <div class="col-lg-8">
                <div class="main-card mb-3 card">
                    <div class="card-body">
                        <h5 class="card-title">Add Notice</h5>
                        <div class="form-row">
                            <div class="col-md-12">



                                <div class="position-relative form-group" >

                                    <label>Teacher List<sup>*</sup></label>
                                    <div class="input-group input-group-icon" style="width: 32%;">
                                        <asp:DropDownList ID="ddl_teacher" class="form-control" runat="server" Style="width: 100%!important"></asp:DropDownList>
                                    </div>


                                </div>

                                <div class="clearfix"></div>

                                <div class="position-relative form-group">

                                    <label>Date<sup>*</sup></label>

                                    <asp:TextBox ID="txt_date" runat="server" CssClass="form-control calender-icon" Style="width: 32%"></asp:TextBox>
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

                                
                                <div class="clearfix"></div>

                                <div class="position-relative form-group">
                                    <label>Notice<sup>*</sup></label>
                                    <div class="input-group">
                                        <asp:TextBox ID="txt_notice_details" runat="server" CssClass="form-control" TextMode="MultiLine" Height="150px"></asp:TextBox>


                                    </div>
                                </div>
                                
                                <div class="clearfix"></div>
                                <label>Choose File (<sup> jpg,png,pdf-FIle Size-300KB </sup>)</label>
                                <div class="input-group input-group-icon">
                                    <asp:FileUpload ID="fl_Photo" runat="server" />
                                    <asp:HiddenField ID="Hd_Photo" runat="server" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7"
                                        runat="server" ControlToValidate="fl_Photo"
                                        ErrorMessage="Invalid File. Please upload a File with extension: png, jpg, pdf" ForeColor="Red"
                                        ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.jpeg|JPG|.JPEG|.PNG|.pdf|.PDF)$"
                                        ValidationGroup="A" SetFocusOnError="true" Display="Dynamic" CssClass="error"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="card-footer pull-right" style="padding: 11px 0px 0px 0px;">
                                <asp:Button ID="btn_submit" runat="server" Text="Send" CssClass="btn btn-primary" OnClick="btn_submit_Click" ValidationGroup="A" style="width: 120px;" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>


        </div>
    </div>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
