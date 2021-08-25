/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package Generadores;

import Interfaces.Generable;
import java.util.ArrayList;
import java.util.List;
import java.util.Random;

/**
 *
 * @author jorge
 */
public class Poisson implements Generable {
    

    private float lambda;
    final float epsilon =(float) Math.E;
    private float mayor;
    //el constructor va a tomar los valores dependiendo de las caracteristicas de la distribucion
    
    
    
    public Poisson(float m)
    {
        this.lambda = m;
    }
    
    public List generar(int cantidad) {
        List v = new ArrayList();
        float valor;
        for (int i = 0; i < cantidad; i++) {
            valor=poisson(lambda,cantidad);
            v.add(valor);
            if (valor > mayor) {
                mayor = valor;
            }
        }
        
        return v;
    }

    @Override
    public float getAtributo() {
        return mayor;
    }
    
    public int poisson(float lambda, int cantidad)
    {
        Generador generador = new Generador(cantidad);
        int x = -1;
        float p = 1;
        float a = (float) Math.exp(-lambda);
        
        
        float num;
        do{
            float random = generador.nextRandom();
            p = p*random;
            x++;
        }
        while(p >= a);
        
        return x;
    }

    @Override
    public float getMenor() {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }

    @Override
    public float getMayor() {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }
    
}
