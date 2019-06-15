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
using NovaAplicacao.DAO;

namespace NovaAplicacao
{
    class Program
    {
        public static IOrganizationService _orgService;
        static void Main(string[] args)
        {
            Conexao.Conectar();

            //Chamando ID:
            Guid userID = SystemUser.RetornaGuidUsuario();
            Console.WriteLine("ID:" + userID.ToString());

            //Exibir informações:
            SystemUser.ExibirInformacaoUsuario(userID);

            /*
            //Atualizar o Home Phone (nome logico: homephone) | Atualizar e-mail 2 (nome logico: personalemailaddress).
            Console.WriteLine("Digite um telefone:");
            String telefone = Console.ReadLine();
            SystemUser.AtualizaTelefone(userID, telefone);
            Console.ReadLine();
            Console.WriteLine("Digite um e-mail:");
            String email = Console.ReadLine();
            SystemUser.AtualizaEmail(userID, email);
            */

            //Obter leads:
            //Lead.ListaLeads();
            Console.WriteLine("Digite o endereco de email:");
            String email = Console.ReadLine();
            Lead.EncontrarPorEmail(email);
            Console.ReadLine();
        }
    }
}
