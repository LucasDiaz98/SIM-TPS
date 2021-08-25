/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package Testeadores;

import Interfaces.Testeable;
import Modelo.Intervalos;
import java.util.List;

/**
 *
 * @author Jose
 */
public class TestUniforme implements Testeable {

    private final int cantidadIntervalos;
    private int cantidadNumeros;
    private double resultado;
    private double[] tabulado;
    private List<Float> numerosAtestear;

    public TestUniforme(List numerosATestear, int cantidadIntervalos) {
        this.numerosAtestear = numerosATestear;
        this.tabulado = chiCuadrado();
        this.cantidadIntervalos = cantidadIntervalos;
        this.cantidadNumeros = numerosATestear.size();
    }

    //calcula el valor de chi cuadrado para la serie
    public double valorTestChiCuadrado(Intervalos[] tablaFrecuencias) {
        double fe = frecuenciaEsparada();
        for (int i = 0; i < tablaFrecuencias.length; i++) {
            resultado += (Math.pow(tablaFrecuencias[i].getFo() - fe, 2)) / fe;
        }
        return resultado;
    }

    //verifica si la frecuencia acumulada "resultado" es menor a la frecuencia tabulada en chi cuadrado
    public boolean testChiCuadrado(Intervalos[] tablaFrecuencias) {

        if (cantidadIntervalos > 1) {
            if (tabuladoChiCuadrado() > resultado) {
                return true;
            }

        }

        return false;
    }

    public double frecuenciaEsparada() {
        double fe = cantidadNumeros / cantidadIntervalos;
        return fe;
    }

    public double tabuladoChiCuadrado() {
        return tabulado[cantidadIntervalos - 2];
    }

    public double[] chiCuadrado() {
        tabulado = new double[30];
        tabulado[0] = 3.841;
        tabulado[1] = 5.991;
        tabulado[2] = 7.815;
        tabulado[3] = 9.488;
        tabulado[4] = 11.070;
        tabulado[5] = 12.592;
        tabulado[6] = 14.067;
        tabulado[7] = 15.507;
        tabulado[8] = 16.919;
        tabulado[9] = 18.307;
        tabulado[10] = 19.675;
        tabulado[11] = 21.026;
        tabulado[12] = 22.362;
        tabulado[13] = 23.685;
        tabulado[14] = 24.996;
        tabulado[15] = 26.296;
        tabulado[16] = 27.587;
        tabulado[17] = 28.869;
        tabulado[18] = 30.144;
        tabulado[19] = 31.410;
        tabulado[20] = 32.671;
        tabulado[21] = 33.924;
        tabulado[22] = 35.172;
        tabulado[23] = 36.415;
        tabulado[24] = 37.652;
        tabulado[25] = 38.885;
        tabulado[26] = 40.113;
        tabulado[27] = 41.337;
        tabulado[28] = 42.557;
        tabulado[29] = 43.773;
        return tabulado;
    }

    @Override
    public Intervalos[] generarTablaFrecuenciasGeneral(float diferencia, float inicial) {
        Intervalos[] tablaFrencuencias = new Intervalos[cantidadIntervalos];

        for (int j = 0; j < cantidadIntervalos; j++) {
            float limiteSuperior = ((diferencia * (j + 1) / (float) cantidadIntervalos)) + inicial;
            float limiteInferior = ((diferencia * (j) / (float) cantidadIntervalos)) + inicial;
            //inicializo
            tablaFrencuencias[j] = new Intervalos(0, 0, limiteInferior, limiteSuperior);
            for (Float v : numerosAtestear) {
                //si el objeto v de la lista de numeros esta dentro del itervalo debo contar
                if (v < limiteSuperior && v >= limiteInferior) {
                    tablaFrencuencias[j].sumar();

                }
            }
            tablaFrencuencias[j].setLimInferior(limiteInferior);
            tablaFrencuencias[j].setLimSuperior(limiteSuperior);
            tablaFrencuencias[j].setFe((float) frecuenciaEsparada());

        }
        return tablaFrencuencias;
    }

    

    
}
