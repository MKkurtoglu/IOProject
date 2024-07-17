using Autofac;
using Autofac.Extensions.DependencyInjection;
using Base.DependencyResolvers;
using Base.Extensions;
using Base.Utilities.IoC;
using Base.Utilities.Security.Encryption;
using Base.Utilities.Security.JWT;
using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using BusinessLayer.DependencyResolvers.Autofac;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.EntityFramework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.IdentityModel.Tokens;
using Base.DependencyResolvers;



var builder = WebApplication.CreateBuilder(args);

builder.Host
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>((container) =>
    {
        container.RegisterModule(new AutofacBusinessModule());
    }) ;


// Add services to the container.

builder.Services.AddControllers();
var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();
//builder.Services.AddSingleton<IProductService,ProductManager>();
//builder.Services.AddSingleton<IProductDal, EfProductDal>();var tokenOptions = builder.Configuration.GetSection("Tokenoptions").Get<TokenOptions>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = tokenOptions.Issuer,
        ValidAudience = tokenOptions.Audience,
        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
    };
    
});

builder.Services.AddDependencyResolvers( new CoreModule()  // istediðimiz kadar module ekleyebileceðiz.
    );
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
