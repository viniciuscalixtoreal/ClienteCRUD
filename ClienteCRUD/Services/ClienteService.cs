using ClienteCRUD.Data;
using ClienteCRUD.Models;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Collections.Generic;

namespace ClienteCRUD.Services
{
    public class ClienteService
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
            var tel = cliente.Telefones;
            var key = $"cliente:{cliente.Id}";

            if (cliente.Id == 0)
            {
                var cli = new Cliente { Nome = cliente.Nome, Endereco = cliente.Endereco, Sexo = cliente.Sexo };
                cliente = _repository.Salvar(cli);

                foreach (var item in tel)
                {
                    item.Cliente = cliente;
                    item.Ativo = true;
                }

                cliente.Telefones = tel;
                _repository.Salvar(cliente);
                _redis.StringSet(key, JsonConvert.SerializeObject(cliente));
            }
            else
            {
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
