using PlaylistMigrator.Application.Interfaces;
using PlaylistMigrator.Infrastructure.Clients;
using PlaylistMigrator.Infrastructure.Mappings.YandexMusic;
using PlaylistMigrator.Infrastructure.Services.YandexMusic;
using PlaylistMigrator.web.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddTransient<YandexTrackMapper>();
builder.Services.AddTransient<YandexAlbumMapper>();
builder.Services.AddTransient<YandexArtistMapper>();
builder.Services.AddTransient<YandexPlaylistMapper>();

builder.Services.AddScoped<ISourceMusicService, YandexMusicSourceService>();

builder.Services.AddHttpClient<YandexMusicClient>(client =>
{
    client.BaseAddress = new Uri("https://api.music.yandex.ru/");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
