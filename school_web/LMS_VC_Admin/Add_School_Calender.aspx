<%@ Page Title="" Language="C#" MasterPageFile="~/LMS_VC_Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Add_School_Calender.aspx.cs" Inherits="school_web.LMS_VC_Admin.Add_School_Calender" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Create School Calendar
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="../Autocomplete/jquery-ui.css" rel="stylesheet" />
    <script src="../Autocomplete/jquery-ui.js"></script>


    <script>
        $(function () {
            $("#<%=txt_date.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "1900:2100",
               
               
            }).attr("readonly", "true");
        });
    </script>
    <style>
        calender-icon {
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
            top: 5px;
            left: 89px;
        }

        .dt-button-collection {
            margin-top: 6.6px!important;
        }

        .notificationpan {
            display: none;
            width: 20%;
            background-color: rgb(255, 76, 76);
            position: fixed;
            top: 63px!important;
            right: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="app-main__inner">
        <div class="app-page-title">
            <div class="page-title-wrapper">
                <div class="page-title-heading">
                    <div class="page-title-icon">
                        <i class="fa fa-calendar icon-gradient bg-mean-fruit"></i>
                    </div>
                    <div>
                        <asp:Literal ID="ltUsertop" runat="server">Create School Calendar</asp:Literal>

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
        <asp:HiddenField ID="slid" runat="server" />
        <div class="row">
            <div class="col-lg-8">
                <div class="main-card mb-3 card">
                    <div class="card-body">
                        <h5 class="card-title"></h5>
                        <div class="form-row">
                            <div class="col-md-12">
                                <div class="position-relative form-group">
                                    <label>Select Date</label>
                                    <asp:TextBox ID="txt_date" runat="server" CssClass="form-control" placeholder="Date"  AutoPostBack="true" OnTextChanged="txt_date_TextChanged"></asp:TextBox>
                                    <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                </div>
                                <div class="position-relative form-group">
                                    <label>Day Name</label>
                                    <asp:TextBox ID="txt_day_name" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                                <div class="position-relative form-group">
                                    <label>Select Type</label>
                                    <asp:DropDownList ID="ddl_type" runat="server" CssClass="form-control" placeholder=" Type">
                                        <asp:ListItem>H</asp:ListItem>
                                        <asp:ListItem>CL</asp:ListItem>
                                        <asp:ListItem>Class</asp:ListItem>
                                        <asp:ListItem>No Class</asp:ListItem>
                                        <asp:ListItem>NA</asp:ListItem>
                                        <asp:ListItem></asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                                <div class="position-relative form-group">
                                    <label>Details </label>
                                    <asp:TextBox ID="txt_description" runat="server" CssClass="form-control" placeholder="Details" TextMode="MultiLine" Style="height: 100px"></asp:TextBox>
                                </div>





                                <asp:Button ID="btn_Submit" runat="server" Text="Add" class="mt-2 btn btn-primary" OnClick="btn_Submit_Click" />
                                <asp:Button ID="btn_cancel" runat="server" Text="Cancel" class="mt-2 btn btn-danger" OnClick="btn_cancel_Click" Visible="false" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-8" style="display: none">
                <div class="main-card mb-3 card">
                    <div class="card-body">
                        <asp:HiddenField ID="HdID" runat="server" />
                        <h5 class="card-title">Added School Calendar </h5>
                        <div class="form-row">
                            <div class="col-md-4">
                                <div class="position-relative form-group">
                                    <label>Select Month</label>
                                    <div class="input-group input-group-icon">
                                        <asp:DropDownList ID="ddl_month" runat="server" CssClass="form-control">
                                            <asp:ListItem>Jan</asp:ListItem>
                                            <asp:ListItem>Feb</asp:ListItem>
                                            <asp:ListItem>Mar</asp:ListItem>
                                            <asp:ListItem>Apr</asp:ListItem>
                                            <asp:ListItem>May</asp:ListItem>
                                            <asp:ListItem>Jun</asp:ListItem>
                                            <asp:ListItem>Jul</asp:ListItem>
                                            <asp:ListItem>Aug</asp:ListItem>
                                            <asp:ListItem>Sep</asp:ListItem>
                                            <asp:ListItem>Oct</asp:ListItem>
                                            <asp:ListItem>Nov</asp:ListItem>
                                            <asp:ListItem>Dec</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="position-relative form-group">
                                    <label>Select Year</label>
                                    <div class="input-group input-group-icon">
                                        <asp:DropDownList ID="ddl_year" runat="server" CssClass="form-control">
                                            <asp:ListItem>2021</asp:ListItem>
                                            <asp:ListItem>2022</asp:ListItem>
                                            <asp:ListItem>2023</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <asp:Button ID="Btn_Find" runat="server" OnClick="Btn_Find_Click" class="btn btn-primary" Text="Find" Style="margin-top: 30px;" />
                            </div>
                        </div>
                        <table style="width: 100%;" id="example" class="table table-hover table-striped table-bordered">
                            <thead>
                                <tr>
                                    <th>Sl No.</th>
                                    <th>Date</th>
                                    <th>Day</th>
                                    <th>Type</th>
                                    <th>Details</th>
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
                                                <asp:Label ID="lbl_Date" runat="server" Font-Names="Arial" Text='<%#Bind("Date") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_Day" runat="server" Font-Names="Arial" Text='<%#Bind("Day") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_type" runat="server" Font-Names="Arial" Text='<%#Bind("Type") %>'></asp:Label>
                                            </td>

                                            <td>
                                                <asp:Label ID="lbl_Remarks" runat="server" Font-Names="Arial" Text='<%#Bind("Details") %>'></asp:Label>
                                            </td>





                                            <td>

                                                <div class="btn-actions-pane-right text-capitalize actions-icon-btn">
                                                    <div class="btn-group dropdown">
                                                        <button type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" class="btn-icon btn-icon-only btn btn-link">
                                                            <i class="pe-7s-menu btn-icon-wrapper"></i>
                                                        </button>
                                                        <div tabindex="-1" role="menu" aria-hidden="true" class="dropdown-menu-right rm-pointers dropdown-menu-shadow dropdown-menu-hover-link dropdown-menu">




                                                            <asp:LinkButton ID="lnk_edit" runat="server" CssClass="dropdown-item" OnClick="lnk_edit_Click"><i class="dropdown-icon lnr lnr-pencil"></i><span>Edit</span></asp:LinkButton>

                                                            <asp:LinkButton ID="lnk_Delete" runat="server" CssClass="dropdown-item" OnClick="lnk_Delete_Click" OnClientClick='return confirm("Are you sure want to delete ?")'><i class="dropdown-icon lnr-trash"></i><span>Delete</span></asp:LinkButton>

                                                            <asp:Label ID="lbl_id" runat="server" Font-Names="Arial" Text='<%#Bind("Id") %>' Visible="false"></asp:Label>

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

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
