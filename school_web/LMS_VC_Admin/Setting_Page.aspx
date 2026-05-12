<%@ Page Title="" Language="C#" MasterPageFile="~/LMS_VC_Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Setting_Page.aspx.cs" Inherits="school_web.LMS_VC_Admin.Setting_Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Setting
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="app-main__inner">
        <div class="app-page-title">
            <div class="page-title-wrapper">
                <div class="page-title-heading">
                    <div class="page-title-icon">
                        <i class="pe-7s-settings icon-gradient bg-mean-fruit"></i>
                    </div>
                    <div>
                        <asp:Literal ID="ltUsertop" runat="server">Setting</asp:Literal>

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
                                    <label>School Name</label>
                                    <asp:TextBox ID="txt_schoolname" runat="server" CssClass="form-control"></asp:TextBox>

                                </div>
                                <div class="clearfix"></div>
                                <div class="position-relative form-group">
                                    <label>School Logo</label>
                                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" />

                                </div>
                                <div class="clearfix"></div>
                                <div class="position-relative form-group">
                                    <label>Footer Copy Right</label>
                                    <asp:TextBox ID="txt_footer_copyright" runat="server" CssClass="form-control"></asp:TextBox>

                                </div>
                                <div class="clearfix"></div>
                                <div class="position-relative form-group">
                                    <label>Push API Key</label>
                                    <asp:TextBox ID="txt_apikey" runat="server" CssClass="form-control" TextMode="MultiLine" Style="height: 100px;"></asp:TextBox>

                                </div>
                                <div class="clearfix"></div>
                                <div class="position-relative form-group">
                                    <label>Push Sender ID </label>
                                    <asp:TextBox ID="txt_push_sendid" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="position-relative form-group">
                                    <label>SMS API Key</label>
                                    <asp:TextBox ID="txt_smsapikey" runat="server" CssClass="form-control" ></asp:TextBox>

                                </div>
                                <div class="clearfix"></div>
                                <div class="position-relative form-group">
                                    <label>SMS Sender ID </label>
                                    <asp:TextBox ID="txt_sms_senderkey" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>

                                <div class="clearfix"></div>
                                <div class="position-relative form-group">
                                    <label>Student Send Password Template </label>
                                    <asp:TextBox ID="txt_studentsms" runat="server" CssClass="form-control" TextMode="MultiLine" Style="height: 100px;"></asp:TextBox>

                                </div>

                                <div class="clearfix"></div>
                                <div class="position-relative form-group">
                                    <label>Teacher Send Password Template</label>
                                    <asp:TextBox ID="txt_teachersms" runat="server" CssClass="form-control" TextMode="MultiLine" Style="height: 100px;"></asp:TextBox>

                                </div>


                                <div class="clearfix"></div>
                                <div class="position-relative form-group">
                                    <label>Class & Subject </label>
                                    <asp:CheckBox ID="chk_class" runat="server" Text="Add Class" CssClass="chat-box form-control" />
                                    <asp:CheckBox ID="chk_subject" runat="server" Text="Add Subject" CssClass="chat-box form-control"/>
                                </div>
                                <div class="clearfix"></div>
                                <div class="position-relative form-group">
                                    <label>Teacher & Student</label>
                                    <asp:CheckBox ID="chk_teacher" runat="server" Text="Add Teacher" CssClass="chat-box form-control" />
                                    <asp:CheckBox ID="chk_student" runat="server" Text="Add Student" CssClass="chat-box form-control" />
                                </div>
                                <div class="clearfix"></div>
                                <div class="position-relative form-group">
                                    <label>Financial Report</label>
                                    <asp:CheckBox ID="chk_financial" runat="server" Text="Online Payment List" CssClass="chat-box form-control" />
                                  
                                </div>
                                <asp:Button ID="btn_Submit" runat="server" Text="Add" class="mt-2 btn btn-primary" OnClick="btn_Submit_Click" />

                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-4">
                <div class="card-body">
                    <h5 class="card-title"></h5>
                    <div class="form-row">
                        <table style="margin: 0px; padding: 0px; float: left; height: auto; width: 100%; width: 100%" class="table-bordered table">
                            <tr>
                                <td style="padding: 2px 2px 2px 2px; width: 100px;">School Logo

                                </td>
                                <td>
                                     <img id="logo" runat="server" style="height:150px; width:150px;" />
                                </td>
                            </tr>

                        </table>
                    </div>
                </div>

            </div>

        </div>
    </div>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
