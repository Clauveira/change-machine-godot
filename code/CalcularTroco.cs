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
        int resultado_busca_auxiliar = 0;
        double valor_restante_auxiliar = resultadoTroco;
        arrayQuantiaMoedasNescessarias = new int[] { 0, 0, 0, 0, 0, 0 };

        while (valor_restante_auxiliar > 0)
        {
            resultado_busca_auxiliar = BuscarMelhorMoeda(valor_restante_auxiliar);
            if (resultado_busca_auxiliar == -1)
            {
                arrayQuantiaMoedasNescessarias = new int[] { 0, 0, 0, 0, 0, 0 };
                aplicacao.GetResultadoControlNode().ExibirMensagem("Moedas insuficientes.\nAbasteça e tente novamente.");
                break;
            }
            else
            {
                valor_restante_auxiliar = Math.Round(valor_restante_auxiliar - aplicacao.valorMoedas[resultado_busca_auxiliar], 2);
                arrayQuantiaMoedasNescessarias[resultado_busca_auxiliar] += 1;
            }
        }
    }

    private int BuscarMelhorMoeda(double valor_restante)
    {
        for (int i = aplicacao.GetMainNode().GetInventario().Length - 1; i >= 0; i--)
        {
            if (valor_restante >= aplicacao.valorMoedas[i] && aplicacao.GetMainNode().GetInventario()[i] - arrayQuantiaMoedasNescessarias[i] > 0)
            {
                return i;
            }
        }
        return -1;
    }

    public int[] GetArrayQuantiaMoedasNescessarias()
    {
        return arrayQuantiaMoedasNescessarias;
    }
}
