using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using secondApi.Data;
using secondApi.Filters;

var builder = WebApplication.CreateBuilder(args);

//configure the service
//builder.Services.AddControllers(); //senza filtro validazione model

builder.Services.AddControllers(options => {
    options.Filters.Add(new ActionFilter());
});

//enable cors, disable security to allor different origins of data
builder.Services.AddCors(c => c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader())); // cors

//json serializer
builder.Services.AddControllersWithViews().AddNewtonsoftJson(options=>
options.SerializerSettings.ReferenceLoopHandling=Newtonsoft.Json.ReferenceLoopHandling.Ignore)
    .AddNewtonsoftJson(options=>options.SerializerSettings.ContractResolver=new DefaultContractResolver());

//Db context for migration in services
//builder.Services.AddDbContext<ApiDbContext>(option => option.UseSqlServer("User ID=postgres;Password=mysecretpassword;Host=localhost;Port=5432;Database=postgres;Pooling=true;"));

//link to ApiDbContext in order to see the database context and DbSet for each entity/class => table that will be created
builder.Services.AddEntityFrameworkNpgsql().AddDbContext<ApiDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("SongsAppConn")));

builder.Services.AddSwaggerGen();//swagger
builder.Services.AddMvc().AddXmlSerializerFormatters();
//configure the app services
var app = builder.Build();
app.MapControllers();


app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());//cors


app.UseSwagger();//swagger
app.UseSwaggerUI(c=> {
    c.RoutePrefix = "";
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");

});

app.Run();
