using Godot;
using System;

public class Main : Node
{
    private int[] inventarioMoedas = new int[6];
    public EfeitoClick efeitoClick;
    private Aplicacao aplicacao;

    public override void _Ready()
    {
        aplicacao = GetNode<Aplicacao>("/root/Aplicacao");
        efeitoClick = GetNode<EfeitoClick>("efeito_click");
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
    public bool AbastecerMoedas(int[] arrayQuantidade)
    {
        if (arrayQuantidade.Length == inventarioMoedas.Length)
        {
            for (int i = 0; i < arrayQuantidade.Length; i++)
            {
                inventarioMoedas[i] += arrayQuantidade[i];
            }
            aplicacao.GetPainelMoedasControl().atualizar_quantia_exibida();
            return true;
        }
        return false;
    }

    public bool RetirarMoedas(int[] arrayQuantidade)
    {
        if (arrayQuantidade.Length == inventarioMoedas.Length)
        {
            for (int i = 0; i < arrayQuantidade.Length; i++)
            {
                if (arrayQuantidade[i] > inventarioMoedas[i])
                {
                    return false;
                }
            }
            for (int i = 0; i < arrayQuantidade.Length; i++)
            {
                inventarioMoedas[i] -= arrayQuantidade[i];
            }
            aplicacao.GetPainelMoedasControl().atualizar_quantia_exibida();
            return true;
        }
        return false;
    }

    public int GetQuantiaValorMoeda(Tipo.VALOR_MOEDA tipo)
    {
        return inventarioMoedas[((int)tipo)];
    }

    public int[] GetInventario()
    {
        return inventarioMoedas;
    }
}