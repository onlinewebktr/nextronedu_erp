<%@ Page Title="" Language="C#" MasterPageFile="~/LMS_VC_Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="vc_class_summary.aspx.cs" Inherits="school_web.LMS_VC_Admin.vc_class_summary" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    VC Class Summary
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
         .dt-button-collection {
           margin-top: -59.4px!important;
        }
    </style>
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
                        <asp:Literal ID="ltUsertop" runat="server"> VC Class Summary</asp:Literal>

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
                            <div class="form-row">
                                <div class="row" style="padding: 10px 0px 10px 0px; border: 1px solid #ccc; margin: 0px auto; background: #dbdbdb;">

                                    <div class="col-md-12">
                                        <div class="form-group form-contro" style="text-align: center">
                                            <asp:RadioButton ID="rd_day" runat="server" Text="Day" OnCheckedChanged="rd_day_CheckedChanged" AutoPostBack="true" />
                                            <asp:RadioButton ID="rd_month" runat="server" Text="Month" OnCheckedChanged="rd_month_CheckedChanged" AutoPostBack="true" />
                                            <asp:RadioButton ID="rd_year" runat="server" Text="Year" OnCheckedChanged="rd_year_CheckedChanged" AutoPostBack="true" />
                                        </div>
                                    </div>
                                    <div class="col-md-3" id="date1" runat="server" visible="false">
                                        <div class="form-group">
                                            <label>Select Date</label>
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
                                    <div class="col-md-3" id="year1" runat="server" visible="false">
                                        <div class="form-group">
                                            <label>Select Year:</label>
                                            <div class="input-group">
                                                <asp:DropDownList ID="ddl_year" runat="server" CssClass="form-control">

                                                    <asp:ListItem>2021</asp:ListItem>
                                                    <asp:ListItem>2022</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <br />
                                            <asp:Button ID="btn_find" runat="server" Text="Find" CssClass="btn btn-primary" OnClick="btn_Find_Click" Style="margin: 10px 0px 0px 0px;" />
                                            <asp:LinkButton ID="lbnk_summeryteacherclass" Visible="false" runat="server" OnClick="lbnk_summeryteacherclass_Click" class="btn-print noPrint" Style="float: right; font-size: 30px; color: #2651be;"><i class="fa fa-file-excel-o"> </i></asp:LinkButton>
                                        </div>
                                    </div>



                                </div>
                            </div>
                            <hr />
                            <div class="col-md-12" style="max-height: 200px; overflow: auto;">
                                <div class="form-group">

                                    <asp:GridView ID="grd_taken_class" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%" Style="text-align: center;">
                                        <Columns>


                                            <asp:TemplateField HeaderText="Teacher Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_teachername" runat="server" Text='<%#Bind("Name") %>'></asp:Label>
                                                    <asp:Label ID="lbl_teacherid" runat="server" Text='<%#Bind("Teacher_Id") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="No of VC Class Taken">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnk_view_details" runat="server" OnClick="lnk_view_details_Click" Text='<%#Bind("total_class") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <HeaderStyle BackColor="#2651be" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <RowStyle ForeColor="#000066" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />

                                    </asp:GridView>
                                </div>

                            </div>

                        </asp:Panel>

                        <asp:Panel ID="pnl_view" runat="server" Visible="false">
                            <h5 class="card-title">VC Class Taken Report For 
                            <asp:Label ID="lbl_month_year" runat="server"></asp:Label>
                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-warning pull-right" OnClick="LinkButton1_Click">Back</asp:LinkButton></h5>
                            <div class="form-row">
                                <div class="col-md-4">
                                    <div class="position-relative form-group">
                                        <label>Select Teacher</label>
                                        <div class="input-group input-group-icon">
                                            <asp:DropDownList ID="ddl_teacher" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="position-relative form-group">
                                        <label>Select Class</label>
                                        <div class="input-group input-group-icon">
                                            <asp:DropDownList ID="ddl_class" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_class_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="position-relative form-group">
                                        <label>Select Section</label>
                                        <div class="input-group input-group-icon">
                                            <asp:DropDownList ID="ddl_section" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_section_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>


                                <div class="col-md-3">
                                    <div class="position-relative form-group">
                                        <label>Select Subject</label>
                                        <div class="input-group input-group-icon">
                                            <asp:DropDownList ID="ddl_subject" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-1">
                                    <div class="position-relative form-group">
                                        <div class="input-group input-group-icon">
                                            <asp:Button ID="btn_search" runat="server" Text="Find" CssClass="btn btn-primary" OnClick="btn_search_Click" Style="margin-top: 30px" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-1">
                                    <asp:LinkButton ID="lnk_excel_download" runat="server" OnClick="lnk_excel_download_Click" class="btn-print noPrint" Style="float: right; font-size: 30px; color: #a94442; display: none"><i class="fa fa-file-excel-o"> </i></asp:LinkButton>

                                </div>
                            </div>
                            <hr />
                            <table style="width: 100%;" id="example" class="table table-hover table-striped table-bordered">
                                <thead>
                                    <tr>
                                        <th>Sl No.</th>
                                        <th>Teacher</th>
                                        <th>Class</th>
                                        <th>Section</th>
                                        <th>Subject</th>
                                        <th>Date</th>
                                        <th>Created Time</th>
                                        <th>Start Time</th>
                                        <th>End Time</th>
                                        <th>Zoom Id</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="RpDetailsStudent" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                 
                                                <td>
                                                    <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label></td>
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
                                                    <asp:Label ID="lbl_Date" runat="server" Text='<%#Bind("Date") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_createdtime" runat="server" Text='<%#Bind("CreatedOn","{0:hh:mm:ss tt}") %>'></asp:Label>
                                                </td>



                                                <td>
                                                    <asp:Label ID="lbl_Meeting_start_at" runat="server" Text='<%#Bind("Meeting_start_at","{0:hh:mm:ss tt}") %>'></asp:Label>
                                                </td>

                                                <td>
                                                    <asp:Label ID="lbl_end_time" runat="server" Text='<%#Bind("End_Time","{0:hh:mm:ss tt}") %>'></asp:Label>
                                                </td>

                                                <td>
                                                    <asp:Label ID="lbl_Zoom_id" runat="server" Text='<%#Bind("Zoom_id") %>'></asp:Label>
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
    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Subject Description</h5>
                    <asp:Button ID="btnclose" runat="server" class="btn btn-secondary" OnClientClick="return close()" Text="Close" />
                </div>
                <div class="modal-body">
                    <asp:Literal ID="ltDescription" runat="server"></asp:Literal>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
