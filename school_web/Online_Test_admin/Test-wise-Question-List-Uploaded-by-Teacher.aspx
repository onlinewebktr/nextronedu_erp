<%@ Page Title="" Language="C#" MasterPageFile="~/Online_Test_admin/Admin.Master" AutoEventWireup="true" CodeBehind="Test-wise-Question-List-Uploaded-by-Teacher.aspx.cs" Inherits="school_web.Online_Test_admin.Test_wise_Question_List_Uploaded_by_Teacher" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">Question Verification
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
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
                <div class="breadcrumb-title pe-3">Exam Setting</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Question Verification</li>
                        </ol>
                    </nav>
                </div>


            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-12">
                    <h6 class="mb-0 text-uppercase"></h6>
                    <hr />
                    <div class="card">
                        <div class="card-body">

                            <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5 hdrmodify">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="find-dv">
                                            <div class="row">

                                                

                                                <div class="col-sm-2">
                                                    <label for="validationCustom01" class="find-dv-lbl">Test Name</label>
                                                    <asp:DropDownList ID="ddl_Test_name" runat="server" class="form-select find-dv-txtbx" >
                                                    </asp:DropDownList>
                                                </div>
 



                                                <div class="col-sm-6">
                                                    <asp:Button ID="btn_find" runat="server" Text="Find"    class="btn btn-primary find-dv-btn" OnClick="btn_find_Click" />

                                                </div>
                                                <div class="col-sm-2">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="grd-wpr">
                                            <div id="tblPrintIQ" runat="server">
                                                <div class="pgslry-head-div head" style="border-bottom: 1px solid #000; margin: 0px; float: left; width: 100%;">
                                                    <div style="margin: 0px; padding: 0px; height: 110px; width: 20%; float: left;">
                                                        <asp:Image ID="imglogo" runat="server" Style="height: 100px; width: 100px; margin: 0px 0px 0px 10px;" />
                                                    </div>
                                                    <div style="margin: 0px; padding: 0px; height: 110px; width: 80%; float: left;">
                                                        <h1 style="margin: 0px 0px 0px 0px; width: 100%; padding: 0px; font-weight: bold; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 20px; text-decoration: underline;">
                                                            <asp:Label ID="lbl_heading" runat="server"></asp:Label>
                                                        </h1>
                                                        <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                            <asp:Label ID="lbl_address" runat="server"></asp:Label>
                                                        </div>
                                                        <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                            <span style="font-size: 14px; font-weight: bold;">Question Upload Form Mobile  <asp:Label ID="lbl_class22" runat="server"></asp:Label></span>
                                                        </div>
                                                    </div>
                                                </div>

                                                <asp:Panel ID="Panel1" runat="server">
                                                    <table id="datatable" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                                        <thead>
                                                            <tr>
                                                                <th>#</th>
                                                                <th class="hiddenOnPrint">Action</th>
                                                                <th>Class</th>
                                                                <th>Test Name</th>
                                                                <th>Live Date</th>
                                                                <th>Live Time</th>
                                                                <th>Paper Time (Minutes)</th>
                                                                <th>No. of Que.</th>
                                                                <th>Status</th>
                                                                
                                                                   
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:Repeater ID="rd_view" runat="server" OnItemDataBound="rd_view_ItemDataBound">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                                        </td>
                                                                        <td class="hiddenOnPrint">
                                                                            <div class="user-box dropdown" style="float: left; display: inherit; height: auto; border-left: 0px solid #f0f0f0; margin-left: 0px;">
                                                                                <a class="d-flex align-items-center nav-link dropdown-toggle dropdown-toggle-nocaret" style="font-size: 29px; padding: 0px; border: 0px;"
                                                                                    href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                                                                                    <div class="user-info ps-3" style="padding: 0px !important; border: 0px !important;">
                                                                                        <i class="bx bx-grid-horizontal"></i>
                                                                                    </div>
                                                                                </a>
                                                                                <ul class="dropdown-menu dropdown-menu-end">
                                                                                       
                                                                               
                                                                                   


                                                                                     <li>
                                                                                        <asp:Panel ID="Panel2" runat="server">
     

                  <a class="dropdown-item" 
   href='<%# "../OnlineTest/view_uploaded_question?regid=" + ViewState["Userid"].ToString() + "&testid=" + Eval("Entry_id") + "&type=admin" %>' 
   target="_blank">
   <i class='bx bx-notepad' style="margin-right: 3px;"></i>Question Details
