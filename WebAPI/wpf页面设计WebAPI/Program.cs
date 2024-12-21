using Microsoft.EntityFrameworkCore;
using wpf页面设计WebAPI.Automapper;
using wpf页面设计WebAPI.DataModel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(m =>
{
    // 显示注释
    string path = AppContext.BaseDirectory + "wpf页面设计WebAPI.xml";
    m.IncludeXmlComments(path, true);
});

// 数据库上下文 (获取在appsetting.json中配置的数据库字段)
builder.Services.AddDbContext<DailyContext>(m => m.UseSqlServer(builder.Configuration.GetConnectionString("ConStr")));

// automapper
builder.Services.AddAutoMapper(typeof(AutoMapperSetting));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   
}
//发布时移出
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
