using Godot;
using System;

public class Aplicacao : Node
{
    private Main MainNode;
    private WindowDialog AbastecimentoSangriaWindowDialog;
    private PainelMoedas PainelMoedasControl;
    private WindowDialog ResultadoWindowDialog;
    private ResultadoControl ResultadoControlNode;
    private CalcularTroco CalculoTroco;
    public double[] valorMoedas = new double[] { 0.01, 0.05, 0.10, 0.25, 0.50, 1.00 };

    public override void _Ready()
    {
        MainNode = GetNode<Main>("/root/Main");
        AbastecimentoSangriaWindowDialog = GetNode<WindowDialog>("/root/Main/CanvasLayer/AbastecimentoWindowDialog");
        PainelMoedasControl = GetNode<PainelMoedas>("/root/Main/CanvasLayer/MainControl/VBoxContainer/Body/MarginContainer/ScrollContainer/VBoxContainer/PainelMoedas");
        ResultadoWindowDialog = GetNode<WindowDialog>("/root/Main/CanvasLayer/ResultadoWindowDialog");
        ResultadoControlNode = GetNode<ResultadoControl>("/root/Main/CanvasLayer/ResultadoWindowDialog/VBoxContainer");
        CalculoTroco = GetNode<CalcularTroco>("/root/Main/CanvasLayer/MainControl/VBoxContainer/Body/MarginContainer/ScrollContainer/VBoxContainer/PainelAcoes/MarginContainer/VBoxContainer/Calculo");
    }

    public Main GetMainNode()
    {
        return MainNode;
    }
    public WindowDialog GetAbastecimentoSangriaWindowDialog()
    {
        return AbastecimentoSangriaWindowDialog;
    }
    public PainelMoedas GetPainelMoedasControl()
    {
        return PainelMoedasControl;
    }
    public WindowDialog GetResultadoWindowDialog()
    {
        return ResultadoWindowDialog;
    }
    public ResultadoControl GetResultadoControlNode()
    {
        return ResultadoControlNode;
    }
    public CalcularTroco GetCalculoTroco()
    {
        return CalculoTroco;
    }
}