</a>
                                                                                        </asp:Panel>
                                                                                    </li>

                                                                                    <li>
                                                                                        <asp:Panel ID="Panel3" runat="server" Visible="false">
     

                  <a class="dropdown-item" 
   href='<%# "../OnlineTest/Upload_Question?regid=" + ViewState["Userid"].ToString() + "&testid=" + Eval("Entry_id") + "&type=admin" %>' 
   target="_blank">
   <i class='bx bx-notepad' style="margin-right: 3px;"></i>Add Question
</a>
                                                                                        </asp:Panel>
                                                                                    </li>




                                                                                       <li>
                                                                                        <asp:LinkButton ID="lnkDel" class="dropdown-item" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"> Delete All Question</i></asp:LinkButton>
                                                                                    </li>


                                                                                         <li>
                                                                                          <asp:Panel ID="Panel11" runat="server">
                                                                                            <a class="dropdown-item" href="Question_Verification.aspx?examid=<%#Eval("Entry_id") %>" target="_blank"><i class='bx bx-check-shield'>Question Veriication</i> </a>
                                                                                        </asp:Panel>
                                                                                    </li>
                                                                                </ul>
                                                                            </div>


                                                                        </td>
                                                                         



                                                                        <td style="text-align: left;">
                                                                             <asp:Label ID="lbl_subject" Visible="false" runat="server" Text='<%#Bind("subjectname1")%>'></asp:Label>
                                                                              <asp:Label ID="lbl_subjectname_view" runat="server" Visible="false" ></asp:Label>
                                                                             <asp:Label ID="lbl_session" Visible="false" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                            <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("Course_Name")%>'></asp:Label>
                                                                        </td>

                                                                      

                                                                       
                                                                        
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_exam_name" runat="server" Text='<%#Bind("Exam_name")%>'></asp:Label>
                                                                        </td>


                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_livedate" runat="server" Text='<%#Bind("live_date_one")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="live_time" runat="server" Text='<%#Bind("live_time_one")%>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lbl_exam_duration" runat="server" Text='<%#Bind("Exam_duration")%>'></asp:Label>
                                                                        </td>

                                                                      

                                                                        <td style="text-align: left;">

                                                                            <asp:Label ID="lbl_no_question" runat="server" Text='<%#Bind("toquestion")%>'></asp:Label>
                                                                        </td>


                                                                        <td style="text-align: left;">

                                                                            <asp:Label ID="lbl_Status" runat="server" Text='<%#Bind("Status")%>'></asp:Label>
                                                                            <asp:Label ID="lbl_Session_id" Visible="false" runat="server" Text='<%#Bind("Session_id")%>'></asp:Label>
                                                                            <asp:Label ID="lbl_Class_id" Visible="false" runat="server" Text='<%#Bind("Class_id")%>'></asp:Label>
                                                                            <asp:Label ID="lbl_Exam_id" Visible="false" runat="server" Text='<%#Bind("Entry_id")%>'></asp:Label>
                                                                            <asp:Label ID="lbl_id" Visible="false" runat="server" Text='<%#Bind("Id")%>'></asp:Label>
                                                                             <asp:Label ID="lblquestion_uploadstatus" Visible="false" runat="server"></asp:Label>
                                                                              
                                                                        </td>
                                                                        
                                                                           
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </tbody>
                                                    </table>
                                                </asp:Panel>
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
