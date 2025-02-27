using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.UI;

public class ControladorFase : MonoBehaviour
{
    internal float TempoRestante;
    public GameObject telaGanhou, telaPerdeuErrou, telaPerdeuTempo, telaPause;

    //Elementos gráficos presentes na barra superior do game
    public Image imagemTacaSelecionada;
    public Text textoTempoRestante, textoFaseAtual;

    //Vetores das imagens das garrafas e dos tipos de bebidas
    public Sprite[] bebidas;
    public string[] tipos;

    //váriaveis representando o objeto do personagem e da garrafa
    public GameObject personagem;
    public SpriteRenderer bebidaNaTela;

    //váriaveis de controle
    internal int faseAtual, numBebidaAtual;
    internal string nomeBebidaAtual, nomeTacaAtual;
    internal Vector3 posInicialPersonagem;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        posInicialPersonagem = personagem.transform.localPosition;
        TempoRestante = 60;
        faseAtual = 1;
        nomeTacaAtual = "";
        EscolherUmaBebida();

        imagemTacaSelecionada.sprite = null;

    }

    // Update is called once per frame
    void Update()
    {
        //Código para diminuir o tempo 
        TempoRestante -= Time.deltaTime;

        //Código para atualizar os textos da tela:
        textoTempoRestante.text = "Tempo restante:" + TempoRestante.ToString("00");
        textoFaseAtual.text = "Fase: " + faseAtual;

        //Verificar se o tempo acabou:
        if(TempoRestante <= 0)
        {
            telaPerdeuTempo.SetActive(true);
            Time.timeScale = 0;
            TempoRestante = 0;
        }



    }

    public void PegarTaca(GameObject taca)
    {
        imagemTacaSelecionada.sprite = taca.GetComponent<SpriteRenderer>().sprite;
        imagemTacaSelecionada.preserveAspect = true;
        nomeTacaAtual = taca.GetComponent<ControladorTacas>().tipo;
    }

    public void Comparar()
    {
        if (nomeTacaAtual == nomeBebidaAtual)
        {
            telaGanhou.SetActive(true);
            Time.timeScale = 0;
        }
        else if (nomeTacaAtual != "")
        {
            telaPerdeuErrou.SetActive(true);
            Time.timeScale = 0;
        }


    }

    public void EscolherUmaBebida()
    {
        int ValorAleatorio = (int)(Random.value * bebidas.Length);

        if (numBebidaAtual == ValorAleatorio)
            ValorAleatorio++;

        bebidaNaTela.sprite = bebidas[ValorAleatorio];
        nomeBebidaAtual = tipos[ValorAleatorio];
    }

    public void Pausar()
    {
        telaPause.SetActive(true);
        Time.timeScale = 0;
    }

    public void Despausar()
    {
        telaPause.SetActive(false);
        Time.timeScale = 1;
    }

    public void AvancarFase()
    {
        //Avanço a fase 
        faseAtual += 1;

        //Reposociono o personagem a posição inicial
        personagem.transform.localPosition = posInicialPersonagem;

        //Adiciono 10s ao tempo restante
        TempoRestante += 10;

        //"Tiro" a taça da mão do jogador e mando escolher nova bebida
        nomeTacaAtual = "";
        EscolherUmaBebida();
        imagemTacaSelecionada.sprite = null;

        //Desligo a "Tela Ganhou" e decongelo o tempo
        telaGanhou.SetActive(false);
        Time.timeScale = 1;

    }

    public void RecomecarFase()
    {
        
        faseAtual = 1;

        //Reposociono o personagem a posição inicial
        personagem.transform.localPosition = posInicialPersonagem;

        //Adiciono 10s ao tempo restante
        TempoRestante = 60;

        //"Tiro" a taça da mão do jogador e mando escolher nova bebida
        nomeTacaAtual = "";
        EscolherUmaBebida();
        imagemTacaSelecionada.sprite = null;

        
        telaPerdeuErrou.SetActive(false);
        telaPerdeuTempo.SetActive(false); //Essa linha é duplicada da de cima,mas adaptada
        Time.timeScale = 1;

    }

}
