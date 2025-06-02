using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PubSub.OcppServer.Adapters;
using PubSub.OcppServer.Data;
using PubSub.OcppServer.Data.Interfaces;
using PubSub.OcppServer.OcppMessageIncomingHandlers.v16;
using PubSub.OcppServer.OcppMessageIncomingHandlers.v201;
using PubSub.OcppServer.Profiles;
using PubSub.OcppServer.Services;
using PubSub.OcppServer.Services.Interfaces;
using Scalar.AspNetCore;
using OcppClientManager = PubSub.OcppServer.Services.OcppClientManager;

namespace PubSub.OcppServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddScoped<OcppHandler16>();
            builder.Services.AddScoped<OcppHandler201>();
            builder.Services.AddScoped<IOcppHandlerFactory, OcppHandlerFactory>();
            builder.Services.AddScoped<IOcppMessageSerializer, OcppMessageSerializer>();
            builder.Services.AddScoped<IRepositoryFactory, RepositoryFactory>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<ISendMessageBus, SendMessageBus>();

            builder.Services.AddOptions<AuthMessageSenderOptions>()
                .Bind(builder.Configuration
                    .GetSection("AuthMessageSenderOptions"));
            builder.Services.AddOptions<JwtOptions>()
                .Bind(builder.Configuration
                    .GetSection("JwtOptions"));
      
               
                builder.Services.AddDbContext<ChargingContext>(options =>
                    options.UseNpgsql(builder.Configuration
                            .GetConnectionString("PgsqlConnection"),
                        opt => opt.CommandTimeout(180)));
      

            builder.Services.AddScoped<IOcppServer, Services.OcppServer>();
            builder.Services.AddScoped<IOcppMessageDispatcher, OcppMessageDispatcher>();
            builder.Services.AddScoped<IApiHandler, ApiHandler>();
            builder.Services.AddSingleton<IOcppClientManager, OcppClientManager>();
            builder.Services.AddSingleton<IFacilityClientManager, FacilityClientManager>();
            // Register both security handlers
            builder.Services.AddScoped<ISecurityProfileHandler, BasicAuthSecurityHandler>();
            builder.Services.AddScoped<ISecurityProfileHandler, MTlsSecurityHandler>();
            builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
            builder.Services.AddScoped<IUserManagementService, UserManagementService>();
            builder.Services.AddScoped<IEnergyPriceAdapter, EnergyPriceAdapter>();

            // OCPP message handlers
            builder.Services.AddScoped<OcppHandlerContext>();
            builder.Services.AddScoped<OcppMessageIncomingHandlers.v16.AuthorizeIncomingHandler>();
            builder.Services.AddScoped<OcppMessageIncomingHandlers.v201.AuthorizeIncomingHandler>();
            builder.Services.AddScoped<OcppMessageIncomingHandlers.v16.BootNotificationIncomingHandler>();
            builder.Services.AddScoped<OcppMessageIncomingHandlers.v201.BootNotificationIncomingHandler>();

            builder.Services.AddScoped<DataTransferIncomingHandler>();
            builder.Services.AddScoped<DiagnosticStatusNotificationIncomingHandler>();
            builder.Services.AddScoped<FirmwareStatusNotificationIncomingHandler>();
            builder.Services.AddScoped<GetCertificateStatusIncomingHandler>();
            builder.Services.AddScoped<OcppMessageIncomingHandlers.v16.HeartbeatIncomingHandler>();
            builder.Services.AddScoped<OcppMessageIncomingHandlers.v201.HeartbeatIncomingHandler>();
            builder.Services.AddScoped<MeterValuesIncomingHandler>();
            builder.Services.AddScoped<OcppMessageIncomingHandlers.v16.SecurityEventNotificationIncomingHandler>();
            builder.Services.AddScoped<OcppMessageIncomingHandlers.v201.SecurityEventNotificationIncomingHandler>();
            builder.Services.AddScoped<StartTransactionIncomingHandler>();
            builder.Services.AddScoped<OcppMessageIncomingHandlers.v16.StatusNotificationIncomingHandler>();
            builder.Services.AddScoped<OcppMessageIncomingHandlers.v201.StatusNotificationIncomingHandler>();
            builder.Services.AddScoped<StopTransactionIncomingHandler>();

            builder.Services.AddScoped<IOcppRequestManager, OcppRequestManager>();
            builder.Services.AddScoped<IChargingProfileService, ChargingProfileService>();

            builder.Services.AddControllers();
            
            builder.Services.AddAutoMapper(typeof(Program));
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowedSpecificOrigin", builder =>
                    {
                        builder.WithOrigins("https://yourdomain.com", "http://localhost:3000")
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                    });
            });
            builder.Services.AddAuthentication(cfg => {
                cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                cfg.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x => {
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = async ctx =>
                    {
                        var zzz = "";
                        var exceptionMessage = ctx.Exception;
                    }
                };
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8
                            .GetBytes(builder.Configuration["JwtOptions:JWTSecret"])
                    ),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", policy =>
                    policy.RequireClaim("IsAdmin", "True"));
            });
            builder.Services.AddTransient<IEmailSender, EmailSender>();
            builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddMemoryCache();
            builder.Services.AddOpenApi();
            builder.Services.AddEndpointsApiExplorer();
            


            var app = builder.Build();
            
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }
            app.MapControllers();
            app.MapControllerRoute(
                name: "default",
                pattern: "ocpp/{controller=Home}/{action=Index}/{id?}");

            app.UseAuthentication();
            app.UseRouting();
            app.UseCors("AllowedSpecificOrigin");
            app.UseAuthorization();
          
            app.UseWebSockets();
            app.UseDeveloperExceptionPage();
           
            app.Run();
            
        }
        
    }
}

