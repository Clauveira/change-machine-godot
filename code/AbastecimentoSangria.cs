using Godot;
using System;

public class AbastecimentoSangria : Control
{
    private bool eh_sangria = false;
    private Aplicacao aplicacao;

    public override void _Ready()
    {
        aplicacao = GetNode<Aplicacao>("/root/Aplicacao");
    }

    public void _on_AbastecimentoButton_pressed()
    {
        eh_sangria = false;
        aplicacao.GetAbastecimentoSangriaWindowDialog().PopupCentered();
        aplicacao.GetAbastecimentoSangriaWindowDialog().WindowTitle = "Abastecimento";
        AnalizarOcultarMoedas();
    }
    public void _on_SangriaButton_pressed()
    {
        eh_sangria = true;
        aplicacao.GetAbastecimentoSangriaWindowDialog().PopupCentered();
        aplicacao.GetAbastecimentoSangriaWindowDialog().WindowTitle = "Sangria";
        AnalizarOcultarMoedas();
    }

    public void _on_ButtonOk_pressed()
    {
        int[] array_quantidade_alterar = new int[aplicacao.GetMainNode().GetInventario().Length];
        for (int i = 0; i < aplicacao.GetMainNode().GetInventario().Length; i++)
        {
            array_quantidade_alterar[i] = GetValorAlterarNaPosicao(i);
        }

        if (eh_sangria)
        {
            aplicacao.GetMainNode().sangrar_moedas(array_quantidade_alterar);
        }
        else
        {
            aplicacao.GetMainNode().abastecer_moedas(array_quantidade_alterar);
        }
        aplicacao.GetAbastecimentoSangriaWindowDialog().Hide();
    }

    public int GetValorAlterarNaPosicao(int posicao)
    {
        return System.Convert.ToInt32(
        GetNode<SpinBox>("VBoxContainer/MarginContainer/ListaQuantiaMoedas/QuantiaDeMoedasAlterar"
            + posicao + "/Panel/MarginContainer/HBoxContainer/SpinBox").Value);
    }

    public void _on_ButtonCancelar_pressed()
    {
        aplicacao.GetAbastecimentoSangriaWindowDialog().Hide();
    }

    public void _on_AbastecimentoWindowDialog_about_to_show()
    {
        LimparCampos();
        RescalonarValores(eh_sangria);
    }

    public void LimparCampos()
    {
        for (int i = 0; i < aplicacao.GetMainNode().GetInventario().Length; i++)
        {
            GetNode<SpinBox>("VBoxContainer/MarginContainer/ListaQuantiaMoedas/QuantiaDeMoedasAlterar"
            + i + "/Panel/MarginContainer/HBoxContainer/SpinBox").Value = 0;
        }
    }
    public void RescalonarValores(bool eh_sangria)
    {
        int novo_valor_maximo = 999;
        for (int i = 0; i < aplicacao.GetMainNode().GetInventario().Length; i++)
        {
            if (eh_sangria)
            {
                novo_valor_maximo = aplicacao.GetMainNode().GetInventario()[i];
            }
            GetNode<SpinBox>("VBoxContainer/MarginContainer/ListaQuantiaMoedas/QuantiaDeMoedasAlterar"
            + i + "/Panel/MarginContainer/HBoxContainer/SpinBox").MaxValue = novo_valor_maximo;
        }
    }

    public void AnalizarOcultarMoedas()
    {
        bool new_visible = true;
        bool eh_sem_moedas = true;
        for (int i = 0; i < aplicacao.GetMainNode().GetInventario().Length; i++)
        {
            if (eh_sangria)
            {
                new_visible = aplicacao.GetMainNode().GetInventario()[i] > 0;
            }
            GetNode<Control>("VBoxContainer/MarginContainer/ListaQuantiaMoedas/QuantiaDeMoedasAlterar" + i).Visible = new_visible;
            if (eh_sem_moedas && new_visible == true)
            {
                eh_sem_moedas = false;
            }
        }
        GetNode<Label>("VBoxContainer/MarginContainer/SemMoedas").Visible = eh_sem_moedas;
    }
}
