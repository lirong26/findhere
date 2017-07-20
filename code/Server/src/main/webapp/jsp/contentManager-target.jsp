<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
    pageEncoding="ISO-8859-1"%>
<%@ page import="java.util.List" %>
<%@ page import="model.Apply" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
	<meta http-equiv="Content-type" content="text/html; charset=utf-8" />
	<title>FindHere Member</title>
	<link rel="stylesheet" href="/FindHere/css/style.css" type="text/css" media="all" />
</head>
<body>
<!-- Header -->
<div id="header">
	<div class="shell">
		<!-- Logo + Top Nav -->
		<div id="top">
			<h1><a href="#">FindHere</a></h1>
			<div id="top-navigation">
				Welcome <a href="#"><strong>Member</strong></a>
				<span>|</span>
				<a href="/FindHere/LogOut">Log out</a>
			</div>
		</div>
		<!-- End Logo + Top Nav -->
		
		<!-- Main Nav -->
		<div id="navigation">
			<ul>
			    <li><a href="/FindHere/MemberWelcome"><span>Welcome</span></a></li>
			    <li><a href="#" class="active"><span>Upload Target</span></a></li>
			    <li><a href="/FindHere/MemberContent"><span>Upload Object</span></a></li>
			    <li><a href="/FindHere/MemberVerifyTarget?pageIndex=0"><span>Verifying Target</span></a></li>
			    <li><a href="#"><span>Manage Content</span></a></li>
			</ul>
		</div>
		<!-- End Main Nav -->
	</div>
</div>
<!-- End Header -->

<!-- Container -->
<div id="container">
	<div class="shell">
		
		<!-- Small Nav -->
		<div class="small-nav">
		</div>
		<!-- End Small Nav -->
		
		<br />
		<!-- Main -->
		<div id="main">
			<div class="cl">&nbsp;</div>
			
			<!-- Content -->
			<div id="content">
				
				<!-- Box -->
				<div class="box" style="width:1000px; height:500px">
					<!-- Box Head -->
					<div class="box-head" style="height:45px">
						<h2 class="left">Your Targets</h2>
						<div class="right">
							<form action="/FindHere/SetContents/addTarget" method="post" enctype="multipart/form-data">
							<label><font size="4px">add New Target</font></label>
							<input type="file" class="field" name="newTarget" />
							<input type="submit" class="button" value="add" style="height:35px" />
							</form>
						</div>
					</div>
					<!-- End Box Head -->	

					<!-- Table -->
					<div class="table">
						<table width="100%" border="0" cellspacing="0" cellpadding="0">
							<tr>
								<th>image</th>
								<th>targetID</th>
								<th width="110" class="ac">Content Control</th>
							</tr>
							<%
							List<String> targetIDList = (List<String>)request.getAttribute("targetIDs");
							int pageIndex = Integer.parseInt(request.getAttribute("pageIndex").toString());
							for(int i = 0; i < targetIDList.size(); i++){
							%>
							<tr>
								<td></td>
								<td><h3><%=targetIDList.get(i) %></h3></td>
								<td>
									<a href="/FindHere/RejectApply?
										targetID=<%=targetIDList.get(i) %>
										&pageIndex=<%=pageIndex %>" class="ico del">Delete</a>
								</td>
							</tr>
							<%
							}
							%>
						</table>
						
						
						<!-- Pagging -->
						<div class="pagging">
							<div class="left">Showing 1-12 of 44</div>
							<div class="right">
								<% 
								int previous = (pageIndex-1<0)? 0:pageIndex-1;
								int next = pageIndex + 1;
								%>
								<a href="/FindHere/ConfirmMember?pageIndex=0">Start</a>
								<a href="/FindHere/ConfirmMember?pageIndex=<%=previous %>">Previous</a>
								<a href="/FindHere/ConfirmMember?pageIndex=<%=pageIndex %>"><font color="red"><%=pageIndex+1 %></font></a>
								<a href="/FindHere/ConfirmMember?pageIndex=<%=pageIndex+1 %>"><%=pageIndex+2 %></a>
								<a href="/FindHere/ConfirmMember?pageIndex=<%=pageIndex+2 %>"><%=pageIndex+3 %></a>
								<a href="/FindHere/ConfirmMember?pageIndex=<%=pageIndex+3 %>"><%=pageIndex+4 %></a>
								<span>...</span>
								<a href="/FindHere/ConfirmMember?pageIndex=<%=next %>">Next</a>
							</div>
						</div>
						<!-- End Pagging -->
						
					</div>
					<!-- Table -->
					
				</div>
				<!-- End Box -->
				

			</div>
			<!-- End Content -->
			
			<!-- Sidebar -->
			<div id="sidebar">
				
			</div>
			<!-- End Sidebar -->
			
			<div class="cl">&nbsp;</div>			
		</div>
		<!-- Main -->
	</div>
</div>
<!-- End Container -->

<!-- Footer -->
<div id="footer">
	<div class="shell">
		<span class="left">&copy; 2017 - FindHere</span>
	</div>
</div>
<!-- End Footer -->
	
</body>
</html>
