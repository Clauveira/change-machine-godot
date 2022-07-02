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
        aplicacao.GetAbastecimentoSangriaWindowDialog().PopupCentered();
        eh_sangria = false;
    }
    public void _on_SangriaButton_pressed()
    {
        aplicacao.GetAbastecimentoSangriaWindowDialog().PopupCentered();
        eh_sangria = true;
    }

    public void _on_ButtonOk_pressed()
    {
        if (eh_sangria)
        {
            aplicacao.GetMainNode().sangrar_moedas(new int[6] { 5, 5, 5, 5, 5, 5 });
        }
        else
        {
            aplicacao.GetMainNode().abastecer_moedas(new int[6] { 5, 5, 5, 5, 5, 5 });
        }
        aplicacao.GetAbastecimentoSangriaWindowDialog().Hide();
    }

    public void _on_ButtonCancelar_pressed()
    {
        aplicacao.GetAbastecimentoSangriaWindowDialog().Hide();
    }
}
