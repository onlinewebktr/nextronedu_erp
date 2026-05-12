<%@ Page Title="" Language="C#" MasterPageFile="~/LMS_VC_Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Student_Attendance_Chart_Month_Wise.aspx.cs" Inherits="school_web.LMS_VC_Admin.Student_Attendance_Chart_Month_Wise" EnableEventValidation="false" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">Attendance Calculation
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
                                    <asp:Literal ID="ltUsertop" runat="server">Attendance Calculation</asp:Literal>
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
                                            <td style="padding: 10px 10px 10px 10px; font-weight: bold">Session Tracking Head for Exam
                                            </td>
                                            <td style="padding: 10px 10px 10px 10px; font-weight: bold">Month</td>

                                            <td style="padding: 10px 10px 10px 10px; font-weight: bold">Class 
                                            </td>
                                            <td style="padding: 10px 10px 10px 10px; font-weight: bold">Section 
                                            </td>


                                            <td style="padding: 10px 10px 10px 10px; font-weight: bold"></td>

                                        </tr>

                                        <tr>
                                            <td style="padding: 10px 10px 10px 10px">
                                                <asp:DropDownList ID="ddl_session" Style="width: 100px!important;" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_session_SelectedIndexChanged">
                                                    
                                                     
                                                </asp:DropDownList>
                                                
                                            </td>
                                            <td style="padding: 10px 10px 10px 10px">
                                                <asp:DropDownList ID="ddl_exam_activity" Style="width: 100px!important;" runat="server" CssClass="form-control">
                                                    
                                                     
                                                </asp:DropDownList>
                                                
                                            </td>

                                            <td style="padding: 10px 10px 10px 10px; font-weight: bold">
                                                <asp:DropDownList ID="ddl_month" Style="width: 100px!important;" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </td>


                                            <td style="padding: 10px 10px 10px 10px">
                                                <asp:DropDownList ID="ddl_class" runat="server" Style="width: 100px!important;" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_class_SelectedIndexChanged"></asp:DropDownList>
                                            </td>

                                            <td style="padding: 10px 10px 10px 10px; font-weight: bold">
                                                <asp:DropDownList ID="ddl_section" runat="server" Style="width: 100px!important;" CssClass="form-control"></asp:DropDownList>
                                            </td>




                                            <td style="padding: 10px 10px 10px 10px">
                                                <asp:Button ID="btn_find" runat="server" Text="Attendance Calculate" class="mt-2 btn btn-primary" OnClick="btn_find_Click" ValidationGroup="a" Style="float: left" />
                                               
                                                  <asp:Button ID="btn_save" runat="server" Text="Save Attendance" Visible="false" class="mt-2 btn btn-primary" OnClick="btn_save_Click" ValidationGroup="a" Style="float: right" />
                                                 <asp:ImageButton ID="imgexcel2" runat="server" Visible="false" ImageUrl="~/images/excel_con.png" CssClass="excelbutton22" OnClick="imgexcel2_Click"
                                                    Style="height: 31px; width: 32px; margin-top: 1px; margin: 8px 0px 0px 13px;" />


                                            </td>
                                        </tr>

                                    </table>

                                    <div runat="server" visible="false" id="grid111">

                                        <table style="margin: 0px; padding: 0px; width: 64%; display:none" border="1" >
                                            <tr>
                                                <td style="padding: 5px;">Total Student
                                                </td>

                                                <td style="padding: 5px;">
                                                    <asp:Label ID="lbltotal_student" Font-Bold="true" runat="server">0</asp:Label>
                                                </td>
                                                <td style="padding: 5px;">Total Prsent Student
                                                </td>

                                                <td style="padding: 5px;">
                                                    <asp:Label ID="lbl_persenstudent" Font-Bold="true" runat="server">0</asp:Label>
                                                </td>

                                                <td style="padding: 5px;">Total Absent Student
                                                </td>

                                                <td style="padding: 5px;">
                                                    <asp:Label ID="lbl_totalabsentstudent" Font-Bold="true" runat="server">0</asp:Label>
                                                </td>

                                            </tr>
                                        </table>
                                         <div style="overflow-y: scroll; width: 1047px;">
                                        <asp:GridView ID="GrdView" OnRowDataBound="GrdView_RowDataBound" runat="server" class="table table-hover table-striped table-bordered" AutoGenerateColumns="False" Width="100%">
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
                                                <asp:TemplateField HeaderText="01">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_date1" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="02">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_date2" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="03">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_date3" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="04">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_date4" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="05">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_date5" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="06">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_date6" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="07">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_date7" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="08">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_date8" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="09">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_date9" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="10">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_date10" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="11">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_date11" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="12">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_date12" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="13">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_date13" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="14">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_date14" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="15">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_date15" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="16">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_date16" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="17">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_date17" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="18">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_date18" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="19">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_date19" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="20">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_date20" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="21">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_date21" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="22">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_date22" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="23">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_date23" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="24">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_date24" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="25">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_date25" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="26">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_date26" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="27">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_date27" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="28">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_date28" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="29">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_date29" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="30">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_date30" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="31">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_date31" runat="server"></asp:Label>
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
                </div>
                <asp:HiddenField ID="hd_id" runat="server" />
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="imgexcel2" />
            </Triggers>
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
</asp:Content>
