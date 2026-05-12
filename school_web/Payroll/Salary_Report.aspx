<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Salary_Report.aspx.cs" Inherits="school_web.Payroll.Salary_Report" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Salary Report</title>
    
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <!--favicon-->
    <script src="../assets/js/jquery-1.10.2.min.js"></script>
    
    <script src="../assets/js/bootstrap.min.js"></script>
    
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/sale_entry.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css2?family=Lato:ital,wght@0,100;0,300;0,400;1,100&display=swap" rel="stylesheet" />
    <link href="../assets/css/icons.css" rel="stylesheet" />
    <script type="text/javascript" language="javascript">
        function DisableBackButton() {
            window.history.forward()
        }
        DisableBackButton();
        window.onload = DisableBackButton;
        window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
        window.onunload = function () { void (0) }
    </script>
    <style>
        body, #form1 {
    font-size: 13px;
    color: #fff;
    letter-spacing: .5px;
    /* background: #f7f7ff; */
    background: #9fa19f;
    overflow-x: hidden;
    font-family: 'Lato', sans-serif;
}
    </style>

      <script type="text/javascript">
          function PrintPanel() {
              var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
              var printWindow = window.open('', '', 'height=400,width=800');
              printWindow.document.write('<html><head>');
              printWindow.document.write('</head><body>');
              printWindow.document.write('<link href="https://fonts.googleapis.com/css2?family=Dosis:wght@200;300;400;500;600;700;800&family=Montserrat:ital,wght@0,300;0,400;0,500;0,600;0,700;0,800;0,900;1,200;1,300;1,400;1,500;1,600;1,700;1,800;1,900&family=Roboto:ital,wght@0,100;0,300;0,400;0,500;0,700;0,900;1,100;1,300;1,400;1,500;1,700;1,900&display=swap" rel="stylesheet"/><link href="../assets/css/Print.css" rel="stylesheet" type="text/css" /><link href="../css/sale_entry.css" rel="stylesheet" type="text/css" /><link href="css/bootstrap.min.css" rel="stylesheet" type="text/css" />');
              
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
</head>
<body>
    <form id="form1" runat="server">
           <div class="wrapper">
            <div class="page-wrapper">
                <div class="page-content">
                    <div id="notification">
                        <div id="pan" class="notificationpan">

                            <div id="success" runat="server" visible="false" style="float: left; width: 100%; height: auto;" class="alert alert-success border-0 bg-success alert-dismissible fade show py-2">
                                <div class="d-flex align-items-center">

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
                    <div class="page-breadcrumb d-flex align-items-center mb-2 eeewe">


                        <div class="col-xl-11">
                            <div class="ps-3 d-none d-sm-flex">
                                <nav aria-label="breadcrumb">
                                    <ol class="breadcrumb mb-0 p-0">
                                        <li class="breadcrumb-item">
                                            <a style="color: #fff;" id="a1" runat="server"><i class="bx bx-home-alt" style="color: #fff;"></i></a>
                                        </li>
                                        <li class="breadcrumb-item active" aria-current="page" style="color: #fff;">Salary Report</li>
                                    </ol>
                                </nav>
                            </div>
                        </div>
                        <div class="col-xl-1">
                            <nav aria-label="breadcrumb">
                                <ol class="breadcrumb mb-0 p-0">
                                    <li class="breadcrumb-item">
                                        <a id="a2" runat="server" style="background: #e80c0c; color: #fff; padding: 5px 5px 5px 5px; border-radius: 5px; margin: 0px 0px 0px 55px; text-decoration: none;"><i class="bx bx-arrow-back"></i>Back</a>
                                    </li>

                                </ol>
                            </nav>
                        </div>
                    </div>


                    <div class="row">
                        <div class="col-xl-12">
                            <div class="card" style="margin-top: 10px; margin-bottom:10px">
                                <div class="card-body">
                                    <div class="p-4 border rounded" style="background: #690259; color: #fff;">
                                        <div class="row">
                                            <div class="col-md-3">
                                                <label for="validationCustom01" class="form-label">Month<sup>*</sup></label>
                                                 <asp:DropDownList ID="ddl_month" runat="server" CssClass="form-control">
                                </asp:DropDownList>

                                            </div>

                                              <div class="col-md-2">
                                                <label for="validationCustom01" class="form-label">Year<sup>*</sup></label>
                                                 <asp:DropDownList ID="ddlyear" runat="server" CssClass="form-control">
                                </asp:DropDownList>

                                            </div>

                                            <div class="col-2">
                                                <asp:Button ID="btn_Submit" runat="server" Text="Find" CssClass="btn btn-primary" ValidationGroup="a" OnClick="btn_Submit_Click" Style="margin: 27px 0px 0px 0px; padding: 6px 10px;" />
                                            </div>


                                               <div class="col-2">
                                            <asp:LinkButton ID="btn_excels" runat="server" OnClick="btn_excels_Click" Style="margin-left: 10px; margin-top:27px;" class="btn btn-primary find-dv-btn">  <i class='bx bx-download'></i> Excel</asp:LinkButton>
                                                        <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" CssClass="btn btn-primary find-dv-btn" Style="margin-left: 10px;margin-top:27px; display:none" runat="server"
                                                            ToolTip="Print"><i class='bx bx-printer'></i></asp:LinkButton>
                                                   </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-xl-12">

                            
                             <div id="tblPrintIQ" runat="server">
                            <h6 class="mb-0 text-uppercase"style="color:#fff">Salary Report</h6>
                            <hr style="margin: 5px 0px 5px 0px;" />
                            <div class="card">
                                <div class="card-body">
                                    <div class="table-responsive">
                                        <div id="example2_wrapper" class="dataTables_wrapper dt-bootstrap5">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:Panel ID="pnl_grid" runat="server" Visible="false">
                                                        
                                                        <asp:GridView ID="grvExcelData" OnRowCreated="grvExcelData_RowCreated" class="table table-striped table-bordered dataTable" runat="server" CssClass="table table-bordered" Width="100%" OnRowDataBound="grvExcelData_RowDataBound">
                                                        </asp:GridView>
                                                       
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
            </div>
        </div>
    </form>
</body>
</html>
