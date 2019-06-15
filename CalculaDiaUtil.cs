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
    public class CalculaDiaUtil : CodeActivity
    {
        [Input("Data inicial")]
        public InArgument<DateTime> In_DataInicial { get; set; }

        [Output("Proxima data util")]
        public OutArgument<DateTime> Out_DataCalculada { get; set; }

        protected override void Execute(CodeActivityContext ExecutionContext)
        {
            IWorkflowContext context = ExecutionContext.GetExtension<IWorkflowContext>();
            IOrganizationServiceFactory serviceFactory = ExecutionContext.GetExtension<IOrganizationServiceFactory>();
            IOrganizationService orgService = serviceFactory.CreateOrganizationService(context.InitiatingUserId);

            DateTime dataInicial = ExecutionContext.GetValue <DateTime> (In_DataInicial);

            DateTime dataCalculada = new DateTime();
            dataCalculada = VerificaDiaUtil(dataInicial);

            DateTime dataHoraCalculada = new DateTime(dataCalculada.Year, dataCalculada.Month, dataCalculada.Day, 9, 0, 0);
            ExecutionContext.SetValue(Out_DataCalculada, dataHoraCalculada);

        }
        public static DateTime VerificaDiaUtil(DateTime data)
        {
            if (data.DayOfWeek == DayOfWeek.Saturday)
            {
                data = data.AddDays(2);
                return data;
            }
            else if (data.DayOfWeek == DayOfWeek.Friday)
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
 