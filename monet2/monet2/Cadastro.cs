using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monet2 {
    public class Cadastro {
        public string nome;
        public string rua;
        public string bairro;
        public string cidade;
        public string uf;
        public string cep;
        public string desconto;
        public string telefone;

        public Cadastro(string nome, string rua, string bairro, string cidade, string uf, string cep, string desconto, string telefone)
        {
            this.nome = nome;
            this.rua = rua;
            this.bairro = bairro;
            this.cidade = cidade;
            this.uf = uf;
            this.cep = cep;
            this.desconto = desconto;
            this.telefone = telefone;
        }

        public Cadastro(string nome, string rua, string bairro, string cidade, string uf, string cep) {
            this.nome = nome;
            this.rua = rua;
            this.bairro = bairro;
            this.cidade = cidade;
            this.uf = uf;
            this.cep = cep;
        }

    }
}
