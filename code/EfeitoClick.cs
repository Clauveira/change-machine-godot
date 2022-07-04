using Godot;
using System;

public class EfeitoClick : AnimatedSprite
{
    public void EfeitoMoedaClique()
    {
        Position = GetGlobalMousePosition();
        Frame = 0;
        Playing = true;
    }
}
