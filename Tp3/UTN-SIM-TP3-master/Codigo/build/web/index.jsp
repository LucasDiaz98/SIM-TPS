<%-- 
    Document   : index
    Created on : Mar 24, 2016, 3:45:50 PM
    Author     : Jose
--%>

<%@page contentType="text/html" pageEncoding="UTF-8"%>
<!DOCTYPE html>
<html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
        <link href="${pageContext.request.contextPath}/css/estilos.css" rel="stylesheet">
        <title>TP1 Simulacion</title>
    </head>
    <body>
        <%-- 
        EL MENU ES UN FRAGMENTO UNICO QUE LO INSERTE EN TODAS LAS PAGINAS.
        LO MODIFICAS UNA VEZ Y CAMBIA EN TODOS.
        
        COMO LO HICE? pone en google "navbar css" y copias el codigo de alguien que ya lo hizo.
        COMO FUNCIONA CSS? mirate un videito de youtube.
        
        Controlen que funcione todo, y depues le empiezan a meter estilo y alinear las cosas que queden lindas.
        --%>
        
        <%@ include file="/WEB-INF/menu.jspf" %>
        
        <h1>TP3 - Simulación</h1>
        <h4>Seleccione para que distribucion desea generar valores de variables aleatorias:  </h4>
        <p></p>
        <a href="${pageContext.request.contextPath}/uniforme.jsp">
            <button type="button"> Uniforme </button>
        </a>

        <p></p>
        <a href="${pageContext.request.contextPath}/exponencial.jsp">
            <button type="button"> Exponencial </button>
        </a>


        <p></p>
        <a href="${pageContext.request.contextPath}/poisson.jsp">
            <button type="button"> Poisson </button>
        </a>

        <p></p>
        <a href="${pageContext.request.contextPath}/normal.jsp">
            <button type="button"> Normal </button>
        </a>
    </body>
</html>
