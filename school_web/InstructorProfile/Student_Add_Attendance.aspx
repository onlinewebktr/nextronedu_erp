<%@ Page Title="" Language="C#" MasterPageFile="~/InstructorProfile/Teacher_Profile.Master" AutoEventWireup="true" CodeBehind="Student_Add_Attendance.aspx.cs" Inherits="school_web.InstructorProfile.Student_Add_Attendance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Student Add Attendance
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
    <script language="Javascript">
       <!--
    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode != 46 && charCode > 31
          && (charCode < 48 || charCode > 57))
            return false;

        return true;
    }
    //-->
    </script>
    <style>
        .waiting {
            padding: 15px 15px 15px 14px;
            font-size: 16px;
            bottom: 0px;
            left: 1px;
            top: 300px;
            background: #fff0;
            color: #1a1313;
            height: 55px!important;
            z-index: 1000;
            font-size: 17px;
            text-align: center;
            width: 99.8%;
            position: fixed;
        }

        .app-wrapper-footer {
            display: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%; position: relative">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="app-main__inner">
                    <div class="app-page-title">
                        <div class="page-title-wrapper">
                            <div class="page-title-heading">
                                <div class="page-title-icon">
                                    <i class="pe-7s-menu icon-gradient bg-mean-fruit"></i>
                                </div>
                                <div>
                                    <asp:Literal ID="ltUsertop" runat="server">Enter Student Remarks</asp:Literal>
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
                                            <td style="padding: 10px 10px 10px 10px; font-weight: bold">Date</td>

                                            <td style="padding: 10px 10px 10px 10px; font-weight: bold">Class 
                                            </td>
                                            <td style="padding: 10px 10px 10px 10px; font-weight: bold">Section 
                                            </td>
                                            <td style="padding: 10px 10px 10px 10px; font-weight: bold">Subject 
                                            </td>
                                             <td style="padding: 10px 10px 10px 10px; font-weight: bold">Period 
                                            </td>
                                            <td style="padding: 10px 10px 10px 10px; font-weight: bold"></td>

                                        </tr>

                                        <tr>
                                            <td style="padding: 10px 10px 10px 10px">
                                                <asp:DropDownList ID="ddl_session" Style="width: 100px!important;" runat="server" CssClass="form-control" AutoPostBack="false"></asp:DropDownList>
                                            </td>


                                            <td style="padding: 10px 10px 10px 10px; font-weight: bold">
                                                <asp:TextBox ID="txt_date" runat="server" CssClass="classTarget" Enabled="false"></asp:TextBox>
                                            </td>


                                            <td style="padding: 10px 10px 10px 10px">
                                                <asp:DropDownList ID="ddl_class" runat="server" Style="width: 100px!important;" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_class_SelectedIndexChanged"></asp:DropDownList>
                                            </td>

                                            <td style="padding: 10px 10px 10px 10px; font-weight: bold">
                                                <asp:DropDownList ID="ddl_section" runat="server" Style="width: 100px!important;" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_section_SelectedIndexChanged"></asp:DropDownList>
                                            </td>


                                            <td style="padding: 10px 10px 10px 10px; font-weight: bold">
                                                <asp:DropDownList ID="ddl_subject" runat="server" Style="width: 100px!important;" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_subject_SelectedIndexChanged"></asp:DropDownList>
                                            </td>
                                             <td style="padding: 10px 10px 10px 10px; font-weight: bold">
                                                <asp:DropDownList ID="ddl_period" runat="server" Style="width: 100px!important;" CssClass="form-control"></asp:DropDownList>
                                            </td>
                                            <td style="padding: 10px 10px 10px 10px">
                                                <asp:Button ID="btn_find" runat="server" Text="Find" class="mt-2 btn btn-primary" OnClick="btn_find_Click" ValidationGroup="a" Style="float: right" />

                                            </td>
                                        </tr>

                                    </table>

                                    <div runat="server" visible="false" id="grid111">
                                        <asp:GridView ID="GrdView" runat="server" class="table table-hover table-striped table-bordered" AutoGenerateColumns="False" Width="100%" OnRowDataBound="GrdView_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl. No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Student Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_FullName" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Admission No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_reg_id" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Roll No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_roll_no" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Attandance Action">
                                                    <ItemTemplate>
                                                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" Style="margin: 0px 0px 0px 25px; font-size: 12px" OnDataBound="RadioButtonList1_DataBound"></asp:RadioButtonList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>







                                            </Columns>
                                        </asp:GridView>
                                        <asp:Button ID="btn_save_all" runat="server" Style="width: 176px; height: 37px; float: right" CssClass="mt-2 btn btn-primary" Text="Save All" OnClick="btn_save_all_Click" OnClientClick='return confirm("Are you sure want to save all ?")' />
                                    </div>
                                </div>
                            </div>



                        </div>

                    </div>
                </div>
                <asp:HiddenField ID="hd_id" runat="server" />
            </ContentTemplate>

        </asp:UpdatePanel>
        <asp:UpdateProgress ID="UpdateProgress2"
            runat="server" AssociatedUpdatePanelID="UpdatePanel2"
            DynamicLayout="False">
            <ProgressTemplate>
                <p class="waiting">
                    &nbsp;&nbsp;&nbsp;
                                            <img src="../images/Processing.gif" />

                </p>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
        rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
        //On Page Load.
        $(function () {
            SetDatePicker();

        });

        //On UpdatePanel Refresh.
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    SetDatePicker();


                }
            });
        };
        function SetDatePicker() {
            $("[id$=txt_date]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                buttonImage: 'calendar.png',
                changeMonth: true,
                changeYear: true,
                yearRange: "1900:2100",
                dateFormat: "dd/mm/yy",
                // minDate: 0
            });
        }




    </script>
</asp:Content>
