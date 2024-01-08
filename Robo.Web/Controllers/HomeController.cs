using Microsoft.AspNetCore.Mvc;
using Robo.Web.Models;
using Newtonsoft.Json;
using ROBO.API.Model;
using System.Diagnostics;
using System.Text;
using System;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Robo.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private const string endPointRobo = "https://localhost:7275/api/Robo";
        
        private HttpClient client = new HttpClient();
        private static bool corpoRoboCriado = false;
        private static Corpo corpo;


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
                var response = client.GetAsync(endPointRobo).Result;
                var resposta = response.Content.ReadAsStringAsync().Result;
                corpoRoboCriado = true;
                var novoRobo = JsonConvert.DeserializeObject<Corpo>(resposta);
                corpo = novoRobo;
                ViewBag.Corpo = corpo;
                return View(ViewBag.Robo);
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpPost, ActionName("MexerRotacao")]
        public IActionResult MexerRotacao(string button)
        {
            
            switch (button)
            {
                case "Direita":
                    corpo.Cabeca.rotacoesCabeca.sttRotacao++;
                    break;
                case "Esquerda":
                    corpo.Cabeca.rotacoesCabeca.sttRotacao--;
                    break;
                default:
                    return BadRequest("Status de botão inválido");
            }
            var json = JsonConvert.SerializeObject(corpo.Cabeca.rotacoesCabeca);
            EndPointDadosRoboApi(json, "MEXERCABECA");
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("MexerInclinacao")]
        public IActionResult MexerInclinacao(string button)
        {
            if (button == "Cima")
            {
                corpo.Cabeca.inclinacoesCabeca.sttInclinacao--;
            }
            else if (button == "Baixo")
            {
                corpo.Cabeca.inclinacoesCabeca.sttInclinacao++;
            }
            else return BadRequest("Status de botão inválido");

            var json = JsonConvert.SerializeObject(corpo.Cabeca.inclinacoesCabeca);
            EndPointDadosRoboApi(json, "INCLINARCABECA");
            return RedirectToAction(nameof(Index));
            
        }

        [HttpPost, ActionName("MexerCotovelo")]
        public IActionResult MexerCotovelo(string button)
        {
            string? json = null;

            if (button == "DescontrairCotoveloDireito")
            {
                corpo.BracoDireito.CotoveloBraco.sttCotovelo--;
                corpo.BracoDireito.CotoveloBraco.ladoCotovelo = "D";
                json = JsonConvert.SerializeObject(corpo.BracoDireito.CotoveloBraco);
                EndPointDadosRoboApi(json, "COTOVELO");
            }
            else if (button == "ContrairCotoveloDireito")
            {
                //quanto maior o status, maior a contração
                corpo.BracoDireito.CotoveloBraco.sttCotovelo++;
                corpo.BracoDireito.CotoveloBraco.ladoCotovelo = "D";
                json = JsonConvert.SerializeObject(corpo.BracoDireito.CotoveloBraco);
                EndPointDadosRoboApi(json,"COTOVELO");
            }
            else if (button == "DescontrairCotoveloEsquerdo")
            {
                corpo.BracoEsquerdo.CotoveloBraco.sttCotovelo--;
                corpo.BracoEsquerdo.CotoveloBraco.ladoCotovelo = "E";
                json = JsonConvert.SerializeObject(corpo.BracoEsquerdo.CotoveloBraco);
                EndPointDadosRoboApi(json, "COTOVELO");
            }
            else if (button == "ContrairCotoveloEsquerdo")
            {
                corpo.BracoEsquerdo.CotoveloBraco.sttCotovelo++;
                corpo.BracoEsquerdo.CotoveloBraco.ladoCotovelo = "E";
                json = JsonConvert.SerializeObject(corpo.BracoEsquerdo.CotoveloBraco);
                EndPointDadosRoboApi(json, "COTOVELO");

            }else return BadRequest("Status de botão inválido");

            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("MexerPulso")]
        public IActionResult MexerPulso(string button)
        {
            string? json = null;

            if (button == "MexerPulsoDDireita")
            {
                corpo.BracoDireito.PulsoBraco.sttPulso++;
                corpo.BracoDireito.PulsoBraco.ladoPulso = "D";
                json = JsonConvert.SerializeObject(corpo.BracoDireito.PulsoBraco);
                EndPointDadosRoboApi(json, "PULSO");
            }
            else if (button == "MexerPulsoDEsquerda")
            {
                corpo.BracoDireito.PulsoBraco.sttPulso--;
                corpo.BracoDireito.PulsoBraco.ladoPulso = "D";
                json = JsonConvert.SerializeObject(corpo.BracoDireito.PulsoBraco);
                EndPointDadosRoboApi(json, "PULSO");

            }
            else if (button == "MexerPulsoEDireita")
            {
                corpo.BracoDireito.PulsoBraco.sttPulso++;
                corpo.BracoDireito.PulsoBraco.ladoPulso = "E";
                json = JsonConvert.SerializeObject(corpo.BracoDireito.PulsoBraco);
                EndPointDadosRoboApi(json, "PULSO");

            }
            else if (button == "MexerPulsoEEsquerda")
            {
                corpo.BracoDireito.PulsoBraco.sttPulso--;
                corpo.BracoDireito.PulsoBraco.ladoPulso = "E";
                json = JsonConvert.SerializeObject(corpo.BracoDireito.PulsoBraco);
                EndPointDadosRoboApi(json, "PULSO");

            }
            else return BadRequest("Status de botão inválido");

            return RedirectToAction(nameof(Index));
        }

        private void EndPointDadosRoboApi(string json, string tipoEndPoint)
        {
            try
            {
                string endpoint = "";
                switch (tipoEndPoint)
                {
                    case "ROBO":
                        endpoint = "https://localhost:7275/api/Robo";
                        break;
                    case "MEXERCABECA":
                        endpoint = "https://localhost:7275/api/Robo/Cabeca/Mexer";
                        break;
                    case "INCLINARCABECA":
                        endpoint = "https://localhost:7275/api/Robo/Cabeca/Inclinar";
                        break;
                    case "COTOVELO":
                        endpoint = "https://localhost:7275/api/Robo/Cotovelo/Mexer";
                        break;
                    case "PULSO":
                        endpoint = "https://localhost:7275/api/Robo/Pulso/Mexer";
                        break;

                }

                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                var result = client.PostAsync(endpoint, httpContent).Result;
                var response = result.Content.ReadAsStringAsync().Result;
                var RoboResposta = JsonConvert.DeserializeObject<Corpo>(response);
                corpo = RoboResposta;
                ViewBag.Corpo = corpo;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
