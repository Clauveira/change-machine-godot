using Godot;
using System;

public class Main : Node
{
    private string filepath = "user://TrocoPro_save.data";
    private const int versaoArquivoSave = 1;
    private int[] inventarioMoedas = new int[6] { 0, 0, 0, 0, 0, 0 };
    public EfeitoClick efeitoClick;
    private Aplicacao AplicacaoNode;

    public override void _Ready()
    {
        AplicacaoNode = GetNode<Aplicacao>("/root/Aplicacao");
        efeitoClick = GetNode<EfeitoClick>("efeito_click");
        CarregarSave();
        AplicacaoNode.GetPainelMoedasControl().AtualizarQuantiaExibida();
    }
    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        if (@event.IsActionPressed("F1"))
        {
            GetNode<AnimationPlayer>("AnimTheme").Play("Default");
        }
        if (@event.IsActionPressed("F2"))
        {
            GetNode<AnimationPlayer>("AnimTheme").Play("Dark");
        }
    }
    public bool AbastecerMoedas(int[] arrayQuantidade)
    {
        if (arrayQuantidade.Length == inventarioMoedas.Length)
        {
            for (int i = 0; i < arrayQuantidade.Length; i++)
            {
                inventarioMoedas[i] += arrayQuantidade[i];
            }
            AplicacaoNode.GetPainelMoedasControl().AtualizarQuantiaExibida();
            Salvar();
            return true;
        }
        return false;
    }

    public bool RetirarMoedas(int[] arrayQuantidade)
    {
        if (arrayQuantidade.Length == inventarioMoedas.Length)
        {
            for (int i = 0; i < arrayQuantidade.Length; i++)
            {
                if (arrayQuantidade[i] > inventarioMoedas[i])
                {
                    return false;
                }
            }
            for (int i = 0; i < arrayQuantidade.Length; i++)
            {
                inventarioMoedas[i] -= arrayQuantidade[i];
            }
            AplicacaoNode.GetPainelMoedasControl().AtualizarQuantiaExibida();
            Salvar();
            return true;
        }
        return false;
    }

    public int GetQuantiaValorMoeda(Tipo.VALOR_MOEDA tipo)
    {
        return inventarioMoedas[((int)tipo)];
    }

    public int[] GetInventario()
    {
        return inventarioMoedas;
    }

    public void Salvar()
    {
        File file = new File();
        file.Open(filepath, File.ModeFlags.Write);
        //Inicio da sequencia de save
        file.Store32(Convert.ToUInt32(versaoArquivoSave));
        foreach (int item in inventarioMoedas)
        {
            file.Store32(Convert.ToUInt32(item));
        }
        //Final da sequencia de save
        file.Close();
    }
    private void CarregarSave()
    {
        int versao_arquivo_carregando;
        File file = new File();
        if (file.FileExists(filepath))
        {
            file.Open(filepath, File.ModeFlags.Read);
            //Inicio da sequencia de save
            versao_arquivo_carregando = Convert.ToInt32(file.Get32());
            for (int i = 0; i < inventarioMoedas.Length; i++)
            {
                inventarioMoedas[i] = Convert.ToInt32(file.Get32());
            }
            //Final da sequencia de save
            file.Close();
        }
        else
        {
            inventarioMoedas = new int[6] { 0, 0, 0, 0, 0, 0 };
        }

    }

    private void _on_Main_tree_exiting()
    {
        Salvar();
    }

    public float SomaTodalInventario()
    {
        float auxiliar = 0.0f;
        for (int i = 0; i < inventarioMoedas.Length; i++)
        {
            auxiliar += (float)(inventarioMoedas[i] * AplicacaoNode.valorMoedas[i]);
        }
        return auxiliar;
    }

}