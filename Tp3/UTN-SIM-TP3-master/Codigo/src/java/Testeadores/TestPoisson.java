/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package Testeadores;

import Interfaces.Testeable;
import Modelo.Intervalos;
import java.util.ArrayList;
import java.util.List;

/**
 *
 * @author Jose
 */
public class TestPoisson implements Testeable {

    private double[] tabulado;
    private int cantidadIntervalos;
    private int cantidadNumeros;
    private double resultado;
    private List<Float> numerosAtestear;
    private float lambda;

    public TestPoisson(List<Float> numerosAtestear, int cantidadIntervalos, float lambda) {
        this.numerosAtestear = numerosAtestear;
        this.lambda = lambda;
        this.cantidadIntervalos = cantidadIntervalos;
        //
        this.tabulado = chiCuadrado();
        this.cantidadNumeros = numerosAtestear.size();
    }

    @Override
    public Intervalos[] generarTablaFrecuenciasGeneral(float diferencia, float inicial) {
        if (cantidadIntervalos == 0) {
            Intervalos[] tablaFrencuenciasDiscreta = new Intervalos[(int) diferencia];
            for (int i = 0; i < tablaFrencuenciasDiscreta.length; i++) {
                //iniciliazo
                tablaFrencuenciasDiscreta[i] = new Intervalos(0, 0, i, i);
                for (Float v : numerosAtestear) {
                    if (v == i) {

                        tablaFrencuenciasDiscreta[i].sumar();

                    }
                }

            }
            return tablaFrencuenciasDiscreta;

        } else {
            Intervalos[] tablaFrencuencias = new Intervalos[cantidadIntervalos];

            for (int j = 0; j < cantidadIntervalos; j++) {

                float limiteSuperior = limiteSuperior(j, diferencia, inicial);
                float limiteInferior = limiteInferior(j, diferencia, inicial);
                tablaFrencuencias[j] = new Intervalos(0, 0, limiteInferior, limiteSuperior);
                for (Float v : numerosAtestear) {
                //si el objeto v de la lista de numeros esta dentro del itervalo debo contar
                    //en el ultimo intervalo, toma el limite superior
                    if (j == cantidadIntervalos - 1) {
                        if (v <= limiteSuperior + 0.01f && v >= limiteInferior) {
                            tablaFrencuencias[j].sumar();

                        }
                    } else {
                        //en los demas casaso tomas menor solo
                        if (v < limiteSuperior && v >= limiteInferior) {
                            tablaFrencuencias[j].sumar();

                        }

                    }

                }
                
                tablaFrencuencias[j].calcularFePoisson(lambda, cantidadNumeros);
            }
            return tablaFrencuencias;
        }

    }
    
    private float limiteSuperior(int j, float diferencia, float inicial) {
        return ((diferencia * (j + 1) / (float) cantidadIntervalos)) + inicial;
    }

    private float limiteInferior(int j, float diferencia, float inicial) {
        return ((diferencia * (j) / (float) cantidadIntervalos)) + inicial;
    }
        public ArrayList<Intervalos> restructurarTabla(Intervalos[] tablaFrecuencias) {
        ArrayList<Intervalos> tablaNuevosIntervalos = new ArrayList();
        float feAc = 0;
        float feAcSig = 0;
        int foAc = 0;
        int foAcSig = 0;
        float limSup = 0;
        float limSupSig = 0;

        for (int i = 0; i < tablaFrecuencias.length; i++) {
            Intervalos aux = new Intervalos();
            feAcSig = 0;
            foAc = 0;
            foAcSig = 0;
            limSup = 0;
            limSupSig = 0;
            int j=0;
            if (tablaFrecuencias[i].getFe() < 5) {

                for (j= i; j < tablaFrecuencias.length; j++) {

                    feAc += tablaFrecuencias[j].getFe();

                    foAc += tablaFrecuencias[j].getFe();
                    limSup = tablaFrecuencias[j].getLimSuperior();
                    if (feAc >= 5) {
                        for (int k = j+1; k < tablaFrecuencias.length; k++) {

                            feAcSig += tablaFrecuencias[k].getFe();

                            foAcSig += tablaFrecuencias[k].getFe();
                            limSupSig = tablaFrecuencias[k].getLimSuperior();
                            if (feAcSig >= 5 &&k>tablaFrecuencias.length-1) {
                                break;
                            } 
                            if (feAcSig < 5 &&k==tablaFrecuencias.length-1){
                                feAc+=feAcSig;
                                foAc+=foAcSig;
                                limSup=limSupSig;
                            }
                            
                        }
                        //break;
                    }
                }
                aux.setFe(feAc);
                aux.setFo(foAc);
                aux.setLimSuperior(limSup);
                aux.setLimInferior(tablaFrecuencias[i].getLimInferior());
                tablaNuevosIntervalos.add(aux);
                i=j;
            } else {
                tablaNuevosIntervalos.add(tablaFrecuencias[i]);
            }
            
        }
        return tablaNuevosIntervalos;
    }
        

    @Override
    public double valorTestChiCuadrado(Intervalos[] tablaFrecuencias) {
        //es nuevo metodo llamado restructurar tabla!1!!!!
        ArrayList<Intervalos> tablaNuevosIntervalos = restructurarTabla(tablaFrecuencias);
        for (Intervalos tablaNuevosIntervalo : tablaNuevosIntervalos) {
            resultado += (Math.pow(tablaNuevosIntervalo.getFo() - tablaNuevosIntervalo.getFe(), 2)) / tablaNuevosIntervalo.getFe();
            
        }

        return resultado;
    }
    
    

    @Override
    public boolean testChiCuadrado(Intervalos[] tablaFrecuencias) {
        if (cantidadIntervalos > 1) {
            if (tabuladoChiCuadrado() > resultado) {
                return true;
            }

        }

        return false;
    }

    @Override
    public double tabuladoChiCuadrado() {
        return tabulado[cantidadIntervalos - 2];
    }

    @Override
    public double frecuenciaEsparada() {
        return 0;
    }

    private double[] chiCuadrado() {
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
}
