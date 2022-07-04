using Godot;
using System;

public class CalcularTroco : Control
{
    private WindowDialog ResultadoWindowDialog;
    private SpinBox ValorContaSpinBox;
    private SpinBox ValorPagoSpinBox;
    private Label TotalLabel;
    private Button OkButton;
    private double resultadoTroco;
    private int[] arrayQuantiaMoedasNescessarias = { 0, 0, 0, 0, 0, 0 };
    private Aplicacao aplicacao;

    public override void _Ready()
    {
        aplicacao = GetNode<Aplicacao>("/root/Aplicacao");
        ResultadoWindowDialog = GetNode<WindowDialog>("/root/Main/CanvasLayer/ResultadoWindowDialog");
        ValorContaSpinBox = GetNode<SpinBox>("VBoxContainer/ContaContainer/ValorConta");
        ValorPagoSpinBox = GetNode<SpinBox>("VBoxContainer/PagoContainer/ValorPago");
        TotalLabel = GetNode<Label>("/root/Main/CanvasLayer/ResultadoWindowDialog/VBoxContainer/TotalControl/TotalLabel");
        OkButton = GetNode<Button>("/root/Main/CanvasLayer/ResultadoWindowDialog/VBoxContainer/ButtonsMarginContainer/HBoxContainer/ButtonOk");
    }

    public void _on_CalcularButton_pressed()
    {
        resultadoTroco = Math.Round(ValorPagoSpinBox.Value - ValorContaSpinBox.Value, 2);
        DefinirMensagemCalculoTotal();
        SelecionarMoedas();
        ResultadoWindowDialog.PopupCentered();
    }

    private void DefinirMensagemCalculoTotal()
    {
        if (resultadoTroco > 0)
        {
            TotalLabel.Text = "Troco: R$" + (String.Format("{0,0:0.00}", resultadoTroco));
            OkButton.Icon = null;
        }
        else if (resultadoTroco < 0)
        {

            aplicacao.GetResultadoControlNode().ExibirMensagem("Pagamento insuficiente.\nFaltam R$: " + (String.Format("{0,0:0.00}", -resultadoTroco)));
            TotalLabel.Text = "";
            OkButton.Icon = ResourceLoader.Load("res://sprites/icons/alert.png") as Texture;
        }
        else
        {
            aplicacao.GetResultadoControlNode().ExibirMensagem("Valor exato.");
            TotalLabel.Text = "";
            OkButton.Icon = null;
        }
    }

    private void SelecionarMoedas()
    {
        double valorRestanteAuxiliar = resultadoTroco;
        arrayQuantiaMoedasNescessarias = new int[] { 0, 0, 0, 0, 0, 0 };

        double auxiliarDrouble;
        int auxiliarInt;
        for (int i = aplicacao.GetMainNode().GetInventario().Length - 1; i >= 0 && Math.Round(valorRestanteAuxiliar, 2) > 0; i--)
        {
            auxiliarDrouble = Math.Floor(valorRestanteAuxiliar / aplicacao.valorMoedas[i]);
            auxiliarInt = Math.Min((int)auxiliarDrouble, aplicacao.GetMainNode().GetInventario()[i]);

            valorRestanteAuxiliar = Math.Round(valorRestanteAuxiliar - auxiliarInt * aplicacao.valorMoedas[i], 2);
            arrayQuantiaMoedasNescessarias[i] += auxiliarInt;
        }
        if (Math.Round(valorRestanteAuxiliar, 2) > 0)
        {
            arrayQuantiaMoedasNescessarias = new int[] { 0, 0, 0, 0, 0, 0 };
            aplicacao.GetResultadoControlNode().ExibirMensagem("Moedas insuficientes.\nAbasteça e tente novamente.");
        }
    }

    public int[] GetArrayQuantiaMoedasNescessarias()
    {
        return arrayQuantiaMoedasNescessarias;
    }
}
