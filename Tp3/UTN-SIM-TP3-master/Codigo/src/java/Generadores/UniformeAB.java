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
public class UniformeAB implements Generable{
    
    private float inicial;
    private float end;

    
    //el constructor va a tomar los valores dependiendo de las caracteristicas de la distribucion
    public UniformeAB(float inicial, float end) {
        this.inicial = inicial;
        this.end = end;
    }
    

    @Override
    public List generar(int cantidad) {
       
        List v= new ArrayList(cantidad);
        Generador generador= new Generador(cantidad);
        for (int i = 0; i < cantidad; i++) {
            v.add(generador.nextRandom()*(end-inicial)+inicial);
        }
        return v;
    }

    @Override
    public float getAtributo() {
        return end-inicial;
    }

    @Override
    public float getMenor() {
        return inicial;
    }

    @Override
    public float getMayor() {
        return end;
    }
    
    
}
