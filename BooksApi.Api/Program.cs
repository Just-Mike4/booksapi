using BooksApi.Api.Data;
using BooksApi.Api.Models;
using BooksApi.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.ComponentModel.DataAnnotations;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseInMemoryDatabase("BooksApiDb"));

// builder.Services.AddDbContext<ApplicationDbContext>(options =>
//     options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")));

// "ConnectionStrings": {
  //   "DefaultConnection": "Server=localhost;port=3306;Database=booksapidb;Uid=root;Pwd=pass;"
  // }

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IBookService, BookService>();

var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.ASCII.GetBytes(jwtSettings["Key"] ?? throw new InvalidOperationException("JWT Key not configured"));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidateAudience = true,
            ValidAudience = jwtSettings["Audience"],
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Books API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
{
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
}
);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();

app.UseAuthentication(); 
app.UseAuthorization();


app.MapControllers();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated();
}


var bookCreateDtoType = typeof(BooksApi.Api.Models.BookCreateDto);
var publicationYearPropertyCreate = bookCreateDtoType.GetProperty(nameof(BooksApi.Api.Models.BookCreateDto.PublicationYear));
if (publicationYearPropertyCreate != null)
{
    var rangeAttribute = publicationYearPropertyCreate.GetCustomAttributes(typeof(RangeAttribute), false).FirstOrDefault() as RangeAttribute;
}

var bookUpdateDtoType = typeof(BooksApi.Api.Models.BookUpdateDto);
var publicationYearPropertyUpdate = bookUpdateDtoType.GetProperty(nameof(BooksApi.Api.Models.BookUpdateDto.PublicationYear));
if (publicationYearPropertyUpdate != null)
{
    var rangeAttribute = publicationYearPropertyUpdate.GetCustomAttributes(typeof(RangeAttribute), false).FirstOrDefault() as RangeAttribute;
}

app.Run();