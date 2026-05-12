<%@ Page Title="" Language="C#" MasterPageFile="~/LMS_VC_Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Student_topic_readout_details.aspx.cs" Inherits="school_web.LMS_VC_Admin.Student_topic_readout_details" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Student Learning Report
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="app-main__inner">
        <div class="app-page-title">
            <div class="page-title-wrapper">
                <div class="page-title-heading">
                    <div class="page-title-icon">
                        <i class="pe-7s-users icon-gradient bg-mean-fruit"></i>
                    </div>
                    <div>
                        <asp:Literal ID="ltUsertop" runat="server">Student Learning Report</asp:Literal>

                    </div>
                </div>
                <div class="page-title-actions">
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
                            <div class="col-md-2 col-sm-12">
                                <div class="position-relative form-group">
                                    <label>Class</label>
                                    <asp:DropDownList ID="ddl_CourseCat" class="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_CourseCat_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3 col-sm-12">
                                <div class="position-relative form-group">
                                    <label>Subject</label>
                                    <asp:DropDownList ID="ddl_Course" class="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_Course_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-4 col-sm-12">
                                <div class="position-relative form-group">
                                    <label>Lesson</label>
                                    <asp:DropDownList ID="ddlSectionname" class="form-control" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-1 col-sm-12">
                                <asp:Button ID="Btn_Find" runat="server" OnClick="Btn_Find_Click" class="btn  btn-primary" Text="Find" Style="margin-top: 30px;" />
                            </div>
                            <div class="col-md-2 col-sm-12">
                                <asp:Label ID="lbl_total_student" runat="server"></asp:Label>
                            </div>
                        </div>
                        <hr />
                        <asp:ImageButton ID="img_expor_excel" OnClick="img_expor_excel_Click" runat="server" ImageUrl="~/images/excel_con.png" Style="height: 20px; width: 20px; display: none" />
                        <table style="width: 100%;" id="example" class="table table-hover table-striped table-bordered">
                            <thead>
                                <tr>
                                    <th>Sl No.</th>
                                    <th>Student Name</th>
                                    <th>Admission No.</th>
                                    <th>Mobile No.</th>
                                    <th>class</th>
                                    <th>Section</th>
                                    <th>Subject</th>
                                    <th>Lesson</th>
                                    <th>Topic</th>
                                    <th>Date</th>
                                    <%--<th>Read Out</th>--%>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="RPDetails" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lbl_studentname" runat="server" Text='<%#Bind("Original_Name") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_admissionserialnumber" runat="server" Text='<%#Bind("admissionserialnumber") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_mobilenumber" runat="server" Text='<%#Bind("mobilenumber") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Section" runat="server" Text='<%#Bind("Section") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_CourseName" runat="server" Font-Names="Arial" Text='<%#Bind("CourseName") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_SetionName" runat="server" Font-Names="Arial" Text='<%#Bind("SetionName") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_TopicName" runat="server" Font-Names="Arial" Text='<%#Bind("TopicName") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Start_at" runat="server" Font-Names="Arial" Text='<%#Bind("Start_at") %>'></asp:Label>
                                                <asp:Label ID="lbl_ReadOut" runat="server" Font-Names="Arial" Text='<%#Bind("ReadOut") %>' Visible="false"></asp:Label>


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
