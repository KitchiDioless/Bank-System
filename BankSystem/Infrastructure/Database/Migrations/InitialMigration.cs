using FluentMigrator;

namespace BankApp.Infrastructure.Database.Migrations;

[Migration(1, "Initial")]
public class InitialMigration : Migration
{
    public override void Up()
    {
        Create.Table("Users")
            .WithColumn("Id").AsString().PrimaryKey()
            .WithColumn("Password").AsString();

        Create.Table("Accounts")
            .WithColumn("AccountNumber").AsString().PrimaryKey()
            .WithColumn("UserId").AsString().ForeignKey("Users", "Id")
            .WithColumn("PinCode").AsString()
            .WithColumn("Balance").AsDouble();
    }

    public override void Down()
    {
        Delete.Table("Accounts");
        Delete.Table("Users");
    }
}