using Godot;
using System;

public class ResultadoControl : VBoxContainer
{
    const string quantiaDaMoedaNodePath = "MarginContainer/ListaQuantiaMoedas/QuantiaDaMoeda"; //adicionar caractere ao fim;
    private int[] arrayQuantiaMoedasNescessariasParaRetirar = new int[6] { 0, 0, 0, 0, 0, 0 };
    private Aplicacao AplicacaoNode;


    public override void _Ready()
    {
        AplicacaoNode = GetNode<Aplicacao>("/root/Aplicacao");
    }

    public void _on_ButtonCancelar_pressed()
    {
        FecharPopup();
    }

    public void _on_ButtonOk_pressed()
    {
        AplicacaoNode.GetMainNode().RetirarMoedas(arrayQuantiaMoedasNescessariasParaRetirar);
        FecharPopup();
    }

    private void _on_ResultadoWindowDialog_about_to_show()
    {
        arrayQuantiaMoedasNescessariasParaRetirar = AplicacaoNode.GetCalculoTroco().GetArrayQuantiaMoedasNescessarias();
        for (int i = 0; i < arrayQuantiaMoedasNescessariasParaRetirar.Length; i++)
        {
            GetNode<QuantiaDaMoedaResultado>(quantiaDaMoedaNodePath + i).SetQuantiaExibida(arrayQuantiaMoedasNescessariasParaRetirar[i]);
        }
    }

    private void _on_ResultadoWindowDialog_popup_hide()
    {
        GetNode<Label>("MarginContainer/LabelInsuficiente").Visible = false;
    }

    public void ExibirMensagem(string newMensagem)
    {
        GetNode<Label>("MarginContainer/LabelInsuficiente").Visible = true;
        GetNode<Label>("MarginContainer/LabelInsuficiente").Text = newMensagem;
    }

    public void FecharPopup()
    {
        AplicacaoNode.GetResultadoWindowDialog().Hide();
        GetNode<Label>("MarginContainer/LabelInsuficiente").Visible = false;
    }
}
