<%-- 
    Document   : poisson
    Created on : 13/04/2016, 22:54:51
    Author     : fback
--%>

<%@page contentType="text/html" pageEncoding="UTF-8"%>
<!DOCTYPE html>
<html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
        <link href="${pageContext.request.contextPath}/css/estilos.css" rel="stylesheet">
        <title>Distribucion Poisson</title>
    </head>
    <body>
        <%@ include file="/WEB-INF/menu.jspf" %>
        <h1>Distribución Poisson</h1>
        <hr style="width: 100%; color: black; height: 1px; background-color:black;" />

        <form method="post" action="${pageContext.request.contextPath}/generarNumeros" name="datos">
            <br>
            "En promedio llegan 3 paciente por minuto a la sala"
            <br>
            "Genero y veo en 8 minutos cuantos va a haber"
            <br>
            Valor de la Lambda = media en este caso creo:
            <input name="lamda" value="" type="number" required>
            <br><br>
            Cantidad de valores a generar:
            <input name="cantidad" value="" type="number" required>
            <br><br>
            <%-- Agrego un parametro con el mismo ID en todos los formularios, 
            pero distinto valor para cada distribucion, este parametro va a estar escondido--%>
            <input name="distribucion" value="poisson" type="hidden">
            <input type="submit" value="Generar" />
        </form>
        <p> λ es un parámetro positivo que representa el número de veces que se espera que ocurra el
            fenómeno durante un intervalo dado. Por ejemplo, si el suceso estudiado tiene lugar en 
            promedio 4 veces por minuto y estamos interesados en la probabilidad de que ocurra k 
            veces dentro de un intervalo de 10 minutos, usaremos un 
            modelo de distribución de Poisson con λ = 10×4 = 40</p>

        <p>Ejemplo :
            <br>
            Supóngase que estamos investigando la seguridad de un crucero muy peligroso. Los archivos de la policía 
            indican una media de cinco accidentes por mes en él. El número de accidentes está distribuido conforme 
            a la distribución de Poisson, y la división de seguridad en carreteras quiere calcular la probabilidad 
            de exactamente 0,1,2,3 y 4 accidentes en un mes determinado.
            <br>
            Aplicando la fórmula anterior:
            <br>
            P(0) = (5)0 (e-5) /0! = 0.00674
            <br>
            P(1) = (5)1 (e-5) /1! = 0.03370
            <br>
            P(2) = (5)2 (e-5) /2! = 0.08425
            <br>
            P(3) = (5)3 (e-5) /3! = 0.14042
            <br>
            P(4) = (5)4 (e-5) /4! = 0.17552
            <br>
            Para saber cual es la probabilidad en 3 o menos, sumaremos las probabilidades de 0,1,2,3 lo que será igual a :
            <br>
            P(0) = 0.00674
            <br>
            P(1) = 0.03370
            <br>
            P(2) = 0.08425
            <br>
            P(3) = 0.14042
            <br>
            P(3 o menos) = 0.26511
            <br>
            Dado que la probabilidad de que haya 3 o menos accidentes es de 0.26511 entonces la probabilidad de 
            que ocurran más de tres debe ser = 1 –0.26511 = 0.73489.</p>
    </body>
</html>
