/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package Interfaces;

import Modelo.Intervalos;

/**
 *
 * @author Jose
 */
public interface Testeable {
        public Intervalos[] generarTablaFrecuenciasGeneral(float diferencia, float inicial);
        public double valorTestChiCuadrado(Intervalos[] tablaFrecuencias);
        public boolean testChiCuadrado(Intervalos[] tablaFrecuencias);
        public double tabuladoChiCuadrado();
        //cada distribucion va a tener disitnta frecuecnia esperada
        public double frecuenciaEsparada();
        
}
