using Godot;
using System;



public class PainelMoedas : Control
{
    [Signal]
    delegate void QuantiaMoedaAtualizada();
    private Main MainNode;
    private Aplicacao AplicacaoNode;

    public override void _Ready()
    {
        AplicacaoNode = GetNode<Aplicacao>("/root/Aplicacao");
    }

    public void AtualizarQuantiaExibida()
    {
        EmitSignal("QuantiaMoedaAtualizada");
        GetNode<Label>("LabelTotal").Text = "Saldo: " + String.Format("{0:0.00}", AplicacaoNode.GetMainNode().SomaTodalInventario());
    }

}
