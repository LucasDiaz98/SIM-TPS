/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package Interfaces;

import java.util.List;


/**
 *
 * @author jorge
 */
public interface Generable {
    
    public List generar(int cantidad);

    public float getAtributo();
    
    public float getMenor();
    
    public float getMayor();
}
