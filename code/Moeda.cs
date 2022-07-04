using Godot;
using System;


[Tool]
public class Moeda : Control
{
    [Export(PropertyHint.Range, "0,100,1,or_greater")]
    public int quantia = 0;

    private NodePath label_quantia_node_path = "PanelContainer/Container/MoedaTexture/LabelQuantia";

    private NodePath texture_node_path = "PanelContainer/Container/MoedaTexture";


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
        GetNode<Label>(label_quantia_node_path).Visible = eh_exibe_quantia;
        GetNode<Panel>("PanelContainer/Container/MoedaTexture/Panel").Visible = eh_exibe_quantia;
    }

    private Aplicacao aplicacao;

    public override void _Ready()
    {
        aplicacao = GetNode<Aplicacao>("/root/Aplicacao");
        aplicacao.GetPainelMoedasControl().Connect("QuantiaMoedaAtualizada", this as Moeda, "AtualizaQuantia");
        AtualizaQuantia();
        AtualizaTextura();
        AtualizaEhExibeQuantia();
    }

    public void AtualizaTextura()
    {
        GetNode<TextureRect>(texture_node_path).Texture = ResourceLoader.Load(
            "res://sprites/" + tipo_moeda.ToString().ToLower().Replace("_", "-")
            + "-" + lado_moeda.ToString().ToLower() + ".png") as Texture;
    }

    public void AtualizaQuantia()
    {
        quantia = aplicacao.GetMainNode().GetQuantiaValorMoeda(tipo_moeda);
        GetNode<Label>(label_quantia_node_path).Text = quantia.ToString();
    }
    public void AtualizaEhExibeQuantia()
    {
        GetNode<Label>(label_quantia_node_path).Visible = eh_exibe_quantia;
        GetNode<Panel>("PanelContainer/Container/MoedaTexture/Panel").Visible = eh_exibe_quantia;
    }

    public void _on_Button_pressed()
    {
        GetNode<TextureRect>(texture_node_path).RectPivotOffset = GetNode<TextureRect>(texture_node_path).RectSize / 2;
        switch (lado_moeda)
        {
            case Tipo.LADO_MOEDA.CARA:
                SetLadoMoeda(Tipo.LADO_MOEDA.COROA);
                break;
            case Tipo.LADO_MOEDA.COROA:
                SetLadoMoeda(Tipo.LADO_MOEDA.CARA);
                break;
        }
        aplicacao.GetMainNode().efeito_click.efeito_moeda_clique();
        GetNode<AnimationPlayer>("PanelContainer/Container/MoedaTexture/AnimationPlayer").Play("bounce");
        GetNode<AnimationPlayer>("PanelContainer/Container/MoedaTexture/AnimationPlayer").Seek(0);
    }
    public void centraliza_pivot()
    {
        GetNode<TextureRect>(texture_node_path).RectPivotOffset = GetNode<TextureRect>(texture_node_path).RectSize / 2;
    }

    public void play_coin_sound()
    {
        GetNode<AudioStreamPlayer>("AudioStreamPlayer").Seek(0);
        GetNode<AudioStreamPlayer>("AudioStreamPlayer").Playing = true;
    }
}
