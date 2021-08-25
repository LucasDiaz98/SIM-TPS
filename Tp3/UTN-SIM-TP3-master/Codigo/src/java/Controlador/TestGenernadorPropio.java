/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package Controlador;

import Interfaces.Testeable;
import Modelo.Intervalos;
import Testeadores.TestNormal;
import Testeadores.TestPoisson;
import Testeadores.TestUniforme;
import Testeadores.TesteExponencial;
import java.io.IOException;
import java.nio.charset.Charset;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.List;
import java.util.logging.Level;
import java.util.logging.Logger;
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
@WebServlet(name = "TestGenernadorPropio", urlPatterns = {"/TestGenernadorPropio"})
public class TestGenernadorPropio extends HttpServlet {

    protected void processRequest(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException {
        //lo que me pasan por parametro
        int cantidadIntervalos = Integer.parseInt(request.getParameter("cantidadIntervalos"));
        //valido los datos que sean correctos, EN realidad se hace con javascript del lado del cliente
        if (cantidadIntervalos < 2 || cantidadIntervalos > 32) {
            request.getRequestDispatcher("error.jsp").forward(request, response);
        }
        HttpSession session = request.getSession(true);
        //Hay un atributo hidden en cada formulario que me decide en que parte de switch entra
        //para poder unificar el jsp en uno solo y cambiar el titulo
        //debe coincidir con el switch
        String opcionDist = request.getParameter("distribucion");
        request.setAttribute("titulo", opcionDist);
        //creo un tester con todos los numeros a testear y la cantidad de intervalos
        Testeable tester = null;
        float media;
        float desviacion;
        List numerosGenerados = (List) session.getAttribute("numerosGenerados");
        switch (opcionDist) {
            case "uniformeAB":
                //cada tester va a tener un constructor diferente
                tester = new TestUniforme(numerosGenerados, cantidadIntervalos);
                break;
            case "exponencial":
                //recupero la info del servlet anterior para el contructor
                media=(float)session.getAttribute("media");
                //cada tester va a tener un constructor diferente
                tester = new TesteExponencial(numerosGenerados, cantidadIntervalos,media );
                break;
            case "poisson":
                Float lamda=(float)session.getAttribute("lamda");
                tester = new TestPoisson(numerosGenerados, cantidadIntervalos,lamda);
                break;
            case "normal":
                //recupero la info del servlet anterior para el contructor
                media=(float)session.getAttribute("media");
                desviacion=(float)session.getAttribute("desviacion");
                //cada tester va a tener un constructor diferente
                tester = new TestNormal(numerosGenerados, cantidadIntervalos, media, desviacion);             
                break;
        }
        
        //la tabla de frecuencia es lo que debo graficar
        //la tabla de frecuencia la vamos a tratar de unificar para todas las ditribuciones
        //neceisto calcular el valor inicual de la tabla y su diferencia con respecto al otro
        float inicial = (Float) (session.getAttribute("inicial"));
        float diferencia = (Float) (session.getAttribute("diferencia"));
        Intervalos[] tablaFrecuencias = tester.generarTablaFrecuenciasGeneral(diferencia, inicial);
        //comento la parte del test
        
        //para todos tienen el mismo nombre, pero vuelo a usar un strategy
        request.setAttribute("tablaFrecuencias", tablaFrecuencias);
        
        double valorChiCuadrado= tester.valorTestChiCuadrado(tablaFrecuencias);        
        request.setAttribute("valorChi", valorChiCuadrado);
        
        boolean resultado = tester.testChiCuadrado(tablaFrecuencias);
        request.setAttribute("testChiCuadrado", resultado);

        double tabuladoChiCuadrado = tester.tabuladoChiCuadrado();
        request.setAttribute("tabuladoChi", tabuladoChiCuadrado);
        
        //generarTxt();
        //request.getRequestDispatcher("graficoSinTest.jsp").forward(request, response);
        request.getRequestDispatcher("grafico.jsp").forward(request, response);
    }

    private void generarTxt(List lista) {
        Path file = Paths.get("C:\\Users\\Jose\\Desktop\\numerosPropios.txt");

        try {
            Files.write(file, lista, Charset.forName("UTF-8"));
        } catch (IOException ex) {
            Logger.getLogger(TestGenernadorPropio.class.getName()).log(Level.SEVERE, null, "error al crear txt");
            Logger.getLogger(TestGenernadorPropio.class.getName()).log(Level.SEVERE, null, ex);
        }
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
