
using InteractivePlatform.BL.UOW;
using InteractivePlatform.DAL.Model;
using InteractivePlatform.BL.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace InteractivePlatform.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            //--------------------- Register the DbContext ---------------------//
            builder.Services.AddDbContext<InteractivePlatformContext>(op => op.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("EC")));


            //--------------------- Register UnitOfWork ---------------------//
            builder.Services.AddScoped<UnitOfWorks>();


            //------------------------validate token------------------------//

            builder.Services.AddAuthentication(option => option.DefaultAuthenticateScheme = "myscheme")
                .AddJwtBearer("myscheme",
                //validate token
                op =>
                {
                    #region secret key
                    string key = "Welcome to our First Api Website for InteractivePlatform project";
                    var secertkey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
                    #endregion
                    op.TokenValidationParameters = new TokenValidationParameters()
                    {
                        IssuerSigningKey = secertkey,
                        ValidateIssuer = false,
                        ValidateAudience = false

                    };
                }
                );
            //------------------------ Inject File Service ------------------------//
            builder.Services.AddTransient<IFileService, FileService>();

            //--------------------- Define CORS policy name ---------------------//
            string corsPolicyName = "AllowAll";


            builder.Services.AddCors(options =>
            {
                options.AddPolicy(corsPolicyName, builder =>
                {
                    builder.SetIsOriginAllowed(origin => true) // Allow any origin
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials();
                });
            });

            var app = builder.Build();
            app.UseRouting();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(builder.Environment.ContentRootPath, "Uploads")),
                RequestPath = "/Resources"
            });

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            //--------------------- Use CORS policy ---------------------//
            app.UseCors(corsPolicyName);


            app.MapControllers();

            app.Run();
        }
    }
}
