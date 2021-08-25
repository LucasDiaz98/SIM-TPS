/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package Generadores;

import Interfaces.Generable;
import java.util.ArrayList;
import java.util.List;

/**
 *
 * @author jorge
 */
public class Exponencial implements Generable {

    private float media;
    private float mayor;
    final float epsilon = (float) Math.E;

    public Exponencial(float media) {
        this.media = media;
        mayor = 0;
    }

    @Override
    public float getAtributo() {
        return mayor;
    }

    @Override
    public List generar(int cantidad) {
        List v = new ArrayList(cantidad);
        Generador generador = new Generador(cantidad);
        float valor;
        
        for (int i = 0; i < cantidad; i++) {
            
            valor = (float) ((-media) * Math.log(1 - generador.nextRandom()));

            v.add(valor);
            if (valor > mayor) {
                mayor = valor;
            }
        }
        return v;
    }

    @Override
    public float getMenor() {
        return 0;
    }

    @Override
    public float getMayor() {
        return mayor;
    }

}
