<%@ Page Title="" Language="C#" MasterPageFile="~/Dvlpr_Prof/Site1.Master" AutoEventWireup="true" CodeBehind="Check_Update.aspx.cs" Inherits="school_web.Dvlpr_Prof.Check_Update" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Check For Update
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .btn {
            padding: 3px 10px 3px 10px !important;
        }

        table.table-bordered.dataTable tbody th, table.table-bordered.dataTable tbody td {
            border-bottom-width: 0;
            text-align: center !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="app-main__inner">
        <div class="app-page-title">
            <div class="page-title-wrapper">
                <div class="page-title-heading">
                    <div class="page-title-icon">
                        <i class="pe-7s-rocket icon-gradient bg-mean-fruit"></i>
                    </div>
                    <div>
                        Update Info(<asp:Label ID="lbl_updateed_version" runat="server"></asp:Label>)
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
                            <div class="col-md-12">



                                <div style="margin: 0px; float: left; height: auto; width: 100%;">
                                    <div style="margin: 0px; padding: 0%; float: left; height: auto; width: 100%" id="pnl_payment_history" runat="server" visible="false">
                                        <table class="table">

                                            <tr>
                                                <td colspan="8">
                                                    <asp:GridView ID="GridView1_update" runat="server" AutoGenerateColumns="False" Style="width: 100%" class="table table-bordered dataTable" ShowFooter="false">
                                                        <Columns>

                                                            <asp:TemplateField HeaderText="Sl. No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_sln" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="User">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_User" runat="server" Text='<%#Bind("Update_By")%>'></asp:Label>

                                                                </ItemTemplate>

                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Current ERP Version">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Update_version" runat="server" Text='<%#Bind("Update_version")%>'></asp:Label>

                                                                </ItemTemplate>

                                                            </asp:TemplateField>



                                                            <asp:TemplateField HeaderText="Previous Version">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_PreviousVersion" runat="server" Text='<%#Bind("Previous_Version")%>'></asp:Label>

                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Update Date Time">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Finsiah_date_time" runat="server" Text='<%#Bind("Finsiah_date_time1")%>'></asp:Label>

                                                                </ItemTemplate>

                                                            </asp:TemplateField>







                                                            <asp:TemplateField HeaderText="Update Specification">
                                                                <ItemTemplate>

                                                                    <asp:LinkButton ID="lnk_view_Update_Note1" CssClass="btn btn-info form-fnd-btns" runat="server" CausesValidation="false" OnClick="lnk_view_Update_Note1_Click" ToolTip="Details"> <i class="bx bxs-detail"></i><span>View Update Note</span></asp:LinkButton>
                                                                    <asp:Label ID="lbl_Update_Note" runat="server" Text='<%#Bind("Update_note")%>' Visible="false"></asp:Label>

                                                                </ItemTemplate>

                                                            </asp:TemplateField>


                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="8">


                                                    <asp:GridView ID="grd_fee" runat="server" AutoGenerateColumns="False" Style="width: 100%" class="table table-bordered dataTable" ShowFooter="false" OnRowDataBound="grd_fee_RowDataBound">
                                                        <Columns>

                                                            <asp:TemplateField HeaderText="Sl. No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_sln" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ERP Version">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Version_name" runat="server" Text='<%#Bind("Version_name")%>'></asp:Label>

                                                                </ItemTemplate>

                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Release Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Release_Date" runat="server" Text='<%#Bind("Release_Date")%>'></asp:Label>

                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Release Time">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Release_Time" runat="server" Text='<%#Bind("Release_Time")%>'></asp:Label>

                                                                </ItemTemplate>

                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Start Time">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Update_Time" runat="server" Text='<%#Bind("Update_Start_time")%>'></asp:Label>

                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="End Time">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_updateendtime" runat="server"></asp:Label>

                                                                </ItemTemplate>

                                                            </asp:TemplateField>




                                                            <asp:TemplateField HeaderText="Update Specification">
                                                                <ItemTemplate>

                                                                    <asp:LinkButton ID="lnk_view_Update_Note" CssClass="btn btn-info form-fnd-btns" runat="server" CausesValidation="false" OnClick="lnk_view_Update_Note_Click" ToolTip="Details"> <i class="bx bxs-detail"></i><span>View Update Note</span></asp:LinkButton>
                                                                    <asp:Label ID="lbl_Update_Note" runat="server" Text='<%#Bind("Update_Note")%>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lbl_duration" runat="server" Text='<%#Bind("Update_duration_hours")%>' Visible="false"></asp:Label>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Action">
                                                                <ItemTemplate>


                                                                    <asp:Button ID="btn_update_data" runat="server" Text="Click here to initiate the update process" CssClass="btn btn-primary form-fnd-btns" Visible="false" OnClick="btn_update_data_Click" OnClientClick="return confirm('Are you sure you want to update this?');" />
                                                                    <asp:Label ID="lbl_Version_count" runat="server" Text='<%#Bind("Version_count")%>' Visible="false"></asp:Label>



                                                                </ItemTemplate>

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
    <script type="text/javascript">
        function openModal() {
            $('#myModal').modal('show');

        }
    </script>
    <div class="modal fade" id="myModal" role="dialog" style="top: 60px">
        <div class="modal-dialog md-width" style="max-width: 600px;">
            <!-- Modal content-->
            <div class="modal-content" style="position: relative;">
                <div class="modal-header">
                    <h3 class="modal-title" style="font-size: 20px;">
                        <asp:Label ID="lbl_data_heading" runat="server" Text="Release Note">

                        </asp:Label></h3>

                    <%-- <a href="#!" data-dismiss="modal" style="margin: 5px 0px 0px 0px !important; padding: 3px 5px 4px 5px; background: #fa2020; border: #9b0202;"
                        class="btn btn-primary find-dv-btn" OnClientClick="return close()" >Close</a>--%>
                    <%-- <a href="#!" data-dismiss="modal" style="margin: 5px 0px 0px 0px !important; padding: 3px 5px 4px 5px; background: #fa2020; border: #9b0202;"
                        class="btn btn-primary find-dv-btn" OnClientClick="return close()" >Close</a>--%>

                    <asp:LinkButton ID="LinkButton1" Style="margin: 5px 0px 0px 0px !important; padding: 3px 5px 4px 5px; background: #fa2020; border: #9b0202;"
                        class="btn btn-primary find-dv-btn" runat="server">Close</asp:LinkButton>
                </div>
                <div class="modal-body md-bdy" style="height: 300px; overflow: auto;">
                    <div class="row g-3 needs-validation" novalidate="">
                        <div class="col-md-12">

                            <asp:Label ID="lbl_data" runat="server">

                            </asp:Label>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
