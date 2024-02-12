namespace SupplyManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Database : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tb_m_accounts",
                c => new
                    {
                        guid = c.Guid(nullable: false),
                        password = c.String(),
                        otp = c.Int(nullable: false),
                        status = c.Int(nullable: false),
                        is_used = c.Boolean(nullable: false),
                        expired_time = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        role_guid = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.guid)
                .ForeignKey("dbo.tb_m_company", t => t.guid)
                .ForeignKey("dbo.tb_m_roles", t => t.role_guid, cascadeDelete: true)
                .Index(t => t.guid)
                .Index(t => t.role_guid);
            
            CreateTable(
                "dbo.tb_m_company",
                c => new
                    {
                        guid = c.Guid(nullable: false),
                        name = c.String(maxLength: 100),
                        address = c.String(maxLength: 100),
                        email = c.String(maxLength: 50),
                        phone_number = c.String(maxLength: 15),
                        foto = c.String(),
                    })
                .PrimaryKey(t => t.guid)
                .Index(t => t.email, unique: true)
                .Index(t => t.phone_number, unique: true);
            
            CreateTable(
                "dbo.tb_m_vendor",
                c => new
                    {
                        guid = c.Guid(nullable: false),
                        bidang_usaha = c.String(maxLength: 100),
                        jenis_perusahaan = c.String(maxLength: 50),
                        status_vendor = c.Int(nullable: false),
                        company_guid = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.guid)
                .ForeignKey("dbo.tb_m_company", t => t.company_guid, cascadeDelete: true)
                .Index(t => t.company_guid);
            
            CreateTable(
                "dbo.tb_m_roles",
                c => new
                    {
                        guid = c.Guid(nullable: false),
                        name = c.String(maxLength: 25),
                    })
                .PrimaryKey(t => t.guid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tb_m_accounts", "role_guid", "dbo.tb_m_roles");
            DropForeignKey("dbo.tb_m_vendor", "company_guid", "dbo.tb_m_company");
            DropForeignKey("dbo.tb_m_accounts", "guid", "dbo.tb_m_company");
            DropIndex("dbo.tb_m_vendor", new[] { "company_guid" });
            DropIndex("dbo.tb_m_company", new[] { "phone_number" });
            DropIndex("dbo.tb_m_company", new[] { "email" });
            DropIndex("dbo.tb_m_accounts", new[] { "role_guid" });
            DropIndex("dbo.tb_m_accounts", new[] { "guid" });
            DropTable("dbo.tb_m_roles");
            DropTable("dbo.tb_m_vendor");
            DropTable("dbo.tb_m_company");
            DropTable("dbo.tb_m_accounts");
        }
    }
}
