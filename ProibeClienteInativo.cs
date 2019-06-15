using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Entities; //lead e task
using Microsoft.Xrm.Sdk.Query;


namespace Treinamento.Plugins
{
    public class ProibeClienteInativo : IPlugin
    {
        //Variável global pra org service:
        public static IOrganizationService _orgService;

        //Cabeçalho padrão do plugin:
        public void Execute(IServiceProvider serviceProvider)
        {

            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            _orgService = serviceFactory.CreateOrganizationService(context.UserId);

            Entity venda = (Entity)context.InputParameters["Target"];
            Guid vendaID = venda.Id;
            String NomeVenda = String.Empty;

            EntityReference referencia = (EntityReference)venda["smart_lp_cliente"];

            Entity retrieveConta = new Entity();
            ColumnSet attributes = new ColumnSet(new string[] { "statecode" });
            retrieveConta = _orgService.Retrieve("account", referencia.Id, attributes);

            OptionSetValue opcao = (OptionSetValue) retrieveConta["statecode"];

            if (opcao.Value == 1)
            {
                throw new Exception("Não pode realizar venda para uma conta inativa");
            }

        }

    }
}
