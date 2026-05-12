<%@ Page Title="" Language="C#" MasterPageFile="~/Student_Profile/main.Master" AutoEventWireup="true" CodeBehind="school-calendar.aspx.cs" Inherits="school_web.Student_Profile.school_calendar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    School Calendar
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
                    <h4 class="card-title">School Calendar</h4>
                </div>
                <div class="card-body" style="padding-top: 0px;">
                    <div class="headingtablee">
                        <div class="row">
                            <div class="col-lg-2 col-width-50 pads-rght-5">
                                <label for="validationCustom01" class="lebelheadpp">Month</label>
                                <asp:DropDownList ID="ddl_month" runat="server" class="form-control form-control-custom"></asp:DropDownList>
                            </div> 
                            <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                                <asp:Button ID="btn_find" runat="server" Text="Find" class="mt-2 btn btn-primary fnd-btnmrgn" OnClick="btn_submit_Click" />
                            </div>
                        </div>



                        <div class="row">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <div class="texbox-border"> 
                                    <p style="text-align: center; border: 1px solid #000; padding: 5px 0px 8px 0px; margin: 0px; background: #0074ff; width: 100%; color: #141212; float: left; text-align: center">
                                        <asp:Label ID="Label1" Style="font-size: 12px;" runat="server" Font-Bold="True" Font-Size="Medium"
                                            ForeColor="#000">School Calendar List
                                                        <asp:Label ID="lbl_session" runat="server"></asp:Label></asp:Label><br /> 
                                    </p>
                                    <div class="wrapper">
                                        <div class="scrollbar" id="style-2">
                                            <div class="force-overflow">
                                                <asp:Calendar ID="Calendar1" runat="server" OnDayRender="Calendar1_DayRender" SelectionMode="None" ShowTitle="False" ShowGridLines="True" CellSpacing="1" ShowNextPrevMonth="false" Style="width: 100%; margin: 0px;">
                                                    <WeekendDayStyle BorderColor="#CC9900" Wrap="True" />
                                                </asp:Calendar> 
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
</asp:Content>
