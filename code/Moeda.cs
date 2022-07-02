using Godot;
using System;


[Tool]
public class Moeda : Control
{
    [Export(PropertyHint.Range, "0,100,1,or_greater")]
    public int quantia = 0;

    private Tipo.VALOR_MOEDA tipo_moeda = Tipo.VALOR_MOEDA.UM_REAL;
    [Export]
    public Tipo.VALOR_MOEDA TipoMoeda
    {
        get => tipo_moeda;
        set => SetTipoMoeda(value);
    }
    private void SetTipoMoeda(Tipo.VALOR_MOEDA new_tipo_moeda)
    {
        tipo_moeda = new_tipo_moeda;
        AtualizaTextura();
    }

    private Tipo.LADO_MOEDA lado_moeda = Tipo.LADO_MOEDA.COROA;
    [Export]
    public Tipo.LADO_MOEDA LadoMoeda
    {
        get => lado_moeda;
        set => SetLadoMoeda(value);
    }
    private void SetLadoMoeda(Tipo.LADO_MOEDA new_lado_moeda)
    {
        lado_moeda = new_lado_moeda;
        AtualizaTextura();
    }

    private bool eh_exibe_quantia = false;
    [Export]
    public bool EhExibeQuantia
    {
        get => eh_exibe_quantia;
        set => SetEhExibeQuantia(value);
    }
    private void SetEhExibeQuantia(bool new_eh_exibe_quantia)
    {
        eh_exibe_quantia = new_eh_exibe_quantia;
        GetNode<Label>("PanelContainer/VBoxContainer/LabelQuantia").Visible = eh_exibe_quantia;
    }

    private Aplicacao aplicacao;

    public override void _Ready()
    {
        aplicacao = GetNode<Aplicacao>("/root/Aplicacao");
        aplicacao.GetPainelMoedasControl().Connect("QuantiaMoedaAtualizada", this as Moeda, "AtualizaQuantia");
        AtualizaQuantia();
        AtualizaTextura();
    }

    public void AtualizaTextura()
    {
        GetNode<TextureRect>("PanelContainer/VBoxContainer/MoedaTexture").Texture = ResourceLoader.Load(
            "res://sprites/" + tipo_moeda.ToString().ToLower().Replace("_", "-")
            + "-" + lado_moeda.ToString().ToLower() + ".png") as Texture;
    }

    public void AtualizaQuantia()
    {
        quantia = aplicacao.GetMainNode().GetQuantiaValorMoeda(tipo_moeda);
        GetNode<Label>("PanelContainer/VBoxContainer/LabelQuantia").Text = quantia.ToString();
    }
}
