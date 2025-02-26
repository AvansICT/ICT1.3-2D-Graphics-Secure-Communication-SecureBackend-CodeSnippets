/*  ⚠⚠⚠ This is not a full Program.cs, it only contains the snippets used to setup Identity Framework. Use the snippets in your own Program.cs ⚠⚠⚠ */

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<IdentityUser>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequiredLength = 50;
})
.AddRoles<IdentityRole>()
.AddDapperStores(options =>
{
    options.ConnectionString = sqlConnectionString;
});

// ⏫⏫⏫ This code should be used above your: var app = builder.Build(); ⏫⏫⏫

var app = builder.Build();

// ⏬⏬⏬ This code should be used belowe your: var app = builder.Build(); ⏬⏬⏬
app.UseAuthorization();
app.MapGroup("/account").MapIdentityApi<IdentityUser>();
app.MapControllers().RequireAuthorization(); // 👈 Replace your current app.MapControllers() with this one. 

app.Run();
