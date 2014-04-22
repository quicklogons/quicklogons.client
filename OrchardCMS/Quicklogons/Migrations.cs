using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;
using Orchard.Environment.Extensions;
using System.Data;

namespace Quicklogons
{
    [OrchardFeature("Quicklogons")]
    public class Migrations : DataMigrationImpl
    {
        public int Create()
        {
            SchemaBuilder.CreateTable(
                "QuicklogonsSettingsPartRecord",
                table => table.ContentPartRecord()
                              .Column("Url", DbType.String, command => command.WithLength(1024))
                              .Column("SecureUrl", DbType.String, command => command.WithLength(1024))
                              .Column("SiteKey", DbType.String, command => command.WithLength(255))
                              .Column("EncryptedSiteSecret", DbType.String, command => command.WithLength(512))
                              .Column("HashAlgorithm", DbType.String, command => command.WithLength(255)));
            return 2;
        }

        public int UpdateFrom2()
        {
            SchemaBuilder.CreateTable(
                "UserProfilePartRecord",
                table => table.ContentPartRecord()
                              .Column("Name", DbType.String, command => command.WithLength(255)));

            ContentDefinitionManager.AlterPartDefinition("UserProfilePart", builder => builder.Attachable());
            ContentDefinitionManager.AlterTypeDefinition("User", alt => alt.WithPart("UserProfilePart"));

            return 3;
        }
    }
}