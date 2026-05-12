<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/main.Master" AutoEventWireup="true" CodeBehind="App_Install_Report.aspx.cs" Inherits="school_web.Admin.App_Install_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    App Installed Student List
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="https://fonts.googleapis.com/css2?family=Dosis:wght@200;300;400;500;600;700;800&family=Montserrat:ital,wght@0,300;0,400;0,500;0,600;0,700;0,800;0,900;1,200;1,300;1,400;1,500;1,600;1,700;1,800;1,900&family=Roboto:ital,wght@0,100;0,300;0,400;0,500;0,700;0,900;1,100;1,300;1,400;1,500;1,700;1,900&display=swap" rel="stylesheet"/><link href="../assets/css/Print.css" rel="stylesheet" type="text/css" />');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body ></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
        jQuery(".sn-bill-head-text").fitText(0.38);
    </script>
    <style>
        .head {
            display: none;
        }

        #pageFooter {
            display: none;
        }

        .home-grph-wpr {
            width: 114%;
        }
    </style>
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


            <div class="page-breadcrumb d-none d-sm-flex align-items-center mb-3">
                <div class="breadcrumb-title pe-3"><a href="student-report-home.aspx" class="backlnk-css"><i class="bx bx-arrow-back"></i></a>Report</div>
                <div class="ps-3">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb mb-0 p-0">
                            <li class="breadcrumb-item"><a href="Home.aspx"><i class="bx bx-home-alt"></i></a>
                            </li>
                            <li class="breadcrumb-item active" aria-current="page">App Installed List</li>
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
                                                        <label for="validationCustom01" class="find-dv-lbl">Session</label>
                                                        <asp:DropDownList ID="ddlsession" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Class</label>
                                                        <asp:DropDownList ID="ddlclass" runat="server" class="form-control find-dv-txtbx"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Section</label>
                                                        <asp:DropDownList ID="ddl_Section" runat="server" class="form-control find-dv-txtbx">
                                                            <asp:ListItem Value="0">ALL</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label for="validationCustom01" class="find-dv-lbl">Download Status</label>
                                                        <asp:DropDownList ID="ddl_downloadstatus" runat="server" class="form-control find-dv-txtbx">
                                                          
                                                            <asp:ListItem>All</asp:ListItem>
                                                            <asp:ListItem>Yes</asp:ListItem>
                                                            <asp:ListItem>No</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:Button ID="btn_find" runat="server" Text="Find" class="btn btn-primary find-dv-btn" OnClick="btn_find_Click" />
                                                     <asp:LinkButton ID="btn_excels" runat="server" Visible="false" OnClick="btn_excels_Click" class="btn btn-primary find-dv-btn">  <i class='bx bx-download'></i> Excel</asp:LinkButton>
                                                        <asp:LinkButton ID="print1" Visible="false" OnClientClick="return PrintPanel()" CssClass="btn btn-primary find-dv-btn" Style="margin-left: 10px;" runat="server"
                                                            ToolTip="Print"><i class='bx bx-printer'></i></asp:LinkButton>
                                                    </div>
                                                     
                                                </div>
                                            </div>
                                            <div class="grd-wpr">
                                                <div id="tblPrintIQ" runat="server">
                                                    <div class="prnt-dv-wpr">
                                                        <div id="content">

                                                            <div class="pgslry-head-div head">

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
                                                                    <div style="display: none; margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                        Email Id. :<asp:Label ID="lbl_emaiid" runat="server"></asp:Label>

                                                                        &nbsp;&nbsp;

                            website :
                            <asp:Label ID="lbl_website" runat="server"></asp:Label>
                                                                    </div>
                                                                    <div style="display: none; margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                        Contact Details :<asp:Label ID="lbl_contact_details" runat="server"></asp:Label>


                                                                    </div>
                                                                    <div style="margin: 7px 0px 0px 0px; padding: 0px; float: left; height: auto; text-align: center; vertical-align: middle; font-size: 13px; width: 100%;">
                                                                        <span style="font-size: 14px; font-weight: bold;">Student List-<asp:Label ID="lbl_class22" runat="server"></asp:Label></span>


                                                                    </div>
                                                                </div>


                                                            </div>

                                                            <asp:GridView ID="GrdView" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%" OnRowDataBound="GrdView_RowDataBound">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Sl No.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Total Device">
    <ItemTemplate>

        <asp:Label 
            ID="lbl_total_device" 
            runat="server"
            CssClass="badge bg-success"
            Text='<%# Bind("Total_Device") %>'>
        </asp:Label>

        &nbsp;

        <asp:LinkButton  style="margin-top: 5px;
    text-align: center;"
            ID="lnk_view" 
            runat="server"
            CssClass="btn btn-sm btn-primary primary1"
            Text="View"
            Visible="false" OnClick="lnk_view_Click">
        </asp:LinkButton>

    </ItemTemplate>
</asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Session">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_session" runat="server" Text='<%#Bind("session")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Admission No.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label6" runat="server" Text='<%#Bind("admissionserialnumber")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Class">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_class" runat="server" Text='<%#Bind("class")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Section">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_section" runat="server" Text='<%#Bind("Section")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Roll No.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label1" runat="server" Text='<%#Bind("rollnumber")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Student Name">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label2" runat="server" Text='<%#Bind("studentname")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Mobile No.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_mobile_no" runat="server" Text='<%#Bind("mobilenumber")%>'></asp:Label>,
                                                                        <asp:Label ID="lbl_father_mob" runat="server" Text='<%#Bind("father_mob")%>'></asp:Label>
                                                                            
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Download Status">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_status" runat="server"></asp:Label>
                                                                          
                                                                            <asp:Label ID="lbl_gcm_id" runat="server" Visible="false" Text='<%#Bind("gcm_id")%>'></asp:Label>
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
                         
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!--end row-->
    </div>
    <style>
        .p11{
                font-size: 10px;
                font-weight:normal;
        }
        .modal-header {
    display: flex;
    flex-shrink: 0;
    align-items: center;
    justify-content: space-between;
    padding: 1rem 1rem;
    border-bottom: 1px solid #dee2e6;
    border-top-left-radius: calc(.3rem - 1px);
    border-top-right-radius: calc(.3rem - 1px);
    padding: 32px 0px 5px 15px!important;
}
        @media (min-width: 992px) {
    .modal-lg, .modal-xl {
        max-width: 834px!important;
    }
}
        .modal-body{

    padding-top:20px !important;
}
        .modal-title {
    margin-bottom: 0;
    line-height: 1.5;
    font-size: 19px;
}
        .modal {
    position: fixed;
    top: 85px;
    left: 0;
    z-index: 1050;
    display: none;
    width: 100%;
    height: 100%;
    overflow: hidden;
    outline: 0;
}
         /* DEVICE COUNT BADGE */

.badge{

    padding:6px 10px;

    border-radius:20px;

    font-size:12px;

    color:#fff;

    font-weight:600;
}

/* ZERO DEVICE */

.badge-zero{

    background:#ef4444;
}

/* DEVICE AVAILABLE */

.badge-success{

    background:#22c55e;
}

/* VIEW BUTTON */

.primary1{

    border-radius:20px;

    padding:3px 10px;

    font-size:11px;
}
.device-popup-bg{

    position:fixed;

    inset:0;

    background:rgba(0,0,0,0.45);

    z-index:9999;

    display:flex;

    justify-content:center;

    align-items:center;
}

.device-popup{

    background:#fff;

    width:95%;

    max-width:800px;

    border-radius:16px;

    padding:20px;

    max-height:85vh;

    overflow:auto;
}

.popup-header{

    display:flex;

    justify-content:space-between;

    align-items:center;

    margin-bottom:15px;
}

.close-btn{

    font-size:22px;

    color:red;

    text-decoration:none;
}
.mycess
{
    text-align:center;
}
    </style>


    <div class="modal fade"
     id="deviceModal"
     tabindex="-1"
     role="dialog">

    <div class="modal-dialog modal-lg">

        <div class="modal-content">

            <div class="modal-header">

                <h4 class="modal-title">
                    Student Device List
                </h4>

                <button type="button"
                        class="close"
                        data-dismiss="modal">

                    &times;

                </button>

            </div>

            <div class="modal-body">

                <asp:GridView 
                    ID="grd_device"
                    runat="server"
                    AutoGenerateColumns="false"
                    CssClass="table table-bordered"   style="font-size:13px;">

                    <Columns>
                         <asp:TemplateField HeaderText="Sl No.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_sl" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                        </ItemTemplate>
                             <ItemStyle CssClass="p11" />
                                                                    </asp:TemplateField>
                         <asp:BoundField 
                            DataField="admissionserialnumber"
                            HeaderText="Adm. No." ItemStyle-CssClass="p11" />
                        
                        <asp:BoundField 
                            DataField="DeviceId"
                            HeaderText="Device ID" ItemStyle-CssClass="p11" />

                         <asp:BoundField 
                            DataField="Brand"
                            HeaderText="Brand" ItemStyle-CssClass="p11"  />
                         <asp:BoundField 
                            DataField="Model"
                            HeaderText="Model" ItemStyle-CssClass="p11"  />
                         <asp:BoundField 
                            DataField="DeviceName"
                            HeaderText="DeviceName" ItemStyle-CssClass="p11"  />
                         <asp:BoundField 
                            DataField="AndroidVersion"
                            HeaderText="AndroidVersion" ItemStyle-CssClass="p11"  />

                        <asp:BoundField 
                            DataField="LoginTime"
                            HeaderText="Login Time" ItemStyle-CssClass="p11"  />

                        <asp:TemplateField HeaderText="Action">

                            <ItemTemplate>

                               <asp:LinkButton
    ID="lnk_remove"
    runat="server"
    CssClass="btn btn-danger btn-sm"
    Text="Remove"
    CommandArgument='<%# Eval("DeviceId") + "," + Eval("admissionserialnumber") %>'
    OnClick="lnk_remove_Click">
</asp:LinkButton>
                                

                            </ItemTemplate>
                            <ItemStyle CssClass="mycess" />

                        </asp:TemplateField>

                    </Columns>

                </asp:GridView>

            </div>

        </div>

    </div>

</div>
</asp:Content>
