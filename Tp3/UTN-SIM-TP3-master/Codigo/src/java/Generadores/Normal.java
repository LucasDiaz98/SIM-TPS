/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package Generadores;

import Interfaces.Generable;
import static java.lang.Math.sqrt;
import java.util.ArrayList;
import java.util.List;

/**
 *
 * @author jorge
 */
public class Normal implements Generable {

    private float media;
    private float desviacionEstandar;
    private float mayor;
    private float menor;

    public Normal(float media, float desviacionEstandar) {
        this.media = media;
        this.desviacionEstandar = desviacionEstandar;
    }

    public List generar(int cantidad) {
        List v = new ArrayList(cantidad);
        Generador generador = new Generador(cantidad);

        for (int i = 0; i < (cantidad / 2); i++) {
            float random1 = generador.nextRandom();
            float random2 = generador.nextRandom();
            while (random1 == 0f) {
                random1 = generador.nextRandom();
            }
            float x1estandar = (float) (sqrt(-2 * Math.log(random1)) * Math.cos(2 * Math.PI * random2));
            float x2estandar = (float) (sqrt(-2 * Math.log(random1)) * Math.sin(2 * Math.PI * random2));
            //me da valores infinitos
//            if (Math.abs(x1estandar) > 10) {
//                if (x1estandar > 10) {
//                    x1estandar = 10;
//                }
//                if (x1estandar < 10) {
//                    x1estandar = -10;
//                }
//
//            }
//            if (Math.abs(x2estandar) > 10) {
//                if (x2estandar > 10) {
//                    x2estandar = 10;
//                }
//                if (x2estandar < 10) {
//                    x2estandar = -10;
//                }
//
//            }
            float x1 = x1estandar * desviacionEstandar + media;
            float x2 = x2estandar * desviacionEstandar + media;

            v.add(x1);
            v.add(x2);
            //guardo el mayor para graficar
            if (x1 > mayor) {
                mayor = x1;
            }

            if (x2 > mayor) {
                mayor = x2;
            }
            if (i == 0) {
                if(x1<x2){
                    menor=x1;
                } else{
                    menor=x2;
                }
            } else {
                if (x1 < menor) {
                    menor = x1;
                }

                if (x2 < menor) {
                    menor = x2;
                }
            }

        }
        return v;
    }

    @Override
    public float getAtributo() {
        return mayor - menor;
    }

    public float getMedia() {
        return media;
    }

    public void setMedia(float media) {
        this.media = media;
    }

    public float getDesviacionEstandar() {
        return desviacionEstandar;
    }

    public void setDesviacionEstandar(float desviacionEstandar) {
        this.desviacionEstandar = desviacionEstandar;
    }

    @Override
    public float getMenor() {
        return menor;
    }

    @Override
    public float getMayor() {
        return mayor;
    }

}
