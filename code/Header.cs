using Godot;
using System;

public class Header : Control
{
    private Aplicacao AplicacaoNode;

    public override void _Ready()
    {
        AplicacaoNode = GetNode<Aplicacao>("/root/Aplicacao");

    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      CanvasLayer/MainControl/MenuLateralControl/AnimMenu
    //  }

    private void _on_IconeMenu_pressed()
    {
        GetNode<AnimationPlayer>("/root/Main/CanvasLayer/MainControl/MenuLateralControl/AnimMenu").Play("Oppening");
    }
}
