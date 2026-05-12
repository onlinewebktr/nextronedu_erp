<%@ Page Title="" Language="C#" MasterPageFile="~/LMS_VC_Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Add_Class_Routine.aspx.cs" Inherits="school_web.LMS_VC_Admin.Add_Class_Routine" %>

<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Set Class Routine
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .table-bordered th {
            border: 1px solid #e9ecef;
            font-size: 13px;
        }

        .table-bordered td {
            border: 1px solid #e9ecef;
            font-size: 14px;
            vertical-align: middle!important;
        }

        .notificationpan {
            background-color: rgb(255, 76, 76);
            position: fixed;
            top: 70px;
            right: 10px;
            padding: 10px 10px;
            width: 667px!important;
        }

        .calendar, .calendar table {
            border: 1px solid #556;
            font-size: 12px;
            color: #000;
            cursor: default;
            background: #eef;
            font-family: tahoma,verdana,sans-serif;
            z-index: 99999999!important;
        }
    </style>
    <script language="Javascript">
       <!--
    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode != 46 && charCode > 31
          && (charCode < 48 || charCode > 57))
            return false;

        return true;
    }
    //-->
    </script>
    <style>
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

        .app-wrapper-footer {
            display: none;
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
                                    <i class="pe-7s-menu icon-gradient bg-mean-fruit"></i>
                                </div>
                                <div>
                                    <asp:Literal ID="ltUsertop" runat="server">Set Class Routine</asp:Literal>
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
                    <asp:HiddenField ID="hd_regid" runat="server" />
                    <div class="row">

                        <div class="col-lg-12">
                            <div class="main-card mb-3 card">
                                <div class="card-body">
                                    <table class="tab-content table table-bordered">
                                        <tr>
                                            <td style="padding: 10px 10px 10px 10px; font-weight: bold;">Session
                                            </td>
                                            <td style="padding: 10px 10px 10px 10px;">
                                                <asp:DropDownList ID="ddl_session" Style="width: 118px!important;" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </td>
                                            <td style="padding: 10px 10px 10px 10px; font-weight: bold">Day 
                                            </td>
                                            <td style="padding: 10px 10px 10px 10px">
                                                <asp:DropDownList ID="ddlday" runat="server" Style="width: 200px!important;" CssClass="form-control" >

                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Monday</asp:ListItem>
                                                    <asp:ListItem>Tuesday</asp:ListItem>
                                                    <asp:ListItem>Wednesday</asp:ListItem>
                                                    <asp:ListItem>Thursday</asp:ListItem>
                                                    <asp:ListItem>Friday</asp:ListItem>
                                                    <asp:ListItem>Saturday</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>

                                            <td style="padding: 10px 10px 10px 10px; font-weight: bold">Class Name
                                            </td>
                                            <td style="padding: 10px 10px 10px 10px">
                                                <asp:DropDownList ID="ddl_class" runat="server" Style="width: 100px!important;" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_class_SelectedIndexChanged"></asp:DropDownList>
                                            </td>
                                            <td style="padding: 10px 10px 10px 10px; font-weight: bold">Section 
                                            </td>
                                            <td style="padding: 10px 10px 10px 10px; font-weight: bold">
                                                <asp:DropDownList ID="ddl_section" runat="server" Style="width: 150px!important;" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_section_SelectedIndexChanged"></asp:DropDownList>
                                            </td>
                                            <td style="padding: 10px 10px 10px 10px; font-weight: bold">Subject 
                                            </td>
                                            <td style="padding: 10px 10px 10px 10px">
                                                <asp:DropDownList ID="ddl_subject" runat="server" Style="width: 200px!important;" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_subject_SelectedIndexChanged"></asp:DropDownList>
                                            </td>



                                        </tr>

                                        <tr id="add" runat="server" visible="false">
                                            <td colspan="9">
                                                <table class="tab-content table table-bordered" style="margin: 20px 0px 0px 0px;">
                                                    <tr>
                                                        <td style="padding: 5px; font-weight: bold">Class Period</td>
                                                        <td style="padding: 5px; font-weight: bold">Day</td>
                                                        <td style="padding: 5px; font-weight: bold">Starting Time</td>
                                                        <td style="padding: 5px; font-weight: bold">End Time</td>

                                                    </tr>
                                                    <tr>
                                                        <td style="padding: 5px;">
                                                            <asp:DropDownList ID="ddl_period" runat="server" Style="width: 200px!important;" CssClass="form-control" OnSelectedIndexChanged="ddl_period_SelectedIndexChanged" AutoPostBack="true">
                                                            </asp:DropDownList>



                                                        </td>
                                                        <td style="padding: 5px;">
                                                            <asp:DropDownList ID="ddl_day" runat="server" Style="width: 200px!important;" CssClass="form-control" Enabled="false">
                                                                <asp:ListItem>Select</asp:ListItem>
                                                                <asp:ListItem>Monday</asp:ListItem>
                                                                <asp:ListItem>Tuesday</asp:ListItem>
                                                                <asp:ListItem>Wednesday</asp:ListItem>
                                                                <asp:ListItem>Thursday</asp:ListItem>
                                                                <asp:ListItem>Friday</asp:ListItem>
                                                                <asp:ListItem>Saturday</asp:ListItem>
                                                            </asp:DropDownList>



                                                        </td>

                                                        <td style="padding: 5px; vertical-align: middle!important;">
                                                            <asp:Label ID="lbl_starttime" runat="server"></asp:Label>

                                                        </td>
                                                        <td style="padding: 5px; vertical-align: middle!important;">
                                                            <asp:Label ID="lbl_endtime" runat="server"></asp:Label>

                                                        </td>



                                                    </tr>
                                                </table>




                                            </td>
                                            <td>
                                                <asp:Button ID="btn_Add" runat="server" CssClass="mt-2 btn btn-primary" Text="Add" Style="width: 176px; height: 37px; margin-top: 53px!important; float: left;"
                                                    OnClick="btn_Add_Click" />
                                            </td>


                                        </tr>

                                    </table>

                                    <div runat="server" visible="false" id="grid111">
                                        <table style="width: 100%;" class="table table-hover table-striped table-bordered">
                                            <thead>
                                                <tr>
                                                    <th>Sl. No.</th>
                                                    <th>Session</th>
                                                    <th>Class</th>
                                                    <th>Section</th>
                                                    <th>Subject</th>
                                                    <th>Class Period</th>
                                                    <th>Day</th>
                                                    <th>Starting Time</th>
                                                    <th>End Time </th>

                                                    <th>Action</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="RPDetails" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="lbl_Session" runat="server" Font-Names="Arial" Text='<%#Bind("Session") %>'></asp:Label>
                                                            </td>

                                                            <td>
                                                                <asp:Label ID="lbl_classname" runat="server" Font-Names="Arial" Text='<%#Bind("Classname") %>'></asp:Label>
                                                            </td>

                                                            <td>
                                                                <asp:Label ID="lbl_section" runat="server" Font-Names="Arial" Text='<%#Bind("Section") %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbl_subject" runat="server" Font-Names="Arial" Text='<%#Bind("subjectname") %>'></asp:Label>
                                                            </td>

                                                            <td>
                                                                <asp:Label ID="lbl_Class_period" runat="server" Font-Names="Arial" Text='<%#Bind("Class_period") %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbl_Day" runat="server" Font-Names="Arial" Text='<%#Bind("Day") %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblStart_date_time" runat="server" Font-Names="Arial" Text='<%#Bind("Start_Time1") %>'></asp:Label></td>

                                                            <td>
                                                                <asp:Label ID="lbl_End_datetime" runat="server" Font-Names="Arial" Text='<%#Bind("End_time1") %>'></asp:Label>
                                                            </td>


                                                            <td>
                                                                <div class="btn-actions-pane-right text-capitalize actions-icon-btn">
                                                                    <div class="btn-group dropdown">
                                                                        <button type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" class="btn-icon btn-icon-only btn btn-link">
                                                                            <i class="pe-7s-menu btn-icon-wrapper"></i>
                                                                        </button>
                                                                        <div tabindex="-1" role="menu" aria-hidden="true" class="dropdown-menu-right rm-pointers dropdown-menu-shadow dropdown-menu-hover-link dropdown-menu">
                                                                            <asp:LinkButton ID="lnk_edit" runat="server" CssClass="dropdown-item" OnClick="lnk_edit_Click" CausesValidation="false">Edit</asp:LinkButton>
                                                                            <asp:LinkButton ID="lnkDel" runat="server" CssClass="dropdown-item" OnClick="lnkDel_Click" OnClientClick="return confirm('Are you sure want to delete this?');" CausesValidation="false">Delete</asp:LinkButton>

                                                                            <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id") %>' Visible="false"></asp:Label>
                                                                            <asp:Label ID="lbl_Session_id" runat="server" Text='<%#Bind("Session_id") %>' Visible="false"></asp:Label>
                                                                            <asp:Label ID="lbl_Class_id" runat="server" Text='<%#Bind("Class_id") %>' Visible="false"></asp:Label>
                                                                            <asp:Label ID="lbl_Subject_id" runat="server" Text='<%#Bind("Subject_id") %>' Visible="false"></asp:Label>

                                                                        </div>
                                                                    </div>
                                                                </div>
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
                </div>
                <asp:HiddenField ID="hd_id" runat="server" />
                <asp:HiddenField ID="hd_subjectid" runat="server" />
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
</asp:Content>
