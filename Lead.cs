using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System.ServiceModel.Description;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Tooling.Connector;
namespace NovaAplicacao.DAO
{
    public class Lead
    {

        public static void ListaLeads()
        {
            Console.WriteLine("Obtendo leads...");
            QueryExpression consultaLead = new QueryExpression("lead");
            consultaLead.ColumnSet.AddColumns("fullname");

            EntityCollection resultado = Program._orgService.RetrieveMultiple(consultaLead);
            Int32 quantidadeRegistros = resultado.Entities.Count();

            Console.WriteLine("\nQuantidade de registros encontrados: " + quantidadeRegistros + "\n");

            foreach (Entity registroLead in resultado.Entities)
            {
                Console.WriteLine("Nome do lead: " + registroLead.Attributes["fullname"].ToString());

            }
        }

        public static void ObterLeads()
        {
            String xml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                          <entity name='lead'>
                            <attribute name='fullname' />
                            <attribute name='leadid' />
                            <attribute name='emailaddress1' />
                            <attribute name='telephone1' />
                            <order attribute='fullname' descending='true' />
                          </entity>
                        </fetch>";

            EntityCollection resultado = Program._orgService.RetrieveMultiple(new FetchExpression(xml));

            foreach (Entity result in resultado.Entities)
            {
                String nome = String.Empty, telefone = String.Empty, email = String.Empty;

                if (result.Contains("fullname"))
                {
                    nome = result["fullname"].ToString();
                }
                if (result.Contains("telephone1"))
                {
                    telefone = result["telephone1"].ToString();
                }
                if (result.Contains("emailaddress1"))
                {
                    email = result["emailaddress1"].ToString();
                }

                Console.WriteLine("Nome: " + nome);
                Console.WriteLine("Telefone: " + telefone);
                Console.WriteLine("Email: " + email);
                Console.WriteLine("\n");
            }
        }

        public static void EncontrarPorEmail(String _emailRecebido)
        {
            String xml = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                          <entity name='lead'>
                            <attribute name='fullname' />
                            <attribute name='leadid' />
                            <attribute name='emailaddress1' />
                            <attribute name='telephone1' />
                            <order attribute='fullname' descending='true' />
                            <filter type='and'>
                              <condition attribute='emailaddress1' operator='eq' value='" + _emailRecebido + @"' />
                            </filter>
                          </entity>
                        </fetch>";

            EntityCollection resultado = Program._orgService.RetrieveMultiple(new FetchExpression(xml));

            foreach (Entity result in resultado.Entities)
            {
                String nome = String.Empty, telefone = String.Empty, email = String.Empty;

                if (result.Contains("fullname"))
                {
                    nome = result["fullname"].ToString();
                }
                if (result.Contains("telephone1"))
                {
                    telefone = result["telephone1"].ToString();
                }
                if (result.Contains("emailaddress1"))
                {
                    email = result["emailaddress1"].ToString();
                }

                Console.WriteLine("Nome: " + nome);
                Console.WriteLine("Telefone: " + telefone);
                Console.WriteLine("Email: " + email);
                Console.WriteLine("\n");
            }
        }
    }
}
