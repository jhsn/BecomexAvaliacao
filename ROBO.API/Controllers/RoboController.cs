using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ROBO.API.Model;
using ROBO.API.BLL;


namespace ROBO.API.Controllers
{
    [ApiController]
    [Route("api/Robo")]
    [Produces("application/json")]
    public class RoboController : ControllerBase
    {
        private static Corpo? corpo;

        Corpo CorpoRobo
        {
            get
            {
                if (corpo == null)
                {
                    corpo = new Corpo();
                }
                return corpo;
            }
            set { corpo = value; }
        }

        [HttpGet]
        public IActionResult GetEstadoRobo()
        {
            try
            {
                return Ok(CorpoRobo);
            }
            catch (Exception)
            {

                return BadRequest("Erro ao Inicializar o Robo, tente novemente!");
            }
        }
        public ValidarAcoes validaAcao = new ValidarAcoes();


        [HttpPost]
        [Route("Cabeca/Mexer")]
        public IActionResult MexerCabeca(Rotacoes rotacao)
        {
            try {

                bool retornoValidacao;
                bool inclinadoParaBaixo;

                if (rotacao.sttRotacao < 1 || rotacao.sttRotacao > 5)
                    throw new Exception("Status não é valido para rotacionar a cabeça, ele deve ser entre 1 e 5!");
                
                if (rotacao.sttRotacao == CorpoRobo.Cabeca.rotacoesCabeca.sttRotacao)
                    throw new Exception("Você está enviando o mesmo status para mexer a cabeça do robô.");


                inclinadoParaBaixo = validaAcao.ValidarInclinacaoCabecaBaixo(CorpoRobo.Cabeca.inclinacoesCabeca.sttInclinacao);
                if (inclinadoParaBaixo == true)
                    throw new Exception("A cabeça do robô não pode esta para baixo para poder mexer a cabeça.");


                retornoValidacao = validaAcao.ValidarMovimentos(CorpoRobo.Cabeca.rotacoesCabeca.sttRotacao, rotacao.sttRotacao);
                if (retornoValidacao)
                {
                    CorpoRobo.Cabeca.rotacoesCabeca.sttRotacao = rotacao.sttRotacao   ;
                }
                else 
                    throw new Exception($"Você esta pulando um status para mexer a cabeça! Status enviado : {rotacao.sttRotacao} . Atual: {CorpoRobo.Cabeca.rotacoesCabeca.sttRotacao}");

                return Ok(rotacao);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("Cabeca/Inclinar")]
        public IActionResult InclinarCabeca(Inclinacoes inclinacao)
        {
            try
            {
                bool retornoValidacao;

                if (inclinacao.sttInclinacao < 1 || inclinacao.sttInclinacao > 3)
                    throw new Exception("Status não é valido para inclinar a cabeça, ele deve ser entre 1 e 3!");

                if (inclinacao.sttInclinacao == CorpoRobo.Cabeca.inclinacoesCabeca.sttInclinacao)
                    throw new Exception("Você está enviando o mesmo status para inclinar a cabeça do robô.");

                retornoValidacao = validaAcao.ValidarMovimentos(CorpoRobo.Cabeca.inclinacoesCabeca.sttInclinacao, inclinacao.sttInclinacao);
                
                if (retornoValidacao)
                {
                    CorpoRobo.Cabeca.inclinacoesCabeca.sttInclinacao = inclinacao.sttInclinacao;

                }
                else 
                    throw new Exception($"Você esta pulando um status da inclinação! Status enviado :{inclinacao.sttInclinacao}. Atual: {CorpoRobo.Cabeca.inclinacoesCabeca.sttInclinacao}");

                return Ok(CorpoRobo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [Route("Cotovelo/Mexer")]
        public IActionResult MexerCotovelos(Cotovelos cotovelo){
            
            try {

                bool retornoValidacao;

                if (cotovelo.sttCotovelo < 1 || cotovelo.sttCotovelo > 4)
                    throw new Exception("Status não é valido para o Cotovelo, ele deve ser entre 1 e 4!");

                if (cotovelo.ladoCotovelo == "D")
                {
                    if (cotovelo.sttCotovelo == CorpoRobo.BracoDireito.CotoveloBraco.sttCotovelo)
                        throw new Exception("Você está enviando o mesmo status do cotovelo direito para o robô.");

                    retornoValidacao = validaAcao.ValidarMovimentos(CorpoRobo.BracoDireito.CotoveloBraco.sttCotovelo, cotovelo.sttCotovelo);

                    if (retornoValidacao)
                    {
                        CorpoRobo.BracoDireito.CotoveloBraco.sttCotovelo = cotovelo.sttCotovelo;
                        CorpoRobo.BracoDireito.CotoveloBraco.ladoCotovelo = cotovelo.ladoCotovelo;

                    }
                    else 
                        throw new Exception($"Você esta pulando um status para mexer o cotovelo direito! Status enviado : {cotovelo.sttCotovelo} - Atual: {CorpoRobo.BracoDireito.CotoveloBraco.sttCotovelo}");

                }
                else if (cotovelo.ladoCotovelo == "E")
                {

                    if (cotovelo.sttCotovelo == CorpoRobo.BracoEsquerdo.CotoveloBraco.sttCotovelo)
                        throw new Exception("Você está enviando o mesmo status do cotovelo esquerdo para o robô.");

                    retornoValidacao = validaAcao.ValidarMovimentos(CorpoRobo.BracoEsquerdo.CotoveloBraco.sttCotovelo, cotovelo.sttCotovelo);
                    if (retornoValidacao)
                    {
                        CorpoRobo.BracoEsquerdo.CotoveloBraco.sttCotovelo = cotovelo.sttCotovelo;
                        CorpoRobo.BracoEsquerdo.CotoveloBraco.ladoCotovelo = cotovelo.ladoCotovelo;

                    }
                    else 
                        throw new Exception($"Você esta pulando um status! Status enviado : {cotovelo.sttCotovelo} - Atual: {CorpoRobo.BracoDireito.CotoveloBraco.sttCotovelo}");

                }
                else
                {
                    throw new Exception("Você deve excolher um dos cotovelos para movimenta-lo Direito(D) ou Esquerdo(E)!");
                }

                return Ok(CorpoRobo);

            }
            catch (Exception ex) { 
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Pulso/Mexer")]
        public IActionResult MexerPulsos(Pulsos pulso)
        {
            try
            {
                bool retornoValidacao;
                bool cotoveloFortementeContraido;

                if (pulso.sttPulso < 1 || pulso.sttPulso > 7)
                    throw new Exception("Status não é valido para o Pulso, ele deve ser entre 1 e 7!");

                if(pulso.ladoPulso == "D")
                {
                    if (pulso.sttPulso == CorpoRobo.BracoDireito.PulsoBraco.sttPulso)
                        throw new Exception("Você está enviando o mesmo status de pulso direito para o robô.");

                    cotoveloFortementeContraido = validaAcao.ValidarCotoveloContraido(CorpoRobo.BracoDireito.CotoveloBraco.sttCotovelo);
                    if (cotoveloFortementeContraido == false)
                        throw new Exception($"Para movimentar o pulso direito o cotovelo direito deve estar fortemente contraído. Atualmente está {CorpoRobo.BracoDireito.CotoveloBraco.situacaoSttCotovelo}");

                    retornoValidacao = validaAcao.ValidarMovimentos(CorpoRobo.BracoDireito.PulsoBraco.sttPulso, pulso.sttPulso);
                    
                    if (retornoValidacao)
                    {
                        CorpoRobo.BracoDireito.PulsoBraco.sttPulso = pulso.sttPulso;
                        CorpoRobo.BracoDireito.PulsoBraco.ladoPulso = pulso.ladoPulso;
                        
                    }
                    else 
                        throw new Exception($"Você esta pulando um status do pulso direito! Status enviado : {pulso.sttPulso} - Atual: {CorpoRobo.BracoDireito.PulsoBraco.sttPulso}");

                }
                else if(pulso.ladoPulso == "E")
                {
                    if (pulso.sttPulso == CorpoRobo.BracoEsquerdo.PulsoBraco.sttPulso)
                        throw new Exception("Você está enviando o mesmo status de pulso esquerdo para o robô.");

                    cotoveloFortementeContraido = validaAcao.ValidarCotoveloContraido(CorpoRobo.BracoEsquerdo.CotoveloBraco.sttCotovelo);
                   
                    if (cotoveloFortementeContraido == false)
                        throw new Exception($"Para movimentar o pulso esquerdo o cotovelo esquerdo deve estar fortemente contraído. Atualmente está {CorpoRobo.BracoEsquerdo.CotoveloBraco.situacaoSttCotovelo}");

                    retornoValidacao = validaAcao.ValidarMovimentos(CorpoRobo.BracoEsquerdo.PulsoBraco.sttPulso, pulso.sttPulso);
                    if (retornoValidacao)
                    {
                        CorpoRobo.BracoEsquerdo.PulsoBraco.sttPulso = pulso.sttPulso;
                        CorpoRobo.BracoEsquerdo.PulsoBraco.ladoPulso = pulso.ladoPulso;

                    }
                    else throw new Exception($"Você esta pulando um status do pulso esquerdo! Status enviado : {pulso.sttPulso} - Atual: {CorpoRobo.BracoEsquerdo.PulsoBraco.sttPulso}");
                }
                else
                {
                    throw new Exception("Você deve excolher um dos pulso para movimenta-lo Direito(D) ou Esquerdo(E)!");
                }


                return Ok(CorpoRobo);

            } 
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
