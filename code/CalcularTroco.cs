using Godot;
using System;

public class CalcularTroco : Control
{
    private WindowDialog ResultadoWindowDialog;
    private SpinBox ValorContaSpinBox;
    private SpinBox ValorPagoSpinBox;
    private Label TotalLabel;
    private double resultado_troco;


    public override void _Ready()
    {
        ResultadoWindowDialog = GetNode<WindowDialog>("/root/Main/CanvasLayer/ResultadoWindowDialog");
        ValorContaSpinBox = GetNode<SpinBox>("VBoxContainer/ContaContainer/ValorConta");
        ValorPagoSpinBox = GetNode<SpinBox>("VBoxContainer/PagoContainer/ValorPago");
        TotalLabel = GetNode<Label>("/root/Main/CanvasLayer/ResultadoWindowDialog/VBoxContainer/TotalControl/TotalLabel");
    }

    public void _on_CalcularButton_pressed(){
        ResultadoWindowDialog.PopupCentered();
        resultado_troco = ValorPagoSpinBox.Value - ValorContaSpinBox.Value;
        
        if (resultado_troco > 0){
            TotalLabel.Text = "Troco: R$" + (String.Format("{0,0:0.00}", resultado_troco));
        }
        else if (resultado_troco < 0){
            TotalLabel.Text = "Pagamento insuficiente, faltam R$: " + (String.Format("{0,0:0.00}", -resultado_troco));
        }
        else {
            TotalLabel.Text = "Valor exato.";
        }
    }
}
