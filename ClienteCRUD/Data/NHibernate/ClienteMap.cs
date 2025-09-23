using ClienteCRUD.Models;
using FluentNHibernate.Mapping;

namespace ClienteCRUD.Data.NHibernate
{
    public class ClienteMap : ClassMap<Cliente>
    {
        public ClienteMap()
        {
            Table("Clientes");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Nome).Not.Nullable().Length(120);
            Map(x => x.Sexo).Nullable();
            Map(x => x.Endereco).Length(200);

            HasMany(x => x.Telefones)
                .KeyColumn("ClienteId")
                .Cascade.AllDeleteOrphan()
                .Inverse()
                .Not.LazyLoad();
        }
    }
}
