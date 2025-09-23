using ClienteCRUD.Models;
using System.Collections.Generic;

namespace ClienteCRUD.Data
{
    public interface IClienteRepository
    {
        List<Cliente> ObterTodos();
        Cliente ObterPorId(int id);
        Cliente Salvar(Cliente cliente);
        void Deletar(int id);
    }
}
