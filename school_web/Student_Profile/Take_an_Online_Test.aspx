<%@ Page Title="" Language="C#" MasterPageFile="~/Student_Profile/main.Master" AutoEventWireup="true" CodeBehind="Take_an_Online_Test.aspx.cs" Inherits="school_web.Student_Profile.Take_an_Online_Test" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Take An Online Test 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="../Online_Test_admin/Doc/font-awesome-4.6.3/css/font-awesome.min.css" rel="stylesheet" />
    <style>
        .clndr-icon {
            font-size: 14px !important;
            color: #ff2956;
            position: absolute;
            top: 8px;
            left: 75px;
        }

        .clndr-icon {
            font-size: 11px !important;
            color: #ff2956;
            position: absolute;
            top: 10px;
            right: 3px;
            left: auto;
        }


        .table td {
            text-align: left !important;
        }

        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            border-color: rgb(255 255 255 / 20%);
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
                    <h4 class="card-title">Take An Online Test </h4>
                </div>
                <div class="card-body" style="padding-top: 0px;">
                    <div class="headingtablee">

                        <div style="padding: 5px 10px 5px 10px; width: 100%; float: left; text-align: center;">
                            <asp:Label ID="lbl_msg2" runat="server" Font-Bold="True" ForeColor="Red" Style="font-size: 18px;"></asp:Label>
                        </div>

                        <div class="row">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">

                                <div class="1">
                                    <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                        <div class="row">
                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                <div class="grd-wpr">
                                                    <div id="content">

                                                        <asp:Repeater ID="RPDetails" runat="server" OnItemDataBound="RPDetails_ItemDataBound">
                                                            <ItemTemplate>
                                                                <div class="col-md-4 col-lg-4 col-sm-12 col-xs-12" style="background-color: #eaee4e; border-radius: 25px;">
                                                                    <table class="table" style="width: 100%;">

                                                                        <tr>
                                                                            <td style="padding: 2px 0px 2px 0px"><i class="fa fa-check-square-o" aria-hidden="true" style="color: #000"></i>&nbsp;Exam Name :  
                                 <asp:Label ID="lbl_examname" Font-Bold="true" runat="server" Text='<%#Bind("Exam_name") %>'></asp:Label>
                                                                            </td>

                                                                        </tr>




                                                                        <tr>
                                                                            <td style="padding: 2px 0px 2px 0px"><i class="fa fa-check-square-o" aria-hidden="true" style="color: #000"></i>&nbsp;Subject :  
                                 <asp:Label ID="lbl_subject" Font-Bold="true" runat="server" Visible="false" Text='<%#Bind("subject_name") %>'></asp:Label>
                                                                                <asp:Label ID="lbl_subjectview" Font-Bold="true" runat="server"></asp:Label>
                                                                            </td>

                                                                        </tr>




                                                                        <tr>
                                                                            <td style="padding: 2px 0px 2px 0px"><i class="fa fa-calendar" aria-hidden="true" style="color: #000"></i>&nbsp;Exam Date :  
                                                     <asp:Label ID="lbltestdate" Font-Bold="true" runat="server" Text='<%#Bind("live_date1") %>'></asp:Label>
                                                                            </td>

                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding: 2px 0px 2px 0px"><i class="fa fa-clock-o" aria-hidden="true" style="color: #000"></i>&nbsp;Exam Start Time :  
                                                     <asp:Label ID="lbl_Exam_starttime" Font-Bold="true" runat="server" Text='<%#Bind("live_time1") %>'></asp:Label>
                                                                            </td>

                                                                        </tr>

                                                                        <tr>
                                                                            <td style="padding: 2px 0px 2px 0px">
                                                                                <i class="fa fa-clock-o" aria-hidden="true" style="color: #000"></i>&nbsp;Exam link validation till 
                                                                            
                                    <asp:Label ID="lbl_TestDate_dis" Font-Bold="true" runat="server" Text='<%#Bind("End_date") %>'></asp:Label>
                                                                                <asp:Label ID="lbl_TestDate" runat="server" Text='<%#Bind("Exam_endtime") %>'></asp:Label>






                                                                            </td>
                                                                        </tr>

                                                                        <tr>
                                                                            <td style="padding: 2px 0px 2px 0px">
                                                                                <i class="fa fa-clock-o" aria-hidden="true" style="color: #000"></i>
                                                                                <asp:Label ID="lbl_Duration" runat="server" Font-Bold="true" Text='<%#Bind("Exam_duration") %>'></asp:Label>
                                                                                <asp:Label ID="lbl12" runat="server" Font-Bold="true" Visible="false">(In minutes)</asp:Label>
                                                                            </td>
                                                                        </tr>



                                                                        <tr>
                                                                            <td style="padding: 2px 0px 2px 0px"><i class="fa fa-check-square-o" aria-hidden="true" style="color: #000"></i>&nbsp;Status :  
                                                    <asp:Label ID="lbl_tackstatus" Font-Bold="true" ForeColor="Maroon" runat="server"></asp:Label>
                                                                            </td>

                                                                        </tr>

                                                                        <tr>
                                                                            <td style="padding: 2px 0px 2px 0px"><i class="fa fa-check-square-o" aria-hidden="true" style="color: #000"></i>&nbsp;Status :  
                                                    <asp:Label ID="lbl_Status" Font-Bold="true" ForeColor="Maroon" runat="server" Text='<%#Bind("Status") %>'></asp:Label>
                                                                            </td>

                                                                        </tr>

                                                                        <tr>
                                                                            <td>

                                                                                <asp:Label ID="lbl_testid" runat="server" Text='<%#Bind("Exam_id") %>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lbl_Exam_Id" runat="server" Text='<%#Bind("Exam_id") %>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lbl_Class_Id" runat="server" Text='<%#Bind("Class_id") %>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lbl_Sub_id" runat="server" Text='1' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lbl_examendatetime" runat="server" Text='<%#Bind("live_date") %>' Visible="false"></asp:Label>

                                                                                <asp:Label ID="lbl_End_Exam_Idate" runat="server" Text='<%#Bind("End_Exam_Idate") %>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lbl_Exam_Start_Idate" runat="server" Text='<%#Bind("Exam_Start_Idate") %>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lbl_Exam_intendtime" runat="server" Text='<%#Bind("Exam_intendtime") %>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lbl_Exam_intstarttime" runat="server" Text='<%#Bind("Exam_intstarttime") %>' Visible="false"></asp:Label>


                                                                                <asp:Button ID="btn_view_test" runat="server" Text="Give Test" Style="width: 90px; margin: 5px 5px 5px 10px; background-color: #1d8cf8; color: #fff;"
                                                                                    OnClick="btn_view_test_Click" CssClass="btn btn-info" />
                                                                                <asp:Button ID="btn_view_result" runat="server" Text="View Result" CssClass="btn btn-info" Style="width: 100px; margin: 5px 5px 5px 10px; background-color: #1d8cf8; color: #fff;"
                                                                                    OnClick="btn_view_result_Click" Visible="false" />
                                                                                <asp:Label ID="lbl_section" Visible="false" Font-Bold="true" runat="server" Text='<%#Bind("Section") %>'></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
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
</asp:Content>
