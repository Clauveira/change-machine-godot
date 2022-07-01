using Godot;
using System;

public class CalcularTroco : Control
{
    private WindowDialog ResultadoWindowDialog;
    private SpinBox ValorContaSpinBox;
    private SpinBox ValorPagoSpinBox;
    private Label TotalLabel;


    public override void _Ready()
    {
        ResultadoWindowDialog = GetNode<WindowDialog>("/root/Main/CanvasLayer/ResultadoWindowDialog");
        ValorContaSpinBox = GetNode<SpinBox>("VBoxContainer/ContaContainer/ValorConta");
        ValorPagoSpinBox = GetNode<SpinBox>("VBoxContainer/PagoContainer/ValorPago");
        TotalLabel = GetNode<Label>("/root/Main/CanvasLayer/ResultadoWindowDialog/VBoxContainer/TotalControl/TotalLabel");
    }

    public void _on_CalcularButton_pressed(){
        ResultadoWindowDialog.PopupCentered();
        GD.Print(ValorPagoSpinBox.Value - ValorContaSpinBox.Value);
        TotalLabel.Text = "Total: " + (ValorPagoSpinBox.Value - ValorContaSpinBox.Value);
    }
}
