<%@ Page Title="" Language="C#" MasterPageFile="~/Student_Profile/webview/Site1.Master" AutoEventWireup="true" CodeBehind="Online_Exam_List.aspx.cs" Inherits="school_web.Student_Profile.webview.Online_Exam_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Upcoming/Running Exam
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
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
            text-align: left!important;
        }
         .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            border-color: rgb(255 255 255 / 20%);
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="fullinfo">
        <div id="notification">
            <div id="pan" class="notificationpan">
                <div style="float: left; width: 100%; height: auto;">
                    <asp:Label ID="lbl_msg" runat="server" Font-Bold="True" ForeColor="White"></asp:Label>
                </div>
                <img src="../images/close.png" onclick="$(function () { $('.notificationpan').show().slideUp(1000);});"
                    class="closenotificationpan" alt="" />
            </div>
        </div>

        <div class="clearfix"></div>
        <div class="texbox-border" style="padding: 0px 5px; overflow: auto">
            <div style="padding: 5px 10px 5px 10px; width: 100%; float: left; text-align: center;">
                <asp:Label ID="lbl_msg2" runat="server" Font-Bold="True" ForeColor="Red" Style="font-size: 18px;"></asp:Label>
            </div>

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
                                    <asp:Label ID="lbl_subjectview" Font-Bold="true" runat="server" ></asp:Label>
                                </td>

                            </tr>




                            <tr >
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
                                                    <asp:Label ID="lbl_Status" Font-Bold="true" ForeColor="Maroon" runat="server" Text='<%#Bind("Status") %>'   ></asp:Label>
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
                                    
                                  
                                    <asp:Button ID="btn_view_test" runat="server" Text="Give Test" Style="width: 90px; margin: 5px 5px 5px 10px;" OnClick="btn_view_test_Click" CssClass="btn btn-info" />
                                    <asp:Button ID="btn_view_result" runat="server" Text="View Result" CssClass="btn btn-info" Style="width: 100px; margin: 5px 5px 5px 10px;" OnClick="btn_view_result_Click" Visible="false" />
                                    <asp:Label ID="lbl_section" Visible="false" Font-Bold="true" runat="server" Text='<%#Bind("Section") %>'></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

        </div>
    </div>
</asp:Content>
