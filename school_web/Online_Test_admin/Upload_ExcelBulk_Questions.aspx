<%@ Page Title="" Language="C#" MasterPageFile="~/Online_Test_admin/Admin.Master" AutoEventWireup="true" CodeBehind="Upload_ExcelBulk_Questions.aspx.cs" Inherits="school_web.Online_Test_admin.Upload_ExcelBulk_Questions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Upload Bulk Questions Via Excel
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
                <div class="breadcrumb-title pe-3">Exam Setup</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Upload Bulk Question</li>
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
                                    <div class="col-sm-7">
                                        <table border="0" style="width: 100%; margin: 0px auto; padding: 0px; float: none" class="table table-bordered">


                                            <tr>
                                                <td class="lefttd" style="width: 212px;">Exam Name<sup> </sup>
                                                </td>
                                                <td class="righttd">
                                                    <asp:Label ID="lbl_exmaname" runat="server"></asp:Label>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lefttd" style="width: 212px;">Class <sup></sup>
                                                </td>
                                                <td class="righttd">
                                                    <asp:Label ID="lbl_classname" runat="server"></asp:Label>


                                                </td>
                                            </tr>

                                            <tr>
                                                <td class="lefttd" style="width: 212px; width: 185px;">Subject <sup></sup>
                                                </td>
                                                <td class="righttd">
                                                    <asp:Label ID="lbl_subject" runat="server"></asp:Label>


                                                </td>
                                            </tr>

                                            <tr style="display: none">
                                                <td class="lefttd" style="width: 15px; width: 185px;">Default Language 
                                                </td>
                                                <td class="righttd">
                                                    <asp:Label ID="lbl_primerylanguage" runat="server">English</asp:Label>

                                                </td>
                                            </tr>

                                            <tr>
                                                <td class="lefttd" style="width: 15px; width: 185px;">Section </td>
                                                <td class="righttd">
                                                  <asp:DropDownList ID="ddl_section" runat="server" class="form-select">
                                </asp:DropDownList>

                                                </td>
                                            </tr>

                                            <tr>
                                                <td class="lefttd" style="width: 15px; width: 185px;">Chose CSV Excel Question <sup>*</sup>
                                                </td>
                                                <td class="righttd">
                                                    <asp:FileUpload ID="FileUpload1" runat="server" Style="margin: 0px; float: left; float: left" />
                                                </td>
                                            </tr>


                                            <tr>
                                                <td colspan="2" style="padding: 10px 0px 10px 0px; text-align: center;">

                                                    <asp:Button ID="btn_s_add" runat="server"
                                                        Height="31px" Text="Upload" Width="80px" ValidationGroup="a"
                                                        OnClick="btn_s_add_Click" CssClass="mt-2 btn btn-primary" />

                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="padding: 10px 0px 10px 0px; text-align: center;">

                                                    <asp:Label ID="lblmsg" runat="server" Style="color: #d81515;"></asp:Label>

                                                </td>
                                            </tr>



                                        </table>
                                    </div>

                                    <div class="col-sm-5">
                                        <div class="card">
                                            <div class="card-body">
                                                <div class="p-4 border rounded">
                                                    <div class="row g-3 needs-validation" novalidate="">
                                                        <div class="col-md-12">
                                                            <a href="Doc/Bulk_question_CSV.csv" style="font-size: 12px;" download="" class="btnbtn btn-1 btn-sep icon-cart">Download Question format CSV format </a>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </div>

                                    <div class="col-sm-12">
                                        <asp:Panel ID="pnl_grd" runat="server" Visible="false">
                                            <div class="card">
                                                <div class="card-body">


                                                    <div class="col-md-12">
                                                        <h5 class="card-title" style="font-size: 15px;">Uploaded Question
                            <asp:Label ID="lbl_total_question" runat="server" Text="0" Style="float: right;"></asp:Label></h5>

                                                        <div class="row" style="overflow: hidden; padding: 0px; max-height: 1px;">
                                                            <asp:GridView ID="grvExcelData" runat="server" CssClass="table table-bordered table-striped" Width="100%" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                                                <HeaderStyle BackColor="#df5015" Font-Bold="true" ForeColor="White" />
                                                            </asp:GridView>
                                                        </div>

                                                        <div class="row" style="padding: 0px;">

                                                            <asp:GridView ID="GrdView" runat="server" class="mb-0 table table-bordered" AutoGenerateColumns="False" Width="100%" OnRowDataBound="GrdView_RowDataBound">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Sl No.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>

                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="80" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Question">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_Question_name" runat="server" Text='<%#Bind("Question_name")%>'></asp:Label>
                                                                            <asp:Label ID="lbl_id" Visible="false" runat="server" Text='<%#Bind("Id")%>'></asp:Label>
                                                                            <asp:Label ID="lbl_Status" Visible="false" runat="server" Text='<%#Bind("Status")%>'></asp:Label>
                                                                            <asp:Label ID="lbl_row_error" Visible="false" runat="server" Text='<%#Bind("row_error")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Option1">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_Option1" runat="server" Text='<%#Bind("Option1")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Option2">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_Option2" runat="server" Text='<%#Bind("Option2")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>


                                                                    <asp:TemplateField HeaderText="Option3">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_Option3" runat="server" Text='<%#Bind("Option3")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>


                                                                    <asp:TemplateField HeaderText="Option4">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_Option4" runat="server" Text='<%#Bind("Option4")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>


                                                                    <asp:TemplateField HeaderText="Answer Option">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_AnswerOption" runat="server" Text='<%#Bind("AnswerOption")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="118px" />
                                                                    </asp:TemplateField>


                                                                    <asp:TemplateField HeaderText="Marks">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_Marks" runat="server" Text='<%#Bind("Marks")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Explanation">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_Explanation" runat="server" Text='<%#Bind("Explanation")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Action">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_Objective_Entry_id" runat="server" Text='<%#Bind("Objective_Entry_id")%>' Visible="false"></asp:Label>
                                                                            <asp:LinkButton ID="lnkDel" runat="server" CssClass="mb-2 mr-2 btn btn-danger" OnClick="lnkDel_Click1" OnClientClick="return confirm('Are you sure want to delete?');" CausesValidation="false">Delete</asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>

                                                        </div>

                                                        <asp:Button ID="btn_final_butmit" runat="server" CssClass="btn btn-primary" Style="float: right; margin: 18px 0px 0px 0px;"
                                                            Text="Final Submit" Visible="false" OnClick="btn_final_butmit_Click" OnClientClick='return confirm("Are you sure want to final submit ?")' />
                                                    </div>

                                                </div>
                                            </div>
                                        </asp:Panel>

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
    <asp:HiddenField ID="hd_id" runat="server" />
    <asp:HiddenField ID="hd_userid" runat="server" />
    <asp:HiddenField ID="hd_type" runat="server" />
    <asp:HiddenField ID="hd_test_id" runat="server" />
    <asp:HiddenField ID="hd_question_id" runat="server" />
    <asp:HiddenField ID="hd_class_id" runat="server" />
    <asp:HiddenField ID="hd_exam_type" runat="server" />

    <asp:HiddenField ID="hd_marks" runat="server" />
    <asp:HiddenField ID="hd_sessionid" runat="server" />
    <asp:HiddenField ID="hd_section_id" runat="server" />

    <asp:HiddenField ID="hd_Objective_Entry_id" runat="server" />
</asp:Content>
