using Godot;
using System;



public class PainelMoedas : Control
{
    [Signal]
    delegate void QuantiaMoedaAtualizada();

    private Main MainNode;
    public override void _Ready()
    {

    }

    public void atualizar_quantia_exibida()
    {
        EmitSignal("QuantiaMoedaAtualizada");
    }

}
