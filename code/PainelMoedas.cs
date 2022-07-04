using Godot;
using System;



public class PainelMoedas : Control
{
    [Signal]
    delegate void QuantiaMoedaAtualizada();
    private Main MainNode;

    public void AtualizarQuantiaExibida()
    {
        EmitSignal("QuantiaMoedaAtualizada");
    }

}
