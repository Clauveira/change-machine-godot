using Godot;
using System;

public class Main : Node
{
    public WindowDialog ResultadoWindowDialog;

    public override void _Ready()
    {
        ResultadoWindowDialog = GetNode<WindowDialog>("CanvasLayer/ResultadoWindowDialog");
        GD.Print(ResultadoWindowDialog);
    }
}