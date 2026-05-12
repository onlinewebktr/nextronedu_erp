<%@ Page Title="" Language="C#" MasterPageFile="~/Student_Profile/main.Master" AutoEventWireup="true" CodeBehind="home-work.aspx.cs" Inherits="school_web.Student_Profile.home_work" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Home Work
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


            <div class="main-card mb-3 card">
                <div class="card-header">
                    <h4 class="card-title">Home Work</h4>
                </div>
                <div class="card-body" style="padding-top: 0px;">
                    <div class="headingtablee">
                        <div class="row">
                            <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                                <label for="validationCustom01" class="lebelheadpp">Teacher</label>
                                <asp:DropDownList ID="ddl_teacher" runat="server" class="form-control form-control-custom" AutoPostBack="true" OnSelectedIndexChanged="ddl_teacher_SelectedIndexChanged"></asp:DropDownList>
                            </div>

                            <div class="col-lg-2 col-width-50 pads-rght-5">
                                <label for="validationCustom01" class="lebelheadpp">Subject Name</label>
                                <asp:DropDownList ID="ddl_subject" runat="server" class="form-control form-control-custom" AutoPostBack="true" OnSelectedIndexChanged="ddl_subject_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <div class="col-lg-2 col-width-50 pads-lft-5">
                                <label for="validationCustom01" class="lebelheadpp">Date</label>
                                <div class="clndr-dv-wpr" style="position: relative;">
                                    <asp:TextBox ID="txt_date" runat="server" class="form-control"></asp:TextBox>
                                    <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                </div>
                            </div>

                            <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                                <asp:Button ID="btn_find" runat="server" Text="Find" class="mt-2 btn btn-primary fnd-btnmrgn" OnClick="btn_find_Click" />
                            </div>
                        </div>

                        <div class="row">
                            <asp:Repeater ID="rd_view" runat="server">
                                <ItemTemplate>
                                    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                        <div class="std-mt-dv">
                                            <a href="homework-details.aspx?topicid=<%#Eval("Home_Work_id") %>">
                                                <div class="std-mt-date-sec">
                                                    <p class="std-mt-date-mnth"><%#Eval("Month_name") %></p>
                                                    <p class="std-mt-date-day"><%#Eval("Day_name") %></p>
                                                </div>

                                                <div class="std-mt-cont-sec">
                                                    <h2 class="std-mt-cont-sub"><%#Eval("Subject_name") %> By <%#Eval("Upload_by_name") %></h2>
                                                    <p class="std-mt-cont-desc"><%#Eval("Topic") %></p>
                                                    <p class="std-mt-cont-upload-date">Upload Date : <%#Eval("Upload_Date") %></p>
                                                </div>
                                            </a>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
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
            $("#<%=txt_date.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "1900:2100"
            });
        });
    </script>

    <link href="assets/css/calender-modified.css" rel="stylesheet" />
</asp:Content>
