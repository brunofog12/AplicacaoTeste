using BibliotecaBruno;
using Caelum.Stella.CSharp.Format;
using Caelum.Stella.CSharp.Http;
using Caelum.Stella.CSharp.Inwords;
using Caelum.Stella.CSharp.Validation;
using Caelum.Stella.CSharp.Vault;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace Serializar
{
    class Program
    {
        private static JavaScriptSerializer serializeJson;

        static void Main(string[] args)
        {
            Usuario usuario = new Usuario()
            {
                Nome = "Bruno Gomes",
                Cpf = "91634571304",
                Email = "bruno.gomes@fitbank.com.br"
            };

            Estabelecimento estabelecimento = new Estabelecimento()
            {
                ID = 1,
                Cnpj = "01236481616413",
                RazaoSocial = "Empresa Teste"
            };

            Estabelecimento estabelecimento2 = new Estabelecimento()
            {
                ID = 2,
                Cnpj = "41313131",
                RazaoSocial = "Empresa 02"
            };

            ValidaCpf(usuario.Cpf);
            ValidaCnpj(estabelecimento.Cnpj);
            ValidaTitulo("15948612");

            double valor = 7536.80;
            string numeroExtenso = new Numero(valor).Extenso();
            Console.WriteLine(numeroExtenso);
            string moedaExtenso = new MoedaBRL(valor).Extenso();
            Console.WriteLine(moedaExtenso);
            Money dinheiro = new Money(Currency.BRL, 10.56);
            Console.WriteLine(dinheiro.ToString());

            string cep = "60357161";
            //string resultado = GetEndereco(cep);
            //Console.WriteLine(resultado);

            try
            {
                string cepJason = new ViaCEP().GetEnderecoJson(cep);
                var cepEndereco = new ViaCEP().GetEndereco(cep);
                Console.WriteLine(cepJason);
                Console.WriteLine(cepEndereco.Bairro);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
            

            Console.ReadKey();

            StreamWriter stream = new StreamWriter(@"C:\Users\bruno\source\repos\Serializar\xmlUsuario.xml");
            StreamWriter streamjs = new StreamWriter(@"C:\Users\bruno\source\repos\Serializar\jsUsuario.json");

            XmlSerializer serialUsuario = new XmlSerializer(typeof(Usuario));
            serialUsuario.Serialize(stream, usuario);

            XmlSerializer serialEstabelecimento = new XmlSerializer(typeof(Estabelecimento));
            serialEstabelecimento.Serialize(stream, estabelecimento);
            serialEstabelecimento.Serialize(stream, estabelecimento2);


            serializeJson = new JavaScriptSerializer();
            string objSerializeJson = serializeJson.Serialize(usuario);
            string objSerializeJson2 = serializeJson.Serialize(estabelecimento);
            string objSerializeJson3 = serializeJson.Serialize(estabelecimento2);

            streamjs.WriteLine(objSerializeJson);
            streamjs.WriteLine(objSerializeJson2);
            streamjs.WriteLine(objSerializeJson3);
            streamjs.Close();
        }

        private static string GetEndereco(string cep)
        {
            string urlCep = "http://viacep.com.br/ws/" + cep + "/json/";
            string resultado = new HttpClient().GetStringAsync(urlCep).Result;
            return resultado;
        }

        private static void HttpClient()
        {
            throw new NotImplementedException();
        }

        private static void ValidaCpf(string cpf)
        {
            try
            {
                new CPFValidator().AssertValid(cpf);
                string cpfFormatado = new CPFFormatter().Format(cpf);
                Console.WriteLine("O CPF " + cpfFormatado + " é válido. ");
            }
            catch (System.Exception ex)
            {
                string cpfFormatado = new CPFFormatter().Format(cpf);
                Console.WriteLine("O CPF " + cpfFormatado + " é inválido. " + ex.Message);
            }
        }

        private static void ValidaCnpj(string cnpj)
        {
            try
            {
                new CNPJValidator().AssertValid(cnpj);
                string cnpjFormatado = new CNPJFormatter().Format(cnpj);
                Console.WriteLine("O CNPJ " + cnpjFormatado + " é válido. ");
            }
            catch (System.Exception ex)
            {
                string cnpjFormatado = new CNPJFormatter().Format(cnpj);
                Console.WriteLine("O CNPJ " + cnpjFormatado + " é inválido. " + ex.Message);
            }
        }

        private static void ValidaTitulo(string titulo)
        {
            try
            {
                new TituloEleitoralValidator().AssertValid(titulo);
                string tituloFormatado = new TituloEleitoralFormatter().Format(titulo);
                Console.WriteLine("O título " + tituloFormatado + " é válido. ");
            }
            catch (System.Exception ex)
            {
                string tituloFormatado = new TituloEleitoralFormatter().Format(titulo);
                Console.WriteLine("O título " + tituloFormatado + " é inválido. " + ex.Message);
            }
        }


    }
}
