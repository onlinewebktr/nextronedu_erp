<%@ Page Title="" Language="C#" MasterPageFile="~/InstructorProfile/Teacher_Profile.Master" AutoEventWireup="true" CodeBehind="Student_Add_Attendance_class_wise.aspx.cs" Inherits="school_web.InstructorProfile.Student_Add_Attendance_class_wise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Make Student Attendance
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .table-bordered th {
            border: 1px solid #e9ecef;
            font-size: 13px;
        }

        .table-bordered td {
            border: 1px solid #e9ecef00;
            font-size: 16px;
        }

        .notificationpan {
            background-color: rgb(255, 76, 76);
            position: fixed;
            top: 70px;
            right: 10px;
            padding: 10px 10px;
            width: 667px !important;
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
            height: 55px !important;
            z-index: 1000;
            font-size: 17px;
            text-align: center;
            width: 99.8%;
            position: fixed;
        }

        .app-wrapper-footer {
            display: none;
        }

        .ui-datepicker .ui-datepicker-title select {
            font-size: 1em;
            margin: 1px 0;
            COLOR: #000;
            z-index: 99999 !important;
        }

        .ui-datepicker .ui-datepicker-title select {
            font-size: 1em;
            margin: 1px 0;
            COLOR: #000;
            z-index: 99999 !important;
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
            top: 32px;
            left: 318px;
        }

         table {
            /*box-shadow: 0 1px 1px 0 rgb(0 0 0 / 14%), 0 7px 10px -5px rgb(244 67 54 / 40%);*/
            /*background: linear-gradient( 60deg,#f7807e,#e53935);*/
            border-radius: 0px;
            /*border: 1px dashed #d1c5c5!important;*/
            background: #fff!important;
            border-bottom: 0px solid #c8c5c5;
        }

        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            padding: 5px 5px;
            vertical-align: middle;
            border-color: #ddd0;
            font-size: 12px;
            color: #000;
            padding: 6px 0px 5px 7px!important;
            text-align: center;
        }

        .table-bordered > thead > tr > th, .table-bordered > tbody > tr > th, .table-bordered > tfoot > tr > th, .table-bordered > thead > tr > .table-bordered > tbody > tr > .table-bordered > tfoot > tr > th {
            background: #e1dddd!important;
        }

        .table-bordered > thead > tr > th, .table-bordered > tbody > tr > th, .table-bordered > tfoot > tr > th, .table-bordered > thead > tr > td, .table-bordered > tbody > tr > td, .table-bordered > tfoot > tr > td {
            border: 1px solid #e7e7e7;
            text-align: center;
            padding: 3px 4px 3px 5px;
            font-size: 11px;
        }

        .form-control {
            display: block;
            width: 100%;
            height: 25px!important;
            font-size: 14px!important;
            padding: 2px 5px;
        }

        .table {
            margin-bottom: 9px!important;
        }

        label {
            display: inline!important;
            max-width: 100%;
            margin-bottom: 5px;
            font-weight: bold;
        }


        .rdobtnS {
            margin: 0px 0px 0px 0px;
            font-size: 12px;
        }

            .rdobtnS tr {
                padding: 0px 2px;
                width: 33px;
                float: left;
            }

                .rdobtnS tr td {
                    padding: 0px;
                    width: 30px;
                    margin: 0px;
                    height: 30px;
                    float: left;
                }

                    .rdobtnS tr td label {
                        position: relative;
                        top: -33px;
                    }


        .att-imgs {
            padding: 3px;
            width: 44px;
            height: 44px;
            border: 1px solid #a1a1a1;
            border-radius: 50%;
        }

        .att-name {
            padding: 0px;
            width: 100%;
            float: left;
            font-size: 12px;
            font-weight: 500;
        }

        .att-adm_no {
            padding: 0px;
            width: 100%;
            float: left;
        }

        .att-roll {
            padding: 0px;
            width: 100%;
            float: left;
        }

        .img-dv {
            padding: 0px;
            width: 45px;
            float: left;
        }

        .contnt-dv {
            padding: 0px 0px 0px 10px;
            float: left;
            width: 52%;
            text-align: left;
        }

        .action-dv {
            padding: 8px 0px 0px 0px;
            float: right;
        }

        .ui-datepicker-trigger {
            display: none;
        }

        .container {
            padding-right: 10px;
            padding-left: 10px;
        }


        .rdobtnS tr:nth-child(1) {
        }

        .rdobtnS tr:nth-child(2) {
        }

        .rdobtnS tr:nth-child(3) {
        }


        .rdobtnS tr:nth-child(1) input[type="radio"] {
            margin: 0px;
            width: 30px;
            height: 30px;
            border-radius: 15px;
            border: 1px solid #39c500;
            background-color: white;
            -webkit-appearance: none; /*to disable the default appearance of radio button*/
            -moz-appearance: none;
        }

            .rdobtnS tr:nth-child(1) input[type="radio"]:focus { /*no need, if you don't disable default appearance*/
                outline: none; /*to remove the square border on focus*/
            }

            .rdobtnS tr:nth-child(1) input[type="radio"]:checked { /*no need, if you don't disable default appearance*/
                background-color: #3ed700;
            }

                .rdobtnS tr:nth-child(1) input[type="radio"]:checked ~ span:first-of-type {
                    color: white;
                }

                .rdobtnS tr:nth-child(1) input[type="radio"]:checked label {
                    color: #fff;
                }

        /*========================SecondRoW============================*/
        .rdobtnS tr:nth-child(2) input[type="radio"] {
            margin: 0px;
            width: 30px;
            height: 30px;
            border-radius: 15px;
            border: 1px solid #ff0000;
            background-color: white;
            -webkit-appearance: none; /*to disable the default appearance of radio button*/
            -moz-appearance: none;
        }

            .rdobtnS tr:nth-child(2) input[type="radio"]:focus { /*no need, if you don't disable default appearance*/
                outline: none; /*to remove the square border on focus*/
            }

            .rdobtnS tr:nth-child(2) input[type="radio"]:checked { /*no need, if you don't disable default appearance*/
                background-color: #ff0000;
            }

                .rdobtnS tr:nth-child(2) input[type="radio"]:checked ~ span:first-of-type {
                    color: white;
                }


        /*========================THIRDROWS============================*/
        .rdobtnS tr:nth-child(3) input[type="radio"] {
            margin: 0px;
            width: 30px;
            height: 30px;
            border-radius: 15px;
            border: 1px solid #ffa500;
            background-color: white;
            -webkit-appearance: none; /*to disable the default appearance of radio button*/
            -moz-appearance: none;
        }

            .rdobtnS tr:nth-child(3) input[type="radio"]:focus { /*no need, if you don't disable default appearance*/
                outline: none; /*to remove the square border on focus*/
            }

            .rdobtnS tr:nth-child(3) input[type="radio"]:checked { /*no need, if you don't disable default appearance*/
                background-color: #ffa500;
            }

                .rdobtnS tr:nth-child(3) input[type="radio"]:checked ~ span:first-of-type {
                    color: white;
                }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%; position: relative">
        <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>--%>
        <div class="app-main__inner">
            <div class="app-page-title">
                <div class="page-title-wrapper">
                    <div class="page-title-heading">
                        <div class="page-title-icon">
                            <i class="pe-7s-menu icon-gradient bg-mean-fruit"></i>
                        </div>
                        <div>
                            <asp:Literal ID="ltUsertop" runat="server">Make Student Attendance</asp:Literal>
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


                                    <td style="padding: 10px 10px 10px 10px; font-weight: bold"></td>

                                </tr>

                                <tr>
                                    <td style="padding: 10px 10px 10px 10px">
                                        <asp:DropDownList ID="ddl_session" Style="width: 100px!important;" runat="server" CssClass="form-control" AutoPostBack="false"></asp:DropDownList>
                                    </td>


                                    <td style="padding: 10px 10px 10px 10px; font-weight: bold">
                                        <asp:TextBox ID="txt_date" runat="server" CssClass="form-control calender-icon"></asp:TextBox>

                                        <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
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
                                    </td>


                                    <td style="padding: 10px 10px 10px 10px">
                                        <asp:DropDownList ID="ddl_class" runat="server" Style="width: 100px!important;" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_class_SelectedIndexChanged"></asp:DropDownList>
                                    </td>

                                    <td style="padding: 10px 10px 10px 10px; font-weight: bold">
                                        <asp:DropDownList ID="ddl_section" runat="server" Style="width: 100px!important;" CssClass="form-control"></asp:DropDownList>
                                    </td>




                                    <td style="padding: 10px 10px 10px 10px">
                                        <asp:Button ID="btn_find" runat="server" Text="Find" class="mt-2 btn btn-primary" OnClick="btn_find_Click" ValidationGroup="a" Style="float: right" />

                                    </td>
                                </tr>

                            </table>

                            <div runat="server" visible="false" id="grid111">
                                <asp:GridView ID="GrdView" runat="server" class="table-bordered" AutoGenerateColumns="False" Width="100%" OnRowDataBound="GrdView_RowDataBound">
                                    <Columns>

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <div class="img-dv">
                                                    <img src="<%#Eval("Student_img") %>" class="att-imgs" />
                                                </div>
                                                <div class="contnt-dv">
                                                    <asp:Label ID="lbl_FullName" runat="server" class="att-name" Text='<%#Bind("studentname")%>'></asp:Label>
                                                    <asp:Label ID="lbl_reg_id" runat="server" class="att-adm_no" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                    <asp:Label ID="lbl_roll_no" runat="server" class="att-roll" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                    <asp:Label ID="lbl_father_mob" Visible="false" runat="server" class="att-roll" Text='<%#Bind("father_mob")%>'></asp:Label>
                                                    <asp:Label ID="lbl_Father_whatsApp_no" Visible="false" runat="server" class="att-roll" Text='<%#Bind("Father_whatsApp_no")%>'></asp:Label>
                                                    <asp:Label ID="lbl_classname" Visible="false" runat="server" class="att-roll" Text='<%#Bind("classname")%>'></asp:Label>
                                                </div>
                                                <div class="action-dv">
                                                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" class="rdobtnS" OnDataBound="RadioButtonList1_DataBound"></asp:RadioButtonList>
                                                </div>
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
        <%--  </ContentTemplate>

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
        </asp:UpdateProgress>--%>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
    <%--  <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
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




    </script>--%>
</asp:Content>
