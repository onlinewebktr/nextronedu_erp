<%@ Page Title="" Language="C#" MasterPageFile="~/_adminETutorProf/webview/Site1.Master" AutoEventWireup="true" CodeBehind="Student_List_For_Edit.aspx.cs" Inherits="school_web._adminETutorProf.webview.Student_List_For_Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Edit Student List 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="../../assets/css/responsiveDatatable.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="fullinfo">

        <div id="notification">
            <div id="pan" class="notificationpan">
                <div style="float: left; width: 100%; height: auto;">
                    <asp:Label ID="lbl_msg" runat="server" Font-Bold="True" ForeColor="White"></asp:Label>
                </div>
                 
                <img src="../../images/close.png" onclick="$(function () { $('.notificationpan').show().slideUp(1000);});"
                    class="closenotificationpan" alt="" />
            </div>
        </div>

        <div class="clearfix"></div>
        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" style="padding-right: 5px; padding-left: 5px;">
            <p class="textcont1 ">Class</p>
        </div>
        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9" style="padding-right: 5px; padding-left: 5px;">
            <p class="textcont3">
                <asp:DropDownList ID="ddl_class" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_class_SelectedIndexChanged" AutoPostBack="true">
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





        <div class="clearfix"></div>
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="text-align: center">
            <asp:Button ID="btn_find" runat="server" Text="Find" CssClass="btn btn-primary" OnClick="btn_find_Click" />
        </div>

        <div class="clearfix"></div>
        <div class="texbox-border" style="padding: 0px 5px; overflow: auto">
            <table id="datatable" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                <thead>
                    <tr>
                        <th>#</th>
                        <th>Action</th>
                        <th>Session</th>
                        <th>Admission No.</th>
                        <th>Class</th>
                        <th>Section</th>
                        <th>Roll No.</th>
                        <th>Student Name</th>
                        <th>DOB</th>

                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rd_view" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkEdit" class="dropdown-item" runat="server" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Edit" Style="color: #ff3030; font-weight: bold;"> <span>Edit Student Details</span></asp:LinkButton>
                                    <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                    <asp:Label ID="lbl_session_id" runat="server" Text='<%#Bind("Session_id")%>' Visible="false"></asp:Label>
                                </td>
                                <td style="text-align: left;">
                                    <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                </td>
                                <td style="text-align: left;">
                                    <asp:Label ID="lbl_admissionserialnumber" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                </td>

                                <td style="text-align: left;">
                                    <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                </td>
                                <td style="text-align: left;">
                                    <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                </td>
                                <td style="text-align: left;">
                                    <asp:Label ID="Label1" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                </td>
                                <td style="text-align: left;">
                                    <asp:Label ID="Label2" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                </td>
                                <td style="text-align: left;">
                                    <asp:Label ID="lbl_dob" runat="server" Text='<%#Bind("dob")%>'></asp:Label>
                                </td>

                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>

        </div>
    </div>
</asp:Content>
