<%@ Page Title="" Language="C#" MasterPageFile="~/InstructorProfile/Teacher_Profile.Master" AutoEventWireup="true" CodeBehind="Student_view_remarks.aspx.cs" Inherits="school_web.InstructorProfile.Student_view_remarks" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">View Remaks
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .table-bordered th {
            border: 1px solid #e9ecef;
            font-size: 13px;
        }

        .table-bordered td {
            border: 1px solid #e9ecef;
            font-size: 16px;
        }

        .notificationpan {
            background-color: rgb(255, 76, 76);
            position: fixed;
            top: 70px;
            right: 10px;
            padding: 10px 10px;
            width: 667px!important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div class="app-main__inner">
            <div class="app-page-title">
                <div class="page-title-wrapper">
                    <div class="page-title-heading">
                        <div class="page-title-icon">
                            <i class="pe-7s-menu icon-gradient bg-mean-fruit"></i>
                        </div>
                        <div>
                            <asp:Literal ID="ltUsertop" runat="server">Subjective Paper Marks List</asp:Literal>
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
                                    <td style="padding: 10px 10px 10px 10px; font-weight: bold">Session
                                    </td>
                                    <td style="padding: 10px 10px 10px 10px; font-weight: bold">Tracking Head for Exam
                                    </td>
                                    
                                    <td style="padding: 10px 10px 10px 10px; font-weight: bold">Class 
                                    </td>
                                    <td style="padding: 10px 10px 10px 10px; font-weight: bold">Section 
                                    </td>
                                   
                                    <td style="padding: 10px 10px 10px 10px; font-weight: bold"></td>


                                </tr>

                                <tr>
                                    <td style="padding: 10px 10px 10px 10px">
                                        <asp:DropDownList ID="ddl_session" Style="width: 100px!important;" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_session_SelectedIndexChanged"></asp:DropDownList>
                                    </td>


                                    <td style="padding: 10px 10px 10px 10px">
                                        <asp:DropDownList ID="ddl_exam_Tracking" Style="width: 118px!important;" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_exam_Tracking_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </td>

                                    
                                    <td style="padding: 10px 10px 10px 10px">
                                        <asp:DropDownList ID="ddl_class" runat="server" Style="width: 100px!important;" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_class_SelectedIndexChanged"></asp:DropDownList>
                                    </td>

                                    <td style="padding: 10px 10px 10px 10px; font-weight: bold">
                                        <asp:DropDownList ID="ddl_section" runat="server" Style="width: 100px!important;" CssClass="form-control" ></asp:DropDownList>
                                    </td>


                                    
                                    <td style="padding: 10px 10px 10px 10px">
                                        <asp:Button ID="btn_find" runat="server" Text="Find" class="mt-2 btn btn-primary" OnClick="btn_find_Click1" ValidationGroup="a" Style="float: right" />

                                        <asp:ImageButton ID="imgexcel2" runat="server" Visible="false" ImageUrl="~/images/excel_con.png" CssClass="excelbutton22" OnClick="imgexcel2_Click"
                                            Style="height: 31px; width: 32px; margin-top: 1px; margin: 8px 0px 0px 13px;" />

                                    </td>
                                </tr>

                            </table>

                            <div runat="server" visible="false" id="grid111">
                                <asp:GridView ID="GrdView" runat="server" class="table table-hover table-striped table-bordered" AutoGenerateColumns="False" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl. No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Student Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_FullName" runat="server" Text='<%#Bind("FullName")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Admission No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_reg_id" runat="server" Text='<%#Bind("reg_id")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Roll No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_roll_no" runat="server" Text='<%#Bind("roll_no")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Remarks" runat="server" Text='<%#Bind("Remarks")%>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        
                                    </Columns>
                                </asp:GridView>






                            </div>




                        </div>
                    </div>



                </div>

            </div>
        </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
