<%-- 
    Document   : uniforme
    Created on : Mar 24, 2016, 4:16:57 PM
    Author     : Jose
--%>

<%@page contentType="text/html" pageEncoding="UTF-8"%>
<!DOCTYPE html>
<html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
        <link href="${pageContext.request.contextPath}/css/estilos.css" rel="stylesheet">
        <title>Distribución Uniforme</title>
  
    </head>
    <body>
        <%@ include file="/WEB-INF/menu.jspf" %>
        <h1>Distribución Uniforme</h1>
        <hr style="width: 100%; color: black; height: 1px; background-color:black;" />
            
        
        <form method="post" action="${pageContext.request.contextPath}/generarNumeros" name="datos">
        <br>
        Valor inicial:
        <input name="inicial" value="" type="number" required>
        <br><br>
        Valor final:
        <input name="end" value="" type="number" required>
        <br><br>
        Cantidad de valores a generar:
        <input name="cantidad" value="" type="number" required>
        <br><br>
        <%-- Agrego un parametro con el mismo ID en todos los formularios, 
        pero distinto valor para cada distribucion, este parametro va a estar escondido--%>
        <input name="distribucion" value="uniformeAB" type="hidden">
        <input type="submit" value="Generar" />

        </form>
        
            <p></p>
       
    </body>
</html>
