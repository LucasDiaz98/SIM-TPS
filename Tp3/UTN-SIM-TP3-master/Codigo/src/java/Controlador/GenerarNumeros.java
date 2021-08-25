/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package Controlador;

import Generadores.*;
import Interfaces.Generable;
import java.io.IOException;
import java.util.List;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

/**
 *
 * @author Jose
 */
@WebServlet(name = "generarNumeros", urlPatterns = {"/generarNumeros"})
public class GenerarNumeros extends HttpServlet {

    protected void processRequest(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException {

        //Hay un atributo hidden en cada formulario que me decide en que parte de switch entra
        //para poder unificar el jsp en uno solo y cambiar el titulo
        //debe coincidir con el switch
        String opcionDist = request.getParameter("distribucion");
        request.setAttribute("titulo", opcionDist);
        //la cantidad es lo mismo para cada distribucion
        int cantidad = Integer.parseInt(request.getParameter("cantidad"));
        //esto deberia validarse con javascript
        if (cantidad <= 0) {
            request.getRequestDispatcher("error.jsp");
            return;
        }

        HttpSession session = request.getSession(true);
        List numeros = null;
        //implementamos un strategy dependiendo de la distribucion a generar
        Generable generador;
        float media;
        switch (opcionDist) {
            case "uniformeAB":
                //tomo los valores del formulario
                float inicial = Float.parseFloat(request.getParameter("inicial"));
                float end = Float.parseFloat(request.getParameter("end"));
                //creo un generador y los numeros
                generador = new UniformeAB(inicial, end);
                numeros = generador.generar(cantidad);
                //Esta informacion la voy a usar a la otra de testear
                session.setAttribute("inicial", inicial);
                float diferencia = generador.getAtributo();
                session.setAttribute("diferencia", diferencia);
                break;
            case "exponencial":
                //tomo los valores del formulario
                media = Float.parseFloat(request.getParameter("media"));
                //creo un generador y los numeros
                generador = new Exponencial(media);
                numeros = generador.generar(cantidad);
                //Esta informacion la voy a usar a la otra de testear y graficar
                float mayor = generador.getAtributo();
                session.setAttribute("inicial", -0f);
                session.setAttribute("diferencia", mayor);
                session.setAttribute("media", media);
                break;
            case "poisson":
                 //Haga primero la parte de generar y luego vuelve aca para agregar la info necesaria para tester
                float lambda = Float.parseFloat(request.getParameter("lamda"));
                
                generador = new Poisson(lambda);
                numeros=generador.generar(cantidad);
                //Esta informacion la voy a usar a la otra de testear y graficar
                //Esta informacion la voy a usar a la otra de testear y graficar
                session.setAttribute("lamda", lambda);
                mayor=generador.getAtributo();
                session.setAttribute("inicial", -0f);
                session.setAttribute("diferencia", mayor);
                break;
            case "normal":
                //tomo los valores del formulario
                media = Float.parseFloat(request.getParameter("media"));
                float desviacion = Float.parseFloat(request.getParameter("desviacion"));
                //creo un generador y los numeros
                generador = new Normal(media, desviacion);
                numeros = generador.generar(cantidad);
                //Esta informacion la voy a usar a la otra de testear y graficar
               
                session.setAttribute("inicial", generador.getMenor());
                session.setAttribute("diferencia", generador.getAtributo());
                session.setAttribute("mayorNormal", generador.getMayor());
                session.setAttribute("desviacion", desviacion);
                session.setAttribute("media", media);

                break;
        }

        //para todo va a ser lo mismo
        session.setAttribute("numerosGenerados", numeros);
        session.setAttribute("cantidadNumeros", cantidad);
        request.getRequestDispatcher("mostrar.jsp").forward(request, response);
    }

// <editor-fold defaultstate="collapsed" desc="HttpServlet methods. Click on the + sign on the left to edit the code.">
    /**
     * Handles the HTTP <code>GET</code> method.
     *
     * @param request servlet request
     * @param response servlet response
     * @throws ServletException if a servlet-specific error occurs
     * @throws IOException if an I/O error occurs
     */
    @Override
    protected void doGet(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException {
        processRequest(request, response);
    }

    /**
     * Handles the HTTP <code>POST</code> method.
     *
     * @param request servlet request
     * @param response servlet response
     * @throws ServletException if a servlet-specific error occurs
     * @throws IOException if an I/O error occurs
     */
    @Override
    protected void doPost(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException {
        processRequest(request, response);
    }

    /**
     * Returns a short description of the servlet.
     *
     * @return a String containing servlet description
     */
    @Override
    public String getServletInfo() {
        return "Short description";
    }// </editor-fold>

}
