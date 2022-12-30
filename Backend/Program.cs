using AutoMapper;
using Backend.AutoMapper;
using Backend.Data;
using Backend.Interfaces;
using Backend.Models;
using Backend.Repositories;
using Backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    //Bd
    string con = builder.Configuration.GetConnectionString("Connection");
    var secret = builder.Configuration["Secret"];
    con = con.Replace("{nass}", secret);
    builder.Services.AddDbContext<Context>(options => options.UseMySql(con, ServerVersion.AutoDetect(con)));

    //Jwt
    builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(JwtSettings.SectionName));

    //AutoMapper convierte DTO para clases y clases para DTO
    var mapperConfig = new MapperConfiguration(mc =>
    {
        mc.AddProfile(new AutoMapperConfig());
    });

    IMapper mapper = mapperConfig.CreateMapper();
    builder.Services.AddSingleton(mapper);

    //Services
    builder.Services.AddSingleton<IJwtTokenGeneratorInterface, JwtTokenGeneratorService>();

    //Interfaces
    builder.Services.AddScoped<IUserInterface, UserRepository>();
    builder.Services.AddScoped<IAuthInterface, AuthRepository>();

    //AuthToken
    builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = !builder.Environment.IsDevelopment();
                x.SaveToken = true;
                x.IncludeErrorDetails = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["JwtSettings:Secret"] ?? "")),
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JwtSettings:Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
}

var app = builder.Build();
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        try
        {
            var context = services.GetRequiredService<Context>();

            await DbInitializer.Initialize(context);
        }
        catch (Exception ex)
        {
            string erroBD = ex.Message.ToString();
        }
    }

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
