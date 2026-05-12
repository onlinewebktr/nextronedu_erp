<%@ Page Title="" Language="C#" MasterPageFile="~/LMS_VC_Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Payment_Entry.aspx.cs" Inherits="school_web.LMS_VC_Admin.Payment_Entry" %>

<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
  Missing Payment Entry
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .calendar, .calendar table {
            border: 1px solid #556;
            font-size: 12px;
            color: #000;
            cursor: default;
            background: #eef;
            font-family: tahoma,verdana,sans-serif;
            z-index: 99999999!important;
        }

        td {
            vertical-align: middle!important;
        }

        .app-wrapper-footer {
            display: none;
        }

        .waiting {
            padding: 15px 15px 15px 14px;
            font-size: 16px;
            bottom: 0px;
            left: 1px;
            top: 300px;
            background: #fff0;
            color: #1a1313;
            height: 55px!important;
            z-index: 1000;
            font-size: 17px;
            text-align: center;
            width: 99.8%;
            position: fixed;
        }

        .notificationpan {
           
            width: 100%;
            background-color: rgb(255, 76, 76);
            position: fixed;
            top: 72px!important;
            right: 25px!important;
            padding: 10px 10px;
            width: 369px!important;
            height: auto;
            border: 1px solid rgb(162, 162, 162);
            box-shadow: 2px 7px 19px -2px rgb(154 154 154 / 80%);
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%; position: relative">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="app-main__inner">
                    <div class="app-page-title">
                        <div class="page-title-wrapper">
                            <div class="page-title-heading">
                                <div class="page-title-icon">
                                    <i class="pe-7s-display1 icon-gradient bg-mean-fruit"></i>
                                </div>
                                <div>
                                    <asp:Literal ID="ltUsertop" runat="server">  Missing Payment Entry</asp:Literal>

                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="notification">
                        <div id="pan" class="notificationpan">
                            <div style="float: left; width: 100%; height: auto;">
                                <asp:Label ID="lblmessage" runat="server" Font-Bold="True" ForeColor="White"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <asp:HiddenField ID="slid" runat="server" />
                    <div class="row">
                        <div class="col-lg-2"></div>
                        <div class="col-lg-5">
                            <div class="main-card mb-3 card">
                                <div class="card-body">
                                    <h5 class="card-title"></h5>
                                    <div class="form-row">
                                        <div class="col-md-12">

                                            <div class="position-relative form-group">
                                                <label>Admission No</label>
                                                <asp:TextBox ID="txt_admission_no" runat="server" CssClass="form-control"></asp:TextBox>

                                            </div>
                                            <div class="position-relative form-group">
                                                <label>Reference Id(Order Tracking ID)</label>
                                                <asp:TextBox ID="txt_refrrenceid_OrderTrackingID" runat="server" CssClass="form-control"></asp:TextBox>

                                            </div>
                                            <div class="position-relative form-group">
                                                <label>Software Transaction Id(Transaction Id)</label>
                                                <asp:TextBox ID="txt_TransactionId" runat="server" CssClass="form-control"></asp:TextBox>

                                            </div>
                                            <div class="position-relative form-group">
                                                <label>Date</label>
                                                <asp:TextBox ID="txt_date" runat="server" CssClass="form-control"></asp:TextBox>

                                            </div>

                                            <div class="position-relative form-group">
                                                <label>Time</label>
                                                <cc1:TimeSelector ID="time_StartTime" runat="server" MinuteIncrement="1" Font-Size="Larger" DisplaySeconds="true" DisplayButtons="true"></cc1:TimeSelector>

                                            </div>

                                            <div class="position-relative form-group">
                                                <label>Months(Please enter comma separated month name)</label>
                                                <asp:TextBox ID="txt_months" runat="server" CssClass="form-control"></asp:TextBox>

                                            </div>

                                            <div class="position-relative form-group">
                                                <label>Total Amount</label>
                                                <asp:TextBox ID="txt_total_amount" CssClass="form-control" AutoPostBack="true" runat="server" onkeypress="return isNumberKey(event)" OnTextChanged="txt_total_amount_TextChanged"></asp:TextBox>

                                            </div>

                                            <div class="position-relative form-group">
                                                <label>Payable  Amount</label>
                                                <asp:TextBox ID="txt_paybalamount" CssClass="form-control" AutoPostBack="true" runat="server" onkeypress="return isNumberKey(event)" OnTextChanged="txt_paybalamount_TextChanged"></asp:TextBox>

                                            </div>
                                            <div class="position-relative form-group">
                                                <label>Discount Amount</label>
                                                <asp:TextBox ID="txt_discountamount" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>

                                            </div>


                                            <asp:Button ID="btn_Submit" runat="server" Text="Add" class="mt-2 btn btn-primary" OnClick="btn_Submit_Click" />
                                            <asp:Button ID="btn_cancel" runat="server" Text="Cancel" class="mt-2 btn btn-danger" OnClick="btn_cancel_Click" Visible="false" />
                                            <asp:HiddenField ID="hd_id" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2"></div>
                    </div>
                </div>
                <asp:HiddenField ID="HiddenField1" runat="server" />
            </ContentTemplate>

        </asp:UpdatePanel>
        <asp:UpdateProgress ID="UpdateProgress2"
            runat="server" AssociatedUpdatePanelID="UpdatePanel2"
            DynamicLayout="False">
            <ProgressTemplate>
                <p class="waiting">
                    &nbsp;&nbsp;&nbsp;
                                            <img src="../images/Processing.gif" />

                </p>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
    <link href="../Autocomplete/jquery-ui.css" rel="stylesheet" />
    <script src="../Autocomplete/jquery-ui.js"></script>
    <script type="text/javascript">
        //On Page Load.
        $(function () {
            SetDatePicker();


        });

        //On UpdatePanel Refresh.
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    SetDatePicker();



                }
            });
        };
        function SetDatePicker() {
            $("[id$=txt_date]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                buttonImage: 'calendar.png',
                changeMonth: true,
                changeYear: true,
                yearRange: "1900:2100",
                dateFormat: "dd/mm/yy",

            });
        }


    </script>
</asp:Content>
