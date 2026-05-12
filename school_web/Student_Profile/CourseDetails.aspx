<%@ Page Title="" Language="C#" MasterPageFile="~/Student_Profile/User.Master" AutoEventWireup="true" CodeBehind="CourseDetails.aspx.cs" Inherits="school_web.Student_Profile.CourseDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Subject Details
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ol class="breadcrumb">
        <li class="breadcrumb-item"><a href="Dashboard.aspx">Dashboard</a> <i class="fa fa-angle-right"></i></li>
        <li class="breadcrumb-item">
            <a href="CourseView.aspx">My Subject </a><i class="fa fa-angle-right"></i></li>
        <li class="breadcrumb-item">Subject Details</li>
    </ol>
    <asp:HiddenField ID="hdUserId" runat="server" />
    <asp:HiddenField ID="hd_CourseId" runat="server" />
    <asp:HiddenField ID="hd_sectionid" runat="server" />
    <div class="grid-form blogDetails" id="blogDetails">
        <div class="grid-form1">
            <div class="panel-body">
                <div class="row" style="padding: 0px 0px 0px 0px;">
                    <h2 class=" blue_bg">
                        <asp:Literal ID="LtCourseName" runat="server" />
                    </h2>
                  

                    <div class="col-lg-8">
                        <div class="blog-details-right">
                            <div class="categories">
                                <h3>Lessons</h3>
                                <asp:Repeater ID="RpLesson" runat="server" OnItemDataBound="RpLesson_ItemDataBound">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdSectionID" runat="server" Value='<%#Eval("SectionID") %>' />
                                        <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
                                            <div class="panel panel-default">
                                                <div class="panel-heading" role="tab" id="<%#Eval("SectionID") %>">
                                                    <h4 class="panel-title">
                                                        <a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapse<%#Eval("SectionID") %>" aria-expanded="true" aria-controls="collapse<%#Eval("SectionID") %>">
                                                            <b>
                                                                <asp:Literal ID="Literal9" runat="server" Text='<%#Eval("LessonNo") %>' />
                                                            </b>: 
                                                        <asp:Literal ID="Literal10" runat="server" Text='<%#Eval("SetionName") %>' />
                                                        </a>
                                                    </h4>
                                                </div>
                                                <div id="collapse<%#Eval("SectionID") %>" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="<%#Eval("SectionID") %>">
                                                    <div class="panel-body" style="background: #fafafa;">
                                                        <asp:Repeater ID="RpSection" runat="server">
                                                            <ItemTemplate>
                                                                <p>
                                                                    <b>
                                                                        <asp:Literal ID="Ltslno" runat="server" Text='<%#Eval("slno") %>' />
                                                                    </b>: 
                                                       <a href="TopicDetails.aspx?TopicId=<%#Eval("TopicID") %>&SectionID=<%#Eval("SectionID") %>">
                                                           <asp:Literal ID="LtTopicName" runat="server" Text='<%#Eval("TopicName") %>' />
                                                           <asp:Image ID="ImageTick" runat="server" ImageUrl="images/tick1.png" Visible="false" />
                                                           <asp:HiddenField ID="hdTopicID" runat="server" Value='<%#Eval("ReadTopic") %>' />
                                                                </p>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                            <%--<div class="relative">
                                    <h3>Related Posts</h3>
                                    <a href="blogDetails.html">
                                        <div class="relative-item">
                                            <img src="img/blog/recentpost1.jpg" alt="" />
                                            <span><i class="far fa-calendar-alt"></i>February 22, 2018</span>
                                            <p>Nivea for men’s sensitive hydro gel.</p>
                                        </div>
                                    </a>
                                </div>--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
