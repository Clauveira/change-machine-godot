using Godot;
using System;

public class AbastecimentoSangria : Control
{
    private bool ehSangria = false;
    private Aplicacao AplicacaoNode;

    public override void _Ready()
    {
        AplicacaoNode = GetNode<Aplicacao>("/root/Aplicacao");
    }

    public void _on_AbastecimentoButton_pressed()
    {
        ehSangria = false;
        if (AplicacaoNode.eh_mobile)
        {
            AplicacaoNode.GetAbastecimentoSangriaWindowDialog().Popup_();
        }
        else
        {
            AplicacaoNode.GetAbastecimentoSangriaWindowDialog().PopupCentered();
        }
        AplicacaoNode.GetAbastecimentoSangriaWindowDialog().WindowTitle = "Abastecimento";
        AnalizarOcultarMoedas();
    }
    public void _on_SangriaButton_pressed()
    {
        ehSangria = true;
        if (AplicacaoNode.eh_mobile)
        {
            AplicacaoNode.GetAbastecimentoSangriaWindowDialog().Popup_();
        }
        else
        {
            AplicacaoNode.GetAbastecimentoSangriaWindowDialog().PopupCentered();
        }

        AplicacaoNode.GetAbastecimentoSangriaWindowDialog().WindowTitle = "Sangria";
        AnalizarOcultarMoedas();
    }

    public void _on_ButtonOk_pressed()
    {
        int[] array_quantidade_alterar = new int[AplicacaoNode.GetMainNode().GetInventario().Length];
        for (int i = 0; i < AplicacaoNode.GetMainNode().GetInventario().Length; i++)
        {
            array_quantidade_alterar[i] = GetValorAlterarNaPosicao(i);
        }

        if (ehSangria)
        {
            AplicacaoNode.GetMainNode().RetirarMoedas(array_quantidade_alterar);
        }
        else
        {
            AplicacaoNode.GetMainNode().AbastecerMoedas(array_quantidade_alterar);
        }
        AplicacaoNode.GetAbastecimentoSangriaWindowDialog().Hide();
        LimparCampos();
    }

    public int GetValorAlterarNaPosicao(int posicao)
    {
        return System.Convert.ToInt32(
        GetNode<SpinBox>("VBoxContainer/MarginContainer/ListaQuantiaMoedas/QuantiaDeMoedasAlterar"
            + posicao + "/Panel/MarginContainer/HBoxContainer/SpinBox").Value);
    }

    public void _on_ButtonCancelar_pressed()
    {
        AplicacaoNode.GetAbastecimentoSangriaWindowDialog().Hide();
        LimparCampos();
    }

    public void _on_AbastecimentoWindowDialog_about_to_show()
    {
        RescalonarValores(ehSangria);
    }

    public void LimparCampos()
    {
        for (int i = 0; i < AplicacaoNode.GetMainNode().GetInventario().Length; i++)
        {
            GetNode<SpinBox>("VBoxContainer/MarginContainer/ListaQuantiaMoedas/QuantiaDeMoedasAlterar"
            + i + "/Panel/MarginContainer/HBoxContainer/SpinBox").Value = 0;
        }
    }
    public void RescalonarValores(bool eh_sangria)
    {
        int novoValorMaximo = 999;
        for (int i = 0; i < AplicacaoNode.GetMainNode().GetInventario().Length; i++)
        {
            if (eh_sangria)
            {
                novoValorMaximo = AplicacaoNode.GetMainNode().GetInventario()[i];
            }
            GetNode<SpinBox>("VBoxContainer/MarginContainer/ListaQuantiaMoedas/QuantiaDeMoedasAlterar"
            + i + "/Panel/MarginContainer/HBoxContainer/SpinBox").MaxValue = novoValorMaximo;
        }
    }

    public void AnalizarOcultarMoedas()
    {
        bool newVisible = true;
        bool ehSemMoedas = true;
        for (int i = 0; i < AplicacaoNode.GetMainNode().GetInventario().Length; i++)
        {
            if (ehSangria)
            {
                newVisible = AplicacaoNode.GetMainNode().GetInventario()[i] > 0;
            }
            GetNode<Control>("VBoxContainer/MarginContainer/ListaQuantiaMoedas/QuantiaDeMoedasAlterar" + i).Visible = newVisible;
            if (ehSemMoedas && newVisible == true)
            {
                ehSemMoedas = false;
            }
        }
        GetNode<Label>("VBoxContainer/MarginContainer/SemMoedas").Visible = ehSemMoedas;
    }
}
