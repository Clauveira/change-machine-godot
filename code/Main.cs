using Godot;
using System;

public class Main : Node
{
    private int[] inventario_moedas = new int[6];
    private Aplicacao aplicacao;

    public override void _Ready()
    {
        aplicacao = GetNode<Aplicacao>("/root/Aplicacao");
    }

    public bool abastecer_moedas(int[] array_quantidade)
    {
        if (array_quantidade.Length == inventario_moedas.Length)
        {
            for (int i = 0; i < array_quantidade.Length; i++)
            {
                inventario_moedas[i] += array_quantidade[i];
            }
            aplicacao.GetPainelMoedasControl().atualizar_quantia_exibida();
            return true;
        }
        return false;
    }

    public bool sangrar_moedas(int[] array_quantidade)
    {
        if (array_quantidade.Length == inventario_moedas.Length)
        {
            for (int i = 0; i < array_quantidade.Length; i++)
            {
                if (array_quantidade[i] > inventario_moedas[i])
                {
                    return false;
                }
            }
            for (int i = 0; i < array_quantidade.Length; i++)
            {
                inventario_moedas[i] -= array_quantidade[i];
            }
            aplicacao.GetPainelMoedasControl().atualizar_quantia_exibida();
            return true;
        }
        return false;
    }

    public int GetQuantiaValorMoeda(Tipo.VALOR_MOEDA tipo)
    {
        return inventario_moedas[((int)tipo)];
    }

    public int[] GetInventario()
    {
        return inventario_moedas;
    }
}