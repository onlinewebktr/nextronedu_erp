<%@ Page Title="" Language="C#" MasterPageFile="~/InstructorProfile/Teacher_Profile.Master" AutoEventWireup="true" CodeBehind="View_Class_Wise_Study_Material.aspx.cs" Inherits="school_web.InstructorProfile.View_Class_Wise_Study_Material" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
 Uploaded Study Material
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .ui-datepicker .ui-datepicker-title select {
            font-size: 1em;
            margin: 1px 0;
            COLOR: #000;
            z-index: 99999!important;
        }

        .ui-datepicker .ui-datepicker-title select {
            font-size: 1em;
            margin: 1px 0;
            COLOR: #000;
            z-index: 99999!important;
        }

        .gridcss th {
            font-size: 14px!important;
        }

        .calender-icon {
            margin: 0px 0px 0px 0px;
            position: relative;
            font-size: 13px;
            font-weight: normal;
            width: 100%;
        }

        .clndr-icon {
            font-size: 14px !important;
            color: #ff2956;
            position: absolute;
            top: 4px;
            left: 75px;
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
                        <asp:Literal ID="ltUsertop" runat="server"> Uploaded Study Material</asp:Literal>

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
                            <div class="col-md-4">
                                <div class="position-relative form-group">
                                    <label>Class</label>
                                    <div class="input-group input-group-icon">
                                        <asp:DropDownList ID="ddl_class" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_class_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-2">
                                <div class="position-relative form-group">
                                    <label>Section</label>
                                    <div class="input-group input-group-icon">
                                        <asp:DropDownList ID="ddl_section" runat="server" CssClass="form-control calender-icon" AutoPostBack="false"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="position-relative form-group">
                                    <label>Start Date</label>
                                    <asp:TextBox ID="txt_date" runat="server" CssClass="form-control"></asp:TextBox>
                                    <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="position-relative form-group">
                                    <label>End Date</label>

                                    <asp:TextBox ID="txt_enddate" runat="server" CssClass="form-control calender-icon"></asp:TextBox>
                                    <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>

                                </div>
                            </div>
                            <div class="col-md-1">
                                <asp:Button ID="Btn_Find" runat="server" OnClick="Btn_Find_Click" class="btn btn-primary" Text="Find" Style="margin-top: 30px;" />
                            </div>
                        </div>
                        <hr />
                        <table style="width: 100%;" id="example" class="table table-hover table-striped table-bordered">
                            <thead>
                                <tr>
                                    <th>Sl. No.</th>
                                    <th>Teacher Name</th>
                                    <th>Class</th>
                                    <th>Section</th>
                                    <th>Subject</th>
                                    <th>Topic</th>
                                    <th>Video</th>
                                    <th>Date</th>
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
                                                <asp:Label ID="lbl_teachername" runat="server" Font-Names="Arial" Text='<%#Bind("teachername") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_CategoryName" runat="server" Text='<%#Bind("CategoryName") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Section_Subject" runat="server" Text='<%#Bind("Section_Subject") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_CourseName" runat="server" Text='<%#Bind("CourseName") %>'></asp:Label>
                                            </td>

                                            <td>
                                                <asp:Label ID="lbl_TopicName" runat="server" Text='<%#Bind("TopicName") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <iframe width="200" height="100" src='<%#Eval("VideoLink") %>' frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
                                            </td>




                                            <td>
                                                <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date") %>'></asp:Label>
                                            </td>

                                            <td>
                                                <div class="btn-actions-pane-right text-capitalize actions-icon-btn">
                                                    <div class="btn-group dropdown">
                                                        <button type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" class="btn-icon btn-icon-only btn btn-link">
                                                            <i class="pe-7s-menu btn-icon-wrapper"></i>
                                                        </button>
                                                        <div tabindex="-1" role="menu" aria-hidden="true" class="dropdown-menu-right rm-pointers dropdown-menu-shadow dropdown-menu-hover-link dropdown-menu">
                                                            <asp:LinkButton ID="lnk_view" runat="server" CssClass="dropdown-item" OnClick="lnk_view_Click"><i class="dropdown-icon lnr-inbox"></i><span>View Details</span></asp:LinkButton>
                                                            <asp:LinkButton ID="lnk_edit" runat="server" CssClass="dropdown-item" OnClick="lnk_edit_Click"><i class="dropdown-icon lnr lnr-pencil"></i><span>Edit</span></asp:LinkButton>
                                                            <asp:LinkButton ID="lnk_Delete" runat="server" CssClass="dropdown-item" OnClick="lnk_Delete_Click" OnClientClick='return confirm("Are you sure want to delete ?")'><i class="dropdown-icon lnr-trash"></i><span>Delete</span></asp:LinkButton>
                                                            <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lbl_Details" runat="server" Text='<%#Bind("Details") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lbl_VideoPostion" runat="server" Text='<%#Bind("VideoPostion") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lbl_TopicID" runat="server" Text='<%#Bind("TopicID") %>' Visible="false"></asp:Label>
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
    <style>
        @media (min-width: 576px) {
            .modal-dialog {
                max-width: 800px;
                margin: 15px 0px 0px 305px!important;
            }
        }
    </style>
    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Uploaded Study Material</h5>
                    <asp:Button ID="btnclose" runat="server" class="btn btn-secondary" OnClientClick="return close()" Text="Close" />
                </div>
                <div class="modal-body">

                    <table class="table table-bordered" style="width: 100%">

                        <tr>
                            <td style="font-size: 16px; font-weight: bold;">Topic
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 14px;">
                                <asp:Label ID="lbl_topic" runat="server" Style="font-size: 14px!important"></asp:Label>
                            </td>

                        </tr>


                        <tr>
                            <td style="font-size: 16px; font-weight: bold;">Description
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="font-size: 14px!important; height: 150px; overflow-x: hidden; overflow-y: scroll; float: left; width: 100%;">
                                    <asp:Label ID="txt_info1" runat="server" Style="font-size: 13px!important; word-break: break-all;"></asp:Label>
                                </div>


                            </td>

                        </tr>
                        <tr>
                            <td style="font-size: 16px; font-weight: bold;">Uploaded Study Material List
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="font-size: 14px!important; height: 100px; overflow-x: hidden; overflow-y: scroll; float: left; width: 100%;">
                                    <asp:GridView ID="GrdViewimg" runat="server" class="mb-0 table table-bordered" CssClass="table table-striped table-bordered gridcss" AutoGenerateColumns="False" Width="100%" OnRowDataBound="GrdViewimg_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>






                                            <asp:TemplateField HeaderText="Download">
                                                <ItemTemplate>
                                                    <a id="a2" runat="server" href='<%#Eval("Images") %>' download target="_blank" style="display: block; padding: 2px; font-family: ebrima; font-size: 31px; color: #0066CC; text-decoration: none;"><i class="fa fa-cloud-download" aria-hidden="true"></i></a>

                                                    <a id="a1" runat="server" href='<%#Eval("Images") %>' target="_blank" style="display: block; padding: 2px; font-size: 31px; color: #0066CC; text-decoration: none;">
                                                        <asp:Image ID="Image2" runat="server" ImageUrl='<%# Bind("Images") %>' Style="margin: 0px; height: 50px; width: 50px; border: 2px solid #f93; padding: 1px" />

                                                    </a>


                                                </ItemTemplate>

                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Uploading Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Upload_Date" runat="server" Text='<%#Bind("Upload_Date")%>'></asp:Label>

                                                </ItemTemplate>

                                            </asp:TemplateField>




                                            <asp:TemplateField HeaderText="File Type">
                                                <ItemTemplate>

                                                    <asp:Label ID="lbl_Type" runat="server" Text='<%#Bind("Type")%>'></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>




                                            <asp:TemplateField HeaderText="" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Button ID="BtnDeleteimg" OnClientClick="return confirm('Are you sure want to delete ?')" runat="server" OnClick="BtnDeleteimg_Click" class="btn btn-sm btn-success" Text="Delete" />
                                                    <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>



                                        </Columns>

                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>


                    </table>


                </div>
            </div>
        </div>
    </div>
    <link href="../Autocomplete/jquery-ui.css" rel="stylesheet" />

    <script src="../Autocomplete/jquery-ui.js"></script>
    <div class="input-group input-group-icon">
    </div>
    <script>
        $(function () {
            $("#<%=txt_date.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "1900:2100"
            });
        });
    </script>
    <script>
        $(function () {
            $("#<%=txt_enddate.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "1900:2100"
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
