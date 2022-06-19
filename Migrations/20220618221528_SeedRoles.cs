using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarritoDeCompras.Migrations
{
    public partial class SeedRoles : Migration
    {
        private string RolAdministrador = Guid.NewGuid().ToString();
        private string RolCliente = Guid.NewGuid().ToString();

        private string usuario1Id = Guid.NewGuid().ToString();
        private string usuario2Id = Guid.NewGuid().ToString();
        private string usuario3Id = Guid.NewGuid().ToString();
        private string usuario4Id = Guid.NewGuid().ToString();
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            SeedAspNetRolesSQL(migrationBuilder);
            SeedAspNetUsers(migrationBuilder);
            SeedAspNetUserRoles(migrationBuilder);

        }

        private void SeedAspNetRolesSQL(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@$"INSERT INTO [AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp])
            VALUES ('{RolAdministrador}', 'Administrador', 'ADMINISTRADOR', null)");
            migrationBuilder.Sql(@$"INSERT INTO [AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp])
            VALUES ('{RolCliente}', 'Cliente', 'CLIENTE', null)");
        }

        private void SeedAspNetUsers(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@$"INSERT INTO [AspNetUsers] ([Id], [Nombre], [Apellido], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed],
            [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount])
            VALUES 
            ('{usuario1Id}', 'Usuario', 'NroUno', 'usuario1@gmail.com', 'USUARIO1@GMAIL.COM', 'usuario1@gmail.com', 'USUARIO1@GMAIL.COM', 0,
            'AQAAAAEAACcQAAAAEJicpfZVSykbIhpx5sS8yEksCq5iKxltyRnIGmUN1et8v0nIQirC9rTBIB6jeO5hRA==',
            '4IIR7S3IM2ZL7WLZQDRTUDXHF3NGDKK3', 'c33584bb-3118-4122-97d9-bda9664939b0', null, 0, 0, null, 1, 0)");

            migrationBuilder.Sql(@$"INSERT INTO [AspNetUsers] ([Id], [Nombre], [Apellido], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed],
            [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount])
            VALUES 
            ('{usuario2Id}', 'Usuario', 'NroDos', 'usuario2@gmail.com', 'USUARIO2@GMAIL.COM', 'usuario2@gmail.com', 'USUARIO2@GMAIL.COM', 0, 
            'AQAAAAEAACcQAAAAEJicpfZVSykbIhpx5sS8yEksCq5iKxltyRnIGmUN1et8v0nIQirC9rTBIB6jeO5hRA==',
            '4IIR7S3IM2ZL7WLZQDRTUDXHF3NGDKK3', 'c33584bb-3118-4122-97d9-bda9664939b0', null, 0, 0, null, 1, 0)");

            migrationBuilder.Sql(@$"INSERT INTO [AspNetUsers] ([Id], [Nombre], [Apellido], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed],
            [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount])
            VALUES 
            ('{usuario3Id}', 'Usuario', 'NroTres', 'usuario3@gmail.com', 'USUARIO3@GMAIL.COM', 'usuario3@gmail.com', 'USUARIO3@GMAIL.COM', 0, 
            'AQAAAAEAACcQAAAAEJicpfZVSykbIhpx5sS8yEksCq5iKxltyRnIGmUN1et8v0nIQirC9rTBIB6jeO5hRA==',
            '4IIR7S3IM2ZL7WLZQDRTUDXHF3NGDKK3', 'c33584bb-3118-4122-97d9-bda9664939b0', null, 0, 0, null, 1, 0)");

            migrationBuilder.Sql(@$"INSERT INTO [AspNetUsers] ([Id], [Nombre], [Apellido], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed],
            [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount])
            VALUES 
            ('{usuario4Id}', 'Usuario', 'NroCuatro', 'usuario4@gmail.com', 'USUARIO4@GMAIL.COM', 'usuario4@gmail.com', 'USUARIO4@GMAIL.COM', 0, 
            'AQAAAAEAACcQAAAAEJicpfZVSykbIhpx5sS8yEksCq5iKxltyRnIGmUN1et8v0nIQirC9rTBIB6jeO5hRA==',
            '4IIR7S3IM2ZL7WLZQDRTUDXHF3NGDKK3', 'c33584bb-3118-4122-97d9-bda9664939b0', null, 0, 0, null, 1, 0)");
        }

        private void SeedAspNetUserRoles(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@$"INSERT INTO [AspNetUserRoles] ([UserId], [RoleId])
            VALUES ('{usuario1Id}', '{RolAdministrador}')");

            migrationBuilder.Sql(@$"INSERT INTO [AspNetUserRoles] ([UserId], [RoleId])
            VALUES ('{usuario1Id}', '{RolCliente}')");

            migrationBuilder.Sql(@$"INSERT INTO [AspNetUserRoles] ([UserId], [RoleId])
            VALUES ('{usuario2Id}', '{RolCliente}')");

            migrationBuilder.Sql(@$"INSERT INTO [AspNetUserRoles] ([UserId], [RoleId])
            VALUES ('{usuario3Id}', '{RolCliente}')");

            migrationBuilder.Sql(@$"INSERT INTO [AspNetUserRoles] ([UserId], [RoleId])
            VALUES ('{usuario4Id}', '{RolCliente}')");
        }

        

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
