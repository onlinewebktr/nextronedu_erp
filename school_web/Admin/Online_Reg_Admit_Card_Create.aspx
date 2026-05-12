<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Online_Reg_Admit_Card_Create.aspx.cs" Inherits="school_web.Admin.Online_Reg_Admit_Card_Note" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Create Admit Card

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        input, textarea {
            margin: 0;
            width: 26px !important;
            height: 40px !important;
            font-family: inherit;
            font-size: inherit;
            line-height: inherit;
        }

        #notification {
            z-index: 9999;
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


    <script type="text/javascript">
        function openModal() {
            $('#myModal').modal('show');
        }
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

            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3" style="position: relative">
                <div class="breadcrumb-title pe-3">Online Reg.</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Create Admit Card</li>
                        </ol>
                    </nav>
                </div>
                <a href="#" data-toggle="modal" data-target="#myModal" style="float: right; position: absolute; right: 0px; font-size: 23px; top: 2px;"><i class="bx bx-cog"></i></a>
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
                                    <div class="col-md-3">
                                        <label for="validationCustom01" class="form-label">Session <sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_session" runat="server" class="form-select find-dv-txtbx" AutoPostBack="true" OnSelectedIndexChanged="ddl_session_SelectedIndexChanged"></asp:DropDownList>
                                    </div>

                                    <div class="col-md-2">
                                        <label for="validationCustom01" class="find-dv-lbl">Test Name</label>
                                        <asp:DropDownList ID="ddl_test_name" runat="server" class="form-select"  ></asp:DropDownList>
                                    </div>

                                    <div class="col-md-2">
                                        <label for="validationCustom01" class="form-label">Class <sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_class" runat="server" class="form-select find-dv-txtbx"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <label for="validationCustom01" class="form-label">Exam Date<sup>*</sup></label>
                                        <div class="clndr-div">
                                            <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
                                            <asp:TextBox ID="txt_exam_date" runat="server" class="form-control find-dv-txtbx" Style="width: 100%!important; height: 31px !important;"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-3">
                                        <asp:Button ID="btn_find" runat="server" Text="Find" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btn_find_Click" Style="margin: 24px 0px 0px 0px; padding: 3px 10px; width: 60px!important; height: 31px!important;" />
                                        <asp:Button ID="btn_reset" runat="server" Text="Reset" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btn_reset_Click" Style="margin: 22px 0px 0px 0px; padding: 2px 10px; background: #bbb; border: 1px solid #ababab; width: 67px!important; height: 30px!important;" />
                                    </div>


                                    <div class="grd-wpr" style="overflow: auto">
                                        <asp:GridView ID="GrdView" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%" OnRowDataBound="GrdView_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Apply Id">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_Registration_id" runat="server" Text='<%#Bind("Registration_id")%>'></asp:Label>
                                                        <asp:Label ID="lbl_Class_id" runat="server" Text='<%#Bind("Class_id")%>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lbl_Session_id" runat="server" Text='<%#Bind("Session_id")%>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lbl_test_id" runat="server" Text='<%#Bind("Test_id")%>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Student Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_lbl_student_name" runat="server" Text='<%#Bind("Name")%>'></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date of birth">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_lbl_dob" runat="server" Text='<%#Bind("DOB")%>'></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Class">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("Course_Name")%>'></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Room">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddl_rooms" runat="server" Style="height: 28px;"></asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Examination Date">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt_Examination_Date" Style="width: 100px!important; height: 30px!important;"
                                                            runat="server" class="Calender"></asp:TextBox>

                                                        <img src="../Grid_calender/calender.png" style="float: right; margin: -22px 2px 0px 0px; position: relative;" />

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remarks">
                                                    <ItemTemplate>

                                                        <asp:TextBox ID="txt_remarks" runat="server" TextMode="MultiLine" Style="width: 300px!important; height: 50px!important;"></asp:TextBox>


                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Examination start time">
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
                                                            <asp:ListItem>30</asp:ListItem>
                                                            <asp:ListItem>40</asp:ListItem>
                                                            <asp:ListItem>50</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="ddl_am_pm" runat="server">
                                                            <asp:ListItem>AM</asp:ListItem>
                                                            <asp:ListItem>PM</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>



                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Examination end time">
                                                    <ItemTemplate>

                                                        <asp:DropDownList ID="ddl_hours_e" runat="server" Style="width: 45px">
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

                                                        <asp:DropDownList ID="ddl_minutes_e" runat="server" Style="width: 45px">
                                                            <asp:ListItem>00</asp:ListItem>
                                                            <asp:ListItem>05</asp:ListItem>
                                                            <asp:ListItem>10</asp:ListItem>
                                                            <asp:ListItem>15</asp:ListItem>
                                                            <asp:ListItem>20</asp:ListItem>
                                                            <asp:ListItem>30</asp:ListItem>
                                                            <asp:ListItem>40</asp:ListItem>
                                                            <asp:ListItem>50</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="ddl_am_pm_e" runat="server" Style="width: 50px">
                                                            <asp:ListItem>AM</asp:ListItem>
                                                            <asp:ListItem>PM</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>



                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Examination Type">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddl_examtype" runat="server" Style="height: 28px;">
                                                            <asp:ListItem>Exam</asp:ListItem>
                                                            <asp:ListItem>Viva</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>



                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>


                                        <asp:Button ID="btn_save" runat="server" Text="Save" Visible="false" CssClass="btn btn-success" CausesValidation="false" OnClick="btn_save_Click" Style="margin: 0px 4px 0px 0px; padding: 6px 10px; width: 60px!important; height: 37px!important; float: right;" />
                                        <asp:Button ID="btn_publish_admitcard" runat="server" Text="Publish" Visible="false" CssClass="btn btn-danger" CausesValidation="false" OnClick="btn_publish_admitcard_Click" Style="margin: 0px 4px 0px 0px; padding: 6px 10px; width: 80px!important; height: 37px!important; float: right;" />

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

    <script>
        $(function () {
            $("#<%=txt_exam_date.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                readOnly: true,
                yearRange: "1900:2100",
            }).attr("readonly", "true");
        });

        $(function () {
            $("#<%=txt_exam_date_auto.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                readOnly: true,
                yearRange: "1900:2100",
            }).attr("readonly", "true");
        });
    </script>



    <link href="../assets/css/modal-custom.css" rel="stylesheet" />
    <div class="modal fade" id="myModal" role="dialog" style="top: 0px">
        <div class="modal-dialog md-width">
            <!-- Modal content-->
            <div class="modal-content" style="position: relative">
                <div class="modal-header">
                    <h3 class="modal-title" style="font-size: 20px;">Find With Auto Fill</h3>
                    <button type="button" class="mdl-close-btn" data-dismiss="modal"><i class="fa fa-times" aria-hidden="true"></i></button>
                </div>
                <div class="modal-body md-bdy">
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Session</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddl_session_auto" runat="server" class="form-select"></asp:DropDownList>
                            </div>
                        </div>
                    </div>

                     <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Exam Name</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddl_exam_name_auto" runat="server" class="form-select"></asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Class</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddl_class_auto" runat="server" class="form-select"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Exam Date</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txt_exam_date_auto" runat="server" class="form-control" Style="width: 100% !important;"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Room</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddl_room_no_auto" runat="server" class="form-select"></asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Exam Start Time</label>
                            </div>
                            <div class="col-sm-8">
                                <div class="row">
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddl_s_hour_auto" runat="server" class="form-select">
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
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddl_s_minut_auto" runat="server" class="form-select">
                                            <asp:ListItem>00</asp:ListItem>
                                            <asp:ListItem>05</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>40</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddl_s_am_pm" runat="server" class="form-select">
                                            <asp:ListItem>AM</asp:ListItem>
                                            <asp:ListItem>PM</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Exam End Time</label>
                            </div>
                            <div class="col-sm-8">
                                <div class="row">
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddl_e_hour_auto" runat="server" class="form-select">
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
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddl_e_minut_auto" runat="server" class="form-select">
                                            <asp:ListItem>00</asp:ListItem>
                                            <asp:ListItem>05</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>40</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddl_e_am_pm" runat="server" class="form-select">
                                            <asp:ListItem>AM</asp:ListItem>
                                            <asp:ListItem>PM</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Exam Type</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddl_examtype_auto" runat="server" class="form-select">
                                    <asp:ListItem>Exam</asp:ListItem>
                                    <asp:ListItem>Viva</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4">
                                <label for="validationCustom01" class="find-dv-lbl">Remarks</label>
                            </div>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txt_remarks_auto" runat="server" TextMode="MultiLine" Style="width: 100% !important; height: 70px !important;" class="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="mdl-frm-row">
                        <div class="row">
                            <div class="col-sm-4"></div>
                            <div class="col-sm-8">
                                <asp:Button ID="btn_auto_fill" OnClick="btn_auto_fill_Click" runat="server" Text="Submit" class="btn btn-primary" Style="width: auto !important;" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
