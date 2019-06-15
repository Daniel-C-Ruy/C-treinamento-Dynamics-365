using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using System.Activities;
using Microsoft.Xrm.Sdk.Workflow;

namespace Treinamento.Workflow
{
    public class CalculaComissao : CodeActivity
    {
        [Input("Percentual de comissao")]
        public InArgument<Decimal> In_ComissaoInicial { get; set; }

        [Input ("Valor total")]
        public InArgument<Decimal> In_ValorTotal { get; set; }


        [Output("Valor calculado")]
        public OutArgument<Decimal> Out_ValorCalculado { get; set; }

        protected override void Execute(CodeActivityContext ExecutionContext)
        {
            IWorkflowContext context = ExecutionContext.GetExtension<IWorkflowContext>();
            IOrganizationServiceFactory serviceFactory = ExecutionContext.GetExtension<IOrganizationServiceFactory>();
            IOrganizationService orgService = serviceFactory.CreateOrganizationService(context.InitiatingUserId);


            Decimal valorTotal = ExecutionContext.GetValue<Decimal>(In_ValorTotal);
            Decimal percentualComissao = ExecutionContext.GetValue<Decimal>(In_ComissaoInicial);

            Decimal comissaoCalculada = 0.0M;
            comissaoCalculada = percentualComissao * valorTotal;
            ExecutionContext.SetValue(Out_ValorCalculado, comissaoCalculada);
        }
    }
}
