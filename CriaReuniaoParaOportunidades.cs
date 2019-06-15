using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Entities; //lead e task.

/*
    criar um plugin que agenda uma reunião (appointment) na criação de uma oportunidade
    a reuniao sera agendada no proximo dia util da criaçao da oportunidade
    nome da reuniao - "apresentação do produto"
*/

namespace Treinamento
{
    public class CriaReuniaoParaOportunidades : IPlugin
    {
        //Variável global pra org service:
        public static IOrganizationService _orgService;

        //Cabeçalho padrão do plugin:
        public void Execute(IServiceProvider serviceProvider)
        {

            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            _orgService = serviceFactory.CreateOrganizationService(context.UserId);

            if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity)
            {
                Entity oportunity = (Entity)context.InputParameters["Target"];
                Guid oportunityID = oportunity.Id;
                String NomeOportunidade = String.Empty;

                if (oportunity.Contains("regardingobjectid"))
                {
                    NomeOportunidade = oportunity["regardingobjectid"].ToString();
                }

                EntityReference referencia = new EntityReference(oportunity.LogicalName, oportunityID);
                DateTime horarioReuniao = VerificaDiaUtil(DateTime.Today);
                DateTime horarioDataReuniao = new DateTime(horarioReuniao.Year, horarioReuniao.Month, horarioReuniao.Day, 9, 0, 0);
                Entity novaReuniao = new Entity("appointment");

                novaReuniao["regardingobjectid"] = referencia; //lookup (Entity Reference)
                novaReuniao["subject"] = "Apresentação do produto";
                novaReuniao["scheduledstart"] = horarioDataReuniao;
                novaReuniao["scheduledend"] = horarioDataReuniao.AddMinutes(30);
                _orgService.Create(novaReuniao);
            }
        }
        public static DateTime VerificaDiaUtil(DateTime data)
        {
            if(data.DayOfWeek == DayOfWeek.Saturday)
            {
                data = data.AddDays(2);
                return data;
            } else if (data.DayOfWeek == DayOfWeek.Friday)
            {
                data = data.AddDays(3);
                return data;
            }
            else
            {
                return data.AddDays(1);
            }
        }
    }
    
}
