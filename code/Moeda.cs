using Godot;
using System;


[Tool]
public class Moeda : Control
{
    public enum TIPO_MOEDA{
        UM_CENTAVO,
        CINCO_CENTAVOS,
        DEZ_CENTAVOS,
        VINTE_E_CINCO_CENTAVOS,
        CINQUENTA_CENTAVOS,
        UM_REAL,
        /*
        DOIS_REAIS,
        CINCO_REAIS,
        DEZ_REAIS,
        VINTE_REAIS,
        CINQUENTA_REAIS,
        CEM_REAIS,
        DUZENTOS_REAIS,
        */
    }

    public enum LADO_MOEDA{
            CARA,
            COROA,
    }

    
    [Export(PropertyHint.Range, "0,100,1,or_greater")]
    public int quantia = 0;
    
    private TIPO_MOEDA tipo_moeda = TIPO_MOEDA.UM_REAL;
    [Export]
    public TIPO_MOEDA TipoMoeda {
        get => tipo_moeda;
        set => SetTipoMoeda(value);
    }
    private void SetTipoMoeda(TIPO_MOEDA new_tipo_moeda)
    {
        tipo_moeda = new_tipo_moeda;
        atualiza_textura();
    }

    private LADO_MOEDA lado_moeda = LADO_MOEDA.COROA;
    [Export]
    public LADO_MOEDA LadoMoeda{
        get => lado_moeda;
        set => SetLadoMoeda(value);
    }
    private void SetLadoMoeda(LADO_MOEDA new_lado_moeda)
    {
        lado_moeda = new_lado_moeda;
        atualiza_textura();
    }

    public override void _Ready()
    {
        GetNode<Label>("PanelContainer/VBoxContainer/LabelQuantia").Text = quantia.ToString();
        atualiza_textura();
        
    }

    public void atualiza_textura(){
        GetNode<TextureRect>("PanelContainer/VBoxContainer/MoedaTexture").Texture = ResourceLoader.Load(
            "res://sprites/" + tipo_moeda.ToString().ToLower().Replace("_", "-")
            + "-" + lado_moeda.ToString().ToLower() + ".png") as Texture;

    }
}
