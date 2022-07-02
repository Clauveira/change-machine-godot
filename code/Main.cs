using Godot;
using System;

public class Main : Node
{
    private float[] inventario_moedas = new float[6];
    public WindowDialog ResultadoWindowDialog;
    
    public override void _Ready()
    {
        ResultadoWindowDialog = GetNode<WindowDialog>("CanvasLayer/ResultadoWindowDialog");
    }

    public bool abastecer_moedas(float[] quantidade){
        if (quantidade.Length == inventario_moedas.Length){
            for (int i = 0; i < quantidade.Length; i++)
            {
                inventario_moedas[i] += quantidade[i];
            }
            return true;
        }
        return false;
    }

    public bool sangrar_moedas(float[] quantidade)
    {
        if (quantidade.Length == inventario_moedas.Length){
            for (int i = 0; i < quantidade.Length; i++)
            {
                if (quantidade[i] > inventario_moedas[i]){
                    return false;
                }
            }
            for (int i = 0; i < quantidade.Length; i++)
            {
                inventario_moedas[i] -= quantidade[i];
            }
            return true;
        }
        return false;
    }
}