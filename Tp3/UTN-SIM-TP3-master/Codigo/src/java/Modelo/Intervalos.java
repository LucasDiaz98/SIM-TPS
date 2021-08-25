/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

package Modelo;

import javax.crypto.Mac;

/**
 *
 * @author a4
 */
public class Intervalos {
    
    private int fo;
    private float fe;
    private float limInferior;
    private float limSuperior;

    public Intervalos(int fo, float fe, float limInferior, float limSuperior) {
        this.fo = 0;
        this.fe = fe;
        this.limInferior = limInferior;
        this.limSuperior = limSuperior;
    }
    public Intervalos() {
        this.fo = 0;
        this.fe = 0;
        this.limInferior = 0;
        this.limSuperior = 0;
    }

    public int getFo() {
        return fo;
    }


    public float getFe() {
        return fe;
    }

    public void setFo(int fo) {
        this.fo = fo;
    }

    public void setFe(float fe) {
        this.fe = fe;
    }

    public void setLimInferior(float limInferior) {
        this.limInferior = limInferior;
    }

    public void setLimSuperior(float limSuperior) {
        this.limSuperior = limSuperior;
    }



    public float getLimInferior() {
        return limInferior;
    }


    public float getLimSuperior() {
        return limSuperior;
    }

    public void sumar(){
        fo++;
    }
       
    public float calcularFeExponencial(float media,int totalNumeros) {
        float prob=(float) (Math.exp(-(limInferior / media)) - Math.exp(-(limSuperior / media)));
        fe=prob*totalNumeros;
        return fe;
    }
    
    public void calcularFeNormal(float media,int totalNumeros,float desviacion, float totalIntervalo) {
        //donde mierda saco la formula?
        //http://www.uv.es/zuniga/06_La_distribucion_normal_o_de_Gauss.pdf
        float constante = (float) (1/Math.sqrt(2*Math.PI*Math.pow(desviacion,2)));
//        float A = limSuperior;
//        float B = limInferior;
//        
//        float divisiorB= B-media;
//        float primerMult = (float) (Math.pow(desviacion,2));
//        float segundoMultB = (float) Math.pow(Math.E,-(Math.pow(divisiorB,2)/2*primerMult));
//        float barrowB = (primerMult*segundoMultB/(-divisiorB));
//        
//        float divisiorA= A-media;
//        float segundoMultA = (float)  Math.pow(Math.E,-(Math.pow(divisiorA,2)/2*primerMult));
//        float barrowA = (primerMult*segundoMultA/(-divisiorA));
//        
//        
//        float prob=(float)constante*(barrowB-barrowA);
         //funcion densidad
        float marcaClase = (limSuperior+limInferior)/2;
        double exponente=(-(Math.pow((marcaClase-media),2))/(2*desviacion*desviacion));;
        float factorQueMultiplica = (float)  Math.pow(Math.E,exponente);
        float prob =  constante*factorQueMultiplica;
        fe=prob*totalNumeros*totalIntervalo;
        
    }
    
    public float calcularFePoisson(float media, int totalNumeros){
            
            //float prob= (float) ((Math.exp(-(media)))*(Math.pow(media, limSuperior))) / (factorial(limSuperior));
            //P(x) = l x * e-l / x! 
            //http://www.gestiopolis.com/que-es-la-distribucion-de-poisson/
            //calculo la proba de la marca en vez de la del intervalo
            int marcaDeClase = (int) ((limSuperior+limInferior)/2);
            float prob= (float) ((Math.exp(-(media)))*(Math.pow(media, marcaDeClase))) / (factorial(marcaDeClase));
            fe=prob*totalNumeros;
            return fe;
        }
    public long factorial (int numero) {
        if (numero==0)
          return 1;
        else
          return (long) (numero * factorial(numero-1));
     }
   
    
    
}
