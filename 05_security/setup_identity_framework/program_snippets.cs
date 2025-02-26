
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

// ⏫⏫⏫ This code should be used above your  var app = builder.Build(); code ⏫⏫⏫

var app = builder.Build();

// ⏬⏬⏬ This code should be used belowd your  var app = builder.Build(); code ⏬⏬⏬
app.UseAuthorization();
app.MapGroup("/account").MapIdentityApi<IdentityUser>();
app.MapControllers().RequireAuthorization();

app.Run();
