<%@ Page Title="" Language="C#" MasterPageFile="~/_adminETutorProf/webview/Site1.Master" AutoEventWireup="true" CodeBehind="View_Class_Activity.aspx.cs" Inherits="school_web._adminETutorProf.webview.View_Class_Activity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    View Class Activity
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
            right: 0px;
            left: auto;
        }
    </style>
    <link href="../../Autocomplete/jquery-ui.css" rel="stylesheet" />
    <script src="../../Autocomplete/jquery-ui.js"></script>
    <script>
        $(function () {
            $("#<%=txt_date.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "1900:2100",
            });
        });
    </script>

    <script>
        $(function () {
            $("#<%=txt_enddate.ClientID %>").datepicker({
                dateFormat: "dd/mm/yy",
                changeMonth: true,
                changeYear: true,
                yearRange: "1900:2100",


            });
        });
    </script>
     
    <link href="../../Student_Profile/css/customK.css" rel="stylesheet" />
    <link href="../../Student_Profile/css/mediaK.css" rel="stylesheet" />
    <link href="../../Student_Profile/css/black-dashboard.min.css" rel="stylesheet" />
    <style>
        .container {
            width: 100%;
            padding-right: 14px !important;
            padding-left: 12px !important;
        }
       /* .notificationpan {
    display: none;
    width: 100%;
    position: absolute;
    top: 310px !important;
    right: 0%;
    padding: 10px 10px;
    height: auto;
}*/
.card-body {
    flex: 1 1 auto;
      padding: 0px;
}
       .card-container {
  display: flex;
  flex-wrap: wrap;
  gap: 16px;
  padding: 20px;
}

.card {
  background-color: #ffffff;
  border: 1px solid #ddd;
  border-radius: 8px;
  box-shadow: 0 2px 8px rgba(0,0,0,0.1);
  padding: 16px;
  width: 100%;
  font-family: Arial, sans-serif;
}

.card-body h5 {
  font-size: 18px;
  margin-bottom: 10px;
  color: #333;
}

.card-body p {
  font-size: 14px;
  margin: 4px 0;
  color: #555;
}

.card-actions {
  margin-top: 10px;
}

.btn-delete {
  background-color: #e14eca;
  color: #fff;
  padding: 6px 10px;
  border-radius: 4px;
  font-weight: 500;
  border: none;
  cursor: pointer;
  text-decoration: none;
}
.textcont1 {
    height: auto;
    width: 100%;
    margin: 2px 0px 0px 7px;
    padding: 0px 0px 0px 0px;
    float: left;
    font-size: 13px;
    line-height: 18px;
    font-weight: 400;
    color: #4c5258;
    text-align: left;
    font-weight: bold !important;
}

    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="fullinfo">
         
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
                        <asp:LinkButton ID="LinkButton1" class="btn-close" runat="server" Style="color: #fff">X</asp:LinkButton>

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
                        <asp:LinkButton ID="LinkButton2" class="btn-close" runat="server" Style="color: #fff">X</asp:LinkButton>

                    </div>
                </div>
            </div>

        <div class="clearfix"></div>
        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2" style="padding-right: 5px; padding-left: 5px;">
            <p class="textcont1 ">Class</p>
        </div>
        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10" style="padding-right: 5px; padding-left: 5px;">
            <p class="textcont3">
                <asp:DropDownList ID="ddl_class" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_class_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
            </p>
        </div>
         <div class="clearfix"></div>
        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2" style="padding-right: 5px; padding-left: 5px;">
            <p class="textcont1 ">Section</p>
        </div>
        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10" style="padding-right: 5px; padding-left: 5px;">
            <p class="textcont3">
                <asp:DropDownList ID="ddl_section" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddl_section_SelectedIndexChanged">
                </asp:DropDownList>
            </p>
        </div>
        <div class="clearfix"></div>

        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2" style="padding-right: 5px; padding-left: 5px;">
            <p class="textcont1 ">Subject</p>
        </div>
        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10" style="padding-right: 5px; padding-left: 5px;">
            <p class="textcont3">
                <asp:DropDownList ID="ddl_subject" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </p>
        </div>
         <div class="clearfix"></div>
        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" style="padding-right: 5px; padding-left: 5px;">
            <p class="textcont1 ">From Date  </p>
        </div>
        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" style="padding-right: 5px; padding-left: 5px;">
            <p class="textcont3" style="position: relative">
                <asp:TextBox ID="txt_date" runat="server" CssClass="calender-icon"></asp:TextBox>
                <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
            </p>
        </div>
        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" style="padding-right: 5px; padding-left: 5px;">
            <p class="textcont1 ">To Date </p>
        </div>
        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" style="padding-right: 5px; padding-left: 5px;">
            <p class="textcont3" style="position: relative">
                <asp:TextBox ID="txt_enddate" runat="server" CssClass="calender-icon"></asp:TextBox>
                <i class="fa fa-calendar clndr-icon" aria-hidden="true"></i>
            </p>
        </div>
        <div class="clearfix"></div>
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="text-align: center">
            <asp:Button ID="btn_submit" runat="server" Text="Find" style="font-size: 13px;" CssClass="btn btn-primary" OnClick="btn_submit_Click" />
        </div>

        <div class="clearfix"></div>
       <%-- <div class="texbox-border" style="padding: 0px 5px; overflow: auto">--%>


        <div class="card-container">
    <asp:Repeater ID="RPDetails" runat="server" OnItemDataBound="RPDetails_ItemDataBound">
        <ItemTemplate>
            <div class="card">
                <div class="card-body">
                   
                    <p style="display:none"><strong>Class:</strong> <%#Eval("Course_Name") %></p>
                     <p><strong>Subject:</strong> <%#Eval("Subject_name") %></p>
                    <p><strong>Section:</strong> <%#Eval("Section_data") %></p>
                    <p><strong>Remarks:</strong> <%#Eval("Remarks") %></p>
                    <p><strong>Date:</strong> <%#Eval("Date") %></p>
                    
                    
                        <a id="a1" runat="server" href='<%#Eval("Attachment") %>' download target="_blank">
                            <i class="fa fa-download" aria-hidden="true"></i> Download
                        </a>
                    
                    
                    <div class="card-actions">
                        <asp:LinkButton ID="lnk_Delete" runat="server"
                            CssClass="btn-delete"
                            OnClick="lnk_Delete_Click"
                            OnClientClick='return confirm("Are you sure want to delete ?")'>
                            Delete
                        </asp:LinkButton>
                         <asp:Label ID="lbl_attachment" Visible="false" runat="server" Text='<%#Bind("Attachment") %>'></asp:Label>
                        <asp:Label ID="lbl_Id" runat="server" Text='<%#Bind("Id") %>' Visible="false"></asp:Label>
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>



          

        <%--</div>--%>
    </div>
</asp:Content>
