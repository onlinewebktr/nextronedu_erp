<%@ Page Title="" Language="C#" MasterPageFile="~/Dvlpr_Prof/Site1.Master" AutoEventWireup="true" CodeBehind="Delete_bill_history.aspx.cs" Inherits="school_web.Dvlpr_Prof.Delete_bill_history" ValidateRequest="false" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Deleted Bill History
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
            top: 5px;
            left: -23px;
        }
    </style>
    <link href="../Autocomplete/jquery-ui.css" rel="stylesheet" />
    <script src="../Autocomplete/jquery-ui.js"></script>
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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="app-main__inner">
        <div class="app-page-title">
            <div class="page-title-wrapper">
                <div class="page-title-heading">
                    <div class="page-title-icon">
                        <i class="pe-7s-users icon-gradient bg-mean-fruit"></i>
                    </div>
                    <div>
                        Deleted Bill History
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


        <div class="row">
            <div class="col-lg-12">
                <div class="main-card mb-3 card">
                    <div class="card-body">
                        <h5 class="card-title"></h5>
                        <div class="form-row">

                            <div class="col-md-7">

                                <table style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%;">
                                    <tr>


                                        <td>Start Date
                                        </td>
                                        <td  >
                                             <asp:TextBox ID="txt_date" runat="server" CssClass="form-control calender-icon"></asp:TextBox>
                                    
                                        </td>
                                        <td>End Date
                                        </td>
                                        <td>
                                              <asp:TextBox ID="txt_enddate" runat="server" CssClass="form-control calender-icon"></asp:TextBox>
                                  
                                        </td>




                                        <td>

                                            <asp:Button ID="btn_find_date" runat="server" Text="Find" CssClass="btn btn-primary form-fnd-btns" OnClick="btn_find_date_Click" />
                                        </td>






                                    </tr>

                                </table>


                            </div>
                            <div class="col-md-5">

                                <table style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%;">
                                    <tr>


                                        <td>Session
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddl_session" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </td>
                                        <td>Admission No.
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_admission_no" runat="server" class="form-control find-dv-txtbx"></asp:TextBox>
                                        </td>




                                        <td>

                                            <asp:Button ID="btn_find_admission_no" runat="server" Text="Find" CssClass="btn btn-primary form-fnd-btns" OnClick="btn_find_admission_no_Click" />
                                        </td>






                                    </tr>

                                </table>
                            </div>

                            <div class="col-md-12">
                                <div style="margin: 0px; float: left; height: auto; width: 100%;">
                                    <div style="margin: 0px; padding: 0%; float: left; height: auto; width: 100%" id="pnl_payment_history" runat="server" visible="false">
                                        <table class="table">

                                            <tr>
                                                <td colspan="7"><b>Deleted Bill History</b>

                                                </td>
                                                <td>
                                                     <asp:LinkButton ID="btn_excels" runat="server" Style="margin: 3px 0px 2px 0px;
    float: right;" OnClick="btn_excels_Click" class="btn btn-primary find-dv-btn">  <i class='bx bx-download'></i> Excel</asp:LinkButton>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="8">
                                                    <asp:Label ID="lbl_msg" runat="server" Font-Bold="true" ForeColor="Black"></asp:Label>

                                                    <asp:GridView ID="grd_fee" runat="server" AutoGenerateColumns="False" Style="width: 100%" class="table table-striped table-bordered dataTable" ShowFooter="True">
                                                        <Columns>

                                                            <asp:TemplateField HeaderText="Sl. No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_sln" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Slip No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_slipno" runat="server" Text='<%#Bind("Slip_no") %>'></asp:Label>



                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Addmission No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Addmission_no" runat="server" Text='<%#Bind("Addmission_no") %>'></asp:Label>

                                                                </ItemTemplate>

                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Session">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Session" runat="server" Text='<%#Bind("Session") %>'></asp:Label>

                                                                </ItemTemplate>

                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_date" runat="server" Text='<%#Bind("Date") %>'></asp:Label>

                                                                </ItemTemplate>

                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Payment Mode">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_paymenetmode" runat="server" Text='<%#Bind("mode") %>'></asp:Label>

                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Type" runat="server" Text='<%#Bind("Type") %>'></asp:Label>

                                                                </ItemTemplate>

                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Total Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Amount" runat="server" Text='<%#Bind("Amount","{0:n}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="td2" Width="100px" />
                                                                <HeaderStyle CssClass="td2" />

                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete By">
                                                                <ItemTemplate>

                                                                    <asp:Label ID="lbl_Type" runat="server" Text='<%#Bind("Insert_time_user_id") %>'></asp:Label>


                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="td2" Width="100px" />
                                                                <HeaderStyle CssClass="td2" />

                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Date time">
                                                                <ItemTemplate>

                                                                    <asp:Label ID="lbl_Type" runat="server" Text='<%#Bind("insert_time_date1") %>'></asp:Label>


                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="td2" Width="100px" />
                                                                <HeaderStyle CssClass="td2" />

                                                            </asp:TemplateField>


                                                        </Columns>
                                                    </asp:GridView>


                                                </td>
                                            </tr>



                                        </table>

                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>



    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
