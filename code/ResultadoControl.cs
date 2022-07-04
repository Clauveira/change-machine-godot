using Godot;
using System;

public class ResultadoControl : VBoxContainer
{
    const string quantiaDaMoedaNodePath = "MarginContainer/ListaQuantiaMoedas/QuantiaDaMoeda"; //adicionar caractere ao fim;
    private int[] arrayQuantiaMoedasNescessariasRecebidas;
    private Aplicacao aplicacao;


    public override void _Ready()
    {
        aplicacao = GetNode<Aplicacao>("/root/Aplicacao");
    }

    public void _on_ButtonCancelar_pressed()
    {
        FecharPopup();
    }

    public void _on_ButtonOk_pressed()
    {
        aplicacao.GetMainNode().RetirarMoedas(arrayQuantiaMoedasNescessariasRecebidas);
        FecharPopup();
    }

    private void _on_ResultadoWindowDialog_about_to_show()
    {
        arrayQuantiaMoedasNescessariasRecebidas = aplicacao.GetCalculoTroco().GetArrayQuantiaMoedasNescessarias();
        for (int i = 0; i < arrayQuantiaMoedasNescessariasRecebidas.Length; i++)
        {
            GetNode<QuantiaDaMoedaResultado>(quantiaDaMoedaNodePath + i).SetQuantiaExibida(arrayQuantiaMoedasNescessariasRecebidas[i]);
        }
    }

    public void ExibirMensagem(string newMensagem)
    {
        GetNode<Label>("MarginContainer/LabelInsuficiente").Visible = true;
        GetNode<Label>("MarginContainer/LabelInsuficiente").Text = newMensagem;
    }

    public void FecharPopup()
    {
        aplicacao.GetResultadoWindowDialog().Hide();
        GetNode<Label>("MarginContainer/LabelInsuficiente").Visible = false;
    }
}
