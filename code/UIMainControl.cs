using Godot;
using System;

public class UIMainControl : Node
{
    DynamicFont fonteMedium;
    private float minWindowSize;
    private WindowDialog AbastecimentoSangriaWindow;
    private WindowDialog ResultadoWindow;
    private Aplicacao AplicacaoNode;

    public override void _Ready()
    {
        AplicacaoNode = GetNode<Aplicacao>("/root/Aplicacao");
        AbastecimentoSangriaWindow = AplicacaoNode.GetAbastecimentoSangriaWindowDialog();
        ResultadoWindow = AplicacaoNode.GetResultadoWindowDialog();
        fonteMedium = ResourceLoader.Load("res://theme/fonts/dynamic_font_medium.tres") as DynamicFont;
    }

    public int CalculaTamanhoFonte()
    {
        return Mathf.Clamp(
            Convert.ToInt32(minWindowSize * 0.036),
            12, 30);
    }

    public void _on_MainControl_resized()
    {
        minWindowSize = Math.Min(OS.WindowSize.x, OS.WindowSize.y * 0.8f);
        fonteMedium.Size = CalculaTamanhoFonte();
        AbastecimentoSangriaWindow.SetSize(new Vector2(minWindowSize * 0.8f, minWindowSize * 0.9f));

        float auxiliarMobileY;
        if (AplicacaoNode.eh_mobile)
        {
            auxiliarMobileY = minWindowSize * 0.2f;
        }
        else
        {
            auxiliarMobileY = OS.WindowSize.y * 0.5f - AbastecimentoSangriaWindow.RectSize.y * 0.5f;
        }

        AbastecimentoSangriaWindow.SetPosition(new Vector2(
            OS.WindowSize.x * 0.5f - AbastecimentoSangriaWindow.RectSize.x * 0.5f,
            auxiliarMobileY));

        ResultadoWindow.SetSize(new Vector2(minWindowSize * 0.8f, minWindowSize * 0.9f));
        //ResultadoWindow.PopupCentered();
        ResultadoWindow.SetPosition(new Vector2(
            OS.WindowSize.x * 0.5f - ResultadoWindow.RectSize.x * 0.5f,
            OS.WindowSize.y * 0.5f - ResultadoWindow.RectSize.y * 0.5f))
            ;
    }
}
