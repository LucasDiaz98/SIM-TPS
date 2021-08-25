/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package Generadores;

/**
 *
 * @author Jose
 */
public class Generador {
    
    private float seed;//semilla
    private float a;//multiplicador
    private float c;//constante aditiva
    private int m;//modulo
    private float ultimaSemilla;


    public Generador(float seed, float a, float c, int m) {
        this.seed = seed;
        this.a = a;
        this.c = c;
        this.m = m;
        this.ultimaSemilla= seed; // icicializo la ultima semilla
    }
    
    public Generador(int cantidad){
        this.seed = 11;
        this.a = 13;
        this.c = 91;
        this.m = cantidad;
        this.ultimaSemilla= seed;
    }
    
       
    public float nextRandom(){
//        ultimaSemilla = (a*ultimaSemilla  + c) % m; //divido
//        float rnd = ultimaSemilla/(m);
        float rnd=(float) Math.random();
        return rnd;
    }
    
    
    public void reiniciar(){
        ultimaSemilla=seed;
    }

}
