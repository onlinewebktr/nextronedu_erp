<%@ Page Title="" Language="C#" MasterPageFile="~/InstructorProfile/Teacher_Profile.Master" AutoEventWireup="true" CodeBehind="final-report-card.aspx.cs" Inherits="school_web.InstructorProfile.final_report_card" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    final Report Card
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .table td, .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            padding: 8px !important;
        }

        .modal-backdrop.fade, .fade.blockOverlay {
            opacity: 0;
            display: none;
        }

        .collect-feesss {
            text-transform: capitalize;
            padding: 4px 5px;
            height: auto;
            font-size: 13px;
            font-weight: 500;
        }

        .nowordbreak {
            white-space: nowrap;
            word-break: keep-all;
        }

        .button-61 {
            align-items: center;
            appearance: none;
            border-radius: 4px;
            border-style: none;
            box-shadow: rgba(0, 0, 0, .2) 0 3px 1px -2px, rgba(0, 0, 0, .14) 0 2px 2px 0, rgba(0, 0, 0, .12) 0 1px 5px 0;
            box-sizing: border-box;
            color: #fff;
            cursor: pointer;
            display: inline-flex;
            font-size: .875rem;
            font-weight: 500;
            height: 36px;
            justify-content: center;
            letter-spacing: .0892857em;
            line-height: normal;
            min-width: 64px;
            outline: none;
            overflow: visible;
            padding: 0 16px;
            position: relative;
            text-align: center;
            text-decoration: none;
            text-transform: uppercase;
            transition: box-shadow 280mscubic-bezier(.4, 0, .2, 1);
            user-select: none;
            -webkit-user-select: none;
            touch-action: manipulation;
            vertical-align: middle;
            will-change: transform, opacity;
        }

            .button-61:active {
                box-shadow: rgba(0, 0, 0, .2) 0 5px 5px -3px, rgba(0, 0, 0, .14) 0 8px 10px 1px, rgba(0, 0, 0, .12) 0 3px 14px 2px;
                background: #A46BF5;
                color: #fff;
            }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="app-main__inner">
        <div class="app-page-title">
            <div class="page-title-wrapper">
                <div class="page-title-heading">
                    <div class="page-title-icon">
                        <i class="pe-7s-network icon-gradient bg-mean-fruit"></i>
                    </div>
                    <div>
                        <asp:Literal ID="ltUsertop" runat="server">Termwise Report Card</asp:Literal>
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

        <div class="row">
            <div class="col-lg-12">
                <div class="main-card mb-3 card">
                    <div class="card-body">

                        <asp:DropDownList ID="ddl_session" Style="display: none" runat="server"></asp:DropDownList>

                        <div class="row">
                            <div class="form-group col-xs-10 col-sm-3 col-md-2 col-lg-2">
                                <label for="validationCustom01" class="form-label">Class<sup>*</sup></label>
                                <asp:DropDownList ID="ddl_CourseCat" class="form-control" runat="server" Style="width: 100%" AutoPostBack="true" OnSelectedIndexChanged="ddl_CourseCat_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <div class="form-group col-xs-10 col-sm-3 col-md-10 col-lg-10">
                                <div class="row" style="padding: 0px 0px 0px 15px;">
                                    <div class="form-group col-xs-10 col-sm-3 col-md-2 col-lg-2">
                                        <label>Section<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_section" class="form-control" runat="server" Style="width: 98%"></asp:DropDownList>
                                    </div> 
                                    <div class="form-group col-xs-10 col-sm-3 col-md-1 col-lg-1">
                                        <asp:Button ID="btn_find" runat="server" class="btn btn-sm btn-success" Text="Find" Style="margin: 28px 0px 0px 0px; padding: 6px 10px 8px;" OnClick="btn_find_Click" />
                                    </div>

                                    <div class="form-group col-xs-10 col-sm-3 col-md-1 col-lg-1">
                                        <asp:Button ID="btn_print_all" runat="server" Text="Print All" class="btn btn-sm btn-success" OnClick="btn_print_all_Click" Style="margin: 28px 0px 0px 0px; padding: 6px 10px 8px;" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <hr />
                        <table id="example2" data-page-length='100000' class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                            <thead>
                                <tr>
                                    <th>#</th>
                                    <th>Admission No.</th>
                                    <th>Class</th>

                                    <th>Session</th>
                                    <th>Section</th>
                                    <th>Roll No.</th>
                                    <th>Student Name</th>
                                    <th>Action</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="rd_view" runat="server" OnItemDataBound="rd_view_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:Label ID="lbl_admissionserialnumber" runat="server" Text='<%#Bind("Admission_no")%>'></asp:Label>
                                            </td>


                                            <td style="text-align: left;">
                                                <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                            </td>

                                            <td style="text-align: left;">
                                                <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("session")%>'></asp:Label>
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
                                                <asp:Label ID="lbl_session_id" Visible="false" runat="server" Text='<%#Bind("Session_id")%>'></asp:Label>
                                                <asp:Label ID="lbl_class_id" Visible="false" runat="server" Text='<%#Bind("Class_id")%>'></asp:Label>
                                                <asp:Label ID="lbl_branch_id" Visible="false" runat="server" Text='<%#Bind("Branch_id")%>'></asp:Label>

                                                <a id="rpcard_link" style="background-color: #f7f100; min-width: 30px; color: #000;"
                                                    runat="server" class="button-61 nowordbreak collect-feesss" target="_blank"><span>print</span></a>

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
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
