<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Print_admit_card_Online_Reg.aspx.cs" Inherits="school_web.Admin.slip.Print_admit_card_Online_Reg" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Admit Card</title>
    <link href="css/admit-card.css" rel="stylesheet" /> 
    <script src="../../assets/js/jquery-1.10.2.min.js"></script>
    <script src="../../assets/Angular/angular.min.js"></script>
    <script src="../../assets/Angular/angular-sanitize.min.js"></script>


    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="css/admit-card.css" rel="stylesheet" type="text/css" />');
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


        <div class="invoice-sec" data-ng-app="RpCardApp" data-ng-controller="RpCardAppCtrl">
            <div class="prnt-btn-sec">
                <div class="prnt-btn-wpr">
                    <div class="print-btn-sec">
                        <div class="noPrint" style="float: left">
                            <a id="A1" runat="server" class="back-btn"></a>
                        </div>
                        <div class="noPrint" style="float: right">
                            <asp:LinkButton ID="print1" OnClientClick="return PrintPanel()" CssClass="print-btn" runat="server" ToolTip="Print"></asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>

            <div class="ints-loader-wpr" id="intsLoader">
                <div class="ints-loader-wpr-inr">
                    <div class="ints-loader">
                        <p class="ints-loader-txt">
                            <img src="../../assets/images/icons/loader-ico.gif" class="ints-loader-img" />
                            <asp:Label ID="lblmessage" runat="server"></asp:Label>
                        </p>
                    </div>
                </div>
            </div>
            <div id="tblPrintIQ" runat="server">
                <div class="invoice-inr-sec" data-ng-repeat="x in reportAdmitS track by $index">
                    <div class="printheadse333">
                        <div class="invoice-wpr">
                            <div class="printpage-sec-main">
                                <div class="headdivv">
                                    <div class="printlogo4455">
                                        <asp:Image ID="schoollogo" runat="server" ImageUrl="{{x.MySchoolDetailsItem[0].LogoSchool}}" class="printlogo" />
                                    </div>
                                    <div class="schoolnameheadin">
                                        <asp:Label ID="lbl_school_name" CssClass="informatchild22-lab" Text="{{x.MySchoolDetailsItem[0].School_name}}" runat="server"></asp:Label>
                                        <asp:Label ID="lbl_under" CssClass="informatchild22-pp33" runat="server" Visible="false" Text="Under the aegis of Delhi Public School Society, New Delhi "></asp:Label>
                                        <asp:Label ID="lbl_affilation_no" CssClass="informatchild22-pp33 pp33italic" runat="server" Text="{{x.MySchoolDetailsItem[0].Affiliation_no}}"></asp:Label>
                                        <asp:Label ID="lbl_address" CssClass="informatchild22-pp33 pp33italic" Text="{{x.MySchoolDetailsItem[0].Address}}" runat="server"></asp:Label>
                                        <asp:Label ID="lbl_mobileno_emailid" CssClass="informatchild22-pp33 pp33italic" Text="{{x.MySchoolDetailsItem[0].Mobile_no_email}}" runat="server"></asp:Label>

                                        <div class="acardd">
                                            <asp:Label ID="lbl_termname" CssClass="admitcarterm1" runat="server" Text="{{x.MySchoolDetailsItem[0].Class_names}}"></asp:Label>
                                            <asp:Label ID="lbl_session" CssClass="admitcarterm2" runat="server" Text="{{x.MySchoolDetailsItem[0].Session}}"></asp:Label>
                                            <asp:Label ID="Label3" CssClass="admit-for-h {{x.Print_for}}" runat="server"><span>{{x.Print_for}}</span></asp:Label>
                                        </div>
                                    </div>

                                    <div class="printlogo4455">
                                    </div>

                                </div>

                                <div class="nammarggg">

                                    <div class="name1sec">
                                        <div class="namehead">
                                            <p class="nameheadp">STUDENT'S NAME <span class="spdottt">:</span> </p>
                                            <asp:Label ID="lbl_student_name" runat="server" CssClass="nameheadll" Text="{{x.Student_name}}"></asp:Label>
                                        </div>

                                        <div class="namehead">
                                            <p class="nameheadp">DATE OF BIRTH <span class="spdottt">:</span></p>
                                            <asp:Label ID="lbl_dob" runat="server" CssClass="nameheadll" Text="{{x.Subject_DOB}}"></asp:Label>
                                        </div>
                                        <div class="namehead">
                                            <p class="nameheadp">FATHER'S NAME <span class="spdottt">:</span></p>
                                            <asp:Label ID="lbl_father_name" runat="server" CssClass="nameheadll" Text="{{x.Father_name}}"></asp:Label>

                                        </div>

                                        <div class="namehead">
                                            <p class="nameheadp">MOBILE NO. <span class="spdottt">:</span></p>
                                            <asp:Label ID="lbl_mother_name" runat="server" CssClass="nameheadll" Text="{{x.Student_mob_no}}"></asp:Label>
                                        </div>


                                    </div>

                                    <div class="name1sec">
                                        <div class="namehead">
                                            <p class="nameheadp">Apply ID <span class="spdottt">:</span> </p>
                                            <asp:Label ID="lbl_admission_no" runat="server" CssClass="nameheadll" Text="{{x.Registration_id}}"></asp:Label>
                                        </div>

                                        <div class="namehead">
                                            <p class="nameheadp">CLASS <span class="spdottt">:</span></p>
                                            <asp:Label ID="lbl_class_sec" runat="server" CssClass="nameheadll" Style="padding: 0px 10px 0px 10px;" Text="{{x.Course_Name}}"></asp:Label>

                                            <%--<asp:Label ID="lbl_Session_name" runat="server" Text="{{x.Session_name}}"></asp:Label>--%>
                                        </div>


                                        <div class="namehead">
                                            <p class="nameheadp">MOTHER'S NAME <span class="spdottt">:</span></p>
                                            <asp:Label ID="Label1" runat="server" CssClass="nameheadll" Text="{{x.Mother_name}}"></asp:Label>
                                        </div>

                                        <div class="namehead {{x.Mother_mobile}}">
                                            <p class="nameheadp">MOBILE NO. <span class="spdottt">:</span></p>
                                            <asp:Label ID="Label2" runat="server" CssClass="nameheadll" Text="{{x.Mother_mobile}}"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="studimggr">
                                        <div class="studimggr-inrs">
                                            <asp:Image ID="studentlogo" runat="server" ImageUrl="{{x.Student_img}}" class="studimggrt {{x.Student_img}}" />
                                        </div>
                                    </div>
                                </div>



                                <div class="printlogo4455" style="display: none">
                                    <img src="printi_mg/logo.png" class="img-responsive printlogo" />

                                    <asp:Image ID="studentlogo11" runat="server" ImageUrl='<%#Bind("Student_img")%>' class="img-responsive printlogo" Style="height: 133px; width: 120px;" />
                                </div>

                                <div class="administration-paragraph">

                                    <table>
                                        <tr>
                                            <th colspan="8" style="text-align: center; background-color: #f7f7f7">{{x.MySchoolDetailsItem[0].ExamTypeText}} STARTING DATE & TIME  </th>
                                        </tr>

                                        <tr>
                                            <th>SN</th>
                                            <th>DATE</th>
                                            <th>START TIME</th>
                                            <th>END TIME</th>
                                            <th>DAY</th>
                                            <th>ROOM</th>
                                            <th>EXAM TYPE	</th>
                                            <th>SIGNATURE OF THE EXAMINER</th>
                                        </tr>
                                        <tr data-ng-repeat="item in x.MyExamDetailsItem track by $index">
                                            <td>{{$index+1}}</td>
                                            <td>{{item.Exam_date}}</td>
                                            <td>{{item.Exam_s_time}}</td>
                                            <td>{{item.Exam_e_time}}</td>
                                            <td>{{item.Exam_day}}</td>
                                            <td>{{item.Room_Name}}</td>
                                            <td>{{item.Exam_Type}}</td>
                                            <td></td>
                                        </tr>
                                    </table>
                                    
                                    <div class="courses-sec33"> 
                                        <table>
                                            <tr>
                                                <td><b style="font-size: 15px;">IMPORTANT NOTE  : </b>
                                                    <p data-ng-bind-html="x.Remarks"></p>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>


                                    <%--<div class="sig-dv">
                                        <div class="sig-left">
                                            <div class="lft-sig-img-dv">
                                                <img src="{{x.MySigDetailsItem[0].Class_teacher_sig}}" class="lft-sig-img {{x.MySigDetailsItem[0].Class_teacher_sig}}" />
                                            </div>
                                            <p class="sig-ps">CLASS TEACHER</p>
                                        </div>
                                        <div class="sig-left">
                                            <div class="cntr-sig-img-dv">
                                                <img src="{{x.MySigDetailsItem[0].Examinee_sig}}" class="cntr-sig-img {{x.MySigDetailsItem[0].Examinee_sig}}" />
                                            </div>
                                            <p class="sig-ps">EXAMINATION INCHARGE</p>
                                        </div>
                                        <div class="sig-left">
                                            <div class="rght-sig-img-dv">
                                                <img src="{{x.MySigDetailsItem[0].Principal_sig}}" class="rght-sig-img {{x.MySigDetailsItem[0].Principal_sig}}" />
                                            </div>
                                            <p class="sig-ps">PRINCIPAL</p>
                                        </div>
                                    </div>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>





        <asp:HiddenField ID="hd_session_id" runat="server" />
        <asp:HiddenField ID="hd_class_id" runat="server" />
        <asp:HiddenField ID="hd_admission_no" runat="server" />
        <asp:HiddenField ID="hd_from" runat="server" />
        <asp:HiddenField ID="hd_branch_id" runat="server" />
        <script type="text/javascript">
            var app = angular.module('RpCardApp', ['ngSanitize']);
            app.controller('RpCardAppCtrl', function ($scope, $http) {

                var session_id = $("#<%=hd_session_id.ClientID%>").val();
                var class_id = $("#<%=hd_class_id.ClientID%>").val();
                var admission_no = $("#<%=hd_admission_no.ClientID%>").val();
                var coming_from = $("#<%=hd_from.ClientID%>").val();
                var branch_id = $("#<%=hd_branch_id.ClientID%>").val();

                messge("Please Wait...");
                $("#intsLoader").removeClass("hidden");
                $http.get("webServices/admitCard.asmx/fetch_admit_card_details", { params: { "Session_id": session_id, "Class_id": class_id, "Admission_no": admission_no, "Coming_from": coming_from, "Branch_id": branch_id } }).then(function (response) {
                    $scope.reportAdmitS = response.data;

                    $("#intsLoader").addClass("hidden");
                })

            });


            function messge(msg) {
                $("#<%=lblmessage.ClientID%>").text(msg);
                $('.ints-loader-wpr').hide().slideDown(0);
            }
        </script>
    </form>
</body>
</html>
