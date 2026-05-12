<%@ Page Title="" Language="C#" MasterPageFile="~/Student_Profile/main.Master" AutoEventWireup="true" CodeBehind="Test_Info.aspx.cs" Inherits="school_web.Student_Profile.Test_Info" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">Test Info
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .texbox-border {
            margin: 6px 0px 0px 0px;
            padding: 0px;
            height: auto;
            width: 100%;
            border-bottom: 1px solid #fff;
        }

        img {
            /*top: 8px;*/
            position: inherit!important;
            /* left: 100px;*/
        }
    </style>
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
                    <h4 class="card-title">Test Info </h4>
                </div>
                <div class="card-body" style="padding-top: 0px;">
                    <div class="headingtablee">

                        <div style="padding: 5px 10px 5px 10px; width: 100%; float: left; text-align: center;">
                            <asp:Label ID="lbl_msg2" runat="server" Font-Bold="True" ForeColor="Red" Style="font-size: 18px;"></asp:Label>
                        </div>

                        <div class="row">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">

                                <div class="table-responsive">
                                    <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                        <div class="row">
                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                <div class="grd-wpr">
                                                     <div class="row">


                        <div class="col-md-12 col-lg-12 col-sm-12 col-xs-12">
                            <p style="margin-top: 0px;" class="p1">
                                <span style="font-weight: bold">Test Name :</span>
                                <asp:Label ID="lbl_testname" runat="server"></asp:Label>
                                &nbsp;
                            </p>

                            <p style="margin-top: 0px;" class="p1">
                                <span style="font-weight: bold">Subject :</span>
                                <asp:Label ID="lbl_subject" runat="server"></asp:Label>
                                &nbsp;
                            </p>
                        </div>
                        <div class="col-md-12 col-lg-12 col-sm-12 col-xs-12">
                            <p style="margin-top: 0px;" class="p1">
                                <span style="font-weight: bold">Number of questions :</span>
                                <asp:Label ID="lbl_no_of_question" runat="server"></asp:Label>
                                |  Time :
                                                    <asp:Label ID="lbl_tot_time" runat="server"></asp:Label>
                                <asp:Label ID="lbl_tot_time_type" runat="server"></asp:Label>
                            </p>
                        </div>







                        <div class="col-md-12 col-lg-12 col-sm-12 col-xs-12">


                            <p style="margin-top: 0px; margin-bottom: 5px; text-align: left; float: left;"
                                class="p1">
                                <%-- <span style="font-weight: bold">General Info :</span>

                                <asp:Label ID="lbl_generalinfo" runat="server"></asp:Label>
                                <br />--%>

                                <asp:Image ID="img1" runat="server" CssClass="img-responsive" />

                            </p>
                        </div>
                        <div class="col-md-12 col-lg-12 col-sm-12 col-xs-12" style="display: none">
                            <div style="display: none" class="p1">
                                Select Language - 
                                                <asp:DropDownList ID="ddl_language" runat="server">
                                                    <asp:ListItem>English</asp:ListItem>

                                                </asp:DropDownList>
                            </div>
                        </div>
                        <%--<asp:ListItem>Hindi</asp:ListItem>--%>
                        <div class="col-md-12 col-lg-12 col-sm-12 col-xs-12">
                            <%-- <b style="color: red; font-size: 16px;">Note:- Best viewed in Mobile Please your device should be portrait mode.</b>
                            <br />
                            <asp:CheckBox ID="chk" Style="font-size: 16px;" runat="server" Text=" I do hereby declare that I have carefully read and understood the all instructions." Checked="true" />

                            <a id="popup" href='TandC.aspx' style="color: blue; display: none">T&C

                            </a>
                            <br />--%>
                            <div style="text-align: center; width: 100%;">
                                <asp:Button ID="btn_s_add" runat="server"
                                    Height="31px" Text="Start Test" Width="100px" OnClientClick="return confirm('Are you sure want to start test?')" OnClick="btn_s_add_Click" CssClass="btn btn-info" Style="width: 100px!important; height: 36px!important; font-size: 16px; margin: 0px 0px 7px 0px;" />
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
                    </div>
                </div>
            </div>
        </div>
        <!--end row-->
    </div>
       <asp:HiddenField ID="hd_userid" runat="server" />
    <asp:HiddenField ID="hd_testid" runat="server" />
    <asp:HiddenField ID="hd_section" runat="server" />
    <asp:HiddenField ID="hd_entry_id" runat="server" />
    <asp:HiddenField ID="hd_package_id" runat="server" />

    <asp:HiddenField ID="hd_examcategoryid" runat="server" />
</asp:Content>
