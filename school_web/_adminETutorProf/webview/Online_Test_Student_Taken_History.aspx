<%@ Page Title="" Language="C#" MasterPageFile="~/_adminETutorProf/webview/Site1.Master" AutoEventWireup="true" CodeBehind="Online_Test_Student_Taken_History.aspx.cs" Inherits="school_web._adminETutorProf.webview.Online_Test_Student_Taken_History" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
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
    </style>
    <link href="../../Autocomplete/jquery-ui.css" rel="stylesheet" />
    <script src="../../Autocomplete/jquery-ui.js"></script>
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
        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" style="padding-right: 5px; padding-left: 5px;">
            <p class="textcont1 ">Class</p>
        </div>
        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9" style="padding-right: 5px; padding-left: 5px;">
            <p class="textcont3">
                <asp:DropDownList ID="ddl_class" runat="server" CssClass="form-control"  AutoPostBack="true" OnSelectedIndexChanged="ddl_class_SelectedIndexChanged">
                </asp:DropDownList>
            </p>
        </div>
        <div class="clearfix"></div>
        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" style="padding-right: 5px; padding-left: 5px;">
            <p class="textcont1 ">Section</p>
        </div>
        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9" style="padding-right: 5px; padding-left: 5px;">
            <p class="textcont3">
                <asp:DropDownList ID="ddl_section" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </p>
        </div>
        <div class="clearfix"></div>

        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" style="padding-right: 5px; padding-left: 5px;">
            <p class="textcont1 ">Exam Date  </p>
        </div>
        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9" style="padding-right: 5px; padding-left: 5px;">
            <p class="textcont3" style="position: relative">
                <asp:TextBox ID="txt_date" runat="server" CssClass="calender-icon"></asp:TextBox>
                <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
            </p>
        </div>
        
         
        <div class="clearfix"></div>
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="text-align: center">
            <asp:Button ID="btn_submit" runat="server" Text="Find" CssClass="btn btn-primary" OnClick="btn_submit_Click" />
        </div>

        <div class="clearfix"></div>
        <div class="texbox-border" style="padding: 0px 5px; overflow: auto">
            <table style="margin: 0px; padding: 0px; width: 100%" border="1">
                            <tr>
                                <td style="padding: 5px;">Total Students
                                </td>

                                <td style="padding: 5px; background-color: #020dea; color: #fff!important;">
                                    <asp:Label ID="lbltotal_student" Font-Bold="true" runat="server">0</asp:Label>
                                </td>
                                <td style="padding: 5px;">Total Present Students
                                </td>

                                <td style="padding: 5px; background-color: #009f25; color: #fff!important;">
                                    <asp:Label ID="lbl_persenstudent" Font-Bold="true" runat="server">0</asp:Label>
                                </td>

                                <td style="padding: 5px;">Total Absent Students
                                </td>

                                <td style="padding: 5px; background-color: #f00; color: #fff!important;">
                                    <asp:Label ID="lbl_totalabsentstudent" Font-Bold="true" runat="server">0</asp:Label>
                                </td>
                              
                            </tr>
                        </table>
                 <table id="datatable" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                <thead>
                                                    <tr>
                                                        <th>SL.No. </th>
                                                       
                                                        <th>Name</th>
                                                         <th>Adm. No.</th>
                                                        <th>Roll No.</th>
                                                         <th>Section</th>
                                                        <th>Test Name</th>
                                                       
                                                        <th>Status</th>
                                                         <th>Date</th>
                                                        <th>Full Marks</th>
                                                         <th>Obten Marks</th>
                                                        <th style="display:none">

                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="RPDetails" runat="server" OnItemDataBound="RPDetails_ItemDataBound">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                </td>
                                                                
                                                               


                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>

                                                                </td>
                                                                 <td style="text-align: left;">
                                                                    <asp:Label ID="lbl_Adm_No" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="lbl_course" Visible="false" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                     <asp:Label ID="lbl_roll_no"  runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                </td>


                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="lbl_Section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                </td>

                                                                 <td style="text-align: left;">
                                                                    <asp:Label ID="lbl_Exam_name" runat="server" Text='<%#Bind("Exam_name")%>'></asp:Label>
                                                                </td>

                                                                

                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="lbl_status" runat="server" Text='<%#Bind("attendance_status")%>'></asp:Label>
                                                                </td>
                                                               


                                                              <td style="text-align: left;">
                                                                    <asp:Label ID="lbl_live_date_one" runat="server" Text='<%#Bind("live_date_one")%>'></asp:Label>
                                                                </td>

                                                              <td style="text-align: left;">
                                                                    <asp:Label ID="lbl_Full_Marks" runat="server" Text='<%#Bind("Full_Marks")%>'></asp:Label>
                                                                </td>


                                                                 <td style="text-align: left;">
                                                                    <asp:Label ID="lbl_Obtains_Marks" runat="server" Text='<%#Bind("Obtains_Marks")%>'></asp:Label>
                                                                </td>


                                                                <td style="text-align:center;display:none">
                                                                <asp:Panel ID="Panel2" runat="server">

                                                                      <a class="btn btn-info" href="../../Online_Test_admin/Result_viewdetails.aspx?studentid=<%#Eval("admissionserialnumber") %>&testid=<%#Eval("Test_code") %>&Attemptid=<%#Eval("Attempt_id")%>&open=teach"    target="_blank"><i class='bx bx-notepad'></i><span>Result View</span></a>
                                                                    </asp:Panel>
                                                                </td>
                                                                
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>

        </div>
    </div>
</asp:Content>
