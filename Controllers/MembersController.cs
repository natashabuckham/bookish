using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Bookish.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookish.Controllers;

public class MembersController : Controller
{
    private readonly Bookish.BookishContext bookishContext;

    public MembersController (BookishContext bookishContext)
    {
        this.bookishContext = bookishContext;
    }

    
    
}