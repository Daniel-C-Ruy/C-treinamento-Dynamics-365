using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Entities; //lead e task.

namespace Treinamento
{

    public class CriarTarefaParaLead : IPlugin
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
                Entity lead = (Entity)context.InputParameters["Target"];
                Guid leadID = lead.Id;
                String NomeLead = String.Empty;

                if (lead.Contains("fullname"))
                {
                    NomeLead = lead["fullname"].ToString();
                }
                
                EntityReference referencia = new EntityReference(lead.LogicalName, leadID);

                Entity novaTarefa = new Entity("task");
                novaTarefa["scheduledend"] = (DateTime)DateTime.Now.AddDays(1);
                novaTarefa["subject"] = "Realizar ligação para " + NomeLead;
                novaTarefa["description"] = "Falar com " + NomeLead;
                novaTarefa["regardingobjectid"] = referencia;
                novaTarefa["actualdurationminutes"] = 60;
                _orgService.Create(novaTarefa);
            }
        }
    }
}
