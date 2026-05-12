<%@ Page Title="" Language="C#" MasterPageFile="~/InstructorProfile/Teacher_Profile.Master" AutoEventWireup="true" CodeBehind="Assined_Class_And_Subject.aspx.cs" Inherits="school_web.InstructorProfile.Assined_Class_And_Subject" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Assined Subject & Events
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        h1, h2, h3, h4, h5, h6 {
            font-weight: 400;
            margin: 0px 0px!important;
            
        }

        .h4 {
            font-weight: 400;
            margin: 0px 0px!important;
            border-bottom: 1px solid #ccc5c5;
        }

        .page-head {
            margin: 5px 0px 5px 12px;
            width: 100%;
        }

        .block {
            margin: 0px;
            width: 250px;
            height: auto;
            padding: 5px;
            box-sizing: border-box;
            text-align: center;
            /*background: #F2F2F2;
            border: 1px solid #ccc;*/
            display: inline-block;
            border-radius: 4px;
            -webkit-border-radius: 4px;
            -moz-border-radius: 4px;
        }

        #content table th, #content table td {
            padding: 0px 0px!important;
            border: none!important;
        }

        #content table {
            border: 1px none #cfcfcf;
        }

        .block a {
            width: 100%;
            text-align: center;
            text-decoration: none;
            padding: 5px;
            font-size: 14px;
        }



            .block a .fa {
                font-size: 20px;
                width: 50px;
                height: 50px;
                margin: 15px auto 0px auto;
                padding: 15px 10px 10px 10px;
                box-sizing: border-box;
                border-radius: 50%;
                background: #008000;
                color: #fff;
            }

        .block figure {
            margin: 0px;
            margin: 0px;
            background: #07578a;
            width: 100%;
            float: left;
            box-sizing: border-box;
            border-radius: 4px 4px 0px 0px;
            -moz-border-radius: 4px 4px 4px 4px;
            -webkit-border-radius: 4px 4px 4px 4px;
            -moz-border-radius: 4px 4px 4px 4px;
        }

            /*.block figure:hover {
                background: #93c3cd;
            }*/

            .block figure figcaption {
                padding: 10px;
                width: 100%;
                margin: 10px 0px 0px 0px;
                float: left;
                box-sizing: border-box;
                /*background: #0564ad;*/
                background: linear-gradient(to right, rgb(218, 140, 255), rgb(154, 85, 255));
                color: #fff;
                border-radius: 0px 0px 4px 4px;
                -moz-border-radius: 0px 0px 4px 4px;
                -webkit-border-radius: 0px 0px 4px 4px;
                -moz-border-radius: 0px 0px 4px 4px;
                font-size: 15px;
            }

        .table-bordered th, .table-bordered td {
            border: 1px solid #e9ecef00;
            font-size: 11px;
            vertical-align: top;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="app-main__inner">
        <div class="app-page-title">
            <div class="page-title-wrapper">
                <div class="page-title-heading">
                    <div class="page-title-icon">
                        <i class="pe-7s-notebook icon-gradient bg-mean-fruit"></i>
                    </div>
                    <div>
                        <asp:Literal ID="ltUsertop" runat="server">Assined Subject & Events</asp:Literal>

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

              <asp:GridView ID="GrdView" runat="server" class="mb-0 table table-bordered" AutoGenerateColumns="False" Width="100%" OnRowDataBound="GrdView_RowDataBound" ShowHeader="False">
                            <Columns>

                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>

                                        <div class="row" style="margin: 0px 0px 0px 0px;">
                                            <div class="page-head">
                                                <h4 class="h4">
                                                    <asp:Label ID="lbl_sessionname" runat="server" Text='<%#Bind("Sessionname")%>'>--</asp:Label><asp:Label ID="lbl_coursename" runat="server" Text='<%#Bind("Course_Name")%>'></asp:Label>(Section-<asp:Label ID="lbl_section" runat="server" Text='<%#Bind("section") %>'></asp:Label>)

                                                    <asp:Label ID="lbl_Session_id" runat="server" Text='<%#Bind("Session_id") %>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lbl_coursid" runat="server" Text='<%#Bind("CategoryID") %>' Visible="false"></asp:Label>
                                                  
                                                     
                                                    
                                                </h4>
                                            </div>
                                            <div class="row" style="margin: 0px 0px 0px 0px;">
                                                <asp:DataList ID="ddl_subject" runat="server" RepeatColumns="5" CellPadding="0" Style="margin: 0px auto; padding: 0px; height: auto;"
                                                    RepeatDirection="Horizontal">
                                                    <ItemTemplate>
                                                        <div class="row" style="margin: 0px 0px 0px 0px;">
                                                            <div class="block">
                                                                <figure>

                                                                    <figcaption>
                                                                        <asp:Label ID="lbl_CourseName" runat="server" Text='<%#Bind("Subject_name") %>'></asp:Label></h4>
                                                                    </figcaption>
                                                                </figure>
                                                            </div>
                                                        </div>

                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </div>
                                        </div>

                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>



                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
