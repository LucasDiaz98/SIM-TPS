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

    <body>
        <%@ include file="/WEB-INF/menu.jspf" %>
         <h1>Grafico Solo - <span style="color: red">${titulo}</span></h1>
         <hr style="width: 100%; color: black; height: 1px; background-color:black;" />
        
        <%--Div that will hold the chart--%>
        <div id="chart_div" style="text-align: left"></div>

    </body>
</html>
