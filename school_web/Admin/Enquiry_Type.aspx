<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="Enquiry_Type.aspx.cs" Inherits="school_web.Admin.Enquiry_Type" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
   Create Enquiry Head
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style>
        .box {
            position: relative;
            border-radius: 3px;
            background: none;
            margin-bottom: 10px;
            width: 100%;
            box-shadow: 0 1px 1px rgba(0,0,0,0.1);
        }

        .tablists {
            margin: 0;
            padding: 0;
            list-style: none;
        }

            .tablists li {
                display: block;
                border-bottom: 1px solid #ddd;
            }

                .tablists li a {
                    color: #444;
                    padding: 10px;
                    display: block;
                }

                    .tablists li a.active {
                        
                        text-decoration: underline;
                        color: #ffffff;
                        text-decoration: underline;
                        background: #1c77fd;
                        padding: 5px 0px 7px 4px;
                    }

                    .tablists li a.deactive {
                        color: #444;
                        text-decoration: none;
                        padding: 5px 0px 7px 4px;
                    }

        .box-header.with-border {
            border-bottom: 1px solid #b6aeae;
        }

        .box-header {
            color: #444;
            display: block;
            padding: 1px 0px 3px 8px;
            position: relative;
            width: 99%;
            margin-left: 4px;
        }

            .box-header > .fa, .box-header > .glyphicon, .box-header > .ion, .box-header .box-title {
                display: inline-block;
                font-size: 16px;
                margin: 0;
                line-height: 1;
            }

        .buttons-print {
            display: none;
        }

        .buttons-copy {
            display: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--start page wrapper -->
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

            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                <div class="breadcrumb-title pe-3">Front Office</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">Create Enquiry Head</li>
                        </ol>
                    </nav>
                </div>
            </div>



            <asp:HiddenField ID="HdID" runat="server" />
            <div class="row">
                <div class="col-xl-3">
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation" novalidate="">
                                    <div class="box border0">
                                        <ul class="tablists">
                                            <li>
                                                <asp:LinkButton ID="lnk_Purpose" runat="server" OnClick="lnk_Purpose_Click">Purpose</asp:LinkButton></li>

                                            <li style="display:none">
                                                <asp:LinkButton ID="lnk_Source" runat="server" OnClick="lnk_Source_Click">Source</asp:LinkButton></li>
                                            <li>
                                                <asp:LinkButton ID="lnk_Reference" runat="server" OnClick="lnk_Reference_Click">Reference</asp:LinkButton></li>


                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
                <div class="col-xl-3">

                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation" novalidate="">
                                    <div class="box-header with-border" style="width: 97%;">
                                        <h3 class="box-title">
                                            <asp:Label ID="lbl_type" runat="server"></asp:Label></h3>
                                    </div>

                                    <div class="col-md-12">
                                        <label for="validationCustom01" class="form-label">
                                            <asp:Label ID="lbl_type_head" runat="server"></asp:Label><sup>*</sup></label>
                                        <asp:TextBox ID="txt_name" runat="server" class="form-control"></asp:TextBox>
                                    </div>



                                    <div class="col-12">
                                        <asp:Button ID="btn_Submit" runat="server" Text="Add" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btn_Submit_Click1" />
                                        <asp:Button ID="btn_cancel" runat="server" Text="Cancel" class="btn btn-dark" Visible="false" CausesValidation="false" OnClick="btn_cancel_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


                <div class="col-xl-6">
                    <div class="card">
                        <div class="card-body">
                            <div class="p-4 border rounded">
                                <div class="row g-3 needs-validation">
                                    <div class="box-header with-border">
                                        <h3 class="box-title">
                                            <asp:Label ID="lbl_type1" runat="server"></asp:Label></h3>
                                    </div>
                                    <div style="margin: 0px; padding: 0px; float: left; height: auto">
                                        <asp:LinkButton ID="btn_excels" runat="server" Style="margin: 5px 0px 0px 10px !important;" OnClick="btn_excels_Click" class="btn btn-primary find-dv-btn">  <i class='bx bx-download'></i> Excel</asp:LinkButton>
                                    </div>
                                    <asp:Panel ID="Panel1" runat="server" style="margin: 0px;">
                                        <table id="example21" class="table table-striped table-bordered dataTable" role="grid" aria-describedby="example2_info">
                                            <thead>
                                                <tr>
                                                    <th>#</th>

                                                    <th>
                                                        <asp:Label ID="lbl_head" runat="server"></asp:Label></th>
                                                    <th>Action</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rd_view" runat="server">
                                                    <ItemTemplate>

                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                                            </td>
                                                            <td style="text-align: left;">
                                                                <asp:Label ID="lbl_name" runat="server" Text='<%#Bind("Enquiry_Type")%>'></asp:Label>
                                                            </td>


                                                            <td style="text-align: left;">
                                                                <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" OnClick="lnkEdit_Click" ToolTip="Edit"> <i class="lni lni-pencil-alt"> </i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDel" runat="server" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this?');" CausesValidation="false" OnClick="lnkDel_Click"><i class="lni lni-trash"> </i></asp:LinkButton>
                                                                <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id")%>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lbl_Enquiry_Type_Id" runat="server" Text='<%#Bind("Enquiry_Type_Id")%>' Visible="false"></asp:Label>

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
        <!--end row-->
    </div>

    <!--end page wrapper -->
    <asp:HiddenField ID="hd_id" runat="server" />

</asp:Content>
