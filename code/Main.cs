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

    public bool abastecer_moedas(int[] quantidade)
    {
        if (quantidade.Length == inventario_moedas.Length)
        {
            for (int i = 0; i < quantidade.Length; i++)
            {
                inventario_moedas[i] += quantidade[i];
            }
            aplicacao.GetPainelMoedasControl().atualizar_quantia_exibida();
            return true;
        }
        return false;
    }

    public bool sangrar_moedas(int[] quantidade)
    {
        if (quantidade.Length == inventario_moedas.Length)
        {
            for (int i = 0; i < quantidade.Length; i++)
            {
                if (quantidade[i] > inventario_moedas[i])
                {
                    return false;
                }
            }
            for (int i = 0; i < quantidade.Length; i++)
            {
                inventario_moedas[i] -= quantidade[i];
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
}