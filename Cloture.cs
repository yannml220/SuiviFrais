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
    public class Cloture : TaskScheduler
    {

        private readonly ILogger<Cloture> _logger;
        public GestionnaireDates gestionnaire;
       

            
        public Cloture(IScheduleConfig<Cloture> config, ILogger<Cloture> logger)
            : base(config.CronExpression, config.TimeZoneInfo)
        {
            this._logger = logger;
        }



        public override Task DoWork(CancellationToken cancellationToken)   //on override la fonction DoWork qui va servir de base pour effectuer la cloture  tous les 1er du mois
        {                                                                  //la fréquence d'activité de ce service de cloture est défini par une expression de Cron 
            var annee = DateTime.Now.Year.ToString();
            var moisClot = GestionnaireDates.getMoisPrecedentFromNow();  //désigne le mois à cloturer 
            var dateMoisClot = annee+moisClot ;                           //sous la forme aaaamm
            MysqlDataAccess acces = new MysqlDataAccess();
            acces.UpdateDb("UPDATE fichefrais SET idetat = 'CL' WHERE  mois = "+dateMoisClot );  //cloture des fichefrais pour le mois passé en param (mois précédent le mois actuel) 
            //acces.showConnectionString();                  pour tester la validité de la connection string chargée à partir de appsettings.json
            _logger.LogInformation($"{DateTime.Now:hh:mm:ss} Cloture is working." +Environment.NewLine);
            _logger.LogInformation("nouvelle fonction entre :" + GestionnaireDates.entre("1","20") +Environment.NewLine);
            _logger.LogInformation(" la date de la cloture :" + dateMoisClot + Environment.NewLine);
            return Task.CompletedTask;
        }

    }
}
