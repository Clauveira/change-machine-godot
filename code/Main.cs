using Godot;
using System;

public class Main : Node
{
    private int[] inventario_moedas = new int[6];
    private Aplicacao aplicacao;
    public efeito_click efeito_click;

    public override void _Ready()
    {
        aplicacao = GetNode<Aplicacao>("/root/Aplicacao");
        efeito_click = GetNode<efeito_click>("efeito_click");
    }
    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        if (@event.IsActionPressed("F1"))
        {
            GetNode<AnimationPlayer>("AnimTheme").Play("Default");
        }
        if (@event.IsActionPressed("F2"))
        {
            GetNode<AnimationPlayer>("AnimTheme").Play("Dark");
        }
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