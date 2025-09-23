using ClienteCRUD.Data.NHibernate;
using ClienteCRUD.Models;
using System.Collections.Generic;
using System.Linq;

namespace ClienteCRUD.Data
{
    public class ClienteRepository : IClienteRepository
    {
        public List<Cliente> ObterTodos()
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                return session.Query<Cliente>().ToList();
            }
        }

        public Cliente ObterPorId(int id)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                return session.Get<Cliente>(id);
            }
        }

        public Cliente Salvar(Cliente cliente)
        {
            using (var session = NHibernateHelper.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                session.SaveOrUpdate(cliente);
                transaction.Commit();
                return cliente;
            }
        }

        public void Deletar(int id)
        {
            using (var session = NHibernateHelper.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var cliente = session.Get<Cliente>(id);
                if (cliente != null)
                {
                    session.Delete(cliente);
                    transaction.Commit();
                }
            }
        }
    }
}
