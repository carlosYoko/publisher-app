using Microsoft.EntityFrameworkCore;
using Publisher.Data;

using PubContext _context = new();

FindAndUpdate();
void FindAndUpdate()
{
    var _context = new PubContext();

    //var author = _context.Authors.FirstOrDefault(a => a.FirstName == "Gabriel" && a.LastName == "Garcia");
    var author = _context.Authors.AsNoTracking().Where(a => a.FirstName == "Carlos");
    //Console.WriteLine(author.FirstName);


    //if (author != null)
    //{
    //    author.LastName = "Garcia Marquez";
    //    _context.SaveChanges();

    //}
}
