<%@ Page Title="" Language="C#" MasterPageFile="~/InstructorProfile/Teacher_Profile.Master" AutoEventWireup="true" CodeBehind="mark-entry.aspx.cs" Inherits="school_web.InstructorProfile.mark_entry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Mark Entry
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script language="Javascript">
       <!--
    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode != 46 && charCode > 31
          && (charCode < 48 || charCode > 57))
            return false;

        return true;
    }
    //-->
    </script>
    <style>
        .table td, .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            padding: 8px!important;
        }

        .modal-backdrop.fade, .fade.blockOverlay {
            opacity: 0;
            display: none;
        }

        .txtbxError {
            border-bottom: 2px solid #ef0000!important;
            padding: 3px 7px 2px !important;
        }
    </style>

    <%--<script type="text/javascript">
        $(window).on('load', function () {
            $('#myModalS').modal('show');
        });
    </script>--%>

    <%--<script type="text/javascript">
         function openModal() {
             alert("44");
             $('#myModalS').modal('show');

         }
    </script>--%>
   <script type="text/javascript">
       var arr = new Array();
       function Calculation(ele) {
           var max_mark = parseInt($("#<%=txt_mm.ClientID %>").val());

           var txt = $("input[id*='txt_marks']");
           var chk = $("input[id*='chkSelect']");
           var checkedIndex = $(ele).closest('tr').index();


           if ($(ele).closest('tr').find($("input[id*='chkSelect']")).is(':checked')) {
               arr.push(checkedIndex);
           }
           else {
               arr = arr.filter(function (item) {
                   return item !== checkedIndex
               });
           }

           for (var i = 0; i < arr.length; i++) {
               txt[arr[i]].value = i + 1;
               txt[arr[i]].disabled = false;
           }

           for (vari = 0; i < chk.length; i++) {
               if (!chk[i].checked) {
                   var valueess = txt[i].value;

                   if (valueess <= max_mark) {
                       $(txt[i]).removeClass("txtbxError");
                   }
                   else {
                       if (valueess.match(/^-?\d+$/)) {
                           txt[i].value = '';
                           $(txt[i]).addClass("txtbxError");
                       }
                       else {
                           //txt[i].value = '';
                           //$(txt[i]).addClass("txtbxError");
                       }
                   }
               }
           }
       }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="app-main__inner">
        <div class="app-page-title">
            <div class="page-title-wrapper">
                <div class="page-title-heading">
                    <div class="page-title-icon">
                        <i class="pe-7s-network icon-gradient bg-mean-fruit"></i>
                    </div>
                    <div>
                        <asp:Literal ID="ltUsertop" runat="server">Marks Entry</asp:Literal>
                    </div>
                </div>
                <div class="page-title-actions">
                </div>
            </div>
        </div>
        <div id="notification">
            <div id="pan" class="notificationpan">
                <div style="float: left; width: 235px; height: auto;">
                    <asp:Label ID="lbl_msg" runat="server" Font-Bold="True" ForeColor="White"></asp:Label>
                </div>
                <img src="../images/close.png" onclick="$(function () { $('.notificationpan').show().slideUp(1000);});"
                    class="closenotificationpan" alt="" />
            </div>
        </div>

        <div class="row">
            <div class="col-lg-12">
                <div class="main-card mb-3 card">
                    <div class="card-body">



                        <div class="row">
                            <div class="form-group col-xs-10 col-sm-3 col-md-2 col-lg-2">
                                <label data-toggle="modal" data-target="#myModalS">Class<sup>*</sup></label>
                                <asp:DropDownList ID="ddl_CourseCat" class="form-control" runat="server" Style="width: 98%" AutoPostBack="true" OnSelectedIndexChanged="ddl_CourseCat_SelectedIndexChanged"></asp:DropDownList>
                            </div>




                            <div class="form-group col-xs-10 col-sm-3 col-md-10 col-lg-10">
                                <div class="row" style="padding: 0px 0px 0px 15px;">
                                    <div class="form-group col-xs-10 col-sm-3 col-md-2 col-lg-2">
                                        <label>Section<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_section" class="form-control" runat="server" Style="width: 98%" AutoPostBack="true" OnSelectedIndexChanged="ddl_section_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="form-group col-xs-10 col-sm-3 col-md-2 col-lg-2">
                                        <label>Term<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_term" class="form-control" runat="server" Style="width: 98%" AutoPostBack="true" OnSelectedIndexChanged="ddl_term_SelectedIndexChanged"></asp:DropDownList>
                                    </div>

                                    <div class="form-group col-xs-10 col-sm-3 col-md-2 col-lg-2">
                                        <label>Assessment<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_assesment" class="form-control" runat="server" Style="width: 98%" AutoPostBack="true" OnSelectedIndexChanged="ddl_assesment_SelectedIndexChanged"></asp:DropDownList>
                                    </div>

                                    <div class="form-group col-xs-10 col-sm-3 col-md-3 col-lg-3">
                                        <label>Subject<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_subject" class="form-control" runat="server" Style="width: 98%" AutoPostBack="true" OnSelectedIndexChanged="ddl_subject_SelectedIndexChanged"></asp:DropDownList>
                                    </div>

                                    <div class="form-group col-xs-10 col-sm-3 col-md-2 col-lg-2">
                                        <label>Subject Activity<sup>*</sup></label>
                                        <asp:DropDownList ID="ddl_exam_level" class="form-control" runat="server" Style="width: 98%"></asp:DropDownList>
                                    </div>
                                    <div class="form-group col-xs-10 col-sm-3 col-md-1 col-lg-1">
                                        <asp:Button ID="btn_find" runat="server" class="btn btn-sm btn-success" Text="Find" Style="margin: 28px 0px 0px 0px; padding: 6px 10px 8px;" OnClick="btn_find_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>



                        <hr />
                        <table style="width: 100%;" id="example1" class="table table-hover table-striped table-bordered">
                            <thead>
                                <tr>
                                    <th  colspan="4"  >
                                          <asp:Label ID="lbl_datevalid" runat="server" Font-Bold="true"  ForeColor="Red"  ></asp:Label>

                                    </th>
                                    </tr>
                                <tr>
                                    <th>#</th>
                                    <th>Student</th>
                                    <th>Roll No.</th>
                                    <th>
                                        <asp:Label ID="lbl_activity_type" runat="server"></asp:Label>
                                        <asp:Label ID="lbl_max_marks" runat="server"></asp:Label>
                                        <asp:TextBox ID="txt_mm" Style="display: none" runat="server"></asp:TextBox>
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="RPDetails" runat="server" OnItemDataBound="RPDetails_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbl_SL" runat="server" Text='<%#Container.ItemIndex+1 %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_studentname" runat="server" Font-Names="Arial" Text='<%#Bind("studentname") %>'></asp:Label>
                                                (<asp:Label ID="lbl_adm_no" runat="server" Font-Names="Arial" Text='<%#Bind("admissionserialnumber") %>'></asp:Label>)
                                            </td>
                                            <td>
                                                <asp:Label ID="lbl_rollnumber" runat="server" Font-Names="Arial" Text='<%#Bind("rollnumber") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkSelect" Style="display: none"
                                                    runat="server" CssClass="chkbox m-0" Text=" " />

                                                <asp:TextBox ID="txt_marks" onblur="Calculation(this.value)" runat="server" class="grd-txtbx-clas"></asp:TextBox>
                                                <asp:Label ID="lbl_mark_ids" runat="server" Visible="false" Font-Names="Arial" Text="0"></asp:Label>
                                                <asp:LinkButton ID="lnk_remarks" runat="server" class="grd-remks" OnClick="lnk_remarks_Click"><i class="fa fa-ellipsis-v fa-w-6"></i></asp:LinkButton>

                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>

                        <div class="form-group col-xs-10 col-sm-3 col-md-12 col-lg-12"> 
                            <asp:Button ID="btn_fina_saved" Enabled="false"   runat="server" class="btn btn-sm btn-success" Text="Final Submit" Style="margin: 0px 0px 0px 10px; padding: 6px 10px 8px; float: right;" 
                                OnClick="btn_fina_saved_Click" OnClientClick="return confirm('Are you sure you want to final submit?  After final submission you can not update marks.');" />

                            <asp:Button ID="btn_save" runat="server" Enabled="false" class="btn btn-sm btn-success"  OnClientClick="return confirm('Are you sure you want to save?');" Text="Save" Style="margin: 0px 0px 0px 10px; padding: 6px 10px 8px; float: right;"
                                OnClick="btn_save_Click" /> 
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%--<style>
        .modal {
            display: block !important;
            opacity: 1 !important;
        }
    </style>--%>



    <div class="modal fade show" id="myModalSS" tabindex="-1" visible="false" runat="server" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" style="display: block; padding-right: 5px;">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Remark Type</h5>
                    <asp:LinkButton ID="lnk_close_popup" OnClick="lnk_close_popup_Click" class="close" runat="server"><span aria-hidden="true">&times;</span></asp:LinkButton>
                    <%--<button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        
                    </button>--%>
                </div>
                <div class="modal-body">
                    <asp:DropDownList ID="ddl_remarks" class="form-control" runat="server"></asp:DropDownList>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btn_save_rmrks" runat="server" class="btn btn-primary" Text="Save" OnClick="btn_save_rmrks_Click" />
                </div>
            </div>
        </div>
    </div>

    <style>
        .grd-txtbx-clas {
            margin: 0px 10px 0px 0px;
            padding: 3px 7px;
            border: 0px;
            border-bottom: 1px solid #6208d5;
            background: rgb(255 255 255 / 0%);
            width: 100px;
            float: left;
        }

        .grd-remks {
            margin: 0px 0px 0px 0px;
            padding: 0px;
            border: 0px;
            font-size: 16px;
            color: #6208d5;
        }
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Script" runat="server">
</asp:Content>
