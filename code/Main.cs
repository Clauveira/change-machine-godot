using Godot;
using System;

public class Main : Node
{
    private const int versaoArquivoSave = 1;
    private int[] inventarioMoedas = new int[6];
    public EfeitoClick efeitoClick;
    private Aplicacao aplicacao;
    private string filepath = "user://Magnetic_Projectiles_save.data";

    public override void _Ready()
    {
        aplicacao = GetNode<Aplicacao>("/root/Aplicacao");
        efeitoClick = GetNode<EfeitoClick>("efeito_click");
        CarregarSave();
        aplicacao.GetPainelMoedasControl().AtualizarQuantiaExibida();
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
            aplicacao.GetPainelMoedasControl().AtualizarQuantiaExibida();
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
            aplicacao.GetPainelMoedasControl().AtualizarQuantiaExibida();
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

    private void Salvar()
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
        GD.Print("Salvo");
        file.Close();
    }
    private void CarregarSave()
    {
        int versao_arquivo_carregando;
        File file = new File();
        if (!file.FileExists(filepath))
        {
            GD.Print("Não há arquivo salvo");
            return;
        }

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

    private void _on_Main_tree_exiting()
    {
        Salvar();
    }

}