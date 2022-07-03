using Godot;
using System;

public class ResultadoControl : VBoxContainer
{
    private Aplicacao aplicacao;

    public override void _Ready()
    {
        aplicacao = GetNode<Aplicacao>("/root/Aplicacao");
    }

    public void _on_ButtonCancelar_pressed()
    {
        aplicacao.GetResultadoWindowDialog().Hide();
    }

    public void _on_ButtonOk_pressed()
    {
        //TODO: Remover as moedas do inventário
        aplicacao.GetResultadoWindowDialog().Hide();
    }
}
