using ClienteCRUD.Models;
using System.Collections.Generic;

namespace ClienteCRUD.Services
{
    public interface IClienteService
    {
        List<Cliente> ObterTodos();
        Cliente ObterPorId(int id);
        void Salvar(Cliente cliente);
        void Deletar(int id);
    }
}
