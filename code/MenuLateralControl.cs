using Godot;
using System;

public class MenuLateralControl : Control
{
    private Aplicacao AplicacaoNode;

    public override void _Ready()
    {
        AplicacaoNode = GetNode<Aplicacao>("/root/Aplicacao");
        AplicacaoNode.PaginaPrincipal.Visible = true;
        AplicacaoNode.PaginaCreditos.Visible = false;
        if (OS.GetName() == "HTML5")
        {
            GetNode<Label>("Panel/VBoxContainer/Label4").Visible = false;
        }
    }

    private void _on_BackButton_pressed()
    {
        GetNode<AnimationPlayer>("AnimMenu").Play("Closing");
    }

    private void _on_PaginaCalculoButton_pressed()
    {
        if (!AplicacaoNode.PaginaPrincipal.Visible)
        {
            AplicacaoNode.PaginaPrincipal.Visible = true;
            AplicacaoNode.PaginaCreditos.Visible = false;
            GetNode<AnimationPlayer>("AnimMenu").Play("Closing");
        }
    }

    private void _on_PaginaCreditosButton_pressed()
    {
        if (!AplicacaoNode.PaginaCreditos.Visible)
        {
            AplicacaoNode.PaginaCreditos.Visible = true;
            AplicacaoNode.PaginaPrincipal.Visible = false;
            GetNode<AnimationPlayer>("AnimMenu").Play("Closing");
        }
    }
    private void _on_SairButton_pressed()
    {
        GetTree().Quit();
    }


}
