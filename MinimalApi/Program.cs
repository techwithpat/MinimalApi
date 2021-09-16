using Microsoft.EntityFrameworkCore;
using MinimalApi;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BookContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("BookDb")));
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/api/books", async (BookContext db) => await db.Books.ToListAsync());

app.MapGet("/api/books/{id}", async (BookContext db, int id) => await db.Books.FindAsync(id));

app.MapPost("/api/books", async (BookContext db, Book book) => 
{
    await db.Books.AddAsync(book);
    await db.SaveChangesAsync();
    Results.Accepted();
});

app.MapPut("/api/books/{id}", async (BookContext db, int id, Book book) => 
{
    if (id != book.Id) return Results.BadRequest();

    db.Update(book);
    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/api/books/{id}", async (BookContext db, int id) => 
{
    var book = await db.Books.FindAsync(id);
    if (book == null) return Results.NotFound();

    db.Books.Remove(book);
    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.Run();
