using ClienteCRUD.Models;
using FluentNHibernate.Mapping;

namespace ClienteCRUD.Data.NHibernate
{
    public class TelefoneMap : ClassMap<Telefone>
    {
        public TelefoneMap()
        {
            Table("Telefones");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Numero)
                .Nullable()
                .Length(15);
            Map(x => x.Ativo);

            References(x => x.Cliente)
                .Column("ClienteId")
                .Nullable();
        }
    }
}
