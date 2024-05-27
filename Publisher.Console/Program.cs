using Publisher.Data;

using var _context = new PubContext();

var author = _context.Authors.Find(1);

Console.WriteLine(author.FirstName);