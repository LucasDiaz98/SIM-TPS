<%-- 
    Document   : puntoA
    Created on : Mar 24, 2016, 4:16:57 PM
    Author     : Jose
--%>

<%@page contentType="text/html" pageEncoding="UTF-8"%>
<%@taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c" %>
<!DOCTYPE html>
<html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
        <title>Graficar</title>
    </head>
    <body>
        <%@ include file="/WEB-INF/menu.jspf" %>
        
        <h1>Graficar <span style="color: red"><%= request.getParameter("titulo")%></span></h1>
        <%-- Quise usar ${titulo} pero no me anduvo, asi paso el nombre de la distribucion a graficar--%>

        <hr style="width: 100%; color: black; height: 1px; background-color:black;" />
            
        <form method="post" action="${pageContext.request.contextPath}/TestGenernadorPropio">
        <br>
        Cantidad de intervalos (Entre 2 y 31):
        <input name="cantidadIntervalos" value="" type="number" required>
        <br>
        <br>
        <input name="distribucion" value="<%= request.getParameter("titulo")%>" type="hidden">
        <button type="submit">Graficar</button>
        </form>
    </body>
</html>
