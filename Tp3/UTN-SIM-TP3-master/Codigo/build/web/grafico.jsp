<%-- 
    Document   : graficar
    Created on : Mar 24, 2016, 6:48:41 PM
    Author     : Jose
--%>

<%@page import="java.text.DecimalFormat"%>
<%@page import="Modelo.Intervalos"%>
<%@page contentType="text/html" pageEncoding="UTF-8"%>
<%@taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c" %>
<!DOCTYPE html>
<html>
    <head>
        <jsp:useBean id="tablaFrecuencias" scope="request"  type="Intervalos[]" />
        <link href="${pageContext.request.contextPath}/css/estilos.css" rel="stylesheet">

        <!--Load the AJAX API-->
        <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
        <title>JSP Page</title>

        <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
        <script type="text/javascript">

            // Load the Visualization API and the corechart package.
            google.charts.load('current', {'packages': ['corechart']});

            // Set a callback to run when the Google Visualization API is loaded.
            google.charts.setOnLoadCallback(drawChart);

            // Callback that creates and populates a data table,
            // instantiates the pie chart, passes in the data and
            // draws it.
            function drawChart() {

                // Create the data table.
                var data = new google.visualization.DataTable();
                data.addColumn('string', 'Intervalo');
                data.addColumn('number', 'Frecuencia');
                data.addRows([
                        //estaria bueno escribirlo con tags del core
                
            <% 
                
                for (int i = 0; i < tablaFrecuencias.length; i++) { 
            
                String limiteSupTxt = String.format("%.2f", tablaFrecuencias[i].getLimSuperior() - 0.01);
                String limiteInfTxt = String.format("%.2f", tablaFrecuencias[i].getLimInferior());
            

                if (i > tablaFrecuencias.length-1){
            
            %>

            ['<%= limiteInfTxt %>-<%= limiteSupTxt %>', <%= tablaFrecuencias[i].getFo() %> ]
            <%break;}%>

                        ['<%= limiteInfTxt %>-<%= limiteSupTxt %>', <%= tablaFrecuencias[i].getFo() %>],
            <%}%>

                                ]);
                                        // Set chart options
                                        var options = {
                                        title: 'Histograma de numeros aleatorios generados por el sistema',
                                                width: 1400,
                                                height: 500,
                                                bar:     {groupWidth: '98%'},
                                                hAxis:   {
                                                title: 'Intervalos'
                                                },
                                                vAxis:   {   gridlines: { count: 10 },
                                                        scaleType: null,
                                                        title: 'Frecunencias',
                                                        //con esto dibujo los valores que quiero que tome el eje verical,
                                                        //ticks: [0, x, x,x] haciendo que empieze de cero
                                                        ticks: [0,
            <c:forEach items="${tablaFrecuencias}" var="inter">
                ${inter.getFo()},
            </c:forEach>
                                                        ]
                                                }
                                        };
                                        // Instantiate and draw our chart, passing in some options.
                                        var chart = new google.visualization.ColumnChart(document.getElementById('chart_div'));
                                chart.draw(data, options);
                            }
        </script>
    </head>

       <%@ include file="/WEB-INF/menu.jspf" %>
         <h1>Test Chi Cuadrado - <span style="color: red">${titulo}</span></h1>
         <hr style="width: 100%; color: black; height: 1px; background-color:black;" />
        <%
            boolean testChiCuadrado = (Boolean) (request.getAttribute("testChiCuadrado"));
            double valorChi = (Double) ((request.getAttribute("valorChi")));
            double tabuladoChi=(Double)((request.getAttribute("tabuladoChi")));
            //double fe= (Double)((request.getAttribute("fe")));
        %>
         <br>El valor de Chi Cuadrado calculado es: <%=valorChi%>
<br> El valor de Chi cuadrado tabulado para una confianza de 0,95 es: <%=tabuladoChi%>
        <%
            if (testChiCuadrado == true) {
        %>
        <br>
        <b><p style="color: green">El Generador SI pasa el test de Chi Cuadrado con una confianza de 0,95</p></b>
        <%
        } else {
        %>
        <b><p style="color: red">El Generador NO pasa el test de Chi Cuadradocon una confianza de 0,95</p></b>
        <%        }

    %>
        <%--Aca hay que hacer uso del titulo para hacer tambien una variacion, porque en la uniforme la fe es la misma para todo intervano, pero en la exponencial no--%>
        <c:choose>
            <c:when test="${titulo == 'uniformeAB'}">
               <%--para todos los intervalos--%>
               La frecuencia esperada es de: <c:out value="${tablaFrecuencias[0].getFe()}" />
            </c:when>
               
            <c:when test="${titulo == 'exponencial' || titulo == 'normal' || titulo == 'poisson'}">
                    <%-- INICIO TABLA--%>
                    
                    <table  class="table" border="1" style="width:100% ; text-align: center">
                        <%-- INICIO CABEZERA TABLA--%>  
                        <thead style="background-color: #D8D8D8" >
                            <th>Intervalo</th>
                            <th>Frecuencia Esperada</th> 
                        </thead>
                    <c:forEach items="${tablaFrecuencias}" var="inter" varStatus="loop">
                        <tbody>
                            <td> ${loop.index+1} </td>
                            <td> ${inter.getFe()}</td>
                        </tbody>
                             
                    </c:forEach>
                    </table>
              
            </c:when>

               

                
            <c:otherwise>
                por defecto
            </c:otherwise>
        </c:choose>
        <%--Div that will hold the chart--%>
        <div id="chart_div" style="text-align: left"></div>

    
</html>
