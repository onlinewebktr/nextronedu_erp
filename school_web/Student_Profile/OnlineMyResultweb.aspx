<%@ Page Title="" Language="C#" MasterPageFile="~/Student_Profile/main.Master" AutoEventWireup="true" CodeBehind="OnlineMyResultweb.aspx.cs" Inherits="school_web.Student_Profile.OnlineMyResultweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">My Online Test Result
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
       <style>
        .clndr-icon {
            font-size: 14px !important;
            color: #ff2956;
            position: absolute;
            top: 8px;
            left: 75px;
        }

        .clndr-icon {
            font-size: 11px !important;
            color: #ff2956;
            position: absolute;
            top: 10px;
            right: 3px;
            left: auto;
        }


        .table td {
            text-align: left!important;
        }
    </style>
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
                    <h4 class="card-title">My Online Test Result </h4>
                </div>
                <div class="card-body" style="padding-top: 0px;">
                    <div class="headingtablee">

                        <div style="padding: 5px 10px 5px 10px; width: 100%; float: left; text-align: center;">
                            <asp:Label ID="lbl_msg2" runat="server" Font-Bold="True" ForeColor="Red" Style="font-size: 18px;"></asp:Label>
                        </div>

                        <div class="row">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">

                                <div class="1">
                                    <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                        <div class="row">
                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                <div class="grd-wpr">
                                                    <div id="content">
                                                         <table id="datatable" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                        <thead>
                                            <tr>
                                                <th>SL.No. </th>
                                                <th>Exam Name</th>
                                                <th>Subject</th>
                                                <th>Exam Date</th>
                                                <th>Exam Duration</th>
                                                <th>No. of Question</th>
                                                <th>No. of Correct ans </th>
                                                <th>Full Marks </th>
                                                <th>Obtains Marks </th>
                                                <th class="hiddenOnPrint">Action</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="RPDetails" runat="server" OnItemDataBound="RPDetails_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:Label ID="lbl_Exam_name" runat="server" Text='<%#Bind("Exam_name")%>'></asp:Label>

                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:Label ID="lbl_subject"   runat="server" Visible="false" Text='<%#Bind("subject_name") %>'></asp:Label>
                                                            <asp:Label ID="lbl_subjectview"   runat="server"></asp:Label>
                                                        </td>


                                                        <td style="text-align: left;">
                                                            <asp:Label ID="lbltestdate"  runat="server" Text='<%#Bind("live_date1") %>'></asp:Label>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:Label ID="lbl_Duration" runat="server" Font-Bold="true" Text='<%#Bind("Exam_duration") %>'></asp:Label>
                                                            <asp:Label ID="lbl12" runat="server"   >(In minutes)</asp:Label>
                                                        </td>

                                                        <td style="text-align: left;">
                                                            <asp:Label ID="lbl_noofquestion"  runat="server" Text='<%#Bind("noofquestion") %>'></asp:Label>
                                                        </td>

                                                        <td style="text-align: left;">
                                                            <asp:Label ID="lbl_Correct_answer" runat="server" Text='<%#Bind("Correct_answer") %>'></asp:Label>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:Label ID="lbl_Full_Marks" runat="server" Text='<%#Bind("Full_Marks") %>'></asp:Label>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:Label ID="lbl_Obtains_Marks" runat="server" Text='<%#Bind("Obtains_Marks") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btn_view_result" runat="server" Text="View Result" CssClass="btn btn-info"  Style="margin: 5px 5px 5px 10px;margin: 5px 5px 5px 10px;
    background-color: #0c1d8b;
    color: #fff;" OnClick="btn_view_result_Click"   />
                                                            <asp:Button ID="btn_testans" runat="server" Text="Explanation View" CssClass="btn btn-warning" Style="margin: 5px 5px 5px 10px;background-color: #f37f63;
    color: #fff;" OnClick="btn_testans_Click"   />


                                                            <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lbl_testid" runat="server" Text='<%#Bind("Exam_id") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lbl_Exam_Id" runat="server" Text='<%#Bind("Exam_id") %>' Visible="false"></asp:Label>

                                                              
                                                             <asp:Label ID="lbl_icreated_date" runat="server" Text='<%#Bind("icreated_date") %>' Visible="false"></asp:Label>
                                                             <asp:Label ID="lbl_Attempt_id" runat="server" Text='<%#Bind("Attempt_id") %>' Visible="false"></asp:Label>


                                                            
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
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
                </div>
            </div>
        </div>
        <!--end row-->
    </div>

</asp:Content>
