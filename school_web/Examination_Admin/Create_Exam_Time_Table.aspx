<%@ Page Title="" Language="C#" MasterPageFile="~/Examination_Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Create_Exam_Time_Table.aspx.cs" Inherits="school_web.Examination_Admin.Create_Exam_Time_Table" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Create Exam Time Table
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        input, textarea {
            margin: 0;
            width: 26px !important;
            height: 40px !important;
            font-family: inherit;
            font-size: inherit;
            line-height: inherit;
        }
    </style>
    <script src="../Grid_calender/Scripts/jquery.dynDateTime.min.js" type="text/javascript"></script>
    <script src="../Grid_calender/Scripts/calendar-en.min.js" type="text/javascript"></script>
    <link href="../Grid_calender/Styles/calendar-blue.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(".Calender").dynDateTime({
                showsTime: false,
                ifFormat: "%d/%m/%Y",
                daFormat: "%l;%M %p, %e %m,  %Y",
                align: "BR",
                electric: false,
                singleClick: false,
                minDate: 0,
                startDate: new Date(),

                displayArea: ".siblings('.dtcDisplayArea')",
                button: ".next()"
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-wrapper">
        <div class="page-content">
            <div id="notification">
                <div id="pan" class="notificationpan">
                    <div id="success" runat="server" visible="false" style="float: left; width: 100%; height: auto;" class="alert alert-success border-0 bg-success alert-dismissible fade show py-2">
                        <div class="d-flex align-items-center">
                            <div class="font-35 text-white">
                                <i class='bx bxs-check-circle'></i>
                            </div>
                            <div class="ms-3">
                                <h6 class="mb-0 text-white">Success Alerts</h6>
                                <asp:Label ID="lbl_success" runat="server" ForeColor="White" class="text-white"></asp:Label>
                            </div>
                        </div>
                        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                    </div>

                    <div id="warning" runat="server" visible="false" class="alert alert-warning border-0 bg-warning alert-dismissible fade show py-2">
                        <div class="d-flex align-items-center">
                            <div class="font-35 text-dark">
                                <i class='bx bx-info-circle'></i>
                            </div>
                            <div class="ms-3">
                                <h6 class="mb-0 text-dark">Warning Alerts</h6>
                                <asp:Label ID="lbl_warning" runat="server" ForeColor="White" class="text-white"></asp:Label>
                            </div>
                        </div>
                        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                    </div>
                </div>
            </div>
            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                <div class="breadcrumb-title pe-3">Admit Card</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Create Exam Time Table</li>
                        </ol>
                    </nav>
                </div>
            </div>

            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <asp:Label ID="ltUsertop" runat="server" Style="font-weight: 500; font-size: 1rem;" class="mb-0 text-uppercase" Text=" "></asp:Label>
                    <hr />
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation" novalidate="">
                                    <div class="col-sm-5">
                                        <div class="row">
                                            <div class="col-sm-4">
                                                <label for="validationCustom01" class="find-dv-lbl">Session</label>
                                                <asp:DropDownList ID="ddlsession" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-4">
                                                <label for="validationCustom01" class="form-label">Class <sup>*</sup></label>
                                                <asp:DropDownList ID="ddl_class" runat="server" class="form-select find-dv-txtbx" OnSelectedIndexChanged="ddl_class_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-4">
                                                <label for="validationCustom01" class="form-label">Exam Term <sup>*</sup></label>
                                                <asp:DropDownList ID="ddl_examtearm" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_examtearm_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-5">
                                        <div class="row">
                                            <div class="col-md-4">
                                                <label for="validationCustom01" class="form-label">Exam<sup>*</sup></label>
                                                <asp:DropDownList ID="ddl_exam" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-4">
                                                <label for="validationCustom01" class="form-label">Section <sup>*</sup></label>
                                                <asp:DropDownList ID="ddl_section" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                            </div>

                                            <div class="col-md-4">
                                                <label for="validationCustom01" class="form-label">Exam Type<sup>*</sup></label>
                                                <asp:DropDownList ID="ddl_exam_shift_type" runat="server" class="form-select find-dv-txtbx" OnSelectedIndexChanged="ddl_exam_shift_type_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="1">Single Shift</asp:ListItem>
                                                    <asp:ListItem Value="2">Double Shift</asp:ListItem>
                                                    <asp:ListItem Value="3">Three Shift</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-2">
                                        <asp:Button ID="btn_find" runat="server" Text="Find" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btn_find_Click" Style="margin: 24px 0px 0px 0px; padding: 0px 10px; width: 60px!important; height: 31px!important;" />
                                        <asp:Button ID="btn_reset" runat="server" Text="Reset" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btn_reset_Click" Style="margin: 24px 0px 0px 0px; padding: 0px 10px; background: #bbb; border: 1px solid #ababab; width: 67px!important; height: 31px!important;" />
                                    </div>


                                    <div class="grd-wpr">
                                        <asp:GridView ID="GrdView" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%" OnRowDataBound="GrdView_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Subject Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Subjectname" runat="server" Text='<%#Bind("Subject_name")%>'></asp:Label>
                                                        <asp:Label ID="Subject_id" runat="server" Text='<%#Bind("Subject_id")%>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt_Examination_Date" Style="width: 100px!important; height: 30px!important;"
                                                            runat="server" class="Calender"></asp:TextBox>
                                                        <img src="../Grid_calender/calender.png" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Start time">
                                                    <ItemTemplate> 
                                                        <asp:DropDownList ID="ddl_hours" runat="server">
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
                                                        </asp:DropDownList>

                                                        <asp:DropDownList ID="ddl_minutes" runat="server">
                                                            <asp:ListItem>00</asp:ListItem>
                                                            <asp:ListItem>05</asp:ListItem>
                                                            <asp:ListItem>10</asp:ListItem>
                                                            <asp:ListItem>15</asp:ListItem>
                                                            <asp:ListItem>20</asp:ListItem>
                                                            <asp:ListItem>25</asp:ListItem>
                                                            <asp:ListItem>30</asp:ListItem>
                                                            <asp:ListItem>35</asp:ListItem>
                                                            <asp:ListItem>40</asp:ListItem>
                                                            <asp:ListItem>45</asp:ListItem>
                                                            <asp:ListItem>50</asp:ListItem>
                                                            <asp:ListItem>55</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="ddl_am_pm" runat="server">
                                                            <asp:ListItem>AM</asp:ListItem>
                                                            <asp:ListItem>PM</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="End time">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddl_hours_e" runat="server">
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
                                                        </asp:DropDownList>

                                                        <asp:DropDownList ID="ddl_minutes_e" runat="server">
                                                            <asp:ListItem>00</asp:ListItem>
                                                            <asp:ListItem>05</asp:ListItem>
                                                            <asp:ListItem>10</asp:ListItem>
                                                            <asp:ListItem>15</asp:ListItem>
                                                            <asp:ListItem>20</asp:ListItem>
                                                            <asp:ListItem>25</asp:ListItem>
                                                            <asp:ListItem>30</asp:ListItem>
                                                            <asp:ListItem>35</asp:ListItem>
                                                            <asp:ListItem>40</asp:ListItem>
                                                            <asp:ListItem>45</asp:ListItem>
                                                            <asp:ListItem>50</asp:ListItem>
                                                            <asp:ListItem>55</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="ddl_am_pm_e" runat="server">
                                                            <asp:ListItem>AM</asp:ListItem>
                                                            <asp:ListItem>PM</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Shift">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddl_shift" runat="server">
                                                            <asp:ListItem Value="1">1st Shift</asp:ListItem>
                                                            <asp:ListItem Value="2">2nd Shift</asp:ListItem>
                                                            <asp:ListItem Value="3">3rd Shift</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>


                                        <asp:Button ID="btn_save" runat="server" Text="Save" Visible="false" CssClass="btn btn-success" CausesValidation="false" OnClick="btn_save_Click" Style="margin: 0px 4px 0px 0px; padding: 6px 10px; width: 60px!important; height: 37px!important; float: right;" />

                                    </div> 
                                </div>
                            </div>
                        </div>
                    </div>
                </div> 
            </div>
        </div>
        <!--end row-->
    </div>
</asp:Content>
