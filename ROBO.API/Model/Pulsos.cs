using System.Globalization;

namespace ROBO.API.Model
{
    public class Pulsos
    {
        /// <summary>
        /// status atual do pulso selecionar uma das opções 
        /// 1 - Rotação de -90º 2 - Rotação de -45º 3 - Em Repouso 4 - Rotação de 45º 5 - Rotação de 90º 6 - Rotação de 135º 7 - Rotação de 180º
        /// </summary>
        public int sttPulso {  get; set; }

        /// <summary>
        /// Define o lado do pulso a ser alterado Direito ou esquerdo
        /// </summary>
        public string ladoPulso { get; set; }
        public string situacaoSttPulso {
            get
            {
                return sttPulso switch
                {
                    1 => "Rotação de -90º",
                    2 => "Rotação de -45º",
                    3 => "Em Repouso",
                    4 => "Rotação de 45º",
                    5 => "Rotação de 90º",
                    6 => "Rotação de 135º",
                    7 => "Rotação de 180º"
                };
            }

            private set
            {
                situacaoSttPulso = value;
            }
        }

        /// <summary>
        /// define o status inicial do pulso
        /// </summary>
        public Pulsos()
        {
            sttPulso = 3;
        }
    }
}
