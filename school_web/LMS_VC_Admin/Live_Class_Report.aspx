<%@ Page Title="" Language="C#" MasterPageFile="~/LMS_VC_Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Live_Class_Report.aspx.cs" Inherits="school_web.LMS_VC_Admin.Live_Class_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Live Class Report
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .dt-button-collection {
            margin-top: -59.4px!important;
        }

        .input-group > .form-control, .input-group > .form-control-plaintext, .input-group > .custom-select, .input-group > .custom-file {
            position: relative;
            flex: 1 1 auto;
            width: 1%;
            margin-bottom: 0;
            font-weight: bold!important;
        }
         .notificationpan {
            display: none;
            width: 100%;
            background-color: rgb(255, 76, 76);
            position: fixed;
            top: 66px!important;
            right: 10px;
            padding: 10px 10px;
            width: 290px;
            height: auto;
            border: 1px solid rgb(162, 162, 162);
            box-shadow: 2px 7px 19px -2px rgb(154 154 154 / 80%);
        }
           .clndr-icon {
            font-size: 14px !important;
            color: #ff2956;
            position: absolute;
            top: 5px;
            left: -23px;
        }

        .texbox-border {
            margin: 6px 0px 0px 0px;
            padding: 0px;
            height: auto;
            width: 100%;
            border-bottom: 1px solid #00000038;
        }
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
            left: -23px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="app-main__inner">
        <div class="app-page-title">
            <div class="page-title-wrapper">
                <div class="page-title-heading">
                    <div class="page-title-icon">
                        <i class="pe-7s-display1 icon-gradient bg-mean-fruit"></i>
                    </div>
                    <div>
                        <asp:Literal ID="ltUsertop" runat="server">Live Class Report</asp:Literal>

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
                        <asp:Panel ID="pnltop" runat="server">
                            <div class="form-row" style="font-size: 16px;">
                                <div class="row" style="padding: 10px 0px 10px 0px; border: 1px solid #ccc; margin: 0px auto; background: #dbdbdb;">

                                    <div class="col-md-12">
                                        <div class="form-group form-contro" style="text-align: center">
                                            <asp:RadioButton ID="rd_day" GroupName="ak" runat="server" Text="Day" OnCheckedChanged="rd_day_CheckedChanged" AutoPostBack="true" />
                                            <asp:RadioButton ID="rd_month" GroupName="ak" runat="server" Text="Month" OnCheckedChanged="rd_month_CheckedChanged" AutoPostBack="true" />
                                            <asp:RadioButton ID="rd_year" GroupName="ak" runat="server" Text="Year" OnCheckedChanged="rd_year_CheckedChanged" AutoPostBack="true" />
                                        </div>
                                    </div>
                                    <div class="col-md-3" id="date1" runat="server" visible="false">
                                        <div class="form-group">
                                            <label>Date</label>
                                            <div class="input-group input-group-icon">
                                                <asp:DropDownList ID="ddl_date" runat="server" CssClass="form-control">
                                                    <asp:ListItem>01</asp:ListItem>
                                                    <asp:ListItem>02</asp:ListItem>
                                                    <asp:ListItem>03</asp:ListItem>
                                                    <asp:ListItem>04</asp:ListItem>
                                                    <asp:ListItem>05</asp:ListItem>
                                                    <asp:ListItem>06</asp:ListItem>
                                                    <asp:ListItem>07</asp:ListItem>
                                                    <asp:ListItem>08</asp:ListItem>
                                                    <asp:ListItem>09</asp:ListItem>
                                                    <asp:ListItem>10</asp:ListItem>
                                                    <asp:ListItem>11</asp:ListItem>
                                                    <asp:ListItem>12</asp:ListItem>
                                                    <asp:ListItem>13</asp:ListItem>
                                                    <asp:ListItem>14</asp:ListItem>
                                                    <asp:ListItem>15</asp:ListItem>
                                                    <asp:ListItem>16</asp:ListItem>
                                                    <asp:ListItem>17</asp:ListItem>
                                                    <asp:ListItem>18</asp:ListItem>
                                                    <asp:ListItem>19</asp:ListItem>
                                                    <asp:ListItem>20</asp:ListItem>
                                                    <asp:ListItem>21</asp:ListItem>
                                                    <asp:ListItem>22</asp:ListItem>
                                                    <asp:ListItem>23</asp:ListItem>
                                                    <asp:ListItem>24</asp:ListItem>
                                                    <asp:ListItem>25</asp:ListItem>
                                                    <asp:ListItem>26</asp:ListItem>
                                                    <asp:ListItem>27</asp:ListItem>
                                                    <asp:ListItem>28</asp:ListItem>
                                                    <asp:ListItem>29</asp:ListItem>
                                                    <asp:ListItem>30</asp:ListItem>
                                                    <asp:ListItem>31</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3" id="month1" runat="server" visible="false">
                                        <div class="form-group">
                                            <label>Month</label>
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
                                    <div class="col-md-4" id="year1" runat="server" visible="false">
                                        <div class="form-group">
                                            <label>Year:</label>
                                            <div class="input-group">
                                                <asp:DropDownList ID="ddl_year" runat="server" CssClass="form-control">
                                                   
                                                    <asp:ListItem>2022</asp:ListItem>
                                                     <asp:ListItem>2023</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <br />
                                            <asp:Button ID="btn_find" runat="server" Text="Find" CssClass="btn btn-primary" OnClick="btn_find_Click" Style="margin: 10px 0px 0px 0px;" />

                                        </div>
                                    </div>



                                </div>
                            </div>
                            <hr />


                        </asp:Panel>

                        <asp:Panel ID="pnl_view" runat="server" Visible="false">
                            <h5 class="card-title"> 
                            <asp:Label ID="lbl_month_year" runat="server" Style="color: #f81b1b;"></asp:Label>&nbsp; | Total Class:-<asp:Label ID="lbl_totalfranchise" runat="server" Style="color: #f81b1b;"></asp:Label>

                            </h5>


                            <hr />
                            <table style="width: 100%;" id="example" class="table table-hover table-striped table-bordered">
                                <thead>

                                    <tr>

                                        <th>Sl No.</th>
                                        <th>Date</th>
                                        <th>Teacher</th>
                                        <th>Class</th>
                                        <th>Section</th>
                                        <th>Subject</th>
                                        <th>Start Time</th>
                                        <th>End Time</th>
                                        <th>Student Strength</th>
                                        <th>Student present</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="RpDetailsStudent" runat="server">
                                        <ItemTemplate>
                                            <tr>

                                                <td>
                                                    <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_teacherName" runat="server" Text='<%#Bind("Name") %>'></asp:Label>

                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_CategoryName" runat="server" Text='<%#Bind("CategoryName") %>'></asp:Label>

                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("section") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_CourseName" runat="server" Text='<%#Bind("CourseName") %>'></asp:Label>
                                                </td>




                                                <td>
                                                    <asp:Label ID="lbl_Meeting_start_at" runat="server" Text='<%#Bind("Meeting_start_at","{0:hh:mm:ss tt}") %>'></asp:Label>
                                                </td>

                                                <td>
                                                    <asp:Label ID="lbl_end_time" runat="server" Text='<%#Bind("End_Time","{0:hh:mm:ss tt}") %>'></asp:Label>
                                                </td>

                                                <td>
                                                    <asp:Label ID="lbl_student_count" runat="server" Text='<%#Bind("no_of_student") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_student_present" runat="server" Text='<%#Bind("no_of_persent") %>'></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
