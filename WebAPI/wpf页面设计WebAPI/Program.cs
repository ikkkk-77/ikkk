using Microsoft.EntityFrameworkCore;
using wpfҳ�����WebAPI.Automapper;
using wpfҳ�����WebAPI.DataModel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(m =>
{
    // ��ʾע��
    string path = AppContext.BaseDirectory + "wpfҳ�����WebAPI.xml";
    m.IncludeXmlComments(path, true);
});

// ���ݿ������� (��ȡ��appsetting.json�����õ����ݿ��ֶ�)
builder.Services.AddDbContext<DailyContext>(m => m.UseSqlServer(builder.Configuration.GetConnectionString("ConStr")));

// automapper
builder.Services.AddAutoMapper(typeof(AutoMapperSetting));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   
}
//����ʱ�Ƴ�
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
