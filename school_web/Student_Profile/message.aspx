<%@ Page Title="" Language="C#" MasterPageFile="~/Student_Profile/main.Master" AutoEventWireup="true" CodeBehind="message.aspx.cs" Inherits="school_web.Student_Profile.message" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Message
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
        <style>
        .texbox-border {
            margin: 6px 0px 0px 0px;
            padding: 0px;
            height: auto;
            width: 100%;
            border-bottom: 1px solid #00000038;
        }


        .navbar-expand-lg .navbar-nav {
            flex-direction: row;
            float: right !important;
        }


        .dropdown-menu > li > a {
            font-size: 14px !important;
        }

        .notificationpan {
            top: 58px!important;
        }
    </style>
   <%-- <link href="css/bootstrap.min.css" rel="stylesheet" />--%>
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
                        <asp:LinkButton ID="LinkButton1" class="btn-close" runat="server" Style="color: #fff">X</asp:LinkButton>
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
                        <asp:LinkButton ID="LinkButton2" class="btn-close" runat="server" Style="color: #fff">X</asp:LinkButton>
                    </div>
                </div>
            </div>


            <div class="main-card mb-3 card">
                <div class="card-header">
                    <h4 class="card-title">Message</h4>
                </div>
                <div class="card-body" style="padding-top: 0px;">
                    <div class="headingtablee">

                        <div class="row">
                            <div class="col-lg-2 col-width-50 pads-rght-5">
                                <label for="validationCustom01" class="lebelheadpp">From Date</label>
                                <div class="clndr-dv-wpr" style="position: relative;">
                                    <asp:TextBox ID="txt_from_date" runat="server" class="form-control"></asp:TextBox>
                                    <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                </div>
                            </div>
                            <div class="col-lg-2 col-width-50 pads-lft-5">
                                <label for="validationCustom01" class="lebelheadpp">To Date</label>
                                <div class="clndr-dv-wpr" style="position: relative;">
                                    <asp:TextBox ID="txt_to_date" runat="server" class="form-control"></asp:TextBox>
                                    <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                </div>
                            </div>

                            <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                                <asp:Button ID="btn_find" runat="server" Text="Find" class="mt-2 btn btn-primary fnd-btnmrgn" OnClick="btn_find_Click" />
                            </div>
                        </div>


                        <asp:Repeater ID="RPDetails" runat="server" OnItemDataBound="rp_messages_ItemDataBound">
                            <ItemTemplate>

                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="margin-top: 4px; padding-right: 10px; padding-left: 10px; border-bottom: 0px dashed #beb7b7;">


                                    <div style="width: 100%; float: left; height: auto; background-color: #ffffff; border-radius: 5px; box-shadow: rgba(0, 0, 0, 0.16) 0px 1px 4px; margin: 5px 0px 5px 0px;">

                                        <div style="width: 20%; height: 30px; padding: 0px; float: left;">
                                            <asp:Label ID="Label3" runat="server" Text='<%#Bind("month") %>' Style="font-size: 17px; width: 100%; float: left; text-align: center; background: #F34336; color: #fff; padding: 4px 0px 4px 0px; border-radius: 6px 0px 0px 0px;"></asp:Label>
                                            <asp:Label ID="Label1" runat="server" Text='<%#Bind("day") %>' Style="font-size: 30px; float: left; width: 100%; text-align: center; margin: 14px 0px 0px 0px;"></asp:Label>

                                            <br />
                                           
                                        </div>

                                        <div style="position: relative; width: 80%; height: auto; padding: 5px 5px 5px 10px; float: left; border-left: 1px solid #c9c9c9;">
                                            <asp:Panel ID="pnl_link" runat="server">
                                                <asp:Label ID="lbl_link" runat="server" Text='<%#Bind("Link") %>' Visible="false"></asp:Label>
                                                <a href="<%#Eval("Link") %>" target="_blank" style="position: absolute; right: 5px; top: 5px; font-size: 15px;"><i class="fa fa-external-link" aria-hidden="true"></i></a>
                                            </asp:Panel>

                                            <asp:Label ID="lbl_sunjects" runat="server" Text='<%#Bind("Subject") %>' Style="font-size: 15px; font-size: 15px; width: 100%; float: left; color: #000; padding: 0px 20px 1px 0px; border-bottom: 1px solid #f18d8d;"></asp:Label>

                                            <asp:Label ID="lbl_notice1" runat="server" Text='<%#Bind("Details") %>' Style="line-height: 25px; font-size: 14px; margin: 0px 0px 5px 0px; float: left; width: 100%; min-height: 50px;"
                                                 ></asp:Label>
                                            <asp:Label ID="lbl_notice2" runat="server" Style="line-height: 25px; font-size: 14px; margin: 0px 0px 5px 0px; float: left; width: 100%; min-height: 50px; padding: 0px 20px 0px 0px"></asp:Label>
                                            <br />
                                           

                                            <asp:Label ID="lbl_Attachments" runat="server" Text='<%#Bind("Attachments") %>' Visible="false"></asp:Label>
                                            <asp:Panel ID="Panel1" runat="server" Style="display: block;">
                                                <a href='<%#Eval("Attachments") %>' target="_blank" download style="display: block; padding: 0px 0px 0px 0px; font-size: 18px; color: #0066CC; text-decoration: none; float: right;"><i class="fa fa-download" aria-hidden="true"></i></a>
                                            </asp:Panel>


                                            <p style="margin: 5px 0px -2px 0px; padding: 0px; width: 100%; float: left; font-size: 13px; color: #1804ff;">
                                                Uploaded Date : <%#Eval("Date") %>
                                            </p>
                                             <p style="margin: 5px 0px -2px 0px; padding: 0px; width: 100%; float: left; font-size: 13px; color: #1804ff;">
                                                Post By : <%#Eval("Message_By") %>
                                            </p>
                                        </div>

                                    </div>



                                </div>




                            </ItemTemplate>
                        </asp:Repeater>




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
