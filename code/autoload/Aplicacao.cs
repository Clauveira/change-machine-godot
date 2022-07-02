using Godot;
using System;

public class Aplicacao : Node
{
    private Main MainNode;
    private WindowDialog AbastecimentoSangriaWindowDialog;
    private PainelMoedas PainelMoedasControl;
    private WindowDialog ResultadoWindowDialog;

    public override void _Ready()
    {
        MainNode = GetNode<Main>("/root/Main");
        AbastecimentoSangriaWindowDialog = GetNode<WindowDialog>("/root/Main/CanvasLayer/AbastecimentoWindowDialog");
        PainelMoedasControl = GetNode<PainelMoedas>("/root/Main/CanvasLayer/MainControl/VBoxContainer/Body/MarginContainer/ScrollContainer/VBoxContainer/PainelMoedas");
        ResultadoWindowDialog = GetNode<WindowDialog>("/root/Main/CanvasLayer/ResultadoWindowDialog");
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
}
