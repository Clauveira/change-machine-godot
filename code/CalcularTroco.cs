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
        double valor_restante_auxiliar = resultadoTroco;
        arrayQuantiaMoedasNescessarias = new int[] { 0, 0, 0, 0, 0, 0 };
        GD.Print(resultadoTroco);

        int resultado_busca_auxiliar = 0;
        while (valor_restante_auxiliar > 0)
        {
            resultado_busca_auxiliar = BuscarMelhorMoeda(valor_restante_auxiliar);
            if (resultado_busca_auxiliar == -1)
            {
                GD.Print("SEM MOEDAS" + aplicacao.GetMainNode().GetInventario()[0]);

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
        GD.Print("resultado_busca_auxiliar fim: " + resultado_busca_auxiliar);
        GD.Print(valor_restante_auxiliar + " | " + arrayQuantiaMoedasNescessarias[0] + " _ " + arrayQuantiaMoedasNescessarias[1] + " _ " + arrayQuantiaMoedasNescessarias[2] + " _ " + arrayQuantiaMoedasNescessarias[3] + " _ " + arrayQuantiaMoedasNescessarias[4] + " _ " + arrayQuantiaMoedasNescessarias[5]);
    }

    private int BuscarMelhorMoeda(double valor_restante)
    {
        GD.Print("restante: " + valor_restante);
        if (valor_restante >= 1.0 && aplicacao.GetMainNode().GetInventario()[5] - arrayQuantiaMoedasNescessarias[5] > 0)
        {
            return 5;
        }
        else if (valor_restante >= 0.5 && aplicacao.GetMainNode().GetInventario()[4] - arrayQuantiaMoedasNescessarias[4] > 0)
        {
            return 4;
        }
        else if (valor_restante >= 0.25 && aplicacao.GetMainNode().GetInventario()[3] - arrayQuantiaMoedasNescessarias[3] > 0)
        {
            return 3;
        }
        else if (valor_restante >= 0.10 && aplicacao.GetMainNode().GetInventario()[2] - arrayQuantiaMoedasNescessarias[2] > 0)
        {
            return 2;
        }
        else if (valor_restante >= 0.05 && aplicacao.GetMainNode().GetInventario()[1] - arrayQuantiaMoedasNescessarias[1] > 0)
        {
            return 1;
        }
        else if (valor_restante >= 0.01 && aplicacao.GetMainNode().GetInventario()[0] - arrayQuantiaMoedasNescessarias[0] > 0)
        {
            return 0;
        }
        else
        {
            return -1;
        }
    }

    public int[] GetArrayQuantiaMoedasNescessarias()
    {
        return arrayQuantiaMoedasNescessarias;
    }
}
