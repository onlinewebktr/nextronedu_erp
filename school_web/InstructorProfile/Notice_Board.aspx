<%@ Page Title="" Language="C#" MasterPageFile="~/InstructorProfile/Teacher_Profile.Master" AutoEventWireup="true" CodeBehind="Notice_Board.aspx.cs" Inherits="school_web.InstructorProfile.Notice_Board" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Notice Board
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .ui-datepicker .ui-datepicker-title select {
            font-size: 1em;
            margin: 1px 0;
            COLOR: #000;
            z-index: 99999!important;
        }

        .ui-datepicker .ui-datepicker-title select {
            font-size: 1em;
            margin: 1px 0;
            COLOR: #000;
            z-index: 99999!important;
        }

        .gridcss th {
            font-size: 14px!important;
        }

        .calender-icon {
            margin: 0px 0px 0px 0px;
            position: relative;
        }

        .clndr-icon {
            font-size: 16px !important;
            color: #ff2956;
            position: absolute;
            top: 33px;
            right: 10px;
        }
    </style>

    <link href="../Autocomplete/jquery-ui.css" rel="stylesheet" />

    <script src="../Autocomplete/jquery-ui.js"></script>
    <script>
        $(function () {
            $("#<%=txt_date.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "1900:2100",


            });
        });
    </script>
    <script>
        $(function () {
            $("#<%=txt_enddate.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "1900:2100",


            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="app-main__inner">
        <div class="app-page-title">
            <div class="page-title-wrapper">
                <div class="page-title-heading">
                    <div class="page-title-icon">
                        <i class="fa fa-bullhorn icon-gradient bg-mean-fruit"></i>
                    </div>
                    <div>
                        <asp:Literal ID="ltUsertop" runat="server"> Notice Board</asp:Literal>

                    </div>
                </div>

            </div>
        </div>
        <div id="notification">
            <div id="pan" class="notificationpan">
                <div style="float: left; width: 235px; height: auto;">
                    <asp:Label ID="lbl_msg" runat="server" Font-Bold="True" ForeColor="White"></asp:Label>
                </div>
                <img src="../images/close.png" onclick="$(function () { $('.notificationpan').show().slideUp(1000);});"
                    class="closenotificationpan" alt="" />
            </div>
        </div>
        <div class="clearfix"></div>
        <div class="row">
            <div class="col-lg-12">
                <div class="main-card mb-3 card">
                    <div class="card-body">
                        <div class="form-row">
                            <div class="col-md-2">
                                <div class="position-relative form-group">
                                    <label>Start Date</label>
                                    <asp:TextBox ID="txt_date" runat="server" CssClass="form-control  calender-icon"></asp:TextBox>
                                    <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="position-relative form-group">
                                    <label>End Date</label>

                                    <asp:TextBox ID="txt_enddate" runat="server" CssClass="form-control  calender-icon"></asp:TextBox>
                                    <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                </div>
                            </div>
                            <div class="col-md-1">
                                <asp:Button ID="Btn_Find" runat="server" OnClick="Btn_Find_Click" class="btn btn-primary" Text="Find" Style="margin-top: 30px;" />
                            </div>
                        </div>
                        <hr />
                        <table style="width: 100%;" id="example" class="table table-hover table-striped table-bordered">
                            <thead>
                                <tr>
                                    <th>Sl No.</th>

                                    <th>Notice Deatils</th>

                                    <th>File</th>
                                    <th>Date</th>

                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="RPDetails" runat="server" OnItemDataBound="RPDetails_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label></td>

                                            <td>
                                                <asp:Label ID="lbl_Details" Style="word-break: break-all;" runat="server" Text='<%#Bind("Notice") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <a id="a1" runat="server" href='<%#Eval("Attachment") %>' download style="display: block; padding: 5px 0px 7px 9px; font-family: ebrima; font-size: 21px; color: #0066CC; text-decoration: none;"><i class="fa fa-download" aria-hidden="true"></i></a>


                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_date" runat="server" Text='<%#Bind("Posted_Date") %>'></asp:Label>

                                                <asp:Label ID="lbl_Attachment" runat="server" Text='<%#Bind("Attachment") %>' Visible="false"></asp:Label>
                                                <asp:Label ID="lbl_Teacher_Id" runat="server" Text='<%#Bind("Teacher_Id") %>' Visible="false"></asp:Label>
                                            </td>


                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                </div>

            </div>


        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
