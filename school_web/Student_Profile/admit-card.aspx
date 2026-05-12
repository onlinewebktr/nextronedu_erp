<%@ Page Title="" Language="C#" MasterPageFile="~/Student_Profile/main.Master" AutoEventWireup="true" CodeBehind="admit-card.aspx.cs" Inherits="school_web.Student_Profile.admit_card" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Admit Card
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script language="javascript">
        var popupWindow = null;
        function positionedPopup(url, winName, w, h, t, l, scroll) {
            settings =
                'height=' + h + ',width=' + w + ',top=' + t + ',left=' + l + ',scrollbars=' + scroll + ',resizable'
            popupWindow = window.open(url, winName, settings)
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pagemainhh">
        <div class="container">
            <div id="notification">
                <div id="pan" class="notificationpan">
                    <div id="success" runat="server" visible="false" style="float: left; width: 100%; height: auto;" class="alert alert-success border-0 bg-success alert-dismissible fade show py-2">
                        <div class="d-flex align-items-center">
                            <div class="font-35 text-white">
                                <i class='bx bxs-check-circle'></i>
                            </div>
                            <div class="ms-3">
                                <asp:Label ID="lbl_success" runat="server" ForeColor="White" class="text-white"></asp:Label>
                            </div>
                        </div>
                        <asp:LinkButton ID="LinkButton1" class="btn-closes" runat="server" Style="color: #fff">X</asp:LinkButton>
                    </div>

                    <div id="warning" runat="server" visible="false" class="alert alert-warning border-0 bg-warning alert-dismissible fade show py-2">
                        <div class="d-flex align-items-center">
                            <div class="font-35 text-dark">
                                <i class='bx bx-info-circle'></i>
                            </div>
                            <div class="ms-3">
                                <asp:Label ID="lbl_warning" runat="server" ForeColor="White" class="text-white"></asp:Label>
                            </div>
                        </div>
                        <asp:LinkButton ID="LinkButton2" class="btn-closes" runat="server" Style="color: #fff">X</asp:LinkButton>
                    </div>
                </div>
            </div>

            <div class="main-card mb-3 card">
                <div class="card-header">
                    <h4 class="card-title">Admit Card</h4>
                </div>
                <div class="card-body">
                    <div class="headingtablee">
                        <div class="row">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <div class="table-responsive">
                                    <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                        <div class="row">
                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                <div id="Div1" class="grd-wpr" runat="server">
                                                    <div id="content">
                                                        <table id="datatable" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                            <thead>
                                                                <tr>
                                                                    <th>#</th>
                                                                    <th>Session</th>
                                                                    <th>Term</th>
                                                                    <th>Exam/Assesment Name</th>
                                                                    <th>Action</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <asp:Repeater ID="rd_view" runat="server" OnItemDataBound="rd_view_ItemDataBound">
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("Session")%>'></asp:Label>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <asp:Label ID="lbl_term_name" runat="server" Text='<%#Bind("Term_name")%>'></asp:Label>
                                                                            </td>


                                                                            <td style="text-align: left;">
                                                                                 <asp:Label ID="lbl_Assesment_name" runat="server"   Text='<%#Bind("Assesment_name")%>'></asp:Label>
                                                                                <asp:Label ID="lbl_session_id" runat="server" Visible="false" Text='<%#Bind("Session_id")%>'></asp:Label>
                                                                                <asp:Label ID="lbl_class_id" runat="server" Visible="false" Text='<%#Bind("Class_id")%>'></asp:Label>
                                                                                <asp:Label ID="lbl_Exam_id" runat="server" Visible="false" Text='<%#Bind("Exam_id")%>'></asp:Label>
                                                                                <asp:Label ID="lbl_term_id" runat="server" Visible="false" Text='<%#Bind("Term_id")%>'></asp:Label>
                                                                            </td>

                                                                            <td style="text-align: left;">
                                                                                <a id="rpcard_link" onclick="positionedPopup(this.href,'myWindow','950','650','200','200','yes');return false" runat="server" style="background-color: #e14eca; color: #fff; padding: 2px 5px 2px 5px; width: auto; border-radius: 2px; font-weight: 500; display: inherit;"><i class='bx bx-happy-heart-eyes'></i><span>View Progress Report</span> </a>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                                <div id="Div2" class="grd-wpr" runat="server" visible="false">
                                                    <p>Sorry! No admit cards have been published here.</p>
                                                </div>



                                            </div>
                                        </div>
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
    <asp:HiddenField ID="hd_session_id" runat="server" />
    <asp:HiddenField ID="hd_class" runat="server" />
    <asp:HiddenField ID="hd_section" runat="server" />
    <asp:HiddenField ID="hd_term_id" runat="server" />
    <asp:HiddenField ID="hd_exam_id" runat="server" />
    <asp:HiddenField ID="hd_shift" runat="server" />
    <asp:HiddenField ID="hd_admission_no" runat="server" />
    <asp:HiddenField ID="hd_Branch_id" runat="server" />
</asp:Content>
