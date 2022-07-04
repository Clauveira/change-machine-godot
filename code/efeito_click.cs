using Godot;
using System;

public class efeito_click : AnimatedSprite
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
    }

    public void efeito_moeda_clique()
    {
        Position = GetGlobalMousePosition();
        Frame = 0;
        Playing = true;
    }
}
