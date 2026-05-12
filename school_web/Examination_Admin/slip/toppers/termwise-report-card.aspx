<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="termwise-report-card.aspx.cs" Inherits="school_web.Examination_Admin.slip.toppers.termwise_report_card" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Report Card</title>
    <link href="css/report-card.css" rel="stylesheet" />
    <script src="../../../assets/js/jquery-1.10.2.min.js"></script>
    <script src="../../../assets/Angular/angular.min.js"></script>

    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=tblPrintIQ.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<link href="css/report-card.css" rel="stylesheet" type="text/css" />');
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
        <div class="invoice-sec" data-ng-app="RpCardApp" data-ng-controller="RpCardAppCtrl" runat="server" id="reportcrdDV">
            <div class="prnt-btn-sec" id="printBtns" runat="server">
                <div class="prnt-btn-wpr">
                    <div class="print-btn-sec">
                        <div class="noPrint" style="float: left">
                            <asp:Button ID="btn_back" CssClass="back-btn" runat="server" OnClick="btn_back_Click" />
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
                            <img src="../assets/images/loader-ico.gif" class="ints-loader-img" />
                            <asp:Label ID="lblmessage" runat="server"></asp:Label>
                        </p>
                    </div>
                </div>
            </div>


            <div id="tblPrintIQ" runat="server">
                <div class="invoice-inr-sec" data-ng-repeat="x in reportCardS track by $index">
                    <div class="invoice-height-manage">
                        <div class="invoice-wpr">
                            <img src="{{x.Watermar_image}}" class="wtr-mrk-img v-false {{x.MySubjectMarkShowItemII[0].Is_watermark_show}}" />
                            <div class="report-card-wpr" style="height: {{x.MySubjectMarkShowItemII[0].Height_dv}}">
                                <div class="report-card-head {{x.Content_header}}">
                                    <div class="report-card-left-dv">
                                        <img src="{{x.Frim_logo}}" />
                                        <asp:Label ID="lbl_estd" runat="server" Text="{{x.Estd}}" class="estdTextP v-false {{x.MySubjectMarkShowItemII[0].Is_estd_show}}"></asp:Label>
                                    </div>
                                    <div class="report-card-cntr-dv">
                                        <asp:Label ID="lbl_school_name" class="report-card-schlname" runat="server" Text="{{x.Firm_name}}"></asp:Label>
                                        <asp:Label ID="lbl_aff_text" class="report-card-schl-affno-by" runat="server" Text="{{x.Affiliated_by_full_text}}"></asp:Label>
                                        <asp:Label ID="lbl_address" runat="server" class="report-card-schl-add" Text="{{x.Firm_address}}"></asp:Label>
                                        <asp:Label ID="lbl_contact_no" runat="server" class="report-card-schl-cont v-false {{x.MySubjectMarkShowItemII[0].Is_contact_no_show}}" Text="{{x.Firm_contact_no}}"></asp:Label>
                                        <p class="report-card-schl-emil v-false {{x.MySubjectMarkShowItemII[0].Is_email_show}}">
                                            Email : 
                                        <asp:Label ID="lbl_email" runat="server" Text="{{x.Firm_email}}"></asp:Label>
                                        </p>

                                        <p class="report-card-schl-emil v-false {{x.MySubjectMarkShowItemII[0].Is_website_show}}">
                                            Website : 
                                        <asp:Label ID="lbl_website" runat="server" Text="{{x.Website}}"></asp:Label>
                                        </p>
                                    </div>
                                    <asp:Label ID="lbl_aff_no" class="report-card-schl-affno {{x.Frim_aff_no}}" runat="server" Text="{{x.MySubjectMarkShowItemII[0].Aff_text}} : {{x.Frim_aff_no}}"></asp:Label>
                                </div>




                                <div class="image-headers {{x.Header_templete}}">
                                    <img src="{{x.Header_templete}}" />
                                    <div class="report-card-std-img-dv">
                                        <div class="sdt-img-dv">
                                            <img src="{{x.Student_image}}" class="{{x.MySubjectMarkShowItemII[0].Is_std_img_hide}}  {{x.Student_image}}" />
                                        </div>
                                    </div>
                                </div>
                                <div class="hdr-exm-info">
                                    <h2 class="hdr-exm-info-name">Progress Report ( {{x.Term_name}} )</h2>
                                    <p class="hdr-exm-info-session">SESSION : {{x.Session}}</p>
                                </div>

                                <div class="report-card-std-info-dv">
                                    <table>
                                        <tr>
                                            <td class="bdrtp0 bdlftp0">Admn. No./Roll No.</td>
                                            <td class="devidrs bdrtp0">:</td>
                                            <td class="bdrtp0">{{x.Admission_no}} / {{x.Roll_no}}</td>

                                            <td class="bdrtp0">Father's Name</td>
                                            <td class="devidrs bdrtp0">:</td>
                                            <td class="bdrtp0">{{x.Father_name}}</td>

                                            <td class="bdrtp0">Total Meetings</td>
                                            <td class="devidrs bdrtp0">:</td>
                                            <td class="bdrtp0 bdlrght0" style="width: 150px;"></td>
                                        </tr>
                                        <tr>
                                            <td class="bdlftp0">Name</td>
                                            <td class="devidrs">:</td>
                                            <td>{{x.Student_name}}</td>

                                            <td>Mother's Name</td>
                                            <td class="devidrs">:</td>
                                            <td>{{x.Mother_name}}</td>

                                            <td>Meetings Present</td>
                                            <td class="devidrs">:</td>
                                            <td class="bdlrght0"></td>
                                        </tr>
                                        <tr>
                                            <td class="bdlrbtm0 bdlftp0">Class / Section</td>
                                            <td class="devidrs bdlrbtm0">:</td>
                                            <td class="bdlrbtm0">{{x.For_class}} / {{x.Section}}</td>

                                            <td class="bdlrbtm0">Date of Birth</td>
                                            <td class="devidrs bdlrbtm0">:</td>
                                            <td class="bdlrbtm0">{{x.Date_of_birth}}</td>

                                            <td class="bdlrbtm0">Attendance</td>
                                            <td class="devidrs bdlrbtm0">:</td>
                                            <td class="bdlrbtm0 bdlrght0"></td>
                                        </tr>
                                    </table>
                                </div>


                                <div class="subs-mrks-area-dv">
                                    <table>
                                        <%--<tr>
                                            <th class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}">A</th>
                                            <th class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}" colspan="18">SCHOLASTIC AREAS</th>
                                        </tr>--%>
                                        <tr>
                                            <th class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}">SN</th>
                                            <th class="{{x.MySubjectMarkShowItemII[0].Is_text_center}} {{x.MySubjectMarkShowItemII[0].Is_subj_code_hide}}">SUBJECT CODE</th>
                                            <th>SUBJECTS</th>
                                            <th class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}" data-ng-repeat="item in x.MySubjectHeading track by $index">{{item.Short_Name}} <span class="{{x.MySubjectMarkShowItemII[0].Max_mark_show}}">({{item.Maximum_Marks}})</span></th>
                                            <th class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}">{{x.MySubjectMarkShowItemII[0].grade_head_text}} (100)</th><%--{{x.MySubjectMarkShowItemII[0].SubjFulmarks}}--%>
                                            <th class="{{x.MySubjectMarkShowItemII[0].Is_text_center}} {{x.MySubjectMarkShowItemII[0].Grade}}">GRADE</th>
                                            <th class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}">Highest</th>
                                        </tr>

                                        <tr data-ng-repeat="item in x.MySubjectMarkShowItemII track by $index">
                                            <td class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}">{{$index+1}}</td>
                                            <td class="{{x.MySubjectMarkShowItemII[0].Is_text_center}} {{x.MySubjectMarkShowItemII[0].Is_subj_code_hide}}">{{item.Subject}}</td>
                                            <td>{{item.Subject_name}}</td>
                                            <td class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}" data-ng-repeat="itemx in item.MySubjectMarkItemIII track by $index">{{itemx.Marks}}</td>
                                            <td class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}">{{item.Total_marks}}</td>
                                            <td class="{{x.MySubjectMarkShowItemII[0].Is_text_center}} {{item.Grade}}"><span class="{{item.Gbgclass}}" style="{{item.gradebg}}">{{item.Grade}}</span></td>
                                            <td class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}">{{item.SubjHighestMark}}</td>
                                        </tr>
                                    </table>

                                    <div class="ovrll-sec">
                                        <div class="ovrll-ttl-marks-dv">
                                            <p class="ovrll-ttl-marks-cnt-p">Total Marks </p>
                                            <p class="ovrll-ttl-marks-num-p">{{x.MyOverallNoDetailData[0].Overall_obt_marks}}/{{x.MyOverallNoDetailData[0].Overall_full_marks}}</p>
                                        </div>
                                        <div class="ovrll-ttl-marks-dv" style="margin: 0px 0px 0px 0px; width: 33.2%;">
                                            <p class="ovrll-ttl-marks-cnt-p">Percentage </p>
                                            <p class="ovrll-ttl-marks-num-p">{{x.MyOverallNoDetailData[0].Overall_percents}} (%)</p>
                                        </div>
                                        <div class="ovrll-ttl-marks-dv" style="margin: 0px 0px 0px 0px; float: right;">
                                            <p class="ovrll-ttl-marks-cnt-p">Grade </p>
                                            <p class="ovrll-ttl-marks-num-p {{x.MyOverallNoDetailData[0].IfbgColorR}}"><span class="{{x.MyOverallNoDetailData[0].Grade_bg_class}}" style="{{x.myoverallnodetaildata[0].gradebg}}">{{x.MyOverallNoDetailData[0].Grade}}</span></p>
                                        </div>
                                    </div>
                                </div>
                                 <div class="subs-mrks-area-b-dvpT" style="margin-top: 5px;">
                                        <table>
                                            <tr>
                                                 
                                                <th colspan="2">CO-SCHOLASTIC AREAS : A</th>
                                            </tr>
                                           <%-- <tr>
                                                
                                                <th class="th-bg-rmov">ACTIVITIES</th> 
                                                <th class="th-bg-rmov {{x.MySubjectMarkShowItemII[0].Is_text_center}}">GRADE</th>
                                            </tr>--%>
                                            <tr data-ng-repeat="item in x.MyDescplineData track by $index">
                                                
                                                <td>{{item.Activity_name}}</td>
                                                <td class="{{x.MySubjectMarkShowItemII[0].Is_text_center}}">{{item.Term_grade}}</td>
                                            </tr>
                                        </table>
                                    </div>






                                <%--<div class="subs-mrks-area-lft-dv">  
                                    <div class="remarks-rp {{x.MySubjectMarkShowItemII[0].Remarkss}}">
                                        <p>REMARKS :<span>{{x.MySubjectMarkShowItemII[0].Remarkss}}</span></p>
                                    </div> 
                                </div>--%>

                                <%--<div class="subs-mrks-area-rght-dv">
                                    <div class="qr-dvs {{x.MySubjectMarkShowItemII[0].qr_div_true}}">
                                        <img src="{{x.MyOverallNoDetailData[0].Graphurl}}" />
                                    </div> 


                                    <div class="rp-card-graph-sec {{x.MySubjectMarkShowItemII[0].Graph}}">
                                        <p style="text-align: center; margin: 0px 0px 10px 0px; padding: 0px; width: 100%; float: left;">
                                            Subject Wise Position
                                        </p>
                                        <div class="rp-card-graph-wpr">
                                            <p class="rp-card-graph-txt0">0</p>
                                            <p class="rp-card-graph-txt50">{{x.MySubjectMarkShowItemII[0].TermSubj_hlf_mark}}</p>
                                            <p class="rp-card-graph-txt100">{{x.MySubjectMarkShowItemII[0].SubjFulmarks}}</p>
                                            <div class="rp-card-graph-tbl-wpr" style="height: {{x.MySubjectMarkShowItemII[0].GraphHeight}}">
                                                <div class="rp-card-graph-tbl-inr" data-ng-repeat="item in x.MyGraphDetailData track by $index" style="width: {{item.Grph_width}}%;">
                                                    <div class="rp-card-graph-tbl-nobg" style="height: {{item.BlankHeight}}%"></div>
                                                    <div class="rp-card-graph-tbl" style="background: {{item.bg_color}}; height: {{item.final_perc}}%"></div>
                                                    <p>{{item.Subject_name}}</p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                     
                                </div>--%>





                                <%--<div class="grding-scale-dv-sec">
                                    <p class="grding-scale-dv-p">{{x.MyMarkRangeData[0].RowCount}} Point Grading Scale : <span data-ng-repeat="item in x.MyMarkRangeData track by $index">{{item.Grade}} [{{item.Lower_Range}}-{{item.Upper_Range}}]</span></p>
                                </div>--%>

                                <div class="sig-dv v-false {{x.MySubjectMarkShowItemII[0].Sign_bottom}}">
                                    <div class="sig-left">
                                        <div class="lft-sig-img-dv">
                                            <img class="lft-sig-img {{x.MySignatureDetailData[0].Signature_image}}" src="{{x.MySignatureDetailData[0].Signature_image}}" />
                                        </div>
                                        <p class="sig-ps">{{x.MySignatureDetailData[0].Signature_name}}</p>
                                    </div>
                                    <div class="sig-left">
                                        <div class="cntr-sig-img-dv">
                                            <img src="{{x.MySignatureDetailData[1].Signature_image}}" class="cntr-sig-img {{x.MySignatureDetailData[1].Signature_image}}" />
                                        </div>
                                        <p class="sig-ps {{x.ParantSign}}">{{x.MySignatureDetailData[1].Signature_name}}</p>
                                    </div>

                                    <div class="sig-left">
                                        <div class="rght-sig-img-dv">
                                            <img src="{{x.MySignatureDetailData[2].Signature_image}}" class="rght-sig-img {{x.MySignatureDetailData[2].Signature_image}}" />
                                        </div>
                                        <p class="sig-ps">{{x.MySignatureDetailData[2].Signature_name}}</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>



                    <div class="invoice-height-manage">
                        <div class="invoice-wpr">
                            <img src="{{x.Watermar_image}}" class="wtr-mrk-img v-false {{x.MySubjectMarkShowItemII[0].Is_watermark_show}}" />
                            <div class="report-card-wpr" style="height: {{x.MySubjectMarkShowItemII[0].Height_dv}}">
                                <div class="report-card-head {{x.Content_header}}">
                                    <div class="report-card-left-dv">
                                        <img src="{{x.Frim_logo}}" />
                                        <asp:Label ID="Label1" runat="server" Text="{{x.Estd}}" class="estdTextP v-false {{x.MySubjectMarkShowItemII[0].Is_estd_show}}"></asp:Label>
                                    </div>
                                    <div class="report-card-cntr-dv">
                                        <asp:Label ID="Label2" class="report-card-schlname" runat="server" Text="{{x.Firm_name}}"></asp:Label>
                                        <asp:Label ID="Label3" class="report-card-schl-affno-by" runat="server" Text="{{x.Affiliated_by_full_text}}"></asp:Label>
                                        <asp:Label ID="Label4" runat="server" class="report-card-schl-add" Text="{{x.Firm_address}}"></asp:Label>
                                        <asp:Label ID="Label5" runat="server" class="report-card-schl-cont v-false {{x.MySubjectMarkShowItemII[0].Is_contact_no_show}}" Text="{{x.Firm_contact_no}}"></asp:Label>
                                        <p class="report-card-schl-emil v-false {{x.MySubjectMarkShowItemII[0].Is_email_show}}">
                                            Email : 
                                        <asp:Label ID="Label6" runat="server" Text="{{x.Firm_email}}"></asp:Label>
                                        </p>

                                        <p class="report-card-schl-emil v-false {{x.MySubjectMarkShowItemII[0].Is_website_show}}">
                                            Website : 
                                        <asp:Label ID="Label7" runat="server" Text="{{x.Website}}"></asp:Label>
                                        </p>
                                    </div>
                                    <asp:Label ID="Label8" class="report-card-schl-affno {{x.Frim_aff_no}}" runat="server" Text="{{x.MySubjectMarkShowItemII[0].Aff_text}} : {{x.Frim_aff_no}}"></asp:Label>
                                </div>




                                <div class="image-headers {{x.Header_templete}}">
                                    <img src="{{x.Header_templete}}" />
                                </div>
                                <div class="hdr-exm-info">
                                    <h2 class="hdr-exm-info-name">Progress Report ( {{x.Term_name}} )</h2>
                                    <p class="hdr-exm-info-session">SESSION : {{x.Session}}</p>
                                </div>

                                <div class="report-card-std-info-dv">
                                    <table>
                                        <tr>
                                            <td class="bdrtp0 bdlftp0">Admn. No./Roll No.</td>
                                            <td class="devidrs bdrtp0">:</td>
                                            <td class="bdrtp0">{{x.Admission_no}} / {{x.Roll_no}}</td>

                                            <td class="bdrtp0">Father's Name</td>
                                            <td class="devidrs bdrtp0">:</td>
                                            <td class="bdrtp0">{{x.Father_name}}</td>

                                            <td class="bdrtp0">Total Meetings</td>
                                            <td class="devidrs bdrtp0">:</td>
                                            <td class="bdrtp0 bdlrght0" style="width: 150px;"></td>
                                        </tr>
                                        <tr>
                                            <td class="bdlftp0">Name</td>
                                            <td class="devidrs">:</td>
                                            <td>{{x.Student_name}}</td>

                                            <td>Mother's Name</td>
                                            <td class="devidrs">:</td>
                                            <td>{{x.Mother_name}}</td>

                                            <td>Meetings Present</td>
                                            <td class="devidrs">:</td>
                                            <td class="bdlrght0"></td>
                                        </tr>
                                        <tr>
                                            <td class="bdlrbtm0 bdlftp0">Class / Section</td>
                                            <td class="devidrs bdlrbtm0">:</td>
                                            <td class="bdlrbtm0">{{x.For_class}} / {{x.Section}}</td>

                                            <td class="bdlrbtm0">Date of Birth</td>
                                            <td class="devidrs bdlrbtm0">:</td>
                                            <td class="bdlrbtm0">{{x.Date_of_birth}}</td>

                                            <td class="bdlrbtm0">Attendance</td>
                                            <td class="devidrs bdlrbtm0">:</td>
                                            <td class="bdlrbtm0 bdlrght0"></td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="performance-dv-sec">
                                    <table>
                                        <tr>
                                            <th>Performance</th>
                                            <th class="txt-center" data-ng-repeat="item in x.MyPerformanceDatasubjItem track by $index">{{item.PerfSubj}}</th>
                                        </tr>
                                        <tr>
                                            <th>Self</th>
                                            <td class="txt-center" data-ng-repeat="item in x.MyPerformanceContentItem track by $index">{{item.PerfSubjContent}}</td>
                                        </tr>
                                        <tr>
                                            <th>Average</th>
                                            <td class="txt-center" data-ng-repeat="item in x.MyPerformanceContentItem track by $index">{{item.Average}}</td>
                                        </tr>
                                        <tr>
                                            <th>Highest</th>
                                            <td class="txt-center" data-ng-repeat="item in x.MyPerformanceContentItem track by $index">{{item.HighestMark}}</td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="graphs-wprs">

                                    <div class="chart-container">
                                        <!-- Y-axis numbers -->
                                        <div class="y-axis">
                                            <div>100</div>
                                            <div>80</div>
                                            <div>60</div>
                                            <div>40</div>
                                            <div>20</div>
                                            <div>0</div>
                                        </div>

                                        <!-- Chart -->
                                        <div class="chart">
                                            <!-- COM -->
                                            <div class="group" data-ng-repeat="item in x.MyPerformanceContentItem track by $index">
                                                <div class="bars">
                                                    <div class="bar marks" style="height: {{item.Average_in_hundred_Self}}%;"></div>
                                                    <!-- 240 / 250 * 100 -->
                                                    <div class="bar max" style="height: {{item.HighestMark_in_hundred}}%;"></div>
                                                    <!-- 250 / 250 * 100 -->
                                                    <div class="bar avg" style="height: {{item.Average_in_hundred_for_grapg}}%;"></div>
                                                    <!-- 230 / 250 * 100 -->
                                                </div>
                                                <div class="label">{{item.SubjName}}</div>
                                            </div>
                                        </div>
                                    </div>

                                    <!-- Legend -->
                                    <div class="legend">
                                        <div><span style="background: #4A90E2"></span>marks</div>
                                        <div><span style="background: #F5A623"></span>max</div>
                                        <div><span style="background: #D0021B"></span>Avg</div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="invoice-height-manage">
                        <div class="invoice-wpr">
                            <img src="{{x.Watermar_image}}" class="wtr-mrk-img v-false {{x.MySubjectMarkShowItemII[0].Is_watermark_show}}" />
                            <div class="report-card-wpr" style="height: {{x.MySubjectMarkShowItemII[0].Height_dv}}">
                                <div class="report-card-head {{x.Content_header}}">
                                    <div class="report-card-left-dv">
                                        <img src="{{x.Frim_logo}}" />
                                        <asp:Label ID="Label9" runat="server" Text="{{x.Estd}}" class="estdTextP v-false {{x.MySubjectMarkShowItemII[0].Is_estd_show}}"></asp:Label>
                                    </div>
                                    <div class="report-card-cntr-dv">
                                        <asp:Label ID="Label10" class="report-card-schlname" runat="server" Text="{{x.Firm_name}}"></asp:Label>
                                        <asp:Label ID="Label11" class="report-card-schl-affno-by" runat="server" Text="{{x.Affiliated_by_full_text}}"></asp:Label>
                                        <asp:Label ID="Label12" runat="server" class="report-card-schl-add" Text="{{x.Firm_address}}"></asp:Label>
                                        <asp:Label ID="Label13" runat="server" class="report-card-schl-cont v-false {{x.MySubjectMarkShowItemII[0].Is_contact_no_show}}" Text="{{x.Firm_contact_no}}"></asp:Label>
                                        <p class="report-card-schl-emil v-false {{x.MySubjectMarkShowItemII[0].Is_email_show}}">
                                            Email : 
                                        <asp:Label ID="Label14" runat="server" Text="{{x.Firm_email}}"></asp:Label>
                                        </p>

                                        <p class="report-card-schl-emil v-false {{x.MySubjectMarkShowItemII[0].Is_website_show}}">
                                            Website : 
                                        <asp:Label ID="Label15" runat="server" Text="{{x.Website}}"></asp:Label>
                                        </p>
                                    </div>
                                    <asp:Label ID="Label16" class="report-card-schl-affno {{x.Frim_aff_no}}" runat="server" Text="{{x.MySubjectMarkShowItemII[0].Aff_text}} : {{x.Frim_aff_no}}"></asp:Label>
                                </div>




                                <div class="image-headers {{x.Header_templete}}">
                                    <img src="{{x.Header_templete}}" />
                                </div>
                                <div class="hdr-exm-info">
                                    <h2 class="hdr-exm-info-name">Progress Report ( {{x.Term_name}} )</h2>
                                    <p class="hdr-exm-info-session">SESSION : {{x.Session}}</p>
                                </div>

                                <div class="report-card-std-info-dv">
                                    <table>
                                        <tr>
                                            <td class="bdrtp0 bdlftp0">Admn. No./Roll No.</td>
                                            <td class="devidrs bdrtp0">:</td>
                                            <td class="bdrtp0">{{x.Admission_no}} / {{x.Roll_no}}</td>

                                            <td class="bdrtp0">Father's Name</td>
                                            <td class="devidrs bdrtp0">:</td>
                                            <td class="bdrtp0">{{x.Father_name}}</td>

                                            <td class="bdrtp0">Total Meetings</td>
                                            <td class="devidrs bdrtp0">:</td>
                                            <td class="bdrtp0 bdlrght0" style="width: 150px;"></td>
                                        </tr>
                                        <tr>
                                            <td class="bdlftp0">Name</td>
                                            <td class="devidrs">:</td>
                                            <td>{{x.Student_name}}</td>

                                            <td>Mother's Name</td>
                                            <td class="devidrs">:</td>
                                            <td>{{x.Mother_name}}</td>

                                            <td>Meetings Present</td>
                                            <td class="devidrs">:</td>
                                            <td class="bdlrght0"></td>
                                        </tr>
                                        <tr>
                                            <td class="bdlrbtm0 bdlftp0">Class / Section</td>
                                            <td class="devidrs bdlrbtm0">:</td>
                                            <td class="bdlrbtm0">{{x.For_class}} / {{x.Section}}</td>

                                            <td class="bdlrbtm0">Date of Birth</td>
                                            <td class="devidrs bdlrbtm0">:</td>
                                            <td class="bdlrbtm0">{{x.Date_of_birth}}</td>

                                            <td class="bdlrbtm0">Attendance</td>
                                            <td class="devidrs bdlrbtm0">:</td>
                                            <td class="bdlrbtm0 bdlrght0"></td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="performance-dv-sec">
                                    <table>
                                        <tr>
                                            <th>Comprative Analysis</th>
                                            <th class="txt-center" data-ng-repeat="item in x.MyPerformanceDatasubjItem track by $index">{{item.PerfSubj}}</th>
                                        </tr>
                                        <tr data-ng-repeat="item in x.MyanalysisContentItem track by $index">
                                            <th>{{item.examName}}</th>
                                            <td class="txt-center" data-ng-repeat="itemx in item.MyanalysisSubContentItem track by $index">{{itemx.marks}}</td>
                                        </tr>
                                        <tr>
                                            <th>Cumulative</th>
                                            <td class="txt-center" data-ng-repeat="item in x.MyPerformanceContentItem track by $index">{{item.Average_in_hundred}}</td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="graphs-wprs">
                                    <div class="rp-card-graph-sec">
                                        <p style="text-align: center; margin: -2px 0px 4px 0px; padding: 0px; width: 100%; float: left;">
                                            ({{x.Term_name}}) Comprative Analysis
                                        </p>
                                        <div class="rp-card-graph-wpr">
                                            <p class="rp-card-graph-txt0">0</p>
                                            <p class="rp-card-graph-txt50">50<%--{{x.MySubjectMarkShowItemII[0].TermSubj_hlf_mark}}--%></p>
                                            <p class="rp-card-graph-txt100">100<%--{{x.MySubjectMarkShowItemII[0].SubjFulmarks}}--%></p>
                                            <div class="rp-card-graph-tbl-wpr" style="height: {{x.MySubjectMarkShowItemII[0].GraphHeight}}">
                                                <div class="rp-card-graph-tbl-inr" data-ng-repeat="item in x.MyGraphDetailData track by $index" style="width: {{item.Grph_width}}%;">
                                                    <div class="rp-card-graph-tbl-nobg" style="height: {{item.BlankHeight}}%"></div>
                                                    <div class="rp-card-graph-tbl" style="background: {{item.bg_color}}; height: {{item.final_perc}}%"><span>{{item.final_perc}}%</span></div>
                                                    <p>{{item.Subject_name}}</p>
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

        <asp:Panel ID="pnl_dues" runat="server" Visible="false">
            <div class="duesmsg-dv">
                <div class="duesmsg-dv-inrs">
                    <img src="error-con.png" />
                    <p class="duesmsg-p">Please clear your dues or reach out to the school.</p>
                </div>
            </div>
        </asp:Panel>


        <style>
            .duesmsg-dv {
                width: 100%;
                float: left;
                margin: 0px;
                padding: 150px 0px 0px 0px;
            }

            .duesmsg-dv-inrs {
                width: 100%;
                float: left;
                margin: 0px;
                padding: 0px;
                text-align: center;
            }

                .duesmsg-dv-inrs img {
                    width: 200px;
                    margin: 0px auto;
                }

            .duesmsg-p {
                width: 100%;
                float: left;
                margin: 10px 0px 0px 0px;
                padding: 0px;
                color: #f00;
                text-align: center;
                font-size: 40px;
                font-weight: 700;
                letter-spacing: 1px;
            }
        </style>
        <asp:HiddenField ID="hd_req_from" runat="server" />
        <asp:HiddenField ID="hd_adm_no" runat="server" />
        <asp:HiddenField ID="hd_section" runat="server" />
        <asp:HiddenField ID="hd_session_id" runat="server" />
        <asp:HiddenField ID="hd_class_id" runat="server" />
        <asp:HiddenField ID="hd_branch_id" runat="server" />
        <asp:HiddenField ID="hd_term_id" runat="server" />


        <script type="text/javascript">
            var app = angular.module('RpCardApp', []);
            app.controller('RpCardAppCtrl', function ($scope, $http) {
                var adm_no = $("#<%=hd_adm_no.ClientID%>").val();
                var session_id = $("#<%=hd_session_id.ClientID%>").val();
                var class_id = $("#<%=hd_class_id.ClientID%>").val();
                var section = $("#<%=hd_section.ClientID%>").val();
                var branch_id = $("#<%=hd_branch_id.ClientID%>").val();
                var term_id = $("#<%=hd_term_id.ClientID%>").val();

                messge("Please Wait...");
                $("#intsLoader").removeClass("hidden");
                $http.get("api/termRP.asmx/fetch_rp_card_bulks", { params: { "Session_id": session_id, "Class_id": class_id, "Section": section, "Branch_id": branch_id, "Term_id": term_id, "Adm_no": adm_no } }).then(function (response) {
                    $scope.reportCardS = response.data;
                    $("#intsLoader").addClass("hidden");
                }).catch(function (error) {
                    // Handle the error here
                    console.error("Error fetching report card data:", error);
                    alert("An error occurred while fetching the report card data. Please try again later.");
                    $("#intsLoader").addClass("hidden"); // Hide the loader even if there's an error
                });
            });
            function messge(msg) {
                $("#<%=lblmessage.ClientID%>").text(msg);
                $('.ints-loader-wpr').hide().slideDown(0);
            }
        </script>
    </form>
</body>
</html>
