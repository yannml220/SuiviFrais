using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Hosting;

using System.Threading;
using System.Threading.Tasks;
using Cronos;
using Microsoft.Extensions.Logging;

namespace SuiviFrais
{
    public class MiseEnPaiement : TaskScheduler
    {

        private readonly ILogger<MiseEnPaiement> _logger;
        public GestionnaireDates gestionnaire;



        public MiseEnPaiement(IScheduleConfig<MiseEnPaiement> config, ILogger<MiseEnPaiement> logger)
            : base(config.CronExpression, config.TimeZoneInfo)
        {
            this._logger = logger;
        }



        public override Task DoWork(CancellationToken cancellationToken)   //on override la fonction DoWork qui va servir de base pour effectuer la mise en paiement tous les 20 du mois
        {                                                                  //la fréquence d'activité de ce service de mise en paiement est défini par une expression de Cron 
            var annee = DateTime.Now.Year.ToString();
            var moisClot = GestionnaireDates.getMoisPrecedentFromNow();  //désigne le mois dont les fiches vont être mise en paiement ( le mois N-1)
            var dateMoisClot = annee + moisClot;
            MysqlDataAccess acces = new MysqlDataAccess();
            acces.UpdateDb("UPDATE fichefrais SET idetat = 'PA' WHERE  mois = " + dateMoisClot+" AND idetat = 'VA'");  //mise en paiement automatique des fiches validée concernant le mois passé en param (mois N-1) 

            _logger.LogInformation($"{DateTime.Now:hh:mm:ss} Mise en paiement is working." + Environment.NewLine);
            _logger.LogInformation("nouvelle fonction entre :" + GestionnaireDates.entre("1", "20") + Environment.NewLine);
            _logger.LogInformation(" la date de la mise en paiement :" + dateMoisClot  +Environment.NewLine);
            return Task.CompletedTask;
        }

    }
}
