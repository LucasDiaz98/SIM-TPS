<%-- 
    Document   : mostrar
    Created on : Mar 24, 2016, 6:48:41 PM
    Author     : Jose
--%>

<%@page contentType="text/html" pageEncoding="UTF-8"%>
<%@taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c" %>
<!DOCTYPE html>
<html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
        <link href="${pageContext.request.contextPath}/css/estilos.css" rel="stylesheet">
        <title>Mostrar Numeros Generados</title>
    </head>
    <body>
        <%@ include file="/WEB-INF/menu.jspf" %>
                

        <h1>Lista de Numero Aleatorios con distribucion <span style="color: red">${titulo}</span></h1>
        <hr style="width: 100%; color: black; height: 1px; background-color:black;" />
        <%-- uso un form en vez de un href para pasar el titul--%>
        <form method="post" action="graficar.jsp">
            <input name="titulo" value="${titulo}" type="hidden">
            <button type="sumit">Graficar</button>
        </form>

        <a href="${pageContext.request.contextPath}/graficar.jsp"> 
                
        </a> 
                
        <%-- INICIO TABLA--%>      
        <table  class="table" border="1" style="width:100% ; text-align: center">
            <%-- INICIO CABEZERA TABLA--%>  
            <thead style="background-color: #D8D8D8" >
                <th>Iteracion</th>
                <th>Numero</th> 
            </thead>
            <%-- INICIO CUERPO/FILA TABLA--%>  
 
            <c:forEach items="${numerosGenerados}" var="p"  varStatus="loop">
                <tbody>
                    <td> ${loop.index+1} </td>
                    <td> ${p}</td>
                </tbody>
            </c:forEach>
        </table>

</body>
</html>
