using ClienteCRUD.Data;
using ClienteCRUD.Models;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Collections.Generic;

namespace ClienteCRUD.Services
{
    public class ClienteService : IClienteService
    {
        private readonly ClienteRepository _repository;
        private readonly IDatabase _redis;

        public ClienteService(ClienteRepository repository)
        {
            _repository = repository;
            var conn = ConnectionMultiplexer.Connect("localhost:6379");
            _redis = conn.GetDatabase();
        }

        public List<Cliente> ObterTodos()
        {
            return _repository.ObterTodos();
        }

        public Cliente ObterPorId(int id)
        {
            var key = $"cliente:{id}";
            var json = _redis.StringGet(key);
            if (json.HasValue)
                return JsonConvert.DeserializeObject<Cliente>(json);

            var cliente = _repository.ObterPorId(id);
            if (cliente != null)
                _redis.StringSet(key, JsonConvert.SerializeObject(cliente));

            return cliente;
        }

        public void Salvar(Cliente cliente)
        {
            var telefones = cliente.Telefones;
            var key = $"cliente:{cliente.Id}";

            if (cliente.Id == 0)
            {
                var novoCliente = new Cliente
                {
                    Nome = cliente.Nome,
                    Endereco = cliente.Endereco,
                    Sexo = cliente.Sexo
                };

                novoCliente = _repository.Salvar(novoCliente);

                foreach (var telefone in telefones)
                {
                    if (!string.IsNullOrWhiteSpace(telefone.Numero))
                    {
                        telefone.Cliente = novoCliente;
                        telefone.Ativo = true;
                        telefone.ClienteId = novoCliente.Id;
                    }
                }

                novoCliente.Telefones = telefones;

                _repository.Salvar(novoCliente);
                _redis.StringSet($"cliente:{novoCliente.Id}", JsonConvert.SerializeObject(novoCliente));
            }
            else
            {
                foreach (var telefone in cliente.Telefones)
                {
                    telefone.Cliente = cliente;
                    telefone.ClienteId = cliente.Id;
                }

                _repository.Salvar(cliente);
                _redis.StringSet(key, JsonConvert.SerializeObject(cliente));
            }
        }

        public void Deletar(int id)
        {
            _repository.Deletar(id);
            _redis.KeyDelete($"cliente:{id}");
        }
    }
}
